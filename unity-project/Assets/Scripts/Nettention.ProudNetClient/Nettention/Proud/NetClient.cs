using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ProudC2C;
using ProudC2S;
using ProudS2C;

namespace Nettention.Proud
{
	public sealed class NetClient : NetCore, IP2PGroupMember, IDisposable, ISendDest_C
	{
		public delegate void JoinServerCompleteDelegate(ErrorInfo info, ByteArray replyFromServer);

		public delegate void LeaveServerDelegate(ErrorInfo info);

		public delegate void P2PMemberJoinDelegate(HostID memberHostID, HostID groupHostID, int memberCount, ByteArray customField);

		public delegate void P2PMemberLeaveDelegate(HostID memberHostID, HostID groupHostID, int memberCount);

		public delegate void ChangeP2PRelayStateDelegate(HostID remoteHostID, ErrorType reason);

		public delegate void ChangeServerUdpStateDelegate(ErrorType reason);

		public delegate void SynchronizeServerTimeDelegate();

		private struct EmergencyLogData
		{
			public string text;

			public TraceID tid;

			public long addedTime;
		}

		private enum WorkerState
		{
			IssueConnect = 0,
			Connecting = 1,
			JustConnected = 2,
			Connected = 3,
			Disconnecting = 4,
			Disconnected = 5
		}

		private delegate bool Main_IssueConnectDelegate(ref SocketError outCode);

		private static readonly string NoServerConnectionErrorText = "Cannot send messages unless connection to server exists!";

		private Queue<EmergencyLogData> emergencyLogQueue = new Queue<EmergencyLogData>();

		private bool _disposed;

		internal object m_critSec = new object();

		internal static MessagePriority MessagePriority_Holepunch = MessagePriority.Ring99;

		private TimeAlarm reliablePing_Timer = new TimeAlarm(NetConfig.DefaultNoPingTimeoutTimeMs);

		private long tcpAndUdp_DoForShortInterval_lastTimeMs;

		private TimeAlarm removeTooOldUdpSendPacketQueueOnNeed_Timer = new TimeAlarm(NetConfig.UdpPacketBoardLongIntervalMs);

		private TimeAlarm processSendReadyRemotes_Timer = new TimeAlarm(NetConfig.EveryRemoteIssueSendOnNeedIntervalMs);

		private TimeAlarm processRecvReadyRemotes_Timer = new TimeAlarm(NetConfig.EveryRemoteRecvOnNeedIntervalMs);

		private object connectDisconnectPhaseLockObject = new object();

		internal bool supressSubsequentDisconnectionEvents;

		internal uint requestServerTimeCount;

		internal long dxServerTimeDiffMs;

		internal int serverUdpRecentPingMs;

		internal int serverUdpLastPingMs;

		internal int serverTcpRecentPingMs;

		internal int serverTcpLastPingMs;

		internal int toServerUdpSendCount;

		internal long lastReportUdpCountTimeMs;

		private int disconnectInvokeCount;

		private int connectCount;

		private long lastRequestServerTimeTimeMs;

		private P2PGroups_C p2pGroups = new P2PGroups_C();

		internal long enablePingTestEndTimeMs;

		internal long p2pConnectionTrialEndTimeMs;

		internal long p2pHolepunchIntervalMs;

		internal long m_everyRemoteIssueSendOnNeedIntervalMs = NetConfig.EveryRemoteIssueSendOnNeedIntervalMs;

		internal int internalVersion = NetConfig.InternalNetVersion;

		internal long lastFrameMoveInvokedTimeMs;

		internal HostID localHostID;

		public NetConnectionParam connectionParam;

		private TcpLayer_C toServerTcpLayer;

		private FinalUserWorkItemQueue finalUserWorkItemQueue = new FinalUserWorkItemQueue();

		private FinalUserWorkItemQueue postponedFinalUserWorkItemQueue = new FinalUserWorkItemQueue();

		internal UdpSocket_C toServerUdpSocket;

		private bool toServerUdpSocketFailed;

		private Queue<Message> loopbackFinalReceivedMessageQueue = new Queue<Message>();

		private Queue<ReceivedMessage> preFinalRecvQueue = new Queue<ReceivedMessage>();

		internal NetSettings settings = new NetSettings();

		internal FallbackableUdpLayer_C toServerUdp_fallbackable;

		internal Guid serverInstanceGuid;

		internal bool enableLog;

		private Dictionary<HostID, RemotePeer> remotePeers = new Dictionary<HostID, RemotePeer>();

		private Dictionary<HostID, RemotePeer> peerGarbages = new Dictionary<HostID, RemotePeer>();

		private List<UdpSocket_C> garbages = new List<UdpSocket_C>();

		internal Dictionary<ushort, UdpSocket_C> recycles = new Dictionary<ushort, UdpSocket_C>();

		internal ProudC2C.Proxy c2cProxy = new ProudC2C.Proxy();

		internal ProudC2S.Proxy c2sProxy = new ProudC2S.Proxy();

		internal ProudS2C.Stub s2cStub = new ProudS2C.Stub();

		internal ProudC2C.Stub c2cStub = new ProudC2C.Stub();

		internal Random random = new Random();

		private NetClientStats netClientStats = new NetClientStats();

		private long virtualSpeedHackMultiplicationMs = 1L;

		private long speedHackDetectorPingCooltimeMs = (NetConfig.EnableSpeedHackDetectorByDefault ? 0 : NetConfig.InfiniteCoolTimeMs);

		private long lastTcpStreamReceivedTimeMs;

		private long lastCheckSendQueueTimeMs;

		private long sendQueueHeavyStartTimeMs;

		private Thread workerThread;

		private volatile bool stopworkerthread;

		private ServerAsSendDest serverAsSendDest;

		private sortISendDestComparer sendDestComparer = new sortISendDestComparer();

		private sortHostIDComparer hostIDComparer = new sortHostIDComparer();

		private object hostTag;

		internal SessionKey selfP2PSessionKey = new SessionKey();

		private ushort selfEncryptCount;

		private ushort selfDecryptCount;

		private ushort toServerEncryptCount;

		private ushort toServerDecryptCount;

		private SessionKey toServerSessionKey = new SessionKey();

		private bool m_useNetworkerThread = true;

		private long lastHeartbeatTimeMs;

		private List<ushort> unusedUdpPorts = new List<ushort>();

		private List<ushort> usedUdpPorts = new List<ushort>();

		private JoinServerCompleteDelegate joinServerCompleteHandler;

		internal RuntimePlatform platformType;

		private bool m_isWebPlayer;

		private bool m_isProactorAsyncModel;

		private LeaveServerDelegate leaveServerHandler;

		private P2PMemberJoinDelegate p2pMemberJoinHandler;

		private P2PMemberLeaveDelegate p2pMemberLeaveHandler;

		private ChangeP2PRelayStateDelegate changeP2PRelayStateHandler;

		private ChangeServerUdpStateDelegate changeServerUdpStateHandler;

		private SynchronizeServerTimeDelegate synchronizeServerTimeHandler;

		internal bool holsterMoreCallback_FORONETHREADEDMODEL;

		internal bool postponeThisCallback_FORONETHREADEDMODEL;

		public ApplicationHint ApplicationHint = default(ApplicationHint);

		private long issueConnectStartTimeMs;

		private volatile int disconnectingModeHeartbeatCount;

		private long disconnectingModeStartTimeMs;

		private volatile bool disconnectingModeWarned;

		private long shutdownIssuedTimeMs;

		private long gracefulDisconnectTimeoutMs;

		private volatile WorkerState m_state_USE_FUNC = WorkerState.Disconnected;

		private AsyncCallback tcpRecvAsyncCallback;

		private AsyncCallback tcpSendAsyncCallback;

		internal AsyncCallback udpRecvAsyncCallback;

		private AsyncCallback udpSendAsyncCallback;

		private Main_IssueConnectDelegate main_IssueConnectHandler;

		private IAsyncResult connectIssueResult;

		public bool isIpV6Network;

		public JoinServerCompleteDelegate JoinServerCompleteHandler
		{
			set
			{
				joinServerCompleteHandler = value;
			}
		}

		public LeaveServerDelegate LeaveServerHandler
		{
			set
			{
				leaveServerHandler = value;
			}
		}

		public P2PMemberJoinDelegate P2PMemberJoinHandler
		{
			set
			{
				p2pMemberJoinHandler = value;
			}
		}

		public P2PMemberLeaveDelegate P2PMemberLeaveHandler
		{
			set
			{
				p2pMemberLeaveHandler = value;
			}
		}

		public ChangeP2PRelayStateDelegate ChangeP2PRelayStateHandler
		{
			set
			{
				changeP2PRelayStateHandler = value;
			}
		}

		public ChangeServerUdpStateDelegate ChangeServerUdpStateHandler
		{
			set
			{
				changeServerUdpStateHandler = value;
			}
		}

		public SynchronizeServerTimeDelegate SynchronizeServerTimeHandler
		{
			set
			{
				synchronizeServerTimeHandler = value;
			}
		}

		internal TcpLayer_C ToServerTcp
		{
			get
			{
				return toServerTcpLayer;
			}
		}

		internal UdpSocket_C ToServerUdp
		{
			get
			{
				return toServerUdpSocket;
			}
		}

		internal bool IsWebPlayer
		{
			get
			{
				return m_isWebPlayer;
			}
		}

		internal bool IsProactorAsyncModel
		{
			get
			{
				return m_isProactorAsyncModel;
			}
		}

		public bool UseNetworkerThread
		{
			get
			{
				return m_useNetworkerThread;
			}
			set
			{
				lock (connectDisconnectPhaseLockObject)
				{
					m_useNetworkerThread = value;
					if (m_useNetworkerThread && workerThread == null)
					{
						stopworkerthread = false;
						workerThread = new Thread(WorkerProc);
						workerThread.Start();
					}
					else if (!m_useNetworkerThread && workerThread != null)
					{
						stopworkerthread = true;
						workerThread.Join();
						workerThread = null;
					}
				}
			}
		}

		internal long ReliablePingTimerIntervalMs
		{
			get
			{
				return settings.defaultTimeoutTimeMs * 3 / 10;
			}
		}

		internal IPEndPoint ToServerUdpSocketLocalAddr
		{
			get
			{
				if (toServerUdpSocket == null)
				{
					return NetUtil.MakeUnassignedIPEndPoint;
				}
				return toServerUdpSocket.localAddr;
			}
		}

		internal IPEndPoint ToServerUdpSocketAddrAtServer
		{
			get
			{
				if (toServerUdpSocket != null)
				{
					return toServerUdpSocket.addrOfHereAtServer;
				}
				return NetUtil.MakeUnassignedIPEndPoint;
			}
		}

		internal long IndirectServerTimeDiffMs
		{
			get
			{
				return dxServerTimeDiffMs;
			}
		}

		long IP2PGroupMember.IndirectServerTimeDiffMs
		{
			get
			{
				return dxServerTimeDiffMs;
			}
		}

		HostID IP2PGroupMember.MemberHostID
		{
			get
			{
				return localHostID;
			}
		}

		HostID ISendDest_C.SendDestHostID
		{
			get
			{
				return localHostID;
			}
		}

		public long ServerTimeDiffMs
		{
			get
			{
				return dxServerTimeDiffMs;
			}
		}

		public IPEndPoint ServerAddrPort
		{
			get
			{
				lock (m_critSec)
				{
					if (ToServerTcp != null)
					{
						IPEndPoint iPEndPoint = (IPEndPoint)ToServerTcp.socket.RemoteEndPoint;
						return new IPEndPoint(iPEndPoint.Address, iPEndPoint.Port);
					}
					return NetUtil.MakeUnassignedIPEndPoint;
				}
			}
		}

		public long ServerTimeMs
		{
			get
			{
				lock (m_critSec)
				{
					long timeMs = PreciseCurrentTime.GetTimeMs();
					return timeMs - dxServerTimeDiffMs;
				}
			}
		}

		public bool HasServerConnection
		{
			get
			{
				ServerConnectionState output = new ServerConnectionState();
				return GetServerConnectionState(ref output) == ConnectionState.Connected;
			}
		}

		public int InternalVersion
		{
			get
			{
				return internalVersion;
			}
		}

		public IPEndPoint PublicAddress
		{
			get
			{
				lock (m_critSec)
				{
					if (ToServerTcp != null)
					{
						return new IPEndPoint(ToServerTcp.localAddrAtServer.Address, ToServerTcp.localAddrAtServer.Port);
					}
					return NetUtil.MakeUnassignedIPEndPoint;
				}
			}
		}

		public IPEndPoint TcpLocalAddr
		{
			get
			{
				lock (m_critSec)
				{
					if (ToServerTcp == null)
					{
						return NetUtil.MakeUnassignedIPEndPoint;
					}
					return new IPEndPoint(ToServerTcp.localAddr.Address, ToServerTcp.localAddr.Port);
				}
			}
		}

		public IPEndPoint UdpLocalAddr
		{
			get
			{
				lock (m_critSec)
				{
					if (ToServerUdp == null)
					{
						return NetUtil.MakeUnassignedIPEndPoint;
					}
					return new IPEndPoint(ToServerUdp.localAddr.Address, ToServerUdp.localAddr.Port);
				}
			}
		}

		public override HostID LocalHostID
		{
			get
			{
				return localHostID;
			}
		}

		public override int MessageMaxLength
		{
			get
			{
				return NetConfig.MessageMaxLength;
			}
		}

		private WorkerState State
		{
			get
			{
				return m_state_USE_FUNC;
			}
			set
			{
				if (value > m_state_USE_FUNC)
				{
					disconnectingModeHeartbeatCount = 0;
					m_state_USE_FUNC = value;
					disconnectingModeStartTimeMs = PreciseCurrentTime.GetTimeMs();
					disconnectingModeWarned = false;
				}
			}
		}

		internal override object GetCritSec()
		{
			return m_critSec;
		}

		public void HolsterMoreCallbackUntilNextFrameMove()
		{
			holsterMoreCallback_FORONETHREADEDMODEL = true;
		}

		public void PostponeThisCallback()
		{
			postponeThisCallback_FORONETHREADEDMODEL = true;
		}

		public NetClient()
		{
			serverAsSendDest = new ServerAsSendDest(this);
			InitWorker();
			InitStub();
			AttachProxy(c2cProxy);
			AttachProxy(c2sProxy);
		}

		internal long GetIndirectServerTimeMs(HostID peerHostID)
		{
			lock (m_critSec)
			{
				long timeMs = PreciseCurrentTime.GetTimeMs();
				RemotePeer peerByHostID = GetPeerByHostID(peerHostID);
				if (peerByHostID != null)
				{
					if (!peerByHostID.m_forceRelayP2P)
					{
						peerByHostID.m_jitDirectP2PNeeded = true;
					}
					return timeMs - peerByHostID.IndirectServerTimeDiffMs;
				}
				return timeMs - dxServerTimeDiffMs;
			}
		}

		internal RemotePeer GetPeerByHostID(HostID hostID)
		{
			lock (m_critSec)
			{
				RemotePeer value;
				if (remotePeers.TryGetValue(hostID, out value))
				{
					return value;
				}
			}
			return null;
		}

		internal RemotePeer GetPeerByUdpAddr(IPEndPoint UdpAddr)
		{
			lock (m_critSec)
			{
				foreach (RemotePeer value in remotePeers.Values)
				{
					if (!value.garbaged && value.p2pHolepunchedRemoteToLocalAddr.Equals(UdpAddr))
					{
						return value;
					}
				}
			}
			return null;
		}

		internal object GetHostTag(HostID hostID)
		{
			lock (m_critSec)
			{
				if (hostID == HostID.Server)
				{
					return serverAsSendDest.hostTag;
				}
				if (hostID == localHostID)
				{
					return hostTag;
				}
				RemotePeer peerByHostID = GetPeerByHostID(hostID);
				if (peerByHostID != null)
				{
					return peerByHostID.hostTag;
				}
				return null;
			}
		}

		public bool SetHostTag(HostID hostID, object setHostTag)
		{
			lock (m_critSec)
			{
				if (hostID == HostID.Server)
				{
					serverAsSendDest.hostTag = setHostTag;
					return true;
				}
				if (hostID == localHostID)
				{
					hostTag = setHostTag;
					return true;
				}
				RemotePeer peerByHostID = GetPeerByHostID(hostID);
				if (peerByHostID == null)
				{
					return false;
				}
				peerByHostID.hostTag = setHostTag;
				return true;
			}
		}

		private void RemovePeer(RemotePeer peer)
		{
			if (peer.ToPeerUdpSocket != null)
			{
				if (peer.RelayedP2P)
				{
					GarbageSocket(peer.ToPeerUdpSocket);
				}
				else
				{
					RecycleUdpSocket(peer.ToPeerUdpSocket);
				}
				peer.udpSocket = null;
			}
			peer.owner = null;
			remotePeers.Remove(peer.peerHostID);
		}

		private void AllClearRecycleToGarbage()
		{
			foreach (UdpSocket_C value in recycles.Values)
			{
				GarbageSocket(value);
			}
			recycles.Clear();
		}

		private void CleanupEvenUnstableSituation(bool clearFinalUserWorkItem)
		{
			toServerUdp_fallbackable = null;
			toServerTcpLayer = null;
			toServerUdpSocket = null;
			AllClearRecycleToGarbage();
			garbages.Clear();
			if (clearFinalUserWorkItem)
			{
				finalUserWorkItemQueue.Clear();
			}
			supressSubsequentDisconnectionEvents = false;
			requestServerTimeCount = 0u;
			dxServerTimeDiffMs = 0L;
			serverUdpRecentPingMs = 0;
			serverUdpLastPingMs = 0;
			lastRequestServerTimeTimeMs = 0L;
			p2pGroups.Clear();
			p2pConnectionTrialEndTimeMs = NetConfig.P2PHolepunchEndTimeMs;
			p2pHolepunchIntervalMs = NetConfig.P2PHolepunchIntervalMs;
			lastReportUdpCountTimeMs = NetConfig.ReportRealUdpCountIntervalMs;
			netClientStats = new NetClientStats();
			internalVersion = NetConfig.InternalNetVersion;
			settings = new NetSettings();
			serverInstanceGuid = Guid.Empty;
			preFinalRecvQueue.Clear();
			toServerUdpSocketFailed = false;
			loopbackFinalReceivedMessageQueue.Clear();
			localHostID = HostID.None;
			connectionParam = new NetConnectionParam();
			postponedFinalUserWorkItemQueue.Clear();
			lastFrameMoveInvokedTimeMs = 0L;
			remotePeers.Clear();
			peerGarbages.Clear();
			removeTooOldUdpSendPacketQueueOnNeed_Timer = new TimeAlarm(NetConfig.UdpPacketBoardLongIntervalMs);
			processSendReadyRemotes_Timer = new TimeAlarm(NetConfig.EveryRemoteIssueSendOnNeedIntervalMs);
			virtualSpeedHackMultiplicationMs = 1L;
			speedHackDetectorPingCooltimeMs = (NetConfig.EnableSpeedHackDetectorByDefault ? 0 : NetConfig.InfiniteCoolTimeMs);
			lastTcpStreamReceivedTimeMs = 0L;
			ApplicationHint.recentFrameRate = 0.0;
			lastCheckSendQueueTimeMs = 0L;
			sendQueueHeavyStartTimeMs = 0L;
			tcpAndUdp_DoForShortInterval_lastTimeMs = 0L;
			shutdownIssuedTimeMs = 0L;
			unusedUdpPorts.Clear();
			usedUdpPorts.Clear();
		}

		public bool Connect(NetConnectionParam param, bool isIpV6Network = false)
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("Resource was disposed.");
			}
			lock (connectDisconnectPhaseLockObject)
			{
				Interlocked.Increment(ref connectCount);
				lock (m_critSec)
				{
					this.isIpV6Network = isIpV6Network;
					if (State != WorkerState.Disconnected)
					{
						throw new Exception(string.Format("Wrong state({0})! Disconnect() or GetServerConnectionState() may be required.", State.ToString()));
					}
					if (toServerUdpSocket != null || toServerUdp_fallbackable != null)
					{
						throw new Exception(string.Format("Unstability in Connect #1! Process={0}", Process.GetCurrentProcess().ProcessName));
					}
					if (param.serverIP == "0.0.0.0" || param.serverPort == 0 || param.serverPort == ushort.MaxValue || param.serverIP == "255.255.255.255")
					{
						throw new Exception(ErrorInfo.TypeToString(ErrorType.UnknownAddrPort));
					}
					platformType = PlatformGetter.StaticPlatformType;
					m_isWebPlayer = false;
					switch (platformType)
					{
					case RuntimePlatform.OSXWebPlayer:
					case RuntimePlatform.WindowsWebPlayer:
						m_isWebPlayer = true;
						break;
					}
					m_isProactorAsyncModel = false;
					switch (platformType)
					{
					case RuntimePlatform.OSXEditor:
					case RuntimePlatform.OSXPlayer:
					case RuntimePlatform.WindowsPlayer:
					case RuntimePlatform.OSXWebPlayer:
					case RuntimePlatform.OSXDashboardPlayer:
					case RuntimePlatform.WindowsWebPlayer:
					case RuntimePlatform.WindowsEditor:
					case RuntimePlatform.LinuxPlayer:
						m_isProactorAsyncModel = true;
						break;
					}
					if (param.asyncModel == AsyncModel.Proactor)
					{
						m_isProactorAsyncModel = true;
					}
					else
					{
						m_isProactorAsyncModel = false;
					}
					if (param.tunedNetworkerSendIntervalMs_TEST > 0)
					{
						m_everyRemoteIssueSendOnNeedIntervalMs = param.tunedNetworkerSendIntervalMs_TEST;
					}
					else
					{
						m_everyRemoteIssueSendOnNeedIntervalMs = NetConfig.EveryRemoteIssueSendOnNeedIntervalMs;
					}
					long timeMs = PreciseCurrentTime.GetTimeMs();
					removeTooOldUdpSendPacketQueueOnNeed_Timer.Interval = NetConfig.UdpPacketBoardLongIntervalMs;
					removeTooOldUdpSendPacketQueueOnNeed_Timer.Reset(timeMs);
					processSendReadyRemotes_Timer.Interval = m_everyRemoteIssueSendOnNeedIntervalMs;
					processSendReadyRemotes_Timer.Reset(timeMs);
					reliablePing_Timer.Interval = ReliablePingTimerIntervalMs;
					reliablePing_Timer.Reset(timeMs);
					finalUserWorkItemQueue.Clear();
					postponedFinalUserWorkItemQueue.Clear();
					lastTcpStreamReceivedTimeMs = timeMs;
					p2pConnectionTrialEndTimeMs = NetConfig.P2PHolepunchEndTimeMs;
					p2pHolepunchIntervalMs = NetConfig.P2PHolepunchIntervalMs;
					netClientStats = new NetClientStats();
					lastFrameMoveInvokedTimeMs = timeMs;
					lastHeartbeatTimeMs = timeMs;
					shutdownIssuedTimeMs = 0L;
					toServerEncryptCount = 0;
					toServerDecryptCount = 0;
					lastRequestServerTimeTimeMs = 0L;
					requestServerTimeCount = 0u;
					dxServerTimeDiffMs = 0L;
					serverUdpRecentPingMs = 0;
					serverUdpLastPingMs = 0;
					localHostID = HostID.None;
					unusedUdpPorts.Clear();
					usedUdpPorts.Clear();
					for (int i = 0; i < param.localUdpPortPool.Count; i++)
					{
						if (param.localUdpPortPool[i] <= 0)
						{
							throw new Exception(ErrorInfo.TypeToString(ErrorType.InvalidPortPool));
						}
						if (unusedUdpPorts.Contains(param.localUdpPortPool[i]))
						{
							throw new Exception(ErrorInfo.TypeToString(ErrorType.InvalidPortPool));
						}
						unusedUdpPorts.Add(param.localUdpPortPool[i]);
					}
					speedHackDetectorPingCooltimeMs = (NetConfig.EnableSpeedHackDetectorByDefault ? 0 : NetConfig.InfiniteCoolTimeMs);
					selfEncryptCount = 0;
					selfDecryptCount = 0;
					toServerTcpLayer = new TcpLayer_C(this);
					supressSubsequentDisconnectionEvents = false;
					connectionParam = param.Clone();
					connectionParam.serverIP = connectionParam.serverIP.Trim();
					if (connectionParam.serverIP == "")
					{
						connectionParam.serverIP = "localhost";
					}
					toServerUdp_fallbackable = new FallbackableUdpLayer_C(this);
					m_state_USE_FUNC = WorkerState.IssueConnect;
					if (m_useNetworkerThread)
					{
						stopworkerthread = false;
						workerThread = new Thread(WorkerProc);
						workerThread.Start();
					}
					return true;
				}
			}
		}

		public void Disconnect()
		{
			Disconnect(NetConfig.DefaultGracefulDisconnectTimeoutMs, new byte[0]);
		}

		public void Disconnect(long localGracefulDisconnectTimeout, byte[] comment)
		{
			lock (connectDisconnectPhaseLockObject)
			{
				bool flag = false;
				Interlocked.Increment(ref disconnectInvokeCount);
				long ticks = DateTime.Now.Ticks;
				long num = Math.Max(localGracefulDisconnectTimeout * 2, 100000L);
				if (enableLog || settings.emergencyLogLineCount > 0)
				{
					Log(TraceID.System, string.Format("Client {0} : User call CNetClient.Disconnect.", localHostID));
				}
				int num2 = 0;
				while (true)
				{
					lock (m_critSec)
					{
						if (!m_useNetworkerThread)
						{
							Heartbeat_Work();
						}
						if (State == WorkerState.Disconnected && garbages.Count == 0)
						{
							CleanupEvenUnstableSituation(true);
							break;
						}
						if (DateTime.Now.Ticks - ticks > num * 10000000)
						{
							State = WorkerState.Disconnected;
							CleanupEvenUnstableSituation(true);
							break;
						}
						if (!flag)
						{
							flag = true;
							if (State == WorkerState.Connected)
							{
								shutdownIssuedTimeMs = PreciseCurrentTime.GetTimeMs();
								gracefulDisconnectTimeoutMs = localGracefulDisconnectTimeout;
								c2sProxy.ShutdownTcp(HostID.Server, RmiContext.ReliableSendForPN, ByteArray.CopyFrom(comment));
							}
							else if (State < WorkerState.Connected)
							{
								State = WorkerState.Disconnecting;
							}
						}
					}
					if (num2 > 0)
					{
						Thread.Sleep(10);
					}
					num2++;
				}
				if (workerThread != null)
				{
					stopworkerthread = true;
					workerThread.Join();
					workerThread = null;
				}
				lock (m_critSec)
				{
					CleanupEvenUnstableSituation(true);
				}
			}
		}

		public FrameMoveResult FrameMove()
		{
			FrameMoveResult frameMoveResult = new FrameMoveResult();
			if (!m_useNetworkerThread)
			{
				Heartbeat_Work();
			}
			if (lastFrameMoveInvokedTimeMs != -1)
			{
				lastFrameMoveInvokedTimeMs = PreciseCurrentTime.GetTimeMs();
			}
			FrameMove_PullPostponeeToFinalQueue();
			FrameMove_FinalUserWorkItem(frameMoveResult);
			return frameMoveResult;
		}

		private void FrameMove_PullPostponeeToFinalQueue()
		{
			lock (m_critSec)
			{
				while (postponedFinalUserWorkItemQueue.Count > 0)
				{
					finalUserWorkItemQueue.Enqueue(postponedFinalUserWorkItemQueue.Dequeue());
				}
			}
		}

		private void FrameMove_FinalUserWorkItem(FrameMoveResult outResult)
		{
			uint rmiProcessedCnt = 0u;
			uint eventProcessedCnt = 0u;
			FinalUserWorkItem output;
			while (PopFinalUserWorkItem(out output))
			{
				bool outHolsterMoreCallback = false;
				bool outPostponeThisCallback = false;
				DoOneUserWorkItem(output, ref outHolsterMoreCallback, ref outPostponeThisCallback, ref rmiProcessedCnt, ref eventProcessedCnt);
				if (outPostponeThisCallback)
				{
					PostponeFinalUserWorlItem(output);
				}
				if (outHolsterMoreCallback)
				{
					break;
				}
			}
			if (outResult != null)
			{
				outResult.processedMessageCount = rmiProcessedCnt;
				outResult.processedEventCount = eventProcessedCnt;
			}
		}

		private void PostponeFinalUserWorlItem(FinalUserWorkItem UWI)
		{
			lock (m_critSec)
			{
				postponedFinalUserWorkItemQueue.Enqueue(UWI);
				UWI.unsafeMessage.ReadOnlyMessage.ReadOffset = 0;
			}
		}

		private void DecreaseLeaveEventCount(HostID hostid)
		{
			lock (m_critSec)
			{
				RemotePeer peerByHostID = GetPeerByHostID(hostid);
				if (peerByHostID != null)
				{
					peerByHostID.leaveEventCount--;
				}
			}
		}

		private void DoOneUserWorkItem(FinalUserWorkItem UWI, ref bool outHolsterMoreCallback, ref bool outPostponeThisCallback, ref uint rmiProcessedCnt, ref uint eventProcessedCnt)
		{
			switch (UWI.type)
			{
			case FinalUserWorkItemType.RMI:
			{
				ReceivedMessage unsafeMessage2 = UWI.unsafeMessage;
				int readOffset = unsafeMessage2.ReadOnlyMessage.ReadOffset;
				bool flag2 = false;
				bool flag3 = false;
				object obj2 = GetHostTag(unsafeMessage2.remoteHostID);
				RmiID b = RmiID.None;
				Message readOnlyMessage2 = unsafeMessage2.ReadOnlyMessage;
				if (readOnlyMessage2.Read(out b))
				{
					for (int i = 0; i < stubList_NOCSLOCK.Count; i++)
					{
						RmiStub rmiStub = stubList_NOCSLOCK[i];
						try
						{
							readOnlyMessage2.ReadOffset = readOffset;
							rmiStub.holsterMoreCallback_FORONETHREADEDMODEL = false;
							rmiStub.postponeThisCallback_FORONETHREADEDMODEL = false;
							flag3 |= rmiStub.ProcessReceivedMessage(unsafeMessage2, obj2);
							flag2 |= flag3;
							outHolsterMoreCallback |= rmiStub.holsterMoreCallback_FORONETHREADEDMODEL;
							outPostponeThisCallback |= rmiStub.postponeThisCallback_FORONETHREADEDMODEL;
							if (flag3 && rmiStub != s2cStub && rmiStub != c2cStub)
							{
								rmiProcessedCnt++;
							}
						}
						catch (Exception e3)
						{
							if (exceptionHandler != null)
							{
								exceptionHandler(unsafeMessage2.remoteHostID, e3);
							}
						}
						flag3 = false;
					}
				}
				if (!flag2)
				{
					readOnlyMessage2.ReadOffset = readOffset;
					if (noRmiProcessedHandler != null)
					{
						eventProcessedCnt++;
						noRmiProcessedHandler(b);
					}
				}
				break;
			}
			case FinalUserWorkItemType.UserMessage:
			{
				ReceivedMessage unsafeMessage = UWI.unsafeMessage;
				object obj = GetHostTag(unsafeMessage.remoteHostID);
				Message readOnlyMessage = unsafeMessage.ReadOnlyMessage;
				if (receivedUserMessageHandler == null)
				{
					break;
				}
				try
				{
					int num = readOnlyMessage.Length - readOnlyMessage.ReadOffset;
					holsterMoreCallback_FORONETHREADEDMODEL = false;
					postponeThisCallback_FORONETHREADEDMODEL = false;
					RmiContext rmiContext = new RmiContext();
					rmiContext.sentFrom = unsafeMessage.remoteHostID;
					rmiContext.relayed = unsafeMessage.relayed;
					rmiContext.hostTag = obj;
					ByteArray byteArray = new ByteArray();
					byteArray.Count = num;
					Array.Copy(readOnlyMessage.Data.data, readOnlyMessage.ReadOffset, byteArray.data, 0, num);
					receivedUserMessageHandler(unsafeMessage.remoteHostID, rmiContext, byteArray);
					outHolsterMoreCallback |= holsterMoreCallback_FORONETHREADEDMODEL;
					outPostponeThisCallback |= postponeThisCallback_FORONETHREADEDMODEL;
					rmiProcessedCnt++;
					break;
				}
				catch (Exception e2)
				{
					if (exceptionHandler != null)
					{
						exceptionHandler(unsafeMessage.remoteHostID, e2);
					}
					break;
				}
			}
			case FinalUserWorkItemType.LocalEvent:
			{
				LocalEvent localEvent = UWI.localEvent;
				try
				{
					holsterMoreCallback_FORONETHREADEDMODEL = false;
					postponeThisCallback_FORONETHREADEDMODEL = false;
					bool flag = true;
					switch (localEvent.type)
					{
					default:
						return;
					case LocalEventType.ClientJoinCandidate:
					case LocalEventType.ClientJoinApproved:
					case LocalEventType.ClientLeaveAfterDispose:
					case LocalEventType.AddMemberAckComplete:
					case LocalEventType.GroupP2PEnabled:
					case LocalEventType.HackSuspected:
					case LocalEventType.TcpListenFail:
					case LocalEventType.P2PGroupRemoved:
					case LocalEventType.P2PDisconnected:
					case LocalEventType.UnitTestFail:
						return;
					case LocalEventType.ConnectServerSuccess:
						if (joinServerCompleteHandler != null)
						{
							ConnectLocalEvent connectLocalEvent = (ConnectLocalEvent)localEvent;
							ByteArray replyFromServer = null;
							if (connectLocalEvent.userData.data != null)
							{
								replyFromServer = new ByteArray(connectLocalEvent.userData.data, connectLocalEvent.userData.Count);
							}
							joinServerCompleteHandler(new ErrorInfo(), replyFromServer);
						}
						break;
					case LocalEventType.ConnectServerFail:
						if (joinServerCompleteHandler != null)
						{
							ConnectLocalEvent connectLocalEvent2 = (ConnectLocalEvent)localEvent;
							joinServerCompleteHandler(connectLocalEvent2.errorInfo, connectLocalEvent2.userData);
						}
						break;
					case LocalEventType.ClientServerDisconnect:
						if (leaveServerHandler != null)
						{
							leaveServerHandler(localEvent.errorInfo);
						}
						break;
					case LocalEventType.AddMember:
						if (p2pMemberJoinHandler != null)
						{
							MemberLocalEvent memberLocalEvent2 = (MemberLocalEvent)localEvent;
							p2pMemberJoinHandler(memberLocalEvent2.memberHostID, memberLocalEvent2.groupHostID, memberLocalEvent2.memberCount, memberLocalEvent2.customField);
						}
						break;
					case LocalEventType.DelMember:
						if (p2pMemberLeaveHandler != null)
						{
							MemberLocalEvent memberLocalEvent = (MemberLocalEvent)localEvent;
							p2pMemberLeaveHandler(memberLocalEvent.memberHostID, memberLocalEvent.groupHostID, memberLocalEvent.memberCount);
							DecreaseLeaveEventCount(memberLocalEvent.memberHostID);
						}
						break;
					case LocalEventType.DirectP2PEnabled:
						if (changeP2PRelayStateHandler != null)
						{
							changeP2PRelayStateHandler(localEvent.errorInfo.remote, ErrorType.Ok);
						}
						break;
					case LocalEventType.RelayP2PEnabled:
						if (changeP2PRelayStateHandler != null)
						{
							changeP2PRelayStateHandler(localEvent.errorInfo.remote, localEvent.errorInfo.errorType);
						}
						break;
					case LocalEventType.ServerUdpChanged:
						if (changeServerUdpStateHandler != null)
						{
							changeServerUdpStateHandler(localEvent.errorInfo.errorType);
						}
						break;
					case LocalEventType.SynchronizeServerTime:
						if (synchronizeServerTimeHandler != null)
						{
							synchronizeServerTimeHandler();
						}
						break;
					case LocalEventType.Error:
						if (errorHandler != null)
						{
							errorHandler(localEvent.errorInfo);
						}
						break;
					case LocalEventType.Warning:
						if (warningHandler != null)
						{
							warningHandler(localEvent.errorInfo);
						}
						break;
					}
					outHolsterMoreCallback = holsterMoreCallback_FORONETHREADEDMODEL;
					outPostponeThisCallback = postponeThisCallback_FORONETHREADEDMODEL;
					if (flag)
					{
						eventProcessedCnt++;
					}
					break;
				}
				catch (Exception e)
				{
					if (exceptionHandler != null)
					{
						exceptionHandler(localHostID, e);
					}
					break;
				}
			}
			}
		}

		private bool PopFinalUserWorkItem(out FinalUserWorkItem output)
		{
			lock (m_critSec)
			{
				if (finalUserWorkItemQueue.Count > 0)
				{
					output = finalUserWorkItemQueue.Dequeue();
					return true;
				}
				output = null;
				return false;
			}
		}

		public HostIDArray GetGroupMembers(HostID groupHostID)
		{
			HostIDArray hostIDArray = new HostIDArray();
			lock (m_critSec)
			{
				P2PGroup_C p2PGroupByHostID_Internal = GetP2PGroupByHostID_Internal(groupHostID);
				if (p2PGroupByHostID_Internal != null)
				{
					foreach (HostID key in p2PGroupByHostID_Internal.members.Keys)
					{
						hostIDArray.Add(key);
					}
				}
			}
			return hostIDArray;
		}

		private P2PGroup_C GetP2PGroupByHostID_Internal(HostID groupHostID)
		{
			lock (m_critSec)
			{
				P2PGroup_C value;
				p2pGroups.TryGetValue(groupHostID, out value);
				return value;
			}
		}

		public HostIDArray GetLocalJoinedP2PGroups()
		{
			HostIDArray hostIDArray = new HostIDArray();
			lock (m_critSec)
			{
				foreach (HostID key in p2pGroups.Keys)
				{
					hostIDArray.Add(key);
				}
				return hostIDArray;
			}
		}

		public NetClientStats GetStats()
		{
			lock (m_critSec)
			{
				NetClientStats netClientStats = this.netClientStats.Clone();
				netClientStats.remotePeerCount = remotePeers.Count;
				netClientStats.serverUdpEnabled = toServerUdp_fallbackable != null && toServerUdp_fallbackable.RealUdpEnabled;
				this.netClientStats.directP2PEnabledPeerCount = 0u;
				foreach (RemotePeer value in remotePeers.Values)
				{
					if (!value.RelayedP2P)
					{
						this.netClientStats.directP2PEnabledPeerCount++;
					}
				}
				return netClientStats;
			}
		}

		public long GetP2PServerTimeMs(HostID groupHostID)
		{
			lock (m_critSec)
			{
				int num = 1;
				long num2 = IndirectServerTimeDiffMs;
				P2PGroup_C p2PGroupByHostID_Internal = GetP2PGroupByHostID_Internal(groupHostID);
				if (p2PGroupByHostID_Internal != null)
				{
					foreach (IP2PGroupMember value in p2PGroupByHostID_Internal.members.Values)
					{
						if (value != null)
						{
							num++;
							num2 += value.IndirectServerTimeDiffMs;
						}
					}
					long num3 = num2 / num;
					long timeMs = PreciseCurrentTime.GetTimeMs();
					return timeMs - num3;
				}
				return ServerTimeMs;
			}
		}

		public IPEndPoint GetLocalUdpSocketAddr(HostID remotePeerID)
		{
			lock (m_critSec)
			{
				RemotePeer peerByHostID = GetPeerByHostID(remotePeerID);
				if (peerByHostID != null && peerByHostID.ToPeerUdpSocket != null)
				{
					return new IPEndPoint(peerByHostID.ToPeerUdpSocket.localAddr.Address, peerByHostID.ToPeerUdpSocket.localAddr.Port);
				}
				return NetUtil.MakeUnassignedIPEndPoint;
			}
		}

		public DirectP2PInfo GetDirectP2PInfo(HostID remotePeerID)
		{
			lock (m_critSec)
			{
				if (remotePeerID == HostID.Server)
				{
					return null;
				}
				RemotePeer peerByHostID = GetPeerByHostID(remotePeerID);
				if (peerByHostID == null)
				{
					return null;
				}
				if (!peerByHostID.m_forceRelayP2P)
				{
					peerByHostID.m_jitDirectP2PNeeded = true;
				}
				return peerByHostID.GetDirectP2PInfo();
			}
		}

		public NetPeerInfo GetPeerInfo(HostID peerHostID)
		{
			lock (m_critSec)
			{
				RemotePeer value;
				if (!remotePeers.TryGetValue(peerHostID, out value))
				{
					return null;
				}
				return value.ToNetPeerInfo();
			}
		}

		public ConnectionState GetServerConnectionState(ref ServerConnectionState output)
		{
			lock (m_critSec)
			{
				output.realUdpEnabled = toServerUdp_fallbackable != null && toServerUdp_fallbackable.RealUdpEnabled;
				switch (State)
				{
				case WorkerState.IssueConnect:
					return ConnectionState.Connecting;
				case WorkerState.Connecting:
					return ConnectionState.Connecting;
				case WorkerState.JustConnected:
					return ConnectionState.Connecting;
				case WorkerState.Connected:
					return ConnectionState.Connected;
				case WorkerState.Disconnecting:
					return ConnectionState.Disconnecting;
				case WorkerState.Disconnected:
					return ConnectionState.Disconnected;
				default:
					return ConnectionState.Disconnected;
				}
			}
		}

		public int GetLastUnreliablePingMs(HostID remoteHostID)
		{
			int result;
			lock (m_critSec)
			{
				if (remoteHostID == HostID.Server)
				{
					result = serverUdpLastPingMs;
				}
				else
				{
					RemotePeer peerByHostID = GetPeerByHostID(remoteHostID);
					if (peerByHostID != null)
					{
						if (!peerByHostID.m_forceRelayP2P)
						{
							peerByHostID.m_jitDirectP2PNeeded = true;
						}
						result = peerByHostID.lastPingMs;
					}
					else
					{
						P2PGroup_C p2PGroupByHostID_Internal = GetP2PGroupByHostID_Internal(remoteHostID);
						if (p2PGroupByHostID_Internal != null)
						{
							int num = 0;
							int num2 = 0;
							foreach (HostID key in p2PGroupByHostID_Internal.members.Keys)
							{
								int lastUnreliablePingMs = GetLastUnreliablePingMs(key);
								if (lastUnreliablePingMs >= 0)
								{
									num++;
									num2 += lastUnreliablePingMs;
								}
							}
							if (num > 0)
							{
								result = num2 / num;
								return result;
							}
						}
						result = -1;
					}
				}
			}
			return result;
		}

		public int GetLastReliablePingMs(HostID remoteHostID)
		{
			int result;
			lock (m_critSec)
			{
				if (remoteHostID == HostID.Server)
				{
					result = serverTcpLastPingMs;
				}
				else
				{
					RemotePeer peerByHostID = GetPeerByHostID(remoteHostID);
					if (peerByHostID != null)
					{
						if (!peerByHostID.m_forceRelayP2P)
						{
							peerByHostID.m_jitDirectP2PNeeded = true;
						}
						result = peerByHostID.lastReliablePingMs;
					}
					else
					{
						P2PGroup_C p2PGroupByHostID_Internal = GetP2PGroupByHostID_Internal(remoteHostID);
						if (p2PGroupByHostID_Internal != null)
						{
							int num = 0;
							int num2 = 0;
							foreach (HostID key in p2PGroupByHostID_Internal.members.Keys)
							{
								int lastReliablePingMs = GetLastReliablePingMs(key);
								if (lastReliablePingMs >= 0)
								{
									num++;
									num2 += lastReliablePingMs;
								}
							}
							if (num > 0)
							{
								result = num2 / num;
								return result;
							}
						}
						result = -1;
					}
				}
			}
			return result;
		}

		public double GetLastUnreliablePingSec(HostID remoteHostID)
		{
			int lastUnreliablePingMs = GetLastUnreliablePingMs(remoteHostID);
			if (lastUnreliablePingMs < 0)
			{
				return lastUnreliablePingMs;
			}
			return (double)lastUnreliablePingMs / 1000.0;
		}

		public double GetLastReliablePingSec(HostID remoteHostID)
		{
			int lastReliablePingMs = GetLastReliablePingMs(remoteHostID);
			if (lastReliablePingMs < 0)
			{
				return lastReliablePingMs;
			}
			return (double)lastReliablePingMs / 1000.0;
		}

		public int GetRecentUnreliablePingMs(HostID peerHostID)
		{
			int result;
			lock (m_critSec)
			{
				if (peerHostID == HostID.Server)
				{
					result = serverUdpRecentPingMs;
				}
				else
				{
					RemotePeer peerByHostID = GetPeerByHostID(peerHostID);
					if (peerByHostID != null)
					{
						if (!peerByHostID.m_forceRelayP2P)
						{
							peerByHostID.m_jitDirectP2PNeeded = true;
						}
						result = peerByHostID.recentPingMs;
					}
					else
					{
						P2PGroup_C p2PGroupByHostID_Internal = GetP2PGroupByHostID_Internal(peerHostID);
						if (p2PGroupByHostID_Internal != null)
						{
							int num = 0;
							int num2 = 0;
							foreach (HostID key in p2PGroupByHostID_Internal.members.Keys)
							{
								int recentUnreliablePingMs = GetRecentUnreliablePingMs(key);
								if (recentUnreliablePingMs >= 0)
								{
									num++;
									num2 += recentUnreliablePingMs;
								}
							}
							if (num > 0)
							{
								result = num2 / num;
								return result;
							}
						}
						result = -1;
					}
				}
			}
			return result;
		}

		public int GetRecentReliablePingMs(HostID peerHostID)
		{
			int result;
			lock (m_critSec)
			{
				if (peerHostID == HostID.Server)
				{
					result = serverTcpRecentPingMs;
				}
				else
				{
					RemotePeer peerByHostID = GetPeerByHostID(peerHostID);
					if (peerByHostID != null)
					{
						if (!peerByHostID.m_forceRelayP2P)
						{
							peerByHostID.m_jitDirectP2PNeeded = true;
						}
						result = peerByHostID.recentReliablePingMs;
					}
					else
					{
						P2PGroup_C p2PGroupByHostID_Internal = GetP2PGroupByHostID_Internal(peerHostID);
						if (p2PGroupByHostID_Internal != null)
						{
							int num = 0;
							int num2 = 0;
							foreach (HostID key in p2PGroupByHostID_Internal.members.Keys)
							{
								int recentReliablePingMs = GetRecentReliablePingMs(key);
								if (recentReliablePingMs >= 0)
								{
									num++;
									num2 += recentReliablePingMs;
								}
							}
							if (num > 0)
							{
								result = num2 / num;
								return result;
							}
						}
						result = -1;
					}
				}
			}
			return result;
		}

		public double GetRecentUnreliablePingSec(HostID remoteHostID)
		{
			int recentUnreliablePingMs = GetRecentUnreliablePingMs(remoteHostID);
			if (recentUnreliablePingMs < 0)
			{
				return recentUnreliablePingMs;
			}
			return (double)recentUnreliablePingMs / 1000.0;
		}

		public double GetRecentReliablePingSec(HostID remoteHostID)
		{
			int recentReliablePingMs = GetRecentReliablePingMs(remoteHostID);
			if (recentReliablePingMs < 0)
			{
				return recentReliablePingMs;
			}
			return (double)recentReliablePingMs / 1000.0;
		}

		public ErrorType GetUnreliableMessagingLossRatioPercent(HostID remotePeerID, out int outputPercent)
		{
			ErrorType result;
			lock (m_critSec)
			{
				if (remotePeerID == HostID.Server)
				{
					if (toServerUdp_fallbackable == null || !toServerUdp_fallbackable.RealUdpEnabled)
					{
						outputPercent = 0;
						result = ErrorType.Ok;
						return result;
					}
					UdpSocket_C udpSocket_C = toServerUdpSocket;
					if (udpSocket_C != null)
					{
						int unreliableMessagingLossRatioPercent = udpSocket_C.udpPacketDefragBoard.GetUnreliableMessagingLossRatioPercent(toServerUdp_fallbackable.serverAddr);
						outputPercent = unreliableMessagingLossRatioPercent;
						result = ErrorType.Ok;
						return result;
					}
				}
				if (remotePeerID == localHostID)
				{
					outputPercent = 0;
					result = ErrorType.Ok;
				}
				else
				{
					RemotePeer peerByHostID = GetPeerByHostID(remotePeerID);
					if (peerByHostID != null)
					{
						UdpSocket_C toPeerUdpSocket = peerByHostID.ToPeerUdpSocket;
						if (toPeerUdpSocket != null)
						{
							int unreliableMessagingLossRatioPercent2 = toPeerUdpSocket.udpPacketDefragBoard.GetUnreliableMessagingLossRatioPercent(peerByHostID.p2pHolepunchedRemoteToLocalAddr);
							outputPercent = unreliableMessagingLossRatioPercent2;
							result = ErrorType.Ok;
						}
						else if (toServerUdpSocket != null)
						{
							int outputPercent2 = 0;
							GetUnreliableMessagingLossRatioPercent(HostID.Server, out outputPercent2);
							int num = (100 - outputPercent2) * (100 - peerByHostID.CSPacketLossPercent) / 100;
							outputPercent = peerByHostID.CSPacketLossPercent;
							result = ErrorType.Ok;
						}
						else
						{
							outputPercent = peerByHostID.CSPacketLossPercent;
							result = ErrorType.Ok;
						}
					}
					else
					{
						outputPercent = 100;
						result = ErrorType.InvalidHostID;
					}
				}
			}
			return result;
		}

		public bool InvalidateUdpSocket(HostID peerID, ref DirectP2PInfo outDirectP2PInfo)
		{
			lock (m_critSec)
			{
				if (peerID == HostID.Server)
				{
					return false;
				}
				RemotePeer peerByHostID = GetPeerByHostID(peerID);
				if (peerByHostID != null)
				{
					outDirectP2PInfo = peerByHostID.GetDirectP2PInfo();
					bool hasBeenHolepunched = outDirectP2PInfo.HasBeenHolepunched;
					if (peerByHostID.udpSocket != null && !peerByHostID.ToPeerUdpSocket.IsSocketClosed())
					{
						peerByHostID.ToPeerUdpSocket.CloseSocketOnly();
						peerByHostID.FallbackP2PToRelay(true, ErrorType.UserRequested);
					}
					return hasBeenHolepunched;
				}
				return false;
			}
		}

		public bool RestoreUdpSocket(HostID peerID)
		{
			lock (m_critSec)
			{
				RemotePeer peerByHostID = GetPeerByHostID(peerID);
				if (peerByHostID != null)
				{
					peerByHostID.m_restoreNeeded = true;
					return true;
				}
				return false;
			}
		}

		public void TEST_FallbackUdpToTcp(FallbackMethod mode)
		{
			if (!HasServerConnection)
			{
				return;
			}
			lock (m_critSec)
			{
				switch (mode)
				{
				case FallbackMethod.PeersUdpToTcp:
					FirstChanceFallbackEveryPeerUdpToTcp(ErrorType.UserRequested);
					break;
				case FallbackMethod.CloseUdpSocket:
					if (toServerUdpSocket != null)
					{
						toServerUdpSocket.CloseSocketOnly();
					}
					{
						foreach (RemotePeer value in remotePeers.Values)
						{
							if (value.udpSocket != null)
							{
								value.ToPeerUdpSocket.CloseSocketOnly();
							}
						}
						break;
					}
				case FallbackMethod.ServerUdpToTcp:
					FirstChanceFallbackServerUdpToTcp(ErrorType.UserRequested);
					break;
				}
			}
		}

		private void FirstChanceFallbackEveryPeerUdpToTcp(ErrorType reason)
		{
			foreach (RemotePeer value in remotePeers.Values)
			{
				value.FallbackP2PToRelay(true, reason);
			}
		}

		internal void TEST_EnableVirtualSpeedHack(long multipliedSpeed)
		{
			if (multipliedSpeed <= 0)
			{
				throw new Exception("Invalid parameter!");
			}
			virtualSpeedHackMultiplicationMs = multipliedSpeed;
		}

		public ReliableUdpHostStats GetPeerReliableUdpStats(HostID peerID)
		{
			lock (m_critSec)
			{
				RemotePeer value;
				if (remotePeers.TryGetValue(peerID, out value) && value.toPeerReliableUdp.host != null)
				{
					return value.toPeerReliableUdp.host.GetStats();
				}
			}
			return null;
		}

		public bool IsLocalHostBehindNat(ref bool output)
		{
			if (!HasServerConnection)
			{
				return false;
			}
			output = !ToServerUdpSocketLocalAddr.Equals(ToServerUdpSocketAddrAtServer);
			return true;
		}

		public SocketInfo GetSocketInfo(HostID remoteHostID)
		{
			SocketInfo result;
			lock (m_critSec)
			{
				SocketInfo socketInfo = new SocketInfo();
				socketInfo.tcpSocket = IntPtr.Zero;
				socketInfo.udpSocket = IntPtr.Zero;
				if (ToServerTcp != null && ToServerTcp.socket != null)
				{
					socketInfo.tcpSocket = ToServerTcp.socket.Handle;
					if (HostID.Server == remoteHostID)
					{
						if (toServerUdpSocket != null && toServerUdpSocket.socket != null)
						{
							socketInfo.udpSocket = toServerUdpSocket.socket.Handle;
							result = socketInfo;
							return result;
						}
					}
					else
					{
						RemotePeer peerByHostID = GetPeerByHostID(remoteHostID);
						if (peerByHostID == null)
						{
							result = null;
							return result;
						}
						if (peerByHostID.RelayedP2P && toServerUdpSocket != null && toServerUdpSocket.socket != null)
						{
							socketInfo.udpSocket = toServerUdpSocket.socket.Handle;
							result = socketInfo;
							return result;
						}
						if (!peerByHostID.RelayedP2P && peerByHostID.ToPeerUdpSocket != null && peerByHostID.ToPeerUdpSocket.socket != null)
						{
							socketInfo.udpSocket = peerByHostID.ToPeerUdpSocket.socket.Handle;
							result = socketInfo;
							return result;
						}
					}
					result = null;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		public bool DumpEmergencyLog(string inputFileName)
		{
			Queue<EmergencyLogData> queue = new Queue<EmergencyLogData>();
			lock (m_critSec)
			{
				while (emergencyLogQueue.Count > 0)
				{
					queue.Enqueue(emergencyLogQueue.Dequeue());
				}
			}
			TextWriter textWriter = new StreamWriter(inputFileName, false, Encoding.Unicode);
			while (queue.Count > 0)
			{
				EmergencyLogData emergencyLogData = queue.Dequeue();
				textWriter.WriteLine(string.Format("[{0}] [{1}] {2}", emergencyLogData.tid.ToString(), DateTime.FromBinary(emergencyLogData.addedTime).ToString(), emergencyLogData.text));
			}
			textWriter.Close();
			textWriter.Dispose();
			return true;
		}

		public void SetDefaultTimeoutTimeMs(int newVal)
		{
			lock (m_critSec)
			{
				if (newVal < 1000)
				{
					Sysutil.ShowUserMisuseError("Too short timeout value. It may cause unfair disconnection.");
				}
				else
				{
					settings.defaultTimeoutTimeMs = newVal;
				}
			}
		}

		public ErrorType ForceP2PRelay(HostID remotePeerID, bool enable)
		{
			lock (m_critSec)
			{
				if (remotePeerID == HostID.Server)
				{
					return ErrorType.InvalidHostID;
				}
				RemotePeer peerByHostID = GetPeerByHostID(remotePeerID);
				if (peerByHostID != null)
				{
					peerByHostID.m_forceRelayP2P = enable;
					return ErrorType.Ok;
				}
				return ErrorType.InvalidHostID;
			}
		}

		private HostID GetSrcHostIDByAddrAtDestSide_NOLOCK(IPEndPoint addr)
		{
			if (!NetUtil.IsUnicastEndpoint(addr))
			{
				return HostID.None;
			}
			if (toServerUdp_fallbackable != null && NetUtil.IsUnicastEndpoint(toServerUdp_fallbackable.serverAddr) && toServerUdp_fallbackable.serverAddr.Equals(addr))
			{
				return HostID.Server;
			}
			if (ToServerUdpSocketAddrAtServer.Equals(addr))
			{
				return localHostID;
			}
			foreach (RemotePeer value in remotePeers.Values)
			{
				if (value.p2pHolepunchedRemoteToLocalAddr.Equals(addr))
				{
					return value.peerHostID;
				}
			}
			return HostID.None;
		}

		private bool SendUserMessage(HostID remote, RmiContext rmiContext, byte[] payload)
		{
			return SendUserMessage(new HostID[1] { remote }, rmiContext, payload);
		}

		public override bool SendByRmiProxy(SendFragRefs sendData, SendOpt sendContext, HostID[] sendTo)
		{
			if (State != WorkerState.Disconnected)
			{
				return Send_CompressLayer(sendData, sendContext, sendTo);
			}
			return false;
		}

		protected override HostID[] ConvertGroupToIndividualsAndUnion(HostID[] sendTo)
		{
			lock (m_critSec)
			{
				List<ISendDest_C> list = new List<ISendDest_C>();
				ConvertGroupToIndividualsAndUnion(sendTo, list);
				HostID[] array = new HostID[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					ISendDest_C sendDest_C = list[i];
					if (sendDest_C != null)
					{
						array[i] = sendDest_C.SendDestHostID;
					}
					else
					{
						array[i] = HostID.None;
					}
				}
				return array;
			}
		}

		private void ConvertGroupToIndividualsAndUnion(List<HostID> sendTo, List<ISendDest_C> sendDestList)
		{
			int count = sendTo.Count;
			for (int i = 0; i < count; i++)
			{
				if (sendTo[i] != HostID.None)
				{
					ConvertAndAppendP2PGroupToPeerList(sendTo[i], sendDestList);
				}
			}
			sendDestList = Sysutil.UnionDuplicates(sendDestList, sendDestComparer);
		}

		private void ConvertGroupToIndividualsAndUnion(HostID[] sendTo, List<ISendDest_C> sendDestList)
		{
			int num = sendTo.Length;
			for (int i = 0; i < num; i++)
			{
				if (sendTo[i] != HostID.None)
				{
					ConvertAndAppendP2PGroupToPeerList(sendTo[i], sendDestList);
				}
			}
			sendDestList = Sysutil.UnionDuplicates(sendDestList, sendDestComparer);
		}

		private bool ConvertAndAppendP2PGroupToPeerList(HostID sendTo, List<ISendDest_C> sendTo2)
		{
			P2PGroup_C p2PGroupByHostID_Internal = GetP2PGroupByHostID_Internal(sendTo);
			if (p2PGroupByHostID_Internal == null)
			{
				sendTo2.Add(GetSendDestByHostID(sendTo));
			}
			else
			{
				foreach (HostID key in p2PGroupByHostID_Internal.members.Keys)
				{
					ISendDest_C sendDestByHostID = GetSendDestByHostID(key);
					if (sendDestByHostID != null)
					{
						sendTo2.Add(sendDestByHostID);
					}
				}
			}
			return true;
		}

		private ISendDest_C GetSendDestByHostID(HostID peerHostID)
		{
			lock (m_critSec)
			{
				switch (peerHostID)
				{
				case HostID.Server:
					return serverAsSendDest;
				case HostID.None:
					return null;
				default:
				{
					if (peerHostID == localHostID)
					{
						return this;
					}
					RemotePeer peerByHostID = GetPeerByHostID(peerHostID);
					if (peerByHostID != null && !peerByHostID.garbaged)
					{
						return peerByHostID;
					}
					return null;
				}
				}
			}
		}

		internal override bool Send_BroadcastLayer(SendFragRefs payload, SendOpt sendContext, HostID[] sendTo)
		{
			lock (m_critSec)
			{
				if (ToServerTcp == null || localHostID == HostID.None)
				{
					EnqueError(ErrorInfo.From(ErrorType.PermissionDenied, HostID.None, NoServerConnectionErrorText));
					return false;
				}
				List<HostID> obj = new List<HostID>(sendTo);
				obj = Sysutil.UnionDuplicates(obj, hostIDComparer);
				List<ISendDest_C> list = new List<ISendDest_C>();
				ConvertGroupToIndividualsAndUnion(obj, list);
				RelayDestList_C relayDestList_C = new RelayDestList_C();
				int num = 0;
				CompressedRelayDestList_C compressedRelayDestList_C = new CompressedRelayDestList_C();
				HostIDArray hostIDArray = new HostIDArray();
				RelayDest_C value = default(RelayDest_C);
				RelayDest_C value2 = default(RelayDest_C);
				for (int i = 0; i < list.Count; i++)
				{
					ISendDest_C sendDest_C = list[i];
					if (sendDest_C == this && sendContext.enableLoopback)
					{
						Message message = new Message();
						payload.ToAssembledMessage(message);
						loopbackFinalReceivedMessageQueue.Enqueue(message);
					}
					else if (sendDest_C == serverAsSendDest)
					{
						Send_ToServer_Directly_Copy(HostID.Server, sendContext.reliability, payload, sendContext);
					}
					else
					{
						if (sendDest_C == null || sendDest_C == this)
						{
							continue;
						}
						RemotePeer remotePeer = (RemotePeer)sendDest_C;
						bool flag = false;
						if (NetConfig.UseIsSameLanToLocalForMaxDirectP2PMulticast)
						{
							flag = remotePeer.IsSameLanToLocal;
						}
						if (!remotePeer.RelayedP2P && !remotePeer.m_forceRelayP2P && !remotePeer.IsRelayMuchFasterThanDirectP2P(serverUdpRecentPingMs, sendContext.forceRelayThresholdRatio) && (num < sendContext.maxDirectBroadcastCount || flag))
						{
							if (!flag)
							{
								num++;
							}
							if (!sendContext.INTERNAL_USE_isProudNetSpecificRmi)
							{
								remotePeer.toRemotePeerSendUdpMessageTrialCount++;
							}
							if (sendContext.reliability == MessageReliability.Reliable)
							{
								remotePeer.toPeerReliableUdp.SendWithSplitter_Copy(payload);
								continue;
							}
							remotePeer.ToPeerUdp.SendWithSplitter_Copy(payload, sendContext);
							if (GetIntersectionOfHostIDListAndP2PGroupsOfRemotePeer(obj, remotePeer, hostIDArray))
							{
								compressedRelayDestList_C.AddSubset(hostIDArray, remotePeer.peerHostID);
							}
							continue;
						}
						if (sendContext.reliability == MessageReliability.Reliable)
						{
							remotePeer.toPeerReliableUdp.host.sender_INTERNAL.StreamToSenderWindowOnNeed(true);
							value.frameNumber = remotePeer.toPeerReliableUdp.NextFrameNumberForAnotherReliablySendingFrame;
							value.remotePeer = remotePeer;
							relayDestList_C.Add(value);
						}
						else if (sendContext.allowRelaySend)
						{
							value2.frameNumber = (FrameNumber)0;
							value2.remotePeer = remotePeer;
							relayDestList_C.Add(value2);
							if (GetIntersectionOfHostIDListAndP2PGroupsOfRemotePeer(obj, remotePeer, hostIDArray))
							{
								compressedRelayDestList_C.AddSubset(hostIDArray, HostID.None);
							}
							else
							{
								compressedRelayDestList_C.AddIndividual(remotePeer.peerHostID);
							}
						}
						if (sendContext.enableP2PJitTrigger && !remotePeer.m_forceRelayP2P)
						{
							remotePeer.m_jitDirectP2PNeeded = true;
						}
					}
				}
				if (relayDestList_C.Count > 0)
				{
					if (sendContext.reliability == MessageReliability.Unreliable)
					{
						HostIDArray hostIDArray2 = new HostIDArray();
						Message message2 = new Message();
						if (!NetConfig.ForceCompressedRelayDestListOnly && relayDestList_C.Count <= compressedRelayDestList_C.AllHostIDCount + 1)
						{
							for (int j = 0; j < relayDestList_C.Count; j++)
							{
								hostIDArray2.Add(relayDestList_C[j].remotePeer.peerHostID);
							}
							message2.Write(MessageType.UnreliableRelay1);
							message2.Write(sendContext.priority);
							message2.WriteScalar(sendContext.uniqueID);
							message2.Write(hostIDArray2);
						}
						else
						{
							message2.Write(MessageType.UnreliableRelay1_RelayDestListCompressed);
							message2.Write(sendContext.priority);
							message2.WriteScalar(sendContext.uniqueID);
							message2.Write(compressedRelayDestList_C.includeeHostIDList);
							int count = compressedRelayDestList_C.p2pGroupList.Count;
							message2.WriteScalar(count);
							foreach (KeyValuePair<HostID, P2PGroupSubset_C> p2pGroup in compressedRelayDestList_C.p2pGroupList)
							{
								message2.Write(p2pGroup.Key);
								message2.Write(p2pGroup.Value.excludeeHostIDList);
							}
						}
						message2.WriteScalar(payload.TotalLength);
						SendFragRefs sendFragRefs = new SendFragRefs();
						sendFragRefs.Add(message2);
						sendFragRefs.Add(payload);
						SendOpt sendOpt = sendContext.Clone();
						sendOpt.INTERNAL_USE_isProudNetSpecificRmi = true;
						Send_ToServer_Directly_Copy(HostID.None, MessageReliability.Unreliable, sendFragRefs, sendOpt);
					}
					else
					{
						SendFragRefs sendFragRefs2 = new SendFragRefs();
						Message header = new Message();
						TcpLayer_C.AddSplitterButShareBuffer(payload, sendFragRefs2, header);
						RelayDestList ret;
						relayDestList_C.ToSerializable(out ret);
						SendFragRefs sendFragRefs3 = new SendFragRefs();
						Message message3 = new Message();
						message3.Write(MessageType.ReliableRelay1);
						message3.Write(ret);
						message3.WriteScalar(sendFragRefs2.TotalLength);
						sendFragRefs3.Add(message3);
						sendFragRefs3.Add(sendFragRefs2);
						SendOpt sendOpt2 = sendContext.Clone();
						sendOpt2.INTERNAL_USE_isProudNetSpecificRmi = true;
						Send_ToServer_Directly_Copy(HostID.None, MessageReliability.Reliable, sendFragRefs3, sendOpt2);
					}
				}
				return true;
			}
		}

		internal bool GetIntersectionOfHostIDListAndP2PGroupsOfRemotePeer(List<HostID> sortedHostIDList, RemotePeer rp, HostIDArray outSubsetGroupHostIDList)
		{
			bool result = false;
			outSubsetGroupHostIDList.Clear();
			foreach (HostID key in rp.joinedP2PGroups.Keys)
			{
				if (sortedHostIDList.BinarySearch(key) >= 0)
				{
					outSubsetGroupHostIDList.Add(key);
					result = true;
				}
			}
			return result;
		}

		internal override void EnqueError(ErrorInfo info)
		{
			EnqueLocalEvent(new LocalEvent
			{
				type = LocalEventType.Error,
				errorInfo = info
			});
		}

		public override void EnqueWarning(ErrorInfo info)
		{
			EnqueLocalEvent(new LocalEvent
			{
				type = LocalEventType.Warning,
				errorInfo = info
			});
		}

		private void EnquePacketDefragWarning(IPEndPoint addrPort, string text)
		{
			lock (m_critSec)
			{
				HostID remote = HostID.None;
				RemotePeer peerByUdpAddr = GetPeerByUdpAddr(addrPort);
				if (peerByUdpAddr == null)
				{
					if (toServerUdp_fallbackable.serverAddr.Equals(addrPort))
					{
						remote = HostID.Server;
					}
				}
				else
				{
					remote = peerByUdpAddr.peerHostID;
				}
				EnqueWarning(ErrorInfo.From(ErrorType.InvalidPacketFormat, remote, text));
			}
		}

		internal override bool AsyncCallbackMayOccur()
		{
			return false;
		}

		internal void Send_ToServer_Directly_Copy(HostID destHostID, MessageReliability reliability, SendFragRefs sendData2, SendOpt sendOpt)
		{
			if (reliability == MessageReliability.Reliable)
			{
				ToServerTcp.AddToSendQueueWithSplitterAndSignal_Copy(sendData2, new SendOpt());
				return;
			}
			RequestServerUdpSocketReady_FirstTimeOnly();
			toServerUdp_fallbackable.SendWithSplitterViaUdpOrTcp_Copy(destHostID, sendData2, sendOpt);
		}

		private void RequestServerUdpSocketReady_FirstTimeOnly()
		{
			if (toServerUdpSocket == null && !toServerUdp_fallbackable.serverUdpReadyWaiting && settings.fallbackMethod <= FallbackMethod.PeersUdpToTcp && !toServerUdpSocketFailed)
			{
				c2sProxy.C2S_RequestCreateUdpSocket(HostID.Server, RmiContext.ReliableSendForPN);
				toServerUdp_fallbackable.serverUdpReadyWaiting = true;
			}
		}

		private void EnqueLocalEvent(LocalEvent e)
		{
			lock (m_critSec)
			{
				finalUserWorkItemQueue.Enqueue(new FinalUserWorkItem(e));
			}
		}

		private void EnqueueConnectFailEvent(ErrorType errorType, SocketError socketErrorCode, string comment, ByteArray reply)
		{
			lock (m_critSec)
			{
				if (supressSubsequentDisconnectionEvents)
				{
					return;
				}
				ConnectLocalEvent connectLocalEvent = new ConnectLocalEvent();
				ConnectErrorInfo connectErrorInfo = new ConnectErrorInfo();
				connectLocalEvent.type = LocalEventType.ConnectServerFail;
				connectErrorInfo.comment = comment;
				connectErrorInfo.errorType = errorType;
				connectErrorInfo.socketError = socketErrorCode;
				connectErrorInfo.remote = HostID.Server;
				connectLocalEvent.userData = reply;
				try
				{
					connectErrorInfo.remoteAddr = new IPEndPoint(IPAddress.Parse(connectionParam.serverIP), connectionParam.serverPort);
				}
				catch (Exception)
				{
					if (!isIpV6Network)
					{
						connectErrorInfo.remoteAddr = new IPEndPoint(IPAddress.Any, connectionParam.serverPort);
					}
					else
					{
						connectErrorInfo.remoteAddr = new IPEndPoint(IPAddress.IPv6Any, connectionParam.serverPort);
					}
				}
				connectLocalEvent.errorInfo = connectErrorInfo;
				EnqueLocalEvent(connectLocalEvent);
				supressSubsequentDisconnectionEvents = true;
			}
		}

		private void EnqueueConnectFailEvent(ErrorType errorType, ErrorInfo errorInfo)
		{
			lock (m_critSec)
			{
				if (!supressSubsequentDisconnectionEvents)
				{
					EnqueLocalEvent(new ConnectLocalEvent
					{
						type = LocalEventType.ConnectServerFail,
						errorInfo = errorInfo
					});
					supressSubsequentDisconnectionEvents = true;
				}
			}
		}

		internal void EnqueFallbackP2PToRelayEvent(HostID remotePeerID, ErrorType reason)
		{
			EnqueLocalEvent(new LocalEvent
			{
				type = LocalEventType.RelayP2PEnabled,
				errorInfo = new ErrorInfo
				{
					errorType = reason,
					remote = remotePeerID
				}
			});
		}

		private void TcpAndUdp_DoForLongInterval()
		{
			lock (m_critSec)
			{
				long timeMs = PreciseCurrentTime.GetTimeMs();
				if (removeTooOldUdpSendPacketQueueOnNeed_Timer.IsTimeToDo(timeMs))
				{
					if (toServerUdpSocket != null)
					{
						toServerUdpSocket.DoForLongInterval(timeMs, settings.overSendSuspectingThresholdInBytes);
						foreach (RemotePeer value in remotePeers.Values)
						{
							if (!value.garbaged)
							{
								if (value.ToPeerUdpSocket != null)
								{
									value.ToPeerUdpSocket.DoForLongInterval(timeMs, settings.overSendSuspectingThresholdInBytes);
								}
								value.sendQueuedAmountInBytes = value.ToPeerUdp.UdpSendBufferPacketFilledCount;
							}
						}
					}
					if (ToServerTcp != null)
					{
						ToServerTcp.DoForLongInterval(timeMs);
					}
				}
				if (timeMs - tcpAndUdp_DoForShortInterval_lastTimeMs <= NetConfig.ReliableUdpHeartbeatIntervalMs)
				{
					return;
				}
				if (toServerUdpSocket != null)
				{
					toServerUdpSocket.udpPacketFragBoard.DoForShortInterval(timeMs);
				}
				foreach (RemotePeer value2 in remotePeers.Values)
				{
					if (!value2.garbaged && value2.ToPeerUdpSocket != null)
					{
						value2.ToPeerUdpSocket.udpPacketFragBoard.DoForShortInterval(timeMs);
					}
				}
				tcpAndUdp_DoForShortInterval_lastTimeMs = timeMs;
			}
		}

		private void EveryRemote_IssueSendOnNeed()
		{
			lock (m_critSec)
			{
				long timeMs = PreciseCurrentTime.GetTimeMs();
				if (IsProactorAsyncModel)
				{
					if (ToServerTcp != null)
					{
						ToServerTcp.IssueSendOnNeed(timeMs, tcpSendAsyncCallback);
					}
					if (toServerUdpSocket != null)
					{
						toServerUdpSocket.IssueSendOnNeed_IfPossible(timeMs, udpSendAsyncCallback);
					}
					{
						foreach (RemotePeer value in remotePeers.Values)
						{
							if (!value.garbaged && value.ToPeerUdpSocket != null)
							{
								value.ToPeerUdpSocket.IssueSendOnNeed_IfPossible(timeMs, udpSendAsyncCallback);
							}
						}
						return;
					}
				}
				if (ToServerTcp != null)
				{
					ToServerTcp.NonBlockSendUntilWouldBlock(timeMs, ref netClientStats);
				}
				if (toServerUdpSocket != null)
				{
					try
					{
						toServerUdpSocket.NonBlockSendUntilWouldBlock(timeMs, ref netClientStats);
					}
					catch (ObjectDisposedException ex)
					{
						toServerUdpSocket.CloseSocketOnly();
						EnqueError(ErrorInfo.From(ErrorType.DisconnectFromLocal, LocalHostID, ex.ToString()));
					}
				}
				foreach (RemotePeer value2 in remotePeers.Values)
				{
					if (!value2.garbaged && value2.ToPeerUdpSocket != null)
					{
						try
						{
							value2.ToPeerUdpSocket.NonBlockSendUntilWouldBlock(timeMs, ref netClientStats);
						}
						catch (ObjectDisposedException ex2)
						{
							value2.ToPeerUdpSocket.CloseSocketOnly();
							value2.FallbackP2PToRelay(true, ErrorType.DisconnectFromRemote);
							EnqueError(ErrorInfo.From(ErrorType.DisconnectFromLocal, LocalHostID, ex2.ToString()));
						}
					}
				}
			}
		}

		private void EveryRemote_NonBlockRecvUntilWouldBlock()
		{
			lock (m_critSec)
			{
				if (ToServerTcp != null)
				{
					ToServerTcp_NonBlockRecvUntilWouldBlock();
				}
				if (toServerUdpSocket != null)
				{
					Udp_NonBlockRecvUntilWouldBlock(toServerUdpSocket);
				}
				foreach (RemotePeer value in remotePeers.Values)
				{
					if (!value.garbaged && value.ToPeerUdpSocket != null)
					{
						Udp_NonBlockRecvUntilWouldBlock(value.ToPeerUdpSocket);
					}
				}
			}
		}

		private void ToServerTcp_NonBlockRecvUntilWouldBlock()
		{
			while (!ToServerTcp.IsSocketClosed)
			{
				int num;
				while (true)
				{
					try
					{
						num = ToServerTcp.socket.Receive(ToServerTcp.recvBuffer, ToServerTcp.recvBuffer.Length, SocketFlags.None);
					}
					catch (SocketException ex)
					{
						if (ex.SocketErrorCode == SocketError.Interrupted)
						{
							continue;
						}
						ServerConnectionState output = new ServerConnectionState();
						ConnectionState serverConnectionState = GetServerConnectionState(ref output);
						if (ex.SocketErrorCode != SocketError.IOPending && ex.SocketErrorCode != SocketError.WouldBlock && ex.SocketErrorCode != SocketError.TryAgain && ex.SocketErrorCode != SocketError.NoBufferSpaceAvailable && (ConnectionState.Connecting != serverConnectionState || SocketError.NotConnected != ex.SocketErrorCode))
						{
							EnqueueDisconnectionEventAndTransitToDisconnecting(ex.Message, ex.SocketErrorCode);
						}
						return;
					}
					catch (Exception ex2)
					{
						EnqueueDisconnectionEventAndTransitToDisconnecting(ex2.Message, SocketError.Disconnecting);
						return;
					}
					break;
				}
				if (num <= 0)
				{
					EnqueueDisconnectionEventAndTransitToDisconnecting("graceful disconnect", SocketError.Disconnecting);
					break;
				}
				ToServerTcp.recvStream.PushBack_Copy(ToServerTcp.recvBuffer, num);
				lastTcpStreamReceivedTimeMs = PreciseCurrentTime.GetTimeMs();
				netClientStats.totalTcpReceiveBytes += (ulong)num;
				ReceivedMessageList ret = new ReceivedMessageList();
				ExtractMessageFromTcpStream(ref ret);
				foreach (ReceivedMessage item in ret)
				{
					preFinalRecvQueue.Enqueue(item);
				}
				ProcessEveryMessageOrMoveToFinalRecvQueue(null);
				ToServerTcp.lastRecvInvokeWarningTime = 0L;
			}
		}

		private void EnqueueDisconnectionEventAndTransitToDisconnecting(string errorString, SocketError socketError)
		{
			if (shutdownIssuedTimeMs == 0)
			{
				EnqueueDisconnectionEvent(ErrorType.DisconnectFromRemote, ErrorType.TCPConnectFailure, string.Format("Receive byteRec <= 0 msg:{0} socketerrorcode:{1}", errorString, socketError));
			}
			else
			{
				EnqueueDisconnectionEvent(ErrorType.DisconnectFromLocal, ErrorType.TCPConnectFailure, string.Format("Receive byteRec <= 0 msg:{0} socketerrorcode:{1}", errorString + "Client Disconnect Call", socketError));
			}
			State = WorkerState.Disconnecting;
		}

		private void Udp_NonBlockRecvUntilWouldBlock(UdpSocket_C udpSocket)
		{
			while (udpSocket.recycleTime == 0 && !udpSocket.IsSocketClosed())
			{
				IPEndPoint iPEndPoint = null;
				int num = 0;
				while (true)
				{
					try
					{
						IPAddress iPAddress = IPAddress.Parse(connectionParam.serverIP);
						EndPoint remoteEP = ((iPAddress.AddressFamily != AddressFamily.InterNetwork) ? new IPEndPoint(IPAddress.IPv6Any, 0) : new IPEndPoint(IPAddress.Any, 0));
						num = udpSocket.socket.ReceiveFrom(udpSocket.recvBuffer, udpSocket.recvBuffer.Length, SocketFlags.None, ref remoteEP);
						if (remoteEP != null)
						{
							iPEndPoint = (IPEndPoint)remoteEP;
						}
					}
					catch (SocketException ex)
					{
						if (ex.SocketErrorCode == SocketError.Interrupted)
						{
							continue;
						}
						return;
					}
					catch (Exception)
					{
						return;
					}
					break;
				}
				if (num <= 0 || iPEndPoint == null)
				{
					break;
				}
				udpSocket.lastUdpRecvIssuedTime = PreciseCurrentTime.GetTimeMs();
				netClientStats.totalUdpReceiveCount++;
				netClientStats.totalUdpReceiveBytes += (ulong)num;
				AssembledPacket output = new AssembledPacket();
				string outError = "";
				switch (udpSocket.udpPacketDefragBoard.PushFragmentAndPopAssembledPacket(udpSocket.recvBuffer, num, iPEndPoint, GetSrcHostIDByAddrAtDestSide_NOLOCK(iPEndPoint), localHostID, PreciseCurrentTime.GetTimeMs(), MessageMaxLength, ref output, ref outError))
				{
				case AssembledPacketError.Ok:
				{
					ReceivedMessageList ret = new ReceivedMessageList();
					ErrorType outError2;
					ExtractMessageFromUdpRecvQueue(output.packet.assembledData.data, output.packet.assembledData.Count, output.senderAddr, ref ret, out outError2);
					foreach (ReceivedMessage item in ret)
					{
						preFinalRecvQueue.Enqueue(item);
					}
					if (!udpSocket.garbaged)
					{
						ProcessEveryMessageOrMoveToFinalRecvQueue(udpSocket);
					}
					break;
				}
				case AssembledPacketError.Error:
					EnquePacketDefragWarning(iPEndPoint, outError);
					break;
				default:
				{
					int num2 = 1;
					break;
				}
				}
			}
		}

		internal void GarbageSocket(UdpSocket_C socket)
		{
			socket.OnCloseSocketAndMakeOrphant();
			garbages.Add(socket);
		}

		private void RecycleUdpSocket(UdpSocket_C socket)
		{
			recycles.Add((ushort)socket.localAddr.Port, socket);
			socket.recycleTime = PreciseCurrentTime.GetTimeMs();
			socket.ResetPacketFragState();
		}

		private void GarbagePeer(RemotePeer peer)
		{
			if (peer.garbaged || peer.owner != this)
			{
				return;
			}
			if (peer.udpSocket != null)
			{
				if (peer.RelayedP2P)
				{
					GarbageSocket(peer.udpSocket);
				}
				else
				{
					RecycleUdpSocket(peer.udpSocket);
				}
				peer.udpSocket = null;
			}
			peer.owner = null;
			peer.garbaged = true;
			peer.p2pConnectionTrialContext = null;
			peer.RelayedP2P = true;
			peerGarbages.Add(peer.peerHostID, peer);
		}

		private void DoGarbageCollect()
		{
			lock (m_critSec)
			{
				long timeMs = PreciseCurrentTime.GetTimeMs();
				List<ushort> list = new List<ushort>(recycles.Keys);
				foreach (ushort item2 in list)
				{
					UdpSocket_C udpSocket_C = recycles[item2];
					if (timeMs - udpSocket_C.recycleTime > NetConfig.RecyclePairReuseTimeMs + 10000)
					{
						GarbageSocket(udpSocket_C);
						recycles.Remove(item2);
					}
				}
				List<HostID> list2 = new List<HostID>(peerGarbages.Keys);
				foreach (HostID item3 in list2)
				{
					RemotePeer remotePeer = peerGarbages[item3];
					if (remotePeer.leaveEventCount == 0)
					{
						peerGarbages.Remove(item3);
					}
				}
				for (int num = garbages.Count - 1; num >= 0; num--)
				{
					UdpSocket_C udpSocket_C2 = garbages[num];
					bool flag = false;
					if (udpSocket_C2.IsSocketClosed() && !udpSocket_C2.recvIssued && !udpSocket_C2.sendIssued)
					{
						flag = true;
					}
					if (flag)
					{
						ushort item = (ushort)udpSocket_C2.localAddr.Port;
						if (usedUdpPorts.Contains(item))
						{
							unusedUdpPorts.Add(item);
							usedUdpPorts.Remove(item);
						}
						garbages.RemoveAt(num);
					}
				}
			}
		}

		private void EnqueueDisconnectionEvent(ErrorType errorType, ErrorType detailType, string comment)
		{
			lock (m_critSec)
			{
				if (!supressSubsequentDisconnectionEvents)
				{
					if (localHostID == HostID.None)
					{
						EnqueueConnectFailEvent(errorType, SocketError.Success, comment, null);
						return;
					}
					EnqueLocalEvent(new LocalEvent
					{
						type = LocalEventType.ClientServerDisconnect,
						errorInfo = new ErrorInfo
						{
							errorType = errorType,
							detailType = detailType,
							comment = comment,
							remote = HostID.Server
						}
					});
					supressSubsequentDisconnectionEvents = true;
				}
			}
		}

		private void SendServerHolePunchOnNeed()
		{
			lock (m_critSec)
			{
				if (settings.fallbackMethod != FallbackMethod.ServerUdpToTcp && toServerUdpSocket != null && !toServerUdpSocket.IsSocketClosed() && toServerUdp_fallbackable != null && !toServerUdp_fallbackable.RealUdpEnabled && localHostID != HostID.None)
				{
					uint num = BitConverter.ToUInt32(toServerUdp_fallbackable.serverAddr.Address.GetAddressBytes(), 0);
					if (num != 0 && num != uint.MaxValue && toServerUdp_fallbackable.holepunchCooltime != NetConfig.InfiniteCoolTimeMs && toServerUdp_fallbackable.holepunchCooltime < PreciseCurrentTime.GetTimeMs())
					{
						toServerUdp_fallbackable.holepunchCooltime = PreciseCurrentTime.GetTimeMs() + NetConfig.ServerHolepunchIntervalMs;
						SendServerHolepunch();
					}
				}
			}
		}

		private void SendServerHolepunch()
		{
			Message message = new Message();
			message.Write(MessageType.ServerHolepunch);
			message.Write(toServerUdp_fallbackable.holepunchMagicNumber);
			if (enableLog || settings.emergencyLogLineCount > 0)
			{
				Log(TraceID.Holepunch, string.Format("Sending ServerHolepunch: {0}", toServerUdp_fallbackable.serverAddr.ToString()));
			}
			toServerUdpSocket.AddToSendQueueWithSplitterAndSignal_Copy(HostID.Server, FilterTag.CreateFilterTag(LocalHostID, HostID.Server), toServerUdp_fallbackable.serverAddr, message, PreciseCurrentTime.GetTimeMs(), new SendOpt(MessagePriority_Holepunch, true));
		}

		private void RequestServerTimeOnNeed()
		{
			if (localHostID != HostID.None)
			{
				if (PreciseCurrentTime.GetTimeMs() - lastRequestServerTimeTimeMs > NetConfig.UnreliablePingIntervalMs / virtualSpeedHackMultiplicationMs)
				{
					requestServerTimeCount++;
					lastRequestServerTimeTimeMs = PreciseCurrentTime.GetTimeMs();
					Message message = new Message();
					message.Write(MessageType.UnreliablePing);
					message.Write(PreciseCurrentTime.GetTimeMs());
					message.Write(serverUdpRecentPingMs);
					toServerUdp_fallbackable.SendWithSplitterViaUdpOrTcp_Copy(HostID.Server, new SendFragRefs(message), new SendOpt(MessagePriority.Ring0, true));
				}
				if (reliablePing_Timer.IsTimeToDo(PreciseCurrentTime.GetTimeMs()))
				{
					c2sProxy.ReliablePing(HostID.Server, RmiContext.ReliableSendForPN, (float)ApplicationHint.recentFrameRate, (int)PreciseCurrentTime.GetTimeMs());
				}
			}
			else
			{
				LogLastServerUdpPacketReceived();
			}
		}

		private void LogLastServerUdpPacketReceived()
		{
			if (toServerUdp_fallbackable != null)
			{
				long num = PreciseCurrentTime.GetTimeMs() - toServerUdp_fallbackable.lastServerUdpPacketReceivedTime;
				if (num > 0)
				{
					toServerUdp_fallbackable.lastUdpPacketReceivedInterval = num;
				}
				toServerUdp_fallbackable.lastServerUdpPacketReceivedTime = PreciseCurrentTime.GetTimeMs();
				toServerUdp_fallbackable.lastServerUdpPacketReceivedCount++;
			}
		}

		private void SpeedHackPingOnNeed()
		{
			if (localHostID != HostID.None && speedHackDetectorPingCooltimeMs != NetConfig.InfiniteCoolTimeMs && speedHackDetectorPingCooltimeMs <= PreciseCurrentTime.GetTimeMs())
			{
				speedHackDetectorPingCooltimeMs = PreciseCurrentTime.GetTimeMs() + NetConfig.SpeedHackDetectorPingIntervalMs / virtualSpeedHackMultiplicationMs;
				Message message = new Message();
				message.Write(MessageType.SpeedHackDetectorPing);
				toServerUdp_fallbackable.SendWithSplitterViaUdpOrTcp_Copy(HostID.Server, new SendFragRefs(message), new SendOpt(MessagePriority.Ring0, true));
			}
		}

		private void P2PPingOnNeed()
		{
			foreach (RemotePeer value in remotePeers.Values)
			{
				if (value == null || value.garbaged || value.peerHostID == HostID.Server)
				{
					continue;
				}
				if (value.m_ReliablePingDiffCoolTime <= PreciseCurrentTime.GetTimeMs())
				{
					value.m_ReliablePingDiffCoolTime = PreciseCurrentTime.GetTimeMs() + NetConfig.ReliablePingIntervalMs;
					Message message = new Message();
					message.Write(MessageType.P2PReliablePing);
					message.Write(PreciseCurrentTime.GetTimeMs());
					SendFragRefs payload = new SendFragRefs(message);
					Send_BroadcastLayer(payload, new SendOpt(RmiContext.ReliableSendForPN)
					{
						priority = MessagePriority.Ring0
					}, new HostID[1] { value.peerHostID });
				}
				if (value.m_UnreliablePingDiffCoolTime > PreciseCurrentTime.GetTimeMs())
				{
					continue;
				}
				value.m_UnreliablePingDiffCoolTime = PreciseCurrentTime.GetTimeMs() + NetConfig.UnreliablePingIntervalMs;
				value.lastPingSendTimeMs = PreciseCurrentTime.GetTimeMs();
				if (!value.RelayedP2P)
				{
					Message message2 = new Message();
					message2.Write(MessageType.P2PUnreliablePing);
					message2.Write(PreciseCurrentTime.GetTimeMs());
					SendFragRefs sendData = new SendFragRefs(message2);
					value.ToPeerUdp.SendWithSplitter_Copy(sendData, new SendOpt(MessagePriority.Ring0, true));
					continue;
				}
				long num = PreciseCurrentTime.GetTimeMs() - value.lastDirectUdpPacketReceivedTimeMs;
				if (num > 0)
				{
					value.lastUdpPacketReceivedIntervalMs = num;
				}
				value.lastDirectUdpPacketReceivedTimeMs = PreciseCurrentTime.GetTimeMs();
				value.directUdpPacketReceiveCount++;
				value.indirectServerTimeDiffMs = 0L;
				value.lastPingMs = serverUdpRecentPingMs + value.peerToServerPingMs;
				if (value.setToRelayedButLastPingIsNotCalulcatedYet)
				{
					value.recentPingMs = 0;
					value.setToRelayedButLastPingIsNotCalulcatedYet = false;
				}
				if (value.recentPingMs > 0)
				{
					value.recentPingMs = (int)Sysutil.LerpInt(value.recentPingMs, value.lastPingMs, NetConfig.LagLinearProgrammingFactorPercent, 100L);
				}
				else
				{
					value.recentPingMs = value.lastPingMs;
				}
			}
		}

		private void FallbackServerUdpToTcpOnNeed()
		{
			long timeMs = PreciseCurrentTime.GetTimeMs();
			if (toServerUdp_fallbackable.RealUdpEnabled && timeMs - toServerUdp_fallbackable.lastServerUdpPacketReceivedTime > NetConfig.FallbackServerUdpToTcpTimeoutMs)
			{
				FirstChanceFallbackServerUdpToTcp(ErrorType.ServerUdpFailed);
			}
		}

		private void FirstChanceFallbackServerUdpToTcp(ErrorType reasonToShow)
		{
			if (toServerUdp_fallbackable.RealUdpEnabled)
			{
				toServerUdp_fallbackable.RealUdpEnabled = false;
				EnqueLocalEvent(new LocalEvent
				{
					type = LocalEventType.ServerUdpChanged,
					errorInfo = new ErrorInfo
					{
						errorType = ErrorType.ServerUdpFailed,
						remote = HostID.Server
					}
				});
				if (toServerUdp_fallbackable.tcpFallbackCount < NetConfig.ServerUdpRepunchMaxTrialCount)
				{
					toServerUdp_fallbackable.holepunchCooltime = NetConfig.ServerUdpRepunchIntervalMs;
					toServerUdp_fallbackable.tcpFallbackCount++;
				}
				else
				{
					toServerUdp_fallbackable.holepunchCooltime = NetConfig.InfiniteCoolTimeMs;
				}
				c2sProxy.NotifyUdpToTcpFallbackByClient(HostID.Server, RmiContext.ReliableSendForPN);
			}
		}

		private void ReportP2PPeerPingOnNeed()
		{
			if (!settings.enablePingTest || PreciseCurrentTime.GetTimeMs() - enablePingTestEndTimeMs <= NetConfig.ReportP2PPeerPingTestIntervalMs)
			{
				return;
			}
			foreach (KeyValuePair<HostID, RemotePeer> remotePeer in remotePeers)
			{
				enablePingTestEndTimeMs = PreciseCurrentTime.GetTimeMs();
				HostID key = remotePeer.Key;
				if (localHostID >= key)
				{
					continue;
				}
				RemotePeer value = remotePeer.Value;
				if (!value.garbaged)
				{
					if (!value.RelayedP2P && value.recentPingMs > 0 && value.peerToServerPingMs > 0 && serverUdpRecentPingMs + value.peerToServerPingMs < value.recentPingMs)
					{
						c2sProxy.ReportP2PPeerPing(HostID.Server, RmiContext.ReliableSendForPN, value.peerHostID, serverUdpRecentPingMs + value.peerToServerPingMs);
					}
					else if (!value.RelayedP2P)
					{
						c2sProxy.ReportP2PPeerPing(HostID.Server, RmiContext.ReliableSendForPN, value.peerHostID, value.recentPingMs);
					}
				}
			}
		}

		private void ReportRealUdpCount()
		{
			if (!NetConfig.UseReportRealUdpCount)
			{
				return;
			}
			lock (m_critSec)
			{
				if (localHostID == HostID.None || lastReportUdpCountTimeMs <= 0 || PreciseCurrentTime.GetTimeMs() - lastReportUdpCountTimeMs <= NetConfig.ReportRealUdpCountIntervalMs)
				{
					return;
				}
				lastReportUdpCountTimeMs = PreciseCurrentTime.GetTimeMs();
				c2sProxy.ReportC2SUdpMessageTrialCount(HostID.Server, RmiContext.ReliableSendForPN, toServerUdpSendCount);
				foreach (RemotePeer value in remotePeers.Values)
				{
					if (!value.garbaged)
					{
						c2cProxy.ReportUdpMessageCount(value.peerHostID, RmiContext.ReliableSendForPN, value.receiveudpMessageSuccessCount);
					}
				}
			}
		}

		private void CheckSendQueue()
		{
			long timeMs = PreciseCurrentTime.GetTimeMs();
			if (ToServerTcp == null || timeMs - lastCheckSendQueueTimeMs <= NetConfig.SendQueueHeavyWarningCheckCoolTimeMs)
			{
				return;
			}
			int num = ToServerTcp.sendQueue.Length;
			if (toServerUdpSocket != null)
			{
				num += toServerUdpSocket.udpPacketFragBoard.FromTotalPacketInBytesByAddr(toServerUdp_fallbackable.serverAddr);
			}
			if (sendQueueHeavyStartTimeMs != 0)
			{
				if (num > NetConfig.SendQueueHeavyWarningCapacity)
				{
					if (timeMs - sendQueueHeavyStartTimeMs > NetConfig.SendQueueHeavyWarningTimeMs)
					{
						sendQueueHeavyStartTimeMs = timeMs;
						EnqueWarning(ErrorInfo.From(ErrorType.SendQueueIsHeavy, HostID.Server, string.Format("{0} bytes in send queue", num)));
					}
				}
				else
				{
					sendQueueHeavyStartTimeMs = 0L;
				}
			}
			else if (num > NetConfig.SendQueueHeavyWarningCapacity)
			{
				sendQueueHeavyStartTimeMs = timeMs;
			}
			lastCheckSendQueueTimeMs = timeMs;
		}

		private void UpdateP2PGroup_MemberJoin(HostID groupHostID, HostID memberHostID, ByteArray customField, int eventID, FrameNumber p2pFirstFrameNumber, Guid connectionMagicNumber, ByteArray p2pAESSessionKey, ByteArray p2pRC4SessionKey, bool enableDirectP2P, ushort bindPort)
		{
			if (State != WorkerState.Connected)
			{
				return;
			}
			P2PGroup_C p2PGroup_C = GetP2PGroupByHostID_Internal(groupHostID);
			if (p2PGroup_C == null)
			{
				p2PGroup_C = CreateP2PGroupObject_INTERNAL(groupHostID);
			}
			bool localPortReuseSuccess = false;
			if (memberHostID != HostID.Server)
			{
				RemotePeer remotePeer = GetPeerByHostID(memberHostID);
				if (localHostID != memberHostID)
				{
					if (remotePeer == null)
					{
						remotePeer = new RemotePeer(this);
						remotePeer.peerHostID = memberHostID;
						remotePeer.magicNumber = connectionMagicNumber;
						remotePeer.m_allowDirectP2P = enableDirectP2P;
						remotePeers.Add(remotePeer.peerHostID, remotePeer);
						remotePeer.RelayedP2P = true;
						if (p2pAESSessionKey.Count != 0)
						{
							if (!CryptoAes.ExpandFrom(remotePeer.p2pSessionKey.aesKey, p2pAESSessionKey.data, 0, settings.encryptedMessageKeyLength / 8, CryptoAes.DEFAULT_BLOCK_SIZE))
							{
								throw new Exception("Failed to create SessionKey");
							}
						}
						else
						{
							remotePeer.p2pSessionKey.aesKey.Clear();
						}
						if (p2pRC4SessionKey.Count != 0)
						{
							if (!CryptoRc4.ExpandFrom(remotePeer.p2pSessionKey.rc4Key, p2pRC4SessionKey.data, settings.fastEncryptedMessageKeyLength / 8))
							{
								throw new Exception("Failed to create SessionKey");
							}
						}
						else
						{
							remotePeer.p2pSessionKey.rc4Key.keyExists = true;
						}
						remotePeer.toPeerReliableUdp.ResetEngine(p2pFirstFrameNumber);
						if (bindPort != 0 && enableDirectP2P)
						{
							localPortReuseSuccess = remotePeer.NewUdpSocketBindPort(bindPort);
						}
					}
					else if (remotePeer.garbaged)
					{
						remotePeer.InitGarbage(this);
						remotePeer.peerHostID = memberHostID;
						remotePeer.magicNumber = connectionMagicNumber;
						remotePeer.m_allowDirectP2P = enableDirectP2P;
						remotePeer.RelayedP2P = true;
						if (p2pAESSessionKey.Count != 0)
						{
							if (!CryptoAes.ExpandFrom(remotePeer.p2pSessionKey.aesKey, p2pAESSessionKey.data, 0, settings.encryptedMessageKeyLength / 8, CryptoAes.DEFAULT_BLOCK_SIZE))
							{
								throw new Exception("Failed to create SessionKey");
							}
						}
						else
						{
							remotePeer.p2pSessionKey.Clear();
						}
						if (p2pRC4SessionKey.Count != 0)
						{
							if (!CryptoRc4.ExpandFrom(remotePeer.p2pSessionKey.rc4Key, p2pRC4SessionKey.data, settings.fastEncryptedMessageKeyLength / 8))
							{
								throw new Exception("Failed to create SessionKey");
							}
						}
						else
						{
							remotePeer.p2pSessionKey.rc4Key.keyExists = true;
						}
						remotePeer.toPeerReliableUdp.ResetEngine(p2pFirstFrameNumber);
						if (bindPort != 0 && enableDirectP2P)
						{
							localPortReuseSuccess = remotePeer.NewUdpSocketBindPort(bindPort);
						}
						remotePeer.garbaged = false;
						peerGarbages.Remove(memberHostID);
					}
					remotePeer.joinedP2PGroups.Add(p2PGroup_C.groupHostID, p2PGroup_C);
					p2PGroup_C.members.Add(memberHostID, remotePeer);
				}
				else
				{
					p2PGroup_C.members.Add(memberHostID, this);
				}
			}
			else
			{
				p2PGroup_C.members.Add(memberHostID, serverAsSendDest);
			}
			c2sProxy.P2PGroup_MemberJoin_Ack(HostID.Server, RmiContext.ReliableSendForPN, groupHostID, memberHostID, eventID, localPortReuseSuccess);
			MemberLocalEvent memberLocalEvent = new MemberLocalEvent();
			memberLocalEvent.type = LocalEventType.AddMember;
			memberLocalEvent.groupHostID = groupHostID;
			memberLocalEvent.memberHostID = memberHostID;
			memberLocalEvent.memberCount = p2PGroup_C.members.Count;
			if (customField.Count > 0)
			{
				memberLocalEvent.customField.AddRange(customField.data);
			}
			EnqueLocalEvent(memberLocalEvent);
		}

		private P2PGroup_C CreateP2PGroupObject_INTERNAL(HostID groupHostID)
		{
			lock (m_critSec)
			{
				P2PGroup_C p2PGroup_C = new P2PGroup_C();
				p2PGroup_C.groupHostID = groupHostID;
				p2pGroups.Add(groupHostID, p2PGroup_C);
				return p2PGroup_C;
			}
		}

		internal void Log(TraceID TID, string s)
		{
			lock (m_critSec)
			{
				if (enableLog && localHostID != HostID.None)
				{
					c2sProxy.NotifyLog(HostID.Server, RmiContext.ReliableSendForPN, TID, s);
				}
				if (settings.emergencyLogLineCount > 0)
				{
					EmergencyLogData item = default(EmergencyLogData);
					item.tid = TID;
					item.text = s;
					item.addedTime = DateTime.Now.Ticks;
					emergencyLogQueue.Enqueue(item);
					if (emergencyLogQueue.Count > settings.emergencyLogLineCount)
					{
						emergencyLogQueue.Dequeue();
					}
				}
			}
		}

		private void RemoveRemotePeerIfNoGroupRelationDetected(RemotePeer memberRC)
		{
			foreach (P2PGroup_C value in p2pGroups.Values)
			{
				if (value.members.ContainsValue(memberRC))
				{
					return;
				}
			}
			c2sProxy.P2P_NotifyDirectP2PDisconnected(HostID.Server, RmiContext.ReliableSendForPN, memberRC.peerHostID, ErrorType.NoP2PGroupRelation);
			if (enableLog || settings.emergencyLogLineCount > 0)
			{
				Log(TraceID.Holepunch, string.Format("[Client {0}]: Disconnected direct p2p to Client {1}.\n", localHostID, memberRC.peerHostID));
			}
			GarbagePeer(memberRC);
		}

		private bool New_ToServerUdpSocket()
		{
			if (toServerUdpSocketFailed)
			{
				return false;
			}
			if (toServerUdpSocket == null)
			{
				try
				{
					toServerUdpSocket = new UdpSocket_C(this, PreciseCurrentTime.GetTimeMs(), RequestReceiveSpeedAtReceiverSide_NoRelay);
					IPEndPoint localAddr = ToServerTcp.localAddr;
					uint num = BitConverter.ToUInt32(localAddr.Address.GetAddressBytes(), 0);
					if (num == 0 || num == uint.MaxValue)
					{
						EnqueWarning(ErrorInfo.From(ErrorType.LocalSocketCreationFailed, localHostID, num.ToString()));
					}
					if (!CreateUdpSocket(toServerUdpSocket, new IPEndPoint(localAddr.Address, 0)))
					{
						toServerUdpSocket = null;
						toServerUdpSocketFailed = true;
						EnqueWarning(ErrorInfo.From(ErrorType.LocalSocketCreationFailed, LocalHostID, "UDP socket for server connection"));
						return false;
					}
				}
				catch (Exception e)
				{
					if (exceptionHandler != null)
					{
						exceptionHandler(localHostID, e);
					}
					toServerUdpSocket = null;
					toServerUdpSocketFailed = true;
					return false;
				}
				if (IsProactorAsyncModel)
				{
					toServerUdpSocket.IssueRecvFrom_UnlessSocketClosed(PreciseCurrentTime.GetTimeMs(), udpRecvAsyncCallback);
					return true;
				}
				return true;
			}
			return true;
		}

		private bool CreateUdpSocket(UdpSocket_C udpSocket, IPEndPoint udpLocalAddr)
		{
			bool flag = false;
			foreach (ushort unusedUdpPort in unusedUdpPorts)
			{
				udpLocalAddr.Port = unusedUdpPort;
				if (udpSocket.CreateSocket(IsProactorAsyncModel, udpLocalAddr))
				{
					usedUdpPorts.Add((ushort)udpLocalAddr.Port);
					unusedUdpPorts.Remove((ushort)udpLocalAddr.Port);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				udpLocalAddr.Port = 0;
				flag = udpSocket.CreateSocket(IsProactorAsyncModel, udpLocalAddr);
				if (usedUdpPorts.Count != 0 || unusedUdpPorts.Count != 0)
				{
					EnqueWarning(ErrorInfo.From(ErrorType.NoneAvailableInPortPool, LocalHostID, string.Format("Arbitary port number used: {0}", udpSocket.localAddr.Port)));
				}
			}
			return flag;
		}

		private void RequestReceiveSpeedAtReceiverSide_NoRelay(IPEndPoint dest)
		{
			lock (m_critSec)
			{
				if (toServerUdpSocket != null && !toServerUdpSocket.IsSocketClosed())
				{
					Message message = new Message();
					message.Write(MessageType.RequestReceiveSpeedAtReceiverSide_NoRelay);
					toServerUdpSocket.AddToSendQueueWithSplitterAndSignal_Copy(HostID.Server, FilterTag.CreateFilterTag(localHostID, HostID.Server), dest, message, PreciseCurrentTime.GetTimeMs(), new SendOpt(MessagePriority.Ring1, true));
				}
			}
		}

		internal override bool NextEncryptCount(HostID remote, ref ushort output)
		{
			lock (m_critSec)
			{
				if (ToServerTcp == null || localHostID == HostID.None)
				{
					return false;
				}
				RemotePeer peerByHostID = GetPeerByHostID(remote);
				if (peerByHostID != null)
				{
					output = peerByHostID.encryptCount;
					RemotePeer remotePeer = peerByHostID;
					remotePeer.encryptCount++;
					return true;
				}
				if (localHostID == remote)
				{
					output = selfEncryptCount;
					selfEncryptCount++;
					return true;
				}
				if (remote == HostID.Server)
				{
					output = toServerEncryptCount;
					toServerEncryptCount++;
					return true;
				}
				return false;
			}
		}

		internal override void PrevEncryptCount(HostID remote)
		{
			lock (m_critSec)
			{
				if (ToServerTcp != null && localHostID != HostID.None)
				{
					RemotePeer peerByHostID = GetPeerByHostID(remote);
					if (peerByHostID != null)
					{
						RemotePeer remotePeer = peerByHostID;
						remotePeer.encryptCount--;
					}
					else if (localHostID == remote)
					{
						selfEncryptCount--;
					}
					else if (remote == HostID.Server)
					{
						toServerEncryptCount--;
					}
				}
			}
		}

		internal override bool GetExpectedDecryptCount(HostID remote, ref ushort output)
		{
			lock (m_critSec)
			{
				RemotePeer peerByHostID = GetPeerByHostID(remote);
				if (peerByHostID != null)
				{
					output = peerByHostID.decryptCount;
					return true;
				}
				if (localHostID == remote)
				{
					output = selfDecryptCount;
					return true;
				}
				if (remote == HostID.Server)
				{
					output = toServerDecryptCount;
					return true;
				}
				return false;
			}
		}

		internal override bool NextDecryptCount(HostID remote)
		{
			lock (m_critSec)
			{
				RemotePeer peerByHostID = GetPeerByHostID(remote);
				if (peerByHostID != null)
				{
					RemotePeer remotePeer = peerByHostID;
					remotePeer.decryptCount++;
					return true;
				}
				if (localHostID == remote)
				{
					selfDecryptCount++;
					return true;
				}
				if (remote == HostID.Server)
				{
					toServerDecryptCount++;
					return true;
				}
				return false;
			}
		}

		internal override SessionKey GetCryptSessionKey(HostID remote, ref string errorOut)
		{
			lock (m_critSec)
			{
				RemotePeer peerByHostID = GetPeerByHostID(remote);
				SessionKey sessionKey = null;
				if (ToServerTcp == null || localHostID == HostID.None)
				{
					errorOut = "No connection to server!";
					return null;
				}
				if (peerByHostID != null)
				{
					sessionKey = peerByHostID.p2pSessionKey;
				}
				else if (localHostID == remote)
				{
					sessionKey = selfP2PSessionKey;
				}
				else if (remote == HostID.Server)
				{
					sessionKey = toServerSessionKey;
				}
				if (sessionKey != null && !sessionKey.KeyExists)
				{
					errorOut = string.Format("key not exists!");
					return null;
				}
				if (sessionKey == null)
				{
					errorOut = string.Format("{0} remote rp is {1} in netclient!", (int)remote, (peerByHostID == null) ? "NULL" : "not NULL");
				}
				return sessionKey;
			}
		}

		public void Dispose()
		{
			_Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void _Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					Disconnect();
				}
				_disposed = true;
			}
		}

		private void ToServerTcp_ReceiveCallback(IAsyncResult ar)
		{
			TcpLayer_C tcpLayer_C = (TcpLayer_C)ar.AsyncState;
			if (tcpLayer_C == null)
			{
				return;
			}
			lock (m_critSec)
			{
				tcpLayer_C.recvIssued = false;
				if (tcpLayer_C.IsSocketClosed)
				{
					return;
				}
				int num = 0;
				string arg = "";
				SocketError socketError = SocketError.Success;
				try
				{
					num = tcpLayer_C.socket.EndReceive(ar);
				}
				catch (SocketException ex)
				{
					arg = ex.Message;
					socketError = ex.SocketErrorCode;
				}
				catch (Exception ex2)
				{
					arg = ex2.Message;
				}
				if (num <= 0)
				{
					EnqueueDisconnectionEvent(ErrorType.DisconnectFromRemote, ErrorType.TCPConnectFailure, string.Format("EndReceive byteRec <= 0 msg:{0} socketerrorcode:{1}", arg, socketError));
					State = WorkerState.Disconnecting;
				}
				else if (tcpLayer_C.IsSocketClosed)
				{
					if (State < WorkerState.Disconnecting)
					{
						EnqueueDisconnectionEvent(ErrorType.DisconnectFromRemote, ErrorType.TCPConnectFailure, string.Format("close socket completion"));
						State = WorkerState.Disconnecting;
					}
				}
				else
				{
					lastTcpStreamReceivedTimeMs = PreciseCurrentTime.GetTimeMs();
					tcpLayer_C.recvStream.PushBack_Copy(tcpLayer_C.recvBuffer, num);
					netClientStats.totalTcpReceiveBytes += (ulong)num;
					ReceivedMessageList ret = new ReceivedMessageList();
					ExtractMessageFromTcpStream(ref ret);
					foreach (ReceivedMessage item in ret)
					{
						preFinalRecvQueue.Enqueue(item);
					}
					ProcessEveryMessageOrMoveToFinalRecvQueue(null);
				}
				if (State < WorkerState.Disconnecting)
				{
					SocketError socketError2 = tcpLayer_C.IssueRecvAndCheck(tcpRecvAsyncCallback);
					if (socketError2 != SocketError.Success)
					{
						EnqueueDisconnectionEvent(ErrorType.TCPConnectFailure, ErrorType.Unexpected, string.Format("{0} Tcp issueRecv Failed SocketErrorCode", socketError2));
						State = WorkerState.Disconnecting;
					}
				}
			}
		}

		private void ToServerTcp_SendCallback(IAsyncResult ar)
		{
			TcpLayer_C tcpLayer_C = (TcpLayer_C)ar.AsyncState;
			if (tcpLayer_C == null)
			{
				return;
			}
			lock (m_critSec)
			{
				tcpLayer_C.sendIssued = false;
				int num = 0;
				try
				{
					num = tcpLayer_C.socket.EndSend(ar);
				}
				catch (SocketException ex)
				{
					num = -1;
					string message = ex.Message;
					SocketError socketErrorCode = ex.SocketErrorCode;
				}
				catch (Exception ex2)
				{
					num = -1;
					string message2 = ex2.Message;
				}
				if (num < 0)
				{
					EnqueueDisconnectionEvent(ErrorType.DisconnectFromRemote, ErrorType.TCPConnectFailure, string.Format("compdatalength < 0 sendcomplete"));
					return;
				}
				if (num > 0)
				{
					tcpLayer_C.sendQueue.PopFront(num);
					netClientStats.totalTcpSendBytes += (ulong)num;
				}
				tcpLayer_C.IssueSendOnNeed(PreciseCurrentTime.GetTimeMs(), tcpSendAsyncCallback);
			}
		}

		private void UdpReceiveCallback(IAsyncResult ar)
		{
			UdpSocket_C udpSocket_C = (UdpSocket_C)ar.AsyncState;
			if (udpSocket_C == null)
			{
				return;
			}
			lock (m_critSec)
			{
				udpSocket_C.recvIssued = false;
				if (udpSocket_C.IsSocketClosed())
				{
					return;
				}
				int num = 0;
				IPAddress iPAddress = IPAddress.Parse(connectionParam.serverIP);
				IPEndPoint iPEndPoint = ((iPAddress.AddressFamily != AddressFamily.InterNetwork) ? new IPEndPoint(IPAddress.IPv6Any, 0) : new IPEndPoint(IPAddress.Any, 0));
				EndPoint end_point = iPEndPoint;
				try
				{
					num = udpSocket_C.socket.EndReceiveFrom(ar, ref end_point);
					iPEndPoint = (IPEndPoint)end_point;
				}
				catch (SocketException ex)
				{
					string message = ex.Message;
					SocketError socketErrorCode = ex.SocketErrorCode;
				}
				catch (Exception ex2)
				{
					string message2 = ex2.Message;
				}
				if (udpSocket_C.recycleTime == 0 && num > 0)
				{
					long timeMs = PreciseCurrentTime.GetTimeMs();
					netClientStats.totalUdpReceiveCount++;
					netClientStats.totalUdpReceiveBytes += (ulong)num;
					AssembledPacket output = new AssembledPacket();
					string outError = "";
					switch (udpSocket_C.udpPacketDefragBoard.PushFragmentAndPopAssembledPacket(udpSocket_C.recvBuffer, num, iPEndPoint, GetSrcHostIDByAddrAtDestSide_NOLOCK(iPEndPoint), localHostID, timeMs, MessageMaxLength, ref output, ref outError))
					{
					case AssembledPacketError.Ok:
					{
						ReceivedMessageList ret = new ReceivedMessageList();
						ErrorType outError2;
						ExtractMessageFromUdpRecvQueue(output.packet.assembledData.data, output.packet.assembledData.Count, output.senderAddr, ref ret, out outError2);
						foreach (ReceivedMessage item in ret)
						{
							preFinalRecvQueue.Enqueue(item);
						}
						if (!udpSocket_C.garbaged || udpSocket_C == toServerUdpSocket)
						{
							ProcessEveryMessageOrMoveToFinalRecvQueue(udpSocket_C);
						}
						break;
					}
					case AssembledPacketError.Error:
						EnquePacketDefragWarning(iPEndPoint, outError);
						break;
					}
				}
				udpSocket_C.IssueRecvFrom_UnlessSocketClosed(PreciseCurrentTime.GetTimeMs(), udpRecvAsyncCallback);
			}
		}

		private void UdpSendCallback(IAsyncResult ar)
		{
			UdpSocket_C udpSocket_C = (UdpSocket_C)ar.AsyncState;
			lock (m_critSec)
			{
				udpSocket_C.sendIssued = false;
				int num = 0;
				try
				{
					num = udpSocket_C.socket.EndSend(ar);
				}
				catch (SocketException ex)
				{
					num = -1;
					string message = ex.Message;
					SocketError socketErrorCode = ex.SocketErrorCode;
				}
				catch (Exception ex2)
				{
					num = -1;
					string message2 = ex2.Message;
				}
				udpSocket_C.RestoreTtlOnCompletion();
				if (num > 0)
				{
					netClientStats.totalUdpSendCount++;
					netClientStats.totalUdpSendBytes += (ulong)num;
				}
				udpSocket_C.IssueSendOnNeed_IfPossible(PreciseCurrentTime.GetTimeMs(), udpSendAsyncCallback);
			}
		}

		private void ExtractMessageFromTcpStream(ref ReceivedMessageList ret)
		{
			ret.Clear();
			int length = ToServerTcp.recvStream.Length;
			ErrorType outError;
			int num = TcpLayer_C.ExtractMessagesFromStreamAndRemoveFlushedStream(ToServerTcp.recvStream, ref ret, HostID.Server, settings.clientMessageMaxLength, out outError);
			if (num < 0)
			{
				EnqueError(ErrorInfo.From(outError, HostID.Server, "Received stream from TCP server became inconsistent!"));
				if (ToServerTcp != null && ToServerTcp.socket != null)
				{
					ToServerTcp.CloseSocket();
				}
			}
		}

		private void ExtractMessageFromUdpRecvQueue(byte[] udpPacket, int udpPacketLength, IPEndPoint remoteAddr, ref ReceivedMessageList ret, out ErrorType outError)
		{
			ret.Clear();
			int count = ret.Count;
			TcpLayerMessageExtractor tcpLayerMessageExtractor = new TcpLayerMessageExtractor();
			tcpLayerMessageExtractor.recvStream = udpPacket;
			tcpLayerMessageExtractor.recvStreamCount = udpPacketLength;
			tcpLayerMessageExtractor.extractedMessageAddTarget = ret;
			tcpLayerMessageExtractor.senderHostID = HostID.None;
			tcpLayerMessageExtractor.messageMaxLength = settings.clientMessageMaxLength;
			int num = tcpLayerMessageExtractor.Extract(0, out outError);
			if (num < 0)
			{
				EnqueWarning(ErrorInfo.From(outError, localHostID, "ExtractMessageFromUdpRecvQueue : addedCount < 0"));
				return;
			}
			RemotePeer peerByUdpAddr = GetPeerByUdpAddr(remoteAddr);
			for (int i = 0; i < num; i++)
			{
				ReceivedMessage receivedMessage = ret[count + i];
				receivedMessage.remoteAddr_onlyUdp = new IPEndPoint(remoteAddr.Address, remoteAddr.Port);
				if (peerByUdpAddr != null)
				{
					receivedMessage.remoteHostID = peerByUdpAddr.peerHostID;
					long num2 = PreciseCurrentTime.GetTimeMs() - peerByUdpAddr.lastDirectUdpPacketReceivedTimeMs;
					if (num2 > 0)
					{
						peerByUdpAddr.lastUdpPacketReceivedIntervalMs = num2;
					}
					peerByUdpAddr.lastDirectUdpPacketReceivedTimeMs = PreciseCurrentTime.GetTimeMs();
					peerByUdpAddr.directUdpPacketReceiveCount++;
				}
				else if (remoteAddr.Equals(toServerUdp_fallbackable.serverAddr))
				{
					receivedMessage.remoteHostID = HostID.Server;
					LogLastServerUdpPacketReceived();
				}
				else
				{
					receivedMessage.remoteHostID = HostID.None;
				}
			}
		}

		private void ProcessEveryMessageOrMoveToFinalRecvQueue(UdpSocket_C processUdpSocket)
		{
			while (preFinalRecvQueue.Count > 0)
			{
				ReceivedMessage ri = preFinalRecvQueue.Dequeue();
				ProcessMessageOrMoveToFinalRecvQueue(processUdpSocket, ri);
			}
		}

		private void ProcessMessageOrMoveToFinalRecvQueue(UdpSocket_C processUdpSocket, ReceivedMessage ri)
		{
			ProcessMessage_ProudNetLayer(processUdpSocket, ri);
		}

		private bool IsFromRemoteClientPeer(ReceivedMessage receivedInfo)
		{
			if (receivedInfo.RemoteHostID != HostID.Server)
			{
				return receivedInfo.RemoteHostID != HostID.None;
			}
			return false;
		}

		private bool ProcessMessage_ProudNetLayer(UdpSocket_C processUdpSocket, ReceivedMessage receivedInfo)
		{
			Message unsafeMessage = receivedInfo.unsafeMessage;
			int readOffset = unsafeMessage.ReadOffset;
			MessageType b = MessageType.None;
			if (!unsafeMessage.Read(out b))
			{
				unsafeMessage.ReadOffset = readOffset;
				return false;
			}
			bool refMessageProcessed = false;
			switch (b)
			{
			case MessageType.Rmi:
				ProcessMessage_Rmi(receivedInfo, ref refMessageProcessed);
				break;
			case MessageType.UserMessage:
				ProcessMessage_UserMessage(receivedInfo, ref refMessageProcessed);
				break;
			case MessageType.ConnectServerTimedout:
				if (!IsFromRemoteClientPeer(receivedInfo))
				{
					ProcessMessage_ConnectServerTimedout(unsafeMessage);
				}
				refMessageProcessed = true;
				break;
			case MessageType.NotifyServerConnectionHint:
				if (!IsFromRemoteClientPeer(receivedInfo))
				{
					ProcessMessage_NotifyServerConnectionHint(unsafeMessage);
				}
				refMessageProcessed = true;
				break;
			case MessageType.NotifyCSSessionKeySuccess:
				if (!IsFromRemoteClientPeer(receivedInfo))
				{
					ProcessMessage_NotifyCSSessionKeySuccess(unsafeMessage);
				}
				refMessageProcessed = true;
				break;
			case MessageType.NotifyProtocolVersionMismatch:
				if (!IsFromRemoteClientPeer(receivedInfo))
				{
					ProcessMessage_NotifyProtocolVersionMismatch(unsafeMessage);
				}
				refMessageProcessed = true;
				break;
			case MessageType.NotifyServerDeniedConnection:
				if (!IsFromRemoteClientPeer(receivedInfo))
				{
					ProcessMessage_NotifyServerDeniedConnection(unsafeMessage);
				}
				refMessageProcessed = true;
				break;
			case MessageType.NotifyServerConnectSuccess:
				if (!IsFromRemoteClientPeer(receivedInfo))
				{
					ProcessMessage_NotifyServerConnectSuccess(unsafeMessage);
				}
				refMessageProcessed = true;
				break;
			case MessageType.RequestStartServerHolepunch:
				if (!IsFromRemoteClientPeer(receivedInfo))
				{
					ProcessMessage_RequestStartServerHolepunch(unsafeMessage);
				}
				refMessageProcessed = true;
				break;
			case MessageType.ServerHolepunchAck:
				if (!IsFromRemoteClientPeer(receivedInfo))
				{
					ProcessMessage_ServerHolepunchAck(receivedInfo);
				}
				refMessageProcessed = true;
				break;
			case MessageType.NotifyClientServerUdpMatched:
				if (!IsFromRemoteClientPeer(receivedInfo))
				{
					ProcessMessage_NotifyClientServerUdpMatched(unsafeMessage);
				}
				refMessageProcessed = true;
				break;
			case MessageType.PeerUdp_ServerHolepunchAck:
				if (!IsFromRemoteClientPeer(receivedInfo))
				{
					ProcessMessage_PeerUdp_ServerHolepunchAck(receivedInfo);
				}
				refMessageProcessed = true;
				break;
			case MessageType.ReliableUdp_Frame:
				ProcessMessage_ReliableUdp_Frame(processUdpSocket, receivedInfo);
				refMessageProcessed = true;
				break;
			case MessageType.ReliableRelay2:
				if (!IsFromRemoteClientPeer(receivedInfo))
				{
					ProcessMessage_ReliableRelay2(unsafeMessage);
				}
				refMessageProcessed = true;
				break;
			case MessageType.UnreliableRelay2:
				if (!IsFromRemoteClientPeer(receivedInfo))
				{
					ProcessMessage_UnreliableRelay2(processUdpSocket, receivedInfo);
				}
				refMessageProcessed = true;
				break;
			case MessageType.LingerDataFrame2:
				if (!IsFromRemoteClientPeer(receivedInfo))
				{
					ProcessMessage_LingerDataFrame2(processUdpSocket, receivedInfo);
				}
				refMessageProcessed = true;
				break;
			case MessageType.UnreliablePong:
				if (!IsFromRemoteClientPeer(receivedInfo))
				{
					ProcessMessage_UnreliablePong(unsafeMessage);
				}
				refMessageProcessed = true;
				break;
			case MessageType.ArbitaryTouch:
				refMessageProcessed = true;
				break;
			case MessageType.PeerUdp_PeerHolepunch:
				ProcessMessage_PeerUdp_PeerHolepunch(receivedInfo);
				refMessageProcessed = true;
				break;
			case MessageType.PeerUdp_PeerHolepunchAck:
				ProcessMessage_PeerHolepunchAck(receivedInfo);
				refMessageProcessed = true;
				break;
			case MessageType.P2PUnreliablePing:
				ProcessMessage_P2PRequestIndirectServerTimeAndPing(receivedInfo);
				refMessageProcessed = true;
				break;
			case MessageType.P2PUnreliablePong:
				ProcessMessage_P2PReplyIndirectServerTimeAndPong(receivedInfo);
				refMessageProcessed = true;
				break;
			case MessageType.S2CRoutedMulticast1:
				if (!IsFromRemoteClientPeer(receivedInfo))
				{
					ProcessMessage_S2CRoutedMulticast1(processUdpSocket, receivedInfo);
				}
				refMessageProcessed = true;
				break;
			case MessageType.S2CRoutedMulticast2:
				ProcessMessage_S2CRoutedMulticast2(processUdpSocket, receivedInfo);
				refMessageProcessed = true;
				break;
			case MessageType.Encrypted_Reliable:
			case MessageType.Encrypted_UnReliable:
			{
				ReceivedMessage receivedMessage2 = new ReceivedMessage();
				receivedMessage2.unsafeMessage = new Message();
				if (ProcessMessage_Encrypted(b, receivedInfo, receivedMessage2.unsafeMessage))
				{
					receivedMessage2.relayed = receivedInfo.relayed;
					receivedMessage2.remoteAddr_onlyUdp = receivedInfo.remoteAddr_onlyUdp;
					receivedMessage2.remoteHostID = receivedInfo.remoteHostID;
					refMessageProcessed |= ProcessMessage_ProudNetLayer(processUdpSocket, receivedMessage2);
				}
				break;
			}
			case MessageType.Compressed:
			{
				ReceivedMessage receivedMessage = new ReceivedMessage();
				receivedMessage.unsafeMessage = new Message();
				if (ProcessMessage_Compressed(receivedInfo, receivedMessage.unsafeMessage))
				{
					receivedMessage.relayed = receivedInfo.relayed;
					receivedMessage.remoteAddr_onlyUdp = receivedInfo.remoteAddr_onlyUdp;
					receivedMessage.remoteHostID = receivedInfo.remoteHostID;
					refMessageProcessed |= ProcessMessage_ProudNetLayer(processUdpSocket, receivedMessage);
				}
				break;
			}
			case MessageType.RequestReceiveSpeedAtReceiverSide_NoRelay:
				ProcessMessage_RequestReceiveSpeedAtReceiverSide_NoRelay(processUdpSocket, receivedInfo);
				refMessageProcessed = true;
				break;
			case MessageType.ReplyReceiveSpeedAtReceiverSide_NoRelay:
				ProcessMessage_ReplyReceiveSpeedAtReceiverSide_NoRelay(receivedInfo);
				refMessageProcessed = true;
				break;
			case MessageType.PolicyRequest:
				refMessageProcessed = true;
				break;
			case MessageType.P2PReliablePing:
				ProcessMessage_P2PReliablePing(receivedInfo);
				refMessageProcessed = true;
				break;
			case MessageType.P2PReliablePong:
				ProcessMessage_P2PReliablePong(receivedInfo);
				refMessageProcessed = true;
				break;
			}
			int length = receivedInfo.ReadOnlyMessage.Length;
			int readOffset2 = receivedInfo.ReadOnlyMessage.ReadOffset;
			if (refMessageProcessed && length != readOffset2 && b != MessageType.Encrypted_Reliable && b != MessageType.Encrypted_UnReliable)
			{
				refMessageProcessed = true;
				string comment = string.Format("{0}위치에서 MessageType:{1}", "ProudNetLayer", (int)b);
				ByteArray lastreceivedMessage = new ByteArray(unsafeMessage.Data.data);
				EnqueError(ErrorInfo.From(ErrorType.InvalidPacketFormat, receivedInfo.remoteHostID, comment, lastreceivedMessage));
			}
			if (!refMessageProcessed)
			{
				unsafeMessage.ReadOffset = readOffset;
				return false;
			}
			return true;
		}

		private void ProcessMessage_ConnectServerTimedout(Message msg)
		{
			EnqueueConnectFailEvent(ErrorType.ConnectServerTimeout, SocketError.Success, null, null);
			State = WorkerState.Disconnecting;
		}

		private void ProcessMessage_NotifyServerConnectionHint(Message msg)
		{
			bool b = false;
			if (!msg.Read(out b))
			{
				EnqueueDisconnectionEvent(ErrorType.ProtocolVersionMismatch, ErrorType.TCPConnectFailure, "");
				State = WorkerState.Disconnecting;
				return;
			}
			NetSettings b2 = new NetSettings();
			if (!msg.Read(out b2))
			{
				EnqueueDisconnectionEvent(ErrorType.ProtocolVersionMismatch, ErrorType.TCPConnectFailure, "");
				State = WorkerState.Disconnecting;
				return;
			}
			settings = b2;
			if (b2.enablePingTest)
			{
				enablePingTestEndTimeMs = PreciseCurrentTime.GetTimeMs();
			}
			reliablePing_Timer.Interval = ReliablePingTimerIntervalMs;
			ByteArray b3 = new ByteArray();
			if (!msg.Read(out b3))
			{
				EnqueueDisconnectionEvent(ErrorType.ProtocolVersionMismatch, ErrorType.TCPConnectFailure, "");
				State = WorkerState.Disconnecting;
				return;
			}
			ByteArray byteArray = new ByteArray();
			ByteArray byteArray2 = new ByteArray();
			if (!CryptoRsa.CreateRandomBlock(byteArray, b2.encryptedMessageKeyLength) || !CryptoAes.ExpandFrom(selfP2PSessionKey.aesKey, byteArray.data, 0, b2.encryptedMessageKeyLength / 8, CryptoAes.DEFAULT_BLOCK_SIZE) || !CryptoRsa.CreateRandomBlock(byteArray2, b2.fastEncryptedMessageKeyLength) || !CryptoRc4.ExpandFrom(selfP2PSessionKey.rc4Key, byteArray2.data, b2.fastEncryptedMessageKeyLength / 8))
			{
				EnqueueDisconnectionEvent(ErrorType.EncryptFail, ErrorType.TCPConnectFailure, "encrypt key switch fail");
				State = WorkerState.Disconnecting;
				return;
			}
			if (msg.ReadOffset != msg.Length)
			{
				EnqueueDisconnectionEvent(ErrorType.ProtocolVersionMismatch, ErrorType.TCPConnectFailure, "");
				State = WorkerState.Disconnecting;
				return;
			}
			enableLog = b;
			try
			{
				ToServerTcp.EnableNagleAlgorithm = settings.enableNagleAlgorithm;
			}
			catch (Exception)
			{
				EnqueWarning(ErrorInfo.From(ErrorType.PermissionDenied, localHostID, "Warning: Failed to set Nagle Algorithm into the Socket."));
			}
			ByteArray byteArray3 = new ByteArray();
			ByteArray byteArray4 = new ByteArray();
			if (!CryptoRsa.CreateRandomBlock(byteArray, b2.encryptedMessageKeyLength) || !CryptoAes.ExpandFrom(toServerSessionKey.aesKey, byteArray.data, 0, b2.encryptedMessageKeyLength / 8, CryptoAes.DEFAULT_BLOCK_SIZE) || !CryptoRsa.EncryptSessionKeyByPublicKey(byteArray3, byteArray, b3) || !CryptoRsa.CreateRandomBlock(byteArray2, b2.fastEncryptedMessageKeyLength) || !CryptoRc4.ExpandFrom(toServerSessionKey.rc4Key, byteArray2.data, b2.fastEncryptedMessageKeyLength / 8) || !CryptoAes.EncryptByteArray(toServerSessionKey.aesKey, byteArray2, byteArray4))
			{
				EnqueueDisconnectionEvent(ErrorType.EncryptFail, ErrorType.TCPConnectFailure, "encrypted key swap fail");
				State = WorkerState.Disconnecting;
				return;
			}
			Message message = new Message();
			message.Write(MessageType.NotifyCSEncryptedSessionKey);
			message.Write(byteArray3);
			message.Write(byteArray4);
			ToServerTcp.AddToSendQueueWithSplitterAndSignal_Copy(new SendFragRefs(message), new SendOpt());
		}

		private void ProcessMessage_NotifyCSSessionKeySuccess(Message msg)
		{
			Message message = new Message();
			message.Write(MessageType.NotifyServerConnectionRequestData);
			message.Write(connectionParam.userData);
			message.Write(connectionParam.protocolVersion);
			message.Write(internalVersion);
			ToServerTcp.AddToSendQueueWithSplitterAndSignal_Copy(new SendFragRefs(message), new SendOpt());
		}

		private void ProcessMessage_NotifyProtocolVersionMismatch(Message msg)
		{
			EnqueueConnectFailEvent(ErrorType.ProtocolVersionMismatch, SocketError.Success, null, null);
			State = WorkerState.Disconnecting;
		}

		private void ProcessMessage_NotifyServerDeniedConnection(Message msg)
		{
			ByteArray b = new ByteArray();
			msg.Read(out b);
			EnqueueConnectFailEvent(ErrorType.NotifyServerDeniedConnection, SocketError.Success, null, b);
			State = WorkerState.Disconnecting;
		}

		private void ProcessMessage_NotifyServerConnectSuccess(Message msg)
		{
			ByteArray b = new ByteArray();
			HostID b2 = HostID.None;
			NamedAddrPort b3 = new NamedAddrPort();
			Guid b4;
			if (!msg.Read(out b2) || !msg.Read(out b4) || !msg.Read(out b) || !msg.Read(out b3))
			{
				EnqueueConnectFailEvent(ErrorType.InvalidPacketFormat, ErrorInfo.From(ErrorType.ProtocolVersionMismatch, HostID.Server, "Bad format in NotifyServerConnectSuccess", null));
				State = WorkerState.Disconnecting;
				return;
			}
			localHostID = b2;
			if (ToServerTcp != null)
			{
				ToServerTcp.localAddrAtServer = b3.ToAddrPort();
			}
			EnqueLocalEvent(new ConnectLocalEvent
			{
				type = LocalEventType.ConnectServerSuccess,
				userData = b
			});
			serverInstanceGuid = b4;
			if (enableLog || settings.emergencyLogLineCount > 0)
			{
				Log(TraceID.Holepunch, string.Format("HostID={0} Connecting to server successful.", b2));
			}
		}

		private void ProcessMessage_RequestStartServerHolepunch(Message msg)
		{
			msg.Read(out toServerUdp_fallbackable.holepunchMagicNumber);
			toServerUdp_fallbackable.holepunchCooltime = 0L;
		}

		private void ProcessMessage_ServerHolepunchAck(ReceivedMessage ri)
		{
			Message unsafeMessage = ri.unsafeMessage;
			IPEndPoint b = new IPEndPoint(0L, 0);
			Guid b2;
			if (unsafeMessage.Read(out b2) && unsafeMessage.Read(out b) && b2.Equals(toServerUdp_fallbackable.holepunchMagicNumber) && toServerUdp_fallbackable.serverAddr.Equals(ri.remoteAddr_onlyUdp))
			{
				Message message = new Message();
				message.Write(MessageType.NotifyHolepunchSuccess);
				message.Write(toServerUdp_fallbackable.holepunchMagicNumber);
				message.Write(ToServerUdpSocketLocalAddr);
				message.Write(b);
				SendFragRefs sendFragRefs = new SendFragRefs();
				sendFragRefs.Add(message);
				toServerUdpSocket.addrOfHereAtServer = b;
				ToServerTcp.AddToSendQueueWithSplitterAndSignal_Copy(sendFragRefs, new SendOpt());
				if (enableLog || settings.emergencyLogLineCount > 0)
				{
					Log(TraceID.Holepunch, string.Format("Message_ServerHolepunchAck. AddrOfHereAtServer={0}", b.ToString()));
				}
			}
		}

		private void ProcessMessage_PeerUdp_ServerHolepunchAck(ReceivedMessage ri)
		{
			Message unsafeMessage = ri.unsafeMessage;
			IPEndPoint b = new IPEndPoint(0L, 0);
			HostID b2 = HostID.None;
			Guid b3;
			if (unsafeMessage.Read(out b3) && unsafeMessage.Read(out b) && unsafeMessage.Read(out b2))
			{
				RemotePeer peerByHostID = GetPeerByHostID(b2);
				if (peerByHostID != null && !peerByHostID.garbaged && peerByHostID.p2pConnectionTrialContext != null)
				{
					peerByHostID.p2pConnectionTrialContext.ProcessMessage_PeerUdp_ServerHolepunchAck(ri, b3, b, b2);
				}
			}
		}

		private void ProcessMessage_NotifyClientServerUdpMatched(Message msg)
		{
			msg.Read(out toServerUdp_fallbackable.holepunchMagicNumber);
			toServerUdp_fallbackable.RealUdpEnabled = true;
			EnqueLocalEvent(new LocalEvent
			{
				type = LocalEventType.ServerUdpChanged,
				errorInfo = new ErrorInfo
				{
					remote = HostID.Server
				}
			});
			if (enableLog || settings.emergencyLogLineCount > 0)
			{
				Log(TraceID.Holepunch, string.Format("Client {0}: Holepunch to server UDP successful.", localHostID));
			}
		}

		private void ProcessMessage_UnreliablePong(Message msg)
		{
			long b = 0L;
			long b2 = 0L;
			if (msg.Read(out b) && msg.Read(out b2))
			{
				long timeMs = PreciseCurrentTime.GetTimeMs();
				int num = (serverUdpLastPingMs = (int)((timeMs - b) / 2));
				if (serverUdpRecentPingMs == 0)
				{
					serverUdpRecentPingMs = num;
				}
				else
				{
					serverUdpRecentPingMs = (int)Sysutil.LerpInt(serverUdpRecentPingMs, num, NetConfig.LagLinearProgrammingFactorPercent, 100L);
				}
				long num2 = b2 + serverUdpRecentPingMs;
				dxServerTimeDiffMs = timeMs - num2;
				EnqueLocalEvent(new LocalEvent
				{
					type = LocalEventType.SynchronizeServerTime,
					errorInfo = new ErrorInfo
					{
						remote = HostID.Server
					}
				});
			}
		}

		private void ProcessMessage_ReliableRelay2(Message msg)
		{
			HostID b = HostID.None;
			FrameNumber b2 = (FrameNumber)0;
			int a = 0;
			if (!msg.Read(out b) || !msg.Read(out b2) || !msg.ReadScalar(ref a) || a < 0 || a >= NetConfig.MessageMaxLength)
			{
				return;
			}
			ByteArray byteArray = new ByteArray();
			byteArray.Count = a;
			if (!msg.Read(out byteArray.data, a))
			{
				return;
			}
			RemotePeer peerByHostID = GetPeerByHostID(b);
			if (peerByHostID == null || peerByHostID.garbaged || peerByHostID.toPeerReliableUdp.failed)
			{
				return;
			}
			ReliableUdpFrame reliableUdpFrame = new ReliableUdpFrame();
			reliableUdpFrame.type = ReliableUdpFrameType.Data;
			reliableUdpFrame.frameNumber = b2;
			reliableUdpFrame.data = byteArray;
			ReceivedMessageList ret = new ReceivedMessageList();
			ErrorType outError = ErrorType.Ok;
			peerByHostID.toPeerReliableUdp.EnqueReceivedFrameAndGetFlushedMessages(reliableUdpFrame, ref ret, out outError);
			if (outError != ErrorType.Ok)
			{
				EnqueError(ErrorInfo.From(outError, peerByHostID.peerHostID, "Stream Extract Error at Reliable UDP"));
			}
			foreach (ReceivedMessage item in ret)
			{
				item.relayed = true;
				item.unsafeMessage.ReadOffset = 0;
				ProcessMessageOrMoveToFinalRecvQueue(null, item);
			}
		}

		private void ProcessMessage_UnreliableRelay2(UdpSocket_C processUdpSocket, ReceivedMessage receivedInfo)
		{
			Message readOnlyMessage = receivedInfo.ReadOnlyMessage;
			if (receivedInfo.RemoteHostID != HostID.Server)
			{
				return;
			}
			HostID b = HostID.None;
			int a = 0;
			if (!readOnlyMessage.Read(out b) || !readOnlyMessage.ReadScalar(ref a) || a < 0 || a >= NetConfig.MessageMaxLength)
			{
				return;
			}
			Message message = new Message();
			message.Length = a;
			if (readOnlyMessage.Read(out message.Data.data, a))
			{
				RemotePeer peerByHostID = GetPeerByHostID(b);
				if (peerByHostID != null && !peerByHostID.garbaged)
				{
					ProcessMessageOrMoveToFinalRecvQueue(processUdpSocket, new ReceivedMessage
					{
						relayed = true,
						unsafeMessage = message,
						remoteHostID = b
					});
				}
			}
		}

		private void ProcessMessage_LingerDataFrame2(UdpSocket_C processUdpSocket, ReceivedMessage rm)
		{
			if (rm.RemoteHostID != HostID.Server)
			{
				return;
			}
			HostID remoteHostID = rm.RemoteHostID;
			Message readOnlyMessage = rm.ReadOnlyMessage;
			HostID b = HostID.None;
			FrameNumber b2 = (FrameNumber)0;
			int a = 0;
			if (!readOnlyMessage.Read(out b) || !readOnlyMessage.Read(out b2) || !readOnlyMessage.ReadScalar(ref a) || a < 0 || a >= NetConfig.MessageMaxLength)
			{
				return;
			}
			ByteArray byteArray = new ByteArray();
			byteArray.Count = a;
			if (!readOnlyMessage.Read(out byteArray.data, a))
			{
				return;
			}
			RemotePeer peerByHostID = GetPeerByHostID(b);
			if (peerByHostID == null || peerByHostID.garbaged || peerByHostID.toPeerReliableUdp.failed)
			{
				return;
			}
			ReliableUdpFrame reliableUdpFrame = new ReliableUdpFrame();
			reliableUdpFrame.type = ReliableUdpFrameType.Data;
			reliableUdpFrame.frameNumber = b2;
			reliableUdpFrame.data = byteArray;
			reliableUdpFrame.data.Count = byteArray.Count;
			ReceivedMessageList ret = new ReceivedMessageList();
			ErrorType outError;
			peerByHostID.toPeerReliableUdp.EnqueReceivedFrameAndGetFlushedMessages(reliableUdpFrame, ref ret, out outError);
			if (outError != ErrorType.Ok)
			{
				EnqueError(ErrorInfo.From(outError, peerByHostID.peerHostID, "Stream Extract Error at Reliable UDP"));
			}
			foreach (ReceivedMessage item in ret)
			{
				item.relayed = true;
				ProcessMessageOrMoveToFinalRecvQueue(processUdpSocket, item);
			}
		}

		private void ProcessMessage_ReliableUdp_Frame(UdpSocket_C processUdpSocket, ReceivedMessage ri)
		{
			RemotePeer peerByUdpAddr = GetPeerByUdpAddr(ri.remoteAddr_onlyUdp);
			if (peerByUdpAddr != null && !peerByUdpAddr.toPeerReliableUdp.failed)
			{
				ReceivedMessageList ret = new ReceivedMessageList();
				ErrorType outError;
				peerByUdpAddr.toPeerReliableUdp.EnqueReceivedFrameAndGetFlushedMessages(ri.unsafeMessage, ref ret, out outError);
				if (outError != ErrorType.Ok)
				{
					EnqueError(ErrorInfo.From(outError, peerByUdpAddr.peerHostID, "Stream Extract Error at Reliable UDP"));
				}
				{
					foreach (ReceivedMessage item in ret)
					{
						item.relayed = ri.relayed;
						ProcessMessageOrMoveToFinalRecvQueue(processUdpSocket, item);
					}
					return;
				}
			}
			ri.unsafeMessage.SkipRead(ri.unsafeMessage.Length - ri.unsafeMessage.ReadOffset);
		}

		private void ProcessMessage_PeerUdp_PeerHolepunch(ReceivedMessage ri)
		{
			P2PConnectionTrialContext.ProcessPeerHolepunch(this, ri);
		}

		private void ProcessMessage_PeerHolepunchAck(ReceivedMessage ri)
		{
			P2PConnectionTrialContext.ProcessPeerHolepunchAck(this, ri);
		}

		private void ProcessMessage_P2PRequestIndirectServerTimeAndPing(ReceivedMessage ri)
		{
			long b = 0L;
			if (ri.ReadOnlyMessage.Read(out b))
			{
				RemotePeer peerByUdpAddr = GetPeerByUdpAddr(ri.remoteAddr_onlyUdp);
				if (peerByUdpAddr != null && peerByUdpAddr.peerHostID != HostID.Server)
				{
					Message message = new Message();
					message.Write(MessageType.P2PUnreliablePong);
					message.Write(b);
					SendFragRefs sendData = new SendFragRefs(message);
					peerByUdpAddr.ToPeerUdp.SendWithSplitter_Copy(sendData, new SendOpt(MessagePriority.Ring0, true));
				}
			}
		}

		private void ProcessMessage_P2PReliablePing(ReceivedMessage ri)
		{
			long b = 0L;
			if (ri.ReadOnlyMessage.Read(out b))
			{
				Message message = new Message();
				message.Write(MessageType.P2PReliablePong);
				message.Write(b);
				SendFragRefs payload = new SendFragRefs(message);
				Send_BroadcastLayer(payload, new SendOpt(RmiContext.ReliableSendForPN)
				{
					priority = MessagePriority.Ring0
				}, new HostID[1] { ri.RemoteHostID });
			}
		}

		private void ProcessMessage_P2PReplyIndirectServerTimeAndPong(ReceivedMessage ri)
		{
			long b = 0L;
			if (!ri.ReadOnlyMessage.Read(out b))
			{
				return;
			}
			RemotePeer peerByUdpAddr = GetPeerByUdpAddr(ri.remoteAddr_onlyUdp);
			if (peerByUdpAddr != null && peerByUdpAddr.peerHostID != HostID.Server)
			{
				long timeMs = PreciseCurrentTime.GetTimeMs();
				int num = (peerByUdpAddr.lastPingMs = (int)((timeMs - b) / 2));
				if (peerByUdpAddr.recentPingMs > 0)
				{
					peerByUdpAddr.recentPingMs = (int)Sysutil.LerpInt(peerByUdpAddr.recentPingMs, num, NetConfig.LagLinearProgrammingFactorPercent, 100L);
				}
				else
				{
					peerByUdpAddr.recentPingMs = num;
				}
				if (timeMs > 0)
				{
					peerByUdpAddr.lastDirectUdpPacketReceivedTimeMs = timeMs;
					peerByUdpAddr.directUdpPacketReceiveCount++;
				}
			}
		}

		private void ProcessMessage_P2PReliablePong(ReceivedMessage ri)
		{
			long b = 0L;
			if (!ri.ReadOnlyMessage.Read(out b))
			{
				return;
			}
			RemotePeer peerByHostID = GetPeerByHostID(ri.RemoteHostID);
			if (peerByHostID != null && peerByHostID.peerHostID != HostID.Server)
			{
				long timeMs = PreciseCurrentTime.GetTimeMs();
				int num = (peerByHostID.lastReliablePingMs = (int)((timeMs - b) / 2));
				if (peerByHostID.recentReliablePingMs > 0)
				{
					peerByHostID.recentReliablePingMs = (int)Sysutil.LerpInt(peerByHostID.recentReliablePingMs, num, NetConfig.LagLinearProgrammingFactorPercent, 100L);
				}
				else
				{
					peerByHostID.recentReliablePingMs = num;
				}
				peerByHostID.lastDirectUdpPacketReceivedTimeMs = PreciseCurrentTime.GetTimeMs();
				peerByHostID.directUdpPacketReceiveCount++;
			}
		}

		private bool ProcessMessage_S2CRoutedMulticast1(UdpSocket_C processUdpSocket, ReceivedMessage ri)
		{
			Message unsafeMessage = ri.unsafeMessage;
			if (ri.RemoteHostID != HostID.Server)
			{
				return false;
			}
			MessagePriority b = MessagePriority.Low;
			long a = 0L;
			if (!unsafeMessage.Read(out b) || !unsafeMessage.ReadScalar(ref a))
			{
				return false;
			}
			HostIDArray b2 = new HostIDArray();
			if (!unsafeMessage.Read(out b2))
			{
				return false;
			}
			ByteArray b3 = new ByteArray();
			if (!unsafeMessage.Read(out b3))
			{
				return false;
			}
			Message message = new Message();
			message.Write(MessageType.S2CRoutedMulticast2);
			message.Write(b3);
			SendOpt sendOpt = new SendOpt(RmiContext.UnreliableSendForPN);
			sendOpt.priority = b;
			sendOpt.uniqueID = a;
			sendOpt.INTERNAL_USE_fraggingOnNeed = false;
			for (int i = 0; i < b2.Count; i++)
			{
				if (b2[i] != localHostID)
				{
					RemotePeer peerByHostID = GetPeerByHostID(b2[i]);
					if (peerByHostID != null && !peerByHostID.garbaged && !peerByHostID.RelayedP2P && peerByHostID.ToPeerUdpSocket != null)
					{
						peerByHostID.ToPeerUdpSocket.AddToSendQueueWithSplitterAndSignal_Copy(b2[i], FilterTag.CreateFilterTag(LocalHostID, b2[i]), peerByHostID.p2pHolepunchedLocalToRemoteAddr, message, PreciseCurrentTime.GetTimeMs(), sendOpt);
					}
				}
			}
			ProcessMessageOrMoveToFinalRecvQueue(processUdpSocket, new ReceivedMessage
			{
				remoteHostID = HostID.Server,
				unsafeMessage = new Message(b3)
			});
			return true;
		}

		private bool ProcessMessage_S2CRoutedMulticast2(UdpSocket_C udpSocket, ReceivedMessage ri)
		{
			Message unsafeMessage = ri.unsafeMessage;
			ByteArray b = new ByteArray();
			if (!unsafeMessage.Read(out b))
			{
				return false;
			}
			ProcessMessageOrMoveToFinalRecvQueue(udpSocket, new ReceivedMessage
			{
				remoteHostID = HostID.Server,
				unsafeMessage = new Message(b),
				relayed = true
			});
			return true;
		}

		private void ProcessMessage_Rmi(ReceivedMessage receivedInfo, ref bool refMessageProcessed)
		{
			Message readOnlyMessage = receivedInfo.ReadOnlyMessage;
			int readOffset = readOnlyMessage.ReadOffset;
			object obj = GetHostTag(receivedInfo.remoteHostID);
			refMessageProcessed |= s2cStub.ProcessReceivedMessage(receivedInfo, obj);
			if (!refMessageProcessed)
			{
				readOnlyMessage.ReadOffset = readOffset;
				refMessageProcessed |= c2cStub.ProcessReceivedMessage(receivedInfo, obj);
			}
			if (refMessageProcessed)
			{
				return;
			}
			readOnlyMessage.ReadOffset = readOffset;
			ReceivedMessage receivedMessage = new ReceivedMessage();
			ByteArray byteArray = new ByteArray();
			byteArray.Count = readOnlyMessage.Length - readOnlyMessage.ReadOffset;
			Array.Copy(readOnlyMessage.Data.data, readOnlyMessage.ReadOffset, byteArray.data, 0, readOnlyMessage.Length - readOnlyMessage.ReadOffset);
			receivedMessage.unsafeMessage = new Message(byteArray);
			receivedMessage.relayed = receivedInfo.relayed;
			receivedMessage.remoteAddr_onlyUdp = receivedInfo.remoteAddr_onlyUdp;
			receivedMessage.remoteHostID = receivedInfo.remoteHostID;
			finalUserWorkItemQueue.Enqueue(new FinalUserWorkItem(receivedMessage, FinalUserWorkItemType.RMI));
			if (receivedMessage.remoteHostID == HostID.Server || receivedMessage.remoteHostID == HostID.None)
			{
				return;
			}
			RemotePeer peerByHostID = GetPeerByHostID(receivedMessage.remoteHostID);
			if (peerByHostID != null && !peerByHostID.garbaged)
			{
				if (!peerByHostID.m_forceRelayP2P)
				{
					peerByHostID.m_jitDirectP2PNeeded = true;
				}
				if (!receivedMessage.relayed)
				{
					peerByHostID.receiveudpMessageSuccessCount++;
				}
			}
		}

		private void ProcessMessage_UserMessage(ReceivedMessage receivedInfo, ref bool refMessageProcessed)
		{
			Message readOnlyMessage = receivedInfo.ReadOnlyMessage;
			ReceivedMessage receivedMessage = new ReceivedMessage();
			ByteArray byteArray = new ByteArray();
			byteArray.Count = readOnlyMessage.Length - readOnlyMessage.ReadOffset;
			Array.Copy(readOnlyMessage.Data.data, readOnlyMessage.ReadOffset, byteArray.data, 0, readOnlyMessage.Length - readOnlyMessage.ReadOffset);
			receivedMessage.unsafeMessage = new Message(byteArray);
			receivedMessage.relayed = receivedInfo.relayed;
			receivedMessage.remoteAddr_onlyUdp = receivedInfo.remoteAddr_onlyUdp;
			receivedMessage.remoteHostID = receivedInfo.remoteHostID;
			finalUserWorkItemQueue.Enqueue(new FinalUserWorkItem(receivedMessage, FinalUserWorkItemType.UserMessage));
			if (receivedMessage.remoteHostID == HostID.Server || receivedMessage.remoteHostID == HostID.None)
			{
				return;
			}
			RemotePeer peerByHostID = GetPeerByHostID(receivedMessage.remoteHostID);
			if (peerByHostID != null && !peerByHostID.garbaged)
			{
				if (!peerByHostID.m_forceRelayP2P)
				{
					peerByHostID.m_jitDirectP2PNeeded = true;
				}
				if (!receivedMessage.relayed)
				{
					peerByHostID.receiveudpMessageSuccessCount++;
				}
			}
		}

		private void ProcessMessage_RequestReceiveSpeedAtReceiverSide_NoRelay(UdpSocket_C processUdpSocket, ReceivedMessage receivedInfo)
		{
			if (processUdpSocket == null)
			{
				return;
			}
			long recentReceiveSpeed = processUdpSocket.udpPacketDefragBoard.GetRecentReceiveSpeed(receivedInfo.RemoteAddr);
			if (recentReceiveSpeed <= 0)
			{
				return;
			}
			Message message = new Message();
			message.Write(MessageType.ReplyReceiveSpeedAtReceiverSide_NoRelay);
			message.Write(recentReceiveSpeed);
			if (receivedInfo.RemoteHostID == HostID.Server)
			{
				toServerUdpSocket.AddToSendQueueWithSplitterAndSignal_Copy(receivedInfo.RemoteHostID, FilterTag.CreateFilterTag(LocalHostID, receivedInfo.RemoteHostID), toServerUdp_fallbackable.serverAddr, message, PreciseCurrentTime.GetTimeMs(), new SendOpt(MessagePriority.Ring1, true));
			}
			else if (receivedInfo.RemoteHostID != HostID.None)
			{
				RemotePeer peerByHostID = GetPeerByHostID(receivedInfo.RemoteHostID);
				if (peerByHostID.ToPeerUdpSocket != null)
				{
					peerByHostID.ToPeerUdpSocket.AddToSendQueueWithSplitterAndSignal_Copy(receivedInfo.RemoteHostID, FilterTag.CreateFilterTag(LocalHostID, receivedInfo.RemoteHostID), peerByHostID.p2pHolepunchedLocalToRemoteAddr, message, PreciseCurrentTime.GetTimeMs(), new SendOpt(MessagePriority.Ring1, true));
				}
			}
		}

		private void ProcessMessage_ReplyReceiveSpeedAtReceiverSide_NoRelay(ReceivedMessage receivedInfo)
		{
			Message readOnlyMessage = receivedInfo.ReadOnlyMessage;
			long b = 0L;
			if (!readOnlyMessage.Read(out b))
			{
				return;
			}
			IPEndPoint dest = null;
			UdpPacketFragBoard udpPacketFragBoard = null;
			if (receivedInfo.RemoteHostID == HostID.Server && toServerUdpSocket != null)
			{
				udpPacketFragBoard = toServerUdpSocket.udpPacketFragBoard;
				dest = toServerUdpSocket.addrOfHereAtServer;
			}
			else
			{
				RemotePeer peerByHostID = GetPeerByHostID(receivedInfo.RemoteHostID);
				if (peerByHostID != null && peerByHostID.ToPeerUdpSocket != null)
				{
					udpPacketFragBoard = peerByHostID.ToPeerUdpSocket.udpPacketFragBoard;
					dest = peerByHostID.p2pHolepunchedLocalToRemoteAddr;
				}
			}
			if (udpPacketFragBoard != null)
			{
				udpPacketFragBoard.SetReceiveSpeedAtReceiverSide(dest, b, PreciseCurrentTime.GetTimeMs());
			}
		}

		private void InitStub()
		{
			s2cStub.P2PGroup_MemberJoin = P2PGroup_MemberJoin;
			s2cStub.P2PGroup_MemberJoin_Unencrypted = P2PGroup_MemberJoin_Unencrypted;
			s2cStub.P2PRecycleComplete = P2PRecycleComplete;
			s2cStub.RequestP2PHolepunch = RequestP2PHolepunch;
			s2cStub.P2P_NotifyDirectP2PDisconnected2 = P2P_NotifyDirectP2PDisconnected2;
			s2cStub.P2PGroup_MemberLeave = P2PGroup_MemberLeave;
			s2cStub.NotifyDirectP2PEstablish = NotifyDirectP2PEstablish;
			s2cStub.ReliablePong = ReliablePong;
			s2cStub.EnableLog = EnableLog;
			s2cStub.DisableLog = DisableLog;
			s2cStub.NotifyUdpToTcpFallbackByServer = NotifyUdpToTcpFallbackByServer;
			s2cStub.RenewP2PConnectionState = RenewP2PConnectionState;
			s2cStub.RequestAutoPrune = RequestAutoPrune;
			s2cStub.RequestMeasureSendSpeed = RequestMeasureSendSpeed;
			s2cStub.S2C_CreateUdpSocketAck = S2C_CreateUdpSocketAck;
			s2cStub.S2C_RequestCreateUdpSocket = S2C_RequestCreateUdpSocket;
			s2cStub.ShutdownTcpAck = ShutdownTcpAck;
			s2cStub.NewDirectP2PConnection = NewDirectP2PConnection;
			s2cStub.NotifySpeedHackDetectorEnabled = NotifySpeedHackDetectorEnabled;
			c2cStub.HolsterP2PHolepunchTrial = HolsterP2PHolepunchTrial;
			c2cStub.ReportServerTimeAndFrameRateAndPing = ReportServerTimeAndFrameRateAndPing;
			c2cStub.ReportServerTimeAndFrameRateAndPong = ReportServerTimeAndFrameRateAndPong;
			c2cStub.ReportUdpMessageCount = ReportUdpMessageCount;
			AttachStub(s2cStub);
			AttachStub(c2cStub);
		}

		private bool P2PGroup_MemberJoin(HostID remote, RmiContext rmiContext, HostID groupHostID, HostID memberHostID, ByteArray customField, int eventID, ByteArray p2pAESSessionKey, ByteArray p2pRC4SessionKey, FrameNumber p2pFirstFrameNumber, Guid connectionMagicNumber, bool enableDirectP2P, int bindport)
		{
			lock (m_critSec)
			{
				UpdateP2PGroup_MemberJoin(groupHostID, memberHostID, customField, eventID, p2pFirstFrameNumber, connectionMagicNumber, p2pAESSessionKey, p2pRC4SessionKey, enableDirectP2P, (ushort)bindport);
			}
			return true;
		}

		private bool P2PGroup_MemberJoin_Unencrypted(HostID remote, RmiContext rmiContext, HostID groupHostID, HostID memberHostID, ByteArray customField, int eventID, FrameNumber p2pFirstFrameNumber, Guid connectionMagicNumber, bool enableDirectP2P, int bindport)
		{
			lock (m_critSec)
			{
				UpdateP2PGroup_MemberJoin(groupHostID, memberHostID, customField, eventID, p2pFirstFrameNumber, connectionMagicNumber, new ByteArray(), new ByteArray(), enableDirectP2P, (ushort)bindport);
			}
			return true;
		}

		private bool P2PRecycleComplete(HostID remote, RmiContext rmiContext, HostID remotePeerID, bool recycled, IPEndPoint internalAddr, IPEndPoint externalAddr, IPEndPoint sendAddr, IPEndPoint recvAddr)
		{
			lock (m_critSec)
			{
				RemotePeer peerByHostID = GetPeerByHostID(remotePeerID);
				if (peerByHostID != null && !peerByHostID.garbaged)
				{
					peerByHostID.m_memberJoinProcessEnd = true;
					if (recycled)
					{
						peerByHostID.udpAddrFromServer = externalAddr;
						peerByHostID.udpAddrInternal = internalAddr;
						peerByHostID.p2pHolepunchedLocalToRemoteAddr = sendAddr;
						peerByHostID.p2pHolepunchedRemoteToLocalAddr = recvAddr;
						peerByHostID.RelayedP2P = false;
						peerByHostID.p2pConnectionTrialContext = null;
						if (IsProactorAsyncModel)
						{
							peerByHostID.ToPeerUdpSocket.IssueRecvFrom_UnlessSocketClosed(PreciseCurrentTime.GetTimeMs(), udpRecvAsyncCallback);
						}
						EnqueLocalEvent(new LocalEvent
						{
							type = LocalEventType.DirectP2PEnabled,
							errorInfo = new ErrorInfo
							{
								remote = remotePeerID
							}
						});
					}
					else
					{
						peerByHostID.AssureUdpSocketNotUnderIssued();
						if (peerByHostID.udpSocket != null)
						{
							GarbageSocket(peerByHostID.udpSocket);
							Console.WriteLine(string.Format("P2PRecycleComplete GarbageSocket Addr:{0}", peerByHostID.udpSocket.localAddr.ToString()));
							peerByHostID.udpSocket = null;
						}
						peerByHostID.RelayedP2P = true;
					}
				}
				return true;
			}
		}

		private bool RequestP2PHolepunch(HostID remote, RmiContext rmiContext, HostID remotePeerID, IPEndPoint internalAddr, IPEndPoint externalAddr)
		{
			lock (m_critSec)
			{
				RemotePeer peerByHostID = GetPeerByHostID(remotePeerID);
				if (peerByHostID == null || peerByHostID.garbaged)
				{
					return true;
				}
				if (peerByHostID.p2pConnectionTrialContext == null)
				{
					return true;
				}
				peerByHostID.udpAddrFromServer = externalAddr;
				peerByHostID.udpAddrInternal = internalAddr;
				if (peerByHostID.p2pConnectionTrialContext.state == null || peerByHostID.p2pConnectionTrialContext.state.state != P2PConnectionTrialContext.State.S_PeerHolepunch)
				{
					peerByHostID.p2pConnectionTrialContext.state = null;
					P2PConnectionTrialContext.PeerHolepunchState peerHolepunchState = new P2PConnectionTrialContext.PeerHolepunchState();
					peerHolepunchState.shotgunMinPortNum = (ushort)externalAddr.Port;
					peerByHostID.p2pConnectionTrialContext.state = peerHolepunchState;
				}
				return true;
			}
		}

		private bool P2P_NotifyDirectP2PDisconnected2(HostID remote, RmiContext rmiContext, HostID remotePeerHostID, ErrorType reason)
		{
			lock (m_critSec)
			{
				RemotePeer peerByHostID = GetPeerByHostID(remotePeerHostID);
				if (peerByHostID != null && !peerByHostID.garbaged && !peerByHostID.RelayedP2P)
				{
					peerByHostID.FallbackP2PToRelay(false, reason);
				}
				return true;
			}
		}

		private bool P2PGroup_MemberLeave(HostID remote, RmiContext rmiContext, HostID memberHostID, HostID groupHostID)
		{
			lock (m_critSec)
			{
				if (enableLog || settings.emergencyLogLineCount > 0)
				{
					Log(TraceID.Holepunch, string.Format("[Client {0}] Received P2PGroup_MemberLeave : remote peer={1},group={2}\n", localHostID, memberHostID, groupHostID));
				}
				RemotePeer peerByHostID = GetPeerByHostID(memberHostID);
				P2PGroup_C p2PGroupByHostID_Internal = GetP2PGroupByHostID_Internal(groupHostID);
				if (p2PGroupByHostID_Internal != null)
				{
					p2PGroupByHostID_Internal.members.Remove(memberHostID);
				}
				if (peerByHostID != null)
				{
					peerByHostID.joinedP2PGroups.Remove(groupHostID);
					RemoveRemotePeerIfNoGroupRelationDetected(peerByHostID);
					peerByHostID.leaveEventCount++;
				}
				if (memberHostID == localHostID)
				{
					p2pGroups.Remove(groupHostID);
				}
				MemberLocalEvent memberLocalEvent = new MemberLocalEvent();
				memberLocalEvent.type = LocalEventType.DelMember;
				memberLocalEvent.memberHostID = memberHostID;
				memberLocalEvent.groupHostID = groupHostID;
				if (p2PGroupByHostID_Internal != null)
				{
					memberLocalEvent.memberCount = p2PGroupByHostID_Internal.members.Count;
				}
				else
				{
					memberLocalEvent.memberCount = 0;
				}
				EnqueLocalEvent(memberLocalEvent);
				return true;
			}
		}

		private bool NotifyDirectP2PEstablish(HostID remote, RmiContext rmiContext, HostID A0, HostID B0, IPEndPoint X0, IPEndPoint Y0, IPEndPoint Z0, IPEndPoint W0)
		{
			HostID lhs = A0;
			HostID rhs = B0;
			IPEndPoint lhs2 = X0;
			IPEndPoint rhs2 = Y0;
			IPEndPoint rhs3 = Z0;
			IPEndPoint lhs3 = W0;
			lock (m_critSec)
			{
				if (localHostID == rhs)
				{
					Sysutil.Swap(ref lhs, ref rhs);
					Sysutil.Swap(ref lhs2, ref rhs3);
					Sysutil.Swap(ref lhs3, ref rhs2);
				}
				RemotePeer peerByHostID = GetPeerByHostID(rhs);
				if (peerByHostID == null || peerByHostID.garbaged)
				{
					return true;
				}
				peerByHostID.p2pHolepunchedLocalToRemoteAddr = lhs2;
				peerByHostID.p2pHolepunchedRemoteToLocalAddr = lhs3;
				peerByHostID.RelayedP2P = false;
				peerByHostID.p2pConnectionTrialContext = null;
				EnqueLocalEvent(new LocalEvent
				{
					type = LocalEventType.DirectP2PEnabled,
					errorInfo = new ErrorInfo
					{
						remote = rhs
					}
				});
			}
			return true;
		}

		private bool ReliablePong(HostID remote, RmiContext rmiContext, int localTimeMs)
		{
			int num = (serverTcpLastPingMs = ((int)PreciseCurrentTime.GetTimeMs() - localTimeMs) / 2);
			if (serverTcpRecentPingMs == 0)
			{
				serverTcpRecentPingMs = num;
			}
			else
			{
				serverTcpRecentPingMs = (int)Sysutil.LerpInt(serverTcpRecentPingMs, num, NetConfig.LagLinearProgrammingFactorPercent, 100L);
			}
			return true;
		}

		private bool EnableLog(HostID remote, RmiContext rmiContext)
		{
			lock (m_critSec)
			{
				enableLog = true;
				return true;
			}
		}

		private bool DisableLog(HostID remote, RmiContext rmiContext)
		{
			lock (m_critSec)
			{
				enableLog = false;
				return true;
			}
		}

		private bool NotifyUdpToTcpFallbackByServer(HostID remote, RmiContext rmiContext)
		{
			lock (m_critSec)
			{
				FallbackServerUdpToTcpOnNeed();
				return true;
			}
		}

		private bool NotifySpeedHackDetectorEnabled(HostID remote, RmiContext rmiContext, bool enable)
		{
			lock (m_critSec)
			{
				if (enable)
				{
					if (speedHackDetectorPingCooltimeMs == NetConfig.InfiniteCoolTimeMs)
					{
						speedHackDetectorPingCooltimeMs = 0L;
					}
				}
				else
				{
					speedHackDetectorPingCooltimeMs = NetConfig.InfiniteCoolTimeMs;
				}
				return true;
			}
		}

		private bool ShutdownTcpAck(HostID remote, RmiContext rmiContext)
		{
			lock (m_critSec)
			{
				if (((m_useNetworkerThread && workerThread != null) || (!m_useNetworkerThread && workerThread == null)) && shutdownIssuedTimeMs > 0 && gracefulDisconnectTimeoutMs != 0)
				{
					shutdownIssuedTimeMs = PreciseCurrentTime.GetTimeMs();
				}
				c2sProxy.ShutdownTcpHandshake(HostID.Server, RmiContext.ReliableSendForPN);
				return true;
			}
		}

		private bool RequestAutoPrune(HostID remote, RmiContext rmiContext)
		{
			lock (m_critSec)
			{
				if ((!m_useNetworkerThread || workerThread != null) && State <= WorkerState.Connected)
				{
					EnqueueDisconnectionEvent(ErrorType.DisconnectFromRemote, ErrorType.TCPConnectFailure, "Autoprune");
					State = WorkerState.Disconnecting;
				}
			}
			return true;
		}

		private bool RenewP2PConnectionState(HostID remote, RmiContext rmiContext, HostID remotePeerID)
		{
			lock (m_critSec)
			{
				RemotePeer peerByHostID = GetPeerByHostID(remotePeerID);
				if (peerByHostID != null)
				{
					peerByHostID.RelayedP2P = true;
					peerByHostID.repunchCount = 0;
					peerByHostID.repunchStartTime = PreciseCurrentTime.GetTimeMs();
					peerByHostID.m_restoreNeeded = false;
					peerByHostID.CreateP2PConnectionTrialContext();
					if (enableLog || settings.emergencyLogLineCount > 0)
					{
						Log(TraceID.Holepunch, string.Format("Perpectly reset p2p connection Client {0}.", peerByHostID.peerHostID));
					}
				}
				return true;
			}
		}

		private bool NewDirectP2PConnection(HostID remote, RmiContext rmiContext, HostID remotePeerID)
		{
			lock (m_critSec)
			{
				RemotePeer peerByHostID = GetPeerByHostID(remotePeerID);
				if (peerByHostID != null && peerByHostID.udpSocket == null)
				{
					peerByHostID.m_newP2PConnectionNeeded = true;
					if (enableLog || settings.emergencyLogLineCount > 0)
					{
						Log(TraceID.Holepunch, string.Format("Request p2p connection to Client {0}.", peerByHostID.peerHostID));
					}
				}
				return true;
			}
		}

		private bool RequestMeasureSendSpeed(HostID remote, RmiContext rmiContext, bool enable)
		{
			return true;
		}

		private bool S2C_RequestCreateUdpSocket(HostID remote, RmiContext rmiContext, NamedAddrPort serverudpaddr)
		{
			lock (m_critSec)
			{
				bool flag = New_ToServerUdpSocket();
				if (flag)
				{
					IPEndPoint serverAddr = new IPEndPoint(IPAddress.Parse(serverudpaddr.addr), serverudpaddr.port);
					toServerUdp_fallbackable.serverAddr = serverAddr;
				}
				c2sProxy.C2S_CreateUdpSocketAck(HostID.Server, RmiContext.ReliableSendForPN, flag);
				return true;
			}
		}

		private bool S2C_CreateUdpSocketAck(HostID remote, RmiContext rmiContext, bool succeed, NamedAddrPort serverudpaddr)
		{
			lock (m_critSec)
			{
				if (succeed && New_ToServerUdpSocket())
				{
					IPEndPoint serverAddr = new IPEndPoint(IPAddress.Parse(serverudpaddr.addr), serverudpaddr.port);
					toServerUdp_fallbackable.serverAddr = serverAddr;
				}
				toServerUdp_fallbackable.serverUdpReadyWaiting = false;
				return true;
			}
		}

		private bool HolsterP2PHolepunchTrial(HostID remote, RmiContext rmiContext)
		{
			lock (m_critSec)
			{
				RemotePeer peerByHostID = GetPeerByHostID(remote);
				if (peerByHostID != null && !peerByHostID.garbaged)
				{
					peerByHostID.p2pConnectionTrialContext = null;
				}
				return true;
			}
		}

		private bool ReportUdpMessageCount(HostID remote, RmiContext rmiContext, int udpSuccessCount)
		{
			lock (m_critSec)
			{
				RemotePeer peerByHostID = GetPeerByHostID(remote);
				if (peerByHostID != null && !peerByHostID.garbaged)
				{
					peerByHostID.toRemotePeerSendUdpMessageSuccessCount = udpSuccessCount;
					c2sProxy.ReportC2CUdpMessageCount(HostID.Server, RmiContext.ReliableSendForPN, peerByHostID.peerHostID, peerByHostID.toRemotePeerSendUdpMessageTrialCount, peerByHostID.toRemotePeerSendUdpMessageSuccessCount);
				}
				return true;
			}
		}

		private bool ReportServerTimeAndFrameRateAndPing(HostID remote, RmiContext rmiContext, long clientLocalTime, double recentFrameRate)
		{
			lock (m_critSec)
			{
				RemotePeer peerByHostID = GetPeerByHostID(remote);
				if (peerByHostID != null && !peerByHostID.garbaged)
				{
					peerByHostID.recentFrameRate = recentFrameRate;
					int outputPercent = 0;
					GetUnreliableMessagingLossRatioPercent(HostID.Server, out outputPercent);
					c2cProxy.ReportServerTimeAndFrameRateAndPong(remote, RmiContext.ReliableSendForPN, clientLocalTime, ServerTimeMs, serverUdpRecentPingMs, ApplicationHint.recentFrameRate, outputPercent);
				}
				return true;
			}
		}

		private bool ReportServerTimeAndFrameRateAndPong(HostID remote, RmiContext rmiContext, long clientOldLocalTime, long serverLocalTime, int serverUdpRecentPing, double recentFrameRate, int packetLossPercent)
		{
			lock (m_critSec)
			{
				RemotePeer peerByHostID = GetPeerByHostID(remote);
				if (peerByHostID != null && !peerByHostID.garbaged)
				{
					int peerToServerPingMs = Math.Max(serverUdpRecentPing, 0);
					peerByHostID.peerToServerPingMs = peerToServerPingMs;
					peerByHostID.recentFrameRate = recentFrameRate;
					peerByHostID.CSPacketLossPercent = packetLossPercent;
					long timeMs = PreciseCurrentTime.GetTimeMs();
					long num = serverLocalTime + peerByHostID.recentPingMs;
					peerByHostID.indirectServerTimeDiffMs = timeMs - num;
				}
				return true;
			}
		}

		private void InitWorker()
		{
			shutdownIssuedTimeMs = 0L;
			disconnectingModeHeartbeatCount = 0;
			disconnectingModeStartTimeMs = 0L;
			disconnectingModeWarned = false;
			m_state_USE_FUNC = WorkerState.Disconnected;
			tcpRecvAsyncCallback = ToServerTcp_ReceiveCallback;
			tcpSendAsyncCallback = ToServerTcp_SendCallback;
			udpRecvAsyncCallback = UdpReceiveCallback;
			udpSendAsyncCallback = UdpSendCallback;
			main_IssueConnectHandler = Main_IssueConnect;
		}

		internal void WorkerProc()
		{
			while (!stopworkerthread)
			{
				Heartbeat_Work();
				int millisecondsTimeout = (int)Math.Max(1u, NetConfig.ClientHeartbeatIntervalMs);
				Thread.Sleep(millisecondsTimeout);
			}
		}

		internal void Heartbeat_Work()
		{
			lock (m_critSec)
			{
				if (!m_useNetworkerThread)
				{
					if (PreciseCurrentTime.GetTimeMs() - lastHeartbeatTimeMs > NetConfig.ClientHeartbeatIntervalMs)
					{
						Heartbeat();
						lastHeartbeatTimeMs = PreciseCurrentTime.GetTimeMs();
					}
				}
				else
				{
					Heartbeat();
				}
			}
		}

		private void Heartbeat_ConnectFailCase(SocketError code)
		{
			toServerTcpLayer.socket.Blocking = true;
			EnqueueConnectFailEvent(ErrorType.TCPConnectFailure, code, null, null);
			State = WorkerState.Disconnecting;
		}

		private bool Main_IssueConnect(ref SocketError outCode)
		{
			outCode = SocketError.Success;
			try
			{
				toServerTcpLayer.socket.Blocking = false;
				toServerTcpLayer.socket.Connect(connectionParam.serverIP, connectionParam.serverPort);
			}
			catch (SocketException ex)
			{
				if (ex.SocketErrorCode == SocketError.WouldBlock)
				{
					return true;
				}
				outCode = ex.SocketErrorCode;
				return false;
			}
			return true;
		}

		private void IssueTcpFirstRecv()
		{
			lock (m_critSec)
			{
				SocketError socketError = toServerTcpLayer.IssueRecvAndCheck(tcpRecvAsyncCallback);
				if (socketError != SocketError.Success)
				{
					EnqueueConnectFailEvent(ErrorType.TCPConnectFailure, socketError, null, null);
					State = WorkerState.Disconnecting;
				}
			}
		}

		private void Heartbeat_Disconnecting()
		{
			lock (m_critSec)
			{
				disconnectingModeHeartbeatCount++;
				if (disconnectingModeHeartbeatCount == 1)
				{
					if (toServerUdpSocket != null)
					{
						toServerUdpSocket.CloseSocketOnly();
					}
					List<HostID> list = new List<HostID>(remotePeers.Keys);
					foreach (HostID item in list)
					{
						RemovePeer(remotePeers[item]);
					}
					AllClearRecycleToGarbage();
					peerGarbages.Clear();
					if (ToServerTcp != null)
					{
						ToServerTcp.CloseSocket();
					}
				}
				DoGarbageCollect();
				int num = 0;
				bool flag = true;
				if (garbages.Count > 0)
				{
					num = 1;
					flag = false;
				}
				else if (toServerUdpSocket != null && toServerUdpSocket.socket != null && (!toServerUdpSocket.IsSocketClosed() || toServerUdpSocket.sendIssued || toServerUdpSocket.recvIssued))
				{
					num = 2;
					flag = false;
				}
				else if (ToServerTcp != null && ToServerTcp.socket != null && (!ToServerTcp.IsSocketClosed || ToServerTcp.sendIssued || ToServerTcp.recvIssued))
				{
					num = 3;
					flag = false;
				}
				if (flag)
				{
					CleanupEvenUnstableSituation(false);
					State = WorkerState.Disconnected;
				}
				if (!flag && PreciseCurrentTime.GetTimeMs() - disconnectingModeStartTimeMs > 5000 && !disconnectingModeWarned)
				{
					if (enableLog || settings.emergencyLogLineCount > 0)
					{
						Log(TraceID.System, string.Format("Too long time elapsed since disconnecting mode! unsafeDisconnectReason={0}", num));
					}
					disconnectingModeWarned = true;
				}
			}
		}

		private void Heartbeat_Connected()
		{
			lock (m_critSec)
			{
				Heartbeat_Connected_AfterLock();
			}
		}

		private void Heartbeat_Connected_AfterLock()
		{
			DoGarbageCollect();
			Heartbeat_ConnectedCase();
			Heartbeat_DetectNatDeviceName();
			while (true)
			{
				bool flag = false;
				long timeMs = PreciseCurrentTime.GetTimeMs();
				if (shutdownIssuedTimeMs > 0 && timeMs - shutdownIssuedTimeMs > gracefulDisconnectTimeoutMs)
				{
					break;
				}
				flag |= LoopbackRecvCompletionCase();
				if (State == WorkerState.Disconnecting || !flag)
				{
					return;
				}
			}
			State = WorkerState.Disconnecting;
		}

		private void Heartbeat_DetectNatDeviceName()
		{
		}

		private void Heartbeat_JustConnected()
		{
			toServerUdp_fallbackable.RealUdpEnabled = false;
			Message message = new Message();
			message.Write(MessageType.RequestServerConnectionHint);
			message.Write((int)platformType);
			ToServerTcp.AddToSendQueueWithSplitterAndSignal_Copy(new SendFragRefs(message), new SendOpt());
			State = WorkerState.Connected;
			if (IsProactorAsyncModel)
			{
				IssueTcpFirstRecv();
			}
			DoGarbageCollect();
		}

		private void Heartbeat_Connecting()
		{
			long timeMs = PreciseCurrentTime.GetTimeMs();
			if (timeMs - issueConnectStartTimeMs > NetConfig.TcpSocketConnectTimeoutMs)
			{
				Heartbeat_ConnectFailCase(SocketError.TimedOut);
				return;
			}
			if (connectIssueResult != null && connectIssueResult.IsCompleted)
			{
				SocketError outCode = SocketError.Success;
				bool flag = main_IssueConnectHandler.EndInvoke(ref outCode, connectIssueResult);
				connectIssueResult = null;
				if (!flag)
				{
					Heartbeat_ConnectFailCase(outCode);
				}
			}
			if (!ToServerTcp.socket.Connected)
			{
				return;
			}
			if (IsProactorAsyncModel)
			{
				ToServerTcp.socket.Blocking = true;
			}
			ToServerTcp.RefreshLocalAddr();
			if (!NetUtil.IsUnicastEndpoint(ToServerTcp.localAddr))
			{
				Sysutil.ShowUserMisuseError("서버와 TCP 연결이 되었는데도 로컬 소켓 주소를 얻을 수 없다니!");
			}
			if (connectIssueResult != null && connectIssueResult.IsCompleted)
			{
				SocketError outCode2 = SocketError.Success;
				bool flag2 = main_IssueConnectHandler.EndInvoke(ref outCode2, connectIssueResult);
				connectIssueResult = null;
				if (!flag2)
				{
					Heartbeat_ConnectFailCase(outCode2);
				}
			}
			State = WorkerState.JustConnected;
		}

		private void Heartbeat_IssueConnect()
		{
			if (enableLog || settings.emergencyLogLineCount > 0)
			{
				Log(TraceID.System, "클라이언트 NetWorker thread 시작");
			}
			try
			{
				if (!isIpV6Network)
				{
					ToServerTcp.socket.Bind(new IPEndPoint(IPAddress.Any, 0));
				}
				else
				{
					ToServerTcp.socket.Bind(new IPEndPoint(IPAddress.IPv6Any, 0));
				}
			}
			catch (SocketException ex)
			{
				EnqueueConnectFailEvent(ErrorType.TCPConnectFailure, ex.SocketErrorCode, ex.ToString() + "Cannot bind TCP socket to a local address!", null);
				Heartbeat_ConnectFailCase(ex.SocketErrorCode);
				return;
			}
			ToServerTcp.RefreshLocalAddr();
			issueConnectStartTimeMs = PreciseCurrentTime.GetTimeMs();
			SocketError outCode = SocketError.Success;
			connectIssueResult = main_IssueConnectHandler.BeginInvoke(ref outCode, null, null);
			State = WorkerState.Connecting;
		}

		private void Heartbeat()
		{
			switch (State)
			{
			case WorkerState.IssueConnect:
				Heartbeat_IssueConnect();
				break;
			case WorkerState.Connecting:
				Heartbeat_Connecting();
				break;
			case WorkerState.JustConnected:
				Heartbeat_JustConnected();
				break;
			case WorkerState.Connected:
				Heartbeat_Connected();
				break;
			case WorkerState.Disconnecting:
				Heartbeat_Disconnecting();
				break;
			}
			TcpAndUdp_DoForLongInterval();
			if (processSendReadyRemotes_Timer.IsTimeToDo(PreciseCurrentTime.GetTimeMs()))
			{
				EveryRemote_IssueSendOnNeed();
			}
			if (!IsProactorAsyncModel && processRecvReadyRemotes_Timer.IsTimeToDo(PreciseCurrentTime.GetTimeMs()))
			{
				EveryRemote_NonBlockRecvUntilWouldBlock();
			}
		}

		private void Heartbeat_ConnectedCase()
		{
			lock (m_critSec)
			{
				if (lastFrameMoveInvokedTimeMs > 0 && PreciseCurrentTime.GetTimeMs() - lastFrameMoveInvokedTimeMs > 30000)
				{
					if (enableLog || settings.emergencyLogLineCount > 0)
					{
						Log(TraceID.System, string.Format("**WARNING** HostID : {0} NetClient.FrameMove is not called in thirty seconds. Is this your intention?", (int)LocalHostID));
					}
					lastFrameMoveInvokedTimeMs = -1L;
				}
				if (PreciseCurrentTime.GetTimeMs() - lastTcpStreamReceivedTimeMs > settings.defaultTimeoutTimeMs)
				{
					if (enableLog || settings.emergencyLogLineCount > 0)
					{
						Log(TraceID.System, string.Format("default timeouttime:{0},Client {1}, CachedTimeMs {2}: 오랫동안 TCP 수신이 없어서 연결해제 모드로 전환합니다.", settings.defaultTimeoutTimeMs, localHostID, PreciseCurrentTime.GetTimeMs()));
					}
					EnqueueDisconnectionEvent(ErrorType.DisconnectFromLocal, ErrorType.ConnectServerTimeout, lastTcpStreamReceivedTimeMs.ToString());
					State = WorkerState.Disconnecting;
				}
				else
				{
					Heartbeat_EveryRemotePeer();
					SendServerHolePunchOnNeed();
					RequestServerTimeOnNeed();
					SpeedHackPingOnNeed();
					P2PPingOnNeed();
					FallbackServerUdpToTcpOnNeed();
					ReportP2PPeerPingOnNeed();
					ReportRealUdpCount();
					CheckSendQueue();
				}
			}
		}

		private void Heartbeat_EveryRemotePeer()
		{
			long timeMs = PreciseCurrentTime.GetTimeMs();
			int num = 0;
			foreach (RemotePeer value in remotePeers.Values)
			{
				if (!value.garbaged)
				{
					value.Heartbeat(timeMs);
					if (value.p2pConnectionTrialContext != null)
					{
						num++;
					}
				}
			}
			int num2 = Math.Max(1, num);
			p2pHolepunchIntervalMs = NetConfig.P2PHolepunchIntervalMs * num2;
			p2pConnectionTrialEndTimeMs = NetConfig.P2PHolepunchEndTimeMs * num2;
		}

		private bool LoopbackRecvCompletionCase()
		{
			lock (m_critSec)
			{
				if (loopbackFinalReceivedMessageQueue.Count > 0)
				{
					ReceivedMessage receivedMessage = new ReceivedMessage();
					receivedMessage.remoteHostID = localHostID;
					receivedMessage.unsafeMessage = loopbackFinalReceivedMessageQueue.Dequeue();
					receivedMessage.unsafeMessage.ReadOffset = 0;
					receivedMessage.relayed = false;
					preFinalRecvQueue.Enqueue(receivedMessage);
					ProcessEveryMessageOrMoveToFinalRecvQueue(null);
					return true;
				}
				return false;
			}
		}
	}
}

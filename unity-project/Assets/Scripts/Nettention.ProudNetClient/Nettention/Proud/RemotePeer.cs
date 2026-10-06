using System;
using System.Net;

namespace Nettention.Proud
{
	internal class RemotePeer : IP2PGroupMember, ISendDest_C
	{
		internal class UdpLayer
		{
			private RemotePeer owner;

			public uint UdpSendBufferPacketFilledCount
			{
				get
				{
					if (owner.udpSocket == null)
					{
						return 0u;
					}
					return owner.ToPeerUdpSocket.GetUdpSendBufferPacketFilledCount(owner.p2pHolepunchedLocalToRemoteAddr);
				}
			}

			public UdpLayer(RemotePeer owner)
			{
				this.owner = owner;
			}

			public void SendWithSplitter_Copy(SendFragRefs sendData, SendOpt sendOpt)
			{
				if (owner.udpSocket != null)
				{
					owner.ToPeerUdpSocket.AddToSendQueueWithSplitterAndSignal_Copy(owner.peerHostID, FilterTag.CreateFilterTag(owner.owner.LocalHostID, owner.peerHostID), owner.p2pHolepunchedLocalToRemoteAddr, sendData, PreciseCurrentTime.GetTimeMs(), sendOpt);
				}
			}
		}

		internal NetClient owner;

		internal UdpSocket_C udpSocket;

		internal bool m_forceRelayP2P;

		internal bool m_restoreNeeded;

		internal bool m_jitDirectP2PNeeded;

		internal bool m_jitDirectP2PTriggered;

		internal bool m_newP2PConnectionNeeded;

		internal bool m_memberJoinProcessEnd;

		internal bool m_allowDirectP2P;

		internal P2PConnectionTrialContext p2pConnectionTrialContext;

		internal Guid magicNumber;

		internal int toRemotePeerSendUdpMessageTrialCount;

		internal int toRemotePeerSendUdpMessageSuccessCount;

		internal int receiveudpMessageSuccessCount;

		internal SessionKey p2pSessionKey = new SessionKey();

		internal ushort encryptCount;

		internal ushort decryptCount;

		internal int recentPingMs;

		internal int recentReliablePingMs;

		internal uint sendQueuedAmountInBytes;

		internal int peerToServerPingMs;

		internal long lastPingSendTimeMs;

		internal int CSPacketLossPercent;

		internal long udpSocketCreationTimeMs;

		internal long lastDirectUdpPacketReceivedTimeMs;

		internal volatile int directUdpPacketReceiveCount;

		internal long lastUdpPacketReceivedIntervalMs;

		public long m_ReliablePingDiffCoolTime;

		public long m_UnreliablePingDiffCoolTime;

		internal long indirectServerTimeDiffMs;

		internal int lastPingMs;

		internal int lastReliablePingMs;

		internal double recentFrameRate;

		private HostID hostID;

		internal IPEndPoint udpAddrFromServer;

		internal IPEndPoint udpAddrInternal;

		internal IPEndPoint p2pHolepunchedLocalToRemoteAddr;

		internal IPEndPoint p2pHolepunchedRemoteToLocalAddr;

		internal P2PGroups_C joinedP2PGroups = new P2PGroups_C();

		internal bool relayedP2P_USE_FUNCTION = true;

		internal long relayedP2PDisabledTimeMs;

		internal bool setToRelayedButLastPingIsNotCalulcatedYet;

		internal int repunchCount;

		internal long repunchStartTime;

		internal long lastCheckSendQueueTimeMs;

		internal long sendQueueHeavyStartTimeMs;

		internal RemotePeerReliableUdp toPeerReliableUdp;

		internal long toPeerReliableUdpHeartbeatLastTimeMs;

		internal long toPeerReportServerTimeAndPingLastTimeMs;

		private UdpLayer toPeerUdp;

		internal object hostTag;

		internal int leaveEventCount;

		internal bool garbaged;

		internal long RenewalSocketCreationTimeMs
		{
			get
			{
				lock (owner.m_critSec)
				{
					return PreciseCurrentTime.GetTimeMs() + owner.random.Next(2000) + 1000;
				}
			}
		}

		internal bool IsRelayConditionByReliableUdpFailure
		{
			get
			{
				if (!RelayedP2P && toPeerReliableUdp.host != null)
				{
					return toPeerReliableUdp.host.sender_INTERNAL.maxResendElapsedTimeMs > owner.settings.defaultTimeoutTimeMs;
				}
				return false;
			}
		}

		public bool RelayedP2P
		{
			get
			{
				return relayedP2P_USE_FUNCTION;
			}
			set
			{
				if (value)
				{
					if (!relayedP2P_USE_FUNCTION)
					{
						setToRelayedButLastPingIsNotCalulcatedYet = true;
					}
					relayedP2P_USE_FUNCTION = true;
				}
				else
				{
					relayedP2P_USE_FUNCTION = false;
					relayedP2PDisabledTimeMs = PreciseCurrentTime.GetTimeMs();
					lastDirectUdpPacketReceivedTimeMs = PreciseCurrentTime.GetTimeMs();
					directUdpPacketReceiveCount = 0;
					lastUdpPacketReceivedIntervalMs = -1L;
				}
				if (toPeerReliableUdp.host != null)
				{
					toPeerReliableUdp.host.sender_INTERNAL.maxResendElapsedTimeMs = 0L;
				}
			}
		}

		public bool IsBehindNat
		{
			get
			{
				return !udpAddrInternal.Address.Equals(udpAddrFromServer.Address);
			}
		}

		internal long IndirectServerTimeDiffMs
		{
			get
			{
				return indirectServerTimeDiffMs;
			}
		}

		long IP2PGroupMember.IndirectServerTimeDiffMs
		{
			get
			{
				return indirectServerTimeDiffMs;
			}
		}

		HostID IP2PGroupMember.MemberHostID
		{
			get
			{
				return peerHostID;
			}
		}

		HostID ISendDest_C.SendDestHostID
		{
			get
			{
				return peerHostID;
			}
		}

		internal UdpSocket_C ToPeerUdpSocket
		{
			get
			{
				return udpSocket;
			}
		}

		internal UdpLayer ToPeerUdp
		{
			get
			{
				return toPeerUdp;
			}
		}

		internal HostID peerHostID
		{
			get
			{
				return hostID;
			}
			set
			{
				hostID = value;
			}
		}

		internal bool IsSameLanToLocal
		{
			get
			{
				if (udpSocket != null && NetUtil.IsSameHost(owner.ToServerUdpSocketAddrAtServer, udpAddrFromServer))
				{
					return NetUtil.IsSameLan(owner.ToServerUdpSocketLocalAddr, udpAddrInternal);
				}
				return false;
			}
		}

		internal bool IsRelayConditionByUdpFailure(long currTime)
		{
			if (!RelayedP2P)
			{
				return currTime - lastDirectUdpPacketReceivedTimeMs > NetConfig.FallbackP2PUdpToTcpTimeoutMs;
			}
			return false;
		}

		public bool IsRelayMuchFasterThanDirectP2P(int serverUdpRecentPingMs, double forceRelayThresholdRatio)
		{
			if (forceRelayThresholdRatio <= 0.0)
			{
				return false;
			}
			if (serverUdpRecentPingMs <= 0 || peerToServerPingMs <= 0 || recentPingMs <= 0)
			{
				return false;
			}
			if (recentPingMs <= 20)
			{
				return false;
			}
			int num = serverUdpRecentPingMs + peerToServerPingMs;
			return (double)num * forceRelayThresholdRatio < (double)recentPingMs;
		}

		public RemotePeer(NetClient owner)
		{
			toPeerReliableUdp = new RemotePeerReliableUdp(this);
			toPeerUdp = new UdpLayer(this);
			InitGarbage(owner);
		}

		public void InitGarbage(NetClient owner)
		{
			m_allowDirectP2P = P2PGroupOption.Default.enableDirectP2P;
			m_jitDirectP2PNeeded = owner.settings.directP2PStartCondition == DirectP2PStartCondition.Always;
			m_jitDirectP2PTriggered = false;
			m_memberJoinProcessEnd = false;
			p2pConnectionTrialContext = null;
			m_forceRelayP2P = false;
			m_restoreNeeded = false;
			m_newP2PConnectionNeeded = false;
			lastPingMs = 0;
			lastReliablePingMs = 0;
			recentFrameRate = 0.0;
			peerToServerPingMs = 0;
			p2pHolepunchedLocalToRemoteAddr = new IPEndPoint(0L, 0);
			p2pHolepunchedRemoteToLocalAddr = new IPEndPoint(0L, 0);
			udpAddrFromServer = new IPEndPoint(0L, 0);
			udpAddrInternal = new IPEndPoint(0L, 0);
			this.owner = owner;
			relayedP2P_USE_FUNCTION = true;
			relayedP2PDisabledTimeMs = 0L;
			m_ReliablePingDiffCoolTime = Sysutil.LerpInt(NetConfig.ReliablePingIntervalMs / 2, NetConfig.ReliablePingIntervalMs, owner.random.Next(256), 256L);
			m_UnreliablePingDiffCoolTime = Sysutil.LerpInt(NetConfig.UnreliablePingIntervalMs / 2, NetConfig.UnreliablePingIntervalMs, owner.random.Next(256), 256L);
			indirectServerTimeDiffMs = 0L;
			lastDirectUdpPacketReceivedTimeMs = PreciseCurrentTime.GetTimeMs();
			directUdpPacketReceiveCount = 0;
			lastUdpPacketReceivedIntervalMs = -1L;
			recentPingMs = 0;
			sendQueuedAmountInBytes = 0u;
			lastPingSendTimeMs = 0L;
			repunchCount = 0;
			repunchStartTime = 0L;
			toRemotePeerSendUdpMessageTrialCount = 0;
			toRemotePeerSendUdpMessageSuccessCount = 0;
			receiveudpMessageSuccessCount = 0;
			lastCheckSendQueueTimeMs = 0L;
			sendQueueHeavyStartTimeMs = 0L;
			hostTag = null;
			udpSocketCreationTimeMs = RenewalSocketCreationTimeMs;
			toPeerReliableUdpHeartbeatLastTimeMs = 0L;
			toPeerReportServerTimeAndPingLastTimeMs = 0L;
			setToRelayedButLastPingIsNotCalulcatedYet = true;
		}

		internal NetPeerInfo ToNetPeerInfo()
		{
			NetPeerInfo netPeerInfo = new NetPeerInfo();
			netPeerInfo.hostID = peerHostID;
			netPeerInfo.udpAddrFromServer = new IPEndPoint(udpAddrFromServer.Address, udpAddrFromServer.Port);
			netPeerInfo.udpAddrInternal = new IPEndPoint(udpAddrInternal.Address, udpAddrInternal.Port);
			netPeerInfo.recentPingMs = recentPingMs;
			netPeerInfo.sendQueuedAmountInBytes = sendQueuedAmountInBytes;
			foreach (HostID key in joinedP2PGroups.Keys)
			{
				netPeerInfo.joinedP2PGroups.Add(key);
			}
			netPeerInfo.relayedP2P = RelayedP2P;
			netPeerInfo.isBehindNat = IsBehindNat;
			netPeerInfo.hostTag = hostTag;
			netPeerInfo.directP2PPeerFrameRate = recentFrameRate;
			netPeerInfo.toRemotePeerSendUdpMessageTrialCount = toRemotePeerSendUdpMessageTrialCount;
			netPeerInfo.toRemotePeerSendUdpMessageSuccessCount = toRemotePeerSendUdpMessageSuccessCount;
			return netPeerInfo;
		}

		internal void CreateP2PConnectionTrialContext()
		{
			p2pConnectionTrialContext = new P2PConnectionTrialContext(this);
		}

		internal void Heartbeat(long currTime)
		{
			if (currTime - toPeerReliableUdpHeartbeatLastTimeMs > NetConfig.ReliableUdpHeartbeatIntervalMs)
			{
				toPeerReliableUdp.Heartbeat();
				toPeerReliableUdpHeartbeatLastTimeMs = currTime;
			}
			if (currTime - toPeerReportServerTimeAndPingLastTimeMs > NetConfig.ReportServerTimeAndPingIntervalMs)
			{
				ApplicationHint applicationHint = owner.ApplicationHint;
				owner.c2cProxy.ReportServerTimeAndFrameRateAndPing(peerHostID, RmiContext.ReliableSendForPN, PreciseCurrentTime.GetTimeMs(), (float)applicationHint.recentFrameRate);
				toPeerReportServerTimeAndPingLastTimeMs = currTime;
			}
			if (owner.IsWebPlayer)
			{
				return;
			}
			if (owner.settings.fallbackMethod != FallbackMethod.PeersUdpToTcp && owner.settings.fallbackMethod != FallbackMethod.CloseUdpSocket && owner.settings.fallbackMethod != FallbackMethod.ServerUdpToTcp && m_allowDirectP2P && m_memberJoinProcessEnd)
			{
				if (udpSocket == null && PreciseCurrentTime.GetTimeMs() > udpSocketCreationTimeMs && m_jitDirectP2PNeeded && !m_jitDirectP2PTriggered)
				{
					m_jitDirectP2PTriggered = true;
					owner.c2sProxy.NotifyJitDirectP2PTriggered(HostID.Server, RmiContext.ReliableSendForPN, peerHostID);
				}
				RestoreUdpSocketOnNeed();
				NewUdpSocketOnNeed();
			}
			if (p2pConnectionTrialContext != null && !p2pConnectionTrialContext.Heartbeat())
			{
				p2pConnectionTrialContext = null;
			}
			if (IsRelayConditionByUdpFailure(currTime))
			{
				FallbackP2PToRelay(true, ErrorType.P2PUdpFailed);
			}
			else if (IsRelayConditionByReliableUdpFailure)
			{
				FallbackP2PToRelay(true, ErrorType.ReliableUdpFailed);
			}
			if (RelayedP2P && repunchStartTime > 0 && currTime > repunchStartTime && udpSocket != null && !ToPeerUdpSocket.IsSocketClosed())
			{
				repunchStartTime = 0L;
				CreateP2PConnectionTrialContext();
			}
			if (udpSocket == null || currTime - lastCheckSendQueueTimeMs <= NetConfig.SendQueueHeavyWarningCheckCoolTimeMs)
			{
				return;
			}
			int num = ToPeerUdpSocket.udpPacketFragBoard.FromTotalPacketInBytesByAddr(p2pHolepunchedLocalToRemoteAddr);
			if (sendQueueHeavyStartTimeMs != 0)
			{
				if (num > NetConfig.SendQueueHeavyWarningCapacity)
				{
					if (currTime - sendQueueHeavyStartTimeMs > NetConfig.SendQueueHeavyWarningTimeMs)
					{
						sendQueueHeavyStartTimeMs = currTime;
						owner.EnqueWarning(ErrorInfo.From(ErrorType.SendQueueIsHeavy, peerHostID, string.Format("sendQueue {0}Bytes", num)));
					}
				}
				else
				{
					sendQueueHeavyStartTimeMs = 0L;
				}
			}
			else if (num > NetConfig.SendQueueHeavyWarningCapacity)
			{
				sendQueueHeavyStartTimeMs = currTime;
			}
			lastCheckSendQueueTimeMs = currTime;
		}

		internal void RestoreUdpSocketOnNeed()
		{
			if (udpSocket == null || ToPeerUdpSocket.socket == null || !m_restoreNeeded)
			{
				return;
			}
			if (ToPeerUdpSocket.IsSocketClosed())
			{
				if (!ToPeerUdpSocket.RestoreSocket(owner))
				{
					return;
				}
				if (owner.IsProactorAsyncModel)
				{
					ToPeerUdpSocket.IssueRecvFrom_UnlessSocketClosed(PreciseCurrentTime.GetTimeMs(), owner.udpRecvAsyncCallback);
				}
				owner.c2sProxy.NotifyPeerUdpSocketRestored(HostID.Server, RmiContext.ReliableSendForPN, peerHostID);
				if (owner.enableLog || owner.settings.emergencyLogLineCount > 0)
				{
					owner.Log(TraceID.Holepunch, string.Format("After restored Udp socket for Client {0}, wait to server reset order.", peerHostID));
				}
			}
			m_restoreNeeded = false;
		}

		internal void NewUdpSocketOnNeed()
		{
			if (udpSocket == null && m_newP2PConnectionNeeded && owner.toServerUdpSocket != null)
			{
				m_newP2PConnectionNeeded = false;
				long timeMs = PreciseCurrentTime.GetTimeMs();
				udpSocket = new UdpSocket_C(owner, timeMs, RequestReceiveSpeedAtReceiverSide_NoRelay);
				IPEndPoint iPEndPoint = new IPEndPoint(owner.ToServerTcp.localAddr.Address, 0);
				uint num = BitConverter.ToUInt32(iPEndPoint.Address.GetAddressBytes(), 0);
				if (num == 0 || num == uint.MaxValue)
				{
					Sysutil.ShowUserMisuseError("UDP 소켓을 생성하기 전에 TCP 연결이 이미 되어있는 상태이어야 하는데!");
				}
				ToPeerUdpSocket.CreateSocket(owner.IsProactorAsyncModel, iPEndPoint);
				if (owner.IsProactorAsyncModel)
				{
					ToPeerUdpSocket.IssueRecvFrom_UnlessSocketClosed(timeMs, owner.udpRecvAsyncCallback);
				}
				CreateP2PConnectionTrialContext();
			}
		}

		internal void FallbackP2PToRelay(bool firstChance, ErrorType reason)
		{
			if (!RelayedP2P)
			{
				RelayedP2P = true;
				if (firstChance)
				{
					owner.c2sProxy.P2P_NotifyDirectP2PDisconnected(HostID.Server, RmiContext.ReliableSendForPN, peerHostID, reason);
				}
				p2pConnectionTrialContext = null;
				if (reason != ErrorType.NoP2PGroupRelation)
				{
					owner.EnqueFallbackP2PToRelayEvent(peerHostID, reason);
				}
				ReserveRepunch();
			}
		}

		internal bool NewUdpSocketBindPort(ushort port)
		{
			AssureUdpSocketNotUnderIssued();
			if (udpSocket != null)
			{
				owner.GarbageSocket(udpSocket);
				Console.WriteLine(string.Format("NewUdpSocketBindPort GarbageSocket Addr:{0}", udpSocket.localAddr.ToString()));
				udpSocket = null;
			}
			lastCheckSendQueueTimeMs = 0L;
			sendQueueHeavyStartTimeMs = 0L;
			if (owner.recycles.TryGetValue(port, out udpSocket))
			{
				ToPeerUdpSocket.recycleTime = 0L;
				ToPeerUdpSocket.garbaged = false;
				owner.recycles.Remove(port);
				return true;
			}
			udpSocket = new UdpSocket_C(owner, PreciseCurrentTime.GetTimeMs(), RequestReceiveSpeedAtReceiverSide_NoRelay);
			IPEndPoint iPEndPoint = new IPEndPoint(owner.ToServerTcp.localAddr.Address, port);
			uint num = BitConverter.ToUInt32(iPEndPoint.Address.GetAddressBytes(), 0);
			if (num == 0 || num == uint.MaxValue)
			{
				Sysutil.ShowUserMisuseError("UDP 소켓을 생성하기 전에 TCP 연결이 이미 되어있는 상태이어야 하는데!");
			}
			if (!ToPeerUdpSocket.CreateSocket(owner.IsProactorAsyncModel, iPEndPoint))
			{
				udpSocket = null;
				return false;
			}
			Console.WriteLine(string.Format("NewUdpSocketBindPort. Addr:{0}", ToPeerUdpSocket.localAddr.ToString()));
			return true;
		}

		internal void ReserveRepunch()
		{
			if (repunchCount < NetConfig.ServerUdpRepunchMaxTrialCount)
			{
				repunchCount++;
				repunchStartTime = PreciseCurrentTime.GetTimeMs() + NetConfig.ServerUdpRepunchIntervalMs;
			}
		}

		internal DirectP2PInfo GetDirectP2PInfo()
		{
			DirectP2PInfo directP2PInfo = new DirectP2PInfo();
			directP2PInfo.localToRemoteAddr = new IPEndPoint(p2pHolepunchedLocalToRemoteAddr.Address, p2pHolepunchedLocalToRemoteAddr.Port);
			directP2PInfo.remoteToLocalAddr = new IPEndPoint(p2pHolepunchedRemoteToLocalAddr.Address, p2pHolepunchedRemoteToLocalAddr.Port);
			if (udpSocket != null)
			{
				directP2PInfo.localUdpSocketAddr = new IPEndPoint(ToPeerUdpSocket.localAddr.Address, ToPeerUdpSocket.localAddr.Port);
			}
			return directP2PInfo;
		}

		internal void RequestReceiveSpeedAtReceiverSide_NoRelay(IPEndPoint dest)
		{
			if (udpSocket != null)
			{
				Message message = new Message();
				message.Write(MessageType.RequestReceiveSpeedAtReceiverSide_NoRelay);
				ToPeerUdpSocket.AddToSendQueueWithSplitterAndSignal_Copy(peerHostID, FilterTag.CreateFilterTag(owner.LocalHostID, peerHostID), dest, message, PreciseCurrentTime.GetTimeMs(), new SendOpt(MessagePriority.Ring1, true));
			}
		}

		internal void AssureUdpSocketNotUnderIssued()
		{
			if (udpSocket != null && (udpSocket.sendIssued || udpSocket.recvIssued))
			{
				int[] array = null;
				array[0] = 1;
			}
		}
	}
}

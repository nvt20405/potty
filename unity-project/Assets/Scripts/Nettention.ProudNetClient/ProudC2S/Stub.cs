using System.Net;
using Nettention.Proud;

namespace ProudC2S
{
	internal class Stub : RmiStub
	{
		public delegate bool ReliablePingDelegate(HostID remote, RmiContext rmiContext, double recentFrameRate, int localTimeMs);

		public delegate bool P2P_NotifyDirectP2PDisconnectedDelegate(HostID remote, RmiContext rmiContext, HostID remotePeerHostID, ErrorType reason);

		public delegate bool NotifyUdpToTcpFallbackByClientDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool P2PGroup_MemberJoin_AckDelegate(HostID remote, RmiContext rmiContext, HostID groupHostID, HostID addedMemberHostID, int eventID, bool localPortReuseSuccess);

		public delegate bool NotifyP2PHolepunchSuccessDelegate(HostID remote, RmiContext rmiContext, HostID A, HostID B, IPEndPoint ABSendAddr, IPEndPoint ABRecvAddr, IPEndPoint BASendAddr, IPEndPoint BARecvAddr);

		public delegate bool ShutdownTcpDelegate(HostID remote, RmiContext rmiContext, ByteArray comment);

		public delegate bool ShutdownTcpHandshakeDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool NotifyLogDelegate(HostID remote, RmiContext rmiContext, TraceID TID, string text);

		public delegate bool NotifyLogHolepunchFreqFailDelegate(HostID remote, RmiContext rmiContext, int rank, string text);

		public delegate bool NotifyNatDeviceNameDelegate(HostID remote, RmiContext rmiContext, string deviceName);

		public delegate bool NotifyPeerUdpSocketRestoredDelegate(HostID remote, RmiContext rmiContext, HostID peerB_ID);

		public delegate bool NotifyJitDirectP2PTriggeredDelegate(HostID remote, RmiContext rmiContext, HostID peerB_ID);

		public delegate bool NotifyNatDeviceNameDetectedDelegate(HostID remote, RmiContext rmiContext, string natDeviceName);

		public delegate bool NotifySendSpeedDelegate(HostID remote, RmiContext rmiContext, double speed);

		public delegate bool ReportP2PPeerPingDelegate(HostID remote, RmiContext rmiContext, HostID peerID, int recentPing);

		public delegate bool C2S_RequestCreateUdpSocketDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool C2S_CreateUdpSocketAckDelegate(HostID remote, RmiContext rmiContext, bool succeed);

		public delegate bool ReportC2CUdpMessageCountDelegate(HostID remote, RmiContext rmiContext, HostID peer, int udpMessageTrialCount, int udpMessageSuccessCount);

		public delegate bool ReportC2SUdpMessageTrialCountDelegate(HostID remote, RmiContext rmiContext, int toServerUdpTrialCount);

		private const string RmiName_ReliablePing = "ReliablePing";

		private const string RmiName_First = "ReliablePing";

		private const string RmiName_P2P_NotifyDirectP2PDisconnected = "P2P_NotifyDirectP2PDisconnected";

		private const string RmiName_NotifyUdpToTcpFallbackByClient = "NotifyUdpToTcpFallbackByClient";

		private const string RmiName_P2PGroup_MemberJoin_Ack = "P2PGroup_MemberJoin_Ack";

		private const string RmiName_NotifyP2PHolepunchSuccess = "NotifyP2PHolepunchSuccess";

		private const string RmiName_ShutdownTcp = "ShutdownTcp";

		private const string RmiName_ShutdownTcpHandshake = "ShutdownTcpHandshake";

		private const string RmiName_NotifyLog = "NotifyLog";

		private const string RmiName_NotifyLogHolepunchFreqFail = "NotifyLogHolepunchFreqFail";

		private const string RmiName_NotifyNatDeviceName = "NotifyNatDeviceName";

		private const string RmiName_NotifyPeerUdpSocketRestored = "NotifyPeerUdpSocketRestored";

		private const string RmiName_NotifyJitDirectP2PTriggered = "NotifyJitDirectP2PTriggered";

		private const string RmiName_NotifyNatDeviceNameDetected = "NotifyNatDeviceNameDetected";

		private const string RmiName_NotifySendSpeed = "NotifySendSpeed";

		private const string RmiName_ReportP2PPeerPing = "ReportP2PPeerPing";

		private const string RmiName_C2S_RequestCreateUdpSocket = "C2S_RequestCreateUdpSocket";

		private const string RmiName_C2S_CreateUdpSocketAck = "C2S_CreateUdpSocketAck";

		private const string RmiName_ReportC2CUdpMessageCount = "ReportC2CUdpMessageCount";

		private const string RmiName_ReportC2SUdpMessageTrialCount = "ReportC2SUdpMessageTrialCount";

		public ReliablePingDelegate ReliablePing = (HostID remote, RmiContext rmiContext, double recentFrameRate, int localTimeMs) => false;

		public P2P_NotifyDirectP2PDisconnectedDelegate P2P_NotifyDirectP2PDisconnected = (HostID remote, RmiContext rmiContext, HostID remotePeerHostID, ErrorType reason) => false;

		public NotifyUdpToTcpFallbackByClientDelegate NotifyUdpToTcpFallbackByClient = (HostID remote, RmiContext rmiContext) => false;

		public P2PGroup_MemberJoin_AckDelegate P2PGroup_MemberJoin_Ack = (HostID remote, RmiContext rmiContext, HostID groupHostID, HostID addedMemberHostID, int eventID, bool localPortReuseSuccess) => false;

		public NotifyP2PHolepunchSuccessDelegate NotifyP2PHolepunchSuccess = (HostID remote, RmiContext rmiContext, HostID A, HostID B, IPEndPoint ABSendAddr, IPEndPoint ABRecvAddr, IPEndPoint BASendAddr, IPEndPoint BARecvAddr) => false;

		public ShutdownTcpDelegate ShutdownTcp = (HostID remote, RmiContext rmiContext, ByteArray comment) => false;

		public ShutdownTcpHandshakeDelegate ShutdownTcpHandshake = (HostID remote, RmiContext rmiContext) => false;

		public NotifyLogDelegate NotifyLog = (HostID remote, RmiContext rmiContext, TraceID TID, string text) => false;

		public NotifyLogHolepunchFreqFailDelegate NotifyLogHolepunchFreqFail = (HostID remote, RmiContext rmiContext, int rank, string text) => false;

		public NotifyNatDeviceNameDelegate NotifyNatDeviceName = (HostID remote, RmiContext rmiContext, string deviceName) => false;

		public NotifyPeerUdpSocketRestoredDelegate NotifyPeerUdpSocketRestored = (HostID remote, RmiContext rmiContext, HostID peerB_ID) => false;

		public NotifyJitDirectP2PTriggeredDelegate NotifyJitDirectP2PTriggered = (HostID remote, RmiContext rmiContext, HostID peerB_ID) => false;

		public NotifyNatDeviceNameDetectedDelegate NotifyNatDeviceNameDetected = (HostID remote, RmiContext rmiContext, string natDeviceName) => false;

		public NotifySendSpeedDelegate NotifySendSpeed = (HostID remote, RmiContext rmiContext, double speed) => false;

		public ReportP2PPeerPingDelegate ReportP2PPeerPing = (HostID remote, RmiContext rmiContext, HostID peerID, int recentPing) => false;

		public C2S_RequestCreateUdpSocketDelegate C2S_RequestCreateUdpSocket = (HostID remote, RmiContext rmiContext) => false;

		public C2S_CreateUdpSocketAckDelegate C2S_CreateUdpSocketAck = (HostID remote, RmiContext rmiContext, bool succeed) => false;

		public ReportC2CUdpMessageCountDelegate ReportC2CUdpMessageCount = (HostID remote, RmiContext rmiContext, HostID peer, int udpMessageTrialCount, int udpMessageSuccessCount) => false;

		public ReportC2SUdpMessageTrialCountDelegate ReportC2SUdpMessageTrialCount = (HostID remote, RmiContext rmiContext, int toServerUdpTrialCount) => false;

		public override RmiID[] GetRmiIDList
		{
			get
			{
				return Common.RmiIDList;
			}
		}

		public override bool ProcessReceivedMessage(ReceivedMessage pa, object hostTag)
		{
			HostID remoteHostID = pa.RemoteHostID;
			if (remoteHostID == HostID.None)
			{
				ShowUnknownHostIDWarning(remoteHostID);
			}
			Message readOnlyMessage = pa.ReadOnlyMessage;
			int readOffset = readOnlyMessage.ReadOffset;
			RmiID b = RmiID.None;
			if (readOnlyMessage.Read(out b))
			{
				switch (b)
				{
				case (RmiID)64001:
				{
					RmiContext rmiContext2 = new RmiContext();
					rmiContext2.sentFrom = pa.RemoteHostID;
					rmiContext2.relayed = pa.IsRelayed;
					rmiContext2.hostTag = hostTag;
					double b3;
					Marshaler.Read(readOnlyMessage, out b3);
					int b4;
					Marshaler.Read(readOnlyMessage, out b4);
					core.PostCheckReadMessage(readOnlyMessage, "ReliablePing");
					if (enableNotifyCallFromStub)
					{
						string text2 = "";
						text2 = text2 + b3 + ",";
						text2 = text2 + b4 + ",";
						NotifyCallFromStub((RmiID)64001, "ReliablePing", text2);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64001,
							rmiName = "ReliablePing",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs2 = PreciseCurrentTime.GetTimeMs();
					if (!ReliablePing(remoteHostID, rmiContext2, b3, b4))
					{
						core.ShowNotImplementedRmiWarning("ReliablePing");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64001,
							rmiName = "ReliablePing",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs2
						});
					}
					goto IL_1937;
				}
				case (RmiID)64002:
				{
					RmiContext rmiContext3 = new RmiContext();
					rmiContext3.sentFrom = pa.RemoteHostID;
					rmiContext3.relayed = pa.IsRelayed;
					rmiContext3.hostTag = hostTag;
					HostID b5;
					Marshaler.Read(readOnlyMessage, out b5);
					ErrorType b6;
					Marshaler.Read(readOnlyMessage, out b6);
					core.PostCheckReadMessage(readOnlyMessage, "P2P_NotifyDirectP2PDisconnected");
					if (enableNotifyCallFromStub)
					{
						string text3 = "";
						text3 = text3 + b5.ToString() + ",";
						text3 = text3 + b6.ToString() + ",";
						NotifyCallFromStub((RmiID)64002, "P2P_NotifyDirectP2PDisconnected", text3);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64002,
							rmiName = "P2P_NotifyDirectP2PDisconnected",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs3 = PreciseCurrentTime.GetTimeMs();
					if (!P2P_NotifyDirectP2PDisconnected(remoteHostID, rmiContext3, b5, b6))
					{
						core.ShowNotImplementedRmiWarning("P2P_NotifyDirectP2PDisconnected");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64002,
							rmiName = "P2P_NotifyDirectP2PDisconnected",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs3
						});
					}
					goto IL_1937;
				}
				case (RmiID)64003:
				{
					RmiContext rmiContext10 = new RmiContext();
					rmiContext10.sentFrom = pa.RemoteHostID;
					rmiContext10.relayed = pa.IsRelayed;
					rmiContext10.hostTag = hostTag;
					core.PostCheckReadMessage(readOnlyMessage, "NotifyUdpToTcpFallbackByClient");
					if (enableNotifyCallFromStub)
					{
						string parameters3 = "";
						NotifyCallFromStub((RmiID)64003, "NotifyUdpToTcpFallbackByClient", parameters3);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64003,
							rmiName = "NotifyUdpToTcpFallbackByClient",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs10 = PreciseCurrentTime.GetTimeMs();
					if (!NotifyUdpToTcpFallbackByClient(remoteHostID, rmiContext10))
					{
						core.ShowNotImplementedRmiWarning("NotifyUdpToTcpFallbackByClient");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64003,
							rmiName = "NotifyUdpToTcpFallbackByClient",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs10
						});
					}
					goto IL_1937;
				}
				case (RmiID)64004:
				{
					RmiContext rmiContext15 = new RmiContext();
					rmiContext15.sentFrom = pa.RemoteHostID;
					rmiContext15.relayed = pa.IsRelayed;
					rmiContext15.hostTag = hostTag;
					HostID b24;
					Marshaler.Read(readOnlyMessage, out b24);
					HostID b25;
					Marshaler.Read(readOnlyMessage, out b25);
					int b26;
					Marshaler.Read(readOnlyMessage, out b26);
					bool b27;
					Marshaler.Read(readOnlyMessage, out b27);
					core.PostCheckReadMessage(readOnlyMessage, "P2PGroup_MemberJoin_Ack");
					if (enableNotifyCallFromStub)
					{
						string text12 = "";
						text12 = text12 + b24.ToString() + ",";
						text12 = text12 + b25.ToString() + ",";
						text12 = text12 + b26 + ",";
						text12 = text12 + b27 + ",";
						NotifyCallFromStub((RmiID)64004, "P2PGroup_MemberJoin_Ack", text12);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64004,
							rmiName = "P2PGroup_MemberJoin_Ack",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs15 = PreciseCurrentTime.GetTimeMs();
					if (!P2PGroup_MemberJoin_Ack(remoteHostID, rmiContext15, b24, b25, b26, b27))
					{
						core.ShowNotImplementedRmiWarning("P2PGroup_MemberJoin_Ack");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64004,
							rmiName = "P2PGroup_MemberJoin_Ack",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs15
						});
					}
					goto IL_1937;
				}
				case (RmiID)64005:
				{
					RmiContext rmiContext12 = new RmiContext();
					rmiContext12.sentFrom = pa.RemoteHostID;
					rmiContext12.relayed = pa.IsRelayed;
					rmiContext12.hostTag = hostTag;
					HostID b15;
					Marshaler.Read(readOnlyMessage, out b15);
					HostID b16;
					Marshaler.Read(readOnlyMessage, out b16);
					IPEndPoint b17;
					Marshaler.Read(readOnlyMessage, out b17);
					IPEndPoint b18;
					Marshaler.Read(readOnlyMessage, out b18);
					IPEndPoint b19;
					Marshaler.Read(readOnlyMessage, out b19);
					IPEndPoint b20;
					Marshaler.Read(readOnlyMessage, out b20);
					core.PostCheckReadMessage(readOnlyMessage, "NotifyP2PHolepunchSuccess");
					if (enableNotifyCallFromStub)
					{
						string text9 = "";
						text9 = text9 + b15.ToString() + ",";
						text9 = text9 + b16.ToString() + ",";
						text9 = text9 + b17.ToString() + ",";
						text9 = text9 + b18.ToString() + ",";
						text9 = text9 + b19.ToString() + ",";
						text9 = text9 + b20.ToString() + ",";
						NotifyCallFromStub((RmiID)64005, "NotifyP2PHolepunchSuccess", text9);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64005,
							rmiName = "NotifyP2PHolepunchSuccess",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs12 = PreciseCurrentTime.GetTimeMs();
					if (!NotifyP2PHolepunchSuccess(remoteHostID, rmiContext12, b15, b16, b17, b18, b19, b20))
					{
						core.ShowNotImplementedRmiWarning("NotifyP2PHolepunchSuccess");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64005,
							rmiName = "NotifyP2PHolepunchSuccess",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs12
						});
					}
					goto IL_1937;
				}
				case (RmiID)64006:
				{
					RmiContext rmiContext19 = new RmiContext();
					rmiContext19.sentFrom = pa.RemoteHostID;
					rmiContext19.relayed = pa.IsRelayed;
					rmiContext19.hostTag = hostTag;
					ByteArray b32;
					Marshaler.Read(readOnlyMessage, out b32);
					core.PostCheckReadMessage(readOnlyMessage, "ShutdownTcp");
					if (enableNotifyCallFromStub)
					{
						string text16 = "";
						text16 = text16 + b32.ToString() + ",";
						NotifyCallFromStub((RmiID)64006, "ShutdownTcp", text16);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64006,
							rmiName = "ShutdownTcp",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs19 = PreciseCurrentTime.GetTimeMs();
					if (!ShutdownTcp(remoteHostID, rmiContext19, b32))
					{
						core.ShowNotImplementedRmiWarning("ShutdownTcp");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64006,
							rmiName = "ShutdownTcp",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs19
						});
					}
					goto IL_1937;
				}
				case (RmiID)64007:
				{
					RmiContext rmiContext6 = new RmiContext();
					rmiContext6.sentFrom = pa.RemoteHostID;
					rmiContext6.relayed = pa.IsRelayed;
					rmiContext6.hostTag = hostTag;
					core.PostCheckReadMessage(readOnlyMessage, "ShutdownTcpHandshake");
					if (enableNotifyCallFromStub)
					{
						string parameters = "";
						NotifyCallFromStub((RmiID)64007, "ShutdownTcpHandshake", parameters);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64007,
							rmiName = "ShutdownTcpHandshake",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs6 = PreciseCurrentTime.GetTimeMs();
					if (!ShutdownTcpHandshake(remoteHostID, rmiContext6))
					{
						core.ShowNotImplementedRmiWarning("ShutdownTcpHandshake");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64007,
							rmiName = "ShutdownTcpHandshake",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs6
						});
					}
					goto IL_1937;
				}
				case (RmiID)64008:
				{
					RmiContext rmiContext17 = new RmiContext();
					rmiContext17.sentFrom = pa.RemoteHostID;
					rmiContext17.relayed = pa.IsRelayed;
					rmiContext17.hostTag = hostTag;
					TraceID b29;
					Marshaler.Read(readOnlyMessage, out b29);
					string b30;
					Marshaler.Read(readOnlyMessage, out b30);
					core.PostCheckReadMessage(readOnlyMessage, "NotifyLog");
					if (enableNotifyCallFromStub)
					{
						string text14 = "";
						text14 = text14 + b29.ToString() + ",";
						text14 = text14 + b30.ToString() + ",";
						NotifyCallFromStub((RmiID)64008, "NotifyLog", text14);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64008,
							rmiName = "NotifyLog",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs17 = PreciseCurrentTime.GetTimeMs();
					if (!NotifyLog(remoteHostID, rmiContext17, b29, b30))
					{
						core.ShowNotImplementedRmiWarning("NotifyLog");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64008,
							rmiName = "NotifyLog",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs17
						});
					}
					goto IL_1937;
				}
				case (RmiID)64009:
				{
					RmiContext rmiContext11 = new RmiContext();
					rmiContext11.sentFrom = pa.RemoteHostID;
					rmiContext11.relayed = pa.IsRelayed;
					rmiContext11.hostTag = hostTag;
					int b13;
					Marshaler.Read(readOnlyMessage, out b13);
					string b14;
					Marshaler.Read(readOnlyMessage, out b14);
					core.PostCheckReadMessage(readOnlyMessage, "NotifyLogHolepunchFreqFail");
					if (enableNotifyCallFromStub)
					{
						string text8 = "";
						text8 = text8 + b13 + ",";
						text8 = text8 + b14.ToString() + ",";
						NotifyCallFromStub((RmiID)64009, "NotifyLogHolepunchFreqFail", text8);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64009,
							rmiName = "NotifyLogHolepunchFreqFail",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs11 = PreciseCurrentTime.GetTimeMs();
					if (!NotifyLogHolepunchFreqFail(remoteHostID, rmiContext11, b13, b14))
					{
						core.ShowNotImplementedRmiWarning("NotifyLogHolepunchFreqFail");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64009,
							rmiName = "NotifyLogHolepunchFreqFail",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs11
						});
					}
					goto IL_1937;
				}
				case (RmiID)64010:
				{
					RmiContext rmiContext13 = new RmiContext();
					rmiContext13.sentFrom = pa.RemoteHostID;
					rmiContext13.relayed = pa.IsRelayed;
					rmiContext13.hostTag = hostTag;
					string b21;
					Marshaler.Read(readOnlyMessage, out b21);
					core.PostCheckReadMessage(readOnlyMessage, "NotifyNatDeviceName");
					if (enableNotifyCallFromStub)
					{
						string text10 = "";
						text10 = text10 + b21.ToString() + ",";
						NotifyCallFromStub((RmiID)64010, "NotifyNatDeviceName", text10);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64010,
							rmiName = "NotifyNatDeviceName",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs13 = PreciseCurrentTime.GetTimeMs();
					if (!NotifyNatDeviceName(remoteHostID, rmiContext13, b21))
					{
						core.ShowNotImplementedRmiWarning("NotifyNatDeviceName");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64010,
							rmiName = "NotifyNatDeviceName",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs13
						});
					}
					goto IL_1937;
				}
				case (RmiID)64011:
				{
					RmiContext rmiContext5 = new RmiContext();
					rmiContext5.sentFrom = pa.RemoteHostID;
					rmiContext5.relayed = pa.IsRelayed;
					rmiContext5.hostTag = hostTag;
					HostID b8;
					Marshaler.Read(readOnlyMessage, out b8);
					core.PostCheckReadMessage(readOnlyMessage, "NotifyPeerUdpSocketRestored");
					if (enableNotifyCallFromStub)
					{
						string text5 = "";
						text5 = text5 + b8.ToString() + ",";
						NotifyCallFromStub((RmiID)64011, "NotifyPeerUdpSocketRestored", text5);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64011,
							rmiName = "NotifyPeerUdpSocketRestored",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs5 = PreciseCurrentTime.GetTimeMs();
					if (!NotifyPeerUdpSocketRestored(remoteHostID, rmiContext5, b8))
					{
						core.ShowNotImplementedRmiWarning("NotifyPeerUdpSocketRestored");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64011,
							rmiName = "NotifyPeerUdpSocketRestored",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs5
						});
					}
					goto IL_1937;
				}
				case (RmiID)64012:
				{
					RmiContext rmiContext8 = new RmiContext();
					rmiContext8.sentFrom = pa.RemoteHostID;
					rmiContext8.relayed = pa.IsRelayed;
					rmiContext8.hostTag = hostTag;
					HostID b9;
					Marshaler.Read(readOnlyMessage, out b9);
					core.PostCheckReadMessage(readOnlyMessage, "NotifyJitDirectP2PTriggered");
					if (enableNotifyCallFromStub)
					{
						string text6 = "";
						text6 = text6 + b9.ToString() + ",";
						NotifyCallFromStub((RmiID)64012, "NotifyJitDirectP2PTriggered", text6);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64012,
							rmiName = "NotifyJitDirectP2PTriggered",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs8 = PreciseCurrentTime.GetTimeMs();
					if (!NotifyJitDirectP2PTriggered(remoteHostID, rmiContext8, b9))
					{
						core.ShowNotImplementedRmiWarning("NotifyJitDirectP2PTriggered");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64012,
							rmiName = "NotifyJitDirectP2PTriggered",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs8
						});
					}
					goto IL_1937;
				}
				case (RmiID)64013:
				{
					RmiContext rmiContext18 = new RmiContext();
					rmiContext18.sentFrom = pa.RemoteHostID;
					rmiContext18.relayed = pa.IsRelayed;
					rmiContext18.hostTag = hostTag;
					string b31;
					Marshaler.Read(readOnlyMessage, out b31);
					core.PostCheckReadMessage(readOnlyMessage, "NotifyNatDeviceNameDetected");
					if (enableNotifyCallFromStub)
					{
						string text15 = "";
						text15 = text15 + b31.ToString() + ",";
						NotifyCallFromStub((RmiID)64013, "NotifyNatDeviceNameDetected", text15);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64013,
							rmiName = "NotifyNatDeviceNameDetected",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs18 = PreciseCurrentTime.GetTimeMs();
					if (!NotifyNatDeviceNameDetected(remoteHostID, rmiContext18, b31))
					{
						core.ShowNotImplementedRmiWarning("NotifyNatDeviceNameDetected");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64013,
							rmiName = "NotifyNatDeviceNameDetected",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs18
						});
					}
					goto IL_1937;
				}
				case (RmiID)64014:
				{
					RmiContext rmiContext4 = new RmiContext();
					rmiContext4.sentFrom = pa.RemoteHostID;
					rmiContext4.relayed = pa.IsRelayed;
					rmiContext4.hostTag = hostTag;
					double b7;
					Marshaler.Read(readOnlyMessage, out b7);
					core.PostCheckReadMessage(readOnlyMessage, "NotifySendSpeed");
					if (enableNotifyCallFromStub)
					{
						string text4 = "";
						text4 = text4 + b7 + ",";
						NotifyCallFromStub((RmiID)64014, "NotifySendSpeed", text4);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64014,
							rmiName = "NotifySendSpeed",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs4 = PreciseCurrentTime.GetTimeMs();
					if (!NotifySendSpeed(remoteHostID, rmiContext4, b7))
					{
						core.ShowNotImplementedRmiWarning("NotifySendSpeed");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64014,
							rmiName = "NotifySendSpeed",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs4
						});
					}
					goto IL_1937;
				}
				case (RmiID)64015:
				{
					RmiContext rmiContext14 = new RmiContext();
					rmiContext14.sentFrom = pa.RemoteHostID;
					rmiContext14.relayed = pa.IsRelayed;
					rmiContext14.hostTag = hostTag;
					HostID b22;
					Marshaler.Read(readOnlyMessage, out b22);
					int b23;
					Marshaler.Read(readOnlyMessage, out b23);
					core.PostCheckReadMessage(readOnlyMessage, "ReportP2PPeerPing");
					if (enableNotifyCallFromStub)
					{
						string text11 = "";
						text11 = text11 + b22.ToString() + ",";
						text11 = text11 + b23 + ",";
						NotifyCallFromStub((RmiID)64015, "ReportP2PPeerPing", text11);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64015,
							rmiName = "ReportP2PPeerPing",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs14 = PreciseCurrentTime.GetTimeMs();
					if (!ReportP2PPeerPing(remoteHostID, rmiContext14, b22, b23))
					{
						core.ShowNotImplementedRmiWarning("ReportP2PPeerPing");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64015,
							rmiName = "ReportP2PPeerPing",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs14
						});
					}
					goto IL_1937;
				}
				case (RmiID)64016:
				{
					RmiContext rmiContext7 = new RmiContext();
					rmiContext7.sentFrom = pa.RemoteHostID;
					rmiContext7.relayed = pa.IsRelayed;
					rmiContext7.hostTag = hostTag;
					core.PostCheckReadMessage(readOnlyMessage, "C2S_RequestCreateUdpSocket");
					if (enableNotifyCallFromStub)
					{
						string parameters2 = "";
						NotifyCallFromStub((RmiID)64016, "C2S_RequestCreateUdpSocket", parameters2);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64016,
							rmiName = "C2S_RequestCreateUdpSocket",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs7 = PreciseCurrentTime.GetTimeMs();
					if (!C2S_RequestCreateUdpSocket(remoteHostID, rmiContext7))
					{
						core.ShowNotImplementedRmiWarning("C2S_RequestCreateUdpSocket");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64016,
							rmiName = "C2S_RequestCreateUdpSocket",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs7
						});
					}
					goto IL_1937;
				}
				case (RmiID)64017:
				{
					RmiContext rmiContext16 = new RmiContext();
					rmiContext16.sentFrom = pa.RemoteHostID;
					rmiContext16.relayed = pa.IsRelayed;
					rmiContext16.hostTag = hostTag;
					bool b28;
					Marshaler.Read(readOnlyMessage, out b28);
					core.PostCheckReadMessage(readOnlyMessage, "C2S_CreateUdpSocketAck");
					if (enableNotifyCallFromStub)
					{
						string text13 = "";
						text13 = text13 + b28 + ",";
						NotifyCallFromStub((RmiID)64017, "C2S_CreateUdpSocketAck", text13);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64017,
							rmiName = "C2S_CreateUdpSocketAck",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs16 = PreciseCurrentTime.GetTimeMs();
					if (!C2S_CreateUdpSocketAck(remoteHostID, rmiContext16, b28))
					{
						core.ShowNotImplementedRmiWarning("C2S_CreateUdpSocketAck");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64017,
							rmiName = "C2S_CreateUdpSocketAck",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs16
						});
					}
					goto IL_1937;
				}
				case (RmiID)64018:
				{
					RmiContext rmiContext9 = new RmiContext();
					rmiContext9.sentFrom = pa.RemoteHostID;
					rmiContext9.relayed = pa.IsRelayed;
					rmiContext9.hostTag = hostTag;
					HostID b10;
					Marshaler.Read(readOnlyMessage, out b10);
					int b11;
					Marshaler.Read(readOnlyMessage, out b11);
					int b12;
					Marshaler.Read(readOnlyMessage, out b12);
					core.PostCheckReadMessage(readOnlyMessage, "ReportC2CUdpMessageCount");
					if (enableNotifyCallFromStub)
					{
						string text7 = "";
						text7 = text7 + b10.ToString() + ",";
						text7 = text7 + b11 + ",";
						text7 = text7 + b12 + ",";
						NotifyCallFromStub((RmiID)64018, "ReportC2CUdpMessageCount", text7);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)64018,
							rmiName = "ReportC2CUdpMessageCount",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs9 = PreciseCurrentTime.GetTimeMs();
					if (!ReportC2CUdpMessageCount(remoteHostID, rmiContext9, b10, b11, b12))
					{
						core.ShowNotImplementedRmiWarning("ReportC2CUdpMessageCount");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)64018,
							rmiName = "ReportC2CUdpMessageCount",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs9
						});
					}
					goto IL_1937;
				}
				case (RmiID)64019:
					{
						RmiContext rmiContext = new RmiContext();
						rmiContext.sentFrom = pa.RemoteHostID;
						rmiContext.relayed = pa.IsRelayed;
						rmiContext.hostTag = hostTag;
						int b2;
						Marshaler.Read(readOnlyMessage, out b2);
						core.PostCheckReadMessage(readOnlyMessage, "ReportC2SUdpMessageTrialCount");
						if (enableNotifyCallFromStub)
						{
							string text = "";
							text = text + b2 + ",";
							NotifyCallFromStub((RmiID)64019, "ReportC2SUdpMessageTrialCount", text);
						}
						if (enableStubProfiling)
						{
							BeforeRmiInvocation(new BeforeRmiSummary
							{
								rmiID = (RmiID)64019,
								rmiName = "ReportC2SUdpMessageTrialCount",
								hostID = remoteHostID,
								hostTag = hostTag
							});
						}
						long timeMs = PreciseCurrentTime.GetTimeMs();
						if (!ReportC2SUdpMessageTrialCount(remoteHostID, rmiContext, b2))
						{
							core.ShowNotImplementedRmiWarning("ReportC2SUdpMessageTrialCount");
						}
						if (enableStubProfiling)
						{
							AfterRmiInvocation(new AfterRmiSummary
							{
								rmiID = (RmiID)64019,
								rmiName = "ReportC2SUdpMessageTrialCount",
								hostID = remoteHostID,
								hostTag = hostTag,
								elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs
							});
						}
						goto IL_1937;
					}
					IL_1937:
					return true;
				}
			}
			readOnlyMessage.ReadOffset = readOffset;
			return false;
		}
	}
}

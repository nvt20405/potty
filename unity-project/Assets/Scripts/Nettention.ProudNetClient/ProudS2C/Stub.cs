using System;
using System.Net;
using Nettention.Proud;

namespace ProudS2C
{
	internal class Stub : RmiStub
	{
		public delegate bool P2PGroup_MemberJoinDelegate(HostID remote, RmiContext rmiContext, HostID groupHostID, HostID memberHostID, ByteArray customField, int eventID, ByteArray p2pAESSessionKey, ByteArray p2pRC4SessionKey, FrameNumber p2pFirstFrameNumber, Guid connectionMagicNumber, bool enableDirectP2P, int bindport);

		public delegate bool P2PGroup_MemberJoin_UnencryptedDelegate(HostID remote, RmiContext rmiContext, HostID groupHostID, HostID memberHostID, ByteArray customField, int eventID, FrameNumber p2pFirstFrameNumber, Guid connectionMagicNumber, bool enableDirectP2P, int bindport);

		public delegate bool P2PRecycleCompleteDelegate(HostID remote, RmiContext rmiContext, HostID remotePeerID, bool recycled, IPEndPoint internalAddr, IPEndPoint externalAddr, IPEndPoint sendAddr, IPEndPoint recvAddr);

		public delegate bool RequestP2PHolepunchDelegate(HostID remote, RmiContext rmiContext, HostID remotePeerID, IPEndPoint internalAddr, IPEndPoint externalAddr);

		public delegate bool P2P_NotifyDirectP2PDisconnected2Delegate(HostID remote, RmiContext rmiContext, HostID remotePeerHostID, ErrorType reason);

		public delegate bool P2PGroup_MemberLeaveDelegate(HostID remote, RmiContext rmiContext, HostID memberHostID, HostID groupHostID);

		public delegate bool NotifyDirectP2PEstablishDelegate(HostID remote, RmiContext rmiContext, HostID A0, HostID B0, IPEndPoint X0, IPEndPoint Y0, IPEndPoint Z0, IPEndPoint W0);

		public delegate bool ReliablePongDelegate(HostID remote, RmiContext rmiContext, int localTimeMs);

		public delegate bool EnableLogDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool DisableLogDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool NotifyUdpToTcpFallbackByServerDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool NotifySpeedHackDetectorEnabledDelegate(HostID remote, RmiContext rmiContext, bool enable);

		public delegate bool ShutdownTcpAckDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RequestAutoPruneDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool RenewP2PConnectionStateDelegate(HostID remote, RmiContext rmiContext, HostID remotePeerID);

		public delegate bool NewDirectP2PConnectionDelegate(HostID remote, RmiContext rmiContext, HostID remotePeerID);

		public delegate bool RequestMeasureSendSpeedDelegate(HostID remote, RmiContext rmiContext, bool enable);

		public delegate bool S2C_RequestCreateUdpSocketDelegate(HostID remote, RmiContext rmiContext, NamedAddrPort serverudpaddr);

		public delegate bool S2C_CreateUdpSocketAckDelegate(HostID remote, RmiContext rmiContext, bool succeed, NamedAddrPort serverudpaddr);

		private const string RmiName_P2PGroup_MemberJoin = "P2PGroup_MemberJoin";

		private const string RmiName_First = "P2PGroup_MemberJoin";

		private const string RmiName_P2PGroup_MemberJoin_Unencrypted = "P2PGroup_MemberJoin_Unencrypted";

		private const string RmiName_P2PRecycleComplete = "P2PRecycleComplete";

		private const string RmiName_RequestP2PHolepunch = "RequestP2PHolepunch";

		private const string RmiName_P2P_NotifyDirectP2PDisconnected2 = "P2P_NotifyDirectP2PDisconnected2";

		private const string RmiName_P2PGroup_MemberLeave = "P2PGroup_MemberLeave";

		private const string RmiName_NotifyDirectP2PEstablish = "NotifyDirectP2PEstablish";

		private const string RmiName_ReliablePong = "ReliablePong";

		private const string RmiName_EnableLog = "EnableLog";

		private const string RmiName_DisableLog = "DisableLog";

		private const string RmiName_NotifyUdpToTcpFallbackByServer = "NotifyUdpToTcpFallbackByServer";

		private const string RmiName_NotifySpeedHackDetectorEnabled = "NotifySpeedHackDetectorEnabled";

		private const string RmiName_ShutdownTcpAck = "ShutdownTcpAck";

		private const string RmiName_RequestAutoPrune = "RequestAutoPrune";

		private const string RmiName_RenewP2PConnectionState = "RenewP2PConnectionState";

		private const string RmiName_NewDirectP2PConnection = "NewDirectP2PConnection";

		private const string RmiName_RequestMeasureSendSpeed = "RequestMeasureSendSpeed";

		private const string RmiName_S2C_RequestCreateUdpSocket = "S2C_RequestCreateUdpSocket";

		private const string RmiName_S2C_CreateUdpSocketAck = "S2C_CreateUdpSocketAck";

		public P2PGroup_MemberJoinDelegate P2PGroup_MemberJoin = (HostID remote, RmiContext rmiContext, HostID groupHostID, HostID memberHostID, ByteArray customField, int eventID, ByteArray p2pAESSessionKey, ByteArray p2pRC4SessionKey, FrameNumber p2pFirstFrameNumber, Guid connectionMagicNumber, bool enableDirectP2P, int bindport) => false;

		public P2PGroup_MemberJoin_UnencryptedDelegate P2PGroup_MemberJoin_Unencrypted = (HostID remote, RmiContext rmiContext, HostID groupHostID, HostID memberHostID, ByteArray customField, int eventID, FrameNumber p2pFirstFrameNumber, Guid connectionMagicNumber, bool enableDirectP2P, int bindport) => false;

		public P2PRecycleCompleteDelegate P2PRecycleComplete = (HostID remote, RmiContext rmiContext, HostID remotePeerID, bool recycled, IPEndPoint internalAddr, IPEndPoint externalAddr, IPEndPoint sendAddr, IPEndPoint recvAddr) => false;

		public RequestP2PHolepunchDelegate RequestP2PHolepunch = (HostID remote, RmiContext rmiContext, HostID remotePeerID, IPEndPoint internalAddr, IPEndPoint externalAddr) => false;

		public P2P_NotifyDirectP2PDisconnected2Delegate P2P_NotifyDirectP2PDisconnected2 = (HostID remote, RmiContext rmiContext, HostID remotePeerHostID, ErrorType reason) => false;

		public P2PGroup_MemberLeaveDelegate P2PGroup_MemberLeave = (HostID remote, RmiContext rmiContext, HostID memberHostID, HostID groupHostID) => false;

		public NotifyDirectP2PEstablishDelegate NotifyDirectP2PEstablish = (HostID remote, RmiContext rmiContext, HostID A0, HostID B0, IPEndPoint X0, IPEndPoint Y0, IPEndPoint Z0, IPEndPoint W0) => false;

		public ReliablePongDelegate ReliablePong = (HostID remote, RmiContext rmiContext, int localTimeMs) => false;

		public EnableLogDelegate EnableLog = (HostID remote, RmiContext rmiContext) => false;

		public DisableLogDelegate DisableLog = (HostID remote, RmiContext rmiContext) => false;

		public NotifyUdpToTcpFallbackByServerDelegate NotifyUdpToTcpFallbackByServer = (HostID remote, RmiContext rmiContext) => false;

		public NotifySpeedHackDetectorEnabledDelegate NotifySpeedHackDetectorEnabled = (HostID remote, RmiContext rmiContext, bool enable) => false;

		public ShutdownTcpAckDelegate ShutdownTcpAck = (HostID remote, RmiContext rmiContext) => false;

		public RequestAutoPruneDelegate RequestAutoPrune = (HostID remote, RmiContext rmiContext) => false;

		public RenewP2PConnectionStateDelegate RenewP2PConnectionState = (HostID remote, RmiContext rmiContext, HostID remotePeerID) => false;

		public NewDirectP2PConnectionDelegate NewDirectP2PConnection = (HostID remote, RmiContext rmiContext, HostID remotePeerID) => false;

		public RequestMeasureSendSpeedDelegate RequestMeasureSendSpeed = (HostID remote, RmiContext rmiContext, bool enable) => false;

		public S2C_RequestCreateUdpSocketDelegate S2C_RequestCreateUdpSocket = (HostID remote, RmiContext rmiContext, NamedAddrPort serverudpaddr) => false;

		public S2C_CreateUdpSocketAckDelegate S2C_CreateUdpSocketAck = (HostID remote, RmiContext rmiContext, bool succeed, NamedAddrPort serverudpaddr) => false;

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
				case (RmiID)63001:
				{
					RmiContext rmiContext2 = new RmiContext();
					rmiContext2.sentFrom = pa.RemoteHostID;
					rmiContext2.relayed = pa.IsRelayed;
					rmiContext2.hostTag = hostTag;
					HostID b4;
					Marshaler.Read(readOnlyMessage, out b4);
					HostID b5;
					Marshaler.Read(readOnlyMessage, out b5);
					ByteArray b6;
					Marshaler.Read(readOnlyMessage, out b6);
					int b7;
					Marshaler.Read(readOnlyMessage, out b7);
					ByteArray b8;
					Marshaler.Read(readOnlyMessage, out b8);
					ByteArray b9;
					Marshaler.Read(readOnlyMessage, out b9);
					FrameNumber b10;
					Marshaler.Read(readOnlyMessage, out b10);
					Guid b11;
					Marshaler.Read(readOnlyMessage, out b11);
					bool b12;
					Marshaler.Read(readOnlyMessage, out b12);
					int b13;
					Marshaler.Read(readOnlyMessage, out b13);
					core.PostCheckReadMessage(readOnlyMessage, "P2PGroup_MemberJoin");
					if (enableNotifyCallFromStub)
					{
						string text2 = "";
						text2 = text2 + b4.ToString() + ",";
						text2 = text2 + b5.ToString() + ",";
						text2 = text2 + b6.ToString() + ",";
						text2 = text2 + b7 + ",";
						text2 = text2 + b8.ToString() + ",";
						text2 = text2 + b9.ToString() + ",";
						text2 = text2 + b10.ToString() + ",";
						text2 = text2 + b11.ToString() + ",";
						text2 = text2 + b12 + ",";
						text2 = text2 + b13 + ",";
						NotifyCallFromStub((RmiID)63001, "P2PGroup_MemberJoin", text2);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63001,
							rmiName = "P2PGroup_MemberJoin",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs2 = PreciseCurrentTime.GetTimeMs();
					if (!P2PGroup_MemberJoin(remoteHostID, rmiContext2, b4, b5, b6, b7, b8, b9, b10, b11, b12, b13))
					{
						core.ShowNotImplementedRmiWarning("P2PGroup_MemberJoin");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63001,
							rmiName = "P2PGroup_MemberJoin",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs2
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63002:
				{
					RmiContext rmiContext3 = new RmiContext();
					rmiContext3.sentFrom = pa.RemoteHostID;
					rmiContext3.relayed = pa.IsRelayed;
					rmiContext3.hostTag = hostTag;
					HostID b14;
					Marshaler.Read(readOnlyMessage, out b14);
					HostID b15;
					Marshaler.Read(readOnlyMessage, out b15);
					ByteArray b16;
					Marshaler.Read(readOnlyMessage, out b16);
					int b17;
					Marshaler.Read(readOnlyMessage, out b17);
					FrameNumber b18;
					Marshaler.Read(readOnlyMessage, out b18);
					Guid b19;
					Marshaler.Read(readOnlyMessage, out b19);
					bool b20;
					Marshaler.Read(readOnlyMessage, out b20);
					int b21;
					Marshaler.Read(readOnlyMessage, out b21);
					core.PostCheckReadMessage(readOnlyMessage, "P2PGroup_MemberJoin_Unencrypted");
					if (enableNotifyCallFromStub)
					{
						string text3 = "";
						text3 = text3 + b14.ToString() + ",";
						text3 = text3 + b15.ToString() + ",";
						text3 = text3 + b16.ToString() + ",";
						text3 = text3 + b17 + ",";
						text3 = text3 + b18.ToString() + ",";
						text3 = text3 + b19.ToString() + ",";
						text3 = text3 + b20 + ",";
						text3 = text3 + b21 + ",";
						NotifyCallFromStub((RmiID)63002, "P2PGroup_MemberJoin_Unencrypted", text3);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63002,
							rmiName = "P2PGroup_MemberJoin_Unencrypted",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs3 = PreciseCurrentTime.GetTimeMs();
					if (!P2PGroup_MemberJoin_Unencrypted(remoteHostID, rmiContext3, b14, b15, b16, b17, b18, b19, b20, b21))
					{
						core.ShowNotImplementedRmiWarning("P2PGroup_MemberJoin_Unencrypted");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63002,
							rmiName = "P2PGroup_MemberJoin_Unencrypted",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs3
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63003:
				{
					RmiContext rmiContext10 = new RmiContext();
					rmiContext10.sentFrom = pa.RemoteHostID;
					rmiContext10.relayed = pa.IsRelayed;
					rmiContext10.hostTag = hostTag;
					HostID b31;
					Marshaler.Read(readOnlyMessage, out b31);
					bool b32;
					Marshaler.Read(readOnlyMessage, out b32);
					IPEndPoint b33;
					Marshaler.Read(readOnlyMessage, out b33);
					IPEndPoint b34;
					Marshaler.Read(readOnlyMessage, out b34);
					IPEndPoint b35;
					Marshaler.Read(readOnlyMessage, out b35);
					IPEndPoint b36;
					Marshaler.Read(readOnlyMessage, out b36);
					core.PostCheckReadMessage(readOnlyMessage, "P2PRecycleComplete");
					if (enableNotifyCallFromStub)
					{
						string text8 = "";
						text8 = text8 + b31.ToString() + ",";
						text8 = text8 + b32 + ",";
						text8 = text8 + b33.ToString() + ",";
						text8 = text8 + b34.ToString() + ",";
						text8 = text8 + b35.ToString() + ",";
						text8 = text8 + b36.ToString() + ",";
						NotifyCallFromStub((RmiID)63003, "P2PRecycleComplete", text8);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63003,
							rmiName = "P2PRecycleComplete",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs10 = PreciseCurrentTime.GetTimeMs();
					if (!P2PRecycleComplete(remoteHostID, rmiContext10, b31, b32, b33, b34, b35, b36))
					{
						core.ShowNotImplementedRmiWarning("P2PRecycleComplete");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63003,
							rmiName = "P2PRecycleComplete",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs10
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63004:
				{
					RmiContext rmiContext15 = new RmiContext();
					rmiContext15.sentFrom = pa.RemoteHostID;
					rmiContext15.relayed = pa.IsRelayed;
					rmiContext15.hostTag = hostTag;
					HostID b40;
					Marshaler.Read(readOnlyMessage, out b40);
					IPEndPoint b41;
					Marshaler.Read(readOnlyMessage, out b41);
					IPEndPoint b42;
					Marshaler.Read(readOnlyMessage, out b42);
					core.PostCheckReadMessage(readOnlyMessage, "RequestP2PHolepunch");
					if (enableNotifyCallFromStub)
					{
						string text11 = "";
						text11 = text11 + b40.ToString() + ",";
						text11 = text11 + b41.ToString() + ",";
						text11 = text11 + b42.ToString() + ",";
						NotifyCallFromStub((RmiID)63004, "RequestP2PHolepunch", text11);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63004,
							rmiName = "RequestP2PHolepunch",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs15 = PreciseCurrentTime.GetTimeMs();
					if (!RequestP2PHolepunch(remoteHostID, rmiContext15, b40, b41, b42))
					{
						core.ShowNotImplementedRmiWarning("RequestP2PHolepunch");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63004,
							rmiName = "RequestP2PHolepunch",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs15
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63005:
				{
					RmiContext rmiContext12 = new RmiContext();
					rmiContext12.sentFrom = pa.RemoteHostID;
					rmiContext12.relayed = pa.IsRelayed;
					rmiContext12.hostTag = hostTag;
					HostID b37;
					Marshaler.Read(readOnlyMessage, out b37);
					ErrorType b38;
					Marshaler.Read(readOnlyMessage, out b38);
					core.PostCheckReadMessage(readOnlyMessage, "P2P_NotifyDirectP2PDisconnected2");
					if (enableNotifyCallFromStub)
					{
						string text9 = "";
						text9 = text9 + b37.ToString() + ",";
						text9 = text9 + b38.ToString() + ",";
						NotifyCallFromStub((RmiID)63005, "P2P_NotifyDirectP2PDisconnected2", text9);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63005,
							rmiName = "P2P_NotifyDirectP2PDisconnected2",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs12 = PreciseCurrentTime.GetTimeMs();
					if (!P2P_NotifyDirectP2PDisconnected2(remoteHostID, rmiContext12, b37, b38))
					{
						core.ShowNotImplementedRmiWarning("P2P_NotifyDirectP2PDisconnected2");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63005,
							rmiName = "P2P_NotifyDirectP2PDisconnected2",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs12
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63006:
				{
					RmiContext rmiContext19 = new RmiContext();
					rmiContext19.sentFrom = pa.RemoteHostID;
					rmiContext19.relayed = pa.IsRelayed;
					rmiContext19.hostTag = hostTag;
					HostID b45;
					Marshaler.Read(readOnlyMessage, out b45);
					HostID b46;
					Marshaler.Read(readOnlyMessage, out b46);
					core.PostCheckReadMessage(readOnlyMessage, "P2PGroup_MemberLeave");
					if (enableNotifyCallFromStub)
					{
						string text14 = "";
						text14 = text14 + b45.ToString() + ",";
						text14 = text14 + b46.ToString() + ",";
						NotifyCallFromStub((RmiID)63006, "P2PGroup_MemberLeave", text14);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63006,
							rmiName = "P2PGroup_MemberLeave",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs19 = PreciseCurrentTime.GetTimeMs();
					if (!P2PGroup_MemberLeave(remoteHostID, rmiContext19, b45, b46))
					{
						core.ShowNotImplementedRmiWarning("P2PGroup_MemberLeave");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63006,
							rmiName = "P2PGroup_MemberLeave",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs19
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63007:
				{
					RmiContext rmiContext6 = new RmiContext();
					rmiContext6.sentFrom = pa.RemoteHostID;
					rmiContext6.relayed = pa.IsRelayed;
					rmiContext6.hostTag = hostTag;
					HostID b22;
					Marshaler.Read(readOnlyMessage, out b22);
					HostID b23;
					Marshaler.Read(readOnlyMessage, out b23);
					IPEndPoint b24;
					Marshaler.Read(readOnlyMessage, out b24);
					IPEndPoint b25;
					Marshaler.Read(readOnlyMessage, out b25);
					IPEndPoint b26;
					Marshaler.Read(readOnlyMessage, out b26);
					IPEndPoint b27;
					Marshaler.Read(readOnlyMessage, out b27);
					core.PostCheckReadMessage(readOnlyMessage, "NotifyDirectP2PEstablish");
					if (enableNotifyCallFromStub)
					{
						string text4 = "";
						text4 = text4 + b22.ToString() + ",";
						text4 = text4 + b23.ToString() + ",";
						text4 = text4 + b24.ToString() + ",";
						text4 = text4 + b25.ToString() + ",";
						text4 = text4 + b26.ToString() + ",";
						text4 = text4 + b27.ToString() + ",";
						NotifyCallFromStub((RmiID)63007, "NotifyDirectP2PEstablish", text4);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63007,
							rmiName = "NotifyDirectP2PEstablish",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs6 = PreciseCurrentTime.GetTimeMs();
					if (!NotifyDirectP2PEstablish(remoteHostID, rmiContext6, b22, b23, b24, b25, b26, b27))
					{
						core.ShowNotImplementedRmiWarning("NotifyDirectP2PEstablish");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63007,
							rmiName = "NotifyDirectP2PEstablish",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs6
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63008:
				{
					RmiContext rmiContext17 = new RmiContext();
					rmiContext17.sentFrom = pa.RemoteHostID;
					rmiContext17.relayed = pa.IsRelayed;
					rmiContext17.hostTag = hostTag;
					int b44;
					Marshaler.Read(readOnlyMessage, out b44);
					core.PostCheckReadMessage(readOnlyMessage, "ReliablePong");
					if (enableNotifyCallFromStub)
					{
						string text13 = "";
						text13 = text13 + b44 + ",";
						NotifyCallFromStub((RmiID)63008, "ReliablePong", text13);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63008,
							rmiName = "ReliablePong",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs17 = PreciseCurrentTime.GetTimeMs();
					if (!ReliablePong(remoteHostID, rmiContext17, b44))
					{
						core.ShowNotImplementedRmiWarning("ReliablePong");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63008,
							rmiName = "ReliablePong",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs17
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63009:
				{
					RmiContext rmiContext11 = new RmiContext();
					rmiContext11.sentFrom = pa.RemoteHostID;
					rmiContext11.relayed = pa.IsRelayed;
					rmiContext11.hostTag = hostTag;
					core.PostCheckReadMessage(readOnlyMessage, "EnableLog");
					if (enableNotifyCallFromStub)
					{
						string parameters3 = "";
						NotifyCallFromStub((RmiID)63009, "EnableLog", parameters3);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63009,
							rmiName = "EnableLog",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs11 = PreciseCurrentTime.GetTimeMs();
					if (!EnableLog(remoteHostID, rmiContext11))
					{
						core.ShowNotImplementedRmiWarning("EnableLog");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63009,
							rmiName = "EnableLog",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs11
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63010:
				{
					RmiContext rmiContext13 = new RmiContext();
					rmiContext13.sentFrom = pa.RemoteHostID;
					rmiContext13.relayed = pa.IsRelayed;
					rmiContext13.hostTag = hostTag;
					core.PostCheckReadMessage(readOnlyMessage, "DisableLog");
					if (enableNotifyCallFromStub)
					{
						string parameters4 = "";
						NotifyCallFromStub((RmiID)63010, "DisableLog", parameters4);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63010,
							rmiName = "DisableLog",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs13 = PreciseCurrentTime.GetTimeMs();
					if (!DisableLog(remoteHostID, rmiContext13))
					{
						core.ShowNotImplementedRmiWarning("DisableLog");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63010,
							rmiName = "DisableLog",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs13
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63011:
				{
					RmiContext rmiContext5 = new RmiContext();
					rmiContext5.sentFrom = pa.RemoteHostID;
					rmiContext5.relayed = pa.IsRelayed;
					rmiContext5.hostTag = hostTag;
					core.PostCheckReadMessage(readOnlyMessage, "NotifyUdpToTcpFallbackByServer");
					if (enableNotifyCallFromStub)
					{
						string parameters2 = "";
						NotifyCallFromStub((RmiID)63011, "NotifyUdpToTcpFallbackByServer", parameters2);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63011,
							rmiName = "NotifyUdpToTcpFallbackByServer",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs5 = PreciseCurrentTime.GetTimeMs();
					if (!NotifyUdpToTcpFallbackByServer(remoteHostID, rmiContext5))
					{
						core.ShowNotImplementedRmiWarning("NotifyUdpToTcpFallbackByServer");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63011,
							rmiName = "NotifyUdpToTcpFallbackByServer",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs5
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63012:
				{
					RmiContext rmiContext8 = new RmiContext();
					rmiContext8.sentFrom = pa.RemoteHostID;
					rmiContext8.relayed = pa.IsRelayed;
					rmiContext8.hostTag = hostTag;
					bool b29;
					Marshaler.Read(readOnlyMessage, out b29);
					core.PostCheckReadMessage(readOnlyMessage, "NotifySpeedHackDetectorEnabled");
					if (enableNotifyCallFromStub)
					{
						string text6 = "";
						text6 = text6 + b29 + ",";
						NotifyCallFromStub((RmiID)63012, "NotifySpeedHackDetectorEnabled", text6);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63012,
							rmiName = "NotifySpeedHackDetectorEnabled",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs8 = PreciseCurrentTime.GetTimeMs();
					if (!NotifySpeedHackDetectorEnabled(remoteHostID, rmiContext8, b29))
					{
						core.ShowNotImplementedRmiWarning("NotifySpeedHackDetectorEnabled");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63012,
							rmiName = "NotifySpeedHackDetectorEnabled",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs8
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63013:
				{
					RmiContext rmiContext18 = new RmiContext();
					rmiContext18.sentFrom = pa.RemoteHostID;
					rmiContext18.relayed = pa.IsRelayed;
					rmiContext18.hostTag = hostTag;
					core.PostCheckReadMessage(readOnlyMessage, "ShutdownTcpAck");
					if (enableNotifyCallFromStub)
					{
						string parameters5 = "";
						NotifyCallFromStub((RmiID)63013, "ShutdownTcpAck", parameters5);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63013,
							rmiName = "ShutdownTcpAck",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs18 = PreciseCurrentTime.GetTimeMs();
					if (!ShutdownTcpAck(remoteHostID, rmiContext18))
					{
						core.ShowNotImplementedRmiWarning("ShutdownTcpAck");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63013,
							rmiName = "ShutdownTcpAck",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs18
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63014:
				{
					RmiContext rmiContext4 = new RmiContext();
					rmiContext4.sentFrom = pa.RemoteHostID;
					rmiContext4.relayed = pa.IsRelayed;
					rmiContext4.hostTag = hostTag;
					core.PostCheckReadMessage(readOnlyMessage, "RequestAutoPrune");
					if (enableNotifyCallFromStub)
					{
						string parameters = "";
						NotifyCallFromStub((RmiID)63014, "RequestAutoPrune", parameters);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63014,
							rmiName = "RequestAutoPrune",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs4 = PreciseCurrentTime.GetTimeMs();
					if (!RequestAutoPrune(remoteHostID, rmiContext4))
					{
						core.ShowNotImplementedRmiWarning("RequestAutoPrune");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63014,
							rmiName = "RequestAutoPrune",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs4
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63015:
				{
					RmiContext rmiContext14 = new RmiContext();
					rmiContext14.sentFrom = pa.RemoteHostID;
					rmiContext14.relayed = pa.IsRelayed;
					rmiContext14.hostTag = hostTag;
					HostID b39;
					Marshaler.Read(readOnlyMessage, out b39);
					core.PostCheckReadMessage(readOnlyMessage, "RenewP2PConnectionState");
					if (enableNotifyCallFromStub)
					{
						string text10 = "";
						text10 = text10 + b39.ToString() + ",";
						NotifyCallFromStub((RmiID)63015, "RenewP2PConnectionState", text10);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63015,
							rmiName = "RenewP2PConnectionState",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs14 = PreciseCurrentTime.GetTimeMs();
					if (!RenewP2PConnectionState(remoteHostID, rmiContext14, b39))
					{
						core.ShowNotImplementedRmiWarning("RenewP2PConnectionState");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63015,
							rmiName = "RenewP2PConnectionState",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs14
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63016:
				{
					RmiContext rmiContext7 = new RmiContext();
					rmiContext7.sentFrom = pa.RemoteHostID;
					rmiContext7.relayed = pa.IsRelayed;
					rmiContext7.hostTag = hostTag;
					HostID b28;
					Marshaler.Read(readOnlyMessage, out b28);
					core.PostCheckReadMessage(readOnlyMessage, "NewDirectP2PConnection");
					if (enableNotifyCallFromStub)
					{
						string text5 = "";
						text5 = text5 + b28.ToString() + ",";
						NotifyCallFromStub((RmiID)63016, "NewDirectP2PConnection", text5);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63016,
							rmiName = "NewDirectP2PConnection",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs7 = PreciseCurrentTime.GetTimeMs();
					if (!NewDirectP2PConnection(remoteHostID, rmiContext7, b28))
					{
						core.ShowNotImplementedRmiWarning("NewDirectP2PConnection");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63016,
							rmiName = "NewDirectP2PConnection",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs7
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63017:
				{
					RmiContext rmiContext16 = new RmiContext();
					rmiContext16.sentFrom = pa.RemoteHostID;
					rmiContext16.relayed = pa.IsRelayed;
					rmiContext16.hostTag = hostTag;
					bool b43;
					Marshaler.Read(readOnlyMessage, out b43);
					core.PostCheckReadMessage(readOnlyMessage, "RequestMeasureSendSpeed");
					if (enableNotifyCallFromStub)
					{
						string text12 = "";
						text12 = text12 + b43 + ",";
						NotifyCallFromStub((RmiID)63017, "RequestMeasureSendSpeed", text12);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63017,
							rmiName = "RequestMeasureSendSpeed",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs16 = PreciseCurrentTime.GetTimeMs();
					if (!RequestMeasureSendSpeed(remoteHostID, rmiContext16, b43))
					{
						core.ShowNotImplementedRmiWarning("RequestMeasureSendSpeed");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63017,
							rmiName = "RequestMeasureSendSpeed",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs16
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63018:
				{
					RmiContext rmiContext9 = new RmiContext();
					rmiContext9.sentFrom = pa.RemoteHostID;
					rmiContext9.relayed = pa.IsRelayed;
					rmiContext9.hostTag = hostTag;
					NamedAddrPort b30;
					Marshaler.Read(readOnlyMessage, out b30);
					core.PostCheckReadMessage(readOnlyMessage, "S2C_RequestCreateUdpSocket");
					if (enableNotifyCallFromStub)
					{
						string text7 = "";
						text7 = text7 + b30.ToString() + ",";
						NotifyCallFromStub((RmiID)63018, "S2C_RequestCreateUdpSocket", text7);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)63018,
							rmiName = "S2C_RequestCreateUdpSocket",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs9 = PreciseCurrentTime.GetTimeMs();
					if (!S2C_RequestCreateUdpSocket(remoteHostID, rmiContext9, b30))
					{
						core.ShowNotImplementedRmiWarning("S2C_RequestCreateUdpSocket");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)63018,
							rmiName = "S2C_RequestCreateUdpSocket",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs9
						});
					}
					goto IL_1b17;
				}
				case (RmiID)63019:
					{
						RmiContext rmiContext = new RmiContext();
						rmiContext.sentFrom = pa.RemoteHostID;
						rmiContext.relayed = pa.IsRelayed;
						rmiContext.hostTag = hostTag;
						bool b2;
						Marshaler.Read(readOnlyMessage, out b2);
						NamedAddrPort b3;
						Marshaler.Read(readOnlyMessage, out b3);
						core.PostCheckReadMessage(readOnlyMessage, "S2C_CreateUdpSocketAck");
						if (enableNotifyCallFromStub)
						{
							string text = "";
							text = text + b2 + ",";
							text = text + b3.ToString() + ",";
							NotifyCallFromStub((RmiID)63019, "S2C_CreateUdpSocketAck", text);
						}
						if (enableStubProfiling)
						{
							BeforeRmiInvocation(new BeforeRmiSummary
							{
								rmiID = (RmiID)63019,
								rmiName = "S2C_CreateUdpSocketAck",
								hostID = remoteHostID,
								hostTag = hostTag
							});
						}
						long timeMs = PreciseCurrentTime.GetTimeMs();
						if (!S2C_CreateUdpSocketAck(remoteHostID, rmiContext, b2, b3))
						{
							core.ShowNotImplementedRmiWarning("S2C_CreateUdpSocketAck");
						}
						if (enableStubProfiling)
						{
							AfterRmiInvocation(new AfterRmiSummary
							{
								rmiID = (RmiID)63019,
								rmiName = "S2C_CreateUdpSocketAck",
								hostID = remoteHostID,
								hostTag = hostTag,
								elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs
							});
						}
						goto IL_1b17;
					}
					IL_1b17:
					return true;
				}
			}
			readOnlyMessage.ReadOffset = readOffset;
			return false;
		}
	}
}

using System;
using System.Net;

namespace Nettention.Proud
{
	internal class P2PConnectionTrialContext
	{
		public enum State
		{
			S_ServerHolepunch = 0,
			S_PeerHolepunch = 1
		}

		internal class StateBase
		{
			public State state;
		}

		internal class ServerHolepunchState : StateBase
		{
			public long sendTimeToDo;

			public int ackReceiveCount;

			public Guid holepunchMagicNumber = Guid.NewGuid();

			public ServerHolepunchState()
			{
				state = State.S_ServerHolepunch;
			}
		}

		internal class PeerHolepunchState : StateBase
		{
			public long sendTimeToDo;

			public int sendTurn;

			public int ackReceiveCount;

			public Guid holepunchMagicNumber = Guid.NewGuid();

			public int offsetShotgunCountdown = NetConfig.ShotgunTrialCount;

			public ushort shotgunMinPortNum = 1023;

			public PeerHolepunchState()
			{
				state = State.S_PeerHolepunch;
			}
		}

		private long startTimeMs;

		private RemotePeer owner;

		public StateBase state;

		private IPEndPoint ServerUdpAddr
		{
			get
			{
				if (owner.owner.toServerUdp_fallbackable != null)
				{
					return owner.owner.toServerUdp_fallbackable.serverAddr;
				}
				return null;
			}
		}

		private IPEndPoint ExternalAddr
		{
			get
			{
				return owner.udpAddrFromServer;
			}
		}

		private IPEndPoint InternalAddr
		{
			get
			{
				return owner.udpAddrInternal;
			}
		}

		public P2PConnectionTrialContext(RemotePeer owner)
		{
			startTimeMs = PreciseCurrentTime.GetTimeMs();
			this.owner = owner;
			state = new ServerHolepunchState();
		}

		private static ushort AdjustUdpPortNumber(ushort portNum)
		{
			if (portNum < 1023 || portNum > 65534)
			{
				portNum = 1023;
			}
			return portNum;
		}

		private static void LogError(NetClient main, string str)
		{
			if (main.enableLog || main.settings.emergencyLogLineCount > 0)
			{
				main.Log(TraceID.Holepunch, string.Format("Holepunch1 실패: {0}", str));
			}
		}

		public static void ProcessPeerHolepunch(NetClient main, ReceivedMessage ri)
		{
			Message readOnlyMessage = ri.ReadOnlyMessage;
			IPEndPoint b = new IPEndPoint(0L, 0);
			HostID b2 = HostID.None;
			Guid b3;
			Guid b4;
			if (!readOnlyMessage.Read(out b2))
			{
				LogError(main, "P2PConnectionTrialContext.ProcessPeerHolepunch 1");
			}
			else if (!readOnlyMessage.Read(out b3))
			{
				LogError(main, "P2PConnectionTrialContext.ProcessPeerHolepunch 2");
			}
			else if (!readOnlyMessage.Read(out b4))
			{
				LogError(main, "P2PConnectionTrialContext.ProcessPeerHolepunch 3");
			}
			else if (!readOnlyMessage.Read(out b))
			{
				LogError(main, "P2PConnectionTrialContext.ProcessPeerHolepunch 4");
			}
			else
			{
				if (!b4.Equals(main.serverInstanceGuid))
				{
					return;
				}
				IPEndPoint remoteAddr_onlyUdp = ri.remoteAddr_onlyUdp;
				RemotePeer peerByHostID = main.GetPeerByHostID(b2);
				if (peerByHostID != null && !peerByHostID.garbaged && peerByHostID.p2pConnectionTrialContext != null && b3.Equals(peerByHostID.magicNumber))
				{
					if (main.enableLog || main.settings.emergencyLogLineCount > 0)
					{
						main.Log(TraceID.Holepunch, string.Format("Received P2P Holepunch. ABS={0} ABR={1}", b.ToString(), remoteAddr_onlyUdp.ToString()));
					}
					NetUtil.IsUnicastEndpoint(peerByHostID.udpAddrFromServer);
					if (NetUtil.IsUnicastEndpoint(peerByHostID.udpAddrInternal) && NetUtil.IsUnicastEndpoint(peerByHostID.ToPeerUdpSocket.localAddr) && NetUtil.IsSameLan(peerByHostID.ToPeerUdpSocket.addrOfHereAtServer, peerByHostID.udpAddrFromServer) && NetUtil.IsSameLan(peerByHostID.udpAddrInternal, peerByHostID.ToPeerUdpSocket.localAddr))
					{
						peerByHostID.p2pConnectionTrialContext.SendPeerHolepunchAck(peerByHostID.udpAddrInternal, b3, b, remoteAddr_onlyUdp);
					}
					if (NetUtil.IsUnicastEndpoint(peerByHostID.udpAddrFromServer))
					{
						peerByHostID.p2pConnectionTrialContext.SendPeerHolepunchAck(peerByHostID.udpAddrFromServer, b3, b, remoteAddr_onlyUdp);
					}
					peerByHostID.p2pConnectionTrialContext.SendPeerHolepunchAck(ri.remoteAddr_onlyUdp, b3, b, remoteAddr_onlyUdp);
				}
			}
		}

		public static void ProcessPeerHolepunchAck(NetClient main, ReceivedMessage ri)
		{
			Message readOnlyMessage = ri.ReadOnlyMessage;
			HostID b = HostID.None;
			IPEndPoint b2 = new IPEndPoint(0L, 0);
			IPEndPoint b3 = new IPEndPoint(0L, 0);
			IPEndPoint b4 = new IPEndPoint(0L, 0);
			Guid b5;
			if (!readOnlyMessage.Read(out b5) || !readOnlyMessage.Read(out b) || !readOnlyMessage.Read(out b2) || !readOnlyMessage.Read(out b3) || !readOnlyMessage.Read(out b4))
			{
				return;
			}
			IPEndPoint remoteAddr_onlyUdp = ri.remoteAddr_onlyUdp;
			RemotePeer peerByHostID = main.GetPeerByHostID(b);
			if (peerByHostID != null && !peerByHostID.garbaged && peerByHostID.p2pConnectionTrialContext != null && peerByHostID.magicNumber.Equals(b5) && peerByHostID.p2pConnectionTrialContext.state != null && peerByHostID.p2pConnectionTrialContext.state.state == State.S_PeerHolepunch)
			{
				peerByHostID.p2pConnectionTrialContext = null;
				RmiContext rmiContext = RmiContext.ReliableSendForPN.Clone();
				rmiContext.maxDirectP2PMulticastCount = 0;
				main.c2cProxy.HolsterP2PHolepunchTrial(peerByHostID.peerHostID, rmiContext);
				main.c2sProxy.NotifyP2PHolepunchSuccess(HostID.Server, RmiContext.ReliableSendForPN, main.LocalHostID, peerByHostID.peerHostID, b2, b3, b4, remoteAddr_onlyUdp);
				if (main.enableLog || main.settings.emergencyLogLineCount > 0)
				{
					main.Log(TraceID.Holepunch, string.Format("HolepunchAck OK. ABS={0} ABR={1} BAS={2} BAR={3}", b2.ToString(), b3.ToString(), b4.ToString(), remoteAddr_onlyUdp.ToString()));
				}
			}
		}

		private void SendPeerHolepunch(IPEndPoint ABSendAddr, Guid magicNumber)
		{
			Message message = new Message();
			message.Write(MessageType.PeerUdp_PeerHolepunch);
			message.Write(owner.owner.LocalHostID);
			message.Write(magicNumber);
			message.Write(owner.owner.serverInstanceGuid);
			message.Write(ABSendAddr);
			owner.ToPeerUdpSocket.AddToSendQueueWithSplitterAndSignal_Copy(owner.peerHostID, FilterTag.CreateFilterTag(owner.owner.LocalHostID, owner.peerHostID), ABSendAddr, message, PreciseCurrentTime.GetTimeMs(), new SendOpt(NetClient.MessagePriority_Holepunch, true));
		}

		private void SendPeerHolepunchAck(IPEndPoint BASendAddr, Guid magicNumber, IPEndPoint ABSendAddr, IPEndPoint ABRecvAddr)
		{
			Message message = new Message();
			message.Write(MessageType.PeerUdp_PeerHolepunchAck);
			message.Write(magicNumber);
			message.Write(owner.owner.LocalHostID);
			message.Write(ABSendAddr);
			message.Write(ABRecvAddr);
			message.Write(BASendAddr);
			owner.ToPeerUdpSocket.AddToSendQueueWithSplitterAndSignal_Copy(owner.peerHostID, FilterTag.CreateFilterTag(owner.owner.LocalHostID, owner.peerHostID), BASendAddr, message, PreciseCurrentTime.GetTimeMs(), new SendOpt(NetClient.MessagePriority_Holepunch, true));
			if (owner.owner.enableLog || owner.owner.settings.emergencyLogLineCount > 0)
			{
				owner.owner.Log(TraceID.Holepunch, string.Format("Try to PeerHolepunchAck. ABS={0} ABR={1} BAS={2}", ABSendAddr.ToString(), ABRecvAddr.ToString(), BASendAddr.ToString()));
			}
		}

		public bool Heartbeat()
		{
			if (PreciseCurrentTime.GetTimeMs() - startTimeMs > owner.owner.p2pConnectionTrialEndTimeMs)
			{
				return false;
			}
			switch (state.state)
			{
			case State.S_ServerHolepunch:
			{
				ServerHolepunchState serverHolepunchState = (ServerHolepunchState)state;
				if (serverHolepunchState.sendTimeToDo < PreciseCurrentTime.GetTimeMs())
				{
					serverHolepunchState.sendTimeToDo = PreciseCurrentTime.GetTimeMs() + NetConfig.ServerHolepunchIntervalMs;
					Message message = new Message();
					message.Write(MessageType.PeerUdp_ServerHolepunch);
					message.Write(serverHolepunchState.holepunchMagicNumber);
					message.Write(owner.peerHostID);
					owner.ToPeerUdpSocket.AddToSendQueueWithSplitterAndSignal_Copy(HostID.Server, FilterTag.CreateFilterTag(owner.owner.LocalHostID, HostID.Server), ServerUdpAddr, message, PreciseCurrentTime.GetTimeMs(), new SendOpt(NetClient.MessagePriority_Holepunch, true));
				}
				break;
			}
			case State.S_PeerHolepunch:
			{
				PeerHolepunchState peerHolepunchState = (PeerHolepunchState)state;
				if (peerHolepunchState.sendTimeToDo >= PreciseCurrentTime.GetTimeMs())
				{
					break;
				}
				peerHolepunchState.sendTimeToDo = PreciseCurrentTime.GetTimeMs() + owner.owner.p2pHolepunchIntervalMs;
				peerHolepunchState.sendTurn++;
				SendPeerHolepunch(owner.udpAddrFromServer, owner.magicNumber);
				if (owner.udpSocket != null && NetUtil.IsSameHost(owner.ToPeerUdpSocket.addrOfHereAtServer, owner.udpAddrFromServer) && NetUtil.IsSameLan(owner.udpAddrInternal, owner.ToPeerUdpSocket.localAddr))
				{
					SendPeerHolepunch(owner.udpAddrInternal, owner.magicNumber);
				}
				if (peerHolepunchState.sendTurn > NetConfig.P2PShotgunStartTurn)
				{
					peerHolepunchState.offsetShotgunCountdown--;
					if (peerHolepunchState.offsetShotgunCountdown < 0)
					{
						peerHolepunchState.offsetShotgunCountdown = NetConfig.ShotgunTrialCount;
						PeerHolepunchState peerHolepunchState2 = peerHolepunchState;
						peerHolepunchState2.shotgunMinPortNum += (ushort)NetConfig.ShotgunRange;
						peerHolepunchState.shotgunMinPortNum = AdjustUdpPortNumber(peerHolepunchState.shotgunMinPortNum);
					}
					IPEndPoint iPEndPoint = new IPEndPoint(owner.udpAddrFromServer.Address, peerHolepunchState.shotgunMinPortNum);
					for (int i = 0; i < NetConfig.ShotgunRange; i++)
					{
						SendPeerHolepunch(iPEndPoint, owner.magicNumber);
						iPEndPoint.Port++;
						iPEndPoint.Port = AdjustUdpPortNumber((ushort)iPEndPoint.Port);
					}
				}
				break;
			}
			}
			return true;
		}

		internal void ProcessMessage_PeerUdp_ServerHolepunchAck(ReceivedMessage ri, Guid magicNumber, IPEndPoint addrOfHereAtServer, HostID peerID)
		{
			if (state == null || state.state != State.S_ServerHolepunch)
			{
				return;
			}
			ServerHolepunchState serverHolepunchState = (ServerHolepunchState)state;
			if (magicNumber.Equals(serverHolepunchState.holepunchMagicNumber) && serverHolepunchState.ackReceiveCount <= 0 && ServerUdpAddr.Equals(ri.remoteAddr_onlyUdp) && owner.ToPeerUdpSocket != null)
			{
				Message message = new Message();
				message.Write(MessageType.PeerUdp_NotifyHolepunchSuccess);
				message.Write(owner.ToPeerUdpSocket.localAddr);
				message.Write(addrOfHereAtServer);
				message.Write(owner.peerHostID);
				SendFragRefs sendFragRefs = new SendFragRefs();
				sendFragRefs.Add(message);
				owner.ToPeerUdpSocket.addrOfHereAtServer = new IPEndPoint(addrOfHereAtServer.Address, addrOfHereAtServer.Port);
				owner.owner.ToServerTcp.AddToSendQueueWithSplitterAndSignal_Copy(sendFragRefs, new SendOpt());
				if (owner.owner.enableLog || owner.owner.settings.emergencyLogLineCount > 0)
				{
					owner.owner.Log(TraceID.Holepunch, string.Format("Message_PeerUdp_ServerHolepunchAck. AddrOfHereAtServer={0}", addrOfHereAtServer.ToString()));
				}
				serverHolepunchState.ackReceiveCount++;
			}
		}
	}
}

using System;
using System.Net;
using Nettention.Proud;

namespace ProudS2C
{
	internal class Proxy : RmiProxy
	{
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

		public bool P2PGroup_MemberJoin(HostID remote, RmiContext rmiContext, HostID groupHostID, HostID memberHostID, ByteArray customField, int eventID, ByteArray p2pAESSessionKey, ByteArray p2pRC4SessionKey, FrameNumber p2pFirstFrameNumber, Guid connectionMagicNumber, bool enableDirectP2P, int bindport)
		{
			Message message = new Message();
			RmiID b = (RmiID)63001;
			message.Write(b);
			Marshaler.Write(message, groupHostID);
			Marshaler.Write(message, memberHostID);
			Marshaler.Write(message, customField);
			Marshaler.Write(message, eventID);
			Marshaler.Write(message, p2pAESSessionKey);
			Marshaler.Write(message, p2pRC4SessionKey);
			Marshaler.Write(message, p2pFirstFrameNumber);
			Marshaler.Write(message, connectionMagicNumber);
			Marshaler.Write(message, enableDirectP2P);
			Marshaler.Write(message, bindport);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "P2PGroup_MemberJoin", (RmiID)63001);
		}

		public bool P2PGroup_MemberJoin(HostID[] remotes, RmiContext rmiContext, HostID groupHostID, HostID memberHostID, ByteArray customField, int eventID, ByteArray p2pAESSessionKey, ByteArray p2pRC4SessionKey, FrameNumber p2pFirstFrameNumber, Guid connectionMagicNumber, bool enableDirectP2P, int bindport)
		{
			Message message = new Message();
			RmiID b = (RmiID)63001;
			message.Write(b);
			Marshaler.Write(message, groupHostID);
			Marshaler.Write(message, memberHostID);
			Marshaler.Write(message, customField);
			Marshaler.Write(message, eventID);
			Marshaler.Write(message, p2pAESSessionKey);
			Marshaler.Write(message, p2pRC4SessionKey);
			Marshaler.Write(message, p2pFirstFrameNumber);
			Marshaler.Write(message, connectionMagicNumber);
			Marshaler.Write(message, enableDirectP2P);
			Marshaler.Write(message, bindport);
			return RmiSend(remotes, rmiContext, message, "P2PGroup_MemberJoin", (RmiID)63001);
		}

		public bool P2PGroup_MemberJoin_Unencrypted(HostID remote, RmiContext rmiContext, HostID groupHostID, HostID memberHostID, ByteArray customField, int eventID, FrameNumber p2pFirstFrameNumber, Guid connectionMagicNumber, bool enableDirectP2P, int bindport)
		{
			Message message = new Message();
			RmiID b = (RmiID)63002;
			message.Write(b);
			Marshaler.Write(message, groupHostID);
			Marshaler.Write(message, memberHostID);
			Marshaler.Write(message, customField);
			Marshaler.Write(message, eventID);
			Marshaler.Write(message, p2pFirstFrameNumber);
			Marshaler.Write(message, connectionMagicNumber);
			Marshaler.Write(message, enableDirectP2P);
			Marshaler.Write(message, bindport);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "P2PGroup_MemberJoin_Unencrypted", (RmiID)63002);
		}

		public bool P2PGroup_MemberJoin_Unencrypted(HostID[] remotes, RmiContext rmiContext, HostID groupHostID, HostID memberHostID, ByteArray customField, int eventID, FrameNumber p2pFirstFrameNumber, Guid connectionMagicNumber, bool enableDirectP2P, int bindport)
		{
			Message message = new Message();
			RmiID b = (RmiID)63002;
			message.Write(b);
			Marshaler.Write(message, groupHostID);
			Marshaler.Write(message, memberHostID);
			Marshaler.Write(message, customField);
			Marshaler.Write(message, eventID);
			Marshaler.Write(message, p2pFirstFrameNumber);
			Marshaler.Write(message, connectionMagicNumber);
			Marshaler.Write(message, enableDirectP2P);
			Marshaler.Write(message, bindport);
			return RmiSend(remotes, rmiContext, message, "P2PGroup_MemberJoin_Unencrypted", (RmiID)63002);
		}

		public bool P2PRecycleComplete(HostID remote, RmiContext rmiContext, HostID remotePeerID, bool recycled, IPEndPoint internalAddr, IPEndPoint externalAddr, IPEndPoint sendAddr, IPEndPoint recvAddr)
		{
			Message message = new Message();
			RmiID b = (RmiID)63003;
			message.Write(b);
			Marshaler.Write(message, remotePeerID);
			Marshaler.Write(message, recycled);
			Marshaler.Write(message, internalAddr);
			Marshaler.Write(message, externalAddr);
			Marshaler.Write(message, sendAddr);
			Marshaler.Write(message, recvAddr);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "P2PRecycleComplete", (RmiID)63003);
		}

		public bool P2PRecycleComplete(HostID[] remotes, RmiContext rmiContext, HostID remotePeerID, bool recycled, IPEndPoint internalAddr, IPEndPoint externalAddr, IPEndPoint sendAddr, IPEndPoint recvAddr)
		{
			Message message = new Message();
			RmiID b = (RmiID)63003;
			message.Write(b);
			Marshaler.Write(message, remotePeerID);
			Marshaler.Write(message, recycled);
			Marshaler.Write(message, internalAddr);
			Marshaler.Write(message, externalAddr);
			Marshaler.Write(message, sendAddr);
			Marshaler.Write(message, recvAddr);
			return RmiSend(remotes, rmiContext, message, "P2PRecycleComplete", (RmiID)63003);
		}

		public bool RequestP2PHolepunch(HostID remote, RmiContext rmiContext, HostID remotePeerID, IPEndPoint internalAddr, IPEndPoint externalAddr)
		{
			Message message = new Message();
			RmiID b = (RmiID)63004;
			message.Write(b);
			Marshaler.Write(message, remotePeerID);
			Marshaler.Write(message, internalAddr);
			Marshaler.Write(message, externalAddr);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "RequestP2PHolepunch", (RmiID)63004);
		}

		public bool RequestP2PHolepunch(HostID[] remotes, RmiContext rmiContext, HostID remotePeerID, IPEndPoint internalAddr, IPEndPoint externalAddr)
		{
			Message message = new Message();
			RmiID b = (RmiID)63004;
			message.Write(b);
			Marshaler.Write(message, remotePeerID);
			Marshaler.Write(message, internalAddr);
			Marshaler.Write(message, externalAddr);
			return RmiSend(remotes, rmiContext, message, "RequestP2PHolepunch", (RmiID)63004);
		}

		public bool P2P_NotifyDirectP2PDisconnected2(HostID remote, RmiContext rmiContext, HostID remotePeerHostID, ErrorType reason)
		{
			Message message = new Message();
			RmiID b = (RmiID)63005;
			message.Write(b);
			Marshaler.Write(message, remotePeerHostID);
			Marshaler.Write(message, reason);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "P2P_NotifyDirectP2PDisconnected2", (RmiID)63005);
		}

		public bool P2P_NotifyDirectP2PDisconnected2(HostID[] remotes, RmiContext rmiContext, HostID remotePeerHostID, ErrorType reason)
		{
			Message message = new Message();
			RmiID b = (RmiID)63005;
			message.Write(b);
			Marshaler.Write(message, remotePeerHostID);
			Marshaler.Write(message, reason);
			return RmiSend(remotes, rmiContext, message, "P2P_NotifyDirectP2PDisconnected2", (RmiID)63005);
		}

		public bool P2PGroup_MemberLeave(HostID remote, RmiContext rmiContext, HostID memberHostID, HostID groupHostID)
		{
			Message message = new Message();
			RmiID b = (RmiID)63006;
			message.Write(b);
			Marshaler.Write(message, memberHostID);
			Marshaler.Write(message, groupHostID);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "P2PGroup_MemberLeave", (RmiID)63006);
		}

		public bool P2PGroup_MemberLeave(HostID[] remotes, RmiContext rmiContext, HostID memberHostID, HostID groupHostID)
		{
			Message message = new Message();
			RmiID b = (RmiID)63006;
			message.Write(b);
			Marshaler.Write(message, memberHostID);
			Marshaler.Write(message, groupHostID);
			return RmiSend(remotes, rmiContext, message, "P2PGroup_MemberLeave", (RmiID)63006);
		}

		public bool NotifyDirectP2PEstablish(HostID remote, RmiContext rmiContext, HostID A0, HostID B0, IPEndPoint X0, IPEndPoint Y0, IPEndPoint Z0, IPEndPoint W0)
		{
			Message message = new Message();
			RmiID b = (RmiID)63007;
			message.Write(b);
			Marshaler.Write(message, A0);
			Marshaler.Write(message, B0);
			Marshaler.Write(message, X0);
			Marshaler.Write(message, Y0);
			Marshaler.Write(message, Z0);
			Marshaler.Write(message, W0);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "NotifyDirectP2PEstablish", (RmiID)63007);
		}

		public bool NotifyDirectP2PEstablish(HostID[] remotes, RmiContext rmiContext, HostID A0, HostID B0, IPEndPoint X0, IPEndPoint Y0, IPEndPoint Z0, IPEndPoint W0)
		{
			Message message = new Message();
			RmiID b = (RmiID)63007;
			message.Write(b);
			Marshaler.Write(message, A0);
			Marshaler.Write(message, B0);
			Marshaler.Write(message, X0);
			Marshaler.Write(message, Y0);
			Marshaler.Write(message, Z0);
			Marshaler.Write(message, W0);
			return RmiSend(remotes, rmiContext, message, "NotifyDirectP2PEstablish", (RmiID)63007);
		}

		public bool ReliablePong(HostID remote, RmiContext rmiContext, int localTimeMs)
		{
			Message message = new Message();
			RmiID b = (RmiID)63008;
			message.Write(b);
			Marshaler.Write(message, localTimeMs);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "ReliablePong", (RmiID)63008);
		}

		public bool ReliablePong(HostID[] remotes, RmiContext rmiContext, int localTimeMs)
		{
			Message message = new Message();
			RmiID b = (RmiID)63008;
			message.Write(b);
			Marshaler.Write(message, localTimeMs);
			return RmiSend(remotes, rmiContext, message, "ReliablePong", (RmiID)63008);
		}

		public bool EnableLog(HostID remote, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)63009;
			message.Write(b);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "EnableLog", (RmiID)63009);
		}

		public bool EnableLog(HostID[] remotes, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)63009;
			message.Write(b);
			return RmiSend(remotes, rmiContext, message, "EnableLog", (RmiID)63009);
		}

		public bool DisableLog(HostID remote, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)63010;
			message.Write(b);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "DisableLog", (RmiID)63010);
		}

		public bool DisableLog(HostID[] remotes, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)63010;
			message.Write(b);
			return RmiSend(remotes, rmiContext, message, "DisableLog", (RmiID)63010);
		}

		public bool NotifyUdpToTcpFallbackByServer(HostID remote, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)63011;
			message.Write(b);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "NotifyUdpToTcpFallbackByServer", (RmiID)63011);
		}

		public bool NotifyUdpToTcpFallbackByServer(HostID[] remotes, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)63011;
			message.Write(b);
			return RmiSend(remotes, rmiContext, message, "NotifyUdpToTcpFallbackByServer", (RmiID)63011);
		}

		public bool NotifySpeedHackDetectorEnabled(HostID remote, RmiContext rmiContext, bool enable)
		{
			Message message = new Message();
			RmiID b = (RmiID)63012;
			message.Write(b);
			Marshaler.Write(message, enable);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "NotifySpeedHackDetectorEnabled", (RmiID)63012);
		}

		public bool NotifySpeedHackDetectorEnabled(HostID[] remotes, RmiContext rmiContext, bool enable)
		{
			Message message = new Message();
			RmiID b = (RmiID)63012;
			message.Write(b);
			Marshaler.Write(message, enable);
			return RmiSend(remotes, rmiContext, message, "NotifySpeedHackDetectorEnabled", (RmiID)63012);
		}

		public bool ShutdownTcpAck(HostID remote, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)63013;
			message.Write(b);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "ShutdownTcpAck", (RmiID)63013);
		}

		public bool ShutdownTcpAck(HostID[] remotes, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)63013;
			message.Write(b);
			return RmiSend(remotes, rmiContext, message, "ShutdownTcpAck", (RmiID)63013);
		}

		public bool RequestAutoPrune(HostID remote, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)63014;
			message.Write(b);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "RequestAutoPrune", (RmiID)63014);
		}

		public bool RequestAutoPrune(HostID[] remotes, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)63014;
			message.Write(b);
			return RmiSend(remotes, rmiContext, message, "RequestAutoPrune", (RmiID)63014);
		}

		public bool RenewP2PConnectionState(HostID remote, RmiContext rmiContext, HostID remotePeerID)
		{
			Message message = new Message();
			RmiID b = (RmiID)63015;
			message.Write(b);
			Marshaler.Write(message, remotePeerID);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "RenewP2PConnectionState", (RmiID)63015);
		}

		public bool RenewP2PConnectionState(HostID[] remotes, RmiContext rmiContext, HostID remotePeerID)
		{
			Message message = new Message();
			RmiID b = (RmiID)63015;
			message.Write(b);
			Marshaler.Write(message, remotePeerID);
			return RmiSend(remotes, rmiContext, message, "RenewP2PConnectionState", (RmiID)63015);
		}

		public bool NewDirectP2PConnection(HostID remote, RmiContext rmiContext, HostID remotePeerID)
		{
			Message message = new Message();
			RmiID b = (RmiID)63016;
			message.Write(b);
			Marshaler.Write(message, remotePeerID);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "NewDirectP2PConnection", (RmiID)63016);
		}

		public bool NewDirectP2PConnection(HostID[] remotes, RmiContext rmiContext, HostID remotePeerID)
		{
			Message message = new Message();
			RmiID b = (RmiID)63016;
			message.Write(b);
			Marshaler.Write(message, remotePeerID);
			return RmiSend(remotes, rmiContext, message, "NewDirectP2PConnection", (RmiID)63016);
		}

		public bool RequestMeasureSendSpeed(HostID remote, RmiContext rmiContext, bool enable)
		{
			Message message = new Message();
			RmiID b = (RmiID)63017;
			message.Write(b);
			Marshaler.Write(message, enable);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "RequestMeasureSendSpeed", (RmiID)63017);
		}

		public bool RequestMeasureSendSpeed(HostID[] remotes, RmiContext rmiContext, bool enable)
		{
			Message message = new Message();
			RmiID b = (RmiID)63017;
			message.Write(b);
			Marshaler.Write(message, enable);
			return RmiSend(remotes, rmiContext, message, "RequestMeasureSendSpeed", (RmiID)63017);
		}

		public bool S2C_RequestCreateUdpSocket(HostID remote, RmiContext rmiContext, NamedAddrPort serverudpaddr)
		{
			Message message = new Message();
			RmiID b = (RmiID)63018;
			message.Write(b);
			Marshaler.Write(message, serverudpaddr);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "S2C_RequestCreateUdpSocket", (RmiID)63018);
		}

		public bool S2C_RequestCreateUdpSocket(HostID[] remotes, RmiContext rmiContext, NamedAddrPort serverudpaddr)
		{
			Message message = new Message();
			RmiID b = (RmiID)63018;
			message.Write(b);
			Marshaler.Write(message, serverudpaddr);
			return RmiSend(remotes, rmiContext, message, "S2C_RequestCreateUdpSocket", (RmiID)63018);
		}

		public bool S2C_CreateUdpSocketAck(HostID remote, RmiContext rmiContext, bool succeed, NamedAddrPort serverudpaddr)
		{
			Message message = new Message();
			RmiID b = (RmiID)63019;
			message.Write(b);
			Marshaler.Write(message, succeed);
			Marshaler.Write(message, serverudpaddr);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "S2C_CreateUdpSocketAck", (RmiID)63019);
		}

		public bool S2C_CreateUdpSocketAck(HostID[] remotes, RmiContext rmiContext, bool succeed, NamedAddrPort serverudpaddr)
		{
			Message message = new Message();
			RmiID b = (RmiID)63019;
			message.Write(b);
			Marshaler.Write(message, succeed);
			Marshaler.Write(message, serverudpaddr);
			return RmiSend(remotes, rmiContext, message, "S2C_CreateUdpSocketAck", (RmiID)63019);
		}

		public override RmiID[] GetRmiIDList()
		{
			return Common.RmiIDList;
		}
	}
}

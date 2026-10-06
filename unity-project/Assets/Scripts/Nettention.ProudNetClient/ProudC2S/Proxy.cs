using System.Net;
using Nettention.Proud;

namespace ProudC2S
{
	internal class Proxy : RmiProxy
	{
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

		public bool ReliablePing(HostID remote, RmiContext rmiContext, double recentFrameRate, int localTimeMs)
		{
			Message message = new Message();
			RmiID b = (RmiID)64001;
			message.Write(b);
			Marshaler.Write(message, recentFrameRate);
			Marshaler.Write(message, localTimeMs);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "ReliablePing", (RmiID)64001);
		}

		public bool ReliablePing(HostID[] remotes, RmiContext rmiContext, double recentFrameRate, int localTimeMs)
		{
			Message message = new Message();
			RmiID b = (RmiID)64001;
			message.Write(b);
			Marshaler.Write(message, recentFrameRate);
			Marshaler.Write(message, localTimeMs);
			return RmiSend(remotes, rmiContext, message, "ReliablePing", (RmiID)64001);
		}

		public bool P2P_NotifyDirectP2PDisconnected(HostID remote, RmiContext rmiContext, HostID remotePeerHostID, ErrorType reason)
		{
			Message message = new Message();
			RmiID b = (RmiID)64002;
			message.Write(b);
			Marshaler.Write(message, remotePeerHostID);
			Marshaler.Write(message, reason);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "P2P_NotifyDirectP2PDisconnected", (RmiID)64002);
		}

		public bool P2P_NotifyDirectP2PDisconnected(HostID[] remotes, RmiContext rmiContext, HostID remotePeerHostID, ErrorType reason)
		{
			Message message = new Message();
			RmiID b = (RmiID)64002;
			message.Write(b);
			Marshaler.Write(message, remotePeerHostID);
			Marshaler.Write(message, reason);
			return RmiSend(remotes, rmiContext, message, "P2P_NotifyDirectP2PDisconnected", (RmiID)64002);
		}

		public bool NotifyUdpToTcpFallbackByClient(HostID remote, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)64003;
			message.Write(b);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "NotifyUdpToTcpFallbackByClient", (RmiID)64003);
		}

		public bool NotifyUdpToTcpFallbackByClient(HostID[] remotes, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)64003;
			message.Write(b);
			return RmiSend(remotes, rmiContext, message, "NotifyUdpToTcpFallbackByClient", (RmiID)64003);
		}

		public bool P2PGroup_MemberJoin_Ack(HostID remote, RmiContext rmiContext, HostID groupHostID, HostID addedMemberHostID, int eventID, bool localPortReuseSuccess)
		{
			Message message = new Message();
			RmiID b = (RmiID)64004;
			message.Write(b);
			Marshaler.Write(message, groupHostID);
			Marshaler.Write(message, addedMemberHostID);
			Marshaler.Write(message, eventID);
			Marshaler.Write(message, localPortReuseSuccess);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "P2PGroup_MemberJoin_Ack", (RmiID)64004);
		}

		public bool P2PGroup_MemberJoin_Ack(HostID[] remotes, RmiContext rmiContext, HostID groupHostID, HostID addedMemberHostID, int eventID, bool localPortReuseSuccess)
		{
			Message message = new Message();
			RmiID b = (RmiID)64004;
			message.Write(b);
			Marshaler.Write(message, groupHostID);
			Marshaler.Write(message, addedMemberHostID);
			Marshaler.Write(message, eventID);
			Marshaler.Write(message, localPortReuseSuccess);
			return RmiSend(remotes, rmiContext, message, "P2PGroup_MemberJoin_Ack", (RmiID)64004);
		}

		public bool NotifyP2PHolepunchSuccess(HostID remote, RmiContext rmiContext, HostID A, HostID B, IPEndPoint ABSendAddr, IPEndPoint ABRecvAddr, IPEndPoint BASendAddr, IPEndPoint BARecvAddr)
		{
			Message message = new Message();
			RmiID b = (RmiID)64005;
			message.Write(b);
			Marshaler.Write(message, A);
			Marshaler.Write(message, B);
			Marshaler.Write(message, ABSendAddr);
			Marshaler.Write(message, ABRecvAddr);
			Marshaler.Write(message, BASendAddr);
			Marshaler.Write(message, BARecvAddr);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "NotifyP2PHolepunchSuccess", (RmiID)64005);
		}

		public bool NotifyP2PHolepunchSuccess(HostID[] remotes, RmiContext rmiContext, HostID A, HostID B, IPEndPoint ABSendAddr, IPEndPoint ABRecvAddr, IPEndPoint BASendAddr, IPEndPoint BARecvAddr)
		{
			Message message = new Message();
			RmiID b = (RmiID)64005;
			message.Write(b);
			Marshaler.Write(message, A);
			Marshaler.Write(message, B);
			Marshaler.Write(message, ABSendAddr);
			Marshaler.Write(message, ABRecvAddr);
			Marshaler.Write(message, BASendAddr);
			Marshaler.Write(message, BARecvAddr);
			return RmiSend(remotes, rmiContext, message, "NotifyP2PHolepunchSuccess", (RmiID)64005);
		}

		public bool ShutdownTcp(HostID remote, RmiContext rmiContext, ByteArray comment)
		{
			Message message = new Message();
			RmiID b = (RmiID)64006;
			message.Write(b);
			Marshaler.Write(message, comment);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "ShutdownTcp", (RmiID)64006);
		}

		public bool ShutdownTcp(HostID[] remotes, RmiContext rmiContext, ByteArray comment)
		{
			Message message = new Message();
			RmiID b = (RmiID)64006;
			message.Write(b);
			Marshaler.Write(message, comment);
			return RmiSend(remotes, rmiContext, message, "ShutdownTcp", (RmiID)64006);
		}

		public bool ShutdownTcpHandshake(HostID remote, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)64007;
			message.Write(b);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "ShutdownTcpHandshake", (RmiID)64007);
		}

		public bool ShutdownTcpHandshake(HostID[] remotes, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)64007;
			message.Write(b);
			return RmiSend(remotes, rmiContext, message, "ShutdownTcpHandshake", (RmiID)64007);
		}

		public bool NotifyLog(HostID remote, RmiContext rmiContext, TraceID TID, string text)
		{
			Message message = new Message();
			RmiID b = (RmiID)64008;
			message.Write(b);
			Marshaler.Write(message, TID);
			Marshaler.Write(message, text);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "NotifyLog", (RmiID)64008);
		}

		public bool NotifyLog(HostID[] remotes, RmiContext rmiContext, TraceID TID, string text)
		{
			Message message = new Message();
			RmiID b = (RmiID)64008;
			message.Write(b);
			Marshaler.Write(message, TID);
			Marshaler.Write(message, text);
			return RmiSend(remotes, rmiContext, message, "NotifyLog", (RmiID)64008);
		}

		public bool NotifyLogHolepunchFreqFail(HostID remote, RmiContext rmiContext, int rank, string text)
		{
			Message message = new Message();
			RmiID b = (RmiID)64009;
			message.Write(b);
			Marshaler.Write(message, rank);
			Marshaler.Write(message, text);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "NotifyLogHolepunchFreqFail", (RmiID)64009);
		}

		public bool NotifyLogHolepunchFreqFail(HostID[] remotes, RmiContext rmiContext, int rank, string text)
		{
			Message message = new Message();
			RmiID b = (RmiID)64009;
			message.Write(b);
			Marshaler.Write(message, rank);
			Marshaler.Write(message, text);
			return RmiSend(remotes, rmiContext, message, "NotifyLogHolepunchFreqFail", (RmiID)64009);
		}

		public bool NotifyNatDeviceName(HostID remote, RmiContext rmiContext, string deviceName)
		{
			Message message = new Message();
			RmiID b = (RmiID)64010;
			message.Write(b);
			Marshaler.Write(message, deviceName);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "NotifyNatDeviceName", (RmiID)64010);
		}

		public bool NotifyNatDeviceName(HostID[] remotes, RmiContext rmiContext, string deviceName)
		{
			Message message = new Message();
			RmiID b = (RmiID)64010;
			message.Write(b);
			Marshaler.Write(message, deviceName);
			return RmiSend(remotes, rmiContext, message, "NotifyNatDeviceName", (RmiID)64010);
		}

		public bool NotifyPeerUdpSocketRestored(HostID remote, RmiContext rmiContext, HostID peerB_ID)
		{
			Message message = new Message();
			RmiID b = (RmiID)64011;
			message.Write(b);
			Marshaler.Write(message, peerB_ID);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "NotifyPeerUdpSocketRestored", (RmiID)64011);
		}

		public bool NotifyPeerUdpSocketRestored(HostID[] remotes, RmiContext rmiContext, HostID peerB_ID)
		{
			Message message = new Message();
			RmiID b = (RmiID)64011;
			message.Write(b);
			Marshaler.Write(message, peerB_ID);
			return RmiSend(remotes, rmiContext, message, "NotifyPeerUdpSocketRestored", (RmiID)64011);
		}

		public bool NotifyJitDirectP2PTriggered(HostID remote, RmiContext rmiContext, HostID peerB_ID)
		{
			Message message = new Message();
			RmiID b = (RmiID)64012;
			message.Write(b);
			Marshaler.Write(message, peerB_ID);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "NotifyJitDirectP2PTriggered", (RmiID)64012);
		}

		public bool NotifyJitDirectP2PTriggered(HostID[] remotes, RmiContext rmiContext, HostID peerB_ID)
		{
			Message message = new Message();
			RmiID b = (RmiID)64012;
			message.Write(b);
			Marshaler.Write(message, peerB_ID);
			return RmiSend(remotes, rmiContext, message, "NotifyJitDirectP2PTriggered", (RmiID)64012);
		}

		public bool NotifyNatDeviceNameDetected(HostID remote, RmiContext rmiContext, string natDeviceName)
		{
			Message message = new Message();
			RmiID b = (RmiID)64013;
			message.Write(b);
			Marshaler.Write(message, natDeviceName);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "NotifyNatDeviceNameDetected", (RmiID)64013);
		}

		public bool NotifyNatDeviceNameDetected(HostID[] remotes, RmiContext rmiContext, string natDeviceName)
		{
			Message message = new Message();
			RmiID b = (RmiID)64013;
			message.Write(b);
			Marshaler.Write(message, natDeviceName);
			return RmiSend(remotes, rmiContext, message, "NotifyNatDeviceNameDetected", (RmiID)64013);
		}

		public bool NotifySendSpeed(HostID remote, RmiContext rmiContext, double speed)
		{
			Message message = new Message();
			RmiID b = (RmiID)64014;
			message.Write(b);
			Marshaler.Write(message, speed);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "NotifySendSpeed", (RmiID)64014);
		}

		public bool NotifySendSpeed(HostID[] remotes, RmiContext rmiContext, double speed)
		{
			Message message = new Message();
			RmiID b = (RmiID)64014;
			message.Write(b);
			Marshaler.Write(message, speed);
			return RmiSend(remotes, rmiContext, message, "NotifySendSpeed", (RmiID)64014);
		}

		public bool ReportP2PPeerPing(HostID remote, RmiContext rmiContext, HostID peerID, int recentPing)
		{
			Message message = new Message();
			RmiID b = (RmiID)64015;
			message.Write(b);
			Marshaler.Write(message, peerID);
			Marshaler.Write(message, recentPing);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "ReportP2PPeerPing", (RmiID)64015);
		}

		public bool ReportP2PPeerPing(HostID[] remotes, RmiContext rmiContext, HostID peerID, int recentPing)
		{
			Message message = new Message();
			RmiID b = (RmiID)64015;
			message.Write(b);
			Marshaler.Write(message, peerID);
			Marshaler.Write(message, recentPing);
			return RmiSend(remotes, rmiContext, message, "ReportP2PPeerPing", (RmiID)64015);
		}

		public bool C2S_RequestCreateUdpSocket(HostID remote, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)64016;
			message.Write(b);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "C2S_RequestCreateUdpSocket", (RmiID)64016);
		}

		public bool C2S_RequestCreateUdpSocket(HostID[] remotes, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)64016;
			message.Write(b);
			return RmiSend(remotes, rmiContext, message, "C2S_RequestCreateUdpSocket", (RmiID)64016);
		}

		public bool C2S_CreateUdpSocketAck(HostID remote, RmiContext rmiContext, bool succeed)
		{
			Message message = new Message();
			RmiID b = (RmiID)64017;
			message.Write(b);
			Marshaler.Write(message, succeed);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "C2S_CreateUdpSocketAck", (RmiID)64017);
		}

		public bool C2S_CreateUdpSocketAck(HostID[] remotes, RmiContext rmiContext, bool succeed)
		{
			Message message = new Message();
			RmiID b = (RmiID)64017;
			message.Write(b);
			Marshaler.Write(message, succeed);
			return RmiSend(remotes, rmiContext, message, "C2S_CreateUdpSocketAck", (RmiID)64017);
		}

		public bool ReportC2CUdpMessageCount(HostID remote, RmiContext rmiContext, HostID peer, int udpMessageTrialCount, int udpMessageSuccessCount)
		{
			Message message = new Message();
			RmiID b = (RmiID)64018;
			message.Write(b);
			Marshaler.Write(message, peer);
			Marshaler.Write(message, udpMessageTrialCount);
			Marshaler.Write(message, udpMessageSuccessCount);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "ReportC2CUdpMessageCount", (RmiID)64018);
		}

		public bool ReportC2CUdpMessageCount(HostID[] remotes, RmiContext rmiContext, HostID peer, int udpMessageTrialCount, int udpMessageSuccessCount)
		{
			Message message = new Message();
			RmiID b = (RmiID)64018;
			message.Write(b);
			Marshaler.Write(message, peer);
			Marshaler.Write(message, udpMessageTrialCount);
			Marshaler.Write(message, udpMessageSuccessCount);
			return RmiSend(remotes, rmiContext, message, "ReportC2CUdpMessageCount", (RmiID)64018);
		}

		public bool ReportC2SUdpMessageTrialCount(HostID remote, RmiContext rmiContext, int toServerUdpTrialCount)
		{
			Message message = new Message();
			RmiID b = (RmiID)64019;
			message.Write(b);
			Marshaler.Write(message, toServerUdpTrialCount);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "ReportC2SUdpMessageTrialCount", (RmiID)64019);
		}

		public bool ReportC2SUdpMessageTrialCount(HostID[] remotes, RmiContext rmiContext, int toServerUdpTrialCount)
		{
			Message message = new Message();
			RmiID b = (RmiID)64019;
			message.Write(b);
			Marshaler.Write(message, toServerUdpTrialCount);
			return RmiSend(remotes, rmiContext, message, "ReportC2SUdpMessageTrialCount", (RmiID)64019);
		}

		public override RmiID[] GetRmiIDList()
		{
			return Common.RmiIDList;
		}
	}
}

using Nettention.Proud;

namespace ProudC2S
{
	internal class Common
	{
		public const RmiID ReliablePing = (RmiID)64001;

		public const RmiID P2P_NotifyDirectP2PDisconnected = (RmiID)64002;

		public const RmiID NotifyUdpToTcpFallbackByClient = (RmiID)64003;

		public const RmiID P2PGroup_MemberJoin_Ack = (RmiID)64004;

		public const RmiID NotifyP2PHolepunchSuccess = (RmiID)64005;

		public const RmiID ShutdownTcp = (RmiID)64006;

		public const RmiID ShutdownTcpHandshake = (RmiID)64007;

		public const RmiID NotifyLog = (RmiID)64008;

		public const RmiID NotifyLogHolepunchFreqFail = (RmiID)64009;

		public const RmiID NotifyNatDeviceName = (RmiID)64010;

		public const RmiID NotifyPeerUdpSocketRestored = (RmiID)64011;

		public const RmiID NotifyJitDirectP2PTriggered = (RmiID)64012;

		public const RmiID NotifyNatDeviceNameDetected = (RmiID)64013;

		public const RmiID NotifySendSpeed = (RmiID)64014;

		public const RmiID ReportP2PPeerPing = (RmiID)64015;

		public const RmiID C2S_RequestCreateUdpSocket = (RmiID)64016;

		public const RmiID C2S_CreateUdpSocketAck = (RmiID)64017;

		public const RmiID ReportC2CUdpMessageCount = (RmiID)64018;

		public const RmiID ReportC2SUdpMessageTrialCount = (RmiID)64019;

		public static RmiID[] RmiIDList = new RmiID[19]
		{
			(RmiID)64001,
			(RmiID)64002,
			(RmiID)64003,
			(RmiID)64004,
			(RmiID)64005,
			(RmiID)64006,
			(RmiID)64007,
			(RmiID)64008,
			(RmiID)64009,
			(RmiID)64010,
			(RmiID)64011,
			(RmiID)64012,
			(RmiID)64013,
			(RmiID)64014,
			(RmiID)64015,
			(RmiID)64016,
			(RmiID)64017,
			(RmiID)64018,
			(RmiID)64019
		};
	}
}

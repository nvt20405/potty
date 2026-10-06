using Nettention.Proud;

namespace ProudS2C
{
	internal class Common
	{
		public const RmiID P2PGroup_MemberJoin = (RmiID)63001;

		public const RmiID P2PGroup_MemberJoin_Unencrypted = (RmiID)63002;

		public const RmiID P2PRecycleComplete = (RmiID)63003;

		public const RmiID RequestP2PHolepunch = (RmiID)63004;

		public const RmiID P2P_NotifyDirectP2PDisconnected2 = (RmiID)63005;

		public const RmiID P2PGroup_MemberLeave = (RmiID)63006;

		public const RmiID NotifyDirectP2PEstablish = (RmiID)63007;

		public const RmiID ReliablePong = (RmiID)63008;

		public const RmiID EnableLog = (RmiID)63009;

		public const RmiID DisableLog = (RmiID)63010;

		public const RmiID NotifyUdpToTcpFallbackByServer = (RmiID)63011;

		public const RmiID NotifySpeedHackDetectorEnabled = (RmiID)63012;

		public const RmiID ShutdownTcpAck = (RmiID)63013;

		public const RmiID RequestAutoPrune = (RmiID)63014;

		public const RmiID RenewP2PConnectionState = (RmiID)63015;

		public const RmiID NewDirectP2PConnection = (RmiID)63016;

		public const RmiID RequestMeasureSendSpeed = (RmiID)63017;

		public const RmiID S2C_RequestCreateUdpSocket = (RmiID)63018;

		public const RmiID S2C_CreateUdpSocketAck = (RmiID)63019;

		public static RmiID[] RmiIDList = new RmiID[19]
		{
			(RmiID)63001,
			(RmiID)63002,
			(RmiID)63003,
			(RmiID)63004,
			(RmiID)63005,
			(RmiID)63006,
			(RmiID)63007,
			(RmiID)63008,
			(RmiID)63009,
			(RmiID)63010,
			(RmiID)63011,
			(RmiID)63012,
			(RmiID)63013,
			(RmiID)63014,
			(RmiID)63015,
			(RmiID)63016,
			(RmiID)63017,
			(RmiID)63018,
			(RmiID)63019
		};
	}
}

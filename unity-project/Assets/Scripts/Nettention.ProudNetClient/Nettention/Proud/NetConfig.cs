namespace Nettention.Proud
{
	public class NetConfig
	{
		public static object writeLockState = new object();

		public static int DefaultMaxDirectP2PMulticastCount = int.MaxValue;

		public static ErrorReaction UserMisuseErrorReaction = ErrorReaction.Assert;

		public static readonly bool EnableTestSplitter = false;

		public static readonly int MessageMinLength = 128;

		public static int MessageMaxLength = 1048576;

		public static bool FraggingOnNeedByDefault = true;

		public static long UnreliablePingIntervalMs = 4300L;

		public static long ReliablePingIntervalMs = 4300L;

		public static long EveryRemoteIssueSendOnNeedIntervalMs = 1L;

		public static long EveryRemoteRecvOnNeedIntervalMs = 1L;

		public static int InternalNetVersion = 197015;

		public static int StreamGrowBy = 1024;

		public static bool EnableSendBrake = false;

		public static long UdpPacketBoardLongIntervalMs = 1000L;

		public static long RecentAssemblyingPacketIDsClearIntervalMs = 3000L;

		public static int MinSendSpeed = 5120;

		public static int MtuLength = 1300;

		public static int TcpIssueRecvLength = 30720;

		public static int TcpRecvBufferLength = 51200;

		public static int UdpIssueRecvLength = MtuLength * 2;

		public static int UdpRecvBufferLength_Client = 409600;

		public static int TcpSendBufferLength = 51200;

		public static int UdpSendBufferLength_Client = 409600;

		public static bool EnableSocketTcpKeepAliveOption = false;

		public static bool EnableMessagePriority = true;

		public static long RemoveTooOldUdpSendPacketQueueTimeoutMs = 180000L;

		public static long AssembleFraggedPacketTimeoutMs = 10000L;

		public static int MessageMaxLengthOrdinaryCase = 65536;

		public static DirectP2PStartCondition DefaultDirectP2PStartCondition = DirectP2PStartCondition.Jit;

		public static int DefaultOverSendSuspectingThresholdInBytes = 15360;

		public static bool UpnpDetectNatDeviceByDefault = true;

		public static bool UpnpTcpAddrPortMappingByDefault = false;

		public static int ShotgunTrialCount = 3;

		public static long InfiniteCoolTimeMs = long.MaxValue;

		public static long UdpHolepunchIntervalMs = 7300L;

		public static long ServerHolepunchIntervalMs = UdpHolepunchIntervalMs;

		public static int P2PShotgunStartTurn = 10;

		public static int P2PHolepunchMaxTurnCount = 30;

		public static long P2PHolepunchIntervalMs = UdpHolepunchIntervalMs;

		public static int ShotgunRange = 0;

		public static long ReliableUdpHeartbeatIntervalMs = 5L;

		public static long ReportServerTimeAndPingIntervalMs = 3000L;

		public static long SendQueueHeavyWarningCheckCoolTimeMs = 2000L;

		public static int SendQueueHeavyWarningCapacity = 10240000;

		public static long SendQueueHeavyWarningTimeMs = 10000L;

		public static int ServerUdpRepunchMaxTrialCount = 1;

		public static long ServerUdpRepunchIntervalMs = ServerHolepunchIntervalMs * 3;

		public static long TcpSocketConnectTimeoutMs = 10000L;

		public static long RecyclePairReuseTimeMs = 10000L;

		public static int LagLinearProgrammingFactorPercent = 80;

		public static bool UseReportRealUdpCount = true;

		public static long ReportRealUdpCountIntervalMs = 10000L;

		public static bool EnableSpeedHackDetectorByDefault = true;

		public static long SpeedHackDetectorPingIntervalMs = 590L;

		public static long ReportP2PPeerPingTestIntervalMs = 3000L;

		public static uint ClientHeartbeatIntervalMs = 2u;

		public static long DefaultGracefulDisconnectTimeoutMs = 2000L;

		public static bool ForceCompressedRelayDestListOnly = false;

		public static long UpdateNetClientStatCloneCoolTimeMs = 3000L;

		public static bool UseIsSameLanToLocalForMaxDirectP2PMulticast = true;

		public static long FallbackServerUdpToTcpTimeoutMs
		{
			get
			{
				return UnreliablePingIntervalMs * 4;
			}
		}

		public static long FallbackP2PUdpToTcpTimeoutMs
		{
			get
			{
				return UnreliablePingIntervalMs * 4;
			}
		}

		public static int DefaultNoPingTimeoutTimeMs
		{
			get
			{
				return 60000;
			}
		}

		public static long P2PHolepunchEndTimeMs
		{
			get
			{
				return P2PHolepunchIntervalMs * P2PHolepunchMaxTurnCount;
			}
		}
	}
}

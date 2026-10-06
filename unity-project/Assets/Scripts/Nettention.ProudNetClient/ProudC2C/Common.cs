using Nettention.Proud;

namespace ProudC2C
{
	internal class Common
	{
		public const RmiID HolsterP2PHolepunchTrial = (RmiID)65001;

		public const RmiID ReportUdpMessageCount = (RmiID)65002;

		public const RmiID ReportServerTimeAndFrameRateAndPing = (RmiID)65003;

		public const RmiID ReportServerTimeAndFrameRateAndPong = (RmiID)65004;

		public static RmiID[] RmiIDList = new RmiID[4]
		{
			(RmiID)65001,
			(RmiID)65002,
			(RmiID)65003,
			(RmiID)65004
		};
	}
}

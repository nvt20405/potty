using Nettention.Proud;

namespace ProudC2C
{
	internal class Proxy : RmiProxy
	{
		private const string RmiName_HolsterP2PHolepunchTrial = "HolsterP2PHolepunchTrial";

		private const string RmiName_First = "HolsterP2PHolepunchTrial";

		private const string RmiName_ReportUdpMessageCount = "ReportUdpMessageCount";

		private const string RmiName_ReportServerTimeAndFrameRateAndPing = "ReportServerTimeAndFrameRateAndPing";

		private const string RmiName_ReportServerTimeAndFrameRateAndPong = "ReportServerTimeAndFrameRateAndPong";

		public bool HolsterP2PHolepunchTrial(HostID remote, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)65001;
			message.Write(b);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "HolsterP2PHolepunchTrial", (RmiID)65001);
		}

		public bool HolsterP2PHolepunchTrial(HostID[] remotes, RmiContext rmiContext)
		{
			Message message = new Message();
			RmiID b = (RmiID)65001;
			message.Write(b);
			return RmiSend(remotes, rmiContext, message, "HolsterP2PHolepunchTrial", (RmiID)65001);
		}

		public bool ReportUdpMessageCount(HostID remote, RmiContext rmiContext, int udpSuccessCount)
		{
			Message message = new Message();
			RmiID b = (RmiID)65002;
			message.Write(b);
			Marshaler.Write(message, udpSuccessCount);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "ReportUdpMessageCount", (RmiID)65002);
		}

		public bool ReportUdpMessageCount(HostID[] remotes, RmiContext rmiContext, int udpSuccessCount)
		{
			Message message = new Message();
			RmiID b = (RmiID)65002;
			message.Write(b);
			Marshaler.Write(message, udpSuccessCount);
			return RmiSend(remotes, rmiContext, message, "ReportUdpMessageCount", (RmiID)65002);
		}

		public bool ReportServerTimeAndFrameRateAndPing(HostID remote, RmiContext rmiContext, long clientLocalTime, double recentFrameRate)
		{
			Message message = new Message();
			RmiID b = (RmiID)65003;
			message.Write(b);
			Marshaler.Write(message, clientLocalTime);
			Marshaler.Write(message, recentFrameRate);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "ReportServerTimeAndFrameRateAndPing", (RmiID)65003);
		}

		public bool ReportServerTimeAndFrameRateAndPing(HostID[] remotes, RmiContext rmiContext, long clientLocalTime, double recentFrameRate)
		{
			Message message = new Message();
			RmiID b = (RmiID)65003;
			message.Write(b);
			Marshaler.Write(message, clientLocalTime);
			Marshaler.Write(message, recentFrameRate);
			return RmiSend(remotes, rmiContext, message, "ReportServerTimeAndFrameRateAndPing", (RmiID)65003);
		}

		public bool ReportServerTimeAndFrameRateAndPong(HostID remote, RmiContext rmiContext, long clientOldLocalTime, long serverLocalTime, int serverUdpRecentPing, double recentFrameRate, int CSPacketLossPercent)
		{
			Message message = new Message();
			RmiID b = (RmiID)65004;
			message.Write(b);
			Marshaler.Write(message, clientOldLocalTime);
			Marshaler.Write(message, serverLocalTime);
			Marshaler.Write(message, serverUdpRecentPing);
			Marshaler.Write(message, recentFrameRate);
			Marshaler.Write(message, CSPacketLossPercent);
			return RmiSend(new HostID[1] { remote }, rmiContext, message, "ReportServerTimeAndFrameRateAndPong", (RmiID)65004);
		}

		public bool ReportServerTimeAndFrameRateAndPong(HostID[] remotes, RmiContext rmiContext, long clientOldLocalTime, long serverLocalTime, int serverUdpRecentPing, double recentFrameRate, int CSPacketLossPercent)
		{
			Message message = new Message();
			RmiID b = (RmiID)65004;
			message.Write(b);
			Marshaler.Write(message, clientOldLocalTime);
			Marshaler.Write(message, serverLocalTime);
			Marshaler.Write(message, serverUdpRecentPing);
			Marshaler.Write(message, recentFrameRate);
			Marshaler.Write(message, CSPacketLossPercent);
			return RmiSend(remotes, rmiContext, message, "ReportServerTimeAndFrameRateAndPong", (RmiID)65004);
		}

		public override RmiID[] GetRmiIDList()
		{
			return Common.RmiIDList;
		}
	}
}

using Nettention.Proud;

namespace ProudC2C
{
	internal class Stub : RmiStub
	{
		public delegate bool HolsterP2PHolepunchTrialDelegate(HostID remote, RmiContext rmiContext);

		public delegate bool ReportUdpMessageCountDelegate(HostID remote, RmiContext rmiContext, int udpSuccessCount);

		public delegate bool ReportServerTimeAndFrameRateAndPingDelegate(HostID remote, RmiContext rmiContext, long clientLocalTime, double recentFrameRate);

		public delegate bool ReportServerTimeAndFrameRateAndPongDelegate(HostID remote, RmiContext rmiContext, long clientOldLocalTime, long serverLocalTime, int serverUdpRecentPing, double recentFrameRate, int CSPacketLossPercent);

		private const string RmiName_HolsterP2PHolepunchTrial = "HolsterP2PHolepunchTrial";

		private const string RmiName_First = "HolsterP2PHolepunchTrial";

		private const string RmiName_ReportUdpMessageCount = "ReportUdpMessageCount";

		private const string RmiName_ReportServerTimeAndFrameRateAndPing = "ReportServerTimeAndFrameRateAndPing";

		private const string RmiName_ReportServerTimeAndFrameRateAndPong = "ReportServerTimeAndFrameRateAndPong";

		public HolsterP2PHolepunchTrialDelegate HolsterP2PHolepunchTrial = (HostID remote, RmiContext rmiContext) => false;

		public ReportUdpMessageCountDelegate ReportUdpMessageCount = (HostID remote, RmiContext rmiContext, int udpSuccessCount) => false;

		public ReportServerTimeAndFrameRateAndPingDelegate ReportServerTimeAndFrameRateAndPing = (HostID remote, RmiContext rmiContext, long clientLocalTime, double recentFrameRate) => false;

		public ReportServerTimeAndFrameRateAndPongDelegate ReportServerTimeAndFrameRateAndPong = (HostID remote, RmiContext rmiContext, long clientOldLocalTime, long serverLocalTime, int serverUdpRecentPing, double recentFrameRate, int CSPacketLossPercent) => false;

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
				case (RmiID)65001:
				{
					RmiContext rmiContext2 = new RmiContext();
					rmiContext2.sentFrom = pa.RemoteHostID;
					rmiContext2.relayed = pa.IsRelayed;
					rmiContext2.hostTag = hostTag;
					core.PostCheckReadMessage(readOnlyMessage, "HolsterP2PHolepunchTrial");
					if (enableNotifyCallFromStub)
					{
						string parameters = "";
						NotifyCallFromStub((RmiID)65001, "HolsterP2PHolepunchTrial", parameters);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)65001,
							rmiName = "HolsterP2PHolepunchTrial",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs2 = PreciseCurrentTime.GetTimeMs();
					if (!HolsterP2PHolepunchTrial(remoteHostID, rmiContext2))
					{
						core.ShowNotImplementedRmiWarning("HolsterP2PHolepunchTrial");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)65001,
							rmiName = "HolsterP2PHolepunchTrial",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs2
						});
					}
					goto IL_05a2;
				}
				case (RmiID)65002:
				{
					RmiContext rmiContext3 = new RmiContext();
					rmiContext3.sentFrom = pa.RemoteHostID;
					rmiContext3.relayed = pa.IsRelayed;
					rmiContext3.hostTag = hostTag;
					int b7;
					Marshaler.Read(readOnlyMessage, out b7);
					core.PostCheckReadMessage(readOnlyMessage, "ReportUdpMessageCount");
					if (enableNotifyCallFromStub)
					{
						string text2 = "";
						text2 = text2 + b7 + ",";
						NotifyCallFromStub((RmiID)65002, "ReportUdpMessageCount", text2);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)65002,
							rmiName = "ReportUdpMessageCount",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs3 = PreciseCurrentTime.GetTimeMs();
					if (!ReportUdpMessageCount(remoteHostID, rmiContext3, b7))
					{
						core.ShowNotImplementedRmiWarning("ReportUdpMessageCount");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)65002,
							rmiName = "ReportUdpMessageCount",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs3
						});
					}
					goto IL_05a2;
				}
				case (RmiID)65003:
				{
					RmiContext rmiContext4 = new RmiContext();
					rmiContext4.sentFrom = pa.RemoteHostID;
					rmiContext4.relayed = pa.IsRelayed;
					rmiContext4.hostTag = hostTag;
					long b8;
					Marshaler.Read(readOnlyMessage, out b8);
					double b9;
					Marshaler.Read(readOnlyMessage, out b9);
					core.PostCheckReadMessage(readOnlyMessage, "ReportServerTimeAndFrameRateAndPing");
					if (enableNotifyCallFromStub)
					{
						string text3 = "";
						text3 = text3 + b8 + ",";
						text3 = text3 + b9 + ",";
						NotifyCallFromStub((RmiID)65003, "ReportServerTimeAndFrameRateAndPing", text3);
					}
					if (enableStubProfiling)
					{
						BeforeRmiInvocation(new BeforeRmiSummary
						{
							rmiID = (RmiID)65003,
							rmiName = "ReportServerTimeAndFrameRateAndPing",
							hostID = remoteHostID,
							hostTag = hostTag
						});
					}
					long timeMs4 = PreciseCurrentTime.GetTimeMs();
					if (!ReportServerTimeAndFrameRateAndPing(remoteHostID, rmiContext4, b8, b9))
					{
						core.ShowNotImplementedRmiWarning("ReportServerTimeAndFrameRateAndPing");
					}
					if (enableStubProfiling)
					{
						AfterRmiInvocation(new AfterRmiSummary
						{
							rmiID = (RmiID)65003,
							rmiName = "ReportServerTimeAndFrameRateAndPing",
							hostID = remoteHostID,
							hostTag = hostTag,
							elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs4
						});
					}
					goto IL_05a2;
				}
				case (RmiID)65004:
					{
						RmiContext rmiContext = new RmiContext();
						rmiContext.sentFrom = pa.RemoteHostID;
						rmiContext.relayed = pa.IsRelayed;
						rmiContext.hostTag = hostTag;
						long b2;
						Marshaler.Read(readOnlyMessage, out b2);
						long b3;
						Marshaler.Read(readOnlyMessage, out b3);
						int b4;
						Marshaler.Read(readOnlyMessage, out b4);
						double b5;
						Marshaler.Read(readOnlyMessage, out b5);
						int b6;
						Marshaler.Read(readOnlyMessage, out b6);
						core.PostCheckReadMessage(readOnlyMessage, "ReportServerTimeAndFrameRateAndPong");
						if (enableNotifyCallFromStub)
						{
							string text = "";
							text = text + b2 + ",";
							text = text + b3 + ",";
							text = text + b4 + ",";
							text = text + b5 + ",";
							text = text + b6 + ",";
							NotifyCallFromStub((RmiID)65004, "ReportServerTimeAndFrameRateAndPong", text);
						}
						if (enableStubProfiling)
						{
							BeforeRmiInvocation(new BeforeRmiSummary
							{
								rmiID = (RmiID)65004,
								rmiName = "ReportServerTimeAndFrameRateAndPong",
								hostID = remoteHostID,
								hostTag = hostTag
							});
						}
						long timeMs = PreciseCurrentTime.GetTimeMs();
						if (!ReportServerTimeAndFrameRateAndPong(remoteHostID, rmiContext, b2, b3, b4, b5, b6))
						{
							core.ShowNotImplementedRmiWarning("ReportServerTimeAndFrameRateAndPong");
						}
						if (enableStubProfiling)
						{
							AfterRmiInvocation(new AfterRmiSummary
							{
								rmiID = (RmiID)65004,
								rmiName = "ReportServerTimeAndFrameRateAndPong",
								hostID = remoteHostID,
								hostTag = hostTag,
								elapsedTime = PreciseCurrentTime.GetTimeMs() - timeMs
							});
						}
						goto IL_05a2;
					}
					IL_05a2:
					return true;
				}
			}
			readOnlyMessage.ReadOffset = readOffset;
			return false;
		}
	}
}

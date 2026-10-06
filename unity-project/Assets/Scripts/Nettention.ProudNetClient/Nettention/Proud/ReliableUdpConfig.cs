using System;

namespace Nettention.Proud
{
	public class ReliableUdpConfig
	{
		public static byte FirstStreamValue = 33;

		public static int MaxRandomStreamLength = 2000;

		public static long FirstResendCoolTime = 2000L;

		public static bool IsResendCoolTimeRelatedToPing = false;

		public static int EnlargeResendCoolTimeRatio = 11;

		public static long MinResendCoolTime = 100L;

		public static long MaxResendCoolTime = 8000L;

		public static double SimulatedUDPReliabilityRatio = 0.7;

		public static int TooOldFrameNumberThreshold = 16777216;

		public static long FrameMoveInterval = 1L;

		public static long ShowUIInterval = 400L;

		public static int ReceiveSpeedBeforeUpdate = 100;

		public static long CalcRecentReceiveInterval = 1000L;

		public static long BrakeMaxSendSpeedThreshold = 8L;

		public static long StreamToSenderWindowCoalesceInterval = 300L;

		public static bool HighPriorityAckFrame = true;

		public static bool HighPriorityDataFrame = true;

		public static int ResendLimitRatio = 10;

		public static int MinResendLimitCount = 1000;

		public static int MaxResendLimitCount = 3000;

		public static int FrameLength
		{
			get
			{
				return Math.Max(1300, NetConfig.MtuLength - 100);
			}
		}

		public static int MaxAckCountInOneFrame
		{
			get
			{
				return Math.Max((FrameLength - 10) / 5, 10);
			}
		}

		public static int MaxSendSpeedInFrameCount
		{
			get
			{
				return 10485760 / FrameLength;
			}
		}
	}
}

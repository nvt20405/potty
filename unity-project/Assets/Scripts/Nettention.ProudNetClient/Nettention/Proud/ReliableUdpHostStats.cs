namespace Nettention.Proud
{
	public class ReliableUdpHostStats
	{
		public int receivedFrameCount;

		public int receivedStreamCount;

		public int totalReceivedStreamLength;

		public int totalAckFrameCount;

		public int recentReceiveSpeed;

		public FrameNumber expectedFrameNumber;

		public FrameNumber lastReceivedDataFrameNumber;

		public int sendStreamCount;

		public int firstSendFrameCount;

		public int resendFrameCount;

		public int totalSendStreamLength;

		public int totalResendCount;

		public int totalFirstSendCount;

		public int recentSendFrameToUdpSpeed;

		public int sendSpeedLimit;

		public FrameNumber firstSenderWindowLastFrame;

		public FrameNumber resendWindowLastFrame;

		public FrameNumber lastExpectedFrameNumberAtSender;

		public int totalReceiveDataCount;
	}
}

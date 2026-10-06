namespace Nettention.Proud
{
	internal class SenderFrame : ReliableUdpFrame
	{
		public long lastSendTime;

		public long resendCoolTime;

		public long firstSendTime;

		public int resendCount;
	}
}

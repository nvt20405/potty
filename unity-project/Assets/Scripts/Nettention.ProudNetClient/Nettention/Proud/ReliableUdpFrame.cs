namespace Nettention.Proud
{
	internal class ReliableUdpFrame
	{
		public ReliableUdpFrameType type;

		public FrameNumber frameNumber;

		public ByteArray data;

		public CompressedFrameNumbers ackedFrameNumbers = new CompressedFrameNumbers();

		public int recentReceiveSpeed;

		public FrameNumber expectedFrameNumber;

		public void CloneTo(ReliableUdpFrame dest)
		{
			dest.frameNumber = frameNumber;
			if (data != null)
			{
				dest.data = data.Clone();
			}
			else
			{
				dest.data = null;
			}
			dest.type = type;
			if (ackedFrameNumbers != null)
			{
				dest.ackedFrameNumbers = ackedFrameNumbers.Clone();
			}
			else
			{
				dest.ackedFrameNumbers = new CompressedFrameNumbers();
			}
			dest.recentReceiveSpeed = recentReceiveSpeed;
			dest.expectedFrameNumber = expectedFrameNumber;
		}
	}
}

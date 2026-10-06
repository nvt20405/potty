namespace Nettention.Proud
{
	public class InvalidPacketFormatErrorInfo : ErrorInfo
	{
		public ByteArray lastReceivedMessage;

		public override string ToString()
		{
			return base.ToString() + string.Format(",lastReceivedMessage:{0}", lastReceivedMessage);
		}
	}
}

namespace Nettention.Proud
{
	internal class TcpPacketCtx
	{
		public ByteArray packet = new ByteArray();

		private long uniqueID;

		public void FromSendOpt(SendOpt opt)
		{
			uniqueID = opt.uniqueID;
		}
	}
}

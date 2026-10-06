namespace Nettention.Proud
{
	internal struct FragHeader
	{
		public ushort splitterFilter;

		public int packetLength;

		public int packetID;

		public int fragmentID;
	}
}

namespace Nettention.Proud
{
	internal class DefraggingPacket
	{
		public FastArray<bool> fragFillFlagList = new FastArray<bool>();

		public ByteArray assembledData = new ByteArray();

		public int fragFilledCount;

		public long createdTime;
	}
}

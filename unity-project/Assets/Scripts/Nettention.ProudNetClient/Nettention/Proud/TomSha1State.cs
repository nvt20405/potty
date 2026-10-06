namespace Nettention.Proud
{
	internal class TomSha1State
	{
		public static readonly int BUF_SIZE = 64;

		public ulong length;

		public uint[] state = new uint[5];

		public uint currentLength;

		public byte[] buf = new byte[BUF_SIZE];
	}
}

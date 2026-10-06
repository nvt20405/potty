namespace Nettention.Proud.zlib
{
	public sealed class Zlib
	{
		public static uint compressBound(uint sourceLen)
		{
			return sourceLen + (sourceLen >> 12) + (sourceLen >> 14) + (sourceLen >> 25) + 13;
		}

		public static int ZlibCompress(byte[] dest, ref int destLen, byte[] source, int sourceLen)
		{
			int level = -1;
			ZStream zStream = new ZStream();
			zStream.next_in = source;
			zStream.avail_in = sourceLen;
			zStream.next_out = dest;
			zStream.avail_out = destLen;
			if (zStream.avail_out != destLen)
			{
				return -5;
			}
			int num = zStream.deflateInit(level);
			if (num != 0)
			{
				return num;
			}
			num = zStream.deflate(4);
			if (num == 1)
			{
				destLen = (int)zStream.total_out;
				return zStream.deflateEnd();
			}
			zStream.deflateEnd();
			if (num != 0)
			{
				return num;
			}
			return -5;
		}

		public static int ZlibUncompress(byte[] dest, ref int destLen, byte[] source, int sourceOffset, int sourceLen)
		{
			ZStream zStream = new ZStream();
			zStream.next_in = source;
			zStream.next_in_index = sourceOffset;
			zStream.avail_in = sourceLen;
			if (zStream.avail_in != sourceLen)
			{
				return -5;
			}
			zStream.next_out = dest;
			zStream.avail_out = destLen;
			if (zStream.avail_out != destLen)
			{
				return -5;
			}
			int num = zStream.inflateInit();
			if (num != 0)
			{
				return num;
			}
			num = zStream.inflate(4);
			if (num == 1)
			{
				destLen = (int)zStream.total_out;
				return zStream.inflateEnd();
			}
			zStream.inflateEnd();
			if (num == 2 || (num == -5 && zStream.avail_in == 0))
			{
				return -3;
			}
			return num;
		}
	}
}

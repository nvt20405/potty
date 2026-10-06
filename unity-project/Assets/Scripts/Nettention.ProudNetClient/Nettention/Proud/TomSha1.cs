using System;

namespace Nettention.Proud
{
	internal class TomSha1
	{
		public static readonly int BLOCK_SIZE = 64;

		public static void init(TomSha1State sha1)
		{
			sha1.state[0] = 1732584193u;
			sha1.state[1] = 4023233417u;
			sha1.state[2] = 2562383102u;
			sha1.state[3] = 271733878u;
			sha1.state[4] = 3285377520u;
			sha1.currentLength = 0u;
			sha1.length = 0uL;
		}

		public static bool process(TomSha1State sha1, byte[] msg, int msgLength)
		{
			if ((ulong)sha1.currentLength > (ulong)TomSha1State.BUF_SIZE)
			{
				return false;
			}
			uint num = (uint)msgLength;
			int num2 = 0;
			while (num != 0)
			{
				if (sha1.currentLength == 0 && (ulong)num >= (ulong)BLOCK_SIZE)
				{
					compress(sha1, msg, num2);
					sha1.length += (ulong)(BLOCK_SIZE * 8);
					num2 += BLOCK_SIZE;
					num -= (uint)BLOCK_SIZE;
					continue;
				}
				uint num3 = (uint)(((ulong)num < (ulong)(BLOCK_SIZE - sha1.currentLength)) ? num : (BLOCK_SIZE - sha1.currentLength));
				Array.Copy(msg, num2, sha1.buf, sha1.currentLength, num3);
				num2 += (int)num3;
				sha1.currentLength += num3;
				num -= num3;
				if (sha1.currentLength == BLOCK_SIZE)
				{
					compress(sha1, sha1.buf, 0);
					sha1.length += (ulong)(8 * BLOCK_SIZE);
					sha1.currentLength = 0u;
				}
			}
			return true;
		}

		public static void compress(TomSha1State sha1, byte[] buf, int pos)
		{
			uint[] array = new uint[80];
			int i;
			for (i = 0; i < 16; i++)
			{
				array[i] = (uint)(((buf[pos + 4 * i] & 0xFF) << 24) | ((buf[pos + 4 * i + 1] & 0xFF) << 16) | ((buf[pos + 4 * i + 2] & 0xFF) << 8) | (buf[pos + 4 * i + 3] & 0xFF));
			}
			uint num = sha1.state[0];
			uint num2 = sha1.state[1];
			uint num3 = sha1.state[2];
			uint num4 = sha1.state[3];
			uint num5 = sha1.state[4];
			for (i = 16; i < 80; i++)
			{
				array[i] = rol(array[i - 3] ^ array[i - 8] ^ array[i - 14] ^ array[i - 16], 1u);
			}
			i = 0;
			while (i < 20)
			{
				num5 = rol(num, 5u) + (num4 ^ (num2 & (num3 ^ num4))) + num5 + array[i++] + 1518500249;
				num2 = rol(num2, 30u);
				num4 = rol(num5, 5u) + (num3 ^ (num & (num2 ^ num3))) + num4 + array[i++] + 1518500249;
				num = rol(num, 30u);
				num3 = rol(num4, 5u) + (num2 ^ (num5 & (num ^ num2))) + num3 + array[i++] + 1518500249;
				num5 = rol(num5, 30u);
				num2 = rol(num3, 5u) + (num ^ (num4 & (num5 ^ num))) + num2 + array[i++] + 1518500249;
				num4 = rol(num4, 30u);
				num = rol(num2, 5u) + (num5 ^ (num3 & (num4 ^ num5))) + num + array[i++] + 1518500249;
				num3 = rol(num3, 30u);
			}
			while (i < 40)
			{
				num5 = rol(num, 5u) + (num2 ^ num3 ^ num4) + num5 + array[i++] + 1859775393;
				num2 = rol(num2, 30u);
				num4 = rol(num5, 5u) + (num ^ num2 ^ num3) + num4 + array[i++] + 1859775393;
				num = rol(num, 30u);
				num3 = rol(num4, 5u) + (num5 ^ num ^ num2) + num3 + array[i++] + 1859775393;
				num5 = rol(num5, 30u);
				num2 = rol(num3, 5u) + (num4 ^ num5 ^ num) + num2 + array[i++] + 1859775393;
				num4 = rol(num4, 30u);
				num = rol(num2, 5u) + (num3 ^ num4 ^ num5) + num + array[i++] + 1859775393;
				num3 = rol(num3, 30u);
			}
			while (i < 60)
			{
				num5 = rol(num, 5u) + ((num2 & num3) | (num4 & (num2 | num3))) + num5 + array[i++] + 2400959708u;
				num2 = rol(num2, 30u);
				num4 = rol(num5, 5u) + ((num & num2) | (num3 & (num | num2))) + num4 + array[i++] + 2400959708u;
				num = rol(num, 30u);
				num3 = rol(num4, 5u) + ((num5 & num) | (num2 & (num5 | num))) + num3 + array[i++] + 2400959708u;
				num5 = rol(num5, 30u);
				num2 = rol(num3, 5u) + ((num4 & num5) | (num & (num4 | num5))) + num2 + array[i++] + 2400959708u;
				num4 = rol(num4, 30u);
				num = rol(num2, 5u) + ((num3 & num4) | (num5 & (num3 | num4))) + num + array[i++] + 2400959708u;
				num3 = rol(num3, 30u);
			}
			while (i < 80)
			{
				num5 = rol(num, 5u) + (num2 ^ num3 ^ num4) + num5 + array[i++] + 3395469782u;
				num2 = rol(num2, 30u);
				num4 = rol(num5, 5u) + (num ^ num2 ^ num3) + num4 + array[i++] + 3395469782u;
				num = rol(num, 30u);
				num3 = rol(num4, 5u) + (num5 ^ num ^ num2) + num3 + array[i++] + 3395469782u;
				num5 = rol(num5, 30u);
				num2 = rol(num3, 5u) + (num4 ^ num5 ^ num) + num2 + array[i++] + 3395469782u;
				num4 = rol(num4, 30u);
				num = rol(num2, 5u) + (num3 ^ num4 ^ num5) + num + array[i++] + 3395469782u;
				num3 = rol(num3, 30u);
			}
			sha1.state[0] = sha1.state[0] + num;
			sha1.state[1] = sha1.state[1] + num2;
			sha1.state[2] = sha1.state[2] + num3;
			sha1.state[3] = sha1.state[3] + num4;
			sha1.state[4] = sha1.state[4] + num5;
		}

		public static bool done(TomSha1State sha1, byte[] outBuf)
		{
			if ((ulong)sha1.currentLength >= (ulong)TomSha1State.BUF_SIZE)
			{
				return false;
			}
			sha1.length += sha1.currentLength * 8;
			sha1.buf[(uint)(UIntPtr)(sha1.currentLength++)] = 128;
			if (sha1.currentLength > 56)
			{
				while (sha1.currentLength < 64)
				{
					sha1.buf[(uint)(UIntPtr)(sha1.currentLength++)] = 0;
				}
				compress(sha1, sha1.buf, 0);
				sha1.currentLength = 0u;
			}
			while (sha1.currentLength < 56)
			{
				sha1.buf[(uint)(UIntPtr)(sha1.currentLength++)] = 0;
			}
			sha1.buf[56] = (byte)((sha1.length >> 56) & 0xFF);
			sha1.buf[57] = (byte)((sha1.length >> 48) & 0xFF);
			sha1.buf[58] = (byte)((sha1.length >> 40) & 0xFF);
			sha1.buf[59] = (byte)((sha1.length >> 32) & 0xFF);
			sha1.buf[60] = (byte)((sha1.length >> 24) & 0xFF);
			sha1.buf[61] = (byte)((sha1.length >> 16) & 0xFF);
			sha1.buf[62] = (byte)((sha1.length >> 8) & 0xFF);
			sha1.buf[63] = (byte)(sha1.length & 0xFF);
			compress(sha1, sha1.buf, 0);
			for (int i = 0; i < 5; i++)
			{
				outBuf[4 * i] = (byte)((sha1.state[i] >> 24) & 0xFF);
				outBuf[4 * i + 1] = (byte)((sha1.state[i] >> 16) & 0xFF);
				outBuf[4 * i + 2] = (byte)((sha1.state[i] >> 8) & 0xFF);
				outBuf[4 * i + 3] = (byte)(sha1.state[i] & 0xFF);
			}
			return true;
		}

		public static uint rol(uint x, uint y)
		{
			return (x << (int)y) | (x >> (int)(32 - y));
		}
	}
}

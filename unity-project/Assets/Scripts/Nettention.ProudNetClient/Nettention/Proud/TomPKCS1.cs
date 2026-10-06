using System;

namespace Nettention.Proud
{
	internal class TomPKCS1
	{
		public static void memSet(byte[] msg, int start, int end, uint character)
		{
			for (int i = start; i < end; i++)
			{
				msg[i] = (byte)character;
			}
		}

		public static bool oaepEncode(byte[] msg, int msgLength, byte[] lParam, int lparamLength, uint modulusBitLength, byte[] outarray, ref uint outlen)
		{
			int hASH_SIZE = TomRsa.HASH_SIZE;
			int num = (int)((modulusBitLength >> 3) + (((modulusBitLength & 7) != 0) ? 1 : 0));
			if (hASH_SIZE * 2 >= num - 2 || msgLength > num - 2 * hASH_SIZE - 2)
			{
				return false;
			}
			byte[] array = new byte[num];
			TomSha1State sha = new TomSha1State();
			if (lParam != null)
			{
				TomSha1.init(sha);
				if (!TomSha1.process(sha, lParam, lparamLength))
				{
					return false;
				}
				if (!TomSha1.done(sha, array))
				{
					return false;
				}
			}
			else
			{
				TomSha1.init(sha);
				if (!TomSha1.process(sha, array, 0))
				{
					return false;
				}
				if (!TomSha1.done(sha, array))
				{
					return false;
				}
			}
			uint num2 = (uint)hASH_SIZE;
			uint num3 = (uint)(num - msgLength - hASH_SIZE * 2 - 2);
			memSet(array, (int)num2, (int)num3, 0u);
			num2 += num3;
			array[(uint)(UIntPtr)(num2++)] = 1;
			Array.Copy(msg, 0L, array, num2, msgLength);
			num2 += (uint)msgLength;
			byte[] array2 = new byte[hASH_SIZE];
			TomRandom.read(array2, hASH_SIZE);
			byte[] array3 = new byte[num];
			if (!mgf1(array2, array2.Length, array3, num - hASH_SIZE - 1))
			{
				return false;
			}
			for (num3 = 0u; (ulong)num3 < (ulong)(num - hASH_SIZE - 1); num3++)
			{
				byte[] array4 = array;
				UIntPtr uIntPtr = (UIntPtr)num3;
				array4[(uint)uIntPtr] = (byte)(array4[(uint)uIntPtr] ^ array3[(uint)(UIntPtr)num3]);
			}
			if (!mgf1(array, (int)num2, array3, hASH_SIZE))
			{
				return false;
			}
			for (num3 = 0u; (ulong)num3 < (ulong)hASH_SIZE; num3++)
			{
				byte[] array5 = array2;
				UIntPtr uIntPtr2 = (UIntPtr)num3;
				array5[(uint)uIntPtr2] = (byte)(array5[(uint)uIntPtr2] ^ array3[(uint)(UIntPtr)num3]);
			}
			num2 = 0u;
			outarray[(uint)(UIntPtr)(num2++)] = 0;
			Array.Copy(array2, 0L, outarray, num2, hASH_SIZE);
			num2 += (uint)hASH_SIZE;
			Array.Copy(array, 0L, outarray, num2, num - hASH_SIZE - 1);
			num2 += (uint)(num - hASH_SIZE - 1);
			outlen = num2;
			return true;
		}

		public static bool mgf1(byte[] seed, int seedLength, byte[] outMask, int maskLength)
		{
			int hASH_SIZE = TomRsa.HASH_SIZE;
			TomSha1State sha = new TomSha1State();
			byte[] array = new byte[hASH_SIZE];
			int num = 0;
			int num2 = 0;
			while (maskLength > 0)
			{
				array[0] = (byte)((num >> 24) & 0xFF);
				array[1] = (byte)((num >> 16) & 0xFF);
				array[2] = (byte)((num >> 8) & 0xFF);
				array[3] = (byte)(num & 0xFF);
				num++;
				TomSha1.init(sha);
				if (!TomSha1.process(sha, seed, seedLength))
				{
					return false;
				}
				if (!TomSha1.process(sha, array, 4))
				{
					return false;
				}
				if (!TomSha1.done(sha, array))
				{
					return false;
				}
				uint num3 = 0u;
				while ((ulong)num3 < (ulong)hASH_SIZE && maskLength > 0)
				{
					outMask[num2++] = array[(uint)(UIntPtr)num3];
					num3++;
					maskLength--;
				}
			}
			return true;
		}
	}
}

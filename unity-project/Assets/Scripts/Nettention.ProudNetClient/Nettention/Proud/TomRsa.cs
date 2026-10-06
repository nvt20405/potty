using System;

namespace Nettention.Proud
{
	internal class TomRsa
	{
		public static readonly int MIN_RSA_SIZE = 1024;

		public static readonly int MAX_RSA_SIZE = 4096;

		public static readonly int HASH_SIZE = 20;

		public static readonly int HASH_BLOCK_SIZE = 64;

		public static void initKey(ref TomRsaKey key)
		{
			if (key.e == null)
			{
				key.e = new TomMPInt();
			}
			if (key.d == null)
			{
				key.d = new TomMPInt();
			}
			if (key.N == null)
			{
				key.N = new TomMPInt();
			}
			if (key.p == null)
			{
				key.p = new TomMPInt();
			}
			if (key.q == null)
			{
				key.q = new TomMPInt();
			}
			if (key.qP == null)
			{
				key.qP = new TomMPInt();
			}
			if (key.dP == null)
			{
				key.dP = new TomMPInt();
			}
			if (key.dQ == null)
			{
				key.dQ = new TomMPInt();
			}
			key.e.init();
			key.d.init();
			key.N.init();
			key.p.init();
			key.q.init();
			key.qP.init();
			key.dP.init();
			key.dQ.init();
		}

		public static bool importKey(byte[] keyArray, int keyLength, ref TomRsaKey key)
		{
			if (keyArray == null)
			{
				return false;
			}
			initKey(ref key);
			if (keyLength < 2)
			{
				return false;
			}
			if (keyArray[0] != 48 && keyArray[0] != 49)
			{
				return false;
			}
			uint num = 0u;
			uint num2 = 1u;
			if (keyArray[(uint)(UIntPtr)num2] < 128)
			{
				num = keyArray[(uint)(UIntPtr)(num2++)];
			}
			else if ((keyArray[(uint)(UIntPtr)num2] & 0x80) != 0)
			{
				if (keyArray[(uint)(UIntPtr)num2] < 129 || keyArray[(uint)(UIntPtr)num2] > 131)
				{
					return false;
				}
				uint num3 = (uint)(keyArray[(uint)(UIntPtr)(num2++)] & 0x7F);
				if ((ulong)(num2 + num3) > (ulong)keyLength)
				{
					return false;
				}
				num = 0u;
				while (num3 != 0)
				{
					num = (num << 8) | keyArray[(uint)(UIntPtr)(num2++)];
					num3--;
				}
			}
			if ((ulong)(num2 + num) > (ulong)keyLength)
			{
				return false;
			}
			byte[] array = new byte[num];
			Array.Copy(keyArray, num2, array, 0L, num);
			int outLen = 0;
			if (!TomDer.decodeInteger(array, key.N, ref outLen))
			{
				return false;
			}
			byte[] array2 = new byte[array.Length - outLen];
			Array.Copy(array, outLen, array2, 0, array.Length - outLen);
			return TomDer.decodeInteger(array2, key.e, ref outLen);
		}

		public static bool encrypt(byte[] msg, int msgLength, byte[] outMsg, byte[] lParam, int lparamLength, ref TomRsaKey key)
		{
			int modulusBitLength = TomMPInt.countBits(key.N);
			uint outlen = 0u;
			if (TomPKCS1.oaepEncode(msg, msgLength, lParam, lparamLength, (uint)modulusBitLength, outMsg, ref outlen))
			{
				return exptMod(outMsg, (int)outlen, outMsg, ref key);
			}
			return false;
		}

		public static bool exptMod(byte[] msg, int msgLength, byte[] outMsg, ref TomRsaKey key)
		{
			TomMPInt tomMPInt = new TomMPInt();
			tomMPInt.init();
			if (!TomMPInt.readUnsignedBin(tomMPInt, msg, msgLength, 0u))
			{
				return false;
			}
			if (TomMPInt.cmp(key.N, tomMPInt) == TomMPInt.MP_LT)
			{
				return false;
			}
			if (!TomMPInt.exptMod(tomMPInt, key.e, key.N, tomMPInt))
			{
				return false;
			}
			uint num = (uint)TomMPInt.unsignedBinSize(key.N);
			if (TomMPInt.unsignedBinSize(tomMPInt) > TomMPInt.unsignedBinSize(key.N))
			{
				return false;
			}
			Array.Clear(outMsg, 0, (int)num);
			return TomMPInt.toUnsignedBin(tomMPInt, outMsg, (int)num - TomMPInt.unsignedBinSize(tomMPInt));
		}
	}
}

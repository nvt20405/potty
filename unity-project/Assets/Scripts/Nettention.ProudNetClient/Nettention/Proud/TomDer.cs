using System;

namespace Nettention.Proud
{
	internal class TomDer
	{
		public static bool decodeInteger(byte[] src, TomMPInt outNum, ref int outLen)
		{
			if (src.Length < 3)
			{
				return false;
			}
			uint num = 0u;
			if ((src[(uint)(UIntPtr)(num++)] & 0x1F) != 2)
			{
				return false;
			}
			uint num2 = src[(uint)(UIntPtr)(num++)];
			if ((num2 & 0x80) == 0)
			{
				if ((ulong)(num + num2) > (ulong)src.Length)
				{
					return false;
				}
				if (!TomMPInt.readUnsignedBin(outNum, src, (int)num2, num))
				{
					return false;
				}
				outLen = (int)(num + num2);
			}
			else
			{
				num2 &= 0x7F;
				if ((ulong)(num + num2) > (ulong)src.Length || num2 > 4 || num2 == 0)
				{
					return false;
				}
				uint num3 = 0u;
				while (num2 != 0)
				{
					num3 = src[(uint)(UIntPtr)(num++)] | (num3 << 8);
					num2--;
				}
				if ((ulong)(num + num3) > (ulong)src.Length)
				{
					return false;
				}
				if (!TomMPInt.readUnsignedBin(outNum, src, (int)num3, num))
				{
					return false;
				}
				outLen = (int)(num + num3);
			}
			if ((src[(uint)(UIntPtr)num] & 0x80) != 0)
			{
				TomMPInt tomMPInt = new TomMPInt();
				if (!tomMPInt.init())
				{
					return false;
				}
				if (!TomMPInt.twoExpt(tomMPInt, TomMPInt.countBits(outNum)))
				{
					tomMPInt.clear();
					return false;
				}
				if (!TomMPInt.sub(outNum, tomMPInt, outNum))
				{
					tomMPInt.clear();
					return false;
				}
				tomMPInt.clear();
			}
			return true;
		}
	}
}

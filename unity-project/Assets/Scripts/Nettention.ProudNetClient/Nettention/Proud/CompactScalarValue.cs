using System;

namespace Nettention.Proud
{
	internal class CompactScalarValue
	{
		private byte[] src;

		private int srcLength;

		private int offset;

		public byte[] filledBlock = new byte[100];

		public int filledBlockLength;

		public long extractedValue;

		public int extracteeLength;

		private void Write(sbyte a)
		{
			filledBlock[filledBlockLength] = (byte)a;
			filledBlockLength++;
		}

		private void Write(short a)
		{
			int num = 2;
			Array.Copy(BitConverter.GetBytes(a), 0, filledBlock, filledBlockLength, num);
			filledBlockLength += num;
		}

		private void Write(int a)
		{
			int num = 4;
			Array.Copy(BitConverter.GetBytes(a), 0, filledBlock, filledBlockLength, num);
			filledBlockLength += num;
		}

		private void Write(long a)
		{
			int num = 8;
			Array.Copy(BitConverter.GetBytes(a), 0, filledBlock, filledBlockLength, num);
			filledBlockLength += num;
		}

		private bool Read(out sbyte a)
		{
			a = 0;
			if (extracteeLength + 1 > srcLength - offset)
			{
				return false;
			}
			a = (sbyte)src[extracteeLength + offset];
			extracteeLength++;
			return true;
		}

		private bool Read(out short a)
		{
			a = 0;
			if (extracteeLength + 2 > srcLength - offset)
			{
				return false;
			}
			a = BitConverter.ToInt16(src, extracteeLength + offset);
			extracteeLength += 2;
			return true;
		}

		private bool Read(out int a)
		{
			a = 0;
			if (extracteeLength + 4 > srcLength - offset)
			{
				return false;
			}
			a = BitConverter.ToInt32(src, extracteeLength + offset);
			extracteeLength += 4;
			return true;
		}

		private bool Read(out long a)
		{
			a = 0L;
			if (extracteeLength + 8 > srcLength - offset)
			{
				return false;
			}
			a = BitConverter.ToInt64(src, extracteeLength + offset);
			extracteeLength += 8;
			return true;
		}

		public void MakeBlock(long src)
		{
			extractedValue = src;
			filledBlockLength = 0;
			if (-128 <= src && src <= 127)
			{
				sbyte a = 1;
				Write(a);
				sbyte a2 = (sbyte)src;
				Write(a2);
			}
			else if (-32768 <= src && src <= 32767)
			{
				sbyte a = 2;
				Write(a);
				short a3 = (short)src;
				Write(a3);
			}
			else if (int.MinValue <= src && src <= int.MaxValue)
			{
				sbyte a = 4;
				Write(a);
				int a4 = (int)src;
				Write(a4);
			}
			else
			{
				sbyte a = 8;
				Write(a);
				Write(src);
			}
		}

		internal bool ExtractValue(byte[] src, int offset, int length)
		{
			extracteeLength = 0;
			this.src = src;
			srcLength = length;
			extracteeLength = 0;
			this.offset = offset;
			sbyte a = 0;
			if (!Read(out a))
			{
				return false;
			}
			switch (a)
			{
			case 1:
			{
				sbyte a4 = 0;
				if (!Read(out a4))
				{
					return false;
				}
				extractedValue = a4;
				return true;
			}
			case 2:
			{
				short a3 = 0;
				if (!Read(out a3))
				{
					return false;
				}
				extractedValue = a3;
				return true;
			}
			case 4:
			{
				int a2 = 0;
				if (!Read(out a2))
				{
					return false;
				}
				extractedValue = a2;
				return true;
			}
			case 8:
				return Read(out extractedValue);
			default:
				return false;
			}
		}
	}
}

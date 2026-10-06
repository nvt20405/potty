using System;

namespace Nettention.Proud
{
	internal class FragHeaderUtil
	{
		public static bool IsInt8Range(int v)
		{
			if (v >= -128)
			{
				return v <= 127;
			}
			return false;
		}

		public static bool IsInt16Range(int v)
		{
			if (v >= -32768)
			{
				return v <= 32767;
			}
			return false;
		}

		public static int GetLengthFlag(int v)
		{
			if (IsInt8Range(v))
			{
				return 0;
			}
			if (IsInt16Range(v))
			{
				return 1;
			}
			return 3;
		}

		public static void WriteCompressedByFlag(Message msg, int v, int flag)
		{
			switch (flag)
			{
			case 0:
				msg.Write((sbyte)v);
				break;
			case 1:
				msg.Write((short)v);
				break;
			case 3:
				msg.Write(v);
				break;
			default:
				throw new Exception("Invalid flag in FragHeader writer!");
			}
		}

		public static bool ReadCompressedByFlag(Message msg, out int outV, int flag)
		{
			outV = 0;
			switch (flag)
			{
			case 0:
			{
				sbyte b2;
				bool result2 = msg.Read(out b2);
				outV = b2;
				return result2;
			}
			case 1:
			{
				short b;
				bool result = msg.Read(out b);
				outV = b;
				return result;
			}
			case 3:
				return msg.Read(out outV);
			default:
				return false;
			}
		}
	}
}

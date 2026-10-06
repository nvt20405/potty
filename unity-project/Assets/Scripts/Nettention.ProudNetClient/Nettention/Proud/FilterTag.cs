namespace Nettention.Proud
{
	internal class FilterTag
	{
		public static byte CreateFilterTag(HostID srcID, HostID destID)
		{
			byte b = 0;
			b |= (byte)((int)(srcID & (HostID)0xF) << 4);
			return (byte)(b | (byte)(destID & (HostID)0xF));
		}

		public static bool ShouldBeFiltered(byte filterTag, HostID srcID, HostID destID)
		{
			byte b = (byte)((filterTag & 0xF0) >> 4);
			byte b2 = (byte)(filterTag & 0xF);
			byte b3 = (byte)(srcID & (HostID)0xF);
			byte b4 = (byte)(destID & (HostID)0xF);
			if (b == 0 || b3 == 0 || b == b3)
			{
				if (b2 != 0 && b4 != 0)
				{
					return b2 != b4;
				}
				return false;
			}
			return true;
		}
	}
}

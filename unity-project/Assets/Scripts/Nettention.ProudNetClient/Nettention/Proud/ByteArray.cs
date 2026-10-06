namespace Nettention.Proud
{
	public class ByteArray : FastArray<byte>
	{
		public ByteArray()
		{
		}

		public ByteArray(byte[] inputData, int count)
		{
			base.GrowPolicy = eGrowPolicy.Normal;
			InitVars();
			AddRange(inputData, count);
		}

		public ByteArray(byte[] inputData)
		{
			AddRange(inputData);
		}

		public new ByteArray Clone()
		{
			return (ByteArray)MemberwiseClone();
		}

		public static ByteArray CopyFrom(byte[] source)
		{
			ByteArray byteArray = new ByteArray();
			byteArray.SetCount(source.Length);
			for (int i = 0; i < source.Length; i++)
			{
				byteArray.data[i] = source[i];
			}
			return byteArray;
		}
	}
}

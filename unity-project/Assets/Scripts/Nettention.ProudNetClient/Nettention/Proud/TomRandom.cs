using System;

namespace Nettention.Proud
{
	internal class TomRandom
	{
		private static Random random = new Random();

		public static void read(byte[] outarray, int size)
		{
			for (int i = 0; i < size; i++)
			{
				outarray[i] = (byte)random.Next(0, 255);
			}
		}
	}
}

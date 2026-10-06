namespace Nettention.Proud
{
	public class CryptoAesKey
	{
		internal int[,] ke = new int[CryptoAes.MAX_ROUNDS + 1, CryptoAes.MAX_BC];

		internal int[,] kd = new int[CryptoAes.MAX_ROUNDS + 1, CryptoAes.MAX_BC];

		internal int keyLength;

		internal int blockSize;

		internal int rounds;

		public int KeyLength
		{
			get
			{
				if (!KeyExists)
				{
					return -1;
				}
				return keyLength;
			}
		}

		public int BlockSize
		{
			get
			{
				if (!KeyExists)
				{
					return -1;
				}
				return blockSize;
			}
		}

		public int Rounds
		{
			get
			{
				if (!KeyExists)
				{
					return -1;
				}
				return rounds;
			}
		}

		public bool KeyExists
		{
			get
			{
				return keyLength > 0;
			}
		}

		public void Clear()
		{
			keyLength = 0;
		}
	}
}

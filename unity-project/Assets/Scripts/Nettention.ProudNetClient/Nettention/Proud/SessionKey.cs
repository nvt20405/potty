namespace Nettention.Proud
{
	internal class SessionKey
	{
		public CryptoAesKey aesKey = new CryptoAesKey();

		public CryptoRc4Key rc4Key = new CryptoRc4Key();

		public bool KeyExists
		{
			get
			{
				if (aesKey.KeyExists)
				{
					return rc4Key.KeyExists;
				}
				return false;
			}
		}

		public void Clear()
		{
			aesKey.Clear();
			rc4Key.Clear();
		}
	}
}

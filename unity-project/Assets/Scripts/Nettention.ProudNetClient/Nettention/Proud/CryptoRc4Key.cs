namespace Nettention.Proud
{
	internal class CryptoRc4Key
	{
		public ByteArray key = new ByteArray();

		public bool keyExists;

		public bool KeyExists
		{
			get
			{
				return keyExists;
			}
		}

		public void Clear()
		{
			key.Clear();
			keyExists = false;
		}
	}
}

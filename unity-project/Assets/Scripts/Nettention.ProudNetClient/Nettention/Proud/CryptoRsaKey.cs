namespace Nettention.Proud
{
	internal class CryptoRsaKey
	{
		public TomRsaKey key;

		public CryptoRsaKey()
		{
			key.e = null;
			key.d = null;
			key.N = null;
			key.p = null;
			key.q = null;
			key.qP = null;
			key.dP = null;
			key.dQ = null;
		}

		public bool FromBlob(ByteArray blob)
		{
			return TomRsa.importKey(blob.data, blob.Count, ref key);
		}
	}
}

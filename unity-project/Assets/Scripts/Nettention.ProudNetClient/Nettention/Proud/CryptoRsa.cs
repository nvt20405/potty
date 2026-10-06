using System;

namespace Nettention.Proud
{
	internal class CryptoRsa
	{
		private static Random random = new Random();

		public static bool CreateRandomBlock(ByteArray output, int length)
		{
			length /= 8;
			output.Clear();
			output.Count = length;
			for (int i = 0; i < length; i++)
			{
				output[i] = (byte)random.Next(0, 255);
			}
			return true;
		}

		public static bool EncryptSessionKeyByPublicKey(ByteArray outEncryptedSessionKey, ByteArray randomBlock, ByteArray publicKeyBlob)
		{
			CryptoRsaKey cryptoRsaKey = new CryptoRsaKey();
			if (!cryptoRsaKey.FromBlob(publicKeyBlob))
			{
				return false;
			}
			uint count = (uint)randomBlock.Count;
			uint num = (uint)(outEncryptedSessionKey.Count = TomMPInt.unsignedBinSize(cryptoRsaKey.key.N));
			if (!TomRsa.encrypt(randomBlock.data, (int)count, outEncryptedSessionKey.data, null, 0, ref cryptoRsaKey.key))
			{
				return false;
			}
			if (num > (uint)outEncryptedSessionKey.Count)
			{
				return false;
			}
			outEncryptedSessionKey.Count = (int)num;
			return true;
		}
	}
}

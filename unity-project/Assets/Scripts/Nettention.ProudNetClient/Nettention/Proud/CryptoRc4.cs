using System;

namespace Nettention.Proud
{
	internal class CryptoRc4
	{
		private static readonly int MaxKeyLength = 256;

		public static bool ExpandFrom(CryptoRc4Key outKey, byte[] inputkey, int keyLength)
		{
			if (keyLength == 0)
			{
				outKey.keyExists = true;
				return true;
			}
			if (inputkey == null)
			{
				return false;
			}
			byte[] array = new byte[MaxKeyLength];
			outKey.key.Count = keyLength;
			for (int i = 0; i < keyLength; i++)
			{
				outKey.key[i] = (byte)i;
				array[i] = inputkey[i % keyLength];
			}
			int num = 0;
			for (int j = 0; j < keyLength; j++)
			{
				num = (num + outKey.key[j] + array[j]) % keyLength;
				int index = num % keyLength;
				byte value = outKey.key[j];
				outKey.key[j] = outKey.key[index];
				outKey.key[index] = value;
			}
			outKey.keyExists = true;
			return true;
		}

		public static int GetEncryptSize(int dataLength)
		{
			return dataLength + 4;
		}

		public static bool Encrypt(CryptoRc4Key key, byte[] input, int offset, int inputLength, byte[] output)
		{
			if (!key.KeyExists)
			{
				return false;
			}
			if (key.key.Count == 0)
			{
				return false;
			}
			if (input == null || inputLength == 0)
			{
				return true;
			}
			if (output.Length < GetEncryptSize(inputLength))
			{
				return false;
			}
			Array.Copy(input, offset, output, 0, inputLength);
			uint value = Crc.Crc32(input, offset, (uint)inputLength);
			Array.Copy(BitConverter.GetBytes(value), 0, output, inputLength, 4);
			return InternalEncrypt(key, output, GetEncryptSize(inputLength));
		}

		public static bool Decrypt(CryptoRc4Key key, byte[] input, int offset, int inputLength, byte[] output, ref int outputLength)
		{
			if (!key.KeyExists)
			{
				return false;
			}
			if (key.key.Count == 0)
			{
				return false;
			}
			if (inputLength == 0)
			{
				return true;
			}
			if (outputLength < inputLength)
			{
				return false;
			}
			Array.Copy(input, offset, output, 0, inputLength);
			if (!InternalEncrypt(key, output, inputLength))
			{
				return false;
			}
			uint num = Crc.Crc32(output, 0, (uint)(inputLength - 4));
			uint num2 = BitConverter.ToUInt32(output, inputLength - 4);
			if (num2 != num)
			{
				return false;
			}
			outputLength = inputLength - 4;
			return true;
		}

		public static bool EncryptByteArray(CryptoRc4Key key, ByteArray input, ByteArray output)
		{
			int encryptSize = GetEncryptSize(input.Count);
			output.SetCount(encryptSize);
			return Encrypt(key, input.data, 0, input.Count, output.data);
		}

		public static bool DecryptByteArray(CryptoRc4Key key, ByteArray input, ByteArray output)
		{
			int outputLength = input.Count;
			output.SetCount(outputLength);
			bool flag = Decrypt(key, input.data, 0, outputLength, output.data, ref outputLength);
			if (flag)
			{
				output.SetCount(outputLength);
			}
			return flag;
		}

		public static bool EncryptMessage(CryptoRc4Key key, Message input, Message output, int offset)
		{
			if (input.Length - offset <= 0)
			{
				return false;
			}
			int encryptSize = GetEncryptSize(input.Length - offset);
			output.Length = encryptSize;
			return Encrypt(key, input.Data.data, offset, input.Length - offset, output.Data.data);
		}

		public static bool DecryptMessage(CryptoRc4Key key, Message input, Message output, int offset)
		{
			int outputLength = (output.Length = input.Length - offset);
			bool flag = Decrypt(key, input.Data.data, offset, outputLength, output.Data.data, ref outputLength);
			if (flag)
			{
				output.Length = outputLength;
			}
			return flag;
		}

		private static void OptimizeEncrypt(int size, CryptoRc4Key key, byte[] output, int length)
		{
			byte[] array = new byte[size];
			Array.Copy(key.key.data, array, size);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < length; i++)
			{
				num = (num + 1) % size;
				num2 = (num2 + array[num]) % size;
				int num3 = array[num] + array[num2];
				byte b = array[num];
				array[num] = array[num2];
				array[num2] = b;
				int num4 = num3 % size;
				int num5 = i;
				output[num5] ^= array[num4];
			}
		}

		public static bool InternalEncrypt(CryptoRc4Key key, byte[] output, int length)
		{
			switch ((FastEncryptLevel)(key.key.Count * 8))
			{
			default:
				return false;
			case FastEncryptLevel.High:
				OptimizeEncrypt(256, key, output, length);
				break;
			case FastEncryptLevel.Middle:
				OptimizeEncrypt(128, key, output, length);
				break;
			case FastEncryptLevel.Low:
				OptimizeEncrypt(64, key, output, length);
				break;
			}
			return true;
		}
	}
}

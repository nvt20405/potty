using System;
using System.Security.Cryptography;
using System.Text;
using LZ4;

public static class EGUtils
{
	public static string Compress(string s)
	{
		byte[] inArray = LZ4Codec.Wrap(Encoding.UTF8.GetBytes(s));
		return Convert.ToBase64String(inArray);
	}

	public static string Decompress(string s)
	{
		byte[] array = LZ4Codec.Unwrap(Convert.FromBase64String(s));
		return Encoding.UTF8.GetString(array, 0, array.Length);
	}

	public static string md5(string data)
	{
		return BitConverter.ToString(encryptData(data)).Replace("-", string.Empty).ToLower();
	}

	private static byte[] encryptData(string data)
	{
		MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		return mD5CryptoServiceProvider.ComputeHash(uTF8Encoding.GetBytes(data));
	}
}

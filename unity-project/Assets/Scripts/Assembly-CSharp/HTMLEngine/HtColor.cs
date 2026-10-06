using System.Globalization;

namespace HTMLEngine
{
	public struct HtColor
	{
		public static readonly HtColor transparent = RGBA(0, 0, 0, 0);

		public static readonly HtColor _error = RGBA(byte.MaxValue, 0, 0);

		public static readonly HtColor maroon = Parse("#800000");

		public static readonly HtColor red = Parse("#FF0000");

		public static readonly HtColor orange = Parse("#FFA500");

		public static readonly HtColor yellow = Parse("#FFFF00");

		public static readonly HtColor olive = Parse("#808000");

		public static readonly HtColor purple = Parse("#800080");

		public static readonly HtColor fuchsia = Parse("#FF00FF");

		public static readonly HtColor white = Parse("#FFFFFF");

		public static readonly HtColor lime = Parse("#00FF00");

		public static readonly HtColor green = Parse("#008000");

		public static readonly HtColor navy = Parse("#000080");

		public static readonly HtColor blue = Parse("#0000FF");

		public static readonly HtColor aqua = Parse("#00FFFF");

		public static readonly HtColor teal = Parse("#008080");

		public static readonly HtColor black = Parse("#000000");

		public static readonly HtColor silver = Parse("#C0C0C0");

		public static readonly HtColor gray = Parse("#808080");

		public byte R;

		public byte G;

		public byte B;

		public byte A;

		public bool IsTransparent
		{
			get
			{
				return A == 0;
			}
		}

		public static HtColor RGBA(byte r, byte g, byte b, byte a = byte.MaxValue)
		{
			return new HtColor
			{
				R = r,
				G = g,
				B = b,
				A = a
			};
		}

		private static bool TryParse(string rs, string gs, string bs, ref byte r, ref byte g, ref byte b)
		{
			return byte.TryParse(rs, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out r) && byte.TryParse(gs, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out g) && byte.TryParse(bs, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out b);
		}

		private static bool TryParse(string rs, string gs, string bs, string aa, ref byte r, ref byte g, ref byte b, ref byte a)
		{
			return byte.TryParse(rs, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out r) && byte.TryParse(gs, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out g) && byte.TryParse(bs, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out b) && byte.TryParse(aa, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out a);
		}

		public static HtColor Parse(string text)
		{
			return Parse(text, _error);
		}

		public static HtColor Parse(string text, HtColor onError)
		{
			if (string.IsNullOrEmpty(text))
			{
				return onError;
			}
			if (text.StartsWith("#"))
			{
				byte r = 0;
				byte g = 0;
				byte b = 0;
				switch (text.Length)
				{
				case 4:
				{
					string text2 = text.Substring(1, 1);
					text2 += text2;
					string text3 = text.Substring(2, 1);
					text3 += text3;
					string text4 = text.Substring(3, 1);
					text4 += text4;
					if (TryParse(text2, text3, text4, ref r, ref g, ref b))
					{
						return RGBA(r, g, b);
					}
					break;
				}
				case 7:
				{
					string rs2 = text.Substring(1, 2);
					string gs2 = text.Substring(3, 2);
					string bs2 = text.Substring(5, 2);
					if (TryParse(rs2, gs2, bs2, ref r, ref g, ref b))
					{
						return RGBA(r, g, b);
					}
					break;
				}
				case 9:
				{
					string rs = text.Substring(1, 2);
					string gs = text.Substring(3, 2);
					string bs = text.Substring(5, 2);
					byte a = byte.MaxValue;
					string aa = text.Substring(7, 2);
					if (TryParse(rs, gs, bs, aa, ref r, ref g, ref b, ref a))
					{
						return RGBA(r, g, b, a);
					}
					break;
				}
				}
			}
			else
			{
				switch (text)
				{
				case "transparent":
					return transparent;
				case "maroon":
					return maroon;
				case "red":
					return red;
				case "orange":
					return orange;
				case "yellow":
					return yellow;
				case "olive":
					return olive;
				case "purple":
					return purple;
				case "fuchsia":
					return fuchsia;
				case "white":
					return white;
				case "lime":
					return lime;
				case "green":
					return green;
				case "navy":
					return navy;
				case "blue":
					return blue;
				case "aqua":
					return aqua;
				case "teal":
					return teal;
				case "black":
					return black;
				case "silver":
					return silver;
				case "gray":
					return gray;
				}
			}
			return onError;
		}

		public override string ToString()
		{
			return string.Format("{0:X2}{1:X2}{2:X2}({3:X2})", R, G, B, A);
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class EGMUDIM
{
	public class SpellerClass
	{
		public bool enabled = true;

		public int position;

		public int count;

		public List<int> vowels = new List<int>();

		public List<char> lasts = new List<char>();

		public void Toogle()
		{
			enabled = !enabled;
		}

		public void Set(int position, char key)
		{
			vowels.Add(this.position);
			lasts.Add(key);
			count++;
			this.position = position;
		}

		public void Clear()
		{
			position = -1;
			count = 0;
			vowels.Clear();
			lasts.Clear();
		}

		public char Last()
		{
			return lasts[count - 1];
		}
	}

	private class AccentCls
	{
		public int pos;

		public int code;

		public int[] subTable;

		public int index;

		public AccentCls(int position, int code, int[] subTable, int index)
		{
			pos = position;
			this.code = code;
			this.subTable = subTable;
			this.index = index;
		}

		public AccentCls(AccentCls other)
		{
			pos = other.pos;
			code = other.code;
			if (other.subTable != null)
			{
				subTable = new int[other.subTable.Length];
				Array.Copy(other.subTable, subTable, other.subTable.Length);
			}
			else
			{
				subTable = null;
			}
			index = other.index;
		}
	}

	private const char CHAR_A = 'A';

	private const char CHAR_a = 'a';

	private const char CHAR_E = 'E';

	private const char CHAR_e = 'e';

	private const char CHAR_U = 'U';

	private const char CHAR_u = 'u';

	private const char CHAR_G = 'G';

	private const char CHAR_g = 'g';

	private const char CHAR_Q = 'Q';

	private const char CHAR_q = 'q';

	private const char CHAR_y = 'y';

	private const char CHAR_Y = 'Y';

	private const char CHAR_i = 'i';

	private const char CHAR_I = 'I';

	private const char CHAR_0x80 = '\u0080';

	private const string vowels = "AIUEOYaiueoy";

	private const string seperators = " !@#$%^&*()_+=-{}[]|\\:\";'<>?,./~`\r\n\t";

	private const string consonants = "BCDFGHJKLMNPQRSTVWXZbcdfghjklmnpqrstvwxz";

	private const string spchk = "AIUEOYaiueoy|BDFJKLQSVWXZbdfjklqsvwxz|'`~?.^*+=";

	private const string vwchk = "|oa|uy|ue|oe|ou|ye|ua|uo|ai|ui|oi|au|iu|ia|eu|ie|ao|eo|ay|uu|io|yu|";

	private const string nvchk = "FfJjWwZz";

	private const string tailConsonantsPattern = "|c|ch|p|t|m|n|ng|nh|";

	public const int VK_TAB = 9;

	public const int VK_BACKSPACE = 8;

	public const int VK_ENTER = 13;

	public const int VK_DELETE = 46;

	public const int VK_SPACE = 32;

	public const int VK_LIMIT = 128;

	public const int VK_LEFT_ARROW = 37;

	public const int VK_RIGHT_ARROW = 39;

	public const int VK_HOME = 36;

	public const int VK_END = 35;

	public const int VK_PAGE_UP = 33;

	public const int VK_PAGE_DOWN = 34;

	public const int VK_UP_ARROW = 38;

	public const int VK_DOWN_ARROW = 40;

	public const int VK_ONOFF = 47;

	public const int VK_SWITCHMETHOD = 44;

	public const int VK_CTRL = 17;

	public const int VK_SHIFT = 16;

	public const int VK_ALT = 18;

	private StringBuilder buffer = new StringBuilder();

	public int off;

	public bool dirty;

	public bool tempOff;

	public bool tempDisableSpellCheck;

	private int w;

	public int startWordOffset;

	public int method = 2;

	private string tailConsonants = string.Empty;

	public string headConsonants = string.Empty;

	public bool newAccentRule = true;

	public SpellerClass Speller = new SpellerClass();

	private AccentCls accent = new AccentCls(-1, 0, null, -1);

	public static readonly List<string> methodNames = new List<string> { "off", "vni", "telex", "viqr", "mixed", "auto" };

	public static readonly int[] vn_A0 = new int[6] { 65, 193, 192, 7842, 195, 7840 };

	public static readonly int[] vn_a0 = new int[6] { 97, 225, 224, 7843, 227, 7841 };

	public static readonly int[] vn_A6 = new int[6] { 194, 7844, 7846, 7848, 7850, 7852 };

	public static readonly int[] vn_a6 = new int[6] { 226, 7845, 7847, 7849, 7851, 7853 };

	public static readonly int[] vn_A8 = new int[6] { 258, 7854, 7856, 7858, 7860, 7862 };

	public static readonly int[] vn_a8 = new int[6] { 259, 7855, 7857, 7859, 7861, 7863 };

	public static readonly int[] vn_O0 = new int[6] { 79, 211, 210, 7886, 213, 7884 };

	public static readonly int[] vn_o0 = new int[6] { 111, 243, 242, 7887, 245, 7885 };

	public static readonly int[] vn_O6 = new int[6] { 212, 7888, 7890, 7892, 7894, 7896 };

	public static readonly int[] vn_o6 = new int[6] { 244, 7889, 7891, 7893, 7895, 7897 };

	public static readonly int[] vn_O7 = new int[6] { 416, 7898, 7900, 7902, 7904, 7906 };

	public static readonly int[] vn_o7 = new int[6] { 417, 7899, 7901, 7903, 7905, 7907 };

	public static readonly int[] vn_U0 = new int[6] { 85, 218, 217, 7910, 360, 7908 };

	public static readonly int[] vn_u0 = new int[6] { 117, 250, 249, 7911, 361, 7909 };

	public static readonly int[] vn_U7 = new int[6] { 431, 7912, 7914, 7916, 7918, 7920 };

	public static readonly int[] vn_u7 = new int[6] { 432, 7913, 7915, 7917, 7919, 7921 };

	public static readonly int[] vn_E0 = new int[6] { 69, 201, 200, 7866, 7868, 7864 };

	public static readonly int[] vn_e0 = new int[6] { 101, 233, 232, 7867, 7869, 7865 };

	public static readonly int[] vn_E6 = new int[6] { 202, 7870, 7872, 7874, 7876, 7878 };

	public static readonly int[] vn_e6 = new int[6] { 234, 7871, 7873, 7875, 7877, 7879 };

	public static readonly int[] vn_I0 = new int[6] { 73, 205, 204, 7880, 296, 7882 };

	public static readonly int[] vn_i0 = new int[6] { 105, 237, 236, 7881, 297, 7883 };

	public static readonly int[] vn_Y0 = new int[6] { 89, 221, 7922, 7926, 7928, 7924 };

	public static readonly int[] vn_y0 = new int[6] { 121, 253, 7923, 7927, 7929, 7925 };

	public static readonly int[][] vncode_2 = new int[24][]
	{
		vn_A0, vn_a0, vn_A6, vn_a6, vn_A8, vn_a8, vn_O0, vn_o0, vn_O6, vn_o6,
		vn_O7, vn_o7, vn_U0, vn_u0, vn_U7, vn_u7, vn_E0, vn_e0, vn_E6, vn_e6,
		vn_I0, vn_i0, vn_Y0, vn_y0
	};

	public static readonly int[] vn_AA = new int[48]
	{
		65, 194, 193, 7844, 192, 7846, 7842, 7848, 195, 7850,
		7840, 7852, 258, 194, 7854, 7844, 7856, 7846, 7858, 7848,
		7860, 7850, 7862, 7852, 97, 226, 225, 7845, 224, 7847,
		7843, 7849, 227, 7851, 7841, 7853, 259, 226, 7855, 7845,
		7857, 7847, 7859, 7849, 7861, 7851, 7863, 7853
	};

	public static readonly int[] vn_AW = new int[48]
	{
		65, 258, 193, 7854, 192, 7856, 7842, 7858, 195, 7860,
		7840, 7862, 194, 258, 7844, 7854, 7846, 7856, 7848, 7858,
		7850, 7860, 7852, 7862, 97, 259, 225, 7855, 224, 7857,
		7843, 7859, 227, 7861, 7841, 7863, 226, 259, 7845, 7855,
		7847, 7857, 7849, 7859, 7851, 7861, 7853, 7863
	};

	public static readonly int[] vn_OO = new int[48]
	{
		79, 212, 211, 7888, 210, 7890, 7886, 7892, 213, 7894,
		7884, 7896, 416, 212, 7898, 7888, 7900, 7900, 7902, 7892,
		7904, 7894, 7906, 7896, 111, 244, 243, 7889, 242, 7891,
		7887, 7893, 245, 7895, 7885, 7897, 417, 244, 7899, 7889,
		7901, 7891, 7903, 7893, 7905, 7895, 7907, 7897
	};

	public static readonly int[] vn_OW = new int[48]
	{
		79, 416, 211, 7898, 210, 7900, 7886, 7902, 213, 7904,
		7884, 7906, 212, 416, 7888, 7898, 7890, 7900, 7892, 7902,
		7894, 7904, 7896, 7906, 111, 417, 243, 7899, 242, 7901,
		7887, 7903, 245, 7905, 7885, 7907, 244, 417, 7889, 7899,
		7891, 7901, 7893, 7903, 7895, 7905, 7897, 7907
	};

	public static readonly int[] vn_UW = new int[24]
	{
		85, 431, 218, 7912, 217, 7914, 7910, 7916, 360, 7918,
		7908, 7920, 117, 432, 250, 7913, 249, 7915, 7911, 7917,
		361, 7919, 7909, 7921
	};

	public static readonly int[] vn_EE = new int[24]
	{
		69, 202, 201, 7870, 200, 7872, 7866, 7874, 7868, 7876,
		7864, 7878, 101, 234, 233, 7871, 232, 7873, 7867, 7875,
		7869, 7877, 7865, 7879
	};

	public static readonly int[] vn_DD = new int[4] { 68, 272, 100, 273 };

	public static readonly int[][] vncode_1 = new int[7][] { vn_AA, vn_EE, vn_OO, vn_AW, vn_OW, vn_UW, vn_DD };

	public static readonly ArrayList modes = new ArrayList
	{
		new ArrayList
		{
			new ArrayList
			{
				new ArrayList { '6', 0, 1, 2 },
				new ArrayList { '7', 4, 5 },
				new ArrayList { '8', 3 },
				new ArrayList { '9', 6 }
			},
			"6789",
			"012345"
		},
		new ArrayList
		{
			new ArrayList
			{
				new ArrayList { 'a', 0 },
				new ArrayList { 'e', 1 },
				new ArrayList { 'o', 2 },
				new ArrayList { 'w', 3, 4, 5 },
				new ArrayList { 'd', 6 }
			},
			"ewoda",
			"zsfrxj"
		},
		new ArrayList
		{
			new ArrayList
			{
				new ArrayList { '^', 0, 1, 2 },
				new ArrayList { '+', 4, 5 },
				new ArrayList { '(', 3 },
				new ArrayList { 'd', 6 }
			},
			"^+(d",
			"='`?~."
		},
		new ArrayList
		{
			new ArrayList
			{
				new ArrayList { '6', 0, 1, 2 },
				new ArrayList { '7', 4, 5 },
				new ArrayList { '8', 3 },
				new ArrayList { '9', 6 },
				new ArrayList { 'a', 0 },
				new ArrayList { 'e', 1 },
				new ArrayList { 'o', 2 },
				new ArrayList { 'w', 3, 4, 5 },
				new ArrayList { 'd', 6 }
			},
			"6789ewoda",
			"0123456zsfrxj"
		}
	};

	public static readonly int[] UI = new int[37]
	{
		85, 218, 217, 7910, 360, 7908, 117, 250, 249, 7911,
		361, 7909, 431, 7912, 7914, 7916, 7918, 7920, 432, 7913,
		7915, 7917, 7919, 7921, 73, 205, 204, 7880, 296, 7882,
		105, 237, 236, 7881, 297, 7883, 0
	};

	public static readonly int[] VN = new int[147]
	{
		97, 65, 225, 193, 224, 192, 7843, 7842, 227, 195,
		7841, 7840, 226, 194, 7845, 7844, 7847, 7846, 7849, 7848,
		7851, 7850, 7853, 7852, 259, 258, 7855, 7854, 7857, 7856,
		7859, 7858, 7861, 7860, 7863, 7862, 101, 69, 233, 201,
		232, 200, 7867, 7866, 7869, 7868, 7865, 7864, 234, 202,
		7871, 7870, 7873, 7872, 7875, 7874, 7877, 7876, 7879, 7878,
		111, 79, 243, 211, 242, 210, 7887, 7886, 245, 213,
		7885, 7884, 244, 212, 7889, 7888, 7891, 7890, 7893, 7892,
		7895, 7894, 7897, 7896, 417, 416, 7899, 7898, 7901, 7900,
		7903, 7902, 7905, 7904, 7907, 7906, 121, 89, 253, 221,
		7923, 7922, 7927, 7926, 7929, 7928, 7925, 7924, 117, 85,
		250, 218, 249, 217, 7911, 7910, 361, 360, 7909, 7908,
		432, 431, 7913, 7912, 7915, 7914, 7917, 7916, 7919, 7918,
		7921, 7920, 105, 73, 237, 205, 236, 204, 7881, 7880,
		297, 296, 7883, 7882, 273, 272, 0
	};

	public static readonly int[] O = new int[37]
	{
		79, 211, 210, 7886, 213, 7884, 111, 243, 242, 7887,
		245, 7885, 212, 7888, 7890, 7892, 7894, 7896, 244, 7889,
		7891, 7893, 7895, 7897, 416, 7898, 7900, 7902, 7904, 7906,
		417, 7899, 7901, 7903, 7905, 7907, 0
	};

	public string GetCurStringBuff()
	{
		return buffer.ToString();
	}

	public int GetBufferLength()
	{
		return buffer.Length;
	}

	public void UpdateBuffer(string mText)
	{
		ClearBuffer();
		int num = mText.LastIndexOfAny(" !@#$%^&*()_+=-{}[]|\\:\";'<>?,./~`\r\n\t".ToCharArray());
		if (num >= 0)
		{
			buffer = new StringBuilder(mText.Substring(num + 1));
		}
		else
		{
			buffer = new StringBuilder(mText);
		}
		startWordOffset = num + 1;
		dirty = false;
	}

	public int CharIsUI(char u)
	{
		int num = 0;
		for (num = 0; UI[num] != 0 && UI[num] != u; num++)
		{
		}
		return (UI[num] == 0) ? (-1) : num;
	}

	public int CharIsO(char o)
	{
		int num = 0;
		for (num = 0; UI[num] != 0 && UI[num] != o; num++)
		{
		}
		return (UI[num] == 0) ? (-1) : num;
	}

	public int CharPriorityCompare(char u1, char u2)
	{
		int num = 0;
		int num2 = -1;
		int num3 = -1;
		int num4 = 0;
		num = 0;
		for (num4 = u1; VN[num] != 0 && VN[num] != num4; num++)
		{
		}
		if (VN[num] != 0)
		{
			num2 = num;
		}
		num = 0;
		for (num4 = u2; VN[num] != 0 && VN[num] != num4; num++)
		{
		}
		if (VN[num] != 0)
		{
			num3 = num;
		}
		return num2 - num3;
	}

	public void SetCharAt(int n, char c)
	{
		buffer[n] = c;
	}

	public bool CheckSpell(char key, int grp)
	{
		StringBuilder stringBuilder = buffer;
		int length = stringBuilder.Length;
		char c = char.ToLower(key);
		if (Speller.enabled && !tempDisableSpellCheck)
		{
			if (grp > 0 && off == 0)
			{
				if (tailConsonants.Length > 0)
				{
					int num = "|c|ch|p|t|m|n|ng|nh|".IndexOf("|" + tailConsonants + "|");
					if (num < 0)
					{
						off = length;
						tailConsonants = string.Empty;
						return true;
					}
					if (num < 9 && grp == 2)
					{
						int markTypeID = GetMarkTypeID(c, 2);
						if (markTypeID != 0 && markTypeID != 1 && markTypeID != 5)
						{
							off = length;
							tailConsonants = string.Empty;
							return true;
						}
					}
				}
			}
			else if (off == 0)
			{
				int num2 = "AIUEOYaiueoy|BDFJKLQSVWXZbdfjklqsvwxz|'`~?.^*+=".IndexOf(key);
				char c2 = '\0';
				if (length > 0)
				{
					c2 = char.ToLower(stringBuilder[length - 1]);
				}
				if (length == 0)
				{
					if ("FfJjWwZz".IndexOf(key) >= 0)
					{
						off = -1;
					}
					else if (num2 >= 0 && num2 < 12)
					{
						Speller.Set(0, key);
					}
					else
					{
						if (num2 == 12 || num2 > 37)
						{
							return false;
						}
						Speller.Clear();
					}
				}
				else
				{
					if (num2 == 12 || num2 > 37)
					{
						ClearBuffer();
						return false;
					}
					if (num2 > 12)
					{
						off = length;
					}
					else if (num2 >= 0)
					{
						int i;
						for (i = 0; i < stringBuilder.Length && "BCDFGHJKLMNPQRSTVWXZbcdfghjklmnpqrstvwxz".IndexOf(stringBuilder[i]) >= 0; i++)
						{
						}
						if (i > 0)
						{
							headConsonants = SliceBuffer(stringBuilder, 0, i).Replace(",", string.Empty).ToLower();
						}
						if (Speller.position < 0)
						{
							if (headConsonants == "q")
							{
								if (length == 1 && c != 'u')
								{
									off = length;
								}
								else if (length > 1 && stringBuilder[1] == 'u' && c == 'u')
								{
									off = length;
								}
							}
							else if (c2 == 'p' && c != 'h')
							{
								off = length;
							}
							else if (c2 == 'k' && c != 'i' && c != 'e' && c != 'y')
							{
								off = length;
							}
							else if (headConsonants == "ngh" && c != 'i' && c != 'e')
							{
								off = length;
							}
							else
							{
								Speller.Set(length, key);
								switch (c)
								{
								case 'y':
									if ("hklmst".IndexOf(c2) < 0)
									{
										off = length;
									}
									break;
								case 'e':
								case 'i':
									if (length > 1 && c2 == 'g')
									{
										off = length;
									}
									if (c2 == 'c')
									{
										off = 1;
									}
									break;
								}
							}
						}
						else if (length - Speller.position > 1)
						{
							off = length;
						}
						else
						{
							string value = "|" + char.ToLower(Speller.Last()) + char.ToLower(key) + "|";
							int num3 = "|oa|uy|ue|oe|ou|ye|ua|uo|ai|ui|oi|au|iu|ia|eu|ie|ao|eo|ay|uu|io|yu|".IndexOf(value);
							if (num3 < 0)
							{
								off = length;
							}
							else if (num3 < 18 && (headConsonants == "c" || headConsonants == "C"))
							{
								off = length;
							}
							else if (c2 == 'y' && string.IsNullOrEmpty(headConsonants) && c != 'e')
							{
								off = length;
							}
							else
							{
								Speller.Set(length, key);
							}
						}
					}
					else
					{
						switch (key)
						{
						case 'H':
						case 'h':
							if (c2 >= '\u0080' || "CGKNPTcgknpt".IndexOf(c2) < 0)
							{
								off = length;
							}
							break;
						case 'G':
						case 'g':
							if (c2 != 'n' && c2 != 'N')
							{
								off = length;
							}
							break;
						case 'R':
						case 'r':
							if (c2 != 't' && c2 != 'T')
							{
								off = length;
							}
							break;
						default:
							if ("BCDFGHJKLMNPQRSTVWXZbcdfghjklmnpqrstvwxz".IndexOf(c2) >= 0)
							{
								off = length;
							}
							break;
						}
					}
				}
			}
			if (off != 0)
			{
				return true;
			}
		}
		return false;
	}

	public bool Append(int count, char lastkey, char key)
	{
		if (" !@#$%^&*()_+=-{}[]|\\:\";'<>?,./~`\r\n\t".IndexOf(key) >= 0)
		{
			ClearBuffer();
			return false;
		}
		buffer.Append(key);
		ArrayList arrayList = modes[method - 1] as ArrayList;
		string text = arrayList[2] as string;
		AdjustAccent(text[0]);
		return true;
	}

	public int GetMarkTypeID(char key, int group)
	{
		ArrayList arrayList = modes[method - 1] as ArrayList;
		if (method != 4)
		{
			string text = arrayList[group] as string;
			return text.IndexOf(key);
		}
		int num = -1;
		for (int i = 0; i < 2; i++)
		{
			ArrayList arrayList2 = modes[i] as ArrayList;
			string text2 = arrayList2[group] as string;
			num = text2.IndexOf(key);
			if (num >= 0)
			{
				return num;
			}
		}
		return num;
	}

	private string SliceBuffer(StringBuilder b, int startIndex, int lastIndex)
	{
		int num = lastIndex - startIndex;
		char[] array = new char[num];
		b.CopyTo(startIndex, array, 0, num);
		return new string(array);
	}

	public int FindAccentPos(char nKey)
	{
		char c = char.ToLower(nKey);
		ArrayList arrayList = modes[method - 1] as ArrayList;
		StringBuilder stringBuilder = buffer;
		int length = stringBuilder.Length;
		if (length == 0 || off != 0)
		{
			return -1;
		}
		int i;
		for (i = 1; i < arrayList.Count; i++)
		{
			string text = arrayList[i] as string;
			if (text.IndexOf(c) >= 0)
			{
				break;
			}
		}
		int num = length - 1;
		int num2 = i;
		if (num2 != 1 || GetMarkTypeID(c, 1) != 3)
		{
			i = num;
			while (i >= 0 && stringBuilder[i] < '\u0080' && "AIUEOYaiueoy".IndexOf(stringBuilder[i]) < 0)
			{
				i--;
			}
			if (i < 0)
			{
				return -1;
			}
			if (i < length - 1)
			{
				tailConsonants = SliceBuffer(stringBuilder, i + 1, length).Replace(",", string.Empty).ToLower();
			}
			while (i - 1 >= 0 && ("AIUEOYaiueoy".IndexOf(stringBuilder[i - 1]) >= 0 || stringBuilder[i - 1] > '\u0080') && CharPriorityCompare(stringBuilder[i - 1], stringBuilder[i]) < 0)
			{
				i--;
			}
			int num3;
			if (i == length - 1 && i - 1 >= 0 && (num3 = CharIsUI(stringBuilder[i - 1])) > 0)
			{
				switch (stringBuilder[i])
				{
				case 'A':
				case 'a':
					if ((i - 2 < 0 || (num3 < 24 && stringBuilder[i - 2] != 'q' && stringBuilder[i - 2] != 'Q') || (num3 >= 24 && stringBuilder[i - 2] != 'g' && stringBuilder[i - 2] != 'G')) && (num2 == 2 || (num2 == 1 && GetMarkTypeID(c, 1) == 1)))
					{
						i--;
					}
					break;
				case 'U':
				case 'u':
					if (i - 2 < 0 || (stringBuilder[i - 2] != 'g' && stringBuilder[i - 2] != 'G'))
					{
						i--;
					}
					break;
				case 'Y':
				case 'y':
					if (!newAccentRule && i - 2 >= 0 && stringBuilder[i - 2] != 'q' && stringBuilder[i - 2] != 'Q')
					{
						i--;
					}
					break;
				}
			}
			if (i == length - 1 && i - 1 >= 0 && CharIsO(stringBuilder[i - 1]) > 0)
			{
				switch (stringBuilder[i])
				{
				case 'A':
				case 'a':
					if (!newAccentRule && (num2 == 2 || (num2 == 1 && GetMarkTypeID(c, 1) != 1)))
					{
						i--;
					}
					break;
				case 'E':
				case 'e':
					if (!newAccentRule)
					{
						i--;
					}
					break;
				}
			}
			if (i == length - 2 && i - 1 >= 0)
			{
				int num4 = CharIsUI(stringBuilder[i]);
				if (num4 >= 0 && ((num4 < 24) & (stringBuilder[i - 1] == 'q' || stringBuilder[i - 1] == 'Q')))
				{
					i++;
				}
			}
			num = i;
		}
		if (GetMarkTypeID(c, 1) == 3 && stringBuilder[0] == 'd')
		{
			return 0;
		}
		return num;
	}

	private void ResetAccentInfo()
	{
		accent = new AccentCls(-1, 0, null, 122);
	}

	public bool PutMark(int pos, int charCodeAtPos, int group, int[] subsTab, char key, bool checkDouble)
	{
		for (int i = 0; i < subsTab.Length; i++)
		{
			if (subsTab[i] != charCodeAtPos)
			{
				continue;
			}
			switch (group)
			{
			case 1:
				if (GetMarkTypeID(key, 1) == 1)
				{
					w++;
				}
				if (i % 2 == 0)
				{
					SetCharAt(pos, (char)subsTab[i + 1]);
					break;
				}
				SetCharAt(pos, (char)subsTab[i - 1]);
				if (checkDouble)
				{
					off = buffer.Length + 1;
				}
				break;
			case 2:
			{
				int markTypeID = GetMarkTypeID(key, 2);
				if (markTypeID < 0)
				{
					break;
				}
				if (markTypeID != i)
				{
					SetCharAt(pos, (char)subsTab[markTypeID]);
					accent = new AccentCls(pos, buffer[pos], subsTab, key);
					break;
				}
				SetCharAt(pos, (char)subsTab[0]);
				ResetAccentInfo();
				if (checkDouble)
				{
					off = buffer.Length + 1;
				}
				break;
			}
			}
			return true;
		}
		return false;
	}

	public void SetMethod(int m)
	{
		ClearBuffer();
		method = m;
	}

	public bool AdjustAccent(char vk)
	{
		if (off != 0)
		{
			return false;
		}
		int num = FindAccentPos(vk);
		AccentCls accentCls = new AccentCls(accent);
		StringBuilder stringBuilder = buffer;
		if (num < 0)
		{
			return false;
		}
		int num2 = vn_OW.Length - 1;
		int num3 = stringBuilder[num];
		while (num2 >= 0 && vn_OW[num2] != num3)
		{
			num2--;
		}
		int num4 = vn_UW.Length - 1;
		if (num > 0)
		{
			num3 = stringBuilder[num - 1];
			while (num4 >= 0 && vn_UW[num4] != num3)
			{
				num4--;
			}
		}
		else
		{
			num4 = -1;
		}
		if (num < stringBuilder.Length - 1 && num > 0 && num2 >= 0 && num4 >= 0 && w == 1)
		{
			if (num2 % 2 == 0)
			{
				ArrayList arrayList = modes[method - 1] as ArrayList;
				string text = arrayList[1] as string;
				PutMark(num, stringBuilder[num], 1, vn_OW, text[1], false);
				if (stringBuilder[0] == 'q' || stringBuilder[0] == 'Q')
				{
					PutMark(num - 1, stringBuilder[num - 1], 1, vn_UW, text[1], false);
				}
			}
			else
			{
				ArrayList arrayList2 = modes[method - 1] as ArrayList;
				string text2 = arrayList2[1] as string;
				if (stringBuilder[0] != 'q' && stringBuilder[0] != 'Q')
				{
					PutMark(num - 1, stringBuilder[num - 1], 1, vn_UW, text2[1], false);
				}
			}
			return true;
		}
		if (accentCls.pos >= 0 && num > 0 && accentCls.pos != num)
		{
			PutMark(accentCls.pos, accentCls.code, 2, accentCls.subTable, (char)accentCls.index, false);
			for (num2 = 0; num2 < vncode_2.Length; num2++)
			{
				int[] subsTab = vncode_2[num2];
				if (PutMark(num, stringBuilder[num], 2, subsTab, (char)accentCls.index, true))
				{
					break;
				}
			}
			return true;
		}
		return false;
	}

	public bool AddKey(char key)
	{
		int num = -1;
		int num2 = -1;
		int num3 = 0;
		int grp = 0;
		int length = buffer.Length;
		int[] array = null;
		if (length == 0 || off != 0 || tempOff)
		{
			if (CheckSpell(key, grp))
			{
				return Append(length, (char)num3, key);
			}
			return Append(0, '\0', key);
		}
		ArrayList arrayList = modes[method - 1] as ArrayList;
		StringBuilder stringBuilder = buffer;
		num = length - 1;
		num3 = stringBuilder[num];
		char c = char.ToLower(key);
		for (grp = 1; grp < arrayList.Count; grp++)
		{
			string text = arrayList[grp] as string;
			if (text.IndexOf(c) >= 0)
			{
				break;
			}
		}
		if (grp >= arrayList.Count)
		{
			CheckSpell(key, 0);
			return Append(length, (char)num3, key);
		}
		if ((num = FindAccentPos(c)) < 0)
		{
			CheckSpell(key, 0);
			return Append(length, (char)num3, key);
		}
		if (CheckSpell(key, grp))
		{
			return Append(length, (char)num3, key);
		}
		num3 = stringBuilder[num];
		int charCodeAtPos = num3;
		bool flag = false;
		if (grp == 1)
		{
			arrayList = arrayList[0] as ArrayList;
			int num4 = 0;
			while (!flag && num4 < arrayList.Count)
			{
				ArrayList arrayList2 = arrayList[num4] as ArrayList;
				if ((char)arrayList2[0] == c)
				{
					for (num4 = 1; num4 < arrayList2.Count; num4++)
					{
						array = vncode_1[(int)arrayList2[num4]];
						AdjustAccent(c);
						charCodeAtPos = stringBuilder[num];
						if (GetMarkTypeID(c, 1) == 3)
						{
							num = 0;
							num3 = stringBuilder[num];
							charCodeAtPos = num3;
						}
						if (PutMark(num, charCodeAtPos, 1, array, c, true))
						{
							if (num > 0 && GetMarkTypeID(c, 1) == 1 && num < length - 1 && CharIsO(stringBuilder[num]) >= 0 && CharIsUI(stringBuilder[num - 1]) >= 0 && stringBuilder[0] != 'q' && stringBuilder[0] != 'Q')
							{
								PutMark(num - 1, stringBuilder[num - 1], 1, vn_UW, c, false);
							}
							flag = true;
							break;
						}
					}
					break;
				}
				num4++;
			}
		}
		else
		{
			for (int i = 0; i < vncode_2.Length; i++)
			{
				array = vncode_2[i];
				if (PutMark(num, charCodeAtPos, 2, array, c, true))
				{
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			CheckSpell(key, 0);
			return Append(length, (char)num3, key);
		}
		if (off != 0)
		{
			buffer.Append(key);
		}
		return num >= 0;
	}

	public void BackSpace()
	{
		int length = buffer.Length;
		if (length <= 0)
		{
			dirty = true;
			return;
		}
		if (accent.pos == length - 1)
		{
			ResetAccentInfo();
		}
		int num = vn_OW.Length - 1;
		char c = buffer[length - 1];
		while (num >= 0 && vn_OW[num] != c)
		{
			num--;
		}
		if (num < 0)
		{
			num = vn_UW.Length - 1;
			while (num >= 0 && vn_UW[num] != c)
			{
				num--;
			}
		}
		if (num >= 0 && num % 2 == 1)
		{
			w--;
		}
		length--;
		buffer.Remove(buffer.Length - 1, 1);
		if (length == Speller.position)
		{
			Speller.position = Speller.vowels[--Speller.count];
		}
		if ((off < 0 && length == 0) || length <= off)
		{
			off = 0;
		}
	}

	public void ClearBuffer()
	{
		off = 0;
		w = 0;
		Speller.Clear();
		ResetAccentInfo();
		tailConsonants = string.Empty;
		headConsonants = string.Empty;
		if (buffer.Length > 0)
		{
			tempOff = false;
			tempDisableSpellCheck = false;
		}
		buffer = new StringBuilder();
	}

	public void SwitchMethod()
	{
		ClearBuffer();
		method = ++method % 5;
	}

	public void Toggle()
	{
	}

	public void ProcessControlKey(int keyCode, bool release)
	{
		switch (keyCode)
		{
		case 9:
		case 13:
			ClearBuffer();
			break;
		case 8:
			if (!release)
			{
				BackSpace();
			}
			break;
		case 33:
		case 34:
		case 35:
		case 36:
		case 37:
		case 38:
		case 39:
		case 40:
		case 46:
			dirty = true;
			break;
		}
	}
}

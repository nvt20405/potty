using System;
using System.Text;

namespace HTMLEngine.Core
{
	internal class Reader
	{
		public static readonly Reader Instance = new Reader();

		public bool AutoSkipMLComments;

		public bool AutoSkipSLComments;

		public bool AutoSkipWhitespace;

		private string text;

		private int begin;

		private int end;

		private int curr;

		private readonly StringBuilder sb;

		public string CurrentText
		{
			get
			{
				return (!IsEof) ? text.Substring(curr) : "(eof)";
			}
		}

		public bool IsEof
		{
			get
			{
				return curr >= end;
			}
		}

		public long Length
		{
			get
			{
				return end - begin;
			}
		}

		public long Position
		{
			get
			{
				return curr - begin;
			}
		}

		public long Rest
		{
			get
			{
				return end - curr;
			}
		}

		public char CurrChar
		{
			get
			{
				return (curr >= begin && curr < end) ? text[curr] : '\0';
			}
		}

		public char NextChar
		{
			get
			{
				return (curr < end - 1) ? text[curr + 1] : '\0';
			}
		}

		public char PrevChar
		{
			get
			{
				return (curr > begin) ? text[curr - 1] : '\0';
			}
		}

		public Reader()
		{
			sb = new StringBuilder(100);
		}

		public void SetSource(string text)
		{
			this.text = text;
			begin = 0;
			end = begin + text.Length;
			curr = begin;
		}

		private void DoAutoSkip()
		{
			if (AutoSkipWhitespace)
			{
				SkipWhitespace();
			}
			while (curr < end)
			{
				int num = 0;
				if (AutoSkipSLComments)
				{
					while (IsOnSLComments())
					{
						SkipToChar('\n');
						if (AutoSkipWhitespace)
						{
							SkipWhitespace();
						}
						num++;
					}
				}
				if (AutoSkipMLComments)
				{
					while (IsOnMLComments())
					{
						while (SkipToChar('*') && CurrChar == '/')
						{
							curr++;
						}
						if (AutoSkipWhitespace)
						{
							SkipWhitespace();
						}
						num++;
					}
				}
				if (num == 0)
				{
					break;
				}
			}
		}

		public bool SkipToChar(char c, bool thenSkipThisChar = true)
		{
			DoAutoSkip();
			while (curr < end)
			{
				if (CurrChar == c)
				{
					if (thenSkipThisChar)
					{
						curr++;
					}
					break;
				}
			}
			DoAutoSkip();
			return curr < end;
		}

		public void Skip(int count)
		{
			curr += count;
			DoAutoSkip();
		}

		public bool SkipWhitespace()
		{
			int num = curr;
			while (IsOnWhitespace())
			{
				curr++;
			}
			return curr > num;
		}

		public string ReadToStopChar(char stopChar, bool ignoreCase = false)
		{
			DoAutoSkip();
			sb.Length = 0;
			while (curr < end)
			{
				char currChar = CurrChar;
				if (CompareChars(currChar, stopChar, ignoreCase))
				{
					DoAutoSkip();
					return sb.ToString();
				}
				sb.Append(currChar);
				curr++;
			}
			DoAutoSkip();
			return sb.ToString();
		}

		public string ReadToStopChar(char[] chars, bool ignoreCase = false)
		{
			DoAutoSkip();
			sb.Length = 0;
			while (curr < end)
			{
				char currChar = CurrChar;
				for (int i = 0; i < chars.Length; i++)
				{
					if (CompareChars(currChar, chars[i], ignoreCase))
					{
						DoAutoSkip();
						return sb.ToString();
					}
				}
				sb.Append(CurrChar);
				curr++;
			}
			DoAutoSkip();
			return sb.ToString();
		}

		public string ReadToStopText(string stopText, bool ignoreCase = false)
		{
			DoAutoSkip();
			char c = stopText[0];
			sb.Length = 0;
			while (curr < end)
			{
				char currChar = CurrChar;
				if (CompareChars(currChar, c, ignoreCase) && IsOnText(stopText))
				{
					DoAutoSkip();
					return sb.ToString();
				}
				sb.Append(currChar);
				curr++;
			}
			DoAutoSkip();
			return sb.ToString();
		}

		public string ReadToWhitespace()
		{
			DoAutoSkip();
			sb.Length = 0;
			while (curr < end)
			{
				if (IsOnWhitespace())
				{
					return sb.ToString();
				}
				sb.Append(CurrChar);
				curr++;
			}
			DoAutoSkip();
			return sb.ToString();
		}

		public string ReadToWhitespaceOrChar(char c)
		{
			DoAutoSkip();
			sb.Length = 0;
			while (curr < end)
			{
				if (IsOnWhitespace() || CurrChar == c)
				{
					return sb.ToString();
				}
				sb.Append(CurrChar);
				curr++;
			}
			DoAutoSkip();
			return sb.ToString();
		}

		public string ReadQuotedString()
		{
			DoAutoSkip();
			sb.Length = 0;
			if (curr < end)
			{
				char currChar = CurrChar;
				curr++;
				while (curr < end)
				{
					char currChar2 = CurrChar;
					curr++;
					if (currChar2 == '\\' && CurrChar == currChar)
					{
						currChar2 = CurrChar;
						curr++;
					}
					else if (currChar2 == currChar)
					{
						return sb.ToString();
					}
					sb.Append(currChar2);
				}
			}
			DoAutoSkip();
			return sb.ToString();
		}

		public bool IsOnSLComments()
		{
			return curr >= begin && curr < end - 1 && CurrChar == '/' && NextChar == '/';
		}

		public bool IsOnMLComments()
		{
			return curr >= begin && curr < end - 1 && CurrChar == '/' && NextChar == '*';
		}

		public bool IsOnWhitespace()
		{
			return curr < end && CurrChar <= ' ';
		}

		public bool IsOnQuote()
		{
			return (curr < end && CurrChar == '\'') || CurrChar == '"';
		}

		public bool IsOnDigit()
		{
			return curr < end && char.IsDigit(CurrChar);
		}

		public bool IsOnLetter()
		{
			return curr < end && char.IsLetter(CurrChar);
		}

		public bool IsOnLetterOrDigit()
		{
			return curr < end && char.IsLetterOrDigit(CurrChar);
		}

		public bool IsOnChar(char c, bool ignoreCase = false)
		{
			return curr < end && CompareChars(CurrChar, c, ignoreCase);
		}

		public bool IsOnChar(char[] chars, bool ignoreCase = false)
		{
			if (curr < end)
			{
				char currChar = CurrChar;
				for (int i = 0; i < chars.Length; i++)
				{
					if (CompareChars(currChar, chars[i], ignoreCase))
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool IsOnText(string text, bool ignoreCase = false)
		{
			int length = text.Length;
			if (Rest < length)
			{
				return false;
			}
			return text.IndexOf(text, curr, (!ignoreCase) ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase) == curr;
		}

		private static bool CompareChars(char c1, char c2, bool ignoreCase)
		{
			return (!ignoreCase) ? (c1 == c2) : (char.ToUpperInvariant(c1) == char.ToUpperInvariant(c2));
		}
	}
}

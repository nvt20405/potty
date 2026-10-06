using System;
using System.Collections.Generic;

namespace HTMLEngine.Core
{
	internal class HtmlChunkTag : HtmlChunk
	{
		private static readonly char[] TAG_NAME_STOP_CHARS = new char[3] { ' ', '/', '>' };

		private static readonly char[] ATTR_NAME_STOP_CHARS = new char[2] { ' ', '=' };

		private static readonly char[] ATTR_VALUE_STOP_CHARS = new char[3] { ' ', '/', '>' };

		private readonly Dictionary<string, string> Attrs = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

		public bool IsClosing;

		public bool IsSingle;

		public string Tag;

		internal override void OnRelease()
		{
			Attrs.Clear();
			base.OnRelease();
		}

		public string GetAttr(string attrName)
		{
			string value;
			return (!Attrs.TryGetValue(attrName, out value)) ? null : value;
		}

		public bool ReadTag(Reader reader)
		{
			reader.AutoSkipWhitespace = true;
			reader.SkipWhitespace();
			if (!reader.IsOnChar('<'))
			{
				return false;
			}
			reader.Skip(1);
			IsClosing = false;
			if (reader.CurrChar == '/')
			{
				IsClosing = true;
				reader.Skip(1);
			}
			Tag = reader.ReadToStopChar(TAG_NAME_STOP_CHARS);
			while (reader.IsOnLetter())
			{
				string key = reader.ReadToStopChar(ATTR_NAME_STOP_CHARS);
				reader.ReadToStopChar('=');
				reader.Skip(1);
				string value = ((!reader.IsOnQuote()) ? reader.ReadToStopChar(ATTR_VALUE_STOP_CHARS) : reader.ReadQuotedString());
				Attrs[key] = value;
				reader.SkipWhitespace();
			}
			switch (Tag)
			{
			case "br":
			case "hr":
			case "img":
			case "meta":
				IsSingle = true;
				break;
			default:
				IsSingle = reader.CurrChar == '/';
				break;
			}
			reader.ReadToStopChar('>');
			if (reader.CurrChar != '>')
			{
				return false;
			}
			reader.Skip(1);
			return IsTagSupported();
		}

		private bool IsTagSupported()
		{
			switch (Tag)
			{
			case "a":
			case "img":
			case "p":
			case "spin":
			case "br":
			case "font":
			case "code":
			case "b":
			case "i":
			case "u":
			case "s":
			case "strike":
			case "effect":
				return true;
			default:
				HtEngine.Log(HtLogLevel.Warning, "Ignoring unsupported tag: " + Tag);
				return false;
			}
		}

		public override string ToString()
		{
			return string.Format("<{0}>", (!IsClosing) ? Tag : ("/" + Tag));
		}
	}
}

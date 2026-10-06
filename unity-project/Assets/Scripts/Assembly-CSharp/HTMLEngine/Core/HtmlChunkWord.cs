namespace HTMLEngine.Core
{
	internal class HtmlChunkWord : HtmlChunk
	{
		public string Text;

		public bool ReadWord(Reader reader)
		{
			reader.AutoSkipWhitespace = false;
			Text = reader.ReadToWhitespaceOrChar('<');
			if (!string.IsNullOrEmpty(Text))
			{
				Text = Text.Replace("&nbsp;", " ").Replace("&gt;", ">").Replace("&lt;", "<");
				return true;
			}
			return false;
		}

		public override string ToString()
		{
			return string.Format("WORD:" + Text);
		}
	}
}

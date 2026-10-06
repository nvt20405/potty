namespace HTMLEngine.Core
{
	internal class HtmlChunkCollection : PList<HtmlChunk>
	{
		internal override void OnRelease()
		{
			Clear();
			base.OnRelease();
		}

		public void Clear(bool releaseItems = true)
		{
			if (releaseItems)
			{
				foreach (HtmlChunk item in list)
				{
					item.Dispose();
				}
			}
			list.Clear();
		}

		public void Read(Reader reader)
		{
			Clear();
			while (!reader.IsEof)
			{
				reader.SkipWhitespace();
				if (reader.IsOnChar('<'))
				{
					HtmlChunkTag htmlChunkTag = OP<HtmlChunkTag>.Acquire();
					if (!htmlChunkTag.ReadTag(reader))
					{
						htmlChunkTag.Dispose();
					}
					else
					{
						Add(htmlChunkTag);
					}
					continue;
				}
				HtmlChunkWord htmlChunkWord = OP<HtmlChunkWord>.Acquire();
				if (!htmlChunkWord.ReadWord(reader))
				{
					htmlChunkWord.Dispose();
					break;
				}
				Add(htmlChunkWord);
			}
		}
	}
}

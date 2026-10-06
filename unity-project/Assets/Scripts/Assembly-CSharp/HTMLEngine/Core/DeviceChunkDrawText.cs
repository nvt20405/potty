namespace HTMLEngine.Core
{
	internal class DeviceChunkDrawText : DeviceChunk
	{
		public DrawTextDeco Deco;

		public bool DecoStop;

		public HtColor Color;

		public string Text;

		public string Id;

		public bool PrevIsWord;

		public override void Draw(float deltaTime, string linkText, object userData)
		{
			HtDevice device = HtEngine.Device;
			if ((Deco & DrawTextDeco.Underline) != DrawTextDeco.None)
			{
				device.FillRect(new HtRect(Rect.X, Rect.Bottom - 2, (!DecoStop) ? base.TotalWidth : Rect.Width, 1), Color, userData);
			}
			if ((Deco & DrawTextDeco.Strike) != DrawTextDeco.None)
			{
				device.FillRect(new HtRect(Rect.X, Rect.Bottom - Rect.Height / 2 - 1, (!DecoStop) ? base.TotalWidth : Rect.Width, 1), Color, userData);
			}
			Font.Draw(Id, Rect, Color, Text, false, DrawTextEffect.None, HtColor.white, 0, linkText, userData);
		}

		public override void MeasureSize()
		{
			HtSize htSize = Font.Measure(Text);
			Rect.Width = htSize.Width;
			Rect.Height = Font.LineSpacing;
		}

		public override string ToString()
		{
			return Text ?? "(null)";
		}
	}
}

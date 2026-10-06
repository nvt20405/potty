namespace HTMLEngine.Core
{
	internal class DeviceChunkDrawImage : DeviceChunk
	{
		public HtImage Image;

		public HtColor Color = HtColor.white;

		public string Id;

		public override void Draw(float deltaTime, string linkText, object userData)
		{
			Image.Draw(Id, Rect, Color, linkText, userData);
		}

		public override void MeasureSize()
		{
			Rect.Width = Image.Width;
			Rect.Height = Image.Height;
		}
	}
}

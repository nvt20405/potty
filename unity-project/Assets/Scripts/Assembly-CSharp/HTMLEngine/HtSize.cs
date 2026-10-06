using System.Globalization;

namespace HTMLEngine
{
	public struct HtSize
	{
		public int Width;

		public int Height;

		public HtSize(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "(Width:{0} Height:{1})", Width, Height);
		}
	}
}

using System.Globalization;

namespace HTMLEngine
{
	public struct HtPoint
	{
		public int X;

		public int Y;

		public HtPoint(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "(X:{0} Y:{1})", X, Y);
		}
	}
}

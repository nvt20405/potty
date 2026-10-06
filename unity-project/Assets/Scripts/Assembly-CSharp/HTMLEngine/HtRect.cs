using System.Globalization;

namespace HTMLEngine
{
	public struct HtRect
	{
		public int X;

		public int Y;

		public int Width;

		public int Height;

		public int Left
		{
			get
			{
				return X;
			}
		}

		public int Right
		{
			get
			{
				return X + Width;
			}
		}

		public int Top
		{
			get
			{
				return Y;
			}
		}

		public int Bottom
		{
			get
			{
				return Y + Height;
			}
		}

		public HtRect(int x, int y, int width, int height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public HtRect Offset(int dx, int dy)
		{
			return new HtRect(X + dx, Y + dy, Width, Height);
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "(X:{0} Y:{1} Width:{2} Height:{3})", X, Y, Width, Height);
		}
	}
}

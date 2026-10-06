namespace HTMLEngine.Core
{
	internal abstract class DeviceChunk : PoolableObject
	{
		public HtRect Rect;

		public HtFont Font;

		public int ExtraSpace;

		public int TotalWidth
		{
			get
			{
				return Rect.Width + ExtraSpace;
			}
		}

		public int TotalHeight
		{
			get
			{
				return Rect.Height;
			}
		}

		public abstract void Draw(float deltaTime, string linkText, object userData);

		internal override void OnAcquire()
		{
		}

		internal override void OnRelease()
		{
		}

		public abstract void MeasureSize();

		public bool Contains(int x, int y)
		{
			int left = Rect.Left;
			int num = Rect.Right + ExtraSpace;
			int top = Rect.Top;
			int bottom = Rect.Bottom;
			return x >= left && x < num && y >= top && y < bottom;
		}
	}
}

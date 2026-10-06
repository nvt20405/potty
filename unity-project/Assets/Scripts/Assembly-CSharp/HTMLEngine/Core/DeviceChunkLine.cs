using System.Collections.Generic;

namespace HTMLEngine.Core
{
	internal class DeviceChunkLine : PoolableObject
	{
		public bool IsFull;

		public int Y;

		public int MaxWidth;

		public int Width;

		public int Height;

		private readonly List<DeviceChunk> list = new List<DeviceChunk>();

		public List<DeviceChunk> Chunks
		{
			get
			{
				return list;
			}
		}

		public int AvailWidth
		{
			get
			{
				DeviceChunk deviceChunk = ((list.Count <= 0) ? null : list[list.Count - 1]);
				int num = ((deviceChunk != null) ? (deviceChunk.Rect.Right + deviceChunk.Font.WhiteSize) : 0);
				return MaxWidth - num;
			}
		}

		internal override void OnAcquire()
		{
			Y = 0;
			MaxWidth = 0;
			Width = 0;
			Height = 0;
		}

		internal override void OnRelease()
		{
			Clear();
		}

		public void Clear(bool releaseItems = true)
		{
			if (releaseItems)
			{
				foreach (DeviceChunk item in list)
				{
					item.Dispose();
				}
			}
			list.Clear();
		}

		public bool AddChunk(DeviceChunk chunk, bool prevIsWord)
		{
			DeviceChunk deviceChunk = ((list.Count <= 0) ? null : list[list.Count - 1]);
			int num = ((deviceChunk != null) ? (deviceChunk.Rect.Right + deviceChunk.Font.WhiteSize) : 0);
			if (num + chunk.Rect.Width > MaxWidth)
			{
				return false;
			}
			if ((deviceChunk != null) & prevIsWord)
			{
				deviceChunk.ExtraSpace = deviceChunk.Font.WhiteSize;
				Width += deviceChunk.ExtraSpace;
			}
			chunk.Rect.X = num;
			chunk.Rect.Y = Y;
			chunk.ExtraSpace = 0;
			Width += chunk.Rect.Width;
			if (chunk.Rect.Height > Height)
			{
				Height = chunk.Rect.Height;
			}
			list.Add(chunk);
			return true;
		}

		public void HorzAlign(TextAlign align)
		{
			if (align == TextAlign.Justify && (!IsFull || list.Count < 2 || MaxWidth - Width <= 0))
			{
				align = TextAlign.Left;
			}
			switch (align)
			{
			case TextAlign.Left:
			{
				int num5 = 0;
				for (int l = 0; l < list.Count; l++)
				{
					DeviceChunk deviceChunk4 = list[l];
					deviceChunk4.Rect.X = num5;
					num5 += deviceChunk4.TotalWidth;
				}
				break;
			}
			case TextAlign.Right:
			{
				int num3 = MaxWidth - Width;
				for (int j = 0; j < list.Count; j++)
				{
					DeviceChunk deviceChunk2 = list[j];
					deviceChunk2.Rect.X = num3;
					num3 += deviceChunk2.TotalWidth;
				}
				break;
			}
			case TextAlign.Center:
			{
				int num4 = (MaxWidth - Width) / 2;
				for (int k = 0; k < list.Count; k++)
				{
					DeviceChunk deviceChunk3 = list[k];
					deviceChunk3.Rect.X = num4;
					num4 += deviceChunk3.TotalWidth;
				}
				break;
			}
			case TextAlign.Justify:
			{
				float num = (float)(MaxWidth - Width) / (float)(list.Count - 1);
				float num2 = 0f;
				for (int i = 0; i < list.Count; i++)
				{
					DeviceChunk deviceChunk = list[i];
					deviceChunk.Rect.X = (int)num2;
					num2 += (float)deviceChunk.TotalWidth;
					num2 += num;
				}
				break;
			}
			}
		}

		public void VertAlign(VertAlign align)
		{
			switch (align)
			{
			case HTMLEngine.Core.VertAlign.Top:
			{
				for (int j = 0; j < list.Count; j++)
				{
					DeviceChunk deviceChunk2 = list[j];
					deviceChunk2.Rect.Y = Y;
				}
				break;
			}
			case HTMLEngine.Core.VertAlign.Middle:
			{
				for (int k = 0; k < list.Count; k++)
				{
					DeviceChunk deviceChunk3 = list[k];
					deviceChunk3.Rect.Y = Y + Height / 2 - deviceChunk3.Rect.Height / 2;
				}
				break;
			}
			case HTMLEngine.Core.VertAlign.Bottom:
			{
				for (int i = 0; i < list.Count; i++)
				{
					DeviceChunk deviceChunk = list[i];
					deviceChunk.Rect.Y = Y + Height - deviceChunk.Rect.Height;
				}
				break;
			}
			}
		}

		public override string ToString()
		{
			return string.Format("Chunks:{0}", list.Count);
		}
	}
}

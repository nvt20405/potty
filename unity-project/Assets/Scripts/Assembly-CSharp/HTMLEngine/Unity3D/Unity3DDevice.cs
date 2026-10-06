using System.Collections.Generic;
using UnityEngine;

namespace HTMLEngine.Unity3D
{
	public class Unity3DDevice : HtDevice
	{
		private readonly Dictionary<string, Unity3DFont> fonts = new Dictionary<string, Unity3DFont>();

		private readonly Dictionary<string, Unity3DImage> images = new Dictionary<string, Unity3DImage>();

		private static Texture2D whiteTex;

		public override HtFont LoadFont(string face, int size, bool bold, bool italic)
		{
			string key = string.Format("{0}{1}{2}{3}", face, size, (!bold) ? string.Empty : "b", (!italic) ? string.Empty : "i");
			Unity3DFont value;
			if (fonts.TryGetValue(key, out value))
			{
				return value;
			}
			value = new Unity3DFont(face, size, bold, italic);
			fonts[key] = value;
			return value;
		}

		public override HtImage LoadImage(string src, int fps)
		{
			Unity3DImage value;
			if (images.TryGetValue(src, out value))
			{
				return value;
			}
			value = new Unity3DImage(src);
			images[src] = value;
			return value;
		}

		public override void FillRect(HtRect rect, HtColor color, object userData)
		{
			if (whiteTex == null)
			{
				whiteTex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
				whiteTex.SetPixel(0, 0, Color.white);
				whiteTex.Apply(false, true);
			}
			Color color2 = GUI.color;
			GUI.color = new Color32(color.R, color.G, color.B, color.A);
			GUI.DrawTexture(new Rect(rect.X, rect.Y, rect.Width, rect.Height), whiteTex);
			GUI.color = color2;
		}

		public override void OnRelease()
		{
		}
	}
}

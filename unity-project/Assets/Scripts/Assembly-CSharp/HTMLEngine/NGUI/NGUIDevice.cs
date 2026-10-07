using System.Collections.Generic;
using UnityEngine;

namespace HTMLEngine.NGUI
{
	public class NGUIDevice : HtDevice
	{
		private readonly Dictionary<string, NGUIFont> fonts = new Dictionary<string, NGUIFont>();

		private readonly Dictionary<string, NGUIImage> images = new Dictionary<string, NGUIImage>();

		private static Texture2D whiteTex;

		public override HtFont LoadFont(string face, int size, bool bold, bool italic)
		{
			string key = string.Format("{0}{1}{2}{3}", face, size, (!bold) ? string.Empty : "b", (!italic) ? string.Empty : "i");
			NGUIFont value;
			if (fonts.TryGetValue(key, out value))
			{
				return value;
			}
			value = new NGUIFont(face, size, bold, italic);
			fonts[key] = value;
			return value;
		}

		public override HtImage LoadImage(string src, int fps)
		{
			NGUIImage value;
			if (images.TryGetValue(src, out value))
			{
				return value;
			}
			value = new NGUIImage(src, fps);
			images[src] = value;
			return value;
		}

		public override void FillRect(HtRect rect, HtColor color, object userData)
		{
			Transform transform = (Transform)((userData is Transform) ? userData : null);
			if (transform != null)
			{
				if (whiteTex == null)
				{
					whiteTex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
					whiteTex.name = "HTMLEngineWhite";
					whiteTex.SetPixel(0, 0, Color.white);
					whiteTex.Apply(false, true);
				}
				GameObject gameObject = new GameObject("fill", typeof(UITexture));
				gameObject.layer = transform.gameObject.layer;
				gameObject.transform.parent = transform;
				gameObject.transform.localPosition = new Vector3(rect.X + rect.Width / 2, -rect.Y - rect.Height / 2 - 2, -1f);
				gameObject.transform.localScale = new Vector3(rect.Width, rect.Height, 1f);
				UITexture component = gameObject.GetComponent<UITexture>();
				component.pivot = UIWidget.Pivot.Center;
				component.mainTexture = whiteTex;
				component.color = new Color32(color.R, color.G, color.B, color.A);
				if (gameObject.transform.localScale.y == 0f)
				{
					gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1f, 1f);
				}
			}
			else
			{
				HtEngine.Log(HtLogLevel.Error, "Can't draw without root.");
			}
		}

		public override void OnRelease()
		{
			foreach (KeyValuePair<string, NGUIFont> font in fonts)
			{
				font.Value.OnRelease();
			}
		}
	}
}

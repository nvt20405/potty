using System;
using UnityEngine;

namespace HTMLEngine.Unity3D
{
	public class Unity3DImage : HtImage
	{
		private readonly bool isTime;

		private readonly GUIStyle timeStyle;

		public readonly Texture2D Texture;

		public override int Width
		{
			get
			{
				if (isTime)
				{
					return 120;
				}
				return (Texture == null) ? 1 : Texture.width;
			}
		}

		public override int Height
		{
			get
			{
				if (isTime)
				{
					return 20;
				}
				return (Texture == null) ? 1 : Texture.height;
			}
		}

		public Unity3DImage(string source)
		{
			if ("#time".Equals(source, StringComparison.InvariantCultureIgnoreCase))
			{
				isTime = true;
				timeStyle = new GUIStyle();
				GUIStyle gUIStyle = timeStyle;
				UnityEngine.Object obj = Resources.Load("fonts/code");
				gUIStyle.font = (Font)((obj is Font) ? obj : null);
				timeStyle.fontSize = 16;
				timeStyle.fontStyle = FontStyle.Normal;
				timeStyle.normal.textColor = Color.white;
				timeStyle.alignment = TextAnchor.MiddleCenter;
			}
			else
			{
				UnityEngine.Object obj2 = Resources.Load(source, typeof(Texture2D));
				Texture = (Texture2D)((obj2 is Texture2D) ? obj2 : null);
				if (Texture == null)
				{
					Debug.LogError("Could not load html image from " + source);
				}
			}
		}

		public override void Draw(string id, HtRect rect, HtColor color, string linkText, object userData)
		{
			if (isTime)
			{
				DateTime now = DateTime.Now;
				timeStyle.Draw(new Rect(rect.X, rect.Y, rect.Width, rect.Height), string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", now.Hour, now.Minute, now.Second, now.Millisecond), false, false, false, false);
			}
			else if (Texture != null)
			{
				Color color2 = GUI.color;
				if (!string.IsNullOrEmpty(id))
				{
					GUI.SetNextControlName(id);
				}
				GUI.color = new Color32(color.R, color.G, color.B, color.A);
				GUI.DrawTexture(new Rect(rect.X, rect.Y, rect.Width, rect.Height), Texture);
				GUI.color = color2;
			}
		}
	}
}

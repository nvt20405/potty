using HTMLEngine.Core;
using UnityEngine;

namespace HTMLEngine.Unity3D
{
	public class Unity3DFont : HtFont
	{
		public readonly GUIStyle style = new GUIStyle();

		public readonly GUIContent content = new GUIContent();

		private readonly int whiteSize;

		public override int LineSpacing
		{
			get
			{
				return (int)style.lineHeight;
			}
		}

		public override int WhiteSize
		{
			get
			{
				return whiteSize;
			}
		}

		public Unity3DFont(string face, int size, bool bold, bool italic)
			: base(face, size, bold, italic)
		{
			string text = string.Format("{0}{1}{2}{3}", face, size, (!bold) ? string.Empty : "b", (!italic) ? string.Empty : "i");
			GUIStyle gUIStyle = style;
			Object obj = Resources.Load("fonts/" + text, typeof(Font));
			gUIStyle.font = (Font)((obj is Font) ? obj : null);
			if (style.font == null)
			{
				Debug.LogError("Could not load font: " + text);
			}
			style.wordWrap = false;
			content.text = " .";
			whiteSize = (int)style.CalcSize(content).x;
			content.text = ".";
			whiteSize -= (int)style.CalcSize(content).x;
		}

		public override HtSize Measure(string text)
		{
			content.text = text;
			Vector2 vector = style.CalcSize(content);
			int num = text.Length;
			while (num > 0 && text[num - 1] == ' ')
			{
				vector.x += WhiteSize;
				num--;
			}
			return new HtSize((int)vector.x, (int)vector.y);
		}

		public override void Draw(string id, HtRect rect, HtColor color, string text, bool isEffect, DrawTextEffect effect, HtColor effectColor, int effectAmount, string linkText, object userData)
		{
			if (string.IsNullOrEmpty(id))
			{
				GUI.SetNextControlName(id);
			}
			content.text = text;
			style.normal.textColor = new Color32(color.R, color.G, color.B, color.A);
			style.Draw(new Rect(rect.X, rect.Y, rect.Width, rect.Height), content, false, false, false, false);
		}
	}
}

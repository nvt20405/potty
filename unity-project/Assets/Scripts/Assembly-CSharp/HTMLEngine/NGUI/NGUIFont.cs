using HTMLEngine.Core;
using UnityEngine;

namespace HTMLEngine.NGUI
{
	public class NGUIFont : HtFont
	{
		public UIFont uiFont;

		private readonly int whiteSize;

		public override int LineSpacing
		{
			get
			{
				return uiFont.size + uiFont.verticalSpacing;
			}
		}

		public override int WhiteSize
		{
			get
			{
				return whiteSize;
			}
		}

		public NGUIFont(string face, int size, bool bold, bool italic)
			: base(face, size, bold, italic)
		{
			UIFont uIFont = null;
			uIFont = ((face == "FontSmall") ? HtEngine.FontSmall : ((!(face == "ThuPhapSmall")) ? HtEngine.NormalSmall : HtEngine.ThuPhapSmall));
			if (uIFont == null)
			{
				Debug.LogError("Could not load font: " + face);
				return;
			}
			uiFont = Object.Instantiate(uIFont) as UIFont;
			Object.DontDestroyOnLoad(uiFont);
			GameObject gameObject = GameObject.Find("/cachedHtmlFonts");
			if (gameObject == null)
			{
				gameObject = new GameObject("cachedHtmlFonts");
				Object.DontDestroyOnLoad(gameObject);
			}
			uiFont.transform.parent = gameObject.transform;
			uiFont.name = face;
			whiteSize = (int)(uiFont.CalculatePrintedSize(" .", true, UIFont.SymbolStyle.None).x * (float)size);
			whiteSize -= (int)(uiFont.CalculatePrintedSize(".", true, UIFont.SymbolStyle.None).x * (float)size);
		}

		public override HtSize Measure(string text)
		{
			Vector2 vector = uiFont.CalculatePrintedSize(text, false, UIFont.SymbolStyle.None) * uiFont.size * 1.1f;
			return new HtSize((int)vector.x, (int)vector.y);
		}

		public override void Draw(string id, HtRect rect, HtColor color, string text, bool isEffect, DrawTextEffect effect, HtColor effectColor, int effectAmount, string linkText, object userData)
		{
			if (isEffect)
			{
				return;
			}
			Transform transform = (Transform)((userData is Transform) ? userData : null);
			if (transform != null)
			{
				GameObject gameObject = new GameObject((!string.IsNullOrEmpty(id)) ? id : "label", typeof(UILabel));
				gameObject.layer = transform.gameObject.layer;
				gameObject.transform.parent = transform;
				gameObject.transform.localPosition = new Vector3(rect.X + rect.Width / 2, -rect.Y - rect.Height / 2, 0f);
				gameObject.transform.localScale = new Vector3(uiFont.size, uiFont.size, 1f);
				UILabel component = gameObject.GetComponent<UILabel>();
				component.pivot = UIWidget.Pivot.Center;
				component.supportEncoding = false;
				component.font = uiFont;
				component.text = text;
				component.color = new Color32(color.R, color.G, color.B, color.A);
				switch (effect)
				{
				case DrawTextEffect.Outline:
					component.effectStyle = UILabel.Effect.Outline;
					break;
				case DrawTextEffect.Shadow:
					component.effectStyle = UILabel.Effect.Shadow;
					break;
				}
				component.effectColor = new Color32(effectColor.R, effectColor.G, effectColor.B, effectColor.A);
				component.effectDistance = new Vector2(effectAmount, effectAmount);
				component.MakePixelPerfect();
				if (!string.IsNullOrEmpty(linkText))
				{
					BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
					boxCollider.isTrigger = true;
					boxCollider.center = new Vector3(0f, 0f, -0.25f);
					boxCollider.size = new Vector3(component.relativeSize.x, 1f, 1f);
					NGUILinkText nGUILinkText = gameObject.AddComponent<NGUILinkText>();
					nGUILinkText.linkText = linkText;
					UIButtonColor uIButtonColor = gameObject.AddComponent<UIButtonColor>();
					uIButtonColor.tweenTarget = gameObject;
					uIButtonColor.hover = new Color32(HtEngine.LinkHoverColor.R, HtEngine.LinkHoverColor.G, HtEngine.LinkHoverColor.B, HtEngine.LinkHoverColor.A);
					uIButtonColor.pressed = new Color(component.color.r * HtEngine.LinkPressedFactor, component.color.g * HtEngine.LinkPressedFactor, component.color.b * HtEngine.LinkPressedFactor, component.color.a);
					uIButtonColor.duration = 0f;
					UIButtonMessage uIButtonMessage = gameObject.AddComponent<UIButtonMessage>();
					uIButtonMessage.target = transform.gameObject;
					uIButtonMessage.functionName = HtEngine.LinkFunctionName;
				}
			}
			else
			{
				HtEngine.Log(HtLogLevel.Error, "Can't draw without root.");
			}
		}

		public void OnRelease()
		{
			if (uiFont != null && (bool)uiFont)
			{
				Object.Destroy(uiFont.gameObject);
				uiFont = null;
			}
		}
	}
}

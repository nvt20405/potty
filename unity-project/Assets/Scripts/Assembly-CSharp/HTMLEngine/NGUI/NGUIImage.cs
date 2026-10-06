using System;
using HTMLEngine.Core;
using UnityEngine;

namespace HTMLEngine.NGUI
{
	public class NGUIImage : HtImage
	{
		private readonly bool isTime;

		private readonly HtFont timeFont;

		public readonly UIAtlas uiAtlas;

		public readonly string spriteName;

		public readonly bool isAnim;

		public readonly int FPS;

		public override int Width
		{
			get
			{
				if (isTime)
				{
					return 120;
				}
				if (uiAtlas == null)
				{
					return 1;
				}
				UIAtlas.Sprite sprite = null;
				if (isAnim)
				{
					int i = 0;
					for (int count = uiAtlas.spriteList.Count; i < count; i++)
					{
						UIAtlas.Sprite sprite2 = uiAtlas.spriteList[i];
						if (string.IsNullOrEmpty(spriteName) || sprite2.name.StartsWith(spriteName))
						{
							sprite = sprite2;
							break;
						}
					}
				}
				else
				{
					sprite = uiAtlas.GetSprite(spriteName);
				}
				return (sprite == null) ? 1 : ((int)sprite.outer.width);
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
				if (uiAtlas == null)
				{
					return 1;
				}
				UIAtlas.Sprite sprite = null;
				if (isAnim)
				{
					int i = 0;
					for (int count = uiAtlas.spriteList.Count; i < count; i++)
					{
						UIAtlas.Sprite sprite2 = uiAtlas.spriteList[i];
						if (string.IsNullOrEmpty(spriteName) || sprite2.name.StartsWith(spriteName))
						{
							sprite = sprite2;
							break;
						}
					}
				}
				else
				{
					sprite = uiAtlas.GetSprite(spriteName);
				}
				return (sprite == null) ? 1 : ((int)sprite.outer.height);
			}
		}

		public NGUIImage(string source, int fps)
		{
			spriteName = source;
			isAnim = fps >= 0;
			FPS = fps;
			uiAtlas = null;
			UIAtlas[] atlas = HtEngine.atlas;
			UIAtlas[] array = atlas;
			foreach (UIAtlas uIAtlas in array)
			{
				if (uIAtlas.GetSprite(spriteName) != null)
				{
					uiAtlas = uIAtlas;
					break;
				}
			}
			if (uiAtlas == null)
			{
				Debug.LogError("Could not found sprite" + spriteName);
			}
		}

		public override void Draw(string id, HtRect rect, HtColor color, string linkText, object userData)
		{
			if (isTime)
			{
				DateTime now = DateTime.Now;
				timeFont.Draw("time", rect, color, string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", now.Hour, now.Minute, now.Second, now.Millisecond), false, DrawTextEffect.None, HtColor.white, 0, linkText, userData);
			}
			else
			{
				if (!(uiAtlas != null))
				{
					return;
				}
				Transform transform = (Transform)((userData is Transform) ? userData : null);
				if (transform != null)
				{
					GameObject gameObject = new GameObject((!string.IsNullOrEmpty(id)) ? id : "image", typeof(UISprite));
					gameObject.layer = transform.gameObject.layer;
					gameObject.transform.parent = transform;
					gameObject.transform.localPosition = new Vector3(rect.X + rect.Width / 2, -rect.Y - rect.Height / 2, -1f);
					gameObject.transform.localScale = new Vector3(rect.Width, rect.Height, 1f);
					UISprite component = gameObject.GetComponent<UISprite>();
					component.pivot = UIWidget.Pivot.Center;
					component.atlas = uiAtlas;
					component.color = new Color32(color.R, color.G, color.B, color.A);
					if (isAnim)
					{
						UISpriteAnimation uISpriteAnimation = gameObject.AddComponent<UISpriteAnimation>();
						uISpriteAnimation.framesPerSecond = FPS;
						uISpriteAnimation.namePrefix = spriteName;
					}
					else
					{
						component.spriteName = spriteName;
						component.MakePixelPerfect();
						if (gameObject.transform.localScale.y == 0f)
						{
							gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1f, 1f);
						}
					}
					if (!string.IsNullOrEmpty(linkText))
					{
						BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
						boxCollider.isTrigger = true;
						boxCollider.center = new Vector3(0f, 0f, -0.25f);
						boxCollider.size = new Vector3(1f, 1f, 1f);
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
		}
	}
}

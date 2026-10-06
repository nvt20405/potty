using System.Collections.Generic;

namespace HTMLEngine.Core
{
	internal class DeviceChunkCollection : PoolableObject
	{
		private readonly List<DeviceChunkLine> list = new List<DeviceChunkLine>();

		internal readonly Stack<HtFont> fontStack = new Stack<HtFont>();

		internal readonly Stack<HtColor> colorStack = new Stack<HtColor>();

		private readonly Stack<TextAlign> alignStack = new Stack<TextAlign>();

		private readonly Stack<VertAlign> valignStack = new Stack<VertAlign>();

		public Dictionary<DeviceChunk, string> Links = new Dictionary<DeviceChunk, string>();

		public List<DeviceChunkLine> Lines
		{
			get
			{
				return list;
			}
		}

		internal override void OnAcquire()
		{
		}

		internal override void OnRelease()
		{
			Clear();
		}

		public void Clear(bool releaseItems = true)
		{
			if (releaseItems)
			{
				foreach (DeviceChunkLine item in list)
				{
					item.Dispose();
				}
			}
			Links.Clear();
			list.Clear();
			fontStack.Clear();
			colorStack.Clear();
			alignStack.Clear();
			valignStack.Clear();
		}

		private DeviceChunkDrawText AcquireDeviceChunkDrawText(string id, string text, HtFont font, HtColor color, DrawTextDeco deco, bool decoStop, bool prevIsWord)
		{
			DeviceChunkDrawText deviceChunkDrawText = OP<DeviceChunkDrawText>.Acquire();
			deviceChunkDrawText.Id = id;
			deviceChunkDrawText.Text = text;
			deviceChunkDrawText.Font = font;
			deviceChunkDrawText.Color = color;
			deviceChunkDrawText.Deco = deco;
			deviceChunkDrawText.DecoStop = decoStop;
			deviceChunkDrawText.PrevIsWord = prevIsWord;
			deviceChunkDrawText.MeasureSize();
			return deviceChunkDrawText;
		}

		private DeviceChunkDrawTextEffect AcquireDeviceChunkDrawTextEffect(string id, string text, HtFont font, HtColor color, DrawTextDeco deco, bool decoStop, DrawTextEffect effect, int effectAmount, HtColor effectColor, bool prevIsWord)
		{
			DeviceChunkDrawTextEffect deviceChunkDrawTextEffect = OP<DeviceChunkDrawTextEffect>.Acquire();
			deviceChunkDrawTextEffect.Id = id;
			deviceChunkDrawTextEffect.Text = text;
			deviceChunkDrawTextEffect.Font = font;
			deviceChunkDrawTextEffect.Color = color;
			deviceChunkDrawTextEffect.Deco = deco;
			deviceChunkDrawTextEffect.DecoStop = decoStop;
			deviceChunkDrawTextEffect.Effect = effect;
			deviceChunkDrawTextEffect.EffectAmount = effectAmount;
			deviceChunkDrawTextEffect.EffectColor = effectColor;
			deviceChunkDrawTextEffect.PrevIsWord = prevIsWord;
			deviceChunkDrawTextEffect.MeasureSize();
			return deviceChunkDrawTextEffect;
		}

		public void Parse(IEnumerator<HtmlChunk> htmlChunks, int viewportWidth, string id = null, HtFont font = null, HtColor color = default(HtColor), TextAlign align = TextAlign.Left, VertAlign valign = VertAlign.Bottom)
		{
			Clear();
			HtFont htFont = HtEngine.Device.LoadFont(HtEngine.DefaultFontFace, HtEngine.DefaultFontSize, false, false);
			font = ((font != null) ? font : htFont);
			color = ((color.R != 0 || color.G != 0 || color.B != 0 || color.A != 0) ? color : HtEngine.DefaultColor);
			DrawTextDeco drawTextDeco = DrawTextDeco.None;
			DrawTextEffect drawTextEffect = DrawTextEffect.None;
			HtColor effectColor = HtEngine.DefaultColor;
			int result = 1;
			string text = null;
			bool prevIsWord = false;
			DeviceChunkLine deviceChunkLine = null;
			DeviceChunkDrawText deviceChunkDrawText = null;
			while (htmlChunks.MoveNext())
			{
				HtmlChunk current = htmlChunks.Current;
				HtmlChunkWord htmlChunkWord = current as HtmlChunkWord;
				if (htmlChunkWord != null)
				{
					if (deviceChunkLine == null)
					{
						deviceChunkLine = NewLine(null, viewportWidth, align, valign);
					}
					deviceChunkDrawText = ((drawTextEffect != DrawTextEffect.None) ? AcquireDeviceChunkDrawTextEffect(id, htmlChunkWord.Text, font, color, drawTextDeco, deviceChunkDrawText != null && deviceChunkDrawText.Deco != drawTextDeco, drawTextEffect, result, effectColor, prevIsWord) : AcquireDeviceChunkDrawText(id, htmlChunkWord.Text, font, color, drawTextDeco, deviceChunkDrawText != null && deviceChunkDrawText.Deco != drawTextDeco, prevIsWord));
					if (text != null && !Links.ContainsKey(deviceChunkDrawText))
					{
						Links.Add(deviceChunkDrawText, text);
					}
					if (!deviceChunkLine.AddChunk(deviceChunkDrawText, prevIsWord))
					{
						prevIsWord = true;
						string text2 = deviceChunkDrawText.Text;
						deviceChunkDrawText.Dispose();
						deviceChunkDrawText = null;
						bool decoStop = deviceChunkDrawText != null && deviceChunkDrawText.Deco != drawTextDeco;
						int num = 0;
						int num2 = viewportWidth;
						while (num < text2.Length)
						{
							num2 -= font.Measure(text2[num].ToString()).Width;
							if (num2 < 0)
							{
								string text3 = text2.Substring(0, num);
								DeviceChunkDrawText deviceChunkDrawText2 = ((drawTextEffect != DrawTextEffect.None) ? AcquireDeviceChunkDrawTextEffect(id, text3, font, color, drawTextDeco, decoStop, drawTextEffect, result, effectColor, prevIsWord) : AcquireDeviceChunkDrawText(id, text3, font, color, drawTextDeco, decoStop, prevIsWord));
								deviceChunkLine = NewLine(deviceChunkLine, viewportWidth, align, valign);
								deviceChunkLine.AddChunk(deviceChunkDrawText2, prevIsWord);
								if (text != null && !Links.ContainsKey(deviceChunkDrawText2))
								{
									Links.Add(deviceChunkDrawText2, text);
								}
								text2 = text2.Substring(num);
								num = 0;
								num2 = viewportWidth;
							}
							else
							{
								num++;
							}
						}
						if (!string.IsNullOrEmpty(text2))
						{
							deviceChunkDrawText = ((drawTextEffect != DrawTextEffect.None) ? AcquireDeviceChunkDrawTextEffect(id, text2, font, color, drawTextDeco, decoStop, drawTextEffect, result, effectColor, prevIsWord) : AcquireDeviceChunkDrawText(id, text2, font, color, drawTextDeco, decoStop, prevIsWord));
							deviceChunkLine = NewLine(deviceChunkLine, viewportWidth, align, valign);
							deviceChunkLine.AddChunk(deviceChunkDrawText, prevIsWord);
							if (text != null && !Links.ContainsKey(deviceChunkDrawText))
							{
								Links.Add(deviceChunkDrawText, text);
							}
						}
					}
					prevIsWord = true;
				}
				else
				{
					prevIsWord = false;
				}
				HtmlChunkTag htmlChunkTag = current as HtmlChunkTag;
				if (htmlChunkTag == null)
				{
					continue;
				}
				string tag = htmlChunkTag.Tag;
				if (1 == 0)
				{
					continue;
				}
				switch (tag)
				{
				case "spin":
				{
					if (htmlChunkTag.IsSingle)
					{
						break;
					}
					if (htmlChunkTag.IsClosing)
					{
						id = null;
						FinishLine(deviceChunkLine, align, valign);
						return;
					}
					id = htmlChunkTag.GetAttr("id");
					ExctractAligns(htmlChunkTag, ref align, ref valign);
					DeviceChunkDrawCompiled deviceChunkDrawCompiled = OP<DeviceChunkDrawCompiled>.Acquire();
					deviceChunkDrawCompiled.Font = font;
					string s = htmlChunkTag.GetAttr("width") ?? "0";
					int result6 = 0;
					if (!int.TryParse(s, out result6))
					{
						result6 = 0;
					}
					if (result6 == 0)
					{
						result6 = ((deviceChunkLine != null) ? (deviceChunkLine.AvailWidth - font.WhiteSize) : viewportWidth);
					}
					if (result6 > 0)
					{
						if (result6 > viewportWidth)
						{
							result6 = viewportWidth;
						}
						deviceChunkDrawCompiled.Parse(htmlChunks, result6, id, font, color, align, valign);
						deviceChunkDrawCompiled.MeasureSize();
						if (deviceChunkLine == null)
						{
							deviceChunkLine = NewLine(null, viewportWidth, align, valign);
						}
						if (!deviceChunkLine.AddChunk(deviceChunkDrawCompiled, prevIsWord))
						{
							deviceChunkLine.IsFull = true;
							deviceChunkLine = NewLine(deviceChunkLine, viewportWidth, align, valign);
							if (!deviceChunkLine.AddChunk(deviceChunkDrawCompiled, prevIsWord))
							{
								HtEngine.Log(HtLogLevel.Error, "Could not fit spin into line. Word is too big: {0}", deviceChunkDrawText);
								deviceChunkDrawCompiled.Dispose();
								deviceChunkDrawCompiled = null;
							}
						}
					}
					else
					{
						HtEngine.Log(HtLogLevel.Warning, "spin width is not given");
					}
					break;
				}
				case "effect":
				{
					if (htmlChunkTag.IsSingle)
					{
						break;
					}
					if (htmlChunkTag.IsClosing)
					{
						drawTextEffect = DrawTextEffect.None;
						break;
					}
					string text4 = htmlChunkTag.GetAttr("name") ?? "outline";
					string text5 = text4;
					if (!(text5 == "shadow"))
					{
						if (text5 == "outline")
						{
							drawTextEffect = DrawTextEffect.Outline;
							result = 1;
							effectColor = HtColor.RGBA(byte.MaxValue, byte.MaxValue, byte.MaxValue, 80);
						}
					}
					else
					{
						drawTextEffect = DrawTextEffect.Shadow;
						result = 1;
						effectColor = HtColor.RGBA(0, 0, 0, 80);
					}
					string attr7 = htmlChunkTag.GetAttr("amount");
					if (attr7 != null && !int.TryParse(attr7, out result))
					{
						HtEngine.Log(HtLogLevel.Error, "Invalid numeric value: " + attr7);
					}
					string attr8 = htmlChunkTag.GetAttr("color");
					if (attr8 != null)
					{
						effectColor = HtColor.Parse(attr8);
					}
					break;
				}
				case "u":
					if (!htmlChunkTag.IsSingle)
					{
						drawTextDeco = ((!htmlChunkTag.IsClosing) ? (drawTextDeco | DrawTextDeco.Underline) : (drawTextDeco & ~DrawTextDeco.Underline));
					}
					break;
				case "s":
				case "strike":
					if (!htmlChunkTag.IsSingle)
					{
						drawTextDeco = ((!htmlChunkTag.IsClosing) ? (drawTextDeco | DrawTextDeco.Strike) : (drawTextDeco & ~DrawTextDeco.Strike));
					}
					break;
				case "code":
					if (!htmlChunkTag.IsSingle)
					{
						if (htmlChunkTag.IsClosing)
						{
							font = ((fontStack.Count <= 0) ? htFont : fontStack.Pop());
							break;
						}
						fontStack.Push(font);
						int size = font.Size;
						bool bold2 = font.Bold;
						bool italic2 = font.Italic;
						font = HtEngine.Device.LoadFont("code", size, bold2, italic2);
					}
					break;
				case "b":
					if (!htmlChunkTag.IsSingle)
					{
						if (htmlChunkTag.IsClosing)
						{
							font = ((fontStack.Count <= 0) ? htFont : fontStack.Pop());
							break;
						}
						fontStack.Push(font);
						string face2 = font.Face;
						int size2 = font.Size;
						bool italic3 = font.Italic;
						font = HtEngine.Device.LoadFont(face2, size2, true, italic3);
					}
					break;
				case "i":
					if (!htmlChunkTag.IsSingle)
					{
						if (htmlChunkTag.IsClosing)
						{
							font = ((fontStack.Count <= 0) ? htFont : fontStack.Pop());
							break;
						}
						fontStack.Push(font);
						string face3 = font.Face;
						int size3 = font.Size;
						bool bold3 = font.Bold;
						font = HtEngine.Device.LoadFont(face3, size3, bold3, true);
					}
					break;
				case "a":
					if (htmlChunkTag.IsSingle)
					{
						break;
					}
					if (htmlChunkTag.IsClosing)
					{
						id = null;
						if (colorStack.Count > 0)
						{
							color = colorStack.Pop();
						}
						text = null;
					}
					else
					{
						id = htmlChunkTag.GetAttr("id");
						text = htmlChunkTag.GetAttr("href");
						colorStack.Push(color);
						color = HtEngine.DefaultLinkColor;
					}
					break;
				case "font":
				{
					if (htmlChunkTag.IsSingle)
					{
						break;
					}
					if (htmlChunkTag.IsClosing)
					{
						font = ((fontStack.Count <= 0) ? htFont : fontStack.Pop());
						color = ((colorStack.Count <= 0) ? HtEngine.DefaultColor : colorStack.Pop());
						break;
					}
					fontStack.Push(font);
					colorStack.Push(color);
					string face = htmlChunkTag.GetAttr("face") ?? font.Face;
					string attr6 = htmlChunkTag.GetAttr("size");
					int result5;
					if (attr6 == null || !int.TryParse(attr6, out result5))
					{
						result5 = font.Size;
					}
					bool bold = font.Bold;
					bool italic = font.Italic;
					font = HtEngine.Device.LoadFont(face, result5, bold, italic);
					color = HtColor.Parse(htmlChunkTag.GetAttr("color"), color);
					break;
				}
				case "br":
					deviceChunkLine = NewLine(deviceChunkLine, viewportWidth, align, valign);
					deviceChunkLine.Height = font.LineSpacing;
					break;
				case "img":
				{
					if (htmlChunkTag.IsClosing)
					{
						break;
					}
					string attr = htmlChunkTag.GetAttr("src");
					string attr2 = htmlChunkTag.GetAttr("width");
					string attr3 = htmlChunkTag.GetAttr("height");
					string attr4 = htmlChunkTag.GetAttr("fps");
					string attr5 = htmlChunkTag.GetAttr("id");
					int result2;
					if (attr2 == null || !int.TryParse(attr2, out result2))
					{
						result2 = -1;
					}
					int result3;
					if (attr3 == null || !int.TryParse(attr3, out result3))
					{
						result3 = -1;
					}
					int result4;
					if (attr4 == null || !int.TryParse(attr4, out result4))
					{
						result4 = -1;
					}
					HtImage htImage = HtEngine.Device.LoadImage(attr, result4);
					if (result2 < 0)
					{
						result2 = htImage.Width;
					}
					if (result3 < 0)
					{
						result3 = htImage.Height;
					}
					DeviceChunkDrawImage deviceChunkDrawImage = OP<DeviceChunkDrawImage>.Acquire();
					if (deviceChunkLine == null)
					{
						deviceChunkLine = NewLine(null, viewportWidth, align, valign);
					}
					deviceChunkDrawImage.Image = htImage;
					deviceChunkDrawImage.Rect.Width = result2;
					deviceChunkDrawImage.Rect.Height = result3;
					deviceChunkDrawImage.Font = font;
					deviceChunkDrawImage.Id = attr5;
					if (text != null && !Links.ContainsKey(deviceChunkDrawImage))
					{
						Links.Add(deviceChunkDrawImage, text);
					}
					if (!deviceChunkLine.AddChunk(deviceChunkDrawImage, prevIsWord))
					{
						deviceChunkLine.IsFull = true;
						deviceChunkLine = NewLine(deviceChunkLine, viewportWidth, align, valign);
						if (!deviceChunkLine.AddChunk(deviceChunkDrawImage, prevIsWord))
						{
							HtEngine.Log(HtLogLevel.Error, "Could not fit image into line. Image is too big: {0}", deviceChunkDrawImage);
							deviceChunkDrawImage.Dispose();
						}
					}
					break;
				}
				case "p":
					if (htmlChunkTag.IsClosing)
					{
						id = null;
						break;
					}
					id = htmlChunkTag.GetAttr("id");
					deviceChunkLine = NewLine(deviceChunkLine, viewportWidth, align, valign);
					ExctractAligns(htmlChunkTag, ref align, ref valign);
					break;
				default:
					HtEngine.Log(HtLogLevel.Error, "Unsupported html tag {0}", htmlChunkTag);
					break;
				}
			}
			FinishLine(deviceChunkLine, align, valign);
		}

		private static void ExctractAligns(HtmlChunkTag tag, ref TextAlign align, ref VertAlign valign)
		{
			string attr = tag.GetAttr("ALIGN");
			if (attr != null)
			{
				switch (attr.ToUpperInvariant())
				{
				case "CENTER":
					align = TextAlign.Center;
					break;
				case "JUSTIFY":
					align = TextAlign.Justify;
					break;
				case "RIGHT":
					align = TextAlign.Right;
					break;
				case "LEFT":
					align = TextAlign.Left;
					break;
				default:
					HtEngine.Log(HtLogLevel.Warning, "Invalid attribute align: '{0}'", attr);
					align = TextAlign.Left;
					break;
				}
			}
			attr = tag.GetAttr("VALIGN");
			if (attr != null)
			{
				switch (attr.ToUpperInvariant())
				{
				case "MIDDLE":
					valign = VertAlign.Middle;
					return;
				case "TOP":
					valign = VertAlign.Top;
					return;
				case "BOTTOM":
					valign = VertAlign.Bottom;
					return;
				}
				HtEngine.Log(HtLogLevel.Warning, "Invalid attribute valign: '{0}'", attr);
				valign = VertAlign.Bottom;
			}
		}

		internal DeviceChunkLine NewLine(DeviceChunkLine prevLine, int viewPortWidth, TextAlign prevAlign, VertAlign prevVAlign)
		{
			int y = 0;
			if (prevLine != null)
			{
				FinishLine(prevLine, prevAlign, prevVAlign);
				y = prevLine.Y + prevLine.Height;
			}
			DeviceChunkLine deviceChunkLine = OP<DeviceChunkLine>.Acquire();
			deviceChunkLine.MaxWidth = viewPortWidth;
			deviceChunkLine.Y = y;
			list.Add(deviceChunkLine);
			return deviceChunkLine;
		}

		internal void FinishLine(DeviceChunkLine line, TextAlign align, VertAlign valign)
		{
			if (line != null)
			{
				line.HorzAlign(align);
				line.VertAlign(valign);
			}
		}
	}
}

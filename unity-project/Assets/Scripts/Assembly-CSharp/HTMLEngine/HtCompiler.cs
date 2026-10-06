using System.Collections.Generic;
using HTMLEngine.Core;

namespace HTMLEngine
{
	public class HtCompiler : PoolableObject
	{
		private readonly Reader reader = new Reader();

		private DeviceChunkCollection d;

		public int CompiledWidth { get; private set; }

		public int CompiledHeight { get; private set; }

		internal override void OnAcquire()
		{
			d = OP<DeviceChunkCollection>.Acquire();
		}

		internal override void OnRelease()
		{
			d.Dispose();
			d = null;
		}

		public string GetLink(int x, int y)
		{
			if (d != null)
			{
				foreach (KeyValuePair<DeviceChunk, string> link in d.Links)
				{
					if (link.Key.Contains(x, y))
					{
						return link.Value;
					}
				}
			}
			return null;
		}

		public void Compile(string source, int width)
		{
			reader.SetSource(source);
			using (HtmlChunkCollection htmlChunkCollection = OP<HtmlChunkCollection>.Acquire())
			{
				htmlChunkCollection.Read(reader);
				Compile(htmlChunkCollection.GetEnumerator(), width);
			}
		}

		internal void Compile(IEnumerator<HtmlChunk> source, int width, string id = null, HtFont font = null, HtColor color = default(HtColor), TextAlign align = TextAlign.Left, VertAlign valign = VertAlign.Bottom)
		{
			d.Clear();
			CompiledWidth = width;
			d.Parse(source, width, id, font, color, align, valign);
			MergeSameTextChunks();
			UpdateHeight();
		}

		private void UpdateHeight()
		{
			if (d.Lines.Count > 0)
			{
				DeviceChunkLine deviceChunkLine = d.Lines[d.Lines.Count - 1];
				CompiledHeight = deviceChunkLine.Y + deviceChunkLine.Height;
			}
			else
			{
				CompiledHeight = 0;
			}
		}

		private void MergeSameTextChunks()
		{
			if (d == null)
			{
				return;
			}
			for (int i = 0; i < d.Lines.Count; i++)
			{
				DeviceChunkLine deviceChunkLine = d.Lines[i];
				DeviceChunk deviceChunk = null;
				int num = 0;
				while (num < deviceChunkLine.Chunks.Count)
				{
					DeviceChunk deviceChunk2 = deviceChunkLine.Chunks[num];
					if (deviceChunk == null)
					{
						deviceChunk = deviceChunk2;
						num++;
						continue;
					}
					string value;
					d.Links.TryGetValue(deviceChunk, out value);
					string value2;
					d.Links.TryGetValue(deviceChunk2, out value2);
					if (string.Equals(value, value2))
					{
						DeviceChunkDrawTextEffect deviceChunkDrawTextEffect = deviceChunk as DeviceChunkDrawTextEffect;
						DeviceChunkDrawTextEffect deviceChunkDrawTextEffect2 = deviceChunk2 as DeviceChunkDrawTextEffect;
						if (deviceChunkDrawTextEffect != null && deviceChunkDrawTextEffect2 != null)
						{
							if (deviceChunkDrawTextEffect.Font.Equals(deviceChunkDrawTextEffect2.Font) && deviceChunkDrawTextEffect.Deco == deviceChunkDrawTextEffect2.Deco && deviceChunkDrawTextEffect.Color.R == deviceChunkDrawTextEffect2.Color.R && deviceChunkDrawTextEffect.Color.G == deviceChunkDrawTextEffect2.Color.G && deviceChunkDrawTextEffect.Color.B == deviceChunkDrawTextEffect2.Color.B && deviceChunkDrawTextEffect.Color.A == deviceChunkDrawTextEffect2.Color.A && (deviceChunkDrawTextEffect.DecoStop || (!deviceChunkDrawTextEffect.DecoStop && !deviceChunkDrawTextEffect2.DecoStop)) && deviceChunkDrawTextEffect.Effect == deviceChunkDrawTextEffect2.Effect && deviceChunkDrawTextEffect.EffectColor.R == deviceChunkDrawTextEffect2.EffectColor.R && deviceChunkDrawTextEffect.EffectColor.G == deviceChunkDrawTextEffect2.EffectColor.G && deviceChunkDrawTextEffect.EffectColor.B == deviceChunkDrawTextEffect2.EffectColor.B && deviceChunkDrawTextEffect.EffectColor.A == deviceChunkDrawTextEffect2.EffectColor.A && deviceChunkDrawTextEffect.EffectAmount == deviceChunkDrawTextEffect2.EffectAmount)
							{
								if (deviceChunkDrawTextEffect2.PrevIsWord)
								{
									deviceChunkDrawTextEffect.Text = string.Concat(deviceChunkDrawTextEffect, " ", deviceChunkDrawTextEffect2.Text);
									deviceChunkDrawTextEffect.Rect.Width += deviceChunkDrawTextEffect.Font.WhiteSize + deviceChunkDrawTextEffect2.Rect.Width;
								}
								else
								{
									deviceChunkDrawTextEffect.Text = string.Concat(deviceChunkDrawTextEffect, deviceChunkDrawTextEffect2.Text);
									deviceChunkDrawTextEffect.Rect.Width += deviceChunkDrawTextEffect2.Rect.Width;
								}
								deviceChunkLine.Chunks.RemoveAt(num);
								deviceChunkDrawTextEffect2.Dispose();
								deviceChunkDrawTextEffect2 = null;
								continue;
							}
						}
						else if (deviceChunkDrawTextEffect == null && deviceChunkDrawTextEffect2 == null)
						{
							DeviceChunkDrawText deviceChunkDrawText = deviceChunk as DeviceChunkDrawText;
							DeviceChunkDrawText deviceChunkDrawText2 = deviceChunk2 as DeviceChunkDrawText;
							if (deviceChunkDrawText != null && deviceChunkDrawText2 != null && deviceChunkDrawText.Font.Equals(deviceChunkDrawText2.Font) && deviceChunkDrawText.Deco == deviceChunkDrawText2.Deco && deviceChunkDrawText.Color.R == deviceChunkDrawText2.Color.R && deviceChunkDrawText.Color.G == deviceChunkDrawText2.Color.G && deviceChunkDrawText.Color.B == deviceChunkDrawText2.Color.B && deviceChunkDrawText.Color.A == deviceChunkDrawText2.Color.A && (deviceChunkDrawText.DecoStop || (!deviceChunkDrawText.DecoStop && !deviceChunkDrawText2.DecoStop)))
							{
								if (deviceChunkDrawText2.PrevIsWord)
								{
									deviceChunkDrawText.Text = string.Concat(deviceChunkDrawText, " ", deviceChunkDrawText2.Text);
									deviceChunkDrawText.Rect.Width += deviceChunkDrawText.Font.WhiteSize + deviceChunkDrawText2.Rect.Width;
								}
								else
								{
									deviceChunkDrawText.Text = string.Concat(deviceChunkDrawText, deviceChunkDrawText2.Text);
									deviceChunkDrawText.Rect.Width += deviceChunkDrawText2.Rect.Width;
								}
								deviceChunkLine.Chunks.RemoveAt(num);
								deviceChunkDrawText2.Dispose();
								deviceChunkDrawText2 = null;
								continue;
							}
						}
					}
					deviceChunk = deviceChunk2;
					num++;
				}
			}
		}

		public void Draw(float deltaTime, object userData = null)
		{
			if (d == null)
			{
				return;
			}
			for (int i = 0; i < d.Lines.Count; i++)
			{
				DeviceChunkLine deviceChunkLine = d.Lines[i];
				for (int j = 0; j < deviceChunkLine.Chunks.Count; j++)
				{
					DeviceChunk deviceChunk = deviceChunkLine.Chunks[j];
					string value;
					if (d.Links.TryGetValue(deviceChunk, out value))
					{
						deviceChunk.Draw(deltaTime, value, userData);
					}
					else
					{
						deviceChunk.Draw(deltaTime, null, userData);
					}
				}
			}
		}

		public void Offset(int dx, int dy)
		{
			if (d == null)
			{
				return;
			}
			for (int i = 0; i < d.Lines.Count; i++)
			{
				DeviceChunkLine deviceChunkLine = d.Lines[i];
				for (int j = 0; j < deviceChunkLine.Chunks.Count; j++)
				{
					DeviceChunk deviceChunk = deviceChunkLine.Chunks[j];
					deviceChunk.Rect.X += dx;
					deviceChunk.Rect.Y += dy;
				}
			}
		}
	}
}

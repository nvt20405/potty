using System.Collections.Generic;

namespace HTMLEngine.Core
{
	internal class DeviceChunkDrawCompiled : DeviceChunk
	{
		public HtCompiler compiled;

		private bool offsetApplied;

		public void Parse(IEnumerator<HtmlChunk> source, int width, string id = null, HtFont font = null, HtColor color = default(HtColor), TextAlign align = TextAlign.Left, VertAlign valign = VertAlign.Bottom)
		{
			compiled.Compile(source, width, id, font, color, align, valign);
			offsetApplied = false;
		}

		internal override void OnAcquire()
		{
			offsetApplied = false;
			compiled = HtEngine.GetCompiler();
			base.OnAcquire();
		}

		internal override void OnRelease()
		{
			compiled.Dispose();
			compiled = null;
			base.OnRelease();
		}

		public override void Draw(float deltaTime, string linkText, object userData)
		{
			if (!offsetApplied)
			{
				compiled.Offset(Rect.X, Rect.Y);
				offsetApplied = true;
			}
			compiled.Draw(deltaTime, userData);
		}

		public override void MeasureSize()
		{
			Rect.Width = compiled.CompiledWidth;
			Rect.Height = compiled.CompiledHeight;
		}
	}
}

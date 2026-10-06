namespace HTMLEngine.Core
{
	internal class DeviceChunkDrawTextEffect : DeviceChunkDrawText
	{
		public DrawTextEffect Effect;

		public HtColor EffectColor;

		public int EffectAmount;

		public override void Draw(float deltaTime, string linkText, object userData)
		{
			bool flag = Text.Length == 1 && Text[0] <= ' ';
			switch (Effect)
			{
			case DrawTextEffect.Shadow:
				if (!flag)
				{
					Font.Draw(null, Rect.Offset(EffectAmount, EffectAmount), EffectColor, Text, true, Effect, EffectColor, EffectAmount, null, userData);
				}
				break;
			case DrawTextEffect.Outline:
				if (!flag)
				{
					Font.Draw(null, Rect.Offset(EffectAmount, 0), EffectColor, Text, true, Effect, EffectColor, EffectAmount, null, userData);
					Font.Draw(null, Rect.Offset(-EffectAmount, 0), EffectColor, Text, true, Effect, EffectColor, EffectAmount, null, userData);
					Font.Draw(null, Rect.Offset(0, EffectAmount), EffectColor, Text, true, Effect, EffectColor, EffectAmount, null, userData);
					Font.Draw(null, Rect.Offset(0, -EffectAmount), EffectColor, Text, true, Effect, EffectColor, EffectAmount, null, userData);
				}
				break;
			}
			HtDevice device = HtEngine.Device;
			if ((Deco & DrawTextDeco.Underline) != DrawTextDeco.None)
			{
				device.FillRect(new HtRect(Rect.X, Rect.Bottom - 2, (!DecoStop) ? base.TotalWidth : Rect.Width, 1), Color, userData);
			}
			if ((Deco & DrawTextDeco.Strike) != DrawTextDeco.None)
			{
				device.FillRect(new HtRect(Rect.X, Rect.Bottom - Rect.Height / 2 - 1, (!DecoStop) ? base.TotalWidth : Rect.Width, 1), Color, userData);
			}
			Font.Draw(Id, Rect, Color, Text, false, Effect, EffectColor, EffectAmount, linkText, userData);
		}
	}
}

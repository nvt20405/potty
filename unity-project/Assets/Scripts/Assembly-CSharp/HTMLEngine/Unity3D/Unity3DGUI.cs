using UnityEngine;

namespace HTMLEngine.Unity3D
{
	public static class Unity3DGUI
	{
		public static float lastCompilerTookSeconds;

		public static float lastDrawTookSeconds;

		public static void Label(Rect rect, string htmlText)
		{
			if (string.IsNullOrEmpty(htmlText))
			{
				return;
			}
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			using (HtCompiler htCompiler = HtEngine.GetCompiler())
			{
				htCompiler.Compile(htmlText, (int)rect.width);
				lastCompilerTookSeconds = Time.realtimeSinceStartup - realtimeSinceStartup;
				realtimeSinceStartup = Time.realtimeSinceStartup;
				GUI.BeginGroup(rect);
				htCompiler.Draw(Time.deltaTime);
				GUI.EndGroup();
				lastDrawTookSeconds = Time.realtimeSinceStartup - realtimeSinceStartup;
			}
		}
	}
}

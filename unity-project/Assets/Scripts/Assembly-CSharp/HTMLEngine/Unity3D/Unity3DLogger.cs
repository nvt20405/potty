using UnityEngine;

namespace HTMLEngine.Unity3D
{
	public class Unity3DLogger : HtLogger
	{
		public override void Log(HtLogLevel level, string message)
		{
			switch (level)
			{
			case HtLogLevel.Debug:
				Debug.Log("[DEBUG]" + message);
				break;
			case HtLogLevel.Info:
				Debug.Log("[INFO]" + message);
				break;
			case HtLogLevel.Warning:
				Debug.LogWarning("[WARN]" + message);
				break;
			case HtLogLevel.Error:
				Debug.LogError("[ERROR]" + message);
				break;
			}
		}
	}
}

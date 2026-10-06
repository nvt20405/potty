using UnityEngine;

public class EGDebug
{
	public static void LogError(string msg)
	{
		Debug.LogError(msg);
	}

	public static void LogError(string msg, Object obj)
	{
		Debug.LogError(msg, obj);
	}

	public static void Log(string msg)
	{
		Debug.Log(msg);
	}

	public static void Log(string msg, Object obj)
	{
		Debug.Log(msg, obj);
	}

	public static void LogWarning(string msg)
	{
		Debug.LogWarning(msg);
	}

	public static void LogWarning(string msg, Object obj)
	{
		Debug.LogWarning(msg, obj);
	}

	public static void Break()
	{
		Debug.Break();
	}
}

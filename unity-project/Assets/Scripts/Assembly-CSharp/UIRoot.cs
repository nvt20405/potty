using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/UI/Root")]
[ExecuteInEditMode]
public class UIRoot : MonoBehaviour
{
	public enum Scaling
	{
		PixelPerfect = 0,
		FixedSize = 1,
		FixedSizeOnMobiles = 2
	}

	private static List<UIRoot> mRoots = new List<UIRoot>();

	public Scaling scalingStyle = Scaling.FixedSize;

	[HideInInspector]
	public bool automatic;

	public int manualHeight = 720;

	public int manualWidth = 640;

	public bool fitWidthOnTallScreens = true;

	public int minimumHeight = 320;

	public int maximumHeight = 1536;

	private Transform mTrans;

	public static List<UIRoot> list
	{
		get
		{
			return mRoots;
		}
	}

	public int activeHeight
	{
		get
		{
			int num = Mathf.Max(2, Screen.height);
			if (scalingStyle == Scaling.FixedSize)
			{
				return GetFixedHeight();
			}
			if (scalingStyle == Scaling.FixedSizeOnMobiles)
			{
				return GetFixedHeight();
			}
			if (num < minimumHeight)
			{
				return minimumHeight;
			}
			if (num > maximumHeight)
			{
				return maximumHeight;
			}
			return num;
		}
	}

	public float pixelSizeAdjustment
	{
		get
		{
			return GetPixelSizeAdjustment(Screen.height);
		}
	}

	public static float GetPixelSizeAdjustment(GameObject go)
	{
		UIRoot uIRoot = NGUITools.FindInParents<UIRoot>(go);
		return (!(uIRoot != null)) ? 1f : uIRoot.pixelSizeAdjustment;
	}

	public float GetPixelSizeAdjustment(int height)
	{
		height = Mathf.Max(2, height);
		if (scalingStyle == Scaling.FixedSize)
		{
			return (float)GetFixedHeight() / (float)height;
		}
		if (scalingStyle == Scaling.FixedSizeOnMobiles)
		{
			return (float)GetFixedHeight() / (float)height;
		}
		if (height < minimumHeight)
		{
			return (float)minimumHeight / (float)height;
		}
		if (height > maximumHeight)
		{
			return (float)maximumHeight / (float)height;
		}
		return 1f;
	}

	private int GetFixedHeight()
	{
		int target = Mathf.Max(2, manualHeight);
		if (Application.isPlaying && fitWidthOnTallScreens && manualWidth > 0 && Screen.width > 0 && Screen.height > 0)
		{
			float screenAspect = (float)Screen.width / (float)Screen.height;
			float designAspect = (float)manualWidth / (float)Mathf.Max(1, manualHeight);
			if (screenAspect < designAspect)
			{
				target = Mathf.CeilToInt((float)manualWidth / screenAspect);
			}
		}
		int max = Mathf.Max(minimumHeight, maximumHeight);
		return Mathf.Clamp(target, Mathf.Max(2, minimumHeight), max);
	}

	private void Awake()
	{
		mTrans = base.transform;
		mRoots.Add(this);
		if (automatic)
		{
			scalingStyle = Scaling.PixelPerfect;
			automatic = false;
		}
	}

	private void OnDestroy()
	{
		mRoots.Remove(this);
	}

	private void Start()
	{
		UIOrthoCamera componentInChildren = GetComponentInChildren<UIOrthoCamera>();
		if (componentInChildren != null)
		{
			EGDebug.LogWarning("UIRoot should not be active at the same time as UIOrthoCamera. Disabling UIOrthoCamera.", componentInChildren);
			Camera component = componentInChildren.gameObject.GetComponent<Camera>();
			componentInChildren.enabled = false;
			if (component != null)
			{
				component.orthographicSize = 1f;
			}
		}
		else
		{
			Update();
		}
	}

	private void Update()
	{
		if (!(mTrans != null))
		{
			return;
		}
		float num = activeHeight;
		if (num > 0f)
		{
			float num2 = 2f / num;
			Vector3 localScale = mTrans.localScale;
			if (!(Mathf.Abs(localScale.x - num2) <= float.Epsilon) || !(Mathf.Abs(localScale.y - num2) <= float.Epsilon) || !(Mathf.Abs(localScale.z - num2) <= float.Epsilon))
			{
				mTrans.localScale = new Vector3(num2, num2, num2);
			}
		}
	}

	public static void Broadcast(string funcName)
	{
		int i = 0;
		for (int count = mRoots.Count; i < count; i++)
		{
			UIRoot uIRoot = mRoots[i];
			if (uIRoot != null)
			{
				uIRoot.BroadcastMessage(funcName, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public static void Broadcast(string funcName, object param)
	{
		if (param == null)
		{
			EGDebug.LogError("SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
			return;
		}
		int i = 0;
		for (int count = mRoots.Count; i < count; i++)
		{
			UIRoot uIRoot = mRoots[i];
			if (uIRoot != null)
			{
				uIRoot.BroadcastMessage(funcName, param, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}

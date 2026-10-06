using System;
using System.IO;
using System.Text;
using UnityEngine;

public class DebugCmd : MonoBehaviour
{
	private class UiAction : AndroidJavaProxy
	{
		private Action mAct;

		public UiAction(Action a)
			: base("java.lang.Runnable")
		{
			mAct = a;
		}

		public void run()
		{
			try
			{
				mAct();
			}
			catch (Exception ex)
			{
				EGDebug.LogError("[DebugCmd] ui " + ((ex != null) ? ex.ToString() : null));
			}
		}
	}

	private const string FALLBACK_DIR = "/storage/emulated/0/Android/data/vn.shg.mobi.mongvolam/files";

	private static string mDir;

	private static int mRealW = -1;

	private static int mRealH = -1;

	private static int mWinTop = -1;

	private static DebugCmd mInstance;

	private float mNext;

	private string mLast = "";

	private static string CmdPath
	{
		get
		{
			return Dir() + "/mvl_cmd.txt";
		}
	}

	private static string OutPath
	{
		get
		{
			return Dir() + "/mvl_out.txt";
		}
	}

	private static string Dir()
	{
		if (mDir != null)
		{
			return mDir;
		}
		try
		{
			string persistentDataPath = Application.persistentDataPath;
			if (!string.IsNullOrEmpty(persistentDataPath) && Directory.Exists(persistentDataPath) && persistentDataPath.StartsWith("/storage"))
			{
				mDir = persistentDataPath;
				return mDir;
			}
		}
		catch (Exception)
		{
		}
		mDir = "/storage/emulated/0/Android/data/vn.shg.mobi.mongvolam/files";
		return mDir;
	}

	private static void LoadMetrics()
	{
		if (mRealH > 0)
		{
			return;
		}
		mRealW = Screen.width;
		mRealH = Screen.height;
		mWinTop = 0;
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getWindowManager", new object[0]).Call<AndroidJavaObject>("getDefaultDisplay", new object[0]);
			AndroidJavaObject androidJavaObject3 = new AndroidJavaObject("android.util.DisplayMetrics");
			androidJavaObject2.Call("getRealMetrics", androidJavaObject3);
			mRealW = androidJavaObject3.Get<int>("widthPixels");
			mRealH = androidJavaObject3.Get<int>("heightPixels");
			AndroidJavaObject androidJavaObject4 = androidJavaObject.Call<AndroidJavaObject>("getWindow", new object[0]).Call<AndroidJavaObject>("getDecorView", new object[0]);
			AndroidJavaObject androidJavaObject5 = androidJavaObject4.Call<AndroidJavaObject>("getRootWindowInsets", new object[0]);
			if (androidJavaObject5 != null)
			{
				mWinTop = androidJavaObject5.Call<int>("getSystemWindowInsetTop", new object[0]);
			}
		}
		catch (Exception ex)
		{
			EGDebug.LogError("[DebugCmd] metrics " + ex.Message);
		}
		if (mWinTop <= 0)
		{
			mWinTop = mRealH - Screen.height;
		}
	}

	public static void Ensure()
	{
		if (!(mInstance != null))
		{
			GameObject gameObject = new GameObject();
			gameObject.name = "DebugCmd";
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			mInstance = gameObject.AddComponent<DebugCmd>();
			EGDebug.Log("[DebugCmd] armed dir=" + Dir());
			TryWake();
		}
	}

	private static void TryWake()
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject act = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			if (act != null)
			{
				AndroidJavaObject androidJavaObject = act.Call<AndroidJavaObject>("getSystemService", new object[1] { "power" });
				AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("android.os.PowerManager");
				int num = androidJavaClass2.GetStatic<int>("SCREEN_BRIGHT_WAKE_LOCK") | androidJavaClass2.GetStatic<int>("ACQUIRE_CAUSES_WAKEUP");
				AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("newWakeLock", new object[2] { num, "mvl:wake" });
				androidJavaObject2.Call("acquire", 120000L);
				EGDebug.Log("[DebugCmd] wakelock acquired");
				act.Call("runOnUiThread", new UiAction(() =>
				{
					AndroidJavaObject androidJavaObject3 = act.Call<AndroidJavaObject>("getWindow", new object[0]);
					AndroidJavaClass androidJavaClass3 = new AndroidJavaClass("android.view.WindowManager$LayoutParams");
					int num2 = androidJavaClass3.GetStatic<int>("FLAG_KEEP_SCREEN_ON") | androidJavaClass3.GetStatic<int>("FLAG_TURN_SCREEN_ON") | androidJavaClass3.GetStatic<int>("FLAG_SHOW_WHEN_LOCKED");
					androidJavaObject3.Call("addFlags", num2);
					EGDebug.Log("[DebugCmd] window flags ok");
				}));
			}
		}
		catch (Exception ex)
		{
			EGDebug.LogError("[DebugCmd] wake " + ((ex != null) ? ex.ToString() : null));
		}
	}

	private void Update()
	{
		if (Time.realtimeSinceStartup < mNext)
		{
			return;
		}
		mNext = Time.realtimeSinceStartup + 0.4f;
		try
		{
			if (!File.Exists(CmdPath))
			{
				return;
			}
			string text = File.ReadAllText(CmdPath).Trim();
			if (text.Length == 0 || text == mLast)
			{
				return;
			}
			mLast = text;
			EGDebug.Log("[DebugCmd] cmd: " + text);
			Run(text);
			try
			{
				File.Delete(CmdPath);
			}
			catch (Exception)
			{
			}
		}
		catch (Exception ex2)
		{
			EGDebug.LogError("[DebugCmd] " + ((ex2 != null) ? ex2.ToString() : null));
		}
	}

	private void Run(string cmd)
	{
		string[] array = cmd.Split(new char[2] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
		if (array.Length != 0)
		{
			string text = array[0].ToLowerInvariant();
			if (text == "screen" && array.Length >= 2)
			{
				ScreenByName(array[1]);
			}
			else if (text == "click" && array.Length >= 3)
			{
				Click(ParseInt(array[1]), ParseInt(array[2]));
			}
			else if (text == "tree" && array.Length >= 2)
			{
				DumpTreeCmd(array[1]);
			}
			else if (text == "info")
			{
				WriteInfo();
			}
			else if (text == "log")
			{
				EGDebug.Log("[DebugCmd] " + cmd);
			}
			else
			{
				EGDebug.LogError("[DebugCmd] unknown command: " + cmd);
			}
		}
	}

	private static int ParseInt(string s)
	{
		int result = 0;
		int.TryParse(s, out result);
		return result;
	}

	private void DumpTreeCmd(string rootName)
	{
		StringBuilder stringBuilder = new StringBuilder();
		try
		{
			GameObject gameObject = GameObject.Find(rootName);
			if (gameObject == null && GUIManager.instance != null && GUIManager.instance.GUI2DRoot != null)
			{
				Transform[] componentsInChildren = GUIManager.instance.GUI2DRoot.GetComponentsInChildren<Transform>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					if (string.Equals(componentsInChildren[i].name, rootName, StringComparison.OrdinalIgnoreCase))
					{
						gameObject = componentsInChildren[i].gameObject;
						break;
					}
				}
			}
			if (gameObject != null)
			{
				DumpTreeRecursive(gameObject.transform, 0, stringBuilder);
			}
			else
			{
				stringBuilder.Append("not found: ").Append(rootName).Append('\n');
			}
		}
		catch (Exception ex)
		{
			stringBuilder.Append("ERR ").Append(ex.Message).Append('\n');
		}
		File.WriteAllText(OutPath, stringBuilder.ToString());
	}

	private static void DumpTreeRecursive(Transform t, int depth, StringBuilder sb)
	{
		if (!(t == null) && depth <= 10)
		{
			for (int i = 0; i < depth; i++)
			{
				sb.Append("  ");
			}
			sb.Append(t.name);
			if (!t.gameObject.activeInHierarchy)
			{
				sb.Append(" [INACTIVE]");
			}
			sb.Append(" pos=").Append(t.localPosition.x.ToString("F0")).Append(',')
				.Append(t.localPosition.y.ToString("F0"));
			UIAnchor component = t.GetComponent<UIAnchor>();
			if (component != null)
			{
				sb.Append(" anchor=").Append(component.side).Append(" off=")
					.Append(component.pixelOffset.x.ToString("F0"))
					.Append(',')
					.Append(component.pixelOffset.y.ToString("F0"));
			}
			UIWidget component2 = t.GetComponent<UIWidget>();
			if (component2 != null)
			{
				sb.Append(' ').Append(component2.GetType().Name).Append(' ')
					.Append(t.localScale.x.ToString("F0"))
					.Append('x')
					.Append(t.localScale.y.ToString("F0"))
					.Append(" pv=")
					.Append(component2.pivot);
			}
			sb.Append('\n');
			for (int j = 0; j < t.childCount; j++)
			{
				DumpTreeRecursive(t.GetChild(j), depth + 1, sb);
			}
		}
	}

	private void ScreenByName(string name)
	{
		if (GUIManager.instance == null)
		{
			EGDebug.LogError("[DebugCmd] no GUIManager");
			return;
		}
		string[] names = Enum.GetNames(typeof(GAME_SCREEN));
		for (int i = 0; i < names.Length; i++)
		{
			if (string.Equals(names[i], name, StringComparison.OrdinalIgnoreCase))
			{
				GAME_SCREEN screen = (GAME_SCREEN)Enum.Parse(typeof(GAME_SCREEN), names[i]);
				EGDebug.Log("[DebugCmd] SetScreen " + screen);
				GUIManager.instance.SetScreen(screen);
				WriteInfo();
				return;
			}
		}
		EGDebug.LogError("[DebugCmd] unknown screen: " + name);
	}

	private void Click(int px, int py)
	{
		LoadMetrics();
		float x = (float)px - (float)(mRealW - Screen.width) * 0.5f;
		float num = (float)py - (float)mWinTop;
		if (num < 0f)
		{
			num = 0f;
		}
		if (num > (float)Screen.height)
		{
			num = Screen.height;
		}
		EGDebug.Log("[DebugCmd] click shot " + px + "," + py + " -> win " + x.ToString("F0") + "," + num.ToString("F0"));
		Vector3 inPos = new Vector3(x, (float)Screen.height - num, 0f);
		RaycastHit hit;
		if (!UICamera.Raycast(inPos, out hit))
		{
			EGDebug.Log("[DebugCmd] click miss " + px + "," + py);
			return;
		}
		GameObject gameObject = hit.collider.gameObject;
		EGDebug.Log("[DebugCmd] click -> " + gameObject.name);
		UICamera.Notify(gameObject, "OnPress", true);
		UICamera.Notify(gameObject, "OnClick", null);
		UICamera.Notify(gameObject, "OnPress", false);
	}

	private void WriteInfo()
	{
		StringBuilder stringBuilder = new StringBuilder();
		try
		{
			LoadMetrics();
			stringBuilder.Append("screen=").Append(Screen.width).Append("x")
				.Append(Screen.height)
				.Append(" real=")
				.Append(mRealW)
				.Append('x')
				.Append(mRealH)
				.Append(" winTop=")
				.Append(mWinTop)
				.Append('\n');
			stringBuilder.Append("dir=").Append(Dir()).Append('\n');
			if (GUIManager.instance != null)
			{
				GUIManager instance = GUIManager.instance;
				stringBuilder.Append("current=").Append(instance.CurrentScreen).Append('\n');
				if (instance.GUI2DRoot != null)
				{
					stringBuilder.Append("manualHeight=").Append(instance.GUI2DRoot.manualHeight).Append('\n');
					stringBuilder.Append("rootPos=").Append(instance.GUI2DRoot.transform.position).Append('\n');
				}
				if (instance.cam2D != null)
				{
					stringBuilder.Append("camPos=").Append(instance.cam2D.transform.position).Append('\n');
				}
				Transform transform = instance.GameFrame.transform;
				stringBuilder.Append("gameFrame=").Append(transform.localScale.x).Append('x')
					.Append(transform.localScale.y)
					.Append(" pos=")
					.Append(transform.position.x)
					.Append(',')
					.Append(transform.position.y)
					.Append('\n');
				stringBuilder.Append("gadgetTop=").Append(Describe(instance.gadgetPanelTop)).Append('\n');
				stringBuilder.Append("gadgetBottom=").Append(Describe(instance.gadgetPanelBottom)).Append('\n');
				stringBuilder.Append("gadgetLogo=").Append(Describe(instance.gadgetLogo)).Append('\n');
				ScreenBase[] componentsInChildren = instance.ScreenContainer.GetComponentsInChildren<ScreenBase>(true);
				foreach (ScreenBase screenBase in componentsInChildren)
				{
					if (!(screenBase == null))
					{
						GameObject gameObject = screenBase.gameObject;
						if (gameObject.activeInHierarchy)
						{
							stringBuilder.Append("screen ").Append(gameObject.name).Append(" bounds=")
								.Append(BoundsOf(gameObject))
								.Append('\n');
						}
					}
				}
				GameObject popUpContainer = instance.popUpContainer;
				if (popUpContainer != null && popUpContainer.activeInHierarchy)
				{
					stringBuilder.Append("popup bounds=").Append(BoundsOf(popUpContainer)).Append('\n');
				}
				DumpWidgets(instance, stringBuilder);
			}
		}
		catch (Exception ex)
		{
			stringBuilder.Append("ERROR ").Append(ex.Message).Append('\n');
		}
		File.WriteAllText(OutPath, stringBuilder.ToString());
	}

	private static string Describe(Component c)
	{
		if (c == null)
		{
			return "null";
		}
		return Describe(c.gameObject);
	}

	private static void DumpWidgets(GUIManager gm, StringBuilder sb)
	{
		try
		{
			if (gm.GUI2DRoot == null)
			{
				return;
			}
			Camera camera = ((gm.cam2D != null) ? gm.cam2D : Camera.main);
			UIWidget[] componentsInChildren = gm.GUI2DRoot.GetComponentsInChildren<UIWidget>(true);
			int num = 0;
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (num >= 100)
				{
					break;
				}
				UIWidget uIWidget = componentsInChildren[i];
				if (uIWidget == null)
				{
					continue;
				}
				Transform transform = uIWidget.transform;
				float x = transform.localScale.x;
				float y = transform.localScale.y;
				string text = transform.name;
				bool activeInHierarchy = transform.gameObject.activeInHierarchy;
				bool flag = x >= 500f && x <= 900f && y >= 950f && y <= 1500f;
				bool flag2 = text.IndexOf("bkg", StringComparison.OrdinalIgnoreCase) >= 0 || text.StartsWith("bg", StringComparison.OrdinalIgnoreCase) || text.IndexOf("background", StringComparison.OrdinalIgnoreCase) >= 0;
				if ((!flag && !flag2) || (!activeInHierarchy && y < 1000f))
				{
					continue;
				}
				sb.Append("w ").Append(text).Append(' ')
					.Append(uIWidget.GetType().Name)
					.Append(" s=")
					.Append(x.ToString("F0"))
					.Append('x')
					.Append(y.ToString("F0"))
					.Append(" p=")
					.Append(transform.localPosition.x.ToString("F0"))
					.Append(',')
					.Append(transform.localPosition.y.ToString("F0"))
					.Append(" par=")
					.Append((transform.parent != null) ? transform.parent.name : "-")
					.Append(" pv=")
					.Append(uIWidget.pivot);
				if (!activeInHierarchy)
				{
					sb.Append(" INACTIVE");
				}
				if (camera != null)
				{
					try
					{
						Vector3[] array = NGUIMath.CalculateWidgetCorners(uIWidget);
						float num2 = 1E+10f;
						float num3 = 1E+10f;
						float num4 = -1E+10f;
						float num5 = -1E+10f;
						for (int j = 0; j < 4; j++)
						{
							if (array[j].x < num2)
							{
								num2 = array[j].x;
							}
							if (array[j].y < num3)
							{
								num3 = array[j].y;
							}
							if (array[j].x > num4)
							{
								num4 = array[j].x;
							}
							if (array[j].y > num5)
							{
								num5 = array[j].y;
							}
						}
						Vector3 vector = camera.WorldToScreenPoint(new Vector3(num2, num3, 0f));
						Vector3 vector2 = camera.WorldToScreenPoint(new Vector3(num4, num5, 0f));
						float num6 = (float)Screen.height - vector2.y;
						float num7 = (float)Screen.height - vector.y;
						sb.Append(" win=(").Append(vector.x.ToString("F0")).Append(',')
							.Append(num6.ToString("F0"))
							.Append(")-(")
							.Append(vector2.x.ToString("F0"))
							.Append(',')
							.Append(num7.ToString("F0"))
							.Append(')');
					}
					catch (Exception ex)
					{
						sb.Append(" cornersERR=").Append(ex.Message);
					}
				}
				sb.Append('\n');
				num++;
			}
			sb.Append("widgetsShown=").Append(num).Append('\n');
		}
		catch (Exception ex2)
		{
			sb.Append("w ERROR ").Append(ex2.Message).Append('\n');
		}
	}

	private static string Describe(GameObject go)
	{
		if (go == null)
		{
			return "null";
		}
		Transform transform = go.transform;
		return transform.position.x.ToString("F0") + "," + transform.position.y.ToString("F0") + " active=" + go.activeInHierarchy;
	}

	private string BoundsOf(GameObject root)
	{
		Camera camera = ((GUIManager.instance.cam2D != null) ? GUIManager.instance.cam2D : Camera.main);
		if (camera == null)
		{
			return "no-camera";
		}
		Transform transform = root.transform;
		float num = 1E+10f;
		float num2 = 1E+10f;
		float num3 = -1E+10f;
		float num4 = -1E+10f;
		int num5 = 0;
		UIWidget[] componentsInChildren = root.GetComponentsInChildren<UIWidget>(true);
		foreach (UIWidget uIWidget in componentsInChildren)
		{
			if (uIWidget == null || !uIWidget.gameObject.activeInHierarchy)
			{
				continue;
			}
			Transform transform2 = uIWidget.transform;
			float x = transform2.localScale.x;
			float y = transform2.localScale.y;
			if (!(x > 3000f) && !(y > 3000f) && !(x < 0f) && !(y < 0f))
			{
				Vector3 vector = transform.InverseTransformPoint(transform2.position);
				float num6 = vector.x - x * 0.5f;
				float num7 = vector.x + x * 0.5f;
				float num8 = vector.y - y * 0.5f;
				float num9 = vector.y + y * 0.5f;
				if (num6 < num)
				{
					num = num6;
				}
				if (num8 < num2)
				{
					num2 = num8;
				}
				if (num7 > num3)
				{
					num3 = num7;
				}
				if (num9 > num4)
				{
					num4 = num9;
				}
				num5++;
			}
		}
		if (num5 == 0)
		{
			return "empty";
		}
		LoadMetrics();
		Vector3 vector2 = camera.WorldToScreenPoint(transform.TransformPoint(new Vector3(num, num2, 0f)));
		Vector3 vector3 = camera.WorldToScreenPoint(transform.TransformPoint(new Vector3(num3, num4, 0f)));
		float num10 = (float)(mRealW - Screen.width) * 0.5f;
		float num11 = vector2.x + num10;
		float num12 = vector3.x + num10;
		float num13 = (float)Screen.height - vector2.y + (float)mWinTop;
		float num14 = (float)Screen.height - vector3.y + (float)mWinTop;
		return "n=" + num5 + " px=(" + num11.ToString("F0") + "," + num13.ToString("F0") + ")-(" + num12.ToString("F0") + "," + num14.ToString("F0") + ") virt=(" + num.ToString("F0") + "," + num2.ToString("F0") + ")-(" + num3.ToString("F0") + "," + num4.ToString("F0") + ")";
	}
}

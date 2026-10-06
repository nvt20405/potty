using UnityEngine;

public class AutoReflectWidth : MonoBehaviour
{
	private enum WidgetMode
	{
		WM_NULL = 0,
		WM_SPRITE = 1,
		WM_DRAGPANEL = 2,
		WM_LABEL = 3,
		WM_SLIDER = 4,
		WM_TEXTURE = 5,
		WM_HTML_CONTENT = 6,
		WM_COLLIDER = 7
	}

	private WidgetMode mode;

	public GameObject sourceObj;

	public bool autoRePositionChild;

	public bool AutoFillMode;

	public bool AutoReflectUVTexture;

	public float startSourceWith = 640f;

	private float startMyWidth;

	private float previousSourceWith;

	private void Awake()
	{
		if (sourceObj == null)
		{
			sourceObj = GameObject.Find("GameFrame");
		}
		UISprite component = base.gameObject.GetComponent<UISprite>();
		if (component != null)
		{
			mode = WidgetMode.WM_SPRITE;
		}
		UIDraggablePanel component2 = base.gameObject.GetComponent<UIDraggablePanel>();
		if (component2 != null)
		{
			mode = WidgetMode.WM_DRAGPANEL;
		}
		NGUIHTML component3 = base.gameObject.GetComponent<NGUIHTML>();
		if (component3 != null)
		{
			mode = WidgetMode.WM_HTML_CONTENT;
			startMyWidth = component3.maxLineWidth;
		}
		UILabel component4 = base.gameObject.GetComponent<UILabel>();
		if (component4 != null)
		{
			mode = WidgetMode.WM_LABEL;
		}
		UISlider component5 = base.gameObject.GetComponent<UISlider>();
		if (component5 != null)
		{
			mode = WidgetMode.WM_SLIDER;
		}
		if (mode == WidgetMode.WM_NULL && GetComponent<BoxCollider>() != null)
		{
			mode = WidgetMode.WM_COLLIDER;
		}
		UITexture component6 = GetComponent<UITexture>();
		if (component6 != null)
		{
			mode = WidgetMode.WM_TEXTURE;
		}
		if (mode == WidgetMode.WM_SPRITE)
		{
			startMyWidth = base.transform.localScale.x;
		}
		else if (mode == WidgetMode.WM_DRAGPANEL)
		{
			UIPanel component7 = base.gameObject.GetComponent<UIPanel>();
			startMyWidth = component7.clipRange.z;
		}
		else if (mode == WidgetMode.WM_LABEL)
		{
			UILabel component8 = base.gameObject.GetComponent<UILabel>();
			startMyWidth = component8.lineWidth;
		}
		else if (mode == WidgetMode.WM_SLIDER)
		{
			startMyWidth = component5.foreground.localScale.x;
			component5.ForceUpdate();
		}
		else if (mode == WidgetMode.WM_TEXTURE)
		{
			startMyWidth = base.transform.localScale.x;
		}
		else if (mode == WidgetMode.WM_COLLIDER)
		{
			startMyWidth = GetComponent<Collider>().bounds.size.x;
		}
		if (startSourceWith == 0f)
		{
			startSourceWith = 640f;
		}
		Reflect();
	}

	private void Reflect()
	{
		if (sourceObj == null)
		{
			return;
		}
		float x = sourceObj.transform.localScale.x;
		if (previousSourceWith == x)
		{
			return;
		}
		float num = 0f;
		float num2 = 0f;
		if (mode == WidgetMode.WM_NULL)
		{
			num = ((previousSourceWith != 0f) ? previousSourceWith : startSourceWith);
			num2 = x;
		}
		previousSourceWith = x;
		if (mode == WidgetMode.WM_SPRITE)
		{
			num = base.transform.localScale.x;
			num2 = ((!AutoFillMode) ? (x / startSourceWith * startMyWidth) : (x - startSourceWith + startMyWidth));
			base.transform.localScale = new Vector3(num2, base.transform.localScale.y, base.transform.localScale.z);
		}
		else if (mode == WidgetMode.WM_COLLIDER)
		{
			num = GetComponent<Collider>().bounds.size.x;
			num2 = ((!AutoFillMode) ? (x / startSourceWith * startMyWidth) : (x - startSourceWith + startMyWidth));
			BoxCollider component = GetComponent<BoxCollider>();
			component.size = new Vector3(num2, component.size.y, component.size.z);
		}
		else if (mode == WidgetMode.WM_DRAGPANEL)
		{
			UIPanel component2 = base.gameObject.GetComponent<UIPanel>();
			num = component2.clipRange.z;
			float x2 = component2.clipRange.x;
			float x3;
			if (AutoFillMode)
			{
				num2 = x - startSourceWith + startMyWidth;
				x3 = ((x2 > 0f) ? (x2 + (num2 - num) / 2f) : ((!(x2 < 0f)) ? x2 : (x2 + (num - num2) / 2f)));
			}
			else
			{
				num2 = x / startSourceWith * startMyWidth;
				x3 = x2;
			}
			component2.clipRange = new Vector4(x3, component2.clipRange.y, num2, component2.clipRange.w);
		}
		else if (mode == WidgetMode.WM_HTML_CONTENT)
		{
			NGUIHTML component3 = base.gameObject.GetComponent<NGUIHTML>();
			num2 = x / startSourceWith * startMyWidth;
			component3.maxLineWidth = (int)num2;
		}
		else if (mode == WidgetMode.WM_LABEL)
		{
			UILabel component4 = base.gameObject.GetComponent<UILabel>();
			num2 = ((!AutoFillMode) ? (x / startSourceWith * startMyWidth) : (x - startSourceWith + startMyWidth));
			num = component4.lineWidth;
			component4.lineWidth = (int)num2;
		}
		else if (mode == WidgetMode.WM_SLIDER)
		{
			UISlider component5 = base.gameObject.GetComponent<UISlider>();
			num = component5.fullSize.x;
			num2 = ((!AutoFillMode) ? (x / startSourceWith * startMyWidth) : (x - startSourceWith + startMyWidth));
			component5.fullSize = new Vector2(num2, component5.fullSize.y);
		}
		else if (mode == WidgetMode.WM_TEXTURE)
		{
			UITexture component6 = GetComponent<UITexture>();
			num = base.transform.localScale.x;
			num2 = ((!AutoFillMode) ? (x / startSourceWith * startMyWidth) : (x - startSourceWith + startMyWidth));
			base.transform.localScale = new Vector3(num2, base.transform.localScale.y, base.transform.localScale.z);
			if (AutoReflectUVTexture)
			{
				Rect rect = default(Rect);
				rect = new Rect(component6.uvRect);
				float x4 = rect.x;
				float width = rect.width;
				float num3 = (rect.width = num2 / (float)component6.mainTexture.width);
				float num5 = num3;
				rect.xMin = Mathf.Clamp01(x4 - num5 + width);
				if (rect.xMax > 1f)
				{
					rect.xMin -= Mathf.Clamp01(rect.xMax - 1f);
				}
				component6.uvRect = rect;
			}
		}
		if (!autoRePositionChild)
		{
			return;
		}
		Transform transform = null;
		Transform transform2 = null;
		if (AutoFillMode)
		{
			foreach (Transform item in base.transform)
			{
				Transform transform4 = item;
				if (transform == null)
				{
					transform = transform4;
				}
				if (transform2 == null)
				{
					transform2 = transform4;
				}
				if (transform4.transform.localPosition.x < transform.transform.localPosition.x && transform4.transform.localPosition.x >= (0f - num) / 2f)
				{
					transform = transform4;
				}
				if (transform4.transform.localPosition.x > transform2.transform.localPosition.x && transform4.transform.localPosition.x <= num / 2f)
				{
					transform2 = transform4;
				}
			}
		}
		float num6 = 0f;
		float num7 = 0f;
		if (transform != null && transform2 != null)
		{
			num6 = transform.localPosition.x;
			num7 = transform2.localPosition.x;
		}
		foreach (Transform item2 in base.transform)
		{
			Transform transform6 = item2;
			float x5 = transform6.localPosition.x;
			float x6 = num2 / num * x5;
			if (AutoFillMode)
			{
				float num8 = (num - num2) / 2f + num6;
				float num9 = (num2 - num) / 2f + num7;
				float num10 = num7 - num6;
				if (transform != transform2 && num10 > 0.5f)
				{
					if (transform6 == transform)
					{
						x6 = num8;
					}
					else if (transform6 == transform2)
					{
						x6 = num9;
					}
					else
					{
						float num11 = (num6 + num7) / 2f;
						x6 = ((x5 < num11) ? (num8 / num6 * x5) : ((!(x5 > num11)) ? x5 : (num9 / num7 * x5)));
					}
				}
				else
				{
					x6 = x5;
				}
			}
			transform6.localPosition = new Vector3(x6, transform6.localPosition.y, transform6.localPosition.z);
		}
	}

	private void Update()
	{
		if (sourceObj == null)
		{
			EGDebug.Log(base.transform.parent.gameObject.name ?? "");
		}
		Reflect();
	}
}

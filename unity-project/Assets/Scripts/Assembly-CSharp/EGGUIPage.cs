using UnityEngine;

public class EGGUIPage : IgnoreTimeScale
{
	public EGGUIPagingPanel pagingPanel;

	public bool autoSetCollider = true;

	private void Awake()
	{
	}

	private void Start()
	{
		if (pagingPanel == null)
		{
			pagingPanel = NGUITools.FindInParents<EGGUIPagingPanel>(base.gameObject);
		}
		if (pagingPanel != null && autoSetCollider)
		{
			BoxCollider component = GetComponent<BoxCollider>();
			component.size = new Vector2(pagingPanel.pageWidth, component.size.y);
			UIPanel component2 = GetComponent<UIPanel>();
			if (component2 != null && component2.clipping == UIDrawCall.Clipping.AlphaClip)
			{
				component2.clipRange = new Vector4(component2.clipRange.x, component2.clipRange.y, pagingPanel.pageWidth, component2.clipRange.w);
			}
		}
	}

	private void OnPress(bool pressed)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && pagingPanel != null)
		{
			pagingPanel.Press(pressed);
		}
	}

	private void OnDrag(Vector2 delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && pagingPanel != null)
		{
			pagingPanel.Drag();
		}
	}

	private void OnScroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && pagingPanel != null)
		{
			pagingPanel.Scroll(delta);
		}
	}
}

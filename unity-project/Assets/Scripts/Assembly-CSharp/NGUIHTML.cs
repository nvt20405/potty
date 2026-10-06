using System.Collections;
using HTMLEngine;
using UnityEngine;

[AddComponentMenu("HTMLEngine/NGUIHTML")]
public class NGUIHTML : MonoBehaviour
{
	public enum AutoScrollType
	{
		MANUAL = 0,
		AUTO_TOP = 1,
		AUTO_BOTTOM = 2
	}

	public string _html = string.Empty;

	public int maxLineWidth;

	public AutoScrollType autoScroll;

	private bool changed;

	private HtCompiler compiler;

	public UIFont NormalSmall;

	public UIFont FontSmall;

	public UIFont ThuPhapSmall;

	public UIAtlas[] atlasList;

	public string html
	{
		get
		{
			return _html;
		}
		set
		{
			_html = value;
			changed = true;
		}
	}

	private void Start()
	{
		compiler = HtEngine.GetCompiler();
		HtEngine.NormalSmall = NormalSmall;
		HtEngine.FontSmall = FontSmall;
		HtEngine.ThuPhapSmall = ThuPhapSmall;
		HtEngine.atlas = atlasList;
	}

	private void Update()
	{
		if (!changed || compiler == null)
		{
			return;
		}
		compiler.Compile(html, (maxLineWidth <= 0) ? Screen.width : maxLineWidth);
		foreach (Transform item in base.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		compiler.Draw(Time.deltaTime, base.transform);
		changed = false;
		if (autoScroll != AutoScrollType.MANUAL)
		{
			StartCoroutine(updateAutoScroll());
		}
	}

	private void OnDestroy()
	{
		if (compiler != null)
		{
			compiler.Dispose();
			compiler = null;
		}
	}

	private IEnumerator updateAutoScroll()
	{
		yield return new WaitForEndOfFrame();
		UIDraggablePanel uiDraggablePanel = NGUITools.FindInParents<UIDraggablePanel>(base.gameObject);
		if (uiDraggablePanel != null)
		{
			switch (autoScroll)
			{
			case AutoScrollType.AUTO_TOP:
				uiDraggablePanel.relativePositionOnReset = Vector2.zero;
				break;
			case AutoScrollType.AUTO_BOTTOM:
				uiDraggablePanel.relativePositionOnReset = new Vector2(0f, 1f);
				break;
			}
			uiDraggablePanel.ResetPosition();
		}
	}
}

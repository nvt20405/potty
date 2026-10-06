using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("EG/GUI/Grid")]
public class EGGUIGrid : MonoBehaviour
{
	public enum Arrangement
	{
		Horizontal = 0,
		Vertical = 1
	}

	public enum ElementPivot
	{
		Center = 0,
		TopLeft = 1
	}

	public Arrangement arrangement;

	public ElementPivot elementPivot;

	public float cellWidth = 200f;

	public float cellHeight = 200f;

	public int maxPerLine = 1;

	public bool repositionNow;

	public bool sorted;

	public bool hideInactive = true;

	private bool mStarted;

	public Vector2 padding;

	public bool FixedGridSize;

	public int FixedColNum;

	public int FixedRowNum;

	private void Start()
	{
		mStarted = true;
		Reposition();
	}

	private void Update()
	{
		if (repositionNow)
		{
			repositionNow = false;
			Reposition();
		}
	}

	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	public void Reposition()
	{
		if (!mStarted)
		{
			repositionNow = true;
			return;
		}
		Transform transform = base.transform;
		int num = 0;
		int num2 = 0;
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if ((bool)child && (!hideInactive || NGUITools.GetActive(child.gameObject)))
			{
				list.Add(child);
			}
		}
		int num3 = ((list.Count > 0) ? ((list.Count - 1) / maxPerLine + 1) : 0);
		int num4 = ((list.Count <= maxPerLine) ? list.Count : maxPerLine);
		int num5 = ((arrangement != Arrangement.Horizontal) ? num3 : num4);
		int num6 = ((arrangement != Arrangement.Horizontal) ? num4 : num3);
		if (FixedGridSize)
		{
			num5 = FixedColNum;
			num6 = FixedRowNum;
		}
		float num7 = (float)num5 * cellWidth + (float)(num5 - 1) * padding.x;
		float num8 = (float)num6 * cellHeight + (float)(num6 - 1) * padding.y;
		if (sorted)
		{
			list.Sort(SortByName);
		}
		int j = 0;
		for (int count = list.Count; j < count; j++)
		{
			Transform transform2 = list[j];
			if (!NGUITools.GetActive(transform2.gameObject) && hideInactive)
			{
				continue;
			}
			float z = transform2.localPosition.z;
			float num9 = 0f;
			float num10 = 0f;
			if (arrangement == Arrangement.Horizontal)
			{
				if (elementPivot == ElementPivot.Center)
				{
					num9 = cellWidth / 2f + (float)num * (cellWidth + padding.x);
					num10 = cellHeight / 2f + (float)num2 * (cellHeight + padding.y);
				}
				else if (elementPivot == ElementPivot.TopLeft)
				{
					num9 = (float)num * (cellWidth + padding.x);
					num10 = (float)num2 * (cellHeight + padding.y);
				}
			}
			else if (elementPivot == ElementPivot.Center)
			{
				num9 = cellWidth / 2f + (float)num2 * (cellWidth + padding.x);
				num10 = cellHeight / 2f + (float)num * (cellHeight + padding.y);
			}
			else
			{
				num9 = (float)num2 * (cellWidth + padding.x);
				num10 = (float)num * (cellHeight + padding.y);
			}
			num9 -= num7 / 2f;
			num10 -= num8 / 2f;
			transform2.localPosition = new Vector3(num9, 0f - num10, z);
			if (++num >= maxPerLine && maxPerLine > 0)
			{
				num = 0;
				num2++;
			}
		}
		UIDraggablePanel uIDraggablePanel = NGUITools.FindInParents<UIDraggablePanel>(base.gameObject);
		if (uIDraggablePanel != null)
		{
			uIDraggablePanel.UpdateScrollbars(true);
		}
	}
}

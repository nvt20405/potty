using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("EG/GUI/List")]
public class EGGUIList : MonoBehaviour
{
	public enum Arrangement
	{
		Horizontal = 0,
		Vertical = 1
	}

	public enum Pivot
	{
		Center = 0,
		Left = 1,
		TopLeft = 2,
		Top = 3,
		TopRight = 4,
		Right = 5,
		BottomRight = 6,
		Bottom = 7,
		BottomLeft = 8
	}

	private static readonly Vector2[] pivotPoints = new Vector2[9]
	{
		new Vector2(0f, 0f),
		new Vector2(-1f, 0f),
		new Vector2(-1f, 1f),
		new Vector2(0f, 1f),
		new Vector2(1f, 1f),
		new Vector2(1f, 0f),
		new Vector2(1f, -1f),
		new Vector2(0f, -1f),
		new Vector2(-1f, -1f)
	};

	public Arrangement arrangement;

	public Pivot elementPivot;

	public Pivot listPivot;

	public float cellWidth = 200f;

	public float cellHeight = 200f;

	public bool repositionNow;

	public bool sorted;

	public bool hideInactive = true;

	private bool mStarted;

	public float padding;

	public int NumOfCell;

	public int numOfPlayer = 1;

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
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if ((bool)child && (!hideInactive || NGUITools.GetActive(child.gameObject)))
			{
				list.Add(child);
			}
		}
		numOfPlayer = list.Count;
		int num2 = ((NumOfCell <= 0) ? numOfPlayer : NumOfCell);
		int num3 = ((arrangement != Arrangement.Horizontal) ? 1 : num2);
		int num4 = ((arrangement == Arrangement.Horizontal) ? 1 : num2);
		float num5 = (float)num3 * cellWidth + (float)(num3 - 1) * padding;
		float num6 = (float)num4 * cellHeight + (float)(num4 - 1) * padding;
		if (sorted)
		{
			list.Sort(SortByName);
		}
		num = num2 / 2 - numOfPlayer / 2;
		int j = 0;
		Vector2 vector = default(Vector2);
		for (int count = list.Count; j < count; j++)
		{
			Transform transform2 = list[j];
			if (NGUITools.GetActive(transform2.gameObject) || !hideInactive)
			{
				float z = transform2.localPosition.z;
				int num7 = ((arrangement == Arrangement.Horizontal) ? num : 0);
				int num8 = ((arrangement != Arrangement.Horizontal) ? num : 0);
				float num9 = (float)num7 * (cellWidth + padding);
				float num10 = (float)num8 * (cellHeight + padding);
				vector = new Vector2(num9, num10);
				Vector2 vector2 = pivotPoints[(int)elementPivot];
				num9 = (vector2.x + 1f) / 2f * cellWidth + num9;
				num10 = (vector2.y * -1f + 1f) / 2f * cellHeight + num10;
				Vector2 vector3 = pivotPoints[(int)listPivot];
				num9 -= (vector3.x + 1f) / 2f * num5;
				num10 -= (vector3.y * -1f + 1f) / 2f * num6;
				transform2.localPosition = new Vector3(num9, 0f - num10, z);
				num++;
			}
		}
	}
}

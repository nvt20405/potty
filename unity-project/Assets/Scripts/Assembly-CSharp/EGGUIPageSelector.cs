using System.Collections.Generic;
using UnityEngine;

public class EGGUIPageSelector : MonoBehaviour
{
	public GameObject listButton;

	public EGGUIPagingPanel panel;

	private NhanVatAvatar previousAvatar;

	public void SelectPage(int page)
	{
		List<GameObject> list = new List<GameObject>();
		foreach (Transform item in listButton.transform)
		{
			Transform transform2 = item;
			list.Add(transform2.gameObject);
		}
		if (page >= list.Count || page < 0)
		{
			return;
		}
		Transform transform3 = listButton.transform;
		UICheckbox component = list[page].GetComponent<UICheckbox>();
		if ((bool)component)
		{
			component.isChecked = true;
			UIPanel component2 = GetComponent<UIPanel>();
			UIDraggablePanel component3 = GetComponent<UIDraggablePanel>();
			EGGUIList component4 = listButton.GetComponent<EGGUIList>();
			float x = ((component4.arrangement != EGGUIList.Arrangement.Horizontal) ? component4.cellWidth : (component4.cellWidth + component4.padding));
			float y = ((component4.arrangement != EGGUIList.Arrangement.Horizontal) ? (component4.cellHeight + component4.padding) : component4.cellHeight);
			Matrix4x4 worldToLocalMatrix = component2.transform.worldToLocalMatrix;
			Vector2 vector = default(Vector2);
			vector = new Vector2(x, y);
			Vector2 vector2 = default(Vector2);
			vector2 = new Vector2(component.transform.localPosition.x, component.transform.localPosition.y);
			Vector2 vector3 = vector2 - vector / 2f;
			Vector2 vector4 = vector2 + vector / 2f;
			Vector3 v = transform3.TransformPoint(new Vector3(vector3.x, vector3.y));
			Vector3 v2 = transform3.TransformPoint(new Vector3(vector4.x, vector4.y));
			Vector3 size = worldToLocalMatrix.MultiplyPoint3x4(v2) - worldToLocalMatrix.MultiplyPoint3x4(v);
			Vector3 center = worldToLocalMatrix.MultiplyPoint3x4(component.transform.position);
			Bounds bounds = default(Bounds);
			bounds = new Bounds(center, size);
			Vector3 vector5 = component2.CalculateConstrainOffset(bounds.min, bounds.max);
			if (vector5.magnitude > 0.001f)
			{
				SpringPanel.Begin(component2.gameObject, component2.transform.localPosition + vector5, 13f);
			}
			return;
		}
		NhanVatAvatar component5 = list[page].GetComponent<NhanVatAvatar>();
		if (component5 != null)
		{
			if (previousAvatar != null)
			{
				previousAvatar.IsSelected = false;
			}
			component5.IsSelected = true;
			previousAvatar = component5;
			UIPanel component6 = GetComponent<UIPanel>();
			UIDraggablePanel component7 = GetComponent<UIDraggablePanel>();
			EGGUIList component8 = listButton.GetComponent<EGGUIList>();
			float x2 = ((component8.arrangement != EGGUIList.Arrangement.Horizontal) ? component8.cellWidth : (component8.cellWidth + component8.padding));
			float y2 = ((component8.arrangement != EGGUIList.Arrangement.Horizontal) ? (component8.cellHeight + component8.padding) : component8.cellHeight);
			Matrix4x4 worldToLocalMatrix2 = component6.transform.worldToLocalMatrix;
			Vector2 vector6 = default(Vector2);
			vector6 = new Vector2(x2, y2);
			Vector2 vector7 = default(Vector2);
			vector7 = new Vector2(component5.transform.localPosition.x, component5.transform.localPosition.y);
			Vector2 vector8 = vector7 - vector6 / 2f;
			Vector2 vector9 = vector7 + vector6 / 2f;
			Vector3 v3 = transform3.TransformPoint(new Vector3(vector8.x, vector8.y));
			Vector3 v4 = transform3.TransformPoint(new Vector3(vector9.x, vector9.y));
			Vector3 size2 = worldToLocalMatrix2.MultiplyPoint3x4(v4) - worldToLocalMatrix2.MultiplyPoint3x4(v3);
			Vector3 center2 = worldToLocalMatrix2.MultiplyPoint3x4(component5.transform.position);
			Bounds bounds2 = default(Bounds);
			bounds2 = new Bounds(center2, size2);
			Vector3 vector10 = component6.CalculateConstrainOffset(bounds2.min, bounds2.max);
			if (vector10.magnitude > 0.001f)
			{
				SpringPanel.Begin(component6.gameObject, component6.transform.localPosition + vector10, 13f);
			}
		}
	}
}

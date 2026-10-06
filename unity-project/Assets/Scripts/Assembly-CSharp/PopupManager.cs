using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
	private const int Offset = -50;

	private static PopupManager _instance;

	public static PopupManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.Find("PopUpContainer").GetComponent<PopupManager>();
			}
			return _instance;
		}
	}

	public void HideAllNonTutorialPopup()
	{
		foreach (Transform item in base.transform)
		{
			Transform transform2 = item;
			TutorialPopup component = transform2.gameObject.GetComponent<TutorialPopup>();
			if (component == null)
			{
				NGUITools.SetActive(transform2.gameObject, false);
			}
		}
	}

	public void Add(GameObject obj, [Optional] Vector3 position)
	{
		float num = 0f;
		UIPanel[] componentsInChildren = GetComponentsInChildren<UIPanel>(true);
		List<UIPanel> list = new List<UIPanel>();
		UIPanel[] array = componentsInChildren;
		UIPanel[] array2 = array;
		foreach (UIPanel uIPanel in array2)
		{
			if (uIPanel.transform.parent == base.transform)
			{
				list.Add(uIPanel);
			}
		}
		foreach (UIPanel item in list)
		{
			float z = item.transform.localPosition.z;
			if (z < num && z > -600f)
			{
				num = z;
			}
		}
		num += -50f;
		if (num < -600f)
		{
			num = -600f;
		}
		obj.transform.parent = base.gameObject.transform;
		obj.transform.localPosition = new Vector3(position.x, position.y, num);
		Utils.SetLayer(obj.transform, "GUIPopUp", true);
	}

	private void Awake()
	{
		foreach (Transform item in base.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
	}

	private void Update()
	{
	}
}

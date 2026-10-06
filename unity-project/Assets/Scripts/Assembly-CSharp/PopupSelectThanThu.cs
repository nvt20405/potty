using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupSelectThanThu : MonoBehaviour
{
	private const int maxItemCount = 12;

	public GameObject ItemRoot;

	public GameObject ThanThuPrefab;

	public static PopupSelectThanThu instance;

	public Func<int, bool> OnFinish;

	private List<UserInfo.PetInfo> thanthuList = new List<UserInfo.PetInfo>();

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<ThanThuItem> ItemList = new List<ThanThuItem>();

	private int startItemGUI_Idx;

	public bool isCreateByKyNgoHuaNguyen;

	public bool isCreateByKyNgoUongRuou;

	private int mHeroSelectedID;

	private void Start()
	{
		dragPanel = ItemRoot.GetComponent<UIDraggablePanel>();
		panel = ItemRoot.GetComponent<UIPanel>();
	}

	private void Update()
	{
		if (ItemList.Count > 0)
		{
			Vector4 clipRange = panel.clipRange;
			ThanThuItem thanThuItem = ItemList[ItemList.Count - 1];
			float y = thanThuItem.transform.localPosition.y;
			ThanThuItem thanThuItem2 = ItemList[0];
			float y2 = thanThuItem2.transform.localPosition.y;
			if (y - clipRange.y > -600f)
			{
				SwapDragListDown();
			}
			else if (y2 - clipRange.y < 600f)
			{
				SwapDragListUp();
			}
		}
	}

	public void SwapDragListDown()
	{
		int num = startItemGUI_Idx + 12;
		if (num >= thanthuList.Count)
		{
			return;
		}
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 340f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -125f, 0f);
		ThanThuItem thanThuItem = ItemList[0];
		ThanThuItem thanThuItem2 = ItemList[ItemList.Count - 1];
		ItemList.RemoveAt(0);
		ItemList.Add(thanThuItem);
		thanThuItem.transform.localPosition = thanThuItem2.transform.localPosition + vector2;
		thanThuItem.SetForPopupSelectThanThu(thanthuList[num]);
		UICheckbox componentInChildren = thanThuItem.GetComponentInChildren<UICheckbox>();
		if (componentInChildren != null)
		{
			if (thanthuList[num].ID == mHeroSelectedID)
			{
				componentInChildren.isChecked = true;
			}
			else
			{
				componentInChildren.isChecked = false;
			}
			EGDebug.Log("heroList[new_idx].HID: " + thanthuList[num].ID + "- mHeroSelectedName: " + mHeroSelectedID + " - isChecked: " + componentInChildren.isChecked);
		}
		startItemGUI_Idx++;
	}

	public void SwapDragListUp()
	{
		if (startItemGUI_Idx == 0)
		{
			return;
		}
		startItemGUI_Idx--;
		int num = startItemGUI_Idx;
		if (num < 0)
		{
			return;
		}
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 340f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -125f, 0f);
		ThanThuItem thanThuItem = ItemList[0];
		ThanThuItem thanThuItem2 = ItemList[ItemList.Count - 1];
		ItemList.RemoveAt(ItemList.Count - 1);
		ItemList.Insert(0, thanThuItem2);
		thanThuItem2.transform.localPosition = thanThuItem.transform.localPosition - vector2;
		thanThuItem2.SetForPopupSelectThanThu(thanthuList[num]);
		UICheckbox componentInChildren = thanThuItem2.GetComponentInChildren<UICheckbox>();
		if (componentInChildren != null)
		{
			if (thanthuList[num].ID == mHeroSelectedID)
			{
				componentInChildren.isChecked = true;
			}
			else
			{
				componentInChildren.isChecked = false;
			}
			EGDebug.Log("heroList[new_idx].HID: " + thanthuList[num].ID + "- mHeroSelectedName: " + mHeroSelectedID + " - isChecked: " + componentInChildren.isChecked);
		}
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	public static void CreateByNormal(Func<int, bool> onFinish, List<int> ignore_list = null)
	{
		Create(onFinish, ignore_list, true);
	}

	public static void CreateByKyNgoUongRuou(Func<int, bool> onFinish, List<int> ignore_list = null)
	{
		Create(onFinish, ignore_list, false, true);
	}

	public static void Create(Func<int, bool> onFinish, List<int> ignore_list = null, bool isOpenByKyNgoHuaNguyen = false, bool isOpenByKyNgoUongRuou = false)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("popup/PopupSelectThanThu"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupSelectThanThu>();
		instance.isCreateByKyNgoHuaNguyen = isOpenByKyNgoHuaNguyen;
		instance.isCreateByKyNgoUongRuou = isOpenByKyNgoUongRuou;
		instance.SyncWithNetworkData(ignore_list);
		instance.OnFinish = onFinish;
	}

	public void SyncWithNetworkData(List<int> ignore_list)
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		thanthuList.Clear();
		startItemGUI_Idx = 0;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 340f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -125f, 0f);
		if (startItemGUI_Idx < 0)
		{
			startItemGUI_Idx = 0;
		}
		foreach (UserInfo.PetInfo item2 in GameManager.instance.m_GameClient.UserInfo.ListThanThu)
		{
			if (ignore_list == null || !ignore_list.Contains(item2.ID))
			{
				thanthuList.Add(item2);
			}
		}
		thanthuList.Sort((UserInfo.PetInfo x, UserInfo.PetInfo y) => ConfigManager.instance.CompareThanThu(x, y));
		int num = 0;
		for (int num2 = startItemGUI_Idx; num2 < thanthuList.Count; num2++)
		{
			UserInfo.PetInfo forPopupSelectThanThu = thanthuList[num2];
			ThanThuItem component = ((GameObject)UnityEngine.Object.Instantiate(ThanThuPrefab)).GetComponent<ThanThuItem>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = vector;
			vector += vector2;
			component.SetForPopupSelectThanThu(forPopupSelectThanThu);
			UIEventListener.Get(component.gameObject).onClick = thanthu_onClick;
			UICheckbox uICheckbox = component.GetComponentsInChildren<UICheckbox>(true)[0];
			UIEventListener.Get(uICheckbox.gameObject).onClick = checkBox_onClick;
			ItemList.Add(component);
			num++;
			if (num >= 12)
			{
				break;
			}
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}

	public void thanthu_onClick(GameObject go)
	{
		ThanThuItem component = go.GetComponent<ThanThuItem>();
		if (component != null)
		{
			UICheckbox componentInChildren = component.GetComponentInChildren<UICheckbox>();
			if (componentInChildren != null)
			{
				componentInChildren.isChecked = true;
				mHeroSelectedID = component.m_Data.ID;
			}
		}
	}

	public void checkBox_onClick(GameObject go)
	{
		UICheckbox component = go.GetComponent<UICheckbox>();
		ThanThuItem component2 = component.transform.parent.GetComponent<ThanThuItem>();
		if (component2 != null && component.isChecked)
		{
			mHeroSelectedID = component2.m_Data.ID;
		}
		EGDebug.Log("HERO SELECTED: " + mHeroSelectedID);
	}

	public void OnOkClick()
	{
		ThanThuItem thanThuItem = null;
		foreach (ThanThuItem item in ItemList)
		{
			UICheckbox uICheckbox = item.GetComponentsInChildren<UICheckbox>(true)[0];
			if (uICheckbox.isChecked)
			{
				thanThuItem = item;
				break;
			}
		}
		if ((bool)thanThuItem)
		{
			OnFinish(thanThuItem.m_Data.ID);
			DestroyPopup();
		}
	}

	public void OnCancelClick()
	{
		DestroyPopup();
	}
}

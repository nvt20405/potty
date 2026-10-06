using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupSelectCostume : MonoBehaviour
{
	private const int maxItemCount = 12;

	public GameObject ItemRoot;

	public GameObject CostumePrefab;

	public static PopupSelectCostume instance;

	public Func<int, int, bool> OnFinish;

	private List<UserInfo.CostumeData> heroList = new List<UserInfo.CostumeData>();

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<CostumeItem> ItemList = new List<CostumeItem>();

	private int startItemGUI_Idx;

	private int mHeroSelectedID;

	private int ownerHID;

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
			CostumeItem costumeItem = ItemList[ItemList.Count - 1];
			float y = costumeItem.transform.localPosition.y;
			CostumeItem costumeItem2 = ItemList[0];
			float y2 = costumeItem2.transform.localPosition.y;
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
		if (num >= heroList.Count)
		{
			return;
		}
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 340f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -125f, 0f);
		CostumeItem costumeItem = ItemList[0];
		CostumeItem costumeItem2 = ItemList[ItemList.Count - 1];
		ItemList.RemoveAt(0);
		ItemList.Add(costumeItem);
		costumeItem.transform.localPosition = costumeItem2.transform.localPosition + vector2;
		costumeItem.SetForPopupSelectCostume(heroList[num]);
		UICheckbox componentInChildren = costumeItem.GetComponentInChildren<UICheckbox>();
		if (componentInChildren != null)
		{
			if (heroList[num].ID == mHeroSelectedID)
			{
				componentInChildren.isChecked = true;
			}
			else
			{
				componentInChildren.isChecked = false;
			}
			EGDebug.Log("heroList[new_idx].HID: " + heroList[num].ID + "- mHeroSelectedName: " + mHeroSelectedID + " - isChecked: " + componentInChildren.isChecked);
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
		CostumeItem costumeItem = ItemList[0];
		CostumeItem costumeItem2 = ItemList[ItemList.Count - 1];
		ItemList.RemoveAt(ItemList.Count - 1);
		ItemList.Insert(0, costumeItem2);
		costumeItem2.transform.localPosition = costumeItem.transform.localPosition - vector2;
		costumeItem2.SetForPopupSelectCostume(heroList[num]);
		UICheckbox componentInChildren = costumeItem2.GetComponentInChildren<UICheckbox>();
		if (componentInChildren != null)
		{
			if (heroList[num].ID == mHeroSelectedID)
			{
				componentInChildren.isChecked = true;
			}
			else
			{
				componentInChildren.isChecked = false;
			}
			EGDebug.Log("heroList[new_idx].HID: " + heroList[num].ID + "- mHeroSelectedName: " + mHeroSelectedID + " - isChecked: " + componentInChildren.isChecked);
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

	public static PopupSelectCostume Create(Func<int, int, bool> onFinish, List<int> ignore_list = null)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("popup/PopupSelectCostume"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupSelectCostume>();
		instance.SyncWithNetworkData(ignore_list);
		instance.OnFinish = onFinish;
		return instance;
	}

	public static PopupSelectCostume CreateForNhanVat(UserInfo.HeroData hero, Func<int, int, bool> onFinish)
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		List<int> list = new List<int>();
		if (userInfo.CostumeList != null)
		{
			for (int i = 0; i < userInfo.CostumeList.Count; i++)
			{
				UserInfo.CostumeData costumeData = userInfo.CostumeList[i];
				CostumeCfg costumeCfg = ConfigManager.instance.m_dicCostumeCfg[costumeData.CodeName];
				if (costumeCfg.NhanVat != hero.Name)
				{
					list.Add(costumeData.ID);
				}
			}
		}
		PopupSelectCostume popupSelectCostume = Create(onFinish, list);
		popupSelectCostume.ownerHID = hero.HID;
		return popupSelectCostume;
	}

	public void SyncWithNetworkData(List<int> ignore_list)
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			if (transform2.gameObject.activeInHierarchy)
			{
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
		}
		ItemList.Clear();
		heroList.Clear();
		startItemGUI_Idx = 0;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 340f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -125f, 0f);
		if (startItemGUI_Idx < 0)
		{
			startItemGUI_Idx = 0;
		}
		if (GameManager.instance.m_GameClient.UserInfo.CostumeList != null)
		{
			foreach (UserInfo.CostumeData costume in GameManager.instance.m_GameClient.UserInfo.CostumeList)
			{
				if (ignore_list == null || !ignore_list.Contains(costume.ID))
				{
					heroList.Add(costume);
				}
			}
		}
		int num = 0;
		for (int i = startItemGUI_Idx; i < heroList.Count; i++)
		{
			UserInfo.CostumeData forPopupSelectCostume = heroList[i];
			CostumeItem component = ((GameObject)UnityEngine.Object.Instantiate(CostumePrefab)).GetComponent<CostumeItem>();
			component.gameObject.SetActive(true);
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = vector;
			vector += vector2;
			component.SetForPopupSelectCostume(forPopupSelectCostume);
			UICheckbox uICheckbox = component.GetComponentsInChildren<UICheckbox>(true)[0];
			UIEventListener.Get(component.gameObject).onClick = nhanvat_onClick;
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

	public void checkBox_onClick(GameObject go)
	{
		UICheckbox component = go.GetComponent<UICheckbox>();
		CostumeItem component2 = component.transform.parent.GetComponent<CostumeItem>();
		if (component2 != null && component.isChecked)
		{
			mHeroSelectedID = component2.m_Data.ID;
		}
		EGDebug.Log("HERO SELECTED: " + mHeroSelectedID);
	}

	public void nhanvat_onClick(GameObject go)
	{
		EGDebug.Log("nhanvat_onClick");
		CostumeItem component = go.GetComponent<CostumeItem>();
		UICheckbox componentInChildren = component.GetComponentInChildren<UICheckbox>();
	}

	public void OnOkClick()
	{
		CostumeItem costumeItem = null;
		foreach (CostumeItem item in ItemList)
		{
			UICheckbox uICheckbox = item.GetComponentsInChildren<UICheckbox>(true)[0];
			if (uICheckbox.isChecked)
			{
				costumeItem = item;
				break;
			}
		}
		if ((bool)costumeItem)
		{
			OnFinish(costumeItem.m_Data.ID, ownerHID);
			DestroyPopup();
		}
	}

	public void OnCancelClick()
	{
		DestroyPopup();
	}
}

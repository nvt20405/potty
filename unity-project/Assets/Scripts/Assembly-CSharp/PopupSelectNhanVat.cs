using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupSelectNhanVat : MonoBehaviour
{
	private const int maxItemCount = 12;

	public GameObject ItemRoot;

	public GameObject DeTuPrefab;

	public static PopupSelectNhanVat instance;

	public Func<int, bool> OnFinish;

	private List<UserInfo.HeroData> heroList = new List<UserInfo.HeroData>();

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<DeTuItem> ItemList = new List<DeTuItem>();

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
			DeTuItem deTuItem = ItemList[ItemList.Count - 1];
			float y = deTuItem.transform.localPosition.y;
			DeTuItem deTuItem2 = ItemList[0];
			float y2 = deTuItem2.transform.localPosition.y;
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
		DeTuItem deTuItem = ItemList[0];
		DeTuItem deTuItem2 = ItemList[ItemList.Count - 1];
		ItemList.RemoveAt(0);
		ItemList.Add(deTuItem);
		deTuItem.transform.localPosition = deTuItem2.transform.localPosition + vector2;
		deTuItem.SetForPopupSelectNhanVat(heroList[num], isCreateByKyNgoUongRuou);
		UICheckbox componentInChildren = deTuItem.GetComponentInChildren<UICheckbox>();
		if (componentInChildren != null)
		{
			if (heroList[num].HID == mHeroSelectedID)
			{
				componentInChildren.isChecked = true;
			}
			else
			{
				componentInChildren.isChecked = false;
			}
			EGDebug.Log("heroList[new_idx].HID: " + heroList[num].HID + "- mHeroSelectedName: " + mHeroSelectedID + " - isChecked: " + componentInChildren.isChecked);
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
		DeTuItem deTuItem = ItemList[0];
		DeTuItem deTuItem2 = ItemList[ItemList.Count - 1];
		ItemList.RemoveAt(ItemList.Count - 1);
		ItemList.Insert(0, deTuItem2);
		deTuItem2.transform.localPosition = deTuItem.transform.localPosition - vector2;
		deTuItem2.SetForPopupSelectNhanVat(heroList[num], isCreateByKyNgoUongRuou);
		UICheckbox componentInChildren = deTuItem2.GetComponentInChildren<UICheckbox>();
		if (componentInChildren != null)
		{
			if (heroList[num].HID == mHeroSelectedID)
			{
				componentInChildren.isChecked = true;
			}
			else
			{
				componentInChildren.isChecked = false;
			}
			EGDebug.Log("heroList[new_idx].HID: " + heroList[num].HID + "- mHeroSelectedName: " + mHeroSelectedID + " - isChecked: " + componentInChildren.isChecked);
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

	public static void CreateByKyNgoHuaNguyen(Func<int, bool> onFinish, List<int> ignore_list = null)
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
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("popup/PopupSelectNhanVat"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupSelectNhanVat>();
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
		foreach (UserInfo.HeroData hero in GameManager.instance.m_GameClient.UserInfo.HeroList)
		{
			if (ignore_list == null || !ignore_list.Contains(hero.HID))
			{
				heroList.Add(hero);
			}
		}
		heroList.Sort((UserInfo.HeroData x, UserInfo.HeroData y) => ConfigManager.instance.CompareNhanVat(x.Name, x.Level, y.Name, y.Level));
		if (heroList.Count > 0 && mHeroSelectedID == 0)
		{
			mHeroSelectedID = heroList[0].HID;
		}
		int num = 0;
		for (int num2 = startItemGUI_Idx; num2 < heroList.Count; num2++)
		{
			UserInfo.HeroData heroData = heroList[num2];
			DeTuItem component = ((GameObject)UnityEngine.Object.Instantiate(DeTuPrefab)).GetComponent<DeTuItem>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = vector;
			vector += vector2;
			component.SetForPopupSelectNhanVat(heroData, isCreateByKyNgoUongRuou);
			UICheckbox uICheckbox = component.GetComponentsInChildren<UICheckbox>(true)[0];
			if (heroData.HID == mHeroSelectedID)
			{
				uICheckbox.isChecked = true;
			}
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
		DeTuItem deTuItem = component.transform.parent.GetComponent<DeTuItem>();
		if (deTuItem == null)
		{
			deTuItem = component.GetComponentInParent<DeTuItem>();
		}
		if (deTuItem != null && component.isChecked)
		{
			mHeroSelectedID = deTuItem.m_Data.HID;
		}
		EGDebug.Log("HERO SELECTED: " + mHeroSelectedID);
		if (isCreateByKyNgoHuaNguyen && component.isChecked && deTuItem != null && deTuItem.m_Data != null)
		{
			NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[deTuItem.m_Data.Name];
			if (nhanVatCfg.HuaNguyen.Length < 3)
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoNVHuongNguyenLabel"));
			}
		}
	}

	public void nhanvat_onClick(GameObject go)
	{
		EGDebug.Log("nhanvat_onClick");
		DeTuItem component = go.GetComponent<DeTuItem>();
		if (component == null || component.m_Data == null)
		{
			return;
		}
		mHeroSelectedID = component.m_Data.HID;
		foreach (DeTuItem item in ItemList)
		{
			UICheckbox componentInChildren = item.GetComponentInChildren<UICheckbox>();
			if (componentInChildren != null)
			{
				componentInChildren.isChecked = item.m_Data != null && item.m_Data.HID == mHeroSelectedID;
			}
		}
		UICheckbox componentInChildren2 = component.GetComponentInChildren<UICheckbox>();
		if (isCreateByKyNgoHuaNguyen && componentInChildren2 != null && componentInChildren2.isChecked)
		{
			NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[component.m_Data.Name];
			if (nhanVatCfg.HuaNguyen.Length < 3)
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoNVHuongNguyenLabel"));
			}
		}
		if (isCreateByKyNgoUongRuou)
		{
			string value = "UR_" + component.m_Data.Name;
			if (component.m_Data.TiemLucBoSung == ConfigManager.instance.GetMaxTiemLucUongRuou(component.m_Data.Level))
			{
				MessagePopup.Create(Localization.instance.Get("MaxTiemLucUongRuouMess"));
			}
			else if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(value))
			{
				MessagePopup.Create(Localization.instance.Get("DeTuDaUongRuouMess"));
			}
		}
	}

	public void OnOkClick()
	{
		int hID = mHeroSelectedID;
		if (hID <= 0)
		{
			foreach (DeTuItem item in ItemList)
			{
				UICheckbox uICheckbox = item.GetComponentsInChildren<UICheckbox>(true)[0];
				if (uICheckbox.isChecked && item.m_Data != null)
				{
					hID = item.m_Data.HID;
					break;
				}
			}
		}
		if (hID <= 0 && heroList != null && heroList.Count > 0)
		{
			hID = heroList[0].HID;
		}
		if (hID > 0)
		{
			OnFinish(hID);
			DestroyPopup();
		}
	}

	public void OnCancelClick()
	{
		DestroyPopup();
	}
}

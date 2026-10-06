using System.Collections.Generic;
using UnityEngine;

public class ScreenDeTu : ScreenBase
{
	private enum ScreenDeTuTab
	{
		TabDeTu = 0,
		TabTanHon = 1
	}

	private const int maxItemCount = 12;

	public GameObject DeTuPrefab;

	public GameObject TanHonPrefab;

	public GameObject ItemRoot;

	private ScreenDeTuTab m_Tab;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<DeTuItem> ItemList = new List<DeTuItem>();

	private List<TanHonItem> TanHonItemList = new List<TanHonItem>();

	private List<UserInfo.HonNhanVatData> ListTanHonData = new List<UserInfo.HonNhanVatData>();

	private int startItemGUI_Idx;

	private Vector3 itemPos;

	private Vector3 itemOffset = new Vector3(0f, -160f, 0f);

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
	}

	private void Start()
	{
		dragPanel = ItemRoot.GetComponent<UIDraggablePanel>();
		panel = ItemRoot.GetComponent<UIPanel>();
	}

	private void Update()
	{
		if (ItemList.Count > 0 && m_Tab == ScreenDeTuTab.TabDeTu)
		{
			Vector4 clipRange = panel.clipRange;
			float num = 0f;
			float num2 = 0f;
			DeTuItem deTuItem = ItemList[ItemList.Count - 1];
			DeTuItem deTuItem2 = ItemList[0];
			num = deTuItem.transform.localPosition.y;
			num2 = deTuItem2.transform.localPosition.y;
			if (num - clipRange.y > -700f)
			{
				SwapDragListDown();
			}
			else if (num2 - clipRange.y < 700f)
			{
				SwapDragListUp();
			}
		}
		if (TanHonItemList.Count > 0 && m_Tab == ScreenDeTuTab.TabTanHon)
		{
			Vector4 clipRange2 = panel.clipRange;
			float num3 = 0f;
			float num4 = 0f;
			TanHonItem tanHonItem = TanHonItemList[TanHonItemList.Count - 1];
			TanHonItem tanHonItem2 = TanHonItemList[0];
			num3 = tanHonItem.transform.localPosition.y;
			num4 = tanHonItem2.transform.localPosition.y;
			if (num3 - clipRange2.y > -700f)
			{
				SwapDragListDown();
			}
			else if (num4 - clipRange2.y < 700f)
			{
				SwapDragListUp();
			}
		}
	}

	public void SwapDragListDown()
	{
		int num = startItemGUI_Idx + 12;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 320f, 0f);
		if (m_Tab == ScreenDeTuTab.TabDeTu)
		{
			if (num < GameManager.instance.m_GameClient.UserInfo.HeroList.Count)
			{
				DeTuItem deTuItem = ItemList[0];
				deTuItem.DeTuID = num;
				DeTuItem deTuItem2 = ItemList[ItemList.Count - 1];
				ItemList.RemoveAt(0);
				ItemList.Add(deTuItem);
				deTuItem.transform.localPosition = deTuItem2.transform.localPosition + itemOffset;
				deTuItem.SetForDeTuTab(GameManager.instance.m_GameClient.UserInfo.HeroList[num]);
				if (NGUITools.GetActive(deTuItem.focusItem.gameObject))
				{
					deTuItem.focusItem.SetActive(false);
				}
				startItemGUI_Idx++;
			}
		}
		else if (m_Tab == ScreenDeTuTab.TabTanHon && num < ListTanHonData.Count)
		{
			TanHonItem tanHonItem = TanHonItemList[0];
			tanHonItem.TanHonID = num;
			TanHonItem tanHonItem2 = TanHonItemList[TanHonItemList.Count - 1];
			TanHonItemList.RemoveAt(0);
			TanHonItemList.Add(tanHonItem);
			tanHonItem.transform.localPosition = tanHonItem2.transform.localPosition + itemOffset;
			tanHonItem.SetDataTanHonTab(ListTanHonData[num]);
			startItemGUI_Idx++;
		}
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
		vector = new Vector3(0f, 320f, 0f);
		if (m_Tab == ScreenDeTuTab.TabDeTu)
		{
			DeTuItem deTuItem = ItemList[0];
			DeTuItem deTuItem2 = ItemList[ItemList.Count - 1];
			if (ItemList.Count == 12)
			{
				ItemList.RemoveAt(ItemList.Count - 1);
				ItemList.Insert(0, deTuItem2);
				deTuItem2.DeTuID = num;
				deTuItem2.transform.localPosition = deTuItem.transform.localPosition - itemOffset;
				deTuItem2.SetForDeTuTab(GameManager.instance.m_GameClient.UserInfo.HeroList[num]);
				if (NGUITools.GetActive(deTuItem2.focusItem.gameObject))
				{
					deTuItem2.focusItem.SetActive(false);
				}
			}
			else if (ItemList.Count < 12)
			{
				DeTuItem component = ((GameObject)Object.Instantiate(DeTuPrefab)).GetComponent<DeTuItem>();
				component.DeTuID = num;
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = deTuItem.transform.localPosition - itemOffset;
				if (NGUITools.GetActive(deTuItem2.focusItem))
				{
					component.focusItem.SetActive(false);
				}
				component.SetForDeTuTab(GameManager.instance.m_GameClient.UserInfo.HeroList[num]);
				UIEventListener.Get(component.gameObject).onClick = onClick_DetuItem;
				UIEventListener.Get(component.btnBoiDuong.gameObject).onClick = onCLick_BoiDuongBtn;
				UIEventListener.Get(component.btnTuLuyen.gameObject).onClick = onCLick_TuLuyenBtn;
				UIEventListener.Get(component.btnTruyenCong.gameObject).onClick = onCLick_TruyenCongBtn;
				UIEventListener.Get(component.btnChuyenSinh.gameObject).onClick = onCLick_ChuyenSinhBtn;
				ItemList.Insert(0, component);
			}
		}
		else if (m_Tab == ScreenDeTuTab.TabTanHon)
		{
			TanHonItem tanHonItem = TanHonItemList[0];
			TanHonItem tanHonItem2 = TanHonItemList[TanHonItemList.Count - 1];
			if (TanHonItemList.Count == 12)
			{
				tanHonItem2.TanHonID = num;
				TanHonItemList.RemoveAt(TanHonItemList.Count - 1);
				TanHonItemList.Insert(0, tanHonItem2);
				tanHonItem2.transform.localPosition = tanHonItem.transform.localPosition - itemOffset;
				tanHonItem2.SetDataTanHonTab(ListTanHonData[num]);
			}
			else if (TanHonItemList.Count < 12)
			{
				TanHonItem component2 = ((GameObject)Object.Instantiate(TanHonPrefab)).GetComponent<TanHonItem>();
				component2.TanHonID = num;
				component2.transform.parent = ItemRoot.transform;
				component2.transform.localScale = new Vector3(1f, 1f, 1f);
				component2.transform.localPosition = tanHonItem.transform.localPosition - itemOffset;
				component2.SetDataTanHonTab(ListTanHonData[num]);
				UIEventListener.Get(component2.btnGoi.gameObject).onClick = onClick_BtnGoi;
				TanHonItemList.Insert(0, component2);
			}
		}
	}

	public void ClearGUIItem()
	{
		startItemGUI_Idx = 0;
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		TanHonItemList.Clear();
	}

	public void SyncWithNetworkData(bool forceRecreate = false)
	{
		if (m_Tab == ScreenDeTuTab.TabDeTu)
		{
			GameManager.instance.m_GameClient.UserInfo.HeroList.Sort((UserInfo.HeroData x, UserInfo.HeroData y) => ConfigManager.instance.CompareNhanVat(x.Name, x.Level, y.Name, y.Level));
		}
		else if (m_Tab == ScreenDeTuTab.TabTanHon)
		{
			ListTanHonData.Clear();
			GameManager.instance.m_GameClient.UserInfo.HonNhanVatList.Sort((UserInfo.HonNhanVatData x, UserInfo.HonNhanVatData y) => ConfigManager.instance.CompareTanHon(x.Name, x.Quantity, y.Name, y.Quantity));
			for (int num = 0; num < GameManager.instance.m_GameClient.UserInfo.HonNhanVatList.Count; num++)
			{
				if (GameManager.instance.m_GameClient.UserInfo.HonNhanVatList[num].Quantity > 0)
				{
					ListTanHonData.Add(GameManager.instance.m_GameClient.UserInfo.HonNhanVatList[num]);
				}
			}
		}
		if ((ItemRoot.transform.childCount == 0) | forceRecreate)
		{
			ClearGUIItem();
			itemPos = new Vector3(0f, 320f, 0f);
			int num2 = 0;
			if (m_Tab == ScreenDeTuTab.TabDeTu)
			{
				GameManager.instance.m_GameClient.UserInfo.HeroList.Sort((UserInfo.HeroData x, UserInfo.HeroData y) => ConfigManager.instance.CompareNhanVat(x.Name, x.Level, y.Name, y.Level));
				for (int num3 = startItemGUI_Idx; num3 < GameManager.instance.m_GameClient.UserInfo.HeroList.Count; num3++)
				{
					UserInfo.HeroData forDeTuTab = GameManager.instance.m_GameClient.UserInfo.HeroList[num3];
					DeTuItem component = ((GameObject)Object.Instantiate(DeTuPrefab)).GetComponent<DeTuItem>();
					component.DeTuID = num3;
					component.transform.parent = ItemRoot.transform;
					component.transform.localScale = new Vector3(1f, 1f, 1f);
					component.transform.localPosition = itemPos;
					itemPos += itemOffset;
					component.SetForDeTuTab(forDeTuTab);
					component.focusItem.gameObject.SetActive(false);
					UIEventListener.Get(component.gameObject).onClick = onClick_DetuItem;
					UIEventListener.Get(component.btnBoiDuong.gameObject).onClick = onCLick_BoiDuongBtn;
					UIEventListener.Get(component.btnTuLuyen.gameObject).onClick = onCLick_TuLuyenBtn;
					UIEventListener.Get(component.btnTruyenCong.gameObject).onClick = onCLick_TruyenCongBtn;
					UIEventListener.Get(component.btnChuyenSinh.gameObject).onClick = onCLick_ChuyenSinhBtn;
					ItemList.Add(component);
					num2++;
					if (num2 >= 12)
					{
						break;
					}
				}
			}
			else if (ListTanHonData.Count > 0)
			{
				for (int num4 = startItemGUI_Idx; num4 < ListTanHonData.Count; num4++)
				{
					UserInfo.HonNhanVatData dataTanHonTab = ListTanHonData[num4];
					TanHonItem component2 = ((GameObject)Object.Instantiate(TanHonPrefab)).GetComponent<TanHonItem>();
					component2.TanHonID = num4;
					component2.transform.parent = ItemRoot.transform;
					component2.transform.localScale = new Vector3(1f, 1f, 1f);
					component2.transform.localPosition = itemPos;
					itemPos += itemOffset;
					component2.SetDataTanHonTab(dataTanHonTab);
					UIEventListener.Get(component2.btnGoi.gameObject).onClick = onClick_BtnGoi;
					TanHonItemList.Add(component2);
					num2++;
					if (num2 >= 12)
					{
						break;
					}
				}
			}
			UIDraggablePanel component3 = ItemRoot.GetComponent<UIDraggablePanel>();
			component3.ResetPosition();
			return;
		}
		int num5 = 0;
		if (m_Tab == ScreenDeTuTab.TabDeTu)
		{
			List<UserInfo.HeroData> heroList = GameManager.instance.m_GameClient.UserInfo.HeroList;
			if (heroList.Count >= 12)
			{
				if (startItemGUI_Idx + 12 >= heroList.Count)
				{
					startItemGUI_Idx = heroList.Count - 12;
				}
			}
			else
			{
				startItemGUI_Idx = 0;
			}
			for (int num6 = startItemGUI_Idx; num6 < heroList.Count; num6++)
			{
				UserInfo.HeroData forDeTuTab2 = heroList[num6];
				if (num5 < ItemList.Count)
				{
					ItemList[num5].DeTuID = num6;
					ItemList[num5].SetForDeTuTab(forDeTuTab2);
				}
				else
				{
					DeTuItem component4 = ((GameObject)Object.Instantiate(DeTuPrefab)).GetComponent<DeTuItem>();
					component4.DeTuID = num6;
					component4.transform.parent = ItemRoot.transform;
					component4.transform.localScale = new Vector3(1f, 1f, 1f);
					component4.transform.localPosition = ItemList[ItemList.Count - 1].transform.localPosition + itemOffset;
					component4.SetForDeTuTab(forDeTuTab2);
					component4.focusItem.gameObject.SetActive(false);
					UIEventListener.Get(component4.gameObject).onClick = onClick_DetuItem;
					UIEventListener.Get(component4.btnBoiDuong.gameObject).onClick = onCLick_BoiDuongBtn;
					UIEventListener.Get(component4.btnTuLuyen.gameObject).onClick = onCLick_TuLuyenBtn;
					UIEventListener.Get(component4.btnTruyenCong.gameObject).onClick = onCLick_TruyenCongBtn;
					UIEventListener.Get(component4.btnChuyenSinh.gameObject).onClick = onCLick_ChuyenSinhBtn;
					ItemList.Add(component4);
				}
				num5++;
				if (num5 >= 12)
				{
					break;
				}
			}
			if (heroList.Count < 12 && ItemList.Count > heroList.Count)
			{
				int index = num5;
				int num7 = 0;
				for (; num5 < ItemList.Count; num5++)
				{
					Object.Destroy(ItemList[num5].gameObject);
					num7++;
				}
				if (num7 > 0)
				{
					ItemList.RemoveRange(index, num7);
				}
				UIDraggablePanel component5 = ItemRoot.GetComponent<UIDraggablePanel>();
				component5.ResetPosition();
			}
		}
		else
		{
			if (m_Tab != ScreenDeTuTab.TabTanHon)
			{
				return;
			}
			if (ListTanHonData.Count >= 12)
			{
				if (startItemGUI_Idx + 12 >= ListTanHonData.Count)
				{
					startItemGUI_Idx = ListTanHonData.Count - 12;
				}
			}
			else
			{
				startItemGUI_Idx = 0;
			}
			for (int num8 = startItemGUI_Idx; num8 < ListTanHonData.Count; num8++)
			{
				UserInfo.HonNhanVatData dataTanHonTab2 = ListTanHonData[num8];
				if (num5 < TanHonItemList.Count)
				{
					TanHonItemList[num5].TanHonID = num8;
					TanHonItemList[num5].SetDataTanHonTab(dataTanHonTab2);
				}
				else
				{
					TanHonItem component6 = ((GameObject)Object.Instantiate(TanHonPrefab)).GetComponent<TanHonItem>();
					component6.TanHonID = num8;
					component6.transform.parent = ItemRoot.transform;
					component6.transform.localScale = new Vector3(1f, 1f, 1f);
					component6.transform.localPosition = TanHonItemList[TanHonItemList.Count - 1].transform.localPosition + itemOffset;
					component6.SetDataTanHonTab(dataTanHonTab2);
					UIEventListener.Get(component6.btnGoi.gameObject).onClick = onClick_BtnGoi;
					TanHonItemList.Add(component6);
				}
				num5++;
				if (num5 >= 12)
				{
					break;
				}
			}
			if (ListTanHonData.Count < 12 && TanHonItemList.Count > ListTanHonData.Count)
			{
				int index2 = num5;
				int num9 = 0;
				for (; num5 < TanHonItemList.Count; num5++)
				{
					Object.Destroy(TanHonItemList[num5].gameObject);
					num9++;
				}
				if (num9 > 0)
				{
					TanHonItemList.RemoveRange(index2, num9);
				}
				UIDraggablePanel component7 = ItemRoot.GetComponent<UIDraggablePanel>();
				component7.ResetPosition();
			}
		}
	}

	public void onClick_BtnGoi(GameObject go)
	{
		TanHonItem itemSelected = go.transform.parent.GetComponent<TanHonItem>();
		if (!(itemSelected != null))
		{
			return;
		}
		if (itemSelected.tanHonType == TanHonItem.TanHonSelectedType.TuLuyen)
		{
			ScreenTuLuyen screenTuLuyen = GUIManager.getScreen(GAME_SCREEN.ScreenTuLuyen) as ScreenTuLuyen;
			UserInfo.HeroData data = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.Name == itemSelected.m_Data.Name);
			screenTuLuyen.Set(data);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTuLuyen);
		}
		else if (itemSelected.tanHonType == TanHonItem.TanHonSelectedType.TrieuHoi)
		{
			GameClient gameClient = GameManager.instance.m_GameClient;
			gameClient.RequestTrieuHonDeTuBangHon(itemSelected.m_Data.ID);
		}
	}

	public void onCLick_BoiDuongBtn(GameObject go)
	{
		DeTuItem component = go.transform.parent.parent.GetComponent<DeTuItem>();
		if (component != null)
		{
			ScreenBoiDuong screenBoiDuong = GUIManager.getScreen(GAME_SCREEN.ScreenBoiDuong) as ScreenBoiDuong;
			screenBoiDuong.Set(component.m_Data);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBoiDuong);
		}
	}

	public void onCLick_TuLuyenBtn(GameObject go)
	{
		DeTuItem component = go.transform.parent.parent.GetComponent<DeTuItem>();
		if (component != null)
		{
			ScreenTuLuyen screenTuLuyen = GUIManager.getScreen(GAME_SCREEN.ScreenTuLuyen) as ScreenTuLuyen;
			screenTuLuyen.Set(component.m_Data);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTuLuyen);
		}
	}

	public void onCLick_TruyenCongBtn(GameObject go)
	{
		DeTuItem component = go.transform.parent.parent.GetComponent<DeTuItem>();
		if (component != null)
		{
			ScreenTruyenCong screenTruyenCong = GUIManager.getScreen(GAME_SCREEN.ScreenTruyenCong) as ScreenTruyenCong;
			screenTruyenCong.Set(component.m_Data);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTruyenCong);
		}
	}

	public void onCLick_ChuyenSinhBtn(GameObject go)
	{
		DeTuItem component = go.transform.parent.parent.GetComponent<DeTuItem>();
		if (component != null)
		{
			if (component.m_Data.Level >= 300 && component.m_Data.CapDotPha == 3)
			{
				ScreenChuyenSinh screenChuyenSinh = GUIManager.getScreen(GAME_SCREEN.ScreenChuyenSinh) as ScreenChuyenSinh;
				screenChuyenSinh.Set(component.m_Data);
				GUIManager.instance.SetScreen(GAME_SCREEN.ScreenChuyenSinh);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoChuaDuDieuKienChuyenSinh"));
			}
		}
	}

	public void displayItemSelected()
	{
		if (ItemList.Count > 0)
		{
			DeTuItem deTuItem = ItemList[0];
			if (deTuItem != null)
			{
				deTuItem.focusItem.gameObject.SetActive(false);
				repositionItems(deTuItem);
			}
		}
	}

	public void onClick_DetuItem(GameObject go)
	{
		EGDebug.Log("onClick_DetuItem");
		DeTuItem component = go.GetComponent<DeTuItem>();
		repositionItems(component);
	}

	public void repositionItems(DeTuItem itemSelected)
	{
		DeTuItem deTuItem = ItemList[0];
		float y = deTuItem.transform.localPosition.y;
		itemPos = new Vector3(0f, y, 0f);
		int num = -1;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, deTuItem.bgFocusItem.transform.localScale.y, 0f);
		for (int i = 0; i < ItemList.Count; i++)
		{
			if (ItemList[i] == itemSelected && ItemList[i].DeTuID == itemSelected.DeTuID && !NGUITools.GetActive(ItemList[i].focusItem))
			{
				num = i;
			}
			if (num == ItemList.Count - 1 && num > 0)
			{
				itemPos += vector;
			}
		}
		for (int j = 0; j < ItemList.Count; j++)
		{
			DeTuItem deTuItem2 = ItemList[j];
			deTuItem2.transform.localPosition = itemPos;
			if (j == num)
			{
				deTuItem2.focusItem.gameObject.SetActive(true);
				itemPos = itemPos - vector + new Vector3(0f, -10f, 0f);
			}
			else
			{
				deTuItem2.focusItem.gameObject.SetActive(false);
				itemPos += itemOffset;
			}
		}
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		SyncWithNetworkData();
	}

	public void onClick_DetuTab(bool isActive)
	{
		if (isActive && m_Tab != ScreenDeTuTab.TabDeTu)
		{
			startItemGUI_Idx = 0;
			m_Tab = ScreenDeTuTab.TabDeTu;
			SyncWithNetworkData(true);
		}
	}

	public void onClick_TanHonTab(bool isActive)
	{
		if (isActive && m_Tab != ScreenDeTuTab.TabTanHon)
		{
			startItemGUI_Idx = 0;
			m_Tab = ScreenDeTuTab.TabTanHon;
			SyncWithNetworkData(true);
		}
	}

	public void updateListTanHon(TrieuHoiDeTuBangHonResponse responseTrieuHoi)
	{
		m_Tab = ScreenDeTuTab.TabTanHon;
		SyncWithNetworkData();
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenThuNhanDeTuResult);
		ScreenThuNhanDeTuResult screenThuNhanDeTuResult = GUIManager.getScreen(GAME_SCREEN.ScreenThuNhanDeTuResult) as ScreenThuNhanDeTuResult;
		LayDeTuResponse layDeTuResponse = new LayDeTuResponse();
		layDeTuResponse.NhanVatName = responseTrieuHoi.DeTuName;
		layDeTuResponse.TanHonCount = 0;
		screenThuNhanDeTuResult.setData(layDeTuResponse);
	}
}

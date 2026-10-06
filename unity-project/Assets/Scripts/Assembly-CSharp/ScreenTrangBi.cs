using System.Collections.Generic;
using UnityEngine;

public class ScreenTrangBi : ScreenBase
{
	public enum ScreenTrangBiTab
	{
		TabAll = 0,
		TabVuKhi = 1,
		TabGiap = 2,
		TabMu = 3,
		TabTrangSuc = 4
	}

	private const int maxItemCount = 12;

	public GameObject TrangBiPerfab;

	public GameObject ItemRoot;

	public ScreenTrangBiTab m_Tab;

	private LoaiTrangBi currentTrangBiType;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<TrangBiItem> ItemList = new List<TrangBiItem>();

	private List<UserInfo.TrangBiData> ListData = new List<UserInfo.TrangBiData>();

	private int startItemGUI_Idx;

	private Vector3 itemPos;

	private Vector3 itemOffset = new Vector3(0f, -160f, 0f);

	private int itemSelectedIndex = -1;

	private List<int> ignore_listID = new List<int>();

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
		if (ItemList.Count > 0)
		{
			Vector4 clipRange = panel.clipRange;
			TrangBiItem trangBiItem = ItemList[ItemList.Count - 1];
			float y = trangBiItem.transform.localPosition.y;
			TrangBiItem trangBiItem2 = ItemList[0];
			float y2 = trangBiItem2.transform.localPosition.y;
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
		addNextItem(ListData);
	}

	public void SwapDragListUp()
	{
		addPrevItem(ListData);
	}

	public void addNextItem(List<UserInfo.TrangBiData> listData)
	{
		if (listData != null || listData.Count > 0)
		{
			int num = startItemGUI_Idx + 12;
			if (num < listData.Count)
			{
				Vector3 vector = default(Vector3);
				vector = new Vector3(0f, 320f, 0f);
				TrangBiItem trangBiItem = ItemList[0];
				trangBiItem.TrangBiID = num;
				TrangBiItem trangBiItem2 = ItemList[ItemList.Count - 1];
				ItemList.RemoveAt(0);
				ItemList.Add(trangBiItem);
				trangBiItem.transform.localPosition = trangBiItem2.transform.localPosition + itemOffset;
				trangBiItem.focusItem.gameObject.SetActive(false);
				trangBiItem.SetForTrangBiTab(listData[num]);
				startItemGUI_Idx++;
			}
		}
	}

	public void addPrevItem(List<UserInfo.TrangBiData> listData)
	{
		if (listData == null || listData.Count <= 0 || startItemGUI_Idx == 0)
		{
			return;
		}
		startItemGUI_Idx--;
		int num = startItemGUI_Idx;
		if (num >= 0)
		{
			Vector3 vector = default(Vector3);
			vector = new Vector3(0f, 320f, 0f);
			TrangBiItem trangBiItem = ItemList[0];
			TrangBiItem trangBiItem2 = ItemList[ItemList.Count - 1];
			if (ItemList.Count == 12)
			{
				ItemList.RemoveAt(ItemList.Count - 1);
				ItemList.Insert(0, trangBiItem2);
				trangBiItem2.TrangBiID = num;
				trangBiItem2.transform.localPosition = trangBiItem.transform.localPosition - itemOffset;
				trangBiItem2.SetForTrangBiTab(listData[num]);
				trangBiItem2.focusItem.SetActive(false);
			}
			else if (ItemList.Count < 12)
			{
				TrangBiItem component = ((GameObject)Object.Instantiate(TrangBiPerfab)).GetComponent<TrangBiItem>();
				component.TrangBiID = num;
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = trangBiItem.transform.localPosition - itemOffset;
				component.focusItem.SetActive(false);
				component.SetForTrangBiTab(listData[num]);
				UIEventListener.Get(component.gameObject).onClick = onClick_TrangBiItem;
				UIEventListener.Get(component.btnCuongHoa.gameObject).onClick = btnCuongHoa_OnClick;
				UIEventListener.Get(component.btnTinhLuyen.gameObject).onClick = btnTinhLuyen_OnClick;
				UIEventListener.Get(component.btnKhamNam.gameObject).onClick = btnKhamNam_OnClick;
				UIEventListener.Get(component.btnHoangKim.gameObject).onClick = btnHoangKim_OnClick;
				ItemList.Insert(0, component);
			}
		}
	}

	public void SyncWithNetworkData(bool forceRecreate = false)
	{
		ListData.Clear();
		GameManager.instance.m_GameClient.UserInfo.TrangBiList.Sort((UserInfo.TrangBiData x, UserInfo.TrangBiData y) => ConfigManager.instance.CompareTrangBi(x.Name, x.Level, y.Name, y.Level, x.ID, y.ID));
		for (int num = 0; num < GameManager.instance.m_GameClient.UserInfo.TrangBiList.Count; num++)
		{
			if (m_Tab == ScreenTrangBiTab.TabAll)
			{
				ListData.Add(GameManager.instance.m_GameClient.UserInfo.TrangBiList[num]);
			}
			else if (m_Tab == ScreenTrangBiTab.TabVuKhi)
			{
				if (TrangBiCfg.GetLoaiTrangBi(GameManager.instance.m_GameClient.UserInfo.TrangBiList[num].Name) == LoaiTrangBi.VuKhi)
				{
					ListData.Add(GameManager.instance.m_GameClient.UserInfo.TrangBiList[num]);
				}
			}
			else if (m_Tab == ScreenTrangBiTab.TabGiap)
			{
				if (TrangBiCfg.GetLoaiTrangBi(GameManager.instance.m_GameClient.UserInfo.TrangBiList[num].Name) == LoaiTrangBi.AoGiap)
				{
					ListData.Add(GameManager.instance.m_GameClient.UserInfo.TrangBiList[num]);
				}
			}
			else if (m_Tab == ScreenTrangBiTab.TabTrangSuc)
			{
				if (TrangBiCfg.GetLoaiTrangBi(GameManager.instance.m_GameClient.UserInfo.TrangBiList[num].Name) == LoaiTrangBi.TrangSuc)
				{
					ListData.Add(GameManager.instance.m_GameClient.UserInfo.TrangBiList[num]);
				}
			}
			else if (m_Tab == ScreenTrangBiTab.TabMu && TrangBiCfg.GetLoaiTrangBi(GameManager.instance.m_GameClient.UserInfo.TrangBiList[num].Name) == LoaiTrangBi.Mu)
			{
				ListData.Add(GameManager.instance.m_GameClient.UserInfo.TrangBiList[num]);
			}
		}
		if ((ItemRoot.transform.childCount == 0) | forceRecreate)
		{
			ClearGUIItem();
			if (GameManager.instance.m_GameClient.UserInfo.TrangBiList != null && GameManager.instance.m_GameClient.UserInfo.TrangBiList.Count > 0)
			{
				UIDraggablePanel component = ItemRoot.GetComponent<UIDraggablePanel>();
				AddItem_ToList(ListData, 0);
				component.ResetPosition();
			}
			return;
		}
		int num2 = 0;
		if (ListData.Count >= 12)
		{
			if (startItemGUI_Idx + 12 >= ListData.Count)
			{
				startItemGUI_Idx = ListData.Count - 12;
			}
		}
		else
		{
			startItemGUI_Idx = 0;
		}
		for (int num3 = startItemGUI_Idx; num3 < ListData.Count; num3++)
		{
			UserInfo.TrangBiData forTrangBiTab = ListData[num3];
			if (num2 < ItemList.Count)
			{
				ItemList[num2].TrangBiID = num3;
				ItemList[num2].SetForTrangBiTab(forTrangBiTab);
			}
			else
			{
				TrangBiItem component2 = ((GameObject)Object.Instantiate(TrangBiPerfab)).GetComponent<TrangBiItem>();
				component2.TrangBiID = num3;
				component2.transform.parent = ItemRoot.transform;
				component2.transform.localScale = new Vector3(1f, 1f, 1f);
				component2.transform.localPosition = ItemList[ItemList.Count - 1].transform.localPosition + itemOffset;
				component2.SetForTrangBiTab(forTrangBiTab);
				component2.focusItem.gameObject.SetActive(false);
				UIEventListener.Get(component2.gameObject).onClick = onClick_TrangBiItem;
				UIEventListener.Get(component2.btnCuongHoa.gameObject).onClick = btnCuongHoa_OnClick;
				UIEventListener.Get(component2.btnTinhLuyen.gameObject).onClick = btnTinhLuyen_OnClick;
				UIEventListener.Get(component2.btnKhamNam.gameObject).onClick = btnKhamNam_OnClick;
				UIEventListener.Get(component2.btnHoangKim.gameObject).onClick = btnHoangKim_OnClick;
				ItemList.Add(component2);
			}
			num2++;
			if (num2 >= 12)
			{
				break;
			}
		}
		if (ListData.Count < 12 && ItemList.Count > ListData.Count)
		{
			int index = num2;
			int num4 = 0;
			for (; num2 < ItemList.Count; num2++)
			{
				Object.Destroy(ItemList[num2].gameObject);
				num4++;
			}
			if (num4 > 0)
			{
				ItemList.RemoveRange(index, num4);
			}
			UIDraggablePanel component3 = ItemRoot.GetComponent<UIDraggablePanel>();
			component3.ResetPosition();
		}
	}

	public void AddItem_ToList(List<UserInfo.TrangBiData> listData, int startIndex)
	{
		if (listData == null || listData.Count <= 0)
		{
			return;
		}
		int num = 0;
		itemPos = new Vector3(0f, 320f, 0f);
		for (int i = startIndex; i < listData.Count; i++)
		{
			UserInfo.TrangBiData forTrangBiTab = listData[i];
			TrangBiItem component = ((GameObject)Object.Instantiate(TrangBiPerfab)).GetComponent<TrangBiItem>();
			component.TrangBiID = i;
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = itemPos;
			itemPos += itemOffset;
			component.SetForTrangBiTab(forTrangBiTab);
			component.focusItem.gameObject.SetActive(false);
			UIEventListener.Get(component.gameObject).onClick = onClick_TrangBiItem;
			UIEventListener.Get(component.btnCuongHoa.gameObject).onClick = btnCuongHoa_OnClick;
			UIEventListener.Get(component.btnTinhLuyen.gameObject).onClick = btnTinhLuyen_OnClick;
			UIEventListener.Get(component.btnKhamNam.gameObject).onClick = btnKhamNam_OnClick;
			UIEventListener.Get(component.btnHoangKim.gameObject).onClick = btnHoangKim_OnClick;
			ItemList.Add(component);
			num++;
			if (num >= 12)
			{
				break;
			}
		}
		itemSelectedIndex = -1;
	}

	public void btnCuongHoa_OnClick(GameObject go)
	{
		TrangBiItem component = go.transform.parent.parent.GetComponent<TrangBiItem>();
		if (component != null)
		{
			ScreenCuongHoa screenCuongHoa = GUIManager.getScreen(GAME_SCREEN.ScreenCuongHoa) as ScreenCuongHoa;
			screenCuongHoa.Set(component.m_Data);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenCuongHoa);
		}
	}

	public void btnKhamNam_OnClick(GameObject go)
	{
		TrangBiItem component = go.transform.parent.parent.GetComponent<TrangBiItem>();
		if (component != null)
		{
			ScreenKhamNam screenKhamNam = GUIManager.getScreen(GAME_SCREEN.ScreenKhamNam) as ScreenKhamNam;
			screenKhamNam.Set(component.m_Data);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenKhamNam);
		}
	}

	public void btnTinhLuyen_OnClick(GameObject go)
	{
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level < 18)
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("LockTinhNangTinhLuyen"), 18));
			return;
		}
		TrangBiItem component = go.transform.parent.parent.GetComponent<TrangBiItem>();
		if (component != null)
		{
			ScreenTinhLuyen screenTinhLuyen = GUIManager.getScreen(GAME_SCREEN.ScreenTinhLuyen) as ScreenTinhLuyen;
			screenTinhLuyen.Set(component.m_Data);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTinhLuyen);
		}
	}

	public void btnHoangKim_OnClick(GameObject go)
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains("TrangBiHoangKim"))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		TrangBiItem component = go.transform.parent.parent.GetComponent<TrangBiItem>();
		if (component != null)
		{
			ScreenTrangBiHoangKim screenTrangBiHoangKim = GUIManager.getScreen(GAME_SCREEN.ScreenTrangBiHoangKim) as ScreenTrangBiHoangKim;
			screenTrangBiHoangKim.Set(component.m_Data);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTrangBiHoangKim);
		}
	}

	public void displayItemSelected()
	{
		if (ItemList.Count > 0)
		{
			TrangBiItem trangBiItem = ItemList[0];
			if (trangBiItem != null)
			{
				trangBiItem.focusItem.gameObject.SetActive(false);
				repositionItems(trangBiItem);
			}
		}
	}

	public void onClick_TrangBiItem(GameObject go)
	{
		TrangBiItem component = go.GetComponent<TrangBiItem>();
		repositionItems(component);
	}

	public void repositionItems(TrangBiItem itemSelected)
	{
		itemSelectedIndex = itemSelected.TrangBiID;
		TrangBiItem trangBiItem = ItemList[0];
		float y = trangBiItem.transform.localPosition.y;
		itemPos = new Vector3(0f, y, 0f);
		int num = -1;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, trangBiItem.bgFocusItem.transform.localScale.y, 0f);
		for (int i = 0; i < ItemList.Count; i++)
		{
			if (ItemList[i] == itemSelected && ItemList[i].TrangBiID == itemSelected.TrangBiID && !NGUITools.GetActive(ItemList[i].focusItem))
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
			TrangBiItem trangBiItem2 = ItemList[j];
			trangBiItem2.transform.localPosition = itemPos;
			if (j == num)
			{
				trangBiItem2.focusItem.gameObject.SetActive(true);
				itemPos = itemPos - vector + new Vector3(0f, -10f, 0f);
			}
			else
			{
				trangBiItem2.focusItem.gameObject.SetActive(false);
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
		SyncWithNetworkData();
	}

	private void onClick_AllTrangBi(bool isActive)
	{
		if (isActive && m_Tab != ScreenTrangBiTab.TabAll)
		{
			startItemGUI_Idx = 0;
			currentTrangBiType = LoaiTrangBi.None;
			m_Tab = ScreenTrangBiTab.TabAll;
			SyncWithNetworkData(true);
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
	}

	private void onClick_VuKhi(bool isActive)
	{
		if (isActive && m_Tab != ScreenTrangBiTab.TabVuKhi)
		{
			startItemGUI_Idx = 0;
			currentTrangBiType = LoaiTrangBi.VuKhi;
			m_Tab = ScreenTrangBiTab.TabVuKhi;
			SyncWithNetworkData(true);
		}
	}

	private void onClick_Mu(bool isActive)
	{
		if (isActive && m_Tab != ScreenTrangBiTab.TabMu)
		{
			startItemGUI_Idx = 0;
			currentTrangBiType = LoaiTrangBi.Mu;
			m_Tab = ScreenTrangBiTab.TabMu;
			SyncWithNetworkData(true);
		}
	}

	private void onClick_Giap(bool isActive)
	{
		if (isActive && m_Tab != ScreenTrangBiTab.TabGiap)
		{
			startItemGUI_Idx = 0;
			currentTrangBiType = LoaiTrangBi.AoGiap;
			m_Tab = ScreenTrangBiTab.TabGiap;
			SyncWithNetworkData(true);
		}
	}

	private void onClick_TrangSuc(bool isActive)
	{
		if (isActive && m_Tab != ScreenTrangBiTab.TabTrangSuc)
		{
			startItemGUI_Idx = 0;
			currentTrangBiType = LoaiTrangBi.TrangSuc;
			m_Tab = ScreenTrangBiTab.TabTrangSuc;
			SyncWithNetworkData(true);
		}
	}

	public void btnBan_OnClick(GameObject go)
	{
		ignore_listID.Clear();
		if (m_Tab == ScreenTrangBiTab.TabAll && GameManager.instance.m_GameClient.UserInfo.TrangBiList != null && GameManager.instance.m_GameClient.UserInfo.TrangBiList.Count > 0)
		{
			for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.TrangBiList.Count; i++)
			{
				TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[GameManager.instance.m_GameClient.UserInfo.TrangBiList[i].Name];
				if (GameManager.instance.m_GameClient.UserInfo.TrangBiList[i].HID > 0 || trangBiCfg.Hang >= ItemClass.Giap)
				{
					ignore_listID.Add(GameManager.instance.m_GameClient.UserInfo.TrangBiList[i].ID);
				}
			}
		}
		if (m_Tab != ScreenTrangBiTab.TabAll && ListData.Count > 0)
		{
			for (int j = 0; j < ListData.Count; j++)
			{
				TrangBiCfg trangBiCfg2 = ConfigManager.instance.m_dicTrangBi[ListData[j].Name];
				if (ListData[j].HID > 0 || trangBiCfg2.Hang >= ItemClass.Giap)
				{
					ignore_listID.Add(ListData[j].ID);
				}
			}
		}
		PopUpBanTrangBi.Create(OnSelectedBanTrangBi, ignore_listID, currentTrangBiType);
	}

	public void btnRa_OnClick(GameObject go)
	{
		ignore_listID.Clear();
		if (m_Tab == ScreenTrangBiTab.TabAll && GameManager.instance.m_GameClient.UserInfo.TrangBiList != null && GameManager.instance.m_GameClient.UserInfo.TrangBiList.Count > 0)
		{
			for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.TrangBiList.Count; i++)
			{
				TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[GameManager.instance.m_GameClient.UserInfo.TrangBiList[i].Name];
				if (GameManager.instance.m_GameClient.UserInfo.TrangBiList[i].HID > 0 || trangBiCfg.Hang >= ItemClass.Giap)
				{
					ignore_listID.Add(GameManager.instance.m_GameClient.UserInfo.TrangBiList[i].ID);
				}
			}
		}
		if (m_Tab != ScreenTrangBiTab.TabAll && ListData.Count > 0)
		{
			for (int j = 0; j < ListData.Count; j++)
			{
				TrangBiCfg trangBiCfg2 = ConfigManager.instance.m_dicTrangBi[ListData[j].Name];
				if (ListData[j].HID > 0 || trangBiCfg2.Hang >= ItemClass.Giap)
				{
					ignore_listID.Add(ListData[j].ID);
				}
			}
		}
		PopUpRaManhTrangBi.Create(OnSelectedRaTrangBi, ignore_listID, currentTrangBiType);
	}

	public bool OnSelectedBanTrangBi(List<int> listID)
	{
		if (listID.Count > 0)
		{
			BanTrangBiRequest banTrangBiRequest = new BanTrangBiRequest();
			banTrangBiRequest.TrangBiList = listID;
			GameManager.instance.m_GameClient.RequestBanTrangBi(banTrangBiRequest);
		}
		return true;
	}

	public bool OnSelectedRaTrangBi(List<int> listID)
	{
		if (listID.Count > 0)
		{
			BanTrangBiRequest banTrangBiRequest = new BanTrangBiRequest();
			banTrangBiRequest.TrangBiList = listID;
			GameManager.instance.m_GameClient.RequestPhanRaTrangBi(banTrangBiRequest);
		}
		return true;
	}

	public void openPopUpPhanThuong(PhanThuongResponse response)
	{
		if (response != null)
		{
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), response);
			SyncWithNetworkData();
		}
	}
}

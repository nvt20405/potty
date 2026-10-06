using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenNguyenKhi : ScreenBase
{
	private enum ScreenNguyenKhiTab
	{
		TabNguyenKhi = 0,
		TabKiemHon = 1
	}

	private const int maxItemCount = 12;

	public GameObject NguyenKhiPrefab;

	public GameObject KiemHonPrefab;

	public GameObject ItemRoot;

	public UILabel lbHienCoNKD;

	private ScreenNguyenKhiTab m_Tab;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<NguyenKhiItem> ItemList = new List<NguyenKhiItem>();

	private List<KiemHonItem> TanHonItemList = new List<KiemHonItem>();

	private int startItemGUI_Idx;

	private Vector3 itemPos;

	private Vector3 itemOffset = new Vector3(0f, -160f, 0f);

	private OtherCfg.NguyenKhiCfg currentNguyenKhiSelected;

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

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		SyncWithNetworkData();
		if (m_Tab == ScreenNguyenKhiTab.TabKiemHon)
		{
			displaySoLuongKiemHon();
		}
		else
		{
			lbHienCoNKD.text = string.Empty;
		}
	}

	public void displaySoLuongKiemHon()
	{
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_NGUYEN_KHI_DAN");
		if (vatPhamTieuThuData != null && vatPhamTieuThuData.Quantity > 0)
		{
			lbHienCoNKD.text = Localization.instance.Get("HienCoLabel") + ": " + vatPhamTieuThuData.Quantity;
		}
		else
		{
			lbHienCoNKD.text = Localization.instance.Get("HienCoLabel") + ": 0";
		}
	}

	private void Update()
	{
		if (ItemList.Count > 0 && m_Tab == ScreenNguyenKhiTab.TabNguyenKhi)
		{
			Vector4 clipRange = panel.clipRange;
			float num = 0f;
			float num2 = 0f;
			NguyenKhiItem nguyenKhiItem = ItemList[ItemList.Count - 1];
			NguyenKhiItem nguyenKhiItem2 = ItemList[0];
			num = nguyenKhiItem.transform.localPosition.y;
			num2 = nguyenKhiItem2.transform.localPosition.y;
			if (num - clipRange.y > -700f)
			{
				SwapDragListDown();
			}
			else if (num2 - clipRange.y < 700f)
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
		if (m_Tab == ScreenNguyenKhiTab.TabNguyenKhi && num < GameManager.instance.m_GameClient.UserInfo.NguyenKhiList.Count)
		{
			NguyenKhiItem nguyenKhiItem = ItemList[0];
			nguyenKhiItem.NguyenKhiID = num;
			NguyenKhiItem nguyenKhiItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(0);
			ItemList.Add(nguyenKhiItem);
			nguyenKhiItem.transform.localPosition = nguyenKhiItem2.transform.localPosition + itemOffset;
			nguyenKhiItem.setData(GameManager.instance.m_GameClient.UserInfo.NguyenKhiList[num]);
			if (NGUITools.GetActive(nguyenKhiItem.focusItem.gameObject))
			{
				nguyenKhiItem.focusItem.SetActive(false);
			}
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
		if (m_Tab != ScreenNguyenKhiTab.TabNguyenKhi)
		{
			return;
		}
		NguyenKhiItem nguyenKhiItem = ItemList[0];
		NguyenKhiItem nguyenKhiItem2 = ItemList[ItemList.Count - 1];
		if (ItemList.Count == 12)
		{
			ItemList.RemoveAt(ItemList.Count - 1);
			ItemList.Insert(0, nguyenKhiItem2);
			nguyenKhiItem2.NguyenKhiID = num;
			nguyenKhiItem2.transform.localPosition = nguyenKhiItem.transform.localPosition - itemOffset;
			nguyenKhiItem2.setData(GameManager.instance.m_GameClient.UserInfo.NguyenKhiList[num]);
			if (NGUITools.GetActive(nguyenKhiItem2.focusItem.gameObject))
			{
				nguyenKhiItem2.focusItem.SetActive(false);
			}
		}
		else if (ItemList.Count < 12)
		{
			NguyenKhiItem component = ((GameObject)Object.Instantiate(NguyenKhiPrefab)).GetComponent<NguyenKhiItem>();
			component.NguyenKhiID = num;
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = nguyenKhiItem.transform.localPosition - itemOffset;
			if (NGUITools.GetActive(nguyenKhiItem2.focusItem))
			{
				component.focusItem.SetActive(false);
			}
			component.setData(GameManager.instance.m_GameClient.UserInfo.NguyenKhiList[num]);
			UIEventListener.Get(component.gameObject).onClick = onClick_NguyenKhiItem;
			UIEventListener.Get(component.btnCuongHoa.gameObject).onClick = onClick_CuongHoaBtn;
			ItemList.Insert(0, component);
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

	public void SyncWithNetworkData(bool forceRecreate = false)
	{
		if ((ItemRoot.transform.childCount == 0) | forceRecreate)
		{
			ClearGUIItem();
			itemPos = new Vector3(0f, 320f, 0f);
			int num = 0;
			if (m_Tab == ScreenNguyenKhiTab.TabNguyenKhi)
			{
				if (GameManager.instance.m_GameClient.UserInfo.NguyenKhiList != null && GameManager.instance.m_GameClient.UserInfo.NguyenKhiList.Count > 0)
				{
					GameManager.instance.m_GameClient.UserInfo.NguyenKhiList.Sort((UserInfo.NguyenKhiData x, UserInfo.NguyenKhiData y) => CompareNguyenKhi(x.Codename, x.Level, y.Codename, y.Level));
					for (int num2 = startItemGUI_Idx; num2 < GameManager.instance.m_GameClient.UserInfo.NguyenKhiList.Count; num2++)
					{
						UserInfo.NguyenKhiData data = GameManager.instance.m_GameClient.UserInfo.NguyenKhiList[num2];
						NguyenKhiItem component = ((GameObject)Object.Instantiate(NguyenKhiPrefab)).GetComponent<NguyenKhiItem>();
						component.NguyenKhiID = num2;
						component.transform.parent = ItemRoot.transform;
						component.transform.localScale = new Vector3(1f, 1f, 1f);
						component.transform.localPosition = itemPos;
						itemPos += itemOffset;
						component.setData(data);
						component.focusItem.gameObject.SetActive(false);
						UIEventListener.Get(component.gameObject).onClick = onClick_NguyenKhiItem;
						UIEventListener.Get(component.btnCuongHoa.gameObject).onClick = onClick_CuongHoaBtn;
						ItemList.Add(component);
						num++;
						if (num >= 12)
						{
							break;
						}
					}
				}
			}
			else if (ConfigManager.instance.OtherConfig.NguyenKhiConfig != null && ConfigManager.instance.OtherConfig.NguyenKhiConfig.Count > 0)
			{
				for (int num3 = 0; num3 < ConfigManager.instance.OtherConfig.NguyenKhiConfig.Count; num3++)
				{
					OtherCfg.NguyenKhiCfg data2 = ConfigManager.instance.OtherConfig.NguyenKhiConfig.Values.ElementAt(num3);
					KiemHonItem component2 = ((GameObject)Object.Instantiate(KiemHonPrefab)).GetComponent<KiemHonItem>();
					component2.transform.parent = ItemRoot.transform;
					component2.transform.localScale = new Vector3(1f, 1f, 1f);
					component2.transform.localPosition = itemPos;
					itemPos += itemOffset;
					component2.setData(data2);
					UIEventListener.Get(component2.btnDoi.gameObject).onClick = onClick_BtnDoi;
				}
			}
			UIDraggablePanel component3 = ItemRoot.GetComponent<UIDraggablePanel>();
			component3.ResetPosition();
			return;
		}
		int num4 = 0;
		if (m_Tab != ScreenNguyenKhiTab.TabNguyenKhi)
		{
			return;
		}
		List<UserInfo.NguyenKhiData> nguyenKhiList = GameManager.instance.m_GameClient.UserInfo.NguyenKhiList;
		nguyenKhiList.Sort((UserInfo.NguyenKhiData x, UserInfo.NguyenKhiData y) => CompareNguyenKhi(x.Codename, x.Level, y.Codename, y.Level));
		if (nguyenKhiList.Count >= 12)
		{
			if (startItemGUI_Idx + 12 >= nguyenKhiList.Count)
			{
				startItemGUI_Idx = nguyenKhiList.Count - 12;
			}
		}
		else
		{
			startItemGUI_Idx = 0;
		}
		for (int num5 = startItemGUI_Idx; num5 < nguyenKhiList.Count; num5++)
		{
			UserInfo.NguyenKhiData data3 = nguyenKhiList[num5];
			if (num4 < ItemList.Count)
			{
				ItemList[num4].NguyenKhiID = num5;
				ItemList[num4].setData(data3);
			}
			else
			{
				NguyenKhiItem component4 = ((GameObject)Object.Instantiate(NguyenKhiPrefab)).GetComponent<NguyenKhiItem>();
				component4.NguyenKhiID = num5;
				component4.transform.parent = ItemRoot.transform;
				component4.transform.localScale = new Vector3(1f, 1f, 1f);
				component4.transform.localPosition = ItemList[ItemList.Count - 1].transform.localPosition + itemOffset;
				component4.setData(data3);
				component4.focusItem.gameObject.SetActive(false);
				UIEventListener.Get(component4.gameObject).onClick = onClick_NguyenKhiItem;
				UIEventListener.Get(component4.btnCuongHoa.gameObject).onClick = onClick_CuongHoaBtn;
				ItemList.Add(component4);
			}
			num4++;
			if (num4 >= 12)
			{
				break;
			}
		}
		if (nguyenKhiList.Count < 12 && ItemList.Count > nguyenKhiList.Count)
		{
			int index = num4;
			int num6 = 0;
			for (; num4 < ItemList.Count; num4++)
			{
				Object.Destroy(ItemList[num4].gameObject);
				num6++;
			}
			if (num6 > 0)
			{
				ItemList.RemoveRange(index, num6);
			}
			UIDraggablePanel component5 = ItemRoot.GetComponent<UIDraggablePanel>();
			component5.ResetPosition();
		}
	}

	public void onClick_BtnDoi(GameObject go)
	{
		currentNguyenKhiSelected = null;
		KiemHonItem component = go.transform.parent.GetComponent<KiemHonItem>();
		if (component != null && component.m_NguyenKhiData != null)
		{
			currentNguyenKhiSelected = component.m_NguyenKhiData;
			PopupYesNo.Create(Localization.instance.Get("DoiNguyenKhiDanConfirmMess"), Localization.instance.Get("Confirm"), Localization.instance.Get("Cancel"), OnDoiNguyenKhiConfirm, null);
		}
	}

	public void OnDoiNguyenKhiConfirm()
	{
		if (currentNguyenKhiSelected != null)
		{
			MuaNguyenKhiRequest muaNguyenKhiRequest = new MuaNguyenKhiRequest();
			muaNguyenKhiRequest.nguyenKhiType = currentNguyenKhiSelected.Loai;
			GameManager.instance.m_GameClient.RequestMuaNguyenKhi(muaNguyenKhiRequest);
		}
	}

	public void onClick_NguyenKhiItem(GameObject go)
	{
		NguyenKhiItem component = go.GetComponent<NguyenKhiItem>();
		repositionItems(component);
	}

	public void repositionItems(NguyenKhiItem itemSelected)
	{
		NguyenKhiItem nguyenKhiItem = ItemList[0];
		float y = nguyenKhiItem.transform.localPosition.y;
		itemPos = new Vector3(0f, y, 0f);
		int num = -1;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, nguyenKhiItem.bgFocusItem.transform.localScale.y, 0f);
		for (int i = 0; i < ItemList.Count; i++)
		{
			if (ItemList[i] == itemSelected && ItemList[i].NguyenKhiID == itemSelected.NguyenKhiID && !NGUITools.GetActive(ItemList[i].focusItem))
			{
				num = i;
			}
			if (num == ItemList.Count - 1 && num > 0)
			{
				itemPos += vector;
			}
		}
		EGDebug.Log("SELECTED INDEX::: " + num);
		for (int j = 0; j < ItemList.Count; j++)
		{
			NguyenKhiItem nguyenKhiItem2 = ItemList[j];
			nguyenKhiItem2.transform.localPosition = itemPos;
			if (j == num)
			{
				nguyenKhiItem2.focusItem.gameObject.SetActive(true);
				itemPos = itemPos - vector + new Vector3(0f, -10f, 0f);
			}
			else
			{
				nguyenKhiItem2.focusItem.gameObject.SetActive(false);
				itemPos += itemOffset;
			}
		}
	}

	public void onClick_CuongHoaBtn(GameObject go)
	{
		NguyenKhiItem component = go.transform.parent.parent.GetComponent<NguyenKhiItem>();
		if (component != null && component.m_NguyenKhiData != null)
		{
			PopUpNguyenKhi.DestroyPopup();
			ScreenCuongHoaNguyenKhi screenCuongHoaNguyenKhi = GUIManager.getScreen(GAME_SCREEN.ScreenCuongHoaNguyenKhi) as ScreenCuongHoaNguyenKhi;
			screenCuongHoaNguyenKhi.Set(component.m_NguyenKhiData);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenCuongHoaNguyenKhi);
		}
	}

	public void onClick_NguyenKhiTab(bool isActive)
	{
		if (isActive && m_Tab != ScreenNguyenKhiTab.TabNguyenKhi)
		{
			startItemGUI_Idx = 0;
			m_Tab = ScreenNguyenKhiTab.TabNguyenKhi;
			lbHienCoNKD.text = string.Empty;
			SyncWithNetworkData(true);
		}
	}

	public void onClick_KiemHonTab(bool isActive)
	{
		if (isActive && m_Tab != ScreenNguyenKhiTab.TabKiemHon)
		{
			startItemGUI_Idx = 0;
			m_Tab = ScreenNguyenKhiTab.TabKiemHon;
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_NGUYEN_KHI_DAN");
			if (vatPhamTieuThuData != null && vatPhamTieuThuData.Quantity > 0)
			{
				lbHienCoNKD.text = Localization.instance.Get("HienCoLabel") + ": " + vatPhamTieuThuData.Quantity;
			}
			else
			{
				lbHienCoNKD.text = Localization.instance.Get("HienCoLabel") + ": 0";
			}
			SyncWithNetworkData(true);
		}
	}

	public int CompareNguyenKhi(string codeName1, int level1, string codeName2, int level2)
	{
		if (!ConfigManager.instance.OtherConfig.NguyenKhiConfig.ContainsKey(codeName1) || !ConfigManager.instance.OtherConfig.NguyenKhiConfig.ContainsKey(codeName2))
		{
			return -1;
		}
		if (level1 > level2)
		{
			return -1;
		}
		if (level1 < level2)
		{
			return 1;
		}
		return codeName1.CompareTo(codeName2);
	}

	public void btnHelp_OnClick()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		if (m_Tab == ScreenNguyenKhiTab.TabNguyenKhi)
		{
			screenHelpInfo.setByLevel(9, 0);
		}
		else if (m_Tab == ScreenNguyenKhiTab.TabKiemHon)
		{
			screenHelpInfo.setByLevel(9, 1);
		}
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
		EGDebug.Log("btnHelp_OnClick");
	}

	public void btnDuocVien_OnClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenLienMinhTrongCay);
	}
}

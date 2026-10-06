using System.Collections.Generic;
using UnityEngine;

public class ScreenTop : ScreenBase
{
	public enum TopCongLucTab
	{
		TabTopMenh = 0,
		TabTopNgoai = 1,
		TabTopThan = 2,
		TabTopKhi = 3
	}

	private const int maxItemCount = 12;

	public GameObject MenuItemPrefab;

	public GameObject TopChienTruongPrefab;

	public GameObject TopTinhLuyenPrefab;

	public GameObject TopCongLucPrefab;

	public GameObject TopBiKipPrefab;

	public GameObject TopNgocPrefab;

	public GameObject TopTrangBiHKPrefab;

	public GameObject TopThienMaPrefab;

	public GameObject TopHanhTauPrefab;

	public GameObject TopHoangKimPrefab;

	public GameObject TopChuyenSinhPrefab;

	public GameObject TopTuLinhPrefab;

	public GameObject titleChienTruongGrp;

	public GameObject titleTinhLuyenGrp;

	public GameObject titleCongLucGrp;

	public GameObject titleBiKipGrp;

	public GameObject titleNgocGrp;

	public GameObject titleThienMaGrp;

	public GameObject titleTBHoangKimGrp;

	public GameObject titleHanhTauGrp;

	public GameObject titleHoangKimGrp;

	public GameObject titleChuyenSinhGrp;

	public GameObject titleTuLinhGrp;

	public GameObject MenuRoot;

	public GameObject ItemRoot;

	public GameObject CongLucItemRoot;

	public GameObject NormalGroupTitle;

	public GameObject CongLucGroupTitle;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<MenuTopItem> MenuItemList = new List<MenuTopItem>();

	private Vector3 itemPos;

	private Vector3 itemOffset = new Vector3(0f, -100f, 0f);

	private int startItemGUI_Idx;

	private CacLoaiTopRequest request = new CacLoaiTopRequest();

	private int MenuItemCount = 11;

	public TopCongLucTab m_CongLucTab;

	private List<CacLoaiTopResponse.TopCongLucData> listTopNgoaiData;

	private List<CacLoaiTopResponse.TopCongLucData> listTopMenhData;

	private List<CacLoaiTopResponse.TopCongLucData> listTopThanData;

	private List<CacLoaiTopResponse.TopCongLucData> listTopKhiData;

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

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		getListMenu();
	}

	public void getListMenu()
	{
		foreach (Transform item in MenuRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		MenuItemList.Clear();
		Vector3 vector = default(Vector3);
		vector = new Vector3(-145f, 0f, -1f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(180f, 0f, 0f);
		for (int i = 0; i < MenuItemCount; i++)
		{
			MenuTopItem menuTopItem = null;
			if (i == 0)
			{
				menuTopItem = ((GameObject)Object.Instantiate(MenuItemPrefab)).GetComponent<MenuTopItem>();
				menuTopItem.setData(MenuTopItem.MenuTop.MENU_TOP_CHIEN_TRUONG);
				UIEventListener.Get(menuTopItem.gameObject).onClick = onClick_TopChienTruongTab;
			}
			if (i == 1)
			{
				menuTopItem = ((GameObject)Object.Instantiate(MenuItemPrefab)).GetComponent<MenuTopItem>();
				menuTopItem.setData(MenuTopItem.MenuTop.MENU_TOP_TINH_LUYEN);
				vector += vector2;
				UIEventListener.Get(menuTopItem.gameObject).onClick = onClick_TopTinhLuyenTab;
			}
			if (i == 2)
			{
				menuTopItem = ((GameObject)Object.Instantiate(MenuItemPrefab)).GetComponent<MenuTopItem>();
				menuTopItem.setData(MenuTopItem.MenuTop.MENU_TOP_CONG_LUC);
				vector += vector2;
				UIEventListener.Get(menuTopItem.gameObject).onClick = onClick_TopCongLucTab;
			}
			if (i == 3)
			{
				menuTopItem = ((GameObject)Object.Instantiate(MenuItemPrefab)).GetComponent<MenuTopItem>();
				menuTopItem.setData(MenuTopItem.MenuTop.MENU_TOP_BI_KIP);
				vector += vector2;
				UIEventListener.Get(menuTopItem.gameObject).onClick = onClick_TopBiKipTab;
			}
			if (i == 4)
			{
				menuTopItem = ((GameObject)Object.Instantiate(MenuItemPrefab)).GetComponent<MenuTopItem>();
				menuTopItem.setData(MenuTopItem.MenuTop.MENU_TOP_NGOC);
				vector += vector2;
				UIEventListener.Get(menuTopItem.gameObject).onClick = onClick_TopNgocTab;
			}
			if (i == 5)
			{
				menuTopItem = ((GameObject)Object.Instantiate(MenuItemPrefab)).GetComponent<MenuTopItem>();
				menuTopItem.setData(MenuTopItem.MenuTop.MENU_TOP_HANH_TAU);
				vector += vector2;
				UIEventListener.Get(menuTopItem.gameObject).onClick = onClick_TopHanhTauTab;
			}
			if (i == 6)
			{
				menuTopItem = ((GameObject)Object.Instantiate(MenuItemPrefab)).GetComponent<MenuTopItem>();
				menuTopItem.setData(MenuTopItem.MenuTop.MENU_TOP_HOANG_KIM);
				vector += vector2;
				UIEventListener.Get(menuTopItem.gameObject).onClick = onClick_TopHoangKimTab;
			}
			if (i == 7)
			{
				menuTopItem = ((GameObject)Object.Instantiate(MenuItemPrefab)).GetComponent<MenuTopItem>();
				menuTopItem.setData(MenuTopItem.MenuTop.MENU_TOP_CHUYEN_SINH);
				vector += vector2;
				UIEventListener.Get(menuTopItem.gameObject).onClick = onClick_TopChuyenSinhTab;
			}
			if (i == 8)
			{
				menuTopItem = ((GameObject)Object.Instantiate(MenuItemPrefab)).GetComponent<MenuTopItem>();
				menuTopItem.setData(MenuTopItem.MenuTop.MENU_TOP_TU_LINH);
				vector += vector2;
				UIEventListener.Get(menuTopItem.gameObject).onClick = onClick_TopTuLinhTab;
			}
			if (i == 9)
			{
				menuTopItem = ((GameObject)Object.Instantiate(MenuItemPrefab)).GetComponent<MenuTopItem>();
				menuTopItem.setData(MenuTopItem.MenuTop.MENU_TOP_THIENMA);
				vector += vector2;
				UIEventListener.Get(menuTopItem.gameObject).onClick = onClick_TopThienMaTab;
			}
			if (i == 10)
			{
				menuTopItem = ((GameObject)Object.Instantiate(MenuItemPrefab)).GetComponent<MenuTopItem>();
				menuTopItem.setData(MenuTopItem.MenuTop.MENU_TOP_TBHOANGKIM);
				vector += vector2;
				UIEventListener.Get(menuTopItem.gameObject).onClick = onClick_TopTBHoangKimTab;
			}
			if (menuTopItem != null)
			{
				menuTopItem.transform.parent = MenuRoot.transform;
				menuTopItem.transform.localScale = new Vector3(1f, 1f, 1f);
				menuTopItem.transform.localPosition = vector;
				MenuItemList.Add(menuTopItem);
			}
		}
		if (MenuItemList[0].gameObject != null)
		{
			onClick_TopChienTruongTab(MenuItemList[0].gameObject);
		}
	}

	public void clearList()
	{
		itemPos = new Vector3(0f, 320f, 0f);
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		foreach (Transform item2 in CongLucItemRoot.transform)
		{
			Transform transform4 = item2;
			Object.Destroy(transform4.gameObject);
		}
	}

	public void displayTopChienTruong(List<CacLoaiTopResponse.TopChienTruongData> listData)
	{
		clearList();
		if (listData != null && listData.Count > 0)
		{
			itemPos = new Vector3(0f, 320f, 0f);
			for (int i = 0; i < listData.Count; i++)
			{
				TopChienTruongItem component = ((GameObject)Object.Instantiate(TopChienTruongPrefab)).GetComponent<TopChienTruongItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(listData[i], i + 1);
				itemPos += itemOffset;
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}

	public void displayTopTinhLuyen(List<CacLoaiTopResponse.TopTinhLuyenData> listData)
	{
		clearList();
		if (listData != null && listData.Count > 0)
		{
			itemPos = new Vector3(0f, 320f, 0f);
			for (int i = 0; i < listData.Count; i++)
			{
				TopTinhLuyenItem component = ((GameObject)Object.Instantiate(TopTinhLuyenPrefab)).GetComponent<TopTinhLuyenItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(listData[i], i + 1);
				itemPos += itemOffset;
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}

	public void displayTopCongLuc(CacLoaiTopResponse response)
	{
		listTopNgoaiData = response.ListTopCongLucNgoai;
		listTopMenhData = response.ListTopCongLucMenh;
		listTopThanData = response.ListTopCongLucThan;
		listTopKhiData = response.ListTopCongLucKhi;
		switch (m_CongLucTab)
		{
		case TopCongLucTab.TabTopNgoai:
			onClick_TopNgoai(true);
			break;
		case TopCongLucTab.TabTopThan:
			onClick_TopThan(true);
			break;
		case TopCongLucTab.TabTopKhi:
			onClick_TopKhi(true);
			break;
		default:
			onClick_TopMenh(true);
			break;
		}
	}

	public void displayTopBiKip(List<CacLoaiTopResponse.TopBiKipData> listData)
	{
		clearList();
		if (listData != null && listData.Count > 0)
		{
			itemPos = new Vector3(0f, 320f, 0f);
			for (int i = 0; i < listData.Count; i++)
			{
				TopBiKipItem component = ((GameObject)Object.Instantiate(TopBiKipPrefab)).GetComponent<TopBiKipItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(listData[i], i + 1);
				itemPos += itemOffset;
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}

	public void displayTopNgoc(List<CacLoaiTopResponse.TopNgocData> listData)
	{
		clearList();
		if (listData != null && listData.Count > 0)
		{
			itemPos = new Vector3(0f, 320f, 0f);
			for (int i = 0; i < listData.Count; i++)
			{
				TopNgocItem component = ((GameObject)Object.Instantiate(TopNgocPrefab)).GetComponent<TopNgocItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(listData[i], i + 1);
				itemPos += itemOffset;
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}

	public void displayTopHanhTau(List<CacLoaiTopResponse.TopHanhTauData> listData)
	{
		clearList();
		if (listData != null && listData.Count > 0)
		{
			itemPos = new Vector3(0f, 320f, 0f);
			for (int i = 0; i < listData.Count; i++)
			{
				TopHanhTauItem component = ((GameObject)Object.Instantiate(TopHanhTauPrefab)).GetComponent<TopHanhTauItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(listData[i], i + 1);
				itemPos += itemOffset;
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}

	public void displayTopHoangKim(List<CacLoaiTopResponse.TopHoangKimData> listData)
	{
		clearList();
		if (listData != null && listData.Count > 0)
		{
			itemPos = new Vector3(0f, 320f, 0f);
			for (int i = 0; i < listData.Count; i++)
			{
				TopHoangKimItem component = ((GameObject)Object.Instantiate(TopHoangKimPrefab)).GetComponent<TopHoangKimItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(listData[i], i + 1);
				itemPos += itemOffset;
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}

	public void displayTopChuyenSinh(List<CacLoaiTopResponse.TopChuyenSinhData> listData)
	{
		clearList();
		if (listData != null && listData.Count > 0)
		{
			itemPos = new Vector3(0f, 320f, 0f);
			for (int i = 0; i < listData.Count; i++)
			{
				TopChuyenSinhItem component = ((GameObject)Object.Instantiate(TopChuyenSinhPrefab)).GetComponent<TopChuyenSinhItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(listData[i], i + 1);
				itemPos += itemOffset;
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}

	public void displayTopTuLinh(List<CacLoaiTopResponse.TopTuLinhData> listData)
	{
		clearList();
		if (listData != null && listData.Count > 0)
		{
			itemPos = new Vector3(0f, 320f, 0f);
			for (int i = 0; i < listData.Count; i++)
			{
				TopTuLinhItem component = ((GameObject)Object.Instantiate(TopTuLinhPrefab)).GetComponent<TopTuLinhItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(listData[i], i + 1);
				itemPos += itemOffset;
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}

	public void displayTopThienMaLenh(List<CacLoaiTopResponse.TopThienMaData> listData)
	{
		clearList();
		if (listData != null && listData.Count > 0)
		{
			itemPos = new Vector3(0f, 320f, 0f);
			for (int i = 0; i < listData.Count; i++)
			{
				TopThienMaItem component = ((GameObject)Object.Instantiate(TopThienMaPrefab)).GetComponent<TopThienMaItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(listData[i], i + 1);
				itemPos += itemOffset;
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}

	public void displayTopTrangBiHK(List<CacLoaiTopResponse.TopTrangBiHoangKimData> listData)
	{
		clearList();
		if (listData != null && listData.Count > 0)
		{
			itemPos = new Vector3(0f, 320f, 0f);
			for (int i = 0; i < listData.Count; i++)
			{
				TopTrangBiHKItem component = ((GameObject)Object.Instantiate(TopTrangBiHKPrefab)).GetComponent<TopTrangBiHKItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(listData[i], i + 1);
				itemPos += itemOffset;
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}

	public void onClick_TopChienTruongTab(GameObject go)
	{
		MenuTopItem component = go.transform.GetComponent<MenuTopItem>();
		setFocusMenuItem(component);
		displayTitleSelected(MenuTopItem.MenuTop.MENU_TOP_CHIEN_TRUONG);
		request.LoaiTop = CacLoaiTopRequest.TopType.TopChienTruong;
		GameManager.instance.m_GameClient.RequestGetCacLoaiTop(request);
	}

	public void onClick_TopTinhLuyenTab(GameObject go)
	{
		MenuTopItem component = go.transform.GetComponent<MenuTopItem>();
		setFocusMenuItem(component);
		displayTitleSelected(MenuTopItem.MenuTop.MENU_TOP_TINH_LUYEN);
		request.LoaiTop = CacLoaiTopRequest.TopType.TopTinhLuyen;
		GameManager.instance.m_GameClient.RequestGetCacLoaiTop(request);
	}

	public void onClick_TopCongLucTab(GameObject go)
	{
		MenuTopItem component = go.transform.GetComponent<MenuTopItem>();
		setFocusMenuItem(component);
		displayTitleSelected(MenuTopItem.MenuTop.MENU_TOP_CONG_LUC);
		request.LoaiTop = CacLoaiTopRequest.TopType.TopCongLuc;
		GameManager.instance.m_GameClient.RequestGetCacLoaiTop(request);
	}

	public void onClick_TopBiKipTab(GameObject go)
	{
		MenuTopItem component = go.transform.GetComponent<MenuTopItem>();
		setFocusMenuItem(component);
		displayTitleSelected(MenuTopItem.MenuTop.MENU_TOP_BI_KIP);
		request.LoaiTop = CacLoaiTopRequest.TopType.TopBiKip;
		GameManager.instance.m_GameClient.RequestGetCacLoaiTop(request);
	}

	public void onClick_TopNgocTab(GameObject go)
	{
		MenuTopItem component = go.transform.GetComponent<MenuTopItem>();
		setFocusMenuItem(component);
		displayTitleSelected(MenuTopItem.MenuTop.MENU_TOP_NGOC);
		request.LoaiTop = CacLoaiTopRequest.TopType.TopNgoc;
		GameManager.instance.m_GameClient.RequestGetCacLoaiTop(request);
	}

	public void onClick_TopHanhTauTab(GameObject go)
	{
		MenuTopItem component = go.transform.GetComponent<MenuTopItem>();
		setFocusMenuItem(component);
		displayTitleSelected(MenuTopItem.MenuTop.MENU_TOP_HANH_TAU);
		request.LoaiTop = CacLoaiTopRequest.TopType.TopHanhTau;
		GameManager.instance.m_GameClient.RequestGetCacLoaiTop(request);
	}

	public void onClick_TopHoangKimTab(GameObject go)
	{
		MenuTopItem component = go.transform.GetComponent<MenuTopItem>();
		setFocusMenuItem(component);
		displayTitleSelected(MenuTopItem.MenuTop.MENU_TOP_HOANG_KIM);
		request.LoaiTop = CacLoaiTopRequest.TopType.TopHoangKim;
		GameManager.instance.m_GameClient.RequestGetCacLoaiTop(request);
	}

	public void onClick_TopChuyenSinhTab(GameObject go)
	{
		MenuTopItem component = go.transform.GetComponent<MenuTopItem>();
		setFocusMenuItem(component);
		displayTitleSelected(MenuTopItem.MenuTop.MENU_TOP_CHUYEN_SINH);
		request.LoaiTop = CacLoaiTopRequest.TopType.TopChuyenSinh;
		GameManager.instance.m_GameClient.RequestGetCacLoaiTop(request);
	}

	public void onClick_TopTuLinhTab(GameObject go)
	{
		MenuTopItem component = go.transform.GetComponent<MenuTopItem>();
		setFocusMenuItem(component);
		displayTitleSelected(MenuTopItem.MenuTop.MENU_TOP_TU_LINH);
		request.LoaiTop = CacLoaiTopRequest.TopType.TopTuLinh;
		GameManager.instance.m_GameClient.RequestGetCacLoaiTop(request);
	}

	public void onClick_TopThienMaTab(GameObject go)
	{
		MenuTopItem component = go.transform.GetComponent<MenuTopItem>();
		setFocusMenuItem(component);
		displayTitleSelected(MenuTopItem.MenuTop.MENU_TOP_THIENMA);
		request.LoaiTop = CacLoaiTopRequest.TopType.TopThienMaLenh;
		GameManager.instance.m_GameClient.RequestGetCacLoaiTop(request);
	}

	public void onClick_TopTBHoangKimTab(GameObject go)
	{
		MenuTopItem component = go.transform.GetComponent<MenuTopItem>();
		setFocusMenuItem(component);
		displayTitleSelected(MenuTopItem.MenuTop.MENU_TOP_TBHOANGKIM);
		request.LoaiTop = CacLoaiTopRequest.TopType.TopTrangBiHK;
		GameManager.instance.m_GameClient.RequestGetCacLoaiTop(request);
	}

	private void setFocusMenuItem(MenuTopItem item)
	{
		if (item != null)
		{
			for (int i = 0; i < MenuItemList.Count; i++)
			{
				MenuItemList[i].isSelected(false);
			}
			item.isSelected(true);
		}
	}

	private void displayTitleSelected(MenuTopItem.MenuTop type)
	{
		NormalGroupTitle.gameObject.SetActive(false);
		CongLucGroupTitle.gameObject.SetActive(false);
		ItemRoot.gameObject.SetActive(false);
		CongLucItemRoot.gameObject.SetActive(false);
		titleBiKipGrp.gameObject.SetActive(false);
		titleCongLucGrp.gameObject.SetActive(false);
		titleNgocGrp.gameObject.SetActive(false);
		titleTinhLuyenGrp.gameObject.SetActive(false);
		titleChienTruongGrp.gameObject.SetActive(false);
		titleHanhTauGrp.gameObject.SetActive(false);
		titleHoangKimGrp.gameObject.SetActive(false);
		titleChuyenSinhGrp.gameObject.SetActive(false);
		titleTuLinhGrp.gameObject.SetActive(false);
		titleThienMaGrp.gameObject.SetActive(false);
		titleTBHoangKimGrp.gameObject.SetActive(false);
		if (type == MenuTopItem.MenuTop.MENU_TOP_CONG_LUC)
		{
			NormalGroupTitle.gameObject.SetActive(false);
			CongLucGroupTitle.gameObject.SetActive(true);
			ItemRoot.gameObject.SetActive(false);
			CongLucItemRoot.gameObject.SetActive(true);
		}
		else
		{
			NormalGroupTitle.gameObject.SetActive(true);
			CongLucGroupTitle.gameObject.SetActive(false);
			ItemRoot.gameObject.SetActive(true);
			CongLucItemRoot.gameObject.SetActive(false);
		}
		switch (type)
		{
		case MenuTopItem.MenuTop.MENU_TOP_CHIEN_TRUONG:
			titleChienTruongGrp.gameObject.SetActive(true);
			break;
		case MenuTopItem.MenuTop.MENU_TOP_TINH_LUYEN:
			titleTinhLuyenGrp.gameObject.SetActive(true);
			break;
		case MenuTopItem.MenuTop.MENU_TOP_CONG_LUC:
			titleCongLucGrp.gameObject.SetActive(true);
			break;
		case MenuTopItem.MenuTop.MENU_TOP_BI_KIP:
			titleBiKipGrp.gameObject.SetActive(true);
			break;
		case MenuTopItem.MenuTop.MENU_TOP_NGOC:
			titleNgocGrp.gameObject.SetActive(true);
			break;
		case MenuTopItem.MenuTop.MENU_TOP_HANH_TAU:
			titleHanhTauGrp.gameObject.SetActive(true);
			break;
		case MenuTopItem.MenuTop.MENU_TOP_HOANG_KIM:
			titleHoangKimGrp.gameObject.SetActive(true);
			break;
		case MenuTopItem.MenuTop.MENU_TOP_CHUYEN_SINH:
			titleChuyenSinhGrp.gameObject.SetActive(true);
			break;
		case MenuTopItem.MenuTop.MENU_TOP_TU_LINH:
			titleTuLinhGrp.gameObject.SetActive(true);
			break;
		case MenuTopItem.MenuTop.MENU_TOP_THIENMA:
			titleThienMaGrp.gameObject.SetActive(true);
			break;
		case MenuTopItem.MenuTop.MENU_TOP_TBHOANGKIM:
			titleTBHoangKimGrp.gameObject.SetActive(true);
			break;
		}
	}

	public void onClick_TopMenh(bool isActive)
	{
		if (!isActive)
		{
			return;
		}
		clearList();
		m_CongLucTab = TopCongLucTab.TabTopMenh;
		if (listTopMenhData != null && listTopMenhData.Count > 0)
		{
			itemPos = new Vector3(0f, 320f, 0f);
			for (int i = 0; i < listTopMenhData.Count; i++)
			{
				TopCongLucItem component = ((GameObject)Object.Instantiate(TopCongLucPrefab)).GetComponent<TopCongLucItem>();
				component.transform.parent = CongLucItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(listTopMenhData[i], i + 1);
				itemPos += itemOffset;
				itemPos.y -= 15f;
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}

	public void onClick_TopNgoai(bool isActive)
	{
		if (!isActive)
		{
			return;
		}
		clearList();
		m_CongLucTab = TopCongLucTab.TabTopNgoai;
		if (listTopNgoaiData != null && listTopNgoaiData.Count > 0)
		{
			itemPos = new Vector3(0f, 320f, 0f);
			for (int i = 0; i < listTopNgoaiData.Count; i++)
			{
				TopCongLucItem component = ((GameObject)Object.Instantiate(TopCongLucPrefab)).GetComponent<TopCongLucItem>();
				component.transform.parent = CongLucItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(listTopNgoaiData[i], i + 1);
				itemPos += itemOffset;
				itemPos.y -= 15f;
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}

	public void onClick_TopThan(bool isActive)
	{
		if (!isActive)
		{
			return;
		}
		clearList();
		m_CongLucTab = TopCongLucTab.TabTopThan;
		if (listTopThanData != null && listTopThanData.Count > 0)
		{
			itemPos = new Vector3(0f, 320f, 0f);
			for (int i = 0; i < listTopThanData.Count; i++)
			{
				TopCongLucItem component = ((GameObject)Object.Instantiate(TopCongLucPrefab)).GetComponent<TopCongLucItem>();
				component.transform.parent = CongLucItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(listTopThanData[i], i + 1);
				itemPos += itemOffset;
				itemPos.y -= 15f;
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}

	public void onClick_TopKhi(bool isActive)
	{
		if (!isActive)
		{
			return;
		}
		clearList();
		m_CongLucTab = TopCongLucTab.TabTopKhi;
		if (listTopKhiData != null && listTopKhiData.Count > 0)
		{
			itemPos = new Vector3(0f, 320f, 0f);
			for (int i = 0; i < listTopKhiData.Count; i++)
			{
				TopCongLucItem component = ((GameObject)Object.Instantiate(TopCongLucPrefab)).GetComponent<TopCongLucItem>();
				component.transform.parent = CongLucItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(listTopKhiData[i], i + 1);
				itemPos += itemOffset;
				itemPos.y -= 15f;
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}
}

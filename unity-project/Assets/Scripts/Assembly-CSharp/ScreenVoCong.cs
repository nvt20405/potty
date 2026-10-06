using System.Collections.Generic;
using UnityEngine;

public class ScreenVoCong : ScreenBase
{
	private enum ScreenVoCongTab
	{
		TabAll = 0,
		TabChieuThuc = 1,
		TabBoPhap = 2,
		TabNoiCong = 3
	}

	private const int maxItemCount = 12;

	public GameObject ItemRoot;

	public GameObject VoCongPerfab;

	private ScreenVoCongTab m_Tab;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<VoCongItem> ItemList = new List<VoCongItem>();

	private int startItemGUI_Idx;

	private Vector3 itemPos;

	private Vector3 itemOffset = new Vector3(0f, -160f, 0f);

	private List<UserInfo.VoCongData> ListData = new List<UserInfo.VoCongData>();

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
			VoCongItem voCongItem = ItemList[ItemList.Count - 1];
			float y = voCongItem.transform.localPosition.y;
			VoCongItem voCongItem2 = ItemList[0];
			float y2 = voCongItem2.transform.localPosition.y;
			if (y - clipRange.y > -700f)
			{
				SwapDragListDown();
			}
			else if (y2 - clipRange.y < 700f)
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

	public void addNextItem(List<UserInfo.VoCongData> listData)
	{
		int num = startItemGUI_Idx + 12;
		if (num < listData.Count)
		{
			Vector3 vector = default(Vector3);
			vector = new Vector3(0f, 320f, 0f);
			Vector3 vector2 = default(Vector3);
			vector2 = new Vector3(0f, -150f, 0f);
			VoCongItem voCongItem = ItemList[0];
			voCongItem.VoCongID = num;
			VoCongItem voCongItem2 = ItemList[ItemList.Count - 1];
			if (NGUITools.GetActive(voCongItem.focusItem))
			{
				voCongItem.focusItem.SetActive(false);
			}
			ItemList.RemoveAt(0);
			ItemList.Add(voCongItem);
			voCongItem.transform.localPosition = voCongItem2.transform.localPosition + vector2;
			voCongItem.SetForVoCongTab(listData[num]);
			startItemGUI_Idx++;
		}
	}

	public void addPrevItem(List<UserInfo.VoCongData> listData)
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
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -150f, 0f);
		VoCongItem voCongItem = ItemList[0];
		VoCongItem voCongItem2 = ItemList[ItemList.Count - 1];
		if (ItemList.Count == 12)
		{
			ItemList.RemoveAt(ItemList.Count - 1);
			ItemList.Insert(0, voCongItem2);
			voCongItem2.VoCongID = num;
			voCongItem2.transform.localPosition = voCongItem.transform.localPosition - vector2;
			voCongItem2.SetForVoCongTab(listData[num]);
			if (NGUITools.GetActive(voCongItem2.focusItem))
			{
				voCongItem2.focusItem.SetActive(false);
			}
		}
		else if (ItemList.Count < 12)
		{
			VoCongItem component = ((GameObject)Object.Instantiate(VoCongPerfab)).GetComponent<VoCongItem>();
			component.VoCongID = num;
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = voCongItem.transform.localPosition - vector2;
			component.SetForVoCongTab(listData[num]);
			if (NGUITools.GetActive(component.focusItem))
			{
				component.focusItem.SetActive(false);
			}
			UIEventListener.Get(component.gameObject).onClick = onClick_VoCongItem;
			UIEventListener.Get(component.btnDotPha.gameObject).onClick = onClick_DotPhaItem;
			UIEventListener.Get(component.btnThamNgo.gameObject).onClick = onClick_ThamNgoItem;
			UIEventListener.Get(component.btnMoKhoa.gameObject).onClick = onClick_MoKhoaItem;
			ItemList.Insert(0, component);
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		SyncWithNetworkData();
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
		ListData.Clear();
		GameManager.instance.m_GameClient.UserInfo.VoCongList.Sort((UserInfo.VoCongData x, UserInfo.VoCongData y) => ConfigManager.instance.CompareVoCong(x.Name, x.Level, y.Name, y.Level));
		for (int num = 0; num < GameManager.instance.m_GameClient.UserInfo.VoCongList.Count; num++)
		{
			if (m_Tab == ScreenVoCongTab.TabAll)
			{
				ListData.Add(GameManager.instance.m_GameClient.UserInfo.VoCongList[num]);
			}
			else if (m_Tab == ScreenVoCongTab.TabChieuThuc)
			{
				CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[GameManager.instance.m_GameClient.UserInfo.VoCongList[num].Name];
				if (cfgVoCong != null && cfgVoCong.m_Class == VCClass.CHIEU_THUC)
				{
					ListData.Add(GameManager.instance.m_GameClient.UserInfo.VoCongList[num]);
				}
			}
			else if (m_Tab == ScreenVoCongTab.TabBoPhap)
			{
				CfgVoCong cfgVoCong2 = ConfigManager.instance.m_dicVCs[GameManager.instance.m_GameClient.UserInfo.VoCongList[num].Name];
				if (cfgVoCong2 != null && cfgVoCong2.m_Class == VCClass.BO_PHAP)
				{
					ListData.Add(GameManager.instance.m_GameClient.UserInfo.VoCongList[num]);
				}
			}
			else if (m_Tab == ScreenVoCongTab.TabNoiCong)
			{
				CfgVoCong cfgVoCong3 = ConfigManager.instance.m_dicVCs[GameManager.instance.m_GameClient.UserInfo.VoCongList[num].Name];
				if (cfgVoCong3 != null && cfgVoCong3.m_Class == VCClass.NOI_CONG)
				{
					ListData.Add(GameManager.instance.m_GameClient.UserInfo.VoCongList[num]);
				}
			}
		}
		if ((ItemRoot.transform.childCount == 0) | forceRecreate)
		{
			ClearGUIItem();
			if (GameManager.instance.m_GameClient.UserInfo.VoCongList != null && GameManager.instance.m_GameClient.UserInfo.VoCongList.Count > 0)
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
			UserInfo.VoCongData forVoCongTab = ListData[num3];
			if (num2 < ItemList.Count)
			{
				ItemList[num2].VoCongID = num3;
				ItemList[num2].SetForVoCongTab(forVoCongTab);
			}
			else
			{
				VoCongItem component2 = ((GameObject)Object.Instantiate(VoCongPerfab)).GetComponent<VoCongItem>();
				component2.VoCongID = num3;
				component2.transform.parent = ItemRoot.transform;
				component2.transform.localScale = new Vector3(1f, 1f, 1f);
				component2.transform.localPosition = ItemList[ItemList.Count - 1].transform.localPosition + itemOffset;
				component2.SetForVoCongTab(forVoCongTab);
				component2.focusItem.gameObject.SetActive(false);
				UIEventListener.Get(component2.gameObject).onClick = onClick_VoCongItem;
				UIEventListener.Get(component2.btnDotPha.gameObject).onClick = onClick_DotPhaItem;
				UIEventListener.Get(component2.btnThamNgo.gameObject).onClick = onClick_ThamNgoItem;
				UIEventListener.Get(component2.btnMoKhoa.gameObject).onClick = onClick_MoKhoaItem;
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

	public void AddItem_ToList(List<UserInfo.VoCongData> listData, int startIndex)
	{
		if (listData == null || listData.Count <= 0)
		{
			return;
		}
		int num = 0;
		itemPos = new Vector3(0f, 320f, 0f);
		for (int i = startIndex; i < listData.Count; i++)
		{
			UserInfo.VoCongData forVoCongTab = listData[i];
			VoCongItem component = ((GameObject)Object.Instantiate(VoCongPerfab)).GetComponent<VoCongItem>();
			component.VoCongID = i;
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = itemPos;
			itemPos += itemOffset;
			component.SetForVoCongTab(forVoCongTab);
			component.focusItem.gameObject.SetActive(false);
			UIEventListener.Get(component.gameObject).onClick = onClick_VoCongItem;
			UIEventListener.Get(component.btnDotPha.gameObject).onClick = onClick_DotPhaItem;
			UIEventListener.Get(component.btnThamNgo.gameObject).onClick = onClick_ThamNgoItem;
			UIEventListener.Get(component.btnMoKhoa.gameObject).onClick = onClick_MoKhoaItem;
			ItemList.Add(component);
			num++;
			if (num >= 12)
			{
				break;
			}
		}
	}

	public void onClick_DotPhaItem(GameObject go)
	{
		VoCongItem component = go.transform.parent.parent.GetComponent<VoCongItem>();
		if (component != null)
		{
			ScreenDotPha screenDotPha = GUIManager.getScreen(GAME_SCREEN.ScreenDotPha) as ScreenDotPha;
			screenDotPha.Set(component.m_Data);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDotPha);
		}
	}

	public void onClick_ThamNgoItem(GameObject go)
	{
		VoCongItem component = go.transform.parent.parent.GetComponent<VoCongItem>();
		if (component != null)
		{
			ScreenThamNgo screenThamNgo = GUIManager.getScreen(GAME_SCREEN.ScreenThamNgo) as ScreenThamNgo;
			screenThamNgo.Set(component.m_Data);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenThamNgo);
		}
	}

	public void onClick_MoKhoaItem(GameObject go)
	{
		VoCongItem component = go.transform.parent.parent.GetComponent<VoCongItem>();
		if (component != null && component.m_Data != null)
		{
			if (component.m_Data.Unlock == 1 || component.m_Data.Level > 9)
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoDaMoKhoaVoCong"));
			}
			else if (component.m_Data.Level == 9)
			{
				ScreenMoKhoa screenMoKhoa = GUIManager.getScreen(GAME_SCREEN.ScreenMoKhoa) as ScreenMoKhoa;
				screenMoKhoa.Set(component.m_Data);
				GUIManager.instance.SetScreen(GAME_SCREEN.ScreenMoKhoa);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("ThongbaoChuaDuDieuKienMoKhoa"));
			}
		}
	}

	public void onClick_VoCongItem(GameObject go)
	{
		VoCongItem component = go.GetComponent<VoCongItem>();
		repositionItems(component);
	}

	public void repositionItems(VoCongItem itemSelected)
	{
		VoCongItem voCongItem = ItemList[0];
		float y = voCongItem.transform.localPosition.y;
		itemPos = new Vector3(0f, y, 0f);
		int num = -1;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, voCongItem.bgFocusItem.transform.localScale.y, 0f);
		for (int i = 0; i < ItemList.Count; i++)
		{
			if (ItemList[i] == itemSelected && ItemList[i].VoCongID == itemSelected.VoCongID && !NGUITools.GetActive(ItemList[i].focusItem))
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
			VoCongItem voCongItem2 = ItemList[j];
			voCongItem2.transform.localPosition = itemPos;
			if (j == num)
			{
				voCongItem2.focusItem.gameObject.SetActive(true);
				itemPos = itemPos - vector + new Vector3(0f, -10f, 0f);
			}
			else
			{
				voCongItem2.focusItem.gameObject.SetActive(false);
				itemPos += itemOffset;
			}
		}
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	private void onClick_AllVoCong(bool isActive)
	{
		if (isActive && m_Tab != ScreenVoCongTab.TabAll)
		{
			startItemGUI_Idx = 0;
			m_Tab = ScreenVoCongTab.TabAll;
			SyncWithNetworkData(true);
		}
	}

	private void onClick_BoPhapBtn(bool isActive)
	{
		if (isActive && m_Tab != ScreenVoCongTab.TabBoPhap)
		{
			startItemGUI_Idx = 0;
			m_Tab = ScreenVoCongTab.TabBoPhap;
			SyncWithNetworkData(true);
		}
	}

	private void onClick_NoiCongBtn(bool isActive)
	{
		if (isActive && m_Tab != ScreenVoCongTab.TabNoiCong)
		{
			startItemGUI_Idx = 0;
			m_Tab = ScreenVoCongTab.TabNoiCong;
			SyncWithNetworkData(true);
		}
	}

	private void onClick_ChieuThucBtn(bool isActive)
	{
		if (isActive && m_Tab != ScreenVoCongTab.TabChieuThuc)
		{
			startItemGUI_Idx = 0;
			m_Tab = ScreenVoCongTab.TabChieuThuc;
			SyncWithNetworkData(true);
		}
	}
}

using System.Collections.Generic;
using UnityEngine;

public class ScreenListTrangBiHoangKim : ScreenBase
{
	public GameObject TrangBiPerfab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<TrangBiHoangKimItem> ItemList = new List<TrangBiHoangKimItem>();

	private List<UserInfo.TrangBiData> ListData = new List<UserInfo.TrangBiData>();

	private Vector3 itemPos;

	private Vector3 itemOffset = new Vector3(0f, -160f, 0f);

	private int itemSelectedIndex = -1;

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

	public void SyncWithNetworkData()
	{
		if (GameManager.instance.m_GameClient.UserInfo.TrangBiList == null || GameManager.instance.m_GameClient.UserInfo.TrangBiList.Count <= 0)
		{
			return;
		}
		ListData.Clear();
		if (ItemRoot.transform.childCount > 0)
		{
			ClearGUIItem();
		}
		GameManager.instance.m_GameClient.UserInfo.TrangBiList.Sort((UserInfo.TrangBiData x, UserInfo.TrangBiData y) => ConfigManager.instance.CompareTrangBi(x.Name, x.Level, y.Name, y.Level, x.ID, y.ID));
		for (int num = 0; num < GameManager.instance.m_GameClient.UserInfo.TrangBiList.Count; num++)
		{
			if (GameManager.instance.m_GameClient.UserInfo.TrangBiList[num].HoangKim >= 1)
			{
				ListData.Add(GameManager.instance.m_GameClient.UserInfo.TrangBiList[num]);
			}
		}
		itemPos = new Vector3(0f, 320f, 0f);
		if (ListData.Count > 0)
		{
			for (int num2 = 0; num2 < ListData.Count; num2++)
			{
				TrangBiHoangKimItem component = ((GameObject)Object.Instantiate(TrangBiPerfab)).GetComponent<TrangBiHoangKimItem>();
				component.TrangBiID = num2;
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				itemPos += itemOffset;
				component.SetData(ListData[num2]);
				component.focusItem.gameObject.SetActive(false);
				UIEventListener.Get(component.gameObject).onClick = onClick_TrangBiItem;
				UIEventListener.Get(component.btnLuyenHoa.gameObject).onClick = btnLuyenHoa_OnClick;
				ItemList.Add(component);
			}
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}

	public void ClearGUIItem()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
	}

	public void onClick_TrangBiItem(GameObject go)
	{
		TrangBiHoangKimItem component = go.GetComponent<TrangBiHoangKimItem>();
		repositionItems(component);
	}

	public void repositionItems(TrangBiHoangKimItem itemSelected)
	{
		itemSelectedIndex = itemSelected.TrangBiID;
		TrangBiHoangKimItem trangBiHoangKimItem = ItemList[0];
		float y = trangBiHoangKimItem.transform.localPosition.y;
		itemPos = new Vector3(0f, y, 0f);
		int num = -1;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, trangBiHoangKimItem.bgFocusItem.transform.localScale.y, 0f);
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
			TrangBiHoangKimItem trangBiHoangKimItem2 = ItemList[j];
			trangBiHoangKimItem2.transform.localPosition = itemPos;
			if (j == num)
			{
				trangBiHoangKimItem2.focusItem.gameObject.SetActive(true);
				itemPos = itemPos - vector + new Vector3(0f, -10f, 0f);
			}
			else
			{
				trangBiHoangKimItem2.focusItem.gameObject.SetActive(false);
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

	public void btnLuyenHoa_OnClick(GameObject go)
	{
		TrangBiHoangKimItem component = go.transform.parent.parent.GetComponent<TrangBiHoangKimItem>();
		if (component != null)
		{
			ScreenBoiDuongTrangBiHoangKim screenBoiDuongTrangBiHoangKim = GUIManager.getScreen(GAME_SCREEN.ScreenBoiDuongTrangBiHoangKim) as ScreenBoiDuongTrangBiHoangKim;
			screenBoiDuongTrangBiHoangKim.displayInfo(component.m_Data);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBoiDuongTrangBiHoangKim);
		}
	}
}

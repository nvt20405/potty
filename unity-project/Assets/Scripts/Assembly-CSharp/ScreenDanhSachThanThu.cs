using System.Collections.Generic;
using UnityEngine;

public class ScreenDanhSachThanThu : ScreenBase
{
	private enum ScreenDeTuTab
	{
		TabDeTu = 0,
		TabTanHon = 1
	}

	private const int maxItemCount = 12;

	public GameObject ThanThuPrefab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<ThanThuItem> ItemList = new List<ThanThuItem>();

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
		if (ItemList.Count > 0)
		{
			Vector4 clipRange = panel.clipRange;
			float num = 0f;
			float num2 = 0f;
			ThanThuItem thanThuItem = ItemList[ItemList.Count - 1];
			ThanThuItem thanThuItem2 = ItemList[0];
			num = thanThuItem.transform.localPosition.y;
			num2 = thanThuItem2.transform.localPosition.y;
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
		if (num < GameManager.instance.m_GameClient.UserInfo.ListThanThu.Count)
		{
			ThanThuItem thanThuItem = ItemList[0];
			thanThuItem.DeTuID = num;
			ThanThuItem thanThuItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(0);
			ItemList.Add(thanThuItem);
			thanThuItem.transform.localPosition = thanThuItem2.transform.localPosition + itemOffset;
			thanThuItem.SetForScreenThanThu(GameManager.instance.m_GameClient.UserInfo.ListThanThu[num]);
			if (NGUITools.GetActive(thanThuItem.focusItem.gameObject))
			{
				thanThuItem.focusItem.SetActive(false);
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
		ThanThuItem thanThuItem = ItemList[0];
		ThanThuItem thanThuItem2 = ItemList[ItemList.Count - 1];
		if (ItemList.Count == 12)
		{
			ItemList.RemoveAt(ItemList.Count - 1);
			ItemList.Insert(0, thanThuItem2);
			thanThuItem2.DeTuID = num;
			thanThuItem2.transform.localPosition = thanThuItem.transform.localPosition - itemOffset;
			thanThuItem2.SetForScreenThanThu(GameManager.instance.m_GameClient.UserInfo.ListThanThu[num]);
			if (NGUITools.GetActive(thanThuItem2.focusItem.gameObject))
			{
				thanThuItem2.focusItem.SetActive(false);
			}
		}
		else if (ItemList.Count < 12)
		{
			ThanThuItem component = ((GameObject)Object.Instantiate(ThanThuPrefab)).GetComponent<ThanThuItem>();
			component.DeTuID = num;
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = thanThuItem.transform.localPosition - itemOffset;
			if (NGUITools.GetActive(thanThuItem2.focusItem))
			{
				component.focusItem.SetActive(false);
			}
			component.SetForScreenThanThu(GameManager.instance.m_GameClient.UserInfo.ListThanThu[num]);
			UIEventListener.Get(component.gameObject).onClick = onClick_DetuItem;
			UIEventListener.Get(component.btnBienHinh.gameObject).onClick = OnBienHinhBtn;
			UIEventListener.Get(component.btnThonPhe.gameObject).onClick = OnThonPheBtn;
			UIEventListener.Get(component.btnTienHoa.gameObject).onClick = OnTienHoaBtn;
			UIEventListener.Get(component.btnTruongThanh.gameObject).onClick = OnTruongThanhBtn;
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
		GameManager.instance.m_GameClient.UserInfo.ListThanThu.Sort((UserInfo.PetInfo x, UserInfo.PetInfo y) => ConfigManager.instance.CompareThanThu(x, y));
		if ((ItemRoot.transform.childCount == 0) | forceRecreate)
		{
			ClearGUIItem();
			itemPos = new Vector3(0f, 320f, 0f);
			int num = 0;
			GameManager.instance.m_GameClient.UserInfo.ListThanThu.Sort((UserInfo.PetInfo x, UserInfo.PetInfo y) => ConfigManager.instance.CompareThanThu(x, y));
			for (int num2 = startItemGUI_Idx; num2 < GameManager.instance.m_GameClient.UserInfo.ListThanThu.Count; num2++)
			{
				UserInfo.PetInfo forScreenThanThu = GameManager.instance.m_GameClient.UserInfo.ListThanThu[num2];
				ThanThuItem component = ((GameObject)Object.Instantiate(ThanThuPrefab)).GetComponent<ThanThuItem>();
				component.DeTuID = num2;
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				itemPos += itemOffset;
				component.SetForScreenThanThu(forScreenThanThu);
				component.focusItem.gameObject.SetActive(false);
				UIEventListener.Get(component.gameObject).onClick = onClick_DetuItem;
				UIEventListener.Get(component.btnBienHinh.gameObject).onClick = OnBienHinhBtn;
				UIEventListener.Get(component.btnThonPhe.gameObject).onClick = OnThonPheBtn;
				UIEventListener.Get(component.btnTienHoa.gameObject).onClick = OnTienHoaBtn;
				UIEventListener.Get(component.btnTruongThanh.gameObject).onClick = OnTruongThanhBtn;
				ItemList.Add(component);
				num++;
				if (num >= 12)
				{
					break;
				}
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
			return;
		}
		int num3 = 0;
		List<UserInfo.PetInfo> listThanThu = GameManager.instance.m_GameClient.UserInfo.ListThanThu;
		if (listThanThu.Count >= 12)
		{
			if (startItemGUI_Idx + 12 >= listThanThu.Count)
			{
				startItemGUI_Idx = listThanThu.Count - 12;
			}
		}
		else
		{
			startItemGUI_Idx = 0;
		}
		for (int num4 = startItemGUI_Idx; num4 < listThanThu.Count; num4++)
		{
			UserInfo.PetInfo forScreenThanThu2 = listThanThu[num4];
			if (num3 < ItemList.Count)
			{
				ItemList[num3].DeTuID = num4;
				ItemList[num3].SetForScreenThanThu(forScreenThanThu2);
			}
			else
			{
				ThanThuItem component3 = ((GameObject)Object.Instantiate(ThanThuPrefab)).GetComponent<ThanThuItem>();
				component3.DeTuID = num4;
				component3.transform.parent = ItemRoot.transform;
				component3.transform.localScale = new Vector3(1f, 1f, 1f);
				component3.transform.localPosition = ItemList[ItemList.Count - 1].transform.localPosition + itemOffset;
				component3.SetForScreenThanThu(forScreenThanThu2);
				component3.focusItem.gameObject.SetActive(false);
				UIEventListener.Get(component3.gameObject).onClick = onClick_DetuItem;
				UIEventListener.Get(component3.btnBienHinh.gameObject).onClick = OnBienHinhBtn;
				UIEventListener.Get(component3.btnThonPhe.gameObject).onClick = OnThonPheBtn;
				UIEventListener.Get(component3.btnTienHoa.gameObject).onClick = OnTienHoaBtn;
				UIEventListener.Get(component3.btnTruongThanh.gameObject).onClick = OnTruongThanhBtn;
				ItemList.Add(component3);
			}
			num3++;
			if (num3 >= 12)
			{
				break;
			}
		}
		if (listThanThu.Count < 12 && ItemList.Count > listThanThu.Count)
		{
			int index = num3;
			int num5 = 0;
			for (; num3 < ItemList.Count; num3++)
			{
				Object.Destroy(ItemList[num3].gameObject);
				num5++;
			}
			if (num5 > 0)
			{
				ItemList.RemoveRange(index, num5);
			}
			UIDraggablePanel component4 = ItemRoot.GetComponent<UIDraggablePanel>();
			component4.ResetPosition();
		}
	}

	public void onClick_DetuItem(GameObject go)
	{
		EGDebug.Log("onClick_DetuItem");
		ThanThuItem component = go.GetComponent<ThanThuItem>();
		repositionItems(component);
	}

	public void repositionItems(ThanThuItem itemSelected)
	{
		ThanThuItem thanThuItem = ItemList[0];
		float y = thanThuItem.transform.localPosition.y;
		itemPos = new Vector3(0f, y, 0f);
		int num = -1;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, thanThuItem.bgFocusItem.transform.localScale.y, 0f);
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
			ThanThuItem thanThuItem2 = ItemList[j];
			thanThuItem2.transform.localPosition = itemPos;
			if (j == num)
			{
				thanThuItem2.focusItem.gameObject.SetActive(true);
				itemPos = itemPos - vector + new Vector3(0f, -10f, 0f);
			}
			else
			{
				thanThuItem2.focusItem.gameObject.SetActive(false);
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

	public void OnThonPheBtn(GameObject btn)
	{
		ScreenThonPheThanThu screenThonPheThanThu = GUIManager.getScreen(GAME_SCREEN.ScreenThonPheThanThu) as ScreenThonPheThanThu;
		screenThonPheThanThu.Set(btn.transform.parent.parent.GetComponent<ThanThuItem>().m_Data.ID);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenThonPheThanThu);
	}

	public void OnTienHoaBtn(GameObject btn)
	{
		UserInfo.PetInfo data = btn.transform.parent.parent.GetComponent<ThanThuItem>().m_Data;
		if (data.Quality == UserInfo.PetInfo.PetQuality.TRUYEN_THUYET && data.growRate == 1f)
		{
			MessagePopup.Create(Localization.instance.Get("ThanThuDatCapCaoNhat"));
			return;
		}
		ScreenNangPhamThanThu screenNangPhamThanThu = GUIManager.getScreen(GAME_SCREEN.ScreenNangPhamThanThu) as ScreenNangPhamThanThu;
		screenNangPhamThanThu.Set(btn.transform.parent.parent.GetComponent<ThanThuItem>().m_Data.ID, ScreenNangPhamThanThu.STATE.BEGIN);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenNangPhamThanThu);
	}

	public void OnTruongThanhBtn(GameObject btn)
	{
		ScreenTruongThanhThanThu screenTruongThanhThanThu = GUIManager.getScreen(GAME_SCREEN.ScreenTruongThanhThanThu) as ScreenTruongThanhThanThu;
		screenTruongThanhThanThu.Set(btn.transform.parent.parent.GetComponent<ThanThuItem>().m_Data.ID);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTruongThanhThanThu);
	}

	public void OnBienHinhBtn(GameObject btn)
	{
		ScreenTruyenCongThanThu screenTruyenCongThanThu = GUIManager.getScreen(GAME_SCREEN.ScreenTruyenCongThanThu) as ScreenTruyenCongThanThu;
		screenTruyenCongThanThu.Set(btn.transform.parent.parent.GetComponent<ThanThuItem>().m_Data.ID, 0);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTruyenCongThanThu);
	}
}

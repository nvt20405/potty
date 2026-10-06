using System.Collections.Generic;
using UnityEngine;

public class ScreenBanBe : ScreenBase
{
	private enum ScreenFriendsTab
	{
		TabHaoHuu = 0,
		TabCuuThu = 1,
		TabGiangHo = 2
	}

	private enum GiangHoAutoTab
	{
		AutoBatCoc = 0,
		AutoThachDau = 1
	}

	private const int maxItemCount = 20;

	public GameObject groupGiangHo;

	public GameObject AutoBatCocTab;

	public AutoThachDauFull AutoThachDauTab;

	public GameObject friendPerfab;

	public GameObject cuuThuPerfab;

	public GameObject ItemRoot;

	public UIButton btnSearch;

	public GameObject groupBatCocNormal;

	public UIButton BatCocAutoButton;

	public UILabel lbMaxLuotBatCoc;

	public UILabel lbBatCocButton;

	private GiangHoAutoTab m_GiangHoTab;

	private ScreenFriendsTab m_Tab;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<BanBeItem> BanBeItemList = new List<BanBeItem>();

	private List<CuuThuItem> CuuThuItemList = new List<CuuThuItem>();

	private int startItemGUI_Idx;

	private Vector3 itemPos;

	private Vector3 itemOffset = new Vector3(0f, -85f, 0f);

	private BanBeItem itemSelectedDelete;

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
		updateViewAuto();
		getListFriends();
		itemSelectedDelete = null;
	}

	public void ClearGUIItem()
	{
		startItemGUI_Idx = 0;
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		BanBeItemList.Clear();
		CuuThuItemList.Clear();
	}

	public void getListFriends(bool forceRecreate = false)
	{
		if (m_Tab == ScreenFriendsTab.TabHaoHuu)
		{
			if (GameManager.instance.m_GameClient.UserInfo.BanBeList == null || GameManager.instance.m_GameClient.UserInfo.BanBeList.Count <= 0)
			{
				ClearGUIItem();
				return;
			}
			GameManager.instance.m_GameClient.UserInfo.BanBeList.Sort((UserInfo.BanBeData x, UserInfo.BanBeData y) => CompareFriends(x, y));
		}
		if (m_Tab == ScreenFriendsTab.TabCuuThu)
		{
			if (GameManager.instance.m_GameClient.UserInfo.CuuThuList == null || GameManager.instance.m_GameClient.UserInfo.CuuThuList.Count <= 0)
			{
				ClearGUIItem();
				return;
			}
			GameManager.instance.m_GameClient.UserInfo.CuuThuList.Sort((UserInfo.CuuThuData x, UserInfo.CuuThuData y) => CompareCuuThu(x, y));
		}
		if ((ItemRoot.transform.childCount == 0) | forceRecreate)
		{
			ClearGUIItem();
			itemPos = new Vector3(0f, 320f, 0f);
			int num = 0;
			if (m_Tab == ScreenFriendsTab.TabHaoHuu)
			{
				for (int num2 = startItemGUI_Idx; num2 < GameManager.instance.m_GameClient.UserInfo.BanBeList.Count; num2++)
				{
					UserInfo.BanBeData friendData = GameManager.instance.m_GameClient.UserInfo.BanBeList[num2];
					BanBeItem component = ((GameObject)Object.Instantiate(friendPerfab)).GetComponent<BanBeItem>();
					component.itemID = num2;
					component.transform.parent = ItemRoot.transform;
					component.transform.localScale = new Vector3(1f, 1f, 1f);
					component.transform.localPosition = itemPos;
					itemPos += itemOffset;
					component.SetFriendData(friendData);
					UIEventListener.Get(component.gameObject).onClick = onClick_BanBeItem;
					UIEventListener.Get(component.focusItem.gameObject).onClick = onClick_BanBeItem;
					UIEventListener.Get(component.btnAccept.gameObject).onClick = acceptBtn_OnClick;
					UIEventListener.Get(component.btnDeny.gameObject).onClick = denyBtn_BanBeItem;
					UIEventListener.Get(component.btnTyThi.gameObject).onClick = tyThiBtn_BanBeItem;
					UIEventListener.Get(component.btnThongTin.gameObject).onClick = thongTinBtn_BanBeItem;
					UIEventListener.Get(component.btnGuiThu.gameObject).onClick = guiThuBtn_BanBeItem;
					UIEventListener.Get(component.btnXoa.gameObject).onClick = xoaBtn_BanBeItem;
					BanBeItemList.Add(component);
					num++;
					if (num >= 20)
					{
						break;
					}
				}
			}
			else
			{
				for (int num3 = startItemGUI_Idx; num3 < GameManager.instance.m_GameClient.UserInfo.CuuThuList.Count; num3++)
				{
					UserInfo.CuuThuData cuuThuData = GameManager.instance.m_GameClient.UserInfo.CuuThuList[num3];
					CuuThuItem component2 = ((GameObject)Object.Instantiate(cuuThuPerfab)).GetComponent<CuuThuItem>();
					component2.itemID = num3;
					component2.transform.parent = ItemRoot.transform;
					component2.transform.localScale = new Vector3(1f, 1f, 1f);
					component2.transform.localPosition = itemPos;
					itemPos += itemOffset;
					component2.SetCuuThuData(cuuThuData);
					UIEventListener.Get(component2.gameObject).onClick = onClick_CuuThuItem;
					UIEventListener.Get(component2.focusItem.gameObject).onClick = onClick_CuuThuItem;
					UIEventListener.Get(component2.btnThongTin.gameObject).onClick = thongTinBtn_CuuThuItem;
					UIEventListener.Get(component2.btnGuiThu.gameObject).onClick = guiThuBtn_CuuThuItem;
					UIEventListener.Get(component2.btnTraThu.gameObject).onClick = traThuBtn_CuuThuItem;
					CuuThuItemList.Add(component2);
					num++;
					if (num >= 20)
					{
						break;
					}
				}
			}
			UIDraggablePanel component3 = ItemRoot.GetComponent<UIDraggablePanel>();
			component3.ResetPosition();
			return;
		}
		int num4 = 0;
		if (m_Tab == ScreenFriendsTab.TabHaoHuu)
		{
			List<UserInfo.BanBeData> banBeList = GameManager.instance.m_GameClient.UserInfo.BanBeList;
			if (banBeList.Count >= 20)
			{
				if (startItemGUI_Idx + 20 >= banBeList.Count)
				{
					startItemGUI_Idx = banBeList.Count - 20;
				}
			}
			else
			{
				startItemGUI_Idx = 0;
			}
			for (int num5 = startItemGUI_Idx; num5 < banBeList.Count; num5++)
			{
				UserInfo.BanBeData friendData2 = banBeList[num5];
				if (num4 < BanBeItemList.Count)
				{
					BanBeItemList[num4].itemID = num5;
					if (NGUITools.GetActive(BanBeItemList[num4].focusItem.gameObject))
					{
						BanBeItemList[num4].SetFriendData(friendData2);
						BanBeItemList[num4].focusItem.gameObject.SetActive(true);
					}
					else
					{
						BanBeItemList[num4].SetFriendData(friendData2);
					}
				}
				else
				{
					BanBeItem component4 = ((GameObject)Object.Instantiate(friendPerfab)).GetComponent<BanBeItem>();
					component4.itemID = num5;
					component4.transform.parent = ItemRoot.transform;
					component4.transform.localScale = new Vector3(1f, 1f, 1f);
					component4.transform.localPosition = BanBeItemList[BanBeItemList.Count - 1].transform.localPosition + itemOffset;
					component4.SetFriendData(friendData2);
					UIEventListener.Get(component4.gameObject).onClick = onClick_BanBeItem;
					UIEventListener.Get(component4.focusItem.gameObject).onClick = onClick_BanBeItem;
					UIEventListener.Get(component4.btnAccept.gameObject).onClick = acceptBtn_OnClick;
					UIEventListener.Get(component4.btnDeny.gameObject).onClick = denyBtn_BanBeItem;
					UIEventListener.Get(component4.btnTyThi.gameObject).onClick = tyThiBtn_BanBeItem;
					UIEventListener.Get(component4.btnThongTin.gameObject).onClick = thongTinBtn_BanBeItem;
					UIEventListener.Get(component4.btnGuiThu.gameObject).onClick = guiThuBtn_BanBeItem;
					UIEventListener.Get(component4.btnXoa.gameObject).onClick = xoaBtn_BanBeItem;
					BanBeItemList.Add(component4);
				}
				num4++;
				if (num4 >= 20)
				{
					break;
				}
			}
			if (banBeList.Count < 20 && BanBeItemList.Count > banBeList.Count)
			{
				int index = num4;
				int num6 = 0;
				for (; num4 < BanBeItemList.Count; num4++)
				{
					Object.Destroy(BanBeItemList[num4].gameObject);
					num6++;
				}
				if (num6 > 0)
				{
					BanBeItemList.RemoveRange(index, num6);
				}
				UIDraggablePanel component5 = ItemRoot.GetComponent<UIDraggablePanel>();
				component5.ResetPosition();
			}
		}
		else
		{
			if (m_Tab != ScreenFriendsTab.TabCuuThu)
			{
				return;
			}
			List<UserInfo.CuuThuData> cuuThuList = GameManager.instance.m_GameClient.UserInfo.CuuThuList;
			if (cuuThuList.Count >= 20)
			{
				if (startItemGUI_Idx + 20 >= cuuThuList.Count)
				{
					startItemGUI_Idx = cuuThuList.Count - 20;
				}
			}
			else
			{
				startItemGUI_Idx = 0;
			}
			for (int num7 = startItemGUI_Idx; num7 < cuuThuList.Count; num7++)
			{
				UserInfo.CuuThuData cuuThuData2 = cuuThuList[num7];
				if (num4 < CuuThuItemList.Count)
				{
					CuuThuItemList[num4].itemID = num7;
					if (NGUITools.GetActive(CuuThuItemList[num4].focusItem.gameObject))
					{
						CuuThuItemList[num4].SetCuuThuData(cuuThuData2);
						CuuThuItemList[num4].focusItem.gameObject.SetActive(true);
					}
					else
					{
						CuuThuItemList[num4].SetCuuThuData(cuuThuData2);
					}
				}
				else
				{
					CuuThuItem component6 = ((GameObject)Object.Instantiate(cuuThuPerfab)).GetComponent<CuuThuItem>();
					component6.itemID = num7;
					component6.transform.parent = ItemRoot.transform;
					component6.transform.localScale = new Vector3(1f, 1f, 1f);
					component6.transform.localPosition = CuuThuItemList[CuuThuItemList.Count - 1].transform.localPosition + itemOffset;
					component6.SetCuuThuData(cuuThuData2);
					UIEventListener.Get(component6.gameObject).onClick = onClick_CuuThuItem;
					UIEventListener.Get(component6.focusItem.gameObject).onClick = onClick_CuuThuItem;
					UIEventListener.Get(component6.btnThongTin.gameObject).onClick = thongTinBtn_CuuThuItem;
					UIEventListener.Get(component6.btnGuiThu.gameObject).onClick = guiThuBtn_CuuThuItem;
					UIEventListener.Get(component6.btnTraThu.gameObject).onClick = traThuBtn_CuuThuItem;
					CuuThuItemList.Add(component6);
				}
				num4++;
				if (num4 >= 20)
				{
					break;
				}
			}
			if (cuuThuList.Count < 20 && CuuThuItemList.Count > cuuThuList.Count)
			{
				int index2 = num4;
				int num8 = 0;
				for (; num4 < CuuThuItemList.Count; num4++)
				{
					Object.Destroy(CuuThuItemList[num4].gameObject);
					num8++;
				}
				if (num8 > 0)
				{
					CuuThuItemList.RemoveRange(index2, num8);
				}
				UIDraggablePanel component7 = ItemRoot.GetComponent<UIDraggablePanel>();
				component7.ResetPosition();
			}
		}
	}

	public void onClick_BanBeItem(GameObject go)
	{
		BanBeItem component = go.GetComponent<BanBeItem>();
		if (!(component != null) || component.mFriend_Data == null || component.mFriend_Data.Status != UserInfo.BanBeData.BanBeStatus.REQUESTING)
		{
			repositionBanBeItems(component);
		}
	}

	public void onClick_CuuThuItem(GameObject go)
	{
		CuuThuItem component = go.GetComponent<CuuThuItem>();
		repositionCuuThuItems(component);
	}

	public void repositionCuuThuItems(CuuThuItem itemSelected)
	{
		CuuThuItem cuuThuItem = CuuThuItemList[0];
		float y = cuuThuItem.transform.localPosition.y;
		itemPos = new Vector3(0f, y, 0f);
		int num = -1;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, cuuThuItem.bgFocusItem.transform.localScale.y, 0f);
		for (int i = 0; i < CuuThuItemList.Count; i++)
		{
			if (CuuThuItemList[i] == itemSelected && CuuThuItemList[i].itemID == itemSelected.itemID && !NGUITools.GetActive(CuuThuItemList[i].focusItem))
			{
				num = i;
			}
			if (num == CuuThuItemList.Count - 1 && num > 0)
			{
				itemPos += vector;
			}
		}
		for (int j = 0; j < CuuThuItemList.Count; j++)
		{
			CuuThuItem cuuThuItem2 = CuuThuItemList[j];
			cuuThuItem2.transform.localPosition = itemPos;
			if (j == num)
			{
				cuuThuItem2.focusItem.gameObject.SetActive(true);
				itemPos = itemPos - vector + new Vector3(0f, -10f, 0f);
			}
			else
			{
				cuuThuItem2.focusItem.gameObject.SetActive(false);
				itemPos += itemOffset;
			}
		}
	}

	public void repositionBanBeItems(BanBeItem itemSelected)
	{
		BanBeItem banBeItem = BanBeItemList[0];
		float y = banBeItem.transform.localPosition.y;
		itemPos = new Vector3(0f, y, 0f);
		int num = -1;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, banBeItem.bgFocusItem.transform.localScale.y, 0f);
		for (int i = 0; i < BanBeItemList.Count; i++)
		{
			if (BanBeItemList[i] == itemSelected && BanBeItemList[i].itemID == itemSelected.itemID && !NGUITools.GetActive(BanBeItemList[i].focusItem))
			{
				num = i;
			}
			if (num == BanBeItemList.Count - 1 && num > 0)
			{
				itemPos += vector;
			}
		}
		for (int j = 0; j < BanBeItemList.Count; j++)
		{
			BanBeItem banBeItem2 = BanBeItemList[j];
			banBeItem2.transform.localPosition = itemPos;
			if (j == num)
			{
				banBeItem2.focusItem.gameObject.SetActive(true);
				itemPos = itemPos - vector + new Vector3(0f, -10f, 0f);
			}
			else
			{
				banBeItem2.focusItem.gameObject.SetActive(false);
				itemPos += itemOffset;
			}
		}
	}

	private void Update()
	{
		if (m_Tab == ScreenFriendsTab.TabHaoHuu)
		{
			if (BanBeItemList.Count > 0)
			{
				Vector4 clipRange = panel.clipRange;
				BanBeItem banBeItem = BanBeItemList[BanBeItemList.Count - 1];
				float y = banBeItem.transform.localPosition.y;
				BanBeItem banBeItem2 = BanBeItemList[0];
				float y2 = banBeItem2.transform.localPosition.y;
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
		else if (m_Tab == ScreenFriendsTab.TabCuuThu && CuuThuItemList.Count > 0)
		{
			Vector4 clipRange2 = panel.clipRange;
			CuuThuItem cuuThuItem = CuuThuItemList[CuuThuItemList.Count - 1];
			float y3 = cuuThuItem.transform.localPosition.y;
			CuuThuItem cuuThuItem2 = CuuThuItemList[0];
			float y4 = cuuThuItem2.transform.localPosition.y;
			if (y3 - clipRange2.y > -600f)
			{
				SwapDragListDown();
			}
			else if (y4 - clipRange2.y < 600f)
			{
				SwapDragListUp();
			}
		}
	}

	public void SwapDragListDown()
	{
		addNextItem();
	}

	public void SwapDragListUp()
	{
		addPrevItem();
	}

	public void addNextItem()
	{
		if (m_Tab == ScreenFriendsTab.TabHaoHuu)
		{
			if (GameManager.instance.m_GameClient.UserInfo.BanBeList == null && GameManager.instance.m_GameClient.UserInfo.BanBeList.Count <= 0)
			{
				return;
			}
			int num = startItemGUI_Idx + 20;
			if (num < GameManager.instance.m_GameClient.UserInfo.BanBeList.Count)
			{
				Vector3 vector = default(Vector3);
				vector = new Vector3(0f, 320f, 0f);
				BanBeItem banBeItem = BanBeItemList[0];
				banBeItem.itemID = num;
				BanBeItem banBeItem2 = BanBeItemList[BanBeItemList.Count - 1];
				if (NGUITools.GetActive(banBeItem.focusItem))
				{
					banBeItem.focusItem.SetActive(false);
				}
				BanBeItemList.RemoveAt(0);
				BanBeItemList.Add(banBeItem);
				banBeItem.transform.localPosition = banBeItem2.transform.localPosition + itemOffset;
				banBeItem.SetFriendData(GameManager.instance.m_GameClient.UserInfo.BanBeList[num]);
				startItemGUI_Idx++;
			}
		}
		else
		{
			if (m_Tab != ScreenFriendsTab.TabCuuThu || (GameManager.instance.m_GameClient.UserInfo.CuuThuList == null && GameManager.instance.m_GameClient.UserInfo.CuuThuList.Count <= 0))
			{
				return;
			}
			int num2 = startItemGUI_Idx + 20;
			if (num2 < GameManager.instance.m_GameClient.UserInfo.CuuThuList.Count)
			{
				Vector3 vector2 = default(Vector3);
				vector2 = new Vector3(0f, 320f, 0f);
				CuuThuItem cuuThuItem = CuuThuItemList[0];
				cuuThuItem.itemID = num2;
				CuuThuItem cuuThuItem2 = CuuThuItemList[CuuThuItemList.Count - 1];
				if (NGUITools.GetActive(cuuThuItem.focusItem))
				{
					cuuThuItem.focusItem.SetActive(false);
				}
				CuuThuItemList.RemoveAt(0);
				CuuThuItemList.Add(cuuThuItem);
				cuuThuItem.transform.localPosition = cuuThuItem2.transform.localPosition + itemOffset;
				cuuThuItem.SetCuuThuData(GameManager.instance.m_GameClient.UserInfo.CuuThuList[num2]);
				startItemGUI_Idx++;
			}
		}
	}

	public void addPrevItem()
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
		if (m_Tab == ScreenFriendsTab.TabHaoHuu)
		{
			if (GameManager.instance.m_GameClient.UserInfo.BanBeList != null && GameManager.instance.m_GameClient.UserInfo.BanBeList.Count > 0)
			{
				BanBeItem banBeItem = BanBeItemList[0];
				if (NGUITools.GetActive(banBeItem.focusItem))
				{
					banBeItem.focusItem.SetActive(false);
				}
				BanBeItem banBeItem2 = BanBeItemList[BanBeItemList.Count - 1];
				if (NGUITools.GetActive(banBeItem2.focusItem))
				{
					banBeItem2.focusItem.SetActive(false);
				}
				if (BanBeItemList.Count == 20)
				{
					banBeItem2.itemID = num;
					BanBeItemList.RemoveAt(BanBeItemList.Count - 1);
					BanBeItemList.Insert(0, banBeItem2);
					banBeItem2.transform.localPosition = banBeItem.transform.localPosition - itemOffset;
					banBeItem2.SetFriendData(GameManager.instance.m_GameClient.UserInfo.BanBeList[num]);
				}
				else if (BanBeItemList.Count < 20)
				{
					BanBeItem component = ((GameObject)Object.Instantiate(friendPerfab)).GetComponent<BanBeItem>();
					component.itemID = num;
					component.transform.parent = ItemRoot.transform;
					component.transform.localScale = new Vector3(1f, 1f, 1f);
					component.transform.localPosition = vector;
					vector += itemOffset;
					component.SetFriendData(GameManager.instance.m_GameClient.UserInfo.BanBeList[num]);
					UIEventListener.Get(component.gameObject).onClick = onClick_BanBeItem;
					UIEventListener.Get(component.focusItem.gameObject).onClick = onClick_BanBeItem;
					UIEventListener.Get(component.btnAccept.gameObject).onClick = acceptBtn_OnClick;
					UIEventListener.Get(component.btnDeny.gameObject).onClick = denyBtn_BanBeItem;
					UIEventListener.Get(component.btnTyThi.gameObject).onClick = tyThiBtn_BanBeItem;
					UIEventListener.Get(component.btnThongTin.gameObject).onClick = thongTinBtn_BanBeItem;
					UIEventListener.Get(component.btnGuiThu.gameObject).onClick = guiThuBtn_BanBeItem;
					UIEventListener.Get(component.btnXoa.gameObject).onClick = xoaBtn_BanBeItem;
					BanBeItemList.Insert(0, component);
				}
			}
		}
		else if (m_Tab == ScreenFriendsTab.TabCuuThu && GameManager.instance.m_GameClient.UserInfo.CuuThuList != null && GameManager.instance.m_GameClient.UserInfo.CuuThuList.Count > 0)
		{
			CuuThuItem cuuThuItem = CuuThuItemList[0];
			if (NGUITools.GetActive(cuuThuItem.focusItem))
			{
				cuuThuItem.focusItem.SetActive(false);
			}
			CuuThuItem cuuThuItem2 = CuuThuItemList[CuuThuItemList.Count - 1];
			cuuThuItem2.itemID = num;
			if (NGUITools.GetActive(cuuThuItem2.focusItem))
			{
				cuuThuItem2.focusItem.SetActive(false);
			}
			if (CuuThuItemList.Count == 20)
			{
				CuuThuItemList.RemoveAt(CuuThuItemList.Count - 1);
				CuuThuItemList.Insert(0, cuuThuItem2);
				cuuThuItem2.transform.localPosition = cuuThuItem.transform.localPosition - itemOffset;
				cuuThuItem2.SetCuuThuData(GameManager.instance.m_GameClient.UserInfo.CuuThuList[num]);
			}
			else if (CuuThuItemList.Count < 20)
			{
				CuuThuItem component2 = ((GameObject)Object.Instantiate(cuuThuPerfab)).GetComponent<CuuThuItem>();
				component2.itemID = num;
				component2.transform.parent = ItemRoot.transform;
				component2.transform.localScale = new Vector3(1f, 1f, 1f);
				component2.transform.localPosition = vector;
				vector += itemOffset;
				component2.SetCuuThuData(GameManager.instance.m_GameClient.UserInfo.CuuThuList[num]);
				UIEventListener.Get(component2.gameObject).onClick = onClick_CuuThuItem;
				UIEventListener.Get(component2.focusItem.gameObject).onClick = onClick_CuuThuItem;
				UIEventListener.Get(component2.btnThongTin.gameObject).onClick = thongTinBtn_CuuThuItem;
				UIEventListener.Get(component2.btnGuiThu.gameObject).onClick = guiThuBtn_CuuThuItem;
				UIEventListener.Get(component2.btnTraThu.gameObject).onClick = traThuBtn_CuuThuItem;
				CuuThuItemList.Add(component2);
			}
		}
	}

	private void onClick_HaoHuuTab(bool isActive)
	{
		if (isActive && m_Tab != ScreenFriendsTab.TabHaoHuu)
		{
			m_Tab = ScreenFriendsTab.TabHaoHuu;
			updateViewAuto();
			startItemGUI_Idx = 0;
			getListFriends(true);
		}
	}

	private void onClick_CuuThuTab(bool isActive)
	{
		if (isActive && m_Tab != ScreenFriendsTab.TabCuuThu)
		{
			m_Tab = ScreenFriendsTab.TabCuuThu;
			updateViewAuto();
			startItemGUI_Idx = 0;
			getListFriends(true);
		}
	}

	private void onClick_GiangHoTab(bool isActive)
	{
		if (isActive && m_Tab != ScreenFriendsTab.TabGiangHo)
		{
			m_Tab = ScreenFriendsTab.TabGiangHo;
			updateViewAuto();
		}
	}

	private void updateViewAuto()
	{
		if (m_Tab == ScreenFriendsTab.TabGiangHo)
		{
			ItemRoot.gameObject.SetActive(false);
			groupGiangHo.gameObject.SetActive(true);
			if (m_GiangHoTab == GiangHoAutoTab.AutoBatCoc)
			{
				AutoBatCocTab.gameObject.SetActive(true);
				AutoThachDauTab.gameObject.SetActive(false);
				int maxLuotBatCocByVip = ConfigManager.GetMaxLuotBatCocByVip(GameManager.instance.m_GameClient.UserInfo.Gamer.Vip);
				int luotBatCoc = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.LuotBatCoc;
				if (luotBatCoc < maxLuotBatCocByVip)
				{
					if (GUIManager.instance.isAutoBatCoc)
					{
						lbBatCocButton.text = Localization.instance.Get("StopBatCocTuDongLabel");
					}
					else
					{
						lbBatCocButton.text = Localization.instance.Get("AutoBatCocLabelButton");
					}
					lbMaxLuotBatCoc.gameObject.SetActive(false);
					groupBatCocNormal.gameObject.SetActive(true);
				}
				else
				{
					lbMaxLuotBatCoc.gameObject.SetActive(true);
					groupBatCocNormal.gameObject.SetActive(false);
				}
			}
			else if (m_GiangHoTab == GiangHoAutoTab.AutoThachDau)
			{
				AutoBatCocTab.gameObject.SetActive(false);
				AutoThachDauTab.gameObject.SetActive(true);
				AutoThachDauTab.getListThachDau();
			}
		}
		if (m_Tab == ScreenFriendsTab.TabCuuThu || m_Tab == ScreenFriendsTab.TabHaoHuu)
		{
			ItemRoot.gameObject.SetActive(true);
			groupGiangHo.gameObject.SetActive(false);
		}
	}

	public void onClickAutoBatCoc(bool isActive)
	{
		if (isActive && m_GiangHoTab != GiangHoAutoTab.AutoBatCoc)
		{
			m_GiangHoTab = GiangHoAutoTab.AutoBatCoc;
			updateViewAuto();
		}
	}

	public void onClickAutoThachDau(bool isActive)
	{
		if (isActive && m_GiangHoTab != GiangHoAutoTab.AutoThachDau)
		{
			m_GiangHoTab = GiangHoAutoTab.AutoThachDau;
			updateViewAuto();
		}
	}

	public void AutoBatCocBtn_OnClick(GameObject go)
	{
		GUIManager.instance.isAutoBatCoc = !GUIManager.instance.isAutoBatCoc;
		ScreenMain screenMain = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain;
		if (GUIManager.instance.isAutoBatCoc)
		{
			GadgetPanelBottom gadgetPanelBottom = GUIManager.instance.gadgetPanelBottom;
			if (gadgetPanelBottom != null)
			{
				gadgetPanelBottom.OnClickBtnHome();
			}
			if (screenMain != null)
			{
				screenMain.OnStartAutoBatCoc();
			}
			lbBatCocButton.text = Localization.instance.Get("StopBatCocTuDongLabel");
		}
		else
		{
			lbBatCocButton.text = Localization.instance.Get("AutoBatCocLabelButton");
			if (screenMain != null)
			{
				screenMain.endAutoBatCoc();
			}
		}
	}

	public void acceptBtn_OnClick(GameObject go)
	{
		BanBeItem component = go.transform.parent.parent.GetComponent<BanBeItem>();
		if (component != null && component.mFriend_Data != null)
		{
			OpBanBeRequest opBanBeRequest = new OpBanBeRequest();
			opBanBeRequest.FriendGID = component.mFriend_Data.GID;
			GameManager.instance.m_GameClient.RequestAcceptBanBe(opBanBeRequest);
		}
	}

	public void denyBtn_BanBeItem(GameObject go)
	{
		BanBeItem component = go.transform.parent.parent.GetComponent<BanBeItem>();
		if (component != null && component.mFriend_Data != null)
		{
			OpBanBeRequest opBanBeRequest = new OpBanBeRequest();
			opBanBeRequest.FriendGID = component.mFriend_Data.GID;
			GameManager.instance.m_GameClient.RequestDeleteBanBe(opBanBeRequest);
		}
	}

	public void tyThiBtn_BanBeItem(GameObject go)
	{
		BanBeItem component = go.transform.parent.parent.GetComponent<BanBeItem>();
		if (!(component != null) || component.mFriend_Data == null)
		{
			return;
		}
		if (component.mFriend_Data.Online)
		{
			int level = 1;
			if (GameManager.instance != null && GameManager.instance.m_GameClient != null && GameManager.instance.m_GameClient.UserInfo != null && GameManager.instance.m_GameClient.UserInfo.Gamer != null)
			{
				level = GameManager.instance.m_GameClient.UserInfo.Gamer.Level;
			}
			PopupThachDau.Create(component.mFriend_Data.GID, component.mFriend_Data.DisplayName, level);
		}
		else
		{
			GameManager.instance.m_GameClient.RequestThachDau(component.mFriend_Data.GID, 0);
		}
	}

	public void thongTinBtn_BanBeItem(GameObject go)
	{
		BanBeItem component = go.transform.parent.parent.GetComponent<BanBeItem>();
		if (component != null && component.mFriend_Data != null)
		{
			XemThongTinMonPhaiRequest xemThongTinMonPhaiRequest = new XemThongTinMonPhaiRequest();
			xemThongTinMonPhaiRequest.TargetGID = component.mFriend_Data.GID;
			GameManager.instance.m_GameClient.RequestXemThongTinMonPhai(xemThongTinMonPhaiRequest);
		}
	}

	public void thongTinBtn_CuuThuItem(GameObject go)
	{
		CuuThuItem component = go.transform.parent.parent.GetComponent<CuuThuItem>();
		if (component != null && component.mCuuThu_Data != null)
		{
			XemThongTinMonPhaiRequest xemThongTinMonPhaiRequest = new XemThongTinMonPhaiRequest();
			xemThongTinMonPhaiRequest.TargetGID = component.mCuuThu_Data.GID;
			GameManager.instance.m_GameClient.RequestXemThongTinMonPhai(xemThongTinMonPhaiRequest);
		}
	}

	public void guiThuBtn_CuuThuItem(GameObject go)
	{
		CuuThuItem component = go.transform.parent.parent.GetComponent<CuuThuItem>();
		if (component != null && component.mCuuThu_Data != null)
		{
			PopUpSendMail.Create(component.mCuuThu_Data.GID, component.mCuuThu_Data.DisplayName);
		}
	}

	public void traThuBtn_CuuThuItem(GameObject go)
	{
		CuuThuItem component = go.transform.parent.parent.GetComponent<CuuThuItem>();
		if (!(component != null) || component.mCuuThu_Data == null)
		{
			return;
		}
		if (component.mCuuThu_Data.Type == UserInfo.CuuThuData.CuuThuType.LUAN_KIEM)
		{
			GUIManager.setScreen(GAME_SCREEN.ScreenLuanKiem);
		}
		else if (component.mCuuThu_Data.Type == UserInfo.CuuThuData.CuuThuType.BAT_COC)
		{
			if (component.mCuuThu_Data.Online)
			{
				MessagePopup.Create(string.Format(Localization.instance.Get("CuuThuBatCocOnline"), component.mCuuThu_Data.DisplayName));
			}
			else
			{
				GameManager.instance.m_GameClient.RequestBatCoc(component.mCuuThu_Data.GID);
			}
		}
	}

	public void guiThuBtn_BanBeItem(GameObject go)
	{
		BanBeItem component = go.transform.parent.parent.GetComponent<BanBeItem>();
		if (component != null && component.mFriend_Data != null)
		{
			PopUpSendMail.Create(component.mFriend_Data.GID, component.mFriend_Data.DisplayName);
		}
	}

	public void xoaBtn_BanBeItem(GameObject go)
	{
		itemSelectedDelete = go.transform.parent.parent.GetComponent<BanBeItem>();
		if (itemSelectedDelete != null && itemSelectedDelete.mFriend_Data != null)
		{
			PopupYesNo.Create(Localization.instance.Get("XoaBanBeConfirmMess"), Localization.instance.Get("MessageResetLuotGHYes"), Localization.instance.Get("KhongBtnLabel"), OnYesDeleteFriendClick, null);
		}
	}

	public void OnYesDeleteFriendClick()
	{
		OpBanBeRequest opBanBeRequest = new OpBanBeRequest();
		opBanBeRequest.FriendGID = itemSelectedDelete.mFriend_Data.GID;
		GameManager.instance.m_GameClient.RequestDeleteBanBe(opBanBeRequest);
	}

	public void btnSearch_OnClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenSearchBanBe);
	}

	public int CompareCuuThu(UserInfo.CuuThuData cuuThu1, UserInfo.CuuThuData cuuThu2)
	{
		if (cuuThu1 == null || cuuThu2 == null)
		{
			return -1;
		}
		if (cuuThu1.Online && !cuuThu2.Online)
		{
			return -1;
		}
		if (!cuuThu1.Online && cuuThu2.Online)
		{
			return 1;
		}
		if ((!cuuThu1.Online && !cuuThu2.Online) || (cuuThu1.Online && cuuThu2.Online))
		{
			return cuuThu1.DisplayName.CompareTo(cuuThu2.DisplayName);
		}
		return 0;
	}

	public int GetScoreSortFriend(UserInfo.BanBeData data)
	{
		if (data.Status == UserInfo.BanBeData.BanBeStatus.REQUESTING)
		{
			return 10;
		}
		if (data.Status == UserInfo.BanBeData.BanBeStatus.CONFIRMED)
		{
			if (data.Online)
			{
				return 8;
			}
			return 7;
		}
		if (data.Status == UserInfo.BanBeData.BanBeStatus.UNCONFIRMED)
		{
			return 5;
		}
		return 1;
	}

	public int CompareFriends(UserInfo.BanBeData friend1, UserInfo.BanBeData friend2)
	{
		if (friend1 == null || friend2 == null)
		{
			return -1;
		}
		if (GetScoreSortFriend(friend1) > GetScoreSortFriend(friend2))
		{
			return -1;
		}
		if (GetScoreSortFriend(friend1) < GetScoreSortFriend(friend2))
		{
			return 1;
		}
		return friend1.DisplayName.CompareTo(friend2.DisplayName);
	}
}

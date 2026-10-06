using System.Collections.Generic;
using UnityEngine;

public class ScreenDoiThuong : ScreenBase
{
	private enum ScreenDoiThuongTab
	{
		TabDoiThuong = 0,
		TabBXH = 1
	}

	public GameObject DoiThuongPrefab;

	public GameObject BXHPrefab;

	public GameObject ItemRoot;

	public GameObject titleBXHGroup;

	public GameObject titleDoiThuongGroup;

	public UILabel lbDiemHienCo;

	public UILabel lbTongDiem;

	private ScreenDoiThuongTab m_Tab;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<DoiDiemThuongItem> DoiThuongItemList = new List<DoiDiemThuongItem>();

	private List<BangXepHangItem> BXHItemList = new List<BangXepHangItem>();

	private int startItemGUI_Idx;

	public List<int> ListDoiThuongCount;

	private int currentSelectedIndex = -1;

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
		if (m_Tab == ScreenDoiThuongTab.TabBXH)
		{
			onClick_BXHTab(true);
		}
		else if (m_Tab == ScreenDoiThuongTab.TabDoiThuong)
		{
			onClick_DoiThuongTab(true);
		}
	}

	public void setListDoiThuongCount(List<int> listCount)
	{
		if (listCount != null)
		{
			ListDoiThuongCount = listCount;
		}
	}

	public void SyncWithNetworkData(GetTopULinhResponse response = null)
	{
		UserInfo.ServerData.ULinhSonTrangCfg uLinhCfg = GameManager.instance.m_GameClient.UserInfo.ServerInfo.ULinhCfg;
		lbDiemHienCo.text = Localization.instance.Get("DiemHienTaiLabel") + ": " + GameManager.instance.m_GameClient.UserInfo.Gamer.ULinhCurrentDiem;
		lbTongDiem.text = Localization.instance.Get("TongDiemLabel") + ": " + GameManager.instance.m_GameClient.UserInfo.Gamer.ULinhTotalDiem;
		if (uLinhCfg == null)
		{
			return;
		}
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		DoiThuongItemList.Clear();
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 215f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -160f, 0f);
		if (m_Tab == ScreenDoiThuongTab.TabDoiThuong)
		{
			EGDebug.Log("uLinhConfig.ListDoiThuong: " + uLinhCfg.ListDoiThuong.Count);
			EGDebug.Log("ListDoiThuongCount: " + ListDoiThuongCount.Count);
			if (uLinhCfg.ListDoiThuong != null && uLinhCfg.ListDoiThuong.Count > 0 && ListDoiThuongCount != null)
			{
				for (int i = 0; i < uLinhCfg.ListDoiThuong.Count; i++)
				{
					UserInfo.ServerData.ULinhSonTrangCfg.DoiThuongItem data = uLinhCfg.ListDoiThuong[i];
					int count = 0;
					if (i < ListDoiThuongCount.Count)
					{
						count = ListDoiThuongCount[i];
					}
					DoiDiemThuongItem component = ((GameObject)Object.Instantiate(DoiThuongPrefab)).GetComponent<DoiDiemThuongItem>();
					component.transform.parent = ItemRoot.transform;
					component.transform.localScale = new Vector3(1f, 1f, 1f);
					component.transform.localPosition = vector;
					vector += vector2;
					UIEventListener.Get(component.btnDoi.gameObject).onClick = btnDoi_OnClick;
					component.setData(data, count, i);
					DoiThuongItemList.Add(component);
				}
			}
		}
		else if (m_Tab == ScreenDoiThuongTab.TabBXH && uLinhCfg.ListPhanThuongTop != null && uLinhCfg.ListPhanThuongTop.Count > 0 && uLinhCfg.SoLuongTop > 0 && response != null)
		{
			for (int j = 0; j < uLinhCfg.SoLuongTop; j++)
			{
				UserInfo.ServerData.ULinhSonTrangCfg.TopItem data2 = ((j >= uLinhCfg.ListPhanThuongTop.Count) ? uLinhCfg.ListPhanThuongTop[uLinhCfg.ListPhanThuongTop.Count - 1] : uLinhCfg.ListPhanThuongTop[j]);
				GetTopULinhResponse.TopULinhData topULinhData;
				if (j < response.ListTop.Count)
				{
					topULinhData = response.ListTop[j];
				}
				else
				{
					topULinhData = new GetTopULinhResponse.TopULinhData();
					topULinhData.DisplayName = string.Empty;
					topULinhData.Diem = 0;
					topULinhData.GID = 0;
				}
				BangXepHangItem component2 = ((GameObject)Object.Instantiate(BXHPrefab)).GetComponent<BangXepHangItem>();
				component2.transform.parent = ItemRoot.transform;
				component2.transform.localScale = new Vector3(1f, 1f, 1f);
				component2.transform.localPosition = vector;
				vector += vector2;
				component2.setData(data2, topULinhData, j);
				BXHItemList.Add(component2);
			}
		}
		UIDraggablePanel component3 = ItemRoot.GetComponent<UIDraggablePanel>();
		component3.ResetPosition();
	}

	public void btnDoi_OnClick(GameObject go)
	{
		currentSelectedIndex = -1;
		DoiDiemThuongItem component = go.transform.parent.GetComponent<DoiDiemThuongItem>();
		if (component != null && component.m_Data != null)
		{
			currentSelectedIndex = component.mIndex;
			if (component.m_Data.Diem >= 1000)
			{
				PopupYesNo.Create(Localization.instance.Get("DoiThuongULinhConfirmMess"), Localization.instance.Get("Confirm"), Localization.instance.Get("Cancel"), callRequestDoiThuong, null);
			}
			else
			{
				callRequestDoiThuong();
			}
		}
	}

	public void callRequestDoiThuong()
	{
		if (currentSelectedIndex > -1)
		{
			DoiThuongULinhRequest doiThuongULinhRequest = new DoiThuongULinhRequest();
			doiThuongULinhRequest.SlotIdx = currentSelectedIndex;
			GameManager.instance.m_GameClient.RequestDoiThuongULinh(doiThuongULinhRequest);
		}
	}

	public void updateInfoList(DoiThuongULinhResponse response)
	{
		ListDoiThuongCount.Clear();
		ListDoiThuongCount = response.UpdateULinhInfo.ListDoiThuongCount;
		SyncWithNetworkData();
		if (response.PhanThuongResponse != null)
		{
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), response.PhanThuongResponse);
		}
	}

	public void onClick_DoiThuongTab(bool isActive)
	{
		if (isActive)
		{
			startItemGUI_Idx = 0;
			m_Tab = ScreenDoiThuongTab.TabDoiThuong;
			titleDoiThuongGroup.gameObject.SetActive(true);
			titleBXHGroup.gameObject.SetActive(false);
			SyncWithNetworkData();
		}
	}

	public void onClick_BXHTab(bool isActive)
	{
		if (isActive)
		{
			startItemGUI_Idx = 0;
			m_Tab = ScreenDoiThuongTab.TabBXH;
			titleDoiThuongGroup.gameObject.SetActive(false);
			titleBXHGroup.gameObject.SetActive(true);
			GameManager.instance.m_GameClient.RequestGetTopULinh();
			SyncWithNetworkData();
		}
	}

	public void btnBack_onClick()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenULinhSonTrang);
	}
}

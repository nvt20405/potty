using System.Collections.Generic;
using UnityEngine;

public class ScreenTonHieu : ScreenBase
{
	public List<TonHieuInfo> listTonHieu = new List<TonHieuInfo>();

	public List<TonHieuItem> ItemList = new List<TonHieuItem>();

	public List<TonHieuData> listDetailLevel = new List<TonHieuData>();

	public List<TonHieuData> listDetailHanhTau = new List<TonHieuData>();

	public List<TonHieuData> listDetailChienTruong = new List<TonHieuData>();

	public List<TonHieuData> listDetailTinhLuyen = new List<TonHieuData>();

	public List<TonHieuData> listDetailCongLuc = new List<TonHieuData>();

	public List<TonHieuData> listDetailLuanKiem = new List<TonHieuData>();

	public List<TonHieuData> listDetailHoangKim = new List<TonHieuData>();

	public List<TonHieuData> listDetailQMD = new List<TonHieuData>();

	public List<TonHieuData> listDetailThanThu = new List<TonHieuData>();

	public List<TonHieuData> listDetailDHVL = new List<TonHieuData>();

	public List<TonHieuData> listDetailThienMa = new List<TonHieuData>();

	public List<TonHieuData> listDetailTBHoangKim = new List<TonHieuData>();

	public GameObject ItemRoot;

	public GameObject ItemDetailRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	public GameObject tonHieuPerfab;

	public UILabel lbInfoTonHieuSelected;

	public List<TonHieuDetailItem> listTOPItem;

	private TonHieuItem typeSelected;

	private void Start()
	{
		dragPanel = ItemRoot.GetComponent<UIDraggablePanel>();
		panel = ItemRoot.GetComponent<UIPanel>();
		getListTonHieu();
		listDetailLevel = getListDataLevel();
		listDetailHanhTau = getListHanhTau();
		listDetailChienTruong = getListChienTruong();
		listDetailTinhLuyen = getListTinhLuyen();
		listDetailCongLuc = getListCongLuc();
		listDetailLuanKiem = getListLuanKiem();
		listDetailHoangKim = getListHoangKim();
		listDetailQMD = getListQMD();
		listDetailThanThu = getListThanThu();
		listDetailDHVL = getListDHVL();
		listDetailThienMa = getListThienMa();
		listDetailTBHoangKim = getListTBHoangKim();
	}

	public override void OnActive()
	{
		base.OnActive();
		getListPhanThuong();
	}

	private List<TonHieuInfo> getListTonHieu()
	{
		List<TonHieuInfo> list = new List<TonHieuInfo>();
		TonHieuInfo tonHieuInfo = new TonHieuInfo();
		tonHieuInfo.TonHieuType = UserInfo.GamerData.TonHieuType.LEVEL;
		tonHieuInfo.DisplayName = Localization.instance.Get("TonHieuLevelTitle");
		tonHieuInfo.Description = Localization.instance.Get("TonHieuLevelDescription");
		list.Add(tonHieuInfo);
		TonHieuInfo tonHieuInfo2 = new TonHieuInfo();
		tonHieuInfo2.TonHieuType = UserInfo.GamerData.TonHieuType.HANH_TAU;
		tonHieuInfo2.DisplayName = Localization.instance.Get("TonHieuHanhTauTitle");
		tonHieuInfo2.Description = Localization.instance.Get("TonHieuHanhTauDescription");
		list.Add(tonHieuInfo2);
		TonHieuInfo tonHieuInfo3 = new TonHieuInfo();
		tonHieuInfo3.TonHieuType = UserInfo.GamerData.TonHieuType.CHIEN_TRUONG;
		tonHieuInfo3.DisplayName = Localization.instance.Get("TonHieuChienTruongTitle");
		tonHieuInfo3.Description = Localization.instance.Get("TonHieuChienTruongDescription");
		list.Add(tonHieuInfo3);
		TonHieuInfo tonHieuInfo4 = new TonHieuInfo();
		tonHieuInfo4.TonHieuType = UserInfo.GamerData.TonHieuType.TINH_LUYEN;
		tonHieuInfo4.DisplayName = Localization.instance.Get("TonHieuTinhLuyenTitle");
		tonHieuInfo4.Description = Localization.instance.Get("TonHieuTinhLuyenDescription");
		list.Add(tonHieuInfo4);
		TonHieuInfo tonHieuInfo5 = new TonHieuInfo();
		tonHieuInfo5.TonHieuType = UserInfo.GamerData.TonHieuType.CONG_LUC;
		tonHieuInfo5.DisplayName = Localization.instance.Get("TonHieuCongLucTitle");
		tonHieuInfo5.Description = Localization.instance.Get("TonHieuCongLucDescription");
		list.Add(tonHieuInfo5);
		TonHieuInfo tonHieuInfo6 = new TonHieuInfo();
		tonHieuInfo6.TonHieuType = UserInfo.GamerData.TonHieuType.LUAN_KIEM;
		tonHieuInfo6.DisplayName = Localization.instance.Get("TonHieuLuanKiemTitle");
		tonHieuInfo6.Description = Localization.instance.Get("TonHieuLuanKiemDescription");
		list.Add(tonHieuInfo6);
		TonHieuInfo tonHieuInfo7 = new TonHieuInfo();
		tonHieuInfo7.TonHieuType = UserInfo.GamerData.TonHieuType.HOANG_KIM;
		tonHieuInfo7.DisplayName = Localization.instance.Get("TonHieuHoangKimTitle");
		tonHieuInfo7.Description = Localization.instance.Get("TonHieuHoangKimDescription");
		list.Add(tonHieuInfo7);
		TonHieuInfo tonHieuInfo8 = new TonHieuInfo();
		tonHieuInfo8.TonHieuType = UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH;
		tonHieuInfo8.DisplayName = Localization.instance.Get("TonHieuQMDTitle");
		tonHieuInfo8.Description = Localization.instance.Get("TonHieuQMDDescription");
		list.Add(tonHieuInfo8);
		TonHieuInfo tonHieuInfo9 = new TonHieuInfo();
		tonHieuInfo9.TonHieuType = UserInfo.GamerData.TonHieuType.THAN_THU;
		tonHieuInfo9.DisplayName = Localization.instance.Get("TonHieuThanThuTitle");
		tonHieuInfo9.Description = Localization.instance.Get("TonHieuThanThuDescription");
		list.Add(tonHieuInfo9);
		TonHieuInfo tonHieuInfo10 = new TonHieuInfo();
		tonHieuInfo10.TonHieuType = UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM;
		tonHieuInfo10.DisplayName = Localization.instance.Get("TonHieuDHVLTitle");
		tonHieuInfo10.Description = Localization.instance.Get("TonHieuDHVLDescription");
		list.Add(tonHieuInfo10);
		TonHieuInfo tonHieuInfo11 = new TonHieuInfo();
		tonHieuInfo11.TonHieuType = UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG;
		tonHieuInfo11.DisplayName = Localization.instance.Get("TonHieuThienMaTitle");
		tonHieuInfo11.Description = Localization.instance.Get("TonHieuThienMaDescription");
		list.Add(tonHieuInfo11);
		TonHieuInfo tonHieuInfo12 = new TonHieuInfo();
		tonHieuInfo12.TonHieuType = UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM;
		tonHieuInfo12.DisplayName = Localization.instance.Get("TonHieuTBHoangKimTitle");
		tonHieuInfo12.Description = Localization.instance.Get("TonHieuTBHoangKimDescription");
		list.Add(tonHieuInfo12);
		return list;
	}

	private List<TonHieuData> getListDataLevel()
	{
		List<TonHieuData> list = new List<TonHieuData>();
		for (int i = 1; i < 6; i++)
		{
			TonHieuData tonHieuData = new TonHieuData();
			tonHieuData.TOP = i;
			tonHieuData.Name = Localization.instance.Get("TonHieu_Level_TOP" + i);
			tonHieuData.Details = Localization.instance.Get("TonHieu_Level_TOP" + i + "_Detail");
			list.Add(tonHieuData);
		}
		return list;
	}

	private List<TonHieuData> getListHanhTau()
	{
		List<TonHieuData> list = new List<TonHieuData>();
		for (int i = 1; i < 6; i++)
		{
			TonHieuData tonHieuData = new TonHieuData();
			tonHieuData.TOP = i;
			tonHieuData.Name = Localization.instance.Get("TonHieu_HanhTau_TOP" + i);
			tonHieuData.Details = Localization.instance.Get("TonHieu_HanhTau_TOP" + i + "_Detail");
			list.Add(tonHieuData);
		}
		return list;
	}

	private List<TonHieuData> getListChienTruong()
	{
		List<TonHieuData> list = new List<TonHieuData>();
		for (int i = 1; i < 4; i++)
		{
			TonHieuData tonHieuData = new TonHieuData();
			tonHieuData.TOP = i;
			tonHieuData.Name = Localization.instance.Get("TonHieu_ChienTruong_TOP" + i);
			tonHieuData.Details = Localization.instance.Get("TonHieu_ChienTruong_TOP" + i + "_Detail");
			list.Add(tonHieuData);
		}
		return list;
	}

	private List<TonHieuData> getListTinhLuyen()
	{
		List<TonHieuData> list = new List<TonHieuData>();
		for (int i = 1; i < 4; i++)
		{
			TonHieuData tonHieuData = new TonHieuData();
			tonHieuData.TOP = i;
			tonHieuData.Name = Localization.instance.Get("TonHieu_TinhLuyen_TOP" + i);
			tonHieuData.Details = Localization.instance.Get("TonHieu_TinhLuyen_TOP" + i + "_Detail");
			list.Add(tonHieuData);
		}
		return list;
	}

	private List<TonHieuData> getListLuanKiem()
	{
		List<TonHieuData> list = new List<TonHieuData>();
		for (int i = 1; i < 4; i++)
		{
			TonHieuData tonHieuData = new TonHieuData();
			tonHieuData.TOP = i;
			tonHieuData.Name = Localization.instance.Get("TonHieu_LuanKiem_TOP" + i);
			tonHieuData.Details = Localization.instance.Get("TonHieu_LuanKiem_TOP" + i + "_Detail");
			list.Add(tonHieuData);
		}
		return list;
	}

	private List<TonHieuData> getListHoangKim()
	{
		List<TonHieuData> list = new List<TonHieuData>();
		for (int i = 1; i < 4; i++)
		{
			TonHieuData tonHieuData = new TonHieuData();
			tonHieuData.TOP = i;
			tonHieuData.Name = Localization.instance.Get("TonHieu_HoangKim_TOP" + i);
			tonHieuData.Details = Localization.instance.Get("TonHieu_HoangKim_TOP" + i + "_Detail");
			list.Add(tonHieuData);
		}
		return list;
	}

	private List<TonHieuData> getListCongLuc()
	{
		List<TonHieuData> list = new List<TonHieuData>();
		for (int i = 1; i < 4; i++)
		{
			TonHieuData tonHieuData = new TonHieuData();
			tonHieuData.TOP = i;
			tonHieuData.Name = Localization.instance.Get("TonHieu_CongLuc_TOP" + i);
			tonHieuData.Details = Localization.instance.Get("TonHieu_CongLuc_TOP" + i + "_Detail");
			list.Add(tonHieuData);
		}
		return list;
	}

	private List<TonHieuData> getListThanThu()
	{
		List<TonHieuData> list = new List<TonHieuData>();
		for (int i = 1; i < 4; i++)
		{
			TonHieuData tonHieuData = new TonHieuData();
			tonHieuData.TOP = i;
			tonHieuData.Name = Localization.instance.Get("TonHieu_ThanThu_TOP" + i);
			tonHieuData.Details = Localization.instance.Get("TonHieu_ThanThu_TOP" + i + "_Detail");
			list.Add(tonHieuData);
		}
		return list;
	}

	private List<TonHieuData> getListQMD()
	{
		List<TonHieuData> list = new List<TonHieuData>();
		for (int i = 1; i < 4; i++)
		{
			TonHieuData tonHieuData = new TonHieuData();
			tonHieuData.TOP = i;
			tonHieuData.Name = Localization.instance.Get("TonHieu_QMD_TOP" + i);
			tonHieuData.Details = Localization.instance.Get("TonHieu_QMD_TOP" + i + "_Detail");
			list.Add(tonHieuData);
		}
		return list;
	}

	private List<TonHieuData> getListDHVL()
	{
		List<TonHieuData> list = new List<TonHieuData>();
		for (int i = 1; i < 4; i++)
		{
			TonHieuData tonHieuData = new TonHieuData();
			tonHieuData.TOP = i;
			tonHieuData.Name = Localization.instance.Get("TonHieu_DHVL_TOP" + i);
			tonHieuData.Details = Localization.instance.Get("TonHieu_DHVL_TOP" + i + "_Detail");
			list.Add(tonHieuData);
		}
		return list;
	}

	private List<TonHieuData> getListThienMa()
	{
		List<TonHieuData> list = new List<TonHieuData>();
		for (int i = 1; i < 4; i++)
		{
			TonHieuData tonHieuData = new TonHieuData();
			tonHieuData.TOP = i;
			tonHieuData.Name = Localization.instance.Get("TonHieu_ThienMa_TOP" + i);
			tonHieuData.Details = Localization.instance.Get("TonHieu_ThienMa_TOP" + i + "_Detail");
			list.Add(tonHieuData);
		}
		return list;
	}

	private List<TonHieuData> getListTBHoangKim()
	{
		List<TonHieuData> list = new List<TonHieuData>();
		for (int i = 1; i < 4; i++)
		{
			TonHieuData tonHieuData = new TonHieuData();
			tonHieuData.TOP = i;
			tonHieuData.Name = Localization.instance.Get("TonHieu_TBHoangKim_TOP" + i);
			tonHieuData.Details = Localization.instance.Get("TonHieu_TBHoangKim_TOP" + i + "_Detail");
			list.Add(tonHieuData);
		}
		return list;
	}

	public void getListPhanThuong()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		if (listTonHieu == null || (listTonHieu != null && listTonHieu.Count == 0))
		{
			listTonHieu = getListTonHieu();
		}
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 315f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -70f, 0f);
		if (listTonHieu != null && listTonHieu.Count > 0)
		{
			for (int i = 0; i < listTonHieu.Count; i++)
			{
				TonHieuItem component = ((GameObject)Object.Instantiate(tonHieuPerfab)).GetComponent<TonHieuItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = vector;
				if (i == 0)
				{
					component.setData(listTonHieu[i], true);
				}
				else
				{
					component.setData(listTonHieu[i]);
				}
				vector += vector2;
				UIEventListener.Get(component.gameObject).onClick = OnClickItem;
				ItemList.Add(component);
			}
		}
		typeSelected = ItemList[0];
		loadTOPData();
	}

	public void OnClickItem(GameObject go)
	{
		if (typeSelected != null)
		{
			typeSelected.setSelected(false);
		}
		TonHieuItem component = go.GetComponent<TonHieuItem>();
		if (component != null && component.curData != null)
		{
			typeSelected = component;
			lbInfoTonHieuSelected.text = component.curData.Description;
			component.setSelected(true);
			loadTOPData();
		}
	}

	public void loadTOPData()
	{
		lbInfoTonHieuSelected.text = typeSelected.curData.Description;
		switch (typeSelected.curData.TonHieuType)
		{
		case UserInfo.GamerData.TonHieuType.LEVEL:
			if (listDetailLevel.Count <= 0)
			{
				listDetailLevel = getListDataLevel();
			}
			initList(listDetailLevel);
			break;
		case UserInfo.GamerData.TonHieuType.HANH_TAU:
			if (listDetailHanhTau.Count <= 0)
			{
				listDetailHanhTau = getListHanhTau();
			}
			initList(listDetailHanhTau);
			break;
		case UserInfo.GamerData.TonHieuType.CHIEN_TRUONG:
			if (listDetailChienTruong.Count <= 0)
			{
				listDetailChienTruong = getListChienTruong();
			}
			initList(listDetailChienTruong);
			break;
		case UserInfo.GamerData.TonHieuType.TINH_LUYEN:
			if (listDetailTinhLuyen.Count <= 0)
			{
				listDetailTinhLuyen = getListTinhLuyen();
			}
			initList(listDetailTinhLuyen);
			break;
		case UserInfo.GamerData.TonHieuType.CONG_LUC:
			if (listDetailCongLuc.Count <= 0)
			{
				listDetailCongLuc = getListCongLuc();
			}
			initList(listDetailCongLuc);
			break;
		case UserInfo.GamerData.TonHieuType.LUAN_KIEM:
			if (listDetailLuanKiem.Count <= 0)
			{
				listDetailLuanKiem = getListLuanKiem();
			}
			initList(listDetailLuanKiem);
			break;
		case UserInfo.GamerData.TonHieuType.HOANG_KIM:
			if (listDetailHoangKim.Count <= 0)
			{
				listDetailHoangKim = getListHoangKim();
			}
			initList(listDetailHoangKim);
			break;
		case UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH:
			if (listDetailQMD.Count <= 0)
			{
				listDetailQMD = getListQMD();
			}
			initList(listDetailQMD);
			break;
		case UserInfo.GamerData.TonHieuType.THAN_THU:
			if (listDetailThanThu.Count <= 0)
			{
				listDetailThanThu = getListThanThu();
			}
			initList(listDetailThanThu);
			break;
		case UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM:
			if (listDetailDHVL.Count <= 0)
			{
				listDetailDHVL = getListDHVL();
			}
			initList(listDetailDHVL);
			break;
		case UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG:
			if (listDetailThienMa.Count <= 0)
			{
				listDetailThienMa = getListThienMa();
			}
			initList(listDetailThienMa);
			break;
		case UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM:
			if (listDetailTBHoangKim.Count <= 0)
			{
				listDetailTBHoangKim = getListTBHoangKim();
			}
			initList(listDetailTBHoangKim);
			break;
		}
	}

	private void initList(List<TonHieuData> listData)
	{
		if (listData != null && listData.Count > 0)
		{
			for (int i = 0; i < listTOPItem.Count; i++)
			{
				if (i < listData.Count && listData[i] != null)
				{
					listTOPItem[i].gameObject.SetActive(true);
					listTOPItem[i].setData(listData[i], typeSelected.curData.TonHieuType);
				}
				else
				{
					listTOPItem[i].gameObject.SetActive(false);
				}
			}
		}
		UIDraggablePanel component = ItemDetailRoot.GetComponent<UIDraggablePanel>();
		component.ResetPosition();
	}

	public void updateView()
	{
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.CurTonHieu == typeSelected.curData.TonHieuType)
		{
			loadTOPData();
		}
	}

	public void btnBack_OnClick()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDoiHinh);
		ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
		screenDoiHinh.TurnOnNhanVatGroup();
	}
}

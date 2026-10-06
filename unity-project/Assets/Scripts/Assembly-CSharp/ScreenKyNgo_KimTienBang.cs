using System;
using System.Collections.Generic;
using UnityEngine;

public class ScreenKyNgo_KimTienBang : ScreenBase
{
	public GameObject phanThuongPerfab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<KimTienBangItem> ItemList = new List<KimTienBangItem>();

	private Vector3 itemPos;

	private Vector3 itemOffset;

	public UILabel lbTimeDienRaSuKien;

	public UILabel lbDaTieu;

	private int currentIdxSelected = -1;

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
		displayInfo();
	}

	private void updateTime()
	{
		UserInfo.ServerData.TichLuyTieuCfg tichLuyTieuConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyTieuConfig;
		if (tichLuyTieuConfig != null)
		{
			DateTime thoiGianBatDau = tichLuyTieuConfig.ThoiGianBatDau;
			DateTime thoiGianKetThuc = tichLuyTieuConfig.ThoiGianKetThuc;
			DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
			if (serverTime <= thoiGianKetThuc && serverTime >= thoiGianBatDau)
			{
				string arg = thoiGianBatDau.Day + "/" + thoiGianBatDau.Month;
				string arg2 = thoiGianKetThuc.Day + "/" + thoiGianKetThuc.Month + "/" + thoiGianKetThuc.Year;
				lbTimeDienRaSuKien.text = string.Format(Localization.instance.Get("TimeDienRaSuKienLabel"), arg, arg2);
			}
		}
	}

	public void displayInfo()
	{
		lbDaTieu.text = string.Format(Localization.instance.Get("InfoDaTieuTien"), GameManager.instance.m_GameClient.UserInfo.Gamer.DiemTichLuyTieu);
		updateTime();
		getListMocThuongTieu();
	}

	public void getListMocThuongTieu()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		itemPos = new Vector3(-145f, 50f, -1f);
		itemOffset = new Vector3(200f, 0f, 0f);
		UserInfo.ServerData.TichLuyTieuCfg tichLuyTieuConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyTieuConfig;
		if (tichLuyTieuConfig == null || tichLuyTieuConfig.ListMocTieu == null || tichLuyTieuConfig.ListMocTieu.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < tichLuyTieuConfig.ListMocTieu.Count; i++)
		{
			UserInfo.ServerData.MocTichLuyTieu mocTichLuyTieu = tichLuyTieuConfig.ListMocTieu[i];
			if (mocTichLuyTieu != null)
			{
				KimTienBangItem component = ((GameObject)UnityEngine.Object.Instantiate(phanThuongPerfab)).GetComponent<KimTienBangItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(mocTichLuyTieu, i, GameManager.instance.m_GameClient.UserInfo.Gamer.DiemTichLuyTieu);
				itemPos += itemOffset;
				UIEventListener.Get(component.gameObject).onClick = onClick_KimTienBangItem;
				UIEventListener.Get(component.btnDoi.gameObject).onClick = onClick_BtnDoi;
				ItemList.Add(component);
			}
		}
	}

	public void onClick_BtnDoi(GameObject go)
	{
		currentIdxSelected = -1;
		KimTienBangItem component = go.transform.parent.GetComponent<KimTienBangItem>();
		if (component != null && component.mThuongTieuData != null && component.IdxMocThuongTieu >= 0)
		{
			currentIdxSelected = component.IdxMocThuongTieu;
			if (CommonHelper.CheckFlag(GameManager.instance.m_GameClient.UserInfo.Gamer.TichLuyTieuFlag, currentIdxSelected))
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoDaNhanPhanThuong"));
			}
			else if (component.mThuongTieuData.GiaTri <= GameManager.instance.m_GameClient.UserInfo.Gamer.DiemTichLuyTieu)
			{
				PopupYesNo.Create(Localization.instance.Get("NhanThuongTichLuyConfirmMsg"), Localization.instance.Get("DongYLabelBtn"), Localization.instance.Get("TuChoiLabelBtn"), OnNhanThuongNow, null);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoChuaDuDiemDoi"));
			}
		}
	}

	public void onClick_KimTienBangItem(GameObject go)
	{
		KimTienBangItem component = go.transform.GetComponent<KimTienBangItem>();
		if (component != null && component.mThuongTieuData != null)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = component.mThuongTieuData.ListPhanThuong;
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), phanThuongResponse);
		}
	}

	public void OnNhanThuongNow()
	{
		NhanThuongTichLuyTieuRequest nhanThuongTichLuyTieuRequest = new NhanThuongTichLuyTieuRequest();
		nhanThuongTichLuyTieuRequest.Idx = currentIdxSelected;
		GameManager.instance.m_GameClient.RequestNhanThuongTichLuyTieu(nhanThuongTichLuyTieuRequest);
	}

	public void updateMainMenuView()
	{
		GadgetPanelBottom gadgetPanelBottom = GUIManager.instance.gadgetPanelBottom;
		if (!(gadgetPanelBottom != null))
		{
			return;
		}
		gadgetPanelBottom.checkDisplayThongBaoSuKien();
		if (gadgetPanelBottom.listKyNgoMenu == null || gadgetPanelBottom.listKyNgoMenu.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < gadgetPanelBottom.listKyNgoMenu.Count; i++)
		{
			if (gadgetPanelBottom.listKyNgoMenu[i].kyNgoID == MenuButtonKyNgo.KyNgoType.TichLuyTieu)
			{
				if (gadgetPanelBottom.checkThongBaoKimTienBang())
				{
					gadgetPanelBottom.listKyNgoMenu[i].notifyIcon.gameObject.SetActive(true);
				}
				else
				{
					gadgetPanelBottom.listKyNgoMenu[i].notifyIcon.gameObject.SetActive(false);
				}
			}
		}
	}
}

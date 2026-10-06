using System;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTichNapHangNgay : ScreenBase
{
	public GameObject phanThuongPerfab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<TichNapHangNgayItem> ItemList = new List<TichNapHangNgayItem>();

	private Vector3 itemPos;

	private Vector3 itemOffset;

	public UILabel lbTimeDienRaSuKien;

	public UILabel lbDaNapHomNay;

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
		UserInfo.ServerData.EventTichNapHangNgayCfg tichLuyNapHangNgayConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyNapHangNgayConfig;
		if (tichLuyNapHangNgayConfig != null)
		{
			DateTime thoiGianBatDau = tichLuyNapHangNgayConfig.ThoiGianBatDau;
			DateTime thoiGianKetThuc = tichLuyNapHangNgayConfig.ThoiGianKetThuc;
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
		lbDaNapHomNay.text = string.Format(Localization.instance.Get("DaNapHomNay"), GameManager.instance.m_GameClient.UserInfo.Gamer.NapHangNgay);
		updateTime();
		getListMocThuongNap();
	}

	public void getListMocThuongNap()
	{
		int napHangNgay = GameManager.instance.m_GameClient.UserInfo.Gamer.NapHangNgay;
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		itemPos = new Vector3(-145f, 50f, -1f);
		itemOffset = new Vector3(200f, 0f, 0f);
		UserInfo.ServerData.EventTichNapHangNgayCfg tichLuyNapHangNgayConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyNapHangNgayConfig;
		if (tichLuyNapHangNgayConfig == null || tichLuyNapHangNgayConfig.ListMocNap == null || tichLuyNapHangNgayConfig.ListMocNap.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < tichLuyNapHangNgayConfig.ListMocNap.Count; i++)
		{
			UserInfo.ServerData.MocTichLuyNapHangNgay mocTichLuyNapHangNgay = tichLuyNapHangNgayConfig.ListMocNap[i];
			if (mocTichLuyNapHangNgay != null)
			{
				TichNapHangNgayItem component = ((GameObject)UnityEngine.Object.Instantiate(phanThuongPerfab)).GetComponent<TichNapHangNgayItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(mocTichLuyNapHangNgay, i, napHangNgay);
				itemPos += itemOffset;
				UIEventListener.Get(component.gameObject).onClick = onClick_TichNapHangNgayItem;
				UIEventListener.Get(component.btnDoi.gameObject).onClick = onClick_BtnDoi;
				ItemList.Add(component);
			}
		}
	}

	public void onClick_BtnDoi(GameObject go)
	{
		currentIdxSelected = -1;
		TichNapHangNgayItem component = go.transform.parent.GetComponent<TichNapHangNgayItem>();
		if (component != null && component.mThuongNapData != null && component.IdxMocThuongNap >= 0)
		{
			currentIdxSelected = component.IdxMocThuongNap;
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(string.Format("TichNapHangNgay{0};", currentIdxSelected)))
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoDaNhanPhanThuong"));
			}
			else if (component.mThuongNapData.GiaTri <= GameManager.instance.m_GameClient.UserInfo.Gamer.NapHangNgay)
			{
				PopupYesNo.Create(Localization.instance.Get("NhanThuongTichLuyConfirmMsg"), Localization.instance.Get("DongYLabelBtn"), Localization.instance.Get("TuChoiLabelBtn"), OnNhanThuongNow, null);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoChuaDuDiemDoi"));
			}
		}
	}

	public void onClick_TichNapHangNgayItem(GameObject go)
	{
		TichNapHangNgayItem component = go.transform.GetComponent<TichNapHangNgayItem>();
		if (component != null && component.mThuongNapData != null)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = component.mThuongNapData.ListPhanThuong;
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), phanThuongResponse);
		}
	}

	public void OnNhanThuongNow()
	{
		NhanThuongTichNapHangNgayRequest nhanThuongTichNapHangNgayRequest = new NhanThuongTichNapHangNgayRequest();
		nhanThuongTichNapHangNgayRequest.Idx = currentIdxSelected;
		GameManager.instance.m_GameClient.RequestNhanThuongNapHangNgay(nhanThuongTichNapHangNgayRequest);
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
			if (gadgetPanelBottom.listKyNgoMenu[i].kyNgoID == MenuButtonKyNgo.KyNgoType.TichLuyNapHangNgay)
			{
				if (gadgetPanelBottom.checkThongBaoTichNapHangNgay())
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

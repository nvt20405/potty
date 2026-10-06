using System;
using System.Collections.Generic;
using UnityEngine;

public class ScreenKyNgo_TuongDuongTeThe : ScreenBase
{
	public GameObject phanThuongPerfab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<TuongDuongTeTheItem> ItemList = new List<TuongDuongTeTheItem>();

	private Vector3 itemPos;

	private Vector3 itemOffset;

	public UILabel lbDescription;

	public UILabel lbCurrentPoint;

	private int currentIdxSelected = -1;

	public UILabel lbTimeSuKien;

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
		UserInfo.ServerData.TichLuyNapCfg tichLuyNapConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyNapConfig;
		if (tichLuyNapConfig != null)
		{
			DateTime thoiGianBatDau = tichLuyNapConfig.ThoiGianBatDau;
			DateTime thoiGianKetThuc = tichLuyNapConfig.ThoiGianKetThuc;
			DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
			if (serverTime <= thoiGianKetThuc && serverTime >= thoiGianBatDau)
			{
				string arg = thoiGianBatDau.Day + "/" + thoiGianBatDau.Month;
				string arg2 = thoiGianKetThuc.Day + "/" + thoiGianKetThuc.Month + "/" + thoiGianKetThuc.Year;
				lbTimeSuKien.text = string.Format(Localization.instance.Get("ThoiGianSuKienTuongDuong"), arg, arg2);
			}
			else
			{
				lbTimeSuKien.text = string.Empty;
			}
		}
	}

	public void displayInfo()
	{
		lbCurrentPoint.text = string.Format(Localization.instance.Get("SoDiemTichLuyNapHienTai"), GameManager.instance.m_GameClient.UserInfo.Gamer.DiemTichLuyNap);
		lbDescription.text = Localization.instance.Get("TuongDuongTeTheDescription");
		updateTime();
		getListMocThuongNap();
	}

	public void getListMocThuongNap()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		itemPos = new Vector3(0f, 80f, 0f);
		itemOffset = new Vector3(0f, -120f, 0f);
		UserInfo.ServerData.TichLuyNapCfg tichLuyNapConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyNapConfig;
		if (tichLuyNapConfig == null || tichLuyNapConfig.ListMocNap == null || tichLuyNapConfig.ListMocNap.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < tichLuyNapConfig.ListMocNap.Count; i++)
		{
			UserInfo.ServerData.MocTichLuyNap mocTichLuyNap = tichLuyNapConfig.ListMocNap[i];
			if (mocTichLuyNap != null)
			{
				TuongDuongTeTheItem component = ((GameObject)UnityEngine.Object.Instantiate(phanThuongPerfab)).GetComponent<TuongDuongTeTheItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(mocTichLuyNap, i, GameManager.instance.m_GameClient.UserInfo.Gamer.DiemTichLuyNap);
				itemPos += itemOffset;
				UIEventListener.Get(component.gameObject).onClick = onClick_TuongDuongTeTheItem;
				UIEventListener.Get(component.btnDoi.gameObject).onClick = onClick_BtnDoi;
				UIEventListener.Get(component.btnHopQua.gameObject).onClick = onClick_BtnHopQua;
				ItemList.Add(component);
			}
		}
	}

	public void onClick_TuongDuongTeTheItem(GameObject go)
	{
		currentIdxSelected = -1;
		TuongDuongTeTheItem component = go.transform.GetComponent<TuongDuongTeTheItem>();
		if (component != null && component.mThuongNapData != null)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = component.mThuongNapData.ListPhanThuong;
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), phanThuongResponse);
		}
	}

	public void onClick_BtnDoi(GameObject go)
	{
		TuongDuongTeTheItem component = go.transform.parent.GetComponent<TuongDuongTeTheItem>();
		if (component != null && component.mThuongNapData != null && component.IdxMocThuongNap >= 0)
		{
			currentIdxSelected = component.IdxMocThuongNap;
			if (CommonHelper.CheckFlag(GameManager.instance.m_GameClient.UserInfo.Gamer.TichLuyNapFlag, currentIdxSelected))
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoDaNhanPhanThuong"));
			}
			else if (component.mThuongNapData.GiaTri <= GameManager.instance.m_GameClient.UserInfo.Gamer.DiemTichLuyNap)
			{
				PopupYesNo.Create(Localization.instance.Get("NhanThuongTichLuyConfirmMsg"), Localization.instance.Get("DongYLabelBtn"), Localization.instance.Get("TuChoiLabelBtn"), OnNhanThuongNow, null);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoChuaDuDiemDoi"));
			}
		}
	}

	public void onClick_BtnHopQua(GameObject go)
	{
		TuongDuongTeTheItem component = go.transform.parent.GetComponent<TuongDuongTeTheItem>();
		if (component != null && component.mThuongNapData != null)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = component.mThuongNapData.ListPhanThuong;
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), phanThuongResponse);
		}
	}

	public void OnNhanThuongNow()
	{
		NhanThuongTichLuyNapRequest nhanThuongTichLuyNapRequest = new NhanThuongTichLuyNapRequest();
		nhanThuongTichLuyNapRequest.Idx = currentIdxSelected;
		GameManager.instance.m_GameClient.RequestNhanThuongTichLuyNap(nhanThuongTichLuyNapRequest);
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
			if (gadgetPanelBottom.listKyNgoMenu[i].kyNgoID == MenuButtonKyNgo.KyNgoType.TichLuyNap)
			{
				if (gadgetPanelBottom.checkThongBaoTuongDuongTeThe())
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

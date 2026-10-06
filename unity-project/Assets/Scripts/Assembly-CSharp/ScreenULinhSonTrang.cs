using System;
using System.Collections.Generic;
using UnityEngine;

public class ScreenULinhSonTrang : ScreenBase
{
	public GameObject sonTrangPerfab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<ULinhSonTrangItem> ItemList = new List<ULinhSonTrangItem>();

	private Vector3 itemPos;

	private Vector3 itemOffset;

	public UILabel lbRefreshMess;

	public UILabel lbDiemHienTai;

	public UILabel lbTongDiem;

	public UILabel lbSuKien;

	public UILabel lbRefreshTime;

	private float nextSecond;

	private DateTime timeReset;

	public GameObject groupRefresh;

	private List<int> listCount = new List<int>();

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
		nextSecond += Time.deltaTime;
		if (nextSecond >= 1f)
		{
			nextSecond = 0f;
			displayTimeReset();
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		updateInfo();
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	public void updateInfo()
	{
		lbDiemHienTai.text = Localization.instance.Get("DiemHienTaiLabel") + ": " + GameManager.instance.m_GameClient.UserInfo.Gamer.ULinhCurrentDiem;
		lbTongDiem.text = Localization.instance.Get("TongDiemLabel") + ": " + GameManager.instance.m_GameClient.UserInfo.Gamer.ULinhTotalDiem;
		lbRefreshMess.text = string.Format(Localization.instance.Get("RefreshMessULinhSonTrangLabel"), GameManager.instance.m_GameClient.UserInfo.ServerInfo.ULinhCfg.KnbReset, GameManager.instance.m_GameClient.UserInfo.ServerInfo.ULinhCfg.KnbResetDiem);
		DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.ULinhCfg.ThoiGianBatDau;
		DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.ULinhCfg.ThoiGianKetThuc;
		string arg = thoiGianBatDau.Day + "/" + thoiGianBatDau.Month;
		string arg2 = thoiGianKetThuc.Day + "/" + thoiGianKetThuc.Month;
		lbSuKien.text = string.Format(Localization.instance.Get("TimeDienRaSuKienLabel"), arg, arg2);
	}

	public void displayTimeReset()
	{
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		TimeSpan timeSpan = timeReset - serverTime;
		if (timeSpan.TotalSeconds <= 0.0)
		{
			DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.ULinhCfg.ThoiGianBatDau;
			DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.ULinhCfg.ThoiGianKetThuc;
			DateTime serverTime2 = GameManager.instance.m_GameClient.ServerTime;
			if (serverTime2 < thoiGianKetThuc && serverTime2 > thoiGianBatDau)
			{
				GameManager.instance.m_GameClient.RequestULinhInfo();
			}
		}
		if (timeSpan.Days >= 0 && timeSpan.Seconds > 0 && timeSpan.Minutes >= 0 && timeSpan.Hours >= 0)
		{
			string empty = string.Empty;
			empty = ((timeSpan.Hours >= 10) ? (empty + timeSpan.Hours + ":") : (empty + "0" + timeSpan.Hours + ":"));
			empty = ((timeSpan.Minutes >= 10) ? (empty + timeSpan.Minutes + ":") : (empty + "0" + timeSpan.Minutes + ":"));
			empty = ((timeSpan.Seconds >= 10) ? (empty + timeSpan.Seconds) : (empty + "0" + timeSpan.Seconds));
			lbRefreshTime.text = Localization.instance.Get("RefreshTimeULinhSonTrangLabel") + " " + empty;
		}
	}

	public void displayListULinhSonTrang(ULinhInfoResponse response)
	{
		timeReset = response.ThoiGianReset;
		displayTimeReset();
		updateInfo();
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 160f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -150f, 0f);
		if (response != null && response.ListDoiDo != null && response.ListDoiDo.Count > 0)
		{
			listCount.Clear();
			listCount = response.ListDoiThuongCount;
			for (int i = 0; i < response.ListDoiDo.Count; i++)
			{
				ULinhInfoResponse.ULinhDoiDoItem dataItem = response.ListDoiDo[i];
				ULinhSonTrangItem component = ((GameObject)UnityEngine.Object.Instantiate(sonTrangPerfab)).GetComponent<ULinhSonTrangItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = vector;
				vector += vector2;
				component.setData(dataItem, i);
				UIEventListener.Get(component.btnDoi.gameObject).onClick = btnDoi_OnClick;
				ItemList.Add(component);
			}
		}
	}

	public void btnDoi_OnClick(GameObject go)
	{
		ULinhSonTrangItem component = go.transform.parent.GetComponent<ULinhSonTrangItem>();
		if (!(component != null) || component.m_Data == null)
		{
			return;
		}
		PhanThuongResponse.PhanThuong vpDoi = component.m_Data.VatPhamDoi;
		if (vpDoi.Loai == PhanThuongResponse.LoaiPhanThuong.BAC)
		{
			if (vpDoi.Count > GameManager.instance.m_GameClient.UserInfo.Gamer.Bac)
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoDuBacDeDoiMess"));
			}
			else
			{
				callRequestDoiThuong(component.mIndex, 0);
			}
		}
		else if (vpDoi.Loai == PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT)
		{
			UserInfo.HonNhanVatData honNhanVatData = GameManager.instance.m_GameClient.UserInfo.HonNhanVatList.Find((UserInfo.HonNhanVatData e) => e.Name == vpDoi.Name);
			if (honNhanVatData != null && honNhanVatData.Quantity >= vpDoi.Count)
			{
				callRequestDoiThuong(component.mIndex, honNhanVatData.ID);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoDuTanHonDeDoiMess"));
			}
		}
		else if (vpDoi.Loai == PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI)
		{
			UserInfo.ManhTrangBiData manhTrangBiData = GameManager.instance.m_GameClient.UserInfo.ManhTrangBiList.Find((UserInfo.ManhTrangBiData e) => e.Name == vpDoi.Name);
			if (manhTrangBiData != null && manhTrangBiData.Quantity >= vpDoi.Count)
			{
				callRequestDoiThuong(component.mIndex, manhTrangBiData.ID);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoDuManhTrangBiDeDoiMess"));
			}
		}
		else if (vpDoi.Loai == PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG)
		{
			UserInfo.ManhVoCongData manhVoCongData = GameManager.instance.m_GameClient.UserInfo.ManhVoCongList.Find((UserInfo.ManhVoCongData e) => e.Name == vpDoi.Name);
			if (manhVoCongData != null && manhVoCongData.Quantity >= vpDoi.Count)
			{
				callRequestDoiThuong(component.mIndex, manhVoCongData.ID);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoDuManhVoCongDeDoiMess"));
			}
		}
		else if (vpDoi.Loai == PhanThuongResponse.LoaiPhanThuong.TRANG_BI)
		{
			UserInfo.TrangBiData trangBiData = GameManager.instance.m_GameClient.UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.Name == vpDoi.Name && e.Level == 1 && e.HID <= 0);
			if (trangBiData != null)
			{
				callRequestDoiThuong(component.mIndex, trangBiData.ID);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoDuTrangBiDeDoiMess"));
			}
		}
		else if (vpDoi.Loai == PhanThuongResponse.LoaiPhanThuong.VO_CONG)
		{
			UserInfo.VoCongData voCongData = GameManager.instance.m_GameClient.UserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.Name == vpDoi.Name && e.Level == 1 && e.HID <= 0);
			if (voCongData != null)
			{
				callRequestDoiThuong(component.mIndex, voCongData.ID);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoDuVoCongDeDoiMess"));
			}
		}
		else if (vpDoi.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
		{
			if (GameManager.instance.m_GameClient.checkKNB(vpDoi.Count))
			{
				callRequestDoiThuong(component.mIndex, 0);
			}
		}
		else if (vpDoi.Loai == PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU)
		{
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == vpDoi.Name && e.Quantity >= 1);
			if (vatPhamTieuThuData != null)
			{
				callRequestDoiThuong(component.mIndex, vatPhamTieuThuData.ID);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoDuVatPhamDeDoiMess"));
			}
		}
	}

	public void updateInfoList(DoiItemULinhResponse response)
	{
		displayListULinhSonTrang(response.UpdateULinhInfo);
		PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), response.PhanThuongResponse);
	}

	public void callRequestDoiThuong(int index, int ID)
	{
		DoiItemULinhRequest doiItemULinhRequest = new DoiItemULinhRequest();
		doiItemULinhRequest.SlotIdx = index;
		if (ID > 0)
		{
			doiItemULinhRequest.VatPhamID = ID;
		}
		EGDebug.Log("RequestDoiItemULinh --- index: " + index + " - vat pham ID: " + ID);
		GameManager.instance.m_GameClient.RequestDoiItemULinh(doiItemULinhRequest);
	}

	public void btnRefresh_OnClick()
	{
		GameManager.instance.m_GameClient.RequestKnbRefreshULinh();
	}

	public void btnDoiThuong_OnClick()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDoiThuong);
		ScreenDoiThuong screenDoiThuong = GUIManager.getScreen(GAME_SCREEN.ScreenDoiThuong) as ScreenDoiThuong;
		screenDoiThuong.setListDoiThuongCount(listCount);
		screenDoiThuong.SyncWithNetworkData();
	}
}

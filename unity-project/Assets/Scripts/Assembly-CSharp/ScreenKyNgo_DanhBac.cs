using System;
using System.Collections.Generic;
using LitJson;
using Nettention.Proud;
using UnityEngine;

public class ScreenKyNgo_DanhBac : ScreenBase
{
	private const int maxItemCount = 12;

	public GameObject ItemRoot;

	public GameObject DanhbacItemPrefab;

	public UILabel lbTime;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<KyNgoDanhBacItem> ItemList = new List<KyNgoDanhBacItem>();

	private List<XocDiaInfoResponse.XocDiaItem> ListData = new List<XocDiaInfoResponse.XocDiaItem>();

	private int startItemGUI_Idx;

	private UnityEngine.Vector3 itemPos;

	private UnityEngine.Vector3 itemOffset;

	private float nextSecond;

	private DateTime timeReset;

	private void Start()
	{
	}

	public override void OnActive()
	{
		base.OnActive();
		getListDanhBacItem();
		if (child3Dscreen == null && GUIManager.instance.homeCity != null)
		{
			child3Dscreen = GUIManager.instance.homeCity.gameObject;
		}
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = false;
		}
		Utils.SetLightMaps("Lightmap/HomeCity/", 2);
		goToDanhBac();
	}

	public void goToDanhBac()
	{
		if (GUIManager.instance.homeCity.IsFightingNienThu)
		{
			(GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain).ThoatDanhNienThu();
		}
		UnityEngine.Vector3 vector = default(UnityEngine.Vector3);
		vector = new UnityEngine.Vector3(-44f, -1.5f, -4f);
		if (GUIManager.instance.homeCity.mainAvatar != null)
		{
			GUIManager.instance.homeCity.mainAvatar.Teleport(vector);
		}
		HomeResponse.Position3D position3D = new HomeResponse.Position3D();
		position3D.X = vector.x + UnityEngine.Random.Range(0f, -0.1f);
		position3D.Y = vector.y;
		position3D.Z = vector.z + UnityEngine.Random.Range(0f, 0.1f);
		GameManager.instance.m_GameClient.C2SProxy.RequestChangeNextPos(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(position3D));
	}

	private void Update()
	{
		nextSecond += Time.deltaTime;
		if (nextSecond >= 1f)
		{
			nextSecond = 0f;
			updateTime();
		}
	}

	public void getListDanhBacItem()
	{
		XocDiaInfoRequest request = new XocDiaInfoRequest();
		GameManager.instance.m_GameClient.RequestXocDiaInfo(request);
	}

	public void updateTime()
	{
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		TimeSpan timeSpan = timeReset - serverTime;
		string text = ((!(Math.Floor(timeSpan.TotalHours) < 10.0)) ? Math.Floor(timeSpan.TotalHours).ToString() : ("0" + Math.Floor(timeSpan.TotalHours)));
		string text2 = ((timeSpan.Minutes >= 10) ? timeSpan.Minutes.ToString() : ("0" + timeSpan.Minutes));
		string text3 = ((timeSpan.Seconds >= 10) ? timeSpan.Seconds.ToString() : ("0" + timeSpan.Seconds));
		lbTime.text = Localization.instance.Get("LamMoiLabel") + ": " + text + ":" + text2 + ":" + text3;
		if (timeSpan.Hours == 0 && timeSpan.Minutes == 0 && timeSpan.Seconds == -1)
		{
			lbTime.text = Localization.instance.Get("LamMoiLabel") + ": 00:00:00)";
			getListDanhBacItem();
		}
	}

	public void displayListItem(XocDiaInfoResponse response)
	{
		timeReset = response.ThoiGianReset;
		updateTime();
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		ListData.Clear();
		startItemGUI_Idx = 0;
		itemPos = new UnityEngine.Vector3(0f, 260f, 0f);
		itemOffset = new UnityEngine.Vector3(0f, -150f, 0f);
		if (response.ListItem == null || response.ListItem.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < response.ListItem.Count; i++)
		{
			KyNgoDanhBacItem component = ((GameObject)UnityEngine.Object.Instantiate(DanhbacItemPrefab)).GetComponent<KyNgoDanhBacItem>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new UnityEngine.Vector3(1f, 1f, 1f);
			component.transform.localPosition = itemPos;
			component.index = i;
			itemPos += itemOffset;
			component.Set(response.ListItem[i]);
			if (i == 1)
			{
				component.setVIP(2);
			}
			if (i == 2)
			{
				component.setVIP(4);
			}
			if (i == 3)
			{
				component.setVIP(6);
			}
			UIEventListener.Get(component.gameObject).onClick = onClick_DanhBacItem;
			UIEventListener.Get(component.btnDoiThuong.gameObject).onClick = onClick_DoiBtn;
			UIEventListener.Get(component.vatPhamFrom.gameObject).onClick = onClick_vatPhamCuocBtn;
			ListData.Add(response.ListItem[i]);
			ItemList.Add(component);
		}
	}

	public void onClick_DanhBacItem(GameObject go)
	{
		KyNgoDanhBacItem component = go.GetComponent<KyNgoDanhBacItem>();
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.Vip >= component.currentVIP)
		{
			if (component.m_Data.VatPhamCuoc.Loai == PhanThuongResponse.LoaiPhanThuong.VANG || component.m_Data.VatPhamCuoc.Loai == PhanThuongResponse.LoaiPhanThuong.BAC)
			{
				if (checkDieuKienVangBac(component.m_Data))
				{
					int index = component.index;
					PopUpKyNgoDanhBac.Create(index, 0);
				}
				return;
			}
			int num = checkDieuKienDanhCuoc(component.m_Data);
			if (num > 0)
			{
				int index2 = component.index;
				PopUpKyNgoDanhBac.Create(index2, num);
			}
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("CapVIPChuaDuMess"));
		}
	}

	public void onClick_DoiBtn(GameObject go)
	{
		KyNgoDanhBacItem component = go.transform.parent.GetComponent<KyNgoDanhBacItem>();
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.Vip >= component.currentVIP)
		{
			if (component.m_Data.VatPhamCuoc.Loai == PhanThuongResponse.LoaiPhanThuong.VANG || component.m_Data.VatPhamCuoc.Loai == PhanThuongResponse.LoaiPhanThuong.BAC)
			{
				if (checkDieuKienVangBac(component.m_Data))
				{
					int index = component.index;
					PopUpKyNgoDanhBac.Create(index, 0);
				}
				return;
			}
			int num = checkDieuKienDanhCuoc(component.m_Data);
			if (num > 0)
			{
				int index2 = component.index;
				PopUpKyNgoDanhBac.Create(index2, num);
			}
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("CapVIPChuaDuMess"));
		}
	}

	public void onClick_vatPhamCuocBtn(GameObject go)
	{
		KyNgoDanhBacItem component = go.transform.parent.GetComponent<KyNgoDanhBacItem>();
		if (component != null && component.m_Data != null)
		{
			PhanThuongResponse.PhanThuong vatPhamCuoc = component.m_Data.VatPhamCuoc;
		}
	}

	public bool checkDieuKienVangBac(XocDiaInfoResponse.XocDiaItem data)
	{
		if (data != null)
		{
			if (data.VatPhamCuoc.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
			{
				if (GameManager.instance.m_GameClient.UserInfo.Gamer.Vang >= data.VatPhamCuoc.Count)
				{
					return true;
				}
				MessagePopup.Create(Localization.instance.Get("KhongCoVangDanhCuocMess"));
			}
			else if (data.VatPhamCuoc.Loai == PhanThuongResponse.LoaiPhanThuong.BAC)
			{
				if (GameManager.instance.m_GameClient.UserInfo.Gamer.Bac >= data.VatPhamCuoc.Count)
				{
					return true;
				}
				MessagePopup.Create(Localization.instance.Get("KhongCoBacDanhCuocMess"));
			}
			return false;
		}
		return false;
	}

	public int checkDieuKienDanhCuoc(XocDiaInfoResponse.XocDiaItem data)
	{
		if (data != null)
		{
			EGDebug.Log("VAT PHAM DANH CUOC: " + data.VatPhamCuoc.Name);
			if (data.VatPhamCuoc.Loai == PhanThuongResponse.LoaiPhanThuong.VO_CONG)
			{
				UserInfo.VoCongData voCongData = GameManager.instance.m_GameClient.UserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.Name == data.VatPhamCuoc.Name && e.Level == 1 && e.HID <= 0);
				if (voCongData != null)
				{
					return voCongData.ID;
				}
				MessagePopup.Create(Localization.instance.Get("KhongCoVatPhamDanhCuocMess"));
			}
			else if (data.VatPhamCuoc.Loai == PhanThuongResponse.LoaiPhanThuong.NGUYEN_KHI)
			{
				UserInfo.NguyenKhiData nguyenKhiData = GameManager.instance.m_GameClient.UserInfo.NguyenKhiList.Find((UserInfo.NguyenKhiData e) => e.Codename == data.VatPhamCuoc.Name && e.Level == 1 && e.HID <= 0);
				if (nguyenKhiData != null)
				{
					return nguyenKhiData.ID;
				}
				MessagePopup.Create(Localization.instance.Get("KhongCoVatPhamDanhCuocMess"));
			}
			else if (data.VatPhamCuoc.Loai == PhanThuongResponse.LoaiPhanThuong.TRANG_BI)
			{
				UserInfo.TrangBiData trangBiData = GameManager.instance.m_GameClient.UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.Name == data.VatPhamCuoc.Name && e.Level == 1 && e.HID <= 0);
				if (trangBiData != null)
				{
					return trangBiData.ID;
				}
				MessagePopup.Create(Localization.instance.Get("KhongCoVatPhamDanhCuocMess"));
			}
			else if (data.VatPhamCuoc.Loai == PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU)
			{
				UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == data.VatPhamCuoc.Name && e.Quantity >= data.VatPhamCuoc.Count);
				if (vatPhamTieuThuData != null)
				{
					return vatPhamTieuThuData.ID;
				}
				MessagePopup.Create(Localization.instance.Get("KhongCoVatPhamDanhCuocMess"));
			}
			else if (data.VatPhamCuoc.Loai == PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT)
			{
				UserInfo.HonNhanVatData honNhanVatData = GameManager.instance.m_GameClient.UserInfo.HonNhanVatList.Find((UserInfo.HonNhanVatData e) => e.Name == data.VatPhamCuoc.Name && e.Quantity >= data.VatPhamCuoc.Count);
				if (honNhanVatData != null)
				{
					return honNhanVatData.ID;
				}
				MessagePopup.Create(Localization.instance.Get("KhongCoTanHonDanhCuocMess"));
			}
			else if (data.VatPhamCuoc.Loai == PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG)
			{
				UserInfo.ManhVoCongData manhVoCongData = GameManager.instance.m_GameClient.UserInfo.ManhVoCongList.Find((UserInfo.ManhVoCongData e) => e.Name == data.VatPhamCuoc.Name && e.Quantity >= data.VatPhamCuoc.Count);
				if (manhVoCongData != null)
				{
					return manhVoCongData.ID;
				}
				MessagePopup.Create(Localization.instance.Get("KhongCoManhVoCongDanhCuocMess"));
			}
			else if (data.VatPhamCuoc.Loai == PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI)
			{
				UserInfo.ManhTrangBiData manhTrangBiData = GameManager.instance.m_GameClient.UserInfo.ManhTrangBiList.Find((UserInfo.ManhTrangBiData e) => e.Name == data.VatPhamCuoc.Name && e.Quantity >= data.VatPhamCuoc.Count);
				if (manhTrangBiData != null)
				{
					return manhTrangBiData.ID;
				}
				MessagePopup.Create(Localization.instance.Get("KhongCoManhTrangBiDanhCuocMess"));
			}
			return -1;
		}
		return -1;
	}
}

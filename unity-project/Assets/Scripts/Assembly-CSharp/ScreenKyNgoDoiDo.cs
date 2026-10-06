using System;
using System.Collections.Generic;
using UnityEngine;

public class ScreenKyNgoDoiDo : ScreenBase
{
	private const int maxItemCount = 12;

	public GameObject doiDoPerfab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<KyNgoDoiDoItem> ItemList = new List<KyNgoDoiDoItem>();

	private Vector3 itemPos;

	private Vector3 itemOffset;

	public UILabel lbTime;

	private float nextSecond;

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
			updateTime();
		}
	}

	public void updateTime()
	{
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.ThoiGianKetThuc;
		TimeSpan timeSpan = thoiGianKetThuc - serverTime;
		if (timeSpan.Seconds > 0 && timeSpan.Minutes >= 0 && timeSpan.Hours >= 0)
		{
			string text = string.Format(Localization.instance.Get("TimeConLaiValueLabel"), timeSpan.Days, (timeSpan.Hours >= 10) ? timeSpan.Hours.ToString() : ("0" + timeSpan.Hours), (timeSpan.Minutes >= 10) ? timeSpan.Minutes.ToString() : ("0" + timeSpan.Minutes), (timeSpan.Seconds >= 10) ? timeSpan.Seconds.ToString() : ("0" + timeSpan.Seconds));
			lbTime.text = text;
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		getListDoiDo();
		updateTime();
	}

	public void getListDoiDo()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		itemPos = new Vector3(0f, 260f, 0f);
		itemOffset = new Vector3(0f, -160f, 0f);
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg != null)
		{
			for (int i = 0; i < 4; i++)
			{
				KyNgoDoiDoItem component = ((GameObject)UnityEngine.Object.Instantiate(doiDoPerfab)).GetComponent<KyNgoDoiDoItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				itemPos += itemOffset;
				component.setData(i);
				UIEventListener.Get(component.btnDoiDo.gameObject).onClick = onClick_DoiDoBtn;
				ItemList.Add(component);
			}
		}
	}

	public void onClick_DoiDoBtn(GameObject go)
	{
		KyNgoDoiDoItem component = go.transform.parent.GetComponent<KyNgoDoiDoItem>();
		if (!(component != null) || component.listItemData == null || component.listItemData.Count <= 0)
		{
			return;
		}
		int soLuotDoiDoByString = ConfigManager.instance.GetSoLuotDoiDoByString(GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay, component.indexDoiDoCfg);
		if (soLuotDoiDoByString >= component.mMaxLuot)
		{
			MessagePopup.Create(Localization.instance.Get("DaHetLuotDoiDoMess"));
			return;
		}
		DoiDoRequest doiDoRequest = new DoiDoRequest();
		doiDoRequest.SlotIdx = component.indexDoiDoCfg;
		int num = checkDuDieuKienDoiDo(component.listItemData[0]);
		if (num == 0)
		{
			return;
		}
		if (num > 0)
		{
			doiDoRequest.VatPham1ID = num;
		}
		int num2 = checkDuDieuKienDoiDo(component.listItemData[1]);
		if (num2 == 0)
		{
			return;
		}
		if (num2 > 0)
		{
			doiDoRequest.VatPham2ID = num2;
		}
		int num3 = checkDuDieuKienDoiDo(component.listItemData[2]);
		if (num3 != 0)
		{
			if (num3 > 0)
			{
				doiDoRequest.VatPham3ID = num3;
			}
			GameManager.instance.m_GameClient.RequestDoiDo(doiDoRequest);
		}
	}

	private int checkDuDieuKienDoiDo(PhanThuongResponse.PhanThuong pt)
	{
		if (pt == null)
		{
			return 0;
		}
		switch (pt.Loai)
		{
		case PhanThuongResponse.LoaiPhanThuong.BAC:
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.Bac >= pt.Count)
			{
				return -1;
			}
			MessagePopup.Create(Localization.instance.Get("ThieuBacDoiDoMess"));
			break;
		case PhanThuongResponse.LoaiPhanThuong.VANG:
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.Vang >= pt.Count)
			{
				return -1;
			}
			MessagePopup.Create(Localization.instance.Get("ThieuVangDoiDoMess"));
			break;
		case PhanThuongResponse.LoaiPhanThuong.TRANG_BI:
		{
			UserInfo.TrangBiData trangBiData = GameManager.instance.m_GameClient.UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.Name == pt.Name && e.Level == pt.Level && e.HID <= 0);
			if (trangBiData != null)
			{
				return trangBiData.ID;
			}
			MessagePopup.Create(Localization.instance.Get("KhongCoTrangBiDoiDoMess"));
			break;
		}
		case PhanThuongResponse.LoaiPhanThuong.VO_CONG:
		{
			UserInfo.VoCongData voCongData = GameManager.instance.m_GameClient.UserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.Name == pt.Name && e.Level == pt.Level && e.HID <= 0);
			if (voCongData != null)
			{
				return voCongData.ID;
			}
			MessagePopup.Create(Localization.instance.Get("KhongCoVoCongDoiDoMess"));
			break;
		}
		case PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU:
		{
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == pt.Name);
			if (vatPhamTieuThuData != null)
			{
				if (vatPhamTieuThuData.Quantity >= pt.Count)
				{
					return vatPhamTieuThuData.ID;
				}
				MessagePopup.Create(Localization.instance.Get("KhongCoDuVatPhamDoiDoMess"));
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoVatPhamDoiDoMess"));
			}
			break;
		}
		case PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT:
		{
			UserInfo.HonNhanVatData honNhanVatData = GameManager.instance.m_GameClient.UserInfo.HonNhanVatList.Find((UserInfo.HonNhanVatData e) => e.Name == pt.Name);
			if (honNhanVatData != null)
			{
				if (honNhanVatData.Quantity >= pt.Count)
				{
					return honNhanVatData.ID;
				}
				MessagePopup.Create(Localization.instance.Get("KhongCoDuHonNVDoiDoMess"));
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoHonNhanVatDoiDoMess"));
			}
			break;
		}
		case PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG:
		{
			UserInfo.ManhVoCongData manhVoCongData = GameManager.instance.m_GameClient.UserInfo.ManhVoCongList.Find((UserInfo.ManhVoCongData e) => e.Name == pt.Name);
			if (manhVoCongData != null)
			{
				if (manhVoCongData.Quantity >= pt.Count)
				{
					return manhVoCongData.ID;
				}
				MessagePopup.Create(Localization.instance.Get("KhongCoDuManhVoCongDoiDoMess"));
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoManhVCDoiDoMess"));
			}
			break;
		}
		case PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI:
		{
			UserInfo.ManhTrangBiData manhTrangBiData = GameManager.instance.m_GameClient.UserInfo.ManhTrangBiList.Find((UserInfo.ManhTrangBiData e) => e.Name == pt.Name);
			if (manhTrangBiData != null)
			{
				if (manhTrangBiData.Quantity >= pt.Count)
				{
					return manhTrangBiData.ID;
				}
				MessagePopup.Create(Localization.instance.Get("KhongCoDuManhTBDoiDoMess"));
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoManhTBDoiDoMess"));
			}
			break;
		}
		}
		return 0;
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
			if (gadgetPanelBottom.listKyNgoMenu[i].kyNgoID == MenuButtonKyNgo.KyNgoType.DoiDo)
			{
				if (gadgetPanelBottom.checkThongBaoDoiDo())
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

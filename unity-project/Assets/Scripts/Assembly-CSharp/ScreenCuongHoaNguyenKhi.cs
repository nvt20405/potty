using UnityEngine;

public class ScreenCuongHoaNguyenKhi : ScreenBase
{
	public NguyenKhiAvatar nguyenKhiAva;

	public UILabel nguyenKhiName;

	public UILabel lbGiaTriHoTroBefore;

	public UILabel lbGiaTriHoTroAfter;

	public UILabel lbLevelBefore;

	public UILabel lbLevelAfter;

	public UILabel lbMaxGiaTriHoTro;

	public UILabel lbMaxLevel;

	public OtherAvatar vatPhamYeuCau;

	public UILabel vpYeuCauName;

	public UILabel soLuongVPCan;

	public UILabel soLuongVPHienCo;

	public UserInfo.NguyenKhiData m_NguyenKhiData;

	public GameObject maxDotPhaGrp;

	public GameObject normalDotPhaGrp;

	public UILabel lbCuongHoaDescription;

	private NangCapNguyenKhiRequest request = new NangCapNguyenKhiRequest();

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

	public void Set(UserInfo.NguyenKhiData data)
	{
		if (data != null)
		{
			m_NguyenKhiData = data;
			request.NguyenKhiID = m_NguyenKhiData.ID;
		}
	}

	public override void OnActive()
	{
		if (m_NguyenKhiData != null)
		{
			displayInfo();
		}
	}

	public void displayInfo()
	{
		nguyenKhiAva.Set(m_NguyenKhiData);
		OtherCfg.NguyenKhiCfg nguyenKhiCfg = ConfigManager.instance.OtherConfig.NguyenKhiConfig[m_NguyenKhiData.Codename];
		if (nguyenKhiCfg != null)
		{
			nguyenKhiName.text = nguyenKhiCfg.DisplayName;
			string text = string.Empty;
			if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.DO_DON)
			{
				text = Localization.instance.Get("ChiSoCoBanDoDonLabel");
			}
			else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.MENH)
			{
				text = Localization.instance.Get("ChiSoCoBanMenhLabel");
			}
			else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.KHI)
			{
				text = Localization.instance.Get("ChiSoCoBanKhiLabel");
			}
			else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.KHANG_BAO)
			{
				text = Localization.instance.Get("ChiSoCoBanKhangBaoLabel");
			}
			else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.NGOAI)
			{
				text = Localization.instance.Get("ChiSoCoBanNgoaiLabel");
			}
			else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.THAN)
			{
				text = Localization.instance.Get("ChiSoCoBanThanLabel");
			}
			lbCuongHoaDescription.text = Localization.instance.Get("CuongHoaNguyenKhiDescription") + ". " + nguyenKhiCfg.MaxLevelDesc;
			UILabel uILabel = lbLevelAfter;
			string text2 = Localization.instance.Get("CapLabel") + ":[5C0C00] ";
			lbLevelBefore.text = text2;
			uILabel.text = text2;
			lbLevelBefore.text += m_NguyenKhiData.Level;
			lbLevelAfter.text += m_NguyenKhiData.Level + 1;
			UILabel uILabel2 = lbGiaTriHoTroBefore;
			text2 = text + ":";
			lbGiaTriHoTroAfter.text = text2;
			uILabel2.text = text2;
			lbGiaTriHoTroBefore.text += ((nguyenKhiCfg.Loai != OtherCfg.NguyenKhiType.DO_DON && nguyenKhiCfg.Loai != OtherCfg.NguyenKhiType.KHANG_BAO && nguyenKhiCfg.Loai != OtherCfg.NguyenKhiType.MENH) ? ("[5C0C00] " + ((float)nguyenKhiCfg.baseStat + nguyenKhiCfg.growStat * (float)(m_NguyenKhiData.Level - 1))) : ("[5C0C00] " + ((float)nguyenKhiCfg.baseStat + nguyenKhiCfg.growStat * (float)(m_NguyenKhiData.Level - 1)) + "%"));
			lbGiaTriHoTroAfter.text += ((nguyenKhiCfg.Loai != OtherCfg.NguyenKhiType.DO_DON && nguyenKhiCfg.Loai != OtherCfg.NguyenKhiType.KHANG_BAO && nguyenKhiCfg.Loai != OtherCfg.NguyenKhiType.MENH) ? ("[5C0C00] " + ((float)nguyenKhiCfg.baseStat + nguyenKhiCfg.growStat * (float)m_NguyenKhiData.Level)) : ("[5C0C00] " + ((float)nguyenKhiCfg.baseStat + nguyenKhiCfg.growStat * (float)m_NguyenKhiData.Level) + "%"));
			lbMaxLevel.text = Localization.instance.Get("CapLabel") + ":[5C0C00] " + m_NguyenKhiData.Level;
			lbMaxGiaTriHoTro.text = text + ":";
			lbMaxGiaTriHoTro.text += ((nguyenKhiCfg.Loai != OtherCfg.NguyenKhiType.DO_DON && nguyenKhiCfg.Loai != OtherCfg.NguyenKhiType.KHANG_BAO && nguyenKhiCfg.Loai != OtherCfg.NguyenKhiType.MENH) ? ("[5C0C00] " + ((float)nguyenKhiCfg.baseStat + nguyenKhiCfg.growStat * (float)(m_NguyenKhiData.Level - 1))) : ("[5C0C00] " + ((float)nguyenKhiCfg.baseStat + nguyenKhiCfg.growStat * (float)(m_NguyenKhiData.Level - 1)) + "%"));
		}
		if (m_NguyenKhiData.ExpRequired >= 0)
		{
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_NGUYEN_KHI_DAN");
			if (vatPhamTieuThuData != null)
			{
				soLuongVPHienCo.text = Localization.instance.Get("HienDangCo") + ":[F4C500] " + vatPhamTieuThuData.Quantity;
			}
			else
			{
				soLuongVPHienCo.text = Localization.instance.Get("HienDangCo") + ":[F4C500] 0";
			}
			vatPhamYeuCau.Set("VP_NGUYEN_KHI_DAN");
			VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu["VP_NGUYEN_KHI_DAN"];
			if (vatPhamTieuThuCfg != null)
			{
				vpYeuCauName.text = vatPhamTieuThuCfg.TenHienThi;
			}
			soLuongVPCan.text = "X " + m_NguyenKhiData.ExpRequired;
			normalDotPhaGrp.gameObject.SetActive(true);
			maxDotPhaGrp.gameObject.SetActive(false);
		}
		else
		{
			normalDotPhaGrp.gameObject.SetActive(false);
			maxDotPhaGrp.gameObject.SetActive(true);
		}
	}

	public void btnCuongHoa_OnClick()
	{
		if (m_NguyenKhiData != null && request != null && request.NguyenKhiID > 0)
		{
			GameManager.instance.m_GameClient.RequestNangCapNguyenKhi(request);
		}
	}

	public void btnHelp_OnClick()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(9, 2);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
		EGDebug.Log("btnHelp_OnClick");
	}

	public void btnDongY_OnClick()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenNguyenKhi);
	}

	public void btnBack_OnClick(GameObject go)
	{
		if (GUIManager.instance.LastScreen == GAME_SCREEN.ScreenDoiHinh)
		{
			ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
			screenDoiHinh.isDisplayChanKhi = true;
		}
		GUIManager.instance.SetScreen(GUIManager.instance.LastScreen);
	}

	public void updateView(int nguyenKhiID)
	{
		if (nguyenKhiID > 0)
		{
			m_NguyenKhiData = GameManager.instance.m_GameClient.UserInfo.NguyenKhiList.Find((UserInfo.NguyenKhiData e) => e.ID == nguyenKhiID);
			displayInfo();
		}
	}
}

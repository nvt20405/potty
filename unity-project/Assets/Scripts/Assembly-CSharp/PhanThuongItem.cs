using UnityEngine;

public class PhanThuongItem : MonoBehaviour
{
	public OtherAvatar otherAvatar;

	public NhanVatAvatar nhanVatAvatar;

	public NguyenKhiAvatar nguyenKhiAvatar;

	public UILabel nameLabel;

	public UILabel countLabel;

	private PhanThuongResponse.PhanThuong phanThuong;

	private void TurnOnNhanVatAvatar()
	{
		if (nhanVatAvatar != null)
		{
			NGUITools.SetActive(nhanVatAvatar.gameObject, true);
		}
		NGUITools.SetActive(otherAvatar.gameObject, false);
		if (nguyenKhiAvatar != null)
		{
			NGUITools.SetActive(nguyenKhiAvatar.gameObject, false);
		}
	}

	private void TurnOnOtherAvatar()
	{
		if (nhanVatAvatar != null)
		{
			NGUITools.SetActive(nhanVatAvatar.gameObject, false);
		}
		NGUITools.SetActive(otherAvatar.gameObject, true);
		if (nguyenKhiAvatar != null)
		{
			NGUITools.SetActive(nguyenKhiAvatar.gameObject, false);
		}
	}

	private void TurnOnNguyenKhiKhiAvatar()
	{
		if (nhanVatAvatar != null)
		{
			NGUITools.SetActive(nhanVatAvatar.gameObject, false);
		}
		NGUITools.SetActive(otherAvatar.gameObject, false);
		if (nguyenKhiAvatar != null)
		{
			NGUITools.SetActive(nguyenKhiAvatar.gameObject, true);
		}
	}

	private void OnClickTrangBi(OtherAvatar avatar)
	{
		if (phanThuong != null)
		{
			UserInfo.TrangBiData trangBiData = new UserInfo.TrangBiData();
			string text = phanThuong.Name;
			if (text.StartsWith("MVK_") || text.StartsWith("MAG_") || text.StartsWith("MTS_") || text.StartsWith("MMU_"))
			{
				text = text.Substring(1);
			}
			trangBiData.Name = text;
			trangBiData.Level = phanThuong.Level;
			PopupTrangBi.CreateByNormalScreen(trangBiData);
		}
	}

	private void OnClickVoCong(OtherAvatar avatar)
	{
		if (phanThuong != null)
		{
			UserInfo.VoCongData voCongData = new UserInfo.VoCongData();
			string text = phanThuong.Name;
			if (text.StartsWith("MVC_"))
			{
				text = text.Substring(1);
			}
			voCongData.Name = text;
			voCongData.Level = phanThuong.Level;
			PopupVoCong.CreateByNormalScreen(GameManager.instance.m_GameClient.UserInfo, voCongData, voCongData.Level);
		}
	}

	private void OnClickVatPhamTieuThu(OtherAvatar avatar)
	{
		if (phanThuong != null)
		{
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData = new UserInfo.VatPhamTieuThuData();
			vatPhamTieuThuData.Name = phanThuong.Name;
			vatPhamTieuThuData.Quantity = phanThuong.Count;
			PopUpVatPham.Create(vatPhamTieuThuData);
		}
	}

	private void OnClickNguyenKhi(NguyenKhiAvatar avatar)
	{
		if (phanThuong != null)
		{
			UserInfo.NguyenKhiData nguyenKhiData = new UserInfo.NguyenKhiData();
			nguyenKhiData.Codename = phanThuong.Name;
			nguyenKhiData.Level = phanThuong.Level;
			PopUpNguyenKhi.CreateByNormalScreen(nguyenKhiData);
		}
	}

	private void OnClickThuCuoi(OtherAvatar avatar)
	{
		if (phanThuong != null)
		{
			UserInfo.ThuCuoiData thuCuoiData = new UserInfo.ThuCuoiData();
			thuCuoiData.CodeName = phanThuong.Name;
			thuCuoiData.Duration = phanThuong.Count;
			PopupTrangBi.CreateByThuCuoiInfo(thuCuoiData);
		}
	}

	private void OnClickThanThu(NhanVatAvatar avatar)
	{
		if (phanThuong != null)
		{
			UserInfo.PetInfo petInfo = new UserInfo.PetInfo();
			petInfo.codename = phanThuong.Name;
			petInfo.curExp = 0;
			petInfo.DiemThonPhe = 0;
			petInfo.growRate = 0f;
			petInfo.heso1 = 50;
			petInfo.heso2 = 50;
			petInfo.level = 1;
			petInfo.maxExp = 100;
			petInfo.petType = UserInfo.PetInfo.PetType.MENH_THAN;
			petInfo.Quality = (UserInfo.PetInfo.PetQuality)phanThuong.Level;
			petInfo.skill = "VC_PET_TIEU_DAO_VO_ANH";
			PopupThanThuInfo.Create(petInfo, false);
		}
	}

	private void OnClickNhanVat(NhanVatAvatar avatar)
	{
		if (phanThuong != null)
		{
			UserInfo.HeroData heroData = new UserInfo.HeroData();
			heroData.Name = avatar.avatar.spriteName;
			heroData.Level = phanThuong.Level;
			heroData.VoCong1Name = ConfigManager.instance.m_dicNhanVats[heroData.Name].VoCongMacDinh;
			heroData.VoCong1Level = 1;
			PopupNhanVat.CreateByNormalScreens(GameManager.instance.m_GameClient.UserInfo, heroData);
		}
	}

	private void OnClickCaoNhan(NhanVatAvatar avatar)
	{
		if (phanThuong != null)
		{
			MessagePopup.Create(Localization.instance.Get("PhanThuongCaoNhanMsg"));
		}
	}

	private void OnClickTyThi(NhanVatAvatar avatar)
	{
		if (phanThuong != null)
		{
			MessagePopup.Create(Localization.instance.Get("PhanThuongTyThiMsg"));
		}
	}

	private void OnClickBangHuu(NhanVatAvatar avatar)
	{
		if (phanThuong != null)
		{
			MessagePopup.Create(Localization.instance.Get("PhanThuongBangHuuMsg"));
		}
	}

	private void OnClickThuongNhan(NhanVatAvatar avatar)
	{
		if (phanThuong != null)
		{
			MessagePopup.Create(Localization.instance.Get("PhanThuongThuongNhanMsg"));
		}
	}

	private void OnClickBanDo(OtherAvatar avatar)
	{
		if (phanThuong != null)
		{
			MessagePopup.Create(Localization.instance.Get("PhanThuongBanDoMsg"));
		}
	}

	public void Set(PhanThuongResponse.PhanThuong pt, bool isPopup, bool isOpenbyCHThanBi = false, bool isOpenByPopupNapTien = false)
	{
		phanThuong = pt;
		switch (pt.Loai)
		{
		case PhanThuongResponse.LoaiPhanThuong.BAC:
			TurnOnOtherAvatar();
			otherAvatar.SetBac(pt.Count);
			if (isPopup)
			{
				Utils.SetLayer(otherAvatar.transform, "GUIPopUp", true);
			}
			else
			{
				Utils.SetLayer(otherAvatar.transform, "Default", true);
			}
			displayInfo(Localization.instance.Get("Bac"), string.Format(Localization.instance.Get("SoLuongPopupPhanThuong"), pt.Count));
			break;
		case PhanThuongResponse.LoaiPhanThuong.VANG:
			TurnOnOtherAvatar();
			otherAvatar.SetVang(pt.Count);
			if (isPopup)
			{
				Utils.SetLayer(otherAvatar.transform, "GUIPopUp", true);
			}
			else
			{
				Utils.SetLayer(otherAvatar.transform, "Default", true);
			}
			displayInfo(Localization.instance.Get("Vang"), string.Format(Localization.instance.Get("SoLuongPopupPhanThuong"), pt.Count));
			break;
		case PhanThuongResponse.LoaiPhanThuong.TRANG_BI:
			TurnOnOtherAvatar();
			if (isOpenbyCHThanBi)
			{
				otherAvatar.Set(pt.Name, 0, phanThuong.Level, phanThuong.Count);
			}
			else
			{
				otherAvatar.Set(pt.Name, 0, phanThuong.Level);
			}
			if (isPopup)
			{
				Utils.SetLayer(otherAvatar.transform, "GUIPopUp", true);
			}
			else
			{
				Utils.SetLayer(otherAvatar.transform, "Default", true);
			}
			otherAvatar.OnEventClick = OnClickTrangBi;
			if (ConfigManager.instance.m_dicTrangBi.ContainsKey(pt.Name))
			{
				displayInfo(ConfigManager.instance.m_dicTrangBi[pt.Name].TenHienThi, string.Empty);
			}
			break;
		case PhanThuongResponse.LoaiPhanThuong.VO_CONG:
			TurnOnOtherAvatar();
			if (isOpenbyCHThanBi)
			{
				otherAvatar.Set(pt.Name, 0, pt.Level, pt.Count);
			}
			else
			{
				otherAvatar.Set(pt.Name, 0, pt.Level);
			}
			if (isPopup)
			{
				Utils.SetLayer(otherAvatar.transform, "GUIPopUp", true);
			}
			else
			{
				Utils.SetLayer(otherAvatar.transform, "Default", true);
			}
			otherAvatar.OnEventClick = OnClickVoCong;
			if (ConfigManager.instance.m_dicVCs.ContainsKey(pt.Name))
			{
				string empty = string.Empty;
				empty = ((pt.Level > 1) ? string.Format(Localization.instance.Get("PhanThuongVoCongItem"), ConfigManager.instance.m_dicVCs[pt.Name].TenHienThi, pt.Level) : ConfigManager.instance.m_dicVCs[pt.Name].TenHienThi);
				displayInfo(empty, string.Empty);
			}
			break;
		case PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU:
			TurnOnOtherAvatar();
			if (isOpenbyCHThanBi | isOpenByPopupNapTien)
			{
				otherAvatar.Set(pt.Name, 0, -1, pt.Count);
			}
			else
			{
				otherAvatar.Set(pt.Name);
			}
			if (isPopup)
			{
				Utils.SetLayer(otherAvatar.transform, "GUIPopUp", true);
			}
			else
			{
				Utils.SetLayer(otherAvatar.transform, "Default", true);
			}
			otherAvatar.OnEventClick = OnClickVatPhamTieuThu;
			if (ConfigManager.instance.m_dicVatPhamTieuThu.ContainsKey(pt.Name))
			{
				displayInfo(ConfigManager.instance.m_dicVatPhamTieuThu[pt.Name].TenHienThi, string.Format(Localization.instance.Get("SoLuongPopupPhanThuong"), pt.Count));
			}
			break;
		case PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT:
		{
			TurnOnNhanVatAvatar();
			string key2 = pt.Name;
			if (pt.Name.StartsWith("TH_"))
			{
				key2 = "NV_" + pt.Name.Substring(3);
			}
			nhanVatAvatar.Set(pt.Name, 0, -1, false, pt.Count);
			if (nhanVatAvatar.tanHon != null)
			{
				nhanVatAvatar.tanHon.gameObject.SetActive(true);
			}
			if (isPopup)
			{
				Utils.SetLayer(nhanVatAvatar.transform, "GUIPopUp", true);
			}
			else
			{
				Utils.SetLayer(nhanVatAvatar.transform, "Default", true);
			}
			nhanVatAvatar.OnEventClick = OnClickNhanVat;
			if (ConfigManager.instance.m_dicNhanVats.ContainsKey(key2))
			{
				displayInfo(string.Format(Localization.instance.Get("PhanThuongTanHonItem"), ConfigManager.instance.m_dicNhanVats[key2].TenHienThi), string.Format(Localization.instance.Get("SoLuongPopupPhanThuong"), pt.Count));
			}
			break;
		}
		case PhanThuongResponse.LoaiPhanThuong.CAO_NHAN:
			TurnOnNhanVatAvatar();
			nhanVatAvatar.Set("Avatar_CaoNhan1", 0, pt.Level);
			if (isPopup)
			{
				Utils.SetLayer(nhanVatAvatar.transform, "GUIPopUp", true);
			}
			else
			{
				Utils.SetLayer(nhanVatAvatar.transform, "Default", true);
			}
			nhanVatAvatar.OnEventClick = OnClickCaoNhan;
			displayInfo(string.Format(Localization.instance.Get("PhanThuongCaoNhanInfo"), pt.Level), string.Format(Localization.instance.Get("SoLuongPopupPhanThuong"), pt.Count));
			break;
		case PhanThuongResponse.LoaiPhanThuong.BAN_DO:
			TurnOnOtherAvatar();
			otherAvatar.Set("Avatar_BanDo1");
			if (isPopup)
			{
				Utils.SetLayer(otherAvatar.transform, "GUIPopUp", true);
			}
			else
			{
				Utils.SetLayer(otherAvatar.transform, "Default", true);
			}
			otherAvatar.OnEventClick = OnClickBanDo;
			displayInfo(string.Format(Localization.instance.Get("PhanThuongBanDoInfo"), pt.Level), string.Empty);
			break;
		case PhanThuongResponse.LoaiPhanThuong.BANG_HUU:
			TurnOnNhanVatAvatar();
			nhanVatAvatar.Set("Avatar_BangHuu1");
			if (isPopup)
			{
				Utils.SetLayer(nhanVatAvatar.transform, "GUIPopUp", true);
			}
			else
			{
				Utils.SetLayer(nhanVatAvatar.transform, "Default", true);
			}
			nhanVatAvatar.OnEventClick = OnClickBangHuu;
			displayInfo(Localization.instance.Get("PhanThuongBangHuuInfo"), string.Empty);
			break;
		case PhanThuongResponse.LoaiPhanThuong.THUONG_NHAN:
			TurnOnNhanVatAvatar();
			nhanVatAvatar.Set("Avatar_ThuongNhan1");
			if (isPopup)
			{
				Utils.SetLayer(nhanVatAvatar.transform, "GUIPopUp", true);
			}
			else
			{
				Utils.SetLayer(nhanVatAvatar.transform, "Default", true);
			}
			nhanVatAvatar.OnEventClick = OnClickThuongNhan;
			displayInfo(Localization.instance.Get("PhanThuongThuongNhanInfo"), string.Empty);
			break;
		case PhanThuongResponse.LoaiPhanThuong.TY_THI:
			TurnOnNhanVatAvatar();
			nhanVatAvatar.Set("Avatar_TyThi1");
			if (isPopup)
			{
				Utils.SetLayer(nhanVatAvatar.transform, "GUIPopUp", true);
			}
			else
			{
				Utils.SetLayer(nhanVatAvatar.transform, "Default", true);
			}
			nhanVatAvatar.OnEventClick = OnClickTyThi;
			displayInfo(Localization.instance.Get("PhanThuongTyThiInfo"), string.Empty);
			break;
		case PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG:
		{
			TurnOnOtherAvatar();
			string key = pt.Name;
			if (pt.Name.StartsWith("MVC_"))
			{
				key = pt.Name.Substring(1);
			}
			otherAvatar.Set(pt.Name, 0, -1, pt.Count);
			if (otherAvatar.manhSprite != null)
			{
				otherAvatar.manhSprite.gameObject.SetActive(true);
			}
			if (isPopup)
			{
				Utils.SetLayer(otherAvatar.transform, "GUIPopUp", true);
			}
			else
			{
				Utils.SetLayer(otherAvatar.transform, "Default", true);
			}
			otherAvatar.OnEventClick = OnClickVoCong;
			CfgVoCong value2;
			if (ConfigManager.instance.m_dicVCs.TryGetValue(key, out value2))
			{
				displayInfo(string.Format(Localization.instance.Get("PhanThuongManhItem"), value2.TenHienThi), string.Format(Localization.instance.Get("SoLuongPopupPhanThuong"), pt.Count));
			}
			break;
		}
		case PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI:
		{
			TurnOnOtherAvatar();
			string text = pt.Name;
			if (pt.Name.StartsWith("MVK_") || pt.Name.StartsWith("MAG_") || pt.Name.StartsWith("MTS_") || pt.Name.StartsWith("MMU_"))
			{
				text = pt.Name.Substring(1);
			}
			otherAvatar.Set(text, 0, -1, pt.Count);
			if (otherAvatar.manhSprite != null)
			{
				otherAvatar.manhSprite.gameObject.SetActive(true);
			}
			if (isPopup)
			{
				Utils.SetLayer(otherAvatar.transform, "GUIPopUp", true);
			}
			else
			{
				Utils.SetLayer(otherAvatar.transform, "Default", true);
			}
			otherAvatar.OnEventClick = OnClickTrangBi;
			TrangBiCfg value3;
			if (ConfigManager.instance.m_dicTrangBi.TryGetValue(text, out value3))
			{
				displayInfo(string.Format(Localization.instance.Get("PhanThuongManhItem"), value3.TenHienThi), string.Format(Localization.instance.Get("SoLuongPopupPhanThuong"), pt.Count));
			}
			break;
		}
		case PhanThuongResponse.LoaiPhanThuong.NGUYEN_KHI:
			TurnOnNguyenKhiKhiAvatar();
			if (nguyenKhiAvatar != null)
			{
				nguyenKhiAvatar.Set(pt.Name, pt.Level);
				if (isPopup)
				{
					Utils.SetLayer(nguyenKhiAvatar.transform, "GUIPopUp", true);
				}
				else
				{
					Utils.SetLayer(nguyenKhiAvatar.transform, "Default", true);
				}
				nguyenKhiAvatar.OnEventClick = OnClickNguyenKhi;
				if (ConfigManager.instance.OtherConfig.NguyenKhiConfig.ContainsKey(pt.Name))
				{
					displayInfo(ConfigManager.instance.OtherConfig.NguyenKhiConfig[pt.Name].DisplayName, string.Format(Localization.instance.Get("SoLuongPopupPhanThuong"), pt.Count));
				}
			}
			break;
		case PhanThuongResponse.LoaiPhanThuong.THU_CUOI:
			TurnOnOtherAvatar();
			if (otherAvatar != null)
			{
				otherAvatar.Set(pt.Name);
				if (isPopup)
				{
					Utils.SetLayer(otherAvatar.transform, "GUIPopUp", true);
				}
				else
				{
					Utils.SetLayer(otherAvatar.transform, "Default", true);
				}
				otherAvatar.OnEventClick = OnClickThuCuoi;
				if (ConfigManager.instance.OtherConfig.ThuCuoiConfig != null && ConfigManager.instance.OtherConfig.ThuCuoiConfig.ContainsKey(pt.Name))
				{
					displayInfo(ConfigManager.instance.OtherConfig.ThuCuoiConfig[pt.Name].DisplayName, string.Format(Localization.instance.Get("TimeSuDungNguaLabel"), pt.Count));
				}
				else
				{
					setNullValue();
				}
			}
			break;
		case PhanThuongResponse.LoaiPhanThuong.THAN_THU:
			TurnOnNhanVatAvatar();
			if (nhanVatAvatar != null)
			{
				nhanVatAvatar.Set(pt.Name + "_" + (pt.Level + 1));
				if (isPopup)
				{
					Utils.SetLayer(otherAvatar.transform, "GUIPopUp", true);
				}
				else
				{
					Utils.SetLayer(otherAvatar.transform, "Default", true);
				}
				nhanVatAvatar.OnEventClick = OnClickThanThu;
				displayInfo(Localization.instance.Get(pt.Name), Localization.instance.Get(((UserInfo.PetInfo.PetQuality)pt.Level/*cast due to constrained. prefix*/).ToString()));
			}
			break;
		case PhanThuongResponse.LoaiPhanThuong.HUYEN_KHI:
			TurnOnOtherAvatar();
			if (otherAvatar != null)
			{
				UserInfo.HuyenKhi huyenKhi = new UserInfo.HuyenKhi();
				huyenKhi.Name = pt.Name;
				huyenKhi.Level = pt.Level;
				huyenKhi.ID = pt.ID;
				otherAvatar.SetHuyenKhi(huyenKhi);
				otherAvatar.countBkg.gameObject.SetActive(false);
				otherAvatar.countLabel.gameObject.SetActive(false);
				otherAvatar.lvlLabel.gameObject.SetActive(false);
				if (isPopup)
				{
					Utils.SetLayer(otherAvatar.transform, "GUIPopUp", true);
				}
				else
				{
					Utils.SetLayer(otherAvatar.transform, "Default", true);
				}
				HuyenKhiCfg value;
				ConfigManager.instance.m_dicHuyenKhi.TryGetValue(pt.Name, out value);
				if (value != null)
				{
					displayInfo(value.TenHienThi, string.Format(Localization.instance.Get("SoLuongPopupPhanThuong"), pt.Count));
				}
				else
				{
					setNullValue();
				}
			}
			break;
		case PhanThuongResponse.LoaiPhanThuong.LOAI_PHAN_THUONG_COUNT:
			break;
		}
	}

	public void setNullValue()
	{
		TurnOnOtherAvatar();
		otherAvatar.Set("empty");
		displayInfo(string.Empty, string.Empty);
	}

	public void displayInfo(string name, string countStr)
	{
		if (nameLabel != null)
		{
			nameLabel.text = name;
		}
		if (countLabel != null)
		{
			countLabel.text = countStr;
		}
	}

	public void setVang(int count, bool showCount)
	{
		TurnOnOtherAvatar();
		otherAvatar.SetVang(count, showCount);
		displayInfo(Localization.instance.Get("Vang"), string.Format(Localization.instance.Get("SoLuongPopupPhanThuong"), count));
	}

	public void setBac(int count, bool showCount)
	{
		TurnOnOtherAvatar();
		otherAvatar.SetBac(count, showCount);
		displayInfo(Localization.instance.Get("Bac"), string.Format(Localization.instance.Get("SoLuongPopupPhanThuong"), count));
	}
}

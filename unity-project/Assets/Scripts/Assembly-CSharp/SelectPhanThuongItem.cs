using UnityEngine;

public class SelectPhanThuongItem : MonoBehaviour
{
	public OtherAvatar otherAvatar;

	public NhanVatAvatar nhanVatAvatar;

	public NguyenKhiAvatar nguyenKhiAvatar;

	public UILabel nameLabel;

	public UILabel countLabel;

	public UICheckbox checkBox;

	public int index = -1;

	public PhanThuongResponse.PhanThuong PhanThuong;

	private void TurnOnNhanVatAvatar()
	{
		NGUITools.SetActive(nguyenKhiAvatar.gameObject, false);
		NGUITools.SetActive(nhanVatAvatar.gameObject, true);
		NGUITools.SetActive(otherAvatar.gameObject, false);
	}

	private void TurnOnOtherAvatar()
	{
		NGUITools.SetActive(nhanVatAvatar.gameObject, false);
		NGUITools.SetActive(nguyenKhiAvatar.gameObject, false);
		NGUITools.SetActive(otherAvatar.gameObject, true);
	}

	private void TurnOnNguyenKhiAvatar()
	{
		NGUITools.SetActive(nhanVatAvatar.gameObject, false);
		NGUITools.SetActive(otherAvatar.gameObject, false);
		NGUITools.SetActive(nguyenKhiAvatar.gameObject, true);
	}

	public void Set(PhanThuongResponse.PhanThuong pt, int indexPT)
	{
		PhanThuong = pt;
		index = indexPT;
		switch (pt.Loai)
		{
		case PhanThuongResponse.LoaiPhanThuong.BAC:
			TurnOnOtherAvatar();
			otherAvatar.SetBac(pt.Count);
			displayInfo(Localization.instance.Get("BacLabel"), string.Format(Localization.instance.Get("SoLuongPopupPhanThuong"), pt.Count.ToString()));
			break;
		case PhanThuongResponse.LoaiPhanThuong.VANG:
			TurnOnOtherAvatar();
			otherAvatar.SetVang(pt.Count);
			displayInfo(Localization.instance.Get("VangLabel"), string.Format(Localization.instance.Get("SoLuongPopupPhanThuong"), pt.Count.ToString()));
			break;
		case PhanThuongResponse.LoaiPhanThuong.TRANG_BI:
			TurnOnOtherAvatar();
			otherAvatar.Set(pt.Name);
			if (ConfigManager.instance.m_dicTrangBi.ContainsKey(pt.Name))
			{
				displayInfo(ConfigManager.instance.m_dicTrangBi[pt.Name].TenHienThi, string.Empty);
			}
			break;
		case PhanThuongResponse.LoaiPhanThuong.VO_CONG:
			TurnOnOtherAvatar();
			if (pt.Level <= 1)
			{
				otherAvatar.Set(pt.Name);
			}
			else
			{
				otherAvatar.Set(pt.Name);
			}
			if (ConfigManager.instance.m_dicVCs.ContainsKey(pt.Name))
			{
				string empty = string.Empty;
				empty = ((pt.Level > 1) ? (ConfigManager.instance.m_dicVCs[pt.Name].TenHienThi + " Cấp " + pt.Level) : ConfigManager.instance.m_dicVCs[pt.Name].TenHienThi);
				displayInfo(empty, string.Empty);
			}
			break;
		case PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU:
			TurnOnOtherAvatar();
			otherAvatar.Set(pt.Name);
			if (ConfigManager.instance.m_dicVatPhamTieuThu.ContainsKey(pt.Name))
			{
				displayInfo(ConfigManager.instance.m_dicVatPhamTieuThu[pt.Name].TenHienThi, "Số lượng : " + pt.Count);
			}
			break;
		case PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT:
		{
			TurnOnNhanVatAvatar();
			string text3 = pt.Name;
			if (pt.Name.StartsWith("NV_"))
			{
				text3 = pt.Name.Replace("NV_", "TH_");
			}
			nhanVatAvatar.Set(text3, 0, -1, false, pt.Count);
			text3 = text3.Replace("TH_", "NV_");
			if (ConfigManager.instance.m_dicNhanVats.ContainsKey(text3))
			{
				displayInfo(string.Format(Localization.instance.Get("PhanThuongTanHonItem"), ConfigManager.instance.m_dicNhanVats[text3].TenHienThi), string.Format(Localization.instance.Get("SoLuongPopupPhanThuong"), pt.Count.ToString()));
			}
			break;
		}
		case PhanThuongResponse.LoaiPhanThuong.CAO_NHAN:
			TurnOnNhanVatAvatar();
			nhanVatAvatar.Set("Avatar_CaoNhan1", 0, pt.Level);
			displayInfo("Đệ nhất kiếm chỉ điểm - Cấp " + pt.Level, "Số lượng : " + pt.Count);
			break;
		case PhanThuongResponse.LoaiPhanThuong.BAN_DO:
			TurnOnOtherAvatar();
			otherAvatar.Set("Avatar_BanDo1");
			displayInfo("Lấy được cổ đồ" + pt.Level, string.Empty);
			break;
		case PhanThuongResponse.LoaiPhanThuong.BANG_HUU:
			TurnOnNhanVatAvatar();
			nhanVatAvatar.Set("Avatar_BangHuu1");
			displayInfo("Gặp được hảo hữu", string.Empty);
			break;
		case PhanThuongResponse.LoaiPhanThuong.THUONG_NHAN:
			TurnOnNhanVatAvatar();
			nhanVatAvatar.Set("Avatar_ThuongNhan1");
			displayInfo("Gặp được thương nhân ", string.Empty);
			break;
		case PhanThuongResponse.LoaiPhanThuong.TY_THI:
			TurnOnNhanVatAvatar();
			nhanVatAvatar.Set("Avatar_TyThi1");
			displayInfo("Gặp được anh hùng", string.Empty);
			break;
		case PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG:
		{
			TurnOnOtherAvatar();
			string text = pt.Name;
			if (!pt.Name.StartsWith("MVC_"))
			{
				text = "M" + pt.Name;
			}
			otherAvatar.Set(text);
			text = text.Substring(1);
			CfgVoCong value;
			if (ConfigManager.instance.m_dicVCs.TryGetValue(text, out value))
			{
				displayInfo(string.Format(Localization.instance.Get("PhanThuongManhItem"), value.TenHienThi), string.Format(Localization.instance.Get("SoLuongPopupPhanThuong"), pt.Count.ToString()));
			}
			break;
		}
		case PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI:
		{
			TurnOnOtherAvatar();
			string text2 = ((!pt.Name.StartsWith("MVK_") && !pt.Name.StartsWith("MAG_") && !pt.Name.StartsWith("MTS_") && !pt.Name.StartsWith("MMU_")) ? ("M" + pt.Name) : pt.Name);
			otherAvatar.Set(text2);
			text2 = text2.Substring(1);
			TrangBiCfg value2;
			if (ConfigManager.instance.m_dicTrangBi.TryGetValue(text2, out value2))
			{
				displayInfo(string.Format(Localization.instance.Get("PhanThuongManhItem"), value2.TenHienThi), string.Format(Localization.instance.Get("SoLuongPopupPhanThuong"), pt.Count.ToString()));
			}
			break;
		}
		case PhanThuongResponse.LoaiPhanThuong.NGUYEN_KHI:
			TurnOnNguyenKhiAvatar();
			if (nguyenKhiAvatar != null)
			{
				nguyenKhiAvatar.Set(pt.Name, pt.Level);
				Utils.SetLayer(nguyenKhiAvatar.transform, "GUIPopUp", true);
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
				Utils.SetLayer(otherAvatar.transform, "GUIPopUp", true);
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
				Utils.SetLayer(otherAvatar.transform, "GUIPopUp", true);
				displayInfo(Localization.instance.Get(pt.Name), Localization.instance.Get(((UserInfo.PetInfo.PetQuality)pt.Level/*cast due to constrained. prefix*/).ToString()));
			}
			break;
		}
		checkBox.radioButtonRoot = base.transform.parent;
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
}

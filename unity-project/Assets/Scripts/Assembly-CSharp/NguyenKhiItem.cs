using UnityEngine;

public class NguyenKhiItem : MonoBehaviour
{
	public int NguyenKhiID;

	public GameObject focusItem;

	public UISprite bgFocusItem;

	public UIButton btnCuongHoa;

	public UILabel lbLoaiHoTro;

	public UILabel lbValueHoTro;

	public UILabel lbUseInfo;

	public UILabel lbNguyenKhiName;

	public NguyenKhiAvatar nguyenKhiAva;

	public UserInfo.NguyenKhiData m_NguyenKhiData;

	public void setData(UserInfo.NguyenKhiData nkData)
	{
		if (nkData == null)
		{
			return;
		}
		m_NguyenKhiData = nkData;
		nguyenKhiAva.Set(nkData);
		OtherCfg.NguyenKhiCfg nguyenKhiCfg = ConfigManager.instance.OtherConfig.NguyenKhiConfig[nkData.Codename];
		if (nkData.HID > 0)
		{
			UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == nkData.HID);
			NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[heroData.Name];
			lbUseInfo.text = nhanVatCfg.TenHienThi + " " + Localization.instance.Get("UsedLabel");
		}
		else
		{
			lbUseInfo.text = string.Empty;
		}
		if (nguyenKhiCfg != null)
		{
			lbNguyenKhiName.text = nguyenKhiCfg.DisplayName;
			if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.DO_DON)
			{
				lbLoaiHoTro.text = Localization.instance.Get("ChiSoCoBanDoDonLabel");
			}
			else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.MENH)
			{
				lbLoaiHoTro.text = Localization.instance.Get("ChiSoCoBanMenhLabel");
			}
			else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.KHI)
			{
				lbLoaiHoTro.text = Localization.instance.Get("ChiSoCoBanKhiLabel");
			}
			else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.KHANG_BAO)
			{
				lbLoaiHoTro.text = Localization.instance.Get("ChiSoCoBanKhangBaoLabel");
			}
			else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.NGOAI)
			{
				lbLoaiHoTro.text = Localization.instance.Get("ChiSoCoBanNgoaiLabel");
			}
			else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.THAN)
			{
				lbLoaiHoTro.text = Localization.instance.Get("ChiSoCoBanThanLabel");
			}
		}
		if (nguyenKhiCfg.baseStat > 0)
		{
			if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.KHANG_BAO || nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.DO_DON || nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.MENH)
			{
				lbValueHoTro.text = m_NguyenKhiData.GetNguyenKhiStats() + "%";
			}
			else
			{
				lbValueHoTro.text = m_NguyenKhiData.GetNguyenKhiStats().ToString();
			}
		}
		else
		{
			lbValueHoTro.text = string.Empty;
		}
	}

	public void OnClick_NguyenKhiAvatar()
	{
		if (m_NguyenKhiData != null)
		{
			PopUpNguyenKhi.CreateByNormalScreen(m_NguyenKhiData);
		}
	}
}

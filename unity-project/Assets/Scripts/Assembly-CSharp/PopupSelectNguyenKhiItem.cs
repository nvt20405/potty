using UnityEngine;

public class PopupSelectNguyenKhiItem : MonoBehaviour
{
	public NguyenKhiAvatar avatar;

	public UILabel nguyenKhiName;

	public UILabel heroName;

	public UILabel lbLoaiHoTro;

	public UserInfo.NguyenKhiData m_Data;

	public void Set(UserInfo.NguyenKhiData data)
	{
		m_Data = data;
		avatar.Set(data);
		OtherCfg.NguyenKhiCfg nguyenKhiCfg = ConfigManager.instance.OtherConfig.NguyenKhiConfig[data.Codename];
		nguyenKhiName.text = nguyenKhiCfg.DisplayName;
		if (data.HID > 0)
		{
			UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == data.HID);
			NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[heroData.Name];
			heroName.text = nhanVatCfg.TenHienThi + " " + Localization.instance.Get("UsedLabel");
		}
		else
		{
			heroName.text = string.Empty;
		}
		UICheckbox componentInChildren = GetComponentInChildren<UICheckbox>();
		componentInChildren.radioButtonRoot = base.transform.parent;
		componentInChildren.optionCanBeNone = true;
		string text = string.Empty;
		if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.DO_DON)
		{
			text = Localization.instance.Get("ChiSoCoBanDoDonLabel") + ":";
		}
		else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.MENH)
		{
			text = Localization.instance.Get("ChiSoCoBanMenhLabel") + ":";
		}
		else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.KHI)
		{
			text = Localization.instance.Get("ChiSoCoBanKhiLabel") + ":";
		}
		else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.KHANG_BAO)
		{
			text = Localization.instance.Get("ChiSoCoBanKhangBaoLabel") + ":";
		}
		else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.NGOAI)
		{
			text = Localization.instance.Get("ChiSoCoBanNgoaiLabel") + ":";
		}
		else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.THAN)
		{
			text = Localization.instance.Get("ChiSoCoBanThanLabel") + ":";
		}
		lbLoaiHoTro.text = ((nguyenKhiCfg.Loai != OtherCfg.NguyenKhiType.DO_DON && nguyenKhiCfg.Loai != OtherCfg.NguyenKhiType.KHANG_BAO && nguyenKhiCfg.Loai != OtherCfg.NguyenKhiType.MENH) ? (text + "[8E0707] " + data.GetNguyenKhiStats()) : (text + "[8E0707] " + data.GetNguyenKhiStats() + "%"));
	}
}

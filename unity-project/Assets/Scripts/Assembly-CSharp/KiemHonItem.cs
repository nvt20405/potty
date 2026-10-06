using UnityEngine;

public class KiemHonItem : MonoBehaviour
{
	public UIButton btnDoi;

	public UILabel lbHoTro;

	public UILabel lbNguyenKhiName;

	public NguyenKhiAvatar nguyenKhiAva;

	public OtherCfg.NguyenKhiCfg m_NguyenKhiData;

	public UILabel lbSoLuongNKDCan;

	public void setData(OtherCfg.NguyenKhiCfg cfg)
	{
		if (cfg != null)
		{
			m_NguyenKhiData = cfg;
			nguyenKhiAva.Set(cfg.Codename, 1);
			lbNguyenKhiName.text = cfg.DisplayName;
			string text = string.Empty;
			if (cfg.Loai == OtherCfg.NguyenKhiType.DO_DON)
			{
				text = Localization.instance.Get("ChiSoCoBanDoDonLabel");
			}
			else if (cfg.Loai == OtherCfg.NguyenKhiType.MENH)
			{
				text = Localization.instance.Get("ChiSoCoBanMenhLabel");
			}
			else if (cfg.Loai == OtherCfg.NguyenKhiType.KHI)
			{
				text = Localization.instance.Get("ChiSoCoBanKhiLabel");
			}
			else if (cfg.Loai == OtherCfg.NguyenKhiType.KHANG_BAO)
			{
				text = Localization.instance.Get("ChiSoCoBanKhangBaoLabel");
			}
			else if (cfg.Loai == OtherCfg.NguyenKhiType.NGOAI)
			{
				text = Localization.instance.Get("ChiSoCoBanNgoaiLabel");
			}
			else if (cfg.Loai == OtherCfg.NguyenKhiType.THAN)
			{
				text = Localization.instance.Get("ChiSoCoBanThanLabel");
			}
			if (cfg.baseStat > 0)
			{
				text = ((cfg.Loai != OtherCfg.NguyenKhiType.DO_DON && cfg.Loai != OtherCfg.NguyenKhiType.KHANG_BAO && cfg.Loai != OtherCfg.NguyenKhiType.MENH) ? (text + "[E99917] + " + cfg.baseStat) : (text + "[E99917] + " + cfg.baseStat + "%"));
			}
			lbHoTro.text = text;
			lbSoLuongNKDCan.text = "X " + cfg.NguyenKhiDanRequired;
		}
		else
		{
			lbHoTro.text = string.Empty;
			lbSoLuongNKDCan.text = string.Empty;
		}
	}
}

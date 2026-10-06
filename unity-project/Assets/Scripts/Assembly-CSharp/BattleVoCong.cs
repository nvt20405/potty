public class BattleVoCong
{
	public CfgVoCong Config { get; set; }

	public VCType Type { get; set; }

	public int Level { get; set; }

	public float CastTime { get; set; }

	public bool HocLom { get; set; }

	public bool Duplicate { get; set; }

	public float damageBuffPer { get; set; }

	public BattleVoCong()
	{
		Type = VCType.VC_NULL;
		Level = 0;
		Config = null;
		HocLom = false;
		Duplicate = false;
	}

	public BattleVoCong(VCType type, int level)
	{
		Type = type;
		Level = level;
		Config = GetConfig(type.ToString());
		CastTime = ((Config != null) ? Config.CastTime : 0f);
		HocLom = false;
		Duplicate = false;
	}

	public float GetTieuHao()
	{
		return (Config != null) ? Config.GetTieuHao(Level) : 0f;
	}

	public float GetSatThuong()
	{
		return (Config != null) ? (Config.GetSatThuong(Level) * (1f + damageBuffPer / 100f)) : 0f;
	}

	public float ChiSo1CoSo()
	{
		return (Config != null) ? Config.ChiSo1CoSo(Level) : 0f;
	}

	public float ChiSo2CoSo()
	{
		return (Config != null) ? Config.ChiSo2CoSo(Level) : 0f;
	}

	public float ChiSo3CoSo()
	{
		return (Config != null) ? Config.ChiSo3CoSo(Level) : 0f;
	}

	public bool IsPause()
	{
		if (Config == null || Config.AutoCast || Config.Hang != 3)
		{
			return false;
		}
		return true;
	}

	private static CfgVoCong GetConfig(string name)
	{
		CfgVoCong value = null;
		if (ConfigManager.instance.m_dicVCs.TryGetValue(name, out value))
		{
			return value;
		}
		return null;
	}

	public ChiSoNhanVat ChiSoTangThem(ChiSoNhanVat chi_so_tham_chieu)
	{
		ChiSoNhanVat chiSoNhanVat = new ChiSoNhanVat();
		switch (Type)
		{
		case VCType.VC_CUU_AM_CHAN_KINH_S:
		case VCType.VC_CUU_AM_CHAN_KINH_A:
		case VCType.VC_CUU_AM_CHAN_KINH_B:
		case VCType.VC_THAN_HANH_BACH_BIEN:
		case VCType.VC_CUU_AM_CHAN_KINH_SS:
			chiSoNhanVat.IAS = chi_so_tham_chieu.IAS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_THAI_CUC_THAN_CONG_S:
		case VCType.VC_THAI_CUC_THAN_CONG_A:
		case VCType.VC_THAI_CUC_THAN_CONG_B:
		case VCType.VC_KIM_CANG_NHAP_THE_S:
		case VCType.VC_KIM_CANG_NHAP_THE_A:
		case VCType.VC_KIM_CANG_NHAP_THE_B:
		case VCType.VC_THAT_TUYET_KINH_S:
		case VCType.VC_THAT_TUYET_KINH_A:
		case VCType.VC_THAT_TUYET_KINH_B:
		case VCType.VC_THAI_CUC_THAN_CONG_SS:
			chiSoNhanVat.Thu = chi_so_tham_chieu.Thu * ChiSo1CoSo() / 100f;
			break;
		case VCType.VC_BAC_MINH_THAN_CONG_S:
		case VCType.VC_BAC_MINH_THAN_CONG_A:
		case VCType.VC_BAC_MINH_THAN_CONG_B:
		case VCType.VC_HON_NGUYEN_CONG_A:
		case VCType.VC_HON_NGUYEN_CONG_S:
		case VCType.VC_HON_NGUYEN_CONG_B:
		case VCType.VC_HON_NGUYEN_CONG_SS:
		case VCType.VC_BAC_MINH_THAN_CONG_SS:
			chiSoNhanVat.MP = chi_so_tham_chieu.MP * ChiSo1CoSo() / 100f;
			break;
		case VCType.VC_THAI_THUONG_THIEN_MA_CONG_S:
		case VCType.VC_THAI_THUONG_THIEN_MA_CONG_A:
		case VCType.VC_THAI_THUONG_THIEN_MA_CONG_B:
		case VCType.VC_NGU_HANH_CONG_PHAP_S:
		case VCType.VC_NGU_HANH_CONG_PHAP_A:
		case VCType.VC_NGU_HANH_CONG_PHAP_B:
		case VCType.VC_TIEU_DAO_TAM_PHAP:
		case VCType.VC_MINH_NGOC_CONG_A:
		case VCType.VC_MINH_NGOC_CONG_S:
		case VCType.VC_MINH_NGOC_CONG_B:
		case VCType.VC_THAI_THUONG_THIEN_MA_CONG_SS:
			chiSoNhanVat.Cong = chi_so_tham_chieu.Cong * ChiSo1CoSo() / 100f;
			break;
		case VCType.VC_THIEN_TAM_CONG_S:
		case VCType.VC_THIEN_TAM_CONG_A:
		case VCType.VC_THIEN_TAM_CONG_B:
		case VCType.VC_THIEN_TAM_CONG_SS:
			chiSoNhanVat.RegMp = chi_so_tham_chieu.RegMp * ChiSo1CoSo() / 100f;
			break;
		case VCType.VC_THIEN_MA_BO_PHAP_S:
		case VCType.VC_THIEN_MA_BO_PHAP_A:
		case VCType.VC_THIEN_MA_BO_PHAP_B:
		case VCType.VC_THIEN_MA_BO_PHAP_SS:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.Cong = chi_so_tham_chieu.Cong * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_THONG_THIEN_BO_PHAP_S:
		case VCType.VC_THONG_THIEN_BO_PHAP_A:
		case VCType.VC_THONG_THIEN_BO_PHAP_B:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.Cong = chi_so_tham_chieu.Cong * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_THIEN_NGUYEN_CHI_THAN_S:
		case VCType.VC_THIEN_NGUYEN_CHI_THAN_A:
		case VCType.VC_THIEN_NGUYEN_CHI_THAN_B:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.Cong = chi_so_tham_chieu.Cong * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_LANG_BA_VI_BO_S:
		case VCType.VC_LANG_BA_VI_BO_A:
		case VCType.VC_LANG_BA_VI_BO_B:
		case VCType.VC_LANG_BA_VI_BO_SS:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			break;
		case VCType.VC_THIEN_CAN_TRUY_S:
		case VCType.VC_THIEN_CAN_TRUY_A:
		case VCType.VC_THIEN_CAN_TRUY_B:
		case VCType.VC_THIEN_CAN_TRUY_SS:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.HP = chi_so_tham_chieu.HP * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_BAT_BO_DANG_KHONG_S:
		case VCType.VC_BAT_BO_DANG_KHONG_A:
		case VCType.VC_BAT_BO_DANG_KHONG_B:
		case VCType.VC_BAT_BO_DANG_KHONG_SS:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.HP = chi_so_tham_chieu.HP * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_THUY_THUONG_PHIEU_S:
		case VCType.VC_THUY_THUONG_PHIEU_A:
		case VCType.VC_THUY_THUONG_PHIEU_B:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.HP = chi_so_tham_chieu.HP * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_THE_VAN_TUNG_S:
		case VCType.VC_THE_VAN_TUNG_A:
		case VCType.VC_THE_VAN_TUNG_B:
		case VCType.VC_THE_VAN_TUNG_SS:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.MP = chi_so_tham_chieu.MP * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_XUYEN_VAN_TUNG_S:
		case VCType.VC_XUYEN_VAN_TUNG_A:
		case VCType.VC_XUYEN_VAN_TUNG_B:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.MP = chi_so_tham_chieu.MP * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_THIEN_TINH_CHI_NGOC_S:
		case VCType.VC_THIEN_TINH_CHI_NGOC_A:
		case VCType.VC_THIEN_TINH_CHI_NGOC_B:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.MP = chi_so_tham_chieu.MP * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_BICH_HO_DU_TUONG_S:
		case VCType.VC_BICH_HO_DU_TUONG_A:
		case VCType.VC_BICH_HO_DU_TUONG_B:
		case VCType.VC_BICH_HO_DU_TUONG_SS:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.Cong = chi_so_tham_chieu.Cong * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_LANG_KHONG_DAP_HU_S:
		case VCType.VC_LANG_KHONG_DAP_HU_A:
		case VCType.VC_LANG_KHONG_DAP_HU_B:
		case VCType.VC_LANG_KHONG_DAP_HU_SS:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.Thu = chi_so_tham_chieu.Thu * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_DAP_TUYET_LUU_HUONG_S:
		case VCType.VC_DAP_TUYET_LUU_HUONG_A:
		case VCType.VC_DAP_TUYET_LUU_HUONG_B:
		case VCType.VC_DAP_TUYET_LUU_HUONG_SS:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.Thu = chi_so_tham_chieu.Thu * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_TRUY_TINH_CAN_NGUYET_S:
		case VCType.VC_TRUY_TINH_CAN_NGUYET_A:
		case VCType.VC_TRUY_TINH_CAN_NGUYET_B:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.Thu = chi_so_tham_chieu.Thu * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_THIEN_PHONG_CHI_THUY_S:
		case VCType.VC_THIEN_PHONG_CHI_THUY_A:
		case VCType.VC_THIEN_PHONG_CHI_THUY_B:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.Thu = chi_so_tham_chieu.Thu * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_NHAN_HANH_CONG_S:
		case VCType.VC_NHAN_HANH_CONG_A:
		case VCType.VC_NHAN_HANH_CONG_B:
		case VCType.VC_PHI_HONG_DAP_TUYET:
		case VCType.VC_KHINH_THAN_THUAT:
		case VCType.VC_TRUY_PHONG_THUAT:
		case VCType.VC_NGU_PHONG_THUAT:
		case VCType.VC_KIEN_BO_CONG:
		case VCType.VC_TRUC_DIEN_TRUY_PHONG_S:
		case VCType.VC_TRUC_DIEN_TRUY_PHONG_A:
		case VCType.VC_TRUC_DIEN_TRUY_PHONG_B:
		case VCType.VC_TRUC_DIEN_TRUY_PHONG_SS:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			break;
		case VCType.VC_THIEN_MA_GIAI_THE_DAI_PHAP_S:
		case VCType.VC_THIEN_MA_GIAI_THE_DAI_PHAP_A:
		case VCType.VC_THIEN_MA_GIAI_THE_DAI_PHAP_B:
			chiSoNhanVat.HP = chi_so_tham_chieu.HP * ChiSo1CoSo() / 100f;
			chiSoNhanVat.RegHp = chi_so_tham_chieu.RegHp * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_TIEN_THIEN_CONG:
		case VCType.VC_HANG_LONG_PHUC_HO_CONG_S:
		case VCType.VC_HANG_LONG_PHUC_HO_CONG_A:
		case VCType.VC_HANG_LONG_PHUC_HO_CONG_B:
			chiSoNhanVat.HP = chi_so_tham_chieu.HP * ChiSo1CoSo() / 100f;
			break;
		case VCType.VC_TU_HA_THAN_CONG:
			chiSoNhanVat.MP = chi_so_tham_chieu.MP * ChiSo1CoSo() / 100f;
			break;
		case VCType.VC_THIEN_DOC_KINH_S:
		case VCType.VC_THIEN_DOC_KINH_A:
		case VCType.VC_THIEN_DOC_KINH_B:
			chiSoNhanVat.IAS = chi_so_tham_chieu.IAS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.MP = chi_so_tham_chieu.MP * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_HAP_MA_CHIEU_HON_S:
		case VCType.VC_HAP_MA_CHIEU_HON_A:
		case VCType.VC_HAP_MA_CHIEU_HON_B:
		case VCType.VC_THUAN_DUONG_VO_CUC_S:
		case VCType.VC_THUAN_DUONG_VO_CUC_A:
		case VCType.VC_THUAN_DUONG_VO_CUC_B:
		case VCType.VC_THUAN_DUONG_VO_CUC_SS:
		case VCType.VC_HAP_MA_CHIEU_HON_SS:
			chiSoNhanVat.MP = chi_so_tham_chieu.MP * ChiSo1CoSo() / 100f;
			break;
		case VCType.VC_THIEN_HANH_KHI_CONG_S:
		case VCType.VC_THIEN_HANH_KHI_CONG_A:
		case VCType.VC_THIEN_HANH_KHI_CONG_B:
		case VCType.VC_THIEN_HANH_KHI_CONG_SS:
			chiSoNhanVat.HP = chi_so_tham_chieu.HP * ChiSo1CoSo() / 100f;
			chiSoNhanVat.RegHp = chi_so_tham_chieu.RegHp * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_CAN_KHON_DAI_NA_DI_S:
		case VCType.VC_CAN_KHON_DAI_NA_DI_A:
		case VCType.VC_CAN_KHON_DAI_NA_DI_B:
		case VCType.VC_CAN_KHON_DAI_NA_DI_SS:
			chiSoNhanVat.HP = chi_so_tham_chieu.HP * ChiSo1CoSo() / 100f;
			break;
		case VCType.VC_CUU_DUONG_THAN_CONG_S:
		case VCType.VC_CUU_DUONG_THAN_CONG_A:
		case VCType.VC_CUU_DUONG_THAN_CONG_B:
		case VCType.VC_CUU_DUONG_THAN_CONG_SS:
			chiSoNhanVat.Thu = chi_so_tham_chieu.Thu * ChiSo1CoSo() / 100f;
			break;
		case VCType.VC_LUC_HOP_KINH_S:
		case VCType.VC_LUC_HOP_KINH_A:
		case VCType.VC_LUC_HOP_KINH_B:
		case VCType.VC_LUC_HOP_KINH_SS:
			chiSoNhanVat.Cong = chi_so_tham_chieu.Cong * ChiSo1CoSo() / 100f;
			break;
		case VCType.VC_LAC_TUYET_BO_PHAP_S:
		case VCType.VC_LAC_TUYET_BO_PHAP_A:
		case VCType.VC_LAC_TUYET_BO_PHAP_B:
		case VCType.VC_LAC_TUYET_BO_PHAP_SS:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.MP = chi_so_tham_chieu.MP * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_LAM_TUOC_VI_S:
		case VCType.VC_LAM_TUOC_VI_A:
		case VCType.VC_LAM_TUOC_VI_B:
		case VCType.VC_PHONG_SUONG_TUYET_ANH_S:
		case VCType.VC_PHONG_SUONG_TUYET_ANH_A:
		case VCType.VC_PHONG_SUONG_TUYET_ANH_B:
		case VCType.VC_PHONG_SUONG_TUYET_ANH_SS:
		case VCType.VC_LAM_TUOC_VI_SS:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.HP = chi_so_tham_chieu.HP * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_HOI_PHONG_DOAT_NGUYET_S:
		case VCType.VC_HOI_PHONG_DOAT_NGUYET_A:
		case VCType.VC_HOI_PHONG_DOAT_NGUYET_B:
		case VCType.VC_BO_PHONG_TROC_ANH_S:
		case VCType.VC_BO_PHONG_TROC_ANH_A:
		case VCType.VC_BO_PHONG_TROC_ANH_B:
		case VCType.VC_BO_PHONG_TROC_ANH_SS:
		case VCType.VC_HOI_PHONG_DOAT_NGUYET_SS:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.Cong = chi_so_tham_chieu.Cong * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_LUU_QUANG_CHUYEN_THE_S:
		case VCType.VC_LUU_QUANG_CHUYEN_THE_A:
		case VCType.VC_LUU_QUANG_CHUYEN_THE_B:
		case VCType.VC_LUU_QUANG_CHUYEN_THE_SS:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.Thu = chi_so_tham_chieu.Thu * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_THIEN_MA_HANH_KHONG_S:
		case VCType.VC_THIEN_MA_HANH_KHONG_A:
		case VCType.VC_THIEN_MA_HANH_KHONG_B:
		case VCType.VC_THIEN_MA_HANH_KHONG_SS:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.HP = chi_so_tham_chieu.HP * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_DI_BO_HOAN_HINH_S:
		case VCType.VC_DI_BO_HOAN_HINH_A:
		case VCType.VC_DI_BO_HOAN_HINH_B:
		case VCType.VC_DI_BO_HOAN_HINH_SS:
			chiSoNhanVat.MS = chi_so_tham_chieu.MS * ChiSo1CoSo() / 100f;
			chiSoNhanVat.Cong = chi_so_tham_chieu.Cong * ChiSo2CoSo() / 100f;
			break;
		case VCType.VC_HOANG_KIM_GIAP_S:
		case VCType.VC_HOANG_KIM_GIAP_A:
		case VCType.VC_HOANG_KIM_GIAP_B:
		case VCType.VC_HOANG_KIM_GIAP_SS:
			chiSoNhanVat.Cong = chi_so_tham_chieu.Cong * ChiSo1CoSo() / 100f;
			chiSoNhanVat.Thu = chi_so_tham_chieu.Thu * ChiSo2CoSo() / 100f;
			break;
		}
		return chiSoNhanVat;
	}
}

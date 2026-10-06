public class VCEffect
{
	public enum VCEffectType
	{
		EFF_NULL = 0,
		EFF_CHOANG = 1,
		EFF_DINH_THAN = 2,
		EFF_XUAT_HUYET = 3,
		EFF_TRUNG_DOC = 4,
		EFF_PHONG_CHIEU = 5,
		EFF_SUY_NHUOC = 6,
		EFF_TRIET_PHONG = 7,
		EFF_TAU_HOA = 8,
		EFF_HON_ME = 9,
		EFF_PHONG_THAN_PHAP = 10,
		EFF_GIAM_CHINH_XAC = 11,
		EFF_PHONG_KINH = 12,
		EFF_PHE_THU_PHAP = 13,
		EFF_PHE_BO_PHAP = 14,
		EFF_THU_HUT = 15,
		EFF_TANG_CONG = 16,
		EFF_PHUC_HOI_HP = 17,
		EFF_PHUC_HOI_MP = 18,
		EFF_BACH_COT_PHU_THE = 19,
		EFF_THAI_CUC_KHI = 20,
		EFF_BAC_MINH_CONG = 21,
		EFF_SAT_KHI = 22,
		EFF_THIEN_TAM_BIEN = 23,
		EFF_THIEN_MA_KHI = 24,
		EFF_LANG_BA = 25,
		EFF_LANG_KHONG = 26,
		EFF_THIEN_CAN = 27,
		EFF_TUNG_HOANH = 28,
		EFF_DAP_THUY = 29,
		EFF_BICH_HO = 30,
		EFF_DAP_TUYET = 31,
		EFF_THONG_THIEN = 32,
		EFF_DANG_KHONG = 33,
		EFF_CAN_NGUYET = 34,
		EFF_XUYEN_VAN = 35,
		EFF_NHAN_HANH = 36,
		EFF_BA_THE = 37,
		EFF_THUAN_BO = 38,
		EFF_HO_THE = 39,
		EFF_THIEN_PHONG = 40,
		EFF_THIEN_TINH = 41,
		EFF_THIEN_NGUYEN = 42,
		EFF_AM_HAN = 43,
		EFF_DOAT_MENH = 44,
		EFF_KHANG_THE = 45,
		EFF_QUY_HOA_CHAN_KHI = 46,
		EFF_HAP_TINH = 47,
		EFF_VO_TUONG = 48,
		EFF_BANG_CO_NGOC_COT = 49,
		EFF_CUU_DUONG_CHAN_KHI = 50,
		EFF_TAM_MA = 51,
		EFF_DIEM_HUYET = 52,
		EFF_TANG_TOC = 53,
		EFF_CUONG_HOA = 54,
		EFF_BO_PHONG = 55,
		EFF_HOAN_HINH = 56,
		EFF_TRUC_DIEN = 57,
		EFF_PHONG_SUONG = 58,
		EFF_PHE_NOI_CONG = 59,
		EFF_KHANG_SAT_THUONG_VC = 60,
		EFF_TANG_THU = 61,
		EFF_SAT_THAN = 62,
		EFF_HOA_HUYET_THAN_CONG = 63,
		EFF_PHONG_THAN_THOAI = 64,
		EFF_BAI_VAN = 65,
		EFF_VAN_KIEM = 66,
		EFF_COOLDOWN_TAM_PHAN = 67,
		EFF_COOLDOWN_TAM_PHAN_HK = 68,
		EFF_THANH_LINH_KIEM = 69,
		EFF_KIM_THAN_BAT_DIET = 70,
		EFF_THIEN_DIA_VO_TINH = 71,
		EFF_PHONG_HOA_TUYET_NGUYET = 72
	}

	public VCType VC { get; set; }

	public int ID { get; set; }

	public VCEffectType Type { get; set; }

	public float Time { get; set; }

	public int StackCount { get; set; }

	public float Parameter_1 { get; set; }

	public float Parameter_2 { get; set; }

	public float Parameter_3 { get; set; }

	public float Parameter_4 { get; set; }

	public float Parameter_5 { get; set; }

	public float Parameter_6 { get; set; }

	public bool Trigger { get; set; }

	public float Misc { get; set; }

	public bool MarkAsRemove { get; set; }

	public VCEffect()
	{
	}

	public VCEffect(int _ID, VCType VC, VCEffectType type, float time, int stackCnt, float param_1 = 0f, float param_2 = 0f, float param_3 = 0f, float param_4 = 0f, float param_5 = 0f, float param_6 = 0f)
	{
		ID = _ID;
		Type = type;
		Time = time;
		StackCount = stackCnt;
		Parameter_1 = param_1;
		Parameter_2 = param_2;
		Parameter_3 = param_3;
		Parameter_4 = param_4;
		Parameter_5 = param_5;
		Parameter_6 = param_6;
		Trigger = false;
		Misc = 0f;
		MarkAsRemove = false;
	}
}

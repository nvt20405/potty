public class TonHieuInfo
{
	public UserInfo.GamerData.TonHieuType TonHieuType { get; set; }

	public string DisplayName { get; set; }

	public string Description { get; set; }

	public static string getStringCheck(UserInfo.GamerData.TonHieuType TonHieuType, int TOP)
	{
		switch (TonHieuType)
		{
		case UserInfo.GamerData.TonHieuType.CHIEN_TRUONG:
			return "TOP" + TOP + "_ChienTruong";
		case UserInfo.GamerData.TonHieuType.TINH_LUYEN:
			return "TOP" + TOP + "_TinhLuyen";
		case UserInfo.GamerData.TonHieuType.CONG_LUC:
			return "TOP" + TOP + "_CongLuc";
		case UserInfo.GamerData.TonHieuType.LUAN_KIEM:
			return "TOP" + TOP + "_LuanKiem";
		case UserInfo.GamerData.TonHieuType.HOANG_KIM:
			return "TOP" + TOP + "_HoangKim";
		case UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH:
			return "TOP" + TOP + "_QMD";
		case UserInfo.GamerData.TonHieuType.THAN_THU:
			return "TOP" + TOP + "_ThanThu";
		case UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM:
			return "TOP" + TOP + "_DHVL";
		case UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG:
			return "TOP" + TOP + "_ThienMa";
		case UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM:
			return "TOP" + TOP + "_TBHK";
		default:
			return string.Empty;
		}
	}
}

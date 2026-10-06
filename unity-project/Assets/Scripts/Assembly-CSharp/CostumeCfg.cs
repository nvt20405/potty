public class CostumeCfg
{
	public class CostumeStat
	{
		public string PhoiGiap { get; set; }

		public string Desc { get; set; }

		public float EffMenh { get; set; }

		public float EffNgoai { get; set; }

		public float EffThan { get; set; }

		public float EffKhi { get; set; }

		public float Ne { get; set; }

		public float Bao { get; set; }

		public float KhangChuong { get; set; }

		public float DoDon { get; set; }

		public float PhanTramChoang { get; set; }

		public float TimeChoang { get; set; }

		public float PhanTramPhongChieu { get; set; }

		public float TimePhongChieu { get; set; }

		public float PhanTramDinhThan { get; set; }

		public float TimeDinhThan { get; set; }

		public float PhanTramPheThuPhap { get; set; }

		public float TimePheThuPhap { get; set; }

		public float PhanTramPheBoPhap { get; set; }

		public float TimePheBoPhap { get; set; }

		public float PhanTramPheThu { get; set; }

		public float TimePheThu { get; set; }

		public float PhanTramPheCong { get; set; }

		public float TimePheCong { get; set; }

		public float PhanTramDoc { get; set; }

		public float TimeDoc { get; set; }

		public float PhanTramXuatHuyet { get; set; }

		public float TimeXuatHuyet { get; set; }
	}

	public string TenHienThi { get; set; }

	public string BundleLinkAndroid { get; set; }

	public string BundleLinkIPhone { get; set; }

	public string BundleLinkWP8 { get; set; }

	public string BundleLinkPC { get; set; }

	public string NhanVat { get; set; }

	public CostumeStat Stats_0 { get; set; }

	public CostumeStat Stats_1 { get; set; }

	public CostumeStat Stats_2 { get; set; }

	public CostumeStat Stats_3 { get; set; }

	public static int GetCostumesVersion()
	{
		return 1;
	}

	public bool IsValid()
	{
		return Stats_0 != null && Stats_1 != null && Stats_2 != null && Stats_3 != null && !string.IsNullOrEmpty(BundleLinkPC) && !string.IsNullOrEmpty(BundleLinkWP8) && !string.IsNullOrEmpty(BundleLinkIPhone) && !string.IsNullOrEmpty(BundleLinkAndroid) && ConfigManager.instance.m_dicNhanVats.ContainsKey(NhanVat);
	}

	public static int GetBuffFromNgoc(UserInfo.TrangBiData.LoaiNgoc loai, int lvl)
	{
		switch (loai)
		{
		case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO:
			return ConfigManager.instance.OtherConfig.GetSoLuongNgocCan(lvl);
		case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_XANH:
			return ConfigManager.instance.OtherConfig.GetSoLuongNgocCan(lvl);
		case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_VANG:
			return ConfigManager.instance.OtherConfig.GetSoLuongNgocCan(lvl) * 15;
		case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_TIM:
			return ConfigManager.instance.OtherConfig.GetSoLuongNgocCan(lvl) * 15;
		default:
			return 0;
		}
	}

	public CostumeStat GetStatFromTinhLuyen(int tinhLuyen)
	{
		switch (tinhLuyen)
		{
		case 0:
			return Stats_0;
		case 1:
			return Stats_1;
		case 2:
			return Stats_2;
		case 3:
			return Stats_3;
		default:
			return null;
		}
	}
}

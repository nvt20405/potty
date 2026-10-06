public class ChiSoNhanVat : ChiSoCoSo
{
	public static double MaxTyleNe = 75.0;

	public static double MaxTyleCrit = 75.0;

	public static double MaxTyleDo = 75.0;

	public float TimeChoangVuKhiEff = 1f;

	public float TimePhongChieuVuKhiEff = 1f;

	public float TimeDinhThanVuKhiEff = 1.5f;

	public float TimeBaTheAoEff = 0.2f;

	public float XacSuatHoTheAoEff = 0.15f;

	public float TimeHoTheAoEff = 1.5f;

	public float XacSuatBurnManaMuEff = 0.25f;

	public float ThuCuoiXacSuatNe { get; set; }

	public float ThuCuoiXacSuatDoDon { get; set; }

	public float ThuCuoiXacSuatBao { get; set; }

	public float ThanThuXacSuatNe { get; set; }

	public float TyleNe { get; set; }

	public float TyleCrit { get; set; }

	public float HesoCrit { get; set; }

	public float DuyenTyleDoDon { get; set; }

	public float DuyenHesoDoDon { get; set; }

	public float ChiSoBuffTocDanh { get; set; }

	public float ChiSoBuffKhangChuong { get; set; }

	public float ChiSoBuffHoaGiai { get; set; }

	public float ChiSoBuffBaoKich { get; set; }

	public float NguyenKhiHesoDoDon { get; set; }

	public float NguyenKhiHesoKhangBao { get; set; }

	public float TyLeNeChienHon { get; set; }

	public float TyLeBaoChienHon { get; set; }

	public float TyLeDoChienHon { get; set; }

	public float TiLeBaoKichCostume { get; set; }

	public float PhanTramDoDonCostume { get; set; }

	public float PhanTramNeCostume { get; set; }

	public float TiLeKhangChuongCostume { get; set; }

	public float PhanTramChoangCostume { get; set; }

	public float ThoiGianChoangCostume { get; set; }

	public float PhanTramPhongChieuCostume { get; set; }

	public float ThoiGianPhongChieuCostume { get; set; }

	public float PhanTramDinhThanCostume { get; set; }

	public float ThoiGianDinhThanCostume { get; set; }

	public float PhanTramPheThuPhapCostume { get; set; }

	public float ThoiGianPheThuPhapCostume { get; set; }

	public float PhanTramPheCongCostume { get; set; }

	public float ThoiGianPheCongCostume { get; set; }

	public float PhanTramPheThuCostume { get; set; }

	public float ThoiGianPheThuCostume { get; set; }

	public float PhanTramXuatHuyetCostume { get; set; }

	public float ThoiGianXuatHuyetCostume { get; set; }

	public float ChinhXacTrangBiEff { get; set; }

	public float KhangBaoTrangBiEff { get; set; }

	public float XacSuatChoangVuKhiEff { get; set; }

	public float XacSuatPhongChieuVuKhiEff { get; set; }

	public float XacSuatDinhThanVuKhiEff { get; set; }

	public float XacSuatBaTheAoEff { get; set; }

	public float RateHoTheAoEff { get; set; }

	public float TiLeHutMauVuKhiEff { get; set; }

	public float XacSuatHoiSinhTSEff { get; set; }

	public float HeSoBurnManaMuEff { get; set; }

	public void Add(ChiSoNhanVat chisonv)
	{
		Add((ChiSoCoSo)chisonv);
		TyleNe += chisonv.TyleNe;
		TyleCrit += chisonv.TyleCrit;
		HesoCrit += chisonv.HesoCrit;
		DuyenTyleDoDon += chisonv.DuyenTyleDoDon;
		DuyenHesoDoDon += chisonv.DuyenHesoDoDon;
		ChiSoBuffTocDanh += chisonv.ChiSoBuffTocDanh;
		ChiSoBuffKhangChuong += chisonv.ChiSoBuffKhangChuong;
		ChiSoBuffHoaGiai += chisonv.ChiSoBuffHoaGiai;
		ChiSoBuffBaoKich += chisonv.ChiSoBuffBaoKich;
		NguyenKhiHesoDoDon += chisonv.NguyenKhiHesoDoDon;
		NguyenKhiHesoKhangBao += chisonv.NguyenKhiHesoKhangBao;
	}
}

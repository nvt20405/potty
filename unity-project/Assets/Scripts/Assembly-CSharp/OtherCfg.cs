using System;
using System.Collections.Generic;

public class OtherCfg
{
	public class HoatDongDailyCfg
	{
		public class HoatDongDaily
		{
			public string Title;

			public string Desc;

			public string KeyLog;

			public int LevelReq;

			public int HoatDongType;

			public TypeHoatDong HoatDong
			{
				get
				{
					return (TypeHoatDong)HoatDongType;
				}
				set
				{
					HoatDongType = (int)value;
				}
			}
		}

		public enum TypeHoatDong
		{
			GIANG_HO = 0,
			HAC_MOC_NHAI = 1,
			CAM_DIA = 2,
			LINH_DUOC = 3,
			THAN_THU = 4,
			QMD = 5,
			DOAN_THIEN = 6,
			BAT_COC = 7,
			THACH_DAU = 8,
			BOI_DUONG_DE_TU = 9,
			THAM_NGO_VC = 10,
			CUONG_HOA_TB = 11,
			DANH_BAC = 12,
			UONG_RUOU = 13
		}

		public List<HoatDongDaily> ListHoatDong;

		public List<PhanThuongResponse.PhanThuong> ListPhanThuong;
	}

	public class ThanBinhCfg
	{
		public List<int> BaseCost;

		public Dictionary<string, float> MinValue;

		public Dictionary<string, float> MaxValue;

		public int DungLuyenReq;

		public int TayLuyenReq;

		public int KhaiQuangReq;
	}

	public class ChuyenSinhCfg
	{
		public int LevelRequired { get; set; }

		public int DotPhaRequired { get; set; }

		public int HeSoDuyen { get; set; }

		public int LenhBaiChuyenSinhRequired { get; set; }

		public int ChuyenSinhDanRequired { get; set; }
	}

	public class BangChienConfig
	{
		public int DoBenCongThanhNho { get; set; }

		public int DoBenCongThanhTo { get; set; }

		public List<string> ListThanhChien { get; set; }

		public List<DayOfWeek> ListNgayBangChien { get; set; }

		public string GioBangChien { get; set; }

		public int ThoiGianDienRa { get; set; }

		public int ThoiGianHoiSinhCongThanh { get; set; }

		public int ThoiGianHoiSinhThuThanh { get; set; }

		public int ThoiGianCongThanh { get; set; }

		public int ThoiGianGetPhanThuong { get; set; }

		public int ThoiGianDenThuThanh { get; set; }

		public DayOfWeek GetNgayBangChien(int cum)
		{
			if (cum > 0 && cum < ListNgayBangChien.Count)
			{
				return ListNgayBangChien[cum];
			}
			return DayOfWeek.Thursday;
		}

		public TimeSpan GetThoiGianDienRaBangChien()
		{
			return TimeSpan.FromMinutes(ThoiGianDienRa);
		}

		public TimeSpan GetThoiGianHoiSinhCongThanh()
		{
			return TimeSpan.FromSeconds(ThoiGianHoiSinhCongThanh);
		}

		public TimeSpan GetThoiGianHoiSinhThuThanh()
		{
			return TimeSpan.FromSeconds(ThoiGianHoiSinhThuThanh);
		}

		public TimeSpan GetThoiGianCongThanh()
		{
			return TimeSpan.FromSeconds(ThoiGianCongThanh);
		}

		public TimeSpan GetThoiGianGetPhanThuong()
		{
			return TimeSpan.FromSeconds(ThoiGianGetPhanThuong);
		}

		public TimeSpan GetThoiGianDenThuThanh()
		{
			return TimeSpan.FromSeconds(ThoiGianDenThuThanh);
		}
	}

	public class LoginConfig
	{
		public int version { get; set; }

		public string androidUrl { get; set; }

		public string appstoreUrl { get; set; }

		public string jbUrl { get; set; }

		public string cfgAndroidUrl { get; set; }

		public uint cfgAndroidCRC { get; set; }

		public string cfgIOSUrl { get; set; }

		public uint cfgIOSCRC { get; set; }

		public string cfgWindowUrl { get; set; }

		public uint cfgWindowCRC { get; set; }

		public int cfgVer { get; set; }

		public string WPUrl { get; set; }

		public string cfgWPUrl { get; set; }

		public uint cfgWPCRC { get; set; }
	}

	public class TinhLuyenCfg
	{
		public List<int> SoManh = new List<int>();

		public int SoLoai { get; set; }

		public int SoTayTuyDan { get; set; }
	}

	public enum NguyenKhiType
	{
		MENH = 0,
		NGOAI = 1,
		THAN = 2,
		KHI = 3,
		DO_DON = 4,
		KHANG_BAO = 5
	}

	public class NguyenKhiCfg
	{
		public string Codename;

		public string DisplayName;

		public NguyenKhiType Loai;

		public int baseStat;

		public float growStat;

		public string MoTa;

		public int NguyenKhiDanRequired;

		public string MaxLevelDesc;
	}

	public class ThuCuoiCfg
	{
		public string CodeName;

		public int MenhBuff;

		public int NgoaiBuff;

		public int ThanBuff;

		public int KhiBuff;

		public int BaoBuff;

		public int NeBuff;

		public int DoDonBuff;

		public int MenhBuffRate;

		public int NgoaiBuffRate;

		public int ThanBuffRate;

		public int KhiBuffRate;

		public string DisplayName;

		public string MoTa;

		public int Loai;

		public int KNB;

		public int ReActiveTime;
	}

	public enum BatQuaiTranDoType
	{
		NONE = 0,
		THANH_LONG = 1,
		BACH_HO = 2,
		HUYEN_VU = 3,
		CHU_TUOC = 4
	}

	public class BatQuaiCuongHoaCfg
	{
		public string CodeName;

		public string DisplayName;

		public int KNBCanDoi;

		public List<string> ListSlot;

		public List<int> ChiSoSlot1;

		public List<int> ChiSoSlot2;

		public List<int> ChiSoSlot3;

		public List<int> ChiSoSlot4;

		public List<int> ChiSoSlot5;

		public List<int> ChiSoSlot6;

		public List<int> ChiSoSlot7;

		public List<int> ChiSoSlot8;
	}

	public class BaoKhoCfg
	{
		public string CodeName;

		public int BacNhanDuoc;

		public int NguyenKhiDanNhanDuoc;
	}

	public class TuiThanConfig
	{
		public string CodeName { get; set; }

		public List<PhanThuongResponse.PhanThuong> PhanThuongList { get; set; }
	}

	public class HelpChildItem
	{
		public string Name { get; set; }

		public string URL { get; set; }
	}

	public class HelpItem
	{
		public List<HelpChildItem> Children = new List<HelpChildItem>();

		public string Name { get; set; }
	}

	public class DangNhapNhanThuongCfg
	{
		public List<PhanThuongResponse.PhanThuong> PhanThuong { get; set; }
	}

	public class CuuVienTieuPhongCfg
	{
		public PhanThuongResponse.PhanThuong PhanThuong;

		public int SoDeTuGiap { get; set; }

		public int SoTrangBiGiap { get; set; }
	}

	public class LenCapNhanThuongCfg
	{
		public int CapYeuCau { get; set; }

		public List<PhanThuongResponse.PhanThuong> PhanThuong { get; set; }
	}

	private List<long> _ChienHonExpLevelCfg = new List<long>
	{
		50000L, 50000L, 50000L, 50000L, 50000L, 50000L, 50000L, 50000L, 50000L, 50000L,
		50000L, 50000L, 50000L, 50000L, 50000L, 50000L, 50000L, 50000L, 50000L, 50000L,
		500000L, 50000000L
	};

	private int _soHonTrieuHoiChienHon = 60;

	private int _tienLinhDanExp = 15000;

	private List<int> _ChienHonDotPhaChiSoCfg = new List<int> { 0, 15, 30, 45 };

	private List<int> _ChienHonDotPhaTanHonNeedCfg = new List<int> { 60, 90, 120 };

	private int _ChienHonDotPhaChienThanDanNeed = 100;

	private int _HuyenNguyenDanNeedChangeBuff = 100;

	private int _huyenKhiNeedCheTac = 100;

	private int _levelChienHonUnlockHK1 = 10;

	private int _levelChienHonUnlockHK2 = 20;

	public List<int> ThienMaExpReq = new List<int>();

	public HoatDongDailyCfg HoatDongDailyConfig;

	public ThanBinhCfg ThanBinhConfig;

	public ChuyenSinhCfg ChuyenSinhConfig;

	public int NguyenKhiDanTruongThanh;

	public int BacTruongThanh;

	public int KNBThonPhe;

	public long BacThonPhe;

	public int NKDThonPhe;

	public Dictionary<string, NguyenKhiCfg> NguyenKhiConfig;

	public Dictionary<string, ThuCuoiCfg> ThuCuoiConfig;

	public Dictionary<string, BatQuaiCuongHoaCfg> BatQuaiConfig;

	public Dictionary<string, BaoKhoCfg> BaoKhoConfig;

	private float _TimeNotifyPos = 0.15f;

	public List<HelpItem> HelpMenu = new List<HelpItem>();

	private List<float> nhanVatMaxExp;

	private List<int> monPhaiMaxExp;

	private List<int> monPhaiGetExp;

	private List<int> nhanVatGetExp;

	private List<int> vuKhiGiaNangCapLevel1;

	private List<int> aoGiapGiaNangCapLevel1;

	private List<int> trangSucGiaNangCapLevel1;

	private List<int> muGiaNangCapLevel1;

	private List<float> trangBiHeSoNangCap;

	private List<int> voCongHang1NangCapHeSo;

	private List<int> voCongHang2NangCapHeSo;

	private List<int> voCongHang3NangCapHeSo;

	private List<string> tenHienThiTienTo;

	private List<string> tenHienThiHauTo;

	private List<string> botTienTo;

	public List<long> ChienHonExpLevelCfg
	{
		get
		{
			return _ChienHonExpLevelCfg;
		}
		set
		{
			_ChienHonExpLevelCfg = value;
		}
	}

	public int SoHonTrieuHoiChienHon
	{
		get
		{
			return _soHonTrieuHoiChienHon;
		}
		set
		{
			_soHonTrieuHoiChienHon = value;
		}
	}

	public int TienLinhDanExp
	{
		get
		{
			return _tienLinhDanExp;
		}
		set
		{
			_tienLinhDanExp = value;
		}
	}

	public List<int> ChienHonDotPhaChiSoCfg
	{
		get
		{
			return _ChienHonDotPhaChiSoCfg;
		}
		set
		{
			_ChienHonDotPhaChiSoCfg = value;
		}
	}

	public List<int> ChienHonDotPhaTanHonNeedCfg
	{
		get
		{
			return _ChienHonDotPhaTanHonNeedCfg;
		}
		set
		{
			_ChienHonDotPhaTanHonNeedCfg = value;
		}
	}

	public int ChienHonDotPhaChienThanDanNeed
	{
		get
		{
			return _ChienHonDotPhaChienThanDanNeed;
		}
		set
		{
			_ChienHonDotPhaChienThanDanNeed = value;
		}
	}

	public int HuyenNguyenDanNeedChangeBuff
	{
		get
		{
			return _HuyenNguyenDanNeedChangeBuff;
		}
		set
		{
			_HuyenNguyenDanNeedChangeBuff = value;
		}
	}

	public int HuyenKhiNeedCheTac
	{
		get
		{
			return _huyenKhiNeedCheTac;
		}
		set
		{
			_huyenKhiNeedCheTac = value;
		}
	}

	public int LevelChienHonUnlockHK1
	{
		get
		{
			return _levelChienHonUnlockHK1;
		}
		set
		{
			_levelChienHonUnlockHK1 = value;
		}
	}

	public int LevelChienHonUnlockHK2
	{
		get
		{
			return _levelChienHonUnlockHK2;
		}
		set
		{
			_levelChienHonUnlockHK2 = value;
		}
	}

	public int NgaoGHMaxServer { get; set; }

	public string CostumeAtlasBundleAndroid { get; set; }

	public string CostumeAtlasBundleIPhone { get; set; }

	public string CostumeAtlasBundlePC { get; set; }

	public string CostumeAtlasBundleWP8 { get; set; }

	public string ShaderListBundleAndroid { get; set; }

	public string ShaderListBundleIPhone { get; set; }

	public string ShaderListBundlePC { get; set; }

	public string ShaderListBundleWP8 { get; set; }

	public int NumToLua1 { get; set; }

	public int NumToLua2 { get; set; }

	public int NumToLua3 { get; set; }

	public int NumToLua0 { get; set; }

	public int MaxLvlNgoc1 { get; set; }

	public int MaxLvlNgoc2 { get; set; }

	public int MaxLvlNgoc3 { get; set; }

	public int MaxLvlNgoc4 { get; set; }

	public int CostDatCuocLienDau { get; set; }

	public int LevelLeague { get; set; }

	public int TanSuatUpdateViTriPlayerBanhChung { get; set; }

	public int TanSuatUpdateViTriOtherBanhChung { get; set; }

	public BangChienConfig BangChien { get; set; }

	public LoginConfig LoginCfg { get; set; }

	public float CT2TimeNotifyPos
	{
		get
		{
			return _TimeNotifyPos;
		}
		set
		{
			_TimeNotifyPos = value;
		}
	}

	public int ThoiGianGetExpLuaTrai { get; set; }

	public int ThoiGianDotLuaTrai { get; set; }

	public List<float> NgocBuffTocDoDanh { get; set; }

	public List<float> NgocBuffKhangChuong { get; set; }

	public List<float> NgocBuffHoaGiai { get; set; }

	public List<float> NgocBuffBaoKich { get; set; }

	public List<float> NgocBuffChiSo { get; set; }

	public List<int> NgocSoLuongCan { get; set; }

	public TinhLuyenCfg TinhLuyenBinh1 { get; set; }

	public TinhLuyenCfg TinhLuyenBinh2 { get; set; }

	public TinhLuyenCfg TinhLuyenBinh3 { get; set; }

	public TinhLuyenCfg TinhLuyenAt1 { get; set; }

	public TinhLuyenCfg TinhLuyenAt2 { get; set; }

	public TinhLuyenCfg TinhLuyenAt3 { get; set; }

	public TinhLuyenCfg TinhLuyenGiap1 { get; set; }

	public TinhLuyenCfg TinhLuyenGiap2 { get; set; }

	public TinhLuyenCfg TinhLuyenGiap3 { get; set; }

	public TinhLuyenCfg TinhLuyenBinh4 { get; set; }

	public TinhLuyenCfg TinhLuyenBinh5 { get; set; }

	public TinhLuyenCfg TinhLuyenAt4 { get; set; }

	public TinhLuyenCfg TinhLuyenAt5 { get; set; }

	public TinhLuyenCfg TinhLuyenGiap4 { get; set; }

	public TinhLuyenCfg TinhLuyenGiap5 { get; set; }

	public Dictionary<string, List<string>> HopThanList { get; set; }

	public List<TuiThanConfig> TuiThanList { get; set; }

	public string CaoNhanThapAvatar { get; set; }

	public string CaoNhanTrungAvatar { get; set; }

	public string CaoNhanCaoAvatar { get; set; }

	public string CaoNhanSieuCapAvatar { get; set; }

	public string TyThiAvatar { get; set; }

	public string ThuongNhanAvatar { get; set; }

	public int MaxAvatar3DInHome { get; set; }

	public int CuocThachDau1 { get; set; }

	public int CuocThachDau2 { get; set; }

	public int CuocThachDau3 { get; set; }

	public int MaxLuotTangTheLuc { get; set; }

	public int MaxLuotDuocTangTheLuc { get; set; }

	public int MaxLuotThuongGiupBanDanhSon { get; set; }

	public int GiaDoiRuou0 { get; set; }

	public int GiaDoiRuou1 { get; set; }

	public int GiaDoiRuou2 { get; set; }

	public int GiaMoThuongDanhSonLan1 { get; set; }

	public int GiaMoThuongDanhSonLan2 { get; set; }

	public int GiaMoThuongDanhSonLan3 { get; set; }

	public int GiaMoThuongDanhSonLan4 { get; set; }

	public int GiaMoThuongDanhSonLan5 { get; set; }

	public int GiaMoThuongDanhSonLan6 { get; set; }

	public int GiaMoTatCaDanhSon { get; set; }

	public int GiaResetDanhSon { get; set; }

	public List<PhanThuongResponse.PhanThuong> ThuongNhapMaGioiThieu { get; set; }

	public List<PhanThuongResponse.PhanThuong> ThuongNapKnbLanDau { get; set; }

	public List<DangNhapNhanThuongCfg> DangNhapNhanThuong { get; set; }

	public List<CuuVienTieuPhongCfg> CuuVienTieuPhong { get; set; }

	public List<LenCapNhanThuongCfg> LenCapNhanThuong { get; set; }

	public List<LenCapNhanThuongCfg> GuiTietKiem { get; set; }

	public List<LenCapNhanThuongCfg> ThanhVien1MilConfig { get; set; }

	public int GiaLayDeTuLoai1 { get; set; }

	public int GiaLayDeTuLoai2 { get; set; }

	public int GiaLayDeTuLoai3 { get; set; }

	public int DongNhanTimeUpLevel { get; set; }

	public List<int> VoCongHang1NangCapHeSo
	{
		get
		{
			return voCongHang1NangCapHeSo;
		}
		set
		{
			voCongHang1NangCapHeSo = value;
		}
	}

	public List<int> VoCongHang2NangCapHeSo
	{
		get
		{
			return voCongHang2NangCapHeSo;
		}
		set
		{
			voCongHang2NangCapHeSo = value;
		}
	}

	public List<int> VoCongHang3NangCapHeSo
	{
		get
		{
			return voCongHang3NangCapHeSo;
		}
		set
		{
			voCongHang3NangCapHeSo = value;
		}
	}

	public List<int> VuKhiGiaNangCapLevel1
	{
		get
		{
			return vuKhiGiaNangCapLevel1;
		}
		set
		{
			vuKhiGiaNangCapLevel1 = value;
		}
	}

	public List<int> AoGiapGiaNangCapLevel1
	{
		get
		{
			return aoGiapGiaNangCapLevel1;
		}
		set
		{
			aoGiapGiaNangCapLevel1 = value;
		}
	}

	public List<int> TrangSucGiaNangCapLevel1
	{
		get
		{
			return trangSucGiaNangCapLevel1;
		}
		set
		{
			trangSucGiaNangCapLevel1 = value;
		}
	}

	public List<int> MuGiaNangCapLevel1
	{
		get
		{
			return muGiaNangCapLevel1;
		}
		set
		{
			muGiaNangCapLevel1 = value;
		}
	}

	public List<float> TrangBiHeSoNangCap
	{
		get
		{
			return trangBiHeSoNangCap;
		}
		set
		{
			trangBiHeSoNangCap = value;
		}
	}

	public List<float> NhanVatMaxExp
	{
		get
		{
			return nhanVatMaxExp;
		}
		set
		{
			nhanVatMaxExp = value;
		}
	}

	public List<int> MonPhaiMaxExp
	{
		get
		{
			return monPhaiMaxExp;
		}
		set
		{
			monPhaiMaxExp = value;
		}
	}

	public List<int> MonPhaiGetExp
	{
		get
		{
			return monPhaiGetExp;
		}
		set
		{
			monPhaiGetExp = value;
		}
	}

	public List<int> NhanVatGetExp
	{
		get
		{
			return nhanVatGetExp;
		}
		set
		{
			nhanVatGetExp = value;
		}
	}

	public List<string> TenHienThiTienTo
	{
		get
		{
			return tenHienThiTienTo;
		}
		set
		{
			tenHienThiTienTo = value;
		}
	}

	public List<string> TenHienThiHauTo
	{
		get
		{
			return tenHienThiHauTo;
		}
		set
		{
			tenHienThiHauTo = value;
		}
	}

	public List<string> BotTienTo
	{
		get
		{
			return botTienTo;
		}
		set
		{
			botTienTo = value;
		}
	}

	public int GetMaxBatQuaiCuongHoa()
	{
		return 9;
	}

	public int GetMaxHoTroThienCang()
	{
		return 14;
	}

	public int GetNumToCanThiet(int lvlTinhLuyen)
	{
		switch (lvlTinhLuyen)
		{
		case 0:
			return NumToLua0;
		case 1:
			return NumToLua1;
		case 2:
			return NumToLua2;
		default:
			return NumToLua3;
		}
	}

	public int GetMaxNgocCostumeByLvlTinhLuyen(int lvlTinhLuyen)
	{
		switch (lvlTinhLuyen)
		{
		case 3:
			return 4000;
		case 2:
			return 2000;
		case 1:
			return 1000;
		default:
			return 500;
		}
	}

	public static string GetNguyenKhiCodeName(NguyenKhiType nkType)
	{
		switch (nkType)
		{
		case NguyenKhiType.MENH:
			return "NK_THIEN_TUONG";
		case NguyenKhiType.NGOAI:
			return "NK_THAT_SAT";
		case NguyenKhiType.THAN:
			return "NK_THAM_LANG";
		case NguyenKhiType.KHI:
			return "NK_THIEN_CO";
		case NguyenKhiType.DO_DON:
			return "NK_VU_KHUC";
		case NguyenKhiType.KHANG_BAO:
			return "NK_PHA_QUAN";
		default:
			return string.Empty;
		}
	}

	public static string GetSoDoBQTCodeName(BatQuaiTranDoType bqType)
	{
		string result = string.Empty;
		switch (bqType)
		{
		case BatQuaiTranDoType.NONE:
			result = string.Empty;
			break;
		case BatQuaiTranDoType.THANH_LONG:
			result = "BQT_THANH_LONG";
			break;
		case BatQuaiTranDoType.BACH_HO:
			result = "BQT_BACH_HO";
			break;
		case BatQuaiTranDoType.HUYEN_VU:
			result = "BQT_HUYEN_VU";
			break;
		case BatQuaiTranDoType.CHU_TUOC:
			result = "BQT_CHU_TUOC";
			break;
		}
		return result;
	}

	public int GetSoLuongNgocCan(int level)
	{
		if (level < 0 || level >= NgocSoLuongCan.Count)
		{
			return -1;
		}
		return NgocSoLuongCan[level];
	}

	public float GetNgocBuffTocDoDanh(int level)
	{
		if (level < 0 || level >= NgocBuffTocDoDanh.Count)
		{
			return 0f;
		}
		return NgocBuffTocDoDanh[level];
	}

	public float GetNgocBuffKhangChuong(int level)
	{
		if (level < 0 || level >= NgocBuffKhangChuong.Count)
		{
			return 0f;
		}
		return NgocBuffKhangChuong[level];
	}

	public float GetNgocBuffHoaGiai(int level)
	{
		if (level < 0 || level >= NgocBuffHoaGiai.Count)
		{
			return 0f;
		}
		return NgocBuffHoaGiai[level];
	}

	public float GetNgocBuffBaoKich(int level)
	{
		if (level < 0 || level >= NgocBuffBaoKich.Count)
		{
			return 0f;
		}
		return NgocBuffBaoKich[level];
	}

	public float GetNgocBuffChiSo(int level)
	{
		if (level < 0 || level >= NgocBuffChiSo.Count)
		{
			return 0f;
		}
		return NgocBuffChiSo[level];
	}

	public TinhLuyenCfg GetTinhLuyenCfgForTrangBi(UserInfo.TrangBiData data)
	{
		if (ConfigManager.instance.m_dicTrangBi.ContainsKey(data.Name))
		{
			TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[data.Name];
			if (trangBiCfg.Hang == ItemClass.Binh)
			{
				if (data.TinhLuyenLevel == 0)
				{
					return ConfigManager.instance.OtherConfig.TinhLuyenBinh1;
				}
				if (data.TinhLuyenLevel == 1)
				{
					return ConfigManager.instance.OtherConfig.TinhLuyenBinh2;
				}
				if (data.TinhLuyenLevel == 2)
				{
					return ConfigManager.instance.OtherConfig.TinhLuyenBinh3;
				}
				if (data.TinhLuyenLevel == 3)
				{
					return ConfigManager.instance.OtherConfig.TinhLuyenBinh4;
				}
				if (data.TinhLuyenLevel == 4)
				{
					return ConfigManager.instance.OtherConfig.TinhLuyenBinh5;
				}
			}
			else if (trangBiCfg.Hang == ItemClass.At)
			{
				if (data.TinhLuyenLevel == 0)
				{
					return ConfigManager.instance.OtherConfig.TinhLuyenAt1;
				}
				if (data.TinhLuyenLevel == 1)
				{
					return ConfigManager.instance.OtherConfig.TinhLuyenAt2;
				}
				if (data.TinhLuyenLevel == 2)
				{
					return ConfigManager.instance.OtherConfig.TinhLuyenAt3;
				}
				if (data.TinhLuyenLevel == 3)
				{
					return ConfigManager.instance.OtherConfig.TinhLuyenAt4;
				}
				if (data.TinhLuyenLevel == 4)
				{
					return ConfigManager.instance.OtherConfig.TinhLuyenAt5;
				}
			}
			else if (trangBiCfg.Hang == ItemClass.Giap)
			{
				if (data.TinhLuyenLevel == 0)
				{
					return ConfigManager.instance.OtherConfig.TinhLuyenGiap1;
				}
				if (data.TinhLuyenLevel == 1)
				{
					return ConfigManager.instance.OtherConfig.TinhLuyenGiap2;
				}
				if (data.TinhLuyenLevel == 2)
				{
					return ConfigManager.instance.OtherConfig.TinhLuyenGiap3;
				}
				if (data.TinhLuyenLevel == 3)
				{
					return ConfigManager.instance.OtherConfig.TinhLuyenGiap4;
				}
				if (data.TinhLuyenLevel == 4)
				{
					return ConfigManager.instance.OtherConfig.TinhLuyenGiap5;
				}
			}
		}
		return null;
	}

	public int GetCostMoTatCa(int luotMoThuongMask)
	{
		int luotMoThuong = DanhSonCfg.GetLuotMoThuong(luotMoThuongMask);
		int num = GiaMoTatCaDanhSon;
		if (luotMoThuong > 3)
		{
			num -= GiaMoThuongDanhSonLan1;
		}
		if (luotMoThuong > 4)
		{
			num -= GiaMoThuongDanhSonLan2;
		}
		if (luotMoThuong > 5)
		{
			num -= GiaMoThuongDanhSonLan3;
		}
		if (luotMoThuong > 6)
		{
			num -= GiaMoThuongDanhSonLan4;
		}
		if (luotMoThuong > 7)
		{
			num = GiaMoThuongDanhSonLan6;
		}
		return num;
	}

	public int GetCostDanhLaiDanhSon(int luotChoi)
	{
		if (luotChoi < 1)
		{
			return 0;
		}
		return 20 + (luotChoi - 1) * 5;
	}

	public int GetNhanVatExpTangTruongByMonPhaiLevel(int levelMonPhai)
	{
		if (levelMonPhai < 1)
		{
			return 0;
		}
		if (levelMonPhai > nhanVatGetExp.Count)
		{
			return 0;
		}
		return nhanVatGetExp[levelMonPhai - 1];
	}

	public int GetMonPhaiExpTangTruong(int levelMonPhai)
	{
		if (levelMonPhai < 1)
		{
			return 0;
		}
		if (levelMonPhai > monPhaiGetExp.Count)
		{
			return 0;
		}
		return monPhaiGetExp[levelMonPhai - 1];
	}

	public long GetNhanVatMaxExpByLevel(string codeName, int level)
	{
		float num = ConfigManager.instance.OtherConfig.NhanVatMaxExp[level - 1];
		float heSoExp = ConfigManager.instance.m_dicNhanVats[codeName].HeSoExp;
		return (long)(num * heSoExp);
	}

	public long GetChienHonMaxExpByLevel(int level)
	{
		return ConfigManager.instance.OtherConfig.ChienHonExpLevelCfg[level - 1];
	}

	public bool IsValid()
	{
		if (monPhaiMaxExp.Count != monPhaiGetExp.Count)
		{
			EGDebug.LogError("MonPhaiMaxExp khong bang MonPhaiGetExp");
			return false;
		}
		if (nhanVatGetExp.Count != monPhaiGetExp.Count)
		{
			EGDebug.LogError("NhanVatGetExp khong bang MonPhaiGetExp");
			return false;
		}
		if (ThoiGianDotLuaTrai <= 0)
		{
			EGDebug.LogError("ThoiGianLuaTrai phải lớn hơn 0 và tính theo phút");
			return false;
		}
		if (ThoiGianGetExpLuaTrai <= 0)
		{
			EGDebug.LogError("ThoiGianGetExpLuaTrai phải lớn hơn 0 và tính theo giây");
			return false;
		}
		if (NumToLua0 <= 0 || NumToLua1 <= 0 || NumToLua2 <= 0 || NumToLua3 <= 0)
		{
			EGDebug.LogError("So to can de che tao costume NumToLua[0..3] phải lớn hơn 0");
			return false;
		}
		return true;
	}
}

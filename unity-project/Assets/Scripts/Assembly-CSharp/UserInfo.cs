using System;
using System.Collections.Generic;
using LitJson;
using Nettention.Proud;
using UnityEngine;

public class UserInfo : ExDataBase
{
	public class CostumeData
	{
		public int ID { get; set; }

		public string CodeName { get; set; }

		public int TinhLuyen { get; set; }

		public int LamNgoc { get; set; }

		public int TuNgoc { get; set; }

		public int HongNgoc { get; set; }

		public int HoangNgoc { get; set; }

		public int GID { get; set; }

		public int GetBuffCongFromNgoc()
		{
			return CostumeCfg.GetBuffFromNgoc(TrangBiData.LoaiNgoc.VP_NGOC_DO, HongNgoc);
		}

		public int GetBuffThuFromNgoc()
		{
			return CostumeCfg.GetBuffFromNgoc(TrangBiData.LoaiNgoc.VP_NGOC_XANH, LamNgoc);
		}

		public int GetBuffMauFromNgoc()
		{
			return CostumeCfg.GetBuffFromNgoc(TrangBiData.LoaiNgoc.VP_NGOC_VANG, HoangNgoc);
		}

		public int GetBuffNoiLucFromNgoc()
		{
			return CostumeCfg.GetBuffFromNgoc(TrangBiData.LoaiNgoc.VP_NGOC_TIM, TuNgoc);
		}
	}

	public class GamerData
	{
		public enum TonHieuType
		{
			LEVEL = 1,
			HANH_TAU = 2,
			CHIEN_TRUONG = 3,
			TINH_LUYEN = 4,
			CONG_LUC = 5,
			LUAN_KIEM = 6,
			HOANG_KIM = 7,
			QUANG_MINH_DINH = 8,
			THAN_THU = 9,
			DAI_HOI_VO_LAM = 10,
			THIEN_MA_THUONG_PHONG = 11,
			TRANG_BI_HOANG_KIM = 12
		}

		public enum CapHoiVienViet
		{
			NONE = 0,
			SAO_VANG = 1,
			SAO_BACH_KIM = 2,
			SAO_KIM_CUONG = 3
		}

		public class VongQuayData
		{
			public List<int> VongQuayCount = new List<int>();

			public List<int> VongQuayGet = new List<int>();

			public VongQuayData()
			{
				VongQuayCount.Add(0);
				VongQuayCount.Add(0);
				VongQuayCount.Add(0);
				VongQuayCount.Add(0);
				VongQuayCount.Add(0);
				VongQuayCount.Add(0);
				VongQuayCount.Add(0);
				VongQuayCount.Add(0);
				VongQuayGet.Add(0);
				VongQuayGet.Add(0);
				VongQuayGet.Add(0);
				VongQuayGet.Add(0);
				VongQuayGet.Add(0);
				VongQuayGet.Add(0);
				VongQuayGet.Add(0);
				VongQuayGet.Add(0);
			}

			public void Increase()
			{
				for (int i = 0; i < VongQuayCount.Count; i++)
				{
					VongQuayCount[i]++;
				}
			}
		}

		public LinhDuocType CurSeed;

		public DateTime ResetAnTromTime;

		public int curThuCuoi;

		public int curThanThu;

		public int memId;

		public CapHoiVienViet HoiVienViet;

		public int BoostExpTurn;

		public int ID { get; set; }

		public string UserName { get; set; }

		public string DisplayName { get; set; }

		public int Level { get; set; }

		public int Vip { get; set; }

		public long Vang { get; set; }

		public long Bac { get; set; }

		public long Exp { get; set; }

		public long ExpMax { get; set; }

		public string GhiChu { get; set; }

		public string GhiChuTrongNgay { get; set; }

		public DateTime NextTimeJoinLM { get; set; }

		public int HuaNguyenCount { get; set; }

		public int HuaNguyenTachCount { get; set; }

		public int RuongThachSanhLvl { get; set; }

		public int RuongThachSanhExp { get; set; }

		public int KnbDaNap { get; set; }

		public List<CaoNhanData> KyNgoCaoNhan { get; set; }

		public List<BanDoData> KyNgoBanDo { get; set; }

		public List<BangHuuData> KyNgoBangHuu { get; set; }

		public List<ThuongNhanData> KyNgoThuongNhan { get; set; }

		public List<TyThiData> KyNgoTyThi { get; set; }

		public int ULinhTotalDiem { get; set; }

		public int ULinhCurrentDiem { get; set; }

		public DateTime BatCocTime { get; set; }

		public DateTime ThachDauTime { get; set; }

		public int QuayXoSo { get; set; }

		public LucDaiMonPhai MonPhaiTuongTro { get; set; }

		public int FriendServerID { get; set; }

		public int FriendGID { get; set; }

		public int DiemThuongNap { get; set; }

		public int DiemTieuKNB { get; set; }

		public int ThuongTieuFlag { get; set; }

		public int KhiThe { get; set; }

		public string VongQuayDataStr { get; set; }

		public int DiemTichLuyNap { get; set; }

		public int DiemTichLuyTieu { get; set; }

		public int TichLuyNapFlag { get; set; }

		public int TichLuyTieuFlag { get; set; }

		public int PhaoHoaCount { get; set; }

		public int PhaoHoaDaNhanThuong { get; set; }

		public int MaxHangLuanKiem { get; set; }

		public int PhaoHoaBangChien { get; set; }

		public List<int> ThangThachDauList { get; set; }

		public int DiemCHThanBi { get; set; }

		public string BacMayManPoint { get; set; }

		public int TuBaoBonLevel { get; set; }

		public int TuBaoBonExp { get; set; }

		public int NapHangNgay { get; set; }

		public DateTime LastTimeJoinLienMinh { get; set; }

		public TonHieuType CurTonHieu { get; set; }

		public int NapMayManHangNgay { get; set; }

		public int BaoKhoDangGiu { get; set; }

		public int LuotCuopBaoKhoTrongNgay { get; set; }

		public string ListCuuThuBaoKho { get; set; }

		public int TopTinhLuyenCap5 { get; set; }

		public int TopTrangBiHKCap3 { get; set; }

		public int TopTrangBiHKCap2 { get; set; }

		public int TopTrangBiHKCap1 { get; set; }

		public int TichLuyCamCung { get; set; }

		public int LuotQuayCamCungDacBiet { get; set; }

		public float DiemHoaVang { get; set; }

		public long GetExpBoosted(long exp)
		{
			if (BoostExpTurn > 0)
			{
				if (Level < 50)
				{
					return exp * 3;
				}
				if (Level < 66)
				{
					return (long)((float)exp * 2.3f);
				}
				if (Level < 80)
				{
					return (long)((float)exp * 1.5f);
				}
				if (Level < 91)
				{
					return (long)((float)exp * 1.2f);
				}
				return (long)((float)exp * 1.1f);
			}
			return exp;
		}
	}

	public class DanhHieuData
	{
		public int GiangHoHaoKiet { get; set; }

		public int QuyTanThanPhan { get; set; }

		public int ThanhDanhHienHach { get; set; }

		public int DucHuyetPhanChien { get; set; }

		public int DungSiXungTran { get; set; }

		public DateTime KinhTamDongPhach { get; set; }

		public DateTime KinhThienDongDia { get; set; }

		public DateTime KinhTheHaiTuc { get; set; }

		public DateTime ThanCongCaiThe { get; set; }

		public DateTime DangPhongTaoCuc { get; set; }

		public int TuyTamVoHoc { get; set; }

		public DateTime ThienHaVoSong { get; set; }

		public DateTime VoDuLuanBi { get; set; }

		public DateTime NhatDaiTonSu { get; set; }

		public DateTime NgaoThiQuanHung { get; set; }

		public DateTime HanHuuDichThu { get; set; }

		public DateTime LoHoaThuanThanh { get; set; }

		public DateTime GiaKinhTuuThuc { get; set; }

		public int LienMinhThanhVien { get; set; }

		public int LuotDongNhan { get; set; }

		public int LuotGhepVC { get; set; }

		public DanhHieuData()
		{
			GiangHoHaoKiet = (QuyTanThanPhan = (ThanhDanhHienHach = (DucHuyetPhanChien = (DungSiXungTran = (TuyTamVoHoc = 0)))));
			LienMinhThanhVien = 0;
			GiaKinhTuuThuc = (LoHoaThuanThanh = (NgaoThiQuanHung = (NhatDaiTonSu = (HanHuuDichThu = (VoDuLuanBi = (ThienHaVoSong = (DangPhongTaoCuc = (ThanCongCaiThe = (KinhTheHaiTuc = (KinhThienDongDia = (KinhTamDongPhach = new DateTime(1900, 1, 1))))))))))));
			LuotDongNhan = (LuotGhepVC = 0);
		}
	}

	public class CaoNhanData
	{
		public enum ClassLevel
		{
			THAP = 0,
			TRUNG = 1,
			CAO = 2,
			SIEU_CAP = 3
		}

		public int Exp { get; set; }

		public ClassLevel Loai { get; set; }

		public float X { get; set; }

		public DateTime Time { get; set; }
	}

	public class ThuongNhanData
	{
		public string TenVP { get; set; }

		public int Gia { get; set; }

		public DateTime GioDi { get; set; }
	}

	public class TyThiData
	{
		public enum NPCLevel
		{
			De = 0,
			BinhThuong = 1,
			Kho = 2,
			SieuKho = 3
		}

		public NPCLevel Loai { get; set; }

		public DateTime GioDi { get; set; }
	}

	public class BanDoData
	{
		public int Loai { get; set; }

		public DateTime Time { get; set; }
	}

	public class BangHuuData
	{
		public string Ten { get; set; }

		public DateTime Time { get; set; }

		public int Gia { get; set; }
	}

	public class BanBeData
	{
		public enum BanBeStatus
		{
			UNCONFIRMED = 0,
			CONFIRMED = 1,
			REQUESTING = 2
		}

		public int GID { get; set; }

		public string DisplayName { get; set; }

		public int Level { get; set; }

		public BanBeStatus Status { get; set; }

		public int Vip { get; set; }

		public bool Online { get; set; }

		public int DanhSonCount { get; set; }
	}

	public class CuuThuData
	{
		public enum CuuThuType
		{
			LUAN_KIEM = 0,
			BAT_COC = 1
		}

		public int GID { get; set; }

		public string DisplayName { get; set; }

		public int Level { get; set; }

		public CuuThuType Type { get; set; }

		public int Vip { get; set; }

		public bool Online { get; set; }

		public int HangLK { get; set; }
	}

	public class GiaTriThoiGianData
	{
		public static int TheLucMax
		{
			get
			{
				return 100;
			}
		}

		public int TheLuc { get; set; }

		public DateTime lastTimeHoiTheLuc { get; set; }

		public DateTime layNhanVat1Time { get; set; }

		public DateTime layNhanVat2Time { get; set; }

		public DateTime layNhanVat3Time { get; set; }

		public int layNhanVat1Num { get; set; }

		public DateTime lastTimeLogin { get; set; }

		public DateTime lastTimeLogout { get; set; }

		public DateTime registerTime { get; set; }

		public int ThamBaiCount { get; set; }

		public int ThamBaiNhanThuongCount { get; set; }

		public int ThamBaiBatQuaiCount { get; set; }

		public int ThamBaiMuCount { get; set; }

		public int ThamBaiTrangSucCount { get; set; }

		public DateTime LastTimeDanhNhanhGH { get; set; }

		public int LuotBatCoc { get; set; }

		public int BatThoCount { get; set; }

		public DateTime BatThoTime { get; set; }
	}

	public class ServerData
	{
		public class DangNhapTetCfg
		{
			public List<PTDangNhapTet> ListThuongDNTet = new List<PTDangNhapTet>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class PTDangNhapTet
		{
			private int index;

			public List<PhanThuongResponse.PhanThuong> ListPhanThuong = new List<PhanThuongResponse.PhanThuong>();

			public int Index
			{
				get
				{
					return index;
				}
				set
				{
					index = value;
				}
			}
		}

		public class DiHoaCungTang
		{
			private int giaTri;

			public List<PhanThuongResponse.PhanThuong> ListPhanThuong = new List<PhanThuongResponse.PhanThuong>();

			public int GiaTri
			{
				get
				{
					return giaTri;
				}
				set
				{
					giaTri = value;
				}
			}
		}

		public class DiHoaCungTop
		{
			private int minGiaTri;

			public List<PhanThuongResponse.PhanThuong> ListPhanThuong = new List<PhanThuongResponse.PhanThuong>();

			public int MinGiaTri
			{
				get
				{
					return minGiaTri;
				}
				set
				{
					minGiaTri = value;
				}
			}
		}

		public class DiHoaCungCfg
		{
			public List<DiHoaCungTang> ListTang = new List<DiHoaCungTang>();

			public List<DiHoaCungTop> ListTop = new List<DiHoaCungTop>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class MocThuongNap
		{
			private int giaTri;

			public List<PhanThuongResponse.PhanThuong> ListPhanThuong = new List<PhanThuongResponse.PhanThuong>();

			public int GiaTri
			{
				get
				{
					return giaTri;
				}
				set
				{
					giaTri = value;
				}
			}
		}

		public class ThuongNapCfg
		{
			public List<MocThuongNap> ListMocNap = new List<MocThuongNap>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class EventVongQuay
		{
			public class VongQuayDieuKien
			{
				public int Kieu;

				public float XacSuat;

				public int Min;

				public int MaxLan;
			}

			public List<int> TopMinList;

			public DateTime ThoiGianKetThuc { get; set; }

			public DateTime ThoiGianHideGui { get; set; }

			public string ThongBao { get; set; }

			public List<PhanThuongResponse> PhanThuongVongQuay { get; set; }

			public List<PhanThuongResponse> PhanThuongTopVongQuay { get; set; }

			public List<VongQuayDieuKien> ListDieuKien { get; set; }
		}

		public class TichLuyNapCfg
		{
			public List<MocTichLuyNap> ListMocNap = new List<MocTichLuyNap>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class MocTichLuyNap
		{
			private int giaTri;

			public List<PhanThuongResponse.PhanThuong> ListPhanThuong = new List<PhanThuongResponse.PhanThuong>();

			public int GiaTri
			{
				get
				{
					return giaTri;
				}
				set
				{
					giaTri = value;
				}
			}
		}

		public class TichLuyTieuCfg
		{
			public List<MocTichLuyTieu> ListMocTieu = new List<MocTichLuyTieu>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class MocTichLuyTieu
		{
			private int giaTri;

			public List<PhanThuongResponse.PhanThuong> ListPhanThuong = new List<PhanThuongResponse.PhanThuong>();

			public int GiaTri
			{
				get
				{
					return giaTri;
				}
				set
				{
					giaTri = value;
				}
			}
		}

		public class EventPhaoHoa
		{
			public class EventPhaoHoaPhanThuong
			{
				public int rate;

				public PhanThuongResponse.PhanThuong pt = new PhanThuongResponse.PhanThuong();
			}

			public int PhaoHoaCount;

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }

			public DateTime ThoiGianHideGUI { get; set; }

			public Dictionary<string, PhanThuongResponse> PhanThuongAll { get; set; }

			public int TopCount { get; set; }

			public List<KeyValuePair<string, int>> TopInfo { get; set; }

			public List<PhanThuongResponse> PhanThuongTop { get; set; }

			public List<EventPhaoHoaPhanThuong> PhanThuongTungLan { get; set; }
		}

		public class EventVatPhamExp
		{
			public int heroValue;

			public int monphaiValue;

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class EventRuongThanBiCfg
		{
			public PhanThuongResponse.PhanThuong PhanThuongFix;

			public List<PhanThuongResponse.PhanThuong> ListPhanThuong = new List<PhanThuongResponse.PhanThuong>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class EventDapNieuCfg
		{
			public List<PhanThuongResponse.PhanThuong> ListPhanThuong = new List<PhanThuongResponse.PhanThuong>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class GuiTietKiemCfg
		{
			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class EventSonMonCfg
		{
			public List<PhanThuongResponse> PhanThuongCaNhan { get; set; }

			public List<PhanThuongResponse> PhanThuongTapThe { get; set; }

			public Dictionary<string, PhanThuongResponse> PhanThuongServer { get; set; }

			public int StartDate { get; set; }

			public DateTime StartEvent { get; set; }
		}

		public class EventBacMayManCfg
		{
			public long MaxPoint { get; set; }

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class EventTuBaoBonCfg
		{
			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class EventTayVucCfg
		{
			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class EventTopMoRuongThanCfg
		{
			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }

			public string RuongCodeName { get; set; }

			public List<PhanThuongResponse> PhanThuongTopEvent { get; set; }
		}

		public class EventTichNapHangNgayCfg
		{
			public List<MocTichLuyNapHangNgay> ListMocNap = new List<MocTichLuyNapHangNgay>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class MocTichLuyNapHangNgay
		{
			private int giaTri;

			public List<PhanThuongResponse.PhanThuong> ListPhanThuong = new List<PhanThuongResponse.PhanThuong>();

			public int GiaTri
			{
				get
				{
					return giaTri;
				}
				set
				{
					giaTri = value;
				}
			}
		}

		public class EventAnTheCaoThu
		{
			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }

			public int GiaKhoiDau { get; set; }

			public int GiaPlus { get; set; }

			public List<PhanThuongAnTheCaoThu> ListPhanThuongRandom { get; set; }
		}

		public class PhanThuongAnTheCaoThu
		{
			public PhanThuongResponse.PhanThuong PhanThuong { get; set; }

			public int ChiSoRanDom { get; set; }
		}

		public class EventCamCungBiBao
		{
			public List<PhanThuongCamCungBiBao> ListPhanThuong = new List<PhanThuongCamCungBiBao>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class PhanThuongCamCungBiBao
		{
			public PhanThuongResponse.PhanThuong PhanThuong { get; set; }

			public int TichLuyYeuCau { get; set; }

			public int ChiSo { get; set; }

			public int MaxLuot { get; set; }
		}

		public class EventQuaySMCfg
		{
			public class EventQuaySMItem
			{
				public float tile;

				public PhanThuongResponse.PhanThuong phanThuong { get; set; }
			}

			public List<EventQuaySMItem> listPhanThuong = new List<EventQuaySMItem>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class EventTopHMNCfg
		{
			public List<PhanThuongResponse.PhanThuong> listPhanThuongTop = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> listPhanThuong2Top = new List<PhanThuongResponse.PhanThuong>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class EventNhiemVuBangCfg
		{
			public class EventNhiemVuBangItem
			{
				public float tile;

				public PhanThuongResponse.PhanThuong phanThuong { get; set; }
			}

			public List<EventNhiemVuBangItem> listPhanThuong = new List<EventNhiemVuBangItem>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class KhuyenMaiKnbCfg
		{
			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }

			public int TiLeKhuyenMai { get; set; }
		}

		public class QuayDeTuCfg
		{
			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }

			public string DeTuTramDam { get; set; }

			public string DeTuVanDam { get; set; }
		}

		public class DuaTopLvlCfg
		{
			public List<PhanThuongResponse.PhanThuong> Ds1 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds2 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds3 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds4 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds5 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds6 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds7 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds8 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds9 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds10 = new List<PhanThuongResponse.PhanThuong>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class DuaTopLuanKiemCfg
		{
			public List<PhanThuongResponse.PhanThuong> Ds1 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds2 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds3 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds4 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds5 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds6 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds7 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds8 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds9 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds10 = new List<PhanThuongResponse.PhanThuong>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class DoiVatPhamCfg
		{
			public List<PhanThuongResponse.PhanThuong> Ds1 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds2 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds3 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Ds4 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Pt1 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Pt2 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Pt3 = new List<PhanThuongResponse.PhanThuong>();

			public List<PhanThuongResponse.PhanThuong> Pt4 = new List<PhanThuongResponse.PhanThuong>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }

			public int Max1 { get; set; }

			public int Max2 { get; set; }

			public int Max3 { get; set; }

			public int Max4 { get; set; }
		}

		public class RuongThachSanhCfg
		{
			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class ULinhSonTrangCfg
		{
			public class DoiThuongItem
			{
				public PhanThuongResponse.PhanThuong PhanThuong = new PhanThuongResponse.PhanThuong();

				public int MaxCount { get; set; }

				public int Diem { get; set; }
			}

			public class TopItem
			{
				public List<PhanThuongResponse.PhanThuong> PhanThuongs = new List<PhanThuongResponse.PhanThuong>();

				public int MinDiem { get; set; }
			}

			public List<DoiThuongItem> ListDoiThuong = new List<DoiThuongItem>();

			public List<TopItem> ListPhanThuongTop = new List<TopItem>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }

			public int KnbReset { get; set; }

			public int KnbResetDiem { get; set; }

			public int SoLuongTop { get; set; }
		}

		public class TopTuanChienTruongCfg
		{
			public class TopItem
			{
				public List<PhanThuongResponse.PhanThuong> PhanThuongs = new List<PhanThuongResponse.PhanThuong>();
			}

			public List<TopItem> ListPhanThuongTop = new List<TopItem>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class EventTheScoinCfg
		{
			public class EventTheScoinMoc
			{
				public int Moc;

				public List<PhanThuongResponse.PhanThuong> ListPhanThuong = new List<PhanThuongResponse.PhanThuong>();
			}

			public List<EventTheScoinMoc> ListMoc = new List<EventTheScoinMoc>();

			public DateTime ThoiGianBatDau { get; set; }

			public DateTime ThoiGianKetThuc { get; set; }
		}

		public class LoginMessage
		{
			public string Tit { get; set; }

			public string Img { get; set; }

			public string Desc { get; set; }

			public LoginMessage()
			{
				Tit = string.Empty;
				Img = string.Empty;
				Desc = string.Empty;
			}
		}

		public EventVongQuay VongQuayInfo;

		public EventPhaoHoa PhaoHoaInfo;

		public EventVatPhamExp VatPhamExpInfo;

		public List<PhanThuongResponse> PhanThuongTopLanhDia = new List<PhanThuongResponse>();

		public List<PhanThuongResponse> PhanThuongTopNienThu = new List<PhanThuongResponse>();

		public string HighlightMsg = string.Empty;

		public List<LoginMessage> LoginMgs = new List<LoginMessage>();

		public string Name { get; set; }

		public int CumServerID { get; set; }

		public DateTime TimeStartSieuCup { get; set; }

		public BanhChungConfig BanhChungCfg { get; set; }

		public DangNhapTetCfg DangNhapTetConfig { get; set; }

		public DiHoaCungCfg DiHoaCungConfig { get; set; }

		public ThuongNapCfg ThuongNapConfig { get; set; }

		public TichLuyNapCfg TichLuyNapConfig { get; set; }

		public TichLuyTieuCfg TichLuyTieuConfig { get; set; }

		public EventRuongThanBiCfg RuongThanBiConfig { get; set; }

		public EventDapNieuCfg EventDapNieuConfig { get; set; }

		public GuiTietKiemCfg GuiTietKiemConfig { get; set; }

		public EventSonMonCfg EventSonMon { get; set; }

		public EventBacMayManCfg BacMayManConfig { get; set; }

		public EventTuBaoBonCfg TuBaoBonConfig { get; set; }

		public EventTayVucCfg TayVucThuongNhanConfig { get; set; }

		public EventTopMoRuongThanCfg TopBaoRuongConfig { get; set; }

		public EventTichNapHangNgayCfg TichLuyNapHangNgayConfig { get; set; }

		public EventAnTheCaoThu AnTheCaoThuConfig { get; set; }

		public EventCamCungBiBao CamCungBiBaoConfig { get; set; }

		public EventQuaySMCfg eventQuaySM { get; set; }

		public EventTopHMNCfg eventTopHMN { get; set; }

		public EventNhiemVuBangCfg eventNhiemVuBang { get; set; }

		public KhuyenMaiKnbCfg KmKnb { get; set; }

		public DuaTopLvlCfg DuaTopLevelCfg { get; set; }

		public DuaTopLuanKiemCfg DuaTopLKCfg { get; set; }

		public DoiVatPhamCfg DoiDoCfg { get; set; }

		public int ID { get; set; }

		public int eventAnGaLuotCount { get; set; }

		public string GoiVatPham { get; set; }

		public string NapKnbImgUrl { get; set; }

		public RuongThachSanhCfg RuongCfg { get; set; }

		public ULinhSonTrangCfg ULinhCfg { get; set; }

		public string LockTinhNang { get; set; }
	}

	public class DiemTichLuyData
	{
		public int MoHopKnbCount { get; set; }

		public int MoHopHang2Count { get; set; }

		public int MoHopHang3Count { get; set; }

		public int MoHopManhHang2Count { get; set; }

		public int MoHopManhHang3Count { get; set; }

		public int MoHopManhVCHang2Count { get; set; }

		public int MoHopManhVCHang3Count { get; set; }

		public int LayDeTuKnbCount { get; set; }

		public int LayDeTuHang3Count { get; set; }

		public int LayDeTuFreeHang3Count { get; set; }

		public int DanhSonTichLuy { get; set; }

		public int DanhSonCount { get; set; }

		public int CaoNhanMax { get; set; }

		public int ThuongNhanMax { get; set; }

		public int BanDoMax { get; set; }

		public int BangHuuMax { get; set; }

		public int TyThiMax { get; set; }
	}

	public class LuanKiemData
	{
		public enum RewardMask
		{
			TOP1000 = 1,
			TOP500 = 2,
			TOP200 = 4,
			TOP100 = 8,
			TOP50 = 0x10,
			TOP10 = 0x20,
			TOP1 = 0x40
		}

		public int Hang { get; set; }

		public int DiemTichLuy { get; set; }

		public DateTime LastTimeGetDiem { get; set; }

		public int LuotLuanKiem { get; set; }

		public int RewardTopMask { get; set; }

		public bool CanGotReward(RewardMask mask)
		{
			return ((uint)RewardTopMask & (uint)mask) != 0;
		}

		public bool IsGotReward(RewardMask mask)
		{
			return (RewardTopMask & ((int)mask << 16)) != 0;
		}

		public int GotReward(RewardMask mask)
		{
			RewardTopMask |= (int)mask << 16;
			return RewardTopMask;
		}

		public int TurnOnReward(RewardMask mask)
		{
			RewardTopMask |= (int)mask;
			return RewardTopMask;
		}

		public bool IsRewardTurnOff(RewardMask mask)
		{
			return !IsGotReward(mask) && !CanGotReward(mask);
		}
	}

	public class VatPhamTieuThuData
	{
		public string Name { get; set; }

		public int Quantity { get; set; }

		public int GID { get; set; }

		public int ID { get; set; }
	}

	public class ManhTrangBiData
	{
		public string Name { get; set; }

		public int Quantity { get; set; }

		public int GID { get; set; }

		public int ID { get; set; }
	}

	public class ManhVoCongData
	{
		public string Name { get; set; }

		public int Quantity { get; set; }

		public int GID { get; set; }

		public int ID { get; set; }
	}

	public class HonNhanVatData
	{
		public string Name { get; set; }

		public int Quantity { get; set; }

		public int ID { get; set; }

		public int GID { get; set; }
	}

	public class GiangHoData
	{
		public class NhiemVuRecord
		{
			public int S { get; set; }

			public int T { get; set; }
		}

		public int GiangHoIdx { get; set; }

		public int HoanThanh { get; set; }

		public int LuotChoi { get; set; }

		public int NumNhanThuong { get; set; }

		public List<NhiemVuRecord> NhiemVu { get; set; }
	}

	public class DanhSonData
	{
		public int DanhSonIdx { get; set; }

		public int LuotChoi { get; set; }

		public byte VuotAi { get; set; }

		public int LuotMoThuong { get; set; }

		public int FriendId { get; set; }
	}

	public class HuyenKhi
	{
		public int ID { get; set; }

		public string Name { get; set; }

		public int ChienHonID { get; set; }

		public int GID { get; set; }

		public int Level { get; set; }

		public string EffName { get; set; }
	}

	public class ChienHon
	{
		public int ID { get; set; }

		public int HID { get; set; }

		public int GID { get; set; }

		public int Level { get; set; }

		public int MenhBoiDuong { get; set; }

		public int NgoaiBoiDuong { get; set; }

		public int ThanBoiDuong { get; set; }

		public int KhiBoiDuong { get; set; }

		public int LoaiBuff { get; set; }

		public int HuyenKhi1ID { get; set; }

		public int HuyenKhi2ID { get; set; }

		public long Exp { get; set; }

		public long MaxExp { get; set; }

		public string Name { get; set; }

		public int Menh { get; set; }

		public int Ngoai { get; set; }

		public int ThanPhap { get; set; }

		public int Noi { get; set; }

		public int DotPha { get; set; }

		public int MenhDotPha
		{
			get
			{
				return Menh + Menh * ConfigManager.instance.OtherConfig.ChienHonDotPhaChiSoCfg[DotPha] / 100;
			}
		}

		public int NgoaiDotPha
		{
			get
			{
				return Ngoai + Ngoai * ConfigManager.instance.OtherConfig.ChienHonDotPhaChiSoCfg[DotPha] / 100;
			}
		}

		public int ThanDotPha
		{
			get
			{
				return ThanPhap + ThanPhap * ConfigManager.instance.OtherConfig.ChienHonDotPhaChiSoCfg[DotPha] / 100;
			}
		}

		public int KhiDotPha
		{
			get
			{
				return Noi + Noi * ConfigManager.instance.OtherConfig.ChienHonDotPhaChiSoCfg[DotPha] / 100;
			}
		}

		public ChienHon()
		{
		}

		public ChienHon(ChienHon other)
		{
			ID = other.ID;
			KhiBoiDuong = other.KhiBoiDuong;
			MenhBoiDuong = other.MenhBoiDuong;
			NgoaiBoiDuong = other.NgoaiBoiDuong;
			ThanBoiDuong = other.ThanBoiDuong;
			DotPha = other.DotPha;
			LoaiBuff = other.LoaiBuff;
			HuyenKhi1ID = other.HuyenKhi1ID;
			HuyenKhi2ID = other.HuyenKhi2ID;
			Menh = other.Menh;
			Ngoai = other.Ngoai;
			ThanPhap = other.ThanPhap;
			Noi = other.Noi;
			Exp = other.Exp;
			MaxExp = other.MaxExp;
			Name = other.Name;
			GID = other.GID;
			HID = other.HID;
			Level = other.Level;
		}

		public ChiSoNhanVat GetChiSoByHuyenKhi(List<HuyenKhi> listHuyenKhi)
		{
			ChiSoNhanVat chiSoNhanVat = new ChiSoNhanVat();
			if (listHuyenKhi == null)
			{
				return chiSoNhanVat;
			}
			HuyenKhi huyenKhi = listHuyenKhi.Find((HuyenKhi e) => e.ID == HuyenKhi1ID);
			if (huyenKhi != null)
			{
				HuyenKhiCfg value;
				ConfigManager.instance.m_dicHuyenKhi.TryGetValue(huyenKhi.Name, out value);
				if (value.LoaiBuff1 == "menh")
				{
					chiSoNhanVat.Menh += (float)Menh * value.ChiSo1ByLevels[huyenKhi.Level - 1] / 100f;
				}
				else if (value.LoaiBuff1 == "ngoai")
				{
					chiSoNhanVat.Ngoai += (float)Ngoai * value.ChiSo1ByLevels[huyenKhi.Level - 1] / 100f;
				}
				else if (value.LoaiBuff1 == "than")
				{
					chiSoNhanVat.ThanPhap += (float)ThanPhap * value.ChiSo1ByLevels[huyenKhi.Level - 1] / 100f;
				}
				else
				{
					chiSoNhanVat.Noi += (float)Noi * value.ChiSo1ByLevels[huyenKhi.Level - 1] / 100f;
				}
				if (value.LoaiBuff2 == "menh")
				{
					chiSoNhanVat.Menh += (float)Menh * value.ChiSo2ByLevels[huyenKhi.Level - 1] / 100f;
				}
				else if (value.LoaiBuff2 == "ngoai")
				{
					chiSoNhanVat.Ngoai += (float)Ngoai * value.ChiSo2ByLevels[huyenKhi.Level - 1] / 100f;
				}
				else if (value.LoaiBuff2 == "than")
				{
					chiSoNhanVat.ThanPhap += (float)ThanPhap * value.ChiSo2ByLevels[huyenKhi.Level - 1] / 100f;
				}
				else
				{
					chiSoNhanVat.Noi += (float)Noi * value.ChiSo2ByLevels[huyenKhi.Level - 1] / 100f;
				}
			}
			HuyenKhi huyenKhi2 = listHuyenKhi.Find((HuyenKhi e) => e.ID == HuyenKhi2ID);
			if (huyenKhi2 != null)
			{
				HuyenKhiCfg value2;
				ConfigManager.instance.m_dicHuyenKhi.TryGetValue(huyenKhi2.Name, out value2);
				if (value2.LoaiBuff1 == "menh")
				{
					chiSoNhanVat.Menh += (float)Menh * value2.ChiSo1ByLevels[huyenKhi2.Level - 1] / 100f;
				}
				else if (value2.LoaiBuff1 == "ngoai")
				{
					chiSoNhanVat.Ngoai += (float)Ngoai * value2.ChiSo1ByLevels[huyenKhi2.Level - 1] / 100f;
				}
				else if (value2.LoaiBuff1 == "than")
				{
					chiSoNhanVat.ThanPhap += (float)ThanPhap * value2.ChiSo1ByLevels[huyenKhi2.Level - 1] / 100f;
				}
				else
				{
					chiSoNhanVat.Noi += (float)Noi * value2.ChiSo1ByLevels[huyenKhi2.Level - 1] / 100f;
				}
				if (value2.LoaiBuff2 == "menh")
				{
					chiSoNhanVat.Menh += (float)Menh * value2.ChiSo2ByLevels[huyenKhi2.Level - 1] / 100f;
				}
				else if (value2.LoaiBuff2 == "ngoai")
				{
					chiSoNhanVat.Ngoai += (float)Ngoai * value2.ChiSo2ByLevels[huyenKhi2.Level - 1] / 100f;
				}
				else if (value2.LoaiBuff2 == "than")
				{
					chiSoNhanVat.ThanPhap += (float)ThanPhap * value2.ChiSo2ByLevels[huyenKhi2.Level - 1] / 100f;
				}
				else
				{
					chiSoNhanVat.Noi += (float)Noi * value2.ChiSo2ByLevels[huyenKhi2.Level - 1] / 100f;
				}
			}
			return chiSoNhanVat;
		}

		public int MenhByDotPha(int dotpha)
		{
			return Menh + Menh * ConfigManager.instance.OtherConfig.ChienHonDotPhaChiSoCfg[dotpha] / 100;
		}

		public int NgoaiByDotPha(int dotpha)
		{
			return Ngoai + Ngoai * ConfigManager.instance.OtherConfig.ChienHonDotPhaChiSoCfg[dotpha] / 100;
		}

		public int ThanByDotPha(int dotpha)
		{
			return ThanPhap + ThanPhap * ConfigManager.instance.OtherConfig.ChienHonDotPhaChiSoCfg[dotpha] / 100;
		}

		public int KhiByDotPha(int dotpha)
		{
			return Noi + Noi * ConfigManager.instance.OtherConfig.ChienHonDotPhaChiSoCfg[dotpha] / 100;
		}

		public ChiSoNhanVat GetChiSoCuoi(List<HuyenKhi> listHuyenKhi)
		{
			ChiSoNhanVat chiSoNhanVat = new ChiSoNhanVat();
			ChiSoNhanVat chiSoByHuyenKhi = GetChiSoByHuyenKhi(listHuyenKhi);
			chiSoNhanVat.Menh = MenhDotPha;
			chiSoNhanVat.Menh += chiSoByHuyenKhi.Menh;
			chiSoNhanVat.Ngoai = NgoaiDotPha;
			chiSoNhanVat.Ngoai += chiSoByHuyenKhi.Ngoai;
			chiSoNhanVat.ThanPhap = ThanDotPha;
			chiSoNhanVat.ThanPhap += chiSoByHuyenKhi.ThanPhap;
			chiSoNhanVat.Noi = KhiDotPha;
			chiSoNhanVat.Noi += chiSoByHuyenKhi.Noi;
			return chiSoNhanVat;
		}
	}

	public class HeroData
	{
		public class BaseData
		{
			public int Menh { get; set; }

			public int Ngoai { get; set; }

			public int ThanPhap { get; set; }

			public int Noi { get; set; }

			public float BAS { get; set; }

			public float BMS { get; set; }

			public float Range { get; set; }

			public BaseData()
			{
			}

			public BaseData(BaseData other)
			{
				Menh = other.Menh;
				Ngoai = other.Ngoai;
				ThanPhap = other.ThanPhap;
				Noi = other.Noi;
				BAS = other.BAS;
				BMS = other.BMS;
				Range = other.Range;
			}
		}

		public class AI
		{
			public enum ENUM_CHIEN_THUAT
			{
				TAN_CONG_CHU_DONG_GAN_NHAT = 0,
				TAN_CONG_CHU_DONG_SINH_LUC_MIN = 1,
				TAN_CONG_CHU_DONG_SINH_LUC_MAX = 2,
				TAN_CONG_CHU_DONG_CONG_MIN = 3,
				TAN_CONG_CHU_DONG_CONG_MAX = 4,
				TAN_CONG_CHU_DONG_THAN_PHAP_MIN = 5,
				TAN_CONG_CHU_DONG_THAN_PHAP_MAX = 6,
				TAN_CONG_CHU_DONG_NOI_MIN = 7,
				TAN_CONG_CHU_DONG_NOI_MAX = 8,
				TAN_CONG_BI_DONG_GAN_NHAT = 9,
				TAN_CONG_BI_DONG_SINH_LUC_MIN = 10,
				TAN_CONG_BI_DONG_SINH_LUC_MAX = 11,
				TAN_CONG_BI_DONG_CONG_MIN = 12,
				TAN_CONG_BI_DONG_CONG_MAX = 13,
				TAN_CONG_BI_DONG_THAN_PHAP_MIN = 14,
				TAN_CONG_BI_DONG_THAN_PHAP_MAX = 15,
				TAN_CONG_BI_DONG_NOI_MIN = 16,
				TAN_CONG_BI_DONG_NOI_MAX = 17,
				BAO_VE = 18,
				TAN_CONG_CHU_DONG_SLOT = 19,
				TAN_CONG_BI_DONG_SLOT = 20
			}

			public ENUM_CHIEN_THUAT ChienThuat { get; set; }

			public int BaoVeDongDoi { get; set; }

			public List<int> TanCongSlotList { get; set; }

			public AI()
			{
				TanCongSlotList = new List<int> { 0, 1, 2, 3 };
			}

			public AI(AI other)
			{
				ChienThuat = other.ChienThuat;
				BaoVeDongDoi = other.BaoVeDongDoi;
				TanCongSlotList = new List<int>(other.TanCongSlotList);
			}

			public bool IsChuDong()
			{
				ENUM_CHIEN_THUAT chienThuat = ChienThuat;
				ENUM_CHIEN_THUAT eNUM_CHIEN_THUAT = chienThuat;
				if ((uint)eNUM_CHIEN_THUAT <= 8u || eNUM_CHIEN_THUAT == ENUM_CHIEN_THUAT.TAN_CONG_CHU_DONG_SLOT)
				{
					return true;
				}
				return false;
			}

			public bool IsBiDong()
			{
				ENUM_CHIEN_THUAT chienThuat = ChienThuat;
				ENUM_CHIEN_THUAT eNUM_CHIEN_THUAT = chienThuat;
				if ((uint)(eNUM_CHIEN_THUAT - 9) <= 8u || eNUM_CHIEN_THUAT == ENUM_CHIEN_THUAT.TAN_CONG_BI_DONG_SLOT)
				{
					return true;
				}
				return false;
			}

			public bool IsBaoVe()
			{
				return ChienThuat == ENUM_CHIEN_THUAT.BAO_VE;
			}
		}

		public int NguyenKhi1ID;

		public int NguyenKhi2ID;

		public int NguyenKhi3ID;

		public int NguyenKhi4ID;

		public int NguyenKhi5ID;

		public int NguyenKhi6ID;

		public int ThienMaLenhID1;

		public int ThienMaLenhID2;

		public int HID { get; set; }

		public int GID { get; set; }

		public string Name { get; set; }

		public int Level { get; set; }

		public int CostumeID { get; set; }

		public int TiemLucBoSung { get; set; }

		public int BddTichLuy { get; set; }

		public BaseData ChiSoGoc { get; set; }

		public int MenhBoiDuong { get; set; }

		public int NgoaiBoiDuong { get; set; }

		public int ThanBoiDuong { get; set; }

		public int KhiBoiDuong { get; set; }

		public AI TrangThai { get; set; }

		public int VoCong1Level { get; set; }

		public int VoCong1ThamNgoExp { get; set; }

		public VCType VoCong1Name { get; set; }

		public int VoCong2ID { get; set; }

		public int VoCong3ID { get; set; }

		public int VoCong4ID { get; set; }

		public int VuKhiID { get; set; }

		public int MuID { get; set; }

		public int AoGiapID { get; set; }

		public int TrangSucID { get; set; }

		public float DHPosX { get; set; }

		public float DHPosY { get; set; }

		public long Exp { get; set; }

		public long MaxExp { get; set; }

		public int CapDotPha { get; set; }

		public float MenhDotPha
		{
			get
			{
				return (float)((double)(ChiSoGoc.Menh * CapDotPha) * 0.1);
			}
		}

		public float NgoaiDotPha
		{
			get
			{
				return (float)((double)(ChiSoGoc.Ngoai * CapDotPha) * 0.1);
			}
		}

		public float ThanDotPha
		{
			get
			{
				return (float)((double)(ChiSoGoc.ThanPhap * CapDotPha) * 0.1);
			}
		}

		public float NoiDotPha
		{
			get
			{
				return (float)((double)(ChiSoGoc.Noi * CapDotPha) * 0.1);
			}
		}

		public int BeQuan { get; set; }

		public int ChuyenSinh { get; set; }

		public int ThienMaBuf { get; set; }

		public int UnlockVCDefault { get; set; }

		public HeroData()
		{
		}

		public HeroData(HeroData other)
		{
			AoGiapID = other.AoGiapID;
			CapDotPha = other.CapDotPha;
			if (other.ChiSoGoc != null)
			{
				ChiSoGoc = new BaseData(other.ChiSoGoc);
			}
			else
			{
				ChiSoGoc = null;
			}
			DHPosX = other.DHPosX;
			DHPosY = other.DHPosY;
			Exp = other.Exp;
			MaxExp = other.MaxExp;
			MuID = other.MuID;
			Name = other.Name;
			GID = other.GID;
			HID = other.HID;
			Level = other.Level;
			TrangSucID = other.TrangSucID;
			TrangThai = new AI(other.TrangThai);
			VoCong1Level = other.VoCong1Level;
			VoCong1Name = other.VoCong1Name;
			VoCong2ID = other.VoCong2ID;
			VoCong3ID = other.VoCong3ID;
			VoCong4ID = other.VoCong4ID;
			VuKhiID = other.VuKhiID;
		}
	}

	public class NguyenKhiData
	{
		public int ID;

		public string Codename;

		public int HID;

		public int Level;

		public int GID;

		public int ExpRequired;

		public NguyenKhiData()
		{
			ID = -1;
			Codename = string.Empty;
			HID = 0;
			Level = 1;
			GID = 0;
			ExpRequired = 1;
		}

		public float GetNguyenKhiStats()
		{
			float num = ConfigManager.instance.OtherConfig.NguyenKhiConfig[Codename].baseStat;
			return (float)ConfigManager.instance.OtherConfig.NguyenKhiConfig[Codename].baseStat + (float)(Level - 1) * ConfigManager.instance.OtherConfig.NguyenKhiConfig[Codename].growStat;
		}

		public OtherCfg.NguyenKhiType GetNguyenKhiLoai()
		{
			if (ConfigManager.instance.OtherConfig.NguyenKhiConfig.ContainsKey(Codename))
			{
				return ConfigManager.instance.OtherConfig.NguyenKhiConfig[Codename].Loai;
			}
			return OtherCfg.NguyenKhiType.MENH;
		}
	}

	public enum LinhDuocType
	{
		THONG_KINH_THAO = 0,
		HUYET_BO_DE = 1,
		HAC_LINH_CHI = 2,
		THIEN_SON_TUYET_LIEN = 3,
		NONE = 4
	}

	public class LinhDuocLandData
	{
		public int ID;

		public LinhDuocType CurLinhDuoc;

		public DateTime StartGrowTime;

		public int Quantity;

		public int GID;

		public int ServerID;
	}

	public class LinhDuocUserData
	{
		public int StealCount;

		public int DailySeedCount;

		public List<LinhDuocLandData> LinhDuocLandList { get; set; }

		public List<LinhDuocAnTromData> LinhDuocAnTromList { get; set; }

		public List<string> LinhDuocActivity { get; set; }
	}

	public class ThuCuoiData
	{
		public int ID;

		public int GID;

		public string CodeName;

		public DateTime ExpiredTime;

		public int Duration;

		public bool isActive;
	}

	public class ThuCuoiInfo
	{
		public List<ThuCuoiData> ThuCuoiList;
	}

	public class PetInfo
	{
		public enum PetQuality
		{
			TRUYEN_THUYET = 4,
			HUYEN_THOAI = 3,
			TRAC_VIET = 2,
			UU_TU = 1,
			PHO_THONG = 0
		}

		public enum PetType
		{
			NGOAI_MENH = 0,
			NGOAI_THAN = 1,
			NGOAI_KHI = 2,
			MENH_THAN = 3,
			MENH_KHI = 4,
			THAN_KHI = 5
		}

		public int ID;

		public int GID;

		public string codename;

		public string skill;

		public int heso1;

		public int heso2;

		public PetType petType;

		public float growRate;

		public int level;

		public int curExp;

		public int maxExp;

		public PetQuality Quality;

		public int DiemThonPhe;

		public PetInfo()
		{
		}

		public PetInfo(PetInfo thanthu)
		{
			codename = thanthu.codename;
			curExp = thanthu.curExp;
			DiemThonPhe = thanthu.DiemThonPhe;
			GID = thanthu.GID;
			growRate = thanthu.growRate;
			heso1 = thanthu.heso1;
			heso2 = thanthu.heso2;
			level = thanthu.level;
			maxExp = thanthu.maxExp;
			petType = thanthu.petType;
			Quality = thanthu.Quality;
			skill = thanthu.skill;
		}

		public int GetCong()
		{
			return (int)((petType != PetType.NGOAI_KHI && petType != PetType.NGOAI_MENH && petType != PetType.NGOAI_THAN) ? 0f : ((float)(GetTongChiSo() * heso1) / 100f));
		}

		public int GetThu()
		{
			return (int)((petType == PetType.THAN_KHI) ? ((float)(GetTongChiSo() * heso1) / 100f) : ((petType != PetType.MENH_THAN && petType != PetType.NGOAI_THAN) ? 0f : ((float)(GetTongChiSo() * heso2) / 100f)));
		}

		public int GetHP()
		{
			return 8 * (int)((petType == PetType.NGOAI_MENH) ? ((float)(GetTongChiSo() * heso2) / 100f) : ((petType != PetType.MENH_THAN && petType != PetType.MENH_KHI) ? 0f : ((float)(GetTongChiSo() * heso1) / 100f)));
		}

		public int GetMP()
		{
			return 8 * (int)((petType != PetType.NGOAI_KHI && petType != PetType.MENH_KHI && petType != PetType.THAN_KHI) ? 0f : ((float)(GetTongChiSo() * heso2) / 100f));
		}

		public int GetTongChiSo()
		{
			int num = 0;
			if (Quality == PetQuality.PHO_THONG)
			{
				num = 10 + (int)(10f * growRate);
			}
			else if (Quality == PetQuality.UU_TU)
			{
				num = 20 + (int)(20f * growRate);
			}
			else if (Quality == PetQuality.TRAC_VIET)
			{
				num = 40 + (int)(40f * growRate);
			}
			else if (Quality == PetQuality.HUYEN_THOAI)
			{
				num = 80 + (int)(70f * growRate);
			}
			else if (Quality == PetQuality.TRUYEN_THUYET)
			{
				num = 150 + (int)(100f * growRate);
			}
			return level * num + DiemThonPhe;
		}

		public float GetNextTruongThanh()
		{
			return (!(growRate + 50f / (float)GetDiemTruongThanh() > 1f)) ? (growRate + 50f / (float)GetDiemTruongThanh()) : 1f;
		}

		public ChiSoCoBan GetChiSo1()
		{
			if (petType == PetType.MENH_KHI || petType == PetType.MENH_THAN)
			{
				return ChiSoCoBan.Menh;
			}
			if (petType == PetType.NGOAI_KHI || petType == PetType.NGOAI_MENH || petType == PetType.NGOAI_THAN)
			{
				return ChiSoCoBan.Ngoai;
			}
			return ChiSoCoBan.ThanPhap;
		}

		public ChiSoCoBan GetChiSo2()
		{
			if (petType == PetType.NGOAI_KHI || petType == PetType.MENH_KHI || petType == PetType.THAN_KHI)
			{
				return ChiSoCoBan.Noi;
			}
			if (petType == PetType.MENH_THAN || petType == PetType.NGOAI_THAN)
			{
				return ChiSoCoBan.ThanPhap;
			}
			return ChiSoCoBan.Menh;
		}

		public int GetDiemTruongThanh()
		{
			int result = 15000;
			if (Quality == PetQuality.PHO_THONG)
			{
				result = 1000;
			}
			else if (Quality == PetQuality.UU_TU)
			{
				result = 3000;
			}
			else if (Quality == PetQuality.TRAC_VIET)
			{
				result = 6000;
			}
			else if (Quality == PetQuality.HUYEN_THOAI)
			{
				result = 8000;
			}
			else if (Quality == PetQuality.TRUYEN_THUYET)
			{
				result = 15000;
			}
			return result;
		}

		public int GetChiPhiThonPhe()
		{
			int result = 9000;
			if (Quality == PetQuality.PHO_THONG)
			{
				result = 10;
			}
			else if (Quality == PetQuality.UU_TU)
			{
				result = 70;
			}
			else if (Quality == PetQuality.TRAC_VIET)
			{
				result = 350;
			}
			else if (Quality == PetQuality.HUYEN_THOAI)
			{
				result = 1800;
			}
			else if (Quality == PetQuality.TRUYEN_THUYET)
			{
				result = 9000;
			}
			return result;
		}

		public int GetDiemThonPhe()
		{
			int result = 0;
			if (Quality == PetQuality.PHO_THONG)
			{
				result = 10;
			}
			else if (Quality == PetQuality.UU_TU)
			{
				result = 70;
			}
			else if (Quality == PetQuality.TRAC_VIET)
			{
				result = 350;
			}
			else if (Quality == PetQuality.HUYEN_THOAI)
			{
				result = 1800;
			}
			else if (Quality == PetQuality.TRUYEN_THUYET)
			{
				result = 9000;
			}
			return result;
		}

		public int GetDiemThonPheBonus()
		{
			int result = 0;
			System.Random random = new System.Random();
			if (Quality == PetQuality.PHO_THONG)
			{
				result = random.Next(-1, 2);
			}
			else if (Quality == PetQuality.UU_TU)
			{
				result = random.Next(-5, 6);
			}
			else if (Quality == PetQuality.TRAC_VIET)
			{
				result = random.Next(-20, 21);
			}
			else if (Quality == PetQuality.HUYEN_THOAI)
			{
				result = random.Next(-50, 51);
			}
			else if (Quality == PetQuality.TRUYEN_THUYET)
			{
				result = random.Next(-200, 200);
			}
			return result;
		}
	}

	public class SonMonInfo
	{
		public KeyValuePair<int, int> Id;

		public int SID;

		public int GID;

		public List<SonMonBuildingInfo> ListCongTrinh;

		public int Score;

		public BattleGamerInfo DoiHinhBaoVe;

		public Dictionary<string, int> ListDoiHinhBaoVe;

		public List<SonMonLog> ChienSu;

		public PhanThuongResponse GetSonMonRaidGet()
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			foreach (SonMonBuildingInfo item in ListCongTrinh.FindAll((SonMonBuildingInfo ct) => ct.LoaiCongTrinh == SonMonBuildingInfo.SonMonBuildingType.XUONG_DA))
			{
				if (dictionary.ContainsKey("VP_DA"))
				{
					dictionary["VP_DA"] += (int)((float)SonMonHelper.GetSonMonCTResource(item) * ConfigManager.instance.SonMonConfig.RaidRate);
				}
				else
				{
					dictionary.Add("VP_DA", (int)((float)SonMonHelper.GetSonMonCTResource(item) * ConfigManager.instance.SonMonConfig.RaidRate));
				}
			}
			foreach (SonMonBuildingInfo item2 in ListCongTrinh.FindAll((SonMonBuildingInfo ct) => ct.LoaiCongTrinh == SonMonBuildingInfo.SonMonBuildingType.XUONG_GO))
			{
				if (dictionary.ContainsKey("VP_GO"))
				{
					dictionary["VP_GO"] += (int)((float)SonMonHelper.GetSonMonCTResource(item2) * ConfigManager.instance.SonMonConfig.RaidRate);
				}
				else
				{
					dictionary.Add("VP_GO", (int)((float)SonMonHelper.GetSonMonCTResource(item2) * ConfigManager.instance.SonMonConfig.RaidRate));
				}
			}
			foreach (SonMonBuildingInfo item3 in ListCongTrinh.FindAll((SonMonBuildingInfo ct) => ct.LoaiCongTrinh == SonMonBuildingInfo.SonMonBuildingType.NHA_BEP))
			{
				if (dictionary.ContainsKey("VP_GA_QUAY"))
				{
					dictionary["VP_GA_QUAY"] += (int)((float)(SonMonHelper.GetSonMonCTResource(item3) / 8) * ConfigManager.instance.SonMonConfig.RaidRate);
				}
				else
				{
					dictionary.Add("VP_GA_QUAY", (int)((float)(SonMonHelper.GetSonMonCTResource(item3) / 8) * ConfigManager.instance.SonMonConfig.RaidRate));
				}
			}
			foreach (SonMonBuildingInfo item4 in ListCongTrinh.FindAll((SonMonBuildingInfo ct) => ct.LoaiCongTrinh == SonMonBuildingInfo.SonMonBuildingType.VUON_THUOC))
			{
				if (dictionary.ContainsKey("VP_NGUYEN_KHI_DAN"))
				{
					dictionary["VP_NGUYEN_KHI_DAN"] += (int)((float)SonMonHelper.GetSonMonCTResource(item4) * ConfigManager.instance.SonMonConfig.RaidRate);
				}
				else
				{
					dictionary.Add("VP_NGUYEN_KHI_DAN", (int)((float)SonMonHelper.GetSonMonCTResource(item4) * ConfigManager.instance.SonMonConfig.RaidRate));
				}
			}
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = new List<PhanThuongResponse.PhanThuong>();
			foreach (KeyValuePair<string, int> item5 in dictionary)
			{
				PhanThuongResponse.PhanThuong phanThuong = new PhanThuongResponse.PhanThuong();
				phanThuong.Name = item5.Key;
				phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
				phanThuong.Count = item5.Value;
				phanThuongResponse.PhanThuongList.Add(phanThuong);
			}
			return phanThuongResponse;
		}

		public PhanThuongResponse GetSonMonRaidLost()
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			foreach (SonMonBuildingInfo item in ListCongTrinh.FindAll((SonMonBuildingInfo ct) => ct.LoaiCongTrinh == SonMonBuildingInfo.SonMonBuildingType.XUONG_DA))
			{
				if (dictionary.ContainsKey("VP_DA"))
				{
					dictionary["VP_DA"] += (int)((float)SonMonHelper.GetSonMonCTResource(item) * SonMonHelper.GetSonMonLoseRate());
				}
				else
				{
					dictionary.Add("VP_DA", (int)((float)SonMonHelper.GetSonMonCTResource(item) * SonMonHelper.GetSonMonLoseRate()));
				}
			}
			foreach (SonMonBuildingInfo item2 in ListCongTrinh.FindAll((SonMonBuildingInfo ct) => ct.LoaiCongTrinh == SonMonBuildingInfo.SonMonBuildingType.XUONG_GO))
			{
				if (dictionary.ContainsKey("VP_GO"))
				{
					dictionary["VP_GO"] += (int)((float)SonMonHelper.GetSonMonCTResource(item2) * SonMonHelper.GetSonMonLoseRate());
				}
				else
				{
					dictionary.Add("VP_GO", (int)((float)SonMonHelper.GetSonMonCTResource(item2) * SonMonHelper.GetSonMonLoseRate()));
				}
			}
			foreach (SonMonBuildingInfo item3 in ListCongTrinh.FindAll((SonMonBuildingInfo ct) => ct.LoaiCongTrinh == SonMonBuildingInfo.SonMonBuildingType.NHA_BEP))
			{
				if (dictionary.ContainsKey("VP_GA_QUAY"))
				{
					dictionary["VP_GA_QUAY"] += (int)((float)SonMonHelper.GetSonMonCTResource(item3) * SonMonHelper.GetSonMonLoseRate());
				}
				else
				{
					dictionary.Add("VP_GA_QUAY", (int)((float)SonMonHelper.GetSonMonCTResource(item3) * SonMonHelper.GetSonMonLoseRate()));
				}
			}
			foreach (SonMonBuildingInfo item4 in ListCongTrinh.FindAll((SonMonBuildingInfo ct) => ct.LoaiCongTrinh == SonMonBuildingInfo.SonMonBuildingType.VUON_THUOC))
			{
				if (dictionary.ContainsKey("VP_NGUYEN_KHI_DAN"))
				{
					dictionary["VP_NGUYEN_KHI_DAN"] += (int)((float)SonMonHelper.GetSonMonCTResource(item4) * SonMonHelper.GetSonMonLoseRate());
				}
				else
				{
					dictionary.Add("VP_NGUYEN_KHI_DAN", (int)((float)SonMonHelper.GetSonMonCTResource(item4) * SonMonHelper.GetSonMonLoseRate()));
				}
			}
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = new List<PhanThuongResponse.PhanThuong>();
			foreach (KeyValuePair<string, int> item5 in dictionary)
			{
				PhanThuongResponse.PhanThuong phanThuong = new PhanThuongResponse.PhanThuong();
				phanThuong.Name = item5.Key;
				phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
				phanThuong.Count = item5.Value;
				phanThuongResponse.PhanThuongList.Add(phanThuong);
			}
			return phanThuongResponse;
		}
	}

	public class SonMonLog
	{
		public int ID;

		public int GID1;

		public int SID1;

		public int GID2;

		public int SID2;

		public int Score1;

		public int Score2;

		public List<string> Team1;

		public List<string> Team2;

		public string DisplayName1;

		public string DisplayName2;

		public string Reward;

		public DateTime RaidTime;
	}

	public class SonMonBuildingInfo
	{
		public enum SonMonBuildingType
		{
			CHINH_SANH = 0,
			VUON_THUOC = 1,
			NHA_BEP = 2,
			XUONG_DA = 3,
			XUONG_GO = 4,
			TIEN_TRANG = 5,
			COUNT_LOAI_CT = 6,
			NULL_CT = 7
		}

		public int ID;

		public int GID;

		public int SID;

		public int Slot;

		public SonMonBuildingType LoaiCongTrinh;

		public int Level;

		public int CurValue;

		public DateTime UpdatedTime;

		public DateTime BuildTime;

		public bool Raid;

		public int NextLevel;

		public int AccLevel;

		public string DisplayName;

		public DateTime RaidTime;

		public string GetProductCodename()
		{
			if (LoaiCongTrinh == SonMonBuildingType.XUONG_DA)
			{
				return "VP_DA";
			}
			if (LoaiCongTrinh == SonMonBuildingType.XUONG_GO)
			{
				return "VP_GO";
			}
			if (LoaiCongTrinh == SonMonBuildingType.NHA_BEP)
			{
				return "VP_GA_QUAY";
			}
			if (LoaiCongTrinh == SonMonBuildingType.VUON_THUOC)
			{
				return "VP_NGUYEN_KHI_DAN";
			}
			return "BAC";
		}
	}

	public class LanhDiaInfo
	{
		public int Turn;

		public DateTime LastRecoverTime;

		public string Position;

		public int Point;

		public DateTime LastMoveTime;
	}

	public class LanhDiaMap
	{
		public List<LanhDiaSquare> Map;

		public bool IsFullUpdate;

		public int CumServer;

		public DateTime DataTime;

		public Dictionary<string, string> LienMinhName;
	}

	public class LanhDiaSquare
	{
		public string Position;

		public int SID;

		public int LienMinhID;

		public int CumServer;

		public List<LanhDiaAvatar> Defender;

		public bool IsUpdated;

		public bool NeedSaved;

		public override bool Equals(object obj)
		{
			LanhDiaSquare lanhDiaSquare = obj as LanhDiaSquare;
			if (!Position.Equals(lanhDiaSquare.Position))
			{
				return false;
			}
			if (SID != lanhDiaSquare.SID)
			{
				return false;
			}
			if (LienMinhID != lanhDiaSquare.LienMinhID)
			{
				return false;
			}
			if (CumServer != lanhDiaSquare.CumServer)
			{
				return false;
			}
			if (Defender != null && lanhDiaSquare.Defender != null)
			{
				if (Defender.Count != lanhDiaSquare.Defender.Count)
				{
					return false;
				}
				for (int i = 0; i < Defender.Count; i++)
				{
					if (!Defender[i].Equals(lanhDiaSquare.Defender[i]))
					{
						return false;
					}
				}
			}
			else if ((Defender != null && lanhDiaSquare.Defender == null) || (Defender == null && lanhDiaSquare.Defender != null))
			{
				return false;
			}
			return true;
		}
	}

	public class LanhDiaAvatar
	{
		public string DisplayName;

		public string Avatar;

		public int Vip;

		public int SID;

		public int GID;

		public int Level;

		public override bool Equals(object obj)
		{
			LanhDiaAvatar lanhDiaAvatar = obj as LanhDiaAvatar;
			if (!DisplayName.Equals(lanhDiaAvatar.DisplayName))
			{
				return false;
			}
			if (!Avatar.Equals(lanhDiaAvatar.Avatar))
			{
				return false;
			}
			if (!Vip.Equals(lanhDiaAvatar.Vip))
			{
				return false;
			}
			if (!SID.Equals(lanhDiaAvatar.SID))
			{
				return false;
			}
			if (!GID.Equals(lanhDiaAvatar.GID))
			{
				return false;
			}
			if (!Level.Equals(lanhDiaAvatar.Level))
			{
				return false;
			}
			return true;
		}
	}

	public class ThienMaLenhInfo
	{
		public int ID;

		public int HID;

		public int GID;

		public string Slot1;

		public string Slot2;

		public string Slot3;

		public int TichLuySlot1;

		public int TichLuySlot2;

		public int TichLuySlot3;

		public float Value1;

		public float Value2;

		public float Value3;

		public int TichLuy1;

		public int TichLuy2;

		public int TichLuy3;

		public float GetTotalValue()
		{
			return Value1 + Value2 + Value3;
		}

		public float GetCurrentValue(UserInfo uInfo, int heroID)
		{
			if (uInfo == null || uInfo.VoCongList == null || uInfo.HeroList == null)
			{
				return Value1;
			}
			HeroData hero = uInfo.HeroList.Find((HeroData vp) => vp.HID == heroID);
			float num = Value1;
			if (!string.IsNullOrEmpty(Slot2) && hero.VoCong3ID > 0 && uInfo.VoCongList.Find((VoCongData vc) => vc.ID == hero.VoCong3ID).Name.Contains(Slot2.Substring(0, (!Slot2.EndsWith("_SS")) ? (Slot2.Length - 2) : (Slot2.Length - 3))))
			{
				num += Value2;
			}
			if (!string.IsNullOrEmpty(Slot3) && hero.VoCong4ID > 0 && uInfo.VoCongList.Find((VoCongData vc) => vc.ID == hero.VoCong4ID).Name.Contains(Slot3.Substring(0, (!Slot3.EndsWith("_SS")) ? (Slot3.Length - 2) : (Slot3.Length - 3))))
			{
				num += Value3;
			}
			return num;
		}
	}

	public class BaoKhoInfo
	{
		public enum LoaiBaoKho
		{
			DONG = 1,
			NGAN = 2,
			KIM = 3,
			NGOC = 4
		}

		public int ID { get; set; }

		public int CumServer { get; set; }

		public int GID { get; set; }

		public int SID { get; set; }

		public string DisplayName { get; set; }

		public int Level { get; set; }

		public int Vip { get; set; }

		public LoaiBaoKho BaoKhoType { get; set; }

		public DateTime TimeChiem { get; set; }

		public DateTime LastTimeThuHoach { get; set; }
	}

	public class DoiHinhData
	{
		public static int MAX_SLOT_RA_TRAN = 8;

		public static int MAX_HOTRO = 8;

		private List<int> m_listRaTran = new List<int>();

		private List<int> m_listHoTro = new List<int>();

		private List<int> m_listThienCang = new List<int>();

		public int CuongHoa_Slot1 { get; set; }

		public int CuongHoa_Slot2 { get; set; }

		public int CuongHoa_Slot3 { get; set; }

		public int CuongHoa_Slot4 { get; set; }

		public int CuongHoa_Slot5 { get; set; }

		public int CuongHoa_Slot6 { get; set; }

		public int CuongHoa_Slot7 { get; set; }

		public int CuongHoa_Slot8 { get; set; }

		public OtherCfg.BatQuaiTranDoType BatQuaiTranType { get; set; }

		public List<int> ListRaTran
		{
			get
			{
				return m_listRaTran;
			}
			set
			{
				m_listRaTran = value;
			}
		}

		public List<int> ListHoTro
		{
			get
			{
				return m_listHoTro;
			}
			set
			{
				m_listHoTro = value;
			}
		}

		public List<int> ListThienCang
		{
			get
			{
				return m_listThienCang;
			}
			set
			{
				m_listThienCang = value;
			}
		}

		public DoiHinhData()
		{
			for (int i = 0; i < MAX_SLOT_RA_TRAN; i++)
			{
				m_listRaTran.Add(0);
			}
			for (int j = 0; j < MAX_HOTRO; j++)
			{
				m_listHoTro.Add(0);
			}
			int num = 8;
			try
			{
				if (ConfigManager.instance != null && ConfigManager.instance.OtherConfig != null)
				{
					num = ConfigManager.instance.OtherConfig.GetMaxHoTroThienCang();
				}
			}
			catch
			{
				num = 8;
			}
			for (int k = 0; k < num; k++)
			{
				m_listThienCang.Add(0);
			}
		}
	}

	public class VoCongData
	{
		public int ID { get; set; }

		public VCType Type { get; set; }

		public int Level { get; set; }

		public int GID { get; set; }

		public string Name { get; set; }

		public int HID { get; set; }

		public int ThamNgoExp { get; set; }

		public int Unlock { get; set; }

		public int ThienThu { get; set; }

		public VoCongData()
		{
			Type = VCType.VC_NULL;
			ID = -1;
			HID = 0;
			ThamNgoExp = 0;
			Unlock = 0;
			ThienThu = 0;
		}
	}

	public class TrangBiData
	{
		public enum LoaiNgoc
		{
			NONE = 0,
			VP_NGOC_DO = 1,
			VP_NGOC_XANH = 2,
			VP_NGOC_VANG = 3,
			VP_NGOC_TIM = 4
		}

		public enum LoaiBuff
		{
			NONE = 0,
			TOC_DANH = 1,
			KHANG_CHUONG = 2,
			HOA_GIAI = 3,
			BAO_KICH = 4,
			TANG_CHI_SO = 5
		}

		public enum TRANG_BI_EFF
		{
			TANG_CONG = 0,
			TANG_MAU = 1,
			TANG_THU = 2,
			TANG_NOI = 3,
			CHINH_XAC = 4,
			KHANG_BAO = 5,
			STUN = 6,
			PHONG_CHIEU = 7,
			DINH_THAN = 8,
			BA_THE = 9,
			HO_THE = 10,
			HUT_MAU = 11,
			HOI_SINH = 12,
			BURN_MANA = 13,
			TRANG_BI_EFF_COUNT = 14
		}

		public class SEffect
		{
			public TRANG_BI_EFF LoaiEff;

			public int GiaTriTichLuy;

			public float EffVal;

			public string GetEffectColor()
			{
				float num = ConfigManager.instance.OtherConfig.ThanBinhConfig.MaxValue[LoaiEff.ToString() + "10"];
				float num2 = ConfigManager.instance.OtherConfig.ThanBinhConfig.MinValue[LoaiEff.ToString() + "1"];
				float num3 = (EffVal - num2) / (num - num2);
				if (num3 < 0.2f)
				{
					return "[FFFFFF]";
				}
				if (num3 < 0.4f)
				{
					return "[00FF00]";
				}
				if (num3 < 0.6f)
				{
					return "[0044FF]";
				}
				if (num3 < 0.8f)
				{
					return "[AA00AA]";
				}
				return "[FFFF00]";
			}
		}

		public int HoangKim { get; set; }

		public int MenhBoiDuong { get; set; }

		public int NgoaiBoiDuong { get; set; }

		public int ThanBoiDuong { get; set; }

		public int KhiBoiDuong { get; set; }

		public List<SEffect> Effect { get; set; }

		public int EffecTichLuy { get; set; }

		public int ID { get; set; }

		public int GID { get; set; }

		public int HID { get; set; }

		public string Name { get; set; }

		public int Level { get; set; }

		public int TinhLuyenLevel { get; set; }

		public int TinhLuyenExp { get; set; }

		public string Ngoc1Name { get; set; }

		public int Ngoc1Lvl { get; set; }

		public string Ngoc2Name { get; set; }

		public int Ngoc2Lvl { get; set; }

		public string Ngoc3Name { get; set; }

		public int Ngoc3Lvl { get; set; }

		public TrangBiData(string _name, int _level, int _tinh_luyen_lvl, string _ngoc1_name = "", int _ngoc1_level = 0, string _ngoc2_name = "", int _ngoc2_level = 0, string _ngoc3_name = "", int _ngoc3_level = 0)
		{
			ID = -1;
			GID = 0;
			HID = 0;
			Name = _name;
			Level = _level;
			TinhLuyenLevel = _tinh_luyen_lvl;
			TinhLuyenExp = 0;
			Ngoc1Name = _ngoc1_name;
			Ngoc1Lvl = _ngoc1_level;
			Ngoc2Name = _ngoc2_name;
			Ngoc2Lvl = _ngoc2_level;
			Ngoc3Name = _ngoc3_name;
			Ngoc3Lvl = _ngoc3_level;
		}

		public TrangBiData()
		{
			ID = -1;
			GID = 0;
			HID = 0;
			Name = string.Empty;
			Level = 0;
			TinhLuyenLevel = 0;
			TinhLuyenExp = 0;
			Ngoc1Name = string.Empty;
			Ngoc1Lvl = 0;
			Ngoc2Name = string.Empty;
			Ngoc2Lvl = 0;
			Ngoc3Name = string.Empty;
			Ngoc3Lvl = 0;
		}

		public static string GetNgocName(LoaiNgoc loaiNgoc)
		{
			if (loaiNgoc == LoaiNgoc.VP_NGOC_DO || loaiNgoc == LoaiNgoc.VP_NGOC_XANH || loaiNgoc == LoaiNgoc.VP_NGOC_VANG || loaiNgoc == LoaiNgoc.VP_NGOC_TIM)
			{
				return loaiNgoc.ToString();
			}
			return string.Empty;
		}

		public static LoaiNgoc GetLoaiNgoc(string name)
		{
			if (name == string.Empty)
			{
				return LoaiNgoc.NONE;
			}
			LoaiNgoc result;
			switch (name)
			{
			case "VP_NGOC_DO":
				result = LoaiNgoc.VP_NGOC_DO;
				break;
			case "VP_NGOC_XANH":
				result = LoaiNgoc.VP_NGOC_XANH;
				break;
			case "VP_NGOC_VANG":
				result = LoaiNgoc.VP_NGOC_VANG;
				break;
			case "VP_NGOC_TIM":
				result = LoaiNgoc.VP_NGOC_TIM;
				break;
			default:
				result = LoaiNgoc.NONE;
				break;
			}
			return result;
		}

		public static void GetLoaiBuff(TrangBiData tb_data, int slot, out LoaiBuff buff, out float ChiSo)
		{
			string name = string.Empty;
			int level = 0;
			switch (slot)
			{
			case 0:
				name = tb_data.Ngoc1Name;
				level = tb_data.Ngoc1Lvl;
				break;
			case 1:
				name = tb_data.Ngoc2Name;
				level = tb_data.Ngoc2Lvl;
				break;
			case 2:
				name = tb_data.Ngoc3Name;
				level = tb_data.Ngoc3Lvl;
				break;
			}
			LoaiNgoc loaiNgoc = GetLoaiNgoc(name);
			switch (loaiNgoc)
			{
			case LoaiNgoc.NONE:
				buff = LoaiBuff.NONE;
				ChiSo = 0f;
				return;
			case LoaiNgoc.VP_NGOC_DO:
				if (tb_data.Name.StartsWith("VK_"))
				{
					buff = LoaiBuff.TOC_DANH;
					ChiSo = ConfigManager.instance.OtherConfig.GetNgocBuffTocDoDanh(level);
					return;
				}
				break;
			}
			if (loaiNgoc == LoaiNgoc.VP_NGOC_XANH && tb_data.Name.StartsWith("AG_"))
			{
				buff = LoaiBuff.KHANG_CHUONG;
				ChiSo = ConfigManager.instance.OtherConfig.GetNgocBuffKhangChuong(level);
			}
			else if (loaiNgoc == LoaiNgoc.VP_NGOC_TIM && tb_data.Name.StartsWith("MU_"))
			{
				buff = LoaiBuff.HOA_GIAI;
				ChiSo = ConfigManager.instance.OtherConfig.GetNgocBuffHoaGiai(level);
			}
			else if (loaiNgoc == LoaiNgoc.VP_NGOC_VANG && tb_data.Name.StartsWith("TS_"))
			{
				buff = LoaiBuff.BAO_KICH;
				ChiSo = ConfigManager.instance.OtherConfig.GetNgocBuffBaoKich(level);
			}
			else
			{
				buff = LoaiBuff.TANG_CHI_SO;
				ChiSo = ConfigManager.instance.OtherConfig.GetNgocBuffChiSo(level);
			}
		}

		public bool IsShowHoangKimEff()
		{
			if (EffecTichLuy >= 226)
			{
				return true;
			}
			return false;
		}
	}

	public class MailData
	{
		public class MailQuaTangContent
		{
			private string msg;

			private List<PhanThuongResponse.PhanThuong> listPT = new List<PhanThuongResponse.PhanThuong>();

			public string Msg
			{
				get
				{
					return msg;
				}
				set
				{
					msg = value;
				}
			}

			public List<PhanThuongResponse.PhanThuong> ListPT
			{
				get
				{
					return listPT;
				}
				set
				{
					listPT = value;
				}
			}
		}

		public class MailTinNhanContent
		{
			public int Id { get; set; }

			public string Name { get; set; }

			public string Msg { get; set; }
		}

		public enum MAIL_TYPE
		{
			QuaTang = 0,
			TinNhan = 1,
			TinNhanHeThong = 2
		}

		public enum MAIL_STATUS
		{
			Unread = 0,
			Read = 1,
			Empty = 2
		}

		private int id;

		private MAIL_TYPE type;

		private string content;

		private MAIL_STATUS status;

		private DateTime removeTime;

		public int GID { get; set; }

		public int Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		public MAIL_TYPE Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}

		public string Content
		{
			get
			{
				return content;
			}
			set
			{
				content = value;
			}
		}

		public MAIL_STATUS Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value;
			}
		}

		public DateTime RemoveTime
		{
			get
			{
				return removeTime;
			}
			set
			{
				removeTime = value;
			}
		}
	}

	public class VCThietLapData
	{
		public enum ENUM_MUC_TIEU_CHIEU
		{
			MUC_TIEU_DANG_TAN_CONG = 0,
			MUC_TIEU_SINH_LUC_MAX = 1,
			MUC_TIEU_SINH_LUC_MIN = 2,
			MUC_TIEU_CONG_MIN = 3,
			MUC_TIEU_CONG_MAX = 4,
			MUC_TIEU_THAN_PHAP_MIN = 5,
			MUC_TIEU_THAN_PHAP_MAX = 6,
			MUC_TIEU_NOI_MIN = 7,
			MUC_TIEU_NOI_MAX = 8,
			HO_TRO = 9,
			MUC_TIEU_SLOT = 10
		}

		public enum ENUM_DUNG_KHI
		{
			NGAY_KHI_CO_THE = 0,
			SINH_LUC_MUC_TIEU_XUONG_THAP = 1,
			SINH_LUC_BAN_THAN_XUONG_THAP = 2,
			NOI_LUC_MUC_TIEU_XUONG_THAP = 3,
			NGAY_KHI_BI_TRANG_THAI_XAU = 4,
			SINH_LUC_MUC_TIEU_TREN = 5
		}

		public enum ENUM_NGUNG_DUNG_KHI
		{
			SINH_LUC_THAP_HON = 0,
			NOI_LUC_THAP_HON = 1
		}

		public class SettingParameter
		{
			private List<int> _DongDoiMucTieuHoTros;

			public ENUM_MUC_TIEU_CHIEU MucTieuChieu { get; set; }

			public ENUM_DUNG_KHI DungKhi { get; set; }

			public double DungKhi_TieuChi_ThapHonPercent { get; set; }

			public List<int> DongDoiMucTieuHoTros
			{
				get
				{
					return _DongDoiMucTieuHoTros;
				}
				set
				{
					_DongDoiMucTieuHoTros = value;
				}
			}

			public ENUM_NGUNG_DUNG_KHI NgungDungKhi { get; set; }

			public double NgungDungKhi_TieuChi_ThapHonPercent { get; set; }

			public List<int> MucTieuChieuSlots { get; set; }

			public SettingParameter(SettingParameter other)
			{
				MucTieuChieu = other.MucTieuChieu;
				DungKhi = other.DungKhi;
				DungKhi_TieuChi_ThapHonPercent = other.DungKhi_TieuChi_ThapHonPercent;
				DongDoiMucTieuHoTros = new List<int>(other.DongDoiMucTieuHoTros);
				NgungDungKhi = other.NgungDungKhi;
				NgungDungKhi_TieuChi_ThapHonPercent = other.NgungDungKhi_TieuChi_ThapHonPercent;
				MucTieuChieuSlots = new List<int>(other.MucTieuChieuSlots);
			}

			public SettingParameter()
			{
				DongDoiMucTieuHoTros = new List<int>();
				MucTieuChieuSlots = new List<int> { 0, 1, 2, 3 };
			}
		}

		private SettingParameter _setting = new SettingParameter();

		public int HID { get; set; }

		public VCType Type { get; set; }

		public SettingParameter Parameter
		{
			get
			{
				return _setting;
			}
			set
			{
				_setting = value;
			}
		}

		public VCThietLapData()
		{
		}

		public VCThietLapData(int hid, VCType type)
		{
			HID = hid;
			Type = type;
		}

		public VCThietLapData(VCThietLapData other)
		{
			HID = other.HID;
			Type = other.Type;
			Parameter = new SettingParameter(other.Parameter);
		}
	}

	public LinhDuocUserData LinhDuocInfo;

	public ThuCuoiInfo ThuCuoi;

	public List<PetInfo> ListThanThu;

	public SonMonInfo SonMon;

	public LanhDiaInfo LanhDiaUser;

	public LanhDiaMap LanhDiaData;

	public List<ThienMaLenhInfo> ListThienMaLenh;

	public LienMinhData LienMinh;

	public LienMinhSelfData LienMinhCaNhan;

	public LienMinhActivity LienMinhActivities;

	public long ServerTimeTick { get; set; }

	public HostID HostID { get; set; }

	public List<CostumeData> CostumeList { get; set; }

	public List<BanBeData> BanBeList { get; set; }

	public List<CuuThuData> CuuThuList { get; set; }

	public List<HonNhanVatData> HonNhanVatList { get; set; }

	public List<DanhSonData> DanhSon { get; set; }

	public List<GiangHoData> GiangHo { get; set; }

	public List<GiangHoData> GiangHoTinhAnh { get; set; }

	public GamerData Gamer { get; set; }

	public LeagueData LeagueInfo { get; set; }

	public GiaTriThoiGianData GiaTriThoiGian { get; set; }

	public LuanKiemData LuanKiem { get; set; }

	public DanhHieuData DanhHieu { get; set; }

	public List<VatPhamTieuThuData> VatPhamTieuThuList { get; set; }

	public List<ManhTrangBiData> ManhTrangBiList { get; set; }

	public List<ManhVoCongData> ManhVoCongList { get; set; }

	public List<HuyenKhi> HuyenKhiList { get; set; }

	public List<ChienHon> ChienHonList { get; set; }

	public List<HeroData> HeroList { get; set; }

	public List<NguyenKhiData> NguyenKhiList { get; set; }

	public DoiHinhData DoiHinh { get; set; }

	public List<VoCongData> VoCongList { get; set; }

	public ServerData ServerInfo { get; set; }

	public List<TrangBiData> TrangBiList { get; set; }

	public List<MailData> MailList { get; set; }

	public List<VCThietLapData> ListVCSetting { get; set; }

	public OtherCfg.ThuCuoiCfg GetThuCuoiData()
	{
		if (Gamer != null && ThuCuoi != null)
		{
			if (Gamer.curThuCuoi <= 0)
			{
				return null;
			}
			ThuCuoiData thuCuoiData = ThuCuoi.ThuCuoiList.Find((ThuCuoiData e) => e.ID == Gamer.curThuCuoi && e.ExpiredTime > DateTime.Now);
			if (thuCuoiData != null)
			{
				return ConfigManager.instance.OtherConfig.ThuCuoiConfig[thuCuoiData.CodeName];
			}
			return null;
		}
		return null;
	}

	public DanhSonData GetDanhSonByIdx(int danhSonIdx)
	{
		if (DanhSon != null)
		{
			foreach (DanhSonData item in DanhSon)
			{
				if (item.DanhSonIdx == danhSonIdx)
				{
					return item;
				}
			}
		}
		return null;
	}

	public GiangHoData GetGiangHoByIdx(int ghIdx)
	{
		if (GiangHo != null)
		{
			foreach (GiangHoData item in GiangHo)
			{
				if (item.GiangHoIdx == ghIdx)
				{
					return item;
				}
			}
		}
		return null;
	}

	public GiangHoData GetGiangHoTinhAnhByIdx(int ghIdx)
	{
		if (GiangHoTinhAnh != null)
		{
			foreach (GiangHoData item in GiangHoTinhAnh)
			{
				if (item.GiangHoIdx == ghIdx)
				{
					return item;
				}
			}
		}
		return null;
	}

	public static ChienHon ChienHonGetExp(ChienHon hero, long expSub)
	{
		ChienHon chienHon = new ChienHon(hero);
		int num = hero.Level;
		int count = ConfigManager.instance.OtherConfig.ChienHonExpLevelCfg.Count;
		long num2 = hero.Exp + expSub;
		long num3 = hero.MaxExp;
		while (num2 >= num3)
		{
			if (num < count)
			{
				num++;
				num2 -= num3;
				num3 = ConfigManager.instance.OtherConfig.GetChienHonMaxExpByLevel(num);
				continue;
			}
			num2 = hero.MaxExp;
			break;
		}
		chienHon.MaxExp = num3;
		chienHon.Level = num;
		chienHon.Exp = num2;
		return chienHon;
	}

	public static HeroData HeroGetExp(HeroData hero, long expSub)
	{
		HeroData heroData = new HeroData(hero);
		int num = hero.Level;
		int count = ConfigManager.instance.OtherConfig.NhanVatMaxExp.Count;
		long num2 = hero.Exp + expSub;
		long num3 = hero.MaxExp;
		while (num2 >= num3)
		{
			if (num < count)
			{
				num++;
				num2 -= num3;
				num3 = ConfigManager.instance.OtherConfig.GetNhanVatMaxExpByLevel(hero.Name, num);
				continue;
			}
			num2 = hero.MaxExp;
			break;
		}
		heroData.MaxExp = num3;
		heroData.Level = num;
		heroData.Exp = num2;
		return heroData;
	}

	public HeroData GetHeroFromDoiHinh(int slot)
	{
		if (HeroList == null || DoiHinh == null || DoiHinh.ListRaTran == null)
		{
			return null;
		}
		if (slot < 1 || slot > DoiHinh.ListRaTran.Count)
		{
			return null;
		}
		int num = DoiHinh.ListRaTran[slot - 1];
		if (num < 1)
		{
			return null;
		}
		for (int i = 0; i < HeroList.Count; i++)
		{
			if (HeroList[i].HID == num)
			{
				return HeroList[i];
			}
		}
		return null;
	}

	public HeroData GetHeroHotroFromDoiHinh(int slot)
	{
		if (HeroList == null || DoiHinh == null || DoiHinh.ListRaTran == null)
		{
			return null;
		}
		if (slot < 1 || slot > DoiHinh.ListHoTro.Count)
		{
			return null;
		}
		int num = DoiHinh.ListHoTro[slot - 1];
		if (num < 1)
		{
			return null;
		}
		for (int i = 0; i < HeroList.Count; i++)
		{
			if (HeroList[i].HID == num)
			{
				return HeroList[i];
			}
		}
		return null;
	}

	public List<HeroData> GetListCloneHeroFromDoiHinh()
	{
		List<HeroData> list = new List<HeroData>();
		foreach (int item in DoiHinh.ListRaTran)
		{
			foreach (HeroData hero in HeroList)
			{
				if (hero.HID == item)
				{
					list.Add(new HeroData(hero));
				}
			}
		}
		return list;
	}

	public VoCongData GetVoCongFromId(int id)
	{
		for (int i = 0; i < VoCongList.Count; i++)
		{
			if (VoCongList[i].ID == id)
			{
				return VoCongList[i];
			}
		}
		return null;
	}

	public List<VoCongData> GetChieuThucFromHero(HeroData hero)
	{
		List<VoCongData> list = new List<VoCongData>();
		CfgVoCong voCongCfgByType = ConfigManager.instance.GetVoCongCfgByType(hero.VoCong1Name);
		if (voCongCfgByType.m_Class == VCClass.CHIEU_THUC)
		{
			VoCongData voCongData = new VoCongData();
			voCongData.GID = ((Gamer != null) ? Gamer.ID : 0);
			voCongData.ID = 0;
			voCongData.Level = hero.VoCong1Level;
			voCongData.Name = hero.VoCong1Name.ToString();
			voCongData.Type = hero.VoCong1Name;
			list.Add(voCongData);
		}
		VoCongData voCongFromId = GetVoCongFromId(hero.VoCong2ID);
		if (voCongFromId != null)
		{
			voCongCfgByType = ConfigManager.instance.GetVoCongCfgByType(voCongFromId.Type);
			if (voCongCfgByType.m_Class == VCClass.CHIEU_THUC)
			{
				list.Add(voCongFromId);
			}
		}
		VoCongData voCongFromId2 = GetVoCongFromId(hero.VoCong3ID);
		if (voCongFromId2 != null)
		{
			voCongCfgByType = ConfigManager.instance.GetVoCongCfgByType(voCongFromId2.Type);
			if (voCongCfgByType.m_Class == VCClass.CHIEU_THUC)
			{
				list.Add(voCongFromId2);
			}
		}
		VoCongData voCongFromId3 = GetVoCongFromId(hero.VoCong4ID);
		if (voCongFromId3 != null)
		{
			voCongCfgByType = ConfigManager.instance.GetVoCongCfgByType(voCongFromId3.Type);
			if (voCongCfgByType.m_Class == VCClass.CHIEU_THUC)
			{
				list.Add(voCongFromId3);
			}
		}
		return list;
	}

	public VCThietLapData GetVCThietLapFromHero(int hid, VCType type)
	{
		for (int i = 0; i < ListVCSetting.Count; i++)
		{
			VCThietLapData vCThietLapData = ListVCSetting[i];
			if (vCThietLapData.HID == hid && vCThietLapData.Type == type)
			{
				return vCThietLapData;
			}
		}
		return null;
	}

	public List<VCThietLapData> GetVCThietLapFromHero(HeroData hero)
	{
		List<VCThietLapData> list = new List<VCThietLapData>();
		if (ListVCSetting != null)
		{
			for (int i = 0; i < ListVCSetting.Count; i++)
			{
				VCThietLapData vCThietLapData = ListVCSetting[i];
				if (vCThietLapData.HID == hero.HID)
				{
					list.Add(vCThietLapData);
				}
			}
		}
		return list;
	}

	public List<VCThietLapData> GetCurrentVCThietLapFromHero(HeroData hero)
	{
		List<VCThietLapData> vCThietLapFromHero = GetVCThietLapFromHero(hero);
		List<VoCongData> chieuThucFromHero = GetChieuThucFromHero(hero);
		List<VCThietLapData> list = new List<VCThietLapData>();
		for (int i = 0; i < chieuThucFromHero.Count; i++)
		{
			bool flag = false;
			for (int j = 0; j < vCThietLapFromHero.Count; j++)
			{
				if (chieuThucFromHero[i].Type == vCThietLapFromHero[j].Type)
				{
					list.Add(vCThietLapFromHero[j]);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				list.Add(new VCThietLapData(hero.HID, chieuThucFromHero[i].Type));
			}
		}
		return list;
	}

	public void UpdateInfo(UserInfo other)
	{
		GameManager.instance.m_GameClient.ServerTimeDiffTick = other.ServerTimeTick - DateTime.Now.Ticks;
		if (other.Gamer != null)
		{
			if (Gamer != null && other.Gamer.Level > Gamer.Level)
			{
				PopupLenLevel.Create(other.Gamer.Level);
			}
			if (GUIManager.instance != null && GUIManager.instance.homeCity != null && GUIManager.instance.homeCity.mainAvatar != null)
			{
				string userName = other.Gamer.DisplayName;
				if (other.DanhHieu != null)
				{
					int khiTheByDanhHieuAtTime = ConfigManager.GetKhiTheByDanhHieuAtTime(other.DanhHieu, DateTime.Now);
					userName = Utils.getStringNameByKhiThe(khiTheByDanhHieuAtTime) + " " + other.Gamer.DisplayName;
				}
				GUIManager.instance.homeCity.mainAvatar.SetUserName(userName);
			}
			Gamer = other.Gamer;
			string empty = string.Empty;
			List<GameClient.EGPaymentInfo> list = new List<GameClient.EGPaymentInfo>();
			empty = PlayerPrefs.GetString("NewEGStoredOrder_" + GameManager.instance.m_GameClient.UserInfo.Gamer.ID);
			if (empty != null && empty.Length > 0)
			{
				list = JsonMapper.ToObject<List<GameClient.EGPaymentInfo>>(empty);
			}
			if (list != null)
			{
				foreach (GameClient.EGPaymentInfo item in list)
				{
					if (Math.Abs((DateTime.Now - item.orderTime).TotalSeconds) > 60.0)
					{
						GameManager.instance.m_GameClient.RequestPaymentConfirm(item.GetPaymentRequest());
					}
				}
			}
		}
		if (other.GiaTriThoiGian != null)
		{
			GiaTriThoiGian = other.GiaTriThoiGian;
		}
		if (other.LuanKiem != null)
		{
			LuanKiem = other.LuanKiem;
		}
		if (other.HeroList != null)
		{
			HeroList = other.HeroList;
		}
		if (other.VoCongList != null)
		{
			VoCongList = other.VoCongList;
		}
		if (other.ListVCSetting != null)
		{
			ListVCSetting = other.ListVCSetting;
		}
		if (other.DoiHinh != null)
		{
			DoiHinh = other.DoiHinh;
		}
		if (other.VatPhamTieuThuList != null)
		{
			VatPhamTieuThuList = other.VatPhamTieuThuList;
		}
		if (other.GiangHo != null && other.GiangHo.Count > 0)
		{
			if (GiangHo != null && GiangHo.Count > 0)
			{
				if (GiangHo[GiangHo.Count - 1].HoanThanh == 0 && other.GiangHo[GiangHo.Count - 1].HoanThanh > 0)
				{
					PopupTinhNangMoi.CreateByGiangHo(GiangHo.Count);
				}
			}
			else if (other.GiangHo[0].HoanThanh > 0)
			{
				PopupTinhNangMoi.CreateByGiangHo(0);
			}
			GiangHo = other.GiangHo;
		}
		if (other.DanhSon != null)
		{
			if (other.DanhSon.Count > 0 && other.DanhSon[0].LuotChoi > 0 && DanhSon != null && (DanhSon.Count <= 0 || DanhSon[0].LuotChoi <= 0))
			{
				PopupTinhNangMoi.CreateByCamDia(1);
			}
			DanhSon = other.DanhSon;
		}
		if (other.DanhHieu != null)
		{
			DanhHieu = other.DanhHieu;
		}
		if (other.TrangBiList != null && other.TrangBiList.Count > 0)
		{
			TrangBiList = other.TrangBiList;
		}
		if (other.HonNhanVatList != null)
		{
			HonNhanVatList = other.HonNhanVatList;
		}
		if (other.MailList != null && other.MailList.Count > 0)
		{
			MailList = other.MailList;
		}
		if (other.ManhTrangBiList != null)
		{
			ManhTrangBiList = other.ManhTrangBiList;
		}
		if (other.ManhVoCongList != null)
		{
			ManhVoCongList = other.ManhVoCongList;
		}
		if (other.ServerInfo != null)
		{
			ServerInfo = other.ServerInfo;
		}
		if (other.BanBeList != null)
		{
			BanBeList = other.BanBeList;
		}
		if (other.CuuThuList != null)
		{
			CuuThuList = other.CuuThuList;
		}
		if (other.LienMinh != null)
		{
			LienMinh = other.LienMinh;
		}
		if (other.LienMinhCaNhan != null)
		{
			LienMinhCaNhan = other.LienMinhCaNhan;
		}
		if (other.LienMinhActivities != null)
		{
			LienMinhActivities = other.LienMinhActivities;
		}
		if (other.NguyenKhiList != null)
		{
			NguyenKhiList = other.NguyenKhiList;
		}
		if (other.LinhDuocInfo != null)
		{
			LinhDuocInfo = other.LinhDuocInfo;
		}
		if (other.ThuCuoi != null)
		{
			ThuCuoi = other.ThuCuoi;
		}
		if (other.LeagueInfo != null)
		{
			LeagueInfo = other.LeagueInfo;
		}
		if (other.CostumeList != null)
		{
			CostumeList = other.CostumeList;
		}
		if (other.ListThanThu != null)
		{
			ListThanThu = other.ListThanThu;
		}
		if (other.SonMon != null)
		{
			if (SonMon == null)
			{
				SonMon = other.SonMon;
			}
			else
			{
				SonMon.Score = other.SonMon.Score;
			}
			if (other.SonMon.ListCongTrinh != null)
			{
				SonMon.ListCongTrinh = other.SonMon.ListCongTrinh;
			}
			if (other.SonMon.DoiHinhBaoVe != null)
			{
				SonMon.DoiHinhBaoVe = other.SonMon.DoiHinhBaoVe;
			}
		}
		if (other.LanhDiaData != null)
		{
			if (LanhDiaData != null)
			{
				for (int i = 0; i < other.LanhDiaData.Map.Count; i++)
				{
					other.LanhDiaData.Map[i].IsUpdated = !LanhDiaData.Map[i].Equals(other.LanhDiaData.Map[i]);
				}
			}
			LanhDiaData = other.LanhDiaData;
		}
		if (other.LanhDiaUser != null)
		{
			LanhDiaUser = other.LanhDiaUser;
		}
		if (other.ListThienMaLenh != null)
		{
			ListThienMaLenh = other.ListThienMaLenh;
		}
		if (other.GiangHoTinhAnh != null)
		{
			GiangHoTinhAnh = other.GiangHoTinhAnh;
		}
		if (other.ChienHonList != null)
		{
			ChienHonList = other.ChienHonList;
		}
		if (other.HuyenKhiList != null)
		{
			HuyenKhiList = other.HuyenKhiList;
		}
		GUIManager.instance.gadgetPanelTop.SetInfo(this);
	}

	public void UpdateTrangBi()
	{
		foreach (HeroData hero in HeroList)
		{
		}
	}
}

using System;
using System.Collections.Generic;
using LitJson;

public class ConfigManager
{
	public class DoiThuongLienMinhCfg
	{
		public int Id;

		public string CodeName;

		public int SoLuong;

		public int CongHien;

		public bool DuyNhat;

		public int TangKiemCacRequired;

		public int SoLanTrongNgay;

		public bool isValid()
		{
			if (CodeName.Contains("TH_"))
			{
				if (instance.m_dicNhanVats != null && instance.m_dicNhanVats.ContainsKey(CodeName.Replace("TH_", "NV_")))
				{
					return true;
				}
				return false;
			}
			if (CodeName.Contains("VP_"))
			{
				if (instance.m_dicVatPhamTieuThu != null && instance.m_dicVatPhamTieuThu.ContainsKey(CodeName))
				{
					return true;
				}
				return false;
			}
			if (CodeName.Contains("VK_") || CodeName.Contains("AG_") || CodeName.Contains("MU_") || CodeName.Contains("TS_"))
			{
				if (instance.m_dicTrangBi != null && instance.m_dicTrangBi.ContainsKey(CodeName))
				{
					return true;
				}
				return false;
			}
			if (CodeName.Contains("VC_"))
			{
				if (instance.m_dicVCs != null && instance.m_dicVCs.ContainsKey(CodeName))
				{
					return true;
				}
				return false;
			}
			return true;
		}
	}

	public class CongTrinhLienMinhCfg
	{
		public string CodeName;

		public List<int> CongHien;

		public List<string> Value;

		public bool isValid()
		{
			if (CodeName == "TuNghiaDuong")
			{
				if (CongHien.Count == 21 && Value.Count == 21)
				{
					return true;
				}
				return false;
			}
			if (CodeName == "ThienHaLau")
			{
				if (CongHien.Count == 21 && Value.Count == 21)
				{
					return true;
				}
				return false;
			}
			if (CodeName == "TangKiemCac")
			{
				if (CongHien.Count == 11 && Value.Count == 11)
				{
					return true;
				}
				return false;
			}
			return false;
		}
	}

	public const int LevelUnlockHuyetChien = 10;

	public const int LuotHoiSinhHuyetChien = 12;

	public const int LevelUnlockDongNhan = 12;

	public const int KhiTheThienHaVoSong = 40;

	public const int KhiTheVoDuLuanBi = 35;

	public const int KhiTheNhatDaiTonSu = 30;

	public const int KhiTheNgaoThiQuanHung = 20;

	public const int KhiTheHanHuuDichThu = 15;

	public const int KhiTheLoHoaThuanThanh = 10;

	public const int KhiTheGiaKinhTuuThuc = 5;

	public const int KhiTheKinhTamDongPhach = 40;

	public const int KhiTheKinhThienDongDia = 30;

	public const int KhiTheKinhTheHaiTuc = 20;

	public const int KhiTheThanCongCaiThe = 10;

	public const int KhiTheDangPhongTaoCuc = 40;

	public const int DanhSonGiangHoIdxDiff = 9;

	private static ConfigManager _instance;

	private bool isInited;

	public Dictionary<string, CfgVoCong> m_dicVCs = new Dictionary<string, CfgVoCong>();

	public Dictionary<string, NhanVatCfg> m_dicNhanVats = new Dictionary<string, NhanVatCfg>();

	public Dictionary<string, VatPhamTieuThuCfg> m_dicVatPhamTieuThu = new Dictionary<string, VatPhamTieuThuCfg>();

	public Dictionary<string, TrangBiCfg> m_dicTrangBi = new Dictionary<string, TrangBiCfg>();

	public List<GiangHoCfg> m_listGiangHo = new List<GiangHoCfg>();

	public List<DanhSonCfg> m_listDanhSon = new List<DanhSonCfg>();

	public Dictionary<string, VCTimeEffectCfg> m_dicVCTimeEffects = new Dictionary<string, VCTimeEffectCfg>();

	public Dictionary<string, NVAnimTimeCfg> m_dicAttackTimeEffects = new Dictionary<string, NVAnimTimeCfg>();

	public List<GoiVatPhamCfg> m_GoiVatPhamList = new List<GoiVatPhamCfg>();

	public List<CuaHangThanBiCfg> m_CuaHangThanBiList = new List<CuaHangThanBiCfg>();

	public List<GiangHoCfg> m_listGiangHoTinhAnh = new List<GiangHoCfg>();

	public Dictionary<string, ChienHonCfg> m_dicChienHons = new Dictionary<string, ChienHonCfg>();

	public Dictionary<string, HuyenKhiCfg> m_dicHuyenKhi = new Dictionary<string, HuyenKhiCfg>();

	public Dictionary<string, EffHuyenKhiCfg> m_dicEffHuyenKhi = new Dictionary<string, EffHuyenKhiCfg>();

	public Dictionary<string, DoiThuongLienMinhCfg> DoiThuongLienMinhConfig;

	public Dictionary<string, CongTrinhLienMinhCfg> CongTrinhLienMinhConfig;

	private bool m_giangHoReady;

	public static ConfigManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new ConfigManager();
				return _instance;
			}
			return _instance;
		}
		set
		{
			_instance = value;
		}
	}

	public OtherCfg OtherConfig { get; set; }

	public HuyetChienCfg HuyetChienConfig { get; set; }

	public ServerCfg ServerConfig { get; set; }

	public Dictionary<string, CostumeCfg> m_dicCostumeCfg { get; set; }

	public QuangMinhDinhCfg QuangMinhDinhConfig { get; set; }

	public List<string> ThanThuDaoMapList { get; set; }

	public SonMonCfg SonMonConfig { get; set; }

	public HoaVangCfg HoaVangConfig { get; set; }

	public bool IsGiangHoReady
	{
		get
		{
			return m_giangHoReady;
		}
	}

	public static int GetLevelTuNghiaDuongUnlockBangChien()
	{
		return 4;
	}

	public int GetServerSubmit()
	{
		return 6;
	}

	public int GetVersionSubmit()
	{
		return 100000;
	}

	public string GenerateInviteCode(int serverID, int gid)
	{
		return ((12345 + gid) * 1000 + serverID).ToString("X");
	}

	public QMDInfo.QMDNVChiSo GetChiSoNPCAi(QuangMinhDinhCfg.NPC npc, int ai)
	{
		QMDInfo.QMDNVChiSo qMDNVChiSo = new QMDInfo.QMDNVChiSo();
		double num = Math.Pow(1f + QuangMinhDinhConfig.NPCHeSoTangChiSo, ai - 1);
		qMDNVChiSo.Menh = (int)((double)npc.Menh * num);
		qMDNVChiSo.Ngoai = (int)((double)npc.Ngoai * num);
		qMDNVChiSo.Than = (int)((double)npc.Than * num);
		qMDNVChiSo.Khi = (int)((double)npc.Khi * num);
		return qMDNVChiSo;
	}

	public UserInfo ConverNPCToUserInfo(QMDInfo info)
	{
		int nPCIdx = info.NPCIdx;
		QuangMinhDinhCfg.NPCDoiHinh nPCDoiHinh = QuangMinhDinhConfig.DanhSachNPC[nPCIdx];
		UserInfo userInfo = new UserInfo();
		int num = 100000000;
		userInfo.DoiHinh = new UserInfo.DoiHinhData();
		userInfo.VoCongList = new List<UserInfo.VoCongData>();
		userInfo.HeroList = new List<UserInfo.HeroData>();
		userInfo.TrangBiList = new List<UserInfo.TrangBiData>();
		userInfo.Gamer = new UserInfo.GamerData();
		userInfo.Gamer.ID = 0;
		int num2 = Math.Min(1 + info.NPCAi / 3, 9);
		for (int i = 0; i < nPCDoiHinh.NPCs.Count; i++)
		{
			QuangMinhDinhCfg.NPC nPC = nPCDoiHinh.NPCs[i];
			QMDInfo.QMDNVChiSo chiSoNPCAi = GetChiSoNPCAi(nPC, info.NPCAi);
			int num3 = num + i;
			UserInfo.HeroData heroData = new UserInfo.HeroData();
			heroData.HID = num3;
			heroData.Level = 1;
			heroData.Name = info.NPCList[i];
			NhanVatCfg nhanVatCfg = m_dicNhanVats[heroData.Name];
			heroData.ChiSoGoc = new UserInfo.HeroData.BaseData();
			heroData.ChiSoGoc.Menh = chiSoNPCAi.Menh;
			heroData.ChiSoGoc.Ngoai = chiSoNPCAi.Ngoai;
			heroData.ChiSoGoc.Noi = chiSoNPCAi.Khi;
			heroData.ChiSoGoc.ThanPhap = chiSoNPCAi.Than;
			heroData.ChiSoGoc.Range = nhanVatCfg.Range;
			heroData.ChiSoGoc.BAS = nhanVatCfg.BAS;
			heroData.ChiSoGoc.BMS = nhanVatCfg.BMS;
			heroData.TrangThai = new UserInfo.HeroData.AI();
			heroData.TrangThai.ChienThuat = nPC.GetAI();
			heroData.VoCong1Name = nhanVatCfg.VoCongMacDinh;
			heroData.VoCong1Level = num2;
			heroData.DHPosX = ((float)i - 1.5f) * 0.25f;
			heroData.DHPosY = 0.5f;
			if (nPC.VoCong2.Length > 0)
			{
				UserInfo.VoCongData voCongData = new UserInfo.VoCongData();
				voCongData.HID = num3;
				voCongData.ID = num + i * 3;
				voCongData.Name = nPC.VoCong2;
				voCongData.Type = (VCType)(int)Enum.Parse(typeof(VCType), voCongData.Name, true);
				voCongData.Level = num2;
				userInfo.VoCongList.Add(voCongData);
				heroData.VoCong2ID = voCongData.ID;
			}
			if (nPC.NoiCong.Length > 0)
			{
				UserInfo.VoCongData voCongData2 = new UserInfo.VoCongData();
				voCongData2.HID = num3;
				voCongData2.ID = num + i * 3 + 1;
				voCongData2.Name = nPC.NoiCong;
				voCongData2.Type = (VCType)(int)Enum.Parse(typeof(VCType), voCongData2.Name, true);
				voCongData2.Level = num2;
				userInfo.VoCongList.Add(voCongData2);
				heroData.VoCong4ID = voCongData2.ID;
			}
			if (nPC.BoPhap.Length > 0)
			{
				UserInfo.VoCongData voCongData3 = new UserInfo.VoCongData();
				voCongData3.HID = num3;
				voCongData3.ID = num + i * 3 + 2;
				voCongData3.Name = nPC.BoPhap;
				voCongData3.Type = (VCType)(int)Enum.Parse(typeof(VCType), voCongData3.Name, true);
				voCongData3.Level = num2;
				userInfo.VoCongList.Add(voCongData3);
				heroData.VoCong3ID = voCongData3.ID;
			}
			userInfo.HeroList.Add(heroData);
			userInfo.DoiHinh.ListRaTran[i] = num3;
		}
		return userInfo;
	}

	public QMDInfo.QMDNVChiSo GetChiSoQMD(string name, int level)
	{
		QMDInfo.QMDNVChiSo qMDNVChiSo = new QMDInfo.QMDNVChiSo();
		NhanVatCfg nhanVatCfg = m_dicNhanVats[name];
		qMDNVChiSo.Menh = (int)Math.Round(nhanVatCfg.Menh + (float)(level - 1) * nhanVatCfg.MenhTT);
		qMDNVChiSo.Ngoai = (int)Math.Round(nhanVatCfg.Ngoai + (float)(level - 1) * nhanVatCfg.NgoaiTT);
		qMDNVChiSo.Than = (int)Math.Round(nhanVatCfg.ThanPhap + (float)(level - 1) * nhanVatCfg.ThanPhapTT);
		qMDNVChiSo.Khi = (int)Math.Round(nhanVatCfg.Noi + (float)(level - 1) * nhanVatCfg.NoiTT);
		QMDInfo.QMDNVChiSo qMDNVChiSo2 = new QMDInfo.QMDNVChiSo();
		qMDNVChiSo2.Menh = qMDNVChiSo.Menh;
		qMDNVChiSo2.Ngoai = qMDNVChiSo.Ngoai;
		qMDNVChiSo2.Than = qMDNVChiSo.Than;
		qMDNVChiSo2.Khi = qMDNVChiSo.Khi;
		foreach (NhanVatCfg.DuyenPhanCfg item in nhanVatCfg.DuyenPhan)
		{
			if (item.ChiSoDuyen == ChiSoDuyenPhan.Menh)
			{
				qMDNVChiSo2.Menh += (int)((float)qMDNVChiSo.Menh * item.HeSo / 100f);
			}
			if (item.ChiSoDuyen == ChiSoDuyenPhan.Ngoai)
			{
				qMDNVChiSo2.Ngoai += (int)((float)qMDNVChiSo.Ngoai * item.HeSo / 100f);
			}
			if (item.ChiSoDuyen == ChiSoDuyenPhan.ThanPhap)
			{
				qMDNVChiSo2.Than += (int)((float)qMDNVChiSo.Than * item.HeSo / 100f);
			}
			if (item.ChiSoDuyen == ChiSoDuyenPhan.Khi)
			{
				qMDNVChiSo2.Khi += (int)((float)qMDNVChiSo.Khi * item.HeSo / 100f);
			}
		}
		return qMDNVChiSo2;
	}

	public int GetBatQuaiCuongHoaLevel(int slot_idx, UserInfo.DoiHinhData doi_hinh_data)
	{
		List<int> list = new List<int>();
		list.Add(1);
		list.Add(1);
		list.Add(1);
		list.Add(1);
		list.Add(1);
		list.Add(1);
		list.Add(1);
		list.Add(1);
		for (int i = 1; i < 9; i++)
		{
			int batQuaiBuffLevel = GetBatQuaiBuffLevel(i, doi_hinh_data);
			switch (GetBatQuaiBuffLoai(i, doi_hinh_data))
			{
			case ChiSoCoBan.Menh:
				if (list[0] == 1)
				{
					list[0] = batQuaiBuffLevel;
				}
				else
				{
					list[4] = batQuaiBuffLevel;
				}
				break;
			case ChiSoCoBan.Ngoai:
				if (list[1] == 1)
				{
					list[1] = batQuaiBuffLevel;
				}
				else
				{
					list[5] = batQuaiBuffLevel;
				}
				break;
			case ChiSoCoBan.ThanPhap:
				if (list[2] == 1)
				{
					list[2] = batQuaiBuffLevel;
				}
				else
				{
					list[6] = batQuaiBuffLevel;
				}
				break;
			case ChiSoCoBan.Noi:
				if (list[3] == 1)
				{
					list[3] = batQuaiBuffLevel;
				}
				else
				{
					list[7] = batQuaiBuffLevel;
				}
				break;
			}
		}
		for (int j = 0; j < doi_hinh_data.ListHoTro.Count; j++)
		{
			if (doi_hinh_data.ListHoTro[j] == -1)
			{
				list[j] = 1;
			}
		}
		return list[slot_idx];
	}

	public int GetBatQuaiBuffLevel(int slot_cuonghoa, UserInfo.DoiHinhData doi_hinh_data)
	{
		int result = 1;
		switch (slot_cuonghoa)
		{
		case 1:
			result = doi_hinh_data.CuongHoa_Slot1;
			break;
		case 2:
			result = doi_hinh_data.CuongHoa_Slot2;
			break;
		case 3:
			result = doi_hinh_data.CuongHoa_Slot3;
			break;
		case 4:
			result = doi_hinh_data.CuongHoa_Slot4;
			break;
		case 5:
			result = doi_hinh_data.CuongHoa_Slot5;
			break;
		case 6:
			result = doi_hinh_data.CuongHoa_Slot6;
			break;
		case 7:
			result = doi_hinh_data.CuongHoa_Slot7;
			break;
		case 8:
			result = doi_hinh_data.CuongHoa_Slot8;
			break;
		}
		return result;
	}

	public int GetBatQuaiBuffHeSo(int slot_cuonghoa, UserInfo.DoiHinhData doi_hinh_data)
	{
		OtherCfg.BatQuaiTranDoType batQuaiTranType = doi_hinh_data.BatQuaiTranType;
		string soDoBQTCodeName = OtherCfg.GetSoDoBQTCodeName(batQuaiTranType);
		if (OtherConfig.BatQuaiConfig.ContainsKey(soDoBQTCodeName))
		{
			OtherCfg.BatQuaiCuongHoaCfg batQuaiCuongHoaCfg = OtherConfig.BatQuaiConfig[soDoBQTCodeName];
			List<int> list = new List<int>();
			int num = 1;
			switch (slot_cuonghoa)
			{
			case 1:
				list = batQuaiCuongHoaCfg.ChiSoSlot1;
				num = doi_hinh_data.CuongHoa_Slot1;
				break;
			case 2:
				list = batQuaiCuongHoaCfg.ChiSoSlot2;
				num = doi_hinh_data.CuongHoa_Slot2;
				break;
			case 3:
				list = batQuaiCuongHoaCfg.ChiSoSlot3;
				num = doi_hinh_data.CuongHoa_Slot3;
				break;
			case 4:
				list = batQuaiCuongHoaCfg.ChiSoSlot4;
				num = doi_hinh_data.CuongHoa_Slot4;
				break;
			case 5:
				list = batQuaiCuongHoaCfg.ChiSoSlot5;
				num = doi_hinh_data.CuongHoa_Slot5;
				break;
			case 6:
				list = batQuaiCuongHoaCfg.ChiSoSlot6;
				num = doi_hinh_data.CuongHoa_Slot6;
				break;
			case 7:
				list = batQuaiCuongHoaCfg.ChiSoSlot7;
				num = doi_hinh_data.CuongHoa_Slot7;
				break;
			case 8:
				list = batQuaiCuongHoaCfg.ChiSoSlot8;
				num = doi_hinh_data.CuongHoa_Slot8;
				break;
			}
			if (num <= list.Count)
			{
				return list[num - 1];
			}
		}
		return 10;
	}

	public ChiSoCoBan GetBatQuaiBuffLoai(int slot_cuonghoa, UserInfo.DoiHinhData doi_hinh_data)
	{
		OtherCfg.BatQuaiTranDoType batQuaiTranType = doi_hinh_data.BatQuaiTranType;
		string soDoBQTCodeName = OtherCfg.GetSoDoBQTCodeName(batQuaiTranType);
		if (OtherConfig.BatQuaiConfig.ContainsKey(soDoBQTCodeName))
		{
			OtherCfg.BatQuaiCuongHoaCfg batQuaiCuongHoaCfg = OtherConfig.BatQuaiConfig[soDoBQTCodeName];
			string text = batQuaiCuongHoaCfg.ListSlot[slot_cuonghoa - 1];
			if (text.StartsWith("N"))
			{
				return ChiSoCoBan.Ngoai;
			}
			if (text.StartsWith("K"))
			{
				return ChiSoCoBan.Noi;
			}
			if (text.StartsWith("T"))
			{
				return ChiSoCoBan.ThanPhap;
			}
			if (text.StartsWith("M"))
			{
				return ChiSoCoBan.Menh;
			}
		}
		return ChiSoCoBan.Menh;
	}

	public List<int> MapBatQuaiCuongHoa2TranDo(UserInfo.DoiHinhData doi_hinh_data)
	{
		List<int> list = new List<int>();
		list.Add(10);
		list.Add(10);
		list.Add(10);
		list.Add(10);
		list.Add(10);
		list.Add(10);
		list.Add(10);
		list.Add(10);
		if (doi_hinh_data == null)
		{
			return list;
		}
		for (int i = 1; i < 9; i++)
		{
			int batQuaiBuffHeSo = GetBatQuaiBuffHeSo(i, doi_hinh_data);
			switch (GetBatQuaiBuffLoai(i, doi_hinh_data))
			{
			case ChiSoCoBan.Menh:
				if (list[0] == 10)
				{
					list[0] = batQuaiBuffHeSo;
				}
				else
				{
					list[4] = batQuaiBuffHeSo;
				}
				break;
			case ChiSoCoBan.Ngoai:
				if (list[1] == 10)
				{
					list[1] = batQuaiBuffHeSo;
				}
				else
				{
					list[5] = batQuaiBuffHeSo;
				}
				break;
			case ChiSoCoBan.ThanPhap:
				if (list[2] == 10)
				{
					list[2] = batQuaiBuffHeSo;
				}
				else
				{
					list[6] = batQuaiBuffHeSo;
				}
				break;
			case ChiSoCoBan.Noi:
				if (list[3] == 10)
				{
					list[3] = batQuaiBuffHeSo;
				}
				else
				{
					list[7] = batQuaiBuffHeSo;
				}
				break;
			}
		}
		for (int j = 0; j < doi_hinh_data.ListHoTro.Count; j++)
		{
			if (doi_hinh_data.ListHoTro[j] == -1)
			{
				list[j] = 10;
			}
		}
		return list;
	}

	public void DecodeInviteCode(int EncodeNum, out int serverID, out int gid)
	{
		serverID = EncodeNum % 1000;
		gid = (EncodeNum - serverID) / 1000 - 12345;
	}

	public int GetTileCuongHoa(int lv_trang_bi, int lv_mon_phai)
	{
		if (lv_trang_bi <= 100)
		{
			return 100;
		}
		float num = (float)lv_trang_bi / (float)lv_mon_phai;
		return (int)Math.Round(500f / (num * num));
	}

	public TimeSpan GetTimeBetweenLuotDotLua()
	{
		return TimeSpan.FromHours(2.0);
	}

	public TimeSpan GetMinTimeToResetLuotDotLua()
	{
		return TimeSpan.FromHours(1.0);
	}

	public int GetNumTopTuanCT2()
	{
		return 10;
	}

	public string GetTenDanhHieuCT2FromTopTuan(int hang)
	{
		string[] array = new string[22]
		{
			"Top 1", "Top 2", "Top 3", "Top 4", "Top 5", "Top 6", "Top 7", "Top 8", "Top 9", "Top 10",
			"Top 11", "Top 12", "Top 13", "Top 14", "Top 15", "Top 16", "Top 17", "Top 18", "Top 19", "Top 20",
			"Top 21", "Top 22"
		};
		return array[hang - 1];
	}

	public PhanThuongResponse GetPhanThuongQMD(int hang)
	{
		PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
		phanThuongResponse.ErrorCode = ERROR_CODE.OK;
		switch (hang)
		{
		case 1:
		{
			PhanThuongResponse.PhanThuong phanThuong12 = new PhanThuongResponse.PhanThuong();
			phanThuong12.Name = "VP_BOI_DUONG_DAN";
			phanThuong12.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong12.Count = 1200;
			phanThuongResponse.PhanThuongList.Add(phanThuong12);
			phanThuong12 = new PhanThuongResponse.PhanThuong();
			phanThuong12.Name = "VP_TAY_TUY_DAN";
			phanThuong12.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong12.Count = 120;
			phanThuongResponse.PhanThuongList.Add(phanThuong12);
			phanThuong12 = new PhanThuongResponse.PhanThuong();
			phanThuong12.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
			phanThuong12.Count = 1200;
			phanThuongResponse.PhanThuongList.Add(phanThuong12);
			break;
		}
		case 2:
		{
			PhanThuongResponse.PhanThuong phanThuong11 = new PhanThuongResponse.PhanThuong();
			phanThuong11.Name = "VP_BOI_DUONG_DAN";
			phanThuong11.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong11.Count = 1000;
			phanThuongResponse.PhanThuongList.Add(phanThuong11);
			phanThuong11 = new PhanThuongResponse.PhanThuong();
			phanThuong11.Name = "VP_TAY_TUY_DAN";
			phanThuong11.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong11.Count = 100;
			phanThuongResponse.PhanThuongList.Add(phanThuong11);
			phanThuong11 = new PhanThuongResponse.PhanThuong();
			phanThuong11.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
			phanThuong11.Count = 1000;
			phanThuongResponse.PhanThuongList.Add(phanThuong11);
			break;
		}
		case 3:
		{
			PhanThuongResponse.PhanThuong phanThuong10 = new PhanThuongResponse.PhanThuong();
			phanThuong10.Name = "VP_BOI_DUONG_DAN";
			phanThuong10.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong10.Count = 900;
			phanThuongResponse.PhanThuongList.Add(phanThuong10);
			phanThuong10 = new PhanThuongResponse.PhanThuong();
			phanThuong10.Name = "VP_TAY_TUY_DAN";
			phanThuong10.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong10.Count = 90;
			phanThuongResponse.PhanThuongList.Add(phanThuong10);
			phanThuong10 = new PhanThuongResponse.PhanThuong();
			phanThuong10.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
			phanThuong10.Count = 900;
			phanThuongResponse.PhanThuongList.Add(phanThuong10);
			break;
		}
		case 4:
		{
			PhanThuongResponse.PhanThuong phanThuong9 = new PhanThuongResponse.PhanThuong();
			phanThuong9.Name = "VP_BOI_DUONG_DAN";
			phanThuong9.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong9.Count = 800;
			phanThuongResponse.PhanThuongList.Add(phanThuong9);
			phanThuong9 = new PhanThuongResponse.PhanThuong();
			phanThuong9.Name = "VP_TAY_TUY_DAN";
			phanThuong9.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong9.Count = 80;
			phanThuongResponse.PhanThuongList.Add(phanThuong9);
			phanThuong9 = new PhanThuongResponse.PhanThuong();
			phanThuong9.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
			phanThuong9.Count = 800;
			phanThuongResponse.PhanThuongList.Add(phanThuong9);
			break;
		}
		case 5:
		{
			PhanThuongResponse.PhanThuong phanThuong8 = new PhanThuongResponse.PhanThuong();
			phanThuong8.Name = "VP_BOI_DUONG_DAN";
			phanThuong8.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong8.Count = 800;
			phanThuongResponse.PhanThuongList.Add(phanThuong8);
			phanThuong8 = new PhanThuongResponse.PhanThuong();
			phanThuong8.Name = "VP_TAY_TUY_DAN";
			phanThuong8.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong8.Count = 80;
			phanThuongResponse.PhanThuongList.Add(phanThuong8);
			phanThuong8 = new PhanThuongResponse.PhanThuong();
			phanThuong8.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
			phanThuong8.Count = 800;
			phanThuongResponse.PhanThuongList.Add(phanThuong8);
			break;
		}
		case 6:
		{
			PhanThuongResponse.PhanThuong phanThuong7 = new PhanThuongResponse.PhanThuong();
			phanThuong7.Name = "VP_BOI_DUONG_DAN";
			phanThuong7.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong7.Count = 800;
			phanThuongResponse.PhanThuongList.Add(phanThuong7);
			phanThuong7 = new PhanThuongResponse.PhanThuong();
			phanThuong7.Name = "VP_TAY_TUY_DAN";
			phanThuong7.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong7.Count = 80;
			phanThuongResponse.PhanThuongList.Add(phanThuong7);
			phanThuong7 = new PhanThuongResponse.PhanThuong();
			phanThuong7.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
			phanThuong7.Count = 800;
			phanThuongResponse.PhanThuongList.Add(phanThuong7);
			break;
		}
		case 7:
		{
			PhanThuongResponse.PhanThuong phanThuong6 = new PhanThuongResponse.PhanThuong();
			phanThuong6.Name = "VP_BOI_DUONG_DAN";
			phanThuong6.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong6.Count = 800;
			phanThuongResponse.PhanThuongList.Add(phanThuong6);
			phanThuong6 = new PhanThuongResponse.PhanThuong();
			phanThuong6.Name = "VP_TAY_TUY_DAN";
			phanThuong6.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong6.Count = 80;
			phanThuongResponse.PhanThuongList.Add(phanThuong6);
			phanThuong6 = new PhanThuongResponse.PhanThuong();
			phanThuong6.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
			phanThuong6.Count = 800;
			phanThuongResponse.PhanThuongList.Add(phanThuong6);
			break;
		}
		case 8:
		{
			PhanThuongResponse.PhanThuong phanThuong5 = new PhanThuongResponse.PhanThuong();
			phanThuong5.Name = "VP_BOI_DUONG_DAN";
			phanThuong5.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong5.Count = 700;
			phanThuongResponse.PhanThuongList.Add(phanThuong5);
			phanThuong5 = new PhanThuongResponse.PhanThuong();
			phanThuong5.Name = "VP_TAY_TUY_DAN";
			phanThuong5.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong5.Count = 70;
			phanThuongResponse.PhanThuongList.Add(phanThuong5);
			phanThuong5 = new PhanThuongResponse.PhanThuong();
			phanThuong5.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
			phanThuong5.Count = 700;
			phanThuongResponse.PhanThuongList.Add(phanThuong5);
			break;
		}
		case 9:
		{
			PhanThuongResponse.PhanThuong phanThuong4 = new PhanThuongResponse.PhanThuong();
			phanThuong4.Name = "VP_BOI_DUONG_DAN";
			phanThuong4.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong4.Count = 700;
			phanThuongResponse.PhanThuongList.Add(phanThuong4);
			phanThuong4 = new PhanThuongResponse.PhanThuong();
			phanThuong4.Name = "VP_TAY_TUY_DAN";
			phanThuong4.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong4.Count = 70;
			phanThuongResponse.PhanThuongList.Add(phanThuong4);
			phanThuong4 = new PhanThuongResponse.PhanThuong();
			phanThuong4.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
			phanThuong4.Count = 700;
			phanThuongResponse.PhanThuongList.Add(phanThuong4);
			break;
		}
		case 10:
		case 11:
		case 12:
		{
			PhanThuongResponse.PhanThuong phanThuong3 = new PhanThuongResponse.PhanThuong();
			phanThuong3.Name = "VP_BOI_DUONG_DAN";
			phanThuong3.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong3.Count = 700;
			phanThuongResponse.PhanThuongList.Add(phanThuong3);
			phanThuong3 = new PhanThuongResponse.PhanThuong();
			phanThuong3.Name = "VP_TAY_TUY_DAN";
			phanThuong3.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong3.Count = 70;
			phanThuongResponse.PhanThuongList.Add(phanThuong3);
			phanThuong3 = new PhanThuongResponse.PhanThuong();
			phanThuong3.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
			phanThuong3.Count = 700;
			phanThuongResponse.PhanThuongList.Add(phanThuong3);
			break;
		}
		case 13:
		case 14:
		case 15:
		case 16:
		{
			PhanThuongResponse.PhanThuong phanThuong2 = new PhanThuongResponse.PhanThuong();
			phanThuong2.Name = "VP_BOI_DUONG_DAN";
			phanThuong2.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong2.Count = 600;
			phanThuongResponse.PhanThuongList.Add(phanThuong2);
			phanThuong2 = new PhanThuongResponse.PhanThuong();
			phanThuong2.Name = "VP_TAY_TUY_DAN";
			phanThuong2.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong2.Count = 60;
			phanThuongResponse.PhanThuongList.Add(phanThuong2);
			phanThuong2 = new PhanThuongResponse.PhanThuong();
			phanThuong2.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
			phanThuong2.Count = 600;
			phanThuongResponse.PhanThuongList.Add(phanThuong2);
			break;
		}
		case 17:
		case 18:
		case 19:
		case 20:
		{
			PhanThuongResponse.PhanThuong phanThuong = new PhanThuongResponse.PhanThuong();
			phanThuong.Name = "VP_BOI_DUONG_DAN";
			phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong.Count = 500;
			phanThuongResponse.PhanThuongList.Add(phanThuong);
			phanThuong = new PhanThuongResponse.PhanThuong();
			phanThuong.Name = "VP_TAY_TUY_DAN";
			phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong.Count = 50;
			phanThuongResponse.PhanThuongList.Add(phanThuong);
			phanThuong = new PhanThuongResponse.PhanThuong();
			phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.VANG;
			phanThuong.Count = 500;
			phanThuongResponse.PhanThuongList.Add(phanThuong);
			break;
		}
		}
		return phanThuongResponse;
	}

	public PhanThuongResponse GetPhanThuongCT2BXHTuan(int hang, int level)
	{
		PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
		phanThuongResponse.ErrorCode = ERROR_CODE.OK;
		switch (hang)
		{
		case 1:
		{
			PhanThuongResponse.PhanThuong phanThuong4 = new PhanThuongResponse.PhanThuong();
			phanThuong4.Name = "VP_BOI_DUONG_DAN";
			phanThuong4.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong4.Count = 4000;
			phanThuongResponse.PhanThuongList.Add(phanThuong4);
			phanThuong4 = new PhanThuongResponse.PhanThuong();
			phanThuong4.Name = "VP_THAN_HUYET_THACH";
			phanThuong4.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong4.Count = 200;
			phanThuongResponse.PhanThuongList.Add(phanThuong4);
			phanThuong4 = new PhanThuongResponse.PhanThuong();
			phanThuong4.Name = "VP_GA_QUAY";
			phanThuong4.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong4.Count = 20;
			phanThuongResponse.PhanThuongList.Add(phanThuong4);
			break;
		}
		case 2:
		{
			PhanThuongResponse.PhanThuong phanThuong3 = new PhanThuongResponse.PhanThuong();
			phanThuong3.Name = "VP_BOI_DUONG_DAN";
			phanThuong3.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong3.Count = 3200;
			phanThuongResponse.PhanThuongList.Add(phanThuong3);
			phanThuong3 = new PhanThuongResponse.PhanThuong();
			phanThuong3.Name = "VP_THAN_HUYET_THACH";
			phanThuong3.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong3.Count = 150;
			phanThuongResponse.PhanThuongList.Add(phanThuong3);
			phanThuong3 = new PhanThuongResponse.PhanThuong();
			phanThuong3.Name = "VP_GA_QUAY";
			phanThuong3.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong3.Count = 16;
			phanThuongResponse.PhanThuongList.Add(phanThuong3);
			break;
		}
		case 3:
		{
			PhanThuongResponse.PhanThuong phanThuong2 = new PhanThuongResponse.PhanThuong();
			phanThuong2.Name = "VP_BOI_DUONG_DAN";
			phanThuong2.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong2.Count = 3200;
			phanThuongResponse.PhanThuongList.Add(phanThuong2);
			phanThuong2 = new PhanThuongResponse.PhanThuong();
			phanThuong2.Name = "VP_THAN_HUYET_THACH";
			phanThuong2.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong2.Count = 150;
			phanThuongResponse.PhanThuongList.Add(phanThuong2);
			phanThuong2 = new PhanThuongResponse.PhanThuong();
			phanThuong2.Name = "VP_GA_QUAY";
			phanThuong2.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong2.Count = 16;
			phanThuongResponse.PhanThuongList.Add(phanThuong2);
			break;
		}
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
		case 9:
		case 10:
		{
			PhanThuongResponse.PhanThuong phanThuong = new PhanThuongResponse.PhanThuong();
			phanThuong.Name = "VP_BOI_DUONG_DAN";
			phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong.Count = 2400;
			phanThuongResponse.PhanThuongList.Add(phanThuong);
			phanThuong = new PhanThuongResponse.PhanThuong();
			phanThuong.Name = "VP_THAN_HUYET_THACH";
			phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong.Count = 100;
			phanThuongResponse.PhanThuongList.Add(phanThuong);
			phanThuong = new PhanThuongResponse.PhanThuong();
			phanThuong.Name = "VP_GA_QUAY";
			phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			phanThuong.Count = 10;
			phanThuongResponse.PhanThuongList.Add(phanThuong);
			break;
		}
		}
		return phanThuongResponse;
	}

	public int GetDiemChienTichFromTopChienTruong(int hang, bool isWonTeam)
	{
		int[] array = new int[10] { 16, 14, 13, 12, 11, 10, 9, 8, 7, 6 };
		if (hang > 0 && hang <= array.Length)
		{
			if (isWonTeam)
			{
				return array[hang - 1] + 2;
			}
			return array[hang - 1];
		}
		if (isWonTeam)
		{
			return 7;
		}
		return 5;
	}

	public int GetTileCuongHoaTangCuong(CuongHoaTrangBiRequest request, UserInfo userInfo, out bool NotEnough)
	{
		int num = 0;
		NotEnough = false;
		int i;
		for (i = 0; i < request.HonIDList.Count; i++)
		{
			UserInfo.HonNhanVatData honNhanVatData = userInfo.HonNhanVatList.Find((UserInfo.HonNhanVatData e) => e.ID == request.HonIDList[i]);
			if (honNhanVatData != null)
			{
				if (request.CountList[i] > honNhanVatData.Quantity)
				{
					request.CountList[i] = honNhanVatData.Quantity;
					NotEnough = true;
				}
				int num2 = GetTileCuongHoaTangByHon(honNhanVatData.Name) * request.CountList[i];
				num += num2;
			}
		}
		return num;
	}

	private int GetTileCuongHoaTangByHon(string hon_name)
	{
		if (instance.m_dicNhanVats.ContainsKey(hon_name))
		{
			NhanVatCfg nhanVatCfg = instance.m_dicNhanVats[hon_name];
			if (nhanVatCfg.Hang == 1)
			{
				return 2;
			}
			if (nhanVatCfg.Hang == 2)
			{
				return 5;
			}
			return 10;
		}
		return 0;
	}

	public static int GetMaxCuongHoaLevelByTinhLuyen(int tinh_luyen_lv)
	{
		switch (tinh_luyen_lv)
		{
		case 0:
			return 2;
		case 1:
			return 3;
		case 2:
			return 4;
		case 3:
			return 5;
		case 4:
			return 6;
		case 5:
			return 7;
		default:
			return 2;
		}
	}

	public static int GetCostThoiLuaKNB(int luot)
	{
		return 5 + luot * 3;
	}

	public static int GetCostThoiLuaBac(int level)
	{
		return level * 10000;
	}

	public static DateTime GetNextTimeThoiLuaKNB(DateTime now)
	{
		return now + TimeSpan.FromSeconds(6.0);
	}

	public static DateTime GetNextTimeThoiLuaBac(DateTime now)
	{
		return now + TimeSpan.FromSeconds(60.0);
	}

	public static int GetMaxLuotThachDau()
	{
		return 60;
	}

	public static int GetTienCuocThachDauByLevel(int level)
	{
		return 1000 * level;
	}

	public static int GiangHoUnlockChienThuatBiDong()
	{
		return 7;
	}

	public static int GetMaxLuotBatTho()
	{
		return 3;
	}

	public static TimeSpan GetTimeUpdateBatTho()
	{
		return TimeSpan.FromHours(1.0);
	}

	public int GetLenLevelThuongBac(int level)
	{
		return level * level * level * 10 + 5000;
	}

	public int GetLenLevelThuongKNB(int level)
	{
		if (level < 20)
		{
			return 20;
		}
		return level;
	}

	public static TimeSpan GetTimeRemoveMail()
	{
		return TimeSpan.FromDays(7.0);
	}

	public int GetMaxTiemLucUongRuou(int level)
	{
		return level * 4;
	}

	public static int GetBacChuocThan(int level)
	{
		return 1000 * level;
	}

	public static int GetMaxLuotBatCocByVip(int vip)
	{
		if (vip <= 2)
		{
			return 6;
		}
		if (vip == 3)
		{
			return 6;
		}
		if (vip == 4)
		{
			return 7;
		}
		if (vip == 5)
		{
			return 7;
		}
		if (vip == 6)
		{
			return 8;
		}
		if (vip == 7)
		{
			return 8;
		}
		if (vip == 8)
		{
			return 9;
		}
		if (vip == 9)
		{
			return 9;
		}
		if (vip == 10)
		{
			return 9;
		}
		if (vip == 11)
		{
			return 10;
		}
		if (vip == 12)
		{
			return 10;
		}
		if (vip >= 13)
		{
			return 11;
		}
		return 6;
	}

	public static TimeSpan GetTimeOutThachDau()
	{
		return TimeSpan.FromSeconds(30.0);
	}

	public static TimeSpan GetTimeBiBatCoc()
	{
		return TimeSpan.FromHours(24.0);
	}

	public static int GetDanhHieuGiangHo(int giangHoIdx)
	{
		if (giangHoIdx < 3)
		{
			return 0;
		}
		if (giangHoIdx >= instance.m_listGiangHo.Count)
		{
			return instance.m_listGiangHo.Count - 3;
		}
		return giangHoIdx - 2;
	}

	public static int GetDanhHieuGiangHo(List<UserInfo.GiangHoData> giangHo)
	{
		if (giangHo == null || giangHo.Count < 4)
		{
			return 0;
		}
		int giangHoIdx = giangHo.FindLast((UserInfo.GiangHoData e) => e.HoanThanh > 0).GiangHoIdx;
		int num = giangHoIdx - 2;
		return (num >= 0) ? num : 0;
	}

	public static GiangHoCfg GetGiangHoByLevelDanhHieu(int level)
	{
		int num = 0;
		if (level < 1)
		{
			num = 0;
		}
		num = level + 2;
		if (num > instance.m_listGiangHo.Count - 1)
		{
			return null;
		}
		return instance.m_listGiangHo[num];
	}

	public static int GetDanhHieuVip(int vip)
	{
		return vip;
	}

	public static int GetVipByLevelDanhHieu(int level)
	{
		return level;
	}

	public static int GetDanhHieuLuanKiem(UserInfo.LuanKiemData luanKiem)
	{
		int[] array = new int[41]
		{
			9900, 9000, 8000, 7000, 6000, 5000, 4000, 3000, 2000, 1000,
			900, 800, 700, 600, 500, 400, 300, 200, 100, 90,
			80, 70, 60, 50, 40, 30, 20, 18, 16, 14,
			12, 10, 9, 8, 7, 6, 5, 4, 3, 2,
			1
		};
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] <= luanKiem.Hang)
			{
				return i + 1;
			}
		}
		return 0;
	}

	public static int GetHangLuanKiemByLevelDanhHieu(int level)
	{
		int[] array = new int[41]
		{
			4900, 4700, 4500, 4000, 3800, 3600, 3400, 3200, 2000, 1000,
			900, 800, 700, 600, 500, 400, 300, 200, 100, 90,
			80, 70, 60, 50, 40, 30, 20, 18, 16, 14,
			12, 10, 9, 8, 7, 6, 5, 4, 3, 2,
			1
		};
		if (level < 1)
		{
			return 10000;
		}
		if (level > array.Length)
		{
			return array[array.Length - 1];
		}
		return array[level - 1];
	}

	public static int GetDanhHieuDongNhan(int luot)
	{
		int[] array = new int[21]
		{
			10, 20, 30, 40, 50, 60, 80, 100, 120, 140,
			160, 190, 220, 250, 280, 350, 400, 500, 600, 700,
			800
		};
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] > luot)
			{
				return i;
			}
		}
		int num = array[array.Length - 1];
		if (luot > num)
		{
			return array.Length + (luot - num) / 500;
		}
		return array.Length;
	}

	public static int GetLuotDongNhanByLevelDanhHieu(int level)
	{
		int[] array = new int[21]
		{
			10, 20, 30, 40, 50, 60, 80, 100, 120, 140,
			160, 190, 220, 250, 280, 350, 400, 500, 600, 700,
			800
		};
		if (level < 1)
		{
			return 0;
		}
		if (level - 1 < array.Length)
		{
			return array[level - 1];
		}
		return array[array.Length - 1] + (level - array.Length) * 500;
	}

	public static int GetDanhHieuTuyTamVoHoc(int luot)
	{
		int[] array = new int[21]
		{
			5, 10, 20, 30, 40, 50, 60, 70, 80, 100,
			120, 140, 160, 180, 200, 250, 300, 350, 400, 450,
			500
		};
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] > luot)
			{
				return i;
			}
		}
		int num = array[array.Length - 1];
		if (luot > num)
		{
			return array.Length + (luot - num) / 100;
		}
		return array.Length;
	}

	public static int GetLuotTuyTamVoHocByLevelDanhHieu(int level)
	{
		int[] array = new int[21]
		{
			5, 10, 20, 30, 40, 50, 60, 70, 80, 100,
			120, 140, 160, 180, 200, 250, 300, 350, 400, 450,
			500
		};
		if (level < 1)
		{
			return 0;
		}
		if (level - 1 < array.Length)
		{
			return array[level - 1];
		}
		return array[array.Length - 1] + (level - array.Length) * 100;
	}

	public static int GetDanhHieuHuyetChien(int vuotAi)
	{
		return vuotAi / 5;
	}

	public static int GetLuotHuyetChienByLevelDanhHieu(int level)
	{
		return level * 5;
	}

	public static int GetDanhHieuLienMinh(int levelLienMinh)
	{
		return levelLienMinh;
	}

	public static int GetLevelLienMinhFromLevelDanhHieu(int level)
	{
		return level;
	}

	public static int GetKhiTheGiangHo(int level)
	{
		if (level < 1)
		{
			return 0;
		}
		return level * 10;
	}

	public static int GetKhiTheVipEffect(int level)
	{
		if (level < 1)
		{
			return 0;
		}
		return level * 5;
	}

	public static int GetKhiTheLuanKiem(int level)
	{
		if (level < 1)
		{
			return 0;
		}
		return level * 10;
	}

	public static int GetKhiTheHuyetChien(int level)
	{
		if (level < 1)
		{
			return 0;
		}
		return level * 10;
	}

	public static int GetKhiTheDungSiXungTran(int level)
	{
		if (level < 1)
		{
			return 0;
		}
		return level * 10;
	}

	public static int GetKhiTheTuyTamVoHoc(int level)
	{
		if (level < 1)
		{
			return 0;
		}
		return level * 10;
	}

	public static int GetKhiTheLienMinhThanhVien(int level)
	{
		if (level < 1)
		{
			return 0;
		}
		return level * 10;
	}

	public static int GetKhiTheByDanhHieuAtTime(UserInfo.DanhHieuData danhHieu, DateTime time)
	{
		int num = 0;
		if (danhHieu == null)
		{
			return 0;
		}
		int khiTheGiangHo = GetKhiTheGiangHo(danhHieu.GiangHoHaoKiet);
		int khiTheVipEffect = GetKhiTheVipEffect(danhHieu.QuyTanThanPhan);
		int khiTheLuanKiem = GetKhiTheLuanKiem(danhHieu.ThanhDanhHienHach);
		int khiTheHuyetChien = GetKhiTheHuyetChien(danhHieu.DucHuyetPhanChien);
		int khiTheDungSiXungTran = GetKhiTheDungSiXungTran(danhHieu.DungSiXungTran);
		int khiTheTuyTamVoHoc = GetKhiTheTuyTamVoHoc(danhHieu.TuyTamVoHoc);
		int khiTheLienMinhThanhVien = GetKhiTheLienMinhThanhVien(danhHieu.LienMinhThanhVien);
		num = khiTheGiangHo;
		if (khiTheVipEffect > 0)
		{
			num += (int)((float)(khiTheGiangHo * khiTheVipEffect) / 100f);
		}
		num += khiTheLuanKiem + khiTheHuyetChien + khiTheDungSiXungTran + khiTheTuyTamVoHoc + khiTheLienMinhThanhVien;
		if (time < danhHieu.ThienHaVoSong)
		{
			num += 40;
		}
		else if (time < danhHieu.VoDuLuanBi)
		{
			num += 35;
		}
		else if (time < danhHieu.NhatDaiTonSu)
		{
			num += 30;
		}
		else if (time < danhHieu.NgaoThiQuanHung)
		{
			num += 20;
		}
		else if (time < danhHieu.HanHuuDichThu)
		{
			num += 15;
		}
		else if (time < danhHieu.LoHoaThuanThanh)
		{
			num += 10;
		}
		else if (time < danhHieu.GiaKinhTuuThuc)
		{
			num += 5;
		}
		if (time < danhHieu.KinhTamDongPhach)
		{
			num += 40;
		}
		else if (time < danhHieu.KinhThienDongDia)
		{
			num += 30;
		}
		else if (time < danhHieu.KinhTheHaiTuc)
		{
			num += 20;
		}
		else if (time < danhHieu.ThanCongCaiThe)
		{
			num += 10;
		}
		if (time < danhHieu.DangPhongTaoCuc)
		{
			num += 40;
		}
		return num;
	}

	public static int GetCostMoThuongDanhSon(int turnMatVang)
	{
		int result = 0;
		switch (turnMatVang)
		{
		case 1:
			result = instance.OtherConfig.GiaMoThuongDanhSonLan1;
			break;
		case 2:
			result = instance.OtherConfig.GiaMoThuongDanhSonLan2;
			break;
		case 3:
			result = instance.OtherConfig.GiaMoThuongDanhSonLan3;
			break;
		case 4:
			result = instance.OtherConfig.GiaMoThuongDanhSonLan4;
			break;
		case 5:
			result = instance.OtherConfig.GiaMoThuongDanhSonLan5;
			break;
		case 6:
			result = instance.OtherConfig.GiaMoThuongDanhSonLan6;
			break;
		}
		return result;
	}

	public static string GetVuKhiMacDinhTheoAnim(string anim)
	{
		switch (anim)
		{
		case "NV_Thuong":
			return "VK_PHUONG_THIEN_KICH";
		case "NV_PhuRiu":
			return "VK_KHAI_SON_PHU";
		case "NV_Phien":
			return "VK_DAP_NGUYET_PHIEN";
		case "NV_Kiem":
			return "VK_QUAN_TU_KIEM";
		case "NV_Hoan":
			return "VK_LONG_PHUNG_SONG_HOAN";
		case "NV_Dao":
			return "VK_LANH_NGUYET_BAO_DAO";
		case "NV_ConBong":
			return "VK_DA_CAU_BONG";
		case "NV_ButTieu":
			return "VK_BICH_NGOC_DICH";
		case "NV_AmKhi":
			return "VK_BICH_NGOC_DICH";
		default:
			return string.Empty;
		}
	}

	public static string GetEnglishAnimName(AnimVuKhi anim)
	{
		switch (anim)
		{
		case AnimVuKhi.NV_AmKhi:
			return "NV_shuriken";
		case AnimVuKhi.NV_BaoTay:
			return "NV_null";
		case AnimVuKhi.NV_ButTieu:
			return "NV_flute";
		case AnimVuKhi.NV_ConBong:
			return "NV_staff";
		case AnimVuKhi.NV_Dao:
			return "NV_dao";
		case AnimVuKhi.NV_Hoan:
			return "NV_ring";
		case AnimVuKhi.NV_Kiem:
			return "NV_sword";
		case AnimVuKhi.NV_Phien:
			return "NV_fan";
		case AnimVuKhi.NV_PhuRiu:
			return "NV_axe";
		case AnimVuKhi.NV_Thuong:
			return "NV_spear";
		default:
			return string.Empty;
		}
	}

	public int GetDanhSonIdxFromGiangHoIdx(int giangHoIdx)
	{
		return giangHoIdx - 9;
	}

	public int GetGiangHoIdxFromDanhSonIdx(int danhSonIdx)
	{
		return danhSonIdx + 9;
	}

	public int GetSoLuotGiupBanDanhSon(string str)
	{
		string text = "HelpDanhSon";
		if (str.Contains(text))
		{
			int startIndex = str.IndexOf(text) + text.Length;
			string s = str.Substring(startIndex, 2);
			return int.Parse(s);
		}
		return 0;
	}

	public string GetSoLuotGiupBanDanhSonString(int count)
	{
		return string.Format("HelpDanhSon{0:00};", count);
	}

	public int GetSoLanNhanEventGiangHoByString(string str)
	{
		string text = "EventGiangHo";
		if (str.Contains(text))
		{
			int startIndex = str.IndexOf(text) + text.Length;
			string s = str.Substring(startIndex, 3);
			return int.Parse(s);
		}
		return 0;
	}

	public string GetSoLanNhanEventGiangHoString(int count)
	{
		return string.Format("EventGiangHo{0:000};", count);
	}

	public int GetSoLuotDuocTangTheLucByString(string str)
	{
		string text = "DuocTheLuc";
		if (str.Contains(text))
		{
			int startIndex = str.IndexOf(text) + text.Length;
			string s = str.Substring(startIndex, 2);
			return int.Parse(s);
		}
		return 0;
	}

	public string GetSoLuotDuocTangTheLucString(int count)
	{
		return string.Format("DuocTheLuc{0:00};", count);
	}

	public string GetSoLuotCT2String(int count)
	{
		return string.Format("ChienTruong_{0:00};", count);
	}

	public int GetSoLuotCT2ByString(string str)
	{
		string text = "ChienTruong_";
		for (int i = 1; i <= 20; i++)
		{
			string text2 = string.Format("{0:00}", i);
			if (str.Contains(text + text2 + ";"))
			{
				return i;
			}
		}
		return 0;
	}

	public int GetSoLuotMuaLuanKiemByString(string str)
	{
		string text = "MuaLuanKiemKNB";
		if (str.Contains(text))
		{
			int startIndex = str.IndexOf(text) + text.Length;
			string s = str.Substring(startIndex, 2);
			return int.Parse(s);
		}
		return 0;
	}

	public int GetSoLuotMuaGaByString(string str)
	{
		string text = "MuaGaKNB";
		if (str.Contains(text))
		{
			int startIndex = str.IndexOf(text) + text.Length;
			string s = str.Substring(startIndex, 2);
			return int.Parse(s);
		}
		return 0;
	}

	public int GetBangChienThanhIdxByString(string ghichu)
	{
		string text = "VaoCongThanhChien";
		if (ghichu.Contains(text))
		{
			int startIndex = ghichu.IndexOf(text) + text.Length;
			string s = ghichu.Substring(startIndex, 2);
			int result = 0;
			if (int.TryParse(s, out result))
			{
				return result;
			}
		}
		return -1;
	}

	public string GetBangChienCongThanhString(int thanhidx)
	{
		return string.Format("VaoCongThanhChien{0:00};", thanhidx);
	}

	public int GetSoLuotThachDauByString(string ghichu)
	{
		string text = "ThachDau";
		if (ghichu.Contains(text))
		{
			int startIndex = ghichu.IndexOf(text) + text.Length;
			string s = ghichu.Substring(startIndex, 2);
			int result = 0;
			if (int.TryParse(s, out result))
			{
				return result;
			}
		}
		return 0;
	}

	public string GetSoLuotThachDauString(int count)
	{
		return string.Format("ThachDau{0:00};", count);
	}

	public int GetRuongBauVatByString(string ghichu, int ruong_idx)
	{
		string text = string.Format("RuongBV{0}_", ruong_idx);
		if (ghichu.Contains(text))
		{
			int startIndex = ghichu.IndexOf(text) + text.Length;
			string s = ghichu.Substring(startIndex, 2);
			int result = 0;
			if (int.TryParse(s, out result))
			{
				return result;
			}
		}
		return 0;
	}

	public string GetSoLuotRuongBauVatString(int ruong_idx, int count)
	{
		return string.Format("RuongBV{0}_{1:00};", ruong_idx, count);
	}

	public string GetMuaLuanKiemString(int count)
	{
		return string.Format("MuaLuanKiemKNB{0:00};", count);
	}

	public string GetMuaGaString(int count)
	{
		return string.Format("MuaGaKNB{0:00};", count);
	}

	public int GetSoLuotDoiDoByString(string str, int idx)
	{
		string text = string.Format("Dong{0}DoiDo", idx);
		if (str.Contains(text))
		{
			int startIndex = str.IndexOf(text) + text.Length;
			string s = str.Substring(startIndex, 2);
			return int.Parse(s);
		}
		return 0;
	}

	public string GetDoiDoString(int count, int idx)
	{
		return string.Format("Dong{0}DoiDo{1:00};", idx, count);
	}

	public int GetNextVipMuaGa(int vip)
	{
		if (vip < 0)
		{
			return 1;
		}
		if (vip < 5)
		{
			return 5;
		}
		if (vip < 7)
		{
			return 7;
		}
		if (vip < 10)
		{
			return 10;
		}
		return -1;
	}

	public int GetMaxMuaGaByVip(int vip)
	{
		if (vip < 0)
		{
			return 1;
		}
		if (vip < 5)
		{
			return 2;
		}
		if (vip < 7)
		{
			return 6;
		}
		if (vip < 10)
		{
			return 15;
		}
		return 45;
	}

	public int GetGiaMuaLuanKiemLenhByCount(int count)
	{
		if (count < 2)
		{
			return 10;
		}
		if (count < 6)
		{
			return 20;
		}
		if (count < 15)
		{
			return 30;
		}
		return 50;
	}

	public int GetGiaMuaGaByCount(int count)
	{
		if (count < 2)
		{
			return 15;
		}
		if (count < 6)
		{
			return 35;
		}
		if (count < 15)
		{
			return 10 + count * 5;
		}
		return count * 10 - 60;
	}

	public int GetHuaNguyenByVip(int vip)
	{
		if (vip < 0)
		{
			return 1;
		}
		switch (vip)
		{
		case 0:
		case 1:
		case 2:
			return 1;
		case 3:
			return 4;
		case 4:
			return 6;
		case 5:
			return 8;
		case 6:
			return 10;
		case 7:
			return 12;
		case 8:
			return 14;
		case 9:
			return 18;
		case 10:
			return 22;
		case 11:
			return 26;
		case 12:
			return 30;
		case 13:
			return 30;
		case 14:
			return 34;
		case 15:
			return 34;
		case 16:
			return 34;
		default:
			return 1;
		}
	}

	public long GetSoVangNapLenVip(int vip)
	{
		switch (vip)
		{
		case 0:
			return 0L;
		case 1:
			return 100L;
		case 2:
			return 200L;
		case 3:
			return 500L;
		case 4:
			return 1500L;
		case 5:
			return 5000L;
		case 6:
			return 9000L;
		case 7:
			return 19000L;
		case 8:
			return 36000L;
		case 9:
			return 59000L;
		case 10:
			return 95000L;
		case 11:
			return 170000L;
		case 12:
			return 300000L;
		case 13:
			return 510000L;
		case 14:
			return 850000L;
		case 15:
			return 1700000L;
		default:
			return -1L;
		}
	}

	public int GetRuongThachSanhExp(int level)
	{
		switch (level)
		{
		case 1:
			return 200;
		case 2:
			return 400;
		case 3:
			return 600;
		case 4:
			return 800;
		case 5:
			return 1000;
		case 6:
			return 1200;
		case 7:
			return 1400;
		case 8:
			return 1600;
		case 9:
			return 1800;
		case 10:
			return 2000;
		case 11:
			return 2200;
		case 12:
			return 2400;
		case 13:
			return 2600;
		case 14:
			return 2800;
		case 15:
			return 3000;
		case 16:
			return 3200;
		case 17:
			return 3400;
		case 18:
			return 3600;
		case 19:
			return 3800;
		case 20:
			return 4000;
		default:
			return 100000000;
		}
	}

	public int GetTuBaoBonExp(int level)
	{
		switch (level)
		{
		case 1:
			return 500;
		case 2:
			return 1000;
		case 3:
			return 2000;
		case 4:
			return 5000;
		case 5:
			return 10000;
		case 6:
			return 20000;
		default:
			return 100000000;
		}
	}

	public int GetLuotQuayTuBaoBon(int count)
	{
		if (count >= 20000)
		{
			return 6;
		}
		if (count >= 10000)
		{
			return 5;
		}
		if (count >= 5000)
		{
			return 4;
		}
		if (count >= 2000)
		{
			return 3;
		}
		if (count >= 1000)
		{
			return 2;
		}
		if (count >= 500)
		{
			return 1;
		}
		return 0;
	}

	public int GetSoLuongDeTuGiap(List<UserInfo.HeroData> listHero)
	{
		int num = 0;
		foreach (UserInfo.HeroData item in listHero)
		{
			if (instance.m_dicNhanVats.ContainsKey(item.Name))
			{
				NhanVatCfg nhanVatCfg = instance.m_dicNhanVats[item.Name];
				if (nhanVatCfg.Hang >= 3)
				{
					num++;
				}
			}
		}
		return num;
	}

	public int GetSoLuongTrangBiGiap(List<UserInfo.TrangBiData> listTrangBi)
	{
		int num = 0;
		foreach (UserInfo.TrangBiData item in listTrangBi)
		{
			if (instance.m_dicTrangBi.ContainsKey(item.Name))
			{
				TrangBiCfg trangBiCfg = instance.m_dicTrangBi[item.Name];
				if (trangBiCfg.Hang >= ItemClass.Giap)
				{
					num++;
				}
			}
		}
		return num;
	}

	public static int GetVipEnableResetLuotGH()
	{
		return 3;
	}

	public static int GetCostResetLuotGH()
	{
		return 5;
	}

	public static int GetMaxLuotDanhNhanhGiangHo()
	{
		return 10;
	}

	public static int GetVipEnableDanhNhanhGiangHo()
	{
		return 3;
	}

	public static TimeSpan GetTimeWaitForResetLuotDanhNhanhByVip(int vip)
	{
		int[] array = new int[14]
		{
			0, 0, 0, 40, 40, 40, 20, 20, 20, 10,
			10, 10, 5, 0
		};
		if (vip < 3)
		{
			return TimeSpan.FromMinutes(0.0);
		}
		if (vip > array.Length - 1)
		{
			return TimeSpan.FromMinutes(array[array.Length - 1]);
		}
		return TimeSpan.FromMinutes(array[vip]);
	}

	public static string GetVatPhamGapDoiCaoNhan()
	{
		return "VP_BINH_RUOU";
	}

	public static string GetVatPhamGapDoiBanDo()
	{
		return "VP_BAN_DO_KHO_BAU";
	}

	public static int GetCostDanhNhanhGiangHo(int vip, TimeSpan duration)
	{
		TimeSpan timeWaitForResetLuotDanhNhanhByVip = GetTimeWaitForResetLuotDanhNhanhByVip(vip);
		if (duration >= timeWaitForResetLuotDanhNhanhByVip)
		{
			return 0;
		}
		int num = (int)timeWaitForResetLuotDanhNhanhByVip.TotalMinutes / 5;
		int num2 = (int)(timeWaitForResetLuotDanhNhanhByVip - duration).TotalMinutes;
		return num2 / num + 1;
	}

	public int GetLuotDanhNhanh(int theLuc, int luotConLai)
	{
		int maxLuotDanhNhanhGiangHo = GetMaxLuotDanhNhanhGiangHo();
		maxLuotDanhNhanhGiangHo = ((maxLuotDanhNhanhGiangHo <= theLuc) ? maxLuotDanhNhanhGiangHo : theLuc);
		return (maxLuotDanhNhanhGiangHo <= luotConLai) ? maxLuotDanhNhanhGiangHo : luotConLai;
	}

	public int GetBonusFromTinhLuyenTrangBi(int tinhLuyenLvl)
	{
		return 100 + tinhLuyenLvl * 10;
	}

	public int GetDoiHinhOpenSlotByLevel(int lvl)
	{
		switch (lvl)
		{
		case 2:
			return 2;
		case 6:
			return 3;
		case 11:
			return 4;
		case 16:
			return 5;
		case 21:
			return 6;
		case 26:
			return 7;
		default:
			return -1;
		}
	}

	public int GetNumOpenDoiHinhSlotByLevel(int lvl)
	{
		if (lvl < 2)
		{
			return 2;
		}
		if (lvl < 6)
		{
			return 3;
		}
		if (lvl < 11)
		{
			return 4;
		}
		if (lvl < 16)
		{
			return 5;
		}
		if (lvl < 21)
		{
			return 6;
		}
		if (lvl < 26)
		{
			return 7;
		}
		return 8;
	}

	public int GetNextLevelUnLockDoiHinhSlot(int lvl)
	{
		if (lvl < 2)
		{
			return 2;
		}
		if (lvl < 6)
		{
			return 6;
		}
		if (lvl < 11)
		{
			return 11;
		}
		if (lvl < 16)
		{
			return 16;
		}
		if (lvl < 21)
		{
			return 21;
		}
		if (lvl < 26)
		{
			return 26;
		}
		return -1;
	}

	public int GetExpCaoNhanByNhiemVuGiangHo(UserInfo.CaoNhanData caoNhan, GiangHoCfg.NhiemVu nv)
	{
		float num = 1f;
		switch (caoNhan.Loai)
		{
		case UserInfo.CaoNhanData.ClassLevel.THAP:
			num = 2f;
			break;
		case UserInfo.CaoNhanData.ClassLevel.TRUNG:
			num = 3f;
			break;
		case UserInfo.CaoNhanData.ClassLevel.CAO:
			num = 4f;
			break;
		case UserInfo.CaoNhanData.ClassLevel.SIEU_CAP:
			num = 5f;
			break;
		}
		return (int)(num * (float)nv.ExpThuong);
	}

	public int GetExpByCaoNhanData(UserInfo.CaoNhanData caoNhan, int level)
	{
		int num = 0;
		switch (caoNhan.Loai)
		{
		case UserInfo.CaoNhanData.ClassLevel.THAP:
			num = 100;
			break;
		case UserInfo.CaoNhanData.ClassLevel.TRUNG:
			num = 200;
			break;
		case UserInfo.CaoNhanData.ClassLevel.CAO:
			num = 400;
			break;
		case UserInfo.CaoNhanData.ClassLevel.SIEU_CAP:
			num = 600;
			break;
		}
		return (int)((float)(level * num) / caoNhan.X);
	}

	public int GetSoQuyenCanTinhLuyen(string name)
	{
		CfgVoCong cfgVoCong = m_dicVCs[name];
		if (cfgVoCong.Hang == 2)
		{
			if (name.EndsWith("_B"))
			{
				return 2;
			}
			if (name.EndsWith("_A"))
			{
				return 3;
			}
		}
		else if (cfgVoCong.Hang == 3)
		{
			if (name.EndsWith("_B"))
			{
				return 1;
			}
			if (name.EndsWith("_A"))
			{
				return 2;
			}
			if (name.EndsWith("_S"))
			{
				return 2;
			}
		}
		return 0;
	}

	public float GetTileThamNgo(UserInfo.VoCongData VoCongDuocThamNgo, List<UserInfo.VoCongData> ListVoCongSuDung)
	{
		int totalThamNgoExp = GetTotalThamNgoExp(VoCongDuocThamNgo, ListVoCongSuDung);
		int thamNgoExp = GetThamNgoExp(VoCongDuocThamNgo.Name, VoCongDuocThamNgo.Level + 1);
		float num = (float)totalThamNgoExp / (float)thamNgoExp;
		if (num > 1f)
		{
			num = 1f;
		}
		return num;
	}

	public int GetThamNgoExp(string name, int lvl)
	{
		CfgVoCong cfgVoCong = m_dicVCs[name];
		int result = 0;
		switch (cfgVoCong.Hang)
		{
		case 1:
			result = OtherConfig.VoCongHang1NangCapHeSo[lvl - 1];
			break;
		case 2:
			result = OtherConfig.VoCongHang2NangCapHeSo[lvl - 1];
			break;
		case 3:
			result = OtherConfig.VoCongHang3NangCapHeSo[lvl - 1];
			break;
		}
		return result;
	}

	public int GetTotalThamNgoExp(UserInfo.VoCongData VoCongDuocThamNgo, List<UserInfo.VoCongData> ListVoCongSuDung)
	{
		int num = VoCongDuocThamNgo.ThamNgoExp;
		foreach (UserInfo.VoCongData item in ListVoCongSuDung)
		{
			num += GetThamNgoExp(item.Name, item.Level);
		}
		return num;
	}

	public int GetTinhLuyenMaxExp(string name, int tinh_luyen_lvl)
	{
		TrangBiCfg trangBiCfg = m_dicTrangBi[name];
		int num = 0;
		int num2;
		switch (trangBiCfg.Hang)
		{
		case ItemClass.Binh:
			num2 = 30;
			break;
		case ItemClass.At:
			num2 = 40;
			break;
		case ItemClass.Giap:
			num2 = 60;
			break;
		default:
			num2 = 60;
			break;
		}
		return (int)((double)num2 * Math.Pow(3.0, tinh_luyen_lvl - 1));
	}

	public int GetDiemTinhLuyenTuManhTrangBi(string name, int count)
	{
		TrangBiCfg trangBiCfg = m_dicTrangBi[name];
		int num = 0;
		int num2;
		switch (trangBiCfg.Hang)
		{
		case ItemClass.Binh:
			num2 = 1;
			break;
		case ItemClass.At:
			num2 = 5;
			break;
		default:
			num2 = 25;
			break;
		}
		return num2 * count;
	}

	public long GetGiaBanTrangBi(UserInfo.TrangBiData data)
	{
		TrangBiCfg trangBiCfg = m_dicTrangBi[data.Name];
		int num = 0;
		int num2;
		switch (trangBiCfg.Hang)
		{
		case ItemClass.Binh:
			num2 = 40000;
			break;
		case ItemClass.At:
			num2 = 200000;
			break;
		default:
			num2 = 200000;
			break;
		}
		num = num2;
		long num3 = 0L;
		if (data.Level > 2)
		{
			num3 = GetGiaCuongHoaTrangBi(data.Name, data.Level - 1);
		}
		return num + num3;
	}

	public long GetGiaCuongHoaTrangBi(string trangBiName, int current_level)
	{
		if (!instance.m_dicTrangBi.ContainsKey(trangBiName))
		{
			return -1L;
		}
		if (current_level > OtherConfig.TrangBiHeSoNangCap.Count || current_level <= 0)
		{
			return -1L;
		}
		TrangBiCfg trangBiCfg = instance.m_dicTrangBi[trangBiName];
		int hang = (int)trangBiCfg.Hang;
		int num = -1;
		LoaiTrangBi loaiTrangBi = TrangBiCfg.GetLoaiTrangBi(trangBiName);
		if (loaiTrangBi == LoaiTrangBi.VuKhi)
		{
			num = OtherConfig.VuKhiGiaNangCapLevel1[hang - 1];
		}
		if (loaiTrangBi == LoaiTrangBi.AoGiap)
		{
			num = OtherConfig.AoGiapGiaNangCapLevel1[hang - 1];
		}
		if (loaiTrangBi == LoaiTrangBi.TrangSuc)
		{
			num = OtherConfig.TrangSucGiaNangCapLevel1[hang - 1];
		}
		if (loaiTrangBi == LoaiTrangBi.Mu)
		{
			num = OtherConfig.MuGiaNangCapLevel1[hang - 1];
		}
		if (num == -1)
		{
			return -1L;
		}
		float num2 = OtherConfig.TrangBiHeSoNangCap[current_level - 1];
		float num3 = (float)num * num2;
		return (long)num3;
	}

	public long GetGiaCuongHoaTrangBi(UserInfo.TrangBiData data)
	{
		return GetGiaCuongHoaTrangBi(data.Name, data.Level);
	}

	public bool CheckDieuKienCuongHoa(UserInfo.TrangBiData tb_data, UserInfo.GamerData gamer_data)
	{
		if (tb_data.Level < gamer_data.Level * 3)
		{
			return true;
		}
		return false;
	}

	public int GetBddTruyenCongNhanDuoc(UserInfo.HeroData DeTuRutExp)
	{
		return (int)((float)DeTuRutExp.BddTichLuy * 0.35f);
	}

	public UserInfo.HeroData GetKetQuaTruyenCong(UserInfo.HeroData DeTuNhanExp, UserInfo.HeroData DeTuRutExp, UserInfo.VatPhamTieuThuData vatPhamTruyenCong)
	{
		long expTruyenCongNhanDuoc = GetExpTruyenCongNhanDuoc(DeTuRutExp, vatPhamTruyenCong);
		return UserInfo.HeroGetExp(DeTuNhanExp, expTruyenCongNhanDuoc);
	}

	public long GetExpTruyenCongNhanDuoc(UserInfo.HeroData DeTuRutExp, UserInfo.VatPhamTieuThuData vatPhamTruyenCong)
	{
		long nhanVatTotalExp = GetNhanVatTotalExp(DeTuRutExp);
		float num = 0f;
		if (vatPhamTruyenCong.Name == "VP_TRUYEN_CONG_1")
		{
			num = 0.8f;
		}
		else if (vatPhamTruyenCong.Name == "VP_TRUYEN_CONG_2")
		{
			num = 1f;
		}
		return (long)((float)nhanVatTotalExp * num);
	}

	public long GetNhanVatTotalExp(UserInfo.HeroData data)
	{
		long num = 0L;
		for (int i = 1; i < data.Level; i++)
		{
			long nhanVatMaxExpByLevel = OtherConfig.GetNhanVatMaxExpByLevel(data.Name, i);
			num += nhanVatMaxExpByLevel;
		}
		return num + data.Exp;
	}

	public long GetChuyenSinhExpTraLai(UserInfo.HeroData data)
	{
		UserInfo.HeroData heroData = new UserInfo.HeroData(data);
		long nhanVatTotalExp = instance.GetNhanVatTotalExp(heroData);
		heroData.Exp = 0L;
		heroData.Level = 300;
		long nhanVatTotalExp2 = instance.GetNhanVatTotalExp(heroData);
		heroData.Exp = 0L;
		heroData.Level = 300;
		long nhanVatTotalExp3 = instance.GetNhanVatTotalExp(heroData);
		long num = nhanVatTotalExp - nhanVatTotalExp2;
		if (num > nhanVatTotalExp3)
		{
			num = nhanVatTotalExp3;
		}
		return num;
	}

	public string GetMaMauByQuality(UserInfo.PetInfo.PetQuality quality)
	{
		switch (quality)
		{
		case UserInfo.PetInfo.PetQuality.PHO_THONG:
			return "[EEEEEE]";
		case UserInfo.PetInfo.PetQuality.UU_TU:
			return "[00FF00]";
		case UserInfo.PetInfo.PetQuality.TRAC_VIET:
			return "[3355FF]";
		case UserInfo.PetInfo.PetQuality.HUYEN_THOAI:
			return "[FF00FF]";
		case UserInfo.PetInfo.PetQuality.TRUYEN_THUYET:
			return "[FFFF00]";
		default:
			return string.Empty;
		}
	}

	public float GetChiSoDeTuGiaTangByDotPhaLvl(string code_name, int dot_pha_lvl)
	{
		return (float)dot_pha_lvl * 10f;
	}

	public int GetVoCongCanDotPha(string code_name, int dot_pha_lvl)
	{
		return 4;
	}

	public int GetTongTiemLuc(UserInfo.HeroData data)
	{
		return data.Level * 15 + data.TiemLucBoSung + data.ThienMaBuf;
	}

	public int GetTiemLucConLai(UserInfo.HeroData data)
	{
		return GetTongTiemLuc(data) - (data.MenhBoiDuong + data.NgoaiBoiDuong + data.ThanBoiDuong + data.KhiBoiDuong);
	}

	public int GetTongTiemLuc(UserInfo.ChienHon data)
	{
		return data.Level * 60;
	}

	public int GetTiemLucConLai(UserInfo.ChienHon data)
	{
		return GetTongTiemLuc(data) - (data.MenhBoiDuong + data.NgoaiBoiDuong + data.ThanBoiDuong + data.KhiBoiDuong);
	}

	public int GetManhVoCongCanDeGhep(string code_name)
	{
		string text = code_name;
		if (text.StartsWith("MVC_"))
		{
			text = "VC_" + text.Substring(4);
		}
		CfgVoCong value = null;
		if (!m_dicVCs.TryGetValue(text, out value))
		{
			m_dicVCs.TryGetValue(code_name, out value);
		}
		if (value != null)
		{
			int result;
			switch (value.Hang)
			{
			case 1:
				result = 5;
				break;
			case 2:
				result = 15;
				break;
			case 3:
				result = 30;
				break;
			default:
				result = 30;
				break;
			}
			return result;
		}
		return 30;
	}

	public int GetManhTrangBiCanDeGhep(string code_name)
	{
		string text = code_name;
		if (text.StartsWith("MMAG_"))
		{
			text = text.Substring(1);
		}
		if (text.StartsWith("MVK_") || text.StartsWith("MMU_") || text.StartsWith("MAG_") || text.StartsWith("MTS_"))
		{
			text = text.Substring(1);
		}
		TrangBiCfg value = null;
		if (!m_dicTrangBi.TryGetValue(text, out value))
		{
			m_dicTrangBi.TryGetValue(code_name, out value);
		}
		if (value != null)
		{
			int result;
			switch ((int)value.Hang)
			{
			case 1:
				result = 5;
				break;
			case 2:
				result = 15;
				break;
			case 3:
				result = 30;
				break;
			default:
				result = 30;
				break;
			}
			return result;
		}
		return 30;
	}

	public int GetTanHonCanTrieuHoi(string code_name)
	{
		string text = code_name;
		if (text.StartsWith("TH_"))
		{
			text = "NV_" + text.Substring(3);
		}
		NhanVatCfg value = null;
		if (!m_dicNhanVats.TryGetValue(text, out value))
		{
			m_dicNhanVats.TryGetValue(code_name, out value);
		}
		if (value != null)
		{
			int result;
			switch (value.Hang)
			{
			case 1:
				result = 5;
				break;
			case 2:
				result = 15;
				break;
			case 3:
				result = 30;
				break;
			default:
				result = 30;
				break;
			}
			return result;
		}
		return 30;
	}

	public int GetTanHonCanDotPha(string code_name, int tang_dot_pha_hien_tai)
	{
		return 30 + tang_dot_pha_hien_tai * 30;
	}

	public void ReadCfgVCTimeEffect(string cfg_data)
	{
		m_dicVCTimeEffects = JsonMapper.ToObject<Dictionary<string, VCTimeEffectCfg>>(cfg_data);
	}

	public void ReadCfgAttackTimeEffect(string cfg_data)
	{
		m_dicAttackTimeEffects = JsonMapper.ToObject<Dictionary<string, NVAnimTimeCfg>>(cfg_data);
	}

	public int CompareNhanVat(string codeName1, int level1, string codeName2, int level2)
	{
		if (!m_dicNhanVats.ContainsKey(codeName1) || !m_dicNhanVats.ContainsKey(codeName2))
		{
			return -1;
		}
		if (m_dicNhanVats[codeName1].Hang > m_dicNhanVats[codeName2].Hang)
		{
			return -1;
		}
		if (m_dicNhanVats[codeName1].Hang < m_dicNhanVats[codeName2].Hang)
		{
			return 1;
		}
		if (level1 > level2)
		{
			return -1;
		}
		if (level1 < level2)
		{
			return 1;
		}
		return codeName1.CompareTo(codeName2);
	}

	public int CompareThanThu(UserInfo.PetInfo thanthu1, UserInfo.PetInfo thanthu2)
	{
		if (thanthu1.Quality > thanthu2.Quality)
		{
			return -1;
		}
		if (thanthu1.Quality < thanthu2.Quality)
		{
			return 1;
		}
		if (thanthu1.level > thanthu2.level)
		{
			return -1;
		}
		if (thanthu1.level < thanthu2.level)
		{
			return 1;
		}
		if (thanthu1.growRate > thanthu2.growRate)
		{
			return -1;
		}
		if (thanthu1.growRate < thanthu2.growRate)
		{
			return 1;
		}
		return thanthu1.codename.CompareTo(thanthu2.codename);
	}

	public int CompareTanHon(string codeName1, int quantity1, string codeName2, int quantity2)
	{
		if (!m_dicNhanVats.ContainsKey(codeName1) || !m_dicNhanVats.ContainsKey(codeName2))
		{
			return -1;
		}
		if (m_dicNhanVats[codeName1].Hang > m_dicNhanVats[codeName2].Hang)
		{
			return -1;
		}
		if (m_dicNhanVats[codeName1].Hang < m_dicNhanVats[codeName2].Hang)
		{
			return 1;
		}
		if (quantity1 > quantity2)
		{
			return -1;
		}
		if (quantity1 < quantity2)
		{
			return 1;
		}
		return codeName1.CompareTo(codeName2);
	}

	public int CompareVoCong(string codeName1, int level1, string codeName2, int level2, int ID1 = 0, int ID2 = 0)
	{
		if (!m_dicVCs.ContainsKey(codeName1) || !m_dicVCs.ContainsKey(codeName2))
		{
			return -1;
		}
		if (m_dicVCs[codeName1].Hang > m_dicVCs[codeName2].Hang)
		{
			return -1;
		}
		if (m_dicVCs[codeName1].Hang < m_dicVCs[codeName2].Hang)
		{
			return 1;
		}
		if (level1 > level2)
		{
			return -1;
		}
		if (level1 < level2)
		{
			return 1;
		}
		if (codeName1 == codeName2)
		{
			if (ID1 < ID2)
			{
				return -1;
			}
			if (ID2 > ID1)
			{
				return 1;
			}
			return 0;
		}
		return codeName1.CompareTo(codeName2);
	}

	public int CompareLevel(int level1, int level2)
	{
		if (level1 > level2)
		{
			return -1;
		}
		if (level1 < level2)
		{
			return 1;
		}
		return 0;
	}

	public int CompareTrangBi(string codeName1, int level1, string codeName2, int level2, int ID1 = 0, int ID2 = 0)
	{
		if (!m_dicTrangBi.ContainsKey(codeName1) || !m_dicTrangBi.ContainsKey(codeName2))
		{
			return -1;
		}
		if (m_dicTrangBi[codeName1].Hang > m_dicTrangBi[codeName2].Hang)
		{
			return -1;
		}
		if (m_dicTrangBi[codeName1].Hang < m_dicTrangBi[codeName2].Hang)
		{
			return 1;
		}
		if (level1 > level2)
		{
			return -1;
		}
		if (level1 < level2)
		{
			return 1;
		}
		if (codeName1 == codeName2)
		{
			if (ID1 < ID2)
			{
				return -1;
			}
			if (ID2 > ID1)
			{
				return 1;
			}
			return 0;
		}
		return codeName1.CompareTo(codeName2);
	}

	public int CompareBanBe(bool online1, UserInfo.BanBeData.BanBeStatus status1, int level1, bool online2, UserInfo.BanBeData.BanBeStatus status2, int level2)
	{
		if (online1 | online2)
		{
			return -1;
		}
		if (status1 > status2)
		{
			return -1;
		}
		if (status1 < status2)
		{
			return 1;
		}
		if (level1 > level2)
		{
			return -1;
		}
		if (level1 < level2)
		{
			return 1;
		}
		return 0;
	}

	public void ReadCfgVC(string cfg_data)
	{
		m_dicVCs = JsonMapper.ToObject<Dictionary<string, CfgVoCong>>(cfg_data);
		foreach (KeyValuePair<string, CfgVoCong> dicVC in m_dicVCs)
		{
			dicVC.Value.Name = dicVC.Key;
			dicVC.Value.TypeStr = dicVC.Key;
		}
	}

	public void ReadGoiVatPhamCfg(string cfg_data)
	{
		m_GoiVatPhamList = JsonMapper.ToObject<List<GoiVatPhamCfg>>(cfg_data);
	}

	public void ReadNhanVatCfg(string cfg_data)
	{
		m_dicNhanVats = JsonMapper.ToObject<Dictionary<string, NhanVatCfg>>(cfg_data);
		foreach (KeyValuePair<string, NhanVatCfg> dicNhanVat in m_dicNhanVats)
		{
			dicNhanVat.Value.Name = dicNhanVat.Key;
		}
	}

	public void ReadVatPhamTieuThuCfg(string cfg_data)
	{
		m_dicVatPhamTieuThu = JsonMapper.ToObject<Dictionary<string, VatPhamTieuThuCfg>>(cfg_data);
		foreach (KeyValuePair<string, VatPhamTieuThuCfg> item in m_dicVatPhamTieuThu)
		{
			item.Value.Name = item.Key;
		}
	}

	public void ReadOtherConfig(string cfg_data)
	{
		OtherConfig = JsonMapper.ToObject<OtherCfg>(cfg_data);
		OtherConfig.IsValid();
		foreach (KeyValuePair<string, OtherCfg.NguyenKhiCfg> item in OtherConfig.NguyenKhiConfig)
		{
			item.Value.Codename = item.Key;
		}
	}

	public void ReadGiangHoConfig(string cfg_data)
	{
		m_listGiangHo = JsonMapper.ToObject<List<GiangHoCfg>>(cfg_data);
	}

	public void ReadGiangHoTinhAnhConfig(string cfg_data)
	{
		m_listGiangHoTinhAnh = JsonMapper.ToObject<List<GiangHoCfg>>(cfg_data);
	}

	public void MarkGiangHoReady()
	{
		m_giangHoReady = true;
	}

	public void ReadHoaVangConfig(string cfg_data)
	{
		HoaVangConfig = JsonMapper.ToObject<HoaVangCfg>(cfg_data);
	}

	public void ReadChienHonCfg(string cfg_data)
	{
		m_dicChienHons = JsonMapper.ToObject<Dictionary<string, ChienHonCfg>>(cfg_data);
		foreach (KeyValuePair<string, ChienHonCfg> dicChienHon in m_dicChienHons)
		{
			dicChienHon.Value.Name = dicChienHon.Key;
		}
	}

	public void ReadHuyenKhiCfg(string cfg_data)
	{
		m_dicHuyenKhi = JsonMapper.ToObject<Dictionary<string, HuyenKhiCfg>>(cfg_data);
		foreach (KeyValuePair<string, HuyenKhiCfg> item in m_dicHuyenKhi)
		{
			item.Value.Name = item.Key;
		}
	}

	public void ReadEffHuyenKhiCfg(string cfg_data)
	{
		m_dicEffHuyenKhi = JsonMapper.ToObject<Dictionary<string, EffHuyenKhiCfg>>(cfg_data);
		foreach (KeyValuePair<string, EffHuyenKhiCfg> item in m_dicEffHuyenKhi)
		{
			item.Value.Name = item.Key;
		}
	}

	public void ReadDanhSonConfig(string cfg_data)
	{
		m_listDanhSon = JsonMapper.ToObject<List<DanhSonCfg>>(cfg_data);
	}

	public void ReadTrangBiConfig(string cfg_data)
	{
		m_dicTrangBi = JsonMapper.ToObject<Dictionary<string, TrangBiCfg>>(cfg_data);
		foreach (string key in m_dicTrangBi.Keys)
		{
			TrangBiCfg trangBiCfg = m_dicTrangBi[key];
			trangBiCfg.Name = key;
		}
	}

	public void ReadHuyetChienCfg(string huyetChienTxt)
	{
		HuyetChienConfig = JsonMapper.ToObject<HuyetChienCfg>(huyetChienTxt);
	}

	public void ReadServerCfg(string data)
	{
		ServerConfig = JsonMapper.ToObject<ServerCfg>(data);
	}

	public void ReadCostumeConfig(string data)
	{
		m_dicCostumeCfg = JsonMapper.ToObject<Dictionary<string, CostumeCfg>>(data);
	}

	public void ReadQuangMinhDinhCfg(string data)
	{
		QuangMinhDinhConfig = JsonMapper.ToObject<QuangMinhDinhCfg>(data);
		string message = string.Empty;
		if (!QuangMinhDinhConfig.IsValid(out message))
		{
			Console.WriteLine(message);
		}
	}

	public void ReadDaoThanThuMap(string data)
	{
		ThanThuDaoMapList = JsonMapper.ToObject<List<string>>(data);
	}

	public void ReadSonMonConfig(string data)
	{
		SonMonConfig = JsonMapper.ToObject<SonMonCfg>(data);
	}

	public void ReadServerSideConfig(string quang_minh_dinh)
	{
		ReadQuangMinhDinhCfg(quang_minh_dinh);
	}

	public void ReadCuaHangThanBiConfig(string cfg_data)
	{
		m_CuaHangThanBiList = JsonMapper.ToObject<List<CuaHangThanBiCfg>>(cfg_data);
	}

	public void ReadAllConfig(string vocongTxt, string trangbiTxt, string nhanVatTxt, string vatphamtieuthuTxt, string giangHoTxt, string danhsonTxt, string otherTxt, string vcTimeEffText, string attackTimeEffText, string goiVatPhamTxt, string huyetChienTxt, string serverTxt, string costumeTxt, string thanthudaoTxt, string sonmonTxt, string cuahangthanbiTxt, string gianghotinhanhTxt, string hoavangTxt, string chienHonTxt, string huyenKhiTxt, string effHuyenKhiTxt)
	{
		ReadCfgVC(vocongTxt);
		ReadTrangBiConfig(trangbiTxt);
		ReadNhanVatCfg(nhanVatTxt);
		ReadDaoThanThuMap(thanthudaoTxt);
		ReadSonMonConfig(sonmonTxt);
		ReadVatPhamTieuThuCfg(vatphamtieuthuTxt);
		if (!string.IsNullOrEmpty(giangHoTxt))
		{
			ReadGiangHoConfig(giangHoTxt);
		}
		ReadDanhSonConfig(danhsonTxt);
		ReadOtherConfig(otherTxt);
		ReadCfgVCTimeEffect(vcTimeEffText);
		ReadCfgAttackTimeEffect(attackTimeEffText);
		ReadGoiVatPhamCfg(goiVatPhamTxt);
		ReadHuyetChienCfg(huyetChienTxt);
		ReadServerCfg(serverTxt);
		ReadCostumeConfig(costumeTxt);
		ReadCuaHangThanBiConfig(cuahangthanbiTxt);
		if (!string.IsNullOrEmpty(gianghotinhanhTxt))
		{
			ReadGiangHoTinhAnhConfig(gianghotinhanhTxt);
		}
		ReadHoaVangConfig(hoavangTxt);
		ReadChienHonCfg(chienHonTxt);
		ReadHuyenKhiCfg(huyenKhiTxt);
		ReadEffHuyenKhiCfg(effHuyenKhiTxt);
	}

	public static int LevelUnlockLuanKiem()
	{
		return 5;
	}

	public static TimeSpan GetTimeUpdateDiemLuanKiem()
	{
		return TimeSpan.FromHours(6.0);
	}

	public static TimeSpan GetTimeIntervalUpdateDiemLuanKiem()
	{
		return TimeSpan.FromMinutes(10.0);
	}

	public static float GetXacSuatThamNgoVoCong(int target_vo_cong_id, List<int> list_vo_cong_su_dung)
	{
		return 10f;
	}

	public static int GetDiemThuongCanDoiLK(DoiThuongEnum doiThuong)
	{
		switch (doiThuong)
		{
		case DoiThuongEnum.Thuong1000:
			return 1000;
		case DoiThuongEnum.Thuong10000:
			return 10000;
		default:
			return 1000000;
		}
	}

	public static string GetAvatarDoiThuong(DoiThuongEnum doiThuong)
	{
		return "VP_BOI_DUONG_DAN";
	}

	public static int GetLuotLuanKiemByVip(int vip)
	{
		if (vip <= 2)
		{
			return 6;
		}
		if (vip == 3)
		{
			return 7;
		}
		if (vip == 4)
		{
			return 8;
		}
		if (vip == 5)
		{
			return 9;
		}
		if (vip == 6)
		{
			return 10;
		}
		if (vip == 7)
		{
			return 11;
		}
		if (vip == 8)
		{
			return 12;
		}
		if (vip == 9)
		{
			return 13;
		}
		if (vip == 10)
		{
			return 14;
		}
		if (vip == 11)
		{
			return 15;
		}
		if (vip == 12)
		{
			return 16;
		}
		if (vip == 13)
		{
			return 17;
		}
		if (vip >= 14)
		{
			return 18;
		}
		return 6;
	}

	public string GetRandomTenHienThi(int num)
	{
		int count = OtherConfig.TenHienThiTienTo.Count;
		int count2 = OtherConfig.TenHienThiHauTo.Count;
		int num2 = num / count2 % count;
		int num3 = num % count2;
		if (num2 >= count || num3 >= count2)
		{
			return string.Empty;
		}
		return OtherConfig.TenHienThiTienTo[num2] + " " + OtherConfig.TenHienThiHauTo[num3];
	}

	public string GetRandomBotName(int num)
	{
		int count = OtherConfig.BotTienTo.Count;
		int index = num % count;
		return OtherConfig.BotTienTo[index] + " " + GetRandomTenHienThi(num);
	}

	public CfgVoCong GetVoCongCfgByType(VCType type)
	{
		return m_dicVCs[type.ToString()];
	}

	public int CostHoiSinhDanhDongNhan()
	{
		return 2;
	}

	public void CheckDuyenValid()
	{
		foreach (KeyValuePair<string, NhanVatCfg> dicNhanVat in m_dicNhanVats)
		{
			foreach (NhanVatCfg.DuyenPhanCfg item in dicNhanVat.Value.DuyenPhan)
			{
				if (item.LoaiDuyenPhan == LoaiDuyenPhan.CungDoi)
				{
					foreach (string item2 in item.DoiTuong)
					{
						if (!instance.m_dicNhanVats.ContainsKey(item2))
						{
							EGDebug.LogError("Nhập lỗi duyên đối tương: " + item2);
						}
					}
				}
				if (item.LoaiDuyenPhan != LoaiDuyenPhan.TrangBiDo)
				{
					continue;
				}
				foreach (string item3 in item.DoiTuong)
				{
					if (!instance.m_dicTrangBi.ContainsKey(item3))
					{
						EGDebug.LogError("Nhập lỗi duyên đối tương: " + item3);
					}
				}
			}
		}
	}

	public int CostLapLienMinh()
	{
		return 500;
	}

	public int CostFinishNhiemVuLienMinh()
	{
		return 10;
	}

	public int CostResetNhiemVuLienMinh()
	{
		return 80;
	}

	public int CostDangHuongLienMinhCaoCap()
	{
		return 100;
	}

	public int CongHienDangHuongLienMinh(bool isCaoCap)
	{
		if (isCaoCap)
		{
			return 500;
		}
		return 300;
	}

	public PhanThuongResponse PhanThuongNhiemVuLienMinh(int level)
	{
		PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
		PhanThuongResponse.PhanThuong phanThuong = new PhanThuongResponse.PhanThuong();
		phanThuong.Name = "BAC";
		phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.BAC;
		phanThuong.Count = 20000 * level;
		phanThuongResponse.PhanThuongList.Add(phanThuong);
		return phanThuongResponse;
	}

	public void ReadDoiThuongLienMinhCfg(string data)
	{
		DoiThuongLienMinhConfig = JsonMapper.ToObject<Dictionary<string, DoiThuongLienMinhCfg>>(data);
		foreach (KeyValuePair<string, DoiThuongLienMinhCfg> item in DoiThuongLienMinhConfig)
		{
		}
	}

	public void ReadCongTrinhLienMinhCfg(string data)
	{
		CongTrinhLienMinhConfig = JsonMapper.ToObject<Dictionary<string, CongTrinhLienMinhCfg>>(data);
		foreach (KeyValuePair<string, CongTrinhLienMinhCfg> item in CongTrinhLienMinhConfig)
		{
			item.Value.CodeName = item.Key;
		}
	}

	public int GetCostNangCapCongTrinh(LienMinhCongTrinh congtrinh)
	{
		if (congtrinh.CongTrinhType == LienMinhCongTrinh.CONGTRINH.TANG_KIEM_CAC)
		{
			return CongTrinhLienMinhConfig["TangKiemCac"].CongHien[congtrinh.CongTrinhLevel + 1];
		}
		if (congtrinh.CongTrinhType == LienMinhCongTrinh.CONGTRINH.THIEN_HA_LAU)
		{
			return CongTrinhLienMinhConfig["ThienHaLau"].CongHien[congtrinh.CongTrinhLevel + 1];
		}
		if (congtrinh.CongTrinhType == LienMinhCongTrinh.CONGTRINH.TU_NGHIA_DUONG)
		{
			return CongTrinhLienMinhConfig["TuNghiaDuong"].CongHien[congtrinh.CongTrinhLevel + 1];
		}
		return 100000;
	}
}

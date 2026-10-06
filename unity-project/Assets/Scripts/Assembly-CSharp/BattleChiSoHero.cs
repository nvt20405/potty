using System;

public class BattleChiSoHero
{
	public static bool CheckActiveDuyen(int HID, BattleGamerInfo battleGInfo, NhanVatCfg.DuyenPhanCfg duyenCfg, int duyen_num)
	{
		BattleGamerInfo.DuLieuHero duLieuHero = battleGInfo.DoiHinhRaTran.Find((BattleGamerInfo.DuLieuHero e) => e.HID == HID);
		int result;
		if (duLieuHero == null)
		{
			BattleGamerInfo.DuLieuHHoTro duLieuHHoTro = battleGInfo.DoiHinhTranDo.Find((BattleGamerInfo.DuLieuHHoTro e) => e.HID == HID);
			result = ((duLieuHHoTro != null && duLieuHHoTro.CheckActiveDuyen(duyenCfg, duyen_num)) ? 1 : 0);
		}
		else
		{
			result = (duLieuHero.CheckActiveDuyen(duyenCfg, duyen_num) ? 1 : 0);
		}
		return (byte)result != 0;
	}

	public static ChiSoNhanVat GetChiSoBatQuaiTran(int HID, BattleGamerInfo battleGInfo)
	{
		ChiSoNhanVat chiSoNhanVat = new ChiSoNhanVat();
		BattleGamerInfo.DuLieuHHoTro duLieuHHoTro = battleGInfo.DoiHinhTranDo.Find((BattleGamerInfo.DuLieuHHoTro e) => e.HID == HID);
		if (duLieuHHoTro != null)
		{
			ChiSoNhanVat chisonv = duLieuHHoTro.DuyenPhanChiSoTangThem();
			chiSoNhanVat.Add(chisonv);
			ChiSoNhanVat chisonv2 = duLieuHHoTro.TrangBiDoChiSoTangThem(true);
			chiSoNhanVat.Add(chisonv2);
			chiSoNhanVat.Menh += duLieuHHoTro.Menh * (1f + (float)duLieuHHoTro.CapDotPha * 0.1f);
			chiSoNhanVat.Ngoai += duLieuHHoTro.Ngoai * (1f + (float)duLieuHHoTro.CapDotPha * 0.1f);
			chiSoNhanVat.Noi += duLieuHHoTro.Noi * (1f + (float)duLieuHHoTro.CapDotPha * 0.1f);
			chiSoNhanVat.ThanPhap += duLieuHHoTro.ThanPhap * (1f + (float)duLieuHHoTro.CapDotPha * 0.1f);
			chiSoNhanVat.Menh += duLieuHHoTro.Menh * duLieuHHoTro.MenhCostumeBuff;
			chiSoNhanVat.Ngoai += duLieuHHoTro.Ngoai * duLieuHHoTro.NgoaiCostumeBuff;
			chiSoNhanVat.ThanPhap += duLieuHHoTro.ThanPhap * duLieuHHoTro.ThanCostumeBuff;
			chiSoNhanVat.Noi += duLieuHHoTro.Noi * duLieuHHoTro.KhiCostumeBuff;
			chiSoNhanVat.Menh = (float)Math.Round(chiSoNhanVat.Menh);
			chiSoNhanVat.Ngoai = (float)Math.Round(chiSoNhanVat.Ngoai);
			chiSoNhanVat.Noi = (float)Math.Round(chiSoNhanVat.Noi);
			chiSoNhanVat.ThanPhap = (float)Math.Round(chiSoNhanVat.ThanPhap);
			chiSoNhanVat.BMS += duLieuHHoTro.BMS;
			chiSoNhanVat.BAS += duLieuHHoTro.BAS;
			for (int num = 0; num < battleGInfo.RawDoiHinhData.ListHoTro.Count; num++)
			{
				if (battleGInfo.RawDoiHinhData.ListHoTro[num] != HID)
				{
					continue;
				}
				int batQuaiCuongHoaLevel = ConfigManager.instance.GetBatQuaiCuongHoaLevel(num, battleGInfo.RawDoiHinhData);
				float num2 = 0f;
				float num3 = 0f;
				float num4 = 0f;
				float num5 = 0f;
				if (batQuaiCuongHoaLevel >= 8)
				{
					BattleVoCong battleVoCong = duLieuHHoTro.ListVoCong.Find((BattleVoCong e) => e.Config.m_Class == VCClass.BO_PHAP);
					if (battleVoCong != null)
					{
						float batQuaiChiSo = battleVoCong.Config.GetBatQuaiChiSo(battleVoCong.Level);
						if (battleVoCong.Config.m_BatQuaiType == OtherCfg.NguyenKhiType.MENH)
						{
							num2 += batQuaiChiSo;
						}
						else if (battleVoCong.Config.m_BatQuaiType == OtherCfg.NguyenKhiType.NGOAI)
						{
							num3 += batQuaiChiSo;
						}
						else if (battleVoCong.Config.m_BatQuaiType == OtherCfg.NguyenKhiType.THAN)
						{
							num4 += batQuaiChiSo;
						}
						else if (battleVoCong.Config.m_BatQuaiType == OtherCfg.NguyenKhiType.KHI)
						{
							num5 += batQuaiChiSo;
						}
					}
				}
				if (batQuaiCuongHoaLevel >= 9)
				{
					BattleVoCong battleVoCong2 = duLieuHHoTro.ListVoCong.Find((BattleVoCong e) => e.Config.m_Class == VCClass.NOI_CONG);
					if (battleVoCong2 != null)
					{
						float batQuaiChiSo2 = battleVoCong2.Config.GetBatQuaiChiSo(battleVoCong2.Level);
						if (battleVoCong2.Config.m_BatQuaiType == OtherCfg.NguyenKhiType.MENH)
						{
							num2 += batQuaiChiSo2;
						}
						else if (battleVoCong2.Config.m_BatQuaiType == OtherCfg.NguyenKhiType.NGOAI)
						{
							num3 += batQuaiChiSo2;
						}
						else if (battleVoCong2.Config.m_BatQuaiType == OtherCfg.NguyenKhiType.THAN)
						{
							num4 += batQuaiChiSo2;
						}
						else if (battleVoCong2.Config.m_BatQuaiType == OtherCfg.NguyenKhiType.KHI)
						{
							num5 += batQuaiChiSo2;
						}
					}
				}
				if (num2 > 0f)
				{
					chiSoNhanVat.Menh += duLieuHHoTro.Menh * (num2 * 0.01f);
				}
				if (num3 > 0f)
				{
					chiSoNhanVat.Ngoai += duLieuHHoTro.Ngoai * (num3 * 0.01f);
				}
				if (num4 > 0f)
				{
					chiSoNhanVat.ThanPhap += duLieuHHoTro.ThanPhap * (num4 * 0.01f);
				}
				if (num5 > 0f)
				{
					chiSoNhanVat.Noi += duLieuHHoTro.Noi * (num5 * 0.01f);
				}
				break;
			}
		}
		return chiSoNhanVat;
	}

	public static ChiSoNhanVat GetChiSoRaTran(int HID, BattleGamerInfo battleGInfo, BattleGamerInfo doithu = null)
	{
		ChiSoNhanVat chiSoNhanVat = new ChiSoNhanVat();
		BattleGamerInfo.DuLieuHero duLieuHero = battleGInfo.DoiHinhRaTran.Find((BattleGamerInfo.DuLieuHero e) => e.HID == HID);
		if (duLieuHero != null)
		{
			ChiSoNhanVat chisonv = duLieuHero.ChienHonChiSoTangThem();
			chiSoNhanVat.Add(chisonv);
			ChienHonCfg value = null;
			if (duLieuHero.ChienHonData != null && ConfigManager.instance.m_dicChienHons.TryGetValue(duLieuHero.ChienHonData.Name, out value))
			{
				chiSoNhanVat.TyLeBaoChienHon = value.Bao;
				chiSoNhanVat.TyLeDoChienHon = value.Do;
				chiSoNhanVat.TyLeNeChienHon = value.Ne;
			}
			ChiSoCoSo chisonv2 = BatQuaiTran.TranDoChiSoTongTangThem(battleGInfo);
			chiSoNhanVat.Add(chisonv2);
			ChiSoNhanVat chisonv3 = duLieuHero.DuyenPhanChiSoTangThem();
			chiSoNhanVat.Add(chisonv3);
			ChiSoNhanVat chisonv4 = duLieuHero.TrangBiDoChiSoTangThem(false);
			chiSoNhanVat.Add(chisonv4);
			chiSoNhanVat.Menh += duLieuHero.Menh * duLieuHero.MenhCostumeBuff;
			chiSoNhanVat.Ngoai += duLieuHero.Ngoai * duLieuHero.NgoaiCostumeBuff;
			chiSoNhanVat.ThanPhap += duLieuHero.ThanPhap * duLieuHero.ThanCostumeBuff;
			chiSoNhanVat.Noi += duLieuHero.Noi * duLieuHero.KhiCostumeBuff;
			CostumeCfg value2 = null;
			if (duLieuHero.CosData != null && ConfigManager.instance.m_dicCostumeCfg.TryGetValue(duLieuHero.CosData.CodeName, out value2))
			{
				CostumeCfg.CostumeStat statFromTinhLuyen = value2.GetStatFromTinhLuyen(duLieuHero.CosData.TinhLuyen);
				chiSoNhanVat.TiLeBaoKichCostume = statFromTinhLuyen.Bao;
				chiSoNhanVat.TiLeKhangChuongCostume = statFromTinhLuyen.KhangChuong;
				chiSoNhanVat.PhanTramDoDonCostume = statFromTinhLuyen.DoDon;
				chiSoNhanVat.PhanTramNeCostume = statFromTinhLuyen.Ne;
				chiSoNhanVat.PhanTramChoangCostume = statFromTinhLuyen.PhanTramChoang;
				chiSoNhanVat.ThoiGianChoangCostume = statFromTinhLuyen.TimeChoang;
				chiSoNhanVat.PhanTramPhongChieuCostume = statFromTinhLuyen.PhanTramPhongChieu;
				chiSoNhanVat.ThoiGianPhongChieuCostume = statFromTinhLuyen.TimePhongChieu;
				chiSoNhanVat.PhanTramDinhThanCostume = statFromTinhLuyen.PhanTramDinhThan;
				chiSoNhanVat.ThoiGianDinhThanCostume = statFromTinhLuyen.TimeDinhThan;
				chiSoNhanVat.PhanTramPheThuPhapCostume = statFromTinhLuyen.PhanTramPheThuPhap;
				chiSoNhanVat.ThoiGianPheThuPhapCostume = statFromTinhLuyen.TimePheThuPhap;
				chiSoNhanVat.PhanTramPheCongCostume = statFromTinhLuyen.PhanTramPheCong;
				chiSoNhanVat.ThoiGianPheCongCostume = statFromTinhLuyen.TimePheCong;
				chiSoNhanVat.PhanTramPheThuCostume = statFromTinhLuyen.PhanTramPheThu;
				chiSoNhanVat.ThoiGianPheThuCostume = statFromTinhLuyen.TimePheThu;
				chiSoNhanVat.PhanTramXuatHuyetCostume += statFromTinhLuyen.PhanTramXuatHuyet;
				chiSoNhanVat.ThoiGianXuatHuyetCostume += statFromTinhLuyen.TimeXuatHuyet;
			}
			if (battleGInfo.ThuCuoiData != null)
			{
				chiSoNhanVat.Menh += (float)battleGInfo.ThuCuoiData.MenhBuff + duLieuHero.Menh * (float)battleGInfo.ThuCuoiData.MenhBuffRate / 100f;
				chiSoNhanVat.Ngoai += (float)battleGInfo.ThuCuoiData.NgoaiBuff + duLieuHero.Ngoai * (float)battleGInfo.ThuCuoiData.NgoaiBuffRate / 100f;
				chiSoNhanVat.ThanPhap += (float)battleGInfo.ThuCuoiData.ThanBuff + duLieuHero.ThanPhap * (float)battleGInfo.ThuCuoiData.ThanBuffRate / 100f;
				chiSoNhanVat.Noi += (float)battleGInfo.ThuCuoiData.KhiBuff + duLieuHero.Noi * (float)battleGInfo.ThuCuoiData.KhiBuffRate / 100f;
				chiSoNhanVat.ThuCuoiXacSuatNe = battleGInfo.ThuCuoiData.NeBuff;
				chiSoNhanVat.ThuCuoiXacSuatDoDon = battleGInfo.ThuCuoiData.DoDonBuff;
				chiSoNhanVat.ThuCuoiXacSuatBao = battleGInfo.ThuCuoiData.BaoBuff;
			}
			if (battleGInfo.ThanThuData != null && battleGInfo.ThanThuData.skill == "VC_PET_TIEU_DAO_VO_ANH")
			{
				chiSoNhanVat.ThanThuXacSuatNe = 0.1f;
			}
			if (duLieuHero.NguyenKhiList != null)
			{
				BattleGamerInfo.DuLieuHero.NguyenKhi nguyenKhi = duLieuHero.NguyenKhiList.Find((BattleGamerInfo.DuLieuHero.NguyenKhi e) => e.Type == OtherCfg.NguyenKhiType.MENH);
				if (nguyenKhi != null)
				{
					chiSoNhanVat.Menh += duLieuHero.Menh * nguyenKhi.ChiSo / 100f;
				}
			}
			ChiSoNhanVat chisonv5 = duLieuHero.NguyenKhiChiSoTangThem();
			chiSoNhanVat.Add(chisonv5);
			chiSoNhanVat.Menh += duLieuHero.Menh;
			chiSoNhanVat.Ngoai += duLieuHero.Ngoai;
			chiSoNhanVat.Noi += duLieuHero.Noi;
			chiSoNhanVat.ThanPhap += duLieuHero.ThanPhap;
			chiSoNhanVat.Menh += duLieuHero.MenhDotPha;
			chiSoNhanVat.Ngoai += duLieuHero.NgoaiDotPha;
			chiSoNhanVat.Noi += duLieuHero.NoiDotPha;
			chiSoNhanVat.ThanPhap += duLieuHero.ThanDotPha;
			chiSoNhanVat.BMS += duLieuHero.BMS;
			chiSoNhanVat.BAS += duLieuHero.BAS;
			chiSoNhanVat.Menh *= 1f + battleGInfo.MenhLienMinhHeSoTangThem;
			chiSoNhanVat.Ngoai *= 1f + battleGInfo.NgoaiLienMinhHeSoTangThem;
			chiSoNhanVat.ThanPhap *= 1f + battleGInfo.ThanLienMinhHeSoTangThem;
			chiSoNhanVat.Noi *= 1f + battleGInfo.KhiLienMinhHeSoTangThem;
			chiSoNhanVat.Menh *= 1f + battleGInfo.MenhHeSoTangThem;
			chiSoNhanVat.Ngoai *= 1f + battleGInfo.NgoaiHeSoTangThem;
			chiSoNhanVat.ThanPhap *= 1f + battleGInfo.ThanHeSoTangThem;
			chiSoNhanVat.Noi *= 1f + battleGInfo.KhiHeSoTangThem;
			chiSoNhanVat.Menh = (float)Math.Round(chiSoNhanVat.Menh);
			chiSoNhanVat.Ngoai = (float)Math.Round(chiSoNhanVat.Ngoai);
			chiSoNhanVat.ThanPhap = (float)Math.Round(chiSoNhanVat.ThanPhap);
			chiSoNhanVat.Noi = (float)Math.Round(chiSoNhanVat.Noi);
			ChiSoNhanVat chisonv6 = duLieuHero.VoCongChiSoTangThem(chiSoNhanVat);
			chiSoNhanVat.IAS *= 1f + chiSoNhanVat.ChiSoBuffTocDanh / 100f;
			chiSoNhanVat.Add(duLieuHero.GetCostumeChiSoTangThem());
			if (battleGInfo.ThanThuData != null)
			{
				ChiSoNhanVat chiSoNhanVat2 = new ChiSoNhanVat();
				if (battleGInfo.ThanThuData.skill == "VC_PET_PHU_QUANG_LUOC_ANH")
				{
					chiSoNhanVat2.Thu = chiSoNhanVat.Thu * (float)(10 + 3 * (int)battleGInfo.ThanThuData.Quality) / 100f;
				}
				else if (battleGInfo.ThanThuData.skill == "VC_PET_CUU_CHUYEN_LY_HON")
				{
					float num = 10f + 2.5f * (float)battleGInfo.ThanThuData.Quality - ((doithu == null) ? 0f : ((doithu.ThanThuData == null) ? 0f : ((!(doithu.ThanThuData.skill != "VC_PET_HU_COT_THUC_TAM")) ? (10f + 2.5f * (float)doithu.ThanThuData.Quality) : 0f)));
					chiSoNhanVat2.Cong = chiSoNhanVat.Cong * num / 100f;
				}
				else if (battleGInfo.ThanThuData.skill == "VC_PET_HUYET_NHIEM_HONG_TRAN")
				{
					float num2 = 10f + 2.5f * (float)battleGInfo.ThanThuData.Quality - ((doithu == null) ? 0f : ((doithu.ThanThuData == null) ? 0f : ((!(doithu.ThanThuData.skill != "VC_PET_THOAI_BO_PHAN_YEN")) ? (10f + 2.5f * (float)doithu.ThanThuData.Quality) : 0f)));
					chiSoNhanVat2.HP = chiSoNhanVat.HP * num2 / 100f;
				}
				else if (battleGInfo.ThanThuData.skill == "VC_PET_PHI_TAN_THIEN_HOA")
				{
					chiSoNhanVat2.MP = chiSoNhanVat.MP * (float)(10 + 3 * (int)battleGInfo.ThanThuData.Quality) / 100f;
				}
				else if (doithu != null && doithu.ThanThuData != null)
				{
					if (doithu.ThanThuData.skill == "VC_PET_HU_COT_THUC_TAM")
					{
						float num3 = 0f - (10f + 2.5f * (float)doithu.ThanThuData.Quality);
						chiSoNhanVat2.Cong = chiSoNhanVat.Cong * num3 / 100f;
					}
					else if (doithu.ThanThuData.skill == "VC_PET_THOAI_BO_PHAN_YEN")
					{
						float num4 = 0f - (10f + 2.5f * (float)doithu.ThanThuData.Quality);
						chiSoNhanVat2.HP = chiSoNhanVat.HP * num4 / 100f;
					}
				}
				chiSoNhanVat.Add(chiSoNhanVat2);
			}
			else if (doithu != null && doithu.ThanThuData != null)
			{
				ChiSoNhanVat chiSoNhanVat3 = new ChiSoNhanVat();
				if (doithu.ThanThuData.skill == "VC_PET_HU_COT_THUC_TAM")
				{
					float num5 = 0f - (10f + 2.5f * (float)doithu.ThanThuData.Quality);
					chiSoNhanVat3.Cong = chiSoNhanVat.Cong * num5 / 100f;
				}
				else if (doithu.ThanThuData.skill == "VC_PET_THOAI_BO_PHAN_YEN")
				{
					float num6 = 0f - (10f + 2.5f * (float)doithu.ThanThuData.Quality);
					chiSoNhanVat3.HP = chiSoNhanVat.HP * num6 / 100f;
				}
				chiSoNhanVat.Add(chiSoNhanVat3);
			}
			chiSoNhanVat.Add(chisonv6);
			if (battleGInfo.ThanThuData != null)
			{
				ChiSoNhanVat chiSoNhanVat4 = new ChiSoNhanVat();
				int tongChiSo = battleGInfo.ThanThuData.GetTongChiSo();
				if (battleGInfo.ThanThuData.petType == UserInfo.PetInfo.PetType.MENH_THAN || battleGInfo.ThanThuData.petType == UserInfo.PetInfo.PetType.NGOAI_THAN)
				{
					chiSoNhanVat4.Thu = (float)(tongChiSo * battleGInfo.ThanThuData.heso2) / 100f;
				}
				if (battleGInfo.ThanThuData.petType == UserInfo.PetInfo.PetType.THAN_KHI)
				{
					chiSoNhanVat4.Thu = (float)(tongChiSo * battleGInfo.ThanThuData.heso1) / 100f;
				}
				if (battleGInfo.ThanThuData.petType == UserInfo.PetInfo.PetType.MENH_THAN || battleGInfo.ThanThuData.petType == UserInfo.PetInfo.PetType.MENH_KHI)
				{
					chiSoNhanVat4.HP = (float)(8 * tongChiSo * battleGInfo.ThanThuData.heso1) / 100f;
				}
				if (battleGInfo.ThanThuData.petType == UserInfo.PetInfo.PetType.NGOAI_MENH)
				{
					chiSoNhanVat4.HP = (float)(8 * tongChiSo * battleGInfo.ThanThuData.heso2) / 100f;
				}
				if (battleGInfo.ThanThuData.petType == UserInfo.PetInfo.PetType.NGOAI_KHI || battleGInfo.ThanThuData.petType == UserInfo.PetInfo.PetType.NGOAI_THAN || battleGInfo.ThanThuData.petType == UserInfo.PetInfo.PetType.NGOAI_MENH)
				{
					chiSoNhanVat4.Cong = (float)(tongChiSo * battleGInfo.ThanThuData.heso1) / 100f;
				}
				if (battleGInfo.ThanThuData.petType == UserInfo.PetInfo.PetType.MENH_KHI || battleGInfo.ThanThuData.petType == UserInfo.PetInfo.PetType.NGOAI_KHI || battleGInfo.ThanThuData.petType == UserInfo.PetInfo.PetType.THAN_KHI)
				{
					chiSoNhanVat4.MP = (float)(8 * tongChiSo * battleGInfo.ThanThuData.heso2) / 100f;
				}
				chiSoNhanVat.Add(chiSoNhanVat4);
			}
			if (duLieuHero != null && duLieuHero.ListTrangBi != null)
			{
				ChiSoNhanVat chisonv7 = duLieuHero.TrangBiDoEffectTangThem();
				chiSoNhanVat.Add(chisonv7);
				foreach (BattleTrangBi item in duLieuHero.ListTrangBi)
				{
					UserInfo.TrangBiData data = item.Data;
					chiSoNhanVat.ChinhXacTrangBiEff = 1f;
					chiSoNhanVat.KhangBaoTrangBiEff = 1f;
					if (data.Effect == null)
					{
						continue;
					}
					if (data.Effect.Exists((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.CHINH_XAC))
					{
						chiSoNhanVat.ChinhXacTrangBiEff *= (100f - data.Effect.Find((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.CHINH_XAC).EffVal) / 100f;
					}
					if (data.Effect.Exists((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.KHANG_BAO))
					{
						chiSoNhanVat.KhangBaoTrangBiEff *= (100f - data.Effect.Find((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.KHANG_BAO).EffVal) / 100f;
					}
					if (data.Effect.Exists((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.STUN))
					{
						chiSoNhanVat.XacSuatChoangVuKhiEff = data.Effect.Find((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.STUN).EffVal / 100f;
					}
					if (data.Effect.Exists((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.PHONG_CHIEU))
					{
						chiSoNhanVat.XacSuatPhongChieuVuKhiEff = data.Effect.Find((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.PHONG_CHIEU).EffVal / 100f;
					}
					if (data.Effect.Exists((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.DINH_THAN))
					{
						chiSoNhanVat.XacSuatDinhThanVuKhiEff = data.Effect.Find((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.DINH_THAN).EffVal / 100f;
					}
					if (data.Effect.Exists((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.BA_THE))
					{
						chiSoNhanVat.XacSuatBaTheAoEff = data.Effect.Find((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.BA_THE).EffVal / 100f;
					}
					if (data.Effect.Exists((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.HO_THE))
					{
						chiSoNhanVat.RateHoTheAoEff = data.Effect.Find((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.HO_THE).EffVal / 100f;
					}
					if (data.Effect.Exists((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.HUT_MAU))
					{
						chiSoNhanVat.TiLeHutMauVuKhiEff = data.Effect.Find((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.HUT_MAU).EffVal / 100f;
					}
					if (data.Effect.Exists((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.HOI_SINH))
					{
						chiSoNhanVat.XacSuatHoiSinhTSEff = data.Effect.Find((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.HOI_SINH).EffVal / 100f;
					}
					if (data.Effect.Exists((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.BURN_MANA))
					{
						chiSoNhanVat.HeSoBurnManaMuEff = data.Effect.Find((UserInfo.TrangBiData.SEffect e) => e.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.BURN_MANA).EffVal;
					}
				}
			}
			TrangBiCfg vKCfg = duLieuHero.GetVKCfg();
			if (vKCfg != null && vKCfg.Anim.Equals("NV_Kiem") && duLieuHero.Name == "NV_KIEM_THANH")
			{
				chiSoNhanVat.Range = ((vKCfg == null) ? duLieuHero.Range : ((double)(vKCfg.Range + 350f))) / 100.0;
			}
			else
			{
				chiSoNhanVat.Range = ((vKCfg == null) ? duLieuHero.Range : ((double)vKCfg.Range)) / 100.0;
			}
		}
		return chiSoNhanVat;
	}
}

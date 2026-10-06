using System.Collections.Generic;

public class PhanThuongResponse : ExDataBase
{
	public enum LoaiPhanThuong
	{
		BAC = 0,
		VANG = 1,
		TRANG_BI = 2,
		VO_CONG = 3,
		VAT_PHAM_TIEU_THU = 4,
		HON_NHAN_VAT = 5,
		CAO_NHAN = 6,
		BAN_DO = 7,
		BANG_HUU = 8,
		THUONG_NHAN = 9,
		TY_THI = 10,
		MANH_VO_CONG = 11,
		MANH_TRANG_BI = 12,
		LOAI_PHAN_THUONG_COUNT = 13,
		NGUYEN_KHI = 14,
		THU_CUOI = 15,
		THAN_THU = 16,
		HUYEN_KHI = 17
	}

	public class PhanThuong
	{
		private string name = string.Empty;

		private int count = 1;

		private int level = 1;

		private LoaiPhanThuong loai;

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public int Count
		{
			get
			{
				return count;
			}
			set
			{
				count = value;
			}
		}

		public int Level
		{
			get
			{
				return level;
			}
			set
			{
				level = value;
			}
		}

		public int ID { get; set; }

		public LoaiPhanThuong Loai
		{
			get
			{
				return loai;
			}
			set
			{
				loai = value;
			}
		}

		public int TyLe { get; set; }

		public PhanThuong()
		{
		}

		public PhanThuong(PhanThuong other)
		{
			Name = other.Name;
			Count = other.Count;
			ID = other.ID;
			Level = other.Level;
			Loai = other.Loai;
			TyLe = other.TyLe;
		}
	}

	public List<PhanThuong> PhanThuongList = new List<PhanThuong>();

	public UserInfo UpdateUserInfo = new UserInfo();

	public string PhanThuongTitle { get; set; }

	public string PhanThuongDesc { get; set; }

	public static string GetPhanThuongCodeName(string codeName)
	{
		if (codeName.StartsWith("TH_"))
		{
			return "NV_" + codeName.Substring(3);
		}
		if (codeName.StartsWith("MVC_"))
		{
			return codeName.Substring(1);
		}
		if (codeName.StartsWith("MVK_") || codeName.StartsWith("MAG_") || codeName.StartsWith("MTS_") || codeName.StartsWith("MMU_"))
		{
			return codeName.Substring(1);
		}
		return codeName;
	}

	public static string GetTenHienThiPhanThuong(PhanThuong pt)
	{
		switch (pt.Loai)
		{
		case LoaiPhanThuong.BAC:
			return string.Format("{0} {1}", pt.Count, Localization.instance.Get("Bac"));
		case LoaiPhanThuong.VANG:
			return string.Format("{0} {1}", pt.Count, Localization.instance.Get("VangFull"));
		case LoaiPhanThuong.VAT_PHAM_TIEU_THU:
		{
			VatPhamTieuThuCfg value8 = null;
			if (ConfigManager.instance.m_dicVatPhamTieuThu.TryGetValue(pt.Name, out value8))
			{
				return string.Format("{0} {1}", pt.Count, value8.TenHienThi);
			}
			break;
		}
		case LoaiPhanThuong.TRANG_BI:
		{
			TrangBiCfg value5 = null;
			if (ConfigManager.instance.m_dicTrangBi.TryGetValue(pt.Name, out value5))
			{
				return string.Format("{0} {1} {3} {2}", pt.Count, value5.TenHienThi, pt.Level, Localization.instance.Get("CapLevelLabel"));
			}
			break;
		}
		case LoaiPhanThuong.VO_CONG:
		{
			CfgVoCong value2 = null;
			if (ConfigManager.instance.m_dicVCs.TryGetValue(pt.Name, out value2))
			{
				return string.Format("{0} {1} {3} {2}", pt.Count, value2.TenHienThi, pt.Level, Localization.instance.Get("CapLevelLabel"));
			}
			break;
		}
		case LoaiPhanThuong.MANH_TRANG_BI:
		{
			TrangBiCfg value7 = null;
			if (ConfigManager.instance.m_dicTrangBi.TryGetValue(pt.Name, out value7))
			{
				return string.Format("{0} {2} {1}", pt.Count, value7.TenHienThi, Localization.instance.Get("MailManhLabel"));
			}
			break;
		}
		case LoaiPhanThuong.MANH_VO_CONG:
		{
			CfgVoCong value6 = null;
			if (ConfigManager.instance.m_dicVCs.TryGetValue(pt.Name, out value6))
			{
				return string.Format("{0} {2} {1}", pt.Count, value6.TenHienThi, Localization.instance.Get("MailManhLabel"));
			}
			break;
		}
		case LoaiPhanThuong.HON_NHAN_VAT:
		{
			NhanVatCfg value4 = null;
			if (ConfigManager.instance.m_dicNhanVats.TryGetValue(pt.Name, out value4))
			{
				return string.Format("{0} {2} {1}", pt.Count, value4.TenHienThi, Localization.instance.Get("MailTanHonLabel"));
			}
			break;
		}
		case LoaiPhanThuong.NGUYEN_KHI:
		{
			OtherCfg.NguyenKhiCfg value3 = null;
			if (ConfigManager.instance.OtherConfig.NguyenKhiConfig.TryGetValue(pt.Name, out value3))
			{
				return string.Format("{0} {2} {1}", pt.Count, value3.Codename, Localization.instance.Get("MailNguyenKhiLabel"));
			}
			break;
		}
		case LoaiPhanThuong.THU_CUOI:
		{
			OtherCfg.ThuCuoiCfg value = null;
			if (ConfigManager.instance.OtherConfig.ThuCuoiConfig.TryGetValue(pt.Name, out value))
			{
				return string.Format("{0} {2} {1}", pt.Count, value.CodeName, Localization.instance.Get("MailThuCuoiLabel"));
			}
			break;
		}
		case LoaiPhanThuong.THAN_THU:
			return string.Format("{0} {1}", pt.Count, pt.Name);
		}
		return string.Empty;
	}

	public static LoaiPhanThuong GetLoaiPhanThuongFromCode(string codeName)
	{
		string text = codeName.ToUpper();
		switch (text)
		{
		case "VANG":
			return LoaiPhanThuong.VANG;
		case "BAC":
			return LoaiPhanThuong.BAC;
		case "CAO_NHAN":
			return LoaiPhanThuong.CAO_NHAN;
		case "BAN_DO":
			return LoaiPhanThuong.BAN_DO;
		case "BANG_HUU":
			return LoaiPhanThuong.BANG_HUU;
		case "THUONG_NHAN":
			return LoaiPhanThuong.THUONG_NHAN;
		case "TY_THI":
			return LoaiPhanThuong.TY_THI;
		default:
			if (text.StartsWith("TS_") || text.StartsWith("VK_") || text.StartsWith("MU_") || text.StartsWith("AG_"))
			{
				return LoaiPhanThuong.TRANG_BI;
			}
			if (text.StartsWith("MTS_") || text.StartsWith("MVK_") || text.StartsWith("MMU_") || text.StartsWith("MAG_"))
			{
				return LoaiPhanThuong.MANH_TRANG_BI;
			}
			if (text.StartsWith("VC_"))
			{
				return LoaiPhanThuong.VO_CONG;
			}
			if (text.StartsWith("MVC_"))
			{
				return LoaiPhanThuong.MANH_VO_CONG;
			}
			if (text.StartsWith("VP_"))
			{
				return LoaiPhanThuong.VAT_PHAM_TIEU_THU;
			}
			if (text.StartsWith("TH_"))
			{
				return LoaiPhanThuong.HON_NHAN_VAT;
			}
			if (text.StartsWith("NK_"))
			{
				return LoaiPhanThuong.NGUYEN_KHI;
			}
			if (text.StartsWith("NGUA_"))
			{
				return LoaiPhanThuong.THU_CUOI;
			}
			if (text.StartsWith("PET_"))
			{
				return LoaiPhanThuong.THAN_THU;
			}
			return LoaiPhanThuong.LOAI_PHAN_THUONG_COUNT;
		}
	}
}

using System.Collections.Generic;

public class DanhSonCfg
{
	public class PhanThuong
	{
		public string CodeName { get; set; }

		public int Level { get; set; }

		public int Count { get; set; }
	}

	private float _hesoSucManh1 = 1f;

	private float _hesoSucManh2 = 1f;

	private float _hesoSucManh3 = 1f;

	public string TenHienThi { get; set; }

	public string MoTa { get; set; }

	public PhanThuong PhanThuong1 { get; set; }

	public PhanThuong PhanThuong2 { get; set; }

	public PhanThuong PhanThuong3 { get; set; }

	public PhanThuong PhanThuong4 { get; set; }

	public PhanThuong PhanThuong5 { get; set; }

	public PhanThuong PhanThuong6 { get; set; }

	public PhanThuong PhanThuong7 { get; set; }

	public PhanThuong PhanThuong8 { get; set; }

	public PhanThuong PhanThuong9 { get; set; }

	public float HeSoSucManh1
	{
		get
		{
			return _hesoSucManh1;
		}
		set
		{
			_hesoSucManh1 = value;
		}
	}

	public float HeSoSucManh2
	{
		get
		{
			return _hesoSucManh2;
		}
		set
		{
			_hesoSucManh2 = value;
		}
	}

	public float HeSoSucManh3
	{
		get
		{
			return _hesoSucManh3;
		}
		set
		{
			_hesoSucManh3 = value;
		}
	}

	public List<GiangHoCfg.NhiemVu.NhanVatGH> DoiHinh1 { get; set; }

	public List<GiangHoCfg.NhiemVu.NhanVatGH> DoiHinh2 { get; set; }

	public List<GiangHoCfg.NhiemVu.NhanVatGH> DoiHinh3 { get; set; }

	public static int GetLuotMoThuong(int luotMoThuongMask)
	{
		int num = 0;
		while (luotMoThuongMask != 0)
		{
			if ((luotMoThuongMask & 1) > 0)
			{
				num++;
			}
			luotMoThuongMask >>= 1;
		}
		return num;
	}

	public static bool IsMoThuong(int luotMoThuongMask, int phanThuongIndex)
	{
		int num = 1 << phanThuongIndex;
		return (luotMoThuongMask & num) != 0;
	}

	public static int GetMoThuong(int luotMoThuongMask, int phanThuongIndex)
	{
		return luotMoThuongMask | (1 << phanThuongIndex);
	}

	public bool IsValid(out string message)
	{
		foreach (GiangHoCfg.NhiemVu.NhanVatGH item in DoiHinh1)
		{
			if (!item.IsValid(out message))
			{
				message = string.Format("DanhSonConfig: Nhập lỗi đội hình 1 danh sơn {1} :\n\t- {0}", message, TenHienThi);
				return false;
			}
		}
		foreach (GiangHoCfg.NhiemVu.NhanVatGH item2 in DoiHinh2)
		{
			if (!item2.IsValid(out message))
			{
				message = string.Format("DanhSonConfig: Nhập lỗi đội hình 2 danh sơn {1} :\n\t- {0}", message, TenHienThi);
				return false;
			}
		}
		foreach (GiangHoCfg.NhiemVu.NhanVatGH item3 in DoiHinh3)
		{
			if (!item3.IsValid(out message))
			{
				message = string.Format("DanhSonConfig: Nhập lỗi đội hình 3 danh sơn {1} :\n\t- {0}", message, TenHienThi);
				return false;
			}
		}
		if (!CheckValidPhanThuong(PhanThuong1, out message))
		{
			return false;
		}
		if (!CheckValidPhanThuong(PhanThuong2, out message))
		{
			return false;
		}
		if (!CheckValidPhanThuong(PhanThuong3, out message))
		{
			return false;
		}
		if (!CheckValidPhanThuong(PhanThuong4, out message))
		{
			return false;
		}
		if (!CheckValidPhanThuong(PhanThuong5, out message))
		{
			return false;
		}
		if (!CheckValidPhanThuong(PhanThuong6, out message))
		{
			return false;
		}
		if (!CheckValidPhanThuong(PhanThuong7, out message))
		{
			return false;
		}
		if (!CheckValidPhanThuong(PhanThuong8, out message))
		{
			return false;
		}
		if (!CheckValidPhanThuong(PhanThuong9, out message))
		{
			return false;
		}
		message = "OK";
		return true;
	}

	private bool CheckValidPhanThuong(PhanThuong pt, out string message)
	{
		if (pt.Count <= 0 || pt.Level < 0)
		{
			message = string.Format("DanhSonConfig: Nhập lỗi phần thưởng danh sơn {1} : {0}", pt.CodeName, TenHienThi);
			return false;
		}
		string phanThuongCodeName = PhanThuongResponse.GetPhanThuongCodeName(pt.CodeName);
		switch (PhanThuongResponse.GetLoaiPhanThuongFromCode(pt.CodeName))
		{
		case PhanThuongResponse.LoaiPhanThuong.TRANG_BI:
		case PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI:
			if (!ConfigManager.instance.m_dicTrangBi.ContainsKey(phanThuongCodeName))
			{
				message = string.Format("DanhSonConfig: Nhập lỗi phần thưởng danh sơn {1} : {0}", pt.CodeName, TenHienThi);
				return false;
			}
			break;
		case PhanThuongResponse.LoaiPhanThuong.VO_CONG:
		case PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG:
			if (!ConfigManager.instance.m_dicVCs.ContainsKey(phanThuongCodeName))
			{
				message = string.Format("DanhSonConfig: Nhập lỗi phần thưởng danh sơn {1} : {0}", pt.CodeName, TenHienThi);
				return false;
			}
			break;
		case PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT:
			if (!ConfigManager.instance.m_dicNhanVats.ContainsKey(phanThuongCodeName))
			{
				message = string.Format("DanhSonConfig: Nhập lỗi phần thưởng danh sơn {1} : {0}", pt.CodeName, TenHienThi);
				return false;
			}
			break;
		case PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU:
			if (!ConfigManager.instance.m_dicVatPhamTieuThu.ContainsKey(phanThuongCodeName))
			{
				message = string.Format("DanhSonConfig: Nhập lỗi phần thưởng danh sơn {1} : {0}", pt.CodeName, TenHienThi);
				return false;
			}
			break;
		}
		message = string.Empty;
		return true;
	}
}

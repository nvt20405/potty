using System;
using System.Collections.Generic;

public class GoiVatPhamCfg
{
	private string codeName;

	private string iconName;

	private string tenHienThi;

	private string moTa;

	private int tongGiaTri;

	private int giaMua;

	private DateTime thoiGianTonTai = new DateTime(2020, 1, 1);

	public List<PhanThuongResponse.PhanThuong> DanhSachItem = new List<PhanThuongResponse.PhanThuong>();

	public string CodeName
	{
		get
		{
			return codeName;
		}
		set
		{
			codeName = value;
		}
	}

	public string IconName
	{
		get
		{
			return iconName;
		}
		set
		{
			iconName = value;
		}
	}

	public string TenHienThi
	{
		get
		{
			return tenHienThi;
		}
		set
		{
			tenHienThi = value;
		}
	}

	public string MoTa
	{
		get
		{
			return moTa;
		}
		set
		{
			moTa = value;
		}
	}

	public int TongGiaTri
	{
		get
		{
			return tongGiaTri;
		}
		set
		{
			tongGiaTri = value;
		}
	}

	public int GiaMua
	{
		get
		{
			return giaMua;
		}
		set
		{
			giaMua = value;
		}
	}

	public DateTime ThoiGianTonTai
	{
		get
		{
			return thoiGianTonTai;
		}
		set
		{
			thoiGianTonTai = value;
		}
	}
}

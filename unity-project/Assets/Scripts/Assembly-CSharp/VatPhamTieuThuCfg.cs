using System;

public class VatPhamTieuThuCfg
{
	private string tenHienThi = string.Empty;

	private string moTa = string.Empty;

	private int giaVang;

	private bool banTrongShop;

	private LoaiVatPhamTieuThu loai;

	private int heSoRandom = 100;

	public string Name { get; set; }

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

	public int GiaVang
	{
		get
		{
			return giaVang;
		}
		set
		{
			giaVang = value;
		}
	}

	public bool BanTrongShop
	{
		get
		{
			return banTrongShop;
		}
		set
		{
			banTrongShop = value;
		}
	}

	public LoaiVatPhamTieuThu Loai
	{
		get
		{
			return loai;
		}
	}

	public string LoaiStr
	{
		set
		{
			try
			{
				loai = (LoaiVatPhamTieuThu)(int)Enum.Parse(typeof(LoaiVatPhamTieuThu), value, true);
			}
			catch (Exception ex)
			{
				EGDebug.LogWarning("VatPhamTieuThu : Error parse LoaiVatPhamTieuThu : " + value + " " + ex.ToString());
			}
		}
	}

	public int HeSoRandom
	{
		get
		{
			return heSoRandom;
		}
		set
		{
			heSoRandom = value;
		}
	}

	public bool IsValid()
	{
		if (tenHienThi.Length == 0)
		{
			return false;
		}
		if (banTrongShop && giaVang <= 0)
		{
			return false;
		}
		return true;
	}
}

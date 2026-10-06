public class CuaHangThanBiCfg
{
	private string codeName = string.Empty;

	private string tenHienThi = string.Empty;

	private string moTa = string.Empty;

	private int level = 1;

	private int count;

	private PhanThuongResponse.LoaiPhanThuong loai;

	private int diemNeed;

	private bool banTrongShop;

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

	public int DiemCan
	{
		get
		{
			return diemNeed;
		}
		set
		{
			diemNeed = value;
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

	public PhanThuongResponse.LoaiPhanThuong Loai
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

	public bool IsValid()
	{
		if (tenHienThi.Length == 0)
		{
			return false;
		}
		if (banTrongShop && diemNeed <= 0)
		{
			return false;
		}
		return true;
	}
}

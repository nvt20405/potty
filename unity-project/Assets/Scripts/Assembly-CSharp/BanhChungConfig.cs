using System;
using System.Collections.Generic;

public class BanhChungConfig
{
	public List<PhanThuongResponse.PhanThuong> Pt1 = new List<PhanThuongResponse.PhanThuong>();

	public List<PhanThuongResponse.PhanThuong> Pt2 = new List<PhanThuongResponse.PhanThuong>();

	public List<PhanThuongResponse.PhanThuong> Pt3 = new List<PhanThuongResponse.PhanThuong>();

	public DateTime NgayBatDau { get; set; }

	public DateTime NgayKetThuc { get; set; }

	public int SoNgLCan { get; set; }

	public int NgLMin { get; set; }

	public int NgLMax { get; set; }

	public int MaxLuotNgL { get; set; }

	public int Lvl1 { get; set; }

	public int Lvl2 { get; set; }

	public int Lvl3 { get; set; }

	public BanhChungConfig()
	{
		Initialize();
	}

	public BanhChungConfig(DateTime ngayBatDau, DateTime ngayKetThuc)
	{
		Initialize();
		NgayBatDau = ngayBatDau;
		NgayKetThuc = ngayKetThuc;
	}

	private void Initialize()
	{
		SoNgLCan = 5;
		NgLMin = 5;
		NgLMax = 10;
		MaxLuotNgL = 10;
	}

	public int GetLevelNoiBanhFromSoBanh(int sobanh)
	{
		if (sobanh < Lvl1)
		{
			return 0;
		}
		if (sobanh < Lvl2)
		{
			return 1;
		}
		if (sobanh < Lvl3)
		{
			return 2;
		}
		return 3;
	}

	public float GetExpNoiBanhFromSoBanh(int sobanh)
	{
		if (sobanh < Lvl1)
		{
			return (float)sobanh / (float)Lvl1;
		}
		if (sobanh < Lvl2)
		{
			return (float)sobanh / (float)Lvl2;
		}
		if (sobanh < Lvl3)
		{
			return (float)sobanh / (float)Lvl3;
		}
		return 1f;
	}
}

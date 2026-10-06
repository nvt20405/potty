using System;
using System.Collections.Generic;
using LitJson;

public class LienMinhData
{
	public enum CHUCVU
	{
		MINH_CHU = 0,
		PHO_MINH_CHU = 1,
		TINH_ANH = 2,
		THANH_VIEN = 3
	}

	public int NienThuItem;

	public List<LienMinhThanhVienData> ThanhVienList;

	public List<LienMinhXinGiaNhapData> XinGiaNhapList;

	public int ID { get; set; }

	public string DisplayName { get; set; }

	public double DiemCongHien { get; set; }

	public int TuNghiaLevel { get; set; }

	public int ThienHaLauLevel { get; set; }

	public int TangKiemCacLevel { get; set; }

	public DateTime DotLuaTraiTime { get; set; }

	public int DotLuaTraiCount { get; set; }

	public string ThongBao { get; set; }

	public int MinhChuID { get; set; }

	public int PhoMinhChuID { get; set; }

	public LienMinhData(string displayName, int minhchuId)
	{
		ID = 0;
		DisplayName = displayName;
		DiemCongHien = 0.0;
		TuNghiaLevel = 0;
		ThienHaLauLevel = 0;
		TangKiemCacLevel = 0;
		DotLuaTraiTime = new DateTime(2000, 1, 1);
		DotLuaTraiCount = 0;
		ThongBao = string.Empty;
		MinhChuID = minhchuId;
		PhoMinhChuID = 0;
		ThanhVienList = new List<LienMinhThanhVienData>();
	}

	public LienMinhData()
	{
		ID = 0;
		DisplayName = string.Empty;
		DiemCongHien = 0.0;
		TuNghiaLevel = 0;
		ThienHaLauLevel = 0;
		TangKiemCacLevel = 0;
		DotLuaTraiTime = new DateTime(2000, 1, 1);
		DotLuaTraiCount = 0;
		ThongBao = string.Empty;
		MinhChuID = 0;
		PhoMinhChuID = 0;
		ThanhVienList = new List<LienMinhThanhVienData>();
	}

	public bool IsThanhVienTinhAnh(int gid)
	{
		int num = ThanhVienList.FindIndex((LienMinhThanhVienData e) => e.ID == gid);
		if (num < 0)
		{
			return false;
		}
		if (PhoMinhChuID > 0)
		{
			return num < 10;
		}
		return num < 9;
	}

	public static List<int> GetLienMinhBoostChiSo(LienMinhData lienminh)
	{
		List<int> list = new List<int>();
		list.Add(0);
		list.Add(0);
		list.Add(0);
		list.Add(0);
		if (lienminh == null)
		{
			return list;
		}
		string json = ConfigManager.instance.CongTrinhLienMinhConfig["ThienHaLau"].Value[lienminh.ThienHaLauLevel];
		return JsonMapper.ToObject<List<int>>(json);
	}
}

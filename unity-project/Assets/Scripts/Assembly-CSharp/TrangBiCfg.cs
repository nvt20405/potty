using System;
using System.Collections.Generic;

public class TrangBiCfg
{
	private int heSoRandom = 100;

	public List<string> TinhLuyen = new List<string>();

	public string Name { get; set; }

	public string TenHienThi { get; set; }

	public ItemClass Hang { get; set; }

	public float CoSo { get; set; }

	public float TangTruong { get; set; }

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

	public string MoTa { get; set; }

	public string Anim { get; set; }

	public float Range { get; set; }

	public float ChinhXac { get; set; }

	public AnimVuKhi GetAnimVK()
	{
		AnimVuKhi animVuKhi = AnimVuKhi.NV_BaoTay;
		try
		{
			return (AnimVuKhi)(int)Enum.Parse(typeof(AnimVuKhi), Anim);
		}
		catch
		{
			return AnimVuKhi.NV_BaoTay;
		}
	}

	public bool IsValid(out string message)
	{
		if (Hang < ItemClass.Binh || Hang > ItemClass.Giap)
		{
			message = string.Format("Hạng trang bị hợp lệ phải từ {0} đến {1}", 3, 1);
			return false;
		}
		if (CoSo < 0f || TangTruong < 0f || HeSoRandom < 0)
		{
			message = "Thông số trang bị phải lớn hơn hoặc bằng 0";
			return false;
		}
		if (!string.IsNullOrEmpty(Anim))
		{
			try
			{
				Enum.Parse(typeof(AnimVuKhi), Anim);
			}
			catch (Exception)
			{
				message = "anim vũ khí không hợp lệ";
				return false;
			}
		}
		if (Hang == ItemClass.Binh)
		{
			if (TinhLuyen.Count < 3)
			{
				message = "Tinh luyện chưa nhập đủ";
				return false;
			}
		}
		else if (TinhLuyen.Count < 4)
		{
			message = "Tinh luyện chưa nhập đủ";
			return false;
		}
		foreach (string item in TinhLuyen)
		{
			if (!ConfigManager.instance.m_dicTrangBi.ContainsKey(item))
			{
				message = "Tinh luyện mảnh " + item + " k tồn tại";
				return false;
			}
		}
		message = "OK";
		return true;
	}

	public static LoaiTrangBi GetLoaiTrangBi(string codeName)
	{
		if (codeName.StartsWith("VK_"))
		{
			return LoaiTrangBi.VuKhi;
		}
		if (codeName.StartsWith("MU_"))
		{
			return LoaiTrangBi.Mu;
		}
		if (codeName.StartsWith("AG_"))
		{
			return LoaiTrangBi.AoGiap;
		}
		if (codeName.StartsWith("TS_"))
		{
			return LoaiTrangBi.TrangSuc;
		}
		return LoaiTrangBi.None;
	}

	public List<int> GetChiSoBoiDuong(UserInfo.TrangBiData tb_data)
	{
		List<int> list = new List<int>();
		list.Add(0);
		list.Add(0);
		list.Add(0);
		list.Add(0);
		float num = CoSo + TangTruong * (float)(tb_data.Level - 1);
		if (tb_data.HoangKim > 0)
		{
			for (int i = 0; i < tb_data.HoangKim; i++)
			{
				num += CoSo + TangTruong * (float)(tb_data.Level - 1);
			}
		}
		if (tb_data.Name.StartsWith("VK_"))
		{
			list[0] = tb_data.MenhBoiDuong;
			list[1] = (int)num + tb_data.NgoaiBoiDuong;
			list[2] = tb_data.ThanBoiDuong;
			list[3] = tb_data.KhiBoiDuong;
		}
		else if (tb_data.Name.StartsWith("MU_"))
		{
			list[0] = tb_data.MenhBoiDuong;
			list[1] = tb_data.NgoaiBoiDuong;
			list[2] = tb_data.ThanBoiDuong;
			list[3] = (int)num + tb_data.KhiBoiDuong;
		}
		else if (tb_data.Name.StartsWith("AG_"))
		{
			list[0] = tb_data.MenhBoiDuong;
			list[1] = tb_data.NgoaiBoiDuong;
			list[2] = (int)num + tb_data.ThanBoiDuong;
			list[3] = tb_data.KhiBoiDuong;
		}
		else if (tb_data.Name.StartsWith("TS_"))
		{
			list[0] = (int)num + tb_data.MenhBoiDuong;
			list[1] = tb_data.NgoaiBoiDuong;
			list[2] = tb_data.ThanBoiDuong;
			list[3] = tb_data.KhiBoiDuong;
		}
		return list;
	}

	public List<int> GetFullChiSo(UserInfo.TrangBiData tb_data)
	{
		List<int> chiSoBoiDuong = GetChiSoBoiDuong(tb_data);
		List<int> list = chiSoBoiDuong;
		float num = ConfigManager.instance.GetBonusFromTinhLuyenTrangBi(tb_data.TinhLuyenLevel);
		UserInfo.TrangBiData.LoaiBuff buff;
		float ChiSo;
		UserInfo.TrangBiData.GetLoaiBuff(tb_data, 0, out buff, out ChiSo);
		UserInfo.TrangBiData.LoaiBuff buff2;
		float ChiSo2;
		UserInfo.TrangBiData.GetLoaiBuff(tb_data, 1, out buff2, out ChiSo2);
		UserInfo.TrangBiData.LoaiBuff buff3;
		float ChiSo3;
		UserInfo.TrangBiData.GetLoaiBuff(tb_data, 2, out buff3, out ChiSo3);
		if (buff == UserInfo.TrangBiData.LoaiBuff.TANG_CHI_SO)
		{
			num += ChiSo;
		}
		if (buff2 == UserInfo.TrangBiData.LoaiBuff.TANG_CHI_SO)
		{
			num += ChiSo2;
		}
		if (buff3 == UserInfo.TrangBiData.LoaiBuff.TANG_CHI_SO)
		{
			num += ChiSo3;
		}
		for (int i = 0; i < list.Count; i++)
		{
			list[i] = (int)Math.Round((float)list[i] * num / 100f);
		}
		return list;
	}

	public List<int> GetChiSo(UserInfo.TrangBiData tb_data)
	{
		float num = 0f;
		List<int> chiSoBoiDuong = GetChiSoBoiDuong(tb_data);
		List<ChiSoCoBan> list = new List<ChiSoCoBan>();
		list.Add(ChiSoCoBan.Menh);
		list.Add(ChiSoCoBan.Ngoai);
		list.Add(ChiSoCoBan.ThanPhap);
		list.Add(ChiSoCoBan.Noi);
		Dictionary<ChiSoCoBan, int> dictionary = new Dictionary<ChiSoCoBan, int>();
		dictionary.Add(ChiSoCoBan.Menh, chiSoBoiDuong[0]);
		dictionary.Add(ChiSoCoBan.Ngoai, chiSoBoiDuong[1]);
		dictionary.Add(ChiSoCoBan.ThanPhap, chiSoBoiDuong[2]);
		dictionary.Add(ChiSoCoBan.Noi, chiSoBoiDuong[3]);
		for (int i = 0; i < chiSoBoiDuong.Count; i++)
		{
			for (int j = i; j < chiSoBoiDuong.Count; j++)
			{
				if (chiSoBoiDuong[i] < chiSoBoiDuong[j])
				{
					int value = chiSoBoiDuong[j];
					chiSoBoiDuong[j] = chiSoBoiDuong[i];
					chiSoBoiDuong[i] = value;
					ChiSoCoBan value2 = list[j];
					list[j] = list[i];
					list[i] = value2;
				}
			}
		}
		List<int> list2 = new List<int>();
		list2.Add(0);
		list2.Add(0);
		list2.Add(0);
		list2.Add(0);
		list2[(int)list[0]] = chiSoBoiDuong[0];
		list2[(int)list[1]] = chiSoBoiDuong[1];
		float num2 = ConfigManager.instance.GetBonusFromTinhLuyenTrangBi(tb_data.TinhLuyenLevel);
		UserInfo.TrangBiData.LoaiBuff buff;
		float ChiSo;
		UserInfo.TrangBiData.GetLoaiBuff(tb_data, 0, out buff, out ChiSo);
		UserInfo.TrangBiData.LoaiBuff buff2;
		float ChiSo2;
		UserInfo.TrangBiData.GetLoaiBuff(tb_data, 1, out buff2, out ChiSo2);
		UserInfo.TrangBiData.LoaiBuff buff3;
		float ChiSo3;
		UserInfo.TrangBiData.GetLoaiBuff(tb_data, 2, out buff3, out ChiSo3);
		if (buff == UserInfo.TrangBiData.LoaiBuff.TANG_CHI_SO)
		{
			num2 += ChiSo;
		}
		if (buff2 == UserInfo.TrangBiData.LoaiBuff.TANG_CHI_SO)
		{
			num2 += ChiSo2;
		}
		if (buff3 == UserInfo.TrangBiData.LoaiBuff.TANG_CHI_SO)
		{
			num2 += ChiSo3;
		}
		for (int k = 0; k < list2.Count; k++)
		{
			list2[k] = (int)Math.Round((float)list2[k] * num2 / 100f);
		}
		return list2;
	}

	public static bool IsValidCodeName(string codeName)
	{
		return GetLoaiTrangBi(codeName) != LoaiTrangBi.None;
	}
}

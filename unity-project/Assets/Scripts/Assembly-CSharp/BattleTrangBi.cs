using System.Collections.Generic;

public class BattleTrangBi
{
	public UserInfo.TrangBiData.LoaiBuff LoaiBuff1;

	public float ChiSoBuff1;

	public UserInfo.TrangBiData.LoaiBuff LoaiBuff2;

	public float ChiSoBuff2;

	public UserInfo.TrangBiData.LoaiBuff LoaiBuff3;

	public float ChiSoBuff3;

	public TrangBiCfg Config { get; set; }

	public UserInfo.TrangBiData Data { get; set; }

	public LoaiTrangBi Class { get; set; }

	public BattleTrangBi()
	{
		Data = new UserInfo.TrangBiData();
	}

	public BattleTrangBi(UserInfo.TrangBiData _data)
	{
		Data = _data;
		Class = TrangBiCfg.GetLoaiTrangBi(Data.Name);
		Config = GetConfig(Data.Name);
		UserInfo.TrangBiData.GetLoaiBuff(_data, 0, out LoaiBuff1, out ChiSoBuff1);
		UserInfo.TrangBiData.GetLoaiBuff(_data, 1, out LoaiBuff2, out ChiSoBuff2);
		UserInfo.TrangBiData.GetLoaiBuff(_data, 2, out LoaiBuff3, out ChiSoBuff3);
	}

	public List<int> ChiSoTrangBi(bool isBatQuai)
	{
		List<int> list = new List<int>();
		list.Add(0);
		list.Add(0);
		list.Add(0);
		list.Add(0);
		if (Data.Level < 1)
		{
			return list;
		}
		if (!isBatQuai)
		{
			return Config.GetChiSo(Data);
		}
		return Config.GetFullChiSo(Data);
	}

	private static TrangBiCfg GetConfig(string name)
	{
		TrangBiCfg value = null;
		if (ConfigManager.instance.m_dicTrangBi.TryGetValue(name, out value))
		{
			return value;
		}
		return null;
	}

	public ChiSoNhanVat ChiSoEffect()
	{
		ChiSoNhanVat chiSoNhanVat = new ChiSoNhanVat();
		if (Data.Effect != null && Data.Effect.Count > 0)
		{
			foreach (UserInfo.TrangBiData.SEffect item in Data.Effect)
			{
				if (item.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.TANG_CONG)
				{
					chiSoNhanVat.Cong += item.EffVal * (float)Data.Level;
				}
				if (item.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.TANG_MAU)
				{
					chiSoNhanVat.HP += item.EffVal * (float)Data.Level;
				}
				if (item.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.TANG_NOI)
				{
					chiSoNhanVat.MP += item.EffVal * (float)Data.Level;
				}
				if (item.LoaiEff == UserInfo.TrangBiData.TRANG_BI_EFF.TANG_THU)
				{
					chiSoNhanVat.Thu += item.EffVal * (float)Data.Level;
				}
			}
		}
		return chiSoNhanVat;
	}

	public ChiSoNhanVat ChiSoTangThem(bool isBatQuai)
	{
		ChiSoNhanVat chiSoNhanVat = new ChiSoNhanVat();
		chiSoNhanVat.Menh = ChiSoTrangBi(isBatQuai)[0];
		chiSoNhanVat.Ngoai = ChiSoTrangBi(isBatQuai)[1];
		chiSoNhanVat.ThanPhap = ChiSoTrangBi(isBatQuai)[2];
		chiSoNhanVat.Noi = ChiSoTrangBi(isBatQuai)[3];
		UserInfo.TrangBiData.LoaiBuff[] array = new UserInfo.TrangBiData.LoaiBuff[3] { LoaiBuff1, LoaiBuff2, LoaiBuff3 };
		float[] array2 = new float[3] { ChiSoBuff1, ChiSoBuff2, ChiSoBuff3 };
		for (int i = 0; i < array.Length; i++)
		{
			switch (array[i])
			{
			case UserInfo.TrangBiData.LoaiBuff.TOC_DANH:
				chiSoNhanVat.ChiSoBuffTocDanh = array2[i];
				break;
			case UserInfo.TrangBiData.LoaiBuff.BAO_KICH:
				chiSoNhanVat.ChiSoBuffBaoKich = array2[i];
				break;
			case UserInfo.TrangBiData.LoaiBuff.HOA_GIAI:
				chiSoNhanVat.ChiSoBuffHoaGiai = array2[i];
				break;
			case UserInfo.TrangBiData.LoaiBuff.KHANG_CHUONG:
				chiSoNhanVat.ChiSoBuffKhangChuong = array2[i];
				break;
			}
		}
		return chiSoNhanVat;
	}
}

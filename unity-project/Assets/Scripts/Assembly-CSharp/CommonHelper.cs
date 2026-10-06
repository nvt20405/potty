using System;
using System.Collections.Generic;

public class CommonHelper
{
	public static int TotalHeSoChieuThucLevel1 = 0;

	public static int TotalHeSoChieuThucLevel2 = 0;

	public static int TotalHeSoChieuThucLevel3 = 0;

	public static List<int> HeSoList = new List<int>();

	public static List<string> VoCongList = new List<string>();

	public static float GetValueVoCongSlot(string vocong, int tichluy)
	{
		if (ConfigManager.instance.m_dicVCs[vocong].CongKichType == VCCongKich.DA_THE)
		{
			return 5f + (float)tichluy * 0.15f;
		}
		if (ConfigManager.instance.m_dicVCs[vocong].CongKichType == VCCongKich.DON_THE)
		{
			return 10f + (float)tichluy * 0.3f;
		}
		return 0f;
	}

	public static string GetRandomVoCongSlot(UserInfo.ThienMaLenhInfo thienmalenh, VCClass slot)
	{
		VoCongList = new List<string>();
		VoCongList.Add(string.Empty);
		HeSoList = new List<int>();
		HeSoList.Add(0);
		TotalHeSoChieuThucLevel1 = 0;
		TotalHeSoChieuThucLevel2 = 0;
		TotalHeSoChieuThucLevel3 = 0;
		foreach (KeyValuePair<string, CfgVoCong> dicVC in ConfigManager.instance.m_dicVCs)
		{
			if (dicVC.Value.ReqThienMa < 1 && dicVC.Value.m_Class == slot && dicVC.Value.HeSoThienMa > 0 && dicVC.Value.CongKichType != VCCongKich.PHU_TRO)
			{
				TotalHeSoChieuThucLevel1 += dicVC.Value.HeSoThienMa;
				TotalHeSoChieuThucLevel2 += dicVC.Value.HeSoThienMa;
				TotalHeSoChieuThucLevel3 += dicVC.Value.HeSoThienMa;
				HeSoList.Add(TotalHeSoChieuThucLevel1);
				VoCongList.Add(dicVC.Value.Name);
			}
		}
		foreach (KeyValuePair<string, CfgVoCong> dicVC2 in ConfigManager.instance.m_dicVCs)
		{
			if (dicVC2.Value.ReqThienMa == 1 && dicVC2.Value.m_Class == slot && dicVC2.Value.HeSoThienMa > 0 && dicVC2.Value.CongKichType != VCCongKich.PHU_TRO)
			{
				TotalHeSoChieuThucLevel2 += dicVC2.Value.HeSoThienMa;
				TotalHeSoChieuThucLevel3 += dicVC2.Value.HeSoThienMa;
				HeSoList.Add(TotalHeSoChieuThucLevel2);
				VoCongList.Add(dicVC2.Value.Name);
			}
		}
		foreach (KeyValuePair<string, CfgVoCong> dicVC3 in ConfigManager.instance.m_dicVCs)
		{
			if (dicVC3.Value.ReqThienMa == 2 && dicVC3.Value.m_Class == slot && dicVC3.Value.HeSoThienMa > 0 && dicVC3.Value.CongKichType != VCCongKich.PHU_TRO)
			{
				TotalHeSoChieuThucLevel3 += dicVC3.Value.HeSoThienMa;
				HeSoList.Add(TotalHeSoChieuThucLevel3);
				VoCongList.Add(dicVC3.Value.Name);
			}
		}
		Random random = new Random();
		int num = 0;
		int num2 = 0;
		int num3;
		switch (slot)
		{
		case VCClass.CHIEU_THUC:
			num3 = thienmalenh.TichLuySlot1;
			break;
		case VCClass.BO_PHAP:
			num3 = thienmalenh.TichLuySlot2;
			break;
		default:
			num3 = thienmalenh.TichLuySlot3;
			break;
		}
		num2 = num3;
		num = ((num2 < 70) ? random.Next(0, TotalHeSoChieuThucLevel1) : ((num2 >= 150) ? random.Next(0, TotalHeSoChieuThucLevel3) : random.Next(0, TotalHeSoChieuThucLevel2)));
		for (int i = 0; i < HeSoList.Count; i++)
		{
			if (num < HeSoList[i])
			{
				return VoCongList[i];
			}
		}
		return string.Empty;
	}

	public static List<int> GetTrangBiEffLevel(UserInfo.TrangBiData trangBi)
	{
		List<int> list = new List<int>();
		list.Add(1);
		list.Add(1);
		list.Add(1);
		for (int num = ConfigManager.instance.OtherConfig.ThanBinhConfig.BaseCost.Count - 1; num >= 0; num--)
		{
			if (ConfigManager.instance.OtherConfig.ThanBinhConfig.BaseCost[num] < trangBi.EffecTichLuy)
			{
				int num2 = num % 3 + 1;
				int num3 = (int)Math.Ceiling((float)(num + 1) / 3f) + 1;
				for (int i = 0; i < num2; i++)
				{
					list[i] = num3;
				}
				for (int j = num2 - 1; j < 3; j++)
				{
					list[j] = num3 - 1;
				}
				break;
			}
		}
		return list;
	}

	public static float GetTrangBiEffRandVal(UserInfo.TrangBiData.TRANG_BI_EFF effect, int level)
	{
		Random random = new Random();
		return (float)(int)(10f * (ConfigManager.instance.OtherConfig.ThanBinhConfig.MinValue[effect.ToString() + level] + (float)random.NextDouble() * (ConfigManager.instance.OtherConfig.ThanBinhConfig.MaxValue[effect.ToString() + level] - ConfigManager.instance.OtherConfig.ThanBinhConfig.MinValue[effect.ToString() + level]))) / 10f;
	}

	public static UserInfo.TrangBiData.TRANG_BI_EFF GetRandomEffect(UserInfo.TrangBiData trangBi, Random random)
	{
		List<UserInfo.TrangBiData.TRANG_BI_EFF> list = new List<UserInfo.TrangBiData.TRANG_BI_EFF>();
		list.Add(UserInfo.TrangBiData.TRANG_BI_EFF.TANG_CONG);
		list.Add(UserInfo.TrangBiData.TRANG_BI_EFF.TANG_MAU);
		list.Add(UserInfo.TrangBiData.TRANG_BI_EFF.TANG_NOI);
		list.Add(UserInfo.TrangBiData.TRANG_BI_EFF.TANG_THU);
		list.Add(UserInfo.TrangBiData.TRANG_BI_EFF.CHINH_XAC);
		list.Add(UserInfo.TrangBiData.TRANG_BI_EFF.KHANG_BAO);
		if (trangBi.Name.StartsWith("VK_"))
		{
			list.Add(UserInfo.TrangBiData.TRANG_BI_EFF.STUN);
			list.Add(UserInfo.TrangBiData.TRANG_BI_EFF.PHONG_CHIEU);
			list.Add(UserInfo.TrangBiData.TRANG_BI_EFF.DINH_THAN);
			list.Add(UserInfo.TrangBiData.TRANG_BI_EFF.HUT_MAU);
		}
		if (trangBi.Name.StartsWith("AG_"))
		{
			list.Add(UserInfo.TrangBiData.TRANG_BI_EFF.BA_THE);
			list.Add(UserInfo.TrangBiData.TRANG_BI_EFF.HO_THE);
		}
		if (trangBi.Name.StartsWith("TS_"))
		{
			list.Add(UserInfo.TrangBiData.TRANG_BI_EFF.HOI_SINH);
		}
		if (trangBi.Name.StartsWith("MU_"))
		{
			list.Add(UserInfo.TrangBiData.TRANG_BI_EFF.BURN_MANA);
		}
		return list[random.Next(0, list.Count)];
	}

	public static float GetKhaiQuangTiLeThanhCong(UserInfo.TrangBiData trangBi, int vpSuDung)
	{
		if (trangBi.Effect.Count == 0)
		{
			return vpSuDung;
		}
		if (trangBi.Effect.Count == 1)
		{
			return (float)vpSuDung / 200f * 100f;
		}
		if (trangBi.Effect.Count == 2)
		{
			return (float)vpSuDung / 300f * 100f;
		}
		return 0f;
	}

	public static double StringCompare(string a, string b)
	{
		if (a == b)
		{
			return 100.0;
		}
		if (a.Length == 0 || b.Length == 0)
		{
			return 0.0;
		}
		string empty = string.Empty;
		string empty2 = string.Empty;
		if (a.Length > b.Length)
		{
			empty = b;
			empty2 = a;
		}
		else
		{
			empty = a;
			empty2 = b;
		}
		int num = 0;
		int i = 0;
		string text = empty;
		foreach (char c in text)
		{
			int num2 = i;
			bool flag = false;
			for (; i < empty2.Length; i++)
			{
				if (c == empty2[i])
				{
					num++;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				i = num2;
			}
		}
		return (float)num / (float)((empty.Length + empty2.Length) / 2) * 100f;
	}

	public static int RaiseFlag(int giatri, int flag_idx)
	{
		return giatri | (1 << flag_idx);
	}

	public static bool CheckFlag(int giatri, int flag_idx)
	{
		return (giatri & (1 << flag_idx)) != 0;
	}

	public static int DownFlag(int giatri, int flag_idx)
	{
		return giatri & ~(1 << flag_idx);
	}

	public static long RaiseFlagLong(long giatri, int flag_idx)
	{
		long num = 1L << flag_idx;
		return giatri | num;
	}

	public static bool CheckFlagLong(long giatri, int flag_idx)
	{
		long num = 1L << flag_idx;
		long num2 = giatri & num;
		return num2 != 0;
	}

	public static float GetVCTimeLength(VCType name)
	{
		VCTimeEffectCfg value;
		ConfigManager.instance.m_dicVCTimeEffects.TryGetValue(name.ToString(), out value);
		return (value != null) ? value.Time : 2f;
	}

	public static bool IsHarmfulEffect(VCEffect.VCEffectType type)
	{
		return type == VCEffect.VCEffectType.EFF_CHOANG || type == VCEffect.VCEffectType.EFF_DINH_THAN || type == VCEffect.VCEffectType.EFF_GIAM_CHINH_XAC || type == VCEffect.VCEffectType.EFF_HON_ME || type == VCEffect.VCEffectType.EFF_PHE_BO_PHAP || type == VCEffect.VCEffectType.EFF_PHE_THU_PHAP || type == VCEffect.VCEffectType.EFF_PHONG_CHIEU || type == VCEffect.VCEffectType.EFF_PHONG_KINH || type == VCEffect.VCEffectType.EFF_PHONG_THAN_PHAP || type == VCEffect.VCEffectType.EFF_SUY_NHUOC || type == VCEffect.VCEffectType.EFF_TAU_HOA || type == VCEffect.VCEffectType.EFF_TRUNG_DOC || type == VCEffect.VCEffectType.EFF_TRIET_PHONG || type == VCEffect.VCEffectType.EFF_XUAT_HUYET || type == VCEffect.VCEffectType.EFF_AM_HAN || type == VCEffect.VCEffectType.EFF_DOAT_MENH || type == VCEffect.VCEffectType.EFF_BAI_VAN || type == VCEffect.VCEffectType.EFF_DIEM_HUYET;
	}

	public static bool IsHelpfulEffect(VCEffect.VCEffectType type)
	{
		return type == VCEffect.VCEffectType.EFF_TANG_CONG || type == VCEffect.VCEffectType.EFF_TANG_THU || type == VCEffect.VCEffectType.EFF_PHUC_HOI_MP || type == VCEffect.VCEffectType.EFF_PHUC_HOI_HP || type == VCEffect.VCEffectType.EFF_BACH_COT_PHU_THE || type == VCEffect.VCEffectType.EFF_THAI_CUC_KHI || type == VCEffect.VCEffectType.EFF_BAC_MINH_CONG || type == VCEffect.VCEffectType.EFF_SAT_KHI || type == VCEffect.VCEffectType.EFF_THIEN_MA_KHI || type == VCEffect.VCEffectType.EFF_LANG_BA || type == VCEffect.VCEffectType.EFF_LANG_KHONG || type == VCEffect.VCEffectType.EFF_THIEN_CAN || type == VCEffect.VCEffectType.EFF_TUNG_HOANH || type == VCEffect.VCEffectType.EFF_DAP_THUY || type == VCEffect.VCEffectType.EFF_BICH_HO || type == VCEffect.VCEffectType.EFF_DAP_TUYET || type == VCEffect.VCEffectType.EFF_THONG_THIEN || type == VCEffect.VCEffectType.EFF_DANG_KHONG || type == VCEffect.VCEffectType.EFF_CAN_NGUYET || type == VCEffect.VCEffectType.EFF_XUYEN_VAN || type == VCEffect.VCEffectType.EFF_NHAN_HANH || type == VCEffect.VCEffectType.EFF_BA_THE || type == VCEffect.VCEffectType.EFF_THUAN_BO || type == VCEffect.VCEffectType.EFF_HO_THE || type == VCEffect.VCEffectType.EFF_THIEN_PHONG || type == VCEffect.VCEffectType.EFF_THIEN_TINH || type == VCEffect.VCEffectType.EFF_THIEN_NGUYEN || type == VCEffect.VCEffectType.EFF_QUY_HOA_CHAN_KHI || type == VCEffect.VCEffectType.EFF_VO_TUONG || type == VCEffect.VCEffectType.EFF_BANG_CO_NGOC_COT || type == VCEffect.VCEffectType.EFF_CUU_DUONG_CHAN_KHI || type == VCEffect.VCEffectType.EFF_TAM_MA || type == VCEffect.VCEffectType.EFF_TANG_TOC || type == VCEffect.VCEffectType.EFF_CUONG_HOA || type == VCEffect.VCEffectType.EFF_BO_PHONG || type == VCEffect.VCEffectType.EFF_HOAN_HINH || type == VCEffect.VCEffectType.EFF_TRUC_DIEN || type == VCEffect.VCEffectType.EFF_PHONG_SUONG || type == VCEffect.VCEffectType.EFF_KHANG_SAT_THUONG_VC;
	}

	public static List<int> GetServerListFromString(string str, List<CRemoteFarmClient> ServerInfos)
	{
		List<int> list = new List<int>();
		if (str != string.Empty)
		{
			string[] array = str.Split('_');
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Length <= 0)
				{
					continue;
				}
				int server_id = int.Parse(array[i]);
				if (list.Contains(server_id))
				{
					continue;
				}
				if (ServerInfos != null)
				{
					CRemoteFarmClient cRemoteFarmClient = ServerInfos.Find((CRemoteFarmClient e) => e.m_GameServerID == server_id);
					if (cRemoteFarmClient != null)
					{
						list.Add(server_id);
					}
				}
				else
				{
					list.Add(server_id);
				}
			}
		}
		return list;
	}
}

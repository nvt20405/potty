using System;
using System.Collections.Generic;

public class CommonHero
{
	public static int GetMenhCoSo(ChienHonCfg config, int level)
	{
		if (level < 1)
		{
			return 0;
		}
		return (int)Math.Round(config.Menh + (float)(level - 1) * config.MenhTT);
	}

	public static int GetNgoaiCoSo(ChienHonCfg config, int level)
	{
		if (level < 1)
		{
			return 0;
		}
		return (int)Math.Round(config.Ngoai + (float)(level - 1) * config.NgoaiTT);
	}

	public static int GetNoiCoSo(ChienHonCfg config, int level)
	{
		if (level < 1)
		{
			return 0;
		}
		return (int)Math.Round(config.Noi + (float)(level - 1) * config.NoiTT);
	}

	public static int GetThanPhapCoSo(ChienHonCfg config, int level)
	{
		if (level < 1)
		{
			return 0;
		}
		return (int)Math.Round(config.ThanPhap + (float)(level - 1) * config.ThanPhapTT);
	}

	public static int GetMenhCoSo(NhanVatCfg config, int level)
	{
		if (level < 1)
		{
			return 0;
		}
		return (int)Math.Round(config.Menh + (float)(level - 1) * config.MenhTT);
	}

	public static int GetNgoaiCoSo(NhanVatCfg config, int level)
	{
		if (level < 1)
		{
			return 0;
		}
		return (int)Math.Round(config.Ngoai + (float)(level - 1) * config.NgoaiTT);
	}

	public static int GetNoiCoSo(NhanVatCfg config, int level)
	{
		if (level < 1)
		{
			return 0;
		}
		return (int)Math.Round(config.Noi + (float)(level - 1) * config.NoiTT);
	}

	public static int GetThanPhapCoSo(NhanVatCfg config, int level)
	{
		if (level < 1)
		{
			return 0;
		}
		return (int)Math.Round(config.ThanPhap + (float)(level - 1) * config.ThanPhapTT);
	}

	public static CfgVoCong GetCfgVC(VCType type)
	{
		CfgVoCong value;
		ConfigManager.instance.m_dicVCs.TryGetValue(type.ToString(), out value);
		return value;
	}

	public static float GetVCAnimTime(VCType name)
	{
		VCTimeEffectCfg value;
		ConfigManager.instance.m_dicVCTimeEffects.TryGetValue(name.ToString(), out value);
		if (value == null)
		{
			string baseVCName = BattleHero.GetBaseVCName(name.ToString());
			ConfigManager.instance.m_dicVCTimeEffects.TryGetValue(baseVCName, out value);
		}
		return (value != null) ? value.Time : 2.5f;
	}

	public static float GetVCTimeDamage(VCType name, int number)
	{
		VCTimeEffectCfg value;
		ConfigManager.instance.m_dicVCTimeEffects.TryGetValue(name.ToString(), out value);
		if (value == null)
		{
			string baseVCName = BattleHero.GetBaseVCName(name.ToString());
			ConfigManager.instance.m_dicVCTimeEffects.TryGetValue(baseVCName, out value);
		}
		if (value != null)
		{
			switch (number)
			{
			case 1:
				return value.TimeDamage_1;
			case 2:
				return value.TimeDamage_2;
			case 3:
				return value.TimeDamage_3;
			case 4:
				return value.TimeDamage_4;
			case 5:
				return value.TimeDamage_5;
			case 6:
				return value.TimeDamage_6;
			case 7:
				return value.TimeDamage_7;
			case 8:
				return value.TimeDamage_8;
			case 9:
				return value.TimeDamage_9;
			case 10:
				return value.TimeDamage_10;
			case 11:
				return value.TimeDamage_11;
			case 12:
				return value.TimeDamage_12;
			case 13:
				return value.TimeDamage_13;
			}
		}
		return 0f;
	}

	public static double GetAttackThuongTimeDamage(string anim_vukhi, int combo_number, double ROF)
	{
		if (ROF < 0.5)
		{
			return 0.4000000059604645 * ROF;
		}
		NVAnimTimeCfg value;
		ConfigManager.instance.m_dicAttackTimeEffects.TryGetValue(anim_vukhi, out value);
		return (value != null) ? value.GetTimeDamage(combo_number, ROF) : (0.4000000059604645 * ROF);
	}

	public static List<double> GetListVCTimeDamage(VCType name)
	{
		List<double> list = new List<double>();
		VCTimeEffectCfg value;
		ConfigManager.instance.m_dicVCTimeEffects.TryGetValue(name.ToString(), out value);
		if (value != null)
		{
			if (value.TimeDamage_1 > 0f)
			{
				list.Add(value.TimeDamage_1 * value.Time);
			}
			if (value.TimeDamage_2 > 0f)
			{
				list.Add(value.TimeDamage_2 * value.Time);
			}
			if (value.TimeDamage_3 > 0f)
			{
				list.Add(value.TimeDamage_3 * value.Time);
			}
			if (value.TimeDamage_4 > 0f)
			{
				list.Add(value.TimeDamage_4 * value.Time);
			}
			if (value.TimeDamage_5 > 0f)
			{
				list.Add(value.TimeDamage_5 * value.Time);
			}
			if (value.TimeDamage_6 > 0f)
			{
				list.Add(value.TimeDamage_6 * value.Time);
			}
			if (value.TimeDamage_7 > 0f)
			{
				list.Add(value.TimeDamage_7 * value.Time);
			}
			if (value.TimeDamage_8 > 0f)
			{
				list.Add(value.TimeDamage_8 * value.Time);
			}
			if (value.TimeDamage_9 > 0f)
			{
				list.Add(value.TimeDamage_9 * value.Time);
			}
			if (value.TimeDamage_10 > 0f)
			{
				list.Add(value.TimeDamage_10 * value.Time);
			}
		}
		return list;
	}
}

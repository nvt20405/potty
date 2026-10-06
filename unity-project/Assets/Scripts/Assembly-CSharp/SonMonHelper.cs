using System;

public class SonMonHelper
{
	public static string GenTimeString(TimeSpan time)
	{
		if (time.Days > 0)
		{
			return time.Days + "d " + time.Hours + "h ";
		}
		return time.Hours + "h " + time.Minutes + "m " + time.Seconds + "s";
	}

	public static bool CheckThuHoachValid(UserInfo.SonMonBuildingInfo ct, UserInfo uInfo)
	{
		if (ct.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.CHINH_SANH || ct.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.NULL_CT)
		{
			return false;
		}
		if (ct.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.XUONG_DA && uInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_DA"))
		{
			return false;
		}
		if (ct.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.XUONG_GO && uInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_GO"))
		{
			return false;
		}
		if (GetSonMonCTResource(ct) > ConfigManager.instance.SonMonConfig.CongTrinhCfg[ct.LoaiCongTrinh.ToString()][ct.Level - 1].Storage / ConfigManager.instance.SonMonConfig.MinThuHoachDivide)
		{
			return true;
		}
		return false;
	}

	public static float GetSonMonLoseRate()
	{
		return 0.9f;
	}

	public static float GetSonMonWonRate()
	{
		return 0.9f;
	}

	public static int GetSonMonCTMaxLevel()
	{
		return 100;
	}

	public static int GetGiaDoThamSonMon(int count)
	{
		return Math.Max(0, (count - 5) * 10);
	}

	public static int GetSonMonCTResource(UserInfo.SonMonBuildingInfo ct)
	{
		DateTime now = DateTime.Now;
		now = GameManager.instance.m_GameClient.ServerTime;
		DateTime dateTime = ((!(ct.UpdatedTime > ct.BuildTime) && !(ct.BuildTime > now)) ? ct.BuildTime : DateTime.Now);
		if (ct.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.NULL_CT || ct.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.CHINH_SANH)
		{
			return 0;
		}
		if ((dateTime - ct.UpdatedTime).TotalDays > 1.0)
		{
			ct.UpdatedTime = DateTime.Now - new TimeSpan(1, 0, 0, 0);
		}
		int num = ct.CurValue + (int)Math.Floor((dateTime - ct.UpdatedTime).TotalHours / 24.0 * (double)ConfigManager.instance.SonMonConfig.CongTrinhCfg[ct.LoaiCongTrinh.ToString()][ct.Level - 1].Production);
		if (ct.Level < ConfigManager.instance.SonMonConfig.CongTrinhCfg[ct.LoaiCongTrinh.ToString()].Count)
		{
			num += (int)Math.Floor((DateTime.Now - dateTime).TotalHours / 24.0 * (double)ConfigManager.instance.SonMonConfig.CongTrinhCfg[ct.LoaiCongTrinh.ToString()][ct.Level].Production);
		}
		num = ((num <= ConfigManager.instance.SonMonConfig.CongTrinhCfg[ct.LoaiCongTrinh.ToString()][ct.Level - 1].Storage) ? num : ConfigManager.instance.SonMonConfig.CongTrinhCfg[ct.LoaiCongTrinh.ToString()][ct.Level - 1].Storage);
		return (num > 0) ? num : 0;
	}

	public static int GetKNBXayNhanH(UserInfo.SonMonBuildingInfo ct)
	{
		double num = (ct.BuildTime - DateTime.Now).TotalSeconds;
		int num2 = 0;
		if (num < 0.0)
		{
			return 0;
		}
		if (num > 306540.0)
		{
			num2 += (int)((num - 306540.0) / 480.0);
			num = 306540.0;
		}
		if (num > 152940.0)
		{
			num2 += (int)((num - 152940.0) / 360.0);
			num = 152940.0;
		}
		if (num > 76410.0)
		{
			num2 += (int)((num - 76410.0) / 300.0);
			num = 76410.0;
		}
		if (num > 37740.0)
		{
			num2 += (int)((num - 37740.0) / 300.0);
			num = 37740.0;
		}
		if (num > 18540.0)
		{
			num2 += (int)((num - 18540.0) / 240.0);
			num = 18540.0;
		}
		if (num > 8941.0)
		{
			num2 += (int)((num - 8941.0) / 180.0);
			num = 8941.0;
		}
		if (num > 4140.0)
		{
			num2 += (int)((num - 4140.0) / 120.0);
			num = 4140.0;
		}
		if (num > 1740.0)
		{
			num2 += (int)((num - 1740.0) / 100.0);
			num = 1740.0;
		}
		if (num > 540.0)
		{
			num2 += (int)((num - 540.0) / 80.0);
			num = 540.0;
		}
		if (num > 0.0)
		{
			num2 += (int)(num / 60.0);
		}
		return Math.Max(10, num2);
	}
}

using UnityEngine;

public class ScreenThienMaMain : ScreenBase
{
	public void HaPhong_OnClick(GameObject go)
	{
		bool flag = true;
		if (GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran != null && GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran.Count > 0)
		{
			for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran.Count; i++)
			{
				if (GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran[i] <= 0)
				{
					flag = false;
					break;
				}
			}
		}
		if (!flag)
		{
			MessagePopup.Create(Localization.instance.Get("ChuaMoHaPhongThongBao"));
		}
		else
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenThienMaHaPhong);
		}
	}

	public void ThuongPhong_OnClick(GameObject go)
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains("ThienMaThuongPhong"))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
		}
		else
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenThienMaThuongPhong);
		}
	}
}

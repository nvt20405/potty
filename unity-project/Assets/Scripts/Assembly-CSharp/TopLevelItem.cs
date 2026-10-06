using UnityEngine;

public class TopLevelItem : MonoBehaviour
{
	public UILabel lbName;

	public UILabel lbInfo;

	public int ID;

	public void setData(DuaTopLevelResponse.TopGamer data, int id)
	{
		if (data != null)
		{
			ID = id;
			lbName.text = "[FFEE00]" + (ID + 1) + ". [-]" + data.DisplayName;
			lbInfo.text = Localization.instance.Get("CapLabel") + ": " + data.Level + "\nEXP: " + data.Exp;
		}
	}

	public void onClick_btnQuaTang()
	{
		if (ID >= 0)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			if (ID == 0 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds1 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds1.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds1;
			}
			else if (ID == 1 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds2 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds2.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds2;
			}
			else if (ID == 2 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds3 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds3.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds3;
			}
			else if (ID == 3 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds4 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds4.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds4;
			}
			else if (ID == 4 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds5 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds5.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds5;
			}
			else if (ID == 5 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds6 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds6.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds6;
			}
			else if (ID == 6 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds7 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds7.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds7;
			}
			else if (ID == 7 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds8 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds8.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds8;
			}
			else if (ID == 8 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds9 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds9.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds9;
			}
			else if (ID == 9 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds10 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds10.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.Ds10;
			}
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), phanThuongResponse);
		}
	}
}

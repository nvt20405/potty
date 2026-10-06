using UnityEngine;

public class TopLuanKiemItem : MonoBehaviour
{
	public UILabel lbName;

	public int ID;

	public void setData(DuaTopLuanKiemResponse.TopGamer data, int id)
	{
		if (data != null)
		{
			ID = id;
			lbName.text = "[FFEE00]" + (ID + 1) + ". [-]" + data.DisplayName;
		}
	}

	public void onClick_btnQuaTang()
	{
		if (ID >= 0)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			if (ID == 0 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds1 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds1.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds1;
			}
			else if (ID == 1 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds2 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds2.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds2;
			}
			else if (ID == 2 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds3 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds3.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds3;
			}
			else if (ID == 3 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds4 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds4.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds4;
			}
			else if (ID == 4 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds5 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds5.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds5;
			}
			else if (ID == 5 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds6 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds6.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds6;
			}
			else if (ID == 6 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds7 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds7.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds7;
			}
			else if (ID == 7 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds8 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds8.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds8;
			}
			else if (ID == 8 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds9 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds9.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds9;
			}
			else if (ID == 9 && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds10 != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds10.Count > 0)
			{
				phanThuongResponse.PhanThuongList = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.Ds10;
			}
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), phanThuongResponse);
		}
	}
}

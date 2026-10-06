using System;
using UnityEngine;

public class ScreenHoatDongLienMinh : ScreenBase
{
	private const string lockBangChien = "BangChien";

	public UILabel DotLuaTraiLabel;

	public UILabel BangChienLabel;

	public GameObject batDauLuaTraiBtn;

	public GameObject thamGiaLuaTraiBtn;

	public GameObject grpBangChien;

	private BangChienResponse _response;

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		SetInfo();
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo != null && userInfo.ServerInfo != null && !userInfo.ServerInfo.LockTinhNang.Contains("BangChien"))
		{
			SyncNetworkData(null);
			GameManager.instance.m_GameClient.RequestBangChienGetInfo(false);
		}
		BangChienLabel.text = Localization.instance.Get("BangChienDesc");
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	private void OnHelpBtnClick()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(8, 2);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
	}

	public void SetInfo()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo != null && userInfo.LienMinh != null)
		{
			if (userInfo.LienMinh.DotLuaTraiCount <= 0)
			{
				DotLuaTraiLabel.text = string.Format(Localization.instance.Get("DotLuaTraiLabel"), Localization.instance.Get("ChuaDienRaLuaTraiLabel"));
				batDauLuaTraiBtn.gameObject.SetActive(userInfo.Gamer.ID == userInfo.LienMinh.MinhChuID || userInfo.Gamer.ID == userInfo.LienMinh.PhoMinhChuID);
				thamGiaLuaTraiBtn.gameObject.SetActive(false);
			}
			else
			{
				DotLuaTraiLabel.text = string.Format(Localization.instance.Get("DotLuaTraiLabel"), string.Format(Localization.instance.Get("DaDienRaLuaTraiLabel"), userInfo.LienMinh.DotLuaTraiTime.ToString("HH:mm:ss")));
				batDauLuaTraiBtn.gameObject.SetActive(false);
				TimeSpan timeSpan = GameManager.instance.m_GameClient.ServerTime - userInfo.LienMinh.DotLuaTraiTime;
				thamGiaLuaTraiBtn.gameObject.SetActive(timeSpan < TimeSpan.FromMinutes(ConfigManager.instance.OtherConfig.ThoiGianDotLuaTrai));
			}
		}
	}

	private void OnThamGiaLuaTraiBtnClick()
	{
		GameManager.instance.m_GameClient.RequestThamGiaLuaTrai(true);
	}

	private void OnDongYBatDauLuaTrai()
	{
		GameManager.instance.m_GameClient.RequestBatDauLuaTrai();
	}

	private void OnBatDauLuaTraiBtnClick()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo != null && userInfo.LienMinh != null)
		{
			if (userInfo.Gamer.ID == userInfo.LienMinh.MinhChuID || userInfo.Gamer.ID == userInfo.LienMinh.PhoMinhChuID)
			{
				PopupYesNo.Create(Localization.instance.Get("ConfirmBatDauLuaTraiMessage"), Localization.instance.Get("ConfirmLuaTraiYesLabel"), Localization.instance.Get("ConfirmLuaTraiNoLabel"), OnDongYBatDauLuaTrai, null);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("LuaTraiQuyenMinhChuMessage"));
			}
		}
	}

	private void Update()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (_response != null && userInfo != null && userInfo.ServerInfo != null && !userInfo.ServerInfo.LockTinhNang.Contains("BangChien"))
		{
			SyncNetworkData(_response);
		}
	}

	public void SyncNetworkData(BangChienResponse response)
	{
		BangChienLabel.text = Localization.instance.Get("BangChienDesc");
		_response = response;
		if (response != null)
		{
			TimeSpan timeSpan = response.TimeBatDau - GameManager.instance.m_GameClient.ServerTime;
			if (timeSpan.TotalHours > 12.0)
			{
				BangChienLabel.text = string.Format(Localization.instance.Get("BangChienDesc1"), response.TimeBatDau.ToString("dd/MM/yyyy HH:mm"));
			}
			else if (response.TimeBatDau >= GameManager.instance.m_GameClient.ServerTime)
			{
				UILabel bangChienLabel = BangChienLabel;
				bangChienLabel.text = bangChienLabel.text + " " + string.Format(Localization.instance.Get("BangChienDesc2"), string.Format("{0:00}:{1:00}:{2:00}", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds));
			}
			else
			{
				BangChienLabel.text = Localization.instance.Get("BangChienDesc3");
			}
		}
	}

	private void OnBatDauBangChienBtnClick()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo != null && userInfo.ServerInfo != null && !userInfo.ServerInfo.LockTinhNang.Contains("BangChien"))
		{
			GUIManager.setScreen(GAME_SCREEN.ScreenBangChien);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
		}
	}

	private void OnBatDauKiemMaBtnClick()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains("trongcay;"))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level < 28)
		{
			MessagePopup.Create(Localization.instance.Get("TrongCayChuaDuLevel"));
		}
		else
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenLienMinhTrongCay);
		}
	}

	private void OnBackBtnClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenLienMinhMain);
	}
}

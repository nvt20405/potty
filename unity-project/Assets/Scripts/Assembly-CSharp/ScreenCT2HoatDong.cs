using System;

public class ScreenCT2HoatDong : ScreenBase
{
	public UILabel lblLevel;

	public UILabel lblStartTime;

	public UILabel lblNextTime;

	public UILabel lblDeadLine;

	public UICheckbox autoCheckBox;

	private GetListCT2Response response;

	private void OnTuDongThamGia(bool isSelected)
	{
		if (isSelected && !GameManager.instance.m_GameClient.isAutoChienTruong)
		{
			MessagePopup.Create(Localization.instance.Get("KichHoatAutoCT2"));
		}
		else if (!isSelected && GameManager.instance.m_GameClient.isAutoChienTruong)
		{
			MessagePopup.Create(Localization.instance.Get("BoKichHoatAutoCT2"));
		}
		GameManager.instance.m_GameClient.isAutoChienTruong = isSelected;
	}

	private void OnJoinBtnClick()
	{
		GameManager.instance.m_GameClient.RequestThamGiaCT2();
	}

	private void OnBXHBtnClick()
	{
		GameManager.instance.m_GameClient.RequestCT2BXHTuanNay();
	}

	private void OnTroGiupBtnClick()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(6, 4);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		GameManager.instance.m_GameClient.RequestGetListCT2();
		autoCheckBox.isChecked = GameManager.instance.m_GameClient.isAutoChienTruong;
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	public void SyncWithNetworkData(GetListCT2Response response)
	{
		this.response = response;
		lblLevel.text = string.Format(Localization.instance.Get("CT2LevelRange"), response.MinLevel, response.MaxLevel);
		lblStartTime.text = string.Format(Localization.instance.Get("CT2StartTimeLabel"), response.StartTime.Hour, response.StartTime.Minute);
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (serverTime < response.NextTime)
		{
			TimeSpan timeSpan = response.NextTime - serverTime;
			lblNextTime.text = string.Format(Localization.instance.Get("CT2NextTimeLabel2"), (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
		}
		else
		{
			lblNextTime.text = string.Format(Localization.instance.Get("CT2NextTimeLabel"), response.NextTime.Hour, response.NextTime.Minute);
		}
	}

	private void Update()
	{
		if (response != null)
		{
			DateTime dateTime = response.DeadLineToJoin - TimeSpan.FromTicks(GameManager.instance.m_GameClient.ServerTimeDiffTick);
			DateTime now = DateTime.Now;
			if (now < dateTime)
			{
				TimeSpan timeSpan = dateTime - now;
				lblDeadLine.text = string.Format(Localization.instance.Get("CT2DeadLineLabel"), (int)timeSpan.TotalMinutes, timeSpan.Seconds);
			}
			else
			{
				lblDeadLine.text = Localization.instance.Get("CT2DeadLineLabel2");
			}
			DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
			if (serverTime < response.NextTime)
			{
				TimeSpan timeSpan2 = response.NextTime - serverTime;
				lblNextTime.text = string.Format(Localization.instance.Get("CT2NextTimeLabel2"), (int)timeSpan2.TotalHours, timeSpan2.Minutes, timeSpan2.Seconds);
			}
			else
			{
				lblNextTime.text = string.Format(Localization.instance.Get("CT2NextTimeLabel"), response.NextTime.Hour, response.NextTime.Minute);
			}
		}
	}
}

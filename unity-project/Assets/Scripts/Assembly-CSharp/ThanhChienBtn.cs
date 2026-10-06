using UnityEngine;

public class ThanhChienBtn : MonoBehaviour
{
	public int ThanhIdx;

	public UILabel tenThanhLabel;

	public UILabel BangHoiLabel;

	private void OnEnable()
	{
		SetTenThanh();
	}

	private void SetTenThanh()
	{
		if (ConfigManager.instance != null && ThanhIdx >= 0 && ThanhIdx < ConfigManager.instance.OtherConfig.BangChien.ListThanhChien.Count)
		{
			tenThanhLabel.text = ConfigManager.instance.OtherConfig.BangChien.ListThanhChien[ThanhIdx];
		}
	}

	public void SetInfo(string tenBang, string tenBangChu)
	{
		SetTenThanh();
		BangHoiLabel.text = tenBang;
	}

	private void OnClick()
	{
		ScreenBangChien screenBangChien = GUIManager.getScreen(GAME_SCREEN.ScreenBangChien) as ScreenBangChien;
		if (screenBangChien._response != null && screenBangChien._response.TimeBatDau < GameManager.instance.m_GameClient.ServerTime)
		{
			string arg = ConfigManager.instance.OtherConfig.BangChien.ListThanhChien[ThanhIdx];
			PopupYesNo.Create(string.Format(Localization.instance.Get("BangChienVaoThanhConfirm"), arg), Localization.instance.Get("MessageResetLuotGHYes"), Localization.instance.Get("MessageResetLuotGHNo"), () =>
			{
				GameManager.instance.m_GameClient.RequestBangChienVaoThanh(ThanhIdx);
			}, null);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("BangChienChuaDenGio"));
		}
	}
}

using UnityEngine;

public class ScreenSettings : ScreenBase
{
	public GameObject GiftcodeItem;

	public GameObject hoatDongItem;

	public GameObject troGiupItem;

	public GameObject voLamPhoItem;

	public GameObject lienHeItem;

	public GameObject phanHoiItem;

	public UILabel lbSounds;

	public UILabel lbThongBaoChat;

	public UILabel lbPublisherID;

	public UILabel MaMoiLabel;

	private void Start()
	{
		UIEventListener.Get(hoatDongItem.gameObject).onClick = hoatDongItem_OnClick;
		UIEventListener.Get(troGiupItem.gameObject).onClick = troGiupItem_OnClick;
		UIEventListener.Get(voLamPhoItem.gameObject).onClick = voLamPhoItem_OnClick;
		UIEventListener.Get(lienHeItem.gameObject).onClick = lienHeItem_OnClick;
		UIEventListener.Get(phanHoiItem.gameObject).onClick = phanHoiItem_OnClick;
	}

	private void Update()
	{
	}

	public void OnMoiBanChiTiet()
	{
		PopupYes.Create(Localization.instance.Get("InviteCodeFullDesc"), Localization.instance.Get("ClosePopupBtn"), null, true);
	}

	public override void OnActive()
	{
		base.OnActive();
		if (PlayerPrefs.GetInt("sounds_value", 1) == 1)
		{
			lbSounds.text = Localization.instance.Get("OnLabelBtn");
			AudioListener.pause = false;
		}
		else
		{
			lbSounds.text = Localization.instance.Get("OffLabelBtn");
			AudioListener.pause = true;
		}
		string key = "thongbaoChat_value" + GameManager.instance.m_GameClient.UserInfo.Gamer.ID;
		if (PlayerPrefs.GetInt(key, 1) == 1)
		{
			lbThongBaoChat.text = Localization.instance.Get("OnLabelBtn");
		}
		else
		{
			lbThongBaoChat.text = Localization.instance.Get("OffLabelBtn");
		}
		lbPublisherID.text = Localization.instance.Get("SohaIDLabel") + " " + GameManager.instance.m_GameClient.UserInfo.Gamer.UserName;
		string arg = ConfigManager.instance.GenerateInviteCode(GameManager.instance.ServerID, GameManager.instance.GamerID);
		string value = "MaMoi" + GameManager.GAMER_VERSION;
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			NGUITools.SetActive(MaMoiLabel.transform.parent.gameObject, false);
		}
		else
		{
			NGUITools.SetActive(MaMoiLabel.transform.parent.gameObject, true);
			MaMoiLabel.text = string.Format(Localization.instance.Get("InviteCodeLabel"), arg);
		}
		string value2 = "Giftcode" + GameManager.GAMER_VERSION;
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value2))
		{
			NGUITools.SetActive(GiftcodeItem, false);
		}
		else
		{
			NGUITools.SetActive(GiftcodeItem, true);
		}
		if (GameManager.instance.m_GameClient.isIpV6)
		{
			NGUITools.SetActive(GiftcodeItem, false);
			NGUITools.SetActive(MaMoiLabel.transform.parent.gameObject, false);
		}
	}

	public void hoatDongItem_OnClick(GameObject go)
	{
		PopupLoginMessage.Create(GameManager.instance.m_GameClient.UserInfo.ServerInfo);
	}

	public void troGiupItem_OnClick(GameObject go)
	{
		ScreenHelpMenu screenHelpMenu = GUIManager.getScreen(GAME_SCREEN.ScreenHelpMenu) as ScreenHelpMenu;
		screenHelpMenu.mCurrentMainMenuIndex = 0;
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpMenu);
	}

	public void voLamPhoItem_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenVoLamPho);
	}

	public void lienHeItem_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenLienHe);
	}

	public void phanHoiItem_OnClick(GameObject go)
	{
		Application.OpenURL("mailto:hotrogame@sohagame.vn?subject=Phản_hồi&body=");
	}

	public void btnDangXuat_OnClick()
	{
		PopupYesNo.Create(Localization.instance.Get("ConfirmChuongMonDangXuatMess"), Localization.instance.Get("DangXuatBtnLabel"), Localization.instance.Get("KhongBtnLabel"), OnYesConfirmBtnDangXuatClick, null);
	}

	public void OnYesConfirmBtnDangXuatClick()
	{
		SohaSDKManager.instance.Logout();
	}

	public void btnSound_OnClick(GameObject go)
	{
		if (PlayerPrefs.GetInt("sounds_value", 1) == 1)
		{
			AudioListener.pause = true;
			PlayerPrefs.SetInt("sounds_value", 0);
			lbSounds.text = Localization.instance.Get("OffLabelBtn");
		}
		else
		{
			AudioListener.pause = false;
			PlayerPrefs.SetInt("sounds_value", 1);
			lbSounds.text = Localization.instance.Get("OnLabelBtn");
		}
	}

	public void btnThongBaoChat_OnClick(GameObject go)
	{
		string key = "thongbaoChat_value" + GameManager.instance.m_GameClient.UserInfo.Gamer.ID;
		if (PlayerPrefs.GetInt(key, 1) == 1)
		{
			PlayerPrefs.SetInt(key, 0);
			lbThongBaoChat.text = Localization.instance.Get("OffLabelBtn");
		}
		else
		{
			PlayerPrefs.SetInt(key, 1);
			lbThongBaoChat.text = Localization.instance.Get("OnLabelBtn");
		}
	}

	public void btnGiftCode_OnClick(GameObject go)
	{
		EGDebug.Log("btnGiftCode_OnClick");
		PopUpGiftCode.Create();
	}
}

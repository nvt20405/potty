using System;
using Nettention.Proud;
using UnityEngine;

public class ScreenCT2 : ScreenBase
{
	public UILabel diemLabel;

	public UILabel dietLabel;

	public UILabel thoiGianConLaiLabel;

	public GameObject m_detu1Gr;

	public UISprite m_detu1AvatarSpr;

	public UISprite m_detu1HpBarSpr;

	public GameObject m_detu2Gr;

	public UISprite m_detu2AvatarSpr;

	public UISprite m_detu2HpBarSpr;

	public GameObject m_detu3Gr;

	public UISprite m_detu3AvatarSpr;

	public UISprite m_detu3HpBarSpr;

	public GameObject m_detu4Gr;

	public UISprite m_detu4AvatarSpr;

	public UISprite m_detu4HpBarSpr;

	public GameObject m_detu5Gr;

	public UISprite m_detu5AvatarSpr;

	public UISprite m_detu5HpBarSpr;

	public GameObject m_detu6Gr;

	public UISprite m_detu6AvatarSpr;

	public UISprite m_detu6HpBarSpr;

	public GameObject m_detu7Gr;

	public UISprite m_detu7AvatarSpr;

	public UISprite m_detu7HpBarSpr;

	public GameObject m_detu8Gr;

	public UISprite m_detu8AvatarSpr;

	public UISprite m_detu8HpBarSpr;

	public UISprite m_tongHpBarSpr;

	private DateTime m_TimeOut = DateTime.Now;

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(0);
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = false;
		}
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
		Utils.SetLightMaps("Lightmap/Chinh_ta_dai_chien/");
		UISprite[] array3 = new UISprite[8] { m_detu1HpBarSpr, m_detu2HpBarSpr, m_detu3HpBarSpr, m_detu4HpBarSpr, m_detu5HpBarSpr, m_detu6HpBarSpr, m_detu7HpBarSpr, m_detu8HpBarSpr };
		UISprite[] array4 = array3;
		UISprite[] array5 = array4;
		foreach (UISprite uISprite in array5)
		{
			uISprite.color = Color.green;
		}
	}

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

	public override void OnDeactive()
	{
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = true;
		}
		base.OnDeactive();
	}

	private void SetChild3DScreen()
	{
		if (!(child3Dscreen != null))
		{
			GameObject gameObject = Utils.instantiatePrefab("GUI/Screens3D/ScreenChienTruong3D", GUIManager.instance.ScreenContainer3D, false, false);
			gameObject.transform.parent = GUIManager.instance.ScreenContainer3D;
			gameObject.transform.localPosition = UnityEngine.Vector3.zero;
			gameObject.transform.localScale = UnityEngine.Vector3.one;
			gameObject.SetActive(false);
			child3Dscreen = gameObject;
		}
	}

	public ScreenChienTruong3D Get3DCom()
	{
		if (child3Dscreen == null)
		{
			SetChild3DScreen();
		}
		return child3Dscreen.GetComponent<ScreenChienTruong3D>();
	}

	private void OnBackBtnClick()
	{
		GameManager.instance.m_GameClient.RequestCT2Quit();
		GameManager.instance.isStartJoinCT2 = false;
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenCT2HoatDong);
		Destroy3D();
	}

	private void OnXepHangBtnClick()
	{
		GameManager.instance.m_GameClient.RequestCT2BXH();
	}

	public void Destroy3D()
	{
		if (child3Dscreen != null)
		{
			UnityEngine.Object.Destroy(child3Dscreen);
		}
		child3Dscreen = null;
	}

	public void OnBattleEnd()
	{
		GameManager.instance.m_GameClient.m_C2SProxy.RequestCT2EndBattle(HostID.Server, RmiContext.ReliableSend);
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		if (GameManager.instance.CT2KetThuc)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenCT2HoatDong);
			Destroy3D();
			if (PopupBXHChinhTa.instance != null)
			{
				PopupBXHChinhTa.instance.gameObject.SetActive(true);
			}
		}
		else
		{
			ScreenChienTruong3D screenChienTruong3D = Get3DCom();
			screenChienTruong3D.UpdateHpDoiHinh(screenBattle.MyTeamData);
		}
	}

	public void SetDiem(int diem)
	{
		diemLabel.text = string.Format(Localization.instance.Get("CT2DiemLabel"), diem);
	}

	public void SetDiet(int kill)
	{
		dietLabel.text = string.Format(Localization.instance.Get("CT2KillLabel"), kill);
	}

	public void SetTimeOut(DateTime timeOut)
	{
		m_TimeOut = timeOut;
	}

	private void Update()
	{
		TimeSpan timeSpan = m_TimeOut - GameManager.instance.m_GameClient.ServerTime;
		thoiGianConLaiLabel.text = string.Format(Localization.instance.Get("CT2TimeOutLabel"), (int)timeSpan.TotalMinutes, timeSpan.Seconds);
	}
}

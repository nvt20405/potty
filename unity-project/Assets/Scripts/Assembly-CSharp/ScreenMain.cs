using System;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using UnityEngine;

public class ScreenMain : ScreenBase
{
	public UILabel fpsLabel;

	public UISprite newMailIcon;

	public UILabel newMailLabel;

	public UISprite newRequestFriendIcon;

	public UILabel newRequestFriendLabel;

	public GameObject btnDongNhan;

	public GameObject groupButton;

	public GameObject btnMobGameHoTro;

	private GameObject currOffUserSelected;

	public bool waittingRequestBatCoc;

	public int currOffUserID = -1;

	private float xacSuatDiChuyen = 0.7f;

	private List<ButtonSuKienThanhChinh> listNewEventButton = new List<ButtonSuKienThanhChinh>();

	private List<MenuButtonKyNgo> listNewEventNapTien = new List<MenuButtonKyNgo>();

	private Vector3 itemPos = new Vector3(-50f, 550f, 0f);

	public GameObject btnEventNapTien;

	public GameObject btnKyNgo_Perfab;

	public GameObject EventNapTienPanel;

	public UISprite bkgListEventNapTien;

	private float spaceButton = 80f;

	private float nextSecond;

	public GameObject buttonEventPrefab;

	public GameObject ThoatNienThuBtn;

	public ParticleSystem NienThuHitEffect;

	public UILabel DiemNienThu;

	public UILabel ThoiGianNienThu;

	public GameObject EventNapTienGrp;

	private GadgetPanelBottom botPanel;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
		btnMobGameHoTro.SetActive(false);
	}

	private void Update()
	{
		if (ThoatNienThuBtn != null && ThoatNienThuBtn.activeSelf && GUIManager.instance.homeCity != null && GUIManager.instance.homeCity.CurNienThu != null)
		{
			TimeSpan timeSpan = GUIManager.instance.homeCity.CurNienThu.TimeStart + new TimeSpan(0, 15, 0) - GameManager.instance.m_GameClient.ServerTime;
			if (ThoiGianNienThu != null)
			{
				ThoiGianNienThu.text = string.Format(Localization.instance.Get("NienThuThoiGian"), timeSpan.Minutes, timeSpan.Seconds);
			}
			if (timeSpan.TotalSeconds < 0.0)
			{
				ThoatDanhNienThu();
			}
		}
		if (fpsLabel != null && GUIManager.instance != null && GUIManager.instance.fpsCounter != null)
		{
			fpsLabel.text = string.Format("{0} fps", GUIManager.instance.fpsCounter.FPS);
		}
		nextSecond += Time.deltaTime;
		if (nextSecond >= 300f)
		{
			nextSecond = 0f;
			refreshMail(false);
		}
		if (GUIManager.instance != null && GUIManager.instance.isAutoBatCoc && !waittingRequestBatCoc)
		{
			if (currOffUserSelected != null)
			{
				goToOfflineUser(currOffUserSelected);
			}
			else
			{
				getNewTargetBatCoc();
			}
		}
	}

	private void refreshMail(bool showLoading = true)
	{
		GameManager.instance.m_GameClient.RequestRefreshMail(showLoading);
	}

	private void OnDanhDongNhanClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenDongNhan);
		ScreenDongNhan screenDongNhan = GUIManager.getScreen(GAME_SCREEN.ScreenDongNhan) as ScreenDongNhan;
		screenDongNhan.OnDanhDongNhanClick();
	}

	private void OnChatBtnClick()
	{
		if (GameManager.instance.m_GameClient.isIpV6)
		{
			MessagePopup.Create(Localization.instance.Get("ErrorIPV6Mess"));
		}
		else
		{
			GUIManager.setScreen(GAME_SCREEN.ScreenChat);
		}
	}

	private void OnMailBtnClick()
	{
		if (GameManager.instance.m_GameClient.isIpV6)
		{
			MessagePopup.Create(Localization.instance.Get("ErrorIPV6Mess"));
		}
		else
		{
			GUIManager.setScreen(GAME_SCREEN.ScreenMail);
		}
	}

	private void OnFriendsBtnClick()
	{
		if (GameManager.instance.m_GameClient.isIpV6)
		{
			MessagePopup.Create(Localization.instance.Get("ErrorIPV6Mess"));
		}
		else
		{
			GameManager.instance.m_GameClient.RequestBanBeCuuThuInfo();
		}
	}

	private void OnSettingBtnClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenSettings);
	}

	private void OnEventNapTienBtnClick()
	{
	}

	private void OnMobGameSupportBtnClick()
	{
	}

	public void initListEventNapTien()
	{
		if (listNewEventNapTien != null && listNewEventNapTien.Count > 0)
		{
			foreach (Transform item in groupButton.transform)
			{
				Transform transform2 = item;
				MenuButtonKyNgo component = transform2.gameObject.GetComponent<MenuButtonKyNgo>();
				for (int i = 0; i < listNewEventNapTien.Count; i++)
				{
					if (component != null && component == listNewEventNapTien[i])
					{
						UnityEngine.Object.Destroy(transform2.gameObject);
					}
				}
			}
			listNewEventNapTien.Clear();
		}
		float x = -120f;
		float num = 200f;
		Vector3 vector = default(Vector3);
		vector = new Vector3(x, 0f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(240f, 0f, 0f);
		Vector3 vector3 = default(Vector3);
		vector3 = new Vector3(0f, -80f, 0f);
		int num2 = 1;
		int num3 = 0;
		int num4 = 1;
		int num5 = 2;
		int num6 = 3;
		int num7 = 4;
		int num8 = 5;
		int num9 = 6;
		int num10 = 7;
		int num11 = 8;
		int num12 = 9;
		int num13 = 10;
		int num14 = 11;
		bool flag = false;
		for (int j = 0; j <= 11; j++)
		{
			MenuButtonKyNgo menuButtonKyNgo = null;
			if (j == num3 && GameManager.instance.m_GameClient.UserInfo.Gamer.KnbDaNap == 0)
			{
				num2 = 1;
				flag = true;
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.NapLanDau, false);
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnNapLanDauBtnClick;
			}
			if (j == num4 && botPanel.checkDisplayDiHoaCungMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.DiHoaCung, false);
				if (flag)
				{
					vector += vector2;
					if (vector.x > num)
					{
						num2++;
						vector += vector3;
						vector.x = x;
					}
				}
				else
				{
					flag = true;
				}
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnDiHoaCungBtnClick;
			}
			if (j == num5 && botPanel.checkThuongNapMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.ThuongNap, false);
				if (flag)
				{
					vector += vector2;
					if (vector.x > num)
					{
						num2++;
						vector += vector3;
						vector.x = x;
					}
				}
				else
				{
					flag = true;
				}
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnThuongNapBtnClick;
			}
			if (j == num6 && botPanel.checkULinhSonTrangMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.SonTrang, false);
				if (flag)
				{
					vector += vector2;
					if (vector.x > num)
					{
						num2++;
						vector += vector3;
						vector.x = x;
					}
				}
				else
				{
					flag = true;
				}
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnSonTrangBtnClick;
			}
			if (j == num7 && botPanel.checkDisplayThanTaiMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.ThanTai, false);
				if (flag)
				{
					vector += vector2;
					if (vector.x > num)
					{
						num2++;
						vector += vector3;
						vector.x = x;
					}
				}
				else
				{
					flag = true;
				}
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnThanTaiBtnClick;
			}
			if (j == num8 && botPanel.checkDisplayRuongTSMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.RuongThachSanh, false);
				if (flag)
				{
					vector += vector2;
					if (vector.x > num)
					{
						num2++;
						vector += vector3;
						vector.x = x;
					}
				}
				else
				{
					flag = true;
				}
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnThachSanhBtnClick;
			}
			if (j == num9 && botPanel.checkDisplayTichLuyNapMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.TichLuyNap, false);
				if (flag)
				{
					vector += vector2;
					if (vector.x > num)
					{
						num2++;
						vector += vector3;
						vector.x = x;
					}
				}
				else
				{
					flag = true;
				}
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnTichLuyNapBtnClick;
			}
			if (j == num10 && botPanel.checkDisplayTichLuyTieuMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.TichLuyTieu, false);
				if (flag)
				{
					vector += vector2;
					if (vector.x > num)
					{
						num2++;
						vector += vector3;
						vector.x = x;
					}
				}
				else
				{
					flag = true;
				}
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnTichLuyTieuBtnClick;
			}
			if (j == num11 && botPanel.checkDisplayVongQuayMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.VongQuay, false);
				if (flag)
				{
					vector += vector2;
					if (vector.x > num)
					{
						num2++;
						vector += vector3;
						vector.x = x;
					}
				}
				else
				{
					flag = true;
				}
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnVongQuayBtnClick;
			}
			if (j == num12 && botPanel.checkDisplayBaoRuongMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.BaoRuong, false);
				if (flag)
				{
					vector += vector2;
					if (vector.x > num)
					{
						num2++;
						vector += vector3;
						vector.x = x;
					}
				}
				else
				{
					flag = true;
				}
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnBaoRuongBtnClick;
			}
			if (j == num13 && botPanel.checkDisplayNapHangNgayMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.TichLuyNapHangNgay, false);
				if (flag)
				{
					vector += vector2;
					if (vector.x > num)
					{
						num2++;
						vector += vector3;
						vector.x = x;
					}
				}
				else
				{
					flag = true;
				}
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnNapHangNgayBtnClick;
			}
			if (j == num14 && botPanel.checkDisplayTuBaoBonMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.TuBaoBon, false);
				if (flag)
				{
					vector += vector2;
					if (vector.x > num)
					{
						num2++;
						vector += vector3;
						vector.x = x;
					}
				}
				else
				{
					flag = true;
				}
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnTuBaoBonBtnClick;
			}
			if (menuButtonKyNgo != null)
			{
				menuButtonKyNgo.transform.parent = EventNapTienPanel.transform;
				menuButtonKyNgo.transform.localScale = new Vector3(1f, 1f, 1f);
				menuButtonKyNgo.transform.localPosition = vector;
				listNewEventNapTien.Add(menuButtonKyNgo);
			}
		}
		if (listNewEventNapTien != null && listNewEventNapTien.Count > 0)
		{
			for (int k = 0; k < listNewEventNapTien.Count; k++)
			{
				listNewEventNapTien[k].focus.gameObject.SetActive(false);
			}
			if (listNewEventNapTien.Count > 2)
			{
				float y = 35 + num2 * 75;
				bkgListEventNapTien.transform.localScale = new Vector3(bkgListEventNapTien.transform.localScale.x, y, bkgListEventNapTien.transform.localScale.z);
			}
		}
	}

	private void OnNapLanDauBtnClick(GameObject go)
	{
		PopUpNapTien.Create();
	}

	private void OnDiHoaCungBtnClick(GameObject go)
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenKyNgoDiHoaCung);
	}

	private void OnThuongNapBtnClick(GameObject go)
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenKyNgoThuongNap);
	}

	private void OnSonTrangBtnClick(GameObject go)
	{
		GameManager.instance.m_GameClient.RequestULinhInfo();
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenULinhSonTrang);
	}

	private void OnThanTaiBtnClick(GameObject go)
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenKyNgoThanTai);
	}

	private void OnThachSanhBtnClick(GameObject go)
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenRuongThachSanh);
	}

	private void OnTichLuyNapBtnClick(GameObject go)
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenTuongDuongTeThe);
	}

	private void OnTichLuyTieuBtnClick(GameObject go)
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenKimTienBang);
	}

	private void OnVongQuayBtnClick(GameObject go)
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenVongQuay);
	}

	private void OnBaoRuongBtnClick(GameObject go)
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenEventMoRuong);
	}

	private void OnGuiTietKiemBtnClick(GameObject go)
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenKyNgoGuiTietKiem);
	}

	private void OnNapHangNgayBtnClick(GameObject go)
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenTichNapHangNgay);
	}

	private void OnTuBaoBonBtnClick(GameObject go)
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenTuBaoBon);
	}

	public override void OnActive()
	{
		if (child3Dscreen == null && GUIManager.instance.homeCity != null)
		{
			child3Dscreen = GUIManager.instance.homeCity.gameObject;
		}
		base.OnActive();
		try
		{
			if (GUIManager.instance.homeCity != null && GUIManager.instance.homeCity.mainAvatar == null)
			{
				if (GameManager.instance.m_GameClient.HomeResponse != null)
				{
					GUIManager.instance.homeCity.SyncWithNetworkData();
				}
				else
				{
					GameManager.instance.m_GameClient.RequestGetOtherPlayer();
				}
			}
		}
		catch (Exception ex)
		{
			EGDebug.LogError("[ScreenMain] Sync avatar on active error: " + ((ex != null) ? ex.ToString() : null));
		}
		GUIManager.ShowGadgets(6);
		botPanel = GUIManager.instance.gadgetPanelBottom;
		try
		{
			if (botPanel != null)
			{
				botPanel.checkDisplayNotifyMenu();
				if (botPanel.checkDisplayEventNapTien())
				{
					if (btnEventNapTien != null)
					{
						btnEventNapTien.gameObject.SetActive(true);
					}
				}
				else if (btnEventNapTien != null)
				{
					btnEventNapTien.gameObject.SetActive(false);
				}
			}
		}
		catch (Exception ex2)
		{
			EGDebug.LogError("[ScreenMain] botPanel check error: " + ((ex2 != null) ? ex2.ToString() : null));
		}
		if (EventNapTienGrp != null)
		{
			EventNapTienGrp.gameObject.SetActive(false);
		}
		if (GUIManager.instance.cam2D != null)
		{
			AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
			if (GUIManager.instance.homeCity != null)
			{
				AudioListener component2 = GUIManager.instance.homeCity.cam.GetComponent<AudioListener>();
				if (!GameManager.instance.isStartTutorial)
				{
					if (component != null)
					{
						component.enabled = false;
					}
					if (component2 != null)
					{
						component2.enabled = true;
					}
				}
				else
				{
					if (component != null)
					{
						component.enabled = true;
					}
					if (component2 != null)
					{
						component2.enabled = false;
					}
				}
			}
		}
		if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBanhChung && GUIManager.instance.homeCity != null && GUIManager.instance.homeCity.mainAvatar != null)
		{
			HomeResponse.Position3D obj = new HomeResponse.Position3D(GUIManager.instance.homeCity.mainAvatar.transform.position.x, GUIManager.instance.homeCity.mainAvatar.transform.position.y, GUIManager.instance.homeCity.mainAvatar.transform.position.z);
			GameManager.instance.m_GameClient.SendRequest(GameManager.instance.m_GameClient.C2SProxy.RequestChangeNextPos, JsonMapper.ToJson(obj, false), false);
		}
		getInfoNewMail();
		getRequestFriend();
		Utils.SetLightMaps("Lightmap/HomeCity/", 2);
		if (listNewEventButton != null && listNewEventButton.Count > 0)
		{
			if (groupButton != null)
			{
				foreach (Transform item in groupButton.transform)
				{
					Transform transform2 = item;
					ButtonSuKienThanhChinh component3 = transform2.gameObject.GetComponent<ButtonSuKienThanhChinh>();
					for (int i = 0; i < listNewEventButton.Count; i++)
					{
						if (component3 != null && component3 == listNewEventButton[i])
						{
							UnityEngine.Object.Destroy(transform2.gameObject);
						}
					}
				}
			}
			listNewEventButton.Clear();
		}
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg != null)
		{
			DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
			DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.ThoiGianBatDau;
			DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLevelCfg.ThoiGianKetThuc;
			if (serverTime >= thoiGianBatDau && serverTime <= thoiGianKetThuc.AddDays(3.0))
			{
				ButtonSuKienThanhChinh buttonSuKienThanhChinh = addNewEventButton(ButtonSuKienThanhChinh.SuKienType.TopLevel);
				if (buttonSuKienThanhChinh != null)
				{
					UIEventListener.Get(buttonSuKienThanhChinh.gameObject).onClick = onClick_BtnTopLevelEvent;
				}
			}
		}
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg != null)
		{
			DateTime serverTime2 = GameManager.instance.m_GameClient.ServerTime;
			DateTime thoiGianBatDau2 = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.ThoiGianBatDau;
			DateTime thoiGianKetThuc2 = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DuaTopLKCfg.ThoiGianKetThuc;
			if (serverTime2 >= thoiGianBatDau2 && serverTime2 <= thoiGianKetThuc2.AddDays(3.0))
			{
				ButtonSuKienThanhChinh buttonSuKienThanhChinh2 = addNewEventButton(ButtonSuKienThanhChinh.SuKienType.TopLuanKiem);
				if (buttonSuKienThanhChinh2 != null)
				{
					UIEventListener.Get(buttonSuKienThanhChinh2.gameObject).onClick = onClick_BtnTopLuanKiemEvent;
				}
			}
		}
		ButtonSuKienThanhChinh buttonSuKienThanhChinh3 = addNewEventButton(ButtonSuKienThanhChinh.SuKienType.CacLoaiTop);
		if (buttonSuKienThanhChinh3 != null)
		{
			UIEventListener.Get(buttonSuKienThanhChinh3.gameObject).onClick = onClick_BtnCacLoaiTop;
		}
		ButtonSuKienThanhChinh buttonSuKienThanhChinh4 = addNewEventButton(ButtonSuKienThanhChinh.SuKienType.Facebook);
		if (buttonSuKienThanhChinh4 != null)
		{
			UIEventListener.Get(buttonSuKienThanhChinh4.gameObject).onClick = onClick_BtnFaceBook;
		}
		if (!GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains("HoiVienViet") && !GameManager.instance.m_GameClient.isIpV6)
		{
			ButtonSuKienThanhChinh buttonSuKienThanhChinh5 = addNewEventButton(ButtonSuKienThanhChinh.SuKienType.HoiVienViet);
			if (buttonSuKienThanhChinh5 != null)
			{
				UIEventListener.Get(buttonSuKienThanhChinh5.gameObject).onClick = onClick_BtnHoiVienViet;
			}
		}
		SohaSDKManager.instance.ShowSohaDashBoardButton();
	}

	public void onClick_BtnTopLevelEvent(GameObject go)
	{
		if (GameManager.instance.m_GameClient.isIpV6)
		{
			MessagePopup.Create(Localization.instance.Get("ErrorIPV6Mess"));
		}
		else
		{
			GameManager.instance.m_GameClient.RequestGetDuaTopLevelInfo();
		}
	}

	public void onClick_BtnTopLuanKiemEvent(GameObject go)
	{
		if (GameManager.instance.m_GameClient.isIpV6)
		{
			MessagePopup.Create(Localization.instance.Get("ErrorIPV6Mess"));
		}
		else
		{
			GameManager.instance.m_GameClient.RequestGetDuaTopLuanKiemInfo();
		}
	}

	public void onClick_BtnCacLoaiTop(GameObject go)
	{
		if (GameManager.instance.m_GameClient.isIpV6)
		{
			MessagePopup.Create(Localization.instance.Get("ErrorIPV6Mess"));
		}
		else
		{
			GUIManager.setScreen(GAME_SCREEN.ScreenTop);
		}
	}

	public void onClick_BtnFaceBook(GameObject go)
	{
		Application.OpenURL("https://m.facebook.com/groups/MongVoLam3D/");
	}

	public void onClick_BtnHoiVienViet(GameObject go)
	{
		if (!GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains("HoiVienViet"))
		{
			Application.OpenURL("http://mongvolam.sohagame.vn/game-viet");
		}
	}

	public void getRequestFriend()
	{
		if (newRequestFriendIcon != null)
		{
			newRequestFriendIcon.gameObject.SetActive(false);
		}
		if (newRequestFriendLabel != null)
		{
			newRequestFriendLabel.gameObject.SetActive(false);
		}
		if (GameManager.instance.m_GameClient.UserInfo.BanBeList == null || GameManager.instance.m_GameClient.UserInfo.BanBeList.Count <= 0)
		{
			return;
		}
		int num = 0;
		for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.BanBeList.Count; i++)
		{
			UserInfo.BanBeData banBeData = GameManager.instance.m_GameClient.UserInfo.BanBeList[i];
			if (banBeData.Status == UserInfo.BanBeData.BanBeStatus.REQUESTING)
			{
				num++;
			}
		}
		if (num > 0)
		{
			if (newRequestFriendIcon != null)
			{
				newRequestFriendIcon.gameObject.SetActive(true);
			}
			if (newRequestFriendLabel != null)
			{
				newRequestFriendLabel.gameObject.SetActive(true);
				newRequestFriendLabel.text = num.ToString();
			}
		}
	}

	public void getInfoNewMail()
	{
		if (newMailIcon != null)
		{
			newMailIcon.gameObject.SetActive(false);
		}
		if (newMailLabel != null)
		{
			newMailLabel.gameObject.SetActive(false);
		}
		if (GameManager.instance.m_GameClient.UserInfo.MailList == null || GameManager.instance.m_GameClient.UserInfo.MailList.Count <= 0)
		{
			return;
		}
		int num = 0;
		for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.MailList.Count; i++)
		{
			UserInfo.MailData mailData = GameManager.instance.m_GameClient.UserInfo.MailList[i];
			if (mailData.Status == UserInfo.MailData.MAIL_STATUS.Unread)
			{
				num++;
			}
		}
		if (num > 0)
		{
			if (newMailIcon != null)
			{
				newMailIcon.gameObject.SetActive(true);
			}
			if (newMailLabel != null)
			{
				newMailLabel.gameObject.SetActive(true);
				newMailLabel.text = num.ToString();
			}
		}
	}

	public override void OnDeactive()
	{
		if (GUIManager.instance.cam2D != null)
		{
			AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
			if (component != null)
			{
				component.enabled = true;
			}
		}
		SohaSDKManager.instance.HideSohaDashBoardButton();
		base.OnDeactive();
		Resources.UnloadUnusedAssets();
	}

	private void OnTestBattleClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenTestBattle);
	}

	public void endAutoBatCoc()
	{
		currOffUserID = -1;
		currOffUserSelected = null;
		GUIManager.instance.isAutoBatCoc = false;
		waittingRequestBatCoc = false;
		CoroutineManager.stopCoroutine();
	}

	public void OnStartAutoBatCoc()
	{
		int maxLuotBatCocByVip = ConfigManager.GetMaxLuotBatCocByVip(GameManager.instance.m_GameClient.UserInfo.Gamer.Vip);
		int luotBatCoc = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.LuotBatCoc;
		currOffUserID = -1;
		currOffUserSelected = null;
		if (luotBatCoc >= maxLuotBatCocByVip)
		{
			GUIManager.instance.isAutoBatCoc = false;
			MessagePopup.Create(Localization.instance.Get("HetLuotBatCocLabel"));
		}
		else
		{
			GUIManager.instance.isAutoBatCoc = true;
			startAutoBatCoc();
		}
	}

	public void endBattleAutoBatCoc()
	{
		waittingRequestBatCoc = false;
		CoroutineManager.WaitForSeconds(0.5f, continueAutoBatCoc);
	}

	public void continueAutoBatCoc()
	{
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.Release(true);
		}
		if (PopupBattleResult.instance != null)
		{
			PopupBattleResult.DestroyPopup();
		}
		currOffUserID = -1;
		currOffUserSelected = null;
		int maxLuotBatCocByVip = ConfigManager.GetMaxLuotBatCocByVip(GameManager.instance.m_GameClient.UserInfo.Gamer.Vip);
		int luotBatCoc = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.LuotBatCoc;
		if (luotBatCoc < maxLuotBatCocByVip)
		{
			startAutoBatCoc();
			return;
		}
		currOffUserID = -1;
		currOffUserSelected = null;
		GUIManager.instance.isAutoBatCoc = false;
		MessagePopup.Create(Localization.instance.Get("HetLuotBatCocLabel"));
	}

	public void getNewTargetBatCoc()
	{
		if (GUIManager.instance.homeCity == null)
		{
			return;
		}
		if (GUIManager.instance.homeCity.otherPlayers.Count > 0)
		{
			Dictionary<int, GameObject> dictionary = new Dictionary<int, GameObject>();
			for (int i = 0; i < GUIManager.instance.homeCity.otherPlayers.Count; i++)
			{
				GameObject gameObject = GUIManager.instance.homeCity.otherPlayers.Values.ElementAt(i);
				PlayerMovement component = gameObject.GetComponent<PlayerMovement>();
				if (component != null && !component.IsOnline)
				{
					dictionary.Add(i, gameObject);
				}
			}
			if (dictionary.Count > 0)
			{
				int index = UnityEngine.Random.Range(0, dictionary.Count - 1);
				currOffUserSelected = GUIManager.instance.homeCity.otherPlayers.Values.ElementAt(dictionary.Keys.ElementAt(index));
				currOffUserID = GUIManager.instance.homeCity.otherPlayers.Keys.ElementAt(dictionary.Keys.ElementAt(index));
			}
			else
			{
				getListOtherPlayer();
			}
		}
		else if (GUIManager.instance.homeCity.mainAvatar != null && UnityEngine.Random.Range(0f, 1f) < xacSuatDiChuyen)
		{
			GUIManager.instance.homeCity.mainAvatar.MoveRandomTarget();
		}
	}

	public void startAutoBatCoc()
	{
		int maxLuotBatCocByVip = ConfigManager.GetMaxLuotBatCocByVip(GameManager.instance.m_GameClient.UserInfo.Gamer.Vip);
		int luotBatCoc = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.LuotBatCoc;
		if (luotBatCoc < maxLuotBatCocByVip)
		{
			if (!GUIManager.instance.isAutoBatCoc || GUIManager.instance.homeCity == null)
			{
				return;
			}
			if (GUIManager.instance.homeCity.otherPlayers.Count > 0)
			{
				Dictionary<int, GameObject> dictionary = new Dictionary<int, GameObject>();
				for (int i = 0; i < GUIManager.instance.homeCity.otherPlayers.Count; i++)
				{
					GameObject gameObject = GUIManager.instance.homeCity.otherPlayers.Values.ElementAt(i);
					PlayerMovement component = gameObject.GetComponent<PlayerMovement>();
					if (!component.IsOnline)
					{
						dictionary.Add(i, gameObject);
					}
				}
				if (dictionary.Count > 0)
				{
					int index = UnityEngine.Random.Range(0, dictionary.Count - 1);
					currOffUserSelected = GUIManager.instance.homeCity.otherPlayers.Values.ElementAt(dictionary.Keys.ElementAt(index));
					currOffUserID = GUIManager.instance.homeCity.otherPlayers.Keys.ElementAt(dictionary.Keys.ElementAt(index));
					if (currOffUserSelected == null)
					{
						getListOtherPlayer();
					}
				}
				else
				{
					getListOtherPlayer();
				}
			}
			else
			{
				getListOtherPlayer();
			}
		}
		else
		{
			GUIManager.instance.isAutoBatCoc = false;
			currOffUserID = -1;
			currOffUserSelected = null;
			MessagePopup.Create(Localization.instance.Get("HetLuotBatCocLabel"));
		}
	}

	public void getListOtherPlayer()
	{
		GameManager.instance.m_GameClient.RequestGetListOtherPlayer(GameManager.instance.m_GameClient.UserInfo.Gamer.Level, true);
		CoroutineManager.WaitForSeconds(0.5f, startAutoBatCoc);
	}

	public void goToOfflineUser(GameObject go)
	{
		if (!(GUIManager.instance.homeCity == null) && !(GUIManager.instance.homeCity.mainAvatar == null))
		{
			if (GUIManager.instance.homeCity.mainAvatar != null)
			{
				GUIManager.instance.homeCity.mainAvatar.MoveTo(go.transform.position);
			}
			float x = go.transform.position.x;
			if (GUIManager.instance.homeCity.mainAvatar.transform.position.x < x + 0.5f && GUIManager.instance.homeCity.mainAvatar.transform.position.x > x - 0.5f && currOffUserID > 0)
			{
				GameManager.instance.m_GameClient.RequestBatCoc(currOffUserID);
				currOffUserSelected = null;
				waittingRequestBatCoc = true;
			}
		}
	}

	public ButtonSuKienThanhChinh addNewEventButton(ButtonSuKienThanhChinh.SuKienType type)
	{
		ButtonSuKienThanhChinh component = ((GameObject)UnityEngine.Object.Instantiate(buttonEventPrefab)).GetComponent<ButtonSuKienThanhChinh>();
		listNewEventButton.Add(component);
		component.transform.parent = groupButton.transform;
		component.transform.localScale = new Vector3(1f, 1f, 1f);
		component.transform.localPosition = new Vector3(itemPos.x, itemPos.y + (float)(listNewEventButton.Count - 1) * spaceButton, itemPos.z);
		component.setTitle(type);
		return component;
	}

	public void JoinDanhNienThu(NienThuData nienthu)
	{
		if (DiemNienThu != null)
		{
			DiemNienThu.text = string.Format(Localization.instance.Get("DiemNienThuLabel"), nienthu.TotalScore);
		}
		if (GUIManager.instance.homeCity != null)
		{
			GUIManager.instance.homeCity.InstantiateNienThuAvatar(nienthu);
		}
		if (ThoatNienThuBtn != null)
		{
			ThoatNienThuBtn.SetActive(true);
		}
		if (groupButton != null)
		{
			groupButton.SetActive(false);
		}
	}

	public void ThoatDanhNienThu()
	{
		if (GUIManager.instance.homeCity != null)
		{
			GUIManager.instance.homeCity.ThoatNienThu();
		}
		if (ThoatNienThuBtn != null)
		{
			ThoatNienThuBtn.SetActive(false);
		}
		if (groupButton != null)
		{
			groupButton.SetActive(true);
		}
	}

	public void PlayEffectNienThu()
	{
		if (NienThuHitEffect != null)
		{
			NienThuHitEffect.Play();
		}
	}

	public void OpenHoatDongHangNgay()
	{
		if (GameManager.instance.m_GameClient.isIpV6)
		{
			MessagePopup.Create(Localization.instance.Get("ErrorIPV6Mess"));
			return;
		}
		string value = "HoatDong;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		ThoatDanhNienThu();
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHoatDongHangNgay);
	}

	public void OnBtnTestProudnet()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenThangCapChienHon);
	}
}

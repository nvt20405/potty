using System;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLienMinhMain : ScreenBase
{
	private const float minScrollValue = 250f;

	public LuaTraiLienMinh LuaTrai;

	public GameObject HUDPanel;

	public List<PlayerLienMinhInfo> listPlayerLienMinhInfo = new List<PlayerLienMinhInfo>();

	public GameObject luaTraiGrp;

	public UILabel giaThoiLuaVang;

	public UILabel giaThoiLuaBac;

	public UILabel btnThoiLuaBacLabel;

	public UILabel btnThoiLuaVangLabel;

	public UILabel heSoExpLabel;

	public UILabel thoiGianLabel;

	public GameObject ThongBaoGrp;

	public GameObject LienMinhRoot;

	public GameObject ItemRoot;

	private bool enableThoiLuaBac;

	private bool enableThoiLuaVang;

	private bool isLienMinh;

	public UILabel ThongBaoLienMinh;

	public GameObject SuaThongBaoBtn;

	public GameObject chatItemPerfab;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private UIDraggablePanel dragPanelLM;

	private UIPanel panelLM;

	private List<GamerChatItem> ChatItemList = new List<GamerChatItem>();

	private List<GamerChatItem> ChatLienMinhList = new List<GamerChatItem>();

	private Vector3 itemPos;

	private Vector3 itemOffset;

	private Vector3 itemPosLM;

	private Vector3 itemOffsetLM;

	private int maxItemChat = 50;

	private float timeCount = 0.2f;

	private float timeGetExpCountDown;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>(true);
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
		dragPanel = ItemRoot.GetComponent<UIDraggablePanel>();
		panel = ItemRoot.GetComponent<UIPanel>();
		dragPanelLM = LienMinhRoot.GetComponent<UIDraggablePanel>();
		panelLM = LienMinhRoot.GetComponent<UIPanel>();
	}

	public void getListChatItem()
	{
		ChatInfoRequest chatInfoRequest = new ChatInfoRequest();
		chatInfoRequest.ID = 0;
		GameManager.instance.m_GameClient.RequestChatInfo(chatInfoRequest);
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh != null)
		{
			ChatLienMinhInfoRequest chatLienMinhInfoRequest = new ChatLienMinhInfoRequest();
			chatLienMinhInfoRequest.lienminhID = GameManager.instance.m_GameClient.UserInfo.LienMinh.ID;
			GameManager.instance.m_GameClient.RequestChatLienMinhInfo(chatLienMinhInfoRequest);
		}
	}

	public void displayListChatItem(ChatInfo chatInfo)
	{
		bool activeSelf = ItemRoot.activeSelf;
		bool activeSelf2 = luaTraiGrp.activeSelf;
		luaTraiGrp.SetActive(true);
		ItemRoot.SetActive(true);
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ChatItemList.Clear();
		itemPos = new Vector3(0f, 120f, 0f);
		itemOffset = new Vector3(0f, -120f, 0f);
		if (chatInfo.listChat != null && chatInfo.listChat.Count > 0)
		{
			for (int i = 0; i < chatInfo.listChat.Count; i++)
			{
				ChatItem chatData = chatInfo.listChat[i];
				GamerChatItem component = ((GameObject)UnityEngine.Object.Instantiate(chatItemPerfab)).GetComponent<GamerChatItem>();
				component.transform.parent = ItemRoot.transform;
				component.SetChatData(chatData);
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				itemOffset = new Vector3(0f, 0f - (component.spBackground.transform.localScale.y + 30f), 0f);
				itemPos += itemOffset;
				ChatItemList.Add(component);
			}
			if (dragPanel.gameObject.activeInHierarchy)
			{
				dragPanel.ResetPosition();
			}
		}
		ItemRoot.SetActive(activeSelf);
		luaTraiGrp.SetActive(activeSelf2);
	}

	public void displayListChatLienMinhItem(ChatInfo chatInfo)
	{
		bool activeSelf = LienMinhRoot.activeSelf;
		bool activeSelf2 = luaTraiGrp.activeSelf;
		luaTraiGrp.SetActive(true);
		LienMinhRoot.SetActive(true);
		foreach (Transform item in LienMinhRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ChatLienMinhList.Clear();
		itemPosLM = new Vector3(0f, 120f, 0f);
		itemOffsetLM = new Vector3(0f, -120f, 0f);
		if (chatInfo.listChat != null && chatInfo.listChat.Count > 0)
		{
			for (int i = 0; i < chatInfo.listChat.Count; i++)
			{
				ChatItem chatData = chatInfo.listChat[i];
				GamerChatItem component = ((GameObject)UnityEngine.Object.Instantiate(chatItemPerfab)).GetComponent<GamerChatItem>();
				component.transform.parent = LienMinhRoot.transform;
				component.SetChatData(chatData);
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPosLM;
				itemOffsetLM = new Vector3(0f, 0f - (component.spBackground.transform.localScale.y + 30f), 0f);
				itemPosLM += itemOffsetLM;
				ChatLienMinhList.Add(component);
			}
			if (dragPanelLM.gameObject.activeInHierarchy)
			{
				dragPanelLM.ResetPosition();
			}
		}
		LienMinhRoot.SetActive(activeSelf);
		luaTraiGrp.SetActive(activeSelf2);
	}

	public void addItemToChatLienMinhList(ChatItem newItem)
	{
		if (ChatLienMinhList.Count >= maxItemChat)
		{
			GamerChatItem gamerChatItem = ChatLienMinhList[0];
			GamerChatItem gamerChatItem2 = ChatLienMinhList[ChatLienMinhList.Count - 1];
			ChatLienMinhList.RemoveAt(0);
			ChatLienMinhList.Add(gamerChatItem);
			itemOffsetLM = new Vector3(0f, 0f - (gamerChatItem2.spBackground.transform.localScale.y + 30f), 0f);
			itemPosLM = gamerChatItem2.transform.localPosition + itemOffsetLM;
			gamerChatItem.transform.localPosition = itemPosLM;
			gamerChatItem.SetChatData(newItem);
		}
		else
		{
			GamerChatItem component = ((GameObject)UnityEngine.Object.Instantiate(chatItemPerfab)).GetComponent<GamerChatItem>();
			component.transform.parent = LienMinhRoot.transform;
			component.SetChatData(newItem);
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = itemPosLM;
			itemOffsetLM = new Vector3(0f, 0f - (component.spBackground.transform.localScale.y + 30f), 0f);
			itemPosLM += itemOffsetLM;
			ChatLienMinhList.Add(component);
		}
		if (dragPanelLM.gameObject.activeInHierarchy && Utils.CalculateVerticalScrollValueInPixel(dragPanelLM) < 250f)
		{
			dragPanelLM.ResetPosition();
		}
	}

	public void addItemToChatList(ChatItem newItem)
	{
		if (ChatItemList.Count >= maxItemChat)
		{
			GamerChatItem gamerChatItem = ChatItemList[0];
			GamerChatItem gamerChatItem2 = ChatItemList[ChatItemList.Count - 1];
			ChatItemList.RemoveAt(0);
			ChatItemList.Add(gamerChatItem);
			itemOffset = new Vector3(0f, 0f - (gamerChatItem2.spBackground.transform.localScale.y + 30f), 0f);
			itemPos = gamerChatItem2.transform.localPosition + itemOffset;
			gamerChatItem.transform.localPosition = itemPos;
			gamerChatItem.SetChatData(newItem);
		}
		else
		{
			GamerChatItem component = ((GameObject)UnityEngine.Object.Instantiate(chatItemPerfab)).GetComponent<GamerChatItem>();
			component.transform.parent = ItemRoot.transform;
			component.SetChatData(newItem);
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = itemPos;
			itemOffset = new Vector3(0f, 0f - (component.spBackground.transform.localScale.y + 30f), 0f);
			itemPos += itemOffset;
			ChatItemList.Add(component);
		}
		if (dragPanel.gameObject.activeInHierarchy && Utils.CalculateVerticalScrollValueInPixel(dragPanel) < 250f)
		{
			dragPanel.ResetPosition();
		}
	}

	public void btnChat_OnClick(GameObject go)
	{
		if (isLienMinh)
		{
			PopupChat.Create(ChatType.BANG_HOI);
		}
		else
		{
			PopupChat.Create(ChatType.THE_GIOI);
		}
	}

	private void OnActivateBangHoi(bool isActive)
	{
		if (isActive)
		{
			isLienMinh = true;
		}
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh != null)
		{
			LienMinhRoot.SetActive(isActive);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ChuaGiaNhapLienMinh"));
		}
	}

	private void OnActivateTheGioi(bool isActive)
	{
		ItemRoot.SetActive(isActive);
		if (isActive)
		{
			isLienMinh = false;
		}
	}

	public PlayerLienMinhInfo CreateGUIPanel(int gid, Transform player3DPivot)
	{
		for (int i = 0; i < listPlayerLienMinhInfo.Count; i++)
		{
			if (listPlayerLienMinhInfo[i].ID == gid)
			{
				return listPlayerLienMinhInfo[i];
			}
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("prefabs/lienminh/PlayerLienMinhInfo"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		gameObject.transform.parent = HUDPanel.transform;
		gameObject.transform.localScale = Vector3.one;
		UIFollowTarget component = gameObject.GetComponent<UIFollowTarget>();
		component.target = player3DPivot;
		component.uiCamera = GUIManager.instance.cam2D;
		component.gameCamera = GUIManager.instance.lienMinh3D.cam;
		PlayerLienMinhInfo component2 = gameObject.GetComponent<PlayerLienMinhInfo>();
		component2.ID = gid;
		listPlayerLienMinhInfo.Add(component2);
		return component2;
	}

	public void PlayGetExp(int gid, int exp)
	{
		foreach (PlayerLienMinhInfo item in listPlayerLienMinhInfo)
		{
			if (item.ID == gid)
			{
				item.TangExp(exp);
			}
		}
	}

	public void OnEnable()
	{
		if (GUIManager.instance.lienMinh3D != null)
		{
			child3Dscreen = GUIManager.instance.lienMinh3D.gameObject;
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.ID != GameManager.instance.m_GameClient.UserInfo.LienMinh.MinhChuID)
		{
			SuaThongBaoBtn.SetActive(false);
		}
		else
		{
			SuaThongBaoBtn.SetActive(true);
		}
		ThongBaoLienMinh.text = GameManager.instance.m_GameClient.UserInfo.LienMinh.ThongBao;
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(2);
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = false;
		}
		listPlayerLienMinhInfo.Clear();
		getListChatItem();
		GUIManager.instance.lienMinh3D.RefreshThanhVien();
		if (CheckIsTrongGioLuaTrai())
		{
			GameManager.instance.m_GameClient.RequestThamGiaLuaTrai(true);
		}
		Utils.SetLightMaps("Lightmap/LIEN_MINH/");
	}

	public override void OnDeactive()
	{
		if (CheckIsTrongGioLuaTrai())
		{
			GameManager.instance.m_GameClient.RequestRoiDiLuaTrai();
		}
		GUIManager.ShowGadgets(6);
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = true;
		}
		foreach (PlayerLienMinhInfo item in listPlayerLienMinhInfo)
		{
			UnityEngine.Object.Destroy(item.gameObject);
		}
		listPlayerLienMinhInfo.Clear();
		base.OnDeactive();
	}

	private void Update()
	{
		timeCount += Time.deltaTime;
		timeGetExpCountDown -= Time.deltaTime;
		if (!(timeCount > 0.5f))
		{
			return;
		}
		if (LuaTrai != null && CheckIsTrongGioLuaTrai())
		{
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			luaTraiGrp.SetActive(true);
			ThongBaoGrp.SetActive(false);
			if (luaTraiGrp.gameObject.activeInHierarchy && LuaTrai != null && userInfo.LienMinh != null)
			{
				if (GameManager.instance.m_GameClient.ServerTime < LuaTrai.NextTimeThoiLuaFree)
				{
					TimeSpan timeSpan = LuaTrai.NextTimeThoiLuaFree - GameManager.instance.m_GameClient.ServerTime;
					btnThoiLuaBacLabel.text = string.Format(Localization.instance.Get("ChoThoiLuaBtnLabel"), Mathf.Max((int)timeSpan.TotalSeconds, 1));
					enableThoiLuaBac = false;
				}
				else
				{
					btnThoiLuaBacLabel.text = Localization.instance.Get("ThoiLuaBtnLabel");
					enableThoiLuaBac = true;
				}
				if (GameManager.instance.m_GameClient.ServerTime < LuaTrai.NextTimeThoiLuaKNB)
				{
					TimeSpan timeSpan2 = LuaTrai.NextTimeThoiLuaKNB - GameManager.instance.m_GameClient.ServerTime;
					btnThoiLuaVangLabel.text = string.Format(Localization.instance.Get("ChoThoiLuaBtnLabel"), Mathf.Max((int)timeSpan2.TotalSeconds, 1));
					enableThoiLuaVang = false;
				}
				else
				{
					btnThoiLuaVangLabel.text = Localization.instance.Get("ThoiLuaBtnLabel");
					enableThoiLuaVang = true;
				}
				DateTime dateTime = userInfo.LienMinh.DotLuaTraiTime.AddMinutes(ConfigManager.instance.OtherConfig.ThoiGianDotLuaTrai);
				TimeSpan timeSpan3 = dateTime - GameManager.instance.m_GameClient.ServerTime;
				if (timeSpan3.TotalMinutes > 0.0)
				{
					thoiGianLabel.text = string.Format(Localization.instance.Get("ThoiGianLuaTrai"), timeSpan3.Minutes, timeSpan3.Seconds);
				}
			}
			if (timeGetExpCountDown < 0f)
			{
				GameManager.instance.m_GameClient.RequestGetExpLuaTrai();
				timeGetExpCountDown = ConfigManager.instance.OtherConfig.ThoiGianGetExpLuaTrai;
			}
			else if (LuaTrai.NextTimeGetExp < GameManager.instance.m_GameClient.ServerTime && timeGetExpCountDown < (float)ConfigManager.instance.OtherConfig.ThoiGianGetExpLuaTrai)
			{
				GameManager.instance.m_GameClient.RequestGetExpLuaTrai();
				timeGetExpCountDown = ConfigManager.instance.OtherConfig.ThoiGianGetExpLuaTrai + 2;
			}
		}
		else
		{
			luaTraiGrp.SetActive(false);
			ThongBaoGrp.SetActive(true);
			enableThoiLuaBac = false;
			enableThoiLuaVang = false;
			if (GUIManager.instance.lienMinh3D != null)
			{
				GUIManager.instance.lienMinh3D.TatLuaTrai();
			}
		}
		timeCount = 0f;
	}

	public void UpdateLuaTrai(LuaTraiLienMinh luaTrai)
	{
		LuaTrai = luaTrai;
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (luaTrai != null && userInfo.LienMinh != null && userInfo.LienMinh.DotLuaTraiCount > 0)
		{
			TimeSpan timeSpan = GameManager.instance.m_GameClient.ServerTime - userInfo.LienMinh.DotLuaTraiTime;
			if (timeSpan.TotalMinutes > 0.0 && timeSpan.TotalMinutes < (double)ConfigManager.instance.OtherConfig.ThoiGianDotLuaTrai)
			{
				luaTraiGrp.gameObject.SetActive(true);
				if (GUIManager.instance.lienMinh3D != null)
				{
					GUIManager.instance.lienMinh3D.LoadLuaTrai();
				}
				giaThoiLuaBac.text = LuaTrai.CostThoiLuaBac.ToString();
				giaThoiLuaVang.text = LuaTrai.CostThoiLuaVang.ToString();
				heSoExpLabel.text = string.Format(Localization.instance.Get("HeSoExpThoiLua"), luaTrai.HeSoExp);
				if (GameManager.instance.m_GameClient.ServerTime < LuaTrai.NextTimeThoiLuaFree)
				{
					TimeSpan timeSpan2 = LuaTrai.NextTimeThoiLuaFree - GameManager.instance.m_GameClient.ServerTime;
					btnThoiLuaBacLabel.text = string.Format(Localization.instance.Get("ChoThoiLuaBtnLabel"), Mathf.Max((int)timeSpan2.TotalSeconds, 1));
					enableThoiLuaBac = false;
				}
				else
				{
					btnThoiLuaBacLabel.text = Localization.instance.Get("ThoiLuaBtnLabel");
					enableThoiLuaBac = true;
				}
				if (GameManager.instance.m_GameClient.ServerTime < LuaTrai.NextTimeThoiLuaKNB)
				{
					TimeSpan timeSpan3 = LuaTrai.NextTimeThoiLuaKNB - GameManager.instance.m_GameClient.ServerTime;
					btnThoiLuaVangLabel.text = string.Format(Localization.instance.Get("ChoThoiLuaBtnLabel"), Mathf.Max((int)timeSpan3.TotalSeconds, 1));
					enableThoiLuaVang = false;
				}
				else
				{
					btnThoiLuaVangLabel.text = Localization.instance.Get("ThoiLuaBtnLabel");
					enableThoiLuaVang = true;
				}
			}
			else
			{
				luaTraiGrp.gameObject.SetActive(false);
				enableThoiLuaBac = false;
				enableThoiLuaVang = false;
				if (GUIManager.instance.lienMinh3D != null)
				{
					GUIManager.instance.lienMinh3D.TatLuaTrai();
				}
			}
		}
		else
		{
			luaTraiGrp.gameObject.SetActive(false);
			enableThoiLuaBac = false;
			enableThoiLuaVang = false;
			if (GUIManager.instance.lienMinh3D != null)
			{
				GUIManager.instance.lienMinh3D.TatLuaTrai();
			}
		}
	}

	private bool CheckIsTrongGioLuaTrai()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo.LienMinh != null && userInfo.LienMinh.DotLuaTraiCount > 0)
		{
			TimeSpan timeSpan = GameManager.instance.m_GameClient.ServerTime - userInfo.LienMinh.DotLuaTraiTime;
			if (timeSpan.TotalMinutes > 0.0 && timeSpan.TotalMinutes < (double)ConfigManager.instance.OtherConfig.ThoiGianDotLuaTrai)
			{
				return true;
			}
		}
		return false;
	}

	private void OnThoiLuaVangBtnClick()
	{
		if (LuaTrai != null && enableThoiLuaVang && GameManager.instance.m_GameClient.checkKNB(LuaTrai.CostThoiLuaVang))
		{
			GameManager.instance.m_GameClient.RequestThoiLuaLienMinh(true);
		}
	}

	private void OnThoiLuaBacBtnClick()
	{
		if (LuaTrai != null && enableThoiLuaBac)
		{
			GameManager.instance.m_GameClient.RequestThoiLuaLienMinh(false);
		}
	}

	private void OnSuaThongBao()
	{
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.ID == GameManager.instance.m_GameClient.UserInfo.LienMinh.MinhChuID)
		{
			PopupThongBaoLienMinh.Create();
		}
	}

	public void GoToLanhDia()
	{
		string value = "LanhDia;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level < ConfigManager.instance.SonMonConfig.LevelReq)
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("ChuaDuLevel"), ConfigManager.instance.SonMonConfig.LevelReq));
			return;
		}
		if (GameManager.instance.m_GameClient.UserInfo.LanhDiaData == null || (GameManager.instance.m_GameClient.ServerTime - GameManager.instance.m_GameClient.UserInfo.LanhDiaData.DataTime).TotalSeconds > 10.0)
		{
			GameManager.instance.m_GameClient.RequestGetLanhDiaInfo();
			return;
		}
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenLanhDiaMap);
		((ScreenLanhDiaMap)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenLanhDiaMap)).Set();
		GadgetPanelBottom gadgetPanelBottom = GUIManager.instance.gadgetPanelBottom;
		gadgetPanelBottom.bangHoiGrp.SetActive(false);
	}

	public void GoToNienThu()
	{
		string value = "NienThu;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
		}
		else
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenLienMinhNienThu);
		}
	}

	public void OnHelpBtn()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(8, 3);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
		EGDebug.Log("btnHelp_OnClick");
	}
}

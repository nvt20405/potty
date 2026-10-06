using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenThanhChien : ScreenBase
{
	private const float minScrollValue = 250f;

	public ScreenThanhChien3D ThanhChienObj;

	public int ThanhIdx;

	public UILabel DoBenCongThanh;

	public UILabel TenThanh;

	public UILabel TenBangChanGiu;

	public UILabel TopLienMinh;

	public UILabel Damage;

	public UILabel thoiGianLabel;

	public UILabel cong1BtnLabel;

	public UILabel cong2BtnLabel;

	public UILabel cong3BtnLabel;

	public GameObject damageGrp;

	public GameObject chatGrp;

	public NguoiChoiBangChien playerInfo;

	private DateTime timeEnd = DateTime.Now;

	public GameObject LienMinhRoot;

	public GameObject ItemRoot;

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

	private bool isLienMinh;

	public bool IsThuThanh()
	{
		if (playerInfo != null && playerInfo.Def > 0)
		{
			return true;
		}
		return false;
	}

	private IEnumerator DelayPlayBattle(float seconds, BangChienCongThanhResponse response)
	{
		yield return new WaitForSeconds(seconds);
		PlayBattle(response);
	}

	public IEnumerator EnemyRunOnWall(BangChienCongThanhResponse response)
	{
		if (response.Enemy != null)
		{
			BangChienPlayer enemyP = ThanhChienObj.GetEnemyPlayer(response.Enemy);
			Vector3 pos = enemyP.gameObject.transform.localPosition;
			if (response.Gate == 1)
			{
				pos = ThanhChienObj.Cong1Collider.transform.localPosition;
			}
			else if (response.Gate == 2)
			{
				pos = ThanhChienObj.Cong2Collider.transform.localPosition;
			}
			else if (response.Gate == 3)
			{
				pos = ThanhChienObj.Cong3Collider.transform.localPosition;
			}
			yield return null;
			if (enemyP == null)
			{
				PlayBattle(response);
				yield break;
			}
			enemyP.AttackerRotateToWall();
			enemyP.LocalTeleport(pos);
			yield return StartCoroutine(DelayPlayBattle(1.2f, response));
		}
	}

	public void PlayBattle(BangChienCongThanhResponse response)
	{
		if (response.Battle != null)
		{
			playerInfo = response.Player;
			GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
			ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
			if (screenBattle != null)
			{
				screenBattle.Replay(response.Battle, "BM_CONG_THANH_CHIEN");
				screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenThanhChien;
				screenBattle.OnFinishReplay += OnFinishBattle;
			}
		}
		else if (ThanhChienObj != null && ThanhChienObj.mainPlayer != null)
		{
			ThanhChienObj.mainPlayer.playerInfo = response.Player;
			ThanhChienObj.mainPlayer.LocalTeleport(new Vector3(response.Player.X, response.Player.Y, response.Player.Z));
		}
		if (response.PhanThuong != null)
		{
			PopupDanhSachPhanThuong popupDanhSachPhanThuong = PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), string.Format(Localization.instance.Get("BangChienPhanThuongDesc"), response.PhanThuong.DiemCongHien), response.PhanThuong.PhanThuong);
			if (response.Battle != null)
			{
				popupDanhSachPhanThuong.gameObject.SetActive(false);
			}
			if (response.PhanThuong.PhanThuong.UpdateUserInfo != null)
			{
				GameManager.instance.m_GameClient.UserInfo.UpdateInfo(response.PhanThuong.PhanThuong.UpdateUserInfo);
			}
		}
	}

	public void OnFinishBattle()
	{
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		if (screenBattle != null)
		{
			screenBattle.OnFinishReplay -= OnFinishBattle;
		}
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.instance.gameObject.SetActive(true);
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		SpawnScreen3D();
		GUIManager.ShowGadgets(0);
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = false;
		}
		getListChatItem();
		Utils.SetLightMaps("Lightmap/Cong_Thanh_Chien/");
	}

	private void Awake()
	{
		dragPanel = ItemRoot.GetComponent<UIDraggablePanel>();
		panel = ItemRoot.GetComponent<UIPanel>();
		dragPanelLM = LienMinhRoot.GetComponent<UIDraggablePanel>();
		panelLM = LienMinhRoot.GetComponent<UIPanel>();
	}

	private void OnActivateChat(bool isActive)
	{
		chatGrp.SetActive(isActive);
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
		bool activeSelf2 = chatGrp.activeSelf;
		chatGrp.SetActive(true);
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
		chatGrp.SetActive(activeSelf2);
	}

	public void displayListChatLienMinhItem(ChatInfo chatInfo)
	{
		bool activeSelf = LienMinhRoot.activeSelf;
		bool activeSelf2 = chatGrp.activeSelf;
		chatGrp.SetActive(true);
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
		chatGrp.SetActive(activeSelf2);
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

	public override void OnDeactive()
	{
		if (ThanhChienObj != null)
		{
			ThanhChienObj.gameObject.SetActive(false);
			UnityEngine.Object.Destroy(ThanhChienObj.gameObject);
			ThanhChienObj = null;
		}
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = true;
		}
		base.OnDeactive();
	}

	private void Update()
	{
		if (timeEnd > GameManager.instance.m_GameClient.ServerTime)
		{
			TimeSpan timeSpan = timeEnd - GameManager.instance.m_GameClient.ServerTime;
			thoiGianLabel.text = string.Format(Localization.instance.Get("ThoiGianLuaTrai"), (int)timeSpan.TotalMinutes, timeSpan.Seconds);
		}
		else
		{
			thoiGianLabel.text = string.Format(Localization.instance.Get("ThoiGianLuaTrai"), 0, 0);
		}
	}

	public void SyncWithNetworkData(BangChienResponse response)
	{
		timeEnd = response.TimeBatDau + ConfigManager.instance.OtherConfig.BangChien.GetThoiGianDienRaBangChien();
	}

	public void SyncWithNetworkData(BangChienPhanThuong response)
	{
		TenThanh.text = ConfigManager.instance.OtherConfig.BangChien.ListThanhChien[response.Thanh.ThanhIdx];
		DoBenCongThanh.text = string.Format("{0}: {1}\n{2}: {3}\n{4}: {5}", Localization.instance.Get("BangChienCong1Label"), response.Thanh.CongThanh1, Localization.instance.Get("BangChienCong2Label"), response.Thanh.CongThanh2, Localization.instance.Get("BangChienCong3Label"), response.Thanh.CongThanh3);
		TenBangChanGiu.text = ((!string.IsNullOrEmpty(response.Thanh.LMName)) ? string.Format(Localization.instance.Get("BangChienBangThuThanh"), response.Thanh.LienMinhServerID, response.Thanh.LMName) : Localization.instance.Get("BangChienThanhTrong"));
		TopLienMinh.text = string.Format(Localization.instance.Get("BangChienTopLienMinh"), (response.LMTop1 == null || string.IsNullOrEmpty(response.LMTop1.LienMinhName)) ? "___" : response.LMTop1.LienMinhName, (response.LMTop1 != null) ? response.LMTop1.DoBen : 0, (response.LMTop2 == null || string.IsNullOrEmpty(response.LMTop2.LienMinhName)) ? "___" : response.LMTop2.LienMinhName, (response.LMTop2 != null) ? response.LMTop2.DoBen : 0, (response.LMTop3 == null || string.IsNullOrEmpty(response.LMTop3.LienMinhName)) ? "___" : response.LMTop3.LienMinhName, (response.LMTop3 != null) ? response.LMTop3.DoBen : 0);
		if (response.Player.Def > 0)
		{
			damageGrp.SetActive(false);
			cong1BtnLabel.text = string.Format(Localization.instance.Get("BangChienThuBtnLabel"), Localization.instance.Get("BangChienCong1Label"));
			cong2BtnLabel.text = string.Format(Localization.instance.Get("BangChienThuBtnLabel"), Localization.instance.Get("BangChienCong2Label"));
			cong3BtnLabel.text = string.Format(Localization.instance.Get("BangChienThuBtnLabel"), Localization.instance.Get("BangChienCong3Label"));
			TenBangChanGiu.text = string.Format(Localization.instance.Get("BangChienBuffMau"), response.Thanh.DefBuffMau);
		}
		else
		{
			damageGrp.SetActive(true);
			cong1BtnLabel.text = string.Format(Localization.instance.Get("BangChienCongBtnLabel"), Localization.instance.Get("BangChienCong1Label"));
			cong2BtnLabel.text = string.Format(Localization.instance.Get("BangChienCongBtnLabel"), Localization.instance.Get("BangChienCong2Label"));
			cong3BtnLabel.text = string.Format(Localization.instance.Get("BangChienCongBtnLabel"), Localization.instance.Get("BangChienCong3Label"));
			Damage.text = string.Format(Localization.instance.Get("BangChienTongDamage"), response.LMDamage);
		}
	}

	public void SyncWithNetworkData(BangChienListPlayerResponse response)
	{
		TenThanh.text = ConfigManager.instance.OtherConfig.BangChien.ListThanhChien[response.Thanh.ThanhIdx];
		DoBenCongThanh.text = string.Format("{0}: {1}\n{2}: {3}\n{4}: {5}", Localization.instance.Get("BangChienCong1Label"), response.Thanh.CongThanh1, Localization.instance.Get("BangChienCong2Label"), response.Thanh.CongThanh2, Localization.instance.Get("BangChienCong3Label"), response.Thanh.CongThanh3);
		TenBangChanGiu.text = ((!string.IsNullOrEmpty(response.Thanh.LMName)) ? string.Format(Localization.instance.Get("BangChienBangThuThanh"), response.Thanh.LienMinhServerID, response.Thanh.LMName) : Localization.instance.Get("BangChienThanhTrong"));
		TopLienMinh.text = string.Format(Localization.instance.Get("BangChienTopLienMinh"), (response.LMTop1 == null || string.IsNullOrEmpty(response.LMTop1.LienMinhName)) ? "___" : response.LMTop1.LienMinhName, (response.LMTop1 != null) ? response.LMTop1.DoBen : 0, (response.LMTop2 == null || string.IsNullOrEmpty(response.LMTop2.LienMinhName)) ? "___" : response.LMTop2.LienMinhName, (response.LMTop2 != null) ? response.LMTop2.DoBen : 0, (response.LMTop3 == null || string.IsNullOrEmpty(response.LMTop3.LienMinhName)) ? "___" : response.LMTop3.LienMinhName, (response.LMTop3 != null) ? response.LMTop3.DoBen : 0);
		if (playerInfo != null && playerInfo.Def > 0)
		{
			damageGrp.SetActive(false);
			cong1BtnLabel.text = string.Format(Localization.instance.Get("BangChienThuBtnLabel"), Localization.instance.Get("BangChienCong1Label"));
			cong2BtnLabel.text = string.Format(Localization.instance.Get("BangChienThuBtnLabel"), Localization.instance.Get("BangChienCong2Label"));
			cong3BtnLabel.text = string.Format(Localization.instance.Get("BangChienThuBtnLabel"), Localization.instance.Get("BangChienCong3Label"));
			TenBangChanGiu.text = string.Format(Localization.instance.Get("BangChienBuffMau"), response.Thanh.DefBuffMau);
		}
		else
		{
			damageGrp.SetActive(true);
			cong1BtnLabel.text = string.Format(Localization.instance.Get("BangChienCongBtnLabel"), Localization.instance.Get("BangChienCong1Label"));
			cong2BtnLabel.text = string.Format(Localization.instance.Get("BangChienCongBtnLabel"), Localization.instance.Get("BangChienCong2Label"));
			cong3BtnLabel.text = string.Format(Localization.instance.Get("BangChienCongBtnLabel"), Localization.instance.Get("BangChienCong3Label"));
			Damage.text = string.Format(Localization.instance.Get("BangChienTongDamage"), response.LMDamage);
		}
	}

	public void SyncWithNetworkData(BangChienVaoThanhResponse response)
	{
		TenThanh.text = ConfigManager.instance.OtherConfig.BangChien.ListThanhChien[response.ThanhChienObj.ThanhIdx];
		DoBenCongThanh.text = string.Format("{0}: {1}\n{2}: {3}\n{4}: {5}", Localization.instance.Get("BangChienCong1Label"), response.ThanhChienObj.CongThanh1, Localization.instance.Get("BangChienCong2Label"), response.ThanhChienObj.CongThanh2, Localization.instance.Get("BangChienCong3Label"), response.ThanhChienObj.CongThanh3);
		TenBangChanGiu.text = ((!string.IsNullOrEmpty(response.ThanhChienObj.LMName)) ? string.Format(Localization.instance.Get("BangChienBangThuThanh"), response.ThanhChienObj.LienMinhServerID, response.ThanhChienObj.LMName) : Localization.instance.Get("BangChienThanhTrong"));
		TopLienMinh.text = string.Format(Localization.instance.Get("BangChienTopLienMinh"), (response.LMTop1 == null || string.IsNullOrEmpty(response.LMTop1.LienMinhName)) ? "___" : response.LMTop1.LienMinhName, (response.LMTop1 != null) ? response.LMTop1.DoBen : 0, (response.LMTop2 == null || string.IsNullOrEmpty(response.LMTop2.LienMinhName)) ? "___" : response.LMTop2.LienMinhName, (response.LMTop2 != null) ? response.LMTop2.DoBen : 0, (response.LMTop3 == null || string.IsNullOrEmpty(response.LMTop3.LienMinhName)) ? "___" : response.LMTop3.LienMinhName, (response.LMTop3 != null) ? response.LMTop3.DoBen : 0);
		if (response.Player.Def > 0)
		{
			damageGrp.SetActive(false);
			cong1BtnLabel.text = string.Format(Localization.instance.Get("BangChienThuBtnLabel"), Localization.instance.Get("BangChienCong1Label"));
			cong2BtnLabel.text = string.Format(Localization.instance.Get("BangChienThuBtnLabel"), Localization.instance.Get("BangChienCong2Label"));
			cong3BtnLabel.text = string.Format(Localization.instance.Get("BangChienThuBtnLabel"), Localization.instance.Get("BangChienCong3Label"));
			TenBangChanGiu.text = string.Format(Localization.instance.Get("BangChienBuffMau"), response.ThanhChienObj.DefBuffMau);
		}
		else
		{
			damageGrp.SetActive(true);
			cong1BtnLabel.text = string.Format(Localization.instance.Get("BangChienCongBtnLabel"), Localization.instance.Get("BangChienCong1Label"));
			cong2BtnLabel.text = string.Format(Localization.instance.Get("BangChienCongBtnLabel"), Localization.instance.Get("BangChienCong2Label"));
			cong3BtnLabel.text = string.Format(Localization.instance.Get("BangChienCongBtnLabel"), Localization.instance.Get("BangChienCong3Label"));
			Damage.text = string.Format(Localization.instance.Get("BangChienTongDamage"), response.LMPlayer.DoBen);
		}
		playerInfo = response.Player;
	}

	public void SyncWithNetworkData(BangChienCongThanhResponse response)
	{
		TenThanh.text = ConfigManager.instance.OtherConfig.BangChien.ListThanhChien[response.Thanh.ThanhIdx];
		DoBenCongThanh.text = string.Format("{0}: {1}\n{2}: {3}\n{4}: {5}", Localization.instance.Get("BangChienCong1Label"), response.Thanh.CongThanh1, Localization.instance.Get("BangChienCong2Label"), response.Thanh.CongThanh2, Localization.instance.Get("BangChienCong3Label"), response.Thanh.CongThanh3);
		TenBangChanGiu.text = ((!string.IsNullOrEmpty(response.Thanh.LMName)) ? string.Format(Localization.instance.Get("BangChienBangThuThanh"), response.Thanh.LienMinhServerID, response.Thanh.LMName) : Localization.instance.Get("BangChienThanhTrong"));
		TopLienMinh.text = string.Format(Localization.instance.Get("BangChienTopLienMinh"), (response.LMTop1 == null || string.IsNullOrEmpty(response.LMTop1.LienMinhName)) ? "___" : response.LMTop1.LienMinhName, (response.LMTop1 != null) ? response.LMTop1.DoBen : 0, (response.LMTop2 == null || string.IsNullOrEmpty(response.LMTop2.LienMinhName)) ? "___" : response.LMTop2.LienMinhName, (response.LMTop2 != null) ? response.LMTop2.DoBen : 0, (response.LMTop3 == null || string.IsNullOrEmpty(response.LMTop3.LienMinhName)) ? "___" : response.LMTop3.LienMinhName, (response.LMTop3 != null) ? response.LMTop3.DoBen : 0);
		if (response.Player.Def > 0)
		{
			damageGrp.SetActive(false);
			TenBangChanGiu.text = string.Format(Localization.instance.Get("BangChienBuffMau"), response.Thanh.DefBuffMau);
		}
		else
		{
			damageGrp.SetActive(true);
			Damage.text = string.Format(Localization.instance.Get("BangChienTongDamage"), response.LMPlayer.DoBen);
		}
		playerInfo = response.Player;
		ThanhChienObj.mainPlayer.playerInfo = response.Player;
	}

	private void SpawnScreen3D()
	{
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("GUI/Screens3D/ScreenThanhChien3D"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		gameObject.transform.parent = GUIManager.instance.ScreenContainer3D;
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localPosition = Vector3.zero;
		child3Dscreen = gameObject;
		ThanhChienObj = gameObject.GetComponent<ScreenThanhChien3D>();
		if (playerInfo != null)
		{
			ThanhChienObj.SetActiveCam(playerInfo.Def <= 0);
			ThanhChienObj.SpawnPlayerAvatar(playerInfo);
		}
	}

	private void OnBackBtnClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenBangChien);
	}

	private void OnCong1BtnClick()
	{
		if (ThanhChienObj.mainPlayer.playerInfo != null && ThanhChienObj.mainPlayer.playerInfo.Def <= 0)
		{
			NguoiChoiBangChien nguoiChoiBangChien = ThanhChienObj.mainPlayer.playerInfo;
			if (nguoiChoiBangChien != null && nguoiChoiBangChien.TimeHoiSinh < GameManager.instance.m_GameClient.ServerTime)
			{
				Vector3 position = ThanhChienObj.Cong1Collider.transform.position;
				position = new Vector3(position.x + (float)UnityEngine.Random.Range(-4, 4), position.y, position.z);
				ThanhChienObj.mainPlayer.MoveToNewDes(position);
			}
		}
		else
		{
			NguoiChoiBangChien nguoiChoiBangChien2 = ThanhChienObj.mainPlayer.playerInfo;
			if (nguoiChoiBangChien2 != null && nguoiChoiBangChien2.TimeHoiSinh < GameManager.instance.m_GameClient.ServerTime)
			{
				Vector3 position2 = ThanhChienObj.Cong1DefCollider.transform.position;
				position2 = new Vector3(position2.x + (float)UnityEngine.Random.Range(-4, 4), position2.y, position2.z + UnityEngine.Random.Range(-1.6f, 1.6f));
				ThanhChienObj.mainPlayer.MoveToNewDes(position2);
			}
		}
	}

	private void OnCong2BtnClick()
	{
		if (ThanhChienObj.mainPlayer.playerInfo != null && ThanhChienObj.mainPlayer.playerInfo.Def <= 0)
		{
			NguoiChoiBangChien nguoiChoiBangChien = ThanhChienObj.mainPlayer.playerInfo;
			if (nguoiChoiBangChien != null && nguoiChoiBangChien.TimeHoiSinh < GameManager.instance.m_GameClient.ServerTime)
			{
				Vector3 position = ThanhChienObj.Cong2Collider.transform.position;
				position = new Vector3(position.x + (float)UnityEngine.Random.Range(-4, 4), position.y, position.z);
				ThanhChienObj.mainPlayer.MoveToNewDes(position);
			}
		}
		else
		{
			NguoiChoiBangChien nguoiChoiBangChien2 = ThanhChienObj.mainPlayer.playerInfo;
			if (nguoiChoiBangChien2 != null && nguoiChoiBangChien2.TimeHoiSinh < GameManager.instance.m_GameClient.ServerTime)
			{
				Vector3 position2 = ThanhChienObj.Cong2DefCollider.transform.position;
				position2 = new Vector3(position2.x + (float)UnityEngine.Random.Range(-4, 4), position2.y, position2.z + UnityEngine.Random.Range(-1.6f, 1.6f));
				ThanhChienObj.mainPlayer.MoveToNewDes(position2);
			}
		}
	}

	private void OnCong3BtnClick()
	{
		if (ThanhChienObj.mainPlayer.playerInfo != null && ThanhChienObj.mainPlayer.playerInfo.Def <= 0)
		{
			NguoiChoiBangChien nguoiChoiBangChien = ThanhChienObj.mainPlayer.playerInfo;
			if (nguoiChoiBangChien != null && nguoiChoiBangChien.TimeHoiSinh < GameManager.instance.m_GameClient.ServerTime)
			{
				Vector3 position = ThanhChienObj.Cong3Collider.transform.position;
				position = new Vector3(position.x + (float)UnityEngine.Random.Range(-4, 4), position.y, position.z);
				ThanhChienObj.mainPlayer.MoveToNewDes(position);
			}
		}
		else
		{
			NguoiChoiBangChien nguoiChoiBangChien2 = ThanhChienObj.mainPlayer.playerInfo;
			if (nguoiChoiBangChien2 != null && nguoiChoiBangChien2.TimeHoiSinh < GameManager.instance.m_GameClient.ServerTime)
			{
				Vector3 position2 = ThanhChienObj.Cong3DefCollider.transform.position;
				position2 = new Vector3(position2.x + (float)UnityEngine.Random.Range(-4, 4), position2.y, position2.z + UnityEngine.Random.Range(-1.6f, 1.6f));
				ThanhChienObj.mainPlayer.MoveToNewDes(position2);
			}
		}
	}
}

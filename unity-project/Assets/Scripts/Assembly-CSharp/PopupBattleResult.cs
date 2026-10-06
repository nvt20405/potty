using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupBattleResult : MonoBehaviour
{
	public static PopupBattleResult instance;

	public GameObject thang;

	public GameObject daiThang;

	public GameObject thua;

	public UISprite thangBkg;

	public GameObject groupInfoThua;

	public GameObject star1;

	public GameObject star2;

	public GameObject star3;

	public UILabel expReward;

	public UILabel bacReward;

	public DeTuBattleResult[] deTuList = new DeTuBattleResult[8];

	public GameObject[] lotDeTuGrp = new GameObject[4];

	public EGGUIGrid gridDeTu;

	public EGGUIGrid gridLot;

	private List<UserInfo.HeroData> listDeTu;

	private long expDeTu;

	private long expMP;

	private long bacMP;

	public Action OnClosePopup;

	public Action OnXemLaiBattle;

	public GameObject btnDoiHinh;

	private int EnemyID;

	public static PopupBattleResult Create(BattleReplay replay, List<UserInfo.HeroData> listDeTu, long expMP, long bacMP, long expDeTu, int gidDoiThu)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("popup/PopupBattleResult"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupBattleResult>();
		instance.listDeTu = listDeTu;
		instance.expDeTu = expDeTu;
		instance.expMP = expMP;
		instance.bacMP = bacMP;
		instance.groupInfoThua.gameObject.SetActive(false);
		instance.SyncWithNetworkData(replay);
		instance.btnDoiHinh.SetActive(gidDoiThu > 0);
		instance.EnemyID = gidDoiThu;
		return instance;
	}

	public bool ChkTeam1IsOwner(BattleReplay replay)
	{
		if (replay == null)
		{
			return true;
		}
		return GameManager.instance.m_GameClient.UserInfo.Gamer.ID == replay.Team1Data.GID || replay.Team1Data.GID == 0;
	}

	public void SyncWithNetworkData(BattleReplay replay)
	{
		BattleReplay.TeamInfo teamInfo = null;
		teamInfo = ((!ChkTeam1IsOwner(replay)) ? replay.Team2Data : replay.Team1Data);
		thang.SetActive(teamInfo.SoSao > 0 && teamInfo.SoSao < 3);
		thua.SetActive(teamInfo.SoSao == 0);
		groupInfoThua.SetActive(NGUITools.GetActive(thua.gameObject));
		daiThang.SetActive(teamInfo.SoSao == 3);
		if (teamInfo.SoSao > 0)
		{
			thangBkg.color = Color.white;
		}
		else
		{
			thangBkg.color = new Color(111f / 255f, 111f / 255f, 111f / 255f);
		}
		star1.SetActive(teamInfo.SoSao == 1);
		star2.SetActive(teamInfo.SoSao == 2);
		star3.SetActive(teamInfo.SoSao == 3);
		expReward.text = string.Format("+ {0}", expMP);
		bacReward.text = string.Format("+ {0}", bacMP);
		for (int i = 0; i < deTuList.Length; i++)
		{
			if (i < listDeTu.Count)
			{
				deTuList[i].gameObject.SetActive(true);
				deTuList[i].SetInfo(listDeTu[i], expDeTu);
			}
			else
			{
				deTuList[i].gameObject.SetActive(false);
			}
		}
		int num = (listDeTu.Count - 1) / 2 + 1;
		gridDeTu.Reposition();
		for (int j = 0; j < lotDeTuGrp.Length; j++)
		{
			lotDeTuGrp[j].gameObject.SetActive(j < num);
		}
		gridLot.Reposition();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	private void OnXemLaiBtnClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		if (screenBattle != null)
		{
			base.gameObject.SetActive(false);
			screenBattle.PhatLai();
		}
		if (OnXemLaiBattle != null)
		{
			OnXemLaiBattle();
		}
	}

	private void OnDongYBtnClick()
	{
		if (OnClosePopup != null)
		{
			OnClosePopup();
		}
		if (instance.gameObject == base.gameObject)
		{
			instance = null;
		}
		if (GUIManager.instance.LastScreen == GAME_SCREEN.ScreenBanBe)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBanBe);
			ScreenBanBe screenBanBe = GUIManager.getScreen(GAME_SCREEN.ScreenBanBe) as ScreenBanBe;
		}
		UnityEngine.Object.Destroy(base.gameObject);
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
	}

	private void OnDoiHinhBtnClick()
	{
		if (EnemyID > 0)
		{
			XemThongTinMonPhaiRequest xemThongTinMonPhaiRequest = new XemThongTinMonPhaiRequest();
			xemThongTinMonPhaiRequest.TargetGID = EnemyID;
			GameManager.instance.m_GameClient.RequestXemThongTinMonPhai(xemThongTinMonPhaiRequest);
		}
	}

	private void OnChieuMoBtnClick()
	{
		if (OnClosePopup != null)
		{
			OnClosePopup();
		}
		if (instance.gameObject == base.gameObject)
		{
			instance = null;
		}
		if (GiangHoPopup.instance != null)
		{
			GiangHoPopup.DestroyPopup();
		}
		GUIManager.setScreen(GAME_SCREEN.ScreenChoDeTu);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void OnBoiDuongBtnClick()
	{
		if (OnClosePopup != null)
		{
			OnClosePopup();
		}
		if (instance.gameObject == base.gameObject)
		{
			instance = null;
		}
		if (GiangHoPopup.instance != null)
		{
			GiangHoPopup.DestroyPopup();
		}
		GUIManager.setScreen(GAME_SCREEN.ScreenDeTu);
		ScreenDeTu screenDeTu = GUIManager.getScreen(GAME_SCREEN.ScreenDeTu) as ScreenDeTu;
		screenDeTu.displayItemSelected();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void OnCuongHoaBtnClick()
	{
		if (OnClosePopup != null)
		{
			OnClosePopup();
		}
		if (instance.gameObject == base.gameObject)
		{
			instance = null;
		}
		if (GiangHoPopup.instance != null)
		{
			GiangHoPopup.DestroyPopup();
		}
		ScreenTrangBi screenTrangBi = GUIManager.getScreen(GAME_SCREEN.ScreenTrangBi) as ScreenTrangBi;
		GUIManager.setScreen(GAME_SCREEN.ScreenTrangBi);
		screenTrangBi.displayItemSelected();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void OnTinhLuyenBtnClick()
	{
		if (OnClosePopup != null)
		{
			OnClosePopup();
		}
		if (instance.gameObject == base.gameObject)
		{
			instance = null;
		}
		if (GiangHoPopup.instance != null)
		{
			GiangHoPopup.DestroyPopup();
		}
		ScreenTrangBi screenTrangBi = GUIManager.getScreen(GAME_SCREEN.ScreenTrangBi) as ScreenTrangBi;
		GUIManager.setScreen(GAME_SCREEN.ScreenTrangBi);
		screenTrangBi.displayItemSelected();
		UnityEngine.Object.Destroy(base.gameObject);
	}
}

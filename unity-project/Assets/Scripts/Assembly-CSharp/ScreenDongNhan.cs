using System;
using LitJson;
using Nettention.Proud;
using UnityEngine;

public class ScreenDongNhan : ScreenBase
{
	private const float GuiUpdateInterval = 0.5f;

	public UILabel timeLabel;

	public DongNhanPanel dongNhanPanel;

	public UIPanel infoPanel;

	public UILabel vangLabel;

	public UILabel waitLabel;

	public UILabel levelLabel;

	public UILabel thoiGianDiet;

	private DongNhanResponse response;

	private float timeUpdateGUI = 0.5f;

	private float lastTimeClickRefresh = 6f;

	private bool isRequestingDanhDongNhan;

	public void UpdateDongNhan(DongNhanResponse response)
	{
		dongNhanPanel.SetInfo(response);
	}

	public void StartDongNhan(DongNhanResponse response)
	{
		if (GUIManager.instance != null && GUIManager.instance.homeCity != null)
		{
			GUIManager.instance.homeCity.InstantiateDongNhanAvatar();
		}
		dongNhanPanel.gameObject.SetActive(true);
		dongNhanPanel.SetInfo(response);
		(GUIManager.getScreen(GAME_SCREEN.ScreenMain) as ScreenMain).btnDongNhan.SetActive(true);
		infoPanel.gameObject.SetActive(false);
	}

	public void DestroyDongNhan()
	{
		GUIManager.instance.homeCity.DestroyDongNhan();
		dongNhanPanel.gameObject.SetActive(false);
		(GUIManager.getScreen(GAME_SCREEN.ScreenMain) as ScreenMain).btnDongNhan.SetActive(false);
		infoPanel.gameObject.SetActive(true);
	}

	public void OnDestroy()
	{
		response = null;
	}

	public override void OnActive()
	{
		if (child3Dscreen == null && GUIManager.instance.homeCity != null)
		{
			child3Dscreen = GUIManager.instance.homeCity.gameObject;
		}
		base.OnActive();
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = false;
		}
		GUIManager.ShowGadgets(6);
		if (GUIManager.instance.CurrentScreen != GAME_SCREEN.ScreenBattle)
		{
			GameManager.instance.m_GameClient.GetDongNhanInfo();
		}
		Utils.SetLightMaps("Lightmap/HomeCity/", 2);
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

	private void Start()
	{
		vangLabel.text = ConfigManager.instance.CostHoiSinhDanhDongNhan().ToString();
	}

	private void Update()
	{
		lastTimeClickRefresh += Time.deltaTime;
		if (timeUpdateGUI <= 0f && response != null)
		{
			DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
			if (serverTime < response.TimeStartDongNhan)
			{
				infoPanel.gameObject.SetActive(true);
				dongNhanPanel.gameObject.SetActive(false);
				timeLabel.gameObject.SetActive(true);
				TimeSpan timeSpan = response.TimeStartDongNhan - serverTime;
				timeLabel.text = string.Format("{0:00} : {1:00} : {2:00}", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
				(GUIManager.getScreen(GAME_SCREEN.ScreenMain) as ScreenMain).btnDongNhan.SetActive(false);
			}
			else
			{
				infoPanel.gameObject.SetActive(false);
				timeLabel.gameObject.SetActive(false);
				dongNhanPanel.gameObject.SetActive(true);
				(GUIManager.getScreen(GAME_SCREEN.ScreenMain) as ScreenMain).btnDongNhan.SetActive(true);
			}
			timeUpdateGUI = 0.5f;
		}
		timeUpdateGUI -= Time.deltaTime;
	}

	public void SyncWithNetworkData(DongNhanResponse response)
	{
		this.response = response;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (serverTime < response.TimeStartDongNhan)
		{
			infoPanel.gameObject.SetActive(true);
			dongNhanPanel.gameObject.SetActive(false);
			timeLabel.gameObject.SetActive(true);
			TimeSpan timeSpan = response.TimeStartDongNhan - serverTime;
			timeLabel.text = string.Format("{0:00} : {1:00} : {2:00}", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
			levelLabel.text = string.Format(Localization.instance.Get("LevelDongNhanLabel"), response.LevelDongNhan);
			thoiGianDiet.text = string.Format(Localization.instance.Get("ThoiGianDietDongNhan"), response.DurationLastBattle / 60, response.DurationLastBattle % 60);
			if (GUIManager.instance != null && GUIManager.instance.homeCity != null && GUIManager.instance.homeCity.dongNhanAvatar3D != null)
			{
				DestroyDongNhan();
			}
		}
		else
		{
			infoPanel.gameObject.SetActive(false);
			dongNhanPanel.gameObject.SetActive(true);
			UpdateDongNhan(response);
			if (GUIManager.instance != null && GUIManager.instance.homeCity != null && GUIManager.instance.homeCity.dongNhanAvatar3D == null)
			{
				GUIManager.instance.homeCity.InstantiateDongNhanAvatar();
			}
			(GUIManager.getScreen(GAME_SCREEN.ScreenMain) as ScreenMain).btnDongNhan.SetActive(true);
			timeLabel.gameObject.SetActive(false);
		}
	}

	private void OnCloseBtnClick()
	{
		if (GUIManager.instance.LastScreen == GAME_SCREEN.ScreenDongNhan)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenMain);
		}
		else
		{
			GUIManager.instance.SetScreen(GUIManager.instance.LastScreen);
		}
	}

	public void OnDanhBtnClick()
	{
		if ((response.NextBattleTime - GameManager.instance.m_GameClient.ServerTime).TotalSeconds > 0.0)
		{
			PopupDongNhanWaiting.Create(response.NextBattleTime, 2);
		}
		else
		{
			DanhDongNhan();
		}
	}

	public void OnTopBtnClick()
	{
		PopupTopDongNhan.Create(response);
	}

	private void OnRefreshBtnClick()
	{
		if (lastTimeClickRefresh > 5f)
		{
			GameManager.instance.m_GameClient.GetDongNhanInfo();
			lastTimeClickRefresh = 0f;
		}
		else
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("WaitForSecondsMsg"), 5));
		}
	}

	public void OnDanhDongNhanClick()
	{
		if (GUIManager.instance != null && GUIManager.instance.homeCity != null && GUIManager.instance.homeCity.mainAvatar != null && GUIManager.instance.homeCity.dongNhanAvatar3D != null)
		{
			if (GUIManager.instance.homeCity.mainAvatar.GetComponent<Collider>().bounds.Intersects(GUIManager.instance.homeCity.dongNhanAvatar3D.GetComponent<Collider>().bounds))
			{
				OnDanhBtnClick();
				return;
			}
			UnityEngine.Vector3 vector = default(UnityEngine.Vector3);
			vector = new UnityEngine.Vector3(GUIManager.instance.homeCity.dongNhanAvatar3D.transform.localPosition.x, GUIManager.instance.homeCity.dongNhanAvatar3D.transform.localPosition.y, GUIManager.instance.homeCity.dongNhanAvatar3D.transform.localPosition.z);
			UnityEngine.Vector3 vector2 = default(UnityEngine.Vector3);
			vector2 = new UnityEngine.Vector3(UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(-2, 2));
			vector += vector2;
			GUIManager.instance.homeCity.mainAvatar.MoveToNewDes(vector);
			isRequestingDanhDongNhan = true;
		}
	}

	public void OnPlayerEnterDongNhanTrigger(PlayerController player)
	{
		if (isRequestingDanhDongNhan)
		{
			OnDanhBtnClick();
			isRequestingDanhDongNhan = false;
		}
	}

	private void DanhDongNhan()
	{
		GameManager.instance.m_GameClient.RequestDanhDongNhan(false);
	}

	private void DanhNgayDongNhan()
	{
		GameManager.instance.m_GameClient.RequestDanhDongNhan(true);
		if (PopupDongNhanWaiting.instance != null)
		{
			PopupDongNhanWaiting.DestroyPopup();
		}
		isRequestingDanhDongNhan = false;
	}

	public void OnDanhNgayDongNhanClick()
	{
		if (GUIManager.instance != null && GUIManager.instance.homeCity != null && GUIManager.instance.homeCity.dongNhanAvatar3D != null)
		{
			UnityEngine.Vector3 vector = default(UnityEngine.Vector3);
			vector = new UnityEngine.Vector3(GUIManager.instance.homeCity.dongNhanAvatar3D.transform.position.x, GUIManager.instance.homeCity.dongNhanAvatar3D.transform.position.y, GUIManager.instance.homeCity.dongNhanAvatar3D.transform.position.z);
			UnityEngine.Vector3 vector2 = default(UnityEngine.Vector3);
			vector2 = new UnityEngine.Vector3(UnityEngine.Random.Range(-2, 2), 0f, UnityEngine.Random.Range(-2, 2));
			vector += vector2;
			if (GUIManager.instance != null && GUIManager.instance.homeCity != null && GUIManager.instance.homeCity.mainAvatar != null)
			{
				GUIManager.instance.homeCity.mainAvatar.Teleport(vector);
			}
			HomeResponse.Position3D position3D = new HomeResponse.Position3D();
			position3D.X = vector.x;
			position3D.Y = vector.y;
			position3D.Z = vector.z;
			GameManager.instance.m_GameClient.C2SProxy.RequestChangeNextPos(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(position3D));
			DanhNgayDongNhan();
		}
	}

	public void OnFinishDanhDongNhan()
	{
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		ScreenDongNhan screenDongNhan = GUIManager.getScreen(GAME_SCREEN.ScreenDongNhan) as ScreenDongNhan;
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.instance.gameObject.SetActive(true);
		}
		screenBattle.OnFinishReplay -= screenDongNhan.OnFinishDanhDongNhan;
		UnityEngine.Vector3 point = new UnityEngine.Vector3(-26f, 0.1f, -23f) + new UnityEngine.Vector3(UnityEngine.Random.Range(-2f, 2f), 0f, UnityEngine.Random.Range(-2f, 2f));
		if (GUIManager.instance != null && GUIManager.instance.homeCity != null && GUIManager.instance.homeCity.mainAvatar != null)
		{
			GUIManager.instance.homeCity.mainAvatar.Teleport(point);
		}
		HomeResponse.Position3D position3D = new HomeResponse.Position3D();
		position3D.X = point.x;
		position3D.Y = point.y;
		position3D.Z = point.z;
		GameManager.instance.m_GameClient.C2SProxy.RequestChangeNextPos(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(position3D));
	}

	private void OnAutoBtnClick()
	{
		PopupAutoTayDoc.Create();
	}
}

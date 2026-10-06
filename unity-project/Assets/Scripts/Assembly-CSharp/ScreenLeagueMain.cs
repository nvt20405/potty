using System;
using LitJson;
using Nettention.Proud;
using UnityEngine;

public class ScreenLeagueMain : ScreenBase
{
	private enum View
	{
		LEAGUE = 0,
		CUP = 1,
		THAMBAI = 2,
		COUNT_VIEW = 3
	}

	public GameObject LeagueView;

	public GameObject CupView;

	public GameObject ThamBaiView;

	public GameObject ThamBaiBtn;

	private DateTime visitTime;

	public UISprite leagueTabSprite;

	public UISprite cupTabSprite;

	public UISprite worshipTabSprite;

	public GetSieuCupInfoResponse sieuCupResponse;

	public GetLeagueDataResponse leagueDataResponse;

	public override void OnDeactive()
	{
		base.OnDeactive();
		child3Dscreen = null;
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = true;
		}
		GUIManager.instance.cam2D.clearFlags = CameraClearFlags.Skybox;
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		if (sieuCupResponse != null)
		{
			SyncWithNetworkData(sieuCupResponse);
		}
		else
		{
			SwitchView(View.LEAGUE);
		}
		if (visitTime < GameManager.instance.m_GameClient.UserInfo.LeagueInfo.resultTime && GameManager.instance.m_GameClient.ServerTime > GameManager.instance.m_GameClient.UserInfo.LeagueInfo.resultTime)
		{
			GameManager.instance.m_GameClient.RequestGetLeagueData();
			GameManager.instance.m_GameClient.RequestGetSieuCupData();
		}
		visitTime = GameManager.instance.m_GameClient.ServerTime;
		Utils.SetLightMaps("Lightmap/HomeCity/", 2);
	}

	private void OnHelp()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(10, 0);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
	}

	private void SwitchView(View view)
	{
		string spriteName = "menu_toi";
		string spriteName2 = "menu_sang";
		UnityEngine.Vector3 vector = default(UnityEngine.Vector3);
		vector = new UnityEngine.Vector3(164f, 86f, 1f);
		UnityEngine.Vector3 vector2 = default(UnityEngine.Vector3);
		vector2 = new UnityEngine.Vector3(160f, 70f, 1f);
		if (view == View.LEAGUE)
		{
			LeagueView.SetActive(true);
			leagueTabSprite.spriteName = spriteName2;
			leagueTabSprite.transform.localScale = vector;
		}
		else
		{
			LeagueView.SetActive(false);
			leagueTabSprite.spriteName = spriteName;
			leagueTabSprite.transform.localScale = vector2;
		}
		if (CupView != null)
		{
			if (view == View.CUP)
			{
				CupView.SetActive(true);
				cupTabSprite.spriteName = spriteName2;
				cupTabSprite.transform.localScale = vector;
				if (sieuCupResponse == null)
				{
					GameManager.instance.m_GameClient.RequestGetSieuCupData();
				}
				else
				{
					CupView.GetComponent<SuperCupView>().SetInfo(sieuCupResponse);
				}
			}
			else
			{
				CupView.SetActive(false);
				cupTabSprite.spriteName = spriteName;
				cupTabSprite.transform.localScale = vector2;
			}
		}
		if (ThamBaiView != null)
		{
			if (view == View.THAMBAI)
			{
				ThamBaiView.SetActive(true);
				worshipTabSprite.spriteName = spriteName2;
				worshipTabSprite.transform.localScale = new UnityEngine.Vector3(vector.x, vector.y - 8f, vector.z);
			}
			else
			{
				ThamBaiView.SetActive(false);
				worshipTabSprite.spriteName = spriteName;
				worshipTabSprite.transform.localScale = vector2;
			}
		}
	}

	public void GoToLeague()
	{
		SwitchView(View.LEAGUE);
		if (child3Dscreen != null)
		{
			child3Dscreen.SetActive(false);
			child3Dscreen = null;
		}
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = true;
		}
		GUIManager.instance.cam2D.clearFlags = CameraClearFlags.Skybox;
	}

	public void GoToCup()
	{
		SwitchView(View.CUP);
		if (child3Dscreen != null)
		{
			child3Dscreen.SetActive(false);
			child3Dscreen = null;
		}
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = true;
		}
		GUIManager.instance.cam2D.clearFlags = CameraClearFlags.Skybox;
	}

	public void SyncWithNetworkData(GetSieuCupInfoResponse sieuCupResponse)
	{
		this.sieuCupResponse = sieuCupResponse;
		if (sieuCupResponse.Champion == null || sieuCupResponse.Champion.GID == 0 || sieuCupResponse.Champion.SID == 0)
		{
			ThamBaiBtn.SetActive(false);
		}
		else
		{
			ThamBaiBtn.SetActive(true);
		}
		if (GameManager.instance.m_GameClient.ServerTime + TimeSpan.FromDays(1.0) > sieuCupResponse.TimeStart)
		{
			GoToCup();
		}
		else
		{
			GoToLeague();
		}
	}

	public void GoToThamBai()
	{
		SwitchView(View.THAMBAI);
		if (GUIManager.instance.homeCity != null)
		{
			child3Dscreen = GUIManager.instance.homeCity.gameObject;
			child3Dscreen.SetActive(true);
		}
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = false;
		}
		GUIManager.instance.cam2D.clearFlags = CameraClearFlags.Depth;
		GoToThamBai3D();
	}

	private void GoToThamBai3D()
	{
		if (GUIManager.instance.homeCity.IsFightingNienThu)
		{
			(GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain).ThoatDanhNienThu();
		}
		UnityEngine.Vector3 vector = default(UnityEngine.Vector3);
		vector = new UnityEngine.Vector3(-37.66621f, -1.53726f, -6.82079f);
		if (GUIManager.instance.homeCity.mainAvatar != null)
		{
			GUIManager.instance.homeCity.mainAvatar.Teleport(vector);
		}
		vector.x += UnityEngine.Random.Range(0f, 1f);
		vector.z -= UnityEngine.Random.Range(0f, 1f);
		HomeResponse.Position3D position3D = new HomeResponse.Position3D();
		position3D.X = vector.x;
		position3D.Y = vector.y;
		position3D.Z = vector.z;
		GameManager.instance.m_GameClient.C2SProxy.RequestChangeNextPos(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(position3D));
	}
}

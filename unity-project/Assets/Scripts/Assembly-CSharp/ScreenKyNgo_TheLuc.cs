using System;
using LitJson;
using Nettention.Proud;
using UnityEngine;

public class ScreenKyNgo_TheLuc : ScreenBase
{
	public GameObject messPopUp;

	public UILabel lbMess;

	public UILabel lbChuaDenGioAnGa;

	public UISprite spGaQuay;

	private void Start()
	{
		UIEventListener.Get(base.gameObject).onClick = backGround_onClick;
	}

	private void Update()
	{
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
		Utils.SetLightMaps("Lightmap/HomeCity/", 2);
		goToQuanRuou();
	}

	public void goToQuanRuou()
	{
		if (GUIManager.instance.homeCity.IsFightingNienThu)
		{
			(GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain).ThoatDanhNienThu();
		}
		UnityEngine.Vector3 vector = default(UnityEngine.Vector3);
		vector = new UnityEngine.Vector3(-27f, 0.2f, 6f);
		if (GUIManager.instance.homeCity.mainAvatar != null)
		{
			GUIManager.instance.homeCity.mainAvatar.Teleport(vector);
		}
		HomeResponse.Position3D position3D = new HomeResponse.Position3D();
		position3D.X = vector.x + (float)UnityEngine.Random.Range(0, 1);
		position3D.Y = vector.y;
		position3D.Z = vector.z + (float)UnityEngine.Random.Range(0, 1);
		GameManager.instance.m_GameClient.C2SProxy.RequestChangeNextPos(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(position3D));
		updateMess();
	}

	public void updateMess()
	{
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		string text;
		if ((serverTime.Hour == 12 && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("HoiTheLuc1;")) || (serverTime.Hour == 18 && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("HoiTheLuc2;")))
		{
			text = Localization.instance.Get("ThongBaoAnGaLabel");
			lbChuaDenGioAnGa.gameObject.SetActive(false);
			spGaQuay.gameObject.SetActive(true);
		}
		else
		{
			text = Localization.instance.Get("ThongBaoKyNgoTheLuc");
			lbChuaDenGioAnGa.gameObject.SetActive(true);
			spGaQuay.gameObject.SetActive(false);
		}
		lbMess.text = text;
		updateMainMenuView();
	}

	private void updateMainMenuView()
	{
		GadgetPanelBottom gadgetPanelBottom = GUIManager.instance.gadgetPanelBottom;
		if (!(gadgetPanelBottom != null))
		{
			return;
		}
		gadgetPanelBottom.checkDisplayThongBaoSuKien();
		if (gadgetPanelBottom.listKyNgoMenu == null || gadgetPanelBottom.listKyNgoMenu.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < gadgetPanelBottom.listKyNgoMenu.Count; i++)
		{
			if (gadgetPanelBottom.listKyNgoMenu[i].kyNgoID == MenuButtonKyNgo.KyNgoType.TheLuc)
			{
				if (gadgetPanelBottom.checkThongBaoTheLuc())
				{
					gadgetPanelBottom.listKyNgoMenu[i].notifyIcon.gameObject.SetActive(true);
				}
				else
				{
					gadgetPanelBottom.listKyNgoMenu[i].notifyIcon.gameObject.SetActive(false);
				}
			}
		}
	}

	public void AnGa_onClick()
	{
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (serverTime.Hour == 12 || serverTime.Hour == 18)
		{
			HoiTheLucRequest request = new HoiTheLucRequest();
			GameManager.instance.m_GameClient.RequestHoiTheLuc(request);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ChuaDenGioAnGaThongBao"));
		}
	}

	public void backGround_onClick(GameObject go)
	{
		GadgetPanelBottom gadgetPanelBottom = GUIManager.instance.gadgetPanelBottom;
		if (gadgetPanelBottom != null)
		{
			gadgetPanelBottom.OnClickBtnHome();
		}
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = true;
		}
	}
}

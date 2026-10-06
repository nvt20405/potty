using LitJson;
using Nettention.Proud;
using UnityEngine;

public class ScreenKyNgo_ThamBai : ScreenBase
{
	public GameObject messPopUp;

	public UILabel lbMess;

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
		goToDocCoCauBaiKiem();
	}

	public void goToDocCoCauBaiKiem()
	{
		if (GUIManager.instance.homeCity.IsFightingNienThu)
		{
			(GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain).ThoatDanhNienThu();
		}
		UnityEngine.Vector3 vector = default(UnityEngine.Vector3);
		vector = new UnityEngine.Vector3(35.95f, -2.29f, -13.55f);
		if (GUIManager.instance.homeCity.mainAvatar != null)
		{
			GUIManager.instance.homeCity.mainAvatar.Teleport(vector);
		}
		HomeResponse.Position3D position3D = new HomeResponse.Position3D();
		position3D.X = vector.x + (float)Random.Range(0, 2);
		position3D.Y = vector.y + (float)Random.Range(0, 2);
		position3D.Z = vector.z;
		GameManager.instance.m_GameClient.C2SProxy.RequestChangeNextPos(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(position3D));
		displayMess();
	}

	public void displayMess()
	{
		int thamBaiCount = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.ThamBaiCount;
		lbMess.text = string.Format(Localization.instance.Get("ThamBaiMess"), thamBaiCount);
	}

	public void updateInfo(PhanThuongResponse response)
	{
		displayMess();
		if (response.PhanThuongList != null && response.PhanThuongList.Count > 0)
		{
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), response);
		}
		if (GUIManager.instance.homeCity.ParticleKiemThan != null)
		{
			if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("KyNgoThamBai;"))
			{
				GUIManager.instance.homeCity.ParticleKiemThan.SetActive(true);
			}
			else
			{
				GUIManager.instance.homeCity.ParticleKiemThan.SetActive(false);
			}
		}
	}

	public void ThamBai_onClick()
	{
		if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("KyNgoThamBai;"))
		{
			KyNgoThamBaiRequest request = new KyNgoThamBaiRequest();
			GameManager.instance.m_GameClient.RequestKyNgoThamBai(request);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("DaThamBaiMess"));
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

	public void updateMainMenuView()
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
			if (gadgetPanelBottom.listKyNgoMenu[i].kyNgoID == MenuButtonKyNgo.KyNgoType.ThamBai)
			{
				if (gadgetPanelBottom.checkThongBaoThamBai())
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
}

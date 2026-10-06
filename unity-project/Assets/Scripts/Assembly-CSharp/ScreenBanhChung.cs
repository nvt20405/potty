using System.Collections;
using UnityEngine;

public class ScreenBanhChung : ScreenBase
{
	public ScreenBanhChung3D screen3dObj;

	private BanhChungInfoResponse response;

	public UILabel luotLayNguyenLieu;

	public UILabel tongSobanh;

	public UILabel labelThit;

	public UILabel labelLa;

	public UILabel labelGao;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = false;
		}
		if (screen3dObj == null)
		{
			StartCoroutine(SpawnScreen3D());
		}
		Utils.SetLightMaps("Lightmap/MapBanhTrung/");
		GameManager.instance.m_GameClient.RequestBanhChungInfo();
	}

	public override void OnDeactive()
	{
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = true;
		}
		base.OnDeactive();
		if (screen3dObj != null)
		{
			screen3dObj.gameObject.SetActive(false);
			Object.Destroy(screen3dObj.gameObject);
		}
		screen3dObj = null;
	}

	private IEnumerator SpawnScreen3D()
	{
		PopupLoading.Create();
		EGResourceAsyncLoader loader = EGResourceAsyncLoader.Load("GUI/Screens3D/ScreenBanhChung3D");
		loader.OnLoading = (float e) =>
		{
			if (PopupLoading.instance != null)
			{
				PopupLoading.instance.SetAmmount(e);
			}
		};
		while (!loader.IsDone)
		{
			yield return null;
		}
		PopupLoading.DestroyPopup();
		Object obj = Object.Instantiate(loader.Asset);
		GameObject scr3D = (GameObject)((obj is GameObject) ? obj : null);
		scr3D.transform.parent = GUIManager.instance.ScreenContainer3D;
		scr3D.transform.localScale = Vector3.one;
		scr3D.transform.localRotation = Quaternion.identity;
		scr3D.transform.localPosition = Vector3.zero;
		child3Dscreen = scr3D;
		screen3dObj = scr3D.GetComponent<ScreenBanhChung3D>();
		GUIManager.instance.cam2D.clearFlags = CameraClearFlags.Depth;
		if (response != null)
		{
			screen3dObj.SpawnPlayerAvatar(response.Player, response.Player3DInfo);
		}
	}

	private IEnumerator DelaySyncOthersToFinishLoadScreen3D(BanhChungOthersResponse response)
	{
		while (screen3dObj == null)
		{
			yield return null;
		}
		SyncWithNetworkData(response);
	}

	private IEnumerator DelaySyncToFinishLoadScreen3D(BanhChungInfoResponse response)
	{
		while (screen3dObj == null)
		{
			yield return null;
		}
		SyncWithNetworkData(response);
	}

	public void SyncWithNetworkData(BanhChungOthersResponse response)
	{
		if (screen3dObj == null)
		{
			StartCoroutine(DelaySyncOthersToFinishLoadScreen3D(response));
			return;
		}
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo.ServerInfo.BanhChungCfg != null)
		{
			screen3dObj.SetNoiBanh(userInfo.ServerInfo.BanhChungCfg.GetLevelNoiBanhFromSoBanh(response.Noi.SoBanh));
		}
		screen3dObj.SpawnOthers(response.OtherPlayers);
	}

	public void SyncWithNetworkData(BanhChungInfoResponse response)
	{
		this.response = response;
		ScreenNauBanhChung screenNauBanhChung = GUIManager.getScreen(GAME_SCREEN.ScreenNauBanhChung) as ScreenNauBanhChung;
		screenNauBanhChung.SyncWithNetworkData(response.Noi, response.Player);
		if (screen3dObj == null)
		{
			StartCoroutine(DelaySyncToFinishLoadScreen3D(response));
			return;
		}
		if (screen3dObj.mainPlayer == null)
		{
			screen3dObj.SpawnPlayerAvatar(response.Player, response.Player3DInfo);
		}
		Vector3 position = screen3dObj.mainPlayer.transform.position;
		GameManager.instance.m_GameClient.RequestBanhChungPlayerMove(position.x, position.y, position.z);
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo.ServerInfo.BanhChungCfg != null)
		{
			screen3dObj.SetNoiBanh(userInfo.ServerInfo.BanhChungCfg.GetLevelNoiBanhFromSoBanh(response.Noi.SoBanh));
		}
		tongSobanh.text = string.Format(Localization.instance.Get("TongSoBanhLabel"), response.Noi.SoBanh);
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = userInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_LA_DONG");
		labelLa.text = ((vatPhamTieuThuData != null) ? vatPhamTieuThuData.Quantity.ToString() : "0");
		vatPhamTieuThuData = userInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_GAO_NEP");
		labelGao.text = ((vatPhamTieuThuData != null) ? vatPhamTieuThuData.Quantity.ToString() : "0");
		vatPhamTieuThuData = userInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_THIT_LON");
		labelThit.text = ((vatPhamTieuThuData != null) ? vatPhamTieuThuData.Quantity.ToString() : "0");
		int num = response.Player.NguyenLieu1 + response.Player.NguyenLieu2 + response.Player.NguyenLieu3;
		if (num < userInfo.ServerInfo.BanhChungCfg.MaxLuotNgL)
		{
			luotLayNguyenLieu.text = string.Format(Localization.instance.Get("LuotLayNguyenLieuLabel"), num, userInfo.ServerInfo.BanhChungCfg.MaxLuotNgL);
		}
		else
		{
			luotLayNguyenLieu.text = Localization.instance.Get("FullLuotNgLieuBanhChung");
		}
		if (response.Player.NguyenLieu == "LON")
		{
			screen3dObj.SpawnLonSua();
			return;
		}
		if (response.Player.NguyenLieu == "LA")
		{
			screen3dObj.SpawnBuiDong();
			return;
		}
		if (response.Player.NguyenLieu == "GAO")
		{
			screen3dObj.SpawnBaoGao();
			return;
		}
		if (screen3dObj.lon_sua != null)
		{
			screen3dObj.lon_sua.gameObject.SetActive(false);
			Object.Destroy(screen3dObj.lon_sua.gameObject, 1f);
		}
		if (screen3dObj.baoGao != null)
		{
			screen3dObj.baoGao.gameObject.SetActive(false);
			Object.Destroy(screen3dObj.baoGao.gameObject, 1f);
		}
		if (screen3dObj.buiDong != null)
		{
			screen3dObj.buiDong.gameObject.SetActive(false);
			Object.Destroy(screen3dObj.buiDong.gameObject, 1f);
		}
	}

	private void OnNauBanhBtnClick()
	{
		if (screen3dObj != null && screen3dObj.mainPlayer != null)
		{
			Vector3 randomPosNauBanh = screen3dObj.GetRandomPosNauBanh();
			screen3dObj.mainPlayer.Teleport(randomPosNauBanh);
			GameManager.instance.m_GameClient.RequestBanhChungPlayerMove(randomPosNauBanh.x, randomPosNauBanh.y, randomPosNauBanh.z);
			GUIManager.setScreen(GAME_SCREEN.ScreenNauBanhChung);
			if (response != null)
			{
				ScreenNauBanhChung screenNauBanhChung = GUIManager.getScreen(GAME_SCREEN.ScreenNauBanhChung) as ScreenNauBanhChung;
				screenNauBanhChung.SyncWithNetworkData(response.Noi, response.Player);
			}
		}
	}
}

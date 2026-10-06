using System.Collections;
using UnityEngine;

public class ScreenSonMonMain : ScreenBase
{
	public ScreenSonMon3D screen3dObj;

	public UILabel GoLabel;

	public UILabel DaLabel;

	public UILabel EGoLabel;

	public UILabel EDaLabel;

	public UILabel ENKLabel;

	public UILabel EGaLabel;

	public UILabel DanhVongLabel;

	public GameObject MyGrp;

	public GameObject TargetGrp;

	public UserInfo.SonMonInfo curSonMon;

	public int LuotDoTham;

	public GameObject KNBDoThamGrp;

	public UILabel KNBDoThamLabel;

	public GameObject KNBDoTham2Grp;

	public UILabel KNBDoTham2Label;

	public UILabel enemyName;

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
		GUIManager.ShowGadgets(-1);
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = false;
		}
		if (screen3dObj == null)
		{
			StartCoroutine(SpawnScreen3D(GameManager.instance.m_GameClient.UserInfo.SonMon));
		}
		Utils.SetLightMaps("Lightmap/Son_Mon/", 2);
	}

	public void Set(UserInfo.SonMonInfo sonmon, bool isReadOnly)
	{
		if (screen3dObj != null)
		{
			Object.Destroy(screen3dObj.gameObject);
		}
		StartCoroutine(SpawnScreen3D(sonmon, isReadOnly));
	}

	private IEnumerator SpawnScreen3D(UserInfo.SonMonInfo sonmon, bool isReadOnly = false)
	{
		PopupLoading.Create();
		EGResourceAsyncLoader loader = EGResourceAsyncLoader.Load("gui/screens3d/ScreenSonMon3D");
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
		screen3dObj = scr3D.GetComponent<ScreenSonMon3D>();
		screen3dObj.Create(sonmon, isReadOnly);
		MyGrp.SetActive(!isReadOnly);
		TargetGrp.SetActive(isReadOnly);
		curSonMon = sonmon;
		if (isReadOnly)
		{
			int maxLuot = 5;
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.Vip >= 12)
			{
				maxLuot = 15;
			}
			else if (GameManager.instance.m_GameClient.UserInfo.Gamer.Vip >= 9)
			{
				maxLuot = 13;
			}
			else if (GameManager.instance.m_GameClient.UserInfo.Gamer.Vip >= 5)
			{
				maxLuot = 11;
			}
			else if (GameManager.instance.m_GameClient.UserInfo.Gamer.Vip >= 3)
			{
				maxLuot = 9;
			}
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("DoThamSM"))
			{
				for (int i = 1; i < maxLuot; i++)
				{
					if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("DoThamSM" + i + ";"))
					{
						LuotDoTham = i + 1;
						break;
					}
				}
			}
			else
			{
				LuotDoTham = 1;
			}
			if (SonMonHelper.GetGiaDoThamSonMon(LuotDoTham) > 0)
			{
				KNBDoTham2Grp.SetActive(true);
				KNBDoTham2Label.text = SonMonHelper.GetGiaDoThamSonMon(LuotDoTham).ToString();
			}
			else
			{
				KNBDoTham2Grp.SetActive(false);
			}
			PhanThuongResponse phanthuong = sonmon.GetSonMonRaidGet();
			if (phanthuong.PhanThuongList.Exists((PhanThuongResponse.PhanThuong pt) => pt.Name == "VP_DA"))
			{
				EDaLabel.text = phanthuong.PhanThuongList.Find((PhanThuongResponse.PhanThuong pt) => pt.Name == "VP_DA").Count + string.Empty;
			}
			else
			{
				EDaLabel.text = "0";
			}
			if (phanthuong.PhanThuongList.Exists((PhanThuongResponse.PhanThuong pt) => pt.Name == "VP_GO"))
			{
				EGoLabel.text = phanthuong.PhanThuongList.Find((PhanThuongResponse.PhanThuong pt) => pt.Name == "VP_GO").Count + string.Empty;
			}
			else
			{
				EGoLabel.text = "0";
			}
			if (phanthuong.PhanThuongList.Exists((PhanThuongResponse.PhanThuong pt) => pt.Name == "VP_GA_QUAY"))
			{
				EGaLabel.text = phanthuong.PhanThuongList.Find((PhanThuongResponse.PhanThuong pt) => pt.Name == "VP_GA_QUAY").Count + string.Empty;
			}
			else
			{
				EGaLabel.text = "0";
			}
			if (phanthuong.PhanThuongList.Exists((PhanThuongResponse.PhanThuong pt) => pt.Name == "VP_NGUYEN_KHI_DAN"))
			{
				ENKLabel.text = phanthuong.PhanThuongList.Find((PhanThuongResponse.PhanThuong pt) => pt.Name == "VP_NGUYEN_KHI_DAN").Count + string.Empty;
			}
			else
			{
				ENKLabel.text = "0";
			}
			enemyName.text = string.Empty;
			foreach (UserInfo.SonMonBuildingInfo congtrinh in sonmon.ListCongTrinh)
			{
				if (!string.IsNullOrEmpty(congtrinh.DisplayName))
				{
					enemyName.text = string.Format(Localization.instance.Get("TenSonMon"), congtrinh.DisplayName);
				}
			}
		}
		else
		{
			int maxLuot2 = 5;
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.Vip >= 12)
			{
				maxLuot2 = 15;
			}
			else if (GameManager.instance.m_GameClient.UserInfo.Gamer.Vip >= 9)
			{
				maxLuot2 = 13;
			}
			else if (GameManager.instance.m_GameClient.UserInfo.Gamer.Vip >= 5)
			{
				maxLuot2 = 11;
			}
			else if (GameManager.instance.m_GameClient.UserInfo.Gamer.Vip >= 3)
			{
				maxLuot2 = 9;
			}
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("DoThamSM"))
			{
				for (int j = 1; j < maxLuot2; j++)
				{
					if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("DoThamSM" + j + ";"))
					{
						LuotDoTham = j + 1;
						break;
					}
				}
			}
			else
			{
				LuotDoTham = 1;
			}
			if (SonMonHelper.GetGiaDoThamSonMon(LuotDoTham) > 0)
			{
				KNBDoThamGrp.SetActive(true);
				KNBDoThamLabel.text = SonMonHelper.GetGiaDoThamSonMon(LuotDoTham).ToString();
			}
			else
			{
				KNBDoThamGrp.SetActive(false);
			}
			DanhVongLabel.text = GameManager.instance.m_GameClient.UserInfo.SonMon.Score.ToString();
			if (!GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Quantity > 0 && vp.Name == "VP_GO"))
			{
				GoLabel.text = "0";
			}
			else
			{
				GoLabel.text = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Quantity > 0 && vp.Name == "VP_GO").Quantity + string.Empty;
			}
			UILabel goLabel = GoLabel;
			goLabel.text = goLabel.text + "/" + ConfigManager.instance.SonMonConfig.CongTrinhCfg["CHINH_SANH"][sonmon.ListCongTrinh.Find((UserInfo.SonMonBuildingInfo ct) => ct.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.CHINH_SANH).Level - 1].Storage;
			if (!GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Quantity > 0 && vp.Name == "VP_DA"))
			{
				DaLabel.text = "0";
			}
			else
			{
				DaLabel.text = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Quantity > 0 && vp.Name == "VP_DA").Quantity + string.Empty;
			}
			UILabel daLabel = DaLabel;
			daLabel.text = daLabel.text + "/" + ConfigManager.instance.SonMonConfig.CongTrinhCfg["CHINH_SANH"][sonmon.ListCongTrinh.Find((UserInfo.SonMonBuildingInfo ct) => ct.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.CHINH_SANH).Level - 1].Storage;
		}
		GUIManager.instance.cam2D.clearFlags = CameraClearFlags.Depth;
	}

	public void OnChienSu()
	{
		PopupSonMonChienSu.Create();
	}

	public void OnXepHang()
	{
		GameManager.instance.m_GameClient.RequestGetTopSonMon();
	}

	public void OnTapKichConfirm()
	{
		GameManager.instance.m_GameClient.RequestTanCongSonMon(curSonMon.GID, curSonMon.SID);
	}

	public void OnConfirmDoTham()
	{
		if (SonMonHelper.GetGiaDoThamSonMon(LuotDoTham) > GameManager.instance.m_GameClient.UserInfo.Gamer.Vang)
		{
			MessagePopup.Create(Localization.instance.Get("KhongDuKNB"));
		}
		else
		{
			GameManager.instance.m_GameClient.RequestDoThamSonMonRandom();
		}
	}

	public void OnTapKich()
	{
		if (SonMonHelper.GetGiaDoThamSonMon(LuotDoTham) > 0)
		{
			PopupYesNo.Create(Localization.instance.Get("ConfirmDoThamSonMon"), Localization.instance.Get("DongYLabelBtn"), Localization.instance.Get("TuChoiLabelBtn"), OnConfirmDoTham, null);
		}
		else if (SonMonHelper.GetGiaDoThamSonMon(LuotDoTham) > GameManager.instance.m_GameClient.UserInfo.Gamer.Vang)
		{
			MessagePopup.Create(Localization.instance.Get("KhongDuKNB"));
		}
		else
		{
			GameManager.instance.m_GameClient.RequestDoThamSonMonRandom();
		}
	}

	public void OnPhongThu()
	{
		PopupSonMonDoiHinhPhongThu.Create();
	}

	public void OnRoiKhoiSonMon()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenMain);
	}

	public void OnTroVeSonMon()
	{
		Set(GameManager.instance.m_GameClient.UserInfo.SonMon, false);
	}

	public void UpdateCongTrinh(UserInfo.SonMonBuildingInfo congtrinh)
	{
		UserInfo.SonMonInfo sonMonInfo = curSonMon;
		if (congtrinh.GID == GameManager.instance.m_GameClient.UserInfo.Gamer.ID)
		{
			if (congtrinh.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.CHINH_SANH)
			{
				screen3dObj.ChinhSanhSlot.Set(congtrinh);
			}
			else
			{
				screen3dObj.UtilitySlots[congtrinh.Slot - 1].Set(congtrinh);
			}
		}
		DanhVongLabel.text = GameManager.instance.m_GameClient.UserInfo.SonMon.Score.ToString();
		if (!GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Quantity > 0 && vp.Name == "VP_GO"))
		{
			GoLabel.text = "0";
		}
		else
		{
			GoLabel.text = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Quantity > 0 && vp.Name == "VP_GO").Quantity + string.Empty;
		}
		UILabel goLabel = GoLabel;
		goLabel.text = goLabel.text + "/" + ConfigManager.instance.SonMonConfig.CongTrinhCfg["CHINH_SANH"][sonMonInfo.ListCongTrinh.Find((UserInfo.SonMonBuildingInfo ct) => ct.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.CHINH_SANH).Level - 1].Storage;
		if (!GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Quantity > 0 && vp.Name == "VP_DA"))
		{
			DaLabel.text = "0";
		}
		else
		{
			DaLabel.text = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Quantity > 0 && vp.Name == "VP_DA").Quantity + string.Empty;
		}
		UILabel daLabel = DaLabel;
		daLabel.text = daLabel.text + "/" + ConfigManager.instance.SonMonConfig.CongTrinhCfg["CHINH_SANH"][sonMonInfo.ListCongTrinh.Find((UserInfo.SonMonBuildingInfo ct) => ct.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.CHINH_SANH).Level - 1].Storage;
	}

	public void OnHelpBtn()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(12, 0);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
	}
}

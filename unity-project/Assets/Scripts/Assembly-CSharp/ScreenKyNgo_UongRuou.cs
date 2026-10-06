using System;
using System.Collections.Generic;
using LitJson;
using Nettention.Proud;
using UnityEngine;

public class ScreenKyNgo_UongRuou : ScreenBase
{
	public UILabel lbTitleLamMoi;

	public UISprite spDoiRuou1;

	public UISprite spDoiRuou2;

	public UISprite spDoiRuou3;

	public UISprite spUongRuou1;

	public UISprite spUongRuou2;

	public UISprite spUongRuou3;

	public UILabel lbGiaDoiRuou1;

	public UILabel lbGiaDoiRuou2;

	public UILabel lbGiaDoiRuou3;

	public UILabel lbNameRuou1;

	public UILabel lbNameRuou2;

	public UILabel lbNameRuou3;

	public UILabel lbDescriptionRuou1;

	public UILabel lbDescriptionRuou2;

	public UILabel lbDescriptionRuou3;

	public NhanVatAvatar nhanvat1;

	public NhanVatAvatar nhanvat2;

	public NhanVatAvatar nhanvat3;

	public GameObject doiRuouGroup;

	public GameObject uongRuouGroup;

	private int vatPhamID;

	private UserInfo.HeroData detuSelected;

	private UserInfo.VatPhamTieuThuData dataRuou1;

	private UserInfo.VatPhamTieuThuData dataRuou2;

	private UserInfo.VatPhamTieuThuData dataRuou3;

	private List<string> listTanHonName = new List<string>();

	private float nextSecond;

	public DateTime timeReset;

	private void Start()
	{
		UIEventListener.Get(spUongRuou1.gameObject).onClick = spRuou1_OnClick;
		UIEventListener.Get(spUongRuou2.gameObject).onClick = spRuou2_OnClick;
		UIEventListener.Get(spUongRuou3.gameObject).onClick = spRuou3_OnClick;
		UIEventListener.Get(spDoiRuou1.gameObject).onClick = spRuou1_OnClick;
		UIEventListener.Get(spDoiRuou2.gameObject).onClick = spRuou2_OnClick;
		UIEventListener.Get(spDoiRuou3.gameObject).onClick = spRuou3_OnClick;
		UIEventListener.Get(base.gameObject).onClick = backGround_onClick;
	}

	private void Update()
	{
		nextSecond += Time.deltaTime;
		if (nextSecond >= 1f)
		{
			nextSecond = 0f;
			updateTime();
		}
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

	public override void OnDeactive()
	{
		base.OnDeactive();
		GUIManager.instance.homeCity.cam.GetComponent<CameraMovement>().enabled = true;
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = true;
		}
	}

	public void goToQuanRuou()
	{
		if (GUIManager.instance.homeCity.IsFightingNienThu)
		{
			(GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain).ThoatDanhNienThu();
		}
		UnityEngine.Vector3 vector = default(UnityEngine.Vector3);
		vector = new UnityEngine.Vector3(-15.55f, 3.38f, 22f);
		UnityEngine.Vector3 vector2 = default(UnityEngine.Vector3);
		vector2 = new UnityEngine.Vector3(0f, 338f, 1f);
		if (GUIManager.instance.homeCity.mainAvatar != null)
		{
			GUIManager.instance.homeCity.mainAvatar.Teleport(vector);
			GUIManager.instance.homeCity.mainAvatar.transform.localRotation = Quaternion.Euler(vector2);
		}
		GUIManager.instance.homeCity.cam.transform.localPosition = new UnityEngine.Vector3(-15.5f, 20.12f, 2.3f);
		GUIManager.instance.homeCity.cam.GetComponent<CameraMovement>().enabled = false;
		HomeResponse.Position3D position3D = new HomeResponse.Position3D();
		position3D.X = vector.x + (float)UnityEngine.Random.Range(0, 1);
		position3D.Y = vector.y;
		position3D.Z = vector.z + (float)UnityEngine.Random.Range(0, 1);
		GameManager.instance.m_GameClient.C2SProxy.RequestChangeNextPos(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(position3D));
		displayInfo();
	}

	public void updateTime()
	{
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		TimeSpan timeSpan = timeReset - serverTime;
		string empty = string.Empty;
		empty = ((timeSpan.Hours >= 10) ? (empty + timeSpan.Hours + ":") : (empty + "0" + timeSpan.Hours + ":"));
		empty = ((timeSpan.Minutes >= 10) ? (empty + timeSpan.Minutes + ":") : (empty + "0" + timeSpan.Minutes + ":"));
		empty = ((timeSpan.Seconds >= 10) ? (empty + timeSpan.Seconds) : (empty + "0" + timeSpan.Seconds));
		lbTitleLamMoi.text = Localization.instance.Get("LamMoiLabelMess") + " " + empty;
		if (timeSpan.TotalSeconds <= 0.0)
		{
			GetDoiRuouInfoRequest request = new GetDoiRuouInfoRequest();
			GameManager.instance.m_GameClient.RequestGetDoiRuouInfo(request);
		}
	}

	public void displayInfo()
	{
		VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu["VP_RUOU_TIEM_LUC_0"];
		VatPhamTieuThuCfg vatPhamTieuThuCfg2 = ConfigManager.instance.m_dicVatPhamTieuThu["VP_RUOU_TIEM_LUC_1"];
		VatPhamTieuThuCfg vatPhamTieuThuCfg3 = ConfigManager.instance.m_dicVatPhamTieuThu["VP_RUOU_TIEM_LUC_2"];
		if (vatPhamTieuThuCfg != null)
		{
			lbDescriptionRuou1.text = vatPhamTieuThuCfg.MoTa;
			dataRuou1 = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_RUOU_TIEM_LUC_0");
			if (dataRuou1 != null && dataRuou1.Quantity > 0)
			{
				lbNameRuou1.text = vatPhamTieuThuCfg.TenHienThi + " x" + dataRuou1.Quantity;
			}
			else
			{
				lbNameRuou1.text = vatPhamTieuThuCfg.TenHienThi + " x0";
			}
		}
		else
		{
			lbNameRuou1.text = string.Empty;
			lbDescriptionRuou1.text = string.Empty;
		}
		if (vatPhamTieuThuCfg2 != null)
		{
			lbDescriptionRuou2.text = vatPhamTieuThuCfg2.MoTa;
			dataRuou2 = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_RUOU_TIEM_LUC_1");
			if (dataRuou2 != null && dataRuou2.Quantity > 0)
			{
				lbNameRuou2.text = vatPhamTieuThuCfg2.TenHienThi + " x" + dataRuou2.Quantity;
			}
			else
			{
				lbNameRuou2.text = vatPhamTieuThuCfg2.TenHienThi + " x0";
			}
		}
		else
		{
			lbNameRuou2.text = string.Empty;
			lbDescriptionRuou2.text = string.Empty;
		}
		if (vatPhamTieuThuCfg3 != null)
		{
			lbDescriptionRuou3.text = vatPhamTieuThuCfg3.MoTa;
			dataRuou3 = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_RUOU_TIEM_LUC_2");
			if (dataRuou3 != null && dataRuou3.Quantity > 0)
			{
				lbNameRuou3.text = vatPhamTieuThuCfg3.TenHienThi + " x" + dataRuou3.Quantity;
			}
			else
			{
				lbNameRuou3.text = vatPhamTieuThuCfg3.TenHienThi + " x0";
			}
		}
		else
		{
			lbNameRuou3.text = string.Empty;
			lbDescriptionRuou3.text = string.Empty;
		}
		spDoiRuou1.spriteName = "VP_RUOU_TIEM_LUC_0";
		spDoiRuou2.spriteName = "VP_RUOU_TIEM_LUC_1";
		spDoiRuou3.spriteName = "VP_RUOU_TIEM_LUC_2";
		spUongRuou1.spriteName = "VP_RUOU_TIEM_LUC_0";
		spUongRuou2.spriteName = "VP_RUOU_TIEM_LUC_1";
		spUongRuou3.spriteName = "VP_RUOU_TIEM_LUC_2";
		spDoiRuou1.MakePixelPerfect();
		spDoiRuou2.MakePixelPerfect();
		spDoiRuou3.MakePixelPerfect();
		spUongRuou1.MakePixelPerfect();
		spUongRuou2.MakePixelPerfect();
		spUongRuou3.MakePixelPerfect();
		lbGiaDoiRuou1.text = ConfigManager.instance.OtherConfig.GiaDoiRuou0.ToString();
		lbGiaDoiRuou2.text = ConfigManager.instance.OtherConfig.GiaDoiRuou1.ToString();
		lbGiaDoiRuou3.text = ConfigManager.instance.OtherConfig.GiaDoiRuou2.ToString();
		GetDoiRuouInfoRequest request = new GetDoiRuouInfoRequest();
		GameManager.instance.m_GameClient.RequestGetDoiRuouInfo(request);
		doiRuouGroup.gameObject.SetActive(false);
		uongRuouGroup.gameObject.SetActive(true);
	}

	public void updateView(GetDoiRuouInfoResponse response)
	{
		if (response.ListItem != null && response.ListItem.Count > 0)
		{
			listTanHonName = response.ListItem;
			if (response.ListItem.Count >= 1)
			{
				nhanvat1.Set(response.ListItem[0], 0, -1, false, -1, true);
			}
			if (response.ListItem.Count >= 2)
			{
				nhanvat2.Set(response.ListItem[1], 0, -1, false, -1, true);
			}
			if (response.ListItem.Count >= 3)
			{
				nhanvat3.Set(response.ListItem[2], 0, -1, false, -1, true);
			}
		}
		timeReset = response.ThoiGianReset;
	}

	public void updateMainMenuStatus()
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
			if (gadgetPanelBottom.listKyNgoMenu[i].kyNgoID == MenuButtonKyNgo.KyNgoType.UongRuou)
			{
				if (gadgetPanelBottom.checkThongBaoUongRuou())
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

	public void btnDoiItem1_OnClick(GameObject go)
	{
		if (listTanHonName.Count > 0)
		{
			UserInfo.HonNhanVatData honNhanVatData = GameManager.instance.m_GameClient.UserInfo.HonNhanVatList.Find((UserInfo.HonNhanVatData e) => e.Name == listTanHonName[0]);
			if (honNhanVatData != null && honNhanVatData.Quantity > 0)
			{
				DoiRuouRequest doiRuouRequest = new DoiRuouRequest();
				doiRuouRequest.Idx = 0;
				doiRuouRequest.UseKnb = false;
				doiRuouRequest.TanHonID = honNhanVatData.ID;
				GameManager.instance.m_GameClient.RequestDoiRuou(doiRuouRequest);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoTanHonMess"));
			}
		}
	}

	public void btnDoiItem2_OnClick(GameObject go)
	{
		if (listTanHonName.Count > 0)
		{
			UserInfo.HonNhanVatData honNhanVatData = GameManager.instance.m_GameClient.UserInfo.HonNhanVatList.Find((UserInfo.HonNhanVatData e) => e.Name == listTanHonName[1]);
			if (honNhanVatData != null && honNhanVatData.Quantity > 0)
			{
				DoiRuouRequest doiRuouRequest = new DoiRuouRequest();
				doiRuouRequest.Idx = 1;
				doiRuouRequest.UseKnb = false;
				doiRuouRequest.TanHonID = honNhanVatData.ID;
				GameManager.instance.m_GameClient.RequestDoiRuou(doiRuouRequest);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoTanHonMess"));
			}
		}
	}

	public void btnDoiItem3_OnClick(GameObject go)
	{
		if (listTanHonName.Count > 0)
		{
			UserInfo.HonNhanVatData honNhanVatData = GameManager.instance.m_GameClient.UserInfo.HonNhanVatList.Find((UserInfo.HonNhanVatData e) => e.Name == listTanHonName[2]);
			if (honNhanVatData != null && honNhanVatData.Quantity > 0)
			{
				DoiRuouRequest doiRuouRequest = new DoiRuouRequest();
				doiRuouRequest.Idx = 2;
				doiRuouRequest.UseKnb = false;
				doiRuouRequest.TanHonID = honNhanVatData.ID;
				GameManager.instance.m_GameClient.RequestDoiRuou(doiRuouRequest);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoTanHonMess"));
			}
		}
	}

	public void btnDungKNBItem1_OnClick(GameObject go)
	{
		DoiRuouRequest doiRuouRequest = new DoiRuouRequest();
		doiRuouRequest.Idx = 0;
		doiRuouRequest.UseKnb = true;
		doiRuouRequest.TanHonID = 0;
		GameManager.instance.m_GameClient.RequestDoiRuou(doiRuouRequest);
	}

	public void btnDungKNBItem2_OnClick(GameObject go)
	{
		DoiRuouRequest doiRuouRequest = new DoiRuouRequest();
		doiRuouRequest.Idx = 1;
		doiRuouRequest.UseKnb = true;
		doiRuouRequest.TanHonID = 0;
		GameManager.instance.m_GameClient.RequestDoiRuou(doiRuouRequest);
	}

	public void btnDungKNBItem3_OnClick(GameObject go)
	{
		DoiRuouRequest doiRuouRequest = new DoiRuouRequest();
		doiRuouRequest.Idx = 2;
		doiRuouRequest.UseKnb = true;
		doiRuouRequest.TanHonID = 0;
		GameManager.instance.m_GameClient.RequestDoiRuou(doiRuouRequest);
	}

	public void btnUongRuou1_OnClick(GameObject go)
	{
		if (dataRuou1 != null && dataRuou1.Quantity > 0)
		{
			vatPhamID = dataRuou1.ID;
			PopupSelectNhanVat.Create(onFinishSelectNhanVat, null, false, true);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("KhongCoRuouMess"));
		}
	}

	public void btnUongRuou2_OnClick(GameObject go)
	{
		if (dataRuou2 != null && dataRuou2.Quantity > 0)
		{
			vatPhamID = dataRuou2.ID;
			PopupSelectNhanVat.Create(onFinishSelectNhanVat, null, false, true);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("KhongCoRuouMess"));
		}
	}

	public void btnUongRuou3_OnClick(GameObject go)
	{
		if (dataRuou3 != null && dataRuou3.Quantity > 0)
		{
			vatPhamID = dataRuou3.ID;
			PopupSelectNhanVat.Create(onFinishSelectNhanVat, null, false, true);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("KhongCoRuouMess"));
		}
	}

	public bool onFinishSelectNhanVat(int select_hero)
	{
		if (vatPhamID <= 0 || select_hero <= 0)
		{
			return true;
		}
		detuSelected = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == select_hero);
		UongRuouTieuPhongRequest uongRuouTieuPhongRequest = new UongRuouTieuPhongRequest();
		uongRuouTieuPhongRequest.NhanVatID = select_hero;
		uongRuouTieuPhongRequest.VatPhamID = vatPhamID;
		GameManager.instance.m_GameClient.RequestUongRuouTieuPhong(uongRuouTieuPhongRequest);
		return true;
	}

	public void openPopUpMess(int timeLuc)
	{
		PopUpUongRuou.Create(detuSelected, timeLuc);
	}

	public void btnDoiAm_OnClick(GameObject go)
	{
		doiRuouGroup.gameObject.SetActive(false);
		uongRuouGroup.gameObject.SetActive(true);
	}

	public void btnDoiRuou_OnClick(GameObject go)
	{
		doiRuouGroup.gameObject.SetActive(true);
		uongRuouGroup.gameObject.SetActive(false);
	}

	public void spRuou1_OnClick(GameObject go)
	{
		if (dataRuou1 != null)
		{
			PopUpVatPham.Create(dataRuou1);
			return;
		}
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = new UserInfo.VatPhamTieuThuData();
		vatPhamTieuThuData.Name = "VP_RUOU_TIEM_LUC_0";
		vatPhamTieuThuData.Quantity = 0;
		PopUpVatPham.Create(vatPhamTieuThuData);
	}

	public void spRuou2_OnClick(GameObject go)
	{
		if (dataRuou2 != null)
		{
			PopUpVatPham.Create(dataRuou2);
			return;
		}
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = new UserInfo.VatPhamTieuThuData();
		vatPhamTieuThuData.Name = "VP_RUOU_TIEM_LUC_1";
		vatPhamTieuThuData.Quantity = 0;
		PopUpVatPham.Create(vatPhamTieuThuData);
	}

	public void spRuou3_OnClick(GameObject go)
	{
		if (dataRuou3 != null)
		{
			PopUpVatPham.Create(dataRuou3);
			return;
		}
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = new UserInfo.VatPhamTieuThuData();
		vatPhamTieuThuData.Name = "VP_RUOU_TIEM_LUC_2";
		vatPhamTieuThuData.Quantity = 0;
		PopUpVatPham.Create(vatPhamTieuThuData);
	}

	public void backGround_onClick(GameObject go)
	{
		GadgetPanelBottom gadgetPanelBottom = GUIManager.instance.gadgetPanelBottom;
		if (gadgetPanelBottom != null)
		{
			gadgetPanelBottom.OnClickBtnHome();
		}
	}
}

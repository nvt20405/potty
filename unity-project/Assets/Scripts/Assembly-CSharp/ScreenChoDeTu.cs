using System;
using LitJson;
using Nettention.Proud;
using UnityEngine;

public class ScreenChoDeTu : ScreenBase
{
	public UILabel lbLeftMess;

	public UILabel lbCenterMess;

	public UILabel lbRightMess;

	public UILabel lbLeftPrice;

	public UILabel lbCenterPrice;

	public UILabel lbRightPrice;

	private bool isLeftFree;

	private bool isCenterFree;

	private bool isRightFree;

	public GameObject leftPriceGroup;

	public GameObject rightPriceGroup;

	public GameObject centerPriceGroup;

	private float nextSecond;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			if (GUIManager.instance != null)
			{
				uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
			}
		}
	}

	private void Start()
	{
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
		AudioListener component2 = GUIManager.instance.homeCity.cam.GetComponent<AudioListener>();
		if (!GameManager.instance.isStartTutorial)
		{
			if (component != null)
			{
				component.enabled = false;
			}
			if (component2 != null)
			{
				component2.enabled = true;
			}
		}
		else
		{
			if (component != null)
			{
				component.enabled = true;
			}
			if (component2 != null)
			{
				component2.enabled = false;
			}
		}
		Utils.SetLightMaps("Lightmap/HomeCity/", 2);
		GUIManager.ShowGadgets(6);
		displayInfo();
		gotoMarket();
	}

	public void gotoMarket()
	{
		if (GUIManager.instance.homeCity.IsFightingNienThu)
		{
			(GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain).ThoatDanhNienThu();
		}
		UnityEngine.Vector3 vector = default(UnityEngine.Vector3);
		vector = new UnityEngine.Vector3(-25f, 0f, 10f);
		if (GUIManager.instance.homeCity.mainAvatar != null)
		{
			GUIManager.instance.homeCity.mainAvatar.Teleport(vector);
		}
		GUIManager.instance.homeCity.cam.transform.localPosition = new UnityEngine.Vector3(-25f, 20f, -13f);
		GUIManager.instance.homeCity.cam.GetComponent<CameraMovement>().enabled = false;
		HomeResponse.Position3D position3D = new HomeResponse.Position3D();
		position3D.X = vector.x + (float)UnityEngine.Random.Range(0, 1);
		position3D.Y = vector.y;
		position3D.Z = vector.z + (float)UnityEngine.Random.Range(0, 1);
		GameManager.instance.m_GameClient.C2SProxy.RequestChangeNextPos(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(position3D));
	}

	public void updateTime()
	{
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		DateTime layNhanVat1Time = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.layNhanVat1Time;
		DateTime layNhanVat2Time = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.layNhanVat2Time;
		DateTime layNhanVat3Time = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.layNhanVat3Time;
		int layNhanVat1Num = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.layNhanVat1Num;
		if (layNhanVat1Num < 5)
		{
			if (layNhanVat1Time >= serverTime)
			{
				isLeftFree = false;
				TimeSpan timeSpan = layNhanVat1Time - serverTime;
				lbLeftMess.text = string.Format("{0:00}:{1:00}:{2:00}", Math.Floor(timeSpan.TotalHours), timeSpan.Minutes, timeSpan.Seconds);
				leftPriceGroup.SetActive(true);
			}
			else
			{
				isLeftFree = true;
				lbLeftMess.text = string.Format(Localization.instance.Get("FreeLoaiMotLabel"), layNhanVat1Num);
				leftPriceGroup.SetActive(false);
			}
		}
		else
		{
			isLeftFree = false;
			leftPriceGroup.SetActive(true);
			lbLeftMess.text = string.Format(Localization.instance.Get("FreeLoaiMotLabel"), layNhanVat1Num);
		}
		if (layNhanVat2Time < serverTime)
		{
			lbCenterMess.text = Localization.instance.Get("FreeLabel");
			isCenterFree = true;
			centerPriceGroup.SetActive(false);
		}
		else
		{
			isCenterFree = false;
			centerPriceGroup.SetActive(true);
			TimeSpan timeSpan2 = layNhanVat2Time - serverTime;
			lbCenterMess.text = string.Format("{0:00}:{1:00}:{2:00}", Math.Floor(timeSpan2.TotalHours), timeSpan2.Minutes, timeSpan2.Seconds);
		}
		if (layNhanVat3Time < serverTime)
		{
			lbRightMess.text = Localization.instance.Get("FreeLabel");
			isRightFree = true;
			rightPriceGroup.SetActive(false);
		}
		else
		{
			isRightFree = false;
			rightPriceGroup.SetActive(true);
			TimeSpan timeSpan3 = layNhanVat3Time - serverTime;
			lbRightMess.text = string.Format("{0:00}:{1:00}:{2:00}", Math.Floor(timeSpan3.TotalHours), timeSpan3.Minutes, timeSpan3.Seconds);
		}
	}

	public void displayInfo()
	{
		lbLeftPrice.text = ConfigManager.instance.OtherConfig.GiaLayDeTuLoai1.ToString();
		lbCenterPrice.text = ConfigManager.instance.OtherConfig.GiaLayDeTuLoai2.ToString();
		lbRightPrice.text = ConfigManager.instance.OtherConfig.GiaLayDeTuLoai3.ToString();
		updateTime();
	}

	public void btnThuNhanLeft_OnClick(GameObject go)
	{
		int layNhanVat1Num = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.layNhanVat1Num;
		int giaLayDeTuLoai = ConfigManager.instance.OtherConfig.GiaLayDeTuLoai1;
		if (layNhanVat1Num < 5 && isLeftFree)
		{
			LayDeTuRequest layDeTuRequest = new LayDeTuRequest();
			layDeTuRequest.isFree = isLeftFree;
			layDeTuRequest.loai = LayDeTuRequest.LoaiLayDeTu.Loai1;
			GameManager.instance.m_GameClient.RequestLayDeTu(layDeTuRequest);
		}
		else if (GameManager.instance.m_GameClient.checkKNB(giaLayDeTuLoai))
		{
			LayDeTuRequest layDeTuRequest2 = new LayDeTuRequest();
			layDeTuRequest2.isFree = isLeftFree;
			layDeTuRequest2.loai = LayDeTuRequest.LoaiLayDeTu.Loai1;
			GameManager.instance.m_GameClient.RequestLayDeTu(layDeTuRequest2);
		}
	}

	public void btnThuNhanRight_OnClick(GameObject go)
	{
		if (isRightFree)
		{
			LayDeTuRequest layDeTuRequest = new LayDeTuRequest();
			layDeTuRequest.isFree = isRightFree;
			layDeTuRequest.loai = LayDeTuRequest.LoaiLayDeTu.Loai3;
			GameManager.instance.m_GameClient.RequestLayDeTu(layDeTuRequest);
			return;
		}
		int giaLayDeTuLoai = ConfigManager.instance.OtherConfig.GiaLayDeTuLoai3;
		if (GameManager.instance.m_GameClient.checkKNB(giaLayDeTuLoai))
		{
			LayDeTuRequest layDeTuRequest2 = new LayDeTuRequest();
			layDeTuRequest2.isFree = isRightFree;
			layDeTuRequest2.loai = LayDeTuRequest.LoaiLayDeTu.Loai3;
			GameManager.instance.m_GameClient.RequestLayDeTu(layDeTuRequest2);
		}
	}

	public void btnThuNhanCenter_OnClick(GameObject go)
	{
		if (isCenterFree)
		{
			LayDeTuRequest layDeTuRequest = new LayDeTuRequest();
			layDeTuRequest.isFree = isCenterFree;
			layDeTuRequest.loai = LayDeTuRequest.LoaiLayDeTu.Loai2;
			GameManager.instance.m_GameClient.RequestLayDeTu(layDeTuRequest);
			return;
		}
		int giaLayDeTuLoai = ConfigManager.instance.OtherConfig.GiaLayDeTuLoai2;
		if (GameManager.instance.m_GameClient.checkKNB(giaLayDeTuLoai))
		{
			LayDeTuRequest layDeTuRequest2 = new LayDeTuRequest();
			layDeTuRequest2.isFree = isCenterFree;
			layDeTuRequest2.loai = LayDeTuRequest.LoaiLayDeTu.Loai2;
			GameManager.instance.m_GameClient.RequestLayDeTu(layDeTuRequest2);
		}
	}

	public void updateView(LayDeTuResponse response)
	{
		displayInfo();
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenThuNhanDeTuResult);
		ScreenThuNhanDeTuResult screenThuNhanDeTuResult = GUIManager.getScreen(GAME_SCREEN.ScreenThuNhanDeTuResult) as ScreenThuNhanDeTuResult;
		screenThuNhanDeTuResult.setData(response);
		updateMainMenuView();
	}

	private void updateMainMenuView()
	{
		GadgetPanelBottom gadgetPanelBottom = GUIManager.instance.gadgetPanelBottom;
		if (gadgetPanelBottom != null)
		{
			gadgetPanelBottom.checkDisplayThongBaoCho();
		}
	}

	public void backGround_onClick(GameObject go)
	{
		GadgetPanelBottom gadgetPanelBottom = GUIManager.instance.gadgetPanelBottom;
		if (gadgetPanelBottom != null)
		{
			gadgetPanelBottom.OnClickBtnHome();
			GUIManager.instance.homeCity.cam.GetComponent<CameraMovement>().enabled = true;
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
		GUIManager.instance.homeCity.cam.GetComponent<CameraMovement>().enabled = true;
	}
}

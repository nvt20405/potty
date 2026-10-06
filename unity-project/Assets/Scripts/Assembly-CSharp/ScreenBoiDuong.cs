using UnityEngine;

public class ScreenBoiDuong : ScreenBase
{
	public NhanVatAvatar nhanvatAva;

	public UILabel nhanVatName;

	public UILabel menhValueLabel;

	public UILabel ngoaiValueLabel;

	public UILabel khiValueLabel;

	public UILabel thanValueLabel;

	public UILabel tiemLucValueLabel;

	public UILabel boiDuongValueLabel;

	public UILabel detailSelectedCheckBox;

	public UIButton btnBoiDuong;

	public UILabel motlanValueLabel;

	public UILabel muoilanValueLabel;

	public UILabel motlanCaoCapValueLabel;

	public UILabel muoilanCaoCapValueLabel;

	private GameClient client;

	private int soLuongVangCan;

	private UserInfo.VatPhamTieuThuData boiDuongDanData;

	private StartBoiDuongRequest.LoaiBoiDuong mTypeBoiDuong;

	public UserInfo.HeroData m_HeroData;

	public void Set(UserInfo.HeroData data)
	{
		if (data != null)
		{
			m_HeroData = data;
		}
	}

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

	private void Start()
	{
		UIEventListener.Get(btnBoiDuong.gameObject).onClick = btnBoiDuong_OnClick;
		motlanValueLabel.text = "1 " + Localization.instance.Get("Lan");
		muoilanValueLabel.text = "10 " + Localization.instance.Get("Lan");
		motlanCaoCapValueLabel.text = "1 " + Localization.instance.Get("LanCaoCap") + "2";
		muoilanCaoCapValueLabel.text = "10 " + Localization.instance.Get("LanCaoCap") + "20";
		client = GameManager.instance.m_GameClient;
	}

	public override void OnActive()
	{
		if (m_HeroData != null)
		{
			displayInfo();
		}
	}

	public void displayInfo()
	{
		nhanvatAva.Set(m_HeroData);
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[m_HeroData.Name];
		nhanVatName.text = nhanVatCfg.TenHienThi;
		menhValueLabel.text = m_HeroData.ChiSoGoc.Menh.ToString();
		khiValueLabel.text = m_HeroData.ChiSoGoc.Noi.ToString();
		thanValueLabel.text = m_HeroData.ChiSoGoc.ThanPhap.ToString();
		ngoaiValueLabel.text = m_HeroData.ChiSoGoc.Ngoai.ToString();
		tiemLucValueLabel.text = ConfigManager.instance.GetTiemLucConLai(m_HeroData).ToString();
		boiDuongDanData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_BOI_DUONG_DAN");
		if (boiDuongDanData != null)
		{
			boiDuongValueLabel.text = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_BOI_DUONG_DAN").Quantity.ToString();
		}
		else
		{
			boiDuongValueLabel.text = "0";
		}
		soLuongVangCan = 0;
	}

	private void Update()
	{
	}

	public void MotLan_OnClick(bool isActive)
	{
		if (isActive)
		{
			soLuongVangCan = 0;
			mTypeBoiDuong = StartBoiDuongRequest.LoaiBoiDuong.BoiDuong1Lan;
			detailSelectedCheckBox.text = string.Format(Localization.instance.Get("SoLuongBoiDuongDanCanLabel"), 5);
			EGDebug.Log("boi duong 1 lan onclick: " + mTypeBoiDuong);
		}
	}

	public void MotLanCaoCap_OnClick(bool isActive)
	{
		if (isActive)
		{
			soLuongVangCan = 2;
			mTypeBoiDuong = StartBoiDuongRequest.LoaiBoiDuong.BoiDuong1LanCaoCap;
			detailSelectedCheckBox.text = string.Format(Localization.instance.Get("SoLuongBoiDuongDanCanLabel"), 5) + " " + string.Format(Localization.instance.Get("SoLuongKNBCan"), 2);
			EGDebug.Log("boi duong 1 lan cao cap onclick: " + mTypeBoiDuong);
		}
	}

	public void MuoiLan_OnClick(bool isActive)
	{
		if (isActive)
		{
			soLuongVangCan = 0;
			mTypeBoiDuong = StartBoiDuongRequest.LoaiBoiDuong.BoiDuong10Lan;
			detailSelectedCheckBox.text = string.Format(Localization.instance.Get("SoLuongBoiDuongDanCanLabel"), 50);
			EGDebug.Log("boi duong 10 lan onclick: " + mTypeBoiDuong);
		}
	}

	public void MuoiLanCaoCap_OnClick(bool isActive)
	{
		if (isActive)
		{
			soLuongVangCan = 20;
			mTypeBoiDuong = StartBoiDuongRequest.LoaiBoiDuong.BoiDuong10LanCaoCap;
			detailSelectedCheckBox.text = string.Format(Localization.instance.Get("SoLuongBoiDuongDanCanLabel"), 50) + " " + string.Format(Localization.instance.Get("SoLuongKNBCan"), 20);
			EGDebug.Log("boi duong 10 lan cao cap onclick: " + mTypeBoiDuong);
		}
	}

	public void btnBack_onClick()
	{
		if (GUIManager.instance.LastScreen == GAME_SCREEN.ScreenDoiHinh)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDoiHinh);
		}
		else if (GUIManager.instance.LastScreen == GAME_SCREEN.ScreenTayNai)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTayNai);
		}
		else
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDeTu);
		}
	}

	public void btnBoiDuong_OnClick(GameObject go)
	{
		if (GameManager.instance.m_GameClient.checkKNB(soLuongVangCan))
		{
			boiDuongDanData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_BOI_DUONG_DAN");
			if (boiDuongDanData == null)
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoBoiDuongDanMess"));
			}
			else if (boiDuongDanData != null && m_HeroData != null)
			{
				client.RequestStartBoiDuong(m_HeroData.HID, boiDuongDanData.ID, mTypeBoiDuong);
			}
		}
	}

	public void openScreenBoiDuongConfirm(StartBoiDuongResponse response)
	{
		ScreenBoiDuongConfirm screenBoiDuongConfirm = GUIManager.getScreen(GAME_SCREEN.ScreenBoiDuongConfirm) as ScreenBoiDuongConfirm;
		screenBoiDuongConfirm.Set(m_HeroData, response);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBoiDuongConfirm);
	}

	public void btnHelp_OnClick()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(0, 3);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
		EGDebug.Log("btnHelp_OnClick");
	}
}

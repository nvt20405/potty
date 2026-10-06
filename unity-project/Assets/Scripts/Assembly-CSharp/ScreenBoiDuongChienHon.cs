using UnityEngine;

public class ScreenBoiDuongChienHon : ScreenBase
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

	private GameClient client;

	private int soLuongVangCan;

	private UserInfo.VatPhamTieuThuData dichCanDanData;

	private StartBoiDuongChienHonRequest.LoaiBoiDuong mTypeBoiDuong;

	private int m_chienHonId;

	private int m_heroId;

	public UserInfo.HeroData heroData { get; set; }

	public UserInfo.ChienHon chienHonData { get; set; }

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
		client = GameManager.instance.m_GameClient;
	}

	public override void OnActive()
	{
		if (m_chienHonId <= 0 || m_heroId <= 0)
		{
			return;
		}
		if (GameManager.instance.m_GameClient.UserInfo.ChienHonList != null)
		{
			chienHonData = GameManager.instance.m_GameClient.UserInfo.ChienHonList.Find((UserInfo.ChienHon e) => e.ID == m_chienHonId);
		}
		if (GameManager.instance.m_GameClient.UserInfo.HeroList != null)
		{
			heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == m_heroId);
		}
		if (chienHonData != null && heroData != null)
		{
			displayInfo(chienHonData, heroData);
		}
	}

	public void displayInfo(UserInfo.ChienHon chienHonData, UserInfo.HeroData hero)
	{
		this.chienHonData = chienHonData;
		heroData = hero;
		if (this.chienHonData != null)
		{
			m_chienHonId = this.chienHonData.ID;
		}
		if (heroData != null)
		{
			m_heroId = heroData.HID;
		}
		nhanvatAva.Set(heroData.Name, this.chienHonData.DotPha, this.chienHonData.Level);
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[chienHonData.Name];
		nhanVatName.text = nhanVatCfg.TenHienThi;
		menhValueLabel.text = chienHonData.Menh.ToString();
		khiValueLabel.text = chienHonData.Noi.ToString();
		thanValueLabel.text = chienHonData.ThanPhap.ToString();
		ngoaiValueLabel.text = chienHonData.Ngoai.ToString();
		int tiemLucConLai = ConfigManager.instance.GetTiemLucConLai(chienHonData);
		tiemLucValueLabel.text = tiemLucConLai.ToString();
		dichCanDanData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_DICH_CAN_DAN");
		if (dichCanDanData != null)
		{
			boiDuongValueLabel.text = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_DICH_CAN_DAN").Quantity.ToString();
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

	public void MotLanCaoCap_OnClick(bool isActive)
	{
		if (isActive)
		{
			soLuongVangCan = 2;
			mTypeBoiDuong = StartBoiDuongChienHonRequest.LoaiBoiDuong.BoiDuong1LanCaoCap;
			detailSelectedCheckBox.text = string.Format(Localization.instance.Get("SoLuongBoiDuongDanCanLabel"), 5) + " " + string.Format(Localization.instance.Get("SoLuongKNBCan"), 2);
			EGDebug.Log("boi duong 1 lan cao cap onclick: " + mTypeBoiDuong);
		}
	}

	public void MuoiLanCaoCap_OnClick(bool isActive)
	{
		if (isActive)
		{
			soLuongVangCan = 20;
			mTypeBoiDuong = StartBoiDuongChienHonRequest.LoaiBoiDuong.BoiDuong10LanCaoCap;
			detailSelectedCheckBox.text = string.Format(Localization.instance.Get("SoLuongBoiDuongDanCanLabel"), 50) + " " + string.Format(Localization.instance.Get("SoLuongKNBCan"), 20);
			EGDebug.Log("boi duong 10 lan cao cap onclick: " + mTypeBoiDuong);
		}
	}

	public void btnBack_onClick()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenChienHon);
		ScreenChienHon screenChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenChienHon) as ScreenChienHon;
		screenChienHon.displayNhanVat3D(chienHonData, heroData);
	}

	public void btnBoiDuong_OnClick(GameObject go)
	{
		if (GameManager.instance.m_GameClient.checkKNB(soLuongVangCan))
		{
			dichCanDanData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_DICH_CAN_DAN");
			if (dichCanDanData == null)
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoDichCanDanDanMess"));
			}
			else
			{
				client.RequestStartBoiDuongChienHon(chienHonData.ID, mTypeBoiDuong);
			}
		}
	}

	public void openScreenBoiDuongConfirm(StartBoiDuongChienHonResponse response)
	{
		ScreenBoiDuongChienHonConfirm screenBoiDuongChienHonConfirm = GUIManager.getScreen(GAME_SCREEN.ScreenBoiDuongChienHonConfirm) as ScreenBoiDuongChienHonConfirm;
		screenBoiDuongChienHonConfirm.displayInfo(chienHonData, heroData, response);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBoiDuongChienHonConfirm);
	}
}

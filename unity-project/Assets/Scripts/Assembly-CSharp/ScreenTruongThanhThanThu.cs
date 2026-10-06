using UnityEngine;

public class ScreenTruongThanhThanThu : ScreenBase
{
	private UserInfo.PetInfo thanthu;

	public ThanThuAvatar thanthuAvatar;

	public UILabel thanThuName;

	public UISlider TruongThanhBar1;

	public UILabel TruongThanhLabel1;

	public UISlider TruongThanhBar2;

	public UILabel TruongThanhLabel2;

	public UISprite ChiSo1IconTruoc;

	public UISprite ChiSo2IconTruoc;

	public UILabel ChiSo1Truoc;

	public UILabel ChiSo2Truoc;

	public UISprite ChiSo1IconSau;

	public UISprite ChiSo2IconSau;

	public UILabel ChiSo1Sau;

	public UILabel ChiSo2Sau;

	public UILabel NguyenKhiDanLabel;

	public UILabel BacLabel;

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

	public void Set()
	{
		thanthu = GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == thanthu.ID);
		thanThuName.text = Localization.instance.Get(thanthu.codename);
		UserInfo.PetInfo petInfo = new UserInfo.PetInfo(thanthu);
		petInfo.growRate = thanthu.GetNextTruongThanh();
		thanthuAvatar.Set(thanthu);
		TruongThanhBar1.sliderValue = thanthu.growRate;
		TruongThanhLabel1.text = (float)(int)(thanthu.growRate * 1000f) / 10f + "%";
		TruongThanhBar2.sliderValue = petInfo.growRate;
		TruongThanhLabel2.text = (float)(int)(petInfo.growRate * 1000f) / 10f + "%";
		if (thanthu.GetChiSo1() == ChiSoCoBan.Menh)
		{
			ChiSo1IconTruoc.spriteName = "icon_mau";
			ChiSo1IconSau.spriteName = "icon_mau";
			ChiSo1Truoc.text = thanthu.GetHP().ToString();
			ChiSo1Sau.text = petInfo.GetHP().ToString();
		}
		else if (thanthu.GetChiSo1() == ChiSoCoBan.Ngoai)
		{
			ChiSo1IconTruoc.spriteName = "icon_cong";
			ChiSo1IconSau.spriteName = "icon_cong";
			ChiSo1Truoc.text = thanthu.GetCong().ToString();
			ChiSo1Sau.text = petInfo.GetCong().ToString();
		}
		else
		{
			ChiSo1IconTruoc.spriteName = "icon_thu";
			ChiSo1IconSau.spriteName = "icon_thu";
			ChiSo1Truoc.text = thanthu.GetThu().ToString();
			ChiSo1Sau.text = petInfo.GetThu().ToString();
		}
		if (thanthu.GetChiSo2() == ChiSoCoBan.Menh)
		{
			ChiSo2IconTruoc.spriteName = "icon_mau";
			ChiSo2IconSau.spriteName = "icon_mau";
			ChiSo2Truoc.text = thanthu.GetHP().ToString();
			ChiSo2Sau.text = petInfo.GetHP().ToString();
		}
		else if (thanthu.GetChiSo2() == ChiSoCoBan.ThanPhap)
		{
			ChiSo2IconTruoc.spriteName = "icon_thu";
			ChiSo2IconSau.spriteName = "icon_thu";
			ChiSo2Truoc.text = thanthu.GetThu().ToString();
			ChiSo2Sau.text = petInfo.GetThu().ToString();
		}
		else
		{
			ChiSo2IconTruoc.spriteName = "icon_noi";
			ChiSo2IconSau.spriteName = "icon_noi";
			ChiSo2Truoc.text = thanthu.GetMP().ToString();
			ChiSo2Sau.text = petInfo.GetMP().ToString();
		}
		NguyenKhiDanLabel.text = ConfigManager.instance.OtherConfig.NguyenKhiDanTruongThanh.ToString();
		BacLabel.text = ConfigManager.instance.OtherConfig.BacTruongThanh.ToString();
	}

	public void Set(int thanthuId)
	{
		thanthu = GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == thanthuId);
		Set();
	}

	public void OnTruongThanhBtn()
	{
		GameManager.instance.m_GameClient.RequestTruongThanhThanThu(thanthu.ID);
	}

	public void OnBackBtn()
	{
		GUIManager.instance.SetScreen(GUIManager.instance.LastScreen);
	}

	public void OnHelpBtn()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(11, 3);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
	}
}

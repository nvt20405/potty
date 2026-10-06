using System.Collections.Generic;
using UnityEngine;

public class ScreenTruyenCongThanThu : ScreenBase
{
	private UserInfo.PetInfo thanthu;

	public ThanThuAvatar thanthuAvatar;

	public UILabel thanThuName;

	public UISlider TruongThanhBar2;

	public UILabel TruongThanhLabel2;

	public UISprite ChiSo1IconSau;

	public UISprite ChiSo2IconSau;

	public UILabel ChiSo1Sau;

	public UILabel ChiSo2Sau;

	public UISprite SkillSau;

	public UISprite SkillSauBg;

	public UILabel QualitySau;

	public UILabel ExpSau;

	public ThanThuAvatar thanthuSau;

	public ThanThuAvatar thanthuHiSinh;

	public int selectedThanthu;

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

	public void SyncWithNetwork()
	{
		Set(thanthu.ID, 0);
	}

	private void Set(int thanthuHiSinhId = 0)
	{
		UserInfo.PetInfo petInfo = GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == thanthuHiSinhId);
		thanthu = GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == thanthu.ID);
		thanThuName.text = Localization.instance.Get(thanthu.codename);
		UserInfo.PetInfo petInfo2 = new UserInfo.PetInfo(thanthu);
		if (petInfo != null)
		{
			petInfo2 = new UserInfo.PetInfo(petInfo);
			petInfo2.codename = thanthu.codename;
			petInfo2.heso1 = thanthu.heso1;
			petInfo2.heso2 = thanthu.heso2;
			petInfo2.petType = thanthu.petType;
			petInfo2.skill = thanthu.skill;
			selectedThanthu = thanthuHiSinhId;
		}
		else
		{
			selectedThanthu = 0;
		}
		thanthuAvatar.Set(thanthu);
		thanthuSau.Set(petInfo2);
		TruongThanhBar2.sliderValue = petInfo2.growRate;
		TruongThanhLabel2.text = (float)(int)(petInfo2.growRate * 1000f) / 10f + "%";
		if (thanthu.GetChiSo1() == ChiSoCoBan.Menh)
		{
			ChiSo1IconSau.spriteName = "icon_mau";
			ChiSo1Sau.text = petInfo2.GetHP().ToString();
		}
		else if (thanthu.GetChiSo1() == ChiSoCoBan.Ngoai)
		{
			ChiSo1IconSau.spriteName = "icon_cong";
			ChiSo1Sau.text = petInfo2.GetCong().ToString();
		}
		else
		{
			ChiSo1IconSau.spriteName = "icon_thu";
			ChiSo1Sau.text = petInfo2.GetThu().ToString();
		}
		if (thanthu.GetChiSo2() == ChiSoCoBan.Menh)
		{
			ChiSo2IconSau.spriteName = "icon_mau";
			ChiSo2Sau.text = petInfo2.GetHP().ToString();
		}
		else if (thanthu.GetChiSo2() == ChiSoCoBan.ThanPhap)
		{
			ChiSo2IconSau.spriteName = "icon_thu";
			ChiSo2Sau.text = petInfo2.GetThu().ToString();
		}
		else
		{
			ChiSo2IconSau.spriteName = "icon_noi";
			ChiSo2Sau.text = petInfo2.GetMP().ToString();
		}
		SkillSau.spriteName = petInfo2.skill;
		SkillSauBg.spriteName = "bkg_avatar" + (int)((petInfo2.Quality != UserInfo.PetInfo.PetQuality.TRUYEN_THUYET) ? petInfo2.Quality : ((UserInfo.PetInfo.PetQuality)5));
		ExpSau.text = Localization.instance.Get("Exp") + petInfo2.curExp + "/" + petInfo2.maxExp;
		QualitySau.text = ConfigManager.instance.GetMaMauByQuality(petInfo2.Quality) + Localization.instance.Get(petInfo2.Quality.ToString()) + "[-]";
		if (petInfo == null)
		{
			ReleaseAllSlot();
		}
		else
		{
			thanthuHiSinh.Set(petInfo);
		}
	}

	public void Set(int thanthuId, int thanthuHiSinhId)
	{
		thanthu = GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == thanthuId);
		Set(thanthuHiSinhId);
	}

	public void OnBackBtn()
	{
		GUIManager.instance.SetScreen(GUIManager.instance.LastScreen);
	}

	public void OnSelectThanThu(GameObject button)
	{
		List<int> list = new List<int>();
		list.Add(thanthu.ID);
		PopupSelectThanThu.CreateByNormal(SetThanThuSlot1, list);
	}

	public void ReleaseAllSlot()
	{
		thanthuHiSinh.Release();
	}

	public bool SetThanThuSlot1(int id)
	{
		Set(id);
		thanthuHiSinh.Set(GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == id));
		selectedThanthu = id;
		return true;
	}

	public void btnBack_OnClick()
	{
		GUIManager.instance.SetScreen(GUIManager.instance.LastScreen);
	}

	public void OnTruyenCongButton()
	{
		if (selectedThanthu != 0)
		{
			PopupYesNo.Create(string.Format(Localization.instance.Get("ConfirmTruyenCongThanThu"), Localization.instance.Get(GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == selectedThanthu).codename), Localization.instance.Get(GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == selectedThanthu).Quality.ToString())), Localization.instance.Get("DongY"), Localization.instance.Get("TuChoi"), TruyenCong, null);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ChuaChonThanThu"));
		}
	}

	public void TruyenCong()
	{
		GameManager.instance.m_GameClient.RequestTruyenCongThanThu(thanthu.ID, selectedThanthu);
	}

	public void ShowThanThu()
	{
		PopupThanThuInfo.Create(thanthu, false);
	}

	public void OnSkillClick()
	{
		UserInfo.VoCongData voCongData = new UserInfo.VoCongData();
		voCongData.Name = thanthu.skill;
		if (selectedThanthu > 0)
		{
			voCongData.Level = (int)(GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == selectedThanthu).Quality + 1);
		}
		else
		{
			voCongData.Level = (int)(thanthu.Quality + 1);
		}
		PopupVoCong.CreateByNormalScreen(null, voCongData, voCongData.Level);
	}

	public void OnHelpBtn()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(11, 6);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
	}
}

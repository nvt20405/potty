using System.Collections.Generic;
using UnityEngine;

public class ScreenNangPhamThanThu : ScreenBase
{
	public enum STATE
	{
		SUCCESS = 0,
		FAIL = 1,
		BEGIN = 2
	}

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

	public UISprite SkillTruoc;

	public UISprite SkillSau;

	public UISprite SkillTruocBg;

	public UISprite SkillSauBg;

	public UILabel QualityTruoc;

	public UILabel QualitySau;

	public UILabel NguyenKhiDanLabel;

	public UILabel BacLabel;

	public GameObject ThanhCongLabel;

	public GameObject ThatBaiLabel;

	public GameObject XacSuatGrp;

	public int ThuHonCount;

	public UILabel ThuHonCountLabel;

	public ThanThuAvatar thanthuHiSinh1;

	public ThanThuAvatar thanthuHiSinh2;

	public ThanThuAvatar thanthuHiSinh3;

	public ThanThuAvatar thanthuHiSinh4;

	public List<ThanThuAvatar> thanthuHiSinh = new List<ThanThuAvatar>();

	public List<int> selectedThanthu;

	private STATE curState = STATE.BEGIN;

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

	public void Set(STATE state)
	{
		thanthu = GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == thanthu.ID);
		thanThuName.text = Localization.instance.Get(thanthu.codename);
		UserInfo.PetInfo petInfo = new UserInfo.PetInfo(thanthu);
		if (thanthu.Quality != UserInfo.PetInfo.PetQuality.TRUYEN_THUYET)
		{
			petInfo.growRate = 0f;
			petInfo.Quality = thanthu.Quality + 1;
		}
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
		SkillSau.spriteName = petInfo.skill;
		SkillTruoc.spriteName = thanthu.skill;
		SkillSauBg.spriteName = "bkg_avatar" + (int)((petInfo.Quality != UserInfo.PetInfo.PetQuality.TRUYEN_THUYET) ? petInfo.Quality : ((UserInfo.PetInfo.PetQuality)5));
		SkillTruocBg.spriteName = "bkg_avatar" + (int)((thanthu.Quality != UserInfo.PetInfo.PetQuality.TRUYEN_THUYET) ? thanthu.Quality : ((UserInfo.PetInfo.PetQuality)5));
		QualitySau.text = ConfigManager.instance.GetMaMauByQuality(petInfo.Quality) + Localization.instance.Get(petInfo.Quality.ToString()) + "[-]";
		QualityTruoc.text = ConfigManager.instance.GetMaMauByQuality(thanthu.Quality) + Localization.instance.Get(thanthu.Quality.ToString()) + "[-]";
		if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_THU_HON"))
		{
			ThuHonCountLabel.text = "0/" + GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_THU_HON").Quantity;
		}
		else
		{
			ThuHonCountLabel.text = "0/0";
		}
		ThuHonCount = 0;
		ReleaseAllSlot();
		selectedThanthu = new List<int>();
		curState = STATE.BEGIN;
		switch (state)
		{
		case STATE.BEGIN:
		{
			XacSuatGrp.SetActive(true);
			ThanhCongLabel.SetActive(false);
			ThatBaiLabel.SetActive(false);
			int num = 75 - (int)thanthu.Quality * 5;
			num += ThuHonCount * (int)(4 - thanthu.Quality);
			XacSuatGrp.GetComponent<UISlider>().sliderValue = (float)num / 100f;
			XacSuatGrp.transform.Find("XacSuat").GetComponent<UILabel>().text = num + "%";
			break;
		}
		case STATE.SUCCESS:
			XacSuatGrp.SetActive(false);
			ThanhCongLabel.SetActive(true);
			ThatBaiLabel.SetActive(false);
			break;
		case STATE.FAIL:
			XacSuatGrp.SetActive(false);
			ThanhCongLabel.SetActive(false);
			ThatBaiLabel.SetActive(true);
			break;
		}
	}

	private void UpdateXacSuat()
	{
		XacSuatGrp.SetActive(true);
		ThanhCongLabel.SetActive(false);
		ThatBaiLabel.SetActive(false);
		float num = 75 - (int)thanthu.Quality * 5;
		num += (float)ThuHonCount * (16f / Mathf.Pow(4f, (float)thanthu.Quality));
		XacSuatGrp.GetComponent<UISlider>().sliderValue = num / 100f;
		XacSuatGrp.transform.Find("XacSuat").GetComponent<UILabel>().text = num + "%";
	}

	public void Set(int thanthuId, STATE state)
	{
		thanthu = GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == thanthuId);
		Set(state);
	}

	public void OnBackBtn()
	{
		GUIManager.instance.SetScreen(GUIManager.instance.LastScreen);
	}

	public void OnSelectThanThu(GameObject button)
	{
		List<int> list = new List<int>();
		foreach (UserInfo.PetInfo item in GameManager.instance.m_GameClient.UserInfo.ListThanThu.FindAll((UserInfo.PetInfo p) => p.Quality != thanthu.Quality))
		{
			list.Add(item.ID);
		}
		list.Add(GameManager.instance.m_GameClient.UserInfo.Gamer.curThanThu);
		list.Add(thanthu.ID);
		PopupSelectMultiThanThu.Create(SelectThanThu, list, selectedThanthu, 4);
	}

	public void ReleaseAllSlot()
	{
		thanthuHiSinh1.Release();
		thanthuHiSinh2.Release();
		thanthuHiSinh3.Release();
		thanthuHiSinh4.Release();
	}

	public bool SelectThanThu(List<int> selectedList)
	{
		selectedThanthu = selectedList;
		int i;
		for (i = 0; i < selectedThanthu.Count; i++)
		{
			thanthuHiSinh[i].Set(GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == selectedThanthu[i]));
		}
		for (int num = selectedThanthu.Count; num < 4; num++)
		{
			thanthuHiSinh[num].Release();
		}
		return true;
	}

	public bool SetThanThuSlot1(int id)
	{
		thanthuHiSinh1.Set(GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == id));
		selectedThanthu.Add(id);
		return true;
	}

	public bool SetThanThuSlot2(int id)
	{
		thanthuHiSinh2.Set(GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == id));
		selectedThanthu.Add(id);
		return true;
	}

	public bool SetThanThuSlot3(int id)
	{
		thanthuHiSinh3.Set(GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == id));
		selectedThanthu.Add(id);
		return true;
	}

	public bool SetThanThuSlot4(int id)
	{
		thanthuHiSinh4.Set(GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == id));
		selectedThanthu.Add(id);
		return true;
	}

	public void AddThuHon()
	{
		if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_THU_HON") && GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_THU_HON").Quantity >= ThuHonCount + 1)
		{
			float num = 75 - (int)thanthu.Quality * 5;
			num += (float)ThuHonCount * (16f / Mathf.Pow(4f, (float)thanthu.Quality));
			if (!(num >= 100f))
			{
				ThuHonCount++;
				ThuHonCountLabel.text = ThuHonCount + "/" + GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_THU_HON").Quantity;
				UpdateXacSuat();
			}
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("SoLuongVatPhamKhongDuDung"));
		}
	}

	public void GiamThuHon()
	{
		if (ThuHonCount > 0)
		{
			ThuHonCount--;
			ThuHonCountLabel.text = ThuHonCount + "/" + GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_THU_HON").Quantity;
			UpdateXacSuat();
		}
	}

	public void NangPhamButton()
	{
		if (selectedThanthu.Count == 4)
		{
			GameManager.instance.m_GameClient.RequestNangPhamThanThu(thanthu.ID, selectedThanthu[0], selectedThanthu[1], selectedThanthu[2], selectedThanthu[3], ThuHonCount);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ChuaChon4ThanThu"));
		}
	}

	public void btnBack_OnClick()
	{
		GUIManager.instance.SetScreen(GUIManager.instance.LastScreen);
	}

	public void OnHelpBtn()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(11, 4);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
	}

	public void OnNextSkillBtn()
	{
		UserInfo.VoCongData voCongData = new UserInfo.VoCongData();
		voCongData.Name = thanthu.skill;
		voCongData.Level = (int)(thanthu.Quality + 2);
		PopupVoCong.CreateByNormalScreen(null, voCongData, voCongData.Level);
	}

	public void OnCurSkillBtn()
	{
		UserInfo.VoCongData voCongData = new UserInfo.VoCongData();
		voCongData.Name = thanthu.skill;
		voCongData.Level = (int)(thanthu.Quality + 1);
		PopupVoCong.CreateByNormalScreen(null, voCongData, voCongData.Level);
	}
}

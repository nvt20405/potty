using System.Collections.Generic;
using UnityEngine;

public class PopupThanThuInfo : MonoBehaviour
{
	public static PopupThanThuInfo instance;

	public UILabel CongLabel;

	public UILabel ThuLabel;

	public UILabel MauLabel;

	public UILabel NoiLabel;

	public UILabel LevelLabel;

	public UISprite _ThanThuAvatar;

	public UILabel ThanThuName;

	public UILabel ThanThuDesc;

	public UILabel SkillDesc;

	public UISprite ThanThuSkill;

	public UISprite thanthuBg;

	public UISprite skillBg;

	public UISprite lvlBg;

	public GameObject buttonGrp;

	private UserInfo.PetInfo thanthu;

	public GameObject thonpheBtn;

	public GameObject truongThanhBtn;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnCloseClick()
	{
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(UserInfo.PetInfo thanthu, bool isOwner)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupThanThuInfo"))).GetComponent<PopupThanThuInfo>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set(thanthu, isOwner);
	}

	private void Set(UserInfo.PetInfo thanthu, bool isOwner)
	{
		this.thanthu = thanthu;
		CongLabel.text = thanthu.GetCong().ToString();
		ThuLabel.text = thanthu.GetThu().ToString();
		MauLabel.text = thanthu.GetHP().ToString();
		NoiLabel.text = thanthu.GetMP().ToString();
		LevelLabel.text = thanthu.level.ToString();
		_ThanThuAvatar.spriteName = thanthu.codename + "_" + (int)(thanthu.Quality + 1);
		ThanThuName.text = Localization.instance.Get(thanthu.codename);
		ThanThuDesc.text = Localization.instance.Get(thanthu.codename + "_DESC");
		thanthuBg.spriteName = "bkg_avatar" + (int)((thanthu.Quality != UserInfo.PetInfo.PetQuality.TRUYEN_THUYET) ? thanthu.Quality : ((UserInfo.PetInfo.PetQuality)5));
		skillBg.spriteName = "bkg_avatar" + (int)((thanthu.Quality != UserInfo.PetInfo.PetQuality.TRUYEN_THUYET) ? thanthu.Quality : ((UserInfo.PetInfo.PetQuality)5));
		UISprite uISprite = lvlBg;
		int quality = (int)thanthu.Quality;
		uISprite.spriteName = "hang" + quality + "_tl1_lvl_bkg";
		SkillDesc.text = string.Format(ConfigManager.instance.m_dicVCs[thanthu.skill].Mota2, ConfigManager.instance.m_dicVCs[thanthu.skill].ChiSo1CoSo((int)(thanthu.Quality + 1)));
		ThanThuSkill.spriteName = thanthu.skill;
		buttonGrp.SetActive(isOwner);
		if (isOwner)
		{
			if (thanthu.growRate == 1f && thanthu.Quality == UserInfo.PetInfo.PetQuality.TRUYEN_THUYET)
			{
				thonpheBtn.SetActive(true);
				truongThanhBtn.SetActive(false);
			}
			else
			{
				thonpheBtn.SetActive(false);
				truongThanhBtn.SetActive(true);
			}
		}
	}

	public void OnDoiThanThuClick()
	{
		PopupSelectThanThu.CreateByNormal(DoiThanThuRequest, new List<int> { thanthu.ID });
	}

	public bool DoiThanThuRequest(int id)
	{
		GameManager.instance.m_GameClient.RequestDoiThanThu(id);
		DestroyPopup();
		return true;
	}

	public void OnTruongThanh()
	{
		ScreenTruongThanhThanThu screenTruongThanhThanThu = GUIManager.getScreen(GAME_SCREEN.ScreenTruongThanhThanThu) as ScreenTruongThanhThanThu;
		screenTruongThanhThanThu.Set(thanthu.ID);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTruongThanhThanThu);
		DestroyPopup();
	}

	public void OnThonPhe()
	{
		ScreenThonPheThanThu screenThonPheThanThu = GUIManager.getScreen(GAME_SCREEN.ScreenThonPheThanThu) as ScreenThonPheThanThu;
		screenThonPheThanThu.Set(thanthu.ID);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenThonPheThanThu);
		DestroyPopup();
	}
}

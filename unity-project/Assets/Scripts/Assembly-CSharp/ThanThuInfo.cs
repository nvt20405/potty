using UnityEngine;

public class ThanThuInfo : MonoBehaviour
{
	public UILabel CongLabel;

	public UILabel ThuLabel;

	public UILabel HPLabel;

	public UILabel MPLabel;

	public UILabel ThanThuName;

	public UISprite SkillSprite;

	public UISprite SkillBg;

	public UILabel LevelLabel;

	public UILabel QualityLabel;

	public UISprite QualitySprite;

	public UISprite QualitySprite2;

	public UISlider CurExpBar;

	public UILabel CurExpLabel;

	private UserInfo.PetInfo thanthu;

	private bool isOwner;

	public GameObject ThanThuModel;

	public GameObject ThanThu3DRoot;

	public void Set(UserInfo.PetInfo thanthu, bool isOwner)
	{
		if (ThanThuModel != null)
		{
			Object.Destroy(ThanThuModel);
		}
		string text = "_01";
		if (thanthu.Quality == UserInfo.PetInfo.PetQuality.PHO_THONG)
		{
			text = "_01";
		}
		if (thanthu.Quality == UserInfo.PetInfo.PetQuality.UU_TU)
		{
			text = "_01";
		}
		if (thanthu.Quality == UserInfo.PetInfo.PetQuality.TRAC_VIET)
		{
			text = "_02";
		}
		if (thanthu.Quality == UserInfo.PetInfo.PetQuality.HUYEN_THOAI)
		{
			text = "_03";
		}
		if (thanthu.Quality == UserInfo.PetInfo.PetQuality.TRUYEN_THUYET)
		{
			text = "_04";
		}
		Object obj = Object.Instantiate(Resources.Load("ThanThu/" + thanthu.codename + text, typeof(GameObject)));
		ThanThuModel = (GameObject)((obj is GameObject) ? obj : null);
		if (ThanThuModel != null)
		{
			ThanThuModel.transform.parent = ThanThu3DRoot.transform;
			ThanThuModel.transform.localScale = Vector3.one;
			ThanThuModel.transform.localPosition = Vector3.zero;
			ThanThuModel.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
			if (thanthu.codename.Contains("PHUONG_HOANG"))
			{
				ThanThuModel.transform.Find(thanthu.codename + text).transform.localPosition = new Vector3(0f, -1f, 0f);
			}
			else if (thanthu.codename.Contains("THAN_LONG"))
			{
				ThanThuModel.transform.Find(thanthu.codename + text).transform.localPosition = new Vector3(0f, -1.83f, 0f);
			}
		}
		this.isOwner = isOwner;
		this.thanthu = thanthu;
		CongLabel.text = thanthu.GetCong().ToString();
		ThuLabel.text = thanthu.GetThu().ToString();
		HPLabel.text = thanthu.GetHP().ToString();
		MPLabel.text = thanthu.GetMP().ToString();
		ThanThuName.text = Localization.instance.Get(thanthu.codename);
		SkillSprite.spriteName = thanthu.skill;
		SkillBg.spriteName = "bkg_avatar" + (int)((thanthu.Quality != UserInfo.PetInfo.PetQuality.TRUYEN_THUYET) ? thanthu.Quality : ((UserInfo.PetInfo.PetQuality)5));
		LevelLabel.text = thanthu.level.ToString();
		QualityLabel.text = Localization.instance.Get(thanthu.Quality.ToString());
		QualitySprite.spriteName = "CapDo_" + (int)(thanthu.Quality + 1);
		QualitySprite2.spriteName = "CapDo_" + (int)(thanthu.Quality + 1);
		CurExpLabel.text = thanthu.curExp + "/" + thanthu.maxExp;
		CurExpBar.sliderValue = (float)thanthu.curExp / (float)thanthu.maxExp;
	}

	public void OnAvatarClick()
	{
		if (isOwner)
		{
			PopupThanThuInfo.Create(thanthu, isOwner);
		}
	}

	public void OnSkillClick()
	{
		UserInfo.VoCongData voCongData = new UserInfo.VoCongData();
		voCongData.Name = thanthu.skill;
		voCongData.Level = (int)(thanthu.Quality + 1);
		PopupVoCong.CreateByNormalScreen(null, voCongData, voCongData.Level);
	}

	public void OnHelpBtn()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(11, 0);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
	}
}

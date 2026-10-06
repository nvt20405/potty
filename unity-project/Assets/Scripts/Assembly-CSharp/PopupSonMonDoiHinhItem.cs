using UnityEngine;

public class PopupSonMonDoiHinhItem : MonoBehaviour
{
	public UILabel TenCongTrinh;

	public GameObject SwapBtn;

	public UISprite AvatarCongTrinh;

	public UISprite AvatarDeTu;

	public UISprite BgDeTu;

	public UserInfo.SonMonBuildingInfo CongTrinh;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Set(UserInfo.SonMonBuildingInfo congtrinh, int heroID)
	{
		UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData hero) => hero.HID == heroID);
		if (heroData == null)
		{
			base.gameObject.SetActive(false);
			return;
		}
		CongTrinh = congtrinh;
		TenCongTrinh.text = ConfigManager.instance.SonMonConfig.CongTrinhCfg[congtrinh.LoaiCongTrinh.ToString()][0].DisplayName;
		AvatarCongTrinh.spriteName = congtrinh.LoaiCongTrinh.ToString();
		BgDeTu.spriteName = "bkg_avatar" + ConfigManager.instance.m_dicNhanVats[heroData.Name].Hang;
		if (heroData.CostumeID > 0)
		{
			AvatarDeTu.spriteName = heroData.Name + "_TT";
		}
		else
		{
			AvatarDeTu.spriteName = heroData.Name;
		}
	}

	public void ShowSwap(bool show)
	{
		SwapBtn.SetActive(show);
	}
}

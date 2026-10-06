using UnityEngine;

public class DeTuBattleResult : MonoBehaviour
{
	public NhanVatAvatar avatar;

	public UILabel expReward;

	public UILabel nameNV;

	public GameObject levelupFlag;

	public void SetInfo(UserInfo.HeroData heroData, long expSup)
	{
		UserInfo.HeroData heroData2 = UserInfo.HeroGetExp(heroData, expSup);
		avatar.Set(heroData);
		expReward.text = string.Format("+ {0}", expSup);
		nameNV.text = ConfigManager.instance.m_dicNhanVats[heroData.Name].TenHienThi;
		if (levelupFlag != null)
		{
			levelupFlag.SetActive(heroData2.Level > heroData.Level);
		}
	}
}

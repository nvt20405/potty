using UnityEngine;

public class SearchBanBeItem : MonoBehaviour
{
	public UILabel lbName;

	public UISprite spVIP;

	public UILabel lbLevel;

	public UILabel lbMess;

	public UIButton btnKetBan;

	public UserInfo.BanBeData mFriend_Data;

	public void SetFriendData(UserInfo.BanBeData data)
	{
		if (data != null)
		{
			mFriend_Data = data;
			displayBanBeInfo();
		}
	}

	public void displayBanBeInfo()
	{
		spVIP.spriteName = "icon_vip" + mFriend_Data.Vip;
		spVIP.MakePixelPerfect();
		lbName.text = mFriend_Data.DisplayName;
		lbLevel.text = Localization.instance.Get("CapLabel") + " " + mFriend_Data.Level;
		if (GameManager.instance.m_GameClient.UserInfo.BanBeList.Find((UserInfo.BanBeData e) => e.GID == mFriend_Data.GID) != null)
		{
			btnKetBan.gameObject.SetActive(false);
			lbMess.gameObject.SetActive(true);
		}
		else
		{
			btnKetBan.gameObject.SetActive(true);
			lbMess.gameObject.SetActive(false);
		}
	}
}

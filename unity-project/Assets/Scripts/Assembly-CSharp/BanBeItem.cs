using UnityEngine;

public class BanBeItem : MonoBehaviour
{
	public UILabel lbName;

	public UISprite spVIP;

	public UILabel lbLevel;

	public UISprite bgFocusItem;

	public int itemID;

	public UserInfo.BanBeData mFriend_Data;

	public GameObject ketBanGrp;

	public GameObject statusGrp;

	public UILabel lbStatus;

	public UISprite spStatus;

	public UIButton btnAccept;

	public UIButton btnDeny;

	public UIButton btnTyThi;

	public UIButton btnThongTin;

	public UIButton btnGuiThu;

	public UIButton btnXoa;

	public GameObject focusItem;

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
		btnTyThi.gameObject.SetActive(true);
		btnThongTin.gameObject.SetActive(true);
		btnGuiThu.gameObject.SetActive(true);
		btnXoa.gameObject.SetActive(true);
		focusItem.gameObject.SetActive(false);
		if (mFriend_Data.Status == UserInfo.BanBeData.BanBeStatus.CONFIRMED)
		{
			statusGrp.gameObject.SetActive(true);
			ketBanGrp.gameObject.SetActive(false);
			if (mFriend_Data.Online)
			{
				spStatus.color = Color.green;
				lbStatus.text = "Online";
				spStatus.gameObject.SetActive(true);
			}
			else
			{
				spStatus.color = Color.red;
				lbStatus.text = "Offline";
				spStatus.gameObject.SetActive(true);
			}
		}
		else if (mFriend_Data.Status == UserInfo.BanBeData.BanBeStatus.REQUESTING)
		{
			statusGrp.gameObject.SetActive(false);
			ketBanGrp.gameObject.SetActive(true);
		}
		else if (mFriend_Data.Status == UserInfo.BanBeData.BanBeStatus.UNCONFIRMED)
		{
			statusGrp.gameObject.SetActive(true);
			lbStatus.text = Localization.instance.Get("ChoPhanHoiMessLabel");
			spStatus.gameObject.SetActive(false);
			ketBanGrp.gameObject.SetActive(false);
			btnTyThi.gameObject.SetActive(false);
			btnThongTin.gameObject.SetActive(false);
			btnGuiThu.gameObject.SetActive(false);
		}
	}
}

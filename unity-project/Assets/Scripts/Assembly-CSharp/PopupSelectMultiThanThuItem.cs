using UnityEngine;

public class PopupSelectMultiThanThuItem : MonoBehaviour
{
	public ThanThuAvatar thanthuAvatar;

	public UILabel nhanVatName;

	public UILabel noteLabel;

	public UILabel chiso1Label;

	public UILabel chiso2Label;

	public UISprite chiso1Sprite;

	public UISprite chiso2Sprite;

	public UISprite skillSprite;

	public UISprite skillBg;

	public UICheckbox checkBox;

	public UserInfo.PetInfo m_Data;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Set(UserInfo.PetInfo data)
	{
		if (data == null)
		{
			return;
		}
		m_Data = data;
		nhanVatName.text = Localization.instance.Get(data.codename);
		thanthuAvatar.Set(data);
		if (data.petType == UserInfo.PetInfo.PetType.MENH_KHI || data.petType == UserInfo.PetInfo.PetType.MENH_THAN)
		{
			chiso1Sprite.spriteName = "icon_mau";
			chiso1Label.text = data.GetHP().ToString();
		}
		else if (data.petType == UserInfo.PetInfo.PetType.NGOAI_KHI || data.petType == UserInfo.PetInfo.PetType.NGOAI_MENH || data.petType == UserInfo.PetInfo.PetType.NGOAI_THAN)
		{
			chiso1Sprite.spriteName = "icon_cong";
			chiso1Label.text = data.GetCong().ToString();
		}
		else if (data.petType == UserInfo.PetInfo.PetType.THAN_KHI)
		{
			chiso1Sprite.spriteName = "icon_thu";
			chiso1Label.text = data.GetThu().ToString();
		}
		if (data.petType == UserInfo.PetInfo.PetType.NGOAI_KHI || data.petType == UserInfo.PetInfo.PetType.MENH_KHI || data.petType == UserInfo.PetInfo.PetType.THAN_KHI)
		{
			chiso2Sprite.spriteName = "icon_noi";
			chiso2Label.text = data.GetMP().ToString();
		}
		else if (data.petType == UserInfo.PetInfo.PetType.MENH_THAN || data.petType == UserInfo.PetInfo.PetType.NGOAI_THAN)
		{
			chiso2Sprite.spriteName = "icon_thu";
			chiso2Label.text = data.GetThu().ToString();
		}
		else if (data.petType == UserInfo.PetInfo.PetType.NGOAI_MENH)
		{
			chiso2Sprite.spriteName = "icon_mau";
			chiso2Label.text = data.GetHP().ToString();
		}
		skillSprite.spriteName = data.skill;
		skillBg.spriteName = "bkg_avatar" + (int)((data.Quality != UserInfo.PetInfo.PetQuality.TRUYEN_THUYET) ? data.Quality : ((UserInfo.PetInfo.PetQuality)5));
		if (noteLabel != null)
		{
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.curThanThu == data.ID)
			{
				noteLabel.text = Localization.instance.Get("RaTranLabel");
			}
			else
			{
				noteLabel.text = string.Empty;
			}
		}
	}
}

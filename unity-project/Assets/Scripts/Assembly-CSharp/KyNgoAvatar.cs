using System;
using UnityEngine;

public class KyNgoAvatar : MonoBehaviour
{
	public UISprite avatar;

	public UISprite bkg;

	public UILabel nameLabel;

	public Action<GameObject> OnAvatarClick;

	private static readonly Color selectedColor = new Color(226f / 255f, 2f / 255f, 2f / 255f);

	private static readonly Color normColor = Color.white;

	public bool IsSelected { get; set; }

	private void Awake()
	{
		IsSelected = false;
	}

	public void SetInfo(string code, bool isSelected = false)
	{
		IsSelected = isSelected;
		avatar.spriteName = code;
		switch (code)
		{
		case "kyngo_caonhan":
			nameLabel.text = Localization.instance.Get("KyNgoGiangHoCaoNhanBtn");
			break;
		case "kyngo_khobau":
			nameLabel.text = Localization.instance.Get("KyNgoGiangHoBanDoBtn");
			break;
		case "kyngo_banghuu":
			nameLabel.text = Localization.instance.Get("KyNgoGiangHoBangHuuBtn");
			break;
		case "kyngo_thuongnhan":
			nameLabel.text = Localization.instance.Get("KyNgoGiangHoThuongNhanBtn");
			break;
		case "kyngo_tythi":
			nameLabel.text = Localization.instance.Get("KyNgoGiangHoTyThiBtn");
			break;
		default:
			nameLabel.text = string.Empty;
			break;
		}
		bkg.color = ((!isSelected) ? normColor : selectedColor);
	}

	private void OnClick()
	{
		if (OnAvatarClick != null)
		{
			OnAvatarClick(base.gameObject);
		}
	}

	public void Select(bool isSelect)
	{
		IsSelected = isSelect;
		bkg.color = ((!IsSelected) ? normColor : selectedColor);
	}
}

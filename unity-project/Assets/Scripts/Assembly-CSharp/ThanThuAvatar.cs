using UnityEngine;

public class ThanThuAvatar : MonoBehaviour
{
	public UISprite thanthuAvatar;

	public UISprite bgSprite;

	public UISprite lvlBgSprite;

	public UILabel levelSprite;

	public void Set(UserInfo.PetInfo thanthu)
	{
		thanthuAvatar.gameObject.SetActive(true);
		bgSprite.gameObject.SetActive(true);
		if (lvlBgSprite != null)
		{
			lvlBgSprite.gameObject.SetActive(true);
		}
		if (levelSprite != null)
		{
			levelSprite.gameObject.SetActive(true);
		}
		thanthuAvatar.spriteName = thanthu.codename + "_" + (int)(thanthu.Quality + 1);
		int num = (int)thanthu.Quality;
		if (lvlBgSprite != null)
		{
			lvlBgSprite.spriteName = "hang" + num + "_tl1_lvl_bkg";
		}
		if (num == 4)
		{
			num = 5;
		}
		bgSprite.spriteName = "bkg_avatar" + num;
		if (levelSprite != null)
		{
			levelSprite.text = thanthu.level.ToString();
		}
	}

	public void Release()
	{
		thanthuAvatar.gameObject.SetActive(false);
		bgSprite.gameObject.SetActive(false);
		if (lvlBgSprite != null)
		{
			lvlBgSprite.gameObject.SetActive(false);
		}
		if (levelSprite != null)
		{
			levelSprite.gameObject.SetActive(false);
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}

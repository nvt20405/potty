using UnityEngine;

public class TrangBiAvatar : MonoBehaviour
{
	public UISprite avatar;

	public UISprite avatarBkg;

	public UISprite bkg;

	public UISprite lvlBkg;

	public UILabel lvlLabel;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnAvatarClick()
	{
		EGDebug.Log("NhanVatAvatar click");
	}

	public void Set(string code, int tinhLuyenLvl, int lvl, bool showLevel = true)
	{
		avatar.spriteName = code;
		int num = 0;
	}

	public void Set(UserInfo.TrangBiData data)
	{
		EGDebug.Log("SPRITE NAME ::: " + data.Name);
		Set(data.Name, 0, data.Level);
	}
}

using UnityEngine;

public class CostumeAvatar : MonoBehaviour
{
	public delegate void OnCostumeAvatarClickDelegate(CostumeAvatar avatar);

	public UISprite avatar;

	public UISprite avatarBkg;

	public GameObject star1;

	public GameObject star2;

	public GameObject star3;

	public GameObject ngocGrp;

	public UISprite hoangNgocSprite;

	public UISprite hongNgocSprite;

	public UISprite lamNgocSprite;

	public UISprite tuNgocSprite;

	public UISprite[] spStars;

	public OnCostumeAvatarClickDelegate onAvatarClick;

	public string CodeName;

	private void OnAvatarClick()
	{
		EGDebug.Log("OnAvatarClick");
		if (onAvatarClick != null)
		{
			onAvatarClick(this);
		}
	}

	public void SetInfo(string codeName, int star = -1, int hoangNgoc = -1, int hongNgoc = -1, int lamNgoc = -1, int tuNgoc = -1)
	{
		if (string.IsNullOrEmpty(codeName))
		{
			codeName = "costume_empty";
		}
		avatar.spriteName = codeName;
		CodeName = codeName;
		avatar.MakePixelPerfect();
		star1.SetActive(star == 1);
		star2.SetActive(star == 2);
		star3.SetActive(star > 2);
		if (spStars != null)
		{
		}
		if (hongNgoc >= 0 || hoangNgoc >= 0 || lamNgoc >= 0 || tuNgoc >= 0)
		{
			ngocGrp.SetActive(true);
			hoangNgocSprite.spriteName = GetNgocSpriteName(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_VANG, hoangNgoc);
			hongNgocSprite.spriteName = GetNgocSpriteName(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO, hongNgoc);
			lamNgocSprite.spriteName = GetNgocSpriteName(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_XANH, lamNgoc);
			tuNgocSprite.spriteName = GetNgocSpriteName(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_TIM, tuNgoc);
		}
		else
		{
			ngocGrp.SetActive(false);
		}
	}

	public static string GetNgocSpriteName(UserInfo.TrangBiData.LoaiNgoc loaiNgoc, int ngocLevel)
	{
		string text = "icon_";
		if (ngocLevel > 0 && ngocLevel <= 10)
		{
			string result;
			switch (loaiNgoc)
			{
			case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO:
				result = text + "do_hang" + ngocLevel;
				break;
			case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_TIM:
				result = text + "tim_hang" + ngocLevel;
				break;
			case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_VANG:
				result = text + "vang_hang" + ngocLevel;
				break;
			case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_XANH:
				result = text + "xanh_hang" + ngocLevel;
				break;
			default:
				result = "icon_ngoc_empty";
				break;
			}
			return result;
		}
		return "icon_ngoc_empty";
	}
}

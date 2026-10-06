using UnityEngine;

public class VIPItem : MonoBehaviour
{
	public UISprite levelVIP;

	public int currentVIP;

	public UISprite bgNormal;

	public UISprite bgFocus;

	public void displayLevel(int level)
	{
		if (level > 0)
		{
			currentVIP = level;
			levelVIP.spriteName = "icon_vip" + level;
			levelVIP.MakePixelPerfect();
		}
	}

	public void isSelected(bool selected)
	{
		if (selected)
		{
			bgNormal.gameObject.SetActive(false);
			bgFocus.gameObject.SetActive(true);
		}
		else
		{
			bgNormal.gameObject.SetActive(true);
			bgFocus.gameObject.SetActive(false);
		}
	}
}

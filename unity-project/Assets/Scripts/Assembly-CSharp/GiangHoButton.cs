using UnityEngine;

public class GiangHoButton : MonoBehaviour
{
	public UISprite icon;

	public string GiangHoCodeName { get; private set; }

	public void SetInfo(string codeName)
	{
		icon.spriteName = codeName;
		GiangHoCodeName = codeName;
	}
}

using UnityEngine;

public class BacMayManState : MonoBehaviour
{
	public UILabel lbDisplayNum;

	public UISprite spBkg;

	public void setData()
	{
		lbDisplayNum.text = string.Empty;
	}

	public void isActive()
	{
		spBkg.spriteName = "bkg_number_bacmayman";
	}

	public void isDeactive()
	{
		spBkg.spriteName = "bkg_number_bacmayman_deactive";
	}

	public void isLock()
	{
		spBkg.spriteName = "bkg_number_bacmayman_lock";
	}
}

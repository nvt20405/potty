using UnityEngine;

public class ThachDauTuDongItem : MonoBehaviour
{
	public enum USERSTATUS
	{
		NONE = 0,
		WIN = 1,
		FAIL = 2
	}

	public UILabel lbName;

	public UISprite spVIP;

	public UILabel lbLevel;

	public UISprite spStatus;

	public UICheckbox checkBox;

	public int GID;

	public bool isWon;

	public void init(int ID, string DisplayName, int VIP, int Level)
	{
		GID = ID;
		lbName.text = DisplayName;
		lbLevel.text = Localization.instance.Get("CapLabel") + " " + Level;
		spVIP.spriteName = "icon_vip" + VIP;
		spVIP.MakePixelPerfect();
		spStatus.gameObject.SetActive(false);
	}

	public void setStatus(USERSTATUS status)
	{
		switch (status)
		{
		case USERSTATUS.NONE:
			spStatus.gameObject.SetActive(false);
			break;
		case USERSTATUS.WIN:
			spStatus.spriteName = "thach-dau_17";
			spStatus.MakePixelPerfect();
			spStatus.gameObject.SetActive(true);
			checkBox.gameObject.SetActive(false);
			isWon = true;
			break;
		case USERSTATUS.FAIL:
			spStatus.spriteName = "thach-dau_26";
			spStatus.MakePixelPerfect();
			spStatus.gameObject.SetActive(true);
			checkBox.isChecked = false;
			isWon = false;
			break;
		}
	}

	public void onCheckBoxSelected(bool isActive)
	{
	}
}

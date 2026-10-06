using UnityEngine;

public class MenuTopItem : MonoBehaviour
{
	public enum MenuTop
	{
		MENU_TOP_CHIEN_TRUONG = 0,
		MENU_TOP_TINH_LUYEN = 1,
		MENU_TOP_CONG_LUC = 2,
		MENU_TOP_BI_KIP = 3,
		MENU_TOP_NGOC = 4,
		MENU_TOP_HANH_TAU = 5,
		MENU_TOP_HOANG_KIM = 6,
		MENU_TOP_CHUYEN_SINH = 7,
		MENU_TOP_TU_LINH = 8,
		MENU_TOP_THIENMA = 9,
		MENU_TOP_TBHOANGKIM = 10
	}

	public UILabel lbName;

	public UISprite focus;

	public void setData(MenuTop menuType)
	{
		string empty = string.Empty;
		string text;
		switch (menuType)
		{
		case MenuTop.MENU_TOP_CHIEN_TRUONG:
			text = Localization.instance.Get("TopChienTruongLabel");
			break;
		case MenuTop.MENU_TOP_TINH_LUYEN:
			text = Localization.instance.Get("TopTinhLuyenLabel");
			break;
		case MenuTop.MENU_TOP_CONG_LUC:
			text = Localization.instance.Get("TopCongLucLabel");
			break;
		case MenuTop.MENU_TOP_BI_KIP:
			text = Localization.instance.Get("TopBiKipLabel");
			break;
		case MenuTop.MENU_TOP_NGOC:
			text = Localization.instance.Get("TopNgocLabel");
			break;
		case MenuTop.MENU_TOP_HANH_TAU:
			text = Localization.instance.Get("TopHanhTauLabel");
			break;
		case MenuTop.MENU_TOP_HOANG_KIM:
			text = Localization.instance.Get("TopHoangKimLabel");
			break;
		case MenuTop.MENU_TOP_CHUYEN_SINH:
			text = Localization.instance.Get("TopChuyenSinhLabel");
			break;
		case MenuTop.MENU_TOP_TU_LINH:
			text = Localization.instance.Get("TopTuLinhLabel");
			break;
		case MenuTop.MENU_TOP_THIENMA:
			text = Localization.instance.Get("TopThienMaLabel");
			break;
		case MenuTop.MENU_TOP_TBHOANGKIM:
			text = Localization.instance.Get("TopTBHoangKimLabel");
			break;
		default:
			text = string.Empty;
			break;
		}
		empty = text;
		lbName.text = empty;
	}

	public void isSelected(bool isActive)
	{
		focus.gameObject.SetActive(isActive);
	}
}

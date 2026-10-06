using UnityEngine;

public class CuuThuItem : MonoBehaviour
{
	public UILabel lbName;

	public UILabel lbStatusCuuThu;

	public UISprite bgFocusItem;

	public UIButton btnThongTin;

	public UIButton btnGuiThu;

	public UIButton btnTraThu;

	public UILabel lbBtnTraThu;

	public UISprite spStatus;

	public UILabel lbStatus;

	public GameObject focusItem;

	public int itemID;

	public UserInfo.CuuThuData mCuuThu_Data;

	public void SetCuuThuData(UserInfo.CuuThuData data)
	{
		if (data != null)
		{
			mCuuThu_Data = data;
			displayCuuThuInfo();
		}
	}

	public void displayCuuThuInfo()
	{
		lbName.text = mCuuThu_Data.DisplayName;
		focusItem.gameObject.SetActive(false);
		if (mCuuThu_Data.Type == UserInfo.CuuThuData.CuuThuType.LUAN_KIEM)
		{
			lbStatusCuuThu.text = Localization.instance.Get("CuuThuLuanKiemStatus");
			lbBtnTraThu.text = Localization.instance.Get("DanhLaiButtonLabel");
		}
		else if (mCuuThu_Data.Type == UserInfo.CuuThuData.CuuThuType.BAT_COC)
		{
			lbStatusCuuThu.text = Localization.instance.Get("CuuThuBatCocStatus");
			lbBtnTraThu.text = Localization.instance.Get("BatLaiButtonLabel");
		}
		if (mCuuThu_Data.Online)
		{
			spStatus.color = Color.green;
			lbStatus.text = "Online";
		}
		else
		{
			spStatus.color = Color.red;
			lbStatus.text = "Offline";
		}
	}
}

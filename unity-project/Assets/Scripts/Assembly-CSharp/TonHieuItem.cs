using UnityEngine;

public class TonHieuItem : MonoBehaviour
{
	public UISprite spFocusItem;

	public TonHieuInfo curData;

	public UILabel lbTonHieuName;

	public UISprite bkgTonHieu;

	public void setData(TonHieuInfo info, bool isSelected = false)
	{
		if (info != null)
		{
			curData = info;
			lbTonHieuName.text = info.DisplayName;
			if (isSelected)
			{
				spFocusItem.gameObject.SetActive(true);
			}
			else
			{
				spFocusItem.gameObject.SetActive(false);
			}
			string spriteName = "tonhieu_level_bg";
			switch (info.TonHieuType)
			{
			case UserInfo.GamerData.TonHieuType.LEVEL:
				spriteName = "tonhieu_level_bg";
				break;
			case UserInfo.GamerData.TonHieuType.HANH_TAU:
				spriteName = "tonhieu_giangho_bg";
				break;
			case UserInfo.GamerData.TonHieuType.CHIEN_TRUONG:
				spriteName = "tonhieu_chientruong_bg";
				break;
			case UserInfo.GamerData.TonHieuType.TINH_LUYEN:
				spriteName = "tonhieu_tinhluyen_bg";
				break;
			case UserInfo.GamerData.TonHieuType.CONG_LUC:
				spriteName = "tonhieu_congluc_bg";
				break;
			case UserInfo.GamerData.TonHieuType.LUAN_KIEM:
				spriteName = "tonhieu_luankiem_bg";
				break;
			case UserInfo.GamerData.TonHieuType.HOANG_KIM:
				spriteName = "tonhieu_hoangkim_bg";
				break;
			case UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH:
				spriteName = "tonhieu_QMD_bg";
				break;
			case UserInfo.GamerData.TonHieuType.THAN_THU:
				spriteName = "tonhieu_thanthu_bg";
				break;
			case UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM:
				spriteName = "tonhieu_DHVL_bg";
				break;
			case UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG:
				spriteName = "tonhieu_thienma_bg";
				break;
			case UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM:
				spriteName = "tonhieu_tbhoangkim_bg";
				break;
			}
			bkgTonHieu.spriteName = spriteName;
			bkgTonHieu.MakePixelPerfect();
		}
	}

	public void setSelected(bool isSelect)
	{
		spFocusItem.gameObject.SetActive(isSelect);
	}
}

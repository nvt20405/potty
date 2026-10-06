using UnityEngine;

public class ButtonSuKienThanhChinh : MonoBehaviour
{
	public enum SuKienType
	{
		TopLevel = 0,
		TopLuanKiem = 1,
		Facebook = 2,
		HoiVienViet = 3,
		CacLoaiTop = 4
	}

	public UISprite bkg;

	private SuKienType m_Type;

	public void setTitle(SuKienType type)
	{
		m_Type = type;
		switch (m_Type)
		{
		case SuKienType.TopLevel:
			bkg.spriteName = "icon_toplevel";
			break;
		case SuKienType.TopLuanKiem:
			bkg.spriteName = "icon_topluankiem";
			break;
		case SuKienType.Facebook:
			bkg.spriteName = "btn_facebook";
			break;
		case SuKienType.HoiVienViet:
			bkg.spriteName = "btn_gameviet";
			break;
		case SuKienType.CacLoaiTop:
			bkg.spriteName = "icon_top_all";
			break;
		}
		bkg.MakePixelPerfect();
	}
}

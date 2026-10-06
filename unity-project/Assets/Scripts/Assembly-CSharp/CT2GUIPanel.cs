using UnityEngine;

public class CT2GUIPanel : MonoBehaviour
{
	public UILabel NameLabel;

	public UISprite HPBarBkgSpr;

	public UISprite HPBarSpr;

	public UILabel lbDanhHieu;

	public UISprite spBkgDanhHieu;

	public UISprite spIconBall1;

	public UISprite spIconBall2;

	public GameObject grpDanhHieu;

	public GameObject grpTonHieuTim;

	public GameObject grpTonHieuVang;

	public GameObject parTimLeft;

	public GameObject parTimRight;

	public GameObject parVangLeft;

	public GameObject parVangRight;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void displayDanhHieu(ChienTruongChinhTa.NguoiChoi infoDanhHieu)
	{
	}

	private void displayTonHieuByLevel(int top, UserInfo.GamerData.TonHieuType type)
	{
		string key = string.Empty;
		string spriteName = string.Empty;
		string spriteName2 = string.Empty;
		for (int i = 1; i < 6; i++)
		{
			if (top == i)
			{
				spriteName = "tonhieu_ball_top" + i;
				if (type == UserInfo.GamerData.TonHieuType.LEVEL)
				{
					key = "TonHieu_Level_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.HANH_TAU)
				{
					key = "TonHieu_HanhTau_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.CHIEN_TRUONG)
				{
					key = "TonHieu_ChienTruong_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.TINH_LUYEN)
				{
					key = "TonHieu_TinhLuyen_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.CONG_LUC)
				{
					key = "TonHieu_CongLuc_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.LUAN_KIEM)
				{
					key = "TonHieu_LuanKiem_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.HOANG_KIM)
				{
					key = "TonHieu_HoangKim_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH)
				{
					key = "TonHieu_QMD_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.THAN_THU)
				{
					key = "TonHieu_ThanThu_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM)
				{
					key = "TonHieu_DHVL_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG)
				{
					key = "TonHieu_ThienMa_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM)
				{
					key = "TonHieu_TBHoangKim_TOP" + i;
				}
			}
		}
		switch (type)
		{
		case UserInfo.GamerData.TonHieuType.LEVEL:
			spriteName2 = "tonhieu_level_bg";
			break;
		case UserInfo.GamerData.TonHieuType.HANH_TAU:
			spriteName2 = "tonhieu_giangho_bg";
			break;
		case UserInfo.GamerData.TonHieuType.CHIEN_TRUONG:
			spriteName2 = "tonhieu_chientruong_bg";
			break;
		case UserInfo.GamerData.TonHieuType.TINH_LUYEN:
			spriteName2 = "tonhieu_tinhluyen_bg";
			break;
		case UserInfo.GamerData.TonHieuType.CONG_LUC:
			spriteName2 = "tonhieu_congluc_bg";
			break;
		case UserInfo.GamerData.TonHieuType.LUAN_KIEM:
			spriteName2 = "tonhieu_luankiem_bg";
			break;
		case UserInfo.GamerData.TonHieuType.HOANG_KIM:
			spriteName2 = "tonhieu_hoangkim_bg";
			break;
		case UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH:
			spriteName2 = "tonhieu_QMD_bg";
			break;
		case UserInfo.GamerData.TonHieuType.THAN_THU:
			spriteName2 = "tonhieu_thanthu_bg";
			break;
		case UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM:
			spriteName2 = "tonhieu_DHVL_bg";
			break;
		case UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG:
			spriteName2 = "tonhieu_thienma_bg";
			break;
		case UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM:
			spriteName2 = "tonhieu_tbhoangkim_bg";
			break;
		}
		grpTonHieuVang.gameObject.SetActive(false);
		grpTonHieuTim.gameObject.SetActive(false);
		if (top == 1)
		{
			grpTonHieuVang.gameObject.SetActive(true);
			parVangRight.GetComponent<ParticleSystem>().Simulate(0f, true, true);
			parVangRight.GetComponent<ParticleSystem>().Play();
			parVangLeft.GetComponent<ParticleSystem>().Simulate(0f, true, true);
			parVangLeft.GetComponent<ParticleSystem>().Play();
		}
		if (top == 2)
		{
			grpTonHieuTim.gameObject.SetActive(true);
			parTimRight.GetComponent<ParticleSystem>().Simulate(0f, true, true);
			parTimRight.GetComponent<ParticleSystem>().Play();
			parTimLeft.GetComponent<ParticleSystem>().Simulate(0f, true, true);
			parTimLeft.GetComponent<ParticleSystem>().Play();
		}
		lbDanhHieu.text = Localization.instance.Get(key);
		spBkgDanhHieu.spriteName = spriteName2;
		spIconBall1.spriteName = spriteName;
		spIconBall2.spriteName = spriteName;
	}
}

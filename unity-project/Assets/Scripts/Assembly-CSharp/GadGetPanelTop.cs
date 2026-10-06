using UnityEngine;

public class GadGetPanelTop : MonoBehaviour
{
	public UITexture avatar;

	public UISprite vipIcon;

	public UISprite ChinhTaIcon;

	public UILabel level;

	public UILabel theLuc;

	public UILabel vang;

	public UILabel bac;

	public UILabel TenMonPhai;

	public UISlider lvlProgress;

	public UILabel lblProgress;

	public UISprite spTheLucBG;

	public UISprite spIconHoiVienViet;

	public GameObject particleTheLuc;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
	}

	public void SetLevel(int lvl)
	{
		level.text = lvl.ToString();
	}

	public void SetVang(long vang)
	{
		this.vang.text = string.Format("[ffffff]{0}[-]", vang);
	}

	public void SetBac(long bac)
	{
		this.bac.text = string.Format("{0}", bac);
	}

	public void SetTheLuc(int theLuc, int theLucMax)
	{
		this.theLuc.text = string.Format("[ffffff]{0}/ {1}[-]", theLuc, theLucMax);
	}

	public void SetExp(long hienTai, long max)
	{
		lblProgress.text = string.Format("{0}/{1}", hienTai, max);
		if (max > 0)
		{
			lvlProgress.sliderValue = (float)((double)hienTai / (double)max);
		}
		else
		{
			lvlProgress.sliderValue = 1f;
		}
	}

	public void SetVip(int vip)
	{
		if (vip > 0)
		{
			vipIcon.gameObject.SetActive(true);
			vipIcon.spriteName = string.Format("icon_vip{0}", vip);
			vipIcon.MakePixelPerfect();
		}
		else
		{
			vipIcon.gameObject.SetActive(false);
		}
	}

	public void SetInfo(UserInfo gInfo)
	{
		if (gInfo == null)
		{
			return;
		}
		if (gInfo.Gamer != null)
		{
			SetLevel(gInfo.Gamer.Level);
			SetExp(gInfo.Gamer.Exp, gInfo.Gamer.ExpMax);
			SetVang(gInfo.Gamer.Vang);
			SetBac(gInfo.Gamer.Bac);
			SetVip(gInfo.Gamer.Vip);
			TenMonPhai.text = gInfo.Gamer.DisplayName;
			if (gInfo.Gamer.MonPhaiTuongTro == LucDaiMonPhai.CaiBang || gInfo.Gamer.MonPhaiTuongTro == LucDaiMonPhai.ThieuLam || gInfo.Gamer.MonPhaiTuongTro == LucDaiMonPhai.NgaMi)
			{
				ChinhTaIcon.spriteName = "chinh_phai_levle";
			}
			else if (gInfo.Gamer.MonPhaiTuongTro == LucDaiMonPhai.LangKhach || gInfo.Gamer.MonPhaiTuongTro == LucDaiMonPhai.HiepKhachDao || gInfo.Gamer.MonPhaiTuongTro == LucDaiMonPhai.DoanThi)
			{
				ChinhTaIcon.spriteName = "trung_lap_levle";
			}
			else
			{
				ChinhTaIcon.spriteName = "ta_phai_levle";
			}
			spIconHoiVienViet.gameObject.SetActive(true);
			switch (gInfo.Gamer.HoiVienViet)
			{
			case UserInfo.GamerData.CapHoiVienViet.SAO_KIM_CUONG:
				spIconHoiVienViet.spriteName = "sao_viet_kimcuong";
				break;
			case UserInfo.GamerData.CapHoiVienViet.SAO_VANG:
				spIconHoiVienViet.spriteName = "sao_viet_vang";
				break;
			case UserInfo.GamerData.CapHoiVienViet.SAO_BACH_KIM:
				spIconHoiVienViet.spriteName = "sao_viet_bachkim";
				break;
			case UserInfo.GamerData.CapHoiVienViet.NONE:
				spIconHoiVienViet.spriteName = string.Empty;
				spIconHoiVienViet.gameObject.SetActive(false);
				break;
			}
		}
		if (gInfo.GiaTriThoiGian != null)
		{
			SetTheLuc(gInfo.GiaTriThoiGian.TheLuc, UserInfo.GiaTriThoiGianData.TheLucMax);
			if (gInfo.Gamer.BoostExpTurn > 0)
			{
				spTheLucBG.spriteName = "TheLucBG2";
				particleTheLuc.gameObject.SetActive(true);
			}
			else
			{
				spTheLucBG.spriteName = "TheLucBG";
				particleTheLuc.gameObject.SetActive(false);
			}
		}
	}

	public void onClick_buttonMail(GameObject go)
	{
	}

	public void OnWorldmapClick()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenWorldmap);
	}
}

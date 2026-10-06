using UnityEngine;

public class HoatDongHangNgayItem : MonoBehaviour
{
	public GameObject ThucHienGrp;

	public GameObject NhanThuongGrp;

	public UILabel TitleLabel;

	public UILabel DescLabel;

	public UICheckbox Checkbox;

	public OtherCfg.HoatDongDailyCfg.HoatDongDaily Hoatdong;

	public void Set(OtherCfg.HoatDongDailyCfg.HoatDongDaily hoatdong)
	{
		string text = GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay ?? string.Empty;
		TitleLabel.text = hoatdong.Title;
		DescLabel.text = hoatdong.Desc;
		Checkbox.isChecked = text.Contains(hoatdong.KeyLog);
		if (text.Contains(hoatdong.KeyLog + "(x)"))
		{
			NhanThuongGrp.SetActive(false);
			ThucHienGrp.SetActive(false);
		}
		else if (text.Contains(hoatdong.KeyLog))
		{
			NhanThuongGrp.SetActive(true);
			ThucHienGrp.SetActive(false);
		}
		else
		{
			NhanThuongGrp.SetActive(false);
			ThucHienGrp.SetActive(true);
		}
		Hoatdong = hoatdong;
		WireButtons();
	}

	private void WireButtons()
	{
		WireGroup(NhanThuongGrp, OnNhanThuongClicked);
		WireGroup(ThucHienGrp, OnThucHienClicked);
	}

	private void WireGroup(GameObject group, UIEventListener.VoidDelegate handler)
	{
		if (!(group == null))
		{
			UIButtonMessage[] componentsInChildren = group.GetComponentsInChildren<UIButtonMessage>(true);
			foreach (UIButtonMessage uIButtonMessage in componentsInChildren)
			{
				uIButtonMessage.enabled = false;
			}
			UIEventListener.Get(group).onClick = handler;
			Collider[] componentsInChildren2 = group.GetComponentsInChildren<Collider>(true);
			foreach (Collider collider in componentsInChildren2)
			{
				UIEventListener.Get(collider.gameObject).onClick = handler;
			}
			Collider2D[] componentsInChildren3 = group.GetComponentsInChildren<Collider2D>(true);
			foreach (Collider2D collider2D in componentsInChildren3)
			{
				UIEventListener.Get(collider2D.gameObject).onClick = handler;
			}
		}
	}

	private void OnNhanThuongClicked(GameObject go)
	{
		NhanThuong();
	}

	private void OnThucHienClicked(GameObject go)
	{
		ThucHien();
	}

	public void NhanThuong()
	{
		Debug.Log("[Daily] NhanThuong key=" + ((Hoatdong != null) ? Hoatdong.KeyLog : "NULL"));
		if (Hoatdong != null)
		{
			GameManager.instance.m_GameClient.RequestThuongDailyActivities(Hoatdong.KeyLog);
			NhanThuongGrp.SetActive(false);
			ThucHienGrp.SetActive(false);
		}
	}

	public void ThucHien()
	{
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level < Hoatdong.LevelReq)
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("NhienVuHangNgayKoDuLevel"), Hoatdong.LevelReq));
			return;
		}
		if (Hoatdong.HoatDong == OtherCfg.HoatDongDailyCfg.TypeHoatDong.GIANG_HO)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenWorldmap);
		}
		else if (Hoatdong.HoatDong == OtherCfg.HoatDongDailyCfg.TypeHoatDong.HAC_MOC_NHAI)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHuyetChien);
		}
		else if (Hoatdong.HoatDong == OtherCfg.HoatDongDailyCfg.TypeHoatDong.CAM_DIA)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDanhSon);
		}
		else if (Hoatdong.HoatDong == OtherCfg.HoatDongDailyCfg.TypeHoatDong.LINH_DUOC)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenLienMinhTrongCay);
		}
		else if (Hoatdong.HoatDong == OtherCfg.HoatDongDailyCfg.TypeHoatDong.THAN_THU)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBatThanThu);
		}
		else if (Hoatdong.HoatDong == OtherCfg.HoatDongDailyCfg.TypeHoatDong.QMD)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenQuangMinhDinh);
		}
		else if (Hoatdong.HoatDong == OtherCfg.HoatDongDailyCfg.TypeHoatDong.DOAN_THIEN)
		{
			if (GameManager.instance.m_GameClient.ServerTime.Hour != 12 || GameManager.instance.m_GameClient.ServerTime.Minute >= 4)
			{
				MessagePopup.Create(Localization.instance.Get("ChuaToiGioDoanThien"));
				return;
			}
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenCT2HoatDong);
		}
		else if (Hoatdong.HoatDong == OtherCfg.HoatDongDailyCfg.TypeHoatDong.BAT_COC)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBanBe);
		}
		else if (Hoatdong.HoatDong == OtherCfg.HoatDongDailyCfg.TypeHoatDong.THACH_DAU)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBanBe);
		}
		else if (Hoatdong.HoatDong == OtherCfg.HoatDongDailyCfg.TypeHoatDong.BOI_DUONG_DE_TU)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDeTu);
		}
		else if (Hoatdong.HoatDong == OtherCfg.HoatDongDailyCfg.TypeHoatDong.THAM_NGO_VC)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenVoCong);
		}
		else if (Hoatdong.HoatDong == OtherCfg.HoatDongDailyCfg.TypeHoatDong.CUONG_HOA_TB)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTrangBi);
		}
		else if (Hoatdong.HoatDong == OtherCfg.HoatDongDailyCfg.TypeHoatDong.DANH_BAC)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenKyNgoDanhBac);
		}
		else if (Hoatdong.HoatDong == OtherCfg.HoatDongDailyCfg.TypeHoatDong.UONG_RUOU)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenKyNgoUongRuou);
		}
		GameManager.instance.m_GameClient.RequestUpdateDailyActivities(Hoatdong.KeyLog);
	}
}

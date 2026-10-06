using System;
using UnityEngine;

public class ScreenRuongThachSanh : ScreenBase
{
	public UILabel lbTime;

	public UILabel lbSecond;

	public UILabel lbRuongLevel;

	public UILabel lbMaxExp;

	public UILabel lbMinExp;

	public UISlider expProgressBar;

	public GameObject m_animRuongTS;

	public UIButton spVangBac;

	private float nextSecond;

	private void Start()
	{
	}

	private void Update()
	{
		nextSecond += Time.deltaTime;
		if (nextSecond >= 1f)
		{
			nextSecond = 0f;
			updateTime();
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		updateTime();
		displayRuongInfo();
	}

	public void displayRuongInfo()
	{
		int ruongThachSanhLvl = GameManager.instance.m_GameClient.UserInfo.Gamer.RuongThachSanhLvl;
		EGDebug.Log("currentRuongLevel: " + ruongThachSanhLvl);
		int ruongThachSanhExp = ConfigManager.instance.GetRuongThachSanhExp(ruongThachSanhLvl + 1);
		int ruongThachSanhExp2 = GameManager.instance.m_GameClient.UserInfo.Gamer.RuongThachSanhExp;
		if (ruongThachSanhExp >= 100000000)
		{
			lbRuongLevel.text = string.Format(Localization.instance.Get("CapRuongCaoNhatLevel"), (ruongThachSanhLvl * 150).ToString());
			expProgressBar.gameObject.SetActive(false);
			lbMinExp.gameObject.SetActive(false);
			lbMaxExp.gameObject.SetActive(false);
		}
		else
		{
			lbRuongLevel.text = string.Format(Localization.instance.Get("RuongLevelLabel"), ruongThachSanhLvl, (ruongThachSanhLvl * 150).ToString());
		}
		lbMaxExp.text = ruongThachSanhExp.ToString();
		lbMinExp.text = ruongThachSanhExp2.ToString();
		expProgressBar.sliderValue = (float)ruongThachSanhExp2 / (float)ruongThachSanhExp;
		string value = "NhanRuongTS;";
		if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(value) && ruongThachSanhLvl > 0)
		{
			spVangBac.gameObject.SetActive(true);
			startPlayAnim();
		}
		else
		{
			spVangBac.gameObject.SetActive(false);
			stopAnim();
		}
	}

	public void updateTime()
	{
		DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.RuongCfg.ThoiGianBatDau;
		DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.RuongCfg.ThoiGianKetThuc;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		TimeSpan timeSpan = thoiGianKetThuc - serverTime;
		if (serverTime <= thoiGianKetThuc && serverTime >= thoiGianBatDau)
		{
			string arg = ((timeSpan.Hours >= 10) ? timeSpan.Hours.ToString() : ("0" + timeSpan.Hours));
			string arg2 = ((timeSpan.Minutes >= 10) ? timeSpan.Minutes.ToString() : ("0" + timeSpan.Minutes));
			lbTime.text = string.Format(Localization.instance.Get("TimeThuLoiLabel"), timeSpan.Days, arg, arg2) + " - ";
			lbSecond.text = timeSpan.Seconds + " " + Localization.instance.Get("SecondLabel");
		}
		else
		{
			lbTime.text = string.Empty;
			lbSecond.text = string.Empty;
		}
	}

	public void vangBac_OnClick()
	{
		PopupYesNo.Create(string.Format(Localization.instance.Get("MessNhanThuongRuongTS")), Localization.instance.Get("NhanThuongLabelBtn"), Localization.instance.Get("QuayLaiLabel"), nhanThuongRuongTS, null);
	}

	public void nhanThuongRuongTS()
	{
		NhanRuongThachSanhRequest request = new NhanRuongThachSanhRequest();
		GameManager.instance.m_GameClient.RequestNhanRuongThachSanh(request);
	}

	public void updateInfo(PhanThuongResponse response)
	{
		displayRuongInfo();
		PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), response);
		updateMainMenuView();
	}

	private void updateMainMenuView()
	{
		GadgetPanelBottom gadgetPanelBottom = GUIManager.instance.gadgetPanelBottom;
		if (!(gadgetPanelBottom != null))
		{
			return;
		}
		gadgetPanelBottom.checkDisplayThongBaoSuKien();
		if (gadgetPanelBottom.listKyNgoMenu == null || gadgetPanelBottom.listKyNgoMenu.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < gadgetPanelBottom.listKyNgoMenu.Count; i++)
		{
			if (gadgetPanelBottom.listKyNgoMenu[i].kyNgoID == MenuButtonKyNgo.KyNgoType.RuongThachSanh)
			{
				if (gadgetPanelBottom.checkThongBaoRuongTS())
				{
					gadgetPanelBottom.listKyNgoMenu[i].notifyIcon.gameObject.SetActive(true);
				}
				else
				{
					gadgetPanelBottom.listKyNgoMenu[i].notifyIcon.gameObject.SetActive(false);
				}
			}
		}
	}

	public void startPlayAnim()
	{
		m_animRuongTS.SetActive(true);
		m_animRuongTS.GetComponent<ParticleSystem>().Simulate(0f, true, true);
		m_animRuongTS.GetComponent<ParticleSystem>().Play();
	}

	public void stopAnim()
	{
		m_animRuongTS.SetActive(false);
		m_animRuongTS.GetComponent<ParticleSystem>().Stop();
	}

	public void btnNapTien_OnClick(GameObject go)
	{
		PopUpNapTien.Create();
	}
}

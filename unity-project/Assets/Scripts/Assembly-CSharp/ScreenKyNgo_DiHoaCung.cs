using System;
using UnityEngine;

public class ScreenKyNgo_DiHoaCung : ScreenBase
{
	public UILabel lbSuKienDienRa;

	public UILabel lbCurrentCap;

	public UILabel lbNangCapCan;

	public UILabel lbThachDaGop;

	private int tongSoLuongGachDaGop;

	public UISprite spCurrentTang;

	public UIButton btnNhan;

	private int currentIndex = -1;

	private int nextIndex = -1;

	public GameObject mAnimNhanThuong;

	private float nextSecond;

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

	public override void OnActive()
	{
		base.OnActive();
		GameManager.instance.m_GameClient.RequestGetDiHoaCungInfo();
		mAnimNhanThuong.gameObject.SetActive(false);
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

	public void updateTime()
	{
		UserInfo.ServerData.DiHoaCungCfg diHoaCungConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DiHoaCungConfig;
		if (diHoaCungConfig != null)
		{
			DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
			DateTime thoiGianBatDau = diHoaCungConfig.ThoiGianBatDau;
			DateTime thoiGianKetThuc = diHoaCungConfig.ThoiGianKetThuc;
			DateTime dateTime = thoiGianKetThuc.AddDays(2.0);
			if (serverTime <= thoiGianKetThuc && serverTime >= thoiGianBatDau)
			{
				TimeSpan timeSpan = thoiGianKetThuc - serverTime;
				string empty = string.Empty;
				empty = ((timeSpan.Hours >= 10) ? (empty + timeSpan.Hours + ":") : (empty + "0" + timeSpan.Hours + ":"));
				empty = ((timeSpan.Minutes >= 10) ? (empty + timeSpan.Minutes + ":") : (empty + "0" + timeSpan.Minutes + ":"));
				empty = ((timeSpan.Seconds >= 10) ? (empty + timeSpan.Seconds) : (empty + "0" + timeSpan.Seconds));
				lbSuKienDienRa.text = string.Format(Localization.instance.Get("ThoiGianThanTaiConLaiLabel"), timeSpan.Days) + " " + empty;
			}
			if (serverTime > thoiGianKetThuc && serverTime <= dateTime)
			{
				lbSuKienDienRa.text = Localization.instance.Get("ThongBaoEventDaKetThuc");
			}
		}
		else
		{
			lbSuKienDienRa.text = string.Empty;
		}
	}

	public void updateInfo(int tongGachAll)
	{
		lbThachDaGop.text = string.Format(Localization.instance.Get("DongGopHienTaiLabel"), GameManager.instance.m_GameClient.UserInfo.Gamer.DiemTieuKNB);
		int num = 0;
		tongSoLuongGachDaGop = tongGachAll;
		updateTime();
		currentIndex = -1;
		nextIndex = -1;
		UserInfo.ServerData.DiHoaCungCfg diHoaCungConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DiHoaCungConfig;
		if (diHoaCungConfig.ListTang != null)
		{
			diHoaCungConfig.ListTang.Sort((UserInfo.ServerData.DiHoaCungTang x, UserInfo.ServerData.DiHoaCungTang y) => CompareTangDiHoaCung(y, x));
			for (int num2 = 0; num2 < diHoaCungConfig.ListTang.Count; num2++)
			{
				if (num2 == 0 && tongSoLuongGachDaGop < diHoaCungConfig.ListTang[num2].GiaTri)
				{
					nextIndex = 0;
					currentIndex = -1;
					num = diHoaCungConfig.ListTang[num2].GiaTri;
					break;
				}
				bool flag = CommonHelper.CheckFlag(GameManager.instance.m_GameClient.UserInfo.Gamer.ThuongTieuFlag, num2);
				if (tongSoLuongGachDaGop >= diHoaCungConfig.ListTang[num2].GiaTri)
				{
					if (flag)
					{
						continue;
					}
					currentIndex = num2;
					for (int num3 = 0; num3 < diHoaCungConfig.ListTang.Count; num3++)
					{
						if (tongSoLuongGachDaGop < diHoaCungConfig.ListTang[num3].GiaTri)
						{
							nextIndex = num3;
							num = diHoaCungConfig.ListTang[num3].GiaTri;
							break;
						}
					}
					break;
				}
				nextIndex = num2;
				num = diHoaCungConfig.ListTang[num2].GiaTri;
				break;
			}
		}
		Debug.Log("current index: " + currentIndex + " - nextIndex: " + nextIndex);
		if (currentIndex >= 0)
		{
			if (currentIndex == 0)
			{
				spCurrentTang.gameObject.SetActive(true);
			}
			else
			{
				spCurrentTang.gameObject.SetActive(false);
			}
			btnNhan.gameObject.SetActive(true);
			mAnimNhanThuong.gameObject.SetActive(true);
			mAnimNhanThuong.GetComponent<ParticleSystem>().Play();
			lbCurrentCap.text = string.Format(Localization.instance.Get("HangNgocHoaThachItem"), currentIndex + 1);
			lbNangCapCan.text = string.Empty;
			return;
		}
		btnNhan.gameObject.SetActive(false);
		if (nextIndex >= 0)
		{
			if (nextIndex == 0)
			{
				spCurrentTang.gameObject.SetActive(true);
			}
			else
			{
				spCurrentTang.gameObject.SetActive(false);
			}
			lbCurrentCap.text = string.Format(Localization.instance.Get("HangNgocHoaThachItem"), nextIndex + 1);
			num = diHoaCungConfig.ListTang[nextIndex].GiaTri - tongSoLuongGachDaGop;
			lbNangCapCan.text = string.Format(Localization.instance.Get("NgocHoaThachCanDeNangCap"), num);
		}
		else
		{
			lbCurrentCap.text = string.Format(Localization.instance.Get("HangNgocHoaThachItem"), diHoaCungConfig.ListTang.Count);
			num = 0;
			lbNangCapCan.text = string.Empty;
		}
	}

	public int CompareTangDiHoaCung(UserInfo.ServerData.DiHoaCungTang tangData1, UserInfo.ServerData.DiHoaCungTang tangData2)
	{
		if (tangData1.GiaTri > tangData2.GiaTri)
		{
			return -1;
		}
		if (tangData1.GiaTri < tangData2.GiaTri)
		{
			return 1;
		}
		return 0;
	}

	public void onClick_btnNhan()
	{
		UserInfo.ServerData.DiHoaCungCfg diHoaCungConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DiHoaCungConfig;
		if (diHoaCungConfig.ListTang != null && currentIndex >= 0)
		{
			NhanThuongDiHoaCungAllRequest nhanThuongDiHoaCungAllRequest = new NhanThuongDiHoaCungAllRequest();
			nhanThuongDiHoaCungAllRequest.Tang = currentIndex;
			GameManager.instance.m_GameClient.RequestNhanThuongDiHoaCungAll(nhanThuongDiHoaCungAllRequest);
		}
	}

	public void onClick_XepHangBtn()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTopDiHoaCung);
	}

	public void onClick_ChiTietBtn()
	{
		ScreenDiHoaCungDetail screenDiHoaCungDetail = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenDiHoaCungDetail) as ScreenDiHoaCungDetail;
		screenDiHoaCungDetail.tongSoLuongGachDaGop = tongSoLuongGachDaGop;
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDiHoaCungDetail);
	}

	public void displayPhanPhuong(PhanThuongResponse response)
	{
		if (response != null)
		{
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), response);
			updateInfo(tongSoLuongGachDaGop);
		}
	}

	public void btnHelp_OnClick()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(7, 6);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
		EGDebug.Log("btnHelp_OnClick");
	}
}

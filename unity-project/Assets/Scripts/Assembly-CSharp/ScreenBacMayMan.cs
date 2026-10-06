using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBacMayMan : ScreenBase
{
	public UILabel lbTime;

	private float nextSecond;

	public QuaySoItem QuayBacMayManLuot1;

	public QuaySoItem QuayBacMayManLuot2;

	public List<BacMayManState> listBacMayManState = new List<BacMayManState>();

	private bool[] m_animFinish = new bool[2];

	private void Start()
	{
		QuayBacMayManLuot1.OnFinishPlay = onStopAnim;
		QuayBacMayManLuot2.OnFinishPlay = onStopAnim;
	}

	public override void OnActive()
	{
		base.OnActive();
		updateTime();
		setCurrentData();
		QuayBacMayManLuot1.listItem[0].lbDisplayNum.text = "0";
		QuayBacMayManLuot1.listItem[1].lbDisplayNum.text = "0";
		QuayBacMayManLuot2.listItem[0].lbDisplayNum.text = "0";
		QuayBacMayManLuot2.listItem[1].lbDisplayNum.text = "0";
	}

	private void setCurrentData()
	{
		for (int i = 0; i < listBacMayManState.Count; i++)
		{
			listBacMayManState[i].lbDisplayNum.text = "0";
			listBacMayManState[i].isDeactive();
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.BacMayManPoint == null || GameManager.instance.m_GameClient.UserInfo.Gamer.BacMayManPoint.Length <= 0)
		{
			return;
		}
		for (int j = 0; j < listBacMayManState.Count; j++)
		{
			if (j < GameManager.instance.m_GameClient.UserInfo.Gamer.BacMayManPoint.Length)
			{
				listBacMayManState[j].lbDisplayNum.text = GameManager.instance.m_GameClient.UserInfo.Gamer.BacMayManPoint[GameManager.instance.m_GameClient.UserInfo.Gamer.BacMayManPoint.Length - j - 1].ToString();
				listBacMayManState[j].isActive();
			}
		}
	}

	public int GetLuotQuayTrongNgay()
	{
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("QuayBacMayMan2;"))
		{
			return 2;
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("QuayBacMayMan1;"))
		{
			return 1;
		}
		return 0;
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
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.BacMayManConfig == null)
		{
			return;
		}
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.BacMayManConfig.ThoiGianBatDau;
		DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.BacMayManConfig.ThoiGianKetThuc;
		TimeSpan timeSpan = thoiGianKetThuc - serverTime;
		if (timeSpan.Seconds > 0 && timeSpan.Minutes >= 0 && timeSpan.Hours >= 0)
		{
			string empty = string.Empty;
			empty = ((timeSpan.Hours >= 10) ? (empty + timeSpan.Hours + ":") : (empty + "0" + timeSpan.Hours + ":"));
			empty = ((timeSpan.Minutes >= 10) ? (empty + timeSpan.Minutes + ":") : (empty + "0" + timeSpan.Minutes + ":"));
			empty = ((timeSpan.Seconds >= 10) ? (empty + timeSpan.Seconds) : (empty + "0" + timeSpan.Seconds));
			if (timeSpan.Days > 0)
			{
				lbTime.text = string.Format(Localization.instance.Get("ThoiGianThanTaiConLaiLabel"), timeSpan.Days) + " " + empty;
			}
			else
			{
				lbTime.text = Localization.instance.Get("ThoiGianConLai") + " " + empty;
			}
		}
	}

	public void btnQuay_OnClick()
	{
		GameManager.instance.m_GameClient.RequestQuayBacMayMan();
	}

	public void updateView(QuayBacMayManResponse response)
	{
		displaySoKNBNhanDuoc(response.NumQuayDuoc);
		if (GetLuotQuayTrongNgay() == 1)
		{
			GameManager.instance.m_GameClient.RequestQuayBacMayMan();
		}
		if (response.PhanThuong != null && response.PhanThuong.PhanThuongList != null && response.PhanThuong.PhanThuongList.Count > 0)
		{
			StartCoroutine(openPopUpPhanThuong(5f, response.PhanThuong));
		}
	}

	public void displaySoKNBNhanDuoc(int soKNB)
	{
		m_animFinish[0] = false;
		m_animFinish[1] = false;
		switch (GetLuotQuayTrongNgay())
		{
		case 1:
			QuayBacMayManLuot1.Play(soKNB, CardIndex.ECI_1, 120f, 0.1f, 25, 100f);
			break;
		case 2:
			QuayBacMayManLuot2.Play(soKNB, CardIndex.ECI_2, 120f, 0.1f, 25, 100f);
			break;
		}
	}

	private void onStopAnim(CardIndex cardNumber)
	{
		m_animFinish[0] = true;
		m_animFinish[1] = true;
		StartCoroutine(delayDisplayNumber(2f));
	}

	public IEnumerator delayDisplayNumber(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		setCurrentData();
	}

	public IEnumerator openPopUpPhanThuong(float waitTime, PhanThuongResponse response)
	{
		yield return new WaitForSeconds(waitTime);
		PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), response);
	}

	public void updateMainMenuView()
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
			if (gadgetPanelBottom.listKyNgoMenu[i].kyNgoID == MenuButtonKyNgo.KyNgoType.BacMayMan)
			{
				if (gadgetPanelBottom.checkThongBaoBacMayMan())
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
}

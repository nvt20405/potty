using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenKyNgo_TuBaoBon : ScreenBase
{
	public UILabel lbTime;

	public UILabel lbDescription;

	public UILabel lbSoLuot;

	private float nextSecond;

	private bool[] m_animFinish = new bool[5];

	public List<QuaySoItem> listTheBai = new List<QuaySoItem>();

	private int soKNBNhanDuoc;

	private void Start()
	{
		for (int i = 0; i < listTheBai.Count; i++)
		{
			listTheBai[i].OnFinishPlay = onStopAnim;
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		displayInfo();
		updateTime();
		resetTimeMachine();
	}

	private void resetTimeMachine()
	{
		for (int i = 0; i < listTheBai.Count; i++)
		{
			for (int j = 0; j < listTheBai[i].listItem.Count; j++)
			{
				listTheBai[i].listItem[j].lbDisplayNum.text = "0";
			}
		}
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
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.TuBaoBonConfig == null)
		{
			return;
		}
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TuBaoBonConfig.ThoiGianBatDau;
		DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TuBaoBonConfig.ThoiGianKetThuc;
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

	public void displayInfo()
	{
		UserInfo.GamerData gamer = GameManager.instance.m_GameClient.UserInfo.Gamer;
		int tuBaoBonLevel = gamer.TuBaoBonLevel;
		int tuBaoBonExp = gamer.TuBaoBonExp;
		int tuBaoBonExp2 = ConfigManager.instance.GetTuBaoBonExp(tuBaoBonLevel + 1);
		if (tuBaoBonExp2 == 100000000)
		{
			lbDescription.text = Localization.instance.Get("ThongBaoTuBaoBonMax");
		}
		else if (tuBaoBonExp < tuBaoBonExp2)
		{
			lbDescription.text = string.Format(Localization.instance.Get("ThongbaoTubaoBon"), ConfigManager.instance.GetTuBaoBonExp(tuBaoBonLevel + 1) - tuBaoBonExp);
		}
		else
		{
			lbDescription.text = string.Empty;
		}
		int luotQuayTuBaoBon = ConfigManager.instance.GetLuotQuayTuBaoBon(tuBaoBonExp);
		if (tuBaoBonLevel < luotQuayTuBaoBon)
		{
			lbSoLuot.text = string.Format(Localization.instance.Get("SoLuotQuayTuBaoBon"), luotQuayTuBaoBon - tuBaoBonLevel);
		}
		else
		{
			lbSoLuot.text = string.Format(Localization.instance.Get("SoLuotQuayTuBaoBon"), 0);
		}
	}

	public void displaySoKNBNhanDuoc(int soKNB)
	{
		soKNBNhanDuoc = soKNB;
		for (int i = 0; i < listTheBai.Count; i++)
		{
			m_animFinish[i] = false;
		}
		if (soKNB <= 0)
		{
			return;
		}
		List<int> list = new List<int>();
		while (soKNB > 0)
		{
			list.Add(soKNB % 10);
			soKNB /= 10;
		}
		list.Reverse();
		for (int j = 0; j < listTheBai.Count; j++)
		{
			CardIndex index = CardIndex.ECI_1;
			int num = listTheBai.Count - 1 - j;
			if (num == 0)
			{
				index = CardIndex.ECI_1;
			}
			if (num == 1)
			{
				index = CardIndex.ECI_2;
			}
			if (num == 2)
			{
				index = CardIndex.ECI_3;
			}
			if (num == 3)
			{
				index = CardIndex.ECI_4;
			}
			if (num == 4)
			{
				index = CardIndex.ECI_5;
			}
			if (j < list.Count)
			{
				listTheBai[listTheBai.Count - 1 - j].Play(list[list.Count - 1 - j], index, 200f, 0.1f, 15, 150f);
			}
			else
			{
				listTheBai[listTheBai.Count - 1 - j].Play(0, index, 200f, 0.1f, 15, 150f);
			}
		}
	}

	public void btnQuay_OnClick()
	{
		GameManager.instance.m_GameClient.RequestQuayTuBaoBon();
	}

	private void onStopAnim(CardIndex cardNumber)
	{
		m_animFinish[(int)cardNumber] = true;
		bool flag = true;
		for (int i = 0; i < listTheBai.Count; i++)
		{
			if (!m_animFinish[i])
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			StartCoroutine(openPopUpPhanThuong(1.5f));
		}
	}

	public IEnumerator openPopUpPhanThuong(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		displayInfo();
		PhanThuongResponse response = new PhanThuongResponse();
		PhanThuongResponse.PhanThuong phanThuong = new PhanThuongResponse.PhanThuong
		{
			Loai = PhanThuongResponse.LoaiPhanThuong.VANG,
			Count = soKNBNhanDuoc
		};
		response.PhanThuongList.Clear();
		response.PhanThuongList.Add(phanThuong);
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
			if (gadgetPanelBottom.listKyNgoMenu[i].kyNgoID == MenuButtonKyNgo.KyNgoType.TuBaoBon)
			{
				if (gadgetPanelBottom.checkThongBaoTuBaoBon())
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

	public void updateView(QuayTuBaoBonResponse response)
	{
		displayInfo();
		displaySoKNBNhanDuoc(response.SoKnbNhanDuoc);
	}
}

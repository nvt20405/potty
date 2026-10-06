using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenKyNgo_ThanTai : ScreenBase
{
	public UILabel lbTime;

	public UILabel lbSoVangCan;

	public UIButton btnDonThanTai;

	public GameObject soVangCanGroup;

	private float nextSecond;

	private int soVangCan;

	private bool[] m_animFinish = new bool[5];

	public List<KyNgoThanTaiItem> listTheBai = new List<KyNgoThanTaiItem>();

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
		displaySoVangCan();
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
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		DateTime dateTime = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.registerTime.AddDays(5.0);
		TimeSpan timeSpan = dateTime - serverTime;
		if (timeSpan.Days <= 4 && timeSpan.Seconds > 0 && timeSpan.Minutes >= 0 && timeSpan.Hours >= 0)
		{
			string empty = string.Empty;
			empty = ((timeSpan.Hours >= 10) ? (empty + timeSpan.Hours + ":") : (empty + "0" + timeSpan.Hours + ":"));
			empty = ((timeSpan.Minutes >= 10) ? (empty + timeSpan.Minutes + ":") : (empty + "0" + timeSpan.Minutes + ":"));
			empty = ((timeSpan.Seconds >= 10) ? (empty + timeSpan.Seconds) : (empty + "0" + timeSpan.Seconds));
			lbTime.text = string.Format(Localization.instance.Get("ThoiGianThanTaiConLaiLabel"), timeSpan.Days) + " " + empty;
		}
	}

	public void displaySoVangCan()
	{
		EGDebug.Log("GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu:  " + GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu);
		if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains("ThanTai"))
		{
			soVangCan = 50;
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(string.Format("ThanTai{0};", 50)))
		{
			soVangCan = 300;
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(string.Format("ThanTai{0};", 300)))
		{
			soVangCan = 2000;
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(string.Format("ThanTai{0};", 2000)))
		{
			soVangCan = 10000;
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(string.Format("ThanTai{0};", 10000)))
		{
			soVangCan = 0;
			soVangCanGroup.gameObject.SetActive(false);
			MessagePopup.Create(Localization.instance.Get("HetVongQuayThanTaiMess"));
		}
		lbSoVangCan.text = Localization.instance.Get("NeedLabel") + "\n" + soVangCan;
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
				listTheBai[listTheBai.Count - 1 - j].Play(list[list.Count - 1 - j], index);
			}
			else
			{
				listTheBai[listTheBai.Count - 1 - j].Play(0, index);
			}
		}
	}

	public void btnDonThanTai_OnClick()
	{
		if (soVangCan > 0)
		{
			if (GameManager.instance.m_GameClient.checkKNB(soVangCan))
			{
				ThanTaiRequest thanTaiRequest = new ThanTaiRequest();
				thanTaiRequest.SoKnb = soVangCan;
				GameManager.instance.m_GameClient.RequestThanTai(thanTaiRequest);
			}
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("HetVongQuayThanTaiMess"));
		}
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
		displaySoVangCan();
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
			if (gadgetPanelBottom.listKyNgoMenu[i].kyNgoID == MenuButtonKyNgo.KyNgoType.ThanTai)
			{
				if (gadgetPanelBottom.checkThongBaoThanTai())
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

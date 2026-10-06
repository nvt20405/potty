using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenQuayDiemHoaVang : ScreenBase
{
	public List<QuayHoaVangItem> listQuayHoaVang;

	private bool[] m_animFinish = new bool[3];

	public UILabel lbDiemHienTai;

	private QuayDiemHoaVangResponse curReponse;

	private bool isQuay;

	private void Start()
	{
		for (int i = 0; i < listQuayHoaVang.Count; i++)
		{
			listQuayHoaVang[i].OnFinishPlay = onStopAnim;
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		isQuay = false;
		for (int i = 0; i < listQuayHoaVang.Count; i++)
		{
			for (int j = 0; j < listQuayHoaVang[i].listItem.Count; j++)
			{
				listQuayHoaVang[i].listItem[j].spDisplayNum.spriteName = "icon_dauhoi";
			}
		}
		lbDiemHienTai.text = Localization.instance.Get("DiemHienTaiLabel") + ": " + GameManager.instance.m_GameClient.UserInfo.Gamer.DiemHoaVang;
	}

	public void onClick_BtnQuay()
	{
		if (!isQuay)
		{
			GameManager.instance.m_GameClient.RequestQuayDiemHoaVang();
		}
	}

	public void displayKetQuaQuayThuong(LoaiQuayDiemHoaVang slot1, LoaiQuayDiemHoaVang slot2, LoaiQuayDiemHoaVang slot3)
	{
		m_animFinish[0] = false;
		m_animFinish[1] = false;
		m_animFinish[2] = false;
		for (int i = 0; i < listQuayHoaVang.Count; i++)
		{
			if (i == 0)
			{
				listQuayHoaVang[i].Play(slot1, CardIndex.ECI_1);
			}
			if (i == 1)
			{
				listQuayHoaVang[i].Play(slot2, CardIndex.ECI_2);
			}
			if (i == 2)
			{
				listQuayHoaVang[i].Play(slot3, CardIndex.ECI_3);
			}
		}
		isQuay = true;
	}

	private void onStopAnim(CardIndex cardNumber)
	{
		m_animFinish[(int)cardNumber] = true;
		bool flag = true;
		for (int i = 0; i < listQuayHoaVang.Count; i++)
		{
			if (!m_animFinish[i])
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			isQuay = false;
			StartCoroutine(openPopUpPhanThuong(1.5f));
		}
	}

	public IEnumerator openPopUpPhanThuong(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		if (curReponse != null && curReponse.ptResponse != null && curReponse.ptResponse.PhanThuongList != null && curReponse.ptResponse.PhanThuongList.Count > 0)
		{
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), curReponse.ptResponse);
		}
	}

	public void updateView(QuayDiemHoaVangResponse response)
	{
		if (response != null)
		{
			curReponse = response;
			lbDiemHienTai.text = Localization.instance.Get("DiemHienTaiLabel") + ": " + GameManager.instance.m_GameClient.UserInfo.Gamer.DiemHoaVang;
			if (response.loaiKetQua != null && response.loaiKetQua.Count == 3)
			{
				displayKetQuaQuayThuong(response.loaiKetQua[0], response.loaiKetQua[1], response.loaiKetQua[2]);
			}
		}
	}

	public void btnBack_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHoaVangMain);
	}

	public void btnQuaTang_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenListComboHoaVang);
	}
}

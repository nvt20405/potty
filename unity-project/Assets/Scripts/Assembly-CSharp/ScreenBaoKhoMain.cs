using System.Collections.Generic;
using UnityEngine;

public class ScreenBaoKhoMain : ScreenBase
{
	public List<BaoKhoHang1InfoItem> listBaoKho;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public override void OnActive()
	{
		base.OnActive();
		if (GameManager.instance.m_GameClient.BaoKhoInfoResponse != null)
		{
			if (GameManager.instance.m_GameClient.ServerTime >= GameManager.instance.m_GameClient.LastTimeGetBaoKhoInfo.AddMinutes(5.0))
			{
				GameManager.instance.m_GameClient.RequestGetBaoKhoInfo();
			}
			else
			{
				displayInfo(GameManager.instance.m_GameClient.BaoKhoInfoResponse);
			}
		}
		else
		{
			GameManager.instance.m_GameClient.RequestGetBaoKhoInfo();
		}
	}

	public void displayInfo(GetBaoKhoInfoResponse response)
	{
		if (listBaoKho == null || response == null)
		{
			return;
		}
		if (response.listBaoKhoNgoc.Count > 0)
		{
			for (int i = 0; i < response.listBaoKhoNgoc.Count; i++)
			{
				listBaoKho[i].setData(response.listBaoKhoNgoc[i]);
			}
		}
		if (response.listBaoKhoKim.Count <= 0)
		{
			return;
		}
		for (int j = 0; j < response.listBaoKhoKim.Count; j++)
		{
			if (listBaoKho[j + 5] != null)
			{
				listBaoKho[j + 5].setData(response.listBaoKhoKim[j]);
			}
		}
	}

	public void updateBaoKhoData(UserInfo.BaoKhoInfo baoKhoData)
	{
		if (baoKhoData == null || listBaoKho == null)
		{
			return;
		}
		for (int i = 0; i < listBaoKho.Count; i++)
		{
			if (listBaoKho[i] != null && listBaoKho[i].baokhoData != null && listBaoKho[i].baokhoData.ID == baoKhoData.ID)
			{
				listBaoKho[i].setData(baoKhoData);
				break;
			}
		}
	}

	public void OnBaoKhoSuMonClick(GameObject go)
	{
		if (GameManager.instance.m_GameClient.BaoKhoInfoResponse != null && GameManager.instance.m_GameClient.BaoKhoInfoResponse.listMyBaoKho != null && GameManager.instance.m_GameClient.BaoKhoInfoResponse.listMyBaoKho.Count > 0)
		{
			GUIManager.setScreen(GAME_SCREEN.ScreenMyListBaoKho);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("KhongCoBaoKhoMess"));
		}
	}

	public void OnHoangTrieuBaoKhoClick()
	{
	}

	public void OnNganBaoKhoClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenListBaoKho);
		ScreenListBaoKho screenListBaoKho = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenListBaoKho) as ScreenListBaoKho;
		screenListBaoKho.displayInfo(true);
	}

	public void OnThietBaoKhoClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenListBaoKho);
		ScreenListBaoKho screenListBaoKho = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenListBaoKho) as ScreenListBaoKho;
		screenListBaoKho.displayInfo(false);
	}
}

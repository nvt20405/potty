using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenKyNgo_DapNieu : ScreenBase
{
	public List<KyNgoDapNieuItem> listItem;

	public UILabel lbInfo;

	public UILabel lbTime;

	private float nextSecond;

	private int soLuotDapTrongNgay = -1;

	private int soNieuDangDap;

	private int currIndex = -1;

	private List<PhanThuongResponse.PhanThuong> listPhanThuongDapNieu = new List<PhanThuongResponse.PhanThuong>();

	private UserInfo.ServerData.EventDapNieuCfg eventDapNieuConfig;

	public GameObject btnRefresh;

	public bool dangDapNieu;

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
		listPhanThuongDapNieu.Clear();
		updateTime();
		updateInfo();
		dangDapNieu = false;
	}

	private void updateInfo()
	{
		soLuotDapTrongNgay = getSoLuotDapNieu();
		string text = string.Format(Localization.instance.Get("DapNieuInfo"), soLuotDapTrongNgay);
		lbInfo.text = text;
		btnRefresh.gameObject.SetActive(false);
		List<int> indexPhanThuongDapNieu = getIndexPhanThuongDapNieu();
		List<int> slotDaDapNieu = getSlotDaDapNieu();
		for (int i = 0; i < listItem.Count; i++)
		{
			listItem[i].setEmptyData();
		}
		if (indexPhanThuongDapNieu == null || slotDaDapNieu == null || indexPhanThuongDapNieu.Count != slotDaDapNieu.Count || indexPhanThuongDapNieu.Count <= 0)
		{
			return;
		}
		int num = -1;
		int num2 = -1;
		for (int j = 0; j < indexPhanThuongDapNieu.Count; j++)
		{
			num = slotDaDapNieu[j];
			num2 = indexPhanThuongDapNieu[j];
			if (listItem[num] != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.EventDapNieuConfig.ListPhanThuong[num2] != null)
			{
				listItem[num].setData(GameManager.instance.m_GameClient.UserInfo.ServerInfo.EventDapNieuConfig.ListPhanThuong[num2]);
				listItem[num].Open();
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

	private void updateTime()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.EventDapNieuConfig != null)
		{
			DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
			DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.EventDapNieuConfig.ThoiGianBatDau;
			DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.EventDapNieuConfig.ThoiGianKetThuc;
			if (serverTime <= thoiGianKetThuc && serverTime >= thoiGianBatDau)
			{
				TimeSpan timeSpan = thoiGianKetThuc - serverTime;
				string text = string.Format(Localization.instance.Get("ThoiGianConLaiMess"), timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
				lbTime.text = text;
			}
		}
	}

	private void Shuffle(List<PhanThuongResponse.PhanThuong> list)
	{
		System.Random random = new System.Random();
		int num = list.Count;
		while (num > 1)
		{
			num--;
			int index = random.Next(num + 1);
			PhanThuongResponse.PhanThuong value = list[index];
			list[index] = list[num];
			list[num] = value;
		}
	}

	public void dapNieu1_OnClick(GameObject go)
	{
		onDapNieu(0);
	}

	public void dapNieu2_OnClick(GameObject go)
	{
		onDapNieu(1);
	}

	public void dapNieu3_OnClick(GameObject go)
	{
		onDapNieu(2);
	}

	public void dapNieu4_OnClick(GameObject go)
	{
		onDapNieu(3);
	}

	public void dapNieu5_OnClick(GameObject go)
	{
		onDapNieu(4);
	}

	public void dapNieu6_OnClick(GameObject go)
	{
		onDapNieu(5);
	}

	public void dapNieu7_OnClick(GameObject go)
	{
		onDapNieu(6);
	}

	public void dapNieu8_OnClick(GameObject go)
	{
		onDapNieu(7);
	}

	public void dapNieu9_OnClick(GameObject go)
	{
		onDapNieu(8);
	}

	public void onDapNieu(int index)
	{
		if (soLuotDapTrongNgay >= 3 && getSoLanDaDapTrongLuot() >= 3)
		{
			MessagePopup.Create(Localization.instance.Get("HetLuotDapNieuMess"));
		}
		else if (!dangDapNieu && !listItem[index].isOpen)
		{
			currIndex = index;
			NhanThuongDapNieuRequest nhanThuongDapNieuRequest = new NhanThuongDapNieuRequest();
			nhanThuongDapNieuRequest.slotNieu = index;
			GameManager.instance.m_GameClient.RequestNhanThuongDapNieu(nhanThuongDapNieuRequest);
			dangDapNieu = true;
		}
	}

	public void btnRefresh_OnClick()
	{
		if (!dangDapNieu && getSoLanDaDapTrongLuot() == 3)
		{
			if (soLuotDapTrongNgay >= 3)
			{
				MessagePopup.Create(Localization.instance.Get("HetLuotDapNieuMess"));
			}
			else
			{
				updateInfo();
			}
		}
	}

	public void updateView(PhanThuongResponse phanThuongResponse)
	{
		if (currIndex >= 0)
		{
			listItem[currIndex].setData(phanThuongResponse.PhanThuongList[0]);
			listItem[currIndex].Hit();
		}
		soLuotDapTrongNgay = getSoLuotDapNieu();
		string text = string.Format(Localization.instance.Get("DapNieuInfo"), soLuotDapTrongNgay);
		lbInfo.text = text;
		if (soLuotDapTrongNgay < 3 && getSoLanDaDapTrongLuot() == 3)
		{
			btnRefresh.gameObject.SetActive(true);
		}
		StartCoroutine(openPopUp(1f, phanThuongResponse));
	}

	public IEnumerator openPopUp(float waitTime, PhanThuongResponse phanThuongResponse)
	{
		yield return new WaitForSeconds(waitTime);
		PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongDesc"), phanThuongResponse);
		if (getSoLanDaDapTrongLuot() == 3)
		{
			List<int> listIdxDaDap = getIndexPhanThuongDapNieu();
			getSlotDaDapNieu();
			eventDapNieuConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.EventDapNieuConfig;
			List<PhanThuongResponse.PhanThuong> listPTRemain = new List<PhanThuongResponse.PhanThuong>();
			for (int i = 0; i < eventDapNieuConfig.ListPhanThuong.Count; i++)
			{
				if (!listIdxDaDap.Contains(i))
				{
					listPTRemain.Add(eventDapNieuConfig.ListPhanThuong[i]);
				}
			}
			int count = 0;
			for (int k = 0; k < listItem.Count; k++)
			{
				if (!listItem[k].isOpen && count < listPTRemain.Count && listPTRemain[count] != null)
				{
					listItem[k].notHit(listPTRemain[count]);
					count++;
				}
			}
		}
		dangDapNieu = false;
	}

	private int getSoLuotDapNieu()
	{
		int result = 0;
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(string.Format("DapNieu{0}", 1)))
		{
			result = 1;
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(string.Format("DapNieu{0}", 2)))
			{
				result = 2;
				if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(string.Format("DapNieu{0}", 3)))
				{
					result = 3;
				}
			}
		}
		return result;
	}

	private int getSoLanDaDapTrongLuot()
	{
		int result = 0;
		int soLuotDapNieu = getSoLuotDapNieu();
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(string.Format("DapNieu{0}_Lan{1}", soLuotDapNieu, 1)))
		{
			result = 1;
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(string.Format("DapNieu{0}_Lan{1}", soLuotDapNieu, 2)))
			{
				result = 2;
				if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(string.Format("DapNieu{0}_Lan{1}", soLuotDapNieu, 3)))
				{
					result = 3;
				}
			}
		}
		return result;
	}

	private List<int> getIndexPhanThuongDapNieu()
	{
		List<int> list = new List<int>();
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.EventDapNieuConfig != null)
		{
			int soLuotDapNieu = getSoLuotDapNieu();
			int soLanDaDapTrongLuot = getSoLanDaDapTrongLuot();
			for (int i = 0; i < soLanDaDapTrongLuot; i++)
			{
				string text = string.Format("DapNieu{0}_Lan{1}", soLuotDapNieu, i + 1);
				if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(text))
				{
					continue;
				}
				for (int j = 0; j < GameManager.instance.m_GameClient.UserInfo.ServerInfo.EventDapNieuConfig.ListPhanThuong.Count; j++)
				{
					if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(text + "_Index" + j))
					{
						list.Add(j);
					}
				}
			}
		}
		return list;
	}

	private List<int> getSlotDaDapNieu()
	{
		List<int> list = new List<int>();
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.EventDapNieuConfig != null)
		{
			int soLuotDapNieu = getSoLuotDapNieu();
			int soLanDaDapTrongLuot = getSoLanDaDapTrongLuot();
			List<int> indexPhanThuongDapNieu = getIndexPhanThuongDapNieu();
			if (indexPhanThuongDapNieu != null && indexPhanThuongDapNieu.Count > 0)
			{
				for (int i = 0; i < indexPhanThuongDapNieu.Count; i++)
				{
					for (int j = 0; j < soLanDaDapTrongLuot; j++)
					{
						string text = string.Format("DapNieu{0}_Lan{1}_Index{2}", soLuotDapNieu, j + 1, indexPhanThuongDapNieu[i]);
						if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(text))
						{
							continue;
						}
						for (int k = 0; k < GameManager.instance.m_GameClient.UserInfo.ServerInfo.EventDapNieuConfig.ListPhanThuong.Count; k++)
						{
							if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(text + "_Slot" + k))
							{
								list.Add(k);
							}
						}
					}
				}
			}
		}
		return list;
	}
}

using System;
using UnityEngine;

public class PopupDangNhapNhanThuongTet : MonoBehaviour
{
	public GameObject nhanThuongPrefab;

	public GameObject ItemRoot;

	public static PopupDangNhapNhanThuongTet instance;

	public void OnCloseClick()
	{
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create()
	{
		DestroyPopup();
		instance = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Popup/PopupDangNhapNhanThuongTet"))).GetComponent<PopupDangNhapNhanThuongTet>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.displayListPhanThuong();
	}

	public void displayListPhanThuong()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		UserInfo.ServerData.DangNhapTetCfg dangNhapTetConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DangNhapTetConfig;
		if (dangNhapTetConfig == null || dangNhapTetConfig.ListThuongDNTet == null || dangNhapTetConfig.ListThuongDNTet.Count <= 0)
		{
			return;
		}
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 130f, -3f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -160f, 0f);
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		int num = (int)(GameManager.instance.m_GameClient.ServerTime - dangNhapTetConfig.ThoiGianBatDau).TotalDays;
		int num2 = num;
		for (int i = 0; i < dangNhapTetConfig.ListThuongDNTet.Count; i++)
		{
			if (i >= num2 && num2 >= 0 && num2 < dangNhapTetConfig.ListThuongDNTet.Count)
			{
				UserInfo.ServerData.PTDangNhapTet ptDangNhap = dangNhapTetConfig.ListThuongDNTet[i];
				DangNhapNhanThuongTetItem component = ((GameObject)UnityEngine.Object.Instantiate(nhanThuongPrefab)).GetComponent<DangNhapNhanThuongTetItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = vector;
				vector += vector2;
				component.setData(ptDangNhap, i, num2);
			}
		}
	}

	public static bool checkTodayNhanThuongTet()
	{
		UserInfo.ServerData.DangNhapTetCfg dangNhapTetConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DangNhapTetConfig;
		if (dangNhapTetConfig != null)
		{
			DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
			if (serverTime >= dangNhapTetConfig.ThoiGianBatDau && serverTime <= dangNhapTetConfig.ThoiGianKetThuc && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("DangNhapNhanThuongTet"))
			{
				return true;
			}
			return false;
		}
		return false;
	}
}

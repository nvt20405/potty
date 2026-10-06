using System;
using UnityEngine;

public class PopupMyBaoKho : MonoBehaviour
{
	public UILabel lbPham;

	public UILabel lbTimeChiem;

	public UILabel lbBacNhanMoiGio;

	public UILabel lbNKDNhanMoiGio;

	public UILabel lbCurSanLuongBac;

	public UILabel lbCurSanLuongNKD;

	public UISprite spBkg;

	private float nextSecond;

	private UserInfo.BaoKhoInfo baokhoData;

	private OtherCfg.BaoKhoCfg cfg;

	public static PopupMyBaoKho instance;

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(UserInfo.BaoKhoInfo data)
	{
		DestroyPopup();
		instance = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Popup/PopupMyBaoKho"))).GetComponent<PopupMyBaoKho>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.displayInfo(data);
	}

	private void Update()
	{
		nextSecond += Time.deltaTime;
		if (nextSecond >= 1f)
		{
			nextSecond = 0f;
			updateSanLuong();
			updateTime();
		}
	}

	public void displayInfo(UserInfo.BaoKhoInfo data)
	{
		if (data != null)
		{
			baokhoData = data;
			cfg = null;
			switch (baokhoData.BaoKhoType)
			{
			case UserInfo.BaoKhoInfo.LoaiBaoKho.DONG:
				cfg = ConfigManager.instance.OtherConfig.BaoKhoConfig["BAO_KHO_DONG"];
				lbPham.text = Localization.instance.Get("PhamDONGLabel");
				spBkg.spriteName = "bao_kho_4";
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.NGAN:
				cfg = ConfigManager.instance.OtherConfig.BaoKhoConfig["BAO_KHO_NGAN"];
				lbPham.text = Localization.instance.Get("PhamNGANLabel");
				spBkg.spriteName = "bao_kho_3";
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.KIM:
				cfg = ConfigManager.instance.OtherConfig.BaoKhoConfig["BAO_KHO_KIM"];
				lbPham.text = Localization.instance.Get("PhamKIMLabel");
				spBkg.spriteName = "bao_kho_2";
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.NGOC:
				cfg = ConfigManager.instance.OtherConfig.BaoKhoConfig["BAO_KHO_NGOC"];
				lbPham.text = Localization.instance.Get("PhamNGOCLabel");
				spBkg.spriteName = "bao_kho_1";
				break;
			}
			spBkg.MakePixelPerfect();
			if (cfg != null)
			{
				lbBacNhanMoiGio.text = cfg.BacNhanDuoc.ToString();
				lbNKDNhanMoiGio.text = cfg.NguyenKhiDanNhanDuoc.ToString();
			}
			updateSanLuong();
			updateTime();
		}
	}

	private void updateTime()
	{
		if (baokhoData != null)
		{
			TimeSpan timeSpan = GameManager.instance.m_GameClient.ServerTime - baokhoData.TimeChiem;
			if (timeSpan.TotalHours < 1.0)
			{
				lbTimeChiem.text = timeSpan.Minutes + "' " + timeSpan.Seconds + "s";
				return;
			}
			lbTimeChiem.text = timeSpan.Hours + "h " + timeSpan.Minutes + "' " + timeSpan.Seconds + "s";
		}
	}

	private void updateSanLuong()
	{
		if (baokhoData != null && cfg != null)
		{
			TimeSpan timeSpan = ((!((GameManager.instance.m_GameClient.ServerTime - baokhoData.TimeChiem).TotalHours >= 5.0)) ? ((!(baokhoData.LastTimeThuHoach > baokhoData.TimeChiem)) ? (GameManager.instance.m_GameClient.ServerTime - baokhoData.TimeChiem) : (GameManager.instance.m_GameClient.ServerTime - baokhoData.LastTimeThuHoach)) : ((!(baokhoData.LastTimeThuHoach > baokhoData.TimeChiem)) ? new TimeSpan(5, 0, 0) : (baokhoData.TimeChiem.AddHours(5.0) - baokhoData.LastTimeThuHoach)));
			int num = Convert.ToInt32(timeSpan.TotalMinutes / 60.0 * (double)cfg.BacNhanDuoc);
			int num2 = Convert.ToInt32(timeSpan.TotalMinutes / 60.0 * (double)cfg.NguyenKhiDanNhanDuoc);
			if (num > 0)
			{
				lbCurSanLuongBac.text = num.ToString();
			}
			else
			{
				lbCurSanLuongBac.text = "0";
			}
			if (num2 > 0)
			{
				lbCurSanLuongNKD.text = num2.ToString();
			}
			else
			{
				lbCurSanLuongNKD.text = "0";
			}
		}
	}

	public void btnDong_OnClick(GameObject go)
	{
		DestroyPopup();
	}

	public void btnThu_OnClick(GameObject go)
	{
		if (baokhoData != null)
		{
			ThuHoachBaoKhoRequest thuHoachBaoKhoRequest = new ThuHoachBaoKhoRequest();
			thuHoachBaoKhoRequest.BaoKhoID = baokhoData.ID;
			GameManager.instance.m_GameClient.RequestThuHoachBaoKho(thuHoachBaoKhoRequest);
		}
	}

	public void OnCloseClick(GameObject go)
	{
		DestroyPopup();
	}
}

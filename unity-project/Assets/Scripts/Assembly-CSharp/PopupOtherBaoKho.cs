using System;
using UnityEngine;

public class PopupOtherBaoKho : MonoBehaviour
{
	public static PopupOtherBaoKho instance;

	public UILabel lbPham;

	public UILabel lbTimeChiem;

	public UILabel lbVang;

	public GameObject grpPrice;

	public UILabel lbMonPhaiChiemGiu;

	public UILabel lbLevelMonPhai;

	public UISprite spBkg;

	public UISprite spVIP;

	private float nextSecond;

	private UserInfo.BaoKhoInfo baokhoData;

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
		instance = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("popup/PopupOtherBaoKho"))).GetComponent<PopupOtherBaoKho>();
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

	private void displayInfo(UserInfo.BaoKhoInfo data)
	{
		if (data != null)
		{
			baokhoData = data;
			switch (baokhoData.BaoKhoType)
			{
			case UserInfo.BaoKhoInfo.LoaiBaoKho.DONG:
				lbPham.text = Localization.instance.Get("PhamDONGLabel");
				spBkg.spriteName = "bao_kho_4";
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.NGAN:
				lbPham.text = Localization.instance.Get("PhamNGANLabel");
				spBkg.spriteName = "bao_kho_3";
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.KIM:
				lbPham.text = Localization.instance.Get("PhamKIMLabel");
				spBkg.spriteName = "bao_kho_2";
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.NGOC:
				lbPham.text = Localization.instance.Get("PhamNGOCLabel");
				spBkg.spriteName = "bao_kho_1";
				break;
			}
			spBkg.MakePixelPerfect();
			updateTime();
			lbMonPhaiChiemGiu.text = "S" + baokhoData.SID + ". " + baokhoData.DisplayName;
			lbLevelMonPhai.text = baokhoData.Level.ToString();
			spVIP.spriteName = "icon_vip" + baokhoData.Vip;
			int luotCuopBaoKhoTrongNgay = GameManager.instance.m_GameClient.UserInfo.Gamer.LuotCuopBaoKhoTrongNgay;
			int num = 0;
			int num2 = luotCuopBaoKhoTrongNgay + 1;
			if (num2 >= 7)
			{
				num = ((num2 > 12) ? (10 * (num2 - 12)) : 5);
			}
			if (num > 0)
			{
				grpPrice.gameObject.SetActive(true);
				lbVang.text = num.ToString();
			}
			else
			{
				grpPrice.gameObject.SetActive(false);
			}
		}
	}

	public void btnDong_OnClick(GameObject go)
	{
		DestroyPopup();
	}

	public void btnCuop_OnClick(GameObject go)
	{
		CuopBaoKhoRequest cuopBaoKhoRequest = new CuopBaoKhoRequest();
		cuopBaoKhoRequest.BaoKhoID = baokhoData.ID;
		GameManager.instance.m_GameClient.RequestCuopBaoKho(cuopBaoKhoRequest);
		DestroyPopup();
	}

	public void OnCloseClick(GameObject go)
	{
		DestroyPopup();
	}
}

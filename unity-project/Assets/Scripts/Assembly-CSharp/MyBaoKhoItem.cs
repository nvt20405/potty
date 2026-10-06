using System;
using UnityEngine;

public class MyBaoKhoItem : MonoBehaviour
{
	public UISprite spBkg;

	public UILabel lbTimeChiem;

	private float nextSecond;

	public GameObject grpIconBaoKho;

	private UserInfo.BaoKhoInfo baokhoData;

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
			lbTimeChiem.text = Localization.instance.Get("TimeChiemLabel") + "\n" + timeSpan.Hours + "h " + timeSpan.Minutes + "p " + timeSpan.Seconds + "s";
		}
	}

	public void setData(UserInfo.BaoKhoInfo data)
	{
		if (data != null)
		{
			baokhoData = data;
			TimeSpan timeSpan = GameManager.instance.m_GameClient.ServerTime - baokhoData.TimeChiem;
			lbTimeChiem.text = Localization.instance.Get("TimeChiemLabel") + "\n" + timeSpan.Hours + "h " + timeSpan.Minutes + "p " + timeSpan.Seconds + "s";
			switch (baokhoData.BaoKhoType)
			{
			case UserInfo.BaoKhoInfo.LoaiBaoKho.DONG:
				spBkg.spriteName = "bao_kho_4";
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.NGAN:
				spBkg.spriteName = "bao_kho_3";
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.KIM:
				spBkg.spriteName = "bao_kho_2";
				break;
			case UserInfo.BaoKhoInfo.LoaiBaoKho.NGOC:
				spBkg.spriteName = "bao_kho_1";
				break;
			}
			spBkg.MakePixelPerfect();
			grpIconBaoKho.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
		}
	}

	public void onClickBK(GameObject go)
	{
		if (baokhoData != null)
		{
			PopupMyBaoKho.Create(baokhoData);
		}
	}
}

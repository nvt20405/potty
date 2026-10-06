using System;
using UnityEngine;

public class KyNgoCaoNhanPage : MonoBehaviour
{
	public UILabel timeLabel;

	public UILabel talking;

	public GameObject btnGrp;

	public UILabel expLabel;

	public UILabel gapdoiDesc;

	private UserInfo.CaoNhanData caoNhan;

	private int index;

	public void SetInfo(UserInfo.CaoNhanData data, int idx)
	{
		if (data != null)
		{
			TimeSpan timeSpan = data.Time - GameManager.instance.m_GameClient.ServerTime;
			if (timeSpan.Ticks > 0)
			{
				timeLabel.text = string.Format(Localization.instance.Get("KyNgoGiangHoCaoNhanThoiGianMsg"), (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
				btnGrp.SetActive(false);
			}
			else
			{
				timeLabel.text = Localization.instance.Get("KyNgoGiangHoCaoNhanMsg");
				btnGrp.SetActive(true);
			}
			expLabel.text = string.Format("+ {0}", data.Exp);
			VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu[ConfigManager.GetVatPhamGapDoiCaoNhan()];
			gapdoiDesc.text = string.Format(Localization.instance.Get("CaoNhanGapDoiDesc"), vatPhamTieuThuCfg.TenHienThi);
			caoNhan = data;
			index = idx;
		}
	}

	private void Update()
	{
		if (caoNhan != null)
		{
			TimeSpan timeSpan = caoNhan.Time - GameManager.instance.m_GameClient.ServerTime;
			if (timeSpan.Ticks > 0)
			{
				timeLabel.text = string.Format(Localization.instance.Get("KyNgoGiangHoCaoNhanThoiGianMsg"), (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
				btnGrp.SetActive(false);
			}
			else
			{
				timeLabel.text = Localization.instance.Get("KyNgoGiangHoCaoNhanMsg");
				btnGrp.SetActive(true);
			}
		}
	}

	private void OnDataBtnClick()
	{
		GameManager.instance.m_GameClient.RequestCaoNhan(index, false);
	}

	private void OnHauTaBtnClick()
	{
		GameManager.instance.m_GameClient.RequestCaoNhan(index, true);
	}
}

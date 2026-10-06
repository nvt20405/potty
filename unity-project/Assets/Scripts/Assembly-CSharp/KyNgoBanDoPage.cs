using System;
using UnityEngine;

public class KyNgoBanDoPage : MonoBehaviour
{
	public UILabel timeLabel;

	public GameObject btnGrp;

	public UILabel descGapDoi;

	private UserInfo.BanDoData banDo;

	private int index;

	private void Update()
	{
		if (banDo != null)
		{
			TimeSpan timeSpan = banDo.Time - GameManager.instance.m_GameClient.ServerTime;
			if (timeSpan.Ticks > 0)
			{
				timeLabel.text = string.Format(Localization.instance.Get("KyNgoGiangHoBanDoThoiGianMsg"), (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
				btnGrp.SetActive(false);
			}
			else
			{
				timeLabel.text = Localization.instance.Get("KyNgoGiangHoNhanDuocKhoBauMsg");
				btnGrp.SetActive(true);
			}
		}
	}

	public void SetInfo(UserInfo.BanDoData data, int idx)
	{
		if (data != null)
		{
			banDo = data;
			TimeSpan timeSpan = data.Time - GameManager.instance.m_GameClient.ServerTime;
			if (timeSpan.Ticks > 0)
			{
				timeLabel.text = string.Format(Localization.instance.Get("KyNgoGiangHoBanDoThoiGianMsg"), (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
				btnGrp.SetActive(false);
			}
			else
			{
				timeLabel.text = Localization.instance.Get("KyNgoGiangHoNhanDuocKhoBauMsg");
				btnGrp.SetActive(true);
			}
			VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu[ConfigManager.GetVatPhamGapDoiBanDo()];
			descGapDoi.text = string.Format(Localization.instance.Get("BanDoGapDoiDesc"), vatPhamTieuThuCfg.TenHienThi);
			index = idx;
		}
	}

	private void OnDataBtnClick()
	{
		GameManager.instance.m_GameClient.RequestBanDo(index, false);
	}

	private void OnHauTaBtnClick()
	{
		GameManager.instance.m_GameClient.RequestBanDo(index, true);
	}
}

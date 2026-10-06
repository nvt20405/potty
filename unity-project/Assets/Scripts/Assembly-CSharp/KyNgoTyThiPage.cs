using System;
using UnityEngine;

public class KyNgoTyThiPage : MonoBehaviour
{
	public UILabel timeLabel;

	public NhanVatAvatar avatar;

	public GameObject btnGrp;

	private UserInfo.TyThiData tyThi;

	private int index;

	private void Update()
	{
		if (tyThi != null)
		{
			TimeSpan timeSpan = tyThi.GioDi - GameManager.instance.m_GameClient.ServerTime;
			if (timeSpan.Ticks > 0)
			{
				timeLabel.text = string.Format(Localization.instance.Get("KyNgoGiangHoTyThiThoiGianMsg"), (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
				btnGrp.SetActive(true);
			}
			else
			{
				timeLabel.text = Localization.instance.Get("KyNgoGiangHoTyThiRoiDiMsg");
				btnGrp.SetActive(false);
			}
		}
	}

	public void SetInfo(UserInfo.TyThiData data, int idx)
	{
		if (data != null)
		{
			tyThi = data;
			TimeSpan timeSpan = data.GioDi - GameManager.instance.m_GameClient.ServerTime;
			avatar.Set(ConfigManager.instance.OtherConfig.TyThiAvatar);
			if (timeSpan.Ticks > 0)
			{
				timeLabel.text = string.Format(Localization.instance.Get("KyNgoGiangHoTyThiThoiGianMsg"), (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
				btnGrp.SetActive(true);
			}
			else
			{
				timeLabel.text = Localization.instance.Get("KyNgoGiangHoTyThiRoiDiMsg");
				btnGrp.SetActive(false);
			}
			index = idx;
		}
	}

	private void OnYesConfirmDataBtnClick()
	{
		GameManager.instance.m_GameClient.RequestTyThi(index, false);
	}

	private void OnDataBtnClick()
	{
		PopupYesNo.Create(Localization.instance.Get("TyThiBaiPhucConfirmMsg"), Localization.instance.Get("TyThiBaiPhucConfirmYes"), Localization.instance.Get("TyThiBaiPhucConfirmNo"), OnYesConfirmDataBtnClick, null);
	}

	private void OnHauTaBtnClick()
	{
		GameManager.instance.m_GameClient.RequestTyThi(index, true);
	}
}

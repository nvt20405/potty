using System;
using UnityEngine;

public class KyNgoBangHuuPage : MonoBehaviour
{
	public UILabel timeLabel;

	public NhanVatAvatar avatar;

	public GameObject btnGrp;

	public UILabel costLabel;

	private UserInfo.BangHuuData bangHuu;

	private int index;

	private void Awake()
	{
		avatar.OnEventClick = OnBangHuuAvatarClick;
	}

	private void OnBangHuuAvatarClick(NhanVatAvatar avatar)
	{
		if (bangHuu != null)
		{
			NhanVatCfg value = null;
			if (ConfigManager.instance.m_dicNhanVats.TryGetValue(bangHuu.Ten, out value))
			{
				PopupNhanVat.CreateByNhanVatAvatar(value);
			}
		}
	}

	private void Update()
	{
		if (bangHuu != null)
		{
			TimeSpan timeSpan = bangHuu.Time - GameManager.instance.m_GameClient.ServerTime;
			if (timeSpan.Ticks > 0)
			{
				timeLabel.text = string.Format(Localization.instance.Get("KyNgoGiangHoBangHuuThoiGianMsg"), (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
				btnGrp.SetActive(true);
			}
			else
			{
				timeLabel.text = Localization.instance.Get("KyNgoGiangHoBangHuuRoiDiMsg");
				btnGrp.SetActive(false);
			}
		}
	}

	public void SetInfo(UserInfo.BangHuuData data, int idx)
	{
		if (data != null)
		{
			bangHuu = data;
			TimeSpan timeSpan = data.Time - GameManager.instance.m_GameClient.ServerTime;
			avatar.Set(data.Ten);
			costLabel.text = data.Gia.ToString();
			if (timeSpan.Ticks > 0)
			{
				timeLabel.text = string.Format(Localization.instance.Get("KyNgoGiangHoBangHuuThoiGianMsg"), (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
				btnGrp.SetActive(true);
			}
			else
			{
				timeLabel.text = Localization.instance.Get("KyNgoGiangHoBangHuuRoiDiMsg");
				btnGrp.SetActive(false);
			}
			index = idx;
		}
	}

	private void OnYesConfirmDataBtnClick()
	{
		GameManager.instance.m_GameClient.RequestBangHuu(index, false);
	}

	private void OnDataBtnClick()
	{
		PopupYesNo.Create(Localization.instance.Get("BangHuuMoiRuouConfirmMsg"), Localization.instance.Get("BangHuuMoiRuouConfirmYes"), Localization.instance.Get("BangHuuMoiRuouConfirmNo"), OnYesConfirmDataBtnClick, null);
	}

	private void OnHauTaBtnClick()
	{
		PopupYesNo.Create(string.Format(Localization.instance.Get("BangHuuConfirmMsg"), bangHuu.Gia, ConfigManager.instance.m_dicNhanVats[bangHuu.Ten].TenHienThi), Localization.instance.Get("DongYLabelBtn"), Localization.instance.Get("TuChoiLabelBtn"), ConfirmBangHuu, null);
	}

	public void ConfirmBangHuu()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (bangHuu == null || userInfo == null)
		{
			return;
		}
		if (userInfo.HeroList != null)
		{
			for (int i = 0; i < userInfo.HeroList.Count; i++)
			{
				if (userInfo.HeroList[i].Name == bangHuu.Ten)
				{
					NhanVatCfg value = null;
					if (ConfigManager.instance.m_dicNhanVats.TryGetValue(bangHuu.Ten, out value))
					{
						MessagePopup.Create(string.Format(Localization.instance.Get("KyNgoBangHuuCoDeTuRoi"), value.TenHienThi));
					}
					else
					{
						MessagePopup.Create(string.Format(Localization.instance.Get("KyNgoBangHuuCoDeTuRoi"), string.Empty));
					}
					return;
				}
			}
		}
		GameManager.instance.m_GameClient.RequestBangHuu(index, true);
	}
}

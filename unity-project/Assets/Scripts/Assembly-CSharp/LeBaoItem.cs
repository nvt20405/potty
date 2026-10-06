using System;
using UnityEngine;

public class LeBaoItem : MonoBehaviour
{
	public UILabel lbLeBaoName;

	public UILabel lbDescription;

	public UILabel lbCountDown;

	public UILabel lbGiaBanDau;

	public UILabel lbGiaKhuyenMai;

	public OtherAvatar leBaoAvatar;

	public UIButton btnMua;

	public GoiVatPhamCfg m_Data;

	private float nextSecond;

	private void Update()
	{
		nextSecond += Time.deltaTime;
		if (nextSecond >= 1f)
		{
			nextSecond = 0f;
			updateTime();
		}
	}

	public void updateTime()
	{
		if (m_Data == null)
		{
			return;
		}
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		TimeSpan timeSpan = m_Data.ThoiGianTonTai - serverTime;
		if (serverTime < m_Data.ThoiGianTonTai)
		{
			string text = ((timeSpan.Hours >= 10) ? timeSpan.Hours.ToString() : ("0" + timeSpan.Hours));
			string text2 = ((timeSpan.Minutes >= 10) ? timeSpan.Minutes.ToString() : ("0" + timeSpan.Minutes));
			string text3 = ((timeSpan.Seconds >= 10) ? timeSpan.Seconds.ToString() : ("0" + timeSpan.Seconds));
			if (timeSpan.TotalDays > 360.0)
			{
				lbCountDown.text = string.Empty;
				return;
			}
			lbCountDown.text = string.Format(Localization.instance.Get("TimeTonTaiMess"), timeSpan.Days, text + ":" + text2 + ":" + text3);
		}
	}

	public void Set(GoiVatPhamCfg data)
	{
		if (data != null)
		{
			m_Data = data;
			displayInfo();
		}
	}

	public void displayInfo()
	{
		lbLeBaoName.text = m_Data.TenHienThi;
		if (m_Data.MoTa.Contains("{") && m_Data.MoTa.Contains("}"))
		{
			lbDescription.text = string.Format(m_Data.MoTa, m_Data.TongGiaTri, m_Data.GiaMua);
		}
		else
		{
			lbDescription.text = m_Data.MoTa;
		}
		lbGiaBanDau.text = m_Data.TongGiaTri.ToString();
		lbGiaKhuyenMai.text = m_Data.GiaMua.ToString();
		leBaoAvatar.Set(m_Data.IconName);
		updateTime();
	}
}

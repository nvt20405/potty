using System;
using UnityEngine;

public class ThuCuoiItem : MonoBehaviour
{
	public int id;

	public UILabel time;

	public UISprite avatar;

	private TimeSpan timeSpan;

	private DateTime expiredTime;

	public GameObject CurEquip;

	private void Start()
	{
	}

	private void Update()
	{
		timeSpan = expiredTime - GameManager.instance.m_GameClient.ServerTime;
		if (timeSpan.Days > 0)
		{
			time.text = timeSpan.Days + "d+";
		}
		else if (timeSpan.Hours > 0)
		{
			time.text = timeSpan.Hours + "h+";
		}
		else if (timeSpan.TotalHours < 0.0)
		{
			time.text = string.Empty;
		}
		else if (timeSpan.Hours == 0)
		{
			time.text = "<1h";
		}
		else
		{
			time.text = string.Empty;
		}
	}

	public void Create(UserInfo.ThuCuoiData thucuoi)
	{
		id = thucuoi.ID;
		avatar.spriteName = thucuoi.CodeName;
		if (thucuoi.isActive)
		{
			expiredTime = thucuoi.ExpiredTime;
		}
		if (thucuoi.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi)
		{
			CurEquip.SetActive(true);
		}
		else
		{
			CurEquip.SetActive(false);
		}
	}
}

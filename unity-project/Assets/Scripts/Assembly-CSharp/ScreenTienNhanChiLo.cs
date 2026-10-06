using System;
using System.Collections;
using UnityEngine;

public class ScreenTienNhanChiLo : ScreenBase
{
	public UILabel lbTimeConLai;

	public UILabel lbSoLanRut;

	public UISprite bkgHopRutQue;

	private float nextSecond;

	public GameObject particleRutQue;

	public GameObject particleRutQueActive;

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		updateTime();
		displayInfo();
		particleRutQue.gameObject.SetActive(false);
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

	public void updateTime()
	{
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		DateTime dateTime = DateTime.Now.Date.AddDays(1.0);
		TimeSpan timeSpan = dateTime - serverTime;
		lbTimeConLai.text = string.Format(Localization.instance.Get("NapMayManTimeLabel"), timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
	}

	public void displayResponse(PhanThuongResponse res)
	{
		particleRutQueActive.SetActive(false);
		particleRutQue.SetActive(true);
		bkgHopRutQue.gameObject.SetActive(false);
		particleRutQue.GetComponent<ParticleSystem>().Simulate(0f, true, true);
		particleRutQue.GetComponent<ParticleSystem>().Play();
		StartCoroutine(displayPopUpPhanThuong(2.3f, res));
	}

	public IEnumerator displayPopUpPhanThuong(float waitTime, PhanThuongResponse res)
	{
		yield return new WaitForSeconds(waitTime);
		displayInfo();
		bkgHopRutQue.gameObject.SetActive(true);
		particleRutQue.SetActive(false);
		PopupDanhSachPhanThuong.Create(Localization.instance.Get("ChucMungLabel"), Localization.instance.Get("PhanThuongRutQueLabel"), res);
	}

	public void displayInfo()
	{
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.NapMayManHangNgay >= 200 && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("RutQueTienNhan;"))
		{
			particleRutQueActive.gameObject.SetActive(true);
			particleRutQue.GetComponent<ParticleSystem>().Play();
		}
		else
		{
			particleRutQueActive.gameObject.SetActive(false);
		}
	}

	public void btnNap_OnClick()
	{
		PopUpNapTien.Create();
	}

	public void btnRutQue_OnClick()
	{
		GameManager.instance.m_GameClient.RequestRutQueTienNhan();
	}
}

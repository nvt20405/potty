using System;
using UnityEngine;

public class PopupAutoTayDoc : MonoBehaviour
{
	private const float DELAY_BATTLE = 5.5f;

	public UILabel m_tongDam;

	public UILabel m_soHit;

	private int m_lastBattleType = -1;

	private int m_battleNeedRequestAgaint;

	private float m_delayRequestAgaint;

	private bool m_autoStarted;

	private bool m_isGetInfo;

	private bool m_thongBaoFree;

	private float m_delayRefresh;

	private DateTime m_lastBattleKNBTime = default(DateTime);

	private DateTime m_nextBattleFreeTime = default(DateTime);

	private DongNhanResponse m_dnResponse;

	public static PopupAutoTayDoc instance;

	public static void Release()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	private void OnCloseBtnClick()
	{
		Release();
	}

	private void Update()
	{
		if (m_dnResponse != null && !m_autoStarted)
		{
			m_delayRefresh += Time.deltaTime;
			if (m_delayRefresh >= 1f)
			{
				m_delayRefresh = 0f;
				if (m_dnResponse.TimeStartDongNhan < GameManager.instance.m_GameClient.ServerTime)
				{
					StartAutoDongNhan();
				}
			}
		}
		if (m_delayRequestAgaint > 0f)
		{
			m_delayRequestAgaint -= Time.deltaTime;
			if (m_delayRequestAgaint <= 0f)
			{
				DanhKnbTayDoc();
			}
		}
		if (m_autoStarted)
		{
			AutoTayDoc();
		}
	}

	public static PopupAutoTayDoc Create()
	{
		Release();
		instance = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Popup/PopupAutoTayDoc"))).GetComponent<PopupAutoTayDoc>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = Vector3.one;
		instance.StartAutoDongNhan();
		return instance;
	}

	public void StartAutoDongNhan()
	{
		if (m_dnResponse == null || m_dnResponse.MauDongNhan <= 0)
		{
			GameManager.instance.m_GameClient.GetDongNhanInfo();
		}
		else if (!m_autoStarted)
		{
			m_autoStarted = true;
			SetupAuto();
		}
	}

	public void UpdateInfoTayDoc(DongNhanResponse response)
	{
		m_dnResponse = response;
		TimeSpan timeSpan = new TimeSpan(0, 0, 2);
		m_nextBattleFreeTime = response.NextBattleTime + timeSpan;
		if (m_autoStarted && m_dnResponse.LuotDanh > 0)
		{
			m_soHit.text = string.Format(Localization.instance.Get("AutoTayDocSoLanTanCong"), m_dnResponse.LuotDanh);
			m_tongDam.text = string.Format(Localization.instance.Get("AutoTayDocTongSatThuong"), m_dnResponse.TotalThuongTon);
		}
		if (m_dnResponse.TimeStartDongNhan <= GameManager.instance.m_GameClient.ServerTime)
		{
			StartAutoDongNhan();
		}
	}

	private void SetupAuto()
	{
		m_tongDam.gameObject.SetActive(true);
		m_soHit.gameObject.SetActive(true);
		Time.timeScale = 1f;
		m_nextBattleFreeTime = GameManager.instance.m_GameClient.ServerTime;
		m_lastBattleKNBTime = GameManager.instance.m_GameClient.ServerTime;
	}

	private void AutoTayDoc()
	{
		int num = 2;
		if (m_dnResponse.MauDongNhan <= 0)
		{
			EndAuto();
			return;
		}
		TimeSpan timeSpan = GameManager.instance.m_GameClient.ServerTime - m_lastBattleKNBTime;
		if ((m_nextBattleFreeTime - GameManager.instance.m_GameClient.ServerTime).TotalSeconds <= 0.0)
		{
			DanhFreeTayDoc();
		}
		else if (timeSpan.TotalSeconds >= 5.5 && GameManager.instance.m_GameClient.UserInfo.Gamer.Vang >= num)
		{
			DanhKnbTayDoc();
		}
		else if (GameManager.instance.m_GameClient.UserInfo.Gamer.Vang < num && !m_thongBaoFree)
		{
			m_thongBaoFree = true;
			MessagePopup.Create("Chưởng môn đã hết KNB, tự động đánh miễn phí (35 giây một lần)");
		}
	}

	private void EndAuto()
	{
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		if (screenBattle != null)
		{
			screenBattle.BattleEnd();
			PopupDanhSachPhanThuong.Release();
		}
		if (PopupDuocThanhTuu.instance != null)
		{
			PopupDuocThanhTuu.DestroyPopup();
		}
		ScreenDongNhan screenDongNhan = GUIManager.getScreen(GAME_SCREEN.ScreenDongNhan) as ScreenDongNhan;
		if (screenDongNhan != null)
		{
			screenDongNhan.OnTopBtnClick();
		}
		Release();
	}

	public void RequestAgaint()
	{
		if (m_lastBattleType == 1)
		{
			m_delayRequestAgaint = 0.5f;
			m_battleNeedRequestAgaint = m_lastBattleType;
		}
	}

	private void DanhFreeTayDoc()
	{
		if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBattle)
		{
			ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
			if (screenBattle != null)
			{
				screenBattle.BattleEnd();
				PopupDanhSachPhanThuong.Release();
			}
		}
		if (PopupDuocThanhTuu.instance != null)
		{
			PopupDuocThanhTuu.DestroyPopup();
		}
		TimeSpan timeSpan = new TimeSpan(0, 0, 35);
		m_nextBattleFreeTime = GameManager.instance.m_GameClient.ServerTime + timeSpan;
		GameManager.instance.m_GameClient.RequestDanhDongNhan(false);
		m_lastBattleType = 0;
	}

	private void DanhKnbTayDoc()
	{
		if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBattle)
		{
			ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
			if (screenBattle != null)
			{
				screenBattle.BattleEnd();
				PopupDanhSachPhanThuong.Release();
			}
		}
		if (PopupDuocThanhTuu.instance != null)
		{
			PopupDuocThanhTuu.DestroyPopup();
		}
		ScreenDongNhan screenDongNhan = GUIManager.getScreen(GAME_SCREEN.ScreenDongNhan) as ScreenDongNhan;
		if ((bool)screenDongNhan)
		{
			screenDongNhan.OnDanhNgayDongNhanClick();
			m_lastBattleKNBTime = GameManager.instance.m_GameClient.ServerTime;
			m_lastBattleType = 1;
		}
	}
}

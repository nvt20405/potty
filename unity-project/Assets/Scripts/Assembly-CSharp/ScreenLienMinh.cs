using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLienMinh : MonoBehaviour
{
	public static ScreenLienMinh instance;

	public static ScreenNewLienMinh ScreenNewLienMinh;

	public static ScreenListLMGiaNhap ScreenLienMinhGiaNhap;

	public static ScreenLienMinhThanhVienGuest ScreenLMThanhVienGuest;

	public static ScreenQuanLyLienMinh ScreenQuanLyLienMinh;

	public static ScreenQuanLyThanhVienLienMinh ScreenQuanLyThanhVienLienMinh;

	public static ScreenQuanLyCongTrinhLienMinh ScreenQuanLyCongTrinhLienMinh;

	public static ScreenCongHienLienMinh ScreenCongHienLienMinh;

	public List<GameObject> LienMinhButtons;

	public static List<LienMinhData> TopLienMinhList;

	public static int CurLienMinh;

	public static string ScreenStatus;

	private void Start()
	{
		instance = this;
	}

	public void OnLienMinh()
	{
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh == null || GameManager.instance.m_GameClient.UserInfo.LienMinh.ID <= 0)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenNewLienMinh);
			ScreenNewLienMinh = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenNewLienMinh) as ScreenNewLienMinh;
			{
				foreach (GameObject lienMinhButton in LienMinhButtons)
				{
					lienMinhButton.SetActive(false);
				}
				return;
			}
		}
		foreach (GameObject lienMinhButton2 in LienMinhButtons)
		{
			lienMinhButton2.SetActive(true);
		}
		if (GUIManager.instance.lienMinh3D != null)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenLienMinhMain);
		}
		else
		{
			StartCoroutine(Init());
		}
	}

	private IEnumerator Init()
	{
		PopupLoading.Create();
		yield return StartCoroutine(GUIManager.instance.LoadSceneLienMinh3D());
	}

	public void OnDisable()
	{
	}

	private void Update()
	{
	}

	public void OnQuanLy()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenQuanLyLienMinh);
		ScreenQuanLyLienMinh = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenQuanLyLienMinh) as ScreenQuanLyLienMinh;
	}

	public void OnCongHien()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenCongHienLienMinh);
		ScreenCongHienLienMinh = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenCongHienLienMinh) as ScreenCongHienLienMinh;
	}
}

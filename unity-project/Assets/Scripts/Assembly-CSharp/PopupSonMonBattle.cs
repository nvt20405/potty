using System.Collections.Generic;
using UnityEngine;

public class PopupSonMonBattle : MonoBehaviour
{
	public static PopupSonMonBattle instance;

	public List<SonMonBattleRound> RoundList;

	public List<UILabel> ScoreBoard;

	public UILabel AttackerName;

	public UILabel DefenderName;

	private int CurRound;

	private TanCongSonMonResponse MatchData;

	public GameObject ResultBtn;

	public GameObject RewardBtn;

	public GameObject group;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public static void Create(TanCongSonMonResponse response)
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
		}
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupSonMonBattle"))).GetComponent<PopupSonMonBattle>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = Vector3.one;
		instance.ScoreBoard[0].text = "0";
		instance.ScoreBoard[1].text = "0";
		instance.AttackerName.text = response.AttackerName;
		instance.DefenderName.text = response.DefenderName;
		instance.CurRound = 0;
		instance.MatchData = response;
		instance.RewardBtn.gameObject.SetActive(false);
		instance.ResultBtn.gameObject.SetActive(true);
		int num = 0;
		foreach (BattleReplay item in response.FullMatch)
		{
			instance.RoundList[num].gameObject.SetActive(true);
			instance.RoundList[num].Set(item, (num >= response.DefendHeroes.Count) ? string.Empty : response.DefendHeroes[num], (num >= response.AttackHeroes.Count) ? string.Empty : response.AttackHeroes[num], 0, num == 0);
			num++;
		}
	}

	public void ShowFinalResult()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		foreach (BattleReplay item in MatchData.FullMatch)
		{
			RoundList[num].gameObject.SetActive(true);
			RoundList[num].Set(item, (num >= MatchData.DefendHeroes.Count) ? string.Empty : MatchData.DefendHeroes[num], (num >= MatchData.AttackHeroes.Count) ? string.Empty : MatchData.AttackHeroes[num], (item.Winner == 1) ? 1 : 2, true, true);
			if (item.Winner == 1)
			{
				num2++;
			}
			else
			{
				num3++;
			}
			num++;
		}
		ScoreBoard[0].text = num2.ToString();
		ScoreBoard[1].text = num3.ToString();
		CurRound = num;
		FinishResult();
	}

	public void ShowNextResult()
	{
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		screenBattle.OnFinishReplay -= ShowNextResult;
		group.SetActive(true);
		CurRound++;
		if (CurRound < MatchData.FullMatch.Count)
		{
			RoundList[CurRound].gameObject.SetActive(true);
			RoundList[CurRound].Set(MatchData.FullMatch[CurRound], (CurRound >= MatchData.DefendHeroes.Count) ? string.Empty : MatchData.DefendHeroes[CurRound], (CurRound >= MatchData.AttackHeroes.Count) ? string.Empty : MatchData.AttackHeroes[CurRound], 0, true, true);
		}
		else
		{
			FinishResult();
		}
	}

	public void UpdateScore()
	{
		if (MatchData.FullMatch[CurRound].Winner == 1)
		{
			ScoreBoard[0].text = (int.Parse(ScoreBoard[0].text) + 1).ToString();
		}
		else
		{
			ScoreBoard[1].text = (int.Parse(ScoreBoard[1].text) + 1).ToString();
		}
	}

	public void FinishResult()
	{
		RewardBtn.gameObject.SetActive(true);
		ResultBtn.gameObject.SetActive(false);
	}

	public void HidePopup()
	{
		group.SetActive(false);
	}

	public void Close()
	{
		ScreenSonMonMain screenSonMonMain = (ScreenSonMonMain)GUIManager.getScreen(GAME_SCREEN.ScreenSonMonMain);
		screenSonMonMain.Set(GameManager.instance.m_GameClient.UserInfo.SonMon, false);
		PopupDanhSachPhanThuong.Create(Localization.instance.Get("ChienLoiPham"), string.Empty, MatchData.phanthuong);
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}
}

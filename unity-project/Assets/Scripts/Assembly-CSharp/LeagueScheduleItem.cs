using UnityEngine;

public class LeagueScheduleItem : MonoBehaviour
{
	public UISprite avatar;

	public UILabel displayName;

	public UILabel round;

	public UILabel level;

	public UILabel result;

	public UISprite resultBkg;

	public GameObject button;

	public UILabel buttonLabel;

	public int GID;

	public int SID;

	public int MatchID;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SubmitDoiHinh()
	{
		GameManager.instance.m_GameClient.RequestSubmitDoiHinhLeague();
	}

	public void XemTranDau()
	{
		GameManager.instance.m_GameClient.RequestViewLeagueReplay(MatchID);
	}

	public void OnItemSchedule()
	{
		if (buttonLabel.text == Localization.instance.Get("View"))
		{
			XemTranDau();
		}
		else if (buttonLabel.text == Localization.instance.Get("Submit"))
		{
			SubmitDoiHinh();
		}
	}

	public void SyncServer()
	{
		int num = int.Parse(round.text);
		if (num > GameManager.instance.m_GameClient.UserInfo.LeagueInfo.curRound)
		{
			result.gameObject.SetActive(false);
			resultBkg.gameObject.SetActive(false);
			button.SetActive(false);
		}
		else if (num == GameManager.instance.m_GameClient.UserInfo.LeagueInfo.curRound)
		{
			if (GameManager.instance.m_GameClient.ServerTime > GameManager.instance.m_GameClient.UserInfo.LeagueInfo.roundTime && GameManager.instance.m_GameClient.ServerTime < GameManager.instance.m_GameClient.UserInfo.LeagueInfo.resultTime)
			{
				result.gameObject.SetActive(true);
				resultBkg.gameObject.SetActive(true);
				buttonLabel.text = Localization.instance.Get("OnGoing");
				button.SetActive(true);
				return;
			}
			result.gameObject.SetActive(false);
			resultBkg.gameObject.SetActive(false);
			if (GameManager.instance.m_GameClient.UserInfo.LeagueInfo.SubmitAvaiable)
			{
				buttonLabel.text = Localization.instance.Get("Submit");
				button.SetActive(true);
			}
			else
			{
				buttonLabel.text = Localization.instance.Get("Submitted");
				button.SetActive(true);
			}
		}
		else
		{
			result.gameObject.SetActive(true);
			resultBkg.gameObject.SetActive(true);
			buttonLabel.text = Localization.instance.Get("View");
			button.SetActive(true);
		}
	}

	public void Init(LeagueGamer gamer, int round, int MatchID, bool win = false)
	{
		SID = gamer.SID;
		GID = gamer.GID;
		this.MatchID = MatchID;
		avatar.spriteName = gamer.Avatar;
		displayName.text = "S" + gamer.SID + ". " + gamer.DisplayName;
		this.round.text = round.ToString();
		level.text = gamer.Level.ToString();
		if (round > GameManager.instance.m_GameClient.UserInfo.LeagueInfo.curRound)
		{
			result.gameObject.SetActive(false);
			resultBkg.gameObject.SetActive(false);
			button.SetActive(false);
		}
		else if (round == GameManager.instance.m_GameClient.UserInfo.LeagueInfo.curRound)
		{
			if (GameManager.instance.m_GameClient.ServerTime > GameManager.instance.m_GameClient.UserInfo.LeagueInfo.roundTime && GameManager.instance.m_GameClient.ServerTime < GameManager.instance.m_GameClient.UserInfo.LeagueInfo.resultTime)
			{
				result.gameObject.SetActive(true);
				resultBkg.gameObject.SetActive(true);
				buttonLabel.text = Localization.instance.Get("OnGoing");
				button.SetActive(true);
				return;
			}
			result.gameObject.SetActive(false);
			resultBkg.gameObject.SetActive(false);
			if (GameManager.instance.m_GameClient.UserInfo.LeagueInfo.SubmitAvaiable)
			{
				buttonLabel.text = Localization.instance.Get("Submit");
				button.SetActive(true);
			}
			else
			{
				buttonLabel.text = Localization.instance.Get("Submitted");
				button.SetActive(true);
			}
		}
		else
		{
			result.gameObject.SetActive(true);
			resultBkg.gameObject.SetActive(true);
			if (win)
			{
				resultBkg.spriteName = "Daihoi_thang";
			}
			else
			{
				resultBkg.spriteName = "Daihoi_thua";
			}
			buttonLabel.text = Localization.instance.Get("View");
			button.SetActive(true);
		}
	}
}

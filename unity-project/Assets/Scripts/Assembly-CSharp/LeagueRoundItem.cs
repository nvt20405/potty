using UnityEngine;

public class LeagueRoundItem : MonoBehaviour
{
	public UISprite avatar1;

	public UILabel displayName1;

	public UILabel level1;

	public UILabel result1;

	public UISprite resultBkg1;

	public UISprite avatar2;

	public UILabel displayName2;

	public UILabel level2;

	public UILabel result2;

	public UISprite resultBkg2;

	public GameObject ReplayButton;

	public GameObject VSLabel;

	public UILabel ReplayLabel;

	private int id;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Init(LeagueMatch match)
	{
		LeagueGamer leagueGamer = GameManager.instance.m_GameClient.UserInfo.LeagueInfo.PlayerList.Find((LeagueGamer p) => p.SID == match.SID1 && p.GID == match.GID1);
		LeagueGamer leagueGamer2 = GameManager.instance.m_GameClient.UserInfo.LeagueInfo.PlayerList.Find((LeagueGamer p) => p.SID == match.SID2 && p.GID == match.GID2);
		id = match.ID;
		avatar1.spriteName = leagueGamer.Avatar;
		displayName1.text = "S" + leagueGamer.SID + ". " + leagueGamer.DisplayName;
		level1.text = leagueGamer.Level.ToString();
		avatar2.spriteName = leagueGamer2.Avatar;
		displayName2.text = "S" + leagueGamer2.SID + ". " + leagueGamer2.DisplayName;
		level2.text = leagueGamer2.Level.ToString();
		if (match.Result == 0 || GameManager.instance.m_GameClient.ServerTime < GameManager.instance.m_GameClient.UserInfo.LeagueInfo.resultTime)
		{
			result1.gameObject.SetActive(false);
			resultBkg1.gameObject.SetActive(false);
			result2.gameObject.SetActive(false);
			resultBkg2.gameObject.SetActive(false);
			if (GameManager.instance.m_GameClient.ServerTime > GameManager.instance.m_GameClient.UserInfo.LeagueInfo.roundTime && GameManager.instance.m_GameClient.ServerTime < GameManager.instance.m_GameClient.UserInfo.LeagueInfo.resultTime)
			{
				ReplayLabel.text = Localization.instance.Get("OnGoing");
				ReplayButton.gameObject.SetActive(true);
			}
			else
			{
				ReplayButton.gameObject.SetActive(false);
			}
		}
		else if (match.Result > 0)
		{
			result1.gameObject.SetActive(true);
			resultBkg1.gameObject.SetActive(true);
			result2.gameObject.SetActive(false);
			resultBkg2.gameObject.SetActive(false);
			result1.text = Localization.instance.Get("Win");
			ReplayLabel.text = Localization.instance.Get("View");
			ReplayButton.gameObject.SetActive(true);
		}
		else
		{
			result1.gameObject.SetActive(false);
			resultBkg1.gameObject.SetActive(false);
			result2.gameObject.SetActive(true);
			resultBkg2.gameObject.SetActive(true);
			result1.text = Localization.instance.Get("Win");
			ReplayLabel.text = Localization.instance.Get("View");
			ReplayButton.gameObject.SetActive(true);
		}
	}

	public void OnViewReplay()
	{
		if (ReplayLabel.text == Localization.instance.Get("View"))
		{
			GameManager.instance.m_GameClient.RequestViewLeagueReplay(id);
		}
	}
}

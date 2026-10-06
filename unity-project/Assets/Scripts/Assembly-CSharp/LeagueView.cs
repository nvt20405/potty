using UnityEngine;

public class LeagueView : MonoBehaviour
{
	private enum LeagueGroup
	{
		SCHEDULE = 0,
		ROUND = 1,
		TABLE = 2,
		COUNT_LEAGUE_GROUP = 3
	}

	public GameObject ScheduleGrp;

	public GameObject RoundGrp;

	public GameObject TableGrp;

	public UILabel Announce;

	public UILabel NullSeasonAnnounce;

	public bool isInit;

	public UISprite leagueBg;

	public UISprite leagueLogo;

	private void OnEnable()
	{
		SwitchView(LeagueGroup.SCHEDULE);
	}

	private void Start()
	{
		NullSeasonAnnounce.text = GameManager.instance.m_GameClient.UserInfo.LeagueInfo.seasonAnnounce;
		if (GameManager.instance.m_GameClient.UserInfo.LeagueInfo.PlayerList.Count > 0)
		{
			if (GameManager.instance.m_GameClient.UserInfo.LeagueInfo.PlayerList[0].Rank == 1)
			{
				leagueBg.spriteName = "Daihoi_bgr_sonhap";
				leagueLogo.spriteName = "Daihoi_sonhap";
				leagueLogo.MakePixelPerfect();
			}
			else if (GameManager.instance.m_GameClient.UserInfo.LeagueInfo.PlayerList[0].Rank == 2)
			{
				leagueBg.spriteName = "Daihoi_bgr_caothu";
				leagueLogo.spriteName = "Daihoi_caothu";
				leagueLogo.MakePixelPerfect();
			}
			else if (GameManager.instance.m_GameClient.UserInfo.LeagueInfo.PlayerList[0].Rank == 3)
			{
				leagueBg.spriteName = "Daihoi_bgr_daihiep";
				leagueLogo.spriteName = "Daihoi_dai_hiep";
				leagueLogo.MakePixelPerfect();
			}
		}
	}

	private void Update()
	{
	}

	private void SwitchView(LeagueGroup view)
	{
		if (view == LeagueGroup.SCHEDULE)
		{
			ScheduleGrp.SetActive(true);
			Announce.text = GameManager.instance.m_GameClient.UserInfo.LeagueInfo.scheduleAnnounce;
		}
		else
		{
			ScheduleGrp.SetActive(false);
		}
		if (view == LeagueGroup.ROUND)
		{
			RoundGrp.SetActive(true);
			Announce.text = GameManager.instance.m_GameClient.UserInfo.LeagueInfo.roundAnnounce;
		}
		else
		{
			RoundGrp.SetActive(false);
		}
		if (view == LeagueGroup.TABLE)
		{
			TableGrp.SetActive(true);
			Announce.text = GameManager.instance.m_GameClient.UserInfo.LeagueInfo.tableAnnounce;
		}
		else
		{
			TableGrp.SetActive(false);
		}
	}

	public void GoToSchedule()
	{
		SwitchView(LeagueGroup.SCHEDULE);
	}

	public void GoToRound()
	{
		SwitchView(LeagueGroup.ROUND);
	}

	public void GoToTable()
	{
		SwitchView(LeagueGroup.TABLE);
	}
}

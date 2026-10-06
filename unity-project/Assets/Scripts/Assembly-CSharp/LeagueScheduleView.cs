using UnityEngine;

public class LeagueScheduleView : MonoBehaviour
{
	public LeagueScheduleItem itemPrefab;

	public GameObject itemList;

	public Vector3 itemSize;

	private bool isInit;

	private void Start()
	{
	}

	private void OnEnable()
	{
		if (!isInit)
		{
			Init();
		}
	}

	private void Update()
	{
	}

	private void Init()
	{
		isInit = true;
		Init(GameManager.instance.m_GameClient.UserInfo.LeagueInfo);
	}

	private void Init(LeagueData data)
	{
		if (data.PlayerList == null || data.PlayerList.Count < 8)
		{
			itemPrefab.gameObject.SetActive(false);
			return;
		}
		itemPrefab.gameObject.SetActive(true);
		LeagueMatch match;
		foreach (LeagueMatch leagueMatch in data.LeagueMatchList)
		{
			match = leagueMatch;
			if (match.GID1 == GameManager.instance.m_GameClient.UserInfo.Gamer.ID && match.SID1 == GameManager.instance.m_GameClient.UserInfo.ServerInfo.ID)
			{
				LeagueScheduleItem leagueScheduleItem = (LeagueScheduleItem)Object.Instantiate(itemPrefab);
				leagueScheduleItem.transform.parent = itemList.transform;
				leagueScheduleItem.transform.localPosition = itemList.transform.localPosition + itemSize * (itemList.transform.childCount - 11);
				leagueScheduleItem.transform.localScale = Vector3.one;
				LeagueGamer gamer = GameManager.instance.m_GameClient.UserInfo.LeagueInfo.PlayerList.Find((LeagueGamer e) => e.SID == match.SID2 && e.GID == match.GID2);
				leagueScheduleItem.Init(gamer, match.Round, match.ID, match.Result > 0);
			}
			else if (match.GID2 == GameManager.instance.m_GameClient.UserInfo.Gamer.ID && match.SID2 == GameManager.instance.m_GameClient.UserInfo.ServerInfo.ID)
			{
				LeagueScheduleItem leagueScheduleItem2 = (LeagueScheduleItem)Object.Instantiate(itemPrefab);
				leagueScheduleItem2.transform.parent = itemList.transform;
				leagueScheduleItem2.transform.localPosition = itemList.transform.localPosition + itemSize * (itemList.transform.childCount - 11);
				leagueScheduleItem2.transform.localScale = Vector3.one;
				LeagueGamer gamer2 = GameManager.instance.m_GameClient.UserInfo.LeagueInfo.PlayerList.Find((LeagueGamer e) => e.SID == match.SID1 && e.GID == match.GID1);
				leagueScheduleItem2.Init(gamer2, match.Round, match.ID, match.Result < 0);
			}
		}
		itemPrefab.gameObject.SetActive(false);
	}
}

using UnityEngine;

public class LeagueRoundView : MonoBehaviour
{
	public LeagueRoundItem itemPrefab;

	public GameObject itemList;

	public Vector3 itemSize;

	private bool isInit;

	private void OnEnable()
	{
		if (!isInit)
		{
			Init();
		}
	}

	private void Start()
	{
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
		int num = 0;
		foreach (LeagueMatch item in data.LeagueMatchList.FindAll((LeagueMatch m) => m.Round == data.curRound))
		{
			itemPrefab.gameObject.SetActive(true);
			num++;
			LeagueRoundItem leagueRoundItem = (LeagueRoundItem)Object.Instantiate(itemPrefab);
			leagueRoundItem.transform.parent = itemList.transform;
			leagueRoundItem.transform.localPosition = itemList.transform.localPosition + itemSize * ((float)itemList.transform.childCount - 9.5f);
			leagueRoundItem.transform.localScale = Vector3.one;
			leagueRoundItem.Init(item);
			itemPrefab.gameObject.SetActive(false);
		}
		itemPrefab.gameObject.SetActive(false);
	}
}

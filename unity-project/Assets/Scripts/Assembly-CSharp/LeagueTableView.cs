using UnityEngine;

public class LeagueTableView : MonoBehaviour
{
	public LeagueTableItem itemPrefab;

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
		itemPrefab.gameObject.SetActive(true);
		int num = 0;
		foreach (LeagueGamer player in data.PlayerList)
		{
			num++;
			LeagueTableItem leagueTableItem = (LeagueTableItem)Object.Instantiate(itemPrefab);
			leagueTableItem.transform.parent = itemList.transform;
			leagueTableItem.transform.localPosition = itemList.transform.localPosition + itemSize * (itemList.transform.childCount - 9);
			leagueTableItem.transform.localScale = Vector3.one;
			if (num > GameManager.instance.m_GameClient.UserInfo.LeagueInfo.promotedCount && player.Rank < 3)
			{
				leagueTableItem.transform.localPosition += itemSize * 0.25f;
			}
			if (num >= 9 - GameManager.instance.m_GameClient.UserInfo.LeagueInfo.relegatedCount && player.Rank > 1)
			{
				leagueTableItem.transform.localPosition += itemSize * 0.25f;
			}
			leagueTableItem.Init(player, num);
		}
		itemPrefab.gameObject.SetActive(false);
	}
}

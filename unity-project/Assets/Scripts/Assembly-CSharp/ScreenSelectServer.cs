using System.Collections.Generic;
using UnityEngine;

public class ScreenSelectServer : ScreenBase
{
	public GameObject singleGroup;

	public GameObject allGroup;

	public SelectServerItem SelectedSrv;

	public GameObject AllItemRoot;

	public GameObject AllItemPrefab;

	public GameObject buttonViewAll;

	public GameObject ChuaChoiLabel;

	private CRemoteFarmClient SelectServerInfo;

	public GameObject animNhanVat;

	private List<SelectServerItem> listItem = new List<SelectServerItem>();

	private void Start()
	{
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(0);
		LoginResponse loginResponse = GameManager.instance.m_LoginResponse;
		int num = PlayerPrefs.GetInt("SaveServerId" + GameManager.instance.m_userName, -1);
		int select_server_id = -1;
		if (num > -1)
		{
			select_server_id = num;
		}
		else if (loginResponse.PlayedServerList.Count > 0)
		{
			select_server_id = loginResponse.PlayedServerList[0];
		}
		if (select_server_id == -1)
		{
			List<CRemoteFarmClient> list = new List<CRemoteFarmClient>();
			foreach (CRemoteFarmClient server in loginResponse.Servers)
			{
				if (server.m_Status == CRemoteFarmClient.ServerStatus.NEW)
				{
					list.Add(server);
				}
			}
			if (list.Count > 0)
			{
				int index = Random.Range(0, list.Count);
				select_server_id = list[index].m_GameServerID;
			}
		}
		CRemoteFarmClient cRemoteFarmClient = loginResponse.Servers.Find((CRemoteFarmClient e) => e.m_GameServerID == select_server_id);
		if (cRemoteFarmClient == null)
		{
			loginResponse.Servers.Sort((CRemoteFarmClient x, CRemoteFarmClient y) => CompareServer(y.m_GameServerID, x.m_GameServerID));
			cRemoteFarmClient = loginResponse.Servers[0];
		}
		SelectServer(cRemoteFarmClient);
		NGUITools.SetActive(singleGroup, true);
		NGUITools.SetActive(allGroup, false);
		if (animNhanVat != null)
		{
			animNhanVat.SetActive(true);
			animNhanVat.GetComponent<ParticleSystem>().Simulate(0f, true, true);
			animNhanVat.GetComponent<ParticleSystem>().Play();
		}
	}

	public void SelectServer(CRemoteFarmClient selectServer)
	{
		SelectServerInfo = selectServer;
		SelectedSrv.Set(SelectServerInfo);
		NGUITools.SetActive(singleGroup, true);
		NGUITools.SetActive(allGroup, false);
	}

	public void OnShowAllServer()
	{
		NGUITools.SetActive(singleGroup, false);
		NGUITools.SetActive(allGroup, true);
		foreach (Transform item in AllItemRoot.transform)
		{
			Transform transform2 = item;
			SelectServerItem component = transform2.GetComponent<SelectServerItem>();
			if (component != null)
			{
				Object.Destroy(transform2.gameObject);
			}
		}
		listItem.Clear();
		LoginResponse loginResponse = GameManager.instance.m_LoginResponse;
		List<int> playedServerList = loginResponse.PlayedServerList;
		Vector3 vector = default(Vector3);
		vector = new Vector3(-135f, 200f, -5f);
		if (playedServerList.Count > 0)
		{
			int num = 0;
			for (num = 0; num < playedServerList.Count; num++)
			{
				int server_idx = playedServerList[num];
				CRemoteFarmClient cRemoteFarmClient = loginResponse.Servers.Find((CRemoteFarmClient e) => e.m_GameServerID == server_idx);
				if (cRemoteFarmClient != null)
				{
					SelectServerItem component2 = ((GameObject)Object.Instantiate(AllItemPrefab)).GetComponent<SelectServerItem>();
					component2.transform.parent = AllItemRoot.transform;
					component2.transform.localScale = new Vector3(1f, 1f, 1f);
					component2.transform.localPosition = vector;
					component2.Set(cRemoteFarmClient);
					listItem.Add(component2);
					if (num % 2 == 0)
					{
						vector += new Vector3(265f, 0f, 0f);
						continue;
					}
					vector += new Vector3(0f, -80f, 0f);
					vector.x = -135f;
				}
			}
			if (num % 2 == 1)
			{
				vector += new Vector3(0f, -80f, 0f);
			}
			vector.Set(0f, vector.y + 20f, -5f);
		}
		ChuaChoiLabel.transform.localPosition = new Vector3(0f, vector.y, -5f);
		loginResponse.Servers.Sort((CRemoteFarmClient x, CRemoteFarmClient y) => CompareServer(y.m_GameServerID, x.m_GameServerID));
		vector += new Vector3(0f, -70f, 0f);
		vector.x = -135f;
		int num2 = 0;
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, 0f, 0f);
		for (int num3 = 0; num3 < loginResponse.Servers.Count; num3++)
		{
			CRemoteFarmClient cRemoteFarmClient2 = loginResponse.Servers[num3];
			if (!playedServerList.Contains(cRemoteFarmClient2.m_GameServerID))
			{
				if (num2 == 11)
				{
					vector2 = vector;
				}
				SelectServerItem component3 = ((GameObject)Object.Instantiate(AllItemPrefab)).GetComponent<SelectServerItem>();
				component3.transform.parent = AllItemRoot.transform;
				component3.transform.localScale = new Vector3(1f, 1f, 1f);
				component3.transform.localPosition = vector;
				component3.Set(cRemoteFarmClient2);
				listItem.Add(component3);
				if (num2 % 2 == 0)
				{
					vector += new Vector3(265f, 0f, 0f);
				}
				else
				{
					vector += new Vector3(0f, -80f, 0f);
					vector.x = -135f;
				}
				if (num2 >= 10)
				{
					component3.gameObject.SetActive(false);
				}
				num2++;
			}
		}
		if (num2 > 10)
		{
			UIButton component4 = ((GameObject)Object.Instantiate(buttonViewAll)).GetComponent<UIButton>();
			component4.transform.parent = AllItemRoot.transform;
			component4.transform.localScale = new Vector3(1f, 1f, 1f);
			component4.transform.localPosition = new Vector3(0f, vector2.y, vector2.z);
			UIEventListener.Get(component4.gameObject).onClick = onClick_ViewAllServer;
			component4.gameObject.SetActive(true);
		}
	}

	public void onClick_ViewAllServer(GameObject go)
	{
		if (!(go != null))
		{
			return;
		}
		go.gameObject.SetActive(false);
		if (listItem != null)
		{
			for (int i = 0; i < listItem.Count; i++)
			{
				listItem[i].gameObject.SetActive(true);
			}
		}
	}

	public void OnBackClick()
	{
		if (GameManager.instance.m_GameClient.m_transport != null && GameManager.instance.m_GameClient.m_transport.State == CJsonTransport.EState.Connected)
		{
			GameManager.instance.m_GameClient.m_transport.Disconnect();
		}
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenLogin);
	}

	public void OnConnect()
	{
		if (SelectServerInfo.m_Status == CRemoteFarmClient.ServerStatus.DOWN)
		{
			MessagePopup.Create(Localization.instance.Get("SelectServerDown"));
			return;
		}
		GameManager.instance.m_addrPort = SelectServerInfo.m_AddrPort;
		GameManager.instance.m_GameClient.gameObject.SetActive(true);
		if (GameManager.instance.m_GameClient.m_transport != null && GameManager.instance.m_GameClient.m_transport.State == CJsonTransport.EState.Connected && !GameManager.instance.m_GameClient.isIpV6)
		{
			GameManager.instance.m_GameClient.m_transport.SetAsGlobal();
			GameManager.instance.m_GameClient.RequestGetGamerInfo();
		}
		else
		{
			GameManager.instance.m_GameClient.IssueConnect();
		}
	}

	public int CompareServer(int id1, int id2)
	{
		if (id1 > id2)
		{
			return 1;
		}
		return -1;
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	private void Update()
	{
	}
}

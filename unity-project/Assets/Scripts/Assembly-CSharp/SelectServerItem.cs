using UnityEngine;

public class SelectServerItem : MonoBehaviour
{
	public UILabel nameLabel;

	public UILabel status;

	public UISprite statusBkg;

	private CRemoteFarmClient m_ServerInfo;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnSelectServer()
	{
		ScreenSelectServer screenSelectServer = GUIManager.getScreen(GAME_SCREEN.ScreenSelectServer) as ScreenSelectServer;
		screenSelectServer.SelectServer(m_ServerInfo);
	}

	public void Set(CRemoteFarmClient info)
	{
		m_ServerInfo = info;
		string text = string.Format("{0} - {1}", info.m_GameServerID, info.m_farmClientName);
		nameLabel.text = text;
		if (info.m_Status == CRemoteFarmClient.ServerStatus.NEW)
		{
			status.text = Localization.instance.Get("ServerStatusNew");
			status.color = new Color(1f, 1f, 1f);
			statusBkg.color = new Color(1f, 0.43f, 0.43f);
		}
		else if (info.m_Status == CRemoteFarmClient.ServerStatus.GOOD)
		{
			status.text = Localization.instance.Get("ServerStatusGood");
		}
		else if (info.m_Status == CRemoteFarmClient.ServerStatus.DOWN)
		{
			status.text = Localization.instance.Get("ServerStatusDown");
			status.color = new Color(1f, 1f, 1f);
			statusBkg.color = new Color(0.52f, 0.4f, 0.4f);
		}
	}
}

using System.Collections.Generic;

public class ServerList
{
	private List<CRemoteFarmClient> m_farmList = new List<CRemoteFarmClient>();

	public List<CRemoteFarmClient> FarmList
	{
		get
		{
			return m_farmList;
		}
		set
		{
			m_farmList = value;
		}
	}
}

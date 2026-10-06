using Nettention.Proud;

public class CRemoteFarmClient
{
	public enum ServerStatus
	{
		GOOD = 0,
		NEW = 1,
		DOWN = 2
	}

	public HostID m_hostID;

	public string m_farmClientName;

	public byte m_serverType;

	public AddressPort m_AddrPort;

	public int m_CumServer;

	public ServerStatus m_Status = ServerStatus.DOWN;

	public int m_GameServerID;

	public CRemoteFarmClient()
	{
		m_farmClientName = string.Empty;
	}
}

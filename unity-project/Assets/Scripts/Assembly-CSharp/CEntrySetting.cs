using System;

public class CEntrySetting
{
	public Guid m_Version;

	public int m_serverPort;

	public static CEntrySetting m_Instance = new CEntrySetting();

	public static CEntrySetting Instance
	{
		get
		{
			return m_Instance;
		}
	}

	public CEntrySetting()
	{
		m_Version = new Guid("{0xea5c62cc,0xcc2e,0x4675,{0x8d,0x39,0x1c,0x84,0x84,0x17,0x65,0xcb}}");
		m_serverPort = 33334;
	}
}

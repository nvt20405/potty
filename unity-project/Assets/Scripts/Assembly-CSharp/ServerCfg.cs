public class ServerCfg
{
	private int _BattleTime = 60;

	public string FarmServerIP { get; set; }

	public string ServerIP { get; set; }

	public string PublicIP { get; set; }

	public int ServerPort { get; set; }

	public string GameConn { get; set; }

	public string LoginConn { get; set; }

	public string MongoConn { get; set; }

	public string MongoDB { get; set; }

	public int BattleTime
	{
		get
		{
			return _BattleTime;
		}
		set
		{
			_BattleTime = value;
		}
	}
}

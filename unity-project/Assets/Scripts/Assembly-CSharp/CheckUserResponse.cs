public class CheckUserResponse : ExDataBase
{
	public int GID { get; set; }

	public int GameServerID { get; set; }

	public string GamerServerName { get; set; }

	public bool reconnect { get; set; }

	public CheckUserResponse()
	{
		reconnect = false;
	}
}

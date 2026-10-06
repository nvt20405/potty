using System.Collections.Generic;

public class LoginResponse : ExDataBase
{
	public string AccessToken { get; set; }

	public List<int> PlayedServerList { get; set; }

	public List<CRemoteFarmClient> Servers { get; set; }

	public OtherCfg.LoginConfig LoginCfg { get; set; }

	public string RealUserName { get; set; }

	public string RealPublisherAccessToken { get; set; }
}

using System.Collections.Generic;

public class BanhChungOthersResponse : ExDataBase
{
	public NoiBanh Noi { get; set; }

	public List<HomeResponse.Gamer3DInfo> OtherPlayers { get; set; }
}

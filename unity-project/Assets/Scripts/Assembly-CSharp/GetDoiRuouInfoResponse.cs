using System;
using System.Collections.Generic;

public class GetDoiRuouInfoResponse : ExDataBase
{
	public int ID { get; set; }

	public DateTime ThoiGianReset { get; set; }

	public List<string> ListItem { get; set; }

	public GetDoiRuouInfoResponse(int id)
	{
		ID = id;
	}

	public GetDoiRuouInfoResponse()
	{
		ID = 0;
	}
}

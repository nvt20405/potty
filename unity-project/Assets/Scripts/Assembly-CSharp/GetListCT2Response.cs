using System;

public class GetListCT2Response : ExDataBase
{
	public int GID { get; set; }

	public int ServerID { get; set; }

	public int MinLevel { get; set; }

	public int MaxLevel { get; set; }

	public int NumPlayer { get; set; }

	public int MaxPlayer { get; set; }

	public DateTime StartTime { get; set; }

	public DateTime NextTime { get; set; }

	public DateTime DeadLineToJoin { get; set; }
}

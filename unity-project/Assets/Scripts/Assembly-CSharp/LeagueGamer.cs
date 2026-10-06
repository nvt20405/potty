using System.Collections.Generic;

public class LeagueGamer
{
	public int GID;

	public int SID;

	public int LID;

	public int Num;

	public string DisplayName;

	public int Rank;

	public BattleGamerInfo BattleData;

	public int Point;

	public int Star;

	public List<int> History;

	public int Level;

	public string Avatar;

	public KeyValuePair<int, int> Id
	{
		get
		{
			return new KeyValuePair<int, int>(GID, SID);
		}
	}

	public LeagueGamer(int gid, int sid, string displayName)
	{
		DisplayName = displayName;
		GID = gid;
		SID = sid;
		LID = 0;
		Num = 0;
		Rank = 1;
		BattleData = null;
		Point = 0;
		Star = 0;
		History = new List<int>();
	}

	public LeagueGamer()
	{
		DisplayName = string.Empty;
		GID = 0;
		SID = 0;
		LID = 0;
		Num = 0;
		Rank = 1;
		BattleData = null;
		Point = 0;
		Star = 0;
		History = new List<int>();
	}

	public bool isBot()
	{
		if (SID == 0)
		{
			return true;
		}
		return false;
	}
}

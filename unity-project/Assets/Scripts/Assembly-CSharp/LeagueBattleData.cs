using System.Collections.Generic;

public class LeagueBattleData
{
	public int Level;

	public int GID;

	public int SID;

	public BattleGamerInfo BattleData;

	public KeyValuePair<int, int> Id
	{
		get
		{
			return new KeyValuePair<int, int>(GID, SID);
		}
	}

	public LeagueBattleData()
	{
		GID = 0;
		SID = 0;
		BattleData = null;
	}

	public LeagueBattleData(int gid, int sid)
	{
		GID = gid;
		SID = sid;
		BattleData = null;
	}
}

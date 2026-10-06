using System;
using System.Collections.Generic;

public class LeagueData
{
	public DateTime LeagueStartTime;

	public List<LeagueGamer> PlayerList;

	public List<LeagueMatch> LeagueMatchList;

	public List<PhanThuongResponse> PhanThuongList;

	public string scheduleAnnounce;

	public string roundAnnounce;

	public string tableAnnounce;

	public string seasonAnnounce;

	public int curRound;

	public DateTime roundTime;

	public DateTime resultTime;

	public bool SubmitAvaiable;

	public int relegatedCount;

	public int promotedCount;
}

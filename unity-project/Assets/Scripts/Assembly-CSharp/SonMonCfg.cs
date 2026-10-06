using System.Collections.Generic;

public class SonMonCfg
{
	public class SonMonCTCfg
	{
		public string DisplayName;

		public int Level;

		public float Production;

		public int Storage;

		public Dictionary<string, int> BuildCost;

		public string Model;

		public int BuildTime;

		public int Score;
	}

	public Dictionary<string, List<SonMonCTCfg>> CongTrinhCfg;

	public List<string> Description;

	public int MinThuHoachDivide = 1;

	public float RaidRate;

	public int LevelReq;
}

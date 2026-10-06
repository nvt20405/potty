using System;
using System.Collections.Generic;

public class DongNhanResponse : ExDataBase
{
	public class MonPhaiTopSrv
	{
		public int Gid { get; set; }

		public string Name { get; set; }

		public int Level { get; set; }

		public int HitCount { get; set; }

		public long TotalThuongTon { get; set; }

		public bool IsLastHit { get; set; }
	}

	public class MonPhaiTop
	{
		public int Gid { get; set; }

		public string Name { get; set; }

		public int Level { get; set; }

		public int HitCount { get; set; }

		public long TotalThuongTon { get; set; }

		public bool IsLastHit { get; set; }

		public MonPhaiTop()
		{
		}

		public MonPhaiTop(MonPhaiTopSrv mp)
		{
			Gid = mp.Gid;
			Name = mp.Name;
			Level = mp.Level;
			TotalThuongTon = mp.TotalThuongTon;
		}
	}

	private DateTime _lastTime = DateTime.MinValue;

	public int LevelDongNhan { get; set; }

	public long MauDongNhan { get; set; }

	public long MauDongNhanOrig { get; set; }

	public DateTime TimeStartDongNhan { get; set; }

	public List<MonPhaiTop> LastTop10 { get; set; }

	public DateTime NextBattleTime
	{
		get
		{
			return _lastTime;
		}
		set
		{
			_lastTime = value;
		}
	}

	public int LuotDanh { get; set; }

	public long TotalThuongTon { get; set; }

	public List<MonPhaiTop> Top10 { get; set; }

	public int DurationLastBattle { get; set; }
}

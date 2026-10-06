using System;
using System.Collections.Generic;

public class DuaTopLuanKiemResponse : ExDataBase
{
	public class TopGamer
	{
		public int ID { get; set; }

		public string DisplayName { get; set; }
	}

	public int id;

	public List<TopGamer> ListTop = new List<TopGamer>();

	public string message { get; set; }

	public DateTime WriteTime { get; set; }
}

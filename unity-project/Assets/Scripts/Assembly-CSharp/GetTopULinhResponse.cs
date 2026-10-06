using System;
using System.Collections.Generic;

public class GetTopULinhResponse : ExDataBase
{
	public class TopULinhData
	{
		public int GID { get; set; }

		public string DisplayName { get; set; }

		public int Diem { get; set; }
	}

	public List<TopULinhData> ListTop = new List<TopULinhData>();

	public int id;

	public DateTime WriteTime { get; set; }
}

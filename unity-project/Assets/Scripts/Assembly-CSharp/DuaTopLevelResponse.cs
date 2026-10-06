using System.Collections.Generic;

public class DuaTopLevelResponse : ExDataBase
{
	public class TopGamer
	{
		public int ID { get; set; }

		public string DisplayName { get; set; }

		public int Level { get; set; }

		public long Exp { get; set; }
	}

	public int id;

	public List<TopGamer> ListTop = new List<TopGamer>();

	public string message { get; set; }
}

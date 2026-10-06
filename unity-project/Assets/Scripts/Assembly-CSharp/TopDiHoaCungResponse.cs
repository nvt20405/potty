using System.Collections.Generic;

public class TopDiHoaCungResponse : ExDataBase
{
	public class TopDiHoaCungData
	{
		public int GID { get; set; }

		public string DisplayName { get; set; }

		public int SoGach { get; set; }
	}

	public List<TopDiHoaCungData> ListTop = new List<TopDiHoaCungData>();

	public int id;
}

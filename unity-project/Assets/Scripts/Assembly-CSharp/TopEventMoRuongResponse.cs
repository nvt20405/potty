using System.Collections.Generic;

public class TopEventMoRuongResponse : ExDataBase
{
	public class MoRuongData
	{
		public int GID { get; set; }

		public string DisplayName { get; set; }

		public int SoLuong { get; set; }
	}

	public List<MoRuongData> ListTop = new List<MoRuongData>();

	public int id;
}

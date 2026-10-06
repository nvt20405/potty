using System.Collections.Generic;

public class BanhChungBXHResponse : ExDataBase
{
	public class BXHItem
	{
		public int GID { get; set; }

		public string UName { get; set; }

		public int SID { get; set; }

		public string UAva { get; set; }

		public int SoBanh { get; set; }
	}

	public List<BXHItem> ListHomNay { get; set; }

	public List<BXHItem> ListHomTruoc { get; set; }

	public NguoiNauBanh Player { get; set; }

	public List<PhanThuongResponse> ListPhanThuong { get; set; }
}

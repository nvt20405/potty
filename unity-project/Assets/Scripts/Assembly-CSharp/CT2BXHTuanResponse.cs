using System.Collections.Generic;

public class CT2BXHTuanResponse : ExDataBase
{
	public class TopMonPhai
	{
		public string DanhHieu { get; set; }

		public int Vip { get; set; }

		public int Level { get; set; }

		public int GID { get; set; }

		public string Ten { get; set; }

		public int DiemChienTich { get; set; }

		public PhanThuongResponse PhanThuong { get; set; }
	}

	public List<TopMonPhai> ListTop { get; set; }
}

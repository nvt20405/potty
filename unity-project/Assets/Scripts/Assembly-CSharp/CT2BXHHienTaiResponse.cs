using System.Collections.Generic;

public class CT2BXHHienTaiResponse : ExDataBase
{
	public class TopMonPhai
	{
		public int Hang { get; set; }

		public int Vip { get; set; }

		public int Level { get; set; }

		public int ServerId { get; set; }

		public int GID { get; set; }

		public string Ten { get; set; }

		public int Diem { get; set; }

		public int Kill { get; set; }

		public PhanThuongResponse PhanThuong { get; set; }
	}

	public List<TopMonPhai> ListTopTaPhai { get; set; }

	public List<TopMonPhai> ListTopChinhPhai { get; set; }

	public int TeamPlayer { get; set; }
}

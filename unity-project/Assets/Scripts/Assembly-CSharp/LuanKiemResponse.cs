using System.Collections.Generic;

public class LuanKiemResponse : ExDataBase
{
	public class MonPhaiLKInfo
	{
		public int GID { get; set; }

		public int BotID { get; set; }

		public string Name { get; set; }

		public int Level { get; set; }

		public int Hang { get; set; }

		public string Desc { get; set; }

		public bool EnableKhieuChien { get; set; }

		public string[] NhanVats { get; set; }
	}

	public class DoiThuongItem
	{
		public string AvatarCodeName { get; set; }

		public string RewardDesc { get; set; }

		public string MoTaDesc { get; set; }

		public bool IsLinhThuong { get; set; }

		public int CodeDoiThuong { get; set; }

		public bool IsActive { get; set; }
	}

	public string TichDiemDescription { get; set; }

	public int SoLuotKhieuChien { get; set; }

	public UserInfo UserInfoResponse { get; set; }

	public List<MonPhaiLKInfo> TopLuanKiem { get; set; }

	public List<MonPhaiLKInfo> Top10 { get; set; }

	public List<DoiThuongItem> ListDoiThuong { get; set; }
}

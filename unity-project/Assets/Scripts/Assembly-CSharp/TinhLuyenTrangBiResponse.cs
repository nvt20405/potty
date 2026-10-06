public class TinhLuyenTrangBiResponse : ExDataBase
{
	public int TrangBiID { get; set; }

	public int OldTinhLuyenLvl { get; set; }

	public int NewTinhLuyenLvl { get; set; }

	public UserInfo UpdateInfo { get; set; }
}

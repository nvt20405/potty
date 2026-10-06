public class DanhDanhSonResponse : ExDataBase
{
	public int DanhSonIdx { get; set; }

	public BattleReplay Battle1 { get; set; }

	public BattleReplay Battle2 { get; set; }

	public BattleReplay Battle3 { get; set; }

	public UserInfo UpdateUserInfo { get; set; }

	public PhanThuongResponse PhanThuong { get; set; }
}

public class DongNhanBattleResponse : ExDataBase
{
	public DongNhanResponse DongNhan { get; set; }

	public UserInfo UpdateUserInfo { get; set; }

	public BattleReplay Battle { get; set; }

	public PhanThuongResponse PhanThuong { get; set; }
}

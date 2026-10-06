public class BatCocResponse : ExDataBase
{
	public int GID { get; set; }

	public BattleReplay Battle { get; set; }

	public PhanThuongResponse PhanThuong { get; set; }

	public int ExpMP { get; set; }

	public int Bac { get; set; }

	public int ExpNV { get; set; }

	public UserInfo UpdateUserInfo { get; set; }
}

public class KetQuaThachDauResponse : ExDataBase
{
	public int EnemyID { get; set; }

	public string EnemyName { get; set; }

	public int TienCuoc { get; set; }

	public bool IsWon { get; set; }

	public BattleReplay Battle { get; set; }

	public UserInfo UpdateUserInfo { get; set; }

	public PhanThuongResponse PhanThuong { get; set; }

	public int ExpMonPhai { get; set; }

	public int ExpDeTu { get; set; }
}

public class DauLuanKiemResponse : ExDataBase
{
	public LuanKiemResponse LuanKiemInfo { get; set; }

	public BattleReplay BattleReplayData { get; set; }

	public int EnemyID { get; set; }

	public long ExpMP { get; set; }

	public long ExpDeTu { get; set; }

	public long BacReward { get; set; }

	public PhanThuongResponse PhanThuong { get; set; }
}

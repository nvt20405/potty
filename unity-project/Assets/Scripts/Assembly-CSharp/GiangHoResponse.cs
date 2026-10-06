public class GiangHoResponse : ExDataBase
{
	public BattleReplay Battle { get; set; }

	public PhanThuongResponse PhanThuong { get; set; }

	public int GiangHoIdx { get; set; }

	public int NhiemVuIdx { get; set; }

	public long ExpMP { get; set; }

	public long ExpDeTu { get; set; }

	public long BacReward { get; set; }

	public UserInfo UpdateUserInfo { get; set; }

	public bool GiangHoTinhAnh { get; set; }
}

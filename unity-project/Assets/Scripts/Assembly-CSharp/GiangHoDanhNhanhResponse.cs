using System.Collections.Generic;

public class GiangHoDanhNhanhResponse : ExDataBase
{
	public List<PhanThuongResponse> PhanThuong { get; set; }

	public int GiangHoIdx { get; set; }

	public int NhiemVuIdx { get; set; }

	public List<long> ExpMP { get; set; }

	public List<long> ExpDeTu { get; set; }

	public List<long> BacReward { get; set; }

	public UserInfo UpdateUserInfo { get; set; }

	public bool GiangHoTinhAnh { get; set; }
}

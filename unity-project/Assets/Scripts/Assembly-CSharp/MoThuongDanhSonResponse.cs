using System.Collections.Generic;

public class MoThuongDanhSonResponse : ExDataBase
{
	public int DanhSonIdx { get; set; }

	public int OpenedSlot { get; set; }

	public int OpSlot2 { get; set; }

	public int OpSlot3 { get; set; }

	public List<PhanThuongResponse.PhanThuong> PhanThuongList { get; set; }

	public UserInfo UpdateUserInfo { get; set; }
}

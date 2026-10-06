using System.Collections.Generic;

public class GetListBaoKhoHang2Response : ExDataBase
{
	public List<UserInfo.BaoKhoInfo> listBaoKhoDong = new List<UserInfo.BaoKhoInfo>();

	public List<UserInfo.BaoKhoInfo> listBaoKhoNgan = new List<UserInfo.BaoKhoInfo>();

	public int GID { get; set; }
}

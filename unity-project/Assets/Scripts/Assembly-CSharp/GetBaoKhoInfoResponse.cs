using System.Collections.Generic;

public class GetBaoKhoInfoResponse : ExDataBase
{
	public List<UserInfo.BaoKhoInfo> listBaoKhoNgoc = new List<UserInfo.BaoKhoInfo>();

	public List<UserInfo.BaoKhoInfo> listBaoKhoKim = new List<UserInfo.BaoKhoInfo>();

	public List<UserInfo.BaoKhoInfo> listMyBaoKho = new List<UserInfo.BaoKhoInfo>();

	public int GID { get; set; }
}

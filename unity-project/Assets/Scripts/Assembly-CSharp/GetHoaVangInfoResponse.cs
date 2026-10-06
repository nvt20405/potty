using System;
using System.Collections.Generic;

public class GetHoaVangInfoResponse : ExDataBase
{
	public List<PhanThuongResponse.PhanThuong> ListHoaVangData;

	public DateTime LastUpdateTime { get; set; }
}

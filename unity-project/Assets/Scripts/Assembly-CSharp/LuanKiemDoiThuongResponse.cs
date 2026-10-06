using System.Collections.Generic;

public class LuanKiemDoiThuongResponse : ExDataBase
{
	public List<LuanKiemResponse.DoiThuongItem> List { get; set; }

	public PhanThuongResponse PhanThuong { get; set; }
}

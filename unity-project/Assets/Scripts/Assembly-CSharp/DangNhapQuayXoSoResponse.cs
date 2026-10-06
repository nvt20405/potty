using System.Collections.Generic;

public class DangNhapQuayXoSoResponse : ExDataBase
{
	public PhanThuongResponse PhanThuong = new PhanThuongResponse();

	public List<string> VisualList = new List<string>();

	public int Num { get; set; }
}

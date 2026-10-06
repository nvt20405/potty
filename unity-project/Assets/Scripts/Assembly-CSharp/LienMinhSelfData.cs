using System.Collections.Generic;

public class LienMinhSelfData
{
	public int CongHien;

	public int CongHienLienMinh;

	public List<LienMinhNhiemVu> NhiemVuList;

	public List<int> RequestList;

	public bool FreeCongHien;

	public LienMinhSelfData()
	{
		CongHien = 0;
		CongHienLienMinh = 0;
		NhiemVuList = new List<LienMinhNhiemVu>();
		RequestList = new List<int>();
	}
}

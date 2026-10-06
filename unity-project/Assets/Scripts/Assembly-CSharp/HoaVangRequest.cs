using System.Collections.Generic;

public class HoaVangRequest : ExDataBase
{
	public enum LoaiHoaVang
	{
		TRANG_BI = 1,
		VAT_PHAM_TIEU_THU = 2
	}

	public int index { get; set; }

	public LoaiHoaVang loaiHoaVang { get; set; }

	public List<int> listTrangBiHoaVang { get; set; }
}

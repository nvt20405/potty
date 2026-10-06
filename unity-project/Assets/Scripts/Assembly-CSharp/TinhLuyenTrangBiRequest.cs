using System.Collections.Generic;

public class TinhLuyenTrangBiRequest
{
	public class ManhTrangBiSuDung
	{
		public int ID { get; set; }

		public int Quantity { get; set; }
	}

	public List<ManhTrangBiSuDung> listManhSuDung = new List<ManhTrangBiSuDung>();

	public int TayTuyDan_ID { get; set; }

	public int TrangBiID { get; set; }
}

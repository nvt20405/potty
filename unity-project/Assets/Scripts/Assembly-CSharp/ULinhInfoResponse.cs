using System;
using System.Collections.Generic;

public class ULinhInfoResponse : ExDataBase
{
	public class ULinhDoiDoItem
	{
		public PhanThuongResponse.PhanThuong VatPhamDoi = new PhanThuongResponse.PhanThuong();

		public PhanThuongResponse.PhanThuong VatPhamNhan = new PhanThuongResponse.PhanThuong();

		public int Diem { get; set; }
	}

	public List<ULinhDoiDoItem> ListDoiDo = new List<ULinhDoiDoItem>();

	public List<int> ListDoiThuongCount = new List<int>();

	public int ID { get; set; }

	public DateTime ThoiGianReset { get; set; }

	public ULinhInfoResponse(int id)
	{
		ID = id;
	}

	public ULinhInfoResponse()
	{
		ID = 0;
	}
}

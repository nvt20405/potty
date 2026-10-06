using System;
using System.Collections.Generic;

public class XocDiaInfoResponse : ExDataBase
{
	public class XocDiaItem
	{
		public PhanThuongResponse.PhanThuong VatPhamCuoc = new PhanThuongResponse.PhanThuong();

		public PhanThuongResponse.PhanThuong PhanThuong = new PhanThuongResponse.PhanThuong();
	}

	public int ID { get; set; }

	public DateTime ThoiGianReset { get; set; }

	public List<XocDiaItem> ListItem { get; set; }

	public XocDiaInfoResponse(int id)
	{
		ID = id;
	}

	public XocDiaInfoResponse()
	{
		ID = 0;
	}
}

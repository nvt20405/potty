using System;
using System.Collections.Generic;

public class TayVucInfoResponse : ExDataBase
{
	public class TayVucData
	{
		public PhanThuongResponse.PhanThuong pt = new PhanThuongResponse.PhanThuong();

		public bool isBuy;

		public int GiaBan { get; set; }
	}

	public List<TayVucData> ListItem = new List<TayVucData>();

	public int ID { get; set; }

	public DateTime ThoiGianReset { get; set; }

	public string strThoiGianReset { get; set; }

	public TayVucInfoResponse(int id)
	{
		ID = id;
	}

	public TayVucInfoResponse()
	{
		ID = 0;
	}
}

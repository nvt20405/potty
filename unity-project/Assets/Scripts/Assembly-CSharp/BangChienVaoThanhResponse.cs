using System.Collections.Generic;

public class BangChienVaoThanhResponse : ExDataBase
{
	public ThanhChien ThanhChienObj { get; set; }

	public NguoiChoiBangChien Player { get; set; }

	public List<NguoiChoiBangChien> ListNguoiChoi { get; set; }

	public LienMinhCongThanh LMTop1 { get; set; }

	public LienMinhCongThanh LMTop2 { get; set; }

	public LienMinhCongThanh LMTop3 { get; set; }

	public LienMinhCongThanh LMPlayer { get; set; }

	public BangChienPhanThuong PhanThuong { get; set; }
}

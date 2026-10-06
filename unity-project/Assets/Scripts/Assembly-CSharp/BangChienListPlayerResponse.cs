using System.Collections.Generic;

public class BangChienListPlayerResponse : ExDataBase
{
	public List<NguoiChoiBangChien> ListOtherPlayer { get; set; }

	public ThanhChien Thanh { get; set; }

	public LienMinhCongThanh LMTop1 { get; set; }

	public LienMinhCongThanh LMTop2 { get; set; }

	public LienMinhCongThanh LMTop3 { get; set; }

	public int LMDamage { get; set; }
}

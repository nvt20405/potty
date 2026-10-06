public class BangChienPhanThuong : ExDataBase
{
	public NguoiChoiBangChien Player { get; set; }

	public PhanThuongResponse PhanThuong { get; set; }

	public int DiemCongHien { get; set; }

	public ThanhChien Thanh { get; set; }

	public LienMinhCongThanh LMTop1 { get; set; }

	public LienMinhCongThanh LMTop2 { get; set; }

	public LienMinhCongThanh LMTop3 { get; set; }

	public int LMDamage { get; set; }
}

public class BangChienCongThanhResponse : ExDataBase
{
	public ThanhChien Thanh { get; set; }

	public BattleReplay Battle { get; set; }

	public int Damage { get; set; }

	public int GID { get; set; }

	public int Gate { get; set; }

	public LienMinhCongThanh LMPlayer { get; set; }

	public LienMinhCongThanh LMTop1 { get; set; }

	public LienMinhCongThanh LMTop2 { get; set; }

	public LienMinhCongThanh LMTop3 { get; set; }

	public NguoiChoiBangChien Player { get; set; }

	public BangChienPhanThuong PhanThuong { get; set; }

	public NguoiChoiBangChien Enemy { get; set; }
}

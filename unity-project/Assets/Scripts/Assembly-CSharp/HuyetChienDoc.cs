public class HuyetChienDoc
{
	public int Gid { get; set; }

	public int LastLevel { get; set; }

	public int LastSaoDatDuoc { get; set; }

	public int Luot { get; set; }

	public int Level { get; set; }

	public int SaoDatDuoc { get; set; }

	public int SaoDangCo { get; set; }

	public int BestLevel { get; set; }

	public int BestSao { get; set; }

	public int TangMenh { get; set; }

	public int TangNgoai { get; set; }

	public int TangThanPhap { get; set; }

	public int TangKhi { get; set; }

	public HuyetChienOpponent DoiThuKho { get; set; }

	public HuyetChienOpponent DoiThuBt { get; set; }

	public HuyetChienOpponent DoiThuDe { get; set; }

	public bool TangThuocTinh { get; set; }

	public bool NhanThuong { get; set; }

	public ChiSoCoBan TangThuocTinh1 { get; set; }

	public ChiSoCoBan TangThuocTinh2 { get; set; }

	public ChiSoCoBan TangThuocTinh3 { get; set; }

	public int RecordInTop { get; set; }

	public int DuDoan { get; set; }

	public bool IsAlive { get; set; }

	public HuyetChienDoc(int gid)
	{
		Gid = gid;
		LastLevel = 0;
		Luot = 0;
		Level = -1;
		SaoDatDuoc = 0;
		SaoDangCo = 0;
		BestLevel = 0;
		BestSao = 0;
		DoiThuKho = new HuyetChienOpponent();
		DoiThuBt = new HuyetChienOpponent();
		DoiThuDe = new HuyetChienOpponent();
		TangNgoai = 0;
		TangMenh = 0;
		TangThanPhap = 0;
		TangKhi = 0;
		TangThuocTinh = true;
		NhanThuong = true;
		IsAlive = false;
		RecordInTop = 0;
		DuDoan = 100;
	}

	public HuyetChienDoc()
	{
		Gid = -1;
		LastLevel = 0;
		Luot = 0;
		Level = -1;
		SaoDatDuoc = 0;
		SaoDangCo = 0;
		BestLevel = 0;
		BestSao = 0;
		DoiThuKho = new HuyetChienOpponent();
		DoiThuBt = new HuyetChienOpponent();
		DoiThuDe = new HuyetChienOpponent();
		TangNgoai = 0;
		TangMenh = 0;
		TangThanPhap = 0;
		TangKhi = 0;
		TangThuocTinh = true;
		NhanThuong = true;
		IsAlive = false;
		RecordInTop = 0;
		DuDoan = 100;
	}
}

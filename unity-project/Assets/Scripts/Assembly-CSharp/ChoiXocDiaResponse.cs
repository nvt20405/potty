public class ChoiXocDiaResponse : ExDataBase
{
	public XocDiaInfoResponse UpdateXocDiaInfo { get; set; }

	public PhanThuongResponse PhanThuongResponse { get; set; }

	public bool IsWin { get; set; }

	public int XucXac1 { get; set; }

	public int XucXac2 { get; set; }

	public int XucXac3 { get; set; }

	public int TongXucXac { get; set; }
}

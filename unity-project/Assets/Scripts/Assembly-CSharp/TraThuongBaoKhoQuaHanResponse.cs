public class TraThuongBaoKhoQuaHanResponse : ExDataBase
{
	public int BID { get; set; }

	public int GID { get; set; }

	public int SID { get; set; }

	public int CumServer { get; set; }

	public long BacNhanDuoc { get; set; }

	public int NKDNhanDuoc { get; set; }

	public bool BaoKhoBiCuop { get; set; }

	public bool isThuHoiOldBaoKho { get; set; }

	public bool isThuHoiBaoKhoQuaHan { get; set; }

	public GetBaoKhoInfoResponse UpdateBaoKhoInfo { get; set; }
}

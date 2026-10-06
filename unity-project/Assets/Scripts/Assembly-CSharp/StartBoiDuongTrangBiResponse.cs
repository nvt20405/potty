public class StartBoiDuongTrangBiResponse : ExDataBase
{
	public UserInfo UpdateInfo { get; set; }

	public int TrangBiID { get; set; }

	public int MenhThayDoi { get; set; }

	public int NgoaiThayDoi { get; set; }

	public int ThanThayDoi { get; set; }

	public int KhiThayDoi { get; set; }

	public ChiSoCoBan LoaiCong { get; set; }

	public ChiSoCoBan LoaiTru { get; set; }
}

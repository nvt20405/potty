public class StartBoiDuongResponse : ExDataBase
{
	public UserInfo UpdateInfo { get; set; }

	public int HID { get; set; }

	public int MenhThayDoi { get; set; }

	public int NgoaiThayDoi { get; set; }

	public int KhiThayDoi { get; set; }

	public int ThanThayDoi { get; set; }

	public ChiSoCoBan LoaiCong { get; set; }

	public ChiSoCoBan LoaiTru { get; set; }

	public StartBoiDuongResponse()
	{
		HID = 0;
		MenhThayDoi = 0;
		NgoaiThayDoi = 0;
		KhiThayDoi = 0;
		ThanThayDoi = 0;
	}
}

public class StartBoiDuongChienHonResponse : ExDataBase
{
	public UserInfo UpdateInfo { get; set; }

	public int ID { get; set; }

	public int MenhThayDoi { get; set; }

	public int NgoaiThayDoi { get; set; }

	public int KhiThayDoi { get; set; }

	public int ThanThayDoi { get; set; }

	public ChiSoCoBan LoaiCong { get; set; }

	public ChiSoCoBan LoaiTru { get; set; }

	public StartBoiDuongChienHonResponse()
	{
		ID = 0;
		MenhThayDoi = 0;
		NgoaiThayDoi = 0;
		KhiThayDoi = 0;
		ThanThayDoi = 0;
	}
}

public class DongNhanRequest
{
	public enum RequestType
	{
		BinhThuong = 0,
		HoiSinhNgay = 1
	}

	public RequestType TypeRequest { get; set; }
}

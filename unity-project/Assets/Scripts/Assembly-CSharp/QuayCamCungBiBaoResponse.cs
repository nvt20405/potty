public class QuayCamCungBiBaoResponse : ExDataBase
{
	public UserInfo UpdateInfo { get; set; }

	public int LuotQuay { get; set; }

	public PhanThuongResponse ptResponse { get; set; }

	public int indexPT { get; set; }

	public QuayCamCungType TypeSelected { get; set; }

	public bool isNhanThuongNow { get; set; }
}

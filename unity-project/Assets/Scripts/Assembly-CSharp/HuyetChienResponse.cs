public class HuyetChienResponse : ExDataBase
{
	public HuyetChienDoc HuyetChienInfo { get; set; }

	public int PhanTramBuff { get; set; }

	public PhanThuongResponse PhanThuong { get; set; }

	public UserInfo UpdateUserInfo { get; set; }

	public string TienHoiSinh { get; set; }

	public int GiaTienHoiSinh { get; set; }
}

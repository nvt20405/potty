using System.Collections.Generic;

public class QuayDiemHoaVangResponse : ExDataBase
{
	public List<LoaiQuayDiemHoaVang> loaiKetQua;

	public UserInfo UpdateInfo { get; set; }

	public PhanThuongResponse ptResponse { get; set; }
}

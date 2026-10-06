public class LienMinhNhiemVu
{
	public enum NHIEMVU
	{
		TIEU_DIET_AC_NHAN = 0,
		TRUY_TIM_BAO_VAT = 1,
		BAT_CHUOT = 2,
		BAT_COC = 3,
		CAM_DIA = 4,
		THACH_DAU = 5,
		LUAN_KIEM = 6
	}

	public int Id { get; set; }

	public int Gid { get; set; }

	public int NhiemVuNo { get; set; }

	public string NhiemVuDesc { get; set; }

	public NHIEMVU NhiemVuType { get; set; }

	public int Count { get; set; }

	public int YeuCau { get; set; }

	public string ClientDesc { get; set; }

	public bool Active { get; set; }

	public int CongHienValue { get; set; }

	public LienMinhNhiemVu(int id, int gid, int nhiemVuNo)
	{
		Id = id;
		Gid = gid;
		NhiemVuNo = nhiemVuNo;
		NhiemVuType = NHIEMVU.TIEU_DIET_AC_NHAN;
		Count = 0;
		YeuCau = 0;
		Active = false;
		CongHienValue = 0;
		ClientDesc = string.Empty;
	}

	public LienMinhNhiemVu()
	{
		Id = 0;
		Gid = 0;
		NhiemVuNo = 0;
		NhiemVuType = NHIEMVU.TIEU_DIET_AC_NHAN;
		Count = 0;
		YeuCau = 0;
		Active = true;
		CongHienValue = 0;
		ClientDesc = string.Empty;
	}
}

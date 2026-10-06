public class StartBoiDuongRequest
{
	public enum LoaiBoiDuong
	{
		BoiDuong1Lan = 0,
		BoiDuong1LanCaoCap = 1,
		BoiDuong10Lan = 2,
		BoiDuong10LanCaoCap = 3
	}

	public int HeroID { get; set; }

	public int BoiDuongDanID { get; set; }

	public LoaiBoiDuong loai { get; set; }
}

public class BoiDuongTrangBiHoangKimRequest
{
	public enum LoaiLuyenHoa
	{
		BoiDuong1Lan = 0,
		BoiDuong10Lan = 1
	}

	public int TrangBiID { get; set; }

	public LoaiLuyenHoa loai { get; set; }
}

public class CacLoaiTopRequest
{
	public enum TopType
	{
		TopChienTruong = 0,
		TopTinhLuyen = 1,
		TopCongLuc = 2,
		TopBiKip = 3,
		TopNgoc = 4,
		TopHanhTau = 5,
		TopHoangKim = 6,
		TopChuyenSinh = 7,
		TopTuLinh = 8,
		TopTrangBiHK = 9,
		TopThienMaLenh = 10,
		TopNONE = 11
	}

	public TopType LoaiTop { get; set; }
}

using System.Collections.Generic;

public class CacLoaiTopResponse : ExDataBase
{
	public class TopChienTruongData
	{
		public int GID { get; set; }

		public string DisplayName { get; set; }

		public int pkChienTruong { get; set; }
	}

	public class TopTinhLuyenData
	{
		public int GID { get; set; }

		public string DisplayName { get; set; }

		public int TinhLuyenCap1 { get; set; }

		public int TinhLuyenCap2 { get; set; }

		public int TinhLuyenCap3 { get; set; }

		public int TinhLuyenCap5 { get; set; }
	}

	public class TopCongLucData
	{
		public int GID { get; set; }

		public string UserName { get; set; }

		public string NhanVatName { get; set; }

		public int ChiSo { get; set; }
	}

	public class TopBiKipData
	{
		public int GID { get; set; }

		public string DisplayName { get; set; }

		public int SoBiKipMaxLevel { get; set; }
	}

	public class TopNgocData
	{
		public int GID { get; set; }

		public string DisplayName { get; set; }

		public int SoNgoc { get; set; }
	}

	public class TopHanhTauData
	{
		public int GID { get; set; }

		public string UserName { get; set; }

		public string TenGiangHo { get; set; }

		public string TenAiGiangHo { get; set; }
	}

	public class TopHoangKimData
	{
		public int GID { get; set; }

		public string UserName { get; set; }

		public int SoLuong { get; set; }
	}

	public class TopChuyenSinhData
	{
		public int GID { get; set; }

		public string UserName { get; set; }

		public int SoLuong { get; set; }
	}

	public class TopTuLinhData
	{
		public int GID { get; set; }

		public string UserName { get; set; }

		public string PhamChat { get; set; }

		public int ChiSo { get; set; }
	}

	public class TopThienMaData
	{
		public int GID { get; set; }

		public string DisplayName { get; set; }

		public int TotalDiem { get; set; }
	}

	public class TopTrangBiHoangKimData
	{
		public string DisplayName { get; set; }

		public int TopTrangBiHKCap3 { get; set; }

		public int TopTrangBiHKCap2 { get; set; }

		public int TopTrangBiHKCap1 { get; set; }
	}

	public CacLoaiTopRequest.TopType TopType = CacLoaiTopRequest.TopType.TopNONE;

	public List<TopChienTruongData> ListTopChienTruong = new List<TopChienTruongData>();

	public List<TopTinhLuyenData> ListTopTinhLuyen = new List<TopTinhLuyenData>();

	public List<TopCongLucData> ListTopCongLucNgoai = new List<TopCongLucData>();

	public List<TopCongLucData> ListTopCongLucMenh = new List<TopCongLucData>();

	public List<TopCongLucData> ListTopCongLucThan = new List<TopCongLucData>();

	public List<TopCongLucData> ListTopCongLucKhi = new List<TopCongLucData>();

	public List<TopBiKipData> ListTopBiKip = new List<TopBiKipData>();

	public List<TopNgocData> ListTopNgoc = new List<TopNgocData>();

	public List<TopHanhTauData> ListTopHanhTau = new List<TopHanhTauData>();

	public List<TopHoangKimData> ListTopHoangKim = new List<TopHoangKimData>();

	public List<TopChuyenSinhData> ListTopChuyenSinh = new List<TopChuyenSinhData>();

	public List<TopTuLinhData> ListTopTuLinh = new List<TopTuLinhData>();

	public List<TopThienMaData> ListTopThienMa = new List<TopThienMaData>();

	public List<TopTrangBiHoangKimData> ListTopTBHoangKim = new List<TopTrangBiHoangKimData>();
}

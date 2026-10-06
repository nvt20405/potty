using System.Collections.Generic;

public class HoaVangCfg
{
	public class HoaVangItem
	{
		public string Name { get; set; }

		public string DisplayName { get; set; }

		public int DiemNhanDuoc { get; set; }

		public int Count { get; set; }

		public PhanThuongResponse.LoaiPhanThuong Loai { get; set; }
	}

	public class ComboItem
	{
		public List<string> ListYeuCau { get; set; }

		public PhanThuongResponse.PhanThuong PhanThuongItem { get; set; }
	}

	public List<HoaVangItem> ListHoaVang { get; set; }

	public List<ComboItem> ComboPhanThuong { get; set; }
}

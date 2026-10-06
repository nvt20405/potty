using System.Collections.Generic;

public class UseTanHonTangLevelChienHonRequest
{
	public enum DungTienLinhDan
	{
		Use1 = 1,
		Use10 = 10,
		Use100 = 100
	}

	public class HonNhanVat
	{
		public int Count { get; set; }

		public int ID { get; set; }
	}

	private List<HonNhanVat> _HonNVsNeed = new List<HonNhanVat>();

	public int ChienHonID { get; set; }

	public DungTienLinhDan UseTienLinhDan { get; set; }

	public List<HonNhanVat> HonNVsNeed
	{
		get
		{
			return _HonNVsNeed;
		}
		set
		{
			_HonNVsNeed = value;
		}
	}
}

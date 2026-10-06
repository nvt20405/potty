public class ThanhChien
{
	private string _lmName = string.Empty;

	private string _bangchu = string.Empty;

	public int LienMinhID { get; set; }

	public int LienMinhServerID { get; set; }

	public string LMName
	{
		get
		{
			return _lmName;
		}
		set
		{
			_lmName = value;
		}
	}

	public string BangChu
	{
		get
		{
			return _bangchu;
		}
		set
		{
			_bangchu = value;
		}
	}

	public int TuanChiemDuoc { get; set; }

	public int CumServerID { get; set; }

	public int ThanhIdx { get; set; }

	public int CongThanh1 { get; set; }

	public int CongThanh2 { get; set; }

	public int CongThanh3 { get; set; }

	public int DefBuffMau { get; set; }

	public int DangCongThanh { get; set; }

	public int ID { get; set; }
}

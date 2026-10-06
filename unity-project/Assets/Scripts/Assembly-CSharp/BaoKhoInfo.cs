using System;

public class BaoKhoInfo
{
	public enum LoaiBaoKho
	{
		NGOC = 1,
		KIM = 2,
		NGAN = 3,
		DONG = 4
	}

	public int BID { get; set; }

	public int GID { get; set; }

	public int SID { get; set; }

	public int Cum { get; set; }

	public string DisplayName { get; set; }

	public int Level { get; set; }

	public string Vip { get; set; }

	public int BaoKhoType { get; set; }

	public DateTime TimeChiem { get; set; }
}

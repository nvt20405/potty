using System.Collections.Generic;

public class LayDeTuResponse : ExDataBase
{
	public string NhanVatName { get; set; }

	public int TanHonCount { get; set; }

	public List<string> BonusTanHonName { get; set; }

	public int BonusTanHonNhanDuocIdx { get; set; }

	public int BonusTanHonNhanDuocCount { get; set; }

	public UserInfo UpdateInfo { get; set; }
}

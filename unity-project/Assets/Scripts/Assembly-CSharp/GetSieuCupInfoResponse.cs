using System;
using System.Collections.Generic;

public class GetSieuCupInfoResponse : ExDataBase
{
	public DateTime TimeStart { get; set; }

	public List<SieuCupGame> ListTranDau { get; set; }

	public List<SieuCupPlayer> ListPlayers { get; set; }

	public List<SieuCupDatCuoc> ListDatCuoc { get; set; }

	public SieuCupChampion Champion { get; set; }

	public string ThongBao { get; set; }

	public UserInfo UpdateUserInfo { get; set; }
}

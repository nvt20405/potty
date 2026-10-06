using System.Collections.Generic;

public class CuopBaoKhoReponse : ExDataBase
{
	public int GID;

	public int SID;

	public string DisplayName;

	public int VIP;

	public int Level;

	public bool IsSuccess;

	public List<BattleReplay> Replays;

	public UserInfo UpdateInfo;

	public PhanThuongResponse phanthuong;

	public bool isRequestBattleData;

	public UserInfo.BaoKhoInfo BaoKhoData { get; set; }

	public GetBaoKhoInfoResponse UpdateBaoKhoInfo { get; set; }
}

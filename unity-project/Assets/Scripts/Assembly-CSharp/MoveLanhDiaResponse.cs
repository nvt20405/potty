using System.Collections.Generic;

public class MoveLanhDiaResponse : ExDataBase
{
	public int GID;

	public int SID;

	public string Position;

	public bool IsSuccess;

	public List<BattleReplay> Replays;

	public UserInfo LanhDia;

	public PhanThuongResponse phanthuong;
}

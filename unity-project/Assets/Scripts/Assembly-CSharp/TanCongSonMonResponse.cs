using System.Collections.Generic;

public class TanCongSonMonResponse : ExDataBase
{
	public List<BattleReplay> FullMatch;

	public string AttackerName;

	public string DefenderName;

	public List<string> DefendHeroes;

	public List<string> AttackHeroes;

	public UserInfo updateInfo;

	public PhanThuongResponse phanthuong;
}

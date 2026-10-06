using System.Collections.Generic;

public class BanPhaoHoaResponse : ExDataBase
{
	public int score;

	public int totalScore;

	public string pos = string.Empty;

	public bool isOwn;

	public List<KeyValuePair<string, int>> TopInfo { get; set; }
}

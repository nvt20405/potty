using System.Collections.Generic;

public class LienMinhActivity
{
	public int LienMinhID;

	public List<string> Contents;

	public LienMinhActivity(int id)
	{
		LienMinhID = id;
		Contents = new List<string>();
	}

	public LienMinhActivity()
	{
		LienMinhID = 0;
		Contents = new List<string>();
	}
}

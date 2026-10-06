using System.Collections.Generic;

public class ChatHistory
{
	public List<string> listChat = new List<string>();

	public int ID;

	public ChatHistory()
	{
	}

	public ChatHistory(int _id)
	{
		ID = _id;
	}
}

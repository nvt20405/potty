using System.Collections.Generic;

public class ChatInfo
{
	public List<ChatItem> listChat = new List<ChatItem>();

	public int ID;

	public ChatInfo()
	{
	}

	public ChatInfo(int _id)
	{
		ID = _id;
	}
}

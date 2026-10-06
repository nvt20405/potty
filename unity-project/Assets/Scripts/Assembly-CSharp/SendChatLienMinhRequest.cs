using System.Collections.Generic;

public class SendChatLienMinhRequest
{
	public int lienminhID;

	public List<int> MemberList;

	public string Content { get; set; }
}

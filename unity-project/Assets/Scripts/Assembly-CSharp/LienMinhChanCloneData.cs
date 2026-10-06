using System;

public class LienMinhChanCloneData
{
	public int LienMinhID;

	public int AcceptCount;

	public DateTime LogDate;

	public DateTime StartDate;

	public LienMinhChanCloneData(int LienMinhID, int AcceptCount, DateTime StartDate)
	{
		this.LienMinhID = LienMinhID;
		this.AcceptCount = AcceptCount;
		LogDate = StartDate;
		this.StartDate = StartDate;
	}
}

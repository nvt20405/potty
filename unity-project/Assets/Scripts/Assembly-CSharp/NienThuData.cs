using System;

public class NienThuData
{
	public int CurHP;

	public int MaxHP;

	public int PosID;

	public DateTime TimeStart;

	public int SecretValue;

	public int TotalScore;

	public NienThuData()
	{
	}

	public NienThuData(DateTime timeStart, int totalScore)
	{
		CurHP = 100;
		MaxHP = 100;
		SecretValue = new Random().Next(0, 9999999);
		PosID = new Random().Next(0, 7);
		TotalScore = totalScore;
		TimeStart = timeStart;
	}
}

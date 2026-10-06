using System.Collections.Generic;

public class ThanThuDaoInfo
{
	public class WallBlock
	{
		public int X;

		public int Y;

		public bool Top;

		public bool Bot;

		public bool Left;

		public bool Right;

		public WallBlock()
		{
			Top = false;
			Bot = false;
			Left = false;
			Right = false;
			X = 0;
			Y = 0;
		}

		public WallBlock(string info)
		{
			if (info.Length != 6)
			{
				Top = false;
				Bot = false;
				Left = false;
				Right = false;
				X = 0;
				Y = 0;
				return;
			}
			X = int.Parse(info[0].ToString());
			Y = int.Parse(info[1].ToString());
			if (int.Parse(info[2].ToString()) == 1)
			{
				Top = true;
			}
			else
			{
				Top = false;
			}
			if (int.Parse(info[3].ToString()) == 1)
			{
				Bot = true;
			}
			else
			{
				Bot = false;
			}
			if (int.Parse(info[4].ToString()) == 1)
			{
				Left = true;
			}
			else
			{
				Left = false;
			}
			if (int.Parse(info[5].ToString()) == 1)
			{
				Right = true;
			}
			else
			{
				Right = false;
			}
		}
	}

	public class MobInfo
	{
		public int X;

		public int Y;

		public string codeName;

		public MobInfo(string info)
		{
			if (info.Length < 3)
			{
				X = 0;
				Y = 0;
				codeName = string.Empty;
			}
			else
			{
				X = int.Parse(info[0].ToString());
				Y = int.Parse(info[1].ToString());
				codeName = info.Substring(2, info.Length - 2);
			}
		}
	}

	public List<WallBlock> WallList;

	public List<MobInfo> MobList;

	public int Goal;

	public List<string> _wallList
	{
		set
		{
			if (value == null || value.Count <= 0)
			{
				return;
			}
			WallList = new List<WallBlock>();
			foreach (string item in value)
			{
				WallList.Add(new WallBlock(item));
			}
		}
	}

	public List<string> _mobList
	{
		set
		{
			if (value == null || value.Count <= 0)
			{
				return;
			}
			MobList = new List<MobInfo>();
			foreach (string item in value)
			{
				MobList.Add(new MobInfo(item));
			}
		}
	}
}

using System.Collections.Generic;

public class FriensInfoResponse : ExDataBase
{
	public class FriendInfo
	{
		public int GID { get; set; }

		public List<int> HeroIds { get; set; }

		public List<string> HeroCodeNames { get; set; }

		public List<int> HeroLevels { get; set; }
	}

	public List<FriendInfo> ListHeros { get; set; }
}

namespace Nettention.Proud
{
	internal class P2PGroup_C
	{
		public HostID groupHostID;

		public P2PGroupMembers_C members = new P2PGroupMembers_C();

		private void ToInfo(ref P2PGroup ret)
		{
			foreach (HostID key in members.Keys)
			{
				ret.members.Add(key);
			}
			ret.groupHostID = groupHostID;
		}
	}
}

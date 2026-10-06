namespace Nettention.Proud
{
	internal class MemberLocalEvent : LocalEvent
	{
		public HostID memberHostID;

		public HostID groupHostID;

		public int memberCount;

		public ByteArray customField = new ByteArray();
	}
}

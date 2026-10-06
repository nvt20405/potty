namespace Nettention.Proud
{
	internal class ServerAsSendDest : ISendDest_C, IP2PGroupMember
	{
		internal object hostTag;

		internal NetClient owner;

		public HostID MemberHostID
		{
			get
			{
				return HostID.Server;
			}
		}

		public HostID SendDestHostID
		{
			get
			{
				return HostID.Server;
			}
		}

		public long IndirectServerTimeDiffMs
		{
			get
			{
				return owner.IndirectServerTimeDiffMs;
			}
		}

		public ServerAsSendDest(NetClient owner)
		{
			this.owner = owner;
		}
	}
}

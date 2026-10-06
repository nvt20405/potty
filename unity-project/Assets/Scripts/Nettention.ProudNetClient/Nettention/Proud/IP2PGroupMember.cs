namespace Nettention.Proud
{
	internal interface IP2PGroupMember
	{
		HostID MemberHostID { get; }

		long IndirectServerTimeDiffMs { get; }
	}
}

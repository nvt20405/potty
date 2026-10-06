namespace Nettention.Proud
{
	internal enum LocalEventType
	{
		None = 0,
		ConnectServerSuccess = 1,
		ConnectServerFail = 2,
		ClientServerDisconnect = 3,
		ClientJoinCandidate = 4,
		ClientJoinApproved = 5,
		ClientLeaveAfterDispose = 6,
		AddMemberAckComplete = 7,
		AddMember = 8,
		DelMember = 9,
		DirectP2PEnabled = 10,
		RelayP2PEnabled = 11,
		GroupP2PEnabled = 12,
		ServerUdpChanged = 13,
		SynchronizeServerTime = 14,
		HackSuspected = 15,
		TcpListenFail = 16,
		P2PGroupRemoved = 17,
		P2PDisconnected = 18,
		UnitTestFail = 19,
		Error = 20,
		Warning = 21
	}
}

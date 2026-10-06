using System.Collections.Generic;
using System.Net;

namespace Nettention.Proud
{
	public class NetPeerInfo
	{
		public IPEndPoint udpAddrFromServer;

		public IPEndPoint udpAddrInternal;

		public HostID hostID;

		public bool relayedP2P;

		public List<HostID> joinedP2PGroups = new List<HostID>();

		public bool isBehindNat;

		public bool realUdpEnabled;

		public int recentPingMs;

		public uint sendQueuedAmountInBytes;

		public object hostTag;

		public double directP2PPeerFrameRate;

		public int toRemotePeerSendUdpMessageTrialCount;

		public int toRemotePeerSendUdpMessageSuccessCount;

		public override string ToString()
		{
			return string.Format("HostID={0},RelayedP2P={1},JoinedP2PGroupCount={2},IsBehindNat={3},RealUdpEnabled={4}", hostID, relayedP2P, joinedP2PGroups.Count, isBehindNat, realUdpEnabled);
		}
	}
}

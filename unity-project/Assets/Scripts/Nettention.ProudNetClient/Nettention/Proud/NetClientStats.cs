using System;

namespace Nettention.Proud
{
	public class NetClientStats : ICloneable
	{
		public ulong totalTcpReceiveBytes;

		public ulong totalTcpSendBytes;

		public ulong totalUdpSendCount;

		public ulong totalUdpSendBytes;

		public ulong totalUdpReceiveCount;

		public ulong totalUdpReceiveBytes;

		public int remotePeerCount;

		public bool serverUdpEnabled;

		public uint directP2PEnabledPeerCount;

		public ulong TotalSendBytes
		{
			get
			{
				return totalTcpSendBytes + totalUdpSendBytes;
			}
		}

		public ulong TotalReceiveBytes
		{
			get
			{
				return totalTcpReceiveBytes + totalUdpReceiveBytes;
			}
		}

		object ICloneable.Clone()
		{
			return Clone();
		}

		public NetClientStats Clone()
		{
			return (NetClientStats)MemberwiseClone();
		}

		public override string ToString()
		{
			return string.Format("ServerUdpEnabled={0},RemotePeerCount={1},DirectP2PEnabledPeerCount={2},TotalUdpReceiveBytes={3}", serverUdpEnabled, remotePeerCount, directP2PEnabledPeerCount, totalUdpReceiveBytes);
		}
	}
}

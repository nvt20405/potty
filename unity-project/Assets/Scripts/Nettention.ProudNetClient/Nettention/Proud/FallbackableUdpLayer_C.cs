using System;
using System.Net;

namespace Nettention.Proud
{
	internal class FallbackableUdpLayer_C
	{
		private NetClient owner;

		public Guid holepunchMagicNumber;

		public volatile bool serverUdpReadyWaiting;

		public volatile bool realUdpEnabled_USE_FUNCTION;

		public long realUdpEnabledTime;

		public IPEndPoint serverAddr;

		public long holepunchCooltime = NetConfig.InfiniteCoolTimeMs;

		public int tcpFallbackCount;

		public long lastServerUdpPacketReceivedTime;

		public volatile int lastServerUdpPacketReceivedCount;

		public long lastUdpPacketReceivedInterval = -1L;

		private TcpLayer_C FallbackTcpLayer
		{
			get
			{
				return owner.ToServerTcp;
			}
		}

		public bool RealUdpEnabled
		{
			get
			{
				return realUdpEnabled_USE_FUNCTION;
			}
			set
			{
				if (realUdpEnabled_USE_FUNCTION != value)
				{
					realUdpEnabled_USE_FUNCTION = value;
					lastServerUdpPacketReceivedTime = PreciseCurrentTime.GetTimeMs();
					lastServerUdpPacketReceivedCount = 0;
					lastUdpPacketReceivedInterval = -1L;
					if (value)
					{
						realUdpEnabledTime = PreciseCurrentTime.GetTimeMs();
					}
				}
			}
		}

		public FallbackableUdpLayer_C(NetClient owner)
		{
			this.owner = owner;
			lastServerUdpPacketReceivedTime = PreciseCurrentTime.GetTimeMs();
		}

		public void SendWithSplitterViaUdpOrTcp_Copy(HostID hostID, SendFragRefs sendData, SendOpt sendOpt)
		{
			if (RealUdpEnabled && !owner.ToServerUdp.IsSocketClosed())
			{
				if (!sendOpt.INTERNAL_USE_isProudNetSpecificRmi)
				{
					owner.toServerUdpSendCount++;
				}
				owner.ToServerUdp.AddToSendQueueWithSplitterAndSignal_Copy(hostID, FilterTag.CreateFilterTag(owner.LocalHostID, HostID.Server), serverAddr, sendData, PreciseCurrentTime.GetTimeMs(), sendOpt);
			}
			else
			{
				FallbackTcpLayer.AddToSendQueueWithSplitterAndSignal_Copy(sendData, sendOpt);
			}
		}
	}
}

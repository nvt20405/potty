namespace Nettention.Proud
{
	internal class ReliableUdpHostDelegates
	{
		internal delegate void SendOneFrameToUdpLayerDelegate(ReliableUdpFrame frame);

		internal delegate bool IsReliableChannelDelegate();

		internal delegate uint GetUdpSendBufferPacketFilledCountDelegate();

		internal delegate int GetRecentPingMsDelegate();

		public SendOneFrameToUdpLayerDelegate sendOneFrameToUdpLayer = (ReliableUdpFrame frame) =>
		{
		};

		public IsReliableChannelDelegate isReliableChannel = () => false;

		public GetUdpSendBufferPacketFilledCountDelegate getUdpSendBufferPacketFilledCount = () => 0u;

		public GetRecentPingMsDelegate getRecentPingMs = () => 0;
	}
}

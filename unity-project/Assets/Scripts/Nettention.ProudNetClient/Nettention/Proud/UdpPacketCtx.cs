namespace Nettention.Proud
{
	internal class UdpPacketCtx : ListNode<UdpPacketCtx>
	{
		public ByteArray packet = new ByteArray();

		public long uniqueID;

		public short ttl = -1;

		public HostID hostID;
	}
}

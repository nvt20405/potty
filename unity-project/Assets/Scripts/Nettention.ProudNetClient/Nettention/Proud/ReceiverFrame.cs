namespace Nettention.Proud
{
	internal class ReceiverFrame : ReliableUdpFrame
	{
		public ReceiverFrame(ReliableUdpFrame from)
		{
			from.CloneTo(this);
		}
	}
}

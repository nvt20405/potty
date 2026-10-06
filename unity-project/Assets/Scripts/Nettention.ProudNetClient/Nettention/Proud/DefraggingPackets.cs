using System.Collections.Generic;

namespace Nettention.Proud
{
	internal class DefraggingPackets : Dictionary<int, DefraggingPacket>
	{
		public UnreliableMessageLossMeasurer m_unreliableMessageLossRatio = new UnreliableMessageLossMeasurer();

		public SendSpeedMeasurer recentReceiveSpeed = new SendSpeedMeasurer();
	}
}

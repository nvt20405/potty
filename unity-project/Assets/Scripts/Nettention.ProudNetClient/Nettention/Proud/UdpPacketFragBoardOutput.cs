using System.Collections.Generic;
using System.Net;

namespace Nettention.Proud
{
	internal class UdpPacketFragBoardOutput
	{
		public ByteArray sendFragFrag = new ByteArray();

		public List<UdpPacketCtx> owningPackets = new List<UdpPacketCtx>();

		public IPEndPoint sendTo;

		public short ttl = -1;

		public void ResetForReuse()
		{
			owningPackets.Clear();
			sendFragFrag.Count = 0;
			sendTo = null;
			ttl = -1;
		}
	}
}

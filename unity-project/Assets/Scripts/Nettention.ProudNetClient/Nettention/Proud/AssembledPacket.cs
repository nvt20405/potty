using System.Net;

namespace Nettention.Proud
{
	internal class AssembledPacket
	{
		public DefraggingPacket packet;

		public IPEndPoint senderAddr;
	}
}

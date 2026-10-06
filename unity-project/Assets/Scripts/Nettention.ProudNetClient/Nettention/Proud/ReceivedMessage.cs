using System.Net;

namespace Nettention.Proud
{
	public class ReceivedMessage
	{
		public Message unsafeMessage;

		public HostID remoteHostID;

		public IPEndPoint remoteAddr_onlyUdp;

		public bool relayed;

		public Message WriteOnlyMessage
		{
			get
			{
				return unsafeMessage;
			}
		}

		public Message ReadOnlyMessage
		{
			get
			{
				return unsafeMessage;
			}
		}

		public IPEndPoint RemoteAddr
		{
			get
			{
				return remoteAddr_onlyUdp;
			}
		}

		public HostID RemoteHostID
		{
			get
			{
				return remoteHostID;
			}
		}

		public bool IsRelayed
		{
			get
			{
				return relayed;
			}
		}
	}
}

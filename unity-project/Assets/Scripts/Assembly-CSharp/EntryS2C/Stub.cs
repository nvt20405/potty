using Nettention.Proud;

namespace EntryS2C
{
	public class Stub : IJsonStub
	{
		public delegate bool NotifyRegisterDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool NotifyFirstLogonSuccessDelegate(HostID remote, RmiContext rmiContext, string serverDic);

		public delegate bool NotifyErrorDelegate(HostID remote, RmiContext rmiContext, int errorCode);

		public delegate bool NotifyAckDelegate(HostID remote, RmiContext rmiContext, string msg);

		public NotifyRegisterDelegate NotifyRegister;

		public NotifyFirstLogonSuccessDelegate NotifyFirstLogonSuccess;

		public NotifyErrorDelegate NotifyError;

		public NotifyAckDelegate NotifyAck;

		public bool Dispatch(string rmiName, string data)
		{
			switch (rmiName)
			{
			case "NotifyRegister":
				if (NotifyRegister != null)
				{
					return NotifyRegister(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyFirstLogonSuccess":
				if (NotifyFirstLogonSuccess != null)
				{
					return NotifyFirstLogonSuccess(HostID.Server, new RmiContext(), data);
				}
				return true;
			case "NotifyError":
				if (NotifyError != null)
				{
					int result = 0;
					if (!int.TryParse(data, out result))
					{
						try
						{
							string s = EGUtils.Decompress(data);
							int.TryParse(s, out result);
						}
						catch
						{
						}
					}
					return NotifyError(HostID.Server, new RmiContext(), result);
				}
				return true;
			case "NotifyAck":
				if (NotifyAck != null)
				{
					return NotifyAck(HostID.Server, new RmiContext(), data);
				}
				return true;
			default:
				return true;
			}
		}

		public bool HasHandler(string rmiName)
		{
			switch (rmiName)
			{
			case "NotifyRegister":
				return NotifyRegister != null;
			case "NotifyFirstLogonSuccess":
				return NotifyFirstLogonSuccess != null;
			case "NotifyError":
				return NotifyError != null;
			case "NotifyAck":
				return NotifyAck != null;
			default:
				return false;
			}
		}
	}
}

using Nettention.Proud;

namespace EntryC2S
{
	public class Stub : IJsonStub
	{
		public delegate bool RequestRegisterDelegate(HostID remote, RmiContext rmiContext, string data);

		public delegate bool RequestFirstLogonDelegate(HostID remote, RmiContext rmiContext, string data);

		public RequestRegisterDelegate RequestRegister;

		public RequestFirstLogonDelegate RequestFirstLogon;

		public bool Dispatch(string rmiName, string data)
		{
			if (!(rmiName == "RequestRegister"))
			{
				if (rmiName == "RequestFirstLogon")
				{
					if (RequestFirstLogon != null)
					{
						return RequestFirstLogon(HostID.Server, new RmiContext(), data);
					}
					return true;
				}
				return true;
			}
			if (RequestRegister != null)
			{
				return RequestRegister(HostID.Server, new RmiContext(), data);
			}
			return true;
		}

		public bool HasHandler(string rmiName)
		{
			if (!(rmiName == "RequestRegister"))
			{
				if (rmiName == "RequestFirstLogon")
				{
					return RequestFirstLogon != null;
				}
				return false;
			}
			return RequestRegister != null;
		}
	}
}

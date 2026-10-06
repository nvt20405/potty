using Nettention.Proud;

namespace EntryC2S
{
	public class Proxy : IJsonProxy
	{
		public bool RequestRegister(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestRegister", data);
			return true;
		}

		public bool RequestRegister(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestRegister(HostID.Server, rmiContext, data);
		}

		public bool RequestFirstLogon(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("RequestFirstLogon", data);
			return true;
		}

		public bool RequestFirstLogon(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return RequestFirstLogon(HostID.Server, rmiContext, data);
		}
	}
}

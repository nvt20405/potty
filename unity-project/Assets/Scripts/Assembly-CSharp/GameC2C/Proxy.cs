using Nettention.Proud;

namespace GameC2C
{
	public class Proxy : IJsonProxy
	{
		public bool RequestTestC2C(HostID remote, RmiContext rmiContext)
		{
			CJsonTransport.Send("RequestTestC2C", "");
			return true;
		}

		public bool RequestTestC2C(HostID[] remotes, RmiContext rmiContext)
		{
			return RequestTestC2C(HostID.Server, rmiContext);
		}
	}
}

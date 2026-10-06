using Nettention.Proud;

namespace GameC2C
{
	public class Stub : IJsonStub
	{
		public delegate bool RequestTestC2CDelegate(HostID remote, RmiContext rmiContext);

		public RequestTestC2CDelegate RequestTestC2C;

		public bool Dispatch(string rmiName, string data)
		{
			if (rmiName == "RequestTestC2C")
			{
				if (RequestTestC2C != null)
				{
					return RequestTestC2C(HostID.Server, new RmiContext());
				}
				return true;
			}
			return true;
		}

		public bool HasHandler(string rmiName)
		{
			if (rmiName == "RequestTestC2C")
			{
				return RequestTestC2C != null;
			}
			return false;
		}
	}
}

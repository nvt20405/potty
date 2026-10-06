using System.Net;

namespace Nettention.Proud
{
	public class NamedAddrPort
	{
		public string addr;

		public ushort port;

		public static readonly NamedAddrPort Unassigned = new NamedAddrPort("", ushort.MaxValue);

		public NamedAddrPort()
		{
			addr = "";
			port = ushort.MaxValue;
		}

		public NamedAddrPort(string addr, ushort port)
		{
			this.addr = addr;
			this.port = port;
		}

		public NamedAddrPort(IPEndPoint addrPort)
		{
			addr = addrPort.Address.ToString();
			port = (ushort)addrPort.Port;
		}

		public IPEndPoint ToAddrPort()
		{
			return new IPEndPoint(IPAddress.Parse(addr), port);
		}

		public override string ToString()
		{
			return string.Format("{0}:{1}", addr, port);
		}

		public bool IsUnicastEndpoint()
		{
			addr = addr.Trim();
			if (port != 0 && port != ushort.MaxValue && !(addr == "") && !(addr == "0.0.0.0"))
			{
				return !(addr == "255.255.255.255");
			}
			return false;
		}
	}
}

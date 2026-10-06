using System.Net;
using System.Net.Sockets;

namespace Nettention.Proud
{
	public class ConnectErrorInfo : ErrorInfo
	{
		public IPEndPoint remoteAddr;

		public SocketError socketError;

		public override string ToString()
		{
			return base.ToString() + string.Format(",remoteAddr:{0},socketError:{1}", remoteAddr.ToString(), socketError);
		}
	}
}

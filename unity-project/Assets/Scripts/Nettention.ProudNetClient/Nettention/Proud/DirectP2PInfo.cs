using System.Net;

namespace Nettention.Proud
{
	public class DirectP2PInfo
	{
		public IPEndPoint localUdpSocketAddr = NetUtil.MakeUnassignedIPEndPoint;

		public IPEndPoint localToRemoteAddr = NetUtil.MakeUnassignedIPEndPoint;

		public IPEndPoint remoteToLocalAddr = NetUtil.MakeUnassignedIPEndPoint;

		public bool HasBeenHolepunched
		{
			get
			{
				if (NetUtil.IsUnicastEndpoint(localUdpSocketAddr) && NetUtil.IsUnicastEndpoint(localToRemoteAddr))
				{
					return NetUtil.IsUnicastEndpoint(remoteToLocalAddr);
				}
				return false;
			}
		}
	}
}

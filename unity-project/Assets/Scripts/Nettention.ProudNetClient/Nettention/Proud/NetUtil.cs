using System;
using System.Net;
using System.Net.Sockets;

namespace Nettention.Proud
{
	internal class NetUtil
	{
		public static IPEndPoint UnassignedIPEndPoint = MakeUnassignedIPEndPoint;

		public static IPEndPoint MakeUnassignedIPEndPoint
		{
			get
			{
				return new IPEndPoint(-1L, 65535);
			}
		}

		public static bool IsUnicastEndpoint(IPEndPoint addrPort)
		{
			if (addrPort.Port == 0 || (ushort)addrPort.Port == ushort.MaxValue)
			{
				return false;
			}
			uint num = BitConverter.ToUInt32(addrPort.Address.GetAddressBytes(), 0);
			if (num != 0)
			{
				return num != uint.MaxValue;
			}
			return false;
		}

		public static bool IsSameHost(IPEndPoint a, IPEndPoint b)
		{
			return a.Address.Equals(b.Address);
		}

		public static bool IsSameLan(IPEndPoint a, IPEndPoint b)
		{
			byte[] addressBytes = a.Address.GetAddressBytes();
			byte[] addressBytes2 = b.Address.GetAddressBytes();
			if (addressBytes[0] == addressBytes2[0] && addressBytes[1] == addressBytes2[1])
			{
				return addressBytes[2] == addressBytes2[2];
			}
			return false;
		}

		private static void SetSocketSendAndRecvBufferLength(Socket socket, int sendBufferLength, int recvBufferLength)
		{
			socket.SendBufferSize = sendBufferLength;
			socket.ReceiveBufferSize = recvBufferLength;
		}

		public static void SetTcpDefaultBehavior_Client(Socket socket)
		{
			SetSocketSendAndRecvBufferLength(socket, NetConfig.TcpSendBufferLength, NetConfig.TcpRecvBufferLength);
			if (NetConfig.EnableSocketTcpKeepAliveOption)
			{
				socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
			}
		}

		public static void SetUdpDefaultBehavior_Client(bool weborpc, Socket socket)
		{
			if (!weborpc)
			{
				socket.Blocking = false;
			}
			SetSocketSendAndRecvBufferLength(socket, NetConfig.UdpSendBufferLength_Client, NetConfig.UdpRecvBufferLength_Client);
		}
	}
}

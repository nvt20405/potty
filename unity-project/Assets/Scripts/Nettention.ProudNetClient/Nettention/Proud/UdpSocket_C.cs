using System;
using System.Net;
using System.Net.Sockets;

namespace Nettention.Proud
{
	internal class UdpSocket_C
	{
		public Socket socket;

		public bool sendIssued;

		public bool recvIssued;

		internal byte[] recvBuffer = new byte[NetConfig.UdpIssueRecvLength];

		private bool socketClosedOrClosing_CS_PROTECTED;

		private IPEndPoint recvFrom = NetUtil.MakeUnassignedIPEndPoint;

		private short ttlToRestoreOnSendCompletion = -1;

		private bool ttlMustBeRestoreOnSendCompletion;

		public UdpPacketFragBoard udpPacketFragBoard = new UdpPacketFragBoard();

		public UdpPacketFragBoardOutput m_sendIssuedFragment = new UdpPacketFragBoardOutput();

		public bool garbaged;

		public IPEndPoint localAddr = new IPEndPoint(0L, 0);

		public IPEndPoint addrOfHereAtServer = new IPEndPoint(0L, 0);

		public long recycleTime;

		public int restoredCount;

		public bool justRestored;

		public long lastUdpRecvIssuedTime;

		internal UdpPacketDefragBoard udpPacketDefragBoard = new UdpPacketDefragBoard();

		public IPEndPoint sendtoAddr;

		private NetClient m_ownerMain;

		public UdpSocket_C(NetClient ownerMain, long currentTime, UdpPacketFragBoard.RequestReceiveSpeedAtReceiverSide_NoRelayDelegate requestReceiveSpeedDelegate)
		{
			m_ownerMain = ownerMain;
			lastUdpRecvIssuedTime = currentTime;
			udpPacketFragBoard.RequestReceiveSpeedAtReceiverSide_NoRelay = requestReceiveSpeedDelegate;
		}

		public void DoForLongInterval(long currTime, int overSendSupectionThresholdInBytes)
		{
			udpPacketFragBoard.DoForLongInterval(currTime, overSendSupectionThresholdInBytes);
			udpPacketDefragBoard.DoForLongInterval(currTime);
		}

		public bool CreateSocket(bool weborpc, IPEndPoint udpLocalAddr)
		{
			try
			{
				if (udpLocalAddr.AddressFamily == AddressFamily.InterNetwork)
				{
					socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				}
				else if (udpLocalAddr.AddressFamily == AddressFamily.InterNetworkV6)
				{
					socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
				}
				socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, false);
				socket.Bind(udpLocalAddr);
			}
			catch (SocketException)
			{
				if (socket != null)
				{
					socket.Close();
					socket = null;
				}
				return false;
			}
			if (!RefreshLocalAddr())
			{
				socket.Close();
				socket = null;
				return false;
			}
			NetUtil.SetUdpDefaultBehavior_Client(weborpc, socket);
			return true;
		}

		private bool RefreshLocalAddr()
		{
			IPEndPoint iPEndPoint = (IPEndPoint)socket.LocalEndPoint;
			uint num = BitConverter.ToUInt32(iPEndPoint.Address.GetAddressBytes(), 0);
			if (num == 0 || num == uint.MaxValue)
			{
				return false;
			}
			localAddr = iPEndPoint;
			return true;
		}

		public void IssueSendOnNeed_IfPossible(long currentTime, AsyncCallback sendCallback)
		{
			if (recycleTime != 0 || sendIssued || IsSocketClosed() || !udpPacketFragBoard.PopAnySendQueueFilledOneWithCoalesce(m_sendIssuedFragment, currentTime) || m_sendIssuedFragment.sendFragFrag.Count <= 0)
			{
				return;
			}
			sendIssued = true;
			SocketError socketError = SocketError.Success;
			if (m_sendIssuedFragment.ttl >= 0)
			{
				ttlToRestoreOnSendCompletion = socket.Ttl;
				ttlMustBeRestoreOnSendCompletion = true;
				socket.Ttl = m_sendIssuedFragment.ttl;
			}
			sendtoAddr = new IPEndPoint(m_sendIssuedFragment.sendTo.Address, m_sendIssuedFragment.sendTo.Port);
			try
			{
				socket.BeginSendTo(m_sendIssuedFragment.sendFragFrag.data, 0, m_sendIssuedFragment.sendFragFrag.Count, SocketFlags.None, m_sendIssuedFragment.sendTo, sendCallback, this);
			}
			catch (SocketException ex)
			{
				if (ex.SocketErrorCode != SocketError.IOPending)
				{
					socketError = ex.SocketErrorCode;
				}
			}
			if (socketError != SocketError.Success)
			{
				sendIssued = false;
			}
		}

		public void IssueRecvFrom_UnlessSocketClosed(long currentTime, AsyncCallback recvCallback)
		{
			if (recycleTime != 0 || recvIssued || IsSocketClosed())
			{
				return;
			}
			recvIssued = true;
			EndPoint remote_end = recvFrom;
			bool flag;
			do
			{
				flag = true;
				try
				{
					socket.BeginReceiveFrom(recvBuffer, 0, recvBuffer.Length, SocketFlags.None, ref remote_end, recvCallback, this);
				}
				catch (SocketException ex)
				{
					switch (ex.SocketErrorCode)
					{
					default:
						recvIssued = false;
						return;
					case SocketError.MessageSize:
					case SocketError.NetworkReset:
					case SocketError.ConnectionReset:
						flag = false;
						break;
					case SocketError.IOPending:
						flag = true;
						break;
					}
				}
			}
			while (!flag);
			lastUdpRecvIssuedTime = currentTime;
		}

		public void NonBlockSendUntilWouldBlock(long currentTime, ref NetClientStats netClientStats)
		{
			while (recycleTime == 0 && !IsSocketClosed() && (m_sendIssuedFragment.sendFragFrag.Count != 0 || (udpPacketFragBoard.PopAnySendQueueFilledOneWithCoalesce(m_sendIssuedFragment, currentTime) && m_sendIssuedFragment.sendFragFrag.Count > 0)))
			{
				if (m_sendIssuedFragment.ttl >= 0)
				{
					ttlToRestoreOnSendCompletion = socket.Ttl;
					ttlMustBeRestoreOnSendCompletion = true;
					socket.Ttl = m_sendIssuedFragment.ttl;
				}
				sendtoAddr = new IPEndPoint(m_sendIssuedFragment.sendTo.Address, m_sendIssuedFragment.sendTo.Port);
				while (true)
				{
					try
					{
						int num = socket.SendTo(m_sendIssuedFragment.sendFragFrag.data, m_sendIssuedFragment.sendFragFrag.Count, SocketFlags.None, sendtoAddr);
						if (ttlMustBeRestoreOnSendCompletion)
						{
							socket.Ttl = ttlToRestoreOnSendCompletion;
							ttlMustBeRestoreOnSendCompletion = false;
						}
						if (num > 0)
						{
							m_sendIssuedFragment.sendFragFrag.SetCount(0);
							netClientStats.totalUdpSendCount++;
							netClientStats.totalUdpSendBytes += (ulong)num;
							break;
						}
						return;
					}
					catch (SocketException ex)
					{
						if (ex.SocketErrorCode == SocketError.Interrupted)
						{
							continue;
						}
						if (ttlMustBeRestoreOnSendCompletion)
						{
							socket.Ttl = ttlToRestoreOnSendCompletion;
							ttlMustBeRestoreOnSendCompletion = false;
						}
						return;
					}
				}
			}
		}

		public void AddToSendQueueWithSplitterAndSignal_Copy(HostID finalDestHostID, byte filterTag, IPEndPoint sendTo, SendFragRefs sendData, long addedTime, SendOpt sendOpt)
		{
			SendFragRefs sendFragRefs = new SendFragRefs();
			Message header = new Message();
			TcpLayer_C.AddSplitterButShareBuffer(sendData, sendFragRefs, header);
			udpPacketFragBoard.AddNewPacket(finalDestHostID, filterTag, sendTo, sendFragRefs, addedTime, sendOpt);
		}

		public void AddToSendQueueWithSplitterAndSignal_Copy(HostID finalDestHostID, byte filterTag, IPEndPoint sendTo, Message msg, long addedTime, SendOpt sendOpt)
		{
			AddToSendQueueWithSplitterAndSignal_Copy(finalDestHostID, filterTag, sendTo, new SendFragRefs(msg), addedTime, sendOpt);
		}

		public uint GetUdpSendBufferPacketFilledCount(IPEndPoint destAddr)
		{
			return (uint)udpPacketFragBoard.GetTotalPacketCountOfAddr(destAddr);
		}

		internal void RestoreTtlOnCompletion()
		{
			if (ttlMustBeRestoreOnSendCompletion)
			{
				ttlMustBeRestoreOnSendCompletion = false;
				socket.Ttl = ttlToRestoreOnSendCompletion;
			}
		}

		public void ResetPacketFragState()
		{
			udpPacketFragBoard.Clear();
			udpPacketDefragBoard.Clear();
		}

		public void CloseSocketOnly()
		{
			lock (m_ownerMain.m_critSec)
			{
				if (!socketClosedOrClosing_CS_PROTECTED)
				{
					socketClosedOrClosing_CS_PROTECTED = true;
					socket.Close();
					socket = null;
				}
			}
		}

		public void OnCloseSocketAndMakeOrphant()
		{
			CloseSocketOnly();
		}

		public bool IsSocketClosed()
		{
			if (socket != null)
			{
				return socketClosedOrClosing_CS_PROTECTED;
			}
			return true;
		}

		internal bool RestoreSocket(NetClient main)
		{
			if (!IsSocketClosed())
			{
				Sysutil.ThrowInvalidArgumentException();
			}
			lock (m_ownerMain.m_critSec)
			{
				socket = null;
				if (!main.isIpV6Network)
				{
					socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				}
				else
				{
					socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
				}
				socketClosedOrClosing_CS_PROTECTED = false;
				restoredCount++;
			}
			IPEndPoint iPEndPoint = new IPEndPoint(main.ToServerTcp.localAddr.Address, 0);
			uint num = BitConverter.ToUInt32(iPEndPoint.Address.GetAddressBytes(), 0);
			if (num == 0 || num == uint.MaxValue)
			{
				Sysutil.ShowUserMisuseError("UDP 소켓을 생성하기 전에 TCP 연결이 이미 되어있는 상태이어야 하는데!");
			}
			socket.Bind(iPEndPoint);
			if (!RefreshLocalAddr())
			{
				return false;
			}
			NetUtil.SetUdpDefaultBehavior_Client(main.IsProactorAsyncModel, socket);
			return true;
		}
	}
}

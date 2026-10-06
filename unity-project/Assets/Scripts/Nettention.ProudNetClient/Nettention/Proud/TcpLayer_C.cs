using System;
using System.Net;
using System.Net.Sockets;

namespace Nettention.Proud
{
	internal class TcpLayer_C
	{
		internal static readonly short Splitter = 22291;

		private NetClient m_owner;

		public bool sendIssued;

		public bool recvIssued;

		public volatile bool socketClosedOrClosing_CS_PROTECTED;

		public IPEndPoint localAddrAtServer;

		public IPEndPoint localAddr;

		internal Socket socket;

		internal byte[] recvBuffer = new byte[NetConfig.TcpIssueRecvLength];

		public StreamQueue recvStream = new StreamQueue(NetConfig.StreamGrowBy);

		public TcpSendQueue sendQueue = new TcpSendQueue();

		public long lastRecvInvokeWarningTime;

		public long lastSendInvokeWarningTime;

		public bool IsSocketClosed
		{
			get
			{
				return socketClosedOrClosing_CS_PROTECTED;
			}
		}

		public bool EnableNagleAlgorithm
		{
			set
			{
				socket.NoDelay = !value;
			}
		}

		internal static void AddSplitterButShareBuffer(SendFragRefs payload, SendFragRefs ret, Message header)
		{
			header.Write(Splitter);
			header.WriteScalar(payload.TotalLength);
			ret.Add(header);
			ret.Add(payload);
		}

		internal static int ExtractMessagesFromStreamAndRemoveFlushedStream(StreamQueue recvStream, ref ReceivedMessageList extractedMessageAddTarget, HostID senderHostID, int messageMaxLength, out ErrorType outError)
		{
			TcpLayerMessageExtractor tcpLayerMessageExtractor = new TcpLayerMessageExtractor();
			tcpLayerMessageExtractor.recvStream = recvStream.block;
			tcpLayerMessageExtractor.recvStreamCount = recvStream.Length + recvStream.headIndex;
			tcpLayerMessageExtractor.extractedMessageAddTarget = extractedMessageAddTarget;
			tcpLayerMessageExtractor.senderHostID = senderHostID;
			tcpLayerMessageExtractor.messageMaxLength = messageMaxLength;
			int result = tcpLayerMessageExtractor.Extract(recvStream.headIndex, out outError);
			recvStream.PopFront(tcpLayerMessageExtractor.outLastSuccessOffset);
			return result;
		}

		public TcpLayer_C(NetClient owner)
		{
			m_owner = owner;
			if (!owner.isIpV6Network)
			{
				socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			}
			else
			{
				socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
			}
			NetUtil.SetTcpDefaultBehavior_Client(socket);
		}

		public void CloseSocket()
		{
			lock (m_owner.m_critSec)
			{
				if (!socketClosedOrClosing_CS_PROTECTED)
				{
					socketClosedOrClosing_CS_PROTECTED = true;
					socket.Close();
				}
			}
		}

		public void AddToSendQueueWithSplitterAndSignal_Copy(SendFragRefs sendData, SendOpt sendOpt)
		{
			SendFragRefs sendFragRefs = new SendFragRefs();
			Message header = new Message();
			AddSplitterButShareBuffer(sendData, sendFragRefs, header);
			sendQueue.PushBack_Copy(sendFragRefs, sendOpt);
		}

		public void IssueSendOnNeed(long currentTime, AsyncCallback sendCallback)
		{
			if (IsSocketClosed || sendIssued)
			{
				return;
			}
			int brakedSendAmount = GetBrakedSendAmount(currentTime);
			if (brakedSendAmount <= 0)
			{
				return;
			}
			sendIssued = true;
			ByteArray output = new ByteArray();
			sendQueue.FillSendBuf(ref output, brakedSendAmount);
			SocketError socketError = SocketError.Success;
			try
			{
				socket.BeginSend(output.data, 0, output.Count, SocketFlags.None, sendCallback, this);
			}
			catch (SocketException ex)
			{
				if (ex.SocketErrorCode != SocketError.IOPending)
				{
					socketError = ex.SocketErrorCode;
				}
			}
			sendQueue.sendBrake.Accumulate(brakedSendAmount, currentTime);
			sendQueue.sendSpeed.Accumulate(brakedSendAmount, currentTime);
			if (socketError != SocketError.Success)
			{
				sendIssued = false;
				CloseSocket();
			}
			else
			{
				lastSendInvokeWarningTime = 0L;
			}
		}

		public SocketError IssueRecvAndCheck(AsyncCallback recvCallback)
		{
			if (!recvIssued && !IsSocketClosed)
			{
				recvIssued = true;
				try
				{
					socket.BeginReceive(recvBuffer, 0, recvBuffer.Length, SocketFlags.None, recvCallback, this);
				}
				catch (SocketException ex)
				{
					if (ex.SocketErrorCode != SocketError.IOPending)
					{
						recvIssued = false;
						CloseSocket();
						return ex.SocketErrorCode;
					}
				}
				lastRecvInvokeWarningTime = 0L;
				return SocketError.Success;
			}
			return SocketError.Success;
		}

		public void NonBlockSendUntilWouldBlock(long currentTime, ref NetClientStats netClientStats)
		{
			while (!IsSocketClosed)
			{
				int brakedSendAmount = GetBrakedSendAmount(currentTime);
				if (brakedSendAmount <= 0)
				{
					break;
				}
				int num = 0;
				ByteArray output = new ByteArray();
				sendQueue.FillSendBuf(ref output, brakedSendAmount);
				while (true)
				{
					try
					{
						num = socket.Send(output.data, output.Count, SocketFlags.None);
					}
					catch (SocketException ex)
					{
						if (ex.SocketErrorCode == SocketError.Interrupted)
						{
							continue;
						}
						if (ex.SocketErrorCode != SocketError.IOPending && ex.SocketErrorCode != SocketError.WouldBlock && ex.SocketErrorCode != SocketError.TryAgain && ex.SocketErrorCode != SocketError.NotConnected && ex.SocketErrorCode != SocketError.NoBufferSpaceAvailable)
						{
							CloseSocket();
						}
						return;
					}
					catch (ObjectDisposedException)
					{
						CloseSocket();
						return;
					}
					break;
				}
				if (num > 0)
				{
					sendQueue.sendBrake.Accumulate(num, currentTime);
					sendQueue.sendSpeed.Accumulate(num, currentTime);
					sendQueue.PopFront(num);
					lastSendInvokeWarningTime = 0L;
					netClientStats.totalTcpSendBytes += (ulong)num;
					continue;
				}
				break;
			}
		}

		public void DoForLongInterval(long currTime)
		{
			sendQueue.DoForLongInterval(currTime);
		}

		public int GetBrakedSendAmount(long currTime)
		{
			int length = sendQueue.Length;
			return Math.Min(sendQueue.sendBrake.GetMaxSendAmount(currTime, sendQueue.allowedMaxSendSpeed.Value), length);
		}

		public void RefreshLocalAddr()
		{
			localAddr = (IPEndPoint)socket.LocalEndPoint;
		}
	}
}

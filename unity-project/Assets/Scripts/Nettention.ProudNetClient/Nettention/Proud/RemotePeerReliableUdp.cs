using System;

namespace Nettention.Proud
{
	internal class RemotePeerReliableUdp
	{
		internal ReliableUdpHost host;

		internal RemotePeer owner;

		internal bool failed;

		public FrameNumber NextFrameNumberForAnotherReliablySendingFrame
		{
			get
			{
				return host.YieldFrameNumberForSendingLongFrameReliably();
			}
		}

		internal HostID TEST_HostID
		{
			get
			{
				return owner.peerHostID;
			}
		}

		internal FrameNumber RecvExpectFrameNumber
		{
			get
			{
				return host.RecvExpectFrameNumber;
			}
		}

		public RemotePeerReliableUdp(RemotePeer owner)
		{
			this.owner = owner;
		}

		public void ResetEngine(FrameNumber frameNumber)
		{
			host = new ReliableUdpHost(new ReliableUdpHostDelegates
			{
				getRecentPingMs = GetRecentUnreliablePingMs,
				getUdpSendBufferPacketFilledCount = GetUdpSendBufferPacketFilledCount,
				isReliableChannel = IsReliableChannel,
				sendOneFrameToUdpLayer = SendOneFrameToUdpLayer
			}, frameNumber);
		}

		public void SendWithSplitter_Copy(SendFragRefs sendData)
		{
			SendFragRefs sendFragRefs = new SendFragRefs();
			Message header = new Message();
			TcpLayer_C.AddSplitterButShareBuffer(sendData, sendFragRefs, header);
			ByteArray byteArray = new ByteArray();
			sendFragRefs.CopyTo(byteArray);
			host.Send(byteArray.data, byteArray.Count);
		}

		public bool EnqueReceivedFrameAndGetFlushedMessages(ReliableUdpFrame frame, ref ReceivedMessageList ret, out ErrorType outError)
		{
			ret.Clear();
			host.TakeReceivedFrame(frame);
			int num = TcpLayer_C.ExtractMessagesFromStreamAndRemoveFlushedStream(host.ReceivedStream, ref ret, owner.peerHostID, owner.owner.settings.clientMessageMaxLength, out outError);
			if (num < 0)
			{
				failed = true;
			}
			return true;
		}

		public bool EnqueReceivedFrameAndGetFlushedMessages(Message msg, ref ReceivedMessageList ret, out ErrorType outError)
		{
			ReliableUdpFrame reliableUdpFrame = new ReliableUdpFrame();
			outError = ErrorType.Ok;
			if (!msg.Read(out reliableUdpFrame.type))
			{
				return false;
			}
			switch (reliableUdpFrame.type)
			{
			case ReliableUdpFrameType.Data:
				if (!msg.Read(out reliableUdpFrame.frameNumber))
				{
					return false;
				}
				reliableUdpFrame.data = new ByteArray();
				if (!msg.Read(out reliableUdpFrame.data))
				{
					return false;
				}
				break;
			case ReliableUdpFrameType.Ack:
				if (!msg.Read(out reliableUdpFrame.frameNumber))
				{
					return false;
				}
				if (!msg.Read(out reliableUdpFrame.recentReceiveSpeed))
				{
					return false;
				}
				break;
			}
			return EnqueReceivedFrameAndGetFlushedMessages(reliableUdpFrame, ref ret, out outError);
		}

		public void Heartbeat()
		{
			host.FrameMove();
		}

		public void SendOneFrameToUdpLayer(ReliableUdpFrame frame)
		{
			MessagePriority priority = RmiContext.UnreliableSendForPN.priority;
			switch (frame.type)
			{
			case ReliableUdpFrameType.Data:
				if (ReliableUdpConfig.HighPriorityDataFrame)
				{
					priority = MessagePriority.Ring1;
				}
				break;
			case ReliableUdpFrameType.Ack:
				if (ReliableUdpConfig.HighPriorityAckFrame)
				{
					priority = MessagePriority.Ring1;
				}
				break;
			}
			if (!owner.RelayedP2P)
			{
				SendFragRefs ret = new SendFragRefs();
				Message header = new Message();
				RemotePeerReliableUdpHelper.BuildSendDataFromFrame(frame, ref ret, header);
				owner.ToPeerUdp.SendWithSplitter_Copy(ret, new SendOpt(priority, true));
				return;
			}
			switch (frame.type)
			{
			case ReliableUdpFrameType.Data:
			{
				Message message = new Message();
				message.Write(MessageType.LingerDataFrame1);
				message.Write(owner.peerHostID);
				message.Write(frame.frameNumber);
				bool enableTestSplitter = message.EnableTestSplitter;
				message.EnableTestSplitter = false;
				message.WriteScalar(frame.data.Count);
				SendFragRefs sendFragRefs = new SendFragRefs();
				sendFragRefs.Add(message);
				sendFragRefs.Add(frame.data.data, frame.data.Count);
				owner.owner.Send_ToServer_Directly_Copy(owner.peerHostID, MessageReliability.Reliable, sendFragRefs, new SendOpt(RmiContext.UnreliableSendForPN));
				message.EnableTestSplitter = enableTestSplitter;
				break;
			}
			case ReliableUdpFrameType.Ack:
				break;
			}
		}

		internal uint GetUdpSendBufferPacketFilledCount()
		{
			if (!owner.RelayedP2P)
			{
				return owner.ToPeerUdp.UdpSendBufferPacketFilledCount;
			}
			return 1u;
		}

		internal bool IsReliableChannel()
		{
			return owner.RelayedP2P;
		}

		internal int GetRecentUnreliablePingMs()
		{
			return Math.Max(owner.recentPingMs, 0);
		}
	}
}

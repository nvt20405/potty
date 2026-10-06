using System.Collections.Generic;

namespace Nettention.Proud
{
	internal class ReliableUDPReceiver
	{
		internal class AcksToSend : List<FrameNumber>
		{
		}

		internal class ReceiverWindow : List<ReceiverFrame>
		{
		}

		private ReliableUdpHost owner;

		internal long lastSendGatheredAcksTimeMs;

		internal FrameNumber expectedFrameNumber;

		internal FrameNumber lastReceivedDataFrameNumber;

		internal AcksToSend acksToSend = new AcksToSend();

		internal ReceiverWindow receiverWindow = new ReceiverWindow();

		internal StreamQueue receivedStream = new StreamQueue(NetConfig.StreamGrowBy);

		internal int totalReceivedStreamLength;

		internal int totalAckFrameCount;

		internal int recentReceiveFrameCount;

		internal long recentReceiveFrameCountStartTimeMs;

		internal int recentReceiveSpeed = ReliableUdpConfig.ReceiveSpeedBeforeUpdate;

		internal int totalReceivedDataFrameCount;

		public ReliableUDPReceiver(ReliableUdpHost owner, FrameNumber firstFrameNumber)
		{
			this.owner = owner;
			expectedFrameNumber = firstFrameNumber;
			lastReceivedDataFrameNumber = firstFrameNumber;
		}

		public void FrameMove()
		{
			long timeMs = PreciseCurrentTime.GetTimeMs();
			if (recentReceiveFrameCountStartTimeMs == 0)
			{
				recentReceiveFrameCountStartTimeMs = timeMs;
			}
			if (timeMs - recentReceiveFrameCountStartTimeMs > ReliableUdpConfig.CalcRecentReceiveInterval)
			{
				recentReceiveSpeed = (int)Sysutil.Lerp(recentReceiveSpeed, recentReceiveFrameCount, 0.1 / (double)(timeMs - recentReceiveFrameCountStartTimeMs));
				recentReceiveFrameCount = 0;
				recentReceiveFrameCountStartTimeMs = timeMs;
			}
			if (timeMs - lastSendGatheredAcksTimeMs > ReliableUdpConfig.StreamToSenderWindowCoalesceInterval / 5)
			{
				SendGatheredAcks();
				lastSendGatheredAcksTimeMs = timeMs;
			}
		}

		public void SendGatheredAcks()
		{
			while (acksToSend.Count > 0)
			{
				ReliableUdpFrame reliableUdpFrame = new ReliableUdpFrame();
				reliableUdpFrame.type = ReliableUdpFrameType.Ack;
				reliableUdpFrame.frameNumber = (FrameNumber)0;
				int num = 0;
				acksToSend.Sort();
				for (int i = 0; i < acksToSend.Count; i++)
				{
					FrameNumber n = acksToSend[i];
					reliableUdpFrame.ackedFrameNumbers.AddSortedNumber(n);
					num++;
					if (reliableUdpFrame.ackedFrameNumbers.Count >= ReliableUdpConfig.MaxAckCountInOneFrame)
					{
						break;
					}
				}
				reliableUdpFrame.recentReceiveSpeed = recentReceiveSpeed;
				reliableUdpFrame.expectedFrameNumber = expectedFrameNumber;
				owner.sender_INTERNAL.SendOneFrame(reliableUdpFrame);
				acksToSend.RemoveRange(0, num);
			}
		}

		public void ProcessReceivedFrame(ReliableUdpFrame frame)
		{
			switch (frame.type)
			{
			case ReliableUdpFrameType.Data:
				ProcessDataFrame(frame);
				break;
			case ReliableUdpFrameType.Ack:
				ProcessAckFrame(frame);
				break;
			}
		}

		public void ProcessDataFrame(ReliableUdpFrame frame)
		{
			totalReceivedDataFrameCount++;
			if (!owner.dg_INTERNAL.isReliableChannel() && FrameNumberUtil.Compare(frame.frameNumber, expectedFrameNumber) >= 0)
			{
				acksToSend.Add(frame.frameNumber);
				totalAckFrameCount++;
			}
			lastReceivedDataFrameNumber = frame.frameNumber;
			if (!IsTooOldFrame(frame.frameNumber))
			{
				AddToReceivedFrames(frame);
				FlushAckAccumulatedToOutput();
			}
		}

		public void FlushAckAccumulatedToOutput()
		{
			while (receiverWindow.Count > 0)
			{
				ReceiverFrame receiverFrame = receiverWindow[0];
				if (receiverFrame.frameNumber != expectedFrameNumber)
				{
					break;
				}
				receivedStream.PushBack_Copy(receiverFrame.data.data, receiverFrame.data.Count);
				totalReceivedStreamLength += receiverFrame.data.Count;
				receiverWindow.RemoveAt(0);
				expectedFrameNumber = FrameNumberUtil.NextFrameNumber(expectedFrameNumber);
			}
		}

		public void AddToReceivedFrames(ReliableUdpFrame receivedFrame)
		{
			for (int i = 0; i < receiverWindow.Count; i++)
			{
				ReceiverFrame receiverFrame = receiverWindow[i];
				if (receiverFrame.frameNumber == receivedFrame.frameNumber)
				{
					return;
				}
				if (FrameNumberUtil.Compare(receivedFrame.frameNumber, receiverFrame.frameNumber) < 0)
				{
					receiverWindow.Insert(i, new ReceiverFrame(receivedFrame));
					recentReceiveFrameCount++;
					return;
				}
			}
			receiverWindow.Add(new ReceiverFrame(receivedFrame));
			recentReceiveFrameCount++;
		}

		public void ProcessAckFrame(ReliableUdpFrame frame)
		{
			UncompressedFrameNumberArray dest = new UncompressedFrameNumberArray();
			frame.ackedFrameNumbers.Uncompress(ref dest);
			if (dest.Count > 0)
			{
				owner.sender_INTERNAL.lastReceivedAckFrameNumber = dest[dest.Count - 1];
				owner.sender_INTERNAL.lastReceivedAckTimeMs = PreciseCurrentTime.GetTimeMs();
				owner.sender_INTERNAL.totalAckFrameReceivedCount++;
			}
			owner.sender_INTERNAL.RemoveSpecifiedAndItsPastsFromSenderWindow(frame.expectedFrameNumber);
			owner.sender_INTERNAL.lastExpectedFrameNumberAtSender = frame.expectedFrameNumber;
			for (int i = 0; i < dest.Count; i++)
			{
				FrameNumber frameID = dest[i];
				owner.sender_INTERNAL.RemoveFromSenderWindow(frameID);
			}
			owner.sender_INTERNAL.remoteReceiveSpeed = (int)Sysutil.LerpInt(owner.sender_INTERNAL.remoteReceiveSpeed, frame.recentReceiveSpeed, 9L, 10L) + 1000;
		}

		public bool IsTooOldFrame(FrameNumber frameID)
		{
			return FrameNumberUtil.Compare(frameID, expectedFrameNumber) < 0;
		}
	}
}

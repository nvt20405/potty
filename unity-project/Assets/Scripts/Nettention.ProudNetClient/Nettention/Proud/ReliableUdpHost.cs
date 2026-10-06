namespace Nettention.Proud
{
	internal class ReliableUdpHost
	{
		internal ReliableUdpHostDelegates dg_INTERNAL;

		internal ReliableUDPSender sender_INTERNAL;

		internal ReliableUDPReceiver receiver_INTERNAL;

		public StreamQueue ReceivedStream
		{
			get
			{
				return receiver_INTERNAL.receivedStream;
			}
		}

		public FrameNumber RecvExpectFrameNumber
		{
			get
			{
				return receiver_INTERNAL.expectedFrameNumber;
			}
		}

		public ReliableUdpHost(ReliableUdpHostDelegates dg, FrameNumber firstFrameNumber)
		{
			dg_INTERNAL = dg;
			sender_INTERNAL = new ReliableUDPSender(this, firstFrameNumber);
			receiver_INTERNAL = new ReliableUDPReceiver(this, firstFrameNumber);
		}

		public void TakeReceivedFrame(ReliableUdpFrame frame)
		{
			receiver_INTERNAL.ProcessReceivedFrame(frame);
		}

		public void Send(byte[] stream, int count)
		{
			sender_INTERNAL.SendViaReliableUdp(stream, count);
			sender_INTERNAL.StreamToSenderWindowOnNeed(false);
		}

		public void FrameMove()
		{
			receiver_INTERNAL.FrameMove();
			sender_INTERNAL.FrameMove();
		}

		public ReliableUdpHostStats GetStats()
		{
			ReliableUdpHostStats reliableUdpHostStats = new ReliableUdpHostStats();
			reliableUdpHostStats.receivedFrameCount = receiver_INTERNAL.receiverWindow.Count;
			reliableUdpHostStats.receivedStreamCount = receiver_INTERNAL.receivedStream.Length;
			reliableUdpHostStats.totalReceivedStreamLength = receiver_INTERNAL.totalReceivedStreamLength;
			reliableUdpHostStats.totalAckFrameCount = receiver_INTERNAL.totalAckFrameCount;
			reliableUdpHostStats.recentReceiveSpeed = receiver_INTERNAL.recentReceiveSpeed;
			reliableUdpHostStats.expectedFrameNumber = receiver_INTERNAL.expectedFrameNumber;
			reliableUdpHostStats.lastReceivedDataFrameNumber = receiver_INTERNAL.lastReceivedDataFrameNumber;
			reliableUdpHostStats.sendStreamCount = sender_INTERNAL.sendStream.Length;
			reliableUdpHostStats.firstSendFrameCount = sender_INTERNAL.firstSenderWindow.Count;
			reliableUdpHostStats.resendFrameCount = sender_INTERNAL.resendWindow.Count;
			reliableUdpHostStats.totalSendStreamLength = sender_INTERNAL.totalSendStreamLength;
			reliableUdpHostStats.totalResendCount = sender_INTERNAL.totalResendCount;
			reliableUdpHostStats.totalFirstSendCount = sender_INTERNAL.totalFirstSendCount;
			reliableUdpHostStats.recentSendFrameToUdpSpeed = sender_INTERNAL.recentSendFrameToUdpSpeed;
			reliableUdpHostStats.sendSpeedLimit = sender_INTERNAL.sendSpeedLimit;
			reliableUdpHostStats.totalReceiveDataCount = receiver_INTERNAL.totalReceivedDataFrameCount;
			if (sender_INTERNAL.firstSenderWindow.Count > 0)
			{
				reliableUdpHostStats.firstSenderWindowLastFrame = sender_INTERNAL.firstSenderWindow[0].frameNumber;
			}
			else
			{
				reliableUdpHostStats.firstSenderWindowLastFrame = (FrameNumber)0;
			}
			if (sender_INTERNAL.resendWindow.Count > 0)
			{
				reliableUdpHostStats.resendWindowLastFrame = sender_INTERNAL.resendWindow[0].frameNumber;
			}
			else
			{
				reliableUdpHostStats.resendWindowLastFrame = (FrameNumber)0;
			}
			reliableUdpHostStats.lastExpectedFrameNumberAtSender = sender_INTERNAL.lastExpectedFrameNumberAtSender;
			return reliableUdpHostStats;
		}

		public FrameNumber YieldFrameNumberForSendingLongFrameReliably()
		{
			return sender_INTERNAL.NextFrameNumber;
		}
	}
}

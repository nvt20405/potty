using System;
using System.Collections.Generic;

namespace Nettention.Proud
{
	internal class ReliableUDPSender
	{
		internal class SenderWindow : List<SenderFrame>
		{
		}

		private ReliableUdpHost owner;

		private FrameNumber currentFrameNumber;

		internal int remoteReceiveSpeed = ReliableUdpConfig.ReceiveSpeedBeforeUpdate;

		internal long lastDoStreamToSenderWindowTimeMs;

		internal int totalAckFrameReceivedCount;

		internal int sendSpeedLimit = ReliableUdpConfig.MaxSendSpeedInFrameCount;

		internal StreamQueue sendStream = new StreamQueue(NetConfig.StreamGrowBy);

		internal SenderWindow firstSenderWindow = new SenderWindow();

		internal SenderWindow resendWindow = new SenderWindow();

		internal int totalSendStreamLength;

		internal int totalResendCount;

		internal int totalFirstSendCount;

		internal int recentSendFrameToUdpCount = ReliableUdpConfig.ReceiveSpeedBeforeUpdate;

		internal long recentSendFrameToUdpStartTimeMs;

		internal int recentSendFrameToUdpSpeed = ReliableUdpConfig.ReceiveSpeedBeforeUpdate;

		internal long maxResendElapsedTimeMs;

		internal FrameNumber lastExpectedFrameNumberAtSender;

		internal FrameNumber lastReceivedAckFrameNumber;

		internal long lastReceivedAckTimeMs;

		public FrameNumber NextFrameNumber
		{
			get
			{
				FrameNumber result = currentFrameNumber;
				currentFrameNumber = FrameNumberUtil.NextFrameNumber(currentFrameNumber);
				return result;
			}
		}

		public ReliableUDPSender(ReliableUdpHost owner, FrameNumber firstFrameNumber)
		{
			this.owner = owner;
			currentFrameNumber = firstFrameNumber;
		}

		public void SendViaReliableUdp(byte[] streamToAdd, int count)
		{
			sendStream.PushBack_Copy(streamToAdd, count);
			totalSendStreamLength += count;
		}

		public void FrameMove()
		{
			CalcRecentSendSpeed();
			int num = recentSendFrameToUdpSpeed;
			int num2 = remoteReceiveSpeed;
			if (num * ReliableUdpConfig.BrakeMaxSendSpeedThreshold / 10 > num2)
			{
				sendSpeedLimit = num2 + 1;
			}
			else
			{
				sendSpeedLimit = ReliableUdpConfig.MaxSendSpeedInFrameCount;
			}
			StreamToSenderWindowOnNeed(false);
			ReSendWindowToUdpSenderOnNeed();
			FirstSenderWindowToUdpSenderOnNeed();
		}

		private void CalcRecentSendSpeed()
		{
			long timeMs = PreciseCurrentTime.GetTimeMs();
			if (recentSendFrameToUdpStartTimeMs == 0)
			{
				recentSendFrameToUdpStartTimeMs = timeMs;
			}
			if (timeMs - recentSendFrameToUdpStartTimeMs > ReliableUdpConfig.CalcRecentReceiveInterval)
			{
				recentSendFrameToUdpSpeed = (int)Sysutil.Lerp(recentSendFrameToUdpSpeed, recentSendFrameToUdpCount, 0.1 / (double)(timeMs - recentSendFrameToUdpStartTimeMs));
				recentSendFrameToUdpCount = 0;
				recentSendFrameToUdpStartTimeMs = timeMs;
			}
		}

		private void FirstSenderWindowToUdpSenderOnNeed()
		{
			long timeMs = PreciseCurrentTime.GetTimeMs();
			owner.dg_INTERNAL.getRecentPingMs();
			bool flag = owner.dg_INTERNAL.isReliableChannel();
			for (int num = firstSenderWindow.Count - 1; num >= 0; num--)
			{
				SenderFrame senderFrame = firstSenderWindow[num];
				senderFrame.lastSendTime = timeMs;
				senderFrame.firstSendTime = timeMs;
				senderFrame.resendCoolTime = ReliableUdpConfig.FirstResendCoolTime;
				senderFrame.resendCoolTime = Math.Max(senderFrame.resendCoolTime, ReliableUdpConfig.MinResendCoolTime);
				senderFrame.resendCoolTime = Math.Min(senderFrame.resendCoolTime, ReliableUdpConfig.MaxResendCoolTime);
				totalFirstSendCount++;
				SendOneFrame(senderFrame);
				if (!flag)
				{
					resendWindow.Insert(0, senderFrame);
				}
				firstSenderWindow.RemoveAt(num);
			}
		}

		private void ReSendWindowToUdpSenderOnNeed()
		{
			long timeMs = PreciseCurrentTime.GetTimeMs();
			float val = resendWindow.Count / ReliableUdpConfig.ResendLimitRatio;
			val = Math.Max(val, ReliableUdpConfig.MinResendLimitCount);
			val = Math.Min(val, ReliableUdpConfig.MaxResendLimitCount);
			int num = int.MaxValue;
			bool flag = owner.dg_INTERNAL.isReliableChannel();
			for (int num2 = resendWindow.Count - 1; num2 >= 0; num2--)
			{
				SenderFrame senderFrame = resendWindow[num2];
				if (timeMs - senderFrame.lastSendTime > senderFrame.resendCoolTime)
				{
					if (senderFrame.firstSendTime > 0)
					{
						maxResendElapsedTimeMs = Math.Max(maxResendElapsedTimeMs, timeMs - senderFrame.firstSendTime);
					}
					senderFrame.resendCoolTime = senderFrame.resendCoolTime * ReliableUdpConfig.EnlargeResendCoolTimeRatio / 10;
					senderFrame.resendCoolTime = Math.Min(senderFrame.resendCoolTime, ReliableUdpConfig.MaxResendCoolTime);
					senderFrame.lastSendTime = timeMs;
					senderFrame.resendCount++;
					totalResendCount++;
					SendOneFrame(senderFrame);
					if (flag)
					{
						resendWindow.RemoveAt(num2);
					}
					num--;
				}
			}
		}

		public void StreamToSenderWindowOnNeed(bool moveNow)
		{
			long timeMs = PreciseCurrentTime.GetTimeMs();
			if (!moveNow && (owner.dg_INTERNAL.getUdpSendBufferPacketFilledCount() == 0 || timeMs - lastDoStreamToSenderWindowTimeMs > ReliableUdpConfig.StreamToSenderWindowCoalesceInterval))
			{
				moveNow = true;
			}
			if (moveNow)
			{
				lastDoStreamToSenderWindowTimeMs = timeMs;
				while (sendStream.Length > 0)
				{
					SenderFrame senderFrame = new SenderFrame();
					senderFrame.frameNumber = NextFrameNumber;
					int num = Math.Min(ReliableUdpConfig.FrameLength, sendStream.Length);
					senderFrame.data = new ByteArray();
					senderFrame.data.Count = num;
					senderFrame.type = ReliableUdpFrameType.Data;
					sendStream.GetBlockedData(ref senderFrame.data.data, num);
					firstSenderWindow.Insert(0, senderFrame);
					sendStream.PopFront(num);
				}
			}
		}

		public bool RemoveFromSenderWindow(FrameNumber frameID)
		{
			for (int num = resendWindow.Count - 1; num >= 0; num--)
			{
				SenderFrame senderFrame = resendWindow[num];
				if (senderFrame.frameNumber == frameID)
				{
					resendWindow.RemoveAt(num);
					return true;
				}
			}
			return false;
		}

		public void RemoveSpecifiedAndItsPastsFromSenderWindow(FrameNumber frameID)
		{
			for (int num = resendWindow.Count - 1; num >= 0; num--)
			{
				SenderFrame senderFrame = resendWindow[num];
				if (FrameNumberUtil.Compare(senderFrame.frameNumber, frameID) < 0)
				{
					resendWindow.RemoveAt(num);
				}
			}
		}

		public void SendOneFrame(ReliableUdpFrame frame)
		{
			owner.dg_INTERNAL.sendOneFrameToUdpLayer(frame);
			if (frame.type == ReliableUdpFrameType.Data)
			{
				recentSendFrameToUdpCount++;
			}
		}
	}
}

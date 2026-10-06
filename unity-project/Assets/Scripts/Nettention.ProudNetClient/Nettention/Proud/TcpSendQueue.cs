using System.Collections.Generic;

namespace Nettention.Proud
{
	internal class TcpSendQueue
	{
		private Queue<TcpPacketCtx> queue = new Queue<TcpPacketCtx>();

		private TcpPacketCtx partialSentPacket;

		private int partialSentLength;

		private int totalLength;

		private RecentReceiveSpeedAtReceiverSide recentReceiveSpeedAtReceiverSide = new RecentReceiveSpeedAtReceiverSide();

		public SendBrake sendBrake = new SendBrake();

		public AllowedMaxSendSpeed allowedMaxSendSpeed = new AllowedMaxSendSpeed();

		public SendSpeedMeasurer sendSpeed = new SendSpeedMeasurer();

		public int Length
		{
			get
			{
				return totalLength;
			}
		}

		private void CheckConsist()
		{
		}

		public void PushBack_Copy(SendFragRefs sendData, SendOpt sendOpt)
		{
			TcpPacketCtx tcpPacketCtx = new TcpPacketCtx();
			tcpPacketCtx.FromSendOpt(sendOpt);
			sendData.ToAssembledByteArray(tcpPacketCtx.packet);
			queue.Enqueue(tcpPacketCtx);
			totalLength += tcpPacketCtx.packet.Count;
			CheckConsist();
		}

		public void FillSendBuf(ref ByteArray output, int length)
		{
			output.Count = 0;
			int num = 0;
			if (partialSentPacket != null)
			{
				int num2 = partialSentPacket.packet.Count - partialSentLength;
				for (int i = 0; i < num2; i++)
				{
					output.Add(partialSentPacket.packet.data[partialSentLength + i]);
				}
				num += num2;
			}
			foreach (TcpPacketCtx item in queue)
			{
				output.AddRange(item.packet.data, item.packet.Count);
				num += item.packet.Count;
				if (num < length)
				{
					break;
				}
			}
		}

		public void PopFront(int length)
		{
			if (length < 0)
			{
				Sysutil.ThrowInvalidArgumentException();
			}
			if (length == 0)
			{
				return;
			}
			if (partialSentPacket != null)
			{
				if (partialSentLength + length < partialSentPacket.packet.Count)
				{
					partialSentLength += length;
					totalLength -= length;
					return;
				}
				int num = partialSentPacket.packet.Count - partialSentLength;
				totalLength -= num;
				length -= num;
				partialSentPacket = null;
				partialSentLength = 0;
			}
			while (length > 0 && queue.Count > 0)
			{
				TcpPacketCtx tcpPacketCtx = queue.Dequeue();
				if (tcpPacketCtx.packet.Count <= length)
				{
					totalLength -= tcpPacketCtx.packet.Count;
					length -= tcpPacketCtx.packet.Count;
					continue;
				}
				partialSentPacket = tcpPacketCtx;
				partialSentLength = length;
				totalLength -= length;
				length = 0;
			}
			CheckConsist();
		}

		public void DoForLongInterval(long currTime)
		{
			sendBrake.DoForLongInterval(currTime);
			sendSpeed.DoForLongInterval(currTime);
			recentReceiveSpeedAtReceiverSide.DoForLongInterval(currTime);
			allowedMaxSendSpeed.DoForLongInterval(sendSpeed.RecentSpeed, recentReceiveSpeedAtReceiverSide.Value);
		}
	}
}

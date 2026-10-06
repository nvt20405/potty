using System;

namespace Nettention.Proud
{
	internal class RemotePeerReliableUdpHelper
	{
		public static FrameNumber GetRandomFrameNumber(Random random)
		{
			int num = random.Next(int.MaxValue);
			if (num == 0)
			{
				num++;
			}
			return (FrameNumber)num;
		}

		public static void BuildSendDataFromFrame(ReliableUdpFrame frame, ref SendFragRefs ret, Message header)
		{
			ret.Clear();
			header.Write(MessageType.ReliableUdp_Frame);
			header.Write(frame.type);
			switch (frame.type)
			{
			case ReliableUdpFrameType.Data:
				header.Write(frame.frameNumber);
				header.WriteScalar(frame.data.Count);
				ret.Add(header);
				ret.Add(frame.data.data, frame.data.Count);
				break;
			case ReliableUdpFrameType.Ack:
				header.Write(frame.expectedFrameNumber);
				header.Write(frame.recentReceiveSpeed);
				ret.Add(header);
				break;
			case ReliableUdpFrameType.None:
				break;
			}
		}
	}
}

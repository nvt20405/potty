namespace Nettention.Proud
{
	internal class TcpLayerMessageExtractor
	{
		public byte[] recvStream;

		public int recvStreamCount;

		public ReceivedMessageList extractedMessageAddTarget;

		public HostID senderHostID;

		public int messageMaxLength;

		public int outLastSuccessOffset;

		public int Extract(int readoffset, out ErrorType outError)
		{
			outError = ErrorType.Ok;
			if (recvStreamCount == 0)
			{
				return 0;
			}
			int num = 0;
			Message message = new Message();
			message.UseExternalBuffer(ref recvStream, recvStreamCount);
			message.Length = recvStreamCount;
			message.ReadOffset = readoffset;
			int num2 = 0;
			while (true)
			{
				short b = 0;
				ByteArray byteArray = new ByteArray();
				if (message.Read(out b))
				{
					if (b != TcpLayer_C.Splitter)
					{
						message.ReadOffset = 0;
						ByteArray data = message.Data;
						int num3 = num2;
						int length = message.Length;
						int num4 = num3;
						while (true)
						{
							if (num4 < length)
							{
								if (data[num4] == 0)
								{
									break;
								}
								num4++;
								continue;
							}
							outLastSuccessOffset = num2;
							return num;
						}
						num2 = num4 + 1;
						message.ReadOffset = num4 + 1;
						continue;
					}
					int a = 0;
					if (message.ReadScalar(ref a))
					{
						if (a >= 0)
						{
							if (a <= messageMaxLength)
							{
								if (message.CanRead(a))
								{
									byteArray.Count = a;
									if (!message.Read(out byteArray.data, a))
									{
										break;
									}
									ReceivedMessage receivedMessage = new ReceivedMessage();
									receivedMessage.remoteHostID = senderHostID;
									receivedMessage.unsafeMessage = new Message(byteArray);
									extractedMessageAddTarget.Add(receivedMessage);
									num++;
									num2 = message.ReadOffset - readoffset;
									continue;
								}
								outLastSuccessOffset = num2;
								return num;
							}
							outLastSuccessOffset = message.Length;
							outError = ErrorType.TooLargeMessageDetected;
							return -1;
						}
						outLastSuccessOffset = message.Length;
						outError = ErrorType.InvalidPacketFormat;
						return -1;
					}
					outLastSuccessOffset = num2;
					return num;
				}
				outLastSuccessOffset = num2;
				return num;
			}
			outLastSuccessOffset = num2;
			return num;
		}
	}
}

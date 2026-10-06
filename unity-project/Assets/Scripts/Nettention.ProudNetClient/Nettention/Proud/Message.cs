using System;
using System.Net;
using System.Text;

namespace Nettention.Proud
{
	public class Message
	{
		private enum MessageSplitter
		{
			Splitter = 254
		}

		private static readonly int STRING_LENGTH_LIMIT = 1048576;

		private int readByteOffset;

		private ByteArray msgBuffer;

		private bool enableTestSplitter = NetConfig.EnableTestSplitter;

		private static int GUID_LENGTH = 16;

		public bool EnableTestSplitter
		{
			get
			{
				return enableTestSplitter;
			}
			set
			{
				enableTestSplitter = value;
			}
		}

		public ByteArray Data
		{
			get
			{
				return msgBuffer;
			}
		}

		public int Length
		{
			get
			{
				return msgBuffer.Count;
			}
			set
			{
				if (readByteOffset > value)
				{
					readByteOffset = value;
				}
				msgBuffer.Count = value;
			}
		}

		public int ReadOffset
		{
			get
			{
				return readByteOffset;
			}
			set
			{
				if (value > msgBuffer.Count)
				{
					throw new Exception("Too Big offset!");
				}
				readByteOffset = value;
			}
		}

		public Message()
		{
			msgBuffer = new ByteArray();
		}

		public Message(ByteArray buffer)
		{
			msgBuffer = buffer;
		}

		private void ThrowArrayIsNullError()
		{
			throw new Exception("error: msgBuffer ArrayPtr is null!");
		}

		private void WriteTestSplitterOnCase()
		{
			if (enableTestSplitter)
			{
				byte value = 254;
				Write_NoTestSplitter(BitConverter.GetBytes(value));
			}
		}

		private void Write_NoTestSplitter(byte[] data)
		{
			if (msgBuffer == null)
			{
				ThrowArrayIsNullError();
			}
			msgBuffer.AddRange(data);
		}

		private void Write_NoTestSplitter(byte[] data, int dataLength)
		{
			if (msgBuffer == null)
			{
				ThrowArrayIsNullError();
			}
			msgBuffer.AddRange(data, dataLength);
		}

		public void UseExternalBuffer(ref byte[] buf, int capacity)
		{
			if (capacity > NetConfig.MessageMaxLength)
			{
				throw new Exception("UseExternalBuffer failed due to too large capacity");
			}
			msgBuffer.UseExternalBuffer(ref buf, capacity);
		}

		public bool CanRead(int count)
		{
			if (count == 0)
			{
				return true;
			}
			int num = readByteOffset;
			if (!CanRead_NoTestSplitter(count))
			{
				readByteOffset = num;
				return false;
			}
			ReadAndCheckTestSplitterOnCase();
			readByteOffset = num;
			return true;
		}

		private bool CanRead_NoTestSplitter(int count)
		{
			if (msgBuffer.data == null)
			{
				throw new Exception("Cannot read from Null pointer message!");
			}
			int num = readByteOffset;
			if (ReadOffset + count > msgBuffer.Count)
			{
				readByteOffset = num;
				return false;
			}
			readByteOffset = num;
			return true;
		}

		internal void AppendFragments(SendFragRefs fragments)
		{
			for (int i = 0; i < fragments.FragmentCount; i++)
			{
				msgBuffer.AddRange(fragments.FragmentData[i].Data, fragments.FragmentData[i].Length);
			}
		}

		public void Write(byte[] data)
		{
			if (data != null && data.Length != 0)
			{
				Write_NoTestSplitter(data);
				WriteTestSplitterOnCase();
			}
		}

		public void Write(byte[] data, int dataLength)
		{
			if (data != null && dataLength > 0)
			{
				Write_NoTestSplitter(data, dataLength);
				WriteTestSplitterOnCase();
			}
		}

		public void Write(bool b)
		{
			Write(BitConverter.GetBytes(b));
		}

		public void Write(sbyte b)
		{
			Write(BitConverter.GetBytes(b), 1);
		}

		public void Write(byte b)
		{
			msgBuffer.Add(b);
			WriteTestSplitterOnCase();
		}

		public void Write(short b)
		{
			Write(BitConverter.GetBytes(b));
		}

		public void Write(ushort b)
		{
			Write(BitConverter.GetBytes(b));
		}

		public void Write(int b)
		{
			Write(BitConverter.GetBytes(b));
		}

		public void Write(long b)
		{
			Write(BitConverter.GetBytes(b));
		}

		public void Write(ulong b)
		{
			Write(BitConverter.GetBytes(b));
		}

		public void Write(uint b)
		{
			Write(BitConverter.GetBytes(b));
		}

		public void Write(float b)
		{
			Write(BitConverter.GetBytes(b));
		}

		public void Write(double b)
		{
			Write(BitConverter.GetBytes(b));
		}

		public void Write(EncryptMode b)
		{
			Write((byte)b);
		}

		public void Write(Guid b)
		{
			Write(b.ToByteArray());
		}

		public void Write(HostID b)
		{
			Write((int)b);
		}

		public void Write(RmiID b)
		{
			Write((ushort)b);
		}

		internal void Write(MessageType b)
		{
			Write((byte)b);
		}

		public void Write(ErrorType b)
		{
			Write((uint)b);
		}

		internal void Write(ReliableUdpFrameType b)
		{
			Write((byte)b);
		}

		internal void Write(FragHeader header)
		{
			int lengthFlag = FragHeaderUtil.GetLengthFlag(header.packetLength);
			int lengthFlag2 = FragHeaderUtil.GetLengthFlag(header.packetID);
			int lengthFlag3 = FragHeaderUtil.GetLengthFlag(header.fragmentID);
			header.splitterFilter |= (ushort)((lengthFlag << 12) | (lengthFlag2 << 10) | (lengthFlag3 << 8));
			Write(header.splitterFilter);
			FragHeaderUtil.WriteCompressedByFlag(this, header.packetLength, lengthFlag);
			FragHeaderUtil.WriteCompressedByFlag(this, header.packetID, lengthFlag2);
			FragHeaderUtil.WriteCompressedByFlag(this, header.fragmentID, lengthFlag3);
		}

		public void Write(IPEndPoint b)
		{
			byte[] addressBytes = b.Address.GetAddressBytes();
			uint num = (uint)(addressBytes[3] << 24);
			num += (uint)(addressBytes[2] << 16);
			num += (uint)(addressBytes[1] << 8);
			num += addressBytes[0];
			Write(num);
			Write((ushort)b.Port);
		}

		public void Write(ByteArray b)
		{
			WriteScalar(b.Count);
			Write(b.data, b.Count);
		}

		public void Write(string b)
		{
			if (b.Length < 0 || b.Length > STRING_LENGTH_LIMIT)
			{
				throw new Exception(string.Format("Message.Write String failed! length = {0}", b.Length));
			}
			WriteScalar(b.Length);
			Write(Encoding.Unicode.GetBytes(b));
		}

		public void Write(FrameNumber b)
		{
			Write((int)b);
		}

		public void Write(NamedAddrPort b)
		{
			Write(b.addr);
			Write(b.port);
		}

		public void Write(MessagePriority b)
		{
			Write((byte)b);
		}

		public void Write(HostIDArray b)
		{
			int count = b.Count;
			WriteScalar(count);
			for (int i = 0; i < count; i++)
			{
				Write(b[i]);
			}
		}

		internal void Write(RelayDestList b)
		{
			int count = b.Count;
			WriteScalar(count);
			for (int i = 0; i < count; i++)
			{
				Write(b[i]);
			}
		}

		internal void Write(RelayDest b)
		{
			Write(b.sendTo);
			Write(b.frameNumber);
		}

		public void WriteScalar(long a)
		{
			CompactScalarValue compactScalarValue = new CompactScalarValue();
			compactScalarValue.MakeBlock(a);
			Write(compactScalarValue.filledBlock, compactScalarValue.filledBlockLength);
		}

		public void WriteScalar(int a)
		{
			WriteScalar((long)a);
		}

		public void WriteScalar(uint a)
		{
			WriteScalar((long)a);
		}

		public void WriteScalar(ulong a)
		{
			WriteScalar((long)a);
		}

		private void ReadAndCheckTestSplitterOnCase()
		{
			if (enableTestSplitter)
			{
				byte b = 0;
				if (!Read(out b) || b != 254)
				{
					throw new Exception(string.Format("CMessage: Test splitter failure(reading={0},length={1})", ReadOffset, Length));
				}
			}
		}

		public bool SkipRead(int count)
		{
			ReadAndCheckTestSplitterOnCase();
			if (msgBuffer == null)
			{
				throw new Exception("Read data from Null message");
			}
			if (ReadOffset + count > msgBuffer.Count)
			{
				return false;
			}
			readByteOffset += count;
			return true;
		}

		public bool ReadScalar(ref long a)
		{
			CompactScalarValue compactScalarValue = new CompactScalarValue();
			if (!compactScalarValue.ExtractValue(msgBuffer.data, ReadOffset, Length))
			{
				return false;
			}
			a = compactScalarValue.extractedValue;
			SkipRead(compactScalarValue.extracteeLength);
			return true;
		}

		public bool ReadScalar(ref int a)
		{
			long a2 = 0L;
			if (!ReadScalar(ref a2))
			{
				return false;
			}
			a = (int)a2;
			return true;
		}

		public bool ReadScalar(ref ulong a)
		{
			long a2 = 0L;
			if (!ReadScalar(ref a2))
			{
				return false;
			}
			a = (ulong)a2;
			return true;
		}

		public bool Read(out byte[] b, int count)
		{
			b = new byte[count];
			if (count == 0)
			{
				return true;
			}
			if (msgBuffer == null)
			{
				throw new Exception("Cannot read from Null pointer message!");
			}
			if (ReadOffset + count > msgBuffer.Count)
			{
				return false;
			}
			Array.Copy(msgBuffer.data, ReadOffset, b, 0, count);
			readByteOffset += count;
			ReadAndCheckTestSplitterOnCase();
			return true;
		}

		public bool Read(out bool b)
		{
			b = false;
			if (msgBuffer == null)
			{
				throw new Exception("Cannot read from Null pointer message!");
			}
			if (ReadOffset + 1 > msgBuffer.Count)
			{
				return false;
			}
			b = BitConverter.ToBoolean(msgBuffer.data, ReadOffset);
			readByteOffset++;
			ReadAndCheckTestSplitterOnCase();
			return true;
		}

		public bool Read(out sbyte b)
		{
			b = 0;
			if (msgBuffer == null)
			{
				throw new Exception("Cannot read from Null pointer message!");
			}
			if (ReadOffset + 1 > msgBuffer.Count)
			{
				return false;
			}
			b = (sbyte)msgBuffer.data[ReadOffset];
			readByteOffset++;
			ReadAndCheckTestSplitterOnCase();
			return true;
		}

		public bool Read(out byte b)
		{
			b = 0;
			if (msgBuffer == null)
			{
				throw new Exception("Cannot read from Null pointer message!");
			}
			if (ReadOffset + 1 > msgBuffer.Count)
			{
				return false;
			}
			b = msgBuffer.data[ReadOffset];
			readByteOffset++;
			ReadAndCheckTestSplitterOnCase();
			return true;
		}

		public bool Read(out short b)
		{
			b = 0;
			if (msgBuffer == null)
			{
				throw new Exception("Cannot read from Null pointer message!");
			}
			if (ReadOffset + 2 > msgBuffer.Count)
			{
				return false;
			}
			b = BitConverter.ToInt16(msgBuffer.data, ReadOffset);
			readByteOffset += 2;
			ReadAndCheckTestSplitterOnCase();
			return true;
		}

		public bool Read(out ushort b)
		{
			b = 0;
			if (msgBuffer == null)
			{
				throw new Exception("Cannot read from Null pointer message!");
			}
			if (ReadOffset + 2 > msgBuffer.Count)
			{
				return false;
			}
			b = BitConverter.ToUInt16(msgBuffer.data, ReadOffset);
			readByteOffset += 2;
			ReadAndCheckTestSplitterOnCase();
			return true;
		}

		public bool Read(out int b)
		{
			b = 0;
			if (msgBuffer == null)
			{
				throw new Exception("Cannot read from Null pointer message!");
			}
			if (ReadOffset + 4 > msgBuffer.Count)
			{
				return false;
			}
			b = BitConverter.ToInt32(msgBuffer.data, ReadOffset);
			readByteOffset += 4;
			ReadAndCheckTestSplitterOnCase();
			return true;
		}

		public bool Read(out uint b)
		{
			b = 0u;
			if (msgBuffer == null)
			{
				throw new Exception("Cannot read from Null pointer message!");
			}
			if (ReadOffset + 4 > msgBuffer.Count)
			{
				return false;
			}
			b = BitConverter.ToUInt32(msgBuffer.data, ReadOffset);
			readByteOffset += 4;
			ReadAndCheckTestSplitterOnCase();
			return true;
		}

		public bool Read(out long b)
		{
			b = 0L;
			if (msgBuffer == null)
			{
				throw new Exception("Cannot read from Null pointer message!");
			}
			if (ReadOffset + 8 > msgBuffer.Count)
			{
				return false;
			}
			b = BitConverter.ToInt64(msgBuffer.data, ReadOffset);
			readByteOffset += 8;
			ReadAndCheckTestSplitterOnCase();
			return true;
		}

		public bool Read(out ulong b)
		{
			b = 0uL;
			if (msgBuffer == null)
			{
				throw new Exception("Cannot read from Null pointer message!");
			}
			if (ReadOffset + 8 > msgBuffer.Count)
			{
				return false;
			}
			b = BitConverter.ToUInt64(msgBuffer.data, ReadOffset);
			readByteOffset += 8;
			ReadAndCheckTestSplitterOnCase();
			return true;
		}

		public bool Read(out float b)
		{
			b = 0f;
			if (msgBuffer == null)
			{
				throw new Exception("Cannot read from Null pointer message!");
			}
			if (ReadOffset + 4 > msgBuffer.Count)
			{
				return false;
			}
			b = BitConverter.ToSingle(msgBuffer.data, ReadOffset);
			readByteOffset += 4;
			ReadAndCheckTestSplitterOnCase();
			return true;
		}

		public bool Read(out double b)
		{
			b = 0.0;
			if (msgBuffer == null)
			{
				throw new Exception("Cannot read from Null pointer message!");
			}
			if (ReadOffset + 8 > msgBuffer.Count)
			{
				return false;
			}
			b = BitConverter.ToDouble(msgBuffer.data, ReadOffset);
			readByteOffset += 8;
			ReadAndCheckTestSplitterOnCase();
			return true;
		}

		internal bool Read(out FragHeader header)
		{
			header = default(FragHeader);
			if (!Read(out header.splitterFilter))
			{
				return false;
			}
			int flag = (header.splitterFilter & 0x3000) >> 12;
			int flag2 = (header.splitterFilter & 0xC00) >> 10;
			int flag3 = (header.splitterFilter & 0x300) >> 8;
			if (FragHeaderUtil.ReadCompressedByFlag(this, out header.packetLength, flag) && FragHeaderUtil.ReadCompressedByFlag(this, out header.packetID, flag2))
			{
				return FragHeaderUtil.ReadCompressedByFlag(this, out header.fragmentID, flag3);
			}
			return false;
		}

		public bool Read(out RmiID b)
		{
			b = RmiID.None;
			ushort b2 = 0;
			if (!Read(out b2))
			{
				return false;
			}
			b = (RmiID)b2;
			return true;
		}

		public bool Read(out EncryptMode b)
		{
			b = EncryptMode.None;
			byte b2 = 0;
			if (!Read(out b2))
			{
				return false;
			}
			b = (EncryptMode)b2;
			return true;
		}

		public bool Read(out HostID b)
		{
			b = HostID.None;
			int b2 = 0;
			if (!Read(out b2))
			{
				return false;
			}
			b = (HostID)b2;
			return true;
		}

		public bool Read(out ByteArray b)
		{
			b = new ByteArray();
			int a = 0;
			if (!ReadScalar(ref a))
			{
				return false;
			}
			if (a < 0 || a > NetConfig.MessageMaxLength)
			{
				return false;
			}
			b.Count = a;
			return Read(out b.data, a);
		}

		public bool Read(out IPEndPoint b)
		{
			b = new IPEndPoint(new IPAddress(0L), 0);
			uint b2 = 0u;
			if (!Read(out b2))
			{
				return false;
			}
			ushort b3 = 0;
			if (!Read(out b3))
			{
				return false;
			}
			b.Address = new IPAddress(b2);
			b.Port = b3;
			return true;
		}

		public bool Read(out ErrorType b)
		{
			b = ErrorType.Ok;
			uint b2 = 0u;
			if (!Read(out b2))
			{
				return false;
			}
			b = (ErrorType)b2;
			return true;
		}

		public bool Read(out string b)
		{
			b = "";
			int a = 0;
			if (!ReadScalar(ref a))
			{
				return false;
			}
			if (a < 0 || a > STRING_LENGTH_LIMIT)
			{
				throw new Exception(string.Format("Message Read String failed! length = {0}", a));
			}
			if (a == 0)
			{
				return true;
			}
			a = 2 * a;
			if (ReadOffset + a > msgBuffer.Count)
			{
				return false;
			}
			b = Encoding.Unicode.GetString(msgBuffer.data, ReadOffset, a);
			readByteOffset += a;
			return true;
		}

		public bool Read(out FrameNumber b)
		{
			b = (FrameNumber)0;
			uint b2 = 0u;
			if (!Read(out b2))
			{
				return false;
			}
			b = (FrameNumber)b2;
			return true;
		}

		public bool Read(out Guid b)
		{
			byte[] b2 = new byte[GUID_LENGTH];
			if (!Read(out b2, GUID_LENGTH))
			{
				b = Guid.Empty;
				return false;
			}
			b = new Guid(b2);
			return true;
		}

		public bool Read(out NamedAddrPort b)
		{
			b = new NamedAddrPort();
			if (Read(out b.addr))
			{
				return Read(out b.port);
			}
			return false;
		}

		internal bool Read(out ReliableUdpFrameType b)
		{
			b = ReliableUdpFrameType.None;
			byte b2 = 0;
			bool flag = Read(out b2);
			if (flag)
			{
				b = (ReliableUdpFrameType)b2;
			}
			return flag;
		}

		internal bool Read(out MessageType b)
		{
			b = MessageType.None;
			byte b2 = 0;
			if (!Read(out b2))
			{
				return false;
			}
			b = (MessageType)b2;
			return true;
		}

		internal bool Read(out FallbackMethod b)
		{
			b = FallbackMethod.None;
			byte b2 = 0;
			if (!Read(out b2))
			{
				return false;
			}
			b = (FallbackMethod)b2;
			return true;
		}

		internal bool Read(out DirectP2PStartCondition b)
		{
			b = DirectP2PStartCondition.Last;
			byte b2 = 0;
			if (!Read(out b2))
			{
				return false;
			}
			b = (DirectP2PStartCondition)b2;
			return true;
		}

		internal bool Read(out NetSettings b)
		{
			b = new NetSettings();
			if (Read(out b.fallbackMethod) && Read(out b.serverMessageMaxLength) && Read(out b.clientMessageMaxLength) && Read(out b.defaultTimeoutTimeMs) && Read(out b.directP2PStartCondition) && Read(out b.overSendSuspectingThresholdInBytes) && Read(out b.enableNagleAlgorithm) && Read(out b.encryptedMessageKeyLength) && Read(out b.fastEncryptedMessageKeyLength) && Read(out b.allowServerAsP2PGroupMember) && Read(out b.enableP2PEncryptedMessaging) && Read(out b.upnpDetectNatDevice) && Read(out b.upnpTcpAddPortMapping) && Read(out b.enableLookaheadP2PSend) && Read(out b.enablePingTest) && Read(out b.ignoreFailedBindPort))
			{
				return Read(out b.emergencyLogLineCount);
			}
			return false;
		}

		internal bool Read(out MessagePriority b)
		{
			b = MessagePriority.Last;
			byte b2 = 0;
			if (!Read(out b2))
			{
				return false;
			}
			b = (MessagePriority)b2;
			return true;
		}

		internal bool Read(out HostIDArray b)
		{
			b = new HostIDArray();
			int a = 0;
			if (!ReadScalar(ref a))
			{
				return false;
			}
			if (a < 0 || a > NetConfig.MessageMaxLength)
			{
				return false;
			}
			b.Count = a;
			for (int i = 0; i < a; i++)
			{
				if (!Read(out b.data[i]))
				{
					return false;
				}
			}
			return true;
		}
	}
}

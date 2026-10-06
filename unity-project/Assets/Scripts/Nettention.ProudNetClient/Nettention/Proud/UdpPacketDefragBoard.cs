using System;
using System.Collections.Generic;
using System.Net;

namespace Nettention.Proud
{
	internal class UdpPacketDefragBoard
	{
		private Dictionary<IPEndPoint, DefraggingPackets> addrPortToDefraggingPacketsMap = new Dictionary<IPEndPoint, DefraggingPackets>();

		private long m_recentAssemblyingPacketIDsClearTime;

		private static int GetAppropriateFlagListLength(int packetLength)
		{
			if (packetLength <= 0)
			{
				return 0;
			}
			return (packetLength - 1) / NetConfig.MtuLength + 1;
		}

		private void PruneTooOldDefragBoard(long currentTime)
		{
			List<IPEndPoint> list = new List<IPEndPoint>(addrPortToDefraggingPacketsMap.Keys);
			foreach (IPEndPoint item in list)
			{
				DefraggingPackets defraggingPackets = addrPortToDefraggingPacketsMap[item];
				List<int> list2 = new List<int>(defraggingPackets.Keys);
				foreach (int item2 in list2)
				{
					DefraggingPacket defraggingPacket = defraggingPackets[item2];
					if (currentTime - defraggingPacket.createdTime > NetConfig.AssembleFraggedPacketTimeoutMs)
					{
						defraggingPackets.Remove(item2);
					}
				}
				if (defraggingPackets.Count == 0 && defraggingPackets.recentReceiveSpeed.IsRemovingSafeForCalcSpeed(currentTime))
				{
					addrPortToDefraggingPacketsMap.Remove(item);
				}
			}
		}

		internal AssembledPacketError PushFragmentAndPopAssembledPacket(byte[] fragData, int fragLength, IPEndPoint senderAddr, HostID srcHostID, HostID localHostID, long currtime, int messageMaxLength, ref AssembledPacket output, ref string outError)
		{
			Message message = new Message();
			message.UseExternalBuffer(ref fragData, fragLength);
			message.Length = fragLength;
			message.ReadOffset = 0;
			FragHeader header = default(FragHeader);
			if (!message.Read(out header))
			{
				outError = "UDP frag header missing!";
				return AssembledPacketError.Error;
			}
			byte b = (byte)((header.splitterFilter & 0xC000) >> 14);
			if (b != UdpPacketFragBoard.FragSplitter && b != UdpPacketFragBoard.FullPacketSplitter)
			{
				outError = string.Format("Cannot identify UDP fragment nor full packet! fragheaderSplitter={0}", b);
				return AssembledPacketError.Error;
			}
			byte b2 = (byte)(header.splitterFilter & 0xFF);
			b2 ^= (byte)(header.packetID & 0xFF);
			if (FilterTag.ShouldBeFiltered(b2, srcHostID, localHostID))
			{
				return AssembledPacketError.Assembling;
			}
			if (b == UdpPacketFragBoard.FragSplitter && (header.packetLength <= 0 || header.packetLength > messageMaxLength || header.fragmentID < 0 || header.fragmentID > header.packetLength / NetConfig.MtuLength))
			{
				outError = string.Format("UDP frag length is wrong #1! packetlength={0}, maxlength={1}, fragID={2}", header.packetLength, messageMaxLength, header.fragmentID);
				return AssembledPacketError.Error;
			}
			if (b == UdpPacketFragBoard.FullPacketSplitter && (header.packetLength <= 0 || header.packetLength > messageMaxLength))
			{
				outError = "UDP full packet length is wrong!";
				return AssembledPacketError.Error;
			}
			int num = NetConfig.MtuLength * header.fragmentID;
			int num2 = Math.Min(NetConfig.MtuLength, header.packetLength - num);
			int num3 = message.Length - message.ReadOffset;
			if (b == UdpPacketFragBoard.FragSplitter && num2 != num3)
			{
				outError = string.Format("UDP frag length is wrong #2! desiredFragLength=%d, fragPayloadLength=%d", num2, num3);
				return AssembledPacketError.Error;
			}
			DefraggingPackets value = null;
			if (!addrPortToDefraggingPacketsMap.TryGetValue(senderAddr, out value))
			{
				value = new DefraggingPackets();
				value.recentReceiveSpeed.TouchFirstTime(currtime);
				addrPortToDefraggingPacketsMap.Add(senderAddr, value);
			}
			if (b == UdpPacketFragBoard.FragSplitter)
			{
				DefraggingPacket value2 = null;
				if (!value.TryGetValue(header.packetID, out value2))
				{
					value2 = new DefraggingPacket();
					value2.assembledData.Count = header.packetLength;
					value2.fragFillFlagList.Count = GetAppropriateFlagListLength(header.packetLength);
					value2.createdTime = currtime;
					Array.Clear(value2.fragFillFlagList.data, 0, value2.fragFillFlagList.Count);
					value.Add(header.packetID, value2);
				}
				else if (value2.assembledData.Count != header.packetLength)
				{
					outError = string.Format("UDP frag length is wrong #3! assembledDataCount={0}, packetLength={1}", value2.assembledData.Count, header.packetLength);
					value.Remove(header.packetID);
					return AssembledPacketError.Error;
				}
				if (header.fragmentID >= value2.fragFillFlagList.Count)
				{
					outError = "UDP FragID is wrong!";
					return AssembledPacketError.Error;
				}
				if (num3 + num > value2.assembledData.Count)
				{
					outError = "UDP Frag Payload Length is wrong!";
					return AssembledPacketError.Error;
				}
				if (!value2.fragFillFlagList[header.fragmentID])
				{
					value2.fragFillFlagList[header.fragmentID] = true;
					value2.fragFilledCount++;
					value.recentReceiveSpeed.Accumulate(fragLength, currtime);
					Array.Copy(message.Data.data, message.ReadOffset, value2.assembledData.data, num, num3);
				}
				if (value2.fragFillFlagList.Count == value2.fragFilledCount)
				{
					if (!value.m_unreliableMessageLossRatio.AddPacketID(header.packetID))
					{
						return AssembledPacketError.Assembling;
					}
					value.m_unreliableMessageLossRatio.UpdateUnreliableMessagingLossRatioVars(header.packetID);
					output.senderAddr = senderAddr;
					output.packet = value2;
					value.Remove(header.packetID);
					return AssembledPacketError.Ok;
				}
			}
			else if (b == UdpPacketFragBoard.FullPacketSplitter && num3 > 0)
			{
				value.recentReceiveSpeed.Accumulate(fragLength, currtime);
				if (!value.m_unreliableMessageLossRatio.AddPacketID(header.packetID))
				{
					return AssembledPacketError.Assembling;
				}
				value.m_unreliableMessageLossRatio.UpdateUnreliableMessagingLossRatioVars(header.packetID);
				output.senderAddr = senderAddr;
				DefraggingPacket defraggingPacket = new DefraggingPacket();
				defraggingPacket.assembledData.Count = num3;
				Array.Copy(message.Data.data, message.ReadOffset, defraggingPacket.assembledData.data, 0, num3);
				output.packet = defraggingPacket;
				return AssembledPacketError.Ok;
			}
			return AssembledPacketError.Assembling;
		}

		public void DoForLongInterval(long currentTime)
		{
			bool flag = false;
			if (currentTime - m_recentAssemblyingPacketIDsClearTime > NetConfig.RecentAssemblyingPacketIDsClearIntervalMs)
			{
				flag = true;
				m_recentAssemblyingPacketIDsClearTime = currentTime;
			}
			foreach (DefraggingPackets value in addrPortToDefraggingPacketsMap.Values)
			{
				if (flag)
				{
					value.m_unreliableMessageLossRatio.ResetUnreliableMessagingLossRatioVars();
				}
				DoForLongInterval(value, currentTime);
			}
			PruneTooOldDefragBoard(currentTime);
		}

		private void DoForLongInterval(DefraggingPackets packets, long currentTime)
		{
			packets.recentReceiveSpeed.DoForLongInterval(currentTime);
		}

		public int GetUnreliableMessagingLossRatioPercent(IPEndPoint senderAddr)
		{
			DefraggingPackets value;
			if (addrPortToDefraggingPacketsMap.TryGetValue(senderAddr, out value))
			{
				return value.m_unreliableMessageLossRatio.GetUnreliableMessagingLossRatioPercent();
			}
			return 0;
		}

		public long GetRecentReceiveSpeed(IPEndPoint src)
		{
			DefraggingPackets value = null;
			if (addrPortToDefraggingPacketsMap.TryGetValue(src, out value))
			{
				return value.recentReceiveSpeed.RecentSpeed;
			}
			return 0L;
		}

		public void Remove(IPEndPoint srcAddr)
		{
			addrPortToDefraggingPacketsMap.Remove(srcAddr);
		}

		public void Clear()
		{
			addrPortToDefraggingPacketsMap.Clear();
		}
	}
}

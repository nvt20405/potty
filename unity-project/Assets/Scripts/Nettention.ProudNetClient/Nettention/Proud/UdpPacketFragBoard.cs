using System;
using System.Collections.Generic;
using System.Net;

namespace Nettention.Proud
{
	internal class UdpPacketFragBoard
	{
		internal class PacketQueue : ListNode<PacketQueue>
		{
			internal class PerPriorityQueue
			{
				public ListNode<UdpPacketCtx>.ListOwner fraggableUdpPacketList = new ListNode<UdpPacketCtx>.ListOwner();

				public ListNode<UdpPacketCtx>.ListOwner noFraggableUdpPacketList = new ListNode<UdpPacketCtx>.ListOwner();

				public int TotalLengthInBytes
				{
					get
					{
						int num = 0;
						for (UdpPacketCtx udpPacketCtx = fraggableUdpPacketList.First; udpPacketCtx != null; udpPacketCtx = udpPacketCtx.Next)
						{
							num += udpPacketCtx.packet.Count;
						}
						for (UdpPacketCtx udpPacketCtx2 = noFraggableUdpPacketList.First; udpPacketCtx2 != null; udpPacketCtx2 = udpPacketCtx2.Next)
						{
							num += udpPacketCtx2.packet.Count;
						}
						return num;
					}
				}
			}

			internal PerPriorityQueue[] priorities = new PerPriorityQueue[6];

			internal long lastAccessedTime;

			internal IPEndPoint remoteAddr = new IPEndPoint(0L, 0);

			internal byte filterTag;

			internal List<UdpPacketCtx> fragBoardedPackets = new List<UdpPacketCtx>();

			internal int fragBoardTotalBytes;

			internal int destFragID;

			internal int globalOffsetInFragBoard;

			internal int localOffsetInFragBoard;

			internal int srcIndexInFragBoard;

			internal int currentPacketID;

			internal SendBrake sendBrake = new SendBrake();

			internal SendSpeedMeasurer sendSpeed = new SendSpeedMeasurer();

			internal AllowedMaxSendSpeed allowedMaxSendSpeed = new AllowedMaxSendSpeed();

			internal RecentReceiveSpeedAtReceiverSide recentReceiveSpeedAtReceiverSide = new RecentReceiveSpeedAtReceiverSide();

			public int TotalCount
			{
				get
				{
					int num = 0;
					for (int i = 0; i < 6; i++)
					{
						num += priorities[i].fraggableUdpPacketList.Count;
						num += priorities[i].noFraggableUdpPacketList.Count;
					}
					return num + fragBoardedPackets.Count;
				}
			}

			public bool IsEmpty
			{
				get
				{
					for (int i = 0; i < 6; i++)
					{
						if (priorities[i].fraggableUdpPacketList.Count > 0)
						{
							return false;
						}
						if (priorities[i].noFraggableUdpPacketList.Count > 0)
						{
							return false;
						}
					}
					return fragBoardedPackets.Count <= 0;
				}
			}

			public int TotalLengthInBytes
			{
				get
				{
					int num = 0;
					for (int i = 0; i < 6; i++)
					{
						num += priorities[i].TotalLengthInBytes;
					}
					if (fragBoardTotalBytes > 0)
					{
						num += fragBoardTotalBytes - globalOffsetInFragBoard;
					}
					return num;
				}
			}

			public PacketQueue()
			{
				Random random = new Random();
				currentPacketID = random.Next();
				for (int i = 0; i < 6; i++)
				{
					priorities[i] = new PerPriorityQueue();
				}
			}

			public bool IsExistant(long currTime, bool enableSendBrake)
			{
				if (TotalCount != 0)
				{
					if (enableSendBrake)
					{
						return !sendBrake.BrakeNeeded(currTime, allowedMaxSendSpeed.Value);
					}
					return true;
				}
				return false;
			}

			public void PopFragmentOrFullPacket(long currentTime, UdpPacketFragBoardOutput output)
			{
				for (int i = 0; i < 6; i++)
				{
					PerPriorityQueue perPriorityQueue = priorities[i];
					UdpPacketCtx first = perPriorityQueue.noFraggableUdpPacketList.First;
					if (first != null)
					{
						output.ResetForReuse();
						output.sendTo = new IPEndPoint(remoteAddr.Address, remoteAddr.Port);
						FragHeader header = new FragHeader
						{
							splitterFilter = 0
						};
						header.splitterFilter |= (ushort)(FullPacketSplitter << 14);
						header.packetLength = first.packet.Count;
						header.packetID = 0;
						header.fragmentID = 0;
						header.splitterFilter |= (ushort)((filterTag ^ header.packetID) & 0xFF);
						Message message = new Message(output.sendFragFrag);
						message.Write(header);
						output.sendFragFrag.AddRange(first.packet.data, first.packet.Count);
						output.ttl = first.ttl;
						output.owningPackets.Add(first);
						first.UnlinkSelf();
						return;
					}
				}
				bool flag = fragBoardedPackets.Count == 0;
				short num = -1;
				if (flag)
				{
					ResetFragBoardState();
					currentPacketID++;
					int num2 = 0;
					for (int j = 0; j < 6; j++)
					{
						PerPriorityQueue perPriorityQueue2 = priorities[j];
						while (perPriorityQueue2.fraggableUdpPacketList.Count > 0)
						{
							UdpPacketCtx first2 = perPriorityQueue2.fraggableUdpPacketList.First;
							if (num2 == 0)
							{
								fragBoardedPackets.Add(first2);
								fragBoardTotalBytes += first2.packet.Count;
								num = first2.ttl;
								first2.UnlinkSelf();
							}
							else
							{
								if (fragBoardTotalBytes + first2.packet.Count >= NetConfig.MtuLength || num != first2.ttl)
								{
									goto end_IL_021a;
								}
								fragBoardedPackets.Add(first2);
								fragBoardTotalBytes += first2.packet.Count;
								first2.UnlinkSelf();
							}
							num2++;
						}
						continue;
						end_IL_021a:
						break;
					}
				}
				if (fragBoardTotalBytes <= 0)
				{
					Sysutil.ThrowInvalidArgumentException();
				}
				output.ResetForReuse();
				output.sendTo = new IPEndPoint(remoteAddr.Address, remoteAddr.Port);
				int num3 = Math.Min(NetConfig.MtuLength, fragBoardTotalBytes - globalOffsetInFragBoard);
				FragHeader header2 = new FragHeader
				{
					splitterFilter = 0
				};
				header2.splitterFilter |= (ushort)(FragSplitter << 14);
				header2.packetLength = fragBoardTotalBytes;
				header2.packetID = currentPacketID;
				header2.fragmentID = destFragID;
				header2.splitterFilter |= (ushort)((filterTag ^ header2.packetID) & 0xFF);
				Message message2 = new Message(output.sendFragFrag);
				message2.Write(header2);
				output.ttl = num;
				int num4 = globalOffsetInFragBoard;
				while (globalOffsetInFragBoard < num4 + num3)
				{
					UdpPacketCtx udpPacketCtx = fragBoardedPackets[srcIndexInFragBoard];
					int num5 = Math.Min(num4 + num3 - globalOffsetInFragBoard, udpPacketCtx.packet.Count - localOffsetInFragBoard);
					if (num5 <= 0)
					{
						fragBoardedPackets.Clear();
						throw new Exception("Unexpected at PacketQueue Pop!");
					}
					output.sendFragFrag.AddRange(udpPacketCtx.packet.data, localOffsetInFragBoard, num5);
					localOffsetInFragBoard += num5;
					globalOffsetInFragBoard += num5;
					if (localOffsetInFragBoard == udpPacketCtx.packet.Count)
					{
						srcIndexInFragBoard++;
						localOffsetInFragBoard = 0;
					}
				}
				destFragID++;
				if (globalOffsetInFragBoard == fragBoardTotalBytes)
				{
					output.owningPackets.AddRange(fragBoardedPackets);
					fragBoardedPackets.Clear();
				}
				int count = output.sendFragFrag.Count;
				sendBrake.Accumulate(count, currentTime);
				sendSpeed.Accumulate(count, currentTime);
			}

			private void ResetFragBoardState()
			{
				fragBoardTotalBytes = 0;
				destFragID = 0;
				globalOffsetInFragBoard = 0;
				localOffsetInFragBoard = 0;
				srcIndexInFragBoard = 0;
			}
		}

		public delegate void RequestReceiveSpeedAtReceiverSide_NoRelayDelegate(IPEndPoint dest);

		public static readonly byte FragSplitter = 1;

		public static readonly byte FullPacketSplitter = 2;

		private ListNode<PacketQueue>.ListOwner m_sendReadyList = new ListNode<PacketQueue>.ListOwner();

		private Dictionary<IPEndPoint, PacketQueue> addrPortToQueueMap = new Dictionary<IPEndPoint, PacketQueue>();

		private bool enableSendBrake = true;

		private RequestReceiveSpeedAtReceiverSide_NoRelayDelegate requestReceiveSpeedAtReceiverSide_NoRelay = (IPEndPoint dest) =>
		{
		};

		public RequestReceiveSpeedAtReceiverSide_NoRelayDelegate RequestReceiveSpeedAtReceiverSide_NoRelay
		{
			set
			{
				requestReceiveSpeedAtReceiverSide_NoRelay = value;
			}
		}

		private void AssertConsist()
		{
		}

		private void AddToSendReadyListOnNeed(PacketQueue packetQueue, long currTime)
		{
			if (packetQueue.Owner == null && packetQueue.IsExistant(currTime, enableSendBrake))
			{
				m_sendReadyList.PushBack(packetQueue);
			}
		}

		public void Remove(IPEndPoint key)
		{
			PacketQueue value;
			if (addrPortToQueueMap.TryGetValue(key, out value))
			{
				value.UnlinkSelf();
				addrPortToQueueMap.Remove(key);
			}
		}

		public void Clear()
		{
			foreach (PacketQueue value in addrPortToQueueMap.Values)
			{
				value.UnlinkSelf();
			}
			addrPortToQueueMap.Clear();
		}

		public void AddNewPacket(HostID finalDestHostID, byte filterTag, IPEndPoint sendTo, SendFragRefs sendData, long addedTime, SendOpt sendOpt)
		{
			if (!NetUtil.IsUnicastEndpoint(sendTo) || sendData.TotalLength <= 0)
			{
				return;
			}
			PacketQueue value = null;
			if (!addrPortToQueueMap.TryGetValue(sendTo, out value))
			{
				PacketQueue packetQueue = new PacketQueue();
				packetQueue.remoteAddr = sendTo;
				packetQueue.filterTag = filterTag;
				packetQueue.lastAccessedTime = addedTime;
				addrPortToQueueMap.Add(sendTo, packetQueue);
				value = packetQueue;
			}
			if (!value.remoteAddr.Equals(sendTo))
			{
				throw new Exception("PacketQueue consistency failed!");
			}
			if (sendOpt.priority < MessagePriority.Ring0 || sendOpt.priority >= MessagePriority.Last)
			{
				Sysutil.ThrowInvalidArgumentException();
			}
			if (!NetConfig.EnableMessagePriority)
			{
				sendOpt.priority = MessagePriority.Ring0;
			}
			PacketQueue.PerPriorityQueue perPriorityQueue = value.priorities[(int)sendOpt.priority];
			UdpPacketCtx udpPacketCtx;
			if (sendOpt.uniqueID != 0 && finalDestHostID != HostID.None)
			{
				udpPacketCtx = ((!sendOpt.INTERNAL_USE_fraggingOnNeed) ? perPriorityQueue.noFraggableUdpPacketList.First : perPriorityQueue.fraggableUdpPacketList.First);
				while (udpPacketCtx != null)
				{
					if (udpPacketCtx.uniqueID == 0 || sendOpt.uniqueID != udpPacketCtx.uniqueID || finalDestHostID != udpPacketCtx.hostID)
					{
						udpPacketCtx = udpPacketCtx.Next;
						continue;
					}
					goto IL_00f6;
				}
			}
			UdpPacketCtx udpPacketCtx2 = new UdpPacketCtx();
			if (sendOpt.INTERNAL_USE_fraggingOnNeed)
			{
				perPriorityQueue.fraggableUdpPacketList.PushBack(udpPacketCtx2);
			}
			else
			{
				perPriorityQueue.noFraggableUdpPacketList.PushBack(udpPacketCtx2);
			}
			udpPacketCtx2.uniqueID = sendOpt.uniqueID;
			udpPacketCtx2.ttl = sendOpt.ttl;
			udpPacketCtx2.hostID = finalDestHostID;
			sendData.CopyTo(udpPacketCtx2.packet);
			goto IL_016d;
			IL_00f6:
			sendData.CopyTo(udpPacketCtx.packet);
			goto IL_016d;
			IL_016d:
			if (value.IsEmpty)
			{
				throw new Exception("PacketQueue consistency 2 failed!");
			}
			value.lastAccessedTime = addedTime;
			AddToSendReadyListOnNeed(value, addedTime);
			AssertConsist();
		}

		public int GetTotalPacketCountOfAddr(IPEndPoint addr)
		{
			PacketQueue value = null;
			if (addrPortToQueueMap.TryGetValue(addr, out value))
			{
				return value.TotalCount;
			}
			return 0;
		}

		public int FromTotalPacketInBytesByAddr(IPEndPoint addr)
		{
			PacketQueue value = null;
			if (addrPortToQueueMap.TryGetValue(addr, out value))
			{
				return value.TotalLengthInBytes;
			}
			return 0;
		}

		public bool PopAnySendQueueFilledOneWithCoalesce(UdpPacketFragBoardOutput output, long currentTime)
		{
			PacketQueue first = m_sendReadyList.First;
			if (first == null)
			{
				return false;
			}
			if (first.IsEmpty)
			{
				throw new Exception("Unexpected state in RemoteToPacketSendMap!");
			}
			first.PopFragmentOrFullPacket(currentTime, output);
			if (output.sendFragFrag.Count == 0)
			{
				throw new Exception("Unexpected state in RemoteToPacketSendMap #2!");
			}
			if (!first.IsExistant(currentTime, enableSendBrake) && first.Owner != null)
			{
				first.UnlinkSelf();
				first.lastAccessedTime = currentTime;
			}
			else
			{
				first.UnlinkSelf();
				m_sendReadyList.PushBack(first);
				first.lastAccessedTime = currentTime;
			}
			AssertConsist();
			return true;
		}

		public void DoForLongInterval(long currentTime, int overSendSupectingThresholdInBytes)
		{
			List<IPEndPoint> list = new List<IPEndPoint>(addrPortToQueueMap.Keys);
			foreach (IPEndPoint item in list)
			{
				PacketQueue packetQueue = addrPortToQueueMap[item];
				packetQueue.sendBrake.DoForLongInterval(currentTime);
				packetQueue.sendSpeed.DoForLongInterval(currentTime);
				packetQueue.recentReceiveSpeedAtReceiverSide.DoForLongInterval(currentTime);
				if (packetQueue.sendSpeed.RecentSpeed > overSendSupectingThresholdInBytes)
				{
					requestReceiveSpeedAtReceiverSide_NoRelay(item);
				}
				packetQueue.allowedMaxSendSpeed.DoForLongInterval(packetQueue.sendSpeed.RecentSpeed, packetQueue.recentReceiveSpeedAtReceiverSide.Value);
				if (currentTime - packetQueue.lastAccessedTime > NetConfig.RemoveTooOldUdpSendPacketQueueTimeoutMs)
				{
					packetQueue.UnlinkSelf();
					addrPortToQueueMap.Remove(item);
				}
			}
		}

		public void DoForShortInterval(long currentTime)
		{
			foreach (PacketQueue value in addrPortToQueueMap.Values)
			{
				AddToSendReadyListOnNeed(value, currentTime);
			}
		}

		public void SetReceiveSpeedAtReceiverSide(IPEndPoint dest, long speed, long currTime)
		{
			PacketQueue value = null;
			if (addrPortToQueueMap.TryGetValue(dest, out value))
			{
				value.recentReceiveSpeedAtReceiverSide.SetValue(speed, currTime);
			}
		}
	}
}

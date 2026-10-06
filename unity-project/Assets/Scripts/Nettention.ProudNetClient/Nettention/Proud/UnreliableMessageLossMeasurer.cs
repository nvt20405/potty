using System;
using System.Collections.Generic;

namespace Nettention.Proud
{
	internal class UnreliableMessageLossMeasurer
	{
		private Dictionary<int, int> m_recentAssemblyingPacketIDs = new Dictionary<int, int>();

		private int m_nMinPacketIDValue = int.MaxValue;

		private int m_nMaxPacketIDValue = int.MinValue;

		private bool m_hasReceivedUnreliablePacket;

		public bool AddPacketID(int nPacketID)
		{
			if (m_recentAssemblyingPacketIDs.ContainsKey(nPacketID))
			{
				return false;
			}
			m_recentAssemblyingPacketIDs.Add(nPacketID, 0);
			return true;
		}

		public void UpdateUnreliableMessagingLossRatioVars(int nPacketID)
		{
			if (nPacketID < m_nMinPacketIDValue)
			{
				m_nMinPacketIDValue = nPacketID;
			}
			if (nPacketID > m_nMaxPacketIDValue)
			{
				m_nMaxPacketIDValue = nPacketID;
			}
			m_hasReceivedUnreliablePacket = true;
		}

		public int GetUnreliableMessagingLossRatioPercent()
		{
			if (!m_hasReceivedUnreliablePacket)
			{
				return 0;
			}
			int num = m_nMaxPacketIDValue - m_nMinPacketIDValue + 1;
			if (num < 0)
			{
				num = m_nMinPacketIDValue - m_nMaxPacketIDValue + 1;
			}
			if (num < 30)
			{
				return 0;
			}
			int val = 100 - 100 * m_recentAssemblyingPacketIDs.Count / num;
			val = Math.Min(val, 100);
			return Math.Max(val, 0);
		}

		public void ResetUnreliableMessagingLossRatioVars()
		{
			m_recentAssemblyingPacketIDs.Clear();
			m_nMinPacketIDValue = int.MaxValue;
			m_nMaxPacketIDValue = int.MinValue;
			m_hasReceivedUnreliablePacket = false;
		}
	}
}

using System.Collections.Generic;

namespace Nettention.Proud
{
	internal class CompressedRelayDestList_C
	{
		internal Dictionary<HostID, P2PGroupSubset_C> p2pGroupList = new Dictionary<HostID, P2PGroupSubset_C>();

		internal HostIDArray includeeHostIDList = new HostIDArray();

		internal int AllHostIDCount
		{
			get
			{
				int num = p2pGroupList.Count;
				foreach (P2PGroupSubset_C value in p2pGroupList.Values)
				{
					num += value.excludeeHostIDList.Count;
				}
				return num + includeeHostIDList.Count;
			}
		}

		internal void AddSubset(HostIDArray subsetGroupHostID, HostID hostID)
		{
			for (int i = 0; i < subsetGroupHostID.Count; i++)
			{
				P2PGroupSubset_C p2PGroupSubset_C;
				if (!p2pGroupList.ContainsKey(subsetGroupHostID[i]))
				{
					p2PGroupSubset_C = new P2PGroupSubset_C();
					p2pGroupList.Add(subsetGroupHostID[i], p2PGroupSubset_C);
				}
				else
				{
					p2PGroupSubset_C = p2pGroupList[subsetGroupHostID[i]];
				}
				if (hostID != HostID.None)
				{
					p2PGroupSubset_C.excludeeHostIDList.Add(hostID);
				}
			}
		}

		internal void AddIndividual(HostID hostID)
		{
			includeeHostIDList.Add(hostID);
		}
	}
}

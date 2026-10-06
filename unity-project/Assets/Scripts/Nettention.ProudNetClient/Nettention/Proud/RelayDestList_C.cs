namespace Nettention.Proud
{
	internal class RelayDestList_C : FastArray<RelayDest_C>
	{
		internal void ToSerializable(out RelayDestList ret)
		{
			ret = new RelayDestList();
			RelayDest value = default(RelayDest);
			for (int i = 0; i < base.Count; i++)
			{
				RelayDest_C relayDest_C = base[i];
				value.frameNumber = relayDest_C.frameNumber;
				value.sendTo = relayDest_C.remotePeer.peerHostID;
				ret.Add(value);
			}
		}
	}
}

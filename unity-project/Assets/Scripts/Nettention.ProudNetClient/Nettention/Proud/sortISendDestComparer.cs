using System.Collections.Generic;

namespace Nettention.Proud
{
	internal class sortISendDestComparer : IComparer<ISendDest_C>
	{
		public int Compare(ISendDest_C a, ISendDest_C b)
		{
			if (a.SendDestHostID > b.SendDestHostID)
			{
				return 1;
			}
			if (a.SendDestHostID == b.SendDestHostID)
			{
				return 0;
			}
			return -1;
		}
	}
}

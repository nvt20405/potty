using System.Collections.Generic;

namespace Nettention.Proud
{
	internal class sortHostIDComparer : IComparer<HostID>
	{
		public int Compare(HostID a, HostID b)
		{
			if (a > b)
			{
				return 1;
			}
			if (a == b)
			{
				return 0;
			}
			return -1;
		}
	}
}

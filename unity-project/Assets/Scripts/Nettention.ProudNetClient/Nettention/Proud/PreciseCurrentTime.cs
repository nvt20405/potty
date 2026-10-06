using System;

namespace Nettention.Proud
{
	public class PreciseCurrentTime
	{
		private static readonly object timeCritSec = new object();

		private static long baseTime = 0L;

		public static long GetTimeMs()
		{
			lock (timeCritSec)
			{
				long ticks = DateTime.Now.Ticks;
				if (baseTime == 0)
				{
					baseTime = ticks;
				}
				return (ticks - baseTime) / 10000;
			}
		}
	}
}

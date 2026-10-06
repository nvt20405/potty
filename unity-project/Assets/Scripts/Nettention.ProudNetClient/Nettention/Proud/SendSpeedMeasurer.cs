namespace Nettention.Proud
{
	internal class SendSpeedMeasurer
	{
		private long recentSpeed;

		private long lastIntervalTotalBytes;

		private long lastLongIntervalWorkTime;

		private long lastAccumulateTime;

		public long RecentSpeed
		{
			get
			{
				return recentSpeed;
			}
		}

		public bool IsRemovingSafeForCalcSpeed(long currentTime)
		{
			return currentTime - lastAccumulateTime > NetConfig.UdpPacketBoardLongIntervalMs * 3;
		}

		public void TouchFirstTime(long currentTime)
		{
			lastAccumulateTime = currentTime;
		}

		public void Accumulate(int byteCount, long currentTime)
		{
			lastIntervalTotalBytes += byteCount;
			lastAccumulateTime = currentTime;
		}

		public void DoForLongInterval(long currentTime)
		{
			if (lastLongIntervalWorkTime == 0)
			{
				lastLongIntervalWorkTime = currentTime;
			}
			long num = currentTime - lastLongIntervalWorkTime;
			if (num != 0 && num > NetConfig.UdpPacketBoardLongIntervalMs * 2 / 10)
			{
				long v = lastIntervalTotalBytes / num;
				recentSpeed = Sysutil.LerpInt(recentSpeed, v, 7L, 10L);
				lastIntervalTotalBytes = 0L;
				lastLongIntervalWorkTime = currentTime;
			}
		}
	}
}

namespace Nettention.Proud
{
	internal class SendBrake
	{
		private long lastClearTime;

		private int totalBytes;

		private long m_lastAccumulatedTime;

		public bool BrakeNeeded(long currTimeMs, long maxSendSpeed)
		{
			if (!NetConfig.EnableSendBrake)
			{
				return false;
			}
			if (m_lastAccumulatedTime == currTimeMs)
			{
				return false;
			}
			if (lastClearTime == 0)
			{
				lastClearTime = currTimeMs;
				return false;
			}
			if (totalBytes >= NetConfig.MinSendSpeed)
			{
				return totalBytes > (int)((currTimeMs - lastClearTime) * maxSendSpeed);
			}
			return false;
		}

		public void Accumulate(int byteLength, long currTime)
		{
			m_lastAccumulatedTime = currTime;
			totalBytes += byteLength;
		}

		public void DoForLongInterval(long currTime)
		{
			long num = currTime - lastClearTime;
			if (num > 0)
			{
				lastClearTime = currTime;
				totalBytes = 0;
			}
		}

		public int GetMaxSendAmount(long currTime, double maxSendSpeed)
		{
			if (lastClearTime == 0)
			{
				lastClearTime = currTime;
			}
			return 104857600;
		}
	}
}

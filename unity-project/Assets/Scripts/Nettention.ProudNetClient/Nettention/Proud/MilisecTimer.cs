namespace Nettention.Proud
{
	internal class MilisecTimer
	{
		private const long MiliSecPerTick = 10000L;

		private bool timerStopped = true;

		private long stopTime;

		private long lastElapsedTime;

		private long baseTime;

		public long TimeMs
		{
			get
			{
				if (stopTime != 0)
				{
					return stopTime;
				}
				return GetPreciseCurrentTimeMs() - baseTime;
			}
		}

		public bool IsStopped
		{
			get
			{
				return timerStopped;
			}
		}

		public void Reset()
		{
			long preciseCurrentTimeMs;
			if (stopTime != 0)
			{
				preciseCurrentTimeMs = stopTime;
				stopTime = 0L;
			}
			else
			{
				preciseCurrentTimeMs = GetPreciseCurrentTimeMs();
			}
			baseTime = preciseCurrentTimeMs;
			lastElapsedTime = preciseCurrentTimeMs;
			timerStopped = false;
		}

		public void Start()
		{
			long preciseCurrentTimeMs = GetPreciseCurrentTimeMs();
			if (timerStopped)
			{
				baseTime += preciseCurrentTimeMs - stopTime;
			}
			stopTime = 0L;
			lastElapsedTime = preciseCurrentTimeMs;
			timerStopped = false;
		}

		public void Stop()
		{
			if (!timerStopped)
			{
				lastElapsedTime = (stopTime = ((stopTime == 0) ? GetPreciseCurrentTimeMs() : stopTime));
				timerStopped = true;
			}
		}

		private long GetPreciseCurrentTimeMs()
		{
			return PreciseCurrentTime.GetTimeMs();
		}
	}
}

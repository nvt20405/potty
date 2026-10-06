namespace Nettention.Proud
{
	internal class TimeAlarm
	{
		private long timeToDo;

		private long interval;

		public long Interval
		{
			set
			{
				if (value <= 0)
				{
					Sysutil.ThrowInvalidArgumentException();
				}
				interval = value;
			}
		}

		public TimeAlarm(long alarmInterval)
		{
			interval = alarmInterval;
		}

		public bool IsTimeToDo(long currentTime)
		{
			if (timeToDo == 0)
			{
				timeToDo = currentTime + interval * 3 / 10;
				return false;
			}
			if (currentTime >= timeToDo)
			{
				timeToDo = currentTime + interval;
				return true;
			}
			return false;
		}

		public void Reset(long currentTime)
		{
			timeToDo = currentTime + interval * 3 / 10;
		}
	}
}

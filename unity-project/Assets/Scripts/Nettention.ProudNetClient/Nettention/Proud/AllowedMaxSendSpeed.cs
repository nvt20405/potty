using System;

namespace Nettention.Proud
{
	internal class AllowedMaxSendSpeed
	{
		private static readonly long VeryLargeValue = 2146483647L;

		private long _value = VeryLargeValue;

		public long Value
		{
			get
			{
				return _value;
			}
		}

		public void DoForLongInterval(long recentSendSpeed, long recentReceiveSpeed)
		{
			if (recentReceiveSpeed == 0)
			{
				recentReceiveSpeed = VeryLargeValue;
			}
			if (recentSendSpeed * 7 / 10 > recentReceiveSpeed)
			{
				_value = recentReceiveSpeed * 8 / 10;
			}
			else
			{
				_value *= 2L;
				_value = Math.Min(_value, VeryLargeValue);
			}
			_value = Math.Max(_value, NetConfig.MinSendSpeed);
		}
	}
}

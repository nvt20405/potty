namespace Nettention.Proud
{
	internal class RecentReceiveSpeedAtReceiverSide
	{
		private long _value;

		private long lastClearTime;

		public long Value
		{
			get
			{
				return _value;
			}
		}

		public void SetValue(long newValue, long currTime)
		{
			_value = newValue;
			lastClearTime = currTime;
		}

		public void DoForLongInterval(long currentTime)
		{
			if (lastClearTime == 0)
			{
				lastClearTime = currentTime;
			}
			if (currentTime - lastClearTime > NetConfig.UdpPacketBoardLongIntervalMs * 3)
			{
				lastClearTime = currentTime;
				_value = 0L;
			}
		}
	}
}

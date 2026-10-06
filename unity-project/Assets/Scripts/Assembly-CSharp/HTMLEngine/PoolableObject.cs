using System;

namespace HTMLEngine
{
	public abstract class PoolableObject : IDisposable
	{
		private ObjectPoolHandler _handler;

		internal void SetPoolHandler(ObjectPoolHandler handler)
		{
			_handler = handler;
		}

		internal abstract void OnAcquire();

		internal abstract void OnRelease();

		public void Dispose()
		{
			if (_handler != null)
			{
				_handler(this);
			}
		}

		~PoolableObject()
		{
			if (_handler != null)
			{
				Type type = GetType();
				HtEngine.Log(HtLogLevel.Warning, "Poolable object is not disposed: " + ((type != null) ? type.ToString() : null));
			}
		}
	}
}

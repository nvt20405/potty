using System.Collections.Generic;

namespace HTMLEngine
{
	public sealed class OP<T> where T : PoolableObject, new()
	{
		public static readonly OP<T> Instance = new OP<T>(16);

		private readonly Queue<T> _pool;

		private int _capacity;

		private readonly ObjectPoolHandler _returnHandler;

		public int Count
		{
			get
			{
				return _pool.Count;
			}
		}

		private OP(int capacity)
		{
			if (capacity < 1)
			{
				capacity = 1;
			}
			_returnHandler = ReturnObject;
			_pool = new Queue<T>();
			for (int i = 0; i < capacity; i++)
			{
				T item = CreateInstance();
				_pool.Enqueue(item);
			}
			_capacity = capacity;
		}

		private static T CreateInstance()
		{
			return new T();
		}

		public static T Acquire()
		{
			return Instance.AcquireInternal();
		}

		private T AcquireInternal()
		{
			if (_pool.Count == 0)
			{
				for (int i = 0; i < _capacity; i++)
				{
					T item = CreateInstance();
					_pool.Enqueue(item);
				}
				_capacity *= 2;
			}
			T val = _pool.Dequeue();
			val.SetPoolHandler(_returnHandler);
			val.OnAcquire();
			return val;
		}

		private void ReturnObject(PoolableObject obj)
		{
			obj.SetPoolHandler(null);
			obj.OnRelease();
			_pool.Enqueue((T)obj);
		}
	}
}

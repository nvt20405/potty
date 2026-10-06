using System.Collections.Generic;

namespace HTMLEngine
{
	internal class PDictionary<K, V> : PoolableObject
	{
		private readonly Dictionary<K, V> dict = new Dictionary<K, V>();

		public int Count
		{
			get
			{
				return dict.Count;
			}
		}

		public IEnumerable<K> Keys
		{
			get
			{
				return dict.Keys;
			}
		}

		public IEnumerable<V> Values
		{
			get
			{
				return dict.Values;
			}
		}

		public V this[K key]
		{
			get
			{
				V value;
				return (!dict.TryGetValue(key, out value)) ? default(V) : value;
			}
			set
			{
				dict[key] = value;
			}
		}

		internal override void OnAcquire()
		{
		}

		internal override void OnRelease()
		{
			dict.Clear();
		}

		public void Clear()
		{
			dict.Clear();
		}
	}
}

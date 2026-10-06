using System.Collections.Generic;

namespace HTMLEngine
{
	internal class PIgnoreCaseDictionary<V> : PoolableObject
	{
		private readonly Dictionary<string, V> dict = new Dictionary<string, V>();

		public int Count
		{
			get
			{
				return dict.Count;
			}
		}

		public IEnumerable<string> Keys
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

		public V this[string key]
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

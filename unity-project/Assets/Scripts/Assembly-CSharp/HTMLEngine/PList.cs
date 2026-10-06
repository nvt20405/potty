using System.Collections.Generic;

namespace HTMLEngine
{
	internal class PList<T> : PoolableObject
	{
		protected readonly List<T> list = new List<T>();

		public int Count
		{
			get
			{
				return list.Count;
			}
		}

		public IEnumerable<T> Items
		{
			get
			{
				return list;
			}
		}

		public T this[int index]
		{
			get
			{
				return list[index];
			}
			set
			{
				list[index] = value;
			}
		}

		internal override void OnAcquire()
		{
		}

		internal override void OnRelease()
		{
			list.Clear();
		}

		public void Add(T value)
		{
			list.Add(value);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		public override string ToString()
		{
			using (PStringBuilder pStringBuilder = OP<PStringBuilder>.Acquire())
			{
				for (int i = 0; i < list.Count; i++)
				{
					T val = list[i];
					pStringBuilder.Append('[');
					pStringBuilder.Append((!object.Equals(val, default(T))) ? val.ToString() : "null");
					pStringBuilder.Append(']');
					if (i < list.Count - 1)
					{
						pStringBuilder.Append(',');
					}
				}
				return pStringBuilder.ToString();
			}
		}
	}
}

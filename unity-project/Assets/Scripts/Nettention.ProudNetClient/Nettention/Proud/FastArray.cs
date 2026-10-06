using System;

namespace Nettention.Proud
{
	public class FastArray<T> : ICloneable
	{
		public T[] data;

		private int length;

		private int capacity;

		private int minCapacity;

		private eGrowPolicy growPolicy;

		public T this[int index]
		{
			get
			{
				BoundCheck(index);
				return data[index];
			}
			set
			{
				BoundCheck(index);
				data[index] = value;
			}
		}

		public int Count
		{
			get
			{
				return length;
			}
			set
			{
				SetCount(value);
			}
		}

		public eGrowPolicy GrowPolicy
		{
			get
			{
				return growPolicy;
			}
			set
			{
				growPolicy = value;
			}
		}

		object ICloneable.Clone()
		{
			return Clone();
		}

		public FastArray<T> Clone()
		{
			return (FastArray<T>)MemberwiseClone();
		}

		protected void InitVars()
		{
			data = null;
			length = 0;
			capacity = 0;
			minCapacity = 0;
		}

		public void Clear()
		{
			Count = 0;
		}

		protected void BoundCheck(int index)
		{
			if (index < 0 || index >= Count)
			{
				Sysutil.ThrowArrayOutOfBoundException();
			}
		}

		public void AddRange(T[] data)
		{
			if (data == null || data.Length < 0)
			{
				Sysutil.ThrowInvalidArgumentException();
			}
			if (data.Length != 0)
			{
				int count = Count;
				SetCount(Count + data.Length);
				int destinationIndex = count;
				Array.Copy(data, 0, this.data, destinationIndex, data.Length);
			}
		}

		public void AddRange(T[] data, int dataLength)
		{
			if (data == null || data.Length < 0 || dataLength < 0 || data.Length < dataLength)
			{
				Sysutil.ThrowInvalidArgumentException();
			}
			if (dataLength != 0)
			{
				int count = Count;
				SetCount(Count + dataLength);
				int destinationIndex = count;
				Array.Copy(data, 0, this.data, destinationIndex, dataLength);
			}
		}

		public void AddRange(T[] data, int offset, int dataLength)
		{
			if (data == null || data.Length < 0 || dataLength < 0 || data.Length < dataLength || offset > data.Length)
			{
				Sysutil.ThrowInvalidArgumentException();
			}
			if (dataLength != 0)
			{
				int count = Count;
				SetCount(Count + dataLength);
				int destinationIndex = count;
				Array.Copy(data, offset, this.data, destinationIndex, dataLength);
			}
		}

		public void Add(T value)
		{
			Insert(Count, value);
		}

		public void RemoveAt(int index)
		{
			BoundCheck(index);
			for (int i = index; i < length; i++)
			{
				data[i] = data[i + 1];
			}
			if (!typeof(T).IsValueType)
			{
				data[length] = default(T);
			}
			SetCount(length - 1);
		}

		public void Insert(int indexAt, T value)
		{
			InsertRange(indexAt, value);
		}

		public void InsertRange(int indexAt, T data)
		{
			if (data == null || indexAt > Count || indexAt < 0)
			{
				Sysutil.ThrowInvalidArgumentException();
			}
			int count = Count;
			SetCount(Count + 1);
			int num = count - indexAt;
			if (num > 0)
			{
				for (int num2 = num - 1; num2 >= 0; num2--)
				{
					this.data[num2 + indexAt + 1] = this.data[num2 + indexAt];
				}
			}
			this.data[indexAt] = data;
		}

		public void InsertRange(int indexAt, T[] data)
		{
			if (data == null || indexAt > Count || indexAt < 0)
			{
				Sysutil.ThrowInvalidArgumentException();
			}
			int count = Count;
			SetCount(Count + data.Length);
			int num = count - indexAt;
			if (num > 0)
			{
				for (int num2 = num - 1; num2 >= 0; num2--)
				{
					this.data[num2 + indexAt + 1] = this.data[num2 + indexAt];
				}
			}
			Array.Copy(data, 0, this.data, indexAt, data.Length);
		}

		private int GetRecommendedCapacity(int actualCount)
		{
			switch (growPolicy)
			{
			case eGrowPolicy.Normal:
			{
				int val2 = length / 8;
				val2 = Math.Min(val2, 1024);
				val2 = Math.Max(val2, 4);
				return Math.Max(minCapacity, actualCount + val2);
			}
			case eGrowPolicy.HighSpeed:
			{
				int val = length / 8;
				val = Math.Max(val, 16);
				val = Math.Max(val, 64);
				return Math.Max(minCapacity, actualCount + val);
			}
			case eGrowPolicy.LowMemory:
				return Math.Max(minCapacity, actualCount);
			default:
				Sysutil.ThrowInvalidArgumentException();
				return -1;
			}
		}

		public void SetCount(int newVal)
		{
			if (newVal < 0)
			{
				Sysutil.ThrowInvalidArgumentException();
			}
			if (newVal == length)
			{
				return;
			}
			if (newVal > capacity)
			{
				int recommendedCapacity = GetRecommendedCapacity(newVal);
				if (capacity == 0)
				{
					data = new T[recommendedCapacity];
				}
				else
				{
					Array.Resize(ref data, recommendedCapacity);
				}
				capacity = recommendedCapacity;
			}
			length = newVal;
		}

		public void UseExternalBuffer(ref T[] buffer, int capacity)
		{
			if (data != null)
			{
				throw new Exception("FastArray.UseExternalBuffer");
			}
			if (buffer != null && capacity != 0)
			{
				this.capacity = capacity;
				data = buffer;
				length = 0;
			}
		}
	}
}

using System;

namespace Nettention.Proud
{
	internal class CompressedFrameNumbers : ICloneable
	{
		private struct Elem
		{
			internal FrameNumber left;

			internal FrameNumber right;
		}

		private FastArray<Elem> array = new FastArray<Elem>();

		public int Count
		{
			get
			{
				return array.Count;
			}
		}

		public void AddSortedNumber(FrameNumber n)
		{
			if (array.Count == 0)
			{
				Elem value = default(Elem);
				value.right = n;
				value.left = n;
				array.Add(value);
				return;
			}
			Elem value2 = array[array.Count - 1];
			if (value2.left != n && value2.right != n)
			{
				if (FrameNumberUtil.Adjucent(value2.right, n))
				{
					value2.right = n;
					array[array.Count - 1] = value2;
				}
				else if (FrameNumberUtil.Adjucent(value2.left, n))
				{
					value2.right = n;
					array[array.Count - 1] = value2;
				}
				else
				{
					Elem value3 = default(Elem);
					value3.right = n;
					value3.left = n;
					array.Add(value3);
				}
			}
		}

		public void Uncompress(ref UncompressedFrameNumberArray dest)
		{
			dest.Clear();
			for (int i = 0; i < array.Count; i++)
			{
				Elem elem = array[i];
				while (true)
				{
					dest.Add(elem.left);
					if (elem.left == elem.right)
					{
						break;
					}
					elem.left = FrameNumberUtil.NextFrameNumber(elem.left);
				}
			}
		}

		public bool ReadFrom(ref Message msg)
		{
			int a = 0;
			if (!msg.ReadScalar(ref a))
			{
				return false;
			}
			array.Count = a;
			for (int i = 0; i < array.Count; i++)
			{
				Elem value = array[i];
				sbyte b = 0;
				if (!msg.Read(out b))
				{
					return false;
				}
				if (b == 0)
				{
					if (!msg.Read(out value.left))
					{
						return false;
					}
					value.right = value.left;
					array[i] = value;
					continue;
				}
				if (!msg.Read(out value.left))
				{
					return false;
				}
				if (!msg.Read(out value.right))
				{
					return false;
				}
				array[i] = value;
			}
			return true;
		}

		public void WriteTo(ref Message msg)
		{
			int count = array.Count;
			msg.WriteScalar(count);
			for (int i = 0; i < array.Count; i++)
			{
				Elem elem = array[i];
				if (elem.left == elem.right)
				{
					msg.Write(0);
					msg.Write(elem.left);
				}
				else
				{
					msg.Write(1);
					msg.Write(elem.left);
					msg.Write(elem.right);
				}
			}
		}

		object ICloneable.Clone()
		{
			return Clone();
		}

		public CompressedFrameNumbers Clone()
		{
			return (CompressedFrameNumbers)MemberwiseClone();
		}
	}
}

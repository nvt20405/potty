using System;
using System.Collections.Generic;

namespace Nettention.Proud
{
	internal class Sysutil
	{
		public static readonly double PROUDNET_PI = Math.PI;

		public static void ShowUserMisuseError(string text)
		{
			if (NetConfig.UserMisuseErrorReaction != ErrorReaction.Assert)
			{
				object obj = null;
				int num = (int)obj;
			}
		}

		public static double Lerp(double v1, double v2, double ratio)
		{
			return v1 + (v2 - v1) * ratio;
		}

		public static long LerpInt(long v1, long v2, long ratio1, long ratio2)
		{
			return v1 + (v2 - v1) * ratio1 / ratio2;
		}

		public static void Swap<T>(ref T lhs, ref T rhs)
		{
			T val = lhs;
			lhs = rhs;
			rhs = val;
		}

		public static bool IsCombinationEmpty<T>(T min1, T max1, T min2, T max2) where T : IComparable<T>
		{
			if (min1.CompareTo(max1) > 0)
			{
				Swap(ref min1, ref max1);
			}
			if (min2.CompareTo(max2) > 0)
			{
				Swap(ref min2, ref max2);
			}
			if ((min2.CompareTo(min1) > 0 || max1.CompareTo(max2) > 0) && (min1.CompareTo(min2) > 0 || max2.CompareTo(max1) > 0) && (min1.CompareTo(min2) > 0 || min2.CompareTo(max1) > 0 || max1.CompareTo(max2) > 0))
			{
				if (min2.CompareTo(min1) <= 0 && min1.CompareTo(max2) <= 0)
				{
					return max2.CompareTo(max1) > 0;
				}
				return true;
			}
			return false;
		}

		public static void ThrowInvalidArgumentException()
		{
			throw new Exception("An Invalid argument is detected!");
		}

		public static void ThrowArrayOutOfBoundException()
		{
			throw new Exception("Array index out of range!");
		}

		internal static List<T> UnionDuplicates<T>(List<T> obj, IComparer<T> comparer)
		{
			obj.Sort(comparer);
			List<T> list = new List<T>();
			int num = 0;
			int count = obj.Count;
			for (int i = 0; i < count; i++)
			{
				if (obj[i] != null && (num == 0 || comparer.Compare(obj[i], list[num - 1]) != 0))
				{
					list.Add(obj[i]);
					num++;
				}
			}
			return list;
		}
	}
}

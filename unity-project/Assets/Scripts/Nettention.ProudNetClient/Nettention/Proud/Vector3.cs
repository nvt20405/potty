using System;

namespace Nettention.Proud
{
	public struct Vector3
	{
		public double x;

		public double y;

		public double z;

		public Vector3 Normal
		{
			get
			{
				double length = Length;
				if (length != 0.0)
				{
					return this / Length;
				}
				return From(0.0, 0.0, 0.0);
			}
		}

		public double LengthSq
		{
			get
			{
				return x * x + y * y + z * z;
			}
		}

		public double Length
		{
			get
			{
				return Math.Sqrt(x * x + y * y + z * z);
			}
			set
			{
				Vector3 normal = Normal;
				normal *= value;
				this = normal;
			}
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static Vector3 From(double x, double y, double z)
		{
			return new Vector3
			{
				x = x,
				y = y,
				z = z
			};
		}

		private static Vector3 From(Vector3 src)
		{
			return From(src.x, src.y, src.z);
		}

		public static Vector3 operator +(Vector3 c1, Vector3 c2)
		{
			return new Vector3
			{
				x = c1.x + c2.x,
				y = c1.y + c2.y,
				z = c1.z + c2.z
			};
		}

		public static Vector3 operator -(Vector3 c1, Vector3 c2)
		{
			return new Vector3
			{
				x = c1.x - c2.x,
				y = c1.y - c2.y,
				z = c1.z - c2.z
			};
		}

		public static Vector3 operator *(Vector3 c1, double scale)
		{
			Vector3 result = From(c1);
			result.x *= scale;
			result.y *= scale;
			result.z *= scale;
			return result;
		}

		public static Vector3 operator /(Vector3 c1, double scale)
		{
			Vector3 result = From(c1);
			result.x /= scale;
			result.y /= scale;
			result.z /= scale;
			return result;
		}

		public static Vector3 operator -(Vector3 c1)
		{
			return new Vector3
			{
				x = 0.0 - c1.x,
				y = 0.0 - c1.y,
				z = 0.0 - c1.z
			};
		}

		public static bool operator ==(Vector3 lhs, Vector3 rhs)
		{
			if (lhs.x == rhs.x && lhs.y == rhs.y)
			{
				return lhs.z == rhs.z;
			}
			return false;
		}

		public static bool operator !=(Vector3 lhs, Vector3 rhs)
		{
			if (lhs.x == rhs.x && lhs.y == rhs.y)
			{
				return lhs.z != rhs.z;
			}
			return true;
		}

		public static Vector3 Lerp(Vector3 a, Vector3 b, double ratio)
		{
			return new Vector3
			{
				x = Sysutil.Lerp(a.x, b.x, ratio),
				y = Sysutil.Lerp(a.y, b.y, ratio),
				z = Sysutil.Lerp(a.z, b.z, ratio)
			};
		}

		public static double Dot(Vector3 a, Vector3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}
	}
}

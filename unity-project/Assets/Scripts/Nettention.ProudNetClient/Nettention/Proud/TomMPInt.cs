using System;

namespace Nettention.Proud
{
	internal class TomMPInt
	{
		public delegate bool reduxDelegate(TomMPInt x, TomMPInt n, ref uint rho);

		public delegate bool redux2Delegate(TomMPInt x, TomMPInt m, TomMPInt mu);

		public static readonly int MP_DIGIT_BIT = 28;

		public static readonly uint MP_MASK = (uint)((1 << MP_DIGIT_BIT) - 1);

		public static readonly uint MP_DIGIT_MAX = MP_MASK;

		public static readonly int CHAR_BIT = 8;

		public static readonly int MP_DIGIT_SIZE = 4;

		public static readonly int MP_WORD_BYTE_SIZE = 8;

		public static readonly int MP_PREC = 32;

		public static readonly int MP_LT = -1;

		public static readonly int MP_EQ = 0;

		public static readonly int MP_GT = 1;

		public static readonly int MP_ZPOS = 0;

		public static readonly int MP_NEG = 1;

		public static readonly int MP_OKAY = 0;

		public static readonly int MP_MEM = -2;

		public static readonly int MP_VAL = -3;

		public static readonly int MP_RANGE = MP_VAL;

		public static readonly int MP_YES = 1;

		public static readonly int MP_NO = 0;

		public static readonly int TOOM_MUL_CUTOFF = 350;

		public static readonly int MP_WARRAY = 1 << MP_WORD_BYTE_SIZE * CHAR_BIT - 2 * MP_DIGIT_BIT + 1;

		public static readonly int MP_TAB_SIZE = 256;

		public int _used;

		public int _alloc;

		public int _sign;

		public uint[] _dp;

		public bool init()
		{
			_dp = new uint[MP_PREC];
			Array.Clear(_dp, 0, _dp.Length);
			_used = 0;
			_alloc = MP_PREC;
			_sign = MP_ZPOS;
			return true;
		}

		public void clear()
		{
			if (_dp != null)
			{
				for (int i = 0; i < _used; i++)
				{
					_dp[i] = 0u;
				}
				_dp = null;
				_alloc = 0;
				_used = 0;
				_sign = MP_ZPOS;
			}
		}

		public static void zero(TomMPInt a)
		{
			a._sign = MP_ZPOS;
			a._used = 0;
			for (int i = 0; i < a._alloc; i++)
			{
				a._dp[i] = 0u;
			}
		}

		public static bool grow(TomMPInt a, int size)
		{
			if (a._alloc < size)
			{
				size += MP_PREC * 2 - size % MP_PREC;
				Array.Resize(ref a._dp, size);
				int i = a._alloc;
				a._alloc = size;
				for (; i < a._alloc; i++)
				{
					a._dp[i] = 0u;
				}
			}
			return true;
		}

		public static void clamp(TomMPInt a)
		{
			while (a._used > 0 && a._dp[a._used - 1] == 0)
			{
				a._used--;
			}
			if (a._used == 0)
			{
				a._sign = MP_ZPOS;
			}
		}

		public static bool leftShiftDigit(TomMPInt a, int b)
		{
			if (b <= 0)
			{
				return true;
			}
			if (a._alloc < a._used + b && !grow(a, a._used + b))
			{
				return false;
			}
			a._used += b;
			for (int num = a._used - 1; num >= b; num--)
			{
				a._dp[num] = a._dp[num - b];
			}
			for (int i = 0; i < b; i++)
			{
				a._dp[i] = 0u;
			}
			return true;
		}

		public static void rightShiftDigit(TomMPInt a, int b)
		{
			if (b <= 0)
			{
				return;
			}
			if (a._used <= b)
			{
				zero(a);
				return;
			}
			int num = b;
			int i;
			for (i = 0; i < a._used - b; i++)
			{
				a._dp[i] = a._dp[num++];
			}
			for (; i < a._used; i++)
			{
				a._dp[i] = 0u;
			}
			a._used -= b;
		}

		public static bool copy(TomMPInt a, TomMPInt b)
		{
			if (a == b)
			{
				return true;
			}
			if (b._alloc < a._used && !grow(b, a._used))
			{
				return false;
			}
			int i;
			for (i = 0; i < a._used; i++)
			{
				b._dp[i] = a._dp[i];
			}
			for (; i < b._used; i++)
			{
				b._dp[i] = 0u;
			}
			b._used = a._used;
			b._sign = a._sign;
			return true;
		}

		public static bool mul2D(TomMPInt a, int b, TomMPInt c)
		{
			if (a != c && !copy(a, c))
			{
				return false;
			}
			if (c._alloc < c._used + b / MP_DIGIT_BIT + 1 && !grow(c, c._used + b / MP_DIGIT_BIT + 1))
			{
				return false;
			}
			if (b >= MP_DIGIT_BIT && !leftShiftDigit(c, b / MP_DIGIT_BIT))
			{
				return false;
			}
			uint num = (uint)(b % MP_DIGIT_BIT);
			if (num != 0)
			{
				uint num2 = (uint)((1 << (int)num) - 1);
				uint num3 = (uint)MP_DIGIT_BIT - num;
				uint num4 = 0u;
				for (int i = 0; i < c._used; i++)
				{
					uint num5 = (c._dp[i] >> (int)num3) & num2;
					c._dp[i] = ((c._dp[i] << (int)num) | num4) & MP_MASK;
					num4 = num5;
				}
				if (num4 != 0)
				{
					c._dp[c._used++] = num4;
				}
			}
			clamp(c);
			return true;
		}

		public static bool readUnsignedBin(TomMPInt a, byte[] b, int c, uint position)
		{
			if (a._alloc < 2 && !grow(a, 2))
			{
				return false;
			}
			zero(a);
			int num = 0;
			while (c-- > 0)
			{
				if (!mul2D(a, 8, a))
				{
					return false;
				}
				checked
				{
					a._dp[0] |= b[(int)(IntPtr)(long)unchecked((ulong)(position + num++))];
				}
				a._used++;
			}
			clamp(a);
			return true;
		}

		public static bool toUnsignedBin(TomMPInt a, byte[] b, int position)
		{
			TomMPInt tomMPInt = new TomMPInt();
			tomMPInt.init();
			if (!copy(a, tomMPInt))
			{
				return false;
			}
			int len = 0;
			while (isZero(tomMPInt) == 0)
			{
				b[position + len++] = (byte)(tomMPInt._dp[0] & 0xFF);
				if (!div2D(tomMPInt, 8, tomMPInt, null))
				{
					return false;
				}
			}
			reverse(b, len, position);
			return true;
		}

		public static void reverse(byte[] s, int len, int offset)
		{
			int num = 0;
			int num2 = len - 1;
			while (num < num2)
			{
				byte b = s[num + offset];
				s[num + offset] = s[num2 + offset];
				s[num2 + offset] = b;
				num++;
				num2--;
			}
		}

		public static bool twoExpt(TomMPInt a, int b)
		{
			zero(a);
			if (!grow(a, b / MP_DIGIT_BIT + 1))
			{
				return false;
			}
			a._used = b / MP_DIGIT_BIT + 1;
			a._dp[b / MP_DIGIT_BIT] = (uint)(1 << b % MP_DIGIT_BIT);
			return true;
		}

		public static bool sub(TomMPInt a, TomMPInt b, TomMPInt c)
		{
			int sign = a._sign;
			int sign2 = b._sign;
			if (sign != sign2)
			{
				c._sign = sign;
				if (!sAdd(a, b, c))
				{
					return false;
				}
			}
			else if (cmpMag(a, b) != MP_LT)
			{
				c._sign = sign;
				if (!sSub(a, b, c))
				{
					return false;
				}
			}
			else
			{
				c._sign = ((sign == MP_ZPOS) ? MP_NEG : MP_ZPOS);
				if (!sSub(b, a, c))
				{
					return false;
				}
			}
			return true;
		}

		public static bool sAdd(TomMPInt a, TomMPInt b, TomMPInt c)
		{
			int used;
			int used2;
			TomMPInt tomMPInt;
			if (a._used > b._used)
			{
				used = b._used;
				used2 = a._used;
				tomMPInt = a;
			}
			else
			{
				used = a._used;
				used2 = b._used;
				tomMPInt = b;
			}
			if (c._alloc < used2 + 1 && !grow(c, used2 + 1))
			{
				return false;
			}
			int used3 = c._used;
			c._used = used2 + 1;
			uint num = 0u;
			int i;
			for (i = 0; i < used; i++)
			{
				c._dp[i] = a._dp[i] + b._dp[i] + num;
				num = c._dp[i] >> MP_DIGIT_BIT;
				c._dp[i] &= MP_MASK;
			}
			if (used != used2)
			{
				for (; i < used2; i++)
				{
					c._dp[i] = tomMPInt._dp[i] + num;
					num = c._dp[i] >> MP_DIGIT_BIT;
					c._dp[i] &= MP_MASK;
				}
			}
			c._dp[i++] = num;
			for (int j = c._used; j < used3; j++)
			{
				c._dp[i++] = 0u;
			}
			clamp(c);
			return true;
		}

		public static bool sSub(TomMPInt a, TomMPInt b, TomMPInt c)
		{
			int used = b._used;
			int used2 = a._used;
			if (c._alloc < used2 && !grow(c, used2))
			{
				return false;
			}
			int used3 = c._used;
			c._used = used2;
			uint num = 0u;
			int i;
			for (i = 0; i < used; i++)
			{
				c._dp[i] = a._dp[i] - b._dp[i] - num;
				num = c._dp[i] >> CHAR_BIT * MP_DIGIT_SIZE - 1;
				c._dp[i] &= MP_MASK;
			}
			for (; i < used2; i++)
			{
				c._dp[i] = a._dp[i] - num;
				num = c._dp[i] >> CHAR_BIT * MP_DIGIT_SIZE - 1;
				c._dp[i] &= MP_MASK;
			}
			for (int j = c._used; j < used3; j++)
			{
				c._dp[i++] = 0u;
			}
			clamp(c);
			return true;
		}

		public static int countBits(TomMPInt a)
		{
			if (a._used == 0)
			{
				return 0;
			}
			int num = (a._used - 1) * MP_DIGIT_BIT;
			for (uint num2 = a._dp[a._used - 1]; num2 != 0; num2 >>= 1)
			{
				num++;
			}
			return num;
		}

		public static int cmpMag(TomMPInt a, TomMPInt b)
		{
			if (a._used > b._used)
			{
				return MP_GT;
			}
			if (a._used < b._used)
			{
				return MP_LT;
			}
			int num = a._used - 1;
			for (int i = 0; i < a._used; i++)
			{
				if (a._dp[num] > b._dp[num])
				{
					return MP_GT;
				}
				if (a._dp[num] < b._dp[num])
				{
					return MP_LT;
				}
				num--;
			}
			return MP_EQ;
		}

		public static int unsignedBinSize(TomMPInt a)
		{
			int num = countBits(a);
			return num / 8 + (((num & 7) != 0) ? 1 : 0);
		}

		public static int cmp(TomMPInt a, TomMPInt b)
		{
			if (a._sign != b._sign)
			{
				if (a._sign == MP_NEG)
				{
					return MP_LT;
				}
				return MP_GT;
			}
			if (a._sign == MP_NEG)
			{
				return cmpMag(b, a);
			}
			return cmpMag(a, b);
		}

		public static bool exptMod(TomMPInt G, TomMPInt X, TomMPInt P, TomMPInt Y)
		{
			if (P._sign == MP_NEG)
			{
				return false;
			}
			if (X._sign == MP_NEG)
			{
				TomMPInt tomMPInt = new TomMPInt();
				TomMPInt tomMPInt2 = new TomMPInt();
				tomMPInt.init();
				if (!invMod(G, P, tomMPInt))
				{
					return false;
				}
				tomMPInt2.init();
				if (abs(X, tomMPInt2))
				{
					return exptMod(tomMPInt, tomMPInt2, P, Y);
				}
				return false;
			}
			if (reduceIs2KL(P))
			{
				return sExptMod(G, X, P, Y, 1);
			}
			int num = drIsModulus(P);
			if (num == 0)
			{
				num = reduceIs2K(P) << 1;
			}
			if (isOdd(P) == 1 || num != 0)
			{
				return exptModFast(G, X, P, Y, num);
			}
			return sExptMod(G, X, P, Y, 0);
		}

		public static bool exptModFast(TomMPInt G, TomMPInt X, TomMPInt P, TomMPInt Y, int redmode)
		{
			TomMPInt[] array = new TomMPInt[MP_TAB_SIZE];
			for (int i = 0; i < MP_TAB_SIZE; i++)
			{
				array[i] = new TomMPInt();
			}
			int num = countBits(X);
			int num2 = ((num <= 7) ? 2 : ((num <= 36) ? 3 : ((num <= 140) ? 4 : ((num <= 450) ? 5 : ((num <= 1303) ? 6 : ((num > 3529) ? 8 : 7))))));
			if (MP_TAB_SIZE < 256)
			{
				num2 = 5;
			}
			array[1].init();
			for (num = 1 << num2 - 1; num < 1 << num2; num++)
			{
				array[num].init();
			}
			uint d = 0u;
			reduxDelegate reduxDelegate2;
			switch (redmode)
			{
			case 0:
				if (!montgomerySetup(P, ref d))
				{
					return false;
				}
				reduxDelegate2 = ((P._used * 2 + 1 >= MP_WARRAY || P._used >= 1 << CHAR_BIT * MP_WORD_BYTE_SIZE - 2 * MP_DIGIT_BIT) ? new reduxDelegate(montgomeryReduce) : new reduxDelegate(fastMontgomeryReduce));
				break;
			case 1:
				drSetup(P, ref d);
				reduxDelegate2 = drReduce;
				break;
			default:
				if (!reduce2KSetup(P, ref d))
				{
					return false;
				}
				reduxDelegate2 = reduce2K;
				break;
			}
			TomMPInt tomMPInt = new TomMPInt();
			tomMPInt.init();
			if (redmode == 0)
			{
				if (!montgomeryCalcNormalization(tomMPInt, P))
				{
					return false;
				}
				if (!mulMod(G, tomMPInt, P, array[1]))
				{
					return false;
				}
			}
			else
			{
				setDigit(tomMPInt, 1u);
				if (!mod(G, P, array[1]))
				{
					return false;
				}
			}
			if (!copy(array[1], array[1 << num2 - 1]))
			{
				return false;
			}
			for (num = 0; num < num2 - 1; num++)
			{
				if (!sqr(array[1 << num2 - 1], array[1 << num2 - 1]))
				{
					return false;
				}
				if (!reduxDelegate2(array[1 << num2 - 1], P, ref d))
				{
					return false;
				}
			}
			for (num = (1 << num2 - 1) + 1; num < 1 << num2; num++)
			{
				if (!mul(array[num - 1], array[1], array[num]))
				{
					return false;
				}
				if (!reduxDelegate2(array[num], P, ref d))
				{
					return false;
				}
			}
			int num3 = 0;
			int num4 = 1;
			uint num5 = 0u;
			int num6 = X._used - 1;
			int num7 = 0;
			int num8 = 0;
			while (true)
			{
				if (--num4 == 0)
				{
					if (num6 == -1)
					{
						break;
					}
					num5 = X._dp[num6--];
					num4 = MP_DIGIT_BIT;
				}
				int num9 = (int)((num5 >> MP_DIGIT_BIT - 1) & 1);
				num5 <<= 1;
				if (num3 == 0 && num9 == 0)
				{
					continue;
				}
				if (num3 == 1 && num9 == 0)
				{
					if (sqr(tomMPInt, tomMPInt))
					{
						if (!reduxDelegate2(tomMPInt, P, ref d))
						{
							return false;
						}
						continue;
					}
					return false;
				}
				num8 |= num9 << num2 - ++num7;
				num3 = 2;
				if (num7 != num2)
				{
					continue;
				}
				for (num = 0; num < num2; num++)
				{
					if (!sqr(tomMPInt, tomMPInt))
					{
						return false;
					}
					if (!reduxDelegate2(tomMPInt, P, ref d))
					{
						return false;
					}
				}
				if (!mul(tomMPInt, array[num8], tomMPInt))
				{
					return false;
				}
				if (!reduxDelegate2(tomMPInt, P, ref d))
				{
					return false;
				}
				num7 = 0;
				num8 = 0;
				num3 = 1;
			}
			if (num3 == 2 && num7 > 0)
			{
				for (num = 0; num < num7; num++)
				{
					if (!sqr(tomMPInt, tomMPInt))
					{
						return false;
					}
					if (!reduxDelegate2(tomMPInt, P, ref d))
					{
						return false;
					}
					num8 <<= 1;
					if ((num8 & (1 << num2)) != 0)
					{
						if (!mul(tomMPInt, array[1], tomMPInt))
						{
							return false;
						}
						if (!reduxDelegate2(tomMPInt, P, ref d))
						{
							return false;
						}
					}
				}
			}
			if (redmode == 0 && !reduxDelegate2(tomMPInt, P, ref d))
			{
				return false;
			}
			exch(tomMPInt, Y);
			return true;
		}

		public static bool mulMod(TomMPInt a, TomMPInt b, TomMPInt c, TomMPInt d)
		{
			TomMPInt tomMPInt = new TomMPInt();
			tomMPInt.init();
			if (mul(a, b, tomMPInt))
			{
				return mod(tomMPInt, c, d);
			}
			return false;
		}

		public static bool montgomeryCalcNormalization(TomMPInt a, TomMPInt b)
		{
			int num = countBits(b) % MP_DIGIT_BIT;
			if (b._used > 1)
			{
				if (!twoExpt(a, (b._used - 1) * MP_DIGIT_BIT + num - 1))
				{
					return false;
				}
			}
			else
			{
				setDigit(a, 1u);
				num = 1;
			}
			for (int i = num - 1; i < MP_DIGIT_BIT; i++)
			{
				if (!mul2(a, a))
				{
					return false;
				}
				if (cmpMag(a, b) != MP_LT && !sSub(a, b, a))
				{
					return false;
				}
			}
			return true;
		}

		public static bool mul2(TomMPInt a, TomMPInt b)
		{
			if (b._alloc < a._used + 1 && !grow(b, a._used + 1))
			{
				return false;
			}
			int used = b._used;
			b._used = a._used;
			uint num = 0u;
			int i;
			for (i = 0; i < a._used; i++)
			{
				uint num2 = a._dp[i] >> MP_DIGIT_BIT - 1;
				b._dp[i] = ((a._dp[i] << 1) | num) & MP_MASK;
				num = num2;
			}
			if (num != 0)
			{
				b._dp[i] = 1u;
				b._used++;
			}
			for (i = b._used; i < used; i++)
			{
				b._dp[i] = 0u;
			}
			b._sign = a._sign;
			return true;
		}

		public static bool reduce2K(TomMPInt a, TomMPInt n, ref uint d)
		{
			TomMPInt tomMPInt = new TomMPInt();
			tomMPInt.init();
			int b = countBits(n);
			while (div2D(a, b, tomMPInt, a))
			{
				if (d != 1 && !mulDigit(tomMPInt, d, tomMPInt))
				{
					return false;
				}
				if (!sAdd(a, tomMPInt, a))
				{
					return false;
				}
				bool flag;
				if (cmpMag(a, n) != MP_LT)
				{
					sSub(a, n, a);
					flag = true;
				}
				else
				{
					flag = false;
				}
				if (!flag)
				{
					return true;
				}
			}
			return false;
		}

		public static bool reduce2KSetup(TomMPInt a, ref uint d)
		{
			TomMPInt tomMPInt = new TomMPInt();
			tomMPInt.init();
			int b = countBits(a);
			if (!twoExpt(tomMPInt, b))
			{
				return false;
			}
			if (!sSub(tomMPInt, a, tomMPInt))
			{
				return false;
			}
			d = tomMPInt._dp[0];
			return true;
		}

		public static bool drReduce(TomMPInt x, TomMPInt n, ref uint k)
		{
			int used = n._used;
			if (x._alloc < used + used && !grow(x, used + used))
			{
				return false;
			}
			bool flag;
			do
			{
				int num = 0;
				int num2 = used;
				uint num3 = 0u;
				for (int i = 0; i < used; i++)
				{
					ulong num4 = x._dp[num2++] * k + x._dp[num] + num3;
					x._dp[num++] = (uint)(num4 & MP_MASK);
					num3 = (uint)(num4 >> MP_DIGIT_BIT);
				}
				x._dp[num++] = num3;
				for (int j = used + 1; j < x._used; j++)
				{
					x._dp[num++] = 0u;
				}
				clamp(x);
				if (cmpMag(x, n) != MP_LT)
				{
					sSub(x, n, x);
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			while (flag);
			return true;
		}

		public static void drSetup(TomMPInt a, ref uint d)
		{
			d = (uint)(1 << MP_DIGIT_BIT) - a._dp[0];
		}

		public static bool fastMontgomeryReduce(TomMPInt x, TomMPInt n, ref uint rho)
		{
			ulong[] array = new ulong[MP_WARRAY];
			int used = x._used;
			if (x._alloc < n._used + 1 && !grow(x, n._used + 1))
			{
				return false;
			}
			int i;
			for (i = 0; i < x._used; i++)
			{
				array[i] = x._dp[i];
			}
			for (; i < n._used * 2 + 1; i++)
			{
				array[i] = 0uL;
			}
			for (i = 0; i < n._used; i++)
			{
				uint num = (uint)(((array[i] & MP_MASK) * rho) & MP_MASK);
				for (int j = 0; j < n._used; j++)
				{
					array[i + j] += (ulong)((long)num * (long)n._dp[j]);
				}
				array[i + 1] += array[i] >> MP_DIGIT_BIT;
			}
			for (; i <= n._used * 2 + 1; i++)
			{
				array[i + 1] += array[i] >> MP_DIGIT_BIT;
			}
			for (i = 0; i < n._used + 1; i++)
			{
				x._dp[i] = (uint)(array[n._used + i] & MP_MASK);
			}
			for (; i < used; i++)
			{
				x._dp[i] = 0u;
			}
			x._used = n._used + 1;
			clamp(x);
			if (cmpMag(x, n) != MP_LT)
			{
				return sSub(x, n, x);
			}
			return true;
		}

		public static bool montgomeryReduce(TomMPInt x, TomMPInt n, ref uint rho)
		{
			uint rho2 = rho;
			int num = n._used * 2 + 1;
			if (num < MP_WARRAY && n._used < 1 << CHAR_BIT * MP_WORD_BYTE_SIZE - 2 * MP_DIGIT_BIT)
			{
				return fastMontgomeryReduce(x, n, ref rho2);
			}
			if (x._alloc < num && !grow(x, num))
			{
				return false;
			}
			x._used = num;
			for (int i = 0; i < n._used; i++)
			{
				uint num2 = (x._dp[i] * rho2) & MP_MASK;
				uint num3 = 0u;
				int num4 = i;
				for (int j = 0; j < n._used; j++)
				{
					uint num5 = num2 * n._dp[j] + num3 + x._dp[num4];
					num3 = num5 >> MP_DIGIT_BIT;
					x._dp[num4++] = num5 & MP_MASK;
				}
				while (num3 != 0)
				{
					x._dp[num4] += num3;
					num3 = x._dp[num4] >> MP_DIGIT_BIT;
					x._dp[num4++] &= MP_MASK;
				}
			}
			clamp(x);
			rightShiftDigit(x, n._used);
			if (cmpMag(x, n) != MP_LT)
			{
				return sSub(x, n, x);
			}
			return true;
		}

		public static bool montgomerySetup(TomMPInt n, ref uint rho)
		{
			uint num = n._dp[0];
			if ((num & 1) == 0)
			{
				return false;
			}
			uint num2 = (((num + 2) & 4) << 1) + num;
			num2 *= 2 - num * num2;
			num2 *= 2 - num * num2;
			num2 *= 2 - num * num2;
			rho = (uint)(((1L << (MP_DIGIT_BIT & 0x1F)) - num2) & MP_MASK);
			return true;
		}

		public static int reduceIs2K(TomMPInt a)
		{
			if (a._used == 0)
			{
				return MP_NO;
			}
			if (a._used == 1)
			{
				return MP_YES;
			}
			if (a._used > 1)
			{
				int num = countBits(a);
				uint num2 = 1u;
				int num3 = 1;
				for (int i = MP_DIGIT_BIT; i < num; i++)
				{
					if ((a._dp[num3] & num2) == 0)
					{
						return MP_NO;
					}
					num2 <<= 1;
					if (num2 > MP_MASK)
					{
						num3++;
						num2 = 1u;
					}
				}
			}
			return MP_YES;
		}

		public static int drIsModulus(TomMPInt a)
		{
			if (a._used < 2)
			{
				return 0;
			}
			for (int i = 1; i < a._used; i++)
			{
				if (a._dp[i] != MP_MASK)
				{
					return 0;
				}
			}
			return 1;
		}

		public static bool sExptMod(TomMPInt G, TomMPInt X, TomMPInt P, TomMPInt Y, int redmode)
		{
			TomMPInt[] array = new TomMPInt[MP_TAB_SIZE];
			TomMPInt tomMPInt = new TomMPInt();
			int num = countBits(X);
			int num2 = ((num <= 7) ? 2 : ((num <= 36) ? 3 : ((num <= 140) ? 4 : ((num <= 450) ? 5 : ((num <= 1303) ? 6 : ((num > 3529) ? 8 : 7))))));
			if (MP_TAB_SIZE < 256)
			{
				num2 = 5;
			}
			array[1].init();
			for (num = 1 << num2 - 1; num < 1 << num2; num++)
			{
				array[num].init();
			}
			tomMPInt.init();
			redux2Delegate redux2Delegate2;
			if (redmode == 0)
			{
				if (!reduceSetup(tomMPInt, P))
				{
					return false;
				}
				redux2Delegate2 = reduce;
			}
			else
			{
				if (!reduce2KSetupL(P, tomMPInt))
				{
					return false;
				}
				redux2Delegate2 = reduce2KL;
			}
			if (!mod(G, P, array[1]))
			{
				return false;
			}
			if (!copy(array[1], array[1 << num2 - 1]))
			{
				return false;
			}
			for (num = 0; num < num2 - 1; num++)
			{
				if (!sqr(array[1 << num2 - 1], array[1 << num2 - 1]))
				{
					return false;
				}
				if (!redux2Delegate2(array[1 << num2 - 1], P, tomMPInt))
				{
					return false;
				}
			}
			for (num = (1 << num2 - 1) + 1; num < 1 << num2; num++)
			{
				if (!mul(array[num - 1], array[1], array[num]))
				{
					return false;
				}
				if (!redux2Delegate2(array[num], P, tomMPInt))
				{
					return false;
				}
			}
			TomMPInt tomMPInt2 = new TomMPInt();
			tomMPInt2.init();
			setDigit(tomMPInt2, 1u);
			int num3 = 0;
			int num4 = 1;
			int num5 = 0;
			int num6 = X._used - 1;
			int num7 = 0;
			int num8 = 0;
			while (true)
			{
				if (--num4 == 0)
				{
					if (num6 == -1)
					{
						break;
					}
					num5 = (int)X._dp[num6--];
					num4 = MP_DIGIT_BIT;
				}
				int num9 = (num5 >> MP_DIGIT_BIT - 1) & 1;
				num5 <<= 1;
				if (num3 == 0 && num9 == 0)
				{
					continue;
				}
				if (num3 == 1 && num9 == 0)
				{
					if (sqr(tomMPInt2, tomMPInt2))
					{
						if (!redux2Delegate2(tomMPInt2, P, tomMPInt))
						{
							return false;
						}
						continue;
					}
					return false;
				}
				num8 |= num9 << num2 - ++num7;
				num3 = 2;
				if (num7 != num2)
				{
					continue;
				}
				for (num = 0; num < num2; num++)
				{
					if (!sqr(tomMPInt2, tomMPInt2))
					{
						return false;
					}
					if (!redux2Delegate2(tomMPInt2, P, tomMPInt))
					{
						return false;
					}
				}
				if (!mul(tomMPInt2, array[num8], tomMPInt2))
				{
					return false;
				}
				if (!redux2Delegate2(tomMPInt2, P, tomMPInt))
				{
					return false;
				}
				num7 = 0;
				num8 = 0;
				num3 = 1;
			}
			if (num3 == 2 && num7 > 0)
			{
				for (num = 0; num < num7; num++)
				{
					if (!sqr(tomMPInt2, tomMPInt2))
					{
						return false;
					}
					if (!redux2Delegate2(tomMPInt2, P, tomMPInt))
					{
						return false;
					}
					num8 <<= 1;
					if ((num8 & (1 << num2)) != 0)
					{
						if (!mul(tomMPInt2, array[1], tomMPInt2))
						{
							return false;
						}
						if (!redux2Delegate2(tomMPInt2, P, tomMPInt))
						{
							return false;
						}
					}
				}
			}
			exch(tomMPInt2, Y);
			return true;
		}

		public static bool sqr(TomMPInt a, TomMPInt b)
		{
			bool result = ((a._used * 2 + 1 >= MP_WARRAY || a._used >= 1 << MP_WORD_BYTE_SIZE * CHAR_BIT - 2 * MP_DIGIT_BIT - 1) ? sSqr(a, b) : fastsSqr(a, b));
			b._sign = MP_ZPOS;
			return result;
		}

		public static bool fastsSqr(TomMPInt a, TomMPInt b)
		{
			uint[] array = new uint[MP_WARRAY];
			int num = a._used + a._used;
			if (b._alloc < num && !grow(b, num))
			{
				return false;
			}
			ulong num2 = 0uL;
			int i;
			for (i = 0; i < num; i++)
			{
				ulong num3 = 0uL;
				int num4 = Math.Min(a._used - 1, i);
				int num5 = i - num4;
				int val = Math.Min(a._used - num5, num4 + 1);
				val = Math.Min(val, num4 - num5 + 1 >> 1);
				for (int j = 0; j < val; j++)
				{
					num3 += (ulong)((long)a._dp[num5++] * (long)a._dp[num4--]);
				}
				num3 = num3 + num3 + num2;
				if ((i & 1) == 0)
				{
					num3 += (ulong)((long)a._dp[i >> 1] * (long)a._dp[i >> 1]);
				}
				array[i] = (uint)(num3 & MP_MASK);
				num2 = num3 >> MP_DIGIT_BIT;
			}
			int used = b._used;
			b._used = a._used + a._used;
			for (i = 0; i < num; i++)
			{
				b._dp[i] = array[i] & MP_MASK;
			}
			for (; i < used; i++)
			{
				b._dp[i] = 0u;
			}
			clamp(b);
			return true;
		}

		public static bool sSqr(TomMPInt a, TomMPInt b)
		{
			TomMPInt tomMPInt = new TomMPInt();
			int used = a._used;
			if (!initSize(tomMPInt, 2 * used + 1))
			{
				return false;
			}
			tomMPInt._used = 2 * used + 1;
			for (int i = 0; i < used; i++)
			{
				ulong num = (ulong)(tomMPInt._dp[2 * i] + (long)a._dp[i] * (long)a._dp[i]);
				tomMPInt._dp[i + i] = (uint)(num & MP_MASK);
				uint num2 = (uint)(num >> MP_DIGIT_BIT);
				uint num3 = a._dp[i];
				int num4 = 2 * i + 1;
				for (int j = i + 1; j < used; j++)
				{
					num = (ulong)num3 * (ulong)a._dp[j];
					num = tomMPInt._dp[num4] + num + num + num2;
					tomMPInt._dp[num4++] = (uint)(num & MP_MASK);
					num2 = (uint)(num >> MP_DIGIT_BIT);
				}
				while (num2 != 0)
				{
					num = (ulong)tomMPInt._dp[num4] + (ulong)num2;
					tomMPInt._dp[num4++] = (uint)(num & MP_MASK);
					num2 = (uint)(num >> MP_DIGIT_BIT);
				}
			}
			clamp(tomMPInt);
			exch(tomMPInt, b);
			return true;
		}

		public static bool reduce2KL(TomMPInt a, TomMPInt n, TomMPInt d)
		{
			TomMPInt tomMPInt = new TomMPInt();
			tomMPInt.init();
			int b = countBits(n);
			while (div2D(a, b, tomMPInt, a))
			{
				if (!mul(tomMPInt, d, tomMPInt))
				{
					return false;
				}
				if (!sAdd(a, tomMPInt, a))
				{
					return false;
				}
				if (cmpMag(a, n) == MP_LT)
				{
					return true;
				}
				sSub(a, n, a);
			}
			return false;
		}

		public static bool reduce(TomMPInt x, TomMPInt m, TomMPInt mu)
		{
			TomMPInt tomMPInt = new TomMPInt();
			int used = m._used;
			tomMPInt.init();
			if (!copy(tomMPInt, x))
			{
				return false;
			}
			rightShiftDigit(tomMPInt, used - 1);
			if (used > 1 << MP_DIGIT_BIT - 1)
			{
				if (!mul(tomMPInt, mu, tomMPInt))
				{
					return false;
				}
			}
			else if (!sMulHighDigs(tomMPInt, mu, tomMPInt, used))
			{
				return false;
			}
			rightShiftDigit(tomMPInt, used + 1);
			if (!mod2D(x, MP_DIGIT_BIT * (used + 1), x))
			{
				return false;
			}
			if (!sMulDigs(tomMPInt, m, tomMPInt, used + 1))
			{
				return false;
			}
			if (!sub(x, tomMPInt, x))
			{
				return false;
			}
			if (cmpDigit(x, 0u) == MP_LT)
			{
				setDigit(tomMPInt, 1u);
				if (!leftShiftDigit(tomMPInt, used + 1))
				{
					return false;
				}
				if (!add(x, tomMPInt, x))
				{
					return false;
				}
			}
			while (cmp(x, m) != MP_LT)
			{
				if (!sSub(x, m, x))
				{
					return false;
				}
			}
			return true;
		}

		public static bool sMulDigs(TomMPInt a, TomMPInt b, TomMPInt c, int digs)
		{
			TomMPInt tomMPInt = new TomMPInt();
			if (digs < MP_WARRAY && Math.Min(a._used, b._used) < 1 << CHAR_BIT * MP_WORD_BYTE_SIZE - 2 * MP_DIGIT_BIT)
			{
				return fastSMulDigs(a, b, c, digs);
			}
			if (!initSize(tomMPInt, digs))
			{
				return false;
			}
			tomMPInt._used = digs;
			int used = a._used;
			for (int i = 0; i < used; i++)
			{
				uint num = 0u;
				int num2 = Math.Min(b._used, digs - i);
				int num3 = i;
				int j;
				for (j = 0; j < num2; j++)
				{
					uint num4 = tomMPInt._dp[num3] + a._dp[i] * b._dp[j] + num;
					tomMPInt._dp[num3++] = num4 & MP_MASK;
					num = num4 >> MP_DIGIT_BIT;
				}
				if (i + j < digs)
				{
					tomMPInt._dp[num3] = num;
				}
			}
			clamp(tomMPInt);
			exch(tomMPInt, c);
			return true;
		}

		public static bool fastSMulDigs(TomMPInt a, TomMPInt b, TomMPInt c, int digs)
		{
			uint[] array = new uint[MP_WARRAY];
			if (c._alloc < digs && !grow(c, digs))
			{
				return false;
			}
			int num = Math.Min(digs, a._used + b._used);
			ulong num2 = 0uL;
			int i;
			for (i = 0; i < num; i++)
			{
				int num3 = Math.Min(b._used - 1, i);
				int num4 = i - num3;
				int num5 = Math.Min(a._used - num4, num3 + 1);
				for (int j = 0; j < num5; j++)
				{
					num2 += (ulong)((long)a._dp[num4++] * (long)b._dp[num3--]);
				}
				array[i] = (uint)(num2 & MP_MASK);
				num2 >>= MP_DIGIT_BIT;
			}
			int used = c._used;
			c._used = num;
			for (i = 0; i < num + 1; i++)
			{
				c._dp[i] = array[i];
			}
			for (; i < used; i++)
			{
				c._dp[i] = 0u;
			}
			clamp(c);
			return true;
		}

		public static bool add(TomMPInt a, TomMPInt b, TomMPInt c)
		{
			int sign = a._sign;
			int sign2 = b._sign;
			if (sign == sign2)
			{
				c._sign = sign;
				return sAdd(a, b, c);
			}
			if (cmpMag(a, b) == MP_LT)
			{
				c._sign = sign2;
				return sSub(b, a, c);
			}
			c._sign = sign;
			return sSub(a, b, c);
		}

		public static void setDigit(TomMPInt a, uint b)
		{
			zero(a);
			a._dp[0] = b & MP_MASK;
			a._used = ((a._dp[0] != 0) ? 1 : 0);
		}

		public static int cmpDigit(TomMPInt a, uint b)
		{
			if (a._sign == MP_NEG)
			{
				return MP_LT;
			}
			if (a._used > 1)
			{
				return MP_GT;
			}
			if (a._dp[0] > b)
			{
				return MP_GT;
			}
			if (a._dp[0] < b)
			{
				return MP_LT;
			}
			return MP_EQ;
		}

		public static bool sMulHighDigs(TomMPInt a, TomMPInt b, TomMPInt c, int digs)
		{
			TomMPInt tomMPInt = new TomMPInt();
			if (!initSize(tomMPInt, a._used + b._used + 1))
			{
				return false;
			}
			tomMPInt._used = a._used + b._used + 1;
			int used = a._used;
			int used2 = b._used;
			for (int i = 0; i < used; i++)
			{
				uint num = 0u;
				int num2 = digs;
				for (int j = digs - i; j < used2; j++)
				{
					uint num3 = tomMPInt._dp[num2] + a._dp[i] * b._dp[j] + num;
					tomMPInt._dp[num2++] = num3 & MP_MASK;
					num = num3 >> MP_DIGIT_BIT;
				}
				tomMPInt._dp[num2] = num;
			}
			clamp(tomMPInt);
			exch(tomMPInt, c);
			return true;
		}

		public static bool mul(TomMPInt a, TomMPInt b, TomMPInt c)
		{
			int num = ((a._sign == b._sign) ? MP_ZPOS : MP_NEG);
			int num2 = a._used + b._used + 1;
			bool result = ((num2 >= MP_WARRAY || Math.Min(a._used, b._used) > 1 << CHAR_BIT * 8 - 2 * MP_DIGIT_BIT) ? sMul(a, b, c, a._used + b._used + 1) : fastSMulDigs(a, b, c, num2));
			c._sign = ((c._used > 0) ? num : MP_ZPOS);
			return result;
		}

		public static bool sMul(TomMPInt a, TomMPInt b, TomMPInt c, int digs)
		{
			TomMPInt tomMPInt = new TomMPInt();
			if (!initSize(tomMPInt, digs))
			{
				return false;
			}
			tomMPInt._used = digs;
			int used = a._used;
			for (int i = 0; i < used; i++)
			{
				uint num = 0u;
				int num2 = Math.Min(b._used, digs - i);
				uint num3 = a._dp[i];
				int num4 = i;
				int j;
				for (j = 0; j < num2; j++)
				{
					uint num5 = tomMPInt._dp[num4] + num3 * b._dp[j] + num;
					tomMPInt._dp[num4++] = num5 & MP_MASK;
					num = num5 >> MP_DIGIT_BIT;
				}
				if (i + j < digs)
				{
					tomMPInt._dp[num4] = num;
				}
			}
			clamp(tomMPInt);
			exch(tomMPInt, c);
			return true;
		}

		public static bool reduce2KSetupL(TomMPInt a, TomMPInt d)
		{
			TomMPInt tomMPInt = new TomMPInt();
			tomMPInt.init();
			if (twoExpt(tomMPInt, countBits(a)))
			{
				return sSub(tomMPInt, a, d);
			}
			return false;
		}

		public static bool reduceSetup(TomMPInt a, TomMPInt b)
		{
			if (twoExpt(a, b._used * 2 * MP_DIGIT_BIT))
			{
				return div(a, b, a, null);
			}
			return false;
		}

		public static bool reduceIs2KL(TomMPInt a)
		{
			if (a._used == 0)
			{
				return false;
			}
			if (a._used == 1)
			{
				return true;
			}
			if (a._used > 1)
			{
				int num;
				for (int i = (num = 0); i < a._used; i++)
				{
					if (a._dp[i] == MP_MASK)
					{
						num++;
					}
				}
				return num >= a._used / 2;
			}
			return true;
		}

		public static bool abs(TomMPInt a, TomMPInt b)
		{
			if (a != b && !copy(a, b))
			{
				return false;
			}
			b._sign = MP_ZPOS;
			return true;
		}

		public static int isZero(TomMPInt a)
		{
			if (a._used != 0)
			{
				return MP_NO;
			}
			return MP_YES;
		}

		public static int isOdd(TomMPInt a)
		{
			if (a._used <= 0 || (a._dp[0] & 1) != 1)
			{
				return MP_NO;
			}
			return MP_YES;
		}

		public static int isEven(TomMPInt a)
		{
			if (a._used <= 0 || (a._dp[0] & 1) != 0)
			{
				return MP_NO;
			}
			return MP_YES;
		}

		public static bool invMod(TomMPInt a, TomMPInt b, TomMPInt c)
		{
			if (b._sign == MP_NEG || isZero(b) == 1)
			{
				return false;
			}
			if (isOdd(b) == 1)
			{
				return fastInvMod(a, b, c);
			}
			return slowInvMod(a, b, c);
		}

		public static bool fastInvMod(TomMPInt a, TomMPInt b, TomMPInt c)
		{
			if (isEven(b) == 1)
			{
				return false;
			}
			TomMPInt tomMPInt = new TomMPInt();
			TomMPInt tomMPInt2 = new TomMPInt();
			TomMPInt tomMPInt3 = new TomMPInt();
			TomMPInt tomMPInt4 = new TomMPInt();
			TomMPInt tomMPInt5 = new TomMPInt();
			TomMPInt tomMPInt6 = new TomMPInt();
			tomMPInt.init();
			tomMPInt2.init();
			tomMPInt3.init();
			tomMPInt4.init();
			tomMPInt5.init();
			tomMPInt6.init();
			if (!copy(b, tomMPInt))
			{
				return false;
			}
			if (!mod(a, b, tomMPInt2))
			{
				return false;
			}
			if (!copy(tomMPInt, tomMPInt3))
			{
				return false;
			}
			if (!copy(tomMPInt2, tomMPInt4))
			{
				return false;
			}
			setDigit(tomMPInt6, 1u);
			while (true)
			{
				if (isEven(tomMPInt3) != 1)
				{
					while (isEven(tomMPInt4) == 1)
					{
						if (!div2(tomMPInt4, tomMPInt4))
						{
							return false;
						}
						if (isOdd(tomMPInt6) == 1 && !sub(tomMPInt6, tomMPInt, tomMPInt6))
						{
							return false;
						}
						if (!div2(tomMPInt6, tomMPInt6))
						{
							return false;
						}
					}
					if (cmp(tomMPInt3, tomMPInt4) != MP_LT)
					{
						if (!sub(tomMPInt3, tomMPInt4, tomMPInt3))
						{
							return false;
						}
						if (!sub(tomMPInt5, tomMPInt6, tomMPInt5))
						{
							return false;
						}
					}
					else
					{
						if (!sub(tomMPInt4, tomMPInt3, tomMPInt4))
						{
							return false;
						}
						if (!sub(tomMPInt6, tomMPInt5, tomMPInt6))
						{
							return false;
						}
					}
					if (isZero(tomMPInt3) != 0)
					{
						break;
					}
				}
				else
				{
					if (!div2(tomMPInt3, tomMPInt3))
					{
						return false;
					}
					if (isOdd(tomMPInt5) == 1 && !sub(tomMPInt5, tomMPInt, tomMPInt5))
					{
						return false;
					}
					if (!div2(tomMPInt5, tomMPInt5))
					{
						return false;
					}
				}
			}
			if (cmpDigit(tomMPInt4, 1u) != MP_EQ)
			{
				return false;
			}
			int sign = a._sign;
			while (tomMPInt6._sign == MP_NEG)
			{
				if (!add(tomMPInt6, b, tomMPInt6))
				{
					return false;
				}
			}
			exch(tomMPInt6, c);
			c._sign = sign;
			return true;
		}

		public static bool div2(TomMPInt a, TomMPInt b)
		{
			if (b._alloc < a._used && !grow(b, a._used))
			{
				return false;
			}
			int used = b._used;
			int used2 = a._used;
			uint num = 0u;
			for (int num2 = b._used - 1; num2 >= 0; num2--)
			{
				uint num3 = a._dp[num2] & 1;
				b._dp[num2] = (a._dp[num2] >> 1) | (num << MP_DIGIT_BIT - 1);
				num = num3;
			}
			for (int i = b._used; i < used; i++)
			{
				b._dp[i] = 0u;
			}
			b._sign = a._sign;
			clamp(b);
			return true;
		}

		public static bool slowInvMod(TomMPInt a, TomMPInt b, TomMPInt c)
		{
			if (b._sign == MP_NEG || isZero(b) == 1)
			{
				return false;
			}
			TomMPInt tomMPInt = new TomMPInt();
			TomMPInt tomMPInt2 = new TomMPInt();
			TomMPInt tomMPInt3 = new TomMPInt();
			TomMPInt tomMPInt4 = new TomMPInt();
			TomMPInt tomMPInt5 = new TomMPInt();
			TomMPInt tomMPInt6 = new TomMPInt();
			TomMPInt tomMPInt7 = new TomMPInt();
			TomMPInt tomMPInt8 = new TomMPInt();
			tomMPInt.init();
			tomMPInt2.init();
			tomMPInt3.init();
			tomMPInt4.init();
			tomMPInt5.init();
			tomMPInt6.init();
			tomMPInt7.init();
			tomMPInt8.init();
			if (!mod(a, b, tomMPInt))
			{
				return false;
			}
			if (!copy(b, tomMPInt2))
			{
				return false;
			}
			if (isEven(tomMPInt) == 1 && isEven(tomMPInt2) == 1)
			{
				return false;
			}
			if (!copy(tomMPInt, tomMPInt3))
			{
				return false;
			}
			if (!copy(tomMPInt2, tomMPInt4))
			{
				return false;
			}
			setDigit(tomMPInt5, 1u);
			setDigit(tomMPInt8, 1u);
			while (true)
			{
				if (isEven(tomMPInt3) != 1)
				{
					while (isEven(tomMPInt4) == 1)
					{
						if (!div2(tomMPInt4, tomMPInt4))
						{
							return false;
						}
						if (isOdd(tomMPInt7) == 1 || isOdd(tomMPInt8) == 1)
						{
							if (!add(tomMPInt7, tomMPInt2, tomMPInt7))
							{
								return false;
							}
							if (!sub(tomMPInt8, tomMPInt, tomMPInt8))
							{
								return false;
							}
						}
						if (!div2(tomMPInt7, tomMPInt7))
						{
							return false;
						}
						if (!div2(tomMPInt8, tomMPInt8))
						{
							return false;
						}
					}
					if (cmp(tomMPInt3, tomMPInt4) != MP_LT)
					{
						if (!sub(tomMPInt3, tomMPInt4, tomMPInt3))
						{
							return false;
						}
						if (!sub(tomMPInt5, tomMPInt7, tomMPInt5))
						{
							return false;
						}
						if (!sub(tomMPInt6, tomMPInt8, tomMPInt6))
						{
							return false;
						}
					}
					else
					{
						if (!sub(tomMPInt4, tomMPInt3, tomMPInt4))
						{
							return false;
						}
						if (!sub(tomMPInt7, tomMPInt5, tomMPInt7))
						{
							return false;
						}
						if (!sub(tomMPInt8, tomMPInt6, tomMPInt8))
						{
							return false;
						}
					}
					if (isZero(tomMPInt3) != 0)
					{
						break;
					}
					continue;
				}
				if (div2(tomMPInt3, tomMPInt3))
				{
					if (isOdd(tomMPInt5) == 1 || isOdd(tomMPInt6) == 1)
					{
						if (!add(tomMPInt5, tomMPInt2, tomMPInt5))
						{
							return false;
						}
						if (!sub(tomMPInt6, tomMPInt, tomMPInt6))
						{
							return false;
						}
					}
					if (!div2(tomMPInt5, tomMPInt5))
					{
						return false;
					}
					if (!div2(tomMPInt6, tomMPInt6))
					{
						return false;
					}
					continue;
				}
				return false;
			}
			if (cmpDigit(tomMPInt4, 1u) != MP_EQ)
			{
				return false;
			}
			while (cmpDigit(tomMPInt7, 0u) == MP_LT)
			{
				if (!add(tomMPInt7, b, tomMPInt7))
				{
					return false;
				}
			}
			while (cmpMag(tomMPInt7, b) != MP_LT)
			{
				if (!sub(tomMPInt7, b, tomMPInt7))
				{
					return false;
				}
			}
			exch(tomMPInt7, c);
			return true;
		}

		public static bool mod(TomMPInt a, TomMPInt b, TomMPInt c)
		{
			TomMPInt tomMPInt = new TomMPInt();
			tomMPInt.init();
			if (!div(a, b, null, tomMPInt))
			{
				return false;
			}
			if (tomMPInt._sign != b._sign)
			{
				return add(b, tomMPInt, c);
			}
			exch(tomMPInt, c);
			return true;
		}

		public static bool div(TomMPInt a, TomMPInt b, TomMPInt c, TomMPInt d)
		{
			if (isZero(b) == 1)
			{
				return false;
			}
			if (cmpMag(a, b) == MP_LT)
			{
				if (d != null && !copy(a, d))
				{
					return false;
				}
				if (c != null)
				{
					zero(c);
				}
				return true;
			}
			TomMPInt tomMPInt = new TomMPInt();
			if (!initSize(tomMPInt, a._used + 2))
			{
				return false;
			}
			tomMPInt._used = a._used + 2;
			TomMPInt tomMPInt2 = new TomMPInt();
			TomMPInt tomMPInt3 = new TomMPInt();
			TomMPInt tomMPInt4 = new TomMPInt();
			TomMPInt tomMPInt5 = new TomMPInt();
			tomMPInt2.init();
			tomMPInt3.init();
			tomMPInt4.init();
			if (!copy(a, tomMPInt4))
			{
				return false;
			}
			tomMPInt5.init();
			if (!copy(b, tomMPInt5))
			{
				return false;
			}
			int sign = ((a._sign == b._sign) ? MP_ZPOS : MP_NEG);
			tomMPInt4._sign = (tomMPInt5._sign = MP_ZPOS);
			int num = countBits(tomMPInt5) % MP_DIGIT_BIT;
			if (num < MP_DIGIT_BIT - 1)
			{
				num = MP_DIGIT_BIT - 1 - num;
				if (!mul2D(tomMPInt4, num, tomMPInt4))
				{
					return false;
				}
				if (!mul2D(tomMPInt5, num, tomMPInt5))
				{
					return false;
				}
			}
			else
			{
				num = 0;
			}
			int num2 = tomMPInt4._used - 1;
			int num3 = tomMPInt5._used - 1;
			if (!leftShiftDigit(tomMPInt5, num2 - num3))
			{
				return false;
			}
			while (cmp(tomMPInt4, tomMPInt5) != MP_LT)
			{
				tomMPInt._dp[num2 - num3]++;
				if (!sub(tomMPInt4, tomMPInt5, tomMPInt4))
				{
					return false;
				}
			}
			rightShiftDigit(tomMPInt5, num2 - num3);
			for (int num4 = num2; num4 >= num3 + 1; num4--)
			{
				if (num4 <= tomMPInt4._used)
				{
					if (tomMPInt4._dp[num4] == tomMPInt5._dp[num3])
					{
						tomMPInt._dp[num4 - num3 - 1] = (uint)((1 << MP_DIGIT_BIT) - 1);
					}
					else
					{
						ulong num5 = (ulong)tomMPInt4._dp[num4] << MP_DIGIT_BIT;
						num5 |= tomMPInt4._dp[num4 - 1];
						num5 /= tomMPInt5._dp[num3];
						if (num5 > MP_MASK)
						{
							num5 = MP_MASK;
						}
						tomMPInt._dp[num4 - num3 - 1] = (uint)(num5 & MP_MASK);
					}
					tomMPInt._dp[num4 - num3 - 1] = (tomMPInt._dp[num4 - num3 - 1] + 1) & MP_MASK;
					do
					{
						tomMPInt._dp[num4 - num3 - 1] = (tomMPInt._dp[num4 - num3 - 1] - 1) & MP_MASK;
						zero(tomMPInt2);
						tomMPInt2._dp[0] = ((num3 - 1 >= 0) ? tomMPInt5._dp[num3 - 1] : 0u);
						tomMPInt2._dp[1] = tomMPInt5._dp[num3];
						tomMPInt2._used = 2;
						if (mulDigit(tomMPInt2, tomMPInt._dp[num4 - num3 - 1], tomMPInt2))
						{
							tomMPInt3._dp[0] = ((num4 - 2 >= 0) ? tomMPInt4._dp[num4 - 2] : 0u);
							tomMPInt3._dp[1] = ((num4 - 1 >= 0) ? tomMPInt4._dp[num4 - 1] : 0u);
							tomMPInt3._dp[2] = tomMPInt4._dp[num4];
							tomMPInt3._used = 3;
							continue;
						}
						return false;
					}
					while (cmpMag(tomMPInt2, tomMPInt3) == MP_GT);
					if (!mulDigit(tomMPInt5, tomMPInt._dp[num4 - num3 - 1], tomMPInt2))
					{
						return false;
					}
					if (!leftShiftDigit(tomMPInt2, num4 - num3 - 1))
					{
						return false;
					}
					if (!sub(tomMPInt4, tomMPInt2, tomMPInt4))
					{
						return false;
					}
					if (tomMPInt4._sign == MP_NEG)
					{
						if (!copy(tomMPInt5, tomMPInt2))
						{
							return false;
						}
						if (!leftShiftDigit(tomMPInt2, num4 - num3 - 1))
						{
							return false;
						}
						if (!add(tomMPInt4, tomMPInt2, tomMPInt4))
						{
							return false;
						}
						tomMPInt._dp[num4 - num3 - 1] = (tomMPInt._dp[num4 - num3 - 1] - 1) & MP_MASK;
					}
				}
			}
			tomMPInt4._sign = ((tomMPInt4._used == 0) ? MP_ZPOS : a._sign);
			if (c != null)
			{
				clamp(tomMPInt);
				exch(tomMPInt, c);
				c._sign = sign;
			}
			if (d != null)
			{
				div2D(tomMPInt4, num, tomMPInt4, null);
				exch(tomMPInt4, d);
			}
			return true;
		}

		public static bool div2D(TomMPInt a, int b, TomMPInt c, TomMPInt d)
		{
			TomMPInt tomMPInt = new TomMPInt();
			if (b <= 0)
			{
				if (!copy(a, c))
				{
					return false;
				}
				if (d != null)
				{
					zero(d);
				}
				return true;
			}
			tomMPInt.init();
			if (d != null && !mod2D(a, b, tomMPInt))
			{
				return false;
			}
			if (!copy(a, c))
			{
				return false;
			}
			if (b >= MP_DIGIT_BIT)
			{
				rightShiftDigit(c, b / MP_DIGIT_BIT);
			}
			uint num = (uint)(b % MP_DIGIT_BIT);
			if (num != 0)
			{
				uint num2 = (uint)((1 << (int)num) - 1);
				uint num3 = (uint)MP_DIGIT_BIT - num;
				uint num4 = 0u;
				for (int num5 = c._used - 1; num5 >= 0; num5--)
				{
					uint num6 = c._dp[num5] & num2;
					c._dp[num5] = (c._dp[num5] >> (int)num) | (num4 << (int)num3);
					num4 = num6;
				}
			}
			clamp(c);
			if (d != null)
			{
				exch(tomMPInt, d);
			}
			return true;
		}

		public static bool mod2D(TomMPInt a, int b, TomMPInt c)
		{
			if (b <= 0)
			{
				zero(c);
				return true;
			}
			if (b >= a._used * MP_DIGIT_BIT)
			{
				return copy(a, c);
			}
			if (!copy(a, c))
			{
				return false;
			}
			for (int i = b / MP_DIGIT_BIT + ((b % MP_DIGIT_BIT != 0) ? 1 : 0); i < c._used; i++)
			{
				c._dp[i] = 0u;
			}
			c._dp[b / MP_DIGIT_BIT] &= (uint)((1 << (int)((ulong)b % (ulong)MP_DIGIT_BIT)) - 1);
			clamp(c);
			return true;
		}

		public static void exch(TomMPInt a, TomMPInt b)
		{
			TomMPInt tomMPInt = new TomMPInt();
			tomMPInt._alloc = a._alloc;
			tomMPInt._dp = a._dp;
			tomMPInt._sign = a._sign;
			tomMPInt._used = a._used;
			a._alloc = b._alloc;
			a._dp = b._dp;
			a._sign = b._sign;
			a._used = b._used;
			b._alloc = tomMPInt._alloc;
			b._dp = tomMPInt._dp;
			b._sign = tomMPInt._sign;
			b._used = tomMPInt._used;
		}

		public static bool mulDigit(TomMPInt a, uint b, TomMPInt c)
		{
			if (c._alloc < a._used + 1 && !grow(c, a._used + 1))
			{
				return false;
			}
			int used = c._used;
			c._sign = a._sign;
			uint num = 0u;
			int i;
			for (i = 0; i < a._used; i++)
			{
				ulong num2 = (ulong)(num + (long)a._dp[i] * (long)b);
				c._dp[i] = (uint)(num2 & MP_MASK);
				num = (uint)(num2 >> MP_DIGIT_BIT);
			}
			c._dp[i++] = num;
			while (i < used)
			{
				c._dp[i++] = 0u;
			}
			c._used = a._used + 1;
			clamp(c);
			return true;
		}

		public static bool initSize(TomMPInt a, int size)
		{
			size += MP_PREC * 2 - size % MP_PREC;
			a._dp = new uint[size];
			a._used = 0;
			a._alloc = size;
			a._sign = MP_ZPOS;
			for (int i = 0; i < size; i++)
			{
				a._dp[i] = 0u;
			}
			return true;
		}
	}
}

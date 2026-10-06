namespace Nettention.Proud
{
	internal class FrameNumberUtil
	{
		public static FrameNumber NextFrameNumber(FrameNumber n)
		{
			return n + 1;
		}

		public static bool Adjucent(FrameNumber a, FrameNumber b)
		{
			return NextFrameNumber(a) == b;
		}

		public static int Compare(FrameNumber a, FrameNumber b)
		{
			return a - b;
		}
	}
}

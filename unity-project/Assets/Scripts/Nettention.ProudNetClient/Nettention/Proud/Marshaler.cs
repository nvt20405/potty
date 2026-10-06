using System;
using System.Net;

namespace Nettention.Proud
{
	public class Marshaler
	{
		public static void Read(Message msg, out byte b)
		{
			b = 0;
			msg.Read(out b);
		}

		public static void Read(Message msg, out sbyte b)
		{
			b = 0;
			msg.Read(out b);
		}

		public static void Read(Message msg, out ushort b)
		{
			b = 0;
			msg.Read(out b);
		}

		public static void Read(Message msg, out short b)
		{
			b = 0;
			msg.Read(out b);
		}

		public static void Read(Message msg, out uint b)
		{
			b = 0u;
			msg.Read(out b);
		}

		public static void Read(Message msg, out int b)
		{
			b = 0;
			msg.Read(out b);
		}

		public static void Read(Message msg, out ulong b)
		{
			b = 0uL;
			msg.Read(out b);
		}

		public static void Read(Message msg, out long b)
		{
			b = 0L;
			msg.Read(out b);
		}

		public static void Read(Message msg, out float b)
		{
			b = 0f;
			msg.Read(out b);
		}

		public static void Read(Message msg, out double b)
		{
			b = 0.0;
			msg.Read(out b);
		}

		public static void Read(Message msg, out HostID b)
		{
			b = HostID.None;
			msg.Read(out b);
		}

		public static void Read(Message msg, out ByteArray b)
		{
			b = new ByteArray();
			msg.Read(out b);
		}

		public static void Read(Message msg, out IPEndPoint b)
		{
			b = new IPEndPoint(0L, 0);
			msg.Read(out b);
		}

		public static void Read(Message msg, out ErrorType b)
		{
			b = ErrorType.Ok;
			msg.Read(out b);
		}

		public static void Read(Message msg, out bool b)
		{
			b = false;
			msg.Read(out b);
		}

		internal static void Read(Message msg, out TraceID b)
		{
			b = TraceID.System;
			byte b2 = 0;
			msg.Read(out b2);
			b = (TraceID)b2;
		}

		public static void Read(Message msg, out string b)
		{
			b = "";
			msg.Read(out b);
		}

		public static void Read(Message msg, out FrameNumber b)
		{
			b = (FrameNumber)0;
			msg.Read(out b);
		}

		public static void Read(Message msg, out Guid b)
		{
			msg.Read(out b);
		}

		public static void Read(Message msg, out NamedAddrPort b)
		{
			b = new NamedAddrPort();
			msg.Read(out b);
		}

		public static void Write(Message msg, byte b)
		{
			msg.Write(b);
		}

		public static void Write(Message msg, sbyte b)
		{
			msg.Write(b);
		}

		public static void Write(Message msg, ushort b)
		{
			msg.Write(b);
		}

		public static void Write(Message msg, short b)
		{
			msg.Write(b);
		}

		public static void Write(Message msg, uint b)
		{
			msg.Write(b);
		}

		public static void Write(Message msg, int b)
		{
			msg.Write(b);
		}

		public static void Write(Message msg, ulong b)
		{
			msg.Write(b);
		}

		public static void Write(Message msg, long b)
		{
			msg.Write(b);
		}

		public static void Write(Message msg, float b)
		{
			msg.Write(b);
		}

		public static void Write(Message msg, double b)
		{
			msg.Write(b);
		}

		public static void Write(Message msg, HostID b)
		{
			msg.Write(b);
		}

		public static void Write(Message msg, ErrorType b)
		{
			msg.Write(b);
		}

		public static void Write(Message msg, bool b)
		{
			msg.Write(b);
		}

		public static void Write(Message msg, IPEndPoint b)
		{
			msg.Write(b);
		}

		public static void Write(Message msg, ByteArray b)
		{
			msg.Write(b);
		}

		internal static void Write(Message msg, TraceID b)
		{
			msg.Write((byte)b);
		}

		public static void Write(Message msg, string b)
		{
			msg.Write(b);
		}

		public static void Write(Message msg, FrameNumber b)
		{
			msg.Write(b);
		}

		public static void Write(Message msg, Guid b)
		{
			msg.Write(b);
		}

		public static void Write(Message msg, NamedAddrPort b)
		{
			msg.Write(b);
		}
	}
}

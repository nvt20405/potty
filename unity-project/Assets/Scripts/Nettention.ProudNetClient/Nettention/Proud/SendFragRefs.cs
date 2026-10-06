using System;

namespace Nettention.Proud
{
	public class SendFragRefs
	{
		private const int MaxCount = 200;

		private Frag[] dataBuf = new Frag[200];

		private int length;

		public Frag[] FragmentData
		{
			get
			{
				return dataBuf;
			}
		}

		public int FragmentCount
		{
			get
			{
				return length;
			}
			private set
			{
				length = value;
			}
		}

		public int TotalLength
		{
			get
			{
				return GetTotalLength();
			}
		}

		public SendFragRefs()
		{
		}

		public SendFragRefs(SendFragRefs src)
		{
			if (src.length > 0)
			{
				Array.Copy(src.dataBuf, dataBuf, src.length);
			}
		}

		public SendFragRefs(Message msg)
		{
			Add(msg.Data.data, msg.Length);
		}

		public void Add(byte[] fragment, int length)
		{
			dataBuf[this.length] = new Frag(fragment, length);
			this.length++;
		}

		public void Add(Message msg)
		{
			Add(msg.Data.data, msg.Length);
		}

		public void Add(SendFragRefs src)
		{
			int fragmentCount = src.FragmentCount;
			int fragmentCount2 = FragmentCount;
			FragmentCount = fragmentCount2 + fragmentCount;
			for (int i = 0; i < fragmentCount; i++)
			{
				dataBuf[fragmentCount2 + i] = new Frag(src.dataBuf[i].data, src.dataBuf[i].Length);
			}
		}

		private int GetTotalLength()
		{
			int num = 0;
			int num2 = length;
			for (int i = 0; i < num2; i++)
			{
				num += dataBuf[i].Length;
			}
			return num;
		}

		public void Clear()
		{
			Array.Clear(dataBuf, 0, dataBuf.Length);
			length = 0;
		}

		public void ToAssembledByteArray(ByteArray output)
		{
			int fragmentCount = FragmentCount;
			for (int i = 0; i < fragmentCount; i++)
			{
				output.AddRange(dataBuf[i].data, dataBuf[i].Length);
			}
		}

		public void ToAssembledMessage(Message ret)
		{
			ret.Length = TotalLength;
			int fragmentCount = FragmentCount;
			int num = 0;
			for (int i = 0; i < fragmentCount; i++)
			{
				int num2 = dataBuf[i].Length;
				Array.Copy(dataBuf[i].data, 0, ret.Data.data, num, num2);
				num += num2;
			}
		}

		public void CopyTo(ByteArray ret)
		{
			ret.Count = TotalLength;
			int num = 0;
			int fragmentCount = FragmentCount;
			for (int i = 0; i < fragmentCount; i++)
			{
				int num2 = dataBuf[i].Length;
				Array.Copy(dataBuf[i].data, 0, ret.data, num, num2);
				num += num2;
			}
		}
	}
}

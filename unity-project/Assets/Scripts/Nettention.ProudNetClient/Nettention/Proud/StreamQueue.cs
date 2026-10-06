using System;

namespace Nettention.Proud
{
	internal class StreamQueue
	{
		private int growBy;

		internal byte[] block;

		internal int headIndex;

		private int contentsLength;

		public int Length
		{
			get
			{
				return contentsLength;
			}
		}

		public StreamQueue(int growBy)
		{
			this.growBy = growBy;
			block = new byte[growBy];
		}

		public void PushBack_Copy(byte[] data, int length)
		{
			if (headIndex + contentsLength + length < block.Length)
			{
				Array.Copy(data, 0, block, headIndex + contentsLength, length);
				contentsLength += length;
				return;
			}
			if (headIndex > 0 && block.Length > 0)
			{
				Shrink();
			}
			if (contentsLength + length > block.Length)
			{
				Array.Resize(ref block, contentsLength + length + growBy);
			}
			Array.Copy(data, 0, block, contentsLength, length);
			contentsLength += length;
		}

		private void Shrink()
		{
			if (contentsLength > 0)
			{
				Array.Copy(block, headIndex, block, 0, contentsLength);
			}
			headIndex = 0;
		}

		private void PushBack_Copy(SendFragRefs sendData)
		{
			for (int i = 0; i < sendData.FragmentCount; i++)
			{
				if (sendData.FragmentData[i].data != null)
				{
					PushBack_Copy(sendData.FragmentData[i].data, sendData.FragmentData[i].Length);
				}
			}
		}

		public int PopFront(int length)
		{
			length = Math.Min(length, contentsLength);
			headIndex += length;
			contentsLength -= length;
			if (contentsLength <= growBy / 64)
			{
				Shrink();
			}
			return length;
		}

		public int PopAll()
		{
			return PopFront(Length);
		}

		public void GetBlockedData(ref byte[] data, int length)
		{
			Array.Copy(block, headIndex, data, 0, length);
		}
	}
}

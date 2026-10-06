namespace Nettention.Proud
{
	public class Frag
	{
		public byte[] data;

		public int length;

		public byte[] Data
		{
			get
			{
				return data;
			}
		}

		public int Length
		{
			get
			{
				return length;
			}
		}

		public Frag()
		{
			data = null;
		}

		public Frag(byte[] fragment, int length)
		{
			data = fragment;
			this.length = length;
		}
	}
}

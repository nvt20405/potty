using System.Text;

namespace HTMLEngine
{
	internal class PStringBuilder : PoolableObject
	{
		private readonly StringBuilder sb = new StringBuilder();

		internal override void OnAcquire()
		{
		}

		internal override void OnRelease()
		{
			sb.Length = 0;
		}

		public void Append(char c)
		{
			sb.Append(c);
		}

		public void Append(string s)
		{
			sb.Append(s);
		}

		public override string ToString()
		{
			return sb.ToString();
		}

		public static implicit operator StringBuilder(PStringBuilder psb)
		{
			return psb.sb;
		}
	}
}

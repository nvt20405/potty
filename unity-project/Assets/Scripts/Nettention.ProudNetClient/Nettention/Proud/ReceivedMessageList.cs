using System.Collections.Generic;

namespace Nettention.Proud
{
	internal class ReceivedMessageList : List<ReceivedMessage>
	{
		public ReceivedMessageList()
		{
			base.Capacity = 1000;
		}
	}
}

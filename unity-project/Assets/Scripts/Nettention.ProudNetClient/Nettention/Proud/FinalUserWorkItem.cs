namespace Nettention.Proud
{
	internal class FinalUserWorkItem
	{
		public FinalUserWorkItemType type = FinalUserWorkItemType.Last;

		public ReceivedMessage unsafeMessage;

		public LocalEvent localEvent;

		public FinalUserWorkItem(ReceivedMessage msg, FinalUserWorkItemType type)
		{
			unsafeMessage = msg;
			this.type = type;
		}

		public FinalUserWorkItem(LocalEvent e)
		{
			localEvent = e;
			type = FinalUserWorkItemType.LocalEvent;
		}
	}
}

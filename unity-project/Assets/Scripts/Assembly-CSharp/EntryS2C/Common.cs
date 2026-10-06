using Nettention.Proud;

namespace EntryS2C
{
	public class Common
	{
		public const RmiID NotifyRegister = (RmiID)2001;

		public const RmiID NotifyFirstLogonSuccess = (RmiID)2002;

		public const RmiID NotifyError = (RmiID)2003;

		public const RmiID NotifyAck = (RmiID)2004;

		public static RmiID[] RmiIDList = new RmiID[4]
		{
			(RmiID)2001,
			(RmiID)2002,
			(RmiID)2003,
			(RmiID)2004
		};
	}
}

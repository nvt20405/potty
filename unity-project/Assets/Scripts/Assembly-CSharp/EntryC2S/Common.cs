using Nettention.Proud;

namespace EntryC2S
{
	public class Common
	{
		public const RmiID RequestRegister = (RmiID)1001;

		public const RmiID RequestFirstLogon = (RmiID)1002;

		public static RmiID[] RmiIDList = new RmiID[2]
		{
			(RmiID)1001,
			(RmiID)1002
		};
	}
}

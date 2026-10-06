namespace Nettention.Proud
{
	public interface IRmiHost
	{
		HostID LocalHostID { get; }

		void AttachProxy(RmiProxy proxy);

		void AttachStub(RmiStub stub);

		void DetachProxy(RmiProxy proxy);

		void DetachStub(RmiStub stub);

		bool SendByRmiProxy(SendFragRefs sendData, SendOpt sendContext, HostID[] sendTo);

		void PostCheckReadMessage(Message msg, string RMIName);

		void ShowNotImplementedRmiWarning(string RMIName);
	}
}

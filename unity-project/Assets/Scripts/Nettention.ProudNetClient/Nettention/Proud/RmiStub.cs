using System;

namespace Nettention.Proud
{
	public class RmiStub
	{
		public bool holsterMoreCallback_FORONETHREADEDMODEL;

		public bool postponeThisCallback_FORONETHREADEDMODEL;

		public IRmiHost core;

		public bool internalUse;

		public bool enableNotifyCallFromStub;

		public bool enableStubProfiling;

		public virtual RmiID[] GetRmiIDList { get; set; }

		public virtual int GetRmiIDListCount { get; set; }

		public virtual bool ProcessReceivedMessage(ReceivedMessage pa, object hostTag)
		{
			return false;
		}

		public virtual void AfterRmiInvocation(AfterRmiSummary summary)
		{
		}

		public virtual void BeforeRmiInvocation(BeforeRmiSummary summary)
		{
		}

		public virtual void NotifyCallFromStub(RmiID RMIId, string methodName, string parameters)
		{
		}

		public void ShowUnknownHostIDWarning(HostID remoteHostID)
		{
			Console.WriteLine(string.Format("Warning: unknown HostID {0} in ProcessReceivedMessage!", (int)remoteHostID));
		}

		public void HolsterMoreCallbackUntilNextFrameMove()
		{
			holsterMoreCallback_FORONETHREADEDMODEL = true;
		}

		public void PostponeThisCallback()
		{
			postponeThisCallback_FORONETHREADEDMODEL = true;
		}
	}
}

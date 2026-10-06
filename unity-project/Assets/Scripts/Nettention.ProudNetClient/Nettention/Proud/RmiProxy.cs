namespace Nettention.Proud
{
	public abstract class RmiProxy
	{
		internal IRmiHost core;

		public bool internalUse;

		public bool enableNotifySendByProxy = true;

		public RmiID[] RmiIDList
		{
			get
			{
				return GetRmiIDList();
			}
		}

		public abstract RmiID[] GetRmiIDList();

		public virtual void NotifySendByProxy(HostID sendTo, MessageSummary summary, RmiContext rmiContext)
		{
		}

		public bool RmiSend(HostID[] remotes, RmiContext rmiContext, Message msg, string rmiName, RmiID rmiID)
		{
			if (core == null)
			{
				Sysutil.ShowUserMisuseError("ProudNet RMI Proxy is not attached yet!");
				return false;
			}
			rmiContext.AssureValidation();
			Message message = new Message();
			message.Write(MessageType.Rmi);
			SendFragRefs sendFragRefs = new SendFragRefs(message);
			sendFragRefs.Add(msg.Data.data, msg.Length);
			bool result = core.SendByRmiProxy(sendFragRefs, new SendOpt(rmiContext), remotes);
			if (!internalUse)
			{
				MessageSummary messageSummary = new MessageSummary();
				messageSummary.payloadLength = sendFragRefs.TotalLength;
				messageSummary.rmiID = rmiID;
				messageSummary.rmiName = rmiName;
				messageSummary.encryptMode = rmiContext.encryptMode;
				messageSummary.compressMode = rmiContext.compressMode;
				if (enableNotifySendByProxy)
				{
					for (int i = 0; i < remotes.Length; i++)
					{
						NotifySendByProxy(remotes[i], messageSummary, rmiContext);
					}
				}
			}
			return result;
		}
	}
}

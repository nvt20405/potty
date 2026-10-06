using LitJson;
using Nettention.Proud;

namespace EntryS2C
{
	public class Proxy : IJsonProxy
	{
		public bool NotifyRegister(HostID remote, RmiContext rmiContext, string data)
		{
			CJsonTransport.Send("NotifyRegister", data);
			return true;
		}

		public bool NotifyRegister(HostID[] remotes, RmiContext rmiContext, string data)
		{
			return NotifyRegister(HostID.Server, rmiContext, data);
		}

		public bool NotifyFirstLogonSuccess(HostID remote, RmiContext rmiContext, string serverDic)
		{
			CJsonTransport.Send("NotifyFirstLogonSuccess", serverDic);
			return true;
		}

		public bool NotifyFirstLogonSuccess(HostID[] remotes, RmiContext rmiContext, string serverDic)
		{
			return NotifyFirstLogonSuccess(HostID.Server, rmiContext, serverDic);
		}

		public bool NotifyError(HostID remote, RmiContext rmiContext, int errorCode)
		{
			CJsonTransport.Send("NotifyError", JsonMapper.ToJson(new object[1] { errorCode }));
			return true;
		}

		public bool NotifyError(HostID[] remotes, RmiContext rmiContext, int errorCode)
		{
			return NotifyError(HostID.Server, rmiContext, errorCode);
		}

		public bool NotifyAck(HostID remote, RmiContext rmiContext, string msg)
		{
			CJsonTransport.Send("NotifyAck", msg);
			return true;
		}

		public bool NotifyAck(HostID[] remotes, RmiContext rmiContext, string msg)
		{
			return NotifyAck(HostID.Server, rmiContext, msg);
		}
	}
}

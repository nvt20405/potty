using System;

namespace Nettention.Proud
{
	public class NetConnectionParam : ICloneable
	{
		public string serverIP = "";

		public ushort serverPort;

		public Guid protocolVersion = Guid.Empty;

		public ByteArray userData = new ByteArray();

		public AsyncModel asyncModel;

		public FastArray<ushort> localUdpPortPool = new FastArray<ushort>();

		public long tunedNetworkerSendIntervalMs_TEST;

		object ICloneable.Clone()
		{
			return Clone();
		}

		public NetConnectionParam Clone()
		{
			return (NetConnectionParam)MemberwiseClone();
		}
	}
}

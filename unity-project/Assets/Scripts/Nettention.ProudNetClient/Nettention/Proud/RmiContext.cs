using System;

namespace Nettention.Proud
{
	public class RmiContext : ICloneable
	{
		public bool relayed;

		public HostID sentFrom;

		public int maxDirectP2PMulticastCount;

		public long uniqueID;

		public MessagePriority priority = MessagePriority.Medium;

		public MessageReliability reliability = MessageReliability.Reliable;

		public bool enableLoopback = true;

		public object hostTag;

		public bool enableP2PJitTrigger = true;

		public bool allowRelaySend = true;

		public double forceRelayThresholdRatio;

		public bool INTERNAL_USE_isProudNetSpecificRmi;

		public EncryptMode encryptMode;

		public CompressMode compressMode;

		public static readonly RmiContext ReliableSend = new RmiContext(MessagePriority.High, MessageReliability.Reliable, EncryptMode.None);

		public static readonly RmiContext UnreliableSend = new RmiContext(MessagePriority.Medium, MessageReliability.Unreliable, EncryptMode.None);

		public static readonly RmiContext FastEncryptedReliableSend = new RmiContext(MessagePriority.High, MessageReliability.Reliable, EncryptMode.Fast);

		public static readonly RmiContext FastEncryptedUnreliableSend = new RmiContext(MessagePriority.Medium, MessageReliability.Unreliable, EncryptMode.Fast);

		public static readonly RmiContext SecureReliableSend = new RmiContext(MessagePriority.High, MessageReliability.Reliable, EncryptMode.Secure);

		public static readonly RmiContext SecureUnreliableSend = new RmiContext(MessagePriority.Medium, MessageReliability.Unreliable, EncryptMode.Secure);

		internal static readonly RmiContext ReliableSendForPN = GetReliableSendForPN(EncryptMode.None);

		internal static readonly RmiContext UnreliableSendForPN = GetUnreliableSendForPN(EncryptMode.None);

		internal static readonly RmiContext SecureReliableSendForPN = GetReliableSendForPN(EncryptMode.Secure);

		internal static readonly RmiContext SecureUnreliableSendForPN = GetUnreliableSendForPN(EncryptMode.Secure);

		object ICloneable.Clone()
		{
			return Clone();
		}

		public RmiContext Clone()
		{
			return (RmiContext)MemberwiseClone();
		}

		public RmiContext()
		{
		}

		public RmiContext(MessagePriority priority, MessageReliability reliability, EncryptMode encryptMode)
		{
			this.priority = priority;
			this.reliability = reliability;
			this.encryptMode = encryptMode;
			maxDirectP2PMulticastCount = NetConfig.DefaultMaxDirectP2PMulticastCount;
		}

		public void AssureValidation()
		{
			if (reliability == MessageReliability.Unreliable && (priority < MessagePriority.High || priority > MessagePriority.Low))
			{
				throw new Exception("RMI messaging cannot have Engine level priority!");
			}
		}

		private static RmiContext GetReliableSendForPN(EncryptMode encryptMode)
		{
			RmiContext rmiContext = new RmiContext(MessagePriority.High, MessageReliability.Reliable, encryptMode);
			rmiContext.enableP2PJitTrigger = false;
			rmiContext.INTERNAL_USE_isProudNetSpecificRmi = true;
			return rmiContext;
		}

		private static RmiContext GetUnreliableSendForPN(EncryptMode encryptMode)
		{
			RmiContext rmiContext = new RmiContext(MessagePriority.Medium, MessageReliability.Unreliable, encryptMode);
			rmiContext.enableP2PJitTrigger = false;
			rmiContext.INTERNAL_USE_isProudNetSpecificRmi = true;
			return rmiContext;
		}
	}
}

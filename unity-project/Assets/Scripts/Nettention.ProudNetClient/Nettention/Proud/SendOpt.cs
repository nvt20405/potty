namespace Nettention.Proud
{
	public class SendOpt
	{
		public MessageReliability reliability = MessageReliability.Last;

		public int maxDirectBroadcastCount;

		public long uniqueID;

		public MessagePriority priority = MessagePriority.Last;

		public EncryptMode encryptMode;

		public CompressMode compressMode;

		public bool enableP2PJitTrigger = true;

		public bool enableLoopback = true;

		public bool allowRelaySend = true;

		public short ttl = -1;

		public double forceRelayThresholdRatio;

		public bool INTERNAL_USE_fraggingOnNeed = NetConfig.FraggingOnNeedByDefault;

		public bool INTERNAL_USE_isProudNetSpecificRmi;

		public SendOpt()
		{
		}

		public SendOpt(RmiContext rmiContext)
		{
			reliability = rmiContext.reliability;
			maxDirectBroadcastCount = rmiContext.maxDirectP2PMulticastCount;
			uniqueID = rmiContext.uniqueID;
			priority = rmiContext.priority;
			enableLoopback = rmiContext.enableLoopback;
			enableP2PJitTrigger = rmiContext.enableP2PJitTrigger;
			allowRelaySend = rmiContext.allowRelaySend;
			encryptMode = rmiContext.encryptMode;
			forceRelayThresholdRatio = rmiContext.forceRelayThresholdRatio;
			INTERNAL_USE_isProudNetSpecificRmi = rmiContext.INTERNAL_USE_isProudNetSpecificRmi;
			compressMode = rmiContext.compressMode;
		}

		public SendOpt(MessagePriority priority, bool isProudNetSpecificRmi)
		{
			this.priority = priority;
			INTERNAL_USE_isProudNetSpecificRmi = isProudNetSpecificRmi;
		}

		public SendOpt Clone()
		{
			SendOpt sendOpt = new SendOpt();
			sendOpt.reliability = reliability;
			sendOpt.maxDirectBroadcastCount = maxDirectBroadcastCount;
			sendOpt.uniqueID = uniqueID;
			sendOpt.priority = priority;
			sendOpt.encryptMode = encryptMode;
			sendOpt.compressMode = compressMode;
			sendOpt.enableP2PJitTrigger = enableP2PJitTrigger;
			sendOpt.enableLoopback = enableLoopback;
			sendOpt.allowRelaySend = allowRelaySend;
			sendOpt.ttl = ttl;
			sendOpt.forceRelayThresholdRatio = forceRelayThresholdRatio;
			sendOpt.INTERNAL_USE_fraggingOnNeed = INTERNAL_USE_fraggingOnNeed;
			sendOpt.INTERNAL_USE_isProudNetSpecificRmi = INTERNAL_USE_isProudNetSpecificRmi;
			return sendOpt;
		}
	}
}

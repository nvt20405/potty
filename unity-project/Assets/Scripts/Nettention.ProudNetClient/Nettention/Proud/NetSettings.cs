namespace Nettention.Proud
{
	internal class NetSettings
	{
		public FallbackMethod fallbackMethod;

		public int serverMessageMaxLength = NetConfig.MessageMaxLengthOrdinaryCase;

		public int clientMessageMaxLength = NetConfig.MessageMaxLengthOrdinaryCase;

		public int defaultTimeoutTimeMs = NetConfig.DefaultNoPingTimeoutTimeMs;

		public DirectP2PStartCondition directP2PStartCondition = NetConfig.DefaultDirectP2PStartCondition;

		public int overSendSuspectingThresholdInBytes = NetConfig.DefaultOverSendSuspectingThresholdInBytes;

		public bool enableNagleAlgorithm = true;

		public int encryptedMessageKeyLength = 128;

		public int fastEncryptedMessageKeyLength;

		public bool allowServerAsP2PGroupMember;

		public bool enableP2PEncryptedMessaging = true;

		public bool upnpDetectNatDevice = NetConfig.UpnpDetectNatDeviceByDefault;

		public bool upnpTcpAddPortMapping = NetConfig.UpnpTcpAddrPortMappingByDefault;

		public int emergencyLogLineCount;

		public bool enableLookaheadP2PSend = true;

		public bool enablePingTest;

		public bool ignoreFailedBindPort;
	}
}

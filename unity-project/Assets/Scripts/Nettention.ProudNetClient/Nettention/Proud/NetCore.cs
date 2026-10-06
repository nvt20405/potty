using System;
using System.Collections.Generic;
using Nettention.Proud.zlib;

namespace Nettention.Proud
{
	public abstract class NetCore : IRmiHost
	{
		public delegate void ErrorInfoDelegate(ErrorInfo errorInfo);

		public delegate void ExceptionDelegate(HostID remoteID, Exception e);

		public delegate void NoRmiProcessedDelegate(RmiID rmiID);

		public delegate void ReceiveUserMessageDelegate(HostID sender, RmiContext rmiContext, ByteArray payload);

		private const string DuplicatedRmiIDErrorText = "Duplicated RMI ID is found. Review RMI ID declaration in .PIDL files.";

		private const string AsyncCallbackMayOccurErrorText = "Already async callback may occur! server start or client connection should have not been done before here!";

		private const string BadRmiIDErrorText = "Wrong RMI ID is found. RMI ID should be >=1000 or <65530.";

		private const string RmiInterfaceErrorText = "IRmiProxy or IRmiStub pointer is invalid.";

		protected ErrorInfoDelegate errorHandler;

		protected ErrorInfoDelegate warningHandler;

		protected ErrorInfoDelegate informationHandler;

		protected ExceptionDelegate exceptionHandler;

		protected NoRmiProcessedDelegate noRmiProcessedHandler;

		protected ReceiveUserMessageDelegate receivedUserMessageHandler;

		protected List<RmiProxy> proxyList_NOCSLOCK = new List<RmiProxy>();

		protected List<RmiStub> stubList_NOCSLOCK = new List<RmiStub>();

		protected List<RmiID> proxyRmiIDList_NOCSLOCK = new List<RmiID>();

		protected List<RmiID> stubRmiIDList_NOCSLOCK = new List<RmiID>();

		public ErrorInfoDelegate ErrorHandler
		{
			set
			{
				errorHandler = value;
			}
		}

		public ErrorInfoDelegate WarningHandler
		{
			set
			{
				warningHandler = value;
			}
		}

		public ErrorInfoDelegate InformationHandler
		{
			set
			{
				informationHandler = value;
			}
		}

		public ExceptionDelegate ExceptionHandler
		{
			set
			{
				exceptionHandler = value;
			}
		}

		public NoRmiProcessedDelegate NoRmiProcessedHandler
		{
			set
			{
				noRmiProcessedHandler = value;
			}
		}

		public ReceiveUserMessageDelegate ReceivedUserMessageHandler
		{
			set
			{
				receivedUserMessageHandler = value;
			}
		}

		public virtual int MessageMaxLength
		{
			get
			{
				return NetConfig.MessageMaxLength;
			}
		}

		public virtual HostID LocalHostID
		{
			get
			{
				return HostID.None;
			}
		}

		internal abstract object GetCritSec();

		internal bool Send_SecureLayer(SendFragRefs payload, SendOpt sendContext, HostID[] sendTo)
		{
			bool result = false;
			if (sendContext.encryptMode != EncryptMode.None && sendContext.reliability != MessageReliability.Last)
			{
				HostID[] array;
				lock (GetCritSec())
				{
					array = ConvertGroupToIndividualsAndUnion(sendTo);
				}
				string errorOut = "";
				for (int i = 0; i < array.Length; i++)
				{
					HostID[] array2 = new HostID[1] { array[i] };
					if (LocalHostID == array2[0])
					{
						result = Send_BroadcastLayer(payload, sendContext, array2);
						continue;
					}
					SessionKey cryptSessionKey;
					if ((cryptSessionKey = GetCryptSessionKey(array2[0], ref errorOut)) != null)
					{
						lock (GetCritSec())
						{
							Message message = new Message();
							if (sendContext.reliability == MessageReliability.Reliable)
							{
								ushort output = 0;
								if (!NextEncryptCount(array2[0], ref output))
								{
									EnqueError(ErrorInfo.From(ErrorType.EncryptFail, array2[0], "NextEncryptCount 얻어오기 실패!!"));
									return false;
								}
								message.Write(output);
							}
							message.AppendFragments(payload);
							Message message2 = new Message();
							if ((sendContext.encryptMode != EncryptMode.Secure) ? CryptoRc4.EncryptMessage(cryptSessionKey.rc4Key, message, message2, 0) : CryptoAes.EncryptMessage(cryptSessionKey.aesKey, message, message2, 0))
							{
								Message message3 = new Message();
								if (sendContext.reliability == MessageReliability.Reliable)
								{
									message3.Write(MessageType.Encrypted_Reliable);
								}
								else
								{
									message3.Write(MessageType.Encrypted_UnReliable);
								}
								message3.Write(sendContext.encryptMode);
								message3.WriteScalar(message2.Length);
								SendFragRefs sendFragRefs = new SendFragRefs();
								sendFragRefs.Add(message3);
								sendFragRefs.Add(message2);
								result = Send_BroadcastLayer(sendFragRefs, sendContext, array2);
							}
							else
							{
								EnqueError(ErrorInfo.From(ErrorType.EncryptFail, array2[0], "Encrypt Error"));
								if (sendContext.reliability == MessageReliability.Reliable)
								{
									PrevEncryptCount(array2[0]);
								}
							}
						}
						continue;
					}
					lock (GetCritSec())
					{
						if (errorOut.Length > 0)
						{
							EnqueError(ErrorInfo.From(ErrorType.EncryptFail, array2[0], errorOut));
						}
						else
						{
							EnqueError(ErrorInfo.From(ErrorType.EncryptFail, array2[0], "StartServerParameter.m_enableP2PEncryptedMessaging=false. P2P Messaging can not encrypted!!"));
						}
					}
				}
				return result;
			}
			return Send_BroadcastLayer(payload, sendContext, sendTo);
		}

		internal bool Send_CompressLayer(SendFragRefs payload, SendOpt sendContext, HostID[] sendTo)
		{
			if (sendContext.compressMode != CompressMode.None && payload.TotalLength > 50)
			{
				Message message = new Message();
				message.AppendFragments(payload);
				int length = message.Length;
				Message message2 = new Message();
				message2.Length = length;
				int destLen = (int)Zlib.compressBound((uint)length);
				int num = Zlib.ZlibCompress(message2.Data.data, ref destLen, message.Data.data, length);
				if (num != 0)
				{
					string comment = string.Format("Packet compression failed! Error code={0}", num);
					EnqueError(ErrorInfo.From(ErrorType.CompressFail, sendTo[0], comment));
				}
				else if (destLen + 9 < payload.TotalLength)
				{
					message2.Length = destLen;
					Message message3 = new Message();
					message3.Write(MessageType.Compressed);
					message3.WriteScalar(message2.Length);
					message3.WriteScalar(payload.TotalLength);
					SendFragRefs sendFragRefs = new SendFragRefs();
					sendFragRefs.Add(message3);
					sendFragRefs.Add(message2);
					return Send_SecureLayer(sendFragRefs, sendContext, sendTo);
				}
			}
			return Send_SecureLayer(payload, sendContext, sendTo);
		}

		internal bool ProcessMessage_Compressed(ReceivedMessage receivedInfo, Message uncompressedOutput)
		{
			Message unsafeMessage = receivedInfo.unsafeMessage;
			int readOffset = unsafeMessage.ReadOffset;
			int a = 0;
			int a2 = 0;
			if (!unsafeMessage.ReadScalar(ref a) || !unsafeMessage.ReadScalar(ref a2))
			{
				unsafeMessage.ReadOffset = readOffset;
				return false;
			}
			if (a2 > MessageMaxLength)
			{
				unsafeMessage.ReadOffset = readOffset;
				return false;
			}
			int destLen = a2;
			uncompressedOutput.Length = a2;
			if (Zlib.ZlibUncompress(uncompressedOutput.Data.data, ref destLen, unsafeMessage.Data.data, unsafeMessage.ReadOffset, a) != 0 || destLen != a2)
			{
				unsafeMessage.ReadOffset = readOffset;
				return false;
			}
			return true;
		}

		internal virtual bool Send_BroadcastLayer(SendFragRefs payload, SendOpt sendContext, HostID[] sendTo)
		{
			return false;
		}

		internal virtual void EnqueError(ErrorInfo info)
		{
		}

		public abstract void EnqueWarning(ErrorInfo info);

		internal virtual bool AsyncCallbackMayOccur()
		{
			return false;
		}

		public void AttachProxy(RmiProxy proxy)
		{
			if (AsyncCallbackMayOccur())
			{
				throw new Exception("Already async callback may occur! server start or client connection should have not been done before here!");
			}
			if (proxy == null)
			{
				throw new Exception("IRmiProxy or IRmiStub pointer is invalid.");
			}
			RmiID[] rmiIDList = proxy.GetRmiIDList();
			for (int i = 0; i < proxy.RmiIDList.Length; i++)
			{
				if ((ushort)rmiIDList[i] < 100 || (ushort)rmiIDList[i] > ushort.MaxValue)
				{
					throw new Exception("Wrong RMI ID is found. RMI ID should be >=1000 or <65530.");
				}
				if (proxyRmiIDList_NOCSLOCK.Contains(rmiIDList[i]))
				{
					throw new Exception("Duplicated RMI ID is found. Review RMI ID declaration in .PIDL files.");
				}
				proxyRmiIDList_NOCSLOCK.Add(rmiIDList[i]);
			}
			proxy.core = this;
			proxyList_NOCSLOCK.Add(proxy);
		}

		public void AttachStub(RmiStub stub)
		{
			if (stub == null)
			{
				throw new Exception("IRmiProxy or IRmiStub pointer is invalid.");
			}
			RmiID[] getRmiIDList = stub.GetRmiIDList;
			for (int i = 0; i < stub.GetRmiIDListCount; i++)
			{
				if ((ushort)getRmiIDList[i] < 100 || (ushort)getRmiIDList[i] > ushort.MaxValue)
				{
					throw new Exception("Wrong RMI ID is found. RMI ID should be >=1000 or <65530.");
				}
				if (stubRmiIDList_NOCSLOCK.Contains(getRmiIDList[i]))
				{
					throw new Exception("Duplicated RMI ID is found. Review RMI ID declaration in .PIDL files.");
				}
				stubRmiIDList_NOCSLOCK.Add(getRmiIDList[i]);
			}
			stub.core = this;
			stubList_NOCSLOCK.Add(stub);
		}

		public void DetachProxy(RmiProxy proxy)
		{
			if (AsyncCallbackMayOccur())
			{
				throw new Exception("Already async callback may occur! server start or client connection should have not been done before here!");
			}
			for (int i = 0; i < proxyList_NOCSLOCK.Count; i++)
			{
				RmiProxy rmiProxy = proxyList_NOCSLOCK[i];
				if (rmiProxy == proxy)
				{
					RmiID[] rmiIDList = rmiProxy.RmiIDList;
					for (int j = 0; j < rmiProxy.RmiIDList.Length; j++)
					{
						RmiID item = rmiIDList[j];
						proxyRmiIDList_NOCSLOCK.Remove(item);
					}
					proxyList_NOCSLOCK.RemoveAt(i);
					proxy.core = null;
					break;
				}
			}
		}

		public void DetachStub(RmiStub stub)
		{
			for (int i = 0; i < stubList_NOCSLOCK.Count; i++)
			{
				RmiStub rmiStub = stubList_NOCSLOCK[i];
				if (rmiStub == stub)
				{
					RmiID[] getRmiIDList = rmiStub.GetRmiIDList;
					for (int j = 0; j < rmiStub.GetRmiIDListCount; j++)
					{
						RmiID item = getRmiIDList[j];
						stubRmiIDList_NOCSLOCK.Remove(item);
					}
					stubList_NOCSLOCK.RemoveAt(i);
					stub.core = null;
					break;
				}
			}
		}

		internal virtual bool NextEncryptCount(HostID remote, ref ushort output)
		{
			return false;
		}

		internal virtual void PrevEncryptCount(HostID remote)
		{
		}

		internal virtual bool GetExpectedDecryptCount(HostID remote, ref ushort output)
		{
			return false;
		}

		internal virtual bool NextDecryptCount(HostID remote)
		{
			return false;
		}

		internal virtual SessionKey GetCryptSessionKey(HostID remote, ref string errorOut)
		{
			return null;
		}

		public void ShowError_NOCSLOCK(ErrorInfo errorInfo)
		{
			errorHandler(errorInfo);
		}

		public virtual void ShowNotImplementedRmiWarning(string RMIName)
		{
			EnqueWarning(ErrorInfo.From(ErrorType.InvalidPacketFormat, LocalHostID, string.Format("Warning: Not Implemented Rmi {0} Called!", RMIName)));
		}

		public virtual void PostCheckReadMessage(Message msg, string RMIName)
		{
			if (msg.ReadOffset != msg.Length)
			{
				EnqueWarning(ErrorInfo.From(ErrorType.InvalidPacketFormat, LocalHostID, string.Format("Warning: Rmi Received Message {0} can't Read All!", RMIName)));
			}
		}

		internal bool ProcessMessage_Encrypted(MessageType msgType, ReceivedMessage receivedInfo, Message decryptedOutput)
		{
			Message readOnlyMessage = receivedInfo.ReadOnlyMessage;
			int readOffset = readOnlyMessage.ReadOffset;
			EncryptMode b = EncryptMode.None;
			int a = 0;
			if (!readOnlyMessage.Read(out b) || !readOnlyMessage.ReadScalar(ref a))
			{
				readOnlyMessage.ReadOffset = readOffset;
				return false;
			}
			string errorOut = "";
			SessionKey cryptSessionKey = GetCryptSessionKey(receivedInfo.remoteHostID, ref errorOut);
			bool flag = false;
			if (cryptSessionKey != null)
			{
				switch (b)
				{
				case EncryptMode.Secure:
					flag = CryptoAes.DecryptMessage(cryptSessionKey.aesKey, readOnlyMessage, decryptedOutput, readOnlyMessage.ReadOffset);
					break;
				case EncryptMode.Fast:
					flag = CryptoRc4.DecryptMessage(cryptSessionKey.rc4Key, readOnlyMessage, decryptedOutput, readOnlyMessage.ReadOffset);
					break;
				}
			}
			if (!flag)
			{
				lock (GetCritSec())
				{
					errorOut += " decrypt failed";
					EnqueError(ErrorInfo.From(ErrorType.DecryptFail, receivedInfo.remoteHostID, errorOut));
					readOnlyMessage.ReadOffset = readOffset;
					return false;
				}
			}
			if (msgType == MessageType.Encrypted_Reliable)
			{
				ushort b2 = 0;
				ushort output = 0;
				if (!decryptedOutput.Read(out b2))
				{
					lock (GetCritSec())
					{
						EnqueError(ErrorInfo.From(ErrorType.DecryptFail, receivedInfo.remoteHostID, "decryptCount1 read failed!!"));
						decryptedOutput.ReadOffset = readOffset;
						return false;
					}
				}
				if (!GetExpectedDecryptCount(receivedInfo.remoteHostID, ref output))
				{
					lock (GetCritSec())
					{
						EnqueError(ErrorInfo.From(ErrorType.DecryptFail, receivedInfo.remoteHostID, "GetExpectedDecryptCount failed!!"));
						decryptedOutput.ReadOffset = readOffset;
						return false;
					}
				}
				if (b2 != output)
				{
					lock (GetCritSec())
					{
						string comment = string.Format("decryptCount1({0}) != decryptCount2({1})", (int)b2, (int)output);
						EnqueError(ErrorInfo.From(ErrorType.DecryptFail, receivedInfo.remoteHostID, comment));
						decryptedOutput.ReadOffset = readOffset;
						return false;
					}
				}
				NextDecryptCount(receivedInfo.remoteHostID);
			}
			return true;
		}

		private void CheckDefaultTimeoutTimeValidation(long timeoutTime)
		{
			if (timeoutTime < NetConfig.DefaultNoPingTimeoutTimeMs)
			{
				throw new Exception("No ping timeout value is too small!");
			}
		}

		public bool SendUserMessage(HostID[] remotes, RmiContext rmiContext, byte[] payload)
		{
			rmiContext.AssureValidation();
			Message message = new Message();
			message.Write(MessageType.UserMessage);
			SendFragRefs sendFragRefs = new SendFragRefs();
			sendFragRefs.Add(message);
			sendFragRefs.Add(payload, payload.Length);
			return SendByRmiProxy(sendFragRefs, new SendOpt(rmiContext), remotes);
		}

		protected void CleanupEveryProxyAndStub()
		{
			for (int i = 0; i < proxyList_NOCSLOCK.Count; i++)
			{
				proxyList_NOCSLOCK[i].core = null;
			}
			proxyList_NOCSLOCK.Clear();
			for (int j = 0; j < stubList_NOCSLOCK.Count; j++)
			{
				stubList_NOCSLOCK[j].core = null;
			}
			stubList_NOCSLOCK.Clear();
		}

		public abstract bool SendByRmiProxy(SendFragRefs sendData, SendOpt sendContext, HostID[] sendTo);

		protected abstract HostID[] ConvertGroupToIndividualsAndUnion(HostID[] sendTo);
	}
}

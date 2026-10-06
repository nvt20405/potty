using System.Globalization;

namespace Nettention.Proud
{
	public class ErrorInfo
	{
		public ErrorType errorType;

		public ErrorType detailType;

		public HostID remote;

		public string comment = "";

		internal static ErrorInfo From(ErrorType errorType, HostID remote, string comment)
		{
			ErrorInfo errorInfo = new ErrorInfo();
			errorInfo.errorType = errorType;
			errorInfo.remote = remote;
			errorInfo.comment = comment;
			return errorInfo;
		}

		internal static ErrorInfo From(ErrorType errorType, HostID remote, string comment, ByteArray lastreceivedMessage)
		{
			InvalidPacketFormatErrorInfo invalidPacketFormatErrorInfo = new InvalidPacketFormatErrorInfo();
			invalidPacketFormatErrorInfo.errorType = errorType;
			invalidPacketFormatErrorInfo.remote = remote;
			invalidPacketFormatErrorInfo.comment = comment;
			if (lastreceivedMessage != null)
			{
				invalidPacketFormatErrorInfo.lastReceivedMessage = lastreceivedMessage;
			}
			return invalidPacketFormatErrorInfo;
		}

		public static string TypeToString(ErrorType e)
		{
			switch (CultureInfo.CurrentCulture.LCID)
			{
			case 1042:
				return TypeToString_Kor(e);
			default:
				return TypeToString_Eng(e);
			case 2052:
				return TypeToString_Chn(e);
			}
		}

		public override string ToString()
		{
			return string.Format("Error:{0},Detail:{1},Remote:{2},Comment:{3}", TypeToString(errorType), TypeToString(detailType), remote, comment);
		}

		public static string TypeToString_Kor(ErrorType e)
		{
			switch (e)
			{
			case ErrorType.Unexpected:
				return "의도되지 않은 상황이 발생했습니다.";
			case ErrorType.AlreadyConnected:
				return "이미 연결되어 있었습니다.";
			case ErrorType.TCPConnectFailure:
				return "TCP 연결이 실패했습니다.";
			case ErrorType.InvalidSessionKey:
				return "잘못된 세션 암호 키입니다.";
			case ErrorType.EncryptFail:
				return "암호화가 실패했습니다.";
			case ErrorType.DecryptFail:
				return "복호화 실패 혹은 해커에 의한 조작된 데이터입니다.";
			case ErrorType.ConnectServerTimeout:
				return "서버와의 연결 시도가 타임 아웃하였습니다.";
			case ErrorType.ProtocolVersionMismatch:
				return "서버와 프로토콜 버전이 맞지 않습니다.";
			case ErrorType.NotifyServerDeniedConnection:
				return "서버에서 연결을 거부했습니다.";
			case ErrorType.ConnectServerSuccessful:
				return "서버와의 연결이 성공했습니다.";
			case ErrorType.DisconnectFromRemote:
				return "상대측 호스트가 연결을 끊었습니다.";
			case ErrorType.DisconnectFromLocal:
				return "로컬 호스트에서 능동적으로 연결을 끊었습니다.";
			case ErrorType.DangerousArgumentWarning:
				return "위험한 호출 파라메터가 있습니다.";
			case ErrorType.UnknownAddrPort:
				return "알 수 없는 인터넷 주소입니다.";
			case ErrorType.ServerNotReady:
				return "서버가 준비되지 않았습니다.";
			case ErrorType.ServerPortListenFailure:
				return "서버 소켓의 listen을 시작할 수 없습니다. TCP 또는 UDP 소켓이 이미 사용중인 포트인지 확인하십시오.";
			case ErrorType.AlreadyExists:
				return "이미 개체가 존재합니다.";
			case ErrorType.PermissionDenied:
				return "접근이 거부되었습니다.";
			case ErrorType.BadSessionGuid:
				return "잘못된 session Guid입니다.";
			case ErrorType.InvalidCredential:
				return "잘못된 credential입니다.";
			case ErrorType.InvalidHeroName:
				return "잘못된 hero name입니다.";
			case ErrorType.LoadDataPreceded:
				return "로딩 과정이 unlock 후 lock 한 후 꼬임이 발생했습니다.";
			case ErrorType.AdjustedGamerIDNotFilled:
				return "출력 파라메터 AdjustedGamerIDNotFilled가 채워지지 않았습니다.";
			case ErrorType.NoHero:
				return "플레이어 캐릭터가 존재하지 않습니다.";
			case ErrorType.UnitTestFailed:
				return "UnitTestFailed";
			case ErrorType.P2PUdpFailed:
				return "peer-to-peer UDP 통신이 막혔습니다.";
			case ErrorType.ReliableUdpFailed:
				return "P2P reliable UDP가 실패했습니다.";
			case ErrorType.ServerUdpFailed:
				return "클라이언트-서버 UDP 통신이 막혔습니다.";
			case ErrorType.NoP2PGroupRelation:
				return "더 이상 같이 소속된 P2P 그룹이 없습니다.";
			case ErrorType.ExceptionFromUserFunction:
				return "사용자 정의 함수(RMI 수신 루틴 혹은 이벤트 핸들러)에서 exception이 throw되었습니다.";
			case ErrorType.UserRequested:
				return "사용자의 요청에 의한 실패입니다.";
			case ErrorType.InvalidPacketFormat:
				return "잘못된 패킷 형식입니다. 상대측 호스트가 해킹되었거나 버그일 수 있습니다.";
			case ErrorType.TooLargeMessageDetected:
				return "너무 큰 크기의 메시징이 시도되었습니다. 기술지원부에 문의하십시오.";
			case ErrorType.CannotEncryptUnreliableMessage:
				return "Unreliable 메세지는 암호화할 수 없습니다.";
			case ErrorType.ValueNotExist:
				return "존재하지 않는 값입니다.";
			case ErrorType.TimeOut:
				return "타임 아웃입니다.";
			case ErrorType.LoadedDataNotFound:
				return "로드된 데이터를 찾을 수 없습니다.";
			case ErrorType.SendQueueIsHeavy:
				return "송신 queue가 너무 많이 쌓여 있습니다. 송신량을 조절하는 것을 권장합니다.";
			case ErrorType.TooSlowHeartbeatWarning:
				return "Heartbeat가 너무 늦게 호출되고 있습니다.기아화를 의심하세요.";
			case ErrorType.CompressFail:
				return "메시지 압축을 푸는데 실패 하였습니다.";
			case ErrorType.LocalSocketCreationFailed:
				return "클라이언트 소켓의 listen을 시작할 수 없습니다. TCP 또는 UDP 소켓이 이미 사용중인 포트인지 확인하십시오.";
			case ErrorType.NoneAvailableInPortPool:
				return "Socket을 생성할 때 Port Pool 내 port number로의 bind가 실패했습니다. 대신 임의의 port number가 사용되었습니다. Port Pool의 갯수가 충분한지 확인하십시요.";
			case ErrorType.InvalidPortPool:
				return "Port pool 내 값들 중 하나 이상이 잘못되었습니다. 포트를 0(임의 포트 바인딩)으로 하거나 중복되지 않았는지 확인하십시요.";
			case ErrorType.InvalidHostID:
				return "유효하지 않은 HostID입니다.";
			case ErrorType.MessageOverload:
				return "사용자가 소화하는 메시지 처리 속도보다 내부적으로 쌓이는 메시지의 속도가 더 높습니다. 지나치게 너무 많은 메시지를 송신하려고 했는지, 혹은 사용자의 메시지 수신 함수가 지나치게 느리게 작동하고 있는지 확인하십시오.";
			default:
				return "<none>";
			}
		}

		public static string TypeToString_Eng(ErrorType e)
		{
			switch (e)
			{
			case ErrorType.Unexpected:
				return "Unexpected Error.";
			case ErrorType.AlreadyConnected:
				return "Already connected.";
			case ErrorType.TCPConnectFailure:
				return "TCP connection failure.";
			case ErrorType.InvalidSessionKey:
				return "Invalid session key.";
			case ErrorType.EncryptFail:
				return "Encryption failed.";
			case ErrorType.DecryptFail:
				return "Decryption failed or hack suspected.";
			case ErrorType.ConnectServerTimeout:
				return "Connect to server timed out.";
			case ErrorType.ProtocolVersionMismatch:
				return "Mispatched protocol between hosts.";
			case ErrorType.NotifyServerDeniedConnection:
				return "Server denied connection attempt.";
			case ErrorType.ConnectServerSuccessful:
				return "Connecting to server successful.";
			case ErrorType.DisconnectFromRemote:
				return "Remote host disconnected.";
			case ErrorType.DisconnectFromLocal:
				return "Local host disconnected.";
			case ErrorType.DangerousArgumentWarning:
				return "Dangerous parameters are detected.";
			case ErrorType.UnknownAddrPort:
				return "Unknown Internet address.";
			case ErrorType.ServerNotReady:
				return "Server is not ready.";
			case ErrorType.ServerPortListenFailure:
				return "Server socket listen failure. Make sure that the TCP or UDP listening port is not already in use.";
			case ErrorType.AlreadyExists:
				return "Object already exists.";
			case ErrorType.PermissionDenied:
				return "Permission denied.";
			case ErrorType.BadSessionGuid:
				return "Bad session Guid.";
			case ErrorType.InvalidCredential:
				return "Invalid credential.";
			case ErrorType.InvalidHeroName:
				return "Invalid player character name.";
			case ErrorType.LoadDataPreceded:
				return "Corruption occurred while unlocked loading and locking.";
			case ErrorType.AdjustedGamerIDNotFilled:
				return "Output parameter AdjustedGamerIDNotFilled is not filled.";
			case ErrorType.NoHero:
				return "No Player Character(Hero) Found.";
			case ErrorType.UnitTestFailed:
				return "UnitTestFailed";
			case ErrorType.P2PUdpFailed:
				return "peer-to-peer UDP comm is blocked.";
			case ErrorType.ReliableUdpFailed:
				return "P2P reliable UDP failed.";
			case ErrorType.ServerUdpFailed:
				return "Client-server UDP comm is blocked.";
			case ErrorType.NoP2PGroupRelation:
				return "No common P2P group exists anymore.";
			case ErrorType.ExceptionFromUserFunction:
				return "An exception is thrown from user function. It may be an RMI function or event handler.";
			case ErrorType.UserRequested:
				return "By user request.";
			case ErrorType.InvalidPacketFormat:
				return "Invalid packet format. Remote host is hacked or has a bug.";
			case ErrorType.TooLargeMessageDetected:
				return "Too large message is detected. Contact technical supports.";
			case ErrorType.CannotEncryptUnreliableMessage:
				return "An unreliable message cannot be encrypted.";
			case ErrorType.ValueNotExist:
				return "Not exist value.";
			case ErrorType.TimeOut:
				return "Working is timeout.";
			case ErrorType.LoadedDataNotFound:
				return "Can not found loaddata.";
			case ErrorType.SendQueueIsHeavy:
				return "SendQueue has Accumulated too much.";
			case ErrorType.TooSlowHeartbeatWarning:
				return "Heartbeat Call in too slow.Suspected starvation";
			case ErrorType.CompressFail:
				return "Message uncompress fail.";
			case ErrorType.LocalSocketCreationFailed:
				return "Unable to start listening of client socket. Must check if either TCP or UDP socket is already in use.";
			case ErrorType.NoneAvailableInPortPool:
				return "Failed binding to local port that defined in Port Pool. Please check number of values in Port Pool are sufficient.";
			case ErrorType.InvalidPortPool:
				return "Range of user defined port is wrong. Set port to 0(random port binding) or check if it is overlaped.";
			case ErrorType.InvalidHostID:
				return "Invalid HostID.";
			case ErrorType.MessageOverload:
				return "The speed of stacking messages are higher than the speed of processing them. Check that you are sending too many messages, or your message processing routines are running too slowly.";
			default:
				return "<none>";
			}
		}

		public static string TypeToString_Chn(ErrorType e)
		{
			switch (e)
			{
			case ErrorType.Unexpected:
				return "发生了以外的情况.";
			case ErrorType.AlreadyConnected:
				return "已连接.";
			case ErrorType.TCPConnectFailure:
				return "TCP/IP 连接失败.";
			case ErrorType.InvalidSessionKey:
				return "session key不正确.";
			case ErrorType.EncryptFail:
				return "暗号化失败.";
			case ErrorType.DecryptFail:
				return "符号化失败或被操作的数据.";
			case ErrorType.ConnectServerTimeout:
				return "服务器连接超时.";
			case ErrorType.ProtocolVersionMismatch:
				return "跟服务器的版本不同 需要上级.";
			case ErrorType.NotifyServerDeniedConnection:
				return "被据否了服务器连接.";
			case ErrorType.ConnectServerSuccessful:
				return "服务器连接成功.";
			case ErrorType.DisconnectFromRemote:
				return "Remote host disconnected.";
			case ErrorType.DisconnectFromLocal:
				return "Local host disconnected.";
			case ErrorType.DangerousArgumentWarning:
				return "Dangerous parameters are detected.";
			case ErrorType.UnknownAddrPort:
				return "在网上没有地址.";
			case ErrorType.ServerNotReady:
				return "服务器还没准备.";
			case ErrorType.ServerPortListenFailure:
				return "Server socket listen failure. Make sure that the TCP or UDP listening port is not already in use.";
			case ErrorType.AlreadyExists:
				return "个体已存在.";
			case ErrorType.PermissionDenied:
				return "访问被拒绝.";
			case ErrorType.BadSessionGuid:
				return "session Guid不正确.";
			case ErrorType.InvalidCredential:
				return "credential不正确.";
			case ErrorType.InvalidHeroName:
				return "hero name不正确.";
			case ErrorType.LoadDataPreceded:
				return "加载过程unlock后与lock后发生隔阂.";
			case ErrorType.AdjustedGamerIDNotFilled:
				return "Output parameter AdjustedGamerIDNotFilled is not filled.";
			case ErrorType.NoHero:
				return "No Player Character(Hero) Found.";
			case ErrorType.UnitTestFailed:
				return "UnitTestFailed";
			case ErrorType.P2PUdpFailed:
				return "peer-to-peer UDP comm is blocked.";
			case ErrorType.ReliableUdpFailed:
				return "P2P reliable UDP failed.";
			case ErrorType.ServerUdpFailed:
				return "Client-server UDP comm is blocked.";
			case ErrorType.NoP2PGroupRelation:
				return "No common P2P group exists anymore.";
			case ErrorType.ExceptionFromUserFunction:
				return "An exception is thrown from user function. It may be an RMI function or event handler.";
			case ErrorType.UserRequested:
				return "By user request.";
			case ErrorType.InvalidPacketFormat:
				return "Invalid packet format. Remote host is hacked or has a bug.";
			case ErrorType.TooLargeMessageDetected:
				return "Too large message is detected. Contact technical supports.";
			case ErrorType.CannotEncryptUnreliableMessage:
				return "An unreliable message cannot be encrypted.";
			case ErrorType.ValueNotExist:
				return "Not exist value.";
			case ErrorType.TimeOut:
				return "Working is timeout.";
			case ErrorType.LoadedDataNotFound:
				return "Can not found loaddata.";
			case ErrorType.SendQueueIsHeavy:
				return "SendQueue has Accumulated too much.";
			case ErrorType.TooSlowHeartbeatWarning:
				return "Heartbeat Call in too slow.Suspected starvation";
			case ErrorType.CompressFail:
				return "Message uncompress fail.";
			case ErrorType.LocalSocketCreationFailed:
				return "Unable to start listening of client socket. Must check if either TCP or UDP socket is already in use.";
			case ErrorType.NoneAvailableInPortPool:
				return "Failed binding to local port that defined in Port Pool. Please check number of values in Port Pool are sufficient.";
			case ErrorType.InvalidPortPool:
				return "Range of user defined port is wrong. Set port to 0(random port binding) or check if it is overlaped.";
			case ErrorType.InvalidHostID:
				return "Invalid HostID.";
			case ErrorType.MessageOverload:
				return "消息的堆叠速度高于处理它们的速度。检查您要发送的邮件太多，或消息处理程序运行过慢。";
			default:
				return "<none>";
			}
		}
	}
}

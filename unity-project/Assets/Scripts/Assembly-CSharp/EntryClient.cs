using System;
using EntryC2S;
using EntryS2C;
using LitJson;
using Nettention.Proud;
using UnityEngine;

public class EntryClient : MonoBehaviour
{
	public class SohaUserInfo
	{
		public string id = string.Empty;

		public string username = string.Empty;

		public string avatar = string.Empty;
	}

	public class SohaLoginResponse
	{
		public string status = string.Empty;

		public string type = string.Empty;

		public SohaUserInfo user_info = new SohaUserInfo();

		public string access_token = string.Empty;

		public string message = string.Empty;
	}

	private string m_failMessage = string.Empty;

	private CJsonTransport m_transport;

	private EntryC2S.Proxy m_C2SProxy = new EntryC2S.Proxy();

	private EntryS2C.Stub m_S2CStub = new EntryS2C.Stub();

	private HostID m_myP2PGroupID;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void Login(string userName, string password)
	{
		LoginRequest loginRequest = new LoginRequest();
		loginRequest.user = userName;
		loginRequest.pass = password;
		loginRequest.Version = GameManager.GAMER_VERSION;
		GameManager.instance.m_userName = userName;
		loginRequest.PublisherAccessToken = "direct";
		loginRequest.Platform = "PC";
		SendRequest(m_C2SProxy.RequestFirstLogon, JsonMapper.ToJson(loginRequest, false));
	}

	public void LoginSoha(string userName, string accessToken)
	{
		GameManager.instance.m_accessToken = accessToken;
		LoginRequest loginRequest = new LoginRequest();
		loginRequest.user = userName;
		loginRequest.PublisherAccessToken = accessToken;
		loginRequest.Version = GameManager.GAMER_VERSION;
		loginRequest.Platform = "Android";
		SendRequest(m_C2SProxy.RequestFirstLogon, JsonMapper.ToJson(loginRequest, false));
	}

	public void OnGamerVersionKhongDuOk()
	{
		Application.OpenURL(GameManager.instance.m_LoginResponse.LoginCfg.androidUrl);
	}

	public bool OnFirstLogonSuccess(HostID hostId, RmiContext context, string data)
	{
		OnGetResponse();
		LoginResponse loginResponse = JsonMapper.ToObject<LoginResponse>(data);
		if (loginResponse != null)
		{
			if (loginResponse.ErrorCode == ERROR_CODE.OK)
			{
				base.gameObject.SetActive(false);
				GameManager.instance.m_LoginResponse = loginResponse;
				if (loginResponse.RealUserName.Length > 0)
				{
					GameManager.instance.m_userName = loginResponse.RealUserName;
				}
				if (loginResponse.RealPublisherAccessToken != null && loginResponse.RealPublisherAccessToken.Length > 0)
				{
					GameManager.instance.m_accessToken = loginResponse.RealPublisherAccessToken;
				}
				PlayerPrefs.SetString("UserAccount", GameManager.instance.m_userName);
				GameManager.instance.StartDownloadConfig();
			}
			else
			{
				GameManager.instance.m_accessToken = string.Empty;
				string text = string.Format("ErrorCode: {0}, Message: {1}", loginResponse.ErrorCode, loginResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			string text2 = "OnFirstLogonSuccess : response null";
			MessagePopup.Create(text2);
			EGDebug.LogError(text2);
		}
		return true;
	}

	public bool OnRegisterSuccess(HostID hostId, RmiContext context, string data)
	{
		OnGetResponse();
		LoginResponse loginResponse = JsonMapper.ToObject<LoginResponse>(data);
		if (loginResponse != null)
		{
			if (loginResponse.ErrorCode == ERROR_CODE.OK)
			{
				EGDebug.LogWarning("[EntryClient] OnRegisterSuccess: OK");
				MessagePopup.Create("Đăng ký tài khoản thành công!");
				PlayerPrefs.SetString("UserAccount", GameManager.instance.m_userName);
				if (!string.IsNullOrEmpty(GameManager.instance.m_userPass))
				{
					PlayerPrefs.SetString("UserPassword", GameManager.instance.m_userPass);
				}
				GUIManager.setScreen(GAME_SCREEN.ScreenLogin);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", loginResponse.ErrorCode, loginResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			string text2 = "OnRegisterSuccess : response null";
			MessagePopup.Create(text2);
			EGDebug.LogError(text2);
		}
		return true;
	}

	public void Init()
	{
		m_transport = new CJsonTransport(OnTransportMessage);
		S2CStubMessage_Start();
	}

	public void StartLogin(string userName, string passWord, string token)
	{
		if (string.IsNullOrEmpty(userName))
		{
			userName = PlayerPrefs.GetString("UserAccount", "player1");
		}
		if (string.IsNullOrEmpty(passWord))
		{
			passWord = PlayerPrefs.GetString("UserPassword", "1");
		}
		EGDebug.LogWarning("[EntryClient] StartLogin sending: user='" + userName + "', pass='" + passWord + "'");
		Login(userName, passWord);
	}

	private void OnTransportMessage(string rmiName, string data)
	{
		try
		{
			m_S2CStub.Dispatch(rmiName, data);
		}
		catch (Exception ex)
		{
			EGDebug.LogError("[EntryClient] Dispatch " + rmiName + " error: " + ((ex != null) ? ex.ToString() : null));
		}
	}

	private void OnTransportConnected()
	{
		PopupNetworkLoading.DestroyPopup();
		GAME_SCREEN gAME_SCREEN = GAME_SCREEN.ScreenLogin;
		if (GUIManager.instance != null && GUIManager.instance.CurrentScreen != GAME_SCREEN.ScreenStart)
		{
			gAME_SCREEN = GUIManager.instance.CurrentScreen;
		}
		else if (GameManager.instance.currentScreen != null && GameManager.instance.currentScreen.screenId != GAME_SCREEN.ScreenStart)
		{
			gAME_SCREEN = GameManager.instance.currentScreen.screenId;
		}
		EGDebug.LogWarning("[EntryClient] OnTransportConnected screenId: " + gAME_SCREEN);
		switch (gAME_SCREEN)
		{
		case GAME_SCREEN.ScreenLogin:
			StartLogin(GameManager.instance.m_userName, GameManager.instance.m_userPass, GameManager.instance.m_accessToken);
			break;
		case GAME_SCREEN.ScreenReg:
		{
			EGDebug.LogWarning("[EntryClient] Send RequestRegister: user='" + GameManager.instance.m_userName + "'");
			LoginRequest loginRequest = new LoginRequest();
			loginRequest.user = GameManager.instance.m_userName;
			loginRequest.pass = GameManager.instance.m_userPass;
			loginRequest.PublisherAccessToken = "direct";
			SendRequest(m_C2SProxy.RequestRegister, JsonMapper.ToJson(loginRequest, false));
			break;
		}
		default:
			EGDebug.LogWarning("[EntryClient] Unknown screen on connected: " + gAME_SCREEN.ToString() + ", default to Login");
			StartLogin(GameManager.instance.m_userName, GameManager.instance.m_userPass, GameManager.instance.m_accessToken);
			break;
		}
	}

	private void OnTransportDisconnected()
	{
		MessagePopup.Create(Localization.instance.Get("ConnectNetworkError"));
		PopupNetworkLoading.DestroyPopup();
	}

	private void Update()
	{
		if (m_transport != null)
		{
			m_transport.Pump();
		}
	}

	private void OnDestroy()
	{
		if (m_transport != null)
		{
			m_transport.Dispose();
		}
		m_transport = null;
	}

	public void IssueConnect()
	{
		if (m_transport == null)
		{
			Init();
		}
		base.gameObject.SetActive(true);
		m_transport.OnConnected = OnTransportConnected;
		m_transport.OnDisconnected = OnTransportDisconnected;
		m_transport.SetAsGlobal();
		m_transport.Connect();
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public void Disconnect()
	{
		if (m_transport != null)
		{
			m_transport.Disconnect();
		}
	}

	public void SendRequest(Func<HostID, RmiContext, string, bool> func, string data, bool showLoading = true)
	{
		func(HostID.Server, RmiContext.ReliableSend, data);
		if (showLoading)
		{
			PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		}
	}

	public void OnGetResponse()
	{
		PopupNetworkLoading.DestroyPopup();
	}

	private void S2CStubMessage_Start()
	{
		m_S2CStub.NotifyFirstLogonSuccess = OnFirstLogonSuccess;
		m_S2CStub.NotifyRegister = OnRegisterSuccess;
		m_S2CStub.NotifyError = (HostID remote, RmiContext rmiContext, int errorCode) =>
		{
			PopupNetworkLoading.DestroyPopup();
			switch ((ERROR_CODE)errorCode)
			{
			case ERROR_CODE.ACCOUNT_DA_TON_TAI:
				MessagePopup.Create(Localization.instance.Get("DaCoTaiKhoanNayRoi"));
				break;
			case ERROR_CODE.SAI_MAT_KHAU:
				MessagePopup.Create(Localization.instance.Get("MatKhauKhongDung"));
				break;
			case ERROR_CODE.ACCOUNT_CHUA_TON_TAI:
				MessagePopup.Create(Localization.instance.Get("TaiKhoanChuaCo"));
				break;
			default:
				MessagePopup.Create("Mã lỗi: " + errorCode);
				break;
			}
			return true;
		};
		m_S2CStub.NotifyAck = (HostID remote, RmiContext rmiContext, string msg) => true;
	}
}

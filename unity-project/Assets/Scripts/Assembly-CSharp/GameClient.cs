using System;
using System.Collections;
using System.Collections.Generic;
using GameC2C;
using GameC2S;
using GameS2C;
using LitJson;
using Nettention.Proud;
using UnityEngine;

public class GameClient : MonoBehaviour
{
	public class EGPaymentInfo
	{
		public string orderId;

		public DateTime orderTime;

		public PaymentRequest GetPaymentRequest()
		{
			PaymentRequest paymentRequest = new PaymentRequest();
			paymentRequest.OrderID = orderId;
			return paymentRequest;
		}

		public static EGPaymentInfo GetEGPayment(PaymentRequest request)
		{
			EGPaymentInfo eGPaymentInfo = new EGPaymentInfo();
			eGPaymentInfo.orderId = request.OrderID;
			eGPaymentInfo.orderTime = DateTime.Now;
			return eGPaymentInfo;
		}
	}

	private Func<HostID, RmiContext, string, bool> m_requestFuncPending;

	private string m_requestDataPending;

	public bool isIpV6;

	public List<UserInfo> m_listLKUser;

	private UserInfo m_UserInfo = new UserInfo();

	private string m_failMessage = string.Empty;

	public CJsonTransport m_transport;

	private bool _sessionReady;

	private bool _paused;

	private float _nextReconnect;

	private float _retryDelay = 1f;

	public GameC2S.Proxy m_C2SProxy = new GameC2S.Proxy();

	private GameS2C.Stub m_S2CStub = new GameS2C.Stub();

	public GameC2C.Proxy m_C2CProxy = new GameC2C.Proxy();

	private GameC2C.Stub m_C2CStub = new GameC2C.Stub();

	private HostID m_myP2PGroupID;

	public long ServerTimeDiffTick;

	public bool isAutoChienTruong;

	private KetQuaThachDauResponse _ketQuaThachDauResponse;

	private bool FailedStartDongNhan;

	private DateTime LastTimeBanPhaoHoa = DateTime.Now;

	public HomeResponse HomeResponse { get; private set; }

	public SieuCupChampionInfo SieuCupChampionResponse { get; private set; }

	public GetBaoKhoInfoResponse BaoKhoInfoResponse { get; private set; }

	public DateTime LastTimeGetBaoKhoInfo { get; private set; }

	public GetHoaVangInfoResponse HoaVangInfoResponse { get; private set; }

	public DateTime LastTimeGetHoaVangInfo { get; private set; }

	public UserInfo UserInfo
	{
		get
		{
			return m_UserInfo;
		}
		private set
		{
			m_UserInfo = value;
		}
	}

	public GameC2S.Proxy C2SProxy
	{
		get
		{
			return m_C2SProxy;
		}
		set
		{
			m_C2SProxy = value;
		}
	}

	public GameC2C.Proxy C2CProxy
	{
		get
		{
			return m_C2CProxy;
		}
		set
		{
			m_C2CProxy = value;
		}
	}

	public DateTime ServerTime
	{
		get
		{
			return DateTime.Now + new TimeSpan(ServerTimeDiffTick);
		}
	}

	private void Awake()
	{
	}

	public void Init()
	{
		m_transport = new CJsonTransport(OnTransportMessage);
		S2CStubMessage_Start();
	}

	public void SendRequest(Func<HostID, RmiContext, string, bool> func, string data, bool showLoading = true)
	{
		m_requestFuncPending = null;
		m_requestDataPending = string.Empty;
		if (m_transport == null || m_transport.State != CJsonTransport.EState.Connected)
		{
			_nextReconnect = 0f;
			return;
		}
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

	public IEnumerator LoadAllCfg(float startPercent = 0.2f, float stopPercent = 0.4f)
	{
		if (ConfigManager.instance.m_dicNhanVats == null || ConfigManager.instance.m_dicNhanVats.Count <= 0)
		{
			string local_folder = "Config";
			TextAsset vocongTxt = (TextAsset)Resources.Load(local_folder + "/VoCong", typeof(TextAsset));
			if (PopupLoading.instance != null)
			{
				PopupLoading.instance.SetAmmount(startPercent + (stopPercent - startPercent) * 0.05f);
			}
			yield return null;
			TextAsset trangbiTxt = (TextAsset)Resources.Load(local_folder + "/TrangBi", typeof(TextAsset));
			if (PopupLoading.instance != null)
			{
				PopupLoading.instance.SetAmmount(startPercent + (stopPercent - startPercent) * 0.1f);
			}
			yield return null;
			TextAsset nhanVatTxt = (TextAsset)Resources.Load(local_folder + "/NhanVat", typeof(TextAsset));
			if (PopupLoading.instance != null)
			{
				PopupLoading.instance.SetAmmount(startPercent + (stopPercent - startPercent) * 0.17f);
			}
			yield return null;
			TextAsset otherTxt = (TextAsset)Resources.Load(local_folder + "/Other", typeof(TextAsset));
			if (PopupLoading.instance != null)
			{
				PopupLoading.instance.SetAmmount(startPercent + (stopPercent - startPercent) * 0.2f);
			}
			yield return null;
			TextAsset vatphamtieuthuTxt = (TextAsset)Resources.Load(local_folder + "/VatPhamTieuThu", typeof(TextAsset));
			if (PopupLoading.instance != null)
			{
				PopupLoading.instance.SetAmmount(startPercent + (stopPercent - startPercent) * 0.21f);
			}
			yield return null;
			float t = startPercent + (stopPercent - startPercent) * 0.4f;
			if (PopupLoading.instance != null)
			{
				PopupLoading.instance.SetAmmount(t);
			}
			yield return null;
			TextAsset vcTimeEffTxt = (TextAsset)Resources.Load(local_folder + "/VCTimeEffect", typeof(TextAsset));
			if (PopupLoading.instance != null)
			{
				PopupLoading.instance.SetAmmount(startPercent + (stopPercent - startPercent) * 0.41f);
			}
			yield return null;
			TextAsset attackTimeEffTxt = (TextAsset)Resources.Load(local_folder + "/AttackTimeEffect", typeof(TextAsset));
			if (PopupLoading.instance != null)
			{
				PopupLoading.instance.SetAmmount(startPercent + (stopPercent - startPercent) * 0.42f);
			}
			yield return null;
			TextAsset goiVatPhamTxt = (TextAsset)Resources.Load(local_folder + "/GoiVatPhamVIP", typeof(TextAsset));
			if (PopupLoading.instance != null)
			{
				PopupLoading.instance.SetAmmount(startPercent + (stopPercent - startPercent) * 0.47f);
			}
			yield return null;
			TextAsset huyetChienTxt = (TextAsset)Resources.Load(local_folder + "/HuyetChien", typeof(TextAsset));
			if (PopupLoading.instance != null)
			{
				PopupLoading.instance.SetAmmount(startPercent + (stopPercent - startPercent) * 0.54f);
			}
			yield return null;
			TextAsset danhSonTxt = (TextAsset)Resources.Load(local_folder + "/DanhSon", typeof(TextAsset));
			if (PopupLoading.instance != null)
			{
				PopupLoading.instance.SetAmmount(startPercent + (stopPercent - startPercent) * 0.64f);
			}
			yield return null;
			TextAsset serverTxt = (TextAsset)Resources.Load(local_folder + "/Server", typeof(TextAsset));
			if (PopupLoading.instance != null)
			{
				PopupLoading.instance.SetAmmount(startPercent + (stopPercent - startPercent) * 0.65f);
			}
			yield return null;
			UnityEngine.Object obj = Resources.Load(local_folder + "/Costume", typeof(TextAsset));
			TextAsset costumeTxt = (TextAsset)((obj is TextAsset) ? obj : null);
			if (PopupLoading.instance != null)
			{
				PopupLoading.instance.SetAmmount(startPercent + (stopPercent - startPercent) * 0.7f);
			}
			UnityEngine.Object obj2 = Resources.Load(local_folder + "/SonMon", typeof(TextAsset));
			TextAsset sonmonTxt = (TextAsset)((obj2 is TextAsset) ? obj2 : null);
			UnityEngine.Object obj3 = Resources.Load(local_folder + "/CuaHangThanBi", typeof(TextAsset));
			TextAsset cuahangthanbiTxt = (TextAsset)((obj3 is TextAsset) ? obj3 : null);
			if (PopupLoading.instance != null)
			{
				PopupLoading.instance.SetAmmount(startPercent + (stopPercent - startPercent) * 0.7f);
			}
			UnityEngine.Object obj5 = Resources.Load(local_folder + "/HoaVang", typeof(TextAsset));
			TextAsset hoavangTxt = (TextAsset)((obj5 is TextAsset) ? obj5 : null);
			UnityEngine.Object obj6 = Resources.Load(local_folder + "/ChienHon", typeof(TextAsset));
			TextAsset chienHonTxt = (TextAsset)((obj6 is TextAsset) ? obj6 : null);
			UnityEngine.Object obj7 = Resources.Load(local_folder + "/HuyenKhi", typeof(TextAsset));
			TextAsset huyenKhiTxt = (TextAsset)((obj7 is TextAsset) ? obj7 : null);
			UnityEngine.Object obj8 = Resources.Load(local_folder + "/EffHuyenKhi", typeof(TextAsset));
			TextAsset effHuyenKhiTxt = (TextAsset)((obj8 is TextAsset) ? obj8 : null);
			ConfigManager.instance.ReadAllConfig(vocongTxt.text, trangbiTxt.text, nhanVatTxt.text, vatphamtieuthuTxt.text, null, danhSonTxt.text, otherTxt.text, vcTimeEffTxt.text, attackTimeEffTxt.text, goiVatPhamTxt.text, huyetChienTxt.text, serverTxt.text, costumeTxt.text, "{}", sonmonTxt.text, cuahangthanbiTxt.text, null, hoavangTxt.text, chienHonTxt.text, huyenKhiTxt.text, effHuyenKhiTxt.text);
			TextAsset doithuongLMTxt = (TextAsset)Resources.Load(local_folder + "/DoiThuongLM", typeof(TextAsset));
			ConfigManager.instance.ReadDoiThuongLienMinhCfg(doithuongLMTxt.text);
			TextAsset congTrinhLMTxt = (TextAsset)Resources.Load(local_folder + "/CongTrinhLM", typeof(TextAsset));
			ConfigManager.instance.ReadCongTrinhLienMinhCfg(congTrinhLMTxt.text);
			if (PopupLoading.instance != null)
			{
				PopupLoading.instance.SetAmmount(stopPercent);
			}
			yield return null;
			Resources.UnloadAsset(vocongTxt);
			Resources.UnloadAsset(trangbiTxt);
			Resources.UnloadAsset(nhanVatTxt);
			Resources.UnloadAsset(vatphamtieuthuTxt);
			Resources.UnloadAsset(danhSonTxt);
			Resources.UnloadAsset(otherTxt);
			Resources.UnloadAsset(vcTimeEffTxt);
			Resources.UnloadAsset(attackTimeEffTxt);
			Resources.UnloadAsset(goiVatPhamTxt);
			Resources.UnloadAsset(huyetChienTxt);
			Resources.UnloadAsset(serverTxt);
		}
	}

	public IEnumerator LoadGiangHoCfgLazy()
	{
		if (ConfigManager.instance.IsGiangHoReady)
		{
			yield break;
		}
		TextAsset giangHoTxt = (TextAsset)Resources.Load("Config/GiangHo", typeof(TextAsset));
		yield return null;
		TextAsset giangHo1Txt = (TextAsset)Resources.Load("Config/GiangHo1", typeof(TextAsset));
		yield return null;
		if (!ConfigManager.instance.IsGiangHoReady)
		{
			ConfigManager.instance.ReadGiangHoConfig((giangHoTxt != null) ? giangHoTxt.text : null);
			yield return null;
			if (!ConfigManager.instance.IsGiangHoReady)
			{
				ConfigManager.instance.ReadGiangHoTinhAnhConfig((giangHo1Txt != null) ? giangHo1Txt.text : null);
				ConfigManager.instance.MarkGiangHoReady();
			}
		}
		if (giangHoTxt != null)
		{
			Resources.UnloadAsset(giangHoTxt);
		}
		if (giangHo1Txt != null)
		{
			Resources.UnloadAsset(giangHo1Txt);
		}
	}

	public void EnsureGiangHoReady()
	{
		if (!ConfigManager.instance.IsGiangHoReady)
		{
			TextAsset textAsset = (TextAsset)Resources.Load("Config/GiangHo", typeof(TextAsset));
			TextAsset textAsset2 = (TextAsset)Resources.Load("Config/GiangHo1", typeof(TextAsset));
			ConfigManager.instance.ReadGiangHoConfig((textAsset != null) ? textAsset.text : null);
			ConfigManager.instance.ReadGiangHoTinhAnhConfig((textAsset2 != null) ? textAsset2.text : null);
			ConfigManager.instance.MarkGiangHoReady();
			if (textAsset != null)
			{
				Resources.UnloadAsset(textAsset);
			}
			if (textAsset2 != null)
			{
				Resources.UnloadAsset(textAsset2);
			}
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (m_transport != null)
		{
			m_transport.Pump();
			if (_sessionReady && !_paused && m_transport.State == CJsonTransport.EState.Disconnected && Time.realtimeSinceStartup >= _nextReconnect)
			{
				_nextReconnect = Time.realtimeSinceStartup + _retryDelay;
				_retryDelay = Mathf.Min(15f, _retryDelay * 2f);
				m_transport.Connect();
			}
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

	private void OnApplicationPause(bool paused)
	{
		_paused = paused;
		if (!paused && _sessionReady && m_transport != null)
		{
			_nextReconnect = 0f;
			m_transport.Connect();
		}
	}

	public void ReConnect()
	{
		GameManager.instance.m_GameClient.m_transport.Connect();
	}

	public void IssueConnect()
	{
		_sessionReady = false;
		if (m_transport == null)
		{
			m_transport = new CJsonTransport(OnTransportMessage);
		}
		m_transport.OnConnected = OnTransportConnected;
		m_transport.OnDisconnected = OnTransportDisconnected;
		m_transport.SetAsGlobal();
		m_transport.Connect();
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	private void OnTransportMessage(string rmiName, string data)
	{
		try
		{
			m_S2CStub.Dispatch(rmiName, data);
			m_C2CStub.Dispatch(rmiName, data);
		}
		catch (Exception ex)
		{
			EGDebug.LogError("[GameClient] Dispatch " + rmiName + " error: " + ((ex != null) ? ex.ToString() : null));
		}
	}

	private void OnTransportConnected()
	{
		EnsureGiangHoReady();
		PopupNetworkLoading.DestroyPopup();
		try
		{
			CheckUserRequest obj = new CheckUserRequest
			{
				user = GameManager.instance.m_userName,
				token = GameManager.instance.m_LoginResponse.AccessToken,
				reconnect = _sessionReady,
				supportsChunks = true,
				supportsCompression = false
			};
			m_C2SProxy.RequestNextLogon(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(obj, false));
			PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		}
		catch (Exception)
		{
			MessagePopup.Create(Localization.instance.Get("ConnectNetworkError"));
		}
	}

	private void OnTransportDisconnected()
	{
		if (!_sessionReady)
		{
			MessagePopup.Create(Localization.instance.Get("ConnectNetworkError"));
		}
		PopupNetworkLoading.DestroyPopup();
	}

	public void RequestGetFriendsDanhSon(List<int> listFriends)
	{
		FriendsInfoRequest friendsInfoRequest = new FriendsInfoRequest();
		friendsInfoRequest.ListFriends = listFriends;
		friendsInfoRequest.NumPlayerInDoiHinh = 2;
		SendRequest(C2SProxy.RequestGetFriendsDoiHinh, JsonMapper.ToJson(friendsInfoRequest));
	}

	public bool OnGetFriendsDanhSonResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		FriensInfoResponse friensInfoResponse = JsonMapper.ToObject<FriensInfoResponse>(data);
		if (friensInfoResponse != null)
		{
			if (friensInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (PopupDanhSon.instance != null)
				{
					PopupDanhSon.instance.SetFriends(friensInfoResponse);
				}
			}
			else if (friensInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(friensInfoResponse.ErrorMessage);
				MessagePopup.Create(friensInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("[OnGetFriendsResponse] - Error: Code = {0}, - Message : {1}", friensInfoResponse.ErrorCode, friensInfoResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnBatThoSuccess : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestBatTho()
	{
		SendRequest(C2SProxy.RequestBatTho, string.Empty);
	}

	public bool OnBatThoSuccess(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongDesc"), phanThuongResponse);
				if (GUIManager.instance.isAutoBatCoc)
				{
					StartCoroutine(closePopUp(2f));
				}
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(phanThuongResponse.ErrorMessage);
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("[OnBatThoSuccess] - Error: Code = {0}, - Message : {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnBatThoSuccess : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	private IEnumerator closePopUp(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.instance.gameObject.SetActive(false);
		}
	}

	public void RequestBatCoc(int enemyId)
	{
		BatCocRequest batCocRequest = new BatCocRequest();
		batCocRequest.OfflineUser = enemyId;
		SendRequest(C2SProxy.RequestBatCoc, JsonMapper.ToJson(batCocRequest, false));
	}

	public static void OnEndBattleBatCoc()
	{
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.instance.gameObject.SetActive(true);
		}
		if (PopupBattleResult.instance != null)
		{
			PopupBattleResult.instance.gameObject.SetActive(true);
		}
		if (GUIManager.instance.isAutoBatCoc)
		{
			ScreenMain screenMain = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain;
			screenMain.endBattleAutoBatCoc();
		}
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		screenBattle.OnFinishReplay -= OnEndBattleBatCoc;
	}

	public static void OnEndBattleAnTrom()
	{
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.instance.gameObject.SetActive(true);
		}
		if (PopupBattleResult.instance != null)
		{
			PopupBattleResult.instance.gameObject.SetActive(true);
		}
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		ScreenLienMinhTrongCay screenLienMinhTrongCay = (ScreenLienMinhTrongCay)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenLienMinhTrongCay);
		screenLienMinhTrongCay.OnSyncData();
		screenBattle.OnFinishReplay -= OnEndBattleAnTrom;
	}

	public bool OnBatCocSuccess(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		BatCocResponse batCocResponse = JsonMapper.ToObject<BatCocResponse>(data);
		if (batCocResponse != null)
		{
			if (batCocResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (batCocResponse.Battle != null)
				{
					if (batCocResponse.UpdateUserInfo != null && batCocResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
					{
						UserInfo.UpdateInfo(batCocResponse.UpdateUserInfo);
					}
					ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
					GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
					screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenMain;
					List<UserInfo.HeroData> listCloneHeroFromDoiHinh = UserInfo.GetListCloneHeroFromDoiHinh();
					PopupBattleResult popupBattleResult = PopupBattleResult.Create(batCocResponse.Battle, listCloneHeroFromDoiHinh, batCocResponse.ExpMP, batCocResponse.Bac, batCocResponse.ExpNV, batCocResponse.GID);
					popupBattleResult.gameObject.SetActive(false);
					if (batCocResponse.PhanThuong != null)
					{
						if (batCocResponse.PhanThuong.UpdateUserInfo != null && batCocResponse.PhanThuong.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
						{
							UserInfo.UpdateInfo(batCocResponse.PhanThuong.UpdateUserInfo);
						}
						PopupDanhSachPhanThuong popupDanhSachPhanThuong = PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongBatCocTitle"), Localization.instance.Get("PhanThuongBatCocDesc"), batCocResponse.PhanThuong);
						popupDanhSachPhanThuong.gameObject.SetActive(false);
					}
					if (batCocResponse.Battle.Winner == 1 && GUIManager.instance != null && GUIManager.instance.homeCity != null)
					{
						GUIManager.instance.homeCity.RemoveOfflinetUser(batCocResponse.GID);
					}
					screenBattle.Replay(batCocResponse.Battle);
					if (GUIManager.instance.isAutoBatCoc)
					{
						GUIManager.instance.homeCity.RemoveOfflinetUser(batCocResponse.GID);
						StartCoroutine(endBattle(2f));
					}
					screenBattle.OnFinishReplay += OnEndBattleBatCoc;
				}
			}
			else if (batCocResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(batCocResponse.ErrorMessage);
				MessagePopup.Create(batCocResponse.ErrorMessage);
				if (GUIManager.instance.isAutoBatCoc)
				{
					GUIManager.instance.homeCity.RemoveOfflinetUser(batCocResponse.GID);
					ScreenMain screenMain = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain;
					screenMain.waittingRequestBatCoc = false;
					screenMain.currOffUserID = -1;
					screenMain.getListOtherPlayer();
				}
			}
			else
			{
				if (GUIManager.instance.isAutoBatCoc)
				{
					ScreenMain screenMain2 = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain;
					screenMain2.endAutoBatCoc();
				}
				string text = string.Format("[OnBatCocSuccess] - Error: Code = {0}, - Message : {1}", batCocResponse.ErrorCode, batCocResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnBatCocSuccess : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	private IEnumerator endBattle(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBattle)
		{
			screenBattle.BattleEnd();
		}
	}

	public void RequestChuocThan()
	{
		SendRequest(C2SProxy.RequestChuocThan, string.Empty);
	}

	public bool OnChuocThanSuccess(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UserInfo userInfo = JsonMapper.ToObject<UserInfo>(data);
		if (userInfo != null)
		{
			if (userInfo.ErrorCode == ERROR_CODE.OK)
			{
				UserInfo.UpdateInfo(userInfo);
				PopupChuocThan.DestroyPopup();
			}
			else if (userInfo.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(userInfo.ErrorMessage);
				MessagePopup.Create(userInfo.ErrorMessage);
			}
			else
			{
				string text = string.Format("[OnChuocThanSuccess] - Error: Code = {0}, - Message : {1}", userInfo.ErrorCode, userInfo.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnChuocThanSuccess : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestTangTheLuc(int gid)
	{
		TangTheLucRequest tangTheLucRequest = new TangTheLucRequest();
		tangTheLucRequest.EnemyID = gid;
		SendRequest(C2SProxy.RequestTangTheLuc, JsonMapper.ToJson(tangTheLucRequest, false));
	}

	public bool OnDuocTangTheLuc(HostID remote, RmiContext rmiContext, string data)
	{
		TangTheLucResponse tangTheLucResponse = JsonMapper.ToObject<TangTheLucResponse>(data);
		if (tangTheLucResponse != null)
		{
			if (tangTheLucResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (tangTheLucResponse.UpdateUserInfo != null && tangTheLucResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(tangTheLucResponse.UpdateUserInfo);
				}
				MessagePopup.Create(string.Format(Localization.instance.Get("DuocTangTheLucSuccessMsg"), tangTheLucResponse.EnemyName));
			}
			else if (tangTheLucResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(tangTheLucResponse.ErrorMessage);
				MessagePopup.Create(tangTheLucResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("[OnDuocTangTheLuc] - Error: Code = {0}, - Message : {1}", tangTheLucResponse.ErrorCode, tangTheLucResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnDuocTangTheLuc : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public bool OnTangTheLuc(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		TangTheLucResponse tangTheLucResponse = JsonMapper.ToObject<TangTheLucResponse>(data);
		if (tangTheLucResponse != null)
		{
			if (tangTheLucResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (tangTheLucResponse.UpdateUserInfo != null && tangTheLucResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(tangTheLucResponse.UpdateUserInfo);
				}
				MessagePopup.Create(Localization.instance.Get("TangTheLucSuccessMsg"));
			}
			else if (tangTheLucResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(tangTheLucResponse.ErrorMessage);
				MessagePopup.Create(tangTheLucResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("[OnDuocTangTheLuc] - Error: Code = {0}, - Message : {1}", tangTheLucResponse.ErrorCode, tangTheLucResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnDuocTangTheLuc : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestThachDau(int gid, int tienCuoc)
	{
		ThachDauRequest thachDauRequest = new ThachDauRequest();
		thachDauRequest.EnemyID = gid;
		thachDauRequest.TienCuoc = tienCuoc;
		SendRequest(C2SProxy.RequestThachDau, JsonMapper.ToJson(thachDauRequest, false));
	}

	public bool OnNhanDuocThachDau(HostID remote, RmiContext rmiContext, string data)
	{
		DuocThachDauResponse duocThachDauResponse = JsonMapper.ToObject<DuocThachDauResponse>(data);
		if (duocThachDauResponse != null)
		{
			if (duocThachDauResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBattle || GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenCT2)
				{
					RequestNhanThachDau(duocThachDauResponse.EnemyID, 0);
				}
				else
				{
					PopupNhanDuocThachDau.Create(duocThachDauResponse.EnemyID, duocThachDauResponse.EnemyName, duocThachDauResponse.TienCuoc);
				}
			}
			else if (duocThachDauResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(duocThachDauResponse.ErrorMessage);
				MessagePopup.Create(duocThachDauResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("[OnNhanDuocThachDau] - Error: Code = {0}, - Message : {1}", duocThachDauResponse.ErrorCode, duocThachDauResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnNhanDuocThachDau : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestNhanThachDau(int enemyId, int thachDauCode)
	{
		NhanThachDauRequest nhanThachDauRequest = new NhanThachDauRequest();
		nhanThachDauRequest.EnemyID = enemyId;
		nhanThachDauRequest.NhanThachDauCode = thachDauCode;
		SendRequest(m_C2SProxy.RequestNhanThachDau, JsonMapper.ToJson(nhanThachDauRequest, false));
	}

	private void OnEndBattleThachDau()
	{
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		screenBattle.OnFinishReplay -= OnEndBattleThachDau;
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.instance.gameObject.SetActive(true);
		}
		if (_ketQuaThachDauResponse == null || _ketQuaThachDauResponse.ErrorCode != ERROR_CODE.OK || _ketQuaThachDauResponse.Battle == null)
		{
			return;
		}
		if (_ketQuaThachDauResponse.IsWon)
		{
			if (_ketQuaThachDauResponse.TienCuoc > 0)
			{
				MessagePopup.Create(string.Format(Localization.instance.Get("ThachDauCuocThanhCongMsg"), _ketQuaThachDauResponse.EnemyName, _ketQuaThachDauResponse.TienCuoc));
			}
			else
			{
				MessagePopup.Create(string.Format(Localization.instance.Get("ThachDauThanhCongMsg"), _ketQuaThachDauResponse.EnemyName));
			}
		}
		else if (_ketQuaThachDauResponse.TienCuoc > 0)
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("ThachDauCuocThatBaiMsg"), _ketQuaThachDauResponse.EnemyName, _ketQuaThachDauResponse.TienCuoc));
		}
		else
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("ThachDauThatBaiMsg"), _ketQuaThachDauResponse.EnemyName));
		}
	}

	public bool OnKetQuaThachDau(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		KetQuaThachDauResponse ketQuaThachDauResponse = JsonMapper.ToObject<KetQuaThachDauResponse>(data);
		if (ketQuaThachDauResponse != null)
		{
			if (ketQuaThachDauResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (ketQuaThachDauResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(ketQuaThachDauResponse.UpdateUserInfo);
				}
				PopupThachDau.DestroyPopup();
				PopupNhanDuocThachDau.DestroyPopup();
				_ketQuaThachDauResponse = ketQuaThachDauResponse;
				if (ketQuaThachDauResponse.Battle != null)
				{
					if (!GUIManager.instance.isAutoThachDau)
					{
						List<UserInfo.HeroData> listCloneHeroFromDoiHinh = UserInfo.GetListCloneHeroFromDoiHinh();
						PopupBattleResult popupBattleResult = PopupBattleResult.Create(ketQuaThachDauResponse.Battle, listCloneHeroFromDoiHinh, ketQuaThachDauResponse.ExpMonPhai, 0L, ketQuaThachDauResponse.ExpDeTu, ketQuaThachDauResponse.EnemyID);
						ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
						GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
						screenBattle.Replay(ketQuaThachDauResponse.Battle);
						screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenMain;
						screenBattle.OnFinishReplay += OnEndBattleThachDau;
					}
					else if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBanBe)
					{
						ScreenBanBe screenBanBe = GUIManager.getScreen(GAME_SCREEN.ScreenBanBe) as ScreenBanBe;
						screenBanBe.AutoThachDauTab.continueThachDau(ketQuaThachDauResponse.PhanThuong, ketQuaThachDauResponse.IsWon);
						return true;
					}
				}
				else
				{
					MessagePopup.Create(Localization.instance.Get("ThachDauDeny"));
				}
				if (ketQuaThachDauResponse.PhanThuong != null && ketQuaThachDauResponse.PhanThuong.PhanThuongList.Count > 0)
				{
					PopupDanhSachPhanThuong popupDanhSachPhanThuong = PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongDesc"), ketQuaThachDauResponse.PhanThuong);
					popupDanhSachPhanThuong.gameObject.SetActive(false);
				}
			}
			else if (ketQuaThachDauResponse.ErrorCode == ERROR_CODE.WAIT_FOR_ANOTHER_USER)
			{
				PopupNetworkLoading.Create(ketQuaThachDauResponse.ErrorMessage);
			}
			else if (ketQuaThachDauResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(ketQuaThachDauResponse.ErrorMessage);
				MessagePopup.Create(ketQuaThachDauResponse.ErrorMessage);
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBanBe)
				{
					GUIManager.instance.isAutoThachDau = false;
					return true;
				}
			}
			else
			{
				string text = string.Format("[OnKetQuaThachDau] - Error: Code = {0}, - Message : {1}", ketQuaThachDauResponse.ErrorCode, ketQuaThachDauResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnKetQuaThachDau : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void GetTopHuyetChien()
	{
		SendRequest(C2SProxy.RequestGetTopHuyetChien, string.Empty);
	}

	public bool OnGetTopHuyetChien(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		HuyetChienTopResponse huyetChienTopResponse = JsonMapper.ToObject<HuyetChienTopResponse>(data);
		if (huyetChienTopResponse != null)
		{
			if (huyetChienTopResponse.ErrorCode == ERROR_CODE.OK)
			{
				PopupTopHuyetChien.Create(huyetChienTopResponse);
			}
			else if (huyetChienTopResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(huyetChienTopResponse.ErrorMessage);
				MessagePopup.Create(huyetChienTopResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("Error: Code = {0}, - Message : {1}", huyetChienTopResponse.ErrorCode, huyetChienTopResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnGetTopHuyetChien : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void DanhHuyetChien(int cls)
	{
		DanhHuyetChienRequest danhHuyetChienRequest = new DanhHuyetChienRequest();
		danhHuyetChienRequest.OpponentClass = cls;
		if (GUIManager.instance.isAutoHacMocNhai)
		{
			SendRequest(C2SProxy.RequestDanhHuyetChien, JsonMapper.ToJson(danhHuyetChienRequest, false), false);
		}
		else
		{
			SendRequest(C2SProxy.RequestDanhHuyetChien, JsonMapper.ToJson(danhHuyetChienRequest, false));
		}
	}

	public bool OnDanhHuyetChien(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DanhHuyetChienResponse danhHuyetChienResponse = JsonMapper.ToObject<DanhHuyetChienResponse>(data);
		if (danhHuyetChienResponse != null)
		{
			if (danhHuyetChienResponse.ErrorCode == ERROR_CODE.OK)
			{
				ScreenHuyetChien screenHuyetChien = GUIManager.getScreen(GAME_SCREEN.ScreenHuyetChien) as ScreenHuyetChien;
				if (GUIManager.instance.isAutoHacMocNhai)
				{
					if (danhHuyetChienResponse.HuyetChienInfo != null && danhHuyetChienResponse.HuyetChienInfo.ErrorCode == ERROR_CODE.OK)
					{
						if (danhHuyetChienResponse.HuyetChienInfo.UpdateUserInfo != null)
						{
							UserInfo.UpdateInfo(danhHuyetChienResponse.HuyetChienInfo.UpdateUserInfo);
						}
						if (screenHuyetChien != null)
						{
							screenHuyetChien.SyncWithNetworkData(danhHuyetChienResponse.HuyetChienInfo);
							if (!danhHuyetChienResponse.HuyetChienInfo.HuyetChienInfo.IsAlive && danhHuyetChienResponse.HuyetChienInfo.HuyetChienInfo.Level >= 0)
							{
								screenHuyetChien.endAutoHMN();
							}
							else if (danhHuyetChienResponse.Battle != null && danhHuyetChienResponse.Battle.Winner == 1)
							{
								if (danhHuyetChienResponse.PhanThuong != null && danhHuyetChienResponse.PhanThuong.PhanThuongList.Count > 0)
								{
									for (int i = 0; i < danhHuyetChienResponse.PhanThuong.PhanThuongList.Count; i++)
									{
										screenHuyetChien.listAllPhanThuong.Add(danhHuyetChienResponse.PhanThuong.PhanThuongList[i]);
									}
								}
								screenHuyetChien.startAutoGoHMN();
							}
							else
							{
								screenHuyetChien.endAutoHMN();
							}
						}
					}
				}
				else
				{
					bool flag = false;
					if (danhHuyetChienResponse.HuyetChienInfo.HuyetChienInfo.LastLevel > 50 && danhHuyetChienResponse.Battle != null)
					{
						if (screenHuyetChien.DaTungThua)
						{
							flag = false;
						}
						else if (danhHuyetChienResponse.Battle.Winner == 1)
						{
							flag = true;
						}
						else
						{
							flag = false;
							screenHuyetChien.DaTungThua = true;
						}
					}
					if (danhHuyetChienResponse.Battle != null)
					{
						List<UserInfo.HeroData> listCloneHeroFromDoiHinh = UserInfo.GetListCloneHeroFromDoiHinh();
						PopupBattleResult popupBattleResult = PopupBattleResult.Create(danhHuyetChienResponse.Battle, listCloneHeroFromDoiHinh, 0L, 0L, 0L, 0);
						if (!flag)
						{
							popupBattleResult.gameObject.SetActive(false);
						}
						popupBattleResult.OnClosePopup = screenHuyetChien.OnCloseBattleResult;
					}
					if (UserInfo.DanhHieu != null && danhHuyetChienResponse.HuyetChienInfo != null && danhHuyetChienResponse.HuyetChienInfo.UpdateUserInfo != null && danhHuyetChienResponse.HuyetChienInfo.UpdateUserInfo.DanhHieu != null && UserInfo.DanhHieu.DucHuyetPhanChien < danhHuyetChienResponse.HuyetChienInfo.UpdateUserInfo.DanhHieu.DucHuyetPhanChien)
					{
						PopupDuocThanhTuu popupDuocThanhTuu = PopupDuocThanhTuu.CreateCuuTinhLienHoan(danhHuyetChienResponse.HuyetChienInfo.UpdateUserInfo.DanhHieu);
						if (!flag)
						{
							popupDuocThanhTuu.gameObject.SetActive(false);
						}
					}
					if (danhHuyetChienResponse.HuyetChienInfo != null && danhHuyetChienResponse.HuyetChienInfo.ErrorCode == ERROR_CODE.OK)
					{
						if (danhHuyetChienResponse.HuyetChienInfo.UpdateUserInfo != null)
						{
							UserInfo.UpdateInfo(danhHuyetChienResponse.HuyetChienInfo.UpdateUserInfo);
						}
						if (screenHuyetChien != null)
						{
							screenHuyetChien.SyncWithNetworkData(danhHuyetChienResponse.HuyetChienInfo);
						}
					}
					if (danhHuyetChienResponse.Battle != null)
					{
						ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
						screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenHuyetChien;
						if (screenBattle != null)
						{
							if (!flag)
							{
								GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
								screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenHuyetChien;
								screenBattle.Replay(danhHuyetChienResponse.Battle, "BM_HAC_MOC_NHAI");
							}
							else
							{
								screenBattle.DataReplay = danhHuyetChienResponse.Battle;
								screenBattle.BattleEnd();
							}
						}
						if (danhHuyetChienResponse.PhanThuong != null && danhHuyetChienResponse.PhanThuong.PhanThuongList.Count > 0)
						{
							PopupDanhSachPhanThuong popupDanhSachPhanThuong = PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongDesc"), danhHuyetChienResponse.PhanThuong);
							if (!flag)
							{
								popupDanhSachPhanThuong.gameObject.SetActive(false);
							}
						}
					}
				}
				if (danhHuyetChienResponse.PhanThuong != null && danhHuyetChienResponse.PhanThuong.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(danhHuyetChienResponse.PhanThuong.UpdateUserInfo);
				}
			}
			else if (danhHuyetChienResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(danhHuyetChienResponse.ErrorMessage);
				MessagePopup.Create(danhHuyetChienResponse.ErrorMessage);
			}
			else if (danhHuyetChienResponse.ErrorCode == ERROR_CODE.MAT_DONG_BO)
			{
				MessagePopup.Create(danhHuyetChienResponse.ErrorMessage);
				ScreenHuyetChien screenHuyetChien2 = GUIManager.getScreen(GAME_SCREEN.ScreenHuyetChien) as ScreenHuyetChien;
				if (screenHuyetChien2 != null)
				{
					screenHuyetChien2.SyncWithNetworkData(danhHuyetChienResponse.HuyetChienInfo);
					if (GUIManager.instance.isAutoHacMocNhai)
					{
						if (danhHuyetChienResponse.Battle != null && danhHuyetChienResponse.Battle.Winner == 1)
						{
							if (danhHuyetChienResponse.PhanThuong != null && danhHuyetChienResponse.PhanThuong.PhanThuongList.Count > 0)
							{
								for (int j = 0; j < danhHuyetChienResponse.PhanThuong.PhanThuongList.Count; j++)
								{
									screenHuyetChien2.listAllPhanThuong.Add(danhHuyetChienResponse.PhanThuong.PhanThuongList[j]);
								}
							}
							screenHuyetChien2.startAutoGoHMN();
						}
						else
						{
							screenHuyetChien2.endAutoHMN();
						}
					}
				}
			}
			else
			{
				string text = string.Format("Error: Code = {0}, - Message : {1}", danhHuyetChienResponse.ErrorCode, danhHuyetChienResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnDanhHuyetChien : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void TangChiSoHuyetChien(ChiSoCoBan chiso)
	{
		HuyetChienTangThuocTinhRequest huyetChienTangThuocTinhRequest = new HuyetChienTangThuocTinhRequest();
		huyetChienTangThuocTinhRequest.Chiso = chiso;
		if (GUIManager.instance.isAutoHacMocNhai)
		{
			SendRequest(C2SProxy.RequestHuyetChienTangThuocTinh, JsonMapper.ToJson(huyetChienTangThuocTinhRequest, false), false);
		}
		else
		{
			SendRequest(C2SProxy.RequestHuyetChienTangThuocTinh, JsonMapper.ToJson(huyetChienTangThuocTinhRequest, false));
		}
	}

	public bool OnTangChiSoHuyetChien(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		HuyetChienResponse huyetChienResponse = JsonMapper.ToObject<HuyetChienResponse>(data);
		if (huyetChienResponse != null)
		{
			if (huyetChienResponse.ErrorCode == ERROR_CODE.OK)
			{
				ScreenHuyetChien screenHuyetChien = GUIManager.getScreen(GAME_SCREEN.ScreenHuyetChien) as ScreenHuyetChien;
				if (screenHuyetChien != null)
				{
					screenHuyetChien.SyncWithNetworkData(huyetChienResponse);
					if (GUIManager.instance.isAutoHacMocNhai)
					{
						screenHuyetChien.startAutoGoHMN();
					}
				}
			}
			else if (huyetChienResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(huyetChienResponse.ErrorMessage);
				MessagePopup.Create(huyetChienResponse.ErrorMessage);
			}
			else if (huyetChienResponse.ErrorCode == ERROR_CODE.MAT_DONG_BO)
			{
				MessagePopup.Create(huyetChienResponse.ErrorMessage);
				ScreenHuyetChien screenHuyetChien2 = GUIManager.getScreen(GAME_SCREEN.ScreenHuyetChien) as ScreenHuyetChien;
				if (screenHuyetChien2 != null)
				{
					screenHuyetChien2.SyncWithNetworkData(huyetChienResponse);
					if (GUIManager.instance.isAutoHacMocNhai)
					{
						screenHuyetChien2.startAutoGoHMN();
					}
				}
			}
			else
			{
				string text = string.Format("Error: Code = {0}, - Message : {1}", huyetChienResponse.ErrorCode, huyetChienResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnTangChiSoHuyetChien : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void NhanThuongHuyetChien()
	{
		if (GUIManager.instance.isAutoHacMocNhai)
		{
			SendRequest(C2SProxy.RequestNhanThuongHuyetChien, string.Empty, false);
		}
		else
		{
			SendRequest(C2SProxy.RequestNhanThuongHuyetChien, string.Empty);
		}
	}

	public bool OnNhanThuongHuyetChien(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		HuyetChienResponse huyetChienResponse = JsonMapper.ToObject<HuyetChienResponse>(data);
		if (huyetChienResponse != null)
		{
			if (huyetChienResponse.ErrorCode == ERROR_CODE.OK)
			{
				ScreenHuyetChien screenHuyetChien = GUIManager.getScreen(GAME_SCREEN.ScreenHuyetChien) as ScreenHuyetChien;
				if (screenHuyetChien != null)
				{
					screenHuyetChien.SyncWithNetworkData(huyetChienResponse);
					if (GUIManager.instance.isAutoHacMocNhai)
					{
						if (huyetChienResponse.PhanThuong != null && huyetChienResponse.PhanThuong.PhanThuongList.Count > 0)
						{
							for (int i = 0; i < huyetChienResponse.PhanThuong.PhanThuongList.Count; i++)
							{
								screenHuyetChien.listAllPhanThuong.Add(huyetChienResponse.PhanThuong.PhanThuongList[i]);
							}
						}
						screenHuyetChien.startAutoGoHMN();
					}
				}
				if (huyetChienResponse.PhanThuong != null && huyetChienResponse.PhanThuong.ErrorCode == ERROR_CODE.OK && huyetChienResponse.PhanThuong.UpdateUserInfo != null && huyetChienResponse.PhanThuong.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(huyetChienResponse.PhanThuong.UpdateUserInfo);
				}
			}
			else if (huyetChienResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(huyetChienResponse.ErrorMessage);
				MessagePopup.Create(huyetChienResponse.ErrorMessage);
			}
			else if (huyetChienResponse.ErrorCode == ERROR_CODE.MAT_DONG_BO)
			{
				MessagePopup.Create(huyetChienResponse.ErrorMessage);
				ScreenHuyetChien screenHuyetChien2 = GUIManager.getScreen(GAME_SCREEN.ScreenHuyetChien) as ScreenHuyetChien;
				if (screenHuyetChien2 != null)
				{
					screenHuyetChien2.SyncWithNetworkData(huyetChienResponse);
					if (GUIManager.instance.isAutoHacMocNhai)
					{
						if (huyetChienResponse.PhanThuong != null && huyetChienResponse.PhanThuong.PhanThuongList.Count > 0)
						{
							for (int j = 0; j < huyetChienResponse.PhanThuong.PhanThuongList.Count; j++)
							{
								screenHuyetChien2.listAllPhanThuong.Add(huyetChienResponse.PhanThuong.PhanThuongList[j]);
							}
						}
						screenHuyetChien2.startAutoGoHMN();
					}
				}
			}
			else
			{
				string text = string.Format("Error: Code = {0}, - Message : {1}", huyetChienResponse.ErrorCode, huyetChienResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnNhanThuongHuyetChien : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void StartHuyetChien()
	{
		SendRequest(C2SProxy.RequestStartHuyetChien, string.Empty);
	}

	public bool OnStartHuyetChien(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		HuyetChienResponse huyetChienResponse = JsonMapper.ToObject<HuyetChienResponse>(data);
		if (huyetChienResponse != null)
		{
			if (huyetChienResponse.ErrorCode == ERROR_CODE.OK)
			{
				ScreenHuyetChien screenHuyetChien = GUIManager.getScreen(GAME_SCREEN.ScreenHuyetChien) as ScreenHuyetChien;
				if (screenHuyetChien != null)
				{
					screenHuyetChien.SyncWithNetworkData(huyetChienResponse);
				}
			}
			else if (huyetChienResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(huyetChienResponse.ErrorMessage);
				MessagePopup.Create(huyetChienResponse.ErrorMessage);
			}
			else if (huyetChienResponse.ErrorCode == ERROR_CODE.MAT_DONG_BO)
			{
				EGDebug.Log("MAT DONG BO DU LIEU: StartHuyetChien");
				MessagePopup.Create(huyetChienResponse.ErrorMessage);
				ScreenHuyetChien screenHuyetChien2 = GUIManager.getScreen(GAME_SCREEN.ScreenHuyetChien) as ScreenHuyetChien;
				if (screenHuyetChien2 != null)
				{
					screenHuyetChien2.SyncWithNetworkData(huyetChienResponse);
				}
			}
			else
			{
				string text = string.Format("Error: Code = {0}, - Message : {1}", huyetChienResponse.ErrorCode, huyetChienResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnStartHuyetChien : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void HoiSinhHuyetChien()
	{
		SendRequest(C2SProxy.RequestHoiSinhHuyetChien, string.Empty);
	}

	public bool OnHoiSinhHuyetChien(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		HuyetChienResponse huyetChienResponse = JsonMapper.ToObject<HuyetChienResponse>(data);
		if (huyetChienResponse != null)
		{
			if (huyetChienResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (huyetChienResponse.UpdateUserInfo != null && huyetChienResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(huyetChienResponse.UpdateUserInfo);
				}
				ScreenHuyetChien screenHuyetChien = GUIManager.getScreen(GAME_SCREEN.ScreenHuyetChien) as ScreenHuyetChien;
				if (screenHuyetChien != null)
				{
					screenHuyetChien.SyncWithNetworkData(huyetChienResponse);
				}
			}
			else if (huyetChienResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(huyetChienResponse.ErrorMessage);
				MessagePopup.Create(huyetChienResponse.ErrorMessage);
			}
			else if (huyetChienResponse.ErrorCode == ERROR_CODE.MAT_DONG_BO)
			{
				EGDebug.Log("MAT DONG BO DU LIEU: HoiSinhHuyetChien");
				MessagePopup.Create(huyetChienResponse.ErrorMessage);
				ScreenHuyetChien screenHuyetChien2 = GUIManager.getScreen(GAME_SCREEN.ScreenHuyetChien) as ScreenHuyetChien;
				if (screenHuyetChien2 != null)
				{
					screenHuyetChien2.SyncWithNetworkData(huyetChienResponse);
				}
			}
			else
			{
				string text = string.Format("Error: Code = {0}, - Message : {1}", huyetChienResponse.ErrorCode, huyetChienResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnHoiSinhHuyetChien : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void GetHuyetChienInfo()
	{
		if (GUIManager.instance.isAutoHacMocNhai)
		{
			SendRequest(C2SProxy.RequestHuyetChienInfo, string.Empty, false);
		}
		else
		{
			SendRequest(C2SProxy.RequestHuyetChienInfo, string.Empty);
		}
	}

	public bool OnReceiveHuyetChienInfo(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		HuyetChienResponse huyetChienResponse = JsonMapper.ToObject<HuyetChienResponse>(data);
		if (huyetChienResponse != null)
		{
			if (huyetChienResponse.ErrorCode == ERROR_CODE.OK)
			{
				ScreenHuyetChien screenHuyetChien = GUIManager.getScreen(GAME_SCREEN.ScreenHuyetChien) as ScreenHuyetChien;
				if (screenHuyetChien != null)
				{
					screenHuyetChien.SyncWithNetworkData(huyetChienResponse);
				}
			}
			else if (huyetChienResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(huyetChienResponse.ErrorMessage);
				MessagePopup.Create(huyetChienResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("Error: Code = {0}, - Message : {1}", huyetChienResponse.ErrorCode, huyetChienResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnReceiveHuyetChienInfo : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestThuongNhan(int index, bool muaNgay)
	{
		ThuongNhanRequest thuongNhanRequest = new ThuongNhanRequest();
		thuongNhanRequest.ThuongNhanIndex = index;
		thuongNhanRequest.MuaNgay = muaNgay;
		SendRequest(C2SProxy.RequestThuongNhan, JsonMapper.ToJson(thuongNhanRequest, false));
	}

	public bool OnReceiveThuongNhan(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null && phanThuongResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				ScreenKyNgoGiangHo screenKyNgoGiangHo = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoGiangHo) as ScreenKyNgoGiangHo;
				if (screenKyNgoGiangHo != null)
				{
					screenKyNgoGiangHo.SyncWithNetworkData(UserInfo);
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("ThuongNhanPhanThuongTitle"), Localization.instance.Get("ThuongNhanPhanThuongDesc"), phanThuongResponse);
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(phanThuongResponse.ErrorMessage);
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("Error: Code = {0}, - Message : {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnReceiveThuongNhan : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestTyThi(int index, bool khieuChien)
	{
		TyThiRequest tyThiRequest = new TyThiRequest();
		tyThiRequest.TyThiIndex = index;
		tyThiRequest.KhieuChien = khieuChien;
		SendRequest(C2SProxy.RequestTyThi, JsonMapper.ToJson(tyThiRequest, false));
	}

	public bool OnReceiveTyThi(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		TyThiResponse tyThiResponse = JsonMapper.ToObject<TyThiResponse>(data);
		if (tyThiResponse != null)
		{
			if (tyThiResponse.ErrorCode == ERROR_CODE.OK)
			{
				ScreenKyNgoGiangHo screenKyNgoGiangHo = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoGiangHo) as ScreenKyNgoGiangHo;
				if (tyThiResponse.Battle != null)
				{
					List<UserInfo.HeroData> listCloneHeroFromDoiHinh = UserInfo.GetListCloneHeroFromDoiHinh();
					PopupBattleResult popupBattleResult = PopupBattleResult.Create(tyThiResponse.Battle, listCloneHeroFromDoiHinh, 0L, 0L, 0L, -1);
					popupBattleResult.gameObject.SetActive(false);
					popupBattleResult.OnClosePopup = screenKyNgoGiangHo.OnCloseBattleResult;
				}
				if (tyThiResponse.PhanThuong != null && tyThiResponse.PhanThuong.UpdateUserInfo != null && tyThiResponse.PhanThuong.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(tyThiResponse.PhanThuong.UpdateUserInfo);
				}
				if (screenKyNgoGiangHo != null)
				{
					screenKyNgoGiangHo.SyncWithNetworkData(UserInfo);
				}
				if (tyThiResponse.Battle != null)
				{
					GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
					ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
					if (tyThiResponse.PhanThuong != null && tyThiResponse.PhanThuong.PhanThuongList.Count > 0)
					{
						if (tyThiResponse.Battle.Winner == 1)
						{
							PopupDanhSachPhanThuong popupDanhSachPhanThuong = PopupDanhSachPhanThuong.Create(Localization.instance.Get("TyThiSuccessPhanThuongTitle"), Localization.instance.Get("TyThiSuccessPhanThuongDesc"), tyThiResponse.PhanThuong);
							popupDanhSachPhanThuong.gameObject.SetActive(false);
						}
						else
						{
							PopupDanhSachPhanThuong popupDanhSachPhanThuong2 = PopupDanhSachPhanThuong.Create(Localization.instance.Get("TyThiFailPhanThuongTitle"), Localization.instance.Get("TyThiFailPhanThuongDesc"), tyThiResponse.PhanThuong);
							popupDanhSachPhanThuong2.gameObject.SetActive(false);
						}
					}
					screenBattle.Replay(tyThiResponse.Battle);
					screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenKyNgoGiangHo;
				}
				else if (tyThiResponse.PhanThuong != null && tyThiResponse.PhanThuong.PhanThuongList.Count > 0)
				{
					PopupDanhSachPhanThuong.Create(Localization.instance.Get("TyThiFailPhanThuongTitle"), Localization.instance.Get("TyThiFailPhanThuongDesc"), tyThiResponse.PhanThuong);
				}
			}
			else if (tyThiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(tyThiResponse.ErrorMessage);
				MessagePopup.Create(tyThiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("Error: Code = {0}, - Message : {1}", tyThiResponse.ErrorCode, tyThiResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnReceiveTyThi : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestBangHuu(int index, bool isThuPhuc)
	{
		BangHuuRequest bangHuuRequest = new BangHuuRequest();
		bangHuuRequest.BangHuuIndex = index;
		bangHuuRequest.IsThuPhuc = isThuPhuc;
		SendRequest(C2SProxy.RequestBangHuu, JsonMapper.ToJson(bangHuuRequest, false));
	}

	public bool OnReceiveBangHuu(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		BangHuuResponse bangHuuResponse = JsonMapper.ToObject<BangHuuResponse>(data);
		if (bangHuuResponse != null)
		{
			if (bangHuuResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (bangHuuResponse.UpdateUserInfo != null && bangHuuResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(bangHuuResponse.UpdateUserInfo);
				}
				ScreenKyNgoGiangHo screenKyNgoGiangHo = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoGiangHo) as ScreenKyNgoGiangHo;
				if (screenKyNgoGiangHo != null)
				{
					screenKyNgoGiangHo.SyncWithNetworkData(UserInfo);
				}
				NhanVatCfg value;
				if (ConfigManager.instance.m_dicNhanVats.TryGetValue(bangHuuResponse.TenNV, out value))
				{
					LayDeTuResponse layDeTuResponse = new LayDeTuResponse();
					layDeTuResponse.NhanVatName = bangHuuResponse.TenNV;
					layDeTuResponse.TanHonCount = bangHuuResponse.Count;
					string empty = string.Empty;
					empty = ((bangHuuResponse.Count <= 0) ? string.Format(Localization.instance.Get("BangHuuResponseMsgDeTu"), value.TenHienThi) : string.Format(Localization.instance.Get("BangHuuResponseMsgTanHon"), bangHuuResponse.Count, value.TenHienThi));
					ScreenThuNhanDeTuResult screenThuNhanDeTuResult = GUIManager.getScreen(GAME_SCREEN.ScreenThuNhanDeTuResult) as ScreenThuNhanDeTuResult;
					if (screenThuNhanDeTuResult != null)
					{
						GUIManager.instance.SetScreen(GAME_SCREEN.ScreenThuNhanDeTuResult);
						screenThuNhanDeTuResult.setData(layDeTuResponse);
					}
					else
					{
						MessagePopup.Create(empty);
					}
				}
			}
			else if (bangHuuResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(bangHuuResponse.ErrorMessage);
				MessagePopup.Create(bangHuuResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("Error: Code = {0}, - Message : {1}", bangHuuResponse.ErrorCode, bangHuuResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnReceiveBangHuu : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestBanDo(int index, bool isNhanDoi)
	{
		BanDoRequest banDoRequest = new BanDoRequest();
		banDoRequest.BanDoIndex = index;
		banDoRequest.IsNhanDoi = isNhanDoi;
		SendRequest(C2SProxy.RequestBanDo, JsonMapper.ToJson(banDoRequest, false));
	}

	public bool OnReceiveBanDo(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				ScreenKyNgoGiangHo screenKyNgoGiangHo = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoGiangHo) as ScreenKyNgoGiangHo;
				if (screenKyNgoGiangHo != null)
				{
					screenKyNgoGiangHo.SyncWithNetworkData(UserInfo);
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("KhoBauPhanThuongTitle"), Localization.instance.Get("KhoBauPhanThuongDesc"), phanThuongResponse);
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(phanThuongResponse.ErrorMessage);
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("Error: Code = {0}, - Message : {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnReceiveBanDo : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestCaoNhan(int index, bool hauTa)
	{
		CaoNhanRequest caoNhanRequest = new CaoNhanRequest();
		caoNhanRequest.CaoNhanIndex = index;
		caoNhanRequest.IsHauTa = hauTa;
		SendRequest(C2SProxy.RequestCaoNhan, JsonMapper.ToJson(caoNhanRequest, false));
	}

	public bool OnReceiveCaoNhan(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		CaoNhanResponse caoNhanResponse = JsonMapper.ToObject<CaoNhanResponse>(data);
		if (caoNhanResponse != null)
		{
			if (caoNhanResponse.ErrorCode == ERROR_CODE.OK)
			{
				PopupDeTuGetExp.Create(UserInfo.GetListCloneHeroFromDoiHinh(), caoNhanResponse.Exp, Localization.instance.Get("PopupDeTuGetExpCaoNhanMsg"));
				if (caoNhanResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(caoNhanResponse.UpdateUserInfo);
				}
				ScreenKyNgoGiangHo screenKyNgoGiangHo = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoGiangHo) as ScreenKyNgoGiangHo;
				if (screenKyNgoGiangHo != null)
				{
					screenKyNgoGiangHo.SyncWithNetworkData(UserInfo);
				}
			}
			else if (caoNhanResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(caoNhanResponse.ErrorMessage);
				MessagePopup.Create(caoNhanResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("Error: Code = {0}, - Message : {1}", caoNhanResponse.ErrorCode, caoNhanResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnReceiveCaoNhan : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestGetGamerInfo()
	{
		List<string> list = new List<string>();
		list.Add("gamer");
		list.Add("giatrithoigian");
		list.Add("hero");
		list.Add("doihinh");
		list.Add("vocong");
		list.Add("vcthietlap");
		list.Add("luankiem");
		list.Add("trangbi");
		list.Add("vatphamtieuthu");
		list.Add("giangho");
		list.Add("danhson");
		list.Add("danhhieu");
		list.Add("honnhanvat");
		list.Add("mail");
		list.Add("manhtrangbi");
		list.Add("manhvocong");
		list.Add("serverinfo");
		list.Add("banbe");
		list.Add("cuuthu");
		List<string> list2 = list;
		list2.Add("lienminh");
		list2.Add("nguyenkhi");
		list2.Add("thucuoi");
		list2.Add("costume");
		list2.Add("thanthu");
		list2.Add("lanhdia");
		list2.Add("thienma");
		list2.Add("gianghotinhanh");
		list2.Add("chienhon");
		list2.Add("huyenkhi");
		if (isIpV6)
		{
			OnReceiveGamerInfo("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");
		}
		else
		{
			SendRequest(C2SProxy.RequestGetInfo, JsonMapper.ToJson(list2, false));
		}
	}

	public void OnReceiveGamerInfo(string data)
	{
		OnGetResponse();
		try
		{
			EGDebug.LogWarning("[OnReceiveGamerInfo] START data len = " + ((data != null) ? data.Length : 0));
			UserInfo userInfo = JsonMapper.ToObject<UserInfo>(data);
			EGDebug.LogWarning("[OnReceiveGamerInfo] Deserialized userInfo: " + ((userInfo != null) ? "OK" : "NULL"));
			if (userInfo != null)
			{
				if (userInfo.ErrorCode == ERROR_CODE.OK)
				{
					try
					{
						UserInfo.UpdateInfo(userInfo);
					}
					catch (Exception ex)
					{
						EGDebug.LogError("[OnReceiveGamerInfo] UpdateInfo exception: " + ((ex != null) ? ex.ToString() : null));
					}
					int num = ((UserInfo.HeroList != null) ? UserInfo.HeroList.Count : 0);
					string text = ((UserInfo.Gamer != null) ? UserInfo.Gamer.GhiChu : string.Empty);
					EGDebug.LogWarning("[OnReceiveGamerInfo] HeroList count = " + num + ", GhiChu = " + text);
					if (num == 0)
					{
						EGDebug.LogWarning("[OnReceiveGamerInfo] Go to ScreenSelectFirstDeTu");
						GUIManager.instance.SetScreen(GAME_SCREEN.ScreenSelectFirstDeTu);
						return;
					}
					if (!text.Contains("ChonNguaLanDau;"))
					{
						EGDebug.LogWarning("[OnReceiveGamerInfo] Go to ScreenSelectTheFirstHorse");
						GUIManager.instance.SetScreen(GAME_SCREEN.ScreenSelectTheFirstHorse);
						return;
					}
					EGDebug.LogWarning("[OnReceiveGamerInfo] Go to ScreenMain");
					GUIManager.setScreen(GAME_SCREEN.ScreenMain);
					if (UserInfo.Gamer != null && string.IsNullOrEmpty(UserInfo.Gamer.DisplayName))
					{
						PopupDatTenMonPhai.CreateDatTenLanDau();
					}
				}
				else if (userInfo.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
				{
					MessagePopup.Create(userInfo.ErrorMessage);
					EGDebug.Log(userInfo.ErrorMessage);
				}
				else
				{
					MessagePopup.Create(string.Format("Error: Code = {0}, - Message : {1}", userInfo.ErrorCode, userInfo.ErrorMessage));
					EGDebug.Log(string.Format("Error: Code = {0}, - Message : {1}", userInfo.ErrorCode, userInfo.ErrorMessage));
				}
			}
			else
			{
				EGDebug.Log("OnReceiveGamerInfo : response is null");
				MessagePopup.Create("OnReceiveGamerInfo : response is null");
			}
		}
		catch (Exception ex2)
		{
			EGDebug.LogError("[OnReceiveGamerInfo] Fatal exception: " + ((ex2 != null) ? ex2.ToString() : null));
		}
	}

	public void RequestGetListOtherPlayer(int level, bool isBatCoc = false)
	{
		GetListOthersRequest getListOthersRequest = new GetListOthersRequest();
		getListOthersRequest.Level = level;
		getListOthersRequest.isBatCoc = isBatCoc;
		if (isIpV6)
		{
			OnReceiveListOnlineInMain(HostID.None, new RmiContext(), "VAIAACQCAADwD3siTXlJbmZvIjp7IklEIjo1MzIsIlBvc0luSG9tZRYA8B5YIjotMTMuMTEyMTc0MDM0MTE4NywiWSI6MC4xNjU5MjExNjY1MzkxOTIsIlosAPAbMi44NTMwMDA2NDA4NjkxLCJpc05pZW5UaHUiOmZhbHNlfSwiVXNlck5hYQDxBiJIXHUxRUFGYyBMb25nIFx1MDExMBMA8AoxaSBCYW5nIiwiTGV2ZWwiOjkwLCJDb2RlOQD2C1Z1S2hpIjoiVktfQkFJX1ZBTl9IT19USFUiJADxB0F2YXRhciI6Ik5WX0tJRU1fVEhBTkghAEFzdHVtSACQIjoiIiwiVGhhoAAAWQD2AiI6IlBFVF9USElFTl9MQU5HHwDwBFF1YWxpdHkiOjMsIk5vaUNvbmeCABBDWAAQSXwAM09ORzkAQE1BX0MOALRTUyIsIkJvUGhhcCsAEUWtAPALVFVOR19CIiwiSXNPbmxpbmUiOnRydWUsIlOGAeAwLCJLaGlUaGUiOjUyMKAAQHVDdW/wAPAFIiwiVmlwIjoxNCwiTGllbk1pbmgyAPMMOTMsImdoaUNodVRyb25nTmdheSI6IlRPUDJf3QDxFzsiLCJEYW5oSGlldVR5cGUiOjEsIkdpYW5nSG9Db3VudCI6MTJ9/QH4CU90aGVyR2FtZXJzIjpbXSwiT2ZmbGluZRMAgk5vTGVMaXN0IQDQRXJyb3JDb2RlIjowfQ==");
		}
		else
		{
			SendRequest(C2SProxy.RequestGetListOtherPlayer, JsonMapper.ToJson(getListOthersRequest, false), false);
		}
	}

	public bool OnReceiveListOnlineInMain(HostID remote, RmiContext rmiContext, string data)
	{
		HomeResponse homeResponse = JsonMapper.ToObject<HomeResponse>(data);
		if (homeResponse != null)
		{
			if (homeResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.homeCity != null && GUIManager.instance.homeCity.gameObject.activeInHierarchy)
				{
					GUIManager.instance.homeCity.SyncWithOnlineData(homeResponse);
				}
			}
			else if (homeResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(homeResponse.ErrorMessage);
			}
			else
			{
				EGDebug.Log(string.Format("Error: Code = {0}, - Message : {1}", homeResponse.ErrorCode, homeResponse.ErrorMessage));
			}
		}
		else
		{
			EGDebug.Log("OnReceiveListOnlineInMain : response is null");
		}
		return true;
	}

	public bool OnReceiveListOnMain(HostID remote, RmiContext rmiContext, string data)
	{
		HomeResponse homeResponse = JsonMapper.ToObject<HomeResponse>(data);
		if (homeResponse != null)
		{
			if (homeResponse.ErrorCode == ERROR_CODE.OK)
			{
				HomeResponse = homeResponse;
				if (GUIManager.instance.homeCity != null)
				{
					if (GUIManager.instance.homeCity.gameObject.activeInHierarchy)
					{
						GUIManager.instance.homeCity.SyncWithNetworkData();
					}
					else
					{
						GUIManager.instance.homeCity.IsDirty = true;
					}
					if (GUIManager.instance.isAutoBatCoc)
					{
						GUIManager.instance.homeCity.SpawnRandomOfflineUser();
					}
				}
			}
			else if (homeResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(homeResponse.ErrorMessage);
			}
			else
			{
				EGDebug.Log(string.Format("Error: Code = {0}, - Message : {1}", homeResponse.ErrorCode, homeResponse.ErrorMessage));
			}
		}
		else
		{
			EGDebug.Log("Lỗi nhận danh sách người chơi trong Thành Chính");
		}
		return true;
	}

	public void GetLuanKiemInfo()
	{
		SendRequest(m_C2SProxy.RequestGetLuanKiemInfo, string.Empty);
	}

	public bool OnReceiveLuanKiemInfo(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		LuanKiemResponse luanKiemResponse = JsonMapper.ToObject<LuanKiemResponse>(data);
		if (luanKiemResponse != null)
		{
			if (luanKiemResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (luanKiemResponse.UserInfoResponse != null && luanKiemResponse.UserInfoResponse.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(luanKiemResponse.UserInfoResponse);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenLuanKiem)
				{
					ScreenLuanKiem screenLuanKiem = GUIManager.getScreen(GAME_SCREEN.ScreenLuanKiem) as ScreenLuanKiem;
					screenLuanKiem.SyncWithNetworkData(luanKiemResponse);
				}
			}
			else if (luanKiemResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(luanKiemResponse.ErrorMessage);
				EGDebug.Log(luanKiemResponse.ErrorMessage);
			}
			else
			{
				MessagePopup.Create(string.Format("Error: Code = {0}, - Message : {1}", luanKiemResponse.ErrorCode, luanKiemResponse.ErrorMessage));
				EGDebug.Log(string.Format("Error: Code = {0}, - Message : {1}", luanKiemResponse.ErrorCode, luanKiemResponse.ErrorMessage));
			}
		}
		else
		{
			EGDebug.Log("Lỗi nhận danh sách người chơi trong Thành Chính");
		}
		return true;
	}

	public bool OnCapNhatDiemThuongLK(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UserInfo userInfo = JsonMapper.ToObject<UserInfo>(data);
		if (userInfo != null)
		{
			if (userInfo.ErrorCode == ERROR_CODE.OK)
			{
				UserInfo.UpdateInfo(userInfo);
			}
			else if (userInfo.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(userInfo.ErrorMessage);
				EGDebug.LogWarning(userInfo.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", userInfo.ErrorCode, userInfo.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("Lỗi cập nhật điểm thưởng luận kiếm");
		}
		return true;
	}

	public void DauLuanKiem(int enemyId, int botId)
	{
		LuanKiemBattleRequest luanKiemBattleRequest = new LuanKiemBattleRequest();
		luanKiemBattleRequest.BotID = botId;
		luanKiemBattleRequest.GID = enemyId;
		SendRequest(m_C2SProxy.RequestDauLuanKiem, JsonMapper.ToJson(luanKiemBattleRequest, false));
	}

	public bool OnDauLuanKiemResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DauLuanKiemResponse dauLuanKiemResponse = JsonMapper.ToObject<DauLuanKiemResponse>(data);
		if (dauLuanKiemResponse != null)
		{
			if (dauLuanKiemResponse.ErrorCode == ERROR_CODE.OK)
			{
				List<UserInfo.HeroData> listCloneHeroFromDoiHinh = UserInfo.GetListCloneHeroFromDoiHinh();
				PopupBattleResult popupBattleResult = PopupBattleResult.Create(dauLuanKiemResponse.BattleReplayData, listCloneHeroFromDoiHinh, dauLuanKiemResponse.ExpMP, 0L, dauLuanKiemResponse.ExpDeTu, dauLuanKiemResponse.EnemyID);
				if (UserInfo.DanhHieu != null && dauLuanKiemResponse.LuanKiemInfo != null && dauLuanKiemResponse.LuanKiemInfo.UserInfoResponse != null && dauLuanKiemResponse.LuanKiemInfo.UserInfoResponse.DanhHieu != null && UserInfo.DanhHieu.ThanhDanhHienHach < dauLuanKiemResponse.LuanKiemInfo.UserInfoResponse.DanhHieu.ThanhDanhHienHach)
				{
					PopupDuocThanhTuu popupDuocThanhTuu = PopupDuocThanhTuu.CreateThanhDanhHienHach(dauLuanKiemResponse.LuanKiemInfo.UserInfoResponse.DanhHieu);
					popupDuocThanhTuu.gameObject.SetActive(false);
				}
				if (dauLuanKiemResponse.LuanKiemInfo != null && dauLuanKiemResponse.LuanKiemInfo.UserInfoResponse != null && dauLuanKiemResponse.LuanKiemInfo.UserInfoResponse.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(dauLuanKiemResponse.LuanKiemInfo.UserInfoResponse);
				}
				ScreenLuanKiem screenLuanKiem = GUIManager.getScreen(GAME_SCREEN.ScreenLuanKiem) as ScreenLuanKiem;
				screenLuanKiem.SyncWithNetworkData(dauLuanKiemResponse.LuanKiemInfo);
				GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
				popupBattleResult.gameObject.SetActive(false);
				popupBattleResult.OnClosePopup = screenLuanKiem.OnCloseBattleResult;
				if (dauLuanKiemResponse.PhanThuong != null && dauLuanKiemResponse.PhanThuong.PhanThuongList.Count > 0)
				{
					PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongDesc"), dauLuanKiemResponse.PhanThuong).gameObject.SetActive(false);
				}
				ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
				screenBattle.Replay(dauLuanKiemResponse.BattleReplayData, "BM_HOA_SON_LUAN_KIEM");
				screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenLuanKiem;
			}
			else if (dauLuanKiemResponse.ErrorCode == ERROR_CODE.MAT_DONG_BO)
			{
				ScreenLuanKiem screenLuanKiem2 = GUIManager.getScreen(GAME_SCREEN.ScreenLuanKiem) as ScreenLuanKiem;
				screenLuanKiem2.SyncWithNetworkData(dauLuanKiemResponse.LuanKiemInfo);
				MessagePopup.Create(dauLuanKiemResponse.ErrorMessage);
				EGDebug.LogWarning(dauLuanKiemResponse.ErrorMessage);
			}
			else if (dauLuanKiemResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(dauLuanKiemResponse.ErrorMessage);
				EGDebug.LogWarning(dauLuanKiemResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", dauLuanKiemResponse.ErrorCode, dauLuanKiemResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnDauLuanKiemResponse: response is null");
		}
		return true;
	}

	public void NhanThuongLuanKiem(UserInfo.LuanKiemData.RewardMask code)
	{
		if (UserInfo.Gamer.Level < ConfigManager.LevelUnlockLuanKiem())
		{
			MessagePopup.Create(Localization.instance.Get("LuanKiemInvalidLevel"), ConfigManager.LevelUnlockLuanKiem());
			return;
		}
		if (UserInfo.LuanKiem == null)
		{
			MessagePopup.Create(Localization.instance.Get("LuanKiemInvalidNull"));
			return;
		}
		LuanKiemNhanThuongRequest luanKiemNhanThuongRequest = new LuanKiemNhanThuongRequest();
		luanKiemNhanThuongRequest.Reward = code;
		luanKiemNhanThuongRequest.ID = UserInfo.Gamer.ID;
		SendRequest(m_C2SProxy.RequestNhanThuongLuanKiem, JsonMapper.ToJson(luanKiemNhanThuongRequest, false));
	}

	public bool OnNhanThuongLuanKiemResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		LuanKiemDoiThuongResponse luanKiemDoiThuongResponse = JsonMapper.ToObject<LuanKiemDoiThuongResponse>(data);
		if (luanKiemDoiThuongResponse != null)
		{
			if (luanKiemDoiThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (luanKiemDoiThuongResponse.PhanThuong != null && luanKiemDoiThuongResponse.PhanThuong.UpdateUserInfo != null && luanKiemDoiThuongResponse.PhanThuong.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(luanKiemDoiThuongResponse.PhanThuong.UpdateUserInfo);
				}
				ScreenLuanKiem screenLuanKiem = GUIManager.getScreen(GAME_SCREEN.ScreenLuanKiem) as ScreenLuanKiem;
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("LuanKiemPhanThuongTitle"), Localization.instance.Get("LuanKiemPhanThuongDesc"), luanKiemDoiThuongResponse.PhanThuong);
				screenLuanKiem.SyncDoiThuongInfo(luanKiemDoiThuongResponse);
			}
			else if (luanKiemDoiThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(luanKiemDoiThuongResponse.ErrorMessage);
				EGDebug.LogWarning(luanKiemDoiThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", luanKiemDoiThuongResponse.ErrorCode, luanKiemDoiThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("Lỗi đấu luận kiếm");
		}
		return true;
	}

	public void DoiThuongLuanKiem(DoiThuongEnum doiThuong)
	{
		if (UserInfo.Gamer.Level < ConfigManager.LevelUnlockLuanKiem())
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("LuanKiemInvalidLevel"), ConfigManager.LevelUnlockLuanKiem()));
			return;
		}
		if (UserInfo.LuanKiem == null)
		{
			MessagePopup.Create(Localization.instance.Get("LuanKiemInvalidNull"));
			return;
		}
		if (UserInfo.LuanKiem.DiemTichLuy < ConfigManager.GetDiemThuongCanDoiLK(doiThuong))
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("LuanKiemInvalidThieuDiem"), ConfigManager.GetDiemThuongCanDoiLK(doiThuong)));
			return;
		}
		LuanKiemDoiThuongRequest luanKiemDoiThuongRequest = new LuanKiemDoiThuongRequest();
		luanKiemDoiThuongRequest.DoiThuong = doiThuong;
		luanKiemDoiThuongRequest.ID = UserInfo.Gamer.ID;
		SendRequest(m_C2SProxy.RequestDoiThuongLuanKiem, JsonMapper.ToJson(luanKiemDoiThuongRequest, false));
	}

	public bool OnDoiThuongLuanKiemResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		LuanKiemDoiThuongResponse luanKiemDoiThuongResponse = JsonMapper.ToObject<LuanKiemDoiThuongResponse>(data);
		if (luanKiemDoiThuongResponse != null)
		{
			if (luanKiemDoiThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (luanKiemDoiThuongResponse.PhanThuong != null && luanKiemDoiThuongResponse.PhanThuong.UpdateUserInfo != null && luanKiemDoiThuongResponse.PhanThuong.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(luanKiemDoiThuongResponse.PhanThuong.UpdateUserInfo);
				}
				ScreenLuanKiem screenLuanKiem = GUIManager.getScreen(GAME_SCREEN.ScreenLuanKiem) as ScreenLuanKiem;
				screenLuanKiem.SyncDoiThuongInfo(luanKiemDoiThuongResponse);
				if (luanKiemDoiThuongResponse.PhanThuong != null)
				{
					PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongDesc"), luanKiemDoiThuongResponse.PhanThuong);
				}
			}
			else if (luanKiemDoiThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(luanKiemDoiThuongResponse.ErrorMessage);
				EGDebug.LogWarning(luanKiemDoiThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", luanKiemDoiThuongResponse.ErrorCode, luanKiemDoiThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("Lỗi đấu luận kiếm");
		}
		return true;
	}

	public void RequestVuotAiDanhSon(int dsIdx, int friendId)
	{
		DanhDanhSonRequest danhDanhSonRequest = new DanhDanhSonRequest();
		danhDanhSonRequest.DanhSonIdx = dsIdx;
		danhDanhSonRequest.FriendId = friendId;
		SendRequest(m_C2SProxy.RequestVuotAiDanhSon, JsonMapper.ToJson(danhDanhSonRequest, false));
	}

	public bool OnVuotAiDanhSon(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DanhDanhSonResponse danhDanhSonResponse = JsonMapper.ToObject<DanhDanhSonResponse>(data);
		if (danhDanhSonResponse != null)
		{
			if (danhDanhSonResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (danhDanhSonResponse.UpdateUserInfo != null && danhDanhSonResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(danhDanhSonResponse.UpdateUserInfo);
				}
				if (danhDanhSonResponse.PhanThuong != null && danhDanhSonResponse.PhanThuong.PhanThuongList.Count > 0)
				{
					PopupDanhSachPhanThuong popupDanhSachPhanThuong = PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongNhanDuoc"), danhDanhSonResponse.PhanThuong);
					popupDanhSachPhanThuong.gameObject.SetActive(false);
					UserInfo.UpdateInfo(danhDanhSonResponse.PhanThuong.UpdateUserInfo);
				}
				UserInfo userInfo = UserInfo;
				UserInfo.DanhSonData danhSonData = userInfo.GetDanhSonByIdx(danhDanhSonResponse.DanhSonIdx);
				if (danhSonData == null)
				{
					danhSonData = new UserInfo.DanhSonData();
					danhSonData.DanhSonIdx = danhDanhSonResponse.DanhSonIdx;
				}
				if (PopupDanhSon.instance == null)
				{
					PopupDanhSon.Create(danhSonData);
				}
				else
				{
					PopupDanhSon.instance.SetInfo(danhSonData);
				}
				if (PopupDanhSon.instance != null)
				{
					PopupDanhSon.instance.gameObject.SetActive(false);
				}
				ScreenDanhSon screenDanhSon = GUIManager.getScreen(GAME_SCREEN.ScreenDanhSon) as ScreenDanhSon;
				screenDanhSon.StartBattle(danhDanhSonResponse);
			}
			else if (danhDanhSonResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(danhDanhSonResponse.ErrorMessage);
				EGDebug.LogWarning(danhDanhSonResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", danhDanhSonResponse.ErrorCode, danhDanhSonResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnVuotAiDanhSon : response is null");
		}
		return true;
	}

	public void MoThuongDanhSon(int dsIdx)
	{
		DanhDanhSonRequest danhDanhSonRequest = new DanhDanhSonRequest();
		danhDanhSonRequest.DanhSonIdx = dsIdx;
		if (GUIManager.instance.isAutoCamDia)
		{
			SendRequest(m_C2SProxy.RequestMoThuongDanhSon, JsonMapper.ToJson(danhDanhSonRequest));
		}
		else
		{
			SendRequest(m_C2SProxy.RequestMoThuongDanhSon, JsonMapper.ToJson(danhDanhSonRequest, false));
		}
	}

	public bool OnMoThuongDanhSon(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		MoThuongDanhSonResponse moThuongDanhSonResponse = JsonMapper.ToObject<MoThuongDanhSonResponse>(data);
		if (moThuongDanhSonResponse != null)
		{
			if (moThuongDanhSonResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.isAutoCamDia)
				{
					if (moThuongDanhSonResponse.UpdateUserInfo != null && moThuongDanhSonResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
					{
						UserInfo.UpdateInfo(moThuongDanhSonResponse.UpdateUserInfo);
					}
					ScreenDanhSon screenDanhSon = GUIManager.getScreen(GAME_SCREEN.ScreenDanhSon) as ScreenDanhSon;
					if (moThuongDanhSonResponse.PhanThuongList != null && moThuongDanhSonResponse.PhanThuongList.Count > 0)
					{
						screenDanhSon.addPhanThuongToList(moThuongDanhSonResponse.PhanThuongList);
						screenDanhSon.continueAutoCamDia();
					}
				}
				else
				{
					UserInfo.DanhSonData danhSonByIdx = UserInfo.GetDanhSonByIdx(moThuongDanhSonResponse.DanhSonIdx);
					if (!(PopupDanhSon.instance != null))
					{
						PopupDanhSon.Create(danhSonByIdx);
					}
					if (moThuongDanhSonResponse.PhanThuongList != null && moThuongDanhSonResponse.PhanThuongList.Count > 0)
					{
						StartCoroutine(PopupDanhSon.instance.VisualQuayThuong(moThuongDanhSonResponse, danhSonByIdx, moThuongDanhSonResponse.OpenedSlot));
					}
					else if (moThuongDanhSonResponse.UpdateUserInfo != null && moThuongDanhSonResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
					{
						UserInfo.UpdateInfo(moThuongDanhSonResponse.UpdateUserInfo);
					}
				}
			}
			else if (moThuongDanhSonResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				if (GUIManager.instance.isAutoCamDia)
				{
					ScreenDanhSon screenDanhSon2 = GUIManager.getScreen(GAME_SCREEN.ScreenDanhSon) as ScreenDanhSon;
					screenDanhSon2.endAutoCamDia();
				}
				MessagePopup.Create(moThuongDanhSonResponse.ErrorMessage);
				EGDebug.LogWarning(moThuongDanhSonResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", moThuongDanhSonResponse.ErrorCode, moThuongDanhSonResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnMoThuongDanhSon : response is null");
		}
		return true;
	}

	public void RequestMoHetDanhSon(int dsIdx)
	{
		DanhDanhSonRequest danhDanhSonRequest = new DanhDanhSonRequest();
		danhDanhSonRequest.DanhSonIdx = dsIdx;
		SendRequest(m_C2SProxy.RequestMoHetDanhSon, JsonMapper.ToJson(danhDanhSonRequest, false));
	}

	public bool OnMoHetDanhSon(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		MoThuongDanhSonResponse moThuongDanhSonResponse = JsonMapper.ToObject<MoThuongDanhSonResponse>(data);
		if (moThuongDanhSonResponse != null)
		{
			if (moThuongDanhSonResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (moThuongDanhSonResponse.UpdateUserInfo != null && moThuongDanhSonResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(moThuongDanhSonResponse.UpdateUserInfo);
				}
				if (PopupDanhSon.instance != null)
				{
					PopupDanhSon.instance.SetInfo(UserInfo.GetDanhSonByIdx(moThuongDanhSonResponse.DanhSonIdx));
				}
				else
				{
					PopupDanhSon.Create(UserInfo.GetDanhSonByIdx(moThuongDanhSonResponse.DanhSonIdx));
				}
				if (moThuongDanhSonResponse.PhanThuongList != null && moThuongDanhSonResponse.PhanThuongList.Count > 0)
				{
					PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
					phanThuongResponse.PhanThuongList = moThuongDanhSonResponse.PhanThuongList;
					PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongDesc"), phanThuongResponse);
				}
			}
			else if (moThuongDanhSonResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(moThuongDanhSonResponse.ErrorMessage);
				EGDebug.LogWarning(moThuongDanhSonResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", moThuongDanhSonResponse.ErrorCode, moThuongDanhSonResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnMoHetDanhSon : response is null");
		}
		return true;
	}

	public void RequestDanhDanhSon(int dsIdx, int friendId)
	{
		DanhDanhSonRequest danhDanhSonRequest = new DanhDanhSonRequest();
		danhDanhSonRequest.DanhSonIdx = dsIdx;
		danhDanhSonRequest.FriendId = friendId;
		EGDebug.Log("danh son index: " + dsIdx);
		if (GUIManager.instance.isAutoCamDia)
		{
			SendRequest(m_C2SProxy.RequestDanhDanhSon, JsonMapper.ToJson(danhDanhSonRequest));
		}
		else
		{
			SendRequest(m_C2SProxy.RequestDanhDanhSon, JsonMapper.ToJson(danhDanhSonRequest, false));
		}
	}

	public bool OnDanhDanhSonResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DanhDanhSonResponse danhDanhSonResponse = JsonMapper.ToObject<DanhDanhSonResponse>(data);
		if (danhDanhSonResponse != null)
		{
			if (danhDanhSonResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (danhDanhSonResponse.UpdateUserInfo != null && danhDanhSonResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(danhDanhSonResponse.UpdateUserInfo);
				}
				ScreenDanhSon screenDanhSon = GUIManager.getScreen(GAME_SCREEN.ScreenDanhSon) as ScreenDanhSon;
				UserInfo userInfo = UserInfo;
				UserInfo.DanhSonData danhSonData = userInfo.GetDanhSonByIdx(danhDanhSonResponse.DanhSonIdx);
				if (GUIManager.instance.isAutoCamDia)
				{
					screenDanhSon.SyncWithNetworkData();
					if (danhSonData == null)
					{
						danhSonData = new UserInfo.DanhSonData();
						danhSonData.DanhSonIdx = danhDanhSonResponse.DanhSonIdx;
					}
					int luotMoThuong = DanhSonCfg.GetLuotMoThuong(danhSonData.LuotMoThuong);
					int num = 3;
					int costMoThuongDanhSon = ConfigManager.GetCostMoThuongDanhSon(luotMoThuong - num + 1);
					if (GameManager.instance.m_GameClient.checkKNB(costMoThuongDanhSon))
					{
						GameManager.instance.m_GameClient.MoThuongDanhSon(danhSonData.DanhSonIdx);
					}
				}
				else
				{
					int num2 = 0;
					for (int num3 = UserInfo.DanhSon.Count; num3 > 0; num3--)
					{
						if (UserInfo.DanhSon[num3 - 1].DanhSonIdx > num2)
						{
							num2 = UserInfo.DanhSon[num3 - 1].DanhSonIdx;
						}
					}
					bool flag = true;
					if (num2 > 6 && danhDanhSonResponse.DanhSonIdx < num2 / 2)
					{
						flag = false;
					}
					if (danhDanhSonResponse.Battle1.Winner == 2 || danhDanhSonResponse.Battle2 == null || (danhDanhSonResponse.Battle2 != null && danhDanhSonResponse.Battle2.Winner != 1) || danhDanhSonResponse.Battle3 == null || (danhDanhSonResponse.Battle3 != null && danhDanhSonResponse.Battle3.Winner != 1))
					{
						flag = true;
					}
					if (flag)
					{
						screenDanhSon.StartBattle(danhDanhSonResponse);
					}
					else
					{
						screenDanhSon.SyncWithNetworkData();
					}
					if (danhSonData == null)
					{
						danhSonData = new UserInfo.DanhSonData();
						danhSonData.DanhSonIdx = danhDanhSonResponse.DanhSonIdx;
					}
					if (PopupDanhSon.instance == null)
					{
						PopupDanhSon.Create(danhSonData);
					}
					else
					{
						PopupDanhSon.instance.SetInfo(danhSonData);
					}
					if (danhDanhSonResponse.PhanThuong != null && danhDanhSonResponse.PhanThuong.PhanThuongList.Count > 0)
					{
						PopupDanhSachPhanThuong popupDanhSachPhanThuong = PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongNhanDuoc"), danhDanhSonResponse.PhanThuong);
						if (flag)
						{
							popupDanhSachPhanThuong.gameObject.SetActive(false);
						}
					}
					if (flag)
					{
						PopupDanhSon.instance.gameObject.SetActive(false);
					}
				}
			}
			else if (danhDanhSonResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				if (GUIManager.instance.isAutoCamDia)
				{
					ScreenDanhSon screenDanhSon2 = GUIManager.getScreen(GAME_SCREEN.ScreenDanhSon) as ScreenDanhSon;
					screenDanhSon2.endAutoCamDia();
				}
				if (PopupDanhSon.instance == null)
				{
					PopupDanhSon.DestroyPopup();
				}
				MessagePopup.Create(danhDanhSonResponse.ErrorMessage);
				EGDebug.LogWarning(danhDanhSonResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", danhDanhSonResponse.ErrorCode, danhDanhSonResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnDanhDanhSonResponse : response is null");
		}
		return true;
	}

	public void RequestNhanThuongGH(int ghIdx, bool isGHTinhAnh = false)
	{
		GiangHoRequest giangHoRequest = new GiangHoRequest();
		giangHoRequest.GiangHoIdx = ghIdx;
		giangHoRequest.GiangHoTinhAnh = isGHTinhAnh;
		SendRequest(C2SProxy.RequestNhanThuongGiangHo, JsonMapper.ToJson(giangHoRequest, false));
	}

	public bool OnNhanThuongGH(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null && phanThuongResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				ScreenWorldmap screenWorldmap = GUIManager.getScreen(GAME_SCREEN.ScreenWorldmap) as ScreenWorldmap;
				if (screenWorldmap != null)
				{
					screenWorldmap.SyncWithNetworkData();
				}
				if (GiangHoPopup.instance != null)
				{
					GiangHoCfg cfg;
					UserInfo.GiangHoData giangHo;
					if (GiangHoPopup.instance != null && GiangHoPopup.instance.isGHTinhAnh)
					{
						cfg = ConfigManager.instance.m_listGiangHoTinhAnh[GiangHoPopup.instance.giangHoIdx];
						giangHo = UserInfo.GetGiangHoTinhAnhByIdx(GiangHoPopup.instance.giangHoIdx);
					}
					else
					{
						cfg = ConfigManager.instance.m_listGiangHo[GiangHoPopup.instance.giangHoIdx];
						giangHo = UserInfo.GetGiangHoByIdx(GiangHoPopup.instance.giangHoIdx);
					}
					GiangHoPopup.instance.SyncWithNetworkData(giangHo, cfg);
				}
				if (phanThuongResponse.PhanThuongList.Count > 0)
				{
					PopupDanhSachPhanThuong popupDanhSachPhanThuong = PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongGiangHoTitle"), Localization.instance.Get("PhanThuongGiangHoDesc"), phanThuongResponse);
				}
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnNhanThuongGH : response is null");
		}
		return true;
	}

	public void RequestAnGaGH(int ghIdx, bool isGHTinhAnh)
	{
		GiangHoRequest giangHoRequest = new GiangHoRequest();
		giangHoRequest.GiangHoTinhAnh = isGHTinhAnh;
		giangHoRequest.GiangHoIdx = ghIdx;
		SendRequest(C2SProxy.RequestAnGaGiangHo, JsonMapper.ToJson(giangHoRequest, false));
	}

	public bool OnAnGaGH(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UserInfo userInfo = JsonMapper.ToObject<UserInfo>(data);
		if (userInfo != null)
		{
			if (userInfo.ErrorCode == ERROR_CODE.OK)
			{
				MessagePopup.Create(string.Format(Localization.instance.Get("GiangHoAnGaMsg"), userInfo.GiaTriThoiGian.TheLuc - UserInfo.GiaTriThoiGian.TheLuc));
				UserInfo.UpdateInfo(userInfo);
				ScreenWorldmap screenWorldmap = GUIManager.getScreen(GAME_SCREEN.ScreenWorldmap) as ScreenWorldmap;
				screenWorldmap.SyncWithNetworkData();
				if (GiangHoPopup.instance != null)
				{
					GiangHoCfg cfg;
					UserInfo.GiangHoData giangHo;
					if (GiangHoPopup.instance.isGHTinhAnh)
					{
						cfg = ConfigManager.instance.m_listGiangHoTinhAnh[GiangHoPopup.instance.giangHoIdx];
						giangHo = UserInfo.GetGiangHoTinhAnhByIdx(GiangHoPopup.instance.giangHoIdx);
					}
					else
					{
						cfg = ConfigManager.instance.m_listGiangHo[GiangHoPopup.instance.giangHoIdx];
						giangHo = UserInfo.GetGiangHoByIdx(GiangHoPopup.instance.giangHoIdx);
					}
					GiangHoPopup.instance.SyncWithNetworkData(giangHo, cfg);
				}
			}
			else if (userInfo.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(userInfo.ErrorMessage);
				EGDebug.LogWarning(userInfo.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", userInfo.ErrorCode, userInfo.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnAnGaGH : response is null");
		}
		return true;
	}

	public void RequestDanhNhanhGH(int ghIdx, int nvIdx, bool isGHTA)
	{
		DanhGiangHoRequest danhGiangHoRequest = new DanhGiangHoRequest();
		danhGiangHoRequest.GiangHoIdx = ghIdx;
		danhGiangHoRequest.NhiemVuIdx = nvIdx;
		danhGiangHoRequest.GiangHoTinhAnh = isGHTA;
		SendRequest(m_C2SProxy.RequestDanhNhanhGiangHo, JsonMapper.ToJson(danhGiangHoRequest, false));
	}

	public bool OnDanhNhanhGiangHoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GiangHoDanhNhanhResponse giangHoDanhNhanhResponse = JsonMapper.ToObject<GiangHoDanhNhanhResponse>(data);
		if (giangHoDanhNhanhResponse != null)
		{
			if (giangHoDanhNhanhResponse.ErrorCode == ERROR_CODE.OK)
			{
				List<UserInfo.HeroData> listCloneHeroFromDoiHinh = UserInfo.GetListCloneHeroFromDoiHinh();
				if (giangHoDanhNhanhResponse.UpdateUserInfo != null && giangHoDanhNhanhResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(giangHoDanhNhanhResponse.UpdateUserInfo);
				}
				ScreenWorldmap screenWorldmap = GUIManager.getScreen(GAME_SCREEN.ScreenWorldmap) as ScreenWorldmap;
				if (screenWorldmap != null)
				{
					screenWorldmap.SyncWithNetworkData();
				}
				if (GiangHoPopup.instance != null)
				{
					GiangHoCfg cfg;
					UserInfo.GiangHoData giangHo;
					if (giangHoDanhNhanhResponse.GiangHoTinhAnh)
					{
						cfg = ConfigManager.instance.m_listGiangHoTinhAnh[GiangHoPopup.instance.giangHoIdx];
						giangHo = UserInfo.GetGiangHoTinhAnhByIdx(GiangHoPopup.instance.giangHoIdx);
					}
					else
					{
						cfg = ConfigManager.instance.m_listGiangHo[GiangHoPopup.instance.giangHoIdx];
						giangHo = UserInfo.GetGiangHoByIdx(GiangHoPopup.instance.giangHoIdx);
					}
					GiangHoPopup.instance.SyncWithNetworkData(giangHo, cfg);
				}
				PopupDanhNhanhGiangHo.Create(listCloneHeroFromDoiHinh, giangHoDanhNhanhResponse);
			}
			else if (giangHoDanhNhanhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(giangHoDanhNhanhResponse.ErrorMessage);
				EGDebug.LogWarning(giangHoDanhNhanhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", giangHoDanhNhanhResponse.ErrorCode, giangHoDanhNhanhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnDanhNhanhGiangHoResponse: response is Null");
		}
		return true;
	}

	public void RequestResetLuotNVGH(int ghIdx, int nvIdx, bool isGHTA)
	{
		DanhGiangHoRequest danhGiangHoRequest = new DanhGiangHoRequest();
		danhGiangHoRequest.GiangHoIdx = ghIdx;
		danhGiangHoRequest.NhiemVuIdx = nvIdx;
		danhGiangHoRequest.GiangHoTinhAnh = isGHTA;
		SendRequest(m_C2SProxy.RequestResetLuotGiangHo, JsonMapper.ToJson(danhGiangHoRequest, false));
	}

	public bool OnResetLuotNVGH(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UserInfo userInfo = JsonMapper.ToObject<UserInfo>(data);
		if (userInfo != null)
		{
			if (userInfo.ErrorCode == ERROR_CODE.OK)
			{
				UserInfo.UpdateInfo(userInfo);
				ScreenWorldmap screenWorldmap = GUIManager.getScreen(GAME_SCREEN.ScreenWorldmap) as ScreenWorldmap;
				screenWorldmap.SyncWithNetworkData();
				if (GiangHoPopup.instance != null)
				{
					GiangHoCfg cfg;
					UserInfo.GiangHoData giangHo;
					if (GiangHoPopup.instance.isGHTinhAnh)
					{
						cfg = ConfigManager.instance.m_listGiangHoTinhAnh[GiangHoPopup.instance.giangHoIdx];
						giangHo = UserInfo.GetGiangHoTinhAnhByIdx(GiangHoPopup.instance.giangHoIdx);
					}
					else
					{
						cfg = ConfigManager.instance.m_listGiangHo[GiangHoPopup.instance.giangHoIdx];
						giangHo = UserInfo.GetGiangHoByIdx(GiangHoPopup.instance.giangHoIdx);
					}
					GiangHoPopup.instance.SyncWithNetworkData(giangHo, cfg);
				}
			}
			else if (userInfo.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(userInfo.ErrorMessage);
				EGDebug.LogWarning(userInfo.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", userInfo.ErrorCode, userInfo.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnAnGaGH : response is null");
		}
		return true;
	}

	public void RequestDanhGiangHo(int ghIdx, int nvIdx, bool isGHTA = false)
	{
		DanhGiangHoRequest danhGiangHoRequest = new DanhGiangHoRequest();
		danhGiangHoRequest.GiangHoIdx = ghIdx;
		danhGiangHoRequest.NhiemVuIdx = nvIdx;
		danhGiangHoRequest.GiangHoTinhAnh = isGHTA;
		if (isIpV6)
		{
			OnDanhGiangHoResponse(HostID.None, new RmiContext(), "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");
		}
		else
		{
			SendRequest(m_C2SProxy.RequestDanhGiangHo, JsonMapper.ToJson(danhGiangHoRequest, false));
		}
	}

	public bool OnDanhGiangHoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GiangHoResponse giangHoResponse = JsonMapper.ToObject<GiangHoResponse>(data);
		if (giangHoResponse != null)
		{
			if (giangHoResponse.ErrorCode == ERROR_CODE.OK)
			{
				List<UserInfo.HeroData> listCloneHeroFromDoiHinh = UserInfo.GetListCloneHeroFromDoiHinh();
				ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
				PopupBattleResult popupBattleResult = PopupBattleResult.Create(giangHoResponse.Battle, listCloneHeroFromDoiHinh, giangHoResponse.ExpMP, giangHoResponse.BacReward, giangHoResponse.ExpDeTu, 0);
				if (UserInfo.DanhHieu != null && giangHoResponse.UpdateUserInfo != null && giangHoResponse.UpdateUserInfo.DanhHieu != null && UserInfo.DanhHieu.GiangHoHaoKiet < giangHoResponse.UpdateUserInfo.DanhHieu.GiangHoHaoKiet)
				{
					PopupDuocThanhTuu popupDuocThanhTuu = PopupDuocThanhTuu.CreateGiangHoHaoKiet(giangHoResponse.UpdateUserInfo.DanhHieu);
					popupDuocThanhTuu.gameObject.SetActive(false);
				}
				if (giangHoResponse.UpdateUserInfo != null && giangHoResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(giangHoResponse.UpdateUserInfo);
				}
				ScreenWorldmap screenWorldmap = GUIManager.getScreen(GAME_SCREEN.ScreenWorldmap) as ScreenWorldmap;
				screenWorldmap.SyncWithNetworkData();
				GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
				popupBattleResult.gameObject.SetActive(false);
				if (GiangHoPopup.instance != null)
				{
					GiangHoPopup.instance.gameObject.SetActive(false);
					popupBattleResult.OnClosePopup = GiangHoPopup.instance.OnCloseResultPanel;
				}
				if (giangHoResponse.PhanThuong != null && giangHoResponse.PhanThuong.PhanThuongList.Count > 0)
				{
					if (giangHoResponse.PhanThuong.PhanThuongList[0].Loai == PhanThuongResponse.LoaiPhanThuong.CAO_NHAN || giangHoResponse.PhanThuong.PhanThuongList[0].Loai == PhanThuongResponse.LoaiPhanThuong.BAN_DO || giangHoResponse.PhanThuong.PhanThuongList[0].Loai == PhanThuongResponse.LoaiPhanThuong.BANG_HUU || giangHoResponse.PhanThuong.PhanThuongList[0].Loai == PhanThuongResponse.LoaiPhanThuong.THUONG_NHAN || giangHoResponse.PhanThuong.PhanThuongList[0].Loai == PhanThuongResponse.LoaiPhanThuong.TY_THI)
					{
						PopupKyNgoGiangHo.Create(giangHoResponse.PhanThuong.PhanThuongList[0]).gameObject.SetActive(false);
					}
					else
					{
						PopupDanhSachPhanThuong popupDanhSachPhanThuong = PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongGiangHoTitle"), Localization.instance.Get("PhanThuongGiangHoDesc"), giangHoResponse.PhanThuong);
						popupDanhSachPhanThuong.gameObject.SetActive(false);
					}
				}
				GiangHoCfg giangHoCfg = ((!giangHoResponse.GiangHoTinhAnh) ? ConfigManager.instance.m_listGiangHo[giangHoResponse.GiangHoIdx] : ConfigManager.instance.m_listGiangHoTinhAnh[giangHoResponse.GiangHoIdx]);
				if (giangHoResponse.NhiemVuIdx == giangHoCfg.NhiemVuList.Count - 1)
				{
					screenBattle.Replay(giangHoResponse.Battle, giangHoCfg.Map, giangHoCfg.HoiThoaiList, giangHoCfg.HoiThoaiTime);
				}
				else
				{
					screenBattle.Replay(giangHoResponse.Battle, giangHoCfg.Map);
				}
				screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenWorldmap;
			}
			else if (giangHoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(giangHoResponse.ErrorMessage);
				EGDebug.LogWarning(giangHoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", giangHoResponse.ErrorCode, giangHoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("GiangHoResponse is null");
		}
		return true;
	}

	public void SendChat(string msg)
	{
		SendRequest(m_C2SProxy.RequestDanhGiangHo, "\"" + msg + "\"", false);
	}

	public bool OnReceiveChatMsg(HostID remote, RmiContext rmiContext, string msg)
	{
		return true;
	}

	public void SetHeroData(UserInfo.HeroData propHero)
	{
		SendRequest(m_C2SProxy.RequestSetHeroData, JsonMapper.ToJson(propHero, false));
	}

	public void SetVCSetting(UserInfo.VCThietLapData vcThietLap)
	{
		SendRequest(m_C2SProxy.RequestSetVCSetting, JsonMapper.ToJson(vcThietLap, false));
	}

	public bool OnSetHeroDataResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UserInfo userInfo = JsonMapper.ToObject<UserInfo>(data);
		if (userInfo != null)
		{
			if (userInfo.ErrorCode == ERROR_CODE.OK)
			{
				UserInfo.UpdateInfo(userInfo);
				ScreenChienThuat screenChienThuat = GUIManager.getScreen(GAME_SCREEN.ScreenChienThuat) as ScreenChienThuat;
				screenChienThuat.SyncWithNetworkData();
			}
			else if (userInfo.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(userInfo.ErrorMessage);
				EGDebug.LogWarning(userInfo.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", userInfo.ErrorCode, userInfo.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetHeroDataResponse : response is null");
		}
		return true;
	}

	public bool OnStartDongNhan(HostID remote, RmiContext rmiContext, string data)
	{
		try
		{
			if (GameManager.instance.m_GameClient.UserInfo == null || GameManager.instance.m_GameClient.UserInfo.Gamer == null)
			{
				FailedStartDongNhan = true;
				return true;
			}
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level < 12)
			{
				return true;
			}
			DongNhanResponse dongNhanResponse = JsonMapper.ToObject<DongNhanResponse>(data);
			if (dongNhanResponse != null)
			{
				if (dongNhanResponse.ErrorCode == ERROR_CODE.OK)
				{
					string text = string.Format(Localization.instance.Get("DongNhanXuatHienMsg"), dongNhanResponse.LevelDongNhan, dongNhanResponse.MauDongNhan);
					EGDebug.LogWarning(text);
					MessagePopup.Create(text);
					ScreenDongNhan screenDongNhan = GUIManager.getScreen(GAME_SCREEN.ScreenDongNhan) as ScreenDongNhan;
					screenDongNhan.SyncWithNetworkData(dongNhanResponse);
					screenDongNhan.StartDongNhan(dongNhanResponse);
				}
				else if (dongNhanResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
				{
					MessagePopup.Create(dongNhanResponse.ErrorMessage);
					EGDebug.LogWarning(dongNhanResponse.ErrorMessage);
				}
				else
				{
					string text2 = string.Format("ErrorCode: {0}, Message: {1}", dongNhanResponse.ErrorCode, dongNhanResponse.ErrorMessage);
					MessagePopup.Create(text2);
					EGDebug.LogWarning(text2);
				}
			}
			else
			{
				EGDebug.LogError("OnStartDongNhan: response is null");
			}
		}
		catch (Exception ex)
		{
			MessagePopup.Create(ex.ToString());
		}
		return true;
	}

	public bool OnEndDongNhan(HostID remote, RmiContext rmiContext, string data)
	{
		if (GameManager.instance.m_GameClient.UserInfo == null || GameManager.instance.m_GameClient.UserInfo.Gamer == null || GameManager.instance.m_GameClient.UserInfo.Gamer.Level < 12)
		{
			return true;
		}
		DongNhanResponse dongNhanResponse = JsonMapper.ToObject<DongNhanResponse>(data);
		if (dongNhanResponse != null)
		{
			if (dongNhanResponse.ErrorCode == ERROR_CODE.OK)
			{
				string text = Localization.instance.Get("DongNhanKetThucMsg");
				EGDebug.LogWarning(text);
				MessagePopup.Create(text);
				ScreenDongNhan screenDongNhan = GUIManager.getScreen(GAME_SCREEN.ScreenDongNhan) as ScreenDongNhan;
				screenDongNhan.SyncWithNetworkData(dongNhanResponse);
				screenDongNhan.DestroyDongNhan();
			}
			else if (dongNhanResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(dongNhanResponse.ErrorMessage);
				EGDebug.LogWarning(dongNhanResponse.ErrorMessage);
			}
			else
			{
				string text2 = string.Format("ErrorCode: {0}, Message: {1}", dongNhanResponse.ErrorCode, dongNhanResponse.ErrorMessage);
				MessagePopup.Create(text2);
				EGDebug.LogWarning(text2);
			}
		}
		else
		{
			EGDebug.LogError("OnEndDongNhan: response is null");
		}
		return true;
	}

	public void GetDongNhanInfo()
	{
		SendRequest(m_C2SProxy.RequestGetDongNhanInfo, string.Empty);
	}

	public bool OnGetDongNhanInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DongNhanResponse dongNhanResponse = JsonMapper.ToObject<DongNhanResponse>(data);
		if (dongNhanResponse != null)
		{
			if (dongNhanResponse.ErrorCode == ERROR_CODE.OK)
			{
				FailedStartDongNhan = false;
				ScreenDongNhan screenDongNhan = GUIManager.getScreen(GAME_SCREEN.ScreenDongNhan) as ScreenDongNhan;
				screenDongNhan.SyncWithNetworkData(dongNhanResponse);
				if (PopupAutoTayDoc.instance != null)
				{
					PopupAutoTayDoc.instance.UpdateInfoTayDoc(dongNhanResponse);
				}
			}
			else if (dongNhanResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(dongNhanResponse.ErrorMessage);
				EGDebug.LogWarning(dongNhanResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", dongNhanResponse.ErrorCode, dongNhanResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("Lỗi setHeroData");
		}
		return true;
	}

	public void RequestDanhDongNhan(bool dungVang)
	{
		DongNhanRequest dongNhanRequest = new DongNhanRequest();
		if (dungVang)
		{
			dongNhanRequest.TypeRequest = DongNhanRequest.RequestType.HoiSinhNgay;
		}
		else
		{
			dongNhanRequest.TypeRequest = DongNhanRequest.RequestType.BinhThuong;
		}
		SendRequest(m_C2SProxy.RequestDanhDongNhan, JsonMapper.ToJson(dongNhanRequest, false));
	}

	public bool OnDanhDongNhanResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DongNhanBattleResponse dongNhanBattleResponse = JsonMapper.ToObject<DongNhanBattleResponse>(data);
		if (dongNhanBattleResponse != null)
		{
			if (dongNhanBattleResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (UserInfo.DanhHieu != null && dongNhanBattleResponse.UpdateUserInfo != null && dongNhanBattleResponse.UpdateUserInfo.DanhHieu != null && UserInfo.DanhHieu.DungSiXungTran < dongNhanBattleResponse.UpdateUserInfo.DanhHieu.DungSiXungTran)
				{
					PopupDuocThanhTuu popupDuocThanhTuu = PopupDuocThanhTuu.CreateDungSiXungTran(dongNhanBattleResponse.UpdateUserInfo.DanhHieu);
					popupDuocThanhTuu.gameObject.SetActive(false);
				}
				if (dongNhanBattleResponse.UpdateUserInfo != null && dongNhanBattleResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(dongNhanBattleResponse.UpdateUserInfo);
				}
				if (dongNhanBattleResponse.PhanThuong != null && dongNhanBattleResponse.PhanThuong.UpdateUserInfo != null && dongNhanBattleResponse.PhanThuong.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(dongNhanBattleResponse.PhanThuong.UpdateUserInfo);
				}
				ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
				ScreenDongNhan screenDongNhan = GUIManager.getScreen(GAME_SCREEN.ScreenDongNhan) as ScreenDongNhan;
				if (dongNhanBattleResponse.Battle != null)
				{
					GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
					screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenDongNhan;
					screenBattle.Replay(dongNhanBattleResponse.Battle);
					screenBattle.OnFinishReplay += screenDongNhan.OnFinishDanhDongNhan;
				}
				if (dongNhanBattleResponse.PhanThuong != null && dongNhanBattleResponse.PhanThuong.PhanThuongList.Count > 0)
				{
					PopupDanhSachPhanThuong popupDanhSachPhanThuong = PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongNhanDuoc"), dongNhanBattleResponse.PhanThuong);
					popupDanhSachPhanThuong.gameObject.SetActive(false);
				}
				screenDongNhan.SyncWithNetworkData(dongNhanBattleResponse.DongNhan);
				if (PopupAutoTayDoc.instance != null)
				{
					PopupAutoTayDoc.instance.UpdateInfoTayDoc(dongNhanBattleResponse.DongNhan);
				}
			}
			else if (dongNhanBattleResponse.ErrorCode == ERROR_CODE.MAT_DONG_BO)
			{
				ScreenDongNhan screenDongNhan2 = GUIManager.getScreen(GAME_SCREEN.ScreenDongNhan) as ScreenDongNhan;
				screenDongNhan2.SyncWithNetworkData(dongNhanBattleResponse.DongNhan);
				if (PopupAutoTayDoc.instance != null)
				{
					PopupAutoTayDoc.instance.UpdateInfoTayDoc(dongNhanBattleResponse.DongNhan);
				}
				if (dongNhanBattleResponse.UpdateUserInfo != null && dongNhanBattleResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(dongNhanBattleResponse.UpdateUserInfo);
				}
			}
			else if (dongNhanBattleResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(dongNhanBattleResponse.ErrorMessage);
				EGDebug.LogWarning(dongNhanBattleResponse.ErrorMessage);
				if (PopupAutoTayDoc.instance != null)
				{
					PopupAutoTayDoc.instance.RequestAgaint();
				}
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", dongNhanBattleResponse.ErrorCode, dongNhanBattleResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnDanhDongNhanResponse: response is null");
		}
		return true;
	}

	public void RequestGetDiHoaCungInfo()
	{
		SendRequest(m_C2SProxy.RequestGetDiHoaCungInfo, string.Empty);
	}

	public bool OnGetDiHoaCungInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DiHoaCungInfoResponse diHoaCungInfoResponse = JsonMapper.ToObject<DiHoaCungInfoResponse>(data);
		if (diHoaCungInfoResponse != null)
		{
			if (diHoaCungInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKyNgoDiHoaCung)
				{
					ScreenKyNgo_DiHoaCung screenKyNgo_DiHoaCung = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoDiHoaCung) as ScreenKyNgo_DiHoaCung;
					screenKyNgo_DiHoaCung.updateInfo(diHoaCungInfoResponse.TongGachAll);
				}
			}
			else if (diHoaCungInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(diHoaCungInfoResponse.ErrorMessage);
				EGDebug.LogWarning(diHoaCungInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", diHoaCungInfoResponse.ErrorCode, diHoaCungInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetDiHoaCungInfoResponse : response null");
		}
		return true;
	}

	public void RequestGetTopDiHoaCung()
	{
		SendRequest(m_C2SProxy.RequestGetTopDiHoaCung, string.Empty);
	}

	public bool OnGetTopDiHoaCungResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		TopDiHoaCungResponse topDiHoaCungResponse = JsonMapper.ToObject<TopDiHoaCungResponse>(data);
		if (topDiHoaCungResponse != null)
		{
			if (topDiHoaCungResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTopDiHoaCung)
				{
					ScreenTopDiHoaCung screenTopDiHoaCung = GUIManager.getScreen(GAME_SCREEN.ScreenTopDiHoaCung) as ScreenTopDiHoaCung;
					screenTopDiHoaCung.displayListTopDiHoaCung(topDiHoaCungResponse);
				}
			}
			else if (topDiHoaCungResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(topDiHoaCungResponse.ErrorMessage);
				EGDebug.LogWarning(topDiHoaCungResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", topDiHoaCungResponse.ErrorCode, topDiHoaCungResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetTopDiHoaCungResponse : response null");
		}
		return true;
	}

	public void RequestNhanThuongDiHoaCungAll(NhanThuongDiHoaCungAllRequest request)
	{
		SendRequest(m_C2SProxy.RequestNhanThuongDiHoaCungAll, JsonMapper.ToJson(request, false));
	}

	public bool OnNhanThuongDiHoaCungAllResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDiHoaCungDetail)
				{
					ScreenDiHoaCungDetail screenDiHoaCungDetail = GUIManager.getScreen(GAME_SCREEN.ScreenDiHoaCungDetail) as ScreenDiHoaCungDetail;
					screenDiHoaCungDetail.updateInfo(phanThuongResponse);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKyNgoDiHoaCung)
				{
					ScreenKyNgo_DiHoaCung screenKyNgo_DiHoaCung = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoDiHoaCung) as ScreenKyNgo_DiHoaCung;
					screenKyNgo_DiHoaCung.displayPhanPhuong(phanThuongResponse);
				}
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetTopDiHoaCungResponse : response null");
		}
		return true;
	}

	public void RequestPhanRaTrangBi(BanTrangBiRequest request)
	{
		SendRequest(m_C2SProxy.RequestPhanRaTrangBi, JsonMapper.ToJson(request, false));
	}

	public bool OnPhanRaTrangBiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTrangBi)
				{
					ScreenTrangBi screenTrangBi = GUIManager.getScreen(GAME_SCREEN.ScreenTrangBi) as ScreenTrangBi;
					screenTrangBi.openPopUpPhanThuong(phanThuongResponse);
				}
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnPhanRaTrangBiResponse : response null");
		}
		return true;
	}

	public void RequestKhamNgoc(KhamNgocRequest request)
	{
		SendRequest(m_C2SProxy.RequestKhamNgoc, JsonMapper.ToJson(request, false));
	}

	public bool OnKhamNgocResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		KhamNgocResponse khamNgocResponse = JsonMapper.ToObject<KhamNgocResponse>(data);
		if (khamNgocResponse != null)
		{
			if (khamNgocResponse.ErrorCode == ERROR_CODE.OK)
			{
				UserInfo.UpdateInfo(khamNgocResponse.UpdateInfo);
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKhamNam)
				{
					ScreenKhamNam screenKhamNam = GUIManager.getScreen(GAME_SCREEN.ScreenKhamNam) as ScreenKhamNam;
					screenKhamNam.updateInfo(true);
				}
			}
			else if (khamNgocResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(khamNgocResponse.ErrorMessage);
				EGDebug.LogWarning(khamNgocResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", khamNgocResponse.ErrorCode, khamNgocResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnKhamNgocResponse : response null");
		}
		return true;
	}

	public void RequestGoNgoc(GoNgocRequest request)
	{
		SendRequest(m_C2SProxy.RequestGoNgoc, JsonMapper.ToJson(request, false));
	}

	public bool OnGoNgocResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GoNgocResponse goNgocResponse = JsonMapper.ToObject<GoNgocResponse>(data);
		if (goNgocResponse != null)
		{
			if (goNgocResponse.ErrorCode == ERROR_CODE.OK)
			{
				UserInfo.UpdateInfo(goNgocResponse.UpdateInfo);
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKhamNam)
				{
					ScreenKhamNam screenKhamNam = GUIManager.getScreen(GAME_SCREEN.ScreenKhamNam) as ScreenKhamNam;
					screenKhamNam.updateInfo(false);
				}
			}
			else if (goNgocResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(goNgocResponse.ErrorMessage);
				EGDebug.LogWarning(goNgocResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", goNgocResponse.ErrorCode, goNgocResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGoNgocResponse : response null");
		}
		return true;
	}

	public void RequestTuLuyenDeTu(int HeroID, int TanHonID)
	{
		TuLuyenDeTuRequest tuLuyenDeTuRequest = new TuLuyenDeTuRequest();
		tuLuyenDeTuRequest.HeroID = HeroID;
		tuLuyenDeTuRequest.HonID = TanHonID;
		SendRequest(m_C2SProxy.RequestTuLuyenDeTu, JsonMapper.ToJson(tuLuyenDeTuRequest, false));
	}

	public bool OnTuLuyenDeTuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		TuLuyenDeTuResponse tuLuyenDeTuResponse = JsonMapper.ToObject<TuLuyenDeTuResponse>(data);
		if (tuLuyenDeTuResponse != null)
		{
			if (tuLuyenDeTuResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (tuLuyenDeTuResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(tuLuyenDeTuResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTuLuyen)
				{
					ScreenTuLuyen screenTuLuyen = GUIManager.getScreen(GAME_SCREEN.ScreenTuLuyen) as ScreenTuLuyen;
					screenTuLuyen.updateDeTuInfo();
				}
			}
			else if (tuLuyenDeTuResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(tuLuyenDeTuResponse.ErrorMessage);
				EGDebug.LogWarning(tuLuyenDeTuResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", tuLuyenDeTuResponse.ErrorCode, tuLuyenDeTuResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnTuLuyenDeTuResponse : response null");
		}
		return true;
	}

	public void RequestTrieuHonDeTuBangHon(int TanHonID)
	{
		TrieuHoiDeTuBangHonRequest trieuHoiDeTuBangHonRequest = new TrieuHoiDeTuBangHonRequest();
		trieuHoiDeTuBangHonRequest.HonID = TanHonID;
		SendRequest(m_C2SProxy.RequestTrieuHoiDeTuBangHon, JsonMapper.ToJson(trieuHoiDeTuBangHonRequest, false));
	}

	public bool OnTrieuHonDeTuBangHonResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		TrieuHoiDeTuBangHonResponse trieuHoiDeTuBangHonResponse = JsonMapper.ToObject<TrieuHoiDeTuBangHonResponse>(data);
		if (trieuHoiDeTuBangHonResponse != null)
		{
			if (trieuHoiDeTuBangHonResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (trieuHoiDeTuBangHonResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(trieuHoiDeTuBangHonResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDeTu)
				{
					ScreenDeTu screenDeTu = GUIManager.getScreen(GAME_SCREEN.ScreenDeTu) as ScreenDeTu;
					screenDeTu.updateListTanHon(trieuHoiDeTuBangHonResponse);
				}
			}
			else if (trieuHoiDeTuBangHonResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(trieuHoiDeTuBangHonResponse.ErrorMessage);
				EGDebug.LogWarning(trieuHoiDeTuBangHonResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", trieuHoiDeTuBangHonResponse.ErrorCode, trieuHoiDeTuBangHonResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnTrieuHonDeTuBangHonResponse : response null");
		}
		return true;
	}

	public void RequestThanTai(ThanTaiRequest request)
	{
		SendRequest(m_C2SProxy.RequestThanTai, JsonMapper.ToJson(request, false));
	}

	public bool OnThanTaiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThanTaiResponse thanTaiResponse = JsonMapper.ToObject<ThanTaiResponse>(data);
		if (thanTaiResponse != null)
		{
			if (thanTaiResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thanTaiResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(thanTaiResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKyNgoThanTai)
				{
					ScreenKyNgo_ThanTai screenKyNgo_ThanTai = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoThanTai) as ScreenKyNgo_ThanTai;
					screenKyNgo_ThanTai.displaySoKNBNhanDuoc(thanTaiResponse.SoKnbNhanDuoc);
					screenKyNgo_ThanTai.updateMainMenuView();
				}
			}
			else if (thanTaiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thanTaiResponse.ErrorMessage);
				EGDebug.LogWarning(thanTaiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thanTaiResponse.ErrorCode, thanTaiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSelectStartDeTuResponse : response null");
		}
		return true;
	}

	public void RequestLenCapNhanThuong(LenCapNhanThuongRequest request)
	{
		SendRequest(m_C2SProxy.RequestLenCapNhanThuong, JsonMapper.ToJson(request, false));
	}

	public bool OnLenCapNhanThuongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKyNgoThangCap)
				{
					ScreenKyNgo_ThangCap screenKyNgo_ThangCap = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoThangCap) as ScreenKyNgo_ThangCap;
					screenKyNgo_ThangCap.getListThangCap();
					screenKyNgo_ThangCap.updateMainMenuView();
					PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), phanThuongResponse);
				}
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSelectStartDeTuResponse : response null");
		}
		return true;
	}

	public void RequestCuuVienTieuPhong(CuuVienTieuPhongRequest request)
	{
		SendRequest(m_C2SProxy.RequestCuuVienTieuPhong, JsonMapper.ToJson(request, false));
	}

	public bool OnCuuVienTieuPhongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKyNgoCuuTieuPhong)
				{
					ScreenKyNgo_CuuTieuPhong screenKyNgo_CuuTieuPhong = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoCuuTieuPhong) as ScreenKyNgo_CuuTieuPhong;
					screenKyNgo_CuuTieuPhong.updateInfo(phanThuongResponse);
				}
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnCuuVienTieuPhongResponse : response null");
		}
		return true;
	}

	public void RequestChatInfo(ChatInfoRequest request)
	{
		SendRequest(m_C2SProxy.RequestChatInfo, JsonMapper.ToJson(request, false));
	}

	public bool OnChatInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ChatInfoResponse chatInfoResponse = JsonMapper.ToObject<ChatInfoResponse>(data);
		if (chatInfoResponse != null)
		{
			if (chatInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenChat)
				{
					ScreenChat screenChat = GUIManager.getScreen(GAME_SCREEN.ScreenChat) as ScreenChat;
					screenChat.displayListChatItem(chatInfoResponse.listChat);
				}
				else if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenLienMinhMain)
				{
					ScreenLienMinhMain screenLienMinhMain = GUIManager.getScreen(GAME_SCREEN.ScreenLienMinhMain) as ScreenLienMinhMain;
					screenLienMinhMain.displayListChatItem(chatInfoResponse.listChat);
				}
				else if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenThanhChien)
				{
					ScreenThanhChien screenThanhChien = GUIManager.getScreen(GAME_SCREEN.ScreenThanhChien) as ScreenThanhChien;
					screenThanhChien.displayListChatItem(chatInfoResponse.listChat);
				}
			}
			else if (chatInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(chatInfoResponse.ErrorMessage);
				EGDebug.LogWarning(chatInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", chatInfoResponse.ErrorCode, chatInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnChatInfoResponse : response null");
		}
		return true;
	}

	public void RequestSendChatAll(SendChatRequest request)
	{
		SendRequest(m_C2SProxy.RequestSendChatAll, JsonMapper.ToJson(request, false), false);
	}

	public bool OnGetChatAllResponse(HostID remote, RmiContext rmiContext, string data)
	{
		NewChatResponse newChatResponse = JsonMapper.ToObject<NewChatResponse>(data);
		if (newChatResponse != null)
		{
			if (newChatResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenChat)
				{
					ScreenChat screenChat = GUIManager.getScreen(GAME_SCREEN.ScreenChat) as ScreenChat;
					screenChat.addItemToChatList(newChatResponse.NewItem);
				}
				else if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenLienMinhMain)
				{
					ScreenLienMinhMain screenLienMinhMain = GUIManager.getScreen(GAME_SCREEN.ScreenLienMinhMain) as ScreenLienMinhMain;
					screenLienMinhMain.addItemToChatList(newChatResponse.NewItem);
				}
				else if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenThanhChien)
				{
					ScreenThanhChien screenThanhChien = GUIManager.getScreen(GAME_SCREEN.ScreenThanhChien) as ScreenThanhChien;
					if (!screenThanhChien.chatGrp.activeInHierarchy)
					{
						PopUpNewMess.Create(newChatResponse.NewItem);
					}
					screenThanhChien.addItemToChatList(newChatResponse.NewItem);
				}
				else if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenChatLanhDia)
				{
					ScreenChat screenChat2 = GUIManager.getScreen(GAME_SCREEN.ScreenChatLanhDia) as ScreenChat;
					screenChat2.addItemToChatList(newChatResponse.NewItem);
				}
				else
				{
					string key = "thongbaoChat_value" + GameManager.instance.m_GameClient.UserInfo.Gamer.ID;
					if (PlayerPrefs.GetInt(key, 1) == 1 && GUIManager.instance.CurrentScreen != GAME_SCREEN.ScreenSelectFirstDeTu && GUIManager.instance.CurrentScreen != GAME_SCREEN.ScreenBattle && GameManager.instance.m_GameClient.UserInfo.Gamer.Level >= 2)
					{
						PopUpNewMess.Create(newChatResponse.NewItem);
					}
				}
			}
			else if (newChatResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(newChatResponse.ErrorMessage);
				EGDebug.LogWarning(newChatResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", newChatResponse.ErrorCode, newChatResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetChatAllResponse : response null");
		}
		return true;
	}

	public void RequestNhanRuongThachSanh(NhanRuongThachSanhRequest request)
	{
		SendRequest(m_C2SProxy.RequestNhanRuongThachSanh, JsonMapper.ToJson(request, false));
	}

	public bool OnNhanRuongThachSanhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenRuongThachSanh)
				{
					ScreenRuongThachSanh screenRuongThachSanh = GUIManager.getScreen(GAME_SCREEN.ScreenRuongThachSanh) as ScreenRuongThachSanh;
					screenRuongThachSanh.updateInfo(phanThuongResponse);
				}
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnNhanRuongThachSanhResponse : response null");
		}
		return true;
	}

	public void RequestPaymentConfirm(PaymentRequest request)
	{
		SendRequest(m_C2SProxy.RequestPaymentConfirm, JsonMapper.ToJson(request, false));
	}

	public bool OnPaymentConfirmResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PaymentResponse paymentResponse = JsonMapper.ToObject<PaymentResponse>(data);
		if (paymentResponse != null)
		{
			if (paymentResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (paymentResponse.PhanThuong.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(paymentResponse.PhanThuong.UpdateUserInfo);
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), string.Format(Localization.instance.Get("ChuongMonDaNapMess"), paymentResponse.RealMoneyCount), paymentResponse.PhanThuong);
				if ((PopUpUuDaiVIP.instance != null || PopUpNapTien.instance != null) && PopUpNapTien.instance != null)
				{
					PopUpNapTien.instance.displayUserInfo();
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenRuongThachSanh)
				{
					ScreenRuongThachSanh screenRuongThachSanh = GUIManager.getScreen(GAME_SCREEN.ScreenRuongThachSanh) as ScreenRuongThachSanh;
					if (PopUpNapTien.instance != null)
					{
						PopUpNapTien.DestroyPopup();
					}
					screenRuongThachSanh.displayRuongInfo();
				}
				SohaSDKManager.instance.FinishPayment();
			}
			else if (paymentResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(paymentResponse.ErrorMessage);
				EGDebug.LogWarning(paymentResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", paymentResponse.ErrorCode, paymentResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnPaymentConfirmResponse : response null");
		}
		return true;
	}

	public void RequestUongRuouTieuPhong(UongRuouTieuPhongRequest request)
	{
		SendRequest(m_C2SProxy.RequestUongRuouTieuPhong, JsonMapper.ToJson(request, false));
	}

	public bool OnUongRuouTieuPhongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UongRuouTieuPhongResponse uongRuouTieuPhongResponse = JsonMapper.ToObject<UongRuouTieuPhongResponse>(data);
		if (uongRuouTieuPhongResponse != null)
		{
			if (uongRuouTieuPhongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (uongRuouTieuPhongResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(uongRuouTieuPhongResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKyNgoUongRuou)
				{
					ScreenKyNgo_UongRuou screenKyNgo_UongRuou = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoUongRuou) as ScreenKyNgo_UongRuou;
					screenKyNgo_UongRuou.displayInfo();
					screenKyNgo_UongRuou.openPopUpMess(uongRuouTieuPhongResponse.TiemLuc);
					screenKyNgo_UongRuou.updateMainMenuStatus();
				}
			}
			else if (uongRuouTieuPhongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(uongRuouTieuPhongResponse.ErrorMessage);
				EGDebug.LogWarning(uongRuouTieuPhongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", uongRuouTieuPhongResponse.ErrorCode, uongRuouTieuPhongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnUongRuouTieuPhongResponse : response null");
		}
		return true;
	}

	public void RequestDangNhapNhanThuong(DangNhapNhanThuongRequest request)
	{
		SendRequest(m_C2SProxy.RequestDangNhapNhanThuong, JsonMapper.ToJson(request, false));
	}

	public bool OnDangNhapNhanThuongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDangNhapNhanThuong)
				{
					ScreenKyNgo_DangNhapNhanThuong screenKyNgo_DangNhapNhanThuong = GUIManager.getScreen(GAME_SCREEN.ScreenDangNhapNhanThuong) as ScreenKyNgo_DangNhapNhanThuong;
					screenKyNgo_DangNhapNhanThuong.updateView(phanThuongResponse);
				}
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("DangNhapNhanThuong : response null");
		}
		return true;
	}

	public void RequestGetDoiRuouInfo(GetDoiRuouInfoRequest request)
	{
		SendRequest(m_C2SProxy.RequestGetDoiRuouInfo, JsonMapper.ToJson(request, false));
	}

	public bool OnGetDoiRuouInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetDoiRuouInfoResponse getDoiRuouInfoResponse = JsonMapper.ToObject<GetDoiRuouInfoResponse>(data);
		if (getDoiRuouInfoResponse != null)
		{
			if (getDoiRuouInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKyNgoUongRuou)
				{
					ScreenKyNgo_UongRuou screenKyNgo_UongRuou = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoUongRuou) as ScreenKyNgo_UongRuou;
					screenKyNgo_UongRuou.updateView(getDoiRuouInfoResponse);
				}
			}
			else if (getDoiRuouInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getDoiRuouInfoResponse.ErrorMessage);
				EGDebug.LogWarning(getDoiRuouInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getDoiRuouInfoResponse.ErrorCode, getDoiRuouInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetDoiRuouInfoResponse : response null");
		}
		return true;
	}

	public void RequestDoiRuou(DoiRuouRequest request)
	{
		SendRequest(m_C2SProxy.RequestDoiRuou, JsonMapper.ToJson(request, false));
	}

	public bool OnDoiRuouResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DoiRuouResponse doiRuouResponse = JsonMapper.ToObject<DoiRuouResponse>(data);
		if (doiRuouResponse != null)
		{
			if (doiRuouResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (doiRuouResponse.PhanThuongResponse != null)
				{
					UserInfo.UpdateInfo(doiRuouResponse.PhanThuongResponse.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKyNgoUongRuou)
				{
					ScreenKyNgo_UongRuou screenKyNgo_UongRuou = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoUongRuou) as ScreenKyNgo_UongRuou;
					screenKyNgo_UongRuou.updateView(doiRuouResponse.UpdateDoiRuouInfo);
					screenKyNgo_UongRuou.displayInfo();
				}
			}
			else if (doiRuouResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(doiRuouResponse.ErrorMessage);
				EGDebug.LogWarning(doiRuouResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", doiRuouResponse.ErrorCode, doiRuouResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnUongRuouTieuPhongResponse : response null");
		}
		return true;
	}

	public void RequestSendChatLienSrv(SendChatRequest request)
	{
		SendRequest(m_C2SProxy.RequestSendChatLienSrv, JsonMapper.ToJson(request, false));
	}

	public void RequestGetChatLienSrv()
	{
		SendRequest(m_C2SProxy.RequestGetChatLienSrv, string.Empty, false);
	}

	public bool OnChatLienSrvResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ChatInfoResponse chatInfoResponse = JsonMapper.ToObject<ChatInfoResponse>(data);
		if (chatInfoResponse != null)
		{
			if (chatInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenChat)
				{
					ScreenChat screenChat = GUIManager.getScreen(GAME_SCREEN.ScreenChat) as ScreenChat;
					screenChat.displayLienSrvListChatItem(chatInfoResponse.listChat);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenChatLanhDia)
				{
					ScreenChat screenChat2 = GUIManager.getScreen(GAME_SCREEN.ScreenChatLanhDia) as ScreenChat;
					screenChat2.displayLienSrvListChatItem(chatInfoResponse.listChat);
				}
			}
			else if (chatInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(chatInfoResponse.ErrorMessage);
				EGDebug.LogWarning(chatInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", chatInfoResponse.ErrorCode, chatInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnXocDiaInfoResponse : response null");
		}
		return true;
	}

	public void RequestXocDiaInfo(XocDiaInfoRequest request)
	{
		SendRequest(m_C2SProxy.RequestXocDiaInfo, JsonMapper.ToJson(request, false));
	}

	public bool OnXocDiaInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		XocDiaInfoResponse xocDiaInfoResponse = JsonMapper.ToObject<XocDiaInfoResponse>(data);
		if (xocDiaInfoResponse != null)
		{
			if (xocDiaInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKyNgoDanhBac)
				{
					ScreenKyNgo_DanhBac screenKyNgo_DanhBac = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoDanhBac) as ScreenKyNgo_DanhBac;
					screenKyNgo_DanhBac.displayListItem(xocDiaInfoResponse);
				}
			}
			else if (xocDiaInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(xocDiaInfoResponse.ErrorMessage);
				EGDebug.LogWarning(xocDiaInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", xocDiaInfoResponse.ErrorCode, xocDiaInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnXocDiaInfoResponse : response null");
		}
		return true;
	}

	public void RequestChoiXocDia(ChoiXocDiaRequest request)
	{
		SendRequest(m_C2SProxy.RequestChoiXocDia, JsonMapper.ToJson(request, false));
	}

	public bool OnChoiXocDiaResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ChoiXocDiaResponse choiXocDiaResponse = JsonMapper.ToObject<ChoiXocDiaResponse>(data);
		if (choiXocDiaResponse != null)
		{
			if (choiXocDiaResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (choiXocDiaResponse.PhanThuongResponse != null && choiXocDiaResponse.PhanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(choiXocDiaResponse.PhanThuongResponse.UpdateUserInfo);
				}
				if (PopUpKyNgoDanhBac.instance != null)
				{
					PopUpKyNgoDanhBac.instance.startPlayAnim(choiXocDiaResponse);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKyNgoDanhBac)
				{
					ScreenKyNgo_DanhBac screenKyNgo_DanhBac = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoDanhBac) as ScreenKyNgo_DanhBac;
					screenKyNgo_DanhBac.displayListItem(choiXocDiaResponse.UpdateXocDiaInfo);
				}
			}
			else if (choiXocDiaResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(choiXocDiaResponse.ErrorMessage);
				EGDebug.LogWarning(choiXocDiaResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", choiXocDiaResponse.ErrorCode, choiXocDiaResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnXocDiaInfoResponse : response null");
		}
		return true;
	}

	public void RequestHuaNguyen(HuaNguyenRequest request)
	{
		SendRequest(m_C2SProxy.RequestHuaNguyen, JsonMapper.ToJson(request, false));
	}

	public bool OnHuaNguyenResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		HuaNguyenResponse huaNguyenResponse = JsonMapper.ToObject<HuaNguyenResponse>(data);
		if (huaNguyenResponse != null)
		{
			if (huaNguyenResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (huaNguyenResponse.phanthuong.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(huaNguyenResponse.phanthuong.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKyNgoHuaNguyen)
				{
					ScreenKyNgo_HuaNguyen screenKyNgo_HuaNguyen = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoHuaNguyen) as ScreenKyNgo_HuaNguyen;
					screenKyNgo_HuaNguyen.updateInfo(huaNguyenResponse);
					screenKyNgo_HuaNguyen.updateMainMenuView();
				}
			}
			else if (huaNguyenResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(huaNguyenResponse.ErrorMessage);
				EGDebug.LogWarning(huaNguyenResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", huaNguyenResponse.ErrorCode, huaNguyenResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSelectStartDeTuResponse : response null");
		}
		return true;
	}

	public void RequestSelectStartDeTu(SelectStartDeTuRequest request)
	{
		SendRequest(m_C2SProxy.RequestSelectStartDeTu, JsonMapper.ToJson(request, false));
	}

	public bool OnSelectStartDeTuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SelectStartDeTuResponse selectStartDeTuResponse = JsonMapper.ToObject<SelectStartDeTuResponse>(data);
		if (selectStartDeTuResponse != null)
		{
			if (selectStartDeTuResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (selectStartDeTuResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(selectStartDeTuResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenSelectNhanVatFullItem)
				{
					ScreenSelectTheFirstHorse screenSelectTheFirstHorse = (ScreenSelectTheFirstHorse)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenSelectTheFirstHorse);
					GUIManager.setScreen(GAME_SCREEN.ScreenSelectTheFirstHorse);
				}
			}
			else if (selectStartDeTuResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(selectStartDeTuResponse.ErrorMessage);
				EGDebug.LogWarning(selectStartDeTuResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", selectStartDeTuResponse.ErrorCode, selectStartDeTuResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSelectStartDeTuResponse : response null");
		}
		return true;
	}

	public void RequestKyNgoThamBai(KyNgoThamBaiRequest request)
	{
		SendRequest(m_C2SProxy.RequestKyNgoThamBai, JsonMapper.ToJson(request, false));
	}

	public bool OnKyNgoThamBaiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKyNgoThamBai)
				{
					ScreenKyNgo_ThamBai screenKyNgo_ThamBai = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoThamBai) as ScreenKyNgo_ThamBai;
					screenKyNgo_ThamBai.updateInfo(phanThuongResponse);
					screenKyNgo_ThamBai.updateMainMenuView();
				}
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnKyNgoThamBaiResponse : response null");
		}
		return true;
	}

	public void RequestTruyenCong(TruyenCongRequest request)
	{
		SendRequest(m_C2SProxy.RequestTruyenCong, JsonMapper.ToJson(request, false));
	}

	public bool OnTruyenCongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		TruyenCongResponse truyenCongResponse = JsonMapper.ToObject<TruyenCongResponse>(data);
		if (truyenCongResponse != null)
		{
			if (truyenCongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (truyenCongResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(truyenCongResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTruyenCong)
				{
					ScreenTruyenCong screenTruyenCong = GUIManager.getScreen(GAME_SCREEN.ScreenTruyenCong) as ScreenTruyenCong;
					screenTruyenCong.startPlayAnim();
				}
			}
			else if (truyenCongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(truyenCongResponse.ErrorMessage);
				EGDebug.LogWarning(truyenCongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", truyenCongResponse.ErrorCode, truyenCongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnTruyenCongResponse : response null");
		}
		return true;
	}

	public void RequestSetDoiHinh(int doi_hinh_idx, int hero_id)
	{
		SetDoiHinhRequest setDoiHinhRequest = new SetDoiHinhRequest();
		setDoiHinhRequest.Slot = doi_hinh_idx;
		setDoiHinhRequest.HeroID = hero_id;
		SendRequest(m_C2SProxy.RequestSetDoiHinh, JsonMapper.ToJson(setDoiHinhRequest, false));
	}

	public bool OnSetDoiHinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SetDoiHinhResponse setDoiHinhResponse = JsonMapper.ToObject<SetDoiHinhResponse>(data);
		if (setDoiHinhResponse != null)
		{
			if (setDoiHinhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (setDoiHinhResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(setDoiHinhResponse.UpdateInfo);
				}
				ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
				if (screenDoiHinh != null)
				{
					if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
					{
						screenDoiHinh.SyncWithNetworkData();
					}
					if (TutorialPopup.instance != null)
					{
						if (screenDoiHinh.ItemRoot != null)
						{
							UIPanel component = screenDoiHinh.ItemRoot.GetComponent<UIPanel>();
							component.transform.localPosition = new UnityEngine.Vector3(-250f, component.transform.localPosition.y, component.transform.localPosition.z);
							component.clipRange = new Vector4(250f, component.clipRange.y, component.clipRange.z, component.clipRange.w);
						}
						TutorialPopup.instance.ShowNextTutorial();
					}
				}
				if (GUIManager.instance != null && GUIManager.instance.homeCity != null && (bool)GUIManager.instance.homeCity.mainAvatar)
				{
					UserInfo.HeroData nvDaiDien = UserInfo.GetHeroFromDoiHinh(1);
					UserInfo.TrangBiData trangBiData = UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == nvDaiDien.VuKhiID);
					UserInfo.ThuCuoiData thuCuoiData = GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi && e.ExpiredTime > GameManager.instance.m_GameClient.ServerTime);
					string costume = string.Empty;
					UserInfo.CostumeData costumeData = null;
					if (UserInfo.CostumeList != null)
					{
						costumeData = UserInfo.CostumeList.Find((UserInfo.CostumeData e) => e.ID == nvDaiDien.CostumeID);
						costume = ((costumeData == null) ? string.Empty : costumeData.CodeName);
					}
					UserInfo.PetInfo petInfo = GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.curThanThu);
					GUIManager.instance.homeCity.mainAvatar.SetCodeName(nvDaiDien.Name, (trangBiData != null) ? trangBiData.Name : string.Empty, (thuCuoiData != null) ? thuCuoiData.CodeName : string.Empty, costume, (petInfo != null) ? petInfo.codename : string.Empty, (petInfo != null) ? petInfo.Quality : UserInfo.PetInfo.PetQuality.PHO_THONG);
				}
			}
			else if (setDoiHinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(setDoiHinhResponse.ErrorMessage);
				EGDebug.LogWarning(setDoiHinhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", setDoiHinhResponse.ErrorCode, setDoiHinhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetDoiHinhResponse : response null");
		}
		return true;
	}

	public void RequestLayDeTu(LayDeTuRequest request)
	{
		SendRequest(m_C2SProxy.RequestLayDeTu, JsonMapper.ToJson(request, false));
	}

	public bool OnLayDeTuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		LayDeTuResponse layDeTuResponse = JsonMapper.ToObject<LayDeTuResponse>(data);
		if (layDeTuResponse != null)
		{
			if (layDeTuResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (layDeTuResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(layDeTuResponse.UpdateInfo);
				}
				if (TutorialPopup.instance != null)
				{
					TutorialPopup.instance.ShowNextTutorial();
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenChoDeTu)
				{
					ScreenChoDeTu screenChoDeTu = GUIManager.getScreen(GAME_SCREEN.ScreenChoDeTu) as ScreenChoDeTu;
					screenChoDeTu.updateView(layDeTuResponse);
				}
			}
			else if (layDeTuResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(layDeTuResponse.ErrorMessage);
				EGDebug.LogWarning(layDeTuResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", layDeTuResponse.ErrorCode, layDeTuResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnLayDeTuResponse : response null");
		}
		return true;
	}

	public void RequestHoiTheLuc(HoiTheLucRequest request)
	{
		SendRequest(m_C2SProxy.RequestHoiTheLuc, JsonMapper.ToJson(request, false));
	}

	public bool OnHoiTheLucResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		HoiTheLucResponse hoiTheLucResponse = JsonMapper.ToObject<HoiTheLucResponse>(data);
		if (hoiTheLucResponse != null)
		{
			if (hoiTheLucResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (hoiTheLucResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(hoiTheLucResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKyNGoTheLuc)
				{
					ScreenKyNgo_TheLuc screenKyNgo_TheLuc = GUIManager.getScreen(GAME_SCREEN.ScreenKyNGoTheLuc) as ScreenKyNgo_TheLuc;
					screenKyNgo_TheLuc.updateMess();
					MessagePopup.Create(string.Format(Localization.instance.Get("ChuongMonDuocPhucHoiTheLucMess"), hoiTheLucResponse.SoTheLucNhanDuoc));
				}
				MessagePopup.Create(string.Format(Localization.instance.Get("SoTheLucNhanDuocLabel"), hoiTheLucResponse.SoTheLucNhanDuoc.ToString()));
			}
			else if (hoiTheLucResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(hoiTheLucResponse.ErrorMessage);
				EGDebug.LogWarning(hoiTheLucResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", hoiTheLucResponse.ErrorCode, hoiTheLucResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("HoiTheLuc : response null");
		}
		return true;
	}

	public void RequestOpenHop(OpenHopRequest request)
	{
		SendRequest(m_C2SProxy.RequestOpenHop, JsonMapper.ToJson(request, false));
	}

	public bool OnOpenHopResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTayNai)
				{
					ScreenTayNai screenTayNai = GUIManager.getScreen(GAME_SCREEN.ScreenTayNai) as ScreenTayNai;
					screenTayNai.m_Tab = ScreenTayNai.ScreenTayNaiTab.TabVatPham;
					screenTayNai.SyncWithNetworkData();
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenEventMoRuong)
				{
					GameManager.instance.m_GameClient.RequestGetDiemMoRuong();
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), phanThuongResponse);
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnOpenHopResponse : response null");
		}
		return true;
	}

	public void RequestBuyLeBao(BuyLeBaoRequest request)
	{
		SendRequest(m_C2SProxy.RequestBuyLeBao, JsonMapper.ToJson(request, false));
	}

	public bool OnBuyLeBaoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenChoLeBao)
				{
					ScreenChoLeBao screenChoLeBao = GUIManager.getScreen(GAME_SCREEN.ScreenChoLeBao) as ScreenChoLeBao;
					screenChoLeBao.updateView();
					screenChoLeBao.openPopUpPhanThuong(phanThuongResponse);
				}
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBuyLeBaoResponse : response null");
		}
		return true;
	}

	public void RequestBuyVatPham(BuyVatPhamRequest request)
	{
		SendRequest(m_C2SProxy.RequestBuyVatPham, JsonMapper.ToJson(request, false));
	}

	public bool OnBuyVatPhamResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		BuyVatPhamResponse buyVatPhamResponse = JsonMapper.ToObject<BuyVatPhamResponse>(data);
		if (buyVatPhamResponse != null)
		{
			if (buyVatPhamResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (buyVatPhamResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(buyVatPhamResponse.UpdateInfo);
				}
				MessagePopup.Create(Localization.instance.Get("MuaThanhCongMess"));
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenChoVatPham)
				{
					ScreenChoVatPham screenChoVatPham = GUIManager.getScreen(GAME_SCREEN.ScreenChoVatPham) as ScreenChoVatPham;
					screenChoVatPham.getListVatPham();
				}
			}
			else if (buyVatPhamResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(buyVatPhamResponse.ErrorMessage);
				EGDebug.LogWarning(buyVatPhamResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", buyVatPhamResponse.ErrorCode, buyVatPhamResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBuyVatPhamResponse : response null");
		}
		return true;
	}

	public void RequestSetDoiHinhHoTro(SetDoiHinhHoTroRequest request)
	{
		SendRequest(m_C2SProxy.RequestSetDoiHinhHoTro, JsonMapper.ToJson(request, false));
	}

	public bool OnSetDoiHinhHoTroResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SetDoiHinhHoTroResponse setDoiHinhHoTroResponse = JsonMapper.ToObject<SetDoiHinhHoTroResponse>(data);
		if (setDoiHinhHoTroResponse != null)
		{
			if (setDoiHinhHoTroResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (setDoiHinhHoTroResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(setDoiHinhHoTroResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
				{
					ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
					if (NGUITools.GetActive(screenDoiHinh.BatQuaiGroup.gameObject))
					{
						screenDoiHinh.updateNVSupport();
					}
				}
				if (PopupNhanVat.instance != null)
				{
					PopupNhanVat.instance.updateNhanVatBatQuaiView();
				}
			}
			else if (setDoiHinhHoTroResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(setDoiHinhHoTroResponse.ErrorMessage);
				EGDebug.LogWarning(setDoiHinhHoTroResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", setDoiHinhHoTroResponse.ErrorCode, setDoiHinhHoTroResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetDoiHinhResponse : response null");
		}
		return true;
	}

	public void RequestGetDuaTopLevelInfo()
	{
		SendRequest(m_C2SProxy.RequestGetDuaTopLevelInfo, string.Empty);
	}

	public bool OnGetDuaTopLevelInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DuaTopLevelResponse duaTopLevelResponse = JsonMapper.ToObject<DuaTopLevelResponse>(data);
		if (duaTopLevelResponse != null)
		{
			if (duaTopLevelResponse.ErrorCode == ERROR_CODE.OK)
			{
				ScreenTopEvent screenTopEvent = GUIManager.getScreen(GAME_SCREEN.ScreenTopEvent) as ScreenTopEvent;
				GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTopEvent);
				screenTopEvent.displayTopLevel(duaTopLevelResponse);
			}
			else if (duaTopLevelResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(duaTopLevelResponse.ErrorMessage);
				EGDebug.LogWarning(duaTopLevelResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", duaTopLevelResponse.ErrorCode, duaTopLevelResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetDuaTopLevelInfoResponse : response null");
		}
		return true;
	}

	public void RequestGetDuaTopLuanKiemInfo()
	{
		SendRequest(m_C2SProxy.RequestGetDuaTopLuanKiemInfo, string.Empty);
	}

	public bool OnGetDuaTopLuanKiemInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DuaTopLuanKiemResponse duaTopLuanKiemResponse = JsonMapper.ToObject<DuaTopLuanKiemResponse>(data);
		if (duaTopLuanKiemResponse != null)
		{
			if (duaTopLuanKiemResponse.ErrorCode == ERROR_CODE.OK)
			{
				ScreenTopEvent screenTopEvent = GUIManager.getScreen(GAME_SCREEN.ScreenTopEvent) as ScreenTopEvent;
				GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTopEvent);
				screenTopEvent.displayTopLuanKiem(duaTopLuanKiemResponse);
			}
			else if (duaTopLuanKiemResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(duaTopLuanKiemResponse.ErrorMessage);
				EGDebug.LogWarning(duaTopLuanKiemResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", duaTopLuanKiemResponse.ErrorCode, duaTopLuanKiemResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetDuaTopLuanKiemInfoResponse : response null");
		}
		return true;
	}

	public void RequestOpenDoiHinhHoTro(OpenDoiHinhHoTroRequest request)
	{
		SendRequest(m_C2SProxy.RequestOpenDoiHinhHoTro, JsonMapper.ToJson(request, false));
	}

	public bool OnOpenDoiHinhHoTroResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		OpenDoiHinhHoTroResponse openDoiHinhHoTroResponse = JsonMapper.ToObject<OpenDoiHinhHoTroResponse>(data);
		if (openDoiHinhHoTroResponse != null)
		{
			if (openDoiHinhHoTroResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (openDoiHinhHoTroResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(openDoiHinhHoTroResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
				{
					ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
					if (NGUITools.GetActive(screenDoiHinh.BatQuaiGroup.gameObject))
					{
						screenDoiHinh.getListBatQuaiHoTro();
					}
				}
			}
			else if (openDoiHinhHoTroResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(openDoiHinhHoTroResponse.ErrorMessage);
				EGDebug.LogWarning(openDoiHinhHoTroResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", openDoiHinhHoTroResponse.ErrorCode, openDoiHinhHoTroResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetDoiHinhResponse : response null");
		}
		return true;
	}

	public void RequestSetVoCong(int hero_id, int vc_id)
	{
		SetVoCongRequest setVoCongRequest = new SetVoCongRequest();
		setVoCongRequest.HeroID = hero_id;
		setVoCongRequest.VoCongID = vc_id;
		SendRequest(m_C2SProxy.RequestSetVoCong, JsonMapper.ToJson(setVoCongRequest, false));
	}

	public bool OnSetVoCongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SetVoCongResponse setVoCongResponse = JsonMapper.ToObject<SetVoCongResponse>(data);
		if (setVoCongResponse != null)
		{
			if (setVoCongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (setVoCongResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(setVoCongResponse.UpdateInfo);
				}
				if (PopupNhanVat.instance != null)
				{
					PopupNhanVat.instance.updateNhanVatBatQuaiView();
					if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
					{
						ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
						screenDoiHinh.displayInfoBatQuai(true);
					}
					return true;
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
				{
					ScreenDoiHinh screenDoiHinh2 = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
					screenDoiHinh2.SyncWithNetworkData();
				}
			}
			else if (setVoCongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(setVoCongResponse.ErrorMessage);
				EGDebug.LogWarning(setVoCongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", setVoCongResponse.ErrorCode, setVoCongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetDoiHinhResponse : response null");
		}
		return true;
	}

	public void RequestStartBoiDuong(int hero_id, int boi_duong_dan_id, StartBoiDuongRequest.LoaiBoiDuong loai)
	{
		StartBoiDuongRequest startBoiDuongRequest = new StartBoiDuongRequest();
		startBoiDuongRequest.HeroID = hero_id;
		startBoiDuongRequest.BoiDuongDanID = boi_duong_dan_id;
		startBoiDuongRequest.loai = loai;
		SendRequest(m_C2SProxy.RequestStartBoiDuong, JsonMapper.ToJson(startBoiDuongRequest, false));
	}

	public bool OnStartBoiDuongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		StartBoiDuongResponse startBoiDuongResponse = JsonMapper.ToObject<StartBoiDuongResponse>(data);
		if (startBoiDuongResponse != null)
		{
			if (startBoiDuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (startBoiDuongResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(startBoiDuongResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBoiDuong)
				{
					ScreenBoiDuong screenBoiDuong = GUIManager.getScreen(GAME_SCREEN.ScreenBoiDuong) as ScreenBoiDuong;
					screenBoiDuong.openScreenBoiDuongConfirm(startBoiDuongResponse);
				}
			}
			else if (startBoiDuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(startBoiDuongResponse.ErrorMessage);
				EGDebug.LogWarning(startBoiDuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", startBoiDuongResponse.ErrorCode, startBoiDuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetTrangBiResponse : response null");
		}
		return true;
	}

	public void RequestEndBoiDuong(int hero_id)
	{
		EndBoiDuongRequest endBoiDuongRequest = new EndBoiDuongRequest();
		endBoiDuongRequest.HeroID = hero_id;
		SendRequest(m_C2SProxy.RequestEndBoiDuong, JsonMapper.ToJson(endBoiDuongRequest, false));
	}

	public bool OnEndBoiDuongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		EndBoiDuongResponse endBoiDuongResponse = JsonMapper.ToObject<EndBoiDuongResponse>(data);
		if (endBoiDuongResponse != null)
		{
			if (endBoiDuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (endBoiDuongResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(endBoiDuongResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBoiDuongConfirm)
				{
					ScreenBoiDuongConfirm screenBoiDuongConfirm = GUIManager.getScreen(GAME_SCREEN.ScreenBoiDuongConfirm) as ScreenBoiDuongConfirm;
					screenBoiDuongConfirm.openScreenBoiDuong();
				}
			}
			else if (endBoiDuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(endBoiDuongResponse.ErrorMessage);
				EGDebug.LogWarning(endBoiDuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", endBoiDuongResponse.ErrorCode, endBoiDuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetTrangBiResponse : response null");
		}
		return true;
	}

	public void RequestBanTrangBi(BanTrangBiRequest request)
	{
		SendRequest(m_C2SProxy.RequestBanTrangBi, JsonMapper.ToJson(request, false));
	}

	public bool OnBanTrangBiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		BanTrangBiResponse banTrangBiResponse = JsonMapper.ToObject<BanTrangBiResponse>(data);
		if (banTrangBiResponse != null)
		{
			if (banTrangBiResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (banTrangBiResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(banTrangBiResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTrangBi)
				{
					ScreenTrangBi screenTrangBi = GUIManager.getScreen(GAME_SCREEN.ScreenTrangBi) as ScreenTrangBi;
					screenTrangBi.SyncWithNetworkData();
					MessagePopup.Create(string.Format(Localization.instance.Get("BanTrangBiThanhCongMess"), banTrangBiResponse.BacNhanDuoc));
				}
			}
			else if (banTrangBiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(banTrangBiResponse.ErrorMessage);
				EGDebug.LogWarning(banTrangBiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", banTrangBiResponse.ErrorCode, banTrangBiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetTrangBiResponse : response null");
		}
		return true;
	}

	public void RequestCuongHoaTrangBi(CuongHoaTrangBiRequest request)
	{
		SendRequest(m_C2SProxy.RequestCuongHoaTrangBi, JsonMapper.ToJson(request, false));
	}

	public bool OnCuongHoaTrangBiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		CuongHoaTrangBiResponse cuongHoaTrangBiResponse = JsonMapper.ToObject<CuongHoaTrangBiResponse>(data);
		if (cuongHoaTrangBiResponse != null)
		{
			if (cuongHoaTrangBiResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (cuongHoaTrangBiResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(cuongHoaTrangBiResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenCuongHoa)
				{
					ScreenCuongHoa screenCuongHoa = GUIManager.getScreen(GAME_SCREEN.ScreenCuongHoa) as ScreenCuongHoa;
					screenCuongHoa.updateInfo(cuongHoaTrangBiResponse.TrangBiID, cuongHoaTrangBiResponse.ThayDoiLevel);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
				{
					ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
					screenDoiHinh.SetNhanVatInfo();
				}
			}
			else if (cuongHoaTrangBiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(cuongHoaTrangBiResponse.ErrorMessage);
				EGDebug.LogWarning(cuongHoaTrangBiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", cuongHoaTrangBiResponse.ErrorCode, cuongHoaTrangBiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetTrangBiResponse : response null");
		}
		return true;
	}

	public void RequestGhepManhTrangBi(GhepManhTrangBiRequest request)
	{
		SendRequest(m_C2SProxy.RequestGhepManhTrangBi, JsonMapper.ToJson(request, false));
	}

	public bool OnGhepManhTrangBiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GhepManhTrangBiResponse ghepManhTrangBiResponse = JsonMapper.ToObject<GhepManhTrangBiResponse>(data);
		if (ghepManhTrangBiResponse != null)
		{
			if (ghepManhTrangBiResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (ghepManhTrangBiResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(ghepManhTrangBiResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTayNai)
				{
					ScreenTayNai screenTayNai = GUIManager.getScreen(GAME_SCREEN.ScreenTayNai) as ScreenTayNai;
					screenTayNai.SyncWithNetworkData();
					screenTayNai.startPlayAnim(ghepManhTrangBiResponse.TrangBiName);
				}
			}
			else if (ghepManhTrangBiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(ghepManhTrangBiResponse.ErrorMessage);
				EGDebug.LogWarning(ghepManhTrangBiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", ghepManhTrangBiResponse.ErrorCode, ghepManhTrangBiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGhepManhTrangBiResponse : response null");
		}
		return true;
	}

	public void RequestGhepManhVoCong(GhepManhVoCongRequest request)
	{
		SendRequest(m_C2SProxy.RequestGhepManhVoCong, JsonMapper.ToJson(request, false));
	}

	public bool OnGhepManhVoCongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GhepManhVoCongResponse ghepManhVoCongResponse = JsonMapper.ToObject<GhepManhVoCongResponse>(data);
		if (ghepManhVoCongResponse != null)
		{
			if (ghepManhVoCongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (UserInfo.DanhHieu != null && ghepManhVoCongResponse.UpdateInfo.DanhHieu != null && UserInfo.DanhHieu.TuyTamVoHoc < ghepManhVoCongResponse.UpdateInfo.DanhHieu.TuyTamVoHoc)
				{
					PopupDuocThanhTuu popupDuocThanhTuu = PopupDuocThanhTuu.CreateTuyTamVoHoc(ghepManhVoCongResponse.UpdateInfo.DanhHieu);
				}
				if (ghepManhVoCongResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(ghepManhVoCongResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTayNai)
				{
					ScreenTayNai screenTayNai = GUIManager.getScreen(GAME_SCREEN.ScreenTayNai) as ScreenTayNai;
					screenTayNai.SyncWithNetworkData();
					screenTayNai.startPlayAnim(ghepManhVoCongResponse.TrangBiName);
				}
			}
			else if (ghepManhVoCongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(ghepManhVoCongResponse.ErrorMessage);
				EGDebug.LogWarning(ghepManhVoCongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", ghepManhVoCongResponse.ErrorCode, ghepManhVoCongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGhepManhVoCongResponse : response null");
		}
		return true;
	}

	public void RequestThamNgoVoCong(ThamNgoVoCongRequest request)
	{
		SendRequest(m_C2SProxy.RequestThamNgoVoCong, JsonMapper.ToJson(request, false));
	}

	public bool OnThamNgoVoCongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThamNgoVoCongResponse thamNgoVoCongResponse = JsonMapper.ToObject<ThamNgoVoCongResponse>(data);
		if (thamNgoVoCongResponse != null)
		{
			if (thamNgoVoCongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thamNgoVoCongResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(thamNgoVoCongResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenThamNgo)
				{
					ScreenThamNgo screenThamNgo = GUIManager.getScreen(GAME_SCREEN.ScreenThamNgo) as ScreenThamNgo;
					screenThamNgo.updateScreenView(thamNgoVoCongResponse);
				}
			}
			else if (thamNgoVoCongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thamNgoVoCongResponse.ErrorMessage);
				EGDebug.LogWarning(thamNgoVoCongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thamNgoVoCongResponse.ErrorCode, thamNgoVoCongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnThamNgoVoCongResponse : response null");
		}
		return true;
	}

	public void RequestTinhLuyenVoCong(TinhLuyenVoCongRequest request)
	{
		SendRequest(m_C2SProxy.RequestTinhLuyenVoCong, JsonMapper.ToJson(request, false));
	}

	public bool OnTinhLuyenVoCongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		TinhLuyenVoCongResponse tinhLuyenVoCongResponse = JsonMapper.ToObject<TinhLuyenVoCongResponse>(data);
		if (tinhLuyenVoCongResponse != null)
		{
			if (tinhLuyenVoCongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (tinhLuyenVoCongResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(tinhLuyenVoCongResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDotPha)
				{
					ScreenDotPha screenDotPha = GUIManager.getScreen(GAME_SCREEN.ScreenDotPha) as ScreenDotPha;
					screenDotPha.updateInfo();
				}
			}
			else if (tinhLuyenVoCongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(tinhLuyenVoCongResponse.ErrorMessage);
				EGDebug.LogWarning(tinhLuyenVoCongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", tinhLuyenVoCongResponse.ErrorCode, tinhLuyenVoCongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnTinhLuyenVoCongResponse : response null");
		}
		return true;
	}

	public void RequestTinhLuyenTrangBi(TinhLuyenTrangBiRequest request)
	{
		SendRequest(m_C2SProxy.RequestTinhLuyenTrangBi, JsonMapper.ToJson(request, false));
	}

	public bool OnTinhLuyenTrangBiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		TinhLuyenTrangBiResponse tinhLuyenTrangBiResponse = JsonMapper.ToObject<TinhLuyenTrangBiResponse>(data);
		if (tinhLuyenTrangBiResponse != null)
		{
			if (tinhLuyenTrangBiResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (tinhLuyenTrangBiResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(tinhLuyenTrangBiResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTinhLuyen)
				{
					ScreenTinhLuyen screenTinhLuyen = GUIManager.getScreen(GAME_SCREEN.ScreenTinhLuyen) as ScreenTinhLuyen;
					screenTinhLuyen.startPlayAnim();
					screenTinhLuyen.updateInfo(tinhLuyenTrangBiResponse);
				}
			}
			else if (tinhLuyenTrangBiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(tinhLuyenTrangBiResponse.ErrorMessage);
				EGDebug.LogWarning(tinhLuyenTrangBiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", tinhLuyenTrangBiResponse.ErrorCode, tinhLuyenTrangBiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetTrangBiResponse : response null");
		}
		return true;
	}

	public void RequestSetTrangBi(int hero_id, int tb_id)
	{
		SetTrangBiRequest setTrangBiRequest = new SetTrangBiRequest();
		setTrangBiRequest.HeroID = hero_id;
		setTrangBiRequest.TrangBiID = tb_id;
		SendRequest(m_C2SProxy.RequestSetTrangBi, JsonMapper.ToJson(setTrangBiRequest, false));
	}

	public bool OnSetTrangBiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SetTrangBiResponse setTrangBiResponse = JsonMapper.ToObject<SetTrangBiResponse>(data);
		if (setTrangBiResponse != null)
		{
			if (setTrangBiResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (setTrangBiResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(setTrangBiResponse.UpdateInfo);
				}
				if (TutorialPopup.instance != null)
				{
					TutorialPopup.instance.ShowNextTutorial();
				}
				if (GUIManager.instance != null && GUIManager.instance.homeCity != null && (bool)GUIManager.instance.homeCity.mainAvatar)
				{
					UserInfo.HeroData nvDaiDien = UserInfo.GetHeroFromDoiHinh(1);
					UserInfo.TrangBiData trangBiData = UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == nvDaiDien.VuKhiID);
					UserInfo.ThuCuoiData thuCuoiData = GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi && e.ExpiredTime > GameManager.instance.m_GameClient.ServerTime);
					string costume = string.Empty;
					UserInfo.CostumeData costumeData = null;
					if (UserInfo.CostumeList != null)
					{
						costumeData = UserInfo.CostumeList.Find((UserInfo.CostumeData e) => e.ID == nvDaiDien.CostumeID);
						costume = ((costumeData == null) ? string.Empty : costumeData.CodeName);
					}
					GUIManager.instance.homeCity.mainAvatar.SetCodeName(nvDaiDien.Name, (trangBiData != null) ? trangBiData.Name : string.Empty, (thuCuoiData != null) ? thuCuoiData.CodeName : string.Empty, costume, string.Empty);
				}
				if (PopupNhanVat.instance != null)
				{
					PopupNhanVat.instance.updateNhanVatBatQuaiView();
					if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
					{
						ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
						screenDoiHinh.displayInfoBatQuai(true);
					}
					return true;
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
				{
					ScreenDoiHinh screenDoiHinh2 = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
					screenDoiHinh2.SyncWithNetworkData();
				}
			}
			else if (setTrangBiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(setTrangBiResponse.ErrorMessage);
				EGDebug.LogWarning(setTrangBiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", setTrangBiResponse.ErrorCode, setTrangBiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetTrangBiResponse : response null");
		}
		return true;
	}

	public void RequestSetDoiHinhAndTranHinh(SetDoiHinhAndTranHinhRequest request)
	{
		SendRequest(m_C2SProxy.RequestSetDoiHinhAndTranHinh, JsonMapper.ToJson(request, false));
	}

	public bool OnSetDoiHinhAndTranHinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SetDoiHinhAndTranHinhResponse setDoiHinhAndTranHinhResponse = JsonMapper.ToObject<SetDoiHinhAndTranHinhResponse>(data);
		if (setDoiHinhAndTranHinhResponse != null)
		{
			if (setDoiHinhAndTranHinhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (setDoiHinhAndTranHinhResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(setDoiHinhAndTranHinhResponse.UpdateInfo);
				}
				if (PopupTranHinh.instance != null)
				{
					PopupTranHinh.instance.SyncWithNetworkData();
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
				{
					ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
					screenDoiHinh.SyncWithNetworkData();
				}
				if (GUIManager.instance != null && GUIManager.instance.homeCity != null && (bool)GUIManager.instance.homeCity.mainAvatar)
				{
					UserInfo.HeroData nvDaiDien = UserInfo.GetHeroFromDoiHinh(1);
					UserInfo.TrangBiData trangBiData = UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == nvDaiDien.VuKhiID);
					UserInfo.ThuCuoiData thuCuoiData = GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi && e.ExpiredTime > GameManager.instance.m_GameClient.ServerTime);
					string costume = string.Empty;
					UserInfo.CostumeData costumeData = null;
					if (UserInfo.CostumeList != null)
					{
						costumeData = UserInfo.CostumeList.Find((UserInfo.CostumeData e) => e.ID == nvDaiDien.CostumeID);
						costume = ((costumeData == null) ? string.Empty : costumeData.CodeName);
					}
					UserInfo.PetInfo petInfo = GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.curThanThu);
					GUIManager.instance.homeCity.mainAvatar.SetCodeName(nvDaiDien.Name, (trangBiData != null) ? trangBiData.Name : string.Empty, (thuCuoiData != null) ? thuCuoiData.CodeName : string.Empty, costume, (petInfo != null) ? petInfo.codename : string.Empty, (petInfo != null) ? petInfo.Quality : UserInfo.PetInfo.PetQuality.PHO_THONG);
				}
			}
			else if (setDoiHinhAndTranHinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(setDoiHinhAndTranHinhResponse.ErrorMessage);
				EGDebug.LogWarning(setDoiHinhAndTranHinhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", setDoiHinhAndTranHinhResponse.ErrorCode, setDoiHinhAndTranHinhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetTrangBiResponse : response null");
		}
		return true;
	}

	public void RequestSearchBanBe(SearchBanBeRequest request)
	{
		SendRequest(m_C2SProxy.RequestSearchBanBe, JsonMapper.ToJson(request, false));
	}

	public bool OnSearchBanBeResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SearchBanBeResponse searchBanBeResponse = JsonMapper.ToObject<SearchBanBeResponse>(data);
		if (searchBanBeResponse != null)
		{
			if (searchBanBeResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenSearchBanBe)
				{
					ScreenSearchBanBe screenSearchBanBe = GUIManager.getScreen(GAME_SCREEN.ScreenSearchBanBe) as ScreenSearchBanBe;
					screenSearchBanBe.displayListGamer(searchBanBeResponse.ListData);
				}
			}
			else if (searchBanBeResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(searchBanBeResponse.ErrorMessage);
				EGDebug.LogWarning(searchBanBeResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", searchBanBeResponse.ErrorCode, searchBanBeResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSearchBanBeResponse : response null");
		}
		return true;
	}

	public void RequestAddBanBe(OpBanBeRequest request)
	{
		SendRequest(m_C2SProxy.RequestAddBanBe, JsonMapper.ToJson(request, false));
	}

	public bool OnAddBanBeResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		OpBanBeResponse opBanBeResponse = JsonMapper.ToObject<OpBanBeResponse>(data);
		if (opBanBeResponse != null)
		{
			if (opBanBeResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (opBanBeResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(opBanBeResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBanBe)
				{
					ScreenBanBe screenBanBe = GUIManager.getScreen(GAME_SCREEN.ScreenBanBe) as ScreenBanBe;
					screenBanBe.getListFriends();
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenSearchBanBe)
				{
					MessagePopup.Create(Localization.instance.Get("RequestKetBanMess"));
				}
			}
			else if (opBanBeResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(opBanBeResponse.ErrorMessage);
				EGDebug.LogWarning(opBanBeResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", opBanBeResponse.ErrorCode, opBanBeResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnAddBanBeResponse : response null");
		}
		return true;
	}

	public void RequestAcceptBanBe(OpBanBeRequest request)
	{
		SendRequest(m_C2SProxy.RequestAcceptBanBe, JsonMapper.ToJson(request, false));
	}

	public bool OnAcceptBanBeResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		OpBanBeResponse opBanBeResponse = JsonMapper.ToObject<OpBanBeResponse>(data);
		if (opBanBeResponse != null)
		{
			if (opBanBeResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (opBanBeResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(opBanBeResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBanBe)
				{
					ScreenBanBe screenBanBe = GUIManager.getScreen(GAME_SCREEN.ScreenBanBe) as ScreenBanBe;
					screenBanBe.getListFriends();
				}
			}
			else if (opBanBeResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(opBanBeResponse.ErrorMessage);
				EGDebug.LogWarning(opBanBeResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", opBanBeResponse.ErrorCode, opBanBeResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnAcceptBanBeResponse : response null");
		}
		return true;
	}

	public void RequestDeleteBanBe(OpBanBeRequest request)
	{
		SendRequest(m_C2SProxy.RequestDeleteBanBe, JsonMapper.ToJson(request, false));
	}

	public bool OnDeleteBanBeResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		OpBanBeResponse opBanBeResponse = JsonMapper.ToObject<OpBanBeResponse>(data);
		if (opBanBeResponse != null)
		{
			if (opBanBeResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (opBanBeResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(opBanBeResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBanBe)
				{
					ScreenBanBe screenBanBe = GUIManager.getScreen(GAME_SCREEN.ScreenBanBe) as ScreenBanBe;
					screenBanBe.getListFriends();
				}
			}
			else if (opBanBeResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(opBanBeResponse.ErrorMessage);
				EGDebug.LogWarning(opBanBeResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", opBanBeResponse.ErrorCode, opBanBeResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnDeleteBanBeResponse : response null");
		}
		return true;
	}

	public void RequestBanBeCuuThuInfo()
	{
		SendRequest(m_C2SProxy.RequestBanBeCuuThuInfo, string.Empty);
	}

	public bool OnBanBeCuuThuInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		OpBanBeResponse opBanBeResponse = JsonMapper.ToObject<OpBanBeResponse>(data);
		if (opBanBeResponse != null)
		{
			if (opBanBeResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (opBanBeResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(opBanBeResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenMain)
				{
					GUIManager.setScreen(GAME_SCREEN.ScreenBanBe);
				}
			}
			else if (opBanBeResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(opBanBeResponse.ErrorMessage);
				EGDebug.LogWarning(opBanBeResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", opBanBeResponse.ErrorCode, opBanBeResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBanBeCuuThuInfoResponse : response null");
		}
		return true;
	}

	public void RequestULinhInfo()
	{
		SendRequest(m_C2SProxy.RequestULinhInfo, string.Empty);
	}

	public bool OnULinhInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ULinhInfoResponse uLinhInfoResponse = JsonMapper.ToObject<ULinhInfoResponse>(data);
		if (uLinhInfoResponse != null)
		{
			if (uLinhInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				GUIManager.instance.SetScreen(GAME_SCREEN.ScreenULinhSonTrang);
				ScreenULinhSonTrang screenULinhSonTrang = GUIManager.getScreen(GAME_SCREEN.ScreenULinhSonTrang) as ScreenULinhSonTrang;
				screenULinhSonTrang.displayListULinhSonTrang(uLinhInfoResponse);
			}
			else if (uLinhInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(uLinhInfoResponse.ErrorMessage);
				EGDebug.LogWarning(uLinhInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", uLinhInfoResponse.ErrorCode, uLinhInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnULinhInfoResponse : response null");
		}
		return true;
	}

	public void RequestDoiItemULinh(DoiItemULinhRequest request)
	{
		SendRequest(m_C2SProxy.RequestDoiItemULinh, JsonMapper.ToJson(request, false));
	}

	public bool OnDoiItemULinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DoiItemULinhResponse doiItemULinhResponse = JsonMapper.ToObject<DoiItemULinhResponse>(data);
		if (doiItemULinhResponse != null)
		{
			if (doiItemULinhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (doiItemULinhResponse.PhanThuongResponse != null && doiItemULinhResponse.PhanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(doiItemULinhResponse.PhanThuongResponse.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenULinhSonTrang)
				{
					ScreenULinhSonTrang screenULinhSonTrang = GUIManager.getScreen(GAME_SCREEN.ScreenULinhSonTrang) as ScreenULinhSonTrang;
					screenULinhSonTrang.updateInfoList(doiItemULinhResponse);
				}
			}
			else if (doiItemULinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(doiItemULinhResponse.ErrorMessage);
				EGDebug.LogWarning(doiItemULinhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", doiItemULinhResponse.ErrorCode, doiItemULinhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnDoiItemULinhResponse : response null");
		}
		return true;
	}

	public void RequestKnbRefreshULinh()
	{
		SendRequest(m_C2SProxy.RequestKnbRefreshULinh, string.Empty);
	}

	public bool OnKnbRefreshULinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		KnbRefreshULinhResponse knbRefreshULinhResponse = JsonMapper.ToObject<KnbRefreshULinhResponse>(data);
		if (knbRefreshULinhResponse != null)
		{
			if (knbRefreshULinhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (knbRefreshULinhResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(knbRefreshULinhResponse.UpdateUserInfo);
				}
				if (knbRefreshULinhResponse.UpdateULinhInfo != null && GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenULinhSonTrang)
				{
					ScreenULinhSonTrang screenULinhSonTrang = GUIManager.getScreen(GAME_SCREEN.ScreenULinhSonTrang) as ScreenULinhSonTrang;
					screenULinhSonTrang.displayListULinhSonTrang(knbRefreshULinhResponse.UpdateULinhInfo);
				}
			}
			else if (knbRefreshULinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(knbRefreshULinhResponse.ErrorMessage);
				EGDebug.LogWarning(knbRefreshULinhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", knbRefreshULinhResponse.ErrorCode, knbRefreshULinhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnKnbRefreshULinhResponse : response null");
		}
		return true;
	}

	public void RequestDoiThuongULinh(DoiThuongULinhRequest request)
	{
		SendRequest(m_C2SProxy.RequestDoiThuongULinh, JsonMapper.ToJson(request, false));
	}

	public bool OnDoiThuongULinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DoiThuongULinhResponse doiThuongULinhResponse = JsonMapper.ToObject<DoiThuongULinhResponse>(data);
		if (doiThuongULinhResponse != null)
		{
			if (doiThuongULinhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (doiThuongULinhResponse.PhanThuongResponse != null && doiThuongULinhResponse.PhanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(doiThuongULinhResponse.PhanThuongResponse.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiThuong)
				{
					ScreenDoiThuong screenDoiThuong = GUIManager.getScreen(GAME_SCREEN.ScreenDoiThuong) as ScreenDoiThuong;
					screenDoiThuong.updateInfoList(doiThuongULinhResponse);
				}
			}
			else if (doiThuongULinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(doiThuongULinhResponse.ErrorMessage);
				EGDebug.LogWarning(doiThuongULinhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", doiThuongULinhResponse.ErrorCode, doiThuongULinhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnDoiThuongULinhResponse : response null");
		}
		return true;
	}

	public void RequestGetOtherPlayer()
	{
		GetListOthersRequest getListOthersRequest = new GetListOthersRequest();
		getListOthersRequest.Level = UserInfo.Gamer.Level;
		SendRequest(C2SProxy.RequestGetListOtherPlayer, JsonMapper.ToJson(getListOthersRequest, false), false);
	}

	public void RequestGetTopULinh()
	{
		SendRequest(m_C2SProxy.RequestGetTopULinh, string.Empty);
	}

	public bool OnGetTopULinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetTopULinhResponse getTopULinhResponse = JsonMapper.ToObject<GetTopULinhResponse>(data);
		if (getTopULinhResponse != null)
		{
			if (getTopULinhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiThuong)
				{
					ScreenDoiThuong screenDoiThuong = GUIManager.getScreen(GAME_SCREEN.ScreenDoiThuong) as ScreenDoiThuong;
					screenDoiThuong.SyncWithNetworkData(getTopULinhResponse);
				}
			}
			else if (getTopULinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getTopULinhResponse.ErrorMessage);
				EGDebug.LogWarning(getTopULinhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getTopULinhResponse.ErrorCode, getTopULinhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetTopULinhResponse : response null");
		}
		return true;
	}

	public void RequestDatTenMonPhai(DatTenMonPhaiRequest request)
	{
		SendRequest(m_C2SProxy.RequestDatTenMonPhai, JsonMapper.ToJson(request, false));
	}

	public bool OnDatTenMonPhaiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				try
				{
					PopupDatTenMonPhai.DestroyPopup();
				}
				catch (Exception)
				{
				}
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				try
				{
					SohaSDKManager.instance.SetUserInfo(m_UserInfo.ServerInfo.ID.ToString(), m_UserInfo.ServerInfo.Name, GameManager.instance.m_userName, (!string.IsNullOrEmpty(m_UserInfo.Gamer.DisplayName)) ? m_UserInfo.Gamer.DisplayName : "unnamed", m_UserInfo.Gamer.Level.ToString());
				}
				catch (Exception)
				{
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTayNai)
				{
					ScreenTayNai screenTayNai = (ScreenTayNai)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenTayNai);
					if (screenTayNai != null)
					{
						try
						{
							screenTayNai.SyncWithNetworkData();
						}
						catch (Exception)
						{
						}
					}
				}
				PopupDatTenMonPhai.DestroyPopup();
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetTopULinhResponse : response null");
		}
		return true;
	}

	public void RequestDoiDo(DoiDoRequest request)
	{
		SendRequest(m_C2SProxy.RequestDoiDo, JsonMapper.ToJson(request, false));
	}

	public bool OnDoiDoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKyNgoDoiDo)
				{
					ScreenKyNgoDoiDo screenKyNgoDoiDo = (ScreenKyNgoDoiDo)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenKyNgoDoiDo);
					screenKyNgoDoiDo.getListDoiDo();
					screenKyNgoDoiDo.updateMainMenuView();
					PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), phanThuongResponse);
				}
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetTopULinhResponse : response null");
		}
		return true;
	}

	public void RequestUseRuongThanBi(UseRuongThanRequest request)
	{
		SendRequest(m_C2SProxy.RequestUseRuongThanBi, JsonMapper.ToJson(request, false));
	}

	public void RequestUseRuongThan(UseRuongThanRequest request)
	{
		SendRequest(m_C2SProxy.RequestUseRuongThan, JsonMapper.ToJson(request, false));
	}

	public bool OnUseRuongThanResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				PopupSelectPhanThuong.Release();
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTayNai)
				{
					ScreenTayNai screenTayNai = (ScreenTayNai)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenTayNai);
					screenTayNai.SyncWithNetworkData();
				}
				if (PopupRuongThanBiDetail.instance != null)
				{
					PopupRuongThanBiDetail.DestroyPopup();
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), phanThuongResponse);
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnKichHoatGiftCodeResponse : response null");
		}
		return true;
	}

	public void RequestUseTuiThan(UseTuiThanRequest request)
	{
		SendRequest(m_C2SProxy.RequestUseTuiThan, JsonMapper.ToJson(request, false));
	}

	public bool OnUseTuiThanResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				PopupSelectPhanThuong.Release();
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTayNai)
				{
					ScreenTayNai screenTayNai = (ScreenTayNai)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenTayNai);
					screenTayNai.SyncWithNetworkData();
				}
				if (PopupRuongThanBiDetail.instance != null)
				{
					PopupRuongThanBiDetail.DestroyPopup();
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), phanThuongResponse);
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnKichHoatGiftCodeResponse : response null");
		}
		return true;
	}

	public void RequestKichHoatGiftCode(KichHoatGiftCodeRequest request)
	{
		SendRequest(m_C2SProxy.RequestKichHoatGiftCode, JsonMapper.ToJson(request, false));
	}

	public bool OnKichHoatGiftCodeResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				if (PopUpGiftCode.instance != null)
				{
					PopUpGiftCode.DestroyPopup();
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), phanThuongResponse);
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnKichHoatGiftCodeResponse : response null");
		}
		return true;
	}

	public void RequestUseCustomItem(UseCustomItemRequest request)
	{
		SendRequest(m_C2SProxy.RequestUseCustomItem, JsonMapper.ToJson(request, false));
	}

	public bool OnUseCustomItemResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UseCustomItemResponse useCustomItemResponse = JsonMapper.ToObject<UseCustomItemResponse>(data);
		if (useCustomItemResponse != null)
		{
			if (useCustomItemResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (useCustomItemResponse.PhanThuong.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(useCustomItemResponse.PhanThuong.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTayNai)
				{
					ScreenTayNai screenTayNai = GUIManager.getScreen(GAME_SCREEN.ScreenTayNai) as ScreenTayNai;
					screenTayNai.m_Tab = ScreenTayNai.ScreenTayNaiTab.TabVatPham;
					screenTayNai.SyncWithNetworkData();
					screenTayNai.displayMessResponse(useCustomItemResponse);
				}
				if (useCustomItemResponse.VatPhamName == "VP_LUAN_KIEM_LENH" && GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenLuanKiem)
				{
					ScreenLuanKiem screenLuanKiem = GUIManager.getScreen(GAME_SCREEN.ScreenLuanKiem) as ScreenLuanKiem;
					screenLuanKiem.SyncWithUserInfo(UserInfo.LuanKiem, ConfigManager.GetLuotLuanKiemByVip(UserInfo.Gamer.Vip));
				}
				else if (useCustomItemResponse.VatPhamName == "VP_GA_QUAY")
				{
					if (GiangHoPopup.instance != null)
					{
						GiangHoCfg cfg;
						UserInfo.GiangHoData giangHo;
						if (GiangHoPopup.instance.isGHTinhAnh)
						{
							cfg = ConfigManager.instance.m_listGiangHoTinhAnh[GiangHoPopup.instance.giangHoIdx];
							giangHo = UserInfo.GetGiangHoTinhAnhByIdx(GiangHoPopup.instance.giangHoIdx);
						}
						else
						{
							cfg = ConfigManager.instance.m_listGiangHo[GiangHoPopup.instance.giangHoIdx];
							giangHo = UserInfo.GetGiangHoByIdx(GiangHoPopup.instance.giangHoIdx);
						}
						GiangHoPopup.instance.SyncWithNetworkData(giangHo, cfg);
					}
				}
				else if (useCustomItemResponse.VatPhamName == "VP_CUSTOM_TU_HON_DAN")
				{
					GUIManager.instance.gadgetPanelTop.SetInfo(GameManager.instance.m_GameClient.UserInfo);
				}
			}
			else if (useCustomItemResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(useCustomItemResponse.ErrorMessage);
				EGDebug.LogWarning(useCustomItemResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", useCustomItemResponse.ErrorCode, useCustomItemResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnUseCustomItemResponse : response null");
		}
		return true;
	}

	public void RequestBuyVatPhamAndUse(BuyVatPhamAndUseRequest request)
	{
		SendRequest(m_C2SProxy.RequestBuyVatPhamAndUse, JsonMapper.ToJson(request, false));
	}

	public bool OnBuyVatPhamAndUseResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		BuyVatPhamAndUseResponse buyVatPhamAndUseResponse = JsonMapper.ToObject<BuyVatPhamAndUseResponse>(data);
		if (buyVatPhamAndUseResponse != null)
		{
			if (buyVatPhamAndUseResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (buyVatPhamAndUseResponse.PhanThuong.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(buyVatPhamAndUseResponse.PhanThuong.UpdateUserInfo);
				}
				if (buyVatPhamAndUseResponse.VatPhamName == "VP_LUAN_KIEM_LENH" && GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenLuanKiem)
				{
					ScreenLuanKiem screenLuanKiem = GUIManager.getScreen(GAME_SCREEN.ScreenLuanKiem) as ScreenLuanKiem;
					screenLuanKiem.SyncWithUserInfo(UserInfo.LuanKiem, ConfigManager.GetLuotLuanKiemByVip(UserInfo.Gamer.Vip));
				}
				else if (buyVatPhamAndUseResponse.VatPhamName == "VP_GA_QUAY" && GiangHoPopup.instance != null)
				{
					int giangHoIdx = GiangHoPopup.instance.giangHoIdx;
					GiangHoCfg giangHoCfg;
					UserInfo.GiangHoData giangHoData;
					if (GiangHoPopup.instance.isGHTinhAnh)
					{
						giangHoCfg = ((giangHoIdx >= ConfigManager.instance.m_listGiangHoTinhAnh.Count) ? null : ConfigManager.instance.m_listGiangHoTinhAnh[giangHoIdx]);
						giangHoData = ((giangHoIdx >= UserInfo.GiangHoTinhAnh.Count) ? null : UserInfo.GiangHoTinhAnh[giangHoIdx]);
					}
					else
					{
						giangHoCfg = ((giangHoIdx >= ConfigManager.instance.m_listGiangHo.Count) ? null : ConfigManager.instance.m_listGiangHo[giangHoIdx]);
						giangHoData = ((giangHoIdx >= UserInfo.GiangHo.Count) ? null : UserInfo.GiangHo[giangHoIdx]);
					}
					if (giangHoData != null && giangHoCfg != null)
					{
						GiangHoPopup.instance.SyncWithNetworkData(giangHoData, giangHoCfg);
					}
				}
			}
			else if (buyVatPhamAndUseResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(buyVatPhamAndUseResponse.ErrorMessage);
				EGDebug.LogWarning(buyVatPhamAndUseResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", buyVatPhamAndUseResponse.ErrorCode, buyVatPhamAndUseResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBuyVatPhamAndUseResponse : response null");
		}
		return true;
	}

	public void RequestXemThongTinMonPhai(XemThongTinMonPhaiRequest request)
	{
		SendRequest(m_C2SProxy.RequestXemThongTinMonPhai, JsonMapper.ToJson(request, false));
	}

	public bool OnXemThongTinMonPhaiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UserInfo userInfo = JsonMapper.ToObject<UserInfo>(data);
		if (userInfo != null)
		{
			if (userInfo.ErrorCode == ERROR_CODE.OK)
			{
				ScreenDoiHinh screenDoiHinh = (ScreenDoiHinh)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenDoiHinh);
				screenDoiHinh.ShowAnotherUserInfo(userInfo);
			}
			else if (userInfo.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(userInfo.ErrorMessage);
				EGDebug.LogWarning(userInfo.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", userInfo.ErrorCode, userInfo.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnXemThongTinMonPhaiResponse : response null");
		}
		return true;
	}

	public void RequestUseMailPhanThuong(UseMailPhanThuongRequest request)
	{
		SendRequest(m_C2SProxy.RequestUseMailPhanThuong, JsonMapper.ToJson(request, false));
	}

	public bool OnUseMailPhanThuongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenMail)
				{
					ScreenMail screenMail = GUIManager.getScreen(GAME_SCREEN.ScreenMail) as ScreenMail;
					screenMail.openPopUpDSPhanThuong(phanThuongResponse);
				}
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnUseMailPhanThuongResponse : response null");
		}
		return true;
	}

	public void RequestSendMail(SendMailRequest request)
	{
		SendRequest(m_C2SProxy.RequestSendMail, JsonMapper.ToJson(request, false));
	}

	public bool OnSendMailResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SendMailResponse sendMailResponse = JsonMapper.ToObject<SendMailResponse>(data);
		if (sendMailResponse != null)
		{
			if (sendMailResponse.ErrorCode == ERROR_CODE.OK)
			{
				MessagePopup.Create(Localization.instance.Get("SendMailSuccessMess"));
			}
			else if (sendMailResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(sendMailResponse.ErrorMessage);
				EGDebug.LogWarning(sendMailResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", sendMailResponse.ErrorCode, sendMailResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSendMailResponse : response null");
		}
		return true;
	}

	public void RequestRefreshMail(bool showLoading)
	{
		SendRequest(m_C2SProxy.RequestRefreshMail, string.Empty, showLoading);
	}

	public bool OnRefreshMailResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UserInfo userInfo = JsonMapper.ToObject<UserInfo>(data);
		if (userInfo != null)
		{
			if (userInfo.ErrorCode == ERROR_CODE.OK)
			{
				UserInfo.UpdateInfo(userInfo);
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenMail)
				{
					ScreenMail screenMail = GUIManager.getScreen(GAME_SCREEN.ScreenMail) as ScreenMail;
					screenMail.SyncWithNetworkData(true, true);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenMain)
				{
					ScreenMain screenMain = GUIManager.getScreen(GAME_SCREEN.ScreenMain) as ScreenMain;
					screenMain.getInfoNewMail();
				}
			}
			else if (userInfo.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(userInfo.ErrorMessage);
				EGDebug.LogWarning(userInfo.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", userInfo.ErrorCode, userInfo.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnUseMailPhanThuongResponse : response null");
		}
		return true;
	}

	public void RequestReadAllMail()
	{
		SendRequest(m_C2SProxy.RequestReadAllMail, string.Empty, false);
	}

	public void RequestDangNhapQuayXoSo(DangNhapQuayXoSoRequest request)
	{
		SendRequest(m_C2SProxy.RequestDangNhapQuayXoSo, JsonMapper.ToJson(request, false));
	}

	public bool OnDangNhapQuayXoSoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DangNhapQuayXoSoResponse dangNhapQuayXoSoResponse = JsonMapper.ToObject<DangNhapQuayXoSoResponse>(data);
		if (dangNhapQuayXoSoResponse != null)
		{
			if (dangNhapQuayXoSoResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (dangNhapQuayXoSoResponse.PhanThuong.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(dangNhapQuayXoSoResponse.PhanThuong.UpdateUserInfo);
				}
				if (PopupDangNhapHangNgay.instance != null)
				{
					PopupDangNhapHangNgay.instance.startQuay(dangNhapQuayXoSoResponse);
				}
			}
			else if (dangNhapQuayXoSoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(dangNhapQuayXoSoResponse.ErrorMessage);
				EGDebug.LogWarning(dangNhapQuayXoSoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", dangNhapQuayXoSoResponse.ErrorCode, dangNhapQuayXoSoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnDangNhapQuayXoSoResponse : response null");
		}
		return true;
	}

	public void RequestHighlight()
	{
		SendRequest(m_C2SProxy.RequestHighlight, string.Empty, false);
	}

	public bool OnHighlightResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		HighlightResponse highlightResponse = JsonMapper.ToObject<HighlightResponse>(data);
		if (highlightResponse != null)
		{
			if (highlightResponse.ErrorCode == ERROR_CODE.OK)
			{
				List<HighLightItem> listItem = highlightResponse.listItem;
				listItem.Reverse();
				SystemMessage componentInChildren = GUIManager.instance.gadgetPanelTop.GetComponentInChildren<SystemMessage>();
				if (componentInChildren != null)
				{
					componentInChildren.SetData(listItem);
				}
			}
			else if (highlightResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(highlightResponse.ErrorMessage);
				EGDebug.LogWarning(highlightResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", highlightResponse.ErrorCode, highlightResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnHighlightResponse : response null");
		}
		return true;
	}

	public void RequestSetTranHinh(List<int> heroList, List<float> DhXList, List<float> DhYList)
	{
		SetTranHinhRequest setTranHinhRequest = new SetTranHinhRequest();
		setTranHinhRequest.HeroList = heroList;
		setTranHinhRequest.DhXList = DhXList;
		setTranHinhRequest.DhYList = DhYList;
		SendRequest(m_C2SProxy.RequestSetTranHinh, JsonMapper.ToJson(setTranHinhRequest, false));
	}

	public bool OnSetTranHinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SetTranHinhResponse setTranHinhResponse = JsonMapper.ToObject<SetTranHinhResponse>(data);
		if (setTranHinhResponse != null)
		{
			if (setTranHinhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (setTranHinhResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(setTranHinhResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
				{
					ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
					screenDoiHinh.SyncWithNetworkData();
				}
				PopupTranHinh.DestroyPopup();
			}
			else if (setTranHinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(setTranHinhResponse.ErrorMessage);
				EGDebug.LogWarning(setTranHinhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", setTranHinhResponse.ErrorCode, setTranHinhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetTrangBiResponse : response null");
		}
		return true;
	}

	public bool checkKNB(float soLuongCan)
	{
		if (soLuongCan > (float)GameManager.instance.m_GameClient.m_UserInfo.Gamer.Vang)
		{
			PopUpCheckKNB.Create();
			return false;
		}
		return true;
	}

	public bool OnDangNhapTrungTK(HostID remote, RmiContext rmiContext, string data)
	{
		_sessionReady = false;
		MessagePopup.Create(data);
		m_transport.Disconnect();
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenLogin);
		GUIManager.instance.ReloadScene(3f);
		return true;
	}

	public bool OnBaoTriServer(HostID remote, RmiContext rmiContext, string data)
	{
		_sessionReady = false;
		MessagePopup.Create(data);
		m_transport.Disconnect();
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenLogin);
		GUIManager.instance.ReloadScene(3f);
		return true;
	}

	public bool OnMatDongBoDuLieu(HostID remote, RmiContext rmiContext, string data)
	{
		_sessionReady = false;
		MessagePopup.Create(data);
		m_transport.Disconnect();
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenLogin);
		GUIManager.instance.ReloadScene(3f);
		return true;
	}

	public bool LogOffGameServer()
	{
		_sessionReady = false;
		GameManager.instance.m_GameClient.m_transport.Disconnect();
		GUIManager.instance.ReloadScene();
		return true;
	}

	public void RequestCT2Quit()
	{
		SendRequest(m_C2SProxy.RequestCT2Quit, string.Empty, false);
	}

	public void RequestGetExpLuaTrai()
	{
		SendRequest(m_C2SProxy.RequestGetExpLuaTrai, string.Empty, false);
	}

	public bool OnGetExpLuaTrai(HostID remote, RmiContext rmiContext, string data)
	{
		GetExpLuaTraiLienMinhResponse getExpLuaTraiLienMinhResponse = JsonMapper.ToObject<GetExpLuaTraiLienMinhResponse>(data);
		if (getExpLuaTraiLienMinhResponse != null)
		{
			if (getExpLuaTraiLienMinhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (getExpLuaTraiLienMinhResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(getExpLuaTraiLienMinhResponse.UpdateUserInfo);
				}
				ScreenLienMinhMain screenLienMinhMain = GUIManager.getScreen(GAME_SCREEN.ScreenLienMinhMain) as ScreenLienMinhMain;
				screenLienMinhMain.UpdateLuaTrai(getExpLuaTraiLienMinhResponse.LuaTrai);
				EGDebug.Log("Get Exp : " + getExpLuaTraiLienMinhResponse.Exp);
				screenLienMinhMain.PlayGetExp(UserInfo.Gamer.ID, getExpLuaTraiLienMinhResponse.Exp);
			}
			else if (getExpLuaTraiLienMinhResponse.ErrorCode == ERROR_CODE.MAT_DONG_BO)
			{
				if (getExpLuaTraiLienMinhResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(getExpLuaTraiLienMinhResponse.UpdateUserInfo);
				}
				ScreenLienMinhMain screenLienMinhMain2 = GUIManager.getScreen(GAME_SCREEN.ScreenLienMinhMain) as ScreenLienMinhMain;
				screenLienMinhMain2.UpdateLuaTrai(getExpLuaTraiLienMinhResponse.LuaTrai);
				EGDebug.Log(getExpLuaTraiLienMinhResponse.ErrorMessage);
				MessagePopup.Create(getExpLuaTraiLienMinhResponse.ErrorMessage);
			}
			else if (getExpLuaTraiLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(getExpLuaTraiLienMinhResponse.ErrorMessage);
				MessagePopup.Create(getExpLuaTraiLienMinhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ERROR {0} - Message: {1}", getExpLuaTraiLienMinhResponse.ErrorCode, getExpLuaTraiLienMinhResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnGetExpLuaTrai : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestThoiLuaLienMinh(bool useKNB)
	{
		ThoiLuaLienMinhRequest thoiLuaLienMinhRequest = new ThoiLuaLienMinhRequest();
		thoiLuaLienMinhRequest.UseKNB = useKNB;
		SendRequest(m_C2SProxy.RequestLienMinhThoiLua, JsonMapper.ToJson(thoiLuaLienMinhRequest, false));
	}

	public bool OnThoiLuaLienMinh(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThoiLuaLienMinhResponse thoiLuaLienMinhResponse = JsonMapper.ToObject<ThoiLuaLienMinhResponse>(data);
		if (thoiLuaLienMinhResponse != null)
		{
			if (thoiLuaLienMinhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thoiLuaLienMinhResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(thoiLuaLienMinhResponse.UpdateUserInfo);
				}
				ScreenLienMinhMain screenLienMinhMain = GUIManager.getScreen(GAME_SCREEN.ScreenLienMinhMain) as ScreenLienMinhMain;
				screenLienMinhMain.UpdateLuaTrai(thoiLuaLienMinhResponse.LuaTrai);
				GUIManager.setScreen(GAME_SCREEN.ScreenLienMinhMain);
			}
			else if (thoiLuaLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(thoiLuaLienMinhResponse.ErrorMessage);
				MessagePopup.Create(thoiLuaLienMinhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ERROR {0} - Message: {1}", thoiLuaLienMinhResponse.ErrorCode, thoiLuaLienMinhResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnThoiLuaLienMinh : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestRoiDiLuaTrai()
	{
		SendRequest(m_C2SProxy.RequestRoiKhoiLuaTraiLienMinh, string.Empty, false);
	}

	public bool OnRoiKhoiLuaTraiLienMinh(HostID remote, RmiContext rmiContext, string data)
	{
		ThamGiaLuaTraiResponse thamGiaLuaTraiResponse = JsonMapper.ToObject<ThamGiaLuaTraiResponse>(data);
		if (thamGiaLuaTraiResponse != null)
		{
			if (thamGiaLuaTraiResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thamGiaLuaTraiResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(thamGiaLuaTraiResponse.UpdateUserInfo);
				}
				ScreenLienMinhMain screenLienMinhMain = GUIManager.getScreen(GAME_SCREEN.ScreenLienMinhMain) as ScreenLienMinhMain;
				screenLienMinhMain.UpdateLuaTrai(thamGiaLuaTraiResponse.LuaTrai);
			}
			else if (thamGiaLuaTraiResponse.ErrorCode == ERROR_CODE.MAT_DONG_BO)
			{
				if (thamGiaLuaTraiResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(thamGiaLuaTraiResponse.UpdateUserInfo);
				}
				ScreenLienMinhMain screenLienMinhMain2 = GUIManager.getScreen(GAME_SCREEN.ScreenLienMinhMain) as ScreenLienMinhMain;
				screenLienMinhMain2.UpdateLuaTrai(thamGiaLuaTraiResponse.LuaTrai);
			}
			else if (thamGiaLuaTraiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(thamGiaLuaTraiResponse.ErrorMessage);
				MessagePopup.Create(thamGiaLuaTraiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ERROR {0} - Message: {1}", thamGiaLuaTraiResponse.ErrorCode, thamGiaLuaTraiResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnBatDauLuaTraiLienMinh : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestThamGiaLuaTrai(bool foreGround)
	{
		SendRequest(m_C2SProxy.RequestThamGiaLuaTraiLienMinh, string.Empty, foreGround);
	}

	public void RequestBatDauLuaTrai()
	{
		SendRequest(m_C2SProxy.RequestSetGioLuaTraiLienMinh, string.Empty);
	}

	public bool OnThamGiaLuaTraiLienMinh(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThamGiaLuaTraiResponse thamGiaLuaTraiResponse = JsonMapper.ToObject<ThamGiaLuaTraiResponse>(data);
		if (thamGiaLuaTraiResponse != null)
		{
			if (thamGiaLuaTraiResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thamGiaLuaTraiResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(thamGiaLuaTraiResponse.UpdateUserInfo);
				}
				ScreenLienMinhMain screenLienMinhMain = GUIManager.getScreen(GAME_SCREEN.ScreenLienMinhMain) as ScreenLienMinhMain;
				screenLienMinhMain.UpdateLuaTrai(thamGiaLuaTraiResponse.LuaTrai);
				GUIManager.setScreen(GAME_SCREEN.ScreenLienMinhMain);
			}
			else if (thamGiaLuaTraiResponse.ErrorCode == ERROR_CODE.MAT_DONG_BO)
			{
				EGDebug.Log(thamGiaLuaTraiResponse.ErrorMessage);
				MessagePopup.Create(thamGiaLuaTraiResponse.ErrorMessage);
				if (thamGiaLuaTraiResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(thamGiaLuaTraiResponse.UpdateUserInfo);
				}
				ScreenLienMinhMain screenLienMinhMain2 = GUIManager.getScreen(GAME_SCREEN.ScreenLienMinhMain) as ScreenLienMinhMain;
				screenLienMinhMain2.UpdateLuaTrai(thamGiaLuaTraiResponse.LuaTrai);
				GUIManager.setScreen(GAME_SCREEN.ScreenLienMinhMain);
			}
			else if (thamGiaLuaTraiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(thamGiaLuaTraiResponse.ErrorMessage);
				MessagePopup.Create(thamGiaLuaTraiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ERROR {0} - Message: {1}", thamGiaLuaTraiResponse.ErrorCode, thamGiaLuaTraiResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnBatDauLuaTraiLienMinh : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public bool OnBatDauLuaTraiLienMinh(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThamGiaLuaTraiResponse thamGiaLuaTraiResponse = JsonMapper.ToObject<ThamGiaLuaTraiResponse>(data);
		if (thamGiaLuaTraiResponse != null)
		{
			if (thamGiaLuaTraiResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thamGiaLuaTraiResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(thamGiaLuaTraiResponse.UpdateUserInfo);
				}
				ScreenLienMinhMain screenLienMinhMain = GUIManager.getScreen(GAME_SCREEN.ScreenLienMinhMain) as ScreenLienMinhMain;
				screenLienMinhMain.UpdateLuaTrai(thamGiaLuaTraiResponse.LuaTrai);
				GUIManager.setScreen(GAME_SCREEN.ScreenLienMinhMain);
			}
			else if (thamGiaLuaTraiResponse.ErrorCode == ERROR_CODE.MAT_DONG_BO)
			{
				EGDebug.Log(thamGiaLuaTraiResponse.ErrorMessage);
				MessagePopup.Create(thamGiaLuaTraiResponse.ErrorMessage);
				if (thamGiaLuaTraiResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(thamGiaLuaTraiResponse.UpdateUserInfo);
				}
				ScreenLienMinhMain screenLienMinhMain2 = GUIManager.getScreen(GAME_SCREEN.ScreenLienMinhMain) as ScreenLienMinhMain;
				screenLienMinhMain2.UpdateLuaTrai(thamGiaLuaTraiResponse.LuaTrai);
				GUIManager.setScreen(GAME_SCREEN.ScreenLienMinhMain);
			}
			else if (thamGiaLuaTraiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(thamGiaLuaTraiResponse.ErrorMessage);
				MessagePopup.Create(thamGiaLuaTraiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ERROR {0} - Message: {1}", thamGiaLuaTraiResponse.ErrorCode, thamGiaLuaTraiResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnBatDauLuaTraiLienMinh : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestCT2BXH()
	{
		SendRequest(m_C2SProxy.RequestCT2GetBXH, string.Empty);
	}

	public bool OnCT2BXH(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		CT2BXHHienTaiResponse cT2BXHHienTaiResponse = JsonMapper.ToObject<CT2BXHHienTaiResponse>(data);
		if (cT2BXHHienTaiResponse != null)
		{
			if (cT2BXHHienTaiResponse.ErrorCode == ERROR_CODE.OK)
			{
				PopupBXHChinhTa.Create(cT2BXHHienTaiResponse, -1);
			}
			else if (cT2BXHHienTaiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(cT2BXHHienTaiResponse.ErrorMessage);
				MessagePopup.Create(cT2BXHHienTaiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ERROR {0} - Message: {1}", cT2BXHHienTaiResponse.ErrorCode, cT2BXHHienTaiResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnCT2BXH : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestCT2BXHTuanNay()
	{
		SendRequest(m_C2SProxy.RequestCT2BangXepHangTuanNay, string.Empty);
	}

	public void RequestCT2BXHTuanTruoc()
	{
		SendRequest(m_C2SProxy.RequestCT2BangXepHangTuanTruoc, string.Empty);
	}

	public bool OnCT2BXHTuanNay(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		CT2BXHTuanResponse cT2BXHTuanResponse = JsonMapper.ToObject<CT2BXHTuanResponse>(data);
		if (cT2BXHTuanResponse != null)
		{
			if (cT2BXHTuanResponse.ErrorCode == ERROR_CODE.OK)
			{
				PopupBXHTuanChinhTa.CreateTuanNay(cT2BXHTuanResponse);
			}
			else if (cT2BXHTuanResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(cT2BXHTuanResponse.ErrorMessage);
				MessagePopup.Create(cT2BXHTuanResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ERROR {0} - Message: {1}", cT2BXHTuanResponse.ErrorCode, cT2BXHTuanResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnCT2BXHTuanNay : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public bool OnCT2BXHTuanTruoc(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		CT2BXHTuanResponse cT2BXHTuanResponse = JsonMapper.ToObject<CT2BXHTuanResponse>(data);
		if (cT2BXHTuanResponse != null)
		{
			if (cT2BXHTuanResponse.ErrorCode == ERROR_CODE.OK)
			{
				PopupBXHTuanChinhTa.CreateTuanTruoc(cT2BXHTuanResponse);
			}
			else if (cT2BXHTuanResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(cT2BXHTuanResponse.ErrorMessage);
				MessagePopup.Create(cT2BXHTuanResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ERROR {0} - Message: {1}", cT2BXHTuanResponse.ErrorCode, cT2BXHTuanResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnCT2BXHTuanNay : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestThamGiaCT2()
	{
		JoinCT2Request obj = new JoinCT2Request();
		SendRequest(m_C2SProxy.RequestThamGiaCT2, JsonMapper.ToJson(obj, false));
	}

	public bool OnThamGiaCT2(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		JoinCT2Response joinCT2Response = JsonMapper.ToObject<JoinCT2Response>(data);
		if (joinCT2Response != null)
		{
			if (joinCT2Response.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen != GAME_SCREEN.ScreenCT2)
				{
					ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
					ScreenChienTruong3D screenChienTruong3D = screenCT.Get3DCom();
					GameManager.instance.isStartJoinCT2 = false;
					GUIManager.setScreen(GAME_SCREEN.ScreenCT2);
					GameManager.instance.isStartJoinCT2 = true;
					screenCT.SetTimeOut(joinCT2Response.RoomInfo.TimeOut);
					screenCT.SetDiem(joinCT2Response.ThongTinNguoiChoi.Diem);
					screenCT.SetDiet(joinCT2Response.ThongTinNguoiChoi.Kill);
					screenChienTruong3D.OnJoinCT2Response(joinCT2Response);
					GameManager.instance.CT2KetThuc = false;
				}
			}
			else if (joinCT2Response.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(joinCT2Response.ErrorMessage);
				MessagePopup.Create(joinCT2Response.ErrorMessage);
			}
			else
			{
				string text = string.Format("ERROR {0} - Message: {1}", joinCT2Response.ErrorCode, joinCT2Response.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnThamGiaCT2 : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestGetListCT2()
	{
		GetListCT2Request obj = new GetListCT2Request();
		SendRequest(m_C2SProxy.RequestListCT2, JsonMapper.ToJson(obj, false));
	}

	public bool OnGetListCT2(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetListCT2Response getListCT2Response = JsonMapper.ToObject<GetListCT2Response>(data);
		if (getListCT2Response != null)
		{
			if (getListCT2Response.ErrorCode == ERROR_CODE.OK)
			{
				ScreenCT2HoatDong screenCT2HoatDong = GUIManager.getScreen(GAME_SCREEN.ScreenCT2HoatDong) as ScreenCT2HoatDong;
				screenCT2HoatDong.SyncWithNetworkData(getListCT2Response);
			}
			else if (getListCT2Response.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(getListCT2Response.ErrorMessage);
				MessagePopup.Create(getListCT2Response.ErrorMessage);
			}
			else
			{
				string text = string.Format("ERROR {0} - Message: {1}", getListCT2Response.ErrorCode, getListCT2Response.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnGetListCT2 : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public bool OnCT2RuneXuatHien(HostID remote, RmiContext rmiContext, int runeType, int runeIndex, float posx, float posz)
	{
		ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
		ScreenChienTruong3D screenChienTruong3D = screenCT.Get3DCom();
		screenChienTruong3D.SpawnRune(runeType, runeIndex, posx, posz);
		EGDebug.Log("OnCT2RuneXuatHien");
		return true;
	}

	public bool OnCT2GotRune(HostID remote, RmiContext rmiContext, int gid, int serverId, int runeType, int runeIndex, float tyleHp, string teamInfo)
	{
		if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenCT2)
		{
			ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
			ScreenChienTruong3D screenChienTruong3D = screenCT.Get3DCom();
			if (runeIndex >= 0)
			{
				screenChienTruong3D.SpawnRune(runeType, runeIndex, -100f, -100f);
			}
			CT2BasePlayer cT2BasePlayer = screenChienTruong3D.SearchPlayer(gid, serverId);
			CT2Player1 cT2Player = cT2BasePlayer as CT2Player1;
			if (cT2Player != null && !string.IsNullOrEmpty(teamInfo))
			{
				float[] tyLeHp = JsonMapper.ToObject<float[]>(teamInfo);
				screenChienTruong3D.UpdateHpDoiHinh(tyLeHp);
			}
			if (cT2BasePlayer != null)
			{
				cT2BasePlayer.GotRune((ChienTruongChinhTa.RuneType)runeType, tyleHp);
			}
		}
		return true;
	}

	public bool OnCT2BattleResult(HostID remote, RmiContext rmiContext, string data)
	{
		if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenCT2 && !isAutoChienTruong)
		{
			ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
			ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
			GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
			CT2BattleResponse cT2BattleResponse = JsonMapper.ToObject<CT2BattleResponse>(data);
			screenCT.SetDiem(cT2BattleResponse.ThongTinNguoiChoi.Diem);
			screenCT.SetDiet(cT2BattleResponse.ThongTinNguoiChoi.Kill);
			screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenCT2;
			screenBattle.MyTeamData = ((!screenBattle.ChkTeam1IsOwner()) ? cT2BattleResponse.BattleResult.Team2Data : cT2BattleResponse.BattleResult.Team1Data);
			List<UserInfo.HeroData> listCloneHeroFromDoiHinh = UserInfo.GetListCloneHeroFromDoiHinh();
			PopupBattleResult popupBattleResult = PopupBattleResult.Create(cT2BattleResponse.BattleResult, listCloneHeroFromDoiHinh, 0L, 0L, 0L, -1);
			screenBattle.DataReplay = cT2BattleResponse.BattleResult;
			screenBattle.BattleEnd();
			popupBattleResult.OnClosePopup = screenCT.OnBattleEnd;
		}
		else if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenCT2)
		{
			ScreenCT2 screenCT2 = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
			CT2BattleResponse cT2BattleResponse2 = JsonMapper.ToObject<CT2BattleResponse>(data);
			screenCT2.SetDiem(cT2BattleResponse2.ThongTinNguoiChoi.Diem);
			screenCT2.SetDiet(cT2BattleResponse2.ThongTinNguoiChoi.Kill);
			screenCT2.OnBattleEnd();
		}
		return true;
	}

	public bool OnCT2KetThuc(HostID remote, RmiContext rmiContext, string data)
	{
		CT2KetThucResponse cT2KetThucResponse = JsonMapper.ToObject<CT2KetThucResponse>(data);
		if (cT2KetThucResponse != null)
		{
			if (cT2KetThucResponse.ErrorCode == ERROR_CODE.OK)
			{
				GameManager.instance.CT2KetThuc = true;
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenCT2)
				{
					GameManager.instance.isStartJoinCT2 = false;
					ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
					screenCT.Destroy3D();
					GUIManager.instance.SetScreen(GAME_SCREEN.ScreenCT2HoatDong);
				}
				ScreenCT2HoatDong screenCT2HoatDong = GUIManager.getScreen(GAME_SCREEN.ScreenCT2HoatDong) as ScreenCT2HoatDong;
				PopupBXHChinhTa popupBXHChinhTa = PopupBXHChinhTa.Create(cT2KetThucResponse.BangXepHang, cT2KetThucResponse.TeamWinner);
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBattle)
				{
					popupBXHChinhTa.gameObject.SetActive(false);
				}
			}
			else if (cT2KetThucResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(cT2KetThucResponse.ErrorMessage);
				MessagePopup.Create(cT2KetThucResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ERROR {0} - Message: {1}", cT2KetThucResponse.ErrorCode, cT2KetThucResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnCT2KetThuc : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public bool OnCT2PlayerPos(HostID remote, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z, float vel_x, float vel_z)
	{
		ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
		ScreenChienTruong3D screenChienTruong3D = screenCT.Get3DCom();
		CT2BasePlayer cT2BasePlayer = screenChienTruong3D.SearchPlayer(GID, SID);
		if (cT2BasePlayer != null)
		{
			cT2BasePlayer.PosFSetTarget(pos_x, pos_z, vel_x, vel_z);
		}
		return true;
	}

	public bool OnCT2NPCMove(HostID remote, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z, float vel_x, float vel_z, byte state)
	{
		ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
		ScreenChienTruong3D screenChienTruong3D = screenCT.Get3DCom();
		CT2BasePlayer cT2BasePlayer = screenChienTruong3D.SearchPlayer(GID, SID);
		if (cT2BasePlayer != null)
		{
			cT2BasePlayer.PosFSetTarget(pos_x, pos_z, vel_x, vel_z, state);
		}
		return true;
	}

	public bool OnCT2NPCIdle(HostID remote, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z)
	{
		ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
		ScreenChienTruong3D screenChienTruong3D = screenCT.Get3DCom();
		CT2BasePlayer cT2BasePlayer = screenChienTruong3D.SearchPlayer(GID, SID);
		if (cT2BasePlayer != null)
		{
			cT2BasePlayer.PosFSetTarget_Idle(pos_x, pos_z);
		}
		return true;
	}

	public bool OnCT2NPCAttack(HostID remote, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z)
	{
		ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
		ScreenChienTruong3D screenChienTruong3D = screenCT.Get3DCom();
		CT2BasePlayer cT2BasePlayer = screenChienTruong3D.SearchPlayer(GID, SID);
		if (cT2BasePlayer != null)
		{
			cT2BasePlayer.PosFSetTarget_Attack(pos_x, pos_z);
		}
		return true;
	}

	public bool OnCT2HPPercent(HostID remote, RmiContext rmiContext, int GID, int SID, float tyleHp, int sinhMenhValue)
	{
		ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
		ScreenChienTruong3D screenChienTruong3D = screenCT.Get3DCom();
		CT2BasePlayer cT2BasePlayer = screenChienTruong3D.SearchPlayer(GID, SID);
		if (cT2BasePlayer != null)
		{
			cT2BasePlayer.SetTyleHp(tyleHp);
			cT2BasePlayer.ShowName(sinhMenhValue, screenChienTruong3D.GetMainPlayerChinhPhai());
		}
		return true;
	}

	public bool OnCT2HPTeam(HostID remote, RmiContext rmiContext, string data)
	{
		ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
		ScreenChienTruong3D screenChienTruong3D = screenCT.Get3DCom();
		float[] tyLeHp = JsonMapper.ToObject<float[]>(data);
		screenChienTruong3D.UpdateHpDoiHinh(tyLeHp);
		return true;
	}

	public bool OnCT2Teleport(HostID remote, RmiContext rmiContext, int GID, int SID, float pos_x, float pos_z)
	{
		ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
		ScreenChienTruong3D screenChienTruong3D = screenCT.Get3DCom();
		CT2BasePlayer cT2BasePlayer = screenChienTruong3D.SearchPlayer(GID, SID);
		if (cT2BasePlayer != null)
		{
			cT2BasePlayer.Teleport(pos_x, pos_z);
		}
		return true;
	}

	public bool OnCT2PlayerAppear(HostID remote, RmiContext rmiContext, string data)
	{
		ChienTruongChinhTa.NguoiChoi info = JsonMapper.ToObject<ChienTruongChinhTa.NguoiChoi>(data);
		ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
		ScreenChienTruong3D screenChienTruong3D = screenCT.Get3DCom();
		screenChienTruong3D.SpawnPlayer3(info);
		return true;
	}

	public bool OnCT2PlayerState(HostID remote, RmiContext rmiContext, int GID, int SID, byte state)
	{
		ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
		ScreenChienTruong3D screenChienTruong3D = screenCT.Get3DCom();
		CT2BasePlayer cT2BasePlayer = screenChienTruong3D.SearchPlayer(GID, SID);
		if (cT2BasePlayer != null)
		{
			cT2BasePlayer.SetState((CT2EntityState)state);
		}
		return true;
	}

	public void RequestGetTopLienMinh()
	{
		GetTopLienMinhRequest obj = new GetTopLienMinhRequest();
		SendRequest(m_C2SProxy.RequestGetTopLienMinh, JsonMapper.ToJson(obj));
	}

	public bool OnGetTopLienMinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetTopLienMinhResponse getTopLienMinhResponse = JsonMapper.ToObject<GetTopLienMinhResponse>(data);
		if (getTopLienMinhResponse.ErrorCode == ERROR_CODE.OK)
		{
			ScreenLienMinh.TopLienMinhList = getTopLienMinhResponse.TopLienMinh;
			if (ScreenLienMinh.ScreenStatus == "ScreenLienMinhGiaNhap")
			{
				GUIManager.instance.SetScreen(GAME_SCREEN.ScreenListLienMinhGiaNhap);
				ScreenLienMinh.ScreenLienMinhGiaNhap = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenListLienMinhGiaNhap) as ScreenListLMGiaNhap;
			}
		}
		else if (getTopLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			MessagePopup.Create(getTopLienMinhResponse.ErrorMessage);
			EGDebug.LogWarning(getTopLienMinhResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", getTopLienMinhResponse.ErrorCode, getTopLienMinhResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestSearchLienMinh(SearchLienMinhRequest request)
	{
		SendRequest(m_C2SProxy.RequestSearchLienMinh, JsonMapper.ToJson(request));
	}

	public bool OnSearchLienMinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SearchLienMinhResponse searchLienMinhResponse = JsonMapper.ToObject<SearchLienMinhResponse>(data);
		if (searchLienMinhResponse.ErrorCode == ERROR_CODE.OK)
		{
			ScreenLienMinh.TopLienMinhList = searchLienMinhResponse.LienMinhList;
			if (ScreenLienMinh.ScreenStatus == "ScreenLienMinhGiaNhap")
			{
				GUIManager.instance.SetScreen(GAME_SCREEN.ScreenListLienMinhGiaNhap);
				ScreenLienMinh.ScreenLienMinhGiaNhap = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenListLienMinhGiaNhap) as ScreenListLMGiaNhap;
				ScreenLienMinh.ScreenLienMinhGiaNhap.OnEnable();
			}
		}
		else if (searchLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			MessagePopup.Create(searchLienMinhResponse.ErrorMessage);
			EGDebug.LogWarning(searchLienMinhResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", searchLienMinhResponse.ErrorCode, searchLienMinhResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestUpdateLienMinhInfo()
	{
		UpdateLienMinhRequest obj = new UpdateLienMinhRequest();
		SendRequest(m_C2SProxy.RequestUpdateLienMinhData, JsonMapper.ToJson(obj));
	}

	public bool OnUpdateLienMinhInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UpdateLienMinhResponse updateLienMinhResponse = JsonMapper.ToObject<UpdateLienMinhResponse>(data);
		if (updateLienMinhResponse.ErrorCode == ERROR_CODE.OK)
		{
			if (updateLienMinhResponse.updateUserInfo.LienMinh == null)
			{
				GameManager.instance.m_GameClient.m_UserInfo.LienMinh = null;
				GUIManager.instance.gadgetPanelBottom.bangHoiGrp.GetComponent<ScreenLienMinh>().OnLienMinh();
			}
			else
			{
				bool flag = false;
				if (GameManager.instance.m_GameClient.m_UserInfo.LienMinh != null && (updateLienMinhResponse.updateUserInfo.LienMinh.TangKiemCacLevel != GameManager.instance.m_GameClient.UserInfo.LienMinh.TangKiemCacLevel || updateLienMinhResponse.updateUserInfo.LienMinh.ThienHaLauLevel != GameManager.instance.m_GameClient.UserInfo.LienMinh.ThienHaLauLevel || updateLienMinhResponse.updateUserInfo.LienMinh.TuNghiaLevel != GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel))
				{
					flag = true;
				}
				GameManager.instance.m_GameClient.UserInfo.UpdateInfo(updateLienMinhResponse.updateUserInfo);
				if (ScreenLienMinh.ScreenStatus != null && ScreenLienMinh.ScreenStatus == "UpdateInfo")
				{
					GUIManager.instance.gadgetPanelBottom.bangHoiGrp.GetComponent<ScreenLienMinh>().OnLienMinh();
					if (flag)
					{
						GUIManager.instance.lienMinh3D.RefreshCongTrinh(true);
					}
				}
			}
		}
		else if (updateLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			MessagePopup.Create(updateLienMinhResponse.ErrorMessage);
			EGDebug.LogWarning(updateLienMinhResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", updateLienMinhResponse.ErrorCode, updateLienMinhResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestGetLienMinhInfo(int id)
	{
		GetThongTinLienMinhRequest getThongTinLienMinhRequest = new GetThongTinLienMinhRequest();
		getThongTinLienMinhRequest.LienMinhID = id;
		SendRequest(m_C2SProxy.RequestGetThongTinLienMinh, JsonMapper.ToJson(getThongTinLienMinhRequest));
	}

	public bool OnGetThongTinLienMinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetThongTinLienMinhResponse getThongTinLienMinhResponse = JsonMapper.ToObject<GetThongTinLienMinhResponse>(data);
		if (getThongTinLienMinhResponse.ErrorCode == ERROR_CODE.OK)
		{
			ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh] = getThongTinLienMinhResponse.LienMinh;
			if (ScreenLienMinh.ScreenStatus == "LienMinhDetail")
			{
				GUIManager.instance.SetScreen(GAME_SCREEN.ScreenThanhVienLienMinhGuest);
				ScreenLienMinh.ScreenLMThanhVienGuest = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenThanhVienLienMinhGuest) as ScreenLienMinhThanhVienGuest;
			}
		}
		else if (getThongTinLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			MessagePopup.Create(getThongTinLienMinhResponse.ErrorMessage);
			EGDebug.LogWarning(getThongTinLienMinhResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", getThongTinLienMinhResponse.ErrorCode, getThongTinLienMinhResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestGiaNhapLienMinh(int id)
	{
		XinGiaNhapLienMinhRequest xinGiaNhapLienMinhRequest = new XinGiaNhapLienMinhRequest();
		xinGiaNhapLienMinhRequest.LienMinhID = id;
		SendRequest(m_C2SProxy.RequestGiaNhapLienMinh, JsonMapper.ToJson(xinGiaNhapLienMinhRequest));
	}

	public bool OnGiaNhapLienMinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		XinGiaNhapLienMinhResponse xinGiaNhapLienMinhResponse = JsonMapper.ToObject<XinGiaNhapLienMinhResponse>(data);
		if (xinGiaNhapLienMinhResponse.ErrorCode == ERROR_CODE.OK)
		{
			GameManager.instance.m_GameClient.UserInfo.LienMinhCaNhan.RequestList.Add(xinGiaNhapLienMinhResponse.LienMinhID);
			if (ScreenLienMinh.ScreenStatus == "LienMinhXinGiaNhap")
			{
				ScreenLienMinh.ScreenLienMinhGiaNhap.OnEnable();
			}
		}
		else if (xinGiaNhapLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			MessagePopup.Create(xinGiaNhapLienMinhResponse.ErrorMessage);
			EGDebug.LogWarning(xinGiaNhapLienMinhResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", xinGiaNhapLienMinhResponse.ErrorCode, xinGiaNhapLienMinhResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestRutGiaNhapLienMinh(int id)
	{
		RutGiaNhapLienMinhRequest rutGiaNhapLienMinhRequest = new RutGiaNhapLienMinhRequest();
		rutGiaNhapLienMinhRequest.LienMinhID = id;
		SendRequest(m_C2SProxy.RequestRutGiaNhapLienMinh, JsonMapper.ToJson(rutGiaNhapLienMinhRequest));
	}

	public bool OnRutGiaNhapLienMinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		RutGiaNhapLienMinhResponse rutGiaNhapLienMinhResponse = JsonMapper.ToObject<RutGiaNhapLienMinhResponse>(data);
		if (rutGiaNhapLienMinhResponse.ErrorCode == ERROR_CODE.OK)
		{
			GameManager.instance.m_GameClient.UserInfo.LienMinhCaNhan.RequestList.Remove(rutGiaNhapLienMinhResponse.LienMinhID);
			if (ScreenLienMinh.ScreenStatus == "LienMinhRutGiaNhap")
			{
				ScreenLienMinh.ScreenLienMinhGiaNhap.OnEnable();
			}
		}
		else if (rutGiaNhapLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			MessagePopup.Create(rutGiaNhapLienMinhResponse.ErrorMessage);
			EGDebug.LogWarning(rutGiaNhapLienMinhResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", rutGiaNhapLienMinhResponse.ErrorCode, rutGiaNhapLienMinhResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestLapLienMinh(LapLienMinhRequest request)
	{
		SendRequest(m_C2SProxy.RequestLapLienMinh, JsonMapper.ToJson(request));
	}

	public bool OnLapLienMinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		LapLienMinhResponse lapLienMinhResponse = JsonMapper.ToObject<LapLienMinhResponse>(data);
		if (lapLienMinhResponse.ErrorCode == ERROR_CODE.OK)
		{
			GameManager.instance.m_GameClient.UserInfo.UpdateInfo(lapLienMinhResponse.UpdateInfo);
			ScreenLienMinh.instance.OnLienMinh();
		}
		else if (lapLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			MessagePopup.Create(lapLienMinhResponse.ErrorMessage);
			EGDebug.LogWarning(lapLienMinhResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", lapLienMinhResponse.ErrorCode, lapLienMinhResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestChapNhanGiaNhapLienMinh(ChapNhanGiaNhapLienMinhRequest request)
	{
		SendRequest(m_C2SProxy.RequestChapNhanGiaNhapLienMinh, JsonMapper.ToJson(request));
	}

	public bool OnChapNhanGiaNhapLienMinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ChapNhanGiaNhapLienMinhResponse chapNhanGiaNhapLienMinhResponse = JsonMapper.ToObject<ChapNhanGiaNhapLienMinhResponse>(data);
		if (chapNhanGiaNhapLienMinhResponse.ErrorCode == ERROR_CODE.OK)
		{
			if (chapNhanGiaNhapLienMinhResponse.Ok)
			{
				UserInfo.LienMinh.ThanhVienList.Add(chapNhanGiaNhapLienMinhResponse.ThanhVien);
			}
			foreach (LienMinhXinGiaNhapData xinGiaNhap in UserInfo.LienMinh.XinGiaNhapList)
			{
				if (xinGiaNhap.ID == chapNhanGiaNhapLienMinhResponse.ThanhVien.ID)
				{
					UserInfo.LienMinh.XinGiaNhapList.Remove(xinGiaNhap);
					break;
				}
			}
			ScreenLienMinh.ScreenQuanLyThanhVienLienMinh.OnGiaNhap(true);
		}
		else if (chapNhanGiaNhapLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			MessagePopup.Create(chapNhanGiaNhapLienMinhResponse.ErrorMessage);
			EGDebug.LogWarning(chapNhanGiaNhapLienMinhResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", chapNhanGiaNhapLienMinhResponse.ErrorCode, chapNhanGiaNhapLienMinhResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestThoatLienMinh(ThoatLienMinhRequest request)
	{
		SendRequest(m_C2SProxy.RequestThoatLienMinh, JsonMapper.ToJson(request));
	}

	public bool OnThoatLienMinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThoatLienMinhResponse thoatLienMinhResponse = JsonMapper.ToObject<ThoatLienMinhResponse>(data);
		if (thoatLienMinhResponse.ErrorCode == ERROR_CODE.OK)
		{
			UserInfo.LienMinh = null;
			GUIManager.instance.gadgetPanelBottom.OnClickBtnHome();
		}
		else if (thoatLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			MessagePopup.Create(thoatLienMinhResponse.ErrorMessage);
			EGDebug.LogWarning(thoatLienMinhResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", thoatLienMinhResponse.ErrorCode, thoatLienMinhResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestFinishNhiemVuLienMinh(FinishNhiemVuLienMinhRequest request)
	{
		SendRequest(m_C2SProxy.RequestFinishNhiemVuLienMinh, JsonMapper.ToJson(request));
	}

	public bool OnFinishNhiemVuLienMinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		FinishNhiemVuLienMinhResponse finishNhiemVuLienMinhResponse = JsonMapper.ToObject<FinishNhiemVuLienMinhResponse>(data);
		if (finishNhiemVuLienMinhResponse.ErrorCode == ERROR_CODE.OK)
		{
			GameManager.instance.m_GameClient.UserInfo.UpdateInfo(finishNhiemVuLienMinhResponse.updateUserInfo);
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuong"), string.Empty, finishNhiemVuLienMinhResponse.pt);
			ScreenLienMinh.ScreenCongHienLienMinh.OnCongHienTab(true);
		}
		else if (finishNhiemVuLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			MessagePopup.Create(finishNhiemVuLienMinhResponse.ErrorMessage);
			EGDebug.LogWarning(finishNhiemVuLienMinhResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", finishNhiemVuLienMinhResponse.ErrorCode, finishNhiemVuLienMinhResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestDangHuongLienMinh(DangHuongLienMinhRequest request)
	{
		SendRequest(m_C2SProxy.RequestDangHuongLienMinh, JsonMapper.ToJson(request));
	}

	public bool OnDangHuongLienMinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DangHuongLienMinhResponse dangHuongLienMinhResponse = JsonMapper.ToObject<DangHuongLienMinhResponse>(data);
		if (dangHuongLienMinhResponse.ErrorCode == ERROR_CODE.OK)
		{
			GameManager.instance.m_GameClient.UserInfo.UpdateInfo(dangHuongLienMinhResponse.updateUserInfo);
			ScreenLienMinh.ScreenCongHienLienMinh.OnDangHuongTab(true);
		}
		else if (dangHuongLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			MessagePopup.Create(dangHuongLienMinhResponse.ErrorMessage);
			EGDebug.LogWarning(dangHuongLienMinhResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", dangHuongLienMinhResponse.ErrorCode, dangHuongLienMinhResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestDoiThuongLienMinh(DoiThuongLienMinhRequest request)
	{
		SendRequest(m_C2SProxy.RequestDoiThuongLienMinh, JsonMapper.ToJson(request));
	}

	public bool OnDoiThuongLienMinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DoiThuongLienMinhResponse doiThuongLienMinhResponse = JsonMapper.ToObject<DoiThuongLienMinhResponse>(data);
		if (doiThuongLienMinhResponse.ErrorCode == ERROR_CODE.OK)
		{
			GameManager.instance.m_GameClient.UserInfo.UpdateInfo(doiThuongLienMinhResponse.updateUserInfo);
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuong"), string.Empty, doiThuongLienMinhResponse.pt);
			ScreenLienMinh.ScreenCongHienLienMinh.OnDoiThuongTab(true, false);
		}
		else if (doiThuongLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			MessagePopup.Create(doiThuongLienMinhResponse.ErrorMessage);
			EGDebug.LogWarning(doiThuongLienMinhResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", doiThuongLienMinhResponse.ErrorCode, doiThuongLienMinhResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestDoiMinhChu(DoiMinhChuRequest request)
	{
		SendRequest(m_C2SProxy.RequestDoiThuongLienMinh, JsonMapper.ToJson(request));
	}

	public bool OnDoiMinhChuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DoiMinhChuResponse doiMinhChuResponse = JsonMapper.ToObject<DoiMinhChuResponse>(data);
		if (doiMinhChuResponse.ErrorCode == ERROR_CODE.OK)
		{
			GameManager.instance.m_GameClient.UserInfo.LienMinh.MinhChuID = doiMinhChuResponse.BangChuId;
		}
		else if (doiMinhChuResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			MessagePopup.Create(doiMinhChuResponse.ErrorMessage);
			EGDebug.LogWarning(doiMinhChuResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", doiMinhChuResponse.ErrorCode, doiMinhChuResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestDoiPhoMinhChu(DoiPhoMinhChuRequest request)
	{
		SendRequest(m_C2SProxy.RequestDoiPhoMinhChu, JsonMapper.ToJson(request));
	}

	public bool OnDoiPhoMinhChuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DoiPhoMinhChuResponse doiPhoMinhChuResponse = JsonMapper.ToObject<DoiPhoMinhChuResponse>(data);
		if (doiPhoMinhChuResponse.ErrorCode == ERROR_CODE.OK)
		{
			GameManager.instance.m_GameClient.UserInfo.LienMinh.PhoMinhChuID = doiPhoMinhChuResponse.BangChuId;
			ScreenLienMinh.ScreenQuanLyThanhVienLienMinh.OnDanhSach(true);
		}
		else if (doiPhoMinhChuResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			MessagePopup.Create(doiPhoMinhChuResponse.ErrorMessage);
			EGDebug.LogWarning(doiPhoMinhChuResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", doiPhoMinhChuResponse.ErrorCode, doiPhoMinhChuResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestNangCapCongTrinh(NangCapCongTrinhRequest request)
	{
		SendRequest(m_C2SProxy.RequestNangCapCongTrinh, JsonMapper.ToJson(request));
	}

	public bool OnNangCapCongTrinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		NangCapCongTrinhResponse nangCapCongTrinhResponse = JsonMapper.ToObject<NangCapCongTrinhResponse>(data);
		if (nangCapCongTrinhResponse.ErrorCode == ERROR_CODE.OK)
		{
			GameManager.instance.m_GameClient.UserInfo.UpdateInfo(nangCapCongTrinhResponse.updateUserInfo);
			ScreenLienMinh.ScreenQuanLyCongTrinhLienMinh.OnEnable();
			GUIManager.instance.lienMinh3D.RefreshCongTrinh(false);
		}
		else if (nangCapCongTrinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			MessagePopup.Create(nangCapCongTrinhResponse.ErrorMessage);
			EGDebug.LogWarning(nangCapCongTrinhResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", nangCapCongTrinhResponse.ErrorCode, nangCapCongTrinhResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestDuoiKhoiLienMinh(DuoiKhoiLienMinhRequest request)
	{
		SendRequest(m_C2SProxy.RequestDuoiKhoiLienMinh, JsonMapper.ToJson(request));
	}

	public bool OnDuoiKhoiLienMinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DuoiKhoiLienMinhResponse duoiKhoiLienMinhResponse = JsonMapper.ToObject<DuoiKhoiLienMinhResponse>(data);
		if (duoiKhoiLienMinhResponse.ErrorCode == ERROR_CODE.OK)
		{
			GameManager.instance.m_GameClient.UserInfo.UpdateInfo(duoiKhoiLienMinhResponse.updateUserInfo);
			ScreenLienMinh.ScreenQuanLyThanhVienLienMinh.OnDanhSach(true);
		}
		else if (duoiKhoiLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			if (duoiKhoiLienMinhResponse.updateUserInfo != null)
			{
				GameManager.instance.m_GameClient.UserInfo.UpdateInfo(duoiKhoiLienMinhResponse.updateUserInfo);
			}
			MessagePopup.Create(duoiKhoiLienMinhResponse.ErrorMessage);
			EGDebug.LogWarning(duoiKhoiLienMinhResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", duoiKhoiLienMinhResponse.ErrorCode, duoiKhoiLienMinhResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestResetNhiemVuLienMinh(ResetNhiemVuLienMinhRequest request)
	{
		SendRequest(m_C2SProxy.RequestResetNhiemVuLienMinh, JsonMapper.ToJson(request));
	}

	public bool OnResetNhiemVuLienMinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ResetNhiemVuLienMinhResponse resetNhiemVuLienMinhResponse = JsonMapper.ToObject<ResetNhiemVuLienMinhResponse>(data);
		if (resetNhiemVuLienMinhResponse.ErrorCode == ERROR_CODE.OK)
		{
			GameManager.instance.m_GameClient.UserInfo.UpdateInfo(resetNhiemVuLienMinhResponse.UpdateUserInfo);
			ScreenLienMinh.ScreenCongHienLienMinh.OnCongHienTab(true);
		}
		else if (resetNhiemVuLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			MessagePopup.Create(resetNhiemVuLienMinhResponse.ErrorMessage);
			EGDebug.LogWarning(resetNhiemVuLienMinhResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", resetNhiemVuLienMinhResponse.ErrorCode, resetNhiemVuLienMinhResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestSuaThongBaoLienMinh(SuaThongBaoLienMinhRequest request)
	{
		SendRequest(m_C2SProxy.RequestSuaThongBaoLienMinh, JsonMapper.ToJson(request));
	}

	public bool OnSuaThongBaoLienMinhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SuaThongBaoLienMinhResponse suaThongBaoLienMinhResponse = JsonMapper.ToObject<SuaThongBaoLienMinhResponse>(data);
		if (suaThongBaoLienMinhResponse.ErrorCode == ERROR_CODE.OK)
		{
			GameManager.instance.m_GameClient.UserInfo.LienMinh.ThongBao = suaThongBaoLienMinhResponse.content;
			UnityEngine.Object.FindObjectOfType<ScreenLienMinhMain>().OnEnable();
		}
		else if (suaThongBaoLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
		{
			MessagePopup.Create(suaThongBaoLienMinhResponse.ErrorMessage);
			EGDebug.LogWarning(suaThongBaoLienMinhResponse.ErrorMessage);
		}
		else
		{
			string text = string.Format("ErrorCode: {0}, Message: {1}", suaThongBaoLienMinhResponse.ErrorCode, suaThongBaoLienMinhResponse.ErrorMessage);
			MessagePopup.Create(text);
			EGDebug.LogWarning(text);
		}
		return true;
	}

	public void RequestSendLienMinhChat(SendChatLienMinhRequest request)
	{
		SendRequest(m_C2SProxy.RequestSendChatLienMinh, JsonMapper.ToJson(request, false), false);
	}

	public bool OnSendLienMinhChatResponse(HostID remote, RmiContext rmiContext, string data)
	{
		SendChatLienMinhResponse sendChatLienMinhResponse = JsonMapper.ToObject<SendChatLienMinhResponse>(data);
		if (sendChatLienMinhResponse != null)
		{
			if (sendChatLienMinhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenChat)
				{
					ScreenChat screenChat = GUIManager.getScreen(GAME_SCREEN.ScreenChat) as ScreenChat;
					screenChat.addItemToChatLienMinhList(sendChatLienMinhResponse.NewItem);
				}
				else if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenLienMinhMain)
				{
					ScreenLienMinhMain screenLienMinhMain = GUIManager.getScreen(GAME_SCREEN.ScreenLienMinhMain) as ScreenLienMinhMain;
					screenLienMinhMain.addItemToChatLienMinhList(sendChatLienMinhResponse.NewItem);
				}
				else if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenThanhChien)
				{
					ScreenThanhChien screenThanhChien = GUIManager.getScreen(GAME_SCREEN.ScreenThanhChien) as ScreenThanhChien;
					screenThanhChien.addItemToChatLienMinhList(sendChatLienMinhResponse.NewItem);
				}
				else if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenChatLanhDia)
				{
					ScreenChat screenChat2 = GUIManager.getScreen(GAME_SCREEN.ScreenChatLanhDia) as ScreenChat;
					screenChat2.addItemToChatLienMinhList(sendChatLienMinhResponse.NewItem);
				}
				else
				{
					string key = "thongbaoChat_value" + GameManager.instance.m_GameClient.UserInfo.Gamer.ID;
					if (PlayerPrefs.GetInt(key, 1) == 1 && GUIManager.instance.CurrentScreen != GAME_SCREEN.ScreenSelectFirstDeTu && GUIManager.instance.CurrentScreen != GAME_SCREEN.ScreenBattle)
					{
						PopUpNewMess.Create(sendChatLienMinhResponse.NewItem);
					}
				}
			}
			else if (sendChatLienMinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(sendChatLienMinhResponse.ErrorMessage);
				EGDebug.LogWarning(sendChatLienMinhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", sendChatLienMinhResponse.ErrorCode, sendChatLienMinhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSendLienMinhChatResponse : response null");
		}
		return true;
	}

	public void RequestChatLienMinhInfo(ChatLienMinhInfoRequest request)
	{
		SendRequest(m_C2SProxy.RequestChatLienMinhInfo, JsonMapper.ToJson(request, false));
	}

	public bool OnChatLienMinhInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ChatLienMinhInfoResponse chatLienMinhInfoResponse = JsonMapper.ToObject<ChatLienMinhInfoResponse>(data);
		if (chatLienMinhInfoResponse != null)
		{
			if (chatLienMinhInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenChat)
				{
					ScreenChat screenChat = GUIManager.getScreen(GAME_SCREEN.ScreenChat) as ScreenChat;
					screenChat.displayListChatLienMinhItem(chatLienMinhInfoResponse.listChat);
				}
				else if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenLienMinhMain)
				{
					ScreenLienMinhMain screenLienMinhMain = GUIManager.getScreen(GAME_SCREEN.ScreenLienMinhMain) as ScreenLienMinhMain;
					screenLienMinhMain.displayListChatLienMinhItem(chatLienMinhInfoResponse.listChat);
				}
				else if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenThanhChien)
				{
					ScreenThanhChien screenThanhChien = GUIManager.getScreen(GAME_SCREEN.ScreenThanhChien) as ScreenThanhChien;
					screenThanhChien.displayListChatLienMinhItem(chatLienMinhInfoResponse.listChat);
				}
				else if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenChatLanhDia)
				{
					ScreenChat screenChat2 = GUIManager.getScreen(GAME_SCREEN.ScreenChatLanhDia) as ScreenChat;
					screenChat2.displayListChatLienMinhItem(chatLienMinhInfoResponse.listChat);
				}
			}
			else if (chatLienMinhInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(chatLienMinhInfoResponse.ErrorMessage);
				EGDebug.LogWarning(chatLienMinhInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", chatLienMinhInfoResponse.ErrorCode, chatLienMinhInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnChatLienMinhInfoResponse : response null");
		}
		return true;
	}

	public void RequestCardPayment(CardPaymentRequest request)
	{
		SendRequest(m_C2SProxy.RequestCardPayment, JsonMapper.ToJson(request, false));
	}

	public void RequestLienDauData()
	{
		SendRequest(m_C2SProxy.RequestLienDauData, string.Empty);
	}

	public bool OnLienDauDataResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		LienDauInfoResponse lienDauInfoResponse = JsonMapper.ToObject<LienDauInfoResponse>(data);
		if (lienDauInfoResponse != null)
		{
			if (lienDauInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				ScreenLeagueMain screenLeagueMain = GUIManager.getScreen(GAME_SCREEN.ScreenLeagueMain) as ScreenLeagueMain;
				if (lienDauInfoResponse.SieuCupInfo != null && lienDauInfoResponse.SieuCupInfo.ErrorCode == ERROR_CODE.OK)
				{
					screenLeagueMain.sieuCupResponse = lienDauInfoResponse.SieuCupInfo;
				}
				if (lienDauInfoResponse.LeagueInfo != null && lienDauInfoResponse.LeagueInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(lienDauInfoResponse.LeagueInfo.userInfo);
					screenLeagueMain.leagueDataResponse = lienDauInfoResponse.LeagueInfo;
				}
				GUIManager.setScreen(GAME_SCREEN.ScreenLeagueMain);
				screenLeagueMain.SyncWithNetworkData(lienDauInfoResponse.SieuCupInfo);
			}
			else if (lienDauInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(lienDauInfoResponse.ErrorMessage);
				EGDebug.LogWarning(lienDauInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", lienDauInfoResponse.ErrorCode, lienDauInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnLienDauDataResponse : response null");
		}
		return true;
	}

	public void RequestSieuCupChampion()
	{
		SendRequest(m_C2SProxy.RequestSieuCupChampion, string.Empty, false);
	}

	public bool OnSieuCupChampionResponse(HostID remote, RmiContext rmiContext, string data)
	{
		SieuCupChampionInfo sieuCupChampionInfo = JsonMapper.ToObject<SieuCupChampionInfo>(data);
		if (sieuCupChampionInfo != null)
		{
			if (sieuCupChampionInfo.ErrorCode == ERROR_CODE.OK)
			{
				SieuCupChampionResponse = sieuCupChampionInfo;
				if (GUIManager.instance.homeCity != null)
				{
					GUIManager.instance.homeCity.SyncWithSieuCupData(sieuCupChampionInfo.Champion);
				}
			}
			else if (sieuCupChampionInfo.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(sieuCupChampionInfo.ErrorMessage);
				EGDebug.LogWarning(sieuCupChampionInfo.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", sieuCupChampionInfo.ErrorCode, sieuCupChampionInfo.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSieuCupChampionResponse : response null");
		}
		return true;
	}

	public void RequestThamBaiSieuCup()
	{
		SendRequest(m_C2SProxy.RequestThamBaiSieuCup, string.Empty);
	}

	public bool OnThamBaiSieuCup(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("ThamBaiPhanThuongPopup"), Localization.instance.Get("ThamBaiPhanThuongDesc"), phanThuongResponse);
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnThamBaiSieuCup : response null");
		}
		return true;
	}

	public void RequestGetSieuCupData()
	{
		SendRequest(m_C2SProxy.RequestGetSieuCupData, string.Empty);
	}

	public bool OnGetSieuCupData(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetSieuCupInfoResponse getSieuCupInfoResponse = JsonMapper.ToObject<GetSieuCupInfoResponse>(data);
		if (getSieuCupInfoResponse != null)
		{
			if (getSieuCupInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				ScreenLeagueMain screenLeagueMain = GUIManager.getScreen(GAME_SCREEN.ScreenLeagueMain) as ScreenLeagueMain;
				screenLeagueMain.sieuCupResponse = getSieuCupInfoResponse;
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenLeagueMain)
				{
					screenLeagueMain.CupView.GetComponent<SuperCupView>().SetInfo(getSieuCupInfoResponse);
					screenLeagueMain.SyncWithNetworkData(getSieuCupInfoResponse);
				}
				if (GUIManager.instance.homeCity != null)
				{
					GUIManager.instance.homeCity.SyncWithSieuCupData(getSieuCupInfoResponse.Champion);
				}
			}
			else if (getSieuCupInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getSieuCupInfoResponse.ErrorMessage);
				EGDebug.LogWarning(getSieuCupInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getSieuCupInfoResponse.ErrorCode, getSieuCupInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetSieuCupData : response null");
		}
		return true;
	}

	public void RequestGetSieuCupBattle(int matchID)
	{
		GetSieuCupBattleRequest getSieuCupBattleRequest = new GetSieuCupBattleRequest();
		getSieuCupBattleRequest.TranDauID = matchID;
		SendRequest(m_C2SProxy.RequestGetSieuCupBattle, JsonMapper.ToJson(getSieuCupBattleRequest, false));
	}

	public bool OnGetSieuCupBattle(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetSieuCupBattleResponse getSieuCupBattleResponse = JsonMapper.ToObject<GetSieuCupBattleResponse>(data);
		if (getSieuCupBattleResponse != null)
		{
			if (getSieuCupBattleResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (getSieuCupBattleResponse.Battle != null)
				{
					ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
					if (screenBattle != null)
					{
						GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
						screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenLeagueMain;
						screenBattle.Replay(getSieuCupBattleResponse.Battle, "BM_LANG_MO_HOANG_DE");
					}
				}
			}
			else if (getSieuCupBattleResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getSieuCupBattleResponse.ErrorMessage);
				EGDebug.LogWarning(getSieuCupBattleResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getSieuCupBattleResponse.ErrorCode, getSieuCupBattleResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetSieuCupBattle : response null");
		}
		return true;
	}

	public void RequestSieuCupDatCuoc(int matchID, int choice)
	{
		SieuCupDatCuocRequest sieuCupDatCuocRequest = new SieuCupDatCuocRequest();
		sieuCupDatCuocRequest.TranDauID = matchID;
		sieuCupDatCuocRequest.Choice = choice;
		SendRequest(m_C2SProxy.RequestSieuCupDatCuoc, JsonMapper.ToJson(sieuCupDatCuocRequest, false));
	}

	public bool OnSieuCupDatCuoc(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetSieuCupInfoResponse getSieuCupInfoResponse = JsonMapper.ToObject<GetSieuCupInfoResponse>(data);
		if (getSieuCupInfoResponse != null)
		{
			if (getSieuCupInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (getSieuCupInfoResponse.UpdateUserInfo != null && getSieuCupInfoResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(getSieuCupInfoResponse.UpdateUserInfo);
				}
				ScreenLeagueMain screenLeagueMain = GUIManager.getScreen(GAME_SCREEN.ScreenLeagueMain) as ScreenLeagueMain;
				screenLeagueMain.sieuCupResponse = getSieuCupInfoResponse;
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenLeagueMain)
				{
					screenLeagueMain.GoToCup();
					screenLeagueMain.CupView.GetComponent<SuperCupView>().SetInfo(getSieuCupInfoResponse);
				}
				MessagePopup.Create(Localization.instance.Get("DatCuocSucceedMsg"));
			}
			else if (getSieuCupInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getSieuCupInfoResponse.ErrorMessage);
				EGDebug.LogWarning(getSieuCupInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getSieuCupInfoResponse.ErrorCode, getSieuCupInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSieuCupDatCuoc : response null");
		}
		return true;
	}

	public void RequestBanhChungGetNguyenLieu()
	{
		SendRequest(m_C2SProxy.RequestBanhChungNhatNguyenLieu, string.Empty);
	}

	public bool OnBanhChungGetNguyenLieu(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("NgLieuBanhChungTitle"), Localization.instance.Get("NgLieuBanhChungDesc"), phanThuongResponse);
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBanhChungGetNguyenLieu : response null");
		}
		return true;
	}

	public void RequestBanhChungNhanThuong(int lvl)
	{
		SendRequest(m_C2SProxy.RequestBanhChungGetPhanThuong, lvl.ToString());
	}

	public bool OnBanhChungNhanThuongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTopBanhChungTitle"), Localization.instance.Get("PhanThuongDesc"), phanThuongResponse);
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBanhChungNhanThuongResponse : response null");
		}
		return true;
	}

	public void RequestBanhChungInfo()
	{
		SendRequest(m_C2SProxy.RequestBanhChungGetInfo, string.Empty);
	}

	public bool OnBanhChungInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		BanhChungInfoResponse banhChungInfoResponse = JsonMapper.ToObject<BanhChungInfoResponse>(data);
		if (banhChungInfoResponse != null)
		{
			if (banhChungInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				ScreenBanhChung screenBanhChung = GUIManager.getScreen(GAME_SCREEN.ScreenBanhChung) as ScreenBanhChung;
				screenBanhChung.SyncWithNetworkData(banhChungInfoResponse);
			}
			else if (banhChungInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(banhChungInfoResponse.ErrorMessage);
				EGDebug.LogWarning(banhChungInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", banhChungInfoResponse.ErrorCode, banhChungInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBanhChungInfoResponse : response null");
		}
		return true;
	}

	public void RequestBanhChungGetBXH()
	{
		SendRequest(m_C2SProxy.RequestBanhChungBXH, string.Empty);
	}

	public bool OnBanhChungGetBXH(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		BanhChungBXHResponse banhChungBXHResponse = JsonMapper.ToObject<BanhChungBXHResponse>(data);
		if (banhChungBXHResponse != null)
		{
			if (banhChungBXHResponse.ErrorCode == ERROR_CODE.OK)
			{
				GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBXHBanhChung);
				ScreenBXHBanhChung screenBXHBanhChung = GUIManager.getScreen(GAME_SCREEN.ScreenBXHBanhChung) as ScreenBXHBanhChung;
				screenBXHBanhChung.SyncWithNetworkData(banhChungBXHResponse);
			}
			else if (banhChungBXHResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(banhChungBXHResponse.ErrorMessage);
				EGDebug.LogWarning(banhChungBXHResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", banhChungBXHResponse.ErrorCode, banhChungBXHResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBanhChungGetBXH : response null");
		}
		return true;
	}

	public void RequestBanhChungNauBanh()
	{
		SendRequest(C2SProxy.RequestBanhChungNauBanh, string.Empty);
	}

	public bool OnBanhChungNauBanh(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("NauBanhSuccess"), Localization.instance.Get("NauBanhDesc"), phanThuongResponse);
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBanhChungNauBanh : response null");
		}
		return true;
	}

	public void RequestBanhChungPlayerMove(float x, float y, float z)
	{
		m_C2SProxy.RequestBanhChungPlayerMove(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(new HomeResponse.Position3D(x, y, z)));
	}

	public void RequestBanhChungGetOthers()
	{
		m_C2SProxy.RequestBanhChungGetOthers(HostID.Server, RmiContext.ReliableSend);
	}

	public bool OnBanhChungOthers(HostID remote, RmiContext rmiContext, string data)
	{
		BanhChungOthersResponse banhChungOthersResponse = JsonMapper.ToObject<BanhChungOthersResponse>(data);
		if (banhChungOthersResponse != null)
		{
			if (banhChungOthersResponse.ErrorCode == ERROR_CODE.OK)
			{
				ScreenBanhChung screenBanhChung = GUIManager.getScreen(GAME_SCREEN.ScreenBanhChung) as ScreenBanhChung;
				screenBanhChung.SyncWithNetworkData(banhChungOthersResponse);
			}
			else if (banhChungOthersResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(banhChungOthersResponse.ErrorMessage);
				EGDebug.LogWarning(banhChungOthersResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", banhChungOthersResponse.ErrorCode, banhChungOthersResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBanhChungOthers : response null");
		}
		return true;
	}

	public void RequestBangChienGetPhanThuongThuThanh(int thanhIdx)
	{
		m_C2SProxy.RequestBangChienGetPhanThuongThuThanh(HostID.Server, RmiContext.ReliableSend, thanhIdx);
	}

	public bool OnBangChienKetThucResponse(HostID remote, RmiContext rmiContext, string data)
	{
		BangChienKetThucResponse bangChienKetThucResponse = JsonMapper.ToObject<BangChienKetThucResponse>(data);
		if (bangChienKetThucResponse != null)
		{
			if (bangChienKetThucResponse.ErrorCode == ERROR_CODE.OK)
			{
				PopupKetThucBangChien.Create(bangChienKetThucResponse.ThanhCong, bangChienKetThucResponse.ResultMessage);
				GUIManager.setScreen(GAME_SCREEN.ScreenBangChien);
				ScreenBangChien screenBangChien = GUIManager.getScreen(GAME_SCREEN.ScreenBangChien) as ScreenBangChien;
				screenBangChien.SyncWithNetWorkData(bangChienKetThucResponse.BangChienInfo);
			}
			else if (bangChienKetThucResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(bangChienKetThucResponse.ErrorMessage);
				EGDebug.LogWarning(bangChienKetThucResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", bangChienKetThucResponse.ErrorCode, bangChienKetThucResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBangChienKetThucResponse : response null");
		}
		return true;
	}

	public bool OnBangChienGetPhanThuongThuThanh(HostID remote, RmiContext rmiContext, string data)
	{
		BangChienPhanThuong bangChienPhanThuong = JsonMapper.ToObject<BangChienPhanThuong>(data);
		ScreenThanhChien screenThanhChien = GUIManager.getScreen(GAME_SCREEN.ScreenThanhChien) as ScreenThanhChien;
		screenThanhChien.ThanhChienObj.mainPlayer.dangLayPhanThuong = false;
		if (bangChienPhanThuong != null)
		{
			if (bangChienPhanThuong.ErrorCode == ERROR_CODE.OK)
			{
				screenThanhChien.SyncWithNetworkData(bangChienPhanThuong);
				if (bangChienPhanThuong.PhanThuong.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(bangChienPhanThuong.PhanThuong.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenThanhChien)
				{
					PopupDanhSachPhanThuong popupDanhSachPhanThuong = PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), string.Format(Localization.instance.Get("BangChienPhanThuongDesc"), bangChienPhanThuong.DiemCongHien), bangChienPhanThuong.PhanThuong);
				}
				if (bangChienPhanThuong.Player != null)
				{
					screenThanhChien.ThanhChienObj.mainPlayer.playerInfo = bangChienPhanThuong.Player;
					screenThanhChien.playerInfo = bangChienPhanThuong.Player;
				}
			}
			else if (bangChienPhanThuong.ErrorCode == ERROR_CODE.MAT_DONG_BO)
			{
				if (bangChienPhanThuong.Player != null)
				{
					screenThanhChien.ThanhChienObj.mainPlayer.playerInfo = bangChienPhanThuong.Player;
					screenThanhChien.playerInfo = bangChienPhanThuong.Player;
				}
				EGDebug.LogWarning(bangChienPhanThuong.ErrorMessage);
			}
			else if (bangChienPhanThuong.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				if (bangChienPhanThuong.Player != null)
				{
					screenThanhChien.ThanhChienObj.mainPlayer.playerInfo = bangChienPhanThuong.Player;
				}
				MessagePopup.Create(bangChienPhanThuong.ErrorMessage);
				EGDebug.LogWarning(bangChienPhanThuong.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", bangChienPhanThuong.ErrorCode, bangChienPhanThuong.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBangChienGetPhanThuongThuThanh : response null");
		}
		return true;
	}

	public void RequestBangChienCongThanh(int thanhIdx, int gate)
	{
		BangChienCongThanhRequest bangChienCongThanhRequest = new BangChienCongThanhRequest();
		bangChienCongThanhRequest.Gate = gate;
		bangChienCongThanhRequest.ThanhIdx = thanhIdx;
		SendRequest(m_C2SProxy.RequestBangChienCongThanh, JsonMapper.ToJson(bangChienCongThanhRequest, false));
	}

	public bool OnBangChienCongThanh(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		BangChienCongThanhResponse bangChienCongThanhResponse = JsonMapper.ToObject<BangChienCongThanhResponse>(data);
		if (bangChienCongThanhResponse != null)
		{
			if (bangChienCongThanhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenThanhChien)
				{
					ScreenThanhChien screenThanhChien = GUIManager.getScreen(GAME_SCREEN.ScreenThanhChien) as ScreenThanhChien;
					screenThanhChien.SyncWithNetworkData(bangChienCongThanhResponse);
					if (screenThanhChien.IsThuThanh())
					{
						StartCoroutine(screenThanhChien.EnemyRunOnWall(bangChienCongThanhResponse));
					}
					else
					{
						screenThanhChien.PlayBattle(bangChienCongThanhResponse);
					}
				}
				else if (bangChienCongThanhResponse.PhanThuong != null && bangChienCongThanhResponse.PhanThuong.PhanThuong.UpdateUserInfo != null)
				{
					GameManager.instance.m_GameClient.UserInfo.UpdateInfo(bangChienCongThanhResponse.PhanThuong.PhanThuong.UpdateUserInfo);
				}
			}
			else if (bangChienCongThanhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(bangChienCongThanhResponse.ErrorMessage);
				EGDebug.LogWarning(bangChienCongThanhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", bangChienCongThanhResponse.ErrorCode, bangChienCongThanhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBangChienCongThanh : response null");
		}
		return true;
	}

	public void RequestBangChienDenCongThanh(int thanhIdx, int gate, bool visible = true)
	{
		BangChienCongThanhRequest bangChienCongThanhRequest = new BangChienCongThanhRequest();
		bangChienCongThanhRequest.Gate = gate;
		bangChienCongThanhRequest.ThanhIdx = thanhIdx;
		SendRequest(m_C2SProxy.RequestBangChienDenCongThanh, JsonMapper.ToJson(bangChienCongThanhRequest, false), visible);
	}

	public bool OnBangChienDenCongThanh(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		BangChienVaoThanhResponse bangChienVaoThanhResponse = JsonMapper.ToObject<BangChienVaoThanhResponse>(data);
		if (bangChienVaoThanhResponse != null)
		{
			if (bangChienVaoThanhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (bangChienVaoThanhResponse.PhanThuong != null)
				{
					ScreenThanhChien screenThanhChien = GUIManager.getScreen(GAME_SCREEN.ScreenThanhChien) as ScreenThanhChien;
					screenThanhChien.SyncWithNetworkData(bangChienVaoThanhResponse);
					if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenThanhChien)
					{
						PopupDanhSachPhanThuong popupDanhSachPhanThuong = PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), string.Format(Localization.instance.Get("BangChienPhanThuongDesc"), bangChienVaoThanhResponse.PhanThuong.DiemCongHien), bangChienVaoThanhResponse.PhanThuong.PhanThuong);
					}
					if (bangChienVaoThanhResponse.PhanThuong.PhanThuong.UpdateUserInfo != null)
					{
						UserInfo.UpdateInfo(bangChienVaoThanhResponse.PhanThuong.PhanThuong.UpdateUserInfo);
					}
				}
			}
			else if (bangChienVaoThanhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(bangChienVaoThanhResponse.ErrorMessage);
				EGDebug.LogWarning(bangChienVaoThanhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", bangChienVaoThanhResponse.ErrorCode, bangChienVaoThanhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBangChienDenCongThanh : response null");
		}
		return true;
	}

	public void RequestBangChienMove(int thanhIdx, float x, float y, float z)
	{
		m_C2SProxy.RequestBangChienUserMove(HostID.Server, RmiContext.ReliableSend, thanhIdx, x, y, z);
	}

	public void RequestBangChienGetOtherUser(int thanhIdx)
	{
		m_C2SProxy.RequestBangChienOtherUser(HostID.Server, RmiContext.ReliableSend, thanhIdx);
	}

	public bool OnBangChienListOtherUser(HostID remote, RmiContext rmiContext, string data)
	{
		BangChienListPlayerResponse bangChienListPlayerResponse = JsonMapper.ToObject<BangChienListPlayerResponse>(data);
		if (bangChienListPlayerResponse != null)
		{
			if (bangChienListPlayerResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenThanhChien)
				{
					ScreenThanhChien screenThanhChien = GUIManager.getScreen(GAME_SCREEN.ScreenThanhChien) as ScreenThanhChien;
					screenThanhChien.SyncWithNetworkData(bangChienListPlayerResponse);
					if (screenThanhChien.ThanhChienObj != null)
					{
						screenThanhChien.ThanhChienObj.SpawnPlayers(bangChienListPlayerResponse.ListOtherPlayer);
					}
				}
			}
			else if (bangChienListPlayerResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(bangChienListPlayerResponse.ErrorMessage);
				EGDebug.LogWarning(bangChienListPlayerResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", bangChienListPlayerResponse.ErrorCode, bangChienListPlayerResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBangChienListOtherUser : response null");
		}
		return true;
	}

	public void RequestBangChienVaoThanh(int thanhIdx)
	{
		BangChienVaoThanhRequest bangChienVaoThanhRequest = new BangChienVaoThanhRequest();
		bangChienVaoThanhRequest.ThanhIdx = thanhIdx;
		SendRequest(m_C2SProxy.RequestBangChienVaoThanh, JsonMapper.ToJson(bangChienVaoThanhRequest, false));
	}

	public bool OnBangChienVaoThanhResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		BangChienVaoThanhResponse bangChienVaoThanhResponse = JsonMapper.ToObject<BangChienVaoThanhResponse>(data);
		if (bangChienVaoThanhResponse != null)
		{
			if (bangChienVaoThanhResponse.ErrorCode == ERROR_CODE.OK)
			{
				GUIManager.instance.SetScreen(GAME_SCREEN.ScreenThanhChien);
				ScreenThanhChien screenThanhChien = GUIManager.getScreen(GAME_SCREEN.ScreenThanhChien) as ScreenThanhChien;
				UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
				int avatarID = userInfo.DoiHinh.ListRaTran.Find((int e) => e > 0);
				string text = userInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == avatarID).Name;
				string displayName = userInfo.Gamer.DisplayName;
				screenThanhChien.ThanhIdx = bangChienVaoThanhResponse.ThanhChienObj.ThanhIdx;
				UnityEngine.Vector3 vector = default(UnityEngine.Vector3);
				vector = new UnityEngine.Vector3(bangChienVaoThanhResponse.Player.X, bangChienVaoThanhResponse.Player.Y, bangChienVaoThanhResponse.Player.Z);
				screenThanhChien.ThanhChienObj.SetActiveCam(bangChienVaoThanhResponse.Player.Def <= 0);
				if (screenThanhChien.playerInfo == null)
				{
					screenThanhChien.ThanhChienObj.SpawnPlayerAvatar(bangChienVaoThanhResponse.Player);
				}
				else if (screenThanhChien.ThanhChienObj.mainPlayer != null)
				{
					screenThanhChien.ThanhChienObj.mainPlayer.LocalTeleport(new UnityEngine.Vector3(bangChienVaoThanhResponse.Player.X, bangChienVaoThanhResponse.Player.Y, bangChienVaoThanhResponse.Player.Z));
					screenThanhChien.ThanhChienObj.mainPlayer.playerInfo = bangChienVaoThanhResponse.Player;
				}
				screenThanhChien.ThanhChienObj.SpawnPlayers(bangChienVaoThanhResponse.ListNguoiChoi);
				screenThanhChien.playerInfo = bangChienVaoThanhResponse.Player;
				screenThanhChien.SyncWithNetworkData(bangChienVaoThanhResponse);
			}
			else if (bangChienVaoThanhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(bangChienVaoThanhResponse.ErrorMessage);
				EGDebug.LogWarning(bangChienVaoThanhResponse.ErrorMessage);
			}
			else
			{
				string text2 = string.Format("ErrorCode: {0}, Message: {1}", bangChienVaoThanhResponse.ErrorCode, bangChienVaoThanhResponse.ErrorMessage);
				MessagePopup.Create(text2);
				EGDebug.LogWarning(text2);
			}
		}
		else
		{
			EGDebug.LogError("OnBangChienVaoThanhResponse : response null");
		}
		return true;
	}

	public void RequestBangChienGetInfo(bool visible = true)
	{
		SendRequest(m_C2SProxy.RequestBangChienGetInfo, string.Empty, visible);
	}

	public bool OnBangChienGetInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		BangChienResponse bangChienResponse = JsonMapper.ToObject<BangChienResponse>(data);
		if (bangChienResponse != null)
		{
			if (bangChienResponse.ErrorCode == ERROR_CODE.OK)
			{
				ScreenBangChien screenBangChien = GUIManager.getScreen(GAME_SCREEN.ScreenBangChien) as ScreenBangChien;
				if (screenBangChien != null)
				{
					screenBangChien.SyncWithNetWorkData(bangChienResponse);
				}
				ScreenHoatDongLienMinh screenHoatDongLienMinh = GUIManager.getScreen(GAME_SCREEN.ScreenHoatDongLienMinh) as ScreenHoatDongLienMinh;
				screenHoatDongLienMinh.SyncNetworkData(bangChienResponse);
				ScreenThanhChien screenThanhChien = GUIManager.getScreen(GAME_SCREEN.ScreenThanhChien) as ScreenThanhChien;
				screenThanhChien.SyncWithNetworkData(bangChienResponse);
			}
			else if (bangChienResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(bangChienResponse.ErrorMessage);
				EGDebug.LogWarning(bangChienResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", bangChienResponse.ErrorCode, bangChienResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBangChienGetInfoResponse : response null");
		}
		return true;
	}

	public void RequestLapNguyenKhi(LapNguyenKhiRequest request)
	{
		SendRequest(m_C2SProxy.RequestLapNguyenKhi, JsonMapper.ToJson(request, false));
	}

	public bool OnLapNguyenKhiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		LapNguyenKhiResponse lapNguyenKhiResponse = JsonMapper.ToObject<LapNguyenKhiResponse>(data);
		if (lapNguyenKhiResponse != null)
		{
			if (lapNguyenKhiResponse.ErrorCode == ERROR_CODE.OK)
			{
				GameManager.instance.m_GameClient.UserInfo.UpdateInfo(lapNguyenKhiResponse.updateUserInfo);
				UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData h) => h.Name == "NV_QUACH_TUONG");
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
				{
					ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
					screenDoiHinh.SyncWithNetworkData();
				}
			}
			else if (lapNguyenKhiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(lapNguyenKhiResponse.ErrorMessage);
				EGDebug.LogWarning(lapNguyenKhiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", lapNguyenKhiResponse.ErrorCode, lapNguyenKhiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnLapNguyenKhiResponse : response null");
		}
		return true;
	}

	public void RequestThaoNguyenKhi(LapNguyenKhiRequest request)
	{
		SendRequest(m_C2SProxy.RequestThaoNguyenKhi, JsonMapper.ToJson(request, false));
	}

	public bool OnThaoNguyenKhiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThaoNguyenKhiResponse thaoNguyenKhiResponse = JsonMapper.ToObject<ThaoNguyenKhiResponse>(data);
		if (thaoNguyenKhiResponse != null)
		{
			if (thaoNguyenKhiResponse.ErrorCode == ERROR_CODE.OK)
			{
				GameManager.instance.m_GameClient.UserInfo.UpdateInfo(thaoNguyenKhiResponse.updateUserInfo);
			}
			else if (thaoNguyenKhiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thaoNguyenKhiResponse.ErrorMessage);
				EGDebug.LogWarning(thaoNguyenKhiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thaoNguyenKhiResponse.ErrorCode, thaoNguyenKhiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnThaoNguyenKhiResponse : response null");
		}
		return true;
	}

	public void RequestGetQMDInfo()
	{
		SendRequest(m_C2SProxy.RequestQMDInfo, string.Empty);
	}

	public bool OnGetQMDInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		QMDInfoResponse qMDInfoResponse = JsonMapper.ToObject<QMDInfoResponse>(data);
		if (qMDInfoResponse != null)
		{
			if (qMDInfoResponse.ErrorCode == ERROR_CODE.OK || qMDInfoResponse.ErrorCode == ERROR_CODE.MAT_DONG_BO)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenQuangMinhDinh)
				{
					ScreenQuangMinhDinh screenQuangMinhDinh = GUIManager.getScreen(GAME_SCREEN.ScreenQuangMinhDinh) as ScreenQuangMinhDinh;
					screenQuangMinhDinh.SyncCongPhaWithNetworkData(qMDInfoResponse);
				}
			}
			else if (qMDInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(qMDInfoResponse.ErrorMessage);
				EGDebug.LogWarning(qMDInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", qMDInfoResponse.ErrorCode, qMDInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetQMDInfo : response null");
		}
		return true;
	}

	public void RequestQMDSelect(QMDSelectRequest request)
	{
		SendRequest(m_C2SProxy.RequestQMDSelect, JsonMapper.ToJson(request, false));
	}

	public bool OnQMDSelectResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		QMDInfoResponse qMDInfoResponse = JsonMapper.ToObject<QMDInfoResponse>(data);
		if (qMDInfoResponse != null)
		{
			if (qMDInfoResponse.ErrorCode == ERROR_CODE.OK || qMDInfoResponse.ErrorCode == ERROR_CODE.MAT_DONG_BO)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenQuangMinhDinh)
				{
					ScreenQuangMinhDinh screenQuangMinhDinh = GUIManager.getScreen(GAME_SCREEN.ScreenQuangMinhDinh) as ScreenQuangMinhDinh;
					screenQuangMinhDinh.SyncCongPhaWithNetworkData(qMDInfoResponse);
				}
			}
			else if (qMDInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(qMDInfoResponse.ErrorMessage);
				EGDebug.LogWarning(qMDInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", qMDInfoResponse.ErrorCode, qMDInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetQMDInfo : response null");
		}
		return true;
	}

	public void RequestQMDSelect(QMDTranHinhRequest request)
	{
		SendRequest(m_C2SProxy.RequestQMDTranHinhChienThuat, JsonMapper.ToJson(request, false));
	}

	public void RequestQMDGetChiTietNPC()
	{
		SendRequest(m_C2SProxy.RequestQMDGetChiTietNPC, string.Empty);
	}

	public bool OnQMDGetChiTietNPCResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UserInfo userInfo = JsonMapper.ToObject<UserInfo>(data);
		if (userInfo != null)
		{
			if (userInfo.ErrorCode == ERROR_CODE.OK)
			{
				ScreenDoiHinh screenDoiHinh = (ScreenDoiHinh)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenDoiHinh);
				screenDoiHinh.ShowAnotherUserInfo(userInfo);
			}
			else if (userInfo.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(userInfo.ErrorMessage);
				EGDebug.LogWarning(userInfo.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", userInfo.ErrorCode, userInfo.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnQMDGetChiTietNPCResponse : response null");
		}
		return true;
	}

	public void RequestQMDGetBXH()
	{
		SendRequest(m_C2SProxy.RequestQMDGetBXH, string.Empty);
	}

	public bool OnQMDGetBXHResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		CT2BXHTuanResponse cT2BXHTuanResponse = JsonMapper.ToObject<CT2BXHTuanResponse>(data);
		if (cT2BXHTuanResponse != null)
		{
			if (cT2BXHTuanResponse.ErrorCode == ERROR_CODE.OK)
			{
				PopupBXHQuangMinhDinh.Create(cT2BXHTuanResponse);
			}
			else if (cT2BXHTuanResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(cT2BXHTuanResponse.ErrorMessage);
				MessagePopup.Create(cT2BXHTuanResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ERROR {0} - Message: {1}", cT2BXHTuanResponse.ErrorCode, cT2BXHTuanResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "QMDGetBXH : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestQMDXongPha()
	{
		SendRequest(m_C2SProxy.RequestQMDXongPha, string.Empty);
	}

	public bool OnQMDXongPhaResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		TyThiResponse tyThiResponse = JsonMapper.ToObject<TyThiResponse>(data);
		if (tyThiResponse != null)
		{
			if (tyThiResponse.ErrorCode == ERROR_CODE.OK)
			{
				ScreenQuangMinhDinh screenQuangMinhDinh = GUIManager.getScreen(GAME_SCREEN.ScreenQuangMinhDinh) as ScreenQuangMinhDinh;
				if (tyThiResponse.Battle != null)
				{
					List<UserInfo.HeroData> listCloneHeroFromDoiHinh = UserInfo.GetListCloneHeroFromDoiHinh();
					PopupBattleResult popupBattleResult = PopupBattleResult.Create(tyThiResponse.Battle, listCloneHeroFromDoiHinh, 0L, 0L, 0L, -1);
					popupBattleResult.gameObject.SetActive(false);
					popupBattleResult.OnClosePopup = screenQuangMinhDinh.OnCloseBattleResult;
				}
				if (tyThiResponse.PhanThuong != null && tyThiResponse.PhanThuong.UpdateUserInfo != null && tyThiResponse.PhanThuong.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(tyThiResponse.PhanThuong.UpdateUserInfo);
				}
				if (tyThiResponse.Battle != null)
				{
					ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
					if (screenBattle != null)
					{
						screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenQuangMinhDinh;
					}
					screenQuangMinhDinh.BackToXongPha = true;
					GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
					if (tyThiResponse.PhanThuong != null && tyThiResponse.PhanThuong.PhanThuongList.Count > 0)
					{
						PopupDanhSachPhanThuong popupDanhSachPhanThuong = PopupDanhSachPhanThuong.Create(Localization.instance.Get("XongPhaQuangMinhDinh"), Localization.instance.Get("PhanThuongDesc"), tyThiResponse.PhanThuong);
						popupDanhSachPhanThuong.gameObject.SetActive(false);
					}
					screenBattle.Replay(tyThiResponse.Battle, "BM_MINH_GIAO");
					screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenQuangMinhDinh;
				}
				else if (tyThiResponse.PhanThuong != null && tyThiResponse.PhanThuong.PhanThuongList.Count > 0)
				{
					PopupDanhSachPhanThuong.Create(Localization.instance.Get("TyThiFailPhanThuongTitle"), Localization.instance.Get("TyThiFailPhanThuongDesc"), tyThiResponse.PhanThuong);
				}
			}
			else if (tyThiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				EGDebug.Log(tyThiResponse.ErrorMessage);
				MessagePopup.Create(tyThiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("Error: Code = {0}, - Message : {1}", tyThiResponse.ErrorCode, tyThiResponse.ErrorMessage);
				EGDebug.Log(text);
				MessagePopup.Create(text);
			}
		}
		else
		{
			string text2 = "OnReceiveTyThi : response is null";
			EGDebug.Log(text2);
			MessagePopup.Create(text2);
		}
		return true;
	}

	public void RequestBeQuanDeTu(BeQuanRequest request)
	{
		SendRequest(m_C2SProxy.RequestBeQuanDeTu, JsonMapper.ToJson(request, false));
	}

	public bool OnBeQuanDeTuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		BeQuanResponse beQuanResponse = JsonMapper.ToObject<BeQuanResponse>(data);
		if (beQuanResponse != null)
		{
			if (beQuanResponse.ErrorCode == ERROR_CODE.OK || beQuanResponse.ErrorCode == ERROR_CODE.MAT_DONG_BO)
			{
				GameManager.instance.m_GameClient.UserInfo.UpdateInfo(beQuanResponse.updateInfo);
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenQuangMinhDinh)
				{
					ScreenQuangMinhDinh screenQuangMinhDinh = GUIManager.getScreen(GAME_SCREEN.ScreenQuangMinhDinh) as ScreenQuangMinhDinh;
					screenQuangMinhDinh.SyncBequanWithNetworkData();
				}
			}
			else if (beQuanResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(beQuanResponse.ErrorMessage);
				EGDebug.LogWarning(beQuanResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", beQuanResponse.ErrorCode, beQuanResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetQMDInfo : response null");
		}
		return true;
	}

	public void RequestNangCapNguyenKhi(NangCapNguyenKhiRequest request)
	{
		SendRequest(m_C2SProxy.RequestNangCapNguyenKhi, JsonMapper.ToJson(request, false));
	}

	public bool OnNangCapNguyenKhiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		NangCapNguyenKhiResponse nangCapNguyenKhiResponse = JsonMapper.ToObject<NangCapNguyenKhiResponse>(data);
		if (nangCapNguyenKhiResponse != null)
		{
			if (nangCapNguyenKhiResponse.ErrorCode == ERROR_CODE.OK)
			{
				GameManager.instance.m_GameClient.UserInfo.UpdateInfo(nangCapNguyenKhiResponse.updateUserInfo);
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenCuongHoaNguyenKhi)
				{
					ScreenCuongHoaNguyenKhi screenCuongHoaNguyenKhi = GUIManager.getScreen(GAME_SCREEN.ScreenCuongHoaNguyenKhi) as ScreenCuongHoaNguyenKhi;
					screenCuongHoaNguyenKhi.updateView(nangCapNguyenKhiResponse.NguyenKhiID);
				}
			}
			else if (nangCapNguyenKhiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(nangCapNguyenKhiResponse.ErrorMessage);
				EGDebug.LogWarning(nangCapNguyenKhiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", nangCapNguyenKhiResponse.ErrorCode, nangCapNguyenKhiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnNangCapNguyenKhiResponse : response null");
		}
		return true;
	}

	public void RequestMuaNguyenKhi(MuaNguyenKhiRequest request)
	{
		SendRequest(m_C2SProxy.RequestMuaNguyenKhi, JsonMapper.ToJson(request, false));
	}

	public bool OnMuaNguyenKhiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		MuaNguyenKhiResponse muaNguyenKhiResponse = JsonMapper.ToObject<MuaNguyenKhiResponse>(data);
		if (muaNguyenKhiResponse != null)
		{
			if (muaNguyenKhiResponse.ErrorCode == ERROR_CODE.OK)
			{
				GameManager.instance.m_GameClient.UserInfo.UpdateInfo(muaNguyenKhiResponse.updateUserInfo);
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenNguyenKhi)
				{
					ScreenNguyenKhi screenNguyenKhi = GUIManager.getScreen(GAME_SCREEN.ScreenNguyenKhi) as ScreenNguyenKhi;
					screenNguyenKhi.displaySoLuongKiemHon();
					PopupDanhSachPhanThuong.Create(Localization.instance.Get("DoiNguyenKhiDanTitlePopup"), Localization.instance.Get("PhanThuongNhanDuoc"), muaNguyenKhiResponse.phanthuong);
				}
			}
			else if (muaNguyenKhiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(muaNguyenKhiResponse.ErrorMessage);
				EGDebug.LogWarning(muaNguyenKhiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", muaNguyenKhiResponse.ErrorCode, muaNguyenKhiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnMuaNguyenKhiResponse : response null");
		}
		return true;
	}

	public void RequestChonHatGiong(ChonHatGiongRequest request)
	{
		SendRequest(m_C2SProxy.RequestChonHatGiong, JsonMapper.ToJson(request, false));
	}

	public bool OnChonHatGiongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ChonHatGiongResponse chonHatGiongResponse = JsonMapper.ToObject<ChonHatGiongResponse>(data);
		if (chonHatGiongResponse != null)
		{
			if (chonHatGiongResponse.ErrorCode == ERROR_CODE.OK)
			{
				GameManager.instance.m_GameClient.UserInfo.Gamer.CurSeed = chonHatGiongResponse.HatGiongType;
				GameManager.instance.m_GameClient.UserInfo.UpdateInfo(chonHatGiongResponse.updateInfo);
				if (PopupChonHatGiong.instance != null)
				{
					PopupChonHatGiong.instance.Set();
				}
			}
			else if (chonHatGiongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(chonHatGiongResponse.ErrorMessage);
				EGDebug.LogWarning(chonHatGiongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", chonHatGiongResponse.ErrorCode, chonHatGiongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnChonHatGiongResponse : response null");
		}
		return true;
	}

	public void RequestLayHatGiong(LayHatGiongRequest request)
	{
		SendRequest(m_C2SProxy.RequestLayHatGiong, JsonMapper.ToJson(request, false));
	}

	public bool OnLayHatGiongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		LayHatGiongResponse layHatGiongResponse = JsonMapper.ToObject<LayHatGiongResponse>(data);
		if (layHatGiongResponse != null)
		{
			if (layHatGiongResponse.ErrorCode == ERROR_CODE.OK)
			{
				GameManager.instance.m_GameClient.UserInfo.UpdateInfo(layHatGiongResponse.updateInfo);
				if (PopupChonHatGiong.instance != null)
				{
					PopupChonHatGiong.instance.Set();
					ScreenLienMinhTrongCay screenLienMinhTrongCay = (ScreenLienMinhTrongCay)GUIManager.getScreen(GAME_SCREEN.ScreenLienMinhTrongCay);
					screenLienMinhTrongCay.OnSyncData();
				}
			}
			else if (layHatGiongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(layHatGiongResponse.ErrorMessage);
				EGDebug.LogWarning(layHatGiongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", layHatGiongResponse.ErrorCode, layHatGiongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnLayHatGiongResponse : response null");
		}
		return true;
	}

	public void RequestTrongCay(TrongCayRequest request)
	{
		SendRequest(m_C2SProxy.RequestTrongCay, JsonMapper.ToJson(request, false));
	}

	public bool OnTrongCayResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		TrongCayResponse trongCayResponse = JsonMapper.ToObject<TrongCayResponse>(data);
		if (trongCayResponse != null)
		{
			if (trongCayResponse.ErrorCode == ERROR_CODE.OK)
			{
				GameManager.instance.m_GameClient.UserInfo.UpdateInfo(trongCayResponse.updateInfo);
				ScreenLienMinhTrongCay screenLienMinhTrongCay = (ScreenLienMinhTrongCay)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenLienMinhTrongCay);
				screenLienMinhTrongCay.OnSyncData();
			}
			else if (trongCayResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(trongCayResponse.ErrorMessage);
				EGDebug.LogWarning(trongCayResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", trongCayResponse.ErrorCode, trongCayResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnTrongCayResponse : response null");
		}
		return true;
	}

	public void RequestThuHoach(ThuHoachRequest request)
	{
		SendRequest(m_C2SProxy.RequestThuHoach, JsonMapper.ToJson(request, false));
	}

	public bool OnThuHoachResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThuHoachResponse thuHoachResponse = JsonMapper.ToObject<ThuHoachResponse>(data);
		if (thuHoachResponse != null)
		{
			if (thuHoachResponse.ErrorCode == ERROR_CODE.OK)
			{
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongBatCocTitle"), Localization.instance.Get("PhanThuongDesc"), thuHoachResponse.phanthuong);
				GameManager.instance.m_GameClient.UserInfo.UpdateInfo(thuHoachResponse.updateInfo);
				ScreenLienMinhTrongCay screenLienMinhTrongCay = (ScreenLienMinhTrongCay)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenLienMinhTrongCay);
				screenLienMinhTrongCay.OnSyncData();
			}
			else if (thuHoachResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thuHoachResponse.ErrorMessage);
				EGDebug.LogWarning(thuHoachResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thuHoachResponse.ErrorCode, thuHoachResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnTrongCayResponse : response null");
		}
		return true;
	}

	public void RequestGetAnTromList(GetAnTromListRequest request)
	{
		SendRequest(m_C2SProxy.RequestGetAnTromList, JsonMapper.ToJson(request, false));
	}

	public bool OnGetAnTromListResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetAnTromListResponse getAnTromListResponse = JsonMapper.ToObject<GetAnTromListResponse>(data);
		if (getAnTromListResponse != null)
		{
			if (getAnTromListResponse.ErrorCode == ERROR_CODE.OK)
			{
				GameManager.instance.m_GameClient.UserInfo.UpdateInfo(getAnTromListResponse.updateData);
				PopupAnTromLinhDuoc.Create();
			}
			else if (getAnTromListResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getAnTromListResponse.ErrorMessage);
				EGDebug.LogWarning(getAnTromListResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getAnTromListResponse.ErrorCode, getAnTromListResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetAnTromListResponse : response null");
		}
		return true;
	}

	public void RequestAnTrom(AnTromRequest request)
	{
		SendRequest(m_C2SProxy.RequestAnTrom, JsonMapper.ToJson(request, false));
	}

	public bool OnAnTromResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		AnTromResponse anTromResponse = JsonMapper.ToObject<AnTromResponse>(data);
		if (anTromResponse != null)
		{
			if (anTromResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (anTromResponse.battle != null)
				{
					ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
					GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
					screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenLienMinhTrongCay;
					List<UserInfo.HeroData> listCloneHeroFromDoiHinh = UserInfo.GetListCloneHeroFromDoiHinh();
					PopupBattleResult popupBattleResult = PopupBattleResult.Create(anTromResponse.battle, listCloneHeroFromDoiHinh, 0L, 0L, 0L, 0);
					popupBattleResult.gameObject.SetActive(false);
					if (anTromResponse.phanthuong != null)
					{
						PopupDanhSachPhanThuong popupDanhSachPhanThuong = PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongBatCocTitle"), Localization.instance.Get("PhanThuongAnTromDesc"), anTromResponse.phanthuong);
						popupDanhSachPhanThuong.gameObject.SetActive(false);
					}
					if (anTromResponse.updateInfo != null)
					{
						UserInfo.UpdateInfo(anTromResponse.updateInfo);
					}
					screenBattle.Replay(anTromResponse.battle);
					screenBattle.OnFinishReplay += OnEndBattleAnTrom;
				}
				else
				{
					if (anTromResponse.phanthuong != null)
					{
						PopupDanhSachPhanThuong popupDanhSachPhanThuong2 = PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongBatCocTitle"), Localization.instance.Get("PhanThuongAnTromDesc"), anTromResponse.phanthuong);
					}
					if (anTromResponse.updateInfo != null)
					{
						UserInfo.UpdateInfo(anTromResponse.updateInfo);
					}
					ScreenLienMinhTrongCay screenLienMinhTrongCay = (ScreenLienMinhTrongCay)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenLienMinhTrongCay);
					screenLienMinhTrongCay.OnSyncData();
				}
				PopupAnTromLinhDuoc.DestroyPopup();
			}
			else if (anTromResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(anTromResponse.ErrorMessage);
				EGDebug.LogWarning(anTromResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", anTromResponse.ErrorCode, anTromResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnAnTromResponse : response null");
		}
		return true;
	}

	public void RequestGetLinhDuocInfo(GetLinhDuocDataRequest request)
	{
		SendRequest(m_C2SProxy.RequestGetLinhDuocInfo, JsonMapper.ToJson(request, false));
	}

	public bool OnGetLinhDuocInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetLinhDuocDataResponse getLinhDuocDataResponse = JsonMapper.ToObject<GetLinhDuocDataResponse>(data);
		if (getLinhDuocDataResponse != null)
		{
			if (getLinhDuocDataResponse.ErrorCode == ERROR_CODE.OK)
			{
				PopupAnTromLinhDuoc.DestroyPopup();
				ScreenLienMinhTrongCay screenLienMinhTrongCay = (ScreenLienMinhTrongCay)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenLienMinhTrongCay);
				screenLienMinhTrongCay.OnSyncData(getLinhDuocDataResponse.linhDuocData);
			}
			else if (getLinhDuocDataResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getLinhDuocDataResponse.ErrorMessage);
				EGDebug.LogWarning(getLinhDuocDataResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getLinhDuocDataResponse.ErrorCode, getLinhDuocDataResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnAnTromResponse : response null");
		}
		return true;
	}

	public void RequestGetGamerLinhDuoc(GetGamerLinhDuocRequest request)
	{
		SendRequest(m_C2SProxy.RequestGetGamerLinhDuoc, JsonMapper.ToJson(request, false));
	}

	public bool OnGetGamerLinhDuocResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetGamerLinhDuocResponse getGamerLinhDuocResponse = JsonMapper.ToObject<GetGamerLinhDuocResponse>(data);
		if (getGamerLinhDuocResponse != null)
		{
			if (getGamerLinhDuocResponse.ErrorCode == ERROR_CODE.OK)
			{
				GameManager.instance.m_GameClient.UserInfo.UpdateInfo(getGamerLinhDuocResponse.updateData);
				ScreenLienMinhTrongCay screenLienMinhTrongCay = (ScreenLienMinhTrongCay)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenLienMinhTrongCay);
				screenLienMinhTrongCay.OnSyncData();
			}
			else if (getGamerLinhDuocResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getGamerLinhDuocResponse.ErrorMessage);
				EGDebug.LogWarning(getGamerLinhDuocResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getGamerLinhDuocResponse.ErrorCode, getGamerLinhDuocResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetGamerLinhDuocResponse : response null");
		}
		return true;
	}

	public void RequestDangNhapNhanThuongTet(DangNhapNhanThuongTetRequest request)
	{
		SendRequest(m_C2SProxy.RequestDangNhapNhanThuongTet, JsonMapper.ToJson(request, false));
	}

	public bool OnDangNhapNhanThuongTetResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				if (PopupDangNhapNhanThuongTet.instance != null)
				{
					PopupDangNhapNhanThuongTet.instance.displayListPhanThuong();
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), phanThuongResponse);
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("DangNhapNhanThuong : response null");
		}
		return true;
	}

	public void RequestDungNgua(LapNguaRequest request)
	{
		SendRequest(m_C2SProxy.RequestDungNgua, JsonMapper.ToJson(request, false));
	}

	public bool OnDungNguaResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		LapNguaResponse lapNguaResponse = JsonMapper.ToObject<LapNguaResponse>(data);
		if (lapNguaResponse != null)
		{
			if (lapNguaResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (lapNguaResponse.updateData != null)
				{
					GameManager.instance.m_GameClient.UserInfo.UpdateInfo(lapNguaResponse.updateData);
				}
				if (GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi > 0)
				{
					UserInfo.ThuCuoiData thuCuoiData = GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi && e.ExpiredTime > GameManager.instance.m_GameClient.ServerTime);
					if (thuCuoiData != null && thuCuoiData.ExpiredTime > DateTime.Now && thuCuoiData.isActive)
					{
						GUIManager.instance.homeCity.mainAvatar.avatar.LoadThuCuoi(thuCuoiData.CodeName);
					}
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
				{
					ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
					screenDoiHinh.SetNhanVatInfo(false);
				}
				ThuCuoiInfo thuCuoiInfo = UnityEngine.Object.FindObjectOfType<ThuCuoiInfo>();
				if (thuCuoiInfo != null)
				{
					thuCuoiInfo.Create();
				}
			}
			else if (lapNguaResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(lapNguaResponse.ErrorMessage);
				EGDebug.LogWarning(lapNguaResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", lapNguaResponse.ErrorCode, lapNguaResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnDungNguaResponse : response null");
		}
		return true;
	}

	public void RequestThaoNgua(ThaoNguaRequest request)
	{
		SendRequest(m_C2SProxy.RequestThaoNgua, JsonMapper.ToJson(request, false));
	}

	public bool OnThaoNguaResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThaoNguaResponse thaoNguaResponse = JsonMapper.ToObject<ThaoNguaResponse>(data);
		if (thaoNguaResponse != null)
		{
			if (thaoNguaResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thaoNguaResponse.updateData != null)
				{
					GameManager.instance.m_GameClient.UserInfo.UpdateInfo(thaoNguaResponse.updateData);
				}
				GUIManager.instance.homeCity.mainAvatar.avatar.LoadThuCuoi(string.Empty);
				ThuCuoiInfo thuCuoiInfo = UnityEngine.Object.FindObjectOfType<ThuCuoiInfo>();
				if (thuCuoiInfo != null)
				{
					thuCuoiInfo.Create();
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
				{
					ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
					screenDoiHinh.SetNhanVatInfo(false);
				}
			}
			else if (thaoNguaResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thaoNguaResponse.ErrorMessage);
				EGDebug.LogWarning(thaoNguaResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thaoNguaResponse.ErrorCode, thaoNguaResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnThaoNguaResponse : response null");
		}
		return true;
	}

	public void RequestActiveNgua(ActiveNguaRequest request)
	{
		SendRequest(m_C2SProxy.RequestActiveNgua, JsonMapper.ToJson(request, false));
	}

	public bool OnActiveNguaResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ActiveNguaResponse activeNguaResponse = JsonMapper.ToObject<ActiveNguaResponse>(data);
		if (activeNguaResponse != null)
		{
			if (activeNguaResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (activeNguaResponse.updateData != null)
				{
					GameManager.instance.m_GameClient.UserInfo.UpdateInfo(activeNguaResponse.updateData);
				}
				if (GUIManager.instance != null && GUIManager.instance.homeCity != null && (bool)GUIManager.instance.homeCity.mainAvatar)
				{
					UserInfo.HeroData nvDaiDien = UserInfo.GetHeroFromDoiHinh(1);
					UserInfo.TrangBiData trangBiData = UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == nvDaiDien.VuKhiID);
					UserInfo.ThuCuoiData thuCuoiData = GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi && e.ExpiredTime > GameManager.instance.m_GameClient.ServerTime);
					string costume = string.Empty;
					UserInfo.CostumeData costumeData = null;
					if (UserInfo.CostumeList != null)
					{
						costumeData = UserInfo.CostumeList.Find((UserInfo.CostumeData e) => e.ID == nvDaiDien.CostumeID);
						costume = ((costumeData == null) ? string.Empty : costumeData.CodeName);
					}
					GUIManager.instance.homeCity.mainAvatar.SetCodeName(nvDaiDien.Name, (trangBiData != null) ? trangBiData.Name : string.Empty, (thuCuoiData != null) ? thuCuoiData.CodeName : string.Empty, costume, string.Empty);
				}
				ThuCuoiInfo thuCuoiInfo = UnityEngine.Object.FindObjectOfType<ThuCuoiInfo>();
				if (thuCuoiInfo != null)
				{
					thuCuoiInfo.Create();
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
				{
					ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
					screenDoiHinh.SetNhanVatInfo(false);
				}
			}
			else if (activeNguaResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(activeNguaResponse.ErrorMessage);
				EGDebug.LogWarning(activeNguaResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", activeNguaResponse.ErrorCode, activeNguaResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnActiveNguaResponse : response null");
		}
		return true;
	}

	public void RequestVongQuay(int id)
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestVongQuay(HostID.Server, RmiContext.ReliableSend, id);
	}

	public bool OnVongQuayResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		VongQuayResponse vongQuayResponse = JsonMapper.ToObject<VongQuayResponse>(data);
		if (vongQuayResponse != null)
		{
			if (vongQuayResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (vongQuayResponse.updateInfo != null)
				{
					GameManager.instance.m_GameClient.UserInfo.UpdateInfo(vongQuayResponse.updateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTayNai)
				{
					ScreenTayNai screenTayNai = GUIManager.getScreen(GAME_SCREEN.ScreenTayNai) as ScreenTayNai;
					screenTayNai.m_Tab = ScreenTayNai.ScreenTayNaiTab.TabVatPham;
					screenTayNai.SyncWithNetworkData();
				}
				ScreenVongQuay.instance.BeginSpin(vongQuayResponse.pos, vongQuayResponse.pt);
			}
			else if (vongQuayResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(vongQuayResponse.ErrorMessage);
				EGDebug.LogWarning(vongQuayResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", vongQuayResponse.ErrorCode, vongQuayResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetSoDoBatQuaiTranResponse : response null");
		}
		return true;
	}

	public void RequestSetSoDoBatQuaiTran(SetSoDoBatQuaiTranRequest request)
	{
		SendRequest(m_C2SProxy.RequestSetSoDoBatQuaiTran, JsonMapper.ToJson(request, false));
	}

	public bool OnSetSoDoBatQuaiTranResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SetSoDoBatQuaiTranResponse setSoDoBatQuaiTranResponse = JsonMapper.ToObject<SetSoDoBatQuaiTranResponse>(data);
		if (setSoDoBatQuaiTranResponse != null)
		{
			if (setSoDoBatQuaiTranResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (setSoDoBatQuaiTranResponse.UpdateInfo != null)
				{
					GameManager.instance.m_GameClient.UserInfo.UpdateInfo(setSoDoBatQuaiTranResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
				{
					ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
					screenDoiHinh.updateSoDoBatQuaiTran();
				}
				if (PopupSoDoBatQuai.instance != null)
				{
					PopupSoDoBatQuai.instance.updateView(setSoDoBatQuaiTranResponse);
				}
				if (PopupSelectSoDoBatQuai.instance != null)
				{
					PopupSelectSoDoBatQuai.DestroyPopup();
				}
			}
			else if (setSoDoBatQuaiTranResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(setSoDoBatQuaiTranResponse.ErrorMessage);
				EGDebug.LogWarning(setSoDoBatQuaiTranResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", setSoDoBatQuaiTranResponse.ErrorCode, setSoDoBatQuaiTranResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetSoDoBatQuaiTranResponse : response null");
		}
		return true;
	}

	public void RequestCuongHoaBatQuaiTran(CuongHoaBatQuaiTranRequest request)
	{
		SendRequest(m_C2SProxy.RequestCuongHoaBatQuaiTran, JsonMapper.ToJson(request, false));
	}

	public bool OnCuongHoaBatQuaiTranResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		CuongHoaBatQuaiTranResponse cuongHoaBatQuaiTranResponse = JsonMapper.ToObject<CuongHoaBatQuaiTranResponse>(data);
		if (cuongHoaBatQuaiTranResponse != null)
		{
			if (cuongHoaBatQuaiTranResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (cuongHoaBatQuaiTranResponse.UpdateInfo != null)
				{
					GameManager.instance.m_GameClient.UserInfo.UpdateInfo(cuongHoaBatQuaiTranResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
				{
					ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
					screenDoiHinh.updateSoDoBatQuaiTran();
				}
				if (PopupSoDoBatQuai.instance != null)
				{
					PopupSoDoBatQuai.instance.UpdateView(cuongHoaBatQuaiTranResponse);
				}
			}
			else if (cuongHoaBatQuaiTranResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(cuongHoaBatQuaiTranResponse.ErrorMessage);
				EGDebug.LogWarning(cuongHoaBatQuaiTranResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", cuongHoaBatQuaiTranResponse.ErrorCode, cuongHoaBatQuaiTranResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnCuongHoaBatQuaiTranResponse : response null");
		}
		return true;
	}

	public void RequestOpenDoiHinhThienCangTran(OpenDoiHinhThienCangRequest request)
	{
		SendRequest(m_C2SProxy.RequestOpenDoiHinhThienCangTran, JsonMapper.ToJson(request, false));
	}

	public bool OnOpenDoiHinhThienCangResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		OpenDoiHinhThienCangResponse openDoiHinhThienCangResponse = JsonMapper.ToObject<OpenDoiHinhThienCangResponse>(data);
		if (openDoiHinhThienCangResponse != null)
		{
			if (openDoiHinhThienCangResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (openDoiHinhThienCangResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(openDoiHinhThienCangResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
				{
					ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
					if (NGUITools.GetActive(screenDoiHinh.ThienCangGroup.gameObject))
					{
						screenDoiHinh.getListThienCangHoTro();
					}
				}
			}
			else if (openDoiHinhThienCangResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(openDoiHinhThienCangResponse.ErrorMessage);
				EGDebug.LogWarning(openDoiHinhThienCangResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", openDoiHinhThienCangResponse.ErrorCode, openDoiHinhThienCangResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetThienCangResponse : response null");
		}
		return true;
	}

	public void RequestSetDoiHinhThienCangTran(SetDoiHinhThienCangRequest request)
	{
		SendRequest(m_C2SProxy.RequestSetDoiHinhThienCangTran, JsonMapper.ToJson(request, false));
	}

	public bool OnSetSoDoThienCangResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SetDoiHinhThienCangResponse setDoiHinhThienCangResponse = JsonMapper.ToObject<SetDoiHinhThienCangResponse>(data);
		if (setDoiHinhThienCangResponse != null)
		{
			if (setDoiHinhThienCangResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (setDoiHinhThienCangResponse.UpdateInfo != null)
				{
					GameManager.instance.m_GameClient.UserInfo.UpdateInfo(setDoiHinhThienCangResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
				{
					ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
					screenDoiHinh.updateSoDoThienCangTran();
				}
			}
			else if (setDoiHinhThienCangResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(setDoiHinhThienCangResponse.ErrorMessage);
				EGDebug.LogWarning(setDoiHinhThienCangResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", setDoiHinhThienCangResponse.ErrorCode, setDoiHinhThienCangResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetSoDoBatQuaiTranResponse : response null");
		}
		return true;
	}

	public void RequestViewLeagueReplay(int id)
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestViewLeagueReplay(HostID.Server, RmiContext.ReliableSend, id);
	}

	public bool OnRequestviewLeagueReplayResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		LeagueBattleReplayResponse leagueBattleReplayResponse = JsonMapper.ToObject<LeagueBattleReplayResponse>(data);
		if (leagueBattleReplayResponse != null)
		{
			if (leagueBattleReplayResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (leagueBattleReplayResponse.replay != null)
				{
					ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
					GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
					screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenLeagueMain;
					List<UserInfo.HeroData> listCloneHeroFromDoiHinh = UserInfo.GetListCloneHeroFromDoiHinh();
					PopupBattleResult popupBattleResult = PopupBattleResult.Create(leagueBattleReplayResponse.replay, listCloneHeroFromDoiHinh, 0L, 0L, 0L, 0);
					popupBattleResult.gameObject.SetActive(false);
					screenBattle.Replay(leagueBattleReplayResponse.replay, "BM_HOA_SON_LUAN_KIEM");
				}
			}
			else if (leagueBattleReplayResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(leagueBattleReplayResponse.ErrorMessage);
				EGDebug.LogWarning(leagueBattleReplayResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", leagueBattleReplayResponse.ErrorCode, leagueBattleReplayResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetLeagueDataResponse : response null");
		}
		return true;
	}

	public void RequestSubmitDoiHinhLeague()
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestSubmitDoiHinhLeague(HostID.Server, RmiContext.ReliableSend, "DaiHoi");
	}

	public void RequestSubmitDoiHinhSieuCup()
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestSubmitDoiHinhLeague(HostID.Server, RmiContext.ReliableSend, "MinhChu");
	}

	public bool OnSubmitDoiHinhLeagueResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SubmitLeagueDoiHinhResponse submitLeagueDoiHinhResponse = JsonMapper.ToObject<SubmitLeagueDoiHinhResponse>(data);
		if (submitLeagueDoiHinhResponse != null)
		{
			if (submitLeagueDoiHinhResponse.ErrorCode == ERROR_CODE.OK)
			{
				GameManager.instance.m_GameClient.UserInfo.LeagueInfo.SubmitAvaiable = false;
				LeagueScheduleItem[] array = UnityEngine.Object.FindObjectsOfType<LeagueScheduleItem>();
				LeagueScheduleItem[] array2 = array;
				foreach (LeagueScheduleItem leagueScheduleItem in array2)
				{
					leagueScheduleItem.SyncServer();
				}
			}
			else if (submitLeagueDoiHinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(submitLeagueDoiHinhResponse.ErrorMessage);
				EGDebug.LogWarning(submitLeagueDoiHinhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", submitLeagueDoiHinhResponse.ErrorCode, submitLeagueDoiHinhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetLeagueDataResponse : response null");
		}
		return true;
	}

	public void RequestGetLeagueData()
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestGetLeagueData(HostID.Server, RmiContext.ReliableSend);
	}

	public bool OnGetLeagueDataResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetLeagueDataResponse getLeagueDataResponse = JsonMapper.ToObject<GetLeagueDataResponse>(data);
		if (getLeagueDataResponse != null)
		{
			if (getLeagueDataResponse.ErrorCode == ERROR_CODE.OK)
			{
				GameManager.instance.m_GameClient.UserInfo.UpdateInfo(getLeagueDataResponse.userInfo);
				GUIManager.instance.SetScreen(GAME_SCREEN.ScreenLeagueMain);
			}
			else if (getLeagueDataResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getLeagueDataResponse.ErrorMessage);
				EGDebug.LogWarning(getLeagueDataResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getLeagueDataResponse.ErrorCode, getLeagueDataResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetLeagueDataResponse : response null");
		}
		return true;
	}

	public void RequestTakeOnCostume(int id, int hid)
	{
		TakeOnCostumeRequest takeOnCostumeRequest = new TakeOnCostumeRequest();
		takeOnCostumeRequest.HID = hid;
		takeOnCostumeRequest.ID = id;
		SendRequest(m_C2SProxy.RequestTakeOnCostume, JsonMapper.ToJson(takeOnCostumeRequest, false));
	}

	public bool OnTakeOnCostume(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UserInfo userInfo = JsonMapper.ToObject<UserInfo>(data);
		if (userInfo != null)
		{
			if (userInfo.ErrorCode == ERROR_CODE.OK)
			{
				UserInfo.UpdateInfo(userInfo);
				ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
				screenDoiHinh.TurnOnCostumeGroup();
				screenDoiHinh.SetNhanVatInfo(false);
				if (GUIManager.instance != null && GUIManager.instance.homeCity != null && (bool)GUIManager.instance.homeCity.mainAvatar)
				{
					UserInfo.HeroData nvDaiDien = UserInfo.GetHeroFromDoiHinh(1);
					UserInfo.TrangBiData trangBiData = UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == nvDaiDien.VuKhiID);
					UserInfo.ThuCuoiData thuCuoiData = GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi && e.ExpiredTime > GameManager.instance.m_GameClient.ServerTime);
					string costume = string.Empty;
					UserInfo.CostumeData costumeData = null;
					if (UserInfo.CostumeList != null)
					{
						costumeData = UserInfo.CostumeList.Find((UserInfo.CostumeData e) => e.ID == nvDaiDien.CostumeID);
						costume = ((costumeData == null) ? string.Empty : costumeData.CodeName);
					}
					GUIManager.instance.homeCity.mainAvatar.SetCodeName(nvDaiDien.Name, (trangBiData != null) ? trangBiData.Name : string.Empty, (thuCuoiData != null) ? thuCuoiData.CodeName : string.Empty, costume, string.Empty);
				}
			}
			else if (userInfo.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(userInfo.ErrorMessage);
				EGDebug.LogWarning(userInfo.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", userInfo.ErrorCode, userInfo.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnTakeOnCostume : response null");
		}
		return true;
	}

	public void RequestTakeOffCostume(int hid)
	{
		TakeOffCostumeRequest takeOffCostumeRequest = new TakeOffCostumeRequest();
		takeOffCostumeRequest.HID = hid;
		SendRequest(m_C2SProxy.RequestTakeOffCostume, JsonMapper.ToJson(takeOffCostumeRequest, false));
	}

	public bool OnTakeOffCostume(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UserInfo userInfo = JsonMapper.ToObject<UserInfo>(data);
		if (userInfo != null)
		{
			if (userInfo.ErrorCode == ERROR_CODE.OK)
			{
				UserInfo.UpdateInfo(userInfo);
				ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
				screenDoiHinh.TurnOnCostumeGroup();
				screenDoiHinh.SetNhanVatInfo(false);
				if (GUIManager.instance != null && GUIManager.instance.homeCity != null && (bool)GUIManager.instance.homeCity.mainAvatar)
				{
					UserInfo.HeroData nvDaiDien = UserInfo.GetHeroFromDoiHinh(1);
					UserInfo.TrangBiData trangBiData = UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == nvDaiDien.VuKhiID);
					UserInfo.ThuCuoiData thuCuoiData = GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi && e.ExpiredTime > GameManager.instance.m_GameClient.ServerTime);
					string costume = string.Empty;
					UserInfo.CostumeData costumeData = null;
					if (UserInfo.CostumeList != null)
					{
						costumeData = UserInfo.CostumeList.Find((UserInfo.CostumeData e) => e.ID == nvDaiDien.CostumeID);
						costume = ((costumeData == null) ? string.Empty : costumeData.CodeName);
					}
					GUIManager.instance.homeCity.mainAvatar.SetCodeName(nvDaiDien.Name, (trangBiData != null) ? trangBiData.Name : string.Empty, (thuCuoiData != null) ? thuCuoiData.CodeName : string.Empty, costume, string.Empty);
				}
			}
			else if (userInfo.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(userInfo.ErrorMessage);
				EGDebug.LogWarning(userInfo.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", userInfo.ErrorCode, userInfo.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnTakeOffCostume : response null");
		}
		return true;
	}

	public void RequestGoNgocCostume(int id, UserInfo.TrangBiData.LoaiNgoc loai)
	{
		GoNgocCostumeRequest goNgocCostumeRequest = new GoNgocCostumeRequest();
		goNgocCostumeRequest.ID = id;
		goNgocCostumeRequest.LoaiNgoc = loai;
		SendRequest(m_C2SProxy.RequestGoNgocCostume, JsonMapper.ToJson(goNgocCostumeRequest, false));
	}

	public bool OnGoNgocCostume(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UserInfo userInfo = JsonMapper.ToObject<UserInfo>(data);
		if (userInfo != null)
		{
			if (userInfo.ErrorCode == ERROR_CODE.OK)
			{
				UserInfo.UpdateInfo(userInfo);
				ScreenKhamNgocCostume screenKhamNgocCostume = GUIManager.getScreen(GAME_SCREEN.ScreenKhamNgocCostume) as ScreenKhamNgocCostume;
				screenKhamNgocCostume.SyncWithNetworkData();
			}
			else if (userInfo.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(userInfo.ErrorMessage);
				EGDebug.LogWarning(userInfo.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", userInfo.ErrorCode, userInfo.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGoNgocCostume : response null");
		}
		return true;
	}

	public void RequestKhamNgocCostume(int id, UserInfo.TrangBiData.LoaiNgoc loai, int lvlKham)
	{
		KhamNgocCostumeRequest khamNgocCostumeRequest = new KhamNgocCostumeRequest();
		khamNgocCostumeRequest.ID = id;
		khamNgocCostumeRequest.LoaiNgoc = loai;
		khamNgocCostumeRequest.Count = lvlKham;
		SendRequest(m_C2SProxy.RequestKhamNgocCostume, JsonMapper.ToJson(khamNgocCostumeRequest, false));
	}

	public bool OnKhamNgocCostume(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UserInfo userInfo = JsonMapper.ToObject<UserInfo>(data);
		if (userInfo != null)
		{
			if (userInfo.ErrorCode == ERROR_CODE.OK)
			{
				UserInfo.UpdateInfo(userInfo);
				ScreenKhamNgocCostume screenKhamNgocCostume = GUIManager.getScreen(GAME_SCREEN.ScreenKhamNgocCostume) as ScreenKhamNgocCostume;
				screenKhamNgocCostume.SyncWithNetworkData();
			}
			else if (userInfo.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(userInfo.ErrorMessage);
				EGDebug.LogWarning(userInfo.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", userInfo.ErrorCode, userInfo.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnKhamNgocCostume : response null");
		}
		return true;
	}

	public void RequestTinhLuyenCostume(int id, int phoiGiapID)
	{
		TinhLuyenCostumeRequest tinhLuyenCostumeRequest = new TinhLuyenCostumeRequest();
		tinhLuyenCostumeRequest.ID = id;
		tinhLuyenCostumeRequest.PhoiGiapID = phoiGiapID;
		SendRequest(m_C2SProxy.RequestTinhLuyenCostume, JsonMapper.ToJson(tinhLuyenCostumeRequest, false));
	}

	public bool OnTinhLuyenCostume(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UserInfo userInfo = JsonMapper.ToObject<UserInfo>(data);
		if (userInfo != null)
		{
			if (userInfo.ErrorCode == ERROR_CODE.OK)
			{
				UserInfo.UpdateInfo(userInfo);
				ScreenCreateCostume screenCreateCostume = GUIManager.getScreen(GAME_SCREEN.ScreenCreateCostume) as ScreenCreateCostume;
				screenCreateCostume.InitListOfCostumeAvatarForMe();
			}
			else if (userInfo.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(userInfo.ErrorMessage);
				EGDebug.LogWarning(userInfo.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", userInfo.ErrorCode, userInfo.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnTinhLuyenCostume : response null");
		}
		return true;
	}

	public void RequestCreateCostume(string costume, int phoiGiapID = 0)
	{
		CreateCostumeRequest createCostumeRequest = new CreateCostumeRequest();
		createCostumeRequest.Costume = costume;
		createCostumeRequest.PhoiGiapID = phoiGiapID;
		SendRequest(m_C2SProxy.RequestCreateCostume, JsonMapper.ToJson(createCostumeRequest, false));
	}

	public bool OnCreateCostume(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UserInfo userInfo = JsonMapper.ToObject<UserInfo>(data);
		if (userInfo != null)
		{
			if (userInfo.ErrorCode == ERROR_CODE.OK)
			{
				UserInfo.UpdateInfo(userInfo);
				ScreenCreateCostume screenCreateCostume = GUIManager.getScreen(GAME_SCREEN.ScreenCreateCostume) as ScreenCreateCostume;
				screenCreateCostume.InitListOfCostumeAvatarForMe();
			}
			else if (userInfo.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(userInfo.ErrorMessage);
				EGDebug.LogWarning(userInfo.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", userInfo.ErrorCode, userInfo.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnCreateCostume : response null");
		}
		return true;
	}

	public void RequestNhanThuongTichLuyNap(NhanThuongTichLuyNapRequest request)
	{
		SendRequest(m_C2SProxy.RequestNhanThuongTichLuyNap, JsonMapper.ToJson(request, false));
	}

	public bool OnNhanThuongTichLuyNapResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTuongDuongTeThe)
				{
					ScreenKyNgo_TuongDuongTeThe screenKyNgo_TuongDuongTeThe = GUIManager.getScreen(GAME_SCREEN.ScreenTuongDuongTeThe) as ScreenKyNgo_TuongDuongTeThe;
					screenKyNgo_TuongDuongTeThe.updateMainMenuView();
					screenKyNgo_TuongDuongTeThe.displayInfo();
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), phanThuongResponse);
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("NhanThuongTichLuyNap : response null");
		}
		return true;
	}

	public void RequestNhanThuongTichLuyTieu(NhanThuongTichLuyTieuRequest request)
	{
		SendRequest(m_C2SProxy.RequestNhanThuongTichLuyTieu, JsonMapper.ToJson(request, false));
	}

	public bool OnNhanThuongTichLuyTieuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKimTienBang)
				{
					ScreenKyNgo_KimTienBang screenKyNgo_KimTienBang = GUIManager.getScreen(GAME_SCREEN.ScreenKimTienBang) as ScreenKyNgo_KimTienBang;
					screenKyNgo_KimTienBang.displayInfo();
					screenKyNgo_KimTienBang.updateMainMenuView();
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), phanThuongResponse);
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("NhanThuongTichLuyNap : response null");
		}
		return true;
	}

	public void RequestGetCacLoaiTop(CacLoaiTopRequest request)
	{
		SendRequest(m_C2SProxy.RequestGetCacLoaiTop, JsonMapper.ToJson(request, false));
	}

	public bool OnGetCacLoaiTopResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		CacLoaiTopResponse cacLoaiTopResponse = JsonMapper.ToObject<CacLoaiTopResponse>(data);
		if (cacLoaiTopResponse != null)
		{
			if (cacLoaiTopResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTop && cacLoaiTopResponse.TopType != CacLoaiTopRequest.TopType.TopNONE)
				{
					ScreenTop screenTop = GUIManager.getScreen(GAME_SCREEN.ScreenTop) as ScreenTop;
					switch (cacLoaiTopResponse.TopType)
					{
					case CacLoaiTopRequest.TopType.TopChienTruong:
						screenTop.displayTopChienTruong(cacLoaiTopResponse.ListTopChienTruong);
						break;
					case CacLoaiTopRequest.TopType.TopTinhLuyen:
						screenTop.displayTopTinhLuyen(cacLoaiTopResponse.ListTopTinhLuyen);
						break;
					case CacLoaiTopRequest.TopType.TopCongLuc:
						screenTop.displayTopCongLuc(cacLoaiTopResponse);
						break;
					case CacLoaiTopRequest.TopType.TopBiKip:
						screenTop.displayTopBiKip(cacLoaiTopResponse.ListTopBiKip);
						break;
					case CacLoaiTopRequest.TopType.TopNgoc:
						screenTop.displayTopNgoc(cacLoaiTopResponse.ListTopNgoc);
						break;
					case CacLoaiTopRequest.TopType.TopHanhTau:
						screenTop.displayTopHanhTau(cacLoaiTopResponse.ListTopHanhTau);
						break;
					case CacLoaiTopRequest.TopType.TopHoangKim:
						screenTop.displayTopHoangKim(cacLoaiTopResponse.ListTopHoangKim);
						break;
					case CacLoaiTopRequest.TopType.TopChuyenSinh:
						screenTop.displayTopChuyenSinh(cacLoaiTopResponse.ListTopChuyenSinh);
						break;
					case CacLoaiTopRequest.TopType.TopTuLinh:
						screenTop.displayTopTuLinh(cacLoaiTopResponse.ListTopTuLinh);
						break;
					case CacLoaiTopRequest.TopType.TopThienMaLenh:
						screenTop.displayTopThienMaLenh(cacLoaiTopResponse.ListTopThienMa);
						break;
					case CacLoaiTopRequest.TopType.TopTrangBiHK:
						screenTop.displayTopTrangBiHK(cacLoaiTopResponse.ListTopTBHoangKim);
						break;
					}
				}
			}
			else if (cacLoaiTopResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(cacLoaiTopResponse.ErrorMessage);
				EGDebug.LogWarning(cacLoaiTopResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", cacLoaiTopResponse.ErrorCode, cacLoaiTopResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("Get danh sach cac loai TOP : response null");
		}
		return true;
	}

	public bool OnDenGioChienTruong(HostID remote, RmiContext rmiContext, string data)
	{
		UserInfo userInfo = JsonMapper.ToObject<UserInfo>(data);
		MessagePopup.Create(userInfo.ErrorMessage);
		if (isAutoChienTruong)
		{
			RequestThamGiaCT2();
		}
		return true;
	}

	public void RequestDoiTenLienMinh(string newName)
	{
		DoiTenBangRequest doiTenBangRequest = new DoiTenBangRequest();
		doiTenBangRequest.NewName = newName;
		SendRequest(m_C2SProxy.RequestDoiTenBang, JsonMapper.ToJson(doiTenBangRequest, false));
	}

	public bool OnDoiTenBangResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DoiTenBangResponse doiTenBangResponse = JsonMapper.ToObject<DoiTenBangResponse>(data);
		if (doiTenBangResponse != null)
		{
			if (doiTenBangResponse.ErrorCode == ERROR_CODE.OK)
			{
				GameManager.instance.m_GameClient.UserInfo.UpdateInfo(doiTenBangResponse.updateInfo);
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenQuanLyLienMinh)
				{
					((ScreenQuanLyLienMinh)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenQuanLyLienMinh)).OnEnable();
				}
			}
			else if (doiTenBangResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(doiTenBangResponse.ErrorMessage);
				EGDebug.LogWarning(doiTenBangResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", doiTenBangResponse.ErrorCode, doiTenBangResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnDoiTenBangResponse : response null");
		}
		return true;
	}

	public void RequestGetThongTinLienServer(int sid, int gid)
	{
		GetThongTinLienServerRequest getThongTinLienServerRequest = new GetThongTinLienServerRequest();
		getThongTinLienServerRequest.TargetGID = gid;
		getThongTinLienServerRequest.TargetSID = sid;
		SendRequest(m_C2SProxy.RequestGetThongTinLienServer, JsonMapper.ToJson(getThongTinLienServerRequest, false));
	}

	public bool OnGetThongTinLienServerResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetThongTinLienServerResponse getThongTinLienServerResponse = JsonMapper.ToObject<GetThongTinLienServerResponse>(data);
		if (getThongTinLienServerResponse != null)
		{
			if (getThongTinLienServerResponse.ErrorCode == ERROR_CODE.OK)
			{
				ScreenDoiHinh screenDoiHinh = (ScreenDoiHinh)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenDoiHinh);
				screenDoiHinh.ShowAnotherUserInfo(getThongTinLienServerResponse.info);
			}
			else if (getThongTinLienServerResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getThongTinLienServerResponse.ErrorMessage);
				EGDebug.LogWarning(getThongTinLienServerResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getThongTinLienServerResponse.ErrorCode, getThongTinLienServerResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetThongTinLienServerResponse : response null");
		}
		return true;
	}

	public bool OnBanPhaoHoaNotify(HostID remote, RmiContext rmiContext, string data)
	{
		StartCoroutine(PhaoHoaBangChien(data));
		return true;
	}

	private IEnumerator PhaoHoaBangChien(string data)
	{
		yield return new WaitForSeconds(1f);
		MessagePopup.Create(data);
		UnityEngine.Object rs = Resources.Load("FX/Prefabs/MISC_FIREWORK");
		UnityEngine.Object.Instantiate(rs, GUIManager.instance.ScreenContainer3D.GetComponentInChildren<ScreenMain3D>().mainAvatar.transform.position, Quaternion.identity);
	}

	public void RequestBanPhaoHoa()
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestBanPhaoHoa(HostID.Server, RmiContext.ReliableSend);
	}

	public void RequestGetTopVongQuay()
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestGetTopVongQuay(HostID.Server, RmiContext.ReliableSend);
	}

	public bool OnGetTopVongQuayResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetTopEventResponse getTopEventResponse = JsonMapper.ToObject<GetTopEventResponse>(data);
		if (getTopEventResponse != null)
		{
			if (getTopEventResponse.ErrorCode == ERROR_CODE.OK)
			{
				ScreenVongQuay screenVongQuay = (ScreenVongQuay)GUIManager.getScreen(GAME_SCREEN.ScreenVongQuay);
				screenVongQuay.ShowTop(getTopEventResponse.TopInfo);
			}
			else if (getTopEventResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getTopEventResponse.ErrorMessage);
				EGDebug.LogWarning(getTopEventResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getTopEventResponse.ErrorCode, getTopEventResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetTopVongQuayResponse : response null");
		}
		return true;
	}

	public void RequestGetTopBanPhaoHoaEvent()
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestGetTopPhaoHoa(HostID.Server, RmiContext.ReliableSend);
	}

	public bool OnGetTopBanPhaoHoaEventResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetTopEventResponse getTopEventResponse = JsonMapper.ToObject<GetTopEventResponse>(data);
		if (getTopEventResponse != null)
		{
			if (getTopEventResponse.ErrorCode == ERROR_CODE.OK)
			{
				GameManager.instance.m_GameClient.m_UserInfo.ServerInfo.PhaoHoaInfo.TopInfo = new List<KeyValuePair<string, int>>();
				foreach (KeyValuePair<string, int> item in getTopEventResponse.TopInfo)
				{
					GameManager.instance.m_GameClient.m_UserInfo.ServerInfo.PhaoHoaInfo.TopInfo.Add(item);
				}
				ScreenBanPhaoHoaEvent screenBanPhaoHoaEvent = (ScreenBanPhaoHoaEvent)GUIManager.getScreen(GAME_SCREEN.ScreenBanPhaoHoaEvent);
				screenBanPhaoHoaEvent.OnTopGrpActive();
			}
			else if (getTopEventResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getTopEventResponse.ErrorMessage);
				EGDebug.LogWarning(getTopEventResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getTopEventResponse.ErrorCode, getTopEventResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetTopBanPhaoHoaEventResponse : response null");
		}
		return true;
	}

	public void RequestGetPhanThuongBanPhaoHoaEvent()
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestGetPhanThuongPhaoHoa(HostID.Server, RmiContext.ReliableSend);
	}

	public bool OnGetPhanThuongPhaoHoaEventResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetPhanThuongPhaoHoaResponse getPhanThuongPhaoHoaResponse = JsonMapper.ToObject<GetPhanThuongPhaoHoaResponse>(data);
		if (getPhanThuongPhaoHoaResponse != null)
		{
			if (getPhanThuongPhaoHoaResponse.ErrorCode == ERROR_CODE.OK)
			{
				GameManager.instance.m_GameClient.m_UserInfo.ServerInfo.PhaoHoaInfo.PhaoHoaCount = getPhanThuongPhaoHoaResponse.score;
				ScreenBanPhaoHoaEvent screenBanPhaoHoaEvent = (ScreenBanPhaoHoaEvent)GUIManager.getScreen(GAME_SCREEN.ScreenBanPhaoHoaEvent);
				screenBanPhaoHoaEvent.OnPhanThuongGrpActive();
			}
			else if (getPhanThuongPhaoHoaResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getPhanThuongPhaoHoaResponse.ErrorMessage);
				EGDebug.LogWarning(getPhanThuongPhaoHoaResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getPhanThuongPhaoHoaResponse.ErrorCode, getPhanThuongPhaoHoaResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetPhanThuongPhaoHoaEventResponse : response null");
		}
		return true;
	}

	public void RequestBanPhaoHoaEvent()
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestBanPhaoHoaEvent(HostID.Server, RmiContext.ReliableSend);
	}

	public bool OnBanPhaoHoaEventResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		BanPhaoHoaResponse banPhaoHoaResponse = JsonMapper.ToObject<BanPhaoHoaResponse>(data);
		if (banPhaoHoaResponse != null)
		{
			if (banPhaoHoaResponse.ErrorCode == ERROR_CODE.OK)
			{
				UnityEngine.Object obj = Resources.Load("FX/Prefabs/MISC_FIREWORK");
				HomeResponse.Position3D position3D = JsonMapper.ToObject<HomeResponse.Position3D>(banPhaoHoaResponse.pos);
				if (obj != null && (DateTime.Now - LastTimeBanPhaoHoa).TotalSeconds > 3.0)
				{
					LastTimeBanPhaoHoa = DateTime.Now;
					UnityEngine.Object.Instantiate(obj, new UnityEngine.Vector3(position3D.X, position3D.Y, position3D.Z), Quaternion.identity);
				}
				if (banPhaoHoaResponse.isOwn)
				{
					GameManager.instance.m_GameClient.m_UserInfo.ServerInfo.PhaoHoaInfo.PhaoHoaCount = banPhaoHoaResponse.totalScore;
					GameManager.instance.m_GameClient.m_UserInfo.Gamer.PhaoHoaCount = banPhaoHoaResponse.score;
					if (banPhaoHoaResponse.TopInfo != null)
					{
						GameManager.instance.m_GameClient.m_UserInfo.ServerInfo.PhaoHoaInfo.TopInfo = banPhaoHoaResponse.TopInfo;
					}
					ScreenBanPhaoHoaEvent screenBanPhaoHoaEvent = (ScreenBanPhaoHoaEvent)GUIManager.getScreen(GAME_SCREEN.ScreenBanPhaoHoaEvent);
					screenBanPhaoHoaEvent.OnEnable();
				}
			}
			else if (banPhaoHoaResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(banPhaoHoaResponse.ErrorMessage);
				EGDebug.LogWarning(banPhaoHoaResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", banPhaoHoaResponse.ErrorCode, banPhaoHoaResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBanPhaoHoaEventResponse : response null");
		}
		return true;
	}

	public void RequestLinhThuongPhaoHoaEvent(int mocThuong)
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestLinhThuongPhaoHoaEvent(HostID.Server, RmiContext.ReliableSend, mocThuong);
	}

	public bool OnLinhThuongPhaoHoaEventResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				PopupDanhSachPhanThuong.Create(string.Empty, string.Empty, phanThuongResponse);
				ScreenBanPhaoHoaEvent screenBanPhaoHoaEvent = (ScreenBanPhaoHoaEvent)GUIManager.getScreen(GAME_SCREEN.ScreenBanPhaoHoaEvent);
				screenBanPhaoHoaEvent.OnPhanThuongGrpActive();
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBanPhaoHoaEventResponse : response null");
		}
		return true;
	}

	public void RequestGetListOtherUser(GetListOtherUserRequest request)
	{
		EGDebug.Log("RequestGetListOtherUser:::::::::::::");
		SendRequest(m_C2SProxy.RequestGetListOtherUser, JsonMapper.ToJson(request, false));
	}

	public bool OnGetListOtherUserResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetListOtherUserResponse getListOtherUserResponse = JsonMapper.ToObject<GetListOtherUserResponse>(data);
		if (getListOtherUserResponse != null)
		{
			if (getListOtherUserResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (getListOtherUserResponse.listUser != null && getListOtherUserResponse.listUser.Count > 0 && GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBanBe)
				{
					ScreenBanBe screenBanBe = GUIManager.getScreen(GAME_SCREEN.ScreenBanBe) as ScreenBanBe;
					screenBanBe.AutoThachDauTab.addOtherUser(getListOtherUserResponse.listUser);
				}
			}
			else if (getListOtherUserResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getListOtherUserResponse.ErrorMessage);
				EGDebug.LogWarning(getListOtherUserResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getListOtherUserResponse.ErrorCode, getListOtherUserResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetListOtherUserResponse : response null");
		}
		return true;
	}

	public void RequestNhanThuongDapNieu(NhanThuongDapNieuRequest request)
	{
		SendRequest(m_C2SProxy.RequestNhanThuongDapNieu, JsonMapper.ToJson(request, false));
	}

	public bool OnNhanThuongDapNieuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		NhanThuongDapNieuResponse nhanThuongDapNieuResponse = JsonMapper.ToObject<NhanThuongDapNieuResponse>(data);
		if (nhanThuongDapNieuResponse != null)
		{
			if (nhanThuongDapNieuResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (nhanThuongDapNieuResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(nhanThuongDapNieuResponse.updateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenKyNgoDapNieu)
				{
					ScreenKyNgo_DapNieu screenKyNgo_DapNieu = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoDapNieu) as ScreenKyNgo_DapNieu;
					screenKyNgo_DapNieu.updateView(nhanThuongDapNieuResponse.PhanThuong);
				}
			}
			else if (nhanThuongDapNieuResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(nhanThuongDapNieuResponse.ErrorMessage);
				EGDebug.LogWarning(nhanThuongDapNieuResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", nhanThuongDapNieuResponse.ErrorCode, nhanThuongDapNieuResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetListOtherUserResponse : response null");
		}
		return true;
	}

	public void RequestChuyenSinhDeTu(int HeroID)
	{
		m_C2SProxy.RequestChuyenSinhDeTu(HostID.Server, RmiContext.ReliableSend, HeroID);
	}

	public bool OnChuyenSinhDeTuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ChuyenSinhResponse chuyenSinhResponse = JsonMapper.ToObject<ChuyenSinhResponse>(data);
		if (chuyenSinhResponse != null)
		{
			if (chuyenSinhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (chuyenSinhResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(chuyenSinhResponse.updateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenChuyenSinh)
				{
					ScreenChuyenSinh screenChuyenSinh = GUIManager.getScreen(GAME_SCREEN.ScreenChuyenSinh) as ScreenChuyenSinh;
					screenChuyenSinh.updateView(chuyenSinhResponse.phanthuong);
				}
			}
			else if (chuyenSinhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(chuyenSinhResponse.ErrorMessage);
				EGDebug.LogWarning(chuyenSinhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", chuyenSinhResponse.ErrorCode, chuyenSinhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnChuyenSinhDeTuResponse : response null");
		}
		return true;
	}

	public void RequestSetBoPhapNhanVatBatQuai(SetDoNhanVatBatQuaiRequest request)
	{
		SendRequest(m_C2SProxy.RequestSetBoPhapNhanVatBatQuai, JsonMapper.ToJson(request));
	}

	public void RequestSetNoiCongNhanVatBatQuai(SetDoNhanVatBatQuaiRequest request)
	{
		SendRequest(m_C2SProxy.RequestSetNoiCongNhanVatBatQuai, JsonMapper.ToJson(request));
	}

	public void RequestSetTrangBiNhanVatBatQuai(SetDoNhanVatBatQuaiRequest request)
	{
		SendRequest(m_C2SProxy.RequestSetTrangBiNhanVatBatQuai, JsonMapper.ToJson(request));
	}

	public bool OnSetDoBatQuaiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SetDoNhanVatBatQuaiResponse setDoNhanVatBatQuaiResponse = JsonMapper.ToObject<SetDoNhanVatBatQuaiResponse>(data);
		if (setDoNhanVatBatQuaiResponse != null)
		{
			if (setDoNhanVatBatQuaiResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (setDoNhanVatBatQuaiResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(setDoNhanVatBatQuaiResponse.updateInfo);
				}
				if (PopupNhanVat.instance != null)
				{
					PopupNhanVat.instance.updateNhanVatBatQuaiView();
				}
			}
			else if (setDoNhanVatBatQuaiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(setDoNhanVatBatQuaiResponse.ErrorMessage);
				EGDebug.LogWarning(setDoNhanVatBatQuaiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", setDoNhanVatBatQuaiResponse.ErrorCode, setDoNhanVatBatQuaiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetNoiCongBatQuaiResponse : response null");
		}
		return true;
	}

	public void RequestSetThanThu(SelectStartThanThuRequest request)
	{
		SendRequest(m_C2SProxy.RequestSetThanThu, JsonMapper.ToJson(request, false));
	}

	public bool OnSetThanThuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SelectStartThanThuResponse selectStartThanThuResponse = JsonMapper.ToObject<SelectStartThanThuResponse>(data);
		if (selectStartThanThuResponse != null)
		{
			if (selectStartThanThuResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (selectStartThanThuResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(selectStartThanThuResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenSelectTheFirstPet)
				{
					GUIManager.instance.SetScreen(GAME_SCREEN.ScreenSelectTheFirstHorse);
				}
			}
			else if (selectStartThanThuResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(selectStartThanThuResponse.ErrorMessage);
				EGDebug.LogWarning(selectStartThanThuResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", selectStartThanThuResponse.ErrorCode, selectStartThanThuResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetNoiCongBatQuaiResponse : response null");
		}
		return true;
	}

	public void RequestBatThanThu()
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestBatThanThu(HostID.Server, RmiContext.ReliableSend);
	}

	public void RequestAutoResolveThanThuDao()
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestAutoResolveThanThuDao(HostID.Server, RmiContext.ReliableSend);
	}

	public bool OnBatThanThuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThanThuCaptureResponse response = JsonMapper.ToObject<ThanThuCaptureResponse>(data);
		if (response != null)
		{
			if (response.ErrorCode == ERROR_CODE.OK)
			{
				if (response.updateInfo != null)
				{
					UserInfo.UpdateInfo(response.updateInfo);
				}
				ScreenBatThanThu screenBatThanThu = (ScreenBatThanThu)GUIManager.getScreen(GAME_SCREEN.ScreenBatThanThu);
				if (screenBatThanThu != null)
				{
					screenBatThanThu.OnReceiveThanThu(GameManager.instance.m_GameClient.m_UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == response.newPetId));
				}
			}
			else if (response.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(response.ErrorMessage);
				EGDebug.LogWarning(response.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", response.ErrorCode, response.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnBatThanThuResponse : response null");
		}
		return true;
	}

	public void RequestTruongThanhThanThu(int thanthuId)
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestTruongThanhThanThu(HostID.Server, RmiContext.ReliableSend, thanthuId);
	}

	public bool OnTruongThanhThanThuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThanThuUpdateInfoResponse thanThuUpdateInfoResponse = JsonMapper.ToObject<ThanThuUpdateInfoResponse>(data);
		if (thanThuUpdateInfoResponse != null)
		{
			if (thanThuUpdateInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thanThuUpdateInfoResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(thanThuUpdateInfoResponse.updateInfo);
				}
				ScreenTruongThanhThanThu screenTruongThanhThanThu = GUIManager.getScreen(GAME_SCREEN.ScreenTruongThanhThanThu) as ScreenTruongThanhThanThu;
				screenTruongThanhThanThu.Set();
			}
			else if (thanThuUpdateInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thanThuUpdateInfoResponse.ErrorMessage);
				EGDebug.LogWarning(thanThuUpdateInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thanThuUpdateInfoResponse.ErrorCode, thanThuUpdateInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnTruongThanhThanThuResponse : response null");
		}
		return true;
	}

	public void RequestNangPhamThanThu(int thanthuId, int thanthu1, int thanthu2, int thanthu3, int thanthu4, int thuHon)
	{
		ThanThuNangPhamRequest thanThuNangPhamRequest = new ThanThuNangPhamRequest();
		thanThuNangPhamRequest.MainThanThu = thanthuId;
		thanThuNangPhamRequest.thanthu1 = thanthu1;
		thanThuNangPhamRequest.thanthu2 = thanthu2;
		thanThuNangPhamRequest.thanthu3 = thanthu3;
		thanThuNangPhamRequest.thanthu4 = thanthu4;
		thanThuNangPhamRequest.thuHonCount = thuHon;
		SendRequest(m_C2SProxy.RequestNangPhamThanThu, JsonMapper.ToJson(thanThuNangPhamRequest, false));
	}

	public bool OnNangPhamThanTHuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThanThuNangPhamResponse thanThuNangPhamResponse = JsonMapper.ToObject<ThanThuNangPhamResponse>(data);
		if (thanThuNangPhamResponse != null)
		{
			if (thanThuNangPhamResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thanThuNangPhamResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(thanThuNangPhamResponse.updateInfo);
				}
				ScreenNangPhamThanThu screenNangPhamThanThu = GUIManager.getScreen(GAME_SCREEN.ScreenNangPhamThanThu) as ScreenNangPhamThanThu;
				if (thanThuNangPhamResponse.success)
				{
					screenNangPhamThanThu.Set(ScreenNangPhamThanThu.STATE.SUCCESS);
				}
				else
				{
					screenNangPhamThanThu.Set(ScreenNangPhamThanThu.STATE.FAIL);
				}
			}
			else if (thanThuNangPhamResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thanThuNangPhamResponse.ErrorMessage);
				EGDebug.LogWarning(thanThuNangPhamResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thanThuNangPhamResponse.ErrorCode, thanThuNangPhamResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnNangPhamThanTHuResponse : response null");
		}
		return true;
	}

	public void RequestDoiThanThu(int thanthuId)
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestDoiThanThu(HostID.Server, RmiContext.ReliableSend, thanthuId);
	}

	public bool OnDoiThanThuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThanThuUpdateInfoResponse thanThuUpdateInfoResponse = JsonMapper.ToObject<ThanThuUpdateInfoResponse>(data);
		if (thanThuUpdateInfoResponse != null)
		{
			if (thanThuUpdateInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thanThuUpdateInfoResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(thanThuUpdateInfoResponse.updateInfo);
				}
				try
				{
					ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
					if (screenDoiHinh != null && screenDoiHinh.ThanThuGrp != null)
					{
						ThanThuInfo component = screenDoiHinh.ThanThuGrp.GetComponent<ThanThuInfo>();
						if (component != null && GameManager.instance.m_GameClient.UserInfo.ListThanThu != null)
						{
							component.Set(GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.curThanThu), true);
						}
					}
					ScreenDanhSachThanThu screenDanhSachThanThu = GUIManager.getScreen(GAME_SCREEN.ScreenDanhSachThanThu) as ScreenDanhSachThanThu;
					if (screenDanhSachThanThu != null && GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDanhSachThanThu)
					{
						screenDanhSachThanThu.SyncWithNetworkData();
					}
					if (GUIManager.instance != null && GUIManager.instance.homeCity != null && GUIManager.instance.homeCity.mainAvatar != null && GameManager.instance.m_GameClient.UserInfo.DoiHinh != null && GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran != null && GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran.Count > 0)
					{
						UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran[0]);
						if (heroData != null && GameManager.instance.m_GameClient.UserInfo.ListThanThu != null)
						{
							UserInfo.PetInfo petInfo = GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo p) => p.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.curThanThu);
							GUIManager.instance.homeCity.mainAvatar.SetCodeName(heroData.Name, string.Empty, string.Empty, string.Empty, (petInfo != null) ? petInfo.codename : string.Empty, (petInfo != null) ? petInfo.Quality : UserInfo.PetInfo.PetQuality.PHO_THONG);
						}
					}
				}
				catch (Exception ex)
				{
					EGDebug.LogWarning("OnDoiThanThuResponse UI update: " + ((ex != null) ? ex.ToString() : null));
				}
			}
			else if (thanThuUpdateInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thanThuUpdateInfoResponse.ErrorMessage);
				EGDebug.LogWarning(thanThuUpdateInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thanThuUpdateInfoResponse.ErrorCode, thanThuUpdateInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnDoiThanThuResponse : response null");
		}
		return true;
	}

	public void RequestThonPheThanThu(int thanthuChinh, int thanthuThonPhe, bool isCaocap)
	{
		ThanThuThonPheRequest thanThuThonPheRequest = new ThanThuThonPheRequest();
		thanThuThonPheRequest.ThanThuChinh = thanthuChinh;
		thanThuThonPheRequest.ThanThuThonPhe = thanthuThonPhe;
		thanThuThonPheRequest.CaoCap = isCaocap;
		SendRequest(m_C2SProxy.RequestThonPheThanThu, JsonMapper.ToJson(thanThuThonPheRequest, false));
	}

	public bool OnThonPheThanThuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThanThuUpdateInfoResponse thanThuUpdateInfoResponse = JsonMapper.ToObject<ThanThuUpdateInfoResponse>(data);
		if (thanThuUpdateInfoResponse != null)
		{
			if (thanThuUpdateInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thanThuUpdateInfoResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(thanThuUpdateInfoResponse.updateInfo);
				}
				ScreenThonPheThanThu screenThonPheThanThu = GUIManager.getScreen(GAME_SCREEN.ScreenThonPheThanThu) as ScreenThonPheThanThu;
				screenThonPheThanThu.BeginThonPhe();
			}
			else if (thanThuUpdateInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thanThuUpdateInfoResponse.ErrorMessage);
				EGDebug.LogWarning(thanThuUpdateInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thanThuUpdateInfoResponse.ErrorCode, thanThuUpdateInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnThonPheThanThuResponse : response null");
		}
		return true;
	}

	public void RequestTruyenCongThanThu(int thanthuChinh, int thanthuTruyenCong)
	{
		ThanThuThonPheRequest thanThuThonPheRequest = new ThanThuThonPheRequest();
		thanThuThonPheRequest.ThanThuChinh = thanthuChinh;
		thanThuThonPheRequest.ThanThuThonPhe = thanthuTruyenCong;
		SendRequest(m_C2SProxy.RequestTruyenCongThanThu, JsonMapper.ToJson(thanThuThonPheRequest, false));
	}

	public bool OnTruyenCongThanThuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThanThuUpdateInfoResponse thanThuUpdateInfoResponse = JsonMapper.ToObject<ThanThuUpdateInfoResponse>(data);
		if (thanThuUpdateInfoResponse != null)
		{
			if (thanThuUpdateInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thanThuUpdateInfoResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(thanThuUpdateInfoResponse.updateInfo);
				}
				ScreenTruyenCongThanThu screenTruyenCongThanThu = GUIManager.getScreen(GAME_SCREEN.ScreenTruyenCongThanThu) as ScreenTruyenCongThanThu;
				screenTruyenCongThanThu.SyncWithNetwork();
			}
			else if (thanThuUpdateInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thanThuUpdateInfoResponse.ErrorMessage);
				EGDebug.LogWarning(thanThuUpdateInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thanThuUpdateInfoResponse.ErrorCode, thanThuUpdateInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnTruyenCongThanThuResponse : response null");
		}
		return true;
	}

	public void RequestStartThanThuDao()
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestGetThanThuDao(HostID.Server, RmiContext.ReliableSend);
	}

	public bool OnStartThanThuDaoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThanThuDaoResponse thanThuDaoResponse = JsonMapper.ToObject<ThanThuDaoResponse>(data);
		if (thanThuDaoResponse != null)
		{
			if (thanThuDaoResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thanThuDaoResponse.updateUserinfo != null)
				{
					UserInfo.UpdateInfo(thanThuDaoResponse.updateUserinfo);
				}
				((ScreenBatThanThu)GUIManager.getScreen(GAME_SCREEN.ScreenBatThanThu)).StartBatThanThu(thanThuDaoResponse.thanthudaoInfo);
			}
			else if (thanThuDaoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thanThuDaoResponse.ErrorMessage);
				EGDebug.LogWarning(thanThuDaoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thanThuDaoResponse.ErrorCode, thanThuDaoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnDoiThanThuResponse : response null");
		}
		return true;
	}

	public void RequestThamGiaGuiTietKiem()
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestThamGiaGuiTietKiem(HostID.Server, RmiContext.ReliableSend);
	}

	public bool OnThamGiaGuiTietKiemRequest(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThamGiaGuiTietKiemResponse thamGiaGuiTietKiemResponse = JsonMapper.ToObject<ThamGiaGuiTietKiemResponse>(data);
		if (thamGiaGuiTietKiemResponse != null)
		{
			if (thamGiaGuiTietKiemResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thamGiaGuiTietKiemResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(thamGiaGuiTietKiemResponse.updateInfo);
				}
				ScreenKyNgo_GuiTietKiem screenKyNgo_GuiTietKiem = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoGuiTietKiem) as ScreenKyNgo_GuiTietKiem;
				if (screenKyNgo_GuiTietKiem != null)
				{
					screenKyNgo_GuiTietKiem.getListThangCap();
				}
			}
			else if (thamGiaGuiTietKiemResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thamGiaGuiTietKiemResponse.ErrorMessage);
				EGDebug.LogWarning(thamGiaGuiTietKiemResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thamGiaGuiTietKiemResponse.ErrorCode, thamGiaGuiTietKiemResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnThamGiaGuiTietKiemRequest : response null");
		}
		return true;
	}

	public void RequestGetGuiTietKiem(int id)
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestGetGuiTietKiem(HostID.Server, RmiContext.ReliableSend, id);
	}

	public bool OnGetGuiTietKiemRequest(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThamGiaGuiTietKiemResponse thamGiaGuiTietKiemResponse = JsonMapper.ToObject<ThamGiaGuiTietKiemResponse>(data);
		if (thamGiaGuiTietKiemResponse != null)
		{
			if (thamGiaGuiTietKiemResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thamGiaGuiTietKiemResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(thamGiaGuiTietKiemResponse.updateInfo);
				}
				ScreenKyNgo_GuiTietKiem screenKyNgo_GuiTietKiem = GUIManager.getScreen(GAME_SCREEN.ScreenKyNgoGuiTietKiem) as ScreenKyNgo_GuiTietKiem;
				if (screenKyNgo_GuiTietKiem != null)
				{
					screenKyNgo_GuiTietKiem.getListThangCap();
				}
			}
			else if (thamGiaGuiTietKiemResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thamGiaGuiTietKiemResponse.ErrorMessage);
				EGDebug.LogWarning(thamGiaGuiTietKiemResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thamGiaGuiTietKiemResponse.ErrorCode, thamGiaGuiTietKiemResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnThamGiaGuiTietKiemRequest : response null");
		}
		return true;
	}

	public void RequestNhanThuong1MilUser()
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestGetThuong1MilUser(HostID.Server, RmiContext.ReliableSend);
	}

	public bool OnNhanThuong1MilUser(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThuongThanhVien1MilResponse thuongThanhVien1MilResponse = JsonMapper.ToObject<ThuongThanhVien1MilResponse>(data);
		if (thuongThanhVien1MilResponse != null)
		{
			if (thuongThanhVien1MilResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thuongThanhVien1MilResponse.updateUserinfo != null)
				{
					UserInfo.UpdateInfo(thuongThanhVien1MilResponse.updateUserinfo);
				}
				PopupDanhSachPhanThuong.Create(string.Empty, string.Empty, thuongThanhVien1MilResponse.phanthuong);
			}
			else if (thuongThanhVien1MilResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thuongThanhVien1MilResponse.ErrorMessage);
				EGDebug.LogWarning(thuongThanhVien1MilResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thuongThanhVien1MilResponse.ErrorCode, thuongThanhVien1MilResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnNhanThuong1MilUser : response null");
		}
		return true;
	}

	public bool OnPopup1MilUser(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThuongThanhVien1MilResponse thuongThanhVien1MilResponse = JsonMapper.ToObject<ThuongThanhVien1MilResponse>(data);
		if (thuongThanhVien1MilResponse != null)
		{
			if (thuongThanhVien1MilResponse.ErrorCode == ERROR_CODE.OK)
			{
				PopupUser1Tr.Create(thuongThanhVien1MilResponse.isSpecial, thuongThanhVien1MilResponse.countUser, thuongThanhVien1MilResponse.phanthuong);
			}
			else if (thuongThanhVien1MilResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thuongThanhVien1MilResponse.ErrorMessage);
				EGDebug.LogWarning(thuongThanhVien1MilResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thuongThanhVien1MilResponse.ErrorCode, thuongThanhVien1MilResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnPopup1MilUser : response null");
		}
		return true;
	}

	public void RequestTanCongSonMon(int targetID, int SID)
	{
		TanCongSonMonRequest tanCongSonMonRequest = new TanCongSonMonRequest();
		tanCongSonMonRequest.targetGID = targetID;
		tanCongSonMonRequest.targetSID = SID;
		SendRequest(m_C2SProxy.RequestTanCongSonMon, JsonMapper.ToJson(tanCongSonMonRequest, false));
	}

	public bool OnTanCongSonMonResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		TanCongSonMonResponse tanCongSonMonResponse = JsonMapper.ToObject<TanCongSonMonResponse>(data);
		if (tanCongSonMonResponse != null)
		{
			if (tanCongSonMonResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (tanCongSonMonResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(tanCongSonMonResponse.updateInfo);
				}
				if (tanCongSonMonResponse.phanthuong != null && tanCongSonMonResponse.phanthuong.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(tanCongSonMonResponse.phanthuong.UpdateUserInfo);
				}
				PopupSonMonBattle.Create(tanCongSonMonResponse);
			}
			else if (tanCongSonMonResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(tanCongSonMonResponse.ErrorMessage);
				EGDebug.LogWarning(tanCongSonMonResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", tanCongSonMonResponse.ErrorCode, tanCongSonMonResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnTanCongSonMonResponse : response null");
		}
		return true;
	}

	public void RequestThuHoachSonMon(int congtrinhID)
	{
		CongTrinhSonMonRequest congTrinhSonMonRequest = new CongTrinhSonMonRequest();
		congTrinhSonMonRequest.CongTrinhID = congtrinhID;
		congTrinhSonMonRequest.ClientInfo = GameManager.instance.m_GameClient.UserInfo.SonMon.ListCongTrinh.Find((UserInfo.SonMonBuildingInfo ct) => ct.ID == congtrinhID);
		SendRequest(m_C2SProxy.RequestThuHoachSonMon, JsonMapper.ToJson(congTrinhSonMonRequest, false));
	}

	public bool OnThuHoachSonMonResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThuHoachSonMonResponse thuHoachSonMonResponse = JsonMapper.ToObject<ThuHoachSonMonResponse>(data);
		if (thuHoachSonMonResponse != null)
		{
			if (thuHoachSonMonResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thuHoachSonMonResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(thuHoachSonMonResponse.updateInfo);
				}
				if (!string.IsNullOrEmpty(thuHoachSonMonResponse.ErrorMessage))
				{
					MessagePopup.Create(thuHoachSonMonResponse.ErrorMessage);
				}
				for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.SonMon.ListCongTrinh.Count; i++)
				{
					if (GameManager.instance.m_GameClient.UserInfo.SonMon.ListCongTrinh[i].ID == thuHoachSonMonResponse.CongTrinh.ID)
					{
						GameManager.instance.m_GameClient.UserInfo.SonMon.ListCongTrinh[i] = thuHoachSonMonResponse.CongTrinh;
					}
				}
				ScreenSonMonMain screenSonMonMain = (ScreenSonMonMain)GUIManager.getScreen(GAME_SCREEN.ScreenSonMonMain);
				screenSonMonMain.UpdateCongTrinh(thuHoachSonMonResponse.CongTrinh);
				if (thuHoachSonMonResponse.phanthuong != null && thuHoachSonMonResponse.phanthuong.PhanThuongList.Count > 0)
				{
					PopupDanhSachPhanThuong.Create(string.Empty, string.Empty, thuHoachSonMonResponse.phanthuong);
				}
			}
			else if (thuHoachSonMonResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thuHoachSonMonResponse.ErrorMessage);
				EGDebug.LogWarning(thuHoachSonMonResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thuHoachSonMonResponse.ErrorCode, thuHoachSonMonResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnThuHoachSonMonResponse : response null");
		}
		return true;
	}

	public void RequestXayDungSonMon(int congtrinhID, UserInfo.SonMonBuildingInfo.SonMonBuildingType loaiCT)
	{
		CongTrinhSonMonRequest congTrinhSonMonRequest = new CongTrinhSonMonRequest();
		congTrinhSonMonRequest.LoaiCT = loaiCT;
		congTrinhSonMonRequest.CongTrinhID = congtrinhID;
		SendRequest(m_C2SProxy.RequestXayDungSonMon, JsonMapper.ToJson(congTrinhSonMonRequest, false));
	}

	public void RequestXayDungSonMon(int congtrinhID)
	{
		CongTrinhSonMonRequest congTrinhSonMonRequest = new CongTrinhSonMonRequest();
		congTrinhSonMonRequest.CongTrinhID = congtrinhID;
		SendRequest(m_C2SProxy.RequestXayDungSonMon, JsonMapper.ToJson(congTrinhSonMonRequest, false));
	}

	public void RequestFastForwardSonMon(int congtrinhID)
	{
		CongTrinhSonMonRequest congTrinhSonMonRequest = new CongTrinhSonMonRequest();
		congTrinhSonMonRequest.XayNhanhRequest = true;
		congTrinhSonMonRequest.CongTrinhID = congtrinhID;
		SendRequest(m_C2SProxy.RequestXayDungSonMon, JsonMapper.ToJson(congTrinhSonMonRequest, false));
	}

	public bool OnXayDungSonMonResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		XayDungSonMonResponse xayDungSonMonResponse = JsonMapper.ToObject<XayDungSonMonResponse>(data);
		if (xayDungSonMonResponse != null)
		{
			if (xayDungSonMonResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (xayDungSonMonResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(xayDungSonMonResponse.updateInfo);
				}
				for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.SonMon.ListCongTrinh.Count; i++)
				{
					if (GameManager.instance.m_GameClient.UserInfo.SonMon.ListCongTrinh[i].ID == xayDungSonMonResponse.CongTrinhUpdate.ID)
					{
						GameManager.instance.m_GameClient.UserInfo.SonMon.ListCongTrinh[i] = xayDungSonMonResponse.CongTrinhUpdate;
					}
				}
				ScreenSonMonMain screenSonMonMain = (ScreenSonMonMain)GUIManager.getScreen(GAME_SCREEN.ScreenSonMonMain);
				screenSonMonMain.UpdateCongTrinh(xayDungSonMonResponse.CongTrinhUpdate);
			}
			else if (xayDungSonMonResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(xayDungSonMonResponse.ErrorMessage);
				EGDebug.LogWarning(xayDungSonMonResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", xayDungSonMonResponse.ErrorCode, xayDungSonMonResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnXayDungSonMonResponse : response null");
		}
		return true;
	}

	public void RequestDoiHinhSonMon(Dictionary<string, int> heroList)
	{
		DoiHinhSonMonRequest doiHinhSonMonRequest = new DoiHinhSonMonRequest();
		doiHinhSonMonRequest.HeroList = heroList;
		SendRequest(m_C2SProxy.RequestDoiHinhSonMon, JsonMapper.ToJson(doiHinhSonMonRequest, false));
	}

	public bool OnDoiHinhSonMonResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DoiHinhSonMonResponse doiHinhSonMonResponse = JsonMapper.ToObject<DoiHinhSonMonResponse>(data);
		if (doiHinhSonMonResponse != null)
		{
			if (doiHinhSonMonResponse.ErrorCode != ERROR_CODE.OK)
			{
				if (doiHinhSonMonResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
				{
					MessagePopup.Create(doiHinhSonMonResponse.ErrorMessage);
					EGDebug.LogWarning(doiHinhSonMonResponse.ErrorMessage);
				}
				else
				{
					string text = string.Format("ErrorCode: {0}, Message: {1}", doiHinhSonMonResponse.ErrorCode, doiHinhSonMonResponse.ErrorMessage);
					MessagePopup.Create(text);
					EGDebug.LogWarning(text);
				}
			}
		}
		else
		{
			EGDebug.LogError("OnDoiHinhSonMonResponse : response null");
		}
		return true;
	}

	public void RequestDoThamSonMonThuDich(int gid, int sid)
	{
		DoThamSonMonRequest doThamSonMonRequest = new DoThamSonMonRequest();
		doThamSonMonRequest.TargetGID = gid;
		doThamSonMonRequest.TargetSID = sid;
		SendRequest(m_C2SProxy.RequestDoThamSonMon, JsonMapper.ToJson(doThamSonMonRequest));
	}

	public void RequestDoThamSonMonRandom()
	{
		DoThamSonMonRequest doThamSonMonRequest = new DoThamSonMonRequest();
		doThamSonMonRequest.TargetGID = -1;
		SendRequest(m_C2SProxy.RequestDoThamSonMon, JsonMapper.ToJson(doThamSonMonRequest));
	}

	public bool OnDoThamSonMonResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DoThamSonMonResponse doThamSonMonResponse = JsonMapper.ToObject<DoThamSonMonResponse>(data);
		if (doThamSonMonResponse != null)
		{
			if (doThamSonMonResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (doThamSonMonResponse.updateData != null)
				{
					UserInfo.UpdateInfo(doThamSonMonResponse.updateData);
				}
				if (doThamSonMonResponse.Target != null && doThamSonMonResponse.Target.ListCongTrinh != null && doThamSonMonResponse.Target.ListCongTrinh.Count > 1)
				{
					ScreenSonMonMain screenSonMonMain = (ScreenSonMonMain)GUIManager.getScreen(GAME_SCREEN.ScreenSonMonMain);
					screenSonMonMain.Set(doThamSonMonResponse.Target, true);
				}
			}
			else if (doThamSonMonResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(doThamSonMonResponse.ErrorMessage);
				EGDebug.LogWarning(doThamSonMonResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", doThamSonMonResponse.ErrorCode, doThamSonMonResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnDoThamSonMonResponse : response null");
		}
		return true;
	}

	public void RequestDoiDoThanBi(DoiItemThanBiRequest request)
	{
		SendRequest(m_C2SProxy.RequestMuaDoThanBi, JsonMapper.ToJson(request));
	}

	public bool OnMuaDoThanBiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DoiItemThanBiReponse doiItemThanBiReponse = JsonMapper.ToObject<DoiItemThanBiReponse>(data);
		if (doiItemThanBiReponse != null)
		{
			if (doiItemThanBiReponse.ErrorCode == ERROR_CODE.OK)
			{
				if (doiItemThanBiReponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(doiItemThanBiReponse.updateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenCuaHangThanBi)
				{
					ScreenCuaHangThanBi screenCuaHangThanBi = (ScreenCuaHangThanBi)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenCuaHangThanBi);
					screenCuaHangThanBi.updateView();
				}
				if (PopUpMua.instance != null)
				{
					PopUpMua.DestroyPopup();
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), doiItemThanBiReponse.PhanThuongResponse);
			}
			else if (doiItemThanBiReponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(doiItemThanBiReponse.ErrorMessage);
				EGDebug.LogWarning(doiItemThanBiReponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", doiItemThanBiReponse.ErrorCode, doiItemThanBiReponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnKichHoatGiftCodeResponse : response null");
		}
		return true;
	}

	public void RequestGetTopSonMon()
	{
		SendRequest(m_C2SProxy.RequestGetTopSonMon, string.Empty);
	}

	public bool OnGetTopSonMonResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetTopSonMonResponse getTopSonMonResponse = JsonMapper.ToObject<GetTopSonMonResponse>(data);
		if (getTopSonMonResponse != null)
		{
			if (getTopSonMonResponse.ErrorCode == ERROR_CODE.OK)
			{
				PopupSonMonEvent.Create(getTopSonMonResponse);
			}
			else if (getTopSonMonResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getTopSonMonResponse.ErrorMessage);
				EGDebug.LogWarning(getTopSonMonResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getTopSonMonResponse.ErrorCode, getTopSonMonResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetTopSonMonResponse : response null");
		}
		return true;
	}

	public void RequestKhaiQuangTrangBi(int itemID, int itemCount)
	{
		KhaiQuangTrangBiRequest khaiQuangTrangBiRequest = new KhaiQuangTrangBiRequest();
		khaiQuangTrangBiRequest.TrangbiID = itemID;
		khaiQuangTrangBiRequest.ItemCount = itemCount;
		SendRequest(m_C2SProxy.RequestKhaiQuangTrangBi, JsonMapper.ToJson(khaiQuangTrangBiRequest));
	}

	public bool OnKhaiQuangTrangBiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		KhaiQuangTrangBiResponse khaiQuangTrangBiResponse = JsonMapper.ToObject<KhaiQuangTrangBiResponse>(data);
		if (khaiQuangTrangBiResponse != null)
		{
			if (khaiQuangTrangBiResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (khaiQuangTrangBiResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(khaiQuangTrangBiResponse.updateInfo);
				}
				if (khaiQuangTrangBiResponse.Success)
				{
					MessagePopup.Create(Localization.instance.Get("KhaiQuangThanhCong"));
				}
				else
				{
					MessagePopup.Create(Localization.instance.Get("KhaiQuangThatBai"));
				}
				ScreenThanBinh screenThanBinh = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenThanBinh) as ScreenThanBinh;
				screenThanBinh.OnUpdateInfo();
			}
			else if (khaiQuangTrangBiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(khaiQuangTrangBiResponse.ErrorMessage);
				EGDebug.LogWarning(khaiQuangTrangBiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", khaiQuangTrangBiResponse.ErrorCode, khaiQuangTrangBiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnKhaiQuangTrangBiResponse : response null");
		}
		return true;
	}

	public void RequestDungLuyenTrangBi(int itemID)
	{
		DungLuyenTrangBiRequest dungLuyenTrangBiRequest = new DungLuyenTrangBiRequest();
		dungLuyenTrangBiRequest.TrangbiID = itemID;
		SendRequest(m_C2SProxy.RequestDungLuyenTrangBi, JsonMapper.ToJson(dungLuyenTrangBiRequest));
	}

	public bool OnDungLuyenTrangBiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DungLuyenTrangBiResponse dungLuyenTrangBiResponse = JsonMapper.ToObject<DungLuyenTrangBiResponse>(data);
		if (dungLuyenTrangBiResponse != null)
		{
			if (dungLuyenTrangBiResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (dungLuyenTrangBiResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(dungLuyenTrangBiResponse.updateInfo);
				}
				ScreenThanBinh screenThanBinh = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenThanBinh) as ScreenThanBinh;
				screenThanBinh.OnShowConfirmGrp(dungLuyenTrangBiResponse.newTrangBi, false);
			}
			else if (dungLuyenTrangBiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(dungLuyenTrangBiResponse.ErrorMessage);
				EGDebug.LogWarning(dungLuyenTrangBiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", dungLuyenTrangBiResponse.ErrorCode, dungLuyenTrangBiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnDungLuyenTrangBiResponse : response null");
		}
		return true;
	}

	public void RequestTayLuyenTrangBi(int itemID)
	{
		TayLuyenTrangBiRequest tayLuyenTrangBiRequest = new TayLuyenTrangBiRequest();
		tayLuyenTrangBiRequest.TrangbiID = itemID;
		SendRequest(m_C2SProxy.RequestTayLuyenTrangBi, JsonMapper.ToJson(tayLuyenTrangBiRequest));
	}

	public bool OnTayLuyenTrangBiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		TayLuyenTrangBiResponse tayLuyenTrangBiResponse = JsonMapper.ToObject<TayLuyenTrangBiResponse>(data);
		if (tayLuyenTrangBiResponse != null)
		{
			if (tayLuyenTrangBiResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (tayLuyenTrangBiResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(tayLuyenTrangBiResponse.updateInfo);
				}
				ScreenThanBinh screenThanBinh = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenThanBinh) as ScreenThanBinh;
				screenThanBinh.OnShowConfirmGrp(tayLuyenTrangBiResponse.newTrangBi, true);
			}
			else if (tayLuyenTrangBiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(tayLuyenTrangBiResponse.ErrorMessage);
				EGDebug.LogWarning(tayLuyenTrangBiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", tayLuyenTrangBiResponse.ErrorCode, tayLuyenTrangBiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnTayLuyenTrangBiResponse : response null");
		}
		return true;
	}

	public void RequestConfirmThanBinhTrangBi()
	{
		SendRequest(m_C2SProxy.RequestConfirmTayLuyenTrangBi, string.Empty);
	}

	public bool OnConfirmThanBinhTrangBiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		TayLuyenTrangBiResponse tayLuyenTrangBiResponse = JsonMapper.ToObject<TayLuyenTrangBiResponse>(data);
		if (tayLuyenTrangBiResponse != null)
		{
			if (tayLuyenTrangBiResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (tayLuyenTrangBiResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(tayLuyenTrangBiResponse.updateInfo);
				}
				ScreenThanBinh screenThanBinh = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenThanBinh) as ScreenThanBinh;
				screenThanBinh.OnUpdateInfo();
			}
			else if (tayLuyenTrangBiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(tayLuyenTrangBiResponse.ErrorMessage);
				EGDebug.LogWarning(tayLuyenTrangBiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", tayLuyenTrangBiResponse.ErrorCode, tayLuyenTrangBiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnTayLuyenTrangBiResponse : response null");
		}
		return true;
	}

	public void RequestGetSonMonInfo()
	{
		SendRequest(m_C2SProxy.RequestGetSonMonInfo, string.Empty);
	}

	public bool OnGetSonMonInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		TayLuyenTrangBiResponse tayLuyenTrangBiResponse = JsonMapper.ToObject<TayLuyenTrangBiResponse>(data);
		if (tayLuyenTrangBiResponse != null)
		{
			if (tayLuyenTrangBiResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (tayLuyenTrangBiResponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(tayLuyenTrangBiResponse.updateInfo);
				}
				GUIManager.instance.SetScreen(GAME_SCREEN.ScreenSonMonMain);
			}
			else if (tayLuyenTrangBiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(tayLuyenTrangBiResponse.ErrorMessage);
				EGDebug.LogWarning(tayLuyenTrangBiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", tayLuyenTrangBiResponse.ErrorCode, tayLuyenTrangBiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetSonMonInfoResponse : response null");
		}
		return true;
	}

	public void RequestUnLockVoCong(UnLockVoCongRequest request)
	{
		SendRequest(m_C2SProxy.RequestUnLockVoCong, JsonMapper.ToJson(request));
	}

	public bool OnUnLockVoCongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UnLockVoCongResponse unLockVoCongResponse = JsonMapper.ToObject<UnLockVoCongResponse>(data);
		if (unLockVoCongResponse != null)
		{
			if (unLockVoCongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (unLockVoCongResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(unLockVoCongResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenMoKhoa)
				{
					ScreenMoKhoa screenMoKhoa = GUIManager.getScreen(GAME_SCREEN.ScreenMoKhoa) as ScreenMoKhoa;
					screenMoKhoa.updateView(unLockVoCongResponse);
				}
			}
			else if (unLockVoCongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(unLockVoCongResponse.ErrorMessage);
				EGDebug.LogWarning(unLockVoCongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", unLockVoCongResponse.ErrorCode, unLockVoCongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnUnLockVoCongResponse : response null");
		}
		return true;
	}

	public void RequestQuayBacMayMan()
	{
		SendRequest(m_C2SProxy.RequestQuayBacMayMan, string.Empty);
	}

	public bool OnQuayBacMayManResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		QuayBacMayManResponse quayBacMayManResponse = JsonMapper.ToObject<QuayBacMayManResponse>(data);
		if (quayBacMayManResponse != null)
		{
			if (quayBacMayManResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (quayBacMayManResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(quayBacMayManResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBacMayMan)
				{
					ScreenBacMayMan screenBacMayMan = GUIManager.getScreen(GAME_SCREEN.ScreenBacMayMan) as ScreenBacMayMan;
					screenBacMayMan.updateView(quayBacMayManResponse);
					screenBacMayMan.updateMainMenuView();
				}
			}
			else if (quayBacMayManResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(quayBacMayManResponse.ErrorMessage);
				EGDebug.LogWarning(quayBacMayManResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", quayBacMayManResponse.ErrorCode, quayBacMayManResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnQuayBacMayManResponse : response null");
		}
		return true;
	}

	public void RequestQuayTuBaoBon()
	{
		SendRequest(m_C2SProxy.RequestQuayTuBaoBon, string.Empty);
	}

	public bool OnQuayTuBaoBonResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		QuayTuBaoBonResponse quayTuBaoBonResponse = JsonMapper.ToObject<QuayTuBaoBonResponse>(data);
		if (quayTuBaoBonResponse != null)
		{
			if (quayTuBaoBonResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (quayTuBaoBonResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(quayTuBaoBonResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTuBaoBon)
				{
					ScreenKyNgo_TuBaoBon screenKyNgo_TuBaoBon = GUIManager.getScreen(GAME_SCREEN.ScreenTuBaoBon) as ScreenKyNgo_TuBaoBon;
					screenKyNgo_TuBaoBon.updateView(quayTuBaoBonResponse);
					screenKyNgo_TuBaoBon.updateMainMenuView();
				}
			}
			else if (quayTuBaoBonResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(quayTuBaoBonResponse.ErrorMessage);
				EGDebug.LogWarning(quayTuBaoBonResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", quayTuBaoBonResponse.ErrorCode, quayTuBaoBonResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnQuayTuBaoBonResponse : response null");
		}
		return true;
	}

	public void RequestGetLanhDiaInfo()
	{
		SendRequest(m_C2SProxy.RequestGetLanhDiaInfo, string.Empty);
	}

	public bool OnGetLanhDiaInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UpdateLanhDiaResponse updateLanhDiaResponse = JsonMapper.ToObject<UpdateLanhDiaResponse>(data);
		if (updateLanhDiaResponse != null)
		{
			if (updateLanhDiaResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (updateLanhDiaResponse.LanhDia != null)
				{
					UserInfo.UpdateInfo(updateLanhDiaResponse.LanhDia);
				}
				if (GUIManager.instance.CurrentScreen != GAME_SCREEN.ScreenLanhDiaMap)
				{
					GUIManager.instance.SetScreen(GAME_SCREEN.ScreenLanhDiaMap);
					((ScreenLanhDiaMap)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenLanhDiaMap)).Set();
					((ScreenLanhDiaMap)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenLanhDiaMap)).Set();
					GadgetPanelBottom gadgetPanelBottom = GUIManager.instance.gadgetPanelBottom;
					gadgetPanelBottom.bangHoiGrp.SetActive(false);
				}
			}
			else if (updateLanhDiaResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(updateLanhDiaResponse.ErrorMessage);
				EGDebug.LogWarning(updateLanhDiaResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", updateLanhDiaResponse.ErrorCode, updateLanhDiaResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetSonMonInfoResponse : response null");
		}
		return true;
	}

	public void RequestMoveLanhDia(string target)
	{
		MoveLanhDiaRequest moveLanhDiaRequest = new MoveLanhDiaRequest();
		moveLanhDiaRequest.TargetPosition = target;
		SendRequest(m_C2SProxy.RequestMoveLanhDia, JsonMapper.ToJson(moveLanhDiaRequest));
	}

	public bool OnMoveLanhDiaResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		MoveLanhDiaResponse moveLanhDiaResponse = JsonMapper.ToObject<MoveLanhDiaResponse>(data);
		if (moveLanhDiaResponse != null)
		{
			if (moveLanhDiaResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (moveLanhDiaResponse.LanhDia != null)
				{
					Debug.Log("OnMoveLanhDia update");
					UserInfo.UpdateInfo(moveLanhDiaResponse.LanhDia);
				}
				if (moveLanhDiaResponse.phanthuong != null)
				{
					PopupDanhSachPhanThuong.Create(string.Empty, string.Empty, moveLanhDiaResponse.phanthuong);
				}
				if (moveLanhDiaResponse.Replays != null && moveLanhDiaResponse.Replays.Count > 0)
				{
					((ScreenLanhDiaMap)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenLanhDiaMap)).StartBattle(moveLanhDiaResponse);
				}
				else
				{
					((ScreenLanhDiaMap)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenLanhDiaMap)).Sync();
				}
			}
			else if (moveLanhDiaResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(moveLanhDiaResponse.ErrorMessage);
				EGDebug.LogWarning(moveLanhDiaResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", moveLanhDiaResponse.ErrorCode, moveLanhDiaResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnMoveLanhDiaResponse : response null");
		}
		return true;
	}

	public bool OnUpdateLanhDiaResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		UpdateLanhDiaResponse updateLanhDiaResponse = JsonMapper.ToObject<UpdateLanhDiaResponse>(data);
		if (updateLanhDiaResponse != null)
		{
			if (updateLanhDiaResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (updateLanhDiaResponse.LanhDia != null)
				{
					UserInfo.UpdateInfo(updateLanhDiaResponse.LanhDia);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenLanhDiaMap)
				{
					((ScreenLanhDiaMap)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenLanhDiaMap)).Sync(!updateLanhDiaResponse.isPassive);
				}
			}
			else if (updateLanhDiaResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(updateLanhDiaResponse.ErrorMessage);
				EGDebug.LogWarning(updateLanhDiaResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", updateLanhDiaResponse.ErrorCode, updateLanhDiaResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnUpdateLanhDiaResponse : response null");
		}
		return true;
	}

	public void RequestGetTopMoRuong()
	{
		SendRequest(m_C2SProxy.RequestGetTopMoRuong, string.Empty);
	}

	public bool OnGetTopMoRuongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		TopEventMoRuongResponse topEventMoRuongResponse = JsonMapper.ToObject<TopEventMoRuongResponse>(data);
		if (topEventMoRuongResponse != null)
		{
			if (topEventMoRuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTopMoRuong)
				{
					ScreenTopMoRuong screenTopMoRuong = GUIManager.getScreen(GAME_SCREEN.ScreenTopMoRuong) as ScreenTopMoRuong;
					screenTopMoRuong.getTopMoRuong(topEventMoRuongResponse);
				}
			}
			else if (topEventMoRuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(topEventMoRuongResponse.ErrorMessage);
				EGDebug.LogWarning(topEventMoRuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", topEventMoRuongResponse.ErrorCode, topEventMoRuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnQuayTuBaoBonResponse : response null");
		}
		return true;
	}

	public void RequestGetTayVucInfo()
	{
		SendRequest(m_C2SProxy.RequestGetTayVucInfo, string.Empty, false);
	}

	public bool OnGetTayVucInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		TayVucInfoResponse tayVucInfoResponse = JsonMapper.ToObject<TayVucInfoResponse>(data);
		if (tayVucInfoResponse != null)
		{
			if (tayVucInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTayVucThuongNhan)
				{
					ScreenTayVucThuongNhan screenTayVucThuongNhan = GUIManager.getScreen(GAME_SCREEN.ScreenTayVucThuongNhan) as ScreenTayVucThuongNhan;
					screenTayVucThuongNhan.getListTayVuc(tayVucInfoResponse);
				}
			}
			else if (tayVucInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(tayVucInfoResponse.ErrorMessage);
				EGDebug.LogWarning(tayVucInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", tayVucInfoResponse.ErrorCode, tayVucInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetTayVucInfoResponse : response null");
		}
		return true;
	}

	public void RequestMuaDoTayVuc(MuaDoTayVucRequest request)
	{
		SendRequest(m_C2SProxy.RequestMuaDoTayVuc, JsonMapper.ToJson(request, false));
	}

	public bool OnGetMuaDoTayVucResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		MuaDoTayVucResponse muaDoTayVucResponse = JsonMapper.ToObject<MuaDoTayVucResponse>(data);
		if (muaDoTayVucResponse != null)
		{
			if (muaDoTayVucResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (muaDoTayVucResponse.PhanThuongResponse != null && muaDoTayVucResponse.PhanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(muaDoTayVucResponse.PhanThuongResponse.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTayVucThuongNhan)
				{
					ScreenTayVucThuongNhan screenTayVucThuongNhan = GUIManager.getScreen(GAME_SCREEN.ScreenTayVucThuongNhan) as ScreenTayVucThuongNhan;
					screenTayVucThuongNhan.getListTayVuc(muaDoTayVucResponse.UpdateTayVucInfo);
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), muaDoTayVucResponse.PhanThuongResponse);
			}
			else if (muaDoTayVucResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(muaDoTayVucResponse.ErrorMessage);
				EGDebug.LogWarning(muaDoTayVucResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", muaDoTayVucResponse.ErrorCode, muaDoTayVucResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetMuaDoTayVucResponse : response null");
		}
		return true;
	}

	public void RequestNhanThuongNapHangNgay(NhanThuongTichNapHangNgayRequest request)
	{
		SendRequest(m_C2SProxy.RequestNhanThuongNapHangNgay, JsonMapper.ToJson(request));
	}

	public bool OnNhanThuongTichNapHangNgayResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTichNapHangNgay)
				{
					ScreenTichNapHangNgay screenTichNapHangNgay = GUIManager.getScreen(GAME_SCREEN.ScreenTichNapHangNgay) as ScreenTichNapHangNgay;
					screenTichNapHangNgay.displayInfo();
					screenTichNapHangNgay.updateMainMenuView();
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), phanThuongResponse);
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("NhanThuongTichLuyNap : response null");
		}
		return true;
	}

	public void RequestQuayThienMaHaPhong(int heroID, int itemID, bool isCaoCap, int specialID)
	{
		QuayThienMaHaPhongRequest quayThienMaHaPhongRequest = new QuayThienMaHaPhongRequest();
		if (heroID > 0)
		{
			quayThienMaHaPhongRequest.HeroID = heroID;
			quayThienMaHaPhongRequest.IsFocus = true;
			quayThienMaHaPhongRequest.IsFocusCaoCap = isCaoCap;
			quayThienMaHaPhongRequest.SpecialItemID = specialID;
		}
		quayThienMaHaPhongRequest.ItemID = itemID;
		SendRequest(m_C2SProxy.RequestQuayThienMaHaPhong, JsonMapper.ToJson(quayThienMaHaPhongRequest, false));
	}

	public bool OnQuayThienMaHaPhongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		QuayThienMaHaPhongResponse quayThienMaHaPhongResponse = JsonMapper.ToObject<QuayThienMaHaPhongResponse>(data);
		if (quayThienMaHaPhongResponse != null)
		{
			if (quayThienMaHaPhongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (quayThienMaHaPhongResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(quayThienMaHaPhongResponse.UpdateInfo);
				}
				if (quayThienMaHaPhongResponse.phanthuong != null)
				{
					PopupDanhSachPhanThuong.Create(string.Empty, string.Empty, quayThienMaHaPhongResponse.phanthuong);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenThienMaHaPhong)
				{
					((ScreenThienMaHaPhong)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenThienMaHaPhong)).Sync(quayThienMaHaPhongResponse.HeroID, quayThienMaHaPhongResponse.BufValue);
				}
			}
			else if (quayThienMaHaPhongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(quayThienMaHaPhongResponse.ErrorMessage);
				EGDebug.LogWarning(quayThienMaHaPhongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", quayThienMaHaPhongResponse.ErrorCode, quayThienMaHaPhongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnQuayThienMaHaPhongResponse : response null");
		}
		return true;
	}

	public bool OnSpawnNienThuReponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SpawnNienThuResponse spawnNienThuResponse = JsonMapper.ToObject<SpawnNienThuResponse>(data);
		if (spawnNienThuResponse != null)
		{
			if (spawnNienThuResponse.ErrorCode == ERROR_CODE.OK)
			{
				GUIManager.instance.homeCity.InstantiateNienThuAvatar(spawnNienThuResponse.NienThu);
			}
			else if (spawnNienThuResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(spawnNienThuResponse.ErrorMessage);
				EGDebug.LogWarning(spawnNienThuResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", spawnNienThuResponse.ErrorCode, spawnNienThuResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSpawnNienThuReponse : response null");
		}
		return true;
	}

	public void RequestSummonNienThu()
	{
		SendRequest(m_C2SProxy.RequestSummonNienThu, string.Empty);
	}

	public bool OnSummonNienThuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SpawnNienThuResponse spawnNienThuResponse = JsonMapper.ToObject<SpawnNienThuResponse>(data);
		if (spawnNienThuResponse != null)
		{
			if (spawnNienThuResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (spawnNienThuResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(spawnNienThuResponse.UpdateInfo);
				}
				GUIManager.instance.SetScreen(GAME_SCREEN.ScreenMain);
				ScreenMain screenMain = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain;
				screenMain.JoinDanhNienThu(spawnNienThuResponse.NienThu);
				GUIManager.instance.homeCity.mainAvatar.Teleport(GUIManager.instance.homeCity.FightNienThuSpawnPoint.transform.position);
				HomeResponse.Position3D position3D = new HomeResponse.Position3D();
				position3D.X = GUIManager.instance.homeCity.FightNienThuSpawnPoint.transform.position.x + UnityEngine.Random.Range(-2f, 2f);
				position3D.Y = GUIManager.instance.homeCity.FightNienThuSpawnPoint.transform.position.y;
				position3D.Z = GUIManager.instance.homeCity.FightNienThuSpawnPoint.transform.position.z + UnityEngine.Random.Range(-2f, 2f);
				position3D.isNienThu = true;
				GUIManager.instance.homeCity.RemoveOfflinetUser();
				GameManager.instance.m_GameClient.C2SProxy.RequestChangeNextPos(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(position3D));
			}
			else if (spawnNienThuResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(spawnNienThuResponse.ErrorMessage);
				EGDebug.LogWarning(spawnNienThuResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", spawnNienThuResponse.ErrorCode, spawnNienThuResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnQuayThienMaHaPhongResponse : response null");
		}
		return true;
	}

	public void RequestThamGiaNienThu()
	{
		SendRequest(m_C2SProxy.RequestThamGiaNienThu, string.Empty);
	}

	public bool OnThamGiaNienThuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SpawnNienThuResponse spawnNienThuResponse = JsonMapper.ToObject<SpawnNienThuResponse>(data);
		if (spawnNienThuResponse != null)
		{
			if (spawnNienThuResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (spawnNienThuResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(spawnNienThuResponse.UpdateInfo);
				}
				GUIManager.instance.SetScreen(GAME_SCREEN.ScreenMain);
				ScreenMain screenMain = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain;
				screenMain.JoinDanhNienThu(spawnNienThuResponse.NienThu);
				GUIManager.instance.homeCity.mainAvatar.Teleport(GUIManager.instance.homeCity.FightNienThuSpawnPoint.transform.position);
				HomeResponse.Position3D position3D = new HomeResponse.Position3D();
				position3D.X = GUIManager.instance.homeCity.FightNienThuSpawnPoint.transform.position.x + UnityEngine.Random.Range(-2f, 2f);
				position3D.Y = GUIManager.instance.homeCity.FightNienThuSpawnPoint.transform.position.y;
				position3D.Z = GUIManager.instance.homeCity.FightNienThuSpawnPoint.transform.position.z + UnityEngine.Random.Range(-2f, 2f);
				position3D.isNienThu = true;
				GUIManager.instance.homeCity.RemoveOfflinetUser();
				GameManager.instance.m_GameClient.C2SProxy.RequestChangeNextPos(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(position3D));
			}
			else if (spawnNienThuResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(spawnNienThuResponse.ErrorMessage);
				EGDebug.LogWarning(spawnNienThuResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", spawnNienThuResponse.ErrorCode, spawnNienThuResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnThamGiaNienThuResponse : response null");
		}
		return true;
	}

	public void RequestDanhNienThu()
	{
		DanhNienThuRequest danhNienThuRequest = new DanhNienThuRequest();
		danhNienThuRequest.Value = GUIManager.instance.homeCity.NienThuAvatar3D.GetComponent<NienThuAvatar3D>().NienThu.SecretValue;
		SendRequest(m_C2SProxy.RequestDanhNienThu, JsonMapper.ToJson(danhNienThuRequest));
	}

	public bool OnDanhNienThuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DanhNienThuResponse danhNienThuResponse = JsonMapper.ToObject<DanhNienThuResponse>(data);
		if (danhNienThuResponse != null)
		{
			if (danhNienThuResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (danhNienThuResponse.phanthuong != null && danhNienThuResponse.phanthuong.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(danhNienThuResponse.phanthuong.UpdateUserInfo);
					PopupDanhSachPhanThuong.Create(string.Empty, string.Empty, danhNienThuResponse.phanthuong);
				}
				if (!string.IsNullOrEmpty(danhNienThuResponse.ErrorMessage))
				{
					MessagePopup.Create(danhNienThuResponse.ErrorMessage);
				}
				if (danhNienThuResponse.NienThu != null)
				{
					GUIManager.instance.SetScreen(GAME_SCREEN.ScreenMain);
					ScreenMain screenMain = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain;
					screenMain.JoinDanhNienThu(danhNienThuResponse.NienThu);
					GUIManager.instance.homeCity.mainAvatar.Teleport(GUIManager.instance.homeCity.FightNienThuSpawnPoint.transform.position);
					HomeResponse.Position3D position3D = new HomeResponse.Position3D();
					position3D.X = GUIManager.instance.homeCity.FightNienThuSpawnPoint.transform.position.x + UnityEngine.Random.Range(-2f, 2f);
					position3D.Y = GUIManager.instance.homeCity.FightNienThuSpawnPoint.transform.position.y;
					position3D.Z = GUIManager.instance.homeCity.FightNienThuSpawnPoint.transform.position.z + UnityEngine.Random.Range(-2f, 2f);
					position3D.isNienThu = true;
					GameManager.instance.m_GameClient.C2SProxy.RequestChangeNextPos(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(position3D));
				}
				else
				{
					ScreenMain screenMain2 = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain;
					screenMain2.ThoatDanhNienThu();
				}
			}
			else if (danhNienThuResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				if (danhNienThuResponse.NienThu != null)
				{
					GUIManager.instance.SetScreen(GAME_SCREEN.ScreenMain);
					ScreenMain screenMain3 = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain;
					screenMain3.JoinDanhNienThu(danhNienThuResponse.NienThu);
				}
				MessagePopup.Create(danhNienThuResponse.ErrorMessage);
				EGDebug.LogWarning(danhNienThuResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", danhNienThuResponse.ErrorCode, danhNienThuResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnDanhNienThuResponse : response null");
		}
		return true;
	}

	public bool OnUpdateNienThuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DanhNienThuResponse danhNienThuResponse = JsonMapper.ToObject<DanhNienThuResponse>(data);
		if (danhNienThuResponse != null)
		{
			if (danhNienThuResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (!string.IsNullOrEmpty(danhNienThuResponse.ErrorMessage))
				{
					MessagePopup.Create(danhNienThuResponse.ErrorMessage);
				}
				if (danhNienThuResponse.NienThu != null)
				{
					ScreenMain screenMain = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain;
					screenMain.JoinDanhNienThu(danhNienThuResponse.NienThu);
				}
				else
				{
					ScreenMain screenMain2 = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain;
					screenMain2.ThoatDanhNienThu();
				}
			}
			else if (danhNienThuResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				if (danhNienThuResponse.NienThu != null)
				{
					GUIManager.instance.SetScreen(GAME_SCREEN.ScreenMain);
					ScreenMain screenMain3 = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMain) as ScreenMain;
					screenMain3.JoinDanhNienThu(danhNienThuResponse.NienThu);
				}
				MessagePopup.Create(danhNienThuResponse.ErrorMessage);
				EGDebug.LogWarning(danhNienThuResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", danhNienThuResponse.ErrorCode, danhNienThuResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnDanhNienThuResponse : response null");
		}
		return true;
	}

	public void RequestNopLenhBaiNienThu(int itemID, int quantity)
	{
		NopLenhBaiNienThuRequest nopLenhBaiNienThuRequest = new NopLenhBaiNienThuRequest();
		nopLenhBaiNienThuRequest.ItemID = itemID;
		nopLenhBaiNienThuRequest.Quantity = quantity;
		SendRequest(m_C2SProxy.RequestDanhNienThu, JsonMapper.ToJson(nopLenhBaiNienThuRequest));
	}

	public bool OnNopLenhBaiNienThuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		NopLenhBaiNienThuResponse nopLenhBaiNienThuResponse = JsonMapper.ToObject<NopLenhBaiNienThuResponse>(data);
		if (nopLenhBaiNienThuResponse != null)
		{
			if (nopLenhBaiNienThuResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (nopLenhBaiNienThuResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(nopLenhBaiNienThuResponse.UpdateInfo);
				}
				if (!string.IsNullOrEmpty(nopLenhBaiNienThuResponse.ErrorMessage))
				{
					MessagePopup.Create(nopLenhBaiNienThuResponse.ErrorMessage);
				}
				ScreenLienMinhNienThu screenLienMinhNienThu = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenLienMinhNienThu) as ScreenLienMinhNienThu;
				screenLienMinhNienThu.OnEnable();
			}
			else if (nopLenhBaiNienThuResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(nopLenhBaiNienThuResponse.ErrorMessage);
				EGDebug.LogWarning(nopLenhBaiNienThuResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", nopLenhBaiNienThuResponse.ErrorCode, nopLenhBaiNienThuResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnNopLenhBaiNienThuResponse : response null");
		}
		return true;
	}

	public void RequestGetTopNienThu()
	{
		SendRequest(m_C2SProxy.RequestGetTopNienThu, string.Empty);
	}

	public bool OnGetTopNienThuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetTopNienThuResponse getTopNienThuResponse = JsonMapper.ToObject<GetTopNienThuResponse>(data);
		if (getTopNienThuResponse != null)
		{
			if (getTopNienThuResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (!string.IsNullOrEmpty(getTopNienThuResponse.ErrorMessage))
				{
					MessagePopup.Create(getTopNienThuResponse.ErrorMessage);
				}
				PopupTopNienThu.Create(getTopNienThuResponse.LienMinhList, getTopNienThuResponse.LienMinhScore);
			}
			else if (getTopNienThuResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getTopNienThuResponse.ErrorMessage);
				EGDebug.LogWarning(getTopNienThuResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getTopNienThuResponse.ErrorCode, getTopNienThuResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnNopLenhBaiNienThuResponse : response null");
		}
		return true;
	}

	public void RequestSelectStartNgua(SelectStartNguaRequest request)
	{
		SendRequest(m_C2SProxy.RequestSelectStartNgua, JsonMapper.ToJson(request, false));
	}

	public bool OnSelectStartNguaResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SelectStartNguaResponse selectStartNguaResponse = JsonMapper.ToObject<SelectStartNguaResponse>(data);
		if (selectStartNguaResponse != null)
		{
			if (selectStartNguaResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (selectStartNguaResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(selectStartNguaResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenSelectTheFirstHorse)
				{
					if (UserInfo.ThuCuoi.ThuCuoiList != null && UserInfo.ThuCuoi.ThuCuoiList.Count == 1)
					{
						ActiveNguaRequest activeNguaRequest = new ActiveNguaRequest();
						activeNguaRequest.id = UserInfo.ThuCuoi.ThuCuoiList[0].ID;
						RequestActiveNgua(activeNguaRequest);
					}
					else
					{
						EGDebug.LogError("Khong set dc ngua");
					}
					if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenSelectTheFirstHorse)
					{
						GUIManager.instance.SetScreen(GAME_SCREEN.ScreenMain);
						if (UserInfo.Gamer.DisplayName == string.Empty)
						{
							PopupDatTenMonPhai.CreateDatTenLanDau();
						}
						else
						{
							PopupLoginMessage.Create(UserInfo.ServerInfo);
						}
					}
				}
			}
			else if (selectStartNguaResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(selectStartNguaResponse.ErrorMessage);
				EGDebug.LogWarning(selectStartNguaResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", selectStartNguaResponse.ErrorCode, selectStartNguaResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetNoiCongBatQuaiResponse : response null");
		}
		return true;
	}

	public void RequestGetDiemMoRuong()
	{
		SendRequest(m_C2SProxy.RequestGetDiemMoRuong, string.Empty, false);
	}

	public bool OnGetDiemMoRuongResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetDiemMoRuongResponse getDiemMoRuongResponse = JsonMapper.ToObject<GetDiemMoRuongResponse>(data);
		if (getDiemMoRuongResponse != null)
		{
			if (getDiemMoRuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenEventMoRuong)
				{
					ScreenEventMoRuong screenEventMoRuong = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenEventMoRuong) as ScreenEventMoRuong;
					screenEventMoRuong.lbRuongHT.text = string.Format(Localization.instance.Get("SoRuongDaMoLabel"), getDiemMoRuongResponse.DiemMoRuong);
				}
			}
			else if (getDiemMoRuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getDiemMoRuongResponse.ErrorMessage);
				EGDebug.LogWarning(getDiemMoRuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getDiemMoRuongResponse.ErrorCode, getDiemMoRuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetDiemMoRuongResponse : response null");
		}
		return true;
	}

	public void RequestUpdateDailyActivities(string key)
	{
		SendRequest(m_C2SProxy.RequestUpdateDailyActivities, "\"" + key + "\"", false);
	}

	public bool OnUpdateDailyActivitiesReponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnUpdateDailyActivitiesReponse : response null");
		}
		return true;
	}

	public void RequestThuongDailyActivities(string key)
	{
		SendRequest(m_C2SProxy.RequestThuongDailyActivities, "\"" + key + "\"");
	}

	public bool OnThuongDailyActivitiesResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				if (phanThuongResponse.PhanThuongList != null)
				{
					PopupDanhSachPhanThuong.Create(phanThuongResponse.PhanThuongTitle, phanThuongResponse.PhanThuongDesc, phanThuongResponse);
				}
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnThuongDailyActivitiesResponse : response null");
		}
		return true;
	}

	public void RequestThienMaQuaySlot(SetThienMaLenhRequest request)
	{
		SendRequest(m_C2SProxy.RequestThienMaQuaySlot, JsonMapper.ToJson(request));
	}

	public bool OnThienMaQuaySlotReponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThienMaLenhResponse thienMaLenhResponse = JsonMapper.ToObject<ThienMaLenhResponse>(data);
		if (thienMaLenhResponse != null)
		{
			if (thienMaLenhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thienMaLenhResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(thienMaLenhResponse.UpdateInfo);
				}
				if (thienMaLenhResponse.ThienMaLenh != null && GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBaoKhi)
				{
					ScreenBaoKhi screenBaoKhi = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenBaoKhi) as ScreenBaoKhi;
					screenBaoKhi.startQuay(thienMaLenhResponse.ThienMaLenh, true);
				}
			}
			else if (thienMaLenhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thienMaLenhResponse.ErrorMessage);
				EGDebug.LogWarning(thienMaLenhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thienMaLenhResponse.ErrorCode, thienMaLenhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnThienMaQuaySlotReponse : response null");
		}
		return true;
	}

	public void RequestThienMaQuaySlotConfirm(bool isOK)
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		m_C2SProxy.RequestThienMaQuaySlotConfirm(HostID.Server, RmiContext.ReliableSend, isOK);
	}

	public bool OnThienMaQuaySlotConfirmReponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThienMaLenhResponse response = JsonMapper.ToObject<ThienMaLenhResponse>(data);
		if (response != null)
		{
			if (response.ErrorCode == ERROR_CODE.OK)
			{
				if (response.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(response.UpdateInfo);
				}
				if (response.ThienMaLenh != null && GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBaoKhi)
				{
					ScreenBaoKhi screenBaoKhi = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenBaoKhi) as ScreenBaoKhi;
					screenBaoKhi.Set(m_UserInfo.ListThienMaLenh.Find((UserInfo.ThienMaLenhInfo vp) => vp.ID == response.ThienMaLenh.ID));
					screenBaoKhi.displayInfo();
				}
			}
			else if (response.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(response.ErrorMessage);
				EGDebug.LogWarning(response.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", response.ErrorCode, response.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnThienMaQuaySlotConfirmReponse : response null");
		}
		return true;
	}

	public void RequestThienMaLenhCreate(SetThienMaLenhRequest request)
	{
		SendRequest(m_C2SProxy.RequestThienMaLenhCreate, JsonMapper.ToJson(request));
	}

	public bool OnThienMaLenhCreateReponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThienMaLenhResponse thienMaLenhResponse = JsonMapper.ToObject<ThienMaLenhResponse>(data);
		if (thienMaLenhResponse != null)
		{
			if (thienMaLenhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thienMaLenhResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(thienMaLenhResponse.UpdateInfo);
				}
				if (thienMaLenhResponse.ThienMaLenh != null)
				{
					ScreenBaoKhi screenBaoKhi = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenBaoKhi) as ScreenBaoKhi;
					GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBaoKhi);
					screenBaoKhi.curBaoKhiData = null;
					screenBaoKhi.displayInfo();
					screenBaoKhi.startQuay(thienMaLenhResponse.ThienMaLenh, false, CardIndex.ECI_1);
				}
			}
			else if (thienMaLenhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thienMaLenhResponse.ErrorMessage);
				EGDebug.LogWarning(thienMaLenhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thienMaLenhResponse.ErrorCode, thienMaLenhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnThienMaLenhCreateReponse : response null");
		}
		return true;
	}

	public void RequestThienMaOpenSlot(SetThienMaLenhRequest request)
	{
		SendRequest(m_C2SProxy.RequestThienMaOpenSlot, JsonMapper.ToJson(request));
	}

	public bool OnThienMaOpenSlotReponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThienMaLenhResponse thienMaLenhResponse = JsonMapper.ToObject<ThienMaLenhResponse>(data);
		if (thienMaLenhResponse != null)
		{
			if (thienMaLenhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thienMaLenhResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(thienMaLenhResponse.UpdateInfo);
				}
				if (thienMaLenhResponse.ThienMaLenh != null && GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBaoKhi)
				{
					ScreenBaoKhi screenBaoKhi = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenBaoKhi) as ScreenBaoKhi;
					screenBaoKhi.startQuay(thienMaLenhResponse.ThienMaLenh, false);
				}
			}
			else if (thienMaLenhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thienMaLenhResponse.ErrorMessage);
				EGDebug.LogWarning(thienMaLenhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thienMaLenhResponse.ErrorCode, thienMaLenhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnThienMaQuaySlotReponse : response null");
		}
		return true;
	}

	public void RequestThienMaUpgradeSlot(SetThienMaLenhRequest request)
	{
		SendRequest(m_C2SProxy.RequestThienMaUpgradeSlot, JsonMapper.ToJson(request));
	}

	public bool OnThienMaUpgradeSlotReponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThienMaLenhResponse thienMaLenhResponse = JsonMapper.ToObject<ThienMaLenhResponse>(data);
		if (thienMaLenhResponse != null)
		{
			if (thienMaLenhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thienMaLenhResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(thienMaLenhResponse.UpdateInfo);
				}
				if (thienMaLenhResponse.ThienMaLenh != null && GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBaoKhi)
				{
					ScreenBaoKhi screenBaoKhi = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenBaoKhi) as ScreenBaoKhi;
					screenBaoKhi.Set(thienMaLenhResponse.ThienMaLenh);
					screenBaoKhi.displayInfo();
					screenBaoKhi.displayPopupResult();
				}
			}
			else if (thienMaLenhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thienMaLenhResponse.ErrorMessage);
				EGDebug.LogWarning(thienMaLenhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thienMaLenhResponse.ErrorCode, thienMaLenhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnThienMaQuaySlotReponse : response null");
		}
		return true;
	}

	public void RequestThienMaEquip(SetThienMaLenhRequest request)
	{
		SendRequest(m_C2SProxy.RequestThienMaEquip, JsonMapper.ToJson(request));
	}

	public bool OnNotifyThienMaEquipReponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThienMaLenhResponse thienMaLenhResponse = JsonMapper.ToObject<ThienMaLenhResponse>(data);
		if (thienMaLenhResponse != null)
		{
			if (thienMaLenhResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thienMaLenhResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(thienMaLenhResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDoiHinh)
				{
					ScreenDoiHinh screenDoiHinh = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
					screenDoiHinh.SyncWithNetworkData();
				}
			}
			else if (thienMaLenhResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thienMaLenhResponse.ErrorMessage);
				EGDebug.LogWarning(thienMaLenhResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thienMaLenhResponse.ErrorCode, thienMaLenhResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnThienMaQuaySlotReponse : response null");
		}
		return true;
	}

	public void RequestSetTonHieu(SetTonHieuRequest request)
	{
		SendRequest(m_C2SProxy.RequestSetTonHieu, JsonMapper.ToJson(request));
	}

	public bool OnSetTonHieuResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SetTonHieuReponse setTonHieuReponse = JsonMapper.ToObject<SetTonHieuReponse>(data);
		if (setTonHieuReponse != null)
		{
			if (setTonHieuReponse.ErrorCode == ERROR_CODE.OK)
			{
				if (setTonHieuReponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(setTonHieuReponse.updateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTonHieu)
				{
					ScreenTonHieu screenTonHieu = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenTonHieu) as ScreenTonHieu;
					screenTonHieu.updateView();
				}
				UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
				GUIManager.instance.homeCity.mainAvatar.SetInfoUser(userInfo.Gamer.DisplayName, userInfo.Gamer.GhiChuTrongNgay, userInfo.Gamer.CurTonHieu, userInfo.Gamer.Level, userInfo.GiangHo.Count);
			}
			else if (setTonHieuReponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(setTonHieuReponse.ErrorMessage);
				EGDebug.LogWarning(setTonHieuReponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", setTonHieuReponse.ErrorCode, setTonHieuReponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetTonHieuResponse : response null");
		}
		return true;
	}

	public void RequestSetTrangBiHoangKim(SetTrangBiHoangKimRequest request)
	{
		SendRequest(m_C2SProxy.RequestSetTrangBiHoangKim, JsonMapper.ToJson(request));
	}

	public bool OnSetTrangBiHoangKimResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		SetTrangBiHoangKimReponse setTrangBiHoangKimReponse = JsonMapper.ToObject<SetTrangBiHoangKimReponse>(data);
		if (setTrangBiHoangKimReponse != null)
		{
			if (setTrangBiHoangKimReponse.ErrorCode == ERROR_CODE.OK)
			{
				if (setTrangBiHoangKimReponse.updateInfo != null)
				{
					UserInfo.UpdateInfo(setTrangBiHoangKimReponse.updateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTrangBiHoangKim)
				{
					ScreenTrangBiHoangKim screenTrangBiHoangKim = GUIManager.getScreen(GAME_SCREEN.ScreenTrangBiHoangKim) as ScreenTrangBiHoangKim;
					screenTrangBiHoangKim.startPlayAnim(setTrangBiHoangKimReponse);
				}
			}
			else if (setTrangBiHoangKimReponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(setTrangBiHoangKimReponse.ErrorMessage);
				EGDebug.LogWarning(setTrangBiHoangKimReponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", setTrangBiHoangKimReponse.ErrorCode, setTrangBiHoangKimReponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetTrangBiHoangKimResponse : response null");
		}
		return true;
	}

	public void RequestDanhAnDanhCaoThu()
	{
		m_C2SProxy.RequestDanhAnDanhCaoThu(HostID.Server, RmiContext.ReliableSend);
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public bool OnDanhAnDanhCaoThu(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		DanhAnDanhCaoThuResponse danhAnDanhCaoThuResponse = JsonMapper.ToObject<DanhAnDanhCaoThuResponse>(data);
		if (danhAnDanhCaoThuResponse != null)
		{
			if (danhAnDanhCaoThuResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (danhAnDanhCaoThuResponse.replay != null)
				{
					ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
					GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
					screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenAnTheCaoThu;
					List<UserInfo.HeroData> listCloneHeroFromDoiHinh = UserInfo.GetListCloneHeroFromDoiHinh();
					PopupBattleResult popupBattleResult = PopupBattleResult.Create(danhAnDanhCaoThuResponse.replay, listCloneHeroFromDoiHinh, 0L, 0L, 0L, 0);
					popupBattleResult.gameObject.SetActive(false);
					if (danhAnDanhCaoThuResponse.phanthuong != null && danhAnDanhCaoThuResponse.phanthuong.PhanThuongList != null && danhAnDanhCaoThuResponse.phanthuong.PhanThuongList.Count > 0)
					{
						if (danhAnDanhCaoThuResponse.phanthuong.UpdateUserInfo != null && danhAnDanhCaoThuResponse.phanthuong.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
						{
							UserInfo.UpdateInfo(danhAnDanhCaoThuResponse.phanthuong.UpdateUserInfo);
						}
						PopupDanhSachPhanThuong popupDanhSachPhanThuong = PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), danhAnDanhCaoThuResponse.phanthuong);
						popupDanhSachPhanThuong.gameObject.SetActive(false);
					}
					screenBattle.Replay(danhAnDanhCaoThuResponse.replay);
					screenBattle.OnFinishReplay += OnDanhAnDanhCaoThuFinish;
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenAnTheCaoThu)
				{
					ScreenAnTheCaoThu screenAnTheCaoThu = GUIManager.getScreen(GAME_SCREEN.ScreenTrangBiHoangKim) as ScreenAnTheCaoThu;
					screenAnTheCaoThu.updateView();
				}
			}
			else if (danhAnDanhCaoThuResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(danhAnDanhCaoThuResponse.ErrorMessage);
				EGDebug.LogWarning(danhAnDanhCaoThuResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", danhAnDanhCaoThuResponse.ErrorCode, danhAnDanhCaoThuResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnSetTrangBiHoangKimResponse : response null");
		}
		return true;
	}

	public static void OnDanhAnDanhCaoThuFinish()
	{
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.instance.gameObject.SetActive(true);
		}
		if (PopupBattleResult.instance != null)
		{
			PopupBattleResult.instance.gameObject.SetActive(true);
		}
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		screenBattle.OnFinishReplay -= OnEndBattleBatCoc;
	}

	public void RequestStartBoiDuongTrangBi(BoiDuongTrangBiHoangKimRequest request)
	{
		SendRequest(m_C2SProxy.RequestStartBoiDuongTrangBi, JsonMapper.ToJson(request));
	}

	public bool OnStartBoiDuongTrangBiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		StartBoiDuongTrangBiResponse startBoiDuongTrangBiResponse = JsonMapper.ToObject<StartBoiDuongTrangBiResponse>(data);
		if (startBoiDuongTrangBiResponse != null)
		{
			if (startBoiDuongTrangBiResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (startBoiDuongTrangBiResponse.UpdateInfo != null && startBoiDuongTrangBiResponse.UpdateInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(startBoiDuongTrangBiResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBoiDuongTrangBiHoangKim)
				{
					ScreenBoiDuongTrangBiHoangKim screenBoiDuongTrangBiHoangKim = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenBoiDuongTrangBiHoangKim) as ScreenBoiDuongTrangBiHoangKim;
					screenBoiDuongTrangBiHoangKim.openScreenBDConfirm(startBoiDuongTrangBiResponse);
				}
			}
			else if (startBoiDuongTrangBiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(startBoiDuongTrangBiResponse.ErrorMessage);
				EGDebug.LogWarning(startBoiDuongTrangBiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", startBoiDuongTrangBiResponse.ErrorCode, startBoiDuongTrangBiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnStartBoiDuongTrangBiResponse : response null");
		}
		return true;
	}

	public void RequestEndBoiDuongTrangBi(EndBoiDuongTrangBiRequest request)
	{
		SendRequest(m_C2SProxy.RequestEndBoiDuongTrangBi, JsonMapper.ToJson(request));
	}

	public bool OnEndBoiDuongTrangBiResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		EndBoiDuongTrangBiResponse endBoiDuongTrangBiResponse = JsonMapper.ToObject<EndBoiDuongTrangBiResponse>(data);
		if (endBoiDuongTrangBiResponse != null)
		{
			if (endBoiDuongTrangBiResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (endBoiDuongTrangBiResponse.UpdateInfo != null && endBoiDuongTrangBiResponse.UpdateInfo.ErrorCode == ERROR_CODE.OK)
				{
					UserInfo.UpdateInfo(endBoiDuongTrangBiResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBoiDuongTrangBiResult)
				{
					ScreenBoiDuongTrangBiResult screenBoiDuongTrangBiResult = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenBoiDuongTrangBiResult) as ScreenBoiDuongTrangBiResult;
					screenBoiDuongTrangBiResult.updateResult(endBoiDuongTrangBiResponse);
				}
			}
			else if (endBoiDuongTrangBiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(endBoiDuongTrangBiResponse.ErrorMessage);
				EGDebug.LogWarning(endBoiDuongTrangBiResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", endBoiDuongTrangBiResponse.ErrorCode, endBoiDuongTrangBiResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnStartBoiDuongTrangBiResponse : response null");
		}
		return true;
	}

	public void RequestRutQueTienNhan()
	{
		m_C2SProxy.RequestRutQueTienNhan(HostID.Server, RmiContext.ReliableSend);
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public bool OnRutQueTienNhanResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse != null && phanThuongResponse.PhanThuongList != null && phanThuongResponse.PhanThuongList.Count > 0)
				{
					if (phanThuongResponse.UpdateUserInfo != null && phanThuongResponse.UpdateUserInfo.ErrorCode == ERROR_CODE.OK)
					{
						UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
					}
					if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenTienNhanChiLo)
					{
						ScreenTienNhanChiLo screenTienNhanChiLo = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenTienNhanChiLo) as ScreenTienNhanChiLo;
						screenTienNhanChiLo.displayResponse(phanThuongResponse);
					}
				}
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnRutQueTienNhanResponse : response null");
		}
		return true;
	}

	public void RequestGetBaoKhoInfo()
	{
		m_C2SProxy.RequestGetBaoKhoInfo(HostID.Server, RmiContext.ReliableSend);
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public bool OnGetBaoKhoInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetBaoKhoInfoResponse getBaoKhoInfoResponse = JsonMapper.ToObject<GetBaoKhoInfoResponse>(data);
		if (getBaoKhoInfoResponse != null)
		{
			if (getBaoKhoInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				BaoKhoInfoResponse = getBaoKhoInfoResponse;
				LastTimeGetBaoKhoInfo = GameManager.instance.m_GameClient.ServerTime;
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBaoKhoMain)
				{
					ScreenBaoKhoMain screenBaoKhoMain = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenBaoKhoMain) as ScreenBaoKhoMain;
					screenBaoKhoMain.displayInfo(getBaoKhoInfoResponse);
				}
			}
			else if (getBaoKhoInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getBaoKhoInfoResponse.ErrorMessage);
				EGDebug.LogWarning(getBaoKhoInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getBaoKhoInfoResponse.ErrorCode, getBaoKhoInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetBaoKhoInfoResponse : response null");
		}
		return true;
	}

	private void setBaoKho(List<UserInfo.BaoKhoInfo> listBK, UserInfo.BaoKhoInfo curBaoKho)
	{
		if (listBK == null || listBK.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < listBK.Count; i++)
		{
			if (listBK[i].ID == curBaoKho.ID)
			{
				listBK[i].GID = curBaoKho.GID;
				listBK[i].SID = curBaoKho.SID;
				listBK[i].Level = curBaoKho.Level;
				listBK[i].Vip = curBaoKho.Vip;
				listBK[i].CumServer = curBaoKho.CumServer;
				listBK[i].DisplayName = curBaoKho.DisplayName;
				listBK[i].TimeChiem = curBaoKho.TimeChiem;
				listBK[i].LastTimeThuHoach = curBaoKho.LastTimeThuHoach;
			}
		}
	}

	public void RequestCuopBaoKho(CuopBaoKhoRequest request)
	{
		SendRequest(m_C2SProxy.RequestCuopBaoKho, JsonMapper.ToJson(request));
	}

	public bool OnCuopBaoKhoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		CuopBaoKhoReponse cuopBaoKhoReponse = JsonMapper.ToObject<CuopBaoKhoReponse>(data);
		if (cuopBaoKhoReponse != null)
		{
			if (cuopBaoKhoReponse.ErrorCode == ERROR_CODE.OK)
			{
				if (cuopBaoKhoReponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(cuopBaoKhoReponse.UpdateInfo);
				}
				if (cuopBaoKhoReponse.IsSuccess)
				{
					if (cuopBaoKhoReponse.BaoKhoData != null && BaoKhoInfoResponse != null)
					{
						if (cuopBaoKhoReponse.BaoKhoData.BaoKhoType == UserInfo.BaoKhoInfo.LoaiBaoKho.NGOC)
						{
							for (int i = 0; i < BaoKhoInfoResponse.listBaoKhoNgoc.Count; i++)
							{
								if (BaoKhoInfoResponse.listBaoKhoNgoc[i].ID == cuopBaoKhoReponse.BaoKhoData.ID)
								{
									BaoKhoInfoResponse.listBaoKhoNgoc[i].GID = cuopBaoKhoReponse.BaoKhoData.GID;
									BaoKhoInfoResponse.listBaoKhoNgoc[i].SID = cuopBaoKhoReponse.BaoKhoData.SID;
									BaoKhoInfoResponse.listBaoKhoNgoc[i].Level = cuopBaoKhoReponse.BaoKhoData.Level;
									BaoKhoInfoResponse.listBaoKhoNgoc[i].Vip = cuopBaoKhoReponse.BaoKhoData.Vip;
									BaoKhoInfoResponse.listBaoKhoNgoc[i].CumServer = cuopBaoKhoReponse.BaoKhoData.CumServer;
									BaoKhoInfoResponse.listBaoKhoNgoc[i].DisplayName = cuopBaoKhoReponse.BaoKhoData.DisplayName;
									BaoKhoInfoResponse.listBaoKhoNgoc[i].TimeChiem = cuopBaoKhoReponse.BaoKhoData.TimeChiem;
									BaoKhoInfoResponse.listBaoKhoNgoc[i].LastTimeThuHoach = cuopBaoKhoReponse.BaoKhoData.LastTimeThuHoach;
								}
							}
						}
						if (cuopBaoKhoReponse.BaoKhoData.BaoKhoType == UserInfo.BaoKhoInfo.LoaiBaoKho.KIM)
						{
							for (int j = 0; j < BaoKhoInfoResponse.listBaoKhoKim.Count; j++)
							{
								if (BaoKhoInfoResponse.listBaoKhoKim[j].ID == cuopBaoKhoReponse.BaoKhoData.ID)
								{
									BaoKhoInfoResponse.listBaoKhoKim[j].GID = cuopBaoKhoReponse.BaoKhoData.GID;
									BaoKhoInfoResponse.listBaoKhoKim[j].SID = cuopBaoKhoReponse.BaoKhoData.SID;
									BaoKhoInfoResponse.listBaoKhoKim[j].Level = cuopBaoKhoReponse.BaoKhoData.Level;
									BaoKhoInfoResponse.listBaoKhoKim[j].Vip = cuopBaoKhoReponse.BaoKhoData.Vip;
									BaoKhoInfoResponse.listBaoKhoKim[j].CumServer = cuopBaoKhoReponse.BaoKhoData.CumServer;
									BaoKhoInfoResponse.listBaoKhoKim[j].DisplayName = cuopBaoKhoReponse.BaoKhoData.DisplayName;
									BaoKhoInfoResponse.listBaoKhoKim[j].TimeChiem = cuopBaoKhoReponse.BaoKhoData.TimeChiem;
									BaoKhoInfoResponse.listBaoKhoKim[j].LastTimeThuHoach = cuopBaoKhoReponse.BaoKhoData.LastTimeThuHoach;
								}
							}
						}
						bool flag = false;
						for (int k = 0; k < BaoKhoInfoResponse.listMyBaoKho.Count; k++)
						{
							if (BaoKhoInfoResponse.listMyBaoKho[k].ID == cuopBaoKhoReponse.BaoKhoData.ID)
							{
								flag = true;
								BaoKhoInfoResponse.listMyBaoKho[k].GID = cuopBaoKhoReponse.BaoKhoData.GID;
								BaoKhoInfoResponse.listMyBaoKho[k].SID = cuopBaoKhoReponse.BaoKhoData.SID;
								BaoKhoInfoResponse.listMyBaoKho[k].Level = cuopBaoKhoReponse.BaoKhoData.Level;
								BaoKhoInfoResponse.listMyBaoKho[k].Vip = cuopBaoKhoReponse.BaoKhoData.Vip;
								BaoKhoInfoResponse.listMyBaoKho[k].CumServer = cuopBaoKhoReponse.BaoKhoData.CumServer;
								BaoKhoInfoResponse.listMyBaoKho[k].DisplayName = cuopBaoKhoReponse.BaoKhoData.DisplayName;
								BaoKhoInfoResponse.listMyBaoKho[k].TimeChiem = cuopBaoKhoReponse.BaoKhoData.TimeChiem;
								BaoKhoInfoResponse.listMyBaoKho[k].LastTimeThuHoach = cuopBaoKhoReponse.BaoKhoData.LastTimeThuHoach;
							}
						}
						if (!flag && BaoKhoInfoResponse.listMyBaoKho.Count < 2)
						{
							BaoKhoInfoResponse.listMyBaoKho.Add(cuopBaoKhoReponse.BaoKhoData);
						}
					}
					if (cuopBaoKhoReponse.UpdateBaoKhoInfo != null)
					{
						if (cuopBaoKhoReponse.UpdateBaoKhoInfo.listMyBaoKho != null)
						{
							BaoKhoInfoResponse.listMyBaoKho.Clear();
							for (int l = 0; l < cuopBaoKhoReponse.UpdateBaoKhoInfo.listMyBaoKho.Count; l++)
							{
								UserInfo.BaoKhoInfo baoKhoInfo = cuopBaoKhoReponse.UpdateBaoKhoInfo.listMyBaoKho[l];
								BaoKhoInfoResponse.listMyBaoKho.Add(baoKhoInfo);
								if (baoKhoInfo.BaoKhoType == UserInfo.BaoKhoInfo.LoaiBaoKho.NGOC)
								{
									setBaoKho(BaoKhoInfoResponse.listBaoKhoNgoc, baoKhoInfo);
								}
								if (baoKhoInfo.BaoKhoType == UserInfo.BaoKhoInfo.LoaiBaoKho.KIM)
								{
									setBaoKho(BaoKhoInfoResponse.listBaoKhoKim, baoKhoInfo);
								}
							}
						}
						if (cuopBaoKhoReponse.UpdateBaoKhoInfo.listBaoKhoNgoc != null && cuopBaoKhoReponse.UpdateBaoKhoInfo.listBaoKhoNgoc.Count > 0)
						{
							BaoKhoInfoResponse.listBaoKhoNgoc.Clear();
							for (int m = 0; m < cuopBaoKhoReponse.UpdateBaoKhoInfo.listBaoKhoNgoc.Count; m++)
							{
								BaoKhoInfoResponse.listBaoKhoNgoc.Add(BaoKhoInfoResponse.listBaoKhoNgoc[m]);
							}
						}
						if (cuopBaoKhoReponse.UpdateBaoKhoInfo.listBaoKhoKim != null && cuopBaoKhoReponse.UpdateBaoKhoInfo.listBaoKhoKim.Count > 0)
						{
							BaoKhoInfoResponse.listBaoKhoKim.Clear();
							for (int n = 0; n < cuopBaoKhoReponse.UpdateBaoKhoInfo.listBaoKhoKim.Count; n++)
							{
								BaoKhoInfoResponse.listBaoKhoKim.Add(cuopBaoKhoReponse.UpdateBaoKhoInfo.listBaoKhoKim[n]);
							}
						}
					}
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBaoKhoMain)
				{
					ScreenBaoKhoMain screenBaoKhoMain = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenBaoKhoMain) as ScreenBaoKhoMain;
					screenBaoKhoMain.displayInfo(BaoKhoInfoResponse);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenListBaoKho)
				{
					ScreenListBaoKho screenListBaoKho = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenListBaoKho) as ScreenListBaoKho;
					screenListBaoKho.updateView(cuopBaoKhoReponse.BaoKhoData);
				}
				if (cuopBaoKhoReponse.Replays != null && cuopBaoKhoReponse.Replays.Count > 0)
				{
					ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
					GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
					screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenLienMinhTrongCay;
					List<UserInfo.HeroData> listCloneHeroFromDoiHinh = UserInfo.GetListCloneHeroFromDoiHinh();
					PopupBattleResult popupBattleResult = PopupBattleResult.Create(cuopBaoKhoReponse.Replays[0], listCloneHeroFromDoiHinh, 0L, 0L, 0L, 0);
					popupBattleResult.gameObject.SetActive(false);
					screenBattle.Replay(cuopBaoKhoReponse.Replays[0]);
					screenBattle.OnFinishReplay += OnEndBattleCuopBaoKho;
				}
			}
			else if (cuopBaoKhoReponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(cuopBaoKhoReponse.ErrorMessage);
				EGDebug.LogWarning(cuopBaoKhoReponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", cuopBaoKhoReponse.ErrorCode, cuopBaoKhoReponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnCuopBaoKhoResponse : response null");
		}
		return true;
	}

	public static void OnEndBattleCuopBaoKho()
	{
		if (GUIManager.instance.LastScreen == GAME_SCREEN.ScreenBaoKhoMain)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBaoKhoMain);
		}
		if (GUIManager.instance.LastScreen == GAME_SCREEN.ScreenListBaoKho)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenListBaoKho);
		}
	}

	public void RequestThuHoachBaoKho(ThuHoachBaoKhoRequest request)
	{
		SendRequest(m_C2SProxy.RequestThuHoachBaoKho, JsonMapper.ToJson(request));
	}

	public bool OnThuHoachBaoKhoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		ThuHoachBaoKhoResponse thuHoachBaoKhoResponse = JsonMapper.ToObject<ThuHoachBaoKhoResponse>(data);
		if (thuHoachBaoKhoResponse != null)
		{
			if (thuHoachBaoKhoResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (thuHoachBaoKhoResponse.ptResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(thuHoachBaoKhoResponse.ptResponse.UpdateUserInfo);
				}
				if (PopupMyBaoKho.instance != null)
				{
					PopupMyBaoKho.DestroyPopup();
				}
				if (thuHoachBaoKhoResponse.ptResponse != null && thuHoachBaoKhoResponse.ptResponse.PhanThuongList.Count > 0)
				{
					PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongThuDuoc"), Localization.instance.Get("PhanThuongThuHoachDes"), thuHoachBaoKhoResponse.ptResponse);
				}
				if (thuHoachBaoKhoResponse.BaoKhoData != null && BaoKhoInfoResponse != null)
				{
					if (thuHoachBaoKhoResponse.BaoKhoData.BaoKhoType == UserInfo.BaoKhoInfo.LoaiBaoKho.NGOC)
					{
						for (int i = 0; i < BaoKhoInfoResponse.listBaoKhoNgoc.Count; i++)
						{
							if (BaoKhoInfoResponse.listBaoKhoNgoc[i].ID == thuHoachBaoKhoResponse.BaoKhoData.ID)
							{
								BaoKhoInfoResponse.listBaoKhoNgoc[i].GID = thuHoachBaoKhoResponse.BaoKhoData.GID;
								BaoKhoInfoResponse.listBaoKhoNgoc[i].SID = thuHoachBaoKhoResponse.BaoKhoData.SID;
								BaoKhoInfoResponse.listBaoKhoNgoc[i].Level = thuHoachBaoKhoResponse.BaoKhoData.Level;
								BaoKhoInfoResponse.listBaoKhoNgoc[i].Vip = thuHoachBaoKhoResponse.BaoKhoData.Vip;
								BaoKhoInfoResponse.listBaoKhoNgoc[i].CumServer = thuHoachBaoKhoResponse.BaoKhoData.CumServer;
								BaoKhoInfoResponse.listBaoKhoNgoc[i].DisplayName = thuHoachBaoKhoResponse.BaoKhoData.DisplayName;
								BaoKhoInfoResponse.listBaoKhoNgoc[i].TimeChiem = thuHoachBaoKhoResponse.BaoKhoData.TimeChiem;
								BaoKhoInfoResponse.listBaoKhoNgoc[i].LastTimeThuHoach = thuHoachBaoKhoResponse.BaoKhoData.LastTimeThuHoach;
							}
						}
					}
					if (thuHoachBaoKhoResponse.BaoKhoData.BaoKhoType == UserInfo.BaoKhoInfo.LoaiBaoKho.KIM)
					{
						for (int j = 0; j < BaoKhoInfoResponse.listBaoKhoKim.Count; j++)
						{
							if (BaoKhoInfoResponse.listBaoKhoKim[j].ID == thuHoachBaoKhoResponse.BaoKhoData.ID)
							{
								BaoKhoInfoResponse.listBaoKhoKim[j].GID = thuHoachBaoKhoResponse.BaoKhoData.GID;
								BaoKhoInfoResponse.listBaoKhoKim[j].SID = thuHoachBaoKhoResponse.BaoKhoData.SID;
								BaoKhoInfoResponse.listBaoKhoKim[j].Level = thuHoachBaoKhoResponse.BaoKhoData.Level;
								BaoKhoInfoResponse.listBaoKhoKim[j].Vip = thuHoachBaoKhoResponse.BaoKhoData.Vip;
								BaoKhoInfoResponse.listBaoKhoKim[j].CumServer = thuHoachBaoKhoResponse.BaoKhoData.CumServer;
								BaoKhoInfoResponse.listBaoKhoKim[j].DisplayName = thuHoachBaoKhoResponse.BaoKhoData.DisplayName;
								BaoKhoInfoResponse.listBaoKhoKim[j].TimeChiem = thuHoachBaoKhoResponse.BaoKhoData.TimeChiem;
								BaoKhoInfoResponse.listBaoKhoKim[j].LastTimeThuHoach = thuHoachBaoKhoResponse.BaoKhoData.LastTimeThuHoach;
							}
						}
					}
					for (int k = 0; k < BaoKhoInfoResponse.listMyBaoKho.Count; k++)
					{
						if (BaoKhoInfoResponse.listMyBaoKho[k].ID == thuHoachBaoKhoResponse.BaoKhoData.ID)
						{
							if (thuHoachBaoKhoResponse.BaoKhoData.GID == 0)
							{
								BaoKhoInfoResponse.listMyBaoKho.RemoveAt(k);
								break;
							}
							BaoKhoInfoResponse.listMyBaoKho[k].GID = thuHoachBaoKhoResponse.BaoKhoData.GID;
							BaoKhoInfoResponse.listMyBaoKho[k].SID = thuHoachBaoKhoResponse.BaoKhoData.SID;
							BaoKhoInfoResponse.listMyBaoKho[k].Level = thuHoachBaoKhoResponse.BaoKhoData.Level;
							BaoKhoInfoResponse.listMyBaoKho[k].Vip = thuHoachBaoKhoResponse.BaoKhoData.Vip;
							BaoKhoInfoResponse.listMyBaoKho[k].CumServer = thuHoachBaoKhoResponse.BaoKhoData.CumServer;
							BaoKhoInfoResponse.listMyBaoKho[k].DisplayName = thuHoachBaoKhoResponse.BaoKhoData.DisplayName;
							BaoKhoInfoResponse.listMyBaoKho[k].TimeChiem = thuHoachBaoKhoResponse.BaoKhoData.TimeChiem;
							BaoKhoInfoResponse.listMyBaoKho[k].LastTimeThuHoach = thuHoachBaoKhoResponse.BaoKhoData.LastTimeThuHoach;
						}
					}
				}
				if (!string.IsNullOrEmpty(thuHoachBaoKhoResponse.strThongBao))
				{
					MessagePopup.Create(thuHoachBaoKhoResponse.strThongBao);
				}
				if (BaoKhoInfoResponse != null)
				{
					if (thuHoachBaoKhoResponse.UpdateBaoKhoInfo.listMyBaoKho != null)
					{
						BaoKhoInfoResponse.listMyBaoKho.Clear();
						for (int l = 0; l < thuHoachBaoKhoResponse.UpdateBaoKhoInfo.listMyBaoKho.Count; l++)
						{
							UserInfo.BaoKhoInfo baoKhoInfo = thuHoachBaoKhoResponse.UpdateBaoKhoInfo.listMyBaoKho[l];
							BaoKhoInfoResponse.listMyBaoKho.Add(baoKhoInfo);
							if (baoKhoInfo.BaoKhoType == UserInfo.BaoKhoInfo.LoaiBaoKho.NGOC)
							{
								setBaoKho(BaoKhoInfoResponse.listBaoKhoNgoc, baoKhoInfo);
							}
							if (baoKhoInfo.BaoKhoType == UserInfo.BaoKhoInfo.LoaiBaoKho.KIM)
							{
								setBaoKho(BaoKhoInfoResponse.listBaoKhoKim, baoKhoInfo);
							}
						}
					}
					if (thuHoachBaoKhoResponse.UpdateBaoKhoInfo.listBaoKhoNgoc != null && thuHoachBaoKhoResponse.UpdateBaoKhoInfo.listBaoKhoNgoc.Count > 0)
					{
						BaoKhoInfoResponse.listBaoKhoNgoc.Clear();
						for (int m = 0; m < thuHoachBaoKhoResponse.UpdateBaoKhoInfo.listBaoKhoNgoc.Count; m++)
						{
							BaoKhoInfoResponse.listBaoKhoNgoc.Add(BaoKhoInfoResponse.listBaoKhoNgoc[m]);
						}
					}
					if (thuHoachBaoKhoResponse.UpdateBaoKhoInfo.listBaoKhoKim != null && thuHoachBaoKhoResponse.UpdateBaoKhoInfo.listBaoKhoKim.Count > 0)
					{
						BaoKhoInfoResponse.listBaoKhoKim.Clear();
						for (int n = 0; n < thuHoachBaoKhoResponse.UpdateBaoKhoInfo.listBaoKhoKim.Count; n++)
						{
							BaoKhoInfoResponse.listBaoKhoKim.Add(thuHoachBaoKhoResponse.UpdateBaoKhoInfo.listBaoKhoKim[n]);
						}
					}
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBaoKhoMain)
				{
					ScreenBaoKhoMain screenBaoKhoMain = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenBaoKhoMain) as ScreenBaoKhoMain;
					screenBaoKhoMain.displayInfo(BaoKhoInfoResponse);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenMyListBaoKho)
				{
					ScreenMyListBaoKho screenMyListBaoKho = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenMyListBaoKho) as ScreenMyListBaoKho;
					screenMyListBaoKho.displayMyBaoKhoList();
				}
			}
			else if (thuHoachBaoKhoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(thuHoachBaoKhoResponse.ErrorMessage);
				EGDebug.LogWarning(thuHoachBaoKhoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", thuHoachBaoKhoResponse.ErrorCode, thuHoachBaoKhoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnThuHoachBaoKhoResponse : response null");
		}
		return true;
	}

	public void RequestGetListBaoKho()
	{
		m_C2SProxy.RequestGetListBaoKho(HostID.Server, RmiContext.ReliableSend);
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public bool OnGetListBaoKhoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetListBaoKhoHang2Response getListBaoKhoHang2Response = JsonMapper.ToObject<GetListBaoKhoHang2Response>(data);
		if (getListBaoKhoHang2Response != null)
		{
			if (getListBaoKhoHang2Response.ErrorCode == ERROR_CODE.OK)
			{
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenListBaoKho)
				{
					ScreenListBaoKho screenListBaoKho = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenListBaoKho) as ScreenListBaoKho;
					screenListBaoKho.setData(getListBaoKhoHang2Response);
				}
			}
			else if (getListBaoKhoHang2Response.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getListBaoKhoHang2Response.ErrorMessage);
				EGDebug.LogWarning(getListBaoKhoHang2Response.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getListBaoKhoHang2Response.ErrorCode, getListBaoKhoHang2Response.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnGetListBaoKhoResponse : response null");
		}
		return true;
	}

	public void RequestQuayCamCungBiBao(QuayCamCungBiBaoResquest request)
	{
		SendRequest(m_C2SProxy.RequestQuayCamCungBiBao, JsonMapper.ToJson(request));
	}

	public bool OnQuayCamCungResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		QuayCamCungBiBaoResponse quayCamCungBiBaoResponse = JsonMapper.ToObject<QuayCamCungBiBaoResponse>(data);
		if (quayCamCungBiBaoResponse != null)
		{
			if (quayCamCungBiBaoResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (quayCamCungBiBaoResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(quayCamCungBiBaoResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenCamCungBiBao)
				{
					ScreenCamCungBiBao screenCamCungBiBao = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenCamCungBiBao) as ScreenCamCungBiBao;
					screenCamCungBiBao.displayResponse(quayCamCungBiBaoResponse);
				}
				if (PopupCamCungBiBaoInfo.instance != null)
				{
					PopupCamCungBiBaoInfo.DestroyPopup();
				}
			}
			else if (quayCamCungBiBaoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(quayCamCungBiBaoResponse.ErrorMessage);
				EGDebug.LogWarning(quayCamCungBiBaoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", quayCamCungBiBaoResponse.ErrorCode, quayCamCungBiBaoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnQuayCamCungResponse : response null");
		}
		return true;
	}

	public void RequestNhanThuongCamCung()
	{
		SendRequest(m_C2SProxy.RequestNhanThuongCamCung, string.Empty);
	}

	public bool OnNhanThuongCamCungResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		NhanThuongCamCungResponse nhanThuongCamCungResponse = JsonMapper.ToObject<NhanThuongCamCungResponse>(data);
		if (nhanThuongCamCungResponse != null)
		{
			if (nhanThuongCamCungResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (nhanThuongCamCungResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(nhanThuongCamCungResponse.UpdateInfo);
				}
				if (PopupCamCungBiBaoInfo.instance != null)
				{
					PopupCamCungBiBaoInfo.DestroyPopup();
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenCamCungBiBao)
				{
					ScreenCamCungBiBao screenCamCungBiBao = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenCamCungBiBao) as ScreenCamCungBiBao;
					screenCamCungBiBao.displayPhanthuong(nhanThuongCamCungResponse);
				}
			}
			else if (nhanThuongCamCungResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(nhanThuongCamCungResponse.ErrorMessage);
				EGDebug.LogWarning(nhanThuongCamCungResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", nhanThuongCamCungResponse.ErrorCode, nhanThuongCamCungResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnNhanThuongCamCungResponse : response null");
		}
		return true;
	}

	public void RequestGetHoaVangInfo()
	{
		m_C2SProxy.RequestGetHoaVangInfo(HostID.Server, RmiContext.ReliableSend);
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public bool OnGetHoaVangInfoResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		GetHoaVangInfoResponse getHoaVangInfoResponse = JsonMapper.ToObject<GetHoaVangInfoResponse>(data);
		if (getHoaVangInfoResponse != null)
		{
			if (getHoaVangInfoResponse.ErrorCode == ERROR_CODE.OK)
			{
				HoaVangInfoResponse = getHoaVangInfoResponse;
				LastTimeGetHoaVangInfo = getHoaVangInfoResponse.LastUpdateTime;
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenListHoaVang)
				{
					ScreenListHoaVang screenListHoaVang = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenListHoaVang) as ScreenListHoaVang;
					screenListHoaVang.displayInfo(getHoaVangInfoResponse);
				}
			}
			else if (getHoaVangInfoResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(getHoaVangInfoResponse.ErrorMessage);
				EGDebug.LogWarning(getHoaVangInfoResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", getHoaVangInfoResponse.ErrorCode, getHoaVangInfoResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("GetHoaVangInfoResponse : response null");
		}
		return true;
	}

	public void RequestHoaVang(HoaVangRequest request)
	{
		SendRequest(m_C2SProxy.RequestHoaVang, JsonMapper.ToJson(request));
	}

	public bool OnHoaVangResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		HoaVangResponse hoaVangResponse = JsonMapper.ToObject<HoaVangResponse>(data);
		if (hoaVangResponse != null)
		{
			if (hoaVangResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (hoaVangResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(hoaVangResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenListHoaVang)
				{
					ScreenListHoaVang screenListHoaVang = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenListHoaVang) as ScreenListHoaVang;
					screenListHoaVang.updateView(hoaVangResponse);
				}
			}
			else if (hoaVangResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(hoaVangResponse.ErrorMessage);
				EGDebug.LogWarning(hoaVangResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", hoaVangResponse.ErrorCode, hoaVangResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnHoaVangResponse : response null");
		}
		return true;
	}

	public void RequestQuayDiemHoaVang()
	{
		m_C2SProxy.RequestQuayDiemHoaVang(HostID.Server, RmiContext.ReliableSend);
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public bool OnQuayDiemHoaVangResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		QuayDiemHoaVangResponse quayDiemHoaVangResponse = JsonMapper.ToObject<QuayDiemHoaVangResponse>(data);
		if (quayDiemHoaVangResponse != null)
		{
			if (quayDiemHoaVangResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (quayDiemHoaVangResponse.UpdateInfo != null)
				{
					UserInfo.UpdateInfo(quayDiemHoaVangResponse.UpdateInfo);
				}
				if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenQuayDiemHoaVang)
				{
					ScreenQuayDiemHoaVang screenQuayDiemHoaVang = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenQuayDiemHoaVang) as ScreenQuayDiemHoaVang;
					screenQuayDiemHoaVang.updateView(quayDiemHoaVangResponse);
				}
			}
			else if (quayDiemHoaVangResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(quayDiemHoaVangResponse.ErrorMessage);
				EGDebug.LogWarning(quayDiemHoaVangResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", quayDiemHoaVangResponse.ErrorCode, quayDiemHoaVangResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnHoaVangResponse : response null");
		}
		return true;
	}

	public void RequestNhanThuongDailyKNB()
	{
		m_C2SProxy.RequestNhanThuongDailyKNB(HostID.Server, RmiContext.ReliableSend);
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public bool OnNhanThuongDailyKNBResponse(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
		if (phanThuongResponse != null)
		{
			if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
			{
				if (phanThuongResponse.UpdateUserInfo != null)
				{
					UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
				}
				PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), phanThuongResponse);
			}
			else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
			{
				MessagePopup.Create(phanThuongResponse.ErrorMessage);
				EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
			}
			else
			{
				string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
				MessagePopup.Create(text);
				EGDebug.LogWarning(text);
			}
		}
		else
		{
			EGDebug.LogError("OnKichHoatGiftCodeResponse : response null");
		}
		return true;
	}

	public void RequestTestProudNet()
	{
		m_C2SProxy.RequestTestProudNet(HostID.Server, RmiContext.ReliableSend, string.Empty);
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public bool OnNotifyTestProudNet(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		MessagePopup.Create("Hello test proudNet");
		return true;
	}

	public void RequestTrieuHoiChienHon(int hid)
	{
		TrieuHoiChienHonRequest trieuHoiChienHonRequest = new TrieuHoiChienHonRequest();
		trieuHoiChienHonRequest.HID = hid;
		m_C2SProxy.RequestTrieuHoiChienHon(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(trieuHoiChienHonRequest, false));
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public bool OnNotifyTrieuHoiChienHon(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		try
		{
			TrieuHoiChienHonResponse response = JsonMapper.ToObject<TrieuHoiChienHonResponse>(data);
			if (response != null)
			{
				if (response.ErrorCode == ERROR_CODE.OK)
				{
					if (response.updateInfo != null)
					{
						UserInfo.UpdateInfo(response.updateInfo);
					}
					GUIManager.instance.SetScreen(GAME_SCREEN.ScreenChienHon);
					ScreenChienHon screenChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenChienHon) as ScreenChienHon;
					UserInfo.HeroData data2 = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == response.HID);
					UserInfo.ChienHon chienhon = GameManager.instance.m_GameClient.UserInfo.ChienHonList.Find((UserInfo.ChienHon e) => e.HID == response.HID);
					screenChienHon.displayNhanVat3D(chienhon, data2);
				}
				else if (response.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
				{
					MessagePopup.Create(response.ErrorMessage);
					EGDebug.LogWarning(response.ErrorMessage);
				}
				else
				{
					string text = string.Format("ErrorCode: {0}, Message: {1}", response.ErrorCode, response.ErrorMessage);
					MessagePopup.Create(text);
					EGDebug.LogWarning(text);
				}
			}
			else
			{
				EGDebug.LogError("OnKichHoatGiftCodeResponse : response null");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
		}
		return true;
	}

	public void RequestChienHonChangeBuff(int chienHonId)
	{
		ChangeChienHonBuffRequest changeChienHonBuffRequest = new ChangeChienHonBuffRequest();
		changeChienHonBuffRequest.ID = chienHonId;
		m_C2SProxy.RequestChienHonChangeBuff(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(changeChienHonBuffRequest));
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public bool OnNotifyChienHonChangeBuff(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		try
		{
			UseTanHonTangLevelChienHonResponse response = JsonMapper.ToObject<UseTanHonTangLevelChienHonResponse>(data);
			if (response != null)
			{
				if (response.ErrorCode == ERROR_CODE.OK)
				{
					if (response.updateInfo != null)
					{
						UserInfo.UpdateInfo(response.updateInfo);
					}
					ScreenDoiThuocTinhChienHon screenDoiThuocTinhChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenDoiThuocTinhChienHon) as ScreenDoiThuocTinhChienHon;
					UserInfo.ChienHon chienhon = response.updateInfo.ChienHonList.Find((UserInfo.ChienHon e) => e.ID == response.ChienHonID);
					screenDoiThuocTinhChienHon.Sync(chienhon);
					ScreenChienHon screenChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenChienHon) as ScreenChienHon;
					screenChienHon.ReActive(true);
				}
				else if (response.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
				{
					MessagePopup.Create(response.ErrorMessage);
					EGDebug.LogWarning(response.ErrorMessage);
				}
				else
				{
					string text = string.Format("ErrorCode: {0}, Message: {1}", response.ErrorCode, response.ErrorMessage);
					MessagePopup.Create(text);
					EGDebug.LogWarning(text);
				}
			}
			else
			{
				EGDebug.LogError("OnKichHoatGiftCodeResponse : response null");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
		}
		return true;
	}

	public void RequestStartBoiDuongChienHon(int chienHonId, StartBoiDuongChienHonRequest.LoaiBoiDuong loai)
	{
		StartBoiDuongChienHonRequest startBoiDuongChienHonRequest = new StartBoiDuongChienHonRequest();
		startBoiDuongChienHonRequest.ID = chienHonId;
		startBoiDuongChienHonRequest.loai = loai;
		m_C2SProxy.RequestStartBoiDuongChienHon(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(startBoiDuongChienHonRequest));
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public bool OnNotifyStartBoiDuongChienHon(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		try
		{
			StartBoiDuongChienHonResponse startBoiDuongChienHonResponse = JsonMapper.ToObject<StartBoiDuongChienHonResponse>(data);
			if (startBoiDuongChienHonResponse != null)
			{
				if (startBoiDuongChienHonResponse.ErrorCode == ERROR_CODE.OK)
				{
					if (startBoiDuongChienHonResponse.UpdateInfo != null)
					{
						UserInfo.UpdateInfo(startBoiDuongChienHonResponse.UpdateInfo);
					}
					if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBoiDuongChienHon)
					{
						ScreenBoiDuongChienHon screenBoiDuongChienHon = GUIManager.getScreen(GAME_SCREEN.ScreenBoiDuongChienHon) as ScreenBoiDuongChienHon;
						screenBoiDuongChienHon.openScreenBoiDuongConfirm(startBoiDuongChienHonResponse);
					}
				}
				else if (startBoiDuongChienHonResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
				{
					MessagePopup.Create(startBoiDuongChienHonResponse.ErrorMessage);
					EGDebug.LogWarning(startBoiDuongChienHonResponse.ErrorMessage);
				}
				else
				{
					string text = string.Format("ErrorCode: {0}, Message: {1}", startBoiDuongChienHonResponse.ErrorCode, startBoiDuongChienHonResponse.ErrorMessage);
					MessagePopup.Create(text);
					EGDebug.LogWarning(text);
				}
			}
			else
			{
				EGDebug.LogError("OnKichHoatGiftCodeResponse : response null");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
		}
		return true;
	}

	public void RequestEndBoiDuongChienHon(int chienHonId)
	{
		EndBoiDuongChienHonRequest endBoiDuongChienHonRequest = new EndBoiDuongChienHonRequest();
		endBoiDuongChienHonRequest.ID = chienHonId;
		m_C2SProxy.RequestEndBoiDuongChienHon(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(endBoiDuongChienHonRequest));
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public bool OnNotifyEndBoiDuongChienHon(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		try
		{
			EndBoiDuongChienHonResponse endBoiDuongChienHonResponse = JsonMapper.ToObject<EndBoiDuongChienHonResponse>(data);
			if (endBoiDuongChienHonResponse != null)
			{
				if (endBoiDuongChienHonResponse.ErrorCode == ERROR_CODE.OK)
				{
					if (endBoiDuongChienHonResponse.UpdateInfo != null)
					{
						UserInfo.UpdateInfo(endBoiDuongChienHonResponse.UpdateInfo);
					}
					if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBoiDuongChienHonConfirm)
					{
						ScreenBoiDuongChienHonConfirm screenBoiDuongChienHonConfirm = GUIManager.getScreen(GAME_SCREEN.ScreenBoiDuongChienHonConfirm) as ScreenBoiDuongChienHonConfirm;
						screenBoiDuongChienHonConfirm.openScreenBoiDuong();
					}
				}
				else if (endBoiDuongChienHonResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
				{
					MessagePopup.Create(endBoiDuongChienHonResponse.ErrorMessage);
					EGDebug.LogWarning(endBoiDuongChienHonResponse.ErrorMessage);
				}
				else
				{
					string text = string.Format("ErrorCode: {0}, Message: {1}", endBoiDuongChienHonResponse.ErrorCode, endBoiDuongChienHonResponse.ErrorMessage);
					MessagePopup.Create(text);
					EGDebug.LogWarning(text);
				}
			}
			else
			{
				EGDebug.LogError("OnKichHoatGiftCodeResponse : response null");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
		}
		return true;
	}

	public void RequestUseTanHonTangLevelChienHon(UseTanHonTangLevelChienHonRequest req)
	{
		m_C2SProxy.RequestUseTanHonTangLevelChienHon(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(req));
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public bool OnNotifyUseTanHonTangLevelChienHon(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		try
		{
			UseTanHonTangLevelChienHonResponse response = JsonMapper.ToObject<UseTanHonTangLevelChienHonResponse>(data);
			if (response != null)
			{
				if (response.ErrorCode == ERROR_CODE.OK)
				{
					if (response.updateInfo != null)
					{
						UserInfo.UpdateInfo(response.updateInfo);
					}
					ScreenThangCapChienHon screenThangCapChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenThangCapChienHon) as ScreenThangCapChienHon;
					UserInfo.ChienHon chienhon = response.updateInfo.ChienHonList.Find((UserInfo.ChienHon e) => e.ID == response.ChienHonID);
					screenThangCapChienHon.Sync(chienhon);
					ScreenChienHon screenChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenChienHon) as ScreenChienHon;
					screenChienHon.ReActive(true);
				}
				else if (response.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
				{
					MessagePopup.Create(response.ErrorMessage);
					EGDebug.LogWarning(response.ErrorMessage);
				}
				else
				{
					string text = string.Format("ErrorCode: {0}, Message: {1}", response.ErrorCode, response.ErrorMessage);
					MessagePopup.Create(text);
					EGDebug.LogWarning(text);
				}
			}
			else
			{
				EGDebug.LogError("OnKichHoatGiftCodeResponse : response null");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
		}
		return true;
	}

	public void RequestDotPhaChienHon(int chienHonId)
	{
		DotPhaChienHonRequest dotPhaChienHonRequest = new DotPhaChienHonRequest();
		dotPhaChienHonRequest.ChienHonID = chienHonId;
		m_C2SProxy.RequestDotPhaChienHon(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(dotPhaChienHonRequest));
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public bool OnNotifyDotPhaChienHon(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		try
		{
			DotPhaChienHonResponse response = JsonMapper.ToObject<DotPhaChienHonResponse>(data);
			if (response != null)
			{
				if (response.ErrorCode == ERROR_CODE.OK)
				{
					if (response.updateInfo != null)
					{
						UserInfo.UpdateInfo(response.updateInfo);
					}
					ScreenDotPhaChienHon screenDotPhaChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenDotPhaChienHon) as ScreenDotPhaChienHon;
					UserInfo.ChienHon chienhon = response.updateInfo.ChienHonList.Find((UserInfo.ChienHon e) => e.ID == response.ChienHonID);
					screenDotPhaChienHon.Sync(chienhon);
					ScreenChienHon screenChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenChienHon) as ScreenChienHon;
					screenChienHon.ReActive(true);
				}
				else if (response.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
				{
					MessagePopup.Create(response.ErrorMessage);
					EGDebug.LogWarning(response.ErrorMessage);
				}
				else
				{
					string text = string.Format("ErrorCode: {0}, Message: {1}", response.ErrorCode, response.ErrorMessage);
					MessagePopup.Create(text);
					EGDebug.LogWarning(text);
				}
			}
			else
			{
				EGDebug.LogError("OnKichHoatGiftCodeResponse : response null");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
		}
		return true;
	}

	public void RequestCheTaoHuyenKhi(int voCongNeed)
	{
		CheTaoHuyenKhiRequest cheTaoHuyenKhiRequest = new CheTaoHuyenKhiRequest();
		cheTaoHuyenKhiRequest.VoCongID = voCongNeed;
		m_C2SProxy.RequestCheTaoHuyenKhi(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(cheTaoHuyenKhiRequest));
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public bool OnNotifyCheTaoHuyenKhi(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		try
		{
			PhanThuongResponse phanThuongResponse = JsonMapper.ToObject<PhanThuongResponse>(data);
			if (phanThuongResponse != null)
			{
				if (phanThuongResponse.ErrorCode == ERROR_CODE.OK)
				{
					if (phanThuongResponse.UpdateUserInfo != null)
					{
						UserInfo.UpdateInfo(phanThuongResponse.UpdateUserInfo);
					}
					ScreenBase screen = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenCheTacHuyenKhi);
					screen.OnActive();
					ScreenChienHon screenChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenChienHon) as ScreenChienHon;
					screenChienHon.ReActive();
					GUIManager.instance.SetScreen(GAME_SCREEN.ScreenCheTacHuyenKhi);
					PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongDesc"), phanThuongResponse);
				}
				else if (phanThuongResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
				{
					MessagePopup.Create(phanThuongResponse.ErrorMessage);
					EGDebug.LogWarning(phanThuongResponse.ErrorMessage);
				}
				else
				{
					string text = string.Format("ErrorCode: {0}, Message: {1}", phanThuongResponse.ErrorCode, phanThuongResponse.ErrorMessage);
					MessagePopup.Create(text);
					EGDebug.LogWarning(text);
				}
			}
			else
			{
				EGDebug.LogError("OnKichHoatGiftCodeResponse : response null");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
		}
		return true;
	}

	public void RequestThangCapHuyenKhi(int hkThang, int hkDot)
	{
		ThangCapHuyenKhiRequest thangCapHuyenKhiRequest = new ThangCapHuyenKhiRequest();
		thangCapHuyenKhiRequest.huyenKhiThangCap = hkThang;
		thangCapHuyenKhiRequest.huyenKhiDot = hkDot;
		m_C2SProxy.RequestThangCapHuyenKhi(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(thangCapHuyenKhiRequest));
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public bool OnNotifyThangCapHuyenKhi(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		try
		{
			ThangCapHuyenKhiResponse response = JsonMapper.ToObject<ThangCapHuyenKhiResponse>(data);
			if (response != null)
			{
				if (response.ErrorCode == ERROR_CODE.OK)
				{
					if (response.UpdateInfo != null)
					{
						UserInfo.UpdateInfo(response.UpdateInfo);
					}
					UserInfo.HuyenKhi huyenKhi = GameManager.instance.m_GameClient.UserInfo.HuyenKhiList.Find((UserInfo.HuyenKhi e) => e.ID == response.huyenKhiThangCap);
					if (huyenKhi != null)
					{
						PopupCuongHoaHuyenKhi.Create(huyenKhi);
					}
					ScreenChienHon screenChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenChienHon) as ScreenChienHon;
					screenChienHon.ReActive();
					if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenDanhSachHuyenKhi && huyenKhi != null)
					{
						ScreenDanhSachHuyenKhi screenDanhSachHuyenKhi = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenDanhSachHuyenKhi) as ScreenDanhSachHuyenKhi;
						screenDanhSachHuyenKhi.UpdateHuyenKhi(huyenKhi);
						screenDanhSachHuyenKhi.OnActive();
					}
				}
				else if (response.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
				{
					MessagePopup.Create(response.ErrorMessage);
					EGDebug.LogWarning(response.ErrorMessage);
				}
				else
				{
					string text = string.Format("ErrorCode: {0}, Message: {1}", response.ErrorCode, response.ErrorMessage);
					MessagePopup.Create(text);
					EGDebug.LogWarning(text);
				}
			}
			else
			{
				EGDebug.LogError("OnKichHoatGiftCodeResponse : response null");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
		}
		return true;
	}

	public void RequestTrangBiHuyenKhi(int chienhon, int huyenkhi, int hk)
	{
		TrangBiHuyenKhiRequest trangBiHuyenKhiRequest = new TrangBiHuyenKhiRequest();
		trangBiHuyenKhiRequest.hk = hk;
		trangBiHuyenKhiRequest.chienHonId = chienhon;
		trangBiHuyenKhiRequest.huyenKhiId = huyenkhi;
		m_C2SProxy.RequestTrangBiHuyenKhi(HostID.Server, RmiContext.ReliableSend, JsonMapper.ToJson(trangBiHuyenKhiRequest));
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
	}

	public bool OnNotifyTrangBiHuyenKhi(HostID remote, RmiContext rmiContext, string data)
	{
		OnGetResponse();
		try
		{
			TrangBiHuyenKhiResponse trangBiHuyenKhiResponse = JsonMapper.ToObject<TrangBiHuyenKhiResponse>(data);
			if (trangBiHuyenKhiResponse != null)
			{
				if (trangBiHuyenKhiResponse.ErrorCode == ERROR_CODE.OK)
				{
					if (trangBiHuyenKhiResponse.UpdateInfo != null)
					{
						UserInfo.UpdateInfo(trangBiHuyenKhiResponse.UpdateInfo);
						ScreenChienHon screenChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenChienHon) as ScreenChienHon;
						screenChienHon.ReActive();
					}
				}
				else if (trangBiHuyenKhiResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
				{
					MessagePopup.Create(trangBiHuyenKhiResponse.ErrorMessage);
					EGDebug.LogWarning(trangBiHuyenKhiResponse.ErrorMessage);
				}
				else
				{
					string text = string.Format("ErrorCode: {0}, Message: {1}", trangBiHuyenKhiResponse.ErrorCode, trangBiHuyenKhiResponse.ErrorMessage);
					MessagePopup.Create(text);
					EGDebug.LogWarning(text);
				}
			}
			else
			{
				EGDebug.LogError("OnKichHoatGiftCodeResponse : response null");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.ToString());
		}
		return true;
	}

	private void S2CStubMessage_Start()
	{
		m_S2CStub.NotifySetGioLuaTraiLienMinh = OnBatDauLuaTraiLienMinh;
		m_S2CStub.NotifyThamGiaLuaTraiLienMinh = OnThamGiaLuaTraiLienMinh;
		m_S2CStub.NotifyLienMinhThoiLua = OnThoiLuaLienMinh;
		m_S2CStub.NotifyGetExpLuaTrai = OnGetExpLuaTrai;
		m_S2CStub.NotifyRoiKhoiLuaTraiLienMinh = OnRoiKhoiLuaTraiLienMinh;
		m_S2CStub.NotifyCT2BXH = OnCT2BXH;
		m_S2CStub.NotifyPhanRaTrangBi = OnPhanRaTrangBiResponse;
		m_S2CStub.NotifyCT2BangXepHangTuanNay = OnCT2BXHTuanNay;
		m_S2CStub.NotifyCT2BangXepHangTuanTruoc = OnCT2BXHTuanTruoc;
		m_S2CStub.NotifyCT2RuneXuatHien = OnCT2RuneXuatHien;
		m_S2CStub.NotifyCT2GotRune = OnCT2GotRune;
		m_S2CStub.NotifyListCT2 = OnGetListCT2;
		m_S2CStub.NotifyThamGiaCT2 = OnThamGiaCT2;
		m_S2CStub.NotifyDoiDo = OnDoiDoResponse;
		m_S2CStub.NotifyKhamNgoc = OnKhamNgocResponse;
		m_S2CStub.NotifyGoNgoc = OnGoNgocResponse;
		m_S2CStub.NotifyGetDiHoaCungInfo = OnGetDiHoaCungInfoResponse;
		m_S2CStub.NotifyGetTopDiHoaCung = OnGetTopDiHoaCungResponse;
		m_S2CStub.NotifyNhanThuongDiHoaCungAll = OnNhanThuongDiHoaCungAllResponse;
		m_S2CStub.NotifyGetDuaTopLuanKiemInfo = OnGetDuaTopLuanKiemInfoResponse;
		m_S2CStub.NotifyGetDuaTopLevelInfo = OnGetDuaTopLevelInfoResponse;
		m_S2CStub.NotifyDangNhapQuayXoSo = OnDangNhapQuayXoSoResponse;
		m_S2CStub.NotifySendMail = OnSendMailResponse;
		m_S2CStub.NotifyXemThongTinMonPhai = OnXemThongTinMonPhaiResponse;
		m_S2CStub.NotifyUseRuongThan = OnUseRuongThanResponse;
		m_S2CStub.NotifyUseMailPhanThuong = OnUseMailPhanThuongResponse;
		m_S2CStub.NotifyRefreshMail = OnRefreshMailResponse;
		m_S2CStub.NotifyGetFriendsDoiHinh = OnGetFriendsDanhSonResponse;
		m_S2CStub.NotifyHighlight = OnHighlightResponse;
		m_S2CStub.NotifyUseCustomItem = OnUseCustomItemResponse;
		m_S2CStub.NotifyBuyVatPhamAndUse = OnBuyVatPhamAndUseResponse;
		m_S2CStub.NotifyBaoTriServer = OnBaoTriServer;
		m_S2CStub.NotifyMatDongBoDuLieu = OnMatDongBoDuLieu;
		m_S2CStub.NotifyDatTenMonPhai = OnDatTenMonPhaiResponse;
		m_S2CStub.NotifyKichHoatGiftCode = OnKichHoatGiftCodeResponse;
		m_S2CStub.NotifyTangTheLuc = OnTangTheLuc;
		m_S2CStub.NotifyDuocTangTheLuc = OnDuocTangTheLuc;
		m_S2CStub.NotifyDoiThuongULinh = OnDoiThuongULinhResponse;
		m_S2CStub.NotifyGetTopULinh = OnGetTopULinhResponse;
		m_S2CStub.NotifyULinhInfo = OnULinhInfoResponse;
		m_S2CStub.NotifyDoiItemULinh = OnDoiItemULinhResponse;
		m_S2CStub.NotifyKnbRefreshULinh = OnKnbRefreshULinhResponse;
		m_S2CStub.NotifyChatInfo = OnChatInfoResponse;
		m_S2CStub.NotifyGetChatAll = OnGetChatAllResponse;
		m_S2CStub.NotifyBatTho = OnBatThoSuccess;
		m_S2CStub.NotifyBatCoc = OnBatCocSuccess;
		m_S2CStub.NotifyChuocThan = OnChuocThanSuccess;
		m_S2CStub.NotifyNhanDuocThachDau = OnNhanDuocThachDau;
		m_S2CStub.NotifyKetQuaThachDau = OnKetQuaThachDau;
		m_S2CStub.NotifySearchBanBe = OnSearchBanBeResponse;
		m_S2CStub.NotifyAddBanBe = OnAddBanBeResponse;
		m_S2CStub.NotifyAcceptBanBe = OnAcceptBanBeResponse;
		m_S2CStub.NotifyDeleteBanBe = OnDeleteBanBeResponse;
		m_S2CStub.NotifyBanBeCuuThuInfo = OnBanBeCuuThuInfoResponse;
		m_S2CStub.NotifyUongRuouTieuPhong = OnUongRuouTieuPhongResponse;
		m_S2CStub.NotifyDoiRuou = OnDoiRuouResponse;
		m_S2CStub.NotifyGetDoiRuouInfo = OnGetDoiRuouInfoResponse;
		m_S2CStub.NotifyDangNhapNhanThuong = OnDangNhapNhanThuongResponse;
		m_S2CStub.NotifyCuuVienTieuPhong = OnCuuVienTieuPhongResponse;
		m_S2CStub.NotifyPaymentConfirm = OnPaymentConfirmResponse;
		m_S2CStub.NotifyNhanRuongThachSanh = OnNhanRuongThachSanhResponse;
		m_S2CStub.NotifyChoiXocDia = OnChoiXocDiaResponse;
		m_S2CStub.NotifyXocDiaInfo = OnXocDiaInfoResponse;
		m_S2CStub.NotifyThanTai = OnThanTaiResponse;
		m_S2CStub.NotifyLenCapNhanThuong = OnLenCapNhanThuongResponse;
		m_S2CStub.NotifyHuaNguyen = OnHuaNguyenResponse;
		m_S2CStub.NotifyOpenHop = OnOpenHopResponse;
		m_S2CStub.NotifyRequestSetDoiHinhAndTranHinh = OnSetDoiHinhAndTranHinhResponse;
		m_S2CStub.NotifyHuyetChienInfo = OnReceiveHuyetChienInfo;
		m_S2CStub.NotifyStartHuyetChien = OnStartHuyetChien;
		m_S2CStub.NotifyDanhHuyetChien = OnDanhHuyetChien;
		m_S2CStub.NotifyHoiSinhHuyetChien = OnHoiSinhHuyetChien;
		m_S2CStub.NotifyNhanThuongHuyetChien = OnNhanThuongHuyetChien;
		m_S2CStub.NotifyHuyetChienTangThuocTinh = OnTangChiSoHuyetChien;
		m_S2CStub.NotifyGetTopHuyetChien = OnGetTopHuyetChien;
		m_S2CStub.NotifyKyNgoThamBai = OnKyNgoThamBaiResponse;
		m_S2CStub.NotifySelectStartDeTu = OnSelectStartDeTuResponse;
		m_S2CStub.NotifyHoiTheLuc = OnHoiTheLucResponse;
		m_S2CStub.NotifyCaoNhan = OnReceiveCaoNhan;
		m_S2CStub.NotifyBanDo = OnReceiveBanDo;
		m_S2CStub.NotifyBangHuu = OnReceiveBangHuu;
		m_S2CStub.NotifyThuongNhan = OnReceiveThuongNhan;
		m_S2CStub.NotifyTyThi = OnReceiveTyThi;
		m_S2CStub.NotifyBuyLeBao = OnBuyLeBaoResponse;
		m_S2CStub.NotifyBuyVatPham = OnBuyVatPhamResponse;
		m_S2CStub.NotifyLayDeTu = OnLayDeTuResponse;
		m_S2CStub.NotifyThamNgoVoCong = OnThamNgoVoCongResponse;
		m_S2CStub.NotifyTinhLuyenVoCong = OnTinhLuyenVoCongResponse;
		m_S2CStub.NotifySetDoiHinhHoTro = OnSetDoiHinhHoTroResponse;
		m_S2CStub.NotifyOpenDoiHinhHoTro = OnOpenDoiHinhHoTroResponse;
		m_S2CStub.NotifyTinhLuyenTrangBi = OnTinhLuyenTrangBiResponse;
		m_S2CStub.NotifyGhepManhTrangBi = OnGhepManhTrangBiResponse;
		m_S2CStub.NotifyGhepManhVoCong = OnGhepManhVoCongResponse;
		m_S2CStub.NotifyBanTrangBi = OnBanTrangBiResponse;
		m_S2CStub.NotifyCuongHoaTrangBi = OnCuongHoaTrangBiResponse;
		m_S2CStub.NotifyTruyenCong = OnTruyenCongResponse;
		m_S2CStub.NotifyTrieuHoiDeTuBangHon = OnTrieuHonDeTuBangHonResponse;
		m_S2CStub.NotifyTuLuyenDeTu = OnTuLuyenDeTuResponse;
		m_S2CStub.NotifyStartBoiDuong = OnStartBoiDuongResponse;
		m_S2CStub.NotifyEndBoiDuong = OnEndBoiDuongResponse;
		m_S2CStub.NotifySetTranHinh = OnSetTranHinhResponse;
		m_S2CStub.NotifySetTrangBi = OnSetTrangBiResponse;
		m_S2CStub.NotifySetVoCong = OnSetVoCongResponse;
		m_S2CStub.NotifySetDoiHinh = OnSetDoiHinhResponse;
		m_S2CStub.NotifySetHeroData = OnSetHeroDataResponse;
		m_S2CStub.NotifyChatMsg = OnReceiveChatMsg;
		m_S2CStub.NotifyDoiThuongLuanKiem = OnDoiThuongLuanKiemResponse;
		m_S2CStub.NotifyNhanThuongLuanKiem = OnNhanThuongLuanKiemResponse;
		m_S2CStub.NotifyGetLuanKiemInfo = OnReceiveLuanKiemInfo;
		m_S2CStub.NotifyListPositionInMain = OnReceiveListOnMain;
		m_S2CStub.NotifyListOnlineInMain = OnReceiveListOnlineInMain;
		m_S2CStub.NotifyCapNhatDiemLuanKiem = OnCapNhatDiemThuongLK;
		m_S2CStub.NotifyDauLuanKiem = OnDauLuanKiemResponse;
		m_S2CStub.NotifyDanhGiangHo = OnDanhGiangHoResponse;
		m_S2CStub.NotifyDanhNhanhGiangHo = OnDanhNhanhGiangHoResponse;
		m_S2CStub.NotifyResetLuotGiangHo = OnResetLuotNVGH;
		m_S2CStub.NotifyAnGaGiangHo = OnAnGaGH;
		m_S2CStub.NotifyNhanThuongGiangHo = OnNhanThuongGH;
		m_S2CStub.NotifyInTimeDanhDongNhan = OnStartDongNhan;
		m_S2CStub.NotifyOutTimeDanhDongNhan = OnEndDongNhan;
		m_S2CStub.NotifyDanhDongNhan = OnDanhDongNhanResponse;
		m_S2CStub.NotifyGetDongNhanInfo = OnGetDongNhanInfoResponse;
		m_S2CStub.NotifyDanhDanhSon = OnDanhDanhSonResponse;
		m_S2CStub.NotifyMoThuongDanhSon = OnMoThuongDanhSon;
		m_S2CStub.NotifyVuotAiDanhSon = OnVuotAiDanhSon;
		m_S2CStub.NotifyMoHetDanhSon = OnMoHetDanhSon;
		m_S2CStub.NotifyDangNhapTrungTK = OnDangNhapTrungTK;
		m_S2CStub.NotifyCT2BattleResult = OnCT2BattleResult;
		m_S2CStub.NotifyCT2PlayerPos = OnCT2PlayerPos;
		m_S2CStub.NotifyCT2Teleport = OnCT2Teleport;
		m_S2CStub.NotifyCT2PlayerAppear = OnCT2PlayerAppear;
		m_S2CStub.NotifyCT2NPCMove = OnCT2NPCMove;
		m_S2CStub.NotifyCT2NPCIdle = OnCT2NPCIdle;
		m_S2CStub.NotifyCT2NPCAttack = OnCT2NPCAttack;
		m_S2CStub.NotifyCT2HPPercent = OnCT2HPPercent;
		m_S2CStub.NotifyCT2HPTeam = OnCT2HPTeam;
		m_S2CStub.NotifyCT2PlayerAppear = OnCT2PlayerAppear;
		m_S2CStub.NotifyGetTopLienMinh = OnGetTopLienMinhResponse;
		m_S2CStub.NotifyGetThongTinLienMinh = OnGetThongTinLienMinhResponse;
		m_S2CStub.NotifyGiaNhapLienMinh = OnGiaNhapLienMinhResponse;
		m_S2CStub.NotifyRutGiaNhapLienMinh = OnRutGiaNhapLienMinhResponse;
		m_S2CStub.NotifyLapLienMinh = OnLapLienMinhResponse;
		m_S2CStub.NotifyChapNhanGiaNhapLienMinh = OnChapNhanGiaNhapLienMinhResponse;
		m_S2CStub.NotifyThoatLienMinh = OnThoatLienMinhResponse;
		m_S2CStub.NotifyFinishNhiemVuLienMinh = OnFinishNhiemVuLienMinhResponse;
		m_S2CStub.NotifyDangHuongLienMinh = OnDangHuongLienMinhResponse;
		m_S2CStub.NotifyDoiThuongLienMinh = OnDoiThuongLienMinhResponse;
		m_S2CStub.NotifyDoiMinhChu = OnDoiMinhChuResponse;
		m_S2CStub.NotifyDoiPhoMinhChu = OnDoiPhoMinhChuResponse;
		m_S2CStub.NotifyNangCapCongTrinh = OnNangCapCongTrinhResponse;
		m_S2CStub.NotifyDuoiKhoiLienMinh = OnDuoiKhoiLienMinhResponse;
		m_S2CStub.NotifyCT2KetThuc = OnCT2KetThuc;
		m_S2CStub.NotifyCT2PlayerState = OnCT2PlayerState;
		m_S2CStub.NotifyResetNhiemVuLienMinh = OnResetNhiemVuLienMinhResponse;
		m_S2CStub.NotifySuaThongBaoLienMinh = OnSuaThongBaoLienMinhResponse;
		m_S2CStub.NotifySendChatLienMinh = OnSendLienMinhChatResponse;
		m_S2CStub.NotifyChatLienMinhInfo = OnChatLienMinhInfoResponse;
		m_S2CStub.NotifyUpdateLienMinhData = OnUpdateLienMinhInfoResponse;
		m_S2CStub.NotifySearchLienMinh = OnSearchLienMinhResponse;
		m_S2CStub.NotifyBangChienGetInfo = OnBangChienGetInfoResponse;
		m_S2CStub.NotifyBangChienVaoThanh = OnBangChienVaoThanhResponse;
		m_S2CStub.NotifyBangChienCongThanh = OnBangChienCongThanh;
		m_S2CStub.NotifyBangChienDenCongThanh = OnBangChienDenCongThanh;
		m_S2CStub.NotifyBangChienListUserMove = OnBangChienListOtherUser;
		m_S2CStub.NotifyBangChienGetPhanThuongThuThanh = OnBangChienGetPhanThuongThuThanh;
		m_S2CStub.NotifyBangChienKetThuc = OnBangChienKetThucResponse;
		m_S2CStub.NotifyLapNguyenKhi = OnLapNguyenKhiResponse;
		m_S2CStub.NotifyThaoNguyenKhi = OnThaoNguyenKhiResponse;
		m_S2CStub.NotifyNangCapNguyenKhi = OnNangCapNguyenKhiResponse;
		m_S2CStub.NotifyMuaNguyenKhi = OnMuaNguyenKhiResponse;
		m_S2CStub.NotifyChonHatGiong = OnChonHatGiongResponse;
		m_S2CStub.NotifyLayHatGiong = OnLayHatGiongResponse;
		m_S2CStub.NotifyAnTrom = OnAnTromResponse;
		m_S2CStub.NotifyThuHoach = OnThuHoachResponse;
		m_S2CStub.NotifyTrongCay = OnTrongCayResponse;
		m_S2CStub.NotifyGetLinhDuocInfo = OnGetLinhDuocInfoResponse;
		m_S2CStub.NotifyGetChatLienSrv = OnChatLienSrvResponse;
		m_S2CStub.NotifyGetGamerLinhDuoc = OnGetGamerLinhDuocResponse;
		m_S2CStub.NotifyGetAnTromList = OnGetAnTromListResponse;
		m_S2CStub.NotifyDungNgua = OnDungNguaResponse;
		m_S2CStub.NotifyThaoNgua = OnThaoNguaResponse;
		m_S2CStub.NotifyActiveNgua = OnActiveNguaResponse;
		m_S2CStub.NotifyDangNhapNhanThuongTet = OnDangNhapNhanThuongTetResponse;
		m_S2CStub.NotifyBanhChungGetInfo = OnBanhChungInfoResponse;
		m_S2CStub.NotifyBanhChungNhatNguyenLieu = OnBanhChungGetNguyenLieu;
		m_S2CStub.NotifyBanhChungNauBanh = OnBanhChungNauBanh;
		m_S2CStub.NotifyBanhChungOthers = OnBanhChungOthers;
		m_S2CStub.NotifyBanhChungBXH = OnBanhChungGetBXH;
		m_S2CStub.NotifyBanhChungGetPhanThuong = OnBanhChungNhanThuongResponse;
		m_S2CStub.NotifyGetLeagueData = OnGetLeagueDataResponse;
		m_S2CStub.NotifySubmitDoiHinhLeague = OnSubmitDoiHinhLeagueResponse;
		m_S2CStub.NotifyViewLeagueReplay = OnRequestviewLeagueReplayResponse;
		m_S2CStub.NotifyCuongHoaBatQuaiTran = OnCuongHoaBatQuaiTranResponse;
		m_S2CStub.NotifySetSoDoBatQuaiTran = OnSetSoDoBatQuaiTranResponse;
		m_S2CStub.NotifySetDoiHinhThienCangTran = OnSetSoDoThienCangResponse;
		m_S2CStub.NotifyOpenDoiHinhThienCangTran = OnOpenDoiHinhThienCangResponse;
		m_S2CStub.NotifyVongQuay = OnVongQuayResponse;
		m_S2CStub.NotifyQMDInfo = OnGetQMDInfoResponse;
		m_S2CStub.NotifyQMDSelect = OnQMDSelectResponse;
		m_S2CStub.NotifyQMDXongPha = OnQMDXongPhaResponse;
		m_S2CStub.NotifyQMDGetChiTietNPC = OnQMDGetChiTietNPCResponse;
		m_S2CStub.NotifyQMDGetBXH = OnQMDGetBXHResponse;
		m_S2CStub.NotifyGetSieuCupBattle = OnGetSieuCupBattle;
		m_S2CStub.NotifyGetSieuCupData = OnGetSieuCupData;
		m_S2CStub.NotifySieuCupDatCuoc = OnSieuCupDatCuoc;
		m_S2CStub.NotifyThamBaiSieuCup = OnThamBaiSieuCup;
		m_S2CStub.NotifySieuCupChampion = OnSieuCupChampionResponse;
		m_S2CStub.NotifyLienDauData = OnLienDauDataResponse;
		m_S2CStub.NotifyBeQuanDeTu = OnBeQuanDeTuResponse;
		m_S2CStub.NotifyDenGioCT = OnDenGioChienTruong;
		m_S2CStub.NotifyCreateCostume = OnCreateCostume;
		m_S2CStub.NotifyTakeOnCostume = OnTakeOnCostume;
		m_S2CStub.NotifyTakeOffCostume = OnTakeOffCostume;
		m_S2CStub.NotifyTinhLuyenCostume = OnTinhLuyenCostume;
		m_S2CStub.NotifyKhamNgocCostume = OnKhamNgocCostume;
		m_S2CStub.NotifyGoNgocCostume = OnGoNgocCostume;
		m_S2CStub.NotifyNhanThuongTichLuyNap = OnNhanThuongTichLuyNapResponse;
		m_S2CStub.NotifyNhanThuongTichLuyTieu = OnNhanThuongTichLuyTieuResponse;
		m_S2CStub.NotifyGetCacLoaiTop = OnGetCacLoaiTopResponse;
		m_S2CStub.NotifyDoiTenBang = OnDoiTenBangResponse;
		m_S2CStub.NotifyGetThongTinLienServer = OnGetThongTinLienServerResponse;
		m_S2CStub.NotifyBanPhaoHoa = OnBanPhaoHoaNotify;
		m_S2CStub.NotifyBanPhaoHoaEvent = OnBanPhaoHoaEventResponse;
		m_S2CStub.NotifyGetPhanThuongPhaoHoa = OnGetPhanThuongPhaoHoaEventResponse;
		m_S2CStub.NotifyGetTopPhaoHoa = OnGetTopBanPhaoHoaEventResponse;
		m_S2CStub.NotifyGetTopVongQuay = OnGetTopVongQuayResponse;
		m_S2CStub.NotifyGetListOtherUser = OnGetListOtherUserResponse;
		m_S2CStub.NotifyLinhThuongPhaoHoaEvent = OnLinhThuongPhaoHoaEventResponse;
		m_S2CStub.NotifyNhanThuongDapNieu = OnNhanThuongDapNieuResponse;
		m_S2CStub.NotifyChuyenSinhDeTu = OnChuyenSinhDeTuResponse;
		m_S2CStub.NotifySetBoPhapNhanVatBatQuai = OnSetDoBatQuaiResponse;
		m_S2CStub.NotifySetNoiCongNhanVatBatQuai = OnSetDoBatQuaiResponse;
		m_S2CStub.NotifySetTrangBiNhanVatBatQuai = OnSetDoBatQuaiResponse;
		m_S2CStub.NotifySetThanThu = OnSetThanThuResponse;
		m_S2CStub.NotifyBatThanThu = OnBatThanThuResponse;
		m_S2CStub.NotifyTruongThanhThanThu = OnTruongThanhThanThuResponse;
		m_S2CStub.NotifyNangPhamThanThu = OnNangPhamThanTHuResponse;
		m_S2CStub.NotifyDoiThanThu = OnDoiThanThuResponse;
		m_S2CStub.NotifyThonPheThanThu = OnThonPheThanThuResponse;
		m_S2CStub.NotifyTruyenCongThanThu = OnTruyenCongThanThuResponse;
		m_S2CStub.NotifyGetThanThuDao = OnStartThanThuDaoResponse;
		m_S2CStub.NotifyThamGiaGuiTietKiem = OnThamGiaGuiTietKiemRequest;
		m_S2CStub.NotifyGetGuiTietKiem = OnGetGuiTietKiemRequest;
		m_S2CStub.NotifyGetThuong1MilUser = OnNhanThuong1MilUser;
		m_S2CStub.NotifyPopupThuong1MilUser = OnPopup1MilUser;
		m_S2CStub.NotifyTanCongSonMon = OnTanCongSonMonResponse;
		m_S2CStub.NotifyThuHoachSonMon = OnThuHoachSonMonResponse;
		m_S2CStub.NotifyXayDungSonMon = OnXayDungSonMonResponse;
		m_S2CStub.NotifyDoiHinhSonMon = OnDoiHinhSonMonResponse;
		m_S2CStub.NotifyDoThamSonMon = OnDoThamSonMonResponse;
		m_S2CStub.NotifyGetTopSonMon = OnGetTopSonMonResponse;
		m_S2CStub.NotifyKhaiQuangTrangBi = OnKhaiQuangTrangBiResponse;
		m_S2CStub.NotifyTayLuyenTrangBi = OnTayLuyenTrangBiResponse;
		m_S2CStub.NotifyDungLuyenTrangBi = OnDungLuyenTrangBiResponse;
		m_S2CStub.NotifyConfirmTayLuyenTrangBi = OnConfirmThanBinhTrangBiResponse;
		m_S2CStub.NotifyGetSonMonInfo = OnGetSonMonInfoResponse;
		m_S2CStub.NotifyMuaDoThanBi = OnMuaDoThanBiResponse;
		m_S2CStub.NotifyUnLockVoCong = OnUnLockVoCongResponse;
		m_S2CStub.NotifyQuayBacMayMan = OnQuayBacMayManResponse;
		m_S2CStub.NotifyGetLanhDiaInfo = OnGetLanhDiaInfoResponse;
		m_S2CStub.NotifyQuayTuBaoBon = OnQuayTuBaoBonResponse;
		m_S2CStub.NotifyUpdateLanhDiaData = OnUpdateLanhDiaResponse;
		m_S2CStub.NotifyMoveLanhDia = OnMoveLanhDiaResponse;
		m_S2CStub.NotifyQuayThienMaHaPhong = OnQuayThienMaHaPhongResponse;
		m_S2CStub.NotifySpawnNienThu = OnSpawnNienThuReponse;
		m_S2CStub.NotifyThamGiaNienThu = OnThamGiaNienThuResponse;
		m_S2CStub.NotifyDanhNienThu = OnDanhNienThuResponse;
		m_S2CStub.NotifySummonNienThu = OnSummonNienThuResponse;
		m_S2CStub.NotifyNopLenhBaiNienThu = OnNopLenhBaiNienThuResponse;
		m_S2CStub.NotifyNhanThuongNapHangNgay = OnNhanThuongTichNapHangNgayResponse;
		m_S2CStub.NotifyGetTopMoRuong = OnGetTopMoRuongResponse;
		m_S2CStub.NotifyGetTayVucInfo = OnGetTayVucInfoResponse;
		m_S2CStub.NotifyMuaDoTayVuc = OnGetMuaDoTayVucResponse;
		m_S2CStub.NotifyGetTopNienThu = OnGetTopNienThuResponse;
		m_S2CStub.NotifySelectStartNgua = OnSelectStartNguaResponse;
		m_S2CStub.NotifyUpdateNienThu = OnUpdateNienThuResponse;
		m_S2CStub.NotifyGetDiemMoRuong = OnGetDiemMoRuongResponse;
		m_S2CStub.NotifyThuongDailyActivities = OnThuongDailyActivitiesResponse;
		m_S2CStub.NotifyUpdateDailyActivities = OnUpdateDailyActivitiesReponse;
		m_S2CStub.NotifyThienMaQuaySlot = OnThienMaQuaySlotReponse;
		m_S2CStub.NotifyThienMaQuaySlotConfirm = OnThienMaQuaySlotConfirmReponse;
		m_S2CStub.NotifyThienMaLenhCreate = OnThienMaLenhCreateReponse;
		m_S2CStub.NotifyThienMaOpenSlot = OnThienMaOpenSlotReponse;
		m_S2CStub.NotifyThienMaUpgradeSlot = OnThienMaUpgradeSlotReponse;
		m_S2CStub.NotifyThienMaEquip = OnNotifyThienMaEquipReponse;
		m_S2CStub.NotifySetTonHieu = OnSetTonHieuResponse;
		m_S2CStub.NotifySetTrangBiHoangKim = OnSetTrangBiHoangKimResponse;
		m_S2CStub.NotifyDanhAnDanhCaoThu = OnDanhAnDanhCaoThu;
		m_S2CStub.NotifyStartBoiDuongTrangBi = OnStartBoiDuongTrangBiResponse;
		m_S2CStub.NotifyEndBoiDuongTrangBi = OnEndBoiDuongTrangBiResponse;
		m_S2CStub.NotifyRutQueTienNhan = OnRutQueTienNhanResponse;
		m_S2CStub.NotifyGetBaoKhoInfo = OnGetBaoKhoInfoResponse;
		m_S2CStub.NotifyCuopBaoKho = OnCuopBaoKhoResponse;
		m_S2CStub.NotifyThuHoachBaoKho = OnThuHoachBaoKhoResponse;
		m_S2CStub.NotifyGetListBaoKho = OnGetListBaoKhoResponse;
		m_S2CStub.NotifyQuayCamCungBiBao = OnQuayCamCungResponse;
		m_S2CStub.NotifyNhanThuongCamCung = OnNhanThuongCamCungResponse;
		m_S2CStub.NotifyGetHoaVangInfo = OnGetHoaVangInfoResponse;
		m_S2CStub.NotifyQuayDiemHoaVang = OnQuayDiemHoaVangResponse;
		m_S2CStub.NotifyHoaVang = OnHoaVangResponse;
		m_S2CStub.NotifyUseTuiThan = OnUseTuiThanResponse;
		m_S2CStub.NotifyNhanThuongDailyKNB = OnNhanThuongDailyKNBResponse;
		m_S2CStub.NotifyTestProudNet = OnNotifyTestProudNet;
		m_S2CStub.NotifyChienHonChangeBuff = OnNotifyChienHonChangeBuff;
		m_S2CStub.NotifyStartBoiDuongChienHon = OnNotifyStartBoiDuongChienHon;
		m_S2CStub.NotifyEndBoiDuongChienHon = OnNotifyEndBoiDuongChienHon;
		m_S2CStub.NotifyTrieuHoiChienHon = OnNotifyTrieuHoiChienHon;
		m_S2CStub.NotifyUseTanHonTangLevelChienHon = OnNotifyUseTanHonTangLevelChienHon;
		m_S2CStub.NotifyDotPhaChienHon = OnNotifyDotPhaChienHon;
		m_S2CStub.NotifyCheTaoHuyenKhi = OnNotifyCheTaoHuyenKhi;
		m_S2CStub.NotifyThangCapHuyenKhi = OnNotifyThangCapHuyenKhi;
		m_S2CStub.NotifyTrangBiHuyenKhi = OnNotifyTrangBiHuyenKhi;
		m_S2CStub.NotifyTingTing = (HostID remote, RmiContext rmiContext, string msg) =>
		{
			EGDebug.Log(msg);
			MessagePopup.Create(msg);
			return true;
		};
		m_S2CStub.NotifyGetInfoSuccess = (HostID remote, RmiContext rmiContext, string data) =>
		{
			OnReceiveGamerInfo(data);
			return true;
		};
		m_S2CStub.NotifyNextLogonSuccess = (HostID remote, RmiContext rmiContext, string data) =>
		{
			OnGetResponse();
			CheckUserResponse checkUserResponse = JsonMapper.ToObject<CheckUserResponse>(data);
			if (checkUserResponse != null)
			{
				if (checkUserResponse.ErrorCode == ERROR_CODE.OK)
				{
					_sessionReady = true;
					_retryDelay = 1f;
					GameManager.instance.GamerID = checkUserResponse.GID;
					GameManager.instance.ServerID = checkUserResponse.GameServerID;
					if (!checkUserResponse.reconnect && GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBattle)
					{
						ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
						screenBattle.BattleEnd();
					}
					PlayerPrefs.SetInt("SaveServerId" + GameManager.instance.m_userName, checkUserResponse.GameServerID);
					EGDebug.Log("Request gamer info");
					if (GUIManager.instance.CurrentScreen != GAME_SCREEN.ScreenCT2)
					{
						if (checkUserResponse.reconnect && m_requestFuncPending != null)
						{
							m_requestFuncPending(HostID.Server, RmiContext.ReliableSend, m_requestDataPending);
							PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
						}
						else
						{
							GameManager.instance.m_GameClient.RequestGetGamerInfo();
						}
					}
				}
				else if (checkUserResponse.ErrorCode == ERROR_CODE.DISPLAY_MESSAGE)
				{
					MessagePopup.Create(checkUserResponse.ErrorMessage);
					EGDebug.LogWarning(checkUserResponse.ErrorMessage);
				}
				else
				{
					string text = string.Format("ErrorCode: {0}, Message: {1}", checkUserResponse.ErrorCode, checkUserResponse.ErrorMessage);
					MessagePopup.Create(text);
					EGDebug.LogWarning(text);
				}
			}
			else
			{
				EGDebug.LogError("OnKichHoatGiftCodeResponse : response null");
			}
			return true;
		};
		m_S2CStub.NotifyGetListLKSuccess = (HostID remote, RmiContext rmiContext, string user_list) =>
		{
			OnGetResponse();
			GameManager.instance.m_GameClient.m_listLKUser = JsonMapper.ToObject<List<UserInfo>>(user_list);
			return true;
		};
		m_S2CStub.NotifyGetBattleResultSuccess = (HostID remote, RmiContext rmiContext, string battle_result) =>
		{
			try
			{
				Utils.LogBattle(battle_result, "battle_nen.txt");
				OnGetResponse();
				BattleReplay battleReplay = JsonMapper.ToObject<BattleReplay>(battle_result);
				string txt = EGUtils.Decompress(battle_result);
				Utils.LogBattle(txt, "battle_giai_nen.txt");
				if (battleReplay.ErrorCode == ERROR_CODE.OK)
				{
					GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
					List<UserInfo.HeroData> listCloneHeroFromDoiHinh = UserInfo.GetListCloneHeroFromDoiHinh();
					PopupBattleResult popupBattleResult = PopupBattleResult.Create(battleReplay, listCloneHeroFromDoiHinh, 0L, 0L, 0L, 0);
					popupBattleResult.gameObject.SetActive(false);
					ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
					screenBattle.Replay(battleReplay);
				}
				else
				{
					EGDebug.Log("NotifyGetBattleResultSuccess failed");
				}
			}
			catch (Exception ex)
			{
				EGDebug.Log(ex.Message);
			}
			return true;
		};
		m_S2CStub.NotifyAck = (HostID remote, RmiContext rmiContext, string msg) =>
		{
			OnGetResponse();
			ExDataBase exDataBase = JsonMapper.ToObject<ExDataBase>(msg);
			if (exDataBase != null && !string.IsNullOrEmpty(exDataBase.ErrorMessage))
			{
				MessagePopup.Create(exDataBase.ErrorMessage);
				EGDebug.Log(exDataBase.ErrorMessage);
			}
			return true;
		};
	}
}

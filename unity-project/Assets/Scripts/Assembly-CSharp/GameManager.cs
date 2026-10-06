using System;
using System.Collections;
using System.IO;
using CodeStage.AntiCheat.Detectors;
using HTMLEngine;
using HTMLEngine.NGUI;
using HTMLEngine.Unity3D;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;

	public static int GAMER_VERSION = 100000;

	public string m_userName;

	public string m_userPass;

	public string m_accessToken;

	public string m_serverIP;

	public LoginResponse m_LoginResponse;

	public bool Trailer;

	private EntryClient _entryCls;

	private GameClient _gameCls;

	public GAME_SCREEN m_gameScreen = GAME_SCREEN.ScreenLogin;

	public ScreenBase currentScreen;

	public AddressPort m_addrPort;

	public int GamerID;

	public int ServerID;

	public bool isStartJoinCT2;

	public bool CT2KetThuc = true;

	public AudioSource loginMusic;

	public AudioSource mainMusic;

	public AudioSource battleMusic;

	public AudioSource dantruongMusic;

	public DateTime m_PauseTime = DateTime.Now;

	public bool isPlayTrailer;

	public bool isStartTutorial;

	private int currentScreenHeight;

	private string qualitySetting = "FullHD";

	private string aASetting = "Uncensored";

	public static GameManager instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = GameObject.Find("GameManager");
				if (gameObject != null)
				{
					_instance = gameObject.GetComponent<GameManager>();
				}
			}
			return _instance;
		}
	}

	public EntryClient m_EntryClient
	{
		get
		{
			if (_entryCls == null)
			{
				_entryCls = UnityEngine.Object.FindObjectOfType<EntryClient>();
			}
			return _entryCls;
		}
	}

	public GameClient m_GameClient
	{
		get
		{
			if (_gameCls == null)
			{
				_gameCls = UnityEngine.Object.FindObjectOfType<GameClient>();
			}
			return _gameCls;
		}
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (GUIManager.instance == null)
		{
			return;
		}
		if (!pauseStatus)
		{
			if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenMain)
			{
				SohaSDKManager.instance.ShowSohaDashBoardButton();
			}
			if (PlayerPrefs.GetInt("sounds_value", 1) == 1)
			{
				AudioListener.pause = false;
			}
			else
			{
				AudioListener.pause = true;
			}
			if (!((DateTime.Now - m_PauseTime).TotalSeconds > 300.0) || GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenStart || GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenLogin || GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenSelectServer || GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenSelectFirstDeTu || GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenReg || TutorialPopup.instance != null)
			{
				return;
			}
			GAME_SCREEN gAME_SCREEN = GUIManager.instance.CurrentScreen;
			if (gAME_SCREEN == GAME_SCREEN.ScreenBattle)
			{
				ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
				if (screenBattle != null)
				{
					screenBattle.BattleEnd();
				}
			}
			m_GameClient.RequestGetGamerInfo();
		}
		else if (pauseStatus)
		{
			m_PauseTime = DateTime.Now;
			if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenMain)
			{
				SohaSDKManager.instance.HideSohaDashBoardButton();
			}
		}
	}

	private void SwitchMusic(AudioSource target)
	{
		if (target == null)
		{
			return;
		}
		AudioSource[] array = new AudioSource[4] { loginMusic, mainMusic, battleMusic, dantruongMusic };
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null && array[i] != target && array[i].isPlaying)
			{
				array[i].Stop();
			}
		}
		target.volume = 1f;
		if (!target.isPlaying)
		{
			target.Play();
		}
	}

	public void FadeInLoginMusic(float sec)
	{
		SwitchMusic(loginMusic);
	}

	public void FadeInMainMusic(float sec)
	{
		SwitchMusic(mainMusic);
	}

	public void FadeInBattleMusic(float sec)
	{
		SwitchMusic(battleMusic);
	}

	public void FadeInDanTruongMusic(float sec)
	{
		SwitchMusic(dantruongMusic);
	}

	public IEnumerator FadeInOutMusic(AudioSource src, AudioSource desc, float sec)
	{
		SwitchMusic(desc);
		yield break;
	}

	private void Awake()
	{
		_instance = this;
		UnityEngine.Object.Destroy(GameObject.Find("Camera Skybox"));
		HtEngine.RegisterLogger(new Unity3DLogger());
		HtEngine.RegisterDevice(new NGUIDevice());
		HtEngine.LinkHoverColor = HtColor.Parse("#FF4444");
		HtEngine.LinkPressedFactor = 0.5f;
		HtEngine.LinkFunctionName = "onLinkClicked";
		SpeedHackDetector.StartDetection(OnDetectHackSpeed, 1f, 5, 30);
		SpeedHackDetector.Instance.autoDispose = false;
	}

	public void OnDetectHackSpeed()
	{
		MessagePopup.Create(Localization.instance.Get("DetectHackspeed"), 3f);
		GUIManager.instance.ReloadScene(3f);
	}

	internal void onLinkClicked(GameObject senderGo)
	{
		NGUILinkText component = senderGo.GetComponent<NGUILinkText>();
		if (component != null)
		{
			Application.OpenURL(component.linkText);
		}
	}

	public void StartDownloadConfig()
	{
		StartCoroutine(DownloadConfigAndCache());
	}

	private IEnumerator DownloadConfigAndCache()
	{
		if (ConfigManager.instance.m_dicNhanVats == null || ConfigManager.instance.m_dicNhanVats.Count == 0)
		{
			PopupNetworkLoading.Create(Localization.instance.Get("LoadingConfigAsset"));
			yield return StartCoroutine(m_GameClient.LoadAllCfg(0f, 1f));
			PopupNetworkLoading.DestroyPopup();
		}
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenSelectServer);
	}

	private void SetScreenSize()
	{
		if (currentScreenHeight >= 1080)
		{
			Screen.SetResolution(600, 980, false);
		}
		else if (currentScreenHeight >= 900)
		{
			Screen.SetResolution(540, 800, false);
		}
		else
		{
			Screen.SetResolution(500, 720, false);
		}
	}

	private void Start()
	{
		_entryCls = UnityEngine.Object.FindObjectOfType<EntryClient>();
		_gameCls = UnityEngine.Object.FindObjectOfType<GameClient>();
		TextAsset textAsset = (TextAsset)Resources.Load("Config/Host", typeof(TextAsset));
		StringReader stringReader = new StringReader(textAsset.text);
		m_serverIP = stringReader.ReadLine();
		Trailer = false;
		PlayerPrefs.SetInt("sounds_value", 1);
		AudioListener.pause = false;
		AudioListener.volume = 1f;
		NGUITools.soundVolume = 1f;
		FadeInLoginMusic(0f);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && TutorialPopup.instance == null)
		{
			PopupYesNo.Create(Localization.instance.Get("QuitGameLabel"), Localization.instance.Get("MessageResetLuotGHYes"), Localization.instance.Get("MessageResetLuotGHNo"), OnQuitOkClick, null);
		}
	}

	public void OnQuitOkClick()
	{
		Application.Quit();
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using Nettention.Proud;
using UnityEngine;

using UnityEngine.SceneManagement;
public class GUIManager : MonoBehaviour
{
	public List<FormBase> formStack = new List<FormBase>();

	public static Color COLOR_LIGHT_BLUE = new Color(0.2f, 0.6f, 255f);

	public static Color COLOR_LIGHT_BLUE2 = new Color(73f / 255f, 134f / 255f, 195f / 255f);

	public static GUIManager instance;

	public UIWidget GameFrame;

	public UIAtlas otherAtlas;

	public UIAtlas nhanvatAtlas;

	public UIAtlas costumeAtlas;

	private bool loadingCostume;

	public bool isAutoBatCoc;

	public bool isAutoThachDau;

	public bool isAutoCamDia;

	public bool isAutoHacMocNhai;

	public ChiSoCoBan autoHMN_ChiSoUuTien1 = ChiSoCoBan.None;

	public ChiSoCoBan autoHMN_ChiSoUuTien2 = ChiSoCoBan.None;

	private Dictionary<GAME_SCREEN, ScreenBase> SCREENS;

	public UIRoot GUI2DRoot;

	public GameObject gadgetLogo;

	public GadGetPanelTop gadgetPanelTop;

	public GadgetPanelBottom gadgetPanelBottom;

	public ScreenMain3D homeCity;

	public ScreenLienMinh3D lienMinh3D;

	public Camera cam2D;

	public EGFPSCounter fpsCounter;

	public GameObject popUpContainer;

	public Transform ScreenContainer;

	public Transform ScreenContainer3D;

	private GAME_SCREEN _currentScreen = GAME_SCREEN.ScreenMain;

	private GAME_SCREEN _lastScreen = GAME_SCREEN.ScreenLogin;

	private AsyncOperation ac;

	private float mBgNext;

	private HashSet<int> mAdaptedPanels = new HashSet<int>();

	private bool _isReady;

	public GAME_SCREEN CurrentScreen
	{
		get
		{
			return _currentScreen;
		}
		private set
		{
			_currentScreen = value;
		}
	}

	public GAME_SCREEN LastScreen
	{
		get
		{
			return _lastScreen;
		}
		private set
		{
			_lastScreen = value;
		}
	}

	public bool IsReady
	{
		get
		{
			return _isReady;
		}
		set
		{
			_isReady = value;
		}
	}

	public void SetCostumeAtlas(UISprite avatar, bool makePerfectPixel)
	{
		if (costumeAtlas != null)
		{
			avatar.atlas = costumeAtlas;
			if (makePerfectPixel)
			{
				avatar.MakePixelPerfect();
			}
		}
		else if (loadingCostume)
		{
			StartCoroutine(DelaySetCostumeAtlas(avatar, makePerfectPixel));
		}
		else
		{
			StartCoroutine(SetCostumeAtlasRoutine(avatar, makePerfectPixel));
		}
	}

	private IEnumerator DelaySetCostumeAtlas(UISprite avatar, bool makePerfectPixel)
	{
		while (loadingCostume)
		{
			yield return null;
		}
		if (costumeAtlas != null)
		{
			avatar.atlas = costumeAtlas;
			if (makePerfectPixel)
			{
				avatar.MakePixelPerfect();
			}
		}
	}

	private IEnumerator SetCostumeAtlasRoutine(UISprite avatar, bool makePerfectPixel)
	{
		loadingCostume = true;
		if (ConfigManager.instance == null || ConfigManager.instance.OtherConfig == null)
		{
			loadingCostume = false;
			yield break;
		}
		string bundleLink = ConfigManager.instance.OtherConfig.CostumeAtlasBundleAndroid;
		WWW www = WWW.LoadFromCacheOrDownload(bundleLink, CostumeCfg.GetCostumesVersion());
		yield return www;
		if (www.error != null)
		{
			loadingCostume = false;
			EGDebug.Log("Download Costume Atlas failed");
		}
		else
		{
			AssetBundle bundle = www.assetBundle;
			AssetBundleRequest request = bundle.LoadAssetAsync<GameObject>("CostumesAtlas");
			yield return request;
			UnityEngine.Object asset = request.asset;
			GameObject atlasPrefab = (GameObject)((asset is GameObject) ? asset : null);
			if (atlasPrefab != null)
			{
				costumeAtlas = atlasPrefab.GetComponent<UIAtlas>();
				avatar.atlas = costumeAtlas;
				loadingCostume = false;
				if (makePerfectPixel)
				{
					avatar.MakePixelPerfect();
				}
			}
			bundle.Unload(false);
		}
		yield return null;
	}

	public void addForm(FormBase fb)
	{
		if (formStack.Contains(fb))
		{
			formStack.Remove(fb);
		}
		formStack.Add(fb);
	}

	public void removeForm(FormBase fb)
	{
		if (formStack.Contains(fb))
		{
			formStack.Remove(fb);
		}
	}

	public void ReloadScene(float waitTimeForReloadScene)
	{
		StartCoroutine(ReloadLevel(0, waitTimeForReloadScene));
	}

	public void ReloadScene()
	{
		StartCoroutine(ReloadLevel(0));
	}

	private IEnumerator ReloadLevel(int level, float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		yield return StartCoroutine(ReloadLevel(level));
	}

	private IEnumerator ReloadLevel(int level)
	{
		PopupNetworkLoading.Create(Localization.instance.Get("LogoutMsg"));
		ac = SceneManager.LoadSceneAsync(level);
		yield return ac;
	}

	public IEnumerator Init()
	{
		Application.targetFrameRate = -1;
		NetConfig.ClientHeartbeatIntervalMs = 100u;
		GameManager.instance.m_EntryClient.Init();
		PopupLoading.instance.SetAmmount(0.1f);
		yield return null;
		GameManager.instance.m_GameClient.Init();
		PopupLoading.instance.SetAmmount(0.2f);
		yield return null;
		yield return StartCoroutine(GameManager.instance.m_GameClient.LoadAllCfg(0.2f, 0.7f));
		PopupLoading.instance.SetAmmount(0.7f);
		yield return null;
		SCREENS = new Dictionary<GAME_SCREEN, ScreenBase>();
		LoadScreen(GAME_SCREEN.ScreenMain);
		PopupLoading.instance.SetAmmount(0.75f);
		yield return null;
		LoadScreen(GAME_SCREEN.ScreenLogin);
		PopupLoading.instance.SetAmmount(0.76f);
		yield return null;
		EGDebug.Log("StartLoadScene");
		yield return StartCoroutine(LoadScene3D());
		PopupLoading.instance.SetAmmount(1f);
		yield return new WaitForEndOfFrame();
		PopupLoading.DestroyPopup();
		Application.targetFrameRate = 25;
		setScreen(GAME_SCREEN.ScreenLogin);
	}

	private IEnumerator LoadBattleAnimRoutine(NhanVatCfg cfg, List<string> clipName, AnimVuKhi vk, GameObject avatarGO)
	{
		if (cfg == null)
		{
			yield break;
		}
		string resourcePath = "FX/Anim/" + vk;
		if (cfg.Sex == NhanVatCfg.GioiTinh.Nu)
		{
			resourcePath += "_F";
		}
		EGResourceAsyncLoader l2 = EGResourceAsyncLoader.Load(resourcePath);
		while (!l2.IsDone)
		{
			yield return null;
		}
		if (avatarGO == null)
		{
			yield break;
		}
		UnityEngine.Object asset = l2.Asset;
		GameObject animContainer = (GameObject)((asset is GameObject) ? asset : null);
		if (!(animContainer != null))
		{
			yield break;
		}
		foreach (AnimationState item in animContainer.GetComponent<Animation>())
		{
			AnimationState state = item;
			if (clipName.Contains(state.name))
			{
				AnimationClip clip = animContainer.GetComponent<Animation>().GetClip(state.name);
				avatarGO.GetComponent<Animation>().AddClip(clip, state.name);
			}
		}
	}

	private IEnumerator LoadSkillAnimRoutine(NhanVatCfg cfg, List<string> clipName, GameObject avatarGO)
	{
		if (cfg == null)
		{
			yield break;
		}
		string resourcePath = "FX/Anim/skill";
		if (cfg.Sex == NhanVatCfg.GioiTinh.Nu)
		{
			resourcePath = "FX/Anim/skill_F";
		}
		EGResourceAsyncLoader l2 = EGResourceAsyncLoader.Load(resourcePath);
		while (!l2.IsDone)
		{
			yield return null;
		}
		if (avatarGO == null)
		{
			yield break;
		}
		UnityEngine.Object asset = l2.Asset;
		GameObject animContainer = (GameObject)((asset is GameObject) ? asset : null);
		if (!(animContainer != null))
		{
			yield break;
		}
		foreach (AnimationState item in animContainer.GetComponent<Animation>())
		{
			AnimationState state = item;
			if (clipName.Contains(state.name))
			{
				AnimationClip clip = animContainer.GetComponent<Animation>().GetClip(state.name);
				avatarGO.GetComponent<Animation>().AddClip(clip, state.name);
			}
		}
	}

	private IEnumerator LoadAvatar3DByCostumeWithBattleAnim(Avatar3D avatar3D, string codeName, string vuKhi, string bophap, string noicong, string costume, bool addVCDefault = false)
	{
		NhanVatCfg cfg = null;
		if (!ConfigManager.instance.m_dicNhanVats.TryGetValue(codeName, out cfg))
		{
			EGDebug.LogError("Nhan Vat Code Invalid - " + codeName);
		}
		EGResourceAsyncLoader l;
		UnityEngine.Object o;
		if (string.IsNullOrEmpty(costume))
		{
			l = EGResourceAsyncLoader.Load("nhanvat/" + codeName, false);
			while (!l.IsDone)
			{
				yield return null;
			}
			o = l.Asset;
		}
		else
		{
			CostumeCfg costumeCfg = null;
			if (ConfigManager.instance.m_dicCostumeCfg.TryGetValue(costume, out costumeCfg))
			{
				EGDebug.Log("Load Costume " + costume);
				l = EGResourceAsyncLoader.Load("costumes/" + costume, false);
				while (!l.IsDone)
				{
					yield return null;
				}
				o = l.Asset;
			}
			else
			{
				EGDebug.Log("Costume " + costume + " invalid");
				l = EGResourceAsyncLoader.Load("nhanvat/" + codeName, false);
				while (!l.IsDone)
				{
					yield return null;
				}
				o = l.Asset;
			}
		}
		if (o == null)
		{
			string empty = string.Empty;
			string resPath = ((cfg.Sex != NhanVatCfg.GioiTinh.Nam) ? "NhanVat/NV_HOANG_DUNG" : "NhanVat/NV_VUONG_TRUNG_DUONG");
			EGResourceAsyncLoader l2 = EGResourceAsyncLoader.Load(resPath);
			while (!l2.IsDone)
			{
				yield return null;
			}
			o = l2.Asset;
		}
		if (o != null)
		{
			if (avatar3D == null)
			{
				yield break;
			}
			UnityEngine.Object obj = UnityEngine.Object.Instantiate(o);
			GameObject avatar = (GameObject)((obj is GameObject) ? obj : null);
			avatar.transform.parent = avatar3D.transform;
			avatar.transform.localPosition = UnityEngine.Vector3.zero;
			avatar.transform.localRotation = Quaternion.identity;
			avatar.transform.localScale = UnityEngine.Vector3.one;
			avatar.gameObject.SetActive(false);
			Utils.SetLayer(avatar.transform, "Unit", true);
			yield return null;
			string resourcePath = "FX/Anim/" + avatar3D.AnimVK;
			if (cfg != null && cfg.Sex == NhanVatCfg.GioiTinh.Nu)
			{
				resourcePath += "_F";
			}
			EGResourceAsyncLoader l3 = EGResourceAsyncLoader.Load(resourcePath);
			while (!l3.IsDone)
			{
				yield return null;
			}
			if (avatar3D == null || avatar == null)
			{
				yield break;
			}
			UnityEngine.Object asset = l3.Asset;
			GameObject animContainer = (GameObject)((asset is GameObject) ? asset : null);
			if (animContainer != null)
			{
				foreach (AnimationState item in animContainer.GetComponent<Animation>())
				{
					AnimationState state = item;
					AnimationClip clip = animContainer.GetComponent<Animation>().GetClip(state.name);
					avatar.GetComponent<Animation>().AddClip(clip, state.name);
				}
			}
			if (addVCDefault)
			{
				GameObject animContainer3 = (GameObject)Resources.Load((cfg == null || cfg.Sex != NhanVatCfg.GioiTinh.Nu) ? "FX/Anim/skill" : "FX/Anim/skill_F");
				if (animContainer != null)
				{
					AnimationClip clip2 = animContainer3.GetComponent<Animation>().GetClip(cfg.VoCongMacDinh.ToString());
					avatar.GetComponent<Animation>().AddClip(clip2, cfg.VoCongMacDinh.ToString());
				}
			}
			avatar.gameObject.SetActive(true);
			avatar3D.AvatarGO = avatar;
			avatar3D.AnimVK = AnimVuKhi.NV_BaoTay;
			avatar3D.CodeName = codeName;
			avatar3D.LoadVK(vuKhi);
			avatar3D.LoadBoPhap(bophap);
			avatar3D.LoadNoiCong(noicong);
		}
		yield return null;
		if (l != null)
		{
			UnityEngine.Object.Destroy(l.gameObject);
		}
	}

	private IEnumerator LoadAvatar3DWithBattleAnim(Avatar3D avatar3D, string codeName, string vuKhi, string bophap, string noicong)
	{
		EGResourceAsyncLoader l = EGResourceAsyncLoader.Load("nhanvat/" + codeName, false);
		while (!l.IsDone)
		{
			yield return null;
		}
		NhanVatCfg cfg = null;
		if (!ConfigManager.instance.m_dicNhanVats.TryGetValue(codeName, out cfg))
		{
			EGDebug.LogError("Nhan Vat Code Invalid - " + codeName);
		}
		UnityEngine.Object o = l.Asset;
		if (o == null)
		{
			string empty = string.Empty;
			string resPath = ((cfg.Sex != NhanVatCfg.GioiTinh.Nam) ? "NhanVat/NV_HOANG_DUNG" : "NhanVat/NV_VUONG_TRUNG_DUONG");
			EGResourceAsyncLoader l2 = EGResourceAsyncLoader.Load(resPath);
			while (!l2.IsDone)
			{
				yield return null;
			}
			o = l2.Asset;
		}
		if (o != null)
		{
			if (avatar3D == null)
			{
				yield break;
			}
			UnityEngine.Object obj = UnityEngine.Object.Instantiate(o);
			GameObject avatar = (GameObject)((obj is GameObject) ? obj : null);
			avatar.transform.parent = avatar3D.transform;
			avatar.transform.localPosition = UnityEngine.Vector3.zero;
			avatar.transform.localRotation = Quaternion.identity;
			avatar.transform.localScale = UnityEngine.Vector3.one;
			avatar.gameObject.SetActive(false);
			Utils.SetLayer(avatar.transform, "Unit", true);
			yield return null;
			string resourcePath = "FX/Anim/" + avatar3D.AnimVK;
			if (cfg != null && cfg.Sex == NhanVatCfg.GioiTinh.Nu)
			{
				resourcePath += "_F";
			}
			EGResourceAsyncLoader l3 = EGResourceAsyncLoader.Load(resourcePath);
			while (!l3.IsDone)
			{
				yield return null;
			}
			if (avatar3D == null || avatar == null)
			{
				yield break;
			}
			UnityEngine.Object asset = l3.Asset;
			GameObject animContainer = (GameObject)((asset is GameObject) ? asset : null);
			if (animContainer != null)
			{
				foreach (AnimationState item in animContainer.GetComponent<Animation>())
				{
					AnimationState state = item;
					AnimationClip clip = animContainer.GetComponent<Animation>().GetClip(state.name);
					avatar.GetComponent<Animation>().AddClip(clip, state.name);
				}
			}
			avatar.gameObject.SetActive(true);
			avatar3D.AvatarGO = avatar;
			avatar3D.AnimVK = AnimVuKhi.NV_BaoTay;
			avatar3D.CodeName = codeName;
			avatar3D.LoadVK(vuKhi);
			avatar3D.LoadBoPhap(bophap);
			avatar3D.LoadNoiCong(noicong);
		}
		yield return null;
		UnityEngine.Object.Destroy(l.gameObject);
	}

	public Avatar3D InstantiateAvatar3DWithBattleAnim(string codeName, string vuKhi = "", string bophap = "", string noicong = "", string costume = "", bool isDisplayVCDefault = false)
	{
		GameObject gameObject = new GameObject();
		Avatar3D avatar3D = gameObject.AddComponent<Avatar3D>();
		avatar3D.CodeName = codeName;
		avatar3D.CostumeName = costume;
		if (string.IsNullOrEmpty(vuKhi))
		{
			avatar3D.AnimVK = AnimVuKhi.NV_BaoTay;
		}
		else
		{
			TrangBiCfg value = null;
			if (ConfigManager.instance.m_dicTrangBi.TryGetValue(vuKhi, out value))
			{
				avatar3D.AnimVK = value.GetAnimVK();
			}
			else
			{
				avatar3D.AnimVK = AnimVuKhi.NV_BaoTay;
			}
		}
		StartCoroutine(LoadAvatar3DByCostumeWithBattleAnim(avatar3D, codeName, vuKhi, bophap, noicong, costume, isDisplayVCDefault));
		return avatar3D;
	}

	private IEnumerator LoadAvatar3DCostumeFromBundles(Avatar3D avatar3D, string codeName, string vuKhi, string bophap, string noicong, List<string> skillanim, List<string> battleAnim, string thuCuoi = "", string costume = "", string thanthu = "", UserInfo.PetInfo.PetQuality thanthuQuality = UserInfo.PetInfo.PetQuality.PHO_THONG)
	{
		NhanVatCfg cfg = null;
		if (!ConfigManager.instance.m_dicNhanVats.TryGetValue(codeName, out cfg))
		{
			EGDebug.LogError("Nhan Vat Code Invalid - " + codeName);
		}
		EGResourceAsyncLoader l;
		UnityEngine.Object o;
		if (string.IsNullOrEmpty(costume))
		{
			l = EGResourceAsyncLoader.Load("nhanvat/" + codeName, false);
			while (!l.IsDone)
			{
				yield return null;
			}
			o = l.Asset;
		}
		else
		{
			CostumeCfg costumeCfg = null;
			if (ConfigManager.instance.m_dicCostumeCfg.TryGetValue(costume, out costumeCfg))
			{
				EGDebug.Log("Load Costume " + costume);
				l = EGResourceAsyncLoader.Load("costumes/" + costume, false);
				while (!l.IsDone)
				{
					yield return null;
				}
				o = l.Asset;
			}
			else
			{
				EGDebug.Log("Costume " + costume + " invalid");
				l = EGResourceAsyncLoader.Load("nhanvat/" + codeName, false);
				while (!l.IsDone)
				{
					yield return null;
				}
				o = l.Asset;
			}
		}
		if (o == null)
		{
			string empty = string.Empty;
			string resPath = ((cfg == null || cfg.Sex != NhanVatCfg.GioiTinh.Nu) ? "NhanVat/NV_VUONG_TRUNG_DUONG" : "NhanVat/NV_HOANG_DUNG");
			EGResourceAsyncLoader l2 = EGResourceAsyncLoader.Load(resPath);
			while (!l2.IsDone)
			{
				yield return null;
			}
			o = l2.Asset;
		}
		if (o != null)
		{
			if (avatar3D == null)
			{
				yield break;
			}
			UnityEngine.Object obj = UnityEngine.Object.Instantiate(o);
			GameObject avatar = (GameObject)((obj is GameObject) ? obj : null);
			avatar.transform.parent = avatar3D.transform;
			avatar.transform.localPosition = UnityEngine.Vector3.zero;
			avatar.transform.localRotation = Quaternion.identity;
			avatar.transform.localScale = UnityEngine.Vector3.one;
			avatar.gameObject.SetActive(false);
			Utils.SetLayer(avatar.transform, "Unit", true);
			yield return null;
			string resourcePath = "FX/Anim/AnimInCity";
			if (cfg != null && cfg.Sex == NhanVatCfg.GioiTinh.Nu)
			{
				resourcePath = "FX/Anim/AnimInCity_F";
			}
			EGResourceAsyncLoader l3 = EGResourceAsyncLoader.Load(resourcePath);
			while (!l3.IsDone)
			{
				yield return null;
			}
			if (avatar3D == null || avatar == null)
			{
				yield break;
			}
			UnityEngine.Object asset = l3.Asset;
			GameObject animContainer = (GameObject)((asset is GameObject) ? asset : null);
			if (animContainer != null)
			{
				foreach (AnimationState item in animContainer.GetComponent<Animation>())
				{
					AnimationState state = item;
					AnimationClip clip = animContainer.GetComponent<Animation>().GetClip(state.name);
					avatar.GetComponent<Animation>().AddClip(clip, state.name);
				}
			}
			string animSex = "male";
			if (cfg != null && cfg.Sex == NhanVatCfg.GioiTinh.Nu)
			{
				animSex = "female";
			}
			EGResourceAsyncLoader l4 = EGResourceAsyncLoader.Load("fx/anim/NV_NPC_" + animSex);
			while (!l4.IsDone)
			{
				yield return null;
			}
			if (avatar3D == null || avatar == null)
			{
				yield break;
			}
			UnityEngine.Object asset2 = l4.Asset;
			GameObject animContainer2 = (GameObject)((asset2 is GameObject) ? asset2 : null);
			if (animContainer2 != null)
			{
				foreach (AnimationState item2 in animContainer2.GetComponent<Animation>())
				{
					AnimationState state2 = item2;
					AnimationClip clip2 = animContainer2.GetComponent<Animation>().GetClip(state2.name);
					avatar.GetComponent<Animation>().AddClip(clip2, state2.name);
				}
			}
			if (skillanim != null && skillanim.Count > 0)
			{
				yield return StartCoroutine(LoadSkillAnimRoutine(cfg, skillanim, avatar));
			}
			if (battleAnim != null && battleAnim.Count > 0)
			{
				yield return StartCoroutine(LoadBattleAnimRoutine(cfg, battleAnim, avatar3D.AnimVK, avatar));
			}
			avatar.gameObject.SetActive(true);
			avatar3D.AvatarGO = avatar;
			avatar3D.AnimVK = AnimVuKhi.NV_BaoTay;
			avatar3D.CodeName = codeName;
			avatar3D.LoadVK(vuKhi);
			if (!string.IsNullOrEmpty(bophap))
			{
				avatar3D.LoadBoPhap(bophap);
			}
			if (!string.IsNullOrEmpty(noicong))
			{
				avatar3D.LoadNoiCong(noicong);
			}
			avatar3D.LoadThuCuoi(thuCuoi);
			avatar3D.LoadThanThu(thanthu, thanthuQuality);
		}
		yield return null;
		if (l != null)
		{
			UnityEngine.Object.Destroy(l.gameObject);
		}
	}

	private IEnumerator LoadAvatar3D(Avatar3D avatar3D, string codeName, string vuKhi, string bophap, string noicong, List<string> skillanim, List<string> battleAnim, string thuCuoi = "", string thanthu = "", UserInfo.PetInfo.PetQuality thanthuQuality = UserInfo.PetInfo.PetQuality.PHO_THONG)
	{
		NhanVatCfg cfg = null;
		if (!ConfigManager.instance.m_dicNhanVats.TryGetValue(codeName, out cfg))
		{
			EGDebug.LogError("Nhan Vat Code Invalid - " + codeName);
		}
		EGResourceAsyncLoader l = EGResourceAsyncLoader.Load("nhanvat/" + codeName, false);
		while (!l.IsDone)
		{
			yield return null;
		}
		UnityEngine.Object o = l.Asset;
		if (o == null)
		{
			string empty = string.Empty;
			string resPath = ((cfg == null || cfg.Sex != NhanVatCfg.GioiTinh.Nu) ? "NhanVat/NV_VUONG_TRUNG_DUONG" : "NhanVat/NV_HOANG_DUNG");
			EGResourceAsyncLoader l2 = EGResourceAsyncLoader.Load(resPath);
			while (!l2.IsDone)
			{
				yield return null;
			}
			o = l2.Asset;
		}
		if (o != null)
		{
			if (avatar3D == null)
			{
				yield break;
			}
			UnityEngine.Object obj = UnityEngine.Object.Instantiate(o);
			GameObject avatar = (GameObject)((obj is GameObject) ? obj : null);
			avatar.transform.parent = avatar3D.transform;
			avatar.transform.localPosition = UnityEngine.Vector3.zero;
			avatar.transform.localRotation = Quaternion.identity;
			avatar.transform.localScale = UnityEngine.Vector3.one;
			avatar.gameObject.SetActive(false);
			yield return null;
			string resourcePath = "FX/Anim/AnimInCity";
			if (cfg != null && cfg.Sex == NhanVatCfg.GioiTinh.Nu)
			{
				resourcePath = "FX/Anim/AnimInCity_F";
			}
			EGResourceAsyncLoader l3 = EGResourceAsyncLoader.Load(resourcePath);
			while (!l3.IsDone)
			{
				yield return null;
			}
			if (avatar3D == null || avatar == null)
			{
				yield break;
			}
			UnityEngine.Object asset = l3.Asset;
			GameObject animContainer = (GameObject)((asset is GameObject) ? asset : null);
			if (animContainer != null)
			{
				foreach (AnimationState item in animContainer.GetComponent<Animation>())
				{
					AnimationState state = item;
					AnimationClip clip = animContainer.GetComponent<Animation>().GetClip(state.name);
					avatar.GetComponent<Animation>().AddClip(clip, state.name);
				}
			}
			string animSex = "male";
			if (cfg != null && cfg.Sex == NhanVatCfg.GioiTinh.Nu)
			{
				animSex = "female";
			}
			EGResourceAsyncLoader l4 = EGResourceAsyncLoader.Load("fx/anim/NV_NPC_" + animSex);
			while (!l4.IsDone)
			{
				yield return null;
			}
			if (avatar3D == null || avatar == null)
			{
				yield break;
			}
			UnityEngine.Object asset2 = l4.Asset;
			GameObject animContainer2 = (GameObject)((asset2 is GameObject) ? asset2 : null);
			if (animContainer2 != null)
			{
				foreach (AnimationState item2 in animContainer2.GetComponent<Animation>())
				{
					AnimationState state2 = item2;
					AnimationClip clip2 = animContainer2.GetComponent<Animation>().GetClip(state2.name);
					avatar.GetComponent<Animation>().AddClip(clip2, state2.name);
				}
			}
			if (skillanim != null && skillanim.Count > 0)
			{
				yield return StartCoroutine(LoadSkillAnimRoutine(cfg, skillanim, avatar));
			}
			if (battleAnim != null && battleAnim.Count > 0)
			{
				yield return StartCoroutine(LoadBattleAnimRoutine(cfg, battleAnim, avatar3D.AnimVK, avatar));
			}
			avatar.gameObject.SetActive(true);
			avatar3D.AvatarGO = avatar;
			avatar3D.AnimVK = AnimVuKhi.NV_BaoTay;
			avatar3D.CodeName = codeName;
			avatar3D.LoadVK(vuKhi);
			if (!string.IsNullOrEmpty(bophap))
			{
				avatar3D.LoadBoPhap(bophap);
			}
			if (!string.IsNullOrEmpty(noicong))
			{
				avatar3D.LoadNoiCong(noicong);
			}
			avatar3D.LoadThuCuoi(thuCuoi);
			avatar3D.LoadThanThu(thanthu, thanthuQuality);
		}
		yield return null;
		if (l != null)
		{
			UnityEngine.Object.Destroy(l.gameObject);
		}
	}

	public Avatar3D InstantiateAvatar3D(string codeName, string vuKhi = "", string bophap = "", string noicong = "", List<string> skillAnim = null, List<string> battleAnim = null, string thuCuoi = "", string costume = "", string thanthuName = "", UserInfo.PetInfo.PetQuality thanthuQuality = UserInfo.PetInfo.PetQuality.PHO_THONG)
	{
		GameObject gameObject = new GameObject();
		Avatar3D avatar3D = gameObject.AddComponent<Avatar3D>();
		avatar3D.CodeName = codeName;
		avatar3D.CostumeName = costume;
		if (string.IsNullOrEmpty(vuKhi))
		{
			avatar3D.AnimVK = AnimVuKhi.NV_BaoTay;
		}
		else
		{
			TrangBiCfg value = null;
			if (ConfigManager.instance.m_dicTrangBi.TryGetValue(vuKhi, out value))
			{
				avatar3D.AnimVK = value.GetAnimVK();
			}
			else
			{
				avatar3D.AnimVK = AnimVuKhi.NV_BaoTay;
			}
		}
		StartCoroutine(LoadAvatar3DCostumeFromBundles(avatar3D, codeName, vuKhi, bophap, noicong, skillAnim, battleAnim, thuCuoi, costume, thanthuName, thanthuQuality));
		return avatar3D;
	}

	private ScreenBase LoadScreen(GAME_SCREEN screenName)
	{
		UnityEngine.Object obj = (screenName.ToString().Contains("LienMinh") ? Resources.Load("gui/screens/screenlienminh/" + screenName) : ((!screenName.ToString().Contains("ThanThu")) ? Resources.Load("gui/screens/" + screenName) : Resources.Load("gui/screens/screenthanthu/" + screenName)));
		if (obj == null)
		{
			return null;
		}
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(obj);
		if (gameObject == null)
		{
			return null;
		}
		UnityEngine.Vector3 localPosition = gameObject.transform.localPosition;
		gameObject.transform.parent = ScreenContainer;
		gameObject.transform.localPosition = localPosition;
		gameObject.transform.localScale = UnityEngine.Vector3.one;
		ScreenBase component = gameObject.GetComponent<ScreenBase>();
		if (component == null)
		{
			return null;
		}
		if (SCREENS.ContainsKey(screenName))
		{
			SCREENS[screenName] = component;
		}
		else
		{
			SCREENS.Add(screenName, component);
		}
		gameObject.SetActive(false);
		return component;
	}

	private void OnLoadingScene3D(float prog)
	{
		if (PopupLoading.instance != null)
		{
			PopupLoading.instance.SetAmmount(0.76f + 0.24f * prog);
		}
	}

	private void OnLoadingSceneLienMinh3D(float prog)
	{
		if (PopupLoading.instance != null)
		{
			PopupLoading.instance.SetAmmount(prog);
		}
	}

	private void OnFinishLoadingSceneLienMinh3D(UnityEngine.Object obj)
	{
		UnityEngine.Object obj2 = UnityEngine.Object.Instantiate(obj);
		GameObject gameObject = (GameObject)((obj2 is GameObject) ? obj2 : null);
		gameObject.transform.parent = instance.ScreenContainer3D;
		gameObject.transform.localPosition = UnityEngine.Vector3.zero;
		gameObject.transform.localScale = UnityEngine.Vector3.one;
		lienMinh3D = gameObject.GetComponent<ScreenLienMinh3D>();
		gameObject.SetActive(true);
		SetScreen(GAME_SCREEN.ScreenLienMinhMain);
		lienMinh3D.RefreshCongTrinh(true);
	}

	private void OnFinishLoadingScene3D(UnityEngine.Object obj)
	{
		UnityEngine.Object obj2 = UnityEngine.Object.Instantiate(obj);
		GameObject gameObject = (GameObject)((obj2 is GameObject) ? obj2 : null);
		gameObject.transform.parent = instance.ScreenContainer3D;
		gameObject.transform.localPosition = UnityEngine.Vector3.zero;
		gameObject.transform.localScale = UnityEngine.Vector3.one;
		homeCity = gameObject.GetComponent<ScreenMain3D>();
		gameObject.SetActive(false);
	}

	private IEnumerator LoadScene3D()
	{
		EGResourceAsyncLoader l = EGResourceAsyncLoader.Load("gui/screens3d/ScreenMain3D", true, OnFinishLoadingScene3D, OnLoadingScene3D);
		yield return null;
		while (!l.IsDone)
		{
			yield return null;
		}
	}

	public IEnumerator LoadSceneLienMinh3D()
	{
		EGResourceAsyncLoader l = EGResourceAsyncLoader.Load("gui/screens3d/ScreenLienMinh3D", true, OnFinishLoadingSceneLienMinh3D, OnLoadingSceneLienMinh3D);
		yield return null;
		while (!l.IsDone)
		{
			yield return null;
		}
	}

	private void Awake()
	{
		Application.targetFrameRate = 25;
		QualitySettings.vSyncCount = 0;
		Screen.sleepTimeout = -1;
		if (instance != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		instance = this;
		ScreenBase[] componentsInChildren = ScreenContainer.GetComponentsInChildren<ScreenBase>(true);
		ScreenBase[] array = componentsInChildren;
		ScreenBase[] array2 = array;
		foreach (ScreenBase screenBase in array2)
		{
			if (screenBase != null)
			{
				UnityEngine.Object.Destroy(screenBase.gameObject);
			}
		}
		GameObject gameObject = new GameObject();
		gameObject.AddComponent<CoroutineManager>();
		gameObject.name = "CoroutineManager";
		DebugCmd.Ensure();
	}

	private void Start()
	{
		PopupLoading.Create();
	}

	private void Update()
	{
		if (ac != null && PopupNetworkLoading.instance != null && ac.isDone)
		{
			PopupNetworkLoading.DestroyPopup();
		}
		if (!IsReady && GameManager.instance != null)
		{
			StartCoroutine(Init());
			IsReady = true;
		}
		if (Time.realtimeSinceStartup >= mBgNext)
		{
			mBgNext = Time.realtimeSinceStartup + 0.5f;
			AdaptBackgrounds();
			AdaptScreenLayout();
		}
	}

	public void AdaptBackgrounds()
	{
		try
		{
			if (GUI2DRoot == null || Screen.width <= 0 || Screen.height <= 0)
			{
				return;
			}
			float num = GUI2DRoot.manualHeight;
			if (num <= 0f)
			{
				return;
			}
			float x = Mathf.Round(num * (float)Screen.width / (float)Screen.height);
			Transform transform = ((popUpContainer != null) ? popUpContainer.transform : null);
			UIWidget[] componentsInChildren = GUI2DRoot.GetComponentsInChildren<UIWidget>(true);
			UIWidget[] array = componentsInChildren;
			foreach (UIWidget uIWidget in array)
			{
				if (uIWidget == null || uIWidget is UILabel || (!(uIWidget is UISprite) && !(uIWidget is UITexture)))
				{
					continue;
				}
				GameObject gameObject = uIWidget.gameObject;
				if (!gameObject.activeInHierarchy)
				{
					continue;
				}
				Transform transform2 = gameObject.transform;
				if ((transform != null && transform2.IsChildOf(transform)) || InClippedPanel(transform2))
				{
					continue;
				}
				UnityEngine.Vector3 localScale = transform2.localScale;
				if (localScale.x < 560f || localScale.x > 900f || localScale.y < 1100f || localScale.y > 1460f)
				{
					continue;
				}
				UnityEngine.Vector3 localPosition = transform2.localPosition;
				if (!(Mathf.Abs(localPosition.x) > 80f) && !(Mathf.Abs(localPosition.y) > 300f))
				{
					AutoReflectWidth component = gameObject.GetComponent<AutoReflectWidth>();
					if (component != null)
					{
						component.enabled = false;
					}
					transform2.localScale = new UnityEngine.Vector3(x, num, localScale.z);
					UnityEngine.Vector3[] array2 = NGUIMath.CalculateWidgetCorners(uIWidget);
					UnityEngine.Vector3 vector = (array2[0] + array2[1] + array2[2] + array2[3]) * 0.25f;
					UnityEngine.Vector3 vector2 = vector;
					if (cam2D != null)
					{
						UnityEngine.Vector3 vector3 = cam2D.WorldToViewportPoint(vector);
						vector2 = cam2D.ViewportToWorldPoint(new UnityEngine.Vector3(0.5f, 0.5f, vector3.z));
					}
					UnityEngine.Vector3 position = transform2.position;
					transform2.position = new UnityEngine.Vector3(position.x + vector2.x - vector.x, position.y + vector2.y - vector.y, position.z);
				}
			}
		}
		catch (Exception ex)
		{
			EGDebug.LogError("[AdaptBg] " + ex.Message);
		}
	}

	private static bool InClippedPanel(Transform t)
	{
		try
		{
			Transform transform = t;
			while (transform != null)
			{
				UIPanel component = transform.GetComponent<UIPanel>();
				if (component != null && component.clipping != UIDrawCall.Clipping.None)
				{
					return true;
				}
				transform = transform.parent;
			}
		}
		catch (Exception)
		{
		}
		return false;
	}

	public void AdaptScreenLayout()
	{
		try
		{
			if (GUI2DRoot == null || GameFrame == null)
			{
				return;
			}
			float num = GUI2DRoot.manualHeight;
			if (num <= 1136f)
			{
				return;
			}
			float num2 = num - 1136f;
			if (gadgetPanelTop != null && gadgetPanelTop.gameObject.activeInHierarchy)
			{
				Transform transform = gadgetPanelTop.transform;
				SystemMessage componentInChildren = gadgetPanelTop.GetComponentInChildren<SystemMessage>();
				if (componentInChildren != null)
				{
					Transform transform2 = componentInChildren.transform;
					UIAnchor uIAnchor = transform2.GetComponent<UIAnchor>();
					if (uIAnchor == null)
					{
						uIAnchor = transform2.gameObject.AddComponent<UIAnchor>();
					}
					uIAnchor.side = UIAnchor.Side.Top;
					uIAnchor.pixelOffset = Vector2.zero;
					uIAnchor.widgetContainer = GameFrame;
				}
				Transform transform3 = transform.Find("anchorTopLeft");
				if (transform3 != null)
				{
					UIAnchor component = transform3.GetComponent<UIAnchor>();
					if (component != null)
					{
						component.side = UIAnchor.Side.TopLeft;
						component.pixelOffset = new Vector2(0f, -36f);
						component.widgetContainer = GameFrame;
					}
				}
				Transform transform4 = transform.Find("anchorTopRight");
				if (transform4 != null)
				{
					UIAnchor component2 = transform4.GetComponent<UIAnchor>();
					if (component2 != null)
					{
						component2.side = UIAnchor.Side.TopRight;
						component2.pixelOffset = new Vector2(0f, -36f);
						component2.widgetContainer = GameFrame;
					}
				}
			}
			if (CurrentScreen == GAME_SCREEN.ScreenDoiHinh && SCREENS.ContainsKey(GAME_SCREEN.ScreenDoiHinh) && SCREENS[GAME_SCREEN.ScreenDoiHinh] != null)
			{
				ScreenBase screenBase = SCREENS[GAME_SCREEN.ScreenDoiHinh];
				if (screenBase.gameObject.activeInHierarchy)
				{
					Transform[] componentsInChildren = screenBase.GetComponentsInChildren<Transform>(true);
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						if (componentsInChildren[i].name == "ThongTinGroup")
						{
							UIAnchor uIAnchor2 = componentsInChildren[i].GetComponent<UIAnchor>();
							if (uIAnchor2 == null)
							{
								uIAnchor2 = componentsInChildren[i].gameObject.AddComponent<UIAnchor>();
							}
							uIAnchor2.side = UIAnchor.Side.Bottom;
							uIAnchor2.pixelOffset = new Vector2(0f, 280f);
							uIAnchor2.widgetContainer = GameFrame;
							break;
						}
					}
				}
			}
			if (!SCREENS.ContainsKey(CurrentScreen) || !(SCREENS[CurrentScreen] != null))
			{
				return;
			}
			ScreenBase screenBase2 = SCREENS[CurrentScreen];
			if (!screenBase2.gameObject.activeInHierarchy)
			{
				return;
			}
			UIAnchor[] componentsInChildren2 = screenBase2.GetComponentsInChildren<UIAnchor>(true);
			foreach (UIAnchor uIAnchor3 in componentsInChildren2)
			{
				if (uIAnchor3 != null && (uIAnchor3.side == UIAnchor.Side.Top || uIAnchor3.side == UIAnchor.Side.TopLeft || uIAnchor3.side == UIAnchor.Side.TopRight) && uIAnchor3.pixelOffset.y == 0f)
				{
					uIAnchor3.pixelOffset = new Vector2(uIAnchor3.pixelOffset.x, -36f);
				}
			}
			UIPanel[] componentsInChildren3 = screenBase2.GetComponentsInChildren<UIPanel>(true);
			foreach (UIPanel uIPanel in componentsInChildren3)
			{
				if (uIPanel == null || uIPanel.clipping == UIDrawCall.Clipping.None)
				{
					continue;
				}
				int instanceID = uIPanel.GetInstanceID();
				if (!mAdaptedPanels.Contains(instanceID))
				{
					Vector4 clipRange = uIPanel.clipRange;
					if (clipRange.w >= 450f && clipRange.w <= 850f)
					{
						uIPanel.clipRange = new Vector4(clipRange.x, clipRange.y - num2 * 0.5f, clipRange.z, clipRange.w + num2);
						mAdaptedPanels.Add(instanceID);
					}
				}
			}
		}
		catch (Exception ex)
		{
			EGDebug.LogError("[AdaptLayout] " + ex.Message);
		}
	}

	public static void log(string str)
	{
	}

	public ScreenBase GetScreen(GAME_SCREEN screen, bool isForceLoad = true)
	{
		if (!SCREENS.ContainsKey(screen))
		{
			if (!isForceLoad)
			{
				return null;
			}
			LoadScreen(screen);
		}
		return SCREENS[screen];
	}

	public void SetScreen(GAME_SCREEN screen)
	{
		try
		{
			if (!SCREENS.ContainsKey(screen))
			{
				LoadScreen(screen);
			}
			if (!SCREENS.ContainsKey(screen) || SCREENS[screen] == null)
			{
				EGDebug.LogError("[GUIManager] Cannot load screen: " + screen);
			}
			else
			{
				if (CurrentScreen == screen)
				{
					return;
				}
				if (SCREENS.ContainsKey(CurrentScreen) && SCREENS[CurrentScreen] != null)
				{
					ScreenBase screenBase = SCREENS[CurrentScreen];
					if (screen == GAME_SCREEN.ScreenBattle && !GameManager.instance.isStartJoinCT2)
					{
						GameManager.instance.FadeInBattleMusic(0f);
					}
					else if ((screen == GAME_SCREEN.ScreenCT2 && !GameManager.instance.isStartJoinCT2) || screen == GAME_SCREEN.ScreenLanhDiaMap || screen == GAME_SCREEN.ScreenThanhChien)
					{
						GameManager.instance.FadeInDanTruongMusic(0f);
					}
					else if ((CurrentScreen == GAME_SCREEN.ScreenBattle && !GameManager.instance.isStartJoinCT2) || (CurrentScreen == GAME_SCREEN.ScreenCT2 && !GameManager.instance.isStartJoinCT2) || CurrentScreen == GAME_SCREEN.ScreenLanhDiaMap || CurrentScreen == GAME_SCREEN.ScreenThanhChien)
					{
						GameManager.instance.FadeInMainMusic(2f);
					}
					try
					{
						screenBase.OnDeactive();
					}
					catch (Exception ex)
					{
						EGDebug.LogError("[GUIManager] OnDeactive error: " + ((ex != null) ? ex.ToString() : null));
					}
					screenBase.gameObject.SetActive(false);
				}
				if (SCREENS.ContainsKey(screen) && SCREENS[screen] != null)
				{
					SCREENS[screen].gameObject.SetActive(true);
					try
					{
						SCREENS[screen].OnActive();
					}
					catch (Exception ex2)
					{
						EGDebug.LogError("[GUIManager] OnActive error on " + screen.ToString() + ": " + ((ex2 != null) ? ex2.ToString() : null));
					}
					if (SCREENS[screen].child3Dscreen != null)
					{
						cam2D.clearFlags = CameraClearFlags.Depth;
					}
					else
					{
						cam2D.clearFlags = CameraClearFlags.Skybox;
					}
					GameManager.instance.currentScreen = SCREENS[screen];
				}
				if (CurrentScreen != GAME_SCREEN.ScreenBattle)
				{
					LastScreen = CurrentScreen;
				}
				CurrentScreen = screen;
				if (gadgetPanelBottom != null)
				{
					try
					{
						gadgetPanelBottom.SetFocusOnScreen(screen);
					}
					catch (Exception ex3)
					{
						EGDebug.LogError("[GUIManager] SetFocusOnScreen error: " + ((ex3 != null) ? ex3.ToString() : null));
					}
				}
				if (SCREENS.ContainsKey(screen) && SCREENS[screen] != null)
				{
					UIPanel component = SCREENS[screen].transform.parent.GetComponent<UIPanel>();
					if ((bool)component)
					{
						component.Refresh();
					}
				}
				EGDebug.Log("Garbage collect");
				GC.Collect();
				if (screen != GAME_SCREEN.ScreenMain)
				{
					return;
				}
				try
				{
					ScreenBase value = null;
					SCREENS.TryGetValue(GAME_SCREEN.ScreenMain, out value);
					if (value != null)
					{
						(value as ScreenMain).ThoatDanhNienThu();
					}
					return;
				}
				catch
				{
					return;
				}
			}
		}
		catch (Exception ex4)
		{
			EGDebug.LogError("[GUIManager] SetScreen fatal error: " + ((ex4 != null) ? ex4.ToString() : null));
		}
	}

	public static void setScreen(GAME_SCREEN screen)
	{
		if (instance != null)
		{
			instance.SetScreen(screen);
		}
	}

	public static ScreenBase getScreen(GAME_SCREEN screen)
	{
		if (instance != null)
		{
			return instance.GetScreen(screen);
		}
		return null;
	}

	public static void GoBackLastScreen()
	{
		if (instance != null)
		{
			instance.SetScreen(instance.LastScreen);
		}
	}

	public static void ShowGadgets(int gadgetsMask)
	{
		if (!(instance == null))
		{
			if (gadgetsMask < 0)
			{
				instance.gadgetPanelBottom.gameObject.SetActive(false);
				instance.gadgetPanelTop.gameObject.SetActive(false);
			}
			else
			{
				instance.gadgetPanelBottom.gameObject.SetActive((gadgetsMask & 2) != 0);
				instance.gadgetPanelTop.gameObject.SetActive((gadgetsMask & 4) != 0);
			}
		}
	}
}

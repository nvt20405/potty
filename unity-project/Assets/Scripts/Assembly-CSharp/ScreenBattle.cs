using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBattle : ScreenBase
{
	public ChiSoHero m_ChiSoHero;

	public UISprite m_sprHanhTauTeam1Hp;

	public UISprite m_sprHanhTauTeam2Hp;

	public UILabel m_labelTeam1Name;

	public UILabel m_labelTeam2Name;

	public UILabel m_khiThe1;

	public UILabel m_khiThe2;

	public UISprite m_sprApChe1;

	public UISprite m_sprApChe2;

	public UISprite m_sprX2;

	public UISprite m_sprDanhSonNumber;

	private List<GameObject> m_charObjPrefabList = new List<GameObject>();

	private GameObject m_GoGround3D;

	public Dictionary<int, BattleHero> m_dicHeros = new Dictionary<int, BattleHero>();

	public ButtonBase btnSkip;

	public GameObject btnToggle;

	public GAME_SCREEN screenBackAfterBattle = GAME_SCREEN.ScreenMain;

	private float[] m_listTeamHPMax = new float[2];

	public UILabel fpsLabel;

	private string m_mapName;

	private PanCamera m_PanCamera;

	private float TOC_DO_DAU;

	private float TONG_TIME_DAU_NC = 10f;

	private float m_fDauNCTeam1MP;

	private float m_fDauNCTeam1HP;

	private float m_fDauNCTeam2MP;

	private float m_fDauNCTeam2HP;

	private bool m_bDauNCKetThuc;

	private float m_fDauNCBreakTime;

	private float DAUNC_BREAK_TIME_MAX = 0.5f;

	private float HoiThoaiTime = GiangHoCfg.TimeHoiThoaiCuoiTran;

	private GameObject m_GoParDauNC;

	private bool TranDauKetThuc;

	public GameObject Container3D { get; set; }

	public BattleReplay DataReplay { get; set; }

	private BattleReplay.TrangThai.DauNoiCong DauNoiCongData { get; set; }

	public BattleReplay.TeamInfo MyTeamData { get; set; }

	public bool DauNCKetThuc
	{
		get
		{
			return m_bDauNCKetThuc;
		}
		set
		{
			m_bDauNCKetThuc = value;
		}
	}

	private List<GiangHoCfg.HoiThoai> HoiThoaiList { get; set; }

	private int HoiThoaiNumber { get; set; }

	private float HoiThoaiTimeCount { get; set; }

	private int DanhSonTranDauNumber { get; set; }

	public bool HoiThoaiStart { get; set; }

	public BattleHero HoiThoaiHero { get; set; }

	public event Action OnFinishReplay;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(0);
		btnSkip.gameObject.SetActive(false);
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = false;
		}
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.Hide();
		}
		Container3D = GameObject.FindGameObjectWithTag("BattleContainer");
	}

	public override void OnDeactive()
	{
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = true;
		}
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		base.OnDeactive();
		Time.timeScale = 1f;
	}

	private void SetChild3DScreen()
	{
		GameObject gameObject = Utils.instantiatePrefab("GUI/Screens3D/ScreenBattle3D", GUIManager.instance.ScreenContainer3D, false, false);
		gameObject.transform.parent = GUIManager.instance.ScreenContainer3D;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
		gameObject.SetActive(true);
		child3Dscreen = gameObject;
		Camera.main.fieldOfView = 60f;
		Camera.main.nearClipPlane = 1f;
		Camera.main.farClipPlane = 50f;
	}

	protected override void firstTimeInit()
	{
		base.firstTimeInit();
		btnSkip.gameObject.SetActive(false);
		UIEventListener.Get(btnSkip.gameObject).onClick = onClick_btnSkip;
		UIEventListener.Get(btnToggle.gameObject).onClick = onClick_btnToggle;
		UIEventListener.Get(m_sprX2.gameObject).onClick = OnTocDoTranDau;
		SetChild3DScreen();
	}

	private void LoadTocDoTranDau()
	{
		if (PlayerPrefs.HasKey("TocDoTranDau"))
		{
			int num = PlayerPrefs.GetInt("TocDoTranDau");
			if (num == 1)
			{
				Time.timeScale = 1f;
				m_sprX2.spriteName = "X1";
			}
			else
			{
				Time.timeScale = 2f;
				m_sprX2.spriteName = "X2";
			}
		}
		else
		{
			m_sprX2.spriteName = "X1";
			Time.timeScale = 1f;
		}
	}

	public void onClick_btnSkip(GameObject go)
	{
		BattleEnd();
	}

	public void onClick_btnToggle(GameObject go)
	{
		btnSkip.gameObject.SetActive(!NGUITools.GetActive(btnSkip.gameObject));
		if (HoiThoaiList != null && HoiThoaiStart)
		{
			HoiThoaiNumber++;
			if (HoiThoaiNumber < HoiThoaiList.Count)
			{
				bool flag = DisplayHoiThoai(HoiThoaiList[HoiThoaiNumber]);
			}
		}
	}

	private bool DisplayHoiThoai(GiangHoCfg.HoiThoai hoi_thoai)
	{
		HoiThoaiTimeCount = 0f;
		if (HoiThoaiHero != null)
		{
			HoiThoaiHero.TatHoiThoai();
		}
		if (hoi_thoai.NhanVatCode == string.Empty)
		{
			HoiThoaiHero = SearchHero11();
			if (HoiThoaiHero != null)
			{
				HoiThoaiHero.DisplayHoiThoai(hoi_thoai.NoiDung);
				m_PanCamera.Mode = PanCamMode.HoiThoai;
				m_PanCamera.VNhanVatHoiThoai = HoiThoaiHero.transform.localPosition;
				m_PanCamera.SetPanZ(HoiThoaiHero.transform.localPosition);
				return true;
			}
		}
		else
		{
			HoiThoaiHero = SearchHero(hoi_thoai.NhanVatCode);
			if (HoiThoaiHero != null)
			{
				HoiThoaiHero.DisplayHoiThoai(hoi_thoai.NoiDung);
				m_PanCamera.Mode = PanCamMode.HoiThoai;
				m_PanCamera.VNhanVatHoiThoai = HoiThoaiHero.transform.localPosition;
				m_PanCamera.SetPanZ(HoiThoaiHero.transform.localPosition);
				return true;
			}
		}
		m_PanCamera.Mode = PanCamMode.BinhThuong;
		return false;
	}

	private void Start()
	{
		NGUITools.SetActive(m_ChiSoHero.gameObject, false);
	}

	private void Update()
	{
		if (fpsLabel != null)
		{
			fpsLabel.text = string.Format("{0} fps", GUIManager.instance.fpsCounter.FPS);
		}
		if (m_PanCamera != null)
		{
			m_PanCamera.Update();
		}
		OnDauNoiCongUpdate();
		OnDisplayChiSoHero();
		OnUpdateHoiThoai();
		if (TranDauKetThuc)
		{
			TranDauKetThuc = false;
			BattleEnd();
		}
	}

	private void OnUpdateHoiThoai()
	{
		if (HoiThoaiList == null || !HoiThoaiStart)
		{
			return;
		}
		HoiThoaiTimeCount += Time.deltaTime;
		if (HoiThoaiTimeCount >= 3.5f)
		{
			HoiThoaiTimeCount = 0f;
			HoiThoaiNumber++;
			if (HoiThoaiNumber < HoiThoaiList.Count)
			{
				DisplayHoiThoai(HoiThoaiList[HoiThoaiNumber]);
			}
		}
	}

	private void Clear()
	{
		StopAllCoroutines();
		if (m_sprDanhSonNumber != null)
		{
			NGUITools.SetActive(m_sprDanhSonNumber.gameObject, false);
		}
		if (m_ChiSoHero != null)
		{
			NGUITools.SetActive(m_ChiSoHero.gameObject, false);
		}
		foreach (GameObject charObjPrefab in m_charObjPrefabList)
		{
			if (charObjPrefab != null)
			{
				UnityEngine.Object.Destroy(charObjPrefab);
			}
		}
		m_charObjPrefabList.Clear();
		if (m_GoGround3D != null)
		{
			UnityEngine.Object.Destroy(m_GoGround3D);
		}
		m_GoGround3D = null;
		m_dicHeros.Clear();
		if (Container3D != null)
		{
			for (int num = Container3D.transform.childCount - 1; num >= 0; num--)
			{
				Transform child = Container3D.transform.GetChild(num);
				if (child != null)
				{
					UnityEngine.Object.Destroy(child.gameObject);
				}
			}
		}
		HoiThoaiTimeCount = 0f;
		HoiThoaiStart = false;
		m_bDauNCKetThuc = true;
		DauNoiCongData = null;
	}

	public void BattleEnd()
	{
		if (DataReplay != null && DataReplay.SpawnInfoList != null)
		{
			foreach (BattleReplay.SpawnInfo spawnInfo in DataReplay.SpawnInfoList)
			{
				if (spawnInfo.TeamID == 1)
				{
					ScreenVoLamPho.SetVoLamPhoStatus(spawnInfo.Name, 1);
				}
			}
		}
		Time.timeScale = 1f;
		Clear();
		AudioListener.pause = false;
		AudioListener.volume = 1f;
		if (GUIManager.instance != null && GUIManager.instance.cam2D != null)
		{
			AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
			if (component != null)
			{
				component.enabled = true;
			}
		}
		if (GUIManager.instance.LastScreen == GAME_SCREEN.ScreenSelectFirstDeTu)
		{
			ScreenSelectFirstDeTu screenSelectFirstDeTu = (ScreenSelectFirstDeTu)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenSelectFirstDeTu);
			screenSelectFirstDeTu.BackFromBattle();
			GUIManager.setScreen(GAME_SCREEN.ScreenSelectFirstDeTu);
			return;
		}
		GUIManager.setScreen(screenBackAfterBattle);
		if (PopupBattleResult.instance != null)
		{
			PopupBattleResult.instance.gameObject.SetActive(true);
		}
		if (PopupDuocThanhTuu.instance != null)
		{
			PopupDuocThanhTuu.instance.gameObject.SetActive(true);
		}
		if (OnFinishReplay != null)
		{
			OnFinishReplay();
		}
	}

	public BattleHero GetHero(int id)
	{
		BattleHero value;
		m_dicHeros.TryGetValue(id, out value);
		return value;
	}

	private void LoadMap(string name)
	{
		m_mapName = name;
		string path = "Prefabs/" + name;
		UnityEngine.Object obj = Resources.Load(path);
		if (obj == null)
		{
			m_mapName = "BM_City";
			path = "Prefabs/" + m_mapName;
			obj = Resources.Load(path);
		}
		if (obj != null)
		{
			UnityEngine.Object obj2 = UnityEngine.Object.Instantiate(obj);
			m_GoGround3D = (GameObject)((obj2 is GameObject) ? obj2 : null);
			m_GoGround3D.transform.parent = child3Dscreen.transform;
			m_GoGround3D.transform.position = new Vector3(0f, -0.02f, 0f);
			m_GoGround3D.transform.eulerAngles = new Vector3(0f, 0f, 0f);
			Utils.SetLightMaps("Lightmap/" + m_mapName + "/");
		}
	}

	private BattleHero SearchHero(string name)
	{
		foreach (KeyValuePair<int, BattleHero> dicHero in m_dicHeros)
		{
			if (dicHero.Value.SpawnInfo.Name == name)
			{
				return dicHero.Value;
			}
		}
		return null;
	}

	private BattleHero SearchHero11()
	{
		foreach (KeyValuePair<int, BattleHero> dicHero in m_dicHeros)
		{
			if (dicHero.Value.SpawnInfo.TeamID == 0)
			{
				return dicHero.Value;
			}
		}
		return null;
	}

	private double GetTeamHp(int teamId)
	{
		double num = 0.0;
		foreach (KeyValuePair<int, BattleHero> dicHero in m_dicHeros)
		{
			if (dicHero.Value.TeamID == teamId && dicHero.Value.State != HeroState.DEAD)
			{
				num += (double)Math.Max(0f, dicHero.Value.ChiSo.HP);
			}
		}
		return num;
	}

	public bool ChkTeam1IsOwner()
	{
		if (DataReplay == null || DataReplay.Team1Data == null)
		{
			return true;
		}
		return GameManager.instance.m_GameClient.UserInfo.Gamer.ID == DataReplay.Team1Data.GID || DataReplay.Team1Data.GID == 0;
	}

	private void DrawHPBar()
	{
		if (ChkTeam1IsOwner())
		{
			m_sprHanhTauTeam1Hp.fillAmount = (float)(GetTeamHp(0) / (double)m_listTeamHPMax[0]);
			m_sprHanhTauTeam2Hp.fillAmount = (float)(GetTeamHp(1) / (double)m_listTeamHPMax[1]);
		}
		else
		{
			m_sprHanhTauTeam1Hp.fillAmount = (float)(GetTeamHp(1) / (double)m_listTeamHPMax[1]);
			m_sprHanhTauTeam2Hp.fillAmount = (float)(GetTeamHp(0) / (double)m_listTeamHPMax[0]);
		}
	}

	private void SpawnHero(BattleReplay.SpawnInfo e)
	{
		if (e == null || e.TeamID < 0 || e.TeamID >= m_listTeamHPMax.Length)
		{
			return;
		}
		m_listTeamHPMax[e.TeamID] += e.HP;
		string value = e.Name;
		if (!string.IsNullOrEmpty(value))
		{
			UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("Battle/Hero"));
			GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
			m_charObjPrefabList.Add(gameObject);
			gameObject.transform.parent = Container3D.transform;
			gameObject.transform.localPosition = new Vector3(e.PosX, 0f, e.PosZ);
			gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0f, e.ORI, 0f));
			BattleHero component = gameObject.GetComponent<BattleHero>();
			component.SpawnInfo = e;
			component.LoadAvatar();
			m_dicHeros.Add(e.HID, component);
			component.HID = e.HID;
			component.Init();
			gameObject.transform.localScale = Vector3.one * e.TyLeModel;
			if (!e.DHPri && DataReplay.Type != BattleType.DongNhan)
			{
				component.gameObject.SetActive(false);
				StartCoroutine(CoHeroDuBiStart(component));
			}
			component.SetVisible(false);
		}
	}

	private IEnumerator CoHeroDuBiStart(BattleHero com_hero)
	{
		yield return new WaitForEndOfFrame();
		NGUITools.SetActive(com_hero.gameObject, false);
		yield return null;
	}

	private void SetHeroRaTranVisible()
	{
		foreach (KeyValuePair<int, BattleHero> dicHero in m_dicHeros)
		{
			if (dicHero.Value.SpawnInfo.DHPri)
			{
				dicHero.Value.gameObject.SetActive(true);
				dicHero.Value.OnVisible();
				dicHero.Value.SetVisible(true);
			}
			else
			{
				dicHero.Value.SetVisible(false);
				dicHero.Value.gameObject.SetActive(false);
			}
		}
	}

	public void ClearOnFinishReplay()
	{
		if (OnFinishReplay != null)
		{
			Delegate[] invocationList = OnFinishReplay.GetInvocationList();
			Delegate[] array = invocationList;
			foreach (Delegate obj in array)
			{
				OnFinishReplay = (Action)Delegate.Remove(OnFinishReplay, (Action)obj);
			}
		}
	}

	public void ReplayDanhSon(BattleReplay result, int number, string mapName)
	{
		DanhSonTranDauNumber = number;
		Replay(result, mapName);
	}

	public void Replay(BattleReplay result, string mapName = "BM_City", List<GiangHoCfg.HoiThoai> hoi_thoai_list = null, float time = 1000f)
	{
		if (result == null)
		{
			EGDebug.LogError("[ScreenBattle] Replay called with null result");
			GUIManager.setScreen(screenBackAfterBattle);
			return;
		}
		TranDauKetThuc = false;
		Clear();
		LoadTocDoTranDau();
		PreloadVCEff();
		HoiThoaiNumber = 0;
		HoiThoaiTimeCount = 0f;
		HoiThoaiStart = false;
		HoiThoaiHero = null;
		HoiThoaiList = hoi_thoai_list;
		HoiThoaiTime = time;
		ClearOnFinishReplay();
		if (PopupBattleResult.instance != null)
		{
			PopupBattleResult.instance.gameObject.SetActive(false);
		}
		DataReplay = result;
		m_sprHanhTauTeam1Hp.fillAmount = 1f;
		m_sprHanhTauTeam2Hp.fillAmount = 1f;
		m_sprHanhTauTeam1Hp.color = Color.green;
		m_sprHanhTauTeam2Hp.color = Color.red;
		if (result.Team1Data == null || result.Team2Data == null)
		{
			EGDebug.LogError("[ScreenBattle] Team1Data or Team2Data is null");
			GUIManager.setScreen(screenBackAfterBattle);
			return;
		}
		if (result.SpawnInfoList == null)
		{
			EGDebug.LogError("[ScreenBattle] SpawnInfoList is null");
			GUIManager.setScreen(screenBackAfterBattle);
			return;
		}
		if (ChkTeam1IsOwner())
		{
			MyTeamData = DataReplay.Team1Data;
			m_labelTeam1Name.text = DataReplay.Team1Data.Name;
			m_labelTeam2Name.text = DataReplay.Team2Data.Name;
			m_khiThe1.text = DataReplay.Team1Data.KhiThe.ToString();
			m_khiThe2.text = DataReplay.Team2Data.KhiThe.ToString();
			Container3D.transform.localRotation = Quaternion.identity;
		}
		else
		{
			MyTeamData = DataReplay.Team2Data;
			Container3D.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
			m_labelTeam1Name.text = DataReplay.Team2Data.Name;
			m_labelTeam2Name.text = DataReplay.Team1Data.Name;
			m_khiThe1.text = DataReplay.Team2Data.KhiThe.ToString();
			m_khiThe2.text = DataReplay.Team1Data.KhiThe.ToString();
		}
		m_dicHeros.Clear();
		m_listTeamHPMax[0] = 0f;
		m_listTeamHPMax[1] = 0f;
		LoadMap(mapName);
		m_PanCamera = new PanCamera();
		if (result.SpawnInfoList != null)
		{
			foreach (BattleReplay.SpawnInfo spawnInfo in result.SpawnInfoList)
			{
				SpawnHero(spawnInfo);
			}
		}
		if (result.States != null)
		{
			StartCoroutine(CoMainReplay(result.States));
		}
	}

	public bool CheckTrongHoiThoai(string name)
	{
		if (HoiThoaiList != null)
		{
			BattleHero battleHero = SearchHero11();
			if (battleHero != null && battleHero.SpawnInfo.Name == name)
			{
				return true;
			}
			GiangHoCfg.HoiThoai hoiThoai = HoiThoaiList.Find((GiangHoCfg.HoiThoai e) => e.NhanVatCode == name);
			return hoiThoai != null;
		}
		return false;
	}

	private void TatHoiThoai()
	{
		foreach (KeyValuePair<int, BattleHero> dicHero in m_dicHeros)
		{
			dicHero.Value.TatHoiThoai();
		}
		m_PanCamera.Mode = PanCamMode.BinhThuong;
	}

	private IEnumerator CoMainReplay(List<BattleReplay.TrangThai> listRepState)
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForSeconds(0.25f);
		if (DataReplay == null || listRepState == null)
		{
			TranDauKetThuc = true;
			yield break;
		}
		if (DataReplay.Team1Data == null || DataReplay.Team2Data == null)
		{
			EGDebug.LogError("[ScreenBattle] CoMainReplay: Team data is null");
			TranDauKetThuc = true;
			yield break;
		}
		if (DataReplay.Type == BattleType.DanhSon)
		{
			StartCoroutine(PlayDanhSonNumber(DanhSonTranDauNumber));
		}
		if (DataReplay.Team1Data.KhiThe > DataReplay.Team2Data.KhiThe)
		{
			StartCoroutine(CoPlayApche((!ChkTeam1IsOwner()) ? 1 : 0));
		}
		else if (DataReplay.Team1Data.KhiThe < DataReplay.Team2Data.KhiThe)
		{
			StartCoroutine(CoPlayApche(ChkTeam1IsOwner() ? 1 : 0));
		}
		else
		{
			NGUITools.SetActive(m_sprApChe1.gameObject, false);
			NGUITools.SetActive(m_sprApChe2.gameObject, false);
		}
		SetHeroRaTranVisible();
		int StateCnt = listRepState.Count;
		float tick = 0f;
		for (int i = 0; i < StateCnt; i++)
		{
			BattleReplay.TrangThai repState = listRepState[i];
			if (repState == null)
			{
				continue;
			}
			float waitTime = repState.T - tick;
			tick = repState.T;
			if (waitTime > 0f)
			{
				yield return new WaitForSeconds(waitTime);
			}
			if (HoiThoaiList != null && HoiThoaiTime != GiangHoCfg.TimeHoiThoaiCuoiTran)
			{
				HoiThoaiTimeCount += waitTime;
				if (HoiThoaiTimeCount >= HoiThoaiTime && !HoiThoaiStart)
				{
					HoiThoaiStart = true;
					PauseAllExcept(0, true);
					HoiThoaiNumber = 0;
					if (DisplayHoiThoai(HoiThoaiList[HoiThoaiNumber]))
					{
						while (HoiThoaiNumber < HoiThoaiList.Count)
						{
							yield return new WaitForSeconds(0.5f);
						}
					}
					PauseAllExcept(0, false);
					TatHoiThoai();
				}
			}
			if (repState.RHData != null)
			{
				bool r = true;
				BattleHero hero = GetHero(repState.RHData.HID);
				if (hero != null)
				{
					if (HoiThoaiList != null && hero.SpawnInfo != null)
					{
						GiangHoCfg.HoiThoai hoi_thoai = HoiThoaiList.Find((GiangHoCfg.HoiThoai hoiThoai) => hoiThoai.NhanVatCode == hero.SpawnInfo.Name);
						r = hoi_thoai == null;
					}
					if (r)
					{
						hero.Remove();
					}
				}
				if (r)
				{
					m_dicHeros.Remove(repState.RHData.HID);
				}
			}
			if (repState.SDuBi != null)
			{
				BattleHero hero2 = GetHero(repState.SDuBi.HID);
				if (hero2 != null)
				{
					hero2.gameObject.SetActive(true);
					hero2.SetVisible(true);
					hero2.OnVisible();
				}
			}
			if (repState.SD != null)
			{
				int SHID = repState.SD.HID;
				BattleHero hero3 = GetHero(SHID);
				if (hero3 != null)
				{
					if (repState.SD.P)
					{
						Utils.SetGrayLightMaps("gray");
						PauseAllExcept(SHID, true);
						hero3.ReplayState(i, listRepState);
						float pauseTime = ((repState.SD.VCD == null) ? 3f : CommonHelper.GetVCTimeLength(repState.SD.VCD.VC));
						yield return new WaitForSeconds(pauseTime);
						PauseAllExcept(SHID, false);
						Utils.SetLightMaps("Lightmap/" + m_mapName + "/");
					}
					else
					{
						hero3.ReplayState(i, listRepState);
					}
				}
			}
			if (repState.SD == null && repState.HUAList != null && repState.HUAList.Count > 0)
			{
				StartCoroutine(CoReplayEffect(repState.HUAList));
			}
			if (repState.ToaSatThuongData != null)
			{
				BattleHero hero4 = GetHero(repState.ToaSatThuongData.HID);
				if (hero4 != null)
				{
					hero4.PlayToaSatThuong(repState.ToaSatThuongData.VC);
				}
			}
			if (repState._HUThanThu != null)
			{
				BattleHero hero5 = GetHero(repState._HUThanThu.HID);
				if (hero5 != null)
				{
					hero5.PlayThanThuEff(repState._HUThanThu.VC);
				}
			}
			if (repState.HUPD != null)
			{
				BattleHero hero6 = GetHero(repState.HUPD.HID);
				if (hero6 != null)
				{
					hero6.PlayPhanDamage();
				}
			}
			if (repState.HUTrD != null)
			{
				BattleHero hero7 = GetHero(repState.HUTrD.HID);
				if (hero7 != null)
				{
					if (hero7.ChiSo != null)
					{
						hero7.ChiSo.HP = repState.HUTrD.Hp;
					}
					if (!GameManager.instance.Trailer)
					{
						hero7.SetDmgStr(Mathf.RoundToInt(repState.HUTrD.HpLost).ToString(), true);
					}
				}
			}
			if (repState.HSData != null)
			{
				BattleHero hero8 = GetHero(repState.HSData.HID);
				if (hero8 != null && hero8.ChiSo != null)
				{
					hero8.ChiSo.HP = repState.HSData.HP;
				}
			}
			if (repState.HURList != null)
			{
				foreach (BattleReplay.TrangThai.RemoveHU e2 in repState.HURList)
				{
					BattleHero hero9 = GetHero(e2.HID);
					if (hero9 != null)
					{
						hero9.RemoveVCHU(e2.HUID);
					}
				}
			}
			if (repState.TKAS != null)
			{
				BattleHero hero10 = GetHero(repState.TKAS.HID);
				if (hero10 != null && hero10.ChiSo != null)
				{
					hero10.ChiSo.AS = repState.TKAS.AS;
				}
			}
			if (repState.TKCong != null)
			{
				BattleHero hero11 = GetHero(repState.TKCong.HID);
				if (hero11 != null && hero11.ChiSo != null)
				{
					hero11.ChiSo.Cong = repState.TKCong.Cong;
				}
			}
			if (repState.TKHP != null)
			{
				BattleHero hero12 = GetHero(repState.TKHP.HID);
				if (hero12 != null && hero12.ChiSo != null)
				{
					hero12.ChiSo.HP = repState.TKHP.HP;
				}
			}
			if (repState.TKHPMax != null)
			{
				BattleHero hero13 = GetHero(repState.TKHPMax.HID);
				if (hero13 != null && hero13.ChiSo != null)
				{
					hero13.ChiSo.HPMax = repState.TKHPMax.HPMax;
				}
			}
			if (repState.TKMP != null)
			{
				BattleHero hero14 = GetHero(repState.TKMP.HID);
				if (hero14 != null && hero14.ChiSo != null)
				{
					hero14.ChiSo.MP = repState.TKMP.MP;
				}
			}
			if (repState.TKMPMax != null)
			{
				BattleHero hero15 = GetHero(repState.TKMPMax.HID);
				if (hero15 != null && hero15.ChiSo != null)
				{
					hero15.ChiSo.MPMax = repState.TKMPMax.MPMax;
				}
			}
			if (repState.TKMS != null)
			{
				BattleHero hero16 = GetHero(repState.TKMS.HID);
				if (hero16 != null && hero16.ChiSo != null)
				{
					hero16.ChiSo.MS = repState.TKMS.MS;
				}
			}
			if (repState.TKRegHP != null)
			{
				BattleHero hero17 = GetHero(repState.TKRegHP.HID);
				if (hero17 != null && hero17.ChiSo != null)
				{
					hero17.ChiSo.RegHP = repState.TKRegHP.RegHp;
				}
			}
			if (repState.TKRegMP != null)
			{
				BattleHero hero18 = GetHero(repState.TKRegMP.HID);
				if (hero18 != null && hero18.ChiSo != null)
				{
					hero18.ChiSo.RegMP = repState.TKRegMP.RegMp;
				}
			}
			if (repState.TKThu != null)
			{
				BattleHero hero19 = GetHero(repState.TKThu.HID);
				if (hero19 != null && hero19.ChiSo != null)
				{
					hero19.ChiSo.Thu = repState.TKThu.Thu;
				}
			}
			if (repState.DauNoiCongData != null)
			{
				m_bDauNCKetThuc = false;
				if (m_PanCamera != null)
				{
					m_PanCamera.DauNoiCong = true;
				}
				DauNoiCongInit(repState.DauNoiCongData);
				DauNoiCongStart(repState.DauNoiCongData);
				while (!m_bDauNCKetThuc)
				{
					yield return new WaitForSeconds(0.2f);
				}
				if (m_GoParDauNC != null)
				{
					UnityEngine.Object.Destroy(m_GoParDauNC);
				}
				m_GoParDauNC = null;
				UnityEngine.Object obj = Resources.Load("FX/Prefabs/MIS_DAU_NOI_CONG_END", typeof(GameObject));
				GameObject goDauNC = (GameObject)((obj is GameObject) ? obj : null);
				UnityEngine.Object obj2 = UnityEngine.Object.Instantiate(goDauNC);
				m_GoParDauNC = (GameObject)((obj2 is GameObject) ? obj2 : null);
				if (m_GoParDauNC != null)
				{
					m_GoParDauNC.transform.parent = m_GoGround3D.transform;
					m_GoParDauNC.transform.localRotation = Quaternion.identity;
					m_GoParDauNC.transform.localPosition = Vector3.zero;
					m_GoParDauNC.transform.localScale = Vector3.one;
				}
				yield return new WaitForSeconds(1f);
			}
			else
			{
				DrawHPBar();
			}
		}
		if (DataReplay.Team1Data.Winner && HoiThoaiList != null && HoiThoaiTime == GiangHoCfg.TimeHoiThoaiCuoiTran)
		{
			SetAllToIdle();
			HoiThoaiStart = true;
			m_PanCamera.DauNoiCong = false;
			HoiThoaiNumber = 0;
			if (DisplayHoiThoai(HoiThoaiList[HoiThoaiNumber]))
			{
				while (HoiThoaiNumber < HoiThoaiList.Count)
				{
					yield return new WaitForSeconds(0.5f);
				}
			}
		}
		else
		{
			if (DataReplay.Type == BattleType.DongNhan)
			{
				yield return new WaitForSeconds(2.5f);
			}
			SetAllToIdle();
			yield return new WaitForSeconds(0.5f);
		}
		if (DataReplay.Type != BattleType.DongNhan)
		{
			yield return new WaitForSeconds(0.2f);
		}
		TranDauKetThuc = true;
		yield return null;
	}

	private IEnumerator CoReplayEffect(List<BattleReplay.TrangThai.HieuUng> listEffectData)
	{
		if (listEffectData != null)
		{
			float tick = 0f;
			foreach (BattleReplay.TrangThai.HieuUng e in listEffectData)
			{
				float waitTime = e.LT - tick;
				tick = e.LT;
				yield return new WaitForSeconds(waitTime);
				BattleHero hr = GetHero(e.HID);
				if (hr != null)
				{
					hr.ReplayEff(e);
				}
			}
		}
		yield return null;
	}

	private void PauseAllExcept(int ID, bool pause)
	{
		foreach (KeyValuePair<int, BattleHero> dicHero in m_dicHeros)
		{
			if (!pause)
			{
				dicHero.Value.SetPause(false);
			}
			else if (dicHero.Key != ID)
			{
				dicHero.Value.SetPause(pause);
			}
		}
	}

	private IEnumerator CoPhatLai()
	{
		PopupNetworkLoading.Create(string.Empty, 1f, false);
		yield return new WaitForSeconds(0.3f);
		Replay(DataReplay, m_mapName);
		yield return null;
	}

	public void PhatLai()
	{
		StartCoroutine(CoPhatLai());
	}

	private IEnumerator TestTime()
	{
		for (int i = 0; i < 40; i++)
		{
			yield return new WaitForSeconds(0.25f);
		}
		yield return null;
	}

	private void DauNoiCongInit(BattleReplay.TrangThai.DauNoiCong DauNoiCongData)
	{
		if (DauNoiCongData == null || DauNoiCongData.Team1Data == null || DauNoiCongData.Team2Data == null || DauNoiCongData.Team1Data.HeroDataList == null || DauNoiCongData.Team1Data.HeroDataList.Count == 0 || DauNoiCongData.Team2Data.HeroDataList == null || DauNoiCongData.Team2Data.HeroDataList.Count == 0)
		{
			return;
		}
		UnityEngine.Object obj = Resources.Load("FX/Prefabs/MIS_DAU_NOI_CONG", typeof(GameObject));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		UnityEngine.Object obj2 = UnityEngine.Object.Instantiate(gameObject);
		m_GoParDauNC = (GameObject)((obj2 is GameObject) ? obj2 : null);
		if (m_GoParDauNC != null)
		{
			m_GoParDauNC.transform.parent = m_GoGround3D.transform;
			m_GoParDauNC.transform.localRotation = Quaternion.identity;
			m_GoParDauNC.transform.localPosition = Vector3.zero;
			m_GoParDauNC.transform.localScale = Vector3.one;
		}
		float num = 3.2f;
		Vector3 vector = default(Vector3);
		vector = new Vector3(1f, 0f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(-1f, 0f, 0f);
		float num2 = 1f;
		int num3 = 1;
		float num4 = 70f / (float)DauNoiCongData.Team1Data.HeroDataList.Count;
		Vector3 vector3 = default(Vector3);
		foreach (BattleReplay.TrangThai.DauNoiCong.HeroData heroData in DauNoiCongData.Team1Data.HeroDataList)
		{
			BattleHero hero = GetHero(heroData.HID);
			if (hero != null)
			{
				hero.ChiSo.HP = heroData.HP;
				Vector3 vector4 = Quaternion.AngleAxis((float)(num3 - 1) * num4 * (float)((num3 % 2 == 0) ? 1 : (-1)), Vector3.up) * vector;
				vector3 = new Vector3(-1f, 0f, 0f);
				hero.transform.localPosition = vector3 + vector4 * num;
				hero.transform.localRotation = Quaternion.Euler(new Vector3(0f, 270f, 0f));
				hero.DauNoiCong();
				num3++;
			}
		}
		num3 = 1;
		float num5 = 70f / (float)DauNoiCongData.Team2Data.HeroDataList.Count;
		Vector3 vector5 = default(Vector3);
		foreach (BattleReplay.TrangThai.DauNoiCong.HeroData heroData2 in DauNoiCongData.Team2Data.HeroDataList)
		{
			BattleHero hero2 = GetHero(heroData2.HID);
			if (hero2 != null)
			{
				hero2.ChiSo.HP = heroData2.HP;
				Vector3 vector6 = Quaternion.AngleAxis((float)(num3 - 1) * num5 * (float)((num3 % 2 == 0) ? 1 : (-1)), Vector3.up) * vector2;
				vector5 = new Vector3(1f, 0f, 0f);
				hero2.transform.localPosition = Vector3.zero + vector6 * num;
				hero2.transform.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
				hero2.DauNoiCong();
				num3++;
			}
		}
	}

	private void DauNoiCongStart(BattleReplay.TrangThai.DauNoiCong noi_cong_data)
	{
		if (noi_cong_data != null && noi_cong_data.Team1Data != null && noi_cong_data.Team2Data != null)
		{
			DauNoiCongData = noi_cong_data;
			m_fDauNCTeam1MP = DauNoiCongData.Team1Data.TongMPConLai;
			m_fDauNCTeam1HP = DauNoiCongData.Team1Data.TongHPConLai;
			m_fDauNCTeam2MP = DauNoiCongData.Team2Data.TongMPConLai;
			m_fDauNCTeam2HP = DauNoiCongData.Team2Data.TongHPConLai;
			TOC_DO_DAU = Mathf.Min(m_fDauNCTeam1MP + m_fDauNCTeam1HP, m_fDauNCTeam2MP + m_fDauNCTeam2HP) / TONG_TIME_DAU_NC;
			m_bDauNCKetThuc = false;
			m_sprHanhTauTeam1Hp.color = Color.blue;
			m_sprHanhTauTeam2Hp.color = Color.blue;
		}
	}

	private void OnDauNoiCongUpdate()
	{
		if (DauNoiCongData == null || DauNoiCongData.Team1Data == null || DauNoiCongData.Team2Data == null || m_bDauNCKetThuc)
		{
			return;
		}
		if (m_fDauNCBreakTime > 0f)
		{
			m_fDauNCBreakTime -= Time.deltaTime;
			return;
		}
		bool flag = false;
		bool flag2 = false;
		float num = TOC_DO_DAU * Time.deltaTime;
		if (m_fDauNCTeam1MP > 0f)
		{
			if (m_fDauNCTeam1MP >= num)
			{
				m_fDauNCTeam1MP -= num;
			}
			else
			{
				m_fDauNCTeam1MP = 0f;
				m_fDauNCTeam1HP -= num - m_fDauNCTeam1MP;
				DauNCTruMauHero(0, num - m_fDauNCTeam1MP);
				if (m_fDauNCTeam2MP > 0f)
				{
					m_fDauNCBreakTime = DAUNC_BREAK_TIME_MAX;
				}
			}
			m_sprHanhTauTeam1Hp.fillAmount = m_fDauNCTeam1MP / DauNoiCongData.Team1Data.TongMPConLai;
		}
		else
		{
			m_sprHanhTauTeam1Hp.color = Color.green;
			if (m_fDauNCTeam1HP > 0f)
			{
				if (m_fDauNCTeam1HP >= num)
				{
					m_fDauNCTeam1HP -= num;
				}
				else
				{
					flag = true;
				}
				m_sprHanhTauTeam1Hp.fillAmount = m_fDauNCTeam1HP / DauNoiCongData.Team1Data.TongHPConLai;
				DauNCTruMauHero(0, num);
			}
		}
		if (m_fDauNCTeam2MP > 0f)
		{
			if (m_fDauNCTeam2MP >= num)
			{
				m_fDauNCTeam2MP -= num;
			}
			else
			{
				m_fDauNCTeam2MP = 0f;
				m_fDauNCTeam2HP -= num - m_fDauNCTeam2MP;
				DauNCTruMauHero(1, num - m_fDauNCTeam2MP);
				if (m_fDauNCTeam1MP > 0f)
				{
					m_fDauNCBreakTime = DAUNC_BREAK_TIME_MAX;
				}
			}
			m_sprHanhTauTeam2Hp.fillAmount = m_fDauNCTeam2MP / DauNoiCongData.Team2Data.TongMPConLai;
		}
		else
		{
			m_sprHanhTauTeam2Hp.color = Color.red;
			if (m_fDauNCTeam2HP > 0f)
			{
				if (m_fDauNCTeam2HP >= num)
				{
					m_fDauNCTeam2HP -= num;
				}
				else
				{
					flag2 = true;
				}
				m_sprHanhTauTeam2Hp.fillAmount = m_fDauNCTeam2HP / DauNoiCongData.Team2Data.TongHPConLai;
				DauNCTruMauHero(1, num);
			}
		}
		if (flag && !flag2)
		{
			m_sprHanhTauTeam1Hp.fillAmount = 0f;
			m_bDauNCKetThuc = true;
		}
		else if (flag2 && !flag)
		{
			m_sprHanhTauTeam2Hp.fillAmount = 0f;
			m_bDauNCKetThuc = true;
		}
		else if (flag & flag2)
		{
			if (m_fDauNCTeam1HP > m_fDauNCTeam2HP)
			{
				m_sprHanhTauTeam2Hp.fillAmount = 0f;
			}
			else
			{
				m_sprHanhTauTeam1Hp.fillAmount = 0f;
			}
			m_bDauNCKetThuc = true;
		}
	}

	private void DauNCTruMauHero(int team, float hp)
	{
		if (DauNoiCongData == null || DauNoiCongData.Team1Data == null || DauNoiCongData.Team2Data == null)
		{
			return;
		}
		List<BattleReplay.TrangThai.DauNoiCong.HeroData> list = ((team != 0) ? DauNoiCongData.Team2Data.HeroDataList : DauNoiCongData.Team1Data.HeroDataList);
		if (list == null)
		{
			return;
		}
		for (int i = 0; i < list.Count; i++)
		{
			BattleReplay.TrangThai.DauNoiCong.HeroData heroData = list[i];
			BattleHero hero = GetHero(heroData.HID);
			if (hero != null && hero.State != HeroState.DEAD)
			{
				float hp2 = hero.DauNCDecHP(hp);
				if (hero.State == HeroState.DEAD)
				{
					DauNCTruMauHero(team, hp2);
				}
				break;
			}
		}
	}

	private void FixedUpdate()
	{
		if (Camera.main == null || !Input.GetMouseButtonDown(0))
		{
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(ray, out hitInfo, 100f))
		{
			BattleHero component = hitInfo.collider.GetComponent<BattleHero>();
			if (component != null)
			{
				NGUITools.SetActive(m_ChiSoHero.gameObject, true);
				MouseSelect(component);
			}
			else
			{
				NGUITools.SetActive(m_ChiSoHero.gameObject, false);
			}
		}
		else
		{
			NGUITools.SetActive(m_ChiSoHero.gameObject, false);
		}
	}

	private void MouseSelect(BattleHero hero)
	{
		foreach (KeyValuePair<int, BattleHero> dicHero in m_dicHeros)
		{
			if (hero != dicHero.Value)
			{
				dicHero.Value.Selected = false;
			}
			else
			{
				hero.Selected = true;
			}
		}
	}

	private void OnDisplayChiSoHero()
	{
		foreach (KeyValuePair<int, BattleHero> dicHero in m_dicHeros)
		{
			if (dicHero.Value.Selected)
			{
				if (dicHero.Value.State == HeroState.DEAD || !dicHero.Value.SpawnInfo.ChoXemChiSo)
				{
					NGUITools.SetActive(m_ChiSoHero.gameObject, false);
				}
				else
				{
					dicHero.Value.DisplayChiSo();
				}
			}
		}
	}

	public void SetAllToIdle()
	{
		foreach (KeyValuePair<int, BattleHero> dicHero in m_dicHeros)
		{
			if (dicHero.Value.State != HeroState.DEAD)
			{
				dicHero.Value.StopAllCoroutines();
				dicHero.Value.StartAnim("idle");
			}
		}
	}

	private IEnumerator CoPlayApche(int team)
	{
		UISprite sprApChe;
		if (team == 0)
		{
			NGUITools.SetActive(m_sprApChe1.gameObject, true);
			NGUITools.SetActive(m_sprApChe2.gameObject, false);
			sprApChe = m_sprApChe1;
		}
		else
		{
			NGUITools.SetActive(m_sprApChe1.gameObject, false);
			NGUITools.SetActive(m_sprApChe2.gameObject, true);
			sprApChe = m_sprApChe2;
		}
		Vector3 pixelPerScale = new Vector3(60f, 94f, 1f);
		sprApChe.transform.localScale = new Vector3(3f * pixelPerScale.x, 3f * pixelPerScale.y, 1f);
		TweenScale.Begin(sprApChe.gameObject, 0.5f, pixelPerScale);
		yield return null;
	}

	public void OnTocDoTranDau(GameObject go)
	{
		if (PlayerPrefs.HasKey("TocDoTranDau"))
		{
			int num = PlayerPrefs.GetInt("TocDoTranDau");
			if (num == 1)
			{
				PlayerPrefs.SetInt("TocDoTranDau", 2);
				Time.timeScale = 2f;
				m_sprX2.spriteName = "X2";
			}
			else
			{
				PlayerPrefs.SetInt("TocDoTranDau", 1);
				Time.timeScale = 1f;
				m_sprX2.spriteName = "X1";
			}
		}
		else
		{
			PlayerPrefs.SetInt("TocDoTranDau", 1);
			Time.timeScale = 1f;
			m_sprX2.spriteName = "X1";
		}
	}

	private IEnumerator PlayDanhSonNumber(int number)
	{
		for (int i = 1; i <= 3; i++)
		{
			m_sprDanhSonNumber.spriteName = string.Format("tang{0}", i);
			m_sprDanhSonNumber.alpha = 1f;
			m_sprDanhSonNumber.MakePixelPerfect();
		}
		NGUITools.SetActive(m_sprDanhSonNumber.gameObject, true);
		m_sprDanhSonNumber.spriteName = string.Format("tang{0}", number);
		Vector3 pixelScale = new Vector3(m_sprDanhSonNumber.transform.localScale.x, m_sprDanhSonNumber.transform.localScale.y, m_sprDanhSonNumber.transform.localScale.z);
		m_sprDanhSonNumber.transform.localScale *= 2f;
		TweenScale.Begin(m_sprDanhSonNumber.gameObject, 0.5f, pixelScale);
		yield return new WaitForSeconds(0.5f);
		TweenAlpha.Begin(m_sprDanhSonNumber.gameObject, 2f, 0f);
		yield return new WaitForSeconds(2f);
		NGUITools.SetActive(m_sprDanhSonNumber.gameObject, false);
		yield return null;
	}

	private void PreloadVCEff()
	{
		Resources.Load("FX/Prefabs/EFF_CHOANG");
		Resources.Load("FX/Prefabs/EFF_DINH_THAN");
		Resources.Load("FX/Prefabs/EFF_XUAT_HUYET");
		Resources.Load("FX/Prefabs/EFF_TRUNG_DOC");
		Resources.Load("FX/Prefabs/EFF_PHONG_CHIEU");
		Resources.Load("FX/Prefabs/EFF_SUY_NHUOC");
		Resources.Load("FX/Prefabs/EFF_TRIET_PHONG");
		Resources.Load("FX/Prefabs/EFF_TAU_HOA");
		Resources.Load("FX/Prefabs/EFF_PHONG_THAN_PHAP");
		Resources.Load("FX/Prefabs/EFF_PHE_THU_PHAP");
		Resources.Load("FX/Prefabs/EFF_PHE_BO_PHAP");
		Resources.Load("FX/Prefabs/EFF_THU_HUT");
		Resources.Load("FX/Prefabs/EFF_TANG_CONG");
		Resources.Load("FX/Prefabs/EFF_NE");
		Resources.Load("FX/Prefabs/EFF_BAO_KICH");
	}
}

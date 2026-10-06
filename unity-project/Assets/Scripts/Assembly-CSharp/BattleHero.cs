using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BattleHero : ClipEntity
{
	private StreamWriter _SW;

	public string CurAnimNamePlay = string.Empty;

	private ThongkeChiSo _ThongKeChiSo = new ThongkeChiSo();

	private GameObject HeroGUIPanelGo;

	public Dictionary<string, float> AnimLengthDic = new Dictionary<string, float>();

	private List<ClipAction> _ClipActionList = new List<ClipAction>();

	private List<UISprite> m_debuffIcons = new List<UISprite>();

	private HeroState m_prevstate;

	private HeroState m_state;

	public bool m_bDauNoiLucStart;

	private BattleReplay.TrangThai m_replayParameter;

	public GameObject GUIPivot;

	public HeroGUIPanel GUIPanel;

	public GameObject m_avatar;

	protected AnimationController m_animationController;

	private NhanVatCfg _cfg;

	private int m_iAttackComboSeq;

	private GameObject m_goVk;

	private WeaponTrail m_weaponTrail;

	private AudioSource m_AudioSource;

	private BattleReplay.SpawnInfo m_spawnInfo;

	private List<RepVcEffect> m_listVCEffect = new List<RepVcEffect>();

	private static HashSet<string> s_registeredAnimEvents = new HashSet<string>();

	public int HID { get; set; }

	public int TeamID { get; set; }

	public int TID { get; set; }

	public int IndexAnimDauNC { get; set; }

	public string TAnimDauNC { get; set; }

	public ThongkeChiSo ChiSo
	{
		get
		{
			return _ThongKeChiSo;
		}
		set
		{
			_ThongKeChiSo = value;
		}
	}

	public GameObject GoVuKhiAo { get; set; }

	public List<ClipAction> ClipActionList
	{
		get
		{
			return _ClipActionList;
		}
		set
		{
			_ClipActionList = value;
		}
	}

	public HeroState State
	{
		get
		{
			return m_state;
		}
		set
		{
			m_state = value;
		}
	}

	public bool Selected { get; set; }

	private string SkillFileStr { get; set; }

	public BattleReplay.SpawnInfo SpawnInfo
	{
		get
		{
			return m_spawnInfo;
		}
		set
		{
			m_spawnInfo = value;
			TeamID = m_spawnInfo.TeamID;
			ChiSo.HPMax = m_spawnInfo.HPMax;
			ChiSo.HP = m_spawnInfo.HP;
			ChiSo.MPMax = m_spawnInfo.MPMax;
			ChiSo.MP = m_spawnInfo.MP;
			ChiSo.RegHP = m_spawnInfo.HPReg;
			ChiSo.RegMP = m_spawnInfo.MPReg;
			ChiSo.AS = m_spawnInfo.AS;
			ChiSo.MS = m_spawnInfo.MS;
			ChiSo.Cong = m_spawnInfo.Cong;
			ChiSo.Thu = m_spawnInfo.Thu;
		}
	}

	private void DestroyClipParticle(ClipAction clip)
	{
		if (clip.GoParticle != null)
		{
			if (clip.GoParticle.GetComponent<ParticleSystem>() != null)
			{
				clip.GoParticle.GetComponent<ParticleSystem>().Stop();
			}
			UnityEngine.Object.Destroy(clip.GoParticle);
			clip.GoParticle = null;
		}
	}

	private void OnTickClipAction()
	{
		int count = ClipActionList.Count;
		for (int num = count - 1; num >= 0; num--)
		{
			ClipAction clipAction = ClipActionList[num];
			if (!base.Pause || !clipAction.PauseAsFrozen)
			{
				if (clipAction.MarkAsRemove)
				{
					DestroyClipParticle(clipAction);
					ClipActionList.RemoveAt(num);
				}
				else if (!clipAction.Started)
				{
					clipAction.TimeDelay -= Time.deltaTime;
					if (clipAction.TimeDelay <= 0f)
					{
						PlayClip(clipAction);
					}
				}
				else
				{
					clipAction.Time -= Time.deltaTime;
					if (clipAction.Time < 0f)
					{
						if (clipAction.GoTanAnh != null)
						{
							UnityEngine.Object.Destroy(clipAction.GoTanAnh);
							clipAction.GoTanAnh = null;
						}
						DestroyClipParticle(clipAction);
						if (clipAction.Name == "play_remove")
						{
							UnityEngine.Object.Destroy(base.gameObject);
						}
						if (clipAction.Name == "play_anim")
						{
							if (clipAction.ParamStr1 == "change")
							{
								StartAnim("idle");
							}
							if (!(clipAction.ParamStr1 == "attack_0") && !(clipAction.ParamStr1 == "attack_1") && clipAction.ParamStr1 == "attack_2")
							{
							}
						}
						ClipActionList.RemoveAt(num);
					}
					else if (clipAction.Name == "play_projectile")
					{
						BattleHero battleHero = SearchHero(clipAction.ParamInt1);
						if (battleHero != null && clipAction.GoParticle != null)
						{
							Vector3 position = battleHero.transform.position;
							position.y += 1.2f;
							Vector3 vector = position - clipAction.GoParticle.transform.position;
							float num2 = vector.magnitude / clipAction.Time;
							if (vector.magnitude <= num2 * Time.deltaTime)
							{
								clipAction.Time = 0f;
							}
							else
							{
								clipAction.GoParticle.transform.position += vector.normalized * num2 * Time.deltaTime;
							}
						}
					}
					else if (clipAction.Name == "play_particle")
					{
						clipAction.TimeDetach -= Time.deltaTime;
						if (clipAction.TimeDetach <= 0f && clipAction.GoParticle != null)
						{
							clipAction.GoParticle.transform.parent = null;
						}
					}
					else if (!(clipAction.Name == "set_hp") && clipAction.Name == "play_remove")
					{
					}
				}
			}
		}
	}

	private void ClearActionList()
	{
		int count = ClipActionList.Count;
		for (int num = count - 1; num >= 0; num--)
		{
			ClipAction clipAction = ClipActionList[num];
			DestroyClipParticle(clipAction);
			if (clipAction.GoTanAnh != null)
			{
				UnityEngine.Object.Destroy(clipAction.GoTanAnh);
				clipAction.GoTanAnh = null;
			}
		}
		ClipActionList.Clear();
	}

	private void PlayClip(ClipAction clip)
	{
		if (clip.Started)
		{
			return;
		}
		clip.Started = true;
		if (clip.Name == "play_anim")
		{
			StartAnim(clip.ParamStr1, clip.ParamBoolean1, clip.ParamFloat1);
		}
		else if (clip.Name == "play_particle")
		{
			UnityEngine.Object obj = Resources.Load(clip.ParamStr1);
			if (obj == null)
			{
				string paramStr = clip.ParamStr1;
				int num = paramStr.LastIndexOf('/');
				if (num >= 0)
				{
					string text = paramStr.Substring(0, num + 1);
					string text2 = paramStr.Substring(num + 1);
					string baseVCName = GetBaseVCName(text2);
					if (baseVCName != text2)
					{
						obj = Resources.Load(text + baseVCName);
					}
				}
			}
			if (obj == null && clip.ParamStr1.Contains("_IMPACT"))
			{
				obj = Resources.Load("FX/Prefabs/VC_IMPACT");
			}
			if (!(obj != null))
			{
				return;
			}
			UnityEngine.Object obj2 = UnityEngine.Object.Instantiate(obj);
			clip.GoParticle = (GameObject)((obj2 is GameObject) ? obj2 : null);
			if (!(clip.GoParticle != null))
			{
				return;
			}
			int layer = base.gameObject.layer;
			clip.GoParticle.layer = layer;
			Transform[] componentsInChildren = clip.GoParticle.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = layer;
			}
			if (clip.Attached)
			{
				clip.GoParticle.transform.parent = base.transform;
				clip.GoParticle.transform.localPosition = clip.ParamVector;
				clip.GoParticle.transform.localScale = Vector3.one;
				clip.GoParticle.transform.localRotation = Quaternion.identity;
			}
			else
			{
				clip.GoParticle.transform.parent = base.transform.parent;
				clip.GoParticle.transform.position = clip.ParamVector;
				clip.GoParticle.transform.localScale = Vector3.one;
				if (clip.LookAtPosition != Vector3.zero)
				{
					clip.GoParticle.transform.LookAt(clip.LookAtPosition);
				}
			}
			ParticleSystem[] componentsInChildren2 = clip.GoParticle.GetComponentsInChildren<ParticleSystem>(true);
			for (int j = 0; j < componentsInChildren2.Length; j++)
			{
				componentsInChildren2[j].Play();
			}
		}
		else if (clip.Name == "play_projectile")
		{
			UnityEngine.Object obj3 = Resources.Load(clip.ParamStr1);
			if (obj3 == null)
			{
				string paramStr2 = clip.ParamStr1;
				int num2 = paramStr2.LastIndexOf('/');
				if (num2 >= 0)
				{
					string text3 = paramStr2.Substring(0, num2 + 1);
					string text4 = paramStr2.Substring(num2 + 1);
					string baseVCName2 = GetBaseVCName(text4);
					if (baseVCName2 != text4)
					{
						obj3 = Resources.Load(text3 + baseVCName2);
					}
				}
			}
			if (!(obj3 != null))
			{
				return;
			}
			UnityEngine.Object obj4 = UnityEngine.Object.Instantiate(obj3);
			clip.GoParticle = (GameObject)((obj4 is GameObject) ? obj4 : null);
			if (clip.GoParticle != null)
			{
				int layer2 = base.gameObject.layer;
				clip.GoParticle.layer = layer2;
				Transform[] componentsInChildren3 = clip.GoParticle.GetComponentsInChildren<Transform>(true);
				for (int k = 0; k < componentsInChildren3.Length; k++)
				{
					componentsInChildren3[k].gameObject.layer = layer2;
				}
				clip.GoParticle.transform.parent = base.transform;
				clip.GoParticle.transform.localPosition = Vector3.zero;
				clip.GoParticle.transform.localScale = Vector3.one;
				clip.GoParticle.transform.localRotation = Quaternion.identity;
				clip.GoParticle.transform.position = clip.ParamVector;
				BattleHero battleHero = SearchHero(clip.ParamInt1);
				if (battleHero != null)
				{
					clip.GoParticle.transform.LookAt(battleHero.transform.position);
				}
				ParticleSystem[] componentsInChildren4 = clip.GoParticle.GetComponentsInChildren<ParticleSystem>(true);
				for (int l = 0; l < componentsInChildren4.Length; l++)
				{
					componentsInChildren4[l].Play();
				}
			}
		}
		else if (clip.Name == "play_vc_vk_ao")
		{
			CfgVoCong value;
			ConfigManager.instance.m_dicVCs.TryGetValue(clip.ParamStr1, out value);
			if (value == null || string.IsNullOrEmpty(value.YeuCauVuKhi))
			{
				return;
			}
			UnityEngine.Object obj5 = Resources.Load("FX/Prefabs/" + value.YeuCauVuKhi, typeof(GameObject));
			GameObject gameObject = (GameObject)((obj5 is GameObject) ? obj5 : null);
			if (gameObject != null)
			{
				UnityEngine.Object obj6 = UnityEngine.Object.Instantiate(gameObject);
				clip.GoParticle = (GameObject)((obj6 is GameObject) ? obj6 : null);
				if (clip.GoParticle != null && m_avatar != null)
				{
					Transform parent = m_avatar.transform.Find("jnt_root/jnt_weapon_R").transform;
					clip.GoParticle.transform.parent = parent;
					clip.GoParticle.transform.localRotation = Quaternion.identity;
					clip.GoParticle.transform.localPosition = Vector3.zero;
				}
			}
		}
		else if (clip.Name == "trail_start")
		{
			if (m_weaponTrail != null)
			{
				m_weaponTrail.StartTrail(clip.ParamFloat1, clip.ParamFloat2);
			}
		}
		else if (clip.Name == "trail_fade_out")
		{
			if (m_weaponTrail != null)
			{
				m_weaponTrail.FadeOut(clip.ParamFloat1);
			}
		}
		else if (clip.Name == "play_vc_name")
		{
			PlayVCName(clip.ParamStr1);
		}
		else if (!(clip.Name == "set_hp"))
		{
			if (clip.Name == "set_vc_effect")
			{
				string empty = string.Empty;
				string text5 = clip.VCHUName.ToString();
				empty = "FX/Prefabs/" + text5;
				clip.GoParticle = base.PlayParticle(empty);
			}
			else if (clip.Name == "play_sound")
			{
				string path = "FX/Sound/" + clip.ParamStr1;
				AudioSource audioSource = m_AudioSource;
				UnityEngine.Object obj7 = Resources.Load(path);
				audioSource.clip = (AudioClip)((obj7 is AudioClip) ? obj7 : null);
				m_AudioSource.volume = 1f;
				m_AudioSource.panLevel = 0f;
				m_AudioSource.loop = false;
				m_AudioSource.Play();
			}
			else if (clip.Name == "play_tan_anh")
			{
				UnityEngine.Object obj8 = UnityEngine.Object.Instantiate(m_avatar);
				clip.GoTanAnh = (GameObject)((obj8 is GameObject) ? obj8 : null);
				clip.GoTanAnh.transform.localScale = Vector3.one * m_spawnInfo.TyLeModel;
				clip.GoTanAnh.transform.position = m_avatar.transform.position;
				clip.GoTanAnh.transform.eulerAngles = m_avatar.transform.eulerAngles;
				clip.GoTanAnh.AddComponent("TanAnhHero");
			}
			else if (clip.Name == "play_remove")
			{
				base.gameObject.AddComponent("TanAnhHero");
			}
		}
	}

	public void PlayClip_VCName(string vc, float delay)
	{
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		if (!(screenBattle == null) && screenBattle.DataReplay != null && screenBattle.DataReplay.Team1Data != null && !screenBattle.DataReplay.Team1Data.Name.StartsWith("EGTest"))
		{
			ClipAction clipAction = new ClipAction();
			clipAction.Name = "play_vc_name";
			clipAction.TimeDelay = delay;
			clipAction.Time = 1f;
			clipAction.ParamStr1 = vc;
			ClipActionList.Add(clipAction);
		}
	}

	public void PlayClip_Anim(string anim_name, float delay, bool loop, float speed = 1f)
	{
		ClipAction clipAction = new ClipAction();
		clipAction.Name = "play_anim";
		clipAction.TimeDelay = delay;
		clipAction.ParamStr1 = anim_name;
		clipAction.ParamBoolean1 = loop;
		clipAction.ParamFloat1 = speed;
		float num = 1f;
		if (anim_name == "change")
		{
			num = 0.8f;
		}
		clipAction.Time = GetAnimationLength(anim_name) * num / speed;
		foreach (ClipAction clipAction2 in ClipActionList)
		{
			if (clipAction2.Name == "play_anim")
			{
				clipAction2.MarkAsRemove = true;
			}
		}
		ClipActionList.Add(clipAction);
		if (delay <= 0f)
		{
			PlayClip(clipAction);
			clipAction.TimeDelay = float.MaxValue;
		}
	}

	public void PlayClip_Particle(string prefab, float delay, float lifeTime, bool attached, Vector3 position, Vector3 lookat, float time_detach = float.MaxValue, bool pauseAsFrozen = true)
	{
		ClipAction clipAction = new ClipAction();
		clipAction.Name = "play_particle";
		clipAction.ParamStr1 = prefab;
		clipAction.TimeDelay = delay;
		clipAction.TimeDetach = time_detach;
		clipAction.Time = lifeTime;
		clipAction.Attached = attached;
		clipAction.ParamVector = position;
		clipAction.PauseAsFrozen = pauseAsFrozen;
		clipAction.LookAtPosition = lookat;
		ClipActionList.Add(clipAction);
		if (delay <= 0f)
		{
			PlayClip(clipAction);
		}
	}

	public void PlayClip_VCVuKhiAo(VCType vc, float delay)
	{
		ClipAction clipAction = new ClipAction();
		clipAction.Name = "play_vc_vk_ao";
		clipAction.ParamStr1 = vc.ToString();
		clipAction.TimeDelay = delay;
		clipAction.Time = CommonHero.GetVCAnimTime(vc);
		ClipActionList.Add(clipAction);
		if (delay <= 0f)
		{
			PlayClip(clipAction);
		}
	}

	public void PlayClip_ProjectTile(string prefab, float delay, int TID)
	{
		if (m_avatar == null)
		{
			return;
		}
		ClipAction clipAction = new ClipAction();
		clipAction.Name = "play_projectile";
		clipAction.ParamStr1 = prefab;
		clipAction.TimeDelay = delay;
		clipAction.Time = 0.3f;
		BattleHero battleHero = SearchHero(TID);
		if (battleHero != null)
		{
			float num = Vector3.Distance(battleHero.transform.position, base.transform.position);
			if (num < 3f)
			{
				clipAction.Time = num / 3f * 0.3f;
			}
		}
		Transform transform = m_avatar.transform.Find("jnt_root/jnt_hip/jnt_spine_01/jnt_spine_02/jnt_chest").transform;
		Vector3 position = transform.position;
		clipAction.ParamVector = position;
		clipAction.ParamInt1 = TID;
		ClipActionList.Add(clipAction);
		if (delay <= 0f)
		{
			PlayClip(clipAction);
		}
	}

	private void PlayClip_TrailStart(float timeToTweenTo, float fadeInTime)
	{
		ClipAction clipAction = new ClipAction();
		clipAction.Name = "trail_start";
		clipAction.TimeDelay = 0f;
		clipAction.ParamFloat1 = timeToTweenTo;
		clipAction.ParamFloat2 = fadeInTime;
		clipAction.Time = 1f;
		ClipActionList.Add(clipAction);
	}

	private void PlayClip_TrailFadeOut(float delay, float fadeOutTime)
	{
		ClipAction clipAction = new ClipAction();
		clipAction.Name = "trail_fade_out";
		clipAction.TimeDelay = delay;
		clipAction.ParamFloat1 = fadeOutTime;
		clipAction.Time = 1f;
		ClipActionList.Add(clipAction);
	}

	public void PlayClip_Sound(string sound_name, float delay)
	{
		ClipAction clipAction = new ClipAction();
		clipAction.Name = "play_sound";
		clipAction.ParamStr1 = sound_name;
		clipAction.TimeDelay = delay;
		clipAction.Time = 5f;
		ClipActionList.Add(clipAction);
	}

	public void PlayClip_SetVCHU(int ID, VCEffect.VCEffectType type, float time)
	{
		foreach (ClipAction clipAction2 in ClipActionList)
		{
			if (clipAction2.VCHUName == type)
			{
				return;
			}
		}
		ClipAction clipAction = new ClipAction();
		clipAction.Name = "set_vc_effect";
		clipAction.TimeDelay = 0f;
		clipAction.Time = time;
		clipAction.ID = ID;
		clipAction.VCHUName = type;
		ClipActionList.Add(clipAction);
		PlayClip(clipAction);
	}

	public void PlayClip_TanAnh(float delay, float lifetime)
	{
		ClipAction clipAction = new ClipAction();
		clipAction.Name = "play_tan_anh";
		clipAction.TimeDelay = delay;
		clipAction.Time = lifetime;
		ClipActionList.Add(clipAction);
	}

	public void PlayClip_Remove(float delay, float lifetime)
	{
		ClipAction clipAction = new ClipAction();
		clipAction.Name = "play_remove";
		clipAction.TimeDelay = delay;
		clipAction.Time = lifetime;
		ClipActionList.Add(clipAction);
	}

	public void RemoveVCHU(int ID)
	{
		int count = ClipActionList.Count;
		for (int num = count - 1; num >= 0; num--)
		{
			ClipAction clipAction = ClipActionList[num];
			if (clipAction.ID == ID)
			{
				clipAction.MarkAsRemove = true;
			}
		}
	}

	private void OnDestroy()
	{
		if (HeroGUIPanelGo != null)
		{
			UnityEngine.Object.Destroy(HeroGUIPanelGo);
		}
		HeroGUIPanelGo = null;
		ClearActionList();
		if (m_goVk != null)
		{
			UnityEngine.Object.Destroy(m_goVk);
		}
		m_avatar = null;
	}

	private void LoadAnimVuKhi()
	{
		UnityEngine.Object obj = Resources.Load("FX/Anim/" + m_spawnInfo.AnimVK, typeof(GameObject));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (!(gameObject != null))
		{
			return;
		}
		foreach (AnimationState item in gameObject.GetComponent<Animation>())
		{
			AnimationState animationState2 = item;
			bool flag = animationState2.name.Contains("change");
			AnimationClip clip = gameObject.GetComponent<Animation>().GetClip(animationState2.name);
			if (animationState2.name.Contains("attack_") && !animationState2.name.Contains("wait"))
			{
				AddAnimEvent(clip, "OnFinish_PlayAttackThuong", 1f);
			}
			if (m_spawnInfo.AnimVK.StartsWith("NV_AmKhi"))
			{
				if (animationState2.name == "attack_0")
				{
					AddAnimEvent(clip, "OnStartAmKhi0", 0.52f);
				}
				else if (animationState2.name == "attack_1")
				{
					AddAnimEvent(clip, "OnStartAmKhi1", 0.55f);
				}
				else if (animationState2.name == "attack_2")
				{
					AddAnimEvent(clip, "OnStartAmKhi2", 0.76f);
				}
				else if (animationState2.name == "attack_3")
				{
					AddAnimEvent(clip, "OnStartAmKhi3", 0.76f);
				}
			}
			else if (m_spawnInfo.AnimVK.StartsWith("NV_Kiem") && m_spawnInfo.Name == "NV_KIEM_THANH")
			{
				if (animationState2.name == "attack_0")
				{
					AddAnimEvent(clip, "OnStartKiemKhi0", 0.52f);
				}
				if (animationState2.name == "attack_1")
				{
					AddAnimEvent(clip, "OnStartKiemKhi0", 0.52f);
				}
				if (animationState2.name == "attack_2")
				{
					AddAnimEvent(clip, "OnStartKiemKhi0", 0.52f);
				}
				if (animationState2.name == "attack_3")
				{
					AddAnimEvent(clip, "OnStartKiemKhi0", 0.52f);
				}
			}
			m_avatar.GetComponent<Animation>().AddClip(clip, animationState2.name);
		}
	}

	public static string GetBaseVCName(string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			return name;
		}
		if (name.EndsWith("_SS"))
		{
			return name.Substring(0, name.Length - 3);
		}
		if (name.EndsWith("_S") || name.EndsWith("_A") || name.EndsWith("_B"))
		{
			return name.Substring(0, name.Length - 2);
		}
		return name;
	}

	private void LoadAnimVC()
	{
		UnityEngine.Object obj = Resources.Load("FX/Anim/" + SkillFileStr, typeof(GameObject));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (gameObject == null)
		{
			return;
		}
		AnimationClip clip = gameObject.GetComponent<Animation>().GetClip("VC_CHARGE");
		if (clip != null)
		{
			m_avatar.GetComponent<Animation>().AddClip(clip, "VC_CHARGE");
		}
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		if (screenBattle != null && screenBattle.DataReplay != null && screenBattle.DataReplay.DauNoiLuc)
		{
			string[] dauNoiLucList = AnimNameCfg.DauNoiLucList;
			string[] array = dauNoiLucList;
			foreach (string text in array)
			{
				AnimationClip clip2 = gameObject.GetComponent<Animation>().GetClip(text);
				if (clip2 != null && text.Contains("DAU_NOI_LUC"))
				{
					if (text.Contains("_TO_"))
					{
						AddAnimEvent(clip2, "OnFinish_DauNoiLucChange", 1f);
					}
					m_avatar.GetComponent<Animation>().AddClip(clip2, text);
				}
			}
		}
		foreach (VCType vC in SpawnInfo.VCList)
		{
			string text2 = vC.ToString();
			string baseVCName = GetBaseVCName(text2);
			AnimationClip clip3 = gameObject.GetComponent<Animation>().GetClip(baseVCName);
			if (clip3 == null)
			{
				clip3 = gameObject.GetComponent<Animation>().GetClip(text2);
			}
			if (clip3 != null)
			{
				AddAnimEvent(clip3, "OnFinish_PlayVC", 1f);
				m_avatar.GetComponent<Animation>().AddClip(clip3, text2);
				if (baseVCName != text2)
				{
					m_avatar.GetComponent<Animation>().AddClip(clip3, baseVCName);
				}
			}
		}
	}

	private void LoadAnimLengthDic()
	{
		AnimLengthDic.Clear();
		UnityEngine.Object obj = Resources.Load("FX/Anim/" + m_spawnInfo.AnimVK, typeof(GameObject));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (!(gameObject != null))
		{
			return;
		}
		foreach (AnimationState item in gameObject.GetComponent<Animation>())
		{
			AnimationState animationState2 = item;
			AnimationClip clip = gameObject.GetComponent<Animation>().GetClip(animationState2.name);
			AnimLengthDic.Add(animationState2.name, clip.length);
		}
	}

	public void Init()
	{
		UnityEngine.Object obj = Resources.Load("Battle/HeroGUIPanel", typeof(GameObject));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (gameObject != null)
		{
			UIFollowTarget component = gameObject.GetComponent<UIFollowTarget>();
			component.uiCamera = GUIManager.instance.cam2D;
			component.gameCamera = Camera.main;
			component.target = GUIPivot.transform;
			component.disableIfInvisible = false;
			UnityEngine.Object obj2 = UnityEngine.Object.Instantiate(gameObject);
			HeroGUIPanelGo = (GameObject)((obj2 is GameObject) ? obj2 : null);
			HeroGUIPanelGo.transform.parent = GUIManager.instance.cam2D.transform;
			HeroGUIPanelGo.transform.localPosition = Vector3.zero;
			HeroGUIPanelGo.transform.localRotation = Quaternion.identity;
			HeroGUIPanelGo.transform.localScale = Vector3.one;
			GUIPanel = HeroGUIPanelGo.GetComponent<HeroGUIPanel>();
			GUIPanel.LabelPopUp.gameObject.SetActive(false);
			GUIPanel.SpriteBkg.gameObject.SetActive(false);
			GUIPanel.gameObject.SetActive(SpawnInfo.DHPri);
		}
		m_AudioSource = base.gameObject.AddComponent<AudioSource>();
		m_avatar.AddComponent("AnimKeyFrameEvent");
		m_avatar.AddComponent("AnimationController");
		m_animationController = m_avatar.GetComponent<AnimationController>();
		if (!m_spawnInfo.Name.StartsWith("NC_DONG_NHAN"))
		{
			SkillFileStr = "skill";
			NhanVatCfg value;
			ConfigManager.instance.m_dicNhanVats.TryGetValue(m_spawnInfo.Name, out value);
			if (value != null)
			{
				SkillFileStr = ((value.Sex != NhanVatCfg.GioiTinh.Nam) ? "skill_F" : "skill");
			}
			LoadAnimVuKhi();
			LoadAnimVC();
			AddEventTanAnh();
			LoadAnimLengthDic();
		}
		SyncLoad();
	}

	private void AddAnimEvent(AnimationClip clip, string funcName, float heso)
	{
		if (!(clip == null))
		{
			string item = clip.name + "_" + funcName;
			if (!s_registeredAnimEvents.Contains(item))
			{
				s_registeredAnimEvents.Add(item);
				AnimationEvent animationEvent = new AnimationEvent();
				animationEvent.functionName = funcName;
				animationEvent.time = clip.length * heso;
				animationEvent.messageOptions = SendMessageOptions.RequireReceiver;
				clip.AddEvent(animationEvent);
			}
		}
	}

	private void Log(string txt)
	{
	}

	private void LoadVuKhi()
	{
		GameObject gameObject = null;
		UnityEngine.Object obj = Resources.Load("VuKhi/" + m_spawnInfo.VK, typeof(GameObject));
		gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (gameObject != null)
		{
			UnityEngine.Object obj2 = UnityEngine.Object.Instantiate(gameObject);
			m_goVk = (GameObject)((obj2 is GameObject) ? obj2 : null);
			Transform parent = m_avatar.transform.Find("jnt_root/jnt_weapon_R").transform;
			m_goVk.transform.parent = parent;
			m_goVk.transform.localRotation = Quaternion.identity;
			m_goVk.transform.localPosition = Vector3.zero;
		}
	}

	private void AddEventTanAnh()
	{
		UnityEngine.Object obj = Resources.Load("FX/Anim/" + SkillFileStr, typeof(GameObject));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (gameObject == null)
		{
			return;
		}
		foreach (VCType vC in m_spawnInfo.VCList)
		{
			string text = vC.ToString();
			string baseVCName = GetBaseVCName(text);
			CfgVoCong value;
			ConfigManager.instance.m_dicVCs.TryGetValue(text, out value);
			if (value == null && baseVCName != text)
			{
				ConfigManager.instance.m_dicVCs.TryGetValue(baseVCName, out value);
			}
			if (value == null || value.m_Class != VCClass.CHIEU_THUC)
			{
				continue;
			}
			AnimationClip clip = gameObject.GetComponent<Animation>().GetClip(baseVCName);
			if (clip == null)
			{
				clip = gameObject.GetComponent<Animation>().GetClip(text);
			}
			if (!(clip != null))
			{
				continue;
			}
			string item = clip.name + "_TanAnh";
			if (!s_registeredAnimEvents.Contains(item))
			{
				s_registeredAnimEvents.Add(item);
				for (int i = 0; i < value.TanAnh; i++)
				{
					AnimationEvent animationEvent = new AnimationEvent();
					animationEvent.functionName = "OnPlayVC_TanAnh";
					animationEvent.time = clip.length * ((float)(i + 1) / (float)value.TanAnh);
					animationEvent.messageOptions = SendMessageOptions.RequireReceiver;
					clip.AddEvent(animationEvent);
				}
			}
		}
	}

	private void LoadParVC()
	{
		foreach (VCType vC in m_spawnInfo.VCList)
		{
			string text = vC.ToString();
			CfgVoCong value;
			ConfigManager.instance.m_dicVCs.TryGetValue(text, out value);
			if (value != null && value.m_Class != VCClass.CHIEU_THUC)
			{
				if (text.EndsWith("_S") || text.EndsWith("_A") || text.EndsWith("_B"))
				{
					text = text.Substring(0, text.Length - 2);
				}
				if (text.EndsWith("_SS"))
				{
					text = text.Substring(0, text.Length - 3);
				}
				PlayClip_Particle("FX/Prefabs/" + text, 0f, float.MaxValue, true, Vector3.zero, Vector3.zero);
			}
		}
	}

	private void LoadWeaponTrail()
	{
		if (m_animationController != null && m_goVk != null && m_spawnInfo.VK != "VK_THIEN_KIEP_THIEN_TOI")
		{
			m_weaponTrail = m_goVk.GetComponent<WeaponTrail>();
			m_weaponTrail.SetTime(0f, 0f, 1f);
			m_animationController.AddTrail(m_weaponTrail);
		}
	}

	public void OnVisible()
	{
		GUIPanel.gameObject.SetActive(true);
		TatHoiThoai();
		if (GUIPanel.SpriteHPBar != null)
		{
			ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
			if (screenBattle.ChkTeam1IsOwner())
			{
				GUIPanel.SpriteHPBar.color = ((m_spawnInfo.TeamID != 0) ? Color.red : Color.green);
			}
			else
			{
				GUIPanel.SpriteHPBar.color = ((m_spawnInfo.TeamID != 0) ? Color.green : Color.red);
			}
		}
		if (m_spawnInfo.Type == HeroType.NhanVat)
		{
			LoadVuKhi();
			LoadWeaponTrail();
			LoadParVC();
		}
	}

	public void LoadAvatar()
	{
		NhanVatCfg value;
		ConfigManager.instance.m_dicNhanVats.TryGetValue(SpawnInfo.Name, out value);
		UnityEngine.Object obj = null;
		obj = ((!string.IsNullOrEmpty(SpawnInfo.Costume)) ? Resources.Load("Costumes/" + SpawnInfo.Costume) : Resources.Load("NhanVat/" + SpawnInfo.Name));
		if (obj != null)
		{
			UnityEngine.Object obj2 = UnityEngine.Object.Instantiate(obj);
			m_avatar = (GameObject)((obj2 is GameObject) ? obj2 : null);
		}
		else if (value.Sex == NhanVatCfg.GioiTinh.Nam)
		{
			UnityEngine.Object obj3 = UnityEngine.Object.Instantiate(Resources.Load("NhanVat/NV_LY_TAM_HOAN"));
			m_avatar = (GameObject)((obj3 is GameObject) ? obj3 : null);
		}
		else
		{
			UnityEngine.Object obj4 = UnityEngine.Object.Instantiate(Resources.Load("NhanVat/NV_LAM_TRIEU_ANH"));
			m_avatar = (GameObject)((obj4 is GameObject) ? obj4 : null);
		}
		m_avatar.transform.parent = base.gameObject.transform;
		m_avatar.transform.localPosition = Vector3.zero;
		m_avatar.transform.localRotation = Quaternion.Euler(Vector3.zero);
	}

	public override void Start()
	{
		try
		{
			base.Start();
			Selected = false;
		}
		catch (Exception ex)
		{
			EGDebug.Log(ex.ToString());
		}
	}

	private Vector3 Get2DWorldPos()
	{
		Vector3 position = base.transform.position;
		position.y += 1.8f;
		Vector3 vector = Camera.main.WorldToScreenPoint(position);
		float num = 1136f / (float)Screen.height;
		Vector3 vector2 = default(Vector3);
		return new Vector3(vector.x * num, vector.y * num, 0f);
	}

	public void PlayVCName(string vc)
	{
		if (!GameManager.instance.Trailer)
		{
			Color c = Color.green;
			CfgVoCong value;
			ConfigManager.instance.m_dicVCs.TryGetValue(vc, out value);
			if (value == null)
			{
				string baseVCName = GetBaseVCName(vc);
				ConfigManager.instance.m_dicVCs.TryGetValue(baseVCName, out value);
			}
			if (value != null)
			{
				c = ((value.Hang == 3) ? new Color(0.8f, 0f, 1f) : ((value.Hang != 2) ? Color.green : Color.blue));
				vc = value.TenHienThi;
			}
			GUIPanel.VCText.Add(vc, c, 0f);
		}
	}

	private GameObject GetGoVCName(string prefab, string text, Color color)
	{
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load(prefab));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		gameObject.transform.parent = GUIPanel.transform;
		gameObject.transform.localPosition = new Vector3(0f, 2.5f, 0f);
		gameObject.transform.localScale = Vector3.one * 1.2f;
		gameObject.transform.localRotation = Quaternion.identity;
		UILabel component = gameObject.GetComponent<UILabel>();
		if (component != null)
		{
			component.text = text;
			component.color = color;
		}
		return gameObject;
	}

	public void SetDmgStr(string sDmg, bool xuat_huyet = false, bool crit = false)
	{
		try
		{
			ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
			if (screenBattle == null || screenBattle.DataReplay == null || screenBattle.DataReplay.Team1Data == null || screenBattle.DataReplay.Team1Data.Name.StartsWith("EGTest"))
			{
				return;
			}
			Color color = default(Color);
			if (!xuat_huyet)
			{
				color = ((m_spawnInfo != null && m_spawnInfo.TeamID != 0) ? Color.white : Color.red);
				if (crit)
				{
					GUIPanel.CritText.Add(sDmg, color, 0f);
				}
				else
				{
					GUIPanel.DmgText.Add(sDmg, color, 0f);
				}
			}
			else
			{
				color = new Color(177f / 255f, 56f / 255f, 203f / 255f);
				GUIPanel.DmgText.Add(sDmg, color, 0f);
			}
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}

	private IEnumerator CoAlphaPlayDamage(GameObject go_damageStr)
	{
		TweenAlpha.Begin(go_damageStr, 0.3f, 1f);
		yield return new WaitForSeconds(0.4f);
		TweenAlpha.Begin(go_damageStr, 1f, 0f).onFinished = RemoveGo;
		yield return null;
	}

	private IEnumerator CoAlphaPlayVCName(GameObject go_damageStr)
	{
		TweenAlpha.Begin(go_damageStr, 0.3f, 1f);
		yield return new WaitForSeconds(1f);
		TweenAlpha.Begin(go_damageStr, 0.8f, 0f).onFinished = RemoveGo;
		yield return null;
	}

	private IEnumerator CoAlphaPlayVCName1(GameObject go_damageStr)
	{
		TweenAlpha.Begin(go_damageStr, 0.25f, 0.75f);
		yield return new WaitForSeconds(0.45f);
		TweenAlpha.Begin(go_damageStr, 0.5f, 0f).onFinished = RemoveGo;
		yield return null;
	}

	private IEnumerator CoDamageScaleText(GameObject go_damageStr, float MAXSCALE, float MINSCALE)
	{
		go_damageStr.transform.localScale = new Vector3(MAXSCALE, MAXSCALE, 1f);
		TweenScale.Begin(go_damageStr, 0.3f, new Vector3(MINSCALE, MINSCALE, 1f));
		yield return null;
	}

	private void RemoveGo(UITweener tweener)
	{
		UnityEngine.Object.Destroy(tweener.transform.gameObject);
	}

	private void RotateTowardCam(UIPanel panel)
	{
		if (!(panel == null))
		{
			Vector3 vector = Camera.main.transform.rotation * Vector3.forward;
			Vector3 forward = vector - Vector3.up * Vector3.Dot(Vector3.up, vector);
			panel.transform.rotation = Quaternion.LookRotation(forward);
		}
	}

	public void SetCfg(NhanVatCfg cfg)
	{
		try
		{
			_cfg = cfg;
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}

	public Vector3 GetNextVelocity(int currentIndex, List<BattleReplay.TrangThai> listRepState)
	{
		Vector3 result = Vector3.zero;
		try
		{
			BattleReplay.TrangThai trangThai = listRepState[currentIndex];
			if (trangThai.SD.S != HeroState.MOVE || base.Pause)
			{
				return result;
			}
			Vector3 vector = default(Vector3);
			vector = new Vector3(trangThai.SD.X, 0f, trangThai.SD.Z);
			Vector3 vector2 = default(Vector3);
			for (int i = currentIndex + 1; i < listRepState.Count; i++)
			{
				BattleReplay.TrangThai trangThai2 = listRepState[i];
				if (trangThai2.SD != null && trangThai2.SD.HID == HID)
				{
					vector2 = new Vector3(trangThai2.SD.X, 0f, trangThai2.SD.Z);
					float num = trangThai2.T - trangThai.T;
					result = ((!(num > 0f)) ? Vector3.zero : ((vector2 - base.transform.localPosition) / num));
					return result;
				}
			}
		}
		catch (Exception ex)
		{
			EGDebug.Log(ex.ToString());
		}
		return result;
	}

	public Vector3 GetNextDestination(int currentIndex, List<BattleReplay.TrangThai> listRepState)
	{
		try
		{
			for (int i = currentIndex + 1; i < listRepState.Count; i++)
			{
				BattleReplay.TrangThai trangThai = listRepState[i];
				if (trangThai.SD != null && trangThai.SD.HID == HID)
				{
					return new Vector3(trangThai.SD.X, 0f, trangThai.SD.Z);
				}
			}
		}
		catch
		{
		}
		return Vector3.zero;
	}

	public void ReplayEff(BattleReplay.TrangThai.HieuUng effectData)
	{
		RepVcEffect repVcEffect = new RepVcEffect();
		repVcEffect.EFFECT.TD = effectData.TD;
		repVcEffect.EFFECT.VCHD = effectData.VCHD;
		PlayEffect(repVcEffect);
	}

	public void ReplayState(int index, List<BattleReplay.TrangThai> listRepState)
	{
		if (listRepState == null || index < 0 || index >= listRepState.Count)
		{
			return;
		}
		m_replayParameter = listRepState[index];
		if (m_replayParameter == null || m_replayParameter.SD == null)
		{
			return;
		}
		if (m_replayParameter.HUAList != null && m_replayParameter.HUAList.Count > 0)
		{
			StartCoroutine(CoRepVCEffect(m_replayParameter.HUAList));
		}
		if (m_state == HeroState.DEAD && m_replayParameter.SD.S != HeroState.SONG_LAI)
		{
			return;
		}
		CancelGoto();
		m_prevstate = m_state;
		m_state = m_replayParameter.SD.S;
		if (m_goVk != null)
		{
			m_goVk.SetActive(true);
		}
		switch (m_state)
		{
		case HeroState.THAY_NGUOI:
		{
			base.gameObject.SetActive(true);
			SetVisible(true);
			OnVisible();
			ActiveHpBar(true);
			Vector3 pos = new Vector3(m_replayParameter.SD.X, 0f, m_replayParameter.SD.Z);
			Vector3 localPosition = new Vector3(m_spawnInfo.PosX, 0f, m_spawnInfo.PosZ);
			base.transform.localPosition = localPosition;
			TweenPosition.Begin(base.gameObject, 0.65f, pos);
			PlayClip_Anim("change", 0f, false);
			break;
		}
		case HeroState.SONG_LAI:
			PlayClip_Particle("FX/Prefabs/EFF_HOI_SINH", 0f, 2f, true, Vector3.zero, Vector3.zero);
			PlayClip_Anim("revival", 0f, false);
			base.transform.localPosition = new Vector3(m_replayParameter.SD.X, 0f, m_replayParameter.SD.Z);
			ActiveHpBar(true);
			if (m_goVk != null)
			{
				m_goVk.SetActive(true);
			}
			break;
		case HeroState.CAST_SKILL:
		{
			BattleHero battleHero = SearchHero(m_replayParameter.SD.VCD.TID);
			if (battleHero != null)
			{
				base.transform.LookAt(battleHero.transform.position);
			}
			base.transform.localPosition = new Vector3(m_replayParameter.SD.X, 0f, m_replayParameter.SD.Z);
			PlayClip_Anim("VC_CHARGE", 0f, true);
			CfgVoCong value;
			ConfigManager.instance.m_dicVCs.TryGetValue(m_replayParameter.SD.VCD.VC.ToString(), out value);
			if (value != null && value.CongKichType != VCCongKich.PHU_TRO && battleHero != null)
			{
				if (value.CongKichType == VCCongKich.DA_THE)
				{
					battleHero.PlayClip_Particle("FX/Prefabs/TARGET", 0f, value.CastTime, false, battleHero.transform.position, Vector3.zero);
				}
				else
				{
					battleHero.PlayClip_Particle("FX/Prefabs/TARGET", 0f, value.CastTime, true, Vector3.zero, Vector3.zero);
				}
			}
			if (m_goVk != null)
			{
				m_goVk.SetActive(false);
			}
			break;
		}
		case HeroState.CHOANG:
			PlayClip_Anim("stun", 0f, true);
			break;
		case HeroState.IDLE:
		case HeroState.DINH_THAN:
			if (m_avatar != null)
			{
				PlayClip_Anim("idle", 0f, true);
				base.transform.localPosition = new Vector3(m_replayParameter.SD.X, 0f, m_replayParameter.SD.Z);
			}
			break;
		case HeroState.ATTACK_SKILL:
			StartCoroutine(CoRepAttackSkill());
			break;
		case HeroState.ATTACK_THUONG:
			if (m_weaponTrail != null)
			{
				m_weaponTrail.SetTrailColor(Color.white);
			}
			RepAttackThuong();
			PlayClip_Sound((m_spawnInfo == null || !m_spawnInfo.AnimVK.StartsWith("NV_AmKhi")) ? "CanChien" : "DanhXa", 0f);
			break;
		case HeroState.DEAD:
			if (ChiSo != null)
			{
				ChiSo.HP = 0f;
			}
			ClearActionList();
			SetPause(false);
			StartAnim("die", false);
			if (m_weaponTrail != null)
			{
				m_weaponTrail.ClearTrail();
			}
			ActiveHpBar(false);
			if (m_goVk != null)
			{
				m_goVk.SetActive(false);
			}
			if (m_replayParameter.SD.D != null)
			{
				Vector3 vector2 = default(Vector3);
				TweenPosition.Begin(pos: new Vector3(m_replayParameter.SD.D.X, 0f, m_replayParameter.SD.D.Z), go: base.gameObject, duration: 0.5f);
			}
			break;
		case HeroState.MOVE:
		{
			if (m_weaponTrail != null)
			{
				m_weaponTrail.ClearTrail();
			}
			Vector3 nextVelocity = GetNextVelocity(index, listRepState);
			Vector3 nextDestination = GetNextDestination(index, listRepState);
			if (nextDestination != Vector3.zero)
			{
				Goto(nextVelocity, nextDestination);
			}
			else
			{
				Vector3 vector = new Vector3(m_replayParameter.SD.X, 0f, m_replayParameter.SD.Z);
				Goto(nextVelocity, vector);
				if (nextVelocity == Vector3.zero)
				{
					base.transform.localPosition = vector;
				}
			}
			StartAnim("run");
			break;
		}
		case HeroState.REMOVE:
			break;
		}
	}

	private IEnumerator CoRepAttackSkill()
	{
		BattleHero tar = SearchHero(m_replayParameter.SD.VCD.TID);
		if (tar != null)
		{
			base.transform.LookAt(tar.transform.position);
		}
		base.transform.localPosition = new Vector3(m_replayParameter.SD.X, 0f, m_replayParameter.SD.Z);
		VCType skillName = m_replayParameter.SD.VCD.VC;
		string baseVcName = GetBaseVCName(skillName.ToString());
		Vector3 lookAtPos = Vector3.zero;
		VCType vCType = skillName;
		VCType vCType2 = vCType;
		if (vCType2 == VCType.VC_LUC_MACH_THAN_KIEM || (uint)(vCType2 - 174) <= 1u || vCType2 == VCType.VC_CAU_HON_THAT_DOAT)
		{
			lookAtPos = base.transform.position;
		}
		float lifeT = CommonHero.GetVCAnimTime(skillName) + 1f;
		if (m_replayParameter.SD.VCD.TeleportToPosData != null)
		{
			TweenPosition.Begin(pos: new Vector3(m_replayParameter.SD.VCD.TeleportToPosData.X, 0f, m_replayParameter.SD.VCD.TeleportToPosData.Z), go: base.gameObject, duration: 0.5f);
		}
		if (m_replayParameter.SD.VCD.IDaThe != null)
		{
			string impactPref = "FX/Prefabs/" + baseVcName + "_IMPACT";
			float impactTime = Mathf.Min(0.25f, CommonHero.GetVCTimeDamage(skillName, 1));
			Vector3 vImpact = new Vector3(m_replayParameter.SD.VCD.IDaThe.X, 0f, m_replayParameter.SD.VCD.IDaThe.Z);
			Vector3 vImpactW = base.transform.parent.transform.TransformPoint(vImpact);
			PlayClip_Particle(impactPref, impactTime, lifeT, false, vImpactW, lookAtPos);
		}
		if (m_replayParameter.SD.VCD.IDonThe != null && tar != null)
		{
			string impactPref2 = "FX/Prefabs/" + baseVcName + "_IMPACT";
			float impactTime2 = Mathf.Min(0.2f, CommonHero.GetVCTimeDamage(skillName, 1));
			Vector3 chestPos = new Vector3(0f, 0.9f, 0f);
			tar.PlayClip_Particle(impactPref2, impactTime2, lifeT, true, chestPos, Vector3.zero, float.MaxValue, false);
		}
		if (m_replayParameter.SD.VCD.IDonThe_1 != null)
		{
			BattleHero extra_1 = SearchHero(m_replayParameter.SD.VCD.IDonThe_1.HID);
			if (extra_1 != null)
			{
				string impactPref3 = "FX/Prefabs/" + baseVcName + "_IMPACT";
				float impactTime3 = Mathf.Min(0.2f, CommonHero.GetVCTimeDamage(skillName, 1));
				Vector3 chestPos2 = new Vector3(0f, 0.9f, 0f);
				extra_1.PlayClip_Particle(impactPref3, impactTime3, lifeT, true, chestPos2, Vector3.zero, float.MaxValue, false);
			}
		}
		if (m_replayParameter.SD.VCD.IPhuTro != null)
		{
			string impactPref4 = "FX/Prefabs/" + baseVcName + "_IMPACT";
			foreach (int i in m_replayParameter.SD.VCD.IPhuTro.DongDoiHoTroList)
			{
				BattleHero dong_doi_ho_tro = SearchHero(i);
				if (dong_doi_ho_tro != null)
				{
					dong_doi_ho_tro.PlayClip_Particle(impactPref4, 0f, lifeT, false, dong_doi_ho_tro.transform.position, Vector3.zero, float.MaxValue, false);
				}
			}
		}
		if (m_replayParameter.SD.AT != null)
		{
			if (m_weaponTrail != null)
			{
				m_weaponTrail.SetTrailColor(Color.red);
			}
			RepAttackThuong();
		}
		else
		{
			float vcALen = CommonHero.GetVCAnimTime(skillName);
			string vcNamePref = "FX/Prefabs/" + baseVcName;
			if (SpawnInfo.Type == HeroType.DongNhanCoc)
			{
				PlayClip_Particle(vcNamePref, 0f, lifeT, false, base.transform.position, Vector3.zero, vcALen);
			}
			else
			{
				PlayClip_Particle(vcNamePref, 0f, lifeT, true, Vector3.zero, Vector3.zero, vcALen);
			}
			if (tar != null)
			{
				PlayClip_ProjectTile(vcNamePref + "_PROJECTILE", 0f, tar.HID);
			}
			if (m_weaponTrail != null)
			{
				m_weaponTrail.SetTrailColor(new Color(1f, 253f / 255f, 251f / 255f, 1f));
			}
			if (m_goVk != null)
			{
				m_goVk.SetActive(false);
			}
			if (m_avatar != null && m_avatar.GetComponent<Animation>() != null && m_avatar.GetComponent<Animation>()[baseVcName] != null)
			{
				PlayClip_Anim(baseVcName, 0f, false);
			}
			else
			{
				PlayClip_Anim(skillName.ToString(), 0f, false);
			}
			PlayClip_VCName(skillName.ToString(), 0f);
			PlayClip_VCVuKhiAo(skillName, 0f);
			PlayClip_Sound("DanhChieu", 0f);
			switch (skillName)
			{
			case VCType.VC_THIEN_NGOAI_PHI_TIEN:
			case VCType.VC_NHAT_KIEM_TAY_LAI:
			case VCType.VC_THIEN_SON_CHIET_MAI_THU:
			case VCType.VC_CAU_HON_THAT_DOAT:
			case VCType.VC_KIEM_TIEU_HONG_TRAN:
			case VCType.VC_PHONG_THAN_THOAI:
				if (tar != null)
				{
					Vector3 v2 = base.transform.localPosition;
					Vector3 v3 = tar.transform.localPosition;
					yield return new WaitForSeconds(0.1f);
					base.transform.localPosition = v3;
					yield return new WaitForSeconds(vcALen * 0.83f - 0.1f);
					TweenPosition.Begin(base.gameObject, 0.2f, v2);
				}
				break;
			case VCType.VC_THIEN_KIEM_CAN_KHON:
			{
				Vector3 v1 = base.transform.localPosition;
				yield return new WaitForSeconds(vcALen - 0.1f);
				TweenPosition.Begin(base.gameObject, 0.1f, v1);
				break;
			}
			}
		}
		yield return null;
	}

	private void RepAttackThuong()
	{
		TID = m_replayParameter.SD.AT.TID;
		BattleHero battleHero = SearchHero(TID);
		if (battleHero != null)
		{
			base.transform.LookAt(battleHero.transform.position);
			battleHero.PlayClip_Particle("FX/Prefabs/VC_IMPACT", 0f, 1f, true, Vector3.zero, Vector3.zero);
		}
		base.transform.localPosition = new Vector3(m_replayParameter.SD.X, 0f, m_replayParameter.SD.Z);
		string animAttack = GetAnimAttack(m_replayParameter.SD.AT.CNbr);
		float animationLength = GetAnimationLength(animAttack);
		PlayClip_TrailStart(animationLength * 0.5f, animationLength * 0.4f);
		if (m_replayParameter.SD.AT.AS <= animationLength)
		{
			float speed = animationLength / m_replayParameter.SD.AT.AS;
			PlayClip_Anim(animAttack, 0f, false, speed);
			PlayClip_TrailFadeOut(animationLength * 0.4f, animationLength * 0.6f);
		}
		else
		{
			PlayClip_Anim(animAttack, 0f, false);
			PlayClip_TrailFadeOut(animationLength * 0.4f, animationLength * 0.6f);
		}
	}

	public void StartAnim(string name, bool loop = true, float speed = 1f)
	{
		try
		{
			Log(name);
			if (base.Pause || m_avatar == null || (m_state == HeroState.DEAD && name != "die"))
			{
				return;
			}
			if (m_avatar.GetComponent<Animation>()[name] == null)
			{
				string baseVCName = GetBaseVCName(name);
				if (baseVCName != name && m_avatar.GetComponent<Animation>()[baseVCName] != null)
				{
					name = baseVCName;
				}
				else
				{
					if (!(m_avatar.GetComponent<Animation>()["attack_0"] != null))
					{
						EGDebug.Log("Error Anim: " + name + " NOT FOUND!!!");
						return;
					}
					name = "attack_0";
				}
			}
			CurAnimNamePlay = name;
			m_avatar.GetComponent<Animation>()[name].speed = speed;
			m_avatar.GetComponent<Animation>()[name].wrapMode = ((!loop) ? WrapMode.Once : WrapMode.Loop);
			m_animationController.CrossfadeAnimation(m_avatar.GetComponent<Animation>()[name], 0.1f);
		}
		catch (Exception ex)
		{
			EGDebug.Log(ex.ToString());
		}
	}

	private float GetAnimationLength(string name)
	{
		float value = 1f;
		AnimLengthDic.TryGetValue(name, out value);
		return value;
	}

	private string GetAnimAttack(int comboNumber)
	{
		m_iAttackComboSeq = comboNumber;
		return string.Format("attack_{0}", m_iAttackComboSeq);
	}

	public string GetAnimWait()
	{
		string empty = string.Empty;
		if (m_iAttackComboSeq == 3)
		{
			return "idle";
		}
		return string.Format("attack_wait_{0}", m_iAttackComboSeq);
	}

	public void FixedUpdate()
	{
	}

	public override void Update()
	{
		try
		{
			if (State == HeroState.NULL)
			{
				StartAnim("idle");
				State = HeroState.IDLE;
			}
			base.Update();
			DrawHPBar();
			OnTickClipAction();
			UpdateDebuffIcons();
		}
		catch (Exception ex)
		{
			EGDebug.Log(ex.ToString());
		}
	}

	private void UpdateDebuffIcons()
	{
		if (HeroGUIPanelGo == null || base.Pause || m_state == HeroState.DEAD)
		{
			for (int i = 0; i < m_debuffIcons.Count; i++)
			{
				if (m_debuffIcons[i] != null)
				{
					NGUITools.SetActive(m_debuffIcons[i].gameObject, false);
				}
			}
			return;
		}
		List<string> list = new List<string>();
		foreach (ClipAction clipAction in ClipActionList)
		{
			if (clipAction.Name == "set_vc_effect" && !clipAction.MarkAsRemove)
			{
				string item = clipAction.VCHUName.ToString();
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
		}
		if (list.Count == 0)
		{
			for (int j = 0; j < m_debuffIcons.Count; j++)
			{
				if (m_debuffIcons[j] != null)
				{
					NGUITools.SetActive(m_debuffIcons[j].gameObject, false);
				}
			}
			return;
		}
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		UIAtlas uIAtlas = null;
		if (screenBattle != null && screenBattle.m_ChiSoHero != null && screenBattle.m_ChiSoHero.m_VCHU1 != null)
		{
			uIAtlas = screenBattle.m_ChiSoHero.m_VCHU1.atlas;
		}
		if (uIAtlas == null)
		{
			return;
		}
		int num = Mathf.Min(list.Count, 3);
		while (m_debuffIcons.Count < num)
		{
			UISprite uISprite = NGUITools.AddSprite(HeroGUIPanelGo, uIAtlas, list[m_debuffIcons.Count]);
			uISprite.depth = 50;
			m_debuffIcons.Add(uISprite);
		}
		float alpha = 0.55f + 0.45f * Mathf.Sin(Time.time * 8f);
		float num2 = 28f;
		for (int k = 0; k < num; k++)
		{
			UISprite uISprite2 = m_debuffIcons[k];
			NGUITools.SetActive(uISprite2.gameObject, true);
			uISprite2.atlas = uIAtlas;
			uISprite2.spriteName = list[k];
			float x = ((float)k - ((float)num - 1f) * 0.5f) * 32f;
			uISprite2.transform.localPosition = new Vector3(x, 30f, -1f);
			uISprite2.transform.localScale = new Vector3(num2, num2, 1f);
			uISprite2.alpha = alpha;
		}
		for (int l = num; l < m_debuffIcons.Count; l++)
		{
			if (m_debuffIcons[l] != null)
			{
				NGUITools.SetActive(m_debuffIcons[l].gameObject, false);
			}
		}
	}

	private void DrawHPBar()
	{
		if (GUIPanel != null && GUIPanel.SpriteHPBar != null)
		{
			GUIPanel.SpriteHPBar.fillAmount = ChiSo.HP / ChiSo.HPMax;
		}
	}

	private void PlayEffect(RepVcEffect e)
	{
		if (e.EFFECT.TD != null)
		{
			if (e.EFFECT.TD.TTData.Ne)
			{
				PlayClip_Particle("FX/Prefabs/EFF_NE", 0f, 2f, true, Vector3.zero, Vector3.zero, float.MaxValue, false);
			}
			else
			{
				ChiSo.HP = e.EFFECT.TD.HP;
				if (!GameManager.instance.Trailer)
				{
					SetDmgStr(Mathf.RoundToInt(e.EFFECT.TD.TTData.HpMatDi).ToString(), false, e.EFFECT.TD.TTData.Cr);
				}
				PlayClip_Particle("FX/Prefabs/VC_IMPACT", 0f, 1.2f, true, new Vector3(0f, 0.9f, 0f), Vector3.zero, float.MaxValue, false);
				if (e.EFFECT.TD.TTData.Cr)
				{
					PlayClip_Particle("FX/Prefabs/EFF_BAO_KICH", 0f, 2f, true, Vector3.zero, Vector3.zero, float.MaxValue, false);
				}
				if (e.EFFECT.TD.TTData.Do)
				{
					PlayClip_Particle("FX/Prefabs/EFF_DO", 0f, 2f, true, Vector3.zero, Vector3.zero, float.MaxValue, false);
				}
			}
		}
		if (e.EFFECT.VCHD != null)
		{
			PlayClip_SetVCHU(e.EFFECT.VCHD.ID, e.EFFECT.VCHD.Type, float.MaxValue);
		}
	}

	public override void OnGotoSuccess()
	{
		base.OnGotoSuccess();
	}

	private BattleHero SearchHero(int HID)
	{
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		return screenBattle.GetHero(HID);
	}

	public void SetPause(bool fr)
	{
		if (!fr || (m_state != HeroState.DEAD && SpawnInfo.Type != HeroType.DongNhanCoc))
		{
			base.Pause = fr;
			m_avatar.GetComponent<Animation>().enabled = !base.Pause;
			m_animationController.GetComponent<Animation>().enabled = !base.Pause;
			if (fr)
			{
				m_animationController.gatherDeltaTimeAutomatically = false;
			}
			else
			{
				m_animationController.gatherDeltaTimeAutomatically = true;
			}
		}
	}

	private float GetVCTimeDamage(string name)
	{
		return 0.5f;
	}

	public void ActiveHpBar(bool r)
	{
		GUIPanel.SpriteHPBarBkg.gameObject.SetActive(r);
		GUIPanel.SpriteHPBar.gameObject.SetActive(r);
	}

	public void SetVisible(bool v)
	{
		if (v)
		{
			Utils.SetLayer(base.gameObject.transform, "GUI3D", true);
		}
		else
		{
			Utils.SetLayer(base.gameObject.transform, "BattleInvisible", true);
		}
		ActiveHpBar(v);
	}

	public void DauNoiCong()
	{
		GUIPanel.gameObject.SetActive(true);
		if (m_goVk != null)
		{
			m_goVk.SetActive(false);
		}
		base.gameObject.SetActive(true);
		if (GUIPanel.SpriteHPBar != null)
		{
			GUIPanel.SpriteHPBar.color = ((m_spawnInfo.TeamID != 0) ? Color.red : Color.green);
		}
		SetVisible(true);
		StartCoroutine(CoDauNoiCong());
	}

	private IEnumerator CoDauNoiCong()
	{
		m_bDauNoiLucStart = true;
		ClearActionList();
		SetPause(false);
		bool check_khoe_ko = ChiSo.HP / ChiSo.HPMax > 0.5f;
		IndexAnimDauNC = UnityEngine.Random.Range(0, 3);
		TAnimDauNC = ((!check_khoe_ko) ? "VC_DAU_NOI_LUC_YEU_" : "VC_DAU_NOI_LUC_KHOE_");
		StartAnim(TAnimDauNC + IndexAnimDauNC);
		ScreenBattle screen = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		while (!screen.DauNCKetThuc)
		{
			float r = UnityEngine.Random.Range(1f, 3f);
			yield return new WaitForSeconds(r);
			int prevIndex = IndexAnimDauNC;
			IndexAnimDauNC++;
			IndexAnimDauNC %= 3;
			StartAnim(TAnimDauNC + prevIndex + "_TO_" + TAnimDauNC + IndexAnimDauNC, false);
			yield return new WaitForSeconds(1f);
		}
		yield return null;
	}

	public float DauNCDecHP(float hp)
	{
		float result = 0f;
		if (ChiSo.HP >= hp)
		{
			ChiSo.HP -= hp;
		}
		else
		{
			result = hp - ChiSo.HP;
			ChiSo.HP = 0f;
		}
		if (ChiSo.HP <= 0f)
		{
			PlayClip_Anim("die", 0f, false);
			ActiveHpBar(false);
			m_state = HeroState.DEAD;
			TweenPosition.Begin(base.gameObject, 0.2f, base.transform.localPosition + new Vector3(2 * ((m_spawnInfo.TeamID == 0) ? 1 : (-1)), 0f, UnityEngine.Random.Range(-0.8f, 0.8f)));
		}
		return result;
	}

	private string GetVCVuKhiAo(VCType vc)
	{
		CfgVoCong value;
		ConfigManager.instance.m_dicVCs.TryGetValue(vc.ToString(), out value);
		if (value != null)
		{
			return value.YeuCauVuKhi;
		}
		return string.Empty;
	}

	public void DisplayChiSo()
	{
		if (!SpawnInfo.ChoXemChiSo)
		{
			return;
		}
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		NhanVatCfg value;
		ConfigManager.instance.m_dicNhanVats.TryGetValue(m_spawnInfo.Name, out value);
		screenBattle.m_ChiSoHero.m_TenNhanVatLabel.text = ((value == null) ? string.Empty : value.TenHienThi);
		if (m_spawnInfo.TeamID == 0)
		{
			NGUITools.SetActive(screenBattle.m_ChiSoHero.m_HPBar_1.gameObject, true);
			NGUITools.SetActive(screenBattle.m_ChiSoHero.m_HPBar_2.gameObject, false);
			screenBattle.m_ChiSoHero.m_HPBar_1.fillAmount = ChiSo.HP / ChiSo.HPMax;
		}
		else
		{
			NGUITools.SetActive(screenBattle.m_ChiSoHero.m_HPBar_1.gameObject, false);
			NGUITools.SetActive(screenBattle.m_ChiSoHero.m_HPBar_2.gameObject, true);
			screenBattle.m_ChiSoHero.m_HPBar_2.fillAmount = ChiSo.HP / ChiSo.HPMax;
		}
		string text = string.Format("{0}/{1}", Mathf.RoundToInt(ChiSo.HP), Mathf.RoundToInt(ChiSo.HPMax));
		screenBattle.m_ChiSoHero.m_HPLabel.text = text;
		screenBattle.m_ChiSoHero.m_MPBar.fillAmount = ChiSo.MP / ChiSo.MPMax;
		text = string.Format("{0}/{1}", Mathf.RoundToInt(ChiSo.MP), Mathf.RoundToInt(ChiSo.MPMax));
		screenBattle.m_ChiSoHero.m_MPLabel.text = text;
		screenBattle.m_ChiSoHero.m_CongLabel.text = Mathf.RoundToInt(ChiSo.Cong).ToString();
		screenBattle.m_ChiSoHero.m_ThuLabel.text = Mathf.RoundToInt(ChiSo.Thu).ToString();
		screenBattle.m_ChiSoHero.m_HPRegLabel.text = Mathf.RoundToInt(ChiSo.RegHP).ToString();
		screenBattle.m_ChiSoHero.m_MPRegLabel.text = Mathf.RoundToInt(ChiSo.RegMP).ToString();
		screenBattle.m_ChiSoHero.m_MSLabel.text = ChiSo.MS.ToString("0.00");
		screenBattle.m_ChiSoHero.m_ASLabel.text = ChiSo.AS.ToString("0.00");
		UISprite[] array = new UISprite[4]
		{
			screenBattle.m_ChiSoHero.m_VC1Sprite,
			screenBattle.m_ChiSoHero.m_VC2Sprite,
			screenBattle.m_ChiSoHero.m_VC3Sprite,
			screenBattle.m_ChiSoHero.m_VC4Sprite
		};
		int num = Mathf.Min(m_spawnInfo.VCList.Count, array.Length);
		for (int i = 0; i < num; i++)
		{
			NGUITools.SetActive(array[i].gameObject, true);
			string baseVCName = GetBaseVCName(m_spawnInfo.VCList[i].ToString());
			array[i].spriteName = baseVCName;
		}
		for (int j = num; j < array.Length; j++)
		{
			NGUITools.SetActive(array[j].gameObject, false);
		}
		List<string> list = new List<string>();
		foreach (ClipAction clipAction in ClipActionList)
		{
			if (clipAction.Name == "set_vc_effect")
			{
				list.Add(clipAction.VCHUName.ToString());
			}
		}
		UISprite[] array2 = new UISprite[10]
		{
			screenBattle.m_ChiSoHero.m_VCHU1,
			screenBattle.m_ChiSoHero.m_VCHU2,
			screenBattle.m_ChiSoHero.m_VCHU3,
			screenBattle.m_ChiSoHero.m_VCHU4,
			screenBattle.m_ChiSoHero.m_VCHU5,
			screenBattle.m_ChiSoHero.m_VCHU6,
			screenBattle.m_ChiSoHero.m_VCHU7,
			screenBattle.m_ChiSoHero.m_VCHU8,
			screenBattle.m_ChiSoHero.m_VCHU9,
			screenBattle.m_ChiSoHero.m_VCHU10
		};
		int num2 = Mathf.Min(array2.Length, list.Count);
		for (int k = 0; k < num2; k++)
		{
			UISprite uISprite = array2[k];
			uISprite.spriteName = list[k].ToString();
			NGUITools.SetActive(uISprite.gameObject, true);
		}
		for (int l = num2; l < array2.Length; l++)
		{
			NGUITools.SetActive(array2[l].gameObject, false);
		}
	}

	public void DisplayHoiThoai(string text)
	{
		NGUITools.SetActive(GUIPanel.VCText.gameObject, false);
		NhanVatCfg value;
		ConfigManager.instance.m_dicNhanVats.TryGetValue(SpawnInfo.Name, out value);
		if (value != null)
		{
			GUIPanel.LabelPopUp.text = value.TenHienThi + ":\n" + text;
		}
		else
		{
			GUIPanel.LabelPopUp.text = text;
		}
		NGUITools.SetActive(GUIPanel.LabelPopUp.gameObject, true);
		NGUITools.SetActive(GUIPanel.SpriteBkg.gameObject, true);
		GUIPanel.LabelPopUp.transform.localPosition = new Vector3((0f - GUIPanel.LabelPopUp.relativeSize.x) * GUIPanel.LabelPopUp.transform.localScale.x / 2f, 0f, -2f);
		float num = 15f;
		GUIPanel.SpriteBkg.transform.localPosition = GUIPanel.LabelPopUp.transform.localPosition + new Vector3(0f - num, num, 2f);
		GUIPanel.SpriteBkg.transform.localScale = new Vector3(GUIPanel.LabelPopUp.relativeSize.x * GUIPanel.LabelPopUp.transform.localScale.x + 2f * num, GUIPanel.LabelPopUp.relativeSize.y * GUIPanel.LabelPopUp.transform.localScale.y + 4f * num, 1f);
		StartCoroutine(CoPlayTextHoiThoai(text));
	}

	public void TatHoiThoai()
	{
		NGUITools.SetActive(GUIPanel.LabelPopUp.gameObject, false);
		NGUITools.SetActive(GUIPanel.SpriteBkg.gameObject, false);
	}

	private IEnumerator CoPlayTextHoiThoai(string text)
	{
		NhanVatCfg cfg;
		ConfigManager.instance.m_dicNhanVats.TryGetValue(SpawnInfo.Name, out cfg);
		string ten_nv = ((cfg == null) ? string.Empty : ("[FF0000]" + cfg.TenHienThi + ":[-]\n"));
		string[] words = text.Split(' ');
		int index = 0;
		GUIPanel.LabelPopUp.text = ten_nv;
		for (; index < words.Length; index++)
		{
			UILabel labelPopUp = GUIPanel.LabelPopUp;
			labelPopUp.text = labelPopUp.text + words[index] + " ";
			yield return new WaitForSeconds(0.05f);
		}
		yield return null;
	}

	public void PlayToaSatThuong(VCType vc)
	{
		int length = vc.ToString().Length;
		string text = vc.ToString().Substring(0, length - 2);
		PlayClip_Particle("FX/Prefabs/" + text + "_IMPACT", 0f, 2f, false, base.transform.position, Vector3.zero);
	}

	public void PlayThanThuEff(string vc)
	{
		PlayClip_Particle("FX/Prefabs/" + vc, 0f, 2f, false, base.transform.position, Vector3.zero);
	}

	public void PlayPhanDamage()
	{
		PlayClip_Particle("FX/Prefabs/EFF_PHAN_CHAN", 0f, 3f, false, base.transform.position, Vector3.zero);
	}

	public void Remove()
	{
		if (m_state != HeroState.DEAD)
		{
			PlayClip_Remove(0f, TanAnhHero.ThoiGianSong);
		}
	}

	private void SyncLoad()
	{
		Resources.Load("FX/Prefabs/VC_IMPACT");
		Resources.Load("FX/Sound/CanChien");
		Resources.Load("FX/Sound/DanhChieu");
		Resources.Load("FX/Sound/DanhXa");
		Resources.Load("FX/Sound/DauNoiCong");
		Resources.Load("VuKhi/" + m_spawnInfo.VK);
		Resources.Load("FX/Anim/" + SkillFileStr);
		if (m_spawnInfo.Type != HeroType.NhanVat)
		{
			return;
		}
		foreach (VCType vC in m_spawnInfo.VCList)
		{
			string text = vC.ToString();
			CfgVoCong value;
			ConfigManager.instance.m_dicVCs.TryGetValue(text, out value);
			if (value == null)
			{
				continue;
			}
			if (value.m_Class == VCClass.CHIEU_THUC)
			{
				Resources.Load("FX/Prefabs/" + text);
				Resources.Load("FX/Prefabs/" + text + "_IMPACT");
				Resources.Load("FX/Prefabs/" + text + "_PROJECTILE");
				continue;
			}
			if (text.EndsWith("_S") || text.EndsWith("_A") || text.EndsWith("_B"))
			{
				text = text.Substring(0, text.Length - 2);
			}
			if (text.EndsWith("_SS"))
			{
				text = text.Substring(0, text.Length - 3);
			}
			Resources.Load("FX/Prefabs/" + text);
		}
	}

	private void AsyncLoad()
	{
		EGResourceAsyncLoader.Load("FX/Prefabs/VC_IMPACT");
		EGResourceAsyncLoader.Load("FX/Sound/CanChien");
		EGResourceAsyncLoader.Load("FX/Sound/DanhChieu");
		EGResourceAsyncLoader.Load("FX/Sound/DanhXa");
		EGResourceAsyncLoader.Load("FX/Sound/DauNoiCong");
		EGResourceAsyncLoader.Load("VuKhi/" + m_spawnInfo.VK);
		EGResourceAsyncLoader.Load("FX/Anim/" + SkillFileStr);
		if (m_spawnInfo.Type != HeroType.NhanVat)
		{
			return;
		}
		foreach (VCType vC in m_spawnInfo.VCList)
		{
			string text = vC.ToString();
			CfgVoCong value;
			ConfigManager.instance.m_dicVCs.TryGetValue(text, out value);
			if (value == null)
			{
				continue;
			}
			if (value.m_Class == VCClass.CHIEU_THUC)
			{
				EGResourceAsyncLoader.Load("FX/Prefabs/" + text);
				EGResourceAsyncLoader.Load("FX/Prefabs/" + text + "_IMPACT");
				EGResourceAsyncLoader.Load("FX/Prefabs/" + text + "_PROJECTILE");
				continue;
			}
			if (text.EndsWith("_S") || text.EndsWith("_A") || text.EndsWith("_B"))
			{
				text = text.Substring(0, text.Length - 2);
			}
			if (text.EndsWith("_SS"))
			{
				text = text.Substring(0, text.Length - 3);
			}
			EGResourceAsyncLoader.Load("FX/Prefabs/" + text);
		}
	}

	private IEnumerator CoRepVCEffect(List<BattleReplay.TrangThai.HieuUng> listEffectData)
	{
		if (listEffectData != null)
		{
			float tick = 0f;
			foreach (BattleReplay.TrangThai.HieuUng e in listEffectData)
			{
				float waitTime = e.LT - tick;
				tick = e.LT;
				yield return new WaitForSeconds(waitTime);
				BattleHero hr = SearchHero(e.HID);
				if (hr != null)
				{
					if (e.ByVC == "VC_THIEN_KIEM_CAN_KHON")
					{
						base.transform.localPosition = hr.transform.localPosition;
					}
					hr.ReplayEff(e);
				}
			}
		}
		yield return null;
	}
}

using System.Collections;
using UnityEngine;

public class Avatar3D : MonoBehaviour
{
	public GameObject AvatarGO;

	public GameObject VuKhiGO;

	public GameObject ThuCuoiGO;

	public GameObject ThanThuGO;

	public AnimVuKhi AnimVK;

	public string VuKhiName = string.Empty;

	public string CodeName = string.Empty;

	public string NoiCongName = string.Empty;

	public string BoPhapName = string.Empty;

	public string ThuCuoiName = string.Empty;

	public string CostumeName = string.Empty;

	public string ThanThuName = string.Empty;

	public UserInfo.PetInfo.PetQuality ThanThuQuality;

	public GameObject noiCongParticle;

	public GameObject boPhapParticle;

	private void OnFinishLoadNoiCongParticle(Object o)
	{
		if (this == null)
		{
			return;
		}
		if (o != null)
		{
			Object obj = Object.Instantiate(o);
			GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
			if (gameObject != null)
			{
				if (base.gameObject != null && base.transform != null)
				{
					gameObject.transform.parent = base.transform;
					if (ThuCuoiName == null || ThuCuoiName == string.Empty)
					{
						gameObject.transform.localPosition = Vector3.zero;
					}
					else
					{
						gameObject.transform.localPosition = Vector3.up;
					}
					gameObject.transform.localScale = Vector3.one;
					gameObject.transform.localRotation = Quaternion.identity;
					noiCongParticle = gameObject;
				}
				else
				{
					Object.Destroy(gameObject);
				}
			}
			else
			{
				EGDebug.Log("INVALID prefabs " + o.name);
			}
		}
		else
		{
			EGDebug.Log("Khong load duoc noi cong " + NoiCongName);
		}
	}

	private void OnFinishLoadBoPhapParticle(Object o)
	{
		if (this == null)
		{
			return;
		}
		if (o != null)
		{
			Object obj = Object.Instantiate(o);
			GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
			if (gameObject != null)
			{
				if (base.gameObject != null && base.transform != null)
				{
					gameObject.transform.parent = base.transform;
					gameObject.transform.localPosition = new Vector3(0f, 0.1f, 0f);
					gameObject.transform.localScale = Vector3.one;
					gameObject.transform.localRotation = Quaternion.identity;
					boPhapParticle = gameObject;
				}
				else
				{
					Object.Destroy(gameObject);
				}
			}
			else
			{
				EGDebug.Log("INVALID prefabs " + o.name);
			}
		}
		else
		{
			EGDebug.Log("Khong load duoc bo phap " + BoPhapName);
		}
	}

	public void LoadNoiCong(string noicong)
	{
		string text = ((!string.IsNullOrEmpty(noicong)) ? noicong : string.Empty);
		if (text.EndsWith("_A") || text.EndsWith("_B") || text.EndsWith("_S") || text.EndsWith("_SS"))
		{
			text = text.TrimEnd('_', 'A', 'B', 'S');
		}
		if (!(text == NoiCongName))
		{
			if (noiCongParticle != null)
			{
				Object.Destroy(noiCongParticle);
				noiCongParticle = null;
			}
			if (!string.IsNullOrEmpty(text))
			{
				EGResourceAsyncLoader.Load("fx/prefabs/" + text, true, OnFinishLoadNoiCongParticle);
			}
			NoiCongName = text;
		}
	}

	public void LoadBoPhap(string bophap)
	{
		string text = ((!string.IsNullOrEmpty(bophap)) ? bophap : string.Empty);
		if (text.EndsWith("_A") || text.EndsWith("_B") || text.EndsWith("_S") || text.EndsWith("_SS"))
		{
			text = text.TrimEnd('_', 'A', 'B', 'S');
		}
		if (!(text == BoPhapName))
		{
			if (boPhapParticle != null)
			{
				Object.Destroy(boPhapParticle);
				boPhapParticle = null;
			}
			if (!string.IsNullOrEmpty(text))
			{
				EGResourceAsyncLoader.Load("fx/prefabs/" + text, true, OnFinishLoadBoPhapParticle);
			}
			BoPhapName = text;
		}
	}

	private void OnFinishLoadThuCuoiPrefabs(Object thucuoiGO)
	{
		OtherCfg.ThuCuoiCfg value;
		if (!(AvatarGO != null) || ConfigManager.instance.OtherConfig.ThuCuoiConfig == null || !ConfigManager.instance.OtherConfig.ThuCuoiConfig.TryGetValue(ThuCuoiName, out value))
		{
			return;
		}
		AnimationController component = AvatarGO.GetComponent<AnimationController>();
		if (thucuoiGO == null)
		{
			Object obj = Resources.Load("thucuoi/" + ThuCuoiName, typeof(GameObject));
			thucuoiGO = ((obj is GameObject) ? obj : null);
		}
		if (!(thucuoiGO != null))
		{
			return;
		}
		Object obj2 = Object.Instantiate(thucuoiGO);
		GameObject gameObject = (GameObject)((obj2 is GameObject) ? obj2 : null);
		Transform transform = AvatarGO.transform;
		if (transform != null)
		{
			gameObject.transform.parent = transform.parent;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			ThuCuoiGO = gameObject;
			if (ThuCuoiName.Contains("KY_LAN"))
			{
				if (transform.GetComponent<ThuCuoiSyncMovement>() == null)
				{
					transform.gameObject.AddComponent<ThuCuoiSyncMovement>();
				}
				transform.GetComponent<ThuCuoiSyncMovement>().StartSync(gameObject.transform.Find("jnt_root").Find("jnt_seat"));
			}
			else if (transform.GetComponent<ThuCuoiSyncMovement>() != null)
			{
				transform.gameObject.GetComponent<ThuCuoiSyncMovement>().StopSync();
			}
		}
		else
		{
			Object.Destroy(gameObject);
			ThuCuoiGO = null;
		}
	}

	private void OnFinishLoadVKPrefabs(Object vuKhiGO)
	{
		TrangBiCfg value;
		if (!(AvatarGO != null) || !ConfigManager.instance.m_dicTrangBi.TryGetValue(VuKhiName, out value))
		{
			return;
		}
		AnimationController component = AvatarGO.GetComponent<AnimationController>();
		if (vuKhiGO == null)
		{
			Object obj = Resources.Load("vukhi/" + ConfigManager.GetVuKhiMacDinhTheoAnim(value.Anim), typeof(GameObject));
			vuKhiGO = ((obj is GameObject) ? obj : null);
		}
		if (!(vuKhiGO != null))
		{
			return;
		}
		Object obj2 = Object.Instantiate(vuKhiGO);
		GameObject gameObject = (GameObject)((obj2 is GameObject) ? obj2 : null);
		Transform transform = AvatarGO.transform.Find("jnt_root/jnt_weapon_R").transform;
		if (transform != null)
		{
			gameObject.transform.parent = transform;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			VuKhiGO = gameObject;
			if (component != null)
			{
				WeaponTrail component2 = gameObject.GetComponent<WeaponTrail>();
				component2.SetTime(0f, 0f, 1f);
				component.AddTrail(component2);
			}
		}
		else
		{
			Object.Destroy(gameObject);
			VuKhiGO = null;
		}
	}

	public void LoadThuCuoi(string thuCuoiName)
	{
		if (AvatarGO == null)
		{
			return;
		}
		if (string.IsNullOrEmpty(thuCuoiName))
		{
			ThuCuoiName = string.Empty;
			if (ThuCuoiGO != null)
			{
				Object.Destroy(ThuCuoiGO);
			}
			ThuCuoiGO = null;
		}
		else if (ThuCuoiName != thuCuoiName)
		{
			ThuCuoiName = thuCuoiName;
			if (ThuCuoiGO != null)
			{
				Object.Destroy(ThuCuoiGO);
			}
			ThuCuoiGO = null;
			OtherCfg.ThuCuoiCfg value;
			if (ConfigManager.instance.OtherConfig.ThuCuoiConfig != null && ConfigManager.instance.OtherConfig.ThuCuoiConfig.TryGetValue(thuCuoiName, out value))
			{
				EGResourceAsyncLoader eGResourceAsyncLoader = EGResourceAsyncLoader.Load("thucuoi/" + thuCuoiName, true, OnFinishLoadThuCuoiPrefabs);
			}
		}
	}

	public void LoadThanThu(string codename, UserInfo.PetInfo.PetQuality quality)
	{
		if (!string.IsNullOrEmpty(codename))
		{
			if (ThanThuGO != null && (codename != ThanThuName || quality != ThanThuQuality))
			{
				Object.Destroy(ThanThuGO);
			}
			string text = "_01";
			if (quality == UserInfo.PetInfo.PetQuality.PHO_THONG)
			{
				text = "_01";
			}
			if (quality == UserInfo.PetInfo.PetQuality.UU_TU)
			{
				text = "_01";
			}
			if (quality == UserInfo.PetInfo.PetQuality.TRAC_VIET)
			{
				text = "_02";
			}
			if (quality == UserInfo.PetInfo.PetQuality.HUYEN_THOAI)
			{
				text = "_03";
			}
			if (quality == UserInfo.PetInfo.PetQuality.TRUYEN_THUYET)
			{
				text = "_04";
			}
			ThanThuName = codename;
			ThanThuQuality = quality;
			ThanThuGO = (GameObject)Object.Instantiate(Resources.Load("thanthu/" + codename + text));
			if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenCT2)
			{
				ThanThuGO.transform.parent = base.transform.parent.parent;
			}
			else
			{
				ThanThuGO.transform.parent = base.transform.parent;
			}
			ThanThuGO.transform.position = base.transform.position + Vector3.right;
			ThanThuGO.transform.localScale = Vector3.one;
			ThanThuGO.GetComponent<ThanThuController>().nav.enabled = true;
			ThanThuGO.GetComponent<ThanThuController>().owner = base.gameObject;
		}
	}

	public void LoadVK(string vkName)
	{
		if (AvatarGO == null)
		{
			return;
		}
		if (string.IsNullOrEmpty(vkName))
		{
			VuKhiName = string.Empty;
			if (VuKhiGO != null)
			{
				Object.Destroy(VuKhiGO);
			}
			AnimVK = AnimVuKhi.NV_BaoTay;
			VuKhiGO = null;
		}
		else
		{
			if (!(VuKhiName != vkName))
			{
				return;
			}
			VuKhiName = vkName;
			AnimVK = AnimVuKhi.NV_BaoTay;
			if (VuKhiGO != null)
			{
				Object.Destroy(VuKhiGO);
			}
			VuKhiGO = null;
			TrangBiCfg value;
			if (!ConfigManager.instance.m_dicTrangBi.TryGetValue(vkName, out value))
			{
				return;
			}
			AnimVK = value.GetAnimVK();
			if (AnimVK == AnimVuKhi.NV_AmKhi)
			{
				if (VuKhiGO != null)
				{
					Object.Destroy(VuKhiGO);
				}
				VuKhiGO = null;
			}
			else
			{
				EGResourceAsyncLoader eGResourceAsyncLoader = EGResourceAsyncLoader.Load("vukhi/" + vkName, true, OnFinishLoadVKPrefabs);
			}
		}
	}

	public void PlayAnimDauNoiLuc()
	{
		string clip_name = "VC_DAU_NOI_LUC_KHOE_2";
		PlayAnimBattle(clip_name, true);
	}

	public void PlayAnimIdle()
	{
		string text = "idle_incity_" + ConfigManager.GetEnglishAnimName(AnimVK).ToLower();
		if (!string.IsNullOrEmpty(ThuCuoiName))
		{
			text = "horse_" + text;
			PlayAnimThuCuoi("horse_idle", true);
		}
		PlayAnimBattle(text, true);
	}

	public void FadingInIdle(float fadeLength)
	{
		string text = "idle_incity_" + ConfigManager.GetEnglishAnimName(AnimVK).ToLower();
		if (!string.IsNullOrEmpty(ThuCuoiName))
		{
			text = "horse_" + text;
			FadeAnimThuCuoi("horse_idle", true, fadeLength);
		}
		FadeAnimBattle(text, true, fadeLength);
	}

	public void FadingInRun(float fadeLength)
	{
		string text = "run_incity_" + ConfigManager.GetEnglishAnimName(AnimVK).ToLower();
		if (!string.IsNullOrEmpty(ThuCuoiName))
		{
			text = "horse_" + text.Replace("run_incity_", "ride_incity_");
			FadeAnimThuCuoi("horse_run", true, fadeLength);
		}
		FadeAnimBattle(text, true, fadeLength);
	}

	private void PlayAnim(Animation anim, string clip_name, bool isLoop)
	{
		if (isLoop)
		{
			anim.wrapMode = WrapMode.Loop;
		}
		else
		{
			anim.wrapMode = WrapMode.Default;
		}
		anim.Play(clip_name);
	}

	private void QueueAnim(Animation anim, string clip_name, bool isLoop)
	{
		if (isLoop)
		{
			anim.wrapMode = WrapMode.Loop;
		}
		else
		{
			anim.wrapMode = WrapMode.Default;
		}
		anim.PlayQueued(clip_name);
	}

	private void FadeQueueAnim(Animation anim, string clip_name, bool isLoop, float fadeLength)
	{
		anim.CrossFadeQueued(clip_name, fadeLength, QueueMode.CompleteOthers, PlayMode.StopSameLayer);
		if (isLoop)
		{
			anim.wrapMode = WrapMode.Loop;
		}
		else
		{
			anim.wrapMode = WrapMode.Default;
		}
	}

	private void FadeAnim(Animation anim, string clip_name, bool isLoop, float fadeLength)
	{
		if (isLoop)
		{
			anim.wrapMode = WrapMode.Loop;
		}
		else
		{
			anim.wrapMode = WrapMode.Default;
		}
		anim.CrossFade(clip_name, fadeLength);
	}

	private IEnumerator DeLayPlayAnimForLoading(string clip_name, bool isLoop)
	{
		while (AvatarGO == null)
		{
			yield return null;
		}
		Animation anim = AvatarGO.GetComponent<Animation>();
		PlayAnim(anim, clip_name, isLoop);
	}

	private IEnumerator DeLayPlayAnimThuCuoiForLoading(string clip_name, bool isLoop)
	{
		while (ThuCuoiGO == null)
		{
			yield return null;
		}
		Animation anim = ThuCuoiGO.GetComponent<Animation>();
		PlayAnim(anim, clip_name, isLoop);
	}

	private IEnumerator DeLayQueueAnimForLoading(string clip_name, bool isLoop)
	{
		while (AvatarGO == null)
		{
			yield return null;
		}
		Animation anim = AvatarGO.GetComponent<Animation>();
		QueueAnim(anim, clip_name, isLoop);
	}

	private IEnumerator DeLayFadeQueueAnimForLoading(string clip_name, bool isLoop, float fadeLength)
	{
		while (AvatarGO == null)
		{
			yield return null;
		}
		Animation anim = AvatarGO.GetComponent<Animation>();
		FadeQueueAnim(anim, clip_name, isLoop, fadeLength);
	}

	private IEnumerator DeLayFadeAnimForLoading(string clip_name, bool isLoop, float fadeLength)
	{
		while (AvatarGO == null)
		{
			yield return null;
		}
		Animation anim = AvatarGO.GetComponent<Animation>();
		FadeAnim(anim, clip_name, isLoop, fadeLength);
	}

	private IEnumerator DeLayFadeAnimThuCuoiForLoading(string clip_name, bool isLoop, float fadeLength)
	{
		while (ThuCuoiGO == null && !string.IsNullOrEmpty(ThuCuoiName))
		{
			yield return null;
		}
		Animation anim = ThuCuoiGO.GetComponent<Animation>();
		FadeAnim(anim, clip_name, isLoop, fadeLength);
	}

	public void FadeAnimBattle(string clip_name, bool isLoop, float fadeLength)
	{
		if (AvatarGO == null)
		{
			StartCoroutine(DeLayFadeAnimForLoading(clip_name, isLoop, fadeLength));
			return;
		}
		Animation component = AvatarGO.GetComponent<Animation>();
		FadeAnim(component, clip_name, isLoop, fadeLength);
	}

	public void FadeAnimThuCuoi(string clip_name, bool isLoop, float fadeLength)
	{
		if (ThuCuoiGO == null && !string.IsNullOrEmpty(ThuCuoiName))
		{
			StartCoroutine(DeLayFadeAnimThuCuoiForLoading(clip_name, isLoop, fadeLength));
		}
		else if (ThuCuoiGO != null)
		{
			Animation component = ThuCuoiGO.GetComponent<Animation>();
			FadeAnim(component, clip_name, isLoop, fadeLength);
		}
	}

	public void FadeQueuedAnimBattle(string clip_name, bool isLoop, float fadeLength)
	{
		if (AvatarGO == null && base.gameObject.activeSelf)
		{
			StartCoroutine(DeLayFadeQueueAnimForLoading(clip_name, isLoop, fadeLength));
		}
		else if (AvatarGO != null)
		{
			Animation component = AvatarGO.GetComponent<Animation>();
			FadeQueueAnim(component, clip_name, isLoop, fadeLength);
		}
	}

	public void PlayAnimBattle(string clip_name, bool isLoop)
	{
		if (AvatarGO == null)
		{
			StartCoroutine(DeLayPlayAnimForLoading(clip_name, isLoop));
		}
		else if (AvatarGO != null)
		{
			Animation component = AvatarGO.GetComponent<Animation>();
			PlayAnim(component, clip_name, isLoop);
		}
	}

	public void PlayAnimThuCuoi(string clip_name, bool isLoop)
	{
		if (ThuCuoiGO == null)
		{
			StartCoroutine(DeLayPlayAnimThuCuoiForLoading(clip_name, isLoop));
			return;
		}
		Animation component = ThuCuoiGO.GetComponent<Animation>();
		PlayAnim(component, clip_name, isLoop);
	}

	public void PlayQueuedAnimBattle(string clip_name, bool isLoop)
	{
		if (AvatarGO == null)
		{
			StartCoroutine(DeLayQueueAnimForLoading(clip_name, isLoop));
			return;
		}
		Animation component = AvatarGO.GetComponent<Animation>();
		QueueAnim(component, clip_name, isLoop);
	}

	public void PlayAnimNoLe()
	{
		NhanVatCfg value = null;
		if (ConfigManager.instance.m_dicNhanVats.TryGetValue(CodeName, out value))
		{
			if (value.Sex == NhanVatCfg.GioiTinh.Nam)
			{
				PlayAnimBattle("tieunhi_idle", true);
			}
			else
			{
				PlayAnimBattle("kinu_idle", true);
			}
		}
	}

	public void FadeInWalkAnim(float fadeLength)
	{
		NhanVatCfg value = null;
		if (ConfigManager.instance.m_dicNhanVats.TryGetValue(CodeName, out value))
		{
			if (value.Sex == NhanVatCfg.GioiTinh.Nam)
			{
				FadeAnimBattle("male_walk", true, fadeLength);
			}
			else
			{
				FadeAnimBattle("female_walk", true, fadeLength);
			}
		}
		else
		{
			EGDebug.Log("Khong load dc config " + CodeName);
		}
	}

	private void Update()
	{
		if (noiCongParticle != null)
		{
			if (ThuCuoiName == null || ThuCuoiName == string.Empty)
			{
				noiCongParticle.transform.localPosition = Vector3.zero;
			}
			else
			{
				noiCongParticle.transform.localPosition = Vector3.up;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class CT2BasePlayer : MonoBehaviour
{
	private bool mIsVisible;

	private GameObject GUIPanelGo;

	public GameObject GUIPivot;

	public CT2GUIPanel GUIPanel;

	private UIFollowTarget m_followTar;

	private GameObject m_particleOb;

	public Avatar3D m_avatar;

	public CT2EntityState State;

	public CT2EntityState PrevState;

	private ChienTruongChinhTa.RuneType _rune;

	private GameObject runeParticle;

	private float _TyleHp = 1f;

	public ChienTruongChinhTa.NguoiChoi Info { get; set; }

	public int _giaTriSinhMenh { get; set; }

	public bool _mainPlayerChinhPhai { get; set; }

	public ChienTruongChinhTa.RuneType Rune
	{
		get
		{
			return _rune;
		}
		private set
		{
			_rune = value;
		}
	}

	public float TyleHp
	{
		get
		{
			return _TyleHp;
		}
		set
		{
			_TyleHp = value;
		}
	}

	public virtual void ShowName(int giaTriSinhMenh, bool mainPlayerChinhPhai, bool isMount = false)
	{
	}

	public virtual void ReShowName()
	{
	}

	public virtual void SetState(CT2EntityState state)
	{
		PrevState = State;
		State = state;
		switch (state)
		{
		case CT2EntityState.ATTACK:
			PlayParticle("FX/Prefabs/MISC_FIGHTING");
			break;
		case CT2EntityState.SAFE:
			if (PrevState != CT2EntityState.SAFE)
			{
				PlayParticle("FX/Prefabs/MISC_SAFE");
			}
			GUIPanel.NameLabel.text = Localization.instance.Get("DangTriThuong");
			break;
		default:
			KetThucTriThuong();
			RemoveParticle();
			break;
		}
	}

	public virtual void GotRune(ChienTruongChinhTa.RuneType rune, float tyleHp)
	{
		SetTyleHp(tyleHp);
		if (_rune != rune)
		{
			_rune = rune;
			if (runeParticle != null)
			{
				UnityEngine.Object.Destroy(runeParticle);
			}
			switch (rune)
			{
			case ChienTruongChinhTa.RuneType.Khi:
			{
				UnityEngine.Object obj3 = UnityEngine.Object.Instantiate(Resources.Load("fx/prefabs/RUNES_KHI_AURA"));
				runeParticle = (GameObject)((obj3 is GameObject) ? obj3 : null);
				runeParticle.transform.parent = base.transform;
				runeParticle.transform.localScale = Vector3.one;
				runeParticle.transform.localPosition = Vector3.zero;
				break;
			}
			case ChienTruongChinhTa.RuneType.Ngoai:
			{
				UnityEngine.Object obj2 = UnityEngine.Object.Instantiate(Resources.Load("fx/prefabs/RUNES_NGOAI_AURA"));
				runeParticle = (GameObject)((obj2 is GameObject) ? obj2 : null);
				runeParticle.transform.parent = base.transform;
				runeParticle.transform.localScale = Vector3.one;
				runeParticle.transform.localPosition = Vector3.zero;
				break;
			}
			case ChienTruongChinhTa.RuneType.Than:
			{
				UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("fx/prefabs/RUNES_THAN_AURA"));
				runeParticle = (GameObject)((obj is GameObject) ? obj : null);
				runeParticle.transform.parent = base.transform;
				runeParticle.transform.localScale = Vector3.one;
				runeParticle.transform.localPosition = Vector3.zero;
				break;
			}
			}
		}
	}

	private void Start()
	{
	}

	public virtual void Update()
	{
		if (GUIPanel != null && Camera.main != null && base.gameObject != null)
		{
			GUIPanel.HPBarSpr.fillAmount = TyleHp;
			Vector3 vector = Camera.main.WorldToViewportPoint(base.gameObject.transform.position);
			bool flag = vector.z > 0f && vector.x > 0f && vector.x < 1f && vector.y > 0f && vector.y < 1f;
			if (mIsVisible != flag)
			{
				GUIPanel.gameObject.SetActive(flag);
				mIsVisible = flag;
			}
		}
	}

	public void SetAvatarBoss(string codeName, string vuKhi = "")
	{
		m_avatar = GUIManager.instance.InstantiateAvatar3D(codeName, vuKhi, string.Empty, string.Empty, new List<string> { "VC_DAU_NOI_LUC_KHOE_2" }, null, string.Empty, string.Empty, string.Empty);
		m_avatar.transform.parent = base.transform;
		m_avatar.transform.localPosition = Vector3.zero;
		m_avatar.transform.localRotation = Quaternion.identity;
		base.transform.localScale = Vector3.one * Info.TyleModel;
	}

	public void SetAvatar(string codeName, string vuKhi = "", string bophap = "", string noicong = "", string thuCuoi = "", string costume = "", string thanthuName = "", UserInfo.PetInfo.PetQuality thanthuQuality = UserInfo.PetInfo.PetQuality.PHO_THONG)
	{
		m_avatar = GUIManager.instance.InstantiateAvatar3D(codeName, vuKhi, bophap, noicong, null, new List<string> { "die" }, thuCuoi, costume, thanthuName, thanthuQuality);
		m_avatar.transform.parent = base.transform;
		m_avatar.transform.localPosition = Vector3.zero;
		m_avatar.transform.localRotation = Quaternion.identity;
		base.transform.localScale = Vector3.one * Info.TyleModel;
	}

	public virtual void Teleport(float x, float z)
	{
	}

	public virtual void PosFSetTarget_Attack(float pos_x, float pos_z)
	{
	}

	public virtual void PosFSetTarget_Idle(float pos_x, float pos_z)
	{
	}

	public virtual void PosFSetTarget(float pos_x, float pos_z, float vel_x, float vel_z)
	{
	}

	public virtual void PosFSetTarget(float pos_x, float pos_z, float vel_x, float vel_z, byte state)
	{
	}

	private void OnDestroy()
	{
		if (GUIPanelGo != null)
		{
			UnityEngine.Object.Destroy(GUIPanelGo);
		}
		GUIPanelGo = null;
	}

	public void InitGUI()
	{
		UnityEngine.Object obj = Resources.Load("prefabs/chientruong/PlayerGUIPanel", typeof(GameObject));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (gameObject != null)
		{
			m_followTar = gameObject.GetComponent<UIFollowTarget>();
			m_followTar.uiCamera = GUIManager.instance.cam2D;
			m_followTar.gameCamera = Camera.main;
			m_followTar.target = GUIPivot.transform;
			m_followTar.disableIfInvisible = false;
			UnityEngine.Object obj2 = UnityEngine.Object.Instantiate(gameObject);
			GUIPanelGo = (GameObject)((obj2 is GameObject) ? obj2 : null);
			ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
			GUIPanelGo.transform.parent = screenCT.transform;
			GUIPanelGo.transform.localPosition = new Vector3(1000f, 1000f, 0f);
			GUIPanelGo.transform.localRotation = Quaternion.identity;
			GUIPanelGo.transform.localScale = Vector3.one;
			GUIPanel = GUIPanelGo.GetComponent<CT2GUIPanel>();
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi > 0 && GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Exists((UserInfo.ThuCuoiData e) => e.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.curThuCuoi && e.ExpiredTime > GameManager.instance.m_GameClient.ServerTime))
			{
				GUIPanel.NameLabel.transform.localPosition = new Vector3(GUIPanel.NameLabel.transform.localPosition.x, 75f, GUIPanel.NameLabel.transform.localPosition.z);
				GUIPanel.HPBarSpr.transform.localPosition = new Vector3(GUIPanel.HPBarSpr.transform.localPosition.x, 100f, GUIPanel.HPBarSpr.transform.localPosition.z);
				GUIPanel.HPBarBkgSpr.transform.localPosition = new Vector3(GUIPanel.HPBarBkgSpr.transform.localPosition.x, 100f, GUIPanel.HPBarBkgSpr.transform.localPosition.z);
			}
			GUIPanel.gameObject.SetActive(true);
		}
	}

	public virtual void PlayParticle(string prefab)
	{
		try
		{
			if (m_particleOb != null)
			{
				UnityEngine.Object.Destroy(m_particleOb);
			}
			UnityEngine.Object obj = Resources.Load(prefab);
			GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
			if (gameObject != null)
			{
				UnityEngine.Object obj2 = UnityEngine.Object.Instantiate(gameObject);
				m_particleOb = (GameObject)((obj2 is GameObject) ? obj2 : null);
				if (m_particleOb != null)
				{
					m_particleOb.transform.parent = base.transform;
					m_particleOb.transform.localPosition = new Vector3(0f, 0f, 0f);
					m_particleOb.transform.localScale = Vector3.one;
				}
			}
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}

	public virtual void RemoveParticle()
	{
		if (m_particleOb != null)
		{
			UnityEngine.Object.Destroy(m_particleOb);
		}
		m_particleOb = null;
	}

	public void TriThuong()
	{
	}

	public void KetThucTriThuong()
	{
		ReShowName();
	}

	public virtual void SetTyleHp(float tyle)
	{
		TyleHp = tyle;
	}

	public CT2BasePlayer SearchEnemyBoss()
	{
		ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
		ScreenChienTruong3D screenChienTruong3D = screenCT.Get3DCom();
		foreach (CT2BasePlayer player in screenChienTruong3D.PlayerList)
		{
			if (player != this && player.Info.TyleModel > 1.9f)
			{
				return player;
			}
		}
		return null;
	}
}

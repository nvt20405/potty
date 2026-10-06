using Nettention.Proud;
using UnityEngine;

public class CT2Player3 : CT2BasePlayer
{
	private double _timeUpdate;

	internal PositionFollower m_positionFollower = new PositionFollower();

	public PositionFollower Follower
	{
		get
		{
			return m_positionFollower;
		}
		set
		{
			m_positionFollower = value;
		}
	}

	private void Start()
	{
		m_positionFollower.EnableAutoFollowDuration = true;
		m_positionFollower.WarpThreshold = 1.0;
	}

	public override void ShowName(int giaTriSinhMenh, bool mainPlayerChinhPhai, bool isMount = false)
	{
		if (isMount)
		{
			GUIPanel.NameLabel.transform.localPosition = new UnityEngine.Vector3(GUIPanel.NameLabel.transform.localPosition.x, 75f, GUIPanel.NameLabel.transform.localPosition.z);
			GUIPanel.HPBarSpr.transform.localPosition = new UnityEngine.Vector3(GUIPanel.HPBarSpr.transform.localPosition.x, 100f, GUIPanel.HPBarSpr.transform.localPosition.z);
			GUIPanel.HPBarBkgSpr.transform.localPosition = new UnityEngine.Vector3(GUIPanel.HPBarBkgSpr.transform.localPosition.x, 100f, GUIPanel.HPBarBkgSpr.transform.localPosition.z);
		}
		base._mainPlayerChinhPhai = mainPlayerChinhPhai;
		base._giaTriSinhMenh = giaTriSinhMenh;
		if (State == CT2EntityState.SAFE)
		{
			GUIPanel.NameLabel.text = Localization.instance.Get("DangTriThuong");
			return;
		}
		string empty = string.Empty;
		empty = Utils.getStringNameByKhiThe(base.Info.KhiThe) + " ";
		empty = ((mainPlayerChinhPhai == base.Info.ChinhPhai || !((double)base.Info.TyleModel < 1.5)) ? ((base.Info.SID <= 0) ? (base.Info.Ten ?? "") : string.Format("s{0}.{1}", base.Info.SID, base.Info.Ten)) : ((base.Info.SID <= 0) ? string.Format("{0} ([ff0000]{1}[-])", base.Info.Ten, giaTriSinhMenh) : (empty + string.Format("s{0}.{1} ([ff0000]{2}[-])", base.Info.SID, base.Info.Ten, giaTriSinhMenh))));
		GUIPanel.NameLabel.text = empty;
	}

	public override void ReShowName()
	{
		if (string.IsNullOrEmpty(m_avatar.ThuCuoiName))
		{
			ShowName(base._giaTriSinhMenh, base._mainPlayerChinhPhai);
		}
		else
		{
			ShowName(base._giaTriSinhMenh, base._mainPlayerChinhPhai, true);
		}
	}

	public override void Update()
	{
		base.Update();
		m_positionFollower.FrameMove(Time.deltaTime);
		base.transform.localPosition = new UnityEngine.Vector3((float)m_positionFollower.FollowerPosition.x, (float)m_positionFollower.FollowerPosition.y + 0.05f, (float)m_positionFollower.FollowerPosition.z);
		CT2EntityState state = State;
		if (state != CT2EntityState.ATTACK || base.Info == null || !(base.Info.TyleModel > 1.9f))
		{
			return;
		}
		_timeUpdate += Time.deltaTime;
		if (_timeUpdate > 2.5)
		{
			_timeUpdate = 0.0;
			CT2BasePlayer cT2BasePlayer = SearchEnemyBoss();
			if (cT2BasePlayer != null)
			{
				base.transform.LookAt(cT2BasePlayer.transform.position);
			}
		}
	}

	public override void Teleport(float x, float z)
	{
		Nettention.Proud.Vector3 position = new Nettention.Proud.Vector3
		{
			x = x,
			z = z
		};
		Nettention.Proud.Vector3 velocity = new Nettention.Proud.Vector3
		{
			x = 0.0,
			y = 0.0,
			z = 0.0
		};
		m_positionFollower.SetFollower(position, velocity);
		m_positionFollower.SetTarget(position, velocity);
		m_avatar.FadingInIdle(0.2f);
	}

	public override void PosFSetTarget(float pos_x, float pos_z, float vel_x, float vel_z)
	{
		Nettention.Proud.Vector3 position = new Nettention.Proud.Vector3
		{
			x = pos_x,
			z = pos_z
		};
		Nettention.Proud.Vector3 vector = new Nettention.Proud.Vector3
		{
			x = vel_x,
			z = vel_z
		};
		m_positionFollower.FollowerVelocity = vector;
		m_positionFollower.SetTarget(position, vector);
		if (vector.Length > 0.0)
		{
			m_avatar.FadingInRun(0.2f);
			UnityEngine.Vector3 vector2 = default(UnityEngine.Vector3);
			vector2 = new UnityEngine.Vector3((float)vector.x, (float)vector.y, (float)vector.z);
			UnityEngine.Vector3 forward = base.transform.parent.transform.TransformDirection(vector2);
			base.transform.rotation = Quaternion.LookRotation(forward);
		}
		else
		{
			m_avatar.FadingInIdle(0.2f);
		}
	}

	public override void PosFSetTarget(float pos_x, float pos_z, float vel_x, float vel_z, byte state)
	{
		Nettention.Proud.Vector3 position = new Nettention.Proud.Vector3
		{
			x = pos_x,
			z = pos_z
		};
		Nettention.Proud.Vector3 vector = new Nettention.Proud.Vector3
		{
			x = vel_x,
			z = vel_z
		};
		m_positionFollower.FollowerVelocity = vector;
		m_positionFollower.SetTarget(position, vector);
		if (vector.Length > 0.0)
		{
			if (state == 3)
			{
				m_avatar.FadeInWalkAnim(0.2f);
			}
			else
			{
				m_avatar.FadingInRun(0.2f);
			}
			UnityEngine.Vector3 vector2 = default(UnityEngine.Vector3);
			vector2 = new UnityEngine.Vector3((float)vector.x, (float)vector.y, (float)vector.z);
			UnityEngine.Vector3 forward = base.transform.parent.transform.TransformDirection(vector2);
			base.transform.rotation = Quaternion.LookRotation(forward);
		}
		else
		{
			m_positionFollower.SetFollower(position, vector);
			m_avatar.FadingInIdle(0.2f);
		}
	}

	public override void PosFSetTarget_Idle(float pos_x, float pos_z)
	{
		Nettention.Proud.Vector3 position = new Nettention.Proud.Vector3
		{
			x = pos_x,
			z = pos_z
		};
		Nettention.Proud.Vector3 velocity = new Nettention.Proud.Vector3
		{
			x = 0.0,
			z = 0.0
		};
		m_positionFollower.SetFollower(position, velocity);
		m_positionFollower.SetTarget(position, velocity);
		m_avatar.FadingInIdle(0.2f);
	}

	public override void PosFSetTarget_Attack(float pos_x, float pos_z)
	{
		Nettention.Proud.Vector3 position = new Nettention.Proud.Vector3
		{
			x = pos_x,
			z = pos_z
		};
		Nettention.Proud.Vector3 velocity = new Nettention.Proud.Vector3
		{
			x = 0.0,
			z = 0.0
		};
		m_positionFollower.SetFollower(position, velocity);
		m_positionFollower.SetTarget(position, velocity);
		m_avatar.PlayAnimDauNoiLuc();
		State = CT2EntityState.ATTACK;
		ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
		ScreenChienTruong3D screenChienTruong3D = screenCT.Get3DCom();
		screenChienTruong3D.PlayParticle_BossDauNC();
	}

	public override void SetState(CT2EntityState state)
	{
		base.SetState(state);
		switch (state)
		{
		case CT2EntityState.IDLE:
			StartIdle();
			break;
		case CT2EntityState.ATTACK:
			PlayParticle("FX/Prefabs/MISC_FIGHTING");
			StartIdle();
			break;
		case CT2EntityState.SAFE:
			TriThuong();
			StartIdle();
			break;
		default:
			KetThucTriThuong();
			RemoveParticle();
			break;
		}
	}

	private void StartIdle()
	{
		Nettention.Proud.Vector3 vector = new Nettention.Proud.Vector3
		{
			x = 0.0,
			z = 0.0
		};
		m_positionFollower.FollowerVelocity = vector;
		m_positionFollower.TargetVelocity = vector;
		m_avatar.FadingInIdle(0.1f);
	}
}

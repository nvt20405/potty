using System;
using Nettention.Proud;
using UnityEngine;

public class CT2Player1 : CT2BasePlayer
{
	private RaycastHit mousePoint;

	private bool pressed;

	private bool unpressed;

	private NavMeshAgent m_navAgent;

	private float m_timeUpdateMove;

	private CT2BasePlayer boss;

	private void Awake()
	{
		m_navAgent = GetComponent<NavMeshAgent>();
	}

	private void Start()
	{
	}

	public override void Update()
	{
		base.Update();
		if (!m_navAgent.hasPath || m_navAgent.remainingDistance <= 0.1f)
		{
			m_avatar.FadingInIdle(0.2f);
		}
		if (Input.GetMouseButtonDown(0))
		{
			unpressed = false;
			if (UICamera.hoveredObject == null)
			{
				pressed = true;
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			unpressed = true;
			pressed = false;
		}
		if (!GameManager.instance.m_GameClient.isAutoChienTruong)
		{
			if (pressed && !unpressed && mousePoint.collider != null && mousePoint.collider.gameObject.layer == 30)
			{
				MoveTo(mousePoint.point);
			}
		}
		else
		{
			if (boss == null)
			{
				ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
				ScreenChienTruong3D screenChienTruong3D = screenCT.Get3DCom();
				if (screenChienTruong3D.SearchPlayer(GameManager.instance.m_GameClient.UserInfo.Gamer.ID, GameManager.instance.m_GameClient.UserInfo.ServerInfo.ID).Info.ChinhPhai)
				{
					boss = screenChienTruong3D.SearchPlayer(-200, 0);
				}
				else
				{
					boss = screenChienTruong3D.SearchPlayer(-100, 0);
				}
			}
			MoveTo(boss.transform.position);
		}
		m_timeUpdateMove += Time.deltaTime;
		if (m_timeUpdateMove >= ((ConfigManager.instance.OtherConfig == null) ? 0.15f : ConfigManager.instance.OtherConfig.CT2TimeNotifyPos))
		{
			m_timeUpdateMove = 0f;
			if (ChkReconnect())
			{
				GameManager.instance.m_GameClient.ReConnect();
				return;
			}
			UnityEngine.Vector3 vector = base.transform.parent.transform.InverseTransformDirection(m_navAgent.velocity);
			GameManager.instance.m_GameClient.m_C2SProxy.RequestCT2PlayerPos_(HostID.Server, RmiContext.ReliableSend, base.transform.localPosition.x, base.transform.localPosition.z, vector.x, vector.z, DateTime.Now.Ticks);
		}
	}

	private bool ChkReconnect()
	{
		return GameManager.instance.m_GameClient.m_transport != null && GameManager.instance.m_GameClient.m_transport.State == CJsonTransport.EState.Connected;
	}

	private void FixedUpdate()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(ray, out hitInfo))
		{
			mousePoint = hitInfo;
		}
	}

	public void MoveTo(UnityEngine.Vector3 dest)
	{
		if (State != CT2EntityState.SAFE && State != CT2EntityState.ATTACK && !(m_navAgent == null))
		{
			if (!m_navAgent.enabled)
			{
				m_navAgent.enabled = true;
			}
			if (m_navAgent.SetDestination(dest))
			{
			}
			m_avatar.FadingInRun(0.2f);
		}
	}

	public override void Teleport(float x, float z)
	{
		if (m_navAgent != null)
		{
			m_navAgent.Stop();
			m_navAgent.enabled = false;
			base.transform.localPosition = new UnityEngine.Vector3(x, 0.1f, z);
			m_navAgent.enabled = true;
		}
	}

	public override void ReShowName()
	{
		string ten = base.Info.Ten;
		ten = Utils.getStringNameByKhiThe(base.Info.KhiThe) + " " + base.Info.Ten;
		GUIPanel.displayDanhHieu(base.Info);
		GUIPanel.NameLabel.text = ten;
	}

	public override void GotRune(ChienTruongChinhTa.RuneType rune, float tyleHp)
	{
		base.GotRune(rune, tyleHp);
	}

	public override void SetTyleHp(float tyle)
	{
		base.SetTyleHp(tyle);
		ScreenCT2 screenCT = GUIManager.getScreen(GAME_SCREEN.ScreenCT2) as ScreenCT2;
		if (screenCT.m_tongHpBarSpr != null)
		{
			screenCT.m_tongHpBarSpr.fillAmount = base.TyleHp;
		}
	}
}

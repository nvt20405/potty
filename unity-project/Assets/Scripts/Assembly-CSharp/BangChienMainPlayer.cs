using System;
using System.Collections;
using UnityEngine;

using UnityEngine.AI;
public class BangChienMainPlayer : BangChienPlayer
{
	private const float updatePosInterval = 1f;

	private const float updatePosOnline = 5f;

	private float timeUpdateOnline = 5f;

	private float timeUpdateViTri = 1f;

	private Vector3 lastPos = Vector3.zero;

	private bool changePos;

	private RaycastHit mousePoint;

	public bool m_bActive = true;

	private bool pressed;

	private bool unpressed;

	public bool dangLayPhanThuong;

	private void Start()
	{
		ScreenThanhChien screenThanhChien = GUIManager.getScreen(GAME_SCREEN.ScreenThanhChien) as ScreenThanhChien;
		thanhChien = screenThanhChien;
	}

	private void DangTriThuong()
	{
		if (playerInfo != null && m_particleOb == null && playerInfo.TimeHoiSinh > GameManager.instance.m_GameClient.ServerTime)
		{
			PlayParticle("FX/Prefabs/MISC_SAFE");
			label.text = Localization.instance.Get("DangTriThuong");
		}
		else if (playerInfo != null && playerInfo.TimeHoiSinh < GameManager.instance.m_GameClient.ServerTime)
		{
			if (m_particleOb != null)
			{
				m_particleOb.gameObject.SetActive(false);
				UnityEngine.Object.Destroy(m_particleOb.gameObject);
			}
			label.text = playerInfo.Ten;
		}
	}

	private void Update()
	{
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
		if (pressed && !unpressed && mousePoint.collider != null)
		{
			if (mousePoint.collider.gameObject.layer == 30 && playerInfo != null && playerInfo.TimeHoiSinh < GameManager.instance.m_GameClient.ServerTime)
			{
				MoveToNewDes(mousePoint.point);
			}
		}
		else if (unpressed && UICamera.hoveredObject == null && !UICamera.isDragging && mousePoint.collider != null)
		{
			PlayerMovement playerMovement = NGUITools.FindInParents<PlayerMovement>(mousePoint.collider.gameObject);
			if (playerMovement != null && playerMovement.GID != base.GID)
			{
				EGDebug.Log("Clicked " + playerMovement.UserName);
				PopupGamerInCity.Create(playerMovement.GID, playerMovement.UserName, playerMovement.CodeName, playerMovement.IsOnline);
			}
			unpressed = false;
		}
		RotateLabelTowardCam();
		NavAnimSetup();
		DangTriThuong();
		if (timeUpdateViTri < 0f && changePos)
		{
			int thanhIdx = ((!(thanhChien == null)) ? thanhChien.ThanhIdx : 0);
			GameManager.instance.m_GameClient.RequestBangChienMove(thanhIdx, base.transform.localPosition.x, base.transform.localPosition.y, base.transform.localPosition.z);
			timeUpdateViTri = 1f;
		}
		if (timeUpdateOnline < 0f)
		{
			lastPos = base.transform.position;
			int thanhIdx2 = ((!(thanhChien == null)) ? thanhChien.ThanhIdx : 0);
			GameManager.instance.m_GameClient.RequestBangChienGetOtherUser(thanhIdx2);
			timeUpdateOnline = 5f;
		}
		timeUpdateOnline -= Time.deltaTime;
		timeUpdateViTri -= Time.deltaTime;
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

	public void MoveToNewDes(Vector3 point)
	{
		changePos = true;
		lastPos = base.transform.position;
		RunTo(point);
	}

	public void Teleport(Vector3 point)
	{
		NavMeshAgent component = GetComponent<NavMeshAgent>();
		if (component != null)
		{
			if (component.enabled)
			{
				component.Stop();
			}
			component.enabled = false;
			base.transform.position = point;
			component.enabled = true;
		}
	}

	public void RequestGetPhanThuong()
	{
		if (playerInfo.Def > 0 && !dangLayPhanThuong)
		{
			DateTime dateTime = playerInfo.LastTimeGetPhanThuong + ConfigManager.instance.OtherConfig.BangChien.GetThoiGianGetPhanThuong() + TimeSpan.FromSeconds(1.0);
			if (dateTime < GameManager.instance.m_GameClient.ServerTime)
			{
				int thanhIdx = ((!(thanhChien == null)) ? thanhChien.ThanhIdx : 0);
				GameManager.instance.m_GameClient.RequestBangChienGetPhanThuongThuThanh(thanhIdx);
				dangLayPhanThuong = true;
			}
		}
	}

	public void RequestRoiCongThanh(int gate)
	{
		int thanhIdx = ((!(thanhChien == null)) ? thanhChien.ThanhIdx : 0);
		if (playerInfo != null && playerInfo.Def > 0)
		{
			GameManager.instance.m_GameClient.RequestBangChienDenCongThanh(thanhIdx, -gate, false);
		}
	}

	public void RequestDenCongThanh(int gate)
	{
		DateTime dateTime = playerInfo.LastTimeDenCongThanh + ConfigManager.instance.OtherConfig.BangChien.GetThoiGianDenThuThanh() + TimeSpan.FromSeconds(1.0);
		if (dateTime < GameManager.instance.m_GameClient.ServerTime)
		{
			int thanhIdx = ((!(thanhChien == null)) ? thanhChien.ThanhIdx : 0);
			if (playerInfo != null && playerInfo.Def > 0)
			{
				GameManager.instance.m_GameClient.RequestBangChienDenCongThanh(thanhIdx, gate);
			}
		}
		else
		{
			PopupActionWaitForSeconds.Create(dateTime, () =>
			{
				RequestDenCongThanh(gate);
			});
		}
	}

	public void RequestCongThanh(int gate)
	{
		DateTime dateTime = playerInfo.LastTimeDenCongThanh + ConfigManager.instance.OtherConfig.BangChien.GetThoiGianCongThanh() + TimeSpan.FromSeconds(1.0);
		if (dateTime < GameManager.instance.m_GameClient.ServerTime)
		{
			int thanhIdx = ((!(thanhChien == null)) ? thanhChien.ThanhIdx : 0);
			if (playerInfo != null && playerInfo.Def <= 0)
			{
				AttackerRotateToWall();
				NhayLenWall();
				StartCoroutine(DelayRequestCongThanh(1f, thanhIdx, gate));
			}
		}
		else
		{
			PopupActionWaitForSeconds.Create(dateTime, () =>
			{
				RequestCongThanh(gate);
			});
		}
	}

	private IEnumerator DelayRequestCongThanh(float seconds, int thanhIdx, int gate)
	{
		yield return new WaitForSeconds(seconds);
		GameManager.instance.m_GameClient.RequestBangChienCongThanh(thanhIdx, gate);
	}
}

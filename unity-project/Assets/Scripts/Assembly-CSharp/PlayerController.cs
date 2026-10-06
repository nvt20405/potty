using LitJson;
using UnityEngine;

using UnityEngine.AI;
public class PlayerController : PlayerMovement
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

	private void Update()
	{
		if (!m_bActive)
		{
			return;
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
		if (pressed && !unpressed && mousePoint.collider != null)
		{
			if (mousePoint.collider.gameObject.layer == 30)
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
		if (timeUpdateViTri < 0f)
		{
			HomeResponse.Position3D position3D = new HomeResponse.Position3D();
			position3D.X = base.transform.position.x;
			position3D.Y = base.transform.position.y;
			position3D.Z = base.transform.position.z;
			GameManager.instance.m_GameClient.SendRequest(GameManager.instance.m_GameClient.C2SProxy.RequestChangeNextPos, JsonMapper.ToJson(position3D, false), false);
			timeUpdateViTri = 1f;
		}
		if (timeUpdateOnline < 0f && changePos)
		{
			float num = Vector3.SqrMagnitude(base.transform.position - lastPos);
			if (num > 1f)
			{
				lastPos = base.transform.position;
				GameManager.instance.m_GameClient.RequestGetListOtherPlayer(GameManager.instance.m_GameClient.UserInfo.Gamer.Level);
				timeUpdateOnline = 5f;
			}
		}
		timeUpdateOnline -= Time.deltaTime;
		timeUpdateViTri -= Time.deltaTime;
	}

	private void FixedUpdate()
	{
		if (!m_bActive)
		{
			return;
		}
		Camera main = Camera.main;
		if (!(main == null))
		{
			Ray ray = main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hitInfo = default(RaycastHit);
			if (Physics.Raycast(ray, out hitInfo))
			{
				mousePoint = hitInfo;
			}
		}
	}

	public void MoveToNewDes(Vector3 point)
	{
		changePos = true;
		lastPos = base.transform.position;
		MoveTo(point);
	}

	public void Teleport(Vector3 point)
	{
		NavMeshAgent component = GetComponent<NavMeshAgent>();
		if (component == null)
		{
			base.transform.position = point;
			return;
		}
		if (component.enabled)
		{
			component.Stop();
		}
		component.enabled = false;
		base.transform.position = point;
		component.enabled = true;
	}
}

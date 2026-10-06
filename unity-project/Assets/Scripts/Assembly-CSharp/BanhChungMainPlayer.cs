using UnityEngine;

public class BanhChungMainPlayer : BanhChungPlayer
{
	private const float updatePosInterval = 1f;

	private const float updatePosOnline = 2f;

	private float timeUpdateOnline = 2f;

	private float timeUpdateViTri = 1f;

	private Vector3 lastPos = Vector3.zero;

	private bool changePos;

	private RaycastHit mousePoint;

	private ScreenBanhChung3D screenBanhChung3D;

	private bool pressed;

	private bool unpressed;

	private void Start()
	{
		ScreenBanhChung screenBanhChung = GUIManager.getScreen(GAME_SCREEN.ScreenBanhChung) as ScreenBanhChung;
		screenBanhChung3D = screenBanhChung.screen3dObj;
	}

	private void OnTriggerEnter(Collider other)
	{
		GameObject gameObject = other.gameObject;
		if (gameObject.name.StartsWith("Bao_Thoc") || gameObject.name.StartsWith("Bui_La_Dong"))
		{
			GameManager.instance.m_GameClient.RequestBanhChungGetNguyenLieu();
			gameObject.SetActive(false);
			if (screenBanhChung3D.baoGao != null)
			{
				screenBanhChung3D.baoGao = null;
			}
			if (screenBanhChung3D.buiDong != null)
			{
				screenBanhChung3D.buiDong = null;
			}
			Object.Destroy(gameObject, 3f);
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
			if ((!(screenBanhChung3D != null) || !(screenBanhChung3D.NoiBanhCollider.gameObject == mousePoint.collider.gameObject)) && mousePoint.collider.gameObject.layer == 30)
			{
				MoveToNewDes(mousePoint.point);
			}
		}
		else if (unpressed && UICamera.hoveredObject == null && !UICamera.isDragging && mousePoint.collider != null)
		{
			unpressed = false;
		}
		RotateLabelTowardCam();
		NavAnimSetup();
		if (timeUpdateViTri < 0f && changePos)
		{
			GameManager.instance.m_GameClient.RequestBanhChungPlayerMove(base.transform.position.x, base.transform.position.y, base.transform.position.z);
			if (ConfigManager.instance.OtherConfig.TanSuatUpdateViTriPlayerBanhChung > 0)
			{
				timeUpdateViTri = ConfigManager.instance.OtherConfig.TanSuatUpdateViTriPlayerBanhChung;
			}
			else
			{
				timeUpdateViTri = 1f;
			}
		}
		if (timeUpdateOnline < 0f)
		{
			lastPos = base.transform.position;
			GameManager.instance.m_GameClient.RequestBanhChungGetOthers();
			if (ConfigManager.instance.OtherConfig.TanSuatUpdateViTriOtherBanhChung > 0)
			{
				timeUpdateOnline = ConfigManager.instance.OtherConfig.TanSuatUpdateViTriOtherBanhChung;
			}
			else
			{
				timeUpdateOnline = 2f;
			}
		}
		timeUpdateOnline -= Time.deltaTime;
		timeUpdateViTri -= Time.deltaTime;
	}

	private void FixedUpdate()
	{
		if (Camera.main != null)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
			changePos = true;
			component.enabled = true;
		}
	}
}

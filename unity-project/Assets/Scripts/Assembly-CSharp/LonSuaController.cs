using UnityEngine;

public class LonSuaController : MonoBehaviour
{
	public const float RunMaxSpeed = 2.4f;

	public const float WalkMaxSpeed = 0.4f;

	public Vector3 currentDes;

	public GameObject avatar;

	public UILabel label;

	public UIPanel ui;

	private NavMeshAgent nav;

	public bool IsInSightOfPlayer;

	private ScreenBanhChung3D screen3dObj;

	public Vector3 spawnPos;

	private float nextTimeToWalk = Random.Range(3f, 6f);

	private float timeCounter;

	private void Awake()
	{
		nav = GetComponent<NavMeshAgent>();
	}

	private void Start()
	{
		ScreenBanhChung screenBanhChung = GUIManager.getScreen(GAME_SCREEN.ScreenBanhChung) as ScreenBanhChung;
		screen3dObj = screenBanhChung.screen3dObj;
	}

	private void Update()
	{
		CheckPosMainPlayer();
		NavAnimSetup();
		RotateGUITowardCam();
		if (!IsInSightOfPlayer)
		{
			WalkAround();
		}
	}

	private Vector3 GetRandomPosArround(Vector3 center)
	{
		return center + new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f));
	}

	private void WalkAround()
	{
		timeCounter += Time.deltaTime;
		if (timeCounter > nextTimeToWalk)
		{
			MoveTo(GetRandomPosArround(spawnPos));
			timeCounter = 0f;
			nextTimeToWalk = Random.Range(3f, 6f);
		}
	}

	private void CatchUp()
	{
		screen3dObj.lon_sua = null;
		GameManager.instance.m_GameClient.RequestBanhChungGetNguyenLieu();
		base.gameObject.SetActive(false);
		Object.Destroy(base.gameObject, 3f);
	}

	private void CheckPosMainPlayer()
	{
		if (!(screen3dObj != null) || !(screen3dObj.mainPlayer != null))
		{
			return;
		}
		Vector3 position = screen3dObj.mainPlayer.transform.position;
		Vector3 vector = base.transform.position - position;
		float sqrMagnitude = vector.sqrMagnitude;
		if (sqrMagnitude < 10f)
		{
			IsInSightOfPlayer = true;
			nav.speed = 2.4f;
			if (sqrMagnitude < 0.8f)
			{
				CatchUp();
			}
			Vector3 vector2 = base.transform.position + vector;
			float num = 15f;
			float num2 = Random.Range(0f, 1f);
			num = ((!((double)num2 < 0.5)) ? 20f : (-20f));
			while ((vector2 - position).sqrMagnitude < 10f && !(num > 60f) && !(num < -60f))
			{
				Vector3 vector3 = Quaternion.Euler(0f, num, 0f) * vector;
				num = ((!(num < 0f)) ? (num + 20f) : (num - 20f));
				vector2 = base.transform.position + vector3;
			}
			MoveTo(vector2);
		}
		else
		{
			IsInSightOfPlayer = false;
			nav.speed = 0.4f;
		}
	}

	protected void RotateGUITowardCam()
	{
		if (ui != null)
		{
			Vector3 forward = Camera.main.transform.forward;
			ui.transform.rotation = Quaternion.LookRotation(forward);
		}
	}

	protected void NavAnimSetup()
	{
		if (!(nav == null))
		{
			float magnitude = Vector3.Project(nav.desiredVelocity, base.transform.forward).magnitude;
			float num = 0.1f;
			if (!nav.hasPath || Vector3.Distance(base.transform.position, nav.destination) <= num)
			{
				avatar.GetComponent<Animation>().CrossFade("idle", 0.2f);
			}
			else if (magnitude > 1.2f)
			{
				avatar.GetComponent<Animation>().CrossFade("run", 0.2f);
			}
			else
			{
				avatar.GetComponent<Animation>().CrossFade("walk", 0.2f);
			}
		}
	}

	public void MoveTo(Vector3 dest)
	{
		if (base.gameObject.activeInHierarchy && !(nav == null))
		{
			if (!nav.enabled)
			{
				nav.enabled = true;
			}
			if (nav.SetDestination(dest))
			{
				currentDes = dest;
			}
		}
	}
}

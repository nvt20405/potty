using UnityEngine;

public class BunnyController : MonoBehaviour
{
	public const float RunMaxSpeed = 2.8f;

	public const float WalkMaxSpeed = 0.34f;

	public Vector3 currentDes;

	public GameObject avatar;

	public UILabel label;

	public UIPanel ui;

	private NavMeshAgent nav;

	public bool IsInSightOfPlayer;

	private void Awake()
	{
		nav = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		CheckPosMainPlayer();
		NavAnimSetup();
		RotateGUITowardCam();
	}

	private void CatchUp()
	{
		GameManager.instance.m_GameClient.RequestBatTho();
		base.gameObject.SetActive(false);
		Object.Destroy(base.gameObject, 0.05f);
		GUIManager.instance.homeCity.bunny = null;
	}

	private void CheckPosMainPlayer()
	{
		if (!(GUIManager.instance != null) || !(GUIManager.instance.homeCity != null) || !(GUIManager.instance.homeCity.mainAvatar != null))
		{
			return;
		}
		Vector3 position = GUIManager.instance.homeCity.mainAvatar.transform.position;
		Vector3 vector = base.transform.position - position;
		float sqrMagnitude = vector.sqrMagnitude;
		if (sqrMagnitude < 10f)
		{
			IsInSightOfPlayer = true;
			nav.speed = 2.8f;
			if (sqrMagnitude < 0.8f)
			{
				CatchUp();
			}
			Vector3 vector2 = base.transform.position + vector;
			NavMeshHit hit = default(NavMeshHit);
			if (NavMesh.SamplePosition(vector2, out hit, 12f, 1))
			{
				vector2 = hit.position;
			}
			float num = 15f;
			float num2 = Random.Range(0f, 1f);
			num = ((!((double)num2 < 0.5)) ? 20f : (-20f));
			NavMeshHit hit2 = default(NavMeshHit);
			while ((vector2 - position).sqrMagnitude < 10f && !(num > 60f) && !(num < -60f))
			{
				Vector3 vector3 = Quaternion.Euler(0f, num, 0f) * vector;
				num = ((!(num < 0f)) ? (num + 20f) : (num - 20f));
				vector2 = base.transform.position + vector3;
				if (NavMesh.SamplePosition(vector3, out hit2, 12f, 1))
				{
					vector2 = hit2.position;
				}
			}
			MoveTo(vector2);
		}
		else
		{
			IsInSightOfPlayer = false;
			nav.speed = 0.34f;
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
		if (nav == null)
		{
			return;
		}
		float magnitude = Vector3.Project(nav.desiredVelocity, base.transform.forward).magnitude;
		float num = 0.1f;
		if (!nav.hasPath || Vector3.Distance(base.transform.position, nav.destination) <= num)
		{
			avatar.GetComponent<Animation>().CrossFade("idle", 0.2f);
		}
		else if (magnitude > 1.2f)
		{
			if (nav.speed == 0.34f)
			{
				avatar.GetComponent<Animation>().CrossFade("run", 0.2f);
				avatar.GetComponent<Animation>()["run"].speed = 0.5f;
			}
			else
			{
				avatar.GetComponent<Animation>().CrossFade("run", 0.2f);
				avatar.GetComponent<Animation>()["run"].speed = 1f;
			}
		}
		else
		{
			avatar.GetComponent<Animation>().CrossFade("walk", 0.2f);
		}
	}

	public void MoveTo(Vector3 dest)
	{
		if (base.gameObject.activeInHierarchy && !(nav == null) && nav.SetDestination(dest))
		{
			currentDes = dest;
		}
	}
}

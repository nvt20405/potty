using UnityEngine;

using UnityEngine.AI;
public class ThanThuController : MonoBehaviour
{
	public const float RunMaxSpeed = 3.4f;

	public const float WalkMaxSpeed = 0.34f;

	public Vector3 currentDes;

	public GameObject avatar;

	public UILabel label;

	public UIPanel ui;

	public GameObject model3D;

	public GameObject owner;

	public NavMeshAgent nav;

	private Vector3 targetPos;

	private float idleTime;

	private void Update()
	{
		CheckPosMainPlayer();
		NavAnimSetup();
		if (nav.speed == 0f)
		{
			idleTime += Time.deltaTime;
		}
		else
		{
			idleTime = 0f;
		}
	}

	private void CheckPosMainPlayer()
	{
		if (!(owner != null))
		{
			return;
		}
		Vector3 position = owner.transform.position;
		float sqrMagnitude = (base.transform.position - position).sqrMagnitude;
		if (sqrMagnitude > 64f)
		{
			NavMeshAgent component = GetComponent<NavMeshAgent>();
			if (component.enabled)
			{
				component.Stop();
			}
			component.enabled = false;
			Vector3 vector = default(Vector3);
			Vector3 vector2 = 2f * new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
			targetPos = position;
			base.transform.position = targetPos;
			component.enabled = true;
		}
		else if (sqrMagnitude > 5f)
		{
			Vector3 vector3 = default(Vector3);
			Vector3 vector4 = 2f * new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
			nav.speed = 3.4f;
			targetPos = position + vector4;
			MoveTo(targetPos);
		}
		else if (idleTime > 3f)
		{
			Vector3 vector5 = default(Vector3);
			Vector3 vector6 = 2f * new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
			nav.speed = 0.34f;
			targetPos = position + vector6;
			MoveTo(targetPos);
		}
		else if ((base.transform.position - targetPos).sqrMagnitude < 0.5f)
		{
			nav.speed = 0f;
		}
		else
		{
			MoveTo(targetPos);
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
			else if (magnitude > 0f)
			{
				avatar.GetComponent<Animation>().CrossFade("run", 0.2f);
			}
			else
			{
				avatar.GetComponent<Animation>().CrossFade("idle", 0.2f);
			}
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

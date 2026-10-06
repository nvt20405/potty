using System.Collections;
using UnityEngine;

using UnityEngine.AI;
public class BangChienPlayer : MonoBehaviour
{
	public const float RunMaxSpeed = 3.5f;

	public const float WalkMaxSpeed = 1.2f;

	public Vector3 currentDes;

	public Avatar3D avatar;

	public UILabel label;

	public UIPanel ui;

	private NavMeshAgent nav;

	private bool isFalling;

	private string codeName;

	private AnimVuKhi vuKhiAnim;

	protected GameObject m_particleOb;

	public NguoiChoiBangChien playerInfo;

	public AnimationClip wallClip;

	public ScreenThanhChien thanhChien;

	private static readonly Vector3[] listArround = new Vector3[32]
	{
		new Vector3(3.999961f, 0f, 0.017702792f),
		new Vector3(-3.9996474f, 0f, -0.10698889f),
		new Vector3(3.9990206f, 0f, 0.08850703f),
		new Vector3(-3.9980807f, 0f, -0.12390013f),
		new Vector3(3.9968274f, 0f, 0.15928352f),
		new Vector3(-3.995261f, 0f, -0.19465443f),
		new Vector3(3.9933815f, 0f, 0.2300101f),
		new Vector3(-3.991189f, 0f, -0.26534775f),
		new Vector3(3.9886842f, 0f, 0.3006646f),
		new Vector3(-3.9858665f, 0f, -0.33595788f),
		new Vector3(3.9827368f, 0f, 0.37122488f),
		new Vector3(-3.979295f, 0f, -0.40646276f),
		new Vector3(3.9755414f, 0f, 0.4416688f),
		new Vector3(-3.9714763f, 0f, -0.47684026f),
		new Vector3(3.9671f, 0f, 0.51197433f),
		new Vector3(-3.9624128f, 0f, -0.5470683f),
		new Vector3(0.017702619f, 0f, -1.9999217f),
		new Vector3(1.7110399f, 0f, -1.0355396f),
		new Vector3(1.7799911f, 0f, 0.9119382f),
		new Vector3(0.15909709f, 0f, 1.993662f),
		new Vector3(-1.6128368f, 0f, 1.1826907f),
		new Vector3(-1.8536143f, 0f, -0.7510751f),
		new Vector3(-0.33465198f, 0f, -1.9718033f),
		new Vector3(1.5020143f, 0f, -1.3205881f),
		new Vector3(-3.2f, 0f, 3.2f),
		new Vector3(3.2f, 0f, 3.2f),
		new Vector3(-3.2f, 0f, -3.2f),
		new Vector3(3.2f, 0f, -3.2f),
		new Vector3(-1.6f, 0f, 1.6f),
		new Vector3(1.6f, 0f, 1.6f),
		new Vector3(-1.6f, 0f, -1.6f),
		new Vector3(1.6f, 0f, -1.6f)
	};

	protected bool runOnWall;

	public string CodeName
	{
		get
		{
			return codeName;
		}
	}

	public string VuKhiName { get; private set; }

	public string UserName { get; set; }

	public int GID { get; set; }

	public int SID { get; set; }

	public int LID { get; set; }

	public string LMName { get; set; }

	private void Awake()
	{
		nav = GetComponent<NavMeshAgent>();
	}

	public void SetCodeName(string codeName, string vuKhi = "", string costume = "")
	{
		if (codeName == CodeName && avatar != null)
		{
			if (vuKhi != VuKhiName)
			{
				avatar.LoadVK(vuKhi);
				VuKhiName = vuKhi;
			}
			return;
		}
		if (avatar != null)
		{
			Object.Destroy(avatar.gameObject);
		}
		this.codeName = codeName;
		VuKhiName = vuKhi;
		TrangBiCfg value;
		if (ConfigManager.instance.m_dicTrangBi.TryGetValue(vuKhi, out value))
		{
			vuKhiAnim = value.GetAnimVK();
		}
		avatar = GUIManager.instance.InstantiateAvatar3D(codeName, vuKhi, string.Empty, string.Empty, null, null, string.Empty, costume, string.Empty);
		avatar.transform.parent = base.transform;
		avatar.transform.localPosition = Vector3.zero;
		avatar.transform.localRotation = Quaternion.identity;
		avatar.transform.localScale = Vector3.one;
		avatar.gameObject.name = "GameObject";
	}

	public void SetUserName(string userName)
	{
		label.text = userName;
		UserName = userName;
	}

	private Vector3 GetRandomPosArround(Vector3 center)
	{
		Vector3 vector = default(Vector3);
		vector = new Vector3(Random.Range(2f, 5f), 0f, 0f);
		Vector3 vector2 = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f) * vector;
		return center + vector2;
	}

	public void NhayLenWall(float seconds)
	{
		StartCoroutine(delayNhayLenWall(seconds));
	}

	private IEnumerator delayNhayLenWall(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		NhayLenWall();
	}

	public void NhayLenWall()
	{
		NavMeshAgent component = GetComponent<NavMeshAgent>();
		if (component != null)
		{
			component.Stop();
			component.enabled = false;
			runOnWall = true;
			avatar.PlayAnimBattle("wall_incity_nv_null", false);
			StartCoroutine(WaitToFinishAnim());
		}
	}

	private IEnumerator WaitToFinishAnim()
	{
		yield return new WaitForSeconds(1.08f);
		OnFinishNhayLenWall();
	}

	public void OnFinishNhayLenWall()
	{
		NavMeshAgent component = GetComponent<NavMeshAgent>();
		if (component != null)
		{
			component.enabled = true;
			component.Stop();
			runOnWall = false;
			LocalTeleport(new Vector3(playerInfo.X, playerInfo.Y, playerInfo.Z));
		}
	}

	private void Update()
	{
		NavAnimSetup();
		RotateLabelTowardCam();
		DangTriThuong();
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
				Object.Destroy(m_particleOb.gameObject);
			}
			label.text = string.Format("s{0}.{1}", playerInfo.SID, playerInfo.Ten);
		}
	}

	protected void RotateLabelTowardCam()
	{
		if (ui != null)
		{
			Vector3 vector = Camera.main.transform.rotation * Vector3.forward;
			Vector3 forward = vector - Vector3.up * Vector3.Dot(Vector3.up, vector);
			ui.transform.rotation = Quaternion.LookRotation(forward);
		}
	}

	protected void NavAnimSetup()
	{
		if (nav == null)
		{
			return;
		}
		float num = 0.1f;
		if (!runOnWall)
		{
			if (!nav.hasPath || Vector3.Distance(base.transform.position, nav.destination) <= num)
			{
				avatar.FadingInIdle(0.2f);
			}
			else
			{
				avatar.FadingInRun(0.2f);
			}
		}
	}

	public void PlayParticle(string prefab)
	{
		if (m_particleOb != null)
		{
			Object.Destroy(m_particleOb);
		}
		Object obj = Resources.Load(prefab);
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		if (gameObject != null)
		{
			Object obj2 = Object.Instantiate(gameObject);
			m_particleOb = (GameObject)((obj2 is GameObject) ? obj2 : null);
			if (m_particleOb != null)
			{
				m_particleOb.transform.parent = base.transform;
				m_particleOb.transform.localPosition = new Vector3(0f, 0f, 0f);
				m_particleOb.transform.localScale = Vector3.one;
			}
		}
	}

	public void LocalTeleport(Vector3 localPoint)
	{
		NavMeshAgent component = GetComponent<NavMeshAgent>();
		if (component != null)
		{
			if (component.enabled)
			{
				component.Stop();
			}
			component.enabled = false;
			base.transform.localPosition = localPoint;
			component.enabled = true;
		}
	}

	public void AttackerRotateToWall()
	{
		NavMeshAgent component = GetComponent<NavMeshAgent>();
		if (component != null)
		{
			if (component.enabled)
			{
				component.Stop();
			}
			component.enabled = false;
			base.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			component.enabled = true;
		}
	}

	public void RunTo(Vector3 dest)
	{
		if (!(nav == null))
		{
			if (!nav.enabled)
			{
				nav.enabled = true;
			}
			Vector3 vector = base.transform.position - dest;
			if (nav.SetDestination(dest))
			{
				currentDes = dest;
			}
		}
	}

	public void MoveTo(Vector3 dest)
	{
		if (!(nav == null))
		{
			if (!nav.enabled)
			{
				nav.enabled = true;
			}
			Vector3 vector = base.transform.position - dest;
			if (nav.SetDestination(dest))
			{
				currentDes = dest;
			}
		}
	}
}

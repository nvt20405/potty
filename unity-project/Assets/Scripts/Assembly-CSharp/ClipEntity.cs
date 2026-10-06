using System;
using System.Collections;
using UnityEngine;

public class ClipEntity : MonoBehaviour
{
	protected float m_fMoveSpeed = 3f;

	private Vector3 m_vGotoTar;

	private Vector3 m_vVelocity;

	private bool m_bMovePos;

	private bool m_bMoveVelocity;

	public float TimeScale { get; set; }

	public bool Pause { get; set; }

	public float DelayTime { get; set; }

	public virtual void Start()
	{
		Pause = false;
		TimeScale = 1f;
	}

	public virtual void Update()
	{
		if (Pause)
		{
			return;
		}
		if (m_bMovePos)
		{
			DelayTime -= Time.deltaTime;
			if (DelayTime <= 0f)
			{
				Vector3 vector = m_vGotoTar - base.transform.localPosition;
				float num = Vector3.Distance(base.transform.localPosition, m_vGotoTar);
				float num2 = m_fMoveSpeed * Common.TIME_PER_FRAME;
				if (num <= num2)
				{
					m_bMovePos = false;
					OnGotoSuccess();
				}
				else
				{
					base.transform.localPosition += vector.normalized * num2;
				}
				if (vector != Vector3.zero)
				{
					Vector3 eulerAngles = Quaternion.LookRotation(vector).eulerAngles;
					base.transform.localRotation = Quaternion.Slerp(base.transform.localRotation, Quaternion.Euler(eulerAngles), 10f * Time.deltaTime);
				}
			}
		}
		else
		{
			if (!m_bMoveVelocity)
			{
				return;
			}
			DelayTime -= Time.deltaTime;
			if (DelayTime <= 0f)
			{
				Transform transform = base.transform;
				Vector3 vector2 = m_vGotoTar - transform.localPosition;
				vector2.y = 0f;
				float b = m_vVelocity.magnitude * Time.deltaTime;
				if (m_vGotoTar != Vector3.zero && vector2.magnitude <= Mathf.Max(0.05f, b))
				{
					transform.localPosition = m_vGotoTar;
					m_bMoveVelocity = false;
					m_vVelocity = Vector3.zero;
				}
				else
				{
					transform.localPosition += m_vVelocity * Time.deltaTime;
				}
				if (m_vVelocity != Vector3.zero)
				{
					Vector3 eulerAngles2 = Quaternion.LookRotation(m_vVelocity).eulerAngles;
					base.transform.localRotation = Quaternion.Slerp(base.transform.localRotation, Quaternion.Euler(eulerAngles2), 15f * Time.deltaTime);
				}
			}
		}
	}

	public virtual void PlayProjectTile(string prefab, Vector3 vTar)
	{
		try
		{
			UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load(prefab));
			GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
			if (gameObject != null)
			{
				gameObject.transform.parent = base.transform;
				gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
				gameObject.transform.localScale = Vector3.one;
				gameObject.GetComponent<ParticleSystem>().transform.LookAt(vTar);
				TweenPosition.Begin(gameObject.gameObject, 0.1f, vTar);
			}
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}

	public virtual void PlayBeam(string prefab, Vector3 vTar)
	{
		try
		{
			UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load(prefab));
			GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
			if (gameObject != null)
			{
				gameObject.transform.parent = base.transform;
				gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
				gameObject.transform.localScale = Vector3.one;
				Renderer renderer = gameObject.GetComponent<ParticleSystem>().GetComponent<Renderer>();
				ParticleSystemRenderer particleSystemRenderer = (ParticleSystemRenderer)((renderer is ParticleSystemRenderer) ? renderer : null);
				gameObject.GetComponent<ParticleSystem>().transform.LookAt(vTar);
				particleSystemRenderer.lengthScale = Vector3.Distance(gameObject.transform.position, vTar);
				particleSystemRenderer.renderMode = ParticleSystemRenderMode.Stretch;
				var particleMain = gameObject.GetComponent<ParticleSystem>().main;
				particleMain.simulationSpeed = TimeScale;
				gameObject.GetComponent<ParticleSystem>().Play();
			}
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}

	public virtual GameObject PlayParticle(string prefab)
	{
		UnityEngine.Object obj = Resources.Load(prefab);
		if (obj == null)
		{
			return null;
		}
		UnityEngine.Object obj2 = UnityEngine.Object.Instantiate(obj);
		GameObject gameObject = (GameObject)((obj2 is GameObject) ? obj2 : null);
		if (gameObject != null)
		{
			gameObject.transform.parent = base.transform;
			gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localRotation = Quaternion.identity;
			var particleMain = gameObject.GetComponent<ParticleSystem>().main;
				particleMain.simulationSpeed = TimeScale;
			gameObject.GetComponent<ParticleSystem>().Play();
		}
		return gameObject;
	}

	public IEnumerator CoTweenAlpha(UILabel label)
	{
		if (label != null)
		{
			TweenAlpha.Begin(label.gameObject, 0.3f, 1f);
			yield return new WaitForSeconds(0.5f);
			TweenAlpha.Begin(label.gameObject, 0.3f, 0f).onFinished = RemoveLabelDmg;
		}
		yield return null;
	}

	private void RemoveLabelDmg(UITweener tweener)
	{
		UILabel component = tweener.gameObject.GetComponent<UILabel>();
		if (component != null)
		{
			UnityEngine.Object.Destroy(component.transform.parent.gameObject);
		}
	}

	public virtual void Goto(Vector3 velocity, Vector3 tar, float delay = 0f)
	{
		m_vVelocity = velocity;
		m_bMoveVelocity = true;
		m_vGotoTar = tar;
		DelayTime = delay;
	}

	public virtual void Goto(Vector3 vTar, float speed, float delay = 0f)
	{
		m_fMoveSpeed = speed;
		m_bMovePos = true;
		m_vGotoTar = vTar;
		DelayTime = delay;
	}

	public virtual void CancelGoto()
	{
		m_bMovePos = false;
		m_bMoveVelocity = false;
		m_vVelocity = Vector3.zero;
	}

	public virtual void OnGotoSuccess()
	{
	}

	public Transform SearchBone(Transform target, string name)
	{
		if (target.name == name)
		{
			return target;
		}
		for (int i = 0; i < target.childCount; i++)
		{
			Transform transform = SearchBone(target.GetChild(i), name);
			if (transform != null)
			{
				return transform;
			}
		}
		return null;
	}
}

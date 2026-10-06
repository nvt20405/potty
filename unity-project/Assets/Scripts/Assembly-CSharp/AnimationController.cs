using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("PocketRPG/Animation Controller")]
[RequireComponent(typeof(Animation))]
public class AnimationController : MonoBehaviour
{
	private AnimationState currentState;

	private float currentStateTime;

	private List<AnimationState> fadingStates;

	private float animationFadeTime = 0.15f;

	protected List<WeaponTrail> trails;

	protected float t = 0.033f;

	protected float m;

	protected Vector3 lastEulerAngles = Vector3.zero;

	protected Vector3 lastPosition = Vector3.zero;

	protected Vector3 eulerAngles = Vector3.zero;

	protected Vector3 position = Vector3.zero;

	private float tempT;

	public bool gatherDeltaTimeAutomatically = true;

	protected float animationIncrement = 0.04f;

	public bool Pause { get; set; }

	private void Awake()
	{
		trails = new List<WeaponTrail>();
		fadingStates = new List<AnimationState>();
		currentState = null;
		GetComponent<Animation>().Stop();
		lastPosition = base.transform.position;
		lastEulerAngles = base.transform.eulerAngles;
		GetComponent<Animation>().playAutomatically = false;
		GetComponent<Animation>().Stop();
	}

	private void Start()
	{
		GetComponent<Animation>().Stop();
	}

	public void SetDeltaTime(float deltaTime)
	{
		t = deltaTime;
	}

	public void SetAnimationSampleRate(int samplesPerSecond)
	{
		animationIncrement = 1f / (float)samplesPerSecond;
	}

	protected virtual void LateUpdate()
	{
		if (gatherDeltaTimeAutomatically)
		{
			t = Mathf.Clamp(Time.deltaTime, 0f, 0.066f);
		}
		else
		{
			t = 0f;
		}
		RunAnimations();
	}

	public void AddTrail(WeaponTrail trail)
	{
		trails.Add(trail);
	}

	public void PlayAnimation(AnimationState state)
	{
		for (int i = 0; i < fadingStates.Count; i++)
		{
			fadingStates[i].weight = 0f;
			fadingStates[i].enabled = false;
		}
		fadingStates.Clear();
		if (currentState != null)
		{
			currentState.enabled = false;
			currentState.weight = 0f;
		}
		currentState = state;
		currentState.weight = 1f;
		currentState.time = (currentStateTime = 0f);
		currentState.enabled = true;
	}

	public void CrossfadeAnimation(AnimationState state, float fadeTime)
	{
		if (currentState == state)
		{
			return;
		}
		animationFadeTime = fadeTime;
		for (int i = 0; i < fadingStates.Count; i++)
		{
			if (state.name == fadingStates[i].name)
			{
				fadingStates.RemoveAt(i);
				if (currentState != null)
				{
					fadingStates.Add(currentState);
				}
				currentState = state;
				return;
			}
		}
		if (currentState != null)
		{
			fadingStates.Add(currentState);
		}
		currentState = state;
		currentState.weight = 0f;
		currentState.time = (currentStateTime = 0f);
		currentState.enabled = true;
	}

	private bool FadeOutAnimation(AnimationState state, float aI)
	{
		state.weight -= aI / animationFadeTime;
		state.time += aI * state.speed;
		if (state.weight <= 0f)
		{
			state.enabled = false;
			return true;
		}
		return false;
	}

	private void FadeInCurrentState(float aI)
	{
		currentState.weight = Mathf.Clamp(currentState.weight + aI / animationFadeTime, 0f, 1f);
		currentStateTime += aI * currentState.speed;
		currentState.time = currentStateTime;
	}

	private void RunAnimations()
	{
		if (!(t > 0f))
		{
			return;
		}
		eulerAngles = base.transform.eulerAngles;
		position = base.transform.position;
		while (tempT < t)
		{
			tempT += animationIncrement;
			for (int i = 0; i < fadingStates.Count; i++)
			{
				if (FadeOutAnimation(fadingStates[i], animationIncrement))
				{
					fadingStates.RemoveAt(i);
					i--;
				}
			}
			if (currentState != null)
			{
				FadeInCurrentState(animationIncrement);
			}
			m = tempT / t;
			base.transform.eulerAngles = new Vector3(Mathf.LerpAngle(lastEulerAngles.x, eulerAngles.x, m), Mathf.LerpAngle(lastEulerAngles.y, eulerAngles.y, m), Mathf.LerpAngle(lastEulerAngles.z, eulerAngles.z, m));
			base.transform.position = Vector3.Lerp(lastPosition, position, m);
			GetComponent<Animation>().Sample();
			for (int j = 0; j < trails.Count; j++)
			{
				if (trails[j].time > 0f)
				{
					trails[j].Itterate(Time.time - t + tempT);
				}
				else
				{
					trails[j].ClearTrail();
				}
			}
		}
		tempT -= t;
		base.transform.position = position;
		base.transform.eulerAngles = eulerAngles;
		lastPosition = position;
		lastEulerAngles = eulerAngles;
		for (int k = 0; k < trails.Count; k++)
		{
			if (trails[k].time > 0f)
			{
				trails[k].UpdateTrail(Time.time, t);
			}
		}
	}
}

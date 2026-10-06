using System.Collections;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
	public delegate void Callback();

	public static CoroutineManager instance;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public static void WaitForSeconds(float waitTime, Callback callback)
	{
		instance.StartCoroutine(instance._WaitForSeconds(waitTime, callback));
	}

	private IEnumerator _WaitForSeconds(float waitTime, Callback callback)
	{
		yield return new WaitForSeconds(waitTime);
		callback();
	}

	public static void stopCoroutine()
	{
		instance.StopAllCoroutines();
	}
}

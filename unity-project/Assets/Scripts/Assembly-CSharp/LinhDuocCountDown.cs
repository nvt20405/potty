using System;
using System.Collections;
using UnityEngine;

public class LinhDuocCountDown : MonoBehaviour
{
	public UILabel label;

	public string prefix;

	public TimeSpan span;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void StopCountDown()
	{
		StopCoroutine(Tick());
	}

	public void StartCountDown(TimeSpan span)
	{
		this.span = span;
		StopAllCoroutines();
		StartCoroutine(Tick());
	}

	private IEnumerator Tick()
	{
		while (span.TotalSeconds > 0.0)
		{
			span -= new TimeSpan(10000000L);
			if (span.Days > 0)
			{
				label.text = prefix + span.Days + "d " + span.Hours + "h ";
			}
			else
			{
				label.text = prefix + span.Hours + "h " + span.Minutes + "m " + span.Seconds + "s";
			}
			yield return new WaitForSeconds(1f);
		}
		base.transform.parent.gameObject.SetActive(false);
	}
}

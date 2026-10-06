using System.Collections;
using UnityEngine;

public class TextTypingEffect : MonoBehaviour
{
	public string LocallizeKey;

	public float Delay;

	public float Duration;

	public UILabel textLabel;

	private string[] contents;

	private float TimeStep = 0.1f;

	private void Start()
	{
		textLabel.text = string.Empty;
		string text = Localization.instance.Get(LocallizeKey);
		contents = text.Split(' ');
		TimeStep = Duration / (float)contents.Length;
		StartCoroutine(PlayEffect());
	}

	private IEnumerator PlayEffect()
	{
		string str = string.Empty;
		yield return new WaitForSeconds(Delay);
		for (int i = 0; i < contents.Length; i++)
		{
			str = str + " " + contents[i];
			textLabel.text = str;
			yield return new WaitForSeconds(TimeStep);
		}
	}

	private void Update()
	{
	}
}

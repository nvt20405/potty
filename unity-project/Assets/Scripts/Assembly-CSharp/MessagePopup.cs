using System.Collections;
using UnityEngine;

public class MessagePopup : MonoBehaviour
{
	public UILabel label;

	public UISprite spBackground;

	public static MessagePopup instance;

	private float time = 2f;

	public static void Create(string message, float display_time)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("popup/MessagePopup"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		gameObject.transform.parent = GUIManager.instance.popUpContainer.transform;
		gameObject.transform.localPosition = new Vector3(0f, 340f, -700f);
		gameObject.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<MessagePopup>();
		instance.label.text = message;
		instance.time = display_time;
		TweenRotation tweenRotation = TweenRotation.Begin(gameObject, 0.2f, Quaternion.Euler(0f, 0f, 0f));
		tweenRotation.onFinished = instance.OnRotFinished;
		instance.spBackground.transform.localScale = new Vector3(instance.spBackground.transform.localScale.x, instance.label.relativeSize.y * instance.label.transform.localScale.y + 30f, instance.spBackground.transform.localScale.z);
	}

	public static void Create(string message)
	{
		Create(message, 2f);
	}

	private IEnumerator DelayDestroyPopup(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		DestroyPopup();
	}

	private void OnRotFinished(UITweener tweener)
	{
		if (base.transform != null)
		{
			base.transform.localRotation = Quaternion.identity;
			StartCoroutine(DelayDestroyPopup(time));
		}
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			instance.gameObject.SetActive(false);
			Object.Destroy(instance.gameObject);
		}
		instance = null;
	}
}

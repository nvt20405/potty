using System;
using UnityEngine;

public class PopupYes : MonoBehaviour
{
	public UISprite bg;

	public UILabel messageLabel;

	public UILabel yesLabel;

	private float relativePoupHeightAndTextHeight;

	public Action yesAction;

	public static PopupYes instance;

	public static PopupYes Create(string message, string yesTxt, Action yesAction, bool canTrai = false)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("popup/PopupYes"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupYes>();
		instance.SetInfo(message, yesTxt, yesAction);
		if (canTrai)
		{
			instance.messageLabel.pivot = UIWidget.Pivot.Left;
		}
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		return instance;
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			instance.gameObject.SetActive(false);
			UnityEngine.Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	private void Awake()
	{
		float num = messageLabel.relativeSize.y * messageLabel.transform.localScale.y;
		relativePoupHeightAndTextHeight = bg.transform.localScale.y - num;
	}

	public void SetInfo(string message, string yesTxt, Action yesAction)
	{
		this.yesAction = yesAction;
		yesLabel.text = yesTxt;
		messageLabel.text = message;
		float num = messageLabel.relativeSize.y * messageLabel.transform.localScale.y;
		bg.transform.localScale = new Vector3(bg.transform.localScale.x, num + relativePoupHeightAndTextHeight);
	}

	private void OnYesClick()
	{
		if (yesAction != null)
		{
			yesAction();
		}
		DestroyPopup();
	}
}

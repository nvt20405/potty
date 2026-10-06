using System;
using UnityEngine;

public class PopupYesNo : MonoBehaviour
{
	public UISprite bg;

	public UILabel messageLabel;

	public UILabel yesLabel;

	public UILabel noLabel;

	private float relativePoupHeightAndTextHeight;

	public Action yesAction;

	public Action noAction;

	public static PopupYesNo instance;

	public static PopupYesNo Create(string message, string yesTxt, string noTxt, Action yesAction, Action noAction)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("popup/PopupYesNo"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupYesNo>();
		instance.SetInfo(message, yesTxt, noTxt, yesAction, noAction);
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

	public void SetInfo(string message, string yesTxt, string noTxt, Action yesAction, Action noAction)
	{
		this.yesAction = yesAction;
		this.noAction = noAction;
		yesLabel.text = yesTxt;
		noLabel.text = noTxt;
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
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		DestroyPopup();
	}

	private void OnNoClick()
	{
		if (noAction != null)
		{
			noAction();
		}
		DestroyPopup();
	}
}

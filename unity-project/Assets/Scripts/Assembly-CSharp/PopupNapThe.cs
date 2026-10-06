using UnityEngine;

public class PopupNapThe : MonoBehaviour
{
	public static PopupNapThe instance;

	public GameObject group;

	public UIInput CardSerial;

	public UIInput CardPin;

	public UIPopupList CardType;

	public GameObject SerialLabel;

	public GameObject TypeLabel;

	public GameObject PinLabel;

	public bool isSelect;

	public static PopupNapThe Create()
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("Popup/PopupNapThe"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupNapThe>();
		instance.Set();
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		return instance;
	}

	public void Set()
	{
		isSelect = false;
	}

	public void Update()
	{
		if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBattle)
		{
			NGUITools.SetActive(group, false);
		}
		else
		{
			NGUITools.SetActive(group, true);
		}
		if (group.transform.childCount > 12)
		{
			CardSerial.gameObject.SetActive(false);
			CardPin.gameObject.SetActive(false);
			SerialLabel.SetActive(false);
			PinLabel.SetActive(false);
			TypeLabel.SetActive(false);
		}
		else
		{
			CardSerial.gameObject.SetActive(true);
			CardPin.gameObject.SetActive(true);
			SerialLabel.SetActive(true);
			PinLabel.SetActive(true);
			TypeLabel.SetActive(true);
		}
	}

	public void OnCloseClick()
	{
		DestroyPopup();
	}

	public void OnNapClick()
	{
		MessagePopup.Create("May chu da tat tinh nang nap.");
		DestroyPopup();
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

	public void OnSelection()
	{
		isSelect = !isSelect;
		CardSerial.gameObject.SetActive(isSelect);
		CardPin.gameObject.SetActive(isSelect);
		SerialLabel.SetActive(isSelect);
		PinLabel.SetActive(isSelect);
		TypeLabel.SetActive(isSelect);
	}
}

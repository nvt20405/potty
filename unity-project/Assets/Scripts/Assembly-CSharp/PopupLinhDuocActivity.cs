using UnityEngine;

public class PopupLinhDuocActivity : MonoBehaviour
{
	public LinhDuocActivityItem item;

	public static PopupLinhDuocActivity instance;

	public GameObject group;

	public static PopupLinhDuocActivity Create()
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("Popup/LinhDuocActivityPopup"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupLinhDuocActivity>();
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		if (GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo != null && GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocActivity != null)
		{
			foreach (string item in GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocActivity)
			{
				string empty = string.Empty;
				string[] array = item.Split(';');
				empty = ((int.Parse(array[1]) <= 0) ? string.Format(Localization.instance.Get("StatusAnTromThatBai"), array[0]) : string.Format(Localization.instance.Get("StatusAnTromThanhCong"), array[0], array[1]));
				instance.item.Create(empty);
			}
		}
		instance.item.gameObject.SetActive(false);
		return instance;
	}

	public static void Sync()
	{
		DestroyPopup();
		Create();
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
	}

	public void OnCloseClick()
	{
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
}

using UnityEngine;

public class PopUpLapLienMinh : MonoBehaviour
{
	public static PopUpLapLienMinh instance;

	public GameObject group;

	public UIInput tenLienMinh;

	public static PopUpLapLienMinh Create()
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("popup/PopupLapLienMinh"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopUpLapLienMinh>();
		instance.Set();
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		return instance;
	}

	public void Set()
	{
		tenLienMinh.text = Localization.instance.Get("TenLienMinh");
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

	public void OnCreateClick()
	{
		LapLienMinhRequest lapLienMinhRequest = new LapLienMinhRequest();
		lapLienMinhRequest.DisplayName = tenLienMinh.text;
		GameManager.instance.m_GameClient.RequestLapLienMinh(lapLienMinhRequest);
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

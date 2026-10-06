using UnityEngine;

public class PopupAnTromLinhDuoc : MonoBehaviour
{
	public AnTromTargetItem tromItem;

	public static PopupAnTromLinhDuoc instance;

	public GameObject group;

	public static PopupAnTromLinhDuoc Create()
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("Popup/TromLinhDuocPopup"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupAnTromLinhDuoc>();
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		if (GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo != null && GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocAnTromList != null)
		{
			foreach (LinhDuocAnTromData linhDuocAnTrom in GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocAnTromList)
			{
				instance.tromItem.Create(linhDuocAnTrom);
			}
		}
		instance.tromItem.gameObject.SetActive(false);
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

	public void OnRefreshClick()
	{
		GetAnTromListRequest request = new GetAnTromListRequest();
		GameManager.instance.m_GameClient.RequestGetAnTromList(request);
	}
}

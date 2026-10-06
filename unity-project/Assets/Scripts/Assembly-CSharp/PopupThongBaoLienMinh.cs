using UnityEngine;

public class PopupThongBaoLienMinh : MonoBehaviour
{
	public static PopupThongBaoLienMinh instance;

	public GameObject group;

	public UIInput tenLienMinh;

	public static PopupThongBaoLienMinh Create()
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("popup/PopupThongBaoLienMinh"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupThongBaoLienMinh>();
		instance.Set();
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		return instance;
	}

	public void Set()
	{
		tenLienMinh.text = GameManager.instance.m_GameClient.UserInfo.LienMinh.ThongBao;
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
		SuaThongBaoLienMinhRequest suaThongBaoLienMinhRequest = new SuaThongBaoLienMinhRequest();
		suaThongBaoLienMinhRequest.content = tenLienMinh.text;
		suaThongBaoLienMinhRequest.lienminhID = GameManager.instance.m_GameClient.UserInfo.LienMinh.ID;
		GameManager.instance.m_GameClient.RequestSuaThongBaoLienMinh(suaThongBaoLienMinhRequest);
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

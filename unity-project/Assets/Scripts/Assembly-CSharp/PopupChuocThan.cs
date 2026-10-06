using UnityEngine;

public class PopupChuocThan : MonoBehaviour
{
	public static PopupChuocThan instance;

	public UILabel messageLabel;

	public static void Create()
	{
		DestroyPopup();
		Object obj = Object.Instantiate(Resources.Load("Popup/PopupChuocThan"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupChuocThan>();
		int level = GameManager.instance.m_GameClient.UserInfo.Gamer.Level;
		instance.SetInfo(ConfigManager.GetBacChuocThan(level));
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
	}

	private void SetInfo(int bacChuoc)
	{
		messageLabel.text = string.Format(Localization.instance.Get("PopupChuocThanMessage"), bacChuoc);
	}

	private void OnChuocThanBtnClick()
	{
		GameManager.instance.m_GameClient.RequestChuocThan();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
		}
	}
}

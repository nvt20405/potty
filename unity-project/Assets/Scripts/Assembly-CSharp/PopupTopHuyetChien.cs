using UnityEngine;

public class PopupTopHuyetChien : MonoBehaviour
{
	public UIPanel panel;

	public static PopupTopHuyetChien instance;

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(HuyetChienTopResponse response)
	{
		DestroyPopup();
		Object obj = Object.Instantiate(Resources.Load("Popup/PopupTopHuyetChien"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupTopHuyetChien>();
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, -40f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -144f, 0f);
		for (int i = 0; i < response.TopList.Count; i++)
		{
			Object obj2 = Object.Instantiate(Resources.Load("Popup/HuyetChienTopMonPhai"));
			GameObject gameObject2 = (GameObject)((obj2 is GameObject) ? obj2 : null);
			gameObject2.transform.parent = instance.panel.transform;
			gameObject2.transform.localPosition = vector;
			gameObject2.transform.localScale = Vector3.one;
			gameObject2.GetComponent<HuyetChienTopMP>().SetInfo(i + 1, response.TopList[i]);
			vector += vector2;
		}
	}

	private void OnCloseBtnClick()
	{
		DestroyPopup();
	}
}

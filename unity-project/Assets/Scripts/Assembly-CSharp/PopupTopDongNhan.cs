using UnityEngine;

public class PopupTopDongNhan : MonoBehaviour
{
	public UIPanel panel;

	public static PopupTopDongNhan instance;

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(DongNhanResponse response)
	{
		if (response != null && response.LastTop10 != null)
		{
			if (instance != null)
			{
				DestroyPopup();
			}
			Object obj = Object.Instantiate(Resources.Load("Popup/PopupTopDongNhan"));
			GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
			PopupManager.instance.Add(gameObject);
			gameObject.transform.localScale = Vector3.one;
			instance = gameObject.GetComponent<PopupTopDongNhan>();
			Vector3 vector = default(Vector3);
			vector = new Vector3(0f, -12f, 0f);
			Vector3 vector2 = default(Vector3);
			vector2 = new Vector3(0f, -84f, 0f);
			for (int i = 0; i < response.LastTop10.Count; i++)
			{
				Object obj2 = Object.Instantiate(Resources.Load("Prefabs/DongNhan/DongNhanTopMonPhai"));
				GameObject gameObject2 = (GameObject)((obj2 is GameObject) ? obj2 : null);
				gameObject2.transform.parent = instance.panel.transform;
				gameObject2.transform.localPosition = vector;
				gameObject2.transform.localScale = Vector3.one;
				gameObject2.GetComponent<DongNhanTopMonPhai>().SetInfo(response.LastTop10[i], i + 1);
				vector += vector2;
			}
		}
	}

	private void OnCloseBtnClick()
	{
		DestroyPopup();
	}
}

using UnityEngine;

public class PopupQuayCamCungThatBai : MonoBehaviour
{
	public static PopupQuayCamCungThatBai instance;

	public void OnCloseClick()
	{
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create()
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupQuayCamCungThatBai"))).GetComponent<PopupQuayCamCungThatBai>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
	}
}

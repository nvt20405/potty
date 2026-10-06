using UnityEngine;

public class PopUpCheckKNB : MonoBehaviour
{
	public static PopUpCheckKNB instance;

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
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupCheckKNB"))).GetComponent<PopUpCheckKNB>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public void btnDeSau_OnClick(GameObject go)
	{
		DestroyPopup();
	}

	public void btnNapNgay_OnClick(GameObject go)
	{
		DestroyPopup();
		PopUpNapTien.Create();
	}
}

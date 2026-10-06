using UnityEngine;

public class PopupCamCungPhanThuongDB : MonoBehaviour
{
	public static PopupCamCungPhanThuongDB instance;

	public PhanThuongItem ptDacBiet;

	public void OnNhanClick()
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

	public static void Create(PhanThuongResponse.PhanThuong ptItem)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupCamCungPhanThuongDB"))).GetComponent<PopupCamCungPhanThuongDB>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.ptDacBiet.Set(ptItem, true);
	}
}

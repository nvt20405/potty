using UnityEngine;

public class PopupCheTacThienMaResult : MonoBehaviour
{
	public OtherAvatar vc1;

	public OtherAvatar vc2;

	public OtherAvatar vc3;

	public UILabel lbInfo1;

	public UILabel lbInfo2;

	public UILabel lbInfo3;

	public static PopupCheTacThienMaResult instance;

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
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupCheTacThienMaResult"))).GetComponent<PopupCheTacThienMaResult>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.displayInfoResult();
	}

	public void displayInfoResult()
	{
	}

	public void btnNhan_OnClick(GameObject go)
	{
		DestroyPopup();
	}
}

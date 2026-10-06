using UnityEngine;

public class PopUpGiftCode : MonoBehaviour
{
	public UIInput inputMess;

	public static PopUpGiftCode instance;

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
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupGiftCode"))).GetComponent<PopUpGiftCode>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public void btnHuy_OnClick(GameObject go)
	{
		DestroyPopup();
	}

	public void btnGui_OnClick(GameObject go)
	{
		if (inputMess.label != null && inputMess.label.text.Length > 0)
		{
			KichHoatGiftCodeRequest kichHoatGiftCodeRequest = new KichHoatGiftCodeRequest();
			kichHoatGiftCodeRequest.Giftcode = inputMess.label.text;
			GameManager.instance.m_GameClient.RequestKichHoatGiftCode(kichHoatGiftCodeRequest);
		}
		DestroyPopup();
	}
}

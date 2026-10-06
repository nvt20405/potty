using UnityEngine;

public class PopUpSendMail : MonoBehaviour
{
	public UIInput inputMess;

	public UILabel lbSendTo;

	public UIButton btnEmoticon;

	public static PopUpSendMail instance;

	private int targetID;

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(int id, string targetName)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupSendMail"))).GetComponent<PopUpSendMail>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.lbSendTo.text = Localization.instance.Get("SendMailToLabel") + " " + targetName;
		instance.targetID = id;
		instance.btnEmoticon.gameObject.SetActive(true);
	}

	public void btnHuy_OnClick(GameObject go)
	{
		DestroyPopup();
		if (PopUpEmoticons.instance != null)
		{
			PopUpEmoticons.DestroyPopup();
		}
	}

	public void btnGui_OnClick(GameObject go)
	{
		if (inputMess.label != null && inputMess.label.text.Length > 0 && targetID > 0)
		{
			SendMailRequest sendMailRequest = new SendMailRequest();
			sendMailRequest.Content = inputMess.label.text;
			sendMailRequest.TargetGID = targetID;
			GameManager.instance.m_GameClient.RequestSendMail(sendMailRequest);
		}
		DestroyPopup();
		if (PopUpEmoticons.instance != null)
		{
			PopUpEmoticons.DestroyPopup();
		}
	}

	public void btnEmoticons_OnClick(GameObject go)
	{
		if (PopUpEmoticons.instance == null)
		{
			PopUpEmoticons.CreateByMail();
		}
		else
		{
			PopUpEmoticons.DestroyPopup();
		}
	}
}

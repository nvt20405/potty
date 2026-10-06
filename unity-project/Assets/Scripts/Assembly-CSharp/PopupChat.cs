using System.Collections.Generic;
using UnityEngine;

public class PopupChat : MonoBehaviour
{
	public UIInput inputMess;

	public UIButton btnEmoticon;

	private ChatType m_ChatType;

	public static PopupChat instance;

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(ChatType _type)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupChat"))).GetComponent<PopupChat>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.m_ChatType = _type;
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
		if (inputMess.label != null && inputMess.label.text.Length > 0)
		{
			if (m_ChatType == ChatType.BANG_HOI)
			{
				SendChatLienMinhRequest sendChatLienMinhRequest = new SendChatLienMinhRequest();
				sendChatLienMinhRequest.Content = inputMess.label.text;
				sendChatLienMinhRequest.lienminhID = GameManager.instance.m_GameClient.UserInfo.LienMinh.ID;
				sendChatLienMinhRequest.MemberList = new List<int>();
				foreach (LienMinhThanhVienData thanhVien in GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList)
				{
					sendChatLienMinhRequest.MemberList.Add(thanhVien.ID);
				}
				GameManager.instance.m_GameClient.RequestSendLienMinhChat(sendChatLienMinhRequest);
			}
			else if (m_ChatType == ChatType.LIEN_SERVER)
			{
				SendChatRequest sendChatRequest = new SendChatRequest();
				sendChatRequest.Content = inputMess.label.text;
				GameManager.instance.m_GameClient.RequestSendChatLienSrv(sendChatRequest);
			}
			else
			{
				SendChatRequest sendChatRequest2 = new SendChatRequest();
				sendChatRequest2.Content = inputMess.label.text;
				GameManager.instance.m_GameClient.RequestSendChatAll(sendChatRequest2);
			}
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
			PopUpEmoticons.CreateByChat();
		}
		else
		{
			PopUpEmoticons.DestroyPopup();
		}
	}
}

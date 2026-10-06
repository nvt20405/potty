using System;
using UnityEngine;

public class GamerChatItem : MonoBehaviour
{
	public UILabel lbName;

	public UILabel lbTime;

	public UILabel lbMess;

	public UISprite spBackground;

	public void SetChatData(ChatItem chatData)
	{
		if (chatData != null)
		{
			lbName.text = chatData.Name;
			lbMess.text = chatData.Content;
			DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
			DateTime sendTime = chatData.SendTime;
			TimeSpan timeSpan = serverTime - sendTime;
			if (timeSpan.TotalHours < 1.0 && timeSpan.Minutes < 1)
			{
				lbTime.text = Localization.instance.Get("ChatTimeMess1");
			}
			else if (timeSpan.TotalHours < 1.0)
			{
				lbTime.text = timeSpan.Minutes + " " + Localization.instance.Get("ChatTimeMess2");
			}
			else if (timeSpan.TotalHours < 24.0)
			{
				lbTime.text = (int)timeSpan.TotalHours + " " + Localization.instance.Get("ChatTimeMess3");
			}
			else
			{
				lbTime.text = string.Empty;
			}
			spBackground.transform.localScale = new Vector3(spBackground.transform.localScale.x, lbMess.relativeSize.y * lbMess.transform.localScale.y + 50f, spBackground.transform.localScale.z);
			BoxCollider component = GetComponent<BoxCollider>();
			component.size = new Vector3(spBackground.transform.localScale.x + 50f, spBackground.transform.localScale.y + 50f, base.gameObject.transform.localScale.z);
			component.center = new Vector3(0f, 0f - spBackground.transform.localScale.y / 2f + 80f, 0f);
		}
	}
}

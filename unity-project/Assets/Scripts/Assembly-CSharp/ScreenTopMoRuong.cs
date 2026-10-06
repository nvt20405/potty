using System.Collections.Generic;
using UnityEngine;

public class ScreenTopMoRuong : ScreenBase
{
	public GameObject ItemRoot;

	public List<TopMoRuongItem> ItemList = new List<TopMoRuongItem>();

	public string RuongCodeName = string.Empty;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.TopBaoRuongConfig != null)
		{
			RuongCodeName = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TopBaoRuongConfig.RuongCodeName;
		}
		GameManager.instance.m_GameClient.RequestGetTopMoRuong();
	}

	public void getTopMoRuong(TopEventMoRuongResponse response)
	{
		if (response != null && response.ListTop != null && response.ListTop.Count > 0)
		{
			for (int i = 0; i < response.ListTop.Count; i++)
			{
				ItemList[i].setData(response.ListTop[i], i);
			}
		}
		else
		{
			for (int j = 0; j < ItemList.Count; j++)
			{
				ItemList[j].setData(null, j);
			}
		}
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}
}

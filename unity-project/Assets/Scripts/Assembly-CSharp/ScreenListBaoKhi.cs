using System.Collections.Generic;
using UnityEngine;

public class ScreenListBaoKhi : ScreenBase
{
	public GameObject BaoKhiPrefab;

	public GameObject ItemRoot;

	private List<BaoKhiItem> ItemList = new List<BaoKhiItem>();

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
		GUIManager.ShowGadgets(6);
		SyncWithNetworkData();
	}

	public void ClearGUIItem()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
	}

	public void SyncWithNetworkData()
	{
		ClearGUIItem();
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 320f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -220f, 0f);
		if (GameManager.instance.m_GameClient.UserInfo.ListThienMaLenh != null && GameManager.instance.m_GameClient.UserInfo.ListThienMaLenh.Count > 0)
		{
			for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.ListThienMaLenh.Count; i++)
			{
				UserInfo.ThienMaLenhInfo dataItem = GameManager.instance.m_GameClient.UserInfo.ListThienMaLenh[i];
				BaoKhiItem component = ((GameObject)Object.Instantiate(BaoKhiPrefab)).GetComponent<BaoKhiItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = vector;
				vector += vector2;
				component.setDataItem(dataItem);
				UIEventListener.Get(component.gameObject).onClick = onClick_BaoKhiItem;
				ItemList.Add(component);
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}

	public void onClick_BaoKhiItem(GameObject go)
	{
		BaoKhiItem component = go.GetComponent<BaoKhiItem>();
		if (component.m_Data != null)
		{
			ScreenBaoKhi screenBaoKhi = GUIManager.getScreen(GAME_SCREEN.ScreenBaoKhi) as ScreenBaoKhi;
			screenBaoKhi.Set(component.m_Data);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBaoKhi);
		}
	}

	public void btnBack_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenThienMaThuongPhong);
	}
}

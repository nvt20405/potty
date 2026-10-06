using System.Collections.Generic;
using UnityEngine;

public class ScreenTopDiHoaCung : ScreenBase
{
	public GameObject TopDHCPrefab;

	public GameObject ItemRoot;

	public UILabel lbCurrentInfo;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<TopDiHoaCungItem> ListTopDiHoaCungItem = new List<TopDiHoaCungItem>();

	private Vector3 itemPos;

	private Vector3 itemOffset = new Vector3(0f, -140f, 0f);

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

	private void Start()
	{
		dragPanel = ItemRoot.GetComponent<UIDraggablePanel>();
		panel = ItemRoot.GetComponent<UIPanel>();
	}

	public override void OnActive()
	{
		base.OnActive();
		lbCurrentInfo.text = string.Format(Localization.instance.Get("DongGopHienTaiLabel"), GameManager.instance.m_GameClient.UserInfo.Gamer.DiemTieuKNB);
		GameManager.instance.m_GameClient.RequestGetTopDiHoaCung();
	}

	public void clearList()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		ListTopDiHoaCungItem.Clear();
	}

	public void displayListTopDiHoaCung(TopDiHoaCungResponse response)
	{
		clearList();
		itemPos = new Vector3(0f, 210f, 0f);
		UserInfo.ServerData.DiHoaCungCfg diHoaCungConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DiHoaCungConfig;
		if (diHoaCungConfig != null && diHoaCungConfig.ListTop != null && response.ListTop != null)
		{
			for (int i = 0; i < diHoaCungConfig.ListTop.Count; i++)
			{
				UserInfo.ServerData.DiHoaCungTop diHoaCungTop = diHoaCungConfig.ListTop[i];
				if (diHoaCungTop != null)
				{
					TopDiHoaCungItem component = ((GameObject)Object.Instantiate(TopDHCPrefab)).GetComponent<TopDiHoaCungItem>();
					component.transform.parent = ItemRoot.transform;
					component.transform.localScale = new Vector3(1f, 1f, 1f);
					component.transform.localPosition = itemPos;
					component.setData(diHoaCungTop, response.ListTop[i], i + 1);
					itemPos += itemOffset;
					ListTopDiHoaCungItem.Add(component);
				}
			}
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}

	public void onClick_btnChiTiet()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDiHoaCungDetail);
	}

	public void onClick_btnBack()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenKyNgoDiHoaCung);
	}
}

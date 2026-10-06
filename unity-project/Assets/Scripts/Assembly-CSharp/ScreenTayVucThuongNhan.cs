using System;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTayVucThuongNhan : ScreenBase
{
	private const int maxItemCount = 12;

	public GameObject TayVucItemPrefab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<TayVucThuongNhanItem> ItemList = new List<TayVucThuongNhanItem>();

	private Vector3 itemPos;

	private Vector3 itemOffset;

	public UILabel lbRefreshTime;

	private float nextSecond;

	private DateTime timeReset;

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
		updateView();
	}

	public void updateView()
	{
		GameManager.instance.m_GameClient.RequestGetTayVucInfo();
	}

	private void Update()
	{
		nextSecond += Time.deltaTime;
		if (nextSecond >= 1f)
		{
			nextSecond = 0f;
			displayTimeReset();
		}
	}

	public void displayTimeReset()
	{
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		TimeSpan timeSpan = timeReset - serverTime;
		if (timeSpan.TotalSeconds <= 0.0)
		{
			DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TayVucThuongNhanConfig.ThoiGianBatDau;
			DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TayVucThuongNhanConfig.ThoiGianKetThuc;
			DateTime serverTime2 = GameManager.instance.m_GameClient.ServerTime;
			if (serverTime2 < thoiGianKetThuc && serverTime2 > thoiGianBatDau)
			{
				GameManager.instance.m_GameClient.RequestGetTayVucInfo();
			}
		}
		if (timeSpan.Days >= 0 && timeSpan.Seconds > 0 && timeSpan.Minutes >= 0 && timeSpan.Hours >= 0)
		{
			string empty = string.Empty;
			empty = ((timeSpan.Hours >= 10) ? (empty + timeSpan.Hours + ":") : (empty + "0" + timeSpan.Hours + ":"));
			empty = ((timeSpan.Minutes >= 10) ? (empty + timeSpan.Minutes + ":") : (empty + "0" + timeSpan.Minutes + ":"));
			empty = ((timeSpan.Seconds >= 10) ? (empty + timeSpan.Seconds) : (empty + "0" + timeSpan.Seconds));
			lbRefreshTime.text = Localization.instance.Get("AutoRefreshLabel") + " " + empty;
		}
	}

	public void getListTayVuc(TayVucInfoResponse response)
	{
		timeReset = response.ThoiGianReset;
		displayTimeReset();
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		itemPos = new Vector3(0f, 320f, 0f);
		itemOffset = new Vector3(0f, -210f, 0f);
		if (response != null && response.ListItem != null && response.ListItem.Count > 0)
		{
			for (int i = 0; i < response.ListItem.Count; i++)
			{
				TayVucThuongNhanItem component = ((GameObject)UnityEngine.Object.Instantiate(TayVucItemPrefab)).GetComponent<TayVucThuongNhanItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				itemPos += itemOffset;
				component.Set(response.ListItem[i], i);
				UIEventListener.Get(component.btnMua.gameObject).onClick = onClick_btnMua;
				ItemList.Add(component);
			}
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}

	public void onClick_btnMua(GameObject go)
	{
		TayVucThuongNhanItem component = go.transform.parent.GetComponent<TayVucThuongNhanItem>();
		if (component.m_Data != null)
		{
			MuaDoTayVucRequest muaDoTayVucRequest = new MuaDoTayVucRequest();
			muaDoTayVucRequest.SlotIdx = component.Index;
			GameManager.instance.m_GameClient.RequestMuaDoTayVuc(muaDoTayVucRequest);
		}
	}
}

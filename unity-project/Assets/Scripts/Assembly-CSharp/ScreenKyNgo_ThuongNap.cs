using System;
using System.Collections.Generic;
using UnityEngine;

public class ScreenKyNgo_ThuongNap : ScreenBase
{
	public GameObject phanThuongPerfab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<KyNgoNapTienItem> ItemList = new List<KyNgoNapTienItem>();

	private Vector3 itemPos;

	private Vector3 itemOffset;

	public UILabel lbTimeConLai;

	public UILabel lbDaNap;

	private float nextSecond;

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
		displayInfo();
	}

	private void Update()
	{
		nextSecond += Time.deltaTime;
		if (nextSecond >= 1f)
		{
			nextSecond = 0f;
			updateTime();
		}
	}

	private void updateTime()
	{
		UserInfo.ServerData.ThuongNapCfg thuongNapConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.ThuongNapConfig;
		if (thuongNapConfig != null)
		{
			DateTime thoiGianBatDau = thuongNapConfig.ThoiGianBatDau;
			DateTime thoiGianKetThuc = thuongNapConfig.ThoiGianKetThuc;
			DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
			if (serverTime <= thoiGianKetThuc && serverTime >= thoiGianBatDau)
			{
				TimeSpan timeSpan = thoiGianKetThuc - serverTime;
				string text = string.Format(Localization.instance.Get("ThoiGianConLaiMess"), timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
				lbTimeConLai.text = text;
			}
		}
	}

	public void displayInfo()
	{
		lbDaNap.text = string.Format(Localization.instance.Get("InfoDaNapTien"), GameManager.instance.m_GameClient.UserInfo.Gamer.DiemThuongNap);
		updateTime();
		getListThuongNapTien();
	}

	public void getListThuongNapTien()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		int num = 0;
		itemPos = new Vector3(-145f, 20f, 0f);
		itemOffset = new Vector3(200f, 0f, 0f);
		UserInfo.ServerData.ThuongNapCfg thuongNapConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.ThuongNapConfig;
		if (thuongNapConfig == null || thuongNapConfig.ListMocNap == null || thuongNapConfig.ListMocNap.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < thuongNapConfig.ListMocNap.Count; i++)
		{
			UserInfo.ServerData.MocThuongNap mocThuongNap = thuongNapConfig.ListMocNap[i];
			if (mocThuongNap != null)
			{
				KyNgoNapTienItem component = ((GameObject)UnityEngine.Object.Instantiate(phanThuongPerfab)).GetComponent<KyNgoNapTienItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				component.setData(mocThuongNap);
				itemPos += itemOffset;
				ItemList.Add(component);
			}
		}
	}
}

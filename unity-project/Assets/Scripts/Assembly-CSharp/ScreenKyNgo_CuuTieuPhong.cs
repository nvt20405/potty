using System;
using System.Collections.Generic;
using UnityEngine;

public class ScreenKyNgo_CuuTieuPhong : ScreenBase
{
	public GameObject itemPerfab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<KyNgoCuuTieuPhongItem> ItemList = new List<KyNgoCuuTieuPhongItem>();

	private List<OtherCfg.CuuVienTieuPhongCfg> ListData = new List<OtherCfg.CuuVienTieuPhongCfg>();

	private int startItemGUI_Idx;

	private Vector3 itemPos;

	private Vector3 itemOffset;

	public UILabel lbTime;

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

	private void Update()
	{
		nextSecond += Time.deltaTime;
		if (nextSecond >= 1f)
		{
			nextSecond = 0f;
			updateTime();
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		getListTargetItem();
		updateTime();
	}

	public void updateTime()
	{
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		DateTime dateTime = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.registerTime.AddDays(14.0);
		TimeSpan timeSpan = dateTime - serverTime;
		if (timeSpan.Days <= 14 && timeSpan.Seconds > 0 && timeSpan.Minutes >= 0 && timeSpan.Hours >= 0)
		{
			string text = ((timeSpan.Minutes >= 10) ? timeSpan.Minutes.ToString() : ("0" + timeSpan.Minutes));
			string text2 = ((timeSpan.Seconds >= 10) ? timeSpan.Seconds.ToString() : ("0" + timeSpan.Seconds));
			lbTime.text = string.Format(Localization.instance.Get("ThoiGianConLaiMess"), timeSpan.Days, timeSpan.Hours, text, text2);
		}
	}

	public void getListTargetItem()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		ListData.Clear();
		startItemGUI_Idx = 0;
		itemPos = new Vector3(0f, 260f, 0f);
		itemOffset = new Vector3(0f, -145f, 0f);
		if (ConfigManager.instance.OtherConfig.CuuVienTieuPhong != null && ConfigManager.instance.OtherConfig.CuuVienTieuPhong.Count > 0)
		{
			for (int i = 0; i < ConfigManager.instance.OtherConfig.CuuVienTieuPhong.Count; i++)
			{
				OtherCfg.CuuVienTieuPhongCfg cfg = ConfigManager.instance.OtherConfig.CuuVienTieuPhong[i];
				KyNgoCuuTieuPhongItem component = ((GameObject)UnityEngine.Object.Instantiate(itemPerfab)).GetComponent<KyNgoCuuTieuPhongItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				itemPos += itemOffset;
				component.Set(cfg, i);
				UIEventListener.Get(component.btnNhan.gameObject).onClick = onClickItem;
				ItemList.Add(component);
			}
		}
	}

	public void onClickItem(GameObject go)
	{
		KyNgoCuuTieuPhongItem component = go.transform.parent.GetComponent<KyNgoCuuTieuPhongItem>();
		if (!(component != null))
		{
			return;
		}
		if (!component.isDuDieuKien)
		{
			MessagePopup.Create(Localization.instance.Get("ChuongMonChuaDuDieuKienMess"));
		}
		else if (component.requestInx != -1)
		{
			string value = string.Format("CuuTieuPhong{0}", component.requestInx);
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(value))
			{
				MessagePopup.Create(Localization.instance.Get("ChuongMonDaNhanMess"));
				return;
			}
			CuuVienTieuPhongRequest cuuVienTieuPhongRequest = new CuuVienTieuPhongRequest();
			cuuVienTieuPhongRequest.Idx = component.requestInx;
			GameManager.instance.m_GameClient.RequestCuuVienTieuPhong(cuuVienTieuPhongRequest);
		}
	}

	public void updateInfo(PhanThuongResponse response)
	{
		getListTargetItem();
		PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), response);
		updateMainMenuView();
	}

	public void updateMainMenuView()
	{
		GadgetPanelBottom gadgetPanelBottom = GUIManager.instance.gadgetPanelBottom;
		if (!(gadgetPanelBottom != null))
		{
			return;
		}
		gadgetPanelBottom.checkDisplayThongBaoSuKien();
		if (gadgetPanelBottom.listKyNgoMenu == null || gadgetPanelBottom.listKyNgoMenu.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < gadgetPanelBottom.listKyNgoMenu.Count; i++)
		{
			if (gadgetPanelBottom.listKyNgoMenu[i].kyNgoID == MenuButtonKyNgo.KyNgoType.CuuTieuPhong)
			{
				if (gadgetPanelBottom.checkThongBaoCuuTieuPhong())
				{
					gadgetPanelBottom.listKyNgoMenu[i].notifyIcon.gameObject.SetActive(true);
				}
				else
				{
					gadgetPanelBottom.listKyNgoMenu[i].notifyIcon.gameObject.SetActive(false);
				}
			}
		}
	}
}

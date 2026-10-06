using System;
using System.Collections.Generic;
using UnityEngine;

public class ScreenKyNgo_DangNhapNhanThuong : ScreenBase
{
	private const int maxItemCount = 12;

	public GameObject phanThuongPerfab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<KyNgoDangNhapNhanThuongItem> ItemList = new List<KyNgoDangNhapNhanThuongItem>();

	private int startItemGUI_Idx;

	private Vector3 itemPos;

	private Vector3 itemOffset;

	private int day_span;

	private string strCheck = "DangNhapNhanThuong;";

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
		getListPhanThuong();
	}

	private void Update()
	{
		if (ItemList.Count > 0)
		{
			Vector4 clipRange = panel.clipRange;
			float num = 0f;
			float num2 = 0f;
			KyNgoDangNhapNhanThuongItem kyNgoDangNhapNhanThuongItem = ItemList[ItemList.Count - 1];
			KyNgoDangNhapNhanThuongItem kyNgoDangNhapNhanThuongItem2 = ItemList[0];
			num = kyNgoDangNhapNhanThuongItem.transform.localPosition.y;
			num2 = kyNgoDangNhapNhanThuongItem2.transform.localPosition.y;
			if (num - clipRange.y > -700f)
			{
				SwapDragListDown();
			}
			else if (num2 - clipRange.y < 700f)
			{
				SwapDragListUp();
			}
		}
	}

	public void SwapDragListDown()
	{
		int num = startItemGUI_Idx + 12 + day_span;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 320f, 0f);
		if (num < ConfigManager.instance.OtherConfig.DangNhapNhanThuong.Count)
		{
			KyNgoDangNhapNhanThuongItem kyNgoDangNhapNhanThuongItem = ItemList[0];
			KyNgoDangNhapNhanThuongItem kyNgoDangNhapNhanThuongItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(0);
			ItemList.Add(kyNgoDangNhapNhanThuongItem);
			kyNgoDangNhapNhanThuongItem.transform.localPosition = kyNgoDangNhapNhanThuongItem2.transform.localPosition + itemOffset;
			if (num > day_span)
			{
				kyNgoDangNhapNhanThuongItem.Set(ConfigManager.instance.OtherConfig.DangNhapNhanThuong[num], num, day_span, false);
			}
			else if (num == day_span && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(strCheck))
			{
				kyNgoDangNhapNhanThuongItem.Set(ConfigManager.instance.OtherConfig.DangNhapNhanThuong[num], num, day_span, true);
			}
			else
			{
				kyNgoDangNhapNhanThuongItem.Set(ConfigManager.instance.OtherConfig.DangNhapNhanThuong[num], num, day_span, false);
			}
			startItemGUI_Idx++;
		}
	}

	public void SwapDragListUp()
	{
		if (startItemGUI_Idx == 0 || (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(strCheck) && startItemGUI_Idx == 1))
		{
			return;
		}
		startItemGUI_Idx--;
		int num = startItemGUI_Idx + day_span;
		if (num >= 0)
		{
			Vector3 vector = default(Vector3);
			vector = new Vector3(0f, 320f, 0f);
			KyNgoDangNhapNhanThuongItem kyNgoDangNhapNhanThuongItem = ItemList[0];
			KyNgoDangNhapNhanThuongItem kyNgoDangNhapNhanThuongItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(ItemList.Count - 1);
			ItemList.Insert(0, kyNgoDangNhapNhanThuongItem2);
			kyNgoDangNhapNhanThuongItem2.transform.localPosition = kyNgoDangNhapNhanThuongItem.transform.localPosition - itemOffset;
			if (num > day_span)
			{
				kyNgoDangNhapNhanThuongItem2.Set(ConfigManager.instance.OtherConfig.DangNhapNhanThuong[num], num, day_span, false);
			}
			else if (num == day_span && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(strCheck))
			{
				kyNgoDangNhapNhanThuongItem2.Set(ConfigManager.instance.OtherConfig.DangNhapNhanThuong[num], num, day_span, true);
			}
			else
			{
				kyNgoDangNhapNhanThuongItem2.Set(ConfigManager.instance.OtherConfig.DangNhapNhanThuong[num], num, day_span, false);
			}
		}
	}

	public void getListPhanThuong()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		int num = 0;
		itemPos = new Vector3(0f, 260f, 0f);
		itemOffset = new Vector3(0f, -155f, 0f);
		if (ConfigManager.instance.OtherConfig.DangNhapNhanThuong == null || ConfigManager.instance.OtherConfig.DangNhapNhanThuong.Count <= 0)
		{
			return;
		}
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		DateTime registerTime = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.registerTime;
		DateTime dateTime = new DateTime(registerTime.Year, registerTime.Month, registerTime.Day);
		day_span = (int)(serverTime - dateTime).TotalDays;
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(strCheck))
		{
			startItemGUI_Idx = 1;
		}
		else
		{
			startItemGUI_Idx = 0;
		}
		for (int i = 0; i < ConfigManager.instance.OtherConfig.DangNhapNhanThuong.Count; i++)
		{
			OtherCfg.DangNhapNhanThuongCfg cfg = ConfigManager.instance.OtherConfig.DangNhapNhanThuong[i];
			if (i >= day_span && (i != day_span || !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(strCheck)))
			{
				KyNgoDangNhapNhanThuongItem component = ((GameObject)UnityEngine.Object.Instantiate(phanThuongPerfab)).GetComponent<KyNgoDangNhapNhanThuongItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				itemPos += itemOffset;
				if (i > day_span)
				{
					component.Set(cfg, i, day_span, false);
				}
				else if (i == day_span)
				{
					component.Set(cfg, i, day_span, true);
				}
				else
				{
					component.Set(cfg, i, day_span, true);
				}
				UIEventListener.Get(component.btnNhan.gameObject).onClick = onClick_NhanBtn;
				ItemList.Add(component);
				num++;
				if (num >= 12)
				{
					break;
				}
			}
		}
	}

	public void updateView(PhanThuongResponse response)
	{
		getListPhanThuong();
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
			if (gadgetPanelBottom.listKyNgoMenu[i].kyNgoID == MenuButtonKyNgo.KyNgoType.DangNhapNhanThuong)
			{
				if (gadgetPanelBottom.checkThongBaoDNNhanThuong())
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

	public void onClick_NhanBtn(GameObject go)
	{
		KyNgoDangNhapNhanThuongItem component = go.transform.parent.GetComponent<KyNgoDangNhapNhanThuongItem>();
		if (component != null)
		{
			DangNhapNhanThuongRequest dangNhapNhanThuongRequest = new DangNhapNhanThuongRequest();
			dangNhapNhanThuongRequest.Idx = component.currentIndex;
			GameManager.instance.m_GameClient.RequestDangNhapNhanThuong(dangNhapNhanThuongRequest);
		}
	}
}

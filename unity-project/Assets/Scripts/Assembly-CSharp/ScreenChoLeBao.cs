using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class ScreenChoLeBao : ScreenBase
{
	private const int maxItemCount = 12;

	public GameObject LeBaoPrefab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<LeBaoItem> ItemList = new List<LeBaoItem>();

	private List<GoiVatPhamCfg> ListData = new List<GoiVatPhamCfg>();

	private int startItemGUI_Idx;

	private Vector3 itemPos;

	private Vector3 itemOffset;

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
		getListLeBao();
	}

	private void Update()
	{
	}

	public void getListLeBao()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		ListData.Clear();
		itemPos = new Vector3(0f, 320f, 0f);
		itemOffset = new Vector3(0f, -260f, 0f);
		startItemGUI_Idx = 0;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.GoiVatPham != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.GoiVatPham.Length > 5)
		{
			List<GoiVatPhamCfg> list = JsonMapper.ToObject<List<GoiVatPhamCfg>>(GameManager.instance.m_GameClient.UserInfo.ServerInfo.GoiVatPham);
			if (list != null && list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					TimeSpan timeSpan = list[i].ThoiGianTonTai - serverTime;
					if (serverTime < list[i].ThoiGianTonTai)
					{
						ListData.Add(list[i]);
					}
				}
			}
		}
		foreach (GoiVatPhamCfg goiVatPham in ConfigManager.instance.m_GoiVatPhamList)
		{
			TimeSpan timeSpan2 = goiVatPham.ThoiGianTonTai - serverTime;
			if (serverTime < goiVatPham.ThoiGianTonTai)
			{
				ListData.Add(goiVatPham);
			}
		}
		if (ListData.Count > 0)
		{
			for (int j = 0; j < ListData.Count; j++)
			{
				if (ListData[j] == null)
				{
					continue;
				}
				if (ListData[j].CodeName.StartsWith("GV_VIP"))
				{
					string text = ListData[j].CodeName.Substring(6, ListData[j].CodeName.Length - 6);
					EGDebug.Log("CAP VIP LE BAO: " + text);
					int num = Convert.ToInt32(text);
					if (num > GameManager.instance.m_GameClient.UserInfo.Gamer.Vip + 3)
					{
						continue;
					}
					string value = ListData[j].CodeName + ";";
					if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(value))
					{
						continue;
					}
				}
				LeBaoItem component = ((GameObject)UnityEngine.Object.Instantiate(LeBaoPrefab)).GetComponent<LeBaoItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				itemPos += itemOffset;
				component.Set(ListData[j]);
				UIEventListener.Get(component.leBaoAvatar.gameObject).onClick = onClick_LeBaoAvatar;
				UIEventListener.Get(component.btnMua.gameObject).onClick = onClick_btnMua;
				ItemList.Add(component);
			}
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}

	public void onClick_LeBaoAvatar(GameObject go)
	{
		LeBaoItem component = go.transform.parent.GetComponent<LeBaoItem>();
		if (component.m_Data != null)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = component.m_Data.DanhSachItem;
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("ThongTinLebaoLabel"), Localization.instance.Get("LeBaoBaoGomLabel"), phanThuongResponse);
		}
	}

	public void onClick_btnMua(GameObject go)
	{
		LeBaoItem component = go.transform.parent.GetComponent<LeBaoItem>();
		if (component.m_Data != null)
		{
			PopUpMua.CreateByLeBaoMarket(component.m_Data.CodeName, component.m_Data.TenHienThi, component.m_Data.GiaMua);
		}
	}

	public void openPopUpPhanThuong(PhanThuongResponse response)
	{
		PopupDanhSachPhanThuong.Create(Localization.instance.Get("MuaThanhCongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), response);
	}

	public void updateView()
	{
		getListLeBao();
		GadgetPanelBottom gadgetPanelBottom = GUIManager.instance.gadgetPanelBottom;
		if (gadgetPanelBottom != null)
		{
			gadgetPanelBottom.checkDisplayThongBaoCho();
		}
	}
}

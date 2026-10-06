using System.Collections.Generic;
using UnityEngine;

public class ScreenKyNgo_ThangCap : ScreenBase
{
	private const int maxItemCount = 12;

	public GameObject thangCapPerfab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<KyNgoThangCapItem> ItemList = new List<KyNgoThangCapItem>();

	private List<OtherCfg.LenCapNhanThuongCfg> ListData = new List<OtherCfg.LenCapNhanThuongCfg>();

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

	private void Update()
	{
		if (ItemList.Count > 0)
		{
			Vector4 clipRange = panel.clipRange;
			float num = 0f;
			float num2 = 0f;
			KyNgoThangCapItem kyNgoThangCapItem = ItemList[ItemList.Count - 1];
			KyNgoThangCapItem kyNgoThangCapItem2 = ItemList[0];
			num = kyNgoThangCapItem.transform.localPosition.y;
			num2 = kyNgoThangCapItem2.transform.localPosition.y;
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
		int num = startItemGUI_Idx + 12;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 320f, 0f);
		if (num < ListData.Count)
		{
			KyNgoThangCapItem kyNgoThangCapItem = ItemList[0];
			KyNgoThangCapItem kyNgoThangCapItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(0);
			ItemList.Add(kyNgoThangCapItem);
			kyNgoThangCapItem.transform.localPosition = kyNgoThangCapItem2.transform.localPosition + itemOffset;
			kyNgoThangCapItem.Set(ListData[num]);
			startItemGUI_Idx++;
		}
	}

	public void SwapDragListUp()
	{
		if (startItemGUI_Idx == 0)
		{
			return;
		}
		startItemGUI_Idx--;
		int num = startItemGUI_Idx;
		if (num >= 0)
		{
			Vector3 vector = default(Vector3);
			vector = new Vector3(0f, 320f, 0f);
			KyNgoThangCapItem kyNgoThangCapItem = ItemList[0];
			KyNgoThangCapItem kyNgoThangCapItem2 = ItemList[ItemList.Count - 1];
			if (ItemList.Count == 12)
			{
				ItemList.RemoveAt(ItemList.Count - 1);
				ItemList.Insert(0, kyNgoThangCapItem2);
				kyNgoThangCapItem2.transform.localPosition = kyNgoThangCapItem.transform.localPosition - itemOffset;
				kyNgoThangCapItem2.Set(ListData[num]);
			}
			else if (ItemList.Count < 12)
			{
				KyNgoThangCapItem component = ((GameObject)Object.Instantiate(thangCapPerfab)).GetComponent<KyNgoThangCapItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = vector;
				vector += itemOffset;
				component.Set(ListData[num]);
				UIEventListener.Get(component.btnNhan.gameObject).onClick = onClick_NhanBtn;
				ItemList.Insert(0, component);
			}
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		getListThangCap();
	}

	public void getListThangCap()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		ListData.Clear();
		startItemGUI_Idx = 0;
		int num = 0;
		itemPos = new Vector3(0f, 260f, 0f);
		itemOffset = new Vector3(0f, -160f, 0f);
		if (ConfigManager.instance.OtherConfig.LenCapNhanThuong != null && ConfigManager.instance.OtherConfig.LenCapNhanThuong.Count > 0)
		{
			for (int i = 0; i < ConfigManager.instance.OtherConfig.LenCapNhanThuong.Count; i++)
			{
				OtherCfg.LenCapNhanThuongCfg lenCapNhanThuongCfg = ConfigManager.instance.OtherConfig.LenCapNhanThuong[i];
				string value = string.Format("ThuongLenLvl{0}", lenCapNhanThuongCfg.CapYeuCau);
				if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(value))
				{
					ListData.Add(lenCapNhanThuongCfg);
				}
			}
		}
		if (ListData == null || ListData.Count <= 0)
		{
			return;
		}
		for (int j = 0; j < ListData.Count; j++)
		{
			KyNgoThangCapItem component = ((GameObject)Object.Instantiate(thangCapPerfab)).GetComponent<KyNgoThangCapItem>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = itemPos;
			itemPos += itemOffset;
			component.Set(ListData[j]);
			UIEventListener.Get(component.btnNhan.gameObject).onClick = onClick_NhanBtn;
			ItemList.Add(component);
			num++;
			if (num >= 12)
			{
				break;
			}
		}
	}

	public void onClick_NhanBtn(GameObject go)
	{
		KyNgoThangCapItem component = go.transform.parent.GetComponent<KyNgoThangCapItem>();
		if (!(component != null))
		{
			return;
		}
		LenCapNhanThuongRequest lenCapNhanThuongRequest = new LenCapNhanThuongRequest();
		for (int i = 0; i < ConfigManager.instance.OtherConfig.LenCapNhanThuong.Count; i++)
		{
			if (ConfigManager.instance.OtherConfig.LenCapNhanThuong[i].CapYeuCau == component.m_Data.CapYeuCau)
			{
				lenCapNhanThuongRequest.Idx = i;
			}
		}
		GameManager.instance.m_GameClient.RequestLenCapNhanThuong(lenCapNhanThuongRequest);
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
			if (gadgetPanelBottom.listKyNgoMenu[i].kyNgoID == MenuButtonKyNgo.KyNgoType.ThangCap)
			{
				if (gadgetPanelBottom.checkThongBaoThangCap())
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

using System.Collections.Generic;
using UnityEngine;

public class ScreenChoVatPham : ScreenBase
{
	private const int maxItemCount = 12;

	public GameObject VatPhamPrefab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<VatPhamMarketItem> ItemList = new List<VatPhamMarketItem>();

	private List<VatPhamTieuThuCfg> ListData = new List<VatPhamTieuThuCfg>();

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
		getListVatPham();
	}

	private void Update()
	{
	}

	public void SwapDragListDown()
	{
		int num = startItemGUI_Idx + 12;
		if (num < ItemList.Count)
		{
			VatPhamMarketItem vatPhamMarketItem = ItemList[0];
			vatPhamMarketItem.vatPhamID = num;
			VatPhamMarketItem vatPhamMarketItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(0);
			ItemList.Add(vatPhamMarketItem);
			vatPhamMarketItem.transform.localPosition = vatPhamMarketItem2.transform.localPosition + itemOffset;
			vatPhamMarketItem.Set(ListData[num]);
		}
	}

	public void SwapDragListUp()
	{
		if (startItemGUI_Idx != 0)
		{
			startItemGUI_Idx--;
			int num = startItemGUI_Idx;
			if (num >= 0)
			{
				VatPhamMarketItem vatPhamMarketItem = ItemList[0];
				VatPhamMarketItem vatPhamMarketItem2 = ItemList[ItemList.Count - 1];
				vatPhamMarketItem2.vatPhamID = num;
				ItemList.RemoveAt(ItemList.Count - 1);
				ItemList.Insert(0, vatPhamMarketItem2);
				vatPhamMarketItem2.transform.localPosition = vatPhamMarketItem.transform.localPosition - itemOffset;
				vatPhamMarketItem2.Set(ListData[num]);
			}
		}
	}

	public void getListVatPham()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		ListData.Clear();
		itemPos = new Vector3(0f, 320f, 0f);
		itemOffset = new Vector3(0f, -230f, 0f);
		startItemGUI_Idx = 0;
		foreach (KeyValuePair<string, VatPhamTieuThuCfg> item2 in ConfigManager.instance.m_dicVatPhamTieuThu)
		{
			if (item2.Value.BanTrongShop)
			{
				ListData.Add(item2.Value);
			}
		}
		if (ListData.Count > 0)
		{
			for (int i = 0; i < ListData.Count; i++)
			{
				VatPhamMarketItem component = ((GameObject)Object.Instantiate(VatPhamPrefab)).GetComponent<VatPhamMarketItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				itemPos += itemOffset;
				component.Set(ListData[i]);
				UIEventListener.Get(component.btnMua.gameObject).onClick = onClick_btnMua;
				ItemList.Add(component);
			}
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}

	public void onClick_btnMua(GameObject go)
	{
		VatPhamMarketItem component = go.transform.parent.GetComponent<VatPhamMarketItem>();
		if (component.m_Data != null)
		{
			PopUpMua.CreateByVatPhamMarket(component.m_Data.Name, component.m_Data.TenHienThi, component.m_Data.GiaVang);
		}
	}
}

using System.Collections.Generic;
using UnityEngine;

public class ScreenKyNgo_GuiTietKiem : ScreenBase
{
	private const int maxItemCount = 12;

	public GameObject thangCapPerfab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<KyNgoGuiTietKiemItem> ItemList = new List<KyNgoGuiTietKiemItem>();

	private List<OtherCfg.LenCapNhanThuongCfg> ListData = new List<OtherCfg.LenCapNhanThuongCfg>();

	private int startItemGUI_Idx;

	private Vector3 itemPos;

	private Vector3 itemOffset;

	public GameObject ChuaThamGiaGrp;

	public GameObject DaThamGiaGrp;

	public UILabel ThoiGianConLai;

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
			KyNgoGuiTietKiemItem kyNgoGuiTietKiemItem = ItemList[ItemList.Count - 1];
			KyNgoGuiTietKiemItem kyNgoGuiTietKiemItem2 = ItemList[0];
			num = kyNgoGuiTietKiemItem.transform.localPosition.y;
			num2 = kyNgoGuiTietKiemItem2.transform.localPosition.y;
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
			KyNgoGuiTietKiemItem kyNgoGuiTietKiemItem = ItemList[0];
			KyNgoGuiTietKiemItem kyNgoGuiTietKiemItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(0);
			ItemList.Add(kyNgoGuiTietKiemItem);
			kyNgoGuiTietKiemItem.transform.localPosition = kyNgoGuiTietKiemItem2.transform.localPosition + itemOffset;
			kyNgoGuiTietKiemItem.Set(ListData[num]);
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
			KyNgoGuiTietKiemItem kyNgoGuiTietKiemItem = ItemList[0];
			KyNgoGuiTietKiemItem kyNgoGuiTietKiemItem2 = ItemList[ItemList.Count - 1];
			if (ItemList.Count == 12)
			{
				ItemList.RemoveAt(ItemList.Count - 1);
				ItemList.Insert(0, kyNgoGuiTietKiemItem2);
				kyNgoGuiTietKiemItem2.transform.localPosition = kyNgoGuiTietKiemItem.transform.localPosition - itemOffset;
				kyNgoGuiTietKiemItem2.Set(ListData[num]);
			}
			else if (ItemList.Count < 12)
			{
				KyNgoGuiTietKiemItem component = ((GameObject)Object.Instantiate(thangCapPerfab)).GetComponent<KyNgoGuiTietKiemItem>();
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
		if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains("GuiTK;"))
		{
			ChuaThamGiaGrp.SetActive(true);
			DaThamGiaGrp.SetActive(false);
		}
		else
		{
			ChuaThamGiaGrp.SetActive(false);
			DaThamGiaGrp.SetActive(true);
		}
		ThoiGianConLai.text = Localization.instance.Get("HanCuoiDangKy") + GameManager.instance.m_GameClient.UserInfo.ServerInfo.GuiTietKiemConfig.ThoiGianKetThuc.ToString();
		foreach (Transform item2 in ItemRoot.transform)
		{
			Transform transform2 = item2;
			Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		ListData.Clear();
		startItemGUI_Idx = 0;
		int num = 0;
		itemPos = new Vector3(0f, 180f, 0f);
		itemOffset = new Vector3(0f, -160f, 0f);
		if (ConfigManager.instance.OtherConfig.GuiTietKiem != null && ConfigManager.instance.OtherConfig.GuiTietKiem.Count > 0)
		{
			for (int i = 0; i < ConfigManager.instance.OtherConfig.GuiTietKiem.Count; i++)
			{
				OtherCfg.LenCapNhanThuongCfg item = ConfigManager.instance.OtherConfig.GuiTietKiem[i];
				string value = string.Format("GuiTK{0};", i);
				if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(value))
				{
					ListData.Add(item);
				}
			}
		}
		if (ListData == null || ListData.Count <= 0)
		{
			return;
		}
		for (int j = 0; j < ListData.Count; j++)
		{
			KyNgoGuiTietKiemItem component = ((GameObject)Object.Instantiate(thangCapPerfab)).GetComponent<KyNgoGuiTietKiemItem>();
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
		KyNgoGuiTietKiemItem component = go.transform.parent.GetComponent<KyNgoGuiTietKiemItem>();
		int id = -1;
		if (!(component != null))
		{
			return;
		}
		for (int i = 0; i < ConfigManager.instance.OtherConfig.GuiTietKiem.Count; i++)
		{
			if (ConfigManager.instance.OtherConfig.GuiTietKiem[i].CapYeuCau == component.m_Data.CapYeuCau)
			{
				id = i;
			}
		}
		GameManager.instance.m_GameClient.RequestGetGuiTietKiem(id);
	}

	public void OnThamGiaGuiTKBtn()
	{
		GameManager.instance.m_GameClient.RequestThamGiaGuiTietKiem();
	}
}

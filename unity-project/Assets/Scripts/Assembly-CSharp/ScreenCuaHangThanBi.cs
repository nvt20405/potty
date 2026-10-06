using System.Collections.Generic;
using UnityEngine;

public class ScreenCuaHangThanBi : ScreenBase
{
	public enum CuaHangThanBiTab
	{
		TabVatPham = 0,
		TabTrangBi = 1,
		TabVoCong = 2,
		TabTanHon = 3
	}

	private const int maxItemCount = 12;

	public GameObject CuaHangThanBiItemPrefab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<CuaHangThanBiItem> ItemList = new List<CuaHangThanBiItem>();

	private Vector3 itemPos;

	private Vector3 itemOffset;

	public UILabel lbDiemThanBi;

	private CuaHangThanBiTab currentTab = CuaHangThanBiTab.TabTanHon;

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
		updateView();
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
		if (PopUpMua.instance != null)
		{
			PopUpMua.DestroyPopup();
		}
	}

	public void displayInfo()
	{
		switch (currentTab)
		{
		case CuaHangThanBiTab.TabVatPham:
			getListVatPham(PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU);
			break;
		case CuaHangThanBiTab.TabTrangBi:
			getListVatPham(PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI);
			break;
		case CuaHangThanBiTab.TabVoCong:
			getListVatPham(PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG);
			break;
		case CuaHangThanBiTab.TabTanHon:
			getListVatPham(PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT);
			break;
		}
	}

	public void updateView()
	{
		lbDiemThanBi.text = Localization.instance.Get("DiemHienTaiLabel") + ": " + GameManager.instance.m_GameClient.UserInfo.Gamer.DiemCHThanBi;
	}

	private void Update()
	{
	}

	public void getListVatPham(PhanThuongResponse.LoaiPhanThuong loaiPT)
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		itemPos = new Vector3(0f, 320f, 0f);
		itemOffset = new Vector3(0f, -230f, 0f);
		if (ConfigManager.instance.m_CuaHangThanBiList != null && ConfigManager.instance.m_CuaHangThanBiList.Count > 0)
		{
			if (loaiPT != PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU)
			{
				for (int i = 0; i < ConfigManager.instance.m_CuaHangThanBiList.Count; i++)
				{
					if (ConfigManager.instance.m_CuaHangThanBiList[i].BanTrongShop && ConfigManager.instance.m_CuaHangThanBiList[i].Loai == loaiPT)
					{
						CuaHangThanBiItem component = ((GameObject)Object.Instantiate(CuaHangThanBiItemPrefab)).GetComponent<CuaHangThanBiItem>();
						component.transform.parent = ItemRoot.transform;
						component.transform.localScale = new Vector3(1f, 1f, 1f);
						component.transform.localPosition = itemPos;
						itemPos += itemOffset;
						component.Set(ConfigManager.instance.m_CuaHangThanBiList[i], i);
						UIEventListener.Get(component.btnMua.gameObject).onClick = onClick_btnMua;
						ItemList.Add(component);
					}
				}
			}
			else
			{
				for (int j = 0; j < ConfigManager.instance.m_CuaHangThanBiList.Count; j++)
				{
					if (ConfigManager.instance.m_CuaHangThanBiList[j].BanTrongShop && ConfigManager.instance.m_CuaHangThanBiList[j].Loai != PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI && ConfigManager.instance.m_CuaHangThanBiList[j].Loai != PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG && ConfigManager.instance.m_CuaHangThanBiList[j].Loai != PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT)
					{
						CuaHangThanBiItem component2 = ((GameObject)Object.Instantiate(CuaHangThanBiItemPrefab)).GetComponent<CuaHangThanBiItem>();
						component2.transform.parent = ItemRoot.transform;
						component2.transform.localScale = new Vector3(1f, 1f, 1f);
						component2.transform.localPosition = itemPos;
						itemPos += itemOffset;
						component2.Set(ConfigManager.instance.m_CuaHangThanBiList[j], j);
						UIEventListener.Get(component2.btnMua.gameObject).onClick = onClick_btnMua;
						ItemList.Add(component2);
					}
				}
			}
		}
		UIDraggablePanel component3 = ItemRoot.GetComponent<UIDraggablePanel>();
		component3.ResetPosition();
	}

	public void onClick_btnMua(GameObject go)
	{
		CuaHangThanBiItem component = go.transform.parent.GetComponent<CuaHangThanBiItem>();
		if (component.m_Data != null)
		{
			if (component.m_Data.DiemCan > GameManager.instance.m_GameClient.UserInfo.Gamer.DiemCHThanBi)
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoChuaDuDiemDoiCHThanBi"));
			}
			else
			{
				PopUpMua.CreateByCuaHangThanBi(component.m_Data.CodeName, component.m_Data.TenHienThi, component.m_Data.DiemCan, component.Index);
			}
		}
	}

	private void onClick_VatPhamBtn(bool isActive)
	{
		if (isActive && currentTab != CuaHangThanBiTab.TabVatPham)
		{
			currentTab = CuaHangThanBiTab.TabVatPham;
			getListVatPham(PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU);
		}
	}

	private void onClick_TrangBiBtn(bool isActive)
	{
		if (isActive && currentTab != CuaHangThanBiTab.TabTrangBi)
		{
			currentTab = CuaHangThanBiTab.TabTrangBi;
			getListVatPham(PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI);
		}
	}

	private void onClick_VoCongBtn(bool isActive)
	{
		if (isActive && currentTab != CuaHangThanBiTab.TabVoCong)
		{
			currentTab = CuaHangThanBiTab.TabVoCong;
			getListVatPham(PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG);
		}
	}

	private void onClick_TanHonBtn(bool isActive)
	{
		if (isActive && currentTab != CuaHangThanBiTab.TabTanHon)
		{
			currentTab = CuaHangThanBiTab.TabTanHon;
			getListVatPham(PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT);
		}
	}
}

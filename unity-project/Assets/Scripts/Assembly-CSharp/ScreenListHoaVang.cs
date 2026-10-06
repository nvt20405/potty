using System.Collections.Generic;
using UnityEngine;

public class ScreenListHoaVang : ScreenBase
{
	public UILabel lbCurDiem;

	public GameObject HoaVangItemPrefab;

	public GameObject ItemRoot;

	private List<HoaVangItem> ItemList = new List<HoaVangItem>();

	private List<int> listTrangBiSelected = new List<int>();

	public HoaVangItem currentItemSelected;

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

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		lbCurDiem.text = Localization.instance.Get("DiemHienTaiLabel") + ": " + GameManager.instance.m_GameClient.UserInfo.Gamer.DiemHoaVang;
		GameManager.instance.m_GameClient.RequestGetHoaVangInfo();
	}

	public void displayInfo(GetHoaVangInfoResponse response)
	{
		ClearGUIItem();
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 320f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -210f, 0f);
		if (response == null || response.ListHoaVangData == null || response.ListHoaVangData.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < response.ListHoaVangData.Count; i++)
		{
			PhanThuongResponse.PhanThuong phanThuong = response.ListHoaVangData[i];
			if (phanThuong != null)
			{
				HoaVangItem component = ((GameObject)Object.Instantiate(HoaVangItemPrefab)).GetComponent<HoaVangItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = vector;
				vector += vector2;
				component.setDataItem(phanThuong, i);
				UIEventListener.Get(component.gameObject).onClick = onClick_HoaVangItem;
				ItemList.Add(component);
			}
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}

	public void ClearGUIItem()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
	}

	public void onClick_HoaVangItem(GameObject go)
	{
		HoaVangItem component = go.GetComponent<HoaVangItem>();
		if (component != null && component.curSoLuong > 0 && component.m_Data != null && component.curSoLuong >= component.m_Data.Count)
		{
			currentItemSelected = component;
			if (component.m_Data.Loai == PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU)
			{
				PopupYesNo.Create(Localization.instance.Get("HoaVangConfirmLabel"), Localization.instance.Get("DongYLabelBtn"), Localization.instance.Get("TuChoiLabelBtn"), OnConfirmHoaVangVatPham, null);
			}
			else if (component.m_Data.Loai == PhanThuongResponse.LoaiPhanThuong.TRANG_BI)
			{
				PopupHoaVangTrangBi.Create(OnSelectedHoaVangTrangBi, component.m_Data.Name, component.m_Data.TyLe);
			}
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("KhongDuDieuKienHoaVang"));
		}
	}

	public bool OnSelectedHoaVangTrangBi(List<int> listID)
	{
		if (listID.Count > 0)
		{
			listTrangBiSelected.Clear();
			for (int i = 0; i < listID.Count; i++)
			{
				listTrangBiSelected.Add(listID[i]);
			}
			PopupYesNo.Create(Localization.instance.Get("HoaVangConfirmLabel"), Localization.instance.Get("DongYLabelBtn"), Localization.instance.Get("TuChoiLabelBtn"), OnConfirmHoaVangTrangBi, null);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ChuaChonDoHoaVangMess"));
		}
		return true;
	}

	private void OnConfirmHoaVangTrangBi()
	{
		HoaVangRequest hoaVangRequest = new HoaVangRequest();
		hoaVangRequest.loaiHoaVang = HoaVangRequest.LoaiHoaVang.TRANG_BI;
		hoaVangRequest.listTrangBiHoaVang = listTrangBiSelected;
		hoaVangRequest.index = currentItemSelected.curIndex;
		GameManager.instance.m_GameClient.RequestHoaVang(hoaVangRequest);
	}

	private void OnConfirmHoaVangVatPham()
	{
		HoaVangRequest hoaVangRequest = new HoaVangRequest();
		hoaVangRequest.loaiHoaVang = HoaVangRequest.LoaiHoaVang.VAT_PHAM_TIEU_THU;
		hoaVangRequest.index = currentItemSelected.curIndex;
		GameManager.instance.m_GameClient.RequestHoaVang(hoaVangRequest);
	}

	public void btnBack_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHoaVangMain);
	}

	public void updateView(HoaVangResponse response)
	{
		if (response != null)
		{
			displayInfo(GameManager.instance.m_GameClient.HoaVangInfoResponse);
			lbCurDiem.text = Localization.instance.Get("DiemHienTaiLabel") + ": " + GameManager.instance.m_GameClient.UserInfo.Gamer.DiemHoaVang;
			PopupYesNo.Create(string.Format(Localization.instance.Get("ThongBaoHoaVangSuccess"), response.DiemNhanDuoc), Localization.instance.Get("DenQuayThuongLabelBtn"), Localization.instance.Get("ClosePopupBtn"), OnOpenScreenQuayThuong, null);
		}
	}

	private void OnOpenScreenQuayThuong()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenQuayDiemHoaVang);
	}
}

using System.Collections.Generic;
using UnityEngine;

public class AutoThachDauFull : MonoBehaviour
{
	private const int maxItemCount = 20;

	public GameObject ItemRoot;

	public GameObject ThachDauAutoItem;

	public UILabel lbMaxLuotThachDau;

	public UIButton btnThachDau;

	public UIButton btnThachDauAll;

	private List<ThachDauTuDongItem> listItem = new List<ThachDauTuDongItem>();

	private Vector3 itemPos;

	private Vector3 itemOffset = new Vector3(0f, -85f, 0f);

	private List<int> listUserSelectedID = new List<int>();

	private List<SimpleUserData> listAllData = new List<SimpleUserData>();

	private List<PhanThuongResponse.PhanThuong> listAllPhanThuong = new List<PhanThuongResponse.PhanThuong>();

	private int luotThachDau;

	private int currentIndexThachDau = -1;

	public void getListThachDau()
	{
		luotThachDau = GameManager.instance.m_GameClient.UserInfo.Gamer.ThangThachDauList.Count;
		if (luotThachDau >= ConfigManager.GetMaxLuotThachDau())
		{
			ThachDauAutoItem.gameObject.SetActive(false);
			btnThachDau.gameObject.SetActive(false);
			btnThachDauAll.gameObject.SetActive(false);
			lbMaxLuotThachDau.gameObject.SetActive(true);
			return;
		}
		ThachDauAutoItem.gameObject.SetActive(true);
		btnThachDau.gameObject.SetActive(true);
		btnThachDauAll.gameObject.SetActive(true);
		lbMaxLuotThachDau.gameObject.SetActive(false);
		listAllData.Clear();
		if (GameManager.instance.m_GameClient.UserInfo.BanBeList != null && GameManager.instance.m_GameClient.UserInfo.BanBeList.Count > 0)
		{
			for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.BanBeList.Count; i++)
			{
				if (!GameManager.instance.m_GameClient.UserInfo.Gamer.ThangThachDauList.Contains(GameManager.instance.m_GameClient.UserInfo.BanBeList[i].GID) && GameManager.instance.m_GameClient.UserInfo.BanBeList[i].Level >= 4)
				{
					SimpleUserData simpleUserData = new SimpleUserData();
					simpleUserData.GID = GameManager.instance.m_GameClient.UserInfo.BanBeList[i].GID;
					simpleUserData.DisplayName = GameManager.instance.m_GameClient.UserInfo.BanBeList[i].DisplayName;
					simpleUserData.Level = GameManager.instance.m_GameClient.UserInfo.BanBeList[i].Level;
					simpleUserData.Vip = GameManager.instance.m_GameClient.UserInfo.BanBeList[i].Vip;
					listAllData.Add(simpleUserData);
				}
			}
		}
		if (GameManager.instance.m_GameClient.UserInfo.CuuThuList != null && GameManager.instance.m_GameClient.UserInfo.CuuThuList.Count > 0)
		{
			for (int j = 0; j < GameManager.instance.m_GameClient.UserInfo.CuuThuList.Count; j++)
			{
				if (!GameManager.instance.m_GameClient.UserInfo.Gamer.ThangThachDauList.Contains(GameManager.instance.m_GameClient.UserInfo.CuuThuList[j].GID) && GameManager.instance.m_GameClient.UserInfo.CuuThuList[j].Level >= 4)
				{
					SimpleUserData simpleUserData2 = new SimpleUserData();
					simpleUserData2.GID = GameManager.instance.m_GameClient.UserInfo.CuuThuList[j].GID;
					simpleUserData2.DisplayName = GameManager.instance.m_GameClient.UserInfo.CuuThuList[j].DisplayName;
					simpleUserData2.Level = GameManager.instance.m_GameClient.UserInfo.CuuThuList[j].Level;
					simpleUserData2.Vip = GameManager.instance.m_GameClient.UserInfo.CuuThuList[j].Vip;
					listAllData.Add(simpleUserData2);
				}
			}
		}
		if (listAllData.Count < ConfigManager.GetMaxLuotThachDau())
		{
			List<int> list = new List<int>();
			for (int k = 0; k < listAllData.Count; k++)
			{
				list.Add(listAllData[k].GID);
			}
			GetListOtherUserRequest getListOtherUserRequest = new GetListOtherUserRequest();
			getListOtherUserRequest.GID = GameManager.instance.m_GameClient.UserInfo.Gamer.ID;
			getListOtherUserRequest.Level = GameManager.instance.m_GameClient.UserInfo.Gamer.Level;
			getListOtherUserRequest.Count = ConfigManager.GetMaxLuotThachDau() - listAllData.Count;
			getListOtherUserRequest.listIDIgnore = list;
			GameManager.instance.m_GameClient.RequestGetListOtherUser(getListOtherUserRequest);
		}
		else
		{
			initListItem();
		}
	}

	public void addOtherUser(List<SimpleUserData> list)
	{
		if (list != null && list.Count > 0)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] != null)
				{
					listAllData.Add(list[i]);
				}
			}
		}
		initListItem();
	}

	private void initListItem()
	{
		ClearList();
		itemPos = new Vector3(0f, 320f, 0f);
		listAllData.Sort((SimpleUserData x, SimpleUserData y) => CompareUser(y.Level, y.Vip, x.Level, x.Vip));
		for (int num = 0; num < listAllData.Count; num++)
		{
			SimpleUserData simpleUserData = listAllData[num];
			ThachDauTuDongItem component = ((GameObject)Object.Instantiate(ThachDauAutoItem)).GetComponent<ThachDauTuDongItem>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = itemPos;
			itemPos += itemOffset;
			component.init(simpleUserData.GID, simpleUserData.DisplayName, simpleUserData.Vip, simpleUserData.Level);
			component.setStatus(ThachDauTuDongItem.USERSTATUS.NONE);
			UIEventListener.Get(component.gameObject).onClick = onClick_ThachDauItem;
			UIEventListener.Get(component.checkBox.gameObject).onClick = onClick_CheckBox;
			listItem.Add(component);
		}
	}

	public void onClick_CheckBox(GameObject go)
	{
		if (luotThachDau + listUserSelectedID.Count > ConfigManager.GetMaxLuotThachDau())
		{
			MessagePopup.Create(Localization.instance.Get("DaDuSoLuongThachDauTrongNgayMess"));
			return;
		}
		ThachDauTuDongItem component = go.transform.parent.GetComponent<ThachDauTuDongItem>();
		if (listUserSelectedID.Count >= ConfigManager.GetMaxLuotThachDau())
		{
			component.checkBox.isChecked = false;
		}
		if (component.checkBox.isChecked && !listUserSelectedID.Contains(component.GID))
		{
			listUserSelectedID.Add(component.GID);
		}
		else if (!component.checkBox.isChecked && listUserSelectedID.Contains(component.GID))
		{
			listUserSelectedID.Remove(component.GID);
		}
	}

	public void onClick_ThachDauItem(GameObject go)
	{
		if (luotThachDau + listUserSelectedID.Count > ConfigManager.GetMaxLuotThachDau())
		{
			MessagePopup.Create(Localization.instance.Get("DaDuSoLuongThachDauTrongNgayMess"));
			return;
		}
		ThachDauTuDongItem component = go.GetComponent<ThachDauTuDongItem>();
		if (listUserSelectedID.Count >= ConfigManager.GetMaxLuotThachDau())
		{
			component.checkBox.isChecked = false;
		}
		component.checkBox.isChecked = !component.checkBox.isChecked;
		if (component.checkBox.isChecked && !listUserSelectedID.Contains(component.GID))
		{
			listUserSelectedID.Add(component.GID);
		}
		else if (!component.checkBox.isChecked && listUserSelectedID.Contains(component.GID))
		{
			listUserSelectedID.Remove(component.GID);
		}
	}

	private void ClearList()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		listItem.Clear();
		listUserSelectedID.Clear();
		listAllPhanThuong.Clear();
	}

	public void ThachDauBtn_OnClick(GameObject go)
	{
		if (!GUIManager.instance.isAutoThachDau)
		{
			int count = GameManager.instance.m_GameClient.UserInfo.Gamer.ThangThachDauList.Count;
			if (count >= ConfigManager.GetMaxLuotThachDau())
			{
				MessagePopup.Create(Localization.instance.Get("HetLuotThachDauAutoMess"));
			}
			else if (listUserSelectedID != null && listUserSelectedID.Count > 0)
			{
				currentIndexThachDau = 0;
				GUIManager.instance.isAutoThachDau = true;
				listAllPhanThuong.Clear();
				GameManager.instance.m_GameClient.RequestThachDau(listUserSelectedID[currentIndexThachDau], 0);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("ChuaChonDoiTuongThachDauMess"));
			}
		}
	}

	public void ThachDauAllBtn_OnClick(GameObject go)
	{
		if (GUIManager.instance.isAutoThachDau)
		{
			return;
		}
		int num = GameManager.instance.m_GameClient.UserInfo.Gamer.ThangThachDauList.Count;
		if (num >= ConfigManager.GetMaxLuotThachDau())
		{
			MessagePopup.Create(Localization.instance.Get("HetLuotThachDauAutoMess"));
			return;
		}
		if (listUserSelectedID != null)
		{
			listUserSelectedID.Clear();
		}
		else
		{
			listUserSelectedID = new List<int>();
		}
		for (int i = 0; i < listItem.Count; i++)
		{
			listItem[i].checkBox.isChecked = true;
			if (num >= ConfigManager.GetMaxLuotThachDau() || GameManager.instance.m_GameClient.UserInfo.Gamer.ThangThachDauList.Contains(listItem[i].GID))
			{
				break;
			}
			listUserSelectedID.Add(listItem[i].GID);
			num++;
		}
		if (listUserSelectedID != null && listUserSelectedID.Count > 0)
		{
			currentIndexThachDau = 0;
			GUIManager.instance.isAutoThachDau = true;
			listAllPhanThuong.Clear();
			GameManager.instance.m_GameClient.RequestThachDau(listUserSelectedID[currentIndexThachDau], 0);
		}
	}

	public void continueThachDau(PhanThuongResponse prevPTThachDau, bool isWon)
	{
		if (prevPTThachDau != null && prevPTThachDau.PhanThuongList != null && prevPTThachDau.PhanThuongList.Count > 0 && listAllPhanThuong != null)
		{
			for (int i = 0; i < prevPTThachDau.PhanThuongList.Count; i++)
			{
				listAllPhanThuong.Add(prevPTThachDau.PhanThuongList[i]);
				EGDebug.Log("add phan thuong :" + prevPTThachDau.PhanThuongList[i].Name);
			}
		}
		for (int j = 0; j < listItem.Count; j++)
		{
			if (listItem[j].GID == listUserSelectedID[currentIndexThachDau])
			{
				if (isWon)
				{
					listItem[j].setStatus(ThachDauTuDongItem.USERSTATUS.WIN);
				}
				else
				{
					listItem[j].setStatus(ThachDauTuDongItem.USERSTATUS.FAIL);
				}
			}
		}
		if (currentIndexThachDau >= listUserSelectedID.Count - 1)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = listAllPhanThuong;
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongDesc"), phanThuongResponse, onClose_popupPhanthuong);
			currentIndexThachDau = -1;
			GUIManager.instance.isAutoThachDau = false;
			listUserSelectedID.Clear();
			listAllPhanThuong.Clear();
		}
		else
		{
			currentIndexThachDau++;
			EGDebug.Log("CURRENT INDEX thach dau: " + currentIndexThachDau);
			if (listUserSelectedID.Count > currentIndexThachDau)
			{
				GameManager.instance.m_GameClient.RequestThachDau(listUserSelectedID[currentIndexThachDau], 0);
			}
		}
	}

	public void onClose_popupPhanthuong()
	{
		int count = GameManager.instance.m_GameClient.UserInfo.Gamer.ThangThachDauList.Count;
		int maxLuotThachDau = ConfigManager.GetMaxLuotThachDau();
		if (count < maxLuotThachDau)
		{
			PopupYesNo.Create(string.Format(Localization.instance.Get("ConfirmAutoThachDau"), count, maxLuotThachDau - count), Localization.instance.Get("Confirm"), Localization.instance.Get("Cancel"), OnContinueAuToThachDau, null);
		}
	}

	public void OnContinueAuToThachDau()
	{
		getListThachDau();
	}

	public int CompareUser(int level1, int vip1, int level2, int vip2)
	{
		if (level1 > level2)
		{
			return -1;
		}
		if (level1 < level2)
		{
			return 1;
		}
		if (vip1 > vip2)
		{
			return -1;
		}
		if (vip1 < vip2)
		{
			return 1;
		}
		return 0;
	}
}

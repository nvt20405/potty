using System.Collections.Generic;
using UnityEngine;

public class PopupSonMonDoiHinhPhongThu : MonoBehaviour
{
	public static PopupSonMonDoiHinhPhongThu instance;

	public List<PopupSonMonDoiHinhItem> ItemList;

	public List<int> ListPhongThu;

	public PopupSonMonDoiHinhItem SelectedItem;

	public Dictionary<string, int> TempDoiHinhBaoVe;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public static void Create()
	{
		GameManager.instance.m_GameClient.UserInfo.SonMon.ListDoiHinhBaoVe = new Dictionary<string, int>();
		int num = 0;
		int i;
		for (i = 0; i < 9; i++)
		{
			UserInfo.SonMonBuildingInfo sonMonBuildingInfo = GameManager.instance.m_GameClient.UserInfo.SonMon.ListCongTrinh.Find((UserInfo.SonMonBuildingInfo ct) => ct.Slot == i + 1);
			if (sonMonBuildingInfo.LoaiCongTrinh != UserInfo.SonMonBuildingInfo.SonMonBuildingType.NULL_CT && sonMonBuildingInfo.LoaiCongTrinh != UserInfo.SonMonBuildingInfo.SonMonBuildingType.CHINH_SANH && sonMonBuildingInfo.LoaiCongTrinh != UserInfo.SonMonBuildingInfo.SonMonBuildingType.TIEN_TRANG)
			{
				GameManager.instance.m_GameClient.UserInfo.SonMon.ListDoiHinhBaoVe.Add((i + 1).ToString(), num);
				num++;
			}
		}
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
		}
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupSonMonDoiHinhPhongThu"))).GetComponent<PopupSonMonDoiHinhPhongThu>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = Vector3.one;
		instance.Set(GameManager.instance.m_GameClient.UserInfo.SonMon.ListDoiHinhBaoVe);
	}

	public void Set(Dictionary<string, int> doihinhBaoVe)
	{
		for (int i = 0; i < ItemList.Count; i++)
		{
			ItemList[i].gameObject.SetActive(false);
		}
		UserInfo.SonMonInfo sonMon = GameManager.instance.m_GameClient.UserInfo.SonMon;
		TempDoiHinhBaoVe = new Dictionary<string, int>(doihinhBaoVe);
		int num = 0;
		int j;
		for (j = 0; j < 9; j++)
		{
			UserInfo.SonMonBuildingInfo sonMonBuildingInfo = sonMon.ListCongTrinh.Find((UserInfo.SonMonBuildingInfo ct) => ct.Slot == j + 1);
			if (sonMonBuildingInfo.LoaiCongTrinh != UserInfo.SonMonBuildingInfo.SonMonBuildingType.NULL_CT && sonMonBuildingInfo.LoaiCongTrinh != UserInfo.SonMonBuildingInfo.SonMonBuildingType.CHINH_SANH && sonMonBuildingInfo.LoaiCongTrinh != UserInfo.SonMonBuildingInfo.SonMonBuildingType.TIEN_TRANG)
			{
				ItemList[num].gameObject.SetActive(true);
				ItemList[num].Set(sonMonBuildingInfo, GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran[TempDoiHinhBaoVe[(j + 1).ToString()]]);
				num++;
			}
		}
	}

	public void OnBeginSwap(GameObject item)
	{
		for (int i = 0; i < ItemList.Count; i++)
		{
			ItemList[i].ShowSwap(true);
			ItemList[i].GetComponent<Collider>().enabled = false;
		}
		item.GetComponent<PopupSonMonDoiHinhItem>().ShowSwap(false);
		SelectedItem = item.GetComponent<PopupSonMonDoiHinhItem>();
	}

	public void OnEndSwap(GameObject item)
	{
		UserInfo.SonMonInfo sonMon = GameManager.instance.m_GameClient.UserInfo.SonMon;
		for (int i = 0; i < ItemList.Count; i++)
		{
			ItemList[i].ShowSwap(false);
			ItemList[i].GetComponent<Collider>().enabled = true;
		}
		if (!(item == null))
		{
			int value = TempDoiHinhBaoVe[SelectedItem.CongTrinh.Slot.ToString()];
			TempDoiHinhBaoVe[SelectedItem.CongTrinh.Slot.ToString()] = TempDoiHinhBaoVe[item.transform.parent.GetComponent<PopupSonMonDoiHinhItem>().CongTrinh.Slot.ToString()];
			TempDoiHinhBaoVe[item.transform.parent.GetComponent<PopupSonMonDoiHinhItem>().CongTrinh.Slot.ToString()] = value;
			Set(TempDoiHinhBaoVe);
		}
	}

	public void OnUpdate()
	{
		GameManager.instance.m_GameClient.RequestDoiHinhSonMon(TempDoiHinhBaoVe);
	}

	public void Close()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}
}

using System.Collections.Generic;
using UnityEngine;

public class PopupSonMonXayDung : MonoBehaviour
{
	public static PopupSonMonXayDung instance;

	public PopupSonMonXayDungItem BaseItem;

	public float ItemDeltaPos;

	public int SonMonBuildingID;

	public UserInfo.SonMonBuildingInfo.SonMonBuildingType SelectedCT;

	public static void Create(List<string> ignoreList, int id)
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
		}
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupSonMonXayDung"))).GetComponent<PopupSonMonXayDung>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = Vector3.one;
		instance.BaseItem.gameObject.SetActive(false);
		instance.Set(ignoreList);
		instance.SonMonBuildingID = id;
	}

	public void Set(List<string> ignoreList)
	{
		foreach (Transform item in BaseItem.transform.parent)
		{
			Transform transform2 = item;
			if (transform2 != BaseItem.transform)
			{
				Object.Destroy(transform2.gameObject);
			}
		}
		int num = 0;
		for (int i = 0; i < 6; i++)
		{
			UserInfo.SonMonBuildingInfo.SonMonBuildingType sonMonBuildingType = (UserInfo.SonMonBuildingInfo.SonMonBuildingType)i;
			if (!ignoreList.Contains(sonMonBuildingType.ToString()))
			{
				BaseItem.gameObject.SetActive(true);
				PopupSonMonXayDungItem component = ((GameObject)Object.Instantiate(BaseItem.gameObject)).GetComponent<PopupSonMonXayDungItem>();
				component.transform.parent = BaseItem.transform.parent;
				component.transform.localScale = Vector3.one;
				component.transform.localPosition = BaseItem.transform.localPosition + num * Vector3.down * ItemDeltaPos;
				component.Set((UserInfo.SonMonBuildingInfo.SonMonBuildingType)i);
				num++;
				BaseItem.gameObject.SetActive(false);
			}
		}
	}

	public static void CreateChinhSanh(int id)
	{
		List<string> list = new List<string>();
		for (int i = 0; i < 6; i++)
		{
			if (i != 0)
			{
				UserInfo.SonMonBuildingInfo.SonMonBuildingType sonMonBuildingType = (UserInfo.SonMonBuildingInfo.SonMonBuildingType)i;
				list.Add(sonMonBuildingType.ToString());
			}
		}
		Create(list, id);
	}

	public static void CreateUtility(int id)
	{
		List<string> list = new List<string>();
		list.Add(UserInfo.SonMonBuildingInfo.SonMonBuildingType.CHINH_SANH.ToString());
		foreach (UserInfo.SonMonBuildingInfo item in GameManager.instance.m_GameClient.UserInfo.SonMon.ListCongTrinh)
		{
			if (item.LoaiCongTrinh != UserInfo.SonMonBuildingInfo.SonMonBuildingType.CHINH_SANH)
			{
				list.Add(item.LoaiCongTrinh.ToString());
			}
		}
		int i;
		for (i = 0; i < 6; i++)
		{
			if (i != 0 && i != 5 && list.FindAll((string ct) =>
			{
				UserInfo.SonMonBuildingInfo.SonMonBuildingType sonMonBuildingType = (UserInfo.SonMonBuildingInfo.SonMonBuildingType)i;
				return ct == sonMonBuildingType.ToString();
			}).Count < 2)
			{
				list.RemoveAll((string ct) =>
				{
					UserInfo.SonMonBuildingInfo.SonMonBuildingType sonMonBuildingType = (UserInfo.SonMonBuildingInfo.SonMonBuildingType)i;
					return ct == sonMonBuildingType.ToString();
				});
			}
		}
		Create(list, id);
	}

	public void Close()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public void XayDungCongTrinh()
	{
		GameManager.instance.m_GameClient.RequestXayDungSonMon(SonMonBuildingID, SelectedCT);
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}
}

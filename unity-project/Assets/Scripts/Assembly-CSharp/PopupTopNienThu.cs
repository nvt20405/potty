using System.Collections.Generic;
using UnityEngine;

public class PopupTopNienThu : MonoBehaviour
{
	public LanhDiaTopItem BaseItem;

	public Transform ItemRoot;

	public float ItemSize;

	public static PopupTopNienThu instance;

	public static PopupTopNienThu Create(List<string> LienMinhList, List<int> ScoreList)
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
		}
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupTopNienThu"))).GetComponent<PopupTopNienThu>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = Vector3.one;
		instance.Set(LienMinhList, ScoreList);
		return instance;
	}

	public void Set(List<string> LienMinhList, List<int> ScoreList)
	{
		BaseItem.gameObject.SetActive(true);
		int num = 0;
		foreach (Transform item in ItemRoot)
		{
			Transform transform2 = item;
			if (transform2.gameObject != BaseItem.gameObject)
			{
				Object.Destroy(transform2.gameObject);
			}
		}
		foreach (string LienMinh in LienMinhList)
		{
			LanhDiaTopItem lanhDiaTopItem = Object.Instantiate(BaseItem) as LanhDiaTopItem;
			lanhDiaTopItem.transform.parent = ItemRoot;
			lanhDiaTopItem.transform.localScale = Vector3.one;
			lanhDiaTopItem.transform.localPosition = num * Vector3.down * ItemSize + BaseItem.transform.localPosition;
			if (num + 1 > GameManager.instance.m_GameClient.UserInfo.ServerInfo.PhanThuongTopNienThu.Count)
			{
				break;
			}
			lanhDiaTopItem.Set(LienMinh, num + 1, GameManager.instance.m_GameClient.UserInfo.ServerInfo.PhanThuongTopNienThu[num], ScoreList[num]);
			num++;
		}
		BaseItem.gameObject.SetActive(false);
	}

	public void Close()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
		}
	}
}

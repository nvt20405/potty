using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopupTopLanhDiaBang : MonoBehaviour
{
	public LanhDiaTopItem BaseItem;

	public Transform ItemRoot;

	public float ItemSize;

	public static PopupTopLanhDiaBang instance;

	public static PopupTopLanhDiaBang Create()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
		}
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupTopLanhDiaBang"))).GetComponent<PopupTopLanhDiaBang>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = Vector3.one;
		instance.Set();
		return instance;
	}

	public void Set()
	{
		ScreenLanhDiaMap screenLanhDiaMap = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenLanhDiaMap) as ScreenLanhDiaMap;
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
		IOrderedEnumerable<KeyValuePair<Vector2, int>> orderedEnumerable = screenLanhDiaMap.LienMinhLanhDia.OrderByDescending((KeyValuePair<Vector2, int> pair) => pair.Value);
		foreach (KeyValuePair<Vector2, int> item2 in orderedEnumerable)
		{
			LanhDiaTopItem lanhDiaTopItem = Object.Instantiate(BaseItem) as LanhDiaTopItem;
			lanhDiaTopItem.transform.parent = ItemRoot;
			lanhDiaTopItem.transform.localScale = Vector3.one;
			lanhDiaTopItem.transform.localPosition = num * Vector3.down * ItemSize + BaseItem.transform.localPosition;
			if (num + 1 > GameManager.instance.m_GameClient.UserInfo.ServerInfo.PhanThuongTopLanhDia.Count)
			{
				break;
			}
			lanhDiaTopItem.Set(GameManager.instance.m_GameClient.UserInfo.LanhDiaData.LienMinhName[item2.Key.y + "," + item2.Key.x], num + 1, GameManager.instance.m_GameClient.UserInfo.ServerInfo.PhanThuongTopLanhDia[num], item2.Value);
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

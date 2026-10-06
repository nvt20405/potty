using UnityEngine;

public class PopupSonMonEvent : MonoBehaviour
{
	public static PopupSonMonEvent instance;

	public static int TabState;

	public GameObject AllGroup;

	public static GetTopSonMonResponse data;

	public PopupSonMonEventItem BaseItem;

	public float ItemDeltaPos;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public static void Create(GetTopSonMonResponse response)
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
		}
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupSonMonEvent"))).GetComponent<PopupSonMonEvent>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = Vector3.one;
		instance.BaseItem.gameObject.SetActive(false);
		instance.Set(response);
	}

	public void Set(GetTopSonMonResponse response)
	{
		BaseItem.gameObject.SetActive(true);
		data = response;
		foreach (Transform item in BaseItem.transform.parent)
		{
			Transform transform2 = item;
			if (transform2 != BaseItem.transform)
			{
				Object.Destroy(transform2.gameObject);
			}
		}
		int num = 0;
		foreach (TopSonMonInfo topPlayer in response.TopPlayers)
		{
			PopupSonMonEventItem component = ((GameObject)Object.Instantiate(BaseItem.gameObject)).GetComponent<PopupSonMonEventItem>();
			component.transform.parent = BaseItem.transform.parent;
			component.transform.localScale = Vector3.one;
			component.transform.localPosition = BaseItem.transform.localPosition + num * Vector3.down * ItemDeltaPos;
			component.Set(num + 1, topPlayer.Name, topPlayer.Avatar, topPlayer.Score, GameManager.instance.m_GameClient.UserInfo.ServerInfo.EventSonMon.PhanThuongCaNhan[num]);
			num++;
		}
		BaseItem.gameObject.SetActive(false);
	}

	public static void Resume()
	{
		Create(data);
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

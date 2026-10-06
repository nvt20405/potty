using UnityEngine;

public class PopupLoginMessage : MonoBehaviour
{
	public GameObject ItemRoot;

	public GameObject ItemPrefab;

	private UserInfo.TrangBiData m_TrangBiData;

	public static PopupLoginMessage instance;

	private void Start()
	{
	}

	public void OnCloseClick()
	{
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public void Update()
	{
	}

	public static void Create(UserInfo.ServerData data)
	{
		DestroyPopup();
		if (!(TutorialPopup.instance != null))
		{
			instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupLoginMessage"))).GetComponent<PopupLoginMessage>();
			PopupManager.instance.Add(instance.gameObject);
			instance.transform.localScale = new Vector3(1f, 1f, 1f);
			instance.Set(data);
		}
	}

	public void Set(UserInfo.ServerData data)
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		int num = 335;
		foreach (UserInfo.ServerData.LoginMessage loginMg in data.LoginMgs)
		{
			PopupLoginMessageItem component = ((GameObject)Object.Instantiate(ItemPrefab)).GetComponent<PopupLoginMessageItem>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = Vector3.one;
			component.transform.localPosition = new Vector3(0f, num, 0f);
			int num2 = component.Set(loginMg);
			num -= num2 + 8;
		}
	}
}

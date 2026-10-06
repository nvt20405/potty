using System.Collections.Generic;
using UnityEngine;

public class PopUpUuDaiVIP : MonoBehaviour
{
	public UILabel infoVIP;

	public GameObject VIPPrefab;

	public GameObject ItemRoot;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<VIPItem> ItemList = new List<VIPItem>();

	public static PopUpUuDaiVIP instance;

	private void Start()
	{
		dragPanel = ItemRoot.GetComponent<UIDraggablePanel>();
		panel = ItemRoot.GetComponent<UIPanel>();
	}

	public static void Create()
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupUuDaiVIP"))).GetComponent<PopUpUuDaiVIP>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.getListVIP();
	}

	public void getListVIP()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 310f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -80f, 0f);
		for (int i = 0; i < 15; i++)
		{
			VIPItem component = ((GameObject)Object.Instantiate(VIPPrefab)).GetComponent<VIPItem>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = vector;
			vector += vector2;
			component.displayLevel(i + 1);
			UIEventListener.Get(component.gameObject).onClick = onClick_VIPItem;
			component.isSelected(false);
			ItemList.Add(component);
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
		ItemList[0].isSelected(true);
		infoVIP.text = Localization.instance.Get("UuDaiVIP1");
	}

	public void onClick_VIPItem(GameObject go)
	{
		for (int i = 0; i < ItemList.Count; i++)
		{
			ItemList[i].isSelected(false);
		}
		VIPItem component = go.gameObject.GetComponent<VIPItem>();
		component.isSelected(true);
		string key = "UuDaiVIP" + component.currentVIP;
		infoVIP.text = Localization.instance.Get(key);
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public void OnClick_CloseBtn()
	{
		DestroyPopup();
	}

	public void OnClick_NapNgayBtn()
	{
		SohaSDKManager.instance.Payment();
	}

	public void OnClick_QuayLaiBtn()
	{
		DestroyPopup();
		PopUpNapTien.Create();
	}
}

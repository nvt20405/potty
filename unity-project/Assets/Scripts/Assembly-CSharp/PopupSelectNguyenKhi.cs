using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupSelectNguyenKhi : MonoBehaviour
{
	private const int maxItemCount = 12;

	public GameObject ItemRoot;

	public GameObject NguyenKhiPrefab;

	public static PopupSelectNguyenKhi instance;

	public Func<int, bool> OnFinish;

	private List<UserInfo.NguyenKhiData> nguyenKhiList = new List<UserInfo.NguyenKhiData>();

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<PopupSelectNguyenKhiItem> ItemList = new List<PopupSelectNguyenKhiItem>();

	private int startItemGUI_Idx;

	public UILabel lbTitle;

	private int mNguyenKhiSelectedID;

	private void Start()
	{
		dragPanel = ItemRoot.GetComponent<UIDraggablePanel>();
		panel = ItemRoot.GetComponent<UIPanel>();
	}

	private void Update()
	{
		if (ItemList.Count > 0)
		{
			Vector4 clipRange = panel.clipRange;
			PopupSelectNguyenKhiItem popupSelectNguyenKhiItem = ItemList[ItemList.Count - 1];
			float y = popupSelectNguyenKhiItem.transform.localPosition.y;
			PopupSelectNguyenKhiItem popupSelectNguyenKhiItem2 = ItemList[0];
			float y2 = popupSelectNguyenKhiItem2.transform.localPosition.y;
			if (y - clipRange.y > -600f)
			{
				SwapDragListDown();
			}
			else if (y2 - clipRange.y < 600f)
			{
				SwapDragListUp();
			}
		}
	}

	public void SwapDragListDown()
	{
		int num = startItemGUI_Idx + 12;
		if (num < nguyenKhiList.Count)
		{
			Vector3 vector = default(Vector3);
			vector = new Vector3(0f, 340f, 0f);
			Vector3 vector2 = default(Vector3);
			vector2 = new Vector3(0f, -135f, 0f);
			PopupSelectNguyenKhiItem popupSelectNguyenKhiItem = ItemList[0];
			PopupSelectNguyenKhiItem popupSelectNguyenKhiItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(0);
			ItemList.Add(popupSelectNguyenKhiItem);
			popupSelectNguyenKhiItem.transform.localPosition = popupSelectNguyenKhiItem2.transform.localPosition + vector2;
			popupSelectNguyenKhiItem.Set(nguyenKhiList[num]);
			Utils.SetLayer(popupSelectNguyenKhiItem.transform, "GUIPopUp", true);
			UICheckbox componentInChildren = popupSelectNguyenKhiItem.GetComponentInChildren<UICheckbox>();
			if (nguyenKhiList[num].ID == mNguyenKhiSelectedID && componentInChildren != null)
			{
				componentInChildren.isChecked = true;
			}
			else
			{
				componentInChildren.isChecked = false;
			}
			startItemGUI_Idx++;
		}
	}

	public void SwapDragListUp()
	{
		if (startItemGUI_Idx == 0)
		{
			return;
		}
		startItemGUI_Idx--;
		int num = startItemGUI_Idx;
		if (num >= 0)
		{
			Vector3 vector = default(Vector3);
			vector = new Vector3(0f, 340f, 0f);
			Vector3 vector2 = default(Vector3);
			vector2 = new Vector3(0f, -135f, 0f);
			PopupSelectNguyenKhiItem popupSelectNguyenKhiItem = ItemList[0];
			PopupSelectNguyenKhiItem popupSelectNguyenKhiItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(ItemList.Count - 1);
			ItemList.Insert(0, popupSelectNguyenKhiItem2);
			popupSelectNguyenKhiItem2.transform.localPosition = popupSelectNguyenKhiItem.transform.localPosition - vector2;
			popupSelectNguyenKhiItem2.Set(nguyenKhiList[num]);
			Utils.SetLayer(popupSelectNguyenKhiItem2.transform, "GUIPopUp", true);
			UICheckbox componentInChildren = popupSelectNguyenKhiItem2.GetComponentInChildren<UICheckbox>();
			if (nguyenKhiList[num].ID == mNguyenKhiSelectedID && componentInChildren != null)
			{
				componentInChildren.isChecked = true;
			}
			else
			{
				componentInChildren.isChecked = false;
			}
		}
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			UnityEngine.Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	public static void Create(Func<int, bool> onFinish, List<int> ignore_list, string title = null)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("Popup/PopupSelectNguyenKhi"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupSelectNguyenKhi>();
		instance.SyncWithNetworkData(ignore_list);
		instance.OnFinish = onFinish;
		EGDebug.Log("TITLE: " + title);
		if (title == null)
		{
			title = Localization.instance.Get("PopupSelectNguyenKhiTitle");
		}
		instance.lbTitle.text = title;
	}

	public void SyncWithNetworkData(List<int> ignore_list)
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		nguyenKhiList.Clear();
		startItemGUI_Idx = 0;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 340f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -135f, 0f);
		if (startItemGUI_Idx < 0)
		{
			startItemGUI_Idx = 0;
		}
		foreach (UserInfo.NguyenKhiData nguyenKhi in GameManager.instance.m_GameClient.UserInfo.NguyenKhiList)
		{
			OtherCfg.NguyenKhiCfg nguyenKhiCfg = ConfigManager.instance.OtherConfig.NguyenKhiConfig[nguyenKhi.Codename];
			if (ignore_list == null || !ignore_list.Contains(nguyenKhi.ID))
			{
				nguyenKhiList.Add(nguyenKhi);
			}
		}
		if (nguyenKhiList.Count <= 0)
		{
			return;
		}
		nguyenKhiList.Sort((UserInfo.NguyenKhiData x, UserInfo.NguyenKhiData y) => CompareNguyenKhi(x.Codename, x.Level, y.Codename, y.Level));
		int num = 0;
		for (int num2 = startItemGUI_Idx; num2 < nguyenKhiList.Count; num2++)
		{
			UserInfo.NguyenKhiData data = nguyenKhiList[num2];
			PopupSelectNguyenKhiItem component = ((GameObject)UnityEngine.Object.Instantiate(NguyenKhiPrefab)).GetComponent<PopupSelectNguyenKhiItem>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = vector;
			vector += vector2;
			component.Set(data);
			UICheckbox componentInChildren = component.GetComponentInChildren<UICheckbox>();
			if (componentInChildren != null)
			{
				UIEventListener.Get(componentInChildren.gameObject).onClick = checkBox_OnClick;
			}
			Utils.SetLayer(component.transform, "GUIPopUp", true);
			ItemList.Add(component);
			num++;
			if (num >= 12)
			{
				break;
			}
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}

	public void checkBox_OnClick(GameObject go)
	{
		UICheckbox component = go.GetComponent<UICheckbox>();
		PopupSelectNguyenKhiItem component2 = component.transform.parent.GetComponent<PopupSelectNguyenKhiItem>();
		if (component2 != null && component.isChecked)
		{
			mNguyenKhiSelectedID = component2.m_Data.ID;
		}
		EGDebug.Log("NguyenKhi SELECTED: " + mNguyenKhiSelectedID);
	}

	public int CompareNguyenKhi(string codeName1, int level1, string codeName2, int level2)
	{
		if (!ConfigManager.instance.OtherConfig.NguyenKhiConfig.ContainsKey(codeName1) || !ConfigManager.instance.OtherConfig.NguyenKhiConfig.ContainsKey(codeName2))
		{
			return -1;
		}
		if (level1 > level2)
		{
			return -1;
		}
		if (level1 < level2)
		{
			return 1;
		}
		return codeName1.CompareTo(codeName2);
	}

	public void OnOkClick()
	{
		PopupSelectNguyenKhiItem popupSelectNguyenKhiItem = null;
		foreach (PopupSelectNguyenKhiItem item in ItemList)
		{
			UICheckbox componentInChildren = item.GetComponentInChildren<UICheckbox>();
			if (componentInChildren.isChecked)
			{
				popupSelectNguyenKhiItem = item;
				break;
			}
		}
		if ((bool)popupSelectNguyenKhiItem)
		{
			OnFinish(popupSelectNguyenKhiItem.m_Data.ID);
			DestroyPopup();
		}
	}

	public void OnCancelClick()
	{
		DestroyPopup();
	}
}

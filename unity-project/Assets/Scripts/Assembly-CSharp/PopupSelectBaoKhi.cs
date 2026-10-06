using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupSelectBaoKhi : MonoBehaviour
{
	private const int maxItemCount = 12;

	public GameObject ItemRoot;

	public GameObject BaoKhiPrefab;

	public static PopupSelectBaoKhi instance;

	public Func<int, bool> OnFinish;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<PopupSelectBaoKhiItem> ItemList = new List<PopupSelectBaoKhiItem>();

	private int startItemGUI_Idx;

	private int mBaoKhiSelectedID;

	private Vector3 itemPos = new Vector3(0f, 340f, 0f);

	private Vector3 itemOffset = new Vector3(0f, -215f, 0f);

	private List<UserInfo.ThienMaLenhInfo> thienMaList = new List<UserInfo.ThienMaLenhInfo>();

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
			PopupSelectBaoKhiItem popupSelectBaoKhiItem = ItemList[ItemList.Count - 1];
			float y = popupSelectBaoKhiItem.transform.localPosition.y;
			PopupSelectBaoKhiItem popupSelectBaoKhiItem2 = ItemList[0];
			float y2 = popupSelectBaoKhiItem2.transform.localPosition.y;
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
		if (num < GameManager.instance.m_GameClient.UserInfo.ListThienMaLenh.Count)
		{
			PopupSelectBaoKhiItem popupSelectBaoKhiItem = ItemList[0];
			PopupSelectBaoKhiItem popupSelectBaoKhiItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(0);
			ItemList.Add(popupSelectBaoKhiItem);
			popupSelectBaoKhiItem.transform.localPosition = popupSelectBaoKhiItem2.transform.localPosition + itemOffset;
			popupSelectBaoKhiItem.Set(GameManager.instance.m_GameClient.UserInfo.ListThienMaLenh[num]);
			UICheckbox componentInChildren = popupSelectBaoKhiItem.GetComponentInChildren<UICheckbox>();
			if (GameManager.instance.m_GameClient.UserInfo.ListThienMaLenh[num].ID == mBaoKhiSelectedID && componentInChildren != null)
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
			PopupSelectBaoKhiItem popupSelectBaoKhiItem = ItemList[0];
			PopupSelectBaoKhiItem popupSelectBaoKhiItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(ItemList.Count - 1);
			ItemList.Insert(0, popupSelectBaoKhiItem2);
			popupSelectBaoKhiItem2.transform.localPosition = popupSelectBaoKhiItem.transform.localPosition - itemOffset;
			popupSelectBaoKhiItem2.Set(GameManager.instance.m_GameClient.UserInfo.ListThienMaLenh[num]);
			UICheckbox componentInChildren = popupSelectBaoKhiItem2.GetComponentInChildren<UICheckbox>();
			if (GameManager.instance.m_GameClient.UserInfo.ListThienMaLenh[num].ID == mBaoKhiSelectedID && componentInChildren != null)
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

	public static void Create(Func<int, bool> onFinish, List<int> listIgnore)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("Popup/PopupSelectBaoKhi"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupSelectBaoKhi>();
		instance.SyncWithNetworkData(listIgnore);
		instance.OnFinish = onFinish;
	}

	public void SyncWithNetworkData(List<int> ignore_list)
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		thienMaList.Clear();
		startItemGUI_Idx = 0;
		if (startItemGUI_Idx < 0)
		{
			startItemGUI_Idx = 0;
		}
		int num = 0;
		if (GameManager.instance.m_GameClient.UserInfo.ListThienMaLenh != null && GameManager.instance.m_GameClient.UserInfo.ListThienMaLenh.Count > 0)
		{
			foreach (UserInfo.ThienMaLenhInfo item2 in GameManager.instance.m_GameClient.UserInfo.ListThienMaLenh)
			{
				if (ignore_list == null || !ignore_list.Contains(item2.ID))
				{
					thienMaList.Add(item2);
				}
			}
			for (int i = startItemGUI_Idx; i < thienMaList.Count; i++)
			{
				UserInfo.ThienMaLenhInfo data = thienMaList[i];
				PopupSelectBaoKhiItem component = ((GameObject)UnityEngine.Object.Instantiate(BaoKhiPrefab)).GetComponent<PopupSelectBaoKhiItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				itemPos += itemOffset;
				component.Set(data);
				UICheckbox componentInChildren = component.GetComponentInChildren<UICheckbox>();
				if (componentInChildren != null)
				{
					UIEventListener.Get(componentInChildren.gameObject).onClick = checkBox_OnClick;
				}
				ItemList.Add(component);
				num++;
				if (num >= 12)
				{
					break;
				}
			}
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}

	public void checkBox_OnClick(GameObject go)
	{
		UICheckbox component = go.GetComponent<UICheckbox>();
		PopupSelectBaoKhiItem component2 = component.transform.parent.GetComponent<PopupSelectBaoKhiItem>();
		if (component2 != null && component.isChecked)
		{
			mBaoKhiSelectedID = component2.m_Data.ID;
		}
	}

	public void OnOkClick()
	{
		PopupSelectBaoKhiItem popupSelectBaoKhiItem = null;
		foreach (PopupSelectBaoKhiItem item in ItemList)
		{
			UICheckbox componentInChildren = item.GetComponentInChildren<UICheckbox>();
			if (componentInChildren.isChecked)
			{
				popupSelectBaoKhiItem = item;
				break;
			}
		}
		if ((bool)popupSelectBaoKhiItem)
		{
			OnFinish(popupSelectBaoKhiItem.m_Data.ID);
			DestroyPopup();
		}
	}

	public void OnCancelClick()
	{
		DestroyPopup();
	}
}

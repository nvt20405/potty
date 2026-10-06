using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupSelectVoCong : MonoBehaviour
{
	private const int maxItemCount = 12;

	public GameObject ItemRoot;

	public GameObject VoCongPrefab;

	public static PopupSelectVoCong instance;

	public Func<int, bool> OnFinish;

	public UILabel lbTitle;

	private List<UserInfo.VoCongData> voCongList = new List<UserInfo.VoCongData>();

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<PopupSelectVoCongItem> ItemList = new List<PopupSelectVoCongItem>();

	private int startItemGUI_Idx;

	private int mVoCongSelectedID;

	private bool isOpenByBatQuai;

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
			PopupSelectVoCongItem popupSelectVoCongItem = ItemList[ItemList.Count - 1];
			float y = popupSelectVoCongItem.transform.localPosition.y;
			PopupSelectVoCongItem popupSelectVoCongItem2 = ItemList[0];
			float y2 = popupSelectVoCongItem2.transform.localPosition.y;
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
		if (num < voCongList.Count)
		{
			Vector3 vector = default(Vector3);
			vector = new Vector3(0f, 340f, 0f);
			Vector3 vector2 = default(Vector3);
			vector2 = new Vector3(0f, -125f, 0f);
			PopupSelectVoCongItem popupSelectVoCongItem = ItemList[0];
			PopupSelectVoCongItem popupSelectVoCongItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(0);
			ItemList.Add(popupSelectVoCongItem);
			popupSelectVoCongItem.transform.localPosition = popupSelectVoCongItem2.transform.localPosition + vector2;
			popupSelectVoCongItem.Set(voCongList[num], isOpenByBatQuai);
			if (voCongList[num].ID == mVoCongSelectedID)
			{
				popupSelectVoCongItem.checkBox.isChecked = true;
			}
			else
			{
				popupSelectVoCongItem.checkBox.isChecked = false;
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
			vector2 = new Vector3(0f, -125f, 0f);
			PopupSelectVoCongItem popupSelectVoCongItem = ItemList[0];
			PopupSelectVoCongItem popupSelectVoCongItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(ItemList.Count - 1);
			ItemList.Insert(0, popupSelectVoCongItem2);
			popupSelectVoCongItem2.transform.localPosition = popupSelectVoCongItem.transform.localPosition - vector2;
			popupSelectVoCongItem2.Set(voCongList[num], isOpenByBatQuai);
			if (voCongList[num].ID == mVoCongSelectedID)
			{
				popupSelectVoCongItem2.checkBox.isChecked = true;
			}
			else
			{
				popupSelectVoCongItem2.checkBox.isChecked = false;
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

	public static void CreateByScreenThamNgo(Func<int, bool> onThamNgoFinish, List<int> ignore_list_TN, VCClass voCongTNClass)
	{
		Create(onThamNgoFinish, ignore_list_TN, voCongTNClass, true);
	}

	public static void Create(Func<int, bool> onFinish, List<int> ignore_list, VCClass voCongClass, bool isOpenByThamNgo = false, string title = null, bool isSelectToNVBatQuai = false)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("Popup/PopupSelectVoCong"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupSelectVoCong>();
		instance.isOpenByBatQuai = isSelectToNVBatQuai;
		instance.SyncWithNetworkData(ignore_list, voCongClass);
		instance.OnFinish = onFinish;
		if (title == null)
		{
			title = Localization.instance.Get("PopupSelectVoCongTitle");
		}
		instance.lbTitle.text = title;
	}

	public void SyncWithNetworkData(List<int> ignore_list, VCClass voCongClass)
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		voCongList.Clear();
		startItemGUI_Idx = 0;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 340f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -125f, 0f);
		if (startItemGUI_Idx < 0)
		{
			startItemGUI_Idx = 0;
		}
		if (GameManager.instance.m_GameClient.UserInfo.VoCongList != null && GameManager.instance.m_GameClient.UserInfo.VoCongList.Count > 0)
		{
			foreach (UserInfo.VoCongData voCong in GameManager.instance.m_GameClient.UserInfo.VoCongList)
			{
				CfgVoCong value;
				if (ConfigManager.instance.m_dicVCs.TryGetValue(voCong.Name, out value) && (voCongClass == VCClass.ALL || voCongClass == value.m_Class) && (ignore_list == null || !ignore_list.Contains(voCong.ID)))
				{
					voCongList.Add(voCong);
				}
			}
			if (voCongList != null && voCongList.Count > 0)
			{
				voCongList.Sort((UserInfo.VoCongData x, UserInfo.VoCongData y) => ConfigManager.instance.CompareVoCong(x.Name, x.Level, y.Name, y.Level));
				int num = 0;
				for (int num2 = startItemGUI_Idx; num2 < voCongList.Count; num2++)
				{
					UserInfo.VoCongData data = voCongList[num2];
					PopupSelectVoCongItem component = ((GameObject)UnityEngine.Object.Instantiate(VoCongPrefab)).GetComponent<PopupSelectVoCongItem>();
					component.transform.parent = ItemRoot.transform;
					component.transform.localScale = new Vector3(1f, 1f, 1f);
					component.transform.localPosition = vector;
					vector += vector2;
					component.Set(data, isOpenByBatQuai);
					UIEventListener.Get(component.checkBox.gameObject).onClick = checkBox_onClick;
					ItemList.Add(component);
					num++;
					if (num >= 12)
					{
						break;
					}
				}
			}
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}

	public static void CreateBy_S(Func<int, bool> onFinish)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("Popup/PopupSelectVoCong"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupSelectVoCong>();
		instance.SyncBy_S();
		instance.OnFinish = onFinish;
		instance.lbTitle.text = Localization.instance.Get("PopupSelectVoCongTitle");
	}

	public void SyncBy_S()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		voCongList.Clear();
		startItemGUI_Idx = 0;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 340f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -125f, 0f);
		if (startItemGUI_Idx < 0)
		{
			startItemGUI_Idx = 0;
		}
		if (GameManager.instance.m_GameClient.UserInfo.VoCongList != null && GameManager.instance.m_GameClient.UserInfo.VoCongList.Count > 0)
		{
			foreach (UserInfo.VoCongData voCong in GameManager.instance.m_GameClient.UserInfo.VoCongList)
			{
				CfgVoCong value;
				ConfigManager.instance.m_dicVCs.TryGetValue(voCong.Name, out value);
				if (voCong.Name.EndsWith("_S") && voCong.HID == 0 && value != null && value.Hang >= 3)
				{
					voCongList.Add(voCong);
				}
			}
			if (voCongList != null && voCongList.Count > 0)
			{
				voCongList.Sort((UserInfo.VoCongData x, UserInfo.VoCongData y) => ConfigManager.instance.CompareVoCong(x.Name, x.Level, y.Name, y.Level));
				int num = 0;
				for (int num2 = startItemGUI_Idx; num2 < voCongList.Count; num2++)
				{
					UserInfo.VoCongData data = voCongList[num2];
					PopupSelectVoCongItem component = ((GameObject)UnityEngine.Object.Instantiate(VoCongPrefab)).GetComponent<PopupSelectVoCongItem>();
					component.transform.parent = ItemRoot.transform;
					component.transform.localScale = new Vector3(1f, 1f, 1f);
					component.transform.localPosition = vector;
					vector += vector2;
					component.Set(data, isOpenByBatQuai);
					UIEventListener.Get(component.checkBox.gameObject).onClick = checkBox_onClick;
					ItemList.Add(component);
					num++;
					if (num >= 12)
					{
						break;
					}
				}
			}
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}

	public void checkBox_onClick(GameObject go)
	{
		UICheckbox component = go.GetComponent<UICheckbox>();
		PopupSelectVoCongItem component2 = component.transform.parent.GetComponent<PopupSelectVoCongItem>();
		if (component2 != null && component.isChecked)
		{
			mVoCongSelectedID = component2.m_Data.ID;
		}
		EGDebug.Log("VO CONG SELECTED: " + mVoCongSelectedID);
	}

	public void OnOkClick()
	{
		PopupSelectVoCongItem popupSelectVoCongItem = null;
		foreach (PopupSelectVoCongItem item in ItemList)
		{
			UICheckbox componentInChildren = item.GetComponentInChildren<UICheckbox>();
			if (componentInChildren.isChecked)
			{
				popupSelectVoCongItem = item;
				break;
			}
		}
		if ((bool)popupSelectVoCongItem)
		{
			OnFinish(popupSelectVoCongItem.m_Data.ID);
			DestroyPopup();
		}
	}

	public void OnCancelClick()
	{
		DestroyPopup();
	}
}

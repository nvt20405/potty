using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupSelectMultiThanThu : MonoBehaviour
{
	private const int maxItemCount = 12;

	public GameObject ItemRoot;

	public GameObject VoCongPrefab;

	public static PopupSelectMultiThanThu instance;

	public Func<List<int>, bool> OnFinish;

	public UILabel lbTitle;

	private List<int> used_vocong_list = new List<int>();

	private List<UserInfo.PetInfo> voCongList = new List<UserInfo.PetInfo>();

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<PopupSelectMultiThanThuItem> ItemList = new List<PopupSelectMultiThanThuItem>();

	private int startItemGUI_Idx;

	private bool isOpenByThamNgoScreen;

	private int maxItemSelect;

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
			PopupSelectMultiThanThuItem popupSelectMultiThanThuItem = ItemList[ItemList.Count - 1];
			float y = popupSelectMultiThanThuItem.transform.localPosition.y;
			PopupSelectMultiThanThuItem popupSelectMultiThanThuItem2 = ItemList[0];
			float y2 = popupSelectMultiThanThuItem2.transform.localPosition.y;
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
			PopupSelectMultiThanThuItem popupSelectMultiThanThuItem = ItemList[0];
			PopupSelectMultiThanThuItem popupSelectMultiThanThuItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(0);
			ItemList.Add(popupSelectMultiThanThuItem);
			popupSelectMultiThanThuItem.transform.localPosition = popupSelectMultiThanThuItem2.transform.localPosition + vector2;
			popupSelectMultiThanThuItem.Set(voCongList[num]);
			if (used_vocong_list != null && used_vocong_list.Count > 0 && used_vocong_list.Contains(voCongList[num].ID))
			{
				popupSelectMultiThanThuItem.checkBox.isChecked = true;
			}
			else
			{
				popupSelectMultiThanThuItem.checkBox.isChecked = false;
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
			PopupSelectMultiThanThuItem popupSelectMultiThanThuItem = ItemList[0];
			PopupSelectMultiThanThuItem popupSelectMultiThanThuItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(ItemList.Count - 1);
			ItemList.Insert(0, popupSelectMultiThanThuItem2);
			popupSelectMultiThanThuItem2.transform.localPosition = popupSelectMultiThanThuItem.transform.localPosition - vector2;
			popupSelectMultiThanThuItem2.Set(voCongList[num]);
			if (used_vocong_list != null && used_vocong_list.Count > 0 && used_vocong_list.Contains(voCongList[num].ID))
			{
				popupSelectMultiThanThuItem2.checkBox.isChecked = true;
			}
			else
			{
				popupSelectMultiThanThuItem2.checkBox.isChecked = false;
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

	public static void Create(Func<List<int>, bool> onFinish, List<int> ignore_list, List<int> used_list, int maxItemSelect = 0)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("popup/PopupSelectMultiThanThu"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupSelectMultiThanThu>();
		if (used_list != null)
		{
			instance.used_vocong_list.Clear();
			foreach (int item in used_list)
			{
				instance.used_vocong_list.Add(item);
			}
		}
		instance.SyncWithNetworkData(ignore_list, used_list);
		instance.OnFinish = onFinish;
		instance.maxItemSelect = maxItemSelect;
	}

	public void SyncWithNetworkData(List<int> ignore_list, List<int> used_list)
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
		if (GameManager.instance.m_GameClient.UserInfo.ListThanThu != null && GameManager.instance.m_GameClient.UserInfo.ListThanThu.Count > 0)
		{
			UserInfo.PetInfo vc;
			foreach (UserInfo.PetInfo item2 in GameManager.instance.m_GameClient.UserInfo.ListThanThu)
			{
				vc = item2;
				if (ignore_list == null || !ignore_list.Exists((int p) => p == vc.ID))
				{
					voCongList.Add(vc);
				}
			}
			voCongList.Sort((UserInfo.PetInfo x, UserInfo.PetInfo y) => ConfigManager.instance.CompareThanThu(x, y));
			int num = 0;
			for (int num2 = startItemGUI_Idx; num2 < voCongList.Count; num2++)
			{
				UserInfo.PetInfo petInfo = voCongList[num2];
				PopupSelectMultiThanThuItem component = ((GameObject)UnityEngine.Object.Instantiate(VoCongPrefab)).GetComponent<PopupSelectMultiThanThuItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = vector;
				UIEventListener.Get(component.checkBox.gameObject).onClick = onClick_CheckBox;
				vector += vector2;
				component.Set(petInfo);
				if (used_list != null && used_list.Count > 0 && used_list.Contains(petInfo.ID))
				{
					component.checkBox.isChecked = true;
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

	public void onClick_CheckBox(GameObject go)
	{
		EGDebug.Log("onClick_VoCongItem");
		PopupSelectMultiThanThuItem component = go.transform.parent.GetComponent<PopupSelectMultiThanThuItem>();
		if (maxItemSelect > 0 && used_vocong_list.Count >= maxItemSelect)
		{
			component.checkBox.isChecked = false;
		}
		if (component.checkBox.isChecked && !used_vocong_list.Contains(component.m_Data.ID))
		{
			used_vocong_list.Add(component.m_Data.ID);
		}
		else if (!component.checkBox.isChecked && used_vocong_list.Contains(component.m_Data.ID))
		{
			used_vocong_list.Remove(component.m_Data.ID);
		}
	}

	public void OnOkClick()
	{
		if (used_vocong_list.Count > 0)
		{
			OnFinish(used_vocong_list);
			DestroyPopup();
		}
	}

	public void OnCancelClick()
	{
		DestroyPopup();
	}
}

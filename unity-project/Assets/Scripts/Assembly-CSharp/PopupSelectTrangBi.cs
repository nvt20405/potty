using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupSelectTrangBi : MonoBehaviour
{
	private const int maxItemCount = 12;

	public GameObject ItemRoot;

	public GameObject TrangBiPrefab;

	public static PopupSelectTrangBi instance;

	public Func<int, bool> OnFinish;

	private List<UserInfo.TrangBiData> trangBiList = new List<UserInfo.TrangBiData>();

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<PopupSelectTrangBiItem> ItemList = new List<PopupSelectTrangBiItem>();

	private int startItemGUI_Idx;

	public UILabel lbTitle;

	private int mTrangBiSelectedID;

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
			PopupSelectTrangBiItem popupSelectTrangBiItem = ItemList[ItemList.Count - 1];
			float y = popupSelectTrangBiItem.transform.localPosition.y;
			PopupSelectTrangBiItem popupSelectTrangBiItem2 = ItemList[0];
			float y2 = popupSelectTrangBiItem2.transform.localPosition.y;
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
		if (num < trangBiList.Count)
		{
			Vector3 vector = default(Vector3);
			vector = new Vector3(0f, 340f, 0f);
			Vector3 vector2 = default(Vector3);
			vector2 = new Vector3(0f, -135f, 0f);
			PopupSelectTrangBiItem popupSelectTrangBiItem = ItemList[0];
			PopupSelectTrangBiItem popupSelectTrangBiItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(0);
			ItemList.Add(popupSelectTrangBiItem);
			popupSelectTrangBiItem.transform.localPosition = popupSelectTrangBiItem2.transform.localPosition + vector2;
			popupSelectTrangBiItem.Set(trangBiList[num]);
			UICheckbox componentInChildren = popupSelectTrangBiItem.GetComponentInChildren<UICheckbox>();
			if (trangBiList[num].ID == mTrangBiSelectedID && componentInChildren != null)
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
			PopupSelectTrangBiItem popupSelectTrangBiItem = ItemList[0];
			PopupSelectTrangBiItem popupSelectTrangBiItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(ItemList.Count - 1);
			ItemList.Insert(0, popupSelectTrangBiItem2);
			popupSelectTrangBiItem2.transform.localPosition = popupSelectTrangBiItem.transform.localPosition - vector2;
			popupSelectTrangBiItem2.Set(trangBiList[num]);
			UICheckbox componentInChildren = popupSelectTrangBiItem2.GetComponentInChildren<UICheckbox>();
			if (trangBiList[num].ID == mTrangBiSelectedID && componentInChildren != null)
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

	public static void Create(Func<int, bool> onFinish, List<int> ignore_list, LoaiTrangBi loaiTrangBi, string title = null, ChiSoCoBan chiSoUuTien = ChiSoCoBan.None)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("Popup/PopupSelectTrangBi"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupSelectTrangBi>();
		instance.SyncWithNetworkData(ignore_list, loaiTrangBi, chiSoUuTien);
		instance.OnFinish = onFinish;
		EGDebug.Log("TITLE: " + title);
		if (title == null)
		{
			title = Localization.instance.Get("PopupSelectTrangBiTitle");
		}
		instance.lbTitle.text = title;
	}

	public void SyncWithNetworkData(List<int> ignore_list, LoaiTrangBi loaiTrangBi, ChiSoCoBan chisoUuTien = ChiSoCoBan.None)
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		trangBiList.Clear();
		startItemGUI_Idx = 0;
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 340f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -135f, 0f);
		if (startItemGUI_Idx < 0)
		{
			startItemGUI_Idx = 0;
		}
		foreach (UserInfo.TrangBiData trangBi in GameManager.instance.m_GameClient.UserInfo.TrangBiList)
		{
			if (ConfigManager.instance.m_dicTrangBi.ContainsKey(trangBi.Name))
			{
				TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[trangBi.Name];
				if ((loaiTrangBi == LoaiTrangBi.None || loaiTrangBi == TrangBiCfg.GetLoaiTrangBi(trangBi.Name)) && (ignore_list == null || !ignore_list.Contains(trangBi.ID)))
				{
					trangBiList.Add(trangBi);
				}
			}
			else
			{
				EGDebug.Log("Trang bị không tồn tại " + trangBi.Name);
			}
		}
		if (chisoUuTien != ChiSoCoBan.None)
		{
			trangBiList.Sort((UserInfo.TrangBiData x, UserInfo.TrangBiData y) => CompareTrangBiByChiSo(chisoUuTien, x.Name, x.Level, y.Name, y.Level));
		}
		else
		{
			trangBiList.Sort((UserInfo.TrangBiData x, UserInfo.TrangBiData y) => ConfigManager.instance.CompareTrangBi(x.Name, x.Level, y.Name, y.Level));
		}
		int num = 0;
		for (int num2 = startItemGUI_Idx; num2 < trangBiList.Count; num2++)
		{
			UserInfo.TrangBiData data = trangBiList[num2];
			PopupSelectTrangBiItem component = ((GameObject)UnityEngine.Object.Instantiate(TrangBiPrefab)).GetComponent<PopupSelectTrangBiItem>();
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
		PopupSelectTrangBiItem component2 = component.transform.parent.GetComponent<PopupSelectTrangBiItem>();
		if (component2 != null && component.isChecked)
		{
			mTrangBiSelectedID = component2.m_Data.ID;
		}
		EGDebug.Log("VO CONG SELECTED: " + mTrangBiSelectedID);
	}

	public void OnOkClick()
	{
		PopupSelectTrangBiItem popupSelectTrangBiItem = null;
		foreach (PopupSelectTrangBiItem item in ItemList)
		{
			UICheckbox componentInChildren = item.GetComponentInChildren<UICheckbox>();
			if (componentInChildren.isChecked)
			{
				popupSelectTrangBiItem = item;
				break;
			}
		}
		if ((bool)popupSelectTrangBiItem)
		{
			OnFinish(popupSelectTrangBiItem.m_Data.ID);
			DestroyPopup();
		}
	}

	public void OnCancelClick()
	{
		DestroyPopup();
	}

	public int CompareTrangBiByChiSo(ChiSoCoBan chiSoUuTien, string codeName1, int level1, string codeName2, int level2)
	{
		if (!ConfigManager.instance.m_dicTrangBi.ContainsKey(codeName1) || !ConfigManager.instance.m_dicTrangBi.ContainsKey(codeName2))
		{
			return -1;
		}
		string empty = string.Empty;
		string text;
		switch (chiSoUuTien)
		{
		case ChiSoCoBan.Menh:
			text = "TS_";
			break;
		case ChiSoCoBan.Ngoai:
			text = "VK_";
			break;
		case ChiSoCoBan.ThanPhap:
			text = "AG_";
			break;
		case ChiSoCoBan.Noi:
			text = "MU_";
			break;
		default:
			text = string.Empty;
			break;
		}
		empty = text;
		ItemClass hang = ConfigManager.instance.m_dicTrangBi[codeName1].Hang;
		ItemClass hang2 = ConfigManager.instance.m_dicTrangBi[codeName2].Hang;
		if (codeName1.StartsWith(empty) && codeName2.StartsWith(empty))
		{
			if (hang > hang2 && hang == ItemClass.Giap)
			{
				return -1;
			}
			if (hang2 > hang && hang2 == ItemClass.Giap)
			{
				return 1;
			}
		}
		else
		{
			if (codeName1.StartsWith(empty) && hang == ItemClass.Giap)
			{
				return -1;
			}
			if (codeName2.StartsWith(empty) && hang2 == ItemClass.Giap)
			{
				return 1;
			}
		}
		if (hang > hang2)
		{
			return -1;
		}
		if (hang < hang2)
		{
			return 1;
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
}

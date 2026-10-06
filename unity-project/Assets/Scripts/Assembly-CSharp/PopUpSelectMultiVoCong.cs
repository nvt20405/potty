using System;
using System.Collections.Generic;
using UnityEngine;

public class PopUpSelectMultiVoCong : MonoBehaviour
{
	private const int maxItemCount = 12;

	public GameObject ItemRoot;

	public GameObject VoCongPrefab;

	public static PopUpSelectMultiVoCong instance;

	public Func<List<int>, bool> OnFinish;

	public UILabel lbTitle;

	private List<int> used_vocong_list = new List<int>();

	private List<UserInfo.VoCongData> voCongList = new List<UserInfo.VoCongData>();

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<PopUpSelectMultiVoCongItem> ItemList = new List<PopUpSelectMultiVoCongItem>();

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
			PopUpSelectMultiVoCongItem popUpSelectMultiVoCongItem = ItemList[ItemList.Count - 1];
			float y = popUpSelectMultiVoCongItem.transform.localPosition.y;
			PopUpSelectMultiVoCongItem popUpSelectMultiVoCongItem2 = ItemList[0];
			float y2 = popUpSelectMultiVoCongItem2.transform.localPosition.y;
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
			PopUpSelectMultiVoCongItem popUpSelectMultiVoCongItem = ItemList[0];
			PopUpSelectMultiVoCongItem popUpSelectMultiVoCongItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(0);
			ItemList.Add(popUpSelectMultiVoCongItem);
			popUpSelectMultiVoCongItem.transform.localPosition = popUpSelectMultiVoCongItem2.transform.localPosition + vector2;
			popUpSelectMultiVoCongItem.Set(voCongList[num]);
			if (used_vocong_list != null && used_vocong_list.Count > 0 && used_vocong_list.Contains(voCongList[num].ID))
			{
				popUpSelectMultiVoCongItem.checkBox.isChecked = true;
			}
			else
			{
				popUpSelectMultiVoCongItem.checkBox.isChecked = false;
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
			PopUpSelectMultiVoCongItem popUpSelectMultiVoCongItem = ItemList[0];
			PopUpSelectMultiVoCongItem popUpSelectMultiVoCongItem2 = ItemList[ItemList.Count - 1];
			ItemList.RemoveAt(ItemList.Count - 1);
			ItemList.Insert(0, popUpSelectMultiVoCongItem2);
			popUpSelectMultiVoCongItem2.transform.localPosition = popUpSelectMultiVoCongItem.transform.localPosition - vector2;
			popUpSelectMultiVoCongItem2.Set(voCongList[num]);
			if (used_vocong_list != null && used_vocong_list.Count > 0 && used_vocong_list.Contains(voCongList[num].ID))
			{
				popUpSelectMultiVoCongItem2.checkBox.isChecked = true;
			}
			else
			{
				popUpSelectMultiVoCongItem2.checkBox.isChecked = false;
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

	public static void Create(Func<List<int>, bool> onFinish, List<int> ignore_list, List<int> used_list, VCClass voCongClass, int maxItemSelect = 0, string title = null, bool isOpenByThamNgo = false)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("popup/PopupSelectMultiVoCong"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopUpSelectMultiVoCong>();
		if (used_list != null)
		{
			instance.used_vocong_list.Clear();
			foreach (int item in used_list)
			{
				instance.used_vocong_list.Add(item);
			}
		}
		instance.isOpenByThamNgoScreen = isOpenByThamNgo;
		instance.SyncWithNetworkData(ignore_list, used_list, voCongClass);
		instance.OnFinish = onFinish;
		instance.maxItemSelect = maxItemSelect;
		if (title == null || title.Length < 3)
		{
			title = Localization.instance.Get("PopupSelectVoCongTitle");
		}
		instance.lbTitle.text = title;
	}

	public void SyncWithNetworkData(List<int> ignore_list, List<int> used_list, VCClass voCongClass)
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
				if (ConfigManager.instance.m_dicVCs.TryGetValue(voCong.Name, out value))
				{
					if ((voCongClass == VCClass.ALL || voCongClass == value.m_Class) && (ignore_list == null || !ignore_list.Contains(voCong.ID)))
					{
						voCongList.Add(voCong);
					}
				}
				else
				{
					int num = 0;
				}
			}
			if (!isOpenByThamNgoScreen)
			{
				voCongList.Sort((UserInfo.VoCongData x, UserInfo.VoCongData y) => ConfigManager.instance.CompareVoCong(x.Name, x.Level, y.Name, y.Level));
			}
			else
			{
				voCongList.Sort((UserInfo.VoCongData x, UserInfo.VoCongData y) => CompareVoCongThamNgo(y.Name, y.Level, x.Name, x.Level));
			}
			int num2 = 0;
			for (int num3 = startItemGUI_Idx; num3 < voCongList.Count; num3++)
			{
				UserInfo.VoCongData voCongData = voCongList[num3];
				PopUpSelectMultiVoCongItem component = ((GameObject)UnityEngine.Object.Instantiate(VoCongPrefab)).GetComponent<PopUpSelectMultiVoCongItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = vector;
				UIEventListener.Get(component.gameObject).onClick = onClick_VoCongItem;
				UIEventListener.Get(component.checkBox.gameObject).onClick = onClick_CheckBox;
				vector += vector2;
				component.Set(voCongData);
				if (used_list != null && used_list.Count > 0 && used_list.Contains(voCongData.ID))
				{
					component.checkBox.isChecked = true;
				}
				ItemList.Add(component);
				num2++;
				if (num2 >= 12)
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
		PopUpSelectMultiVoCongItem component = go.transform.parent.GetComponent<PopUpSelectMultiVoCongItem>();
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

	public void onClick_VoCongItem(GameObject go)
	{
		PopUpSelectMultiVoCongItem component = go.GetComponent<PopUpSelectMultiVoCongItem>();
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

	public int CompareVoCongThamNgo(string codeName1, int level1, string codeName2, int level2, int ID1 = 0, int ID2 = 0)
	{
		if (!ConfigManager.instance.m_dicVCs.ContainsKey(codeName1) || !ConfigManager.instance.m_dicVCs.ContainsKey(codeName2))
		{
			return -1;
		}
		if ((ConfigManager.instance.m_dicVCs[codeName1].m_Class == VCClass.BO_PHAP || ConfigManager.instance.m_dicVCs[codeName1].m_Class == VCClass.NOI_CONG) && (ConfigManager.instance.m_dicVCs[codeName2].m_Class == VCClass.BO_PHAP || ConfigManager.instance.m_dicVCs[codeName2].m_Class == VCClass.NOI_CONG) && ConfigManager.instance.m_dicVCs[codeName1].Hang >= 2 && ConfigManager.instance.m_dicVCs[codeName2].Hang >= 2)
		{
			if (ConfigManager.instance.m_dicVCs[codeName1].Hang > ConfigManager.instance.m_dicVCs[codeName2].Hang && ConfigManager.instance.m_dicVCs[codeName1].Hang >= 2)
			{
				return -1;
			}
			if (ConfigManager.instance.m_dicVCs[codeName1].Hang < ConfigManager.instance.m_dicVCs[codeName2].Hang && ConfigManager.instance.m_dicVCs[codeName2].Hang >= 2)
			{
				return 1;
			}
		}
		if ((ConfigManager.instance.m_dicVCs[codeName1].m_Class == VCClass.BO_PHAP || ConfigManager.instance.m_dicVCs[codeName1].m_Class == VCClass.NOI_CONG) && ConfigManager.instance.m_dicVCs[codeName2].m_Class != VCClass.BO_PHAP && ConfigManager.instance.m_dicVCs[codeName2].m_Class != VCClass.NOI_CONG && ConfigManager.instance.m_dicVCs[codeName1].Hang >= 2)
		{
			return -1;
		}
		if (ConfigManager.instance.m_dicVCs[codeName1].m_Class != VCClass.BO_PHAP && ConfigManager.instance.m_dicVCs[codeName1].m_Class != VCClass.NOI_CONG && (ConfigManager.instance.m_dicVCs[codeName2].m_Class == VCClass.BO_PHAP || ConfigManager.instance.m_dicVCs[codeName2].m_Class == VCClass.NOI_CONG) && ConfigManager.instance.m_dicVCs[codeName2].Hang >= 2)
		{
			return 1;
		}
		if (ConfigManager.instance.m_dicVCs[codeName1].Hang > ConfigManager.instance.m_dicVCs[codeName2].Hang)
		{
			return -1;
		}
		if (ConfigManager.instance.m_dicVCs[codeName1].Hang < ConfigManager.instance.m_dicVCs[codeName2].Hang)
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
		if (codeName1 == codeName2)
		{
			if (ID1 < ID2)
			{
				return -1;
			}
			if (ID2 > ID1)
			{
				return 1;
			}
			return 0;
		}
		return codeName1.CompareTo(codeName2);
	}
}

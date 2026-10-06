using System;
using System.Collections.Generic;
using UnityEngine;

public class ListBase : GameObjectBase
{
	public delegate void OnSelectionSet();

	public delegate void OnSelectionChange();

	public delegate void OnListDataChange();

	public delegate void OnListItemClick();

	public delegate void OnListItemPartClick(int itemIndex, int partIndex);

	public delegate void OnListItemDrag(GameObjectBase item);

	public delegate void OnListItemDrop(GameObjectBase item);

	public delegate void OnShowCollapsibleItem(int index);

	public List<Dictionary<string, string>> overrideData;

	public GameObject itemPrefab;

	public bool isCollapsibleList;

	public Dictionary<int, GameObject> collapsibleItems;

	public float collapsibleOffsetY = -2f;

	public float collapsibleOffsetX = 2f;

	public bool isCollapsiblePositionUpdate;

	public bool isIconPixelPerfect = true;

	public bool isItemClickableOnNull;

	public LAYOUT layout;

	public int maxCols;

	public int maxRows;

	public List<ListItemBase> items;

	private List<Dictionary<string, string>> _listData;

	public bool isItemHiddenOnNull;

	public List<Dictionary<string, string>> pageData;

	public float itemWidth = 100f;

	public float itemHeight = 100f;

	public float hSpace;

	public float vSpace;

	public int itemsPerPage;

	public bool isSelectItemOnClick = true;

	public Transform container;

	public GameObject itemSelect;

	public ButtonBase btnNextPage;

	public ButtonBase btnPrevPage;

	public int pageItemStartIndex;

	public UILabel labelPage;

	public bool isChangeFocusOnMouseUp;

	public bool isItemCheckedOnClick;

	public OnSelectionSet onSelectionSet;

	public OnSelectionChange onSelectionChange;

	public OnListDataChange onListDataChange;

	public OnListItemClick onListItemClick;

	public OnListItemPartClick onListItemPartClick;

	public OnListItemDrag onListItemDrag;

	public OnListItemDrop onListItemDrop;

	public List<int> selectedIndexes;

	public bool isMultiSelectEnabled;

	public bool isSelectHandledOnParent = true;

	public Dictionary<int, float> rowOffsets;

	public Dictionary<int, float> colOffsets;

	public int oldSelectedIndex = -1;

	private int _selectedIndex = -1;

	public OnShowCollapsibleItem onShowCollapsibleItem;

	public int selectedCollapsibleItemIndex = -1;

	private DDKEY _itemDragKey;

	private DDKEY _itemDropKey;

	private int startItemGUI_Idx;

	public bool isSwapList;

	public UIPanel panelContainer;

	public int maxVisibleItems;

	public float oriPanelContainerCenterY;

	public List<Dictionary<string, string>> listData
	{
		get
		{
			return _listData;
		}
		set
		{
			_listData = value;
			if (onListDataChange != null)
			{
				onListDataChange();
			}
		}
	}

	public int selectedIndex
	{
		get
		{
			return _selectedIndex;
		}
		set
		{
			bool flag = false;
			if (!isMultiSelectEnabled)
			{
				if (items != null && _selectedIndex >= 0 && items.Count > listIndexToPageIndex(_selectedIndex) && _selectedIndex != value)
				{
					items[listIndexToPageIndex(_selectedIndex)].isSelected = false;
				}
				if (_selectedIndex != value)
				{
					flag = true;
				}
				oldSelectedIndex = _selectedIndex;
				_selectedIndex = value;
				showCollapsibleItem(_selectedIndex);
				if (_selectedIndex < 0)
				{
					showItemSelect(null);
				}
				else if (items != null && items.Count > listIndexToPageIndex(_selectedIndex) && !isMultiSelectEnabled)
				{
					items[listIndexToPageIndex(_selectedIndex)].isSelected = true;
				}
			}
			else
			{
				oldSelectedIndex = _selectedIndex;
				_selectedIndex = value;
				if (_selectedIndex >= 0)
				{
					if (selectedIndexes == null)
					{
						selectedIndexes = new List<int>();
					}
					if (items[listIndexToPageIndex(_selectedIndex)].isSelected)
					{
						items[listIndexToPageIndex(_selectedIndex)].isSelected = false;
						selectedIndexes.Remove(_selectedIndex);
					}
					else
					{
						selectedIndexes.Add(value);
						items[listIndexToPageIndex(value)].isSelected = true;
					}
					flag = true;
				}
			}
			if (onSelectionSet != null)
			{
				onSelectionSet();
			}
			if (flag && onSelectionChange != null)
			{
				onSelectionChange();
			}
		}
	}

	public DDKEY itemDragKey
	{
		get
		{
			return _itemDragKey;
		}
		set
		{
			_itemDragKey = value;
			if (items != null)
			{
				for (int i = 0; i < items.Count; i++)
				{
					items[i].dragKey = _itemDragKey;
				}
			}
		}
	}

	public DDKEY itemDropKey
	{
		get
		{
			return _itemDropKey;
		}
		set
		{
			_itemDropKey = value;
			if (items != null)
			{
				for (int i = 0; i < items.Count; i++)
				{
					items[i].dropKey = _itemDropKey;
				}
			}
		}
	}

	public void selectFirstIndex()
	{
		for (int i = 0; i < pageData.Count; i++)
		{
			if (pageData[i] != null)
			{
				selectedIndex = i;
				break;
			}
		}
	}

	public void showCollapsibleItem(int index)
	{
		if (!isCollapsibleList)
		{
			return;
		}
		if (index != selectedCollapsibleItemIndex && selectedCollapsibleItemIndex >= 0 && items[selectedCollapsibleItemIndex].collapsibleContent != null)
		{
			items[selectedCollapsibleItemIndex].collapsibleContent.SetActive(false);
		}
		if (index >= 0 && items[index].collapsibleContent != null)
		{
			items[index].collapsibleContent.SetActive(true);
		}
		selectedCollapsibleItemIndex = index;
		if (isCollapsiblePositionUpdate)
		{
			layoutList();
			if (selectedCollapsibleItemIndex >= 0 && items[selectedCollapsibleItemIndex].collapsibleContent != null)
			{
				if (layout == LAYOUT.Vertical)
				{
					for (int i = selectedCollapsibleItemIndex + 1; i < items.Count; i++)
					{
						items[i].transform.localPosition = new Vector3(items[i].transform.localPosition.x, items[i].transform.localPosition.y - items[selectedCollapsibleItemIndex].collapsibleContentHeight + collapsibleOffsetY, items[i].transform.localPosition.z);
					}
					items[selectedCollapsibleItemIndex].collapsibleContent.transform.localPosition = new Vector3(items[selectedCollapsibleItemIndex].transform.localPosition.x, items[selectedCollapsibleItemIndex].transform.localPosition.y - itemHeight + collapsibleOffsetY, items[selectedCollapsibleItemIndex].transform.localPosition.z);
				}
				else
				{
					for (int j = selectedCollapsibleItemIndex + 1; j < items.Count; j++)
					{
						items[j].transform.localPosition = new Vector3(items[j].transform.localPosition.x + items[selectedCollapsibleItemIndex].collapsibleContentWidth + collapsibleOffsetX, items[j].transform.localPosition.y, items[j].transform.localPosition.z);
					}
					items[selectedCollapsibleItemIndex].collapsibleContent.transform.localPosition = new Vector3(items[selectedCollapsibleItemIndex].transform.localPosition.x + itemWidth + collapsibleOffsetX, items[selectedCollapsibleItemIndex].transform.localPosition.y, items[selectedCollapsibleItemIndex].transform.localPosition.z);
				}
			}
		}
		if (onShowCollapsibleItem != null)
		{
			onShowCollapsibleItem(index);
		}
	}

	public int listIndexToPageIndex(int listIndex)
	{
		if (listIndex < 0 || itemsPerPage <= 0)
		{
			return listIndex;
		}
		return listIndex % itemsPerPage;
	}

	private void Awake()
	{
		if (container == null)
		{
			container = new GameObject().transform;
			container.name = "Container";
			container.parent = base.transform;
			container.localScale = Vector3.one;
			container.localPosition = Vector3.zero;
		}
		if (container != null)
		{
			panelContainer = container.GetComponent<UIPanel>();
			if (panelContainer == null || layout == LAYOUT.Horizontal)
			{
				isSwapList = false;
			}
			else
			{
				oriPanelContainerCenterY = panelContainer.clipRange.y;
			}
		}
		if (btnNextPage != null)
		{
			UIEventListener uIEventListener = UIEventListener.Get(btnNextPage.gameObject);
			uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(goNextPage));
		}
		if (btnPrevPage != null)
		{
			UIEventListener uIEventListener2 = UIEventListener.Get(btnPrevPage.gameObject);
			uIEventListener2.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener2.onClick, new UIEventListener.VoidDelegate(goPrevPage));
		}
	}

	private void Start()
	{
		if (isCollapsibleList && isMultiSelectEnabled)
		{
		}
	}

	public void clearSelection()
	{
		oldSelectedIndex = -1;
		if (items == null || items.Count == 0)
		{
			return;
		}
		if (isMultiSelectEnabled)
		{
			for (int i = 0; i < selectedIndexes.Count; i++)
			{
				items[selectedIndexes[i]].isSelected = false;
			}
			selectedIndexes = new List<int>();
			_selectedIndex = -1;
		}
		else
		{
			selectedIndex = -1;
		}
	}

	private void Update()
	{
		if (isSwapList && panelContainer != null && items != null && items.Count > 0)
		{
			Vector4 clipRange = panelContainer.clipRange;
			ListItemBase listItemBase = items[items.Count - 1];
			float y = listItemBase.transform.localPosition.y;
			ListItemBase listItemBase2 = items[0];
			float y2 = listItemBase2.transform.localPosition.y;
			if (y - clipRange.y + oriPanelContainerCenterY > 0f - clipRange.w)
			{
				SwapDragListDown();
			}
			else if (y2 - clipRange.y + oriPanelContainerCenterY < clipRange.w)
			{
				SwapDragListUp();
			}
		}
	}

	public void SwapDragListDown()
	{
		int num = startItemGUI_Idx + maxVisibleItems;
		if (num < pageData.Count)
		{
			Vector3 vector = default(Vector3);
			vector = new Vector3(0f, 0f - itemHeight - vSpace, 0f);
			ListItemBase listItemBase = items[0];
			ListItemBase listItemBase2 = items[items.Count - 1];
			items.RemoveAt(0);
			items.Add(listItemBase);
			listItemBase.transform.localPosition = listItemBase2.transform.localPosition + vector;
			listItemBase.renderItem(pageData[num]);
			startItemGUI_Idx++;
		}
	}

	public void SwapDragListUp()
	{
		if (startItemGUI_Idx != 0)
		{
			startItemGUI_Idx--;
			int num = startItemGUI_Idx;
			if (num >= 0)
			{
				Vector3 vector = default(Vector3);
				vector = new Vector3(0f, 0f - itemHeight - vSpace, 0f);
				ListItemBase listItemBase = items[0];
				ListItemBase listItemBase2 = items[items.Count - 1];
				items.RemoveAt(items.Count - 1);
				items.Insert(0, listItemBase2);
				listItemBase2.transform.localPosition = listItemBase.transform.localPosition - vector;
				listItemBase2.renderItem(pageData[num]);
			}
		}
	}

	public void renderList(List<Dictionary<string, string>> listData = null, bool useOldItem = false)
	{
		if (!useOldItem || items == null)
		{
			clearList();
		}
		if (listData != null)
		{
			this.listData = listData;
		}
		renderPage(0, useOldItem);
	}

	public void showItemSelect(ListItemBase item)
	{
		if (itemSelect != null)
		{
			itemSelect.SetActive(item != null);
			if (item != null)
			{
				itemSelect.transform.localPosition = item.transform.localPosition;
			}
		}
	}

	public void showItemSelect(int index)
	{
		if (!(itemSelect != null))
		{
			return;
		}
		if (index < pageItemStartIndex || index > pageItemStartIndex + itemsPerPage)
		{
			itemSelect.SetActive(false);
			return;
		}
		index = listIndexToPageIndex(index);
		if (items == null || index >= items.Count)
		{
			itemSelect.SetActive(false);
			return;
		}
		itemSelect.SetActive(true);
		itemSelect.transform.localPosition = items[index].transform.localPosition;
	}

	public void goNextPage(GameObject go = null)
	{
		int itemStartIndex = 0;
		if (pageItemStartIndex + itemsPerPage < listData.Count)
		{
			itemStartIndex = pageItemStartIndex + itemsPerPage;
		}
		renderPage(itemStartIndex);
	}

	public void goPrevPage(GameObject go = null)
	{
		int num = 0;
		num = ((pageItemStartIndex - itemsPerPage >= 0) ? (pageItemStartIndex - itemsPerPage) : ((listData.Count % itemsPerPage <= 0) ? (listData.Count - itemsPerPage) : (listData.Count - listData.Count % itemsPerPage)));
		renderPage(num);
	}

	public int getPageIndex()
	{
		return Mathf.FloorToInt((float)pageItemStartIndex / (float)itemsPerPage);
	}

	public int getTotalPage()
	{
		return Mathf.CeilToInt((float)listData.Count / (float)itemsPerPage);
	}

	public int calcMaxVisibleItems()
	{
		maxVisibleItems = 0;
		if (panelContainer != null)
		{
			maxVisibleItems = Mathf.CeilToInt((panelContainer.clipRange.z + vSpace) / (itemHeight + vSpace));
		}
		return maxVisibleItems;
	}

	public void renderPage(int itemStartIndex = -1, bool useOldItem = false)
	{
		if (!useOldItem || items == null)
		{
			clearPage();
			items = new List<ListItemBase>();
		}
		if (listData == null)
		{
			return;
		}
		if (itemStartIndex >= 0)
		{
			pageItemStartIndex = itemStartIndex;
		}
		int num = 0;
		if (itemsPerPage > 0)
		{
			pageData = new List<Dictionary<string, string>>();
			if (itemsPerPage > listData.Count - pageItemStartIndex)
			{
				pageData.AddRange(listData.GetRange(pageItemStartIndex, listData.Count - pageItemStartIndex));
			}
			else
			{
				pageData.AddRange(listData.GetRange(pageItemStartIndex, itemsPerPage));
			}
			num = itemsPerPage;
		}
		else
		{
			pageData = listData;
			num = ((!isSwapList) ? listData.Count : calcMaxVisibleItems());
		}
		for (int i = 0; i < num; i++)
		{
			ListItemBase listItemBase = null;
			if (!useOldItem || items.Count <= i)
			{
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(itemPrefab);
				gameObject.transform.parent = container;
				listItemBase = gameObject.GetComponent<ListItemBase>();
				items.Add(listItemBase);
				listItemBase.parentList = this;
				listItemBase.pageItemIndex = i;
				listItemBase.itemIndex = i + pageItemStartIndex;
				listItemBase.name = "Item " + listItemBase.itemIndex;
				listItemBase.isCheckToggledOnClick = isItemCheckedOnClick;
			}
			else
			{
				listItemBase = items[i];
			}
			if (i < pageData.Count)
			{
				listItemBase.renderItem(pageData[i]);
			}
			else
			{
				listItemBase.renderItem(null);
			}
			if (overrideData != null && overrideData[i] != null)
			{
				listItemBase.renderItem(overrideData[i], null, true);
			}
			listItemBase.transform.localScale = Vector3.one;
			if (_itemDragKey != DDKEY.NONE)
			{
				listItemBase.dragKey = _itemDragKey;
			}
			if (_itemDropKey != DDKEY.NONE)
			{
				listItemBase.dropKey = _itemDropKey;
			}
			if (isCollapsibleList && collapsibleItems != null && collapsibleItems.ContainsKey(listItemBase.itemIndex))
			{
				listItemBase.collapsibleContent = collapsibleItems[listItemBase.itemIndex];
				listItemBase.collapsibleContent.SetActive(false);
			}
			if (i >= pageData.Count - 1)
			{
				break;
			}
		}
		layoutList();
		if (labelPage != null)
		{
			labelPage.text = getPageIndex() + 1 + "/" + getTotalPage();
		}
		showItemSelect(_selectedIndex);
	}

	public void layoutList()
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = 0;
		int num4 = 0;
		for (int i = 0; i < items.Count; i++)
		{
			if (colOffsets != null)
			{
				foreach (KeyValuePair<int, float> colOffset in colOffsets)
				{
					if (colOffset.Key <= i)
					{
						num += colOffset.Value;
					}
				}
			}
			if (rowOffsets != null)
			{
				foreach (KeyValuePair<int, float> rowOffset in rowOffsets)
				{
					if (rowOffset.Key <= i)
					{
						num2 += rowOffset.Value;
					}
				}
			}
			items[i].transform.localPosition = new Vector3(num, num2, 0f);
			switch (layout)
			{
			case LAYOUT.Vertical:
				if (num3 < maxCols - 1)
				{
					num3++;
					break;
				}
				num3 = 0;
				num4++;
				break;
			case LAYOUT.Horizontal:
				if (num4 < maxRows - 1)
				{
					num4++;
					break;
				}
				num4 = 0;
				num3++;
				break;
			}
			num = (itemWidth + hSpace) * (float)num3;
			num2 = (0f - (itemHeight + vSpace)) * (float)num4;
		}
	}

	public void clearPage()
	{
		if (items != null)
		{
			for (int i = 0; i < items.Count; i++)
			{
				UnityEngine.Object.Destroy(items[i].gameObject);
			}
			items = null;
			showItemSelect(null);
		}
	}

	public void clearList()
	{
		clearPage();
		selectedIndexes = new List<int>();
		_selectedIndex = -1;
	}

	public static List<Dictionary<string, string>> titlesToListData(params string[] titles)
	{
		List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
		for (int i = 0; i < titles.Length; i++)
		{
			if (titles[i] != null)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary.Add("title", titles[i]);
				list.Add(dictionary);
			}
			else
			{
				list.Add(null);
			}
		}
		return list;
	}

	public static List<Dictionary<string, string>> labelsToListData(params string[] labels)
	{
		List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
		for (int i = 0; i < labels.Length; i++)
		{
			if (labels[i] != null)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary.Add("label", labels[i]);
				list.Add(dictionary);
			}
			else
			{
				list.Add(null);
			}
		}
		return list;
	}

	public static List<Dictionary<string, string>> iconsToListData(params string[] icons)
	{
		List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
		for (int i = 0; i < icons.Length; i++)
		{
			if (icons[i] != null)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary.Add("icon", icons[i]);
				list.Add(dictionary);
			}
			else
			{
				list.Add(null);
			}
		}
		return list;
	}

	public static List<Dictionary<string, string>> pairsToListData(params string[] pairs)
	{
		string[] separator = new string[1] { "||" };
		char[] separator2 = new char[1] { '|' };
		List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
		for (int i = 0; i < pairs.Length; i++)
		{
			if (pairs[i] != null)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string[] array = pairs[i].Split(separator, StringSplitOptions.None);
				for (int j = 0; j < array.Length; j++)
				{
					string[] array2 = array[j].Split(separator2);
					dictionary.Add(array2[0], array2[1]);
				}
				list.Add(dictionary);
			}
			else
			{
				list.Add(null);
			}
		}
		return list;
	}

	public static List<Dictionary<string, string>> pairsStringToListData(string pairsString)
	{
		string[] pairs = pairsString.Split(new string[1] { "|||" }, StringSplitOptions.None);
		return pairsToListData(pairs);
	}

	public void resetScrollBars()
	{
		UIDraggablePanel component = container.GetComponent<UIDraggablePanel>();
		if (!(component == null))
		{
			if (component.verticalScrollBar != null)
			{
				component.verticalScrollBar.scrollValue = 0f;
			}
			if (component.horizontalScrollBar != null)
			{
				component.horizontalScrollBar.scrollValue = 0f;
			}
		}
	}
}

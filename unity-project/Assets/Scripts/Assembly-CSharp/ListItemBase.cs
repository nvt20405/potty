using System;
using System.Collections.Generic;
using UnityEngine;

public class ListItemBase : ButtonBase
{
	public float collapsibleContentWidth;

	public float collapsibleContentHeight;

	public GameObject collapsibleContent;

	public UILabel title;

	protected override void Awake()
	{
		base.Awake();
		renderItem = (RenderItem)Delegate.Combine(renderItem, new RenderItem(_renderItem));
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void onPartClick(int partIndex)
	{
		if (parentList != null && parentList.onListItemPartClick != null && (parentList.isItemClickableOnNull || itemData != null))
		{
			parentList.onListItemPartClick(itemIndex, partIndex);
		}
	}

	public void _renderItem(Dictionary<string, string> itemData, GameObject go = null, bool isOverride = false)
	{
		go = base.gameObject;
		if (!isOverride)
		{
			base.itemData = itemData;
			parentList.listData[itemIndex] = itemData;
			if (parentList.onListDataChange != null)
			{
				parentList.onListDataChange();
			}
		}
		if (itemData == null && !isOverride)
		{
			if (parentList.isItemHiddenOnNull)
			{
				base.gameObject.SetActive(false);
				return;
			}
			if (label != null)
			{
				label.gameObject.SetActive(false);
			}
			if (icon != null)
			{
				icon.gameObject.SetActive(false);
			}
			return;
		}
		if (title != null)
		{
			if (itemData.ContainsKey("title") && itemData["title"] != null)
			{
				title.gameObject.SetActive(true);
				title.text = itemData["title"];
			}
			else if (!isOverride)
			{
				title.gameObject.SetActive(false);
			}
		}
		if (label != null)
		{
			if (itemData.ContainsKey("label") && itemData["label"] != null)
			{
				label.gameObject.SetActive(true);
				label.text = itemData["label"];
			}
			else if (!isOverride)
			{
				label.gameObject.SetActive(false);
			}
		}
		if (background != null && itemData.ContainsKey("background"))
		{
			background.spriteName = itemData["background"];
		}
		if (icon != null)
		{
			if (itemData.ContainsKey("icon") && itemData["icon"] != null)
			{
				icon.gameObject.SetActive(true);
				icon.spriteName = itemData["icon"];
				if (parentList.isIconPixelPerfect)
				{
					icon.MakePixelPerfect();
				}
			}
			else if (!isOverride)
			{
				icon.gameObject.SetActive(false);
			}
		}
		if (itemData.ContainsKey("cch"))
		{
			collapsibleContentHeight = float.Parse(itemData["cch"]);
		}
		if (itemData.ContainsKey("ccw"))
		{
			collapsibleContentWidth = float.Parse(itemData["ccw"]);
		}
		if (itemData.ContainsKey("isSelected"))
		{
			base.isSelected = bool.Parse(itemData["isSelected"]);
		}
	}

	protected override void UpdateCursor()
	{
		base.UpdateCursor();
		if (DragDropManager.draggedItemData != null && DragDropManager.draggedItemData.ContainsKey("icon") && !string.IsNullOrEmpty(DragDropManager.draggedItemData["icon"]))
		{
			UICursor.Set(DragDropManager.instance.iconAtlas, DragDropManager.draggedItemData["icon"]);
		}
	}

	public override void showSelect()
	{
		if (parentList.isSelectHandledOnParent)
		{
			if (_isSelected)
			{
				parentList.showItemSelect(this);
			}
		}
		else
		{
			base.showSelect();
		}
	}
}

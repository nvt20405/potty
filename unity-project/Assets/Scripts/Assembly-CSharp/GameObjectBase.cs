using System.Collections.Generic;
using UnityEngine;

public class GameObjectBase : MonoBehaviour
{
	public delegate void RenderItem(Dictionary<string, string> itemData, GameObject go = null, bool isOverride = false);

	public delegate void _OnEnable();

	public bool isInitialized;

	public Dictionary<string, string> itemData;

	public DDKEY dragKey;

	public DDKEY dropKey;

	public int itemIndex = -1;

	public int pageItemIndex = -1;

	public ListBase parentList;

	public bool _isSelected;

	protected bool _isHovered;

	public RenderItem renderItem;

	public _OnEnable onEnable;

	public bool isSelected
	{
		get
		{
			return _isSelected;
		}
		set
		{
			_isSelected = value;
			showSelect();
		}
	}

	public bool isHovered
	{
		get
		{
			return _isHovered;
		}
		set
		{
			_isHovered = value;
			showHover();
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void triggerDrop()
	{
		if (DragDropManager.isDragDropEnabled && DragDropManager.draggedItemData != null && dropKey != DDKEY.NONE && DragDropManager.dragKey == dropKey)
		{
			onDrop();
			DragDropManager.clearDrag();
		}
	}

	protected virtual void onDrop()
	{
		if (DragDropManager.draggedItemData == null)
		{
			return;
		}
		if (parentList != null && parentList.onListItemDrop != null)
		{
			parentList.onListItemDrop(this);
		}
		if (DragDropManager.mDraggedItem != null)
		{
			DragDropManager.mDraggedItem.renderItem(itemData);
		}
		else if (DragDropManager.draggedItemIndex >= 0 && DragDropManager.parentList != null)
		{
			DragDropManager.parentList.listData[DragDropManager.draggedItemIndex] = itemData;
			if (DragDropManager.draggedItemIndex >= DragDropManager.parentList.pageItemStartIndex && DragDropManager.draggedItemIndex < DragDropManager.parentList.pageItemStartIndex + DragDropManager.parentList.itemsPerPage)
			{
				DragDropManager.parentList.items[DragDropManager.parentList.listIndexToPageIndex(DragDropManager.draggedItemIndex)].renderItem(itemData);
			}
		}
		renderItem(DragDropManager.draggedItemData);
		if (parentList != null)
		{
			parentList.selectedIndex = itemIndex;
		}
	}

	protected virtual void UpdateCursor()
	{
	}

	protected virtual void OnDrag()
	{
		if (dragKey != DDKEY.NONE && itemData != null && DragDropManager.isDragDropEnabled)
		{
			TouchIconManager.instance.icons[0].gameObject.SetActive(true);
			DragDropManager.mDraggedItem = this;
			DragDropManager.draggedItemIndex = itemIndex;
			DragDropManager.dragKey = dragKey;
			DragDropManager.draggedItemData = itemData;
			DragDropManager.parentList = parentList;
			if (parentList != null && parentList.onListItemDrag != null)
			{
				parentList.onListItemDrag(this);
			}
			if (renderItem != null)
			{
				renderItem(null);
			}
			UpdateCursor();
		}
	}

	public virtual void showSelect()
	{
	}

	public virtual void showHover()
	{
	}

	protected virtual void OnEnable()
	{
		if (!isInitialized)
		{
			initialize();
		}
		if (onEnable != null)
		{
			onEnable();
		}
	}

	protected virtual void OnDisable()
	{
	}

	public virtual void initialize()
	{
		isInitialized = true;
	}

	public static GameObjectBase Get(GameObject go)
	{
		GameObjectBase gameObjectBase = go.GetComponent<GameObjectBase>();
		if (gameObjectBase == null)
		{
			gameObjectBase = go.AddComponent<GameObjectBase>();
		}
		return gameObjectBase;
	}
}

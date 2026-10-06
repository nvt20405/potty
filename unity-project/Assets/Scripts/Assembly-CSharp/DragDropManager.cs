using System.Collections.Generic;
using UnityEngine;

public class DragDropManager : MonoBehaviour
{
	public delegate void OnDragOver(GameObjectBase gob);

	public static DragDropManager instance;

	public static bool isDragDropEnabled = true;

	public UIAtlas iconAtlas;

	public bool clearOnNextFrame;

	public static ListBase parentList;

	public static DDKEY dragKey;

	public static int draggedItemIndex = -1;

	public static GameObjectBase mDraggedItem;

	public static Dictionary<string, string> draggedItemData;

	public static GameObjectBase hoveredObject;

	public OnDragOver onDragOver;

	private void Awake()
	{
		if (instance != null)
		{
			Object.Destroy(base.gameObject);
		}
		instance = this;
		isDragDropEnabled = true;
		Input.multiTouchEnabled = false;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (clearOnNextFrame)
		{
			if (draggedItemData != null)
			{
				if (mDraggedItem != null)
				{
					mDraggedItem.renderItem(draggedItemData);
				}
				else if (draggedItemIndex >= 0 && parentList != null)
				{
					parentList.listData[draggedItemIndex] = draggedItemData;
					parentList.renderPage();
				}
			}
			clearDrag();
		}
		if (draggedItemData != null)
		{
			GameObjectBase gameObjectBase = null;
			if (UICamera.hoveredObject != null)
			{
				gameObjectBase = UICamera.hoveredObject.GetComponent<GameObjectBase>();
			}
			if (gameObjectBase != null)
			{
				if (hoveredObject != gameObjectBase)
				{
					if (hoveredObject != null)
					{
						hoveredObject.isHovered = false;
						hoveredObject = null;
					}
					hoveredObject = gameObjectBase;
					hoveredObject.isHovered = true;
					onDragOver(hoveredObject);
				}
			}
			else if (hoveredObject != null)
			{
				hoveredObject.isHovered = false;
				hoveredObject = null;
			}
		}
		if (Input.GetMouseButtonUp(0) && draggedItemData != null)
		{
			if (hoveredObject != null)
			{
				hoveredObject.triggerDrop();
			}
			clearOnNextFrame = true;
		}
	}

	public static void clearDrag()
	{
		UICursor.Clear();
		draggedItemData = null;
		mDraggedItem = null;
		dragKey = DDKEY.NONE;
		draggedItemIndex = -1;
		parentList = null;
		instance.clearOnNextFrame = false;
		if (hoveredObject != null)
		{
			hoveredObject.isHovered = false;
			hoveredObject = null;
		}
	}
}

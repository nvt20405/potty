using System;
using UnityEngine;

public class ButtonBase : GameObjectBase
{
	public UISprite background;

	public UISprite icon;

	public UILabel label;

	public GameObject select;

	public GameObject check;

	public bool isCheckToggledOnClick;

	public bool _isChecked;

	public bool isChecked
	{
		get
		{
			return _isChecked;
		}
		set
		{
			_isChecked = value;
			if (check != null)
			{
				check.SetActive(_isChecked);
			}
		}
	}

	protected virtual void Awake()
	{
		UIEventListener uIEventListener = UIEventListener.Get(base.gameObject);
		uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
		UIEventListener uIEventListener2 = UIEventListener.Get(base.gameObject);
		uIEventListener2.onPress = (UIEventListener.BoolDelegate)Delegate.Combine(uIEventListener2.onPress, new UIEventListener.BoolDelegate(onPress));
		if (select != null)
		{
			select.SetActive(_isSelected);
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public virtual void onClick(GameObject go)
	{
		if (parentList != null && parentList.isChangeFocusOnMouseUp && (parentList.isItemClickableOnNull || itemData != null))
		{
			if (parentList.isSelectItemOnClick)
			{
				parentList.selectedIndex = itemIndex;
			}
			if (parentList.onListItemClick != null)
			{
				parentList.onListItemClick();
			}
		}
		if (isCheckToggledOnClick)
		{
			isChecked = !isChecked;
		}
	}

	public virtual void onPress(GameObject go, bool isPressed)
	{
		if (((parentList != null) & isPressed) && !parentList.isChangeFocusOnMouseUp)
		{
			parentList.selectedIndex = itemIndex;
			if (parentList.onListItemClick != null)
			{
				parentList.onListItemClick();
			}
		}
	}

	public override void showHover()
	{
		base.showHover();
		if (background != null)
		{
			background.color = ((!_isHovered) ? Color.white : GUIManager.COLOR_LIGHT_BLUE);
		}
	}

	public override void showSelect()
	{
		base.showSelect();
		if (select != null)
		{
			select.SetActive(_isSelected);
		}
	}
}

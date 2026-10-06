using System;
using UnityEngine;

public class FormBase : GameObjectBase
{
	public ButtonBase btnCancel;

	protected virtual void Awake()
	{
		if (btnCancel != null)
		{
			UIEventListener uIEventListener = UIEventListener.Get(btnCancel.gameObject);
			uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick_btnCancel));
		}
	}

	public virtual void onClick_btnCancel(GameObject go)
	{
		base.gameObject.SetActive(false);
	}

	protected override void OnEnable()
	{
		if (GUIManager.instance != null)
		{
			GUIManager.instance.addForm(this);
		}
		base.OnEnable();
	}

	protected override void OnDisable()
	{
		if (GUIManager.instance != null)
		{
			GUIManager.instance.removeForm(this);
		}
		base.OnDisable();
	}

	public virtual bool onESC()
	{
		onClick_btnCancel(null);
		return true;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}

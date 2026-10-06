using System;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMessageBase : FormBase
{
	public delegate void Callback(bool response);

	public static PopUpMessageBase instance;

	public static PopUpMessageBase currentPopUp;

	public ButtonBase btnOK;

	public UILabel labelLabel;

	public UILabel labelDesc;

	public ListBase list;

	public Callback callback;

	public bool isSingleInstance = true;

	public static void init()
	{
		if (!(instance != null))
		{
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("GUI/Controls/PopUpMessageBase"));
			gameObject.transform.parent = GUIManager.instance.popUpContainer.transform;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localPosition = Vector3.zero;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (isSingleInstance)
		{
			instance = this;
		}
		if (btnOK != null)
		{
			UIEventListener uIEventListener = UIEventListener.Get(btnOK.gameObject);
			uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(onOK));
		}
		hide();
	}

	private void Start()
	{
	}

	private void onOK(GameObject go)
	{
		if (callback != null)
		{
			callback(true);
		}
		hide();
	}

	public override void onClick_btnCancel(GameObject go)
	{
		if (NGUITools.GetActive(base.gameObject))
		{
			if (callback != null)
			{
				callback(false);
			}
			hide();
		}
	}

	private void Update()
	{
	}

	public void show(string label = null, string desc = null, string ok = null, string cancel = null, List<Dictionary<string, string>> listData = null)
	{
		currentPopUp = this;
		base.gameObject.SetActive(true);
		if (label != null && labelLabel != null)
		{
			labelLabel.text = label;
		}
		if (desc != null && labelDesc != null)
		{
			labelDesc.text = desc;
		}
		if (ok != null && btnOK != null && btnOK.label != null)
		{
			btnOK.gameObject.SetActive(true);
			btnOK.label.text = ok;
		}
		else if (ok == null && btnOK != null)
		{
			btnOK.gameObject.SetActive(false);
		}
		if (cancel != null && btnCancel != null && btnCancel.label != null)
		{
			btnCancel.gameObject.SetActive(true);
			btnCancel.label.text = cancel;
		}
		else if (cancel == null && btnCancel != null)
		{
			btnCancel.gameObject.SetActive(false);
		}
		if (listData != null && list != null)
		{
			list.gameObject.SetActive(true);
			list.renderList(listData);
			list.resetScrollBars();
		}
		else if (listData == null && list != null)
		{
			list.gameObject.SetActive(false);
		}
	}

	public void hide()
	{
		if (currentPopUp == this)
		{
			currentPopUp = null;
		}
		base.gameObject.SetActive(false);
	}

	public static void showPopUp(string label = null, string desc = null, string ok = null, string cancel = null, List<Dictionary<string, string>> listData = null)
	{
		if (instance == null)
		{
			init();
		}
		instance.show(label, desc, ok, cancel, listData);
	}

	public static void hidePopUp()
	{
		if (instance != null)
		{
			instance.hide();
		}
	}
}

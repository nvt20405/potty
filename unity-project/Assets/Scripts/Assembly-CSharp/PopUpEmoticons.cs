using System.Collections.Generic;
using UnityEngine;

public class PopUpEmoticons : MonoBehaviour
{
	public static PopUpEmoticons instance;

	public GameObject ItemRoot;

	public GameObject emoPerfab;

	private int colCount = 8;

	private int rowCount;

	private float hSpace = 5f;

	public List<string> listData = new List<string>();

	public List<EmoticonItem> ItemList = new List<EmoticonItem>();

	public UIFont font;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void CreateByChat()
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupEmoticons"))).GetComponent<PopUpEmoticons>();
		PopupManager.instance.Add(instance.gameObject, new Vector3(0f, 380f, 0f));
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.displayListEmo();
	}

	public static void CreateByMail()
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupEmoticons"))).GetComponent<PopUpEmoticons>();
		PopupManager.instance.Add(instance.gameObject, new Vector3(0f, 380f, 0f));
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.displayListEmo();
	}

	public void displayListEmo()
	{
		dragPanel = ItemRoot.GetComponent<UIDraggablePanel>();
		panel = ItemRoot.GetComponent<UIPanel>();
		listData.Clear();
		foreach (BMSymbol symbol in font.symbols)
		{
			listData.Add(symbol.sequence);
		}
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
		int num = 0;
		if (listData.Count > 0)
		{
			for (int i = 0; i < listData.Count; i++)
			{
				EmoticonItem component = ((GameObject)Object.Instantiate(emoPerfab)).GetComponent<EmoticonItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				float x = (float)(i % colCount) * (component.wItem + hSpace) - panel.clipRange.z / 2f + 25f;
				float num2 = Mathf.Floor(i / colCount) * (component.hItem + hSpace) + panel.clipRange.w / 2f;
				component.transform.localPosition = new Vector3(x, 0f - num2, -3f);
				component.setString(listData[i]);
				UIEventListener.Get(component.gameObject).onClick = emoticons_OnClick;
				ItemList.Add(component);
			}
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}

	public void emoticons_OnClick(GameObject go)
	{
		EmoticonItem component = go.transform.GetComponent<EmoticonItem>();
		if (component.strData.Length > 0)
		{
			if (PopupChat.instance != null)
			{
				PopupChat.instance.inputMess.label.text += component.strData;
			}
			else if (PopUpSendMail.instance != null)
			{
				PopUpSendMail.instance.inputMess.label.text += component.strData;
			}
		}
	}
}

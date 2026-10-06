using System.Collections.Generic;
using UnityEngine;

public class ScreenTopEvent : ScreenBase
{
	public enum ScreenTopTab
	{
		TabTopLevel = 0,
		TabTopLuanKiem = 1
	}

	private const int maxItemCount = 12;

	public GameObject TopLevelPrefab;

	public GameObject TopLuanKiemPrefab;

	public GameObject ItemRoot;

	public UILabel lbTitle;

	public UILabel lbDescription;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<TopLevelItem> LevelList = new List<TopLevelItem>();

	private List<TopLuanKiemItem> LuanKiemItemList = new List<TopLuanKiemItem>();

	private Vector3 itemPos;

	private Vector3 itemOffset = new Vector3(0f, -130f, 0f);

	private int startItemGUI_Idx;

	public ScreenTopTab m_Tab;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
	}

	private void Start()
	{
		dragPanel = ItemRoot.GetComponent<UIDraggablePanel>();
		panel = ItemRoot.GetComponent<UIPanel>();
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		if (m_Tab != ScreenTopTab.TabTopLevel && m_Tab == ScreenTopTab.TabTopLuanKiem)
		{
		}
	}

	public void clearList()
	{
		startItemGUI_Idx = 0;
		itemPos = new Vector3(0f, 320f, 0f);
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		LevelList.Clear();
		LuanKiemItemList.Clear();
	}

	public void displayTopLevel(DuaTopLevelResponse response)
	{
		clearList();
		lbDescription.text = response.message;
		lbTitle.text = Localization.instance.Get("TopLevelEventTitle");
		if (response.ListTop == null)
		{
			return;
		}
		itemPos = new Vector3(0f, 320f, 0f);
		for (int i = 0; i < response.ListTop.Count; i++)
		{
			DuaTopLevelResponse.TopGamer topGamer;
			if (i < response.ListTop.Count && response.ListTop[i] != null)
			{
				topGamer = response.ListTop[i];
			}
			else
			{
				topGamer = new DuaTopLevelResponse.TopGamer();
				topGamer.DisplayName = string.Empty;
				topGamer.Level = 0;
				topGamer.Exp = 0L;
			}
			TopLevelItem component = ((GameObject)Object.Instantiate(TopLevelPrefab)).GetComponent<TopLevelItem>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = itemPos;
			component.setData(topGamer, i);
			itemPos += itemOffset;
			LevelList.Add(component);
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}

	public void displayTopLuanKiem(DuaTopLuanKiemResponse response)
	{
		clearList();
		lbTitle.text = Localization.instance.Get("TopLuanKiemEventTitle");
		lbDescription.text = response.message;
		if (response.ListTop == null)
		{
			return;
		}
		itemPos = new Vector3(0f, 320f, 0f);
		for (int i = 0; i < 10; i++)
		{
			DuaTopLuanKiemResponse.TopGamer topGamer;
			if (i < response.ListTop.Count && response.ListTop[i] != null)
			{
				topGamer = response.ListTop[i];
			}
			else
			{
				topGamer = new DuaTopLuanKiemResponse.TopGamer();
				topGamer.DisplayName = string.Empty;
			}
			TopLuanKiemItem component = ((GameObject)Object.Instantiate(TopLuanKiemPrefab)).GetComponent<TopLuanKiemItem>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = itemPos;
			itemPos += itemOffset;
			component.setData(topGamer, i);
			LuanKiemItemList.Add(component);
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		component2.ResetPosition();
	}
}

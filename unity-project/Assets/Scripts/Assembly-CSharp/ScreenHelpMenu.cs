using UnityEngine;

public class ScreenHelpMenu : ScreenBase
{
	public GameObject ItemRoot;

	public GameObject MainMenuPrefab;

	public GameObject SubMenuPrefab;

	private Vector3 itemPos;

	private Vector3 itemOffset;

	public int mCurrentMainMenuIndex;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
		mCurrentMainMenuIndex = 0;
	}

	private void Start()
	{
	}

	public override void OnActive()
	{
		base.OnActive();
		getListSubMenu(mCurrentMainMenuIndex);
	}

	public void onClick_MainMenu(GameObject go)
	{
		MainMenuItem component = go.gameObject.GetComponent<MainMenuItem>();
		if (component != null)
		{
			mCurrentMainMenuIndex = component.MainMenuID;
			getListSubMenu(component.MainMenuID);
		}
	}

	public void getListSubMenu(int mainMenuID)
	{
		if (mainMenuID >= ConfigManager.instance.OtherConfig.HelpMenu.Count)
		{
			return;
		}
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		itemPos = new Vector3(0f, 295f, 0f);
		itemOffset = new Vector3(0f, -60f, 0f);
		if (ConfigManager.instance.OtherConfig.HelpMenu == null || ConfigManager.instance.OtherConfig.HelpMenu.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < ConfigManager.instance.OtherConfig.HelpMenu.Count; i++)
		{
			MainMenuItem component = ((GameObject)Object.Instantiate(MainMenuPrefab)).GetComponent<MainMenuItem>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = itemPos;
			itemPos += itemOffset;
			component.setTitle(ConfigManager.instance.OtherConfig.HelpMenu[i].Name);
			component.MainMenuID = i;
			UIEventListener.Get(component.gameObject).onClick = onClick_MainMenu;
			if (ConfigManager.instance.OtherConfig.HelpMenu[i].Children != null && ConfigManager.instance.OtherConfig.HelpMenu[i].Children.Count > 0 && i == mainMenuID)
			{
				for (int j = 0; j < ConfigManager.instance.OtherConfig.HelpMenu[i].Children.Count; j++)
				{
					MainMenuItem component2 = ((GameObject)Object.Instantiate(SubMenuPrefab)).GetComponent<MainMenuItem>();
					component2.transform.parent = ItemRoot.transform;
					component2.transform.localScale = new Vector3(1f, 1f, 1f);
					component2.transform.localPosition = itemPos;
					itemPos += itemOffset;
					component2.setTitle(ConfigManager.instance.OtherConfig.HelpMenu[i].Children[j].Name);
					component2.strURL = ConfigManager.instance.OtherConfig.HelpMenu[i].Children[j].URL;
					UIEventListener.Get(component2.gameObject).onClick = onClick_SubMenuItem;
				}
			}
		}
	}

	public void onClick_SubMenuItem(GameObject go)
	{
		MainMenuItem component = go.gameObject.GetComponent<MainMenuItem>();
		if (component != null)
		{
			ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
			screenHelpInfo.setData(component.strURL, component.strName);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
		}
	}

	public void btnBack_onClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenSettings);
	}
}

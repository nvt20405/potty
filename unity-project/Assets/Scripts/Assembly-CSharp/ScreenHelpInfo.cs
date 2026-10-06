using System.Collections;
using UnityEngine;

public class ScreenHelpInfo : ScreenBase
{
	public NGUIHTML htmlObj;

	private string strURL;

	private string strName;

	public UILabel lbTitle;

	public UIDraggablePanel webView;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public override void OnActive()
	{
		base.OnActive();
		StartCoroutine(DangTaiHelpContent(strURL));
		lbTitle.text = strName;
	}

	public void setData(string strURLItem, string strTitleItem)
	{
		EGDebug.Log("STRING URL: " + strURL);
		strURL = strURLItem;
		strName = strTitleItem;
	}

	public void setByLevel(int mainID, int subID)
	{
		if (mainID >= 0 && ConfigManager.instance.OtherConfig.HelpMenu != null && ConfigManager.instance.OtherConfig.HelpMenu[mainID] != null && ConfigManager.instance.OtherConfig.HelpMenu[mainID].Children != null && ConfigManager.instance.OtherConfig.HelpMenu[mainID].Children[subID] != null)
		{
			strURL = ConfigManager.instance.OtherConfig.HelpMenu[mainID].Children[subID].URL;
			strName = ConfigManager.instance.OtherConfig.HelpMenu[mainID].Children[subID].Name;
			EGDebug.Log("string url: " + strURL);
		}
	}

	private IEnumerator DangTaiHelpContent(string url)
	{
		PopupNetworkLoading.Create(Localization.instance.Get("NetworkLoading"));
		WWW www = new WWW(url);
		yield return www;
		htmlObj.html = www.text;
		webView.ResetPosition();
		PopupNetworkLoading.DestroyPopup();
	}

	public void btnCancel_OnClick()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenMain);
	}

	public void btnMenu_OnClick()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpMenu);
	}

	public void btnBack_OnClick()
	{
		GUIManager.instance.SetScreen(GUIManager.instance.LastScreen);
	}
}

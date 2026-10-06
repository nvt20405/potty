using System.Collections.Generic;
using UnityEngine;

public class ScreenSearchBanBe : ScreenBase
{
	public GameObject friendPerfab;

	public GameObject ItemRoot;

	public UIInput inputSearch;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<SearchBanBeItem> BanBeItemList = new List<SearchBanBeItem>();

	private int startItemGUI_Idx;

	private Vector3 itemPos;

	private Vector3 itemOffset;

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
		clearListBanBe();
		inputSearch.text = string.Empty;
	}

	public void OnSearchTheoLevel()
	{
		int result = 0;
		int.TryParse(inputSearch.text, out result);
		if (result > 0)
		{
			SearchBanBeRequest searchBanBeRequest = new SearchBanBeRequest();
			searchBanBeRequest.Level = result;
			searchBanBeRequest.Name = string.Empty;
			GameManager.instance.m_GameClient.RequestSearchBanBe(searchBanBeRequest);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ErrorSearchTheoLevelMess"));
			inputSearch.text = string.Empty;
		}
	}

	public void OnSearchTheoName()
	{
		if (inputSearch.text != null && inputSearch.text.Length > 0)
		{
			SearchBanBeRequest searchBanBeRequest = new SearchBanBeRequest();
			searchBanBeRequest.Level = 0;
			searchBanBeRequest.Name = inputSearch.text;
			GameManager.instance.m_GameClient.RequestSearchBanBe(searchBanBeRequest);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ErrorSearchTheoNameMess"));
		}
	}

	private void clearListBanBe()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		BanBeItemList.Clear();
	}

	public void displayListGamer(List<UserInfo.BanBeData> listData)
	{
		clearListBanBe();
		startItemGUI_Idx = 0;
		itemPos = new Vector3(0f, -680f, 0f);
		itemOffset = new Vector3(0f, -85f, 0f);
		if (listData == null || listData.Count > 0)
		{
			for (int i = startItemGUI_Idx; i < listData.Count; i++)
			{
				UserInfo.BanBeData friendData = listData[i];
				SearchBanBeItem component = ((GameObject)Object.Instantiate(friendPerfab)).GetComponent<SearchBanBeItem>();
				component.transform.parent = ItemRoot.transform;
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				itemPos += itemOffset;
				component.SetFriendData(friendData);
				UIEventListener.Get(component.btnKetBan.gameObject).onClick = ketBanBtn_OnClick;
				BanBeItemList.Add(component);
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}

	public void ketBanBtn_OnClick(GameObject go)
	{
		SearchBanBeItem component = go.transform.parent.GetComponent<SearchBanBeItem>();
		if (component != null && component.mFriend_Data != null)
		{
			OpBanBeRequest opBanBeRequest = new OpBanBeRequest();
			opBanBeRequest.FriendGID = component.mFriend_Data.GID;
			GameManager.instance.m_GameClient.RequestAddBanBe(opBanBeRequest);
		}
	}

	public void btnBack_OnClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenBanBe);
	}
}

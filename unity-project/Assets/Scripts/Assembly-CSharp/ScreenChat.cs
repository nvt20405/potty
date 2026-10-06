using System.Collections.Generic;
using UnityEngine;

public class ScreenChat : ScreenBase
{
	private const float minScrollValue = 250f;

	public GameObject chatItemPerfab;

	public GameObject ItemRoot;

	public GameObject LienMinhRoot;

	public GameObject LienSrvRoot;

	public GameObject LienMinhTab;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private UIPanel panelLienMinh;

	private UIPanel panelLienSrv;

	private List<GamerChatItem> ChatItemList = new List<GamerChatItem>();

	private List<GamerChatItem> ChatLienMinhList = new List<GamerChatItem>();

	private Vector3 itemPos;

	private Vector3 itemOffset;

	private Vector3 itemPosLienMinh;

	private Vector3 itemOffsetLienMinh;

	private Vector3 itemPosLienSrv;

	private Vector3 itemOffsetLienSrv;

	private int maxItemCount = 50;

	private float LastTimeUpdateLienSrv;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>(true);
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
		panelLienMinh = LienMinhRoot.GetComponent<UIPanel>();
		panelLienSrv = LienSrvRoot.GetComponent<UIPanel>();
	}

	private void Update()
	{
		if (LienSrvRoot.activeInHierarchy)
		{
			UIDraggablePanel component = LienSrvRoot.GetComponent<UIDraggablePanel>();
			float time = Time.time;
			if (time - LastTimeUpdateLienSrv > 10f && Utils.CalculateVerticalScrollValueInPixel(component) < 100f)
			{
				LastTimeUpdateLienSrv = time;
				GameManager.instance.m_GameClient.RequestGetChatLienSrv();
				EGDebug.Log("request update chat lien srv");
			}
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh == null)
		{
			LienMinhTab.SetActive(false);
		}
		else
		{
			LienMinhTab.SetActive(true);
		}
		getListChatItem();
	}

	public void getListChatItem()
	{
		ChatInfoRequest chatInfoRequest = new ChatInfoRequest();
		chatInfoRequest.ID = 0;
		GameManager.instance.m_GameClient.RequestChatInfo(chatInfoRequest);
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh != null)
		{
			ChatLienMinhInfoRequest chatLienMinhInfoRequest = new ChatLienMinhInfoRequest();
			chatLienMinhInfoRequest.lienminhID = GameManager.instance.m_GameClient.UserInfo.LienMinh.ID;
			GameManager.instance.m_GameClient.RequestChatLienMinhInfo(chatLienMinhInfoRequest);
		}
	}

	public void displayLienSrvListChatItem(ChatInfo chatInfo)
	{
		if (!LienSrvRoot.activeInHierarchy)
		{
			return;
		}
		UIDraggablePanel component = LienSrvRoot.GetComponent<UIDraggablePanel>();
		foreach (Transform item in LienSrvRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		itemPosLienSrv = new Vector3(0f, -680f, 0f);
		itemOffsetLienSrv = new Vector3(0f, -120f, 0f);
		if (chatInfo.listChat != null && chatInfo.listChat.Count > 0)
		{
			for (int i = 0; i < chatInfo.listChat.Count; i++)
			{
				ChatItem chatData = chatInfo.listChat[i];
				GamerChatItem component2 = ((GameObject)Object.Instantiate(chatItemPerfab)).GetComponent<GamerChatItem>();
				component2.transform.parent = LienSrvRoot.transform;
				component2.SetChatData(chatData);
				component2.transform.localScale = new Vector3(1f, 1f, 1f);
				component2.transform.localPosition = itemPosLienSrv;
				itemOffsetLienSrv = new Vector3(0f, 0f - (component2.spBackground.transform.localScale.y + 30f), 0f);
				itemPosLienSrv += itemOffsetLienSrv;
			}
			component.ResetPosition();
		}
	}

	public void displayListChatItem(ChatInfo chatInfo)
	{
		bool activeSelf = ItemRoot.activeSelf;
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		ChatItemList.Clear();
		itemPos = new Vector3(0f, -680f, 0f);
		itemOffset = new Vector3(0f, -120f, 0f);
		if (chatInfo.listChat != null && chatInfo.listChat.Count > 0)
		{
			for (int i = 0; i < chatInfo.listChat.Count; i++)
			{
				ChatItem chatData = chatInfo.listChat[i];
				GamerChatItem component = ((GameObject)Object.Instantiate(chatItemPerfab)).GetComponent<GamerChatItem>();
				component.transform.parent = ItemRoot.transform;
				component.SetChatData(chatData);
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPos;
				itemOffset = new Vector3(0f, 0f - (component.spBackground.transform.localScale.y + 30f), 0f);
				itemPos += itemOffset;
				ChatItemList.Add(component);
			}
			UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
			Debug.Log("ITEM POS: " + itemPos.y);
		}
	}

	public void displayListChatLienMinhItem(ChatInfo chatInfo)
	{
		bool activeSelf = LienMinhRoot.activeSelf;
		foreach (Transform item in LienMinhRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		ChatLienMinhList.Clear();
		itemPosLienMinh = new Vector3(0f, -680f, 0f);
		itemOffset = new Vector3(0f, -120f, 0f);
		if (chatInfo.listChat != null && chatInfo.listChat.Count > 0)
		{
			for (int i = 0; i < chatInfo.listChat.Count; i++)
			{
				ChatItem chatData = chatInfo.listChat[i];
				GamerChatItem component = ((GameObject)Object.Instantiate(chatItemPerfab)).GetComponent<GamerChatItem>();
				component.transform.parent = LienMinhRoot.transform;
				component.SetChatData(chatData);
				component.transform.localScale = new Vector3(1f, 1f, 1f);
				component.transform.localPosition = itemPosLienMinh;
				itemOffset = new Vector3(0f, 0f - (component.spBackground.transform.localScale.y + 30f), 0f);
				itemPosLienMinh += itemOffset;
				ChatLienMinhList.Add(component);
			}
			UIDraggablePanel component2 = LienMinhRoot.GetComponent<UIDraggablePanel>();
			component2.ResetPosition();
		}
	}

	public void addItemToChatLienMinhList(ChatItem newItem)
	{
		if (ChatLienMinhList.Count >= maxItemCount)
		{
			GamerChatItem gamerChatItem = ChatLienMinhList[0];
			GamerChatItem gamerChatItem2 = ChatLienMinhList[ChatLienMinhList.Count - 1];
			ChatLienMinhList.RemoveAt(0);
			ChatLienMinhList.Add(gamerChatItem);
			itemOffsetLienMinh = new Vector3(0f, 0f - (gamerChatItem2.spBackground.transform.localScale.y + 30f), 0f);
			itemPosLienMinh = gamerChatItem2.transform.localPosition + itemOffsetLienMinh;
			gamerChatItem.transform.localPosition = itemPosLienMinh;
			gamerChatItem.SetChatData(newItem);
		}
		else
		{
			GamerChatItem component = ((GameObject)Object.Instantiate(chatItemPerfab)).GetComponent<GamerChatItem>();
			component.transform.parent = LienMinhRoot.transform;
			component.SetChatData(newItem);
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = itemPosLienMinh;
			itemOffsetLienMinh = new Vector3(0f, 0f - (component.spBackground.transform.localScale.y + 30f), 0f);
			itemPosLienMinh += itemOffsetLienMinh;
			ChatLienMinhList.Add(component);
		}
		UIDraggablePanel component2 = LienMinhRoot.GetComponent<UIDraggablePanel>();
		if (Utils.CalculateVerticalScrollValueInPixel(component2) < 250f)
		{
			component2.ResetPosition();
		}
	}

	public void addItemToChatList(ChatItem newItem)
	{
		if (ChatItemList.Count >= maxItemCount)
		{
			GamerChatItem gamerChatItem = ChatItemList[0];
			GamerChatItem gamerChatItem2 = ChatItemList[ChatItemList.Count - 1];
			ChatItemList.RemoveAt(0);
			ChatItemList.Add(gamerChatItem);
			itemOffset = new Vector3(0f, 0f - (gamerChatItem2.spBackground.transform.localScale.y + 30f), 0f);
			itemPos = gamerChatItem2.transform.localPosition + itemOffset;
			gamerChatItem.transform.localPosition = itemPos;
			gamerChatItem.SetChatData(newItem);
		}
		else
		{
			GamerChatItem component = ((GameObject)Object.Instantiate(chatItemPerfab)).GetComponent<GamerChatItem>();
			component.transform.parent = ItemRoot.transform;
			component.SetChatData(newItem);
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = itemPos;
			itemOffset = new Vector3(0f, 0f - (component.spBackground.transform.localScale.y + 30f), 0f);
			itemPos += itemOffset;
			ChatItemList.Add(component);
		}
		UIDraggablePanel component2 = ItemRoot.GetComponent<UIDraggablePanel>();
		if (Utils.CalculateVerticalScrollValueInPixel(component2) < 250f)
		{
			component2.ResetPosition();
		}
	}

	private void OnActivateLienSrv(bool isActive)
	{
		LienSrvRoot.SetActive(isActive);
		if (isActive)
		{
			LienMinhRoot.SetActive(false);
			LienSrvRoot.SetActive(true);
			ItemRoot.SetActive(false);
			UIDraggablePanel component = LienSrvRoot.GetComponent<UIDraggablePanel>();
			component.ResetPosition();
		}
	}

	private void OnActivateBangHoi(bool isActive)
	{
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh != null)
		{
			LienMinhRoot.SetActive(isActive);
		}
		if (isActive)
		{
			LienMinhRoot.SetActive(true);
			LienSrvRoot.SetActive(false);
			ItemRoot.SetActive(false);
			UIDraggablePanel component = LienMinhRoot.GetComponent<UIDraggablePanel>();
			component.ResetPosition();
		}
		else if (isActive)
		{
			MessagePopup.Create(Localization.instance.Get("ChuaGiaNhapLienMinh"));
		}
		if (isActive)
		{
			EGDebug.Log("OnActivateBangHoi");
		}
	}

	private void OnActivateTheGioi(bool isActive)
	{
		ItemRoot.SetActive(isActive);
		if (isActive)
		{
			LienMinhRoot.SetActive(false);
			LienSrvRoot.SetActive(false);
			ItemRoot.SetActive(true);
			UIDraggablePanel component = ItemRoot.GetComponent<UIDraggablePanel>();
			component.ResetPosition();
		}
	}

	public void btnChat_OnClick(GameObject go)
	{
		if (LienMinhRoot.activeSelf)
		{
			PopupChat.Create(ChatType.BANG_HOI);
		}
		else if (LienSrvRoot.activeInHierarchy)
		{
			PopupChat.Create(ChatType.LIEN_SERVER);
		}
		else
		{
			PopupChat.Create(ChatType.THE_GIOI);
		}
	}

	public void btnQuayLai_OnClick(GameObject go)
	{
		if (GUIManager.instance.LastScreen == GAME_SCREEN.ScreenLanhDiaMap)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenLanhDiaMap);
		}
	}
}

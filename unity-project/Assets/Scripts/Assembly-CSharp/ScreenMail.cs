using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class ScreenMail : ScreenBase
{
	private enum ScreenMailTab
	{
		TabHeThong = 0,
		TabPhanThuong = 1,
		TabTinNhan = 2
	}

	private const int maxItemCount = 12;

	public GameObject mailPerfab;

	public GameObject ItemRoot;

	private ScreenMailTab m_Tab;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private List<MailItem> ItemList = new List<MailItem>();

	private List<UserInfo.MailData> ListData = new List<UserInfo.MailData>();

	private int startItemGUI_Idx;

	private Vector3 itemPos;

	private Vector3 itemOffset = new Vector3(0f, -230f, 0f);

	public UILabel lbNewMailHeThong;

	public UILabel lbNewMailTinNhan;

	public UILabel lbNewMailQuaTang;

	public UISprite bgNewMailHeThong;

	public UISprite bgNewMailTinNhan;

	public UISprite bgNewMailQuaTang;

	private float nextSecond;

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

	private void Update()
	{
		if (ItemList.Count > 0)
		{
			Vector4 clipRange = panel.clipRange;
			MailItem mailItem = ItemList[ItemList.Count - 1];
			float y = mailItem.transform.localPosition.y;
			MailItem mailItem2 = ItemList[0];
			float y2 = mailItem2.transform.localPosition.y;
			if (y - clipRange.y > -700f)
			{
				SwapDragListDown();
			}
			else if (y2 - clipRange.y < 700f)
			{
				SwapDragListUp();
			}
		}
		nextSecond += Time.deltaTime;
		if (nextSecond >= 300f)
		{
			EGDebug.Log("REFRESH MAIL***********");
			nextSecond = 0f;
			refreshMail(false);
		}
	}

	public void SwapDragListDown()
	{
		addNextItem(ListData);
	}

	public void SwapDragListUp()
	{
		addPrevItem(ListData);
	}

	public void addNextItem(List<UserInfo.MailData> listData)
	{
		if (listData != null || listData.Count > 0)
		{
			int num = startItemGUI_Idx + 12;
			if (num < listData.Count)
			{
				Vector3 vector = default(Vector3);
				vector = new Vector3(0f, 320f, 0f);
				MailItem mailItem = ItemList[0];
				mailItem.mailID = num;
				MailItem mailItem2 = ItemList[ItemList.Count - 1];
				ItemList.RemoveAt(0);
				ItemList.Add(mailItem);
				mailItem.transform.localPosition = mailItem2.transform.localPosition + itemOffset;
				mailItem.SetMailData(listData[num]);
				startItemGUI_Idx++;
			}
		}
	}

	public void addPrevItem(List<UserInfo.MailData> listData)
	{
		if (listData != null && listData.Count > 0 && startItemGUI_Idx != 0)
		{
			startItemGUI_Idx--;
			int num = startItemGUI_Idx;
			if (num >= 0)
			{
				Vector3 vector = default(Vector3);
				vector = new Vector3(0f, 320f, 0f);
				MailItem mailItem = ItemList[0];
				MailItem mailItem2 = ItemList[ItemList.Count - 1];
				mailItem2.mailID = num;
				ItemList.RemoveAt(ItemList.Count - 1);
				ItemList.Insert(0, mailItem2);
				mailItem2.transform.localPosition = mailItem.transform.localPosition - itemOffset;
				mailItem2.SetMailData(listData[num]);
			}
		}
	}

	public void ClearGUIItem()
	{
		startItemGUI_Idx = 0;
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
	}

	public void SyncWithNetworkData(bool isUpdateNewMail = false, bool forceRecreate = false)
	{
		ListData.Clear();
		if (GameManager.instance.m_GameClient.UserInfo.MailList == null || GameManager.instance.m_GameClient.UserInfo.MailList.Count <= 0)
		{
			return;
		}
		if (m_Tab == ScreenMailTab.TabHeThong)
		{
			for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.MailList.Count; i++)
			{
				if (GameManager.instance.m_GameClient.UserInfo.MailList[i].Type == UserInfo.MailData.MAIL_TYPE.TinNhanHeThong)
				{
					ListData.Add(GameManager.instance.m_GameClient.UserInfo.MailList[i]);
				}
			}
		}
		else if (m_Tab == ScreenMailTab.TabPhanThuong)
		{
			for (int j = 0; j < GameManager.instance.m_GameClient.UserInfo.MailList.Count; j++)
			{
				if (GameManager.instance.m_GameClient.UserInfo.MailList[j].Type == UserInfo.MailData.MAIL_TYPE.QuaTang)
				{
					ListData.Add(GameManager.instance.m_GameClient.UserInfo.MailList[j]);
				}
			}
		}
		else if (m_Tab == ScreenMailTab.TabTinNhan)
		{
			for (int k = 0; k < GameManager.instance.m_GameClient.UserInfo.MailList.Count; k++)
			{
				if (GameManager.instance.m_GameClient.UserInfo.MailList[k].Type == UserInfo.MailData.MAIL_TYPE.TinNhan)
				{
					ListData.Add(GameManager.instance.m_GameClient.UserInfo.MailList[k]);
				}
			}
		}
		if ((ItemRoot.transform.childCount == 0) | forceRecreate)
		{
			ClearGUIItem();
			if (ListData != null && ListData.Count > 0)
			{
				AddItem_ToList(ListData, startItemGUI_Idx);
			}
			UIDraggablePanel component = ItemRoot.GetComponent<UIDraggablePanel>();
			component.ResetPosition();
		}
		else
		{
			int l = 0;
			if (ListData.Count >= 12)
			{
				if (startItemGUI_Idx + 12 >= ListData.Count)
				{
					startItemGUI_Idx = ListData.Count - 12;
				}
			}
			else
			{
				startItemGUI_Idx = 0;
			}
			for (int m = startItemGUI_Idx; m < ListData.Count; m++)
			{
				UserInfo.MailData mailData = ListData[m];
				if (l < ItemList.Count)
				{
					ItemList[l].mailID = m;
					ItemList[l].SetMailData(mailData);
				}
				else
				{
					UserInfo.MailData mailData2 = ListData[m];
					MailItem component2 = ((GameObject)Object.Instantiate(mailPerfab)).GetComponent<MailItem>();
					component2.mailID = m;
					component2.transform.parent = ItemRoot.transform;
					component2.transform.localScale = new Vector3(1f, 1f, 1f);
					component2.transform.localPosition = ItemList[ItemList.Count - 1].transform.localPosition + itemOffset;
					component2.SetMailData(mailData2);
					UIEventListener.Get(component2.gameObject).onClick = onClick_MailItem;
					UIEventListener.Get(component2.btnOK.gameObject).onClick = onClick_btnNhan;
					ItemList.Add(component2);
				}
				l++;
				if (l >= 12)
				{
					break;
				}
			}
			if (ListData.Count < 12 && ItemList.Count > ListData.Count)
			{
				int index = l;
				int num = 0;
				for (; l < ItemList.Count; l++)
				{
					Object.Destroy(ItemList[l].gameObject);
					num++;
				}
				if (num > 0)
				{
					ItemList.RemoveRange(index, num);
				}
				UIDraggablePanel component3 = ItemRoot.GetComponent<UIDraggablePanel>();
				component3.ResetPosition();
			}
		}
		GameManager.instance.m_GameClient.RequestReadAllMail();
		if (isUpdateNewMail)
		{
			getInfoNewMail();
		}
		setStatusMail();
	}

	public void setStatusMail()
	{
		for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.MailList.Count; i++)
		{
			UserInfo.MailData mailData = GameManager.instance.m_GameClient.UserInfo.MailList[i];
			if (mailData.Status == UserInfo.MailData.MAIL_STATUS.Unread)
			{
				mailData.Status = UserInfo.MailData.MAIL_STATUS.Read;
			}
		}
	}

	public void AddItem_ToList(List<UserInfo.MailData> listData, int startIndex)
	{
		EGDebug.Log("start index: " + startIndex);
		if (listData == null || listData.Count <= 0)
		{
			return;
		}
		int num = 0;
		itemPos = new Vector3(0f, 320f, 0f);
		for (int i = startIndex; i < listData.Count; i++)
		{
			UserInfo.MailData mailData = listData[i];
			MailItem component = ((GameObject)Object.Instantiate(mailPerfab)).GetComponent<MailItem>();
			component.mailID = i;
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = itemPos;
			itemPos += itemOffset;
			component.SetMailData(mailData);
			UIEventListener.Get(component.gameObject).onClick = onClick_MailItem;
			UIEventListener.Get(component.btnOK.gameObject).onClick = onClick_btnNhan;
			ItemList.Add(component);
			num++;
			if (num >= 12)
			{
				break;
			}
		}
	}

	public void onClick_MailItem(GameObject go)
	{
	}

	public void btnThuMoi_onClick()
	{
		refreshMail();
	}

	public void refreshMail(bool showLoading = true)
	{
		GameManager.instance.m_GameClient.RequestRefreshMail(showLoading);
	}

	public void onClick_btnNhan(GameObject go)
	{
		MailItem component = go.transform.parent.GetComponent<MailItem>();
		if (!(component != null) || component.mMail_Data == null)
		{
			return;
		}
		if (component.mMail_Data.Type == UserInfo.MailData.MAIL_TYPE.QuaTang)
		{
			UseMailPhanThuongRequest useMailPhanThuongRequest = new UseMailPhanThuongRequest();
			useMailPhanThuongRequest.MailID = component.mMail_Data.Id;
			GameManager.instance.m_GameClient.RequestUseMailPhanThuong(useMailPhanThuongRequest);
		}
		else if (component.mMail_Data.Type == UserInfo.MailData.MAIL_TYPE.TinNhan)
		{
			UserInfo.MailData.MailTinNhanContent mailTinNhanContent = JsonMapper.ToObject<UserInfo.MailData.MailTinNhanContent>(component.mMail_Data.Content);
			if (mailTinNhanContent != null)
			{
				PopUpSendMail.Create(mailTinNhanContent.Id, mailTinNhanContent.Name);
			}
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		getInfoNewMail();
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		SyncWithNetworkData();
	}

	public void getInfoNewMail()
	{
		lbNewMailHeThong.gameObject.SetActive(false);
		lbNewMailTinNhan.gameObject.SetActive(false);
		lbNewMailQuaTang.gameObject.SetActive(false);
		bgNewMailHeThong.gameObject.SetActive(false);
		bgNewMailTinNhan.gameObject.SetActive(false);
		bgNewMailQuaTang.gameObject.SetActive(false);
		if (GameManager.instance.m_GameClient.UserInfo.MailList == null || GameManager.instance.m_GameClient.UserInfo.MailList.Count <= 0)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.MailList.Count; i++)
		{
			UserInfo.MailData mailData = GameManager.instance.m_GameClient.UserInfo.MailList[i];
			if (mailData.Status == UserInfo.MailData.MAIL_STATUS.Unread)
			{
				if (mailData.Type == UserInfo.MailData.MAIL_TYPE.TinNhanHeThong)
				{
					num++;
				}
				if (mailData.Type == UserInfo.MailData.MAIL_TYPE.TinNhan)
				{
					num2++;
				}
				if (mailData.Type == UserInfo.MailData.MAIL_TYPE.QuaTang)
				{
					num3++;
				}
			}
		}
		if (num > 0)
		{
			lbNewMailHeThong.gameObject.SetActive(true);
			bgNewMailHeThong.gameObject.SetActive(true);
			lbNewMailHeThong.text = num.ToString();
		}
		if (num2 > 0)
		{
			lbNewMailTinNhan.gameObject.SetActive(true);
			bgNewMailTinNhan.gameObject.SetActive(true);
			lbNewMailTinNhan.text = num2.ToString();
		}
		if (num3 > 0)
		{
			lbNewMailQuaTang.gameObject.SetActive(true);
			bgNewMailQuaTang.gameObject.SetActive(true);
			lbNewMailQuaTang.text = num3.ToString();
		}
	}

	private void onClick_HeThongTab(bool isActive)
	{
		if (isActive && m_Tab != ScreenMailTab.TabHeThong)
		{
			m_Tab = ScreenMailTab.TabHeThong;
			SyncWithNetworkData(false, true);
		}
	}

	private void onClick_TinNhanTab(bool isActive)
	{
		if (isActive && m_Tab != ScreenMailTab.TabTinNhan)
		{
			m_Tab = ScreenMailTab.TabTinNhan;
			SyncWithNetworkData(false, true);
		}
	}

	private void onClick_QuaTangTab(bool isActive)
	{
		if (isActive && m_Tab != ScreenMailTab.TabPhanThuong)
		{
			m_Tab = ScreenMailTab.TabPhanThuong;
			SyncWithNetworkData(false, true);
		}
	}

	public void openPopUpDSPhanThuong(PhanThuongResponse response)
	{
		PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongNhanDuoc"), response);
		SyncWithNetworkData(true);
	}
}

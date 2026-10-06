using System.Collections.Generic;
using UnityEngine;

public class ScreenListBaoKho : ScreenBase
{
	public enum BaoKhoType
	{
		NGAN = 1,
		DONG = 2
	}

	public GameObject ItemRoot;

	public GameObject BaoKhoPrefab;

	public UILabel lbTitle;

	private List<BaoKhoItem> ItemList = new List<BaoKhoItem>();

	private GetListBaoKhoHang2Response responseData;

	private float vSpace = 200f;

	private BaoKhoType currentBKType = BaoKhoType.NGAN;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	public override void OnActive()
	{
		base.OnActive();
		dragPanel = ItemRoot.GetComponent<UIDraggablePanel>();
		panel = ItemRoot.GetComponent<UIPanel>();
		GameManager.instance.m_GameClient.RequestGetListBaoKho();
	}

	public void displayInfo(bool isNganBaoKho)
	{
		if (isNganBaoKho)
		{
			currentBKType = BaoKhoType.NGAN;
		}
		else
		{
			currentBKType = BaoKhoType.DONG;
		}
	}

	private void clearList()
	{
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		ItemList.Clear();
	}

	public void setData(GetListBaoKhoHang2Response response)
	{
		responseData = response;
		displayBaoKhoData();
	}

	public void displayBaoKhoData()
	{
		if (responseData == null)
		{
			return;
		}
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 210f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -160f, 0f);
		if (currentBKType == BaoKhoType.DONG)
		{
			lbTitle.text = Localization.instance.Get("ThietBaoKhoLabelBtn");
			if (responseData.listBaoKhoDong.Count > 0)
			{
				clearList();
				for (int i = 0; i < responseData.listBaoKhoDong.Count; i++)
				{
					UserInfo.BaoKhoInfo data = responseData.listBaoKhoDong[i];
					BaoKhoItem component = ((GameObject)Object.Instantiate(BaoKhoPrefab)).GetComponent<BaoKhoItem>();
					component.transform.parent = ItemRoot.transform;
					component.transform.localScale = new Vector3(1f, 1f, 1f);
					component.transform.localPosition = vector;
					vector += vector2;
					component.setData(data);
					UIEventListener.Get(component.gameObject).onClick = onBaoKhoClick;
					ItemList.Add(component);
				}
			}
		}
		else if (currentBKType == BaoKhoType.NGAN)
		{
			lbTitle.text = Localization.instance.Get("NganBaoKhoLabelBtn");
			if (responseData.listBaoKhoNgan.Count > 0)
			{
				clearList();
				for (int j = 0; j < responseData.listBaoKhoNgan.Count; j++)
				{
					UserInfo.BaoKhoInfo data2 = responseData.listBaoKhoNgan[j];
					BaoKhoItem component2 = ((GameObject)Object.Instantiate(BaoKhoPrefab)).GetComponent<BaoKhoItem>();
					component2.transform.parent = ItemRoot.transform;
					component2.transform.localScale = new Vector3(1f, 1f, 1f);
					component2.transform.localPosition = vector;
					vector += vector2;
					component2.setData(data2);
					UIEventListener.Get(component2.gameObject).onClick = onBaoKhoClick;
					ItemList.Add(component2);
				}
			}
		}
		UIDraggablePanel component3 = ItemRoot.GetComponent<UIDraggablePanel>();
		component3.ResetPosition();
	}

	public void onBaoKhoClick(GameObject go)
	{
		BaoKhoItem component = go.GetComponent<BaoKhoItem>();
		if (!(component != null) || component.baoKhoData == null)
		{
			return;
		}
		if (component.baoKhoData.GID > 0)
		{
			if (component.baoKhoData.GID == GameManager.instance.m_GameClient.UserInfo.Gamer.ID && component.baoKhoData.SID == GameManager.instance.m_GameClient.UserInfo.ServerInfo.ID)
			{
				PopupMyBaoKho.Create(component.baoKhoData);
			}
			else
			{
				PopupOtherBaoKho.Create(component.baoKhoData);
			}
		}
		else
		{
			PopupBaoKhoEmpty.Create(component.baoKhoData);
		}
	}

	public void onClick_NganBaoKho()
	{
		if (currentBKType != BaoKhoType.NGAN)
		{
			currentBKType = BaoKhoType.NGAN;
			lbTitle.text = Localization.instance.Get("NganBaoKhoLabelBtn");
			displayBaoKhoData();
		}
	}

	public void onClick_ThietBaoKho()
	{
		if (currentBKType != BaoKhoType.DONG)
		{
			currentBKType = BaoKhoType.DONG;
			lbTitle.text = Localization.instance.Get("ThietBaoKhoLabelBtn");
			displayBaoKhoData();
		}
	}

	public void OnHoangTrieuBaoKhoClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBaoKhoMain);
	}

	public void OnBaoKhoSuMonClick()
	{
		if (GameManager.instance.m_GameClient.BaoKhoInfoResponse != null && GameManager.instance.m_GameClient.BaoKhoInfoResponse.listMyBaoKho != null && GameManager.instance.m_GameClient.BaoKhoInfoResponse.listMyBaoKho.Count > 0)
		{
			GUIManager.setScreen(GAME_SCREEN.ScreenMyListBaoKho);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("KhongCoBaoKhoMess"));
		}
	}

	public void updateView(UserInfo.BaoKhoInfo baoKhoInfo)
	{
		if (baoKhoInfo == null || ItemList == null || ItemList.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < ItemList.Count; i++)
		{
			if (ItemList[i].baoKhoData.ID == baoKhoInfo.ID)
			{
				ItemList[i].setData(baoKhoInfo);
			}
		}
	}
}

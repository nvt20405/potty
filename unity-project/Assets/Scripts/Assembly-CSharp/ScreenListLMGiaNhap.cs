using System.Collections.Generic;
using UnityEngine;

public class ScreenListLMGiaNhap : ScreenBase
{
	public Transform ListParent;

	public GameObject LienMinhItem;

	public int LienMinhItemSize;

	public GameObject LienMinhDetail;

	public int LienMinhDetailSize;

	public List<GameObject> itemList;

	private int curDetail;

	public UIInput searchText;

	public GameObject searchBtn;

	private void Start()
	{
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
	}

	public void OnEnable()
	{
		curDetail = -1;
		if (ScreenLienMinh.TopLienMinhList != null)
		{
			int num = 0;
			foreach (GameObject item in itemList)
			{
				item.SetActive(false);
			}
			foreach (LienMinhData topLienMinh in ScreenLienMinh.TopLienMinhList)
			{
				num++;
				GameObject gameObject;
				if (num > itemList.Count)
				{
					Object obj = Object.Instantiate(LienMinhItem);
					gameObject = (GameObject)((obj is GameObject) ? obj : null);
					itemList.Add(gameObject);
				}
				else
				{
					gameObject = itemList[num - 1];
				}
				gameObject.SetActive(true);
				gameObject.transform.parent = ListParent;
				gameObject.transform.localScale = LienMinhItem.transform.localScale;
				gameObject.transform.localPosition = new Vector3(0f, -LienMinhItemSize * (num - 2), 0f);
				gameObject.transform.Find("LienMinhName").GetComponent<UILabel>().text = topLienMinh.DisplayName;
				gameObject.transform.Find("Top").GetComponent<UILabel>().text = num.ToString();
				gameObject.transform.Find("ThanhVienCount").GetComponent<UILabel>().text = topLienMinh.ThanhVienList.Count + "/" + ConfigManager.instance.CongTrinhLienMinhConfig["TuNghiaDuong"].Value[topLienMinh.TuNghiaLevel];
				gameObject.name = num.ToString();
			}
		}
		ListParent.GetComponent<UIDraggablePanel>().ResetPosition();
		LienMinhItem.SetActive(false);
		LienMinhDetail.SetActive(false);
	}

	public void OnLienMinhItemTouch(GameObject item)
	{
		int num = int.Parse(item.name) - 1;
		if (num == curDetail)
		{
			for (int i = num + 1; i < itemList.Count; i++)
			{
				itemList[i].transform.localPosition += LienMinhDetailSize * Vector3.up;
			}
			curDetail = -1;
			LienMinhDetail.SetActive(false);
			return;
		}
		if (LienMinhDetail.activeSelf)
		{
			for (int j = curDetail + 1; j < itemList.Count; j++)
			{
				itemList[j].transform.localPosition += LienMinhDetailSize * Vector3.up;
			}
		}
		LienMinhDetail.transform.localPosition = itemList[num].transform.localPosition + LienMinhDetailSize * Vector3.down;
		for (int k = num + 1; k < itemList.Count; k++)
		{
			itemList[k].transform.localPosition += LienMinhDetailSize * Vector3.down;
		}
		curDetail = num;
		if (GameManager.instance.m_GameClient.UserInfo.LienMinhCaNhan != null && GameManager.instance.m_GameClient.UserInfo.LienMinhCaNhan.RequestList.Contains(ScreenLienMinh.TopLienMinhList[curDetail].ID))
		{
			LienMinhDetail.transform.Find("XinGiaNhapLabel").GetComponent<UILabel>().text = Localization.instance.Get("RutXinGiaNhap");
		}
		else
		{
			LienMinhDetail.transform.Find("XinGiaNhapLabel").GetComponent<UILabel>().text = Localization.instance.Get("XinGiaNhap");
		}
		LienMinhDetail.SetActive(true);
	}

	public void OnXinGiaNhap()
	{
		if (GameManager.instance.m_GameClient.UserInfo.LienMinhCaNhan != null && GameManager.instance.m_GameClient.UserInfo.LienMinhCaNhan.RequestList.Contains(ScreenLienMinh.TopLienMinhList[curDetail].ID))
		{
			ScreenLienMinh.ScreenStatus = "LienMinhRutGiaNhap";
			ScreenLienMinh.CurLienMinh = curDetail;
			GameManager.instance.m_GameClient.RequestRutGiaNhapLienMinh(ScreenLienMinh.TopLienMinhList[curDetail].ID);
		}
		else
		{
			ScreenLienMinh.ScreenStatus = "LienMinhXinGiaNhap";
			ScreenLienMinh.CurLienMinh = curDetail;
			GameManager.instance.m_GameClient.RequestGiaNhapLienMinh(ScreenLienMinh.TopLienMinhList[curDetail].ID);
		}
	}

	public void OnLienMinhDetail()
	{
		ScreenLienMinh.ScreenStatus = "LienMinhDetail";
		ScreenLienMinh.CurLienMinh = curDetail;
		GameManager.instance.m_GameClient.RequestGetLienMinhInfo(ScreenLienMinh.TopLienMinhList[curDetail].ID);
	}

	public void OnLienMinhSearch()
	{
		SearchLienMinhRequest searchLienMinhRequest = new SearchLienMinhRequest();
		searchLienMinhRequest.TenLienMinh = searchText.text;
		GameManager.instance.m_GameClient.RequestSearchLienMinh(searchLienMinhRequest);
	}
}

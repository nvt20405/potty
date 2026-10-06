using System.Collections.Generic;
using UnityEngine;

public class ScreenLienMinhThanhVienGuest : ScreenBase
{
	public Transform ListParent;

	public GameObject LienMinhItem;

	public int LienMinhItemSize;

	public GameObject LienMinhDetail;

	public int LienMinhDetailSize;

	public List<GameObject> itemList;

	private int curDetail;

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
	}

	private void OnEnable()
	{
		curDetail = -1;
		if (ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList == null)
		{
			return;
		}
		int num = 0;
		base.transform.Find("LienMinhSoluong").GetComponent<UILabel>().text = Localization.instance.Get("ThanhVien") + ": " + ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList.Count + "/" + ConfigManager.instance.CongTrinhLienMinhConfig["TuNghiaDuong"].Value[ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].TuNghiaLevel];
		base.transform.Find("TenLienMinh").GetComponent<UILabel>().text = ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].DisplayName;
		for (int i = 0; i < ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList.Count; i++)
		{
			if (i != 0 && ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[i].ID == ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].MinhChuID)
			{
				LienMinhThanhVienData value = ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[i];
				ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[i] = ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[0];
				ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[0] = value;
			}
		}
		int num2 = 1;
		if (ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].PhoMinhChuID > 0)
		{
			num2 = 2;
			for (int j = 1; j < ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList.Count; j++)
			{
				if (j != 1 && ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[j].ID == ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].PhoMinhChuID)
				{
					LienMinhThanhVienData value2 = ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[j];
					ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[j] = ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[1];
					ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[1] = value2;
				}
			}
		}
		for (int k = num2; k < ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList.Count; k++)
		{
			for (int l = k; l < ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList.Count; l++)
			{
				if (ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[l].CongHienLienMinh > ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[k].CongHienLienMinh)
				{
					LienMinhThanhVienData value3 = ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[k];
					ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[k] = ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[l];
					ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[l] = value3;
				}
			}
		}
		foreach (GameObject item in itemList)
		{
			item.SetActive(false);
		}
		LienMinhItem.SetActive(true);
		foreach (LienMinhThanhVienData thanhVien in ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList)
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
			gameObject.transform.parent = ListParent;
			gameObject.transform.localScale = LienMinhItem.transform.localScale;
			gameObject.transform.localPosition = new Vector3(0f, -LienMinhItemSize * (num - 2), 0f);
			gameObject.transform.Find("LienMinhName").GetComponent<UILabel>().text = thanhVien.DisplayName;
			gameObject.transform.Find("Vip").GetComponent<UISprite>().spriteName = "icon_vip" + thanhVien.Vip;
			gameObject.transform.Find("Vip").GetComponent<UISprite>().MakePixelPerfect();
			if (thanhVien.ID == ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].MinhChuID)
			{
				gameObject.transform.Find("ChucVu").GetComponent<UILabel>().text = Localization.instance.Get("MinhChu");
				gameObject.transform.Find("ChucVuBg").GetComponent<UISprite>().color = new Color(1f, 1f, 1f, 1f);
			}
			else if (thanhVien.ID == ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].PhoMinhChuID)
			{
				gameObject.transform.Find("ChucVu").GetComponent<UILabel>().text = Localization.instance.Get("PhoMinhChu");
				gameObject.transform.Find("ChucVuBg").GetComponent<UISprite>().color = new Color(1f, 1f, 1f, 1f);
			}
			else
			{
				gameObject.transform.Find("ChucVu").GetComponent<UILabel>().text = string.Empty;
				gameObject.transform.Find("ChucVuBg").GetComponent<UISprite>().color = new Color(1f, 1f, 1f, 0f);
			}
			gameObject.name = num.ToString();
			gameObject.SetActive(true);
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
		LienMinhDetail.SetActive(true);
	}

	public void OnThongTin()
	{
		XemThongTinMonPhaiRequest xemThongTinMonPhaiRequest = new XemThongTinMonPhaiRequest();
		xemThongTinMonPhaiRequest.TargetGID = ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[curDetail].ID;
		GameManager.instance.m_GameClient.RequestXemThongTinMonPhai(xemThongTinMonPhaiRequest);
	}

	public void OnNhanTin()
	{
		PopUpSendMail.Create(ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[curDetail].ID, ScreenLienMinh.TopLienMinhList[ScreenLienMinh.CurLienMinh].ThanhVienList[curDetail].DisplayName);
	}
}

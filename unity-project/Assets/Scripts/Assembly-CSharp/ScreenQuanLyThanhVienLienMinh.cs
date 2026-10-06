using System.Collections.Generic;
using UnityEngine;

public class ScreenQuanLyThanhVienLienMinh : ScreenBase
{
	public Transform ListThanhVienRoot;

	public GameObject ThanhVienItem;

	public int ThanhVienItemSize;

	public GameObject ThanhVienDetail;

	public List<GameObject> ThanhVienButtonList;

	public int ThanhVienDetailSize;

	public List<GameObject> thanhvienItemList;

	private int ThanhVienCurDetail;

	public GameObject DanhSachBtn;

	public GameObject GiaNhapBtn;

	public GameObject LienMinhBtn;

	public Transform ListGiaNhapRoot;

	public GameObject GiaNhapItem;

	public int GiaNhapItemSize;

	public GameObject GiaNhapDetail;

	public List<GameObject> GiaNhapButtonList;

	public int GiaNhapDetailSize;

	public List<GameObject> gianhapItemList;

	private int GiaNhapCurDetail;

	public Transform LienMinhListRoot;

	public GameObject LienMinhItem;

	public int LienMinhItemSize;

	public GameObject LienMinhDetail;

	public int LienMinhDetailSize;

	public List<GameObject> LienMinhItemList;

	public GameObject ThongTinLienMinhBtn;

	private int curDetailLienMinh;

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		OnDanhSach(ListThanhVienRoot.gameObject.activeSelf);
	}

	public void OnDanhSach(bool onSelected)
	{
		if (!onSelected)
		{
			ListThanhVienRoot.gameObject.SetActive(false);
			return;
		}
		ListThanhVienRoot.gameObject.SetActive(true);
		ThanhVienCurDetail = -1;
		for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList.Count; i++)
		{
			if (i != 0 && GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[i].ID == GameManager.instance.m_GameClient.UserInfo.LienMinh.MinhChuID)
			{
				LienMinhThanhVienData value = GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[i];
				GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[i] = GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[0];
				GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[0] = value;
			}
		}
		int num = 1;
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh.PhoMinhChuID > 0)
		{
			num = 2;
			for (int j = 1; j < GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList.Count; j++)
			{
				if (j != 1 && GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[j].ID == GameManager.instance.m_GameClient.UserInfo.LienMinh.PhoMinhChuID)
				{
					LienMinhThanhVienData value2 = GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[j];
					GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[j] = GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[1];
					GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[1] = value2;
				}
			}
		}
		for (int k = num; k < GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList.Count; k++)
		{
			for (int l = k; l < GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList.Count; l++)
			{
				if (GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[l].CongHienLienMinh > GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[k].CongHienLienMinh)
				{
					LienMinhThanhVienData value3 = GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[k];
					GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[k] = GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[l];
					GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[l] = value3;
				}
			}
		}
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList == null)
		{
			return;
		}
		int num2 = 0;
		base.transform.Find("LienMinhStatus").GetComponent<UILabel>().text = string.Format(Localization.instance.Get("LienMinhStatus"), GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel, GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList.Count, GameManager.instance.m_GameClient.UserInfo.LienMinh.DiemCongHien, ConfigManager.instance.CongTrinhLienMinhConfig["TuNghiaDuong"].Value[GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel]);
		base.transform.Find("LienMinhName").GetComponent<UILabel>().text = GameManager.instance.m_GameClient.UserInfo.LienMinh.DisplayName;
		foreach (GameObject thanhvienItem in thanhvienItemList)
		{
			thanhvienItem.SetActive(false);
		}
		foreach (LienMinhThanhVienData thanhVien in GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList)
		{
			num2++;
			GameObject gameObject;
			if (num2 > thanhvienItemList.Count)
			{
				Object obj = Object.Instantiate(ThanhVienItem);
				gameObject = (GameObject)((obj is GameObject) ? obj : null);
				thanhvienItemList.Add(gameObject);
			}
			else
			{
				gameObject = thanhvienItemList[num2 - 1];
			}
			gameObject.SetActive(true);
			gameObject.transform.parent = ListThanhVienRoot;
			gameObject.transform.localScale = ThanhVienItem.transform.localScale;
			gameObject.transform.localPosition = new Vector3(0f, -ThanhVienItemSize * (num2 - 2), 0f);
			gameObject.transform.Find("ThanhVienName").GetComponent<UILabel>().text = thanhVien.DisplayName;
			gameObject.transform.Find("Vip").GetComponent<UISprite>().spriteName = "icon_vip" + thanhVien.Vip;
			gameObject.transform.Find("Vip").GetComponent<UISprite>().MakePixelPerfect();
			if (thanhVien.ID == GameManager.instance.m_GameClient.UserInfo.LienMinh.MinhChuID)
			{
				gameObject.transform.Find("Rank").GetComponent<UILabel>().text = string.Empty;
				gameObject.transform.Find("ChucVu").GetComponent<UISprite>().spriteName = "bang_chu";
			}
			else if (thanhVien.ID == GameManager.instance.m_GameClient.UserInfo.LienMinh.PhoMinhChuID)
			{
				gameObject.transform.Find("Rank").GetComponent<UILabel>().text = string.Empty;
				gameObject.transform.Find("ChucVu").GetComponent<UISprite>().spriteName = "bang_pho";
			}
			else if (num2 <= 10)
			{
				gameObject.transform.Find("Rank").GetComponent<UILabel>().text = string.Empty;
				gameObject.transform.Find("ChucVu").GetComponent<UISprite>().spriteName = "tinh_anh";
			}
			else
			{
				gameObject.transform.Find("Rank").GetComponent<UILabel>().text = num2.ToString();
				gameObject.transform.Find("ChucVu").GetComponent<UISprite>().spriteName = "binh_thuong";
			}
			if (!thanhVien.Online)
			{
				gameObject.transform.Find("Online").GetComponent<UILabel>().text = "[FF0000]Offline[-]";
			}
			else
			{
				gameObject.transform.Find("Online").GetComponent<UILabel>().text = "[00FF00]Online[-]";
			}
			gameObject.transform.Find("CongHien").GetComponent<UILabel>().text = thanhVien.CongHienLienMinh.ToString();
			gameObject.name = num2.ToString();
		}
		ThanhVienItem.SetActive(false);
		ThanhVienDetail.SetActive(false);
	}

	public void OnDetailThanhVien(GameObject item)
	{
		int num = int.Parse(item.name) - 1;
		if (num == ThanhVienCurDetail)
		{
			for (int i = num + 1; i < thanhvienItemList.Count; i++)
			{
				thanhvienItemList[i].transform.localPosition += ThanhVienDetailSize * Vector3.up;
			}
			ThanhVienCurDetail = -1;
			ThanhVienDetail.SetActive(false);
			return;
		}
		if (ThanhVienDetail.activeSelf)
		{
			for (int j = ThanhVienCurDetail + 1; j < thanhvienItemList.Count; j++)
			{
				thanhvienItemList[j].transform.localPosition += ThanhVienDetailSize * Vector3.up;
			}
		}
		ThanhVienDetail.transform.localPosition = thanhvienItemList[num].transform.localPosition + ThanhVienDetailSize * Vector3.down;
		for (int k = num + 1; k < thanhvienItemList.Count; k++)
		{
			thanhvienItemList[k].transform.localPosition += ThanhVienDetailSize * Vector3.down;
		}
		ThanhVienButtonList[0].gameObject.SetActive(true);
		ThanhVienButtonList[1].gameObject.SetActive(true);
		ThanhVienButtonList[2].gameObject.SetActive(true);
		ThanhVienButtonList[3].gameObject.SetActive(true);
		ThanhVienButtonList[4].gameObject.SetActive(false);
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.ID != GameManager.instance.m_GameClient.UserInfo.LienMinh.MinhChuID && GameManager.instance.m_GameClient.UserInfo.Gamer.ID != GameManager.instance.m_GameClient.UserInfo.LienMinh.PhoMinhChuID)
		{
			ThanhVienButtonList[0].gameObject.SetActive(false);
			ThanhVienButtonList[3].gameObject.SetActive(false);
		}
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[num].ID == GameManager.instance.m_GameClient.UserInfo.Gamer.ID)
		{
			ThanhVienButtonList[1].gameObject.SetActive(false);
			ThanhVienButtonList[2].gameObject.SetActive(false);
			ThanhVienButtonList[0].gameObject.SetActive(false);
			ThanhVienButtonList[3].gameObject.SetActive(false);
			ThanhVienButtonList[4].gameObject.SetActive(true);
			ThanhVienDetail.transform.Find("Label").GetComponent<UILabel>().color = new Color(1f, 1f, 1f, 1f);
		}
		else
		{
			ThanhVienDetail.transform.Find("Label").GetComponent<UILabel>().color = new Color(1f, 1f, 1f, 0f);
		}
		ThanhVienCurDetail = num;
		ThanhVienDetail.SetActive(true);
	}

	public void OnThongTinThanhVien()
	{
		XemThongTinMonPhaiRequest xemThongTinMonPhaiRequest = new XemThongTinMonPhaiRequest();
		xemThongTinMonPhaiRequest.TargetGID = GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[ThanhVienCurDetail].ID;
		GameManager.instance.m_GameClient.RequestXemThongTinMonPhai(xemThongTinMonPhaiRequest);
	}

	public void OnNhanTinThanhVien()
	{
		PopUpSendMail.Create(GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[ThanhVienCurDetail].ID, GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[ThanhVienCurDetail].DisplayName);
	}

	public void OnSetPhoMinhChu()
	{
		PopupYesNo.Create(Localization.instance.Get("ConfirmSetPhoMinhChu"), Localization.instance.Get("Confirm"), Localization.instance.Get("Cancel"), OnSetPhoMinhChuConfirm, null);
	}

	public void OnSetPhoMinhChuConfirm()
	{
		DoiPhoMinhChuRequest doiPhoMinhChuRequest = new DoiPhoMinhChuRequest();
		doiPhoMinhChuRequest.TargetId = GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[ThanhVienCurDetail].ID;
		GameManager.instance.m_GameClient.RequestDoiPhoMinhChu(doiPhoMinhChuRequest);
	}

	public void OnKick()
	{
		PopupYesNo.Create(Localization.instance.Get("ConfirmKickMember"), Localization.instance.Get("Confirm"), Localization.instance.Get("Cancel"), OnKicKConfirm, null);
	}

	public void OnKicKConfirm()
	{
		DuoiKhoiLienMinhRequest duoiKhoiLienMinhRequest = new DuoiKhoiLienMinhRequest();
		duoiKhoiLienMinhRequest.TargetId = GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList[ThanhVienCurDetail].ID;
		GameManager.instance.m_GameClient.RequestDuoiKhoiLienMinh(duoiKhoiLienMinhRequest);
	}

	public void OnQuit()
	{
		PopupYesNo.Create(Localization.instance.Get("ConfirmQuit"), Localization.instance.Get("Confirm"), Localization.instance.Get("Cancel"), OnQuitConfirm, null);
	}

	public void OnQuitConfirm()
	{
		ThoatLienMinhRequest thoatLienMinhRequest = new ThoatLienMinhRequest();
		thoatLienMinhRequest.LienMinhID = GameManager.instance.m_GameClient.UserInfo.LienMinh.ID;
		GameManager.instance.m_GameClient.RequestThoatLienMinh(thoatLienMinhRequest);
	}

	public void OnGiaNhap(bool onSelected)
	{
		if (!onSelected)
		{
			ListGiaNhapRoot.gameObject.SetActive(false);
			return;
		}
		ListGiaNhapRoot.gameObject.SetActive(true);
		GiaNhapCurDetail = -1;
		if (GameManager.instance.m_GameClient.UserInfo.LienMinh.XinGiaNhapList != null)
		{
			int num = 0;
			base.transform.Find("LienMinhStatus").GetComponent<UILabel>().text = string.Format(Localization.instance.Get("LienMinhStatus"), GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel, GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList.Count, GameManager.instance.m_GameClient.UserInfo.LienMinh.DiemCongHien, ConfigManager.instance.CongTrinhLienMinhConfig["TuNghiaDuong"].Value[GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel]);
			base.transform.Find("LienMinhName").GetComponent<UILabel>().text = GameManager.instance.m_GameClient.UserInfo.LienMinh.DisplayName;
			foreach (GameObject gianhapItem in gianhapItemList)
			{
				gianhapItem.SetActive(false);
			}
			foreach (LienMinhXinGiaNhapData xinGiaNhap in GameManager.instance.m_GameClient.UserInfo.LienMinh.XinGiaNhapList)
			{
				num++;
				GameObject gameObject;
				if (num > gianhapItemList.Count)
				{
					Object obj = Object.Instantiate(GiaNhapItem);
					gameObject = (GameObject)((obj is GameObject) ? obj : null);
					gianhapItemList.Add(gameObject);
				}
				else
				{
					gameObject = gianhapItemList[num - 1];
				}
				gameObject.SetActive(true);
				gameObject.transform.parent = ListGiaNhapRoot;
				gameObject.transform.localScale = GiaNhapItem.transform.localScale;
				gameObject.transform.localPosition = new Vector3(0f, -GiaNhapItemSize * (num - 2), 0f);
				gameObject.transform.Find("Rank").GetComponent<UILabel>().text = num.ToString();
				gameObject.transform.Find("ThanhVienName").GetComponent<UILabel>().text = xinGiaNhap.DisplayName;
				gameObject.transform.Find("Vip").GetComponent<UISprite>().spriteName = "icon_vip" + xinGiaNhap.Vip;
				gameObject.transform.Find("Vip").GetComponent<UISprite>().MakePixelPerfect();
				gameObject.transform.Find("Level").GetComponent<UILabel>().text = "Lv." + xinGiaNhap.Level;
				gameObject.name = num.ToString();
			}
		}
		GiaNhapItem.SetActive(false);
		GiaNhapDetail.SetActive(false);
	}

	public void OnDetailGiaNhap(GameObject item)
	{
		int num = int.Parse(item.name) - 1;
		if (num == GiaNhapCurDetail)
		{
			for (int i = num + 1; i < gianhapItemList.Count; i++)
			{
				gianhapItemList[i].transform.localPosition += GiaNhapDetailSize * Vector3.up;
			}
			GiaNhapCurDetail = -1;
			GiaNhapDetail.SetActive(false);
			return;
		}
		if (GiaNhapDetail.activeSelf)
		{
			for (int j = GiaNhapCurDetail + 1; j < gianhapItemList.Count; j++)
			{
				gianhapItemList[j].transform.localPosition += GiaNhapDetailSize * Vector3.up;
			}
		}
		GiaNhapDetail.transform.localPosition = gianhapItemList[num].transform.localPosition + GiaNhapDetailSize * Vector3.down;
		for (int k = num + 1; k < gianhapItemList.Count; k++)
		{
			gianhapItemList[k].transform.localPosition += GiaNhapDetailSize * Vector3.down;
		}
		if ((GameManager.instance.m_GameClient.UserInfo.Gamer.ID != GameManager.instance.m_GameClient.UserInfo.LienMinh.MinhChuID && GameManager.instance.m_GameClient.UserInfo.Gamer.ID != GameManager.instance.m_GameClient.UserInfo.LienMinh.PhoMinhChuID) || GameManager.instance.m_GameClient.UserInfo.LienMinh.XinGiaNhapList[num].ID == GameManager.instance.m_GameClient.UserInfo.Gamer.ID)
		{
			GiaNhapButtonList[0].gameObject.SetActive(false);
			GiaNhapButtonList[1].gameObject.SetActive(false);
		}
		else
		{
			GiaNhapButtonList[0].gameObject.SetActive(true);
			GiaNhapButtonList[1].gameObject.SetActive(true);
		}
		GiaNhapCurDetail = num;
		GiaNhapDetail.SetActive(true);
	}

	public void OnThongTinGiaNhap()
	{
		XemThongTinMonPhaiRequest xemThongTinMonPhaiRequest = new XemThongTinMonPhaiRequest();
		xemThongTinMonPhaiRequest.TargetGID = GameManager.instance.m_GameClient.UserInfo.LienMinh.XinGiaNhapList[GiaNhapCurDetail].ID;
		GameManager.instance.m_GameClient.RequestXemThongTinMonPhai(xemThongTinMonPhaiRequest);
	}

	public void OnNhanTinGiaNhap()
	{
		PopUpSendMail.Create(GameManager.instance.m_GameClient.UserInfo.LienMinh.XinGiaNhapList[GiaNhapCurDetail].ID, GameManager.instance.m_GameClient.UserInfo.LienMinh.XinGiaNhapList[GiaNhapCurDetail].DisplayName);
	}

	public void OnDongYGiaNhap()
	{
		ChapNhanGiaNhapLienMinhRequest chapNhanGiaNhapLienMinhRequest = new ChapNhanGiaNhapLienMinhRequest();
		chapNhanGiaNhapLienMinhRequest.Ok = true;
		chapNhanGiaNhapLienMinhRequest.LienMinhID = GameManager.instance.m_GameClient.UserInfo.LienMinh.ID;
		chapNhanGiaNhapLienMinhRequest.ThanhVienID = GameManager.instance.m_GameClient.UserInfo.LienMinh.XinGiaNhapList[GiaNhapCurDetail].ID;
		GameManager.instance.m_GameClient.RequestChapNhanGiaNhapLienMinh(chapNhanGiaNhapLienMinhRequest);
	}

	public void OnTuChoiGiaNhap()
	{
		ChapNhanGiaNhapLienMinhRequest chapNhanGiaNhapLienMinhRequest = new ChapNhanGiaNhapLienMinhRequest();
		chapNhanGiaNhapLienMinhRequest.Ok = false;
		chapNhanGiaNhapLienMinhRequest.LienMinhID = GameManager.instance.m_GameClient.UserInfo.LienMinh.ID;
		chapNhanGiaNhapLienMinhRequest.ThanhVienID = GameManager.instance.m_GameClient.UserInfo.LienMinh.XinGiaNhapList[GiaNhapCurDetail].ID;
		GameManager.instance.m_GameClient.RequestChapNhanGiaNhapLienMinh(chapNhanGiaNhapLienMinhRequest);
	}

	public void OnLienMinh(bool onSelected)
	{
		if (!onSelected)
		{
			LienMinhListRoot.gameObject.SetActive(false);
			return;
		}
		LienMinhListRoot.gameObject.SetActive(true);
		curDetailLienMinh = -1;
		if (ScreenLienMinh.TopLienMinhList == null)
		{
			return;
		}
		int num = 0;
		foreach (GameObject thanhvienItem in thanhvienItemList)
		{
			thanhvienItem.SetActive(false);
		}
		foreach (LienMinhData topLienMinh in ScreenLienMinh.TopLienMinhList)
		{
			num++;
			GameObject gameObject;
			if (num > LienMinhItemList.Count)
			{
				Object obj = Object.Instantiate(LienMinhItem);
				gameObject = (GameObject)((obj is GameObject) ? obj : null);
				LienMinhItemList.Add(gameObject);
			}
			else
			{
				gameObject = LienMinhItemList[num - 1];
			}
			gameObject.SetActive(true);
			gameObject.transform.parent = LienMinhListRoot;
			gameObject.transform.localScale = LienMinhItem.transform.localScale;
			gameObject.transform.localPosition = new Vector3(0f, -LienMinhItemSize * (num - 2), 0f);
			gameObject.transform.Find("LienMinhName").GetComponent<UILabel>().text = topLienMinh.DisplayName;
			gameObject.transform.Find("Top").GetComponent<UILabel>().text = num.ToString();
			gameObject.transform.Find("ThanhVienCount").GetComponent<UILabel>().text = topLienMinh.ThanhVienList.Count + "/" + ConfigManager.instance.CongTrinhLienMinhConfig["TuNghiaDuong"].Value[topLienMinh.TuNghiaLevel];
			switch (num)
			{
			case 1:
				gameObject.transform.Find("ChucVuBg").GetComponent<UISprite>().spriteName = "hang1";
				break;
			case 2:
				gameObject.transform.Find("ChucVuBg").GetComponent<UISprite>().spriteName = "hang2";
				break;
			case 3:
				gameObject.transform.Find("ChucVuBg").GetComponent<UISprite>().spriteName = "hang3";
				break;
			default:
				gameObject.transform.Find("ChucVuBg").GetComponent<UISprite>().spriteName = "hang4";
				break;
			}
			gameObject.name = num.ToString();
		}
		LienMinhItem.SetActive(false);
		LienMinhDetail.SetActive(false);
	}

	public void OnLienMinhDetail(GameObject item)
	{
		int num = int.Parse(item.name) - 1;
		if (num == curDetailLienMinh)
		{
			for (int i = num + 1; i < LienMinhItemList.Count; i++)
			{
				LienMinhItemList[i].transform.localPosition += LienMinhDetailSize * Vector3.up;
			}
			curDetailLienMinh = -1;
			LienMinhDetail.SetActive(false);
			return;
		}
		if (LienMinhDetail.activeSelf)
		{
			for (int j = curDetailLienMinh + 1; j < LienMinhItemList.Count; j++)
			{
				LienMinhItemList[j].transform.localPosition += LienMinhDetailSize * Vector3.up;
			}
		}
		LienMinhDetail.transform.localPosition = LienMinhItemList[num].transform.localPosition + LienMinhDetailSize * Vector3.down;
		for (int k = num + 1; k < LienMinhItemList.Count; k++)
		{
			LienMinhItemList[k].transform.localPosition += LienMinhDetailSize * Vector3.down;
		}
		if (ScreenLienMinh.TopLienMinhList[num].ID == GameManager.instance.m_GameClient.UserInfo.LienMinh.ID)
		{
			ThongTinLienMinhBtn.SetActive(false);
			LienMinhDetail.transform.Find("Label").GetComponent<UILabel>().color = new Color(1f, 1f, 1f, 1f);
		}
		else
		{
			ThongTinLienMinhBtn.SetActive(true);
			LienMinhDetail.transform.Find("Label").GetComponent<UILabel>().color = new Color(1f, 1f, 1f, 0f);
		}
		curDetailLienMinh = num;
		LienMinhDetail.SetActive(true);
	}

	public void OnThongTinLienMinh()
	{
		ScreenLienMinh.ScreenStatus = "LienMinhDetail";
		ScreenLienMinh.CurLienMinh = curDetailLienMinh;
		GameManager.instance.m_GameClient.RequestGetLienMinhInfo(ScreenLienMinh.TopLienMinhList[curDetailLienMinh].ID);
	}
}

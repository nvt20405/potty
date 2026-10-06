using System.Collections.Generic;
using UnityEngine;

public class ScreenCongHienLienMinh : ScreenBase
{
	public UICheckbox DangHuongTab;

	public UICheckbox CongHienTab;

	public UICheckbox DoiThuongTab;

	public List<UICheckbox> DoiThuongTabList;

	public UILabel CongHienStatus;

	public GameObject DangHuong;

	public GameObject NhiemVuItem;

	public GameObject NhiemVuRoot;

	public List<GameObject> NhiemVuItemList;

	public UILabel DangHuongFree;

	public GameObject ResetItem;

	public int NhiemVuItemSize;

	public GameObject DoiThuongItem;

	public GameObject DoiThuongGrp;

	public GameObject DoiThuongRoot;

	public List<GameObject> DoiThuongItemList;

	public int DoiThuongItemSize;

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		AudioListener component = GUIManager.instance.cam2D.GetComponent<AudioListener>();
		if (component != null)
		{
			component.enabled = false;
		}
		OnCongHienTab(true);
		OnDangHuongTab(false);
		OnDoiThuongTab(false, true);
	}

	public void OnDangHuongTab(bool onSelected)
	{
		if (!onSelected)
		{
			DangHuongTab.isChecked = false;
			DangHuong.SetActive(false);
			return;
		}
		DangHuongTab.isChecked = true;
		DangHuong.SetActive(true);
		CongHienStatus.text = string.Format(Localization.instance.Get("CongHienStatus"), GameManager.instance.m_GameClient.UserInfo.LienMinhCaNhan.CongHien.ToString());
		if (GameManager.instance.m_GameClient.UserInfo.LienMinhCaNhan.FreeCongHien)
		{
			DangHuongFree.text = Localization.instance.Get("MienPhi");
			DangHuongFree.transform.parent.Find("Background").GetComponent<UISprite>().spriteName = "button15";
		}
		else
		{
			DangHuongFree.transform.parent.Find("Background").GetComponent<UISprite>().spriteName = "button19";
			DangHuongFree.text = Localization.instance.Get("Xong");
		}
	}

	public void OnCongHienTab(bool onSelected)
	{
		if (!onSelected)
		{
			CongHienTab.isChecked = false;
			NhiemVuRoot.SetActive(false);
			return;
		}
		CongHienTab.isChecked = true;
		NhiemVuRoot.SetActive(true);
		CongHienStatus.text = string.Format(Localization.instance.Get("CongHienStatus"), GameManager.instance.m_GameClient.UserInfo.LienMinhCaNhan.CongHien.ToString());
		foreach (GameObject nhiemVuItem in NhiemVuItemList)
		{
			nhiemVuItem.SetActive(false);
		}
		int num = 0;
		for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.LienMinhCaNhan.NhiemVuList.Count; i++)
		{
			LienMinhNhiemVu lienMinhNhiemVu = GameManager.instance.m_GameClient.UserInfo.LienMinhCaNhan.NhiemVuList[i];
			if (!lienMinhNhiemVu.Active)
			{
				num++;
				continue;
			}
			GameObject gameObject;
			if (i - num >= NhiemVuItemList.Count)
			{
				Object obj = Object.Instantiate(NhiemVuItem);
				gameObject = (GameObject)((obj is GameObject) ? obj : null);
				NhiemVuItemList.Add(gameObject);
			}
			else
			{
				gameObject = NhiemVuItemList[i - num];
			}
			gameObject.SetActive(true);
			gameObject.transform.parent = NhiemVuRoot.transform;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localPosition = NhiemVuItemSize * (i - num) * Vector3.down;
			if (lienMinhNhiemVu.NhiemVuType == LienMinhNhiemVu.NHIEMVU.TRUY_TIM_BAO_VAT)
			{
				PhanThuongResponse.LoaiPhanThuong loaiPhanThuongFromCode = PhanThuongResponse.GetLoaiPhanThuongFromCode(lienMinhNhiemVu.NhiemVuDesc);
				if (loaiPhanThuongFromCode == PhanThuongResponse.LoaiPhanThuong.TRANG_BI)
				{
					foreach (UserInfo.TrangBiData trangBi in GameManager.instance.m_GameClient.UserInfo.TrangBiList)
					{
						if (trangBi.Name == lienMinhNhiemVu.NhiemVuDesc && trangBi.HID <= 0 && trangBi.Level == 1)
						{
							lienMinhNhiemVu.Count = 1;
						}
					}
				}
				if (loaiPhanThuongFromCode == PhanThuongResponse.LoaiPhanThuong.VO_CONG)
				{
					foreach (UserInfo.VoCongData voCong in GameManager.instance.m_GameClient.UserInfo.VoCongList)
					{
						if (voCong.Name == lienMinhNhiemVu.NhiemVuDesc && voCong.HID <= 0 && voCong.Level == 1)
						{
							lienMinhNhiemVu.Count = 1;
						}
					}
				}
			}
			gameObject.transform.Find("Desc").GetComponent<UILabel>().text = lienMinhNhiemVu.ClientDesc + "\n" + lienMinhNhiemVu.Count + "/" + lienMinhNhiemVu.YeuCau;
			gameObject.transform.Find("NhiemVuName").GetComponent<UILabel>().text = Localization.instance.Get(lienMinhNhiemVu.NhiemVuType.ToString());
			gameObject.GetComponent<NhiemVuLienMinhItem>().NhiemVuNo = lienMinhNhiemVu.NhiemVuNo;
		}
		if (num >= 5)
		{
			ResetItem.SetActive(true);
			ResetItem.transform.Find("Desc").GetComponent<UILabel>().text = Localization.instance.Get("ResetNhiemVuLienMinhDesc");
			ResetItem.transform.Find("Label").GetComponent<UILabel>().text = Localization.instance.Get("NhiemVuLienMinh");
		}
		else
		{
			ResetItem.SetActive(false);
			UILabel congHienStatus = CongHienStatus;
			congHienStatus.text = congHienStatus.text + "\n" + string.Format(Localization.instance.Get("ResetNhiemVuLienMinhDesc"));
		}
		NhiemVuRoot.GetComponent<UIDraggablePanel>().ResetPosition();
		NhiemVuItem.SetActive(false);
	}

	public void OnDangHuongFree()
	{
		DangHuongLienMinhRequest dangHuongLienMinhRequest = new DangHuongLienMinhRequest();
		dangHuongLienMinhRequest.CaoCap = false;
		GameManager.instance.m_GameClient.RequestDangHuongLienMinh(dangHuongLienMinhRequest);
	}

	public void OnDangHuongKNB()
	{
		DangHuongLienMinhRequest dangHuongLienMinhRequest = new DangHuongLienMinhRequest();
		dangHuongLienMinhRequest.CaoCap = true;
		GameManager.instance.m_GameClient.RequestDangHuongLienMinh(dangHuongLienMinhRequest);
	}

	public void OnResetNhiemVu()
	{
		ResetNhiemVuLienMinhRequest resetNhiemVuLienMinhRequest = new ResetNhiemVuLienMinhRequest();
		resetNhiemVuLienMinhRequest.ResetNhanh = true;
		GameManager.instance.m_GameClient.RequestResetNhiemVuLienMinh(resetNhiemVuLienMinhRequest);
	}

	public void OnDoiThuongTab(bool onSelected)
	{
		if (!onSelected)
		{
			DoiThuongTab.isChecked = false;
			DoiThuongGrp.SetActive(false);
			return;
		}
		DoiThuongTab.isChecked = true;
		DoiThuongGrp.SetActive(true);
		ScreenLienMinh.ScreenCongHienLienMinh.OnTrangBiTab(true);
		ScreenLienMinh.ScreenCongHienLienMinh.OnVoCongTab(false);
		ScreenLienMinh.ScreenCongHienLienMinh.OnNhanVatTab(false);
		ScreenLienMinh.ScreenCongHienLienMinh.OnOtherTab(false);
	}

	public void OnDoiThuongTab(bool onSelected, bool isInit)
	{
		if (!onSelected)
		{
			DoiThuongTab.isChecked = false;
			DoiThuongGrp.SetActive(false);
			return;
		}
		DoiThuongTab.isChecked = true;
		DoiThuongGrp.SetActive(true);
		if (isInit)
		{
			ScreenLienMinh.ScreenCongHienLienMinh.OnTrangBiTab(true);
			ScreenLienMinh.ScreenCongHienLienMinh.OnVoCongTab(false);
			ScreenLienMinh.ScreenCongHienLienMinh.OnNhanVatTab(false);
			ScreenLienMinh.ScreenCongHienLienMinh.OnOtherTab(false);
		}
		else
		{
			ScreenLienMinh.ScreenCongHienLienMinh.OnTrangBiTab(DoiThuongTabList[0].isChecked);
			ScreenLienMinh.ScreenCongHienLienMinh.OnVoCongTab(DoiThuongTabList[2].isChecked);
			ScreenLienMinh.ScreenCongHienLienMinh.OnNhanVatTab(DoiThuongTabList[1].isChecked);
			ScreenLienMinh.ScreenCongHienLienMinh.OnOtherTab(DoiThuongTabList[3].isChecked);
		}
	}

	public void OnTrangBiTab(bool onSelected)
	{
		DoiThuongTabList[0].isChecked = onSelected;
		if (!onSelected)
		{
			return;
		}
		DoiThuongRoot.GetComponent<UIDraggablePanel>().ResetPosition();
		foreach (GameObject doiThuongItem in DoiThuongItemList)
		{
			doiThuongItem.SetActive(false);
		}
		CongHienStatus.text = string.Format(Localization.instance.Get("CongHienStatus"), GameManager.instance.m_GameClient.UserInfo.LienMinhCaNhan.CongHien.ToString());
		int num = 0;
		DoiThuongItem.SetActive(true);
		foreach (KeyValuePair<string, ConfigManager.DoiThuongLienMinhCfg> item in ConfigManager.instance.DoiThuongLienMinhConfig)
		{
			string value = "DoiThuongCongHienLM" + item.Key;
			if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(value) && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(value) && !item.Value.CodeName.StartsWith("VC_") && !item.Value.CodeName.StartsWith("TH_") && !item.Value.CodeName.StartsWith("BAC") && !item.Value.CodeName.StartsWith("VP_") && !item.Value.CodeName.StartsWith("MVC_") && !item.Value.CodeName.StartsWith("TH_") && item.Value.TangKiemCacRequired <= GameManager.instance.m_GameClient.UserInfo.LienMinh.TangKiemCacLevel)
			{
				GameObject gameObject;
				if (num >= DoiThuongItemList.Count)
				{
					Object obj = Object.Instantiate(DoiThuongItem);
					gameObject = (GameObject)((obj is GameObject) ? obj : null);
					DoiThuongItemList.Add(gameObject);
				}
				else
				{
					gameObject = DoiThuongItemList[num];
				}
				gameObject.SetActive(true);
				gameObject.transform.parent = DoiThuongRoot.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = DoiThuongItemSize * num * Vector3.down;
				if (item.Value.CodeName.StartsWith("VC_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicVCs[item.Value.CodeName].TenHienThi;
				}
				else if (item.Value.CodeName.StartsWith("MVC_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicVCs[item.Value.CodeName.Replace("MVC_", "VC_")].TenHienThi;
				}
				else if (item.Value.CodeName.StartsWith("TH_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicNhanVats[item.Value.CodeName].TenHienThi;
				}
				else if (item.Value.CodeName.Contains("BAC"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = Localization.instance.Get("Bac");
				}
				else if (item.Value.CodeName.Contains("VP_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicVatPhamTieuThu[item.Value.CodeName].TenHienThi;
				}
				else if (item.Value.CodeName.StartsWith("MVK_") || item.Value.CodeName.StartsWith("MMU_") || item.Value.CodeName.StartsWith("MAG_") || item.Value.CodeName.StartsWith("MTS_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicTrangBi[item.Value.CodeName.Replace("MVK_", "VK_").Replace("MMU_", "MU_").Replace("MAG_", "AG")
						.Replace("MTS_", "TS_")].TenHienThi;
				}
				else
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicTrangBi[item.Value.CodeName].TenHienThi;
				}
				gameObject.transform.Find("Desc").GetComponent<UILabel>().text = string.Format(Localization.instance.Get("DoiThuongLienMinhDesc"), item.Value.SoLuong, item.Value.CongHien);
				if (item.Value.DuyNhat)
				{
					UILabel component = gameObject.transform.Find("Desc").GetComponent<UILabel>();
					component.text = component.text + "\n" + string.Format(Localization.instance.Get("DoiThuongLienMinhDuyNhat"));
				}
				else if (item.Value.SoLanTrongNgay > 0)
				{
					UILabel component2 = gameObject.transform.Find("Desc").GetComponent<UILabel>();
					component2.text = component2.text + "\n" + string.Format(Localization.instance.Get("DoiThuongLienMinhLimitNgay"), item.Value.SoLanTrongNgay);
				}
				UserInfo.TrangBiData trangBiData = new UserInfo.TrangBiData();
				trangBiData.Name = item.Value.CodeName;
				trangBiData.Level = 1;
				trangBiData.TinhLuyenLevel = 0;
				gameObject.transform.GetComponent<DoiThuongLienMinhItem>().item.Set(trangBiData);
				gameObject.transform.GetComponent<DoiThuongLienMinhItem>().nhanvat.gameObject.SetActive(false);
				gameObject.transform.GetComponent<DoiThuongLienMinhItem>().item.gameObject.SetActive(true);
				gameObject.transform.GetComponent<DoiThuongLienMinhItem>().DoiThuongCfg = item.Key;
				num++;
			}
		}
		DoiThuongRoot.GetComponent<UIDraggablePanel>().ResetPosition();
		DoiThuongItem.SetActive(false);
	}

	public void OnVoCongTab(bool onSelected)
	{
		DoiThuongTabList[2].isChecked = onSelected;
		if (!onSelected)
		{
			return;
		}
		DoiThuongRoot.GetComponent<UIDraggablePanel>().ResetPosition();
		foreach (GameObject doiThuongItem in DoiThuongItemList)
		{
			doiThuongItem.SetActive(false);
		}
		CongHienStatus.text = string.Format(Localization.instance.Get("CongHienStatus"), GameManager.instance.m_GameClient.UserInfo.LienMinhCaNhan.CongHien.ToString());
		int num = 0;
		DoiThuongItem.SetActive(true);
		foreach (KeyValuePair<string, ConfigManager.DoiThuongLienMinhCfg> item in ConfigManager.instance.DoiThuongLienMinhConfig)
		{
			string value = "DoiThuongCongHienLM" + item.Key;
			if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(value) && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(value) && item.Value.CodeName.Contains("VC_") && item.Value.TangKiemCacRequired <= GameManager.instance.m_GameClient.UserInfo.LienMinh.TangKiemCacLevel)
			{
				GameObject gameObject;
				if (num >= DoiThuongItemList.Count)
				{
					Object obj = Object.Instantiate(DoiThuongItem);
					gameObject = (GameObject)((obj is GameObject) ? obj : null);
					DoiThuongItemList.Add(gameObject);
				}
				else
				{
					gameObject = DoiThuongItemList[num];
				}
				gameObject.SetActive(true);
				gameObject.transform.parent = DoiThuongRoot.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = DoiThuongItemSize * num * Vector3.down;
				Debug.Log(item.Value.CodeName);
				if (item.Value.CodeName.StartsWith("VC_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicVCs[item.Value.CodeName].TenHienThi;
				}
				else if (item.Value.CodeName.StartsWith("MVC_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicVCs[item.Value.CodeName.Replace("MVC_", "VC_")].TenHienThi;
				}
				else if (item.Value.CodeName.StartsWith("TH_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicNhanVats[item.Value.CodeName].TenHienThi;
				}
				else if (item.Value.CodeName.Contains("BAC"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = Localization.instance.Get("Bac");
				}
				else if (item.Value.CodeName.Contains("VP_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicVatPhamTieuThu[item.Value.CodeName].TenHienThi;
				}
				else if (item.Value.CodeName.StartsWith("MVK_") || item.Value.CodeName.StartsWith("MMU_") || item.Value.CodeName.StartsWith("MAG_") || item.Value.CodeName.StartsWith("MTS_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicTrangBi[item.Value.CodeName.Replace("MVK_", "VK_").Replace("MMU_", "MU_").Replace("MAG_", "AG")
						.Replace("MTS_", "TS_")].TenHienThi;
				}
				else
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicTrangBi[item.Value.CodeName].TenHienThi;
				}
				gameObject.transform.Find("Desc").GetComponent<UILabel>().text = string.Format(Localization.instance.Get("DoiThuongLienMinhDesc"), item.Value.SoLuong, item.Value.CongHien);
				if (item.Value.DuyNhat)
				{
					UILabel component = gameObject.transform.Find("Desc").GetComponent<UILabel>();
					component.text = component.text + "\n" + string.Format(Localization.instance.Get("DoiThuongLienMinhDuyNhat"));
				}
				else if (item.Value.SoLanTrongNgay > 0)
				{
					UILabel component2 = gameObject.transform.Find("Desc").GetComponent<UILabel>();
					component2.text = component2.text + "\n" + string.Format(Localization.instance.Get("DoiThuongLienMinhLimitNgay"), item.Value.SoLanTrongNgay);
				}
				UserInfo.VoCongData voCongData = new UserInfo.VoCongData();
				voCongData.Name = item.Value.CodeName;
				voCongData.Level = 1;
				gameObject.transform.GetComponent<DoiThuongLienMinhItem>().item.Set(voCongData);
				gameObject.transform.GetComponent<DoiThuongLienMinhItem>().nhanvat.gameObject.SetActive(false);
				gameObject.transform.GetComponent<DoiThuongLienMinhItem>().item.gameObject.SetActive(true);
				gameObject.transform.GetComponent<DoiThuongLienMinhItem>().DoiThuongCfg = item.Key;
				num++;
			}
		}
		DoiThuongRoot.GetComponent<UIDraggablePanel>().ResetPosition();
		DoiThuongItem.SetActive(false);
	}

	public void OnNhanVatTab(bool onSelected)
	{
		DoiThuongTabList[1].isChecked = onSelected;
		if (!onSelected)
		{
			return;
		}
		DoiThuongRoot.GetComponent<UIDraggablePanel>().ResetPosition();
		foreach (GameObject doiThuongItem in DoiThuongItemList)
		{
			doiThuongItem.SetActive(false);
		}
		CongHienStatus.text = string.Format(Localization.instance.Get("CongHienStatus"), GameManager.instance.m_GameClient.UserInfo.LienMinhCaNhan.CongHien.ToString());
		int num = 0;
		DoiThuongItem.SetActive(true);
		foreach (KeyValuePair<string, ConfigManager.DoiThuongLienMinhCfg> item in ConfigManager.instance.DoiThuongLienMinhConfig)
		{
			string value = "DoiThuongCongHienLM" + item.Key;
			if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(value) && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(value) && item.Value.CodeName.StartsWith("TH_") && item.Value.TangKiemCacRequired <= GameManager.instance.m_GameClient.UserInfo.LienMinh.TangKiemCacLevel)
			{
				GameObject gameObject;
				if (num >= DoiThuongItemList.Count)
				{
					Object obj = Object.Instantiate(DoiThuongItem);
					gameObject = (GameObject)((obj is GameObject) ? obj : null);
					DoiThuongItemList.Add(gameObject);
				}
				else
				{
					gameObject = DoiThuongItemList[num];
				}
				gameObject.SetActive(true);
				gameObject.transform.parent = DoiThuongRoot.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = DoiThuongItemSize * num * Vector3.down;
				Debug.Log(item.Value.CodeName);
				if (item.Value.CodeName.StartsWith("VC_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicVCs[item.Value.CodeName].TenHienThi;
				}
				else if (item.Value.CodeName.StartsWith("MVC_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicVCs[item.Value.CodeName.Replace("MVC_", "VC_")].TenHienThi;
				}
				else if (item.Value.CodeName.StartsWith("TH_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicNhanVats[item.Value.CodeName.Replace("TH_", "NV_")].TenHienThi;
				}
				else if (item.Value.CodeName.Contains("BAC"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = Localization.instance.Get("Bac");
				}
				else if (item.Value.CodeName.Contains("VP_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicVatPhamTieuThu[item.Value.CodeName].TenHienThi;
				}
				else if (item.Value.CodeName.StartsWith("MVK_") || item.Value.CodeName.StartsWith("MMU_") || item.Value.CodeName.StartsWith("MAG_") || item.Value.CodeName.StartsWith("MTS_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicTrangBi[item.Value.CodeName.Replace("MVK_", "VK_").Replace("MMU_", "MU_").Replace("MAG_", "AG")
						.Replace("MTS_", "TS_")].TenHienThi;
				}
				else
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicTrangBi[item.Value.CodeName].TenHienThi;
				}
				gameObject.transform.Find("Desc").GetComponent<UILabel>().text = string.Format(Localization.instance.Get("DoiThuongLienMinhDesc"), item.Value.SoLuong, item.Value.CongHien);
				if (item.Value.DuyNhat)
				{
					UILabel component = gameObject.transform.Find("Desc").GetComponent<UILabel>();
					component.text = component.text + "\n" + string.Format(Localization.instance.Get("DoiThuongLienMinhDuyNhat"));
				}
				else if (item.Value.SoLanTrongNgay > 0)
				{
					UILabel component2 = gameObject.transform.Find("Desc").GetComponent<UILabel>();
					component2.text = component2.text + "\n" + string.Format(Localization.instance.Get("DoiThuongLienMinhLimitNgay"), item.Value.SoLanTrongNgay);
				}
				gameObject.transform.GetComponent<DoiThuongLienMinhItem>().item.gameObject.SetActive(false);
				gameObject.transform.GetComponent<DoiThuongLienMinhItem>().nhanvat.gameObject.SetActive(true);
				UserInfo.HonNhanVatData honNhanVatData = new UserInfo.HonNhanVatData();
				honNhanVatData.Name = item.Value.CodeName;
				honNhanVatData.Quantity = 30;
				gameObject.transform.GetComponent<DoiThuongLienMinhItem>().nhanvat.Set(honNhanVatData);
				gameObject.transform.GetComponent<DoiThuongLienMinhItem>().DoiThuongCfg = item.Key;
				num++;
			}
		}
		DoiThuongRoot.GetComponent<UIDraggablePanel>().ResetPosition();
		DoiThuongItem.SetActive(false);
	}

	public void OnOtherTab(bool onSelected)
	{
		DoiThuongTabList[3].isChecked = onSelected;
		if (!onSelected)
		{
			return;
		}
		DoiThuongRoot.GetComponent<UIDraggablePanel>().ResetPosition();
		foreach (GameObject doiThuongItem in DoiThuongItemList)
		{
			doiThuongItem.SetActive(false);
		}
		CongHienStatus.text = string.Format(Localization.instance.Get("CongHienStatus"), GameManager.instance.m_GameClient.UserInfo.LienMinhCaNhan.CongHien.ToString());
		int num = 0;
		DoiThuongItem.SetActive(true);
		foreach (KeyValuePair<string, ConfigManager.DoiThuongLienMinhCfg> item in ConfigManager.instance.DoiThuongLienMinhConfig)
		{
			string value = "DoiThuongCongHienLM" + item.Key;
			if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(value) && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(value) && !item.Value.CodeName.StartsWith("VC_") && !item.Value.CodeName.StartsWith("TH_") && !item.Value.CodeName.StartsWith("VK_") && !item.Value.CodeName.StartsWith("MU_") && !item.Value.CodeName.StartsWith("TS_") && !item.Value.CodeName.StartsWith("AG_") && !item.Value.CodeName.StartsWith("MVC_") && !item.Value.CodeName.StartsWith("MVK_") && !item.Value.CodeName.StartsWith("MMU_") && !item.Value.CodeName.StartsWith("MTS_") && !item.Value.CodeName.StartsWith("MAG_") && item.Value.TangKiemCacRequired <= GameManager.instance.m_GameClient.UserInfo.LienMinh.TangKiemCacLevel)
			{
				GameObject gameObject;
				if (num >= DoiThuongItemList.Count)
				{
					Object obj = Object.Instantiate(DoiThuongItem);
					gameObject = (GameObject)((obj is GameObject) ? obj : null);
					DoiThuongItemList.Add(gameObject);
				}
				else
				{
					gameObject = DoiThuongItemList[num];
				}
				gameObject.SetActive(true);
				gameObject.transform.parent = DoiThuongRoot.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = DoiThuongItemSize * num * Vector3.down;
				if (item.Value.CodeName.StartsWith("VC_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicVCs[item.Value.CodeName].TenHienThi;
				}
				else if (item.Value.CodeName.StartsWith("TH_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicNhanVats[item.Value.CodeName].TenHienThi;
				}
				else if (item.Value.CodeName.Contains("BAC"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = Localization.instance.Get("Bac");
				}
				else if (item.Value.CodeName.Contains("VP_"))
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicVatPhamTieuThu[item.Value.CodeName].TenHienThi;
				}
				else
				{
					gameObject.transform.Find("ItemName").GetComponent<UILabel>().text = ConfigManager.instance.m_dicTrangBi[item.Value.CodeName].TenHienThi;
				}
				gameObject.transform.Find("Desc").GetComponent<UILabel>().text = string.Format(Localization.instance.Get("DoiThuongLienMinhDesc"), item.Value.SoLuong, item.Value.CongHien);
				if (item.Value.DuyNhat)
				{
					UILabel component = gameObject.transform.Find("Desc").GetComponent<UILabel>();
					component.text = component.text + "\n" + string.Format(Localization.instance.Get("DoiThuongLienMinhDuyNhat"));
				}
				else if (item.Value.SoLanTrongNgay > 0)
				{
					UILabel component2 = gameObject.transform.Find("Desc").GetComponent<UILabel>();
					component2.text = component2.text + "\n" + string.Format(Localization.instance.Get("DoiThuongLienMinhLimitNgay"), item.Value.SoLanTrongNgay);
				}
				if (item.Value.CodeName != "BAC")
				{
					UserInfo.VatPhamTieuThuData vatPhamTieuThuData = new UserInfo.VatPhamTieuThuData();
					vatPhamTieuThuData.Name = item.Value.CodeName;
					vatPhamTieuThuData.Quantity = item.Value.SoLuong;
					gameObject.transform.GetComponent<DoiThuongLienMinhItem>().item.Set(vatPhamTieuThuData);
					gameObject.transform.GetComponent<DoiThuongLienMinhItem>().nhanvat.gameObject.SetActive(false);
					gameObject.transform.GetComponent<DoiThuongLienMinhItem>().item.gameObject.SetActive(true);
				}
				else
				{
					gameObject.transform.GetComponent<DoiThuongLienMinhItem>().nhanvat.gameObject.SetActive(false);
					gameObject.transform.GetComponent<DoiThuongLienMinhItem>().item.gameObject.SetActive(true);
					gameObject.transform.GetComponent<DoiThuongLienMinhItem>().item.SetBac(item.Value.SoLuong);
				}
				gameObject.transform.GetComponent<DoiThuongLienMinhItem>().DoiThuongCfg = item.Key;
				num++;
			}
		}
		DoiThuongRoot.GetComponent<UIDraggablePanel>().ResetPosition();
		DoiThuongItem.SetActive(false);
	}

	private void HelpLienMinh()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(8, 1);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
		EGDebug.Log("btnHelp_OnClick");
	}
}

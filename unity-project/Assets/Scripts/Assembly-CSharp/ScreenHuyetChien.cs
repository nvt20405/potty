using System.Collections.Generic;
using UnityEngine;

public class ScreenHuyetChien : ScreenBase
{
	public HuyetChienResponse response;

	public GameObject startGrp;

	public GameObject chonDoiThuGrp;

	public GameObject tangChiSoGrp;

	public GameObject nhanThuongGrp;

	public GameObject hoiSinhGrp;

	public GameObject endAutoGrp;

	public UILabel gia;

	public UISprite giatien;

	public UILabel aiLabel;

	public UILabel phanTramBuffLabel;

	public GameObject khieuChienGrp;

	public OtherAvatar[] phanThuongAvatars;

	public EGGUIGrid gridPhanthuong;

	public UILabel phanThuongDescLabel;

	public UISprite thuocTinh1;

	public UISprite thuocTinh2;

	public UISprite thuocTinh3;

	public GameObject btnThuocTinh1;

	public GameObject btnThuocTinh2;

	public GameObject btnThuocTinh3;

	public UILabel phanTramThuocTinh1;

	public UILabel phanTramThuocTinh2;

	public UILabel phanTramThuocTinh3;

	public UILabel saoCan1;

	public UILabel saoCan2;

	public UILabel saoCan3;

	public NhanVatAvatar doiThu1;

	public NhanVatAvatar doiThu2;

	public NhanVatAvatar doiThu3;

	public UILabel menhBuffLabel;

	public UILabel ngoaiBuffLabel;

	public UILabel thanBuffLabel;

	public UILabel khiBuffLabel;

	public UILabel saoDangCoLabel;

	public GameObject buffGrp;

	public GameObject saoDangCoGrp;

	public bool DaTungThua;

	public List<PhanThuongResponse.PhanThuong> listAllPhanThuong = new List<PhanThuongResponse.PhanThuong>();

	public override void OnActive()
	{
		base.OnActive();
		endAutoGrp.SetActive(false);
		GUIManager.ShowGadgets(6);
		if (GUIManager.instance.CurrentScreen != GAME_SCREEN.ScreenBattle)
		{
			GameManager.instance.m_GameClient.GetHuyetChienInfo();
		}
	}

	public override void OnDeactive()
	{
		if (GUIManager.instance.isAutoHacMocNhai)
		{
			endAutoHMN();
		}
		base.OnDeactive();
	}

	private void OnBangXepHangClick()
	{
		GameManager.instance.m_GameClient.GetTopHuyetChien();
	}

	private void OnAutoGoHMNClick()
	{
		listAllPhanThuong.Clear();
		PopupAutoHacMocNhai.Create();
	}

	private string GetSpriteNameThuocTinh(ChiSoCoBan thuocTinh)
	{
		switch (thuocTinh)
		{
		case ChiSoCoBan.Menh:
			return "menh";
		case ChiSoCoBan.Ngoai:
			return "ngoai";
		case ChiSoCoBan.Noi:
			return "khi";
		case ChiSoCoBan.ThanPhap:
			return "than";
		default:
			return string.Empty;
		}
	}

	private void OnStartHuyetChienClick()
	{
		GameManager.instance.m_GameClient.StartHuyetChien();
	}

	private void OnDoiThuDeClick()
	{
		GameManager.instance.m_GameClient.DanhHuyetChien(3);
	}

	private void OnDoiThuBtClick()
	{
		GameManager.instance.m_GameClient.DanhHuyetChien(2);
	}

	private void OnDoiThuKhoClick()
	{
		GameManager.instance.m_GameClient.DanhHuyetChien(1);
	}

	private void OnHoiSinhClick()
	{
		GameManager.instance.m_GameClient.HoiSinhHuyetChien();
	}

	private void OnChiSo1Click()
	{
		if (response != null)
		{
			GameManager.instance.m_GameClient.TangChiSoHuyetChien(response.HuyetChienInfo.TangThuocTinh1);
		}
	}

	private void OnChiSo2Click()
	{
		if (response != null)
		{
			GameManager.instance.m_GameClient.TangChiSoHuyetChien(response.HuyetChienInfo.TangThuocTinh2);
		}
	}

	private void OnChiSo3Click()
	{
		if (response != null)
		{
			GameManager.instance.m_GameClient.TangChiSoHuyetChien(response.HuyetChienInfo.TangThuocTinh3);
		}
	}

	private void OnNhanThuongClick()
	{
		GameManager.instance.m_GameClient.NhanThuongHuyetChien();
	}

	public void OnCloseBattleResult()
	{
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.instance.gameObject.SetActive(true);
		}
	}

	public void SyncWithNetworkData(HuyetChienResponse response)
	{
		if (response != null)
		{
			this.response = response;
			if (response.HuyetChienInfo.Level < 0)
			{
				khieuChienGrp.SetActive(true);
				startGrp.SetActive(true);
				buffGrp.SetActive(false);
				saoDangCoGrp.SetActive(false);
				phanTramBuffLabel.text = string.Format(Localization.instance.Get("HuyetChienStartBuff"), response.PhanTramBuff);
			}
			else
			{
				startGrp.SetActive(false);
				buffGrp.SetActive(true);
				saoDangCoGrp.SetActive(true);
				menhBuffLabel.text = string.Format("{0} %", response.HuyetChienInfo.TangMenh);
				ngoaiBuffLabel.text = string.Format("{0} %", response.HuyetChienInfo.TangNgoai);
				thanBuffLabel.text = string.Format("{0} %", response.HuyetChienInfo.TangThanPhap);
				khiBuffLabel.text = string.Format("{0} %", response.HuyetChienInfo.TangKhi);
				saoDangCoLabel.text = string.Format(Localization.instance.Get("HuyetChienSaoDangCoLabel"), response.HuyetChienInfo.SaoDangCo);
			}
			if (!response.HuyetChienInfo.NhanThuong && !GUIManager.instance.isAutoHacMocNhai)
			{
				nhanThuongGrp.SetActive(true);
				phanThuongDescLabel.text = string.Format(Localization.instance.Get("HuyetChienPhanThuongDesc"), response.HuyetChienInfo.Level);
				for (int i = 0; i < phanThuongAvatars.Length; i++)
				{
					if (i < response.PhanThuong.PhanThuongList.Count)
					{
						phanThuongAvatars[i].gameObject.SetActive(true);
						if (response.PhanThuong.PhanThuongList[i].Name == "BAC")
						{
							phanThuongAvatars[i].SetBac(response.PhanThuong.PhanThuongList[i].Count, true);
						}
						else if (response.PhanThuong.PhanThuongList[i].Name == "VANG")
						{
							phanThuongAvatars[i].SetVang(response.PhanThuong.PhanThuongList[i].Count, true);
						}
						else
						{
							phanThuongAvatars[i].Set(response.PhanThuong.PhanThuongList[i].Name, -1, -1, response.PhanThuong.PhanThuongList[i].Count);
						}
					}
					else
					{
						phanThuongAvatars[i].gameObject.SetActive(false);
					}
				}
				gridPhanthuong.Reposition();
			}
			else
			{
				nhanThuongGrp.SetActive(false);
			}
			if (!response.HuyetChienInfo.TangThuocTinh)
			{
				tangChiSoGrp.SetActive(true);
				chonDoiThuGrp.SetActive(false);
				thuocTinh1.spriteName = GetSpriteNameThuocTinh(response.HuyetChienInfo.TangThuocTinh1);
				thuocTinh2.spriteName = GetSpriteNameThuocTinh(response.HuyetChienInfo.TangThuocTinh2);
				thuocTinh3.spriteName = GetSpriteNameThuocTinh(response.HuyetChienInfo.TangThuocTinh3);
				btnThuocTinh1.collider.enabled = response.HuyetChienInfo.SaoDangCo >= ConfigManager.instance.HuyetChienConfig.SaoChiSo1;
				btnThuocTinh2.collider.enabled = response.HuyetChienInfo.SaoDangCo >= ConfigManager.instance.HuyetChienConfig.SaoChiSo2;
				btnThuocTinh3.collider.enabled = response.HuyetChienInfo.SaoDangCo >= ConfigManager.instance.HuyetChienConfig.SaoChiSo3;
				UIButton[] components = btnThuocTinh1.GetComponents<UIButton>();
				components[0].UpdateColor(btnThuocTinh1.collider.enabled, true);
				components[1].UpdateColor(btnThuocTinh1.collider.enabled, true);
				components[2].UpdateColor(btnThuocTinh1.collider.enabled, true);
				components = btnThuocTinh2.GetComponents<UIButton>();
				components[0].UpdateColor(btnThuocTinh2.collider.enabled, true);
				components[1].UpdateColor(btnThuocTinh2.collider.enabled, true);
				components[2].UpdateColor(btnThuocTinh2.collider.enabled, true);
				components = btnThuocTinh3.GetComponents<UIButton>();
				components[0].UpdateColor(btnThuocTinh3.collider.enabled, true);
				components[1].UpdateColor(btnThuocTinh3.collider.enabled, true);
				components[2].UpdateColor(btnThuocTinh3.collider.enabled, true);
				saoCan1.text = string.Format(Localization.instance.Get("HuyetChienCanSao"), ConfigManager.instance.HuyetChienConfig.SaoChiSo1);
				saoCan2.text = string.Format(Localization.instance.Get("HuyetChienCanSao"), ConfigManager.instance.HuyetChienConfig.SaoChiSo2);
				saoCan3.text = string.Format(Localization.instance.Get("HuyetChienCanSao"), ConfigManager.instance.HuyetChienConfig.SaoChiSo3);
				phanTramThuocTinh1.text = string.Format("+\n{0} %", ConfigManager.instance.HuyetChienConfig.PhanTramBuff1);
				phanTramThuocTinh2.text = string.Format("+\n{0} %", ConfigManager.instance.HuyetChienConfig.PhanTramBuff2);
				phanTramThuocTinh3.text = string.Format("+\n{0} %", ConfigManager.instance.HuyetChienConfig.PhanTramBuff3);
				aiLabel.text = string.Format(Localization.instance.Get("HuyetChienCuaAiNum"), response.HuyetChienInfo.Level);
				GetComponent<UIPanel>().Refresh();
			}
			else if (response.HuyetChienInfo.Level >= 0)
			{
				tangChiSoGrp.SetActive(false);
				chonDoiThuGrp.SetActive(true);
				doiThu1.Set(response.HuyetChienInfo.DoiThuDe.NPCTeam);
				doiThu2.Set(response.HuyetChienInfo.DoiThuBt.NPCTeam);
				doiThu3.Set(response.HuyetChienInfo.DoiThuKho.NPCTeam);
				aiLabel.text = string.Format(Localization.instance.Get("HuyetChienCuaAiNum"), response.HuyetChienInfo.Level + 1);
			}
			else
			{
				tangChiSoGrp.SetActive(false);
				chonDoiThuGrp.SetActive(false);
				aiLabel.text = string.Empty;
			}
			if (!response.HuyetChienInfo.IsAlive && response.HuyetChienInfo.Level >= 0)
			{
				hoiSinhGrp.SetActive(true);
				if (response.TienHoiSinh == "BAC")
				{
					giatien.spriteName = "icon_bac";
					gia.color = new Color(248f / 255f, 248f / 255f, 248f / 255f);
				}
				else
				{
					giatien.spriteName = "icon_vang";
					gia.color = new Color(250f / 255f, 208f / 255f, 48f / 255f);
				}
				gia.text = response.GiaTienHoiSinh.ToString();
			}
			else
			{
				hoiSinhGrp.SetActive(false);
			}
		}
		else
		{
			startGrp.SetActive(true);
			tangChiSoGrp.SetActive(false);
			hoiSinhGrp.SetActive(false);
			nhanThuongGrp.SetActive(false);
			chonDoiThuGrp.SetActive(false);
			khieuChienGrp.SetActive(false);
		}
	}

	public void OnStopAutoClick()
	{
		endAutoHMN();
	}

	public void startAutoGoHMN()
	{
		if (GUIManager.instance.autoHMN_ChiSoUuTien1 != ChiSoCoBan.None && GUIManager.instance.autoHMN_ChiSoUuTien2 != ChiSoCoBan.None && GUIManager.instance.autoHMN_ChiSoUuTien1 != GUIManager.instance.autoHMN_ChiSoUuTien2 && response != null)
		{
			GUIManager.instance.isAutoHacMocNhai = true;
			getChiSoSelected();
		}
	}

	private void getChiSoSelected()
	{
		if (!response.HuyetChienInfo.NhanThuong)
		{
			GameManager.instance.m_GameClient.NhanThuongHuyetChien();
		}
		else if (!response.HuyetChienInfo.TangThuocTinh)
		{
			ChiSoCoBan chiSoCoBan = ChiSoCoBan.None;
			if ((response.HuyetChienInfo.TangThuocTinh3 == GUIManager.instance.autoHMN_ChiSoUuTien1 || response.HuyetChienInfo.TangThuocTinh3 == GUIManager.instance.autoHMN_ChiSoUuTien2) && response.HuyetChienInfo.SaoDangCo >= ConfigManager.instance.HuyetChienConfig.SaoChiSo3)
			{
				chiSoCoBan = response.HuyetChienInfo.TangThuocTinh3;
			}
			if (chiSoCoBan != ChiSoCoBan.None)
			{
				GameManager.instance.m_GameClient.TangChiSoHuyetChien(chiSoCoBan);
				return;
			}
			if ((response.HuyetChienInfo.TangThuocTinh2 == GUIManager.instance.autoHMN_ChiSoUuTien1 || response.HuyetChienInfo.TangThuocTinh2 == GUIManager.instance.autoHMN_ChiSoUuTien2) && response.HuyetChienInfo.SaoDangCo >= ConfigManager.instance.HuyetChienConfig.SaoChiSo2)
			{
				chiSoCoBan = response.HuyetChienInfo.TangThuocTinh2;
			}
			if (chiSoCoBan != ChiSoCoBan.None)
			{
				GameManager.instance.m_GameClient.TangChiSoHuyetChien(chiSoCoBan);
				return;
			}
			chiSoCoBan = response.HuyetChienInfo.TangThuocTinh1;
			if (response.HuyetChienInfo.SaoDangCo >= ConfigManager.instance.HuyetChienConfig.SaoChiSo1)
			{
				GameManager.instance.m_GameClient.TangChiSoHuyetChien(chiSoCoBan);
			}
			else
			{
				endAutoHMN();
			}
		}
		else if (response.HuyetChienInfo.Level >= 0)
		{
			GameManager.instance.m_GameClient.DanhHuyetChien(1);
		}
	}

	public void endAutoHMN()
	{
		endAutoGrp.SetActive(false);
		if (listAllPhanThuong != null && listAllPhanThuong.Count > 0)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = listAllPhanThuong;
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongNhanDuoc"), phanThuongResponse);
		}
		GUIManager.instance.isAutoHacMocNhai = false;
		GUIManager.instance.autoHMN_ChiSoUuTien1 = (GUIManager.instance.autoHMN_ChiSoUuTien2 = ChiSoCoBan.None);
	}
}

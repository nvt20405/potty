using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class GadgetPanelBottom : MonoBehaviour
{
	public UISprite mainFocus;

	public UISprite monPhaiFocus;

	public UISprite hoatDongFocus;

	public UISprite kyNgoFocus;

	public UISprite choFocus;

	public UISprite bangHoiFocus;

	public UISprite mainBtnSprite;

	public GameObject monPhaiGrp;

	public GameObject hoatDongGrp;

	public GameObject kyNgoGrp;

	public GameObject choGrp;

	public GameObject bangHoiGrp;

	public GameObject KyNgoDragPanel;

	public GameObject btnKyNgo_Perfab;

	public UISprite doiHinhFocus;

	public UISprite chienThuatFocus;

	public UISprite tayNaiFocus;

	public UISprite deTuFocus;

	public UISprite trangBiFocus;

	public UISprite voCongFocus;

	public UISprite nguyenKhiFocus;

	public UISprite thanthuFocus;

	public UISprite sonMonFocus;

	public UISprite thanBinhFocus;

	public UISprite thienMaFocus;

	public UISprite boiduongTbFocus;

	public UISprite luanKiemFocus;

	public UISprite hacMocNhaiFocus;

	public UISprite dietMaFocus;

	public UISprite camDiaFocus;

	public UISprite chienTruongChinhTaFocus;

	public UISprite duocVienFocus;

	public UISprite leagueFocus;

	public UISprite quangMinhFocus;

	public UISprite thanthuDaoFocus;

	public UISprite napMayManFocus;

	public UISprite cuopMoFocus;

	public UISprite hoaVangFocus;

	public UISprite huyenKhiFocus;

	public UISprite tayVucFocus;

	public UISprite choDeTuFocus;

	public UISprite choVatPhamFocus;

	public UISprite choLeBaoFocus;

	public UISprite naptienFocus;

	public UISprite ruongthanbiFocus;

	public List<MenuButtonKyNgo> listKyNgoMenu = new List<MenuButtonKyNgo>();

	private List<GameObject> listBtnHoatDong = new List<GameObject>();

	public GameObject btnLuanKiem;

	public GameObject btnHacMocNhai;

	public GameObject btnDietMa;

	public GameObject btnCamDia;

	public GameObject btnChienTruongChinhTa;

	public GameObject notifySuKien;

	public GameObject notifyCho;

	public GameObject notifyChoDeTuBtn;

	public GameObject notifyChoLeBaoBtn;

	public GameObject btnNguyenKhi;

	private float panelMonPhaiposX;

	private float panelMonPhaicenterX;

	public GameObject goGiangHoParticle;

	private bool isOpenMain = true;

	public GameObject panelMonPhai;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
		setMainMenuFocus(null);
		setGroupFocus(null);
		setMonPhaiFocus(null);
		setHoatDongFocus(null);
		setChoFocus(null);
		if (panelMonPhai != null)
		{
			UIPanel component = panelMonPhai.GetComponent<UIPanel>();
			panelMonPhaiposX = panelMonPhai.transform.localPosition.x;
			panelMonPhaicenterX = component.clipRange.x;
		}
		displayGiangHoNotify();
	}

	private void displayGiangHoNotify()
	{
		if (!(goGiangHoParticle == null))
		{
			goGiangHoParticle.gameObject.SetActive(false);
			if (GameManager.instance.m_GameClient.UserInfo.GiangHo != null && GameManager.instance.m_GameClient.UserInfo.GiangHo.Count < 5 && isOpenMain)
			{
				goGiangHoParticle.gameObject.SetActive(true);
				goGiangHoParticle.GetComponent<ParticleSystem>().Simulate(0f, true, true);
				goGiangHoParticle.GetComponent<ParticleSystem>().Play();
			}
		}
	}

	public void SetFocusOnScreen(GAME_SCREEN screen)
	{
		if (!(mainBtnSprite == null))
		{
			if (screen == GAME_SCREEN.ScreenMain)
			{
				mainBtnSprite.spriteName = "btn_HanhTau";
			}
			else
			{
				mainBtnSprite.spriteName = "btn_Home";
			}
			if (screen == GAME_SCREEN.ScreenMain || screen == GAME_SCREEN.ScreenWorldmap)
			{
				setMainMenuFocus(null);
			}
		}
	}

	public void OnClickBtnHome()
	{
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenMain)
		{
			isOpenMain = false;
			GUIManager.setScreen(GAME_SCREEN.ScreenWorldmap);
		}
		else
		{
			isOpenMain = true;
			GUIManager.setScreen(GAME_SCREEN.ScreenMain);
		}
		displayGiangHoNotify();
		setGroupFocus(null);
	}

	private void OnClickBtnMonPhai()
	{
		if (GameManager.instance.m_GameClient.isIpV6)
		{
			MessagePopup.Create(Localization.instance.Get("ErrorIPV6Mess"));
			return;
		}
		if (TutorialPopup.instance != null)
		{
			if (panelMonPhai != null)
			{
				UIPanel component = panelMonPhai.GetComponent<UIPanel>();
				panelMonPhai.transform.localPosition = new Vector3(0f, panelMonPhai.transform.localPosition.y, panelMonPhai.transform.localPosition.z);
				component.clipRange = new Vector4(0f, component.clipRange.y, component.clipRange.z, component.clipRange.w);
			}
			TutorialPopup.instance.ShowNextTutorial();
		}
		else if (panelMonPhai != null)
		{
			UIPanel component2 = panelMonPhai.GetComponent<UIPanel>();
			panelMonPhai.transform.localPosition = new Vector3(panelMonPhaiposX, panelMonPhai.transform.localPosition.y, panelMonPhai.transform.localPosition.z);
			component2.clipRange = new Vector4(panelMonPhaicenterX, component2.clipRange.y, component2.clipRange.z, component2.clipRange.w);
		}
		setGroupFocus(monPhaiGrp);
		setMainMenuFocus(monPhaiFocus);
		setMonPhaiFocus(null);
	}

	private void GetListBtnHoatDong()
	{
		Vector3 zero = Vector3.zero;
		Vector3 vector = default(Vector3);
		vector = new Vector3(150f, 0f, 0f);
		if (btnLuanKiem != null)
		{
			btnLuanKiem.transform.localPosition = zero;
			zero += vector;
		}
		if (btnHacMocNhai != null)
		{
			btnHacMocNhai.transform.localPosition = zero;
			zero += vector;
		}
		if (btnDietMa != null)
		{
			btnDietMa.transform.localPosition = zero;
			zero += vector;
		}
		if (btnCamDia != null)
		{
			btnCamDia.transform.localPosition = zero;
			zero += vector;
		}
		if (btnChienTruongChinhTa != null)
		{
			btnChienTruongChinhTa.transform.localPosition = zero;
			zero += vector;
		}
	}

	private void OnClickBtnHoatDong()
	{
		if (GameManager.instance.m_GameClient.isIpV6)
		{
			MessagePopup.Create(Localization.instance.Get("ErrorIPV6Mess"));
			return;
		}
		GetListBtnHoatDong();
		setGroupFocus(hoatDongGrp);
		setMainMenuFocus(hoatDongFocus);
		setHoatDongFocus(null);
	}

	private void OnClickBtnKyNgo()
	{
		if (GameManager.instance.m_GameClient.isIpV6)
		{
			MessagePopup.Create(Localization.instance.Get("ErrorIPV6Mess"));
			return;
		}
		getListBtnKyNgo();
		setGroupFocus(kyNgoGrp);
		setMainMenuFocus(kyNgoFocus);
	}

	private void OnClickBtnCho()
	{
		if (GameManager.instance.m_GameClient.isIpV6)
		{
			PopUpNapTien.Create();
			return;
		}
		setGroupFocus(choGrp);
		setMainMenuFocus(choFocus);
		setChoFocus(null);
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		if (checkThongBaoCho() && notifyChoDeTuBtn != null)
		{
			notifyChoDeTuBtn.gameObject.SetActive(true);
		}
		if (checkThongBaoLeBao() && notifyChoLeBaoBtn != null)
		{
			notifyChoLeBaoBtn.gameObject.SetActive(true);
		}
	}

	private void OnClickBtnBangHoi()
	{
		if (GameManager.instance.m_GameClient.isIpV6)
		{
			MessagePopup.Create(Localization.instance.Get("ErrorIPV6Mess"));
			return;
		}
		string value = "lienminh;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		setGroupFocus(bangHoiGrp);
		setMainMenuFocus(bangHoiFocus);
		GameManager.instance.m_GameClient.RequestUpdateLienMinhInfo();
		ScreenLienMinh.ScreenStatus = "UpdateInfo";
	}

	private void getListBtnKyNgo()
	{
		bool flag = false;
		foreach (Transform item in KyNgoDragPanel.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		listKyNgoMenu.Clear();
		Vector3 vector = default(Vector3);
		vector = new Vector3(-213f, 0f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(150f, 0f, 0f);
		int num = 0;
		int num2 = 1;
		int num3 = 2;
		int num4 = 3;
		int num5 = 4;
		int num6 = 5;
		int num7 = 6;
		int num8 = 7;
		int num9 = 8;
		int num10 = 9;
		int num11 = 10;
		int num12 = 11;
		int num13 = 12;
		int num14 = 13;
		int num15 = 14;
		int num16 = 15;
		int num17 = 16;
		int num18 = 17;
		int num19 = 18;
		int num20 = 19;
		int num21 = 20;
		int num22 = 21;
		int num23 = 22;
		int num24 = 23;
		int num25 = 24;
		int num26 = 25;
		int num27 = 26;
		for (int i = 0; i < 27; i++)
		{
			MenuButtonKyNgo menuButtonKyNgo = null;
			if (i == num && checkDisplayDiHoaCungMenu())
			{
				flag = true;
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.DiHoaCung);
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnDiHoaCungBtnClick;
			}
			if (i == num2 && checkThuongNapMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.ThuongNap);
				if (flag)
				{
					vector += vector2;
				}
				else
				{
					flag = true;
				}
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnThuongNapBtnClick;
			}
			if (i == num3 && checkULinhSonTrangMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.SonTrang);
				if (flag)
				{
					vector += vector2;
				}
				else
				{
					flag = true;
				}
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnSonTrangBtnClick;
			}
			if (i == num4)
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.TheLuc);
				if (flag)
				{
					vector += vector2;
				}
				else
				{
					flag = true;
				}
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnTheLucBtnClick;
			}
			if (i == num5)
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.ThamBai);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnThamBaiBtnClick;
			}
			if (i == num6 && checkDisplayHuaNguyenMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.HuaNguyen);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnHuaNguyenBtnClick;
			}
			if (i == num7 && checkDisplayThangCapMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.ThangCap);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnThangCapBtnClick;
			}
			if (i == num8 && checkDisplayDNNhanThuongMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.DangNhapNhanThuong);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnDangNhapNhanThuongBtnClick;
			}
			if (i == num9 && checkDisplayThanTaiMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.ThanTai);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnThanTaiBtnClick;
			}
			if (i == num10 && checkDisplayDanhBacMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.DanhBac);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnDanhBacBtnClick;
			}
			if (i == num11 && checkDisplayCuuTieuPhongMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.CuuTieuPhong);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnHiepKhachHanhBtnClick;
			}
			if (i == num12 && checkDisplayRuongTSMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.RuongThachSanh);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnThachSanhBtnClick;
			}
			if (i == num13 && checkDisplayUongRuouMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.UongRuou);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnUongRuouBtnClick;
			}
			if (i == num14 && checkDisplayDoiDoMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.DoiDo);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnDoiDoBtnClick;
			}
			if (i == num15 && checkDisplayBanhChungMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.BanhChung);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnBanhChungBtnClick;
			}
			if (i == num16 && checkDisplayTichLuyNapMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.TichLuyNap);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnTichLuyNapBtnClick;
			}
			if (i == num17 && checkDisplayTichLuyTieuMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.TichLuyTieu);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnTichLuyTieuBtnClick;
			}
			if (i == num18 && checkDisplayPhaoHoaMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.PhaoHoa);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnPhaoHoaBtnClick;
			}
			if (i == num19 && checkDisplayDapNieuMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.DapNieu);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnDapNieuBtnClick;
			}
			if (i == num20 && checkDisplayVongQuayMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.VongQuay);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnVongQuayBtnClick;
			}
			if (i == num21 && checkDisplayGuiTietKiemMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.GuiTietKiem);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnGuiTietKiemBtnClick;
			}
			if (i == num22 && checkDisplayBacMayManMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.BacMayMan);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnBacMayManBtnClick;
			}
			if (i == num23 && checkDisplayTuBaoBonMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.TuBaoBon);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnTuBaoBonBtnClick;
			}
			if (i == num24 && checkDisplayBaoRuongMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.BaoRuong);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnBaoRuongBtnClick;
			}
			if (i == num25 && checkDisplayNapHangNgayMenu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.TichLuyNapHangNgay);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnNapHangNgayBtnClick;
			}
			if (i == num26 && checkDisplayAnTheCaoThu())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.AnTheCaoThu);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnAnTheCaoThuBtnClick;
			}
			if (i == num27 && checkDisplayCamCungBiBao())
			{
				menuButtonKyNgo = ((GameObject)UnityEngine.Object.Instantiate(btnKyNgo_Perfab)).GetComponent<MenuButtonKyNgo>();
				menuButtonKyNgo.setID(MenuButtonKyNgo.KyNgoType.CamCung);
				vector += vector2;
				UIEventListener.Get(menuButtonKyNgo.gameObject).onClick = OnCamCungBtnClick;
			}
			if (menuButtonKyNgo != null)
			{
				menuButtonKyNgo.transform.parent = KyNgoDragPanel.transform;
				menuButtonKyNgo.transform.localScale = new Vector3(1f, 1f, 1f);
				menuButtonKyNgo.transform.localPosition = vector;
				listKyNgoMenu.Add(menuButtonKyNgo);
			}
		}
		if (listKyNgoMenu != null && listKyNgoMenu.Count > 0)
		{
			for (int j = 0; j < listKyNgoMenu.Count; j++)
			{
				listKyNgoMenu[j].isSelected(false);
			}
		}
	}

	public bool checkDisplayDiHoaCungMenu()
	{
		UserInfo.ServerData.DiHoaCungCfg diHoaCungConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DiHoaCungConfig;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (diHoaCungConfig != null)
		{
			DateTime thoiGianBatDau = diHoaCungConfig.ThoiGianBatDau;
			DateTime dateTime = diHoaCungConfig.ThoiGianKetThuc.AddDays(2.0);
			if (serverTime >= thoiGianBatDau && serverTime <= dateTime)
			{
				return true;
			}
			return false;
		}
		return false;
	}

	public bool checkThuongNapMenu()
	{
		UserInfo.ServerData.ThuongNapCfg thuongNapConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.ThuongNapConfig;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (thuongNapConfig != null)
		{
			DateTime thoiGianBatDau = thuongNapConfig.ThoiGianBatDau;
			DateTime thoiGianKetThuc = thuongNapConfig.ThoiGianKetThuc;
			if (serverTime <= thoiGianKetThuc && serverTime >= thoiGianBatDau)
			{
				return true;
			}
			return false;
		}
		return false;
	}

	public bool checkULinhSonTrangMenu()
	{
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.ULinhCfg != null)
		{
			DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.ULinhCfg.ThoiGianBatDau;
			DateTime dateTime = GameManager.instance.m_GameClient.UserInfo.ServerInfo.ULinhCfg.ThoiGianKetThuc.AddDays(2.0);
			if (serverTime >= thoiGianBatDau && serverTime <= dateTime)
			{
				return true;
			}
			return false;
		}
		return false;
	}

	public bool checkDisplayDNNhanThuongMenu()
	{
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		DateTime registerTime = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.registerTime;
		DateTime dateTime = new DateTime(registerTime.Year, registerTime.Month, registerTime.Day);
		int num = (int)(serverTime - dateTime).TotalDays;
		string value = "DangNhapNhanThuong;";
		EGDebug.Log("day_span: " + num);
		if ((num == ConfigManager.instance.OtherConfig.DangNhapNhanThuong.Count - 1 && GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(value)) || num >= ConfigManager.instance.OtherConfig.DangNhapNhanThuong.Count)
		{
			return false;
		}
		return true;
	}

	private bool checkDisplayUongRuouMenu()
	{
		string value = "UongRuou;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			return false;
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level >= 22)
		{
			return true;
		}
		return false;
	}

	private bool checkDisplayDoiDoMenu()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg != null)
		{
			DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
			DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.ThoiGianBatDau;
			DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.ThoiGianKetThuc;
			if (serverTime >= thoiGianBatDau && serverTime <= thoiGianKetThuc)
			{
				return true;
			}
		}
		return false;
	}

	private bool checkDisplayBanhChungMenu()
	{
		BanhChungConfig banhChungCfg = GameManager.instance.m_GameClient.UserInfo.ServerInfo.BanhChungCfg;
		if (banhChungCfg != null)
		{
			DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
			if (serverTime > banhChungCfg.NgayBatDau && serverTime < banhChungCfg.NgayKetThuc)
			{
				return true;
			}
		}
		return false;
	}

	private bool checkDisplayDanhBacMenu()
	{
		string value = "DanhBac;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			return false;
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level >= 9)
		{
			return true;
		}
		return false;
	}

	private bool checkDisplayHuaNguyenMenu()
	{
		if (GameManager.instance.m_GameClient.UserInfo.DanhSon != null && GameManager.instance.m_GameClient.UserInfo.DanhSon.Count > 0 && GameManager.instance.m_GameClient.UserInfo.DanhSon[0].LuotChoi > 0)
		{
			return true;
		}
		return false;
	}

	private bool checkDisplayCuuTieuPhongMenu()
	{
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		DateTime dateTime = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.registerTime.AddDays(14.0);
		TimeSpan timeSpan = dateTime - serverTime;
		if (timeSpan.Days <= 14 && timeSpan.Seconds > 0 && timeSpan.Minutes >= 0 && timeSpan.Hours >= 0 && ConfigManager.instance.OtherConfig.CuuVienTieuPhong != null && ConfigManager.instance.OtherConfig.CuuVienTieuPhong.Count > 0)
		{
			for (int i = 0; i < ConfigManager.instance.OtherConfig.CuuVienTieuPhong.Count; i++)
			{
				string value = string.Format("CuuTieuPhong{0}", i);
				if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(value))
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool checkDisplayThanTaiMenu()
	{
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		DateTime dateTime = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.registerTime.AddDays(5.0);
		TimeSpan timeSpan = dateTime - serverTime;
		if (timeSpan.Days <= 4 && timeSpan.Seconds > 0 && timeSpan.Minutes >= 0 && timeSpan.Hours >= 0)
		{
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(string.Format("ThanTai{0};", 50)) && GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(string.Format("ThanTai{0};", 300)) && GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(string.Format("ThanTai{0};", 2000)) && GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(string.Format("ThanTai{0};", 10000)))
			{
				return false;
			}
			return true;
		}
		return false;
	}

	private bool checkDisplayThangCapMenu()
	{
		if (ConfigManager.instance.OtherConfig.LenCapNhanThuong == null)
		{
			return false;
		}
		for (int i = 0; i < ConfigManager.instance.OtherConfig.LenCapNhanThuong.Count; i++)
		{
			string value = string.Format("ThuongLenLvl{0}", ConfigManager.instance.OtherConfig.LenCapNhanThuong[i].CapYeuCau);
			if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(value))
			{
				return true;
			}
		}
		return false;
	}

	public bool checkDisplayRuongTSMenu()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.RuongCfg == null)
		{
			return false;
		}
		DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.RuongCfg.ThoiGianBatDau;
		DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.RuongCfg.ThoiGianKetThuc;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (serverTime <= thoiGianKetThuc && serverTime >= thoiGianBatDau)
		{
			return true;
		}
		return false;
	}

	public bool checkDisplayTichLuyNapMenu()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyNapConfig == null)
		{
			return false;
		}
		DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyNapConfig.ThoiGianBatDau;
		DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyNapConfig.ThoiGianKetThuc;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (serverTime <= thoiGianKetThuc && serverTime >= thoiGianBatDau)
		{
			return true;
		}
		return false;
	}

	public bool checkDisplayTichLuyTieuMenu()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyTieuConfig == null)
		{
			return false;
		}
		DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyTieuConfig.ThoiGianBatDau;
		DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyTieuConfig.ThoiGianKetThuc;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (serverTime <= thoiGianKetThuc && serverTime >= thoiGianBatDau)
		{
			return true;
		}
		return false;
	}

	private bool checkDisplayPhaoHoaMenu()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.PhaoHoaInfo == null)
		{
			return false;
		}
		DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.PhaoHoaInfo.ThoiGianBatDau;
		DateTime thoiGianHideGUI = GameManager.instance.m_GameClient.UserInfo.ServerInfo.PhaoHoaInfo.ThoiGianHideGUI;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (serverTime <= thoiGianHideGUI && serverTime >= thoiGianBatDau)
		{
			return true;
		}
		return false;
	}

	public bool checkDisplayVongQuayMenu()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.VongQuayInfo == null)
		{
			return false;
		}
		DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.VongQuayInfo.ThoiGianKetThuc;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (serverTime <= thoiGianKetThuc)
		{
			return true;
		}
		return false;
	}

	public bool checkDisplayGuiTietKiemMenu()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.GuiTietKiemConfig == null)
		{
			return false;
		}
		DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.GuiTietKiemConfig.ThoiGianKetThuc;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (serverTime <= thoiGianKetThuc || GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains("GuiTK;"))
		{
			int num = 0;
			for (int i = 0; i < ConfigManager.instance.OtherConfig.GuiTietKiem.Count; i++)
			{
				if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains("GuiTK" + i + ";"))
				{
					num++;
				}
			}
			if (num < ConfigManager.instance.OtherConfig.GuiTietKiem.Count)
			{
				return true;
			}
			return false;
		}
		return false;
	}

	private bool checkDisplayDapNieuMenu()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.EventDapNieuConfig == null)
		{
			return false;
		}
		DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.EventDapNieuConfig.ThoiGianBatDau;
		DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.EventDapNieuConfig.ThoiGianKetThuc;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (serverTime <= thoiGianKetThuc && serverTime >= thoiGianBatDau)
		{
			return true;
		}
		return false;
	}

	private bool checkDisplayBacMayManMenu()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.BacMayManConfig == null)
		{
			return false;
		}
		DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.BacMayManConfig.ThoiGianBatDau;
		DateTime date = thoiGianBatDau.AddDays(5.0).Date;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (serverTime <= date && serverTime >= thoiGianBatDau)
		{
			return true;
		}
		return false;
	}

	public bool checkDisplayTuBaoBonMenu()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.TuBaoBonConfig == null)
		{
			return false;
		}
		DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TuBaoBonConfig.ThoiGianBatDau;
		DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TuBaoBonConfig.ThoiGianKetThuc;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (serverTime <= thoiGianKetThuc && serverTime >= thoiGianBatDau)
		{
			return true;
		}
		return false;
	}

	public bool checkDisplayBaoRuongMenu()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.TopBaoRuongConfig == null)
		{
			return false;
		}
		DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TopBaoRuongConfig.ThoiGianBatDau;
		DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TopBaoRuongConfig.ThoiGianKetThuc;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (serverTime <= thoiGianKetThuc && serverTime >= thoiGianBatDau)
		{
			return true;
		}
		return false;
	}

	public bool checkDisplayNapHangNgayMenu()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyNapHangNgayConfig == null)
		{
			return false;
		}
		DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyNapHangNgayConfig.ThoiGianBatDau;
		DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyNapHangNgayConfig.ThoiGianKetThuc;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (serverTime <= thoiGianKetThuc && serverTime >= thoiGianBatDau)
		{
			return true;
		}
		return false;
	}

	public bool checkDisplayAnTheCaoThu()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.AnTheCaoThuConfig == null)
		{
			return false;
		}
		DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.AnTheCaoThuConfig.ThoiGianBatDau;
		DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.AnTheCaoThuConfig.ThoiGianKetThuc;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (serverTime <= thoiGianKetThuc && serverTime >= thoiGianBatDau)
		{
			return true;
		}
		return false;
	}

	public bool checkDisplayCamCungBiBao()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.CamCungBiBaoConfig == null)
		{
			return false;
		}
		DateTime thoiGianBatDau = GameManager.instance.m_GameClient.UserInfo.ServerInfo.CamCungBiBaoConfig.ThoiGianBatDau;
		DateTime thoiGianKetThuc = GameManager.instance.m_GameClient.UserInfo.ServerInfo.CamCungBiBaoConfig.ThoiGianKetThuc;
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if (serverTime <= thoiGianKetThuc && serverTime >= thoiGianBatDau)
		{
			return true;
		}
		return false;
	}

	private void OnDoiHinhBtnClick()
	{
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		setMonPhaiFocus(doiHinhFocus);
		ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
		if (screenDoiHinh != null)
		{
			screenDoiHinh.m_iCurrentSelectIdx = 0;
		}
		GUIManager.setScreen(GAME_SCREEN.ScreenDoiHinh);
	}

	private void OnDeTuBtnClick()
	{
		setMonPhaiFocus(deTuFocus);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDeTu);
	}

	private void OnVoCongBtnClick()
	{
		setMonPhaiFocus(voCongFocus);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenVoCong);
	}

	private void OnTrangBiBtnClick()
	{
		setMonPhaiFocus(trangBiFocus);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTrangBi);
	}

	private void OnTayNaiBtnClick()
	{
		setMonPhaiFocus(tayNaiFocus);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTayNai);
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
	}

	private void OnChienThuatBtnClick()
	{
		setMonPhaiFocus(chienThuatFocus);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenChienThuat);
	}

	private void OnNguyenKhiBtnClick()
	{
		string value = "NguyenKhi";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level < 28)
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoNguyenKhiChuaMo"));
			return;
		}
		setMonPhaiFocus(nguyenKhiFocus);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenNguyenKhi);
	}

	private void OnThanThuBtnClick()
	{
		string value = "ThanThu;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level < 0)
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("ChuaDuLevel"), 0));
			return;
		}
		setMonPhaiFocus(thanthuFocus);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDanhSachThanThu);
	}

	private void OnThienMaBtnClick()
	{
		string value = "ThienMa;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level < 0)
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("ChuaDuLevel"), 0));
			return;
		}
		setMonPhaiFocus(thienMaFocus);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenThienMaMain);
	}

	private void OnSonMonBtnClick()
	{
		string value = "SonMon;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level < ConfigManager.instance.SonMonConfig.LevelReq)
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("ChuaDuLevel"), ConfigManager.instance.SonMonConfig.LevelReq));
			return;
		}
		if (GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran.Count < 8 || GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran.Contains(-1) || GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran.Contains(0))
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("DKSonMon8Hero"), ConfigManager.instance.SonMonConfig.LevelReq));
			return;
		}
		setMonPhaiFocus(sonMonFocus);
		if (GameManager.instance.m_GameClient.UserInfo.SonMon == null)
		{
			GameManager.instance.m_GameClient.RequestGetSonMonInfo();
		}
		else
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenSonMonMain);
		}
	}

	private void OnThanBinhBtnClick()
	{
		string value = "ThanBinh;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		setMonPhaiFocus(thanBinhFocus);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenThanBinh);
	}

	private void OnBoiDuongTBBtnClick()
	{
		string value = "BoiDuongTrangBi;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		bool flag = false;
		if (GameManager.instance.m_GameClient.UserInfo.TrangBiList != null && GameManager.instance.m_GameClient.UserInfo.TrangBiList.Count > 0)
		{
			for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.TrangBiList.Count; i++)
			{
				if (GameManager.instance.m_GameClient.UserInfo.TrangBiList[i].HoangKim >= 1)
				{
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			MessagePopup.Create(Localization.instance.Get("ChuaMoTinhNangLuyenHoa"));
			return;
		}
		setMonPhaiFocus(boiduongTbFocus);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenListTrangBiHoangKim);
	}

	private void OnChienTruongChinhTaBtnClick()
	{
		string value = "ChienTruong";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		if (GameManager.instance != null && GameManager.instance.m_GameClient != null && GameManager.instance.m_GameClient.UserInfo != null && GameManager.instance.m_GameClient.UserInfo.Gamer != null && GameManager.instance.m_GameClient.UserInfo.Gamer.Level < 25)
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("CT2InvalidLevel"), 25));
			return;
		}
		setHoatDongFocus(chienTruongChinhTaFocus);
		GUIManager.setScreen(GAME_SCREEN.ScreenCT2HoatDong);
	}

	private void OnLuanKiemBtnClick()
	{
		if (GameManager.instance != null && GameManager.instance.m_GameClient != null && GameManager.instance.m_GameClient.UserInfo != null && GameManager.instance.m_GameClient.UserInfo.Gamer != null && GameManager.instance.m_GameClient.UserInfo.Gamer.Level < ConfigManager.LevelUnlockLuanKiem())
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("LuanKiemInvalidLevel"), ConfigManager.LevelUnlockLuanKiem()));
			return;
		}
		setHoatDongFocus(luanKiemFocus);
		GUIManager.setScreen(GAME_SCREEN.ScreenLuanKiem);
	}

	private void OnDanhSonBtnClick()
	{
		int giangHoIdxFromDanhSonIdx = ConfigManager.instance.GetGiangHoIdxFromDanhSonIdx(0);
		if (GameManager.instance != null && GameManager.instance.m_GameClient != null && GameManager.instance.m_GameClient.UserInfo != null && GameManager.instance.m_GameClient.UserInfo.GiangHo != null && GameManager.instance.m_GameClient.UserInfo.GiangHo.Count > giangHoIdxFromDanhSonIdx && GameManager.instance.m_GameClient.UserInfo.GiangHo[giangHoIdxFromDanhSonIdx].HoanThanh > 0)
		{
			setHoatDongFocus(camDiaFocus);
			GUIManager.setScreen(GAME_SCREEN.ScreenDanhSon);
		}
		else
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("DanhSonChuaMoLanDau"), ConfigManager.instance.m_listGiangHo[giangHoIdxFromDanhSonIdx].TenHienThi));
		}
	}

	private void OnHuyetChienBtnClick()
	{
		if (GameManager.instance != null && GameManager.instance.m_GameClient != null && GameManager.instance.m_GameClient.UserInfo != null && GameManager.instance.m_GameClient.UserInfo.Gamer != null && GameManager.instance.m_GameClient.UserInfo.Gamer.Level < 10)
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("HuyetChienChuaDuLevel"), 10));
			return;
		}
		setHoatDongFocus(hacMocNhaiFocus);
		GUIManager.setScreen(GAME_SCREEN.ScreenHuyetChien);
	}

	private void OnDongNhanBtnClick()
	{
		if (GameManager.instance != null && GameManager.instance.m_GameClient != null && GameManager.instance.m_GameClient.UserInfo != null && GameManager.instance.m_GameClient.UserInfo.Gamer != null && GameManager.instance.m_GameClient.UserInfo.Gamer.Level < 12)
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("DongNhanChuaDuLevel"), 12));
			return;
		}
		setHoatDongFocus(dietMaFocus);
		GUIManager.setScreen(GAME_SCREEN.ScreenDongNhan);
	}

	private void OnDuocVienBtnClick()
	{
		string value = "NguyenKhi";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
		}
		else if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level >= 28)
		{
			setHoatDongFocus(duocVienFocus);
			GUIManager.setScreen(GAME_SCREEN.ScreenLienMinhTrongCay);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangDuocVienChuaMo"));
		}
	}

	private void OnQuangMinhDinhBtnClick()
	{
		string value = "QuangMinhDinh;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		int num = 30;
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level >= num)
		{
			setHoatDongFocus(quangMinhFocus);
			GUIManager.setScreen(GAME_SCREEN.ScreenQuangMinhDinh);
		}
		else
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("ChuaDuLevel"), num));
		}
	}

	private void OnThanThuDaoBtnClick()
	{
		string value = "ThanThu;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		int num = 0;
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level >= num)
		{
			setHoatDongFocus(thanthuDaoFocus);
			GUIManager.setScreen(GAME_SCREEN.ScreenBatThanThu);
		}
		else
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("ChuaDuLevel"), num));
		}
	}

	private void OnNapMayManBtnClick()
	{
		string value = "TienNhanChiLo;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		setHoatDongFocus(napMayManFocus);
		GUIManager.setScreen(GAME_SCREEN.ScreenTienNhanChiLo);
	}

	private void OnCuopMoBtnClick()
	{
		string value = "CuopMo;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		setHoatDongFocus(cuopMoFocus);
		GUIManager.setScreen(GAME_SCREEN.ScreenBaoKhoMain);
	}

	private void OnHoaVangBtnClick()
	{
		string value = "HoaVang;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		setHoatDongFocus(hoaVangFocus);
		GUIManager.setScreen(GAME_SCREEN.ScreenHoaVangMain);
	}

	private void OnHuyenKhiBtnClick()
	{
		string value = "HuyenKhi;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		setMonPhaiFocus(huyenKhiFocus);
		GUIManager.setScreen(GAME_SCREEN.ScreenHuyenKhiMain);
	}

	private void OnLeagueBtnClick()
	{
		string value = "League;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
		}
		else if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level >= ConfigManager.instance.OtherConfig.LevelLeague)
		{
			setHoatDongFocus(leagueFocus);
			ScreenLeagueMain screenLeagueMain = GUIManager.getScreen(GAME_SCREEN.ScreenLeagueMain) as ScreenLeagueMain;
			if (screenLeagueMain.sieuCupResponse == null || screenLeagueMain.leagueDataResponse == null)
			{
				GameManager.instance.m_GameClient.RequestLienDauData();
			}
			else
			{
				GUIManager.setScreen(GAME_SCREEN.ScreenLeagueMain);
			}
		}
		else
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("TinhNangLeagueChuaMo"), ConfigManager.instance.OtherConfig.LevelLeague));
		}
	}

	private void onChoTayVucClick()
	{
		setChoFocus(tayVucFocus);
		string value = "TayVucThuongNhan;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
		}
		else if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.TayVucThuongNhanConfig != null && GameManager.instance.m_GameClient.ServerTime < GameManager.instance.m_GameClient.UserInfo.ServerInfo.TayVucThuongNhanConfig.ThoiGianKetThuc && GameManager.instance.m_GameClient.ServerTime > GameManager.instance.m_GameClient.UserInfo.ServerInfo.TayVucThuongNhanConfig.ThoiGianBatDau)
		{
			GUIManager.setScreen(GAME_SCREEN.ScreenTayVucThuongNhan);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("HetTimeEvent"));
		}
	}

	private void onChoDeTuClick()
	{
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		setChoFocus(choDeTuFocus);
		GUIManager.setScreen(GAME_SCREEN.ScreenChoDeTu);
	}

	private void onChoVatPhamClick()
	{
		setChoFocus(choVatPhamFocus);
		GUIManager.setScreen(GAME_SCREEN.ScreenChoVatPham);
	}

	private void onChoLeBaoClick()
	{
		setChoFocus(choLeBaoFocus);
		GUIManager.setScreen(GAME_SCREEN.ScreenChoLeBao);
	}

	private void onRuongThanBiClick()
	{
		string value = "CuaHangThanBi;";
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains(value))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		setChoFocus(ruongthanbiFocus);
		GUIManager.setScreen(GAME_SCREEN.ScreenCuaHangThanBi);
	}

	private void onNapTienClick()
	{
		setChoFocus(naptienFocus);
		MessagePopup.Create("May chu da tat tinh nang nap.");
	}

	private void setMainMenuFocus(UISprite spFocus)
	{
		mainFocus.gameObject.SetActive(false);
		monPhaiFocus.gameObject.SetActive(false);
		hoatDongFocus.gameObject.SetActive(false);
		kyNgoFocus.gameObject.SetActive(false);
		choFocus.gameObject.SetActive(false);
		bangHoiFocus.gameObject.SetActive(false);
		if (spFocus != null)
		{
			spFocus.gameObject.SetActive(true);
		}
	}

	private void setHoatDongFocus(UISprite spFocus)
	{
		dietMaFocus.gameObject.SetActive(false);
		hacMocNhaiFocus.gameObject.SetActive(false);
		luanKiemFocus.gameObject.SetActive(false);
		camDiaFocus.gameObject.SetActive(false);
		chienTruongChinhTaFocus.gameObject.SetActive(false);
		duocVienFocus.gameObject.SetActive(false);
		leagueFocus.gameObject.SetActive(false);
		quangMinhFocus.gameObject.SetActive(false);
		thanthuDaoFocus.gameObject.SetActive(false);
		napMayManFocus.gameObject.SetActive(false);
		cuopMoFocus.gameObject.SetActive(false);
		hoaVangFocus.gameObject.SetActive(false);
		if (spFocus != null)
		{
			spFocus.gameObject.SetActive(true);
		}
	}

	private void setChoFocus(UISprite spFocus)
	{
		choDeTuFocus.gameObject.SetActive(false);
		choVatPhamFocus.gameObject.SetActive(false);
		choLeBaoFocus.gameObject.SetActive(false);
		naptienFocus.gameObject.SetActive(false);
		ruongthanbiFocus.gameObject.SetActive(false);
		tayVucFocus.gameObject.SetActive(false);
		if (spFocus != null)
		{
			spFocus.gameObject.SetActive(true);
		}
	}

	private void setMonPhaiFocus(UISprite spFocus)
	{
		chienThuatFocus.gameObject.SetActive(false);
		doiHinhFocus.gameObject.SetActive(false);
		tayNaiFocus.gameObject.SetActive(false);
		deTuFocus.gameObject.SetActive(false);
		trangBiFocus.gameObject.SetActive(false);
		voCongFocus.gameObject.SetActive(false);
		nguyenKhiFocus.gameObject.SetActive(false);
		thanthuFocus.gameObject.SetActive(false);
		sonMonFocus.gameObject.SetActive(false);
		thanBinhFocus.gameObject.SetActive(false);
		thienMaFocus.gameObject.SetActive(false);
		boiduongTbFocus.gameObject.SetActive(false);
		huyenKhiFocus.gameObject.SetActive(false);
		if (spFocus != null)
		{
			spFocus.gameObject.SetActive(true);
		}
	}

	private void setGroupFocus(GameObject go)
	{
		monPhaiGrp.gameObject.SetActive(false);
		hoatDongGrp.gameObject.SetActive(false);
		kyNgoGrp.gameObject.SetActive(false);
		choGrp.gameObject.SetActive(false);
		bangHoiGrp.SetActive(false);
		if (go != null)
		{
			go.gameObject.SetActive(true);
		}
	}

	private void OnBeQuanBtnClick(GameObject go)
	{
		EGDebug.LogWarning("OnBeQuanBtnClick");
	}

	private void OnLongMachBtnClick(GameObject go)
	{
		EGDebug.LogWarning("OnLongMachBtnClick");
	}

	private void OnOanTuTiBtnClick(GameObject go)
	{
		EGDebug.LogWarning("OnOanTuTiBtnClick");
	}

	private void OnThamBaiBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenKyNgoThamBai);
	}

	private void OnTheLucBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenKyNGoTheLuc);
	}

	private void OnHuaNguyenBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenKyNgoHuaNguyen);
	}

	private void OnThangCapBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenKyNgoThangCap);
	}

	private void OnDangNhapNhanThuongBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenDangNhapNhanThuong);
	}

	private void OnThanTaiBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenKyNgoThanTai);
	}

	private void OnDanhBacBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenKyNgoDanhBac);
	}

	private void OnHiepKhachHanhBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenKyNgoCuuTieuPhong);
	}

	private void OnThachSanhBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenRuongThachSanh);
	}

	private void OnDaiLyBtnClick(GameObject go)
	{
	}

	private void OnUongRuouBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenKyNgoUongRuou);
	}

	private void OnSonTrangBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GameManager.instance.m_GameClient.RequestULinhInfo();
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenULinhSonTrang);
	}

	private void OnDoiDoBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenKyNgoDoiDo);
	}

	private void OnBanhChungBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenBanhChung);
	}

	private void OnThuongNapBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenKyNgoThuongNap);
	}

	private void OnDiHoaCungBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenKyNgoDiHoaCung);
	}

	private void OnTichLuyNapBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenTuongDuongTeThe);
	}

	private void OnTichLuyTieuBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenKimTienBang);
	}

	private void OnPhaoHoaBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenBanPhaoHoaEvent);
	}

	private void OnDapNieuBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenKyNgoDapNieu);
	}

	private void OnVongQuayBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenVongQuay);
	}

	private void OnGuiTietKiemBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenKyNgoGuiTietKiem);
	}

	private void OnBacMayManBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenBacMayMan);
	}

	private void OnTuBaoBonBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenTuBaoBon);
	}

	private void OnBaoRuongBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenEventMoRuong);
	}

	private void OnNapHangNgayBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenTichNapHangNgay);
	}

	private void OnAnTheCaoThuBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenAnTheCaoThu);
	}

	private void OnCamCungBtnClick(GameObject go)
	{
		setMenuKyNgoActive(go);
		GUIManager.setScreen(GAME_SCREEN.ScreenCamCungBiBao);
	}

	private void setMenuKyNgoActive(GameObject go)
	{
		MenuButtonKyNgo component = go.GetComponent<MenuButtonKyNgo>();
		if (listKyNgoMenu == null || listKyNgoMenu.Count <= 0 || !(component != null))
		{
			return;
		}
		for (int i = 0; i < listKyNgoMenu.Count; i++)
		{
			if (listKyNgoMenu[i] == component)
			{
				listKyNgoMenu[i].isSelected(true);
			}
			else
			{
				listKyNgoMenu[i].isSelected(false);
			}
		}
	}

	private void OnHoatDongLienMinhBtnClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenHoatDongLienMinh);
	}

	public bool checkDisplayEventNapTien()
	{
		return false;
	}

	public void checkDisplayNotifyMenu()
	{
		checkDisplayThongBaoCho();
		checkDisplayThongBaoSuKien();
	}

	public void checkDisplayThongBaoCho()
	{
		if (!checkThongBaoCho() && !checkThongBaoLeBao())
		{
			notifyCho.gameObject.SetActive(false);
		}
		else
		{
			notifyCho.gameObject.SetActive(true);
		}
	}

	public void checkDisplayThongBaoSuKien()
	{
		if (checkThongBaoSuKien())
		{
			notifySuKien.gameObject.SetActive(true);
		}
		else
		{
			notifySuKien.gameObject.SetActive(false);
		}
	}

	private bool checkThongBaoCho()
	{
		try
		{
			if (GameManager.instance == null || GameManager.instance.m_GameClient == null || GameManager.instance.m_GameClient.UserInfo == null || GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian == null)
			{
				return false;
			}
			if (notifyChoDeTuBtn != null)
			{
				notifyChoDeTuBtn.gameObject.SetActive(false);
				DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
				DateTime layNhanVat1Time = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.layNhanVat1Time;
				DateTime layNhanVat2Time = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.layNhanVat2Time;
				DateTime layNhanVat3Time = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.layNhanVat3Time;
				int layNhanVat1Num = GameManager.instance.m_GameClient.UserInfo.GiaTriThoiGian.layNhanVat1Num;
				if (layNhanVat1Num < 5 && layNhanVat1Time < serverTime)
				{
					notifyChoDeTuBtn.gameObject.SetActive(true);
					return true;
				}
				if (layNhanVat2Time < serverTime)
				{
					notifyChoDeTuBtn.gameObject.SetActive(true);
					return true;
				}
				if (layNhanVat3Time < serverTime)
				{
					notifyChoDeTuBtn.gameObject.SetActive(true);
					return true;
				}
				return false;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}

	private bool checkThongBaoLeBao()
	{
		try
		{
			if (notifyChoLeBaoBtn != null)
			{
				notifyChoLeBaoBtn.gameObject.SetActive(false);
				if (GameManager.instance == null || GameManager.instance.m_GameClient == null || GameManager.instance.m_GameClient.UserInfo == null || GameManager.instance.m_GameClient.UserInfo.ServerInfo == null)
				{
					return false;
				}
				List<GoiVatPhamCfg> list = new List<GoiVatPhamCfg>();
				DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
				if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.GoiVatPham != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.GoiVatPham.Length > 5)
				{
					List<GoiVatPhamCfg> list2 = JsonMapper.ToObject<List<GoiVatPhamCfg>>(GameManager.instance.m_GameClient.UserInfo.ServerInfo.GoiVatPham);
					if (list2 != null && list2.Count > 0)
					{
						for (int i = 0; i < list2.Count; i++)
						{
							TimeSpan timeSpan = list2[i].ThoiGianTonTai - serverTime;
							if (serverTime < list2[i].ThoiGianTonTai)
							{
								list.Add(list2[i]);
							}
						}
					}
				}
				if (ConfigManager.instance != null && ConfigManager.instance.m_GoiVatPhamList != null)
				{
					foreach (GoiVatPhamCfg goiVatPham in ConfigManager.instance.m_GoiVatPhamList)
					{
						TimeSpan timeSpan2 = goiVatPham.ThoiGianTonTai - serverTime;
						if (serverTime < goiVatPham.ThoiGianTonTai)
						{
							list.Add(goiVatPham);
						}
					}
				}
				if (list.Count > 0)
				{
					for (int j = 0; j < list.Count; j++)
					{
						if (list[j] == null)
						{
							continue;
						}
						if (list[j].CodeName.StartsWith("GV_VIP"))
						{
							string value = list[j].CodeName.Substring(6, list[j].CodeName.Length - 6);
							int num = Convert.ToInt32(value);
							if (num <= GameManager.instance.m_GameClient.UserInfo.Gamer.Vip)
							{
								string value2 = list[j].CodeName + ";";
								if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(value2))
								{
									notifyChoLeBaoBtn.gameObject.SetActive(true);
									return true;
								}
							}
						}
						if (list[j].CodeName.StartsWith("GV_1Lan"))
						{
							string value3 = list[j].CodeName + ";";
							if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(value3))
							{
								notifyChoLeBaoBtn.gameObject.SetActive(true);
								return true;
							}
						}
						if (!list[j].CodeName.StartsWith("GV_Ngay1Lan"))
						{
							continue;
						}
						if (list[j].CodeName == "GV_Ngay1Lan_DauTuan" && (serverTime.DayOfWeek == DayOfWeek.Monday || serverTime.DayOfWeek == DayOfWeek.Tuesday))
						{
							string value4 = list[j].CodeName + ";";
							if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(value4))
							{
								notifyChoLeBaoBtn.gameObject.SetActive(true);
								return true;
							}
						}
						if (list[j].CodeName == "GV_Ngay1Lan")
						{
							string value5 = list[j].CodeName + ";";
							if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(value5))
							{
								notifyChoLeBaoBtn.gameObject.SetActive(true);
								return true;
							}
						}
					}
				}
			}
			return false;
		}
		catch
		{
			return false;
		}
	}

	private bool checkThongBaoSuKien()
	{
		if (checkThongBaoTheLuc())
		{
			return true;
		}
		if (checkThongBaoThamBai())
		{
			return true;
		}
		if (checkThongBaoHuaNguyen())
		{
			return true;
		}
		if (checkThongBaoThangCap())
		{
			return true;
		}
		if (checkThongBaoDNNhanThuong())
		{
			return true;
		}
		if (checkThongBaoUongRuou())
		{
			return true;
		}
		if (checkThongBaoRuongTS())
		{
			return true;
		}
		if (checkThongBaoThanTai())
		{
			return true;
		}
		if (checkThongBaoDoiDo())
		{
			return true;
		}
		if (checkThongBaoCuuTieuPhong())
		{
			return true;
		}
		if (checkThongBaoKimTienBang())
		{
			return true;
		}
		if (checkThongBaoTuongDuongTeThe())
		{
			return true;
		}
		if (checkThongBaoBacMayMan())
		{
			return true;
		}
		if (checkThongBaoTuBaoBon())
		{
			return true;
		}
		if (checkThongBaoTichNapHangNgay())
		{
			return true;
		}
		return false;
	}

	public bool checkThongBaoTheLuc()
	{
		DateTime serverTime = GameManager.instance.m_GameClient.ServerTime;
		if ((serverTime.Hour == 12 && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("HoiTheLuc1;")) || (serverTime.Hour == 18 && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("HoiTheLuc2;")))
		{
			return true;
		}
		return false;
	}

	public bool checkThongBaoThamBai()
	{
		if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("KyNgoThamBai;"))
		{
			return true;
		}
		return false;
	}

	public bool checkThongBaoHuaNguyen()
	{
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.HuaNguyenCount <= 0 && checkDisplayHuaNguyenMenu())
		{
			return true;
		}
		return false;
	}

	public bool checkThongBaoThangCap()
	{
		if (checkDisplayThangCapMenu() && ConfigManager.instance.OtherConfig.LenCapNhanThuong != null && ConfigManager.instance.OtherConfig.LenCapNhanThuong.Count > 0)
		{
			for (int i = 0; i < ConfigManager.instance.OtherConfig.LenCapNhanThuong.Count; i++)
			{
				OtherCfg.LenCapNhanThuongCfg lenCapNhanThuongCfg = ConfigManager.instance.OtherConfig.LenCapNhanThuong[i];
				string value = string.Format("ThuongLenLvl{0}", lenCapNhanThuongCfg.CapYeuCau);
				if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(value) && GameManager.instance.m_GameClient.UserInfo.Gamer.Level >= lenCapNhanThuongCfg.CapYeuCau)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool checkThongBaoDNNhanThuong()
	{
		if (checkDisplayDNNhanThuongMenu() && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("DangNhapNhanThuong;"))
		{
			return true;
		}
		return false;
	}

	public bool checkThongBaoUongRuou()
	{
		if (checkDisplayUongRuouMenu() && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("UR_"))
		{
			return true;
		}
		return false;
	}

	public bool checkThongBaoRuongTS()
	{
		if (checkDisplayRuongTSMenu())
		{
			int ruongThachSanhLvl = GameManager.instance.m_GameClient.UserInfo.Gamer.RuongThachSanhLvl;
			if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("NhanRuongTS;") && ruongThachSanhLvl > 0)
			{
				return true;
			}
		}
		return false;
	}

	public bool checkThongBaoThanTai()
	{
		if (checkDisplayThanTaiMenu())
		{
			int num = -1;
			if (!GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains("ThanTai"))
			{
				num = 50;
			}
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(string.Format("ThanTai{0};", 50)))
			{
				num = 300;
			}
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(string.Format("ThanTai{0};", 300)))
			{
				num = 2000;
			}
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(string.Format("ThanTai{0};", 2000)))
			{
				num = 10000;
			}
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(string.Format("ThanTai{0};", 10000)))
			{
				num = 0;
			}
			if (num > 0 && GameManager.instance.m_GameClient.UserInfo.Gamer.Vang >= num)
			{
				return true;
			}
		}
		return false;
	}

	public bool checkThongBaoDoiDo()
	{
		if (checkDisplayDoiDoMenu() && GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg != null)
		{
			for (int i = 0; i < 4; i++)
			{
				int soLuotDoiDoByString = ConfigManager.instance.GetSoLuotDoiDoByString(GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay, i);
				if (soLuotDoiDoByString <= 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool checkThongBaoCuuTieuPhong()
	{
		if (checkDisplayCuuTieuPhongMenu())
		{
			int soLuongDeTuGiap = ConfigManager.instance.GetSoLuongDeTuGiap(GameManager.instance.m_GameClient.UserInfo.HeroList);
			int soLuongTrangBiGiap = ConfigManager.instance.GetSoLuongTrangBiGiap(GameManager.instance.m_GameClient.UserInfo.TrangBiList);
			if (ConfigManager.instance.OtherConfig.CuuVienTieuPhong != null && ConfigManager.instance.OtherConfig.CuuVienTieuPhong.Count > 0)
			{
				for (int i = 0; i < ConfigManager.instance.OtherConfig.CuuVienTieuPhong.Count; i++)
				{
					string value = string.Format("CuuTieuPhong{0}", i);
					OtherCfg.CuuVienTieuPhongCfg cuuVienTieuPhongCfg = ConfigManager.instance.OtherConfig.CuuVienTieuPhong[i];
					if (cuuVienTieuPhongCfg.SoDeTuGiap <= soLuongDeTuGiap && cuuVienTieuPhongCfg.SoTrangBiGiap <= soLuongTrangBiGiap && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(value))
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	public bool checkThongBaoKimTienBang()
	{
		if (checkDisplayTichLuyTieuMenu())
		{
			UserInfo.ServerData.TichLuyTieuCfg tichLuyTieuConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyTieuConfig;
			if (tichLuyTieuConfig != null && tichLuyTieuConfig.ListMocTieu != null && tichLuyTieuConfig.ListMocTieu.Count > 0)
			{
				for (int i = 0; i < tichLuyTieuConfig.ListMocTieu.Count; i++)
				{
					bool flag = CommonHelper.CheckFlag(GameManager.instance.m_GameClient.UserInfo.Gamer.TichLuyTieuFlag, i);
					if (tichLuyTieuConfig.ListMocTieu[i].GiaTri <= GameManager.instance.m_GameClient.UserInfo.Gamer.DiemTichLuyTieu && !flag)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	public bool checkThongBaoTuongDuongTeThe()
	{
		if (checkDisplayTichLuyNapMenu())
		{
			UserInfo.ServerData.TichLuyNapCfg tichLuyNapConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyNapConfig;
			if (tichLuyNapConfig != null && tichLuyNapConfig.ListMocNap != null && tichLuyNapConfig.ListMocNap.Count > 0)
			{
				for (int i = 0; i < tichLuyNapConfig.ListMocNap.Count; i++)
				{
					bool flag = CommonHelper.CheckFlag(GameManager.instance.m_GameClient.UserInfo.Gamer.TichLuyNapFlag, i);
					if (tichLuyNapConfig.ListMocNap[i].GiaTri <= GameManager.instance.m_GameClient.UserInfo.Gamer.DiemTichLuyNap && !flag)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	public bool checkThongBaoTuBaoBon()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.TuBaoBonConfig != null)
		{
			UserInfo.GamerData gamer = GameManager.instance.m_GameClient.UserInfo.Gamer;
			int tuBaoBonLevel = gamer.TuBaoBonLevel;
			int tuBaoBonExp = gamer.TuBaoBonExp;
			int luotQuayTuBaoBon = ConfigManager.instance.GetLuotQuayTuBaoBon(tuBaoBonExp);
			if (tuBaoBonLevel < luotQuayTuBaoBon)
			{
				return true;
			}
		}
		return false;
	}

	public bool checkThongBaoBacMayMan()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.BacMayManConfig != null && DateTime.Now <= GameManager.instance.m_GameClient.UserInfo.ServerInfo.BacMayManConfig.ThoiGianBatDau.AddDays(5.0) && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("QuayBacMayMan2;"))
		{
			return true;
		}
		return false;
	}

	public bool checkThongBaoTichNapHangNgay()
	{
		UserInfo.ServerData.EventTichNapHangNgayCfg tichLuyNapHangNgayConfig = GameManager.instance.m_GameClient.UserInfo.ServerInfo.TichLuyNapHangNgayConfig;
		if (tichLuyNapHangNgayConfig != null && tichLuyNapHangNgayConfig.ListMocNap != null && tichLuyNapHangNgayConfig.ListMocNap.Count > 0)
		{
			for (int i = 0; i < tichLuyNapHangNgayConfig.ListMocNap.Count; i++)
			{
				if (GameManager.instance.m_GameClient.UserInfo.Gamer.NapHangNgay > tichLuyNapHangNgayConfig.ListMocNap[i].GiaTri && !GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(string.Format("TichNapHangNgay{0};", i)))
				{
					return true;
				}
			}
		}
		return false;
	}
}

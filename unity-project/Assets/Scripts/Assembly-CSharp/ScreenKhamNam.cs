using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenKhamNam : ScreenBase
{
	public OtherAvatar trangBiAvatar;

	public UISprite spCurrentNgoc1;

	public UISprite spCurrentNgoc2;

	public UISprite spCurrentNgoc3;

	public UILabel lbTrangBiName;

	public UserInfo.TrangBiData m_TrangBiData;

	public UISprite spNgocDo;

	public UISprite spNgocXanh;

	public UISprite spNgocVang;

	public UISprite spNgocTim;

	public UISprite spLapNgocSelected;

	public UILabel lbLapNgocSelected;

	public UISprite spGoNgocSelected;

	public UILabel lbGoNgocSelected;

	public List<UISprite> listNgocLevel;

	public List<UILabel> listNgocLevelLabel;

	public GameObject groupGoNgoc;

	public GameObject groupLapNgoc;

	public GameObject focusLapNgoc;

	public GameObject focusLoaiNgoc;

	public GameObject focusLevelNgoc;

	public GameObject m_AnimLapNgoc;

	public GameObject m_AnimGoNgoc;

	public UILabel lbSoLuongNgocDo;

	public UILabel lbSoLuongNgocXanh;

	public UILabel lbSoLuongNgocTim;

	public UILabel lbSoLuongNgocVang;

	private UserInfo.TrangBiData.LoaiNgoc loaiNgocSelected = UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO;

	private int viTriSelected = -1;

	private int levelSelected;

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
		if (listNgocLevel != null && listNgocLevel.Count > 0)
		{
			for (int i = 0; i < listNgocLevel.Count; i++)
			{
				UIEventListener.Get(listNgocLevel[i].gameObject).onClick = onClick_LevelNgoc;
			}
		}
	}

	public void Set(UserInfo.TrangBiData data)
	{
		if (data != null)
		{
			m_TrangBiData = data;
		}
	}

	public override void OnActive()
	{
		focusLapNgoc.SetActive(false);
		focusLevelNgoc.SetActive(false);
		focusLoaiNgoc.SetActive(false);
		m_AnimGoNgoc.SetActive(false);
		m_AnimLapNgoc.SetActive(false);
		if (m_TrangBiData != null)
		{
			loaiNgocSelected = UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO;
			viTriSelected = -1;
			levelSelected = 0;
			displayInfo();
		}
	}

	public void displayInfo()
	{
		trangBiAvatar.Set(m_TrangBiData);
		TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[m_TrangBiData.Name];
		lbTrangBiName.text = trangBiCfg.TenHienThi;
		spCurrentNgoc1.gameObject.SetActive(false);
		spCurrentNgoc2.gameObject.SetActive(false);
		spCurrentNgoc3.gameObject.SetActive(false);
		if (trangBiCfg.Hang >= ItemClass.Binh)
		{
			spCurrentNgoc1.gameObject.SetActive(true);
			setIconNgoc(m_TrangBiData.Ngoc1Name, m_TrangBiData.Ngoc1Lvl, spCurrentNgoc1);
		}
		if (trangBiCfg.Hang >= ItemClass.At)
		{
			spCurrentNgoc2.gameObject.SetActive(true);
			setIconNgoc(m_TrangBiData.Ngoc2Name, m_TrangBiData.Ngoc2Lvl, spCurrentNgoc2);
		}
		if (trangBiCfg.Hang >= ItemClass.Giap)
		{
			spCurrentNgoc3.gameObject.SetActive(true);
			setIconNgoc(m_TrangBiData.Ngoc3Name, m_TrangBiData.Ngoc3Lvl, spCurrentNgoc3);
		}
		focusLapNgoc.gameObject.SetActive(true);
		focusLapNgoc.transform.localPosition = spCurrentNgoc1.transform.localPosition;
		displaySoLuongNgoc();
		displayNgocSelectedInfo(m_TrangBiData, 0);
	}

	private void displaySoLuongNgoc()
	{
		if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList != null)
		{
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_NGOC_DO");
			if (vatPhamTieuThuData != null)
			{
				lbSoLuongNgocDo.text = vatPhamTieuThuData.Quantity.ToString();
			}
			else
			{
				lbSoLuongNgocDo.text = "0";
			}
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData2 = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_NGOC_XANH");
			if (vatPhamTieuThuData2 != null)
			{
				lbSoLuongNgocXanh.text = vatPhamTieuThuData2.Quantity.ToString();
			}
			else
			{
				lbSoLuongNgocXanh.text = "0";
			}
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData3 = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_NGOC_TIM");
			if (vatPhamTieuThuData3 != null)
			{
				lbSoLuongNgocTim.text = vatPhamTieuThuData3.Quantity.ToString();
			}
			else
			{
				lbSoLuongNgocTim.text = "0";
			}
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData4 = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_NGOC_VANG");
			if (vatPhamTieuThuData4 != null)
			{
				lbSoLuongNgocVang.text = vatPhamTieuThuData4.Quantity.ToString();
			}
			else
			{
				lbSoLuongNgocVang.text = "0";
			}
		}
	}

	private void setFocusLoaiNgocSelected(UserInfo.TrangBiData.LoaiNgoc loaiNgoc)
	{
		focusLoaiNgoc.gameObject.SetActive(true);
		switch (loaiNgoc)
		{
		case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO:
			focusLoaiNgoc.transform.localPosition = spNgocDo.transform.localPosition;
			break;
		case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_XANH:
			focusLoaiNgoc.transform.localPosition = spNgocXanh.transform.localPosition;
			break;
		case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_VANG:
			focusLoaiNgoc.transform.localPosition = spNgocVang.transform.localPosition;
			break;
		case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_TIM:
			focusLoaiNgoc.transform.localPosition = spNgocTim.transform.localPosition;
			break;
		}
	}

	public void setIconNgoc(string ngocName, int ngocLevel, UISprite currentSprite)
	{
		UserInfo.TrangBiData.LoaiNgoc loaiNgoc = UserInfo.TrangBiData.GetLoaiNgoc(ngocName);
		setNgocData(loaiNgoc, ngocLevel, currentSprite);
	}

	private void setNgocData(UserInfo.TrangBiData.LoaiNgoc loaiNgoc, int ngocLevel, UISprite currentSprite)
	{
		string text = "icon_";
		string text2;
		if (ngocLevel <= 0 || ngocLevel > 10)
		{
			text2 = "icon_ngoc_empty";
		}
		else
		{
			string text3;
			switch (loaiNgoc)
			{
			case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO:
				text3 = text + "do_hang" + ngocLevel;
				break;
			case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_TIM:
				text3 = text + "tim_hang" + ngocLevel;
				break;
			case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_VANG:
				text3 = text + "vang_hang" + ngocLevel;
				break;
			case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_XANH:
				text3 = text + "xanh_hang" + ngocLevel;
				break;
			default:
				text3 = "icon_ngoc_empty";
				break;
			}
			text2 = text3;
		}
		text = text2;
		currentSprite.spriteName = text;
	}

	public void onClick_NgocDo()
	{
		displayLoaiNgocSelected(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO);
	}

	public void onClick_NgocXanh()
	{
		displayLoaiNgocSelected(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_XANH);
	}

	public void onClick_NgocVang()
	{
		displayLoaiNgocSelected(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_VANG);
	}

	public void onClick_NgocTim()
	{
		displayLoaiNgocSelected(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_TIM);
	}

	private void displayLoaiNgocSelected(UserInfo.TrangBiData.LoaiNgoc loaiNgoc)
	{
		loaiNgocSelected = loaiNgoc;
		loadLevelKham(loaiNgoc);
		updateInfoNgocSelected(loaiNgoc, 1);
		setFocusLoaiNgocSelected(loaiNgoc);
	}

	private void updateInfoNgocSelected(UserInfo.TrangBiData.LoaiNgoc loaiNgoc, int level)
	{
		UserInfo.TrangBiData trangBiData = new UserInfo.TrangBiData();
		trangBiData.Name = m_TrangBiData.Name;
		trangBiData.Ngoc1Lvl = level;
		if (loaiNgocSelected != UserInfo.TrangBiData.LoaiNgoc.NONE)
		{
			if (loaiNgocSelected == UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO)
			{
				trangBiData.Ngoc1Name = "VP_NGOC_DO";
			}
			else if (loaiNgocSelected == UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_XANH)
			{
				trangBiData.Ngoc1Name = "VP_NGOC_XANH";
			}
			else if (loaiNgocSelected == UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_VANG)
			{
				trangBiData.Ngoc1Name = "VP_NGOC_VANG";
			}
			else if (loaiNgocSelected == UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_TIM)
			{
				trangBiData.Ngoc1Name = "VP_NGOC_TIM";
			}
		}
		else
		{
			trangBiData.Ngoc1Name = string.Empty;
		}
		UserInfo.TrangBiData.LoaiBuff buff;
		float ChiSo;
		UserInfo.TrangBiData.GetLoaiBuff(trangBiData, 0, out buff, out ChiSo);
		lbLapNgocSelected.text = displayChiSoNgoc(buff, ChiSo);
		setIconNgoc(trangBiData.Ngoc1Name, level, spLapNgocSelected);
		if (listNgocLevel != null && listNgocLevel[level - 1] != null)
		{
			focusLevelNgoc.gameObject.SetActive(true);
			focusLevelNgoc.transform.localPosition = listNgocLevel[level - 1].transform.localPosition;
		}
	}

	public void onClick_CurrentNgoc1()
	{
		if (m_TrangBiData != null)
		{
			focusLapNgoc.gameObject.SetActive(true);
			focusLapNgoc.transform.localPosition = spCurrentNgoc1.transform.localPosition;
			viTriSelected = 0;
			displayNgocSelectedInfo(m_TrangBiData, 0);
		}
	}

	public void onClick_CurrentNgoc2()
	{
		if (m_TrangBiData != null)
		{
			focusLapNgoc.gameObject.SetActive(true);
			focusLapNgoc.transform.localPosition = spCurrentNgoc2.transform.localPosition;
			viTriSelected = 1;
			displayNgocSelectedInfo(m_TrangBiData, 1);
		}
	}

	public void onClick_CurrentNgoc3()
	{
		if (m_TrangBiData != null)
		{
			focusLapNgoc.gameObject.SetActive(true);
			focusLapNgoc.transform.localPosition = spCurrentNgoc3.transform.localPosition;
			viTriSelected = 2;
			displayNgocSelectedInfo(m_TrangBiData, 2);
		}
	}

	public void onClick_LevelNgoc(GameObject go)
	{
		UISprite component = go.transform.GetComponent<UISprite>();
		if (!(component != null) || listNgocLevel == null || listNgocLevel.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < listNgocLevel.Count; i++)
		{
			if (listNgocLevel[i] == component)
			{
				levelSelected = i + 1;
				updateInfoNgocSelected(loaiNgocSelected, levelSelected);
			}
		}
	}

	private string displayChiSoNgoc(UserInfo.TrangBiData.LoaiBuff loaiBuff, float chiSo)
	{
		string empty = string.Empty;
		switch (loaiBuff)
		{
		case UserInfo.TrangBiData.LoaiBuff.TOC_DANH:
			return string.Format(Localization.instance.Get("TangChiSoTocDoDanh"), chiSo);
		case UserInfo.TrangBiData.LoaiBuff.KHANG_CHUONG:
			return string.Format(Localization.instance.Get("TangChiSoKhangChuong"), chiSo);
		case UserInfo.TrangBiData.LoaiBuff.HOA_GIAI:
			return string.Format(Localization.instance.Get("TangChiSoHoaGiai"), chiSo);
		case UserInfo.TrangBiData.LoaiBuff.BAO_KICH:
			return string.Format(Localization.instance.Get("TangChiSoBaoKich"), chiSo);
		case UserInfo.TrangBiData.LoaiBuff.TANG_CHI_SO:
			return string.Format(Localization.instance.Get("TangChiSoNgoc"), chiSo);
		case UserInfo.TrangBiData.LoaiBuff.NONE:
			return Localization.instance.Get("ChuaLapNgocLabel");
		default:
			return "- - -";
		}
	}

	private void displayNgocSelectedInfo(UserInfo.TrangBiData trangBiData, int vitriNgoc)
	{
		if (trangBiData != null && vitriNgoc >= 0)
		{
			viTriSelected = vitriNgoc;
			string ngocName = string.Empty;
			int ngocLevel = 0;
			switch (vitriNgoc)
			{
			case 0:
				ngocName = trangBiData.Ngoc1Name;
				ngocLevel = trangBiData.Ngoc1Lvl;
				break;
			case 1:
				ngocName = trangBiData.Ngoc2Name;
				ngocLevel = trangBiData.Ngoc2Lvl;
				break;
			case 2:
				ngocName = trangBiData.Ngoc3Name;
				ngocLevel = trangBiData.Ngoc3Lvl;
				break;
			}
			UserInfo.TrangBiData.LoaiBuff buff;
			float ChiSo;
			UserInfo.TrangBiData.GetLoaiBuff(trangBiData, vitriNgoc, out buff, out ChiSo);
			if (UserInfo.TrangBiData.GetLoaiNgoc(ngocName) == UserInfo.TrangBiData.LoaiNgoc.NONE)
			{
				groupGoNgoc.gameObject.SetActive(false);
				groupLapNgoc.gameObject.SetActive(true);
				loadLevelKham(loaiNgocSelected);
				lbLapNgocSelected.text = displayChiSoNgoc(buff, ChiSo);
				setIconNgoc(ngocName, ngocLevel, spLapNgocSelected);
				levelSelected = 0;
				focusLevelNgoc.gameObject.SetActive(false);
			}
			else
			{
				groupGoNgoc.gameObject.SetActive(true);
				groupLapNgoc.gameObject.SetActive(false);
				lbGoNgocSelected.text = displayChiSoNgoc(buff, ChiSo);
				setIconNgoc(ngocName, ngocLevel, spGoNgocSelected);
			}
		}
	}

	private void loadLevelKham(UserInfo.TrangBiData.LoaiNgoc loaiNgoc)
	{
		setFocusLoaiNgocSelected(loaiNgoc);
		if (listNgocLevel == null || listNgocLevelLabel == null)
		{
			return;
		}
		for (int i = 0; i < 10; i++)
		{
			if (listNgocLevel[i] != null)
			{
				setNgocData(loaiNgoc, i + 1, listNgocLevel[i]);
				if (i == 0)
				{
					levelSelected = 1;
					focusLevelNgoc.gameObject.SetActive(true);
					focusLevelNgoc.transform.localPosition = listNgocLevel[i].transform.localPosition;
				}
			}
			if (listNgocLevelLabel[i] != null)
			{
				int soLuongNgocCan = ConfigManager.instance.OtherConfig.GetSoLuongNgocCan(i + 1);
				listNgocLevelLabel[i].text = soLuongNgocCan.ToString();
			}
		}
		updateInfoNgocSelected(loaiNgoc, 1);
	}

	public void btnBack_OnClick()
	{
		GUIManager.instance.SetScreen(GUIManager.instance.LastScreen);
	}

	public void btnKhamNam_OnClick()
	{
		if (m_TrangBiData == null)
		{
			return;
		}
		if (viTriSelected >= 0 && loaiNgocSelected != UserInfo.TrangBiData.LoaiNgoc.NONE && levelSelected > 0)
		{
			if (getNgocID() > 0)
			{
				KhamNgocRequest khamNgocRequest = new KhamNgocRequest();
				khamNgocRequest.TrangBiID = m_TrangBiData.ID;
				khamNgocRequest.NgocID = getNgocID();
				khamNgocRequest.Slot = viTriSelected;
				khamNgocRequest.CapKham = levelSelected;
				GameManager.instance.m_GameClient.RequestKhamNgoc(khamNgocRequest);
			}
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ChuaChonNgocDeLap"));
		}
	}

	public void btnGoNgoc_OnClick()
	{
		if (m_TrangBiData != null && viTriSelected >= 0)
		{
			GoNgocRequest goNgocRequest = new GoNgocRequest();
			goNgocRequest.TrangBiID = m_TrangBiData.ID;
			goNgocRequest.Slot = viTriSelected;
			GameManager.instance.m_GameClient.RequestGoNgoc(goNgocRequest);
		}
	}

	private int getNgocID()
	{
		if (m_TrangBiData != null)
		{
			UserInfo.TrangBiData.LoaiNgoc loaiNgoc = UserInfo.TrangBiData.GetLoaiNgoc(m_TrangBiData.Ngoc1Name);
			UserInfo.TrangBiData.LoaiNgoc loaiNgoc2 = UserInfo.TrangBiData.GetLoaiNgoc(m_TrangBiData.Ngoc2Name);
			UserInfo.TrangBiData.LoaiNgoc loaiNgoc3 = UserInfo.TrangBiData.GetLoaiNgoc(m_TrangBiData.Ngoc3Name);
			if (loaiNgocSelected != loaiNgoc && loaiNgocSelected != loaiNgoc2 && loaiNgocSelected != loaiNgoc3)
			{
				if (loaiNgocSelected == UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO)
				{
					UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_NGOC_DO");
					if (vatPhamTieuThuData != null)
					{
						return vatPhamTieuThuData.ID;
					}
					MessagePopup.Create(Localization.instance.Get("KhongCoVatPhamKhamNgoc"));
				}
				else if (loaiNgocSelected == UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_XANH)
				{
					UserInfo.VatPhamTieuThuData vatPhamTieuThuData2 = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_NGOC_XANH");
					if (vatPhamTieuThuData2 != null)
					{
						return vatPhamTieuThuData2.ID;
					}
					MessagePopup.Create(Localization.instance.Get("KhongCoVatPhamKhamNgoc"));
				}
				else if (loaiNgocSelected == UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_VANG)
				{
					UserInfo.VatPhamTieuThuData vatPhamTieuThuData3 = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_NGOC_VANG");
					if (vatPhamTieuThuData3 != null)
					{
						return vatPhamTieuThuData3.ID;
					}
					MessagePopup.Create(Localization.instance.Get("KhongCoVatPhamKhamNgoc"));
				}
				else if (loaiNgocSelected == UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_TIM)
				{
					UserInfo.VatPhamTieuThuData vatPhamTieuThuData4 = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_NGOC_TIM");
					if (vatPhamTieuThuData4 != null)
					{
						return vatPhamTieuThuData4.ID;
					}
					MessagePopup.Create(Localization.instance.Get("KhongCoVatPhamKhamNgoc"));
				}
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoKhongLapCungLoaiNgoc"));
			}
		}
		return 0;
	}

	public void btnHelp_OnClick()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(2, 4);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
	}

	public void updateInfo(bool isLapNgoc)
	{
		UserInfo.TrangBiData trangBiData = GameManager.instance.m_GameClient.UserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.Name == m_TrangBiData.Name && e.ID == m_TrangBiData.ID);
		if (trangBiData != null)
		{
			m_TrangBiData = trangBiData;
			displayNgocSelectedInfo(m_TrangBiData, viTriSelected);
			if (viTriSelected == 0)
			{
				setIconNgoc(m_TrangBiData.Ngoc1Name, m_TrangBiData.Ngoc1Lvl, spCurrentNgoc1);
			}
			if (viTriSelected == 1)
			{
				setIconNgoc(m_TrangBiData.Ngoc2Name, m_TrangBiData.Ngoc2Lvl, spCurrentNgoc2);
			}
			if (viTriSelected == 2)
			{
				setIconNgoc(m_TrangBiData.Ngoc3Name, m_TrangBiData.Ngoc3Lvl, spCurrentNgoc3);
			}
			displaySoLuongNgoc();
			if (isLapNgoc)
			{
				startPlayAnimKham(viTriSelected, m_AnimLapNgoc);
			}
			else
			{
				startPlayAnimKham(viTriSelected, m_AnimGoNgoc);
			}
		}
	}

	public void startPlayAnimKham(int vitriPlay, GameObject anim)
	{
		focusLapNgoc.gameObject.SetActive(false);
		if (vitriPlay == 0)
		{
			anim.transform.localPosition = spCurrentNgoc1.transform.localPosition;
		}
		if (vitriPlay == 1)
		{
			anim.transform.localPosition = spCurrentNgoc2.transform.localPosition;
		}
		if (vitriPlay == 2)
		{
			anim.transform.localPosition = spCurrentNgoc3.transform.localPosition;
		}
		anim.SetActive(true);
		anim.GetComponent<ParticleSystem>().Play();
		StartCoroutine(setFocusAgain(1f, anim));
	}

	public IEnumerator setFocusAgain(float waitTime, GameObject anim)
	{
		yield return new WaitForSeconds(waitTime);
		focusLapNgoc.gameObject.SetActive(true);
		anim.gameObject.SetActive(false);
	}
}

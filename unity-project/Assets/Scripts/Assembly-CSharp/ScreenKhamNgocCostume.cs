using UnityEngine;

public class ScreenKhamNgocCostume : ScreenBase
{
	public CostumeAvatar avatar;

	public UISprite spHongNgoc;

	public UISprite spLamNgoc;

	public UISprite spHoangNgoc;

	public UISprite spTuNgoc;

	public UILabel lblNameCostume;

	public UILabel lblHienco;

	public UISprite spDonViNgoc;

	public GameObject KhamNgocGrp;

	public GameObject GoNgocGrp;

	public UISprite spCurrentGoNgoc;

	public UISprite spCurrentKhamNgoc;

	public UILabel lblGoNgocDetail;

	public UILabel lblKhamNgocDetail;

	public UISprite[] listNgocLevel = new UISprite[10];

	public UILabel[] listNgocLevelLabel = new UILabel[10];

	public GameObject particleSelectNgoc;

	public GameObject particleLvlNgoc;

	public GameObject particleKhamNgoc;

	public GameObject particleGoNgoc;

	public UILabel khamToiDaLabel;

	private UserInfo.CostumeData m_CostumeData;

	private int levelSelected = 1;

	private UserInfo.TrangBiData.LoaiNgoc selectedLoaiNgoc = UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO;

	private int maxLvlNgocKham;

	private void Start()
	{
		for (int i = 0; i < listNgocLevel.Length; i++)
		{
			UIEventListener.Get(listNgocLevel[i].gameObject).onClick = onClick_btnSelectLevelNgoc;
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	public void SyncWithNetworkData()
	{
		if (m_CostumeData == null)
		{
			return;
		}
		UserInfo.CostumeData costumeData = null;
		if (GameManager.instance.m_GameClient.UserInfo.CostumeList != null)
		{
			costumeData = GameManager.instance.m_GameClient.UserInfo.CostumeList.Find((UserInfo.CostumeData e) => e.ID == m_CostumeData.ID);
		}
		if (costumeData != null)
		{
			m_CostumeData = costumeData;
		}
		SetInfo(m_CostumeData);
	}

	public void SetInfo(UserInfo.CostumeData cosData)
	{
		if (cosData != null)
		{
			m_CostumeData = cosData;
			avatar.SetInfo(cosData.CodeName, cosData.TinhLuyen);
			spHongNgoc.spriteName = CostumeAvatar.GetNgocSpriteName(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO, cosData.HongNgoc);
			spLamNgoc.spriteName = CostumeAvatar.GetNgocSpriteName(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_XANH, cosData.LamNgoc);
			spHoangNgoc.spriteName = CostumeAvatar.GetNgocSpriteName(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_VANG, cosData.HoangNgoc);
			spTuNgoc.spriteName = CostumeAvatar.GetNgocSpriteName(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_TIM, cosData.TuNgoc);
			CostumeCfg costumeCfg = ConfigManager.instance.m_dicCostumeCfg[cosData.CodeName];
			int level = ConfigManager.instance.OtherConfig.MaxLvlNgoc1;
			if (cosData.TinhLuyen == 1)
			{
				level = ConfigManager.instance.OtherConfig.MaxLvlNgoc2;
			}
			else if (cosData.TinhLuyen == 2)
			{
				level = ConfigManager.instance.OtherConfig.MaxLvlNgoc3;
			}
			else if (cosData.TinhLuyen == 3)
			{
				level = ConfigManager.instance.OtherConfig.MaxLvlNgoc4;
			}
			else if (cosData.TinhLuyen > 3)
			{
				level = ConfigManager.instance.OtherConfig.MaxLvlNgoc4;
			}
			maxLvlNgocKham = level;
			khamToiDaLabel.text = string.Format(Localization.instance.Get("CostumeKhamNgocToiDa"), ConfigManager.instance.OtherConfig.GetSoLuongNgocCan(level));
			lblNameCostume.text = costumeCfg.TenHienThi;
			SelectNgoc(selectedLoaiNgoc);
		}
	}

	private void displaySoLuongNgoc(UserInfo.TrangBiData.LoaiNgoc loaiNgoc)
	{
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == UserInfo.TrangBiData.GetNgocName(loaiNgoc));
		if (vatPhamTieuThuData != null)
		{
			lblHienco.text = string.Format(Localization.instance.Get("CostumeNgocHienCoLabel"), vatPhamTieuThuData.Quantity);
		}
		else
		{
			lblHienco.text = string.Format(Localization.instance.Get("CostumeNgocHienCoLabel"), 0);
		}
	}

	public void SelectNgoc(UserInfo.TrangBiData.LoaiNgoc loaiNgoc)
	{
		int num = 0;
		selectedLoaiNgoc = loaiNgoc;
		switch (loaiNgoc)
		{
		default:
			return;
		case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO:
			num = m_CostumeData.HongNgoc;
			lblGoNgocDetail.text = string.Format(Localization.instance.Get("CostumeNgocBuffCong"), m_CostumeData.GetBuffCongFromNgoc());
			lblKhamNgocDetail.text = lblGoNgocDetail.text;
			particleSelectNgoc.SetActive(true);
			particleSelectNgoc.transform.localPosition = spHongNgoc.transform.localPosition;
			spDonViNgoc.spriteName = "icon_do_hang0";
			break;
		case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_XANH:
			num = m_CostumeData.LamNgoc;
			lblGoNgocDetail.text = string.Format(Localization.instance.Get("CostumeNgocBuffThu"), m_CostumeData.GetBuffThuFromNgoc());
			lblKhamNgocDetail.text = lblGoNgocDetail.text;
			particleSelectNgoc.SetActive(true);
			particleSelectNgoc.transform.localPosition = spLamNgoc.transform.localPosition;
			spDonViNgoc.spriteName = "icon_xanh_hang0";
			break;
		case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_VANG:
			num = m_CostumeData.HoangNgoc;
			lblGoNgocDetail.text = string.Format(Localization.instance.Get("CostumeNgocBuffMau"), m_CostumeData.GetBuffMauFromNgoc());
			lblKhamNgocDetail.text = lblGoNgocDetail.text;
			particleSelectNgoc.SetActive(true);
			particleSelectNgoc.transform.localPosition = spHoangNgoc.transform.localPosition;
			spDonViNgoc.spriteName = "icon_vang_hang0";
			break;
		case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_TIM:
			num = m_CostumeData.TuNgoc;
			lblGoNgocDetail.text = string.Format(Localization.instance.Get("CostumeNgocBuffNoiLuc"), m_CostumeData.GetBuffNoiLucFromNgoc());
			lblKhamNgocDetail.text = lblGoNgocDetail.text;
			particleSelectNgoc.SetActive(true);
			particleSelectNgoc.transform.localPosition = spTuNgoc.transform.localPosition;
			spDonViNgoc.spriteName = "icon_tim_hang0";
			break;
		}
		displaySoLuongNgoc(loaiNgoc);
		setNgocData(loaiNgoc, levelSelected, spCurrentKhamNgoc);
		setNgocData(loaiNgoc, num, spCurrentGoNgoc);
		if (num > 0)
		{
			GoNgocGrp.SetActive(true);
			KhamNgocGrp.SetActive(false);
			return;
		}
		GoNgocGrp.SetActive(false);
		KhamNgocGrp.SetActive(true);
		for (int i = 0; i < listNgocLevel.Length && i < listNgocLevelLabel.Length; i++)
		{
			if (listNgocLevel[i] != null)
			{
				setNgocData(loaiNgoc, i + 1, listNgocLevel[i]);
				if (i + 1 > maxLvlNgocKham)
				{
					listNgocLevel[i].color = Utils.MakeColor(66, 66, 66);
				}
				else
				{
					listNgocLevel[i].color = Color.white;
				}
			}
			if (listNgocLevelLabel[i] != null)
			{
				int soLuongNgocCan = ConfigManager.instance.OtherConfig.GetSoLuongNgocCan(i + 1);
				listNgocLevelLabel[i].text = soLuongNgocCan.ToString();
				if (i + 1 > maxLvlNgocKham)
				{
					listNgocLevelLabel[i].color = Utils.MakeColor(66, 66, 66);
				}
				else
				{
					listNgocLevelLabel[i].color = Utils.MakeColor(255, 248, 0);
				}
			}
		}
		SelectLevelNgoc(1);
	}

	private void SelectLevelNgoc(int level)
	{
		if (level >= 1 && level <= listNgocLevel.Length)
		{
			levelSelected = level;
			particleLvlNgoc.SetActive(true);
			particleLvlNgoc.transform.localPosition = listNgocLevel[level - 1].transform.localPosition;
			lblKhamNgocDetail.text = GetStringBuffFromNgoc(selectedLoaiNgoc, levelSelected);
		}
	}

	private string GetStringBuffFromNgoc(UserInfo.TrangBiData.LoaiNgoc loaiNgoc, int level)
	{
		string empty = string.Empty;
		switch (loaiNgoc)
		{
		case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO:
			empty = Localization.instance.Get("CostumeNgocBuffCong");
			break;
		case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_XANH:
			empty = Localization.instance.Get("CostumeNgocBuffThu");
			break;
		case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_VANG:
			empty = Localization.instance.Get("CostumeNgocBuffMau");
			break;
		case UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_TIM:
			empty = Localization.instance.Get("CostumeNgocBuffNoiLuc");
			break;
		default:
			return empty;
		}
		return string.Format(empty, CostumeCfg.GetBuffFromNgoc(loaiNgoc, level));
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

	private void onClick_btnSelectLevelNgoc(GameObject go)
	{
		UISprite component = go.GetComponent<UISprite>();
		if (!(component != null) || listNgocLevel == null || listNgocLevel.Length == 0)
		{
			return;
		}
		for (int i = 0; i < listNgocLevel.Length; i++)
		{
			if (listNgocLevel[i] == component)
			{
				if (i + 1 > maxLvlNgocKham)
				{
					int soLuongNgocCan = ConfigManager.instance.OtherConfig.GetSoLuongNgocCan(maxLvlNgocKham);
					MessagePopup.Create(string.Format(Localization.instance.Get("CostumeKhamNgocToiDa"), soLuongNgocCan));
				}
				else
				{
					SelectLevelNgoc(i + 1);
				}
			}
		}
	}

	private void btnGoNgoc_OnClick()
	{
		if (m_CostumeData != null)
		{
			GameManager.instance.m_GameClient.RequestGoNgocCostume(m_CostumeData.ID, selectedLoaiNgoc);
		}
	}

	private void btnKhamNam_OnClick()
	{
		if (m_CostumeData != null)
		{
			GameManager.instance.m_GameClient.RequestKhamNgocCostume(m_CostumeData.ID, selectedLoaiNgoc, levelSelected);
		}
	}

	private void onClick_CurrentNgoc1()
	{
		SelectNgoc(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO);
	}

	private void onClick_CurrentNgoc2()
	{
		SelectNgoc(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_XANH);
	}

	private void onClick_CurrentNgoc3()
	{
		SelectNgoc(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_VANG);
	}

	private void onClick_CurrentNgoc4()
	{
		SelectNgoc(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_TIM);
	}

	private void btnBack_OnClick()
	{
		GUIManager.GoBackLastScreen();
	}

	private void btnHelp_OnClick()
	{
	}
}

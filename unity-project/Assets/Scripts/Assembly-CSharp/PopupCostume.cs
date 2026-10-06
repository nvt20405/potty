using UnityEngine;

public class PopupCostume : MonoBehaviour
{
	public UILabel nameLabel;

	public UILabel descLabel;

	public UILabel effMenhLabel;

	public UILabel effNgoaiLabel;

	public UILabel effThanLabel;

	public UILabel effKhiLabel;

	public UILabel congLabel;

	public UILabel thuLabel;

	public UILabel mauLabel;

	public UILabel noiLucLabel;

	public UISprite hongNgocSprite;

	public UISprite lamNgocSprite;

	public UISprite tuNgocSprite;

	public UISprite hoangNgocSprite;

	public CostumeAvatar costumeAvatar;

	public GameObject btnKhamNgoc;

	public GameObject btnTinhLuyen;

	public GameObject btnChange;

	public GameObject btnGo;

	public UILabel lblTinhLuyenDesc;

	public UILabel lblKhamNgocDesc;

	public GameObject grpTinhLuyenPreview;

	public CostumeAvatar lowAvatar;

	public CostumeAvatar highAvatar;

	public UILabel effMenhLabelTL;

	public UILabel effNgoaiLabelTL;

	public UILabel effThanLabelTL;

	public UILabel effKhiLabelTL;

	public static PopupCostume instance;

	private UserInfo.CostumeData m_costumeData;

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	public static PopupCostume Create(UserInfo.CostumeData cosData, bool isPreview, bool isReadonly)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("popup/PopupCostume"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupCostume>();
		instance.SetInfo(cosData, isPreview, isReadonly);
		return instance;
	}

	private void OnGoBtnClick()
	{
		if (m_costumeData == null)
		{
			return;
		}
		CostumeCfg value = null;
		if (ConfigManager.instance.m_dicCostumeCfg.TryGetValue(costumeAvatar.CodeName, out value))
		{
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.HeroData heroData = userInfo.HeroList.Find((UserInfo.HeroData e) => e.CostumeID == m_costumeData.ID);
			if (heroData != null)
			{
				GameManager.instance.m_GameClient.RequestTakeOffCostume(heroData.HID);
				DestroyPopup();
			}
		}
	}

	public void SetInfo(UserInfo.CostumeData cosData, bool isPreview, bool isReadOnly)
	{
		m_costumeData = cosData;
		nameLabel.text = ConfigManager.instance.m_dicCostumeCfg[cosData.CodeName].TenHienThi;
		costumeAvatar.SetInfo(cosData.CodeName, cosData.TinhLuyen, cosData.HoangNgoc, cosData.HongNgoc, cosData.LamNgoc, cosData.TuNgoc);
		hoangNgocSprite.spriteName = CostumeAvatar.GetNgocSpriteName(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_VANG, cosData.HoangNgoc);
		hongNgocSprite.spriteName = CostumeAvatar.GetNgocSpriteName(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_DO, cosData.HongNgoc);
		lamNgocSprite.spriteName = CostumeAvatar.GetNgocSpriteName(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_XANH, cosData.LamNgoc);
		tuNgocSprite.spriteName = CostumeAvatar.GetNgocSpriteName(UserInfo.TrangBiData.LoaiNgoc.VP_NGOC_TIM, cosData.TuNgoc);
		congLabel.text = cosData.GetBuffCongFromNgoc().ToString();
		thuLabel.text = cosData.GetBuffThuFromNgoc().ToString();
		mauLabel.text = cosData.GetBuffMauFromNgoc().ToString();
		noiLucLabel.text = cosData.GetBuffNoiLucFromNgoc().ToString();
		CostumeCfg costumeCfg = ConfigManager.instance.m_dicCostumeCfg[cosData.CodeName];
		CostumeCfg.CostumeStat statFromTinhLuyen = costumeCfg.GetStatFromTinhLuyen(cosData.TinhLuyen);
		descLabel.text = ((statFromTinhLuyen == null || statFromTinhLuyen.Desc != null) ? statFromTinhLuyen.Desc : string.Empty);
		effMenhLabel.text = string.Format("{0} %", statFromTinhLuyen.EffMenh);
		effNgoaiLabel.text = string.Format("{0} %", statFromTinhLuyen.EffNgoai);
		effThanLabel.text = string.Format("{0} %", statFromTinhLuyen.EffThan);
		effKhiLabel.text = string.Format("{0} %", statFromTinhLuyen.EffKhi);
		if (isPreview)
		{
			btnChange.SetActive(false);
			btnKhamNgoc.SetActive(false);
			btnTinhLuyen.SetActive(false);
			btnGo.SetActive(false);
			lblKhamNgocDesc.gameObject.SetActive(true);
			lblTinhLuyenDesc.gameObject.SetActive(true);
			if (cosData.TinhLuyen <= 0)
			{
				grpTinhLuyenPreview.SetActive(false);
				costumeAvatar.gameObject.SetActive(true);
				effMenhLabelTL.gameObject.SetActive(false);
				effNgoaiLabelTL.gameObject.SetActive(false);
				effThanLabelTL.gameObject.SetActive(false);
				effKhiLabelTL.gameObject.SetActive(false);
				lblKhamNgocDesc.text = string.Format(Localization.instance.Get("CostumeKhamNgocDesc1"), ConfigManager.instance.OtherConfig.MaxLvlNgoc1);
				lblTinhLuyenDesc.text = Localization.instance.Get("CostumeTinhLuyenDesc");
				return;
			}
			grpTinhLuyenPreview.SetActive(true);
			costumeAvatar.gameObject.SetActive(false);
			CostumeCfg.CostumeStat statFromTinhLuyen2 = costumeCfg.GetStatFromTinhLuyen(cosData.TinhLuyen - 1);
			lowAvatar.SetInfo(cosData.CodeName, cosData.TinhLuyen - 1);
			highAvatar.SetInfo(cosData.CodeName, cosData.TinhLuyen);
			effMenhLabelTL.gameObject.SetActive(true);
			effNgoaiLabelTL.gameObject.SetActive(true);
			effThanLabelTL.gameObject.SetActive(true);
			effKhiLabelTL.gameObject.SetActive(true);
			effMenhLabel.text = string.Format("{0} %", statFromTinhLuyen2.EffMenh);
			effNgoaiLabel.text = string.Format("{0} %", statFromTinhLuyen2.EffNgoai);
			effThanLabel.text = string.Format("{0} %", statFromTinhLuyen2.EffThan);
			effKhiLabel.text = string.Format("{0} %", statFromTinhLuyen2.EffKhi);
			effMenhLabelTL.text = string.Format("-> {0} %", statFromTinhLuyen.EffMenh);
			effNgoaiLabelTL.text = string.Format("-> {0} %", statFromTinhLuyen.EffNgoai);
			effThanLabelTL.text = string.Format("-> {0} %", statFromTinhLuyen.EffThan);
			effKhiLabelTL.text = string.Format("-> {0} %", statFromTinhLuyen.EffKhi);
			int num = ConfigManager.instance.OtherConfig.MaxLvlNgoc1;
			int num2 = ConfigManager.instance.OtherConfig.MaxLvlNgoc1;
			if (cosData.TinhLuyen == 1)
			{
				num2 = ConfigManager.instance.OtherConfig.MaxLvlNgoc2;
			}
			else if (cosData.TinhLuyen == 2)
			{
				num = ConfigManager.instance.OtherConfig.MaxLvlNgoc2;
				num2 = ConfigManager.instance.OtherConfig.MaxLvlNgoc3;
			}
			else if (cosData.TinhLuyen == 3)
			{
				num = ConfigManager.instance.OtherConfig.MaxLvlNgoc3;
				num2 = ConfigManager.instance.OtherConfig.MaxLvlNgoc4;
			}
			else if (cosData.TinhLuyen > 3)
			{
				num = ConfigManager.instance.OtherConfig.MaxLvlNgoc4;
				num2 = ConfigManager.instance.OtherConfig.MaxLvlNgoc4;
			}
			lblKhamNgocDesc.text = string.Format(Localization.instance.Get("CostumeKhamNgocDesc2"), num, num2);
			lblTinhLuyenDesc.text = Localization.instance.Get("CostumeTinhLuyenDesc");
		}
		else
		{
			grpTinhLuyenPreview.SetActive(false);
			if (isReadOnly)
			{
				btnGo.SetActive(false);
				btnChange.SetActive(false);
				btnKhamNgoc.SetActive(false);
				btnTinhLuyen.SetActive(false);
				effMenhLabelTL.gameObject.SetActive(false);
				effNgoaiLabelTL.gameObject.SetActive(false);
				effThanLabelTL.gameObject.SetActive(false);
				effKhiLabelTL.gameObject.SetActive(false);
				lblKhamNgocDesc.text = string.Empty;
				lblTinhLuyenDesc.text = string.Empty;
			}
			else
			{
				btnGo.SetActive(true);
				btnChange.SetActive(true);
				btnKhamNgoc.SetActive(true);
				btnTinhLuyen.SetActive(true);
				lblKhamNgocDesc.gameObject.SetActive(false);
				lblTinhLuyenDesc.gameObject.SetActive(false);
			}
			costumeAvatar.gameObject.SetActive(true);
		}
	}

	private void OnChangeBtnClick()
	{
		if (m_costumeData == null)
		{
			return;
		}
		CostumeCfg value = null;
		if (ConfigManager.instance.m_dicCostumeCfg.TryGetValue(costumeAvatar.CodeName, out value))
		{
			UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
			UserInfo.HeroData hero = userInfo.HeroList.Find((UserInfo.HeroData e) => e.CostumeID == m_costumeData.ID);
			PopupSelectCostume.CreateForNhanVat(hero, OnFinishSelectCostume);
			DestroyPopup();
		}
	}

	private bool OnFinishSelectCostume(int costumeid, int hid)
	{
		if (hid > 0)
		{
			GameManager.instance.m_GameClient.RequestTakeOnCostume(costumeid, hid);
		}
		return true;
	}

	private void OnBackBtnClick()
	{
		DestroyPopup();
	}

	private void OnKhamNgocBtnClick()
	{
		if (m_costumeData != null)
		{
			GUIManager.setScreen(GAME_SCREEN.ScreenKhamNgocCostume);
			ScreenKhamNgocCostume screenKhamNgocCostume = GUIManager.getScreen(GAME_SCREEN.ScreenKhamNgocCostume) as ScreenKhamNgocCostume;
			screenKhamNgocCostume.SetInfo(m_costumeData);
			DestroyPopup();
		}
	}

	private void OnTinhLuyenBtnClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenCreateCostume);
		ScreenCreateCostume screenCreateCostume = GUIManager.getScreen(GAME_SCREEN.ScreenCreateCostume) as ScreenCreateCostume;
		screenCreateCostume.SetInfo(costumeAvatar.CodeName);
		DestroyPopup();
	}
}

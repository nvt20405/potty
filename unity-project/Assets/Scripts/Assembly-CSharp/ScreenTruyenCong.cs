using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTruyenCong : ScreenBase
{
	public NhanVatAvatar nhanvatAvaFrom;

	public UILabel nhanVatFromName;

	public NhanVatAvatar nhanvatAvaTo;

	public UILabel nhanVatToName;

	public OtherAvatar truyenCongDuocAva;

	public OtherAvatar truyenCongLinhDuocAva;

	public UILabel truyenCongDuocLabel;

	public UILabel truyenCongLinhDuocLabel;

	public UILabel soLuongCanTCDLabel;

	public UILabel soLuongCanTCLDLabel;

	public UILabel soLuongCoTCDLabel;

	public UILabel soLuongCoTCLDLabel;

	public UILabel expNhanTCDLabel;

	public UILabel expNhanTCLDLabel;

	public UILabel newLvlTCDLabel;

	public UILabel newLvlTCLDLabel;

	public UILabel boiDuongDanNhanLaiLabel;

	public UIButton btnTruyenCong;

	public UIButton btnTCCaoCap;

	private List<int> listHeroID = new List<int>();

	private UserInfo.HeroData m_HeroData;

	private UserInfo.HeroData m_HeroSelectedData;

	private UserInfo.VatPhamTieuThuData dataTCD;

	private UserInfo.VatPhamTieuThuData dataTCLD;

	public GameObject m_AnimTruyenCong;

	private void Start()
	{
		UIEventListener.Get(nhanvatAvaFrom.gameObject).onClick = nhanVat_onSelected;
		UIEventListener.Get(btnTCCaoCap.gameObject).onClick = btnTruyenCongCC_onSelected;
		UIEventListener.Get(btnTruyenCong.gameObject).onClick = btnTruyenCong_onSelected;
	}

	private void Update()
	{
	}

	public void Set(UserInfo.HeroData data)
	{
		if (data != null)
		{
			m_HeroData = data;
		}
	}

	public override void OnActive()
	{
		m_HeroSelectedData = null;
		m_AnimTruyenCong.SetActive(false);
		if (m_HeroData != null)
		{
			displayInfo();
		}
	}

	public void setNullAvatarFrom()
	{
		nhanvatAvaFrom.avatar.spriteName = "plus";
		nhanvatAvaFrom.lvlLabel.text = string.Empty;
		nhanVatFromName.text = string.Empty;
		if (!(nhanvatAvaFrom.groupLevelDotPha != null))
		{
			return;
		}
		foreach (Transform item in nhanvatAvaFrom.groupLevelDotPha.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
	}

	public void displayInfo()
	{
		nhanvatAvaTo.Set(m_HeroData);
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[m_HeroData.Name];
		nhanVatToName.text = nhanVatCfg.TenHienThi;
		setNullAvatarFrom();
		listHeroID.Clear();
		listHeroID.Add(m_HeroData.HID);
		VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu["VP_TRUYEN_CONG_1"];
		truyenCongDuocLabel.text = vatPhamTieuThuCfg.TenHienThi;
		dataTCD = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_TRUYEN_CONG_1");
		if (dataTCD != null)
		{
			truyenCongDuocAva.Set(dataTCD);
		}
		else
		{
			dataTCD = new UserInfo.VatPhamTieuThuData();
			dataTCD.Name = "VP_TRUYEN_CONG_1";
			dataTCD.Quantity = 0;
			truyenCongDuocAva.Set(dataTCD);
		}
		soLuongCoTCDLabel.text = Localization.instance.Get("HienCoLabel") + ": " + dataTCD.Quantity;
		expNhanTCDLabel.text = "[-]" + Localization.instance.Get("ExpNhanLabel") + ": [FFE400] 80%";
		newLvlTCDLabel.text = Localization.instance.Get("NewLevelLabel") + ": ";
		VatPhamTieuThuCfg vatPhamTieuThuCfg2 = ConfigManager.instance.m_dicVatPhamTieuThu["VP_TRUYEN_CONG_2"];
		truyenCongLinhDuocLabel.text = vatPhamTieuThuCfg2.TenHienThi;
		dataTCLD = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_TRUYEN_CONG_2");
		if (dataTCLD != null)
		{
			truyenCongLinhDuocAva.Set(dataTCLD);
		}
		else
		{
			dataTCLD = new UserInfo.VatPhamTieuThuData();
			dataTCLD.Name = "VP_TRUYEN_CONG_2";
			dataTCLD.Quantity = 0;
			truyenCongLinhDuocAva.Set(dataTCLD);
		}
		soLuongCoTCLDLabel.text = Localization.instance.Get("HienCoLabel") + ": " + dataTCLD.Quantity;
		expNhanTCLDLabel.text = "[-]" + Localization.instance.Get("ExpNhanLabel") + ": [FFE400] 100%";
		newLvlTCLDLabel.text = Localization.instance.Get("NewLevelLabel") + ": ";
		boiDuongDanNhanLaiLabel.gameObject.SetActive(false);
	}

	public bool OnSelectedNhanVat(int hero_id)
	{
		if (GameManager.instance.m_GameClient.UserInfo.HeroList != null && GameManager.instance.m_GameClient.UserInfo.HeroList.Count > 0)
		{
			for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.HeroList.Count; i++)
			{
				UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList[i];
				if (heroData.HID == hero_id)
				{
					m_HeroSelectedData = heroData;
				}
			}
		}
		if (m_HeroSelectedData != null)
		{
			nhanvatAvaFrom.Set(m_HeroSelectedData);
			NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[m_HeroSelectedData.Name];
			nhanVatFromName.text = nhanVatCfg.TenHienThi;
			newLvlTCDLabel.text = "[-]" + Localization.instance.Get("NewLevelLabel") + ": [FFE400]" + ConfigManager.instance.GetKetQuaTruyenCong(m_HeroData, m_HeroSelectedData, dataTCD).Level;
			newLvlTCLDLabel.text = "[-]" + Localization.instance.Get("NewLevelLabel") + ": [FFE400]" + ConfigManager.instance.GetKetQuaTruyenCong(m_HeroData, m_HeroSelectedData, dataTCLD).Level;
			int bddTruyenCongNhanDuoc = ConfigManager.instance.GetBddTruyenCongNhanDuoc(m_HeroSelectedData);
			if (bddTruyenCongNhanDuoc <= 0)
			{
				boiDuongDanNhanLaiLabel.gameObject.SetActive(false);
			}
			else
			{
				boiDuongDanNhanLaiLabel.gameObject.SetActive(true);
				boiDuongDanNhanLaiLabel.text = string.Format(Localization.instance.Get("BoiDuongDanNhanLaiLabel"), bddTruyenCongNhanDuoc);
			}
		}
		return true;
	}

	public void nhanVat_onSelected(GameObject go)
	{
		if (listHeroID.Count > 0)
		{
			PopupSelectNhanVat.Create(OnSelectedNhanVat, listHeroID);
		}
	}

	public void btnTruyenCongCC_onSelected(GameObject go)
	{
		if (m_HeroSelectedData != null)
		{
			TruyenCongRequest truyenCongRequest = new TruyenCongRequest();
			truyenCongRequest.DeTuNhanExpID = m_HeroData.HID;
			truyenCongRequest.DeTuRutExpID = m_HeroSelectedData.HID;
			truyenCongRequest.VatPhamID = dataTCLD.ID;
			GameClient gameClient = GameManager.instance.m_GameClient;
			gameClient.RequestTruyenCong(truyenCongRequest);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ChuaChonNhanVatTruyenCongMess"));
		}
	}

	public void btnTruyenCong_onSelected(GameObject go)
	{
		EGDebug.Log("hero ban dau: " + m_HeroData.ChiSoGoc.Menh + " - " + m_HeroData.ChiSoGoc.Ngoai + " - " + m_HeroData.ChiSoGoc.Noi + " - " + m_HeroData.ChiSoGoc.ThanPhap + " ;");
		if (m_HeroSelectedData != null)
		{
			TruyenCongRequest truyenCongRequest = new TruyenCongRequest();
			truyenCongRequest.DeTuNhanExpID = m_HeroData.HID;
			truyenCongRequest.DeTuRutExpID = m_HeroSelectedData.HID;
			truyenCongRequest.VatPhamID = dataTCD.ID;
			GameClient gameClient = GameManager.instance.m_GameClient;
			gameClient.RequestTruyenCong(truyenCongRequest);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ChuaChonNhanVatTruyenCongMess"));
		}
	}

	public void btnBack_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDeTu);
	}

	public void btnHelp_OnClick()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(0, 4);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
		EGDebug.Log("btnHelp_OnClick");
	}

	public void startPlayAnim()
	{
		m_AnimTruyenCong.SetActive(true);
		m_AnimTruyenCong.GetComponent<ParticleSystem>().Simulate(0f, true, true);
		m_AnimTruyenCong.GetComponent<ParticleSystem>().Play();
		StartCoroutine(updateTruyenCongInfo(3f));
	}

	public IEnumerator updateTruyenCongInfo(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		ScreenResultTruyenCong screen = GUIManager.getScreen(GAME_SCREEN.ScreenResultTruyenCong) as ScreenResultTruyenCong;
		screen.Set(m_HeroData);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenResultTruyenCong);
	}
}

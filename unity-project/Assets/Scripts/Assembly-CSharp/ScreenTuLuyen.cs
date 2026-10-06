using System.Collections;
using UnityEngine;

public class ScreenTuLuyen : ScreenBase
{
	public NhanVatAvatar nhanvatAva;

	public UILabel nhanVatName;

	public UILabel dotphaValueLabel;

	public UILabel chisogiatangValueLabel;

	public UILabel dotphaAfterValueLabel;

	public UILabel chisogiatangAfterValueLabel;

	public NhanVatAvatar tanhonAva;

	public UILabel tanhonName;

	public UILabel hienDangCo;

	public UILabel soLuongCan;

	public UIButton btnDotPha;

	private UserInfo.HeroData m_HeroData;

	private UserInfo.HonNhanVatData tanHonData;

	public GameObject m_AnimTuLuyen;

	public GameObject groupMaxDotPha;

	public GameObject groupNormalDotPha;

	public UILabel lbDotPhaCountMax;

	public UILabel lbChiSoDotPhaMax;

	private void Start()
	{
		UIEventListener.Get(btnDotPha.gameObject).onClick = btnDotPha_OnClick;
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
		m_AnimTuLuyen.SetActive(false);
		if (m_HeroData != null)
		{
			displayInfo();
		}
	}

	public void btnDotPha_OnClick(GameObject go)
	{
		GameClient gameClient = GameManager.instance.m_GameClient;
		if (m_HeroData != null)
		{
			if (tanHonData != null)
			{
				gameClient.RequestTuLuyenDeTu(m_HeroData.HID, tanHonData.ID);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("KhongCoTanHonMess"));
			}
		}
	}

	public void displayInfo(bool isTuLuyenUpdate = false)
	{
		nhanvatAva.Set(m_HeroData);
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[m_HeroData.Name];
		nhanVatName.text = nhanVatCfg.TenHienThi;
		int capDotPha = m_HeroData.CapDotPha;
		if (capDotPha >= 3)
		{
			groupNormalDotPha.gameObject.SetActive(false);
			groupMaxDotPha.gameObject.SetActive(true);
			lbDotPhaCountMax.text = capDotPha.ToString();
			lbChiSoDotPhaMax.text = ConfigManager.instance.GetChiSoDeTuGiaTangByDotPhaLvl(m_HeroData.Name, capDotPha) + "%";
			return;
		}
		groupNormalDotPha.gameObject.SetActive(true);
		groupMaxDotPha.gameObject.SetActive(false);
		tanhonAva.Set(m_HeroData.Name, 0, -1, false, -1, true);
		tanhonName.text = nhanVatCfg.TenHienThi;
		dotphaValueLabel.text = capDotPha.ToString();
		dotphaAfterValueLabel.text = (capDotPha + 1).ToString();
		chisogiatangValueLabel.text = ConfigManager.instance.GetChiSoDeTuGiaTangByDotPhaLvl(m_HeroData.Name, capDotPha) + "%";
		chisogiatangAfterValueLabel.text = ConfigManager.instance.GetChiSoDeTuGiaTangByDotPhaLvl(m_HeroData.Name, capDotPha + 1) + "%";
		int tanHonCanDotPha = ConfigManager.instance.GetTanHonCanDotPha(m_HeroData.Name, capDotPha);
		soLuongCan.text = "X" + tanHonCanDotPha;
		tanHonData = GameManager.instance.m_GameClient.UserInfo.HonNhanVatList.Find((UserInfo.HonNhanVatData e) => e.Name == m_HeroData.Name);
		int num = ((tanHonData != null) ? tanHonData.Quantity : 0);
		hienDangCo.text = Localization.instance.Get("HienDangCo") + ": [F4C500]" + num;
	}

	public void updateDeTuInfo()
	{
		startPlayAnim();
		m_HeroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == m_HeroData.HID);
		StartCoroutine(updateInfoAgain(1.8f));
	}

	public IEnumerator updateInfoAgain(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		displayInfo(true);
	}

	public void btnBack_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDeTu);
	}

	public void startPlayAnim()
	{
		m_AnimTuLuyen.SetActive(true);
		m_AnimTuLuyen.GetComponent<ParticleSystem>().Simulate(0f, true, true);
		m_AnimTuLuyen.GetComponent<ParticleSystem>().Play();
	}

	public void btnHelp_OnClick()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(0, 6);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
		EGDebug.Log("btnHelp_OnClick");
	}

	public void btnDongY_OnClick()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDeTu);
	}
}

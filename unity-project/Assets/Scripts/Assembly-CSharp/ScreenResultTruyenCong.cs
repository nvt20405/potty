using UnityEngine;

public class ScreenResultTruyenCong : ScreenBase
{
	public NhanVatAvatar nhanVatAvatar;

	public UILabel nhanVatName;

	public UISlider expBeforeProgressBar;

	public UILabel menhBeforeLabel;

	public UILabel ngoaiBeforeLabel;

	public UILabel khiBeforeLabel;

	public UILabel thanBeforeLabel;

	public UISlider expAfterProgressBar;

	public UILabel menhAfterLabel;

	public UILabel ngoaiAfterLabel;

	public UILabel khiAfterLabel;

	public UILabel thanAfterLabel;

	public UILabel expBeforeValueLabel;

	public UILabel expAfterValueLabel;

	public UIButton btnTiepTuc;

	private UserInfo.HeroData m_HeroData;

	private UserInfo.HeroData m_HeroDataNew;

	private void Start()
	{
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
		if (m_HeroData != null)
		{
			displayInfo();
		}
	}

	public void displayInfo()
	{
		menhBeforeLabel.text = m_HeroData.ChiSoGoc.Menh.ToString();
		ngoaiBeforeLabel.text = m_HeroData.ChiSoGoc.Ngoai.ToString();
		khiBeforeLabel.text = m_HeroData.ChiSoGoc.Noi.ToString();
		thanBeforeLabel.text = m_HeroData.ChiSoGoc.ThanPhap.ToString();
		expBeforeProgressBar.sliderValue = (float)m_HeroData.Exp / (float)m_HeroData.MaxExp;
		EGDebug.Log("expBeforeProgressBar - sliderValue: " + m_HeroData.Exp / m_HeroData.MaxExp);
		expBeforeValueLabel.text = m_HeroData.Exp + "/" + m_HeroData.MaxExp;
		m_HeroDataNew = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == m_HeroData.HID);
		nhanVatAvatar.Set(m_HeroDataNew);
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[m_HeroDataNew.Name];
		nhanVatName.text = nhanVatCfg.TenHienThi;
		menhAfterLabel.text = m_HeroDataNew.ChiSoGoc.Menh.ToString();
		ngoaiAfterLabel.text = m_HeroDataNew.ChiSoGoc.Ngoai.ToString();
		khiAfterLabel.text = m_HeroDataNew.ChiSoGoc.Noi.ToString();
		thanAfterLabel.text = m_HeroDataNew.ChiSoGoc.ThanPhap.ToString();
		expAfterProgressBar.sliderValue = (float)m_HeroDataNew.Exp / (float)m_HeroDataNew.MaxExp;
		EGDebug.Log("expAfterProgressBar - sliderValue: " + m_HeroDataNew.Exp / m_HeroDataNew.MaxExp);
		expAfterValueLabel.text = m_HeroDataNew.Exp + "/" + m_HeroDataNew.MaxExp;
	}

	public void btnTiepTuc_onClick(GameObject go)
	{
		ScreenTruyenCong screenTruyenCong = GUIManager.getScreen(GAME_SCREEN.ScreenTruyenCong) as ScreenTruyenCong;
		screenTruyenCong.Set(m_HeroDataNew);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTruyenCong);
	}
}

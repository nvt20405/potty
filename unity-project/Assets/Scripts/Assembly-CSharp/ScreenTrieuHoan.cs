public class ScreenTrieuHoan : ScreenBase
{
	public UILabel tanHonHienCoLabel;

	public UserInfo.HeroData heroData { get; set; }

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void btnBack_OnClick()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDoiHinh);
		ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
		screenDoiHinh.TurnOnNhanVatGroup();
	}

	public void btnTrieuHoan_OnClick()
	{
		GameManager.instance.m_GameClient.RequestTrieuHoiChienHon(heroData.HID);
	}

	public override void OnActive()
	{
		base.OnActive();
	}

	public void Set(UserInfo.HeroData hr)
	{
		heroData = hr;
		if (GameManager.instance.m_GameClient.UserInfo.HonNhanVatList != null)
		{
			UserInfo.HonNhanVatData honNhanVatData = GameManager.instance.m_GameClient.UserInfo.HonNhanVatList.Find((UserInfo.HonNhanVatData e) => e.Name == heroData.Name);
			if (honNhanVatData != null)
			{
				tanHonHienCoLabel.text = honNhanVatData.Quantity + "/" + ConfigManager.instance.OtherConfig.SoHonTrieuHoiChienHon;
			}
			else
			{
				tanHonHienCoLabel.text = "0/" + ConfigManager.instance.OtherConfig.SoHonTrieuHoiChienHon;
			}
		}
		else
		{
			tanHonHienCoLabel.text = "0/" + ConfigManager.instance.OtherConfig.SoHonTrieuHoiChienHon;
		}
	}
}

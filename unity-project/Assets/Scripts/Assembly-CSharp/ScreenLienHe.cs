using UnityEngine;

public class ScreenLienHe : ScreenBase
{
	public UILabel nhaPhatHanhMesLabel;

	public UILabel webLabel;

	public UILabel hotroLabel;

	public GameObject publisherGrp;

	private void Awake()
	{
		publisherGrp.SetActive(true);
		nhaPhatHanhMesLabel.text = Localization.instance.Get("NhaPhatHanhMessLabel");
		webLabel.text = Localization.instance.Get("WebMessLabel");
		hotroLabel.text = Localization.instance.Get("DichVuMessLabel");
	}

	public void btnClose_OnClick()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenSettings);
	}
}

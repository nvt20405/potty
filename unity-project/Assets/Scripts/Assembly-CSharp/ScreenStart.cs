public class ScreenStart : ScreenBase
{
	public UILabel VersionLabel;

	public override void OnActive()
	{
		base.OnActive();
		int num = GameManager.GAMER_VERSION / 10000;
		int num2 = GameManager.GAMER_VERSION % 10000 / 100;
		int num3 = GameManager.GAMER_VERSION % 100;
		VersionLabel.text = Localization.instance.Get("GameVersion") + string.Format(" {0}.{1}.{2}", num, num2, num3);
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	private void OnStartBtnClick()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenLogin);
	}
}

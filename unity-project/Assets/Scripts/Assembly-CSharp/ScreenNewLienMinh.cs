public class ScreenNewLienMinh : ScreenBase
{
	private void Start()
	{
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
	}

	private void Update()
	{
	}

	public void CreateLienMinh()
	{
		PopUpLapLienMinh.Create();
	}

	public void JoinLienMinh()
	{
		GameManager.instance.m_GameClient.RequestGetTopLienMinh();
		ScreenLienMinh.ScreenStatus = "ScreenLienMinhGiaNhap";
	}

	private void HelpLienMinh()
	{
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(8, 0);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
		EGDebug.Log("btnHelp_OnClick");
	}
}

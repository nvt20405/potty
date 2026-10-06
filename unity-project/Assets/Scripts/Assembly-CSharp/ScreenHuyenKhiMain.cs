public class ScreenHuyenKhiMain : ScreenBase
{
	public void OnCheTacClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenCheTacHuyenKhi);
	}

	public void OnDanhSachClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenDanhSachHuyenKhi);
	}
}

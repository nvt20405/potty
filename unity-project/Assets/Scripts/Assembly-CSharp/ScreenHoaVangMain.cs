using UnityEngine;

public class ScreenHoaVangMain : ScreenBase
{
	public void HoaVang_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenListHoaVang);
	}

	public void QuayThuong_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenQuayDiemHoaVang);
	}
}

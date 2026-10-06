using UnityEngine;

public class ScreenThienMaThuongPhong : ScreenBase
{
	public void CheTac_OnClick(GameObject go)
	{
		PopupThienMaThuongPhong.Create(null, -1, ThienMaAction.CHE_TAC, Localization.instance.Get("CheTacDescription"), "VP_THIEN_MA_TAN_PHIEN");
	}

	public void BaoKhi_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenListBaoKhi);
	}

	public void btnBack_OnClick(GameObject go)
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenThienMaMain);
	}
}

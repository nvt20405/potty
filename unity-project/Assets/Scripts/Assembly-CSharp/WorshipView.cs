using UnityEngine;

public class WorshipView : MonoBehaviour
{
	public void OnBackToMainClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenMain);
	}

	public void OnThamBaiBtnClick()
	{
		GameManager.instance.m_GameClient.RequestThamBaiSieuCup();
	}
}

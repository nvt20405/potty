using UnityEngine;

public class SonMonBattleRound : MonoBehaviour
{
	public UISprite HeroAvatar1;

	public UISprite HeroBg1;

	public UISprite WinIcon1;

	public UISprite HeroAvatar2;

	public UISprite HeroBg2;

	public UISprite WinIcon2;

	public TweenScale button;

	private BattleReplay replay;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Set(BattleReplay replay, string nhanvat1, string nhanvat2, int winState, bool showViewBtn, bool showAnimBtn = false)
	{
		this.replay = replay;
		HeroAvatar1.spriteName = nhanvat1;
		HeroAvatar2.spriteName = nhanvat2;
		if (nhanvat1.Contains("_TT"))
		{
			HeroBg1.spriteName = "bkg_avatar5";
		}
		else
		{
			HeroBg1.spriteName = "bkg_avatar" + ConfigManager.instance.m_dicNhanVats[nhanvat1].Hang;
		}
		if (nhanvat2.Contains("_TT"))
		{
			HeroBg2.spriteName = "bkg_avatar5";
		}
		else
		{
			HeroBg2.spriteName = "bkg_avatar" + ConfigManager.instance.m_dicNhanVats[nhanvat2].Hang;
		}
		switch (winState)
		{
		case 0:
			WinIcon1.gameObject.SetActive(false);
			WinIcon2.gameObject.SetActive(false);
			break;
		case 1:
			WinIcon1.gameObject.SetActive(true);
			WinIcon2.gameObject.SetActive(false);
			break;
		case 2:
			WinIcon1.gameObject.SetActive(false);
			WinIcon2.gameObject.SetActive(true);
			break;
		}
		if (showAnimBtn)
		{
		}
		if (showViewBtn)
		{
			button.gameObject.SetActive(true);
		}
		else
		{
			button.gameObject.SetActive(false);
		}
	}

	public void Replay()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenBattle);
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		screenBattle.Replay(replay);
		screenBattle.OnFinishReplay += base.transform.parent.parent.GetComponent<PopupSonMonBattle>().ShowNextResult;
		if (replay.Winner == 1)
		{
			WinIcon1.gameObject.SetActive(true);
			WinIcon2.gameObject.SetActive(false);
		}
		else
		{
			WinIcon1.gameObject.SetActive(false);
			WinIcon2.gameObject.SetActive(true);
		}
		base.transform.parent.parent.GetComponent<PopupSonMonBattle>().HidePopup();
		base.transform.parent.parent.GetComponent<PopupSonMonBattle>().UpdateScore();
	}
}

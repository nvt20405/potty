using UnityEngine;

public class ScreenBase : MonoBehaviour
{
	public GAME_SCREEN screenId;

	public GameObject child3Dscreen;

	public bool isFirstTimeInitialized;

	public virtual void OnActive()
	{
		if (!isFirstTimeInitialized)
		{
			isFirstTimeInitialized = true;
			firstTimeInit();
		}
		if (child3Dscreen != null)
		{
			child3Dscreen.SetActive(true);
		}
	}

	public virtual void OnDeactive()
	{
		if (child3Dscreen != null)
		{
			child3Dscreen.SetActive(false);
		}
	}

	protected virtual void firstTimeInit()
	{
	}

	public void OnBuyHatGiong()
	{
		PopupChonHatGiong.Create();
	}

	public void OnTromLinhDuoc()
	{
		PopupAnTromLinhDuoc.Create();
	}
}

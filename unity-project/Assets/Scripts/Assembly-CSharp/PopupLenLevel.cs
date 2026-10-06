using UnityEngine;

public class PopupLenLevel : MonoBehaviour
{
	public static PopupLenLevel instance;

	public UILabel thongBaoLabel;

	public UILabel knbLabel;

	public UILabel bacLabel;

	public GameObject group;

	private int Level;

	public static PopupLenLevel Create(int level)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("Popup/PopupLenLevel"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupLenLevel>();
		instance.Set(level);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		return instance;
	}

	public void Set(int level)
	{
		Level = level;
		knbLabel.text = ConfigManager.instance.GetLenLevelThuongKNB(level).ToString();
		bacLabel.text = ConfigManager.instance.GetLenLevelThuongBac(level).ToString();
		int numOpenDoiHinhSlotByLevel = ConfigManager.instance.GetNumOpenDoiHinhSlotByLevel(level);
		thongBaoLabel.text = string.Format(Localization.instance.Get("PopupLenLevelThongBao"), level, numOpenDoiHinhSlotByLevel);
		NGUITools.SetActive(group, false);
		if (level == 12)
		{
			GameManager.instance.m_GameClient.GetDongNhanInfo();
		}
	}

	public void Update()
	{
		if (GUIManager.instance.CurrentScreen == GAME_SCREEN.ScreenBattle)
		{
			NGUITools.SetActive(group, false);
		}
		else
		{
			NGUITools.SetActive(group, true);
		}
	}

	public void OnCloseClick()
	{
		PopupTinhNangMoi.CreateByLevel(Level);
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			instance.gameObject.SetActive(false);
			Object.Destroy(instance.gameObject);
		}
		instance = null;
	}
}

using UnityEngine;

public class PopupThachDau : MonoBehaviour
{
	public UILabel messageLabel;

	public UILabel cuoc1Label;

	public UILabel cuoc2Label;

	public UILabel cuoc3Label;

	public static PopupThachDau instance;

	private int enemyId;

	private int tienCuoc;

	public static PopupThachDau Create(int enemyId, string enemyName, int level)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("Popup/PopupThachDau"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupThachDau>();
		instance.SetInfo(enemyId, enemyName, level);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		return instance;
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

	public void SetInfo(int enemyId, string enemyName, int level)
	{
		this.enemyId = enemyId;
		messageLabel.text = string.Format(Localization.instance.Get("ThachDauPopupLabel"), enemyName);
		cuoc1Label.text = ConfigManager.GetTienCuocThachDauByLevel(level).ToString();
	}

	private void OnYesClick()
	{
		GameManager.instance.m_GameClient.RequestThachDau(enemyId, tienCuoc);
		DestroyPopup();
	}

	private void OnNoClick()
	{
		DestroyPopup();
	}

	private void OnActivateCuoc0(bool isActive)
	{
		if (isActive)
		{
			tienCuoc = 0;
		}
	}

	private void OnActivateCuoc1(bool isActive)
	{
		if (isActive)
		{
			tienCuoc = 1;
		}
	}

	private void OnActivateCuoc2(bool isActive)
	{
		if (isActive)
		{
			tienCuoc = 2;
		}
	}

	private void OnActivateCuoc3(bool isActive)
	{
		if (isActive)
		{
			tienCuoc = 3;
		}
	}
}

using UnityEngine;

public class PopupNhanDuocThachDau : MonoBehaviour
{
	public UILabel messageLabel;

	public UILabel cuoc1Label;

	public static PopupNhanDuocThachDau instance;

	private int enemyId;

	public static PopupNhanDuocThachDau Create(int enemyId, string enemyName, int tienCuoc)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("popup/PopupNhanDuocThachDau"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupNhanDuocThachDau>();
		instance.SetInfo(enemyId, enemyName, tienCuoc);
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

	public void SetInfo(int enemyId, string enemyName, int tienCuoc)
	{
		this.enemyId = enemyId;
		messageLabel.text = string.Format(Localization.instance.Get("NhanDuocThachDauPopupLabel"), enemyName);
		cuoc1Label.text = tienCuoc.ToString();
	}

	private void OnYesClick()
	{
		GameManager.instance.m_GameClient.RequestNhanThachDau(enemyId, 1);
		DestroyPopup();
	}

	private void OnNoClick()
	{
		GameManager.instance.m_GameClient.RequestNhanThachDau(enemyId, 0);
		DestroyPopup();
	}

	private void OnTuChoiGioClick()
	{
		GameManager.instance.m_GameClient.RequestNhanThachDau(enemyId, -1);
		DestroyPopup();
	}
}

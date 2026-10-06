using UnityEngine;

public class PopupUser1Tr : MonoBehaviour
{
	public static PopupUser1Tr instance;

	public GameObject group;

	public UILabel countLabel;

	public GameObject SpecialGrp;

	public GameObject NormalGrp;

	public PhanThuongResponse phanthuong;

	public static PopupUser1Tr Create(bool isSpecial, int count, PhanThuongResponse phanthuong = null)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("Popup/PopupUser1Tr"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupUser1Tr>();
		instance.Set(isSpecial, count, phanthuong);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		return instance;
	}

	public void Set(bool isSpecial, int count, PhanThuongResponse phanthuong = null)
	{
		if (isSpecial)
		{
			SpecialGrp.SetActive(true);
			NormalGrp.SetActive(false);
		}
		else
		{
			SpecialGrp.SetActive(false);
			NormalGrp.SetActive(true);
		}
		this.phanthuong = phanthuong;
		countLabel.text = count.ToString();
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
		DestroyPopup();
	}

	public void OnNhanThuongClick()
	{
		if (phanthuong != null)
		{
			PopupDanhSachPhanThuong.Create(string.Empty, string.Empty, phanthuong);
		}
		else
		{
			GameManager.instance.m_GameClient.RequestNhanThuong1MilUser();
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

using System;
using UnityEngine;

public class PopupDanhSachPhanThuong : MonoBehaviour
{
	public GameObject ItemRoot;

	public GameObject ItemPrefab;

	public UILabel popupLabel;

	public UILabel descLabel;

	private Action onRelease;

	public static PopupDanhSachPhanThuong instance;

	public static void Release(bool callBack = false)
	{
		if (instance != null)
		{
			if (callBack && instance.onRelease != null)
			{
				instance.onRelease();
			}
			UnityEngine.Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	private void OnCloseBtnClick()
	{
		Release(true);
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
	}

	public static PopupDanhSachPhanThuong Create(string popuptext, string desctext, PhanThuongResponse res, Action onRelease = null)
	{
		Release();
		instance = ((GameObject)UnityEngine.Object.Instantiate(Resources.Load("Popup/PopupDanhSachPhanThuong"))).GetComponent<PopupDanhSachPhanThuong>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = Vector3.one;
		instance.onRelease = onRelease;
		instance.Set(popuptext, desctext, res);
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		return instance;
	}

	public void Set(string popuptext, string desctext, PhanThuongResponse res)
	{
		if (string.IsNullOrEmpty(res.PhanThuongTitle))
		{
			popupLabel.text = popuptext;
		}
		else
		{
			popupLabel.text = res.PhanThuongTitle;
		}
		if (string.IsNullOrEmpty(res.PhanThuongDesc))
		{
			descLabel.text = desctext;
		}
		else
		{
			descLabel.text = res.PhanThuongDesc;
		}
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			UnityEngine.Object.Destroy(transform2.gameObject);
		}
		Vector3 vector = default(Vector3);
		vector = new Vector3(-150f, 160f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -130f, 0f);
		foreach (PhanThuongResponse.PhanThuong phanThuong in res.PhanThuongList)
		{
			PhanThuongItem component = ((GameObject)UnityEngine.Object.Instantiate(ItemPrefab)).GetComponent<PhanThuongItem>();
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = Vector3.one;
			component.transform.localPosition = vector;
			vector += vector2;
			component.Set(phanThuong, true);
		}
	}
}

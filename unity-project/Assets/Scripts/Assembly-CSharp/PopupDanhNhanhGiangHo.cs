using System.Collections.Generic;
using UnityEngine;

public class PopupDanhNhanhGiangHo : MonoBehaviour
{
	public UIPanel panel;

	public static PopupDanhNhanhGiangHo instance;

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static PopupDanhNhanhGiangHo Create(List<UserInfo.HeroData> listDeTu, GiangHoDanhNhanhResponse response)
	{
		DestroyPopup();
		Object obj = Object.Instantiate(Resources.Load("popup/PopupDanhNhanhGiangHo"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupDanhNhanhGiangHo>();
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 312f, 0f);
		for (int i = 0; i < response.PhanThuong.Count; i++)
		{
			Object obj2 = Object.Instantiate(Resources.Load("giangho/DanhNhanhGHItem"));
			GameObject gameObject2 = (GameObject)((obj2 is GameObject) ? obj2 : null);
			DanhNhanhGHItem component = gameObject2.GetComponent<DanhNhanhGHItem>();
			component.transform.parent = instance.panel.transform;
			component.transform.localScale = Vector3.one;
			component.transform.localPosition = vector;
			component.SetInfo(listDeTu, response.ExpMP[i], response.BacReward[i], response.ExpDeTu[i], response.PhanThuong[i]);
			vector -= new Vector3(0f, component.HeightBG, 0f);
		}
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		return instance;
	}

	private void OnCloseClick()
	{
		DestroyPopup();
	}
}

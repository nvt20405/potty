using System.Collections.Generic;
using UnityEngine;

public class ScreenBXHBanhChung : ScreenBase
{
	public GameObject itemObj;

	public UIPanel panel;

	private BanhChungBXHResponse response;

	private List<BXHBanhChungItem> listItem = new List<BXHBanhChungItem>();

	private bool homNay = true;

	public UILabel soBanh;

	private void OnHomNayBtnClick()
	{
		homNay = true;
		SyncWithNetworkData(response);
	}

	private void OnHomTruocBtnClick()
	{
		homNay = false;
		SyncWithNetworkData(response);
	}

	public void SyncWithNetworkData(BanhChungBXHResponse response)
	{
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, 172f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -156f, 0f);
		for (int i = response.ListPhanThuong.Count; i < listItem.Count; i++)
		{
			listItem[i].gameObject.SetActive(false);
			Object.Destroy(listItem[i].gameObject);
		}
		for (int j = 0; j < response.ListPhanThuong.Count; j++)
		{
			if (listItem.Count > j)
			{
				if (homNay)
				{
					listItem[j].Set((response.ListHomNay.Count <= j) ? null : response.ListHomNay[j], j + 1, response.ListPhanThuong[j]);
				}
				else
				{
					listItem[j].Set((response.ListHomTruoc.Count <= j) ? null : response.ListHomTruoc[j], j + 1, response.ListPhanThuong[j]);
				}
				continue;
			}
			Object obj = Object.Instantiate(itemObj);
			GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
			gameObject.transform.parent = panel.transform;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localPosition = vector + vector2 * j;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.SetActive(true);
			BXHBanhChungItem component = gameObject.GetComponent<BXHBanhChungItem>();
			if (homNay)
			{
				component.Set((response.ListHomNay.Count <= j) ? null : response.ListHomNay[j], j + 1, response.ListPhanThuong[j]);
			}
			else
			{
				component.Set((response.ListHomTruoc.Count <= j) ? null : response.ListHomTruoc[j], j + 1, response.ListPhanThuong[j]);
			}
			listItem.Add(component);
		}
		this.response = response;
		soBanh.text = string.Format(Localization.instance.Get("LabelSoBanhHomNay"), response.Player.SoBanh);
	}

	private void OnCloseBtnClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenNauBanhChung);
	}
}

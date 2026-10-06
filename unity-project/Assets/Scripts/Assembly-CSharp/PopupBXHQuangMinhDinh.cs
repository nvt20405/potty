using System.Collections.Generic;
using UnityEngine;

public class PopupBXHQuangMinhDinh : MonoBehaviour
{
	public UIPanel clipPanel;

	public static PopupBXHQuangMinhDinh instance;

	private CT2BXHTuanResponse response;

	private List<BXHTuanChinhTaRow> bangXepHang = new List<BXHTuanChinhTaRow>();

	public static PopupBXHQuangMinhDinh Create(CT2BXHTuanResponse response)
	{
		DestroyPopup();
		Object obj = Object.Instantiate(Resources.Load("Popup/PopupBXHQuangMinhDinh"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupBXHQuangMinhDinh>();
		instance.SetInfo(response);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		return instance;
	}

	private void SetTop(List<CT2BXHTuanResponse.TopMonPhai> top)
	{
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, -30f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -58f, 0f);
		for (int i = top.Count; i < bangXepHang.Count; i++)
		{
			bangXepHang[i].gameObject.SetActive(false);
		}
		int num = 0;
		foreach (CT2BXHTuanResponse.TopMonPhai item in top)
		{
			BXHTuanChinhTaRow bXHTuanChinhTaRow = null;
			if (bangXepHang.Count > num)
			{
				bXHTuanChinhTaRow = bangXepHang[num];
				bangXepHang[num].gameObject.SetActive(true);
			}
			else
			{
				Object obj = Object.Instantiate(Resources.Load("Popup/BXHTuanChinhTaRow"));
				GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
				gameObject.transform.parent = clipPanel.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.identity;
				bXHTuanChinhTaRow = gameObject.GetComponent<BXHTuanChinhTaRow>();
				bangXepHang.Add(bXHTuanChinhTaRow);
			}
			bXHTuanChinhTaRow.SetInfo(item.DanhHieu, item.Vip, item.Ten, item.DiemChienTich, item.PhanThuong);
			bXHTuanChinhTaRow.transform.localPosition = vector + vector2 * num;
			num++;
		}
	}

	public void SetInfo(CT2BXHTuanResponse response)
	{
		this.response = response;
		SetTop(response.ListTop);
	}

	private void OnBackBtnClick()
	{
		DestroyPopup();
	}

	private void OnHelpBtnClick()
	{
		DestroyPopup();
		ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
		screenHelpInfo.setByLevel(6, 5);
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}
}

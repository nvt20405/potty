using System.Collections.Generic;
using UnityEngine;

public class ScreenQuanLyLienMinh : ScreenBase
{
	public UILabel TenLienMinh;

	public UILabel LienMinhStatus;

	public GameObject ActivityItem;

	public GameObject ActivityRoot;

	public List<GameObject> itemList;

	public int ItemSize;

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
	}

	public void OnEnable()
	{
		ScreenLienMinh.ScreenStatus = "ScreenQuanLyLienMinh";
		GameManager.instance.m_GameClient.RequestGetTopLienMinh();
		TenLienMinh.text = GameManager.instance.m_GameClient.UserInfo.LienMinh.DisplayName;
		LienMinhStatus.text = string.Format(Localization.instance.Get("LienMinhStatus"), GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel, GameManager.instance.m_GameClient.UserInfo.LienMinh.ThanhVienList.Count, GameManager.instance.m_GameClient.UserInfo.LienMinh.DiemCongHien, ConfigManager.instance.CongTrinhLienMinhConfig["TuNghiaDuong"].Value[GameManager.instance.m_GameClient.UserInfo.LienMinh.TuNghiaLevel]);
		int num = 0;
		for (int num2 = GameManager.instance.m_GameClient.UserInfo.LienMinhActivities.Contents.Count - 1; num2 >= 0; num2--)
		{
			string text = GameManager.instance.m_GameClient.UserInfo.LienMinhActivities.Contents[num2];
			num++;
			GameObject gameObject;
			if (num > itemList.Count)
			{
				Object obj = Object.Instantiate(ActivityItem);
				gameObject = (GameObject)((obj is GameObject) ? obj : null);
				itemList.Add(gameObject);
			}
			else
			{
				gameObject = itemList[num - 1];
			}
			gameObject.transform.parent = ActivityRoot.transform;
			gameObject.transform.localScale = ActivityItem.transform.localScale;
			gameObject.transform.localPosition = new Vector3(0f, -ItemSize * (num - 2), 0f);
			gameObject.transform.Find("Label").GetComponent<UILabel>().text = text;
			gameObject.name = num.ToString();
		}
		ActivityRoot.GetComponent<UIDraggablePanel>().ResetPosition();
		ActivityItem.SetActive(false);
	}

	private void OnQuanLyThanhVien()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenQuanLyThanhVienLienMinh);
		ScreenLienMinh.ScreenQuanLyThanhVienLienMinh = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenQuanLyThanhVienLienMinh) as ScreenQuanLyThanhVienLienMinh;
	}

	private void OnQuanLyCongTrinh()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenQuanLyCongTrinhLienMinh);
		ScreenLienMinh.ScreenQuanLyCongTrinhLienMinh = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenQuanLyCongTrinhLienMinh) as ScreenQuanLyCongTrinhLienMinh;
	}

	private void OnDoiTenBang()
	{
		PopupDoiTenBang.Create();
	}
}

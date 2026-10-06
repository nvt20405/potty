using System.Collections.Generic;
using UnityEngine;

public class PopupBXHChinhTa : MonoBehaviour
{
	public UIPanel clipPanel;

	public UILabel team1Label;

	public UILabel team2Label;

	public UISprite team1Bg;

	public UISprite team2Bg;

	public GameObject team1Win;

	public GameObject team2Win;

	public static PopupBXHChinhTa instance;

	private CT2BXHHienTaiResponse response;

	private int teamWinner;

	private List<BXHChinhTaRow> bangXepHang = new List<BXHChinhTaRow>();

	public static PopupBXHChinhTa Create(CT2BXHHienTaiResponse response, int teamWinner)
	{
		DestroyPopup();
		Object obj = Object.Instantiate(Resources.Load("Popup/PopupBXHChinhTa"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		instance = gameObject.GetComponent<PopupBXHChinhTa>();
		instance.SetInfo(response, teamWinner);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		return instance;
	}

	private void SetTop(int viewTeam, List<CT2BXHHienTaiResponse.TopMonPhai> top)
	{
		team1Label.effectStyle = ((viewTeam == 0) ? UILabel.Effect.Outline : UILabel.Effect.None);
		team1Label.effectDistance = new Vector2(2f, 2f);
		team1Label.color = ((viewTeam != 0) ? Color.white : Color.yellow);
		team1Bg.color = ((viewTeam != 0) ? Color.gray : Color.white);
		team2Label.effectStyle = ((viewTeam == 1) ? UILabel.Effect.Outline : UILabel.Effect.None);
		team2Label.effectDistance = new Vector3(2f, 2f);
		team2Label.color = ((viewTeam != 1) ? Color.white : Color.yellow);
		team2Bg.color = ((viewTeam != 1) ? Color.gray : Color.white);
		Vector3 vector = default(Vector3);
		vector = new Vector3(0f, -30f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(0f, -58f, 0f);
		for (int i = top.Count; i < bangXepHang.Count; i++)
		{
			bangXepHang[i].gameObject.SetActive(false);
		}
		int num = 0;
		foreach (CT2BXHHienTaiResponse.TopMonPhai item in top)
		{
			BXHChinhTaRow bXHChinhTaRow = null;
			if (bangXepHang.Count > num)
			{
				bXHChinhTaRow = bangXepHang[num];
				bangXepHang[num].gameObject.SetActive(true);
			}
			else
			{
				Object obj = Object.Instantiate(Resources.Load("Popup/BXHChinhTaRow"));
				GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
				gameObject.transform.parent = clipPanel.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.identity;
				bXHChinhTaRow = gameObject.GetComponent<BXHChinhTaRow>();
				bangXepHang.Add(bXHChinhTaRow);
			}
			bXHChinhTaRow.SetInfo(item.Hang, item.Vip, item.Ten, item.ServerId, item.Diem, item.Kill);
			bXHChinhTaRow.transform.localPosition = vector + vector2 * num;
			num++;
		}
	}

	public void SetInfo(CT2BXHHienTaiResponse response, int teamWinner)
	{
		this.response = response;
		SetTop(response.TeamPlayer, (response.TeamPlayer != 0) ? response.ListTopTaPhai : response.ListTopChinhPhai);
		team1Win.SetActive(teamWinner == 0);
		team2Win.SetActive(teamWinner == 1);
	}

	private void OnChinhPhaiBtnClick()
	{
		if (response != null)
		{
			SetTop(0, response.ListTopChinhPhai);
		}
	}

	private void OnTaPhaiBtnClick()
	{
		if (response != null)
		{
			SetTop(1, response.ListTopTaPhai);
		}
	}

	private void OnBackBtnClick()
	{
		DestroyPopup();
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

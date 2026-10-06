using System.Collections.Generic;
using UnityEngine;

public class SuperCupView : MonoBehaviour
{
	public UIPanel listVongDauPanel;

	public UIPanel listTranDauPanel;

	public GameObject btnVongDauObj;

	public GameObject grpTranDauObj;

	public GameObject grpTranDauActions;

	public Transform vongDauParent;

	public UILabel thongBaoLabel;

	private GetSieuCupInfoResponse responseData;

	private List<CupRoundBtn> listVongDauButtons = new List<CupRoundBtn>();

	private int actionIdx = -1;

	private int curVongDau;

	private List<GameObject> listTranDauObjs = new List<GameObject>();

	public void SetInfo(GetSieuCupInfoResponse response)
	{
		thongBaoLabel.text = response.ThongBao;
		thongBaoLabel.transform.localScale = new Vector3(32f, 32f, 1f);
		int vongDau = 1;
		List<SieuCupGame> list = response.ListTranDau.FindAll((SieuCupGame e) => e.VongDau == vongDau);
		listVongDauPanel.GetComponent<UIDraggablePanel>().ResetPosition();
		while (list.Count > 0)
		{
			if (vongDau > listVongDauButtons.Count)
			{
				Object obj = Object.Instantiate(btnVongDauObj);
				GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
				gameObject.transform.parent = vongDauParent;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = new Vector3(100f + (float)(vongDau - 1) * 180f, 0f, 0f);
				gameObject.SetActive(true);
				CupRoundBtn component = gameObject.GetComponent<CupRoundBtn>();
				component.label.text = string.Format(Localization.instance.Get("VongDauBtn"), vongDau);
				component.VongDau = vongDau;
				component.view = this;
				listVongDauButtons.Add(component);
			}
			vongDau++;
			list = response.ListTranDau.FindAll((SieuCupGame e) => e.VongDau == vongDau);
		}
		List<CupRoundBtn> list2 = new List<CupRoundBtn>();
		foreach (CupRoundBtn listVongDauButton in listVongDauButtons)
		{
			if (listVongDauButton.VongDau >= vongDau)
			{
				list2.Add(listVongDauButton);
			}
		}
		foreach (CupRoundBtn item in list2)
		{
			Object.Destroy(item.gameObject);
		}
		list2.Clear();
		responseData = response;
		OnViewVongDau(vongDau - 1);
		listVongDauPanel.GetComponent<UIDraggablePanel>().ResetPosition();
	}

	public void OnViewGameDetail(int idx)
	{
		if (actionIdx == idx)
		{
			actionIdx = -1;
		}
		else
		{
			actionIdx = idx;
		}
		OnViewVongDau(curVongDau);
	}

	public void OnViewVongDau(int vongDau)
	{
		if (responseData == null)
		{
			return;
		}
		if (curVongDau != vongDau)
		{
			actionIdx = -1;
			listTranDauPanel.GetComponent<UIDraggablePanel>().ResetPosition();
		}
		foreach (GameObject listTranDauObj in listTranDauObjs)
		{
			listTranDauObj.SetActive(false);
			Object.Destroy(listTranDauObj);
		}
		listTranDauObjs.Clear();
		List<SieuCupGame> list = responseData.ListTranDau.FindAll((SieuCupGame e) => e.VongDau == vongDau);
		int num = 0;
		float num2 = 170f;
		float num3 = 150f;
		float num4 = 80f;
		foreach (CupRoundBtn listVongDauButton in listVongDauButtons)
		{
			if (listVongDauButton.VongDau == vongDau)
			{
				listVongDauButton.bg.spriteName = "button18";
			}
			else
			{
				listVongDauButton.bg.spriteName = "button15";
			}
		}
		SieuCupGame game;
		foreach (SieuCupGame item in list)
		{
			game = item;
			Object obj = Object.Instantiate(grpTranDauObj);
			GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
			gameObject.transform.parent = listTranDauPanel.transform;
			gameObject.transform.localScale = Vector3.one;
			gameObject.transform.localPosition = new Vector3(0f, num2, 0f);
			gameObject.SetActive(true);
			listTranDauObjs.Add(gameObject);
			SuperCupGameInfo component = gameObject.GetComponent<SuperCupGameInfo>();
			component.view = this;
			component.idx = num;
			component.TranDauID = game.ID;
			if (game.Server1ID > 0 && game.Player1ID > 0)
			{
				SieuCupPlayer sieuCupPlayer = responseData.ListPlayers.Find((SieuCupPlayer e) => e.GID == game.Player1ID && e.SID == game.Server1ID);
				component.name1.text = string.Format("s{0}.{1}", sieuCupPlayer.SID, sieuCupPlayer.Ten);
				component.avatar1.spriteName = sieuCupPlayer.Ava;
				component.avatar1.color = new Color(1f, 1f, 1f);
				component.avatar1.transform.parent.GetComponent<SieuCupAvatarItem>().gid = sieuCupPlayer.GID;
				component.avatar1.transform.parent.GetComponent<SieuCupAvatarItem>().sid = sieuCupPlayer.SID;
			}
			else
			{
				component.name1.text = "Free win";
				component.avatar1.spriteName = "NV_TRIEU_CHI_KINH";
				component.avatar1.color = new Color(0f, 0f, 0f);
				component.avatar1.transform.parent.GetComponent<SieuCupAvatarItem>().gid = 0;
				component.avatar1.transform.parent.GetComponent<SieuCupAvatarItem>().sid = 0;
			}
			if (game.Server2ID > 0 && game.Player2ID > 0)
			{
				SieuCupPlayer sieuCupPlayer2 = responseData.ListPlayers.Find((SieuCupPlayer e) => e.GID == game.Player2ID && e.SID == game.Server2ID);
				component.name2.text = string.Format("s{0}.{1}", sieuCupPlayer2.SID, sieuCupPlayer2.Ten);
				component.avatar2.spriteName = sieuCupPlayer2.Ava;
				component.avatar2.color = new Color(1f, 1f, 1f);
				component.avatar2.transform.parent.GetComponent<SieuCupAvatarItem>().gid = sieuCupPlayer2.GID;
				component.avatar2.transform.parent.GetComponent<SieuCupAvatarItem>().sid = sieuCupPlayer2.SID;
			}
			else
			{
				component.name2.text = "Free win";
				component.avatar2.spriteName = "NV_CHU_NGU_THU";
				component.avatar2.color = new Color(0f, 0f, 0f);
				component.avatar2.transform.parent.GetComponent<SieuCupAvatarItem>().gid = 0;
				component.avatar2.transform.parent.GetComponent<SieuCupAvatarItem>().sid = 0;
			}
			if (game.Status == 1)
			{
				component.winner1.gameObject.SetActive(true);
				component.winner2.gameObject.SetActive(false);
			}
			else if (game.Status == 2)
			{
				component.winner1.gameObject.SetActive(false);
				component.winner2.gameObject.SetActive(true);
			}
			else
			{
				component.winner1.gameObject.SetActive(false);
				component.winner2.gameObject.SetActive(false);
			}
			if (num == actionIdx)
			{
				Object obj2 = Object.Instantiate(grpTranDauActions);
				GameObject gameObject2 = (GameObject)((obj2 is GameObject) ? obj2 : null);
				gameObject2.transform.parent = listTranDauPanel.transform;
				gameObject2.transform.localScale = Vector3.one;
				num2 -= num4;
				gameObject2.transform.localPosition = new Vector3(0f, num2, 0f);
				gameObject2.SetActive(true);
				SuperCupGameAction component2 = gameObject2.GetComponent<SuperCupGameAction>();
				component2.idx = num;
				component2.matchID = game.ID;
				component2.name1 = component.name1.text;
				component2.name2 = component.name2.text;
				UserInfo uInfo = GameManager.instance.m_GameClient.UserInfo;
				SieuCupDatCuoc sieuCupDatCuoc = ((responseData.ListDatCuoc == null) ? null : responseData.ListDatCuoc.Find((SieuCupDatCuoc e) => e.MatchID == game.ID && e.GID == uInfo.Gamer.ID && e.SID == uInfo.ServerInfo.ID));
				if (game.Status == 0)
				{
					int iD = GameManager.instance.m_GameClient.UserInfo.Gamer.ID;
					int iD2 = GameManager.instance.m_GameClient.UserInfo.ServerInfo.ID;
					if (game.Player1ID == iD && game.Server1ID == iD2)
					{
						component2.btnDatCuoc1.gameObject.SetActive(false);
						component2.btnDatCuoc2.gameObject.SetActive(false);
						component2.grpCuoc1.SetActive(false);
						component2.grpCuoc2.SetActive(false);
						component2.btnSubmit1.SetActive(true);
						component2.btnSubmit2.SetActive(false);
					}
					else if (game.Player2ID == iD && game.Server2ID == iD2)
					{
						component2.btnDatCuoc1.gameObject.SetActive(false);
						component2.btnDatCuoc2.gameObject.SetActive(false);
						component2.grpCuoc1.SetActive(false);
						component2.grpCuoc2.SetActive(false);
						component2.btnSubmit1.SetActive(false);
						component2.btnSubmit2.SetActive(true);
					}
					else
					{
						component2.btnSubmit1.SetActive(false);
						component2.btnSubmit2.SetActive(false);
						if (sieuCupDatCuoc != null && sieuCupDatCuoc.Choice > 0)
						{
							component2.btnDatCuoc1.gameObject.SetActive(false);
							component2.btnDatCuoc2.gameObject.SetActive(false);
							if (sieuCupDatCuoc.Choice == 1)
							{
								component2.grpCuoc1.SetActive(true);
								component2.grpCuoc2.SetActive(false);
								component2.grpCuoc1.GetComponentInChildren<UILabel>().text = string.Format(Localization.instance.Get("DaCuocLabel"), ConfigManager.instance.OtherConfig.CostDatCuocLienDau);
							}
							else if (sieuCupDatCuoc.Choice == 2)
							{
								component2.grpCuoc1.SetActive(false);
								component2.grpCuoc2.SetActive(true);
								component2.grpCuoc2.GetComponentInChildren<UILabel>().text = string.Format(Localization.instance.Get("DaCuocLabel"), ConfigManager.instance.OtherConfig.CostDatCuocLienDau);
							}
							else
							{
								component2.grpCuoc1.SetActive(false);
								component2.grpCuoc2.SetActive(false);
							}
						}
						else
						{
							if (game.Player1ID <= 0 || game.Player2ID <= 0 || game.Server1ID <= 0 || game.Server2ID <= 0)
							{
								component2.btnDatCuoc1.gameObject.SetActive(false);
								component2.btnDatCuoc2.gameObject.SetActive(false);
							}
							else
							{
								component2.btnDatCuoc1.gameObject.SetActive(true);
								component2.btnDatCuoc2.gameObject.SetActive(true);
								component2.btnDatCuoc1.GetComponentInChildren<UILabel>().text = string.Format(Localization.instance.Get("DatCuocBtn"), ConfigManager.instance.OtherConfig.CostDatCuocLienDau);
								component2.btnDatCuoc2.GetComponentInChildren<UILabel>().text = string.Format(Localization.instance.Get("DatCuocBtn"), ConfigManager.instance.OtherConfig.CostDatCuocLienDau);
							}
							component2.grpCuoc1.SetActive(false);
							component2.grpCuoc2.SetActive(false);
						}
					}
					component2.btnTranDau.SetActive(false);
				}
				else
				{
					component2.btnSubmit1.SetActive(false);
					component2.btnSubmit2.SetActive(false);
					component2.btnDatCuoc1.SetActive(false);
					component2.btnDatCuoc2.SetActive(false);
					if (sieuCupDatCuoc != null && sieuCupDatCuoc.Choice == 1)
					{
						component2.grpCuoc1.SetActive(true);
						component2.grpCuoc2.SetActive(false);
						component2.grpCuoc1.GetComponentInChildren<UILabel>().text = string.Format(Localization.instance.Get("DaCuocLabel"), ConfigManager.instance.OtherConfig.CostDatCuocLienDau);
					}
					else if (sieuCupDatCuoc != null && sieuCupDatCuoc.Choice == 2)
					{
						component2.grpCuoc1.SetActive(false);
						component2.grpCuoc2.SetActive(true);
						component2.grpCuoc2.GetComponentInChildren<UILabel>().text = string.Format(Localization.instance.Get("DaCuocLabel"), ConfigManager.instance.OtherConfig.CostDatCuocLienDau);
					}
					else
					{
						component2.grpCuoc1.SetActive(false);
						component2.grpCuoc2.SetActive(false);
					}
					if (game.Player1ID <= 0 || game.Player2ID <= 0 || game.Server1ID <= 0 || game.Server2ID <= 0)
					{
						component2.btnTranDau.SetActive(false);
					}
					else
					{
						component2.btnTranDau.SetActive(true);
					}
				}
				listTranDauObjs.Add(gameObject2);
			}
			num2 -= num3;
			num++;
		}
		if (curVongDau != vongDau)
		{
			listTranDauPanel.GetComponent<UIDraggablePanel>().ResetPosition();
			curVongDau = vongDau;
		}
	}
}

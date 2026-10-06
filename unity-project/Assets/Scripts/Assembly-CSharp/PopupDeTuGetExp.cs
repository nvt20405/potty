using System.Collections.Generic;
using UnityEngine;

public class PopupDeTuGetExp : MonoBehaviour
{
	public static PopupDeTuGetExp instance;

	public UILabel descLabel;

	public DeTuBattleResult[] deTuList = new DeTuBattleResult[8];

	public GameObject[] lotDeTuGrp = new GameObject[4];

	public EGGUIGrid gridDeTu;

	public EGGUIGrid gridLot;

	public static PopupDeTuGetExp Create(List<UserInfo.HeroData> listDeTu, long expMP, string msg)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("Popup/PopupDeTuGetExp"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupDeTuGetExp>();
		instance.SetInfo(listDeTu, expMP, msg);
		return instance;
	}

	public void SetInfo(List<UserInfo.HeroData> listDeTu, long expDeTu, string msg)
	{
		descLabel.text = msg;
		for (int i = 0; i < deTuList.Length; i++)
		{
			if (i < listDeTu.Count)
			{
				deTuList[i].gameObject.SetActive(true);
				deTuList[i].SetInfo(listDeTu[i], expDeTu);
			}
			else
			{
				deTuList[i].gameObject.SetActive(false);
			}
		}
		int num = (listDeTu.Count - 1) / 2 + 1;
		gridDeTu.Reposition();
		for (int j = 0; j < lotDeTuGrp.Length; j++)
		{
			lotDeTuGrp[j].gameObject.SetActive(j < num);
		}
		gridLot.Reposition();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	private void OnCloseBtnClick()
	{
		DestroyPopup();
	}
}

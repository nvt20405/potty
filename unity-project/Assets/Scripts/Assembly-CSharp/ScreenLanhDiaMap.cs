using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenLanhDiaMap : ScreenBase
{
	public GameObject BaseItem;

	public Dictionary<Vector2, LanhDiaMapItem> MapDict;

	public Dictionary<Vector2, int> LienMinhLanhDia;

	private Dictionary<Vector2, Color> LienMinhColor;

	public GameObject Cursor;

	public GameObject MyCursor;

	public MoveLanhDiaResponse response;

	public Dictionary<Vector2, string> SpriteNames = new Dictionary<Vector2, string>();

	public void Set()
	{
		Utils.SetLightMaps("Lightmap/Lanh_Dia_Bang/");
		if (GameManager.instance.m_GameClient.UserInfo.LanhDiaUser.Position.Equals("0,0") && GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("LanhDia;"))
		{
			MessagePopup.Create(Localization.instance.Get("LanhDiaUserVungAnToan"));
		}
		else if (GameManager.instance.m_GameClient.UserInfo.LanhDiaUser.Position.Equals("0,0"))
		{
			MessagePopup.Create(Localization.instance.Get("LanhDiaUserVungAnToanStart"), 5f);
		}
		GUIManager.ShowGadgets(2);
		LienMinhLanhDia = new Dictionary<Vector2, int>();
		foreach (UserInfo.LanhDiaSquare item in GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map)
		{
			if (item.LienMinhID > 0)
			{
				if (LienMinhLanhDia.ContainsKey(new Vector2(item.LienMinhID, item.SID)))
				{
					Dictionary<Vector2, int> lienMinhLanhDia;
					Dictionary<Vector2, int> dictionary = (lienMinhLanhDia = LienMinhLanhDia);
					Vector2 vector = new Vector2(item.LienMinhID, item.SID);
					Vector2 key = vector;
					int num = lienMinhLanhDia[vector];
					dictionary[key] = num + 1;
				}
				else
				{
					LienMinhLanhDia.Add(new Vector2(item.LienMinhID, item.SID), 1);
				}
			}
		}
		IOrderedEnumerable<KeyValuePair<Vector2, int>> orderedEnumerable = LienMinhLanhDia.OrderByDescending((KeyValuePair<Vector2, int> pair) => pair.Value);
		int num2 = 0;
		LienMinhColor = new Dictionary<Vector2, Color>();
		SpriteNames = new Dictionary<Vector2, string>();
		foreach (KeyValuePair<Vector2, int> item2 in orderedEnumerable)
		{
			num2++;
			switch (num2)
			{
			case 1:
				LienMinhColor.Add(item2.Key, new Color(1f, 1f, 0f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_1");
				continue;
			case 2:
				LienMinhColor.Add(item2.Key, new Color(0.6f, 0f, 0.6f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_2");
				continue;
			case 3:
				LienMinhColor.Add(item2.Key, new Color(0.6f, 0f, 0.6f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_3");
				continue;
			case 4:
				LienMinhColor.Add(item2.Key, new Color(0f, 0.25f, 1f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_2");
				continue;
			case 5:
				LienMinhColor.Add(item2.Key, new Color(0f, 0.25f, 1f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_3");
				continue;
			case 6:
				LienMinhColor.Add(item2.Key, new Color(0f, 1f, 0f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_2");
				continue;
			case 7:
				LienMinhColor.Add(item2.Key, new Color(0f, 1f, 0f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_3");
				continue;
			case 8:
				LienMinhColor.Add(item2.Key, new Color(0f, 1f, 0f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_4");
				continue;
			case 9:
				LienMinhColor.Add(item2.Key, new Color(0f, 1f, 0f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_5");
				continue;
			case 10:
				LienMinhColor.Add(item2.Key, new Color(0f, 1f, 0f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_6");
				continue;
			}
			if (item2.Key == new Vector2(GameManager.instance.m_GameClient.UserInfo.LienMinh.ID, GameManager.instance.m_GameClient.UserInfo.ServerInfo.ID))
			{
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_6");
				LienMinhColor.Add(item2.Key, new Color(1f, 1f, 1f));
			}
			else
			{
				SpriteNames.Add(item2.Key, "white");
				LienMinhColor.Add(item2.Key, new Color(0.8f, 0f, 0f));
			}
		}
		if (MapDict == null || MapDict.Count == 0)
		{
			BaseItem.gameObject.SetActive(true);
			MapDict = new Dictionary<Vector2, LanhDiaMapItem>();
			int num3 = 0;
			foreach (UserInfo.LanhDiaSquare item3 in GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map)
			{
				int num4 = int.Parse(item3.Position.Split(',')[0]);
				int num5 = int.Parse(item3.Position.Split(',')[1]);
				LanhDiaMapItem component = ((GameObject)UnityEngine.Object.Instantiate(BaseItem)).GetComponent<LanhDiaMapItem>();
				component.transform.parent = BaseItem.transform.parent;
				component.transform.localScale = BaseItem.transform.localScale;
				if (num4 >= 12 && num4 <= 15 && num5 >= 12 && num5 <= 15)
				{
					component.transform.localPosition = BaseItem.transform.position + new Vector3(num4 * 100 - 1350, 1350 - num5 * 100, -60f);
				}
				else
				{
					component.transform.localPosition = BaseItem.transform.position + new Vector3(num4 * 100 - 1350, 1350 - num5 * 100, 0f);
				}
				if (item3.LienMinhID > 0)
				{
					component.Set(new Vector2(num4, num5), LienMinhColor[new Vector2(item3.LienMinhID, item3.SID)], item3, SpriteNames[new Vector2(item3.LienMinhID, item3.SID)], num3);
				}
				else
				{
					component.Set(new Vector2(num4, num5), (num4 >= 12 && num4 <= 15 && num5 >= 12 && num5 <= 15) ? Color.cyan : (((num4 + num5) % 2 != 0) ? Color.black : Color.gray), item3, "white", num3);
				}
				MapDict.Add(new Vector2(num4, num5), component);
				num3++;
			}
			BaseItem.gameObject.SetActive(false);
			Debug.Log("init map");
		}
		else
		{
			Sync();
		}
		if (GameManager.instance.m_GameClient.UserInfo.LanhDiaUser.Position.Equals("0,0"))
		{
			MyCursor.SetActive(false);
		}
		else
		{
			int num6 = int.Parse(GameManager.instance.m_GameClient.UserInfo.LanhDiaUser.Position.Split(',')[0]);
			int num7 = int.Parse(GameManager.instance.m_GameClient.UserInfo.LanhDiaUser.Position.Split(',')[1]);
			MyCursor.SetActive(true);
			MyCursor.transform.position = MapDict[new Vector2(num6, num7)].transform.position;
		}
		GUIManager.instance.cam2D.clearFlags = CameraClearFlags.Depth;
		PopupLanhDiaInfo.Create(GameManager.instance.m_GameClient.UserInfo.LanhDiaUser.Position, Localization.instance.Get("ViTriHienTai"));
	}

	private void OnDisable()
	{
		GUIManager.instance.cam2D.clearFlags = CameraClearFlags.Skybox;
		PopupLanhDiaInfo.DestroyPopup();
	}

	public void Sync(bool isShowMessage = false)
	{
		if (isShowMessage)
		{
			if (GameManager.instance.m_GameClient.UserInfo.LanhDiaUser.Position.Equals("0,0") && GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains("LanhDia;"))
			{
				MessagePopup.Create(Localization.instance.Get("LanhDiaUserVungAnToan"));
			}
			else if (GameManager.instance.m_GameClient.UserInfo.LanhDiaUser.Position.Equals("0,0"))
			{
				MessagePopup.Create(Localization.instance.Get("LanhDiaUserVungAnToanStart"), 5f);
			}
		}
		DateTime now = DateTime.Now;
		LienMinhLanhDia = new Dictionary<Vector2, int>();
		foreach (UserInfo.LanhDiaSquare item in GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map)
		{
			if (item.LienMinhID > 0)
			{
				if (LienMinhLanhDia.ContainsKey(new Vector2(item.LienMinhID, item.SID)))
				{
					Dictionary<Vector2, int> lienMinhLanhDia;
					Dictionary<Vector2, int> dictionary = (lienMinhLanhDia = LienMinhLanhDia);
					Vector2 vector = new Vector2(item.LienMinhID, item.SID);
					Vector2 key = vector;
					int num = lienMinhLanhDia[vector];
					dictionary[key] = num + 1;
				}
				else
				{
					LienMinhLanhDia.Add(new Vector2(item.LienMinhID, item.SID), 1);
				}
			}
		}
		Debug.Log("Add xong LienMinh Lanh Dia");
		IOrderedEnumerable<KeyValuePair<Vector2, int>> orderedEnumerable = LienMinhLanhDia.OrderByDescending((KeyValuePair<Vector2, int> pair) => pair.Value);
		int num2 = 0;
		LienMinhColor = new Dictionary<Vector2, Color>();
		SpriteNames = new Dictionary<Vector2, string>();
		foreach (KeyValuePair<Vector2, int> item2 in orderedEnumerable)
		{
			num2++;
			switch (num2)
			{
			case 1:
				LienMinhColor.Add(item2.Key, new Color(1f, 1f, 0f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_1");
				continue;
			case 2:
				LienMinhColor.Add(item2.Key, new Color(0.6f, 0f, 0.6f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_2");
				continue;
			case 3:
				LienMinhColor.Add(item2.Key, new Color(0.6f, 0f, 0.6f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_3");
				continue;
			case 4:
				LienMinhColor.Add(item2.Key, new Color(0f, 0.25f, 1f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_2");
				continue;
			case 5:
				LienMinhColor.Add(item2.Key, new Color(0f, 0.25f, 1f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_3");
				continue;
			case 6:
				LienMinhColor.Add(item2.Key, new Color(0f, 1f, 0f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_2");
				continue;
			case 7:
				LienMinhColor.Add(item2.Key, new Color(0f, 1f, 0f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_3");
				continue;
			case 8:
				LienMinhColor.Add(item2.Key, new Color(0f, 1f, 0f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_4");
				continue;
			case 9:
				LienMinhColor.Add(item2.Key, new Color(0f, 1f, 0f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_5");
				continue;
			case 10:
				LienMinhColor.Add(item2.Key, new Color(0f, 1f, 0f));
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_6");
				continue;
			}
			if (item2.Key == new Vector2(GameManager.instance.m_GameClient.UserInfo.LienMinh.ID, GameManager.instance.m_GameClient.UserInfo.ServerInfo.ID))
			{
				SpriteNames.Add(item2.Key, "bkg_trans_lanhdiabang_6");
				LienMinhColor.Add(item2.Key, new Color(1f, 1f, 1f));
			}
			else
			{
				SpriteNames.Add(item2.Key, "white");
				LienMinhColor.Add(item2.Key, new Color(0.8f, 0f, 0f));
			}
		}
		Debug.Log("Xet xong bg");
		num2 = 0;
		Vector2 vector2 = default(Vector2);
		foreach (UserInfo.LanhDiaSquare item3 in GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map)
		{
			vector2 = new Vector2(int.Parse(item3.Position.Split(',')[0]), int.Parse(item3.Position.Split(',')[1]));
			LanhDiaMapItem lanhDiaMapItem = MapDict[vector2];
			if (item3.LienMinhID > 0)
			{
				lanhDiaMapItem.Set(vector2, LienMinhColor[new Vector2(item3.LienMinhID, item3.SID)], item3, SpriteNames[new Vector2(item3.LienMinhID, item3.SID)], num2, true);
			}
			else
			{
				lanhDiaMapItem.Set(vector2, (vector2.x >= 12f && vector2.x <= 15f && vector2.y >= 12f && vector2.y <= 15f) ? Color.blue : (((vector2.x + vector2.y) % 2f != 0f) ? Color.black : Color.gray), item3, "white", num2, true);
			}
			num2++;
			item3.IsUpdated = false;
		}
		Debug.Log("updated " + num2 + " squares!");
		if (GameManager.instance.m_GameClient.UserInfo.LanhDiaUser.Position.Equals("0,0"))
		{
			MyCursor.SetActive(false);
		}
		else
		{
			int num3 = int.Parse(GameManager.instance.m_GameClient.UserInfo.LanhDiaUser.Position.Split(',')[0]);
			int num4 = int.Parse(GameManager.instance.m_GameClient.UserInfo.LanhDiaUser.Position.Split(',')[1]);
			MyCursor.SetActive(true);
			MyCursor.transform.position = MapDict[new Vector2(num3, num4)].transform.position;
		}
		if (PopupLanhDiaInfo.instance != null)
		{
			PopupLanhDiaInfo.instance.Sync();
		}
		Debug.Log("LogTime: " + (DateTime.Now - now).TotalMilliseconds);
	}

	private void Update()
	{
	}

	public void OnSelectSquare(GameObject square)
	{
		Cursor.SetActive(true);
		Cursor.transform.position = square.transform.position;
		LanhDiaMapItem component = square.GetComponent<LanhDiaMapItem>();
		string text = ((char)(65 + (int)component.Position.y - 1)).ToString() + component.Position.x + " ";
		if (component.Data.LienMinhID > 0)
		{
			PopupLanhDiaInfo.Create(component.Data.Position, text + string.Format(Localization.instance.Get("ChuQuyenLanhTho"), component.Data.SID, GameManager.instance.m_GameClient.UserInfo.LanhDiaData.LienMinhName[component.Data.SID + "," + component.Data.LienMinhID]));
		}
		else if (component.Position.x >= 12f && component.Position.x <= 15f && component.Position.y >= 12f && component.Position.y <= 15f)
		{
			PopupLanhDiaInfo.Create(component.Data.Position, Localization.instance.Get("KhuVucAnToan"));
		}
		else
		{
			PopupLanhDiaInfo.Create(component.Data.Position, text + Localization.instance.Get("DatTrong"));
		}
	}

	public void StartBattle()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBattle);
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		string mapName = "BM_City";
		if (response.Replays != null && response.Replays.Count > 0)
		{
			screenBattle.Replay(response.Replays[0], mapName);
			response.Replays.RemoveAt(0);
			if (response.Replays != null && response.Replays.Count > 0)
			{
				screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenBattle;
			}
			else
			{
				screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenDanhSon;
			}
			screenBattle.OnFinishReplay += StartBattle;
		}
		else
		{
			screenBattle.ClearOnFinishReplay();
			if (response.IsSuccess)
			{
				MessagePopup.Create(Localization.instance.Get("LanhDiaThanhCong"));
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("LanhDiaThatBai"));
			}
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenLanhDiaMap);
			((ScreenLanhDiaMap)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenLanhDiaMap)).Set();
		}
	}

	public void StartBattle(MoveLanhDiaResponse response)
	{
		this.response = response;
		StartBattle();
	}

	public void OnShowTop()
	{
		PopupTopLanhDiaBang.Create();
		PopupLanhDiaInfo.DestroyPopup();
	}

	public void OnChatLanhDia()
	{
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenChatLanhDia);
	}

	public void OnShowShop()
	{
	}
}

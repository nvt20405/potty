using UnityEngine;

public class LanhDiaMapItem : MonoBehaviour
{
	public Vector2 Position;

	public UISprite bg;

	public UISprite Avatar;

	public UISprite RoundTopLeft;

	public UISprite RoundTopRight;

	public UISprite RoundBotLeft;

	public UISprite RoundBotRight;

	public UISprite StraightTopLeft;

	public UISprite StraightLeftTop;

	public UISprite StraightTopRight;

	public UISprite StraightRightTop;

	public UISprite StraightBotLeft;

	public UISprite StraightLeftBot;

	public UISprite StraightBotRight;

	public UISprite StraightRightBot;

	public UserInfo.LanhDiaSquare Data;

	public int Id;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void UpdateBorder()
	{
		if (Data.LienMinhID > 0)
		{
			if (Data.LienMinhID == GameManager.instance.m_GameClient.UserInfo.LienMinh.ID)
			{
				RoundTopLeft.color = Color.green;
				RoundTopRight.color = Color.green;
				RoundBotLeft.color = Color.green;
				RoundBotRight.color = Color.green;
				StraightTopLeft.color = Color.green;
				StraightLeftTop.color = Color.green;
				StraightTopRight.color = Color.green;
				StraightRightTop.color = Color.green;
				StraightBotLeft.color = Color.green;
				StraightLeftBot.color = Color.green;
				StraightBotRight.color = Color.green;
				StraightRightBot.color = Color.green;
			}
			else
			{
				RoundTopLeft.color = new Color(0.9f, 0.5f, 0f);
				RoundTopRight.color = new Color(0.9f, 0.5f, 0f);
				RoundBotLeft.color = new Color(0.9f, 0.5f, 0f);
				RoundBotRight.color = new Color(0.9f, 0.5f, 0f);
				StraightTopLeft.color = new Color(0.9f, 0.5f, 0f);
				StraightLeftTop.color = new Color(0.9f, 0.5f, 0f);
				StraightTopRight.color = new Color(0.9f, 0.5f, 0f);
				StraightRightTop.color = new Color(0.9f, 0.5f, 0f);
				StraightBotLeft.color = new Color(0.9f, 0.5f, 0f);
				StraightLeftBot.color = new Color(0.9f, 0.5f, 0f);
				StraightBotRight.color = new Color(0.9f, 0.5f, 0f);
				StraightRightBot.color = new Color(0.9f, 0.5f, 0f);
			}
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			bool flag4 = true;
			bool flag5 = true;
			bool flag6 = true;
			bool flag7 = true;
			bool flag8 = true;
			bool flag9 = true;
			bool flag10 = true;
			bool flag11 = true;
			bool flag12 = true;
			if (GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map.Count > Id + 26 && Id < 650 && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id + 26].LienMinhID == Data.LienMinhID && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id + 26].SID == Data.SID)
			{
				flag8 = false;
				flag12 = false;
				flag4 = false;
				flag2 = false;
			}
			else
			{
				if (GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map.Count > Id + 1 && (Id + 1) % 26 != 0 && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id + 1].LienMinhID == Data.LienMinhID && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id + 1].SID == Data.SID)
				{
					flag4 = false;
				}
				else
				{
					flag12 = false;
				}
				if (Id - 1 >= 0 && Id % 26 != 0 && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id - 1].LienMinhID == Data.LienMinhID && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id - 1].SID == Data.SID)
				{
					flag2 = false;
				}
				else
				{
					flag8 = false;
				}
			}
			if (Id - 26 >= 0 && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id - 26].LienMinhID == Data.LienMinhID && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id - 26].SID == Data.SID)
			{
				flag6 = false;
				flag10 = false;
				flag3 = false;
				flag = false;
			}
			else
			{
				if (GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map.Count > Id + 1 && (Id + 1) % 26 != 0 && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id + 1].LienMinhID == Data.LienMinhID && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id + 1].SID == Data.SID)
				{
					flag3 = false;
				}
				else
				{
					flag10 = false;
				}
				if (Id - 1 >= 0 && Id % 26 != 0 && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id - 1].LienMinhID == Data.LienMinhID && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id - 1].SID == Data.SID)
				{
					flag = false;
				}
				else
				{
					flag6 = false;
				}
			}
			if (GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map.Count > Id + 1 && (Id + 1) % 26 != 0 && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id + 1].LienMinhID == Data.LienMinhID && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id + 1].SID == Data.SID)
			{
				flag9 = false;
				flag11 = false;
				flag3 = false;
				flag4 = false;
			}
			else
			{
				if (GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map.Count > Id + 26 && Id < 650 && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id + 26].LienMinhID == Data.LienMinhID && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id + 26].SID == Data.SID)
				{
					flag4 = false;
				}
				else
				{
					flag11 = false;
				}
				if (Id - 26 >= 0 && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id - 26].LienMinhID == Data.LienMinhID && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id - 26].SID == Data.SID)
				{
					flag3 = false;
				}
				else
				{
					flag9 = false;
				}
			}
			if (Id - 1 >= 0 && Id % 26 != 0 && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id - 1].LienMinhID == Data.LienMinhID && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id - 1].SID == Data.SID)
			{
				flag5 = false;
				flag7 = false;
				flag = false;
				flag2 = false;
			}
			else
			{
				if (Id - 26 >= 0 && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id - 26].LienMinhID == Data.LienMinhID && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id - 26].SID == Data.SID)
				{
					flag = false;
				}
				else
				{
					flag5 = false;
				}
				if (GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map.Count > Id + 26 && Id < 650 && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id + 26].LienMinhID == Data.LienMinhID && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id + 26].SID == Data.SID)
				{
					flag2 = false;
				}
				else
				{
					flag7 = false;
				}
			}
			RoundTopLeft.gameObject.SetActive(flag);
			RoundTopRight.gameObject.SetActive(flag2);
			RoundBotLeft.gameObject.SetActive(flag3);
			RoundBotRight.gameObject.SetActive(flag4);
			StraightTopLeft.gameObject.SetActive(flag5);
			StraightLeftTop.gameObject.SetActive(flag6);
			StraightTopRight.gameObject.SetActive(flag7);
			StraightRightTop.gameObject.SetActive(flag8);
			StraightBotLeft.gameObject.SetActive(flag9);
			StraightLeftBot.gameObject.SetActive(flag10);
			StraightBotRight.gameObject.SetActive(flag11);
			StraightRightBot.gameObject.SetActive(flag12);
		}
		else
		{
			RoundTopLeft.gameObject.SetActive(false);
			RoundTopRight.gameObject.SetActive(false);
			RoundBotLeft.gameObject.SetActive(false);
			RoundBotRight.gameObject.SetActive(false);
			StraightTopLeft.gameObject.SetActive(false);
			StraightLeftTop.gameObject.SetActive(false);
			StraightTopRight.gameObject.SetActive(false);
			StraightRightTop.gameObject.SetActive(false);
			StraightBotLeft.gameObject.SetActive(false);
			StraightLeftBot.gameObject.SetActive(false);
			StraightBotRight.gameObject.SetActive(false);
			StraightRightBot.gameObject.SetActive(false);
		}
	}

	public void Set(Vector2 position, Color color, UserInfo.LanhDiaSquare data, string spriteName, int id, bool updateNeighbour = false)
	{
		Position = position;
		color.a = 0.3f;
		bg.color = color;
		bg.spriteName = spriteName;
		Data = data;
		UpdateBorder();
		Id = id;
		updateNeighbour = true;
		if (updateNeighbour)
		{
			ScreenLanhDiaMap screenLanhDiaMap = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenLanhDiaMap) as ScreenLanhDiaMap;
			if (GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map.Count > Id + 26 && Id < 650 && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id + 26].LienMinhID == Data.LienMinhID && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id + 26].SID == Data.SID && screenLanhDiaMap.MapDict.ContainsKey(Position + Vector2.right))
			{
				screenLanhDiaMap.MapDict[Position + Vector2.right].UpdateBorder();
			}
			if (Id - 26 >= 0 && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id - 26].LienMinhID == Data.LienMinhID && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id - 26].SID == Data.SID && screenLanhDiaMap.MapDict.ContainsKey(Position - Vector2.right))
			{
				screenLanhDiaMap.MapDict[Position - Vector2.right].UpdateBorder();
			}
			if (GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map.Count > Id + 1 && (Id + 1) % 26 != 0 && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id + 1].LienMinhID == Data.LienMinhID && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id + 1].SID == Data.SID && screenLanhDiaMap.MapDict.ContainsKey(Position + Vector2.up))
			{
				screenLanhDiaMap.MapDict[Position + Vector2.up].UpdateBorder();
			}
			if (Id - 1 >= 0 && Id % 26 != 0 && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id - 1].LienMinhID == Data.LienMinhID && GameManager.instance.m_GameClient.UserInfo.LanhDiaData.Map[Id - 1].SID == Data.SID && screenLanhDiaMap.MapDict.ContainsKey(Position - Vector2.up))
			{
				screenLanhDiaMap.MapDict[Position - Vector2.up].UpdateBorder();
			}
		}
		if (Data.Defender.Count > 0)
		{
			Avatar.gameObject.SetActive(true);
			Avatar.spriteName = Data.Defender[0].Avatar;
		}
		else
		{
			Avatar.gameObject.SetActive(false);
		}
	}
}

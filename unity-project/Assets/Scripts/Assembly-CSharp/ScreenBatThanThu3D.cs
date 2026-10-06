using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBatThanThu3D : MonoBehaviour
{
	public UILabel LuotLabel;

	private ThanThuDaoInfo curThanthuDao;

	public List<ThanThuDaoObject> blockList;

	public ThanThuDaoObject curBlock;

	public ThanThuDaoObject player;

	public ThanThuDaoObject goal;

	public GameObject goalStair;

	public List<ThanThuDaoObject> mobList;

	private static int movingCount;

	private bool isMoving;

	private bool isInit;

	public static int MovingCount
	{
		get
		{
			return movingCount;
		}
		set
		{
			movingCount = value;
			Debug.Log(value);
		}
	}

	private void Start()
	{
	}

	public void SpawnMap(ThanThuDaoInfo thanthuDao)
	{
		isInit = true;
		isMoving = false;
		MovingCount = 0;
		foreach (ThanThuDaoObject block in blockList)
		{
			if (block.TopWall != null)
			{
				block.TopWall.SetActive(false);
			}
			if (block.BotWall != null)
			{
				block.BotWall.SetActive(false);
			}
			if (block.RightWall != null)
			{
				block.RightWall.SetActive(false);
			}
			if (block.LeftWall != null)
			{
				block.LeftWall.SetActive(false);
			}
			block.WallData = null;
			if (block.position.x == (float)thanthuDao.Goal && block.position.y == 6f)
			{
				goal.transform.position = block.transform.position;
				goal.position.x = thanthuDao.Goal;
				goal.position.y = 7f;
			}
		}
		foreach (ThanThuDaoObject mob in mobList)
		{
			Object.Destroy(mob.gameObject);
		}
		mobList = new List<ThanThuDaoObject>();
		curThanthuDao = thanthuDao;
		foreach (ThanThuDaoInfo.WallBlock wall in thanthuDao.WallList)
		{
			SpawnWall(wall);
		}
		foreach (ThanThuDaoInfo.MobInfo mob2 in thanthuDao.MobList)
		{
			SpawnMob(mob2);
		}
	}

	private void SpawnWall(ThanThuDaoInfo.WallBlock wall)
	{
		foreach (ThanThuDaoObject block in blockList)
		{
			if (block.position.x == (float)wall.X && block.position.y == (float)wall.Y)
			{
				block.WallData = wall;
			}
		}
	}

	private void SpawnMob(ThanThuDaoInfo.MobInfo mob)
	{
		if (mob.codeName == "Player")
		{
			SpawnPlayer(new Vector2(mob.X, mob.Y));
			return;
		}
		ThanThuDaoObject component = ((GameObject)Object.Instantiate(Resources.Load("gui/screens/screenthanthu/" + mob.codeName))).GetComponent<ThanThuDaoObject>();
		component.transform.parent = base.transform;
		component.mobData = mob;
		component.position.x = mob.X;
		component.position.y = mob.Y;
		foreach (ThanThuDaoObject block in blockList)
		{
			if (block.position == component.position)
			{
				component.transform.position = block.transform.position;
			}
		}
		mobList.Add(component);
	}

	private void SpawnPlayer(Vector2 position)
	{
		foreach (ThanThuDaoObject block in blockList)
		{
			if (!(block.position == position))
			{
				continue;
			}
			foreach (Transform item in player.transform)
			{
				Transform transform2 = item;
				if (transform2 != player.guiPanel.transform)
				{
					Object.Destroy(transform2.gameObject);
				}
			}
			UserInfo.HeroData hero = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData h) => h.HID == GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran[0]);
			string empty = string.Empty;
			if (hero.VuKhiID > 0)
			{
				empty = GameManager.instance.m_GameClient.UserInfo.TrangBiList.Find((UserInfo.TrangBiData h) => h.ID == hero.VuKhiID).Name;
			}
			string costume = string.Empty;
			if (hero.CostumeID > 0)
			{
				costume = GameManager.instance.m_GameClient.UserInfo.CostumeList.Find((UserInfo.CostumeData c) => c.ID == hero.CostumeID).CodeName;
			}
			((GameObject)Object.Instantiate(Resources.Load("fx/prefabs/MISC_BAT_THU_SPAWN_NV"), player.transform.position, Quaternion.identity)).transform.parent = player.transform;
			player.guiPanel.SetActive(true);
			Avatar3D avatar3D = GUIManager.instance.InstantiateAvatar3D(hero.Name, empty, string.Empty, string.Empty, null, null, string.Empty, costume, string.Empty);
			avatar3D.transform.parent = player.transform;
			avatar3D.transform.localPosition = Vector3.zero;
			avatar3D.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			avatar3D.transform.localScale = 1.5f * Vector3.one;
			avatar3D.FadingInIdle(0.2f);
			player.mobData = new ThanThuDaoInfo.MobInfo(position.x + string.Empty + position.y + "Player");
			curBlock = block;
			if (curBlock.WallData != null)
			{
				player.guiTop.SetActive(true);
				player.guiBot.SetActive(true);
				player.guiLeft.SetActive(true);
				player.guiRight.SetActive(true);
				if (curBlock.wallData.Top)
				{
					player.guiTop.SetActive(false);
				}
				if (curBlock.wallData.Bot)
				{
					player.guiBot.SetActive(false);
				}
				if (curBlock.wallData.Left)
				{
					player.guiLeft.SetActive(false);
				}
				if (curBlock.wallData.Right)
				{
					player.guiRight.SetActive(false);
				}
			}
			else
			{
				player.guiTop.SetActive(true);
				player.guiBot.SetActive(true);
				player.guiLeft.SetActive(true);
				player.guiRight.SetActive(true);
			}
			if (block.position.x == 1f)
			{
				player.guiLeft.SetActive(false);
			}
			if (block.position.x == 6f)
			{
				player.guiRight.SetActive(false);
			}
			if (block.position.y == 1f)
			{
				player.guiBot.SetActive(false);
			}
			if (block.position.y == 6f)
			{
				player.guiTop.SetActive(false);
			}
			player.transform.position = curBlock.transform.position;
			player.mobData.X = (int)block.position.x;
			player.mobData.Y = (int)block.position.y;
			player.position.x = (int)block.position.x;
			player.position.y = (int)block.position.y;
			break;
		}
	}

	public void EndMove(ThanThuDaoObject mob)
	{
		MovingCount--;
		if (MovingCount <= 0)
		{
			MovingCount = 0;
			isMoving = false;
		}
		HandleResult();
	}

	private void HandleResult()
	{
		List<ThanThuDaoObject> list = new List<ThanThuDaoObject>();
		foreach (ThanThuDaoObject mob in mobList)
		{
			if (mob.position == player.position)
			{
				StartCoroutine(IEGameOver());
			}
			if (mob.name.Contains("Trap"))
			{
				continue;
			}
			foreach (ThanThuDaoObject mob2 in mobList)
			{
				if (mob2.name.Contains("Trap") || !(mob != mob2) || !(mob.position == mob2.position) || list.Contains(mob) || list.Contains(mob2))
				{
					continue;
				}
				if (mob.mobData.codeName.Contains("1"))
				{
					list.Add(mob);
				}
				else
				{
					list.Add(mob2);
				}
				if (mob.isMoving)
				{
					mob.StopMove();
					MovingCount--;
					if (MovingCount <= 0)
					{
						MovingCount = 0;
						isMoving = false;
					}
				}
				if (mob2.isMoving)
				{
					mob2.StopMove();
					MovingCount--;
					if (MovingCount <= 0)
					{
						MovingCount = 0;
						isMoving = false;
					}
				}
			}
		}
		foreach (ThanThuDaoObject item in list)
		{
			item.StopMove();
			StartCoroutine(MobVS(item));
		}
	}

	private IEnumerator MobVS(ThanThuDaoObject mob)
	{
		GameObject fx = (GameObject)Object.Instantiate(Resources.Load("fx/prefabs/MISC_BAT_THU_GIAO_CHIEN"), mob.transform.position, Quaternion.identity);
		MovingCount++;
		yield return new WaitForSeconds(1f);
		MovingCount--;
		if (MovingCount <= 0)
		{
			MovingCount = 0;
			isMoving = false;
		}
		fx.SetActive(false);
		mobList.Remove(mob);
		Object.Destroy(mob.gameObject);
		Object.Destroy(fx);
	}

	private IEnumerator IEGameOver()
	{
		GameObject fx = (GameObject)Object.Instantiate(Resources.Load("fx/prefabs/MISC_BAT_THU_GIAO_CHIEN"), player.transform.position, Quaternion.identity);
		MovingCount++;
		yield return new WaitForSeconds(1f);
		MovingCount--;
		GameOver();
		Object.Destroy(fx);
	}

	private bool IsMoveValid(Vector2 curPos, Vector2 nexPos)
	{
		ThanThuDaoObject thanThuDaoObject = null;
		foreach (ThanThuDaoObject block in blockList)
		{
			if (block.position == curPos)
			{
				thanThuDaoObject = block;
			}
		}
		if (thanThuDaoObject != null)
		{
			if (thanThuDaoObject.wallData == null)
			{
				return true;
			}
			if (nexPos.x == curPos.x + 1f)
			{
				if (thanThuDaoObject.WallData.Right)
				{
					return false;
				}
				return true;
			}
			if (nexPos.x == curPos.x - 1f)
			{
				if (thanThuDaoObject.WallData.Left)
				{
					return false;
				}
				return true;
			}
			if (nexPos.y == curPos.y + 1f)
			{
				if (thanThuDaoObject.WallData.Top)
				{
					return false;
				}
				return true;
			}
			if (nexPos.y == curPos.y - 1f)
			{
				if (thanThuDaoObject.WallData.Bot)
				{
					return false;
				}
				return true;
			}
			return false;
		}
		return true;
	}

	public void MoveTo(ThanThuDaoObject nextBlock)
	{
		if (!isInit || isMoving || Mathf.Abs(nextBlock.position.x - curBlock.position.x) > 1f || Mathf.Abs(nextBlock.position.y - curBlock.position.y) > 1f || (Mathf.Abs(nextBlock.position.y - curBlock.position.y) == 1f && Mathf.Abs(nextBlock.position.x - curBlock.position.x) == 1f))
		{
			return;
		}
		if (nextBlock.name == "Goal")
		{
			Finish();
		}
		else
		{
			if (!(nextBlock != null))
			{
				return;
			}
			if (curBlock.WallData != null)
			{
				if (nextBlock.position.x == curBlock.position.x + 1f)
				{
					if (curBlock.WallData.Right)
					{
						return;
					}
				}
				else if (nextBlock.position.x == curBlock.position.x - 1f)
				{
					if (curBlock.WallData.Left)
					{
						return;
					}
				}
				else if (nextBlock.position.y == curBlock.position.y + 1f)
				{
					if (curBlock.WallData.Top)
					{
						return;
					}
				}
				else if (nextBlock.position.y == curBlock.position.y - 1f && curBlock.WallData.Bot)
				{
					return;
				}
			}
			if (nextBlock.wallData != null)
			{
				if (nextBlock.position.x == curBlock.position.x + 1f)
				{
					if (nextBlock.WallData.Left)
					{
						return;
					}
				}
				else if (nextBlock.position.x == curBlock.position.x - 1f)
				{
					if (nextBlock.WallData.Right)
					{
						return;
					}
				}
				else if (nextBlock.position.y == curBlock.position.y + 1f)
				{
					if (nextBlock.WallData.Bot)
					{
						return;
					}
				}
				else if (nextBlock.position.y == curBlock.position.y - 1f && nextBlock.WallData.Top)
				{
					return;
				}
			}
			player.guiPanel.SetActive(false);
			MovingCount++;
			isMoving = true;
			player.StartMove(nextBlock, 0.5f, EndMove);
			AIMove();
			curBlock = nextBlock;
		}
	}

	private void AIMove()
	{
		foreach (ThanThuDaoObject mob in mobList)
		{
			if (mob.guiPanel != null)
			{
				mob.guiPanel.SetActive(false);
			}
			if (mob.mobData.codeName == "Mob2Doc")
			{
				AIMob2Doc(mob);
			}
			else if (mob.mobData.codeName == "Mob2Ngang")
			{
				AIMob2Ngang(mob);
			}
			else if (mob.mobData.codeName == "Mob1Doc")
			{
				AIMob1Doc(mob);
			}
			else if (mob.mobData.codeName == "Mob1Ngang")
			{
				AIMob1Ngang(mob);
			}
		}
	}

	public void Move(ThanThuDaoObject mob, Vector2 pos, float time)
	{
		time = 0.5f;
		foreach (ThanThuDaoObject block in blockList)
		{
			if (block.position == pos)
			{
				MovingCount++;
				mob.StartMove(block, time, EndMove);
			}
		}
	}

	public void AIMob2Doc(ThanThuDaoObject mob)
	{
		int num = 2;
		for (int i = 0; i < num; i++)
		{
			if (player.position.y < mob.position.y)
			{
				if (IsMoveValid(mob.position, mob.position - Vector2.up) && IsMoveValid(mob.position - Vector2.up, mob.position))
				{
					num--;
					i--;
					Move(mob, mob.position - Vector2.up, 1f);
				}
			}
			else if (player.position.y > mob.position.y && IsMoveValid(mob.position, mob.position + Vector2.up) && IsMoveValid(mob.position + Vector2.up, mob.position))
			{
				num--;
				i--;
				Move(mob, mob.position + Vector2.up, 1f);
			}
		}
		for (int j = 0; j < num; j++)
		{
			if (player.position.x < mob.position.x)
			{
				if (IsMoveValid(mob.position, mob.position - Vector2.right) && IsMoveValid(mob.position - Vector2.right, mob.position))
				{
					num--;
					j--;
					Move(mob, mob.position - Vector2.right, 1f);
				}
			}
			else if (player.position.x > mob.position.x && IsMoveValid(mob.position, mob.position + Vector2.right) && IsMoveValid(mob.position + Vector2.right, mob.position))
			{
				num--;
				j--;
				Move(mob, mob.position + Vector2.right, 1f);
			}
			for (int k = 0; k < num; k++)
			{
				if (player.position.y < mob.position.y)
				{
					if (IsMoveValid(mob.position, mob.position - Vector2.up) && IsMoveValid(mob.position - Vector2.up, mob.position))
					{
						num--;
						k--;
						Move(mob, mob.position - Vector2.up, 1f);
					}
				}
				else if (player.position.y > mob.position.y && IsMoveValid(mob.position, mob.position + Vector2.up) && IsMoveValid(mob.position + Vector2.up, mob.position))
				{
					num--;
					k--;
					Move(mob, mob.position + Vector2.up, 1f);
				}
			}
		}
	}

	public void AIMob2Ngang(ThanThuDaoObject mob)
	{
		int num = 2;
		for (int i = 0; i < num; i++)
		{
			if (player.position.x < mob.position.x)
			{
				if (IsMoveValid(mob.position, mob.position - Vector2.right) && IsMoveValid(mob.position - Vector2.right, mob.position))
				{
					num--;
					i--;
					Move(mob, mob.position - Vector2.right, 1f);
				}
			}
			else if (player.position.x > mob.position.x && IsMoveValid(mob.position, mob.position + Vector2.right) && IsMoveValid(mob.position + Vector2.right, mob.position))
			{
				num--;
				i--;
				Move(mob, mob.position + Vector2.right, 1f);
			}
		}
		for (int j = 0; j < num; j++)
		{
			if (player.position.y < mob.position.y)
			{
				if (IsMoveValid(mob.position, mob.position - Vector2.up) && IsMoveValid(mob.position - Vector2.up, mob.position))
				{
					num--;
					j--;
					Move(mob, mob.position - Vector2.up, 1f);
				}
			}
			else if (player.position.y > mob.position.y && IsMoveValid(mob.position, mob.position + Vector2.up) && IsMoveValid(mob.position + Vector2.up, mob.position))
			{
				num--;
				j--;
				Move(mob, mob.position + Vector2.up, 1f);
			}
			for (int k = 0; k < num; k++)
			{
				if (player.position.x < mob.position.x)
				{
					if (IsMoveValid(mob.position, mob.position - Vector2.right) && IsMoveValid(mob.position - Vector2.right, mob.position))
					{
						num--;
						k--;
						Move(mob, mob.position - Vector2.right, 1f);
					}
				}
				else if (player.position.x > mob.position.x && IsMoveValid(mob.position, mob.position + Vector2.right) && IsMoveValid(mob.position + Vector2.right, mob.position))
				{
					num--;
					k--;
					Move(mob, mob.position + Vector2.right, 1f);
				}
			}
		}
	}

	public void AIMob1Doc(ThanThuDaoObject mob)
	{
		int num = 1;
		for (int i = 0; i < num; i++)
		{
			if (player.position.y < mob.position.y)
			{
				if (IsMoveValid(mob.position, mob.position - Vector2.up) && IsMoveValid(mob.position - Vector2.up, mob.position))
				{
					num--;
					Move(mob, mob.position - Vector2.up, 1f);
				}
			}
			else if (player.position.y > mob.position.y && IsMoveValid(mob.position, mob.position + Vector2.up) && IsMoveValid(mob.position + Vector2.up, mob.position))
			{
				num--;
				Move(mob, mob.position + Vector2.up, 1f);
			}
		}
		for (int j = 0; j < num; j++)
		{
			if (player.position.x < mob.position.x)
			{
				if (IsMoveValid(mob.position, mob.position - Vector2.right) && IsMoveValid(mob.position - Vector2.right, mob.position))
				{
					num--;
					Move(mob, mob.position - Vector2.right, 1f);
				}
			}
			else if (player.position.x > mob.position.x && IsMoveValid(mob.position, mob.position + Vector2.right) && IsMoveValid(mob.position + Vector2.right, mob.position))
			{
				num--;
				Move(mob, mob.position + Vector2.right, 1f);
			}
		}
	}

	public void AIMob1Ngang(ThanThuDaoObject mob)
	{
		int num = 1;
		for (int i = 0; i < num; i++)
		{
			if (player.position.x < mob.position.x)
			{
				if (IsMoveValid(mob.position, mob.position - Vector2.right) && IsMoveValid(mob.position - Vector2.right, mob.position))
				{
					num--;
					i--;
					Move(mob, mob.position - Vector2.right, 1f);
				}
			}
			else if (player.position.x > mob.position.x && IsMoveValid(mob.position, mob.position + Vector2.right) && IsMoveValid(mob.position + Vector2.right, mob.position))
			{
				num--;
				i--;
				Move(mob, mob.position + Vector2.right, 1f);
			}
		}
		for (int j = 0; j < num; j++)
		{
			if (player.position.y < mob.position.y)
			{
				if (IsMoveValid(mob.position, mob.position - Vector2.up) && IsMoveValid(mob.position - Vector2.up, mob.position))
				{
					num--;
					j--;
					Move(mob, mob.position - Vector2.up, 1f);
				}
			}
			else if (player.position.y > mob.position.y && IsMoveValid(mob.position, mob.position + Vector2.up) && IsMoveValid(mob.position + Vector2.up, mob.position))
			{
				num--;
				j--;
				Move(mob, mob.position + Vector2.up, 1f);
			}
		}
	}

	private void GameOver()
	{
		ScreenBatThanThu screenBatThanThu = (ScreenBatThanThu)GUIManager.getScreen(GAME_SCREEN.ScreenBatThanThu);
		if (screenBatThanThu != null)
		{
			screenBatThanThu.OnGameOver();
		}
		isInit = false;
	}

	private void Finish()
	{
		player.FinishMove(goalStair.gameObject);
		isInit = false;
	}
}

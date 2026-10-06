using System;
using UnityEngine;

public class ThanThuDaoObject : MonoBehaviour
{
	public ScreenBatThanThu3D screenBatThanThu3D;

	public Vector2 position;

	public ThanThuDaoInfo.WallBlock wallData;

	public ThanThuDaoInfo.MobInfo mobData;

	public GameObject TopWall;

	public GameObject BotWall;

	public GameObject LeftWall;

	public GameObject RightWall;

	private Vector3 targetPos;

	private Vector3 speed;

	private float timer;

	public bool isMoving;

	private bool isFinishMove;

	private Action<ThanThuDaoObject> action;

	private Vector3 next_targetPos;

	private Vector3 next_speed;

	private float next_timer;

	private Vector2 next_pos;

	public GameObject guiPanel;

	public GameObject guiTop;

	public GameObject guiBot;

	public GameObject guiLeft;

	public GameObject guiRight;

	public ThanThuDaoInfo.WallBlock WallData
	{
		get
		{
			return wallData;
		}
		set
		{
			wallData = value;
			if (value != null)
			{
				if (TopWall != null)
				{
					TopWall.SetActive(wallData.Top);
				}
				if (BotWall != null)
				{
					BotWall.SetActive(wallData.Bot);
				}
				if (LeftWall != null)
				{
					LeftWall.SetActive(wallData.Left);
				}
				if (RightWall != null)
				{
					RightWall.SetActive(wallData.Right);
				}
			}
		}
	}

	public void StopMove()
	{
		next_timer = 0f;
	}

	public void FinishGame(ThanThuDaoObject obj)
	{
		GameManager.instance.m_GameClient.RequestBatThanThu();
	}

	public void FinishMove(GameObject target)
	{
		timer = 1f;
		isMoving = true;
		targetPos = target.transform.position;
		speed = (target.transform.position - base.transform.position) / 1f;
		position.x = mobData.X;
		position.y = mobData.Y;
		action = FinishGame;
		if (base.name == "Player")
		{
			GetComponentInChildren<Avatar3D>().FadingInRun(0.2f);
		}
		base.transform.LookAt(target.transform.position - target.transform.position.y * Vector3.up);
		next_timer = 0f;
	}

	public void StartMove(ThanThuDaoObject target, float time, Action<ThanThuDaoObject> action)
	{
		if (!isMoving)
		{
			timer = time;
			isMoving = true;
			targetPos = target.transform.position;
			speed = (target.transform.position - base.transform.position) / time;
			mobData.X = (int)target.position.x;
			mobData.Y = (int)target.position.y;
			position.x = mobData.X;
			position.y = mobData.Y;
			this.action = action;
			if (base.name == "Player")
			{
				GetComponentInChildren<Avatar3D>().FadingInRun(0.2f);
			}
			else
			{
				GetComponent<Animation>().CrossFade("run", 0.2f);
			}
			base.transform.LookAt(target.transform);
		}
		else
		{
			next_timer = time;
			next_targetPos = target.transform.position;
			next_speed = (target.transform.position - base.transform.position) / time;
			next_pos.x = (int)target.position.x;
			next_pos.y = (int)target.position.y;
			this.action = action;
		}
	}

	private void Update()
	{
		if (!isMoving)
		{
			return;
		}
		timer -= Time.deltaTime;
		if (timer > 0f)
		{
			base.transform.position += Time.deltaTime * speed;
		}
		else if (next_timer > 0f)
		{
			if (action != null)
			{
				action(this);
			}
			if (next_timer != 0f)
			{
				timer = next_timer;
				isMoving = true;
				targetPos = next_targetPos;
				speed = (next_targetPos - base.transform.position) / next_timer;
				base.transform.LookAt(next_targetPos);
				GetComponent<Animation>().CrossFade("run", 0.2f);
				mobData.X = (int)next_pos.x;
				mobData.Y = (int)next_pos.y;
				position.x = mobData.X;
				position.y = mobData.Y;
				next_timer = 0f;
			}
		}
		else
		{
			base.transform.position = targetPos;
			isMoving = false;
			if (action != null)
			{
				action(this);
			}
			if (base.name == "Player")
			{
				GetComponentInChildren<Avatar3D>().FadingInIdle(0.2f);
			}
			else
			{
				GetComponent<Animation>().CrossFade("idle");
			}
		}
	}

	private void OnMouseUp()
	{
		ScreenBatThanThu screenBatThanThu = (ScreenBatThanThu)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenBatThanThu);
		if (screenBatThanThu.GameGroup.activeSelf && mobData == null)
		{
			screenBatThanThu3D.MoveTo(this);
		}
	}
}

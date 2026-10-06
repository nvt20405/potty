using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupDangNhapSlotMachineAnim : MonoBehaviour
{
	private enum CURRENT_POS_STATE
	{
		TOP_POS = 1,
		MID_POS = 2,
		BOTTOM_POS = 3
	}

	public OtherAvatar ItemPrefab;

	private CURRENT_POS_STATE m_CurrentPosState;

	private CURRENT_POS_STATE m_NextPosState;

	private CURRENT_POS_STATE m_posState = CURRENT_POS_STATE.MID_POS;

	private float MAX_SPEED;

	private float MIN_SPEED;

	private float SPEED_RATE = 0.1f;

	private float SPEED_DOWN;

	private int NUM_LOOP;

	private float m_currentSpeed;

	private int m_currentLoop;

	private bool m_increaseSpeed = true;

	private Vector3 m_NextPos;

	private PhanThuongResponse finalPhanThuong;

	private bool isStopAnim;

	public Action onFinish;

	private List<string> listDataStr = new List<string>();

	private float ITEM_SIZE = 150f;

	private bool isStartPlay;

	private void Start()
	{
		isStopAnim = false;
	}

	private void Update()
	{
		if (isStopAnim || !isStartPlay || !(base.transform.localPosition != m_NextPos))
		{
			return;
		}
		Vector3 vector = m_NextPos - base.transform.localPosition;
		vector = vector / vector.magnitude * m_currentSpeed * Time.deltaTime;
		Vector3 vector2 = base.transform.localPosition + vector;
		if (m_NextPos != vector2 && Vector3.Dot(m_NextPos - base.transform.localPosition, m_NextPos - vector2) > 0f)
		{
			base.transform.localPosition = vector2;
		}
		else
		{
			base.transform.localPosition = m_NextPos;
			m_CurrentPosState = m_NextPosState;
			getNextPos();
		}
		if (m_increaseSpeed)
		{
			if (m_currentSpeed < MAX_SPEED)
			{
				m_currentSpeed += Time.deltaTime * SPEED_RATE;
				m_currentSpeed = ((!(m_currentSpeed > MAX_SPEED)) ? m_currentSpeed : MAX_SPEED);
			}
		}
		else if (m_currentSpeed > MIN_SPEED)
		{
			m_currentSpeed -= Time.deltaTime * SPEED_DOWN;
			m_currentSpeed = ((!(m_currentSpeed < MIN_SPEED)) ? m_currentSpeed : MIN_SPEED);
		}
	}

	public void Reset(int count)
	{
	}

	public void stopAnim()
	{
		isStopAnim = true;
	}

	public void play(float maxSpeed, float speedRate, List<string> listData, PhanThuongResponse response)
	{
		ItemPrefab.Set("empty");
		isStartPlay = true;
		isStopAnim = false;
		MAX_SPEED = maxSpeed;
		MIN_SPEED = ITEM_SIZE * 0.5f;
		SPEED_RATE = speedRate;
		SPEED_DOWN = speedRate;
		NUM_LOOP = listData.Count;
		m_CurrentPosState = m_CurrentPosState;
		m_NextPosState = m_CurrentPosState;
		m_currentLoop = 0;
		m_currentSpeed = 0f;
		m_increaseSpeed = true;
		finalPhanThuong = response;
		listDataStr = listData;
		if (base.transform.localPosition.y == 0f)
		{
			m_CurrentPosState = CURRENT_POS_STATE.MID_POS;
			m_NextPosState = CURRENT_POS_STATE.MID_POS;
		}
		else if (base.transform.localPosition.y > 0f && base.transform.localPosition.y <= ITEM_SIZE)
		{
			m_CurrentPosState = CURRENT_POS_STATE.TOP_POS;
			m_NextPosState = CURRENT_POS_STATE.TOP_POS;
		}
		else
		{
			m_CurrentPosState = CURRENT_POS_STATE.BOTTOM_POS;
			m_NextPosState = CURRENT_POS_STATE.BOTTOM_POS;
		}
		getNextPos(true);
	}

	private void getNextPos(bool isFirstCall = false)
	{
		if (m_NextPosState == CURRENT_POS_STATE.TOP_POS)
		{
			m_NextPos = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y - ITEM_SIZE, base.transform.localPosition.z);
			m_CurrentPosState = CURRENT_POS_STATE.TOP_POS;
			m_NextPosState = CURRENT_POS_STATE.MID_POS;
		}
		else if (m_NextPosState == CURRENT_POS_STATE.MID_POS)
		{
			m_NextPos = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y - ITEM_SIZE, base.transform.localPosition.z);
			m_CurrentPosState = CURRENT_POS_STATE.MID_POS;
			m_NextPosState = CURRENT_POS_STATE.BOTTOM_POS;
		}
		else
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, ITEM_SIZE, base.transform.localPosition.z);
			m_CurrentPosState = CURRENT_POS_STATE.TOP_POS;
			m_NextPos = new Vector3(base.transform.localPosition.x, 0f, base.transform.localPosition.z);
			m_NextPosState = CURRENT_POS_STATE.MID_POS;
		}
		if (m_CurrentPosState == m_posState && !isFirstCall)
		{
			m_currentLoop++;
			if (m_currentLoop + 1 == NUM_LOOP)
			{
				m_increaseSpeed = false;
			}
			else if (m_currentLoop == NUM_LOOP)
			{
				isStartPlay = false;
				isStopAnim = true;
				onFinish();
			}
		}
		if (m_CurrentPosState == CURRENT_POS_STATE.TOP_POS && listDataStr != null && listDataStr[m_currentLoop] != null)
		{
			if (listDataStr[m_currentLoop] == "VANG")
			{
				ItemPrefab.SetVang(500000);
			}
			else if (listDataStr[m_currentLoop] == "BAC")
			{
				ItemPrefab.SetBac(500000);
			}
			else
			{
				ItemPrefab.Set(listDataStr[m_currentLoop]);
			}
		}
		if (m_NextPosState == m_posState && m_currentLoop + 1 == NUM_LOOP)
		{
			displayPhanThuong();
		}
	}

	public void displayPhanThuong()
	{
		if (finalPhanThuong == null || finalPhanThuong.PhanThuongList[0] == null)
		{
			return;
		}
		if (finalPhanThuong.PhanThuongList[0].Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
		{
			string code = "OtherAvatar_Vang_0";
			if (finalPhanThuong.PhanThuongList[0].Count >= 500000)
			{
				code = "OtherAvatar_Vang_3";
			}
			else if (finalPhanThuong.PhanThuongList[0].Count >= 100000)
			{
				code = "OtherAvatar_Vang_2";
			}
			else if (finalPhanThuong.PhanThuongList[0].Count >= 20000)
			{
				code = "OtherAvatar_Vang_1";
			}
			ItemPrefab.Set(code);
		}
		else if (finalPhanThuong.PhanThuongList[0].Loai == PhanThuongResponse.LoaiPhanThuong.BAC)
		{
			string code2 = "OtherAvatar_Bac_0";
			if (finalPhanThuong.PhanThuongList[0].Count >= 500000)
			{
				code2 = "OtherAvatar_Bac_3";
			}
			else if (finalPhanThuong.PhanThuongList[0].Count >= 100000)
			{
				code2 = "OtherAvatar_Bac_2";
			}
			else if (finalPhanThuong.PhanThuongList[0].Count >= 20000)
			{
				code2 = "OtherAvatar_Bac_1";
			}
			ItemPrefab.Set(code2);
		}
		else
		{
			ItemPrefab.Set(finalPhanThuong.PhanThuongList[0].Name);
		}
	}
}

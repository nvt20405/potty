using System;
using System.Collections.Generic;
using UnityEngine;

public class QuaySoItem : MonoBehaviour
{
	public List<QuaySoItemAnim> listItem;

	private int finalNumber;

	private float itemSize;

	public Action<CardIndex> OnFinishPlay;

	public CardIndex m_CardIndex;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void updatePosition()
	{
		int num = 0;
		float num2 = 0f;
		num2 = UnityEngine.Random.Range(itemSize * 5f, itemSize * 10f);
		num += UnityEngine.Random.Range(-2, 2);
		listItem[0].lbDisplayNum.text = "0";
		listItem[1].lbDisplayNum.text = "0";
		if (listItem[0].transform.localPosition.y != itemSize)
		{
			listItem[0].transform.localPosition = new Vector3(listItem[0].transform.localPosition.x, itemSize, listItem[0].transform.localPosition.z);
		}
		if (listItem[1].transform.localPosition.y != 0f)
		{
			listItem[1].transform.localPosition = new Vector3(listItem[0].transform.localPosition.x, 0f, listItem[0].transform.localPosition.z);
		}
		listItem[0].onFinish = OnFinish1Anim;
		listItem[1].onFinish = OnFinish2Anim;
		listItem[0].play(itemSize * 10f, num2, finalNumber);
		listItem[1].play(itemSize * 10f, num2, finalNumber);
	}

	public void OnFinish1Anim()
	{
		listItem[1].stopAnim();
		if (listItem[1].transform.localPosition.y != itemSize)
		{
			listItem[1].transform.localPosition = new Vector3(listItem[0].transform.localPosition.x, itemSize, listItem[0].transform.localPosition.z);
		}
		OnFinishPlay(m_CardIndex);
	}

	public void OnFinish2Anim()
	{
		listItem[0].stopAnim();
		if (listItem[0].transform.localPosition.y != itemSize)
		{
			listItem[0].transform.localPosition = new Vector3(listItem[0].transform.localPosition.x, 0f, listItem[0].transform.localPosition.z);
		}
		OnFinishPlay(m_CardIndex);
	}

	public void Play(int numberStop, CardIndex index, float itemsize, float SPEED_RATE, int NUM_LOOP, float itemAnimSize)
	{
		finalNumber = numberStop;
		m_CardIndex = index;
		itemSize = itemsize;
		listItem[0].SPEED_RATE = SPEED_RATE;
		listItem[0].NUM_LOOP = NUM_LOOP;
		listItem[0].ITEM_SIZE = itemAnimSize;
		listItem[1].SPEED_RATE = SPEED_RATE;
		listItem[1].NUM_LOOP = NUM_LOOP;
		listItem[1].ITEM_SIZE = itemAnimSize;
		updatePosition();
	}
}

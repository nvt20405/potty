using System;
using System.Collections.Generic;
using UnityEngine;

public class QuayHoaVangItem : MonoBehaviour
{
	private int itemHeight = 180;

	public List<QuayImageItemAnim> listItem;

	private bool isStartPlay;

	private LoaiQuayDiemHoaVang finalNumber;

	private float sizeMinus = 5f;

	private float itemSize = 180f;

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
		listItem[0].spDisplayNum.spriteName = "none";
		listItem[1].spDisplayNum.spriteName = "none";
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

	public void Play(LoaiQuayDiemHoaVang numberStop, CardIndex index)
	{
		finalNumber = numberStop;
		m_CardIndex = index;
		updatePosition();
	}
}

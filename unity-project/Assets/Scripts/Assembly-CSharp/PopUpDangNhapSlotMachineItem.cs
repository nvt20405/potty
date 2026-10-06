using System;
using System.Collections.Generic;
using UnityEngine;

public class PopUpDangNhapSlotMachineItem : MonoBehaviour
{
	private int itemHeight = 200;

	public List<PopupDangNhapSlotMachineAnim> listItem;

	private bool isStartPlay;

	private int finalNumber;

	private float sizeMinus = 5f;

	private float itemSize = 150f;

	public Action<PhanThuongResponse> OnFinishPlay;

	private List<string> listDataString;

	public PhanThuongResponse mPhanThuongResponse;

	private void Start()
	{
		listItem[0].ItemPrefab.Set("empty");
		listItem[1].ItemPrefab.Set("empty");
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
		listItem[0].play(itemSize * 10f, num2, listDataString, mPhanThuongResponse);
		listItem[1].play(itemSize * 10f, num2, listDataString, mPhanThuongResponse);
	}

	public void OnFinish1Anim()
	{
		listItem[1].stopAnim();
		if (listItem[1].transform.localPosition.y != itemSize)
		{
			listItem[1].transform.localPosition = new Vector3(listItem[0].transform.localPosition.x, itemSize, listItem[0].transform.localPosition.z);
		}
		OnFinishPlay(mPhanThuongResponse);
	}

	public void OnFinish2Anim()
	{
		listItem[0].stopAnim();
		if (listItem[0].transform.localPosition.y != itemSize)
		{
			listItem[0].transform.localPosition = new Vector3(listItem[0].transform.localPosition.x, 0f, listItem[0].transform.localPosition.z);
		}
		OnFinishPlay(mPhanThuongResponse);
	}

	public void Play(List<string> listData, PhanThuongResponse phanThuongTarget)
	{
		listDataString = listData;
		mPhanThuongResponse = phanThuongTarget;
		updatePosition();
	}
}

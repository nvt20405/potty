using System.Collections.Generic;
using UnityEngine;

public class MovingCharacter : MonoBehaviour
{
	private GameObject target;

	public float deltaPos;

	public float baseSpeed;

	private float speed;

	private Vector3 speedVec;

	public Transform targetRoot;

	public float maxDis;

	public int Idx;

	public Avatar3D Model;

	private void Start()
	{
	}

	private void Init()
	{
		if (Idx < 0 || Idx > 8)
		{
			return;
		}
		if (Model != null)
		{
			Object.Destroy(Model.gameObject);
		}
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		if (userInfo != null && userInfo.HeroList != null && userInfo.DoiHinh != null && userInfo.DoiHinh.ListRaTran != null && Idx < userInfo.DoiHinh.ListRaTran.Count)
		{
			UserInfo.HeroData heroData = userInfo.HeroList.Find((UserInfo.HeroData hero) => hero.HID == userInfo.DoiHinh.ListRaTran[Idx]);
			if (heroData != null)
			{
				Model = GUIManager.instance.InstantiateAvatar3D(heroData.Name, string.Empty, string.Empty, string.Empty, null, null, string.Empty, string.Empty, string.Empty);
				Model.FadeInWalkAnim(0f);
				Model.transform.parent = base.transform;
				Model.transform.localScale = 2f * Vector3.one;
			}
		}
	}

	private void OnEnable()
	{
		if (Model != null)
		{
			Model.FadeInWalkAnim(0f);
		}
	}

	private void Update()
	{
		if (target == null || Vector3.Distance(base.transform.position, target.transform.position) < deltaPos)
		{
			if (target == null)
			{
				Init();
			}
			NewTarget();
		}
		base.transform.position += Time.deltaTime * speed * speedVec;
	}

	public void NewTarget()
	{
		target = targetRoot.GetChild(Random.Range(0, targetRoot.childCount)).gameObject;
		speed = Random.Range(baseSpeed * 0.5f, baseSpeed * 2f);
		List<Transform> list = new List<Transform>();
		foreach (Transform item in targetRoot)
		{
			Transform transform2 = item;
			float num = Vector3.Distance(transform2.transform.position, target.transform.position);
			if (num > 0f && num < maxDis)
			{
				list.Add(transform2.transform);
			}
		}
		base.transform.position = list[Random.Range(0, list.Count)].position;
		base.transform.LookAt(target.transform.position);
		speedVec = (target.transform.position - base.transform.position).normalized;
	}
}

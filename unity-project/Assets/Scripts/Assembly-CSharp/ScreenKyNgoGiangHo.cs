using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenKyNgoGiangHo : ScreenBase
{
	public EGGUIPagingPanel pagingPanel;

	public Transform topBanner;

	public UIPanel topPanel;

	private Dictionary<int, EGGUIPage> pagesDictionary = new Dictionary<int, EGGUIPage>();

	private Dictionary<int, KyNgoAvatar> avatarDic = new Dictionary<int, KyNgoAvatar>();

	private List<KyNgoCaoNhanPage> caoNhanPages = new List<KyNgoCaoNhanPage>();

	private List<KyNgoBanDoPage> banDoPages = new List<KyNgoBanDoPage>();

	private List<KyNgoBangHuuPage> bangHuuPages = new List<KyNgoBangHuuPage>();

	private List<KyNgoThuongNhanPage> thuongNhanPages = new List<KyNgoThuongNhanPage>();

	private List<KyNgoTyThiPage> tyThiPages = new List<KyNgoTyThiPage>();

	private int selectedPage;

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		SyncWithNetworkData(GameManager.instance.m_GameClient.UserInfo);
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
		pagingPanel.onChangePage = OnSelectPage;
	}

	private void OnAvatarClick(GameObject go)
	{
		int p = 0;
		foreach (KeyValuePair<int, KyNgoAvatar> item in avatarDic)
		{
			if (item.Value.gameObject != go)
			{
				item.Value.Select(false);
				continue;
			}
			p = item.Key;
			item.Value.Select(true);
		}
		selectedPage = p;
		SelectPage(p);
	}

	private void OnSelectPage(int num)
	{
		SelectPage(num);
	}

	private void SelectAvatar(int num)
	{
		KyNgoAvatar value;
		if (!avatarDic.TryGetValue(num, out value))
		{
			return;
		}
		foreach (KeyValuePair<int, KyNgoAvatar> item in avatarDic)
		{
			if (item.Key != num)
			{
				item.Value.Select(false);
			}
			else
			{
				item.Value.Select(true);
			}
		}
		float x = 130f;
		float y = 110f;
		Matrix4x4 worldToLocalMatrix = topPanel.transform.worldToLocalMatrix;
		Transform transform = topBanner;
		Vector2 vector = default(Vector2);
		vector = new Vector2(x, y);
		Vector2 vector2 = default(Vector2);
		vector2 = new Vector2(value.transform.localPosition.x, value.transform.localPosition.y);
		Vector2 vector3 = vector2 - vector / 2f;
		Vector2 vector4 = vector2 + vector / 2f;
		Vector3 v = transform.TransformPoint(new Vector3(vector3.x, vector3.y));
		Vector3 v2 = transform.TransformPoint(new Vector3(vector4.x, vector4.y));
		Vector3 size = worldToLocalMatrix.MultiplyPoint3x4(v2) - worldToLocalMatrix.MultiplyPoint3x4(v);
		Vector3 center = worldToLocalMatrix.MultiplyPoint3x4(value.transform.position);
		Bounds bounds = default(Bounds);
		bounds = new Bounds(center, size);
		Vector3 vector5 = topPanel.CalculateConstrainOffset(bounds.min, bounds.max);
		if (vector5.magnitude > 0.001f)
		{
			SpringPanel.Begin(topPanel.gameObject, topPanel.transform.localPosition + vector5, 13f);
		}
	}

	private void SelectPage(int p)
	{
		if (selectedPage != p)
		{
			SelectAvatar(p);
			selectedPage = p;
		}
		if (pagingPanel.PageSelected != p)
		{
			pagingPanel.MoveToPage(p);
		}
	}

	public void SyncWithNetworkData(UserInfo uInfo)
	{
		EGGUIPage[] componentsInChildren = pagingPanel.GetComponentsInChildren<EGGUIPage>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].gameObject.SetActive(false);
			UnityEngine.Object.Destroy(componentsInChildren[i].gameObject);
		}
		KyNgoAvatar[] componentsInChildren2 = topBanner.GetComponentsInChildren<KyNgoAvatar>(true);
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			componentsInChildren2[j].gameObject.SetActive(false);
			UnityEngine.Object.Destroy(componentsInChildren2[j].gameObject);
		}
		avatarDic.Clear();
		pagesDictionary.Clear();
		caoNhanPages.Clear();
		banDoPages.Clear();
		bangHuuPages.Clear();
		thuongNhanPages.Clear();
		tyThiPages.Clear();
		int k = 0;
		int num = uInfo.Gamer.KyNgoCaoNhan.Count + uInfo.Gamer.KyNgoBanDo.Count + uInfo.Gamer.KyNgoBangHuu.Count + uInfo.Gamer.KyNgoThuongNhan.Count + uInfo.Gamer.KyNgoTyThi.Count;
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		List<int> list4 = new List<int>();
		List<int> list5 = new List<int>();
		int minKyNgoCaoNhan = GetMinKyNgoCaoNhan(uInfo, list);
		int minKyNgoBanDo = GetMinKyNgoBanDo(uInfo, list3);
		int minKyNgoBangHuu = GetMinKyNgoBangHuu(uInfo, list2);
		int minKyNgoThuongNhan = GetMinKyNgoThuongNhan(uInfo, list4);
		int minKyNgoTyThi = GetMinKyNgoTyThi(uInfo, list5);
		DateTime dateTime = ((minKyNgoCaoNhan >= 0) ? uInfo.Gamer.KyNgoCaoNhan[minKyNgoCaoNhan].Time : DateTime.MaxValue);
		DateTime dateTime2 = ((minKyNgoBanDo >= 0) ? uInfo.Gamer.KyNgoBanDo[minKyNgoBanDo].Time : DateTime.MaxValue);
		DateTime dateTime3 = ((minKyNgoBangHuu >= 0) ? uInfo.Gamer.KyNgoBangHuu[minKyNgoBangHuu].Time : DateTime.MaxValue);
		DateTime dateTime4 = ((minKyNgoThuongNhan >= 0) ? uInfo.Gamer.KyNgoThuongNhan[minKyNgoThuongNhan].GioDi : DateTime.MaxValue);
		DateTime dateTime5 = ((minKyNgoTyThi >= 0) ? uInfo.Gamer.KyNgoTyThi[minKyNgoTyThi].GioDi : DateTime.MaxValue);
		Vector3 vector = default(Vector3);
		vector = new Vector3(130f, 0f, 0f);
		for (; k < num; k++)
		{
			if (dateTime <= dateTime3 && dateTime <= dateTime2 && dateTime <= dateTime4 && dateTime <= dateTime5)
			{
				UnityEngine.Object obj = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/KyNgo/CaoNhanPage"));
				GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
				if (gameObject != null)
				{
					gameObject.transform.parent = pagingPanel.transform;
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localScale = Vector3.one;
					gameObject.name = string.Format("Page_{0:00}", k);
					KyNgoCaoNhanPage component = gameObject.GetComponent<KyNgoCaoNhanPage>();
					caoNhanPages.Add(component);
					pagesDictionary.Add(k, gameObject.GetComponent<EGGUIPage>());
					component.SetInfo(uInfo.Gamer.KyNgoCaoNhan[minKyNgoCaoNhan], minKyNgoCaoNhan);
				}
				UnityEngine.Object obj2 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/KyNgo/KyNgoAvatar"));
				GameObject gameObject2 = (GameObject)((obj2 is GameObject) ? obj2 : null);
				gameObject2.transform.parent = topBanner;
				gameObject2.transform.localPosition = k * vector + new Vector3(0f, 0f, -3f);
				gameObject2.transform.localScale = Vector3.one;
				KyNgoAvatar component2 = gameObject2.GetComponent<KyNgoAvatar>();
				component2.SetInfo("kyngo_caonhan", selectedPage == k);
				component2.OnAvatarClick = OnAvatarClick;
				avatarDic.Add(k, component2);
				list.Add(minKyNgoCaoNhan);
				minKyNgoCaoNhan = GetMinKyNgoCaoNhan(uInfo, list);
			}
			else if (dateTime2 <= dateTime3 && dateTime2 <= dateTime && dateTime2 <= dateTime4 && dateTime2 <= dateTime5)
			{
				UnityEngine.Object obj3 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/KyNgo/BanDoPage"));
				GameObject gameObject3 = (GameObject)((obj3 is GameObject) ? obj3 : null);
				if (gameObject3 != null)
				{
					gameObject3.transform.parent = pagingPanel.transform;
					gameObject3.transform.localPosition = Vector3.zero;
					gameObject3.transform.localScale = Vector3.one;
					gameObject3.name = string.Format("Page_{0:00}", k);
					KyNgoBanDoPage component3 = gameObject3.GetComponent<KyNgoBanDoPage>();
					banDoPages.Add(component3);
					pagesDictionary.Add(k, gameObject3.GetComponent<EGGUIPage>());
					component3.SetInfo(uInfo.Gamer.KyNgoBanDo[minKyNgoBanDo], minKyNgoBanDo);
				}
				UnityEngine.Object obj4 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/KyNgo/KyNgoAvatar"));
				GameObject gameObject4 = (GameObject)((obj4 is GameObject) ? obj4 : null);
				gameObject4.transform.parent = topBanner;
				gameObject4.transform.localPosition = k * vector + new Vector3(0f, 0f, -3f);
				gameObject4.transform.localScale = Vector3.one;
				KyNgoAvatar component4 = gameObject4.GetComponent<KyNgoAvatar>();
				component4.SetInfo("kyngo_khobau", selectedPage == k);
				component4.OnAvatarClick = OnAvatarClick;
				avatarDic.Add(k, component4);
				list3.Add(minKyNgoBanDo);
				minKyNgoBanDo = GetMinKyNgoBanDo(uInfo, list3);
			}
			else if (dateTime3 <= dateTime2 && dateTime3 <= dateTime && dateTime3 <= dateTime4 && dateTime3 <= dateTime5)
			{
				UnityEngine.Object obj5 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/KyNgo/BangHuuPage"));
				GameObject gameObject5 = (GameObject)((obj5 is GameObject) ? obj5 : null);
				if (gameObject5 != null)
				{
					gameObject5.transform.parent = pagingPanel.transform;
					gameObject5.transform.localPosition = Vector3.zero;
					gameObject5.transform.localScale = Vector3.one;
					gameObject5.name = string.Format("Page_{0:00}", k);
					KyNgoBangHuuPage component5 = gameObject5.GetComponent<KyNgoBangHuuPage>();
					bangHuuPages.Add(component5);
					pagesDictionary.Add(k, gameObject5.GetComponent<EGGUIPage>());
					component5.SetInfo(uInfo.Gamer.KyNgoBangHuu[minKyNgoBangHuu], minKyNgoBangHuu);
				}
				UnityEngine.Object obj6 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/KyNgo/KyNgoAvatar"));
				GameObject gameObject6 = (GameObject)((obj6 is GameObject) ? obj6 : null);
				gameObject6.transform.parent = topBanner;
				gameObject6.transform.localPosition = k * vector + new Vector3(0f, 0f, -3f);
				gameObject6.transform.localScale = Vector3.one;
				KyNgoAvatar component6 = gameObject6.GetComponent<KyNgoAvatar>();
				component6.SetInfo("kyngo_banghuu", selectedPage == k);
				component6.OnAvatarClick = OnAvatarClick;
				avatarDic.Add(k, component6);
				list2.Add(minKyNgoBangHuu);
				minKyNgoBangHuu = GetMinKyNgoBangHuu(uInfo, list2);
			}
			else if (dateTime4 <= dateTime2 && dateTime4 <= dateTime && dateTime4 <= dateTime3 && dateTime4 <= dateTime5)
			{
				UnityEngine.Object obj7 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/KyNgo/ThuongNhanPage"));
				GameObject gameObject7 = (GameObject)((obj7 is GameObject) ? obj7 : null);
				if (gameObject7 != null)
				{
					gameObject7.transform.parent = pagingPanel.transform;
					gameObject7.transform.localPosition = Vector3.zero;
					gameObject7.transform.localScale = Vector3.one;
					gameObject7.name = string.Format("Page_{0:00}", k);
					KyNgoThuongNhanPage component7 = gameObject7.GetComponent<KyNgoThuongNhanPage>();
					thuongNhanPages.Add(component7);
					pagesDictionary.Add(k, gameObject7.GetComponent<EGGUIPage>());
					component7.SetInfo(uInfo.Gamer.KyNgoThuongNhan[minKyNgoThuongNhan], minKyNgoThuongNhan);
				}
				UnityEngine.Object obj8 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/KyNgo/KyNgoAvatar"));
				GameObject gameObject8 = (GameObject)((obj8 is GameObject) ? obj8 : null);
				gameObject8.transform.parent = topBanner;
				gameObject8.transform.localPosition = k * vector + new Vector3(0f, 0f, -3f);
				gameObject8.transform.localScale = Vector3.one;
				KyNgoAvatar component8 = gameObject8.GetComponent<KyNgoAvatar>();
				component8.SetInfo("kyngo_thuongnhan", selectedPage == k);
				component8.OnAvatarClick = OnAvatarClick;
				avatarDic.Add(k, component8);
				list4.Add(minKyNgoThuongNhan);
				minKyNgoThuongNhan = GetMinKyNgoThuongNhan(uInfo, list4);
			}
			else
			{
				UnityEngine.Object obj9 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/KyNgo/TyThiPage"));
				GameObject gameObject9 = (GameObject)((obj9 is GameObject) ? obj9 : null);
				if (gameObject9 != null)
				{
					gameObject9.transform.parent = pagingPanel.transform;
					gameObject9.transform.localPosition = Vector3.zero;
					gameObject9.transform.localScale = Vector3.one;
					gameObject9.name = string.Format("Page_{0:00}", k);
					KyNgoTyThiPage component9 = gameObject9.GetComponent<KyNgoTyThiPage>();
					tyThiPages.Add(component9);
					pagesDictionary.Add(k, gameObject9.GetComponent<EGGUIPage>());
					gameObject9.GetComponent<KyNgoTyThiPage>().SetInfo(uInfo.Gamer.KyNgoTyThi[minKyNgoTyThi], minKyNgoTyThi);
				}
				UnityEngine.Object obj10 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/KyNgo/KyNgoAvatar"));
				GameObject gameObject10 = (GameObject)((obj10 is GameObject) ? obj10 : null);
				gameObject10.transform.parent = topBanner;
				gameObject10.transform.localPosition = k * vector + new Vector3(0f, 0f, -3f);
				gameObject10.transform.localScale = Vector3.one;
				KyNgoAvatar component10 = gameObject10.GetComponent<KyNgoAvatar>();
				component10.SetInfo("kyngo_tythi", selectedPage == k);
				component10.OnAvatarClick = OnAvatarClick;
				avatarDic.Add(k, component10);
				list5.Add(minKyNgoTyThi);
				minKyNgoTyThi = GetMinKyNgoTyThi(uInfo, list5);
			}
			dateTime = ((minKyNgoCaoNhan >= 0) ? uInfo.Gamer.KyNgoCaoNhan[minKyNgoCaoNhan].Time : DateTime.MaxValue);
			dateTime2 = ((minKyNgoBanDo >= 0) ? uInfo.Gamer.KyNgoBanDo[minKyNgoBanDo].Time : DateTime.MaxValue);
			dateTime3 = ((minKyNgoBangHuu >= 0) ? uInfo.Gamer.KyNgoBangHuu[minKyNgoBangHuu].Time : DateTime.MaxValue);
			dateTime4 = ((minKyNgoThuongNhan >= 0) ? uInfo.Gamer.KyNgoThuongNhan[minKyNgoThuongNhan].GioDi : DateTime.MaxValue);
			dateTime5 = ((minKyNgoTyThi >= 0) ? uInfo.Gamer.KyNgoTyThi[minKyNgoTyThi].GioDi : DateTime.MaxValue);
		}
		pagingPanel.Pagging();
		StartCoroutine(DelayMoveToPage());
	}

	private IEnumerator DelayMoveToPage()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		SelectPage(selectedPage);
	}

	private int GetMinKyNgoCaoNhan(UserInfo uInfo, List<int> excludes)
	{
		int num = -1;
		if (excludes.Count == uInfo.Gamer.KyNgoCaoNhan.Count)
		{
			return num;
		}
		for (int i = 0; i < uInfo.Gamer.KyNgoCaoNhan.Count; i++)
		{
			if (!excludes.Contains(i) && (num < 0 || uInfo.Gamer.KyNgoCaoNhan[i].Time < uInfo.Gamer.KyNgoCaoNhan[num].Time))
			{
				num = i;
			}
		}
		return num;
	}

	private int GetMinKyNgoBangHuu(UserInfo uInfo, List<int> excludes)
	{
		int num = -1;
		if (excludes.Count == uInfo.Gamer.KyNgoBangHuu.Count)
		{
			return num;
		}
		for (int i = 0; i < uInfo.Gamer.KyNgoBangHuu.Count; i++)
		{
			if (!excludes.Contains(i) && (num < 0 || uInfo.Gamer.KyNgoBangHuu[i].Time < uInfo.Gamer.KyNgoBangHuu[num].Time))
			{
				num = i;
			}
		}
		return num;
	}

	private int GetMinKyNgoBanDo(UserInfo uInfo, List<int> excludes)
	{
		int num = -1;
		if (excludes.Count == uInfo.Gamer.KyNgoBanDo.Count)
		{
			return num;
		}
		for (int i = 0; i < uInfo.Gamer.KyNgoBanDo.Count; i++)
		{
			if (!excludes.Contains(i) && (num < 0 || uInfo.Gamer.KyNgoBanDo[i].Time < uInfo.Gamer.KyNgoBanDo[num].Time))
			{
				num = i;
			}
		}
		return num;
	}

	private int GetMinKyNgoThuongNhan(UserInfo uInfo, List<int> excludes)
	{
		int num = -1;
		if (excludes.Count == uInfo.Gamer.KyNgoThuongNhan.Count)
		{
			return num;
		}
		for (int i = 0; i < uInfo.Gamer.KyNgoThuongNhan.Count; i++)
		{
			if (!excludes.Contains(i) && (num < 0 || uInfo.Gamer.KyNgoThuongNhan[i].GioDi < uInfo.Gamer.KyNgoThuongNhan[num].GioDi))
			{
				num = i;
			}
		}
		return num;
	}

	private int GetMinKyNgoTyThi(UserInfo uInfo, List<int> excludes)
	{
		int num = -1;
		if (excludes.Count == uInfo.Gamer.KyNgoTyThi.Count)
		{
			return num;
		}
		for (int i = 0; i < uInfo.Gamer.KyNgoTyThi.Count; i++)
		{
			if (!excludes.Contains(i) && (num < 0 || uInfo.Gamer.KyNgoTyThi[i].GioDi < uInfo.Gamer.KyNgoTyThi[num].GioDi))
			{
				num = i;
			}
		}
		return num;
	}

	private void OnBackBtnClick()
	{
		GUIManager.setScreen(GAME_SCREEN.ScreenWorldmap);
		if (GiangHoPopup.instance != null)
		{
			GiangHoPopup.instance.gameObject.SetActive(true);
		}
	}

	public void OnCloseBattleResult()
	{
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.instance.gameObject.SetActive(true);
		}
	}
}

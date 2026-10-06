using System;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLienMinhTrongCay : ScreenBase
{
	public enum STATE
	{
		HOME = 0,
		OTHER = 1
	}

	public UserInfo.LinhDuocType selectedType;

	public List<GameObject> LinhDuocSlot;

	public List<GameObject> LinhDuocList;

	public List<GameObject> InfoList;

	public List<UILabel> SeedCountList;

	public UILabel StealCount;

	public UILabel NguyenKhiDanCount;

	public UILabel PickSeedCount;

	public UILabel ActivityStatus;

	public List<GameObject> SwitchList;

	public List<GameObject> SwitchList2;

	public UserInfo.LinhDuocUserData StealLinhDuocData;

	public static STATE LandState;

	private int targetThuHoachKNB;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>();
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
		}
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	public override void OnActive()
	{
		base.OnActive();
		if (child3Dscreen == null)
		{
			child3Dscreen = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("GUI/Screens3D/ScreenTrongCay3D"));
			child3Dscreen.transform.parent = GUIManager.instance.ScreenContainer3D.transform;
			child3Dscreen.SetActive(true);
		}
		GUIManager.ShowGadgets(6);
		LinhDuocSlot = new List<GameObject>();
		for (int i = 0; i < 6; i++)
		{
			LinhDuocSlot.Add(child3Dscreen.transform.Find("Slot" + (i + 1)).gameObject);
		}
		selectedType = UserInfo.LinhDuocType.NONE;
		if (GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo == null)
		{
			GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo = new UserInfo.LinhDuocUserData();
			GetGamerLinhDuocRequest getGamerLinhDuocRequest = new GetGamerLinhDuocRequest();
			getGamerLinhDuocRequest.getFull = true;
			GameManager.instance.m_GameClient.RequestGetGamerLinhDuoc(getGamerLinhDuocRequest);
		}
		OnSyncData();
		OnHatGiong1Select(false);
		OnHatGiong2Select(false);
		OnHatGiong3Select(false);
		OnHatGiong4Select(false);
	}

	public void OnChonHatGiong()
	{
		PopupChonHatGiong.Create();
	}

	public void OnAnTromList()
	{
		PopupAnTromLinhDuoc.Create();
	}

	public void OnHatGiong1Select(bool isSelected)
	{
		if (!isSelected && selectedType == UserInfo.LinhDuocType.THONG_KINH_THAO)
		{
			selectedType = UserInfo.LinhDuocType.NONE;
		}
		else if (isSelected)
		{
			if (selectedType != UserInfo.LinhDuocType.THONG_KINH_THAO)
			{
				selectedType = UserInfo.LinhDuocType.THONG_KINH_THAO;
			}
			else
			{
				selectedType = UserInfo.LinhDuocType.NONE;
			}
		}
	}

	public void OnHatGiong2Select(bool isSelected)
	{
		if (!isSelected && selectedType == UserInfo.LinhDuocType.HUYET_BO_DE)
		{
			selectedType = UserInfo.LinhDuocType.NONE;
		}
		else if (isSelected)
		{
			if (selectedType != UserInfo.LinhDuocType.HUYET_BO_DE)
			{
				selectedType = UserInfo.LinhDuocType.HUYET_BO_DE;
			}
			else
			{
				selectedType = UserInfo.LinhDuocType.NONE;
			}
		}
	}

	public void OnHatGiong3Select(bool isSelected)
	{
		if (!isSelected && selectedType == UserInfo.LinhDuocType.HAC_LINH_CHI)
		{
			selectedType = UserInfo.LinhDuocType.NONE;
		}
		else if (isSelected)
		{
			if (selectedType != UserInfo.LinhDuocType.HAC_LINH_CHI)
			{
				selectedType = UserInfo.LinhDuocType.HAC_LINH_CHI;
			}
			else
			{
				selectedType = UserInfo.LinhDuocType.NONE;
			}
		}
	}

	public void OnHatGiong4Select(bool isSelected)
	{
		if (!isSelected && selectedType == UserInfo.LinhDuocType.THIEN_SON_TUYET_LIEN)
		{
			selectedType = UserInfo.LinhDuocType.NONE;
		}
		else if (isSelected)
		{
			if (selectedType != UserInfo.LinhDuocType.THIEN_SON_TUYET_LIEN)
			{
				selectedType = UserInfo.LinhDuocType.THIEN_SON_TUYET_LIEN;
			}
			else
			{
				selectedType = UserInfo.LinhDuocType.NONE;
			}
		}
	}

	public void OnLinhDuocClick(GameObject go)
	{
		if (LandState == STATE.HOME)
		{
			if (GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocLandList[int.Parse(go.name) - 1] != null && GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocLandList[int.Parse(go.name) - 1].Quantity == 0)
			{
				if (GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocLandList[int.Parse(go.name) - 1] != null)
				{
					TrongCayRequest trongCayRequest = new TrongCayRequest();
					trongCayRequest.data = new UserInfo.LinhDuocLandData();
					trongCayRequest.data.CurLinhDuoc = selectedType;
					trongCayRequest.data.ID = GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocLandList[int.Parse(go.name) - 1].ID;
					GameManager.instance.m_GameClient.RequestTrongCay(trongCayRequest);
				}
			}
			else if (GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocLandList[int.Parse(go.name) - 1] != null)
			{
				if ((GameManager.instance.m_GameClient.ServerTime - GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocLandList[int.Parse(go.name) - 1].StartGrowTime).TotalHours > 4.0)
				{
					ThuHoachRequest thuHoachRequest = new ThuHoachRequest();
					thuHoachRequest.landID = GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocLandList[int.Parse(go.name) - 1].ID;
					GameManager.instance.m_GameClient.RequestThuHoach(thuHoachRequest);
				}
				else
				{
					targetThuHoachKNB = int.Parse(go.name) - 1;
					PopupYesNo.Create(string.Format(Localization.instance.Get("ThuHoachNhanh"), 10), Localization.instance.Get("DongY"), Localization.instance.Get("TuChoi"), ThuHoachKNB, null);
				}
			}
		}
		else if (StealLinhDuocData.LinhDuocLandList[int.Parse(go.name) - 1] != null)
		{
			AnTromRequest anTromRequest = new AnTromRequest();
			anTromRequest.landID = StealLinhDuocData.LinhDuocLandList[int.Parse(go.name) - 1].ID;
			GameManager.instance.m_GameClient.RequestAnTrom(anTromRequest);
		}
	}

	public void ThuHoachKNB()
	{
		ThuHoachRequest thuHoachRequest = new ThuHoachRequest();
		thuHoachRequest.landID = GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocLandList[targetThuHoachKNB].ID;
		GameManager.instance.m_GameClient.RequestThuHoach(thuHoachRequest);
	}

	public void BackToHome()
	{
		OnSyncData();
	}

	public void OnSyncData()
	{
		LandState = STATE.HOME;
		foreach (GameObject @switch in SwitchList)
		{
			@switch.SetActive(true);
		}
		foreach (GameObject item in SwitchList2)
		{
			item.SetActive(false);
		}
		if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_NGUYEN_KHI_DAN"))
		{
			NguyenKhiDanCount.text = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_NGUYEN_KHI_DAN").Quantity.ToString();
		}
		else
		{
			NguyenKhiDanCount.text = "0";
		}
		if (GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocActivity != null && GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocActivity.Count > 0)
		{
			string[] array = GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocActivity[0].Split(';');
			if (int.Parse(array[1]) > 0)
			{
				ActivityStatus.text = string.Format(Localization.instance.Get("StatusAnTromThanhCong"), array[0], array[1]);
			}
			else
			{
				ActivityStatus.text = string.Format(Localization.instance.Get("StatusAnTromThatBai"), array[0]);
			}
		}
		else
		{
			ActivityStatus.text = string.Empty;
		}
		StealCount.text = Mathf.Min(5, GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.StealCount) + "/5";
		PickSeedCount.text = GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.DailySeedCount + "/10";
		if (LinhDuocList != null)
		{
			for (int num = 0; num < LinhDuocList.Count; num++)
			{
				UnityEngine.Object.Destroy(LinhDuocList[num].gameObject);
			}
		}
		LinhDuocList = new List<GameObject>();
		if (GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocLandList == null)
		{
			GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocLandList = new List<UserInfo.LinhDuocLandData>();
		}
		else
		{
			for (int num2 = 0; num2 < 6; num2++)
			{
				UserInfo.LinhDuocLandData linhDuocLandData = GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.LinhDuocLandList[num2];
				int num3 = 0;
				TimeSpan timeSpan = GameManager.instance.m_GameClient.ServerTime - linhDuocLandData.StartGrowTime;
				num3 = ((timeSpan.TotalHours >= 4.0) ? 4 : ((timeSpan.TotalHours >= 3.0) ? 3 : ((!(timeSpan.TotalHours >= 1.0)) ? 1 : 2)));
				if (linhDuocLandData == null || linhDuocLandData.Quantity == 0)
				{
					InfoList[num2].SetActive(false);
					continue;
				}
				if (num3 == 4)
				{
					InfoList[num2].SetActive(false);
				}
				else
				{
					InfoList[num2].SetActive(true);
					InfoList[num2].transform.Find("Label").GetComponent<LinhDuocCountDown>().StartCountDown(linhDuocLandData.StartGrowTime + new TimeSpan(4, 0, 0) - GameManager.instance.m_GameClient.ServerTime);
				}
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(GetLinhDuocPrefab(linhDuocLandData.CurLinhDuoc.ToString(), num3));
				gameObject.transform.position = LinhDuocSlot[num2].transform.position;
				gameObject.transform.parent = child3Dscreen.transform;
				LinhDuocList.Add(gameObject);
			}
		}
		int k;
		for (k = 0; k < 4; k++)
		{
			if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData e) =>
			{
				string text = e.Name;
				UserInfo.LinhDuocType linhDuocType = (UserInfo.LinhDuocType)k;
				return text == "VP_SEED_" + linhDuocType;
			}))
			{
				SeedCountList[k].text = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) =>
				{
					string text = e.Name;
					UserInfo.LinhDuocType linhDuocType = (UserInfo.LinhDuocType)k;
					return text == "VP_SEED_" + linhDuocType;
				}).Quantity.ToString();
			}
			else
			{
				SeedCountList[k].text = "0";
			}
		}
	}

	public void OnSyncData(UserInfo.LinhDuocUserData linhDuocData)
	{
		LandState = STATE.OTHER;
		StealLinhDuocData = linhDuocData;
		foreach (GameObject item in SwitchList2)
		{
			item.SetActive(true);
		}
		foreach (GameObject @switch in SwitchList)
		{
			@switch.SetActive(false);
		}
		StealCount.text = Mathf.Min(5, GameManager.instance.m_GameClient.UserInfo.LinhDuocInfo.StealCount) + "/5";
		if (GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Exists((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_NGUYEN_KHI_DAN"))
		{
			NguyenKhiDanCount.text = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_NGUYEN_KHI_DAN").Quantity.ToString();
		}
		else
		{
			NguyenKhiDanCount.text = "0";
		}
		if (LinhDuocList != null)
		{
			for (int num = 0; num < LinhDuocList.Count; num++)
			{
				UnityEngine.Object.Destroy(LinhDuocList[num].gameObject);
			}
		}
		LinhDuocList = new List<GameObject>();
		for (int num2 = 0; num2 < 6; num2++)
		{
			UserInfo.LinhDuocLandData linhDuocLandData = linhDuocData.LinhDuocLandList[num2];
			int num3 = 0;
			TimeSpan timeSpan = GameManager.instance.m_GameClient.ServerTime - linhDuocLandData.StartGrowTime;
			num3 = ((timeSpan.TotalHours >= 4.0) ? 4 : ((timeSpan.TotalHours >= 3.0) ? 3 : ((!(timeSpan.TotalHours >= 1.0)) ? 1 : 2)));
			if (linhDuocLandData == null || linhDuocLandData.Quantity == 0)
			{
				InfoList[num2].SetActive(false);
				continue;
			}
			if (linhDuocLandData.Quantity % 5 != 0)
			{
				InfoList[num2].SetActive(true);
				InfoList[num2].transform.Find("Label").GetComponent<UILabel>().text = Localization.instance.Get("BiAnTrom");
				InfoList[num2].transform.Find("Label").GetComponent<LinhDuocCountDown>().StopCountDown();
			}
			else if (num3 == 4)
			{
				InfoList[num2].SetActive(false);
			}
			else
			{
				InfoList[num2].SetActive(true);
				InfoList[num2].transform.Find("Label").GetComponent<LinhDuocCountDown>().StartCountDown(linhDuocLandData.StartGrowTime + new TimeSpan(4, 0, 0) - GameManager.instance.m_GameClient.ServerTime);
			}
			GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(GetLinhDuocPrefab(linhDuocLandData.CurLinhDuoc.ToString(), num3));
			gameObject.transform.position = LinhDuocSlot[num2].transform.position;
			gameObject.transform.parent = child3Dscreen.transform;
			LinhDuocList.Add(gameObject);
		}
	}

	public GameObject GetLinhDuocPrefab(string Codename, int state)
	{
		return (GameObject)Resources.Load("Prefabs/TrongCay/" + Codename + state);
	}

	public void ShowLinhDuocActivity()
	{
		PopupLinhDuocActivity.Create();
	}
}

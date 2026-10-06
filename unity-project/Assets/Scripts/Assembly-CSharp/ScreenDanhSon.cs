using System.Collections.Generic;

public class ScreenDanhSon : ScreenBase
{
	public UIGrid gridDanhSon;

	private DanhSonItem[] itemsArray;

	private List<UserInfo.DanhSonData> listDs = new List<UserInfo.DanhSonData>();

	private int currDsIndex;

	private List<PhanThuongResponse.PhanThuong> listAllPhanThuong = new List<PhanThuongResponse.PhanThuong>();

	private DanhDanhSonResponse response;

	private int playScreen;

	private void Awake()
	{
		itemsArray = GetComponentsInChildren<DanhSonItem>();
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		SyncWithNetworkData();
	}

	public override void OnDeactive()
	{
		base.OnDeactive();
	}

	public void SyncWithNetworkData()
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		for (int i = 0; i < itemsArray.Length; i++)
		{
			int danhSonIdx = itemsArray[i].DanhSonIdx;
			if (danhSonIdx >= ConfigManager.instance.m_listDanhSon.Count)
			{
				itemsArray[i].gameObject.SetActive(false);
				continue;
			}
			int giangHoIdxFromDanhSonIdx = ConfigManager.instance.GetGiangHoIdxFromDanhSonIdx(danhSonIdx);
			if (userInfo.GiangHo.Count > giangHoIdxFromDanhSonIdx && userInfo.GiangHo[giangHoIdxFromDanhSonIdx].HoanThanh > 0)
			{
				itemsArray[i].gameObject.SetActive(true);
				DanhSonCfg danhSonCfg = ConfigManager.instance.m_listDanhSon[danhSonIdx];
				UserInfo.DanhSonData danhSonByIdx = userInfo.GetDanhSonByIdx(danhSonIdx);
				itemsArray[i].SetInfo(danhSonCfg.TenHienThi, danhSonByIdx != null && danhSonByIdx.VuotAi == 3, danhSonCfg.PhanThuong9);
			}
			else
			{
				itemsArray[i].gameObject.SetActive(false);
			}
		}
		gridDanhSon.Reposition();
	}

	public void StartBattleVuotAi(DanhDanhSonResponse response)
	{
		this.response = response;
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBattle);
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		int giangHoIdxFromDanhSonIdx = ConfigManager.instance.GetGiangHoIdxFromDanhSonIdx(response.DanhSonIdx);
		GiangHoCfg giangHoCfg = null;
		if (giangHoIdxFromDanhSonIdx < ConfigManager.instance.m_listGiangHo.Count)
		{
			giangHoCfg = ConfigManager.instance.m_listGiangHo[giangHoIdxFromDanhSonIdx];
		}
		string mapName = "BM_City";
		if (giangHoCfg != null)
		{
			mapName = giangHoCfg.Map;
		}
		if (response.Battle3 != null)
		{
			screenBattle.ReplayDanhSon(response.Battle3, 3, mapName);
			screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenDanhSon;
			screenBattle.OnFinishReplay += OnFinishBattle3;
			playScreen = 3;
			return;
		}
		MessagePopup.Create(Localization.instance.Get("DanhDanhSonThatBai"));
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.instance.gameObject.SetActive(true);
		}
		if (PopupDanhSon.instance != null)
		{
			PopupDanhSon.instance.gameObject.SetActive(true);
		}
	}

	public void StartBattle(DanhDanhSonResponse response)
	{
		this.response = response;
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBattle);
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		int giangHoIdxFromDanhSonIdx = ConfigManager.instance.GetGiangHoIdxFromDanhSonIdx(response.DanhSonIdx);
		GiangHoCfg giangHoCfg = null;
		if (giangHoIdxFromDanhSonIdx < ConfigManager.instance.m_listGiangHo.Count)
		{
			giangHoCfg = ConfigManager.instance.m_listGiangHo[giangHoIdxFromDanhSonIdx];
		}
		string mapName = "BM_City";
		if (giangHoCfg != null)
		{
			mapName = giangHoCfg.Map;
		}
		if (response.Battle1 != null)
		{
			screenBattle.ReplayDanhSon(response.Battle1, 1, mapName);
			if (response.Battle2 != null)
			{
				screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenBattle;
			}
			else
			{
				screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenDanhSon;
			}
			screenBattle.OnFinishReplay += OnFinishBattle1;
			playScreen = 1;
			return;
		}
		MessagePopup.Create(Localization.instance.Get("DanhDanhSonThatBai"));
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.instance.gameObject.SetActive(true);
		}
		if (PopupDanhSon.instance != null)
		{
			PopupDanhSon.instance.gameObject.SetActive(true);
		}
	}

	private void OnFinishBattle1()
	{
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		screenBattle.OnFinishReplay -= OnFinishBattle1;
		int giangHoIdxFromDanhSonIdx = ConfigManager.instance.GetGiangHoIdxFromDanhSonIdx(response.DanhSonIdx);
		GiangHoCfg giangHoCfg = null;
		if (giangHoIdxFromDanhSonIdx < ConfigManager.instance.m_listGiangHo.Count)
		{
			giangHoCfg = ConfigManager.instance.m_listGiangHo[giangHoIdxFromDanhSonIdx];
		}
		string mapName = "BM_City";
		if (giangHoCfg != null)
		{
			mapName = giangHoCfg.Map;
		}
		if (response.Battle2 != null)
		{
			screenBattle.ReplayDanhSon(response.Battle2, 2, mapName);
			if (response.Battle3 != null)
			{
				screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenBattle;
			}
			else
			{
				screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenDanhSon;
			}
			screenBattle.OnFinishReplay += OnFinishBattle2;
			return;
		}
		if (response.Battle1.Winner == 1)
		{
			MessagePopup.Create(Localization.instance.Get("DanhDanhSonThanhCong"));
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("DanhDanhSonThatBai"));
		}
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.instance.gameObject.SetActive(true);
		}
		if (PopupDanhSon.instance != null)
		{
			PopupDanhSon.instance.gameObject.SetActive(true);
		}
	}

	private void OnFinishBattle2()
	{
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		screenBattle.OnFinishReplay -= OnFinishBattle2;
		int giangHoIdxFromDanhSonIdx = ConfigManager.instance.GetGiangHoIdxFromDanhSonIdx(response.DanhSonIdx);
		GiangHoCfg giangHoCfg = null;
		if (giangHoIdxFromDanhSonIdx < ConfigManager.instance.m_listGiangHo.Count)
		{
			giangHoCfg = ConfigManager.instance.m_listGiangHo[giangHoIdxFromDanhSonIdx];
		}
		string mapName = "BM_City";
		if (giangHoCfg != null)
		{
			mapName = giangHoCfg.Map;
		}
		if (response.Battle3 != null)
		{
			screenBattle.ReplayDanhSon(response.Battle3, 3, mapName);
			screenBattle.screenBackAfterBattle = GAME_SCREEN.ScreenDanhSon;
			screenBattle.OnFinishReplay += OnFinishBattle3;
			return;
		}
		if (response.Battle2.Winner == 1)
		{
			MessagePopup.Create(Localization.instance.Get("DanhDanhSonThanhCong"));
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("DanhDanhSonThatBai"));
		}
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.instance.gameObject.SetActive(true);
		}
		if (PopupDanhSon.instance != null)
		{
			PopupDanhSon.instance.gameObject.SetActive(true);
		}
	}

	public void OnFinishBattle3()
	{
		ScreenBattle screenBattle = GUIManager.getScreen(GAME_SCREEN.ScreenBattle) as ScreenBattle;
		screenBattle.OnFinishReplay -= OnFinishBattle3;
		if (response.Battle3.Winner == 1)
		{
			MessagePopup.Create(Localization.instance.Get("DanhDanhSonThanhCong"));
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("DanhDanhSonThatBai"));
		}
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.instance.gameObject.SetActive(true);
		}
		if (PopupDanhSon.instance != null)
		{
			PopupDanhSon.instance.gameObject.SetActive(true);
		}
	}

	public void AutoBtn_OnClick()
	{
		listDs.Clear();
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		for (int i = 0; i < itemsArray.Length; i++)
		{
			int danhSonIdx = itemsArray[i].DanhSonIdx;
			if (danhSonIdx >= ConfigManager.instance.m_listDanhSon.Count)
			{
				continue;
			}
			int giangHoIdxFromDanhSonIdx = ConfigManager.instance.GetGiangHoIdxFromDanhSonIdx(danhSonIdx);
			if (userInfo.GiangHo.Count <= giangHoIdxFromDanhSonIdx || userInfo.GiangHo[giangHoIdxFromDanhSonIdx].HoanThanh <= 0)
			{
				continue;
			}
			DanhSonCfg danhSonCfg = ConfigManager.instance.m_listDanhSon[danhSonIdx];
			UserInfo.DanhSonData danhSonByIdx = userInfo.GetDanhSonByIdx(danhSonIdx);
			if (danhSonByIdx == null || (danhSonByIdx != null && danhSonByIdx.VuotAi == 0))
			{
				UserInfo.GiangHoData giangHoByIdx = userInfo.GetGiangHoByIdx(giangHoIdxFromDanhSonIdx);
				if (giangHoByIdx != null && giangHoByIdx.HoanThanh > 0)
				{
					danhSonByIdx = new UserInfo.DanhSonData();
					danhSonByIdx.DanhSonIdx = danhSonIdx;
					listDs.Add(danhSonByIdx);
				}
			}
		}
		if (listDs.Count > 0)
		{
			startAutoCamDia();
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("HoanThanhAuToCamDia"));
		}
	}

	public void startAutoCamDia()
	{
		currDsIndex = listDs.Count - 1;
		if (currDsIndex >= 0 && listDs[currDsIndex] != null)
		{
			GUIManager.instance.isAutoCamDia = true;
			listAllPhanThuong.Clear();
			GameManager.instance.m_GameClient.RequestDanhDanhSon(listDs[currDsIndex].DanhSonIdx, 0);
		}
	}

	public void continueAutoCamDia()
	{
		currDsIndex--;
		if (currDsIndex < listDs.Count && currDsIndex >= 0)
		{
			if (listDs[currDsIndex] != null)
			{
				GameManager.instance.m_GameClient.RequestDanhDanhSon(listDs[currDsIndex].DanhSonIdx, 0);
			}
		}
		else
		{
			endAutoCamDia();
		}
	}

	public void addPhanThuongToList(List<PhanThuongResponse.PhanThuong> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			listAllPhanThuong.Add(list[i]);
		}
	}

	public void endAutoCamDia()
	{
		GUIManager.instance.isAutoCamDia = false;
		currDsIndex = -1;
		if (listAllPhanThuong.Count > 0)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = listAllPhanThuong;
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongTitle"), Localization.instance.Get("PhanThuongNhanDuoc"), phanThuongResponse);
		}
	}
}

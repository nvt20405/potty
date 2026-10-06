using System.Collections.Generic;
using Nettention.Proud;
using UnityEngine;

public class ScreenLuanKiem : ScreenBase
{
	public GameObject panelLuanKiem;

	public GameObject panelTop10;

	public GameObject panelDoiThuong;

	public UIPanel listLuanKiem;

	public UIPanel listDoiThuong;

	public UIPanel listTop10;

	public UILabel luanKiemDescLabel;

	public UILabel luotLuanKiemLabel;

	public UILabel thuHangLabel;

	public UILabel diemThuongLabel;

	private LuanKiemResponse lkResponse;

	private void Awake()
	{
		if (GUIManager.instance == null)
		{
			Object.Destroy(base.gameObject);
		}
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].widgetContainer = GUIManager.instance.GameFrame;
		}
	}

	public override void OnActive()
	{
		base.OnActive();
		GUIManager.ShowGadgets(6);
		if (GUIManager.instance.CurrentScreen != GAME_SCREEN.ScreenLuanKiem && GUIManager.instance.CurrentScreen != GAME_SCREEN.ScreenBattle)
		{
			GameManager.instance.m_GameClient.GetLuanKiemInfo();
		}
	}

	public void OnCloseBattleResult()
	{
		if (PopupDanhSachPhanThuong.instance != null)
		{
			PopupDanhSachPhanThuong.instance.gameObject.SetActive(true);
		}
	}

	public void SyncWithUserInfo(UserInfo.LuanKiemData lkData, int maxLuotKhieuChien)
	{
		thuHangLabel.text = string.Format(Localization.instance.Get("ThuHangLuanKiemLabel"), lkData.Hang);
		int num = Mathf.Max(0, maxLuotKhieuChien - lkData.LuotLuanKiem);
		luotLuanKiemLabel.text = string.Format(Localization.instance.Get("LuotLuanKiemLabel"), num, maxLuotKhieuChien);
		diemThuongLabel.text = string.Format(Localization.instance.Get("DiemLuanKiemLabel"), lkData.DiemTichLuy);
	}

	public void SyncWithNetworkData(LuanKiemResponse response)
	{
		lkResponse = response;
		luanKiemDescLabel.text = response.TichDiemDescription;
		SyncWithUserInfo(response.UserInfoResponse.LuanKiem, response.SoLuotKhieuChien);
		LoadLK(response.TopLuanKiem);
		LoadDoiThuong(response.ListDoiThuong);
	}

	public void SyncDoiThuongInfo(LuanKiemDoiThuongResponse response)
	{
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		diemThuongLabel.text = string.Format(Localization.instance.Get("DiemLuanKiemLabel"), userInfo.LuanKiem.DiemTichLuy);
		LoadDoiThuong(response.List);
	}

	private void LoadLK(List<LuanKiemResponse.MonPhaiLKInfo> list)
	{
		UnityEngine.Vector3 vector = default(UnityEngine.Vector3);
		vector = new UnityEngine.Vector3(0f, 300f, 0f);
		UnityEngine.Vector3 vector2 = default(UnityEngine.Vector3);
		vector2 = new UnityEngine.Vector3(0f, 160f, 0f);
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		foreach (Transform item in listLuanKiem.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		for (int i = 0; i < list.Count; i++)
		{
			Object obj = Object.Instantiate(Resources.Load("gui/screenluankiem/ListLuanKiemItem"));
			GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
			gameObject.transform.parent = listLuanKiem.transform;
			gameObject.transform.localScale = UnityEngine.Vector3.one;
			gameObject.transform.localPosition = vector - i * vector2;
			LuanKiemResponse.MonPhaiLKInfo monPhaiLKInfo = list[i];
			LuanKiemListItem.FeatureButton featureButton = LuanKiemListItem.FeatureButton.None;
			featureButton = ((monPhaiLKInfo.GID != userInfo.Gamer.ID) ? (monPhaiLKInfo.EnableKhieuChien ? LuanKiemListItem.FeatureButton.Danh : LuanKiemListItem.FeatureButton.DoiHinh) : LuanKiemListItem.FeatureButton.LamMoi);
			gameObject.GetComponent<LuanKiemListItem>().SetInfo(monPhaiLKInfo.Name, monPhaiLKInfo.NhanVats[0], monPhaiLKInfo.Level, monPhaiLKInfo.Hang, monPhaiLKInfo.Desc, monPhaiLKInfo.GID, monPhaiLKInfo.BotID, featureButton);
		}
	}

	private void LoadTop10(List<LuanKiemResponse.MonPhaiLKInfo> list)
	{
		UnityEngine.Vector3 vector = default(UnityEngine.Vector3);
		vector = new UnityEngine.Vector3(0f, 300f, 0f);
		UnityEngine.Vector3 vector2 = default(UnityEngine.Vector3);
		vector2 = new UnityEngine.Vector3(0f, 160f, 0f);
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		foreach (Transform item in listTop10.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		for (int i = 0; i < list.Count; i++)
		{
			Object obj = Object.Instantiate(Resources.Load("gui/screenluankiem/ListLuanKiemItem"));
			GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
			gameObject.transform.parent = listTop10.transform;
			gameObject.transform.localScale = UnityEngine.Vector3.one;
			gameObject.transform.localPosition = vector - i * vector2;
			LuanKiemResponse.MonPhaiLKInfo monPhaiLKInfo = list[i];
			LuanKiemListItem.FeatureButton buttonshown = LuanKiemListItem.FeatureButton.DoiHinh;
			if (monPhaiLKInfo.BotID > 0)
			{
				buttonshown = LuanKiemListItem.FeatureButton.None;
			}
			gameObject.GetComponent<LuanKiemListItem>().SetInfo(monPhaiLKInfo.Name, monPhaiLKInfo.NhanVats[0], monPhaiLKInfo.Level, monPhaiLKInfo.Hang, monPhaiLKInfo.Desc, monPhaiLKInfo.GID, monPhaiLKInfo.BotID, buttonshown);
		}
	}

	private void LoadDoiThuong(List<LuanKiemResponse.DoiThuongItem> list)
	{
		UnityEngine.Vector3 vector = default(UnityEngine.Vector3);
		vector = new UnityEngine.Vector3(0f, 300f, 0f);
		UnityEngine.Vector3 vector2 = default(UnityEngine.Vector3);
		vector2 = new UnityEngine.Vector3(0f, 160f, 0f);
		UserInfo userInfo = GameManager.instance.m_GameClient.UserInfo;
		foreach (Transform item in listDoiThuong.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		for (int i = 0; i < list.Count; i++)
		{
			Object obj = Object.Instantiate(Resources.Load("gui/screenluankiem/ListDoiThuongItem"));
			GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
			gameObject.transform.parent = listDoiThuong.transform;
			gameObject.transform.localScale = UnityEngine.Vector3.one;
			gameObject.transform.localPosition = vector - i * vector2;
			LuanKiemResponse.DoiThuongItem doiThuongItem = list[i];
			gameObject.GetComponent<LKListDoiThuongItem>().SetItem(doiThuongItem.AvatarCodeName, doiThuongItem.RewardDesc, doiThuongItem.MoTaDesc, doiThuongItem.IsLinhThuong, doiThuongItem.CodeDoiThuong, doiThuongItem.IsActive);
		}
	}

	private void OnUpdateDiemClick()
	{
		GameManager.instance.m_GameClient.C2SProxy.RequestCapNhatDiemLuanKiem(HostID.Server, RmiContext.ReliableSend, string.Empty);
	}

	private void OnActivateDoiThuong(bool isActive)
	{
		panelDoiThuong.SetActive(isActive);
	}

	private void OnActivateLuanKiem(bool isActive)
	{
		panelLuanKiem.SetActive(isActive);
	}

	private void OnActivateTop10(bool isActive)
	{
		panelTop10.SetActive(isActive);
	}
}

using System.Collections.Generic;
using UnityEngine;

public class ScreenDoiHinh : ScreenBase
{
	public UISprite Background;

	public GameObject XemThongTinGroup;

	public UILabel TenMonPhaiLabel;

	private List<NhanVatDoiHinhItem> ListAvatar = new List<NhanVatDoiHinhItem>();

	public NhanVatAvatar[] ListHoTroAvatar;

	public NhanVatAvatar[] ListThienCangAvatar;

	public OtherAvatar[] ListTrangBiHoTro;

	public NhanVatFullInfo NhanVatInfo;

	public CostumeGroup CostumeGrp;

	public GameObject ThienMaGrp;

	public GameObject DetuGroup;

	public GameObject BatQuaiGroup;

	public GameObject ThanhTuuGrp;

	public GameObject ThuCuoiGrp;

	public GameObject ThanThuGrp;

	public GameObject ThienCangGroup;

	public GameObject groupNhanVatSupportAva;

	public UILabel TongKhiTheLabel;

	public UILabel menhHoTroLabel;

	public UILabel ngoaiHoTroLabel;

	public UILabel thanHoTroLabel;

	public UILabel khiHoTroLabel;

	public GameObject ItemRoot;

	public GameObject NhanVat3D;

	public GameObject NhanVatHoTro3D_1;

	public GameObject NhanVatHoTro3D_2;

	public GameObject NhanVatHoTro3D_3;

	public GameObject NhanVatHoTro3D_4;

	public GameObject NhanVatHoTro3D_5;

	public GameObject NhanVatHoTro3D_6;

	public GameObject NhanVatHoTro3D_7;

	public GameObject NhanVatHoTro3D_8;

	public GameObject NhanVatHoTro3D_9;

	public GameObject NhanVatHoTro3D_10;

	public GameObject NhanVatHoTro3D_11;

	public GameObject NhanVatHoTro3D_12;

	public GameObject NhanVatHoTro3D_13;

	public GameObject NhanVatHoTro3D_14;

	public GameObject nhanVatPerfab;

	public GameObject btnTranDoPerfab;

	public Object btnThanhTuuPrefab;

	public Object btnThuCuoiPrefab;

	public Object btnThanThuPrefab;

	public Object btnThienCangPrefab;

	private UIDraggablePanel dragPanel;

	private UIPanel panel;

	private UserInfo RefUserInfo;

	private List<int> list_bat_quai_id = new List<int>();

	private List<int> list_thien_cang_id = new List<int>();

	public int m_iCurrentSelectIdx;

	private int m_iChangeDoiHinhIdx;

	private int m_iCurrentTrangBiSelectedIdx;

	private string titlePopUp = string.Empty;

	private int m_CurrentNVBatQuaiSupportID;

	private int m_NVBatQuaiSupportSelectedIdx;

	private int m_CurrentNVThienCangSupportID;

	private int m_NVThienCangSupportSelectedIdx;

	public GameObject nhanVatAvatar3D;

	public GameObject mAnimBatQuai;

	public GameObject mAnimThienCang;

	public GameObject centerNVInfoGrp;

	public GameObject chanKhiNVGroup;

	public bool isDisplayChanKhi;

	public bool isShowAnotherUserInfo;

	public GameObject nhanvat3D_HoTro1;

	public GameObject nhanvat3D_HoTro2;

	public GameObject nhanvat3D_HoTro3;

	public GameObject nhanvat3D_HoTro4;

	public GameObject nhanvat3D_HoTro5;

	public GameObject nhanvat3D_HoTro6;

	public GameObject nhanvat3D_HoTro7;

	public GameObject nhanvat3D_HoTro8;

	public GameObject nhanvat3D_HoTro9;

	public GameObject nhanvat3D_HoTro10;

	public GameObject nhanvat3D_HoTro11;

	public GameObject nhanvat3D_HoTro12;

	public GameObject nhanvat3D_ThienCang1;

	public GameObject nhanvat3D_ThienCang2;

	public GameObject nhanvat3D_ThienCang3;

	public GameObject nhanvat3D_ThienCang4;

	public GameObject nhanvat3D_ThienCang5;

	public GameObject nhanvat3D_ThienCang6;

	public GameObject nhanvat3D_ThienCang7;

	public GameObject nhanvat3D_ThienCang8;

	public GameObject nhanvat3D_ThienCang9;

	public GameObject nhanvat3D_ThienCang10;

	public GameObject nhanvat3D_ThienCang11;

	public GameObject nhanvat3D_ThienCang12;

	public GameObject nhanvat3D_ThienCang13;

	public GameObject nhanvat3D_ThienCang14;

	public UISprite btnChanKhi;

	public UISprite btnBaoKhi;

	public UILabel[] listChiSoHoTroLabel;

	private bool m_xemDoiHinhBackMain;

	private bool isDisplayThienMa;

	private void Awake()
	{
		UIAnchor[] componentsInChildren = GetComponentsInChildren<UIAnchor>(true);
		UIAnchor[] array = componentsInChildren;
		UIAnchor[] array2 = array;
		foreach (UIAnchor uIAnchor in array2)
		{
			if (GUIManager.instance != null)
			{
				uIAnchor.widgetContainer = GUIManager.instance.GameFrame;
			}
		}
	}

	public bool IsReadOnlyMode()
	{
		if (RefUserInfo == null || RefUserInfo.Gamer.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.ID)
		{
			return false;
		}
		return true;
	}

	private void Start()
	{
		dragPanel = ItemRoot.GetComponent<UIDraggablePanel>();
		panel = ItemRoot.GetComponent<UIPanel>();
	}

	public override void OnActive()
	{
		base.OnActive();
		EGDebug.Log("GUIManager.instance.LastScreen: " + GUIManager.instance.LastScreen);
		SyncWithNetworkData();
		m_iChangeDoiHinhIdx = m_iCurrentSelectIdx;
	}

	public override void OnDeactive()
	{
		RefUserInfo = null;
		isShowAnotherUserInfo = false;
		isDisplayChanKhi = false;
		isDisplayThienMa = false;
		base.OnDeactive();
	}

	public void ShowAnotherUserInfo(UserInfo info, bool xemDoiHinhBackMain = false)
	{
		m_xemDoiHinhBackMain = xemDoiHinhBackMain;
		RefUserInfo = info;
		if (PopupBattleResult.instance != null)
		{
			NGUITools.SetActive(PopupBattleResult.instance.gameObject, false);
		}
		if (PopupTopHuyetChien.instance != null)
		{
			PopupTopHuyetChien.instance.gameObject.SetActive(false);
		}
		if (PopupTopDongNhan.instance != null)
		{
			PopupTopDongNhan.instance.gameObject.SetActive(false);
		}
		m_iCurrentSelectIdx = 0;
		isShowAnotherUserInfo = true;
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDoiHinh);
	}

	public void SyncWithNetworkData()
	{
		if (RefUserInfo == null)
		{
			RefUserInfo = GameManager.instance.m_GameClient.UserInfo;
		}
		foreach (Transform item in ItemRoot.transform)
		{
			Transform transform2 = item;
			Object.Destroy(transform2.gameObject);
		}
		if (IsReadOnlyMode())
		{
			NGUITools.SetActive(XemThongTinGroup, true);
			GUIManager.ShowGadgets(0);
			TenMonPhaiLabel.text = RefUserInfo.Gamer.DisplayName;
		}
		else
		{
			NGUITools.SetActive(XemThongTinGroup, false);
		}
		ListAvatar.Clear();
		List<int> doi_hinh_list_id = RefUserInfo.DoiHinh.ListRaTran;
		Vector3 vector = default(Vector3);
		vector = new Vector3(-200f, 0f, 0f);
		Vector3 vector2 = default(Vector3);
		vector2 = new Vector3(110f, 0f, 0f);
		int i;
		for (i = 0; i < doi_hinh_list_id.Count; i++)
		{
			NhanVatDoiHinhItem component = ((GameObject)Object.Instantiate(nhanVatPerfab)).GetComponent<NhanVatDoiHinhItem>();
			UIEventListener.Get(component.gameObject).onClick = OnDoiHinhClick;
			component.transform.parent = ItemRoot.transform;
			component.transform.localScale = new Vector3(1f, 1f, 1f);
			component.transform.localPosition = vector;
			component.name = "NhanVatDoiHinhItem" + i;
			vector += vector2;
			component.itemID = i;
			if (doi_hinh_list_id[i] == -1)
			{
				component.nhanVatAvatar.Set("lock");
				break;
			}
			if (doi_hinh_list_id[i] == 0)
			{
				component.nhanVatAvatar.Set("empty");
			}
			else
			{
				UserInfo.HeroData heroData = RefUserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == doi_hinh_list_id[i]);
				if (heroData != null)
				{
					component.nhanVatAvatar.Set(heroData.Name, heroData.CapDotPha, -1, false, -1, false, heroData.CostumeID > 0, heroData.ChuyenSinh);
				}
			}
			ListAvatar.Add(component);
		}
		UIButton component2 = ((GameObject)Object.Instantiate(btnTranDoPerfab)).GetComponent<UIButton>();
		component2.transform.parent = ItemRoot.transform;
		component2.transform.localScale = new Vector3(1f, 1f, 1f);
		component2.transform.localPosition = vector;
		UIEventListener.Get(component2.gameObject).onClick = btnTranDo_OnClick;
		if (RefUserInfo.DoiHinh.ListHoTro != null && RefUserInfo.DoiHinh.ListHoTro.Count > 0)
		{
			bool flag = true;
			for (int num = 0; num < RefUserInfo.DoiHinh.ListHoTro.Count; num++)
			{
				if (RefUserInfo.DoiHinh.ListHoTro[num] == -1)
				{
					flag = false;
				}
			}
			if (flag)
			{
				vector += vector2;
				UIButton component3 = ((GameObject)Object.Instantiate(btnThienCangPrefab)).GetComponent<UIButton>();
				component3.transform.parent = ItemRoot.transform;
				component3.transform.localScale = new Vector3(1f, 1f, 1f);
				component3.transform.localPosition = vector;
				UIEventListener.Get(component3.gameObject).onClick = btnThienCang_OnClick;
			}
		}
		vector += vector2;
		GameObject gameObject = (GameObject)Object.Instantiate(btnThanhTuuPrefab);
		gameObject.transform.parent = ItemRoot.transform;
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.localPosition = vector;
		UIEventListener.Get(gameObject).onClick = btnThanhTuu_OnClick;
		if (!GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains("ThuCuoi;"))
		{
			vector += vector2;
			GameObject gameObject2 = (GameObject)Object.Instantiate(btnThuCuoiPrefab);
			gameObject2.transform.parent = ItemRoot.transform;
			gameObject2.transform.localScale = Vector3.one;
			gameObject2.transform.localPosition = vector;
			UIEventListener.Get(gameObject2).onClick = btnThuCuoi_OnClick;
		}
		if (!GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains("ThanThu;"))
		{
			vector += vector2;
			GameObject gameObject3 = (GameObject)Object.Instantiate(btnThanThuPrefab);
			gameObject3.transform.parent = ItemRoot.transform;
			gameObject3.transform.localScale = Vector3.one;
			gameObject3.transform.localPosition = vector;
			UIEventListener.Get(gameObject3).onClick = btnThanThu_OnClick;
		}
		UIDraggablePanel component4 = ItemRoot.GetComponent<UIDraggablePanel>();
		component4.ResetPosition();
		TurnOnNhanVatGroup();
	}

	public void SetNhanVatInfo(bool display3D = true)
	{
		List<int> listRaTran = RefUserInfo.DoiHinh.ListRaTran;
		int current_hero_id = listRaTran[m_iCurrentSelectIdx];
		UserInfo.HeroData heroData = RefUserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == current_hero_id);
		if (heroData != null)
		{
			if (RefUserInfo != null)
			{
				NhanVatInfo.SetForScreenDoiHinh(heroData, RefUserInfo);
			}
			if (display3D)
			{
				displayNhanVat3D(heroData);
			}
		}
	}

	public void OnTranHinhClick()
	{
		PopupTranHinh.Create(IsReadOnlyMode(), RefUserInfo);
	}

	public void OnDoiHinhClick(GameObject go)
	{
		NhanVatDoiHinhItem component = go.GetComponent<NhanVatDoiHinhItem>();
		if (component != null)
		{
			OnDoiHinhClick(component.itemID);
		}
	}

	public bool OnChangeDoiHinh(int select_hero)
	{
		EGDebug.Log("m_iChangeDoiHinhIdx: " + m_iChangeDoiHinhIdx);
		if (m_iChangeDoiHinhIdx >= 0)
		{
			m_iCurrentSelectIdx = m_iChangeDoiHinhIdx;
			GameManager.instance.m_GameClient.RequestSetDoiHinh(m_iChangeDoiHinhIdx, select_hero);
		}
		return true;
	}

	public void OnDeTuClick()
	{
		List<int> listRaTran = RefUserInfo.DoiHinh.ListRaTran;
		int current_hero_id = listRaTran[m_iCurrentSelectIdx];
		UserInfo.HeroData data = RefUserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == current_hero_id);
		if (IsReadOnlyMode())
		{
			PopupNhanVat.CreateByNormalScreens(RefUserInfo, data, true);
		}
		else
		{
			PopupNhanVat.CreateByScreenDoiHinh(RefUserInfo, data);
		}
	}

	public void OnDoiHinhClick(int doi_hinh_idx)
	{
		List<int> listRaTran = RefUserInfo.DoiHinh.ListRaTran;
		int num = listRaTran[doi_hinh_idx];
		TurnOnNhanVatGroup();
		switch (num)
		{
		case -1:
			if (!IsReadOnlyMode())
			{
				MessagePopup.Create(string.Format(Localization.instance.Get("UnlockDoihinhLvl"), ConfigManager.instance.GetNextLevelUnLockDoiHinhSlot(RefUserInfo.Gamer.Level)));
			}
			return;
		case 0:
			if (!IsReadOnlyMode())
			{
				m_iChangeDoiHinhIdx = doi_hinh_idx;
				List<int> ignoreList = getIgnoreList();
				PopupSelectNhanVat.Create(OnChangeDoiHinh, ignoreList);
			}
			return;
		}
		m_iChangeDoiHinhIdx = doi_hinh_idx;
		if (m_iCurrentSelectIdx != doi_hinh_idx)
		{
			m_iCurrentSelectIdx = doi_hinh_idx;
			SetNhanVatInfo();
		}
		else if (NhanVat3D == null)
		{
			SetNhanVatInfo();
		}
		else
		{
			NhanVat3D.GetComponent<Avatar3D>().PlayAnimBattle("idle", true);
		}
	}

	public void OnCloseXemThongTin()
	{
		if (PopupBattleResult.instance != null)
		{
			NGUITools.SetActive(PopupBattleResult.instance.gameObject, true);
		}
		if (PopupTopDongNhan.instance != null)
		{
			PopupTopDongNhan.instance.gameObject.SetActive(true);
		}
		if (PopupTopHuyetChien.instance != null)
		{
			PopupTopHuyetChien.instance.gameObject.SetActive(true);
		}
		GUIManager.ShowGadgets(6);
		if (!m_xemDoiHinhBackMain)
		{
			GUIManager.GoBackLastScreen();
		}
		else
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenMain);
		}
	}

	public void btnThuCuoi_OnClick(GameObject go)
	{
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		if (IsReadOnlyMode())
		{
			if (RefUserInfo.ThuCuoi == null || RefUserInfo.ThuCuoi.ThuCuoiList == null || RefUserInfo.ThuCuoi.ThuCuoiList.Count == 0 || !RefUserInfo.ThuCuoi.ThuCuoiList.Exists((UserInfo.ThuCuoiData e) => e.ExpiredTime > GameManager.instance.m_GameClient.ServerTime || !e.isActive) || RefUserInfo.Gamer.curThuCuoi <= 0 || !RefUserInfo.ThuCuoi.ThuCuoiList.Exists((UserInfo.ThuCuoiData e) => e.ID == RefUserInfo.Gamer.curThuCuoi))
			{
				MessagePopup.Create(Localization.instance.Get("HetThuCuoi"));
				return;
			}
			TurnOnThuCuoiGroup();
			ThuCuoiGrp.GetComponent<ThuCuoiInfo>().Create(RefUserInfo.ThuCuoi.ThuCuoiList.Find((UserInfo.ThuCuoiData e) => e.ID == RefUserInfo.Gamer.curThuCuoi).CodeName);
		}
		else if (GameManager.instance.m_GameClient.UserInfo.ThuCuoi == null || GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList == null || GameManager.instance.m_GameClient.UserInfo.ThuCuoi.ThuCuoiList.Count == 0)
		{
			MessagePopup.Create(Localization.instance.Get("HetThuCuoi"));
			ThaoNguaRequest request = new ThaoNguaRequest();
			GameManager.instance.m_GameClient.RequestThaoNgua(request);
		}
		else
		{
			TurnOnThuCuoiGroup();
			ThuCuoiGrp.GetComponent<ThuCuoiInfo>().Create();
		}
	}

	public void btnThanThu_OnClick(GameObject go)
	{
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		if (IsReadOnlyMode())
		{
			if (RefUserInfo.ListThanThu == null || RefUserInfo.ListThanThu.Count == 0 || RefUserInfo.Gamer.curThanThu <= 0 || !RefUserInfo.ListThanThu.Exists((UserInfo.PetInfo e) => e.ID == RefUserInfo.Gamer.curThanThu))
			{
				MessagePopup.Create(Localization.instance.Get("HetThanThu"));
				return;
			}
			TurnOnThanThuGroup();
			ThanThuGrp.GetComponent<ThanThuInfo>().Set(RefUserInfo.ListThanThu.Find((UserInfo.PetInfo e) => e.ID == RefUserInfo.Gamer.curThanThu), false);
			return;
		}
		if (GameManager.instance.m_GameClient.UserInfo.ListThanThu == null || GameManager.instance.m_GameClient.UserInfo.ListThanThu.Count == 0)
		{
			MessagePopup.Create(Localization.instance.Get("HetThanThu"));
			return;
		}
		TurnOnThanThuGroup();
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.curThanThu > 0)
		{
			ThanThuGrp.GetComponent<ThanThuInfo>().Set(GameManager.instance.m_GameClient.UserInfo.ListThanThu.Find((UserInfo.PetInfo e) => e.ID == GameManager.instance.m_GameClient.UserInfo.Gamer.curThanThu), true);
		}
		else
		{
			GameManager.instance.m_GameClient.RequestDoiThanThu(GameManager.instance.m_GameClient.UserInfo.ListThanThu[0].ID);
			ThanThuGrp.GetComponent<ThanThuInfo>().Set(GameManager.instance.m_GameClient.UserInfo.ListThanThu[0], true);
		}
	}

	public void btnThanhTuu_OnClick(GameObject go)
	{
		TurnOnThanhTuuGroup();
		UserInfo.DanhHieuData danhHieuData = null;
		if (RefUserInfo != null && RefUserInfo.DanhHieu != null)
		{
			danhHieuData = RefUserInfo.DanhHieu;
		}
		int khiTheByDanhHieuAtTime = ConfigManager.GetKhiTheByDanhHieuAtTime(danhHieuData, GameManager.instance.m_GameClient.ServerTime);
		TongKhiTheLabel.text = string.Format(Localization.instance.Get("TongKhiTheMsg"), khiTheByDanhHieuAtTime);
		ThanhTuuInfo component = ThanhTuuGrp.GetComponent<ThanhTuuInfo>();
		component.SyncWithNetworkData(danhHieuData);
		component.SetGiangHoHaoKiet(component.GiangHoHaoKiet);
	}

	public void btnThienCang_OnClick(GameObject go)
	{
		TurnOnThienCangGroup();
		getListThienCangHoTro();
	}

	public void btnTranDo_OnClick(GameObject go)
	{
		displayBatQuaiTranDo();
	}

	public void displayBatQuaiTranDo()
	{
		TurnOnBatQuaiGroup();
		getListBatQuaiHoTro();
	}

	public void startPlayAnimBatQuai()
	{
		mAnimBatQuai.SetActive(true);
		mAnimBatQuai.GetComponent<ParticleSystem>().Play();
	}

	public void startPlayAnimThienCang()
	{
		mAnimThienCang.SetActive(true);
		mAnimThienCang.GetComponent<ParticleSystem>().Play();
	}

	public void getListBatQuaiHoTro()
	{
		list_bat_quai_id = RefUserInfo.DoiHinh.ListHoTro;
		clearNhanVatBatQuai3D();
		List<int> list = ConfigManager.instance.MapBatQuaiCuongHoa2TranDo(RefUserInfo.DoiHinh);
		int i;
		for (i = 0; i < list_bat_quai_id.Count; i++)
		{
			if (list_bat_quai_id[i] == -1)
			{
				ListHoTroAvatar[i].Set("lock");
				listChiSoHoTroLabel[i].text = string.Empty;
				continue;
			}
			if (list_bat_quai_id[i] == 0)
			{
				ListHoTroAvatar[i].Set("empty");
				listChiSoHoTroLabel[i].text = string.Empty;
				continue;
			}
			UserInfo.HeroData heroData = RefUserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == list_bat_quai_id[i]);
			EGDebug.Log("NHAN VAT: " + heroData.Name + " - index: " + i);
			ListHoTroAvatar[i].Set(heroData.Name, heroData.CapDotPha, -1, false, -1, false, heroData.CostumeID > 0, heroData.ChuyenSinh);
			listChiSoHoTroLabel[i].text = list[i] + "%";
			displayNhanVatBatQuai3D(heroData, i, RefUserInfo.DoiHinh);
		}
		displayInfoBatQuai();
	}

	public void getListThienCangHoTro()
	{
		list_thien_cang_id = RefUserInfo.DoiHinh.ListThienCang;
		clearNhanVatThienCang3D();
		int i;
		for (i = 0; i < list_thien_cang_id.Count; i++)
		{
			if (list_thien_cang_id[i] == -1)
			{
				ListThienCangAvatar[i].Set("lock");
				continue;
			}
			if (list_thien_cang_id[i] == 0)
			{
				ListThienCangAvatar[i].Set("empty");
				continue;
			}
			UserInfo.HeroData heroData = RefUserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == list_thien_cang_id[i]);
			ListThienCangAvatar[i].Set(heroData.Name, heroData.CapDotPha, -1, false, -1, false, heroData.CostumeID > 0, heroData.ChuyenSinh);
			displayNhanVatThienCang3D(heroData, i, RefUserInfo.DoiHinh);
		}
	}

	public void clearNhanVatBatQuai3D()
	{
		if (nhanvat3D_HoTro1 != null)
		{
			foreach (Transform item in nhanvat3D_HoTro1.transform)
			{
				Transform transform2 = item;
				Object.Destroy(transform2.gameObject);
			}
		}
		if (nhanvat3D_HoTro2 != null)
		{
			foreach (Transform item2 in nhanvat3D_HoTro2.transform)
			{
				Transform transform4 = item2;
				Object.Destroy(transform4.gameObject);
			}
		}
		if (nhanvat3D_HoTro3 != null)
		{
			foreach (Transform item3 in nhanvat3D_HoTro3.transform)
			{
				Transform transform6 = item3;
				Object.Destroy(transform6.gameObject);
			}
		}
		if (nhanvat3D_HoTro4 != null)
		{
			foreach (Transform item4 in nhanvat3D_HoTro4.transform)
			{
				Transform transform8 = item4;
				Object.Destroy(transform8.gameObject);
			}
		}
		if (nhanvat3D_HoTro5 != null)
		{
			foreach (Transform item5 in nhanvat3D_HoTro5.transform)
			{
				Transform transform10 = item5;
				Object.Destroy(transform10.gameObject);
			}
		}
		if (nhanvat3D_HoTro6 != null)
		{
			foreach (Transform item6 in nhanvat3D_HoTro6.transform)
			{
				Transform transform12 = item6;
				Object.Destroy(transform12.gameObject);
			}
		}
		if (nhanvat3D_HoTro7 != null)
		{
			foreach (Transform item7 in nhanvat3D_HoTro7.transform)
			{
				Transform transform14 = item7;
				Object.Destroy(transform14.gameObject);
			}
		}
		if (nhanvat3D_HoTro8 != null)
		{
			foreach (Transform item8 in nhanvat3D_HoTro8.transform)
			{
				Transform transform16 = item8;
				Object.Destroy(transform16.gameObject);
			}
		}
		if (nhanvat3D_HoTro9 != null)
		{
			foreach (Transform item9 in nhanvat3D_HoTro9.transform)
			{
				Transform transform18 = item9;
				Object.Destroy(transform18.gameObject);
			}
		}
		if (nhanvat3D_HoTro10 != null)
		{
			foreach (Transform item10 in nhanvat3D_HoTro10.transform)
			{
				Transform transform20 = item10;
				Object.Destroy(transform20.gameObject);
			}
		}
		if (nhanvat3D_HoTro11 != null)
		{
			foreach (Transform item11 in nhanvat3D_HoTro11.transform)
			{
				Transform transform22 = item11;
				Object.Destroy(transform22.gameObject);
			}
		}
		if (!(nhanvat3D_HoTro12 != null))
		{
			return;
		}
		foreach (Transform item12 in nhanvat3D_HoTro12.transform)
		{
			Transform transform24 = item12;
			Object.Destroy(transform24.gameObject);
		}
	}

	public void clearNhanVatThienCang3D()
	{
		if (nhanvat3D_ThienCang1 != null)
		{
			foreach (Transform item in nhanvat3D_ThienCang1.transform)
			{
				Transform transform2 = item;
				Object.Destroy(transform2.gameObject);
			}
		}
		if (nhanvat3D_ThienCang2 != null)
		{
			foreach (Transform item2 in nhanvat3D_ThienCang2.transform)
			{
				Transform transform4 = item2;
				Object.Destroy(transform4.gameObject);
			}
		}
		if (nhanvat3D_ThienCang3 != null)
		{
			foreach (Transform item3 in nhanvat3D_ThienCang3.transform)
			{
				Transform transform6 = item3;
				Object.Destroy(transform6.gameObject);
			}
		}
		if (nhanvat3D_ThienCang4 != null)
		{
			foreach (Transform item4 in nhanvat3D_ThienCang4.transform)
			{
				Transform transform8 = item4;
				Object.Destroy(transform8.gameObject);
			}
		}
		if (nhanvat3D_ThienCang5 != null)
		{
			foreach (Transform item5 in nhanvat3D_ThienCang5.transform)
			{
				Transform transform10 = item5;
				Object.Destroy(transform10.gameObject);
			}
		}
		if (nhanvat3D_ThienCang6 != null)
		{
			foreach (Transform item6 in nhanvat3D_ThienCang6.transform)
			{
				Transform transform12 = item6;
				Object.Destroy(transform12.gameObject);
			}
		}
		if (nhanvat3D_ThienCang7 != null)
		{
			foreach (Transform item7 in nhanvat3D_ThienCang7.transform)
			{
				Transform transform14 = item7;
				Object.Destroy(transform14.gameObject);
			}
		}
		if (nhanvat3D_ThienCang8 != null)
		{
			foreach (Transform item8 in nhanvat3D_ThienCang8.transform)
			{
				Transform transform16 = item8;
				Object.Destroy(transform16.gameObject);
			}
		}
		if (nhanvat3D_ThienCang9 != null)
		{
			foreach (Transform item9 in nhanvat3D_ThienCang9.transform)
			{
				Transform transform18 = item9;
				Object.Destroy(transform18.gameObject);
			}
		}
		if (nhanvat3D_ThienCang10 != null)
		{
			foreach (Transform item10 in nhanvat3D_ThienCang10.transform)
			{
				Transform transform20 = item10;
				Object.Destroy(transform20.gameObject);
			}
		}
		if (nhanvat3D_ThienCang11 != null)
		{
			foreach (Transform item11 in nhanvat3D_ThienCang11.transform)
			{
				Transform transform22 = item11;
				Object.Destroy(transform22.gameObject);
			}
		}
		if (nhanvat3D_ThienCang12 != null)
		{
			foreach (Transform item12 in nhanvat3D_ThienCang12.transform)
			{
				Transform transform24 = item12;
				Object.Destroy(transform24.gameObject);
			}
		}
		if (nhanvat3D_ThienCang13 != null)
		{
			foreach (Transform item13 in nhanvat3D_ThienCang13.transform)
			{
				Transform transform26 = item13;
				Object.Destroy(transform26.gameObject);
			}
		}
		if (!(nhanvat3D_ThienCang14 != null))
		{
			return;
		}
		foreach (Transform item14 in nhanvat3D_ThienCang14.transform)
		{
			Transform transform28 = item14;
			Object.Destroy(transform28.gameObject);
		}
	}

	public void displayNhanVatThienCang3D(UserInfo.HeroData heroData, int indexHoTro, UserInfo.DoiHinhData doiHinhData)
	{
		if (heroData == null)
		{
			return;
		}
		GameObject gameObject = null;
		GameObject gameObject2 = null;
		switch (indexHoTro)
		{
		case 0:
			gameObject = NhanVatHoTro3D_1;
			gameObject2 = nhanvat3D_ThienCang1;
			break;
		case 1:
			gameObject = NhanVatHoTro3D_2;
			gameObject2 = nhanvat3D_ThienCang2;
			break;
		case 2:
			gameObject = NhanVatHoTro3D_3;
			gameObject2 = nhanvat3D_ThienCang3;
			break;
		case 3:
			gameObject = NhanVatHoTro3D_4;
			gameObject2 = nhanvat3D_ThienCang4;
			break;
		case 4:
			gameObject = NhanVatHoTro3D_5;
			gameObject2 = nhanvat3D_ThienCang5;
			break;
		case 5:
			gameObject = NhanVatHoTro3D_6;
			gameObject2 = nhanvat3D_ThienCang6;
			break;
		case 6:
			gameObject = NhanVatHoTro3D_7;
			gameObject2 = nhanvat3D_ThienCang7;
			break;
		case 7:
			gameObject = NhanVatHoTro3D_8;
			gameObject2 = nhanvat3D_ThienCang8;
			break;
		case 8:
			gameObject = NhanVatHoTro3D_9;
			gameObject2 = nhanvat3D_ThienCang9;
			break;
		case 9:
			gameObject = NhanVatHoTro3D_10;
			gameObject2 = nhanvat3D_ThienCang10;
			break;
		case 10:
			gameObject = NhanVatHoTro3D_11;
			gameObject2 = nhanvat3D_ThienCang11;
			break;
		case 11:
			gameObject = NhanVatHoTro3D_12;
			gameObject2 = nhanvat3D_ThienCang12;
			break;
		case 12:
			gameObject = NhanVatHoTro3D_13;
			gameObject2 = nhanvat3D_ThienCang13;
			break;
		case 13:
			gameObject = NhanVatHoTro3D_14;
			gameObject2 = nhanvat3D_ThienCang14;
			break;
		}
		if (gameObject2 != null)
		{
			foreach (Transform item in gameObject2.transform)
			{
				Transform transform2 = item;
				Object.Destroy(transform2.gameObject);
			}
		}
		Avatar3D avatar3D = null;
		if (heroData.CostumeID > 0)
		{
			string costume = string.Empty;
			UserInfo.CostumeData costumeData = null;
			if (RefUserInfo.CostumeList != null)
			{
				costumeData = RefUserInfo.CostumeList.Find((UserInfo.CostumeData e) => e.ID == heroData.CostumeID);
			}
			if (costumeData != null)
			{
				costume = costumeData.CodeName;
			}
			avatar3D = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(heroData.Name, string.Empty, string.Empty, string.Empty, costume);
		}
		else
		{
			avatar3D = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(heroData.Name, string.Empty, string.Empty, string.Empty, string.Empty);
		}
		if (avatar3D != null)
		{
			avatar3D.transform.parent = gameObject2.transform;
			avatar3D.transform.localPosition = Vector3.zero;
			avatar3D.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			avatar3D.transform.localScale = Vector3.one;
			avatar3D.PlayAnimBattle("idle", true);
			gameObject = avatar3D.gameObject;
		}
		else
		{
			gameObject = null;
		}
	}

	public void displayNhanVatBatQuai3D(UserInfo.HeroData heroData, int indexHoTro, UserInfo.DoiHinhData doiHinhData)
	{
		if (heroData == null)
		{
			return;
		}
		GameObject gameObject = null;
		GameObject gameObject2 = null;
		switch (indexHoTro)
		{
		case 0:
			gameObject = NhanVatHoTro3D_1;
			gameObject2 = nhanvat3D_HoTro1;
			break;
		case 1:
			gameObject = NhanVatHoTro3D_2;
			gameObject2 = nhanvat3D_HoTro2;
			break;
		case 2:
			gameObject = NhanVatHoTro3D_3;
			gameObject2 = nhanvat3D_HoTro3;
			break;
		case 3:
			gameObject = NhanVatHoTro3D_4;
			gameObject2 = nhanvat3D_HoTro4;
			break;
		case 4:
			gameObject = NhanVatHoTro3D_5;
			gameObject2 = nhanvat3D_HoTro5;
			break;
		case 5:
			gameObject = NhanVatHoTro3D_6;
			gameObject2 = nhanvat3D_HoTro6;
			break;
		case 6:
			gameObject = NhanVatHoTro3D_7;
			gameObject2 = nhanvat3D_HoTro7;
			break;
		case 7:
			gameObject = NhanVatHoTro3D_8;
			gameObject2 = nhanvat3D_HoTro8;
			break;
		case 8:
			gameObject = NhanVatHoTro3D_9;
			gameObject2 = nhanvat3D_HoTro9;
			break;
		case 9:
			gameObject = NhanVatHoTro3D_10;
			gameObject2 = nhanvat3D_HoTro10;
			break;
		case 10:
			gameObject = NhanVatHoTro3D_11;
			gameObject2 = nhanvat3D_HoTro11;
			break;
		case 11:
			gameObject = NhanVatHoTro3D_12;
			gameObject2 = nhanvat3D_HoTro12;
			break;
		}
		if (gameObject2 != null)
		{
			foreach (Transform item in gameObject2.transform)
			{
				Transform transform2 = item;
				Object.Destroy(transform2.gameObject);
			}
		}
		Avatar3D avatar3D = null;
		if (heroData.CostumeID > 0)
		{
			string costume = string.Empty;
			UserInfo.CostumeData costumeData = null;
			if (RefUserInfo.CostumeList != null)
			{
				costumeData = RefUserInfo.CostumeList.Find((UserInfo.CostumeData e) => e.ID == heroData.CostumeID);
			}
			if (costumeData != null)
			{
				costume = costumeData.CodeName;
			}
			avatar3D = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(heroData.Name, string.Empty, string.Empty, string.Empty, costume);
		}
		else
		{
			avatar3D = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(heroData.Name, string.Empty, string.Empty, string.Empty, string.Empty);
		}
		if (avatar3D != null)
		{
			avatar3D.transform.parent = gameObject2.transform;
			avatar3D.transform.localPosition = Vector3.zero;
			avatar3D.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			avatar3D.transform.localScale = Vector3.one;
			avatar3D.PlayAnimBattle("idle", true);
			gameObject = avatar3D.gameObject;
		}
		else
		{
			gameObject = null;
		}
		if (doiHinhData.BatQuaiTranType == OtherCfg.BatQuaiTranDoType.NONE)
		{
			return;
		}
		string soDoBQTCodeName = OtherCfg.GetSoDoBQTCodeName(doiHinhData.BatQuaiTranType);
		OtherCfg.BatQuaiCuongHoaCfg batQuaiCuongHoaCfg = ConfigManager.instance.OtherConfig.BatQuaiConfig[soDoBQTCodeName];
		if (batQuaiCuongHoaCfg != null && batQuaiCuongHoaCfg.ListSlot != null && batQuaiCuongHoaCfg.ListSlot.Count > 0)
		{
			for (int num = 0; num < batQuaiCuongHoaCfg.ListSlot.Count; num++)
			{
				setHoTroType(num, doiHinhData, batQuaiCuongHoaCfg);
			}
		}
	}

	public void setHoTroType(int index, UserInfo.DoiHinhData doihinhData, OtherCfg.BatQuaiCuongHoaCfg cfg)
	{
		if (doihinhData != null)
		{
			List<int> list = ConfigManager.instance.MapBatQuaiCuongHoa2TranDo(doihinhData);
			ChiSoCoBan batQuaiBuffLoai = ConfigManager.instance.GetBatQuaiBuffLoai(index + 1, doihinhData);
			listChiSoHoTroLabel[index].text = list[index] + "%";
		}
	}

	public void displayInfoBatQuai(bool isUpdateUserInfo = false)
	{
		if (isUpdateUserInfo)
		{
			RefUserInfo = GameManager.instance.m_GameClient.UserInfo;
		}
		BattleGamerInfo battleGamerData = BattleGamerInfo.GetBattleGamerData(RefUserInfo);
		ChiSoCoSo chiSoCoSo = BatQuaiTran.TranDoChiSoTongTangThem(battleGamerData);
		menhHoTroLabel.text = Mathf.RoundToInt(chiSoCoSo.Menh).ToString();
		ngoaiHoTroLabel.text = Mathf.RoundToInt(chiSoCoSo.Ngoai).ToString();
		thanHoTroLabel.text = Mathf.RoundToInt(chiSoCoSo.ThanPhap).ToString();
		khiHoTroLabel.text = Mathf.RoundToInt(chiSoCoSo.Noi).ToString();
	}

	public void ThienCangSupport1_OnClick()
	{
		OnClickThienCangSupport(0);
	}

	public void ThienCangSupport2_OnClick()
	{
		OnClickThienCangSupport(1);
	}

	public void ThienCangSupport3_OnClick()
	{
		OnClickThienCangSupport(2);
	}

	public void ThienCangSupport4_OnClick()
	{
		OnClickThienCangSupport(3);
	}

	public void ThienCangSupport5_OnClick()
	{
		OnClickThienCangSupport(4);
	}

	public void ThienCangSupport6_OnClick()
	{
		OnClickThienCangSupport(5);
	}

	public void ThienCangSupport7_OnClick()
	{
		OnClickThienCangSupport(6);
	}

	public void ThienCangSupport8_OnClick()
	{
		OnClickThienCangSupport(7);
	}

	public void ThienCangSupport9_OnClick()
	{
		OnClickThienCangSupport(8);
	}

	public void ThienCangSupport10_OnClick()
	{
		OnClickThienCangSupport(9);
	}

	public void ThienCangSupport11_OnClick()
	{
		OnClickThienCangSupport(10);
	}

	public void ThienCangSupport12_OnClick()
	{
		OnClickThienCangSupport(11);
	}

	public void ThienCangSupport13_OnClick()
	{
		OnClickThienCangSupport(12);
	}

	public void ThienCangSupport14_OnClick()
	{
		OnClickThienCangSupport(13);
	}

	public void OnClickThienCangSupport(int indexsSelected)
	{
		m_CurrentNVThienCangSupportID = list_thien_cang_id[indexsSelected];
		m_NVThienCangSupportSelectedIdx = indexsSelected;
		if (IsReadOnlyMode())
		{
			if (m_CurrentNVThienCangSupportID > 0)
			{
				UserInfo.HeroData heroData = RefUserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == m_CurrentNVThienCangSupportID);
				if (heroData != null)
				{
					PopupNhanVat.CreateToViewAnotherUser(RefUserInfo, heroData);
				}
			}
			return;
		}
		if (m_CurrentNVThienCangSupportID == -1)
		{
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData = RefUserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_BAT_QUAI_TRAN_DO");
			if (vatPhamTieuThuData != null && vatPhamTieuThuData.Quantity > 0)
			{
				ListThienCangAvatar[m_NVThienCangSupportSelectedIdx].IsSelected = true;
				OpenDoiHinhThienCangRequest openDoiHinhThienCangRequest = new OpenDoiHinhThienCangRequest();
				openDoiHinhThienCangRequest.Slot = m_NVThienCangSupportSelectedIdx;
				openDoiHinhThienCangRequest.VatPhamID = vatPhamTieuThuData.ID;
				GameManager.instance.m_GameClient.RequestOpenDoiHinhThienCangTran(openDoiHinhThienCangRequest);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoUnlockNVSupport"));
			}
			return;
		}
		if (m_CurrentNVThienCangSupportID > 0)
		{
			UserInfo.HeroData heroData2 = RefUserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == m_CurrentNVThienCangSupportID);
			if (heroData2 != null)
			{
				PopupNhanVat.CreateByScreenDoiHinh(RefUserInfo, heroData2);
			}
		}
		if (m_CurrentNVThienCangSupportID == 0)
		{
			List<int> ignoreList = getIgnoreList();
			PopupSelectNhanVat.Create(OnChange_ThienCangSupport, ignoreList);
		}
	}

	public bool OnChange_ThienCangSupport(int select_hero)
	{
		SetDoiHinhThienCangRequest setDoiHinhThienCangRequest = new SetDoiHinhThienCangRequest();
		setDoiHinhThienCangRequest.HeroID = select_hero;
		setDoiHinhThienCangRequest.Slot = m_NVThienCangSupportSelectedIdx;
		GameManager.instance.m_GameClient.RequestSetDoiHinhThienCangTran(setDoiHinhThienCangRequest);
		return true;
	}

	public void OnNVSupport1Click()
	{
		OnLoadNVSupport(0, ChiSoCoBan.Menh);
	}

	public void OnNVSupport2Click()
	{
		OnLoadNVSupport(1, ChiSoCoBan.Ngoai);
	}

	public void OnNVSupport3Click()
	{
		OnLoadNVSupport(2, ChiSoCoBan.ThanPhap);
	}

	public void OnNVSupport4Click()
	{
		OnLoadNVSupport(3, ChiSoCoBan.Noi);
	}

	public void OnNVSupport5Click()
	{
		OnLoadNVSupport(4, ChiSoCoBan.Menh);
	}

	public void OnNVSupport6Click()
	{
		OnLoadNVSupport(5, ChiSoCoBan.Ngoai);
	}

	public void OnNVSupport7Click()
	{
		OnLoadNVSupport(6, ChiSoCoBan.ThanPhap);
	}

	public void OnNVSupport8Click()
	{
		OnLoadNVSupport(7, ChiSoCoBan.Noi);
	}

	public void OnLoadNVSupport(int indexsSelected, ChiSoCoBan chiso)
	{
		m_CurrentNVBatQuaiSupportID = list_bat_quai_id[indexsSelected];
		m_NVBatQuaiSupportSelectedIdx = indexsSelected;
		if (IsReadOnlyMode())
		{
			if (m_CurrentNVBatQuaiSupportID > 0)
			{
				UserInfo.HeroData heroData = RefUserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == m_CurrentNVBatQuaiSupportID);
				if (heroData != null)
				{
					PopupNhanVat.CreateToViewAnotherUser(RefUserInfo, heroData, false, true, indexsSelected, chiso);
				}
			}
			return;
		}
		if (m_CurrentNVBatQuaiSupportID == -1)
		{
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData = RefUserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == "VP_BAT_QUAI_TRAN_DO");
			if (vatPhamTieuThuData != null && vatPhamTieuThuData.Quantity > 0)
			{
				ListHoTroAvatar[m_NVBatQuaiSupportSelectedIdx].IsSelected = true;
				OpenDoiHinhHoTroRequest openDoiHinhHoTroRequest = new OpenDoiHinhHoTroRequest();
				openDoiHinhHoTroRequest.Slot = m_NVBatQuaiSupportSelectedIdx;
				openDoiHinhHoTroRequest.VatPhamID = vatPhamTieuThuData.ID;
				GameManager.instance.m_GameClient.RequestOpenDoiHinhHoTro(openDoiHinhHoTroRequest);
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoUnlockNVSupport"));
			}
			return;
		}
		if (m_CurrentNVBatQuaiSupportID > 0)
		{
			UserInfo.HeroData heroData2 = RefUserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == m_CurrentNVBatQuaiSupportID);
			if (heroData2 != null)
			{
				PopupNhanVat.CreateInBatQuaiTranView(RefUserInfo, heroData2, indexsSelected, chiso);
			}
		}
		if (m_CurrentNVBatQuaiSupportID == 0)
		{
			List<int> ignoreList = getIgnoreList();
			PopupSelectNhanVat.Create(OnChangeDoiHinhSupport, ignoreList);
		}
	}

	public void displayNhanVat3D(UserInfo.HeroData data)
	{
		if (data == null)
		{
			return;
		}
		if (NhanVat3D != null)
		{
			Object.Destroy(NhanVat3D);
			NhanVat3D = null;
		}
		string empty = string.Empty;
		string empty2 = string.Empty;
		string empty3 = string.Empty;
		UserInfo refUserInfo = RefUserInfo;
		if (data.VuKhiID > 0)
		{
			UserInfo.TrangBiData trangBiData = refUserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == data.VuKhiID);
			empty = trangBiData.Name;
		}
		if (data.VoCong3ID > 0)
		{
			UserInfo.VoCongData voCongData = refUserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.ID == data.VoCong3ID);
			empty3 = voCongData.Name;
		}
		if (data.VoCong4ID > 0)
		{
			UserInfo.VoCongData voCongData2 = refUserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.ID == data.VoCong4ID);
			empty2 = voCongData2.Name;
		}
		string costume = string.Empty;
		if (data.CostumeID > 0)
		{
			UserInfo.CostumeData costumeData = null;
			if (refUserInfo.CostumeList != null)
			{
				costumeData = refUserInfo.CostumeList.Find((UserInfo.CostumeData e) => e.ID == data.CostumeID);
			}
			if (costumeData != null)
			{
				costume = costumeData.CodeName;
			}
		}
		Avatar3D avatar3D = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(data.Name, empty, empty3, empty2, costume);
		nhanVatAvatar3D.transform.localRotation = Quaternion.EulerAngles(0f, 2.53f, 0f);
		if (avatar3D != null)
		{
			nhanVatAvatar3D.SetActive(true);
			avatar3D.transform.parent = nhanVatAvatar3D.transform;
			avatar3D.transform.localPosition = Vector3.zero;
			avatar3D.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			avatar3D.PlayAnimBattle("idle", true);
			avatar3D.transform.localScale = Vector3.one;
			NhanVat3D = avatar3D.gameObject;
		}
		else
		{
			NhanVat3D = null;
		}
	}

	public void Mu_OnClick(GameObject go)
	{
		titlePopUp = Localization.instance.Get("ChonMuTitle");
		OnChangeTrangBi(0);
	}

	public void VuKhi_OnClick(GameObject go)
	{
		OnChangeTrangBi(1);
	}

	public void AoGiap_OnClick(GameObject go)
	{
		OnChangeTrangBi(2);
	}

	public void TrangSuc_OnClick(GameObject go)
	{
		OnChangeTrangBi(3);
	}

	public void OnChangeTrangBi(int trangBiIdx)
	{
		m_iCurrentTrangBiSelectedIdx = trangBiIdx;
		m_CurrentNVBatQuaiSupportID = list_bat_quai_id[m_NVBatQuaiSupportSelectedIdx];
		UserInfo.HeroData heroData = RefUserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == m_CurrentNVBatQuaiSupportID);
		if (heroData == null)
		{
			return;
		}
		List<int> tb_id = new List<int>();
		tb_id.Add(heroData.MuID);
		tb_id.Add(heroData.VuKhiID);
		tb_id.Add(heroData.AoGiapID);
		tb_id.Add(heroData.TrangSucID);
		UserInfo.TrangBiData trangBiData = RefUserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == tb_id[trangBiIdx]);
		if (trangBiData == null)
		{
			LoaiTrangBi loaiTrangBi = LoaiTrangBi.None;
			switch (trangBiIdx)
			{
			case 0:
				loaiTrangBi = LoaiTrangBi.Mu;
				break;
			case 1:
				loaiTrangBi = LoaiTrangBi.VuKhi;
				break;
			case 2:
				loaiTrangBi = LoaiTrangBi.AoGiap;
				break;
			case 3:
				loaiTrangBi = LoaiTrangBi.TrangSuc;
				break;
			}
			EGDebug.Log("titlePopUp: " + titlePopUp);
			PopupSelectTrangBi.Create(OnChangeSelectedTrangBi, tb_id, loaiTrangBi, titlePopUp);
		}
		else
		{
			PopupTrangBi.CreateByScreenDoiHinh(trangBiData);
		}
	}

	public bool OnChangeDoiHinhSupport(int select_hero)
	{
		SetDoiHinhHoTroRequest setDoiHinhHoTroRequest = new SetDoiHinhHoTroRequest();
		setDoiHinhHoTroRequest.HeroID = select_hero;
		setDoiHinhHoTroRequest.Slot = m_NVBatQuaiSupportSelectedIdx;
		GameManager.instance.m_GameClient.RequestSetDoiHinhHoTro(setDoiHinhHoTroRequest);
		return true;
	}

	public void updateNVSupport()
	{
		getListBatQuaiHoTro();
		SetNhanVatInfo(false);
	}

	public List<int> getIgnoreList()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < RefUserInfo.DoiHinh.ListRaTran.Count; i++)
		{
			list.Add(RefUserInfo.DoiHinh.ListRaTran[i]);
		}
		for (int j = 0; j < RefUserInfo.DoiHinh.ListHoTro.Count; j++)
		{
			list.Add(RefUserInfo.DoiHinh.ListHoTro[j]);
		}
		for (int k = 0; k < RefUserInfo.DoiHinh.ListThienCang.Count; k++)
		{
			list.Add(RefUserInfo.DoiHinh.ListThienCang[k]);
		}
		return list;
	}

	public bool OnChangeSelectedTrangBi(int id)
	{
		UserInfo.TrangBiData data = RefUserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == id);
		ListTrangBiHoTro[m_iCurrentTrangBiSelectedIdx].Set(data);
		UserInfo.HeroData heroData = RefUserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == m_CurrentNVBatQuaiSupportID);
		GameManager.instance.m_GameClient.RequestSetTrangBi(heroData.HID, id);
		return true;
	}

	public void btnHelp_OnClick()
	{
		if (!IsReadOnlyMode())
		{
			ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
			screenHelpInfo.setByLevel(0, 2);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
			EGDebug.Log("btnHelp_OnClick");
		}
	}

	public void btnChanKhi_OnClick()
	{
		if (isShowAnotherUserInfo)
		{
			if (RefUserInfo != null && RefUserInfo.Gamer.Level >= 28)
			{
				isDisplayChanKhi = !isDisplayChanKhi;
				centerNVInfoGrp.gameObject.SetActive(!isDisplayChanKhi);
				ThienMaGrp.gameObject.SetActive(false);
				chanKhiNVGroup.gameObject.SetActive(isDisplayChanKhi);
				if (isDisplayChanKhi)
				{
					btnChanKhi.spriteName = "trang_bi";
					btnBaoKhi.spriteName = "button_baokhi";
				}
				else
				{
					btnChanKhi.spriteName = "nguyen_khi_button";
				}
				btnChanKhi.MakePixelPerfect();
				isDisplayThienMa = false;
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoNguyenKhiUserKhacChuaDcDung"));
			}
		}
		else if (GameManager.instance.m_GameClient.UserInfo.Gamer.Level >= 28)
		{
			isDisplayChanKhi = !isDisplayChanKhi;
			centerNVInfoGrp.gameObject.SetActive(!isDisplayChanKhi);
			ThienMaGrp.gameObject.SetActive(false);
			chanKhiNVGroup.gameObject.SetActive(isDisplayChanKhi);
			if (isDisplayChanKhi)
			{
				btnChanKhi.spriteName = "trang_bi";
				btnBaoKhi.spriteName = "button_baokhi";
			}
			else
			{
				btnChanKhi.spriteName = "nguyen_khi_button";
			}
			btnChanKhi.MakePixelPerfect();
			isDisplayThienMa = false;
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ThongBaoNguyenKhiChuaMo"));
		}
	}

	public void btnThienMa_OnClick()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains("ThienMaThuongPhong"))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		isDisplayThienMa = !isDisplayThienMa;
		centerNVInfoGrp.gameObject.SetActive(!isDisplayThienMa);
		chanKhiNVGroup.gameObject.SetActive(false);
		ThienMaGrp.gameObject.SetActive(isDisplayThienMa);
		if (isDisplayThienMa)
		{
			btnChanKhi.spriteName = "nguyen_khi_button";
			btnBaoKhi.spriteName = "trang_bi";
		}
		else
		{
			btnBaoKhi.spriteName = "button_baokhi";
		}
		btnChanKhi.MakePixelPerfect();
		isDisplayChanKhi = false;
	}

	public void btnTrangBi_OnClick()
	{
		isDisplayChanKhi = !isDisplayChanKhi;
		centerNVInfoGrp.gameObject.SetActive(!isDisplayChanKhi);
		chanKhiNVGroup.gameObject.SetActive(isDisplayChanKhi);
	}

	public void onClick_CuongHoaBatQuaiTran()
	{
		UserInfo.DoiHinhData doiHinh = RefUserInfo.DoiHinh;
		if (doiHinh == null)
		{
			return;
		}
		if (doiHinh.ListHoTro != null && doiHinh.ListHoTro.Count > 0)
		{
			for (int i = 0; i < doiHinh.ListHoTro.Count; i++)
			{
				if (doiHinh.ListHoTro[i] == -1)
				{
					if (IsReadOnlyMode())
					{
						MessagePopup.Create(Localization.instance.Get("ThongBaoChuongMonKhacChuaCoLinhDao"));
					}
					else
					{
						MessagePopup.Create(Localization.instance.Get("ThongBaoViTriCuongHoaDangKhoa"));
					}
					return;
				}
			}
		}
		if (doiHinh.BatQuaiTranType == OtherCfg.BatQuaiTranDoType.NONE)
		{
			if (IsReadOnlyMode())
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoChuongMonKhacChuaChonLinhDao"));
			}
			else
			{
				PopupSelectSoDoBatQuai.Create(doiHinh.BatQuaiTranType);
			}
		}
		else if (IsReadOnlyMode())
		{
			PopupSoDoBatQuai.CreateToAnotherView(doiHinh.BatQuaiTranType, RefUserInfo);
		}
		else
		{
			PopupSoDoBatQuai.Create(doiHinh.BatQuaiTranType, RefUserInfo);
		}
	}

	public bool OnChangeSoDoBatQuai(OtherCfg.BatQuaiTranDoType soDoSelected)
	{
		if (soDoSelected != OtherCfg.BatQuaiTranDoType.NONE)
		{
			SetSoDoBatQuaiTranRequest setSoDoBatQuaiTranRequest = new SetSoDoBatQuaiTranRequest();
			setSoDoBatQuaiTranRequest.SoDoType = soDoSelected;
			GameManager.instance.m_GameClient.RequestSetSoDoBatQuaiTran(setSoDoBatQuaiTranRequest);
		}
		return true;
	}

	public void onClick_BatQuaiTran()
	{
		TurnOnBatQuaiGroup();
		getListBatQuaiHoTro();
	}

	public void btnMonPhaiInfoHelp_OnClick()
	{
		if (!IsReadOnlyMode())
		{
			ScreenHelpInfo screenHelpInfo = GUIManager.getScreen(GAME_SCREEN.ScreenHelpInfo) as ScreenHelpInfo;
			screenHelpInfo.setByLevel(0, 5);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenHelpInfo);
			EGDebug.Log("btnHelp_OnClick");
		}
	}

	public void updateSoDoBatQuaiTran()
	{
		RefUserInfo = GameManager.instance.m_GameClient.UserInfo;
		if (NGUITools.GetActive(BatQuaiGroup.gameObject))
		{
			getListBatQuaiHoTro();
		}
	}

	public void updateSoDoThienCangTran()
	{
		RefUserInfo = GameManager.instance.m_GameClient.UserInfo;
		if (NGUITools.GetActive(ThienCangGroup.gameObject))
		{
			getListThienCangHoTro();
		}
	}

	public void TurnOnNhanVatGroup()
	{
		DetuGroup.SetActive(true);
		BatQuaiGroup.SetActive(false);
		ThanhTuuGrp.SetActive(false);
		ThuCuoiGrp.SetActive(false);
		ThienCangGroup.SetActive(false);
		mAnimBatQuai.SetActive(false);
		mAnimThienCang.SetActive(false);
		groupNhanVatSupportAva.SetActive(false);
		CostumeGrp.gameObject.SetActive(false);
		ThanThuGrp.SetActive(false);
		SetNhanVatInfo();
		if (isDisplayThienMa)
		{
			ThienMaGrp.gameObject.SetActive(true);
		}
		else
		{
			ThienMaGrp.gameObject.SetActive(false);
		}
		if (isDisplayChanKhi)
		{
			chanKhiNVGroup.gameObject.SetActive(true);
		}
		else
		{
			chanKhiNVGroup.gameObject.SetActive(false);
		}
		if (!isDisplayChanKhi && !isDisplayThienMa)
		{
			centerNVInfoGrp.gameObject.SetActive(true);
			btnChanKhi.spriteName = "nguyen_khi_button";
			btnBaoKhi.spriteName = "button_baokhi";
		}
	}

	public UserInfo.HeroData GetCurrentHeroData()
	{
		if (RefUserInfo == null || RefUserInfo.DoiHinh == null || RefUserInfo.DoiHinh.ListRaTran == null || RefUserInfo.HeroList == null || RefUserInfo.DoiHinh.ListRaTran.Count <= m_iCurrentSelectIdx)
		{
			return null;
		}
		List<int> listRaTran = RefUserInfo.DoiHinh.ListRaTran;
		int current_hero_id = listRaTran[m_iCurrentSelectIdx];
		return RefUserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == current_hero_id);
	}

	public void TurnOnCostumeGroup()
	{
		ThienMaGrp.gameObject.SetActive(false);
		CostumeGrp.gameObject.SetActive(true);
		DetuGroup.SetActive(false);
		BatQuaiGroup.SetActive(false);
		ThanhTuuGrp.SetActive(false);
		ThuCuoiGrp.SetActive(false);
		ThienCangGroup.SetActive(false);
		mAnimBatQuai.SetActive(false);
		mAnimThienCang.SetActive(false);
		groupNhanVatSupportAva.SetActive(false);
		ThanThuGrp.SetActive(false);
		UserInfo.HeroData currentHeroData = GetCurrentHeroData();
		CostumeGrp.SetInfo(currentHeroData, RefUserInfo);
	}

	public void TurnOnBatQuaiGroup()
	{
		ThienMaGrp.gameObject.SetActive(false);
		CostumeGrp.gameObject.SetActive(false);
		ThanThuGrp.SetActive(false);
		DetuGroup.SetActive(false);
		BatQuaiGroup.SetActive(true);
		ThuCuoiGrp.SetActive(false);
		ThanhTuuGrp.SetActive(false);
		ThienCangGroup.SetActive(false);
		mAnimThienCang.SetActive(false);
		groupNhanVatSupportAva.SetActive(true);
		startPlayAnimBatQuai();
	}

	public void TurnOnThuCuoiGroup()
	{
		ThienMaGrp.gameObject.SetActive(false);
		DetuGroup.SetActive(false);
		BatQuaiGroup.SetActive(false);
		ThuCuoiGrp.SetActive(true);
		ThanhTuuGrp.SetActive(false);
		ThienCangGroup.SetActive(false);
		mAnimBatQuai.SetActive(false);
		mAnimThienCang.SetActive(false);
		groupNhanVatSupportAva.SetActive(false);
		CostumeGrp.gameObject.SetActive(false);
		ThanThuGrp.SetActive(false);
	}

	public void TurnOnThanThuGroup()
	{
		ThienMaGrp.gameObject.SetActive(false);
		DetuGroup.SetActive(false);
		BatQuaiGroup.SetActive(false);
		ThuCuoiGrp.SetActive(false);
		ThanhTuuGrp.SetActive(false);
		ThienCangGroup.SetActive(false);
		mAnimBatQuai.SetActive(false);
		mAnimThienCang.SetActive(false);
		groupNhanVatSupportAva.SetActive(false);
		CostumeGrp.gameObject.SetActive(false);
		ThanThuGrp.SetActive(true);
	}

	public void TurnOnThanhTuuGroup()
	{
		ThienMaGrp.gameObject.SetActive(false);
		DetuGroup.SetActive(false);
		BatQuaiGroup.SetActive(false);
		ThanhTuuGrp.SetActive(true);
		ThuCuoiGrp.SetActive(false);
		ThienCangGroup.SetActive(false);
		mAnimBatQuai.SetActive(false);
		mAnimThienCang.SetActive(false);
		groupNhanVatSupportAva.SetActive(false);
		CostumeGrp.gameObject.SetActive(false);
		ThanThuGrp.SetActive(false);
	}

	public void TurnOnThienCangGroup()
	{
		ThienMaGrp.gameObject.SetActive(false);
		DetuGroup.SetActive(false);
		BatQuaiGroup.SetActive(false);
		ThanhTuuGrp.SetActive(false);
		ThuCuoiGrp.SetActive(false);
		ThienCangGroup.SetActive(true);
		mAnimBatQuai.SetActive(false);
		CostumeGrp.gameObject.SetActive(false);
		ThanThuGrp.SetActive(false);
		groupNhanVatSupportAva.SetActive(true);
		startPlayAnimThienCang();
	}
}

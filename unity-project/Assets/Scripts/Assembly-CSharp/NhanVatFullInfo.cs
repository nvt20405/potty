using System.Collections.Generic;
using UnityEngine;

public class NhanVatFullInfo : MonoBehaviour
{
	private enum ChiSoGroup
	{
		ChiSoCoBan = 0,
		ChiSoBattle = 1
	}

	public UILabel NameLabel;

	public UILabel LevelLabel;

	public UILabel ExpLabel;

	public OtherAvatar Vocong1Avatar;

	public OtherAvatar Vocong2Avatar;

	public OtherAvatar Vocong3Avatar;

	public OtherAvatar Vocong4Avatar;

	public OtherAvatar VuKhiAvatar;

	public OtherAvatar AoGiapAvatar;

	public OtherAvatar MuAvatar;

	public OtherAvatar TrangSucAvatar;

	public UserInfo RefUserInfo;

	public int curSlotBaoKhiIdx = -1;

	public UILabel MenhLabel;

	public UILabel NgoaiLabel;

	public UILabel ThanLabel;

	public UILabel KhiLabel;

	public UILabel MauLabel;

	public UILabel CongLabel;

	public UILabel ThuLabel;

	public UILabel ManaLabel;

	public UILabel HoiMauLabel;

	public UILabel TocDoDanhLabel;

	public UILabel TocDoDiChuyenLabel;

	public UILabel HoiManaLabel;

	public UISlider expSlider;

	public List<GameObject> listDuyen;

	public List<UISprite> listIconDuyen;

	public UISprite iconLockDuyen6;

	public List<GameObject> listAnimDuyen;

	public UserInfo.HeroData m_HeroData;

	private string titlePopUp = string.Empty;

	public NguyenKhiAvatar nguyenKhiAva1;

	public NguyenKhiAvatar nguyenKhiAva2;

	public NguyenKhiAvatar nguyenKhiAva3;

	public NguyenKhiAvatar nguyenKhiAva4;

	public NguyenKhiAvatar nguyenKhiAva5;

	public NguyenKhiAvatar nguyenKhiAva6;

	public int currentNguyenKhiSlotSelected = -1;

	public OtherAvatar avaLeft1;

	public OtherAvatar avaLeft2;

	public OtherAvatar avaLeft3;

	public OtherAvatar avaRight1;

	public OtherAvatar avaRight2;

	public OtherAvatar avaRight3;

	public UILabel lbInfoLeft;

	public UILabel lbInfoRight;

	public UISprite spCurTonHieuBG;

	public UISprite spIconBall1;

	public UISprite spIconBall2;

	public UILabel lbCurTonHieu;

	public GameObject grpTonHieuTim;

	public GameObject grpTonHieuVang;

	public GameObject parTimLeft;

	public GameObject parTimRight;

	public GameObject parVangLeft;

	public GameObject parVangRight;

	private ChiSoGroup m_ChiSoGroup;

	private void Start()
	{
	}

	private void OnCostumeBtnClick()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains("Costume"))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
		screenDoiHinh.TurnOnCostumeGroup();
	}

	private void Update()
	{
	}

	public void SetForScreenDoiHinh(UserInfo.HeroData heroData, UserInfo _RefUserInfo)
	{
		if (heroData == null)
		{
			return;
		}
		ScreenVoLamPho.SetVoLamPhoStatus(heroData.Name, 1);
		RefUserInfo = _RefUserInfo;
		m_HeroData = heroData;
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[heroData.Name];
		NameLabel.text = nhanVatCfg.TenHienThi;
		LevelLabel.text = heroData.Level.ToString();
		ExpLabel.text = string.Format("{0}/{1}", heroData.Exp, heroData.MaxExp);
		expSlider.sliderValue = (float)heroData.Exp / (float)heroData.MaxExp;
		for (int i = 0; i < listDuyen.Count; i++)
		{
			listDuyen[i].gameObject.SetActive(false);
		}
		for (int j = 0; j < listAnimDuyen.Count; j++)
		{
			listAnimDuyen[j].SetActive(false);
		}
		iconLockDuyen6.gameObject.SetActive(false);
		List<NhanVatCfg.DuyenPhanCfg> duyenPhan = nhanVatCfg.DuyenPhan;
		BattleGamerInfo battleGamerData = BattleGamerInfo.GetBattleGamerData(RefUserInfo);
		if (duyenPhan != null && duyenPhan.Count > 0)
		{
			EGDebug.Log("HERODATA ID: " + heroData.HID);
			for (int k = 0; k < duyenPhan.Count; k++)
			{
				bool flag = BattleChiSoHero.CheckActiveDuyen(heroData.HID, battleGamerData, duyenPhan[k], k + 1);
				listDuyen[k].gameObject.SetActive(true);
				if (flag)
				{
					listIconDuyen[k].color = new Color(1f, 1f, 1f);
					if (listAnimDuyen[k] != null)
					{
						startPlayAnim(listAnimDuyen[k]);
					}
				}
				else
				{
					listIconDuyen[k].color = new Color(0.58f, 0.58f, 0.58f);
				}
				switch (duyenPhan[k].ChiSoDuyen)
				{
				case ChiSoDuyenPhan.Khi:
					listIconDuyen[k].spriteName = "duyen_khi";
					break;
				case ChiSoDuyenPhan.Menh:
					listIconDuyen[k].spriteName = "duyen_menh";
					break;
				case ChiSoDuyenPhan.Ngoai:
					listIconDuyen[k].spriteName = "duyen_ngoai";
					break;
				case ChiSoDuyenPhan.ThanPhap:
					listIconDuyen[k].spriteName = "duyen_than";
					break;
				case ChiSoDuyenPhan.DoDon:
					listIconDuyen[k].spriteName = "duyen_dodon";
					break;
				case ChiSoDuyenPhan.Ne:
					listIconDuyen[k].spriteName = "duyen_ne";
					break;
				case ChiSoDuyenPhan.Bao:
					listIconDuyen[k].spriteName = "duyen_bao";
					break;
				}
				if (k == 5 && heroData.BeQuan == 0)
				{
					iconLockDuyen6.gameObject.SetActive(true);
				}
			}
		}
		UserInfo.VoCongData voCongData = new UserInfo.VoCongData();
		voCongData.ID = 0;
		voCongData.Name = heroData.VoCong1Name.ToString();
		voCongData.Level = heroData.VoCong1Level;
		Vocong1Avatar.Set(voCongData);
		if (heroData.VoCong2ID > 0)
		{
			UserInfo.VoCongData data = RefUserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.ID == heroData.VoCong2ID);
			Vocong2Avatar.Set(data);
		}
		else
		{
			Vocong2Avatar.Set("bgr_vo_cong");
		}
		if (heroData.VoCong3ID > 0)
		{
			UserInfo.VoCongData data2 = RefUserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.ID == heroData.VoCong3ID);
			Vocong3Avatar.Set(data2);
		}
		else
		{
			Vocong3Avatar.Set("bgr_bo_phap");
		}
		if (heroData.VoCong4ID > 0)
		{
			UserInfo.VoCongData data3 = RefUserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.ID == heroData.VoCong4ID);
			Vocong4Avatar.Set(data3);
		}
		else
		{
			Vocong4Avatar.Set("bgr_noicong");
		}
		if (heroData.VuKhiID > 0)
		{
			UserInfo.TrangBiData ngocData = RefUserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == heroData.VuKhiID);
			VuKhiAvatar.setNgocData(ngocData);
		}
		else
		{
			VuKhiAvatar.setTrangBi("bgr_vu_khi", 0, -1, string.Empty, 0, string.Empty, 0, string.Empty);
		}
		if (heroData.MuID > 0)
		{
			UserInfo.TrangBiData ngocData2 = RefUserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == heroData.MuID);
			MuAvatar.setNgocData(ngocData2);
		}
		else
		{
			MuAvatar.setTrangBi("bgr_mu", 0, -1, string.Empty, 0, string.Empty, 0, string.Empty);
		}
		if (heroData.AoGiapID > 0)
		{
			UserInfo.TrangBiData ngocData3 = RefUserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == heroData.AoGiapID);
			AoGiapAvatar.setNgocData(ngocData3);
		}
		else
		{
			AoGiapAvatar.setTrangBi("bgr_ao", 0, -1, string.Empty, 0, string.Empty, 0, string.Empty);
		}
		if (heroData.TrangSucID > 0)
		{
			UserInfo.TrangBiData ngocData4 = RefUserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == heroData.TrangSucID);
			TrangSucAvatar.setNgocData(ngocData4);
		}
		else
		{
			TrangSucAvatar.setTrangBi("bgr_trang_suc", 0, -1, string.Empty, 0, string.Empty, 0, string.Empty);
		}
		ChiSoNhanVat chiSoRaTran = BattleChiSoHero.GetChiSoRaTran(heroData.HID, battleGamerData);
		MenhLabel.text = Mathf.RoundToInt(chiSoRaTran.Menh).ToString();
		NgoaiLabel.text = Mathf.RoundToInt(chiSoRaTran.Ngoai).ToString();
		ThanLabel.text = Mathf.RoundToInt(chiSoRaTran.ThanPhap).ToString();
		KhiLabel.text = Mathf.RoundToInt(chiSoRaTran.Noi).ToString();
		MauLabel.text = Mathf.RoundToInt(chiSoRaTran.HP).ToString();
		CongLabel.text = Mathf.RoundToInt(chiSoRaTran.Cong).ToString();
		ThuLabel.text = Mathf.RoundToInt(chiSoRaTran.Thu).ToString();
		ManaLabel.text = Mathf.RoundToInt(chiSoRaTran.MP).ToString();
		HoiMauLabel.text = chiSoRaTran.RegHp.ToString("0.00");
		TocDoDanhLabel.text = chiSoRaTran.AS().ToString("0.00");
		TocDoDiChuyenLabel.text = chiSoRaTran.MS.ToString("0.00");
		HoiManaLabel.text = chiSoRaTran.RegMp.ToString("0.00");
		if (heroData.NguyenKhi1ID > 0)
		{
			UserInfo.NguyenKhiData nkData = RefUserInfo.NguyenKhiList.Find((UserInfo.NguyenKhiData e) => e.ID == heroData.NguyenKhi1ID);
			nguyenKhiAva1.Set(nkData);
		}
		else
		{
			if (heroData.NguyenKhi1ID == -1)
			{
				nguyenKhiAva1.Lock(true);
			}
			if (heroData.NguyenKhi1ID == 0)
			{
				nguyenKhiAva1.Lock(false);
			}
		}
		if (heroData.NguyenKhi2ID > 0)
		{
			UserInfo.NguyenKhiData nkData2 = RefUserInfo.NguyenKhiList.Find((UserInfo.NguyenKhiData e) => e.ID == heroData.NguyenKhi2ID);
			nguyenKhiAva2.Set(nkData2);
		}
		else
		{
			if (heroData.NguyenKhi2ID == -1)
			{
				nguyenKhiAva2.Lock(true);
			}
			if (heroData.NguyenKhi2ID == 0)
			{
				nguyenKhiAva2.Lock(false);
			}
		}
		if (heroData.NguyenKhi3ID > 0)
		{
			UserInfo.NguyenKhiData nkData3 = RefUserInfo.NguyenKhiList.Find((UserInfo.NguyenKhiData e) => e.ID == heroData.NguyenKhi3ID);
			nguyenKhiAva3.Set(nkData3);
		}
		else
		{
			if (heroData.NguyenKhi3ID == -1)
			{
				nguyenKhiAva3.Lock(true);
			}
			if (heroData.NguyenKhi3ID == 0)
			{
				nguyenKhiAva3.Lock(false);
			}
		}
		if (heroData.NguyenKhi4ID > 0)
		{
			UserInfo.NguyenKhiData nkData4 = RefUserInfo.NguyenKhiList.Find((UserInfo.NguyenKhiData e) => e.ID == heroData.NguyenKhi4ID);
			nguyenKhiAva4.Set(nkData4);
		}
		else
		{
			if (heroData.NguyenKhi4ID == -1)
			{
				nguyenKhiAva4.Lock(true);
			}
			if (heroData.NguyenKhi4ID == 0)
			{
				nguyenKhiAva4.Lock(false);
			}
		}
		if (heroData.NguyenKhi5ID > 0)
		{
			UserInfo.NguyenKhiData nkData5 = RefUserInfo.NguyenKhiList.Find((UserInfo.NguyenKhiData e) => e.ID == heroData.NguyenKhi5ID);
			nguyenKhiAva5.Set(nkData5);
		}
		else
		{
			if (heroData.NguyenKhi5ID == -1)
			{
				nguyenKhiAva5.Lock(true);
			}
			if (heroData.NguyenKhi5ID == 0)
			{
				nguyenKhiAva5.Lock(false);
			}
		}
		if (heroData.NguyenKhi6ID > 0)
		{
			UserInfo.NguyenKhiData nkData6 = RefUserInfo.NguyenKhiList.Find((UserInfo.NguyenKhiData e) => e.ID == heroData.NguyenKhi6ID);
			nguyenKhiAva6.Set(nkData6);
		}
		else
		{
			if (heroData.NguyenKhi6ID == -1)
			{
				nguyenKhiAva6.Lock(true);
			}
			if (heroData.NguyenKhi6ID == 0)
			{
				nguyenKhiAva6.Lock(false);
			}
		}
		avaLeft1.Set("plus");
		avaLeft2.Set("lock");
		avaLeft3.Set("lock");
		avaRight1.Set("plus");
		avaRight2.Set("lock");
		avaRight3.Set("lock");
		lbInfoLeft.text = string.Empty;
		lbInfoRight.text = string.Empty;
		if (RefUserInfo.ListThienMaLenh != null)
		{
			if (heroData.ThienMaLenhID1 > 0)
			{
				UserInfo.ThienMaLenhInfo thienMaLenhInfo = RefUserInfo.ListThienMaLenh.Find((UserInfo.ThienMaLenhInfo e) => e.ID == heroData.ThienMaLenhID1);
				if (thienMaLenhInfo != null)
				{
					avaLeft1.SetBaoKhiAvatar(thienMaLenhInfo.Slot1);
					avaLeft2.SetBaoKhiAvatar(thienMaLenhInfo.Slot2);
					avaLeft3.SetBaoKhiAvatar(thienMaLenhInfo.Slot3);
					lbInfoLeft.text = thienMaLenhInfo.GetCurrentValue(RefUserInfo, heroData.HID) + 100f + "%";
				}
			}
			if (heroData.ThienMaLenhID2 > 0)
			{
				UserInfo.ThienMaLenhInfo thienMaLenhInfo2 = RefUserInfo.ListThienMaLenh.Find((UserInfo.ThienMaLenhInfo e) => e.ID == heroData.ThienMaLenhID2);
				if (thienMaLenhInfo2 != null)
				{
					avaRight1.SetBaoKhiAvatar(thienMaLenhInfo2.Slot1);
					avaRight2.SetBaoKhiAvatar(thienMaLenhInfo2.Slot2);
					avaRight3.SetBaoKhiAvatar(thienMaLenhInfo2.Slot3);
					lbInfoRight.text = thienMaLenhInfo2.GetCurrentValue(RefUserInfo, heroData.HID) + 100f + "%";
				}
			}
		}
		loadTonHieuGamer();
	}

	public void OnVuKhiClick()
	{
		if (TutorialPopup.instance != null)
		{
			TutorialPopup.instance.ShowNextTutorial();
		}
		titlePopUp = Localization.instance.Get("ChonVuKhiTitle");
		OnTrangBiClick(0);
	}

	public void OnAoGiapClick()
	{
		titlePopUp = Localization.instance.Get("ChonAoGiapTitle");
		OnTrangBiClick(2);
	}

	public void OnMuClick()
	{
		titlePopUp = Localization.instance.Get("ChonMuTitle");
		OnTrangBiClick(1);
	}

	public void OnTrangSucClick()
	{
		titlePopUp = Localization.instance.Get("ChonTrangSucTitle");
		OnTrangBiClick(3);
	}

	public void OnTrangBiClick(int idx)
	{
		List<int> tb_id = new List<int>();
		tb_id.Add(m_HeroData.VuKhiID);
		tb_id.Add(m_HeroData.MuID);
		tb_id.Add(m_HeroData.AoGiapID);
		tb_id.Add(m_HeroData.TrangSucID);
		UserInfo.TrangBiData trangBiData = RefUserInfo.TrangBiList.Find((UserInfo.TrangBiData e) => e.ID == tb_id[idx]);
		if (trangBiData == null)
		{
			if (!IsReadOnlyMode())
			{
				LoaiTrangBi loaiTrangBi = LoaiTrangBi.None;
				switch (idx)
				{
				case 0:
					loaiTrangBi = LoaiTrangBi.VuKhi;
					break;
				case 1:
					loaiTrangBi = LoaiTrangBi.Mu;
					break;
				case 2:
					loaiTrangBi = LoaiTrangBi.AoGiap;
					break;
				case 3:
					loaiTrangBi = LoaiTrangBi.TrangSuc;
					break;
				}
				PopupSelectTrangBi.Create(OnChangeTrangBi, tb_id, loaiTrangBi, titlePopUp);
			}
		}
		else if (IsReadOnlyMode())
		{
			PopupTrangBi.CreateByNormalScreen(trangBiData, true);
		}
		else
		{
			PopupTrangBi.CreateByScreenDoiHinh(trangBiData);
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

	public void OnVoCong1Click()
	{
		OnVoCongClick(1);
	}

	public void OnVoCong2Click()
	{
		OnVoCongClick(2);
	}

	public void OnVoCong3Click()
	{
		OnVoCongClick(3);
	}

	public void OnVoCong4Click()
	{
		OnVoCongClick(4);
	}

	public void OnVoCongClick(int idx)
	{
		if (idx == 1)
		{
			UserInfo.VoCongData voCongData = new UserInfo.VoCongData();
			voCongData.ID = 0;
			voCongData.Name = m_HeroData.VoCong1Name.ToString();
			voCongData.Level = m_HeroData.VoCong1Level;
			voCongData.HID = m_HeroData.HID;
			voCongData.Unlock = m_HeroData.UnlockVCDefault;
			voCongData.ThamNgoExp = m_HeroData.VoCong1ThamNgoExp;
			if (IsReadOnlyMode())
			{
				PopupVoCong.CreateByNormalScreen(RefUserInfo, voCongData, voCongData.Level);
			}
			else
			{
				PopupVoCong.CreateByScreenDoiHinh(RefUserInfo, voCongData, true);
			}
			return;
		}
		int[] vc_id = new int[5];
		vc_id[2] = m_HeroData.VoCong2ID;
		vc_id[3] = m_HeroData.VoCong3ID;
		vc_id[4] = m_HeroData.VoCong4ID;
		UserInfo.VoCongData voCongData2 = RefUserInfo.VoCongList.Find((UserInfo.VoCongData e) => e.ID == vc_id[idx]);
		if (voCongData2 != null)
		{
			if (IsReadOnlyMode())
			{
				PopupVoCong.CreateByNormalScreen(RefUserInfo, voCongData2, voCongData2.Level);
			}
			else
			{
				PopupVoCong.CreateByScreenDoiHinh(RefUserInfo, voCongData2);
			}
		}
		else if (!IsReadOnlyMode())
		{
			VCClass voCongClass = VCClass.CHIEU_THUC;
			string title = Localization.instance.Get("ChonChieuThucTitle");
			if (idx == 3)
			{
				voCongClass = VCClass.BO_PHAP;
				title = Localization.instance.Get("ChonBoPhapTitle");
			}
			else if (idx == 4)
			{
				voCongClass = VCClass.NOI_CONG;
				title = Localization.instance.Get("ChonNoiCongTitle");
			}
			PopupSelectVoCong.Create(OnChangeVoCong, null, voCongClass, false, title);
		}
	}

	public void onNguyenKhi1Click()
	{
		if (m_HeroData.NguyenKhi1ID != -1)
		{
			onNguyenKhiClick(0);
		}
	}

	public void onNguyenKhi2Click()
	{
		if (m_HeroData.NguyenKhi2ID != -1)
		{
			onNguyenKhiClick(1);
		}
	}

	public void onNguyenKhi3Click()
	{
		if (m_HeroData.NguyenKhi3ID != -1)
		{
			onNguyenKhiClick(2);
		}
	}

	public void onNguyenKhi4Click()
	{
		if (m_HeroData.NguyenKhi4ID != -1)
		{
			onNguyenKhiClick(3);
		}
	}

	public void onNguyenKhi5Click()
	{
		if (m_HeroData.NguyenKhi5ID != -1)
		{
			onNguyenKhiClick(4);
		}
	}

	public void onNguyenKhi6Click()
	{
		if (m_HeroData.NguyenKhi6ID != -1)
		{
			onNguyenKhiClick(5);
		}
	}

	private void onNguyenKhiClick(int index)
	{
		currentNguyenKhiSlotSelected = index;
		List<int> nk_id = new List<int>();
		nk_id.Add(m_HeroData.NguyenKhi1ID);
		nk_id.Add(m_HeroData.NguyenKhi2ID);
		nk_id.Add(m_HeroData.NguyenKhi3ID);
		nk_id.Add(m_HeroData.NguyenKhi4ID);
		nk_id.Add(m_HeroData.NguyenKhi5ID);
		nk_id.Add(m_HeroData.NguyenKhi6ID);
		if (RefUserInfo.NguyenKhiList == null)
		{
			return;
		}
		UserInfo.NguyenKhiData nguyenKhiData = RefUserInfo.NguyenKhiList.Find((UserInfo.NguyenKhiData e) => e.ID == nk_id[index]);
		if (nguyenKhiData == null)
		{
			if (!IsReadOnlyMode())
			{
				PopupSelectNguyenKhi.Create(OnChangeNguyenKhi, nk_id);
			}
		}
		else if (IsReadOnlyMode())
		{
			PopUpNguyenKhi.CreateByNormalScreen(nguyenKhiData);
		}
		else
		{
			PopUpNguyenKhi.CreateByScreenDoiHinh(nguyenKhiData);
		}
	}

	public bool OnChangeNguyenKhi(int id)
	{
		LapNguyenKhiRequest lapNguyenKhiRequest = new LapNguyenKhiRequest();
		lapNguyenKhiRequest.HID = m_HeroData.HID;
		lapNguyenKhiRequest.NguyenKhiID = id;
		lapNguyenKhiRequest.Slot = currentNguyenKhiSlotSelected + 1;
		GameManager.instance.m_GameClient.RequestLapNguyenKhi(lapNguyenKhiRequest);
		return true;
	}

	public bool OnChangeTrangBi(int id)
	{
		GameManager.instance.m_GameClient.RequestSetTrangBi(m_HeroData.HID, id);
		return true;
	}

	public bool OnChangeVoCong(int id)
	{
		GameManager.instance.m_GameClient.RequestSetVoCong(m_HeroData.HID, id);
		return true;
	}

	public void updateTrangBiInfo(UserInfo.TrangBiData data)
	{
		switch (TrangBiCfg.GetLoaiTrangBi(data.Name))
		{
		case LoaiTrangBi.Mu:
			MuAvatar.Set(data, true);
			break;
		case LoaiTrangBi.VuKhi:
			VuKhiAvatar.Set(data, true);
			break;
		case LoaiTrangBi.AoGiap:
			AoGiapAvatar.Set(data, true);
			break;
		case LoaiTrangBi.TrangSuc:
			TrangSucAvatar.Set(data, true);
			break;
		}
	}

	private void startPlayAnim(GameObject m_animDuyen)
	{
		m_animDuyen.SetActive(true);
		m_animDuyen.GetComponent<ParticleSystem>().Simulate(0f, true, true);
		m_animDuyen.GetComponent<ParticleSystem>().Play();
	}

	public void onThienMa1Click()
	{
		if (IsReadOnlyMode())
		{
			if (m_HeroData.ThienMaLenhID1 > 0)
			{
				if (RefUserInfo.ListThienMaLenh != null)
				{
					UserInfo.ThienMaLenhInfo thienMaLenhInfo = RefUserInfo.ListThienMaLenh.Find((UserInfo.ThienMaLenhInfo e) => e.ID == m_HeroData.ThienMaLenhID1);
					if (thienMaLenhInfo != null)
					{
						PopupBaoKhi.Create(thienMaLenhInfo, true, 1);
					}
				}
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoChuongMonChuaCoBaoKhi"));
			}
		}
		else if (m_HeroData.ThienMaLenhID1 > 0)
		{
			if (RefUserInfo.ListThienMaLenh != null)
			{
				UserInfo.ThienMaLenhInfo thienMaLenhInfo2 = RefUserInfo.ListThienMaLenh.Find((UserInfo.ThienMaLenhInfo e) => e.ID == m_HeroData.ThienMaLenhID1);
				if (thienMaLenhInfo2 != null)
				{
					PopupBaoKhi.Create(thienMaLenhInfo2, false, 1);
				}
			}
		}
		else
		{
			curSlotBaoKhiIdx = 1;
			PopupSelectBaoKhi.Create(OnChangeBaoKhi, getIgnoreListThienMa());
		}
	}

	public void onThienMa2Click()
	{
		if (IsReadOnlyMode())
		{
			if (m_HeroData.ThienMaLenhID2 > 0)
			{
				if (RefUserInfo.ListThienMaLenh != null)
				{
					UserInfo.ThienMaLenhInfo thienMaLenhInfo = RefUserInfo.ListThienMaLenh.Find((UserInfo.ThienMaLenhInfo e) => e.ID == m_HeroData.ThienMaLenhID2);
					if (thienMaLenhInfo != null)
					{
						PopupBaoKhi.Create(thienMaLenhInfo, true, 2);
					}
				}
			}
			else
			{
				MessagePopup.Create(Localization.instance.Get("ThongBaoChuongMonChuaCoBaoKhi"));
			}
		}
		else if (m_HeroData.ThienMaLenhID2 > 0)
		{
			if (RefUserInfo.ListThienMaLenh != null)
			{
				UserInfo.ThienMaLenhInfo thienMaLenhInfo2 = RefUserInfo.ListThienMaLenh.Find((UserInfo.ThienMaLenhInfo e) => e.ID == m_HeroData.ThienMaLenhID2);
				if (thienMaLenhInfo2 != null)
				{
					PopupBaoKhi.Create(thienMaLenhInfo2, false, 2);
				}
			}
		}
		else
		{
			curSlotBaoKhiIdx = 2;
			PopupSelectBaoKhi.Create(OnChangeBaoKhi, getIgnoreListThienMa());
		}
	}

	public void displayPopUpSelectBaoKhi()
	{
		PopupSelectBaoKhi.Create(OnChangeBaoKhi, getIgnoreListThienMa());
	}

	public List<int> getIgnoreListThienMa()
	{
		List<int> list = new List<int>();
		if (m_HeroData.ThienMaLenhID1 > 0)
		{
			list.Add(m_HeroData.ThienMaLenhID1);
		}
		if (m_HeroData.ThienMaLenhID2 > 0)
		{
			list.Add(m_HeroData.ThienMaLenhID2);
		}
		return list;
	}

	public bool OnChangeBaoKhi(int id)
	{
		SetThienMaLenhRequest setThienMaLenhRequest = new SetThienMaLenhRequest();
		setThienMaLenhRequest.HeroID = m_HeroData.HID;
		setThienMaLenhRequest.ID = id;
		setThienMaLenhRequest.SlotID = curSlotBaoKhiIdx;
		GameManager.instance.m_GameClient.RequestThienMaEquip(setThienMaLenhRequest);
		return true;
	}

	public void loadTonHieuGamer()
	{
		int level = RefUserInfo.Gamer.Level;
		if (level < 35)
		{
			displayTonHieuByLevel(5, UserInfo.GamerData.TonHieuType.LEVEL);
		}
		if (level >= 35 && level <= 50)
		{
			displayTonHieuByLevel(4, UserInfo.GamerData.TonHieuType.LEVEL);
		}
		if (level >= 51 && level <= 70)
		{
			displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.LEVEL);
		}
		if (level >= 71 && level <= 90)
		{
			displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.LEVEL);
		}
		if (level >= 91)
		{
			displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.LEVEL);
		}
		switch (RefUserInfo.Gamer.CurTonHieu)
		{
		case UserInfo.GamerData.TonHieuType.HANH_TAU:
			if (RefUserInfo.GiangHo != null)
			{
				int count = GameManager.instance.m_GameClient.UserInfo.GiangHo.Count;
				if (count <= 20)
				{
					displayTonHieuByLevel(5, UserInfo.GamerData.TonHieuType.HANH_TAU);
				}
				if (count >= 21 && count <= 40)
				{
					displayTonHieuByLevel(4, UserInfo.GamerData.TonHieuType.HANH_TAU);
				}
				if (count >= 41 && count <= 60)
				{
					displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.HANH_TAU);
				}
				if (count >= 61 && count <= 70)
				{
					displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.HANH_TAU);
				}
				if (count >= 71)
				{
					displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.HANH_TAU);
				}
			}
			break;
		case UserInfo.GamerData.TonHieuType.CHIEN_TRUONG:
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP1_ChienTruong"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.CHIEN_TRUONG);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP2_ChienTruong"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.CHIEN_TRUONG);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP3_ChienTruong"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.CHIEN_TRUONG);
			}
			break;
		case UserInfo.GamerData.TonHieuType.TINH_LUYEN:
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP1_TinhLuyen"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.TINH_LUYEN);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP2_TinhLuyen"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.TINH_LUYEN);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP3_TinhLuyen"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.TINH_LUYEN);
			}
			break;
		case UserInfo.GamerData.TonHieuType.CONG_LUC:
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP1_CongLuc"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.CONG_LUC);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP2_CongLuc"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.CONG_LUC);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP3_CongLuc"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.CONG_LUC);
			}
			break;
		case UserInfo.GamerData.TonHieuType.LUAN_KIEM:
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP1_LuanKiem"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.LUAN_KIEM);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP2_LuanKiem"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.LUAN_KIEM);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP3_LuanKiem"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.LUAN_KIEM);
			}
			break;
		case UserInfo.GamerData.TonHieuType.HOANG_KIM:
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP1_HoangKim"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.HOANG_KIM);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP2_HoangKim"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.HOANG_KIM);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP3_HoangKim"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.HOANG_KIM);
			}
			break;
		case UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH:
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP1_QMD"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP2_QMD"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP3_QMD"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH);
			}
			break;
		case UserInfo.GamerData.TonHieuType.THAN_THU:
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP1_ThanThu"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.THAN_THU);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP2_ThanThu"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.THAN_THU);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP3_ThanThu"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.THAN_THU);
			}
			break;
		case UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM:
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP1_DHVL"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP2_DHVL"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP3_DHVL"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM);
			}
			break;
		case UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG:
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP1_ThienMa"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP2_ThienMa"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP3_ThienMa"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG);
			}
			break;
		case UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM:
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP1_TBHK"))
			{
				displayTonHieuByLevel(1, UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP2_TBHK"))
			{
				displayTonHieuByLevel(2, UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM);
			}
			if (RefUserInfo.Gamer.GhiChuTrongNgay.Contains("TOP3_TBHK"))
			{
				displayTonHieuByLevel(3, UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM);
			}
			break;
		}
	}

	private void displayTonHieuByLevel(int top, UserInfo.GamerData.TonHieuType type)
	{
		string key = string.Empty;
		string spriteName = string.Empty;
		string spriteName2 = string.Empty;
		for (int i = 1; i < 6; i++)
		{
			if (top == i)
			{
				spriteName = "tonhieu_ball_top" + i;
				if (type == UserInfo.GamerData.TonHieuType.LEVEL)
				{
					key = "TonHieu_Level_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.HANH_TAU)
				{
					key = "TonHieu_HanhTau_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.CHIEN_TRUONG)
				{
					key = "TonHieu_ChienTruong_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.TINH_LUYEN)
				{
					key = "TonHieu_TinhLuyen_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.CONG_LUC)
				{
					key = "TonHieu_CongLuc_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.LUAN_KIEM)
				{
					key = "TonHieu_LuanKiem_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.HOANG_KIM)
				{
					key = "TonHieu_HoangKim_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH)
				{
					key = "TonHieu_QMD_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.THAN_THU)
				{
					key = "TonHieu_ThanThu_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM)
				{
					key = "TonHieu_DHVL_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG)
				{
					key = "TonHieu_ThienMa_TOP" + i;
				}
				if (type == UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM)
				{
					key = "TonHieu_TBHoangKim_TOP" + i;
				}
			}
		}
		switch (type)
		{
		case UserInfo.GamerData.TonHieuType.LEVEL:
			spriteName2 = "tonhieu_level_bg";
			break;
		case UserInfo.GamerData.TonHieuType.HANH_TAU:
			spriteName2 = "tonhieu_giangho_bg";
			break;
		case UserInfo.GamerData.TonHieuType.CHIEN_TRUONG:
			spriteName2 = "tonhieu_chientruong_bg";
			break;
		case UserInfo.GamerData.TonHieuType.TINH_LUYEN:
			spriteName2 = "tonhieu_tinhluyen_bg";
			break;
		case UserInfo.GamerData.TonHieuType.CONG_LUC:
			spriteName2 = "tonhieu_congluc_bg";
			break;
		case UserInfo.GamerData.TonHieuType.LUAN_KIEM:
			spriteName2 = "tonhieu_luankiem_bg";
			break;
		case UserInfo.GamerData.TonHieuType.HOANG_KIM:
			spriteName2 = "tonhieu_hoangkim_bg";
			break;
		case UserInfo.GamerData.TonHieuType.QUANG_MINH_DINH:
			spriteName2 = "tonhieu_QMD_bg";
			break;
		case UserInfo.GamerData.TonHieuType.THAN_THU:
			spriteName2 = "tonhieu_thanthu_bg";
			break;
		case UserInfo.GamerData.TonHieuType.DAI_HOI_VO_LAM:
			spriteName2 = "tonhieu_DHVL_bg";
			break;
		case UserInfo.GamerData.TonHieuType.THIEN_MA_THUONG_PHONG:
			spriteName2 = "tonhieu_thienma_bg";
			break;
		case UserInfo.GamerData.TonHieuType.TRANG_BI_HOANG_KIM:
			spriteName2 = "tonhieu_tbhoangkim_bg";
			break;
		}
		grpTonHieuVang.gameObject.SetActive(false);
		grpTonHieuTim.gameObject.SetActive(false);
		if (top == 1)
		{
			grpTonHieuVang.gameObject.SetActive(true);
			parVangRight.GetComponent<ParticleSystem>().Simulate(0f, true, true);
			parVangRight.GetComponent<ParticleSystem>().Play();
			parVangLeft.GetComponent<ParticleSystem>().Simulate(0f, true, true);
			parVangLeft.GetComponent<ParticleSystem>().Play();
		}
		if (top == 2)
		{
			grpTonHieuTim.gameObject.SetActive(true);
			parTimRight.GetComponent<ParticleSystem>().Simulate(0f, true, true);
			parTimRight.GetComponent<ParticleSystem>().Play();
			parTimLeft.GetComponent<ParticleSystem>().Simulate(0f, true, true);
			parTimLeft.GetComponent<ParticleSystem>().Play();
		}
		lbCurTonHieu.text = Localization.instance.Get(key);
		spCurTonHieuBG.spriteName = spriteName2;
		spIconBall1.spriteName = spriteName;
		spIconBall2.spriteName = spriteName;
	}

	public void OnTonHieuClick()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains("DanhHieu"))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
		}
		else if (!IsReadOnlyMode())
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenTonHieu);
		}
	}

	private void OnChienHonBtnClick()
	{
		if (GameManager.instance.m_GameClient.UserInfo.ServerInfo != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang != null && GameManager.instance.m_GameClient.UserInfo.ServerInfo.LockTinhNang.Contains("ChienHon"))
		{
			MessagePopup.Create(Localization.instance.Get("TinhNangKhoa"));
			return;
		}
		NhanVatCfg value;
		ConfigManager.instance.m_dicNhanVats.TryGetValue(m_HeroData.Name, out value);
		if (value == null)
		{
			return;
		}
		if (value.Hang < 3)
		{
			MessagePopup.Create(Localization.instance.Get("ChienHonPhamGiap"));
			return;
		}
		UserInfo.ChienHon chienHon = null;
		ScreenDoiHinh screenDoiHinh = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
		if (RefUserInfo.ChienHonList != null)
		{
			chienHon = RefUserInfo.ChienHonList.Find((UserInfo.ChienHon e) => e.HID == m_HeroData.HID);
		}
		bool isShowAnotherUserInfo = screenDoiHinh.isShowAnotherUserInfo;
		if (chienHon != null)
		{
			GUIManager.setScreen(GAME_SCREEN.ScreenChienHon);
			ScreenChienHon screenChienHon = GUIManager.getScreen(GAME_SCREEN.ScreenChienHon) as ScreenChienHon;
			if (isShowAnotherUserInfo)
			{
				screenChienHon.ShowOtherUserInfo(RefUserInfo);
			}
			screenChienHon.displayNhanVat3D(chienHon, m_HeroData);
		}
		else if (!isShowAnotherUserInfo)
		{
			GUIManager.setScreen(GAME_SCREEN.ScreenTrieuHoan);
			ScreenTrieuHoan screenTrieuHoan = GUIManager.getScreen(GAME_SCREEN.ScreenTrieuHoan) as ScreenTrieuHoan;
			screenTrieuHoan.Set(m_HeroData);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("NhanVatChuaTrangBiChienHon"));
		}
	}
}

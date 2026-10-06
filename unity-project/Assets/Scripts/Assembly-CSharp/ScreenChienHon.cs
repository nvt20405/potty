using UnityEngine;

public class ScreenChienHon : ScreenBase
{
	public GameObject nhanVatAvatar3D;

	private UserInfo RefUserInfo;

	public UILabel MenhLabel;

	public UILabel NgoaiLabel;

	public UILabel ThanLabel;

	public UILabel KhiLabel;

	public UILabel NeLabel;

	public UILabel BaoLabel;

	public UILabel DoDonLabel;

	public UILabel TenNhanVat;

	public OtherAvatar HuyenKhiAvatar1;

	public OtherAvatar HuyenKhiAvatar2;

	private GameObject m_avatar;

	public UILabel levelLabel;

	public UILabel expProgressLabel;

	public UISlider expSlider;

	public GameObject buffMenhIcon;

	public GameObject buffNgoaiIcon;

	public GameObject buffThanIcon;

	public GameObject buffNoiIcon;

	public UILabel Desc;

	public GameObject ShowOtherBtn;

	public GameObject BackBtn;

	public UILabel capDotPhaLabel;

	private bool isShowAnotherUserInfo;

	public UserInfo.HeroData heroData { get; set; }

	public UserInfo.ChienHon chienHonData { get; set; }

	public GameObject NhanVat3D { get; set; }

	public UserInfo.HuyenKhi huyenKhiData1 { get; set; }

	public UserInfo.HuyenKhi huyenKhiData2 { get; set; }

	public bool IsShowAnotherUserInfo
	{
		get
		{
			return isShowAnotherUserInfo;
		}
	}

	private void Start()
	{
	}

	private void DisplayBuff()
	{
		if (chienHonData != null)
		{
			if (chienHonData.LoaiBuff == 0)
			{
				buffMenhIcon.SetActive(true);
				buffNgoaiIcon.SetActive(false);
				buffThanIcon.SetActive(false);
				buffNoiIcon.SetActive(false);
			}
			else if (chienHonData.LoaiBuff == 1)
			{
				buffMenhIcon.SetActive(false);
				buffNgoaiIcon.SetActive(true);
				buffThanIcon.SetActive(false);
				buffNoiIcon.SetActive(false);
			}
			else if (chienHonData.LoaiBuff == 2)
			{
				buffMenhIcon.SetActive(false);
				buffNgoaiIcon.SetActive(false);
				buffThanIcon.SetActive(true);
				buffNoiIcon.SetActive(false);
			}
			else
			{
				buffMenhIcon.SetActive(false);
				buffNgoaiIcon.SetActive(false);
				buffThanIcon.SetActive(false);
				buffNoiIcon.SetActive(true);
			}
		}
	}

	private void Update()
	{
		if (!(m_avatar == null) || !(NhanVat3D != null))
		{
			return;
		}
		Avatar3D component = NhanVat3D.GetComponent<Avatar3D>();
		if (component != null)
		{
			m_avatar = component.AvatarGO;
			if (m_avatar != null)
			{
				m_avatar.AddComponent<ChienHonTrans>();
			}
		}
	}

	public override void OnDeactive()
	{
		RefUserInfo = null;
		isShowAnotherUserInfo = false;
		ShowOtherBtn.SetActive(false);
		base.OnDeactive();
	}

	public override void OnActive()
	{
		base.OnActive();
		if (RefUserInfo == null)
		{
			RefUserInfo = GameManager.instance.m_GameClient.UserInfo;
		}
	}

	public void ShowOtherUserInfo(UserInfo info)
	{
		isShowAnotherUserInfo = true;
		RefUserInfo = info;
		ShowOtherBtn.SetActive(true);
		BackBtn.SetActive(false);
	}

	public void ReActive(bool ignore3D = false)
	{
		if (RefUserInfo == null)
		{
			RefUserInfo = GameManager.instance.m_GameClient.UserInfo;
		}
		if (RefUserInfo != null && RefUserInfo.ChienHonList != null && RefUserInfo.HeroList != null && chienHonData != null)
		{
			chienHonData = RefUserInfo.ChienHonList.Find((UserInfo.ChienHon e) => e.ID == chienHonData.ID);
			heroData = RefUserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == heroData.HID);
			displayNhanVat3D(chienHonData, heroData, ignore3D);
		}
	}

	public void displayNhanVat3D(UserInfo.ChienHon chienhon, UserInfo.HeroData data, bool ignore3D = false)
	{
		m_avatar = null;
		heroData = data;
		chienHonData = chienhon;
		ChiSoNhanVat chiSoCuoi = chienhon.GetChiSoCuoi(RefUserInfo.HuyenKhiList);
		MenhLabel.text = ((long)chiSoCuoi.Menh).ToString();
		NgoaiLabel.text = ((long)chiSoCuoi.Ngoai).ToString();
		ThanLabel.text = ((long)chiSoCuoi.ThanPhap).ToString();
		KhiLabel.text = ((long)chiSoCuoi.Noi).ToString();
		levelLabel.text = chienHonData.Level.ToString();
		if (chienHonData.MaxExp == 0)
		{
			chienHonData.MaxExp = ConfigManager.instance.OtherConfig.GetChienHonMaxExpByLevel(1);
		}
		expProgressLabel.text = string.Format("{0}/{1}", chienHonData.Exp, chienHonData.MaxExp);
		expSlider.sliderValue = (float)chienHonData.Exp / (float)chienHonData.MaxExp;
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[heroData.Name];
		TenNhanVat.text = nhanVatCfg.TenHienThi;
		capDotPhaLabel.text = string.Format(Localization.instance.Get("ChienHonCapDotPha"), chienHonData.DotPha);
		ChienHonCfg chienHonCfg = ConfigManager.instance.m_dicChienHons[data.Name];
		NeLabel.text = chienHonCfg.Ne.ToString();
		BaoLabel.text = chienHonCfg.Bao.ToString();
		DoDonLabel.text = chienHonCfg.Do.ToString();
		Desc.text = chienHonCfg.Mota;
		if (chienHonData.Level < ConfigManager.instance.OtherConfig.LevelChienHonUnlockHK1)
		{
			HuyenKhiAvatar1.Set("lock");
			HuyenKhiAvatar2.Set("lock");
		}
		else if (chienHonData.Level < ConfigManager.instance.OtherConfig.LevelChienHonUnlockHK2)
		{
			if (RefUserInfo.HuyenKhiList != null)
			{
				huyenKhiData1 = RefUserInfo.HuyenKhiList.Find((UserInfo.HuyenKhi e) => e.ID == chienHonData.HuyenKhi1ID);
				HuyenKhiAvatar1.SetHuyenKhi(huyenKhiData1);
			}
			HuyenKhiAvatar2.Set("lock");
		}
		else if (RefUserInfo.HuyenKhiList != null)
		{
			huyenKhiData1 = RefUserInfo.HuyenKhiList.Find((UserInfo.HuyenKhi e) => e.ID == chienHonData.HuyenKhi1ID);
			HuyenKhiAvatar1.SetHuyenKhi(huyenKhiData1);
			huyenKhiData2 = RefUserInfo.HuyenKhiList.Find((UserInfo.HuyenKhi e) => e.ID == chienHonData.HuyenKhi2ID);
			HuyenKhiAvatar2.SetHuyenKhi(huyenKhiData2);
		}
		DisplayBuff();
		if (!ignore3D)
		{
			if (NhanVat3D != null)
			{
				Object.Destroy(NhanVat3D);
				NhanVat3D = null;
			}
			string empty = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			UserInfo refUserInfo = RefUserInfo;
			string empty4 = string.Empty;
			Avatar3D avatar3D = GUIManager.instance.InstantiateAvatar3DWithBattleAnim(data.Name, empty, empty3, empty2, empty4);
			nhanVatAvatar3D.transform.localRotation = Quaternion.Euler(0f, 2.53f, 0f);
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
	}

	public void OnHuyenKhi1Click()
	{
		if (chienHonData.Level < ConfigManager.instance.OtherConfig.LevelChienHonUnlockHK1)
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("UnlockHuyenKhi2"), ConfigManager.instance.OtherConfig.LevelChienHonUnlockHK1));
		}
		else if (RefUserInfo.HuyenKhiList != null && RefUserInfo.HuyenKhiList.Count > 0)
		{
			if (HuyenKhiAvatar1.IsEmpty())
			{
				if (!isShowAnotherUserInfo)
				{
					PopupSelectHuyenKhi.Create(null, OnHuyenKhi1Select, false, 0);
				}
			}
			else
			{
				PopupHuyenKhi.Create(huyenKhiData1, 1);
			}
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ChuaCoHuyenKhi"));
		}
	}

	private bool OnHuyenKhi1Select(int huyenKhi1ID)
	{
		if (isShowAnotherUserInfo)
		{
			return false;
		}
		GameManager.instance.m_GameClient.RequestTrangBiHuyenKhi(chienHonData.ID, huyenKhi1ID, 1);
		return false;
	}

	public void OnHuyenKhi2Click()
	{
		if (chienHonData.Level < ConfigManager.instance.OtherConfig.LevelChienHonUnlockHK2)
		{
			MessagePopup.Create(string.Format(Localization.instance.Get("UnlockHuyenKhi2"), ConfigManager.instance.OtherConfig.LevelChienHonUnlockHK2));
		}
		else if (RefUserInfo.HuyenKhiList != null && RefUserInfo.HuyenKhiList.Count > 0)
		{
			if (HuyenKhiAvatar2.IsEmpty())
			{
				if (!isShowAnotherUserInfo)
				{
					PopupSelectHuyenKhi.Create(null, OnHuyenKhi2Select, false, 0);
				}
			}
			else
			{
				PopupHuyenKhi.Create(huyenKhiData2, 2);
			}
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ChuaCoHuyenKhi"));
		}
	}

	private bool OnHuyenKhi2Select(int huyenKhi2ID)
	{
		if (isShowAnotherUserInfo)
		{
			return false;
		}
		GameManager.instance.m_GameClient.RequestTrangBiHuyenKhi(chienHonData.ID, huyenKhi2ID, 2);
		return false;
	}

	public void OnChienHonClick()
	{
		if (!isShowAnotherUserInfo)
		{
			PopupChienHon.Create(RefUserInfo, chienHonData, heroData);
		}
	}

	public void OnBtnShowOtherThoat()
	{
		ScreenDoiHinh screenDoiHinh = (ScreenDoiHinh)GUIManager.instance.GetScreen(GAME_SCREEN.ScreenDoiHinh);
		screenDoiHinh.ShowAnotherUserInfo(RefUserInfo, true);
		isShowAnotherUserInfo = false;
	}

	public void btnBack_OnClick()
	{
		if (!isShowAnotherUserInfo)
		{
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDoiHinh);
		}
	}
}

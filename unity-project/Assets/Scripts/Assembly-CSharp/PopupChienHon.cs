using UnityEngine;

public class PopupChienHon : MonoBehaviour
{
	public NhanVatAvatar nhanvatAvatar;

	public UILabel nhanVatName;

	public UILabel MenhLabel;

	public UILabel NgoaiLabel;

	public UILabel ThanPhapLabel;

	public UILabel KhiLabel;

	public UILabel NhanVatDesc;

	public UILabel NhanVatDuyen;

	public UILabel NeLabel;

	public UILabel BaoLabel;

	public UILabel DoLabel;

	public GameObject CloseBtn;

	private UserInfo RefUserInfo;

	private UserInfo.HeroData mHeroData;

	private UserInfo.ChienHon mChienHonData;

	private NhanVatCfg mHeroConfig;

	public UISprite spPham;

	public GameObject buffMenhIcon;

	public GameObject buffNgoaiIcon;

	public GameObject buffThanIcon;

	public GameObject buffNoiIcon;

	public static PopupChienHon instance;

	private void Start()
	{
	}

	private void DisplayBuff()
	{
		if (mChienHonData != null)
		{
			if (mChienHonData.LoaiBuff == 0)
			{
				buffMenhIcon.SetActive(true);
				buffNgoaiIcon.SetActive(false);
				buffThanIcon.SetActive(false);
				buffNoiIcon.SetActive(false);
			}
			else if (mChienHonData.LoaiBuff == 1)
			{
				buffMenhIcon.SetActive(false);
				buffNgoaiIcon.SetActive(true);
				buffThanIcon.SetActive(false);
				buffNoiIcon.SetActive(false);
			}
			else if (mChienHonData.LoaiBuff == 2)
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

	public void OnCloseClick()
	{
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(UserInfo ref_user_info, UserInfo.ChienHon data, UserInfo.HeroData hero)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupChienHon"))).GetComponent<PopupChienHon>();
		PopupManager.instance.Add(instance.gameObject);
		instance.RefUserInfo = ref_user_info;
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set(data, hero);
	}

	public void Set(UserInfo.ChienHon data, UserInfo.HeroData hero)
	{
		mChienHonData = data;
		mHeroData = hero;
		nhanvatAvatar.Set(mHeroData.Name, mChienHonData.DotPha, mChienHonData.Level);
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[data.Name];
		DisplayBuff();
		if (nhanVatCfg.Hang == 3)
		{
			spPham.spriteName = "giap";
		}
		else if (nhanVatCfg.Hang == 2)
		{
			spPham.spriteName = "at";
		}
		else if (nhanVatCfg.Hang == 1)
		{
			spPham.spriteName = "binh";
		}
		ChiSoNhanVat chiSoCuoi = mChienHonData.GetChiSoCuoi(RefUserInfo.HuyenKhiList);
		MenhLabel.text = ((long)chiSoCuoi.Menh).ToString();
		NgoaiLabel.text = ((long)chiSoCuoi.Ngoai).ToString();
		ThanPhapLabel.text = ((long)chiSoCuoi.ThanPhap).ToString();
		KhiLabel.text = ((long)chiSoCuoi.Noi).ToString();
		ChienHonCfg chienHonCfg = ConfigManager.instance.m_dicChienHons[data.Name];
		NeLabel.text = chienHonCfg.Ne.ToString();
		BaoLabel.text = chienHonCfg.Bao.ToString();
		DoLabel.text = chienHonCfg.Do.ToString();
		NhanVatDesc.text = chienHonCfg.Mota;
		nhanVatName.text = chienHonCfg.TenHienThi;
	}

	private string GetSpriteNameThuocTinh(OtherCfg.NguyenKhiType type)
	{
		switch (type)
		{
		case OtherCfg.NguyenKhiType.MENH:
			return "icon_menh";
		case OtherCfg.NguyenKhiType.NGOAI:
			return "icon_ngoai";
		case OtherCfg.NguyenKhiType.THAN:
			return "icon_than";
		case OtherCfg.NguyenKhiType.KHI:
			return "icon_noi_goc";
		default:
			return string.Empty;
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

	private void Set(NhanVatCfg cfgData)
	{
		mHeroConfig = cfgData;
		nhanvatAvatar.Set(cfgData.Name);
		nhanVatName.text = cfgData.TenHienThi;
		if (cfgData.Hang == 3)
		{
			spPham.spriteName = "giap";
		}
		else if (cfgData.Hang == 2)
		{
			spPham.spriteName = "at";
		}
		else if (cfgData.Hang == 1)
		{
			spPham.spriteName = "binh";
		}
		NhanVatDesc.text = cfgData.Mota;
	}

	public void OnThangCapClick()
	{
		DestroyPopup();
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenThangCapChienHon);
		ScreenThangCapChienHon screenThangCapChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenThangCapChienHon) as ScreenThangCapChienHon;
		screenThangCapChienHon.Sync(mChienHonData, mHeroData);
	}

	public void OnDotPhaClick()
	{
		DestroyPopup();
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDotPhaChienHon);
		ScreenDotPhaChienHon screenDotPhaChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenDotPhaChienHon) as ScreenDotPhaChienHon;
		screenDotPhaChienHon.Sync(mChienHonData, mHeroData);
	}

	public void OnDoiThuocTinhClick()
	{
		DestroyPopup();
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenDoiThuocTinhChienHon);
		ScreenDoiThuocTinhChienHon screenDoiThuocTinhChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenDoiThuocTinhChienHon) as ScreenDoiThuocTinhChienHon;
		screenDoiThuocTinhChienHon.Sync(mChienHonData, mHeroData);
	}

	public void OnBoiDuongClick()
	{
		DestroyPopup();
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenBoiDuongChienHon);
		ScreenBoiDuongChienHon screenBoiDuongChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenBoiDuongChienHon) as ScreenBoiDuongChienHon;
		screenBoiDuongChienHon.displayInfo(mChienHonData, mHeroData);
	}
}

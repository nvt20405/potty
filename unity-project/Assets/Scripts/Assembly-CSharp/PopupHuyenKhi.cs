using UnityEngine;

public class PopupHuyenKhi : MonoBehaviour
{
	public static PopupHuyenKhi instance;

	public OtherAvatar avatar;

	public UILabel huyenKhiName;

	public UICheckbox checkBox;

	public UILabel ChiSo1;

	public UISprite ChiSo1Spr;

	public UILabel ChiSo2;

	public UISprite ChiSo2Spr;

	public UILabel mota;

	public UIButton ThayDoiBtn;

	private int m_loaiHuyenKhi;

	public UserInfo.HuyenKhi m_info { get; set; }

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
		}
		instance = null;
	}

	public void OnCloseClick()
	{
		DestroyPopup();
	}

	public static void Create(UserInfo.HuyenKhi info, int loai)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("Popup/PopupHuyenKhi"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupHuyenKhi>();
		instance.Set(info, loai);
	}

	public void Set(UserInfo.HuyenKhi info, int loai)
	{
		if (loai == 0)
		{
			ThayDoiBtn.isEnabled = false;
			ThayDoiBtn.enabled = false;
		}
		m_loaiHuyenKhi = loai;
		m_info = info;
		HuyenKhiCfg huyenKhiCfg = ConfigManager.instance.m_dicHuyenKhi[info.Name];
		huyenKhiName.text = huyenKhiCfg.TenHienThi;
		avatar.SetHuyenKhi(info);
		ChiSo1Spr.spriteName = "icon_" + huyenKhiCfg.LoaiBuff1;
		ChiSo1.text = huyenKhiCfg.ChiSo1ByLevels[info.Level - 1] + "%";
		ChiSo2Spr.spriteName = "icon_" + huyenKhiCfg.LoaiBuff2;
		ChiSo2.text = huyenKhiCfg.ChiSo2ByLevels[info.Level - 1] + "%";
		EffHuyenKhiCfg effHuyenKhiCfg = ConfigManager.instance.m_dicEffHuyenKhi[info.EffName];
		mota.text = string.Format(effHuyenKhiCfg.Mota, effHuyenKhiCfg.HeSoByLevels[info.Level - 1]);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnCuongHoa()
	{
		ScreenChienHon screenChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenChienHon) as ScreenChienHon;
		if (!screenChienHon.IsShowAnotherUserInfo)
		{
			DestroyPopup();
			PopupCuongHoaHuyenKhi.Create(m_info);
		}
	}

	public void OnChangeHuyenKhi()
	{
		ScreenChienHon screenChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenChienHon) as ScreenChienHon;
		if (!screenChienHon.IsShowAnotherUserInfo)
		{
			DestroyPopup();
			PopupSelectHuyenKhi.Create(m_info, OnChangeHuyenKhi, false, m_info.ID);
		}
	}

	private bool OnChangeHuyenKhi(int huyenKhiId)
	{
		ScreenChienHon screenChienHon = GUIManager.instance.GetScreen(GAME_SCREEN.ScreenChienHon) as ScreenChienHon;
		if (screenChienHon.IsShowAnotherUserInfo)
		{
			return false;
		}
		GameManager.instance.m_GameClient.RequestTrangBiHuyenKhi(m_info.ChienHonID, huyenKhiId, m_loaiHuyenKhi);
		return false;
	}
}

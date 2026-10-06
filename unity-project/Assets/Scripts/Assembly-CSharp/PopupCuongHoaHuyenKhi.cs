using UnityEngine;

public class PopupCuongHoaHuyenKhi : MonoBehaviour
{
	public static PopupCuongHoaHuyenKhi instance;

	public OtherAvatar avatar;

	public UILabel huyenKhiName;

	public UICheckbox checkBox;

	public UILabel huyenKhiLevellabel;

	public UILabel huyenKhiLevellabel_next;

	public UILabel ChiSo1;

	public UISprite ChiSo1Spr;

	public UILabel ChiSo2;

	public UISprite ChiSo2Spr;

	public UILabel mota;

	public UILabel ChiSo1_Next;

	public UISprite ChiSo1Spr_Next;

	public UILabel ChiSo2_Next;

	public UISprite ChiSo2Spr_Next;

	public UILabel mota_Next;

	private int m_huyenKhiHySinh;

	public UIButton ThangCapBtn;

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

	public static void Create(UserInfo.HuyenKhi info, int huyenKhiHySinh = 0)
	{
		if (instance != null)
		{
			DestroyPopup();
		}
		Object obj = Object.Instantiate(Resources.Load("popup/PopupCuongHoaHuyenKhi"));
		GameObject gameObject = (GameObject)((obj is GameObject) ? obj : null);
		PopupManager.instance.Add(gameObject);
		gameObject.transform.localScale = Vector3.one;
		instance = gameObject.GetComponent<PopupCuongHoaHuyenKhi>();
		instance.Set(info, huyenKhiHySinh);
	}

	public void Set(UserInfo.HuyenKhi info, int huyenKhiHySinh = 0)
	{
		m_huyenKhiHySinh = huyenKhiHySinh;
		m_info = info;
		HuyenKhiCfg huyenKhiCfg = ConfigManager.instance.m_dicHuyenKhi[info.Name];
		huyenKhiName.text = huyenKhiCfg.TenHienThi;
		ChiSo1Spr.spriteName = "icon_" + huyenKhiCfg.LoaiBuff1;
		ChiSo1Spr_Next.spriteName = "icon_" + huyenKhiCfg.LoaiBuff1;
		ChiSo1.text = huyenKhiCfg.ChiSo1ByLevels[info.Level - 1] + "%";
		ChiSo2Spr.spriteName = "icon_" + huyenKhiCfg.LoaiBuff2;
		ChiSo2Spr_Next.spriteName = "icon_" + huyenKhiCfg.LoaiBuff2;
		ChiSo2.text = huyenKhiCfg.ChiSo2ByLevels[info.Level - 1] + "%";
		EffHuyenKhiCfg effHuyenKhiCfg = ConfigManager.instance.m_dicEffHuyenKhi[info.EffName];
		if (info.Level < huyenKhiCfg.ChiSo2ByLevels.Count)
		{
			ChiSo1_Next.text = huyenKhiCfg.ChiSo1ByLevels[info.Level] + "%";
			ChiSo2_Next.text = huyenKhiCfg.ChiSo2ByLevels[info.Level] + "%";
			huyenKhiLevellabel.text = Localization.instance.Get("HKCap") + " " + info.Level;
			huyenKhiLevellabel_next.text = Localization.instance.Get("HKCap") + " " + (info.Level + 1);
			mota_Next.text = string.Format(effHuyenKhiCfg.Mota, effHuyenKhiCfg.HeSoByLevels[info.Level]);
		}
		else
		{
			ChiSo1_Next.text = "MAX";
			ChiSo2_Next.text = "MAX";
			ThangCapBtn.enabled = false;
			ThangCapBtn.isEnabled = false;
			huyenKhiLevellabel.text = Localization.instance.Get("HKCap") + " " + info.Level;
			huyenKhiLevellabel_next.text = Localization.instance.Get("HKCap") + " " + info.Level;
			mota_Next.text = string.Format(effHuyenKhiCfg.Mota, effHuyenKhiCfg.HeSoByLevels[huyenKhiCfg.ChiSo2ByLevels.Count - 1]);
		}
		if (GameManager.instance.m_GameClient.UserInfo.HuyenKhiList != null)
		{
			UserInfo.HuyenKhi huyenKhi = GameManager.instance.m_GameClient.UserInfo.HuyenKhiList.Find((UserInfo.HuyenKhi e) => e.ID == huyenKhiHySinh);
			if (huyenKhi != null)
			{
				avatar.SetHuyenKhi(huyenKhi);
			}
			else
			{
				avatar.Set("empty");
			}
		}
		else
		{
			avatar.Set("empty");
		}
		mota.text = string.Format(effHuyenKhiCfg.Mota, effHuyenKhiCfg.HeSoByLevels[info.Level - 1]);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnSelectHuyenKhi()
	{
		DestroyPopup();
		HuyenKhiCfg value;
		if (ConfigManager.instance.m_dicHuyenKhi.TryGetValue(m_info.Name, out value))
		{
			PopupSelectHuyenKhi.Create(m_info, null, true, m_info.ID, m_info.Level, value.Loai);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("HuyenKhiKhongHopLe"));
		}
	}

	private bool OnSelectOk(int huyenKhiId)
	{
		return false;
	}

	public void OnCuongHoa()
	{
		PopupSelectHuyenKhi.DestroyPopup();
		if (m_huyenKhiHySinh > 0)
		{
			GameManager.instance.m_GameClient.RequestThangCapHuyenKhi(m_info.ID, m_huyenKhiHySinh);
		}
		else
		{
			MessagePopup.Create(Localization.instance.Get("ChuaChonHuyenKhiHySinh"));
		}
	}
}

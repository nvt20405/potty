using UnityEngine;

public class DSSelectHuyenKhiItem : MonoBehaviour
{
	public OtherAvatar avatar;

	public UILabel huyenKhiName;

	public UICheckbox checkBox;

	public UILabel ChiSo1;

	public UISprite ChiSo1Spr;

	public UILabel ChiSo2;

	public UISprite ChiSo2Spr;

	public UILabel mota;

	public UILabel chienHonDangSuDung;

	public UserInfo.HuyenKhi m_info { get; set; }

	public void Set(UserInfo.HuyenKhi info)
	{
		m_info = info;
		avatar.SetHuyenKhi(info);
		HuyenKhiCfg huyenKhiCfg = ConfigManager.instance.m_dicHuyenKhi[info.Name];
		huyenKhiName.text = huyenKhiCfg.TenHienThi;
		ChiSo1Spr.spriteName = "icon_" + huyenKhiCfg.LoaiBuff1;
		ChiSo1.text = huyenKhiCfg.ChiSo1ByLevels[info.Level - 1] + "%";
		ChiSo2Spr.spriteName = "icon_" + huyenKhiCfg.LoaiBuff2;
		ChiSo2.text = huyenKhiCfg.ChiSo2ByLevels[info.Level - 1] + "%";
		EffHuyenKhiCfg effHuyenKhiCfg = ConfigManager.instance.m_dicEffHuyenKhi[info.EffName];
		mota.text = string.Format(effHuyenKhiCfg.Mota, effHuyenKhiCfg.HeSoByLevels[info.Level - 1]);
		if (GameManager.instance.m_GameClient.UserInfo.ChienHonList != null)
		{
			UserInfo.ChienHon chienHon = GameManager.instance.m_GameClient.UserInfo.ChienHonList.Find((UserInfo.ChienHon e) => e.ID == info.ChienHonID);
			if (chienHon != null)
			{
				ChienHonCfg chienHonCfg = ConfigManager.instance.m_dicChienHons[chienHon.Name];
				chienHonDangSuDung.text = chienHonCfg.TenHienThi + " " + Localization.instance.Get("UsedLabel");
				chienHonDangSuDung.gameObject.SetActive(true);
			}
			else
			{
				chienHonDangSuDung.gameObject.SetActive(false);
			}
		}
		else
		{
			chienHonDangSuDung.gameObject.SetActive(false);
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnCuongHoaClick()
	{
		PopupCuongHoaHuyenKhi.Create(m_info);
	}
}

using System.Collections.Generic;
using UnityEngine;

public class PopUpNguyenKhi : MonoBehaviour
{
	public NguyenKhiAvatar nguyenKhiAvatar;

	public UILabel nguyenKhiName;

	public UILabel chiSoHoTro;

	public UILabel lbDescription;

	private UserInfo.NguyenKhiData m_NguyenKhiData;

	public static PopUpNguyenKhi instance;

	public GameObject groupButtonDoiHinh;

	public GameObject btnBigClose;

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

	public static void CreateByNormalScreen(UserInfo.NguyenKhiData nkData)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupNguyenKhi"))).GetComponent<PopUpNguyenKhi>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.groupButtonDoiHinh.gameObject.SetActive(false);
		instance.btnBigClose.gameObject.SetActive(true);
		instance.Set(nkData);
	}

	public static void CreateByScreenDoiHinh(UserInfo.NguyenKhiData nkData)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupNguyenKhi"))).GetComponent<PopUpNguyenKhi>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.groupButtonDoiHinh.gameObject.SetActive(true);
		instance.btnBigClose.gameObject.SetActive(false);
		instance.Set(nkData);
	}

	public static void CreateByConfig(OtherCfg.NguyenKhiCfg nkConfig)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupNguyenKhi"))).GetComponent<PopUpNguyenKhi>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.groupButtonDoiHinh.gameObject.SetActive(false);
		instance.btnBigClose.gameObject.SetActive(true);
		instance.SetByConfig(nkConfig);
	}

	public void SetByConfig(OtherCfg.NguyenKhiCfg cfg)
	{
		nguyenKhiAvatar.Set(cfg.Codename, 1);
		Utils.SetLayer(nguyenKhiAvatar.transform, "GUIPopUp", true);
		nguyenKhiName.text = cfg.DisplayName;
		if (cfg.MoTa != null)
		{
			lbDescription.text = cfg.MoTa.ToString();
		}
		else
		{
			lbDescription.text = string.Empty;
		}
		string text = string.Empty;
		if (cfg != null)
		{
			if (cfg.Loai == OtherCfg.NguyenKhiType.DO_DON)
			{
				text = Localization.instance.Get("ChiSoCoBanDoDonLabel");
			}
			else if (cfg.Loai == OtherCfg.NguyenKhiType.MENH)
			{
				text = Localization.instance.Get("ChiSoCoBanMenhLabel");
			}
			else if (cfg.Loai == OtherCfg.NguyenKhiType.KHI)
			{
				text = Localization.instance.Get("ChiSoCoBanKhiLabel");
			}
			else if (cfg.Loai == OtherCfg.NguyenKhiType.KHANG_BAO)
			{
				text = Localization.instance.Get("ChiSoCoBanKhangBaoLabel");
			}
			else if (cfg.Loai == OtherCfg.NguyenKhiType.NGOAI)
			{
				text = Localization.instance.Get("ChiSoCoBanNgoaiLabel");
			}
			else if (cfg.Loai == OtherCfg.NguyenKhiType.THAN)
			{
				text = Localization.instance.Get("ChiSoCoBanThanLabel");
			}
			chiSoHoTro.text = ((cfg.Loai != OtherCfg.NguyenKhiType.DO_DON && cfg.Loai != OtherCfg.NguyenKhiType.KHANG_BAO && cfg.Loai != OtherCfg.NguyenKhiType.MENH) ? (text + ": " + cfg.baseStat) : (text + ": " + cfg.baseStat + "%"));
		}
		else
		{
			chiSoHoTro.text = string.Empty;
		}
	}

	public void Set(UserInfo.NguyenKhiData data)
	{
		m_NguyenKhiData = data;
		nguyenKhiAvatar.Set(data);
		Utils.SetLayer(nguyenKhiAvatar.transform, "GUIPopUp", true);
		OtherCfg.NguyenKhiCfg nguyenKhiCfg = ConfigManager.instance.OtherConfig.NguyenKhiConfig[data.Codename];
		nguyenKhiName.text = nguyenKhiCfg.DisplayName;
		if (nguyenKhiCfg.MoTa != null)
		{
			lbDescription.text = nguyenKhiCfg.MoTa.ToString();
		}
		else
		{
			lbDescription.text = string.Empty;
		}
		string text = string.Empty;
		if (nguyenKhiCfg != null)
		{
			if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.DO_DON)
			{
				text = Localization.instance.Get("ChiSoCoBanDoDonLabel");
			}
			else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.MENH)
			{
				text = Localization.instance.Get("ChiSoCoBanMenhLabel");
			}
			else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.KHI)
			{
				text = Localization.instance.Get("ChiSoCoBanKhiLabel");
			}
			else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.KHANG_BAO)
			{
				text = Localization.instance.Get("ChiSoCoBanKhangBaoLabel");
			}
			else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.NGOAI)
			{
				text = Localization.instance.Get("ChiSoCoBanNgoaiLabel");
			}
			else if (nguyenKhiCfg.Loai == OtherCfg.NguyenKhiType.THAN)
			{
				text = Localization.instance.Get("ChiSoCoBanThanLabel");
			}
			chiSoHoTro.text = ((nguyenKhiCfg.Loai != OtherCfg.NguyenKhiType.DO_DON && nguyenKhiCfg.Loai != OtherCfg.NguyenKhiType.KHANG_BAO && nguyenKhiCfg.Loai != OtherCfg.NguyenKhiType.MENH) ? (text + ": " + data.GetNguyenKhiStats()) : (text + ": " + data.GetNguyenKhiStats() + "%"));
		}
		else
		{
			chiSoHoTro.text = string.Empty;
		}
	}

	public void OnDoiClick()
	{
		ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
		List<int> list = new List<int>();
		UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == m_NguyenKhiData.HID);
		if (heroData != null)
		{
			list.Add(heroData.NguyenKhi1ID);
			list.Add(heroData.NguyenKhi2ID);
			list.Add(heroData.NguyenKhi3ID);
			list.Add(heroData.NguyenKhi4ID);
			list.Add(heroData.NguyenKhi5ID);
			list.Add(heroData.NguyenKhi6ID);
		}
		PopupSelectNguyenKhi.Create(screenDoiHinh.NhanVatInfo.OnChangeNguyenKhi, list);
		DestroyPopup();
	}

	public void onCuongHoaClick()
	{
		if (m_NguyenKhiData != null)
		{
			DestroyPopup();
			ScreenCuongHoaNguyenKhi screenCuongHoaNguyenKhi = GUIManager.getScreen(GAME_SCREEN.ScreenCuongHoaNguyenKhi) as ScreenCuongHoaNguyenKhi;
			screenCuongHoaNguyenKhi.Set(m_NguyenKhiData);
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenCuongHoaNguyenKhi);
		}
	}
}

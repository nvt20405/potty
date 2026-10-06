using System.Collections.Generic;
using UnityEngine;

public class PopupVoCong : MonoBehaviour
{
	public OtherAvatar otherAvatar;

	public UILabel VoCongName;

	public UILabel HieuUngLabel;

	public UILabel ThongTinLabel;

	public GameObject DoiHinhBtnGroup;

	public GameObject btnClose;

	public GameObject vcDefaultGrp;

	public GameObject btnThamNgo_DHGrp;

	public GameObject btnDoi_DHGrp;

	public GameObject btnUnlock_DHGrp;

	public GameObject btnThamNgo_VCDefaultGrp;

	public GameObject btnUnlock_VCDefaultGrp;

	public GameObject infoVCGroup;

	public UILabel satThuongLabel;

	public UILabel timeHoiChieuLabel;

	public UILabel noiLabel;

	public UILabel descriptionLabel;

	private UserInfo.VoCongData m_VoCongData;

	public UISprite spPham;

	private bool isVoCongDefault;

	private UserInfo RefUserInfo;

	public static PopupVoCong instance;

	private void Start()
	{
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

	private static void Create(UserInfo ref_user_info, UserInfo.VoCongData data, int level)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupVoCong"))).GetComponent<PopupVoCong>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set(ref_user_info, data, level);
		NGUITools.SetActive(instance.DoiHinhBtnGroup, false);
	}

	private static void CreateByConfigData(CfgVoCong cfg)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupVoCong"))).GetComponent<PopupVoCong>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.RefUserInfo = GameManager.instance.m_GameClient.UserInfo;
		instance.Set(cfg);
		NGUITools.SetActive(instance.DoiHinhBtnGroup, false);
	}

	public void Set(UserInfo ref_user_info, UserInfo.VoCongData data, int level)
	{
		ScreenVoLamPho.SetVoLamPhoStatus(data.Name, 1);
		m_VoCongData = data;
		otherAvatar.Set(data.Name, 0, level, -1, false, false, 0, true);
		CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[data.Name];
		displayInfo(cfgVoCong);
		string mota = cfgVoCong.Mota1;
		if (mota.Contains("{2}"))
		{
			ThongTinLabel.text = string.Format(mota, string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo1CoSo(level)), string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo2CoSo(level)), string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo3CoSo(level)));
		}
		else if (mota.Contains("{1}"))
		{
			ThongTinLabel.text = string.Format(mota, string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo1CoSo(level)), string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo2CoSo(level)));
		}
		else if (mota.Contains("{0}"))
		{
			ThongTinLabel.text = string.Format(mota, string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo1CoSo(level)));
		}
		else
		{
			ThongTinLabel.text = mota;
		}
		if (ref_user_info != null)
		{
			int maxBatQuaiCuongHoa = ConfigManager.instance.OtherConfig.GetMaxBatQuaiCuongHoa();
			if ((ref_user_info.DoiHinh.CuongHoa_Slot1 >= maxBatQuaiCuongHoa || ref_user_info.DoiHinh.CuongHoa_Slot2 >= maxBatQuaiCuongHoa || ref_user_info.DoiHinh.CuongHoa_Slot3 >= maxBatQuaiCuongHoa || ref_user_info.DoiHinh.CuongHoa_Slot4 >= maxBatQuaiCuongHoa || ref_user_info.DoiHinh.CuongHoa_Slot5 >= maxBatQuaiCuongHoa || ref_user_info.DoiHinh.CuongHoa_Slot6 >= maxBatQuaiCuongHoa || ref_user_info.DoiHinh.CuongHoa_Slot7 >= maxBatQuaiCuongHoa || ref_user_info.DoiHinh.CuongHoa_Slot8 >= maxBatQuaiCuongHoa) && cfgVoCong.Hang == 3)
			{
				string text = string.Empty;
				switch (cfgVoCong.m_BatQuaiType)
				{
				case OtherCfg.NguyenKhiType.MENH:
					text = Localization.instance.Get("MenhLabel");
					break;
				case OtherCfg.NguyenKhiType.NGOAI:
					text = Localization.instance.Get("NgoaiLabel");
					break;
				case OtherCfg.NguyenKhiType.THAN:
					text = Localization.instance.Get("ThanLabel");
					break;
				case OtherCfg.NguyenKhiType.KHI:
					text = Localization.instance.Get("KhiLabel");
					break;
				}
				UILabel thongTinLabel = ThongTinLabel;
				string text2 = thongTinLabel.text;
				thongTinLabel.text = text2 + "\n[0000CD]" + string.Format(Localization.instance.Get("MotaInfoBatQuaiInPopUp"), "[FFFF00]" + cfgVoCong.GetBatQuaiChiSo(data.Level)) + "%[-] " + text + ".[-]";
			}
		}
		string mota2 = cfgVoCong.Mota2;
		if (mota2.Contains("{2}"))
		{
			HieuUngLabel.text = string.Format(mota2, string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo1CoSo(level)), string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo2CoSo(level)), string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo3CoSo(level)));
		}
		else if (mota2.Contains("{1}"))
		{
			HieuUngLabel.text = string.Format(mota2, string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo1CoSo(level)), string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo2CoSo(level)));
		}
		else if (mota2.Contains("{0}"))
		{
			HieuUngLabel.text = string.Format(mota2, string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo1CoSo(level)));
		}
		else
		{
			HieuUngLabel.text = mota2;
		}
		satThuongLabel.text = cfgVoCong.GetSatThuong(level).ToString();
		noiLabel.text = Mathf.Floor(cfgVoCong.GetTieuHao(level)).ToString();
	}

	public static void CreateByVoLamPhoScreen(CfgVoCong vcData)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupVoCong"))).GetComponent<PopupVoCong>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.RefUserInfo = GameManager.instance.m_GameClient.UserInfo;
		instance.Set(vcData);
		NGUITools.SetActive(instance.btnClose, true);
		NGUITools.SetActive(instance.vcDefaultGrp, false);
		NGUITools.SetActive(instance.DoiHinhBtnGroup, false);
	}

	public void Set(CfgVoCong cfgVoCong)
	{
		ScreenVoLamPho.SetVoLamPhoStatus(cfgVoCong.Name, 1);
		otherAvatar.Set(cfgVoCong.Name);
		displayInfo(cfgVoCong);
		string mota = cfgVoCong.Mota1;
		if (mota.Contains("{2}"))
		{
			ThongTinLabel.text = string.Format(mota, string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo1CoSo(1)), string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo2CoSo(1)), string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo3CoSo(1)));
		}
		else if (mota.Contains("{1}"))
		{
			ThongTinLabel.text = string.Format(mota, string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo1CoSo(1)), string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo2CoSo(1)));
		}
		else if (mota.Contains("{0}"))
		{
			ThongTinLabel.text = string.Format(mota, string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo1CoSo(1)));
		}
		else
		{
			ThongTinLabel.text = mota;
		}
		if (RefUserInfo != null)
		{
			int maxBatQuaiCuongHoa = ConfigManager.instance.OtherConfig.GetMaxBatQuaiCuongHoa();
			if ((RefUserInfo.DoiHinh.CuongHoa_Slot1 >= maxBatQuaiCuongHoa || RefUserInfo.DoiHinh.CuongHoa_Slot2 >= maxBatQuaiCuongHoa || RefUserInfo.DoiHinh.CuongHoa_Slot3 >= maxBatQuaiCuongHoa || RefUserInfo.DoiHinh.CuongHoa_Slot4 >= maxBatQuaiCuongHoa || RefUserInfo.DoiHinh.CuongHoa_Slot5 >= maxBatQuaiCuongHoa || RefUserInfo.DoiHinh.CuongHoa_Slot6 >= maxBatQuaiCuongHoa || RefUserInfo.DoiHinh.CuongHoa_Slot7 >= maxBatQuaiCuongHoa || RefUserInfo.DoiHinh.CuongHoa_Slot8 >= maxBatQuaiCuongHoa) && cfgVoCong.Hang == 3)
			{
				string text = string.Empty;
				switch (cfgVoCong.m_BatQuaiType)
				{
				case OtherCfg.NguyenKhiType.MENH:
					text = Localization.instance.Get("MenhLabel");
					break;
				case OtherCfg.NguyenKhiType.NGOAI:
					text = Localization.instance.Get("NgoaiLabel");
					break;
				case OtherCfg.NguyenKhiType.THAN:
					text = Localization.instance.Get("ThanLabel");
					break;
				case OtherCfg.NguyenKhiType.KHI:
					text = Localization.instance.Get("KhiLabel");
					break;
				}
				UILabel thongTinLabel = ThongTinLabel;
				string text2 = thongTinLabel.text;
				thongTinLabel.text = text2 + "\n[0000CD]" + string.Format(Localization.instance.Get("MotaInfoBatQuaiInPopUp"), "[FFFF00]" + cfgVoCong.GetBatQuaiChiSo(1)) + "%[-] " + text + ".[-]";
			}
		}
		string mota2 = cfgVoCong.Mota2;
		if (mota2.Contains("{2}"))
		{
			HieuUngLabel.text = string.Format(mota2, string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo1CoSo(1)), string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo2CoSo(1)), string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo3CoSo(1)));
		}
		else if (mota2.Contains("{1}"))
		{
			HieuUngLabel.text = string.Format(mota2, string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo1CoSo(1)), string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo2CoSo(1)));
		}
		else if (mota2.Contains("{0}"))
		{
			HieuUngLabel.text = string.Format(mota2, string.Format("[FF0000]{0}[-]", cfgVoCong.ChiSo1CoSo(1)));
		}
		else
		{
			HieuUngLabel.text = mota2;
		}
		satThuongLabel.text = cfgVoCong.GetSatThuong(1).ToString();
		noiLabel.text = Mathf.Floor(cfgVoCong.GetTieuHao(1)).ToString();
	}

	public void displayInfo(CfgVoCong cfg)
	{
		VoCongName.text = cfg.TenHienThi;
		if (cfg.Hang == 3)
		{
			spPham.spriteName = "giap";
		}
		else if (cfg.Hang == 2)
		{
			spPham.spriteName = "at";
		}
		else if (cfg.Hang == 1)
		{
			spPham.spriteName = "binh";
		}
		if (cfg.m_Class == VCClass.BO_PHAP)
		{
			descriptionLabel.text = Localization.instance.Get("BoPhapLabelBtn");
			infoVCGroup.SetActive(false);
		}
		else if (cfg.m_Class == VCClass.NOI_CONG)
		{
			infoVCGroup.SetActive(false);
			descriptionLabel.text = Localization.instance.Get("NoiCongLabelBtn");
		}
		else if (cfg.m_Class == VCClass.CHIEU_THUC)
		{
			infoVCGroup.SetActive(true);
			string empty = string.Empty;
			empty = Localization.instance.Get("ChieuThucLabelBtn") + ": ";
			if (cfg.CongKichType == VCCongKich.DON_THE)
			{
				descriptionLabel.text = empty + Localization.instance.Get("DonTheVCType");
			}
			else if (cfg.CongKichType == VCCongKich.DA_THE)
			{
				descriptionLabel.text = empty + Localization.instance.Get("DaTheVCType");
			}
			else if (cfg.CongKichType == VCCongKich.PHU_TRO)
			{
				descriptionLabel.text = empty + Localization.instance.Get("PhuTroVCType");
			}
		}
		timeHoiChieuLabel.text = cfg.Cooldown.ToString();
	}

	public static void CreateByNormalScreen(UserInfo ref_user_info, UserInfo.VoCongData data, int level)
	{
		Create(ref_user_info, data, level);
		NGUITools.SetActive(instance.btnClose, true);
		NGUITools.SetActive(instance.vcDefaultGrp, false);
		if (data.ID > 0)
		{
			NGUITools.SetActive(instance.DoiHinhBtnGroup, false);
		}
	}

	public static void CreateByNormalScreen(CfgVoCong data)
	{
		CreateByConfigData(data);
		NGUITools.SetActive(instance.btnClose, true);
		NGUITools.SetActive(instance.vcDefaultGrp, false);
		NGUITools.SetActive(instance.DoiHinhBtnGroup, false);
	}

	public static void CreateByScreenDoiHinh(UserInfo ref_user_info, UserInfo.VoCongData data, bool isDefautVC = false)
	{
		Create(ref_user_info, data, data.Level);
		instance.isVoCongDefault = isDefautVC;
		NGUITools.SetActive(instance.btnClose, false);
		if (isDefautVC)
		{
			NGUITools.SetActive(instance.vcDefaultGrp, true);
		}
		else
		{
			NGUITools.SetActive(instance.vcDefaultGrp, false);
		}
		if (data.ID > 0)
		{
			NGUITools.SetActive(instance.DoiHinhBtnGroup, true);
		}
		if (NGUITools.GetActive(instance.vcDefaultGrp) && data != null)
		{
			if ((data.Unlock == 1 && data.Level <= 10) || data.Level < 9)
			{
				instance.btnUnlock_VCDefaultGrp.SetActive(false);
			}
			else
			{
				instance.btnUnlock_VCDefaultGrp.SetActive(true);
			}
			if ((data.Unlock == 1 && data.Level == 9) || data.Level < 9)
			{
				instance.btnThamNgo_VCDefaultGrp.SetActive(true);
			}
			else
			{
				instance.btnThamNgo_VCDefaultGrp.SetActive(false);
			}
		}
		if (NGUITools.GetActive(instance.DoiHinhBtnGroup) && data != null)
		{
			if ((data.Unlock == 1 && data.Level <= 10) || data.Level < 9)
			{
				instance.btnUnlock_DHGrp.SetActive(false);
			}
			else
			{
				instance.btnUnlock_DHGrp.SetActive(true);
			}
			if (data.Level < 9 || (data.Level == 9 && data.Unlock == 1))
			{
				instance.btnThamNgo_DHGrp.SetActive(true);
			}
			else
			{
				instance.btnThamNgo_DHGrp.SetActive(false);
			}
		}
	}

	public void OnThamNgoClick()
	{
		DestroyPopup();
		if (m_VoCongData != null)
		{
			ScreenThamNgo screenThamNgo = GUIManager.getScreen(GAME_SCREEN.ScreenThamNgo) as ScreenThamNgo;
			if (isVoCongDefault)
			{
				screenThamNgo.Set(m_VoCongData, true);
			}
			else
			{
				screenThamNgo.Set(m_VoCongData);
			}
			GUIManager.instance.SetScreen(GAME_SCREEN.ScreenThamNgo);
		}
	}

	public void OnDoiClick()
	{
		ScreenDoiHinh screenDoiHinh = GUIManager.getScreen(GAME_SCREEN.ScreenDoiHinh) as ScreenDoiHinh;
		List<int> list = new List<int>();
		UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == m_VoCongData.HID);
		if (heroData != null)
		{
			list.Add(heroData.VoCong2ID);
			list.Add(heroData.VoCong3ID);
			list.Add(heroData.VoCong4ID);
		}
		CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[m_VoCongData.Name];
		PopupSelectVoCong.Create(screenDoiHinh.NhanVatInfo.OnChangeVoCong, list, cfgVoCong.m_Class);
		DestroyPopup();
	}

	public void OnUnLockClick()
	{
		if (m_VoCongData == null)
		{
			return;
		}
		if (m_VoCongData.Level != 9)
		{
			MessagePopup.Create(Localization.instance.Get("ThongbaoChuaDuDieuKienMoKhoa"));
			return;
		}
		DestroyPopup();
		ScreenMoKhoa screenMoKhoa = GUIManager.getScreen(GAME_SCREEN.ScreenMoKhoa) as ScreenMoKhoa;
		if (isVoCongDefault)
		{
			screenMoKhoa.Set(m_VoCongData, true);
		}
		else
		{
			screenMoKhoa.Set(m_VoCongData);
		}
		GUIManager.instance.SetScreen(GAME_SCREEN.ScreenMoKhoa);
	}
}

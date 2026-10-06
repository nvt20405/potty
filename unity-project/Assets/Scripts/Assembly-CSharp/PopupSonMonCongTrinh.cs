using System.Collections.Generic;
using UnityEngine;

public class PopupSonMonCongTrinh : MonoBehaviour
{
	public enum State
	{
		BUILDING = 0,
		UPGRADE = 1,
		INFO = 2
	}

	public static PopupSonMonCongTrinh instance;

	public UILabel TenCongTrinhLabel;

	public UILabel CapLabel;

	public UILabel DescLabel;

	public UILabel CurDescLabel;

	public UILabel NexDescLabel;

	public UILabel UpgradeLabel;

	public GameObject NextRequireGrp;

	public GameObject TimeLeft;

	public UILabel TimeLeftLabel;

	public UISlider TimeLeftSlider;

	public UILabel SucChuaLabel;

	public UISprite AvatarSprite;

	public GameObject AllGrp;

	private UserInfo.SonMonBuildingInfo CurCongTrinh;

	public static void Create(State state, UserInfo.SonMonBuildingInfo congTrinh)
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
		}
		instance = ((GameObject)Object.Instantiate(Resources.Load("popup/PopupSonMonCongTrinh"))).GetComponent<PopupSonMonCongTrinh>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = Vector3.one;
		instance.CurCongTrinh = congTrinh;
		instance.Set(state, congTrinh);
	}

	public void Set(State state, UserInfo.SonMonBuildingInfo congTrinh)
	{
		if (state != State.INFO)
		{
			return;
		}
		if (congTrinh.BuildTime > GameManager.instance.m_GameClient.ServerTime)
		{
			TimeLeftLabel.text = (congTrinh.BuildTime - GameManager.instance.m_GameClient.ServerTime).ToString();
			TimeLeftSlider.sliderValue = (float)(congTrinh.BuildTime - GameManager.instance.m_GameClient.ServerTime).TotalSeconds / (float)ConfigManager.instance.SonMonConfig.CongTrinhCfg[congTrinh.LoaiCongTrinh.ToString()][congTrinh.Level].BuildTime;
			TimeLeft.SetActive(true);
			NextRequireGrp.SetActive(false);
		}
		else
		{
			TimeLeft.SetActive(false);
			NextRequireGrp.SetActive(true);
		}
		TenCongTrinhLabel.text = ConfigManager.instance.SonMonConfig.CongTrinhCfg[congTrinh.LoaiCongTrinh.ToString()][0].DisplayName;
		CapLabel.text = string.Format(Localization.instance.Get("ThanhTuuLevelLabel"), congTrinh.Level);
		DescLabel.text = ConfigManager.instance.SonMonConfig.Description[(int)congTrinh.LoaiCongTrinh];
		AvatarSprite.spriteName = congTrinh.LoaiCongTrinh.ToString();
		if (congTrinh.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.CHINH_SANH)
		{
			long num = ConfigManager.instance.SonMonConfig.CongTrinhCfg[congTrinh.LoaiCongTrinh.ToString()][congTrinh.Level - 1].Storage;
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_GO");
			if (vatPhamTieuThuData == null)
			{
				vatPhamTieuThuData = new UserInfo.VatPhamTieuThuData();
			}
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData2 = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData vp) => vp.Name == "VP_DA");
			if (vatPhamTieuThuData2 == null)
			{
				vatPhamTieuThuData2 = new UserInfo.VatPhamTieuThuData();
			}
			CurDescLabel.text = string.Format(Localization.instance.Get("SonMonDescChinhSanh"), vatPhamTieuThuData.Quantity, num, vatPhamTieuThuData2.Quantity, num, Localization.instance.Get("SonMonCurLevel"));
			if (congTrinh.Level < ConfigManager.instance.SonMonConfig.CongTrinhCfg[congTrinh.LoaiCongTrinh.ToString()].Count)
			{
				num = ConfigManager.instance.SonMonConfig.CongTrinhCfg[congTrinh.LoaiCongTrinh.ToString()][congTrinh.Level].Storage;
			}
			NexDescLabel.text = string.Format(Localization.instance.Get("SonMonDescChinhSanh"), vatPhamTieuThuData.Quantity, num, vatPhamTieuThuData2.Quantity, num, Localization.instance.Get("SonMonNextLevel"));
			SucChuaLabel.text = string.Empty;
		}
		else if (congTrinh.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.NHA_BEP || congTrinh.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.XUONG_DA || congTrinh.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.XUONG_GO || congTrinh.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.VUON_THUOC || congTrinh.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.TIEN_TRANG)
		{
			long num2 = ConfigManager.instance.SonMonConfig.CongTrinhCfg[congTrinh.LoaiCongTrinh.ToString()][congTrinh.Level - 1].Storage;
			long num3 = (long)ConfigManager.instance.SonMonConfig.CongTrinhCfg[congTrinh.LoaiCongTrinh.ToString()][congTrinh.Level - 1].Production;
			CurDescLabel.text = Localization.instance.Get("SonMonCurLevel");
			if (congTrinh.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.NHA_BEP)
			{
				CurDescLabel.text += string.Format(Localization.instance.Get("SonMonDescTaiNguyen"), Localization.instance.Get("TheLucLabel"), num2, num3);
			}
			else if (congTrinh.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.XUONG_DA)
			{
				CurDescLabel.text += string.Format(Localization.instance.Get("SonMonDescTaiNguyen"), "##DA", num2, num3);
			}
			else if (congTrinh.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.XUONG_GO)
			{
				CurDescLabel.text += string.Format(Localization.instance.Get("SonMonDescTaiNguyen"), "##GO", num2, num3);
			}
			else if (congTrinh.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.VUON_THUOC)
			{
				CurDescLabel.text += string.Format(Localization.instance.Get("SonMonDescTaiNguyen"), ConfigManager.instance.m_dicVatPhamTieuThu["VP_NGUYEN_KHI_DAN"].TenHienThi, num2, num3);
			}
			else
			{
				CurDescLabel.text += string.Format(Localization.instance.Get("SonMonDescTaiNguyen"), Localization.instance.Get("Bac"), num2, num3);
			}
			if (congTrinh.Level < ConfigManager.instance.SonMonConfig.CongTrinhCfg[congTrinh.LoaiCongTrinh.ToString()].Count)
			{
				num2 = ConfigManager.instance.SonMonConfig.CongTrinhCfg[congTrinh.LoaiCongTrinh.ToString()][congTrinh.Level].Storage;
				num3 = (long)ConfigManager.instance.SonMonConfig.CongTrinhCfg[congTrinh.LoaiCongTrinh.ToString()][congTrinh.Level].Production;
			}
			NexDescLabel.text = Localization.instance.Get("SonMonNextLevel");
			if (congTrinh.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.NHA_BEP)
			{
				NexDescLabel.text += string.Format(Localization.instance.Get("SonMonDescTaiNguyen"), Localization.instance.Get("TheLucLabel"), num2, num3);
			}
			else if (congTrinh.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.XUONG_DA)
			{
				NexDescLabel.text += string.Format(Localization.instance.Get("SonMonDescTaiNguyen"), "##DA", num2, num3);
			}
			else if (congTrinh.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.XUONG_GO)
			{
				NexDescLabel.text += string.Format(Localization.instance.Get("SonMonDescTaiNguyen"), "##GO", num2, num3);
			}
			else if (congTrinh.LoaiCongTrinh == UserInfo.SonMonBuildingInfo.SonMonBuildingType.VUON_THUOC)
			{
				NexDescLabel.text += string.Format(Localization.instance.Get("SonMonDescTaiNguyen"), ConfigManager.instance.m_dicVatPhamTieuThu["VP_NGUYEN_KHI_DAN"].TenHienThi, num2, num3);
			}
			else
			{
				NexDescLabel.text += string.Format(Localization.instance.Get("SonMonDescTaiNguyen"), Localization.instance.Get("Bac"), num2, num3);
			}
			SucChuaLabel.text = string.Format(Localization.instance.Get("SucChuaSonMon"), SonMonHelper.GetSonMonCTResource(congTrinh) * 100 / ConfigManager.instance.SonMonConfig.CongTrinhCfg[congTrinh.LoaiCongTrinh.ToString()][congTrinh.Level - 1].Storage);
		}
		UpgradeLabel.text = string.Empty;
		if (congTrinh.Level >= ConfigManager.instance.SonMonConfig.CongTrinhCfg[congTrinh.LoaiCongTrinh.ToString()].Count)
		{
			return;
		}
		foreach (KeyValuePair<string, int> item in ConfigManager.instance.SonMonConfig.CongTrinhCfg[congTrinh.LoaiCongTrinh.ToString()][congTrinh.Level].BuildCost)
		{
			if (item.Key.Equals("VP_GO"))
			{
				UILabel upgradeLabel = UpgradeLabel;
				upgradeLabel.text = upgradeLabel.text + " ##GO " + item.Value;
			}
			else if (item.Key.Equals("VP_DA"))
			{
				UILabel upgradeLabel2 = UpgradeLabel;
				upgradeLabel2.text = upgradeLabel2.text + " ##DA " + item.Value;
			}
			else if (item.Key.StartsWith("VP_"))
			{
				UILabel upgradeLabel3 = UpgradeLabel;
				string text = upgradeLabel3.text;
				upgradeLabel3.text = text + "\n" + item.Value + " x " + ConfigManager.instance.m_dicVatPhamTieuThu[item.Key].TenHienThi;
			}
			else if (item.Key == "BAC")
			{
				UILabel upgradeLabel4 = UpgradeLabel;
				string text2 = upgradeLabel4.text;
				upgradeLabel4.text = text2 + "\n" + item.Value + " x " + Localization.instance.Get("Bac");
			}
			else if (item.Key == "CHINH_SANH")
			{
				UILabel upgradeLabel5 = UpgradeLabel;
				upgradeLabel5.text = upgradeLabel5.text + "\n" + ConfigManager.instance.SonMonConfig.CongTrinhCfg[item.Key][0].DisplayName + string.Format(Localization.instance.Get("Cấp"), item.Value);
			}
		}
	}

	public void XayDungCongTrinh()
	{
		GameManager.instance.m_GameClient.RequestXayDungSonMon(CurCongTrinh.ID);
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public void XayDungNhanhCongTrinh()
	{
		PopupYesNo.Create(string.Format(Localization.instance.Get("ConfirmXayNhanh"), SonMonHelper.GetKNBXayNhanH(CurCongTrinh)), Localization.instance.Get("DongYLabelBtn"), Localization.instance.Get("TuChoiLabelBtn"), ConfirmXayNhanh, Return);
		AllGrp.SetActive(false);
	}

	public void ConfirmXayNhanh()
	{
		GameManager.instance.m_GameClient.RequestFastForwardSonMon(CurCongTrinh.ID);
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public void Return()
	{
		AllGrp.SetActive(true);
	}

	public void Close()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	private void Update()
	{
		if (CurCongTrinh == null)
		{
			return;
		}
		if (CurCongTrinh.BuildTime > GameManager.instance.m_GameClient.ServerTime)
		{
			TimeLeftLabel.text = SonMonHelper.GenTimeString(CurCongTrinh.BuildTime - GameManager.instance.m_GameClient.ServerTime);
			TimeLeftSlider.sliderValue = (float)(CurCongTrinh.BuildTime - GameManager.instance.m_GameClient.ServerTime).TotalSeconds / (float)ConfigManager.instance.SonMonConfig.CongTrinhCfg[CurCongTrinh.LoaiCongTrinh.ToString()][CurCongTrinh.Level].BuildTime;
			TimeLeft.SetActive(true);
			NextRequireGrp.SetActive(false);
			return;
		}
		TimeLeft.SetActive(false);
		NextRequireGrp.SetActive(true);
		if (CurCongTrinh.Level < CurCongTrinh.NextLevel)
		{
			CurCongTrinh.Level = CurCongTrinh.NextLevel;
			Set(State.INFO, CurCongTrinh);
		}
	}
}

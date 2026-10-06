using UnityEngine;

public class PopupSelectVoCongItem : MonoBehaviour
{
	public OtherAvatar avatar;

	public UILabel voCongName;

	public UILabel heroSuDungName;

	public UICheckbox checkBox;

	public UILabel typeVoCong;

	public UISprite spPham;

	public UserInfo.VoCongData m_Data;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Set(UserInfo.VoCongData data, bool isSelectFromBatQuai = false)
	{
		m_Data = data;
		avatar.Set(data);
		Utils.SetLayer(avatar.gameObject.transform, "GUIPopUp", true);
		CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[data.Name];
		voCongName.text = cfgVoCong.TenHienThi;
		if (cfgVoCong.Hang == 3)
		{
			spPham.spriteName = "giap";
		}
		else if (cfgVoCong.Hang == 2)
		{
			spPham.spriteName = "at";
		}
		else if (cfgVoCong.Hang == 1)
		{
			spPham.spriteName = "binh";
		}
		if (data.HID > 0)
		{
			UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == data.HID);
			NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[heroData.Name];
			heroSuDungName.text = nhanVatCfg.TenHienThi + " " + Localization.instance.Get("UsedLabel");
		}
		else
		{
			heroSuDungName.text = string.Empty;
		}
		if (isSelectFromBatQuai)
		{
			if (cfgVoCong.Hang == 3)
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
				if (text != string.Empty)
				{
					typeVoCong.text = text + " +" + cfgVoCong.GetBatQuaiChiSo(m_Data.Level) + "%";
				}
				else
				{
					typeVoCong.text = string.Empty;
				}
			}
			else
			{
				typeVoCong.text = string.Empty;
			}
		}
		else if (cfgVoCong.m_Class == VCClass.BO_PHAP)
		{
			typeVoCong.text = Localization.instance.Get("BoPhapLabelBtn");
		}
		else if (cfgVoCong.m_Class == VCClass.NOI_CONG)
		{
			typeVoCong.text = Localization.instance.Get("NoiCongLabelBtn");
		}
		else if (cfgVoCong.m_Class == VCClass.CHIEU_THUC)
		{
			if (cfgVoCong.CongKichType == VCCongKich.DON_THE)
			{
				typeVoCong.text = Localization.instance.Get("ChieuThucLabelBtn") + ": " + Localization.instance.Get("DonTheVCType");
			}
			else if (cfgVoCong.CongKichType == VCCongKich.DA_THE)
			{
				typeVoCong.text = Localization.instance.Get("ChieuThucLabelBtn") + ": " + Localization.instance.Get("DaTheVCType");
			}
			else if (cfgVoCong.CongKichType == VCCongKich.PHU_TRO)
			{
				typeVoCong.text = Localization.instance.Get("ChieuThucLabelBtn") + ": " + Localization.instance.Get("PhuTroVCType");
			}
		}
		checkBox.radioButtonRoot = base.transform.parent;
		checkBox.optionCanBeNone = true;
	}
}

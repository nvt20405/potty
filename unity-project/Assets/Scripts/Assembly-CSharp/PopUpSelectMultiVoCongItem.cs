using UnityEngine;

public class PopUpSelectMultiVoCongItem : MonoBehaviour
{
	public OtherAvatar avatar;

	public UILabel voCongName;

	public UILabel heroSuDungName;

	public UICheckbox checkBox;

	public UISprite spPham;

	public UILabel typeVoCong;

	public UserInfo.VoCongData m_Data;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Set(UserInfo.VoCongData data)
	{
		m_Data = data;
		avatar.Set(data);
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
		if (cfgVoCong.m_Class == VCClass.BO_PHAP)
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
	}
}

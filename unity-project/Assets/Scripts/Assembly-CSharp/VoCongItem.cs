using UnityEngine;

public class VoCongItem : MonoBehaviour
{
	public OtherAvatar avatar;

	public UILabel voCongName;

	public UILabel noteLabel;

	public GameObject focusItem;

	public UISprite bgFocusItem;

	public UIButton btnDotPha;

	public UIButton btnThamNgo;

	public UIButton btnMoKhoa;

	public UILabel satThuongLabel;

	public UILabel timeHoiChieuLabel;

	public UILabel noiLabel;

	public GameObject infoVCGroup;

	public int VoCongID;

	public UISprite spPham;

	public UserInfo.VoCongData m_Data;

	public void SetForVoCongTab(UserInfo.VoCongData data)
	{
		if (data == null)
		{
			return;
		}
		m_Data = data;
		avatar.Set(m_Data);
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
		if (((!data.Name.EndsWith("_S") && cfgVoCong.Hang == 2) || (!data.Name.EndsWith("_SS") && cfgVoCong.Hang == 3)) && (cfgVoCong.m_Class == VCClass.BO_PHAP || cfgVoCong.m_Class == VCClass.NOI_CONG))
		{
			btnDotPha.gameObject.SetActive(true);
		}
		else
		{
			btnDotPha.gameObject.SetActive(false);
		}
		if (data.Level == 9 && data.Unlock == 0)
		{
			btnMoKhoa.gameObject.SetActive(true);
		}
		else
		{
			btnMoKhoa.gameObject.SetActive(false);
		}
		if ((data.Level == 9 && data.Unlock == 0) || data.Level == 10)
		{
			btnThamNgo.gameObject.SetActive(false);
		}
		else
		{
			btnThamNgo.gameObject.SetActive(true);
		}
		if (m_Data.HID > 0)
		{
			UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == data.HID);
			NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[heroData.Name];
			noteLabel.text = nhanVatCfg.TenHienThi + " " + Localization.instance.Get("UsedLabel");
		}
		else
		{
			noteLabel.text = string.Empty;
		}
		if (cfgVoCong.m_Class == VCClass.CHIEU_THUC)
		{
			infoVCGroup.SetActive(true);
			satThuongLabel.text = cfgVoCong.GetSatThuong(data.Level).ToString();
			timeHoiChieuLabel.text = cfgVoCong.Cooldown.ToString();
			noiLabel.text = Mathf.Floor(cfgVoCong.GetTieuHao(data.Level)).ToString();
		}
		else
		{
			infoVCGroup.SetActive(false);
		}
		noteLabel.transform.localPosition = new Vector3(noteLabel.transform.localPosition.x, -25f, noteLabel.transform.localPosition.z);
		infoVCGroup.transform.localPosition = new Vector3(infoVCGroup.transform.localPosition.x, -40f, infoVCGroup.transform.localPosition.z);
		if (m_Data.HID > 0 && cfgVoCong.m_Class == VCClass.CHIEU_THUC)
		{
			noteLabel.transform.localPosition = new Vector3(noteLabel.transform.localPosition.x, noteLabel.transform.localPosition.y + 20f, noteLabel.transform.localPosition.z);
			infoVCGroup.transform.localPosition = new Vector3(infoVCGroup.transform.localPosition.x, infoVCGroup.transform.localPosition.y - 20f, infoVCGroup.transform.localPosition.z);
		}
		else
		{
			noteLabel.transform.localPosition = new Vector3(noteLabel.transform.localPosition.x, -25f, noteLabel.transform.localPosition.z);
			infoVCGroup.transform.localPosition = new Vector3(infoVCGroup.transform.localPosition.x, -40f, infoVCGroup.transform.localPosition.z);
		}
	}

	public void setForTanHonTab()
	{
	}

	public void avatar_OnClick()
	{
		if (m_Data != null)
		{
			PopupVoCong.CreateByNormalScreen(GameManager.instance.m_GameClient.UserInfo, m_Data, m_Data.Level);
		}
	}
}

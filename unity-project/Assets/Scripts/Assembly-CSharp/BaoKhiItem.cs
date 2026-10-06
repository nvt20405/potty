using UnityEngine;

public class BaoKhiItem : MonoBehaviour
{
	public OtherAvatar avatar1;

	public OtherAvatar avatar2;

	public OtherAvatar avatar3;

	public UILabel lbInfo;

	public UserInfo.ThienMaLenhInfo m_Data;

	public void setDataItem(UserInfo.ThienMaLenhInfo data)
	{
		m_Data = data;
		avatar1.SetBaoKhiAvatar(data.Slot1);
		avatar2.SetBaoKhiAvatar(data.Slot2);
		avatar3.SetBaoKhiAvatar(data.Slot3);
		Utils.SetLayer(avatar1.gameObject.transform, "Default", true);
		Utils.SetLayer(avatar2.gameObject.transform, "Default", true);
		Utils.SetLayer(avatar3.gameObject.transform, "Default", true);
		if (!string.IsNullOrEmpty(data.Slot1))
		{
			CfgVoCong cfgVoCong = ConfigManager.instance.m_dicVCs[data.Slot1];
			string empty = string.Empty;
			empty = string.Format(Localization.instance.Get("BaoKhiInfo"), cfgVoCong.TenHienThi, data.GetTotalValue());
			if (data.HID > 0)
			{
				UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == data.HID);
				NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[heroData.Name];
				string text = empty;
				empty = text + "\n" + nhanVatCfg.TenHienThi + " " + Localization.instance.Get("UsedLabel");
			}
			lbInfo.text = empty;
		}
		else
		{
			lbInfo.text = string.Empty;
		}
	}
}

using UnityEngine;

public class BanTrangBiItem : MonoBehaviour
{
	public OtherAvatar avatar;

	public UILabel trangBiName;

	public UICheckbox checkBox;

	public UserInfo.TrangBiData m_Data;

	public UISprite spPham;

	public void Set(UserInfo.TrangBiData data)
	{
		m_Data = data;
		avatar.Set(data);
		TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[data.Name];
		trangBiName.text = trangBiCfg.TenHienThi;
		if (trangBiCfg.Hang == ItemClass.Giap)
		{
			spPham.spriteName = "giap";
		}
		else if (trangBiCfg.Hang == ItemClass.At)
		{
			spPham.spriteName = "at";
		}
		else if (trangBiCfg.Hang == ItemClass.Binh)
		{
			spPham.spriteName = "binh";
		}
	}
}

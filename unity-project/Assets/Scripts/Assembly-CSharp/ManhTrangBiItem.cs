using UnityEngine;

public class ManhTrangBiItem : MonoBehaviour
{
	public OtherAvatar avatar;

	public UILabel manhTrangBiName;

	public UILabel descriptionLabel;

	public UILabel soLuongValueLabel;

	public UIButton btnGhep;

	public UILabel statusLabel;

	public int ManhTrangBiID;

	public UserInfo.ManhTrangBiData m_Data;

	public UISprite spPham;

	public void SetManhTBData(UserInfo.ManhTrangBiData data)
	{
		m_Data = data;
		if (data == null)
		{
			return;
		}
		string text = m_Data.Name;
		if (!text.StartsWith("M") || !text.StartsWith("MMU"))
		{
			text = "M" + text;
		}
		EGDebug.Log("code name: " + text);
		avatar.Set(text);
		string text2 = data.Name;
		if (text2.StartsWith("MMAG_"))
		{
			text2 = text2.Substring(1);
		}
		if (text2.StartsWith("MVK_") || text2.StartsWith("MMU_") || text2.StartsWith("MAG_") || text2.StartsWith("MTS_"))
		{
			text2 = text2.Substring(1);
		}
		TrangBiCfg value = null;
		if (!ConfigManager.instance.m_dicTrangBi.TryGetValue(text2, out value))
		{
			ConfigManager.instance.m_dicTrangBi.TryGetValue(data.Name, out value);
		}
		if (value != null)
		{
			manhTrangBiName.text = value.TenHienThi;
			descriptionLabel.text = value.MoTa;
			if (value.Hang == ItemClass.Giap)
			{
				spPham.spriteName = "giap";
			}
			else if (value.Hang == ItemClass.At)
			{
				spPham.spriteName = "at";
			}
			else if (value.Hang == ItemClass.Binh)
			{
				spPham.spriteName = "binh";
			}
		}
		else
		{
			manhTrangBiName.text = data.Name;
			descriptionLabel.text = string.Empty;
		}
		int quantity = data.Quantity;
		int manhTrangBiCanDeGhep = ConfigManager.instance.GetManhTrangBiCanDeGhep(data.Name);
		soLuongValueLabel.text = quantity + "/" + manhTrangBiCanDeGhep;
		if (quantity >= manhTrangBiCanDeGhep)
		{
			statusLabel.text = string.Empty;
			btnGhep.gameObject.SetActive(true);
		}
		else
		{
			statusLabel.text = Localization.instance.Get("TanHonChuaDu");
			btnGhep.gameObject.SetActive(false);
		}
	}

	public void OnAvatarClick()
	{
		if (m_Data != null)
		{
			string text = m_Data.Name;
			if (text.StartsWith("MMAG_"))
			{
				text = text.Substring(1);
			}
			if (text.StartsWith("MVK_") || text.StartsWith("MMU_") || text.StartsWith("MAG_") || text.StartsWith("MTS_"))
			{
				text = text.Substring(1);
			}
			TrangBiCfg value = null;
			if (!ConfigManager.instance.m_dicTrangBi.TryGetValue(text, out value))
			{
				ConfigManager.instance.m_dicTrangBi.TryGetValue(m_Data.Name, out value);
			}
			if (value != null)
			{
				PopupTrangBi.CreateByNormalScreen(value);
			}
		}
	}
}

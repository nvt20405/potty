using UnityEngine;

public class TanChuongItem : MonoBehaviour
{
	public OtherAvatar avatar;

	public UILabel tanChuongName;

	public UILabel descriptionLabel;

	public UILabel soLuongValueLabel;

	public UIButton btnGhep;

	public UIButton btnGhep10Lan;

	public UILabel statusLabel;

	public int TanChuongID;

	public UISprite spPham;

	public UserInfo.ManhVoCongData m_Data;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetTanChuongData(UserInfo.ManhVoCongData data)
	{
		m_Data = data;
		string text = m_Data.Name;
		if (!text.StartsWith("M"))
		{
			text = "M" + text;
		}
		avatar.Set(text);
		string text2 = data.Name;
		if (text2.StartsWith("MVC_"))
		{
			text2 = "VC_" + text2.Substring(4);
		}
		CfgVoCong value = null;
		if (!ConfigManager.instance.m_dicVCs.TryGetValue(text2, out value))
		{
			ConfigManager.instance.m_dicVCs.TryGetValue(data.Name, out value);
		}
		if (value != null)
		{
			tanChuongName.text = value.TenHienThi;
			if (value.Hang == 3)
			{
				spPham.spriteName = "giap";
			}
			else if (value.Hang == 2)
			{
				spPham.spriteName = "at";
			}
			else if (value.Hang == 1)
			{
				spPham.spriteName = "binh";
			}
			string mota = value.Mota1;
			if (mota.Contains("{2}"))
			{
				descriptionLabel.text = string.Format(mota, string.Format("[FF0000]{0}[-]", value.ChiSo1CoSo(1)), string.Format("[FF0000]{0}[-]", value.ChiSo2CoSo(1)), string.Format("[FF0000]{0}[-]", value.ChiSo3CoSo(1)));
			}
			else if (mota.Contains("{1}"))
			{
				descriptionLabel.text = string.Format(mota, string.Format("[FF0000]{0}[-]", value.ChiSo1CoSo(1)), string.Format("[FF0000]{0}[-]", value.ChiSo2CoSo(1)));
			}
			else if (mota.Contains("{0}"))
			{
				descriptionLabel.text = string.Format(mota, string.Format("[FF0000]{0}[-]", value.ChiSo1CoSo(1)));
			}
			else
			{
				descriptionLabel.text = mota;
			}
		}
		else
		{
			tanChuongName.text = data.Name;
			descriptionLabel.text = string.Empty;
		}
		int quantity = data.Quantity;
		int manhVoCongCanDeGhep = ConfigManager.instance.GetManhVoCongCanDeGhep(data.Name);
		soLuongValueLabel.text = quantity + "/" + manhVoCongCanDeGhep;
		if (quantity >= manhVoCongCanDeGhep)
		{
			statusLabel.text = string.Empty;
			btnGhep.gameObject.SetActive(true);
		}
		else
		{
			statusLabel.text = Localization.instance.Get("TanHonChuaDu");
			btnGhep.gameObject.SetActive(false);
		}
		if (quantity >= manhVoCongCanDeGhep * 10)
		{
			btnGhep10Lan.gameObject.SetActive(true);
		}
		else
		{
			btnGhep10Lan.gameObject.SetActive(false);
		}
	}

	public void OnAvatarClick()
	{
		if (m_Data != null)
		{
			string text = m_Data.Name;
			if (text.StartsWith("MVC_"))
			{
				text = "VC_" + text.Substring(4);
			}
			CfgVoCong value = null;
			if (!ConfigManager.instance.m_dicVCs.TryGetValue(text, out value))
			{
				ConfigManager.instance.m_dicVCs.TryGetValue(m_Data.Name, out value);
			}
			if (value != null)
			{
				PopupVoCong.CreateByVoLamPhoScreen(value);
			}
		}
	}
}

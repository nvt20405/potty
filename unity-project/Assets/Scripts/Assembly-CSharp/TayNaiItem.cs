using UnityEngine;

public class TayNaiItem : MonoBehaviour
{
	public OtherAvatar avatar;

	public UILabel tayNaiName;

	public UILabel descriptionLabel;

	public UILabel soLuongLabel;

	public UILabel soLuongValueLabel;

	public UIButton btnUse;

	public UIButton btnUse10Lan;

	public UIButton btnUse100Lan;

	public int TayNaiID;

	public UISprite bgFocusItem;

	public UserInfo.VatPhamTieuThuData m_Data;

	public void SetTayNaiData(UserInfo.VatPhamTieuThuData data)
	{
		if (data == null)
		{
			return;
		}
		m_Data = data;
		avatar.Set(m_Data.Name);
		VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu[data.Name];
		if (vatPhamTieuThuCfg != null)
		{
			tayNaiName.text = vatPhamTieuThuCfg.TenHienThi;
			descriptionLabel.text = vatPhamTieuThuCfg.MoTa;
			if (!m_Data.Name.EndsWith("_KEY"))
			{
				btnUse.gameObject.SetActive(true);
				btnUse10Lan.gameObject.SetActive(true);
			}
			else
			{
				btnUse.gameObject.SetActive(false);
				btnUse10Lan.gameObject.SetActive(false);
			}
			if (m_Data.Name == "VP_HOA_THAN_DAN")
			{
				btnUse100Lan.gameObject.SetActive(true);
			}
			else
			{
				btnUse100Lan.gameObject.SetActive(false);
			}
		}
		soLuongLabel.text = Localization.instance.Get("HienCoLabel");
		soLuongValueLabel.text = data.Quantity.ToString();
	}

	public void OnAvatarClick()
	{
		PopUpVatPham.Create(m_Data);
	}
}

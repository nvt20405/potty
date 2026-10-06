using System.Collections.Generic;
using UnityEngine;

public class TrangBiItem : MonoBehaviour
{
	public OtherAvatar avatar;

	public UILabel nhanVatName;

	public UILabel noteLabel;

	public UISprite spChiSo;

	public UILabel lbChiSo;

	public UISprite spChiSo2;

	public UILabel lbChiSo2;

	public GameObject focusItem;

	public UISprite bgFocusItem;

	public UIButton btnTinhLuyen;

	public UIButton btnCuongHoa;

	public UIButton btnHoangKim;

	public int TrangBiID;

	public UISprite spPham;

	public UserInfo.TrangBiData m_Data;

	public UIButton btnKhamNam;

	public void SetForTrangBiTab(UserInfo.TrangBiData data)
	{
		if (data == null)
		{
			return;
		}
		m_Data = data;
		avatar.SetTrangBiAvatar(m_Data);
		TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[data.Name];
		nhanVatName.text = trangBiCfg.TenHienThi;
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
		if (data.HID > 0)
		{
			UserInfo.HeroData heroData = GameManager.instance.m_GameClient.UserInfo.HeroList.Find((UserInfo.HeroData e) => e.HID == data.HID);
			NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[heroData.Name];
			noteLabel.text = nhanVatCfg.TenHienThi + " " + Localization.instance.Get("UsedLabel");
		}
		else
		{
			noteLabel.text = string.Empty;
		}
		spChiSo.gameObject.SetActive(false);
		spChiSo2.gameObject.SetActive(false);
		UILabel uILabel = lbChiSo;
		string empty = string.Empty;
		lbChiSo2.text = empty;
		uILabel.text = empty;
		List<int> chiSo = trangBiCfg.GetChiSo(data);
		if (chiSo != null && chiSo.Count == 4)
		{
			bool flag = false;
			for (int num = 0; num < chiSo.Count; num++)
			{
				if (num == 0 && chiSo[0] > 0)
				{
					spChiSo.gameObject.SetActive(true);
					spChiSo.spriteName = "icon_menh";
					lbChiSo.text = chiSo[0].ToString();
					flag = true;
				}
				if (num == 1 && chiSo[1] > 0)
				{
					if (flag)
					{
						spChiSo2.gameObject.SetActive(true);
						spChiSo2.spriteName = "icon_ngoai";
						lbChiSo2.text = chiSo[1].ToString();
					}
					else
					{
						spChiSo.gameObject.SetActive(true);
						spChiSo.spriteName = "icon_ngoai";
						lbChiSo.text = chiSo[1].ToString();
						flag = true;
					}
				}
				if (num == 2 && chiSo[2] > 0)
				{
					if (flag)
					{
						spChiSo2.gameObject.SetActive(true);
						spChiSo2.spriteName = "icon_than";
						lbChiSo2.text = chiSo[2].ToString();
					}
					else
					{
						spChiSo.gameObject.SetActive(true);
						spChiSo.spriteName = "icon_than";
						lbChiSo.text = chiSo[2].ToString();
						flag = true;
					}
				}
				if (num == 3 && chiSo[3] > 0)
				{
					if (flag)
					{
						spChiSo2.gameObject.SetActive(true);
						spChiSo2.spriteName = "icon_noi_goc";
						lbChiSo2.text = chiSo[3].ToString();
					}
					else
					{
						spChiSo.gameObject.SetActive(true);
						spChiSo.spriteName = "icon_noi_goc";
						lbChiSo.text = chiSo[3].ToString();
						flag = true;
					}
				}
			}
		}
		btnHoangKim.gameObject.SetActive(false);
		if (trangBiCfg.Hang == ItemClass.Giap)
		{
			if (m_Data.HoangKim == 0)
			{
				btnHoangKim.gameObject.SetActive(true);
			}
			if (m_Data.HoangKim < 3)
			{
				btnHoangKim.gameObject.SetActive(true);
			}
		}
	}

	public void avatar_OnClick()
	{
		if (m_Data != null)
		{
			TrangBiCfg trangBiCfg = ConfigManager.instance.m_dicTrangBi[m_Data.Name];
			PopupTrangBi.CreateByScreenTrangBi(m_Data);
		}
	}
}

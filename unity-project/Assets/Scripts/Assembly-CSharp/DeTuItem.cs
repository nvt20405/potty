using UnityEngine;

public class DeTuItem : MonoBehaviour
{
	public NhanVatAvatar avatar;

	public UILabel nhanVatName;

	public UILabel noteLabel;

	public UILabel tiemLucUongRuouLabel;

	public UILabel messUongRuouLabel;

	public GameObject focusItem;

	public UISprite bgFocusItem;

	public UIButton btnTruyenCong;

	public UIButton btnTuLuyen;

	public UIButton btnBoiDuong;

	public UIButton btnChuyenSinh;

	public UISprite spPham;

	public UserInfo.HeroData m_Data;

	public int DeTuID;

	public void SetForDeTuTab(UserInfo.HeroData data)
	{
		if (data == null)
		{
			return;
		}
		m_Data = data;
		avatar.Set(m_Data);
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[data.Name];
		nhanVatName.text = nhanVatCfg.TenHienThi;
		if (nhanVatCfg.Hang == 3)
		{
			spPham.spriteName = "giap";
		}
		else if (nhanVatCfg.Hang == 2)
		{
			spPham.spriteName = "at";
		}
		else if (nhanVatCfg.Hang == 1)
		{
			spPham.spriteName = "binh";
		}
		if (noteLabel != null)
		{
			if (GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran.Contains(data.HID))
			{
				noteLabel.text = Localization.instance.Get("RaTranLabel");
			}
			else if (GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListHoTro.Contains(data.HID))
			{
				noteLabel.text = Localization.instance.Get("DangTrongBatQuaiTranMess");
			}
			else if (GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListThienCang.Contains(data.HID))
			{
				noteLabel.text = Localization.instance.Get("DangTrongThienCangTranMess");
			}
			else
			{
				noteLabel.text = string.Empty;
			}
		}
	}

	public void SetForPopupSelectNhanVat(UserInfo.HeroData data, bool isOpenScreenKyNgoUongRuou = false)
	{
		if (data == null)
		{
			return;
		}
		m_Data = data;
		avatar.Set(m_Data);
		Utils.SetLayer(avatar.gameObject.transform, "GUIPopUp", true);
		NhanVatCfg nhanVatCfg = ConfigManager.instance.m_dicNhanVats[data.Name];
		nhanVatName.text = nhanVatCfg.TenHienThi;
		if (nhanVatCfg.Hang == 3)
		{
			spPham.spriteName = "giap";
		}
		else if (nhanVatCfg.Hang == 2)
		{
			spPham.spriteName = "at";
		}
		else if (nhanVatCfg.Hang == 1)
		{
			spPham.spriteName = "binh";
		}
		UICheckbox uICheckbox = GetComponentsInChildren<UICheckbox>(true)[0];
		uICheckbox.radioButtonRoot = base.transform.parent;
		uICheckbox.optionCanBeNone = true;
		if (isOpenScreenKyNgoUongRuou)
		{
			string value = "UR_" + data.Name;
			if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay.Contains(value))
			{
				messUongRuouLabel.gameObject.SetActive(true);
				uICheckbox.gameObject.SetActive(false);
			}
			else
			{
				messUongRuouLabel.gameObject.SetActive(false);
				uICheckbox.gameObject.SetActive(true);
			}
			tiemLucUongRuouLabel.text = data.TiemLucBoSung + "/" + ConfigManager.instance.GetMaxTiemLucUongRuou(data.Level);
			if (noteLabel != null)
			{
				noteLabel.text = string.Empty;
			}
			return;
		}
		tiemLucUongRuouLabel.text = string.Empty;
		if (noteLabel != null)
		{
			if (GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListRaTran.Contains(data.HID))
			{
				noteLabel.text = Localization.instance.Get("RaTranLabel");
			}
			else if (GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListHoTro.Contains(data.HID))
			{
				noteLabel.text = Localization.instance.Get("DangTrongBatQuaiTranMess");
			}
			else if (GameManager.instance.m_GameClient.UserInfo.DoiHinh.ListThienCang.Contains(data.HID))
			{
				noteLabel.text = Localization.instance.Get("DangTrongThienCangTranMess");
			}
			else
			{
				noteLabel.text = string.Empty;
			}
		}
	}

	public void OnClick_NVAvatar()
	{
		if (m_Data != null)
		{
			PopupNhanVat.CreateByNormalScreens(GameManager.instance.m_GameClient.UserInfo, m_Data);
		}
	}
}

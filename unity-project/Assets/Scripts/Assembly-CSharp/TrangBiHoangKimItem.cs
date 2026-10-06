using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrangBiHoangKimItem : MonoBehaviour
{
	public OtherAvatar avatar;

	public UILabel trangBiName;

	public UILabel noteLabel;

	public UISprite spChiSo1;

	public UILabel lbChiSo1;

	public UISprite spChiSo2;

	public UILabel lbChiSo2;

	public GameObject focusItem;

	public UISprite bgFocusItem;

	public UIButton btnLuyenHoa;

	public int TrangBiID;

	public UISprite spPham;

	public UserInfo.TrangBiData m_Data;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetData(UserInfo.TrangBiData data)
	{
		if (data == null)
		{
			return;
		}
		m_Data = data;
		avatar.SetTrangBiAvatar(m_Data);
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
		List<int> chiSo = trangBiCfg.GetChiSo(data);
		switch (TrangBiCfg.GetLoaiTrangBi(data.Name))
		{
		case LoaiTrangBi.Mu:
			spChiSo1.spriteName = "icon_noi_goc";
			lbChiSo1.text = chiSo[3].ToString();
			break;
		case LoaiTrangBi.VuKhi:
			spChiSo1.spriteName = "icon_ngoai";
			lbChiSo1.text = chiSo[1].ToString();
			break;
		case LoaiTrangBi.AoGiap:
			spChiSo1.spriteName = "icon_than";
			lbChiSo1.text = chiSo[2].ToString();
			break;
		case LoaiTrangBi.TrangSuc:
			spChiSo1.spriteName = "icon_menh";
			lbChiSo1.text = chiSo[0].ToString();
			break;
		}
		if (m_Data.MenhBoiDuong == m_Data.NgoaiBoiDuong && m_Data.MenhBoiDuong == 100 && ((m_Data.ThanBoiDuong == m_Data.KhiBoiDuong) & (m_Data.ThanBoiDuong == 100)))
		{
			spChiSo2.gameObject.SetActive(false);
			lbChiSo2.text = string.Empty;
			return;
		}
		spChiSo2.gameObject.SetActive(true);
		switch (getChiSoThu2(data))
		{
		case ChiSoCoBan.Menh:
			spChiSo2.spriteName = "icon_menh";
			lbChiSo2.text = data.MenhBoiDuong.ToString();
			break;
		case ChiSoCoBan.Ngoai:
			spChiSo2.spriteName = "icon_ngoai";
			lbChiSo2.text = data.NgoaiBoiDuong.ToString();
			break;
		case ChiSoCoBan.ThanPhap:
			spChiSo2.spriteName = "icon_than";
			lbChiSo2.text = data.ThanBoiDuong.ToString();
			break;
		case ChiSoCoBan.Noi:
			spChiSo2.spriteName = "icon_noi_goc";
			lbChiSo2.text = data.KhiBoiDuong.ToString();
			break;
		case ChiSoCoBan.None:
			spChiSo2.spriteName = "icon_noi_goc";
			lbChiSo2.text = "0";
			break;
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

	public ChiSoCoBan getChiSoThu2(UserInfo.TrangBiData tbData)
	{
		ChiSoCoBan result = ChiSoCoBan.None;
		List<int> list = new List<int>();
		list.Add(tbData.MenhBoiDuong);
		list.Add(tbData.NgoaiBoiDuong);
		list.Add(tbData.ThanBoiDuong);
		list.Add(tbData.KhiBoiDuong);
		switch (TrangBiCfg.GetLoaiTrangBi(tbData.Name))
		{
		case LoaiTrangBi.Mu:
			list.RemoveAt(3);
			break;
		case LoaiTrangBi.VuKhi:
			list.RemoveAt(1);
			break;
		case LoaiTrangBi.AoGiap:
			list.RemoveAt(2);
			break;
		case LoaiTrangBi.TrangSuc:
			list.RemoveAt(0);
			break;
		}
		int num = list.Max();
		if (num == tbData.MenhBoiDuong)
		{
			return ChiSoCoBan.Menh;
		}
		if (num == tbData.NgoaiBoiDuong)
		{
			return ChiSoCoBan.Ngoai;
		}
		if (num == tbData.ThanBoiDuong)
		{
			return ChiSoCoBan.ThanPhap;
		}
		if (num == tbData.KhiBoiDuong)
		{
			return ChiSoCoBan.Noi;
		}
		return result;
	}
}

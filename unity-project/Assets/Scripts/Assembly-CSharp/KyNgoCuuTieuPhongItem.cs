using UnityEngine;

public class KyNgoCuuTieuPhongItem : MonoBehaviour
{
	public UILabel lbDieuKien;

	public UILabel lbThieu;

	public OtherAvatar phanThuongAvartar;

	public bool isDuDieuKien;

	public int requestInx = -1;

	public UIButton btnNhan;

	public OtherCfg.CuuVienTieuPhongCfg m_Data;

	public void Set(OtherCfg.CuuVienTieuPhongCfg cfg, int index)
	{
		if (cfg != null)
		{
			m_Data = cfg;
			requestInx = index;
			setData();
		}
	}

	public void setData()
	{
		btnNhan.gameObject.SetActive(false);
		lbThieu.text = string.Empty;
		if (m_Data.SoTrangBiGiap > 0)
		{
			lbDieuKien.text = string.Format(Localization.instance.Get("CanSoLuongDeTuGiapLabel"), m_Data.SoDeTuGiap) + "\n" + string.Format(Localization.instance.Get("CanSoLuongTrangBiGiapLabel"), m_Data.SoTrangBiGiap);
		}
		else
		{
			lbDieuKien.text = string.Format(Localization.instance.Get("CanSoLuongDeTuGiapLabel"), m_Data.SoDeTuGiap);
		}
		int soLuongDeTuGiap = ConfigManager.instance.GetSoLuongDeTuGiap(GameManager.instance.m_GameClient.UserInfo.HeroList);
		int soLuongTrangBiGiap = ConfigManager.instance.GetSoLuongTrangBiGiap(GameManager.instance.m_GameClient.UserInfo.TrangBiList);
		if (m_Data.SoTrangBiGiap > 0)
		{
			lbDieuKien.text = string.Format(Localization.instance.Get("CanSoLuongDeTuGiapLabel"), m_Data.SoDeTuGiap) + "\n" + string.Format(Localization.instance.Get("CanSoLuongTrangBiGiapLabel"), m_Data.SoTrangBiGiap);
		}
		else
		{
			lbDieuKien.text = string.Format(Localization.instance.Get("CanSoLuongDeTuGiapLabel"), m_Data.SoDeTuGiap);
		}
		if (soLuongDeTuGiap < m_Data.SoDeTuGiap)
		{
			if (m_Data.SoTrangBiGiap > 0)
			{
				if (soLuongTrangBiGiap < m_Data.SoTrangBiGiap)
				{
					lbThieu.text = "(" + Localization.instance.Get("ThieuLabel") + " [FF0000]" + (m_Data.SoDeTuGiap - soLuongDeTuGiap) + "[-])\n(" + Localization.instance.Get("ThieuLabel") + " [FF0000]" + (m_Data.SoTrangBiGiap - soLuongTrangBiGiap) + "[-])";
				}
				else
				{
					lbThieu.text = "(" + Localization.instance.Get("ThieuLabel") + " [FF0000]" + (m_Data.SoDeTuGiap - soLuongDeTuGiap) + "[-])\n(" + Localization.instance.Get("DaDuTrangBiCuuVienLabel") + ")";
				}
			}
			else
			{
				lbThieu.text = "(" + Localization.instance.Get("ThieuLabel") + " [FF0000]" + (m_Data.SoDeTuGiap - soLuongDeTuGiap) + "[-])";
			}
			btnNhan.gameObject.SetActive(false);
		}
		if (soLuongDeTuGiap >= m_Data.SoDeTuGiap)
		{
			btnNhan.gameObject.SetActive(false);
			if (m_Data.SoTrangBiGiap > 0)
			{
				if (soLuongTrangBiGiap < m_Data.SoTrangBiGiap)
				{
					lbThieu.text = "(" + Localization.instance.Get("DaDuTrangBiCuuVienLabel") + ")\n(" + Localization.instance.Get("ThieuLabel") + " [FF0000]" + (m_Data.SoTrangBiGiap - soLuongTrangBiGiap) + "[-])";
				}
				else
				{
					isDuDieuKien = true;
					btnNhan.gameObject.SetActive(true);
				}
			}
			else
			{
				isDuDieuKien = true;
				btnNhan.gameObject.SetActive(true);
			}
		}
		string value = string.Format("CuuTieuPhong{0}", requestInx);
		if (GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChu.Contains(value))
		{
			lbThieu.text = Localization.instance.Get("DaNhanMess");
			btnNhan.gameObject.SetActive(false);
		}
		PhanThuongResponse.PhanThuong phanThuong = m_Data.PhanThuong;
		if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.BAC)
		{
			phanThuongAvartar.SetBac(phanThuong.Count, true);
		}
		else if (phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
		{
			phanThuongAvartar.SetVang(phanThuong.Count, true);
		}
		else
		{
			phanThuongAvartar.Set(phanThuong.Name);
		}
		EGDebug.Log("cfg: " + m_Data.SoDeTuGiap + " - " + m_Data.SoTrangBiGiap);
	}

	public void OnAvatarClick()
	{
		PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
		phanThuongResponse.PhanThuongList.Clear();
		phanThuongResponse.PhanThuongList.Add(m_Data.PhanThuong);
		PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), phanThuongResponse);
	}
}

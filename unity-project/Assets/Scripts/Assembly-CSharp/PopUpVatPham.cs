using System.Collections.Generic;
using UnityEngine;

public class PopUpVatPham : MonoBehaviour
{
	public OtherAvatar vatPhamAvatar;

	public UILabel vatPhamName;

	public UILabel soLuongHienCo;

	public UILabel lbDescription;

	public GameObject ChiTietBtn;

	private UserInfo.VatPhamTieuThuData m_VatPhamData;

	public static PopUpVatPham instance;

	public void OnCloseClick()
	{
		DestroyPopup();
	}

	public static void DestroyPopup()
	{
		if (instance != null)
		{
			Object.Destroy(instance.gameObject);
			instance = null;
		}
	}

	public static void Create(UserInfo.VatPhamTieuThuData data)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupVatPham"))).GetComponent<PopUpVatPham>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.Set(data);
	}

	public static void CreateByConfigData(VatPhamTieuThuCfg data)
	{
		DestroyPopup();
		instance = ((GameObject)Object.Instantiate(Resources.Load("Popup/PopupVatPham"))).GetComponent<PopUpVatPham>();
		PopupManager.instance.Add(instance.gameObject);
		instance.transform.localScale = new Vector3(1f, 1f, 1f);
		instance.SetByConfig(data);
	}

	public void SetByConfig(VatPhamTieuThuCfg data)
	{
		if (data != null)
		{
			vatPhamAvatar.Set(data.Name);
			vatPhamName.text = data.TenHienThi;
			soLuongHienCo.text = string.Empty;
			lbDescription.text = data.MoTa;
			if (data.Name.StartsWith("VP_RUONGTHAN_"))
			{
				NGUITools.SetActive(ChiTietBtn, true);
			}
			else
			{
				NGUITools.SetActive(ChiTietBtn, false);
			}
		}
	}

	public void Set(UserInfo.VatPhamTieuThuData data)
	{
		m_VatPhamData = data;
		vatPhamAvatar.Set(data);
		VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu[data.Name];
		vatPhamName.text = vatPhamTieuThuCfg.TenHienThi;
		soLuongHienCo.text = Localization.instance.Get("SoLuongLabel") + ": " + data.Quantity;
		lbDescription.text = vatPhamTieuThuCfg.MoTa;
		if (data.Name.StartsWith("VP_RUONGTHAN_") || data.Name == "VP_RUONG_THAN_BI" || data.Name.StartsWith("VP_TUITHAN"))
		{
			NGUITools.SetActive(ChiTietBtn, true);
		}
		else
		{
			NGUITools.SetActive(ChiTietBtn, false);
		}
	}

	public void OnChiTietClick()
	{
		if (m_VatPhamData == null)
		{
			return;
		}
		if (m_VatPhamData.Name == "VP_RUONG_THAN_BI")
		{
			VatPhamTieuThuCfg vatPhamTieuThuCfg = ConfigManager.instance.m_dicVatPhamTieuThu[m_VatPhamData.Name];
			if (GameManager.instance.m_GameClient.UserInfo.ServerInfo.RuongThanBiConfig != null)
			{
				PopupRuongThanBiDetail.Create(m_VatPhamData, GameManager.instance.m_GameClient.UserInfo.ServerInfo.RuongThanBiConfig);
			}
			return;
		}
		if (m_VatPhamData.Name.StartsWith("VP_TUITHAN"))
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			if (ConfigManager.instance.OtherConfig.TuiThanList != null)
			{
				for (int i = 0; i < ConfigManager.instance.OtherConfig.TuiThanList.Count; i++)
				{
					if (ConfigManager.instance.OtherConfig.TuiThanList[i].CodeName == m_VatPhamData.Name)
					{
						phanThuongResponse.PhanThuongList = ConfigManager.instance.OtherConfig.TuiThanList[i].PhanThuongList;
					}
				}
			}
			PopupDanhSachPhanThuong.Create(ConfigManager.instance.m_dicVatPhamTieuThu[m_VatPhamData.Name].TenHienThi, Localization.instance.Get("HopThanKhaiMo"), phanThuongResponse);
			return;
		}
		List<string> list = ConfigManager.instance.OtherConfig.HopThanList[m_VatPhamData.Name];
		PhanThuongResponse phanThuongResponse2 = new PhanThuongResponse();
		foreach (string item in list)
		{
			PhanThuongResponse.PhanThuong phanThuong = new PhanThuongResponse.PhanThuong();
			phanThuong.Level = 1;
			phanThuong.Count = 1;
			phanThuong.Name = item;
			if (item.StartsWith("VC_"))
			{
				phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.VO_CONG;
			}
			else if (item.StartsWith("MVC_"))
			{
				phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG;
			}
			else if (item.StartsWith("VK_") || item.StartsWith("MU_") || item.StartsWith("AG_") || item.StartsWith("TS_"))
			{
				phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.TRANG_BI;
			}
			else if (item.StartsWith("MVK_") || item.StartsWith("MMU_") || item.StartsWith("MAG_") || item.StartsWith("MTS_"))
			{
				phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI;
			}
			else if (item.StartsWith("NGUA_"))
			{
				phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.THU_CUOI;
			}
			else if (item.StartsWith("NV_"))
			{
				phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT;
			}
			else if (item.StartsWith("PET_"))
			{
				phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.THAN_THU;
			}
			else if (item.StartsWith("NK_"))
			{
				phanThuong.Loai = PhanThuongResponse.LoaiPhanThuong.NGUYEN_KHI;
			}
			phanThuongResponse2.PhanThuongList.Add(phanThuong);
		}
		PopupDanhSachPhanThuong.Create(ConfigManager.instance.m_dicVatPhamTieuThu[m_VatPhamData.Name].TenHienThi, Localization.instance.Get("HopThanKhaiMo"), phanThuongResponse2);
	}
}

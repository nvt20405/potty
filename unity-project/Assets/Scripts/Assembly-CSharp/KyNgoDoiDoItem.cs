using System.Collections.Generic;
using UnityEngine;

public class KyNgoDoiDoItem : MonoBehaviour
{
	public PhanThuongItem phanThuongItem1;

	public PhanThuongItem phanThuongItem2;

	public PhanThuongItem phanThuongItem3;

	public UILabel lbInfo1;

	public UILabel lbInfo2;

	public UILabel lbInfo3;

	public UISprite spHopQua;

	public UIButton btnDoiDo;

	public int indexDoiDoCfg;

	public UILabel lbLuotDoiDo;

	public int mMaxLuot;

	public OtherAvatar otherAva1;

	public OtherAvatar otherAva2;

	public OtherAvatar otherAva3;

	private List<PhanThuongResponse.PhanThuong> listPhanThuongData;

	public List<PhanThuongResponse.PhanThuong> listItemData;

	public void setData(int index)
	{
		indexDoiDoCfg = index;
		phanThuongItem1.setNullValue();
		phanThuongItem2.setNullValue();
		phanThuongItem3.setNullValue();
		lbLuotDoiDo.text = string.Empty;
		lbInfo1.text = string.Empty;
		lbInfo2.text = string.Empty;
		lbInfo3.text = string.Empty;
		mMaxLuot = 0;
		if (indexDoiDoCfg >= 0)
		{
			getData(indexDoiDoCfg + 1);
		}
	}

	private void getData(int indexCfg)
	{
		switch (indexCfg)
		{
		case 1:
			displayInfo(GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Ds1, GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Pt1, GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Max1);
			break;
		case 2:
			displayInfo(GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Ds2, GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Pt2, GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Max2);
			break;
		case 3:
			displayInfo(GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Ds3, GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Pt3, GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Max3);
			break;
		case 4:
			displayInfo(GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Ds4, GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Pt4, GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Max4);
			break;
		}
	}

	private void displayInfo(List<PhanThuongResponse.PhanThuong> ds, List<PhanThuongResponse.PhanThuong> listPhanThuong, int maxLuot)
	{
		listPhanThuongData = listPhanThuong;
		listItemData = ds;
		mMaxLuot = maxLuot;
		if (ds != null)
		{
			if (ds.Count >= 1 && ds[0] != null)
			{
				phanThuongItem1.Set(ds[0], false);
				if (ds[0].Loai == PhanThuongResponse.LoaiPhanThuong.VO_CONG || ds[0].Loai == PhanThuongResponse.LoaiPhanThuong.TRANG_BI)
				{
					lbInfo1.text = Localization.instance.Get("CapLabel") + ": " + ds[0].Level;
				}
				else
				{
					if (ds[0].Loai != PhanThuongResponse.LoaiPhanThuong.VANG && ds[0].Loai != PhanThuongResponse.LoaiPhanThuong.BAC)
					{
						lbInfo1.text = getSoLuongDangCo(ds[0].Loai, ds[0].Name) + "/" + ds[0].Count;
					}
					else
					{
						lbInfo1.text = "x " + ds[0].Count;
					}
					if (ds[0].Loai == PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI)
					{
						otherAva1.OnEventClick = onOtherAvatarClick1;
					}
				}
			}
			if (ds.Count >= 2 && ds[1] != null)
			{
				phanThuongItem2.Set(ds[1], false);
				if (ds[1].Loai == PhanThuongResponse.LoaiPhanThuong.VO_CONG || ds[1].Loai == PhanThuongResponse.LoaiPhanThuong.TRANG_BI)
				{
					lbInfo2.text = Localization.instance.Get("CapLabel") + ": " + ds[1].Level;
				}
				else
				{
					if (ds[1].Loai != PhanThuongResponse.LoaiPhanThuong.VANG && ds[1].Loai != PhanThuongResponse.LoaiPhanThuong.BAC)
					{
						lbInfo2.text = getSoLuongDangCo(ds[1].Loai, ds[1].Name) + "/" + ds[1].Count;
					}
					else
					{
						lbInfo2.text = "x " + ds[1].Count;
					}
					if (ds[1].Loai == PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI)
					{
						otherAva2.OnEventClick = onOtherAvatarClick2;
					}
				}
			}
			if (ds.Count >= 3 && ds[2] != null)
			{
				phanThuongItem3.Set(ds[2], false);
				if (ds[2].Loai == PhanThuongResponse.LoaiPhanThuong.VO_CONG || ds[2].Loai == PhanThuongResponse.LoaiPhanThuong.TRANG_BI)
				{
					lbInfo3.text = Localization.instance.Get("CapLabel") + ": " + ds[2].Level;
				}
				else
				{
					if (ds[2].Loai != PhanThuongResponse.LoaiPhanThuong.VANG && ds[2].Loai != PhanThuongResponse.LoaiPhanThuong.BAC)
					{
						lbInfo3.text = getSoLuongDangCo(ds[2].Loai, ds[2].Name) + "/" + ds[2].Count;
					}
					else
					{
						lbInfo3.text = "x " + ds[2].Count;
					}
					if (ds[2].Loai == PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI)
					{
						otherAva3.OnEventClick = onOtherAvatarClick3;
					}
				}
			}
		}
		int soLuotDoiDoByString = ConfigManager.instance.GetSoLuotDoiDoByString(GameManager.instance.m_GameClient.UserInfo.Gamer.GhiChuTrongNgay, indexDoiDoCfg);
		lbLuotDoiDo.text = soLuotDoiDoByString + "/" + maxLuot;
	}

	private int getSoLuongDangCo(PhanThuongResponse.LoaiPhanThuong loai, string codeName)
	{
		switch (loai)
		{
		case PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU:
		{
			UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == codeName);
			return (vatPhamTieuThuData != null) ? vatPhamTieuThuData.Quantity : 0;
		}
		case PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI:
		{
			UserInfo.ManhTrangBiData manhTrangBiData = GameManager.instance.m_GameClient.UserInfo.ManhTrangBiList.Find((UserInfo.ManhTrangBiData e) => e.Name == codeName);
			return (manhTrangBiData != null) ? manhTrangBiData.Quantity : 0;
		}
		case PhanThuongResponse.LoaiPhanThuong.MANH_VO_CONG:
		{
			UserInfo.ManhVoCongData manhVoCongData = GameManager.instance.m_GameClient.UserInfo.ManhVoCongList.Find((UserInfo.ManhVoCongData e) => e.Name == codeName);
			return (manhVoCongData != null) ? manhVoCongData.Quantity : 0;
		}
		case PhanThuongResponse.LoaiPhanThuong.HON_NHAN_VAT:
		{
			UserInfo.HonNhanVatData honNhanVatData = GameManager.instance.m_GameClient.UserInfo.HonNhanVatList.Find((UserInfo.HonNhanVatData e) => e.Name == codeName);
			return (honNhanVatData != null) ? honNhanVatData.Quantity : 0;
		}
		default:
			return 0;
		}
	}

	public void onOtherAvatarClick1(OtherAvatar go)
	{
		if (indexDoiDoCfg >= 0)
		{
			PhanThuongResponse.PhanThuong phanThuong = null;
			switch (indexDoiDoCfg)
			{
			case 0:
				phanThuong = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Ds1[0];
				break;
			case 1:
				phanThuong = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Ds2[0];
				break;
			case 2:
				phanThuong = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Ds3[0];
				break;
			case 3:
				phanThuong = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Ds4[0];
				break;
			}
			if (phanThuong != null && phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI)
			{
				PopUpManhTrangBi.Create(phanThuong.Name, getSoLuongDangCo(phanThuong.Loai, phanThuong.Name));
			}
		}
	}

	public void onOtherAvatarClick2(OtherAvatar go)
	{
		if (indexDoiDoCfg >= 0)
		{
			PhanThuongResponse.PhanThuong phanThuong = null;
			switch (indexDoiDoCfg)
			{
			case 0:
				phanThuong = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Ds1[1];
				break;
			case 1:
				phanThuong = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Ds2[1];
				break;
			case 2:
				phanThuong = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Ds3[1];
				break;
			case 3:
				phanThuong = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Ds4[1];
				break;
			}
			if (phanThuong != null && phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI)
			{
				PopUpManhTrangBi.Create(phanThuong.Name, getSoLuongDangCo(phanThuong.Loai, phanThuong.Name));
			}
		}
	}

	public void onOtherAvatarClick3(OtherAvatar go)
	{
		if (indexDoiDoCfg >= 0)
		{
			PhanThuongResponse.PhanThuong phanThuong = null;
			switch (indexDoiDoCfg)
			{
			case 0:
				phanThuong = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Ds1[2];
				break;
			case 1:
				phanThuong = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Ds2[2];
				break;
			case 2:
				phanThuong = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Ds3[2];
				break;
			case 3:
				phanThuong = GameManager.instance.m_GameClient.UserInfo.ServerInfo.DoiDoCfg.Ds4[2];
				break;
			}
			if (phanThuong != null && phanThuong.Loai == PhanThuongResponse.LoaiPhanThuong.MANH_TRANG_BI)
			{
				PopUpManhTrangBi.Create(phanThuong.Name, getSoLuongDangCo(phanThuong.Loai, phanThuong.Name));
			}
		}
	}

	public void btnHopQua_OnClick()
	{
		if (listPhanThuongData != null)
		{
			PhanThuongResponse phanThuongResponse = new PhanThuongResponse();
			phanThuongResponse.PhanThuongList = listPhanThuongData;
			PopupDanhSachPhanThuong.Create(Localization.instance.Get("PhanThuongMess"), Localization.instance.Get("PhanThuongSeNhanDuoc"), phanThuongResponse);
		}
	}
}

using UnityEngine;

public class HoaVangItem : MonoBehaviour
{
	public PhanThuongItem avatar;

	public UILabel lbDiemNhan;

	public UILabel lbYeuCau;

	public UILabel lbHienCo;

	public int curSoLuong;

	public int curIndex = -1;

	public PhanThuongResponse.PhanThuong m_Data;

	public void setDataItem(PhanThuongResponse.PhanThuong data, int index)
	{
		if (data != null)
		{
			m_Data = data;
			curIndex = index;
			avatar.Set(data, false, true);
			lbDiemNhan.text = data.TyLe.ToString();
			lbYeuCau.text = Localization.instance.Get("SoLuongYeuCauLabel") + ": " + data.Count;
			switch (data.Loai)
			{
			case PhanThuongResponse.LoaiPhanThuong.TRANG_BI:
				curSoLuong = getCountTrangBi(data.Name);
				break;
			case PhanThuongResponse.LoaiPhanThuong.VAT_PHAM_TIEU_THU:
				curSoLuong = getCountVatPham(data.Name);
				break;
			}
			lbHienCo.text = Localization.instance.Get("HienCoHoaVangLabel") + " " + curSoLuong;
		}
	}

	public int getCountTrangBi(string Name)
	{
		int num = 0;
		for (int i = 0; i < GameManager.instance.m_GameClient.UserInfo.TrangBiList.Count; i++)
		{
			UserInfo.TrangBiData trangBiData = GameManager.instance.m_GameClient.UserInfo.TrangBiList[i];
			if (trangBiData != null && trangBiData.Name == Name && trangBiData.HID <= 0)
			{
				num++;
			}
		}
		return num;
	}

	public int getCountVatPham(string Name)
	{
		UserInfo.VatPhamTieuThuData vatPhamTieuThuData = GameManager.instance.m_GameClient.UserInfo.VatPhamTieuThuList.Find((UserInfo.VatPhamTieuThuData e) => e.Name == Name);
		return (vatPhamTieuThuData != null) ? vatPhamTieuThuData.Quantity : 0;
	}
}

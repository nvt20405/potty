using UnityEngine;

public class CuaHangThanBiItem : MonoBehaviour
{
	public PhanThuongItem avatar;

	public UILabel lbVatPhamName;

	public UILabel lbDescription;

	public UILabel lbGiaBan;

	public UIButton btnMua;

	public UILabel lbGiaBanTitle;

	public int vatPhamID;

	public CuaHangThanBiCfg m_Data;

	public int Index = -1;

	public void Set(CuaHangThanBiCfg data, int index)
	{
		if (data != null)
		{
			m_Data = data;
			Index = index;
			displayInfo();
		}
	}

	public void displayInfo()
	{
		if (m_Data.Loai == PhanThuongResponse.LoaiPhanThuong.BAC)
		{
			avatar.setBac(m_Data.Count, true);
		}
		else if (m_Data.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
		{
			avatar.setVang(m_Data.Count, true);
		}
		else
		{
			PhanThuongResponse.PhanThuong phanThuong = new PhanThuongResponse.PhanThuong();
			phanThuong.Name = m_Data.CodeName;
			phanThuong.Level = m_Data.Level;
			phanThuong.Count = m_Data.Count;
			phanThuong.Loai = m_Data.Loai;
			avatar.Set(phanThuong, false, true);
		}
		lbVatPhamName.text = m_Data.TenHienThi;
		lbDescription.text = m_Data.MoTa;
		lbGiaBan.text = m_Data.DiemCan.ToString();
		lbGiaBanTitle.text = Localization.instance.Get("GiaBanLabel") + ":";
	}
}

using UnityEngine;

public class TayVucThuongNhanItem : MonoBehaviour
{
	public PhanThuongItem avatar;

	public UILabel lbVatPhamName;

	public UILabel lbGiaBan;

	public UIButton btnMua;

	public UILabel lbGiaBanTitle;

	public int vatPhamID;

	public TayVucInfoResponse.TayVucData m_Data;

	public int Index = -1;

	public void Set(TayVucInfoResponse.TayVucData data, int index)
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
		if (m_Data.pt.Loai == PhanThuongResponse.LoaiPhanThuong.BAC)
		{
			avatar.setBac(m_Data.pt.Count, true);
		}
		else if (m_Data.pt.Loai == PhanThuongResponse.LoaiPhanThuong.VANG)
		{
			avatar.setVang(m_Data.pt.Count, true);
		}
		else
		{
			avatar.Set(m_Data.pt, false, true);
		}
		lbGiaBan.text = m_Data.GiaBan.ToString();
		lbGiaBanTitle.text = Localization.instance.Get("GiaBanLabel") + ":";
		if (m_Data.isBuy)
		{
			btnMua.gameObject.SetActive(false);
		}
		else
		{
			btnMua.gameObject.SetActive(true);
		}
	}
}
